---
title: "SQL Server 2012 SP4 Release Notes | Microsoft Docs"
ms.prod: "sql-non-specified"
ms.prod_service: "sql-non-specified"
ms.service: ""
ms.component: "sql-non-specified"
ms.technology: "server-general"
ms.custom: ""
ms.date: "10/05/2017"
ms.reviewer: ""
ms.suite: "sql"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 67cb8b3e-3d82-47f4-840d-0f12a3bff565
caps.latest.revision: 0
author: "craigg-msft"
ms.author: "craigg"
manager: "jhubbard"
ms.workload: "Inactive"
---
# SQL Server 2012 SP4 release notes
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]
This topic summarizes the improvements included with SQL Server 2012 SP4. The topics also describes issues to review before you install or troubleshoot the installation of SP4. Release Notes are available online only, not on the installation media. This topic is updated periodically as issues are discovered. For a detailed list of fixes in SP4, see [SQL Server 2012 SP4 Release](https://go.microsoft.com/fwlink/?linkid=846937).  

> Service pack 4 includes all of the SQL Server 2012 SP3 cumulative updates.
  
##Download pages
The following items link to the primary download packages for SQL Server 2012 SP3. Download pages have system requirements and basic installation instructions.
- [SQL Server 2012 SP4 Patch installation](https://go.microsoft.com/fwlink/?linkid=846829)
- [SQL Server 2012 SP4 Express](https://go.microsoft.com/fwlink/?linkid=846905)
- [Microsoft SQL Server 2012 SP4 Feature Pack](https://go.microsoft.com/fwlink/?linkid=846907)

## 	SP4 performance and scale improvements

- **Improved distribution agent cleanup procedure** - An oversized distribution database caused blocking and deadlock situation. An improved cleanup procedure aims to eliminate some of these blocking or deadlock scenarios. 
- **Dynamic Memory Object Scaling** - Dynamically partition memory objects based on number of nodes and cores to scale on modern hardware. The goal of dynamic promotion is to prevent potential bottlenecks and automatically partition a thread safe memory object. Unpartitioned memory objects can be dynamically promoted to be partitioned by node. The number of partitions equals the number of NUMA nodes. Memory objects partitioned by node can by further promoted to be partitioned by CPU, where the number of partitions equals number of CPUs.
- **Enable > 8TB for Buffer Pool** - Enable 128-TB Virtual address space for buffer pool usage
- **Change Tracking Cleanup** - Improved change tracking cleanup performance and efficiency for Change Tracking side tables. 

## SP4 supportability and diagnostics improvements

- **Full dumps support for Replication Agents** - Today if replication agents encounter an unhandled exception, the default behavior is to create a mini dump of the exception symptoms. The default behavior requires complex troubleshooting steep for unhandled exceptions. SP4 introduces a new Registry key, which supports the creation of a full dump for Replication Agents.
- **Enhanced diagnostics in showplan XML** - Showplan XML has been enhanced to expose information about enabled trace flags, memory fractions for optimized nested loop join, CPU time, and elapsed time. 
- **Better correlation between diagnostics XE and DMVs** - Query_hash and query_plan_hash fields are used for identifying a query uniquely. DMV defines them as varbinary(8), while XEvent defines them as UINT64. Since SQL server does not have “unsigned bigint”, casting does not always work. This improvement introduces new XEvent action/filter columns equivalent to query_hash and query_plan_hash except they are defined as INT64 which can help correlating queries between XE and DMVs. 
- **Better memory grant/usage diagnostics** - New query_memory_grant_usage XEvent (backport from Server 2016 SP1)
- **Add protocol tracing to SSL negotiation steps**  - Add bit trace information for successful/failed negotiation, including the protocol etc. Can be useful when troubleshooting connectivity scenarios while, for example, deploying TLS 1.2
- **Setting correct compatibility level for distribution database** - After Service Pack Installation the Distribution database compatibility level changes to 90. The level change was due to an issue in sp_vupgrade_replication stored procedure. The SP has now been changed to set the correct compatibility level for the distribution database. 
- **New DBCC command for cloning a database** - Clone database is a new DBCC command added that allows power users such as CSS to trouble shoot existing production databases by cloning the schema and metadata, without the data. The call is performed with DBCC clonedatabase (‘source_database_name’, ‘clone_database_name’). Cloned databases should not be used in production environments. To see if a database has been generated from a call to clone database you can use the following command, select DATABASEPROPERTYEX('clonedb', 'isClone').The return value of 1 is true, and 0 is false. 
- **TempDB file and file size information in SQL Error Log** - If size and auto growth is different for TempDB data files during startup, print the number of files and trigger a warning.
- **IFI support messages in SQL Server Error Log** -  Indicate in the error log that Database Instant File Initialization is enabled/disabled
- **New DMF to replace DBCC INPUTBUFFER** -  A new Dynamic Management Function sys.dm_input_buffer that takes the session_id as parameter is introduced to replace DBCC INPUTBUFFER
- **XEvents enhancement for read routing failure for an Availability Group** - Currently the read_only_rout_fail XEvent only gets fired if there is a routing list present, but none of the servers in the routing list is available for connections. This improvement includes additional information to assist with troubleshooting and it also expands on the code points where the XEvent gets fired. 
- **Improved handling of Service Broker with Availability group failover** - Currently when Service Broker is enabled on an Availability Group Databases, during an AG failover all Service broker connections that originated on the Primary Replica are left open. The improvement closes all such open connections during an AG failover.
- **[Automatic Soft NUMA partitioning](https://msdn.microsoft.com/library/ms345357(SQL.120).aspx)** – With SQL 2014 SP2, Automatic Soft NUMA is introduced when Trace Flag 8079 is enabled at the server level. When Trace Flag 8079 is enabled during startup, SQL Server 2014 SP2 interrogates the hardware layout and automatically configures Soft NUMA on systems reporting 8 or more CPUs per NUMA node. The automatic soft NUMA behavior is Hyperthread (HT/logical processor) aware. The partitioning and creation of additional nodes scales background processing by increasing the number of listeners, scaling, and network and encryption capabilities. It is recommended to first test the performance of the workload with Auto-Soft NUMA before it is turned ON in production.

## See Also
- [Install SQL Server 2012 Servicing Updates](https://msdn.microsoft.com/library/hh479746(v=sql.110).aspx)
- [How to identify your SQL Server version and edition](https://support.microsoft.com/en-us/help/321185)

[!INCLUDE[get-help-options](../includes/paragraph-content/get-help-options.md)]
