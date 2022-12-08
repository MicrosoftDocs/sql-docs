---
title: "Cursor Types (SQLSRV Driver)"
description: "Learn how to use cursor types to create a result set that you can access in any order using the Microsoft Drivers for PHP for SQL Server."
author: David-Engel
ms.author: v-davidengel
ms.date: "02/11/2019"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Cursor Types (SQLSRV Driver)
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

The SQLSRV driver lets you create a result set with rows that you can access in any order, depending on the cursor type.  This topic will discuss client-side (buffered) and server-side (unbuffered) cursors.  
  
## Cursor Types  
When you create a result set with [sqlsrv_query](../../connect/php/sqlsrv-query.md) or with [sqlsrv_prepare](../../connect/php/sqlsrv-prepare.md), you can specify the type of cursor. By default, a forward-only cursor is used, which lets you move one row at a time starting at the first row of the result set until you reach the end of the result set.  
  
You can create a result set with a scrollable cursor, which allows you to access any row in the result set, in any order. The following table lists the values that can be passed to the **Scrollable** option in sqlsrv_query or sqlsrv_prepare.  
  
|Option|Description|  
|----------|---------------|  
|SQLSRV_CURSOR_FORWARD|Lets you move one row at a time starting at the first row of the result set until you reach the end of the result set.<br /><br />This is the default cursor type.<br /><br />[sqlsrv_num_rows](../../connect/php/sqlsrv-num-rows.md) returns an error for result sets created with this cursor type.<br /><br />**forward** is the abbreviated form of SQLSRV_CURSOR_FORWARD.|  
|SQLSRV_CURSOR_STATIC|Lets you access rows in any order but will not reflect changes in the database.<br /><br />**static** is the abbreviated form of SQLSRV_CURSOR_STATIC.|  
|SQLSRV_CURSOR_DYNAMIC|Lets you access rows in any order and will reflect changes in the database.<br /><br />[sqlsrv_num_rows](../../connect/php/sqlsrv-num-rows.md) returns an error for result sets created with this cursor type.<br /><br />**dynamic** is the abbreviated form of SQLSRV_CURSOR_DYNAMIC.|  
|SQLSRV_CURSOR_KEYSET|Lets you access rows in any order. However, a keyset cursor does not update the row count if a row is deleted from the table (a deleted row is returned with no values).<br /><br />**keyset** is the abbreviated form of SQLSRV_CURSOR_KEYSET.|  
|SQLSRV_CURSOR_CLIENT_BUFFERED|Lets you access rows in any order. Creates a client-side cursor query.<br /><br />**buffered** is the abbreviated form of SQLSRV_CURSOR_CLIENT_BUFFERED.|  
  
If a query generates multiple result sets, the **Scrollable** option applies to all result sets.  
  
## Selecting Rows in a Result Set  
After you create a result set, you can use [sqlsrv_fetch](../../connect/php/sqlsrv-fetch.md), [sqlsrv_fetch_array](../../connect/php/sqlsrv-fetch-array.md), or [sqlsrv_fetch_object](../../connect/php/sqlsrv-fetch-object.md) to specify a row.  
  
The following table describes the values you can specify in the *row* parameter.  
  
|Parameter|Description|  
|-------------|---------------|  
|SQLSRV_SCROLL_NEXT|Specifies the next row. This is the default value, if you do not specify the *row* parameter for a scrollable result set.|  
|SQLSRV_SCROLL_PRIOR|Specifies the row before the current row.|  
|SQLSRV_SCROLL_FIRST|Specifies the first row in the result set.|  
|SQLSRV_SCROLL_LAST|Specifies the last row in the result set.|  
|SQLSRV_SCROLL_ABSOLUTE|Specifies the row specified with the *offset* parameter.|  
|SQLSRV_SCROLL_RELATIVE|Specifies the row specified with the *offset* parameter from the current row.|  
  
## Server-Side Cursors and the SQLSRV Driver  
The following example shows the effect of the various cursors. On line 33 of the example, you see the first of three query statements that specify different cursors.  Two of the query statements are commented. Each time you run the program, use a different cursor type to see the effect of the database update on line 47.  
  
