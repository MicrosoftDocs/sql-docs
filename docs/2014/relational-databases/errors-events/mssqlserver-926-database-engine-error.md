---
title: "MSSQLSERVER_926 | Microsoft Docs"
ms.custom: ""
ms.date: "03/08/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "926 (Database Engine error)"
ms.assetid: 57e01668-883b-4be4-84a8-a111caaf0486
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_926
    
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|926|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|DB_SUSPECT|  
|Message Text|Database '%.*ls' cannot be opened. It has been marked SUSPECT by recovery. See the SQL Server errorlog for more information.|  
  
## Explanation  
 The database is marked as suspect because it failed the recovery process that brings a database to a consistent transactional state. This can occur during the following operations:  
  
-   Starting up an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
-   Attaching a database.  
  
-   Using the RESTORE database or RESTORE LOG procedures.  
  
## User Action  
 Inspect the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log and determine the cause of the error. If [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] has been restarted since the failed recovery, look at previous [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error logs to see the reason why recovery failed.  
  
 If the recovery failed because of a persistent I/O error, a torn page, or other possible hardware problem, resolve the underlying hardware problem and restore the database by using a backup. If no backups are available, consider the repair options of DBCC CHECKDB.  
  
 If you are unable to resolve this problem, contact your primary support provider. Have the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log available for review.  
  
## See Also  
 [Back Up and Restore of SQL Server Databases](../backup-restore/back-up-and-restore-of-sql-server-databases.md)   
 [RESTORE &#40;Transact-SQL&#41;](/sql/t-sql/statements/restore-statements-transact-sql)   
 [sys.sysdatabases &#40;Transact-SQL&#41;](/sql/relational-databases/system-compatibility-views/sys-sysdatabases-transact-sql)   
 [Database Detach and Attach &#40;SQL Server&#41;](../../relational-databases/databases/database-detach-and-attach-sql-server.md)  
  
  
