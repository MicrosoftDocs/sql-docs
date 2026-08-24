---
title: Use mssql-python with FastAPI
description: Learn how to build REST APIs with FastAPI and mssql-python for Microsoft SQL and Azure SQL database access.
author: dlevy-msft-sql
ms.author: dlevy
ms.date: 07/17/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---

# Use mssql-python with FastAPI

FastAPI is a modern Python web framework for building APIs. Combined with mssql-python, you can build high-performance REST APIs backed by Microsoft SQL and Azure SQL Database.

## Prerequisites

- Python 3.10 or later.
- The `mssql-python`, `fastapi`, `uvicorn`, `pydantic`, and `PyJWT` packages. Install all with `pip install fastapi uvicorn mssql-python pydantic pyjwt`.
- [!INCLUDE [prereq-linux-macos](includes/prereq-linux-macos.md)]

[!INCLUDE [prereq-create-sql-database](includes/prereq-create-sql-database.md)]

The examples in this article use the **AdventureWorksLT** sample database, specifically the `SalesLT.Product` table. If you don't have AdventureWorksLT installed, see [AdventureWorks sample databases](/sql/samples/adventureworks-install-configure).

## Project setup

### Create a virtual environment

Create and activate a virtual environment so this project's packages stay isolated from other Python installations. This step also prevents the common problem of installing packages into one interpreter while running your app or tests with another.

# [Windows](#tab/windows)

```pwsh
py -m venv .venv
.\.venv\Scripts\Activate.ps1
```

# [Linux/macOS](#tab/linux-macos)

```bash
python3 -m venv .venv
source .venv/bin/activate
```

---

After you activate the environment, `python`, `pip`, and `pytest` all resolve to the same interpreter. Run the remaining commands in this article from the activated environment.

> [!NOTE]
> On Windows on Arm, create the environment with an Arm64 build of Python so `mssql-python` and its dependencies install from prebuilt wheels. On a machine with more than one Python version, `py -m venv` might select a different version or architecture than you expect, so verify with `python -c "import sys, sysconfig; print(sys.version, sysconfig.get_platform())"` after you activate. If `pip` tries to build `cryptography` from source (a Rust and OpenSSL toolchain error), install a wheel-backed version first with `pip install --only-binary=:all: cryptography`, then install the rest.

### Install dependencies

Install the required packages with pip:

```bash
pip install fastapi uvicorn mssql-python pydantic pyjwt
```

### Project structure

Organize your project with separate modules for database, schemas, and CRUD operations:

```text
my_api/
├── main.py
├── database.py
├── models.py
├── schemas.py
├── crud.py
├── test_api.py
└── routers/
    └── products.py
```

## Database connection management

FastAPI uses dependency injection to provide resources like database connections to route handlers. The pattern in this section creates a context manager that opens a connection, yields a cursor, and handles commit/rollback/close automatically.

### Create database.py

The `get_connection_string()` function builds the ODBC connection string from configuration values. The `get_db()` context manager and `get_db_dependency()` generator both follow the same pattern: open a connection, yield a cursor, commit on success, roll back on error, and always close when done. FastAPI's `Depends()` calls `get_db_dependency()` once per request and manages its lifecycle.

```python
# database.py
import mssql_python
from contextlib import contextmanager
from typing import Generator

# Configuration
DATABASE_CONFIG = {
    "server": "<server>.database.windows.net",
    "database": "<database>",
}

def get_connection_string() -> str:
    """Build connection string from config."""
    return (
        f"Server={DATABASE_CONFIG['server']};"
        f"Database={DATABASE_CONFIG['database']};"
        "Authentication=ActiveDirectoryDefault;"
        "Encrypt=yes"
    )
```

> [!NOTE]
> `ActiveDirectoryDefault` uses `DefaultAzureCredential`, which tries multiple credential providers in sequence. The first connection can be slow because the SDK walks the chain until it finds a working provider. In production, if you know which credential type your environment uses, specify it directly (for example, `ActiveDirectoryMSI` for managed identity) to avoid the chain walk. For more information, see [Microsoft Entra authentication](entra-authentication.md).

```python
@contextmanager
def get_db() -> Generator:
    """Database connection context manager for FastAPI dependency injection."""
    conn = mssql_python.connect(get_connection_string())
    cursor = conn.cursor()
    try:
        yield cursor
        conn.commit()
    except Exception:
        conn.rollback()
        raise
    finally:
        cursor.close()
        conn.close()

def get_db_dependency():
    """FastAPI dependency for database cursor."""
    conn = mssql_python.connect(get_connection_string())
    cursor = conn.cursor()
    try:
        yield cursor
        conn.commit()
    except Exception:
        conn.rollback()
        raise
    finally:
        cursor.close()
        conn.close()
```