```  
<?php  
$server = "server_name";  
$conn = sqlsrv_connect( $server, array( 'Database' => 'test' ));  
if ( $conn === false ) {  
   die( print_r( sqlsrv_errors(), true ));  
}  
  
$stmt = sqlsrv_query( $conn, "DROP TABLE dbo.ScrollTest" );  
if ( $stmt !== false ) {   
   sqlsrv_free_stmt( $stmt );   
}  
  
$stmt = sqlsrv_query( $conn, "CREATE TABLE ScrollTest (id int, value char(10))" );  
if ( $stmt === false ) {  
   die( print_r( sqlsrv_errors(), true ));  
}  
  
$stmt = sqlsrv_query( $conn, "INSERT INTO ScrollTest (id, value) VALUES(?,?)", array( 1, "Row 1" ));  
if ( $stmt === false ) {  
   die( print_r( sqlsrv_errors(), true ));  
}  
  
$stmt = sqlsrv_query( $conn, "INSERT INTO ScrollTest (id, value) VALUES(?,?)", array( 2, "Row 2" ));  
if ( $stmt === false ) {  
   die( print_r( sqlsrv_errors(), true ));  
}  
  
$stmt = sqlsrv_query( $conn, "INSERT INTO ScrollTest (id, value) VALUES(?,?)", array( 3, "Row 3" ));  
if ( $stmt === false ) {  
   die( print_r( sqlsrv_errors(), true ));  
}  
  
$stmt = sqlsrv_query( $conn, "SELECT * FROM ScrollTest", array(), array( "Scrollable" => 'keyset' ));  
// $stmt = sqlsrv_query( $conn, "SELECT * FROM ScrollTest", array(), array( "Scrollable" => 'dynamic' ));  
// $stmt = sqlsrv_query( $conn, "SELECT * FROM ScrollTest", array(), array( "Scrollable" => 'static' ));  
  
$rows = sqlsrv_has_rows( $stmt );  
if ( $rows != true ) {  
   die( "Should have rows" );  
}  
  
$result = sqlsrv_fetch( $stmt, SQLSRV_SCROLL_LAST );  
$field1 = sqlsrv_get_field( $stmt, 0 );  
$field2 = sqlsrv_get_field( $stmt, 1 );  
echo "\n$field1 $field2\n";  
  
$stmt2 = sqlsrv_query( $conn, "delete from ScrollTest where id = 3" );  
// or  
// $stmt2 = sqlsrv_query( $conn, "UPDATE ScrollTest SET id = 4 WHERE id = 3" );  
if ( $stmt2 !== false ) {   
   sqlsrv_free_stmt( $stmt2 );   
}  
  
$result = sqlsrv_fetch( $stmt, SQLSRV_SCROLL_LAST );  
$field1 = sqlsrv_get_field( $stmt, 0 );  
$field2 = sqlsrv_get_field( $stmt, 1 );  
echo "\n$field1 $field2\n";  
  
sqlsrv_free_stmt( $stmt );  
sqlsrv_close( $conn );  
?>  
```  
  
## Client-Side Cursors and the SQLSRV Driver  
Client-side cursors are a feature added in version 3.0 of the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)] that allows you to cache an entire result set in memory. Row count is available after the query is executed when using a client-side cursor.  
  
Client-side cursors should be used for small- to medium-sized result sets. Use server-side cursors for large result sets.  
  
A query will return false if the buffer is not large enough to hold the entire result set. You can increase the buffer size up to the PHP memory limit.  
  
Using the SQLSRV driver, you can configure the size of the buffer that holds the result set with the ClientBufferMaxKBSize setting for [sqlsrv_configure](../../connect/php/sqlsrv-configure.md). [sqlsrv_get_config](../../connect/php/sqlsrv-get-config.md) returns the value of ClientBufferMaxKBSize. You can also set the maximum buffer size in the php.ini file with sqlsrv.ClientBufferMaxKBSize (for example, sqlsrv.ClientBufferMaxKBSize = 1024).  
  
