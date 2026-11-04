---
title: Upgrade the PHP Drivers for SQL Server
description: Upgrade the SQLSRV and PDO_SQLSRV drivers and their ODBC dependency on Windows, Linux, and macOS.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, sumitsar, jathakkar
ms.date: 11/02/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
---

# Upgrade the PHP drivers for SQL Server

Upgrade SQLSRV and PDO_SQLSRV together so that both extensions use the same release family. The PHP drivers use the Microsoft ODBC Driver for SQL Server, so check the [system requirements](system-requirements-for-the-php-sql-driver.md) before upgrading.

An upgrade changes native code loaded into PHP processes. A mixed deployment can continue running the previous release, fail at PHP startup, or expose different driver versions to the CLI and web application. Inventory every PHP runtime and retain the previous files and configuration until verification finishes.

## Choose the upgrade path

| Current installation | Target installation | Procedure |
|---|---|---|
| PIE | PIE | Run PIE with the same PHP executable or explicit `php-config`, then verify every SAPI |
| Windows released ZIP | Windows released ZIP | Replace both versioned DLLs and update both configuration entries together |
| Windows released ZIP | PIE | Install both PIE packages, replace versioned DLL entries with PIE entries, and remove duplicates |
| Linux or macOS released archive | PIE | Install both PIE packages, replace archive-filename entries with `sqlsrv.so` and `pdo_sqlsrv.so`, and preserve PDO load order |
| Linux or macOS PIE | New PHP minor version | Install the new PHP development tools, build both extensions for that PHP installation, and configure each SAPI separately |

Don't overwrite the previous binaries until you have a rollback copy. Don't upgrade only one extension when the application uses both APIs.

## Before you upgrade

1. Record the PHP executable, configuration file, architecture, thread-safety mode, and current driver versions.

   ```console
   php --version
   php --ini
   php --ri sqlsrv
   php --ri pdo_sqlsrv
   ```

1. Back up the `php.ini` file and any extension-specific `.ini` files that load `sqlsrv` or `pdo_sqlsrv`.

1. Check the [support matrix](microsoft-php-drivers-for-sql-server-support-matrix.md) for the target PHP version and operating system.

