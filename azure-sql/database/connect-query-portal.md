---
title: Query SQL Database with Query editor in the Azure portal
titleSuffix: Azure SQL Database
description: Learn how to connect to an Azure SQL database and use the Azure portal Query editor (preview) to run Transact-SQL (T-SQL) queries.
author: grrlgeek
ms.author: jeschult
ms.reviewer: wiassaf, mathoma, mbarickman
ms.date: 03/20/2023
ms.service: sql-database
ms.subservice: development
ms.topic: quickstart
ms.custom:
  - sqldbrb=1
  - mode-ui
  - kr2b-contr-experiment
keywords:
  - connect to sql database
  - query sql database
  - azure portal
  - portal
  - query editor
monikerRange: "= azuresql || = azuresql-db || = azuresql-mi"
---
# Quickstart: Use the Azure portal query editor to query Azure SQL Database

[!INCLUDE[appliesto-sqldb](../includes/appliesto-sqldb.md)]

The Azure SQL Database [Query editor](query-editor.md) (preview) is a tool to run SQL queries against Azure SQL Database in the Azure portal. In this quickstart, you connect to an Azure SQL database in the Azure portal and use query editor to run Transact-SQL (T-SQL) queries.

## Prerequisites

- This quickstart uses the `AdventureWorksLT` sample database in an Azure SQL database. If you don't have one already, you can [create a database using sample data in Azure SQL Database](single-database-create-quickstart.md).

- A user account with permissions to connect to the database and query editor. You can either:

  - Have or set up a user that can connect to the database with SQL Authentication.
  - Have or set up a user that can connect to the database with Azure Active Directory (Azure AD) authentication.

## Connect to the query editor

1. On your SQL database **Overview** page in the [Azure portal](https://portal.azure.com), select **Query editor (preview)** from the left menu.

   :::image type="content" source="./media/connect-query-portal/find-query-editor.png" alt-text="Screenshot that shows selecting query editor.":::

1. On the sign-in screen, provide credentials to connect to the database. You can connect using SQL authentication or Azure AD.

   - To connect with SQL authentication, under **SQL server authentication**, enter a **Login** and **Password** for a user that has access to the database, and then select **OK**. You can always use the login and password for the server admin.

     :::image type="content" source="./media/connect-query-portal/login-menu.png" alt-text="Screenshot showing sign-in with SQL authentication.":::

   - To connect using Azure AD, if you're the Azure AD server admin, select **Continue as \<your user or group ID>**. If sign-in is unsuccessful, try refreshing the page.

## Query the database

On the **Query editor (preview)** page, run the following example queries against your `AdventureWorksLT` sample database.

### Run a SELECT query

1. To query for the top 20 products in the database, paste the following [SELECT](/sql/t-sql/queries/select-transact-sql) query into the query editor:

   ```sql
    SELECT TOP 20 pc.Name as CategoryName, p.name as ProductName
    FROM SalesLT.ProductCategory pc
    JOIN SalesLT.Product p
    ON pc.productcategoryid = p.productcategoryid;
   ```

1. Select **Run**, and then review the output in the **Results** pane.

   :::image type="content" source="./media/connect-query-portal/query-editor-results.png" alt-text="Screenshot showing query editor results for a SELECT query.":::

1. Optionally, you can select **Save query** to save the query as an *.sql* file, or select **Export data as** to export the results as a *.json*, *.csv*, or *.xml* file.

### Run an INSERT query

To add a new product to the `SalesLT.Product` table, run the following [INSERT](/sql/t-sql/statements/insert-transact-sql/) T-SQL statement.

1. In the query editor, replace the previous query with the following query:

    ```sql
    INSERT INTO [SalesLT].[Product]
           ( [Name]
           , [ProductNumber]
           , [Color]
           , [ProductCategoryID]
           , [StandardCost]
           , [ListPrice]
           , [SellStartDate]
           )
    VALUES
           ('myNewProduct'
           ,123456789
           ,'NewColor'
           ,1
           ,100
           ,100
           ,GETDATE() );
   ```

1. Select **Run** to add the new product. After the query runs, the **Messages** pane displays **Query succeeded: Affected rows: 1**.

### Run an UPDATE query

Run the following [UPDATE](/sql/t-sql/queries/update-transact-sql/) T-SQL statement to update the price of your new product.

1. In the query editor, replace the previous query with the following query:

   ```sql
   UPDATE [SalesLT].[Product]
   SET [ListPrice] = 125
   WHERE Name = 'myNewProduct';
   ```

1. Select **Run** to update the specified row in the `Product` table. The **Messages** pane displays **Query succeeded: Affected rows: 1**.

### Run a DELETE query

Run the following [DELETE](/sql/t-sql/statements/delete-transact-sql/) T-SQL statement to remove your new product.

1. In the query editor, replace the previous query with the following query:

   ```sql
   DELETE FROM [SalesLT].[Product]
   WHERE Name = 'myNewProduct';
   ```

1. Select **Run** to delete the specified row in the `Product` table. The **Messages** pane displays **Query succeeded: Affected rows: 1**.


## Next steps

- [Query editor (preview)](query-editor.md)
- [What is Azure SQL?](../azure-sql-iaas-vs-paas-what-is-overview.md)
- [Azure SQL glossary of terms](../glossary-terms.md)
- [T-SQL differences between SQL Server and Azure SQL Database](transact-sql-tsql-differences-sql-server.md)
- [Quickstart: Use Azure Data Studio to connect and query Azure SQL Database](/sql/azure-data-studio/quickstart-sql-database)
- [Quickstart: Use SSMS to connect to and query Azure SQL Database or Azure SQL Managed Instance](connect-query-ssms.md)
- [Quickstart: Use Visual Studio Code to connect and query](connect-query-vscode.md)