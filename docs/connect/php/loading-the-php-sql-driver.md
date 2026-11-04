---
title: Load the Microsoft Drivers for PHP
description: Configure and verify the SQLSRV and PDO_SQLSRV extensions for PHP.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, sumitsar, jathakkar
ms.date: 11/02/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
helpviewer_keywords:
  - "loading the driver"
---

# Load the Microsoft Drivers for PHP for SQL Server

<a id="loading-the-microsoft-drivers-for-php-for-sql-server"></a>

The SQLSRV and PDO_SQLSRV drivers are PHP extensions. PHP loads them when the PHP process starts by reading `php.ini` and any additional `.ini` files.

Loading is separate from installation. Copying or building the extension places a file on disk. PHP can't use SQLSRV or PDO_SQLSRV until the active configuration contains the correct entry and the PHP process restarts.

## Protect web diagnostic pages

Before creating a temporary `phpinfo()` page, configure the web server to allow only localhost requests or authenticated administrators. Keep that restriction until you delete the page. `phpinfo()` can expose environment variables, credentials, and server configuration. Remove the page immediately after verification.

## Understand PHP configuration scope

Each PHP server application programming interface (SAPI) can use a different executable and configuration:

| Runtime | Typical configuration source | How to identify it |
|---|---|---|
| Command-line interface (CLI) | The `php.ini` and scan directory reported by `php --ini` | Run `php --ini` with the exact executable |
| IIS FastCGI | The PHP executable configured in the IIS FastCGI application | Use a temporary `phpinfo()` page |
| Apache module | Apache's PHP module configuration | Use a temporary `phpinfo()` page |
| PHP-FPM | The FPM executable, pool, `php.ini`, and scan directory | Run the versioned FPM binary with `-i` or use a temporary `phpinfo()` page |
| Long-running worker | The executable and environment that started the worker | Inspect the worker service and restart it after changes |

Changing the CLI configuration doesn't change IIS, Apache, or PHP-FPM when they use another PHP installation or scan directory.

## Find the PHP configuration

Run these commands with the PHP executable used by your application:

```console
php --ini
php -i | findstr /I "PHP Version Thread Safety Architecture extension_dir"
```

On Linux or macOS, replace `findstr` with `grep`:

```bash
php -i | grep -E "PHP Version|Thread Safety|extension_dir"
file "$(php -r 'echo PHP_BINARY;')"
```

The `file` command identifies the selected executable's architecture. For a universal macOS executable, follow [explicit architecture selection](system-requirements-for-the-php-sql-driver.md#choose-a-compatible-set).

