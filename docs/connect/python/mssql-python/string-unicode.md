---
title: Handle Strings and Unicode with mssql-python
description: Learn how to work with string data types, Unicode, and character encoding with the mssql-python driver for Microsoft SQL.
author: dlevy-msft-sql
ms.author: dlevy
ms.date: 07/16/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---

# Handle strings and Unicode

Microsoft SQL provides multiple string types that the mssql-python driver maps to Python `str` objects. The key decision is whether to use `varchar` (non-Unicode) or `nvarchar` (Unicode):

- Use `nvarchar` when your data might contain characters outside ASCII, such as names, addresses, or user-generated content in any language.
- Use `varchar` when data is strictly ASCII (codes, identifiers, email addresses) and you want to save storage. `varchar` uses 1 byte per character; `nvarchar` uses 2 bytes per character.

| SQL Type | Unicode | Max Length | Python Type |
| ---------- | --------- | ------------ | ------------- |
| `char(n)` | No | 8,000 | `str` |
| `varchar(n)` | No | 8,000 | `str` |
| `varchar(max)` | No | 2 GB | `str` |
| `nchar(n)` | Yes | 4,000 | `str` |
| `nvarchar(n)` | Yes | 4,000 | `str` |
| `nvarchar(max)` | Yes | 2 GB | `str` |
| `text` | No | 2 GB (deprecated) | `str` |
| `ntext` | Yes | 2 GB (deprecated) | `str` |

## Basic string operations

The driver maps all Microsoft SQL string types to Python `str` objects.

### Insert and retrieve strings

Use parameterized queries to safely insert and fetch string data from the database.

```python
import mssql_python

conn = mssql_python.connect(connection_string)
cursor = conn.cursor()

# Create temp table for demo
cursor.execute("""
    CREATE TABLE #StringDemo (
        ID INT IDENTITY(1,1) PRIMARY KEY,
        Name NVARCHAR(100),
        Email NVARCHAR(200)
    )
""")

# Insert string data
cursor.execute(
    "INSERT INTO #StringDemo (Name, Email) VALUES (%(name)s, %(email)s)",
    {"name": "Alice Smith", "email": "alice@example.com"}
)
conn.commit()

# Retrieve string data
cursor.execute("SELECT Name, Email FROM #StringDemo WHERE ID = 1")
row = cursor.fetchone()
print(row.Name)   # 'Alice Smith'
print(row.Email)  # 'alice@example.com'
```

### Strings with special characters

Handle quotes, angle brackets, and other special characters in strings using parameterized queries.

```python
# Quotes and special characters handled automatically
cursor.execute("""
    CREATE TABLE #Notes (
        ID INT IDENTITY(1,1) PRIMARY KEY,
        Title NVARCHAR(200),
        Content NVARCHAR(MAX)
    )
""")
cursor.execute(
    "INSERT INTO #Notes (Title, Content) VALUES (%(title)s, %(content)s)",
    {
        "title": "O'Brien's Report",
        "content": 'Contains "quotes" and special chars: <>&'
    }
)
conn.commit()
```

## Unicode support

Use **nvarchar** columns and Python `str` to store and retrieve text in any language.

### Store Unicode text

Insert Unicode content by passing Python strings to parameterized queries; the driver encodes them as UTF-16LE for nvarchar columns.

```python
# International characters - use nvarchar columns
cursor.execute("""
    CREATE TABLE #Messages (
        ID INT IDENTITY(1,1) PRIMARY KEY,
        Content NVARCHAR(MAX)
    )
""")
cursor.execute("""
    INSERT INTO #Messages (Content) VALUES (%(msg)s)
""", {"msg": "Hello 你好 مرحبا שלום 🎉"})

cursor.execute("SELECT Content FROM #Messages WHERE ID = 1")
row = cursor.fetchone()
print(row.Content)  # 'Hello 你好 مرحبا שלום 🎉'
```

### Unicode in different scripts

Support multiple languages and scripts in a single table by using nvarchar columns and bulk inserts.

```python
messages = [
    {"lang": "English", "text": "Hello, World!"},
    {"lang": "Chinese", "text": "你好，世界！"},
    {"lang": "Japanese", "text": "こんにちは世界！"},
    {"lang": "Korean", "text": "안녕하세요, 세상!"},
    {"lang": "Arabic", "text": "مرحبا بالعالم!"},
    {"lang": "Hebrew", "text": "שלום עולם!"},
    {"lang": "Russian", "text": "Привет мир!"},
    {"lang": "Greek", "text": "Γειά σου Κόσμε!"},
    {"lang": "Emoji", "text": "👋🌍✨🎉"},
]

cursor.execute("""
    CREATE TABLE #Greetings (
        ID INT IDENTITY(1,1) PRIMARY KEY,
        Language NVARCHAR(50),
        Message NVARCHAR(200)
    )
""")
cursor.executemany("""
    INSERT INTO #Greetings (Language, Message) VALUES (%(lang)s, %(text)s)
""", messages)
conn.commit()
```

