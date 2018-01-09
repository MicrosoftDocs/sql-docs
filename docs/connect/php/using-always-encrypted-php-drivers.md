---
title: "Using Always Encrypted with the PHP Drivers for SQL Server | Microsoft Docs"
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
# Using Always Encrypted with the PHP Drivers for SQL Server
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

The topics in this section discuss how to develop PHP applications using [Always Encrypted](https://docs.microsoft.com/en-us/sql/relational-databases/security/encryption/always-encrypted-database-engine) and the PHP Driver for SQL Server (5.2 or later versions).

Always Encrypted allows client application to encrypt data and never reveal the encryption keys or data to the SQL Server. The PHP Driver for SQL Server fulfills this by transparently encrypting and decrypting sensitive data in the client application. Two notable differences between writing a usual PHP application and using Always Encrypted is user must include new Always Enctyped released connection options in the connection string, and user must use parameterized query when passing in sensitive data to the database.

## In This Section

|Topic|Description|
|[Enabling Always Encrypted in a PHP Application](../../connect/php/enabling-always-encrypted-php-application.md)|Describes how to connect with Always Encrypted enabled.|
|[Inserting and Modifying Data with Always Encrypted](../../connect/php/inserting-data-always-encrypted.md)|Describes how to insert data using Always Encrypted in a PHP application.|
|[Retrieving Data with Always Encrypted](../../connect/php/retrieving-data-always-encrypted.md)|Describes how to retrieve data using Always Encrypted in a PHP application.|
|Always Encrypted: Limitations](../../connect/php/always-encrypted-limitations.md)|Lists of limitations using Always Encrypted in a PHP application.|
  
## See Also  
[Programming Guide for PHP SQL Driver](../../connect/php/programming-guide-for-php-sql-driver.md)
[SQLSRV Driver API Reference](../../connect/php/sqlsrv-driver-api-reference.md)  
[PDO_SQLSRV Driver API Reference](../../connect/php/pdo-sqlsrv-driver-reference.md)  
  
