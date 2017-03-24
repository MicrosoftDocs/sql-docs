---
title: "Unsupported SQL Server Features for In-Memory OLTP | Microsoft Docs"
ms.custom: ""
ms.date: "10/17/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine-imoltp"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: c39f03a7-e223-4fd7-bd30-142e28f51654
caps.latest.revision: 55
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# Unsupported SQL Server Features for In-Memory OLTP
[!INCLUDE[tsql-appliesto-ss2016-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-ss2016-asdb-xxxx-xxx-md.md)]

  This topic discusses [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] features that are not supported for use with memory-optimized objects.  
  
## [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Features Not Supported for In-Memory OLTP  
 The following [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] features are not supported on a database that has memory-optimized objects (including memory-optimized data filegroup).  
  
|Unsupported Feature|Feature Description|  
|-------------------------|-------------------------|  
|Data compression for memory-optimized tables.|You can use the data compression feature to help compress the data inside a database, and to help reduce the size of the database. For more information, see [Data Compression](../../relational-databases/data-compression/data-compression.md).|  
|Partitioning of memory-optimized tables and HASH indexes, as well as nonclustered indexes.|The data of partitioned tables and indexes is divided into units that can be spread across more than one filegroup in a database. For more information, see [Partitioned Tables and Indexes](../../relational-databases/partitions/partitioned-tables-and-indexes.md).|  
|Replication|Replication configurations other than transactional replication to memory-optimized tables on subscribers are incompatible with tables or views referencing memory-optimized tables. Replication using sync_mode=’database snapshot’ is not supported if there is a memory-optimized filegroup. For more information, see [Replication to Memory-Optimized Table Subscribers](../../relational-databases/replication/replication-to-memory-optimized-table-subscribers.md).|  
|Mirroring|Database mirroring is not supported for databases with  a MEMORY_OPTIMIZED_DATA filegroup. For more information about mirroring, see [Database Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/database-mirroring-sql-server.md).|  
|Rebuild log|Rebuilding the log, either through attach or ALTER DATABASE, is not supported for databases with  a MEMORY_OPTIMIZED_DATA filegroup.|  
|Linked Server|You cannot access linked servers in the same query or transaction as memory-optimized tables. For more information, see [Linked Servers &#40;Database Engine&#41;](../../relational-databases/linked-servers/linked-servers-database-engine.md).|  
|Bulk logging|Regardless of the recovery model of the database, all operations on durable memory-optimized tables are always fully logged.|  
|Minimal logging|Minimal logging is not supported for memory-optimized tables. For more information about minimal logging, see [The Transaction Log &#40;SQL Server&#41;](../../relational-databases/logs/the-transaction-log-sql-server.md) and [Prerequisites for Minimal Logging in Bulk Import](../../relational-databases/import-export/prerequisites-for-minimal-logging-in-bulk-import.md).|  
|Change tracking|Change tracking can be enabled on a database with In-Memory OLTP objects. However, changes in memory-optimized tables are not tracked.|  
|DDL triggers|Both database-level and server-level DDL triggers are not supported with In-Memory OLTP tables and natively compiled modules.|  
|Change Data Capture (CDC)|CDC cannot be used with a database that has memory-optimized tables, as it uses a DDL trigger for DROP TABLE under the hood.|  
|Fiber mode|Fiber mode is not supported with memory-optimized tables:<br /><br /> If fiber mode is active, you cannot create databases with memory-optimized filegroups or add memory-optimized filegroups to existing databases.<br /><br /> You can enable fiber mode if there are databases with memory-optimized filegroups. However, enabling fiber mode requires a server restart. In that situation, databases with memory-optimized filegroups would fail to recover and you will see an error message suggesting that you disable fiber mode to use databases with memory-optimized filegroups.<br /><br /> Attaching and restoring databases with memory-optimized filegroups will fail if fiber mode is active. The databases will be marked as suspect.<br /><br /> <br /><br /> For more information, see [lightweight pooling Server Configuration Option](../../database-engine/configure-windows/lightweight-pooling-server-configuration-option.md).|  
|Service Broker limitation|Cannot access a queue from a natively compiled stored procedure.<br /><br /> Cannot access a queue in a remote database in a transaction that accesses memory-optimized tables.|  
|Replication on subscribers|Transactional replication to memory-optimized tables on subscribers is supported but with some restrictions. For more information, see [Replication to Memory-Optimized Table Subscribers](../../relational-databases/replication/replication-to-memory-optimized-table-subscribers.md)|  
  
 With a few exceptions, cross-database transactions are not supported. The following table describes which cases are supported, and the corresponding restrictions. (See also, [Cross-Database Queries](../../relational-databases/in-memory-oltp/cross-database-queries.md).)  
  
|Databases|Allowed|Description|  
|---------------|-------------|-----------------|  
|User databases, model, and msdb.|No|Cross-database queries and transactions are not supported.<br /><br /> Queries and transactions that access memory-optimized tables or natively compiled stored procedures cannot access other databases, with the exception of the system databases master (read-only access) and tempdb.|  
|Resource database, tempdb|Yes|There are no restrictions on cross-database transactions that, besides a single user database, use only resource database and tempdb.|  
|master|read-only|Cross-database transactions that touch In-Memory OLTP and the master database fail to commit if it includes any writes to the master database. Cross-database transactions that only read from master and use only one user database are allowed.|  
  
## Scenarios Not Supported  
  
-   Database containment ([Contained Databases](../../relational-databases/databases/contained-databases.md)) is not supported with In-Memory OLTP. Contained database authentication is supported. However, all In-Memory OLTP objects are marked as breaking containment in the DMV dm_db_uncontained_entities.  
  
-   Accessing memory-optimized tables using the context connection from inside CLR stored procedures.  
  
-   Keyset and dynamic cursors on queries accessing memory-optimized tables. These cursors are degraded to static and read-only.  
  
-   Using **MERGE INTO***target* with *target* is a memory-optimized table. **MERGE USING***source* is supported for memory-optimized tables.  
  
-   The ROWVERSION (TIMESTAMP) data type is not supported. See [FROM &#40;Transact-SQL&#41;](../../t-sql/queries/from-transact-sql.md) for more information.  
  
-   Auto-close is not supported with databases that have a MEMORY_OPTIMIZED_DATA filegroup  
  
-   Database snapshots as not supported for databases that have a MEMORY_OPTIMIZED_DATA filegroup.  
  
-   Transactional DDL. CREATE/ALTER/DROP of In-Memory OLTP objects is not supported inside user transactions.  
  
-   Event notification.  
  
-   Policy-based management (PBM). Prevent and log only modes of PBM are not supported. Existence of such policies on the server may prevent In-Memory OLTP DDL from executing successfully. On demand and on schedule modes are supported.  
  
## See Also  
 [SQL Server Support for In-Memory OLTP](../../relational-databases/in-memory-oltp/sql-server-support-for-in-memory-oltp.md)  
  
  
