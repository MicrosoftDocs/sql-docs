---
title: Data Type Mappings for mssql-python
description: Reference for how Python data types map to SQL Server types when using the mssql-python driver.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, randolphwest
ms.date: 08/28/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
ai-usage: ai-assisted
---

# Data type mappings for mssql-python

The mssql-python driver automatically maps Python data types to SQL Server types when sending parameters and converts SQL Server types to Python types when retrieving results.

## Python to SQL Server mappings

When you pass Python values as parameters to `execute()` or `executemany()`, the driver automatically selects the appropriate SQL Server type. For most applications, the automatic mapping is correct. Use `setinputsizes()` (described later in this article) only when you need to override the default, for example to force **varchar** instead of **nvarchar** or to control parameter metadata for string and integer inputs.

| Python type | SQL Server type | Notes |
| ------------- | ----------------- | ------- |
| `None` | `NULL` | SQL NULL value. |
| `bool` | **bit** | `True`→1, `False`→0. |
| `int` | **tinyint**, **smallint**, **int**, **bigint** | Size selected based on value range. |
| `float` | **float** | 64-bit floating point. |
| `decimal.Decimal` | **decimal**, **numeric** | Preserves precision up to 38 digits. |
| `str` | **varchar**, **nvarchar** | Uses **nvarchar** for Unicode content. |
| `bytes`, `bytearray` | **varbinary** | Binary data. |
| `datetime.date` | **date** | Date only. |
| `datetime.time` | **time** | Time only. |
| `datetime.datetime` | **datetime2** | Date and time with nanosecond precision. |
| `uuid.UUID` | **uniqueidentifier** | 16-byte GUID. |

### Integer type selection

The driver automatically selects the smallest integer type that can hold the value:

| Value range | SQL Server type |
| ------------- | ----------------- |
| 0 to 255 | **tinyint** |
| -32,768 to 32,767 | **smallint** |
| -2,147,483,648 to 2,147,483,647 | **int** |
| Larger values | **bigint** |

### Large value types

For strings and binary data exceeding standard limits, the driver automatically uses MAX types:

| Condition | SQL Server type |
| ----------- | ----------------- |
| String > 8,000 bytes (varchar) | **varchar(max)** |
| String > 4,000 chars (nvarchar) | **nvarchar(max)** |
| Binary > 8,000 bytes | **varbinary(max)** |

The driver streams large values to the server to minimize memory usage.

## SQL Server to Python mappings

When the driver retrieves data from SQL Server, the driver converts values to Python types:

| SQL Server type | Python type | Notes |
| ----------------- | ------------- | ------- |
| **bit** | `bool` | True/False. |
| **tinyint**, **smallint**, **int**, **bigint** | `int` | Python integer (arbitrary precision). |
| **real** | `float` | 32-bit floating point. |
| **float** | `float` | 64-bit floating point. |
| **decimal**, **numeric** | `decimal.Decimal` | Preserves precision. |
| **money**, **smallmoney** | `decimal.Decimal` | Fixed precision (four decimal places). |
| **char**, **varchar**, **text** | `str` | Decoded as UTF-8. |
| **nchar**, **nvarchar**, **ntext** | `str` | Decoded as UTF-16LE. |
| **binary**, **varbinary**, **image** | `bytes` | Raw binary. |
| **date** | `datetime.date` | Date only. |
| **time** | `datetime.time` | Time with microsecond precision. |
| **datetime**, **smalldatetime** | `datetime.datetime` | Legacy datetime types. |
| **datetime2** | `datetime.datetime` | High precision datetime. |
| **datetimeoffset** | `datetime.datetime` | Time zone-aware datetime. |
| **uniqueidentifier** | `uuid.UUID` | Python UUID objects. |
| **xml** | `str` | XML as text. |
| **geography**, **geometry** | `bytes` | Spatial types as binary. |
| **hierarchyid** | `bytes` | Hierarchy data as binary. |
| **sql_variant** | Varies | Resolved to the underlying base type. `sql_variant` columns use a streaming fetch path, which might have a slight performance impact compared to fixed-type columns. |
| `NULL` | `None` | Python None. |

## SQL type constants

The driver exports constants that correspond to ODBC SQL type identifiers. You typically use these constants with `setinputsizes()` to override the driver's default type inference when the automatic mapping doesn't match your schema.

### Character types

