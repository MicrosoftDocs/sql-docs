---
title: "Microsoft Drivers for PHP for SQL Server"
description: The Microsoft Drivers for PHP for SQL Server are PHP extensions for connecting PHP applications to Microsoft SQL Server, Azure SQL Database, Azure SQL Managed Instance, and SQL database in Microsoft Fabric.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, sumitsar, jathakkar
ms.date: 07/22/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: overview
ai-usage: ai-assisted
---
# Microsoft Drivers for PHP for SQL Server

[!INCLUDE [Driver_PHP_Download](../../includes/driver_php_download.md)]

The Microsoft Drivers for PHP for SQL Server are PHP extensions that let you read and write data in the [Microsoft SQL Database Engine](../../database-engine/sql-database-engine.md) from PHP scripts. The package ships two drivers that wrap the same [Microsoft ODBC Driver for SQL Server](../odbc/microsoft-odbc-driver-for-sql-server.md) and share the same connection options, so you can pick the API that fits your codebase:

- **SQLSRV** exposes a procedural API (`sqlsrv_*` functions) tailored to SQL Server features.
- **PDO_SQLSRV** implements the [PHP Data Objects (PDO)](https://www.php.net/manual/en/book.pdo.php) interface, so code that already uses PDO for other databases can target SQL Server with minimal changes.

Both drivers connect to Azure SQL Database, SQL database in Microsoft Fabric, Azure SQL Managed Instance, and [all supported versions and editions](microsoft-php-drivers-for-sql-server-support-matrix.md#sql-server-version-certified-compatibility) of SQL Server (including Express editions). They use PHP streams to move large binary and character values without loading them entirely into memory.

## Choose your starting point

| Goal | Start here |
| --- | --- |
| Set up a PHP development environment and run your first query | [Step 1: Configure development environment](step-1-configure-development-environment-for-php-development.md), then [Step 2: Create a SQL database](step-2-create-a-sql-database-for-php-development.md) and [Step 3: Proof of concept connecting to SQL using PHP](step-3-proof-of-concept-connecting-to-sql-using-php.md). |
| Install the driver on Linux or macOS | [Installation tutorial for Linux and macOS](installation-tutorial-linux-mac.md) and [Download the Microsoft Drivers for PHP for SQL Server](download-drivers-php-sql-server.md). |
| Connect to Azure SQL with passwordless authentication | [Connect using Microsoft Entra authentication](azure-active-directory.md) and [Connection options](connection-options.md). |
| Make an existing app resilient to transient failures | [Idle connection resiliency](connection-resiliency.md) and [Step 4: Connect resiliently to SQL with PHP](step-4-connect-resiliently-to-sql-with-php.md). |
| Decide between SQLSRV and PDO_SQLSRV | [Overview of the Microsoft Drivers for PHP for SQL Server](overview-of-the-php-sql-driver.md) and [Comparing execution functions](comparing-execution-functions.md). |
| Diagnose an install, connection, or query problem | [Troubleshooting](troubleshooting-php-sql-driver.md), [Handling errors and warnings](handling-errors-and-warnings.md), and [Logging activity](logging-activity.md). |
| Make an existing app faster | [Performance tuning](performance-tuning-php-sql-driver.md). |

## Quick connect

The following snippet is the shortest end-to-end connection that a working PHP install can run against SQL Server or Azure SQL. Use it to confirm your driver, ODBC dependencies, and network path are wired up before you move on to the production baseline in the next section.

```php
<?php
$server   = getenv('SQL_SERVER')   ?: 'localhost';
$database = getenv('SQL_DATABASE') ?: 'master';
$user     = getenv('SQL_USER');
$password = getenv('SQL_PASSWORD');

$dsn = "sqlsrv:Driver={ODBC Driver 18 for SQL Server};Server=$server;Database=$database;Encrypt=true";
$pdo = new PDO($dsn, $user, $password, [
    PDO::ATTR_ERRMODE => PDO::ERRMODE_EXCEPTION,
]);

foreach ($pdo->query('SELECT @@VERSION AS version') as $row) {
    echo $row['version'], PHP_EOL;
}
```

For a passwordless connection against Azure SQL, add `Authentication=ActiveDirectoryMsi` (managed identity) or another `Authentication` value to the DSN and drop the `$user`/`$password` arguments. The production baseline that follows expands the same pattern with retries, timeouts, and diagnostics.

For a local SQL Server that uses a self-signed certificate, `Encrypt=true` fails validation. Add `TrustServerCertificate=true` for local development only. See [TLS certificate errors](troubleshooting-php-sql-driver.md#tls-certificate-errors) for the production alternatives.

## Production baseline for Azure SQL

Use this snippet as a starting point for a production-oriented Azure SQL connection with the PDO_SQLSRV driver. It reads the server and database from environment variables (Azure App Service app settings, for example), authenticates with a managed identity, enables Transport Layer Security (TLS) with server certificate validation, sets a login timeout that covers a cold-start failover, and sets `ConnectRetryCount` and `ConnectRetryInterval` for SQL Server idle connection resiliency. The application-level `connectWithRetry` and `queryWithRetry` helpers wrap both the initial connect and each statement with a bounded exponential backoff, and separate transient connection errors (which require a fresh connection) from transient query errors (which reuse the same connection).

Requires PHP 8.0 and later versions, the PDO_SQLSRV extension, and Microsoft ODBC Driver for SQL Server 17.3.1.1 and later versions for `Authentication=ActiveDirectoryMsi`. For the full list of supported `Authentication` values, see [Connect using Microsoft Entra authentication](azure-active-directory.md).

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
            $delay = 2 ** ($attempt - 1); // 1, 2, 4 seconds
            error_log(sprintf('connect attempt %d hit transient %s/%d; retrying in %d seconds', $attempt, $sqlstate, $driverCode, $delay));
            sleep($delay);
        }
    }
    throw new RuntimeException('connectWithRetry exhausted retries');
}

