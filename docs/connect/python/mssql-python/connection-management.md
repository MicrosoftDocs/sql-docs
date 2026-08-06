---
title: Manage Connections with mssql-python
description: Learn how to open, close, and manage database connections using context managers, autocommit, and connection attributes with mssql-python.
author: dlevy-msft-sql
ms.author: dlevy
ms.date: 07/31/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---

# Manage connections with mssql-python

Most applications follow a simple pattern: open a connection, run queries, close the connection. The following sections cover opening and closing connections, using context managers, configuring autocommit, and working with connection attributes.

## Open a connection

Use the `connect()` function to establish a connection. Pass a connection string with your server, database, and authentication details:

```python
import mssql_python

conn = mssql_python.connect(
    "Server=<server>.database.windows.net;Database=<database>;"
    "Authentication=ActiveDirectoryDefault;Encrypt=yes"
)
```

The `connect()` function accepts:

- A connection string as the first positional argument or `connection_str` keyword.
- Individual keywords that the driver merges into the connection string.
- Other options like `autocommit`, `timeout`, and `attrs_before`.

You can mix both approaches. Keywords override values in the connection string, which is useful when you store a base connection string in configuration and override settings like `timeout` per call:

```python
# Base connection string from config, with per-call overrides
conn = mssql_python.connect(
    "Server=<server>.database.windows.net;Database=<database>;"
    "Authentication=ActiveDirectoryDefault;Encrypt=yes",
    timeout=30,
    autocommit=True
)
```

## Close a connection

Always close connections when done to return them to the connection pool and release server resources. Unclosed connections hold server-side memory and can eventually exhaust the connection pool, causing new connection attempts to block or fail.

```python
conn = mssql_python.connect(connection_string)
try:
    # Use the connection
    cursor = conn.cursor()
    cursor.execute("SELECT 1")
finally:
    conn.close()
```

Once closed, the connection can't be used:

```python
conn.close()
print(conn.closed)  # True

# This raises an error
cursor = conn.cursor()  # InterfaceError: Cannot create cursor on closed connection
```

Calling `close()` multiple times is safe (idempotent):

```python
conn.close()
conn.close()  # No error
```

## Context managers

Use the `with` statement to manage connections in most applications. It guarantees that the driver closes the connection when the block exits, even if an exception occurs. This approach eliminates the risk of leaked connections from forgotten `close()` calls:

```python
with mssql_python.connect(connection_string) as conn:
    cursor = conn.cursor()
    cursor.execute("CREATE TABLE #Demo (Name NVARCHAR(50))")
    cursor.execute("INSERT INTO #Demo (Name) VALUES ('Widget')")
    conn.commit()  # Must commit explicitly when autocommit=False
# Connection automatically closed
```

The context manager closes the connection on exit. It does **not** automatically commit or roll back transactions:

- **Always**: Calls `close()` on exit, whether or not an exception occurred.
- **`close()` behavior**: If `autocommit=False`, any uncommitted changes are rolled back when the connection closes.
- **You must call `conn.commit()` explicitly** to persist changes.

This design follows PEP 249 behavior and prevents accidental partial commits. If your code raises an exception before reaching `commit()`, the in-progress transaction is safely rolled back:

```python
# Equivalent manual code:
conn = mssql_python.connect(connection_string)
try:
    cursor = conn.cursor()
    cursor.execute("CREATE TABLE #Demo (Name NVARCHAR(50))")
    cursor.execute("INSERT INTO #Demo (Name) VALUES ('Widget')")
    conn.commit()  # Must commit explicitly
finally:
    conn.close()  # Rolls back uncommitted changes if autocommit=False
```

## Autocommit mode

By default, `autocommit=False`, which means each statement runs inside an implicit transaction. You must call `conn.commit()` to persist changes or `conn.rollback()` to discard them. Implicit transactions are the safest choice for data modifications because it lets you group multiple statements into a single atomic operation.

Enable autocommit when you want each statement to commit immediately. Autocommit is useful for DDL operations (`CREATE TABLE`, `ALTER INDEX`), read-only workloads, or administrative scripts where transaction grouping isn't needed:

```python
conn = mssql_python.connect(connection_string)
print(conn.autocommit)  # False

cursor = conn.cursor()
cursor.execute("CREATE TABLE #Demo (Name NVARCHAR(50))")
cursor.execute("INSERT INTO #Demo (Name) VALUES ('Widget')")
conn.commit()  # Required to persist changes
```

Enable autocommit for each statement to commit immediately. Use `autocommit=True` at connection time, or toggle it after connecting with `setautocommit()` or direct property assignment:

```python
# At connection time
conn = mssql_python.connect(connection_string, autocommit=True)

# Or after connection (both forms work)
conn.setautocommit(True)
conn.autocommit = True
print(conn.autocommit)  # True

# Now changes are committed automatically
cursor = conn.cursor()
cursor.execute("SELECT TOP 1 Name FROM Production.Product")
print(cursor.fetchone().Name)
# No commit() needed
```

## Connection timeout

Set the connection timeout to control how long the driver waits to establish a connection before raising an error. A reasonable connection timeout is important for applications deployed in environments with unreliable networks or for failing fast when a server is unreachable:

```python
# At connection time (in seconds)
conn = mssql_python.connect(connection_string, timeout=30)

# Or after connection
conn.timeout = 60
print(conn.timeout)  # 60
```

A timeout of `0` means no timeout (wait indefinitely). Define reasonable timeouts in production; a hung connection attempt with no timeout blocks the calling thread permanently.

If the target is [Azure SQL Database serverless](/azure/azure-sql/database/serverless-tier-overview) with auto-pause enabled, use at least `60`. An auto-paused database resumes on the first connect, and the resume can take 30 to 60 seconds or more. A shorter timeout expires before the resume completes and the connect attempt fails.

