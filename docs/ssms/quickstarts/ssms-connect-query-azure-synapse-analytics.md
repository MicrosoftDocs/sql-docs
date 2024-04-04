---
title: Connect and query a dedicated SQL pool in Azure Synapse Analytics
description: Connect to and query a dedicated SQL pool in Azure Synapse Analytics using SQL Server Management Studio (SSMS).
author: markingmyname
ms.author: maghan
ms.reviewer: mikeray, randolphwest
ms.date: 02/29/2024
ms.service: sql
ms.subservice: ssms
ms.topic: quickstart
ms.custom:
  - intro-quickstart
# CustomerIntent: As a user, I want to connect to and query a dedicated SQL pool in Azure Synapse Analytics using SSMS.
---
# Quickstart: Connect and query a dedicated SQL pool (formerly SQL DW) in Azure Synapse Analytics with SQL Server Management Studio (SSMS)

[!INCLUDE [asa](../../includes/applies-to-version/asa.md)]

In this quickstart, you can get started using SQL Server Management Studio (SSMS) to connect to your dedicated SQL pool (formerly SQL DW) in Azure Synapse Analytics and run some Transact-SQL (T-SQL) commands.

> [!div class="checklist"]
> - Connect to a dedicated SQL pool (formerly SQL DW) in Azure Synapse Analytics
> - Create a table in your new database
> - Insert rows into your new table
> - Query the new table and view the results
> - Use the query window table to verify your connection properties

## Prerequisites

To complete this article, you need SQL Server Management Studio (SSMS) and access to a data source.

