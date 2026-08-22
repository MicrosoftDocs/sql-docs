---
title: Troubleshoot mssql-python
description: Diagnose and resolve common issues when using the mssql-python driver to connect to SQL Server.
author: dlevy-msft-sql
ms.author: dlevy
ms.date: 07/17/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: troubleshooting
ai-usage: ai-assisted
---

# Troubleshoot mssql-python

Diagnose and resolve common issues when using the mssql-python driver to connect to SQL Server, Azure SQL Database, Azure SQL Managed Instance, and SQL database in Microsoft Fabric.

## Install issues

### pip install fails or builds from source

**Symptoms:**

```text
error: Microsoft Visual C++ 14.0 or greater is required
ERROR: Failed building wheel for mssql-python
```

**Possible causes and solutions:**

- **No prebuilt wheel for your platform**
    - Check that you're running a supported Python version (3.10 and later versions) and platform. See [Support lifecycle](support-lifecycle.md) for the compatibility matrix. Upgrade pip before installing with `pip install --upgrade pip`. For repeatable team environments, use the locked workflow in [Repeatable deployments](python-sql-driver-mssql-python-repeatable-deployments-quickstart.md) or the container patterns in [Container and local development](container-local-development.md) to reduce local machine drift.

- **Virtual environment not activated**
    - Activate your virtual environment first. Installing into the system Python can cause permission errors or conflicts.

   ### [Windows](#tab/windows)

   ```console
   python -m venv .venv
   .venv\Scripts\activate
   pip install mssql-python
   ```

   ### [Linux/macOS](#tab/linux-macos)

   ```console
   python -m venv .venv
   source .venv/bin/activate
   pip install mssql-python
   ```

   ---