## Pydantic models

Pydantic models define the shape and validation rules for request and response data. FastAPI uses these models to parse incoming JSON, validate field constraints, and generate OpenAPI documentation automatically.

### Create schemas.py

Separate schemas into `Base`, `Create`, `Update`, and response variants. The `Base` schema holds shared fields, `Create` inherits from it for insert operations, and `Update` makes all fields optional for partial updates.

```python
# schemas.py
from pydantic import BaseModel, ConfigDict, EmailStr, Field
from typing import Optional
from datetime import datetime

# Product schemas
class ProductBase(BaseModel):
    name: str = Field(..., min_length=1, max_length=100)
    product_number: str = Field(..., min_length=1, max_length=25)
    price: float = Field(..., gt=0)
    color: Optional[str] = Field(None, max_length=50)
    size: Optional[str] = Field(None, max_length=50)
    category_id: Optional[int] = None

class ProductCreate(ProductBase):
    pass

class ProductUpdate(BaseModel):
    name: Optional[str] = Field(None, min_length=1, max_length=100)
    product_number: Optional[str] = Field(None, min_length=1, max_length=25)
    price: Optional[float] = Field(None, gt=0)
    color: Optional[str] = Field(None, max_length=50)
    size: Optional[str] = Field(None, max_length=50)
    category_id: Optional[int] = None

class Product(ProductBase):
    id: int

    model_config = ConfigDict(from_attributes=True)

# Pagination
class PaginatedResponse(BaseModel):
    items: list
    total: int
    page: int
    page_size: int
    pages: int
```

## CRUD operations

Encapsulate database queries in a dedicated class to keep route handlers thin. Each static method takes a cursor (injected by FastAPI) and handles one operation using [parameterized queries](parameterized-queries.md) (`%(name)s` placeholders with a dictionary of values) to prevent SQL injection. This separation makes the business logic easier to test and reuse.

### Create crud.py

```python
# crud.py
from typing import Optional, List
from schemas import ProductCreate, ProductUpdate, Product

class ProductCRUD:
    """CRUD operations for products."""
    
    @staticmethod
    def get(cursor, product_id: int) -> Optional[dict]:
        cursor.execute("""
            SELECT ProductID, Name, ProductNumber, ListPrice, Color, Size
            FROM SalesLT.Product
            WHERE ProductID = %(id)s
        """, {"id": product_id})
        
        row = cursor.fetchone()
        if row:
            return {
                "id": row.ProductID,
                "name": row.Name,
                "product_number": row.ProductNumber,
                "price": float(row.ListPrice),
                "color": row.Color,
                "size": row.Size
            }
        return None
    
    @staticmethod
    def get_all(cursor, skip: int = 0, limit: int = 100) -> List[dict]:
        cursor.execute("""
            SELECT ProductID, Name, ProductNumber, ListPrice, Color, Size
            FROM SalesLT.Product
            ORDER BY ProductID
            OFFSET %(skip)s ROWS
            FETCH NEXT %(limit)s ROWS ONLY
        """, {"skip": skip, "limit": limit})
        
        return [{
            "id": row.ProductID,
            "name": row.Name,
            "product_number": row.ProductNumber,
            "price": float(row.ListPrice),
            "color": row.Color,
            "size": row.Size
        } for row in cursor.fetchall()]
    
    @staticmethod
    def count(cursor) -> int:
        cursor.execute("SELECT COUNT(*) FROM SalesLT.Product")
        return cursor.fetchval()
    
    @staticmethod
    def create(cursor, product: ProductCreate) -> dict:
        cursor.execute("""
            INSERT INTO SalesLT.Product (Name, ProductNumber, ListPrice, Color, Size, ProductCategoryID, StandardCost, SellStartDate)
            OUTPUT INSERTED.ProductID, INSERTED.Name, INSERTED.ProductNumber,
                   INSERTED.ListPrice, INSERTED.Color, INSERTED.Size
            VALUES (%(name)s, %(product_number)s, %(price)s, %(color)s, %(size)s, %(category_id)s, 0, GETDATE())
        """, {
            "name": product.name,
            "product_number": product.product_number,
            "price": product.price,
            "color": product.color,
            "size": product.size,
            "category_id": product.category_id
        })
        
        row = cursor.fetchone()
        return {
            "id": row.ProductID,
            "name": row.Name,
            "product_number": row.ProductNumber,
            "price": float(row.ListPrice),
            "color": row.Color,
            "size": row.Size
        }
    
    @staticmethod
    def update(cursor, product_id: int, product: ProductUpdate) -> Optional[dict]:
        # Build dynamic update
        updates = []
        params = {"id": product_id}
        
        if product.name is not None:
            updates.append("Name = %(name)s")
            params["name"] = product.name
        if product.product_number is not None:
            updates.append("ProductNumber = %(product_number)s")
            params["product_number"] = product.product_number
        if product.price is not None:
            updates.append("ListPrice = %(price)s")
            params["price"] = product.price
        if product.category_id is not None:
            updates.append("ProductCategoryID = %(category_id)s")
            params["category_id"] = product.category_id
        
        if not updates:
            return ProductCRUD.get(cursor, product_id)
        
        cursor.execute(f"""
            UPDATE SalesLT.Product SET {', '.join(updates)}
            OUTPUT INSERTED.ProductID, INSERTED.Name, INSERTED.ProductNumber,
                   INSERTED.ListPrice, INSERTED.Color, INSERTED.Size
            WHERE ProductID = %(id)s
        """, params)
        
        row = cursor.fetchone()
        if row:
            return {
                "id": row.ProductID,
                "name": row.Name,
                "product_number": row.ProductNumber,
                "price": float(row.ListPrice),
                "color": row.Color,
                "size": row.Size
            }
        return None
    
    @staticmethod
    def delete(cursor, product_id: int) -> bool:
        cursor.execute("""
            DELETE FROM SalesLT.Product WHERE ProductID = %(id)s
        """, {"id": product_id})
        return cursor.rowcount > 0
    
    @staticmethod
    def search(cursor, query: str, skip: int = 0, limit: int = 100) -> List[dict]:
        cursor.execute("""
            SELECT ProductID, Name, ProductNumber, ListPrice, Color, Size
            FROM SalesLT.Product
            WHERE Name LIKE %(query)s OR ProductNumber LIKE %(query)s
            ORDER BY ProductID
            OFFSET %(skip)s ROWS
            FETCH NEXT %(limit)s ROWS ONLY
        """, {"query": f"%{query}%", "skip": skip, "limit": limit})
        
        return [{
            "id": row.ProductID,
            "name": row.Name,
            "product_number": row.ProductNumber,
            "price": float(row.ListPrice),
            "color": row.Color,
            "size": row.Size
        } for row in cursor.fetchall()]
```