| Constant | Value | Description |
| ---------- | ------- | ------------- |
| `SQL_CHAR` | 1 | Fixed-length ANSI character. |
| `SQL_VARCHAR` | 12 | Variable-length ANSI character. |
| `SQL_LONGVARCHAR` | -1 | Long variable-length ANSI. |
| `SQL_WCHAR` | -8 | Fixed-length Unicode. |
| `SQL_WVARCHAR` | -9 | Variable-length Unicode. |
| `SQL_WLONGVARCHAR` | -10 | Long variable-length Unicode. |

### Numeric types

| Constant | Value | Description |
| ---------- | ------- | ------------- |
| `SQL_BIT` | -7 | Bit/boolean. |
| `SQL_TINYINT` | -6 | 8-bit unsigned integer. |
| `SQL_SMALLINT` | 5 | 16-bit signed integer. |
| `SQL_INTEGER` | 4 | 32-bit signed integer. |
| `SQL_BIGINT` | -5 | 64-bit signed integer. |
| `SQL_REAL` | 7 | 32-bit floating point. |
| `SQL_FLOAT` | 6 | 64-bit floating point. |
| `SQL_DOUBLE` | 8 | 64-bit floating point. |
| `SQL_DECIMAL` | 3 | Fixed precision decimal. |
| `SQL_NUMERIC` | 2 | Fixed precision numeric. |

### Date and time types

| Constant | Value | Description |
| ---------- | ------- | ------------- |
| `SQL_TYPE_DATE` | 91 | Date only. |
| `SQL_TYPE_TIME` | 92 | Time only. |
| `SQL_TYPE_TIMESTAMP` | 93 | Date and time. |
| `SQL_SS_TIME2` | -154 | SQL Server time(n). |
| `SQL_DATETIMEOFFSET` | -155 | SQL Server datetimeoffset. |

> [!NOTE]
> `SQL_SS_TIME2` and `SQL_DATETIMEOFFSET` are internal constants not exported as module-level attributes. Use the integer values (`-154`, `-155`) directly when calling `add_output_converter()`.

### Binary types

| Constant | Value | Description |
| ---------- | ------- | ------------- |
| `SQL_BINARY` | -2 | Fixed-length binary. |
| `SQL_VARBINARY` | -3 | Variable-length binary. |
| `SQL_LONGVARBINARY` | -4 | Long variable-length binary. |

### Other types

| Constant | Value | Description |
| ---------- | ------- | ------------- |
| `SQL_GUID` | -11 | Uniqueidentifier. |
| `SQL_XML` | -152 | XML data. |
| `SQL_SS_UDT` | -151 | User-defined type (spatial). |
| `SQL_SS_VARIANT` | -150 | sql_variant data. |

> [!NOTE]
> `SQL_SS_UDT` and `SQL_SS_VARIANT` are internal constants not exported as module-level attributes. Use the integer values (`-151`, `-150`) directly when calling `add_output_converter()`.

## Use setinputsizes()

Call `setinputsizes()` before `executemany()` to explicitly declare parameter types when the driver's automatic type inference causes unexpected behavior, for example when a column is `varchar(100)` but the driver sends `nvarchar`:

> [!NOTE]
> For decimal values, rely on the driver's automatic type inference rather than `SQL_DECIMAL` or `SQL_NUMERIC`. Those explicit decimal type constants currently have a known issue with `setinputsizes()`. For more information, see [Execute queries](executing-queries.md).

```python
import mssql_python

conn = mssql_python.connect(connection_string)
cursor = conn.cursor()

# Declare types: (sql_type, precision, scale)
cursor.setinputsizes([
    (mssql_python.SQL_WVARCHAR, 100, 0),  # nvarchar(100)
    (mssql_python.SQL_INTEGER, 0, 0),     # int
])

cursor.executemany(
    "SELECT ProductID, Name FROM Production.Product WHERE Name LIKE ? AND ProductSubcategoryID = ?",
    [
        ("Road%", 2),
        ("Mountain%", 1),
    ]
)
```

## Decimal separator

The driver uses locale-aware decimal separators. To customize:

```python
import mssql_python

# Get current separator
sep = mssql_python.getDecimalSeparator()
print(f"Current separator: {sep}")  # Usually "."

# Set custom separator (for locales using comma)
mssql_python.setDecimalSeparator(",")
```

## Custom type converters

Register custom converters for specific SQL types:

```python
import mssql_python
from decimal import Decimal

def money_to_float(value):
    """Convert money values to float instead of Decimal."""
    if value is None:
        return None
    return float(value)

conn.add_output_converter(Decimal, money_to_float)
```

