---
title: Use JSON Data with mssql-python
description: Learn how to work with JSON data in Microsoft SQL using the mssql-python driver, including JSON functions, path expressions, and storage patterns.
author: dlevy-msft-sql
ms.author: dlevy
ms.date: 07/16/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---

# Use JSON data with mssql-python

Microsoft SQL 2016 and later versions and Azure SQL provide JSON support through functions that operate on `nvarchar` columns. The mssql-python driver sends and receives JSON as regular Python strings. You can:

- Store JSON as strings in `nvarchar` columns.
- Query JSON with path expressions using `JSON_VALUE`, `JSON_QUERY`, and `OPENJSON`.
- Transform relational data to JSON with `FOR JSON`.
- Parse JSON into relational format with `OPENJSON`.

> [!NOTE]
> Microsoft SQL stores JSON data in `nvarchar` columns, not in a dedicated JSON column type. The mssql-python driver sends and receives JSON as regular strings. Use Python's built-in `json` module to serialize and deserialize on the client side.

## Store JSON data

Serialize Python dicts to strings with `json.dumps()` before inserting into **nvarchar** columns.

### Insert JSON string

Store a Python dictionary as JSON text in a database table:

```python
import json
import mssql_python

conn = mssql_python.connect(
    "Server=<server>.database.windows.net;"
    "Database=<database>;"
    "Authentication=ActiveDirectoryDefault;"
    "Encrypt=yes"
)
cursor = conn.cursor()

# Create table with JSON column
cursor.execute("""
    CREATE TABLE #JsonProducts (
        ProductID INT IDENTITY PRIMARY KEY,
        Name NVARCHAR(100),
        JsonData NVARCHAR(MAX)
    )
""")

# Python dict to JSON string
product_data = {
    "name": "Widget Pro",
    "specs": {
        "weight": 2.5,
        "dimensions": {"width": 10, "height": 5, "depth": 3}
    },
    "tags": ["electronics", "gadgets", "bestseller"]
}

cursor.execute("""
    INSERT INTO #JsonProducts (Name, JsonData)
    VALUES (%(name)s, %(json)s)
""", {"name": "Widget Pro", "json": json.dumps(product_data)})
conn.commit()
```

### Validate JSON on insert

Use the `ISJSON()` function to validate JSON syntax before inserting:

```python
data = {"name": "Widget", "specs": {"weight": 1.5}}

cursor.execute("""
    CREATE TABLE #JsonValidate (
        ProductID INT IDENTITY, Name NVARCHAR(100), JsonData NVARCHAR(MAX)
    )
""")
cursor.execute("""
    INSERT INTO #JsonValidate (Name, JsonData)
    SELECT %(name)s, %(json)s
    WHERE ISJSON(%(json)s) = 1
""", {"name": "Widget", "json": json.dumps(data)})

if cursor.rowcount == 0:
    raise ValueError("Invalid JSON data")
```

## Query JSON data

Use Microsoft SQL JSON path functions to extract values on the server before returning them to the client.

### Extract scalar values

Use `JSON_VALUE` to extract single values:

```python
# Create table with sample JSON data
cursor.execute("""
    CREATE TABLE #JsonExtract (
        ProductID INT IDENTITY, Name NVARCHAR(100), JsonData NVARCHAR(MAX)
    )
""")
cursor.execute("""
    INSERT INTO #JsonExtract (Name, JsonData) VALUES (
        'Widget Pro',
        '{"name":"Widget Pro","specs":{"weight":2.5,"dimensions":{"width":10,"height":5,"depth":3}},"tags":["electronics","gadgets"]}'
    )
""")

cursor.execute("""
    SELECT 
        Name,
        JSON_VALUE(JsonData, '$.specs.weight') AS Weight,
        JSON_VALUE(JsonData, '$.specs.dimensions.width') AS Width
    FROM #JsonExtract
    WHERE JSON_VALUE(JsonData, '$.name') = %(name)s
""", {"name": "Widget Pro"})

row = cursor.fetchone()
print(f"Weight: {row.Weight}, Width: {row.Width}")
```

