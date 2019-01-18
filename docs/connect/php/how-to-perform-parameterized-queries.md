---
title: "How to: Perform Parameterized Queries | Microsoft Docs"
ms.custom: ""
ms.date: "03/26/2018"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "updating data"
  - "parameterized queries"
ms.assetid: dc7d0ede-a9b6-4ce2-977e-4d1e7ec2131c
author: MightyPen
ms.author: genemi
manager: craigg
---
# How to: Perform Parameterized Queries
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

This topic summarizes and demonstrates how to use the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)] to perform a parameterized query.  
  
The steps for performing a parameterized query can be summarized into four steps:  
  
1.  Put question marks (?) as parameter placeholders in the Transact-SQL string that is the query to be executed.  
  
2.  Initialize or update PHP variables that correspond to the placeholders in the Transact-SQL query.  
  
3.  Use PHP variables from step 2 to create or update an array of parameter values which correspond to parameter placeholders in the Transact-SQL string. The parameter values in the array must be in the same order as the placeholders meant to represent them.
  
4.  Execute the query:  
  
    -   If you are using the SQLSRV driver, use [sqlsrv_query](../../connect/php/sqlsrv-query.md) or [sqlsrv_prepare](../../connect/php/sqlsrv-prepare.md)/[sqlsrv_execute](../../connect/php/sqlsrv-execute.md).  
  
    -   If you are using the PDO_SQLSRV driver, execute the query with [PDO::prepare](../../connect/php/pdo-prepare.md) and [PDOStatement::execute](../../connect/php/pdostatement-execute.md). The topics for [PDO::prepare](../../connect/php/pdo-prepare.md) and [PDOStatement::execute](../../connect/php/pdostatement-execute.md) have code examples.  
  
The rest of this topic discusses parameterized queries using the SQLSRV driver.  
  
> [!NOTE]  
> Parameters are implicitly bound by using **sqlsrv_prepare**. This means that if a parameterized query is prepared using **sqlsrv_prepare** and values in the parameter array are updated, the updated values will be used upon the next execution of the query. See the second example in this topic for more detail.  
  
## Example  
The following example updates the quantity for a specified product ID in the *Production.ProductInventory* table of the AdventureWorks database. The quantity and product ID are parameters in the UPDATE query.  
  
The example then queries the database to verify that the quantity has been correctly updated. The product ID is a parameter in the SELECT query.  
  
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
  
/* Define the Transact-SQL query.  
Use question marks as parameter placeholders. */  
$tsql1 = "UPDATE Production.ProductInventory   
          SET Quantity = ?   
          WHERE ProductID = ?";  
  
/* Initialize $qty and $productId */  
$qty = 10; $productId = 709;  
  
/* Execute the statement with the specified parameter values. */  
$stmt1 = sqlsrv_query( $conn, $tsql1, array($qty, $productId));  
if( $stmt1 === false )  
{  
     echo "Statement 1 could not be executed.\n";  
     die( print_r( sqlsrv_errors(), true));  
}  
  
/* Free statement resources. */  
sqlsrv_free_stmt( $stmt1);  
  
/* Now verify the updated quantity.  
Use a question mark as parameter placeholder. */  
$tsql2 = "SELECT Quantity   
          FROM Production.ProductInventory  
          WHERE ProductID = ?";  
  
/* Execute the statement with the specified parameter value.  
Display the returned data if no errors occur. */  
$stmt2 = sqlsrv_query( $conn, $tsql2, array($productId));  
if( $stmt2 === false )  
{  
     echo "Statement 2 could not be executed.\n";  
     die( print_r(sqlsrv_errors(), true));  
}  
else  
{  
     $qty = sqlsrv_fetch_array( $stmt2);  
     echo "There are $qty[0] of product $productId in inventory.\n";  
}  
  
/* Free statement and connection resources. */  
sqlsrv_free_stmt( $stmt2);  
sqlsrv_close( $conn);  
?>  
```  
  
The previous example uses the **sqlsrv_query** function to execute queries. This function is good for executing one-time queries since it does both statement preparation and execution. The combination of **sqlsrv_prepare**/**sqlsrv_execute** is best for re-execution of a query with different parameter values. To see an example of re-execution of a query with different parameter values, see the next example.  
  
## Example  
The following example demonstrates the implicit binding of variables when you use the **sqlsrv_prepare** function. The example inserts several sales orders into the *Sales.SalesOrderDetail* table. The *$params* array is bound to the statement (*$stmt*) when **sqlsrv_prepare** is called. Before each execution of a query that inserts a new sales order into the table, the *$params* array is updated with new values corresponding to sales order details. The subsequent query execution uses the new parameter values.  
  
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
  
$tsql = "INSERT INTO Sales.SalesOrderDetail (SalesOrderID,   
                                             OrderQty,   
                                             ProductID,   
                                             SpecialOfferID,   
                                             UnitPrice)  
         VALUES (?, ?, ?, ?, ?)";  
  
/* Each sub array here will be a parameter array for a query.  
The values in each sub array are, in order, SalesOrderID, OrderQty,  
 ProductID, SpecialOfferID, UnitPrice. */  
$parameters = array( array(43659, 8, 711, 1, 20.19),  
                     array(43660, 6, 762, 1, 419.46),  
                     array(43661, 4, 741, 1, 818.70)  
                    );  
  
/* Initialize parameter values. */  
$orderId = 0;  
$qty = 0;  
$prodId = 0;  
$specialOfferId = 0;  
$price = 0.0;  
  
/* Prepare the statement. $params is implicitly bound to $stmt. */  
$stmt = sqlsrv_prepare( $conn, $tsql, array( &$orderId,  
                                             &$qty,  
                                             &$prodId,  
                                             &$specialOfferId,  
                                             &$price));  
if( $stmt === false )  
{  
     echo "Statement could not be prepared.\n";  
     die( print_r( sqlsrv_errors(), true));  
}  
  
/* Execute a statement for each set of params in $parameters.  
Because $params is bound to $stmt, as the values are changed, the  
new values are used in the subsequent execution. */  
foreach( $parameters as $params)  
{  
     list($orderId, $qty, $prodId, $specialOfferId, $price) = $params;  
     if( sqlsrv_execute($stmt) === false )  
     {  
          echo "Statement could not be executed.\n";  
          die( print_r( sqlsrv_errors(), true));  
     }  
     else  
     {  
          /* Verify that the row was successfully inserted. */  
          echo "Rows affected: ".sqlsrv_rows_affected( $stmt )."\n";  
     }  
}  
/* Free statement and connection resources. */  
sqlsrv_free_stmt( $stmt);  
sqlsrv_close( $conn);  
?>  
```  
  
## See Also  
[Converting Data Types](../../connect/php/converting-data-types.md)

[Security Considerations for the Microsoft Drivers for PHP for SQL Server](../../connect/php/security-considerations-for-php-sql-driver.md)

[About Code Examples in the Documentation](../../connect/php/about-code-examples-in-the-documentation.md)

[sqlsrv_rows_affected](../../connect/php/sqlsrv-rows-affected.md)  
  
