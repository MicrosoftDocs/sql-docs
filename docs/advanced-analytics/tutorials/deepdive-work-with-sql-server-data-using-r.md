---
title: "Work with SQL Server data using R  (SQL and R deep dive)| Microsoft Docs"
ms.date: "12/14/2017"
ms.reviewer: 
ms.suite: sql
ms.prod: machine-learning-services
ms.prod_service: machine-learning-services
ms.component: 
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "tutorial"
applies_to: 
  - "SQL Server 2016"
  - "SQL Server 2017"
dev_langs: 
  - "R"
ms.assetid: 0a3d7ba0-4113-4cde-9645-debba45cae8f
caps.latest.revision: 20
author: "jeannt"
ms.author: "jeannt"
manager: "cgronlund"
ms.workload: "On Demand"
---
# Work with SQL Server Data using R (SQL and R deep dive)

This article is part of the Data Science Deep Dive tutorial, on how to use [RevoScaleR](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/revoscaler) with SQL Server.

In this lesson, you set up the environment and add the data you need for training your models and run some quick summaries of the data. As part of the process, you must complete these tasks:
  
- Create a new database to store the data for training and scoring two R models.
  
- Create an account (either a Windows user or SQL login) to use when communicating between your workstation and the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] computer.
  
- Create data sources in R for working with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data and database objects.
  
- Use the R data source to load data into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].
  
- Use R to get a list of variables and modify the metadata of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table.
  
- Create a compute context to enable remote execution of R code.
  
- (Optional) Enable tracing on the remote compute context.
  
## Create the database and user

For this walkthrough, create a new database in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], and add a SQL login with permissions to write and read data, and to run R scripts.

> [!NOTE]
> If you are only reading data, the account that runs the R scripts requires SELECT permissions (**db_datareader** role) on the specified database. However, in this tutorial, you must have DDL admin privileges to prepare the database, and to create tables for saving the scoring results.
> 
> Additionally, if you are not the database owner, you need the permission, EXECUTE ANY EXTERNAL SCRIPT, in order to execute R scripts.

1. In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], select the instance where [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] is enabled, right-click **Databases**, and select **New database**.
  
2. Type a name for the new database. You can use any name you want; just remember to edit all the [!INCLUDE[tsql](../../includes/tsql-md.md)] scripts and R scripts in this walkthrough accordingly.
  
    > [!TIP]
    > To view the updated database name, right-click **Databases** and select **Refresh** .
  
3. Click **New Query**, and change the database context to  the master database.
  
4. In the new **Query** window, run the following commands to create the user accounts and assign them to the database used for this tutorial. Be sure to change the database name if needed.
  
**Windows user**
  
```SQL
 -- Create server user based on Windows account
USE master
GO
CREATE LOGIN [<DOMAIN>\<user_name>] FROM WINDOWS WITH DEFAULT_DATABASE=[DeepDive]

 --Add the new user to tutorial database
USE [DeepDive]
GO
CREATE USER [<user_name>] FOR LOGIN [<DOMAIN>\<user_name>] WITH DEFAULT_SCHEMA=[db_datareader]
```

**SQL login**

```SQL
-- Create new SQL login
USE master
GO
CREATE LOGIN DDUser01 WITH PASSWORD='<type password here>', CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF;

-- Add the new SQL login to tutorial database
USE [DeepDive]
GO
CREATE USER [DDUser01] FOR LOGIN [DDUser01] WITH DEFAULT_SCHEMA=[db_datareader]
```

5. To verify that the user has been created, select the new database, expand **Security**, and expand **Users**.

## Troubleshooting

This section lists some common issues that you might run across in the course of setting up the database.

- **How can I verify database connectivity and check SQL queries?**
  
    Before you run R code using the server, you might want to check that the database can be reached from your R development environment. Both [Server Explorer in Visual Studio](https://msdn.microsoft.com/library/x603htbk.aspx) and [SQL Server Management Studio](../../ssms/download-sql-server-management-studio-ssms.md) are free tools with powerful database connectivity and management features.
  
    If you don't want to install additional database management tools, you can create a test connection to the SQL Server instance by using the [ODBC Data Source Administrator](https://msdn.microsoft.com/library/ms714024.aspx) in Control Panel. If the database is configured correctly and you enter the correct user name and password, you should be able to see the database you just created and select it as your default database.
  
    If you cannot connect to the database,  verify that remote connections are enabled for the server, and that the Named Pipes protocol has been enabled. Additional troubleshooting tips are provided in this article: [Troubleshoot Connecting to the SQL Server Database Engine](https://docs.microsoft.com/sql/database-engine/configure-windows/troubleshoot-connecting-to-the-sql-server-database-engine).
  
- **My table name has datareader prefixed to it - why?**
  
    When you specify the default schema for this user as **db_datareader**, all tables and other new objects created by this user are prefixed with the *schema* name. A schema is like a folder that you can add to a database to organize objects. The schema also defines a user's privileges within the database.
  
    When the schema is associated with one particular user name, the user is the _schema owner_. When you create an object, you always create it in your own schema, unless you specifically ask it to be created in another schema.
  
    For example, if you create a table with the name `*`TestData`, and your default schema is **db\_datareader**, the table is created with the name `<database_name>.db_datareader.TestData`.
  
    For this reason, a database can contain multiple tables with the same names, as long as the tables belong to different schemas.
   
    If you are looking for a table and do not specify a schema, the database server looks for a schema that you own. Therefore, there is no need to specify the schema name when accessing tables in a schema associated with your login.
  
- **I don't have DDL privileges. Can I still run the tutorial?**?
  
    Yes; however, you should ask someone to pre-load the data into the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] tables, and skip past the sections that call for creating new tables. The functions that require DDL privileges are called out in the tutorial wherever possible.

    Also, ask your administrator to grant you the permission, EXECUTE ANY EXTERNAL SCRIPT. It is needed for R script execution, whether remote or by using `sp_execute_external_script`.

## Next step

[Create SQL Server data objects using RxSqlServerData](../../advanced-analytics/tutorials/deepdive-create-sql-server-data-objects-using-rxsqlserverdata.md)

## Overview

[Data Science Deep Dive: Using the RevoScaleR Packages](../../advanced-analytics/tutorials/deepdive-data-science-deep-dive-using-the-revoscaler-packages.md)



