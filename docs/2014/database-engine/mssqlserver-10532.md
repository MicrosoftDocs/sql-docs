---
title: "MSSQLSERVER_10532 | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "10532 (Database Engine error)"
ms.assetid: 01da29ee-bf67-433f-8148-587a7e8d1d76
caps.latest.revision: 10
author: "craigg-msft"
ms.author: "rickbyh"
manager: "jhubbard"
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
 [Plan Guides](../../2014/database-engine/plan-guides.md)   
 [sp_create_plan_guide &#40;Transact-SQL&#41;](../Topic/sp_create_plan_guide%20\(Transact-SQL\).md)   
 [sp_create_plan_guide_from_handle &#40;Transact-SQL&#41;](../Topic/sp_create_plan_guide_from_handle%20\(Transact-SQL\).md)  
  
  