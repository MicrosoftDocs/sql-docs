---
title: "PDO::exec"
description: "API reference for the PDO::exec function in the Microsoft PDO_SQLSRV Driver for PHP for SQL Server."
author: David-Engel
ms.author: v-davidengel
ms.date: "08/10/2020"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
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
  
