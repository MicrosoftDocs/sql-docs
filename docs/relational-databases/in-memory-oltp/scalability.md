---
title: "Scalability"
description: Learn about enhancements to scalability to on-disk storage for memory-optimized tables in SQL Server, such as using multiple threads to persist tables.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "08/27/2015"
ms.service: sql
ms.subservice: in-memory-oltp
ms.topic: conceptual
ms.assetid: a4891c57-56bb-49f4-9bb5-f11b745279e5
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Scalability
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]
[!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] contains scalability enhancements to the on-disk storage for memory-optimized tables. 

## Multiple threads to persist memory-optimized tables  
  
[!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] had a single offline checkpoint thread that scanned the transaction log for changes to memory-optimized tables and persisted them in checkpoint files (such as data and delta files). In machines with a larger number of cores, the single offline checkpoint thread could fall behind.  
  
Starting with [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)], there are multiple concurrent threads responsible to persist changes to memory-optimized tables.  
  
## Multi-threaded recovery
In the previous release of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the log apply as part of recovery operation was single threaded. Starting with [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)], the log apply is multi-threaded.  
  
## MERGE Operation  
The MERGE operation is now multi-threaded.  
   
> [!NOTE]
> Manual Merge has been disabled as multi-threaded merge is expected to keep up with the load. 

## Dynamic management views  
The DMVs [sys.dm_db_xtp_checkpoint_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-xtp-checkpoint-stats-transact-sql.md) and [sys.dm_db_xtp_checkpoint_files &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-xtp-checkpoint-files-transact-sql.md) have been changed significantly.  

## Storage management
The In-memory OLTP engine continues to use memory-optimized filegroup based on FILESTREAM, but the individual files in the filegroup are decoupled from FILESTREAM. These files are fully managed (such as for create, drop, and garbage collection) by the In-Memory OLTP engine. 

> [!NOTE]
> [DBCC SHRINKFILE &#40;Transact-SQL&#41;](../../t-sql/database-console-commands/dbcc-shrinkfile-transact-sql.md) is not supported.  
  
## See Also   
[Creating and Managing Storage for Memory-Optimized Objects](../../relational-databases/in-memory-oltp/creating-and-managing-storage-for-memory-optimized-objects.md)     
[Database Files and Filegroups](../../relational-databases/databases/database-files-and-filegroups.md)    
[ALTER DATABASE File and Filegroup Options (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql-file-and-filegroup-options.md)    
