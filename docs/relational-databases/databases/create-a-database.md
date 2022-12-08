---
title: "Create a Database"
description: "Find out how to create a database in SQL Server 2019 by using SQL Server Management Studio or Transact-SQL. View recommendations for the procedure."
ms.custom: FY22Q2Fresh
ms.date: "10/21/2021"
ms.service: sql
ms.reviewer: ""
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords: 
  - "databases [SQL Server], creating"
  - "database creation [SQL Server], SQL Server Management Studio"
  - "creating databases"
author: WilliamDAssafMSFT
ms.author: wiassaf
---
# Create a Database
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  This article describes how to create a database in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].  

> [!NOTE]
> To create a database in Azure SQL Database using T-SQL, see [Create database in Azure SQL Database](../../t-sql/statements/create-database-transact-sql.md).
  
##  <a name="Restrictions"></a> Limitations and restrictions  
  
-   A maximum of 32,767 databases can be specified on an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
##  <a name="Prerequisites"></a> Prerequisites  
  
-   The CREATE DATABASE statement must run in autocommit mode (the default transaction management mode) and is not allowed in an explicit or implicit transaction.  
  
##  <a name="Recommendations"></a> Recommendations  
  
-   The [master](../../relational-databases/databases/master-database.md) database should be backed up whenever a user database is created, modified, or dropped.  
  
-   When you create a database, make the data files as large as possible based on the maximum amount of data you expect in the database.  
  
##  <a name="Security"></a> Security  
  
###  <a name="Permissions"></a> Permissions  
 Requires CREATE DATABASE permission in the `master` database, or requires CREATE ANY DATABASE, or ALTER ANY DATABASE permission.  
  
 To maintain control over disk use on an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], permission to create databases is typically limited to a few SQL Server login.  
  
##  <a name="SSMSProcedure"></a> Use SQL Server Management Studio  
  
#### To create a database  
  
1.  In **Object Explorer**, connect to an instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] and then expand that instance.  
  
2.  Right-click **Databases**, and then select **New Database**.  
  
3.  In **New Database**, enter a database name.  
  
4.  To create the database by accepting all default values, select **OK**; otherwise, continue with the following optional steps.  
  
5.  To change the owner name, select (**...**) to select another owner.  
  
    > [!NOTE]  
    >  The **Use full-text indexing** option is always checked and dimmed because, beginning in [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)], all user databases are full-text enabled.  
  
6.  To change the default values of the primary data and transaction log files, in the **Database files** grid, select the appropriate cell and enter the new value. For more information, see [Add Data or Log Files to a Database](../../relational-databases/databases/add-data-or-log-files-to-a-database.md).  
  
7.  To change the collation of the database, select the **Options** page, and then select a collation from the list.  
  
8.  To change the recovery model, select the **Options** page and select a recovery model from the list.  
  
9. To change database options, select the **Options** page, and then modify the database options. For a description of each option, see [ALTER DATABASE SET Options &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql-set-options.md).  
  
10. To add a new filegroup, select the **Filegroups** page. Select **Add** and then enter the values for the filegroup.  
  
11. To add an extended property to the database, select the **Extended Properties** page.  
  
    1.  In the **Name** column, enter a name for the extended property.  
  
    2.  In the **Value** column, enter the extended property text. For example, enter one or more statements that describe the database.  
  
12. To create the database, select **OK**.  
  
##  <a name="TsqlProcedure"></a> Use Transact-SQL  
  
#### To create a database  
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  From the Standard bar, select **New Query**.  
  
3.  Copy and paste the following example into the query window and select **Execute**. This example creates the database `Sales`. Because the keyword PRIMARY is not used, the first file (`Sales_dat`) becomes the primary file. Because neither MB nor KB is specified in the SIZE parameter for the `Sales_dat` file, it uses MB and is allocated in megabytes. The `Sales_log` file is allocated in megabytes because the `MB` suffix is explicitly stated in the `SIZE` parameter.  
  
```sql  
USE master;  
GO  
CREATE DATABASE Sales  
ON   
( NAME = Sales_dat,  
    FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\saledat.mdf',  
    SIZE = 10,  
    MAXSIZE = 50,  
    FILEGROWTH = 5 )  
LOG ON  
( NAME = Sales_log,  
    FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\salelog.ldf',  
    SIZE = 5MB,  
    MAXSIZE = 25MB,  
    FILEGROWTH = 5MB );  
GO  
```  
  
 For more examples, see [CREATE DATABASE &#40;SQL Server Transact-SQL&#41;](../../t-sql/statements/create-database-transact-sql.md).  
  
## Next steps

 - [Database Files and Filegroups](../../relational-databases/databases/database-files-and-filegroups.md)   
 - [Database Detach and Attach &#40;SQL Server&#41;](../../relational-databases/databases/database-detach-and-attach-sql-server.md)   
 - [ALTER DATABASE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql.md)   
 - [Add Data or Log Files to a Database](../../relational-databases/databases/add-data-or-log-files-to-a-database.md)  
  
