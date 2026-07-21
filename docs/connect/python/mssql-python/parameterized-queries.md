---
title: Build Parameterized Queries with mssql-python
description: Learn how to build safe parameterized queries to prevent SQL injection and improve performance with the mssql-python driver.
author: dlevy-msft-sql
ms.author: dlevy
ms.date: 07/01/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---

# Build parameterized queries

Parameterized queries are essential for:

- **Security**: Preventing SQL injection attacks
- **Performance**: Enabling query plan reuse
- **Correctness**: Proper handling of special characters and data types

The mssql-python driver uses the `pyformat` parameter style with `%(name)s` placeholders by default, but also supports other parameter styles if you prefer that format.

## Basic parameterized queries

### Named parameters

Use named placeholders to pass parameters to queries:

```python
import mssql_python

conn = mssql_python.connect(
    "Server=<server>.database.windows.net;"
    "Database=<database>;"
    "Authentication=ActiveDirectoryDefault;"
    "Encrypt=yes"
)
cursor = conn.cursor()

# Single parameter
cursor.execute(
    "SELECT * FROM Production.Product WHERE ProductSubcategoryID = %(category)s",
    {"category": 5}
)

# Multiple parameters
cursor.execute(
    "SELECT * FROM Production.Product WHERE ProductSubcategoryID = %(cat)s AND ListPrice > %(price)s",
    {"cat": 5, "price": 10.00}
)
```

### Parameter reuse

You can reference the same parameter multiple times:

```python
cursor.execute("""
    SELECT * FROM Production.Product 
    WHERE (Name LIKE %(search)s OR ProductNumber LIKE %(search)s)
    AND ProductSubcategoryID = %(cat)s
""", {"search": "%Road%", "cat": 2})
```

## Data types in parameters

### String parameters

Strings are automatically quoted and special characters are safely escaped:

```python
# Strings are automatically quoted
cursor.execute(
    "SELECT * FROM Person.EmailAddress WHERE EmailAddress = %(email)s",
    {"email": "ken0@adventure-works.com"}
)

# Special characters are escaped
cursor.execute(
    "SELECT * FROM Person.Person WHERE LastName = %(name)s",
    {"name": "O'Brien"}  # Apostrophe handled safely
)
```

### Numeric parameters

Pass numeric values as integers, decimals, or floats depending on the required precision:

```python
from decimal import Decimal

# Integer
cursor.execute("SELECT * FROM Production.Product WHERE ProductID = %(id)s", {"id": 42})

# Decimal for financial precision
cursor.execute(
    "SELECT * FROM Production.Product WHERE ListPrice >= %(min)s AND ListPrice <= %(max)s",
    {"min": Decimal("10.00"), "max": Decimal("100.00")}
)

# Float
cursor.execute(
    "SELECT * FROM Production.Product WHERE Weight > %(threshold)s",
    {"threshold": 15.0}
)
```

### Date/time parameters

Use Python's `datetime` module to pass date, datetime, and time values:

```python
from datetime import date, datetime, time

# Date
cursor.execute(
    "SELECT * FROM Sales.SalesOrderHeader WHERE OrderDate >= %(date)s AND OrderDate < DATEADD(day, 1, %(date)s)",
    {"date": date(2014, 3, 15)}
)

# Datetime
cursor.execute(
    "SELECT * FROM Sales.SalesOrderHeader WHERE ModifiedDate >= %(start)s AND ModifiedDate < %(end)s",
    {"start": datetime(2014, 3, 1), "end": datetime(2014, 4, 1)}
)

# Time
cursor.execute(
    "SELECT * FROM HumanResources.Shift WHERE StartTime >= %(time)s",
    {"time": time(9, 0, 0)}
)
```

### None for NULL

Pass `None` to insert or update NULL values in the database:

