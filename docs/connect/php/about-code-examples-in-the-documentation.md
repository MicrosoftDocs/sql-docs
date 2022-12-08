---
title: "About Code Examples in the Documentation"
description: "There are several points to note when you execute the code examples in the Microsoft Drivers for PHP for SQL Server documentation."
author: David-Engel
ms.author: v-davidengel
ms.date: "03/26/2018"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# About Code Examples in the Documentation
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

## Remarks about the code examples
There are several points to note when you execute the code examples in the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)] documentation:  
  
-   Nearly all the examples assume that SQL Server 2008 or later and the AdventureWorks database are installed on the local computer.  
  
    For information about how to download free editions and trial versions of SQL Server, see [SQL Server](https://go.microsoft.com/fwlink/?LinkID=120193).  
  
    For information about how to download and install the AdventureWorks database, see the [AdventureWorks page in the SQL Server Samples GitHub repository](https://github.com/Microsoft/sql-server-samples/tree/master/samples/databases/adventure-works).
  
-   Nearly all the code examples in this documentation are intended to be run from the command line, which enables automated testing of all the code examples. For information about how to run PHP from the command line, see [Using PHP from the command line](https://php.net/manual/en/features.commandline.php).  
  
-   Although examples are meant to be run from the command line, each example can be run by invoking it from a browser without making any changes to the script. To format output nicely, replace each "\n" with "\<\/br>" in each example before invoking it from a browser.  
  
-   For the purpose of keeping each example narrowly focused, correct error handling is not done in all examples. It is recommended that any call to a **sqlsrv** function or PDO method be checked for errors and handled according to the needs of the application.  
  
    An easy way to obtain error information when an error is encountered is to exit the script with the following line of code:  
  
    ```  
    die( print_r( sqlsrv_errors(), true));  
    ```  
  
    Or, if you are using PDO,  
  
    ```  
    print_r ($stmt->errorInfo());  
    die();  
    ```  
  
    For more information about handling errors and warnings, see [Handling Errors and Warnings](handling-errors-and-warnings.md).  
  
## See Also  
[Overview of the Microsoft Drivers for PHP for SQL Server](overview-of-the-php-sql-driver.md)
  
