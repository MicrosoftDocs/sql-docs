---
title: Custom Type Converters with mssql-python
description: Learn how to register custom functions to convert SQL data types to Python objects using the mssql-python driver.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, randolphwest
ms.date: 08/27/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---

# Custom type converters with mssql-python

By default, the mssql-python driver converts Microsoft SQL data types to appropriate Python types (see [Data type mappings](data-type-mappings.md)). The output converter system lets you override this behavior for specific SQL types, enabling:

- Custom type transformations (for example, money to float instead of Decimal)
- Integration with third-party libraries (for example, spatial data to Shapely objects)
- Business-specific formatting (for example, dates to custom string formats)

## Register output converters

Use `add_output_converter()` to register a converter function. The key is either a Python type that matches the `type_code` in `cursor.description`, or an integer ODBC SQL type code such as `mssql_python.SQL_DECIMAL`:

```python
import mssql_python
from datetime import datetime

def my_datetime_converter(value):
    """Convert datetime to a custom display string."""
    if value is None:
        return None
    return value.strftime("%B %d, %Y at %I:%M %p")

conn = mssql_python.connect(connection_string)
conn.add_output_converter(datetime, my_datetime_converter)

cursor = conn.cursor()
cursor.execute("SELECT CAST('2026-07-16T10:30:00' AS DATETIME2)")
print(cursor.fetchval())  # July 16, 2026 at 10:30 AM
```

### Choose between a Python type key and a SQL type code key

The two key forms differ in how precisely they select columns:

- A **Python type** key applies to every SQL type that maps to that Python type. Registering a converter for `Decimal` transforms **decimal**, **numeric**, **money**, and **smallmoney** columns.

- An **integer SQL type code** key matches the ODBC type code that the column reports, which is narrower but isn't always a single SQL type. `SQL_NUMERIC` transforms only **numeric** columns. `SQL_DECIMAL` transforms **decimal**, **money**, and **smallmoney** columns, because all three report that same code.

Use an integer key when a Python type key is broader than you want, or when you're porting code from pyodbc, which uses integer keys. Use a Python type key when you want one converter to cover a whole family of SQL types.

```python
import mssql_python

def tag_decimal(value):
    return f"D:{value}"

conn.add_output_converter(mssql_python.SQL_DECIMAL, tag_decimal)

cursor.execute("""
    SELECT CAST(12.34 AS decimal(10,2)),
           CAST(56.78 AS numeric(10,2)),
           CAST(90.12 AS money)
""")
print(cursor.fetchone())  # ('D:12.34', Decimal('56.78'), 'D:90.1200')
```

### Converter resolution order

For each column, the driver selects at most one converter, in this order:

1. The converter registered for the column's integer ODBC SQL type code.

1. The converter registered for the Python type in `cursor.description`.

1. The converter registered for `SQL_WVARCHAR`, but only when the column's Python type is `str` or `bytes`.

### Converter function signature

When you register a converter with a Python type key, the type you pass to `add_output_converter()` must match one of the following Python types. The **Converter receives** column shows what the driver passes to your function:

| `cursor.description` type_code | Converter receives | Microsoft SQL types |
| --- | --- | --- |
| `int` | `int` | `tinyint`, `smallint`, `int`, `bigint` |
| `float` | `float` | `real`, `float` |
| `decimal.Decimal` | `Decimal` | `decimal`, `numeric`, `money`, `smallmoney` |
| `str` | `bytes` (UTF-16LE encoded) | `char`, `varchar`, `nchar`, `nvarchar`, `xml` |
| `datetime.datetime` | `datetime` | `datetime`, `datetime2`, `smalldatetime` |
| `datetime.date` | `date` | `date` |
| `bytes` | `bytes` | `varbinary`, `binary`, `geography`, `geometry` |
| `bool` | `bool` | `bit` |

> [!NOTE]
> String-type converters (`str` key) receive raw `bytes` in UTF-16LE encoding, not decoded Python strings. All other types receive the already converted Python object.

```python
from decimal import Decimal

def decimal_converter(value: Decimal | None) -> float | None:
    if value is None:
        return None
    # value is a Decimal object for numeric/money types
    return float(value)
```

## Manage converters

Use these methods to inspect or remove converters already registered on a connection.

### Get existing converter

Retrieve the converter function currently registered for a type:

```python
converter = conn.get_output_converter(str)
if converter:
    print(f"Converter registered: {converter}")
else:
    print("Using default conversion")
```

### Remove a converter

Unregister a converter so the driver reverts to default conversion for that type:

```python
conn.remove_output_converter(str)
# str columns now use default conversion
```

### Clear all converters

Reset all registered converters to use Microsoft SQL's default type mappings:

```python
conn.clear_output_converters()
# All types now use default conversion
```

## Common converter patterns

These examples show the most frequently needed converter functions.

### Convert VARCHAR to uppercase

String-type converters receive raw bytes in UTF-16LE encoding:

