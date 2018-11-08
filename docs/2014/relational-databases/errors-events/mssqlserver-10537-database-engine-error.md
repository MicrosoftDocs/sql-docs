---
title: "MSSQLSERVER_10537 | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "10537 (Database Engine error)"
ms.assetid: 728469a4-6523-4a37-925f-a950d75420fa
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_10537
    
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|10537|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|PG_DUP_ENABLED|  
|Message Text|Cannot enable plan guide '%.*ls' because the enabled plan guide '%.\*ls' contains the same scope and starting offset value as the statement. Disable the existing plan guide before enabling the specified plan guide.|  
  
## Explanation  
 An existing plan guide contains the same scope and starting offset value as the statement in the specified plan guide.  
  
## User Action  
 Disable the existing plan guide before enabling the specified plan guide.  
  
## See Also  
 [sp_create_plan_guide &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-create-plan-guide-transact-sql)   
 [Plan Guides](../performance/plan-guides.md)   
 [sp_create_plan_guide_from_handle &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-create-plan-guide-from-handle-transact-sql)  
  
  
