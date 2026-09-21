---
title: Troubleshoot mssql-python
description: Find troubleshooting guidance for installation, connection, query, data, and operation issues in the mssql-python driver.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, randolphwest, sumitsar
ms.date: 09/17/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: troubleshooting
ai-usage: ai-assisted
---

# Troubleshoot mssql-python

Use this article to find troubleshooting guidance for the `mssql-python` driver. Start with the symptom or error message that matches your issue.

## Install issues

### pip install fails or builds from source

For unsupported Python versions, missing wheels, inactive virtual environments, and missing Linux libraries, see [Troubleshoot installation and connection issues](troubleshoot-installation-connection.md#pip-install-fails-or-builds-from-source).

### Conflicting driver installations

For import errors or unexpected behavior when `mssql-python` and `pyodbc` are installed together, see [Troubleshoot installation and connection issues](troubleshoot-installation-connection.md#conflicting-driver-installations).

## Connection issues

### Unable to connect to server

For SQLSTATE `08001`, unreachable servers, stopped services, and Azure SQL firewall rules, see [Troubleshoot installation and connection issues](troubleshoot-installation-connection.md#unable-to-connect-to-server).

### Login failed

For SQLSTATE `28000`, authentication mode mismatches, invalid credentials, and missing database users, see [Troubleshoot installation and connection issues](troubleshoot-installation-connection.md#login-failed).

### Connection timeout

For SQLSTATE `HYT00` or `HYT01`, network latency, slow servers, and connection timeout settings, see [Troubleshoot installation and connection issues](troubleshoot-installation-connection.md#connection-timeout).

### SSL certificate errors

For untrusted certificate errors and safe local development options, see [Troubleshoot installation and connection issues](troubleshoot-installation-connection.md#ssl-certificate-errors).

## Query execution issues

### Table or object not found

For SQLSTATE `42S02`, database context, schema qualification, and table existence checks, see [Troubleshoot query, data, and operation issues](troubleshoot-query-data-and-operations.md#table-or-object-not-found).

### Syntax error

For SQLSTATE `42000`, SQL syntax, string escaping, and parameterized queries, see [Troubleshoot query, data, and operation issues](troubleshoot-query-data-and-operations.md#syntax-error).

### Parameter errors

For SQLSTATE `07001`, placeholder counts, and supported parameter styles, see [Troubleshoot query, data, and operation issues](troubleshoot-query-data-and-operations.md#parameter-errors).

## Data type issues

### Datetime conversion errors

For SQLSTATE `22007` and datetime parameter conversion, see [Troubleshoot query, data, and operation issues](troubleshoot-query-data-and-operations.md#datetime-conversion-errors).

### Decimal precision issues

For truncated or rounded decimal values, see [Troubleshoot query, data, and operation issues](troubleshoot-query-data-and-operations.md#decimal-precision-issues).

### Unicode encoding issues

For garbled special characters and Unicode column types, see [Troubleshoot query, data, and operation issues](troubleshoot-query-data-and-operations.md#unicode-encoding-issues).

## Performance issues

### Slow query execution

For indexing, large result sets, and connection pooling, see [Troubleshoot query, data, and operation issues](troubleshoot-query-data-and-operations.md#slow-query-execution).

### Memory issues with large results

For streaming and paginating large result sets, see [Troubleshoot query, data, and operation issues](troubleshoot-query-data-and-operations.md#memory-issues-with-large-results).

## Transaction issues

### Temp table scoping with autocommit

For temp tables that disappear after rollback and DDL statements that require autocommit, see [Troubleshoot query, data, and operation issues](troubleshoot-query-data-and-operations.md#temp-table-scoping-with-autocommit).

### Transaction not committed

For data changes that don't persist after the connection closes, see [Troubleshoot query, data, and operation issues](troubleshoot-query-data-and-operations.md#transaction-not-committed).

### Deadlock errors

For SQLSTATE `40001`, retry guidance, and recurring deadlock analysis, see [Troubleshoot query, data, and operation issues](troubleshoot-query-data-and-operations.md#deadlock-errors).

## Bulk load issues

### Constraint violations during bulkcopy

For primary key, unique, check, or foreign key violations during bulk copy, see [Troubleshoot query, data, and operation issues](troubleshoot-query-data-and-operations.md#constraint-violations-during-bulkcopy).

### Column mapping errors

For bulk copy column count and column order mismatches, see [Troubleshoot query, data, and operation issues](troubleshoot-query-data-and-operations.md#column-mapping-errors).

### Type mismatches during bulkcopy

For truncated, rounded, or incorrect values after bulk copy, see [Troubleshoot query, data, and operation issues](troubleshoot-query-data-and-operations.md#type-mismatches-during-bulkcopy).

## NumPy type binding failures

For parameter binding failures with NumPy integer or float types, see [Troubleshoot query, data, and operation issues](troubleshoot-query-data-and-operations.md#numpy-type-binding-failures).

## Bulkcopy with temp tables

For `Invalid object name` errors when you use `bulkcopy()` with a session temp table, see [Troubleshoot query, data, and operation issues](troubleshoot-query-data-and-operations.md#bulk-copy-with-temp-tables).

## Container and CI issues

### Missing system libraries on Linux

For missing `libltdl` or Kerberos libraries in Linux environments, see [Troubleshoot installation and connection issues](troubleshoot-installation-connection.md#missing-system-libraries-on-linux).

### macOS SSL errors after install

For SSL-related errors on macOS, including Apple silicon, see [Troubleshoot installation and connection issues](troubleshoot-installation-connection.md#macos-ssl-errors-after-install).

## Diagnostic tools

### Enable driver logging

Use `mssql_python.setup_logging()` to enable DEBUG logging. The driver logs SQL statements, parameters, internal ODBC operations, and connection state changes.

```python
import mssql_python

# Enable logging to file (default)
mssql_python.setup_logging()

# Output to stdout (useful for CI/CD and containers)
mssql_python.setup_logging(output="stdout")

# Output to both file and stdout
mssql_python.setup_logging(output="both")

# Custom log file path (must use .txt, .log, or .csv extension)
mssql_python.setup_logging(log_file_path="/var/log/myapp/mssql.log")
```

Log files use CSV format and rotate automatically at 512 MB with five backups. The driver sanitizes sensitive data such as passwords and access tokens in log output.

To add application entries to the driver log, use `driver_logger`:

```python
import mssql_python
from mssql_python.logging import driver_logger

mssql_python.setup_logging()

driver_logger.debug("[App] Starting data processing")
driver_logger.error("[App] Failed to process record")
```

> [!CAUTION]
> Logging has performance overhead. Enable it only when you troubleshoot an issue. Don't enable it in production by default.

### Get driver information

Retrieve the driver version and server details from an active connection:

```python
import mssql_python

conn = mssql_python.connect(connection_string)

print(f"Version: {mssql_python.__version__}")
print(f"Server name: {conn.getinfo(mssql_python.SQL_SERVER_NAME)}")
print(f"Database name: {conn.getinfo(mssql_python.SQL_DATABASE_NAME)}")
```

### Check connection state

Run a lightweight query to test whether a connection is still open:

```python
import mssql_python

try:
    cursor = conn.cursor()
    cursor.execute("SELECT 1")
    print("Connection is open")
except mssql_python.Error:
    print("Connection is closed or broken")
```

## Quick reference: Common errors

| Error | SQLSTATE | Common cause | Troubleshooting |
| --- | --- | --- | --- |
| Client unable to establish connection | `08001` | Server unreachable | [Unable to connect to server](troubleshoot-installation-connection.md#unable-to-connect-to-server) |
| Login failed | `28000` | Incorrect credentials | [Login failed](troubleshoot-installation-connection.md#login-failed) |
| Timeout expired | `HYT00` or `HYT01` | Slow network | [Connection timeout](troubleshoot-installation-connection.md#connection-timeout) |
| Invalid object name | `42S02` | Incorrect table or schema | [Table or object not found](troubleshoot-query-data-and-operations.md#table-or-object-not-found) |
| Syntax error | `42000` | SQL error | [Syntax error](troubleshoot-query-data-and-operations.md#syntax-error) |
| Constraint violation | `23000` | Foreign key or primary key violation | [Constraint violations during bulkcopy](troubleshoot-query-data-and-operations.md#constraint-violations-during-bulkcopy) |
| Deadlock | `40001` | Lock contention | [Deadlock errors](troubleshoot-query-data-and-operations.md#deadlock-errors) |

## Related content

- [Error handling and SQLSTATE codes for mssql-python](error-handling.md)
- [Connection strings for mssql-python](connection-strings.md)
- [Connection pooling with mssql-python](connection-pooling.md)
- [GitHub issues](https://github.com/microsoft/mssql-python/issues)
