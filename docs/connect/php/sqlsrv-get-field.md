---
title: "sqlsrv_get_field | Microsoft Docs"
ms.custom: ""
ms.date: "06/26/2018"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "sqlsrv_get_field"
apitype: "NA"
helpviewer_keywords: 
  - "sqlsrv_get_field"
  - "API Reference, sqlsrv_get_field"
  - "retrieving data, as a single field"
ms.assetid: fa17cc56-fb38-433b-a40d-65642f04dc23
author: MightyPen
ms.author: genemi
manager: craigg
---
# sqlsrv_get_field
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

Retrieves data from the specified field of the current row. Field data must be accessed in order. For example, data from the first field cannot be accessed after data from the second field has been accessed.  
  
## Syntax  
  
```  
sqlsrv_get_field( resource $stmt, int $fieldIndex [, int $getAsType])  
```  
  
#### Parameters  
*$stmt*: A statement resource corresponding to an executed statement.  
  
*$fieldIndex*: The index of the field to be retrieved. Indexes begin at zero.  
  
*$getAsType* [OPTIONAL]: A **SQLSRV** constant (**SQLSRV_PHPTYPE_&#x2a;**) that determines the PHP data type for the returned data. For information about supported data types, see [Constants &#40;Microsoft Drivers for PHP for SQL Server&#41;](../../connect/php/constants-microsoft-drivers-for-php-for-sql-server.md). If no return type is specified, a default PHP type will be returned. For information about default PHP types, see [Default PHP Data Types](../../connect/php/default-php-data-types.md). For information about specifying PHP data types, see [How to: Specify PHP Data Types](../../connect/php/how-to-specify-php-data-types.md).  
  
## Return Value  
The field data. You can specify the PHP data type of the returned data by using the *$getAsType* parameter. If no return data type is specified, the default PHP data type will be returned. For information about default PHP types, see [Default PHP Data Types](../../connect/php/default-php-data-types.md). For information about specifying PHP data types, see [How to: Specify PHP Data Types](../../connect/php/how-to-specify-php-data-types.md).  
  
## Remarks  
The combination of **sqlsrv_fetch** and **sqlsrv_get_field** provides forward-only access to data.  
  
The combination of **sqlsrv_fetch**/**sqlsrv_get_field** loads only one field of a result set row into script memory and allows PHP return type specification. (For information about how to specify the PHP return type, see [How to: Specify PHP Data Types](../../connect/php/how-to-specify-php-data-types.md).) This combination of functions also allows data to be retrieved as a stream. (For information about retrieving data as a stream, see [Retrieving Data as a Stream Using the SQLSRV Driver](../../connect/php/retrieving-data-as-a-stream-using-the-sqlsrv-driver.md).)  
  
## Example  
The following example retrieves a row of data that contains a product review and the name of the reviewer. To retrieve data from the result set, **sqlsrv_get_field** is used. The example assumes that SQL Server and the [AdventureWorks](https://github.com/Microsoft/sql-server-samples/tree/master/samples/databases/adventure-works) database are installed on the local computer. All output is written to the console when the example is run from the command line.  
  
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
Comments are of the SQL Server nvarchar type. */  
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
if( sqlsrv_fetch( $stmt ) === false )  
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
while( !feof( $stream))  
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
[SQLSRV Driver API Reference](../../connect/php/sqlsrv-driver-api-reference.md)  

[Retrieving Data](../../connect/php/retrieving-data.md)  

[About Code Examples in the Documentation](../../connect/php/about-code-examples-in-the-documentation.md)  
  
