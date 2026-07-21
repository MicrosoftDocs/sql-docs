---
title: Use mssql-python with Flask
description: Learn how to build web applications with Flask and mssql-python for Microsoft SQL and Azure SQL database access.
author: dlevy-msft-sql
ms.author: dlevy
ms.date: 07/17/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---

# Use mssql-python with Flask

Flask is a lightweight Python web framework that gives you full control over application structure. Combined with mssql-python, you can build web applications and REST APIs backed by Microsoft SQL and Azure SQL Database with minimal overhead.

## Prerequisites

- Python 3.10 or later.
- The `mssql-python` and `flask` packages. Install both with `pip install flask mssql-python`.
- [!INCLUDE [prereq-linux-macos](includes/prereq-linux-macos.md)]

[!INCLUDE [prereq-create-sql-database](includes/prereq-create-sql-database.md)]

The examples in this article use the **AdventureWorksLT** sample database, specifically the `SalesLT.Product` table. If you don't have AdventureWorksLT installed, see [AdventureWorks sample databases](/sql/samples/adventureworks-install-configure).

## Project setup

### Install dependencies

Install the required packages with pip:

```bash
pip install flask mssql-python
```

### Project structure

Organize your project with separate modules for configuration, connection management, routes, and tests:

```text
my_app/
├── app.py            # Flask app and routes
├── config.py         # database settings
├── database.py       # connection lifecycle
├── test_app.py       # pytest tests
└── blueprints/       # optional: routes grouped into modules
    ├── __init__.py
    └── products.py
```

## Database connection management

Flask doesn't include a built-in database layer, so you manage connections directly. The pattern in this section stores one connection per request on Flask's `g` object and closes it automatically when the request ends.

### Create config.py

Centralize database settings in a configuration class. Environment variables let you override defaults without changing code.

```python
# config.py
import os

class Config:
    """Application configuration."""
    DATABASE_SERVER = os.getenv("DB_SERVER", "<server>.database.windows.net")
    DATABASE_NAME = os.getenv("DB_NAME", "<database>")
    POOL_SIZE = int(os.getenv("DB_POOL_SIZE", "10"))
```

### Create database.py

The `database.py` module manages the connection lifecycle. Flask's `g` object is a per-request namespace, so storing the connection there ensures each request gets its own connection that's cleaned up when the request finishes.

The `get_connection_string()` function builds the connection string from the app config. The `get_db()` function creates a connection on first call and reuses it for the rest of the request. The `close_db()` function runs automatically at the end of each request, rolling back the transaction if an exception occurred and committing otherwise. The `init_app()` function registers this teardown behavior with the Flask app.

```python
# database.py
import mssql_python
from flask import g, current_app

def get_connection_string() -> str:
    """Build connection string from Flask app config."""
    cfg = current_app.config
    return (
        f"Server={cfg['DATABASE_SERVER']};"
        f"Database={cfg['DATABASE_NAME']};"
        "Authentication=ActiveDirectoryDefault;"
        "Encrypt=yes"
    )

def get_db():
    """Get a database cursor for the current request.

    The connection is stored on Flask's g object so it persists
    for the duration of the request and is reused across calls.
    """
    if "db_conn" not in g:
        g.db_conn = mssql_python.connect(get_connection_string())
        g.db_cursor = g.db_conn.cursor()
    return g.db_cursor

def close_db(exception=None):
    """Close the database connection at the end of the request."""
    cursor = g.pop("db_cursor", None)
    conn = g.pop("db_conn", None)

    if cursor is not None:
        cursor.close()
    if conn is not None:
        if exception:
            conn.rollback()
        else:
            conn.commit()
        conn.close()

def init_app(app):
    """Register database teardown with the Flask app."""
    app.teardown_appcontext(close_db)
```

> [!NOTE]
> `ActiveDirectoryDefault` uses `DefaultAzureCredential`, which tries multiple credential providers in sequence. The first connection can be slow because the SDK walks the chain until it finds a working provider. In production, if you know which credential type your environment uses, specify it directly (for example, `ActiveDirectoryMSI` for managed identity) to avoid the chain walk. For more information, see [Microsoft Entra authentication](entra-authentication.md).

## Flask application

The following example shows a complete Flask application with routes for listing, retrieving, creating, updating, and deleting products.

### Create app.py

