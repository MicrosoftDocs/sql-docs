---
title: "How to: specify PHP data types"
description: "Learn how to specify PHP Data Types when retrieving data using the Microsoft Drivers for PHP for SQL Server"
author: David-Engel
ms.author: v-davidengel
ms.date: "08/10/2020"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords:
  - "converting data types"
  - "streaming data"
---
# How to: Specify PHP Data Types
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

When using the PDO_SQLSRV driver, you can specify the PHP data type when retrieving data from the server with PDOStatement::bindColumn and PDOStatement::bindParam. See [PDOStatement::bindColumn](../../connect/php/pdostatement-bindcolumn.md) and [PDOStatement::bindParam](../../connect/php/pdostatement-bindparam.md) for more information.  
  
The following steps summarize how to specify PHP data types when retrieving data from the server using the SQLSRV driver:  
  
1.  Set up and execute a Transact-SQL query with [sqlsrv_query](../../connect/php/sqlsrv-query.md) or the combination of [sqlsrv_prepare](../../connect/php/sqlsrv-prepare.md)/[sqlsrv_execute](../../connect/php/sqlsrv-execute.md).  
  
2.  Make a row of data available for reading with [sqlsrv_fetch](../../connect/php/sqlsrv-fetch.md).  
  
3.  Retrieve field data from a returned row using [sqlsrv_get_field](../../connect/php/sqlsrv-get-field.md) with the desired PHP data type specified as the optional third parameter. If the optional third parameter is not specified, data is returned according to the default PHP types. For information about the default PHP return types, see [Default PHP Data Types](../../connect/php/default-php-data-types.md).  
  
    For information about the constants used to specify the PHP data type, see the PHPTYPEs section of [Constants &#40;Microsoft Drivers for PHP for SQL Server&#41;](../../connect/php/constants-microsoft-drivers-for-php-for-sql-server.md).  
  
## Example  
The following example retrieves rows from the *Production.ProductReview* table of the AdventureWorks database. In each returned row, the *ReviewDate* field is retrieved as a string and the *Comments* field is retrieved as a stream. The stream data is displayed by using the PHP [fpassthru](https://php.net/manual/en/function.fpassthru.php) function.  
  
The example assumes that SQL Server and the [AdventureWorks](https://github.com/Microsoft/sql-server-samples/tree/master/samples/databases/adventure-works) database are installed on the local computer. All output is written to the console when the example is run from the command line.  
  
```  
<?php  
/*Connect to the local server using Windows Authentication and specify  
the AdventureWorks database as the database in use. */  
$serverName = "(local)";  
$connectionInfo = array( "Database"=>"AdventureWorks");  
$conn = sqlsrv_connect( $serverName, $connectionInfo);  
if( $conn === false )  
{  
     echo "Could not connect.\n";  
     die( print_r( sqlsrv_errors(), true));  
}  
  
/* Set up the Transact-SQL query. */  
$tsql = "SELECT ReviewerName,   
                ReviewDate,  
                Rating,   
                Comments   
         FROM Production.ProductReview   
         WHERE ProductID = ?   
         ORDER BY ReviewDate DESC";  
  
/* Set the parameter value. */  
$productID = 709;  
$params = array( $productID);  
  
/* Execute the query. */  
$stmt = sqlsrv_query($conn, $tsql, $params);  
if( $stmt === false )  
{  
     echo "Error in statement execution.\n";  
     die( print_r( sqlsrv_errors(), true));  
}  
  
/* Retrieve and display the data. The first and third fields are  
retrieved according to their default types, strings. The second field  
is retrieved as a string with 8-bit character encoding. The fourth  
field is retrieved as a stream with 8-bit character encoding.*/  
while ( sqlsrv_fetch( $stmt))  
{  
   echo "Name: ".sqlsrv_get_field( $stmt, 0 )."\n";  
   echo "Date: ".sqlsrv_get_field( $stmt, 1,   
                       SQLSRV_PHPTYPE_STRING( SQLSRV_ENC_CHAR))."\n";  
   echo "Rating: ".sqlsrv_get_field( $stmt, 2 )."\n";  
   echo "Comments: ";  
   $comments = sqlsrv_get_field( $stmt, 3,   
                            SQLSRV_PHPTYPE_STREAM(SQLSRV_ENC_CHAR));  
   fpassthru( $comments);  
   echo "\n";   
}  
  
/* Free statement and connection resources. */  
sqlsrv_free_stmt( $stmt);  
sqlsrv_close( $conn);  
?>  
```  
  
In the example, retrieving the second field (*ReviewDate*) as a string preserves millisecond accuracy of the SQL Server DATETIME data type. By default, the SQL Server DATETIME data type is retrieved as a PHP DateTime object in which the millisecond accuracy is lost.  
  
Retrieving the fourth field (*Comments*) as a stream is for demonstration purposes. By default, the SQL Server data type nvarchar(3850) is retrieved as a string, which is acceptable for most situations.  
  
> [!NOTE]  
> The [sqlsrv_field_metadata](../../connect/php/sqlsrv-field-metadata.md) function provides a way to obtain field information, including type information, before executing a query.  
  
## See Also  
[Retrieving Data](../../connect/php/retrieving-data.md)

[About Code Examples in the Documentation](../../connect/php/about-code-examples-in-the-documentation.md)

[How to: Retrieve Output Parameters Using the SQLSRV Driver](../../connect/php/how-to-retrieve-output-parameters-using-the-sqlsrv-driver.md)

[How to: Retrieve Input and Output Parameters Using the SQLSRV Driver](../../connect/php/how-to-retrieve-input-and-output-parameters-using-the-sqlsrv-driver.md)  
  
