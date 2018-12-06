---
title: "PDO::beginTransaction | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 4d5db438-9df7-4d22-9907-3ddc63bd2220
author: MightyPen
ms.author: genemi
manager: craigg
---
# PDO::beginTransaction
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

Turns off auto commit mode and begins a transaction.  
  
## Syntax  
  
```  
  
bool PDO::beginTransaction();  
```  
  
## Return Value  
true if the method call succeeded, false otherwise.  
  
## Remarks  
The transaction begun with PDO::beginTransaction ends when [PDO::commit](../../connect/php/pdo-commit.md) or [PDO::rollback](../../connect/php/pdo-rollback.md) is called.  
  
PDO::beginTransaction is not affected by (and does not affect) the value of PDO::ATTR_AUTOCOMMIT.  
  
You are not allowed to call PDO::beginTransaction before the previous PDO::beginTransaction is ended with PDO::rollback or PDO::commit.  
  
The connection returns to auto commit mode if this method fails.  
  
Support for PDO was added in version 2.0 of the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)].  
  
## Example  
The following example uses a database called Test and a table called Table1. It starts a transaction and then issues commands to add two rows and then delete one row. The commands are sent to the database and the transaction is explicitly ended with `PDO::commit`.  
  
```  
<?php  
   $conn = new PDO( "sqlsrv:server=(local); Database = Test", "", "");  
   $conn->beginTransaction();  
   $ret = $conn->exec("insert into Table1(col1, col2) values('a', 'b') ");  
   $ret = $conn->exec("insert into Table1(col1, col2) values('a', 'c') ");  
   $ret = $conn->exec("delete from Table1 where col1 = 'a' and col2 = 'b'");  
   $conn->commit();  
   // $conn->rollback();  
   echo $ret;  
?>  
```  
  
## See Also  
[PDO Class](../../connect/php/pdo-class.md)

[PDO](https://php.net/manual/book.pdo.php)  
  
