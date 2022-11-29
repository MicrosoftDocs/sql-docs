---
title: "Rename a Database"
description: "Rename a Database"
ms.service: sql
ms.subservice: 
ms.topic: conceptual
helpviewer_keywords: 
  - "databases [SQL Server], renaming"
  - "renaming databases"
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: ""
ms.custom: FY22Q2Fresh
ms.date: "04/06/2022"
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Rename a Database

[!INCLUDE [SQL Server Azure SQL Database](../../includes/applies-to-version/sql-asdb.md)]
  This article describes how to rename a user-defined database in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] or Azure SQL Database by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)]. The name of the database can include any characters that follow the rules for identifiers.  

> [!NOTE]
> To rename a database in Azure Synapse Analytics or Parallel Data Warehouse, use the [RENAME (Transact-SQL)](../../t-sql/statements/rename-transact-sql.md) statement.
  
## Limitations and restrictions  
  
- System databases cannot be renamed.
- The database name cannot be changed while other users are accessing the database. 
  - Use SQL Server Management Studio Activity Monitor to find other connections to the database, and close them. For more information, see [Open Activity Monitor in SQL Server Management Studio (SSMS)](../performance-monitor/open-activity-monitor-sql-server-management-studio.md).
  - In SQL Server, you can set a database in single user mode to close any open connections. For more information, see [set the database to single-user mode](../../relational-databases/databases/set-a-database-to-single-user-mode.md).
  - In Azure SQL Database, you must make sure no other users have an open connection to the database to be renamed.
- Renaming a database does not change the physical name of the database files on disk, or the logical names of the files. For more information, see [Database Files and Filegroups](database-files-and-filegroups.md#logical-and-physical-file-names).
- It's not possible to rename an Azure SQL database configured in an [active geo-replication](/azure/azure-sql/database/active-geo-replication-overview) relationship.


## Permissions

Requires ALTER permission on the database.  
  
## Use SQL Server Management Studio

Use the following steps to rename a SQL Server or Azure SQL database using SQL Server Management Studio.

1. In SQL Server Management Studio, select **Object Explorer**. To open **Object Explorer**, select F8. Or on the top menu, select **View**, and then select **Object Explorer**:
  
2. In **Object Explorer**, connect to an instance of SQL Server, and then expand that instance.
  
3. Make sure that there are no open connections to the database. If you are using SQL Server, you can [set the database to single-user mode](../../relational-databases/databases/set-a-database-to-single-user-mode.md) to close any open connections and prevent other users from connecting while you are changing the database name.  
  
4. In Object Explorer, expand **Databases**, right-click the database to rename, and then select **Rename**.  
  
5. Enter the new database name, and then select **OK**
  
6. If the database was your default database, see [Reset your default database after rename](#reset-your-default-database-after-rename).

7. Refresh the database list in Object Explorer.

## Use Transact-SQL  
  
### To rename a SQL Server database by placing it in single-user mode

Use the following steps to rename a SQL Server database using T-SQL in SQL Server Management Studio including the steps to place the database in single-user mode and, after the rename, place the database back in multi-user mode.
  
1. Connect to the `master` database for your instance.  
2. Open a query window.  
3. Copy and paste the following example into the query window and select **Execute**. This example changes the name of the `MyTestDatabase` database to `MyTestDatabaseCopy`.
  
> [!WARNING]
> To quickly obtain exclusive access, the code sample uses the termination option `WITH ROLLBACK IMMEDIATE`. This will cause all incomplete transactions to be rolled back and any other connections to the `MyTestDatabase` database to be immediately disconnected.  

   ```sql
   USE master;  
   GO  
   ALTER DATABASE MyTestDatabase SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
   GO
   ALTER DATABASE MyTestDatabase MODIFY NAME = MyTestDatabaseCopy;
   GO  
   ALTER DATABASE MyTestDatabaseCopy SET MULTI_USER;
   GO
   ```  

4. Optionally, if the database was your default database, see [Reset your default database after rename](#reset-your-default-database-after-rename).

### To rename an Azure SQL Database database

Use the following steps to rename an Azure SQL database using T-SQL in SQL Server Management Studio.
  
1. Connect to the `master` database for your instance.  
2. Open a query window.
3. Make sure that no one is using the database.
4. Copy and paste the following example into the query window and select **Execute**. This example changes the name of the `MyTestDatabase` database to `MyTestDatabaseCopy`.
  
   ```sql
   ALTER DATABASE MyTestDatabase MODIFY NAME = MyTestDatabaseCopy;
   ```  

## Backup after renaming a database  

After renaming a database in SQL Server, back up the `master` database. In Azure SQL Database, this is not needed as backups occur automatically.  
  
## Reset your default database after rename

If the database you're renaming was set as the default database of a SQL Server login, they may encounter Error 4064, `Cannot open user default database`. Use the following command to change the default to the renamed database:

```sql
USE [master]
GO
ALTER LOGIN [login] WITH DEFAULT_DATABASE=[new-database-name];
GO
```

## Next steps

- [ALTER DATABASE (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql.md)
- [Database Identifiers](../../relational-databases/databases/database-identifiers.md)  