### Extract objects or arrays

Use `JSON_QUERY` for objects and arrays.

```python
cursor.execute("""
    CREATE TABLE #JsonQuery (
        ProductID INT IDENTITY, Name NVARCHAR(100), JsonData NVARCHAR(MAX)
    )
""")
cursor.execute("""
    INSERT INTO #JsonQuery (Name, JsonData) VALUES (
        'Widget Pro',
        '{"specs":{"weight":2.5,"color":"blue"},"tags":["electronics","gadgets"]}'
    )
""")

cursor.execute("""
    SELECT 
        Name,
        JSON_QUERY(JsonData, '$.specs') AS Specs,
        JSON_QUERY(JsonData, '$.tags') AS Tags
    FROM #JsonQuery
""")

for row in cursor:
    specs = json.loads(row.Specs) if row.Specs else {}
    tags = json.loads(row.Tags) if row.Tags else []
    print(f"{row.Name}: {specs}, Tags: {tags}")
```

### Parse JSON array to rows

Expand a JSON array into rows using `OPENJSON`:

```python
cursor.execute("""
    CREATE TABLE #JsonArray (
        ProductID INT IDENTITY, Name NVARCHAR(100), JsonData NVARCHAR(MAX)
    )
""")
cursor.execute("""
    INSERT INTO #JsonArray (Name, JsonData) VALUES 
        ('Widget Pro', '{"tags":["electronics","gadgets","bestseller"]}'),
        ('Gadget X', '{"tags":["tools","gadgets"]}')
""")

cursor.execute("""
    SELECT p.Name, t.value AS Tag
    FROM #JsonArray p
    CROSS APPLY OPENJSON(p.JsonData, '$.tags') t
""")

for row in cursor:
    print(f"Product: {row.Name}, Tag: {row.Tag}")
```

### Parse JSON object to columns

Extract individual fields from JSON objects using `JSON_VALUE()` and cast the results to appropriate SQL types.

```python
cursor.execute("""
    CREATE TABLE #JsonCols (
        ProductID INT IDENTITY, Name NVARCHAR(100), JsonData NVARCHAR(MAX)
    )
""")
cursor.execute("""
    INSERT INTO #JsonCols (Name, JsonData) VALUES (
        'Widget Pro',
        '{"name":"Widget Pro","specs":{"weight":2.5,"dimensions":{"width":10,"height":5}}}'
    )
""")

cursor.execute("""
    SELECT 
        p.ProductID,
        j.name AS ProductName,
        j.weight,
        j.width,
        j.height
    FROM #JsonCols p
    CROSS APPLY OPENJSON(p.JsonData)
    WITH (
        name NVARCHAR(100) '$.name',
        weight DECIMAL(5,2) '$.specs.weight',
        width INT '$.specs.dimensions.width',
        height INT '$.specs.dimensions.height'
    ) j
""")
```

## Modify JSON data

Use `JSON_MODIFY` to update a specific path in a JSON document without rewriting the whole value.

### Update JSON value

Modify a single JSON property using `JSON_MODIFY`:

```python
cursor.execute("""
    CREATE TABLE #JsonMod (
        ProductID INT IDENTITY, Name NVARCHAR(100), JsonData NVARCHAR(MAX)
    )
""")
cursor.execute("""
    INSERT INTO #JsonMod (Name, JsonData) VALUES (
        'Widget Pro',
        '{"specs":{"weight":2.5,"dimensions":{"width":10}},"tags":["electronics"]}'
    )
""")

cursor.execute("""
    UPDATE #JsonMod
    SET JsonData = JSON_MODIFY(JsonData, '$.specs.weight', %(weight)s)
    WHERE ProductID = %(id)s
""", {"weight": 3.0, "id": 1})
conn.commit()
```

### Add JSON property

Insert a new property into an existing JSON object:

```python
cursor.execute("""
    CREATE TABLE #JsonAdd (
        ProductID INT IDENTITY, Name NVARCHAR(100), JsonData NVARCHAR(MAX)
    )
""")
cursor.execute("""
    INSERT INTO #JsonAdd (Name, JsonData) VALUES (
        'Widget Pro', '{"specs":{"weight":2.5}}'
    )
""")

cursor.execute("""
    UPDATE #JsonAdd
    SET JsonData = JSON_MODIFY(JsonData, '$.specs.color', %(color)s)
    WHERE ProductID = %(id)s
""", {"color": "blue", "id": 1})
```

### Remove JSON property

Delete a property from a JSON object by setting it to `NULL`:

```python
cursor.execute("""
    CREATE TABLE #JsonRem (
        ProductID INT IDENTITY, Name NVARCHAR(100), JsonData NVARCHAR(MAX)
    )
""")
cursor.execute("""
    INSERT INTO #JsonRem (Name, JsonData) VALUES (
        'Widget Pro', '{"specs":{"weight":2.5,"color":"blue"}}'
    )
""")

cursor.execute("""
    UPDATE #JsonRem
    SET JsonData = JSON_MODIFY(JsonData, '$.specs.color', NULL)
    WHERE ProductID = %(id)s
""", {"id": 1})
```

### Append to JSON array

Add a new value to the end of a JSON array using `append` directive in `JSON_MODIFY`:

```python
cursor.execute("""
    CREATE TABLE #JsonAppend (
        ProductID INT IDENTITY, Name NVARCHAR(100), JsonData NVARCHAR(MAX)
    )
""")
cursor.execute("""
    INSERT INTO #JsonAppend (Name, JsonData) VALUES (
        'Widget Pro', '{"tags":["electronics","gadgets"]}'
    )
""")

cursor.execute("""
    UPDATE #JsonAppend
    SET JsonData = JSON_MODIFY(
        JsonData, 
        'append $.tags', 
        %(tag)s
    )
    WHERE ProductID = %(id)s
""", {"tag": "new-arrival", "id": 1})
```

## Convert relational data to JSON

The `FOR JSON` clause transforms query results into a JSON string on the server side.

### FOR JSON AUTO

Generate JSON from query results:

```python
cursor.execute("""
    SELECT TOP 5 o.SalesOrderID, p.LastName AS CustomerName, o.TotalDue
    FROM Sales.SalesOrderHeader o
    JOIN Sales.Customer c ON o.CustomerID = c.CustomerID
    JOIN Person.Person p ON c.PersonID = p.BusinessEntityID
    FOR JSON AUTO
""")

# Result is a single string containing JSON
json_result = cursor.fetchval()
orders = json.loads(json_result)
print(json.dumps(orders, indent=2))
```

### FOR JSON PATH

Get more control over the JSON structure:

```python
cursor.execute("""
    SELECT 
        o.SalesOrderID AS 'order.id',
        o.OrderDate AS 'order.date',
        p.LastName AS 'customer.name',
        e.EmailAddress AS 'customer.email'
    FROM Sales.SalesOrderHeader o
    JOIN Sales.Customer c ON o.CustomerID = c.CustomerID
    JOIN Person.Person p ON c.PersonID = p.BusinessEntityID
    JOIN Person.EmailAddress e ON p.BusinessEntityID = e.BusinessEntityID
    WHERE o.SalesOrderID = %(id)s
    FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
""", {"id": 43659})

json_result = cursor.fetchval()
order = json.loads(json_result)
# Structure: {"order": {"id": 43659, "date": "..."}, "customer": {"name": "...", "email": "..."}}
```

### Nested JSON

Query data structures with nested JSON arrays and objects by using subqueries with `FOR JSON` to construct hierarchical JSON output.

```python
cursor.execute("""
    SELECT TOP 3
        c.CustomerID,
        p.LastName AS CustomerName,
        (SELECT TOP 3 o.SalesOrderID, o.TotalDue
         FROM Sales.SalesOrderHeader o
         WHERE o.CustomerID = c.CustomerID
         FOR JSON PATH) AS Orders
    FROM Sales.Customer c
    JOIN Person.Person p ON c.PersonID = p.BusinessEntityID
    WHERE c.PersonID IS NOT NULL
    FOR JSON PATH
""")

json_result = cursor.fetchval()
customers = json.loads(json_result)
# Each customer has nested Orders array
```