### Ensure nvarchar columns for Unicode

Always define columns as nvarchar instead of varchar when your data might contain non-ASCII characters.

```sql
-- For Unicode data, always use nvarchar, not varchar
CREATE TABLE #UnicodeDemo (
    ID INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(100),        -- Supports Unicode
    Description NVARCHAR(MAX)  -- Supports large Unicode text
);
```

## String length considerations

Choose between fixed-length and variable-length types based on how consistent your data lengths are.

### Fixed versus variable length

Microsoft SQL's `char(n)` pads values with trailing spaces to the declared length. This padding wastes storage for variable-length data but can improve performance for fixed-width columns, such as country codes. Use `varchar(n)` for most string columns.

The following example shows the difference in how padded versus non-padded columns handle data retrieval:

```python
# char(6) pads to fixed length
cursor.execute(
    "SELECT StateProvinceCode FROM Person.StateProvince WHERE StateProvinceID = 1"
)  # nchar(6) column
row = cursor.fetchone()
print(repr(row.StateProvinceCode))  # 'AB    ' - right-padded with spaces

# nvarchar stores actual length
cursor.execute(
    "SELECT Name FROM Person.StateProvince WHERE StateProvinceID = 1"
)  # nvarchar column
row = cursor.fetchone()
print(repr(row.Name))  # 'Alberta' - no padding
```

### Handle trailing spaces

When retrieving data from fixed-length char columns, use `rstrip()` to remove the padding spaces added by Microsoft SQL Server.

```python
# Strip trailing spaces from char columns
cursor.execute("SELECT ProductNumber FROM Production.Product")
for row in cursor:
    code = row.ProductNumber.rstrip()  # Remove trailing spaces
    print(f"Code: '{code}'")
```

### Large strings (MAX types)

The `nvarchar(max)` and `varchar(max)` types support strings up to 2 GB, ideal for storing large text documents, JSON, or XML content.

```python
# Large text content
large_content = "x" * 100000  # 100K characters

cursor.execute("""
    CREATE TABLE #Documents (
        ID INT IDENTITY(1,1) PRIMARY KEY,
        Content NVARCHAR(MAX)
    )
""")
cursor.execute("""
    INSERT INTO #Documents (Content) VALUES (%(content)s)
""", {"content": large_content})

cursor.execute("SELECT Content FROM #Documents WHERE ID = 1")
row = cursor.fetchone()
print(len(row.Content))  # 100000
```

## String comparison and collation

Microsoft SQL string comparison behavior depends on the collation set on the database or column.

### Case sensitivity

Microsoft SQL string comparison depends on collation. By default, most databases use case-insensitive collation, but you can override this with the `COLLATE` clause.

```python
# Case-insensitive collation (default for many databases)
cursor.execute("SELECT * FROM Person.Person WHERE LastName = %(name)s", {"name": "smith"})
# Might match 'Smith', 'SMITH', 'smith' depending on collation

# For case-sensitive comparison
cursor.execute("""
    SELECT * FROM Person.Person 
    WHERE LastName COLLATE Latin1_General_CS_AS = %(name)s
""", {"name": "Smith"})
```

### LIKE pattern matching

Use the `LIKE` operator with wildcard characters to search for string patterns; escape special characters with bracket notation to match literals.

```python
# Wildcard searches
search_term = "Road"
cursor.execute("""
    SELECT Name FROM Production.Product WHERE Name LIKE %(pattern)s
""", {"pattern": f"%{search_term}%"})

# Escape special characters in search
def escape_like(value: str) -> str:
    """Escape LIKE wildcards in search value."""
    return value.replace("[", "[[]").replace("%", "[%]").replace("_", "[_]")

search = "100%"
cursor.execute("""
    SELECT Name FROM Production.Product WHERE Name LIKE %(pattern)s
""", {"pattern": f"%{escape_like(search)}%"})
```

## Encoding considerations

Encoding behavior depends on the Microsoft SQL column type and the source collation.

### Encoding assumptions and Unicode defaults

The `mssql-python` driver handles encoding automatically based on the Microsoft SQL column type. By default, string parameters are sent as UTF-16LE for nvarchar columns and according to the database collation for varchar columns:

| Column type | Wire encoding | Python result |
| --- | --- | --- |
| `nvarchar`, `nchar`, `ntext` | UTF-16LE | `str` (decoded by driver) |
| `varchar`, `char`, `text` | Database or column collation encoding | `str` (decoded by the driver using the source encoding) |

Python strings are always Unicode internally. When you pass a `str` parameter, the driver encodes it for the target column type. By default, the driver sends string parameters as `nvarchar` (Unicode), which ensures characters are preserved regardless of the database collation. For `varchar` columns, UTF-8 applies only when the database or column uses a UTF-8-enabled collation.

