---
title: "How to: perform transactions"
description: "This topic explains and demonstrates how to perform transactions when using the Microsoft Drivers for PHP for SQL Server"
author: David-Engel
ms.author: v-davidengel
ms.date: "08/10/2020"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords:
  - "transaction support"
---
# How to: Perform Transactions
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

The SQLSRV driver of the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)] provides three functions for performing transactions:  
  
-   [sqlsrv_begin_transaction](../../connect/php/sqlsrv-begin-transaction.md)  
  
-   [sqlsrv_commit](../../connect/php/sqlsrv-commit.md)  
  
-   [sqlsrv_rollback](../../connect/php/sqlsrv-rollback.md)  
  
The PDO_SQLSRV driver provides three methods for performing transactions:  
  
-   [PDO::beginTransaction](../../connect/php/pdo-begintransaction.md)  
  
-   [PDO::commit](../../connect/php/pdo-commit.md)  
  
-   [PDO::rollback](../../connect/php/pdo-rollback.md)  
  
See [PDO::beginTransaction](../../connect/php/pdo-begintransaction.md) for an example.  
  
The remainder of this topic explains and demonstrates how to use the SQLSRV driver to perform transactions.  
  
## Remarks  
The steps to execute a transaction can be summarized as follows:  
  
1.  Begin the transaction with **sqlsrv_begin_transaction**.  
  
2.  Check the success or failure of each query that is part of the transaction.  
  
3.  If appropriate, commit the transaction with **sqlsrv_commit**. Otherwise, roll back the transaction with **sqlsrv_rollback**. After calling **sqlsrv_commit** or **sqlsrv_rollback**, the driver is returned to auto-commit mode.  
  
    By default, the [!INCLUDE[ssDriverPHP](../../includes/ssdriverphp_md.md)] is in auto-commit mode. This means that all queries are automatically committed upon success unless they have been designated as part of an explicit transaction by using **sqlsrv_begin_transaction**.  
  
    If an explicit transaction is not committed with **sqlsrv_commit**, it is rolled back upon closing of the connection or termination of the script.  
  
    Do not use embedded Transact-SQL to perform transactions. For example, do not execute a statement with "BEGIN TRANSACTION" as the Transact-SQL query to begin a transaction. The expected transactional behavior cannot be guaranteed when you use embedded Transact-SQL to perform transactions.  
  
    The **sqlsrv** functions listed earlier should be used to perform transactions.  
  
## Example  
  
### Description  
The following example executes several queries as part of a transaction. If all the queries are successful, the transaction is committed. If any one of the queries fails, the transaction is rolled back.  
  
The example tries to delete a sales order from the *Sales.SalesOrderDetail* table and adjust product inventory levels in the *Product.ProductInventory* table for each product in the sales order. These queries are included in a transaction because all queries must be successful for the database to accurately reflect the state of orders and product availability.  
  
The first query in the example retrieves product IDs and quantities for a specified sales order ID. This query is not part of the transaction. However, the script ends if this query fails because the product IDs and quantities are required to complete queries that are part of the subsequent transaction.  
  
The ensuing queries (deletion of the sales order and updating of the product inventory quantities) are part of the transaction.  
  
The example assumes that SQL Server and the [AdventureWorks](https://github.com/Microsoft/sql-server-samples/tree/master/samples/databases/adventure-works) database are installed on the local computer. All output is written to the console when the example is run from the command line.  
  
### Code  
  
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
  
/* Begin transaction. */  
if( sqlsrv_begin_transaction($conn) === false )   
{   
     echo "Could not begin transaction.\n";  
     die( print_r( sqlsrv_errors(), true));  
}  
  
/* Set the Order ID.  */  
$orderId = 43667;  
  
/* Execute operations that are part of the transaction. Commit on  
success, roll back on failure. */  
if (perform_trans_ops($conn, $orderId))  
{  
     //If commit fails, roll back the transaction.  
     if(sqlsrv_commit($conn))  
     {  
         echo "Transaction committed.\n";  
     }  
     else  
     {  
         echo "Commit failed - rolling back.\n";  
         sqlsrv_rollback($conn);  
     }  
}  
else  
{  
     "Error in transaction operation - rolling back.\n";  
     sqlsrv_rollback($conn);  
}  
  
/*Free connection resources*/  
sqlsrv_close( $conn);  
  
/*----------------  FUNCTION: perform_trans_ops  -----------------*/  
function perform_trans_ops($conn, $orderId)  
{  
    /* Define query to update inventory based on sales order info. */  
    $tsql1 = "UPDATE Production.ProductInventory   
              SET Quantity = Quantity + s.OrderQty   
              FROM Production.ProductInventory p   
              JOIN Sales.SalesOrderDetail s   
              ON s.ProductID = p.ProductID   
              WHERE s.SalesOrderID = ?";  
  
    /* Define the parameters array. */  
    $params = array($orderId);  
  
    /* Execute the UPDATE statement. Return false on failure. */  
    if( sqlsrv_query( $conn, $tsql1, $params) === false ) return false;  
  
    /* Delete the sales order. Return false on failure */  
    $tsql2 = "DELETE FROM Sales.SalesOrderDetail   
              WHERE SalesOrderID = ?";  
    if(sqlsrv_query( $conn, $tsql2, $params) === false ) return false;  
  
    /* Return true because all operations were successful. */  
    return true;  
}  
?>  
```  
  
### Comments  
For the purpose of focusing on transaction behavior, some recommended error handling is not included in the previous example. For a production application, we recommend checking any call to a **sqlsrv** function for errors and handling them accordingly.
  
## See Also  
[Updating Data &#40;Microsoft Drivers for PHP for SQL Server&#41;](../../connect/php/updating-data-microsoft-drivers-for-php-for-sql-server.md)

[Transactions (Database Engine)](/previous-versions/sql/sql-server-2008-r2/ms190612(v=sql.105))

[About Code Examples in the Documentation](../../connect/php/about-code-examples-in-the-documentation.md)  
