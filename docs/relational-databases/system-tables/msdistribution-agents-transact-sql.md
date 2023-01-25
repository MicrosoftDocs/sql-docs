---
title: "MSdistribution_agents (Transact-SQL)"
description: MSdistribution_agents (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "10/28/2015"
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "MSdistribution_agents_TSQL"
  - "MSdistribution_agents"
helpviewer_keywords:
  - "MSdistribution_agents system table"
dev_langs:
  - "TSQL"
ms.assetid: 0e8f0653-1351-41d1-95d2-40f6d5a050ca
---
# MSdistribution_agents (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The **MSdistribution_agents** table contains one row for each Distribution Agent running at the local Distributor. This table is stored in the distribution database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**id**|**int**|The ID of the Distribution Agent.|  
|**name**|**nvarchar(100)**|The name of the Distribution Agent.|  
|**publisher_database_id**|**int**|The ID of the Publisher database.|  
|**publisher_id**|**smallint**|The ID of the Publisher.|  
|**publisher_db**|**sysname**|The name of the Publisher database.|  
|**publication**|**sysname**|The name of the publication.|  
|**subscriber_id**|**smallint**|The ID of the Subscriber, used by well-known agents only. For anonymous agents, this column is reserved.|  
|**subscriber_db**|**sysname**|The Name of the subscription database.|  
|**subscription_type**|**int**|The type of subscription:<br /><br /> **0** = Push.<br /><br /> **1** = Pull.<br /><br /> **2** = Anonymous.|  
|**local_job**|**bit**|Indicates whether there is a SQL Server Agent job on the local Distributor.|  
|**job_id**|**binary(16)**|The job identification number.|  
|**subscription_guid**|**binary(16)**|The ID of the subscriptions of this agent.|  
|**profile_id**|**int**|The configuration ID from the [MSagent_profiles &#40;Transact-SQL&#41;](../../relational-databases/system-tables/msagent-profiles-transact-sql.md) table.|  
|**anonymous_subid**|**uniqueidentifier**|The ID of an anonymous agent.|  
|**subscriber_name**|**sysname**|The name of the Subscriber, used by anonymous agents only.|  
|**virtual_agent_id**|**int**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|**anonymous_agent_id**|**int**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|**creation_date**|**datetime**|The datetime when the Distribution or Merge Agent was created.|  
|**queue_id**|**sysname**|The identifier to locate the queue for queued updating subscriptions. For non-queued subscriptions, the value is NULL. For [!INCLUDE[msCoName](../../includes/msconame-md.md)] Message Queuing-based publications, the value is a GUID that uniquely identifies the queue to be used for the subscription. For SQL Server-based queue publications, the column contains the value **SQL**.<br /><br /> Note: Using [!INCLUDE[msCoName](../../includes/msconame-md.md)] Message Queuing has been deprecated and is no longer supported.|  
|**queue_status**|**int**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|**offload_enabled**|**bit**|Indicates whether the agent can be activated remotely.<br /><br /> **0** specifies that the agent cannot be activated remotely.<br /><br /> **1** specifies that the agent will be activated remotely, and on the remote computer specified in the *offload_server* property.|  
|**offload_server**|**sysname**|The network name of server to be used for remote agent activation.|  
|**dts_package_name**|**sysname**|The name of the DTS package. For example, for a package named **DTSPub_Package**, specify `@dts_package_name = N'DTSPub_Package'`.|  
|**dts_package_password**|**nvarchar(524)**|The password on the package.|  
|**dts_package_location**|**int**|The package location. The location of the package can be **distributor** or **subscriber**.|  
|**sid**|**varbinary(85)**|The security identification number (SID) for the Distribution Agent or Merge Agent during its first execution.|  
|**queue_server**|**sysname**|[!INCLUDE[ssInternalOnly](../../includes/ssinternalonly-md.md)]|  
|**subscriber_security_mode**|**smallint**|The security mode used by the agent when connecting to the Subscriber, which can be one of the following:<br /><br /> **0** = [!INCLUDE[msCoName](../../includes/msconame-md.md)] SQL Server Authentication<br /><br /> **1** = [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows Authentication.|  
|**subscriber_login**|**sysname**|The login used when connecting to the Subscriber.|  
|**subscriber_password**|**nvarchar(524)**|Is the encrypted value of the password that is used when connecting to the Subscriber.|  
|**reset_partial_snapshot_progress**|**bit**|Is if a partially downloaded snapshot will be discarded so that the entire snapshot process can start again.|  
|**job_step_uid**|**uniqueidentifier**|The unique ID of the SQL Server Agent job step in which the agent is started.|  
|**subscriptionstreams**|**tinyint**|Sets the number of connections allowed per Distribution Agent to apply batches of changes in parallel to a Subscriber. A range of values from 1 to 64 is supported.|  
|**memory_optimized**|**bit**|1 indicates that the subscriber can be used for memory optimized tables.|  
|**job_login**|**sysname**||  
|**job_password**|**nvarchar(524)**||  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)  
  
  