## FastAPI application

### Create main.py

The main module wires everything together. Each route declares `cursor = Depends(get_db_dependency)`, which tells FastAPI to call the generator, pass the yielded cursor to the handler, and clean up afterward. FastAPI also validates request bodies against your Pydantic schemas before the handler runs.

```python
# main.py
from fastapi import FastAPI, HTTPException, Depends, Query
from typing import List
from database import get_db_dependency
from schemas import Product, ProductCreate, ProductUpdate, PaginatedResponse
from crud import ProductCRUD

app = FastAPI(
    title="Product API",
    description="REST API for products using mssql-python",
    version="1.0.0"
)

@app.get("/")
def root():
    return {"message": "Product API", "docs": "/docs"}

@app.get("/products", response_model=PaginatedResponse)
def list_products(
    page: int = Query(1, ge=1),
    page_size: int = Query(10, ge=1, le=100),
    cursor = Depends(get_db_dependency)
):
    """List all products with pagination."""
    skip = (page - 1) * page_size
    items = ProductCRUD.get_all(cursor, skip=skip, limit=page_size)
    total = ProductCRUD.count(cursor)
    
    return {
        "items": items,
        "total": total,
        "page": page,
        "page_size": page_size,
        "pages": (total + page_size - 1) // page_size
    }

@app.get("/products/{product_id}", response_model=Product)
def get_product(product_id: int, cursor = Depends(get_db_dependency)):
    """Get a specific product by ID."""
    product = ProductCRUD.get(cursor, product_id)
    if not product:
        raise HTTPException(status_code=404, detail="Product not found")
    return product

@app.post("/products", response_model=Product, status_code=201)
def create_product(product: ProductCreate, cursor = Depends(get_db_dependency)):
    """Create a new product."""
    return ProductCRUD.create(cursor, product)

@app.put("/products/{product_id}", response_model=Product)
def update_product(
    product_id: int,
    product: ProductUpdate,
    cursor = Depends(get_db_dependency)
):
    """Update an existing product."""
    updated = ProductCRUD.update(cursor, product_id, product)
    if not updated:
        raise HTTPException(status_code=404, detail="Product not found")
    return updated

@app.delete("/products/{product_id}", status_code=204)
def delete_product(product_id: int, cursor = Depends(get_db_dependency)):
    """Delete a product."""
    if not ProductCRUD.delete(cursor, product_id):
        raise HTTPException(status_code=404, detail="Product not found")

@app.get("/products/search/", response_model=List[Product])
def search_products(
    q: str = Query(..., min_length=1),
    page: int = Query(1, ge=1),
    page_size: int = Query(10, ge=1, le=100),
    cursor = Depends(get_db_dependency)
):
    """Search products by name or product number."""
    skip = (page - 1) * page_size
    return ProductCRUD.search(cursor, q, skip=skip, limit=page_size)

# Health check endpoint
@app.get("/health")
def health_check(cursor = Depends(get_db_dependency)):
    """Check database connectivity."""
    try:
        cursor.execute("SELECT 1")
        return {"status": "healthy", "database": "connected"}
    except Exception as e:
        raise HTTPException(status_code=503, detail=f"Database unhealthy: {str(e)}")
```

