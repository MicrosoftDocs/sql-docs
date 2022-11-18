---
title: "sqlsrv_commit"
description: "sqlsrv_commit"
author: David-Engel
ms.author: v-davidengel
ms.date: "03/26/2018"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "transaction support"
  - "API Reference, sqlsrv_commit"
  - "sqlsrv_commit"
apiname: "sqlsrv_commit"
apitype: "NA"
---
# sqlsrv_commit
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

Commits the current transaction on the specified connection and returns the connection to the auto-commit mode. The current transaction includes all statements on the specified connection that were executed after the call to [sqlsrv_begin_transaction](../../connect/php/sqlsrv-begin-transaction.md) and before any calls to [sqlsrv_rollback](../../connect/php/sqlsrv-rollback.md) or **sqlsrv_commit**.  
  
> [!NOTE]  
> The [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)] is in auto-commit mode by default. This means that all queries are automatically committed upon success unless they have been designated as part of an explicit transaction by using **sqlsrv_begin_transaction**.  
  
> [!NOTE]  
> If **sqlsrv_commit** is called on a connection that is not in an active transaction and that was initiated with **sqlsrv_begin_transaction**, the call returns **false** and a *Not in Transaction* error is added to the error collection.  
  
## Syntax  
  
```  
  
sqlsrv_commit( resource $conn )  
```  
  
#### Parameters  
*$conn*: The connection on which the transaction is active.  
  
## Return Value  
A Boolean value: **true** if the transaction was successfully committed. Otherwise, **false**.  
  
## Example  
The example below executes two queries as part of a transaction. If both queries are successful, the transaction is committed. If either (or both) of the queries fail, the transaction is rolled back.  
  
The first query in the example inserts a new sales order into the *Sales.SalesOrderDetail* table of the AdventureWorks database. The order is for five units of the product that has product ID 709. The second query reduces the inventory quantity of product ID 709 by five units. These queries are included in a transaction because both queries must be successful for the database to accurately reflect the state of orders and product availability.  
  
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
     die( print_r( sqlsrv_errors(), true ));  
}  
  
/* Initiate transaction. */  
/* Exit script if transaction cannot be initiated. */  
if (sqlsrv_begin_transaction( $conn) === false)  
{  
     echo "Could not begin transaction.\n";  
     die( print_r( sqlsrv_errors(), true ));  
}  
  
/* Initialize parameter values. */  
$orderId = 43659; $qty = 5; $productId = 709;  
$offerId = 1; $price = 5.70;  
  
/* Set up and execute the first query. */  
$tsql1 = "INSERT INTO Sales.SalesOrderDetail   
                     (SalesOrderID,   
                      OrderQty,   
                      ProductID,   
                      SpecialOfferID,   
                      UnitPrice)  
          VALUES (?, ?, ?, ?, ?)";  
$params1 = array( $orderId, $qty, $productId, $offerId, $price);  
$stmt1 = sqlsrv_query( $conn, $tsql1, $params1 );  
  
/* Set up and execute the second query. */  
$tsql2 = "UPDATE Production.ProductInventory   
          SET Quantity = (Quantity - ?)   
          WHERE ProductID = ?";  
$params2 = array($qty, $productId);  
$stmt2 = sqlsrv_query( $conn, $tsql2, $params2 );  
  
/* If both queries were successful, commit the transaction. */  
/* Otherwise, rollback the transaction. */  
if( $stmt1 && $stmt2 )  
{  
     sqlsrv_commit( $conn );  
     echo "Transaction was committed.\n";  
}  
else  
{  
     sqlsrv_rollback( $conn );  
     echo "Transaction was rolled back.\n";  
}  
  
/* Free statement and connection resources. */  
sqlsrv_free_stmt( $stmt1);  
sqlsrv_free_stmt( $stmt2);  
sqlsrv_close( $conn);  
?>  
```  
  
For the purpose of focusing on transaction behavior, some recommended error handling is not included in the preceding example. For a production application, it is recommended that any call to a **sqlsrv** function be checked for errors and handled accordingly.  
  
> [!NOTE]  
> Do not use embedded Transact-SQL to perform transactions. For example, do not execute a statement with "BEGIN TRANSACTION" as the Transact-SQL query to begin a transaction. The expected transactional behavior cannot be guaranteed when using embedded Transact-SQL to perform transactions.  
  
## See Also  
[SQLSRV Driver API Reference](../../connect/php/sqlsrv-driver-api-reference.md)

[How to: Perform Transactions](../../connect/php/how-to-perform-transactions.md)

[Overview of the Microsoft Drivers for PHP for SQL Server](../../connect/php/overview-of-the-php-sql-driver.md)
  
