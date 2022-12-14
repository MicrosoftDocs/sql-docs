---
title: "sqlsrv_rows_affected"
description: "sqlsrv_rows_affected"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "sqlsrv_rows_affected"
  - "API Reference, sqlsrv_rows_affected"
apiname: "sqlsrv_rows_affected"
apitype: "NA"
---
# sqlsrv_rows_affected
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

Returns the number of rows modified by the last statement executed. This function does not return the number of rows returned by a SELECT statement.  
  
## Syntax  
  
```  
  
sqlsrv_rows_affected( resource $stmt)  
```  
  
#### Parameters  
*$stmt*: A statement resource corresponding to an executed statement.  
  
## Return Value  
An integer indicating the number of rows modified by the last executed statement. If no rows were modified, zero (0) is returned. If no information about the number of modified rows is available, negative one (-1) is returned. If an error occurred in retrieving the number of modified rows, **false** is returned.  
  
## Example  
The following example displays the number of rows modified by an UPDATE statement. The example assumes that SQL Server and the [AdventureWorks](https://github.com/Microsoft/sql-server-samples/tree/master/samples/databases/adventure-works) database are installed on the local computer. All output is written to the console when the example is run from the command line.  
  
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
  
/* Set up Transact-SQL query. */  
$tsql = "UPDATE Sales.SalesOrderDetail   
         SET SpecialOfferID = ?   
         WHERE ProductID = ?";  
  
/* Set parameter values. */  
$params = array(2, 709);  
  
/* Execute the statement. */  
$stmt = sqlsrv_query( $conn, $tsql, $params);  
  
/* Get the number of rows affected and display appropriate message.*/  
$rows_affected = sqlsrv_rows_affected( $stmt);  
if( $rows_affected === false)  
{  
     echo "Error in calling sqlsrv_rows_affected.\n";  
     die( print_r( sqlsrv_errors(), true));  
}  
elseif( $rows_affected == -1)  
{  
      echo "No information available.\n";  
}  
else  
{  
      echo $rows_affected." rows were updated.\n";  
}  
  
/* Free statement and connection resources. */  
sqlsrv_free_stmt( $stmt);  
sqlsrv_close( $conn);  
?>  
```  
  
## See Also  
[SQLSRV Driver API Reference](../../connect/php/sqlsrv-driver-api-reference.md)  

[About Code Examples in the Documentation](../../connect/php/about-code-examples-in-the-documentation.md)  

[Updating Data &#40;Microsoft Drivers for PHP for SQL Server&#41;](../../connect/php/updating-data-microsoft-drivers-for-php-for-sql-server.md)  

  
