---
title: "MSSQLSERVER_10535 | Microsoft Docs"
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
  - "10535 (Database Engine error)"
ms.assetid: 478fd978-11d9-4155-8329-f599fdbec14b
caps.latest.revision: 9
author: "craigg-msft"
ms.author: "rickbyh"
manager: "jhubbard"
---
# MSSQLSERVER_10535
    
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|10535|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|PG_NO_PLAN|  
|Message Text|Cannot create plan guide '%.*ls' because a plan was not found in the plan cache that corresponds to the specified plan handle. Specify a cached plan handle. For a list of cached plan handles, query the sys.dm_exec_query_stats dynamic management view.|  
  
## Explanation  
 A plan was not found in the plan cache that corresponds to the specified plan handle.  
  
## User Action  
 Specify a cached plan handle. For a list of cached plan handles, query the sys.dm_exec_query_stats dynamic management view.  
  
## See Also  
 [sp_create_plan_guide_from_handle &#40;Transact-SQL&#41;](../Topic/sp_create_plan_guide_from_handle%20\(Transact-SQL\).md)   
 [sys.dm_exec_query_stats &#40;Transact-SQL&#41;](../Topic/sys.dm_exec_query_stats%20\(Transact-SQL\).md)  
  
  