---
title: "What&#39;s New (Database Engine) | Microsoft Docs"
ms.custom: ""
ms.date: "06/22/2016"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: release-landing
ms.topic: conceptual
helpviewer_keywords: 
  - "what's new [SQL Server Database Engine]"
  - "Database Engine [SQL Server], what's new"
ms.assetid: 8f625d5a-763c-4440-97b8-4b823a6e2439
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# What&#39;s New (Database Engine)
  This latest release of the [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] introduces new features and enhancements that increase the power and productivity of architects, developers, and administrators who design, develop, and maintain data storage systems. These are the areas in which the [!INCLUDE[ssDE](../includes/ssde-md.md)] has been enhanced.  
  
##  <a name="Feature"></a> Database Engine Feature Enhancements  
  
###  <a name="MemoryOpt"></a> Memory-Optimized Tables  
 In-Memory OLTP is a memory-optimized database engine integrated into the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] engine. In-Memory OLTP is optimized for OLTP. For more information, see [In-Memory OLTP &#40;In-Memory Optimization&#41;](../relational-databases/in-memory-oltp/in-memory-oltp-in-memory-optimization.md).  
 
  
###  <a name="DataFiles"></a> SQL Server Data Files in Windows Azure  
 [SQL Server Data Files in Windows Azure](../relational-databases/databases/sql-server-data-files-in-microsoft-azure.md) enables native support for [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] database files stored as Windows Azure Blobs. This feature allows you to create a database in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] running in on-premises or in a virtual machine in Windows Azure with a dedicated storage location for your data in Windows Azure Blob Storage.  
  
  
