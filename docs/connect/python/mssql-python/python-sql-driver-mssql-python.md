---
title: "Microsoft Python Driver for SQL Server - mssql-python"
description: mssql-python is Microsoft's Python driver for SQL Server, Azure SQL Database, Azure SQL Managed Instance, and SQL database in Microsoft Fabric.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: vanto, randolphwest
ms.date: 07/13/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: get-started
ms.custom:
  - ignite-2025
ai-usage: ai-assisted
---

# Microsoft Python Driver for SQL Server - mssql-python

`mssql-python` is Microsoft's Python driver for SQL Server, Azure SQL Database, Azure SQL Managed Instance, and SQL database in Microsoft Fabric. It uses Direct Database Connectivity (DDBC), so you can connect without installing an external driver manager. The driver supports Python 3.10 or later and complies with the [Python Database API Specification 2.0](https://peps.python.org/pep-0249/) while adding Python-friendly improvements for day-to-day development.

## Choose your starting point

- To get a local SQL Server sample running quickly, start with [Quickstart: Connect with the mssql-python driver](python-sql-driver-mssql-python-quickstart.md).
- To connect to Azure SQL with passwordless authentication, start with [Microsoft Entra authentication](entra-authentication.md) and [Connection strings](connection-strings.md).
- To explore data interactively, start with [Connect from a Jupyter Notebook](python-sql-driver-mssql-python-connect-jupyter-notebook.md) or [Rapid prototyping](python-sql-driver-mssql-python-rapid-prototyping-quickstart.md).
- To move large volumes of data efficiently, go to [Bulk copy operations](bulk-copy.md) or the [Bulk copy quickstart](python-sql-driver-mssql-python-bulk-copy-quickstart.md).
- To migrate from another driver, go to [Migrate from pyodbc](migrate-from-pyodbc.md), [Migrate from pymssql](migrate-from-pymssql.md), [Migrate from SQLite](migrate-from-sqlite.md), or [Migrate from PostgreSQL](migrate-from-postgresql.md).

## Production baseline for Azure SQL

Use this sample as a starting point for a production-oriented Azure SQL connection. It reads configuration from the environment, authenticates with managed identity, and enables Tabular Data Stream (TDS) 8.0 encryption. It also sets login and per-statement query timeouts, retries transient failures with exponential backoff (a fresh connection for connection errors, the same connection for query errors like deadlocks), logs outcomes, and relies on context managers to release resources.

The `ConnectRetryCount` and `ConnectRetryInterval` keywords in the connection string enable SQL Server *idle connection resiliency*: the driver transparently reconnects a dropped idle connection. That's distinct from the application-level retry in this sample, which retries a *query* that fails with a transient error such as a deadlock or query timeout. The two are complementary, so keep both.

