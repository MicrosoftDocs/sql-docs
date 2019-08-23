---
title: "Supported SQL Server Features | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: in-memory-oltp
ms.topic: conceptual
ms.assetid: c39f03a7-e223-4fd7-bd30-142e28f51654
author: MightyPen
ms.author: genemi
manager: craigg
---
# Supported SQL Server Features
  This topic discusses [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] features that are or not supported for use with memory-optimized objects.  
  
## [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Features Supported for In-Memory OLTP  
 The following [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] features are supported on a database that has memory-optimized objects, including memory-optimized filegroup.  
  
 For information about supported data types, see [Supported Data Types](supported-data-types-for-in-memory-oltp.md).  
  
-   Options and operations supported on memory-optimized tables. For more information, see [CREATE TABLE &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-table-transact-sql).  
  
-   Options and operations supported on natively compiled stored procedures. For more information, see [CREATE PROCEDURE &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-procedure-transact-sql).  
  
-   Ability to access memory-optimized tables using interpreted [!INCLUDE[tsql](../../../includes/tsql-md.md)]. Interpreted [!INCLUDE[tsql](../../../includes/tsql-md.md)] provides surface area equivalent to accessing tables that are not memory optimized using stored procedures that are not natively compiled and using [!INCLUDE[tsql](../../../includes/tsql-md.md)]. For more information, see [Accessing Memory-Optimized Tables Using Interpreted Transact-SQL](accessing-memory-optimized-tables-using-interpreted-transact-sql.md).  
  
-   Multi-versioning and optimistic concurrency control. For more information, see [Transaction Isolation Levels](../../database-engine/transaction-isolation-levels.md).  
  
-   Backup and Restore of a database that contains memory-optimized data filegroup. For more information, see [Back Up and Restore of SQL Server Databases](../backup-restore/back-up-and-restore-of-sql-server-databases.md).  
  
-   Catalog views, dynamic management views, and extended events for supportability. For more information, see [System Views, Stored Procedures, DMVs and Wait Types for In-Memory OLTP](../../database-engine/system-views-stored-procedures-dmvs-and-wait-types-for-in-memory-oltp.md).  
  
