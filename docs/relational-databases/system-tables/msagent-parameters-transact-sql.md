---
title: "MSagent_parameters (Transact-SQL)"
description: MSagent_parameters (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "03/04/2017"
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "MSagent_parameters_TSQL"
  - "MSagent_parameters"
helpviewer_keywords:
  - "MSagent_parameters system table"
dev_langs:
  - "TSQL"
ms.assetid: be30abc9-c00d-446f-b1b4-1269772f37e6
---
# MSagent_parameters (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The **MSagent_parameters** table contains parameters associated with an agent profile. The parameter names are the same as those supported by the agent. This table is stored in the **msdb** database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**profile_id**|**int**|The profile ID from the **MSagent_profiles** table.|  
|**parameter_name**|**sysname**|The name of the parameter.|  
|**value**|**nvarchar(255)**|The value of the parameter.|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)  
  
  
