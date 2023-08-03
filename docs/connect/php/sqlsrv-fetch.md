---
title: "sqlsrv_fetch"
description: "sqlsrv_fetch"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "sqlsrv_fetch"
  - "API Reference, sqlsrv_fetch"
  - "retrieving data, as a single field"
apiname: "sqlsrv_fetch"
apitype: "NA"
---
# sqlsrv_fetch
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

Makes the next row of a result set available for reading. Use [sqlsrv_get_field](../../connect/php/sqlsrv-get-field.md) to read fields of the row.  
  
## Syntax  
  
```  
  
sqlsrv_fetch( resource $stmt[, row[, ]offset])  
```  
  
#### Parameters  
*$stmt*: A statement resource corresponding to an executed statement.  
  
> [!NOTE]  
> A statement must be executed before results can be retrieved. For information on executing a statement, see [sqlsrv_query](../../connect/php/sqlsrv-query.md) and [sqlsrv_execute](../../connect/php/sqlsrv-execute.md).  
  
*row* [OPTIONAL]: One of the following values, specifying the row to access in a result set that uses a scrollable cursor:  
  
-   SQLSRV_SCROLL_NEXT  
  
-   SQLSRV_SCROLL_PRIOR  
  
-   SQLSRV_SCROLL_FIRST  
  
-   SQLSRV_SCROLL_LAST  
  
-   SQLSRV_SCROLL_ABSOLUTE  
  
-   SQLSRV_SCROLL_RELATIVE  
  
For more information on these values, see [Specifying a Cursor Type and Selecting Rows](../../connect/php/specifying-a-cursor-type-and-selecting-rows.md).  
  
*offset* [OPTIONAL]: Used with SQLSRV_SCROLL_ABSOLUTE and SQLSRV_SCROLL_RELATIVE to specify the row to retrieve. The first record in the result set is 0.  
  
## Return Value  
If the next row of the result set was successfully retrieved, **true** is returned. If there are no more results in the result set, **null** is returned. If an error occurred, **false** is returned.  
  
## Example  
The following example uses **sqlsrv_fetch** to retrieve a row of data containing a product review and the name of the reviewer. To retrieve data from the result set, [sqlsrv_get_field](../../connect/php/sqlsrv-get-field.md) is used. The example assumes that SQL Server and the [AdventureWorks](https://github.com/Microsoft/sql-server-samples/tree/master/samples/databases/adventure-works) database are installed on the local computer. All output is written to the console when the example is run from the command line.  
  
```  
<?php  
/*Connect to the local server using Windows Authentication and  
specify the AdventureWorks database as the database in use. */  
$serverName = "(local)";  
$connectionInfo = array( "Database"=>"AdventureWorks");  
$conn = sqlsrv_connect( $serverName, $connectionInfo);  
if( $conn === false )  
{  
     echo "Could not connect.\n";  
     die( print_r( sqlsrv_errors(), true));  
}  
  
/* Set up and execute the query. Note that both ReviewerName and  
Comments are of SQL Server type nvarchar. */  
$tsql = "SELECT ReviewerName, Comments   
         FROM Production.ProductReview  
         WHERE ProductReviewID=1";  
$stmt = sqlsrv_query( $conn, $tsql);  
if( $stmt === false )  
{  
     echo "Error in statement preparation/execution.\n";  
     die( print_r( sqlsrv_errors(), true));  
}  
  
/* Make the first row of the result set available for reading. */  
if( sqlsrv_fetch( $stmt ) === false)  
{  
     echo "Error in retrieving row.\n";  
     die( print_r( sqlsrv_errors(), true));  
}  
  
/* Note: Fields must be accessed in order.  
Get the first field of the row. Note that no return type is  
specified. Data will be returned as a string, the default for  
a field of type nvarchar.*/  
$name = sqlsrv_get_field( $stmt, 0);  
echo "$name: ";  
  
/*Get the second field of the row as a stream.  
Because the default return type for a nvarchar field is a  
string, the return type must be specified as a stream. */  
$stream = sqlsrv_get_field( $stmt, 1,   
                            SQLSRV_PHPTYPE_STREAM( SQLSRV_ENC_CHAR));  
while( !feof( $stream ))  
{   
    $str = fread( $stream, 10000);  
    echo $str;  
}  
  
/* Free the statement and connection resources. */  
sqlsrv_free_stmt( $stmt);  
sqlsrv_close( $conn);  
?>  
```  
  
## See Also  
[Retrieving Data](../../connect/php/retrieving-data.md)  

[SQLSRV Driver API Reference](../../connect/php/sqlsrv-driver-api-reference.md)  

[About Code Examples in the Documentation](../../connect/php/about-code-examples-in-the-documentation.md)  
  