For more information, see [Custom type converters](custom-type-converters.md).

## Handle special types

### UUID handling

The driver maps `uuid.UUID` to `uniqueidentifier` automatically. You can insert and retrieve UUIDs without manual string conversion:

```python
import uuid

cursor.execute("CREATE TABLE #Users (UserId UNIQUEIDENTIFIER PRIMARY KEY, Name NVARCHAR(100))")

# Insert a generated UUID
user_id = uuid.uuid4()
cursor.execute("INSERT INTO #Users (UserId, Name) VALUES (%(user_id)s, %(name)s)", {"user_id": user_id, "name": "Alice"})
conn.commit()

# Retrieve as uuid.UUID object (default behavior)
cursor.execute("SELECT UserId, Name FROM #Users WHERE Name = %(name)s", {"name": "Alice"})
row = cursor.fetchone()
print(f"Type: {type(row.UserId)}")  # <class 'uuid.UUID'>
print(f"UUID: {row.UserId}")        # e.g., 3b4c8f2a-...

# Use the returned UUID directly in subsequent queries
cursor.execute("SELECT Name FROM #Users WHERE UserId = %(user_id)s", {"user_id": row.UserId})
```

By default, the driver returns `UNIQUEIDENTIFIER` columns as `uuid.UUID` objects. To return pyodbc-compatible uppercase strings instead, set `native_uuid=False`:

```python
import uuid

# Per-connection: return UUIDs as strings
conn2 = mssql_python.connect(connection_string, native_uuid=False)
cursor2 = conn2.cursor()
cursor2.execute("CREATE TABLE #UuidDemo (Id UNIQUEIDENTIFIER DEFAULT NEWID(), Label NVARCHAR(50))")
cursor2.execute("INSERT INTO #UuidDemo (Label) VALUES (%(label)s)", {"label": "test"})
conn2.commit()

cursor2.execute("SELECT Id FROM #UuidDemo")
row = cursor2.fetchone()
print(type(row[0]))  # <class 'str'>
print(row[0])        # e.g., 3B4C8F2A-...
conn2.close()
```

