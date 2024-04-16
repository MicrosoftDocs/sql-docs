---
title: Query SQL Database with query editor in the Azure portal
titleSuffix: Azure SQL Database
description: Learn how to connect to an Azure SQL database and use the Azure portal query editor (preview) to run Transact-SQL (T-SQL) queries.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: ivujic, mathoma
ms.date: 04/15/2024
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
monikerRange: "=azuresql||=azuresql-db"
---
# Quickstart: Use the Azure portal query editor to query Azure SQL Database

[!INCLUDE [appliesto-sqldb](../includes/appliesto-sqldb.md)]

The Azure SQL Database [query editor](query-editor.md) (preview) is a tool to run SQL queries against Azure SQL Database in the Azure portal. In this quickstart, you connect to an Azure SQL database in the Azure portal and use query editor to run Transact-SQL (T-SQL) queries.

- If you don't already have an Azure SQL Database created, visit [Quickstart: Create a single database](single-database-create-quickstart.md). Look for the option to use your offer to [try Azure SQL Database for free (preview)](free-offer.md).

## Prerequisites

### Authentication

You need an account with permissions to connect to the database and query editor. You can use SQL authentication or [Microsoft Entra ID](/entra/fundamentals/new-name)) authentication (recommended). For more information on creating and managing logins in Azure SQL database, visit [Authorize database access](logins-create-manage.md?view=azuresql-db&preserve-view=true).

For more information about public network access, TLS version settings, and connection policy, see [Azure SQL connectivity settings](connectivity-settings.md?view=azuresql-db&preserve-view=true).

### Firewall rule

For public connections to the query editor, you need to [add your outbound IP address to the server's allowed firewall rules](firewall-create-server-level-portal-quickstart.md?view=azuresql-db&preserve-view=true) to access your databases.

If you receive this error, use the following steps to resolve:

   `Cannot open server 'server-name' requested by the login. Client with IP address 'xx.xx.xx.xx' is not allowed to access the server. To enable access, use the Azure Management Portal or run sp_set_firewall_rule on the master database to create a firewall rule for this IP address or address range. It may take up to five minutes for this change to take effect.`

Follow the quick steps below, or for more information, see [add your outbound IP address to the server's allowed firewall rules](firewall-create-server-level-portal-quickstart.md).

1. Return to the **Overview** page of your SQL database.
1. Select the link for the Azure SQL logical server next to **Server name**.
1. In the Resource menu, under **Security**, select **Networking**.
1. Ensure that under **Public network access**, the **Selected networks** option is selected.
    - If this is a test or temporary environment, set the option to **Selected networks**.
    - If not, access must be granted through other means than covered in this quickstart, likely via [private endpoints](private-endpoint-overview.md) (by using Azure Private Link) as outlined in the [network access overview](network-access-controls-overview.md).
1. Under **Firewall rules**, select **Add your client IPv4 address**.
    - If necessary, identify your IPv4 address and provide it in the **Start** and **End** fields.
1. Select **Save**.

For troubleshooting, see [Connection error troubleshooting](query-editor.md#connection-considerations).

## Connect to the query editor

Connect to your database within the query editor.

1. Navigate to your SQL database in the [Azure portal](https://portal.azure.com). For example, visit [your Azure SQL dashboard](https://portal.azure.com/#view/HubsExtension/BrowseResource/resourceType/Microsoft.Sql%2Fazuresql).

1. On your SQL database **Overview** page, select **Query editor (preview)** from the resource menu.

   :::image type="content" source="media/connect-query-portal/find-query-editor.png" alt-text="Screenshot that shows selecting query editor.":::

1. On the sign-in screen, provide credentials to connect to the database.

   - You can connect using SQL or Microsoft Entra authentication.

      - To connect with SQL authentication, under **SQL server authentication**, enter a **Login** and **Password** for a user that has access to the database, and then select **OK**. You can always use the login and password for the server admin.

      - To connect using Microsoft Entra ID, if you're the Microsoft Entra server admin, select **Continue as \<user@domain>**. If sign-in is unsuccessful, try refreshing the page.

         :::image type="content" source="media/connect-query-portal/query-editor-entra-login.png" alt-text="Screenshot from the Azure portal showing sign-in with Microsoft Entra authentication." lightbox="media/connect-query-portal/query-editor-entra-login.png":::

### Connection with other tools

You can also connect to your Azure SQL database using other tools, including:

- [Quickstart: Use Azure Data Studio to connect and query Azure SQL Database](/azure-data-studio/quickstart-sql-database)
- [Quickstart: Use SSMS to connect to and query Azure SQL Database or Azure SQL Managed Instance](connect-query-ssms.md?view=azuresql-db&preserve-view=true)
- [Quickstart: Use Visual Studio Code to connect and query](connect-query-vscode.md?view=azuresql-db&preserve-view=true)

## Query the database

On any database, execute the following query in the Query editor to return the time in UTC, the database name, and your authenticated login name.

```sql
SELECT SYSDATETIMEOFFSET(), DB_NAME(), ORIGINAL_LOGIN();
```

### Query the AdventureWorksLT sample database

This portion of quickstart uses the `AdventureWorksLT` sample database in an Azure SQL database. If you don't have one already, you can [create a database using sample data in Azure SQL Database](single-database-create-quickstart.md). Look for the option to use your offer to [try Azure SQL Database for free (preview)](free-offer.md).

On the **Query editor (preview)** page, run the following example queries against your `AdventureWorksLT` sample database.

> [!TIP]
> New to Azure SQL Database? Get up to speed with in-depth free training content: [Azure SQL Fundamentals](/training/paths/azure-sql-fundamentals/) or review the [Azure SQL glossary of terms](../glossary-terms.md).

For more information about T-SQL in Azure SQL Database, visit [T-SQL differences between SQL Server and Azure SQL Database](transact-sql-tsql-differences-sql-server.md).

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

1. Optionally, you can select **Save query** to save the query as an *.sql* file, or select **Export data as** to export the results as a *.json*, *.csv*, or *.xml* file.

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

1. Select **Run** to add the new product. After the query runs, the **Messages** pane displays **Query succeeded: Affected rows: 1**.

#### Run an UPDATE query

Run the following [UPDATE](/sql/t-sql/queries/update-transact-sql/) T-SQL statement to update the price of your new product.

1. In the query editor, replace the previous query with the following query:

   ```sql
   UPDATE [SalesLT].[Product]
   SET [ListPrice] = 125
   WHERE Name = 'myNewProduct';
   ```

1. Select **Run** to update the specified row in the `Product` table. The **Messages** pane displays **Query succeeded: Affected rows: 1**.

#### Run a DELETE query

Run the following [DELETE](/sql/t-sql/statements/delete-transact-sql/) T-SQL statement to remove your new product.

1. In the query editor, replace the previous query with the following query:

   ```sql
   DELETE FROM [SalesLT].[Product]
   WHERE Name = 'myNewProduct';
   ```

1. Select **Run** to delete the specified row in the `Product` table. The **Messages** pane displays **Query succeeded: Affected rows: 1**.

## Related content

- [Query editor (preview)](query-editor.md)
- [Quickstart: Create a single database](single-database-create-quickstart.md)
- [Azure SQL connectivity settings](connectivity-settings.md)
