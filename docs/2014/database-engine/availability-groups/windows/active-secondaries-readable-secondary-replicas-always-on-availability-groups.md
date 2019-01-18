---
title: "Active Secondaries: Readable Secondary Replicas (Always On Availability Groups) | Microsoft Docs"
ms.custom: ""
ms.date: "10/27/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
helpviewer_keywords: 
  - "connection access to availability replicas"
  - "Availability Groups [SQL Server], availability replicas"
  - "Availability Groups [SQL Server], readable secondary replicas"
  - "active secondary replicas [SQL Server], read-only access to"
  - "readable secondary replicas"
  - "Availability Groups [SQL Server], active secondary replicas"
ms.assetid: 78f3f81a-066a-4fff-b023-7725ff874fdf
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Active Secondaries: Readable Secondary Replicas (Always On Availability Groups)
  The [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] active secondary capabilities include support for read-only access to one or more secondary replicas (*readable secondary replicas*). A readable secondary replica allows read-only access to all its secondary databases. However, readable secondary databases are not set to read-only. They are dynamic. A given secondary database changes as changes on the corresponding primary database are applied to the secondary database. For a typical secondary replica, the data, including durable memory optimized tables, in the secondary databases is in near real time. Furthermore, full-text indexes are synchronized with the secondary databases. In many circumstances, data latency between a primary database and the corresponding secondary database is only a few seconds.  
  
 Security settings that occur in the primary databases are persisted to the secondary databases. This includes users, database roles, and applications roles together with their respective permissions and transparent data encryption (TDE), if enabled on the primary database.  
  
