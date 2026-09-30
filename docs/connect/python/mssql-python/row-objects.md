---
title: Row Objects in mssql-python
description: Learn how to work with Row objects returned by cursor fetch operations in the mssql-python driver.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, randolphwest
ms.date: 07/16/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
ai-usage: ai-assisted
---

# Row objects in mssql-python

The mssql-python driver returns row data as `Row` objects from fetch operations. These objects provide flexible access patterns:

- **Attribute access** (`row.ColumnName`) is the most readable choice for named columns. Use it when your query has a known, stable column list.
- **String key access** (`row['ColumnName']`) provides dictionary-style access to columns by name, useful when you need programmatic column lookup or when column names contain spaces or special characters.
- **Index access** (`row[0]`) is useful for dynamic queries where column names aren't known at development time, or when processing `SELECT *` results.
- **Tuple unpacking** (`a, b, c = row`) is the most concise choice for loop bodies with a small, fixed number of columns.

## Attribute access

Access column values directly by name:

```python
import mssql_python

conn = mssql_python.connect(connection_string)
cursor = conn.cursor()

cursor.execute("SELECT ProductID, Name, ListPrice FROM Production.Product WHERE ProductID = 1")
row = cursor.fetchone()

print(row.ProductID)     # 1
print(row.Name)          # 'Adjustable Race'
print(row.ListPrice)     # 0.00
```

### Case sensitivity

Column name access is case-sensitive and matches the column names returned by SQL Server. If your database uses inconsistent casing, use SQL `AS` aliases to normalize names, or enable the `lowercase` module setting (see [Module configuration](module-configuration.md)):

```python
cursor.execute("SELECT FirstName, LastName, EmailPromotion FROM Person.Person WHERE BusinessEntityID < 10")
row = cursor.fetchone()

print(row.FirstName)      # Works
print(row.LastName)       # Works
print(row.EmailPromotion) # Works
print(row.firstname)      # AttributeError - wrong case
```

### Column aliases

Use SQL aliases to create friendly attribute names:

```python
cursor.execute("""
    SELECT 
        p.ProductID,
        p.Name,
        c.Name AS Category
    FROM Production.Product p
    JOIN Production.ProductSubcategory c ON p.ProductSubcategoryID = c.ProductSubcategoryID
    WHERE p.ProductSubcategoryID IS NOT NULL
""")

for row in cursor:
    print(f"{row.Name} ({row.Category})")
```

## Index access

Access values by zero-based column index:

```python
cursor.execute("SELECT ProductID, Name, ListPrice FROM Production.Product WHERE ProductID < 5")
row = cursor.fetchone()

print(row[0])  # ProductID
print(row[1])  # Name
print(row[2])  # ListPrice
```

### Negative indexing

The driver supports Python-style negative indexing:

```python
cursor.execute("""
    SELECT Demo.A, Demo.B, Demo.C, Demo.D
    FROM (VALUES (1, 2, 3, 4)) AS Demo(A, B, C, D)
""")
row = cursor.fetchone()

print(row[-1])  # Last column (D)
print(row[-2])  # Second to last (C)
```

## String key access

Access column values by name using dictionary-style syntax:

```python
cursor.execute("SELECT ProductID, Name, ListPrice FROM Production.Product WHERE ProductID = 1")
row = cursor.fetchone()

print(row['ProductID'])   # 1
print(row['Name'])        # 'Adjustable Race'
print(row['ListPrice'])   # Decimal('0.00')
```

This is useful when column names contain spaces or special characters, or when you need to access columns programmatically:

```python
column_name = 'ListPrice'
value = row[column_name]  # Programmatic column access
```

### Slicing

Extract multiple values using slices:

```python
cursor.execute("""
    SELECT Demo.A, Demo.B, Demo.C, Demo.D, Demo.E
    FROM (VALUES (1, 2, 3, 4, 5)) AS Demo(A, B, C, D, E)
""")
row = cursor.fetchone()

print(row[1:4])    # Columns B, C, D (indices 1, 2, 3)
print(row[:2])     # First two columns (A, B)
print(row[2:])     # From C to end
```

## Tuple unpacking

Unpack row values directly into variables:

```python
cursor.execute("SELECT ProductID, Name, ListPrice FROM Production.Product WHERE ProductID < 10")

for product_id, name, price in cursor:
    print(f"#{product_id}: {name} - ${price}")
```

### Partial unpacking

Use `*` to capture remaining values:

```python
cursor.execute("""
    SELECT TOP 2 ProductID, Name, ProductNumber, Color, Size, Weight
    FROM Production.Product
    WHERE ProductNumber IS NOT NULL
    ORDER BY ProductID
""")

for product_id, name, *rest in cursor:
    print(f"{product_id}: {name}, extra columns: {rest}")
```

## Row length and iteration

Work with row dimensions and iterate through column values:

### Get column count

Use `len()` to get the number of columns in a row:

```python
cursor.execute("SELECT * FROM Production.Product WHERE ProductID < 5")
row = cursor.fetchone()

print(len(row))  # Number of columns
```

### Iterate over values

Loop through column values in order:

```python
cursor.execute("SELECT ProductID, Name, ListPrice FROM Production.Product WHERE ProductID < 5")
row = cursor.fetchone()

for value in row:
    print(value)
```

### Check if column exists

Use `hasattr()` to test for the presence of a column name:

```python
# Use hasattr to check for column name
if hasattr(row, 'DiscountPrice'):
    print(f"Discount: {row.DiscountPrice}")
else:
    print("No discount available")
```

## Convert to built-in types

Row objects can be converted to standard Python types for integration with other libraries and APIs:

### Convert to tuple

