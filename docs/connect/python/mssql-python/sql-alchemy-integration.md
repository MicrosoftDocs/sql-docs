---
title: Use mssql-python with SQLAlchemy
description: Learn how to use the mssql-python driver with SQLAlchemy ORM and Core for Microsoft SQL and Azure SQL database access.
author: dlevy-msft-sql
ms.author: dlevy
ms.date: 07/17/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---

# Use mssql-python with SQLAlchemy

**SQLAlchemy** is the most widely used Python ORM and database toolkit. Starting with **SQLAlchemy** 2.1.0b2, a built-in dialect for the mssql-python driver lets you use **SQLAlchemy** ORM and Core with Microsoft SQL and Azure SQL Database.

> [!IMPORTANT]
> The mssql-python dialect was added in **SQLAlchemy** 2.1.0b2 (released April 16, 2026). **SQLAlchemy** 2.1 is currently a **pre-release** series and is **not recommended for production use**. Before upgrading from **SQLAlchemy** 2.0, understand:
>
> - **APIs might change** before final stable release (2.1 GA)
> - **Test thoroughly** on your workload before deployment
> - **Use stable SQLAlchemy 2.0.x** for production systems until 2.1 reaches GA
> - **Pin your dependency** to a specific version (for example, `sqlalchemy==2.1.0b2`) rather than using version ranges
>
> See the [Known Limitations](#known-limitations) section for details on when to use pre-release versions.

## Prerequisites

- Python 3.10 or later. **SQLAlchemy** 2.1 dropped support for Python 3.9 and earlier.
- The `mssql-python` and `sqlalchemy` packages (2.1.0b2 or later).

The examples in this article use the **AdventureWorksLT** sample database. If you don't have AdventureWorksLT installed, see [AdventureWorks sample databases](/sql/samples/adventureworks-install-configure).

## Install the pre-release

Because **SQLAlchemy** 2.1 is in beta, `pip install sqlalchemy` installs the latest stable 2.0.x release by default. Install the pre-release explicitly:

```bash
pip install mssql-python "sqlalchemy>=2.1.0b2"
```

Verify the installed version:

```python
import sqlalchemy
print(sqlalchemy.__version__)  # Should show 2.1.0b2 or later
```

## Connection URLs

The mssql-python dialect uses `mssql+mssqlpython` as the URL scheme. The general format is:

```text
mssql+mssqlpython://<username>:<password>@<host>:<port>/<database>
```

### SQL authentication

For SQL authentication, include the username and password in the connection URL:

```python
from sqlalchemy import create_engine

# Replace <password> with your actual password. Avoid using the sa account in production.
engine = create_engine(
    "mssql+mssqlpython://dbuser:<password>@localhost:1433/<database>"
)
```

### Microsoft Entra authentication

For Microsoft Entra authentication, use an empty username and the `authentication` query parameter:

```python
from sqlalchemy import create_engine

engine = create_engine(
    "mssql+mssqlpython://@<server>.database.windows.net/<database>"
    "?authentication=ActiveDirectoryDefault&encrypt=yes"
)
```

> [!NOTE]
> `ActiveDirectoryDefault` uses `DefaultAzureCredential`, which tries multiple credential providers in sequence. The first connection can be slow because the SDK walks the chain until it finds a working provider. In production, if you know which credential type your environment uses, specify it directly (for example, `ActiveDirectoryMSI` for managed identity) to avoid the chain walk. For more information, see [Microsoft Entra authentication](entra-authentication.md).

### Build URLs programmatically

Use `sqlalchemy.engine.URL.create` to avoid manual URL encoding:

```python
from sqlalchemy.engine import URL

url = URL.create(
    "mssql+mssqlpython",
    username="dbuser",
    password="<password>",
    host="localhost",
    port=1433,
    database="<database>",
)
engine = create_engine(url)
```

## Define ORM models

Use **SQLAlchemy**'s declarative mapping to define models that map to Microsoft SQL tables.

```python
from datetime import datetime
from decimal import Decimal

from sqlalchemy import Identity, String, Numeric, Integer, DateTime, func
from sqlalchemy.orm import DeclarativeBase, Mapped, mapped_column


class Base(DeclarativeBase):
    pass


class Product(Base):
    __tablename__ = "Product"
    __table_args__ = {"schema": "SalesLT"}

    product_id: Mapped[int] = mapped_column(
        "ProductID", Integer, Identity(), primary_key=True
    )
    name: Mapped[str] = mapped_column("Name", String(50))
    product_number: Mapped[str] = mapped_column("ProductNumber", String(25))
    color: Mapped[str | None] = mapped_column("Color", String(15))
    list_price: Mapped[Decimal] = mapped_column("ListPrice", Numeric(19, 4))
    standard_cost: Mapped[Decimal] = mapped_column("StandardCost", Numeric(19, 4))
    size: Mapped[str | None] = mapped_column("Size", String(5))
    product_category_id: Mapped[int | None] = mapped_column("ProductCategoryID", Integer)
    sell_start_date: Mapped[datetime] = mapped_column("SellStartDate", DateTime)
    modified_date: Mapped[datetime] = mapped_column(
        "ModifiedDate", DateTime, server_default=func.getdate()
    )
```

> [!TIP]
> Microsoft SQL uses `IDENTITY` for auto-incrementing columns. **SQLAlchemy** maps this automatically for integer primary key columns. The explicit `Identity()` shown above is optional unless you need to control the start and increment values.

## CRUD operations

The following examples show how to insert, query, update, and delete rows by using the ORM session. Each example reuses `new_id`, the `ProductID` returned when you insert a row. To run all four operations together, see the [complete example](#complete-example).

### Create a session

Create a session to execute operations within a transaction:

```python
from sqlalchemy.orm import Session

with Session(engine) as session:
    # Use session for queries and modifications
    pass
```

For applications that create many sessions, use `sessionmaker`:

```python
from sqlalchemy.orm import sessionmaker

SessionLocal = sessionmaker(bind=engine)
```

### Insert rows

Add a new product, commit the session, and capture the generated `ProductID` for the following examples:

```python
from datetime import datetime

with Session(engine) as session:
    product = Product(
        name="Classic Road Bike",
        product_number="BK-C001",
        color="Red",
        list_price=Decimal("1299.99"),
        standard_cost=Decimal("749.99"),
        sell_start_date=datetime(2026, 1, 1),
        product_category_id=6,
    )
    session.add(product)
    session.commit()

    new_id = product.product_id
    print(f"Inserted ProductID: {new_id}")
```

> [!NOTE]
> In `SalesLT.Product`, both `Name` and `ProductNumber` have unique constraints. If you run this insert more than once, change these values or delete the earlier row first. The [complete example](#complete-example) deletes the row it creates, so it can run repeatedly.

### Query rows

Retrieve a single row by primary key, or use `select()` for filtered queries:

```python
from sqlalchemy import select

with Session(engine) as session:
    # Single row by primary key (new_id is from the insert example)
    product = session.get(Product, new_id)
    if product:
        print(f"{product.name}: ${product.list_price}")

    # Filtered query
    stmt = select(Product).where(Product.list_price < 500).order_by(Product.name)
    products = session.scalars(stmt).all()
    for p in products:
        print(f"{p.name}: ${p.list_price}")
```

### Update rows

Modify a field on an existing row and commit:

```python
with Session(engine) as session:
    product = session.get(Product, new_id)
    if product:
        product.list_price = Decimal("1349.99")
        session.commit()
```

### Delete rows

Remove a row and commit:

```python
with Session(engine) as session:
    product = session.get(Product, new_id)
    if product:
        session.delete(product)
        session.commit()
```

### Complete example

The previous sections showed each piece separately. This section combines them into one self-contained script that you can copy, run, and run again.

Create a file named `crud.py` and add the following code. Replace the connection details in `create_engine` with your own (see [Connection URLs](#connection-urls)):

```python
from datetime import datetime
from decimal import Decimal

from sqlalchemy import create_engine, Identity, String, Numeric, Integer, DateTime, func
from sqlalchemy.orm import DeclarativeBase, Mapped, mapped_column, Session

# Replace <password> and <database> with your connection details.
engine = create_engine(
    "mssql+mssqlpython://dbuser:<password>@localhost:1433/<database>"
)


class Base(DeclarativeBase):
    pass


class Product(Base):
    __tablename__ = "Product"
    __table_args__ = {"schema": "SalesLT"}

    product_id: Mapped[int] = mapped_column(
        "ProductID", Integer, Identity(), primary_key=True
    )
    name: Mapped[str] = mapped_column("Name", String(50))
    product_number: Mapped[str] = mapped_column("ProductNumber", String(25))
    color: Mapped[str | None] = mapped_column("Color", String(15))
    list_price: Mapped[Decimal] = mapped_column("ListPrice", Numeric(19, 4))
    standard_cost: Mapped[Decimal] = mapped_column("StandardCost", Numeric(19, 4))
    size: Mapped[str | None] = mapped_column("Size", String(5))
    product_category_id: Mapped[int | None] = mapped_column("ProductCategoryID", Integer)
    sell_start_date: Mapped[datetime] = mapped_column("SellStartDate", DateTime)
    modified_date: Mapped[datetime] = mapped_column(
        "ModifiedDate", DateTime, server_default=func.getdate()
    )


with Session(engine) as session:
    # Create
    product = Product(
        name="Classic Road Bike",
        product_number="BK-C001",
        color="Red",
        list_price=Decimal("1299.99"),
        standard_cost=Decimal("749.99"),
        sell_start_date=datetime(2026, 1, 1),
        product_category_id=6,
    )
    session.add(product)
    session.commit()
    new_id = product.product_id
    print(f"Inserted ProductID: {new_id}")

    # Read
    product = session.get(Product, new_id)
    print(f"Read: {product.name} costs ${product.list_price}")

    # Update
    product.list_price = Decimal("1349.99")
    session.commit()
    print(f"Updated price to ${product.list_price}")

    # Delete
    session.delete(product)
    session.commit()
    print(f"Deleted ProductID: {new_id}")
```

Run the script:

```bash
python crud.py
```

You see output similar to the following:

```output
Inserted ProductID: 1019
Read: Classic Road Bike costs $1299.9900
Updated price to $1349.9900
Deleted ProductID: 1019
```

The script deletes the row it creates, so it doesn't hit the unique constraints on `Name` and `ProductNumber` when you run it again. Each run inserts a new row, so the `ProductID` increases each time.

## Core queries

**SQLAlchemy** Core provides a lower-level SQL expression API. You can use Core with the same engine and table definitions, including ORM-mapped classes.

```python
from sqlalchemy import text

with engine.connect() as conn:
    result = conn.execute(text("SELECT @@VERSION"))
    print(result.scalar())
```

Use table-level constructs for type-safe SQL generation:

```python
from sqlalchemy import insert, select, update, delete

with engine.connect() as conn:
    # Insert
    conn.execute(
        insert(Product).values(
            name="Touring Bike",
            product_number="BK-T002",
            list_price=Decimal("999.99"),
            standard_cost=Decimal("575.00"),
            sell_start_date=datetime(2026, 1, 1)
        )
    )
    conn.commit()

    # Select
    stmt = select(
        Product.name.label("name"),
        Product.list_price.label("list_price"),
    ).where(Product.list_price > 100)
    for row in conn.execute(stmt):
        print(row.name, row.list_price)

    # Delete the inserted row so this example can run again
    conn.execute(delete(Product).where(Product.product_number == "BK-T002"))
    conn.commit()
```

> [!NOTE]
> When you select individual mapped columns whose database name differs from the attribute name (for example, `Product.name` maps to the `Name` column), Core rows are keyed by the database column name. Add `.label("name")` to access the value as `row.name` instead of `row.Name`.

## Connection pooling

**SQLAlchemy** manages a connection pool by default. Tune pool settings for your workload:

```python
engine = create_engine(
    "mssql+mssqlpython://dbuser:<password>@localhost/<database>",
    pool_size=10,
    max_overflow=20,
    pool_timeout=30,
    pool_recycle=3600,
)
```

| Parameter | Description |
| --- | --- |
| `pool_size` | Number of connections to keep open (default: 5). |
| `max_overflow` | Connections allowed beyond `pool_size` (default: 10). |
| `pool_timeout` | Seconds to wait for a connection before raising an error (default: 30). |
| `pool_recycle` | Seconds after which a connection is recycled (default: -1, disabled). Set this value if your database closes idle connections. |

## Use with web frameworks

**SQLAlchemy** is commonly used as the database layer for Flask and FastAPI. The mssql-python dialect works with any framework that supports **SQLAlchemy**.

The following snippets show the recommended session-per-request pattern for each framework. They're illustrative fragments that assume the `engine` and `Product` model from the earlier sections, not complete apps. For complete, runnable applications, see the [FastAPI integration](fast-api-integration.md) and [Flask integration](flask-integration.md) articles.

### FastAPI example

Use a generator dependency to provide a session per request:

```python
from fastapi import Depends, FastAPI, HTTPException
from sqlalchemy.orm import Session, sessionmaker
from sqlalchemy import create_engine

engine = create_engine("mssql+mssqlpython://dbuser:<password>@<server>/<database>")
SessionLocal = sessionmaker(bind=engine)

app = FastAPI()


def get_db():
    db = SessionLocal()
    try:
        yield db
    finally:
        db.close()


@app.get("/products/{product_id}")
def read_product(product_id: int, db: Session = Depends(get_db)):
    product = db.get(Product, product_id)
    if not product:
        raise HTTPException(status_code=404, detail="Product not found")
    return {"name": product.name, "price": float(product.list_price)}
```

### Flask example

Use a context manager to scope the session to the request:

```python
from flask import Flask, jsonify
from sqlalchemy.orm import Session, sessionmaker
from sqlalchemy import create_engine

engine = create_engine("mssql+mssqlpython://dbuser:<password>@<server>/<database>")
SessionLocal = sessionmaker(bind=engine)

app = Flask(__name__)


@app.route("/products/<int:product_id>")
def read_product(product_id):
    with SessionLocal() as session:
        product = session.get(Product, product_id)
        if not product:
            return jsonify({"error": "Not found"}), 404
        return jsonify({"name": product.name, "price": float(product.list_price)})
```

## Alembic migrations

[Alembic](https://alembic.sqlalchemy.org/) handles schema migrations for **SQLAlchemy** projects, and it works with the mssql-python dialect. Alembic's autogenerate feature compares your models to the live database, so a few extra steps keep it from proposing changes to tables you don't manage.

### Set up Alembic

Install Alembic and initialize a migrations directory:

```bash
pip install alembic
alembic init migrations
```

In `alembic.ini`, set the connection URL:

```ini
sqlalchemy.url = mssql+mssqlpython://dbuser:<password>@localhost/<database>
```

### Point Alembic at your models

Autogenerate needs your models' metadata. Put the models Alembic manages in an importable module, such as `models.py`. Because autogenerate proposes dropping any column that a model omits, define a model that fully owns its table rather than reusing the simplified `Product` model from earlier in this article:

```python
# models.py
from datetime import datetime

from sqlalchemy import Identity, String, Integer, DateTime, func
from sqlalchemy.orm import DeclarativeBase, Mapped, mapped_column


class Base(DeclarativeBase):
    pass


class ProductReview(Base):
    __tablename__ = "ProductReview"
    __table_args__ = {"schema": "SalesLT"}

    review_id: Mapped[int] = mapped_column("ReviewID", Integer, Identity(), primary_key=True)
    product_id: Mapped[int] = mapped_column("ProductID", Integer)
    reviewer_name: Mapped[str] = mapped_column("ReviewerName", String(50))
    rating: Mapped[int] = mapped_column("Rating", Integer)
    comments: Mapped[str | None] = mapped_column("Comments", String(500))
    modified_date: Mapped[datetime] = mapped_column("ModifiedDate", DateTime, server_default=func.getdate())
```

> [!CAUTION]
> By default, autogenerate treats every table in the database that isn't in `target_metadata` as removed and emits `drop_table` for it. Against an existing database like AdventureWorksLT, that action can drop dozens of tables. Add an `include_name` filter so Alembic manages only the tables your models define, and always review the generated script before you apply it.

In `migrations/env.py`, replace `target_metadata = None` with the following code. It imports your models and limits autogenerate to the schemas and tables they define:

```python
from models import Base

target_metadata = Base.metadata

# Limit autogenerate to the tables your models define.
managed_schemas = {table.schema for table in target_metadata.tables.values()}
managed_tables = {table.name for table in target_metadata.tables.values()}


def include_name(name, type_, parent_names):
    if type_ == "schema":
        return name in managed_schemas
    if type_ == "table":
        return name in managed_tables
    return True
```

Pass `include_name` and `include_schemas=True` to `context.configure` in both `run_migrations_offline` and `run_migrations_online`. The `include_schemas=True` setting lets Alembic see tables in nondefault schemas such as `SalesLT`:

```python
context.configure(
    connection=connection,
    target_metadata=target_metadata,
    include_name=include_name,
    include_schemas=True,
)
```

### Generate and apply a migration

Generate a migration from your models:

```bash
alembic revision --autogenerate -m "add product review table"
```

Alembic detects the new table and writes a migration script:

```output
INFO  [alembic.autogenerate.compare.tables] Detected added table 'SalesLT.ProductReview'
Generating .../versions/xxxx_add_product_review_table.py ... done
```

The generated `upgrade()` creates the table, and `downgrade()` drops it:

```python
def upgrade() -> None:
    op.create_table(
        "ProductReview",
        sa.Column("ReviewID", sa.Integer(), sa.Identity(always=False), nullable=False),
        sa.Column("ProductID", sa.Integer(), nullable=False),
        sa.Column("ReviewerName", sa.String(length=50), nullable=False),
        sa.Column("Rating", sa.Integer(), nullable=False),
        sa.Column("Comments", sa.String(length=500), nullable=True),
        sa.Column("ModifiedDate", sa.DateTime(), server_default=sa.text("getdate()"), nullable=False),
        sa.PrimaryKeyConstraint("ReviewID"),
        schema="SalesLT",
    )


def downgrade() -> None:
    op.drop_table("ProductReview", schema="SalesLT")
```

Review the script, and then apply all pending migrations:

```bash
alembic upgrade head
```

## Differences from pyodbc dialect

If you're migrating from `mssql+pyodbc`, the mssql-python dialect is similar because both drivers are based on the same ODBC framework. Key differences:

| Topic | `mssql+pyodbc` | `mssql+mssqlpython` |
| --- | --- | --- |
| ODBC driver installation | Requires separate ODBC driver (for example, ODBC Driver 18 for Microsoft SQL). | Driver is bundled. No separate ODBC driver needed. |
| Connection URL | `mssql+pyodbc://user:pass@host/db?driver=ODBC+Driver+18+for+SQL+Server` | `mssql+mssqlpython://user:pass@host/db` |
| `fast_executemany` | Supported via `create_engine(..., fast_executemany=True)`. | Not applicable. The driver handles batch performance internally. |
| Availability | Stable, included in **SQLAlchemy** since 1.x. | Pre-release (**SQLAlchemy** 2.1.0b2+). |

## Known Limitations

The mssql-python dialect for **SQLAlchemy** is in **pre-release**. Before using in production, understand these implications:

- **API Changes**: Method signatures, exception types, and behavior might change before the final stable release. Always pin your **SQLAlchemy** version to a specific pre-release build (for example, `sqlalchemy==2.1.0b2`) and test upgrades thoroughly.

- **Limited Testing**: The dialect has less community testing than the stable `mssql+pyodbc` dialect. You might encounter edge cases or missing features.

- **Feature Gaps**: Some advanced ORM or Core features might not work. Refer to [SQLAlchemy MSSQL dialect documentation](https://docs.sqlalchemy.org/en/latest/dialects/mssql.html) and test your use cases before committing to a project.

- **No Support Guarantee**: Microsoft and **SQLAlchemy** provide best-effort support, but issues might not be resolved before the stable release.

**When to Use the Pre-Release**:

- Development and testing environments
- Proof-of-concept projects
- Migrating from `mssql+pyodbc` if you want to avoid the external ODBC driver dependency
- Projects where you can respond to API changes and perform regression testing

**When NOT to Use the Pre-Release**:

- Production systems with strict stability requirements
- Multi-year legacy applications where dependency updates are rare
- Critical business workloads until **SQLAlchemy** 2.1 reaches stable GA

For the latest pre-release dialect status and known issues, check the [mssql-python GitHub repository](https://github.com/microsoft/mssql-python).

## Troubleshooting

### "No module named 'sqlalchemy.dialects.mssql.mssqlpython'"

This error means your installed **SQLAlchemy** version doesn't include the mssql-python dialect. Verify you have 2.1.0b2 or later:

```bash
pip install "sqlalchemy>=2.1.0b2"
```

### Connection failures

If `create_engine` succeeds but queries fail, verify your connection parameters work with mssql-python directly:

```python
import mssql_python

conn = mssql_python.connect(
    "Server=localhost;Database=<database>;UID=dbuser;PWD=<password>;Encrypt=yes"
)
cursor = conn.cursor()
cursor.execute("SELECT 1")
print(cursor.fetchone())
conn.close()
```

If the direct connection works but **SQLAlchemy** doesn't, check for URL encoding issues in special characters within your password or server name.

## Related content

- [mssql-python connection strings](connection-strings.md)
- [mssql-python connection management](connection-management.md)
- [FastAPI integration](fast-api-integration.md)
- [Flask integration](flask-integration.md)
- [SQLAlchemy MSSQL dialect documentation](https://docs.sqlalchemy.org/en/latest/dialects/mssql.html)