> [!NOTE]  
>  Though you cannot write data to secondary databases, you can write to read-write databases on the server instance that hosts the secondary replica, including user databases and system databases such as **tempdb**.  
  
 [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] also supports the re-routing of read-intent connection requests to a readable secondary replica (*read-only routing*). For information about read-only routing, see [Using a Listener to Connect to a Read-Only Secondary Replica (Read-Only Routing)](../../listeners-client-connectivity-application-failover.md#ConnectToSecondary).  
  
 
  
##  <a name="bkmk_Benefits"></a> Benefits  
 Directing read-only connections to readable secondary replicas provides the following benefits:  
  
-   Offloads your secondary read-only workloads from your primary replica, which conserves its resources for your mission critical workloads. If you have mission critical read-workload or the workload that cannot tolerate latency, you should run it on the primary.  
  
-   Improves your return on investment for the systems that host readable secondary replicas.  
  
 In addition, readable secondaries provide robust support for read-only operations, as follows:  
  
-   Automatic temporary statistics on readable secondary database optimize read-only queries on disk-based tables. For memory-optimized tables, the missing statistics are created automatically. However, there is no auto-update of stale statistics. You will need to manually update the statistics on the primary replica. For more information, see [Statistics for Read-Only Access Databases](#Read-OnlyStats), later in this topic.  
  
-   Read-only workloads for disk-based tables use row versioning to remove blocking contention on the secondary databases. All queries that run against the secondary databases are automatically mapped to snapshot isolation transaction level, even when other transaction isolation levels are explicitly set. Also, all locking hints are ignored. This eliminates reader/writer contention.  
  
-   Read-only workloads for memory-optimized durable tables access the data in exactly the same way it is accessed on the primary database, using native stored procedures or SQL Interoperability with the same transaction isolation level limitations. Reporting workload or read-only queries running on the primary replica can be run on the secondary replica without requiring any changes. Similarly, a reporting workload or read-only queries running on a secondary replica can be run on the primary replica without requiring any changes.  Similar to disk-based tables, all queries that run against the secondary databases are automatically mapped to snapshot isolation transaction level, even when other transaction isolation levels are explicitly set.  
  
-   DML operations are allowed on table variables both for disk-based and memory-optimized table types on the secondary replica.  
  
##  <a name="bkmk_Prerequisites"></a> Prerequisites for the Availability Group  
  
-   **Readable secondary replicas (required)**  
  
     The database administrator needs to configure one or more replicas so that, when running under the secondary role, they allow either all connections (just for read-only access) or only read-intent connections.  
  
    > [!NOTE]  
    >  Optionally, the database administrator can configure any of the availability replicas to exclude read-only connections when running under the primary role.  
  
     For more information, see [About Client Connection Access to Availability Replicas &#40;SQL Server&#41;](about-client-connection-access-to-availability-replicas-sql-server.md).  
  
-   **Availability group listener**  
  
     To support read-only routing, an availability group must possess an [availability group listener](../../listeners-client-connectivity-application-failover.md). The read-only client must direct its connection requests to this listener, and the client's connection string must specify the application intent as "read-only." That is, they must be *read-intent connection requests*.  
  
-   **Read only routing**  
  
     *Read-only routing* refers to the ability of SQL Server to route incoming read-intent connection requests, that are directed to an availability group listener, to an available readable secondary replica. The prerequisites for read-only routing are as follows:  
  
    -   To support read-only routing, a readable secondary replica requires a read-only routing URL. This URL takes effect only when the local replica is running under the secondary role. The read-only routing URL must be specified on a replica-by-replica basis, as needed. Each read-only routing URL is used for routing read-intent connection requests to a specific readable secondary replica. Typically, every readable secondary replica is assigned a read-only routing URL.  
  
    -   Each availability replica that is to support read-only routing when it is the primary replica requires a read-only routing list. A given read-only routing list takes effect only when the local replica is running under the primary role. This list must be specified on a replica-by-replica basis, as needed. Typically, each read-only routing list would contain every read-only routing URL, with the URL of the local replica at the end of the list.  
  
        > [!NOTE]  
        >  Read-intent connection requests are routed to the first available readable secondary on the read-only routing list of the current primary replica. There is no load balancing.  
  
     For more information, see [Configure Read-Only Routing for an Availability Group &#40;SQL Server&#41;](configure-read-only-routing-for-an-availability-group-sql-server.md).  
  
> [!NOTE]  
>  For information about availability group listeners and more information about read-only routing, see [Availability Group Listeners, Client Connectivity, and Application Failover &#40;SQL Server&#41;](../../listeners-client-connectivity-application-failover.md).  
  
##  <a name="bkmk_LimitationsRestrictions"></a> Limitations and Restrictions  
 Some operations are not fully supported, as follows:  
  
-   As soon as a readable replica is enabled for read, it can start accepting connections to its secondary databases. However, if any active transactions exist on a primary database, the row versions will not be fully available on the corresponding secondary database. Any active transactions that existed on the primary replica when the secondary replica was configured must commit or roll back. Until this process completes, the transaction isolation level mapping on the secondary database is incomplete and queries are temporarily blocked.  
  
    > [!WARNING]  
    >  Running long transactions impacts the number of versioned rows kept, both for disk-based and memory-optimized tables.  
  
-   On a secondary database with memory-optimized tables, even though row versions are always generated for memory-optimzied tables, queries are blocked until all active transactions that existed in the primary replica when the secondary replica was enabled for read complete. This ensures that both disk-based and memory-optimized tables are available to the reporting workload and read-only queries at the same time.  
  
-   Change tracking and change data capture are not supported on secondary databases that belong to a readable secondary replica:  
  
    -   Change tracking is explicitly disabled on secondary databases.  
  
    -   Change data capture can be enabled on a secondary database, but this is not supported.  
  
-   Because read operations are mapped to snapshot isolation transaction level, the cleanup of ghost records on the primary replica can be blocked by transactions on one or more secondary replicas. The ghost record cleanup task will automatically clean up the ghost records for disk-based tables on the primary replica when they are no longer needed by any secondary replica.  This is similar to what is done when you run transaction(s) on the primary replica. In the extreme case on the secondary database, you will need to kill a long running read-query that is blocking the ghost cleanup. Note, the ghost clean can be blocked if the secondary replica gets disconnected or when data movement is suspended on the secondary database. This state also prevents log truncation, so if this state persists, we recommend that you remove this secondary database from the availability group. There is no ghost record cleanup issue with memory-optimized tables because the row versions are kept in memory and are independent of the row versions on the primary replica.  
  
-   The DBCC SHRINKFILE operation on files containing disk-based tables might fail on the primary replica if the file contains ghost records that are still needed on a secondary replica.  
  
-   Beginning in [!INCLUDE[ssSQL14](../../../includes/sssql14-md.md)], readable secondary replicas can remain online even when the primary replica is offline due to user action or a failure. However, read-only routing does not work in this situation because the availability group listener is offline as well. Clients must connect directly to the read-only secondary replicas for read-only workloads.  
  
> [!NOTE]  
>  If you query the [sys.dm_db_index_physical_stats](/sql/relational-databases/system-dynamic-management-views/sys-dm-db-index-physical-stats-transact-sql) dynamic management view on a server instance that is hosting a readable secondary replica, you might encounter a REDO blocking issue. This is because this dynamic management view acquires an IS lock on the specified user table or view that can block requests by a REDO thread for an X lock on that user table or view.  
  
##  <a name="bkmk_Performance"></a> Performance Considerations  
 This section discusses several performance considerations for readable secondary databases  
  
 
  
###  <a name="DataLatency"></a> Data Latency  
 Implementing read-only access to secondary replicas is useful if your read-only workloads can tolerate some data latency. In situations where data latency is unacceptable, consider running read-only workloads against the primary replica.  
  
 The primary replica sends log records of changes on primary database to the secondary replicas. On each secondary database, a dedicated redo thread applies the log records. On a read-access secondary database, a given data change does not appear in query results until the log record that contains the change has been applied to the secondary database and the transaction has been committed on primary database.  
  
 This means that there is some latency, usually only a matter of seconds, between the primary and secondary replicas. In unusual cases, however, for example if network issues reduce throughput, latency can become significant. Latency increases when I/O bottlenecks occur and when data movement is suspended. To monitor suspended data movement, you can use the [AlwaysOn Dashboard](use-the-always-on-dashboard-sql-server-management-studio.md) or the [sys.dm_hadr_database_replica_states](/sql/relational-databases/system-dynamic-management-views/sys-dm-hadr-database-replica-states-transact-sql) dynamic management view.  
  
####  <a name="bkmk_LatencyWithInMemOLTP"></a> Data Latency on databases with memory-optimized tables  
 When accessing memory-optimized tables on secondary replica for read workload, a *safe-timestamp* is used to return rows from transactions that have committed earlier than *safe-timestamp*. The safe-timestamp is the oldest timestamp hint used by the garbage collection thread to garbage collect the rows on the primary replica. This timestamp is updated when the number of DML transactions on memory-optimized tables exceed an internal threshold since the last update. Whenever the oldest transaction timestamp is updated on the primary replica, the next DML transaction on a durable memory-optimized table will send this timestamp to be sent to secondary replica as part of a special log record. REDO thread on the secondary replica, updates the safe-timestamp as part of processing this log record.  
  
#### The impact of safe-timestamp on Latency  
  
-   For OLTP workloads with high transaction throughput, the latency should be comparable to disk-based tables. We expect this to be the common case.  
  
-   A long running transaction can cause the safe-timestamp to be delayed arbitrarily.  This is no different when accessing disk-based tables as the timestamp of snapshot isolation is determined by the commit of oldest transaction.  
  
-   Changes made by transactions on the primary replica since the last safe-timestamp update are not visible on the secondary replica till the next transmission and update of the safe-timestamp. If transactional activity on the primary replica stops before the internal threshold for safe-timestamp update is crossed, the changes made since the last update to safe-timestamp will not be visible on the secondary replica. To alleviate this issue, you may need to run a few DML transactions on a dummy durable memory-optimized table on the primary replica. Alternatively, though not recommended, you can force shipping of safe-timestamp by running a manual checkpoint.  
  
#### Monitoring and Troubleshooting data latency in memory-optimized tables  
 You can find out the safe-timestamp by running the following query on the primary replica  
  
```  
  
SELECT MAX(base_generation)   
   AS max_base_generation  
   FROM sys.dm_db_xtp_gc_cycle_stats  
GO  
  
```  
  
 You can also identify the safe-timestamp used on the secondary replica by running the following query concurrently with the active read-workload.  
  
```  
  
SELECT begin_tsn   
   FROM sys.dm_db_xtp_transactions  
GO  
  
```  
  
###  <a name="ReadOnlyWorkloadImpact"></a> Read-Only Workload Impact  
 When you configure a secondary replica for read-only access, your read-only workloads on the secondary databases consume system resources, such as CPU and I/O (for disk-based tables) from redo threads, especially if the read-only workloads on disk-based tables are highly I/O-intensive. There is no IO impact when accessing memory-optimized tables because all the rows reside in memory.  
  
 Also, read-only workloads on the secondary replicas can block data definition language (DDL) changes that are applied through log records.  
  
-   Even though the read operations do not take shared locks because of row versioning, these operations take schema stability (Sch-S) locks, which can block redo operations that are applying DDL changes. DDL operations include ALTER/DROP tables and Views but not DROP or ALTER of stored procedures. So for example, if you drop a table disk-based or memory-optimized, on primary. When REDO thread processes the log record to drop the table, it must acquire a SCH_M lock on the table and can get blocked by a running query accessing table.  This is the same behavior on primary replica except that the drop of the table is done as part of a user session and not REDO thread.  
  
-   There is additional blocking Memory-Optimized Tables. A drop of native stored procedure can cause REDO thread to block if there is a concurrent execution of the native stored procedure on the secondary replica. This is the same behavior on the primary replica except that the drop of the stored procedure is done as part of a user session and not REDO thread.  
  
 Be aware of best practices around building queries, and exercise those best practices in the secondary databases. For example, schedule long-running queries such as aggregations of data during times of low activity.  
  
> [!NOTE]  
>  If a redo thread is blocked by queries on a secondary replica, the **sqlserver.lock_redo_blocked** XEvent is raised.  
  
###  <a name="bkmk_Indexing"></a> Indexing  
 To optimize read-only workloads on the readable secondary replicas, you may want to create indexes on the tables in the secondary databases. Because you cannot make schema or data changes on the secondary databases, create indexes in the primary databases and allow the changes to transfer to the secondary database through the redo process.  
  
 To monitor index usage activity on a secondary replica, query the **user_seeks**, **user_scans**, and **user_lookups** columns of the [sys.dm_db_index_usage_stats](/sql/relational-databases/system-dynamic-management-views/sys-dm-db-index-usage-stats-transact-sql) dynamic management view.  
  
###  <a name="Read-OnlyStats"></a> Statistics for Read-Only Access Databases  
 Statistics on columns of tables and indexed views are used to optimize query plans. For availability groups, statistics that are created and maintained on the primary databases are automatically persisted on the secondary databases as part of applying the transaction log records. However, the read-only workload on the secondary databases may need different statistics than those that are created on the primary databases. However, because secondary databases are restricted to read-only access, statistics cannot be created on the secondary databases.  
  
 To address this problem, the secondary replica creates and maintains temporary statistics for secondary databases in **tempdb**. The suffix _readonly_database_statistic is appended to the name of temporary statistics to differentiate them from the permanent statistics that are persisted from the primary database.  
  
 Only [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] can create and update temporary statistics. However, you can delete temporary statistics and monitor their properties using the same tools that you use for permanent statistics:  
  
-   Delete temporary statistics using the [DROP STATISTICS](/sql/t-sql/statements/drop-statistics-transact-sql)[!INCLUDE[tsql](../../../includes/tsql-md.md)] statement.  
  
-   Monitor statistics using the **sys.stats** and **sys.stats_columns** catalog views. **sys_stats** includes a column, **is_temporary**, to indicate which statistics are permanent and which are temporary.  
  
 There is no support for auto-statistics update for memory-optimized tables on the primary or secondary replica. You must monitor query performance and plans on the secondary replica and manually update the statistics on the primary replica when needed. However, the missing statistics are automatically created both on primary and secondary replica.  
  
 For more information about SQL Server statistics, see [Statistics](../../../relational-databases/statistics/statistics.md).  
  

  
####  <a name="StalePermStats"></a> Stale Permanent Statistics on Secondary Databases  
 [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] detects when permanent statistics on a secondary database are stale. But changes cannot be made to the permanent statistics except through changes on the primary database. For query optimization, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] creates temporary statistics for disk-based tables on the secondary database and uses these statistics instead of the stale permanent statistics.  
  
 When the permanent statistics are updated on the primary database, they are automatically persisted to the secondary database. Then [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] uses the updated permanent statistics, which are more current than the temporary statistics.  
  
 If the availability group fails over, temporary statistics are deleted on all of the secondary replicas.  
  
####  <a name="StatsLimitationsRestrictions"></a> Limitations and Restrictions  
  
-   Because temporary statistics are stored in **tempdb**, a restart of the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] service causes all temporary statistics to disappear.  
  
-   The suffix _readonly_database_statistic is reserved for statistics generated by [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. You cannot use this suffix when creating statistics on a primary database. For more information, see [Statistics](../../../relational-databases/statistics/statistics.md).  
  
##  <a name="bkmk_AccessInMemTables"></a> Accessing memory-optimized tables on a Secondary Replica  
 The read workload isolation levels on secondary replica are only those allowed on the primary replica. There is no mapping of isolations levels done on the secondary replica. This ensure that any reporting workload that can be run on primary replica is able to run on the secondary replica without requiring any changes. This makes it easy for you to migrate a reporting workload from the primary replica to a secondary or vice versa when the secondary replica is not available.  
  
 The following queries fail to run on the secondary replica similarly to the way they fail on the primary replica.  
  
-   For queries running only on memory-optimized tables, the only supported isolation levels are snapshot, repeatable read, and serializable. Any queries with read-uncommitted or read committed isolation level returns an error unless you have enabled the option MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT at the database level.  
  
    ```tsql  
    SET TRANSACTION ISOLATION LEVEL READ_COMMITTED  
    -- This is not allowed  
    BEGIN TRAN  
    SELECT * FROM t_hk  
    COMMIT  
  
    ```  
  
     Error message:  
  
    ```  
    Msg 41368, Level 16, State 0, Line 2  
    Accessing memory optimized tables using the CREAD_COMMITTED isolation level is supported only for autocommit transactions. It is not supported for explicit or implicit transactions. Provide a supported isolation level for the memory optimized table using a table hing, such as WITH (SNAPSHOT).  
    ```  
  
-   No locking hints are supported on memory-optimized tables. For example, all of the following queries fail with an error. Only NOLOCK hint is allowed and it is NOOP when used with memory-optimized tables.  
  
    ```tsql  
    SELECT * FROM t_hk WITH (PAGLOCK)  
    SELECT * FROM t_hk WITH (READPAST)  
    SELECT * FROM t_hk WITH (ROWLOCK)  
    SELECT * FROM t_hk WITH (READPAST)  
    SELECT * FROM t_hk WITH (TABLOCK)  
    SELECT * FROM t_hk WITH (XLOCK)  
    SELECT * FROM t_hk WITH (UPDLOCK)  
    ```  
  
-   For cross-container transactions, transactions with session isolation level "snapshot" that access memory-optimized tables is not supported. For example,  
  
    ```tsql  
    SET TRANSACTION ISOLATION LEVEL SNAPSHOT  
    -- This is not allowed  
    BEGIN TRAN  
       SELECT * FROM t_hk  
    COMMIT  
    ```  
  
     Error message:  
  
    ```  
    Msg 41332, Level 16, State 0, Line 5  
    Memory optimized tables and natively compiled stored procedures cannot be accessed or created when the session TRANSACTION ISOLATION LEVEL is set to SNAPSHOT.  
    ```  
  
##  <a name="bkmk_CapacityPlanning"></a> Capacity Planning Considerations  
  
-   In the case of disk-based tables, readable secondary replicas can require space in **tempdb** for two reasons:  
  
    -   Snapshot isolation level copies row versions into **tempdb**.  
  
    -   Temporary statistics for secondary databases are created and maintained in **tempdb**. The temporary statistics can cause a slight increase in the size of **tempdb**. For more information, see [Statistics for Read-Only Access Databases](#Read-OnlyStats), later in this section.  
  
-   When you configure read-access for one or more secondary replicas, the primary databases add 14 bytes of overhead on deleted, modified, or inserted data rows to store pointers to row versions on the secondary databases for disk-based tables. This 14-byte overhead is carried over to the secondary databases. As the 14-byte overhead is added to data rows, page splits might occur.  
  
     The row version data is not generated by the primary databases. Instead, the secondary databases generate the row versions. However, row versioning increases data storage in both the primary and secondary databases.  
  
     The addition of the row version data depends on the snapshot isolation or read-committed snapshot isolation (RCSI) level setting on the primary database. The table below describes the behavior of versioning on a readable secondary database under different settings for disk based tables.  
  
    |Readable secondary replica?|Snapshot isolation or RCSI level enabled?|Primary Database|Secondary Database|  
    |---------------------------------|-----------------------------------------------|----------------------|------------------------|  
    |No|No|No row versions or 14-byte overhead|No row versions or 14-byte overhead|  
    |No|Yes|Row versions and 14-byte overhead|No row versions, but 14-byte overhead|  
    |Yes|No|No row versions, but 14-byte overhead|Row versions and 14-byte overhead|  
    |Yes|Yes|Row versions and 14-byte overhead|Row versions and 14-byte overhead|  
  
##  <a name="bkmk_RelatedTasks"></a> Related Tasks  
  
-   [Configure Read-Only Access on an Availability Replica &#40;SQL Server&#41;](configure-read-only-access-on-an-availability-replica-sql-server.md)  
  
-   [Configure Read-Only Routing for an Availability Group &#40;SQL Server&#41;](configure-read-only-routing-for-an-availability-group-sql-server.md)  
  
-   [Create or Configure an Availability Group Listener &#40;SQL Server&#41;](create-or-configure-an-availability-group-listener-sql-server.md)  
  
-   [Monitor Availability Groups &#40;Transact-SQL&#41;](monitor-availability-groups-transact-sql.md)  
  
-   [View Availability Replica Properties &#40;SQL Server&#41;](view-availability-replica-properties-sql-server.md)  
  
-   [Use the New Availability Group Dialog Box &#40;SQL Server Management Studio&#41;](use-the-new-availability-group-dialog-box-sql-server-management-studio.md)  
  
##  <a name="RelatedContent"></a> Related Content  
  
-   [SQL Server AlwaysOn Team Blog: The official SQL Server AlwaysOn Team Blog](https://blogs.msdn.com/b/sqlalwayson/)  
  
## See Also  
 [Overview of AlwaysOn Availability Groups &#40;SQL Server&#41;](overview-of-always-on-availability-groups-sql-server.md)   
 [About Client Connection Access to Availability Replicas &#40;SQL Server&#41;](about-client-connection-access-to-availability-replicas-sql-server.md)   
 [Availability Group Listeners, Client Connectivity, and Application Failover &#40;SQL Server&#41;](../../listeners-client-connectivity-application-failover.md)   
 [Statistics](../../../relational-databases/statistics/statistics.md)  
  
  