```python
# Insert NULL
cursor.execute("""
    CREATE TABLE #NullDemo (ID INT IDENTITY, Name NVARCHAR(50), Email NVARCHAR(100))
""")
cursor.execute(
    "INSERT INTO #NullDemo (Name, Email) VALUES (%(name)s, %(email)s)",
    {"name": "Guest", "email": None}
)

# Query with NULL
cursor.execute(
    "UPDATE #NullDemo SET Email = %(email)s WHERE ID = %(id)s",
    {"email": None, "id": 1}
)
```

### Binary parameters

Insert binary data as bytes objects:

```python
# Binary data
hash_value = b'\x00\x01\x02\x03'
cursor.execute("""
    CREATE TABLE #HashDemo (ID INT IDENTITY, DocumentHash VARBINARY(256))
""")
cursor.execute(
    "INSERT INTO #HashDemo (DocumentHash) VALUES (%(hash)s)",
    {"hash": hash_value}
)
```

## Build dynamic queries

### Conditional WHERE clauses

Build WHERE clauses dynamically based on optional search criteria:

```python
def search_products(cursor, name: str | None = None, 
                   category: int | None = None,
                   min_price: float | None = None) -> list:
    """Build query with optional conditions."""
    conditions = []
    params = {}
    
    if name:
        conditions.append("Name LIKE %(name)s")
        params["name"] = f"%{name}%"
    
    if category:
        conditions.append("ProductSubcategoryID = %(category)s")
        params["category"] = category
    
    if min_price is not None:
        conditions.append("ListPrice >= %(min_price)s")
        params["min_price"] = min_price
    
    query = "SELECT TOP 10 * FROM Production.Product"
    if conditions:
        query += " WHERE " + " AND ".join(conditions)
    
    cursor.execute(query, params)
    return cursor.fetchall()

# Usage
products = search_products(cursor, name="Road", min_price=10.0)
```

### IN clause with multiple values

Build the `IN` clause dynamically with a placeholder for each value. Never use string formatting to inject values directly:

```python
def get_products_by_ids(cursor, product_ids: list[int]) -> list:
    """Query with IN clause using qmark (?) placeholders."""
    if not product_ids:
        return []

    placeholders = ", ".join("?" for _ in product_ids)
    query = f"SELECT * FROM Production.Product WHERE ProductID IN ({placeholders})"
    cursor.execute(query, tuple(product_ids))
    return cursor.fetchall()

# Usage
products = get_products_by_ids(cursor, [1, 5, 10, 15])
```

The same pattern works with pyformat (`%(name)s`) placeholders:

```python
def get_products_by_ids(cursor, product_ids: list[int]) -> list:
    """Query with IN clause using pyformat placeholders."""
    if not product_ids:
        return []
    
    # Create named parameters for each ID
    params = {f"id{i}": id for i, id in enumerate(product_ids)}
    placeholders = ", ".join(f"%(id{i})s" for i in range(len(product_ids)))
    
    query = f"SELECT * FROM Production.Product WHERE ProductID IN ({placeholders})"
    cursor.execute(query, params)
    return cursor.fetchall()

# Usage
products = get_products_by_ids(cursor, [1, 5, 10, 15])
```

### Dynamic column selection

Use an allow list to validate columns before dynamically building SELECT lists, while keeping filter values as parameters:

```python
def get_employee(cursor, employee_id: int, columns: list[str] | None = None) -> dict:
    """Get employee with specified columns."""
    # Allow list of permitted columns
    allowed = {"BusinessEntityID", "LoginID", "JobTitle", "HireDate", "SalariedFlag"}
    
    if columns:
        # Validate columns against allow list
        safe_columns = [c for c in columns if c in allowed]
        if not safe_columns:
            raise ValueError("No valid columns specified")
        column_list = ", ".join(safe_columns)
    else:
        column_list = "*"
    
    # ID is always a parameter, never interpolated
    query = f"SELECT {column_list} FROM HumanResources.Employee WHERE BusinessEntityID = %(id)s"
    cursor.execute(query, {"id": employee_id})
    return cursor.fetchone()
```

