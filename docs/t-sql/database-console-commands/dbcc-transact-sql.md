---
title: "DBCC (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "11/14/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "DBCC"
  - "DBCC_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "transactional consistency"
  - "Database Console Command statements"
  - "status information [SQL Server], DBCC statements"
  - "snapshots [SQL Server database snapshots], DBCC statements"
  - "emergency database state [SQL Server]"
  - "TABLOCK"
  - "internal database snapshots [SQL Server]"
  - "reporting DBCC statement progress"
  - "logical consistency of data [SQL Server]"
  - "DBCC statements"
  - "validation statements [SQL Server]"
  - "miscellaneous statements [SQL Server]"
  - "statements [SQL Server], DBCC statements"
  - "DBCC commands [SQL Server]"
  - "result sets [SQL Server], DBCC statements"
  - "maintenance statements [SQL Server]"
  - "physical consistency of data [SQL Server]"
  - "current DBCC statement status"
  - "progress reporting [DBCC statements]"
  - "informational statements [SQL Server]"
ms.assetid: c6da8c04-5b6b-459a-9f76-110c92ca8b29
author: uc-msft
ms.author: umajay
manager: craigg
---
# DBCC (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

The [!INCLUDE[tsql](../../includes/tsql-md.md)] programming language provides DBCC statements that act as Database Console Commands for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].
  
Database Console Command statements are grouped into the following categories.
  
|Command category|Perform|  
|---|---|
|Maintenance|Maintenance tasks on a database, index, or filegroup.|  
|Miscellaneous|Miscellaneous tasks such as enabling trace flags or removing a DLL from memory.|  
|Informational|Tasks that gather and display various types of information.|  
|Validation|Validation operations on a database, table, index, catalog, filegroup, or allocation of database pages.|  
  
DBCC commands take input parameters and return values. All DBCC command parameters can accept both Unicode and DBCS literals.
  