### Run the application

```bash
uvicorn main:app --reload --host 0.0.0.0 --port 8000
```

## Error handling

FastAPI lets you register global exception handlers for specific exception types. When you catch `mssql_python.DatabaseError` and `mssql_python.IntegrityError`, FastAPI returns structured JSON errors with appropriate HTTP status codes instead of generic 500 responses.

### Global exception handler

Add these handlers to `main.py`, right after the `app = FastAPI(...)` line. FastAPI runs the matching handler whenever a route raises that exception type, so you don't need a `try`/`except` block in every route.

```python
# main.py
from fastapi import Request
from fastapi.responses import JSONResponse
import mssql_python

@app.exception_handler(mssql_python.DatabaseError)
async def database_exception_handler(request: Request, exc: mssql_python.DatabaseError):
    """Handle database errors globally."""
    return JSONResponse(
        status_code=500,
        content={"detail": "Database error occurred", "type": "database_error"}
    )

@app.exception_handler(mssql_python.IntegrityError)
async def integrity_exception_handler(request: Request, exc: mssql_python.IntegrityError):
    """Handle integrity constraint violations."""
    error_msg = str(exc)
    
    if "UNIQUE" in error_msg:
        return JSONResponse(
            status_code=409,
            content={"detail": "Resource already exists", "type": "duplicate_error"}
        )
    elif "FOREIGN KEY" in error_msg:
        return JSONResponse(
            status_code=400,
            content={"detail": "Referenced resource not found", "type": "reference_error"}
        )
    
    return JSONResponse(
        status_code=400,
        content={"detail": "Data integrity error", "type": "integrity_error"}
    )
```

> [!NOTE]
> Deleting a product that other rows still reference raises `mssql_python.IntegrityError` from the foreign key constraint, and the handler returns a 400 instead of removing the row. In the AdventureWorksLT sample, most products in `SalesLT.Product` are referenced by `SalesLT.SalesOrderDetail`, so `DELETE` fails for them by design. To test a successful delete, create a product with `POST /products` and delete that one, or remove the referencing rows first.

## Connection pooling

Without connection pooling, each request opens and closes a TCP connection to Microsoft SQL, which adds latency. Connection pooling keeps a set of idle connections ready for reuse. Call `mssql_python.pooling()` once at startup. With pooling enabled, `conn.close()` in `get_db_dependency()` returns the connection to the pool instead of actually closing it.

### Enhanced database module

Enable pooling by calling `mssql_python.pooling()` at startup and configure it with appropriate max size and timeout settings:

```python
# database.py with connection pooling
import mssql_python
from contextlib import contextmanager
import os

# Configure pool
mssql_python.pooling(max_size=20, idle_timeout=300)

DATABASE_URL = os.getenv(
    "DATABASE_URL",
    "Server=<server>.database.windows.net;Database=<database>;"
    "Authentication=ActiveDirectoryDefault;Encrypt=yes"
)

def get_db_dependency():
    """FastAPI dependency with connection pooling."""
    conn = mssql_python.connect(DATABASE_URL)
    cursor = conn.cursor()
    try:
        yield cursor
        conn.commit()
    except Exception:
        conn.rollback()
        raise
    finally:
        cursor.close()
        conn.close()  # Returns to pool
```

## Authentication middleware

You can combine database access with authentication by chaining FastAPI dependencies. The following example validates a JWT bearer token, looks up the matching person record in the AdventureWorksLT sample database, and makes the result available to protected routes.