```python
def uppercase_converter(value):
    if value is None:
        return None
    return value.decode('utf-16-le').upper()

conn.add_output_converter(str, uppercase_converter)

cursor = conn.cursor()
cursor.execute("SELECT 'hello world'")
print(cursor.fetchval())  # HELLO WORLD
```

### Convert money to float

By default, the driver returns DECIMAL/NUMERIC types as `decimal.Decimal`. Convert to float:

```python
from decimal import Decimal

def money_to_float(value):
    if value is None:
        return None
    return float(value)

conn.add_output_converter(Decimal, money_to_float)

cursor = conn.cursor()
cursor.execute("SELECT CAST(19.99 AS MONEY)")
result = cursor.fetchval()
print(type(result))  # <class 'float'>
```

### Parse JSON data

Microsoft SQL Server can store JSON as text. Automatically parse to Python objects:

```python
import json

def json_converter(value):
    if value is None:
        return None
    text = value.decode('utf-16-le')
    try:
        return json.loads(text)
    except json.JSONDecodeError:
        return text  # Return as string if not valid JSON

conn.add_output_converter(str, json_converter)

cursor = conn.cursor()
cursor.execute("SELECT '{\"name\": \"Widget\", \"price\": 19.99}'")
data = cursor.fetchval()
print(data['name'])  # Widget
```

> [!CAUTION]
> This example converts ALL string values (nvarchar, varchar, xml). JSON parsing is attempted on every string column. In practice, apply JSON parsing selectively instead of as a blanket converter.

### Custom datetime formatting

Convert datetime to ISO string format:

```python
from datetime import datetime

def datetime_to_iso(value):
    if value is None:
        return None
    return value.isoformat()

conn.add_output_converter(datetime, datetime_to_iso)

cursor = conn.cursor()
cursor.execute("SELECT TOP 1 ModifiedDate FROM Production.Product")
print(cursor.fetchval())  # 2014-02-08T10:01:36.827000
```

## Security considerations

> [!CAUTION]
> When you register an output converter, the function you provide runs on **every matching database value**.
>
> - Only register converters from trusted code.
> - Never accept converter functions from user input.
> - Malicious converters could execute arbitrary code or leak data.

```python
# DANGEROUS - never do this
def unsafe_example(user_converter_code):
    converter_func = eval(user_converter_code)  # Security risk!
    conn.add_output_converter(str, converter_func)
```

## Example: Spatial data integration

Integrate spatial data with the Shapely library:

```python
from shapely import wkb

def geometry_converter(value):
    """Convert WKB binary to Shapely geometry object."""
    if value is None:
        return None
    return wkb.loads(value)

conn.add_output_converter(bytes, geometry_converter)

# Query must use STAsBinary() to get standard WKB format
cursor = conn.cursor()
cursor.execute(
    "SELECT SpatialLocation.STAsBinary() FROM Person.Address "
    "WHERE SpatialLocation IS NOT NULL AND City = 'Seattle'"
)
for row in cursor:
    shape = row[0]
    print(f"Point: ({shape.x:.4f}, {shape.y:.4f})")
```

> [!NOTE]
> Registering a `bytes` converter affects ALL binary columns (varbinary, geography, geometry). Use `clear_output_converters()` after spatial queries if you also query nonspatial binary data.

## Example: Custom Row formatting

Create a data class converter:

```python
from dataclasses import dataclass

@dataclass
class Product:
    id: int
    name: str
    price: float

def fetch_products_as_dataclass(cursor):
    """Convert rows to Product dataclass instances."""
    cursor.execute(
        "SELECT ProductID, Name, ListPrice FROM Production.Product "
        "WHERE ListPrice > 0"
    )
    results = []
    for row in cursor:
        results.append(Product(
            id=row.ProductID,
            name=row.Name,
            price=float(row.ListPrice)
        ))
    return results

products = fetch_products_as_dataclass(cursor)
for p in products[:5]:
    print(f"{p.name}: ${p.price:.2f}")
```

## Converter scope and lifetime

- **Connection-scoped**: The driver registers converters per connection, not globally.
- **Persistent**: Converters remain active until you remove them or close the connection.
- **Not inherited**: New connections don't inherit converters from other connections.

```python
conn1 = mssql_python.connect(connection_string)
conn1.add_output_converter(str, uppercase_converter)

conn2 = mssql_python.connect(connection_string)
# conn2 does NOT have the converter registered
```

## Performance considerations

- The driver calls converters for **every value** of the registered type.
- Keep converter functions efficient.
- For high-volume queries, consider whether conversion is necessary.
- Profile performance if converters affect throughput.

```python
import time

def slow_converter(value):
    time.sleep(1)  # Avoid time consuming routines like this - adds 1 second for each value processed!
    return str(value) if value else None
```

## Related content

- [Data type mappings for mssql-python](data-type-mappings.md)
- [Retrieve data with mssql-python](retrieving-data.md)
- [Manage connections with mssql-python](connection-management.md)
