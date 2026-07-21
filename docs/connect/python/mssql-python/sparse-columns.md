---
title: Use Sparse Columns and Column Sets with mssql-python
description: Learn how to work with Microsoft SQL sparse columns and column sets for wide tables using the mssql-python driver.
author: dlevy-msft-sql
ms.author: dlevy
ms.date: 07/16/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---

# Use sparse columns and column sets with mssql-python

Sparse columns are a Microsoft SQL storage optimization for NULL values in tables with many nullable columns. Client applications see sparse columns as regular columns. The mssql-python driver reads and writes them like any other column without special handling.

| Feature | Description |
| --------- | ------------- |
| Sparse columns | NULL values use zero storage |
| Column sets | XML representation of all sparse columns |
| Wide tables | Support for up to 30,000 columns |

> [!NOTE]
> Sparse columns are a server-side feature. The mssql-python driver doesn't require any special configuration or API to work with sparse columns. The only client-visible difference: when you use column sets, they return an XML representation of sparse column values.

Best suited for:

- Tables with 20-50%+ NULL values.
- Document storage with variable attributes.
- EAV (Entity-Attribute-Value) patterns.
- Sensor data with many optional readings.

## Create sparse columns

Define sparse columns in your table schema by adding the `SPARSE NULL` modifier to columns that will frequently hold NULL values.

### Basic sparse column table

Create a table with sparse columns for optional attributes.

```sql
CREATE TABLE ProductAttributes (
    ProductID INT PRIMARY KEY,
    ProductName NVARCHAR(100) NOT NULL,
    -- Sparse columns for optional attributes
    Color NVARCHAR(50) SPARSE NULL,
    Size NVARCHAR(20) SPARSE NULL,
    Weight DECIMAL(10,2) SPARSE NULL,
    Material NVARCHAR(100) SPARSE NULL,
    Warranty INT SPARSE NULL,
    Manufacturer NVARCHAR(100) SPARSE NULL
);
```

### With column set

Add a column set to provide XML access to all sparse columns simultaneously.

```sql
CREATE TABLE ProductAttributesWithSet (
    ProductID INT PRIMARY KEY,
    ProductName NVARCHAR(100) NOT NULL,
    -- Column set provides XML access to all sparse columns
    SparseAttributes XML COLUMN_SET FOR ALL_SPARSE_COLUMNS,
    -- Sparse columns
    Color NVARCHAR(50) SPARSE NULL,
    Size NVARCHAR(20) SPARSE NULL,
    Weight DECIMAL(10,2) SPARSE NULL,
    Material NVARCHAR(100) SPARSE NULL,
    Warranty INT SPARSE NULL,
    Manufacturer NVARCHAR(100) SPARSE NULL
);
```

## Insert sparse column data

Insert into sparse columns by name, just as you would with regular columns.

### Insert individual columns

Insert products with specific sparse column values populated.

```python
import mssql_python

conn = mssql_python.connect(connection_string)
cursor = conn.cursor()

# Create the table with sparse columns
cursor.execute("DROP TABLE IF EXISTS ProductAttributes")
cursor.execute("""
    CREATE TABLE ProductAttributes (
        ProductID INT PRIMARY KEY,
        ProductName NVARCHAR(100) NOT NULL,
        Color NVARCHAR(50) SPARSE NULL,
        Size NVARCHAR(20) SPARSE NULL,
        Weight DECIMAL(10,2) SPARSE NULL,
        Material NVARCHAR(100) SPARSE NULL,
        Warranty INT SPARSE NULL,
        Manufacturer NVARCHAR(100) SPARSE NULL
    )
""")

# Insert with some sparse columns populated
cursor.execute("""
    INSERT INTO ProductAttributes (ProductID, ProductName, Color, Size)
    VALUES (%(id)s, %(name)s, %(color)s, %(size)s)
""", {"id": 1, "name": "T-Shirt", "color": "Blue", "size": "Large"})

# Insert with different sparse columns
cursor.execute("""
    INSERT INTO ProductAttributes (ProductID, ProductName, Weight, Material)
    VALUES (%(id)s, %(name)s, %(weight)s, %(material)s)
""", {"id": 2, "name": "Coffee Mug", "weight": 0.35, "material": "Ceramic"})

conn.commit()
```

### Insert through column set (XML)

Insert multiple sparse column values at once by passing XML to the column set.

