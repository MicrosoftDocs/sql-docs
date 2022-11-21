---
title: "dbo.sysproxysubsystem (Transact-SQL)"
description: dbo.sysproxysubsystem (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "dbo.sysproxysubsystem_TSQL"
  - "dbo.sysproxysubsystem"
  - "sysproxysubsystem_TSQL"
  - "sysproxysubsystem"
helpviewer_keywords:
  - "sysproxysubsystem system table"
dev_langs:
  - "TSQL"
ms.assetid: 6d7713f5-1253-4a19-b1fb-635c377c95c1
---
# dbo.sysproxysubsystem (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

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
  
  
