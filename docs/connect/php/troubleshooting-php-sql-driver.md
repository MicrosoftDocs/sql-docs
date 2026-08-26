---
title: Troubleshoot the Microsoft Drivers for PHP for SQL Server
description: Diagnose and resolve common installation, connection, query, and data type problems when you use the Microsoft Drivers for PHP for SQL Server.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, sumitsar, jathakkar
ms.date: 08/25/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: troubleshooting
ai-usage: ai-assisted
---

# Troubleshoot the Microsoft Drivers for PHP for SQL Server

[!INCLUDE [Driver_PHP_Download](../../includes/driver_php_download.md)]

Diagnose and resolve common problems when you use the Microsoft Drivers for PHP for SQL Server to connect to SQL Server, Azure SQL Database, Azure SQL Managed Instance, and SQL database in Microsoft Fabric.

For general error and warning handling patterns, see [Handling errors and warnings](handling-errors-and-warnings.md). For driver-side diagnostic capture, see [Logging activity](logging-activity.md).

## Install problems

### Extension not loaded

**Symptoms**:

- `phpinfo()` doesn't list a `sqlsrv` or `pdo_sqlsrv` section.
- `PDOException: could not find driver` when constructing a `PDO` with the `sqlsrv:` DSN.
- `Fatal error: Uncaught Error: Call to undefined function sqlsrv_connect()`.

**Possible causes and solutions**:

- **Extension not enabled in php.ini**. Verify that both `extension=sqlsrv` and `extension=pdo_sqlsrv` are uncommented. On Windows, use the full filename (`extension=php_sqlsrv_84_ts_x64.dll`). For details, see [Loading the drivers](loading-the-php-sql-driver.md).
- **Wrong thread safety build**. The driver binary must match your PHP build's thread safety (`ts` for thread-safe, `nts` for non-thread-safe). Run `php -i | grep "Thread Safety"` to check. Download the matching binary from the [download page](download-drivers-php-sql-server.md).
- **Microsoft ODBC Driver missing**. The PHP drivers wrap the [Microsoft ODBC Driver for SQL Server](../odbc/microsoft-odbc-driver-for-sql-server.md). On Linux and macOS, install `msodbcsql18` (or `msodbcsql17`) with your package manager before loading the extensions. On Windows, install the ODBC driver from the [download page](../odbc/download-odbc-driver-for-sql-server.md).

Verify a successful install:

```bash
php -m | grep -i sqlsrv
```

You should see both `pdo_sqlsrv` and `sqlsrv` in the output.

### PECL or PIE install fails on Linux or macOS

**Symptoms**:

```text
error: ‘SQL_HANDLE_DBC’ undeclared (first use in this function)
fatal error: 'sql.h' file not found
```

**Fix**:

Install the ODBC development headers before installing the drivers:

- **Ubuntu and Debian**: `sudo apt-get install unixodbc-dev`
- **Red Hat, Fedora, and CentOS**: `sudo dnf install unixODBC-devel`
- **Alpine**: `apk add unixodbc-dev`
- **macOS**: `brew install unixodbc`

On Apple silicon, the headers alone aren't enough. Homebrew installs unixODBC under `/opt/homebrew`, which isn't in the default compiler search path, so the build fails with the same error even when the headers are present. Set the compiler flags before you retry:

```bash
export CPPFLAGS="-I/opt/homebrew/opt/unixodbc/include/"
export LDFLAGS="-L/opt/homebrew/lib/"
```

Then retry with PIE:

```bash
pie install microsoft/sqlsrv
pie install microsoft/pdo_sqlsrv
```

Or with PECL:

```bash
sudo pecl install sqlsrv
sudo pecl install pdo_sqlsrv
```

If the install still fails after the headers are installed, the build tool chain might be incomplete. Install `phpize`, `re2c`, and a C++ compiler (`build-essential` on Debian and Ubuntu, `gcc-c++ make` on Red Hat and Fedora, `build-base` on Alpine). PIE offers to install missing build tools for you on Linux and macOS.

For the full install path, see [Installation tutorial for Linux and macOS](installation-tutorial-linux-mac.md).

### Multiple PHP versions installed

