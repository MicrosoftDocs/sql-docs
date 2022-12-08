---
title: "MSqreader_agents (Transact-SQL)"
description: MSqreader_agents (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "MSqreader_agents_TSQL"
  - "MSqreader_agents"
helpviewer_keywords:
  - "MSqreader_agents system table"
dev_langs:
  - "TSQL"
ms.assetid: dfa1f45e-c531-4385-a097-0a9edd1d7eab
---
# MSqreader_agents (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The **MSqreader_agents** table contains one row for each Queue Reader Agent running at the local Distributor. This table is stored in the distribution database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**id**|**int**|The ID of the Queue Reader Agent.|  
|**name**|**nvarchar(100)**|The name of the Queue Reader Agent.|  
|**job_id**|**binary(16)**|The unique job ID number from **sysjobs** table.|  
|**profile_id**|**int**|The profile ID from the **MSagent_profiles** table.|  
|**job_step_uid**|**uniqueidentifier**|The unique ID of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent job step in which the agent is started.|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)  
  
  
