---
title: "sqlsrv_next_result"
description: "sqlsrv_next_result"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "multiple result sets"
  - "sqlsrv_next_result"
  - "stored procedure support"
  - "API Reference, sqlsrv_next_result"
apiname: "sqlsrv_next_result"
apitype: "NA"
---
# sqlsrv_next_result
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

Makes the next result (result set, row count, or output parameter) of the specified statement active.  
  
> [!NOTE]  
> The first (or only) result returned by a batch query or stored procedure is active without a call to **sqlsrv_next_result**.  
  
## Syntax  
  
```  
  
sqlsrv_next_result( resource $stmt )  
```  
  
#### Parameters  
*$stmt*: The executed statement on which the next result is made active.  
  
## Return Value  
If the next result was successfully made active, the Boolean value **true** is returned. If an error occurred in making the next result active, **false** is returned. If no more results are available, **null** is returned.  
  
## Example 1  
The following example creates and executes a stored procedure that inserts a product review into the *Production.ProductReview* table, and then selects all reviews for the specified product. After execution of the stored procedure, the first result (the number of rows affected by the INSERT query in the stored procedure) is consumed without calling **sqlsrv_next_result**. The next result (the rows returned by the SELECT query in the stored procedure) is made available by calling **sqlsrv_next_result** and consumed using [sqlsrv_fetch_array](../../connect/php/sqlsrv-fetch-array.md).  
  
> [!NOTE]  
> Calling stored procedures using canonical syntax is the recommended practice. For more information about canonical syntax, see [Calling a Stored Procedure](../../relational-databases/native-client-odbc-stored-procedures/calling-a-stored-procedure.md).  
  
