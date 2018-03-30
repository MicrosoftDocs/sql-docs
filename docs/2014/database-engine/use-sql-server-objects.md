---
title: "Use SQL Server Objects | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "dbe-cross-instance"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "server performance [SQL Server], objects for monitoring"
  - "database monitoring [SQL Server], objects for monitoring"
  - "charts [SQL Server]"
  - "System Monitor [SQL Server], counters"
  - "counters [SQL Server], listed"
  - "objects [SQL Server], performance monitoring"
  - "objects [SQL Server], Windows System Monitor"
  - "performance counters [SQL Server], about performance counters"
  - "System Monitor [SQL Server], objects"
  - "performance counters [SQL Server]"
  - "counters [SQL Server], about performance counters"
  - "tuning databases [SQL Server], objects for monitoring"
  - "database performance [SQL Server], objects for monitoring"
  - "SQL Server, objects"
  - "monitoring performance [SQL Server], objects for monitoring"
  - "Windows System Monitor [SQL Server], objects"
  - "Windows System Monitor [SQL Server], counters"
  - "counters [SQL Server]"
  - "performance counters [SQL Server], listed"
ms.assetid: bcd731b1-3c4e-4086-b58a-af7a3af904ad
caps.latest.revision: 47
author: "craigg-msft"
ms.author: "rickbyh"
manager: "jhubbard"
---
# Use SQL Server Objects
  Microsoft [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides objects and counters that can be used by System Monitor to monitor activity in computers running an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. An object is any [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] resource, such as a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] lock or Windows process. Each object contains one or more counters that determine various aspects of the objects to monitor. For example, the **SQL Server Locks** object contains counters called **Number of Deadlocks/sec** and **Lock Timeouts/sec**.  
  
 Some objects have several instances if multiple resources of a given type exist on the computer. For example, the **Processor** object type will have multiple instances if a system has multiple processors. The **Databases** object type has one instance for each database on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Some object types (for example, the **Memory Manager** object) have only one instance. If an object type has multiple instances, you can add counters to track statistics for each instance, or in many cases, all instances at once. Counters for the default instance appear in the format **SQLServer:***\<object name>*. Counters for named instances appear in the format **MSSQL$***\<instance name>***:***\<counter name>* or **SQLAgent$***\<instance name>***:***\<counter name>*.  
  
 By adding or removing counters to the chart and saving the chart settings, you can specify the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] objects and counters that are monitored when System Monitor is started.  
  
 You can configure System Monitor to display statistics from any [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] counter. In addition, you can set a threshold value for any [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] counter and then generate an alert when a counter exceeds a threshold. For more information about setting an alert, see [Create a SQL Server Database Alert](../../2014/database-engine/create-a-sql-server-database-alert.md).  
  