```python
import logging
import os
import time

import mssql_python

logging.basicConfig(
    level=logging.INFO,
    format="%(asctime)s %(levelname)s %(name)s %(message)s",
)
logger = logging.getLogger("app")

# Transient errors that require a fresh connection to recover.
CONNECT_RETRY_ERRORS = frozenset({
    "Timeout expired",
    "Connection timeout expired",
    "Client unable to establish connection",
    "Communication link failure",
    "Connection failure during transaction",
})

# Transient errors that leave the connection usable, such as a deadlock victim
# or a query timeout, so retry on the same connection.
QUERY_RETRY_ERRORS = frozenset({
    "Serialization failure",
    "Timeout expired",
})


def connect_with_retry(conn_str: str, max_attempts: int = 3, login_timeout_s: int = 5) -> mssql_python.Connection:
    """Open a connection, retrying transient failures with exponential backoff."""
    for attempt in range(1, max_attempts + 1):
        try:
            conn = mssql_python.connect(
                conn_str,
                attrs_before={mssql_python.SQL_ATTR_LOGIN_TIMEOUT: login_timeout_s},
            )
            logger.info("connected on attempt %d/%d", attempt, max_attempts)
            return conn
        except mssql_python.OperationalError as exc:
            if exc.driver_error not in CONNECT_RETRY_ERRORS or attempt == max_attempts:
                logger.error("connect failed on attempt %d/%d: %s", attempt, max_attempts, exc.driver_error)
                raise
            delay = 2 ** (attempt - 1)  # 1s, 2s, 4s
            logger.warning(
                "connect attempt %d/%d hit transient error %r; retrying in %ds",
                attempt, max_attempts, exc.driver_error, delay,
            )
            time.sleep(delay)


def execute_with_retry(
    conn: mssql_python.Connection,
    sql: str,
    *params,
    max_attempts: int = 3,
    query_timeout_s: int = 10,
) -> mssql_python.Cursor:
    """Run sql on an open connection and return the ready-to-fetch cursor.

    Retries errors that leave the connection usable so callers don't wrap each
    query in its own function. Pass query values as parameters. Retry only
    idempotent statements; wrap writes in an explicit transaction.
    """
    for attempt in range(1, max_attempts + 1):
        cursor = mssql_python.Cursor(conn, timeout=query_timeout_s)
        try:
            cursor.execute(sql, *params)
            if attempt > 1:
                logger.info("query succeeded on attempt %d/%d", attempt, max_attempts)
            return cursor
        except mssql_python.OperationalError as exc:
            cursor.close()
            if exc.driver_error not in QUERY_RETRY_ERRORS or attempt == max_attempts:
                logger.error("query failed on attempt %d/%d: %s", attempt, max_attempts, exc.driver_error)
                raise
            delay = 2 ** (attempt - 1)  # 1s, 2s, 4s
            logger.warning(
                "query attempt %d/%d hit transient error %r; retrying in %ds",
                attempt, max_attempts, exc.driver_error, delay,
            )
            time.sleep(delay)
    raise RuntimeError("unreachable: the retry loop exits by return or raise")


def main() -> None:
    # Read configuration from the environment; never hard-code secrets.
    server = os.environ["SQL_SERVER"]      # for example, myserver.database.windows.net
    database = os.environ["SQL_DATABASE"]  # for example, AdventureWorks
    client_id = os.getenv("AZURE_CLIENT_ID")  # set for a user-assigned managed identity

    # Authenticate with the workload's managed identity over TDS 8.0 encryption.
    # ConnectRetryCount/ConnectRetryInterval transparently reconnect a dropped
    # idle connection; they don't replay a failed query.
    conn_str = (
        f"Server={server};"
        f"Database={database};"
        "Authentication=ActiveDirectoryMsi;"
        "Encrypt=strict;"
        "ConnectRetryCount=3;"
        "ConnectRetryInterval=10;"
    )
    if client_id:
        conn_str += f"UID={client_id};"

    query = """
        SELECT TOP 10
            p.BusinessEntityID,
            p.FirstName,
            p.LastName
        FROM Person.Person AS p
        ORDER BY p.BusinessEntityID;
    """

    try:
        # Context managers close the cursor and connection automatically.
        with connect_with_retry(conn_str) as conn:
            with execute_with_retry(conn, query) as cursor:
                for business_entity_id, first_name, last_name in cursor.fetchall():
                    print(f"{business_entity_id}\t{first_name}\t{last_name}")
    except mssql_python.Error:
        logger.exception("query failed")
        raise


if __name__ == "__main__":
    main()
```

For deeper guidance on each concern in this sample, see [Microsoft Entra authentication](entra-authentication.md), [Connection pooling](connection-pooling.md), [Encryption and certificates](encryption-certificates.md), [Retry logic](retry-logic.md), and [Error handling](error-handling.md).

## Key features

