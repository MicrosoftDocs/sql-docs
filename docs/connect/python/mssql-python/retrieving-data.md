---
title: Retrieve Data with mssql-python
description: Learn how to fetch query results using fetchone, fetchmany, fetchall, and row iteration with the mssql-python driver.
author: dlevy-msft-sql
ms.author: dlevy
ms.date: 07/01/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---

# Retrieve data with mssql-python

The mssql-python driver offers several fetch methods, row access patterns, and cursor navigation features for retrieving query results.

## Fetch methods

After executing a SELECT query, use fetch methods to retrieve results.

### fetchone()

Returns a single row or `None` if no more rows are available:

```python
cursor.execute("SELECT ProductID, Name, ListPrice FROM Production.Product")

row = cursor.fetchone()
while row:
    print(f"{row.ProductID}: {row.Name} - ${row.ListPrice}")
    row = cursor.fetchone()
```

### fetchmany()

Returns a list of rows. `cursor.arraysize` controls the default batch size (default: 1):

```python
cursor.execute("SELECT * FROM Production.Product")
cursor.arraysize = 100  # Fetch 100 rows at a time

while True:
    rows = cursor.fetchmany()
    if not rows:
        break
    for row in rows:
        print(row.Name)
```

You can also specify the size directly:

```python
rows = cursor.fetchmany(50)  # Fetch up to 50 rows
```

### fetchall()

Returns all remaining rows as a list:

```python
cursor.execute("SELECT * FROM Production.Product WHERE Color = 'Black'")
rows = cursor.fetchall()

print(f"Found {len(rows)} products")
for row in rows:
    print(row.Name)
```

### fetchval()

Returns the first column of the first row, which is useful for scalar queries.

```python
count = cursor.execute("SELECT COUNT(*) FROM Production.Product").fetchval()
print(f"Total products: {count}")

max_price = cursor.execute("SELECT MAX(ListPrice) FROM Production.Product").fetchval()
print(f"Highest price: ${max_price}")
```

## Row access patterns

The `Row` class supports multiple access patterns.

### Index access

Access columns by position (zero-based):

```python
cursor.execute("SELECT ProductID, Name, ListPrice FROM Production.Product WHERE ProductID < 5")
row = cursor.fetchone()

product_id = row[0]
name = row[1]
price = row[2]
```

### Attribute access

Access columns by name:

```python
cursor.execute("SELECT ProductID, Name, ListPrice FROM Production.Product WHERE ProductID < 5")
row = cursor.fetchone()

product_id = row.ProductID
name = row.Name
price = row.ListPrice
```

### Lowercase column names

Enable lowercase attribute names globally:

```python
import mssql_python

settings = mssql_python.get_settings()
settings.lowercase = True

cursor.execute("SELECT ProductID, Name FROM Production.Product WHERE ProductID < 5")
row = cursor.fetchone()
print(row.productid, row.name)  # Lowercase access
settings.lowercase = False  # Restore default
```

### Iteration

Rows support iteration over values:

```python
cursor.execute("SELECT ProductID, Name, ListPrice FROM Production.Product WHERE ProductID < 5")
row = cursor.fetchone()

for value in row:
    print(value)
```

## Cursor iteration

Iterate directly over the cursor to process rows:

```python
cursor.execute("SELECT * FROM Production.Product")

for row in cursor:
    print(row.Name)
```

This pattern is equivalent to calling `fetchone()` repeatedly.

## Column metadata

Access column information through `cursor.description`:

```python
cursor.execute("SELECT ProductID, Name, ListPrice FROM Production.Product WHERE ProductID < 5")

for col in cursor.description:
    name, type_code, display_size, internal_size, precision, scale, null_ok = col
    print(f"Column: {name}, Type: {type_code}, Nullable: {null_ok}")
```

## Row count

The `cursor.rowcount` attribute indicates:

- For SELECT: Returns -1 after `execute()` until fetching begins. Once you start fetching, it reflects the cumulative number of rows fetched so far.
- For INSERT/UPDATE/DELETE: Number of rows affected.

```python
cursor.execute("SELECT * FROM Production.Product")
print(f"Rows returned: {cursor.rowcount}")

cursor.execute("CREATE TABLE #PriceUpd (Name NVARCHAR(50), Price DECIMAL(10,2), CategoryID INT)")
cursor.execute("INSERT INTO #PriceUpd VALUES ('A',10,1),('B',20,1),('C',30,2)")
cursor.execute("UPDATE #PriceUpd SET Price = Price * 1.1 WHERE CategoryID = 1")
print(f"Rows updated: {cursor.rowcount}")
```

## Cursor navigation

### skip()

Skip rows without fetching them:

```python
cursor.execute("SELECT * FROM Production.Product ORDER BY ProductID")
cursor.skip(10)  # Skip first 10 rows
row = cursor.fetchone()  # Returns 11th row
```

### scroll()

Move the cursor position forward:

```python
cursor.execute("SELECT * FROM Production.Product ORDER BY ProductID")

# Move forward 5 rows from current position
cursor.scroll(5, mode='relative')

row = cursor.fetchone()
```

> [!NOTE]
> The driver supports only `mode='relative'` with positive values. Absolute positioning and backward scrolling raise `NotSupportedError` because the driver uses forward-only cursors.

### rownumber

Track the current position:

```python
cursor.execute("SELECT * FROM Production.Product")

print(f"Initial position: {cursor.rownumber}")  # -1 (before first fetch)

row = cursor.fetchone()
print(f"After fetchone: {cursor.rownumber}")    # 0 (first row fetched)
```

## Multiple result sets

Use `nextset()` to process multiple result sets:

```python
cursor.execute("""
    SELECT * FROM Production.Product WHERE Color = 'Black';
    SELECT * FROM Production.ProductCategory;
    SELECT COUNT(*) FROM Production.Product;
""")

# First result set
products = cursor.fetchall()
print(f"Products: {len(products)}")

# Move to second result set
if cursor.nextset():
    categories = cursor.fetchall()
    print(f"Categories: {len(categories)}")

# Move to third result set
if cursor.nextset():
    count = cursor.fetchval()
    print(f"Total count: {count}")
```

## Large result sets

For large result sets, process rows in batches to manage memory:

```python
def process_batch(rows):
    # Example: print each row. Replace with your own logic.
    for row in rows:
        print(row)

cursor.execute("SELECT * FROM LargeTable")
cursor.arraysize = 1000

while True:
    rows = cursor.fetchmany()
    if not rows:
        break

    process_batch(rows)
    print(f"Processed {cursor.rownumber} rows so far")
```

## Context managers

Use context managers for automatic resource cleanup:

```python
with mssql_python.connect(connection_string) as conn:
    with conn.cursor() as cursor:
        cursor.execute("SELECT * FROM Production.Product")
        for row in cursor:
            print(row.Name)
# Cursor and connection closed automatically
```

## Best practices

- **Use `fetchmany()` for large results** to avoid loading everything into memory.
- **Close cursors** when done to release server resources.
- **Use column names** (attribute access) for more readable code.
- **Check `rowcount`** after data modification statements.
- **Handle `None` values** explicitly when columns are nullable.

## Related content

- [Execute queries](executing-queries.md)
- [Data type mappings](data-type-mappings.md)
- [Custom type converters](custom-type-converters.md)
- [Cursor management](cursor-management.md)
