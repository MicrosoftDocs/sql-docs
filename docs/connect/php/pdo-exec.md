---
title: "PDO::exec | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 359a87c6-c13a-4518-8f23-a922e7f3b171
author: MightyPen
ms.author: genemi
manager: craigg
---
# PDO::exec
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

Prepares and executes an SQL statement in a single function call, returning the number of rows affected by the statement.  
  
## Syntax  
  
```  
  
int PDO::exec ($statement)  
```  
  
#### Parameters  
*$statement*: A string containing the SQL statement to execute.  
  
## Return Value  
An integer reporting the number of rows affected.  
  
## Remarks  
If *$statement* contains multiple SQL statements, the count of affected rows is reported for the last statement only.  
  
PDO::exec does not return results for a SELECT statement.  
  
The following attributes affect the behavior of PDO::exec:  
  
-   PDO::ATTR_DEFAULT_FETCH_MODE  
  
-   PDO::SQLSRV_ATTR_ENCODING  
  
-   PDO::SQLSRV_ATTR_QUERY_TIMEOUT  
  
For more information, see [PDO::setAttribute](../../connect/php/pdo-setattribute.md). 
  
Support for PDO was added in version 2.0 of the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)].  
  
## Example  
This example deletes rows in Table1 that have 'xxxyy' in col1. The example then reports how many rows were deleted.  
  
```  
<?php  
   $c = new PDO( "sqlsrv:server=(local)");  
  
   $c->exec("use Test");  
   $ret = $c->exec("delete from Table1 where col1 = 'xxxyy'");  
   echo $ret;  
?>  
```  
  
## See Also  
[PDO Class](../../connect/php/pdo-class.md)

[PDO](https://php.net/manual/book.pdo.php)  
  