The following sample shows:  
  
-   Row count is always available with a client-side cursor.  
  
-   Use of client-side cursors and batch statements.  
  
```  
<?php  
$serverName = "(local)";  
$connectionInfo = array("Database"=>"AdventureWorks");  
$conn = sqlsrv_connect( $serverName, $connectionInfo);  
  
if ( $conn === false ) {  
   echo "Could not connect.\n";  
   die( print_r( sqlsrv_errors(), true));  
}  
  
$tsql = "select * from HumanResources.Department";  
  
// Execute the query with client-side cursor.  
$stmt = sqlsrv_query($conn, $tsql, array(), array("Scrollable"=>"buffered"));  
if (! $stmt) {  
   echo "Error in statement execution.\n";  
   die( print_r( sqlsrv_errors(), true));  
}  
  
// row count is always available with a client-side cursor  
$row_count = sqlsrv_num_rows( $stmt );  
echo "\nRow count = $row_count\n";  
  
// Move to a specific row in the result set.  
$row = sqlsrv_fetch($stmt, SQLSRV_SCROLL_FIRST);  
$EmployeeID = sqlsrv_get_field( $stmt, 0);  
echo "Employee ID = $EmployeeID \n";  
  
// Client-side cursor and batch statements  
$tsql = "select top 2 * from HumanResources.Employee;Select top 3 * from HumanResources.EmployeeAddress";  
  
$stmt = sqlsrv_query($conn, $tsql, array(), array("Scrollable"=>"buffered"));  
if (! $stmt) {  
   echo "Error in statement execution.\n";  
   die( print_r( sqlsrv_errors(), true));  
}  
  
$row_count = sqlsrv_num_rows( $stmt );  
echo "\nRow count for first result set = $row_count\n";  
  
$row = sqlsrv_fetch($stmt, SQLSRV_SCROLL_FIRST);  
$EmployeeID = sqlsrv_get_field( $stmt, 0);  
echo "Employee ID = $EmployeeID \n";  
  
sqlsrv_next_result($stmt);  
  
$row_count = sqlsrv_num_rows( $stmt );  
echo "\nRow count for second result set = $row_count\n";  
  
$row = sqlsrv_fetch($stmt, SQLSRV_SCROLL_LAST);  
$EmployeeID = sqlsrv_get_field( $stmt, 0);  
echo "Employee ID = $EmployeeID \n";  
?>  
```  
  
The following sample shows a client-side cursor using [sqlsrv_prepare](../../connect/php/sqlsrv-prepare.md) and a different client buffer size.
  
```  
<?php  
$serverName = "(local)";  
$connectionInfo = array( "Database"=>"AdventureWorks");  
$conn = sqlsrv_connect( $serverName, $connectionInfo);  
  
if ( $conn === false ) {  
   echo "Could not connect.\n";  
   die( print_r( sqlsrv_errors(), true));  
}  
  
$tsql = "select * from HumanResources.Employee";  
$stmt = sqlsrv_prepare( $conn, $tsql, array(), array("Scrollable" => SQLSRV_CURSOR_CLIENT_BUFFERED, "ClientBufferMaxKBSize" => 51200));
  
if (! $stmt ) {  
   echo "Statement could not be prepared.\n";  
   die( print_r( sqlsrv_errors(), true));  
}  
  
sqlsrv_execute( $stmt);  
  
$row_count = sqlsrv_num_rows( $stmt );  
if ($row_count)  
   echo "\nRow count = $row_count\n";  
  
$row = sqlsrv_fetch($stmt, SQLSRV_SCROLL_FIRST);  
if ($row ) {  
   $EmployeeID = sqlsrv_get_field( $stmt, 0);  
   echo "Employee ID = $EmployeeID \n";  
}  
?>  
```  
  
## See Also  
[Specifying a Cursor Type and Selecting Rows](../../connect/php/specifying-a-cursor-type-and-selecting-rows.md)  
  
