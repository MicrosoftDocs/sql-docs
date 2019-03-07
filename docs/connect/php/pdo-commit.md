---
title: "PDO::commit | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: a0db4a00-9700-4f49-ab16-6522dd1101d3
author: MightyPen
ms.author: genemi
manager: craigg
---
# PDO::commit
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

Sends commands to the database that were issued after calling [PDO::beginTransaction](../../connect/php/pdo-begintransaction.md) and returns the connection to auto commit mode.  
  
## Syntax  
  
```  
  
bool PDO::commit();  
```  
  
## Return Value  
true if the method call succeeded, false otherwise.  
  
## Remarks  
PDO::commit is not affected by (and does not affect) the value of PDO::ATTR_AUTOCOMMIT.  
  
See [PDO::beginTransaction](../../connect/php/pdo-begintransaction.md) for an example that uses PDO::commit.  
  
Support for PDO was added in version 2.0 of the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)].  
  
## See Also  
[PDO Class](../../connect/php/pdo-class.md)

[PDO](https://php.net/manual/book.pdo.php)  
  
