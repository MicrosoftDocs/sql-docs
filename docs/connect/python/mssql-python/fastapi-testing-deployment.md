---
title: Test and Deploy FastAPI Applications with mssql-python
description: Learn how to configure, secure, test, and deploy FastAPI applications that use mssql-python with Microsoft SQL.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, randolphwest
ms.date: 09/17/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---

# Test and deploy FastAPI applications with mssql-python

After you build a FastAPI application with mssql-python, configure it for deployment, connection reuse, error handling, authentication, and automated testing.

## Prerequisites

- Complete [Use mssql-python with FastAPI](fastapi-integration.md), or have an equivalent FastAPI application that uses the AdventureWorksLT sample database. The authentication dependency in this article queries `SalesLT.Customer`.
- Install the production and test dependencies:

  ```bash
  pip install pydantic-settings pyjwt pytest httpx
  ```

## Configure deployment settings

Use Pydantic Settings to load deployment-specific values from environment variables. This approach keeps secrets out of source code and gives each environment its own database, pool, and authentication configuration.

Create `config.py`:

```python
from pydantic_settings import BaseSettings


class Settings(BaseSettings):
    database_server: str
    database_name: str
    pool_size: int = 20
    pool_idle_timeout: int = 300
    jwt_secret: str


settings = Settings()


def get_connection_string() -> str:
    return (
        f"Server={settings.database_server};"
        f"Database={settings.database_name};"
        "Authentication=ActiveDirectoryDefault;"
        "Encrypt=yes"
    )
```

Set `DATABASE_SERVER`, `DATABASE_NAME`, and `JWT_SECRET` in the deployment environment. Pydantic Settings reads the uppercase environment variable names automatically.

> [!NOTE]
> `ActiveDirectoryDefault` tries multiple credential providers in sequence. In production, specify the authentication mode for the deployed identity, such as `ActiveDirectoryMSI` for managed identity, to avoid walking the credential chain. For available modes, see [Microsoft Entra authentication with mssql-python](entra-authentication.md).

## Configure connection pooling

mssql-python enables connection pooling by default. Configure the pool once, before the application creates its first connection. Size the pool for the application's expected concurrent database work and the database service tier.

Update `database.py` to use the deployment settings:

```python
from collections.abc import Generator

import mssql_python

from config import get_connection_string, settings


mssql_python.pooling(
    max_size=settings.pool_size,
    idle_timeout=settings.pool_idle_timeout,
)


def get_db_dependency() -> Generator:
    with mssql_python.connect(get_connection_string()) as conn:
        with conn.cursor() as cursor:
            yield cursor
```

The connection context manager commits after successful request processing, rolls back when request processing raises an exception, and closes the connection. Closing the connection returns it to the pool. For pool keys, sizing, identity isolation, and exhaustion guidance, see [Connection pooling with mssql-python](connection-pooling.md).

## Handle database errors

Register exception handlers so database failures return consistent responses without exposing connection details, queries, or server error text.

Add the handlers after `app = FastAPI(...)` in `main.py`:

```python
import mssql_python
from fastapi import Request
from fastapi.responses import JSONResponse


@app.exception_handler(mssql_python.IntegrityError)
async def integrity_exception_handler(
    request: Request,
    exc: mssql_python.IntegrityError,
):
    return JSONResponse(
        status_code=409,
        content={
            "detail": "The request conflicts with existing data.",
            "type": "integrity_error",
        },
    )


@app.exception_handler(mssql_python.DatabaseError)
async def database_exception_handler(
    request: Request,
    exc: mssql_python.DatabaseError,
):
    return JSONResponse(
        status_code=500,
        content={
            "detail": "A database operation failed.",
            "type": "database_error",
        },
    )
```

Log the exception through your application's protected telemetry pipeline before returning the response. For the exception hierarchy and SQLSTATE handling, see [Error handling and SQLSTATE codes for mssql-python](error-handling.md).

## Add authentication dependencies