### Sort order

Use an allow list to validate sort columns before interpolation:

```python
def get_products_sorted(cursor, sort_by: str = "Name", 
                       descending: bool = False) -> list:
    """Get products with validated sort order."""
    # Allow list of permitted sort columns
    allowed_sorts = {"Name", "ListPrice", "SellStartDate", "ProductID"}
    
    if sort_by not in allowed_sorts:
        sort_by = "Name"  # Default
    
    direction = "DESC" if descending else "ASC"
    
    # sort_by and direction are validated, safe to interpolate
    query = f"SELECT TOP 10 * FROM Production.Product ORDER BY {sort_by} {direction}"
    cursor.execute(query)
    return cursor.fetchall()
```

## INSERT operations

### Single insert

Insert a single row with parameterized values:

```python
cursor.execute("""
    CREATE TABLE #ParamInsert (ID INT IDENTITY, Name NVARCHAR(50), Price DECIMAL(10,2), CategoryID INT)
""")
cursor.execute("""
    INSERT INTO #ParamInsert (Name, Price, CategoryID)
    VALUES (%(name)s, %(price)s, %(category)s)
""", {"name": "New Widget", "price": 29.99, "category": 5})
conn.commit()
```

### Insert with identity return

Use OUTPUT to retrieve the generated identity value after inserting a new row:

```python
cursor.execute("""
    CREATE TABLE #IdentDemo (ProductID INT IDENTITY, Name NVARCHAR(50), Price DECIMAL(10,2), CategoryID INT)
""")
cursor.execute("""
    INSERT INTO #IdentDemo (Name, Price, CategoryID)
    OUTPUT INSERTED.ProductID
    VALUES (%(name)s, %(price)s, %(category)s)
""", {"name": "New Widget", "price": 29.99, "category": 5})

new_id = cursor.fetchval()
conn.commit()
print(f"Created product with ID: {new_id}")
```

### Batch insert with executemany

Use `executemany()` to insert multiple rows efficiently with a single parameterized statement:

> [!TIP]
> For large volumes, `bulkcopy()` is faster than `executemany()` because it uses the bulk insert protocol instead of individual INSERT statements. See [Bulk copy](bulk-copy.md).

```python
cursor.execute("""
    CREATE TABLE #BatchDemo (ID INT IDENTITY, Name NVARCHAR(50), Price DECIMAL(10,2), CategoryID INT)
""")

products = [
    {"name": "Widget A", "price": 19.99, "cat": 1},
    {"name": "Widget B", "price": 29.99, "cat": 1},
    {"name": "Widget C", "price": 39.99, "cat": 2},
]

cursor.executemany("""
    INSERT INTO #BatchDemo (Name, Price, CategoryID)
    VALUES (%(name)s, %(price)s, %(cat)s)
""", products)
conn.commit()
```

## UPDATE operations

Update single rows or multiple rows based on conditions using parameterized WHERE clauses:

```python
# Create temp table with sample data
cursor.execute("""
    CREATE TABLE #UpdDemo (
        ID INT IDENTITY, Name NVARCHAR(50),
        Price DECIMAL(10,2), CategoryID INT, ModifiedAt DATETIME
    )
""")
cursor.execute("""
    INSERT INTO #UpdDemo (Name, Price, CategoryID)
    VALUES ('Widget X', 25.00, 5), ('Widget Y', 30.00, 5), ('Gadget Z', 50.00, 3)
""")

# Single row update
cursor.execute("""
    UPDATE #UpdDemo 
    SET Price = %(price)s, ModifiedAt = %(modified)s
    WHERE ID = %(id)s
""", {"price": 34.99, "modified": datetime.now(), "id": 1})

# Conditional update
cursor.execute("""
    UPDATE #UpdDemo 
    SET Price = Price * %(multiplier)s
    WHERE CategoryID = %(category)s
""", {"multiplier": 1.1, "category": 5})
conn.commit()
```

