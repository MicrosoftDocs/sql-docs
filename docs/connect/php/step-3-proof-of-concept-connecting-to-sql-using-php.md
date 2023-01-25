---
title: "Step 3: Connecting to SQL using PHP"
description: Step 3 is a proof of concept, which shows how you can connect to SQL Server using PHP. The basic examples demonstrate selecting and inserting data.
author: David-Engel
ms.author: v-davidengel
ms.date: 05/05/2021
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Step 3: Proof of concept connecting to SQL using PHP

[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

## Step 1: Connect

This **OpenConnection** function is called near the top in all of the functions that follow.

```php
    function OpenConnection()
    {
        $serverName = "tcp:myserver.database.windows.net,1433";
        $connectionOptions = array("Database"=>"AdventureWorks",
            "Uid"=>"MyUser", "PWD"=>"MyPassword");
        $conn = sqlsrv_connect($serverName, $connectionOptions);
        if($conn == false)
            die(FormatErrors(sqlsrv_errors()));

        return $conn;
    }
```

## Step 2:  Execute query

The [sqlsrv_query()](https://php.net/manual/en/function.sqlsrv-query.php) function can be used to retrieve a result set from a query against SQL Database. This function essentially accepts any query and the connection object and returns a result set, which can be iterated over with the use of [sqlsrv_fetch_array()](https://php.net/manual/en/function.sqlsrv-fetch-array.php).

```php
    function ReadData()
    {
        try
        {
            $conn = OpenConnection();
            $tsql = "SELECT [CompanyName] FROM SalesLT.Customer";
            $getProducts = sqlsrv_query($conn, $tsql);
            if ($getProducts == FALSE)
                die(FormatErrors(sqlsrv_errors()));
            $productCount = 0;
            while($row = sqlsrv_fetch_array($getProducts, SQLSRV_FETCH_ASSOC))
            {
                echo($row['CompanyName']);
                echo("<br/>");
                $productCount++;
            }
            sqlsrv_free_stmt($getProducts);
            sqlsrv_close($conn);
        }
        catch(Exception $e)
        {
            echo("Error!");
        }
    }
```

## Step 3:  Insert a row

In this example, you'll see how to execute an [INSERT](../../t-sql/statements/insert-transact-sql.md) statement safely and pass parameters. Parameter values protect your application from [SQL injection](../../relational-databases/tables/primary-and-foreign-key-constraints.md).

```php
    function InsertData()
    {
        try
        {
            $conn = OpenConnection();

            $tsql = "INSERT SalesLT.Product (Name, ProductNumber, StandardCost, ListPrice, SellStartDate) OUTPUT"
                    . " INSERTED.ProductID VALUES ('SQL Server 1', 'SQL Server 2', 0, 0, getdate())";
            //Insert query
            $insertReview = sqlsrv_query($conn, $tsql);
            if($insertReview == FALSE)
                die(FormatErrors( sqlsrv_errors()));
            echo "Product Key inserted is :";
            while($row = sqlsrv_fetch_array($insertReview, SQLSRV_FETCH_ASSOC))
            {
                echo($row['ProductID']);
            }
            sqlsrv_free_stmt($insertReview);
            sqlsrv_close($conn);
        }
        catch(Exception $e)
        {
            echo("Error!");
        }
    }
```

## Step 4:  Roll back a transaction

This code example demonstrates the use of transactions in which you:

- Begin a transaction


- Insert a row of data, Update another row of data


- Commit your transaction if the insert and update were successful and roll back the transaction if one of them wasn't


```php
    function Transactions()
    {
        try
        {
            $conn = OpenConnection();

            if (sqlsrv_begin_transaction($conn) == FALSE)
                die(FormatErrors(sqlsrv_errors()));

            $tsql1 = "INSERT INTO SalesLT.SalesOrderDetail (SalesOrderID,OrderQty,ProductID,UnitPrice)
            VALUES (71774, 22, 709, 33)";
            $stmt1 = sqlsrv_query($conn, $tsql1);

            /* Set up and execute the second query. */
            $tsql2 = "UPDATE SalesLT.SalesOrderDetail SET OrderQty = (OrderQty + 1) WHERE ProductID = 709";
            $stmt2 = sqlsrv_query( $conn, $tsql2);

            /* If both queries were successful, commit the transaction. */
            /* Otherwise, rollback the transaction. */
            if($stmt1 && $stmt2)
            {
                   sqlsrv_commit($conn);
                   echo("Transaction was commited");
            }
            else
            {
                sqlsrv_rollback($conn);
                echo "Transaction was rolled back.\n";
            }
            /* Free statement and connection resources. */
            sqlsrv_free_stmt( $stmt1);
            sqlsrv_free_stmt( $stmt2);
        }
        catch(Exception $e)
        {
            echo("Error!");
        }
    }
```

## More examples

[Example Application (SQLSRV Driver)](example-application-sqlsrv-driver.md)  
[Example Application (PDO_SQLSRV Driver)](example-application-pdo-sqlsrv-driver.md)  
