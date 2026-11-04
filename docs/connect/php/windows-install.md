---
title: Install the PHP Drivers for SQL Server on Windows
description: Install and enable the SQLSRV and PDO_SQLSRV drivers for PHP on Windows.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, sumitsar, jathakkar
ms.date: 11/02/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---

# Install the PHP drivers for SQL Server on Windows

Use this article to install the SQLSRV and PDO_SQLSRV drivers for PHP on Windows. The drivers are PHP extensions that use the [Microsoft ODBC Driver for SQL Server](../odbc/download-odbc-driver-for-sql-server.md).

Before you start, confirm that your PHP version, architecture, and thread-safety mode are supported in the [support matrix](microsoft-php-drivers-for-sql-server-support-matrix.md). The PHP runtime used by a command-line process and the runtime used by a web server can use different `php.ini` files. Configure and verify each runtime separately.

At the end of the procedure, both `sqlsrv_connect()` and the PDO `sqlsrv` driver should be available to the intended CLI or web application, and each API should complete a test query. A successful file copy alone isn't a completed installation.

## Choose an installation method

| Method | Use it when | What it installs |
|---|---|---|
| PHP Installer for Extensions (PIE) | You want the installer to select the driver build for the PHP executable | Unversioned `php_sqlsrv.dll` and `php_pdo_sqlsrv.dll`, with automatic configuration attempted |
| Released ZIP package | You need to choose, inspect, or deploy exact prebuilt files yourself | Versioned DLLs selected by PHP minor version, thread-safety mode, and architecture |

Use one method for both extensions. Don't keep PIE and ZIP configuration entries active at the same time.

## Install prerequisites

