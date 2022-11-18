---
title: "MSmerge_agents (Transact-SQL)"
description: MSmerge_agents (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "MSmerge_agents"
  - "MSmerge_agents_TSQL"
helpviewer_keywords:
  - "MSmerge_agents system table"
dev_langs:
  - "TSQL"
ms.assetid: 639d2ebb-2c37-4fe0-b14b-1637bc5fc221
---
# MSmerge_agents (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The **MSmerge_agents** table contains one row for each Merge Agent running at the Subscriber. This table is stored in the distribution database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**id**|**int**|The ID of the Merge Agent.|  
|**name**|**nvarchar(100)**|The name of the Merge Agent.|  
|**publisher_id**|**smallint**|The ID of the Publisher.|  
|**publisher_db**|**sysname**|The name of the Publisher database.|  
|**publication**|**sysname**|The name of the publication.|  
|**subscriber_id**|**smallint**|The ID of the Subscriber.|  
|**subscriber_db**|**sysname**|The name of the subscription database.|  
|**local_job**|**bit**|Indicates whether there is a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent job on the local Distributor.|  
|**job_id**|**binary(16)**|The job identification number.|  
|**profile_id**|**int**|The configuration ID from the **MSagent_profiles** table.|  
|**anonymous_subid**|**uniqueidentifier**|The ID of an anonymous agent.|  
|**subscriber_name**|**sysname**|The name of the Subscriber.|  
|**creation_date**|**datetime**|The date and time that the Distribution or Merge Agent was created.|  
|**offload_enabled**|**bit**|Specifies that the agent can be activated remotely.<br /><br /> **0** specifies the agent cannot be activated remotely.<br /><br /> **1** specifies the agent will be activated remotely, and on the remote computer specified in the offload_server property.|  
|**offload_server**|**sysname**|Specifies the network name of server to be used for remote agent activation.|  
|**sid**|**varbinary(85)**|The security identification number (SID) for the Distribution Agent or Merge Agent during its first execution.|  
|**subscriber_security_mode**|**smallint**|The security mode used by the agent when connecting to the Subscriber, which can be one of the following:<br /><br /> **0** = [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication.<br /><br /> **1** = [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows Authentication.|  
|**subscriber_login**|**sysname**|The login used when connecting to the Subscriber.|  
|**subscriber_password**|**nvarchar(524)**|The encrypted value of the password that is used when connecting to the Subscriber.|  
|**publisher_security_mode**|**smallint**|The security mode used by the agent when connecting to the Publisher, which can be one of the following:<br /><br /> **0** = [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication.<br /><br /> **1** = [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows Authentication.|  
|**publisher_login**|**sysname**|The login used when connecting to the Publisher.|  
|**publisher_password**|**nvarchar(524)**|The encrypted value of the password that is used when connecting to the Publisher.|  
|**job_step_uid**|**uniqueidentifier**|The unique ID of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent job step in which the agent is started.|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)  
  
  