## Python integration patterns

These patterns show how to build Python abstractions over JSON-backed tables.

### Repository pattern with JSON

Implement a data access layer that serializes and deserializes Python objects to JSON columns, providing a type-safe interface to the database.

```python
from dataclasses import dataclass, asdict
from typing import Optional
import json

@dataclass
class ProductSpecs:
    weight: float
    color: str
    dimensions: dict

@dataclass
class Product:
    id: Optional[int]
    name: str
    specs: ProductSpecs

class ProductRepository:
    def __init__(self, connection):
        self.conn = connection
        cursor = self.conn.cursor()
        cursor.execute("""
            IF OBJECT_ID('#JsonRepo') IS NULL
                CREATE TABLE #JsonRepo (
                    ProductID INT IDENTITY PRIMARY KEY,
                    Name NVARCHAR(100),
                    JsonData NVARCHAR(MAX)
                )
        """)
        self.conn.commit()
    
    def save(self, product: Product) -> int:
        cursor = self.conn.cursor()
        specs_json = json.dumps(asdict(product.specs))
        
        if product.id:
            cursor.execute("""
                UPDATE #JsonRepo SET Name = %(name)s, JsonData = %(json)s
                WHERE ProductID = %(id)s
            """, {"name": product.name, "json": specs_json, "id": product.id})
        else:
            cursor.execute("""
                INSERT INTO #JsonRepo (Name, JsonData)
                OUTPUT INSERTED.ProductID
                VALUES (%(name)s, %(json)s)
            """, {"name": product.name, "json": specs_json})
            product.id = cursor.fetchval()
        
        self.conn.commit()
        return product.id
    
    def get(self, product_id: int) -> Optional[Product]:
        cursor = self.conn.cursor()
        cursor.execute("""
            SELECT ProductID, Name, JsonData FROM #JsonRepo WHERE ProductID = %(id)s
        """, {"id": product_id})
        
        row = cursor.fetchone()
        if row is None:
            return None
        
        specs_data = json.loads(row.JsonData)
        return Product(
            id=row.ProductID,
            name=row.Name,
            specs=ProductSpecs(**specs_data)
        )
```

Connect to the database, then create the repository and use it to save and retrieve a product. The `save()` method takes the INSERT branch when `id` is `None` and the UPDATE branch otherwise:

```python
conn = mssql_python.connect(connection_string)

repo = ProductRepository(conn)

# id is None, so save() inserts a new row and returns the generated ProductID.
product = Product(
    id=None,
    name="Widget Pro",
    specs=ProductSpecs(weight=2.5, color="black", dimensions={"width": 10, "height": 5})
)
product_id = repo.save(product)
print(f"Saved product {product_id}")

# Read the product back into a typed Product object.
loaded = repo.get(product_id)
print(loaded)

conn.close()
```

The repository creates `#JsonRepo` as a local temporary table scoped to the connection you pass in, so `save()` and `get()` must share that same connection. The table is dropped when the connection closes.

### Efficiently handle large JSON results

When JSON results are large, fetch them in parts across multiple rows.

```python
def fetch_json_in_parts(cursor, query: str, params: dict) -> list:
    """Handle JSON results that might span multiple rows."""
    cursor.execute(query, params)
    
    # FOR JSON might split large results across rows
    json_parts = []
    for row in cursor:
        json_parts.append(row[0])
    
    # Combine parts
    json_string = "".join(json_parts)
    return json.loads(json_string) if json_string else []

# Usage
data = fetch_json_in_parts(cursor, "SELECT TOP 100 * FROM Production.Product FOR JSON AUTO", {})
```

### Convert query results to JSON in Python

Transform relational query results into JSON format in Python by converting each row to a dictionary, then serializing to JSON.