For module-level configuration, see the `native_uuid` setting in [Module configuration](module-configuration.md#native_uuid).

### Datetime with timezone

`datetimeoffset` columns return time zone-aware `datetime` objects. For guidance on working with offset-aware versus offset-naive values, see [Datetime handling](datetime-handling.md).

```python
cursor.execute("SELECT SYSDATETIMEOFFSET()")
row = cursor.fetchone()
dt = row[0]

print(f"DateTime: {dt}")
print(f"Timezone: {dt.tzinfo}")
```

### Spatial data

The driver returns spatial types (`geography`, `geometry`) as `bytes`. You can use parameter strings in WKT format:

```python
# Insert using WKT
cursor.execute("CREATE TABLE #Locations (Name NVARCHAR(100), Geo GEOGRAPHY)")
cursor.execute(
    "INSERT INTO #Locations (Name, Geo) VALUES (%(name)s, geography::STGeomFromText(%(wkt)s, 4326))",
    {"name": "Seattle", "wkt": "POINT(-122.33 47.60)"}
)

# Retrieve as bytes
cursor.execute("SELECT Geo.STAsBinary() FROM #Locations")
row = cursor.fetchone()
geo_bytes = row[0]
```

### rowversion/timestamp

SQL Server's `rowversion` (formerly `timestamp`) is a binary type, not a datetime:

```python
cursor.execute("""
    CREATE TABLE #RowVersionDemo (
        ID int PRIMARY KEY,
        Version rowversion
    )
""")
cursor.execute("INSERT INTO #RowVersionDemo (ID) VALUES (1)")
cursor.execute("SELECT Version FROM #RowVersionDemo WHERE ID = 1")
row = cursor.fetchone()
version = row[0]  # bytes, not datetime
```

## Stream large value types

When inserting or retrieving `varchar(max)`, `nvarchar(max)`, or `varbinary(max)` data, the driver uses Data-at-Execution (DAE) streaming to transmit data in chunks rather than loading the entire value into memory at once.

Streaming activates automatically when input data exceeds size thresholds:

| Data type | Streaming threshold |
| ----------- | ------------------- |
| **varchar(max)** | > 8,000 bytes |
| **nvarchar(max)** | > 4,000 characters |
| **varbinary(max)** | > 8,000 bytes |

Streaming works with `execute()`, `executemany()`, and all fetch APIs (`fetchone()`, `fetchmany()`, `fetchall()`). The driver requires no special configuration and handles streaming automatically for large values.

## Unsupported SQL Server types

The following SQL Server types don't have native Python type mappings.

| SQL Server type | Status |
| ----------------- | -------- |
| **json** | Not supported |
| **vector** | Not supported |
| **table** | Not supported (table-valued parameters) |

> [!TIP]
> Although the `json` SQL Server type isn't supported directly, you can store JSON data in `nvarchar(max)` columns and query it with SQL Server JSON functions (`JSON_VALUE`, `JSON_QUERY`, `OPENJSON`). For patterns and examples, see [JSON data](json-data.md).

The following types return data as `bytes` but don't have native Python type mappings:

| SQL Server type | Python type | Notes |
| ----------------- | ------------- | ------- |
| **geography** | `bytes` | Use `.STAsBinary()` for WKB format. |
| **geometry** | `bytes` | Use `.STAsBinary()` for WKB format. |
| **hierarchyid** | `bytes` | Binary representation. |
| **sql_variant** | Varies | Resolved to the underlying base type. |

## Decimal versus float precision

Use `decimal.Decimal` when exact precision matters, such as financial calculations, currency, or any value where rounding errors are unacceptable. Use `float` when approximate values are acceptable, such as scientific measurements or sensor readings.

The driver maps `decimal.Decimal` to SQL Server `decimal`/`numeric` and preserves precision up to 38 digits. `float` maps to SQL Server `float` (64-bit IEEE 754), which can introduce rounding artifacts:

```python
from decimal import Decimal

# Exact - use for currency and financial data
price = Decimal("19.99")
tax_rate = Decimal("0.0825")
total = price * (1 + tax_rate)  # Decimal("21.6391750")

cursor.execute("""
    CREATE TABLE #PrecisionDemo (
        Description NVARCHAR(100),
        DiscountPct DECIMAL(5,2),
        Rating FLOAT
    )
""")

cursor.execute(
    "INSERT INTO #PrecisionDemo (Description, DiscountPct, Rating) VALUES (%(desc)s, %(pct)s, %(rating)s)",
    {"desc": "Summer sale", "pct": Decimal("0.15"), "rating": 4.5}
)

cursor.execute("SELECT DiscountPct, Rating FROM #PrecisionDemo WHERE Description = %(desc)s", {"desc": "Summer sale"})
row = cursor.fetchone()
print(type(row.DiscountPct))  # <class 'decimal.Decimal'>
print(type(row.Rating))       # <class 'float'>
```

When retrieving `decimal`/`numeric` columns, the driver always returns `decimal.Decimal` objects. When retrieving `float`/`real` columns, the driver returns Python `float`.

> [!WARNING]
> Don't compare `float` values for equality. Use a tolerance range instead: `abs(a - b) < 0.0001`.

## Numpy type binding

The `mssql-python` driver uses `isinstance()` checks to determine SQL types for parameters. Numpy integer types (`numpy.int64`, `numpy.int32`, `numpy.int8`) don't pass `isinstance(x, int)` in NumPy 2.x, which causes binding failures. Only `numpy.float64` works directly because it's a subclass of Python `float`.

Convert numpy values to native Python types before passing them as parameters:

```python
import numpy as np

row_id = np.int64(42)
score = np.float32(3.14)

# This fails: numpy.int64 is not recognized as int
# cursor.execute("SELECT * FROM Products WHERE ID = ?", (row_id,))

# Convert to native Python types first
cursor.execute(
    "SELECT * FROM Production.Product WHERE ProductID = %(product_id)s",
    {"product_id": int(row_id)}
)

# For DataFrames, convert the whole column
import pandas as pd

df = pd.DataFrame({"ProductID": [1, 2, 3], "Quantity": [10, 20, 30]})

cursor.execute("CREATE TABLE #Orders (ProductID INT, Quantity INT)")

for _, row in df.iterrows():
    cursor.execute(
        "INSERT INTO #Orders (ProductID, Quantity) VALUES (%(product_id)s, %(quantity)s)",
        {"product_id": int(row["ProductID"]), "quantity": int(row["Quantity"])}
    )
```

If you work heavily with pandas or numpy data, consider using the [Arrow integration](arrow-integration.md) or [pandas integration](pandas-integration.md) paths, which handle type conversion internally.

## Related content

- [Execute queries](executing-queries.md)
- [Retrieve data](retrieving-data.md)
- [Custom type converters](custom-type-converters.md)