The application module creates the Flask app, loads configuration, and registers the database teardown. Each route function calls `get_db()` to get a cursor, executes queries with [parameterized SQL](parameterized-queries.md) (using `%(name)s` placeholders and a dictionary of values), and returns JSON responses.

```python
# app.py
from flask import Flask, jsonify, request, abort
from config import Config
from database import init_app, get_db

app = Flask(__name__)
app.config.from_object(Config)
init_app(app)

@app.route("/")
def index():
    return jsonify({"message": "Product API", "docs": "/products"})

@app.route("/products")
def list_products():
    """List products with pagination."""
    page = request.args.get("page", 1, type=int)
    page_size = request.args.get("page_size", 10, type=int)
    skip = (page - 1) * page_size

    cursor = get_db()

    cursor.execute("SELECT COUNT(*) FROM SalesLT.Product")
    total = cursor.fetchval()

    cursor.execute("""
        SELECT ProductID, Name, ProductNumber, ListPrice, Color, ProductCategoryID
        FROM SalesLT.Product
        ORDER BY ProductID
        OFFSET %(skip)s ROWS
        FETCH NEXT %(limit)s ROWS ONLY
    """, {"skip": skip, "limit": page_size})

    items = [{
        "id": row.ProductID,
        "name": row.Name,
        "product_number": row.ProductNumber,
        "price": float(row.ListPrice),
        "color": row.Color,
        "category_id": row.ProductCategoryID
    } for row in cursor.fetchall()]

    return jsonify({
        "items": items,
        "total": total,
        "page": page,
        "page_size": page_size,
        "pages": (total + page_size - 1) // page_size
    })

@app.route("/products/<int:product_id>")
def get_product(product_id):
    """Get a single product by ID."""
    cursor = get_db()
    cursor.execute("""
        SELECT ProductID, Name, ProductNumber, ListPrice, Color, ProductCategoryID
        FROM SalesLT.Product
        WHERE ProductID = %(id)s
    """, {"id": product_id})

    row = cursor.fetchone()
    if not row:
        abort(404)

    return jsonify({
        "id": row.ProductID,
        "name": row.Name,
        "product_number": row.ProductNumber,
        "price": float(row.ListPrice),
        "color": row.Color,
        "category_id": row.ProductCategoryID
    })

@app.route("/products", methods=["POST"])
def create_product():
    """Create a new product."""
    data = request.get_json()
    if not data:
        abort(400)

    cursor = get_db()

    # OUTPUT INSERTED returns the new row's columns in the same statement,
    # so you don't need a separate SELECT to get the generated ID and defaults.
    # ProductNumber is required and unique. StandardCost and SellStartDate are
    # also NOT NULL in SalesLT.Product, so supply values for them.
    cursor.execute("""
        INSERT INTO SalesLT.Product
            (Name, ProductNumber, ListPrice, Color, Size, ProductCategoryID, StandardCost, SellStartDate)
        OUTPUT INSERTED.ProductID, INSERTED.Name, INSERTED.ProductNumber, INSERTED.ListPrice,
               INSERTED.Color, INSERTED.ProductCategoryID
        VALUES (%(name)s, %(product_number)s, %(price)s, %(color)s, %(size)s, %(category_id)s, 0, GETDATE())
    """, {
        "name": data["name"],
        "product_number": data["product_number"],
        "price": data["price"],
        "color": data.get("color"),
        "size": data.get("size"),
        "category_id": data["category_id"]
    })

    row = cursor.fetchone()
    return jsonify({
        "id": row.ProductID,
        "name": row.Name,
        "product_number": row.ProductNumber,
        "price": float(row.ListPrice),
        "color": row.Color,
        "category_id": row.ProductCategoryID
    }), 201

@app.route("/products/<int:product_id>", methods=["PUT"])
def update_product(product_id):
    """Update an existing product."""
    data = request.get_json()
    if not data:
        abort(400)

    cursor = get_db()

    updates = []
    params = {"id": product_id}

    for field in ("name", "product_number", "price", "color", "category_id"):
        if field in data:
            col = {"name": "Name", "product_number": "ProductNumber",
                   "price": "ListPrice", "color": "Color",
                   "category_id": "ProductCategoryID"}[field]
            updates.append(f"{col} = %({field})s")
            params[field] = data[field]

    if not updates:
        abort(400)

    cursor.execute(f"""
        UPDATE SalesLT.Product SET {', '.join(updates)}
        OUTPUT INSERTED.ProductID, INSERTED.Name, INSERTED.ProductNumber, INSERTED.ListPrice,
               INSERTED.Color, INSERTED.ProductCategoryID
        WHERE ProductID = %(id)s
    """, params)

    row = cursor.fetchone()
    if not row:
        abort(404)

    return jsonify({
        "id": row.ProductID,
        "name": row.Name,
        "product_number": row.ProductNumber,
        "price": float(row.ListPrice),
        "color": row.Color,
        "category_id": row.ProductCategoryID
    })

@app.route("/products/<int:product_id>", methods=["DELETE"])
def delete_product(product_id):
    """Delete a product."""
    cursor = get_db()
    cursor.execute("DELETE FROM SalesLT.Product WHERE ProductID = %(id)s", {"id": product_id})
    if cursor.rowcount == 0:
        abort(404)
    return "", 204

@app.route("/health")
def health_check():
    """Check database connectivity."""
    try:
        cursor = get_db()
        cursor.execute("SELECT 1")
        return jsonify({"status": "healthy", "database": "connected"})
    except Exception as e:
        return jsonify({"status": "unhealthy", "error": str(e)}), 503
```