## DBCC Internal Database Snapshot Usage  
The following DBCC commands operate on an internal read-only database snapshot that the [!INCLUDE[ssDE](../../includes/ssde-md.md)] creates. This prevents blocking and concurrency problems when these commands are executed. For more information, see [Database Snapshots &#40;SQL Server&#41;](../../relational-databases/databases/database-snapshots-sql-server.md).
- DBCC CHECKALLOC
- DBCC CHECKCATALOG
- DBCC CHECKDB
- DBCC CHECKFILEGROUP
- DBCC CHECKTABLE

When you execute one of these DBCC commands, the [!INCLUDE[ssDE](../../includes/ssde-md.md)] creates a database snapshot and brings it to a transactionally consistent state. The DBCC command then runs the checks against this snapshot. After the DBCC command is completed, this snapshot is dropped.
  
Sometimes an internal database snapshot is not required or cannot be created. When this occurs, the DBCC command executes against the actual database. If the database is online, the DBCC command uses table-locking to ensure the consistency of the objects that it is checking. This behavior is the same as if the WITH TABLOCK option were specified.
  
An internal database snapshot is not created when a DBCC command is executed:
-   Against **master**, and the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is running in single-user mode.  
-   Against a database other than **master**, but the database has been put in single-user mode by using the ALTER DATABASE statement.  
-   Against a read-only database.  
-   Against a database that has been set in emergency mode by using the ALTER DATABASE statement.  
-   Against **tempdb**. In this case, a database snapshot cannot be created because of internal restrictions.  
-   Using the WITH TABLOCK option. In this case, DBCC honors the request by not creating a database snapshot.  
  
The DBCC commands use table locks instead of the internal database snapshots when the command is executed against the following:
-   A read-only filegroup  
-   An FAT file system  
-   A volume that does not support 'named streams'  
-   A volume that does not support 'alternate streams'  
  
> [!NOTE]  
>  Trying to run DBCC CHECKALLOC, or the equivalent part of DBCC CHECKDB, by using the WITH TABLOCK option requires a database X lock. This database lock cannot be set on **tempdb** or **master** and will probably fail on all other databases.  
  
> [!NOTE]  
>  DBCC CHECKDB fails when it is run against **master** if an internal database snapshot cannot be created.  
  
## Progress Reporting for DBCC Commands  
The **sys.dm_exec_requests** catalog view contains information about the progress and the current phase of execution of the DBCC CHECKDB, CHECKFILEGROUP, and CHECKTABLE commands. The **percent_complete** column indicates the percentage complete of the command, and the **command** column reports the current phase of the execution of the command.
  
The definition of a unit of progress depends on the current phase of execution of the DBCC command. Sometimes progress is reported at the granularity of a database page, in other phases it is reported at the granularity of a single database or allocation repair. The following table describes each phase of execution, and the granularity at which the command reports progress.
  
|Execution phase|Description|Progress reporting granularity|  
|---------------------|-----------------|------------------------------------|  
|DBCC TABLE CHECK|The logical and physical consistency of the objects in the database is checked during this phase.|Progress reported at the database page level.<br /><br /> The progress reporting value is updated for each 1000 database pages that are checked.|  
|DBCC TABLE REPAIR|Database repairs are performed during this phase if REPAIR_FAST, REPAIR_REBUILD, or REPAIR_ALLOW_DATA_LOSS is specified, and there are object errors that must be repaired.|Progress reported at the individual repair level.<br /><br /> The counter is updated for each repair that is completed.|  
|DBCC ALLOC CHECK|Allocation structures in the database are checked during this phase.<br /><br /> Note: DBCC CHECKALLOC performs the same checks.|Progress is not reported|  
|DBCC ALLOC REPAIR|Database repairs are performed during this phase if REPAIR_FAST, REPAIR_REBUILD, or REPAIR_ALLOW_DATA_LOSS is specified, and there are allocation errors that must be repaired.|Progress is not reported.|  
|DBCC SYS CHECK|Database system tables are checked during this phase.|Progress reported at the database page level.<br /><br /> The progress reporting value is updated for every 1000 database pages that are checked.|  
|DBCC SYS REPAIR|Database repairs are performed during this phase if REPAIR_FAST, REPAIR_REBUILD, or REPAIR_ALLOW_DATA_LOSS is specified, and there are system table errors that must be repaired.|Progress reported at the individual repair level.<br /><br /> The counter is updated for each repair that is completed.|  
|DBCC SSB CHECK|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Service Broker objects are checked during this phase.<br /><br /> Note: This phase is not executed when DBCC CHECKTABLE is executed.|Progress is not reported.|  
|DBCC CHECKCATALOG|The consistency of database catalogs are checked during this phase.<br /><br /> Note: This phase is not executed when DBCC CHECKTABLE is executed.|Progress is not reported.|  
|DBCC IVIEW CHECK|The logical consistency of any indexed views present in the database is checked during this phase.|Progress reported at the level of the individual database view that is being checked.|  
  
## Informational Statements  
  
|||  
|-|-|  
|[DBCC INPUTBUFFER](../../t-sql/database-console-commands/dbcc-inputbuffer-transact-sql.md)|[DBCC SHOWCONTIG](../../t-sql/database-console-commands/dbcc-showcontig-transact-sql.md)|  
|[DBCC OPENTRAN](../../t-sql/database-console-commands/dbcc-opentran-transact-sql.md)|[DBCC SQLPERF](../../t-sql/database-console-commands/dbcc-sqlperf-transact-sql.md)|  
|[DBCC OUTPUTBUFFER](../../t-sql/database-console-commands/dbcc-outputbuffer-transact-sql.md)|[DBCC TRACESTATUS](../../t-sql/database-console-commands/dbcc-tracestatus-transact-sql.md)|  
|[DBCC PROCCACHE](../../t-sql/database-console-commands/dbcc-proccache-transact-sql.md)|[DBCC USEROPTIONS](../../t-sql/database-console-commands/dbcc-useroptions-transact-sql.md)|  
|[DBCC SHOW_STATISTICS](../../t-sql/database-console-commands/dbcc-show-statistics-transact-sql.md)||  
  
## Validation Statements  
  
|||  
|-|-|  
|[DBCC CHECKALLOC](../../t-sql/database-console-commands/dbcc-checkalloc-transact-sql.md)|[DBCC CHECKFILEGROUP](../../t-sql/database-console-commands/dbcc-checkfilegroup-transact-sql.md)|  
|[DBCC CHECKCATALOG](../../t-sql/database-console-commands/dbcc-checkcatalog-transact-sql.md)|[DBCC CHECKIDENT](../../t-sql/database-console-commands/dbcc-checkident-transact-sql.md)|  
|[DBCC CHECKCONSTRAINTS](../../t-sql/database-console-commands/dbcc-checkconstraints-transact-sql.md)|[DBCC CHECKTABLE](../../t-sql/database-console-commands/dbcc-checktable-transact-sql.md)|  
|[DBCC CHECKDB](../../t-sql/database-console-commands/dbcc-checkdb-transact-sql.md)||  
  
## Maintenance Statements  
  
|||  
|-|-|  
|[DBCC CLEANTABLE](../../t-sql/database-console-commands/dbcc-cleantable-transact-sql.md)|[DBCC INDEXDEFRAG](../../t-sql/database-console-commands/dbcc-indexdefrag-transact-sql.md)|  
|[DBCC DBREINDEX](../../t-sql/database-console-commands/dbcc-dbreindex-transact-sql.md)|[DBCC SHRINKDATABASE](../../t-sql/database-console-commands/dbcc-shrinkdatabase-transact-sql.md)|  
|[DBCC DROPCLEANBUFFERS](../../t-sql/database-console-commands/dbcc-dropcleanbuffers-transact-sql.md)|[DBCC SHRINKFILE](../../t-sql/database-console-commands/dbcc-shrinkfile-transact-sql.md)|  
|[DBCC FREEPROCCACHE](../../t-sql/database-console-commands/dbcc-freeproccache-transact-sql.md)|[DBCC UPDATEUSAGE](../../t-sql/database-console-commands/dbcc-updateusage-transact-sql.md)|  
  
## Miscellaneous Statements  
  
|||  
|-|-|  
|[DBCC dllname (FREE)](../../t-sql/database-console-commands/dbcc-dllname-free-transact-sql.md)|[DBCC HELP](../../t-sql/database-console-commands/dbcc-help-transact-sql.md)|  
|[DBCC FLUSHAUTHCACHE](../../t-sql/database-console-commands/dbcc-flushauthcache-transact-sql.md)|[DBCC TRACEOFF](../../t-sql/database-console-commands/dbcc-traceoff-transact-sql.md)|  
|[DBCC FREESESSIONCACHE](../../t-sql/database-console-commands/dbcc-freesessioncache-transact-sql.md)|[DBCC TRACEON](../../t-sql/database-console-commands/dbcc-traceon-transact-sql.md)|  
|[DBCC FREESYSTEMCACHE](../../t-sql/database-console-commands/dbcc-freesystemcache-transact-sql.md)|[DBCC CLONEDATABASE](../../t-sql/database-console-commands/dbcc-clonedatabase-transact-sql.md) <br /><br /> **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] SP2 through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].|  
  
  
