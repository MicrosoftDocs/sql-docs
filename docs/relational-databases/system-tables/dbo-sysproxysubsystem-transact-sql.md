---
title: "dbo.sysproxysubsystem (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "dbo.sysproxysubsystem_TSQL"
  - "dbo.sysproxysubsystem"
  - "sysproxysubsystem_TSQL"
  - "sysproxysubsystem"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sysproxysubsystem system table"
ms.assetid: 6d7713f5-1253-4a19-b1fb-635c377c95c1
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# dbo.sysproxysubsystem (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Records which [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent subsystem is used by each proxy account. This table is stored in the **msdb** database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**subsystem_id**|**int**|ID of the subsystem. This value corresponds to the **subsystem_id** column in the **syssubsystems** table.|  
|**proxy_id**|**int**|ID of the proxy account. This value corresponds to the **proxy_id** column in the **sysproxies** table.|  
  
## Remarks  
 Only members of the **sysadmin** fixed server role can access this table.  
  
## See Also  
 [dbo.syssubsystems &#40;Transact-SQL&#41;](../../relational-databases/system-tables/dbo-syssubsystems-transact-sql.md)   
 [dbo.sysproxies &#40;Transact-SQL&#41;](../../relational-databases/system-tables/dbo-sysproxies-transact-sql.md)  
  
  
