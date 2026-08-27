---
title: Error Handling and SQLSTATE Codes for mssql-python
description: Reference for mssql-python exception classes, error handling patterns, and SQLSTATE code mappings.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, randolphwest
ms.date: 08/27/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
ai-usage: ai-assisted
---

# Error handling and SQLSTATE codes for mssql-python

The mssql-python driver defines a standard exception hierarchy, common error handling patterns, and SQLSTATE code mappings for SQL Server and Azure SQL.

## Exception hierarchy

The mssql-python driver follows the DB-API 2.0 (PEP 249) exception hierarchy:

```text
Exception (builtins)
├── Warning
└── Error
    ├── InterfaceError
    └── DatabaseError
        ├── DataError
        ├── OperationalError
        ├── IntegrityError
        ├── InternalError
        ├── ProgrammingError
        └── NotSupportedError

ConnectionStringParseError (standalone, not part of hierarchy)
```

### Exception descriptions

Catch the most specific exception that matches your situation. For example, catch `IntegrityError` for constraint violations on INSERT/UPDATE operations, and `ProgrammingError` for SQL syntax issues during development. Catch the base `Error` class only as a fallback.

| Exception | When raised |
| ----------- | ------------- |
| `Warning` | Non-fatal warnings from the database. |
| `Error` | Base class for all database errors. |
| `InterfaceError` | Errors related to the database interface (driver), not the database itself. |
| `DatabaseError` | Errors related to the database. |
| `DataError` | Errors due to problems with processed data (division by zero, value out of range). |
| `OperationalError` | Errors related to database operation (connection lost, memory allocation, transaction errors). |
| `IntegrityError` | Errors when database integrity is affected (foreign key violation, unique constraint). |
| `InternalError` | Internal database errors (cursor not valid, transaction out of sync). |
| `ProgrammingError` | Programming errors (syntax errors, table not found, wrong number of parameters). |
| `NotSupportedError` | Feature not supported by the database or driver. |
| `ConnectionStringParseError` | Invalid connection string syntax or unknown keywords. |

## Basic error handling

Use try-except blocks to handle database errors:

```python
import mssql_python

try:
    conn = mssql_python.connect(connection_string)
    cursor = conn.cursor()
    cursor.execute("INSERT INTO Production.Product (Name) VALUES (%(name)s)", {"name": "Test"})
    conn.commit()
except mssql_python.IntegrityError as e:
    print(f"Constraint violation: {e}")
    conn.rollback()
except mssql_python.ProgrammingError as e:
    print(f"SQL syntax error: {e}")
except mssql_python.OperationalError as e:
    print(f"Connection or operational error: {e}")
except mssql_python.Error as e:
    print(f"Database error: {e}")
finally:
    if 'conn' in locals():
        conn.close()
```

## Access exceptions through the connection

You can catch exceptions through the connection instance:

```python
try:
    cursor.execute("INVALID SQL")
except conn.ProgrammingError as e:
    print(f"Caught via connection: {e}")
```

## Error message structure

mssql-python exception objects expose three attributes that come from the driver's [`Exception` base class](https://github.com/microsoft/mssql-python/blob/main/mssql_python/exceptions.py):

| Attribute | Source | Description |
| --- | --- | --- |
| `driver_error` | Python driver | Standardized English text chosen by the SQLSTATE returned from ODBC (for example, `"Communication link failure"`, `"Invalid authorization specification"`, `"Syntax error or access violation"`). Stable across releases; safe to substring-match. |
| `ddbc_error` | Direct Database Connectivity (DDBC) | The server-side message, typically prefixed with `[Microsoft][SQL Server]`. Format isn't a stable contract. |
| `message` | Composed | `f"Driver Error: {driver_error}; DDBC Error: {ddbc_error}"`. This is what `str(exc)` returns. |

```python
try:
    cursor.execute("SELECT * FROM no_such_table;")
except mssql_python.ProgrammingError as exc:
    print(exc.driver_error)  # Base table or view not found
    print(exc.ddbc_error)    # [Microsoft][SQL Server]Invalid object name 'no_such_table'.
    print(exc)               # Driver Error: Base table or view not found; DDBC Error: ...
```

The SQL Server engine error number (such as `208` or `40501`) isn't exposed as an attribute and isn't reliably embedded in either string. Classify errors by exception subclass plus `driver_error` text. For Azure SQL throttling, see [Retry logic](retry-logic.md).

### SQLSTATE classification

