---
title: "sqlsrv_free_stmt"
description: "sqlsrv_free_stmt"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "sqlsrv_free_stmt"
  - "API Reference, sqlsrv_free_stmt"
apiname: "sqlsrv_free_stmt"
apitype: "NA"
---
# sqlsrv_free_stmt
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

Frees all resources associated with the specified statement. The statement cannot be used again after this function has been called.  
  
## Syntax  
  
```  
  
sqlsrv_free_stmt( resource $stmt)  
```  
  
#### Parameters  
*$stmt*: The statement to be closed.  
  
## Return Value  
The Boolean value **true** unless the function is called with an invalid parameter. If the function is called with an invalid parameter, **false** is returned.  
  
> [!NOTE]  
> **Null** is a valid parameter for this function. This allows the function to be called multiple times in a script. For example, if you free a statement in an error condition and free it again at the end of the script, the second call to **sqlsrv_free_stmt** will return **true** because the first call to **sqlsrv_free_stmt** (in the error condition) sets the statement resource to **null**.  
  
## Example  
The following example creates a statement resource, executes a simple query, and calls **sqlsrv_free_stmt** to free all resources associated with the statement. The example assumes that SQL Server and the [AdventureWorks](https://github.com/Microsoft/sql-server-samples/tree/master/samples/databases/adventure-works) database are installed on the local computer. All output is written to the console when the example is run from the command line.  
  
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
  
$stmt = sqlsrv_query( $conn, "SELECT * FROM Person.Contact");  
if( $stmt )  
{  
     echo "Statement executed.\n";  
}  
else  
{  
     echo "Query could not be executed.\n";  
     die( print_r( sqlsrv_errors(), true));  
}  
  
/*-------------------------------  
     Process query results here.  
-------------------------------*/  
  
/* Free the statement and connection resources. */  
sqlsrv_free_stmt( $stmt);  
sqlsrv_close( $conn);  
?>  
```  
  
## See Also  
[SQLSRV Driver API Reference](../../connect/php/sqlsrv-driver-api-reference.md)  

[About Code Examples in the Documentation](../../connect/php/about-code-examples-in-the-documentation.md)  

[sqlsrv_cancel](../../connect/php/sqlsrv-cancel.md)  
  
