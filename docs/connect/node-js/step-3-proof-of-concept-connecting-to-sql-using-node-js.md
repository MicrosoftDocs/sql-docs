---
title: "Step 3: Connecting to SQL using Node.js"
description: "This example should be considered a proof of concept showing how to connect to SQL using Node.js and is simplified for clarity."
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel
ms.date: "03/25/2026"
ms.service: sql
ms.subservice: connectivity
ms.topic: install-set-up-deploy
---
# Step 3: Proof of concept connecting to SQL using Node.js

:::image type="icon" source="../../includes/media/download.svg" border="false"::: **[Download Node.js SQL driver](../sql-connection-libraries.md#anchor-20-drivers-relational-access)**

This example should be considered a proof of concept only. The sample code is simplified for clarity, and doesn't necessarily represent best practices recommended by Microsoft. Other examples, which use the same crucial functions are available in the [GitHub sample repository](https://github.com/tediousjs/tedious/blob/master/examples/).

## Prerequisites

Before you run the sample code, make sure the following prerequisites are met:

- **Node.js** is installed on your development machine. Download it from [nodejs.org](https://nodejs.org/).
- The **tedious** npm package is installed. Run `npm install tedious` in your project directory.
- A SQL Server instance is available and configured to accept connections:
  - **TCP/IP protocol** is enabled in SQL Server Configuration Manager. TCP/IP is disabled by default on SQL Server Express editions.
  - **SQL Server Browser service** is running if you're connecting to a named instance.
  - The firewall allows connections on the SQL Server port (default: 1433).
  - For SQL Server authentication, a SQL login is enabled and SQL Server is configured for **mixed mode authentication**.

> [!TIP]
> If you receive a connection error such as `Failed to connect`, verify that TCP/IP is enabled and the SQL Server service is running. For local development, try connecting with `server: 'localhost'` or `server: '127.0.0.1'`.

## Step 1: Connect  
  
Use the **new Connection** function to connect to SQL Database.  
  
```javascript  
    var Connection = require('tedious').Connection;  
    var config = {  
        server: 'your_server.database.windows.net',  //update me
        authentication: {
            type: 'default',
            options: {
                userName: 'your_username', //update me
                password: 'your_password'  //update me
            }
        },
        options: {
            // If you're on Microsoft Azure, you need encryption:
            encrypt: true,
            database: 'your_database'  //update me
        }
    };  
    var connection = new Connection(config);  
    connection.on('connect', function(err) {  
        // If no error, then good to proceed.
        console.log("Connected");  
    });
    
    connection.connect();

```  
  
## Step 2: Execute a query  
  
  
Execute all SQL statements using the **new Request** function. If the statement returns rows, such as a select statement, you can retrieve them using the **request.on** function. If there are no rows, the request.on function returns empty lists.  
  
  
```javascript  
    var Connection = require('tedious').Connection;  
    var config = {  
        server: 'your_server.database.windows.net',  //update me
        authentication: {
            type: 'default',
            options: {
                userName: 'your_username', //update me
                password: 'your_password'  //update me
            }
        },
        options: {
            // If you're on Microsoft Azure, you need encryption:
            encrypt: true,
            database: 'your_database'  //update me
        }
    }; 
    var connection = new Connection(config);  
    connection.on('connect', function(err) {  
        // If no error, then good to proceed.  
        console.log("Connected");  
        executeStatement();  
    });  
    
    connection.connect();
  
    var Request = require('tedious').Request;  
    var TYPES = require('tedious').TYPES;  
  
    function executeStatement() {  
        var request = new Request("SELECT c.CustomerID, c.CompanyName,COUNT(soh.SalesOrderID) AS OrderCount FROM SalesLT.Customer AS c LEFT OUTER JOIN SalesLT.SalesOrderHeader AS soh ON c.CustomerID = soh.CustomerID GROUP BY c.CustomerID, c.CompanyName ORDER BY OrderCount DESC;", function(err) {  
        if (err) {  
            console.log(err);}  
        });  
        var result = "";  
        request.on('row', function(columns) {  
            columns.forEach(function(column) {  
              if (column.value === null) {  
                console.log('NULL');  
              } else {  
                result+= column.value + " ";  
              }  
            });  
            console.log(result);  
            result ="";  
        });  
  
        request.on('done', function(rowCount, more) {  
        console.log(rowCount + ' rows returned');  
        });  
        
        // Close the connection after the final event emitted by the request, after the callback passes
        request.on("requestCompleted", function (rowCount, more) {
            connection.close();
        });
        connection.execSql(request);  
    }  
```  
  
## Step 3: Insert a row  
  
In this example, you see how to execute an [INSERT](../../t-sql/statements/insert-transact-sql.md) statement safely, passing parameters, which protect your application from [SQL injection](../../relational-databases/security/sql-injection.md) values.    
  
  
```javascript  
    var Connection = require('tedious').Connection;  
    var config = {  
        server: 'your_server.database.windows.net',  //update me
        authentication: {
            type: 'default',
            options: {
                userName: 'your_username', //update me
                password: 'your_password'  //update me
            }
        },
        options: {
            // If you're on Microsoft Azure, you need encryption:
            encrypt: true,
            database: 'your_database'  //update me
        }
    };  
    var connection = new Connection(config);  
    connection.on('connect', function(err) {  
        // If no error, then good to proceed.  
        console.log("Connected");  
        executeStatement1();  
    });
    
    connection.connect();
    
    var Request = require('tedious').Request  
    var TYPES = require('tedious').TYPES;  
  
    function executeStatement1() {  
        var request = new Request("INSERT SalesLT.Product (Name, ProductNumber, StandardCost, ListPrice, SellStartDate) OUTPUT INSERTED.ProductID VALUES (@Name, @Number, @Cost, @Price, CURRENT_TIMESTAMP);", function(err) {  
         if (err) {  
            console.log(err);}  
        });  
        request.addParameter('Name', TYPES.NVarChar,'SQL Server Express 2014');  
        request.addParameter('Number', TYPES.NVarChar , 'SQLEXPRESS2014');  
        request.addParameter('Cost', TYPES.Int, 11);  
        request.addParameter('Price', TYPES.Int,11);  
        request.on('row', function(columns) {  
            columns.forEach(function(column) {  
              if (column.value === null) {  
                console.log('NULL');  
              } else {  
                console.log("Product id of inserted item is " + column.value);  
              }  
            });  
        });

        // Close the connection after the final event emitted by the request, after the callback passes
        request.on("requestCompleted", function (rowCount, more) {
            connection.close();
        });
        connection.execSql(request);  
    }  
```  

## Step 4: Connect with Windows authentication

The tedious driver supports Windows authentication using NTLM. To connect with domain credentials instead of SQL Server authentication, change the `authentication` section in the connection configuration:

```javascript
var Connection = require('tedious').Connection;
var config = {
    server: '<server>',  //update me
    authentication: {
        type: 'ntlm',
        options: {
            domain: '<domain>',    //update me
            userName: '<username>', //update me
            password: '<password>'  //update me
        }
    },
    options: {
        encrypt: false,
        database: '<database>',  //update me
        port: 1433
    }
};
var connection = new Connection(config);
connection.on('connect', function(err) {
    if (err) {
        console.log('Connection failed', err);
    } else {
        console.log('Connected with Windows authentication');
    }
});

connection.connect();
```

> [!NOTE]
> NTLM authentication requires you to provide domain credentials in the configuration. For trusted connections that use the currently logged-in Windows user without specifying credentials, consider the [msnodesqlv8](https://www.npmjs.com/package/msnodesqlv8) package, which uses the native ODBC driver.

## Related content

- [Node.js Driver for SQL Server](node-js-driver-for-sql-server.md)
- [Step 1:  Configure development environment for Node.js development](step-1-configure-development-environment-for-node-js-development.md)
- [Step 2: Create a SQL database for Node.js development](step-2-create-a-sql-database-for-node-js-development.md)
