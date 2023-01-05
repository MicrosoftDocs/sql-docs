---
title: "sqlsrv_cancel"
description: "sqlsrv_cancel"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "sqlsrv_cancel"
  - "API Reference, sqlsrv_cancel"
apiname: "sqlsrv_cancel"
apitype: "NA"
---
# sqlsrv_cancel
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

Cancels a statement. This means that any pending results for the statement are discarded. After this function is called, the statement can be re-executed if it was prepared with [sqlsrv_prepare](../../connect/php/sqlsrv-prepare.md). Calling this function is not necessary if all the results associated with the statement have been consumed.  
  
## Syntax  
  
```  
  
sqlsrv_cancel( resource $stmt)  
```  
  
#### Parameters  
*$stmt*: The statement to be canceled.  
  
## Return Value  
A Boolean value: **true** if the operation was successful. Otherwise, **false**.  
  
## Example  
The following example targets the [AdventureWorks](https://github.com/Microsoft/sql-server-samples/tree/master/samples/databases/adventure-works) database to execute a query, then consumes and counts results until the variable *$salesTotal* reaches a specified amount. The remaining query results are then discarded. The example assumes that SQL Server and the AdventureWorks database are installed on the local computer. All output is written to the console when the example is run from the command line.  
  
```  
<?php  
/* Connect to the local server using Windows Authentication and   
specify the AdventureWorks database as the database in use. */  
$serverName = "(local)";  
$connectionInfo = array( "Database"=>"AdventureWorks");  
$conn = sqlsrv_connect( $serverName, $connectionInfo);  
if( $conn === false )  
{  
     echo "Could not connect.\n";  
     die( print_r( sqlsrv_errors(), true));  
}  
  
/* Prepare and execute the query. */  
$tsql = "SELECT OrderQty, UnitPrice FROM Sales.SalesOrderDetail";  
$stmt = sqlsrv_prepare( $conn, $tsql);  
if( $stmt === false )  
{  
     echo "Error in statement preparation.\n";  
     die( print_r( sqlsrv_errors(), true));  
}  
if( sqlsrv_execute( $stmt ) === false)  
{  
     echo "Error in statement execution.\n";  
     die( print_r( sqlsrv_errors(), true));  
}  
  
/* Initialize tracking variables. */  
$salesTotal = 0;  
$count = 0;  
  
/* Count and display the number of sales that produce revenue  
of $100,000. */  
while( ($row = sqlsrv_fetch_array( $stmt)) && $salesTotal <=100000)  
{  
     $qty = $row[0];  
     $price = $row[1];  
     $salesTotal += ( $price * $qty);  
     $count++;  
}  
echo "$count sales accounted for the first $$salesTotal in revenue.\n";  
  
/* Cancel the pending results. The statement can be reused. */  
sqlsrv_cancel( $stmt);  
?>  
```  
  
## Comments  
A statement that is prepared and executed using the combination of [sqlsrv_prepare](../../connect/php/sqlsrv-prepare.md) and [sqlsrv_execute](../../connect/php/sqlsrv-execute.md) can be re-executed with **sqlsrv_execute** after calling **sqlsrv_cancel**. A statement that is executed with [sqlsrv_query](../../connect/php/sqlsrv-query.md) cannot be re-executed after calling **sqlsrv_cancel**.  
  
## See Also  
[SQLSRV Driver API Reference](../../connect/php/sqlsrv-driver-api-reference.md)

[Connecting to the Server](../../connect/php/connecting-to-the-server.md)

[Retrieving Data](../../connect/php/retrieving-data.md)

[About Code Examples in the Documentation](../../connect/php/about-code-examples-in-the-documentation.md)

[sqlsrv_free_stmt](../../connect/php/sqlsrv-free-stmt.md)

  
