---
title: "MSSQLSERVER_10536 | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "10536 (Database Engine error)"
ms.assetid: 9f97b41f-0ef8-4ad2-aec0-906a5d7522ba
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_10536
    
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|10536|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|PG_TOO_MANY_STMTS|  
|Message Text|Cannot create plan guide '%.\*ls' because the batch or module corresponding to the specified `@plan_handle` contains more than 1000 eligible statements. Create a plan guide for each statement in the batch or module by specifying a `statement_start_offset` value for each statement.|  
  
## Explanation  
 The batch or module corresponding to the specified `@plan_handle` contains more than 1000 eligible statements.  
  
## User Action  
 Create a plan guide for each statement in the batch or module by specifying a `statement_start_offset` value for each statement.  
  
## See Also  
 [sp_create_plan_guide &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-create-plan-guide-transact-sql)   
 [Plan Guides](../performance/plan-guides.md)   
 [sp_create_plan_guide_from_handle &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-create-plan-guide-from-handle-transact-sql)  
  
  
