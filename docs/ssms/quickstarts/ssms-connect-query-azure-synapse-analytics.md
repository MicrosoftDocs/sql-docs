---
title: Connect and query a dedicated SQL pool (formerly SQL DW) in Azure Synapse Analytics with SQL Server Management Studio (SSMS)
description: Connect to a dedicated SQL pool (formerly SQL DW) in Azure Synapse Analytics using SSMS. Create and query a dedicated SQL pool (formerly SQL DW) in Azure Synapse Analytics by running basic T-SQL queries in SSMS.
ms.service: sql
ms.subservice: ssms
ms.topic: quickstart
author: markingmyname
ms.author: maghan
ms.reviewer: mikeray
ms.custom:
  - contperf-fy21q2
  - intro-quickstart
ms.date: 12/15/2020
---

# Quickstart: Connect and query a dedicated SQL pool (formerly SQL DW) in Azure Synapse Analytics with SQL Server Management Studio (SSMS)

[!INCLUDE [asa](../../includes/applies-to-version/asa.md)]

Get started using SQL Server Management Studio (SSMS) to connect to your dedicated SQL pool (formerly SQL DW) in Azure Synapse Analytics and run some Transact-SQL (T-SQL) commands.

> [!div class="checklist"]
> - Connect to a dedicated SQL pool (formerly SQL DW) in Azure Synapse Analytics
> - Create a table in your new database
> - Insert rows into your new table
> - Query the new table and view the results
> - Use the query window table to verify your connection properties

## Prerequisites

To complete this article, you need SQL Server Management Studio and access to a data source.