1. Install a supported PHP build from the [PHP for Windows](https://windows.php.net/download/) site. Add the PHP directory to `PATH`.

1. Install the Visual C++ Redistributable required by your PHP build. Match its x64 or x86 architecture to PHP, as described in the [PHP for Windows requirements](https://www.php.net/downloads.php?os=windows).

1. Install the latest [Microsoft ODBC Driver for SQL Server](../odbc/download-odbc-driver-for-sql-server.md).

1. Run the following commands to confirm the PHP runtime and configuration file:

   ```console
   php --version
   php --ini
   ```

   If **Loaded Configuration File** is `(none)`, copy `php.ini-development` to `php.ini` in the PHP directory.

1. In `php.ini`, set the extension directory and enable the extensions required by the PHP Installer for Extensions (PIE):

   ```ini
   extension_dir = "ext"
   extension=openssl
   extension=zip
   ```

1. Confirm that PHP can locate the prerequisite extensions:

   ```console
   php --ri openssl
   php --ri zip
   ```

   Fix these prerequisites before downloading PIE.

## Install with PIE

[PIE](https://github.com/php/pie) installs the matching precompiled driver for the PHP runtime. It doesn't require a C compiler on Windows.

1. Change to a directory you can write to, and download the latest PIE PHP archive (PHAR):

   ```console
   curl.exe -fL --output pie.phar https://github.com/php/pie/releases/latest/download/pie.phar
   ```

   The remaining PIE commands refer to the file as `.\pie.phar`, so run them from this same directory.

   Before you run the PHAR, verify it with the [GitHub command-line interface (CLI)](https://cli.github.com/):

   ```console
   gh attestation verify --owner php .\pie.phar
   ```

   Confirm the download before you continue:

   ```console
   dir pie.phar
   php .\pie.phar --version
   ```

1. Install each driver separately:

   ```console
   php .\pie.phar install microsoft/sqlsrv
   php .\pie.phar install microsoft/pdo_sqlsrv
   ```

1. Verify the command-line runtime:

   ```console
   php --ri sqlsrv
   php --ri pdo_sqlsrv
   php -m | findstr /I "sqlsrv pdo_sqlsrv"
   ```

PIE attempts to enable the extensions in the configuration used by the command-line executable. If PIE reports that it couldn't enable an extension, add the [PIE-specific extension entries](loading-the-php-sql-driver.md#install-the-drivers-on-windows-with-pie).

If your web server uses another PHP installation, install both drivers for that installation's PHP version, architecture, and thread-safety mode before configuring it. If the same PHP installation uses a different `php.ini` for the web server, enable the installed drivers in that file. Verify the web runtime separately, and restart the web server after changing its configuration.

PIE changes the selected PHP installation. It doesn't configure IIS, select an IIS site, or restart existing PHP processes.

## Manually install a released ZIP package

Use this method when you need to select the driver files yourself or when PIE isn't suitable.

1. Download the Windows ZIP for the released driver version from the [Microsoft Drivers for PHP for SQL Server releases](https://github.com/microsoft/msphpsql/releases) page.

1. Run `php -i | findstr /I "PHP Version Thread Safety Architecture extension_dir"` and record the PHP version, thread-safety mode, architecture, and extension directory.

1. From the ZIP, copy both matching dynamic-link libraries (DLLs) to the extension directory:

   ```text
   php_sqlsrv_<php-version>_<ts-or-nts>_<x86-or-x64>.dll
   php_pdo_sqlsrv_<php-version>_<ts-or-nts>_<x86-or-x64>.dll
   ```

   For example, a PHP 8.5 x64 thread-safe runtime uses `php_sqlsrv_85_ts_x64.dll` and `php_pdo_sqlsrv_85_ts_x64.dll`.

1. Add the matching extension names to the `php.ini` used by the runtime:

   ```ini
   extension=php_sqlsrv_85_ts_x64.dll
   extension=php_pdo_sqlsrv_85_ts_x64.dll
   ```

   Replace `85`, `ts`, and `x64` with the values for your PHP runtime. Don't mix thread-safe and non-thread-safe files, 32-bit and 64-bit files, or files built for another PHP minor version.

1. Verify the runtime with `php --ri sqlsrv`, `php --ri pdo_sqlsrv`, and `php -m`. For a web server, [restrict diagnostic-page access](loading-the-php-sql-driver.md#protect-web-diagnostic-pages) before creating a temporary `phpinfo()` page. Verify the **Loaded Configuration File** and both driver sections, then remove the page.

## Configure a web server

For IIS, configure the intended `php-cgi.exe` as a FastCGI application by following the [PHP IIS installation guidance](https://www.php.net/manual/en/install.windows.iis.php). Confirm that the FastCGI application uses the same PHP directory and architecture that you used to install the drivers.

For any Windows web server:

1. [Restrict diagnostic-page access](loading-the-php-sql-driver.md#protect-web-diagnostic-pages) before creating a temporary `phpinfo()` page. Identify the web server's PHP executable and active `php.ini`.

1. Ensure both drivers are installed for the web runtime. For a separate PHP installation, use PIE with that installation's PHP executable, or copy both matching DLLs from the released ZIP to its extension directory. Add the driver entries to the web runtime's configuration, including when only its `php.ini` differs from the CLI.

1. Restart the web server or FastCGI processes.

1. Confirm the page contains separate `sqlsrv` and `pdo_sqlsrv` sections.

1. Remove the `phpinfo()` page.

## Test both PHP APIs

Save this program as `verify-php-drivers.php`:

```php
<?php
$failures = [];

if (!extension_loaded('sqlsrv') || !function_exists('sqlsrv_connect')) {
    $failures[] = 'SQLSRV is not available.';
}

if (!extension_loaded('pdo_sqlsrv') || !in_array('sqlsrv', PDO::getAvailableDrivers(), true)) {
    $failures[] = 'PDO_SQLSRV is not available.';
}

echo $failures === []
    ? "SQLSRV and PDO_SQLSRV are available." . PHP_EOL
    : implode(PHP_EOL, $failures) . PHP_EOL;

exit($failures === [] ? 0 : 1);
```

Run it first from the command line:

```console
php verify-php-drivers.php
```

Expected output:

```output
SQLSRV and PDO_SQLSRV are available.
```

If you need to verify a web runtime, place the file temporarily in the site's document root and request it in a browser. Remove it after testing. This program verifies the PHP APIs without requiring or exposing database credentials.

## Test a database connection

Extension loading is only the first installation check. Test both APIs against the database target that the application uses so the test also covers the ODBC driver, network path, encryption, authentication, query execution, returned values, and cleanup.

Set temporary environment variables in PowerShell by answering prompts. Enter the server and database names as raw values, without quotes or driver encoding. Use the credential dialog or prompt for a test account that can connect to the database. Don't include credentials in a command line or recorded terminal session:

```powershell
$env:SQL_SERVER_NAME = Read-Host "Server name (tcp:<server>,1433)"
$env:SQL_DATABASE = Read-Host "Database name"
$credential = Get-Credential -Message "Enter the database test account"
if ($null -eq $credential) {
    throw "Database test credentials are required."
}
$env:SQL_UID = $credential.UserName
$env:SQL_PWD = $credential.GetNetworkCredential().Password
Remove-Variable credential
```

The credential prompt masks the password. The environment contains the plaintext password so PHP can read it. Use these variables only in the test session, don't print them, and clear them after testing.

Save this program as `test-php-drivers.php`. Keep the environment values unchanged. The program doubles closing braces and adds outer braces before passing the database name, user name, and password to the driver. This encoding preserves literal braces and prevents semicolons from separating connection options.

```php
<?php
$serverName = getenv('SQL_SERVER_NAME');
$database = getenv('SQL_DATABASE');
$uid = getenv('SQL_UID');
$pwd = getenv('SQL_PWD');

if ($serverName === false || $serverName === ''
    || $database === false || $database === ''
    || $uid === false || $uid === ''
    || $pwd === false || $pwd === '') {
    fwrite(STDERR, "Set SQL_SERVER_NAME, SQL_DATABASE, SQL_UID, and SQL_PWD." . PHP_EOL);
    exit(1);
}

$database = '{' . str_replace('}', '}}', $database) . '}';
$uid = '{' . str_replace('}', '}}', $uid) . '}';
$pwd = '{' . str_replace('}', '}}', $pwd) . '}';

$connectionOptions = [
    "Database" => $database,
    "UID" => $uid,
    "PWD" => $pwd,
    "Encrypt" => 1,
    "TrustServerCertificate" => 0,
];

$sqlsrvConnection = sqlsrv_connect($serverName, $connectionOptions);
if ($sqlsrvConnection === false) {
    foreach (sqlsrv_errors() as $error) {
        fwrite(
            STDERR,
            "SQLSRV {$error['SQLSTATE']} ({$error['code']}): {$error['message']}" . PHP_EOL
        );
    }
    exit(1);
}

$sqlsrvStatement = sqlsrv_query($sqlsrvConnection, "SELECT 1 AS TEST_VALUE");
if ($sqlsrvStatement === false) {
    foreach (sqlsrv_errors() as $error) {
        fwrite(
            STDERR,
            "SQLSRV {$error['SQLSTATE']} ({$error['code']}): {$error['message']}" . PHP_EOL
        );
    }
    sqlsrv_close($sqlsrvConnection);
    exit(1);
}

$sqlsrvRow = sqlsrv_fetch_array($sqlsrvStatement, SQLSRV_FETCH_ASSOC);
if ($sqlsrvRow === false || $sqlsrvRow === null || (int) $sqlsrvRow['TEST_VALUE'] !== 1) {
    fwrite(STDERR, "SQLSRV returned an unexpected result." . PHP_EOL);
    sqlsrv_free_stmt($sqlsrvStatement);
    sqlsrv_close($sqlsrvConnection);
    exit(1);
}

echo "SQLSRV query: 1" . PHP_EOL;
sqlsrv_free_stmt($sqlsrvStatement);
sqlsrv_close($sqlsrvConnection);

$failed = false;
try {
    $pdoConnection = new PDO(
        "sqlsrv:server=$serverName;Database=$database;Encrypt=1;TrustServerCertificate=0",
        $uid,
        $pwd,
        [PDO::ATTR_ERRMODE => PDO::ERRMODE_EXCEPTION]
    );
    $pdoStatement = $pdoConnection->query("SELECT 1 AS TEST_VALUE");
    $pdoRow = $pdoStatement->fetch(PDO::FETCH_ASSOC);
    if ($pdoRow === false || (int) $pdoRow['TEST_VALUE'] !== 1) {
        throw new RuntimeException("PDO_SQLSRV returned an unexpected result.");
    }

    echo "PDO_SQLSRV query: 1" . PHP_EOL;
} catch (Throwable $exception) {
    fwrite(STDERR, $exception->getMessage() . PHP_EOL);
    $failed = true;
} finally {
    unset($pdoStatement);
    unset($pdoConnection);
}

exit($failed ? 1 : 0);
```

Run the program with the same PHP executable that you verified:

```console
php test-php-drivers.php
```

Expected output:

```output
SQLSRV query: 1
PDO_SQLSRV query: 1
```

If the extension check passes but this program fails, use the SQLSTATE and native error to identify the owning layer. Don't reinstall the PHP extensions for a network, certificate, authentication, or database-permission error.

Remove the script and temporary variables after testing:

```powershell
Remove-Item .\verify-php-drivers.php, .\test-php-drivers.php
Remove-Item Env:\SQL_SERVER_NAME, Env:\SQL_DATABASE, Env:\SQL_UID, Env:\SQL_PWD
```

## Troubleshooting

| Symptom | What to check | Fix |
|---|---|---|
| `Could not open input file: .\pie.phar` | Current directory and download location | Return to the directory containing the verified PHAR, or download and verify it again |
| `Unable to load dynamic library` | `extension_dir`, exact DLL name, PHP minor version, architecture, and thread-safety mode | Select the matching DLL or rerun PIE with the intended PHP executable |
| `The specified module could not be found` | Driver DLL dependencies | Confirm the extension DLL exists, install the matching Visual C++ Redistributable, and inspect any remaining missing DLL dependency |
| CLI loads both drivers but IIS doesn't | Different PHP executable or `php.ini` | Compare `php --ini` with `phpinfo()`, configure the IIS runtime, and restart FastCGI |
| `Module "sqlsrv" is already loaded` | Both PIE and ZIP entries, or repeated entries | Keep one SQLSRV entry and one PDO_SQLSRV entry |
| PDO reports `could not find driver` | PDO_SQLSRV configuration | Run `php --ri pdo_sqlsrv` and check `PDO::getAvailableDrivers()` |
| PIE reports success but an old version remains | Versioned ZIP entries still active | Replace old entries with PIE's unversioned DLL entries and restart every PHP process |

If both extensions load, PHP can call their APIs. Capture SQLSRV errors or the PDO exception before changing driver files. Network, authentication, encryption, certificate, and database-permission errors aren't fixed by reinstalling the PHP extension.

## Related content

- [System requirements for the Microsoft Drivers for PHP for SQL Server](system-requirements-for-the-php-sql-driver.md)
- [Loading the Microsoft Drivers for PHP for SQL Server](loading-the-php-sql-driver.md)
- [Upgrade the PHP drivers for SQL Server](upgrade-driver.md)