## Connection attributes

Use `set_attr()` to modify connection behavior at runtime. Connection attributes control low-level driver settings like access mode, transaction isolation, and packet size. Most applications don't need to change these attributes, but they're useful for specific scenarios:

- **Read-only mode**: Prevents accidental writes in reporting queries.
- **Transaction isolation**: Controls how concurrent transactions interact (use `SERIALIZABLE` for strict consistency, `READ_COMMITTED` for general use).
- **Packet size**: Tune for high-latency or high-throughput networks.

```python
import mssql_python

conn = mssql_python.connect(connection_string)

# Set read-only mode
conn.set_attr(mssql_python.SQL_ATTR_ACCESS_MODE, mssql_python.SQL_MODE_READ_ONLY)

# Set transaction isolation level
conn.set_attr(mssql_python.SQL_ATTR_TXN_ISOLATION, mssql_python.SQL_TXN_SERIALIZABLE)
```

Available attributes:

| Constant | Description |
| ---------- | ------------- |
| `SQL_ATTR_CONNECTION_TIMEOUT` | Connection timeout in seconds. |
| `SQL_ATTR_LOGIN_TIMEOUT` | Sign-in timeout in seconds. |
| `SQL_ATTR_PACKET_SIZE` | Network packet size. |
| `SQL_ATTR_ACCESS_MODE` | Read-only or read-write mode. |
| `SQL_ATTR_TXN_ISOLATION` | Transaction isolation level. |
| `SQL_ATTR_CURRENT_CATALOG` | Current database name. |

### Preconnection attributes

Some attributes must be set before the driver establishes the connection (for example, sign-in timeout). Pass them through `attrs_before`:

```python
conn = mssql_python.connect(
    connection_string,
    attrs_before={
        mssql_python.SQL_ATTR_LOGIN_TIMEOUT: 30,
        mssql_python.SQL_ATTR_CONNECTION_TIMEOUT: 60,
    }
)
```

## Get connection information

Use `getinfo()` to retrieve driver and server metadata for logging, diagnostics, or adapting behavior based on server capabilities:

```python
conn = mssql_python.connect(connection_string)

# Server information
print(f"Server name: {conn.getinfo(mssql_python.SQL_SERVER_NAME)}")
print(f"Database name: {conn.getinfo(mssql_python.SQL_DATABASE_NAME)}")

# Driver information
print(f"Driver name: {conn.getinfo(mssql_python.SQL_DRIVER_NAME)}")
print(f"Driver version: {conn.getinfo(mssql_python.SQL_DRIVER_VER)}")
```

Get a list of available info constants:

```python
constants = mssql_python.get_info_constants()
for name, value in constants.items():
    print(f"{name}: {value}")
```

## Search escape character

The `searchescape` property returns the character used to escape wildcards (`%` and `_`) in `LIKE` patterns. Use it to safely search for literal wildcard characters in user input:

```python
escape = conn.searchescape

# Use in queries with wildcard characters
cursor.execute(
    f"SELECT Name FROM Production.Product WHERE Name LIKE '%{escape}%%' ESCAPE '{escape}'"
)
# Matches names containing literal '%' character
```

## Encoding and decoding

Configure text encoding for SQL statements and results. The default settings work for most applications. Change them only if you connect to a server that uses a non-UTF-8 encoding for `char`/`varchar` columns. The encoding a server uses depends on the [column collation](../../../relational-databases/collations/collation-and-unicode-support.md):

```python
# Set encoding for outbound text
conn.setencoding(encoding='utf-8')

# Get current encoding settings
settings = conn.getencoding()
print(settings)  # {'encoding': 'utf-8', 'ctype': ...}

# Set decoding for inbound text from specific SQL types
conn.setdecoding(mssql_python.SQL_CHAR, encoding='utf-8')

# Get current decoding settings
settings = conn.getdecoding(mssql_python.SQL_CHAR)
print(settings)
```

Default encodings:

| Direction | SQL type | Default encoding |
| ----------- | ---------- | ------------------ |
| Outbound (str) | SQL_WCHAR | `utf-16le` |
| Inbound | SQL_CHAR | `utf-8` |
| Inbound | SQL_WCHAR | `utf-16le` |
| Inbound | SQL_WMETADATA | `utf-16le` |

## Best practices

- **Use context managers** (`with` blocks) for all connections in application code. They guarantee cleanup even when exceptions occur.
- **Use connection pooling** for better performance (enabled by default). See [Connection pooling](connection-pooling.md).
- **Set appropriate timeouts** for your network environment. A 30-second timeout suits most cloud deployments; increase it for cross-region or VPN connections. Use at least `60` for [Azure SQL Database serverless](/azure/azure-sql/database/serverless-tier-overview) with auto-pause enabled, because an auto-paused database can take 30 to 60 seconds or more to resume on the first connect.
- **Set `MultiSubnetFailover=yes` in the connection string** when the target is Azure SQL Database, Azure SQL Managed Instance, SQL database in Microsoft Fabric, an availability group listener, or a failover cluster instance. It's safe on single-IP targets, so leave it on for all Microsoft SQL family endpoints.
- **Use `autocommit=False`** (the default) for data modification scenarios where you need transactional atomicity.
- **Use `autocommit=True`** for DDL operations, read-only queries, and admin scripts.
- **Don't share connections across threads**. The driver's thread safety level is 1 (threads can share the module but not connections).

## Related content

- [Connection strings](connection-strings.md)
- [Connection pooling](connection-pooling.md)
- [Transaction management](transaction-management.md)
- [Cursor management](cursor-management.md)
