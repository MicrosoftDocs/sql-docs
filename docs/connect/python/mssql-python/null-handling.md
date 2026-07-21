---
title: Handle NULL Values with mssql-python
description: Learn how to properly handle NULL values when working with SQL Server data using the mssql-python driver.
author: dlevy-msft-sql
ms.author: dlevy
ms.date: 07/16/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---

# Handle NULL values

SQL NULL represents missing or unknown data. The mssql-python driver maps SQL NULL to Python `None`. The distinction matters because NULL doesn't equal anything, including itself. In SQL, `NULL = NULL` evaluates to NULL (unknown), not true, so use `IS NULL` in queries and `is None` in Python.

## Receive NULL values

### NULL in fetch results

The driver returns NULL values from SQL Server as Python `None`:

```python
import mssql_python

conn = mssql_python.connect(connection_string)
cursor = conn.cursor()

cursor.execute(
    "SELECT TOP 1 FirstName, MiddleName, LastName "
    "FROM Person.Person WHERE MiddleName IS NULL"
)
row = cursor.fetchone()

print(row.FirstName)   # First name value
print(row.MiddleName)  # None (NULL in database)
print(row.LastName)    # Last name value
```

### Check for NULL values

Check if a value is `None` using the `is` operator when iterating through results:

```python
cursor.execute(
    "SELECT FirstName, MiddleName, LastName FROM Person.Person WHERE BusinessEntityID <= 10"
)

for row in cursor:
    if row.MiddleName is None:
        print(f"{row.FirstName} {row.LastName}: No middle name")
    else:
        print(f"{row.FirstName} {row.MiddleName} {row.LastName}")
```

### Use `is None` instead of `== None`

Always use `is None` for NULL checks. The `is` operator checks identity (whether the value is literally `None`), while `==` calls `__eq__` and can give unexpected results with custom objects:

```python
# Correct
if row.MiddleName is None:
    full_name = f"{row.FirstName} {row.LastName}"

# Avoid (works but not idiomatic)
if row.MiddleName == None:
    full_name = f"{row.FirstName} {row.LastName}"
```

## Send NULL values

### Insert NULL with None

To insert NULL values, pass `None`:

```python
cursor.execute(
    "CREATE TABLE #NullInsertDemo "
    "(Name NVARCHAR(50), Email NVARCHAR(100), Phone NVARCHAR(20))"
)
cursor.execute(
    "INSERT INTO #NullInsertDemo (Name, Email, Phone) "
    "VALUES (%(name)s, %(email)s, %(phone)s)",
    {"name": "Alice", "email": None, "phone": "555-1234"}
)
conn.commit()
```

### Update to NULL

Set a column to NULL by passing `None` in the parameters:

```python
cursor.execute(
    "CREATE TABLE #UpdateDemo (ID INT, Email NVARCHAR(100))"
)
cursor.execute("INSERT INTO #UpdateDemo VALUES (100, 'old@example.com')")
cursor.execute(
    "UPDATE #UpdateDemo SET Email = %(email)s WHERE ID = %(id)s",
    {"email": None, "id": 100}
)
conn.commit()
```

### Conditional NULL handling

Define functions that handle optional parameters by setting them to `None` when not provided:

```python
def update_record(cursor, record_id: int, name: str, email: str | None = None):
    """Update record, setting email to NULL if not provided."""
    cursor.execute(
        "UPDATE #Records SET Name = %(name)s, Email = %(email)s "
        "WHERE ID = %(id)s",
        {"name": name, "email": email, "id": record_id}
    )
```

## NULL in WHERE clauses

### IS NULL in queries

Use `IS NULL` in SQL for NULL comparisons:

```python
# Find people without a middle name
cursor.execute("SELECT FirstName FROM Person.Person WHERE MiddleName IS NULL")

# Find people with a middle name
cursor.execute("SELECT FirstName FROM Person.Person WHERE MiddleName IS NOT NULL")
```

### Dynamic NULL handling

When a parameter might be NULL, use conditional logic to construct the appropriate query:

```python
def find_people(cursor, middle_name: str | None = None):
    """Find people, optionally filtering by middle name."""
    if middle_name is None:
        # Find people with NULL middle name
        cursor.execute("SELECT * FROM Person.Person WHERE MiddleName IS NULL")
    else:
        # Find people with specific middle name
        cursor.execute(
            "SELECT * FROM Person.Person WHERE MiddleName = %(middle_name)s",
            {"middle_name": middle_name},
        )
    return cursor.fetchall()
```