If your column is `varchar` and you need to send non-Unicode data to match the column type exactly (for example, to avoid implicit conversion warnings), use `setinputsizes()` to override the default:

```python
import mssql_python

conn = mssql_python.connect(connection_string)
cursor = conn.cursor()

# Create temp table for demo
cursor.execute("CREATE TABLE #AsciiTable (Code VARCHAR(100))")

cursor.setinputsizes([(mssql_python.SQL_VARCHAR, 100, 0)])
cursor.execute(
    "INSERT INTO #AsciiTable (Code) VALUES (?)",
    ("ABC123",)
)
conn.commit()
```

For most applications, the default behavior is correct. Override only when you see implicit conversion warnings in query plans or need to match a specific `varchar` collation.

### Connection encoding

The mssql-python driver automatically handles encoding for the connection based on the Microsoft SQL Server version and configuration. Because Python strings are Unicode, the driver encodes them appropriately (UTF-8 or UTF-16) for the target data type. You don't need to configure connection encoding manually.

### VARCHAR columns with legacy collations

Databases with Windows-1252 (CP1252) collations, such as `Latin1_General_CI_AS`, store extended Latin characters (for example, `€`, `™`, and accented characters) in `varchar` columns using CP1252 encoding. The driver decodes these characters correctly on all platforms.

This difference matters for cross-platform deployments: the same `varchar` data that reads correctly on Windows also reads correctly on Linux, with no special configuration required.

```python
# Create a temp table with a varchar column and insert extended Latin characters
cursor.execute("CREATE TABLE #Products (Name VARCHAR(100))")
cursor.execute("INSERT INTO #Products (Name) VALUES (%(name)s)", {"name": "Café €100 ™"})
conn.commit()

# CP1252 characters in varchar columns are decoded correctly on all platforms
cursor.execute("SELECT Name FROM #Products WHERE Name LIKE '%€%'")
for row in cursor:
    print(row.Name)  # Correct on both Windows and Linux
```

If your schema allows it, migrating `varchar` columns to `nvarchar` avoids encoding ambiguity entirely and supports all Unicode characters.

### File encoding

When reading files to insert into the database, specify the appropriate encoding to preserve Unicode content.

```python
# Reading files with explicit encoding
def insert_file_content(cursor, conn, file_path: str, encoding: str = "utf-8"):
    with open(file_path, "r", encoding=encoding) as f:
        content = f.read()
    
    cursor.execute(
        "INSERT INTO #FileContent (Content) VALUES (%(content)s)",
        {"content": content}
    )
    conn.commit()
```

## Common string operations

These examples cover common string manipulation patterns in both Python and SQL.

### Concatenation

You can concatenate strings either in Python before inserting or using SQL's string operators on the server.

```python
# Concatenate in Python before insert
first_name = "Alice"
last_name = "Smith"
full_name = f"{first_name} {last_name}"

cursor.execute("""
    CREATE TABLE #ConcatDemo (
        ID INT IDENTITY(1,1) PRIMARY KEY,
        FullName NVARCHAR(200)
    )
""")
cursor.execute(
    "INSERT INTO #ConcatDemo (FullName) VALUES (%(name)s)",
    {"name": full_name}
)

# Or concatenate in SQL
cursor.execute("""
    SELECT FirstName + ' ' + LastName AS FullName FROM Person.Person
""")
```

### String formatting
Apply formatting in Python to display strings with currency, padding, or alignment before showing them to users.
```python
from decimal import Decimal

# Format for display
cursor.execute("SELECT Name, ListPrice FROM Production.Product WHERE ListPrice > 0")
for row in cursor.fetchall()[:5]:
    print(f"{row.Name}: ${row.ListPrice:.2f}")

# Pad strings
cursor.execute("SELECT ProductNumber FROM Production.Product")
for row in cursor.fetchall()[:5]:
    padded = row.ProductNumber.ljust(15)  # Left-justify, pad to 15 chars
    print(f"[{padded}]")
```

### NULL versus empty string

Microsoft SQL treats NULL and empty string (`''`) as different values. NULL means "unknown" while empty string means "known to be empty." Choose one convention for your application and be consistent. Most applications use NULL for missing optional fields.

The following example demonstrates how to distinguish between NULL and empty string:

