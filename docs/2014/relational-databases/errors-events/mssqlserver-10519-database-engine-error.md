---
title: "MSSQLSERVER_10519 | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "10519 (Database Engine error)"
ms.assetid: 3be393a1-b186-41ae-afb9-a3d07ff354bb
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_10519
    
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|10519|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|PG_INCOMPATIBLE_STMT_AND_HINTS|  
|Message Text|Cannot create plan guide '%.\*ls' because the hints specified in `@hints` cannot be applied to the statement specified by either `@stmt` or `@statement_start_offset`. Verify that the hints can be applied to the statement.|  
  
## Explanation  
 The hints specified in `@hints` cannot be applied to the statement specified by either `@stmt` or `@statement_start_offset`.  
  
## User Action  
 Specify hints that can be applied to the statement.  
  
## See Also  
 [sp_create_plan_guide &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-create-plan-guide-transact-sql)   
 [Plan Guides](../performance/plan-guides.md)   
 [sp_create_plan_guide_from_handle &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-create-plan-guide-from-handle-transact-sql)  
  
  
