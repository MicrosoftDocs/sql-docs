---
title: "Loading the PHP SQL Driver | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "drivers"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "loading the driver"
ms.assetid: e5c114c5-8204-49c2-94eb-62ca63f5d3ec
caps.latest.revision: 42
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# Loading the PHP SQL Driver
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

This topic provides instructions for loading the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)] into the PHP process space.  
  
There are two options for loading a driver. The driver can be loaded when PHP is started or it can be loaded at PHP script runtime.  
  
## Moving the Driver File into Your Extension Directory  
Regardless of which procedure you use, the first step will be to put the driver file in a directory where the PHP runtime can find it. So, put the driver file in your PHP extension directory. See [System Requirements for the PHP SQL Driver](../../connect/php/system-requirements-for-the-php-sql-driver.md) for a list of the driver files that are installed with the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)].  
  
If necessary, specify the directory location of the driver file in the PHP configuration file (php.ini), using the **extension_dir** option. For example, if you will put the driver file in your c:\php\ext directory, use this option:  
  
```  
extension_dir = "c:\PHP\ext"  
```  
  
## Loading the Driver at PHP Startup  
To load the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)] when PHP is started, first move a driver file into your extension directory. Then, follow these steps:  
  
1.  To enable the **SQLSRV** driver, modify **php.ini** by adding the following line to the extension section, or modifying the line that is already there:  
  
    On Windows (this example uses the version 4.0 thread safe driver for PHP 7): 
    ```  
    extension=php_sqlsrv_7_ts.dll  
    ```  
    On Linux (this example uses the version as installed by PECL): 
    ```  
    extension=sqlsrv.so  
    ```  
    To enable the **PDO_SQLSRV** driver, modify **php.ini** by adding the following line to the extension section, or modifying the line that is already there:  
  
    On Windows (this example uses the version 4.0 thread safe driver for PHP 7):
    ```  
    extension=php_pdo_sqlsrv_7_ts.dll  
    ```  
    On Linux (this example uses the version as installed by PECL):
    ```  
    extension=pdo_sqlsrv.so  
    ```  
  
2.  If you want to use the **PDO_SQLSRV** driver, the PHP Data Objects (PDO) extension must be available, either as a built-in extension, or as a dynamically-loaded extension. If you need to load the PDO_SQLSRV driver dynamically, the php_pdo.dll (or pdo.so on Linux) must be present in the extension directory and the following line needs to be in the php.ini:

    On Windows:  
    ```
    extension=php_pdo.dll  
    ```  
    On Linux:  
    ```
    extension=pdo.so  
    ```  
  
3.  Restart the Web server.  
  
> [!NOTE]  
> To determine whether the driver has been successfully loaded, run a script that calls [phpinfo()](http://go.microsoft.com/fwlink/?LinkId=108678).  
  
For more information about **php.ini** directives, see [Description of core php.ini directives](http://go.microsoft.com/fwlink/?LinkId=105817).  
  
## Loading the Driver at PHP Script Runtime  
To load the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)] at script runtime, first move a driver file into your extension directory. Then include the following line at the start of the PHP script that matches the filename of the driver:  
  
```  
dl('php_pdo_sqlsrv_7_ts.dll');  
```  
  
For more information about PHP functions related to dynamically loading extensions, see [dl](http://go.microsoft.com/fwlink/?LinkId=105818) and [extension_loaded.](http://go.microsoft.com/fwlink/?LinkId=105819)  
  
## See Also  
[Getting Started with the PHP SQL Driver](../../connect/php/getting-started-with-the-php-sql-driver.md)
[System Requirements for the PHP SQL Driver](../../connect/php/system-requirements-for-the-php-sql-driver.md)
[Programming Guide for PHP SQL Driver](../../connect/php/programming-guide-for-php-sql-driver.md)
[SQLSRV Driver API Reference](../../connect/php/sqlsrv-driver-api-reference.md)  
  
