---
title: Create and query a SQL Server database
description: Connect to a SQL Server instance by using SQL Server Management Studio and running basic T-SQL queries.
ms.prod: sql
ms.technology: ssms
ms.topic: quickstart
author: markingmyname
ms.author: maghan
ms.reviewer: sstein
ms.custom: ""
ms.date: 12/14/2020
---

# Quickstart: Create and query a SQL Server database by using SQL Server Management Studio (SSMS)

[!INCLUDE[sqlserver](../../includes/applies-to-version/sqlserver.md)]

This quickstart teaches you how to use SQL Server Management Studio (SSMS) to connect to your SQL Server instance and run some basic Transact-SQL (T-SQL) commands.

The article demonstrates how to follow the below steps:

> [!div class="checklist"]
> - Create a database ("TutorialDB")
> - Create a table ("Customers") in your new database
> - Insert rows into your new table
> - Query the new table and view the results
> - Use the query window table to verify your connection properties
> - Change the server that your query window is connected to

## Prerequisites

- [Quickstart: Connect to SQL Server using SQL Server Management Studio (SSMS)](connect-ssms-sql-server.md)

## Create a database

Create a database named TutorialDB by following the below steps:

1. Right-click your server instance in Object Explorer, and then select **New Query**:

   :::image type="content" source="media/query-ssms-sql-server/new-query.png" alt-text="The New Query link":::

2. Paste the following T-SQL code snippet into the query window:

    ```sql
    USE master
    GO
    IF NOT EXISTS (
       SELECT name
       FROM sys.databases
       WHERE name = N'TutorialDB'
    )
    CREATE DATABASE [TutorialDB]
    GO
   ```

3. Execute the query by selecting **Execute** or selecting F5 on your keyboard.

   :::image type="content" source="media/query-ssms-sql-server/execute.png" alt-text="The Execute command":::
  
    After the query is complete, the new TutorialDB database appears in the list of databases in Object Explorer. If it isn't displayed, right-click the **Databases** node, and then select **Refresh**.

## Create a table in the new database

In this section, you create a table in the newly created TutorialDB database. Because the query editor is still in the context of the *master* database, switch the connection context to the *TutorialDB* database by doing the following steps:

1. In the database drop-down list, select the database that you want, as shown here:

   :::image type="content" source="media/query-ssms-sql-server/change-db.png" alt-text="Change database":::

2. Paste the following T-SQL code snippet into the query window:

    ```sql
    USE [TutorialDB]
    -- Create a new table called 'Customers' in schema 'dbo'
    -- Drop the table if it already exists
    IF OBJECT_ID('dbo.Customers', 'U') IS NOT NULL
    DROP TABLE dbo.Customers
    GO
    -- Create the table in the specified schema
    CREATE TABLE dbo.Customers
    (
       CustomerId        INT    NOT NULL   PRIMARY KEY, -- primary key column
       Name      [NVARCHAR](50)  NOT NULL,
       Location  [NVARCHAR](50)  NOT NULL,
       Email     [NVARCHAR](50)  NOT NULL
    );
    GO
    ```

3. Execute the query by selecting **Execute** or selecting F5 on your keyboard.

After the query is complete, the new Customers table is displayed in the list of tables in Object Explorer. If the table isn't displayed, right-click the **TutorialDB** > **Tables** node in Object Explorer, and then select **Refresh**.

   :::image type="content" source="media/query-ssms-sql-server/new-table.png" alt-text="New table":::

## Insert rows into the new table

Now let's insert some rows into the Customers table that you created. Paste the following T-SQL code snippet into the query window, and then select **Execute**:

   ```sql
   -- Insert rows into table 'Customers'
   INSERT INTO dbo.Customers
      ([CustomerId],[Name],[Location],[Email])
   VALUES
      ( 1, N'Orlando', N'Australia', N''),
      ( 2, N'Keith', N'India', N'keith0@adventure-works.com'),
      ( 3, N'Donna', N'Germany', N'donna0@adventure-works.com'),
      ( 4, N'Janet', N'United States', N'janet1@adventure-works.com')
   GO
   ```

## Query the table and view the results

The results of a query are visible below the query text window. To query the Customers table and view the rows that were inserted, follow the steps below:

1. Paste the following T-SQL code snippet into the query window, and then select **Execute**:

   ```sql
   -- Select rows from table 'Customers'
   SELECT * FROM dbo.Customers;
   ```

    The results of the query are displayed under the area where the text was entered.

   :::image type="content" source="media/query-ssms-sql-server/query-results.png" alt-text="The Results list":::

    You can also modify the way results are presented by selecting one of the following options:

   ![Three options for displaying query results](media/query-ssms-sql-server/results.png)

   - The first button displays the results in **Text View**, as shown in the image in the next section.
   - The middle button displays the results in **Grid View**, which is the default option.
       - This is set as default
   - The third button lets you save the results to a file whose extension is .rpt by default.

## Verify your connection properties by using the query window table

You can find information about the connection properties under the results of your query. After you run the previously mentioned query in the preceding step, review the connection properties at the bottom of the query window.

- You can determine which server and database you're connected to, and the username that you use.
- You can also view the query duration and the number of rows that are returned by the previously executed query.

   :::image type="content" source="media/query-ssms-sql-server/connection-properties.png" alt-text="Connection properties":::

## Additional tools

You can also use [Azure Data Studio](../../azure-data-studio/download-azure-data-studio.md) to connect and query [SQL Server](../../azure-data-studio/quickstart-sql-server.md), an [Azure SQL Database](../../azure-data-studio/quickstart-sql-database.md), and [Azure Synapse Analyticss](../../azure-data-studio/quickstart-sql-dw.md).

## Next steps

The best way to get acquainted with SSMS is through hands-on practice. These articles help you with various features available within SSMS. These articles teach you how to manage the components of SSMS and how to find the features that you use regularly.

- [SQL Server Management Studio (SSMS) Query Editor](../f1-help/database-engine-query-editor-sql-server-management-studio.md)
- [Scripting](../tutorials/scripting-ssms.md)
- [Using Templates in SSMS](../template/templates-ssms.md)
- [SSMS Configuration](../tutorials/ssms-configuration.md)
- [Additional Tips and Tricks for using SSMS](../tutorials/ssms-tricks.md)