### Run the application

Start the development server:

```bash
flask --app app run --debug --port 5000
```

The server listens on `http://localhost:5000`. Open a second terminal and call the endpoints by using `curl` to confirm the app is talking to your database:

```bash
# Check database connectivity
curl http://localhost:5000/health

# List the first page of products
curl "http://localhost:5000/products?page_size=5"

# Get a single product by ID
curl http://localhost:5000/products/680
```

> [!NOTE]
> In PowerShell, `curl` is an alias for `Invoke-WebRequest`. The simple GET commands here run fine, but the response comes back as an object rather than printed JSON. Commands that use `curl` flags like `-X`, `-H`, or `-d` (such as the `POST` example later) don't work as written. On Windows, use `curl.exe` to run the commands exactly as shown, or use PowerShell's `Invoke-RestMethod` (for example, `Invoke-RestMethod http://localhost:5000/health`), which also parses the JSON response for you.

Each endpoint returns JSON. You can also open `http://localhost:5000/products` in a browser to view the paginated list.

## Connection pooling

Without connection pooling, each request opens and closes a TCP connection to Microsoft SQL, which adds latency. Connection pooling keeps a set of idle connections ready for reuse. To enable connection pooling call `mssql_python.pooling()` once at module level. With pooling enabled, `conn.close()` in the `close_db` teardown returns the connection to the pool instead of closing it.

### Enable connection pooling

Enable pooling by calling `mssql_python.pooling()` at module level before any connections are opened:

```python
# database.py with connection pooling
import mssql_python
from flask import g, current_app

# Configure pool at module level
mssql_python.pooling(max_size=20, idle_timeout=300)

def get_db():
    """Get a database cursor with connection pooling."""
    if "db_conn" not in g:
        g.db_conn = mssql_python.connect(get_connection_string())
        g.db_cursor = g.db_conn.cursor()
    return g.db_cursor
```

## Error handling

Flask lets you register handlers for specific exception types. Catching `mssql_python.DatabaseError` and `mssql_python.IntegrityError` lets you return structured JSON error responses instead of default HTML error pages.

### Register error handlers

Add these handlers to the existing `app.py`, after the `app = Flask(__name__)` line. Because the handlers reference the `app` object, they must come after the app is created. `app.py` needs `import mssql_python` at the top. The handlers return structured JSON responses instead of default HTML error pages:

```python
# app.py
import mssql_python

@app.errorhandler(mssql_python.DatabaseError)
def handle_database_error(error):
    """Handle database errors."""
    return jsonify({"error": "Database error occurred"}), 500

@app.errorhandler(mssql_python.IntegrityError)
def handle_integrity_error(error):
    """Handle integrity constraint violations."""
    error_msg = str(error)
    if "UNIQUE" in error_msg:
        return jsonify({"error": "Resource already exists"}), 409
    if "FOREIGN KEY" in error_msg:
        return jsonify({"error": "Referenced resource not found"}), 400
    return jsonify({"error": "Data integrity error"}), 400

@app.errorhandler(404)
def not_found(error):
    return jsonify({"error": "Resource not found"}), 404

@app.errorhandler(400)
def bad_request(error):
    return jsonify({"error": "Bad request"}), 400
```