```python
# Create the table with a column set
cursor.execute("DROP TABLE IF EXISTS ProductAttributesWithSet")
cursor.execute("""
    CREATE TABLE ProductAttributesWithSet (
        ProductID INT PRIMARY KEY,
        ProductName NVARCHAR(100) NOT NULL,
        SparseAttributes XML COLUMN_SET FOR ALL_SPARSE_COLUMNS,
        Color NVARCHAR(50) SPARSE NULL,
        Size NVARCHAR(20) SPARSE NULL,
        Weight DECIMAL(10,2) SPARSE NULL,
        Material NVARCHAR(100) SPARSE NULL,
        Warranty INT SPARSE NULL,
        Manufacturer NVARCHAR(100) SPARSE NULL
    )
""")

# Insert using column set XML
cursor.execute("""
    INSERT INTO ProductAttributesWithSet (ProductID, ProductName, SparseAttributes)
    VALUES (%(id)s, %(name)s, %(xml)s)
""", {
    "id": 3,
    "name": "Laptop Bag",
    "xml": "<Color>Black</Color><Size>Medium</Size><Material>Nylon</Material><Warranty>24</Warranty>"
})
conn.commit()
```

### Dynamic attribute insertion

Create a function that accepts dynamic attributes as a dictionary and builds the XML automatically.

```python
def insert_with_attributes(cursor, product_id: int, name: str, attributes: dict):
    """Insert product with dynamic sparse column attributes."""
    # Build XML for column set
    xml_parts = [f"<{key}>{value}</{key}>" for key, value in attributes.items()]
    attributes_xml = "".join(xml_parts)
    
    cursor.execute("""
        INSERT INTO ProductAttributesWithSet (ProductID, ProductName, SparseAttributes)
        VALUES (%(id)s, %(name)s, %(xml)s)
    """, {"id": product_id, "name": name, "xml": attributes_xml or None})

# Usage
insert_with_attributes(cursor, 4, "Headphones", {
    "Color": "Silver",
    "Warranty": 12,
    "Manufacturer": "AudioTech"
})
conn.commit()
```

## Query sparse columns

Query sparse columns by name, or retrieve all sparse values at once through the column set.

### Query individual columns

Query specific sparse columns using standard SELECT syntax.

```python
# Query specific sparse columns
cursor.execute("""
    SELECT ProductID, ProductName, Color, Size
    FROM ProductAttributes
    WHERE Color IS NOT NULL
""")

for row in cursor:
    print(f"{row.ProductName}: {row.Color}, {row.Size}")
```

### Query through column set

Retrieve all sparse column values as XML from the column set.

```python
# Get column set XML
cursor.execute("""
    SELECT ProductID, ProductName, SparseAttributes
    FROM ProductAttributesWithSet
    WHERE ProductID = %(id)s
""", {"id": 3})

row = cursor.fetchone()
print(f"Product: {row.ProductName}")
print(f"Attributes XML: {row.SparseAttributes}")
```

### Parse column set XML in Python

Parse the XML from the column set to convert it into a Python dictionary for easier manipulation.

```python
from xml.etree import ElementTree as ET

def get_product_attributes(cursor, product_id: int) -> dict:
    """Get product with parsed sparse attributes."""
    cursor.execute("""
        SELECT ProductName, SparseAttributes
        FROM ProductAttributesWithSet
        WHERE ProductID = %(id)s
    """, {"id": product_id})
    
    row = cursor.fetchone()
    if row is None:
        return None
    
    result = {"ProductName": row.ProductName}
    
    # Parse XML column set
    if row.SparseAttributes:
        # Wrap in root element for parsing
        xml_str = f"<root>{row.SparseAttributes}</root>"
        root = ET.fromstring(xml_str)
        
        for elem in root:
            result[elem.tag] = elem.text
    
    return result

# Usage
product = get_product_attributes(cursor, 3)
print(product)
# {'ProductName': 'Laptop Bag', 'Color': 'Black', 'Size': 'Medium', 'Material': 'Nylon', 'Warranty': '24'}
```

### Query with SELECT *

When you use SELECT *, the column set returns as a single XML column instead of individual sparse columns.

```python
# SELECT * returns column set instead of individual sparse columns
cursor.execute("""
    SELECT * FROM ProductAttributesWithSet WHERE ProductID = %(id)s
""", {"id": 3})

row = cursor.fetchone()
# Returns: ProductID, ProductName, SparseAttributes (not individual columns)
print(f"Columns: {[col[0] for col in cursor.description]}")
```

### Query individual columns explicitly

To retrieve individual sparse columns from a table with a column set, list them explicitly in the SELECT clause.

```python
# To get individual sparse columns with column set table, list them explicitly
cursor.execute("""
    SELECT ProductID, ProductName, Color, Size, Weight, Material, Warranty, Manufacturer
    FROM ProductAttributesWithSet
    WHERE ProductID = %(id)s
""", {"id": 3})

# Now each sparse column is available as separate property
row = cursor.fetchone()
print(f"Color: {row.Color}, Material: {row.Material}")
```