## DELETE operations

Delete rows from tables based on parameterized filter conditions:

```python
# Create temp table with sample data
cursor.execute("""
    CREATE TABLE #DelDemo (
        ID INT IDENTITY, Name NVARCHAR(50), Status NVARCHAR(20), OrderDate DATE
    )
""")
cursor.execute("""
    INSERT INTO #DelDemo (Name, Status, OrderDate)
    VALUES ('Order1', 'Active', '2024-06-01'), ('Order2', 'Cancelled', '2022-05-01'),
           ('Order3', 'Cancelled', '2022-11-01')
""")

# Delete single row
cursor.execute(
    "DELETE FROM #DelDemo WHERE ID = %(id)s",
    {"id": 1}
)

# Delete with conditions
cursor.execute("""
    DELETE FROM #DelDemo 
    WHERE Status = %(status)s AND OrderDate < %(date)s
""", {"status": "Cancelled", "date": date(2023, 1, 1)})

conn.commit()
```

## Security considerations

### Never interpolate user input

Always use parameters to escape user input safely:

```python
# DANGEROUS - SQL injection vulnerability!
user_input = "'; DROP TABLE Users;--"
query = f"SELECT * FROM Person.Person WHERE LastName = '{user_input}'"  # DON'T DO THIS

# SAFE - always use parameters
cursor.execute(
    "SELECT * FROM Person.Person WHERE LastName = %(name)s",
    {"name": user_input}  # Input is safely escaped
)
```

### Validate table and column names

Use allow lists to validate table and column identifiers that can't be parameterized:

```python
def query_table(cursor, table: str, columns: list[str]):
    """Query with validated table and column names."""
    # Allow list of permitted tables
    allowed_tables = {"Person.Person", "Production.Product", "Sales.SalesOrderHeader"}
    if table not in allowed_tables:
        raise ValueError(f"Invalid table: {table}")
    
    # Allow list of permitted columns per table
    allowed_columns = {
        "Person.Person": {"BusinessEntityID", "FirstName", "LastName"},
        "Production.Product": {"ProductID", "Name", "ListPrice"},
        "Sales.SalesOrderHeader": {"SalesOrderID", "CustomerID", "TotalDue"},
    }
    
    safe_columns = [c for c in columns if c in allowed_columns.get(table, set())]
    if not safe_columns:
        raise ValueError("No valid columns")
    
    # Safe to interpolate after validation
    query = f"SELECT TOP 5 {', '.join(safe_columns)} FROM {table}"
    cursor.execute(query)
    return cursor.fetchall()
```

### Use stored procedures for complex operations

Stored procedures add another layer of protection and enable complex business logic to be executed server-side:

```python
cursor.execute("""
    EXECUTE dbo.uspGetEmployeeManagers @BusinessEntityID = %(id)s
""", {"id": 5})
rows = cursor.fetchall()
```

## Performance benefits

### Query plan caching

When you use parameterized queries, SQL Server reuses the same execution plan across different parameter values instead of compiling a new plan for each query.

```python
for product_id in range(1, 100):
    cursor.execute(
        "SELECT * FROM Production.Product WHERE ProductID = %(id)s",
        {"id": product_id}
    )
```

### Prepared statements

Use prepared statements for queries that you run often. The driver prepares statements automatically, so running the same query template with different parameters benefits from preparation.

```python
query = "SELECT Name, ListPrice FROM Production.Product WHERE ProductSubcategoryID = %(cat)s"

for category in [1, 2, 3, 4, 5]:
    cursor.execute(query, {"cat": category})
    products = cursor.fetchall()
```

## Related content

- [Executing queries](executing-queries.md)
- [Error handling](error-handling.md)
- [Stored procedures](stored-procedures.md)