- **PEP 249 compliance**: Standard `connect`, `cursor`, `execute`, and `fetch*` interfaces, plus Pythonic extensions.
- **Direct Database Connectivity (DDBC)**: No external driver manager required. Install `mssql-python` and you're ready to connect.
- **Microsoft Entra ID authentication**: Built-in support for [authentication modes](entra-authentication.md), including managed identities and service principals.
- **SQL Server and Windows authentication**: SQL logins, Kerberos, and Windows single sign-on (SSO) on supported platforms.
- **Bulk copy**: [High-performance bulk insert](bulk-copy.md) for large data loads with native TDS protocol support.
- **Native data type support**: [JSON, XML, spatial, sparse columns, datetimeoffset, and decimal/money](data-type-mappings.md) with precise handling.
- **Apache Arrow integration**: [Zero-copy result sets](arrow-integration.md) for fast data interchange with pandas, Polars, and DuckDB.
- **Async patterns**: Use the driver with `asyncio`-based applications and FastAPI through ThreadPoolExecutor workarounds. See [Async patterns](asynchronous-patterns.md) for integration patterns.
- **TLS by default**: [TLS encryption and certificate validation](encryption-certificates.md) on by default (via ODBC Driver 18). TDS 8.0 encryption available when you set `Encrypt=strict`.

## Get started

| Article | Description |
| --- | --- |
| [Installation](installation.md) | Install `mssql-python` and verify your Python environment. |
| [Quickstart: Connect with mssql-python](python-sql-driver-mssql-python-quickstart.md) | Connect to a local or test SQL Server instance and run your first query. |
| [Quickstart: Connect from a Jupyter Notebook](python-sql-driver-mssql-python-connect-jupyter-notebook.md) | Use mssql-python inside a notebook for interactive data exploration. |
| [Quickstart: Bulk copy](python-sql-driver-mssql-python-bulk-copy-quickstart.md) | Move large data sets into SQL Server with the bulk copy API. |
| [Quickstart: Rapid prototyping](python-sql-driver-mssql-python-rapid-prototyping-quickstart.md) | Build small scripts and proofs of concept quickly. |
| [Quickstart: Repeatable deployments](python-sql-driver-mssql-python-repeatable-deployments-quickstart.md) | Package, configure, and ship Python applications that talk to SQL. |
| [Apache Arrow quickstart](python-sql-driver-mssql-python-arrow-quickstart.md) | Fetch query results as Apache Arrow tables for analytics workflows. |

## Configure and authenticate

| Article | Description |
| --- | --- |
| [Connection strings](connection-strings.md) | Connection string syntax, common keywords, and examples. |
| [Build connection strings programmatically](build-connection-strings.md) | Compose connection strings safely from configuration and secrets. |
| [Connection management](connection-management.md) | Open, reuse, and close connections cleanly. |
| [Connection pooling](connection-pooling.md) | Pool tuning, lifetimes, and reuse patterns. |
| [Encryption and certificates](encryption-certificates.md) | TLS encryption modes, certificate validation, and TDS 8.0. |
| [Microsoft Entra authentication](entra-authentication.md) | Passwordless authentication for Azure SQL with managed identity, service principal, interactive, and device code flows. |
| [Security best practices](security-best-practices.md) | Parameterization, secrets management, least privilege, and encryption. |
| [Availability groups](availability-groups.md) | Connect to Always On availability groups and read-only replicas. |

## Work with data

| Article | Description |
| --- | --- |
| [Executing queries](executing-queries.md) | `execute`, `executemany`, multi-statement batches, and result sets. |
| [Retrieving data](retrieving-data.md) | `fetchone`, `fetchmany`, `fetchall`, and streaming patterns. |
| [Parameterized queries](parameterized-queries.md) | Bind parameters safely to prevent SQL injection. |
| [Stored procedures](stored-procedures.md) | Call procedures, read output parameters, and process result sets. |
| [Cursor management](cursor-management.md) | Cursor lifetimes, scrolling, and arraysize tuning. |
| [Row objects](row-objects.md) | Access rows by index, name, or as mappings. |
| [Transaction management](transaction-management.md) | Commit, rollback, savepoints, and isolation levels. |
| [Pagination](pagination.md) | Keyset and offset pagination patterns over large result sets. |
| [Error handling](error-handling.md) | `mssql_python.Error`, `DatabaseError`, and SQL Server error structure. |
| [Retry logic](retry-logic.md) | Detect transient errors and retry with exponential backoff. |

## SQL Server data types and features

