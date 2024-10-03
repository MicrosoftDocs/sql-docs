---
title: Use Node.js to query a database
titleSuffix: Azure SQL Database & Azure SQL Managed Instance
description: How to use Node.js to create a program that connects to a database in Azure SQL Database or Azure SQL Managed Instance, and query it using T-SQL statements.
author: dzsquared
ms.author: drskwier
ms.reviewer: wiassaf, mathoma, v-masebo
ms.date: 12/19/2022
ms.service: azure-sql
ms.subservice: connect
ms.topic: quickstart
ms.custom: sqldbrb=2, devx-track-js, mode-api
ms.devlang: javascript
monikerRange: "= azuresql || = azuresql-db || = azuresql-mi"
---
# Quickstart: Use Node.js to query a database in Azure SQL Database or Azure SQL Managed Instance
[!INCLUDE[appliesto-sqldb-sqlmi](../includes/appliesto-sqldb-sqlmi.md)]

In this quickstart, you use Node.js to connect to a database and query data.

## Prerequisites

To complete this quickstart, you need:

- An Azure account with an active subscription and a database in Azure SQL Database, Azure SQL Managed Instance, or SQL Server on Azure VM. [Create an account for free](https://azure.microsoft.com/free/?ref=microsoft.com&utm_source=microsoft.com&utm_medium=docs&utm_campaign=visualstudio).

  | Action | SQL Database | SQL Managed Instance | SQL Server on Azure VM |
  |:--- |:--- |:---|:---|
  | Create| [Portal](single-database-create-quickstart.md) | [Portal](../managed-instance/instance-create-quickstart.md) | [Portal](../virtual-machines/windows/sql-vm-create-portal-quickstart.md)
  || [CLI](scripts/create-and-configure-database-cli.md) | [Bicep](../managed-instance/create-bicep-quickstart.md) |
  || [PowerShell](scripts/create-and-configure-database-powershell.md) | [PowerShell](../managed-instance/scripts/create-configure-managed-instance-powershell.md) | [PowerShell](../virtual-machines/windows/sql-vm-create-powershell-quickstart.md)
  | Configure | [Server-level IP firewall rule](firewall-create-server-level-portal-quickstart.md)| [Connectivity from a VM](../managed-instance/connect-vm-instance-configure.md)|
  |||[Connectivity from on-premises](../managed-instance/point-to-site-p2s-configure.md) | [Connect to a SQL Server instance](../virtual-machines/windows/sql-vm-create-portal-quickstart.md)
  |Load data|Wide World Importers loaded per quickstart|[Restore Wide World Importers](../managed-instance/restore-sample-database-quickstart.md) | [Restore Wide World Importers](../managed-instance/restore-sample-database-quickstart.md) |
  |||Restore or import AdventureWorks from a [BACPAC](database-import.md) file from [GitHub](https://github.com/Microsoft/sql-server-samples/tree/master/samples/databases/adventure-works)| Restore or import AdventureWorks from a [BACPAC](database-import.md) file from [GitHub](https://github.com/Microsoft/sql-server-samples/tree/master/samples/databases/adventure-works)|



- [Node.js](https://nodejs.org)-related software

  # [macOS](#tab/macos)

  Install Node.js and then install the ODBC driver using the steps on [Install the Microsoft ODBC driver for SQL Server (macOS)](/sql/connect/odbc/linux-mac/install-microsoft-odbc-driver-sql-server-macos).

  # [Ubuntu](#tab/ubuntu)

  Install Node.js and then install the ODBC driver using the steps on [Install the Microsoft ODBC driver for SQL Server (Linux)](/sql/connect/odbc/linux-mac/installing-the-microsoft-odbc-driver-for-sql-server).

  # [Windows](#tab/windows)

  Install Node.js and then install the ODBC driver using the steps on [Download ODBC Driver for SQL Server](/sql/connect/odbc/download-odbc-driver-for-sql-server).

  ---

> [!IMPORTANT]
> The scripts in this article are written to use the **AdventureWorks** database.

## Get server connection information

Get the connection information you need to connect to the database. You'll need the fully qualified server name or host name, database name, and login information for the upcoming steps.

1. Sign in to the [Azure portal](https://portal.azure.com/).

2. Go to the **SQL Databases**  or **SQL Managed Instances** page.

3. On the **Overview** page, review the fully qualified server name next to **Server name** for a database in Azure SQL Database or the fully qualified server name (or IP address) next to **Host** for an Azure SQL Managed Instance or SQL Server on Azure VM. To copy the server name or host name, hover over it and select the **Copy** icon.

> [!NOTE]
> For connection information for SQL Server on Azure VM, see [Connect to SQL Server](../virtual-machines/windows/sql-vm-create-portal-quickstart.md#connect-to-sql-server).

## Create the project

Open a command prompt and create a folder named *sqltest*. Open the folder you created and run the following command:

  ```bash
  npm init -y
  npm install mssql
  ```

## Add code to query the database

1. In your favorite text editor, create a new file, *sqltest.js*, in the folder where you created the project (*sqltest*).

1. Replace its contents with the following code. Then add the appropriate values for your server, database, user, and password.

    ```js
    const sql = require('mssql');

    const config = {
        user: 'username', // better stored in an app setting such as process.env.DB_USER
        password: 'password', // better stored in an app setting such as process.env.DB_PASSWORD
        server: 'your_server.database.windows.net', // better stored in an app setting such as process.env.DB_SERVER
        port: 1433, // optional, defaults to 1433, better stored in an app setting such as process.env.DB_PORT
        database: 'AdventureWorksLT', // better stored in an app setting such as process.env.DB_NAME
        authentication: {
            type: 'default'
        },
        options: {
            encrypt: true
        }
    }

    /*
        //Use Azure VM Managed Identity to connect to the SQL database
        const config = {
            server: process.env["db_server"],
            port: process.env["db_port"],
            database: process.env["db_database"],
            authentication: {
                type: 'azure-active-directory-msi-vm'
            },
            options: {
                encrypt: true
            }
        }

        //Use Azure App Service Managed Identity to connect to the SQL database
        const config = {
            server: process.env["db_server"],
            port: process.env["db_port"],
            database: process.env["db_database"],
            authentication: {
                type: 'azure-active-directory-msi-app-service'
            },
            options: {
                encrypt: true
            }
        }
    */

    console.log("Starting...");
    connectAndQuery();

    async function connectAndQuery() {
        try {
            var poolConnection = await sql.connect(config);
            
            console.log("Reading rows from the Table...");
            var resultSet = await poolConnection.request().query(`SELECT TOP 20 pc.Name as CategoryName,
                p.name as ProductName 
                FROM [SalesLT].[ProductCategory] pc
                JOIN [SalesLT].[Product] p ON pc.productcategoryid = p.productcategoryid`);

            console.log(`${resultSet.recordset.length} rows returned.`);

            // output column headers
            var columns = "";
            for (var column in resultSet.recordset.columns) {
                columns += column + ", ";
            }
            console.log("%s\t", columns.substring(0, columns.length - 2));

            // ouput row contents from default record set
            resultSet.recordset.forEach(row => {
                console.log("%s\t%s", row.CategoryName, row.ProductName);
            });

            // close connection only when we're certain application is finished
            poolConnection.close();
        } catch (err) {
            console.error(err.message);
        }
    }
    ```

> [!NOTE]
> For more information about using managed identity for authentication, complete the tutorial to [access data via managed identity](/azure/app-service/tutorial-connect-msi-sql-database). Details about the Tedious configuration options for Microsoft Entra ID ([formerly Azure Active Directory](/entra/fundamentals/new-name)) are available in the [Tedious documentation](http://tediousjs.github.io/tedious/api-connection.html).


## Run the code

1. At the command prompt, run the program.

    ```bash
    node sqltest.js
    ```

1. Verify the top 20 rows are returned and close the application window.

## Next steps

- [Create a Node.js web app in Azure](/azure/app-service/quickstart-nodejs)

- [Configure Node.js apps](/azure/app-service/configure-language-nodejs)

- [Quickstart: Create a JavaScript function in Azure using Visual Studio Code](/azure/azure-functions/create-first-function-vs-code-node)

- [Use SQL bindings in JavaScript Azure Functions](/azure/azure-functions/functions-bindings-azure-sql?pivots=programming-language-javascript)


- [Connect using Node.js `tedious` module for SQL Server](/sql/connect/node-js/node-js-driver-for-sql-server)