## Update sparse columns

Update individual sparse columns by name or replace all sparse values at once via the column set XML.

### Update individual columns

Update specific sparse column values using standard SQL UPDATE syntax.

```python
cursor.execute("""
    UPDATE ProductAttributes
    SET Color = %(color)s, Weight = %(weight)s
    WHERE ProductID = %(id)s
""", {"id": 1, "color": "Red", "weight": 0.2})
conn.commit()
```

### Update through column set

Replace all sparse column values by updating the column set XML directly.

```python
# Replace all sparse column values via column set
cursor.execute("""
    UPDATE ProductAttributesWithSet
    SET SparseAttributes = %(xml)s
    WHERE ProductID = %(id)s
""", {
    "id": 3,
    "xml": "<Color>Navy</Color><Size>Large</Size><Material>Leather</Material>"
})
conn.commit()
# Note: This clears any sparse columns not included in the XML
```

### Partial update through column set

Update only specific sparse attributes while preserving the values of other attributes not included in the update.

```python
# To update only specific attributes, merge with existing
def update_attributes(cursor, product_id: int, updates: dict):
    """Update specific sparse attributes while preserving others."""
    # Get current attributes
    cursor.execute("""
        SELECT Color, Size, Weight, Material, Warranty, Manufacturer
        FROM ProductAttributesWithSet
        WHERE ProductID = %(id)s
    """, {"id": product_id})
    
    row = cursor.fetchone()
    if row is None:
        raise ValueError(f"Product {product_id} not found")
    
    # Merge updates
    current = {
        "Color": row.Color,
        "Size": row.Size,
        "Weight": row.Weight,
        "Material": row.Material,
        "Warranty": row.Warranty,
        "Manufacturer": row.Manufacturer
    }
    
    for key, value in updates.items():
        current[key] = value
    
    # Build XML with non-null values
    xml_parts = []
    for key, value in current.items():
        if value is not None:
            xml_parts.append(f"<{key}>{value}</{key}>")
    
    cursor.execute("""
        UPDATE ProductAttributesWithSet
        SET SparseAttributes = %(xml)s
        WHERE ProductID = %(id)s
    """, {"id": product_id, "xml": "".join(xml_parts) or None})

# Usage
update_attributes(cursor, 3, {"Color": "Brown", "Warranty": 36})
conn.commit()
```

## Dynamic column patterns

Build flexible queries that search across sparse columns dynamically, using allowlists to validate column names and prevent SQL injection attacks.

### Query EAV-style data

Search for products by a specific attribute name and value using Entity-Attribute-Value (EAV) pattern matching.

```python
def find_products_by_attribute(cursor, attribute_name: str, attribute_value: str) -> list:
    """Find products with specific attribute value."""
    # Validate column name against allowed sparse columns to prevent SQL injection
    allowed_columns = {"Color", "Size", "Weight", "Material", "Warranty", "Manufacturer"}
    if attribute_name not in allowed_columns:
        raise ValueError(f"Invalid attribute: {attribute_name}")
    
    cursor.execute(f"""
        SELECT ProductID, ProductName, {attribute_name}
        FROM ProductAttributesWithSet
        WHERE {attribute_name} = %(value)s
    """, {"value": attribute_value})
    
    return cursor.fetchall()

# Find all blue products
blue_products = find_products_by_attribute(cursor, "Color", "Blue")
```

### Flexible attribute search

Search for products matching multiple optional attributes at once.

```python
def search_by_attributes(cursor, **attributes) -> list:
    """Search products by multiple optional attributes."""
    # Validate column names against allowed sparse columns to prevent SQL injection
    allowed_columns = {"Color", "Size", "Weight", "Material", "Warranty", "Manufacturer"}
    invalid = set(attributes.keys()) - allowed_columns
    if invalid:
        raise ValueError(f"Invalid attributes: {invalid}")
    
    conditions = ["1=1"]  # Always true base condition
    params = {}
    
    for i, (key, value) in enumerate(attributes.items()):
        if value is not None:
            conditions.append(f"{key} = %(attr_{i})s")
            params[f"attr_{i}"] = value
    
    query = f"""
        SELECT ProductID, ProductName, SparseAttributes
        FROM ProductAttributesWithSet
        WHERE {' AND '.join(conditions)}
    """
    
    cursor.execute(query, params)
    return cursor.fetchall()

# Search by multiple attributes
results = search_by_attributes(cursor, Color="Black", Material="Nylon")
```

## Performance considerations

Evaluate when sparse columns provide storage benefits, assess overhead costs, and use performance monitoring to optimize your sparse column design.

### When to use sparse columns

Good candidates for sparse columns include:

- Columns with more than 60-70% NULL values.
- Wide tables with many optional columns.
- Workloads where storage optimization is a priority.

Avoid using sparse columns when:

- Most rows have values (each non-NULL value adds 4 bytes of overhead).
- The column is frequently used in WHERE clauses.
- The column is part of the clustered index.

### Check storage savings

Compare the storage size of sparse and non-sparse versions of the same table.

```sql
-- Compare storage with and without sparse
EXEC sp_spaceused 'ProductAttributes';
EXEC sp_spaceused 'ProductAttributesWithoutSparse';
```

### Index considerations

You can index sparse columns. Filtered indexes work well for sparse data because they skip NULL rows:

```sql
CREATE INDEX IX_Products_Color
ON ProductAttributes(Color)
WHERE Color IS NOT NULL;
```

## Bulk operations

Optimize inserts of multiple products with sparse columns by building column set XML in Python before passing rows to `bulkcopy()`.

### Bulk insert with sparse columns

Use bulk copy to efficiently insert multiple products with sparse attributes.

```python
def bulk_insert_with_attributes(conn, products: list[dict]):
    """Bulk insert products with sparse attributes."""
    rows = []
    for product in products:
        attrs = product.get("attributes", {})
        xml = "".join(f"<{k}>{v}</{k}>" for k, v in attrs.items()) or None
        rows.append((product["id"], product["name"], xml))
    
    cursor = conn.cursor()
    result = cursor.bulkcopy("ProductAttributesWithSet", rows)
    return result["rows_copied"]

# Usage
products = [
    {"id": 100, "name": "Widget A", "attributes": {"Color": "Red", "Size": "Small"}},
    {"id": 101, "name": "Widget B", "attributes": {"Weight": 1.5, "Material": "Steel"}},
    {"id": 102, "name": "Widget C", "attributes": {}},  # No sparse attributes
]
bulk_insert_with_attributes(conn, products)
conn.commit()
```

## Best practices

Follow validation patterns, use column sets for flexibility, and monitor column sparseness to ensure your sparse column design meets performance and maintainability goals.

### Validate sparse column values

Validate that all sparse attributes match the allowed set before inserting data.

```python
# Sparse columns have the same constraints as regular columns
# The SPARSE keyword only affects storage

def validate_and_insert(cursor, product_id: int, name: str, attributes: dict):
    """Insert with validation."""
    allowed_attributes = {"Color", "Size", "Weight", "Material", "Warranty", "Manufacturer"}
    
    invalid = set(attributes.keys()) - allowed_attributes
    if invalid:
        raise ValueError(f"Unknown attributes: {invalid}")
    
    xml_parts = [f"<{k}>{v}</{k}>" for k, v in attributes.items()]
    
    cursor.execute("""
        INSERT INTO ProductAttributesWithSet (ProductID, ProductName, SparseAttributes)
        VALUES (%(id)s, %(name)s, %(xml)s)
    """, {"id": product_id, "name": name, "xml": "".join(xml_parts) or None})
```

### Use column sets for flexibility

Column sets simplify working with sparse columns:

- Add new sparse columns without code changes.
- Store dynamic attributes.
- Automatically serialize and deserialize XML.

Without a column set, you need explicit column lists. With a column set, the XML column handles dynamic attributes automatically.

### Monitor NULL percentages

Analyze the NULL percentage for a column to determine if it's a good candidate for sparse column optimization.

```python
def analyze_sparseness(cursor, table: str, column: str) -> float:
    """Check if column is a good sparse candidate."""
    # Validate identifiers to prevent SQL injection
    import re
    if not re.match(r'^[A-Za-z_][A-Za-z0-9_.]*$', table):
        raise ValueError(f"Invalid table name: {table}")
    if not re.match(r'^[A-Za-z_][A-Za-z0-9_]*$', column):
        raise ValueError(f"Invalid column name: {column}")

    cursor.execute(f"""
        SELECT 
            COUNT(*) AS TotalRows,
            SUM(CASE WHEN {column} IS NULL THEN 1 ELSE 0 END) AS NullRows
        FROM {table}
    """)
    
    row = cursor.fetchone()
    null_percentage = (row.NullRows / row.TotalRows * 100) if row.TotalRows > 0 else 0
    
    print(f"Column {column}: {null_percentage:.1f}% NULL")
    print(f"Recommendation: {'Good sparse candidate' if null_percentage > 60 else 'Keep as regular column'}")
    
    return null_percentage
```

## Related content

- [Data type mappings](data-type-mappings.md)
- [XML data](xml-data.md)
- [Bulk copy operations](bulk-copy.md)
- [Microsoft SQL sparse columns](../../../relational-databases/tables/use-sparse-columns.md)
