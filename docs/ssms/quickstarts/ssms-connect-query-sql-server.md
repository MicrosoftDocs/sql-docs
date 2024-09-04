---
title: Connect and query SQL Server using SSMS
description: Connect to a SQL Server instance in SSMS. Create and query a SQL Server database in SSMS running basic Transact-SQL (T-SQL) queries.
author: erinstellato-ms
ms.author: maghan
ms.reviewer: mikeray, randolphwest
ms.date: 02/29/2024
ms.service: sql
ms.subservice: ssms
ms.topic: quickstart
ms.custom:
  - UpdateFrequency5
---

# Quickstart: Connect and query a SQL Server instance using SQL Server Management Studio (SSMS)

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

Get started using SQL Server Management Studio (SSMS) to connect to your SQL Server instance and run some Transact-SQL (T-SQL) commands.

[!INCLUDE [entra-id](../../includes/entra-id-hard-coded.md)]

The article demonstrates how to follow the below steps:

> [!div class="checklist"]
> - Connect to a SQL Server instance
> - Create a database
> - Create a table in your new database
> - Insert rows into your new table
> - Query the new table and view the results
> - Use the query window table to verify your connection properties

This article covers connecting and querying an instance of SQL Server. For Azure SQL, see [Connect and query Azure SQL Database & SQL Managed Instance](/azure/azure-sql/database/connect-query-ssms).

To use [Azure Data Studio](/azure-data-studio/download-azure-data-studio), see connect and query [SQL Server](/azure-data-studio/quickstart-sql-server), [Azure SQL Database](/azure-data-studio/quickstart-sql-database), and [Azure Synapse Analytics](/azure-data-studio/quickstart-sql-dw).

To learn more about SQL Server Management Studio, see [Tips and tricks for using SQL Server Management Studio (SSMS)](../tutorials/ssms-tricks.md).

## Prerequisites

To complete this quickstart, you need the following prerequisites:

