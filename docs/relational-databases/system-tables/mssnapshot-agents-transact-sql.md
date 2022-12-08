---
title: "MSsnapshot_agents (Transact-SQL)"
description: MSsnapshot_agents (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "MSsnapshot_agents"
  - "MSsnapshot_agents_TSQL"
helpviewer_keywords:
  - "MSsnapshot_agents system table"
dev_langs:
  - "TSQL"
ms.assetid: aeae0a2e-4c21-4c45-be65-1e426fa52bdd
---
# MSsnapshot_agents (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The **MSsnapshot_agents** table contains one row for each Snapshot Agent associated with the local Distributor. This table is stored in the distribution database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**id**|**int**|The ID of the Snapshot Agent.|  
|**name**|**nvarchar(100)**|The name of the Snapshot Agent.|  
|**publisher_id**|**smallint**|The ID of the Publisher.|  
|**publisher_db**|**sysname**|The name of the Publisher database.|  
|**publication**|**sysname**|The name of the publication.|  
|**publication_type**|**int**|The type of publication:<br /><br /> **0** = Transactional.<br /><br /> **1** = Snapshot.<br /><br /> **2** = Merge.|  
|**local_job**|**bit**|Indicates whether there is a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent job on the local Distributor.|  
|**job_id**|**binary(16)**|The job identification number.|  
|**profile_id**|**int**|The configuration ID from the [MSagent_profiles &#40;Transact-SQL&#41;](../../relational-databases/system-tables/msagent-profiles-transact-sql.md) table.|  
|**dynamic_filter_login**|**sysname**|The value used for evaluating the [SUSER_SNAME &#40;Transact-SQL&#41;](../../t-sql/functions/suser-sname-transact-sql.md) function in parameterized filters that define a partition. This column is used for a partitioned snapshot.|  
|**dynamic_filter_hostname**|**sysname**|The value used for evaluating the [HOST_NAME &#40;Transact-SQL&#41;](../../t-sql/functions/host-name-transact-sql.md) function in parameterized filters that define a partition. This column is used for a partitioned snapshot.|  
|**publisher_security_mode**|**smallint**|The security mode used by the agent when connecting to the Publisher, which can be one of the following:<br /><br /> **0** = [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication<br /><br /> **1** = [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows Authentication.|  
|**publisher_login**|**sysname**|The login used when connecting to the Publisher.|  
|**publisher_password**|**nvarchar(524)**|The encrypted value of the password that is used when connecting to the Publisher.|  
|**job_step_uid**|**uniqueidentifier**|The unique ID of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent job step in which the agent is started.|  
|**job_login**|**sysname**||  
|**job_password**|**nvarchar(524)**||  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)  
  
  
