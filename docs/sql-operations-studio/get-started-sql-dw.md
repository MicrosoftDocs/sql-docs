---
title: Connect and query an Azure SQL Data Warehouse using SQL Operations Studio | Microsoft Docs
description: Use SQL Operations Studio to connect to a SQL database and run a query
keywords:
ms.custom: "tools|sos"
ms.date: "11/01/2017"
ms.prod: "sql-non-specified"
ms.reviewer: "alayu; erickang; sanagama; sstein"
ms.suite: "sql"
ms.tgt_pltfrm: ""
ms.topic: "quickstart"
author: "yualan"
ms.author: "alayu"
manager: craigg
ms.workload: "Inactive"
---
# Azure SQL Data Warehouse: Use [!INCLUDE[name-sos](../includes/name-sos-short.md)] to connect and query data

This quickstart demonstrates how to use [!INCLUDE[name-sos](../includes/name-sos-short.md)] to connect to Azure SQL Data Warehouse, and then use Transact-SQL statements to create, insert, and select data in the database. 

## Prerequisites
To use this tutorial, you need:

* An existing SQL data warehouse. To create one, see [Create a SQL Data Warehouse](https://docs.microsoft.com/en-us/azure/sql-data-warehouse/sql-data-warehouse-get-started-provision).
* Install [!INCLUDE[name-sos](../includes/name-sos-short.md)] by following [these directions](download.md).
* The fully qualified SQL server name. To find this, see [Connect to SQL Data Warehouse](https://docs.microsoft com/en-us/azure/sql-data-warehouse/sql-data-warehouse-connect-overview).

## SQL server connection information

Get the connection information needed to connect to the Azure SQL Data Warehouse. You will need the fully qualified server name, database name, and login information in the next procedures.

1. Log in to the [Azure portal](https://portal.azure.com/).

2. Select **More Services** from the bottom of the left-hand menu, and type "sql data warehouses" into the **filter** box and click on it.

3. Click on your data warehouse. 

4. On the **Overview** page for your database, copy your server name.

   ![connection information](./media/get-started-sql-dw/server-name.png) 

5. If you have forgotten the login information for your Azure SQL Data Warehouse server, navigate to the SQL Data Warehouse server page to view the server admin name and, if necessary, reset the password. 

## Connect to your data warehouse

Use [!INCLUDE[name-sos](../includes/name-sos-short.md)] to establish a connection to your Azure SQL Data Warehouse server.

1. When first loading [!INCLUDE[name-sos](../includes/name-sos-short.md)], a connection page should be displayed. If not, click the **New Connection** icon on the top left.
   
   ![New Connection Icon](media/get-started-sql-dw/new-connection-icon.png)

2. Follow the prompts to specify the connection properties for the new connection profile. After specifying each value, press **ENTER** to continue. 

   | Setting       | Suggested value | Description |
   | ------------ | ------------------ | ------------------------------------------------- | 
   | **Server name** | The fully qualified server name | The name should be something like this: **sqldwsample.database.windows.net** |
   | **Authentication** | SQL Login| SQL Authentication is used in this tutorial. |
   | **User name** | The server admin account | This is the account that you specified when you created the server. |
   | **Password (SQL Login)** | The password for your server admin account | This is the password that you specified when you created the server. |
   | **Save Password?** | Yes or No | Select Yes if you do not want to enter the password each time. |
   | **Database name** | *leave blank* | The name of the database to which to connect. |
   | **Server Group** | Select <Default> | If you created a server group, you can set to a specific server group. | 

   ![New Connection Icon](media/get-started-sql-dw/new-connection-screen.png) 

3. If you are successfully connected, ignore this step. If you see the following screen, you need to register your IP address. You can do this through [!INCLUDE[name-sos](../includes/name-sos-short.md)] by clicking add an account, logging in with your Azure credentials, and then adding your IP.

   ![Firewall image](media/get-started-sql-dw/setup-firewall-ip.png)   

4. You should see your connection in the object explorer.

## Create a tutorial database
1. Right click on your server, in the object explorer and select **New Query.**

   ![NewQuery](media/get-started-sql-dw/new-query.png)

1. Paste the following snippet into the query window:

   ```sql
   IF NOT EXISTS (
      SELECT name
      FROM sys.databases
      WHERE name = N'TutorialDB'
   )
   CREATE DATABASE [TutorialDB]
   GO

   ALTER DATABASE [TutorialDB] SET QUERY_STORE=ON
   GO
   ```
1. To execute the query, click **Run**.

## Create a table

1. Change the context to **TutorialDB**: 

   ![Change connection context](media/get-started-sql-dw/change-context.png)

1. Paste the following snippet into the query window:

   ```sql
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

1. To execute the query, click **Run**.

## Insert rows

1. Paste the following snippet into the query window:

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

1. To execute the query, click **Run**.

## View the result
1. Paste the following snippet into the query window:

   ```sql
   -- Select rows from table 'Customers'
   SELECT * FROM dbo.Customers;
   ```

1. To execute the query, click **Run**.

   ![Select results](media/get-started-sql-dw/select-results.png)

## Save result as Excel

Right-click the results table and select **Save As Excel**. 

   ![Save as Excel](media/get-started-sql-dw/save-as-excel.png)


## View chart
View an existing, built-in widget through the dashboard.

## Clean up resources

Other articles in this collection build upon this quickstart. If you plan to continue on to work with subsequent quickstarts, do not clean up the resources created in this quickstart. If you do not plan to continue, use the following steps to delete resources created by this quickstart in the Azure portal.
Clean up resources by deleting the resource groups you no longer need. For details, see [Clean up resources](https://docs.microsoft.com/en-us/azure/sql-database/sql-database-get-started-portal#clean-up-resources).

## Next steps
> [!div class="nextstepaction"]
> [Apply modern code flow using [!INCLUDE[name-sos](../includes/name-sos-short.md)]](tutorial-modern-code-flow-sql-server.md)