---
title: "How to: Send and Retrieve UTF-8 Data Using Built-In UTF-8 Support | Microsoft Docs"
ms.custom: ""
ms.date: "03/23/2018"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "retrieving data, UTF-8 encoded data"
  - "converting data types"
  - "updating data"
ms.assetid: 366c57cf-352f-4202-8074-6ddce44880d1
author: MightyPen
ms.author: genemi
manager: craigg
---
# How to: Send and Retrieve UTF-8 Data Using Built-In UTF-8 Support
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

If you are using the PDO_SQLSRV driver, you can specify the encoding with the PDO::SQLSRV_ATTR_ENCODING attribute. For more information, see [Constants &#40;Microsoft Drivers for PHP for SQL Server&#41;](../../connect/php/constants-microsoft-drivers-for-php-for-sql-server.md).  
  
The remainder of this topic discusses encoding with the SQLSRV driver.  
  
To send or retrieve UTF-8 encoded data to the server:  
  
1.  Make sure that the source or destination column is of type **nchar** or **nvarchar**.  
  
2.  Specify the PHP type as `SQLSRV_PHPTYPE_STRING('UTF-8')` in the parameters array. Or, specify `"CharacterSet" => "UTF-8"` as a connection option.  
  
    When you specify a character set as part of the connection options, the driver assumes that the other connection option strings use that same character set. The server name and query strings are also assumed to use the same character set.  
  
You can pass UTF-8 or SQLSRV_ENC_CHAR to **CharacterSet**, but you cannot pass SQLSRV_ENC_BINARY. The default encoding is SQLSRV_ENC_CHAR.  
  
## Example  
The following example demonstrates how to send and retrieve UTF-8 encoded data by specifying the UTF-8 character set when making the connection. The example updates the Comments column of the Production.ProductReview table for a specified review ID. The example also retrieves the newly updated data and displays it. Note that the Comments column is of type **nvarchar(3850).** Also note that before data is sent to the server it is converted to UTF-8 encoding using the PHP **utf8_encode** function. This is done for demonstration purposes only. In a real application scenario, you would begin with UTF-8 encoded data.  
  
The example assumes that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and the [AdventureWorks](https://github.com/Microsoft/sql-server-samples/tree/master/samples/databases/adventure-works) database are installed on the local computer. All output is written to the browser when the example is run from the browser.  
  
```  
<?php  
  
// Connect to the local server using Windows Authentication and  
// specify the AdventureWorks database as the database in use.   
//   
$serverName = "MyServer";  
$connectionInfo = array( "Database"=>"AdventureWorks", "CharacterSet" => "UTF-8");  
$conn = sqlsrv_connect( $serverName, $connectionInfo);  
  
if ( $conn === false ) {  
   echo "Could not connect.<br>";  
   die( print_r( sqlsrv_errors(), true));  
}  
  
// Set up the Transact-SQL query.  
//   
$tsql1 = "UPDATE Production.ProductReview  
          SET Comments = ?  
          WHERE ProductReviewID = ?";  
  
// Set the parameter values and put them in an array. Note that  
// $comments is converted to UTF-8 encoding with the PHP function  
// utf8_encode to simulate an application that uses UTF-8 encoded data.   
//   
$reviewID = 3;  
$comments = utf8_encode("testing 1, 2, 3, 4.  Testing.");  
$params1 = array(  
                  array( $comments, null ),  
                  array( $reviewID, null )  
                );  
  
// Execute the query.  
//   
$stmt1 = sqlsrv_query($conn, $tsql1, $params1);  
  
if ( $stmt1 === false ) {  
   echo "Error in statement execution.<br>";  
   die( print_r( sqlsrv_errors(), true));  
}  
else {  
   echo "The update was successfully executed.<br>";  
}  
  
// Retrieve the newly updated data.  
//   
$tsql2 = "SELECT Comments   
          FROM Production.ProductReview   
          WHERE ProductReviewID = ?";  
  
// Set up the parameter array.  
//   
$params2 = array($reviewID);  
  
// Execute the query.  
//   
$stmt2 = sqlsrv_query($conn, $tsql2, $params2);  
if ( $stmt2 === false ) {  
   echo "Error in statement execution.<br>";  
   die( print_r( sqlsrv_errors(), true));  
}  
  
// Retrieve and display the data.   
//   
if ( sqlsrv_fetch($stmt2) ) {  
   echo "Comments: ";  
   $data = sqlsrv_get_field( $stmt2, 0 );  
   echo $data."<br>";  
}  
else {  
   echo "Error in fetching data.<br>";  
   die( print_r( sqlsrv_errors(), true));  
}  
  
// Free statement and connection resources.  
//   
sqlsrv_free_stmt( $stmt1 );  
sqlsrv_free_stmt( $stmt2 );  
sqlsrv_close( $conn);  
?>  
```  
  
For information about storing Unicode data, see [Working with Unicode Data](https://msdn.microsoft.com/library/ms175180.aspx).  
  
## Example  
The following example is similar to the first sample but instead of specifying the UTF-8 character set on the connection, this sample shows how to specify the UTF-8 character set on the column.  
  
```  
<?php  
  
// Connect to the local server using Windows Authentication and  
// specify the AdventureWorks database as the database in use.   
//   
$serverName = "MyServer";  
$connectionInfo = array( "Database"=>"AdventureWorks");  
$conn = sqlsrv_connect( $serverName, $connectionInfo);  
  
if ( $conn === false ) {  
   echo "Could not connect.<br>";  
   die( print_r( sqlsrv_errors(), true));  
}  
  
// Set up the Transact-SQL query.  
//   
$tsql1 = "UPDATE Production.ProductReview  
          SET Comments = ?  
          WHERE ProductReviewID = ?";  
  
// Set the parameter values and put them in an array. Note that  
// $comments is converted to UTF-8 encoding with the PHP function  
// utf8_encode to simulate an application that uses UTF-8 encoded data.   
//   
$reviewID = 3;  
$comments = utf8_encode("testing");  
$params1 = array(  
                  array($comments,  
                        SQLSRV_PARAM_IN,  
                        SQLSRV_PHPTYPE_STRING('UTF-8')  
                  ),  
                  array($reviewID)  
                );  
  
// Execute the query.  
//   
$stmt1 = sqlsrv_query($conn, $tsql1, $params1);  
  
if ( $stmt1 === false ) {  
   echo "Error in statement execution.<br>";  
   die( print_r( sqlsrv_errors(), true));  
}  
else {  
   echo "The update was successfully executed.<br>";  
}  
  
// Retrieve the newly updated data.  
//   
$tsql2 = "SELECT Comments   
          FROM Production.ProductReview   
          WHERE ProductReviewID = ?";  
  
// Set up the parameter array.  
//   
$params2 = array($reviewID);  
  
// Execute the query.  
//   
$stmt2 = sqlsrv_query($conn, $tsql2, $params2);  
if ( $stmt2 === false ) {  
   echo "Error in statement execution.<br>";  
   die( print_r( sqlsrv_errors(), true));  
}  
  
// Retrieve and display the data.   
//   
if ( sqlsrv_fetch($stmt2) ) {  
   echo "Comments: ";  
   $data = sqlsrv_get_field($stmt2,   
                            0,   
                            SQLSRV_PHPTYPE_STRING('UTF-8')  
                           );  
   echo $data."<br>";  
}  
else {  
   echo "Error in fetching data.<br>";  
   die( print_r( sqlsrv_errors(), true));  
}  
  
// Free statement and connection resources.  
//   
sqlsrv_free_stmt( $stmt1 );  
sqlsrv_free_stmt( $stmt2 );  
sqlsrv_close( $conn);  
?>  
```  
  
## See Also  
[Retrieving Data](../../connect/php/retrieving-data.md)

[Working with ASCII Data in non-Windows](../../connect/php/how-to-send-and-retrieve-ascii-data-in-linux-mac.md)

[Updating Data &#40;Microsoft Drivers for PHP for SQL Server&#41;](../../connect/php/updating-data-microsoft-drivers-for-php-for-sql-server.md)

[SQLSRV Driver API Reference](../../connect/php/sqlsrv-driver-api-reference.md)

[Constants &#40;Microsoft Drivers for PHP for SQL Server&#41;](../../connect/php/constants-microsoft-drivers-for-php-for-sql-server.md)

[Example Application &#40;SQLSRV Driver&#41;](../../connect/php/example-application-sqlsrv-driver.md)  
  