```python
# auth.py
from fastapi import Depends, HTTPException
from fastapi.security import HTTPBearer, HTTPAuthorizationCredentials
import jwt

security = HTTPBearer()

def get_current_user(
    credentials: HTTPAuthorizationCredentials = Depends(security),
    cursor = Depends(get_db_dependency)
):
    """Validate JWT and return the matching AdventureWorksLT person."""
    try:
        token = credentials.credentials
        # Replace with a strong secret loaded from environment variables
        payload = jwt.decode(token, "your-secret-key", algorithms=["HS256"])
        person_id = int(payload.get("sub"))
        
        if not person_id:
            raise HTTPException(status_code=401, detail="Invalid token")
        
        cursor.execute("""
            SELECT BusinessEntityID, FirstName, LastName
            FROM Person.Person
            WHERE BusinessEntityID = %(id)s
        """, {"id": person_id})
        
        person = cursor.fetchone()
        if not person:
            raise HTTPException(status_code=401, detail="User not found")
        
        return {
            "id": person.BusinessEntityID,
            "first_name": person.FirstName,
            "last_name": person.LastName
        }
        
    except (TypeError, ValueError):
        raise HTTPException(status_code=401, detail="Invalid token subject")
    except jwt.ExpiredSignatureError:
        raise HTTPException(status_code=401, detail="Token expired")
    except jwt.InvalidTokenError:
        raise HTTPException(status_code=401, detail="Invalid token")

# Protected endpoint
@app.get("/me")
def get_me(current_user: dict = Depends(get_current_user)):
    return current_user
```

## Testing

FastAPI provides a `TestClient` built on `httpx` that sends requests to your application without starting a real HTTP server. Write tests with `pytest` to verify routes, status codes, and response shapes.

Before running the tests in this section, install the test dependencies:

```bash
pip install pytest httpx
```

> [!NOTE]
> If you're on the latest Starlette or setting up a new environment, prefer `httpx2` over `httpx`. Recent Starlette versions use `httpx2` for `TestClient` and emit a deprecation warning when only `httpx` is installed. Install it with `pip install pytest httpx2`.

### Test setup

Create a test file that uses `TestClient` to verify route behavior and response schemas:

```python
# test_api.py
from fastapi.testclient import TestClient
from main import app
import uuid
import pytest

client = TestClient(app)

def test_list_products():
    response = client.get("/products")
    assert response.status_code == 200
    data = response.json()
    assert "items" in data
    assert "total" in data

def test_create_product():
    suffix = uuid.uuid4().hex[:8]
    name = f"Test Product {suffix}"
    product_data = {
        "name": name,
        "product_number": f"TEST-{suffix}",
        "price": 19.99,
        "color": "Red",
        "size": "M",
        "category_id": 1
    }
    response = client.post("/products", json=product_data)
    assert response.status_code == 201
    data = response.json()
    assert data["name"] == name
    assert data["price"] == 19.99

def test_get_product_not_found():
    response = client.get("/products/99999")
    assert response.status_code == 404

def test_health_check():
    response = client.get("/health")
    assert response.status_code == 200
    assert response.json()["status"] == "healthy"
```

Run the tests with `pytest` from the project root, the same directory as `main.py`:

```bash
pytest
```

These tests run against your live database rather than mocks, so `test_create_product` inserts a real row into `SalesLT.Product`. In AdventureWorksLT, both `Name` and `ProductNumber` have unique constraints, so the test generates a unique value for each on every run. If you hardcode those values instead, the test fails with a conflict on the second run unless you delete the row first.

## Deployment configuration

Use Pydantic's `BaseSettings` to load configuration from environment variables and `.env` files. This approach keeps secrets out of source code and makes it easy to switch between environments. Install the settings package with `pip install pydantic-settings`.

### Environment variables

Create a settings module that loads configuration from environment variables, allowing you to manage secrets and deployment-specific values outside your code:

```python
# config.py
from pydantic_settings import BaseSettings, SettingsConfigDict

class Settings(BaseSettings):
    database_server: str = "<server>.database.windows.net"
    database_name: str = "<database>"
    pool_size: int = 10

    model_config = SettingsConfigDict(env_file=".env")

settings = Settings()

def get_connection_string() -> str:
    return (
        f"Server={settings.database_server};"
        f"Database={settings.database_name};"
        "Authentication=ActiveDirectoryDefault;"
        "Encrypt=yes"
    )
```

Then update `database.py` to import `get_connection_string` from `config` instead of defining its own copy. By removing the duplicated function, you ensure the app reads connection settings from a single source.

```python
# database.py
from config import get_connection_string
```

## Related content

- [Manage connections with mssql-python](connection-management.md)
- [Connection pooling with mssql-python](connection-pooling.md)
- [Error handling and SQLSTATE codes for mssql-python](error-handling.md)
- [Microsoft Entra authentication with mssql-python](entra-authentication.md)
