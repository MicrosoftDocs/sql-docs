---
title: "MSSQLSERVER_10532 | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "10532 (Database Engine error)"
ms.assetid: 01da29ee-bf67-433f-8148-587a7e8d1d76
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_10532
    
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|10532|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|PG_NO_ELIGIBLE_STMT|  
|Message Text|Cannot create plan guide '%.\*ls' because the batch or module specified by `@plan_handle` does not contain a statement that is eligible for a plan guide. Specify a different value for `@plan_handle`.|  
  
## Explanation  
 The batch or module specified by `@plan_handle` does not contain a statement that is eligible for a plan guide.  
  
## User Action  
 Specify a different value for `@plan_handle`.  
  
## See Also  
 [Plan Guides](../performance/plan-guides.md)   
 [sp_create_plan_guide &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-create-plan-guide-transact-sql)   
 [sp_create_plan_guide_from_handle &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-create-plan-guide-from-handle-transact-sql)  
  
  
