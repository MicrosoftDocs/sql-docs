---
title: "sqlsrv_execute"
description: "sqlsrv_execute"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "sqlsrv_exclude"
  - "executing queries"
  - "API Reference, sqlsrv_execute"
apiname: "sqlsrv_execute"
apitype: "NA"
---
# sqlsrv_execute
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

Executes a previously prepared statement. See [sqlsrv_prepare](../../connect/php/sqlsrv-prepare.md) for information on preparing a statement for execution.  
  
> [!NOTE]  
> This function is ideal for executing a prepared statement multiple times with different parameter values.  
  
## Syntax  
  
```  
  
sqlsrv_execute( resource $stmt)  
```  
  
#### Parameters  
*$stmt*: A resource specifying the statement to be executed. For more information about how to create a statement resource, see [sqlsrv_prepare](../../connect/php/sqlsrv-prepare.md).  
  
## Return Value  
A Boolean value: **true** if the statement was successfully executed. Otherwise, **false**.  
  
## Example  
The following example executes a statement that updates a field in the *Sales.SalesOrderDetail* table in the [AdventureWorks](https://github.com/Microsoft/sql-server-samples/tree/master/samples/databases/adventure-works) database. The example assumes that SQL Server and the AdventureWorks database are installed on the local computer. All output is written to the console when the example is run from the command line.  
  
```  
<?php  
/*Connect to the local server using Windows Authentication and  
specify the AdventureWorks database as the database in use. */  
$serverName = "(local)";  
$connectionInfo = array( "Database"=>"AdventureWorks");  
$conn = sqlsrv_connect( $serverName, $connectionInfo);  
if( $conn === false)  
{  
     echo "Could not connect.\n";  
     die( print_r( sqlsrv_errors(), true));  
}  
  
/* Set up the Transact-SQL query. */  
$tsql = "UPDATE Sales.SalesOrderDetail   
         SET OrderQty = ( ?)   
         WHERE SalesOrderDetailID = ( ?)";  
  
/* Set up the parameters array. Parameters correspond, in order, to  
question marks in $tsql. */  
$params = array( 5, 10);  
  
/* Create the statement. */  
$stmt = sqlsrv_prepare( $conn, $tsql, $params);  
if( $stmt )  
{  
     echo "Statement prepared.\n";  
}  
else  
{  
     echo "Error in preparing statement.\n";  
     die( print_r( sqlsrv_errors(), true));  
}  
  
/* Execute the statement. Display any errors that occur. */  
if( sqlsrv_execute( $stmt))  
{  
      echo "Statement executed.\n";  
}  
else  
{  
     echo "Error in executing statement.\n";  
     die( print_r( sqlsrv_errors(), true));  
}  
  
/* Free the statement and connection resources. */  
sqlsrv_free_stmt( $stmt);  
sqlsrv_close( $conn);  
?>  
```  
  
## See Also  
[SQLSRV Driver API Reference](../../connect/php/sqlsrv-driver-api-reference.md)

[About Code Examples in the Documentation](../../connect/php/about-code-examples-in-the-documentation.md)  

[sqlsrv_query](../../connect/php/sqlsrv-query.md)  
  
