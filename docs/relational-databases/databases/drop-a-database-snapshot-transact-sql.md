---
title: "Drop a Database Snapshot (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "removing database snapshots"
  - "deleting database snapshots"
  - "database snapshots [SQL Server], deleting"
ms.assetid: ad70ec97-d5fb-41aa-b72a-915e74b61b76
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# Drop a Database Snapshot (Transact-SQL)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  Dropping a database snapshot deletes the database snapshot from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and deletes the sparse files that are used by the snapshot. When you drop a database snapshot, all user connections to it are terminated.  
  
## Security  
  
###  <a name="Permissions"></a> Permissions  
 Any user with DROP DATABASE permissions can drop a database snapshot.  
  
##  <a name="TsqlProcedure"></a> How to Drop a Database Snapshot (Using Transact-SQL)  
 **To drop a database snapshot**  
  
1.  Identify the database snapshot that you want to drop. You can view the snapshots on a database in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. For more information, see [View a Database Snapshot &#40;SQL Server&#41;](../../relational-databases/databases/view-a-database-snapshot-sql-server.md).  
  
2.  Issue a [DROP DATABASE](../../t-sql/statements/drop-database-transact-sql.md) statement, specifying the name of the database snapshot to be dropped. The syntax is as follows:  
  
     DROP DATABASE *database_snapshot_name* [ **,**...*n* ]  
  
     Where *database_snapshot_name* is the name of the database snapshot to be dropped.  
  
####  <a name="TsqlExample"></a> Example (Transact-SQL)  
 This example drops a database snapshot named SalesSnapshot0600, without affecting the source database.  
  
```  
DROP DATABASE SalesSnapshot0600 ;  
```  
  
 Any user connections to SalesSnapshot0600 are terminated, and all of the NTFS file system sparse files used by the snapshot are deleted.  
  
> [!NOTE]  
>  For information about the use of sparse files by database snapshots, see [Database Snapshots &#40;SQL Server&#41;](../../relational-databases/databases/database-snapshots-sql-server.md).  
  
##  <a name="RelatedTasks"></a> Related Tasks  
  
-   [Create a Database Snapshot &#40;Transact-SQL&#41;](../../relational-databases/databases/create-a-database-snapshot-transact-sql.md)  
  
-   [View a Database Snapshot &#40;SQL Server&#41;](../../relational-databases/databases/view-a-database-snapshot-sql-server.md)  
  
-   [Revert a Database to a Database Snapshot](../../relational-databases/databases/revert-a-database-to-a-database-snapshot.md)  
  
  
## See Also  
 [DROP DATABASE &#40;Transact-SQL&#41;](../../t-sql/statements/drop-database-transact-sql.md)   
 [Database Snapshots &#40;SQL Server&#41;](../../relational-databases/databases/database-snapshots-sql-server.md)  
  
  
