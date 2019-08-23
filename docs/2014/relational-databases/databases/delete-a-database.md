---
title: "Delete a Database | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "database removal [SQL Server], SQL Server Management Studio"
  - "removing databases"
  - "deleting databases"
  - "dropping databases"
  - "databases [SQL Server], dropping"
  - "database removal [SQL Server]"
ms.assetid: 1fd8c0f5-03e1-449a-af45-b8cacb479d9c
author: stevestein
ms.author: sstein
manager: craigg
---
# Delete a Database
  This topic describes how to delete a user-defined database in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Limitations and Restrictions](#Restrictions)  
  
     [Prerequisites](#Prerequisites)  
  
     [Recommendations](#Recommendations)  
  
     [Security](#Security)  
  
-   **To delete a database, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
-   **Follow Up:**  [After deleting a database](#FollowUp)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   System databases cannot be deleted.  
  
###  <a name="Prerequisites"></a> Prerequisites  
  
-   Delete any database snapshots that exist on the database. For more information, see [Drop a Database Snapshot &#40;Transact-SQL&#41;](drop-a-database-snapshot-transact-sql.md).  
  
-   If the database is involved in log shipping, remove log shipping.  
  
-   If the database is published for transactional replication, or published or subscribed to merge replication, remove replication from the database.  
  
###  <a name="Recommendations"></a> Recommendations  
  
-   Consider taking a full backup of the database. A deleted database can be re-created only by restoring a backup.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 To execute DROP DATABASE, at a minimum, a user must have CONTROL permission on the database.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To delete a database  
  
1.  In **Object Explorer**, connect to an instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)], and then expand that instance.  
  
2.  Expand **Databases**, right-click the database to delete, and then click **Delete**.  
  
3.  Confirm the correct database is selected, and then click **OK**.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To delete a database  
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  From the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**. The example removes the `Sales` and `NewSales` databases.  
  
```sql  
USE master ;  
GO  
DROP DATABASE Sales, NewSales ;  
GO  
```  
  
##  <a name="FollowUp"></a> Follow Up: After deleting a database  
 Back up the **master** database. If **master** must be restored, any database that has been deleted since the last backup of **master** will still have references in the system catalog views and may cause error messages to be raised.  
  
## See Also  
 [CREATE DATABASE &#40;SQL Server Transact-SQL&#41;](/sql/t-sql/statements/create-database-sql-server-transact-sql)   
 [ALTER DATABASE &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-database-transact-sql)  
  
  