mssql-python uses the SQLSTATE returned by ODBC to choose both the Python exception subclass and the `driver_error` text. The full SQLSTATE &rarr; exception mapping is in [`exceptions.py`](https://github.com/microsoft/mssql-python/blob/main/mssql_python/exceptions.py) in the driver source. The next section lists the SQLSTATEs that appear most often with SQL Server and Azure SQL.

## Connection errors

Connection failures from `mssql_python.connect()` raise `mssql_python.OperationalError`, the same as other connectivity failures:

```python
import mssql_python

try:
    conn = mssql_python.connect(
        "Server=unreachable-server.database.windows.net;"
        "Database=<database>;"
        "Authentication=ActiveDirectoryDefault;"
        "Encrypt=yes"
    )
except mssql_python.OperationalError as e:
    print(f"Connection failed: {e.driver_error}")
    # e.driver_error: "Client unable to establish connection"
```

## Connection string errors

Connection string parsing errors raise `ConnectionStringParseError`:

```python
try:
    conn = mssql_python.connect("Servr=localhost;")  # Typo
except mssql_python.ConnectionStringParseError as e:
    print(f"Invalid connection string: {e}")
    # Output: Unknown keyword 'Servr'
```

## SQLSTATE code reference

SQLSTATE codes are five-character codes that identify error conditions. The first two characters indicate the class, and the last three indicate the subclass. You rarely need to inspect these codes directly. Instead, catch the appropriate Python exception type (listed in the "Exception" column). Use SQLSTATE codes when you need to distinguish between specific error conditions within the same exception type, for example to differentiate a deadlock (40001) from a general connection failure (08S01).

### Class 00 - Successful completion

| SQLSTATE | Exception | Description |
| ---------- | ----------- | ------------- |
| 00000 | None | Success |

### Class 01 - Warning

| SQLSTATE | Exception | Description |
| ---------- | ----------- | ------------- |
| 01000 | Warning | General warning |
| 01001 | Warning | Cursor operation conflict |
| 01002 | Warning | Disconnect error |
| 01003 | DataError | NULL value eliminated in set function |
| 01004 | DataError | String data, right truncation |
| 01006 | Warning | Privilege not revoked |
| 01007 | Warning | Privilege not granted |
| 01S00 | Warning | Invalid connection string attribute |
| 01S01 | Warning | Error in row |
| 01S02 | Warning | Option value changed |

### Class 07 - Dynamic SQL error

| SQLSTATE | Exception | Description |
| ---------- | ----------- | ------------- |
| 07001 | ProgrammingError | Wrong number of parameters |
| 07002 | ProgrammingError | COUNT field incorrect |
| 07005 | ProgrammingError | Prepared statement not a cursor-specification |
| 07006 | ProgrammingError | Restricted data type attribute violation |
| 07009 | ProgrammingError | Invalid descriptor index |
| 07S01 | ProgrammingError | Invalid use of default parameter |

### Class 08 - Connection exception

| SQLSTATE | Exception | Description |
| ---------- | ----------- | ------------- |
| 08001 | OperationalError | Client unable to establish connection |
| 08002 | OperationalError | Connection name in use |
| 08003 | OperationalError | Connection does not exist |
| 08004 | OperationalError | Server rejected the connection |
| 08007 | OperationalError | Connection failure during transaction |
| 08S01 | OperationalError | Communication link failure |

### Class 21 - Cardinality violation

| SQLSTATE | Exception | Description |
| ---------- | ----------- | ------------- |
| 21S01 | ProgrammingError | Insert value list does not match column list |
| 21S02 | ProgrammingError | Degree of derived table does not match column list |

### Class 22 - Data exception

| SQLSTATE | Exception | Description |
| ---------- | ----------- | ------------- |
| 22001 | DataError | String data, right truncation |
| 22002 | DataError | Indicator variable required but not supplied |
| 22003 | DataError | Numeric value out of range |
| 22007 | DataError | Invalid datetime format |
| 22008 | DataError | Datetime field overflow |
| 22012 | DataError | Division by zero |
| 22015 | DataError | Interval field overflow |
| 22018 | DataError | Invalid character value for cast specification |
| 22019 | DataError | Invalid escape character |
| 22025 | DataError | Invalid escape sequence |
| 22026 | DataError | String data, length mismatch |

### Class 23 - Integrity constraint violation

| SQLSTATE | Exception | Description |
| ---------- | ----------- | ------------- |
| 23000 | IntegrityError | Integrity constraint violation (general) |

### Class 24 - Invalid cursor state

| SQLSTATE | Exception | Description |
| ---------- | ----------- | ------------- |
| 24000 | InternalError | Invalid cursor state |

### Class 25 - Invalid transaction state

| SQLSTATE | Exception | Description |
| ---------- | ----------- | ------------- |
| 25000 | OperationalError | Invalid transaction state |
| 25S01 | OperationalError | Transaction state unknown |
| 25S02 | OperationalError | Transaction is still active |
| 25S03 | OperationalError | Transaction is rolled back |

### Class 28 - Invalid authorization specification

| SQLSTATE | Exception | Description |
| ---------- | ----------- | ------------- |
| 28000 | OperationalError | Invalid authorization specification (login failed) |

### Class 34 - Invalid cursor name

| SQLSTATE | Exception | Description |
| ---------- | ----------- | ------------- |
| 34000 | ProgrammingError | Invalid cursor name |

### Class 3C - Duplicate cursor name

| SQLSTATE | Exception | Description |
| ---------- | ----------- | ------------- |
| 3C000 | ProgrammingError | Duplicate cursor name |

### Class 3D - Invalid catalog name

| SQLSTATE | Exception | Description |
| ---------- | ----------- | ------------- |
| 3D000 | ProgrammingError | Invalid catalog name |

### Class 3F - Invalid schema name

| SQLSTATE | Exception | Description |
| ---------- | ----------- | ------------- |
| 3F000 | ProgrammingError | Invalid schema name |

### Class 40 - Transaction rollback

| SQLSTATE | Exception | Description |
| ---------- | ----------- | ------------- |
| 40001 | OperationalError | Serialization failure (deadlock) |
| 40002 | OperationalError | Integrity constraint violation caused rollback |
| 40003 | OperationalError | Statement completion unknown |

### Class 42 - Syntax error or access rule violation

| SQLSTATE | Exception | Description |
| ---------- | ----------- | ------------- |
| 42000 | ProgrammingError | Syntax error or access violation |
| 42S01 | ProgrammingError | Base table or view already exists |
| 42S02 | ProgrammingError | Base table or view not found |
| 42S11 | ProgrammingError | Index already exists |
| 42S12 | ProgrammingError | Index not found |
| 42S21 | ProgrammingError | Column already exists |
| 42S22 | ProgrammingError | Column not found |

### Class 44 - WITH CHECK OPTION violation

| SQLSTATE | Exception | Description |
| ---------- | ----------- | ------------- |
| 44000 | IntegrityError | WITH CHECK OPTION violation |

### Class HY - CLI-specific condition

| SQLSTATE | Exception | Description |
| ---------- | ----------- | ------------- |
| HY000 | DatabaseError | General error |
| HY001 | OperationalError | Memory allocation error |
| HY003 | ProgrammingError | Invalid application buffer type |
| HY004 | ProgrammingError | Invalid SQL data type |
| HY007 | ProgrammingError | Associated statement is not prepared |
| HY008 | OperationalError | Operation canceled |
| HY009 | ProgrammingError | Invalid use of null pointer |
| HY010 | ProgrammingError | Function sequence error |
| HY011 | ProgrammingError | Attribute cannot be set now |
| HY012 | ProgrammingError | Invalid transaction operation code |
| HY013 | OperationalError | Memory management error |
| HY014 | OperationalError | Limit on number of handles exceeded |
| HY015 | ProgrammingError | No cursor name available |
| HY016 | ProgrammingError | Cannot modify an implementation row descriptor |
| HY017 | ProgrammingError | Invalid use of automatically allocated descriptor handle |
| HY018 | OperationalError | Server declined cancel request |
| HY019 | ProgrammingError | Non-character and non-binary data sent in pieces |
| HY020 | DataError | Attempt to concatenate a null value |
| HY021 | ProgrammingError | Inconsistent descriptor information |
| HY024 | ProgrammingError | Invalid attribute value |
| HY090 | ProgrammingError | Invalid string or buffer length |
| HY091 | ProgrammingError | Invalid descriptor field identifier |
| HY092 | ProgrammingError | Invalid attribute/option identifier |
| HY095 | ProgrammingError | Function type out of range |
| HY096 | ProgrammingError | Invalid information type |
| HY097 | ProgrammingError | Column type out of range |
| HY098 | ProgrammingError | Scope type out of range |
| HY099 | ProgrammingError | Nullable type out of range |
| HY100 | ProgrammingError | Uniqueness option type out of range |
| HY101 | ProgrammingError | Accuracy option type out of range |
| HY103 | ProgrammingError | Invalid retrieval code |
| HY104 | ProgrammingError | Invalid precision or scale value |
| HY105 | ProgrammingError | Invalid parameter type |
| HY106 | ProgrammingError | Fetch type out of range |
| HY107 | ProgrammingError | Row value out of range |
| HY109 | ProgrammingError | Invalid cursor position |
| HY110 | ProgrammingError | Invalid driver completion |
| HY111 | ProgrammingError | Invalid bookmark value |
| HYC00 | NotSupportedError | Optional feature not implemented |
| HYT00 | OperationalError | Timeout expired |
| HYT01 | OperationalError | Connection timeout expired |

### Class IM - Driver manager error

| SQLSTATE | Exception | Description |
| ---------- | ----------- | ------------- |
| IM001 | InterfaceError | Driver does not support this function |
| IM002 | InterfaceError | Data source name not found |
| IM003 | InterfaceError | Specified driver could not be loaded |
| IM004 | InterfaceError | Driver's SQLAllocHandle on SQL_HANDLE_ENV failed |
| IM005 | InterfaceError | Driver's SQLAllocHandle on SQL_HANDLE_DBC failed |
| IM006 | InterfaceError | Driver's SQLSetConnectAttr failed |
| IM007 | InterfaceError | No data source or driver specified |
| IM008 | InterfaceError | Dialog failed |
| IM009 | InterfaceError | Unable to load translation DLL |
| IM010 | InterfaceError | Data source name too long |
| IM011 | InterfaceError | Driver name too long |
| IM012 | InterfaceError | DRIVER keyword syntax error |
| IM014 | InterfaceError | Invalid DSN |
| IM015 | InterfaceError | Corrupt file data source |

## Common SQL Server error numbers

Beyond SQLSTATE, SQL Server provides native error numbers in parentheses. These are the errors you're most likely to encounter in application code. Build retry logic around error 1205 (deadlock) and transient connection errors (see [Retry logic](retry-logic.md)).

| Error | Message pattern | Resolution |
| ------- | ----------------- | ------------ |
| 208 | Invalid object name | Verify that the table or view exists and check schema qualification. |
| 547 | Constraint violation | A foreign key or check constraint failed. |
| 2627 | Unique constraint violation | A duplicate key value was inserted. |
| 2601 | Unique index violation | A duplicate key exists in the index. |
| 4060 | Cannot open database | The database doesn't exist or access is denied. |
| 18456 | Login failed | Authentication failure. Check credentials. |
| 1205 | Deadlock victim | The transaction was rolled back. Retry the operation. |

## Symptom-to-exception quick reference

Use this table to map common symptoms to the exception type you should catch:

| Symptom | Exception | Likely cause |
| --- | --- | --- |
| "Login failed for user" | `OperationalError` | Wrong credentials or user not mapped to database. |
| "Client unable to establish connection" | `OperationalError` | Server unreachable, firewall, or DNS issue. |
| "Timeout expired" | `OperationalError` | Query or connection timeout. Increase timeout or optimize query. |
| "Invalid object name" | `ProgrammingError` | Table doesn't exist or schema not specified. |
| "Incorrect syntax" | `ProgrammingError` | SQL syntax error. Test query in SSMS. |
| "Wrong number of parameters" | `ProgrammingError` | Parameter count doesn't match placeholders. |
| "Violation of PRIMARY KEY" | `IntegrityError` | Duplicate key. Use MERGE or check before inserting. |
| "Violation of FOREIGN KEY" | `IntegrityError` | Referenced row doesn't exist. Insert parent first. |
| "Transaction was deadlocked" | `OperationalError` (error 1205) | Lock contention. Implement retry logic. |
| "String or binary data would be truncated" | `DataError` | Value exceeds column length. Check data or increase column size. |
| "Conversion failed" | `DataError` | Type mismatch. Use the correct Python type for the column. |
| "Unknown keyword" | `ConnectionStringParseError` | Misspelled keyword, or a keyword that another driver accepts. For more information, see [Keywords from other drivers](connection-strings.md#keywords-from-other-drivers). |
| "Reserved keyword" | `ConnectionStringParseError` | The driver controls this value. For more information, see [Reserved keywords](connection-strings.md#reserved-keywords). |
| "callproc is not supported" | `NotSupportedError` | Use `cursor.execute("EXECUTE ...")` instead. |

## Best practices

- **Catch specific exceptions** before generic ones. Order from most specific (`IntegrityError`) to least specific (`Error`).
- **Always handle IntegrityError** for data modification operations. Constraint violations are expected in normal operation (for example, a user trying to create a duplicate username).
- **Log the full error context** for troubleshooting. The exception exposes `driver_error` (stable, SQLSTATE-derived text) and `ddbc_error` (server-side message). Log both; classify on `driver_error`.
- **Implement retry logic** for transient errors (connection failures, deadlocks). See [Retry logic](retry-logic.md).
- **Use rollback()** in exception handlers to clean up failed transactions. Without explicit rollback, the connection remains in a failed transaction state.

## Related content

- [Troubleshoot mssql-python](troubleshooting.md)
- [Execute queries with mssql-python](executing-queries.md)
- [Manage connections with mssql-python](connection-management.md)