- Install [SQL Server Management Studio](../download-sql-server-management-studio-ssms.md).
- [Azure Synapse Analytics](https://azure.microsoft.com/services/synapse-analytics/?&ef_id=CjwKCAiA17P9BRB2EiwAMvwNyDW39uBCUDMPmL693_KrmcNAV2uiMHZDeNJ-615AoxdIPBNqXwBDMhoCkL8QAvD_BwE:G:s&OCID=AID2100131_SEM_CjwKCAiA17P9BRB2EiwAMvwNyDW39uBCUDMPmL693_KrmcNAV2uiMHZDeNJ-615AoxdIPBNqXwBDMhoCkL8QAvD_BwE:G:s&gclid=CjwKCAiA17P9BRB2EiwAMvwNyDW39uBCUDMPmL693_KrmcNAV2uiMHZDeNJ-615AoxdIPBNqXwBDMhoCkL8QAvD_BwE)

## Connect to a dedicated SQL pool (formerly SQL DW) in Azure Synapse Analytics

[!INCLUDE[ssms-connect-azure-ad](../../includes/ssms-connect-azure-ad.md)]

1. Start SQL Server Management Studio. The first time you run SSMS, the **Connect to Server** window opens. If it doesn't open, you can open it manually by selecting **Object Explorer** > **Connect** > **Database Engine**.

    :::image type="content" source="media/ssms-connect-query-azure-synapse-analytics/connect-object-explorer.png" alt-text="Connect link in Object Explorer":::

2. In the **Connect to Server** window, follow the list below:

    |   Setting   |   Suggested Value(s)   |   Description   |
    |-------------|------------------------|-----------------|
    | **Server type** | Database engine | For **Server type**, select **Database Engine** (usually the default option). |
    | **Server name** | The fully qualified server name | For **Server name**, enter the name of your dedicated SQL pool (formerly SQL DW) server name. |
    | **Authentication** | SQL Server Authentication | Use **SQL Server Authentication** for to connect to dedicated SQL pool (formerly SQL DW). </br> </br> The **Windows Authentication** method isn't supported for Azure SQL. For more information, see [Azure SQL authentication](/azure/sql-database/sql-database-security-overview#access-management). |
    | **Login** | Server account user ID | The user ID from the server account used to create the server. |
    | **Password** | Server account password | The password from the server account used to create the server. |

    :::image type="content" source="media/ssms-connect-query-azure-synapse-analytics/connect-to-azure-synapse-analytics-object-explorer.png" alt-text="Server name field for Azure Synapse Analytics":::

3. After you've completed all the fields, select **Connect**.

    You can also modify other connection options by selecting **Options**. Examples of connection options are the database you're connecting to, the connection timeout value, and the network protocol. This article uses the default values for all the options.

    If you haven't set up your firewall settings, a prompt appears to configure the firewall. Once you sign in, fill in your Azure account login information and continue to set the firewall rule. Then select **OK**. This prompt is a one time action. Once you configure the firewall, the firewall prompt shouldn't appear.

    :::image type="content" source="media/ssms-connect-query-azure-sql/azure-sql-firewall-sign-in-3.png" alt-text="Azure SQL New Firewall Rule":::

4. To verify that your dedicated SQL pool (formerly SQL DW) connection succeeded, expand and explore the objects within **Object Explorer** where the server name, the SQL Server version, and the username are displayed. These objects are different depending on the server type.

    :::image type="content" source="media/ssms-connect-query-azure-synapse-analytics/connect-azure-synapse-analytics.png" alt-text="Connecting to an Azure Synapse Analytics database":::

## Troubleshoot connectivity issues

You can experience connection problems with dedicated SQL pool (formerly SQL DW). For more information on troubleshooting connection problems, visit [Troubleshooting connectivity issues](/azure/azure-sql/database/troubleshoot-common-errors-issues).

## Create a table

In this section, you create a table in your dedicated SQL pool (formerly SQL DW).

1. In Object Explorer, right click on your dedicated SQL pool (formerly SQL DW), select **New query**. 

2. Paste the following T-SQL code snippet into the query window:

    ```sql
    -- Create a new table called 'Customers' in schema 'dbo'
    -- Drop the table if it already exists
    IF OBJECT_ID('dbo.Customers', 'U') IS NOT NULL
    DROP TABLE dbo.Customers
    GO
    -- Create the table in the specified schema
    CREATE TABLE dbo.Customers
    (
       CustomerId        INT    NOT NULL,
       Name      [NVARCHAR](50)  NOT NULL,
       Location  [NVARCHAR](50)  NOT NULL,
       Email     [NVARCHAR](50)  NOT NULL
    );
    GO
    ```

3. Execute the query by selecting **Execute** or selecting F5 on your keyboard.

After the query is complete, the new Customers table is displayed in the list of tables in Object Explorer. If the table isn't displayed, right-click the dedicated SQL pool (formerly SQL DW) **Tables** node in Object Explorer, and then select **Refresh**.

   :::image type="content" source="media/ssms-connect-query-azure-synapse-analytics/new-table.png" alt-text="New table":::

## Insert rows into the new table

Now let's insert some rows into the Customers table that you created. Paste the following T-SQL code snippet into the query window, and then select **Execute**:

   ```sql
   -- Insert rows into table 'Customers'
   INSERT INTO dbo.Customers VALUES ( 1, N'Orlando', N'Australia', N'');
   INSERT INTO dbo.Customers VALUES ( 2, N'Keith', N'India', N'keith0@adventure-works.com');
   INSERT INTO dbo.Customers VALUES (3, N'Donna', N'Germany', N'donna0@adventure-works.com');   
   INSERT INTO dbo.Customers VALUES (4, N'Janet', N'United States', N'janet1@adventure-works.com');
   ```

## Query the table and view the results

The results of a query are visible below the query text window. To query the Customers table and view the rows that were inserted, follow the steps below:

1. Paste the following T-SQL code snippet into the query window, and then select **Execute**:

   ```sql
   -- Select rows from table 'Customers'
   SELECT * FROM dbo.Customers;
   ```

    The results of the query are displayed under the area where the text was entered.

   :::image type="content" source="media/ssms-connect-query-azure-synapse-analytics/query-results.png" alt-text="The Results list":::

    You can also modify the way results are presented by selecting one of the following options:

   ![Three options for displaying query results](media/ssms-connect-query-azure-synapse-analytics/results.png)

   - The first button displays the results in **Text View**, as shown in the image in the next section.
   - The middle button displays the results in **Grid View**, which is the default option.
       - This is set as default
   - The third button lets you save the results to a file whose extension is .rpt by default.

## Verify your connection properties by using the query window table

You can find information about the connection properties under the results of your query. After you run the previously mentioned query in the preceding step, review the connection properties at the bottom of the query window.

- You can determine which server and database you're connected to, and the username that you use.
- You can also view the query duration and the number of rows that are returned by the previously executed query.

   :::image type="content" source="media/ssms-connect-query-azure-synapse-analytics/connection-properties.png" alt-text="Connection properties":::

## Additional tools

You can also use [Azure Data Studio](../../azure-data-studio/download-azure-data-studio.md) to connect and query [SQL Server](../../azure-data-studio/quickstart-sql-server.md), an [Azure SQL Database](../../azure-data-studio/quickstart-sql-database.md), and [Azure Synapse Analytics](../../azure-data-studio/quickstart-sql-dw.md).

## Next steps

The best way to get acquainted with SSMS is through hands-on practice. These articles help you with various features available within SSMS.

- [SQL Server Management Studio (SSMS) Query Editor](../f1-help/database-engine-query-editor-sql-server-management-studio.md)
- [Scripting](../tutorials/scripting-ssms.md)
- [Using Templates in SSMS](../template/templates-ssms.md)
- [SSMS Configuration](../tutorials/ssms-configuration.md)
- [Additional Tips and Tricks for using SSMS](../tutorials/ssms-tricks.md)
