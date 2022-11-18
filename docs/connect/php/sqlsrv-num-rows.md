---
title: "sqlsrv_num_rows"
description: "API reference for the sqlsrv_num_rows function in the Microsoft SQLSRV Driver for PHP for SQL Server."
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "API Reference, sqlsrv_num_rows"
  - "sqlsrv_num_rows"
---
# sqlsrv_num_rows
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

Reports the number of rows in a result set.  
  
## Syntax  
  
```  
  
sqlsrv_num_rows( resource $stmt )  
```  
  
#### Parameters  
*$stmt*: The result set for which to count the rows.  
  
## Return Value  
**false** if there was an error calculating the number of rows. Otherwise, returns the number of rows in the result set.  
  
## Remarks  
sqlsrv_num_rows requires a client-side, static, or keyset cursor, and will return **false** if you use a forward cursor or a dynamic cursor. (A forward cursor is the default.) For more information about cursors, see [sqlsrv_query](../../connect/php/sqlsrv-query.md) and [Cursor Types &#40;SQLSRV Driver&#41;](../../connect/php/cursor-types-sqlsrv-driver.md).  
  
## Example  
  
```  
<?php  
   $server = "server_name";  
   $conn = sqlsrv_connect( $server, array( 'Database' => 'Northwind' ) );  
  
   $stmt = sqlsrv_query( $conn, "select * from orders where CustomerID = 'VINET'" , array(), array( "Scrollable" => SQLSRV_CURSOR_KEYSET ));  
  
   $row_count = sqlsrv_num_rows( $stmt );  
  
   if ($row_count === false)  
      echo "\nerror\n";  
   else if ($row_count >=0)  
      echo "\n$row_count\n";  
?>  
```  
  
The following sample shows that when there is more than one result set (a batch query), the number of rows is only available when you use a client-side cursor.  
  
```  
<?php  
$serverName = "(local)";  
$connectionInfo = array("Database"=>"AdventureWorks");  
$conn = sqlsrv_connect( $serverName, $connectionInfo);  
  
$tsql = "select * from HumanResources.Department";  
  
// Client-side cursor and batch statements  
$tsql = "select top 2 * from HumanResources.Employee;Select top 3 * from HumanResources.EmployeeAddress";  
  
// works  
$stmt = sqlsrv_query($conn, $tsql, array(), array("Scrollable"=>"buffered"));  
  
// fails  
// $stmt = sqlsrv_query($conn, $tsql);  
// $stmt = sqlsrv_query($conn, $tsql, array(), array("Scrollable"=>"forward"));  
// $stmt = sqlsrv_query($conn, $tsql, array(), array("Scrollable"=>"static"));  
// $stmt = sqlsrv_query($conn, $tsql, array(), array("Scrollable"=>"keyset"));  
// $stmt = sqlsrv_query($conn, $tsql, array(), array("Scrollable"=>"dynamic"));  
  
$row_count = sqlsrv_num_rows( $stmt );  
echo "\nRow count for first result set = $row_count\n";  
  
sqlsrv_next_result($stmt);  
  
$row_count = sqlsrv_num_rows( $stmt );  
echo "\nRow count for second result set = $row_count\n";  
?>  
```  
  
## See Also  
[SQLSRV Driver API Reference](../../connect/php/sqlsrv-driver-api-reference.md)  
  
