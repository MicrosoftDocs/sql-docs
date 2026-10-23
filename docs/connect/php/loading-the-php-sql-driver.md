---
title: Loading the Microsoft Drivers for PHP
description: Install and load the Microsoft Drivers for PHP for SQL Server.
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, sumitsar, jathakkar
ms.date: 09/17/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
ai-usage: ai-assisted
helpviewer_keywords:
  - "loading the driver"
---

# Loading the Microsoft Drivers for PHP for SQL Server

This page provides instructions for loading the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)] into the PHP process space.

## Install the drivers on Windows with PIE

[PIE](https://github.com/php/pie), the PHP Installer for Extensions, installs the matching precompiled driver files and enables them in the PHP command-line configuration. Windows doesn't need a build toolchain.

1. Install a [supported PHP version](microsoft-php-drivers-for-sql-server-support-matrix.md#php-version-support) and the [Microsoft ODBC Driver for SQL Server](../odbc/download-odbc-driver-for-sql-server.md).

1. Run `php --ini`. If **Loaded Configuration File** is `(none)`, copy **php.ini-development** from the PHP installation directory to **php.ini** in the same directory.

1. In **php.ini**, set the extension directory and enable the `openssl` and `zip` extensions:

   ```ini
   extension_dir = "ext"
   extension=openssl
   extension=zip
   ```

1. From a writable working directory, download the latest stable `pie.phar`. The command works in Command Prompt and PowerShell:

   ```console
   curl.exe -fL --output pie.phar https://github.com/php/pie/releases/latest/download/pie.phar
   ```

   If you have the [GitHub CLI](https://cli.github.com/), verify that the PHP Foundation published the file:

   ```console
   gh attestation verify --owner php .\pie.phar
   ```

1. Install both drivers by using the PHP executable on `PATH`:

   ```console
   php .\pie.phar install microsoft/sqlsrv
   php .\pie.phar install microsoft/pdo_sqlsrv
   ```

1. Verify that the same PHP command-line runtime loads both drivers:

   ```console
   php --ri sqlsrv
   php --ri pdo_sqlsrv
   ```

For other PIE installation methods, see the [PIE usage documentation](https://github.com/php/pie/blob/1.5.0/docs/usage.md).

## Install the drivers on Linux and macOS

Install the drivers and their platform dependencies with PIE as described in the [Linux and macOS installation tutorial](installation-tutorial-linux-mac.md).

## Install prebuilt drivers manually

You can download the prebuilt drivers from the [Microsoft Drivers for PHP for SQL Server](download-drivers-php-sql-server.md) page. The Windows download is a ZIP that contains SQLSRV and PDO_SQLSRV driver files for each supported PHP version, architecture, and thread-safety mode. Select the files that match your PHP environment. See [System requirements](system-requirements-for-the-php-sql-driver.md#driver-versions) for the file list.

You can also build the drivers from source either when building PHP or by using `phpize`. If you choose to build the drivers from source, you have the option of building them statically into PHP instead of building them as shared extensions by adding `--enable-sqlsrv=static --with-pdo_sqlsrv=static` (on Linux and macOS) or `--enable-sqlsrv=static --with-pdo-sqlsrv=static` (on Windows) to the `./configure` command when building PHP. For more information on the PHP build system and `phpize`, see the [PHP documentation](http://php.net/manual/install.php).

## Moving the driver file into your extension directory

The driver file must be located in a directory where the PHP runtime can find it. It's easiest to put the driver file in your default PHP extension directory - to find the default directory, run `php -i | sls extension_dir` on Windows or `php -i | grep extension_dir` on Linux/macOS. If you aren't using the default extension directory, specify a directory in the PHP configuration file (php.ini), using the **extension_dir** option. For example, on Windows, if you put the driver file in your `c:\php\ext` directory, add the following line to php.ini:

```ini
extension_dir = "c:\PHP\ext"
```

## Loading the driver at PHP startup

To load the SQLSRV driver when PHP is started, first move a driver file into your extension directory. Then, follow these steps:
  
1. To enable the **SQLSRV** driver, modify **php.ini** by adding the following line to the extension section, changing the filename as appropriate for your PHP version and thread safe versus non thread safe installation:

    On Windows:

    ```ini
    extension=php_sqlsrv_83_ts.dll
    ```

    On Linux, if you downloaded the prebuilt binaries for your distribution:

    ```ini
    extension=php_sqlsrv_83_nts.so
    ```

    If you compiled the SQLSRV binary from source, or installed it with PIE or PECL, its name is sqlsrv.so:

    ```ini
    extension=sqlsrv.so
    ```

1. To enable the **PDO_SQLSRV** driver, the PHP Data Objects (PDO) extension must be available, either as a built-in extension or as a dynamically loaded extension.

    On Windows, the prebuilt PHP binaries come with PDO built-in, so there's no need to modify php.ini to load it. If, however, you compiled PHP from source and specified a separate PDO extension to be built, its name is `php_pdo.dll`, and you must copy it to your extension directory and add the following line to php.ini:

    ```ini
    extension=php_pdo.dll
    ```

    On Linux, if you installed PHP using your system's package manager, PDO is probably installed as a dynamically loaded extension named pdo.so. The PDO extension must be loaded before the PDO_SQLSRV extension, or loading fails. Extensions are loaded using individual .ini files, and these files are read after php.ini. Therefore, if pdo.so is loaded through its own .ini file, a separate file loading the PDO_SQLSRV driver after PDO is required.

    To find out which directory the extension-specific .ini files are located, run `php --ini` and note the directory listed under `Scan for additional .ini files in:`. Find the file that loads pdo.so. It should be prefixed with a number, such as 10-pdo.ini. The numerical prefix indicates the loading order of the .ini files, while files that don't have a numerical prefix are loaded alphabetically. Create a file to load the PDO_SQLSRV driver file called either 30-pdo_sqlsrv.ini (any number larger than the one that prefixes pdo.ini works) or pdo_sqlsrv.ini (if pdo.ini isn't prefixed with a number), and add the following line to it, changing the filename as appropriate:

    ```ini
    extension=php_pdo_sqlsrv_3_nts.so
    ```

    As with SQLSRV, if you compiled the PDO_SQLSRV binary from source, or installed it with PIE or PECL, its name is pdo_sqlsrv.so:

    ```ini
    extension=pdo_sqlsrv.so
    ```

    Copy this file to the directory that contains the other .ini files.

    If you compiled PHP from source with built-in PDO support, you don't require a separate .ini file, and you can add the previous line to php.ini.

1. Restart the web server.

> [!NOTE]
> To determine whether the driver is successfully loaded, run a script that calls [phpinfo()](https://php.net/manual/en/function.phpinfo.php).

For more information about **php.ini** directives, see [Description of core php.ini directives](https://php.net/manual/en/ini.core.php).

## Related content

- [Getting Started with the Microsoft Drivers for PHP for SQL Server](getting-started-with-the-php-sql-driver.md)
- [System requirements for the Microsoft Drivers for PHP for SQL Server](system-requirements-for-the-php-sql-driver.md)
- [Programming Guide for the Microsoft Drivers for PHP for SQL Server](programming-guide-for-php-sql-driver.md)
- [SQLSRV Driver API Reference](sqlsrv-driver-api-reference.md)
- [PDO_SQLSRV Driver Reference](pdo-sqlsrv-driver-reference.md)