> [!NOTE]  
>  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] statistics are displayed only when an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is installed. If you stop and restart an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the display of statistics is interrupted and resumes automatically. Also note that you will see [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] counters in the System Monitor snap-in even if [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is not running. On a clustered instance, performance counters only function on the node where [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is running.  
  
 This topic contains the following sections:  
  
-   [SQL Server Agent Performance Objects](#SQLServerAgentPOs)  
  
-   [Service Broker Performance Objects](#ServiceBrokerPOs)  
  
-   [SQL Server Performance Objects](#SQLServerPOs)  
  
-   [SQL Server Replication Performance Objects](#SQLServerReplicationPOs)  
  
-   [SSIS Pipeline Counters](#SsisPipelineCounters)  
  
-   [Required Permissions](#RequiredPermissions)  
  
##  <a name="SQLServerAgentPOs"></a> SQL Server Agent Performance Objects  
 The following table lists the performance objects provided for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent:  
  
|Performance object|Description|  
|------------------------|-----------------|  
|[SQLAgent:Alerts](../../2014/database-engine/sql-server-agent-alerts-object.md)|Provides information about [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent alerts.|  
|[SQLAgent:Jobs](../../2014/database-engine/sql-server-agent-jobs-object.md)|Provides information about [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent jobs.|  
|[SQLAgent:JobSteps](../../2014/database-engine/sql-server-agent-jobsteps-object.md)|Provides information about [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent job steps.|  
|[SQLAgent:Statistics](../../2014/database-engine/sql-server-agent-statistics-object.md)|Provides general information about [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent.|  
  
##  <a name="ServiceBrokerPOs"></a> Service Broker Performance Objects  
 The following table lists the performance objects provided for [!INCLUDE[ssSB](../../includes/sssb-md.md)].  
  
|Performance object|Description|  
|------------------------|-----------------|  
|[SQLServer:Broker Activation](../../2014/database-engine/sql-server-broker-activation-object.md)|Provides information about [!INCLUDE[ssSB](../../includes/sssb-md.md)]-activated tasks.|  
|[SQLServer:Broker Statistics](../../2014/database-engine/sql-server-broker-statistics-object.md)|Provides general [!INCLUDE[ssSB](../../includes/sssb-md.md)] information.|  
|[SQLServer:Broker Transport](../../2014/database-engine/sql-server-broker-and-dbm-transport-object.md)|Provides information on [!INCLUDE[ssSB](../../includes/sssb-md.md)] networking.|  
  
##  <a name="SQLServerPOs"></a> SQL Server Performance Objects  
 The following table describes [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] objects.  
  
|Performance object|Description|  
|------------------------|-----------------|  
|[SQLServer:Access Methods](../../2014/database-engine/sql-server-access-methods-object.md)|Searches through and measures allocation of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database objects (for example, the number of index searches or number of pages that are allocated to indexes and data).|  
|[SQLServer:Backup Device](../../2014/database-engine/sql-server-backup-device-object.md)|Provides information about backup devices used by backup and restore operations, such as the throughput of the backup device.|  
|[SQLServer:Buffer Manager](../../2014/database-engine/sql-server-buffer-manager-object.md)|Provides information about the memory buffers used by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], such as **freememory** and **buffer cache hit ratio**.|  
|[SQL Server:Buffer Node](../../2014/database-engine/sql-server-buffer-node.md)|Provides information about how frequently [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] requests and accesses free pages.|  
|[SQLServer:CLR](../../2014/database-engine/sql-server-clr-object.md)|Provides information about the common language runtime (CLR).|  
|[SQLServer:Cursor Manager by Type](../../2014/database-engine/sql-server-cursor-manager-by-type-object.md)|Provides information about cursors.|  
|[SQLServer:Cursor Manager Total](../../2014/database-engine/sql-server-cursor-manager-total-object.md)|Provides information about cursors.|  
|[SQLServer:Database Mirroring](../../2014/database-engine/sql-server-database-mirroring-object.md)|Provides information about database mirroring.|  
|[SQLServer:Databases](../../2014/database-engine/sql-server-databases-object.md)|Provides information about a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database, such as the amount of free log space available or the number of active transactions in the database. There can be multiple instances of this object.|  
|[SQL Server:Deprecated Features](../../2014/database-engine/sql-server-deprecated-features-object.md)|Counts the number of times that deprecated features are used.|  
|[SQLServer:Exec Statistics](../../2014/database-engine/sql-server-execstatistics-object.md)|Provides information about execution statistics.|  
|[SQLServer:General Statistics](../../2014/database-engine/sql-server-general-statistics-object.md)|Provides information about general server-wide activity, such as the number of users who are connected to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
|[SQL Server:HADR Availability Replica](../../2014/database-engine/sql-server-availability-replica.md)|Provides information about [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)][!INCLUDE[ssHADR](../../includes/sshadr-md.md)] availability replicas.|  
|[SQL Server:HADR Database Replica](../../2014/database-engine/sql-server-database-replica.md)|Provides information about [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)][!INCLUDE[ssHADR](../../includes/sshadr-md.md)] database replicas.|  
|[SQLServer:Latches](../../2014/database-engine/sql-server-latches-object.md)|Provides information about the latches on internal resources, such as database pages, that are used by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
|[SQLServer:Locks](../../2014/database-engine/sql-server-locks-object.md)|Provides information about the individual lock requests made by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], such as lock time-outs and deadlocks. There can be multiple instances of this object.|  
|[SQLServer:Memory Manager](../../2014/database-engine/sql-server-memory-manager-object.md)|Provides information about [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] memory usage, such as the total number of lock structures currently allocated.|  
|[SQLServer:Plan Cache](../../2014/database-engine/sql-server-plan-cache-object.md)|Provides information about the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] cache used to store objects such as stored procedures, triggers, and query plans.|  
|[SQLServer: Resource Pool Stats](../../2014/database-engine/sql-server-resource-pool-stats-object.md)|Provides information about Resource Governor resource pool statistics.|  
|[SQLServer:SQL Errors](../../2014/database-engine/sql-server-sql-errors-object.md)|Provides information about [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] errors.|  
|[SQLServer:SQL Statistics](../../2014/database-engine/sql-server-sql-statistics-object.md)|Provides information about aspects of [!INCLUDE[tsql](../../includes/tsql-md.md)] queries, such as the number of batches of [!INCLUDE[tsql](../../includes/tsql-md.md)] statements received by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
|[SQLServer:Transactions](../../2014/database-engine/sql-server-transactions-object.md)|Provides information about the active transactions in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], such as the overall number of transactions and the number of snapshot transactions.|  
|[SQLServer:User Settable](../../2014/database-engine/sql-server-user-settable-object.md)|Performs custom monitoring. Each counter can be a custom stored procedure or any [!INCLUDE[tsql](../../includes/tsql-md.md)] statement that returns a value to be monitored.|  
|[SQLServer: Wait Statistics](../../2014/database-engine/sql-server-wait-statistics-object.md)|Provides information about waits.|  
|[SQLServer: Workload Group Stats](../../2014/database-engine/sql-server-workload-group-stats-object.md)|Provides information about Resource Governor workload group statistics.|  
  
##  <a name="SQLServerReplicationPOs"></a> SQL Server Replication Performance Objects  
 The following table lists the performance objects provided for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] replication:  
  
|Performance object|Description|  
|------------------------|-----------------|  
|**SQLServer:Replication Agents**<br /><br /> **SQLServer:Replication Snapshot**<br /><br /> **SQLServer:Replication Logreader**<br /><br /> **SQLServer:Replication Dist.**<br /><br /> **SQLServer:Replication Merge**<br /><br /> For more information, see [Monitoring Replication with System Monitor](../../2014/relational-databases/replication/monitoring-replication-with-system-monitor.md).|Provides information about replication agent activity.|  
  
##  <a name="SsisPipelineCounters"></a> SSIS Pipeline Counters  
 For the **SSIS Pipeline** counter, see [Performance Counters](../../2014/integration-services/performance-counters.md).  
  
##  <a name="RequiredPermissions"></a> Required Permissions  
 Use of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] objects depends on Windows permissions, except **SQLAgent:Alerts**. Users must be a member of the **sysadmin** fixed server role to use **SQLAgent:Alerts**.  
  
## See Also  
 [Use Performance Objects](../../2014/database-engine/use-performance-objects.md)   
 [sys.dm_os_performance_counters &#40;Transact-SQL&#41;](../Topic/sys.dm_os_performance_counters%20\(Transact-SQL\).md)  
  
  