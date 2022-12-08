---
title: "How to: retrieve binary data as a stream using the SQLSRV driver"
description: "This topic describes how to retrieve binary data as a stream when using the Microsoft SQLSRV Driver for PHP for SQL Server"
author: David-Engel
ms.author: v-davidengel
ms.date: "08/10/2020"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords:
  - "retrieving data, as a binary stream"
  - "streaming data"
---
# How to: Retrieve Binary Data as a Stream Using the SQLSRV Driver
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

Retrieving data as a stream is only available in the SQLSRV driver of the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)], and is not available in the PDO_SQLSRV driver.  
  
The [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)] takes advantage of PHP streams for retrieving large amounts of binary data from the server. This topic demonstrates how to retrieve binary data as a stream.  
  
Using the streams to retrieve binary data, such as images, avoids using large amounts of script memory by retrieving chunks of data instead of loading the whole object into script memory.  
  
## Example  
The following example retrieves binary data, an image in this case, from the *Production.ProductPhoto* table of the AdventureWorks database. The image is retrieved as a stream and displayed in the browser.  
  
Retrieving image data as a stream is accomplished by using [sqlsrv_fetch](../../connect/php/sqlsrv-fetch.md) and [sqlsrv_get_field](../../connect/php/sqlsrv-get-field.md) with the return type specified as a binary stream. The return type is specified by using the constant **SQLSRV_PHPTYPE_STREAM**. For information about **sqlsrv** constants, see [Constants &#40;Microsoft Drivers for PHP for SQL Server&#41;](../../connect/php/constants-microsoft-drivers-for-php-for-sql-server.md).  
  
The example assumes that SQL Server and the [AdventureWorks](https://github.com/Microsoft/sql-server-samples/tree/master/samples/databases/adventure-works) database are installed on the local computer. All output is written to the browser when the example is run from the browser.  
  
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
  
/* Set up the Transact-SQL query. */  
$tsql = "SELECT LargePhoto   
         FROM Production.ProductPhoto   
         WHERE ProductPhotoID = ?";  
  
/* Set the parameter values and put them in an array. */  
$productPhotoID = 70;  
$params = array( $productPhotoID);  
  
/* Execute the query. */  
$stmt = sqlsrv_query($conn, $tsql, $params);  
if( $stmt === false )  
{  
     echo "Error in statement execution.</br>";  
     die( print_r( sqlsrv_errors(), true));  
}  
  
/* Retrieve and display the data.  
The return data is retrieved as a binary stream. */  
if ( sqlsrv_fetch( $stmt ) )  
{  
   $image = sqlsrv_get_field( $stmt, 0,   
                      SQLSRV_PHPTYPE_STREAM(SQLSRV_ENC_BINARY));  
   header("Content-Type: image/jpg");  
   fpassthru($image);  
}  
else  
{  
     echo "Error in retrieving data.</br>";  
     die(print_r( sqlsrv_errors(), true));  
}  
  
/* Free statement and connection resources. */  
sqlsrv_free_stmt( $stmt);  
sqlsrv_close( $conn);  
?>  
```  
  
Specifying the return type in the example demonstrates how to specify the PHP return type as a binary stream. Technically, it is not required in the example because the *LargePhoto* field has SQL Server type varbinary(max) and is therefore returned as a binary stream by default. For information about default PHP data types, see [Default PHP Data Types](../../connect/php/default-php-data-types.md). For information about how to specify PHP return types, see [How to: Specify PHP Data Types](../../connect/php/how-to-specify-php-data-types.md).  
  
## See Also  
[Retrieving Data](../../connect/php/retrieving-data.md)

[Retrieving Data as a Stream Using the SQLSRV Driver](../../connect/php/retrieving-data-as-a-stream-using-the-sqlsrv-driver.md)

[About Code Examples in the Documentation](../../connect/php/about-code-examples-in-the-documentation.md)  
  