Chain FastAPI dependencies to validate a JSON Web Token (JWT), load the matching AdventureWorksLT customer, and make that customer available to protected routes. Validate the token before acquiring a database connection so an invalid token doesn't use a pooled connection.

Create `auth.py`:

```python
import jwt
from fastapi import Depends, HTTPException
from fastapi.security import HTTPAuthorizationCredentials, HTTPBearer

from config import settings
from database import get_db_dependency


security = HTTPBearer()


def get_customer_id(
    credentials: HTTPAuthorizationCredentials = Depends(security),
) -> int:
    try:
        payload = jwt.decode(
            credentials.credentials,
            settings.jwt_secret,
            algorithms=["HS256"],
        )
        customer_id = int(payload["sub"])
    except (KeyError, TypeError, ValueError):
        raise HTTPException(status_code=401, detail="Invalid token subject")
    except jwt.ExpiredSignatureError:
        raise HTTPException(status_code=401, detail="Token expired")
    except jwt.InvalidTokenError:
        raise HTTPException(status_code=401, detail="Invalid token")

    return customer_id


def get_current_customer(
    customer_id: int = Depends(get_customer_id),
    cursor = Depends(get_db_dependency),
):
    cursor.execute(
        """
        SELECT CustomerID, FirstName, LastName
        FROM SalesLT.Customer
        WHERE CustomerID = %(id)s
        """,
        {"id": customer_id},
    )
    customer = cursor.fetchone()
    if customer is None:
        raise HTTPException(status_code=401, detail="Customer not found")

    return {
        "id": customer.CustomerID,
        "first_name": customer.FirstName,
        "last_name": customer.LastName,
    }
```

Import the dependency and add a protected route to `main.py`:

```python
from auth import get_current_customer


@app.get("/me")
def get_me(current_customer: dict = Depends(get_current_customer)):
    return current_customer
```

Use an identity provider to issue and rotate signing keys. For HS256, set `JWT_SECRET` to at least 32 random bytes. Don't store a production signing secret in the repository or in an image.

## Test the application

FastAPI's `TestClient` sends requests to the application without starting an HTTP server. The following integration tests use the configured database.

Create `test_api.py`:

```python
import uuid

from fastapi.testclient import TestClient

from main import app


client = TestClient(app)


def test_list_products():
    response = client.get("/products")
    assert response.status_code == 200
    data = response.json()
    assert "items" in data
    assert "total" in data


def test_create_product():
    suffix = uuid.uuid4().hex[:8]
    response = client.post(
        "/products",
        json={
            "name": f"Test Product {suffix}",
            "product_number": f"TEST-{suffix}",
            "price": 19.99,
            "color": "Red",
            "size": "M",
            "category_id": 1,
        },
    )
    assert response.status_code == 201
    data = response.json()
    assert data["product_number"] == f"TEST-{suffix}"
    assert data["price"] == 19.99


def test_get_product_not_found():
    response = client.get("/products/99999")
    assert response.status_code == 404


def test_health_check():
    response = client.get("/health")
    assert response.status_code == 200
    assert response.json()["status"] == "healthy"
```

Run the tests from the project root:

```bash
pytest
```

These tests use the configured database, and `test_create_product` inserts a row into `SalesLT.Product`. Use a dedicated test database and reset its data between test runs.

## Deployment checklist

- Set `DATABASE_SERVER`, `DATABASE_NAME`, and `JWT_SECRET` through the deployment platform's secret and configuration stores.
- Use a dedicated Microsoft Entra identity with the minimum required database permissions.
- Set pool size below the database's connection limit and leave capacity for administrative access and other workloads.
- Run database integration tests against an isolated test database.
- Configure protected telemetry for database exceptions, request latency, and pool exhaustion.
- Run Uvicorn without `--reload` in deployed environments.

## Related content

- [Use mssql-python with FastAPI](fastapi-integration.md)
- [Connection pooling with mssql-python](connection-pooling.md)
- [Error handling and SQLSTATE codes for mssql-python](error-handling.md)
- [Microsoft Entra authentication with mssql-python](entra-authentication.md)
- [Security best practices for mssql-python applications](security-best-practices.md)