**Symptoms**:

`phpinfo()` in your web server shows one PHP version, but `php -v` at the command line shows another, and the driver appears loaded in only one of them.

**Fix**:

Each PHP version has its own `php.ini` and `ext` directory. Locate the correct configuration file with `php --ini` from within the environment that's missing the driver, and add the `extension=` lines there. Restart the web server (Apache, Nginx + PHP-FPM, or IIS) after any php.ini change.

## Connection problems

### Unable to connect to server

**Symptoms**:

```text
SQLSTATE[08001]: [Microsoft][ODBC Driver 18 for SQL Server]TCP Provider: A connection attempt failed
SQLSTATE[HYT00]: [Microsoft][ODBC Driver 18 for SQL Server]Login timeout expired
```

**Possible causes and solutions**:

- **Server isn't reachable**. Check that the server name and port are correct. From the PHP host, test raw TCP connectivity.

  ```bash
  # Linux and macOS
  nc -vz <server>.database.windows.net 1433

  # Windows PowerShell
  Test-NetConnection -ComputerName <server>.database.windows.net -Port 1433
  ```

- **Firewall blocks outbound 1433**. Corporate firewalls and cloud NSGs often block outbound port 1433. Add an exception, or allow the [Azure SQL Database IP ranges](/azure/azure-sql/database/network-access-controls-overview) for your region.
- **Azure SQL server firewall**. Add your client's public IP to the server-level firewall rules in the Azure portal.
- **Named instance**. For a named instance, verify the SQL Server Browser service is running on the server and that UDP 1434 is open. Or, connect by port instead of by instance name.

### Login failed

**Symptoms**:

```text
SQLSTATE[28000]: [Microsoft][ODBC Driver 18 for SQL Server][SQL Server]Login failed for user '<user_id>'.
```

**Possible causes and solutions**:

- **SQL authentication mode disabled**. Local SQL Server instances default to Windows Authentication only. Enable mixed mode authentication in SQL Server Management Studio under **Server properties** > **Security**, and then restart the SQL Server service.
- **Azure SQL credentials format**. Azure SQL requires the fully qualified username (`user@servername`) when connecting from tools that don't append it automatically.
- **User not mapped to database**. Verify that the login has a user mapping in the target database and that the user has the required permissions.
- **Prefer Microsoft Entra ID**. For Azure SQL, Azure SQL Managed Instance, and SQL database in Fabric, use Microsoft Entra authentication (`Authentication=ActiveDirectoryMsi`, `Authentication=ActiveDirectoryServicePrincipal`, or an access token) instead of SQL logins. See [Connect using Microsoft Entra authentication](azure-active-directory.md).

### Invalid value specified for connection string attribute 'Authentication'

**Symptoms**:

```text
SQLSTATE[08001]: [Microsoft][ODBC Driver 17 for SQL Server]Invalid value specified for connection string attribute 'Authentication'
```

**Cause**:

The ODBC driver reports the error, but the real problem is *which* driver PDO_SQLSRV bound to. If the DSN doesn't include a `Driver=` keyword and the host has both ODBC 17 and ODBC 18 installed, PDO_SQLSRV can bind to the older version. Older ODBC 17.x builds don't know newer `Authentication` values such as `ActiveDirectoryServicePrincipal` or `ActiveDirectoryDefault`, and even `ActiveDirectoryMsi` requires ODBC 17.3.1.1 or a later version.

**Fix**:

Pin the driver in the DSN:

```php
<?php
$dsn = "sqlsrv:Driver={ODBC Driver 18 for SQL Server};Server=$server;Database=$db;" .
       "Encrypt=true;Authentication=ActiveDirectoryMsi";
$conn = new PDO($dsn, null, null, [PDO::ATTR_ERRMODE => PDO::ERRMODE_EXCEPTION]);
```

The bracketed form (`{ODBC Driver 18 for SQL Server}`) escapes the spaces in the driver name. The error message itself always names the driver that reported it, so the prefix `[Microsoft][ODBC Driver 17 for SQL Server]` in the error is the fastest way to confirm the wrong driver bound.

### Invalid keyword 'UID' was specified in the DSN string

**Symptoms**:

```text
SQLSTATE[IMSSP]: An invalid keyword 'UID' was specified in the DSN string.
```

**Cause**:

PDO_SQLSRV enforces an allow list of DSN keywords and doesn't accept `UID` or `PWD` in the DSN. PDO reserves the second and third constructor arguments for those, and PDO_SQLSRV translates them into ODBC `UID`/`PWD` internally.

**Fix**:

Move the username (and password, for SQL authentication) into the PDO constructor:

```php
<?php
// SQL authentication.
$dsn = "sqlsrv:Driver={ODBC Driver 18 for SQL Server};Server=$server;Database=$db;Encrypt=true";
$conn = new PDO($dsn, $user, $password);

// User-assigned managed identity. Pass the identity's client ID as $username.
$dsn = "sqlsrv:Driver={ODBC Driver 18 for SQL Server};Server=$server;Database=$db;" .
       "Encrypt=true;Authentication=ActiveDirectoryMsi";
$conn = new PDO($dsn, $clientId, null);
```

The SQLSRV procedural driver, by contrast, accepts `UID` and `PWD` in the connection options array passed to `sqlsrv_connect()`.

### PDO_SQLSRV silently ignores AccessToken in the options array

**Symptom**:

You have a Microsoft Entra access token (for example, from `az account get-access-token --resource https://database.windows.net/`, `ManagedIdentityCredential`, or `ClientSecretCredential`), and you pass it to PDO_SQLSRV as `['AccessToken' => $token]` in the fourth constructor argument. The connection attempt fails with a confusing error such as `Windows logins are not supported in this version of SQL Server` or `Login failed for user ''`, as if no credentials were supplied.

**Cause**:

PDO's fourth constructor argument is reserved for driver-specific attribute constants (integer keys such as `PDO::ATTR_ERRMODE`). PDO silently drops string-keyed entries such as `AccessToken`, so PDO_SQLSRV never sees the token. The connection then falls back to Windows Integrated authentication, which the server rejects.

**Fix**:

Move `AccessToken` into the DSN string. Reserve the options array for `PDO::ATTR_*` constants.

```php
<?php
$server = '<server>.database.windows.net';
$token  = getenv('SQL_ACCESS_TOKEN');   // raw JWT, no "Bearer " prefix

$dsn = "sqlsrv:Server=$server;Database=<database>;Encrypt=true;AccessToken=$token";
$conn = new PDO($dsn, null, null, [
    PDO::ATTR_ERRMODE => PDO::ERRMODE_EXCEPTION,
]);
```

For additional Microsoft Entra authentication examples, including the DSN form for PDO_SQLSRV, see [Connect using Microsoft Entra authentication](azure-active-directory.md).

For SQLSRV procedural, `AccessToken` belongs in the connection-info array passed to `sqlsrv_connect()`, which does wrap the raw JWT into `SQL_COPT_SS_ACCESS_TOKEN` for you:

```php
<?php
$server = '<server>.database.windows.net';
$token  = getenv('SQL_ACCESS_TOKEN');   // raw JWT, no "Bearer " prefix

$connectionInfo = [
    'Database'               => '<database>',
    'AccessToken'            => $token,
    'Encrypt'                => true,
    'TrustServerCertificate' => false,
    'Driver'                 => '{ODBC Driver 18 for SQL Server}',
];

$conn = sqlsrv_connect($server, $connectionInfo);
if ($conn === false) {
    print_r(sqlsrv_errors());
    exit(1);
}
```

### TLS certificate errors

**Symptoms**:

```text
SQLSTATE[08001]: SSL Provider: The certificate chain was issued by an authority that is not trusted
SQLSTATE[08001]: SSL Provider: The target principal name is incorrect
```

**Solutions**:

Prefer a trusted certificate. Use `TrustServerCertificate=true` only for local development against a server that you control.

For development against a self-signed certificate:

```php
<?php
$server   = 'localhost';
$database = '<database>';
$user     = '<user_id>';
$password = '<password>';

$dsn = "sqlsrv:Server=$server;Database=$database;Encrypt=true;TrustServerCertificate=true";
$conn = new PDO($dsn, $user, $password, [
    PDO::ATTR_ERRMODE => PDO::ERRMODE_EXCEPTION,
]);
```