```python
# NULL is different from empty string
cursor.execute("""
    CREATE TABLE #NullDemo (
        ID INT IDENTITY(1,1) PRIMARY KEY,
        Name NVARCHAR(100),
        MiddleName NVARCHAR(100)
    )
""")
cursor.execute("""
    INSERT INTO #NullDemo (Name, MiddleName) 
    VALUES (%(name)s, %(middle)s)
""", {"name": "Alice", "middle": None})  # NULL

cursor.execute("""
    INSERT INTO #NullDemo (Name, MiddleName) 
    VALUES (%(name)s, %(middle)s)
""", {"name": "Bob", "middle": ""})  # Empty string

# Query differences
cursor.execute("SELECT * FROM #NullDemo WHERE MiddleName IS NULL")
cursor.execute("SELECT * FROM #NullDemo WHERE MiddleName = ''")
```

### Trim operations
Use Python's string methods to remove leading, trailing, or both whitespace from values retrieved from the database.
```python
cursor.execute("SELECT Name FROM Production.Product")
for row in cursor:
    # Remove whitespace
    trimmed = row.Name.strip()  # Both ends
    left_trimmed = row.Name.lstrip()
    right_trimmed = row.Name.rstrip()
```

## JSON string data

Store JSON documents in **nvarchar(max)** columns and query them with Microsoft SQL's JSON functions.

### Store JSON as nvarchar

Serialize Python dictionaries to JSON strings and insert them into nvarchar columns; retrieve and deserialize them back into Python objects.

```python
import json

data = {"name": "Alice", "scores": [95, 87, 91], "active": True}
json_string = json.dumps(data)

cursor.execute("""
    CREATE TABLE #Configs (
        ID INT IDENTITY(1,1) PRIMARY KEY,
        ConfigData NVARCHAR(MAX)
    )
""")
cursor.execute("""
    INSERT INTO #Configs (ConfigData) VALUES (%(data)s)
""", {"data": json_string})

# Retrieve and parse
cursor.execute("SELECT ConfigData FROM #Configs WHERE ID = 1")
row = cursor.fetchone()
config = json.loads(row.ConfigData)
print(config["name"])  # 'Alice'
```

### Use Microsoft SQL JSON functions

Use Microsoft SQL's JSON functions to parse and filter JSON data directly in queries instead of in client code.

```python
import json

data = {"name": "Alice", "scores": [95, 87, 91], "active": True}

cursor.execute("""
    CREATE TABLE #Configs (
        ID INT IDENTITY(1,1) PRIMARY KEY,
        ConfigData NVARCHAR(MAX)
    )
""")
cursor.execute(
    "INSERT INTO #Configs (ConfigData) VALUES (%(data)s)",
    {"data": json.dumps(data)}
)
conn.commit()

cursor.execute("""
    SELECT JSON_VALUE(ConfigData, '$.name') AS Name
    FROM #Configs
    WHERE JSON_VALUE(ConfigData, '$.active') = 'true'
""")
for row in cursor:
    print(row.Name)  # 'Alice'
```

## Full-text search

Use `LIKE` for pattern matching, or enable a full-text index for more advanced text search.

### Full-text queries

The `LIKE` operator with wildcard patterns provides a straightforward alternative to full-text search when a full-text index isn't available.

```python
# Using CONTAINS (requires full-text index on the table)
cursor.execute("""
    SELECT JobTitle FROM HumanResources.Employee
    WHERE JobTitle LIKE %(search)s
""", {"search": "%Engineer%"})

# Pattern-based search as an alternative to full-text
cursor.execute("""
    SELECT Name FROM Production.Product
    WHERE Name LIKE %(search)s
""", {"search": "%Mountain%"})
```

## Best practices

Apply these guidelines to handle string data correctly across languages and encodings.

### Use nvarchar for international data

If you're unsure whether a column might contain Unicode, use `nvarchar`. The storage cost is modest and it prevents data loss from character conversion.

The following example shows the difference between defining columns for Unicode and ASCII-only data:

```sql
-- Good: supports any language
CREATE TABLE #UserProfile (
    Name NVARCHAR(100),
    Bio NVARCHAR(MAX)
);

-- Limited: ASCII/Latin only
CREATE TABLE #UserProfileAscii (
    Name VARCHAR(100),
    Bio VARCHAR(MAX)
);
```

### Validate string length

Check string length in Python before inserting to prevent truncation errors and provide meaningful error messages to users.

```python
def safe_insert(cursor, name: str, max_length: int = 100):
    """Insert with length validation."""
    if len(name) > max_length:
        raise ValueError(f"Name exceeds {max_length} characters")
    
    cursor.execute(
        "INSERT INTO #UserProfile (Name) VALUES (%(name)s)",
        {"name": name}
    )
```

### Handle binary strings separately

Distinguish between text strings (Python `str`, SQL `nvarchar`) and binary data (Python `bytes`, SQL `varbinary`) to avoid encoding problems.

```python
binary_data = b'\x00\x01\x02'  # bytes - use varbinary
text_data = "Hello"            # str - use nvarchar
```

## Related content

- [Data type mappings for mssql-python](data-type-mappings.md)
- [Handle NULL values](null-handling.md)
- [Work with binary data](binary-data.md)