- Install [SQL Server Management Studio](../download-sql-server-management-studio-ssms.md)
- [Azure Synapse Analytics](https://azure.microsoft.com/services/synapse-analytics/)

## Connect to a dedicated SQL pool (formerly SQL DW) in Azure Synapse Analytics

[!INCLUDE [ssms-connect-azure-ad](../../includes/ssms-connect-azure-ad.md)]

1. Start SQL Server Management Studio. The first time you run SSMS, the **Connect to Server** window opens. If it doesn't open, you can open it manually by selecting **Object Explorer** > **Connect** > **Database Engine**.

   :::image type="content" source="media/ssms-connect-query-azure-synapse-analytics/connect-object-explorer.png" alt-text="Screenshot of the connect link in Object Explorer.":::

1. In the **Connect to Server** window, use the following list for guidance:

   | Setting | Suggested values | Description |
   | --- | --- | --- |
   | **Server type** | Database engine | For **Server type**, select **Database Engine** (usually the default option). |
   | **Server name** | The fully qualified server name | For **Server name**, enter the name of your dedicated SQL pool (formerly SQL DW) server name. |
   | **Authentication** | SQL Server Authentication | Use **SQL Server Authentication** to connect to a dedicated SQL pool (formerly SQL DW).<br /><br />The **Windows Authentication** method isn't supported for Azure SQL. For more information, see [Azure SQL authentication](/azure/sql-database/sql-database-security-overview#access-management). |
   | **Login** | Server account user ID | The user ID from the server account used to create the server. |
   | **Password** | Server account password | The password from the server account used to create the server. |
   | **Encryption** <sup>1</sup> | Encryption method | Select the encryption level for the connection. The default value is *Mandatory*. |
   | **Trust server certificate** | Trust Server Certificate | Check this option to bypass server certificate validation. The default value is *False* (unchecked), which promotes better security using trusted certificates. |
   | **Host Name in Certificate** | Host name of the server | The value provided in this option is used to specify a different, but expected, CN or SAN in the server certificate. |

   <sup>1</sup> [!INCLUDE [ssms-encryption](../includes/ssms-encryption.md)]

   :::image type="content" source="media/ssms-connect-query-azure-synapse-analytics/connect-to-azure-synapse-analytics-object-explorer-ssms20.png" alt-text="Screenshot of connection dialog for Azure Synapse Analytics.":::

1. After you complete all the fields, select **Connect**.

   You can also modify other connection options by selecting **Options**. Examples of connection options are the database you're connecting to, the connection timeout value, and the network protocol. This article uses the default values for all the options.

   If your firewall isn't set up, a prompt appears to configure the firewall. Once you sign in, fill in your Azure account sign in information and continue to set the firewall rule. Then select **OK**. This prompt is a one time action. Once you configure the firewall, the firewall prompt shouldn't appear.

   :::image type="content" source="media/ssms-connect-query-azure-sql/azure-sql-firewall-sign-in-3.png" alt-text="Screenshot of Azure SQL New Firewall Rule." lightbox="media/ssms-connect-query-azure-sql/azure-sql-firewall-sign-in-3.png":::
   :::image type="content" source="media/ssms-connect-query-azure-sql/azure-sql-firewall-sign-in-3.png" alt-text="Screenshot of Azure SQL New Firewall Rule." lightbox="media/ssms-connect-query-azure-sql/azure-sql-firewall-sign-in-3.png":::

1. To verify that your dedicated SQL pool (formerly SQL DW) connection succeeded, expand and explore the objects within **Object Explorer** where the server name, the SQL Server version, and the username are displayed. These objects are different depending on the server type.

   :::image type="content" source="media/ssms-connect-query-azure-synapse-analytics/connect-azure-synapse-analytics.png" alt-text="Screenshot of Connecting to an Azure Synapse Analytics database.":::
   :::image type="content" source="media/ssms-connect-query-azure-synapse-analytics/connect-azure-synapse-analytics.png" alt-text="Screenshot of Connecting to an Azure Synapse Analytics database.":::

## Troubleshoot connectivity issues

You can experience connection problems with dedicated SQL pool (formerly SQL DW). For more information on troubleshooting connection problems, visit [Troubleshooting connectivity issues](/azure/azure-sql/database/troubleshoot-common-errors-issues).

## Create a table

In this section, you create a table in your dedicated SQL pool (formerly SQL DW).

1. In Object Explorer, right-click on your dedicated SQL pool (formerly SQL DW), select **New query**.

1. Paste the following T-SQL code snippet into the query window:

   ```sql
   -- Create a new table called 'Customers' in schema 'dbo'
   -- Drop the table if it already exists
   IF OBJECT_ID('dbo.Customers', 'U') IS NOT NULL
       DROP TABLE dbo.Customers
   GO

   -- Create the table in the specified schema
   CREATE TABLE dbo.Customers (
       CustomerId INT NOT NULL,
       Name NVARCHAR(50) NOT NULL,
       Location NVARCHAR(50) NOT NULL,
       Email NVARCHAR(50) NOT NULL
   );
   GO
   ```

1. Execute the query by selecting **Execute** or selecting F5 on your keyboard.

After the query is complete, the new Customers table is displayed in the list of tables in Object Explorer. If the table isn't displayed, right-click the dedicated SQL pool (formerly SQL DW) **Tables** node in Object Explorer, and then select **Refresh**.

:::image type="content" source="media/ssms-connect-query-azure-synapse-analytics/new-table.png" alt-text="Screenshot of New table.":::

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

The results of a query are visible beneath the query text window. To query the `Customers` table and view the rows that were inserted, paste the following T-SQL code snippet into the query window, and then select **Execute**:

```sql
-- Select rows from table 'Customers'
SELECT * FROM dbo.Customers;
```

The query results are displayed under the area where the text was entered.

:::image type="content" source="media/ssms-connect-query-azure-synapse-analytics/query-results.png" alt-text="Screenshot of the results list.":::

You can also modify the way results are presented by selecting one of the following options:

:::image type="content" source="media/ssms-connect-query-azure-synapse-analytics/results.png" alt-text="Screenshot of three options for displaying query results.":::

- The first button displays the results in **Text View**, as shown in the image in the next section.
- The middle button displays the results in **Grid View**, which is the default option.
- The third button lets you save the results to a file whose extension is `.rpt` by default.

## Verify your connection properties by using the query window table

You can find information about the connection properties under the results of your query. After you run the previously mentioned query in the preceding step, review the connection properties at the bottom of the query window.

- You can determine which server and database you're connected to, and your username.
- You can also view the query duration and the number of rows returned by the previously executed query.

  :::image type="content" source="media/ssms-connect-query-azure-synapse-analytics/connection-properties.png" alt-text="Screenshot of the connection properties." lightbox="media/ssms-connect-query-azure-synapse-analytics/connection-properties.png":::

## Additional tools

You can also use [Azure Data Studio](../../azure-data-studio/download-azure-data-studio.md) to connect and query [SQL Server](/azure-data-studio/quickstart-sql-server), an [Azure SQL Database](/azure-data-studio/quickstart-sql-database), and [Azure Synapse Analytics](/azure-data-studio/quickstart-sql-dw).

## Related content

- [SQL Server Management Studio (SSMS) Query Editor](../f1-help/database-engine-query-editor-sql-server-management-studio.md)
- [Script objects in SQL Server Management Studio](../tutorials/scripting-ssms.md)
- [Use templates in SQL Server Management Studio](../template/templates-ssms.md)
- [SQL Server Management Studio components and configuration](../tutorials/ssms-configuration.md)
- [Tips and tricks for using SQL Server Management Studio (SSMS)](../tutorials/ssms-tricks.md)