### COALESCE for NULL substitution

Use `COALESCE` to substitute default values for NULL at the SQL level. `COALESCE` is more efficient than checking for `None` in Python because the substitution happens on the server, reducing the amount of conditional logic in your application:

```python
cursor.execute("""
    SELECT 
        FirstName,
        COALESCE(MiddleName, '(none)') AS MiddleName,
        COALESCE(Suffix, 'N/A') AS Suffix
    FROM Person.Person
    WHERE BusinessEntityID <= 10
""")

for row in cursor:
    # MiddleName and Suffix will never be None
    print(f"{row.FirstName}: {row.MiddleName}, {row.Suffix}")
```

## NULL-safe operations

### Default values in Python

```python
cursor.execute("SELECT TOP 10 Name, Color FROM Production.Product")

for row in cursor:
    # Use or to provide default
    color = row.Color or "No color"
    print(f"{row.Name}: {color}")
```

### Format NULL values

```python
def format_address(row):
    """Format address handling NULL components."""
    parts = [
        row.AddressLine1,
        row.AddressLine2,
        row.City,
        row.PostalCode,
    ]
    # Filter out None values
    return ", ".join(str(p) for p in parts if p is not None)

cursor.execute(
    "SELECT TOP 10 AddressLine1, AddressLine2, City, PostalCode "
    "FROM Person.Address"
)
for row in cursor:
    print(format_address(row))
```

### NULL in aggregations

SQL aggregate functions handle NULL values differently than you might expect. `COUNT(column)` counts only non-NULL values, while `COUNT(*)` counts all rows. `AVG`, `SUM`, `MIN`, and `MAX` all ignore NULL values. If every value in the column is NULL, these functions return NULL (not zero).

```python
# COUNT excludes NULL values
cursor.execute("SELECT COUNT(Color) FROM Production.Product")  # Counts non-NULL colors
color_count = cursor.fetchval()

# COUNT(*) includes all rows
cursor.execute("SELECT COUNT(*) FROM Production.Product")  # Counts all products
total_count = cursor.fetchval()

# AVG ignores NULL
cursor.execute("SELECT AVG(Weight) FROM Production.Product")  # Average of non-NULL weights
average_weight = cursor.fetchval()
```

## NULL with data types

### NULL numeric values

```python
from decimal import Decimal

cursor.execute("SELECT ListPrice FROM Production.Product WHERE ProductID = 1")
row = cursor.fetchone()

# Check before arithmetic
if row.ListPrice is not None:
    tax = row.ListPrice * Decimal("0.08")
    total = row.ListPrice + tax
else:
    total = Decimal("0")
```

### NULL date values

Check if a date column is `None` before using it in comparisons or calculations:

```python
from datetime import date

cursor.execute("SELECT Name, SellEndDate FROM Production.Product WHERE ProductID <= 10")

for row in cursor:
    if row.SellEndDate is None:
        print(f"{row.Name}: Currently selling")
    else:
        print(f"{row.Name}: Discontinued on {row.SellEndDate}")
```

### NULL string values

Handle NULL string columns by checking for `None` before concatenating:

```python
cursor.execute(
    "SELECT TOP 10 FirstName, MiddleName, LastName FROM Person.Person"
)

for row in cursor:
    # Build full name, handling NULL middle name
    if row.MiddleName:
        full_name = f"{row.FirstName} {row.MiddleName} {row.LastName}"
    else:
        full_name = f"{row.FirstName} {row.LastName}"
    print(full_name)
```

## Bulk operations with NULL

### executemany with NULL values

When using `executemany()`, pass `None` in dictionaries for columns that should be NULL:

```python
users = [
    {"name": "Alice", "title": "Ms.", "suffix": "Jr."},
    {"name": "Bob", "title": None, "suffix": "Sr."},  # NULL title
    {"name": "Carol", "title": "Dr.", "suffix": None},  # NULL suffix
]

cursor.executemany(
    "SELECT FirstName FROM Person.Person WHERE FirstName = %(name)s",
    users
)
```

### Bulk copy with NULL

Bulk copy operations preserve NULL values from your data structures:

```python
cursor = conn.cursor()

cursor.execute("CREATE TABLE ##NullDemo (Name NVARCHAR(50), Email NVARCHAR(100), Phone NVARCHAR(20))")
conn.commit()

data = [
    ("Alice", "alice@example.com", "555-0001"),
    ("Bob", None, "555-0002"),      # NULL Email
    ("Carol", "carol@example.com", None),  # NULL Phone
]

result = cursor.bulkcopy("##NullDemo", data)
conn.commit()
print(f"Copied {result['rows_copied']} rows")
```

