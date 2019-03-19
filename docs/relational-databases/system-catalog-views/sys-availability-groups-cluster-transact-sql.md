---
title: "sys.availability_groups_cluster (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.availability_groups_cluster"
  - "availability_groups_cluster"
  - "availability_groups_cluster_TSQL"
  - "sys.availability_groups_cluster_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "Availability Groups [SQL Server], monitoring"
  - "sys.availability_groups_cluster catalog view"
  - "Availability Groups [SQL Server], WSFC clusters"
ms.assetid: d0f4683f-cdf0-4227-8b68-720ffe58f158
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# sys.availability_groups_cluster (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  Returns a row for each Always On availability group in the Windows Server Failover Clustering (WSFC) . Each row contains the availability group metadata from the WSFC cluster.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**group_id**|**uniqueidentifier**|Unique identifier (GUID) of the availability group.|  
|**name**|**sysname**|Name of the availability group. This is a user-specified name that must be unique within the Windows Server Failover Cluster (WSFC).|  
|**resource_id**|**nvarchar(40)**|Resource ID for the WSFC cluster resource.|  
|**resource_group_id**|**nvarchar(40)**|Resource Group ID for the WSFC cluster resource group of the availability group.|  
|**failure_condition_level**|**int**|User-defined failure condition level under which an automatic failover must be triggered, one of the following integer values:<br /><br /> 1: Specifies that an automatic failover should be initiated when any of the following occurs: <br />- The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service is down.<br />- The lease of the availability group for connecting to the WSFC failover cluster expires because no ACK is received from the server instance. For more information, see [How It Works: SQL Server Always On Lease Timeout](https://blogs.msdn.com/b/psssql/archive/2012/09/07/how-it-works-sql-server-Always%20On-lease-timeout.aspx).<br /><br /> 2: Specifies that an automatic failover should be initiated when any of the following occurs:  <br />- The instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] does not connect to cluster, and the user-specified **health_check_timeout** threshold of the availability group is exceeded. <br />- The availability replica is in failed state. <br />3: Specifies that an automatic failover should be initiated on critical [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] internal errors, such as orphaned spinlocks, serious write-access violations, or too much dumping. This is the default value. <br />4: Specifies that an automatic failover should be initiated on moderate [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] internal errors, such as a persistent out-of-memory condition in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] internal resource pool.<br />5: Specifies that an automatic failover should be initiated on any qualified failure conditions, including:<br />- Exhaustion of SQL Engine worker-threads. <br />- Detection of an unsolvable deadlock.<br /><br /> The failure-condition levels (1-5) range from the least restrictive, level 1, to the most restrictive, level 5. A given condition level encompasses all of the less restrictive levels. Thus, the strictest condition level, 5, includes the four less restrictive condition levels (1-4), level 4 includes levels 1-3, and so forth.<br /><br /> To change this value, use the FAILURE_CONDITION_LEVEL option of the [ALTER AVAILABILITY GROUP](../../t-sql/statements/alter-availability-group-transact-sql.md)[!INCLUDE[tsql](../../includes/tsql-md.md)] statement.|  
|**health_check_timeout**|**int**|Wait time (in milliseconds) for the [sp_server_diagnostics](../../relational-databases/system-stored-procedures/sp-server-diagnostics-transact-sql.md) system stored procedure to return server-health information, before the server instance is assumed to be slow or hung. The default value is 30000 milliseconds (30 seconds).<br /><br /> To change this value, use the HEALTH_CHECK_TIMEOUT option of [ALTER AVAILABILITY GROUP](../../t-sql/statements/alter-availability-group-transact-sql.md)[!INCLUDE[tsql](../../includes/tsql-md.md)] statement.|  
|**automated_backup_preference**|**tinyint**|Preferred location for performing backups on the availability databases in this availability group. One of the following values:<br /><br /> 0: Primary. Backups should always occur on the primary replica.<br />1: Secondary only. Performing backups on a secondary replica is preferable.<br />2: Prefer Secondary. Performing backups on a secondary replica preferable, but performing backups on the primary replica is acceptable if no secondary replica is available for backup operations. This is the default behavior.<br />3: Any Replica. No preference about whether backups are performed on the primary replica or on a secondary replica.<br /><br /> For more information, see [Active Secondaries: Backup on Secondary Replicas &#40;Always On Availability Groups&#41;](../../database-engine/availability-groups/windows/active-secondaries-backup-on-secondary-replicas-always-on-availability-groups.md).|  
|**automated_backup_preference_desc**|**nvarchar(60)**|Description of **automated_backup_preference**, one of:<br /><br /> PRIMARY<br /><br /> SECONDARY_ONLY<br /><br /> SECONDARY<br /><br /> NONE|  
  
## Security  
  
### Permissions  
 Requires VIEW ANY DEFINITION permission on the server instance.  
  
## See Also  
 [sys.availability_replicas &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-availability-replicas-transact-sql.md)   
 [Always On Availability Groups &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/always-on-availability-groups-sql-server.md)   
 [Monitor Availability Groups &#40;Transact-SQL&#41;](../../database-engine/availability-groups/windows/monitor-availability-groups-transact-sql.md)   
 [Monitor Availability Groups &#40;Transact-SQL&#41;](../../database-engine/availability-groups/windows/monitor-availability-groups-transact-sql.md)  
  
  
