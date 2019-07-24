---
title: "What&#39;s New in SQL Server 2014 | Microsoft Docs"
ms.custom: ""
ms.date: "05/25/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: install
ms.topic: conceptual
helpviewer_keywords: 
  - "new features [SQL Server]"
  - "SQL Server, what's new"
  - "SQL Server 2008 what's new"
  - "what's new [SQL Server]"
  - "sql server 2014 sp1"
  - "sql server 2014 sp2"
ms.assetid: 6a428023-e3cc-4626-a88a-4c13ccbd7db0
author: craigg-msft
ms.author: craigg
manager: craigg
---
# What&#39;s New in SQL Server 2014
  This topic summarizes detailed links to new features in [!INCLUDE[ssSQL14](../includes/sssql14-md.md)] and summarizes services packs for [!INCLUDE[ssSQL14](../includes/sssql14-md.md)]  
 
**Try it out:** ![Azure Virtual Machine small](./media/what-s-new-in-sql-server-2016/azure-virtual-machine-small.png)     Have an Azure account?  Then go **[Here](https://ms.portal.azure.com/?flight=1#create/Microsoft.SQLServer2014sp1EnterpriseWindowsServer2012R2)** to spin up a Virtual Machine with SQL Server 2014 Service Pack 1 (SP1) already installed. 
  
-   [What's New &#40;Database Engine&#41;](../database-engine/whats-new-in-sql-server-2016.md)  
  
-   [What's New in Analysis Services and Business Intelligence](../analysis-services/what-s-new-in-analysis-services.md)  
  
-   [What's New in SQL Server Installation](install/what-s-new-in-sql-server-installation.md)  
  
 **[!INCLUDE[ssSQL14](../includes/sssql14-md.md)] has not introduced significant new features to the following:**  
  
-   [What's New &#40;Integration Services&#41;](../integration-services/what-s-new-in-integration-services-in-sql-server-2016.md)  
  
-   [What's New &#40;Reporting Services&#41;](../reporting-services/what-s-new-reporting-services.md)  
  
## [!INCLUDE[ssSQL14](../includes/sssql14-md.md)] Service Pack 1 (SP1)
[!INCLUDE[ssSQL14](../includes/sssql14-md.md)] (SP1) did not introduce significant new features.
-  [SQL Server 2014 Service Pack 1 release information](https://support.microsoft.com/en-us/kb/3058865).
-  [![Download Service Pack 1 for Microsoft?? SQL Server?? 2014](./media/what-s-new-in-sql-server-2016/download.png)](https://www.microsoft.com/en-us/download/details.aspx?id=46694) [Download Service Pack 1 for Microsoft?? SQL Server?? 2014](https://www.microsoft.com/en-us/download/details.aspx?id=46694).


## [!INCLUDE[ssSQL14](../includes/sssql14-md.md)] Service Pack 2 (SP2)
- [SQL Server 2014 Service Pack 2 release information](https://support.microsoft.com/en-us/kb/3171021).
-  [![Download Service Pack 2 for Microsoft?? SQL Server?? 2014](./media/what-s-new-in-sql-server-2016/download.png)](https://go.microsoft.com/fwlink/?LinkID=821558) [Download Service Pack 2 for Microsoft?? SQL Server?? 2014](https://go.microsoft.com/fwlink/?LinkID=821558).
-  [![Download SQL Server 2014 SP2 Feature Pack](./media/what-s-new-in-sql-server-2016/download.png)](https://www.microsoft.com/en-us/download/details.aspx?id=53164) [Download the SQL Server 2014 SP2 Feature Pack](https://www.microsoft.com/en-us/download/details.aspx?id=53164).

[!INCLUDE[ssSQL14](../includes/sssql14-md.md)] (SP2) Includes the following improvements:

### Performance and Scalability Improvements 
-   **Automatic Soft NUMA partitioning:** With [!INCLUDE[ssSQL14](../includes/sssql14-md.md)] SP2, Automatic Soft NUMA is enabled when Trace Flag 8079 is turned on during instance startup. When Trace Flag 8079 is enabled during startup, [!INCLUDE[ssSQL14](../includes/sssql14-md.md)] SP2 will interrogate the hardware layout and automatically configure Soft NUMA on systems reporting 8 or more CPUs per NUMA node. The automatic, soft NUMA behavior is Hyperthread (HT/logical processor) aware. The partitioning and creation of additional nodes scales background processing by increasing the number of listeners, scaling, and network and encryption capabilities. It is recommended to first test the performance workload with Auto-Soft NUMA before turning it in production. [See the Blog for more information](https://blogs.msdn.microsoft.com/psssql/2016/03/30/sql-2016-it-just-runs-faster-automatic-soft-numa/). 
-  **Dynamic Memory Object Scaling:** [!INCLUDE[ssSQL14](../includes/sssql14-md.md)] SP2 dynamically partitions memory objects based on number of nodes and cores to scale on modern hardware. The goal of dynamic promotion is to automatically partition a thread safe memory object (CMEMTHREAD) if it becomes a bottleneck. Un-partitioned memory objects can be dynamically promoted to be partitioned by node (number of partitions equals number of NUMA nodes), and memory objects partitioned by node can by further promoted to be partitioned by CPU (number of partitions equals number of CPUs). [See the blog for more information](https://blogs.msdn.microsoft.com/psssql/2016/04/06/sql-2016-it-just-runs-faster-dynamic-memory-object-cmemthread-partitioning/).
-  **MAXDOP hint for DBCC CHECK\* commands:** This improvement addresses [connect feedback (468694)](https://connect.microsoft.com/SQLServer/feedback/details/468694/maxdop-option-in-dbcc-checkdb). You can now run DBCC CHECKDB with the a MAXDOP setting other than the sp_configure value. If MAXDOP exceeds the value configured with Resource Governor, the Database Engine uses the Resource Governor MAXDOP value, described in ALTER WORKLOAD GROUP (Transact-SQL). All semantic rules used with the max degree of parallelism configuration option are applicable when you use the MAXDOP query hint. For more information, see [DBCC CHECKDB (Transact-SQL)](https://msdn.microsoft.com/library/ms176064.aspx).
-   **Enable >8TB for Buffer Pool:** [!INCLUDE[ssSQL14](../includes/sssql14-md.md)] SP2 enables 128TB of virtual address space for buffer pool usage. This improvement enables SQL Server Buffer Pool to scale beyond 8TB on modern hardware.
-   **SOS_RWLock spinlock Improvement:** The SOS_RWLock is a synchronization primitive used in various places throughout the SQL Server code base.  As the name implies, the code can have multiple shared (readers) or single (writer) ownership. This improvement removes the need for spinlock for SOS_RWLock and instead uses lock-free techniques similar to in-memory OLTP. With this change, many threads can read a data structure protected by SOS_RWLock in parallel without blocking each other and thereby providing increased scalability. Prior to this change, the spinlock implementation allowed only one thread to acquire the SOS_RWLock at a time, even to read a data structure.  [See the blog for more information](https://blogs.msdn.microsoft.com/psssql/2016/04/07/sql-2016-it-just-runs-faster-sos_rwlock-redesign/).
-    **Spatial Native Implementation:** Significant improvement in spatial query performance is introduced in [!INCLUDE[ssSQL14](../includes/sssql14-md.md)] SP2 through native implementation. For more information, see the [knowledge base article KB3107399](https://support.microsoft.com/en-us/kb/3107399).

### Supportability and Diagnostics Improvements
-   **Database Cloning:** Clone database is a new DBCC command that enhances troubleshooting existing production databases by cloning the schema and metadata without the data. The clone is created with the command `DBCC clonedatabase('source_database_name', 'clone_database_name')`.  **Note:** Cloned databases should not be used in production environments. Use the following command determine if a database has been generated from a cloned database: `select DATABASEPROPERTYEX('clonedb', 'isClone')`. The return value of **1** indicates the database is created from clonedatabase while **0** indicates it is not a clone.
-   **Tempdb supportability:**  A new errorlog message that indicates the number of tempdb files and the size/autogrowth of tempdb data files present at server startup.
-   **Database Instant File Initialization Logging:** A new errorlog message that indicates on server statup, the status of Database Instant File Initialization (enabled/disabled).
-   **Module names in callstack:** The Xevent callstack now includes modules names + offset instead of absolute addresses.
-   **New DMF for incremental statistics:** This improvement addresses [connect feedback (797156)](https://connect.microsoft.com/SQLServer/feedback/details/797156/display-sys-dm-db-stats-properties-per-partition-for-incremental-statistics) to enable tracking the incremental statistics at the partition level. A new DMF sys.dm_db_incremental_stats_properties is introduced to expose information per-partition for incremental stats.
-   **Index Usage DMV behavior updated:** This improvement addresses [connect feedback (739566)](https://connect.microsoft.com/SQLServer/feedback/details/739566/rebuilding-an-index-clears-stats-from-sys-dm-db-index-usage-stats) from customers where rebuilding an index will *not* clear any existing row entry from sys.dm_db_index_usage_stats for that index. The behavior will now be the same as in SQL 2008 and SQL Server 2016. [See the blog for more information](https://blogs.msdn.microsoft.com/sql_server_team/index-usage-dmv-behavior-updated/).
-   **Improved correlation between diagnostics XE and DMVs:** This improvement addresses [connect feedback (1934583)](https://connect.microsoft.com/SQLServer/feedback/details/1934583/extended-events-query-hash-and-query-plan-hash-data-types). Query_hash and query_plan_hash are used for identifying a query uniquely. DMV defines them as varbinary(8), while XEvent defines them as UINT64. Since SQL server does not have "unisigned bigint", casting does not always work. This improvement introduces new XEvent action/filter columns equivalent to query_hash and query_plan_hash except they are defined as INT64 which can help correlating queries between XE and DMVs.
-   **Support for UTF-8 in BULK INSERT and BCP:** This improvement addresses [connect feedback (370419)](https://connect.microsoft.com/SQLServer/feedback/details/370419/bulk-insert-and-bcp-does-not-recognize-codepage-65001) where support for export and import of data encoded in UTF-8 character set is now enabled in BULK INSERT and BCP.
-   **Lightweight per-operator query execution profiling:** While troubleshooting query performance, although showplan provides lot of information on the query execution plan and cost of operator in the plan but it has limited information on actual runtime statistics like (CPU, I/O Reads, elapsed time per-thread). SQL 2014 SP2 introduces these additional runtime statistics per operator in the Showplan as well as an XEvent (query_thread_profile) to assist troubleshooting query performance. [See the blog for more information](https://blogs.msdn.microsoft.com/sql_server_team/added-per-operator-level-performance-stats-for-query-processing/).
-   **Change Tracking Cleanup:** A new stored procedure `sp_flush_CT_internal_table_on_demand` is introduced to cleanup change tracking internal tables on demand.
-   **AlwaysON Lease Timeout Logging** Added new logging capability for Lease Timeout messages so that the current time and the expected renewal times are logged. Also a new message was introduced in the SQL Errorlog regarding the timeouts. [See the blog for more information](https://blogs.msdn.microsoft.com/alwaysonpro/2016/02/23/improved-alwayson-availability-group-lease-timeout-diagnostics/).
-   **New DMF for retrieving input buffer in SQL Server:** A new DMF for retrieving the input buffer for a session/request (sys.dm_exec_input_buffer) is now available. This is functionally equivalent to DBCC INPUTBUFFER. [See the blog for more information](https://blogs.msdn.microsoft.com/sql_server_team/new-dmf-for-retrieving-input-buffer-in-sql-server/).
-   **Mitigation for underestimated and overestimated memory grant:** Added new query hints for Resource Governor through MIN_GRANT_PERCENT and MAX_GRANT_PERCENT. This allows you to leverage these hints while running queries by capping their memory grants to prevent memory contention. For more information, see [knowledge base article KB310740](https://support.microsoft.com/en-us/kb/3107401)
-   **Better memory grant/usage diagnostics:** A new extended event was added to the list of tracing capabilities in SQL Server (query_memory_grant_usage) to track memory grants requested and granted. This provides better tracing and analysis capabilities for troubleshooting query execution issues related to memory grants. For more information, see [knowledge base article KB3107173](https://support.microsoft.com/en-us/kb/3107173).
-   **Query execution diagnostics for tempdb spill:**- Hash Warning and Sort Warnings now have additional columns to track physical I/O statistics, memory used and rows affected. We also introduced a new hash_spill_details extended event. Now you can track more granular information for your hash and sort warnings ([KB3107172](https://support.microsoft.com/en-us/kb/3107172)). This improvement is also now exposed through the XML Query Plans in the form of a new attribute to the SpillToTempDbType complex type ([KB3107400](https://support.microsoft.com/en-us/kb/3107400)). Set statistics on now shows sort worktable statistics. .
-   **Improved diagnostics for query execution plans that involve residual predicate pushdown:** The actual rows read will now be reported in the query execution plans to help improve query performance troubleshooting. This should negate the need to capture SET STATISTICS IO separately. This now allows you to see information related to a residual predicate pushdown in a query plan. For more information, see [knowledge base article KB3107397](https://support.microsoft.com/en-us/kb/3107397).


## Additional Information  
 [SQL Server 2014 Resources](../2014-toc/books-online-for-sql-server-2014.md)  
  
 [SQL Server 2014 Release Notes](https://go.microsoft.com/fwlink/p/?linkID=296445)  
  
 [SQL Server 2014 Resource Center](https://msdn.microsoft.com/sqlserver/dn135309)  
  
 [SQLCat Web Site](https://go.microsoft.com/fwlink/p/?linkID=220963)  
  
  
