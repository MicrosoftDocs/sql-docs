---
title: Create a database and permissions for RevoScaleR tutorials - SQL Server Machine Learning
description: Tutorial walkthrough on how to create a SQL Server database for R tutorials..
ms.prod: sql
ms.technology: machine-learning

ms.date: 11/27/2018  
ms.topic: tutorial
ms.author: dphansen
ms.author: davidph
manager: cgronlun
---
# Create a database and permissions (SQL Server and RevoScaleR tutorial)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

This lesson is part of the [RevoScaleR tutorial](deepdive-data-science-deep-dive-using-the-revoscaler-packages.md) on how to use [RevoScaleR functions](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/revoscaler) with SQL Server.

Lesson one is about setting up a SQL Server database and permissions necessary for completing this tutorial. Use [SQL Server Management Studio](https://docs.microsoft.com/sql/ssms/download-sql-server-management-studio-ssms) or another query editor to complete the following tasks:

> [!div class="checklist"]
> * Create a new database to store the data for training and scoring two R models
> * Create a database user login with permissions for creating and using database objects
  
## Create the database

This tutorial requires a database for storing data and code. If you are not an administrator, ask your DBA to create the database and login for you. You will need permissions to write and read data, and to run R scripts.

1. In SQL Server Management Studio, connect to an R-enabled database instance.

2. Right-click **Databases**, and select **New database**.
  
2. Type a name for the new database: RevoDeepDive.
  

## Create a login
  
1. Click **New Query**, and change the database context to  the master database.
  
2. In the new **Query** window, run the following commands to create the user accounts and assign them to the database used for this tutorial. Be sure to change the database name if needed.

3. To verify the login, select the new database, expand **Security**, and expand **Users**.
  
**Windows user**
  
```sql
 -- Create server user based on Windows account
USE master
GO
CREATE LOGIN [<DOMAIN>\<user_name>] FROM WINDOWS WITH DEFAULT_DATABASE=[RevoDeepDive]

 --Add the new user to tutorial database
USE [RevoDeepDive]
GO
CREATE USER [<user_name>] FOR LOGIN [<DOMAIN>\<user_name>] WITH DEFAULT_SCHEMA=[db_datareader]
```

**SQL login**

```sql
-- Create new SQL login
USE master
GO
CREATE LOGIN [DDUser01] WITH PASSWORD='<type password here>', CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF;

-- Add the new SQL login to tutorial database
USE RevoDeepDive
GO
CREATE USER [DDUser01] FOR LOGIN [DDUser01] WITH DEFAULT_SCHEMA=[db_datareader]
```

## Assign permissions

This tutorial demonstrates R script and DDL operations, including creating and deleting tables and stored procedures, and running R script in an external process on SQL Server. In this step, assign permssions to allow these tasks.

This example assumes a SQL login (DDUser01), but if you created a Windows login, use that instead.

```sql
USE RevoDeepDive
GO

EXEC sp_addrolemember 'db_owner', 'DDUser01'
GRANT EXECUTE ANY EXTERNAL SCRIPT TO DDUser01
GO
```

## Troubleshoot connections

This section lists some common issues that you might run across in the course of setting up the database.

- **How can I verify database connectivity and check SQL queries?**
  
    Before you run R code using the server, you might want to check that the database can be reached from your R development environment. Both [Server Explorer in Visual Studio](https://docs.microsoft.com/previous-versions/x603htbk(v=vs.140)) and [SQL Server Management Studio](../../ssms/download-sql-server-management-studio-ssms.md) are free tools with powerful database connectivity and management features.
  
    If you don't want to install additional database management tools, you can create a test connection to the SQL Server instance by using the [ODBC Data Source Administrator](https://docs.microsoft.com/sql/odbc/admin/odbc-data-source-administrator?view=sql-server-2017) in Control Panel. If the database is configured correctly and you enter the correct user name and password, you should be able to see the database you just created and select it as your default database.
  
    Common reasons for connection failures include remote connections are not enabled for the server, and Named Pipes protocol is not enabled. You can find more troubleshooting tips in this article: [Troubleshoot Connecting to the SQL Server Database Engine](https://docs.microsoft.com/sql/database-engine/configure-windows/troubleshoot-connecting-to-the-sql-server-database-engine).
  
- **My table name has datareader prefixed to it - why?**
  
    When you specify the default schema for this user as **db_datareader**, all tables and other new objects created by this user are prefixed with the *schema* name. A schema is like a folder that you can add to a database to organize objects. The schema also defines a user's privileges within the database.
  
    When the schema is associated with one particular user name, the user is the _schema owner_. When you create an object, you always create it in your own schema, unless you specifically ask it to be created in another schema.
  
    For example, if you create a table with the name **TestData**, and your default schema is **db_datareader**, the table is created with the name `<database_name>.db_datareader.TestData`.
  
    For this reason, a database can contain multiple tables with the same names, as long as the tables belong to different schemas.
   
    If you are looking for a table and do not specify a schema, the database server looks for a schema that you own. Therefore, there is no need to specify the schema name when accessing tables in a schema associated with your login.
  
- **I don't have DDL privileges. Can I still run the tutorial?**?
  
    Yes, but you should ask someone to pre-load the data into the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] tables, and skip ahead to the next lesson. The functions that require DDL privileges are called out in the tutorial wherever possible.

    Also, ask your administrator to grant you the permission, EXECUTE ANY EXTERNAL SCRIPT. It is needed for R script execution, whether remote or by using `sp_execute_external_script`.

## Next steps

> [!div class="nextstepaction"]
> [Create SQL Server data objects using RxSqlServerData](../../advanced-analytics/tutorials/deepdive-create-sql-server-data-objects-using-rxsqlserverdata.md)