/**
 * Run a parameterized query, retrying transient statement failures on the same
 * connection. Deadlocks (1205) roll back the transaction before the driver sees
 * the error, so rerunning a single statement is safe. If the statement was part
 * of a multistatement transaction, wrap the whole transaction in your own retry
 * loop so earlier statements replay too.
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
            $isTransient = in_array($driverCode, TRANSIENT_SERVER_ERROR_CODES, true);
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

$dsn = sprintf(
    'sqlsrv:Driver={ODBC Driver 18 for SQL Server};Server=%s;Database=%s;'
    . 'Encrypt=true;TrustServerCertificate=false;'
    . 'LoginTimeout=90;Authentication=ActiveDirectoryMsi;'
    . 'ConnectRetryCount=5;ConnectRetryInterval=15;'
    . 'MultiSubnetFailover=true;',
    $server,
    $database
);

$options = [
    PDO::ATTR_ERRMODE               => PDO::ERRMODE_EXCEPTION,
    PDO::ATTR_DEFAULT_FETCH_MODE    => PDO::FETCH_ASSOC,
    PDO::ATTR_EMULATE_PREPARES      => false,
    PDO::SQLSRV_ATTR_QUERY_TIMEOUT  => 30,
];

$pdo = connectWithRetry($dsn, $options);
$stmt = queryWithRetry($pdo, 'SELECT TOP (?) name FROM sys.databases ORDER BY name', [5]);
foreach ($stmt as $row) {
    echo $row['name'], PHP_EOL;
}
```

This snippet is tuned for Azure SQL Database failover groups and Azure SQL Managed Instance.

- `Driver={ODBC Driver 18 for SQL Server}` pins the ODBC 18 driver. If the host also has ODBC 17 installed, PDO_SQLSRV can bind to ODBC 17. Older 17.x builds reject newer `Authentication` values; for example, `Authentication=ActiveDirectoryMsi` requires ODBC 17.3.1.1 or a later version. See [Invalid value specified for connection string attribute 'Authentication'](troubleshooting-php-sql-driver.md#invalid-value-specified-for-connection-string-attribute-authentication).
- `ConnectRetryCount` and `ConnectRetryInterval` are ODBC connection string keywords that enable SQL Server *idle connection resiliency*: the driver transparently reconnects a broken idle connection. That's distinct from the application-level `queryWithRetry`, which retries a *statement* that fails with a transient error such as a deadlock or query timeout. The two are complementary, so keep both. Make sure `LoginTimeout` is at least `ConnectRetryCount * ConnectRetryInterval` so the idle-reconnect path gets its full budget; the sample uses 90 seconds to cover 5 × 15 seconds of retries plus headroom for the initial login on a cold failover.
- Complement the application-level `error_log()` calls with driver-side diagnostics. For PDO_SQLSRV, set `pdo_sqlsrv.log_severity` in `php.ini` (settable at initialization only); for SQLSRV, call `sqlsrv_configure("LogSubsystems", ...)` at runtime. For more information, see [Logging activity](logging-activity.md).

  ```ini
  ; php.ini - enable PDO_SQLSRV driver diagnostics alongside the application-level
  ; error_log() calls in the sample. Use 1 (errors) in production; -1 (all) is
  ; useful during triage but very chatty.
  [pdo_sqlsrv]
  pdo_sqlsrv.log_severity = 1
  ```

- For a **user-assigned** managed identity, pass the identity's ID as PDO's `$username` argument (`new PDO($dsn, $identityId, null, $options)`). Use the identity's **client ID** on [Azure App Service](/azure/app-service/overview-managed-identity) or [Azure Container Instance](/azure/container-instances/container-instances-managed-identity); otherwise, use its **object ID**. The PHP drivers inherit this behavior from the underlying Microsoft ODBC Driver for SQL Server; for more information, see [Using Microsoft Entra ID with the ODBC Driver](../odbc/using-azure-active-directory.md#example-connection-strings). PDO_SQLSRV rejects `UID` inside the DSN itself, so use the constructor slot. Passing `null` as the user (as the sample does) selects the system-assigned managed identity of the Azure host. For SQLSRV (procedural), pass `UID` in the connection options array.
- Set `MultiSubnetFailover=true` when you connect to a failover-group listener, availability group listener, or failover cluster instance endpoint. Setting it improves connection performance for both single-subnet and multi-subnet availability group listeners. For more information, see [Support for High Availability, disaster recovery](php-driver-for-sql-server-support-for-high-availability-disaster-recovery.md).
- For read scale-out or a readable secondary, add `ApplicationIntent=ReadOnly` to the Data Source Name (DSN).
- For sovereign clouds where the certificate Subject Alternative Name (SAN) doesn't include the host you're connecting to, add `HostNameInCertificate` to the DSN (for example, `*.database.usgovcloudapi.net` for Azure Government).
- The driver relies on the underlying Microsoft ODBC Driver for SQL Server for token acquisition. Managed identity, service principal, and access-token flows all go through ODBC. For more information, see [Using Microsoft Entra ID with the ODBC Driver](../odbc/using-azure-active-directory.md).
- For higher security and portability across environments, keep connection information outside your code. Store connection information in your application's configuration system, and use Azure Key Vault for sensitive values and centrally managed connection settings.
- The equivalent SQLSRV connection uses `sqlsrv_connect($server, ['Database' => $database, 'Encrypt' => true, 'Authentication' => 'ActiveDirectoryMsi', /* ... */])` and returns a resource. The retry pattern is the same: catch a `false` return from `sqlsrv_connect`, inspect `sqlsrv_errors()` for SQLSTATE, and back off before retrying. For a worked example, see [Step 4: Connect resiliently to SQL with PHP](step-4-connect-resiliently-to-sql-with-php.md).
- The retry helpers read `$e->errorInfo[1]` guarded by `isset()`. [`PDOException::$errorInfo`](https://www.php.net/manual/en/class.pdoexception.php) is declared as `?array` and defaults to `null`, so the defensive check falls back to a driver code of `0` and lets the SQLSTATE `08` prefix decide whether to retry.

For more information about each part of this configuration, see:

- [Connection options](connection-options.md)
- [Connect using Microsoft Entra authentication](azure-active-directory.md)
- [Idle connection resiliency](connection-resiliency.md)
- [Connecting to Microsoft Azure SQL Database](connecting-to-microsoft-azure-sql-database.md)
- [Support for High Availability, disaster recovery](php-driver-for-sql-server-support-for-high-availability-disaster-recovery.md)

For the catalog of Azure SQL transient errors, see [Troubleshoot transient connection errors](/azure/azure-sql/database/troubleshoot-common-connectivity-issues).

## Key features

- **Two APIs, one driver package**: Procedural SQLSRV for SQL Server-first code, or PDO_SQLSRV for portable PDO code.
- **Wide platform support**: Runs on Windows, Linux, and macOS with supported PHP versions.
- **Encrypted connections**: TLS-encrypted connections via `Encrypt=true`, with server certificate validation controlled by `TrustServerCertificate`.
- **Microsoft Entra ID authentication**: Passwordless connections with managed identity, service principal, and access-token flows through the underlying Microsoft ODBC Driver for SQL Server.
- **Always Encrypted**: Client-side encryption for sensitive columns, with optional secure enclaves for in-place operations.
- **Connection resiliency**: Built-in idle connection retries with `ConnectRetryCount` and `ConnectRetryInterval`.
- **PHP streams**: Read and write large binary and character values as streams instead of loading them into memory.
- **Rich SQL Server data type support**: **datetimeoffset**, table-valued parameters, **nvarchar**, and Unicode with `PDO::SQLSRV_ENCODING_UTF8`.

## Get started

| Article | Description |
| --- | --- |
| [System requirements](system-requirements-for-the-php-sql-driver.md) | Supported PHP, operating system, and SQL Server versions. |
| [Support matrix](microsoft-php-drivers-for-sql-server-support-matrix.md) | Detailed compatibility matrix for PHP driver releases. |
| [Download the Microsoft Drivers for PHP for SQL Server](download-drivers-php-sql-server.md) | Download links and release artifacts. |
| [Installation tutorial for Linux and macOS](installation-tutorial-linux-mac.md) | Install the driver and its ODBC prerequisites on Linux and macOS. |
| [Loading the drivers](loading-the-php-sql-driver.md) | Enable the extensions in `php.ini`. |
| [Getting started with the PHP SQL driver](getting-started-with-the-php-sql-driver.md) | End-to-end walkthrough that ties the four getting-started steps together. |
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
| [Connect using Windows authentication](how-to-connect-using-windows-authentication.md) | Use Windows integrated authentication on domain-joined hosts. |

## Secure

| Article | Description |
| --- | --- |
| [Security considerations](security-considerations-for-php-sql-driver.md) | Threat model and defense-in-depth guidance for PHP applications. |
| [Always Encrypted with the PHP drivers](using-always-encrypted-php-drivers.md) | Configure client-side encryption for sensitive columns. |
| [Always Encrypted with secure enclaves](always-encrypted-secure-enclaves.md) | Enable rich operations on encrypted columns with secure enclaves. |

## Retrieve and update data

| Article | Description |
| --- | --- |
| [Programming guide](programming-guide-for-php-sql-driver.md) | End-to-end programming guide for both drivers. |
| [Comparing execution functions](comparing-execution-functions.md) | Choose the right execution function for your workload. |
| [Direct and prepared statement execution (PDO_SQLSRV)](direct-statement-execution-prepared-statement-execution-pdo-sqlsrv-driver.md) | When to use direct execution versus prepared statements. |
| [Retrieving data](retrieving-data.md) | Fetch rows, columns, and streaming values. |
| [Updating data](updating-data-microsoft-drivers-for-php-for-sql-server.md) | Insert, update, and delete rows. |
| [Perform parameterized queries](how-to-perform-parameterized-queries.md) | Bind parameters to protect against SQL injection. |
| [Send data as a stream](how-to-send-data-as-a-stream.md) | Stream large binary and character values into SQL Server. |
| [Perform transactions](how-to-perform-transactions.md) | Group statements into atomic transactions. |
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
| [Send and retrieve UTF-8 data](how-to-send-and-retrieve-utf-8-data-using-built-in-utf-8-support.md) | Use `PDO::SQLSRV_ENCODING_UTF8` for Unicode round-trips. |
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

## Reference

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

## Related content

- [Microsoft Drivers for PHP for SQL Server on GitHub](https://github.com/microsoft/msphpsql)
- [Microsoft ODBC Driver for SQL Server](../odbc/microsoft-odbc-driver-for-sql-server.md)
- [SQL Server drivers](../sql-connection-libraries.md)
