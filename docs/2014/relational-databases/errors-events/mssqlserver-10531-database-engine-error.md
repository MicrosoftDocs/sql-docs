---
title: "MSSQLSERVER_10531 | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "10531 (Database Engine error)"
ms.assetid: bb40e994-231c-44ce-933f-8d767fb2f450
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_10531
    
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|10531|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|PG_NO_ELIGIBLE_STMT|  
|Message Text|Cannot create plan guide '%.*ls' from cache because the user does not have adequate permissions. Grant the VIEW SERVER STATE permission to the user creating the plan guide.|  
  
## Explanation  
 The user does not have adequate permissions to create the specified plan guide from the plan cache.  
  
## User Action  
 Grant VIEW SERVER STATE permission to the user creating the plan guide.  
  
## See Also  
 [Plan Guides](../performance/plan-guides.md)   
 [sp_create_plan_guide &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-create-plan-guide-transact-sql)   
 [sp_create_plan_guide_from_handle &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-create-plan-guide-from-handle-transact-sql)  
  
  
