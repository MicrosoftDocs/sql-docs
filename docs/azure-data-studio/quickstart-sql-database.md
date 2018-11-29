---
title: "Quickstart: Connect and query an Azure SQL database using Azure Data Studio | Microsoft Docs"
description: This quickstart shows how to use Azure Data Studio to connect to a SQL database and run a query
ms.custom: "tools|sos"
ms.date: "09/24/2018"
ms.prod: sql
ms.technology: azure-data-studio
ms.reviewer: "alayu; sstein"
ms.topic: "quickstart"
author: "yualan"
ms.author: "alayu"
manager: craigg
---
# Quickstart: Use [!INCLUDE[name-sos](../includes/name-sos-short.md)] to create and query Azure SQL database

This quickstart demonstrates how to use *[!INCLUDE[name-sos](../includes/name-sos-short.md)]* to connect to an Azure SQL Database server, and then use Transact-SQL (T-SQL) statements to create and query the *TutorialDB* database used in [!INCLUDE[name-sos](../includes/name-sos-short.md)] tutorials.

## Prerequisites

To complete this quickstart, you need [!INCLUDE[name-sos](../includes/name-sos-short.md)], and an Azure SQL Database server.

- [Install [!INCLUDE[name-sos](../includes/name-sos-short.md)]](download.md).

If you do not already have an Azure SQL server, complete one of the following Azure SQL Database quickstarts (remember the fully qualified server name, and login credentials!):

- [Create DB - Portal](https://docs.microsoft.com/azure/sql-database/sql-database-get-started-portal)
- [Create DB - CLI](https://docs.microsoft.com/azure/sql-database/sql-database-get-started-cli)
- [Create DB - PowerShell](https://docs.microsoft.com/azure/sql-database/sql-database-get-started-powershell)


## Connect to your Azure SQL Database server

Use [!INCLUDE[name-sos](../includes/name-sos-short.md)] to establish a connection to your Azure SQL Database server.

1. The first time you run [!INCLUDE[name-sos](../includes/name-sos-short.md)] the **Connection** page should open. If you don't see the **Connection** page, click **Add Connection**, or the **New Connection** icon in the **SERVERS** sidebar:
   
   ![New Connection Icon](media/quickstart-sql-database/new-connection-icon.png)

2. This article uses *SQL Login*, but *Windows Authentication* is also supported. Fill in the following fields using the server name, user name, and password for *your* Azure SQL server:

   | Setting       | Suggested value | Description |
   | ------------ | ------------------ | ------------------------------------------------- | 
   | **Server name** | The fully qualified server name | This name should be something like: **servername.database.windows.net** |
   | **Authentication** | SQL Login| This tutorial uses SQL Authentication. |
   | **User name** | The server admin account user name | The user name from the account used to create the server. |
   | **Password (SQL Login)** | The server admin account password | The password from the account used to create the server. |
   | **Save Password?** | Yes or No | Select Yes if you do not want to enter the password each time. |
   | **Database name** | *leave blank* | You are only connecting to the server here. |
   | **Server Group** | Select <Default> | If you created a server group, you can set to a specific server group. | 

   ![New Connection Icon](media/quickstart-sql-database/new-connection-screen.png)  

3. Click **Connect**.

4. If your server does not have a firewall rule allowing Azure Data Studio to connect, the **Create new firewall rule** form opens. Complete the form to create a new firewall rule. For details, see [Firewall rules](https://docs.microsoft.com/azure/sql-database/sql-database-firewall-configure).

   ![New firewall rule](media/quickstart-sql-database/firewall.png)  

After successfully connecting, your server opens in the *SERVERS* sidebar.

## Create the tutorial database

The following sections create the *TutorialDB* database that is used in several [!INCLUDE[name-sos](../includes/name-sos-short.md)] tutorials.

1. Right click on your Azure SQL server in the SERVERS sidebar and select **New Query.**

1. Paste the following SQL snippet into the query editor and click **Run**:

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



## Create a table

The query editor is still connected to the *master* database, but we want to create a table in the *TutorialDB* database. 

1. Connect to the **TutorialDB** database:

   ![Change context](media/quickstart-sql-database/change-context2.png)



1. Create a *Customers* table. 

   Paste the following SQL snippet into the query editor and click **Run**:

   > [!NOTE]
   > You can append this to, or overwrite the previous query in the editor. Note that clicking **Run** executes only the query that is selected. If nothing is selected, clicking **Run** executes all queries in the editor.

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


## Insert rows into table

- Paste the following SQL snippet into the query editor and click **Run**:

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


## View the result
* Paste the following snippet into the query editor and click **Run**:

   ```sql
   -- Select rows from table 'Customers'
   SELECT * FROM dbo.Customers;
   ```

  The query results are displayed:

   ![Select results](media/quickstart-sql-database/select-results2.png)


## Clean up resources

Subsequent quickstart articles build upon the resources created here. If you plan to work through these articles, do *NOT* delete these resources. Otherwise, in the Azure portal, delete the resources you no longer need. For details, see [Clean up resources](https://docs.microsoft.com/azure/sql-database/sql-database-get-started-portal#clean-up-resources).

## Next steps

Now that you have successfully connected to an Azure SQL database and ran a query, try out the [Code editor tutorial](tutorial-sql-editor.md).