> [!CAUTION]
> `TrustServerCertificate=true` disables server certificate validation. Never carry that setting into production, staging, or shared environments.

For a production hostname that doesn't match the certificate Common Name (for example, when connecting through a listener), specify the actual certificate subject:

```php
<?php
$dsn = "sqlsrv:Server=<listener>;Database=<database>;Encrypt=true;HostNameInCertificate=*.database.windows.net;Authentication=ActiveDirectoryMsi";
$conn = new PDO($dsn, null, null, [
    PDO::ATTR_ERRMODE => PDO::ERRMODE_EXCEPTION,
]);
```

### Connection timeout

**Symptoms**:

```text
SQLSTATE[HYT00]: Login timeout expired
```

**Possible causes and solutions**:

- **`LoginTimeout` not set or set too low for cold failover**. Set an explicit `LoginTimeout` (in seconds) in the DSN when connecting to Azure SQL. Failover-group failovers and cold-start databases can take longer than a short client-side timeout allows. See [Connection options](connection-options.md) for the option reference.
- **Idle reconnect budget truncated**. If you set `ConnectRetryCount` and `ConnectRetryInterval`, make sure `LoginTimeout >= ConnectRetryCount * ConnectRetryInterval`. Otherwise the login timeout ends the reconnect loop early. See [Idle connection resiliency](connection-resiliency.md).

```php
<?php
$dsn = "sqlsrv:Driver={ODBC Driver 18 for SQL Server};Server=<server>.database.windows.net;Database=<database>;" .
       "Encrypt=true;LoginTimeout=90;ConnectRetryCount=5;ConnectRetryInterval=15;" .
       "Authentication=ActiveDirectoryMsi";
$conn = new PDO($dsn, null, null, [
    PDO::ATTR_ERRMODE => PDO::ERRMODE_EXCEPTION,
]);
```

## Query execution problems

### Silent failures with PDO

**Symptom**:

A `PDO::exec()` or `PDOStatement::execute()` call returns `false` but doesn't throw an exception.

**Fix**:

With PHP 8.0 and later versions, the default PDO error mode is `PDO::ERRMODE_EXCEPTION`. If a call returns `false` without throwing, the application changed the mode to `PDO::ERRMODE_SILENT` or `PDO::ERRMODE_WARNING`. Set it back to exception mode so failures throw exceptions:

```php
<?php
$conn = new PDO($dsn, $user, $password, [
    PDO::ATTR_ERRMODE => PDO::ERRMODE_EXCEPTION,
]);
```

If you can't change the mode globally, check `$conn->errorInfo()` (or `$stmt->errorInfo()`) after every call. The array contains `[SQLSTATE, driver code, driver message]`.

### Invalid object name

**Symptoms**:

```text
SQLSTATE[42S02]: [Microsoft][ODBC Driver 18 for SQL Server][SQL Server]Invalid object name 'Products'.
```

**Possible causes and solutions**:

- **Wrong database context**. Verify with a quick query:

  ```php
  <?php
  $stmt = $conn->query("SELECT DB_NAME()");
  echo $stmt->fetchColumn();
  ```

- **Missing schema qualifier**. Use fully qualified names to avoid depending on the caller's default schema:

  ```sql
  SELECT * FROM dbo.Products;
  ```

- **Case sensitivity**. Databases created with a case-sensitive collation treat `products` and `Products` as different objects. Match the exact case in the table definition.

### Wrong number of parameters

**Symptoms**:

```text
SQLSTATE[HY093]: Invalid parameter number
SQLSTATE[07002]: COUNT field incorrect or syntax error
```

**Fix**:

For PDO_SQLSRV, the number of `?` placeholders must match the number of values you pass to `execute()`, and each `?` binds a single scalar (not an array). For named parameters, every `:name` in the SQL must appear in the array and vice versa.

```php
<?php
$stmt = $conn->prepare(
    "SELECT * FROM dbo.Products WHERE CategoryID = ? AND ListPrice > ?"
);
$stmt->execute([1, 50.0]);
foreach ($stmt as $row) {
    // ...
}
```