The **Loaded Configuration File** and **Scan for additional .ini files in** values identify the configuration used by the command-line process. A web server or [PHP FastCGI Process Manager (PHP-FPM)](https://www.php.net/manual/en/install.fpm.php) process might use a different PHP executable or configuration file. Use a temporary `phpinfo()` page to identify that process's values.

Before editing a file, record:

- The PHP executable path and version.
- The loaded `php.ini`.
- The additional `.ini` scan directory.
- The extension directory.
- The thread-safety mode and architecture.
- Every process that you must restart.

Back up each configuration file that you change.

<a id="install-the-drivers-on-windows-with-pie"></a>

## Load the drivers on Windows

For an installation with the [PHP Installer for Extensions (PIE)](https://github.com/php/pie), first [download and verify PIE](windows-install.md#install-with-pie). Run the following commands from the directory containing `pie.phar`, with the PHP executable that should load the drivers:

```console
php .\pie.phar install microsoft/sqlsrv
php .\pie.phar install microsoft/pdo_sqlsrv
```

PIE installs `php_sqlsrv.dll` and `php_pdo_sqlsrv.dll`. If PIE can't enable the extensions automatically, remove any existing SQLSRV and PDO_SQLSRV entries from the active `php.ini`, then add:

```ini
extension=php_sqlsrv.dll
extension=php_pdo_sqlsrv.dll
```

Use only one entry for each driver. Restart the PHP process after changing its configuration.

<a id="install-prebuilt-drivers-manually"></a>

For a ZIP installation, copy the dynamic-link libraries (DLLs) that match the PHP minor version, architecture, and thread-safety mode into the `extension_dir` directory. A PHP 8.5 x64 thread-safe runtime uses these filenames:

```text
php_sqlsrv_85_ts_x64.dll
php_pdo_sqlsrv_85_ts_x64.dll
```

Add both filenames to the `php.ini` used by the PHP process:

```ini
extension=php_sqlsrv_85_ts_x64.dll
extension=php_pdo_sqlsrv_85_ts_x64.dll
```

Replace `85`, `ts`, and `x64` with the values for your PHP runtime. Don't mix thread-safe and non-thread-safe files, 32-bit and 64-bit files, or files built for another PHP minor version.

<a id="install-the-drivers-on-linux-and-macos"></a>

## Load the drivers on Linux and macOS

Install the drivers by using [PIE](https://github.com/php/pie). For more information, see the [Linux and macOS installation tutorial](installation-tutorial-linux-mac.md). PIE installs the extension files and attempts to enable them by using the platform's PHP configuration tools or by adding an `extension=` entry to an additional `.ini` file or the standard `php.ini`. If PIE reports that it couldn't enable an extension, add the entry yourself and verify the active configuration.

<a id="moving-the-driver-file-into-your-extension-directory"></a>

For a manual installation, select the prebuilt `.so` files that match your PHP version, architecture, thread-safety mode, and operating system's native libraries. Check the [prebuilt Linux and macOS requirements](system-requirements-for-the-php-sql-driver.md#linux-and-macos-driver-files); use PIE if they don't match your environment.

For example, PHP 8.4 non-thread-safe files are named `SQLSRV_8.4_NTS.so` and `PDO_SQLSRV_8.4_NTS.so`. Place them in the directory returned by `php -i | grep extension_dir`. Rename them to `sqlsrv.so` and `pdo_sqlsrv.so` to use the following entries, or use the exact package filenames in the entries instead. Add the entries to your active PHP configuration in the load order described in this section:

```ini
extension=sqlsrv.so
extension=pdo_sqlsrv.so
```

PHP Data Objects (PDO) must load before PDO_SQLSRV. PHP reads `php.ini` before scanning additional `.ini` files in alphabetical order. If your distribution loads `pdo.so` from a scanned file, place the PDO_SQLSRV entry in a later scanned file, such as `30-pdo_sqlsrv.ini` when PDO uses `10-pdo.ini`. Don't place the PDO_SQLSRV entry in `php.ini` in that case.

## Choose the extension entries

Use entries that match how the drivers were installed:

| Platform and method | SQLSRV entry | PDO_SQLSRV entry |
|---|---|---|
| Windows with PIE | `extension=php_sqlsrv.dll` | `extension=php_pdo_sqlsrv.dll` |
| Windows released ZIP | Exact versioned DLL name, such as `extension=php_sqlsrv_85_ts_x64.dll` | Exact versioned DLL name, such as `extension=php_pdo_sqlsrv_85_ts_x64.dll` |
| Linux or macOS with PIE | `extension=sqlsrv.so` | `extension=pdo_sqlsrv.so` |
| Linux or macOS released archive, renamed files | `extension=sqlsrv.so` | `extension=pdo_sqlsrv.so` |
| Linux or macOS released archive, original files | Exact archive filename, such as `extension=SQLSRV_8.4_NTS.so` | Exact archive filename, such as `extension=PDO_SQLSRV_8.4_NTS.so` |

Don't configure both a short name and a versioned name for the same extension. PHP can report that a module is already loaded, or it can continue loading an older file after an upgrade.

<a id="loading-the-driver-at-php-startup"></a>

## Restart PHP to load the drivers

Restart the process after changing an extension file or configuration file. Restart the web server, PHP-FPM, queue workers, and other long-running PHP processes that load the driver.

The SQLSRV and PDO_SQLSRV extensions can be loaded independently, but install and verify both when your application uses both drivers. PDO is built into many PHP packages. If PDO isn't built in, enable the PDO extension before PDO_SQLSRV.

## Verify the loaded drivers

For the command-line runtime, run:

```console
php --ri sqlsrv
php --ri pdo_sqlsrv
php -m | findstr /I "sqlsrv pdo_sqlsrv"
```

On Linux or macOS, use:

```bash
php --ri sqlsrv
php --ri pdo_sqlsrv
php -m | grep -E "sqlsrv|pdo_sqlsrv"
```

For a web server, [restrict diagnostic-page access](#protect-web-diagnostic-pages) before creating a temporary page containing `<?php phpinfo(); ?>`. Confirm the **Loaded Configuration File**, **extension_dir**, and separate `sqlsrv` and `pdo_sqlsrv` sections. Remove the page after verification.

Also run a PHP-level check so verification covers both APIs rather than only module names. Save this program as `verify-php-drivers.php`:

```php
<?php
$checks = [
    'SQLSRV extension' => extension_loaded('sqlsrv'),
    'SQLSRV API' => function_exists('sqlsrv_connect'),
    'PDO_SQLSRV extension' => extension_loaded('pdo_sqlsrv'),
    'PDO_SQLSRV API' => class_exists('PDO')
        && in_array('sqlsrv', PDO::getAvailableDrivers(), true),
];

$failed = false;
foreach ($checks as $name => $passed) {
    echo $name . ': ' . ($passed ? 'loaded' : 'missing') . PHP_EOL;
    $failed = $failed || !$passed;
}

exit($failed ? 1 : 0);
```

Run the program with each PHP executable that your application uses:

```console
php verify-php-drivers.php
```

Expected output:

```output
SQLSRV extension: loaded
SQLSRV API: loaded
PDO_SQLSRV extension: loaded
PDO_SQLSRV API: loaded
```

For a web runtime, place the file temporarily in the document root and request it in a browser. Remove it after verification. This program doesn't open a database connection or print configuration values.

Delete `verify-php-drivers.php` after you finish the CLI and web-runtime checks.

## Troubleshoot driver loading

| Symptom | Cause to check | Corrective action |
|---|---|---|
| `Unable to load dynamic library` | Wrong filename, `extension_dir`, PHP minor version, architecture, or thread-safety mode | Compare `php -i` with the selected driver file and use an absolute path temporarily to isolate path errors |
| `The specified module could not be found` on Windows | The driver DLL or one of its dependencies isn't available | Confirm the DLL is in `extension_dir`, install the matching Visual C++ runtime, and inspect any remaining missing DLL dependency |
| `undefined symbol` or shared-library error on Linux or macOS | Extension and PHP build mismatch, or missing ODBC dependency | Rebuild with the intended `php-config`, then inspect the reported missing library |
| `Module "sqlsrv" is already loaded` | Duplicate entries or two scanned files | Search the loaded `php.ini` and scan directory, then keep one entry |
| `could not find driver` from PDO | PDO_SQLSRV isn't loaded, or PDO loads after it | Check `php --ri pdo_sqlsrv` and move the PDO_SQLSRV entry after PDO |
| CLI works but web requests fail | Different SAPI configuration or a process that wasn't restarted | Compare CLI and `phpinfo()` configuration values, edit the web configuration, and restart the web process |
| Old `ExtensionVer` remains after an upgrade | The configuration still names an older versioned file | Replace the old entry with the file installed by the selected method, remove duplicates, and restart all PHP processes |

PHP owns extension discovery and load order. PIE or a manual copy supplies the extension file. The operating system loader resolves native dependencies. The Microsoft ODBC Driver owns the database protocol after the extension loads.

## Related content

- [Install the PHP drivers for SQL Server on Windows](windows-install.md)
- [System requirements for the Microsoft Drivers for PHP for SQL Server](system-requirements-for-the-php-sql-driver.md)
- [Upgrade the PHP drivers for SQL Server](upgrade-driver.md)
- [SQLSRV Driver API Reference](sqlsrv-driver-api-reference.md)
- [PDO_SQLSRV Driver Reference](pdo-sqlsrv-driver-reference.md)