###  <a name="AzureVM"></a> Host a SQL Server Database in a Windows Azure Virtual Machine  
 Use the [Deploy a SQL Server Database to a Windows Azure Virtual Machine](https://msdn.microsoft.com/library/dn195938\(v=sql.120\).aspx) Wizard to host a database from an instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] in a Windows Azure Virtual Machine.  
  
  
###  <a name="Backup"></a> Backup and Restore Enhancements  
 [!INCLUDE[ssSQL14](../includes/sssql14-md.md)] contains the following enhancements for [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Backup and Restore:  
  
-   **SQL Server Backup to URL**  
  
     [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Backup to URL was introduced in [!INCLUDE[ssSQL11](../includes/sssql11-md.md)] SP1 CU2 supported only by [!INCLUDE[tsql](../includes/tsql-md.md)], PowerShell and SMO. In [!INCLUDE[ssSQL14](../includes/sssql14-md.md)] you can use [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] to backup to or restore from Windows Azure Blob storage service. The new option is available both for the Backup task, and maintenance plans. For more information, see [Using Backup Task in SQL Server Management Studio](../relational-databases/backup-restore/sql-server-backup-to-url.md#BackupTaskSSMS), [SQL Server Backup to URL Using Maintenance Plan Wizard](../relational-databases/backup-restore/sql-server-backup-to-url.md#MaintenanceWiz), and [Restoring from Windows Azure storage Using SQL Server Management Studio](../relational-databases/backup-restore/sql-server-backup-to-url.md#RestoreSSMS).  
  
-   **SQL Server Managed Backup to Windows Azure**  
  
     Built on [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Backup to URL, [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] is a service that [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] provides to manage and schedule database and log backups. In this release only backup to Windows Azure storage is supported. [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] can be configured both at the database and at instance level allowing for both granular control at the database level and automating at the instance level. [!INCLUDE[ss_smartbackup](../includes/ss-smartbackup-md.md)] can be configured on [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] instances running on-premises and [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] instances running on Windows Azure virtual machines. It is recommended for [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] instances running on Windows Azure virtual machines. For more information, see [SQL Server Managed  Backup to Windows Azure](../relational-databases/backup-restore/sql-server-managed-backup-to-microsoft-azure.md).  
  
-   **Encryption for Backups**  
  
     You can now choose to encrypt the backup file during a backup operation.  It supports several encryption algorithms including AES 128, AES 192, AES 256, and Triple DES. You must use either a certificate or an asymmetric key to perform encryption during backup. For more information, see [Backup Encryption](../relational-databases/backup-restore/backup-encryption.md).  
  
  
###  <a name="CE"></a> New Design for Cardinality Estimation  
 The cardinality estimation logic, called the cardinality estimator, is re-designed in [!INCLUDE[ssSQL14](../includes/sssql14-md.md)] to improve the quality of query plans, and therefore to improve query performance. The new cardinality estimator incorporates assumptions and algorithms that work well on modern OLTP and data warehousing workloads. It is based on in-depth cardinality estimation research on modern workloads, and our learnings over the past 15 years of improving the SQL Server cardinality estimator. Feedback from customers shows that while most queries will benefit from the change or remain unchanged, a small number might show regressions compared to the previous cardinality estimator. For performance tuning and testing recommendations, see [Cardinality Estimation &#40;SQL Server&#41;](../relational-databases/performance/cardinality-estimation-sql-server.md).  
   
  
###  <a name="Durability"></a> Delayed Durability  
 [!INCLUDE[ssSQL14](../includes/sssql14-md.md)] introduces the ability to reduce latency by designating some or all transactions as delayed durable. A delayed durable transaction returns control to the client before the transaction log record is written to disk. Durability can be controlled at the database level, COMMIT level, or ATOMIC block level.  
  
 For more information see the topic [Control Transaction Durability](../relational-databases/logs/control-transaction-durability.md).  
  
  
###  <a name="AlwaysOn"></a> AlwaysOn Enhancements  
 [!INCLUDE[ssSQL14](../includes/sssql14-md.md)] contains the following enhancements for AlwaysOn Failover Cluster Instances and AlwaysOn Availability Groups:  
  
-   An Add Azure Replica Wizard simplifies creating hybrid solutions for AlwaysOn availability groups. For more information, see [Use the Add Azure Replica Wizard &#40;SQL Server&#41;](availability-groups/windows/use-the-add-azure-replica-wizard-sql-server.md).  
  
-   The maximum number of secondary replicas is increased from 4 to 8.  
  
-   When disconnected from the primary replica or during cluster quorum loss, readable secondary replicas now remain available for read workloads.  
  
-   Failover cluster instances (FCIs) can now use Cluster Shared Volumes (CSVs) as cluster shared disks. For more information, see [Always On Failover Cluster Instances](../sql-server/failover-clusters/windows/always-on-failover-cluster-instances-sql-server.md).  
  
-   A new system function, [sys.fn_hadr_is_primary_replica](/sql/relational-databases/system-functions/sys-fn-hadr-is-primary-replica-transact-sql), and a new DMV, [sys.dm_io_cluster_valid_path_names](/sql/relational-databases/system-dynamic-management-views/sys-dm-io-cluster-valid-path-names-transact-sql), is available.  
  
-   The following DMVs were enhanced and now return FCI information: [sys.dm_hadr_cluster](/sql/relational-databases/system-dynamic-management-views/sys-dm-hadr-cluster-transact-sql), [sys.dm_hadr_cluster_members](/sql/relational-databases/system-dynamic-management-views/sys-dm-hadr-cluster-members-transact-sql), and [sys.dm_hadr_cluster_networks](/sql/relational-databases/system-dynamic-management-views/sys-dm-hadr-cluster-networks-transact-sql).  
  
  
###  <a name="OIR"></a> Partition Switching and Indexing  
 The individual partitions of partitioned tables can now be rebuilt. For more information, see [ALTER INDEX &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-index-transact-sql).  
   
  
###  <a name="Lock"></a> Managing the Lock Priority of Online Operations  
 The `ONLINE = ON` option now contains a `WAIT_AT_LOW_PRIORITY` option which permits you to specify how long the rebuild process should wait for the necessary locks. The `WAIT_AT_LOW_PRIORITY` option also allows you to configure the termination of blocking processes related to that rebuild statement. For more information, see [ALTER TABLE &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-table-transact-sql) and [ALTER INDEX &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-index-transact-sql). Troubleshooting information about new types of lock states is available in [sys.dm_tran_locks &#40;Transact-SQL&#41;](/sql/relational-databases/system-dynamic-management-views/sys-dm-tran-locks-transact-sql) and [sys.dm_os_wait_stats &#40;Transact-SQL&#41;](/sql/relational-databases/system-dynamic-management-views/sys-dm-os-wait-stats-transact-sql).  
 
  
###  <a name="CCI"></a> Columnstore Indexes  
 These new features are available for columnstore indexes:  
  
-   **Clustered columnstore indexes**  
  
     Use a clustered columnstore index to improve data compression and query performance for data warehousing workloads that primarily perform bulk loads and read-only queries. Since the clustered columnstore index is updateable, the workload can perform many insert, update, and delete operations. For more information, see [Columnstore Indexes Described](../relational-databases/indexes/columnstore-indexes-described.md) and [Using Clustered Columnstore Indexes](../relational-databases/indexes/indexes.md).  
  
-   **SHOWPLAN**  
  
     SHOWPLAN displays information about columnstore indexes. The **EstimatedExecutionMode** and **ActualExecutionMode** properties have two possible values: **Batch** or **Row**.  The **Storage** property has two possible values: **RowStore** and **ColumnStore**.  
  
-   **Archival data compression**  
  
     ALTER INDEX ... REBUILD has a new COLUMNSTORE_ARCHIVE data compression option that further compresses the specified partitions of a columnstore index. Use this for archival, or for other situations that require a smaller data storage size and can afford more time for storage and retrieval. For more information, see [ALTER INDEX &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-index-transact-sql).  
   
  
###  <a name="Buffer"></a> Buffer Pool Extension  
 The [Buffer Pool Extension](configure-windows/buffer-pool-extension.md) provides the seamless integration of solid-state drives (SSD) as a nonvolatile random access memory (NvRAM) extension to the [!INCLUDE[ssDE](../includes/ssde-md.md)] buffer pool to significantly improve I/O throughput.  
   
  
###  <a name="Stats"></a> Incremental Statistics  
 CREATE STATISTICS and related statistic statements now permits per partition statistics to be created by using the INCREMENTAL option. Related statements allow or report incremental statistics. Affected syntax includes UPDATE STATISTICS, sp_createstats, CREATE INDEX, ALTER INDEX, ALTER DATABASE SET options, DATABASEPROPERTYEX, sys.databases, and sys.stats. For more information, see [CREATE STATISTICS &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-statistics-transact-sql).  
  
  
###  <a name="RG"></a> Resource Governor Enhancements for Physical IO Control  
 The Resource Governor enables you to specify limits on the amount of CPU, physical IO, and memory that incoming application requests can use within a resource pool. In [!INCLUDE[ssSQL14](../includes/sssql14-md.md)], you can use the new MIN_IOPS_PER_VOLUME and MAX_IOPS_PER_VOLUME settings to control the physical IOs issued for user threads for a given resource pool. For more information, see [Resource Governor Resource Pool](../relational-databases/resource-governor/resource-governor-resource-pool.md) and [CREATE RESOURCE POOL &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-resource-pool-transact-sql).  
  
 The MAX_OUTSTANDING_IO_PER_VOLUME setting of the ALTER RESOURCE GOVENOR sets the maximum outstanding I/O operations per disk volume. You can use this setting to tune IO resource governance to the IO characteristics of a disk volume and can be used to limit the number of IOs issued at the SQL Server instance boundary. For more information, see [ALTER RESOURCE GOVERNOR &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-resource-governor-transact-sql).  
  
  
###  <a name="OnlineEvent"></a> Online Index Operation Event Class  
 The progress report for the online index operation event class now has two new data columns: **PartitionId** and **PartitionNumber**. For more information, see [Progress Report: Online Index Operation Event Class](../relational-databases/event-classes/progress-report-online-index-operation-event-class.md).  
  
  
###  <a name="Compat"></a> Database Compatibility Level  
 The 90 compatibility level is not valid in [!INCLUDE[ssSQL14](../includes/sssql14-md.md)]. For more information, see [ALTER DATABASE Compatibility Level &#40;Transact-SQL&#41;](/sql/t-sql/statements/alter-database-transact-sql-compatibility-level)  
  
##  <a name="TSQL"></a> Transact-SQL Enhancements  
  
### Inline specification of CLUSTERED and NONCLUSTERED  
 Inline specification of `CLUSTERED` and `NONCLUSTERED` indexes is now allowed for disk-based tables. Creating a table with inline indexes is equivalent to issuing a create table followed by corresponding `CREATE INDEX` statements. Included columns and filter conditions are not supported with inline indexes.  
  
### SELECT ... INTO  
 The `SELECT ... INTO` statement is improved and can now operate in parallel. The database compatibility level must be at least 110.  
  
### [!INCLUDE[tsql](../includes/tsql-md.md)] Enhancements for In-Memory OLTP  
 For information about the [!INCLUDE[tsql](../includes/tsql-md.md)] changes to support In-Memory OLTP, see [Transact-SQL Support for In-Memory OLTP](../relational-databases/in-memory-oltp/transact-sql-support-for-in-memory-oltp.md).  
  
  
##  <a name="SystemTable"></a> System View Enhancements  
  
### sys.xml_indexes  
 [sys.xml_indexes &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-xml-indexes-transact-sql) has 3 new columns: `xml_index_type`, `xml_index_type_description`, and `path_id`.  
  
### sys.dm_exec_query_profiles  
 [sys.dm_exec_query_profiles &#40;Transact-SQL&#41;](/sql/relational-databases/system-dynamic-management-views/sys-dm-exec-query-profiles-transact-sql) monitors real time query progress while a query is in execution.  
  
### sys.column_store_row_groups  
 [sys.column_store_row_groups &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-column-store-row-groups-transact-sql) provides clustered columnstore index information on a per-segment basis to help the administrator make system management decisions.  
  
### sys.databases  
 [sys.databases &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-databases-transact-sql) has 3 new columns: `is_auto_create_stats_incremental_on`, `is_query_store_on`, and `resource_pool_id`.  
  
### System View Enhancements for In-Memory OLTP  
 For information about system view enhancements to support In-Memory OLTP, see [System Views, Stored Procedures, DMVs and Wait Types for In-Memory OLTP](../../2014/database-engine/system-views-stored-procedures-dmvs-and-wait-types-for-in-memory-oltp.md).  
   
  
##  <a name="Security"></a> Security Enhancements  
  
### CONNECT ANY DATABASE Permission  
 A new server level permission. Grant **CONNECT ANY DATABASE** to a login that must connect to all databases that currently exist and to any new databases that might be created in future. Does not grant any permission in any database beyond connect. Combine with **SELECT ALL USER SECURABLES** or `VIEW SERVER STATE` to allow an auditing process to view all data or all database states on the instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].  
  
### IMPERSONATE ANY LOGIN Permission  
 A new server level permission. When granted, allows a middle-tier process to impersonate the account of clients connecting to it, as it connects to databases. When denied, a high privileged login can be blocked from impersonating other logins. For example, a login with **CONTROL SERVER** permission can be blocked from impersonating other logins.  
  
### SELECT ALL USER SECURABLES Permission  
 A new server level permission. When granted, a login such as an auditor can view data in all databases that the user can connect to.  
  
  
##  <a name="Deployment"></a> Deployment Enhancements  
### Azure VM
[Deploy a SQL Server Database to a Microsoft Azure Virtual Machine](../relational-databases/databases/deploy-a-sql-server-database-to-a-microsoft-azure-virtual-machine.md) enables deployment of a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] database to a Windows Azure VM.  

### ReFS
Deployment of databases on ReFS is now supported.   
  
## See Also  
 [Features Supported by the Editions of SQL Server 2014](../../2014/getting-started/features-supported-by-the-editions-of-sql-server-2014.md)  
   
