---
title: "dbo.syssubsystems (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "dbo.syssubsystems"
  - "syssubsystems"
  - "syssubsystems_TSQL"
  - "dbo.syssubsystems_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "syssubsystems system table"
ms.assetid: 114b3d55-1ad6-4777-b868-8ef0c86ba596
caps.latest.revision: 14
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# dbo.syssubsystems (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Contains information about all available [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent proxy subsystems. The **syssubsystems** table is stored in the **msdb** database.  
  
||  
|-|  
|**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ([!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [current version](http://go.microsoft.com/fwlink/p/?LinkId=299658)).|  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**subsystem_id**|**int**|ID of the subsystem.|  
|**subsystem**|**nvarchar(40)**|Name of the subsystem.|  
|**description_id**|**int**|Message ID of the row in the **sys.messages** catalog view that contains the subsystem description.|  
|**subsystem_dll**|**nvarchar(255)**|Location of the subsystem DLL.|  
|**agent_exe**|**nvarchar(255)**|Full path to the executable that uses the subsystem.|  
|**start_entry_point**|**nvarchar(30)**|Function that is called when the subsystem is initialized.|  
|**event_entry_point**|**nvarchar(30)**|Function that is called when a subsystem step is run.|  
|**stop_entry_point**|**nvarchar(30)**|Function that is called when a subsystem finishes running.|  
|**max_worker_threads**|**int**|Maximum number of concurrent steps for a given subsystem.|  
  
## Remarks  
 Only members of the **sysadmin** fixed server role can access this table.  
  
## See Also  
 [dbo.sysproxysubsystem &#40;Transact-SQL&#41;](../../relational-databases/system-tables/dbo-sysproxysubsystem-transact-sql.md)   
 [dbo.sysproxies &#40;Transact-SQL&#41;](../../relational-databases/system-tables/dbo-sysproxies-transact-sql.md)   
 [sys.messages &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/messages-for-errors-catalog-views-sys-messages.md)  
  
  