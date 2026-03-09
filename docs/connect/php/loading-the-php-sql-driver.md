---
title: "Loading the Microsoft Drivers for PHP"
description: "This page provides instructions for loading the Microsoft Drivers for PHP for SQL Server into the PHP process space."
author: David-Engel
ms.author: davidengel
ms.date: 02/24/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: how-to
helpviewer_keywords:
  - "loading the driver"
---

# Loading the Microsoft Drivers for PHP for SQL Server

[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

This page provides instructions for loading the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)] into the PHP process space.
  
You can download the prebuilt drivers for your platform from the [Microsoft Drivers for PHP for SQL Server](https://github.com/Microsoft/msphpsql/releases) GitHub project page. Each installation package contains SQLSRV and PDO_SQLSRV driver files in threaded and non threaded variants. On Windows, they're also available in 32-bit and 64-bit variants. See [System Requirements for the Microsoft Drivers for PHP for SQL Server](system-requirements-for-the-php-sql-driver.md) for a list of the driver files that are contained in each package. The driver file must match the PHP version, architecture, and threadedness of your PHP environment.

On Linux and macOS, the drivers can alternatively be installed using PECL, as found in the [installation tutorial](installation-tutorial-linux-mac.md).

You can also build the drivers from source either when building PHP or by using `phpize`. If you choose to build the drivers from source, you have the option of building them statically into PHP instead of building them as shared extensions by adding `--enable-sqlsrv=static --with-pdo_sqlsrv=static` (on Linux and macOS) or `--enable-sqlsrv=static --with-pdo-sqlsrv=static` (on Windows) to the `./configure` command when building PHP. For more information on the PHP build system and `phpize`, see the [PHP documentation](http://php.net/manual/install.php).

## Moving the driver file into your extension directory

The driver file must be located in a directory where the PHP runtime can find it. It's easiest to put the driver file in your default PHP extension directory - to find the default directory, run `php -i | sls extension_dir` on Windows or `php -i | grep extension_dir` on Linux/macOS. If you aren't using the default extension directory, specify a directory in the PHP configuration file (php.ini), using the **extension_dir** option. For example, on Windows, if you put the driver file in your `c:\php\ext` directory, add the following line to php.ini:

```
extension_dir = "c:\PHP\ext"
```

## Loading the driver at PHP startup

To load the SQLSRV driver when PHP is started, first move a driver file into your extension directory. Then, follow these steps:
  
1. To enable the **SQLSRV** driver, modify **php.ini** by adding the following line to the extension section, changing the filename as appropriate for your PHP version and thread safe versus non thread safe installation:

    On Windows:

    ```
    extension=php_sqlsrv_83_ts.dll
    ```

    On Linux, if you downloaded the prebuilt binaries for your distribution:

    ```
    extension=php_sqlsrv_83_nts.so
    ```

    If you compiled the SQLSRV binary from source or with PECL, its name is sqlsrv.so:

    ```
    extension=sqlsrv.so
    ```

2. To enable the **PDO_SQLSRV** driver, the PHP Data Objects (PDO) extension must be available, either as a built-in extension, or as a dynamically loaded extension.

    On Windows, the prebuilt PHP binaries come with PDO built-in, so there's no need to modify php.ini to load it. If, however, you compiled PHP from source and specified a separate PDO extension to be built, its name is `php_pdo.dll`, and you must copy it to your extension directory and add the following line to php.ini:

    ```
    extension=php_pdo.dll
    ```

    On Linux, if you installed PHP using your system's package manager, PDO is probably installed as a dynamically loaded extension named pdo.so. The PDO extension must be loaded before the PDO_SQLSRV extension, or loading fails. Extensions are loaded using individual .ini files, and these files are read after php.ini. Therefore, if pdo.so is loaded through its own .ini file, a separate file loading the PDO_SQLSRV driver after PDO is required.

    To find out which directory the extension-specific .ini files are located, run `php --ini` and note the directory listed under `Scan for additional .ini files in:`. Find the file that loads pdo.so. It should be prefixed with a number, such as 10-pdo.ini. The numerical prefix indicates the loading order of the .ini files, while files that don't have a numerical prefix are loaded alphabetically. Create a file to load the PDO_SQLSRV driver file called either 30-pdo_sqlsrv.ini (any number larger than the one that prefixes pdo.ini works) or pdo_sqlsrv.ini (if pdo.ini isn't prefixed with a number), and add the following line to it, changing the filename as appropriate:

    ```
    extension=php_pdo_sqlsrv_3_nts.so
    ```

    As with SQLSRV, if you compiled the PDO_SQLSRV binary from source or with PECL, its name is pdo_sqlsrv.so:

    ```
    extension=pdo_sqlsrv.so
    ```

    Copy this file to the directory that contains the other .ini files.

    If you compiled PHP from source with built-in PDO support, you don't require a separate .ini file, and you can add the previous line to php.ini.

3. Restart the Web server.

> [!NOTE]
> To determine whether the driver is successfully loaded, run a script that calls [phpinfo()](https://php.net/manual/en/function.phpinfo.php).

For more information about **php.ini** directives, see [Description of core php.ini directives](https://php.net/manual/en/ini.core.php).

## See also

[Getting Started with the Microsoft Drivers for PHP for SQL Server](getting-started-with-the-php-sql-driver.md)

[System Requirements for the Microsoft Drivers for PHP for SQL Server](system-requirements-for-the-php-sql-driver.md)

[Programming Guide for the Microsoft Drivers for PHP for SQL Server](programming-guide-for-php-sql-driver.md)

[SQLSRV Driver API Reference](sqlsrv-driver-api-reference.md)

[PDO_SQLSRV Driver API Reference](pdo-sqlsrv-driver-reference.md)