## Blueprints

As your application grows, putting all routes in a single file becomes hard to maintain. Flask Blueprints let you group related routes into separate modules that are registered with the app.

### Organize routes with blueprints

Create a blueprint module for product routes that imports `get_db` and defines endpoints under a shared URL prefix:

```python
# blueprints/products.py
from flask import Blueprint, jsonify, request, abort
from database import get_db

products_bp = Blueprint("products", __name__, url_prefix="/api/products")

@products_bp.route("/")
def list_products():
    """List all products."""
    cursor = get_db()
    cursor.execute("""
        SELECT ProductID, Name, ListPrice, Color, ProductCategoryID
        FROM SalesLT.Product ORDER BY ProductID
    """)
    return jsonify([{
        "id": row.ProductID,
        "name": row.Name,
        "price": float(row.ListPrice),
        "color": row.Color,
        "category_id": row.ProductCategoryID
    } for row in cursor.fetchall()])

@products_bp.route("/<int:product_id>")
def get_product(product_id):
    """Get a product by ID."""
    cursor = get_db()
    cursor.execute(
        "SELECT ProductID, Name, ListPrice, Color FROM SalesLT.Product WHERE ProductID = %(id)s",
        {"id": product_id}
    )
    row = cursor.fetchone()
    if not row:
        abort(404)
    return jsonify({"id": row.ProductID, "name": row.Name, "price": float(row.ListPrice), "color": row.Color})
```

### Register the blueprint

Save the blueprint as `blueprints/products.py`, and add an empty `blueprints/__init__.py` file so Python treats the folder as a package. Then, in `app.py`, import the blueprint with your other imports and register it after the `app = Flask(__name__)` line:

```python
# app.py
from blueprints.products import products_bp

app.register_blueprint(products_bp)
```

Because the blueprint sets `url_prefix="/api/products"`, its routes are served under that prefix. For example, the list route is available at `http://localhost:5000/api/products/`, separate from the `/products` routes defined directly in `app.py`.

## Testing

Flask provides a test client that sends requests to your application without starting a real HTTP server. Use `pytest` fixtures to create the client and reuse it across tests.

### Test setup with pytest

Create a pytest fixture that provides a test client and write tests to verify route behavior:

```python
# test_app.py
import uuid

import pytest
from app import app

@pytest.fixture
def client():
    app.config["TESTING"] = True
    with app.test_client() as client:
        yield client

def test_health_check(client):
    response = client.get("/health")
    assert response.status_code == 200
    data = response.get_json()
    assert data["status"] == "healthy"

def test_list_products(client):
    response = client.get("/products")
    assert response.status_code == 200
    data = response.get_json()
    assert "items" in data
    assert "total" in data

def test_create_product(client):
    suffix = uuid.uuid4().hex[:8]
    name = f"Test Product {suffix}"
    response = client.post("/products", json={
        "name": name,
        "product_number": f"TEST-{suffix}",
        "price": 19.99,
        "category_id": 18
    })
    assert response.status_code == 201
    data = response.get_json()
    assert data["name"] == name

def test_get_product_not_found(client):
    response = client.get("/products/99999")
    assert response.status_code == 404
```

These tests run against your live database rather than mocks, so `test_create_product` inserts a real row into `SalesLT.Product`. In AdventureWorksLT, both `Name` and `ProductNumber` have unique constraints, so the test generates a unique value for each on every run. If you hardcode those values instead, the test fails with a conflict on the second run unless you delete the row first.

### Run the tests

Save the tests as `test_app.py` in your project folder. With your virtual environment activated, install `pytest` and run it from that folder. Installing and running `pytest` inside the same virtual environment as `flask` and `mssql-python` ensures the tests import the packages your app uses. `pytest` automatically discovers `test_app.py` and reports the results:

```bash
pip install pytest
pytest
```


`pytest` discovers `test_app.py` automatically and reports the results:

```output
==================== test session starts ====================
collected 4 items

test_app.py ....                                       [100%]

===================== 4 passed in 3.21s =====================
```

## Related content

- [Connection management](connection-management.md)
- [Connection pooling](connection-pooling.md)
- [Error handling](error-handling.md)
- [FastAPI integration](fastapi-integration.md)
- [Microsoft Entra authentication](entra-authentication.md)
