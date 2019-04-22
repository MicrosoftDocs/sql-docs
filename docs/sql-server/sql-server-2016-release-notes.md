---
title: "SQL Server 2016 Release Notes | Microsoft Docs"
ms.date: "04/25/2018"
ms.prod: sql
ms.reviewer: ""
ms.custom: ""
ms.technology: install
ms.topic: conceptual
helpviewer_keywords:
  - "build notes"
  - "release issues"
ms.assetid: c64077a2-bec8-4c87-9def-3dbfb1ea1fb6
author: craigg-msft
ms.author: craigg
manager: jhubbard
monikerRange: "= sql-server-2016 || = sqlallproducts-allversions"
---

# SQL Server 2016 Release Notes
[!INCLUDE[tsql-appliesto-ss2016-xxxx-xxxx-xxx-md](../includes/tsql-appliesto-ss2016-xxxx-xxxx-xxx-md.md)]
  This article describes limitations and issues with SQL Server 2016 releases, including service packs. For information on what's new, see [What's New in SQL Server 2016](https://docs.microsoft.com/sql/sql-server/what-s-new-in-sql-server-2016).

- [![Download from Evaluation Center](../includes/media/download2.png)](https://www.microsoft.com/evalcenter/evaluate-sql-server-2016)  Download SQL Server 2016  from the **[Evaluation Center](https://www.microsoft.com/evalcenter/evaluate-sql-server-2016)**
- [![Azure Virtual Machine small](../includes/media/azure-vm.png)](https://azuremarketplace.microsoft.com/marketplace/apps/microsoftsqlserver.sql2016sp1-ws2016) Have an Azure account?  Then go **[Here](https://azuremarketplace.microsoft.com/marketplace/apps/microsoftsqlserver.sql2016sp1-ws2016)** to spin up a Virtual Machine with SQL Server 2016 SP1 already installed.
- [![Download SSMS](../includes/media/download2.png)](../ssms/download-sql-server-management-studio-ssms.md) To get the latest version of SQL Server Management Studio, see **[Download SQL Server Management Studio (SSMS)](../ssms/download-sql-server-management-studio-ssms.md)**.

## <a name="bkmk_2016sp2"></a>SQL Server 2016 Service Pack 2 (SP2)

![info_tip](../sql-server/media/info-tip.png) SQL Server 2016 SP2 includes all cumulative updates released after 2016 SP1, up to and including CU8.

- [![Microsoft Download Center](../includes/media/download2.png)](https://go.microsoft.com/fwlink/?linkid=869608) [Download SQL Server 2016 Service Pack 2 (SP2)](https://go.microsoft.com/fwlink/?linkid=869608)
- For a complete list of updates, see [SQL Server 2016 Service Pack 2 release information](https://support.microsoft.com/help/4052908/sql-server-2016-service-pack-2-release-information)

The SQL Server 2016 SP2 installation may require reboot after installation. As a best practice, we recommend to plan and perform a reboot following the installation of SQL Server 2016 SP2.

Performance and Scale related improvements included in SQL Server 2016 SP2.

|Feature|Description|More information|
|---|---|---|
|Improved Distribution DB cleanup procedure	|	An oversized distribution database tables caused blocking and deadlock situation. An improved cleanup procedure aims to eliminate some of these blocking or deadlock scenarios.	|	[KB4040276](https://support.microsoft.com/help/4040276/fix-indirect-checkpoints-on-the-tempdb-database-cause-non-yielding)	|
|Change Tracking Cleanup	|	Improved change tracking cleanup performance and efficiency for Change Tracking side tables.	|	[KB4052129](https://support.microsoft.com//help/4052129/update-for-manual-change-tracking-cleanup-procedure-in-sql-server-2016)	|
|Use CPU time out to cancel Resource Governor request	|	Improves the handling of query requests by actually cancelling the request, if CPU thresholds for a request is reached. This behavior is enabled under trace flag 2422.	|	[KB4038419](https://support.microsoft.com/help/4038419/add-cpu-timeout-to-resource-governor-request-max-cpu-time-sec)	|
|SELECT INTO to create target table in filegroup	|	Starting with SQL Server 2016 SP2, SELECT INTO T-SQL syntax supports loading a table into a filegroup other than a default filegroup of the user using the ON <Filegroup name> keyword in T-SQL syntax.	|		|
|Improved Indirect Checkpoint for TempDB	|	Indirect checkpointing for TempDB is improved to minimize the spinlock contention on DPLists. This improvement allows TempDB workload on SQL Server 2016 to scale out of the box if indirect checkpointing is ON for TempDB.	|	[KB4040276](https://support.microsoft.com/help/4040276)	|
|Improved database backup performance on large memory machines	|	SQL Server 2016 SP2 optimizes the way we drain the on-going I/O during backup resulting in dramatic gains in backup performance for small to medium databases. We have seen more than 100x improvement when taking system database backups on a 2TB machine. The performance gain reduces as the database size increases as the pages to backup and backup I/O takes more time compared to iterating buffer pool. This change will help improve the backup performance for customers hosting multiple small databases on a large high end servers with large memory.	|		|
|VDI backup compression support for TDE enabled databases	|	SQL Server 2016 SP2, adds VDI support to allow VDI backup solutions to leverage compression for TDE enabled databases. With this improvement, a new backup format has been introduced to support backup compression for TDE enabled databases. The SQL Server engine will transparently handle new and old backup formats to restore the backups.	|		|
|Dynamic loading of replication agent profile parameters	|	This new enhancements allows replication agents parameters to be loaded dynamically without having to restart the agent. This change is applicable only to the most commonly used agent profile parameters.	|		|
|Support MAXDOP option for statistics create/update	|	 This enhancement allows to specify the MAXDOP option for a CREATE/UPDATE statistics statement, as well as make sure the right MAXDOP setting is used when statistics are updated as part of create or rebuild for all types of indexes (if the MAXDOP option is present)	|	[KB4041809](https://support.microsoft.com/help/4041809)	|
|Improved Auto Statistics Update for Incremental Statistics	|	 In certain scenarios, when a number of data changes happened across multiple partitions in a table in a way that the total modification counter for incremented statistics exceeds the auto update threshold, but none of the individual partitions exceed the auto update threshold, statistics update may be delayed until much more modifications happen in the table. This behavior is corrected under trace flag 11024.	|		|

Supportability and Diagnostics related improvements included in SQL Server 2016 SP2.

|Feature|Description|More information|
|---|---|---|
|Full DTC support for databases in an Availability Group	|	Cross-databases transactions for databases which are part of an Availability Group are currently not supported for SQL Server 2016. With SQL Server 2016 SP2, we are introducing full support for distributed transactions with Availability Group Databases.	|		|
|Update to sys.databases is_encrypted column to accurately reflect encryption status for TempDB	|	The value of is_encryptedcolumn column in sys.databases is 1 for TempDB, even after you turn off encryption for all user databases and restart SQL Server. The expected behavior would be that the value for this is 0, since TempDB is no longer encrypted in this situation. Starting with SQL Server 2016 SP2, sys.databases.is_encrypted now accurately reflects encryption status for TempDB.	|		|
|New DBCC CLONEDATABASE options to generate verified clone and backup	|	With SQL Server 2016 SP2, DBCC CLONEDATABASE allows two new options:  produce a verified clone, or produce a backup clone. When a clone database is created using WITH VERIFY_CLONEDB option, a consistent database clone is created and verified which will be supported by Microsoft for production use. A new property is introduced to validate if the clone is verified SELECT DATABASEPROPERTYEX('clone_database_name', 'IsVerifiedClone'). When a clone is created with BACKUP_CLONEDB option, a backup is generated in the same folder as the data file to make it easy for customers to move the clone to different server or to send it to Microsoft Customer Support (CSS) for troubleshooting.	|		|
|Service Broker (SSB) support for DBCC CLONEDATABASE	|	Enhanced DBCC CLONEDATABASE command to allow scripting of SSB objects.	|	[KB4092075](https://support.microsoft.com/help/4092075)	|
|New DMV to monitor TempDB version store space usage	|	A new sys.dm_tran_version_store_space_usage DMV is introduced in SQL Server 2016 SP2 to allow monitoring TempDB for version store usage. DBAs can now proactively plan TempDB sizing based on the version store usage requirement per database, without any performance overhead when running it on production servers.	|		|
|Full Dumps support for Replication Agents | Today if replication agents encounter a unhandled exception, the default is to create a mini dump of the exception symptoms. This makes troubleshooting unhandled exception issues very difficult. Through this change we are introducing a new Registry key, which would allow to create a full dump for Replication Agents.	|		|
|Extended Events enhancement for read routing failure for an Availability Group	|	Before, the read_only_rout_fail xEvent fired if there was a routing list present, but none of the servers in the routing list were available for connections. SQL Server 2016 SP2 includes additional information to assist with troubleshooting, and also expand on the code points where this xEvent gets fired.	|		|
|New DMV to monitor the transaction log |	Added a new DMV sys.dm_db_log_stats that returns summary level attributes and information about transaction log files of databases.	|		|
|New DMV to monitor VLF information |	A new DMV sys.dm_db_log_info is introduced in SQL Server 2016 SP2 to expose the VLF information similar to DBCC LOGINFO to monitor, alert and avert potential T-Log issues experienced by customers.	|		|
|Processor Information in sys.dm_os_sys_info|	New columns added to the sys.dm_os_sys_info DMV to expose the processor related information, such as socket_count, and cores_per_numa.	|		|
|Extent modified information in sys.dm_db_file_space_usage|	New column added to sys.dm_db_file_space_usage to track the number of modified extents since the last full backup.	|		|
|Segment information in sys.dm_exec_query_stats	|	New columns were added to sys.dm_exec_query_stats to track number of columnstore segments skipped and read, such as total_columnstore_segment_reads, and total_columnstore_segment_skips.	|	[KB4051358](https://support.microsoft.com/help/4051358)	|
|Setting correct compatibility level for distribution database	|	After Service Pack installation, the Distribution database compatibility level changes to 90. This was because of an code path in sp_vupgrade_replication stored procedure. The SP has now been changed to set the correct compatibility level for the distribution database.	|		|
|Expose last known good DBCC CHECKDB information	|	A new database option has been added to programmatically return the date of the last successful DBCC CHECKDB run. Users can now query DATABASEPROPERTYEX([database], 'lastgoodcheckdbtime') to obtain a single value representing the date/time of the last successful DBCC CHECKDB run on the specified database.	|		|
|Showplan XML enhancements|	[Information on which statistics were used to compile the query plan](https://blogs.msdn.microsoft.com/sql_server_team/sql-server-2017-showplan-enhancements/), including statistics name, modification counter, sampling percent, and when the statistics was updated last time. Note this is added for CE models 120 and later only. For example it is not supported for CE 70.| |
| |A new attribute [EstimateRowsWithoutRowgoal](https://blogs.msdn.microsoft.com/sql_server_team/more-showplan-enhancements-row-goal/) is added to showplan XML if Query Optimizer uses "row goal" logic.| |
| |New runtime attributes [UdfCpuTime and UdfElapsedTime](https://blogs.msdn.microsoft.com/sql_server_team/more-showplan-enhancements-udfs/) in actual showplan XML, to track time spent in scalar User-Defined Functions (UDF).| |
| |Add CXPACKET wait type to [list of possible top 10 waits](https://blogs.msdn.microsoft.com/sql_server_team/new-showplan-enhancements/) in actual showplan XML - Parallel query execution frequently involves CXPACKET waits, but this type of wait was not reporting in actual showplan XML.	|		|
| |Extended the runtime spill warning to report number of pages written to TempDB during a parallelism operator spill.| |
|Replication Support for databases with Supplemental characters collations	|	Replication is now supportable on databases which use the Supplemental Character Collation.	|		|
|Proper handling of Service Broker with Availability group failover	|	In the current implementation when Service Broker is enabled on an Availability Group Databases, during an AG failover all Service broker connections which originated on the Primary Replica are left open. This improvement targets to close all such open connections during an AG failover.	|		|
|Improved parallelism waits troubleshooting	|	by adding a new [CXCONSUMER](https://blogs.msdn.microsoft.com/sql_server_team/making-parallelism-waits-actionable/) wait.	|		|
|Improved consistency between DMVs for same information	|	The sys.dm_exec_session_wait_stats DMV now tracks CXPACKET and CXCONSUMER waits consistently with the sys.dm_os_wait_stats DMV.	|		|
|Improved troubleshooting of intra-query parallelism deadlocks | A new exchange_spill Extended Event to report the number of pages written to TempDB during a parallelism operator spill, in the xEvent field name worktable_physical_writes.| |
| |The spills columns in the sys.dm_exec_query_stats, sys.dm_exec_procedure_stats, and sys.dm_exec_trigger_stats DMVs (such as total_spills) now also include the data spilled by parallelism operators.| |
| |The XML deadlock graph is improved for parallelism deadlock scenarios, with more attributes added to the exchangeEvent resource.| |
| |The XML deadlock graph is improved for deadlocks involving batch-mode operators, with more attributes added to the SyncPoint resource.| |
|Dynamic reloading of some replication agent profile parameters	|	In the current implementation of replication agents any change in the agent profile parameter requires the agent to be stopped and restarted. This improvements allows for the parameters to be dynamically reloaded without having to restart the replication agent.	|		|

![horizontal-bar.png](media/horizontal-bar.png)

## <a name="bkmk_2016sp1"></a>SQL Server 2016 Service Pack 1 (SP1)
![info_tip](../sql-server/media/info-tip.png) SQL Server 2016 SP1 includes all cumulative updates up to SQL Server 2016 RTM CU3 including Security Update MS16-136. It contains a roll-up of solutions provided in SQL Server 2016 cumulative updates up to and includes the latest Cumulative Update - CU3 and Security Update MS16-136 released on November 8th, 2016.

The following features are available in the Standard, Web, Express, and Local DB editions of SQL Server SP1 (except as noted):
- Always encrypted
- Changed data capture (not available in Express)
- Columnstore
- Compression
- Dynamic data masking
- Fine grain auditing
- In Memory OLTP (not availabe in Local DB)
- Multiple filestream containers (not available in Local DB)
- Partitioning
- PolyBase
- Row level security

The following table summarizes key improvements provided in SQL Server 2016 SP1.

|Feature|Description|More information|
|---|---|---|
|Bulk insert into heaps with auto TABLOCK under TF 715| Trace Flag 715 enables table lock for bulk load operations into heap with no non-clustered indexes.|[Migrating SAP workloads to SQL Server just got 2.5x faster](https://blogs.msdn.microsoft.com/sql_server_team/migrating-sap-workloads-to-sql-server-just-got-2-5x-faster/)|
|CREATE OR ALTER|Deploy objects such as Stored Procedures, Triggers, User-Defined Functions, and Views.|[SQL Server Database Engine Blog](https://blogs.msdn.microsoft.com/sqlserverstorageengine/2016/11/17/create-or-alter-another-great-language-enhancement-in-sql-server-2016-sp1/)|
|DROP TABLE support for replication|DROP TABLE DDL support for replication to allow replication articles to be dropped.|[KB 3170123](https://support.microsoft.com/help/3170123/supports-drop-table-ddl-for-articles-that-are-included-in-transactiona)|
|Filestream RsFx Driver signing|The Filestream RsFx driver is signed and certified using Windows Hardware Developer Center Dashboard portal (Dev Portal) allowing SQL Server 2016 SP1 Filestream RsFx driver to be installed on Windows Server 2016/Windows 10 without any issue.|[Migrating SAP workloads to SQL Server just got 2.5x faster](https://blogs.msdn.microsoft.com/sql_server_team/migrating-sap-workloads-to-sql-server-just-got-2-5x-faster/)|
|LPIM to SQL service account - programmatic identification|Allow DBAs to programmatically identify if Lock Pages in Memory (LPIM) privilege is in effect at the service startup time.|[Developers Choice: Programmatically identify LPIM and IFI privileges in SQL Server](https://blogs.msdn.microsoft.com/sql_server_team/developers-choice-programmatically-identify-lpim-and-ifi-privileges-in-sql-server)|
|Manual Change Tracking Cleanup|New stored procedure cleans the change tracking internal table on demand.| [KB 3173157](https://support.microsoft.com/help/3173157/adds-a-stored-procedure-for-the-manual-cleanup-of-the-change-tracking)|
|Parallel INSERT..SELECT Changes for Local temp tables|New Parallel INSERT in INSERT..SELECT operations.|[SQL Server Customer Advisory Team](https://blogs.msdn.microsoft.com/sqlcat/2016/07/21/real-world-parallel-insert-what-else-you-need-to-know/)|
|Showplan XML|Extended diagnostics including grant warning and maximum memory enabled for a query, enabled trace flags, and also surfaces other diagnostic information. | [KB 3190761](https://support.microsoft.com/help/3190761/update-to-improve-diagnostics-by-expose-data-type-of-the-parameters-fo)|
|Storage class memory|Boost the transaction processing using Storage Class Memory in Windows Server 2016, resulting in the ability to accelerate transaction commit times by orders of magnitude.|[SQL Server Database Engine Blog](https://blogs.msdn.microsoft.com/sqlserverstorageengine/2016/12/02/transaction-commit-latency-acceleration-using-storage-class-memory-in-windows-server-2016sql-server-2016-sp1/)|
|USE HINT|Use the query option, `OPTION(USE HINT('<option>'))` to alter query optimizer behavior using supported query level hints. Unlike QUERYTRACEON, the USE HINT option does not require sysadmin privileges.|[Developers Choice: USE HINT query hints](https://blogs.msdn.microsoft.com/sql_server_team/developers-choice-use-hint-query-hints/)|
|XEvent additions|New XEvents and Perfmon diagnostics capabilities improve latency troubleshooting.|[Extended Events](https://docs.microsoft.com/sql/relational-databases/extended-events/extended-events)|

In addition, note the following fixes:
- Based on feedback from DBAs and SQL community, starting SQL 2016 SP1, the Hekaton logging messages are reduced to minimal.
- Review new [Trace flags](https://docs.microsoft.com/sql/t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql).
- The full versions of the WideWorldImporters sample databases now work with Standard Edition and Express Edition, starting SQL Server 2016 SP1 and are available on [Github]( https://github.com/Microsoft/sql-server-samples/releases/tag/wide-world-importers-v1.0). No changes are needed in the sample. The database backups created at RTM for Enterprise edition work with Standard and Express in SP1.

The SQL Server 2016 SP1 installation may require reboot post installation. As a best practice, we recommend to plan and perform a reboot following the installation of SQL Server 2016 SP1.

### Download pages and more information

- [Download Service Pack 1 for Microsoft SQL Server 2016](https://www.microsoft.com/download/details.aspx?id=54276)
- [SQL Server 2016 Service Pack 1 (SP1) Released](https://blogs.msdn.microsoft.com/sqlreleaseservices/sql-server-2016-service-pack-1-sp1-released/)
- [SQL Server 2016 Service Pack 1 release information](https://support.microsoft.com/kb/3182545)
- ![info_tip](../sql-server/media/info-tip.png) [SQL Server Update Center](https://msdn.microsoft.com/library/ff803383.aspx) for links and information for all supported versions, including service packs of [!INCLUDE[ssNoVersion_md](../includes/ssnoversion-md.md)]

![horizontal-bar.png](media/horizontal-bar.png)

##  <a name="bkmk_2016_ga"></a> SQL Server 2016 Release - General Availability (GA)
-   [Database Engine (GA)](#bkmk_ga_instalpatch)
-   [Stretch Database (GA)](#bkmk_ga_stretch)
-   [Query Store (GA)](#bkmk_ga_query_store)
-   [Product Documentation (GA)](#bkmk_ga_docs)

### ![repl_icon_warn](../database-engine/availability-groups/windows/media/repl-icon-warn.gif) <a name="bkmk_ga_instalpatch"></a> Install Patch Requirement (GA)
**Issue and customer impact:** Microsoft has identified a problem that affects the Microsoft VC++ 2013 Runtime binaries that are installed as a prerequisite by SQL Server 2016. An update is available to fix this problem. If this update to the VC runtime binaries is not installed, SQL Server 2016 may experience stability issues in certain scenarios. Before you in stall SQL Server 2016, check to see if the computer needs the patch described in [KB 3164398](https://support.microsoft.com/kb/3164398). The patch is also included in [Cumulative Update Package 1 (CU1) for SQL Server 2016 RTM](https://www.microsoft.com/download/details.aspx?id=53338).

**Resolution:** Use one of the following solutions:

- Install [KB 3138367 - Update for Visual C++ 2013 and Visual C++ Redistributable Package](https://support.microsoft.com/kb/3138367). The KB is the preferred resolution. You can install this before or after you install SQL Server 2016.

    If SQL Server 2016 is already installed, do the following steps in order:

    1.  Download the appropriate *vcredist_\*exe*.
    1.  Stop the SQL Server service for all instances of the database engine.
    1.  Install **KB 3138367**.
    1.  Reboot the computer.


 - Install  [KB 3164398 - Critical Update for SQL Server 2016 MSVCRT prerequisites](https://support.microsoft.com/kb/3164398).

    If you use **KB 3164398**, you can install during SQL Server installation, through Microsoft Update, or from Microsoft Download Center.

    - **During SQL Server 2016 Installation:** If the computer running SQL Server setup has internet access, SQL Server setup checks for the update as part of the overall SQL Server installation. If you accept the update, setup downloads and update the binaries during installation.

    - **Microsoft Update:** The update is available from Microsoft Update as a critical non-security SQL Server 2016 update. Installing through Microsoft update, after SQL Server 2016 requires the server to be restarted following the update.

    - **Download Center:** Finally, the update is available from the Microsoft Download Center. You can download the software for the update and install it on servers after they have SQL Server 2016.


### <a name="bkmk_ga_stretch"></a>Stretch Database

#### Problem with a specific character in a database or table name

**Issue and customer impact:** Attempting to enable Stretch Database on a database or a table fails with an error. The issue occurs when the name of the object includes a character that's treated as a different character when converted from lower case to upper case. An example of a character that causes this issue is the character "ƒ" (created by typing ALT+159).

**Workaround:** If you want to enable Stretch Database on the database or the table, the only option is to rename the object and remove the problem character.

#### Problem with an index that uses the INCLUDE keyword

**Issue and customer impact:** Attempting to enable Stretch Database on a table that has an index that uses the INCLUDE keyword to include additional columns in the index fails with an error.

**Workaround:** Drop the index that uses the INCLUDE keyword, enable Stretch Database on the table, then recreate the index. If you do this, be sure to follow your organization's maintenance practices and policies to ensure minimal or no impact to users of the affected table.

### <a name="bkmk_ga_query_store"></a>Query Store

#### Problem with automatic data cleanup on editions other than Enterprise and Developer

 **Issue and customer impact:** Automatic data cleanup fails on editions other than Enterprise and Developer.
Consequently, if data is not purged manually,  space used by the Query Store will grow over time until configured limit is reached. If not mitigated, this issue will also fill up disk space allocated for the error logs, as every attempt to execute cleanup produces a dump file. Cleanup activation period depends on the workload frequency, but it is no longer than 15 min.

 **Workaround:** If you plan to use Query Store on editions other than Enterprise and Developer, you need to explicitly turn off cleanup policies. It can be done either from SQL Server Management Studio (Database Properties page) or via Transact-SQL script:

```ALTER DATABASE <database name> SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 0), SIZE_BASED_CLEANUP_MODE = OFF)```

Additionally, consider manual cleanup options to prevent Query Store from transitioning to read-only mode. For example, run the following query to periodically clean entire data space:

```ALTER DATABASE <database name> SET QUERY_STORE CLEAR```

Also, execute the following Query Store stored procedures periodically to clean runtime statistics, specific queries or plans:

- `sp_query_store_reset_exec_stats`

- `sp_query_store_remove_plan`

- `sp_query_store_remove_query`


###  <a name="bkmk_ga_docs"></a> Product Documentation (GA)
 **Issue and customer impact:** A downloadable version of the SQL Server 2016 documentation is not yet available. When you use Help Library Manager to attempt to **Install content from online**, you see the SQL Server 2012 and SQL Server 2014 documentation but there are no options for SQL Server 2016 documentation.

 **Workaround:** Use one of the following work-arounds:

 ![Manage Help Settings for SQL Server](../sql-server/media/docs-sql2016-managehelpsettings.png "Manage Help Settings for SQL Server")

-   Use the option **Choose online or local help** and configure help for "I want to use online help".

-   Use the option **Install content from online** and download the SQL Server 2014 Content.

 **F1 Help:** By design when you press F1 in [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)], the online version of the F1 Help article is displayed in the browser. The issues is browser-based help even when you have configured and installed local Help.

**Updating content:**
In SQL Server Management Studio and Visual Studio, the Help Viewer application may freeze (hang) during the process of adding the documentation. To resolve this issue, complete the following steps. For more information about this issue, see [Visual Studio Help Viewer freezes](https://msdn.microsoft.com/library/mt654096.aspx).

* Open the %LOCALAPPDATA%\Microsoft\HelpViewer2.2\HlpViewer_SSMS16_en-US.settings | HlpViewer_VisualStudio14_en-US.settings file in Notepad and change the date in the following code to some date in the future.

```
     Cache LastRefreshed="12/31/2017 00:00:00"
```

## Additional Information
+ [SQL Server 2016 installation](../database-engine/install-windows/installation-for-sql-server-2016.md)
+ [SQL Server Update Center - links and information for all supported versions](https://msdn.microsoft.com/library/ff803383.aspx)

[!INCLUDE[get-help-options](../includes/paragraph-content/get-help-options.md)]

[!INCLUDE[contribute-to-content](../includes/paragraph-content/contribute-to-content.md)]

![MS_Logo_X-Small](../sql-server/media/ms-logo-x-small.png "MS_Logo_X-Small")