-   [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Management Objects. For more information, see [SQL Server Management Objects Support for In-Memory OLTP](sql-server-management-objects-support-for-in-memory-oltp.md).  
  
-   [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. For more information, see [SQL Server Management Studio Support for In-Memory OLTP](sql-server-management-studio-support-for-in-memory-oltp.md).  
  
-   [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] PowerShell. For more information, see [SQL Server PowerShell Overview](https://msdn.microsoft.com/library/cc281954\(SQL.105\).aspx).  
  
-   Import and Export Bulk Data by using the bcp utility. For more information, see [Import and Export Bulk Data by Using the bcp Utility &#40;SQL Server&#41;](../import-export/import-and-export-bulk-data-by-using-the-bcp-utility-sql-server.md).  
  
-   Crash recovery.  
  
-   Multiple containers in a memory-optimized data filegroup to store In-Memory OLTP objects and reduce recovery time objective (RTO).  
  
-   [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] transaction log blocks calculate checksum and validate.  
  
-   The new SNAPSHOT table hint. For more information, see [Table Hints &#40;Transact-SQL&#41;](/sql/t-sql/queries/hints-transact-sql-table).  
  
-   DB COMPAT level.  
  
-   Partially contained database. Contained database authentication is supported. However, all In-Memory OLTP objects are marked as breaking containment in the DMV dm_db_uncontained_entities.  
  
-   Service broker, with limitations. Cannot access a queue from a natively compiled stored procedure. Cannot access a queue in a remote database in a transaction that accesses memory-optimized tables.  
  
-   Failover Clustering: As part of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] AlwaysOn offering, AlwaysOn Failover Cluster Instances leverages Windows Server Failover Clustering (WSFC) functionality to provide local high availability through redundancy at the server-instance level-a failover cluster instance (FCI). For more information, see [AlwaysOn Failover Cluster Instances (SQL Server)](../../sql-server/failover-clusters/windows/always-on-failover-cluster-instances-sql-server.md).  
  
-   Integration with AlwaysOn: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides several options for creating high availability for a server or database, including AlwaysOn. For more information, see [High Availability Solutions &#40;SQL Server&#41;](../../sql-server/failover-clusters/high-availability-solutions-sql-server.md).  
  
-   Log shipping: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Log shipping allows you to automatically send transaction log backups from a primary database on a primary server instance to one or more secondary databases on separate secondary server instances. For more information, see [About Log Shipping &#40;SQL Server&#41;](../../database-engine/log-shipping/about-log-shipping-sql-server.md).  
  
-   Transactional replication to memory-optimized tables on subscribers is supported with some restrictions. For more information, see [Replication to Memory-Optimized Table Subscribers](../replication/replication-to-memory-optimized-table-subscribers.md).  
  
-   Resource Governor: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Resource Governor is a feature that you can use to manage [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] workload and system resource consumption. Resource Governor enables you to specify limits on the amount of CPU, physical IO, and memory that incoming application requests can use. For more information, see [Managing Memory for In-Memory OLTP](../../database-engine/managing-memory-for-in-memory-oltp.md) and [Resource Governor](../resource-governor/resource-governor.md).  
  
-   In-Memory OLTP has restrictions on supported code pages for (var)char columns in memory-optimized tables and supported collations used in indexes and natively compiled stored procedures. For more information, see [Collations and Code Pages](../../database-engine/collations-and-code-pages.md).  
  
-   BACPAC support.  
  
## [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Features Not Supported for In-Memory OLTP  
 The following [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] features are not supported on a database that has memory-optimized objects (including memory-optimized data filegroup).  
  
|Unsupported Feature|Feature Description|  
|-------------------------|-------------------------|  
|Data compression for memory-optimized tables.|You can use the data compression feature to help compress the data inside a database, and to help reduce the size of the database. For more information, see [Data Compression](../data-compression/data-compression.md).|  
|Partitioning of memory-optimized tables and HASH indexes.|The data of partitioned tables and indexes is divided into units that can be spread across more than one filegroup in a database. For more information, see [Partitioned Tables and Indexes](../partitions/partitioned-tables-and-indexes.md).|  
|Transparent Data Encryption (TDE) on the memory-optimized data filegroup of a database.|Transparent data encryption (TDE) performs real-time I/O encryption and decryption of the data and log files. For more information, see [Transparent Data Encryption &#40;TDE&#41;](../security/encryption/transparent-data-encryption.md).<br /><br /> TDE can be enabled on a database that has In-Memory OLTP objects. In-Memory OLTP log records are encrypted if TDE is enabled. The checkpoint files for durable tables are not encrypted, even if TDE is enabled on the database.|  
|Replication|Replication configurations other than transactional replication to memory-optimized tables on subscribers are incompatible with tables or views referencing memory-optimized tables. Replication using sync_mode='database snapshot' is not supported if there is a memory-optimized filegroup. For more information, see [Replication to Memory-Optimized Table Subscribers](../replication/replication-to-memory-optimized-table-subscribers.md).|  
|Multiple Active Result Sets (MARS)|Multiple Active Result Sets (MARS) is not supported with memory-optimized tables. This error can also indicate linked server use. Linked server can use MARS. Linked servers are not supported with memory-optimized tables. Instead, connect directly to the server and database that hosts the memory-optimized tables.|  
|Mirroring|Database mirroring is a solution for increasing the availability of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database. For more information, see [Database Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/database-mirroring-sql-server.md).|  
|Rebuild log|Rebuilding the log, either through attach or ALTER DATABASE, is not supported for databases with a MEMORY_OPTIMIZED_DATA filegroup.|  
|Linked Server|For more information, see [Linked Servers &#40;Database Engine&#41;](../linked-servers/linked-servers-database-engine.md).|  
|Bulk logging|Regardless of the recovery model of the database, all operations on durable memory-optimized tables are always fully logged.|  
|Minimal logging|Minimal logging is not supported for memory-optimized tables. For more information about minimal logging, see [The Transaction Log &#40;SQL Server&#41;](../logs/the-transaction-log-sql-server.md) and [Prerequisites for Minimal Logging in Bulk Import](../import-export/prerequisites-for-minimal-logging-in-bulk-import.md).|  
|Change tracking|Change tracking can be enabled on a database with In-Memory OLTP objects. However, changes in memory-optimized tables are not tracked.|  
|DDL triggers|Both database-level and server-level DDL triggers are not supported with In-Memory OLTP tables and natively compiled stored procedures.|  
|Change Data Capture (CDC)|CDC should not be enabled on a database that has In-Memory OLTP objects, as it prevents certain operations such as DROP.|  
|Database Containment|Database containment is not supported in a database that has natively-compiled stored procedures and memory-optimized tables. For more information, see [Contained Databases](../databases/contained-databases.md)|  
|Context Connections|Accessing memory-optimized tables using the context connection from inside CLR stored procedures is not supported.|  
|Cursors|Keyset and dynamic cursors on queries accessing memory-optimized tables. These queries are degraded to static becoming read-only.|  
|TABLESTAMP|TABLESTAMP is not supported. See [FROM &#40;Transact-SQL&#41;](/sql/t-sql/queries/from-transact-sql) for more information.|  
|AUTO_CLOSE|AUTO_CLOSE is not supported. For more information, see [Set the AUTO_CLOSE Database Option to OFF](../policy-based-management/set-the-auto-close-database-option-to-off.md).|  
|Database Snapshots|Database Snapshots are not supported. For more information, see [Database Snapshots &#40;SQL Server&#41;](../databases/database-snapshots-sql-server.md).|  
|Transactional DDL|Transactional DDL is not supported in In-Memory OLTP.|  
|Event Notifications|Event notifications are not supported. For more information, see [Event Notifications](../service-broker/event-notifications.md).|  
|Fiber mode|Fiber mode is not supported with In-Memory OLTP.|  
|Policy-based management (PBM).|Prevent and log only modes of PBM are not supported. Existence of such policies on the server may prevent In-Memory OLTP DDL from executing successfully. On demand and on schedule modes are supported.|  
|DACFX deploy/extract|DAC Framework deploy/extract is not supported in In-Memory OLTP.|  
  
 With a few exceptions, cross-database transactions are not supported. The following table describes which cases are supported, and the corresponding restrictions. (See also, [Cross-Database Queries](cross-database-queries.md).)  
  
|Databases|Allowed|Description|  
|---------------|-------------|-----------------|  
|User databases, model, and msdb|No|Cross-database queries and transactions are not supported.<br /><br /> Queries and transactions that access memory-optimized tables or natively compiled stored procedures cannot access other databases, with the exception of the system databases master (read-only access) and tempdb.|  
|Resource database, and tempdb|Yes|There are no restrictions on cross-database transactions that, besides a single user database, use only resource database and tempdb.|  
|master|read-only|Cross-database transactions that touch In-Memory OLTP and the master database fail to commit if it includes any writes to the master database. Cross-database transactions that only read from master and use only one user database are allowed.|  
  
## See Also  
 [SQL Server Support for In-Memory OLTP](sql-server-support-for-in-memory-oltp.md)  
  
  
