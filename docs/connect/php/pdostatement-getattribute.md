---
title: "PDOStatement::getAttribute"
description: "API reference for the PDOStatement::getAttribute function in the Microsoft PDO_SQLSRV Driver for PHP for SQL Server."
author: David-Engel
ms.author: v-davidengel
ms.date: "08/10/2020"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# PDOStatement::getAttribute
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

Retrieves the value of a predefined PDOStatement attribute or custom driver attribute.  
  
## Syntax  
  
```  
  
mixed PDOStatement::getAttribute( $attribute );  
```  
  
#### Parameters  
$*attribute*: An integer, one of the PDO::ATTR_* or PDO::SQLSRV_ATTR_\* constants. Supported attributes are the attributes you can set with [PDOStatement::setAttribute](../../connect/php/pdostatement-setattribute.md), PDO::SQLSRV_ATTR_DIRECT_QUERY (for more information, see [Direct Statement Execution and Prepared Statement Execution in the PDO_SQLSRV Driver](../../connect/php/direct-statement-execution-prepared-statement-execution-pdo-sqlsrv-driver.md)), PDO::ATTR_CURSOR and PDO::SQLSRV_ATTR_CURSOR_SCROLL_TYPE (for more information, see [Cursor Types (PDO_SQLSRV Driver)](../../connect/php/cursor-types-pdo-sqlsrv-driver.md)).  
  
## Return Value  
On success, returns a (mixed) value for a predefined PDO attribute or custom driver attribute. Returns null on failure.  
  
## Remarks  
See [PDOStatement::setAttribute](../../connect/php/pdostatement-setattribute.md) for a sample.  
  
Support for PDO was added in version 2.0 of the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)].  
  
## See Also  
[PDOStatement Class](../../connect/php/pdostatement-class.md)

[PDO](https://php.net/manual/book.pdo.php)  
  
