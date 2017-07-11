---
title: "sp_enum_sqlagent_subsystems (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "sp_enum_sqlagent_subsystems"
  - "sp_enum_sqlagent_subsystems_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_enum_sqlagent_subsystems"
ms.assetid: 019a3c9d-bac3-495b-a70a-2c19f1d2e20e
caps.latest.revision: 33
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# sp_enum_sqlagent_subsystems (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Lists the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent subsystems.  
  
||  
|-|  
|**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ([!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [current version](http://go.microsoft.com/fwlink/p/?LinkId=299658)).|  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_enum_sqlagent_subsystems  
```  
  
## Arguments  
 None  
  
## Return Code Values  
 **0** (success) or **1** (Failure)  
  
## Result Sets  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**subsystem**|**nvarchar(40)**|Name of the subsystem.|  
|**description**|**nvarchar(512)**|Description of the subsystem.|  
|**subsystem_dll**|**nvarchar(510)**|DLL module that contains the subsystem.|  
|**agent_exe**|**nvarchar(510)**|Executable module that is used by the subsystem.|  
|**start_entry_point**|**nvarchar(30)**|Procedure that SQL Server Agent calls during job step execution.|  
|**event_entry_point**|**nvarchar(30)**|Procedure that SQL Server Agent calls during job step execution.|  
|**stop_entry_point**|**nvarchar(30)**|Procedure that SQL Server Agent calls during job step execution.|  
|**max_worker_threads**|**int**|Maximum number of threads [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent will start for this subsystem.|  
|**subsystem_id**|**int**|Identifier for the subsystem.|  
  
## Remarks  
 This procedure lists the subsystems available in the instance.  
  
## Permissions  
 By default, members of the **sysadmin** fixed server role can execute this stored procedure. Other users must be granted the **SQLAgentOperatorRole** fixed database role in the **msdb** database.  
  
 For details about **SQLAgentOperatorRole**, see [SQL Server Agent Fixed Database Roles](http://msdn.microsoft.com/library/719ce56b-d6b2-414a-88a8-f43b725ebc79).  
  
## See Also  
 [Implement SQL Server Agent Security](http://msdn.microsoft.com/library/d770d35c-c8de-4e00-9a85-7d03f45a0f0d)   
 [sp_add_jobstep &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-add-jobstep-transact-sql.md)  
  
  
