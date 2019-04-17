---
title: "MSSQLSERVER_10538 | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "10538 (Database Engine error)"
ms.assetid: 284d19b4-4979-4cbe-a9be-ac1104433c69
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_10538
    
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|10538|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|PG_INVALID_PLANGUIDE_HANDLE|  
|Message Text|Cannot find the plan guide either because the specified plan guide ID is NULL or invalid, or you do not have permission on the object referenced by the plan guide. Verify that the plan guide ID is valid, the current session is set to the correct database context, and you have either ALTER DATABASE permission or ALTER permission on the object referenced by the plan guide.|  
  
## Explanation  
 The ID of the specified plan guide is NULL or invalid, or you do not have permission on the object referenced by the plan guide.  
  
## User Action  
 Verify that the plan guide ID is valid, the current session is set to the correct database context, and you have ALTER DATABASE permission or ALTER permission on the object referenced by the plan guide.  
  
## See Also  
 [sp_create_plan_guide &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-create-plan-guide-transact-sql)   
 [Plan Guides](../performance/plan-guides.md)   
 [sp_create_plan_guide_from_handle &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-create-plan-guide-from-handle-transact-sql)  
  
  
