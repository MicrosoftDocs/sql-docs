---
title: "MSSQLSERVER_10507 | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "10507 (Database Engine error)"
ms.assetid: cd83fa81-ac37-4eda-a3c3-17610b051de2
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_10507
    
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|10507|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|PG_STMT_DOES_NOT_MATCH|  
|Message Text|Cannot create plan guide '%.\*ls' because the statement specified by `@stmt` and `@module_or_batch`, or by `@plan_handle` and `@statement_start_offset`, does not match any statement in the specified module or batch. Modify the values to match a statement in the module or batch.|  
  
## Explanation  
 A statement in the specified module or batch could not be matched to the specified statement or statement offset value.  
  
## User Action  
 Modify the specified parameter values to match a statement in the module or batch.  
  
## See Also  
 [Plan Guides](../performance/plan-guides.md)   
 [sp_create_plan_guide &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-create-plan-guide-transact-sql)   
 [sp_create_plan_guide_from_handle &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-create-plan-guide-from-handle-transact-sql)  
  
  