| Article | Description |
| --- | --- |
| [Data type mappings](data-type-mappings.md) | SQL Server-to-Python type table and conversion rules. |
| [Datetime handling](datetime-handling.md) | `datetime`, `datetime2`, `datetimeoffset`, and time zone considerations. |
| [Decimal and money types](decimal-money.md) | Exact numeric types and `decimal.Decimal` precision. |
| [String and Unicode data](string-unicode.md) | `varchar`, `nvarchar`, collations, and code pages. |
| [NULL handling](null-handling.md) | Three-valued logic, sentinels, and pandas interop. |
| [Binary data](binary-data.md) | `varbinary`, `image`, and streaming large objects. |
| [Custom type converters](custom-type-converters.md) | Register input and output converters for custom types. |
| [Bulk copy operations](bulk-copy.md) | High-throughput inserts with the bulk copy API. |
| [JSON data](json-data.md) | Store, query, and shred JSON with `FOR JSON` and `OPENJSON`. |
| [XML data](xml-data.md) | Work with the `xml` data type, XPath, and XQuery. |
| [Spatial data](spatial-data.md) | `geometry` and `geography` types from Python. |
| [Sparse columns](sparse-columns.md) | Sparse columns and column sets for wide tables. |
| [Schema discovery](schema-discovery.md) | Inspect databases, tables, columns, and indexes. |

## Integrate with Python tools and frameworks

| Article | Description |
| --- | --- |
| [Apache Arrow integration](arrow-integration.md) | Fetch results as Arrow tables for zero-copy analytics. |
| [pandas integration](pandas-integration.md) | Load query results into DataFrames and write them back. |
| [Polars integration](polars-integration.md) | Use Polars with mssql-python for columnar workloads. |
| [DuckDB integration](duckdb-integration.md) | Query SQL Server data alongside local DuckDB tables. |
| [FastAPI integration](fastapi-integration.md) | Wire mssql-python into FastAPI services. |
| [Flask integration](flask-integration.md) | Use mssql-python in Flask applications. |
| [Async patterns](asynchronous-patterns.md) | Combine mssql-python with `asyncio` and thread pools. |
| [Data access and analytics patterns](data-access-analytics-patterns.md) | Choose the right read path for cursor access, Arrow extraction, pandas, Polars, and DuckDB analytics over SQL data. |
| [Data loading and movement patterns](data-loading-movement-patterns.md) | Choose the right write path for row inserts, bulk copy, MERGE upserts, DataFrame loading, and CSV ingest. |

## Deploy and operate

| Article | Description |
| --- | --- |
| [Container and local development](container-local-development.md) | Set up Docker containers, devcontainers, and CI pipelines for Python applications that connect to SQL. |
| [Performance tuning](performance-tuning.md) | Pool tuning, prepared statements, batch sizes, and bulk copy. |
| [Troubleshooting](troubleshooting.md) | Common errors, logging, and certificate diagnostics. |
| [Module configuration](module-configuration.md) | Module-level settings, logging hooks, and feature flags. |

## Migrate to mssql-python

| Article | Description |
| --- | --- |
| [Migrate from pyodbc](migrate-from-pyodbc.md) | Map pyodbc APIs and connection strings to mssql-python. |
| [Migrate from pymssql](migrate-from-pymssql.md) | Replace pymssql with mssql-python while preserving behavior. |
| [Migrate from SQLite](migrate-from-sqlite.md) | Move local SQLite workloads to SQL Server or Azure SQL. |
| [Migrate from PostgreSQL](migrate-from-postgresql.md) | One-stop guide for Python developers moving from PostgreSQL to SQL Server with mssql-python. |

## Reference

| Article | Description |
| --- | --- |
| [Support lifecycle](support-lifecycle.md) | Supported Python and SQL Server versions, and update cadence. |
| [What's new](whats-new.md) | Version history and release highlights. |

## Related content

- [mssql-python on GitHub](https://github.com/microsoft/mssql-python)
- [mssql-python on PyPI](https://pypi.org/project/mssql-python/)
- [Release notes](https://github.com/microsoft/mssql-python/releases)
- [Roadmap](https://github.com/microsoft/mssql-python/blob/main/ROADMAP.md)
- [Azure Python Developer Center](https://azure.microsoft.com/develop/python/)
- [Python Database API Specification 2.0](https://peps.python.org/pep-0249/)
