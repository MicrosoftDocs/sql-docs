---
title: "MSSQLSERVER_10502_deleted | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
helpviewer_keywords: 
  - "10502 (Database Engine error)"
ms.assetid: 26d9b64d-4299-4b55-92d0-0a32a3688c0a
caps.latest.revision: 9
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# MSSQLSERVER_10502_deleted
  
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
[Plan Guides](../Topic/Plan%20Guides.md)  
[sp_create_plan_guide &#40;Transact-SQL&#41;](../Topic/sp_create_plan_guide%20(Transact-SQL).md)  
[sp_create_plan_guide_from_handle &#40;Transact-SQL&#41;](../Topic/sp_create_plan_guide_from_handle%20(Transact-SQL).md)  
  