For SQLSRV, pass the parameter array to `sqlsrv_query()` or `sqlsrv_prepare()`:

```php
<?php
$stmt = sqlsrv_query(
    $conn,
    "SELECT * FROM dbo.Products WHERE CategoryID = ? AND ListPrice > ?",
    [1, 50.0]
);
if ($stmt === false) {
    die(print_r(sqlsrv_errors(), true));
}
```

For a broader introduction to parameter binding, see [Perform parameterized queries](how-to-perform-parameterized-queries.md).

### PDO emulated prepares mask errors

**Symptoms**:

A statement runs successfully on one connection but throws a syntax error on another connection that uses the same query text.

**Cause**:

PDO_SQLSRV supports both emulated and native prepared statements. Emulated prepares (`PDO::ATTR_EMULATE_PREPARES = true`) interpolate parameters client-side. Native prepares (`false`) send the query and parameters separately to the server. Behavior differs for `TOP (?)`, table-valued parameters, and some edge cases in type coercion.

**Fix**:

Prefer native prepares in production. Set `PDO::ATTR_EMULATE_PREPARES => false` at connection time so behavior is consistent across environments:

```php
<?php
$conn = new PDO($dsn, null, null, [
    PDO::ATTR_ERRMODE          => PDO::ERRMODE_EXCEPTION,
    PDO::ATTR_EMULATE_PREPARES => false,
]);
```

For details on when to use each mode, see [PDO::prepare](pdo-prepare.md).

## Data type problems

### Unicode characters appear as `?` or garbled

**Symptoms**:

Rows that PHP writes contain question marks or replacement characters instead of the original non-ASCII characters. Reads return garbled text.

**Possible causes and solutions**:

- **Column type is VARCHAR, not NVARCHAR**. **varchar** columns use a code page, not Unicode. Use **nvarchar** for internationalized text.
- **Missing UTF-8 encoding hint on PDO_SQLSRV**. When your SQL Server column is **nvarchar** and your PHP data is UTF-8, tell the driver to convert between UTF-8 (client) and UTF-16 (server):

  ```php
  <?php
  $conn = new PDO(
      "sqlsrv:Server=<server>;Database=<database>;Encrypt=true",
      $user,
      $password,
      [
          PDO::ATTR_ERRMODE                    => PDO::ERRMODE_EXCEPTION,
          PDO::SQLSRV_ATTR_ENCODING            => PDO::SQLSRV_ENCODING_UTF8,
      ]
  );
  ```

- **SQLSRV driver: request UTF-8 explicitly**. `SQLSRV_ENC_CHAR` is the default 8-bit system code page, not UTF-8. For UTF-8 with SQLSRV, set `"CharacterSet" => "UTF-8"` on the connection and pass the literal `'UTF-8'` to `SQLSRV_PHPTYPE_STRING` on fetch or bind. See [Send and retrieve UTF-8 data](how-to-send-and-retrieve-utf-8-data-using-built-in-utf-8-support.md).

### Datetime conversion errors

**Symptoms**:

```text
SQLSTATE[22007]: Invalid character value for cast specification
```

**Fix**:

On PDO_SQLSRV, don't bind a raw `DateTime` object. PDO stringifies bound values before binding, and PHP's `DateTime` has no `__toString()` method, so `execute([new DateTime(...)])` raises `Object of class DateTime could not be converted to string`. Format the value first, or pass an ISO 8601 string (`YYYY-MM-DD HH:MM:SS[.fff]`), not a locale-formatted string.

```php
<?php
$stmt = $conn->prepare("INSERT INTO dbo.Events (EventDate) VALUES (?)");
$stmt->execute([(new DateTime("2026-03-15 10:00:00"))->format("Y-m-d H:i:s.u")]);
```

To fetch **datetime** columns as `DateTime` objects instead of strings on PDO_SQLSRV, set the statement attribute:

```php
<?php
$stmt = $conn->prepare("SELECT EventDate FROM dbo.Events");
$stmt->setAttribute(PDO::SQLSRV_ATTR_FETCHES_DATETIME_TYPE, true);
$stmt->execute();
```