```python
def query_to_json(cursor, query: str, params: dict = None) -> str:
    """Execute query and return results as JSON string."""
    cursor.execute(query, params or {})
    columns = [col[0] for col in cursor.description]
    
    rows = []
    for row in cursor:
        rows.append(dict(zip(columns, row)))
    
    return json.dumps(rows, default=str, indent=2)

# Usage
json_output = query_to_json(cursor, "SELECT TOP 5 ProductID, Name, ListPrice FROM Production.Product WHERE ProductSubcategoryID = %(cat)s", {"cat": 1})
print(json_output)
```

## Index JSON data

Create a computed column backed by a JSON path expression to make the path indexable.

### Computed column with index

Define a computed column that extracts a JSON value and apply an index to it for efficient filtering on frequently queried JSON paths. The following example creates a permanent table, adds a persisted computed column over the JSON `$.specs.weight` path, and creates an index on it.

```python
cursor.execute("""
    IF OBJECT_ID('dbo.ProductCatalog', 'U') IS NOT NULL
        DROP TABLE dbo.ProductCatalog
""")
cursor.execute("""
    CREATE TABLE dbo.ProductCatalog (
        ProductID   INT IDENTITY PRIMARY KEY,
        Name        NVARCHAR(100),
        JsonData    NVARCHAR(MAX)
    )
""")

# Insert sample rows with JSON data
rows = [
    ("Widget Pro",   '{"specs":{"weight":2.5,"color":"blue"}}'),
    ("Gadget X",     '{"specs":{"weight":0.8,"color":"red"}}'),
    ("Heavy Duty",   '{"specs":{"weight":9.1,"color":"gray"}}'),
]
cursor.executemany(
    "INSERT INTO dbo.ProductCatalog (Name, JsonData) VALUES (%(name)s, %(json)s)",
    [{"name": n, "json": j} for n, j in rows]
)
conn.commit()

# Add a persisted computed column that extracts weight from JSON
cursor.execute("""
    ALTER TABLE dbo.ProductCatalog
    ADD ProductWeight AS CAST(JSON_VALUE(JsonData, '$.specs.weight') AS DECIMAL(5,2)) PERSISTED
""")

# Index the computed column for efficient range queries
cursor.execute("""
    CREATE INDEX IX_ProductCatalog_Weight
    ON dbo.ProductCatalog (ProductWeight)
""")
conn.commit()
```

### Query using indexed computed column

Filter by the computed column directly. The query engine uses the index instead of scanning and parsing every JSON document.

```python
cursor.execute("""
    SELECT Name, ProductWeight
    FROM dbo.ProductCatalog
    WHERE ProductWeight > %(min_weight)s
    ORDER BY ProductWeight
""", {"min_weight": 1.0})

for row in cursor:
    print(f"{row.Name}: {row.ProductWeight} kg")

# Cleanup
cursor.execute("DROP TABLE dbo.ProductCatalog")
conn.commit()
```

## Choose between relational columns and JSON storage

Use relational columns when data has a fixed schema, needs referential integrity, participates in JOINs, or appears in WHERE clauses frequently. Use JSON columns (`nvarchar(max)`) when data is sparse, varies across rows, or represents flexible configuration or metadata.

### When to use server-side versus client-side JSON processing

Use Microsoft SQL JSON functions (`JSON_VALUE`, `JSON_QUERY`, `OPENJSON`) when you need to filter, index, or aggregate across JSON fields without fetching every row to the client. This choice is right when only a subset of rows match your criteria, or when you want computed column indexes on JSON paths.

Use client-side Python processing (`json.loads()`) when you retrieve entire documents and process them in application logic. This approach works well when you need the full document and don't filter on JSON fields in the database.

### Document-style workflows

When your application stores and retrieves entire documents, use Python-side serialization and treat the JSON column as opaque storage. Process and query the documents in Python by fetching and deserializing complete JSON blobs:

