---
title: Connect and query with Azure Synapse Analytics
description: This quickstart shows connecting to a dedicated SQL pool in Azure Synapse Analytics using Azure Data Studio.
ms.prod: azure-data-studio
ms.technology: azure-data-studio
ms.topic: quickstart
author: yualan
ms.author: alayu
ms.reviewer: maghan, jrasnick
ms.custom: seodec18; seo-lt-2019
ms.date: 10/15/2020
---

# Quickstart: Use Azure Data Studio to connect and query data using a dedicated SQL pool in Azure Synapse Analytics

This quickstart shows connecting to a dedicated SQL pool in Azure Synapse Analytics using Azure Data Studio.

## Prerequisites
To complete this quickstart, you need Azure Data Studio, and a dedicated SQL pool in Azure Synapse Analytics.

- [Install Azure Data Studio](./download-azure-data-studio.md).

If you don't already have a dedicated SQL pool, see [Create a dedicated SQL pool](/azure/sql-data-warehouse/sql-data-warehouse-get-started-provision).

Remember the server name, and login credentials!


## Connect to your dedicated SQL pool

Use Azure Data Studio to establish a connection to your Azure Synapse Analytics server.

1. The first time you run Azure Data Studio the **Connection** page should open. If you don't see the **Connection** page, select **Add Connection**, or the **New Connection** icon in the **SERVERS** sidebar:
   
   ![Screenshot of the Connection page with the New Connection icon called out.](media/quickstart-sql-dw/new-connection-icon.png)

2. This article uses *SQL Login*, but *Windows Authentication* is also supported. Fill in the fields as follows using the server name, user name, and password for *your* Azure SQL server:

   |   Setting    | Suggested value | Description |
   |--------------|-----------------|-------------| 
   | **Server name** | The fully qualified server name | For example the name should look like to this: **sqlpoolservername.database.windows.net**. |
   | **Authentication** | SQL Login| SQL Authentication is used in this tutorial. |
   | **User name** | The server admin account | This is the account that you specified when you created the server. |
   | **Password (SQL Login)** | The password for your server admin account | This is the password that you specified when you created the server. |
   | **Save Password?** | Yes or No | Select Yes if you don't want to enter the password each time. |
   | **Database name** | *leave blank* | The name of the database to which to connect. |
   | **Server Group** | Select <Default> | If you created a server group, you can set to a specific server group. | 

3. If your server doesn't have a firewall rule allowing Azure Data Studio to connect, the **Create new firewall rule** form opens. Complete the form to create a new firewall rule. For details, see [Firewall rules](/azure/sql-database/sql-database-firewall-configure).

4. After successfully connecting your server opens in the *Servers* sidebar.

## Create a database in your dedicated SQL pool

1. Right-click on your server, in the object explorer and select **New Query.**

2. Paste the following snippet into the query editor and select **Run**:

   ```sql
    IF NOT EXISTS (
       SELECT name
       FROM sys.databases
       WHERE name = N'TutorialDB'
    )
    CREATE DATABASE [TutorialDB] (EDITION = 'datawarehouse', SERVICE_OBJECTIVE='DW100');
    GO  
    
    ALTER DATABASE [TutorialDB] SET QUERY_STORE=ON
    GO
   ```

## Create a table

The query editor is still connected to the *master* database, but we want to create a table in the *TutorialDB* database. 

1. Change the connection context to **TutorialDB**:

2. Paste the following snippet into the query editor and select **Run**:

   > [!NOTE]
   > You can append this to, or overwrite the previous query in the editor. Note that selecting **Run** executes only the query that is selected. If nothing is selected, selecting **Run** executes all queries in the editor.

   ```sql
   -- Create a new table called 'Customers' in schema 'dbo'
   -- Drop the table if it already exists
   IF OBJECT_ID('dbo.Customers', 'U') IS NOT NULL
   DROP TABLE dbo.Customers
   GO
   -- Create the table in the specified schema
   CREATE TABLE dbo.Customers
   (
      CustomerId        INT     NOT NULL,
      Name      [NVARCHAR](50)  NOT NULL,
      Location  [NVARCHAR](50)  NOT NULL,
      Email     [NVARCHAR](50)  NOT NULL
   );
   GO
   ```

    :::image type="content" source="media/quickstart-sql-dw/create-table.png" alt-text="Create a table in the TutorialDB database":::


## Insert rows

1. Paste the following snippet into the query editor and select **Run**:

   ```sql
   -- Insert rows into table 'Customers'
   INSERT INTO dbo.Customers
      ([CustomerId],[Name],[Location],[Email])
      SELECT 1, N'Orlando',N'Australia', N'' UNION ALL
      SELECT 2, N'Keith', N'India', N'keith0@adventure-works.com' UNION ALL
      SELECT 3, N'Donna', N'Germany', N'donna0@adventure-works.com' UNION ALL
      SELECT 4, N'Janet', N'United States', N'janet1@adventure-works.com'
   ```

    :::image type="content" source="media/quickstart-sql-dw/create-rows.png" alt-text="Create rows in the table":::

## View the result

1. Paste the following snippet into the query editor and select **Run**:

   ```sql
   -- Select rows from table 'Customers'
   SELECT * FROM dbo.Customers;
   ```

2. The results of the query are displayed:

    :::image type="content" source="media/quickstart-sql-dw/view-results.png" alt-text="View the results":::


## Clean up resources

If you don't plan to continue working with the sample databases created in this article, then [delete the resource group](/azure/synapse-analytics/sql-data-warehouse/create-data-warehouse-portal#clean-up-resources).

## Next steps
For more information, visit [Connecting to Synapse SQL with Azure Data Studio](/azure/synapse-analytics/sql/get-started-azure-data-studio).

Now that you've successfully connected to an Azure Synapse Analytics and ran a query, try out the [Code editor tutorial](tutorial-sql-editor.md).