Convert a row to a tuple using the `tuple()` constructor:

```python
cursor.execute("SELECT ProductID, Name FROM Production.Product WHERE ProductID = 1")
row = cursor.fetchone()

row_tuple = tuple(row)
print(row_tuple)  # (1, 'Adjustable Race')
```

### Convert to list

Convert a row to a list using the `list()` constructor:

```python
row_list = list(row)
print(row_list)  # [1, 'Adjustable Race']
```

### Convert to dictionary

Convert a row to a dictionary when you need to serialize it to JSON, pass it to a template engine, or merge it with other data. Build the dictionary from `cursor.description` and row values:

```python
cursor.execute("SELECT ProductID, Name, ListPrice FROM Production.Product WHERE ProductID = 1")
row = cursor.fetchone()

# Create dict from description and values
columns = [col[0] for col in cursor.description]
row_dict = dict(zip(columns, row))
print(row_dict)  # {'ProductID': 1, 'Name': 'Adjustable Race', 'ListPrice': Decimal('0.00')}
```

### Helper function for dict conversion

Create a reusable helper function to convert all fetched rows to dictionaries:

```python
def rows_to_dicts(cursor):
    """Convert fetched rows to list of dictionaries."""
    columns = [col[0] for col in cursor.description]
    return [dict(zip(columns, row)) for row in cursor.fetchall()]

cursor.execute("SELECT ProductID, Name, ListPrice FROM Production.Product WHERE ProductID < 10")
products = rows_to_dicts(cursor)
for p in products:
    print(p['Name'])
```

## Work with nullable values

The driver returns NULL values as Python `None`:

```python
cursor.execute("SELECT FirstName, MiddleName, LastName FROM Person.Person WHERE BusinessEntityID = 1")
row = cursor.fetchone()

if row.MiddleName is None:
    full_name = f"{row.FirstName} {row.LastName}"
else:
    full_name = f"{row.FirstName} {row.MiddleName} {row.LastName}"
```

## Work with cursor.description

Access column metadata alongside row data:

```python
cursor.execute("SELECT ProductID, Name, ListPrice FROM Production.Product WHERE ProductID < 5")

# Column information
for col in cursor.description:
    print(f"Name: {col[0]}, Type: {col[1]}")

# Fetch with metadata
row = cursor.fetchone()
for i, col in enumerate(cursor.description):
    print(f"{col[0]}: {row[i]}")
```

## Common patterns

Here are practical patterns for working with Row objects in real-world applications:

### Process rows with named access

Process rows using attribute access for readable, maintainable code:

```python
def process_orders(conn):
    cursor = conn.cursor()
    cursor.execute("""
        SELECT SalesOrderID, CustomerID, OrderDate, TotalDue 
        FROM Sales.SalesOrderHeader 
        WHERE Status = 5
    """)
    
    for order in cursor:
        print(f"Order #{order.SalesOrderID}")
        print(f"  Customer: {order.CustomerID}")
        print(f"  Date: {order.OrderDate}")
        print(f"  Total: ${order.TotalDue:.2f}")
```

### Build objects from rows

For applications with a domain model, map rows to data classes or typed objects. This mapping gives you IDE autocompletion, type checking, and a clear boundary between database rows and application logic.

```python
from dataclasses import dataclass
from datetime import date
from decimal import Decimal

@dataclass
class Product:
    id: int
    name: str
    price: Decimal
    created: date

def get_products(conn) -> list[Product]:
    cursor = conn.cursor()
    cursor.execute("SELECT ProductID, Name, ListPrice, SellStartDate FROM Production.Product WHERE ProductID < 10")
    
    return [
        Product(
            id=row.ProductID,
            name=row.Name,
            price=row.ListPrice,
            created=row.SellStartDate
        )
        for row in cursor
    ]
```

### Export to JSON

Serialize Row objects to JSON with custom type handling for datetime and Decimal values:

```python
import json
from datetime import date, datetime
from decimal import Decimal

def json_serializer(obj):
    """Custom serializer for non-JSON types."""
    if isinstance(obj, (date, datetime)):
        return obj.isoformat()
    if isinstance(obj, Decimal):
        return float(obj)
    raise TypeError(f"Type {type(obj)} not serializable")

def export_to_json(cursor, filename):
    columns = [col[0] for col in cursor.description]
    rows = [dict(zip(columns, row)) for row in cursor.fetchall()]
    
    with open(filename, 'w') as f:
        json.dump(rows, f, default=json_serializer, indent=2)

cursor.execute("SELECT ProductID, Name, ListPrice FROM Production.Product WHERE ProductID < 10")
export_to_json(cursor, "products.json")
```

### Aggregate into groups

Group rows by a column value and collect them into a dictionary for analysis or display:

```python
from collections import defaultdict

cursor.execute("""
    SELECT c.Name AS CategoryName, p.Name AS ProductName, p.ListPrice 
    FROM Production.Product p
    JOIN Production.ProductSubcategory c ON p.ProductSubcategoryID = c.ProductSubcategoryID
    WHERE p.ProductSubcategoryID IS NOT NULL
    ORDER BY c.Name
""")

products_by_category = defaultdict(list)
for row in cursor:
    products_by_category[row.CategoryName].append({
        'name': row.ProductName,
        'price': row.ListPrice
    })

for category, products in products_by_category.items():
    print(f"\n{category}:")
    for p in products:
        print(f"  - {p['name']}: ${p['price']}")
```

## Related content

- [Retrieve data with mssql-python](retrieving-data.md)
- [Data type mappings for mssql-python](data-type-mappings.md)
- [Custom type converters with mssql-python](custom-type-converters.md)