```python
import json

# Create the settings table
cursor.execute("""
    CREATE TABLE #Settings (
        UserID INT PRIMARY KEY,
        ConfigJson NVARCHAR(MAX)
    )
""")

# Store a configuration document
config = {
    "theme": "dark",
    "notifications": {"email": True, "sms": False},
    "custom_fields": {"department": "Engineering", "cost_center": "CC-100"}
}

cursor.execute(
    "INSERT INTO #Settings (UserID, ConfigJson) VALUES (%(uid)s, %(cfg)s)",
    {"uid": 1, "cfg": json.dumps(config)}
)

# Retrieve and process in Python
cursor.execute("SELECT ConfigJson FROM #Settings WHERE UserID = %(uid)s", {"uid": 1})
row = cursor.fetchone()
config = json.loads(row.ConfigJson)
print(config["notifications"]["email"])  # True
```

### Server-side JSON queries

Use Microsoft SQL JSON functions when you need to filter, index, or aggregate across JSON fields without fetching every row. This approach is more efficient than loading all rows into Python to filter in memory:

- `JSON_VALUE` extracts scalar values and can back computed column indexes.
- `JSON_QUERY` extracts objects and arrays.
- `OPENJSON` unpacks JSON into rows for `JOIN`s and aggregation.
- `JSON_MODIFY` updates specific paths without rewriting the entire document.

```python
# Filter by a JSON field server-side
cursor.execute("""
    SELECT UserID, ConfigJson
    FROM #Settings
    WHERE JSON_VALUE(ConfigJson, '$.custom_fields.department') = %(dept)s
""", {"dept": "Engineering"})
```

For frequently queried JSON paths, create a computed column with an index:

```sql
ALTER TABLE Settings
ADD Department AS JSON_VALUE(ConfigJson, '$.custom_fields.department');

CREATE INDEX IX_Settings_Department ON Settings(Department);
```

## Best practices

Apply these guidelines to use JSON columns reliably.

### Validate JSON before storage

Validate JSON and table/column identifiers before storing to prevent injection attacks.

```python
def store_json_safely(cursor, table: str, json_column: str, data: dict):
    """Store JSON with validation."""
    # Validate identifiers to prevent SQL injection
    import re
    if not re.match(r'^[A-Za-z_][A-Za-z0-9_.]*$', table):
        raise ValueError(f"Invalid table name: {table}")
    if not re.match(r'^[A-Za-z_][A-Za-z0-9_]*$', json_column):
        raise ValueError(f"Invalid column name: {json_column}")

    json_str = json.dumps(data)
    
    # Check if valid JSON in Microsoft SQL
    cursor.execute("SELECT ISJSON(%(json)s)", {"json": json_str})
    if cursor.fetchval() != 1:
        raise ValueError("Invalid JSON")
    
    cursor.execute(f"INSERT INTO {table} ({json_column}) VALUES (%(json)s)", {"json": json_str})
```

### Don't overuse JSON

Use JSON columns for flexible or sparse data, such as user preferences or custom fields. Use relational columns for:

- Frequently queried data.
- Data that needs referential integrity.
- Columns used in `WHERE` clauses.

### Handle None/NULL properly

Handle missing or optional JSON fields by inserting NULL values for columns that don't have data.

```python
cursor.execute("""
    CREATE TABLE #JsonOpt (
        ProductID INT IDENTITY, Name NVARCHAR(100), JsonData NVARCHAR(MAX)
    )
""")
cursor.execute("""
    INSERT INTO #JsonOpt (Name, JsonData) VALUES (
        'Widget Pro', '{"required_field":"value"}'
    )
""")

cursor.execute("""
    SELECT 
        Name,
        JSON_VALUE(JsonData, '$.optional_field') AS OptionalValue
    FROM #JsonOpt
""")

for row in cursor:
    # JSON_VALUE returns NULL if path doesn't exist
    value = row.OptionalValue or "default"
    print(f"{row.Name}: {value}")
```

## Related content

- [Data type mappings for mssql-python](data-type-mappings.md)
- [Handle strings and Unicode](string-unicode.md)
- [JSON data in SQL Server](../../../relational-databases/json/json-data-sql-server.md)