1. Check the ODBC compatibility table in [System requirements](system-requirements-for-the-php-sql-driver.md#odbc-driver). Install a supported ODBC version before or during the PHP driver upgrade when the target driver release requires it.

1. Plan a restart of every process that loads PHP, including the web server, [PHP FastCGI Process Manager (PHP-FPM)](https://www.php.net/manual/en/install.fpm.php), queue workers, and long-running command-line workers.

1. Record a baseline from each runtime:

   - The `ExtensionVer` shown by `php --ri sqlsrv` and `php --ri pdo_sqlsrv`.
   - The loaded configuration file and scanned `.ini` directory.
   - The extension directory and active driver filenames.
   - A passing application smoke test that uses each API your application depends on.

1. Prepare a rollback:

   - Keep the previous PHP runtime available if you change the PHP minor version. Record the executable configured for each web server and worker.
   - Keep copies of the previous SQLSRV and PDO_SQLSRV files.
   - Keep copies of every changed `php.ini` or extension-specific `.ini` file.
   - Record the previous ODBC package version if the upgrade changes it.
   - Define how to stop traffic or workers while native extension files are replaced.

## Upgrade on Windows with PIE

Use the PHP Installer for Extensions (PIE) with the PHP executable that serves the application. First, [download and verify PIE](windows-install.md#install-with-pie). Before running either install command, drain application traffic and stop the web server, FastCGI processes, and workers that use this PHP installation. Prevent them from automatically restarting until both extensions and their configuration are ready. Keep a separate command-line session for PIE.

Temporarily comment out the existing SQLSRV and PDO_SQLSRV extension entries in the CLI configuration used by PIE. Otherwise, the PHP process running PIE can keep the old DLLs loaded and prevent replacement. Keep PIE's prerequisite extensions enabled.

From the directory containing `pie.phar`, install both packages separately:

```console
php .\pie.phar install microsoft/sqlsrv
php .\pie.phar install microsoft/pdo_sqlsrv
```

PIE installs `php_sqlsrv.dll` and `php_pdo_sqlsrv.dll`. In every active configuration, replace versioned ZIP entries such as `extension=php_sqlsrv_85_ts_x64.dll` and `extension=php_pdo_sqlsrv_85_ts_x64.dll` with:

```ini
extension=php_sqlsrv.dll
extension=php_pdo_sqlsrv.dll
```

Enable both entries, and remove duplicate SQLSRV and PDO_SQLSRV entries even if PIE reports that automatic enablement succeeded. If the web server uses a different PHP installation, repeat the upgrade and configuration changes for that installation.

Restart each PHP process, then run `php --ri sqlsrv` and `php --ri pdo_sqlsrv` with the corresponding executable. Confirm that `ExtensionVer` reports the target version for both drivers. For a web application, [restrict diagnostic-page access](loading-the-php-sql-driver.md#protect-web-diagnostic-pages) before creating a temporary `phpinfo()` page. Verify both values, then remove the page.

## Upgrade a Windows ZIP installation

1. Download the target release's Windows ZIP from the [Microsoft Drivers for PHP for SQL Server releases](https://github.com/microsoft/msphpsql/releases) page.

1. Confirm the new dynamic-link library (DLL) names match the PHP minor version, architecture, and thread-safety mode. Stop the PHP web server or worker processes before replacing files.

1. Replace both `php_sqlsrv_*.dll` and `php_pdo_sqlsrv_*.dll` files in the PHP extension directory. Keep the existing `extension=` entries if the filenames still match. Otherwise, update them together.

1. Install or upgrade the Microsoft ODBC Driver for SQL Server if the target PHP driver requires a newer supported version.

1. Restart every PHP process that uses the replaced DLLs. A running process keeps the old native extension loaded until it exits.

## Upgrade on Linux or macOS with PIE

1. Follow the [PIE setup and platform prerequisites](installation-tutorial-linux-mac.md#install-the-drivers-with-pie) if PIE isn't installed. Confirm the PHP development tools and ODBC development package are installed. In an interactive session, PIE can prompt to install missing build tools through the platform package manager. In a non-interactive session, install them before running PIE.

1. Select the PHP installation before upgrading. For a versioned installation, add PIE's `--with-php-config` option to both install commands. For example, use `pie install --with-php-config=/usr/bin/php-config8.4 microsoft/sqlsrv` when the intended configuration tool is `/usr/bin/php-config8.4`. Apply the same option to `microsoft/pdo_sqlsrv`. For Alpine's versioned executables, use the [Alpine installation commands](installation-tutorial-linux-mac.md#install-on-alpine).

1. Drain application traffic and stop the web server, PHP-FPM processes, and workers that use this PHP installation. Prevent them from automatically restarting until both extensions and their configuration are ready. Keep a separate command-line session for PIE.

   Temporarily comment out the existing SQLSRV and PDO_SQLSRV extension entries in the CLI configuration used by PIE. Keep PIE's prerequisite extensions enabled.

1. Upgrade both packages separately. These commands use PIE's default PHP installation:

   ```bash
   pie install microsoft/sqlsrv
   pie install microsoft/pdo_sqlsrv
   ```

1. Check every active configuration for the PHP installation used by your application. If a previous manual installation uses archive filenames such as `extension=SQLSRV_8.4_NTS.so` and `extension=PDO_SQLSRV_8.4_NTS.so`, replace those entries with the PIE-installed filenames:

   ```ini
   extension=sqlsrv.so
   extension=pdo_sqlsrv.so
   ```

   Re-enable both driver entries, and remove duplicates. Keep PDO_SQLSRV in a scanned configuration file that loads after PDO, as described in [Load the drivers on Linux and macOS](loading-the-php-sql-driver.md#load-the-drivers-on-linux-and-macos). Repeat this configuration change for the command-line, PHP-FPM, web server, and worker configurations that your application uses.

1. Upgrade the Microsoft ODBC Driver for SQL Server when required by the target release.

1. Restart each CLI worker, queue worker, web server, and PHP-FPM service that uses the upgraded PHP installation.

## Restart and verify

Replacing an extension file doesn't update a PHP process that already loaded the old binary. Restart the web server, PHP-FPM, and any long-running PHP workers before verifying the upgrade.

1. Verify the command-line runtime:

   ```console
   php --ri sqlsrv
   php --ri pdo_sqlsrv
   php -m | findstr /I "sqlsrv pdo_sqlsrv"
   ```

   On Linux or macOS, replace the last command with `php -m | grep -E 'sqlsrv|pdo_sqlsrv'`.

1. For a web application, [restrict diagnostic-page access](loading-the-php-sql-driver.md#protect-web-diagnostic-pages) before creating a temporary `phpinfo()` page. Verify the loaded configuration file, extension directory, and `ExtensionVer` for both drivers. Confirm that `ExtensionVer` reports the target version, then remove the page.

1. Run an existing application smoke test that exercises both SQLSRV and PDO_SQLSRV if your application uses both. Keep the connection configuration unchanged during this check so that an upgrade test doesn't also test an unrelated connection change.

If a driver fails to load, compare the PHP minor version, architecture, thread-safety mode, extension directory, and ODBC installation with the values recorded before the upgrade.

## Verify both APIs and versions

Save this program as `verify-driver-upgrade.php`:

```php
<?php
$results = [
    'SQLSRV' => [
        'loaded' => extension_loaded('sqlsrv') && function_exists('sqlsrv_connect'),
        'version' => phpversion('sqlsrv'),
    ],
    'PDO_SQLSRV' => [
        'loaded' => extension_loaded('pdo_sqlsrv')
            && in_array('sqlsrv', PDO::getAvailableDrivers(), true),
        'version' => phpversion('pdo_sqlsrv'),
    ],
];

$failed = false;
foreach ($results as $name => $result) {
    $version = $result['version'] === false ? 'unknown' : $result['version'];
    echo "$name: " . ($result['loaded'] ? 'loaded' : 'missing') . ", version $version" . PHP_EOL;
    $failed = $failed || !$result['loaded'] || $result['version'] === false;
}

exit($failed ? 1 : 0);
```

Run the program with the CLI executable and, temporarily, through each web runtime. Confirm that both reported versions match the release you installed. Remove the web copy after verification.

This program verifies the PHP extension layer. Run the unchanged application smoke test afterward to verify ODBC loading, connection options, authentication, database access, returned PHP types, and cleanup behavior.

## Verify database behavior before and after the upgrade

Use the same program and connection settings before and after the upgrade. Keeping the test unchanged separates driver changes from application or connection-configuration changes.

Set the `SQL_SERVER_NAME`, `SQL_DATABASE`, `SQL_UID`, and `SQL_PWD` environment variables for a test account by answering prompts. Enter the raw values, without shell quotes or driver encoding. Don't include credentials in a command line or recorded terminal session.

On Windows PowerShell, enter the server name and database name, then use the credential dialog or prompt for the test account:

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

On Linux or macOS, use Bash. Disable command tracing before reading any values. The password prompt doesn't echo your input:

```bash
set +x
IFS= read -r -p 'Server name (tcp:<server>,1433): ' SQL_SERVER_NAME
IFS= read -r -p 'Database name: ' SQL_DATABASE
IFS= read -r -p 'User name: ' SQL_UID
IFS= read -r -s -p 'Password: ' SQL_PWD
printf '\n'
export SQL_SERVER_NAME SQL_DATABASE SQL_UID SQL_PWD
```

The environment contains the plaintext password so PHP can read it. Use these variables only in the test session, don't print them, and clear them after testing.

Save this program as `test-driver-upgrade.php`. Keep the environment values unchanged. The program doubles closing braces and adds outer braces before passing the database name, user name, and password to the driver. This encoding preserves literal braces and prevents semicolons from separating connection options.

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

Run the program before the upgrade and save the successful output. Run the unchanged file again after restarting every PHP process:

```console
php test-driver-upgrade.php
```

Both runs should produce:

```output
SQLSRV query: 1
PDO_SQLSRV query: 1
```

If the post-upgrade run fails, retain the exact SQLSTATE or exception. Then compare the failure with the baseline, extension versions, active configuration, ODBC version, and process restart status. Roll back both PHP extensions together when you can't correct the mismatch within the deployment window.

After verification, delete the test file and clear the temporary environment variables. In PowerShell, run:

```powershell
Remove-Item .\verify-driver-upgrade.php, .\test-driver-upgrade.php
Remove-Item Env:\SQL_SERVER_NAME, Env:\SQL_DATABASE, Env:\SQL_UID, Env:\SQL_PWD
```

On Linux or macOS, run:

```bash
rm verify-driver-upgrade.php test-driver-upgrade.php
unset SQL_SERVER_NAME SQL_DATABASE SQL_UID SQL_PWD
```

## Roll back an unsuccessful upgrade

1. Stop the affected web servers, PHP-FPM pools, and long-running workers.

1. If you changed the PHP minor version, restore the previous PHP runtime and point each web server and worker back to that installation.

1. Restore both previous driver files. Restore the previous SQLSRV and PDO_SQLSRV configuration entries together.

1. Restore the previous ODBC package only if the newer version caused the failure and the older package remains supported by the PHP driver version you restored.

1. Restart the processes and rerun the baseline version checks and application smoke tests.

1. Keep the failed upgrade logs and exact PHP startup or connection error. They help identify whether the failure belongs to PHP loading, the operating-system loader, ODBC, or the database connection.

## Troubleshoot an upgrade

| Symptom | Likely cause | Corrective action |
|---|---|---|
| PIE succeeds but `ExtensionVer` is unchanged | An old versioned entry still loads the previous file | Replace the old entry, remove duplicates, and restart all PHP processes |
| SQLSRV and PDO_SQLSRV report different versions | Only one extension was replaced or one SAPI has stale configuration | Upgrade and configure both extensions together in every runtime |
| CLI reports the target version but the website doesn't | The web SAPI uses another PHP installation or wasn't restarted | Compare configuration and extension directories, then restart the correct process |
| PHP startup reports an architecture or symbol error | The extension doesn't match the PHP build | Restore the previous files, then rebuild or select files for the correct PHP minor version, architecture, and thread-safety mode |
| Extensions load but connections fail after an ODBC change | ODBC configuration, certificate behavior, authentication, or network access changed | Capture the SQLSTATE and native error before rolling back or changing connection settings |
| Worker processes fail while new web requests succeed | Long-running workers still hold the previous extension or configuration | Drain and restart every worker process |

## Related content

- [Install the PHP drivers for SQL Server on Windows](windows-install.md)
- [Linux and macOS installation tutorial](installation-tutorial-linux-mac.md)
- [Loading the Microsoft Drivers for PHP for SQL Server](loading-the-php-sql-driver.md)
