---
title: Connect and query an Azure SQL database using SQL Operations Studio | Microsoft Docs
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
# Azure SQL Database: Use [!INCLUDE[name-sos](../includes/name-sos-short.md)] to connect and query data

This quickstart demonstrates how to use [!INCLUDE[name-sos](../includes/name-sos-short.md)] to connect to an Azure SQL database, and then use Transact-SQL (T-SQL)statements to create, insert, and select data.

## Prerequisites

To complete this quickstart, you need !INCLUDE[name-sos](../includes/name-sos-short.md), and an Azure SQL server where you have *CREATE DATABASE* permissions.

- [Install [!INCLUDE[name-sos](../includes/name-sos-short.md)]](download.md).

If you don't already have an Azure SQL server, complete any one of the following Azure SQL Database quickstarts:

- [Create DB - Portal](https://docs.microsoft.com/azure/sql-database/sql-database-get-started-portal)
- [Create DB - CLI](https://docs.microsoft.com/azure/sql-database/sql-database-get-started-cli)
- [Create DB - PowerShell](https://docs.microsoft.com/azure/sql-database/sql-database-get-started-powershell)


## Get your Azure SQL server connection string

Get the connection information needed to connect to the Azure SQL database. You need the fully qualified server name, database name, and login information in the next procedures.

1. Log in to the [Azure portal](https://portal.azure.com/).

2. Select **SQL Databases** from the left-hand menu, and click your database on the **SQL databases** page. 

3. On the **Overview** page for your database, copy your server name.

   ![connection information](./media/get-started-sql-database/server-name.png) 


4. If you have forgotten the login information for your Azure SQL Database server, navigate to the SQL Database server page to view the server admin name and, if necessary, reset the password. 

## Connect to your database

Use [!INCLUDE[name-sos](../includes/name-sos-short.md)] to establish a connection to your Azure SQL Database server.

1. The first time you run [!INCLUDE[name-sos](../includes/name-sos-short.md)] the **Connection** page should open. If the **Connection** page doesn't open, click the **New Connection** icon in the **SERVERS** page:
   
   ![New Connection Icon](media/get-started-sql-database/new-connection-icon.png)

2. This article uses *SQL Login*, but *Windows Authentication* is also supported. Fill in the fields as follows:

   | Setting | Suggested value | 
   | :--- | :--- |
   | **Authentication** | SQL Login | 
   | **User name** | The server admin account | 
   | **Password (SQL Login)** | The password for your Azure SQL server | 
   | **Save Password?** | Yes or No - Select Yes if you do not want to enter the password each time. |
   | **Database name** | *leave blank* | 
   | **Server Group** | Select \<Default\> | 

   ![New Connection Icon](media/get-started-sql-database/new-connection-screen.png)  

3. If you are successfully connected, skip ahead to the next section. If you see the *Create firewall rule* screen, you need to register your IP address. Add an account, and click **OK**.

   ![Firewall image](media/get-started-sql-database/setup-firewall-ip.png)  


## Create a tutorial database
1. Right click on your Azure SQL server in the SERVERS screen and select **New Query.**

1. Paste the following snippet into the query window.

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
1. To execute the query click **Run**.

## Create a table
1. Copy the snippet below and paste in the query window.
   ```sql
   -- Create a new table called 'Customers' in schema 'dbo'
   -- Drop the table if it already exists
   IF OBJECT_ID('dbo.Customers', 'U') IS NOT NULL
   DROP TABLE dbo.Customers
   GO
   -- Create the table in the specified schema
   CREATE TABLE dbo.Customers
   (
      CustomersId        INT    NOT NULL   PRIMARY KEY, -- primary key column
      Name      [NVARCHAR](50)  NOT NULL,
      Location  [NVARCHAR](50)  NOT NULL,
      Email     [NVARCHAR](50)  NOT NULL
   );
   GO
   ```

2. Change the context to **TutorialDB.** Click **Run** to execute the query.

   ![Change context](media/get-started-sql-database/change-context.png)

## Insert rows
Copy the snippet below to insert four rows and paste in the query window. Click **Run** to execute the query.
   ```sql
   -- Insert rows into table 'Customers'
   INSERT INTO dbo.Customers
      ([CustomersId],[Name],[Location],[Email])
   VALUES
      ( 1, N'Jared', N'Australia', N''),
      ( 2, N'Nikita', N'India', N'nikita@vsdata.io'),
      ( 3, N'Tom', N'Germany', N'tom@vsdata.io'),
      ( 4, N'Jake', N'United States', N'jake@vsdata.io')   
   GO   
   ```

## View the result
Copy the snippet below to view all the rows and paste in the query window. Click **Run** to execute the query.
   ```sql
   -- Select rows from table 'Customers'
   SELECT * FROM dbo.Customers;
   ```
   ![Select results](media/get-started-sql-database/select-results.png)

## Save result as Excel
1. Right click on the results table and save as a **Excel** file. 

   ![Save as Excel](media/get-started-sql-database/save-as-excel.png)

2. Save as **Results.xls**.

## View chart
View an existing, built-in widget through the dashboard.

## Clean up resources
> [!TIP]
> Other Quickstarts in this collection build upon this quick start. If you plan to continue on to work with subsequent quickstarts, **do not clean up the resources created in this quickstart**. If you do not plan to continue, use the following steps to delete resources created by this quickstart in the Azure portal.
Clean up the resources you created in the quickstart either by deleting the ...

To delete the entire resource group including the newly created server:
1.	Locate your resource group in the Azure portal. From the left-hand menu in the Azure portal, click **Resource groups** and then click the name of your resource group, such as our example **myresourcegroup**.
2.	On your resource group page, click **Delete**. Then type the name of your resource group, such as our example **myresourcegroup**, in the text box to confirm deletion, and then click **Delete**.

Or instead, to delete the newly created server:
1.	Locate your server in the Azure portal, if you do not have it open. From the left-hand menu in Azure portal, click **All resources**, and then search for the server you created.
2.	On the **Overview** page, click the **Delete** button on the top pane.
3.	Confirm the server name you want to delete.

## Next steps
> [!div class="nextstepaction"]
> [Apply modern code flow using [!INCLUDE[name-sos](../includes/name-sos-short.md)]](tutorial-modern-code-flow-sql-server.md)
