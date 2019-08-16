---
title: "Remove Defunct Filegroups (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: backup-restore
ms.topic: conceptual
helpviewer_keywords: 
  - "piecemeal restores [SQL Server], defunct filegroups"
  - "defunct filegroups"
  - "restoring filegroups [SQL Server]"
  - "deferred transactions"
  - "filegroups [SQL Server], defunct"
  - "unrestored filegroups"
ms.assetid: 055f9c6a-5c18-4942-98e7-ec918f0ff975
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Remove Defunct Filegroups (SQL Server)
  This topic describes how to remove defunct filegroups in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Limitations and Restrictions](#Restrictions)  
  
-   [Recommendations](#Recommendations)  
  
     [Security](#Security)  
  
-   **To remove defunct filegroups, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   This topic is relevant for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] databases that contain multiple files or filegroups; and, under the simple model, only for read-only filegroups.  
  
-   All files in a filegroup become defunct when an offline filegroup is removed.  
  
###  <a name="Recommendations"></a> Recommendations  
  
-   If an unrestored filegroup will never have to be restored, you can make the filegroup *defunct* by removing it from the database. The defunct filegroup can never be restored to this database, but its metadata remains. After the filegroup is defunct, the database can be restarted, and recovery will make the database consistent across the restored filegroups.  
  
     For example, making a filegroup defunct is an option for resolving deferred transactions that were caused by an offline filegroup that you no longer want in the database. Transactions that were deferred because the filegroup was offline are moved out of the deferred state after the filegroup becomes defunct. For more information, see [Deferred Transactions &#40;SQL Server&#41;](deferred-transactions-sql-server.md).  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Requires ALTER permission on the database.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To remove defunct filegroups  
  
1.  In **Object Explorer**, connect to an instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] and then expand that instance.  
  
2.  Expand **Databases**, right-click the database from which to delete the file, and then click **Properties**.  
  
3.  Select the **Files** page.  
  
4.  In the **Database files** grid, select the files to delete, click **Remove**, and then click **OK**.  
  
5.  Select the **Filegroups** page.  
  
6.  In the **Rows** grid, select the filegroup to delete, click **Remove**, and then click **OK**.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To remove defunct filegroups  
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  From the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**. (**Note:** This example assumes that the files and filegroup already exist. To create these objects, see example B in the [ALTER DATABASE File and Filegroup Options](/sql/t-sql/statements/alter-database-transact-sql-file-and-filegroup-options) topic.) The first example removes the `test1dat3` and `test1dat4` files from the defunct filegroup by using the `ALTER DATABASE` statement with the `REMOVE FILE` clause. The second example removes the defunct filegroup `Test1FG1`by using the `REMOVE FILEGROUP` clause.  
  
```sql  
USE master;  
GO  
ALTER DATABASE AdventureWorks2012  
REMOVE FILE test1dat3 ;  
ALTER DATABASE AdventureWorks2012  
REMOVE FILE test1dat4 ;  
GO  
  
```  
  
```sql  
USE master;  
GO  
ALTER DATABASE AdventureWorks2012  
REMOVE FILEGROUP Test1FG1 ;  
GO  
  
```  
  
## See Also  
 [ALTER DATABASE File and Filegroup Options &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-database-transact-sql-file-and-filegroup-options)   
 [Deferred Transactions &#40;SQL Server&#41;](deferred-transactions-sql-server.md)   
 [File Restores &#40;Full Recovery Model&#41;](file-restores-full-recovery-model.md)   
 [File Restores &#40;Simple Recovery Model&#41;](file-restores-simple-recovery-model.md)   
 [Online Restore &#40;SQL Server&#41;](online-restore-sql-server.md)   
 [Restore Pages &#40;SQL Server&#41;](restore-pages-sql-server.md)   
 [Piecemeal Restores &#40;SQL Server&#41;](piecemeal-restores-sql-server.md)  
  
  
