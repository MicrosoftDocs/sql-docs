---
title: "dbo.sysjobservers (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sysjobservers"
  - "sysjobservers_TSQL"
  - "dbo.sysjobservers"
  - "dbo.sysjobservers_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sysjobservers system table"
ms.assetid: 9abcc20f-a421-4591-affb-62674d04575e
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# dbo.sysjobservers (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Stores the association or relationship of a particular job with one or more target servers.This table is stored in the msdb database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|job_id|**uniqueidentifier**|Job identification number.|  
|server_id|**int**|Server identification number.|  
|last_run_outcome|**tinyint**|Outcome for the job's last run:<br /><br /> **0** = Fail<br /><br /> **1** = Succeed<br /><br /> **3** = Cancel|  
|last_outcome_ message|**nvarchar(1024)**|Associated message, if any, with the last_run_outcome column.|  
|last_run_date|**int**|Date the job was last run.|  
|last_run_time|**int**|Time the job was last run.|  
|last_run_duration|**int**|Duration that the job was run, in hours, minutes, and seconds. Computed by using the formula: (*hours*\*10000) + (*minutes*\*100) + *seconds*.|  
  
## See Also  
 [SQL Server Agent Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/sql-server-agent-tables-transact-sql.md)  
  
  
