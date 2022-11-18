---
title: "MSagent_profiles (Transact-SQL)"
description: MSagent_profiles (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "03/06/2017"
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "MSagent_profiles"
  - "MSagent_profiles_TSQL"
helpviewer_keywords:
  - "MSagent_profiles system table"
dev_langs:
  - "TSQL"
ms.assetid: 4ab1b2ae-b6d9-42b7-9b31-98547dbb7f99
---
# MSagent_profiles (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The **MSagent_profiles** table contains one row for each defined replication agent profile. This table is stored in the **msdb** database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**profile_id**|**int**|The profile ID.|  
|**profile_name**|**sysname**|The unique profile name for agent type.|  
|**agent_type**|**int**|The type of agent:<br /><br /> **1** = Snapshot Agent<br /><br /> **2** = Log Reader Agent<br /><br /> **3** = Distribution Agent<br /><br /> **4** = Merge Agent<br /><br /> **9** = Queue Reader Agent|  
|**type**|**int**|The type of profile:<br /><br /> **0** = System**1** = Custom|  
|**description**|**nvarchar(3000)**|The description of the profile.|  
|**def_profile**|**bit**|Specifies whether this profile is the default for this agent type.|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)  
  
  
