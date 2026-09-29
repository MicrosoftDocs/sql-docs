---
title: Query SQL Database with Query Editor in the Azure Portal
titleSuffix: Azure SQL Database
description: Learn how to connect to an Azure SQL database and use the Azure portal query editor to run Transact-SQL (T-SQL) queries.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: ivujic, mathoma
ms.date: 09/01/2026
ms.service: azure-sql-database
ms.subservice: development
ms.topic: quickstart
ms.custom:
  - sqldbrb=1
  - mode-ui
  - kr2b-contr-experiment
  - sfi-image-nochange
keywords:
  - connect to sql database
  - query sql database
  - azure portal
  - portal
  - query editor
monikerRange: "=azuresql || =azuresql-db"
---

# Quickstart: Use the Azure portal query editor to query Azure SQL Database

[!INCLUDE [appliesto-sqldb](../includes/appliesto-sqldb.md)]

In this quickstart, connect to an Azure SQL database in the Azure portal and use the query editor to run Transact-SQL (T-SQL) queries. The [Azure SQL Database query editor](query-editor.md) is a tool to run SQL queries against Azure SQL Database in the Azure portal.

- If you don't already have an Azure SQL Database created, see [Quickstart: Create a single database - Azure SQL Database](single-database-create-quickstart.md). Look for the option to use your offer to [Deploy Azure SQL Database for free](free-offer.md).

## Prerequisites

### Authentication

You need an account with permissions to connect to the database and query editor. You can use SQL authentication or Microsoft Entra authentication (recommended). Users need at least the Azure role-based access control (RBAC) permission **Read access to the server and database** to use the query editor. For more information on creating and managing logins in Azure SQL Database, see [Authorize database access](logins-create-manage.md?view=azuresql-db&preserve-view=true).

### Firewall rule

#### Azure SQL logical server firewall

If you receive this error: *Your IP address isn't allowed to access this server. Add your IP address to the server's firewall rules or select Allowlist IP xx.xx.xx.xx below. Changes can take up to 5 minutes to take effect.* Select the **Allowlist IP...** link inside the error message box and the Azure portal automatically adds your current IP address to the [allow list of the logical server firewall](firewall-configure.md?view=azuresql-db&preserve-view=true#create-and-manage-ip-firewall-rules). After the firewall rules are updated, select **Connect** again to proceed.

- For more information, see [add your outbound IP address to the server's allowed firewall rules](firewall-create-server-level-portal-quickstart.md?view=azuresql-db&preserve-view=true).
- For troubleshooting, see [Connection error troubleshooting](query-editor.md#connection-considerations).
- For more information about public network access, TLS version settings, and connection policy, see [Azure SQL connectivity settings](connectivity-settings.md?view=azuresql-db&preserve-view=true).

#### Local firewall

The Azure SQL query editor uses port TCP 443. Verify this port is open in your local and corporate firewalls. For more information, see [Port 443 connectivity](query-editor.md#port-443-connectivity).

## Connect to the query editor

Follow these steps to connect to your database within the query editor.

1. Go to your SQL database in the Azure portal. For example, visit [your Azure SQL hub page](https://aka.ms/azuresqlhub), select **Azure SQL Database**, and then select **SQL databases**. Select your Azure SQL Database.

1. On your SQL database **Overview** page, select **Query editor** from the resource menu.

   :::image type="content" source="media/connect-query-portal/find-query-editor.png" alt-text="Screenshot that shows selecting query editor.":::

1. On the sign-in screen, choose either **Microsoft Entra authentication** (recommended) or **SQL authentication**.

   - To connect by using SQL authentication, under **SQL server authentication**, enter a **Login** and **Password** for a user that has access to the database, and then select **OK**. You can always use the login and password for the server admin.

   - To connect by using Microsoft Entra ID, select **Continue as \<user@domain>**.

      :::image type="content" source="media/connect-query-portal/query-editor-entra-login.png" alt-text="Screenshot from the Azure portal showing sign-in with Microsoft Entra authentication." lightbox="media/connect-query-portal/query-editor-entra-login.png":::

## Query the database

On any database, run the following query in the Query editor to get the time in UTC, the database name, and your authenticated login name.

```sql
SELECT SYSDATETIMEOFFSET(), DB_NAME(), ORIGINAL_LOGIN();
```

The results look like you'd expect from any T-SQL query tool:

:::image type="content" source="media/connect-query-portal/sample-query.png" alt-text="Screenshot from the Azure portal SQL query editor for Azure SQL Database.":::

### Query the AdventureWorksLT sample database

This portion of quickstart uses the `AdventureWorksLT` sample database in an Azure SQL database. If you don't have one already, you can [create a database using sample data in Azure SQL Database](single-database-create-quickstart.md). Look for the option to use your offer to [Deploy Azure SQL Database for free](free-offer.md).

On the **Query editor** page, run the following example queries against your `AdventureWorksLT` sample database.

> [!TIP]
> New to Azure SQL Database? Get up to speed with in-depth free training content: [Azure SQL Fundamentals](/training/paths/azure-sql-fundamentals/) or review the [Azure SQL glossary of terms](../glossary-terms.md).

For more information about T-SQL in Azure SQL Database, see [T-SQL differences between SQL Server and Azure SQL Database](transact-sql-tsql-differences-sql-server.md).

#### Run a SELECT query

1. To query for the top 20 products in the database, paste the following [SELECT](/sql/t-sql/queries/select-transact-sql) query into the query editor:

   ```sql
    SELECT TOP 20 pc.Name as CategoryName, p.name as ProductName
    FROM SalesLT.ProductCategory pc
    JOIN SalesLT.Product p
    ON pc.productcategoryid = p.productcategoryid;
   ```

1. Select **Run**, and then review the output in the **Results** pane.

   :::image type="content" source="media/connect-query-portal/query-editor-results.png" alt-text="Screenshot showing query editor results for a SELECT query." lightbox="media/connect-query-portal/query-editor-results.png":::

1. Optionally, you can select the query to save it as a view by using the **Save as view** button.
1. You can download the query results by using the **Download data as** option to export the results as a *.csv*, *.json*, or *.xlsx* file.

#### Run an INSERT query

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

1. Select **Run** to add the new product. After the query runs, the **Messages** pane displays **Started executing on line 1 Query executed successfully**.

#### Run an UPDATE query

Run the following [UPDATE](/sql/t-sql/queries/update-transact-sql/) T-SQL statement to update the price of your new product.

1. In the query editor, replace the previous query with the following query:

   ```sql
   UPDATE [SalesLT].[Product]
   SET [ListPrice] = 125
   WHERE Name = 'myNewProduct';
   ```

1. Select **Run** to update the specified row in the `Product` table. The **Messages** pane displays  **Started executing on line 1 Query executed successfully**.

#### Run a DELETE query

Run the following [DELETE](/sql/t-sql/statements/delete-transact-sql/) T-SQL statement to remove your new product.

1. In the query editor, replace the previous query with the following query:

   ```sql
   DELETE FROM [SalesLT].[Product]
   WHERE Name = 'myNewProduct';
   ```

1. Select **Run** to delete the specified row in the `Product` table. The **Messages** pane displays  **Started executing on line 1 Query executed successfully**.

## Related content

- [Azure portal query editor for Azure SQL Database](query-editor.md)
- [Quickstart: Create a single database - Azure SQL Database](single-database-create-quickstart.md)
- [Connectivity settings](connectivity-settings.md)
