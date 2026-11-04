---
title: System Requirements
description: The Microsoft Drivers for PHP for SQL Server support a wide range of PHP versions, operating systems, and SQL Server versions.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, sumitsar, jathakkar
ms.date: 11/02/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: concept-article
ai-usage: ai-assisted
helpviewer_keywords:
  - "requirements"
---

# System requirements for the Microsoft Drivers for PHP for SQL Server

[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

This article lists the components that you must install on your system to access data in SQL Server or Azure SQL Database by using either the SQLSRV or PDO_SQLSRV driver.

For more information about support life cycles and requirements of the PHP drivers, see the [support matrix](microsoft-php-drivers-for-sql-server-support-matrix.md). The SQLSRV and PDO_SQLSRV drivers in each release have the same PHP, operating system, architecture, thread-safety, and ODBC requirements.

## How the driver stack fits together

A PHP application doesn't connect directly to the database server. Each layer must be compatible with the layer next to it:

| Layer | What you select or configure | What a mismatch looks like |
|---|---|---|
| Application | SQLSRV functions or PDO_SQLSRV classes and attributes | Undefined functions, unavailable PDO driver, or application-level errors |
| PHP extension | SQLSRV and PDO_SQLSRV files built for the PHP minor version, architecture, and thread-safety mode | PHP startup warning, module load failure, or process crash |
| PHP runtime | Command-line interface (CLI), Apache module, PHP-FPM, or IIS FastCGI configuration | The CLI loads the drivers but the web application doesn't |
| Microsoft ODBC Driver | A version supported by the selected PHP driver release | Connection failures or a missing ODBC library |
| Operating system | A supported release and architecture with required runtime and build packages | No compatible package, unresolved shared library, or source-build failure |
| Database product | A supported SQL Server or Azure SQL target | Server-version or feature-specific errors after a connection is established |

SQLSRV and PDO_SQLSRV provide different PHP APIs over the same ODBC driver. Install both when your application uses both APIs. Installing one extension doesn't automatically install or enable the other.

| API | Programming model | Installation-specific check |
|---|---|---|
| SQLSRV | Procedural `sqlsrv_*` functions and SQLSRV constants | `extension_loaded('sqlsrv')` and `function_exists('sqlsrv_connect')` |
| PDO_SQLSRV | PHP Data Objects (PDO) classes, attributes, and exceptions | `extension_loaded('pdo_sqlsrv')` and `sqlsrv` in `PDO::getAvailableDrivers()` |

The extensions can be installed independently. PDO_SQLSRV also requires PDO to load before PDO_SQLSRV.

## Choose a compatible set

Before you download or build a driver, complete these checks:

1. Identify every PHP runtime that serves the application. A scheduled task, command-line worker, IIS site, Apache module, and PHP-FPM pool can use different PHP executables and configuration files.

1. Run the following command with each PHP executable:

   ```console
   php -i | findstr /I "PHP Version Architecture Thread Safety extension_dir"
   ```

   On Linux or macOS, use:

   ```bash
   php -i | grep -E "PHP Version|Thread Safety|extension_dir"
   file "$(php -r 'echo PHP_BINARY;')"
   ```

   The `file` command reports the executable's architecture. If it reports a universal macOS executable, explicitly select the intended architecture, such as `arch -arm64 php --version` or `arch -x86_64 php --version`. Use the same architecture selection when building, loading, and testing the extensions.

1. Select a PHP driver release that supports the PHP minor version, operating system, and database target.

1. Select a supported Microsoft ODBC Driver version from the compatibility table in this article.

1. Match the extension file or PIE build to the PHP architecture and thread-safety mode. Don't copy an extension from another PHP installation unless all of these values match.

1. Configure and verify the CLI and web-server runtimes separately. A successful `php -m` command proves only that the CLI runtime loaded the extension.

## PHP

Download PHP binaries from the [PHP website](https://php.net). Choose a PHP version listed in [PHP version support](microsoft-php-drivers-for-sql-server-support-matrix.md#php-version-support).

- Enable the driver file that matches your PHP version. For filenames, see [Driver versions](#driver-versions). Download the files from [Download the Microsoft Drivers for PHP for SQL Server](download-drivers-php-sql-server.md). For configuration instructions, see [Load the Microsoft Drivers for PHP for SQL Server](loading-the-php-sql-driver.md).

- Choose a web server and PHP integration method supported by PHP and your application framework. Follow the [PHP installation documentation](https://www.php.net/manual/en/install.php) and your framework's deployment instructions to configure the web server to run PHP. Verify extension loading in the web-server runtime separately from the CLI.

### Thread safety and architecture

The PHP build and extension file must agree on both values.

| PHP value | Required driver choice |
|---|---|
| `Thread Safety => enabled` | Thread-safe (`ts` or `TS`) driver file |
| `Thread Safety => disabled` | Non-thread-safe (`nts` or `NTS`) driver file |
| `Architecture => x64` | 64-bit driver file and 64-bit ODBC components |
| `Architecture => x86` | 32-bit Windows driver file and 32-bit ODBC components |

The CLI can use either a thread-safe or non-thread-safe PHP build. Select the extension by the reported value, not by assuming that every CLI build is non-thread-safe.

## ODBC driver

Install a supported Microsoft ODBC Driver for SQL Server on the computer that runs PHP. Download the driver from [Download ODBC Driver for SQL Server](../odbc/download-odbc-driver-for-sql-server.md).

If you're downloading the Windows version of the driver on a 64-bit version of Windows, the ODBC 64-bit installer installs both 32-bit and 64-bit ODBC drivers. If you use a 32-bit version of Windows, use the ODBC x86 installer. On non-Windows platforms, only 64-bit versions of the driver are available.

The table includes historical combinations for upgrade planning. Use the current PHP driver release and its listed ODBC versions for a new installation. Compatibility with an older release doesn't extend its support lifecycle. Check both the [PHP driver support lifecycle](microsoft-php-drivers-for-sql-server-support-matrix.md#microsoft-php-drivers-support-lifecycle-matrix-and-policy) and [PHP's supported versions](https://www.php.net/supported-versions.php).

A blank cell doesn't mean the PHP driver necessarily rejects that ODBC version. Individual features can require newer ODBC versions than the base combination.

|PHP driver version &#8594;<br />&#8595; ODBC driver version|5.13|5.12|5.11|5.10|5.9|5.8|5.6|5.3|5.2|4.3|4.0|3.2|
|------------------|---|---|---|---|---|---|---|---|---|---|---|---|
|ODBC Driver 18    |Yes|Yes|Yes|Yes|   |   |   |   |   |   |   |   |
|ODBC Driver 17    |Yes|Yes|Yes|Yes|Yes|Yes|Yes|Yes|Yes|   |   |   |
|ODBC Driver 13.1  |   |Yes|Yes|Yes|Yes|Yes|Yes|Yes|Yes|Yes|Yes|   |
|ODBC Driver 13    |   |   |   |   |   |   |   |   |   |   |Yes|   |
|ODBC Driver 11    |   |   |   |   |   |Yes|Yes|Yes|Yes|Yes|Yes|Yes|

If you're using the SQLSRV driver, [sqlsrv_client_info](sqlsrv-client-info.md) returns information about which version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Microsoft ODBC Driver for SQL Server is being used by the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)]. If you're using the PDO_SQLSRV driver, you can use [PDO::getAttribute](pdo-getattribute.md) to discover the version.

## SQL Server

For supported SQL Server versions, see [Supported database versions](microsoft-php-drivers-for-sql-server-support-matrix.md#sql-server-version-certified-compatibility).

Evaluate target support separately for SQL Server, Azure SQL Database, Azure SQL Managed Instance, Azure Synapse Analytics, SQL database in Microsoft Fabric, and Warehouse in Microsoft Fabric. Installation requirements are client-side requirements and don't establish that every authentication method, connection option, Transact-SQL (T-SQL) feature, or data type is available on every target. Use the support matrix and the target product's documentation for the scenario your application uses.

## Operating systems

For supported operating systems, see [Supported operating systems](microsoft-php-drivers-for-sql-server-support-matrix.md#supported-operating-systems).

## Driver versions

Each release of the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)] ships SQLSRV and PDO_SQLSRV driver files in thread-safe and non-thread-safe variants. On Windows, each variant is also built for 32-bit and 64-bit PHP. Match the driver file to your PHP version, thread-safety mode, and architecture. To enable a driver file after you download it, see [Loading the Microsoft Drivers for PHP for SQL Server](loading-the-php-sql-driver.md).

On Linux and macOS, install the drivers with PIE, the PHP Installer for Extensions, as described in the [Linux and macOS installation instructions](installation-tutorial-linux-mac.md). PIE replaces the deprecated [PHP Extension Community Library (PECL)](https://pecl.php.net/) package system and names the installed extension `sqlsrv.so` or `pdo_sqlsrv.so`. The following tables list the filenames for the prebuilt binary packages that you manually download from the [Microsoft Drivers for PHP for SQL Server releases](https://github.com/Microsoft/msphpsql/releases) page.

The following tables list the files in the 5.13 release packages. In filenames, `ts` and `TS` mean thread-safe, and `nts` and `NTS` mean non-thread-safe.

### Windows driver files

The `Windows_5.13.xRTW.zip` package contains these files.

| SQLSRV file | PDO_SQLSRV file | PHP version | Thread safe? | Architecture |
|---|---|:---:|:---:|:---:|
| `php_sqlsrv_83_nts_x86.dll` | `php_pdo_sqlsrv_83_nts_x86.dll` | 8.3 | no | 32-bit |
| `php_sqlsrv_83_ts_x86.dll` | `php_pdo_sqlsrv_83_ts_x86.dll` | 8.3 | yes | 32-bit |
| `php_sqlsrv_83_nts_x64.dll` | `php_pdo_sqlsrv_83_nts_x64.dll` | 8.3 | no | 64-bit |
| `php_sqlsrv_83_ts_x64.dll` | `php_pdo_sqlsrv_83_ts_x64.dll` | 8.3 | yes | 64-bit |
| `php_sqlsrv_84_nts_x86.dll` | `php_pdo_sqlsrv_84_nts_x86.dll` | 8.4 | no | 32-bit |
| `php_sqlsrv_84_ts_x86.dll` | `php_pdo_sqlsrv_84_ts_x86.dll` | 8.4 | yes | 32-bit |
| `php_sqlsrv_84_nts_x64.dll` | `php_pdo_sqlsrv_84_nts_x64.dll` | 8.4 | no | 64-bit |
| `php_sqlsrv_84_ts_x64.dll` | `php_pdo_sqlsrv_84_ts_x64.dll` | 8.4 | yes | 64-bit |
| `php_sqlsrv_85_nts_x86.dll` | `php_pdo_sqlsrv_85_nts_x86.dll` | 8.5 | no | 32-bit |
| `php_sqlsrv_85_ts_x86.dll` | `php_pdo_sqlsrv_85_ts_x86.dll` | 8.5 | yes | 32-bit |
| `php_sqlsrv_85_nts_x64.dll` | `php_pdo_sqlsrv_85_nts_x64.dll` | 8.5 | no | 64-bit |
| `php_sqlsrv_85_ts_x64.dll` | `php_pdo_sqlsrv_85_ts_x64.dll` | 8.5 | yes | 64-bit |

Use a non-thread-safe driver file with a non-thread-safe PHP build, and a thread-safe driver file with a thread-safe PHP build. The command-line interface (CLI) can use either build. To confirm which build you have, run `php -i` and check the **Thread Safety** value.

### Linux and macOS driver files

The `Linux_5.13.xRTW.tar.gz` and `Mac_5.13.xRTW.tar.gz` packages contain these files. The Linux archive contains x64 binaries, and the macOS archive contains Arm64 binaries.

Prebuilt files must also match your operating system's native libraries. The Linux 5.13.3 binaries require glibc 2.38 and later versions and a C++ runtime that provides `GLIBCXX_3.4.32`. The macOS 5.13.3 binaries declare macOS 26 as their minimum deployment target. These binary requirements are separate from operating-system support for source builds.

Use PIE when the archive's architecture or native-library requirements don't match your PHP installation. PIE builds for the installed PHP and native libraries. Don't replace system libraries manually to load a prebuilt extension.

| SQLSRV file | PDO_SQLSRV file | PHP version | Thread safe? |
|---|---|:---:|:---:|
| `SQLSRV_8.3_NTS.so` | `PDO_SQLSRV_8.3_NTS.so` | 8.3 | no |
| `SQLSRV_8.3_TS.so` | `PDO_SQLSRV_8.3_TS.so` | 8.3 | yes |
| `SQLSRV_8.4_NTS.so` | `PDO_SQLSRV_8.4_NTS.so` | 8.4 | no |
| `SQLSRV_8.4_TS.so` | `PDO_SQLSRV_8.4_TS.so` | 8.4 | yes |
| `SQLSRV_8.5_NTS.so` | `PDO_SQLSRV_8.5_NTS.so` | 8.5 | no |
| `SQLSRV_8.5_TS.so` | `PDO_SQLSRV_8.5_TS.so` | 8.5 | yes |

Copy each file you need into your PHP extension directory. You can keep the package filename and write `extension=SQLSRV_8.4_NTS.so`, or rename the file to `sqlsrv.so` and write the short form `extension=sqlsrv`. Apply the same naming choice to PDO_SQLSRV. To find the extension directory, run `php -i | grep extension_dir`. Follow [Load the drivers on Linux and macOS](loading-the-php-sql-driver.md#load-the-drivers-on-linux-and-macos) to enable the extensions in the correct configuration file and load PHP Data Objects (PDO) before PDO_SQLSRV.

### Earlier releases

For the driver files included in releases before 5.13, download the package for that release from the [Microsoft Drivers for PHP for SQL Server releases](https://github.com/Microsoft/msphpsql/releases) page and list its contents. Releases before 5.12 publish a separate package for each operating system and PHP version instead of a single package per platform.

## Verify the selected environment

Run these checks after installing the PHP extensions and ODBC driver, but before configuring a web server:

```console
php --version
php --ini
php --ri sqlsrv
php --ri pdo_sqlsrv
```

The last two commands must display separate module-information sections. If either command reports that the extension isn't present, finish the platform installation and loading procedure before testing a database connection.

Create `verify-php-drivers.php` to check both PHP APIs without requiring credentials:

```php
<?php
$failures = [];

if (!extension_loaded('sqlsrv') || !function_exists('sqlsrv_connect')) {
    $failures[] = 'SQLSRV is not loaded.';
}

if (!extension_loaded('pdo_sqlsrv') || !in_array('sqlsrv', PDO::getAvailableDrivers(), true)) {
    $failures[] = 'PDO_SQLSRV is not loaded.';
}

echo $failures === []
    ? "SQLSRV and PDO_SQLSRV are loaded." . PHP_EOL
    : implode(PHP_EOL, $failures) . PHP_EOL;

exit($failures === [] ? 0 : 1);
```

Run it with the PHP executable that serves the application:

```console
php verify-php-drivers.php
```

Expected output:

```output
SQLSRV and PDO_SQLSRV are loaded.
```

This check verifies extension loading. It doesn't verify ODBC connectivity, authentication, certificate trust, network access, or database permissions.

Delete `verify-php-drivers.php` after you record the result.

## Diagnose requirement failures

| Symptom | Likely owner | Check |
|---|---|---|
| `PHP Startup: Unable to load dynamic library` | PHP runtime or extension selection | PHP minor version, architecture, thread-safety mode, `extension_dir`, and exact filename |
| `Call to undefined function sqlsrv_connect()` | SQLSRV extension configuration | `php --ri sqlsrv`, the active `php.ini`, and scanned `.ini` files |
| `PDOException: could not find driver` | PDO_SQLSRV extension or PDO load order | `php --ri pdo_sqlsrv`, `PDO::getAvailableDrivers()`, and whether PDO loads first |
| Missing `msodbcsql` or `libodbc` library | ODBC installation or operating-system loader | Installed ODBC package, architecture, and shared-library dependencies |
| CLI succeeds but the website fails | SAPI-specific PHP configuration | Compare `php --ini` with a temporary `phpinfo()` page after [restricting diagnostic-page access](loading-the-php-sql-driver.md#protect-web-diagnostic-pages) |
| Extension loads but connection fails | ODBC, network, authentication, certificate, or database target | Capture the SQLSRV error collection or PDO exception and troubleshoot the reported SQLSTATE |

## Related content

- [Getting Started with the Microsoft Drivers for PHP for SQL Server](getting-started-with-the-php-sql-driver.md)
- [Programming Guide for the Microsoft Drivers for PHP for SQL Server](programming-guide-for-php-sql-driver.md)
- [SQLSRV Driver API Reference](sqlsrv-driver-api-reference.md)
- [PDO_SQLSRV Driver Reference](pdo-sqlsrv-driver-reference.md)
