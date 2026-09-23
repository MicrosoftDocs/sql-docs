---
title: Use mssql-python with FastAPI
description: Learn how to build REST APIs with FastAPI and mssql-python for Microsoft SQL and Azure SQL database access.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, randolphwest
ms.date: 09/18/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---

# Use mssql-python with FastAPI

FastAPI is a modern Python web framework for building APIs. Combined with mssql-python, you can build high-performance REST APIs backed by Microsoft SQL and Azure SQL Database.

## Prerequisites

- Python 3.10 or later.
- [!INCLUDE [prereq-linux-macos](includes/prereq-linux-macos.md)]

[!INCLUDE [prereq-create-sql-database](../../../includes/paragraph-content/prereq-create-sql-database.md)]

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
pip install fastapi uvicorn mssql-python pydantic
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
└── routers/
    └── products.py
```

## Database connection management

FastAPI uses dependency injection to provide resources like database connections to route handlers. The pattern in this section opens a connection, yields a cursor, and uses the mssql-python connection context manager to commit on success, roll back on an exception, and close the connection.

### Create database.py

The `get_connection_string()` function builds the ODBC connection string from configuration values. FastAPI's `Depends()` calls `get_db_dependency()` once per request and manages its lifecycle.

```python
# database.py
import mssql_python
from collections.abc import Generator

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
def get_db_dependency() -> Generator:
    """FastAPI dependency for database cursor."""
    with mssql_python.connect(get_connection_string()) as conn:
        with conn.cursor() as cursor:
            yield cursor
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
    except Exception:
        raise HTTPException(status_code=503, detail="Database unavailable")
```

### Run the application

```bash
uvicorn main:app --reload --host 0.0.0.0 --port 8000
```

## Test and deploy the application

Use the companion article to finish the application:

### Error handling

The companion article covers database exception handling.

#### Global exception handler

See [Handle database errors](fastapi-testing-deployment.md#handle-database-errors).

### Connection pooling

The companion article covers connection pool configuration.

#### Enhanced database module

See [Configure connection pooling](fastapi-testing-deployment.md#configure-connection-pooling).

### Authentication middleware

See [Add authentication dependencies](fastapi-testing-deployment.md#add-authentication-dependencies).

### Testing

The companion article covers integration testing.

#### Test setup

See [Test the application](fastapi-testing-deployment.md#test-the-application).

### Deployment configuration

The companion article covers deployment configuration and operations.

#### Environment variables

See [Configure deployment settings](fastapi-testing-deployment.md#configure-deployment-settings) and the [deployment checklist](fastapi-testing-deployment.md#deployment-checklist).

## Related content

- [Test and deploy FastAPI applications with mssql-python](fastapi-testing-deployment.md)
- [Manage connections with mssql-python](connection-management.md)
- [Connection pooling with mssql-python](connection-pooling.md)
- [Error handling and SQLSTATE codes for mssql-python](error-handling.md)
- [Microsoft Entra authentication with mssql-python](entra-authentication.md)