## Common patterns

### Optional field processing

Use type hints to clarify which fields can be NULL when mapping rows to dataclasses:

```python
from dataclasses import dataclass
from typing import Optional

@dataclass
class PersonRecord:
    business_entity_id: int
    first_name: str
    middle_name: Optional[str] = None
    suffix: Optional[str] = None

def fetch_person(cursor, person_id: int) -> Optional[PersonRecord]:
    cursor.execute(
        "SELECT BusinessEntityID, FirstName, MiddleName, Suffix "
        "FROM Person.Person WHERE BusinessEntityID = %(id)s",
        {"id": person_id},
    )
    row = cursor.fetchone()
    if row is None:
        return None
    return PersonRecord(
        business_entity_id=row.BusinessEntityID,
        first_name=row.FirstName,
        middle_name=row.MiddleName,  # Will be None if NULL
        suffix=row.Suffix,           # Will be None if NULL
    )
```

### JSON serialization with NULL

Python `None` values automatically convert to JSON `null` when using the `json` module:

```python
import json

cursor.execute(
    "SELECT TOP 5 BusinessEntityID, FirstName, MiddleName FROM Person.Person"
)
rows = cursor.fetchall()

# Convert to JSON-serializable list
people = []
for row in rows:
    people.append({
        "id": row.BusinessEntityID,
        "name": row.FirstName,
        "middle_name": row.MiddleName,  # None becomes null in JSON
    })

json_output = json.dumps(people, indent=2)
print(json_output)
# [
#   {"id": 1, "name": "Ken", "middle_name": "J"},
#   {"id": 3, "name": "Roberto", "middle_name": null}
# ]
```

### Dictionary with NULL filtering

Opt to exclude NULL values when converting rows to dictionaries:

```python
def row_to_dict(row, cursor) -> dict:
    """Convert row to dict, optionally excluding NULL values."""
    columns = [col[0] for col in cursor.description]
    return {col: val for col, val in zip(columns, row) if val is not None}

cursor.execute("SELECT * FROM Person.Person WHERE BusinessEntityID = 1")
row = cursor.fetchone()
person_dict = row_to_dict(row, cursor)
# Only includes non-NULL columns
```

## NULL in DataFrames

When you work with pandas or Polars DataFrames, you need to pay extra attention to null values because these libraries use their own sentinel values.

### pandas NaN and NaT

pandas uses `NaN` (Not a Number) for missing numeric and string values, and `NaT` (Not a Time) for missing datetime values. Neither value is the same as Python `None`:

```python
import pandas as pd
import numpy as np

# When reading SQL results into pandas, NULL becomes NaN or NaT
cursor.execute("SELECT Name, Weight, SellEndDate FROM Production.Product")
table = cursor.arrow()
df = table.to_pandas()

# Check for missing values (covers NaN, NaT, and None)
print(df["Weight"].isna().sum())       # Count of NULL weights
print(df["SellEndDate"].isna().sum())  # Count of NULL dates

# Stage the data in a temp table to avoid mutating the source table
cursor.execute("CREATE TABLE #ProductWeights (Name NVARCHAR(100), Weight DECIMAL(8, 2) NULL)")

# Convert NaN back to None so NULL values round-trip correctly
for _, row in df.iterrows():
    weight = None if pd.isna(row["Weight"]) else float(row["Weight"])
    cursor.execute(
        "INSERT INTO #ProductWeights (Name, Weight) VALUES (%(name)s, %(weight)s)",
        {"name": row["Name"], "weight": weight}
    )
```

> [!WARNING]
> Don't compare with `== np.nan` or `== pd.NaT`. These comparisons always return `False`. Use `pd.isna()` or `pd.notna()` instead.

### Polars null handling

Polars uses its own `null` value (not `NaN`) which maps directly to Python `None`:

```python
import polars as pl

cursor.execute("SELECT Name, Weight, Color FROM Production.Product")
table = cursor.arrow()
df = pl.from_arrow(table)

# Filter rows with non-null values
has_weight = df.filter(pl.col("Weight").is_not_null())

# Replace null with a default
df = df.with_columns(pl.col("Color").fill_null("No color"))
```

## Related content

- [Data type mappings](data-type-mappings.md)
- [Retrieving data](retrieving-data.md)
- [Row objects](row-objects.md)
