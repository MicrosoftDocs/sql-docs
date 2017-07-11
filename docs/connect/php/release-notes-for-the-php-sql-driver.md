---
title: "Release Notes for the PHP SQL Driver | Microsoft Docs"
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
  - "what's new in version 1.1"
ms.assetid: 91cca3d2-ba99-4a6d-b0de-beb9699cb3f8
caps.latest.revision: 37
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# Release Notes for the PHP SQL Driver
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

This topic discusses what was added in the each version of the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)].  
## What's New in Version 4.0  
Support for PHP 7.0  

Full 64-bit support

## What's New in Version 3.2  
Support for PHP 5.6  
  
Includes latest updates for prior PHP versions 5.5 and 5.4  
  
Requires Microsoft ODBC Driver 11 for SQL Server  
  
## What's New in Version 3.1  
Support for PHP 5.5  
  
Requires Microsoft ODBC Driver 11 for SQL Server. Previous versions required SQL Native Client.  
  
## What's New in Version 3.0  
Support for PHP 5.4.  PHP 5.2 is not supported in version 3 of the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)].  
  
AttachDBFileName connection option is added. For more information, see [Connection Options](../../connect/php/connection-options.md).  
  
Support for LocalDB, which was added in [!INCLUDE[ssSQL11](../../includes/sssql11_md.md)]. For more information, see [PHP Driver for SQL Server Support for LocalDB](../../connect/php/php-driver-for-sql-server-support-for-localdb.md).  
  
AttachDBFileName connection option is added. For more information, see [Connection Options](../../connect/php/connection-options.md).  
  
Support for the high-availability, disaster recovery features. For more information, see [PHP Driver for SQL Server Support for High Availability, Disaster Recovery](../../connect/php/php-driver-for-sql-server-support-for-high-availability-disaster-recovery.md).  
  
Support for client-side cursors (caching a result set in-memory). For more information, see [Cursor Types &#40;SQLSRV Driver&#41;](../../connect/php/cursor-types-sqlsrv-driver.md) and [Cursor Types &#40;PDO_SQLSRV Driver&#41;](../../connect/php/cursor-types-pdo-sqlsrv-driver.md).  
  
The PDO::ATTR_EMULATE_PREPARES attribute has been added.  See [PDO::prepare](../../connect/php/pdo-prepare.md) for more information.  
  
## What's New in Version 2.0  
In version 2.0, support for the PDO_SQLSRV driver was added. For more information, see [PDO_SQLSRV Driver Reference](../../connect/php/pdo-sqlsrv-driver-reference.md).  
  
## See Also  
[Overview of the PHP SQL Driver](../../connect/php/overview-of-the-php-sql-driver.md)
  
