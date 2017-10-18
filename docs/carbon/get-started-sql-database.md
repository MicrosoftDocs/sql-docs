---
title: Connect and query an Azure SQL database using Carbon | Microsoft Docs
description: Use Carbon to connect to a SQL database and run a query
author: yualan
ms.author: alayu
manager: craigg
ms.reviewer: achatter, alayu, erickang, sanagama, sstein
ms.service: data-tools
ms.workload: data-tools
ms.prod: NEEDED
ms.custom: mvc
ms.topic: quickstart
ms.date: 10/01/2017
---
# Azure SQL Database: Use Carbon to connect and query data

This quickstart demonstrates how to use Carbon to connect to an Azure SQL database, and then use Transact-SQL statements to create, insert, and select data in the database.

## Prerequisites

This quickstart uses as its starting point the resources created in one of these quick starts:

- [Create DB - Portal](sql-database-get-started-portal.md)
- [Create DB - CLI](sql-database-get-started-cli.md)
- [Create DB - PowerShell](sql-database-get-started-powershell.md)

Before you start, make sure you have installed the newest version of [Carbon](https://go.microsoft.com/fwlink/?linkid=853016) and loaded the [mssql extension](https://aka.ms/mssql-marketplace). For installation guidance for the mssql extension, see [Install Carbon](download-carbon.md).

## Configure Carbon 

### **Mac OS**
For macOS, you need to install OpenSSL which is a prerequiste for DotNet Core that mssql extention uses. Open your terminal and enter the following commands to install **brew** and **OpenSSL**. 

```bash
ruby -e "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/master/install)"
brew update
brew install openssl
mkdir -p /usr/local/lib
ln -s /usr/local/opt/openssl/lib/libcrypto.1.0.0.dylib /usr/local/lib/
ln -s /usr/local/opt/openssl/lib/libssl.1.0.0.dylib /usr/local/lib/
```

### **Linux (Ubuntu)**

No special configuration needed.

### **Windows**

No special configuration needed.

## SQL server connection information

Get the connection information needed to connect to the Azure SQL database. You will need the fully qualified server name, database name, and login information in the next procedures.

1. Log in to the [Azure portal](https://portal.azure.com/).
2. Select **SQL Databases** from the left-hand menu, and click your database on the **SQL databases** page. 
3. On the **Overview** page for your database, review the fully qualified server name as shown in the following image. You can hover over the server name to bring up the **Click to copy** option.

   ![connection information](./media/get-started-sql-database/server-name.png) 

4. If you have forgotten the login information for your Azure SQL Database server, navigate to the SQL Database server page to view the server admin name and, if necessary, reset the password. 

## Connect to your database

Use Carbon to establish a connection to your Azure SQL Database server.

> [!IMPORTANT]
> Before continuing, make sure that you have your server, database, and login information ready. Once you begin entering the connection profile information, if you change your focus from Carbon, you have to restart creating the connection profile.
>

1. In Carbon, click the **New Connection** icon on the top left.
   
   ![New Connection Icon](media/quickstart-sql-server/new-connection-icon.png)

2. Follow the prompts to specify the connection properties for the new connection profile. After specifying each value, press **ENTER** to continue. 

   | Setting       | Suggested value | Description |
   | ------------ | ------------------ | ------------------------------------------------- | 
   | **Server name** | The fully qualified server name | The name should be something like this: **mynewserver20170313.database.windows.net**. |
   | **Database name** | mySampleDatabase | The name of the database to which to connect. |
   | **Authentication** | SQL Login| SQL Authentication is the only authentication type that we have configured in this tutorial. |
   | **User name** | The server admin account | This is the account that you specified when you created the server. |
   | **Password (SQL Login)** | The password for your server admin account | This is the password that you specified when you created the server. |
   | **Save Password?** | Yes or No | Select Yes if you do not want to enter the password each time. |
   | **Enter a name for this profile** | A profile name, such as **mySampleDatabase** | A saved profile name speeds your connection on subsequent logins. | 

5. Press the **ESC** key to close the info message that informs you that the profile is created and connected.

6. Verify your connection in the status bar.

## Create a tutorial database
1. Right click on your server in the object explorer and select **New Query.**

2. Copy the snippet below and paste in the query window. Click **Run** to execute the query.

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

   ALTER DATABASE [TutorialDB] SET QUERY_STORE=ON
   GO
   ```
3. Add section to change database context

## Create a table
1. Copy the snippet below and paste in the query window. Click **Run** to execute the query.
   ```sql
   -- Create a new table called 'Customers' in schema 'dbo'
   -- Drop the table if it already exists
   IF OBJECT_ID('TutorialDB.dbo.Customers', 'U') IS NOT NULL
   DROP TABLE TutorialDB.dbo.Customers
   GO
   -- Create the table in the specified schema
   CREATE TABLE TutorialDB.dbo.Customers
   (
      CustomersId        INT    NOT NULL   PRIMARY KEY, -- primary key column
      Name      [NVARCHAR](50)  NOT NULL,
      Location  [NVARCHAR](50)  NOT NULL,
      Email     [NVARCHAR](50)  NOT NULL
   );
   GO
   ```

## Insert rows
1. Copy the snippet below to insert four rows and and paste in the query window. Click **Run** to execute the query.
   ```sql
   -- Insert rows into table 'Customers'
   INSERT INTO TutorialDB.dbo.Customers
      ([CustomersId],[Name],[Location],[Email])
   VALUES
      ( 1, N'Jared', N'Australia', N''),
      ( 2, N'Nikita', N'India', N'nikita@vsdata.io'),
      ( 3, N'Tom', N'Germany', N'tom@vsdata.io'),
      ( 4, N'Jake', N'United States', N'jake@vsdata.io')   
   GO   
   ```

## View the result
1. Copy the snippet below to view all the rows and paste in the query window. Click **Run** to execute the query.
   ```sql
   -- Insert rows into table 'Customers'
   SELECT * FROM TutorialDB.dbo.Customers;
   ```

   ![Select results](media/quickstart-sql-server/select-results.png)

## Create table with many rows
1. Copy the snippet below to create a table with many rows. Click **Run** to execute the query.
   ```sql
   USE TutorialDB; 
   WITH a AS (SELECT * FROM (VALUES(1),(2),(3),(4),(5),(6),(7),(8),(9),(10)) AS a(a))
   SELECT TOP(1000)
   ROW_NUMBER() OVER (ORDER BY a.a) AS OrderItemId 
   ,a.a + b.a + c.a + d.a + e.a + f.a + g.a + h.a AS OrderId 
   ,a.a * 10 AS Price 
   ,CONCAT(a.a, N' ', b.a, N' ', c.a, N' ', d.a, N' ', e.a, N' ', f.a, N' ', g.a, N' ', h.a) AS ProductName 
   INTO Table_with_5M_rows 
   FROM a, a AS b, a AS c, a AS d, a AS e, a AS f, a AS g, a AS h;
   GO
   ```

## Save result as Excel
1. Right click on the results table and save as a **Excel** file. 

   ![Save as Excel](media/quickstart-sql-server/save-as-json.png)

2. Save as **Results.xls**.

## View chart
View Table size.

View built-in widgets through the dashboard.

## Next steps
> [!div class="nextstepaction"]
> [Apply modern code flow using Carbon](tutorial-modern-code-flow-sql-server.md)

> [!div class="nextstepaction"]
> [Monitor your SQL Server databases using Carbon](tutorial-monitoring-sql-server.md)

> [!div class="nextstepaction"]
> [Backup and restore your SQL Server databases using Carbon](tutorial-backup-restore-sql-server.md)