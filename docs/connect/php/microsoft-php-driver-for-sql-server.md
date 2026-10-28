---
title: Microsoft Drivers for PHP for SQL Server
description: The Microsoft Drivers for PHP for SQL Server are PHP extensions for connecting PHP applications to Microsoft SQL Server, Azure SQL Database, Azure SQL Managed Instance, and SQL database in Microsoft Fabric.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, sumitsar, jathakkar
ms.date: 10/20/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: overview
ai-usage: ai-assisted
---
# Microsoft Drivers for PHP for SQL Server

[!INCLUDE [Driver_PHP_Download](../../includes/driver_php_download.md)]

The Microsoft Drivers for PHP for SQL Server are PHP extensions that you can use to read and write data in the [Microsoft SQL Database Engine](../../database-engine/sql-database-engine.md) from PHP scripts. The two extensions use the same [Microsoft ODBC Driver for SQL Server](../odbc/microsoft-odbc-driver-for-sql-server.md), but they offer different PHP APIs and driver-specific options:

- **SQLSRV** exposes a procedural API (`sqlsrv_*` functions) tailored to SQL Server features.
- **PDO_SQLSRV** implements the [PHP Data Objects (PDO)](https://www.php.net/manual/en/book.pdo.php) interface. You can keep the PDO calls in existing database code, but review its queries and driver-specific settings before switching databases.

Both drivers connect to Azure SQL Database, SQL database in Microsoft Fabric, Azure SQL Managed Instance, and [all supported versions and editions](microsoft-php-drivers-for-sql-server-support-matrix.md#sql-server-version-certified-compatibility) of SQL Server, including Express editions. Both drivers accept PHP streams as input parameters. SQLSRV supports retrieving large binary and character values as streams. Don't assume PDO_SQLSRV has the same output-streaming behavior.

## Choose your starting point

- To verify your installation and run a parameterized query by using SQLSRV or PDO_SQLSRV, start with [Quickstart: Connect and query with the Microsoft Drivers for PHP](getting-started-with-the-php-sql-driver.md).
- To install the driver on Linux or macOS, start with the [Installation tutorial for Linux and macOS](installation-tutorial-linux-mac.md) and [Download the Microsoft Drivers for PHP for SQL Server](download-drivers-php-sql-server.md).
- To connect to Azure SQL with passwordless authentication, start with [Connect using Microsoft Entra authentication](azure-active-directory.md) and [Connection options](connection-options.md).
- To make an existing app resilient to transient failures, go to [Idle connection resiliency](connection-resiliency.md).
- To decide between SQLSRV and PDO_SQLSRV, go to [Overview of the Microsoft Drivers for PHP for SQL Server](overview-of-the-php-sql-driver.md) and [Comparing execution functions](comparing-execution-functions.md).
- To diagnose an install, connection, or query problem, go to [Troubleshooting](troubleshooting-php-sql-driver.md), [Handling errors and warnings](handling-errors-and-warnings.md), and [Logging activity](logging-activity.md).
- To make an existing app faster, go to [Performance tuning](performance-tuning-php-sql-driver.md).

## Production baseline for Azure SQL

This PDO_SQLSRV example reads the server and database from environment variables and connects with a managed identity. It builds a data source name (DSN) and enables Transport Layer Security (TLS) with certificate validation. Read these values from trusted application configuration, not web request parameters. Run the program on a host with a managed-identity endpoint after granting the identity access to the database.

The helper functions make up to three attempts to connect or run the read-only query when a listed error occurs. They wait longer between attempts, a retry pattern called *exponential backoff*. If a query error requires a new connection, the query helper throws the exception for the calling code to handle.

Use a [supported PHP version](system-requirements-for-the-php-sql-driver.md), the PDO_SQLSRV extension, and Microsoft ODBC Driver 18 for SQL Server. For supported authentication values, see [Connect using Microsoft Entra authentication](azure-active-directory.md). Don't reuse the query helper for writes or multistatement transactions without designing an application-specific retry policy: an error doesn't always tell you whether a write committed.

```php
<?php
declare(strict_types=1);

// Transient errors that require a fresh connection to recover. SQLSTATE values
// starting with '08' cover ODBC connection-established and connection-broken
// states (for example, 08001, 08S01).
const CONNECT_RETRY_SQLSTATE_PREFIX = '08';

// SQL Server error codes that are transient regardless of when they surface:
// 1205 (deadlock victim), 1222 (lock request timeout), and the Azure SQL
// throttling, mid-query failover, and "database not currently available"
// codes that arrive with SQLSTATE HY000.
const TRANSIENT_SERVER_ERROR_CODES = [1205, 1222, 40501, 40613, 40197, 10928, 10929, 49918];

// This read helper retries only the listed errors on the same connection.
// Other errors propagate; the caller must decide whether to reconnect.
const RETRYABLE_READ_ERROR_CODES = [1205, 1222, 40501, 10928, 10929];

/**
 * Open a connection, retrying transient failures with exponential backoff.
 */
function connectWithRetry(string $dsn, array $options, int $maxAttempts = 3): PDO
{
    for ($attempt = 1; $attempt <= $maxAttempts; $attempt++) {
        try {
            $pdo = new PDO($dsn, null, null, $options);
            error_log(sprintf('connected on attempt %d/%d', $attempt, $maxAttempts));
            return $pdo;
        } catch (PDOException $e) {
            $sqlstate = (string) $e->getCode();
            $driverCode = isset($e->errorInfo[1]) ? (int) $e->errorInfo[1] : 0;
            $isTransient = str_starts_with($sqlstate, CONNECT_RETRY_SQLSTATE_PREFIX)
                || in_array($driverCode, TRANSIENT_SERVER_ERROR_CODES, true);
            if (!$isTransient || $attempt === $maxAttempts) {
                error_log(sprintf('connect failed on attempt %d/%d: %s', $attempt, $maxAttempts, $e->getMessage()));
                throw $e;
            }
            $delay = 2 ** ($attempt - 1);
            error_log(sprintf('connect attempt %d hit transient %s/%d; retrying in %d seconds', $attempt, $sqlstate, $driverCode, $delay));
            sleep($delay);
        }
    }
    throw new RuntimeException('connectWithRetry exhausted retries');
}

/**
 * Retry the standalone read-only query for errors that allow connection reuse.
 */
function queryWithRetry(PDO $pdo, string $sql, array $params = [], int $maxAttempts = 3): PDOStatement
{
    for ($attempt = 1; $attempt <= $maxAttempts; $attempt++) {
        try {
            $stmt = $pdo->prepare($sql);
            $stmt->execute($params);
            return $stmt;
        } catch (PDOException $e) {
            $driverCode = isset($e->errorInfo[1]) ? (int) $e->errorInfo[1] : 0;
            $isTransient = in_array($driverCode, RETRYABLE_READ_ERROR_CODES, true);
            if (!$isTransient || $attempt === $maxAttempts) {
                error_log(sprintf('query failed on attempt %d/%d: %s', $attempt, $maxAttempts, $e->getMessage()));
                throw $e;
            }
            $delay = 2 ** ($attempt - 1);
            error_log(sprintf('query attempt %d hit transient code %d; retrying in %d seconds', $attempt, $driverCode, $delay));
            sleep($delay);
        }
    }
    throw new RuntimeException('queryWithRetry exhausted retries');
}

// Load endpoint details from application configuration. In Azure App Service,
// these can come from app settings or Key Vault-backed settings.
$server = getenv('SQL_SERVER') ?: null;
$database = getenv('SQL_DATABASE') ?: null;

if ($server === null || $database === null) {
    throw new RuntimeException('Set SQL_SERVER and SQL_DATABASE in your application configuration.');
}
$databaseValue = '{' . str_replace('}', '}}', $database) . '}';

$dsn = sprintf(
    'sqlsrv:Driver={ODBC Driver 18 for SQL Server};Server=%s;Database=%s;'
    . 'Encrypt=true;TrustServerCertificate=false;'
    . 'LoginTimeout=90;Authentication=ActiveDirectoryMsi;'
    . 'ConnectRetryCount=5;ConnectRetryInterval=15;'
    . 'MultiSubnetFailover=true;',
    $server,
    $databaseValue
);

$options = [
    PDO::ATTR_ERRMODE               => PDO::ERRMODE_EXCEPTION,
    PDO::ATTR_DEFAULT_FETCH_MODE    => PDO::FETCH_ASSOC,
    PDO::ATTR_EMULATE_PREPARES      => false,
    PDO::SQLSRV_ATTR_QUERY_TIMEOUT  => 30,
];

$pdo = connectWithRetry($dsn, $options);
$stmt = queryWithRetry($pdo, 'SELECT name FROM sys.databases WHERE database_id > ? ORDER BY name', [1]);
foreach ($stmt as $row) {
    echo $row['name'], PHP_EOL;
}
```

Use these settings for Azure SQL Database failover groups and Azure SQL Managed Instance.

- `Driver={ODBC Driver 18 for SQL Server}` selects ODBC Driver 18 explicitly rather than relying on driver discovery. Install it before running the example.
- Set `SQL_DATABASE` to the actual database name. The program doubles closing braces and encloses the name in braces before placing it in the DSN. Escaping keeps special characters in the name from changing how the driver reads connection settings.
- `ConnectRetryCount` and `ConnectRetryInterval` control ODBC *idle connection resiliency*. The statement's query timeout bounds recovery during execution. With the sample's 30-second query timeout, ODBC might stop before exhausting all five reconnect attempts. `LoginTimeout=90` controls connection establishment, not that recovery budget. The application-level helper retries selected server errors; it doesn't retry a query timeout or replay partially fetched results.
- Enable driver logging alongside the program's `error_log()` calls. For PDO_SQLSRV, set `pdo_sqlsrv.log_severity` in `php.ini` or with `ini_set()` at runtime. For SQLSRV, configure `LogSeverity` and `LogSubsystems` with `sqlsrv_configure()`. For more information, see [Logging activity](logging-activity.md).

  ```ini
  ; php.ini - enable PDO_SQLSRV driver diagnostics alongside the application-level
  ; error_log() calls in the sample. Use 1 (errors) in production; -1 (all) is
  ; useful during triage but very chatty.
  [pdo_sqlsrv]
  pdo_sqlsrv.log_severity = 1
  ```

- For a **user-assigned** managed identity, pass the identity's ID as PDO's `$username` argument (`new PDO($dsn, $identityId, null, $options)`). Use the identity's **client ID** on [Azure App Service](/azure/app-service/overview-managed-identity) or [Azure Container Instance](/azure/container-instances/container-instances-managed-identity); otherwise, use its **object ID**. The PHP drivers inherit this behavior from the underlying Microsoft ODBC Driver for SQL Server; for more information, see [Using Microsoft Entra ID with the ODBC Driver](../odbc/using-azure-active-directory.md#example-connection-strings). Use the PDO constructor's username argument rather than embedding the identity in the DSN. Passing `null` as the user (as the sample does) selects the system-assigned managed identity of the Azure host. For SQLSRV (procedural), pass `UID` in the connection options array.
- Set `MultiSubnetFailover=true` when you connect to a failover-group listener, availability group listener, or failover cluster instance endpoint. Setting it improves connection performance for both single-subnet and multi-subnet availability group listeners. For more information, see [Support for High Availability, disaster recovery](php-driver-for-sql-server-support-for-high-availability-disaster-recovery.md).
- For read scale-out or a readable secondary, add `ApplicationIntent=ReadOnly` to the DSN.
- Use the endpoint supplied by your SQL service, including its cloud-specific domain. Investigate certificate-name mismatches rather than disabling certificate validation. Use `HostNameInCertificate` only when you know the expected certificate name for that endpoint.
- ODBC acquires tokens for managed identity and service principal authentication. With `AccessToken`, your application acquires the token and passes it to the driver; ODBC doesn't acquire it for you. For more information, see [Using Microsoft Entra ID with the ODBC Driver](../odbc/using-azure-active-directory.md).
- Keep connection information outside your code. Store it in your application's configuration system, and use Azure Key Vault for secrets.
- With SQLSRV, set connection options in the array passed to `sqlsrv_connect()`. Check for a `false` return and inspect `sqlsrv_errors()` before deciding whether to retry. Set statement timeouts separately with `QueryTimeout`. [Idle connection resiliency](connection-resiliency.md) demonstrates ODBC reconnection, not an application-level retry loop.
- The retry helpers read `$e->errorInfo[1]` guarded by `isset()`. [`PDOException::$errorInfo`](https://www.php.net/manual/en/class.pdoexception.php) is declared as `?array` and defaults to `null`, so the defensive check falls back to a driver code of `0` and lets the SQLSTATE `08` prefix decide whether to retry.

For more information about each part of this configuration, see:

- [Connection options](connection-options.md)
- [Connect using Microsoft Entra authentication](azure-active-directory.md)
- [Idle connection resiliency](connection-resiliency.md)
- [Connecting to Microsoft Azure SQL Database](connecting-to-microsoft-azure-sql-database.md)
- [Support for High Availability, disaster recovery](php-driver-for-sql-server-support-for-high-availability-disaster-recovery.md)

For the catalog of Azure SQL transient errors, see [Troubleshoot transient connection errors](/azure/azure-sql/database/troubleshoot-common-connectivity-issues).

## Key features

- **Two PHP APIs**: SQLSRV provides procedural functions. PDO_SQLSRV implements PDO. Install either extension or both.
- **Wide platform support**: Runs on Windows, Linux, and macOS with supported PHP versions.
- **Encrypted connections**: TLS-encrypted connections via `Encrypt=true`, with server certificate validation controlled by `TrustServerCertificate`.
- **Microsoft Entra ID authentication**: Managed identity, service principal, and externally acquired access tokens through the underlying Microsoft ODBC Driver for SQL Server. Managed identity is passwordless; `ActiveDirectoryServicePrincipal` uses a client secret.
- **Always Encrypted**: Encrypt sensitive columns on the client, with secure enclaves for supported operations on encrypted data.
- **Connection resiliency**: Built-in idle connection retries with `ConnectRetryCount` and `ConnectRetryInterval`.
- **PHP streams**: Send large binary and character values from stream parameters with either API. Use SQLSRV for streamed retrieval.
- **Rich SQL Server data type support**: **datetimeoffset**, table-valued parameters, **nvarchar**, and Unicode with `PDO::SQLSRV_ENCODING_UTF8`.

## Get started

| Article | Description |
| --- | --- |
| [System requirements](system-requirements-for-the-php-sql-driver.md) | Supported PHP, operating system, and SQL Server versions. |
| [Support matrix](microsoft-php-drivers-for-sql-server-support-matrix.md) | Detailed compatibility matrix for PHP driver releases. |
| [Download the Microsoft Drivers for PHP for SQL Server](download-drivers-php-sql-server.md) | Download links and release artifacts. |
| [Installation tutorial for Linux and macOS](installation-tutorial-linux-mac.md) | Install the driver and its ODBC prerequisites on Linux and macOS. |
| [Loading the drivers](loading-the-php-sql-driver.md) | Enable the extensions in `php.ini`. |
| [Quickstart: Connect and query with the Microsoft Drivers for PHP](getting-started-with-the-php-sql-driver.md) | Verify the extensions, configure a connection, and run the same parameterized query with SQLSRV or PDO_SQLSRV. |
| [Overview of the PHP SQL driver](overview-of-the-php-sql-driver.md) | What's in the package, and when to choose SQLSRV or PDO_SQLSRV. |

## Configure and connect

| Article | Description |
| --- | --- |
| [Connecting to the server](connecting-to-the-server.md) | Open a connection to a SQL Server instance from PHP. |
| [Connection options](connection-options.md) | Full reference for connection keywords, defaults, and how to set them. |
| [Connecting to Microsoft Azure SQL Database](connecting-to-microsoft-azure-sql-database.md) | Connect a PHP application to Azure SQL Database. |
| [Connect on a specified port](how-to-connect-on-a-specified-port.md) | Target a nondefault TCP port. |
| [Connection pooling](connection-pooling-microsoft-drivers-for-php-for-sql-server.md) | Reuse ODBC connections across PHP requests. |
| [Disable Multiple Active Result Sets (MARS)](how-to-disable-multiple-active-resultsets-mars.md) | Turn off MARS for compatibility. |
| [Support for LocalDB](php-driver-for-sql-server-support-for-localdb.md) | Connect to a SQL Server LocalDB instance. |
| [Support for High Availability, disaster recovery](php-driver-for-sql-server-support-for-high-availability-disaster-recovery.md) | Availability group listeners and multi-subnet failover. |
| [Idle connection resiliency](connection-resiliency.md) | Automatic reconnect for broken idle connections. |

## Authenticate

| Article | Description |
| --- | --- |
| [Connect using Microsoft Entra authentication](azure-active-directory.md) | Managed identity, service principal, access token, and password flows. |
| [Connect using SQL Server authentication](how-to-connect-using-sql-server-authentication.md) | Use a SQL login with a username and password. |
| [Connect using Windows authentication](how-to-connect-using-windows-authentication.md) | Use integrated authentication on Windows or Kerberos on Linux and macOS. |

## Secure

| Article | Description |
| --- | --- |
| [Security considerations](security-considerations-for-php-sql-driver.md) | Threat model and defense-in-depth guidance for PHP applications. |
| [Always Encrypted with the PHP drivers](using-always-encrypted-php-drivers.md) | Configure client-side encryption for sensitive columns. |
| [Always Encrypted with secure enclaves](always-encrypted-secure-enclaves.md) | Enable rich operations on encrypted columns with secure enclaves. |

## Retrieve and update data

| Article | Description |
| --- | --- |
| [Programming guide](programming-guide-for-php-sql-driver.md) | Find programming tasks for both drivers. |
| [Comparing execution functions](comparing-execution-functions.md) | Choose the right execution function for your workload. |
| [Direct and prepared statement execution (PDO_SQLSRV)](direct-statement-execution-prepared-statement-execution-pdo-sqlsrv-driver.md) | When to use direct execution versus prepared statements. |
| [Retrieving data](retrieving-data.md) | Fetch rows, columns, and streaming values. |
| [Updating data](updating-data-microsoft-drivers-for-php-for-sql-server.md) | Insert, update, and delete rows. |
| [Perform parameterized queries](how-to-perform-parameterized-queries.md) | Bind parameters to protect against SQL injection. |
| [Send data as a stream](how-to-send-data-as-a-stream.md) | Stream large binary and character values into SQL Server. |
| [Perform transactions](how-to-perform-transactions.md) | Commit or roll back a group of statements together. |
| [Use table-valued parameters](use-table-valued-parameters.md) | Pass a **TABLE** parameter to a stored procedure. |
| [Specify a cursor type and select rows](specifying-a-cursor-type-and-selecting-rows.md) | Choose forward-only, static, dynamic, or keyset cursors. |

## Data types

| Article | Description |
| --- | --- |
| [Converting data types](converting-data-types.md) | How the driver maps PHP types to SQL Server types. |
| [Default SQL Server data types](default-sql-server-data-types.md) | Default SQL Server type for each PHP value. |
| [Default PHP data types](default-php-data-types.md) | Default PHP type for each SQL Server column type. |
| [Specify SQL Server data types (SQLSRV)](how-to-specify-sql-server-data-types-when-using-the-sqlsrv-driver.md) | Override the SQL Server type when binding parameters. |
| [Specify PHP data types](how-to-specify-php-data-types.md) | Override the PHP type when fetching. |
| [Send and retrieve UTF-8 data](how-to-send-and-retrieve-utf-8-data-using-built-in-utf-8-support.md) | Set SQLSRV's `CharacterSet` option or PDO_SQLSRV's `PDO::SQLSRV_ATTR_ENCODING` attribute. |
| [Send and retrieve ASCII data on Linux and macOS](how-to-send-and-retrieve-ascii-data-in-linux-mac.md) | Handle ASCII round-trips on non-Windows hosts. |
| [Format decimals and money (SQLSRV)](formatting-decimals-sqlsrv-driver.md) | Format **decimal** and **money** columns with the SQLSRV driver. |
| [Format decimals and money (PDO_SQLSRV)](formatting-decimals-pdo-sqlsrv-driver.md) | Format **decimal** and **money** columns with the PDO_SQLSRV driver. |
| [Non-system locale settings](non-system-locale-settings.md) | Localized decimal separators and other locale considerations. |

## Errors and diagnostics

| Article | Description |
| --- | --- |
| [Handling errors and warnings](handling-errors-and-warnings.md) | Error and warning handling with both drivers. |
| [Configure error and warning handling (SQLSRV)](how-to-configure-error-and-warning-handling-using-the-sqlsrv-driver.md) | Tune how the SQLSRV driver reports errors and warnings. |
| [Handle errors and warnings (SQLSRV)](how-to-handle-errors-and-warnings-using-the-sqlsrv-driver.md) | Inspect errors returned by SQLSRV functions. |
| [Logging activity](logging-activity.md) | Enable driver logging for diagnostic capture. |

## Deploy and operate

| Article | Description |
| --- | --- |
| [Performance tuning](performance-tuning-php-sql-driver.md) | Connection management, batching, prepared statements, cursors, memory, and server-side monitoring. |
| [Troubleshooting](troubleshooting-php-sql-driver.md) | Diagnose common install, connection, query, data type, transaction, and container problems. |

## Reference content

| Article | Description |
| --- | --- |
| [SQLSRV driver API reference](sqlsrv-driver-api-reference.md) | All `sqlsrv_*` functions, parameters, and return values. |
| [PDO_SQLSRV driver reference](pdo-sqlsrv-driver-reference.md) | PDO and PDOStatement methods supported by the PDO_SQLSRV driver. |
| [Constants](constants-microsoft-drivers-for-php-for-sql-server.md) | Constants exposed by the drivers, including type and encoding constants. |

## Related tasks

| Article | Description |
| --- | --- |
| [Release notes](release-notes-php-sql-driver.md) | Per-version history with new features, bug fixes, platform support changes, and download links. |
| [About code samples in the documentation](about-code-examples-in-the-documentation.md) | Conventions used by the code samples in this section. |
| [Code samples for the PHP SQL driver](code-samples-for-php-sql-driver.md) | End-to-end example applications for SQLSRV and PDO_SQLSRV. |
| [Support resources](support-resources-for-the-php-sql-driver.md) | Community and support channels. |

## Request a feature

To request a feature, open an issue in the [Microsoft Drivers for PHP for SQL Server GitHub repository](https://github.com/microsoft/msphpsql/issues/new/choose).

## Related content

- [Microsoft Drivers for PHP for SQL Server on GitHub](https://github.com/microsoft/msphpsql)
- [Microsoft ODBC Driver for SQL Server](../odbc/microsoft-odbc-driver-for-sql-server.md)
- [Microsoft SQL drivers and frameworks](../sql-connection-libraries.md)
