---
title: "Always Encrypted: Limitations | Microsoft Docs"
ms.date: "01/31/2018"
ms.prod: "sql-non-specified"
ms.prod_service: "drivers"
ms.service: ""
ms.component: "php"
ms.suite: "sql"
ms.custom: ""
ms.technology: 
  - "drivers"
ms.topic: "article"
author: "yuki"
ms.author: "v-kaywon"
manager: "mbarwin"
ms.workload: "Inactive"
---
# Always Encrypted: Limitations
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

The following lists are limitations in SQL Server and the SQLSRV and PDO_SQLSRV drivers regarding Always Encrypted.

## SQLSRV and PDO_SQLSRV
 -   Scrollable cursor
 -   non-UTF8 locales in Linux and macOS
 
## SQLSRV
 -   `sqlsrv_query` for binding parameter without specifying the SQL type
 -   binding parameters in a batch of SQL statements with `sqlsrv_prepare`
 
## PDO_SQLSRV
 -   `PDO::SQLSRV_ATTR_DIRECT_QUERY` statement attribute specified in a parameterized query
 -   `PDO::ATTR_EMULATE_PREPARE` statement attribute specified in a parameterized query
 -   binding parameters in a batch of SQL statements
 
## Data Types
 -   money
 -   smallmoney
 -   text
 -   ntext
 -   xml
 -   image
 -   sql_variant
 
## SQL Commands
 -   arithmetic operations
 -   pattern matching operations (e.g., LIKE operator)
 -   greater/less than
 -   ORDER BY
 -   CLUSTERED INDEX on randomized encrypted columns
 -   equality in randomized encrypted columns
 -   SPARSE column set
 -   parameters in the SELECT list
  
## See Also  
[Programming Guide for PHP SQL Driver](../../connect/php/programming-guide-for-php-sql-driver.md)
[Enabling Always Encrypted in a PHP Application](../../connect/php/enabling-always-encrypted-php-application.md) 
[Inserting and Modifying Data with Always Encrypted](../../connect/php/inserting-data-always-encrypted.md)  
[Retrieving Data with Always Encrypted](../../connect/php/retrieving-data-always-encrypted.md)  
  
