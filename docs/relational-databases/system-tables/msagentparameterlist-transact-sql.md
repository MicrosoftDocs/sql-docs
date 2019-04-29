---
title: "MSagentparameterlist (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "MSagentparameterlist_TSQL"
  - "MSagentparameterlist"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "Msagentparameterlist system table"
ms.assetid: 4ea571a0-078d-4e13-95ee-f3d4bbd4dfb2
author: stevestein
ms.author: sstein
manager: craigg
---
# MSagentparameterlist (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  The **MSagentparameterlist** table contains replication agent parameter information and is used to specify the parameters that can be set for a given agent type. This table is stored in the **msdb** database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**agent_type**|**tinyint**|The type of agent:<br /><br /> **1** = Snapshot Agent.<br /><br /> **2** = Log Reader Agent.<br /><br /> **3** = Distribution Agent.<br /><br /> **4** = Merge Agent.<br /><br /> **9** = Queue Reader Agent.|  
|**parameter_name**|**sysname**|The name of a valid agent parameter.|  
|**default_value**|**nvarchar(4000)**|The default value for the agent parameter, where NULL indicates that no such value exists.|  
|**min_value**|**int**|Sets a lower bound for the agent parameter, where NULL indicates no lower bound.|  
|**max_value**|**int**|Sets an upper bound for the agent parameter, where NULL indicates no upper bound.|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)  
  
  
