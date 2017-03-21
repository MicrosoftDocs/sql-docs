---
title: "Scalability | Microsoft Docs"
ms.custom: ""
ms.date: "08/27/2015"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine-imoltp"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: a4891c57-56bb-49f4-9bb5-f11b745279e5
caps.latest.revision: 6
author: "sabotta"
ms.author: "carlasab"
manager: "jhubbard"
---
# Scalability
  SQL Server 2016 contains scalability enhancements to the on-disk storage for memory-optimized tables.  
  
-   **Multiple threads to persist memory-optimized tables**  
  
     In the previous release of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], there was a single offline checkpoint thread that scanned the transaction log for changes to memory-optimized tables and persisted them in checkpoint files (such as data and delta files). With larger number of COREs, the single offline checkpoint thread could fall behind.  
  
     In [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)], there are multiple concurrent threads responsible to persist changes to memory-optimized tables.  
  
-   **Multi-threaded recovery**  
  
     In the previous release of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the log apply as part of recovery operation was single threaded. In [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)], the log apply is multi-threaded.  
  
-   **MERGE Operation**  
  
     The MERGE operation is now multi-threaded.  
  
-   **Dynamic management views**  
  
     [sys.dm_db_xtp_checkpoint_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-xtp-checkpoint-stats-transact-sql.md) and [sys.dm_db_xtp_checkpoint_files &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-xtp-checkpoint-files-transact-sql.md)have been changed significantly.  
  
 Manual Merge has been disabled as multi-threaded merge is expected to keep up with the load.  
  
 The In-memory OLTP engine continues to use memory-optimized filegroup based on FILESTREAM, but the individual files in the filegroup are decoupled from FILESTREAM. These files are fully managed (such as for create, drop, and garbage collection) by the In-Memory OLTP engine. [DBCC SHRINKFILE &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-shrinkfile-transact-sql.md) is not supported.  
  
  