The example assumes that SQL Server and the [AdventureWorks](https://github.com/Microsoft/sql-server-samples/tree/master/samples/databases/adventure-works) database are installed on the local computer. All output is written to the console when the example is run from the command line.  
  
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
  
/* Drop the stored procedure if it already exists. */  
$tsql_dropSP = "IF OBJECT_ID('InsertProductReview', 'P') IS NOT NULL  
                DROP PROCEDURE InsertProductReview";  
$stmt1 = sqlsrv_query( $conn, $tsql_dropSP);  
if( $stmt1 === false )  
{  
     echo "Error in executing statement 1.\n";  
     die( print_r( sqlsrv_errors(), true));  
}  
  
/* Create the stored procedure. */  
$tsql_createSP = " CREATE PROCEDURE InsertProductReview  
                                    @ProductID int,  
                                    @ReviewerName nvarchar(50),  
                                    @ReviewDate datetime,  
                                    @EmailAddress nvarchar(50),  
                                    @Rating int,  
                                    @Comments nvarchar(3850)  
                   AS  
                       BEGIN  
                             INSERT INTO Production.ProductReview   
                                         (ProductID,  
                                          ReviewerName,  
                                          ReviewDate,  
                                          EmailAddress,  
                                          Rating,  
                                          Comments)  
                                    VALUES  
                                         (@ProductID,  
                                          @ReviewerName,  
                                          @ReviewDate,  
                                          @EmailAddress,  
                                          @Rating,  
                                          @Comments);  
                             SELECT * FROM Production.ProductReview  
                                WHERE ProductID = @ProductID;  
                       END";  
$stmt2 = sqlsrv_query( $conn, $tsql_createSP);  
  
if( $stmt2 === false)  
{  
     echo "Error in executing statement 2.\n";  
     die( print_r( sqlsrv_errors(), true));  
}  
/*-------- The next few steps call the stored procedure. --------*/  
  
/* Define the Transact-SQL query. Use question marks (?) in place of the  
parameters to be passed to the stored procedure */  
$tsql_callSP = "{call InsertProductReview(?, ?, ?, ?, ?, ?)}";  
  
/* Define the parameter array. */  
$productID = 709;  
$reviewerName = "Customer Name";  
$reviewDate = "2008-02-12";  
$emailAddress = "customer@email.com";  
$rating = 3;  
$comments = "[Insert comments here.]";  
$params = array(   
                 $productID,  
                 $reviewerName,  
                 $reviewDate,  
                 $emailAddress,  
                 $rating,  
                 $comments  
               );  
  
/* Execute the query. */  
$stmt3 = sqlsrv_query( $conn, $tsql_callSP, $params);  
if( $stmt3 === false)  
{  
     echo "Error in executing statement 3.\n";  
     die( print_r( sqlsrv_errors(), true));  
}  
  
/* Consume the first result (rows affected by INSERT query in the  
stored procedure) without calling sqlsrv_next_result. */  
echo "Rows affectd: ".sqlsrv_rows_affected($stmt3)."-----\n";  
  
/* Move to the next result and display results. */  
$next_result = sqlsrv_next_result($stmt3);  
if( $next_result )  
{  
     echo "\nReview information for product ID ".$productID.".---\n";  
     while( $row = sqlsrv_fetch_array( $stmt3, SQLSRV_FETCH_ASSOC))  
     {  
          echo "ReviewerName: ".$row['ReviewerName']."\n";  
          echo "ReviewDate: ".date_format($row['ReviewDate'],  
                                             "M j, Y")."\n";  
          echo "EmailAddress: ".$row['EmailAddress']."\n";  
          echo "Rating: ".$row['Rating']."\n\n";  
     }  
}  
elseif( is_null($next_result))  
{  
     echo "No more results.\n";  
}  
else  
{  
     echo "Error in moving to next result.\n";  
     die(print_r(sqlsrv_errors(), true));  
}  
  
/* Free statement and connection resources. */  
sqlsrv_free_stmt( $stmt1 );  
sqlsrv_free_stmt( $stmt2 );  
sqlsrv_free_stmt( $stmt3 );  
sqlsrv_close( $conn );  
?>  
```  
  
When executing a stored procedure that has output parameters, it is recommended that all other results are consumed before accessing the values of output parameters. For more information see [How to: Specify Parameter Direction Using the SQLSRV Driver](../../connect/php/how-to-specify-parameter-direction-using-the-sqlsrv-driver.md).  
  
## Example 2  
The following example executes a batch query that retrieves product review information for a specified product ID, inserts a review for the product, then again retrieves the product review information for the specified product ID. The newly inserted product review will be included in the final result set of the batch query. The example uses [sqlsrv_next_result](../../connect/php/sqlsrv-next-result.md) to move from one result of the batch query to the next.  
  
> [!NOTE]  
> The first (or only) result returned by a batch query or stored procedure is active without a call to **sqlsrv_next_result**.  
  
The example uses the *Purchasing.ProductReview* table of the [AdventureWorks](https://github.com/Microsoft/sql-server-samples/tree/master/samples/databases/adventure-works) database, and assumes that this database is installed on the server. All output is written to the console when the example is run from the command line.  
  
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
  
/* Define the batch query. */  
$tsql = "--Query 1  
         SELECT ProductID, ReviewerName, Rating   
         FROM Production.ProductReview   
         WHERE ProductID=?;  
  
         --Query 2  
         INSERT INTO Production.ProductReview (ProductID,   
                                               ReviewerName,   
                                               ReviewDate,   
                                               EmailAddress,   
                                               Rating)  
         VALUES (?, ?, ?, ?, ?);  
  
         --Query 3  
         SELECT ProductID, ReviewerName, Rating   
         FROM Production.ProductReview   
         WHERE ProductID=?;";  
  
/* Assign parameter values and execute the query. */  
$params = array(798,   
                798,   
                'CustomerName',   
                '2008-4-15',   
                'test@customer.com',   
                3,   
                798 );  
$stmt = sqlsrv_query($conn, $tsql, $params);  
if( $stmt === false )  
{  
     echo "Error in statement execution.\n";  
     die( print_r( sqlsrv_errors(), true));  
}  
  
/* Retrieve and display the first result. */  
echo "Query 1 result:\n";  
while( $row = sqlsrv_fetch_array( $stmt, SQLSRV_FETCH_NUMERIC ))  
{  
     print_r($row);  
}  
  
/* Move to the next result of the batch query. */  
sqlsrv_next_result($stmt);  
  
/* Display the result of the second query. */  
echo "Query 2 result:\n";  
echo "Rows Affected: ".sqlsrv_rows_affected($stmt)."\n";  
  
/* Move to the next result of the batch query. */  
sqlsrv_next_result($stmt);  
  
/* Retrieve and display the third result. */  
echo "Query 3 result:\n";  
while( $row = sqlsrv_fetch_array( $stmt, SQLSRV_FETCH_NUMERIC ))  
{  
     print_r($row);  
}  
  
/* Free statement and connection resources. */  
sqlsrv_free_stmt( $stmt );  
sqlsrv_close( $conn );  
?>  
```  
  
## See Also  
[SQLSRV Driver API Reference](../../connect/php/sqlsrv-driver-api-reference.md)

[About Code Examples in the Documentation](../../connect/php/about-code-examples-in-the-documentation.md)

[Retrieving Data](../../connect/php/retrieving-data.md)

[Updating Data &#40;Microsoft Drivers for PHP for SQL Server&#41;](../../connect/php/updating-data-microsoft-drivers-for-php-for-sql-server.md)

[Example Application &#40;SQLSRV Driver&#41;](../../connect/php/example-application-sqlsrv-driver.md)

  