For details, see [Retrieve datetime objects (PDO_SQLSRV)](how-to-retrieve-datetime-objects-using-pdo-sqlsrv-driver.md).

### Decimal formatting problems

**Symptoms**:

Values between -1 and 1 are missing a leading zero, or **money** and **smallmoney** values show an unexpected number of decimal places.

**Fix**:

PDO_SQLSRV always fetches **decimal** and **numeric** values as strings with their exact precision and scale. Set `PDO::SQLSRV_ATTR_FORMAT_DECIMALS` to add a leading zero to values between -1 and 1:

```php
<?php
$conn->setAttribute(PDO::SQLSRV_ATTR_FORMAT_DECIMALS, true);
```

`PDO::SQLSRV_ATTR_DECIMAL_PLACES` applies only to **money** and **smallmoney** values. It sets their displayed scale from 0 through 4 and might round the displayed value. It doesn't affect **decimal** or **numeric** values.

For details, see [Format decimals and money (PDO_SQLSRV)](formatting-decimals-pdo-sqlsrv-driver.md) or [Format decimals and money (SQLSRV)](formatting-decimals-sqlsrv-driver.md).

## Transaction problems

### Data changes don't persist

**Symptoms**:

Rows you insert or update in PHP don't appear when you query from another session.

**Cause**:

`PDO::beginTransaction()` opens an explicit transaction that requires an explicit `commit()`. If the PHP script ends without calling `commit()`, PDO rolls the transaction back during connection cleanup.

**Fix**:

Always pair `beginTransaction()` with `commit()`, and use `try`/`catch` to roll back on error:

```php
<?php
try {
    $conn->beginTransaction();
    $conn->exec("INSERT INTO dbo.Orders (CustomerID, Total) VALUES (1, 100)");
    $conn->exec("UPDATE dbo.Inventory SET Stock = Stock - 1 WHERE ProductID = 5");
    $conn->commit();
} catch (PDOException $e) {
    $conn->rollBack();
    throw $e;
}
```

For SQLSRV, use [`sqlsrv_begin_transaction`](sqlsrv-begin-transaction.md), [`sqlsrv_commit`](sqlsrv-commit.md), and [`sqlsrv_rollback`](sqlsrv-rollback.md).

### Deadlock errors

**Symptoms**:

```text
SQLSTATE[40001]: [Microsoft][ODBC Driver 18 for SQL Server][SQL Server]Transaction (Process ID 62) was deadlocked
```

**Fix**:

Handle transient deadlock errors with retry logic. Wrap the whole transaction (not just the failing statement) so earlier statements replay on the fresh transaction. For a production-oriented retry pattern, see the sample on the [PHP driver landing page](microsoft-php-driver-for-sql-server.md#production-baseline-for-azure-sql).

Recurring deadlocks indicate a design problem. Capture the deadlock graph and analyze which statements and lock types are involved. Common fixes include reordering operations so competing transactions acquire locks in the same sequence, reducing transaction scope, and adding indexes to reduce lock duration. For a full walkthrough, see [Deadlocks guide](../../relational-databases/sql-server-deadlocks-guide.md).

## Connection resiliency problems

### Reconnect doesn't happen

**Symptoms**:

An idle connection stays broken after an Azure SQL Database failover, even though you set `ConnectRetryCount` and `ConnectRetryInterval`.

**Possible causes and solutions**:

- **Active server-side cursor**. Idle connection resiliency only reconnects *idle* connections. An open server-side cursor or a pending transaction keeps the connection active. Free server-side cursors by using `sqlsrv_free_stmt()` or `$stmt = null;` (PDO) before the failover window, or switch to a client-side buffered cursor. See [Idle connection resiliency](connection-resiliency.md).
- **Non-recoverable session state**. Some session state can't be reestablished, including temp tables, global and local cursors, transaction context, application locks, `EXECUTE AS`/`REVERT`, OLE automation handles, prepared XML handles, and trace flags. Any of these session states prevents automatic reconnect.
- **`LoginTimeout` too small**. If `ConnectRetryCount * ConnectRetryInterval > LoginTimeout`, the driver stops retrying when `LoginTimeout` is reached. Raise `LoginTimeout` to cover the full retry budget.

## Performance problems

For diagnosis and remediation of slow queries, cold starts, large result sets, and bulk inserts, see [Performance tuning](performance-tuning-php-sql-driver.md).

## Enable driver diagnostics

When application-level `error_log()` calls don't provide enough information, turn on driver-side logging. It reports every ODBC call the driver makes.

### PDO_SQLSRV

Set `pdo_sqlsrv.log_severity` in `php.ini` and restart the web server. This setting is only readable at initialization:

```ini
[pdo_sqlsrv]
pdo_sqlsrv.log_severity = 1
```

Values are `0` (off, the default), `-1` (errors, warnings, and notices), `1` (errors), `2` (warnings), and `4` (notices).

### SQLSRV

Enable logging at runtime with `sqlsrv_configure()`:

```php
<?php
sqlsrv_configure("LogSubsystems", SQLSRV_LOG_SYSTEM_CONN | SQLSRV_LOG_SYSTEM_STMT);
sqlsrv_configure("LogSeverity", SQLSRV_LOG_SEVERITY_ERROR | SQLSRV_LOG_SEVERITY_WARNING);
```

Log entries go to the file configured by `error_log` in `php.ini`. For the full list of subsystems and severities, see [Logging activity](logging-activity.md).

## Container and CI problems

### Missing system libraries on Linux

**Symptoms**:

```text
error while loading shared libraries: libodbc.so.2: cannot open shared object file
error while loading shared libraries: libssl.so.1.1: cannot open shared object file
```

**Fix**:

Install the runtime dependencies before installing the PHP driver:

| Distribution | Install command |
| --- | --- |
| Ubuntu and Debian | `sudo apt-get install unixodbc libgssapi-krb5-2` |
| Red Hat and Fedora | `sudo dnf install unixODBC krb5-libs` |
| Alpine | `apk add unixodbc gcompat` |

Then install `msodbcsql18` from the Microsoft package repository. For distribution-specific package repositories and versions, see the [ODBC driver install guide](../odbc/linux-mac/installing-the-microsoft-odbc-driver-for-sql-server.md).

### Docker image builds succeed but connections fail at runtime

**Symptoms**:

The image builds and PHP starts, but `PDO::__construct()` throws an ODBC driver-not-found error.

**Fix**:

Verify the ODBC driver is installed *in the runtime image*, not only the build stage. Install `msodbcsql18` and `unixodbc-dev` in the same stage that ships to production. In a multi-stage build, install them in the final stage. A single-stage Debian-based install looks like this:

```dockerfile
# Pin to a specific PHP minor version in production, for example php:8.4.11-cli.
FROM php:8.4-cli
RUN apt-get update && apt-get install -y --no-install-recommends \
        curl gnupg2 apt-transport-https ca-certificates \
    && curl -sSL https://packages.microsoft.com/keys/microsoft.asc | gpg --dearmor > /usr/share/keyrings/microsoft.gpg \
    && echo "deb [arch=amd64 signed-by=/usr/share/keyrings/microsoft.gpg] https://packages.microsoft.com/debian/12/prod bookworm main" > /etc/apt/sources.list.d/mssql-release.list \
    && apt-get update \
    && ACCEPT_EULA=Y apt-get install -y --no-install-recommends msodbcsql18 unixodbc-dev \
    # $PHPIZE_DEPS ships in the official php image and includes gcc, make, autoconf, and re2c.
    && apt-get install -y --no-install-recommends $PHPIZE_DEPS \
    && pecl install sqlsrv pdo_sqlsrv \
    && docker-php-ext-enable sqlsrv pdo_sqlsrv \
    && apt-get purge -y --auto-remove $PHPIZE_DEPS \
    && rm -rf /var/lib/apt/lists/*
```

## Related content

- [Handling errors and warnings](handling-errors-and-warnings.md)
- [Logging activity](logging-activity.md)
- [Idle connection resiliency](connection-resiliency.md)
- [Performance tuning](performance-tuning-php-sql-driver.md)
- [Connection options](connection-options.md)
- [Support matrix](microsoft-php-drivers-for-sql-server-support-matrix.md)
