---
title: "Loading the Microsoft Drivers for PHP for SQL Server | Microsoft Docs"
ms.custom: ""
ms.date: "03/26/2018"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "loading the driver"
ms.assetid: e5c114c5-8204-49c2-94eb-62ca63f5d3ec
author: MightyPen
ms.author: genemi
manager: craigg
---

# Loading the Microsoft Drivers for PHP for SQL Server
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

This page provides instructions for loading the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)] into the PHP process space.  
  
You can download the prebuilt drivers for your platform from the [Microsoft Drivers for PHP for SQL Server](https://github.com/Microsoft/msphpsql/releases) Github project page. Each installation package contains SQLSRV and PDO_SQLSRV driver files in threaded and non-threaded variants. On Windows, they are also available in 32-bit and 64-bit variants. See [System Requirements for the Microsoft Drivers for PHP for SQL Server](../../connect/php/system-requirements-for-the-php-sql-driver.md) for a list of the driver files that are contained in each package. The driver file must match the PHP version, architecture, and threadedness of your PHP environment.

On Linux and macOS, the drivers can alternatively be installed using PECL, as found in the [installation tutorial](../../connect/php/installation-tutorial-linux-mac.md).
  
## Moving the Driver File into Your Extension Directory  
The driver file must be located in a directory where the PHP runtime can find it. It is easiest to put the driver file in your default PHP extension directory - to find the default directory, run `php -i | sls extension_dir` on Windows or `php -i | grep extension_dir` on Linux/macOS. If you are not using the default extension directory, specify a directory in the PHP configuration file (php.ini), using the **extension_dir** option. For example, on Windows, if you have put the driver file in your `c:\php\ext` directory, add the following line to php.ini:
  
```  
extension_dir = "c:\PHP\ext"  
```

## Loading the Driver at PHP Startup  
To load the SQLSRV driver when PHP is started, first move a driver file into your extension directory. Then, follow these steps:  
  
1.  To enable the **SQLSRV** driver, modify **php.ini** by adding the following line to the extension section, changing the filename as appropriate:  
  
    On Windows: 
    ```  
    extension=php_sqlsrv_72_ts.dll  
    ```  
    On Linux, if you have downloaded the prebuilt binaries for your distribution: 
    ```  
    extension=php_sqlsrv_72_nts.so  
    ```
    If you have compiled the SQLSRV binary from source or with PECL, it will instead be named sqlsrv.so:
    ```
    extension=sqlsrv.so
    ```
  
2.  To enable the **PDO_SQLSRV** driver, the PHP Data Objects (PDO) extension must be available, either as a built-in extension, or as a dynamically loaded extension.

    On Windows, the prebuilt PHP binaries come with PDO built-in, so there is no need to modify php.ini to load it. If, however, you have compiled PHP from source and specified a separate PDO extension to be built, it will be named `php_pdo.dll`, and you must copy it to your extension directory and add the following line to php.ini:  
    ```
    extension=php_pdo.dll  
    ```
    On Linux, if you have installed PHP using your system's package manager, PDO is probably installed as a dynamically loaded extension named pdo.so. The PDO extension must be loaded before the PDO_SQLSRV extension, or loading will fail. Extensions are usually loaded using individual .ini files, and these files are read after php.ini. Therefore, if pdo.so is loaded through its own .ini file, a separate file loading the PDO_SQLSRV driver after PDO is required. 

    To find out which directory the extension-specific .ini files are located, run `php --ini` and note the directory listed under `Scan for additional .ini files in:`. Find the file that loads pdo.so -- it is likely prefixed by a number, such as 10-pdo.ini. The numerical prefix indicates the loading order of the .ini files, while files that do not have a numerical prefix are loaded alphabetically. Create a file to load the PDO_SQLSRV driver file called either 30-pdo_sqlsrv.ini (any number larger than the one that prefixes pdo.ini works) or pdo_sqlsrv.ini (if pdo.ini is not prefixed by a number), and add the following line to it, changing the filename as appropriate:  
    ```
    extension=php_pdo_sqlsrv_72_nts.so
    ```
    As with SQLSRV, if you have compiled the PDO_SQLSRV binary from source or with PECL, it will instead be named pdo_sqlsrv.so:
    ```
    extension=pdo_sqlsrv.so
    ```
    Copy this file to the directory that contains the other .ini files. 

    If you have compiled PHP from source with built-in PDO support, you do not require a separate .ini file, and you can add the appropriate line above to php.ini.
  
3.  Restart the Web server.  
  
> [!NOTE]  
> To determine whether the driver has been successfully loaded, run a script that calls [phpinfo()](https://php.net/manual/en/function.phpinfo.php).  
  
For more information about **php.ini** directives, see [Description of core php.ini directives](https://php.net/manual/en/ini.core.php).  
  
## See Also  
[Getting Started with the Microsoft Drivers for PHP for SQL Server](../../connect/php/getting-started-with-the-php-sql-driver.md)

[System Requirements for the Microsoft Drivers for PHP for SQL Server](../../connect/php/system-requirements-for-the-php-sql-driver.md)

[Programming Guide for the Microsoft Drivers for PHP for SQL Server](../../connect/php/programming-guide-for-php-sql-driver.md)

[SQLSRV Driver API Reference](../../connect/php/sqlsrv-driver-api-reference.md)

[PDO_SQLSRV Driver API Reference](../../connect/php/pdo-sqlsrv-driver-reference.md)  
  
