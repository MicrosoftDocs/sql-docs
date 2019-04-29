---
title: "MSSQLSERVER_10502 | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "10502 (Database Engine error)"
ms.assetid: 26d9b64d-4299-4b55-92d0-0a32a3688c0a
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_10502
    
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|10502|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|PG_DUP_FOUND|  
|Message Text|Cannot create plan guide '%.*ls' because the statement specified by @stmt and @module_or_batch, or by @plan_handle and @statement_start_offset, matches the existing plan guide '%.\*ls' in the database. Drop the existing plan guide before creating the new plan guide.|  
  
## Explanation  
 A plan guide exists for the specified statement.  
  
## User Action  
 Drop the existing plan guide before creating the new plan guide.  
  
## See Also  
 [Plan Guides](../performance/plan-guides.md)   
 [sp_create_plan_guide &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-create-plan-guide-transact-sql)   
 [sp_create_plan_guide_from_handle &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-create-plan-guide-from-handle-transact-sql)  
  
  