- Install [Download SQL Server Management Studio (SSMS)](../download-sql-server-management-studio-ssms.md).
- [Install SQL Server from the Installation Wizard (Setup)](../../database-engine/install-windows/install-sql-server-from-the-installation-wizard-setup.md) and configure a [SQL Server instance](https://www.microsoft.com/en-ca/sql-server/sql-server-downloads).

## Connect to a SQL Server instance

To connect to your SQL Server instance, follow these steps:

1. Start SQL Server Management Studio. The first time you run SSMS, the **Connect to Server** window opens. If it doesn't open, you can open it manually by selecting **Object Explorer** > **Connect** > **Database Engine**.

    :::image type="content" source="media/ssms-connect-query-sql-server/connect-object-explorer.png" alt-text="Screenshot of the connect link in Object Explorer.":::

1. The **Connect to Server** dialog box appears. Enter the following information:

   | Setting | Suggested values | Description |
   | --- | --- | --- |
   | **Server type** | Database Engine | For **Server type**, select **Database Engine** (usually the default option). |
   | **Server name** | The fully qualified server name | For **Server name**, enter the name of your SQL Server (you can also use *localhost* as the server name if you're connecting locally). If you're NOT using the default instance - ***MSSQLSERVER*** - you must enter in the server name and the instance name.<br /><br />If you're unsure how to determine your SQL Server instance name, see [Additional tips and tricks for using SSMS](../tutorials/ssms-tricks.md#find-sql-server-instance-name). |
   | **Authentication** | Windows Authentication<br /><br />SQL Server Authentication<br /><br />Microsoft Entra authentication | Windows Authentication is set as default.<br />You can also use **SQL Server Authentication** to connect. However, if you select **SQL Server Authentication**, a username and password are required.<br />**Microsoft Entra authentication** is available for [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions. For step-by-step configuration instructions, see [Tutorial: Set up Microsoft Entra authentication for SQL Server](../../relational-databases/security/authentication-access/azure-ad-authentication-sql-server-setup-tutorial.md)<br />For more information about authentication types, see [Connect to the server (database engine)](../f1-help/connect-to-server-database-engine.md). |
   | **Login** | Server account user ID | The user ID from the server account used to sign in to the server. A login is required when using **SQL Server Authentication**. |
   | **Password** | Server account password | The password from the server account used to sign in to the server. A password is required when using **SQL Server Authentication**. |
   | **Encryption** <sup>1</sup> | Encryption method | Select the encryption level for the connection. The default value is *Mandatory*. |
   | **Trust server certificate** | Trust Server Certificate | Check this option to bypass server certificate validation. The default value is *False* (unchecked), which promotes better security using trusted certificates. |
   | **Host Name in Certificate** | Host name of the server | The value provided in this option is used to specify a different, but expected, CN or SAN in the server certificate. |

   <sup>1</sup> [!INCLUDE [ssms-encryption](../includes/ssms-encryption.md)]

   :::image type="content" source="media/ssms-connect-query-sql-server/connect-to-sql-server-object-explorer-ssms20.png" alt-text="Screenshot of connection dialog for SQL Server.":::

1. After you complete all the fields, select **Connect**.

   You can also modify extra connection options by selecting **Options**. Examples of connection options are the database you're connecting to, the connection timeout value, and the network protocol. This article uses the default values for all the fields.

1. To verify that your SQL Server connection succeeded, expand and explore the objects within **Object Explorer** where the server name, the SQL Server version, and the username are displayed. These objects are different depending on the server type.

   :::image type="content" source="media/ssms-connect-query-sql-server/connect-on-prem.png" alt-text="Screenshot of connecting to an on-premises server.":::

## Create a database

Now let's create a database named TutorialDB by following the below steps:

1. Right-click your server instance in Object Explorer, and then select **New Query**:

   :::image type="content" source="media/ssms-connect-query-sql-server/new-query.png" alt-text="Screenshot of the new query link.":::

1. Paste the following T-SQL code snippet into the query window:

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

1. Execute the query by selecting **Execute** or selecting F5 on your keyboard.

   :::image type="content" source="media/ssms-connect-query-sql-server/execute.png" alt-text="Screenshot of the Execute command.":::

   After the query is complete, the new TutorialDB database appears in the list of databases in Object Explorer. If it isn't displayed, right-click the **Databases** node, and then select **Refresh**.

## Create a table

In this section, you create a table in the newly created TutorialDB database. Because the query editor is still in the context of the `master` database, switch the connection context to the *TutorialDB* database by doing the following steps:

1. In the database dropdown list, select the database that you want, as shown here:

   :::image type="content" source="media/ssms-connect-query-sql-server/change-db.png" alt-text="Screenshot of change database.":::

1. Paste the following T-SQL code snippet into the query window:

   ```sql
   USE [TutorialDB]

   -- Create a new table called 'Customers' in schema 'dbo'
   -- Drop the table if it already exists
   IF OBJECT_ID('dbo.Customers', 'U') IS NOT NULL
       DROP TABLE dbo.Customers
   GO

   -- Create the table in the specified schema
   CREATE TABLE dbo.Customers (
       CustomerId INT NOT NULL PRIMARY KEY, -- primary key column
       Name NVARCHAR(50) NOT NULL,
       Location NVARCHAR(50) NOT NULL,
       Email NVARCHAR(50) NOT NULL
   );
   GO
   ```

1. Execute the query by selecting **Execute** or selecting F5 on your keyboard.

After the query is complete, the new Customers table is displayed in the list of tables in Object Explorer. If the table isn't displayed, right-click the **TutorialDB** > **Tables** node in Object Explorer, and then select **Refresh**.

:::image type="content" source="media/ssms-connect-query-sql-server/new-table.png" alt-text="Screenshot of new table.":::

## Insert rows

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

The results of a query are visible below the query text window. To query the Customers table and view the rows that were inserted, paste the following T-SQL code snippet into the query window, and then select **Execute**:

```sql
-- Select rows from table 'Customers'
SELECT * FROM dbo.Customers;
```

The query results are displayed under the area where the text was entered.

:::image type="content" source="media/ssms-connect-query-sql-server/query-results.png" alt-text="Screenshot of the results list.":::

You can also modify the way results are presented by selecting one of the following options:

:::image type="content" source="media/ssms-connect-query-sql-server/results.png" alt-text="Screenshot of three options for displaying query results.":::

- The first button displays the results in **Text View**, as shown in the image in the next section.
- The middle button displays the results in **Grid View**, which is the default option.
- The third button lets you save the results to a file whose extension is .rpt by default.

## Troubleshoot connectivity issues

To review troubleshooting techniques to use when you can't connect to an instance of your SQL Server Database Engine on a single server, visit [Troubleshoot connecting to the SQL Server Database Engine](/troubleshoot/sql/connect/network-related-or-instance-specific-error-occurred-while-establishing-connection).

## Related content

- [SQL Server Management Studio (SSMS) Query Editor](../f1-help/database-engine-query-editor-sql-server-management-studio.md)
- [Script objects in SQL Server Management Studio](../tutorials/scripting-ssms.md)
- [Use templates in SQL Server Management Studio](../template/templates-ssms.md)
- [SQL Server Management Studio components and configuration](../tutorials/ssms-configuration.md)
- [Tips and tricks for using SQL Server Management Studio (SSMS)](../tutorials/ssms-tricks.md)
