---
title: Use Visual Studio Code to connect and query
titleSuffix: Azure SQL Database & SQL Managed Instance
description: Learn how to connect to Azure SQL Database or Azure SQL Managed Instance by using Visual Studio Code. Then, run Transact-SQL (T-SQL) statements to query and edit data.
author: dzsquared
ms.author: drskwier
ms.reviewer: wiassaf, mathoma, randolphwest
ms.date: 09/27/2023
ms.service: sql-db-mi
ms.subservice: connect
ms.topic: quickstart
ms.custom:
  - mode-ui
keywords: connect to sql database
monikerRange: "=azuresql || =azuresql-db || =azuresql-mi"
---
# Quickstart: Use Visual Studio Code to connect and query Azure SQL Database or Azure SQL Managed Instance

[!INCLUDE [appliesto-sqldb-sqlmi](../includes/appliesto-sqldb-sqlmi.md)]

[Visual Studio Code](https://code.visualstudio.com/docs) is a graphical code editor for Linux, macOS, and Windows. It supports extensions, including the [mssql extension](https://aka.ms/mssql-marketplace) for querying SQL Server, Azure SQL Database, Azure SQL Managed Instance, and a database in Azure Synapse Analytics. In this quickstart, you use Visual Studio Code to connect to Azure SQL Database or Azure SQL Managed Instance and then run Transact-SQL statements to query, insert, update, and delete data.

## Prerequisites

- A database in Azure SQL Database or Azure SQL Managed Instance. You can use one of these quickstarts to create and then configure a database in Azure SQL Database:

  | Action | Azure SQL Database | Azure SQL Managed Instance |
  | :--- | :--- | :--- |
  | Create | [Portal](single-database-create-quickstart.md) | [Portal](../managed-instance/instance-create-quickstart.md) |
  | | [CLI](scripts/create-and-configure-database-cli.md) | [CLI](https://medium.com/azure-sqldb-managed-instance/working-with-sql-managed-instance-using-azure-cli-611795fe0b44) |
  | | [PowerShell](scripts/create-and-configure-database-powershell.md) | [PowerShell](../managed-instance/scripts/create-configure-managed-instance-powershell.md) |
  | Configure | [Server-level IP firewall rule](firewall-create-server-level-portal-quickstart.md)) | [Connectivity from a virtual machine (VM)](../managed-instance/connect-vm-instance-configure.md) |
  |||[Connectivity from on-premises](../managed-instance/point-to-site-p2s-configure.md)
  |Load data|Wide World Importers loaded per quickstart|[Restore Wide World Importers](../managed-instance/restore-sample-database-quickstart.md)
  | | | Restore or import Adventure Works from a [BACPAC](database-import.md) file from [GitHub](https://github.com/Microsoft/sql-server-samples/tree/master/samples/databases/adventure-works) |

  > [!IMPORTANT]  
  > The scripts in this article are written to use the Adventure Works database. With a SQL Managed Instance, you must either import the Adventure Works database into an instance database or modify the scripts in this article to use the Wide World Importers database.

## Install Visual Studio Code

Make sure you have installed the latest [Visual Studio Code](https://code.visualstudio.com/Download). For installation guidance, see [Install Visual Studio Code](/sql/linux/sql-server-linux-develop-use-vscode#install-and-start-visual-studio-code).

## Configure Visual Studio Code

### Windows

Load the [mssql extension](https://aka.ms/mssql-marketplace) by following these steps:

1. Open Visual Studio Code.
1. Open the Extensions pane (or **Ctrl + Shift + X**).
1. Search for `sql` and then install the **SQL Server (mssql)** extension.

For additional installation guidance, see [mssql for Visual Studio Code](https://marketplace.visualstudio.com/items?itemName=ms-mssql.mssql).

### macOS

For macOS, you need to install OpenSSL, which is a prerequisite for .NET Core that mssql extension uses. Open your terminal and enter the following commands to install **brew** and **OpenSSL**.

```bash
ruby -e "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/master/install)"
brew update
brew install openssl
mkdir -p /usr/local/lib
ln -s /usr/local/opt/openssl/lib/libcrypto.1.0.0.dylib /usr/local/lib/
ln -s /usr/local/opt/openssl/lib/libssl.1.0.0.dylib /usr/local/lib/
```

### Linux (Ubuntu)

Load the [mssql extension](https://aka.ms/mssql-marketplace) by following these steps:

1. Open Visual Studio Code.
1. Open the Extensions pane (or **Ctrl + Shift + X**).
1. Search for `sql` and then install the **SQL Server (mssql)** extension.

For additional installation guidance, see [mssql for Visual Studio Code](https://marketplace.visualstudio.com/items?itemName=ms-mssql.mssql).

## Get server connection information

Get the connection information you need to connect to Azure SQL Database. You need the fully qualified server name or host name, database name, and login information for the upcoming procedures.

1. Sign in to the [Azure portal](https://portal.azure.com/).

1. Navigate to the **SQL databases**  or **SQL Managed Instances** page.

1. On the **Overview** page, review the fully qualified server name next to **Server name** for SQL Database or the fully qualified server name next to **Host** for a SQL Managed Instance. To copy the server name or host name, hover over it and select the **Copy** icon.

## Set language mode to SQL

In Visual Studio Code, set the language mode to **SQL**  to enable mssql commands and T-SQL IntelliSense.

1. Open a new Visual Studio Code window.

1. Press **Ctrl + N**. A new plain text file opens.

1. Select **Plain Text** in the status bar's lower right-hand corner.

1. In the **Select language mode** dropdown list that opens, select **SQL**.

## Connect to your database

Use Visual Studio Code to establish a connection to your server.

> [!IMPORTANT]  
> Before continuing, make sure that you have your server and sign-in information ready. Once you begin entering the connection profile information, if you change your focus from Visual Studio Code, you have to restart creating the profile.

1. In Visual Studio Code, press **Ctrl + Shift + P** (or **F1**) to open the Command Palette.

1. Type `connect` and then choose **MS SQL:Connect**.

1. Select **+ Create Connection Profile**.

1. Follow the prompts to specify the new profile's connection properties. After specifying each value, press **Enter** to continue.

   | Property       | Suggested value | Description  |
   | --- | --- | --- |
   | **Server name** | The fully qualified server name | Something like: **mynewserver20170313.database.windows.net**. |
   | **Database name** | mySampleDatabase | The database to connect to. |
   | **Authentication** | SQL Login | This tutorial uses SQL Authentication. |
   | **User name** | User name | The user name of the server admin account used to create the server. |
   | **Password (SQL Login)** | Password | The password of the server admin account used to create the server. |
   | **Save Password?** | Yes or No | Select **Yes** if you don't want to enter the password each time. |
   | **Enter a name for this profile** | A profile name, such as **mySampleProfile** | A saved profile speeds your connection on subsequent logins. |

   If successful, a notification appears saying your profile is created and connected.

## Query data

Run the following [SELECT](/sql/t-sql/queries/select-transact-sql) Transact-SQL statement to query for the top 20 products by category.

1. In the editor window, paste the following SQL query.

   ```sql
   SELECT pc.Name AS CategoryName,
       p.name AS ProductName
   FROM [SalesLT].[ProductCategory] pc
   INNER JOIN [SalesLT].[Product] p
       ON pc.ProductCategoryId = p.ProductCategoryId;
   ```

1. Press **Ctrl + Shift + E** to run the query and display results from the `Product` and `ProductCategory` tables.

    :::image type="content" source="./media/connect-query-vscode/query.png" alt-text="Screenshot of query to retrieve data from 2 tables." lightbox="./media/connect-query-vscode/query.png":::

## Insert data

Run the following [INSERT](/sql/t-sql/statements/insert-transact-sql) Transact-SQL statement to add a new product into the `SalesLT.Product` table.

1. Replace the previous query with this one.

   ```sql
   INSERT INTO [SalesLT].[Product] (
       [Name],
       [ProductNumber],
       [Color],
       [ProductCategoryID],
       [StandardCost],
       [ListPrice],
       [SellStartDate]
    )
    VALUES (
       'myNewProduct',
       123456789,
       'NewColor',
       1,
       100,
       100,
       GETDATE()
    );
   ```

1. Press **Ctrl + Shift + E** to insert a new row in the `Product` table.

## Update data

Run the following [UPDATE](/sql/t-sql/queries/update-transact-sql) Transact-SQL statement to update the added product.

1. Replace the previous query with this one:

   ```sql
   UPDATE [SalesLT].[Product]
   SET [ListPrice] = 125
   WHERE Name = 'myNewProduct';
   ```

1. Press **Ctrl + Shift + E** to update the specified row in the `Product` table.

## Delete data

Run the following [DELETE](/sql/t-sql/statements/delete-transact-sql) Transact-SQL statement to remove the new product.

1. Replace the previous query with this one:

   ```sql
   DELETE FROM [SalesLT].[Product]
   WHERE Name = 'myNewProduct';
   ```

1. Press **Ctrl + Shift + E** to delete the specified row in the `Product` table.

## Next steps

- [Quickstart: Use SQL Server Management Studio to connect to a database in Azure SQL Database and query data](connect-query-ssms.md)
- [Use the SQL Query editor in the Azure portal to connect and query data](query-editor.md)
- [Create a database IDE with MSSQL extension blog post](/archive/msdn-magazine/2017/june/data-points-visual-studio-code-create-a-database-ide-with-mssql-extension)
