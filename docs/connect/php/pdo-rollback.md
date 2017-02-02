---
title: "PDO::rollback | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "drivers"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: d918c1e3-1be0-4001-b3b0-000db6d9e8b8
caps.latest.revision: 8
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# PDO::rollback
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

Discards database commands that were issued after calling [PDO::beginTransaction](../../connect/php/pdo-begintransaction.md) and returns the connection to auto commit mode.  
  
## Syntax  
  
```  
  
bool PDO::rollBack ();  
```  
  
## Return Value  
true if the method call succeeded, false otherwise.  
  
## Remarks  
PDO::rollback is not affected by (and does not affect) the value of PDO::ATTR_AUTOCOMMIT.  
  
See [PDO::beginTransaction](../../connect/php/pdo-begintransaction.md) for an example that uses PDO::rollback.  
  
Support for PDO was added in version 2.0 of the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)].  
  
## See Also  
[PDO Class](../../connect/php/pdo-class.md)  
[PDO](http://go.microsoft.com/fwlink/?LinkID=187441)  
  