- **Missing Linux system libraries**
    - The driver requires a small set of system libraries on Linux. See [Platform-specific dependencies](container-local-development.md#platform-specific-dependencies) for the packages to install.

### Conflicting driver installations

**Symptoms:**

Import errors or unexpected behavior after installing `mssql-python` alongside `pyodbc` in the same environment.

**Fix:**

`mssql-python` and `pyodbc` can coexist. If you see conflicts, create a clean virtual environment:

# [Windows](#tab/windows)

```console
python -m venv .venv --clear
.venv\Scripts\activate
pip install mssql-python
```

# [Linux/macOS](#tab/linux-macos)

```console
python -m venv .venv --clear
source .venv/bin/activate
pip install mssql-python
```

---

## Connection issues

### Unable to connect to server

**Symptoms:**

```text
OperationalError: [08001] (0) Client unable to establish connection
```

**Possible causes and solutions:**

- **Server not reachable**
    - Verify the server name and port are correct.
    - Check network connectivity: `ping servername` or `telnet servername 1433`.
    - Ensure firewall allows outbound connections on port 1433.

- **SQL Server not running**
    - Verify SQL Server service is started.
    - For named instances, verify SQL Server Browser service is running.

- **Azure SQL firewall rules**
    - Add your client IP to the Azure SQL firewall rules in the Azure portal.
    - For Azure SQL Managed Instance, ensure you're connecting from an allowed network.

```python
# Test basic connectivity
import socket
try:
    sock = socket.create_connection(("<server>.database.windows.net", 1433), timeout=5)
    print("TCP connection successful")
    sock.close()
except Exception as e:
    print(f"Cannot reach server: {e}")
```

### Login failed

**Symptoms:**

```text
OperationalError: [28000] (18456) Login failed for user 'username'.
```

**Possible causes and solutions:**

- **Authentication mode mismatch**
    - For Azure SQL Database, Azure SQL Managed Instance, and SQL database in Fabric, prefer a Microsoft Entra mode such as `Authentication=ActiveDirectoryDefault`.
    - If you're using SQL authentication intentionally, verify that the server allows it and that you're using the correct login format for that endpoint.

- **Incorrect SQL authentication credentials**
    - Verify username and password.
    - For Azure SQL, include the full username: `username@servername`.

- **User doesn't exist in database**
    - Verify the user has access to the specified database.
    - Check if the sign-in is mapped to a database user.

- **Authentication not configured**
    - Use Microsoft Entra authentication (recommended): `Authentication=ActiveDirectoryDefault`.
    - If you're troubleshooting a local SQL Server that should accept SQL authentication, verify that SQL Server uses mixed mode authentication.

### Connection timeout

**Symptoms:**

```text
OperationalError: [HYT00] (0) Timeout expired
OperationalError: [HYT01] (0) Connection timeout expired
```

**Possible causes and solutions:**

- **Server is slow to respond**
    - Increase the connection timeout:

   ```python
   conn = mssql_python.connect(connection_string, timeout=60)
   ```


- **Network latency**
    - Check network path to server.
    - Consider using a shorter network path or VPN.

- **Server under heavy load**
    - Try connecting during off-peak hours.
    - Contact your database administrator.

### SSL certificate errors

**Symptoms:**

```text
OperationalError: [08001] SSL Provider: The certificate chain was issued by an authority that is not trusted
```

**Solutions:**

First, prefer a trusted certificate or the local development patterns in [Container and local development](container-local-development.md). Use `TrustServerCertificate=yes` only for local development against a server that you control.

For development and testing with a self-signed certificate:

```python
conn = mssql_python.connect(
    "Server=<server>.database.windows.net;"
    "Database=<database>;"
    "Authentication=ActiveDirectoryDefault;"
    "Encrypt=yes;"
    "TrustServerCertificate=yes;"  # Don't use in production
)
```

> [!CAUTION]
> `TrustServerCertificate=yes` is a local-only fallback. Don't carry it into shared devcontainers, CI pipelines, or production deployments. For broader guidance, see [Encryption and certificates](encryption-certificates.md).

For production, ensure proper certificates are installed and use:

```python
conn = mssql_python.connect(
    "Server=<server>.database.windows.net;"
    "Database=<database>;"
    "Authentication=ActiveDirectoryDefault;"
    "Encrypt=yes;"
    "HostnameInCertificate=<server>.domain.com;"
)
```

## Query execution issues

### Table or object not found

**Symptoms:**

```text
ProgrammingError: [42S02] (208) Invalid object name 'TableName'.
```

**Possible causes and solutions:**

- **Wrong database context**

   ```python
   # Ensure you're connected to the correct database
   cursor.execute("SELECT DB_NAME()")
   print(cursor.fetchone()[0])
   ```

- **Schema not specified**

   ```python
   # Use fully qualified name
   cursor.execute("SELECT * FROM dbo.TableName")
   ```

- **Table doesn't exist**

   ```python
   # Check if table exists
   cursor.execute("""
       SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES 
       WHERE TABLE_NAME = 'TableName'
   """)
   ```

### Syntax error

**Symptoms:**

```text
ProgrammingError: [42000] (102) Incorrect syntax near '...'.
```

**Solutions:**

1. **Test SQL in SSMS first** to verify syntax
1. **Check string escaping** - use parameterized queries:

   ```python
   # Wrong - vulnerable to syntax issues and SQL injection
   cursor.execute(f"SELECT * FROM Production.Product WHERE Name = '{name}'")
   
   # Correct - use parameters
   cursor.execute("SELECT * FROM Production.Product WHERE Name = %(name)s", {"name": name})
   ```

### Parameter errors

**Symptoms:**

```text
ProgrammingError: [07001] Wrong number of parameters
```

**Solutions:**

1. **Count placeholders and parameters** - they must match

1. **Choose the right parameter style**:

   ```python
   # Qmark style - positional
   cursor.execute("SELECT * FROM Production.Product WHERE ProductID = ? AND Name LIKE ?", (1, "Adjustable%"))
   print(cursor.fetchone())

   # Pyformat style - named
   cursor.execute("SELECT * FROM Production.Product WHERE ProductID = %(id)s AND Name LIKE %(name)s", {"id": 1, "name": "Adjustable%"})
   print(cursor.fetchone())
   ```

## Data type issues

### Datetime conversion errors

**Symptoms:**

```text
DataError: [22007] Invalid datetime format
```

**Solutions:**

Use Python datetime objects instead of strings:

```python
from datetime import datetime

cursor.execute("CREATE TABLE #Events (EventDate DATETIME)")

# Wrong - this raises an error for invalid dates
try:
    cursor.execute("INSERT INTO #Events (EventDate) VALUES (%(event_date)s)", {"event_date": "2024-13-45"})
except Exception as e:
    print(f"Expected error: {e}")

# Correct - use Python datetime objects
cursor.execute("INSERT INTO #Events (EventDate) VALUES (%(event_date)s)", {"event_date": datetime(2024, 3, 15)})
cursor.execute("SELECT EventDate FROM #Events")
print(cursor.fetchone())
```

### Decimal precision issues

**Symptoms:**

Numbers appear truncated or rounded incorrectly.

**Solutions:**

Use `decimal.Decimal` for precise numeric values:

```python
from decimal import Decimal

cursor.execute("CREATE TABLE #PriceDemo (ListPrice DECIMAL(10,2))")
# Preserve full precision
cursor.execute(
    "INSERT INTO #PriceDemo (ListPrice) VALUES (%(list_price)s)",
    {"list_price": Decimal("19.99")}
)
```

### Unicode encoding issues

**Symptoms:**

Special characters appear garbled or cause errors.

**Solutions:**

1. **Use NVARCHAR columns** for Unicode data in your database

1. **Pass strings directly** - the driver handles encoding:

   ```python
   cursor.execute("CREATE TABLE #UnicodeDemo (Name NVARCHAR(50))")
   cursor.execute("INSERT INTO #UnicodeDemo (Name) VALUES (%(name)s)", {"name": "日本語"})
   cursor.execute("SELECT Name FROM #UnicodeDemo")
   print(cursor.fetchone())
   ```

## Performance issues

### Slow query execution

**Possible causes and solutions:**

- **Missing indexes**: Check query execution plan in SSMS.
- **Large result sets**: Use `fetchmany()` instead of `fetchall()`:

   ```python
   cursor.arraysize = 1000
   while True:
       rows = cursor.fetchmany()
       if not rows:
           break
       process_rows(rows)
   ```

- **Connection pooling disabled**: Enable pooling:

   ```python
   import mssql_python
   mssql_python.pooling(max_size=20, idle_timeout=300)
   ```

### Memory issues with large results

**Symptoms:**

Python process runs out of memory.

**Solutions:**

1. **Stream results** instead of loading all into memory:

   ```python
   cursor.execute("SELECT * FROM LargeTable")
   for row in cursor:  # Iterates one row at a time
       process_row(row)
   ```

1. **Use server-side pagination**:

   ```python
   page_size = 1000
   offset = 0
   while True:
       cursor.execute(
           "SELECT * FROM LargeTable ORDER BY ID "
           "OFFSET ? ROWS FETCH NEXT ? ROWS ONLY",
           (offset, page_size)
       )
       rows = cursor.fetchall()
       if not rows:
           break
       process_rows(rows)
       offset += page_size
   ```

## Transaction issues

### Temp table scoping with autocommit

Temp tables (`#tablename`) created inside a transaction disappear when the transaction is rolled back. This is a common source of confusion when autocommit is off (the default):

```python
conn = mssql_python.connect(connection_string)  # autocommit=False by default
cursor = conn.cursor()

cursor.execute("CREATE TABLE #TempData (ID INT, Name NVARCHAR(50))")
cursor.execute("INSERT INTO #TempData VALUES (1, 'test')")

# If the connection rolls back (explicit or on error), #TempData disappears
conn.rollback()

# This fails: Invalid object name '#TempData'
cursor.execute("SELECT * FROM #TempData")
```

**Fix:** Commit immediately after creating a temp table, or use autocommit mode:

```python
cursor.execute("CREATE TABLE #TempData (ID INT, Name NVARCHAR(50))")
conn.commit()  # Lock in the table definition

cursor.execute("INSERT INTO #TempData VALUES (1, 'test')")
conn.commit()
```

DDL statements that require autocommit mode, such as `CREATE DATABASE`, fail inside an open transaction. Set autocommit before running them:

```python
conn.autocommit = True
cursor.execute("CREATE DATABASE TestDB")
conn.autocommit = False
```

### Transaction not committed

**Symptoms:**

Data changes don't persist after closing connection.

**Solution:**

With `autocommit=False` (default), you must call `commit()`:

```python
cursor.execute("CREATE TABLE #Products (Name NVARCHAR(100))")
cursor.execute("INSERT INTO #Products (Name) VALUES (%(name)s)", {"name": "Widget"})
conn.commit()  # Don't forget this!
```

Or use autocommit mode:

```python
conn = mssql_python.connect(connection_string, autocommit=True)
```

### Deadlock errors

**Symptoms:**

```text
OperationalError: [40001] (1205) Transaction ... was deadlocked on lock resources with another process
```

**Solution:**

Retry logic (see [Retry logic](retry-logic.md)) handles the immediate failure, but recurring deadlocks indicate a design problem. To fix the root cause, capture the deadlock graph and analyze which statements and lock types are involved. Common fixes include reordering operations so competing transactions acquire locks in the same sequence, reducing transaction scope, and adding appropriate indexes to reduce lock duration.

For a full walkthrough of deadlock analysis, see [Deadlocks guide](../../../relational-databases/sql-server-deadlocks-guide.md). If you're using Azure SQL Database, see [Analyze and prevent deadlocks](/azure/azure-sql/database/analyze-prevent-deadlocks).

## Bulk load issues

### Constraint violations during bulkcopy

**Symptoms:**

```text
RuntimeError: CHECK constraint ... Conflict occurred in database ...
RuntimeError: Cannot insert duplicate key ... violation of PRIMARY KEY constraint
```

**Cause:**

Data in your batch violates table constraints (primary key, unique, CHECK, or foreign key).

**Fix:**

Validate data before loading. For large datasets, load into a staging table first, then merge into the target:

```python
# Load into staging, then validate
cursor.execute("CREATE TABLE ##Staging (ID INT, Name NVARCHAR(100))")
cursor.bulkcopy("##Staging", rows)

# Check for duplicates before merging
cursor.execute("""
    SELECT s.ID FROM ##Staging s
    INNER JOIN dbo.Target t ON s.ID = t.ID
""")
dupes = cursor.fetchall()
if dupes:
    print(f"Skipping {len(dupes)} duplicate rows")

# Insert only non-duplicate rows
cursor.execute("""
    INSERT INTO dbo.Target (ID, Name)
    SELECT s.ID, s.Name FROM ##Staging s
    WHERE NOT EXISTS (SELECT 1 FROM dbo.Target t WHERE t.ID = s.ID)
""")
conn.commit()
```

For upsert patterns with staging tables, see [Data loading and movement patterns](data-loading-movement-patterns.md).

### Column mapping errors

**Symptoms:**

```text
RuntimeError: Bulk copy failure - column count mismatch
```

**Cause:**

The number of columns in your data doesn't match the target table's column count, or columns are in the wrong order.

**Fix:**

Ensure your data matches the table schema exactly in order and count:

```python
# Check the target table schema
cursor.execute("""
    SELECT COLUMN_NAME, DATA_TYPE
    FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'MyTable'
    ORDER BY ORDINAL_POSITION
""")
for col in cursor.fetchall():
    print(col)

# Match your data to the column order
rows = [
    (1, "Widget", Decimal("19.99")),  # Must match table column order
    (2, "Gadget", Decimal("29.99")),
]
cursor.bulkcopy("dbo.MyTable", rows)
```

### Type mismatches during bulkcopy

**Symptoms:**

Data loads but values are truncated, rounded, or incorrect.

**Cause:**

Python values don't map cleanly to the target column types. Common cases: `float` values loaded into `decimal` columns (precision loss), or oversized strings loaded into fixed-length columns.

**Fix:**

Use the correct Python types that match your schema:

```python
from decimal import Decimal

# Use Decimal for decimal/numeric columns, not float
rows = [
    (1, "Widget", Decimal("19.99")),  # Correct
    # (1, "Widget", 19.99),           # Avoid: float loses precision
]
cursor.bulkcopy("dbo.Products", rows)
```

## Numpy type binding failures

**Symptoms:**

Parameters silently fail or raise data type errors when using numpy integer or float types.

**Cause:**

Numpy types like `numpy.int64` and `numpy.int32` don't pass `isinstance(x, int)` in NumPy 2.x. The driver's type inference doesn't recognize them, which causes unexpected behavior.

**Fix:**

Convert numpy values to native Python types before binding:

```python
import numpy as np

# Convert individual values
cursor.execute("SELECT * FROM Production.Product WHERE ProductID = %(product_id)s", {"product_id": int(np.int64(42))})

# Convert DataFrame values
for _, row in df.iterrows():
    cursor.execute(
        "INSERT INTO #Orders (ProductID, Qty) VALUES (%(product_id)s, %(qty)s)",
        {"product_id": int(row["ProductID"]), "qty": int(row["Qty"])}
    )
```

For larger datasets, use the [Arrow](arrow-integration.md) or [pandas](pandas-integration.md) integration paths instead, which handle type conversion internally.

## Bulkcopy with temp tables

**Symptoms:**

`cursor.bulkcopy("#TempTable", data)` raises `RuntimeError: Invalid object name '#TempTable'`.

**Cause:**

`bulkcopy()` can't resolve session temp tables (`#tablename`) due to metadata lookup limitations. Global temp tables (`##tablename`) and permanent tables work.

**Fix:**

Use a global temp table or a regular staging table:

```python
# Global temp table (visible to all sessions, dropped when last session disconnects)
cursor.execute("CREATE TABLE ##Staging (ID INT, Name NVARCHAR(50))")
cursor.bulkcopy("##Staging", rows)

# Or use a permanent staging table
cursor.execute("CREATE TABLE dbo.Staging (ID INT, Name NVARCHAR(50))")
cursor.bulkcopy("dbo.Staging", rows)
```

For small datasets where a session temp table is preferred, use `executemany()` instead:

```python
cursor.execute("CREATE TABLE #Staging (ID INT, Name NVARCHAR(50))")
cursor.executemany("INSERT INTO #Staging (ID, Name) VALUES (?, ?)", rows)
```

## Container and CI issues

### Missing system libraries on Linux

**Symptoms:**

```text
ImportError: libltdl.so.7: cannot open shared object file: No such file or directory
ImportError: libkrb5.so.3: cannot open shared object file
```

**Fix:**

Install the required system packages. The packages differ by distribution:

| Distribution | Install command |
| --- | --- |
| Ubuntu / Debian | `sudo apt-get install libltdl7 libkrb5-3 libgssapi-krb5-2` |
| Red Hat / Fedora | `sudo dnf install libtool-ltdl krb5-libs` |
| Alpine | `apk add libltdl krb5-libs` |

For Dockerfile examples, see [Container and local development](container-local-development.md).

### macOS SSL errors after install

**Symptoms:**

SSL-related errors when connecting from macOS, especially on Apple Silicon.

**Fix:**

Install OpenSSL via Homebrew and set the linker flags:

```bash
brew install openssl
export LDFLAGS="-L/opt/homebrew/opt/openssl/lib"
export CPPFLAGS="-I/opt/homebrew/opt/openssl/include"
```

## Diagnostic tools

### Enable driver logging

Use `mssql_python.setup_logging()` to enable comprehensive DEBUG logging for troubleshooting. All driver operations are logged, including SQL statements, parameters, internal ODBC operations, and connection state changes.

```python
import mssql_python

# Enable logging to file (default)
mssql_python.setup_logging()

# Output to stdout (useful for CI/CD and containers)
mssql_python.setup_logging(output='stdout')

# Output to both file and stdout
mssql_python.setup_logging(output='both')

# Custom log file path (must use .txt, .log, or .csv extension)
mssql_python.setup_logging(log_file_path="/var/log/myapp/mssql.log")
```

Log files are written in CSV format and automatically rotate at 512 MB with five backups. Sensitive data like passwords and access tokens are automatically sanitized in log output.

To add your own log entries alongside driver logs, use `driver_logger`:

```python
from mssql_python.logging import driver_logger

mssql_python.setup_logging()

driver_logger.debug("[App] Starting data processing")
driver_logger.error("[App] Failed to process record")
# Your entries appear in the same file with the same format
```

> [!CAUTION]
> Logging has performance overhead. Enable it only when troubleshooting, not in production by default.

### Get driver information

Retrieve the driver version and server details from an active connection:

```python
import mssql_python

conn = mssql_python.connect(connection_string)

# Driver version
print(f"Version: {mssql_python.__version__}")

# Server information
print(f"Server name: {conn.getinfo(mssql_python.SQL_SERVER_NAME)}")
print(f"Database name: {conn.getinfo(mssql_python.SQL_DATABASE_NAME)}")
```

### Check connection state

Test whether a connection is still open before attempting operations:

```python
try:
    cursor = conn.cursor()
    cursor.execute("SELECT 1")
    print("Connection is open")
except mssql_python.Error:
    print("Connection is closed or broken")
```

## Quick reference: Common errors

| Error | SQLSTATE | Common cause | Quick fix |
| ------- | ---------- | -------------- | ----------- |
| Client unable to establish connection | 08001 | Server unreachable | Check server name/port |
| Login failed | 28000 | Wrong credentials | Verify username/password |
| Timeout expired | HYT00/HYT01 | Slow network | Increase timeout |
| Invalid object name | 42S02 | Wrong table/schema | Use fully qualified names |
| Syntax error | 42000 | SQL error | Use parameterized queries |
| Constraint violation | 23000 | FK/PK violation | Check data integrity |
| Deadlock | 40001 | Lock contention | Retry, then [analyze the deadlock graph](../../../relational-databases/sql-server-deadlocks-guide.md) |

## Related content

- [Error handling and SQLSTATE codes for mssql-python](error-handling.md)
- [Connection strings for mssql-python](connection-strings.md)
- [Connection pooling with mssql-python](connection-pooling.md)
- [GitHub issues](https://github.com/microsoft/mssql-python/issues)
