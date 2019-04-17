---
title: "Rename a Database | Microsoft Docs"
ms.custom: ""
ms.date: "10/02/2018"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
helpviewer_keywords: 
  - "databases [SQL Server], renaming"
  - "renaming databases"
ms.assetid: 44c69d35-abcb-4da3-9370-5e0bc9a28496
author: "stevestein"
ms.author: "sstein"
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Rename a Database

[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
  This topic describes how to rename a user-defined database in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] or Azure SQL Database by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)]. The name of the database can include any characters that follow the rules for identifiers.  
  
## In This Topic
  
- Before you begin:  
  
     [Limitations and Restrictions](#limitations-and-restrictions)  
  
     [Security](#security)  
  
- To rename a database, using:  
  
     [SQL Server Management Studio](#rename-a-database-using-sql-server-management-studio)  
  
     [Transact-SQL](#rename-a-database-using-transact-sql)  
  
- **Follow Up:**  [After renaming a database](#backup-after-renaming-a-database)  

> [!NOTE]
> To rename a database in Azure SQL Data Warehouse or Parallel Data Warehouse, use the [RENAME (Transact-SQL)](../../t-sql/statements/rename-transact-sql.md) statement.
  
## Before You Begin
  
### Limitations and Restrictions  
  
- System databases cannot be renamed.
- The database name cannot be changed while other users are accessing the database. 
  - In SQL Server, you can set a database in single user mode to close any open connections. For more information, see [set the database to single-user mode](../../relational-databases/databases/set-a-database-to-single-user-mode.md).
  - In Azure SQL Database, you must make sure no other users have an open connection to the database to be renamed.
  
### Security  
  
#### Permissions

Requires ALTER permission on the database.  
  
## Rename a database using SQL Server Management Studio

Use the following steps to rename a SQL Server or Azure SQL database using SQL Server Management Studio.
  
1. In **Object Explorer**, connect to your SQL instance.  
  
2. Make sure that there are no open connections to the database. If you are using SQL Server, you can [set the database to single-user mode](../../relational-databases/databases/set-a-database-to-single-user-mode.md) to close any open connections and prevent other users from connecting while you are changing the database name.  
  
3. In Object Explorer, expand **Databases**, right-click the database to rename, and then click **Rename**.  
  
4. Enter the new database name, and then click **OK**.  
  
## Rename a database using Transact-SQL  
  
### To rename a SQL Server database by placing it in single-user mode

Use the following steps to rename a SQL Server database using T-SQL in SQL Server Management Studio including the steps to place the database in single-user mode and, after the rename, place the database back in multi-user mode.
  
1. Connect to the `master` database for your instance.  
2. Open a query window.  
3. Copy and paste the following example into the query window and click **Execute**. This example changes the name of the `MyTestDatabase` database to `MyTestDatabaseCopy`.
  
   ```sql
   USE master;  
   GO  
   ALTER DATABASE MyTestDatabase SET SINGLE_USER WITH ROLLBACK IMMEDIATE
   GO
   ALTER DATABASE MyTestDatabase MODIFY NAME = MyTestDatabaseCopy ;
   GO  
   ALTER DATABASE MyTestDatabaseCopy SET MULTI_USER
   GO
   ```  

### To rename an Azure SQL Database database

Use the following steps to rename an Azure SQL database using T-SQL in SQL Server Management Studio.
  
1. Connect to the `master` database for your instance.  
2. Open a query window.
3. Make sure that no one is using the database.
4. Copy and paste the following example into the query window and click **Execute**. This example changes the name of the `MyTestDatabase` database to `MyTestDatabaseCopy`.
  
   ```sql
   ALTER DATABASE MyTestDatabase MODIFY NAME = MyTestDatabaseCopy ;
   ```  

## Backup after renaming a database  

After renaming a database in SQL Server, back up the `master` database. In Azure SQL Database, this is not needed as backups occur automatically.  
  
## See Also

- [ALTER DATABASE (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql.md)
- [Database Identifiers](../../relational-databases/databases/database-identifiers.md)  