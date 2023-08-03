---
title: "Delete a Database"
description: Learn how to delete a user-defined database in SQL Server Management Studio in SQL Server by using SQL Server Management Studio or Transact-SQL.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "05/24/2023"
ms.service: sql
ms.subservice: supportability
ms.topic: conceptual
helpviewer_keywords:
  - "database removal [SQL Server], SQL Server Management Studio"
  - "removing databases"
  - "deleting databases"
  - "dropping databases"
  - "databases [SQL Server], dropping"
  - "database removal [SQL Server]"
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Delete a Database
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  This article describes how to delete a user-defined database in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
##  <a name="Prerequisites"></a> Prerequisites  
  
-   Delete any database snapshots that exist on the database. For more information, see [Drop a Database Snapshot (Transact-SQL)](../../relational-databases/databases/drop-a-database-snapshot-transact-sql.md).  
  
-   If the database is involved in log shipping, remove log shipping.  
  
-   If the database is published for transactional replication, or published or subscribed to merge replication, remove replication from the database.  

> [!WARNING] 
> Consider taking a full backup of the database before dropping it. A deleted database can be re-created only by restoring a full backup. For more information, see [Quickstart: Backup and restore a SQL Server database on-premises](../backup-restore/quickstart-backup-restore-database.md).
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  

 To execute DROP DATABASE, at a minimum, a user must have CONTROL permission on the database.  

##  <a name="ADSProcedure"></a> Using Azure Data Studio (preview) 

#### To delete a database  
  
1.  In **Object Explorer**, connect to an instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)], and then expand that instance.  
  
2.  Expand **Databases**, right-click the database to delete, and then select **Delete**.  
  
3.  To confirm you want to delete the database, select **Yes**. 

##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To delete a database  
  
1.  In **Object Explorer**, connect to an instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)], and then expand that instance.  
  
1.  Expand **Databases**, right-click the database to delete, and then select  **Delete**.  
  
1.  Confirm the correct database is selected, and then select **OK**.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  

For more information, see [DROP DATABASE (Transact-SQL)](../../t-sql/statements/drop-database-transact-sql.md).

#### To delete a database  
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
1.  From the Standard bar, select **New Query**.  
  
1.  Copy and paste the following example into the query window and select **Execute**. This example removes the `Sales` and `NewSales` databases.  
  
  ```sql  
  USE master ;  
  GO  
  DROP DATABASE Sales, NewSales ;  
  GO  
  ```  
  
##  <a name="FollowUp"></a> Follow Up: After deleting a database  
 
 Back up the `master` database. If `master` must be restored, any database that has been deleted since the last backup of `master` will still have references in the system catalog views and may cause error messages to be raised.  
  
##  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   System databases cannot be deleted.  
-   For more information, see [DROP DATABASE (Transact-SQL)](../../t-sql/statements/drop-database-transact-sql.md).

## Next steps

 - [CREATE DATABASE &#40;SQL Server Transact-SQL&#41;](../../t-sql/statements/create-database-transact-sql.md)   
 - [ALTER DATABASE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql.md)  
 - [DROP DATABASE (Transact-SQL)](../../t-sql/statements/drop-database-transact-sql.md)
 - [Restore and recovery overview (SQL Server)](../backup-restore/restore-and-recovery-overview-sql-server.md)
  
