---
title: "Features not supported - in-memory OLTP"
description: Learn about SQL Server features that are not supported for memory-optimized objects. View features for In-Memory OLTP that have become supported.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 02/21/2020
ms.service: sql
ms.subservice: in-memory-oltp
ms.topic: conceptual
ms.assetid: c39f03a7-e223-4fd7-bd30-142e28f51654
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Unsupported SQL Server Features for In-Memory OLTP
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

This topic discusses [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] features that aren't supported for use with memory-optimized objects. Plus, the final section lists features that were unsupported for In-Memory OLTP, but later became supported.
  
## [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Features Not Supported for In-Memory OLTP  

The following [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] features aren't supported on a database that has memory-optimized objects (including memory-optimized data filegroup).  

  
|Unsupported Feature|Feature Description|  
|-------------------------|-------------------------|  
|Data compression for memory-optimized tables.|You can use the data compression feature to help compress the data inside a database, and to help reduce the size of the database. For more information, see [Data Compression](../../relational-databases/data-compression/data-compression.md).|  
|Partitioning of memory-optimized tables and HASH indexes, and of nonclustered indexes.|The data of partitioned tables and indexes is divided into units that can be spread across more than one filegroup in a database. For more information, see [Partitioned Tables and Indexes](../../relational-databases/partitions/partitioned-tables-and-indexes.md).|  
| Replication | Replication configurations, other than transactional replication to memory-optimized tables on subscribers, are incompatible with tables or views referencing memory-optimized tables.<br /><br />If there is a memory-optimized filegroup, replication using sync_mode='database snapshot' is not supported.<br /><br />For more information, see [Replication to Memory-Optimized Table Subscribers](../../relational-databases/replication/replication-to-memory-optimized-table-subscribers.md).|
|Mirroring|Database mirroring is not supported for databases with a MEMORY_OPTIMIZED_DATA filegroup. For more information about mirroring, see [Database Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/database-mirroring-sql-server.md).|  
|Rebuild log|Rebuilding the log, either through attach or ALTER DATABASE, is not supported for databases with a MEMORY_OPTIMIZED_DATA filegroup.|  
|Linked Server|You cannot access linked servers in the same query or transaction as memory-optimized tables. For more information, see [Linked Servers &#40;Database Engine&#41;](../../relational-databases/linked-servers/linked-servers-database-engine.md).|  
|Bulk logging|Regardless of the recovery model of the database, all operations on durable memory-optimized tables are always fully logged.|  
|Minimal logging|Minimal logging is not supported for memory-optimized tables. For more information about minimal logging, see [The Transaction Log &#40;SQL Server&#41;](../../relational-databases/logs/the-transaction-log-sql-server.md) and [Prerequisites for Minimal Logging in Bulk Import](../../relational-databases/import-export/prerequisites-for-minimal-logging-in-bulk-import.md).|  
|Change tracking|Change tracking is not supported for memory optimized tables. |
| DDL triggers | Database-level triggers and server-level DDL triggers aren't supported with In-Memory OLTP tables or with natively compiled modules. |  
| Change Data Capture (CDC) | SQL Server 2017 CU15 and higher support enabling CDC on a database that has memory optimized tables. This is only applicable to the database and any on-disk tables in the database. In earlier versions of SQL Server, CDC cannot be used with a database that has memory-optimized tables, because internally CDC uses a DDL trigger for DROP TABLE. |  
| Fiber mode | Fiber mode is not supported with memory-optimized tables:<br /><br />If fiber mode is active, you cannot create databases with memory-optimized filegroups, nor can you add memory-optimized filegroups to existing databases.<br /><br />You can enable fiber mode if there are databases with memory-optimized filegroups. However, enabling fiber mode requires a server restart. In that situation, databases with memory-optimized filegroups would fail to recover. Then you would see an error message suggesting that you disable fiber mode to use databases with memory-optimized filegroups.<br /><br />If fiber mode is active, attaching and restoring a database that has a memory-optimized filegroup fails. The databases would be marked as suspect.<br /><br />For more information, see [lightweight pooling Server Configuration Option](../../database-engine/configure-windows/lightweight-pooling-server-configuration-option.md). |  
|Service Broker limitation|Cannot access a queue from a natively compiled stored procedure.<br /><br /> Cannot access a queue in a remote database in a transaction that accesses memory-optimized tables.|  
|Replication on subscribers|Transactional replication to memory-optimized tables on subscribers is supported, but with some restrictions. For more information, see [Replication to Memory-Optimized Table Subscribers](../../relational-databases/replication/replication-to-memory-optimized-table-subscribers.md)|  

#### Cross-database queries and transactions

With a few exceptions, cross-database transactions aren't supported. The following table describes which cases are supported, and the corresponding restrictions. (See also, [Cross-Database Queries](../../relational-databases/in-memory-oltp/cross-database-queries.md).)  


|Databases|Allowed|Description|  
|---------------|-------------|-----------------|  
| User databases, **model**, and **msdb**. | No | In most cases, cross-database queries and transactions are *not* supported.<br /><br />A query cannot access other databases if the query uses either a memory-optimized table or a natively compiled stored procedure. This restriction applies to transactions and queries.<br /><br />The exceptions are the system databases **tempdb** and **master**. Here the **master** database is available for read-only access. |
| **Resource** database, **tempdb** | Yes | In a transaction that touches In-Memory OLTP objects, the **Resource** and **tempdb** system databases can be used without added restriction.

## Scenarios Not Supported  
  
- Accessing memory-optimized tables by using the context connection from inside CLR stored procedures.  
  
- Keyset and dynamic cursors on queries accessing memory-optimized tables. These cursors are degraded to static and read-only.  
  
- Using **MERGE INTO** _target_, where *target* is a memory-optimized table, is unsupported.
    - **MERGE USING** _source_ is supported for memory-optimized tables.  
  
- The ROWVERSION (TIMESTAMP) data type is not supported. For more information, see [FROM &#40;Transact-SQL&#41;](../../t-sql/queries/from-transact-sql.md).
  
- Auto-close is not supported with databases that have a MEMORY_OPTIMIZED_DATA filegroup  

- Transactional DDL, such as CREATE/ALTER/DROP of In-Memory OLTP objects, is not supported inside user transactions.  
  
- Event notification.  
  
- Policy-based management (PBM).
    - Prevent and log only modes of PBM aren't supported. Existence of such policies on the server may prevent In-Memory OLTP DDL from executing successfully. On-demand and on-schedule modes are supported.  

- Database containment ([Contained Databases](../../relational-databases/databases/contained-databases.md)) is not supported with In-Memory OLTP.
    - Contained database authentication is supported. However, all In-Memory OLTP objects are marked as breaking containment in the dynamic management view (DMV) **dm_db_uncontained_entities**.

## Recently added supports

Sometimes a newer release of SQL Server adds support for a feature that was previously not supported. This section lists features that used to be unsupported for In-Memory OLTP, but that later became supported for In-Memory OLTP.

In the following table, _version_ values such as `(15.x)` refer to the value that is returned by the Transact-SQL statement `SELECT @@Version;`.

| Feature name | Version of SQL Server | Comments |
| :----------- | :-------------------- | :------- |
| Database snapshots | 2019 (15.x) | Database snapshots are now supported for databases that have a MEMORY_OPTIMIZED_DATA filegroup. |

## See Also

- [SQL Server Support for In-Memory OLTP](./transact-sql-support-for-in-memory-oltp.md)
