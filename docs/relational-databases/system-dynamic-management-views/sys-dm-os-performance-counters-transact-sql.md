---
title: "sys.dm_os_performance_counters (Transact-SQL)"
description: sys.dm_os_performance_counters (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/22/2021"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "dm_os_performance_counters"
  - "sys.dm_os_performance_counters_TSQL"
  - "dm_os_performance_counters_TSQL"
  - "sys.dm_os_performance_counters"
helpviewer_keywords:
  - "sys.dm_os_performance_counters dynamic management view"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.dm_os_performance_counters (Transact-SQL)
[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Returns a row per performance counter maintained by the server. For information about each performance counter, see [Use SQL Server Objects](../../relational-databases/performance-monitor/use-sql-server-objects.md).  
  
> [!NOTE]  
>  To call this from [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)] or [!INCLUDE[ssPDW](../../includes/sspdw-md.md)], use the name `sys.dm_pdw_nodes_os_performance_counters`. [!INCLUDE[synapse-analytics-od-unsupported-syntax](../../includes/synapse-analytics-od-unsupported-syntax.md)]  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**object_name**|**nchar(128)**|Category to which this counter belongs.|  
|**counter_name**|**nchar(128)**|Name of the counter. To get more information about a counter, this is the name of the topic to select from the list of counters in [Use SQL Server Objects](../../relational-databases/performance-monitor/use-sql-server-objects.md). |  
|**instance_name**|**nchar(128)**|Name of the specific instance of the counter. Often contains the database name.|  
|**cntr_value**|**bigint**|Current value of the counter.<br /><br /> **Note:** For per-second counters, this value is cumulative. The rate value must be calculated by sampling the value at discrete time intervals. The difference between any two successive sample values is equal to the rate for the time interval used.|  
|**cntr_type**|**int**|Type of counter as defined by the Windows performance architecture. See [WMI Performance Counter Types](/windows/desktop/WmiSdk/wmi-performance-counter-types) on Docs or your Windows Server documentation for more information on performance counter types.|  
|**pdw_node_id**|**int**|**Applies to**: [!INCLUDE[ssSDWfull](../../includes/sssdwfull-md.md)], [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]<br /><br /> The identifier for the node that this distribution is on.|  
  
## Remarks  
 If the installation instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] fails to display the performance counters of the Windows operating system, use the following [!INCLUDE[tsql](../../includes/tsql-md.md)] query to confirm that performance counters have been disabled.  
  
```sql  
SELECT COUNT(*) FROM sys.dm_os_performance_counters;  
```  
  
If the return value is 0 rows, this means that the performance counters have been disabled. You should then look at the setup log and search for error 3409, `Reinstall sqlctr.ini for this instance, and ensure that the instance login account has correct registry permissions.`
This denotes that performance counters were not enabled. The errors immediately before the 3409 error should indicate the root cause for the failure of performance counter enabling. For more information about setup log files, see [View and Read SQL Server Setup Log Files](../../database-engine/install-windows/view-and-read-sql-server-setup-log-files.md).  

Performance counters where the `cntr_type` column value is 65792 display a snapshot of the last observed value only, not an average. 

Performance counters where the `cntr_type` column value is 272696320 or 272696576 display the average number of operations completed during each second of the sample interval. Counters of this type measure time in ticks of the system clock. For example, to get a snapshot-like reading of the last second only for the `Buffer Manager:Lazy writes/sec` and `Buffer Manager:Checkpoint pages/sec` counters, you must compare the delta between two collection points that are one second apart.    

Performance counters where the `cntr_type` column value is 537003264 display the ratio of a subset to its set as a percentage. For example, the `Buffer Manager:Buffer cache hit ratio` counter compares the total number of cache hits and the total number of cache lookups. As such, to get a snapshot-like reading of the last second only, you must compare the delta between the current value and the base value (denominator) between two collection points that are one second apart. The corresponding base value is the performance counter `Buffer Manager:Buffer cache hit ratio base` where the `cntr_type` column value is 1073939712.

Performance counters where the `cntr_type` column value is 1073874176 display how many items are processed on average, as a ratio of the items processed to the number of operations. For example, the `Locks:Average Wait Time (ms)` counters compares the lock waits per second with the lock requests per second, to display the average amount of wait time (in milliseconds) for each lock request that resulted in a wait. As such, to get a snapshot-like reading of the last second only, you must compare the delta between the current value and the base value (denominator) between two collection points that are one second apart. The corresponding base value is the performance counter `Locks:Average Wait Time Base` where the `cntr_type` column value is 1073939712.

Data in the `sys.dm_os_performance_counters` DMV is not persisted after the database engine restarts. Use the `sqlserver_start_time` column in [sys.dm_os_sys_info](sys-dm-os-sys-info-transact-sql.md) to find the last database engine startup time.   

## Permission

On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] and SQL Managed Instance, requires `VIEW SERVER STATE` permission.

On SQL Database **Basic**, **S0**, and **S1** service objectives, and for databases in **elastic pools**, the [server admin](/azure/azure-sql/database/logins-create-manage#existing-logins-and-user-accounts-after-creating-a-new-database) account, the [Azure Active Directory admin](/azure/azure-sql/database/authentication-aad-overview#administrator-structure) account, or membership in the `##MS_ServerStateReader##` [server role](/azure/azure-sql/database/security-server-roles) is required. On all other SQL Database service objectives, either the `VIEW DATABASE STATE` permission on the database, or membership in the `##MS_ServerStateReader##` server role is required.   
 
## Examples  
 The following example returns all performance counters that display snapshot counter values.  
  
```sql  
SELECT object_name, counter_name, instance_name, cntr_value, cntr_type  
FROM sys.dm_os_performance_counters
WHERE cntr_type = 65792 OR cntr_type = 272696320 OR cntr_type = 537003264;  
```  
  
## See Also  
  [SQL Server Operating System Related Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sql-server-operating-system-related-dynamic-management-views-transact-sql.md)   
 [sys.sysperfinfo &#40;Transact-SQL&#41;](../../relational-databases/system-compatibility-views/sys-sysperfinfo-transact-sql.md)  
 [sys.dm_os_sys_info  &#40;Transact-SQL&#41;](sys-dm-os-sys-info-transact-sql.md)
