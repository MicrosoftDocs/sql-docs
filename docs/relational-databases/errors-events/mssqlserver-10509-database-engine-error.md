---
description: "MSSQLSERVER_10509"
title: "MSSQLSERVER_10509 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "10509 (Database Engine error)"
ms.assetid: e9dd5357-ee3d-420a-9a89-d12ab5404e73
author: MashaMSFT
ms.author: mathoma
---
# MSSQLSERVER_10509
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|10509|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|PG_INVALID_STMT|  
|Message Text|Cannot create plan guide '%.\*ls' because the statement specified by **\@stmt** or **\@statement_start_offset** either contains a syntax error or is ineligible for use in a plan guide. Provide a single valid [!INCLUDE[tsql](../../includes/tsql-md.md)] statement or a valid starting position of the statement within the batch. To obtain a valid starting position, query the statement_start_offset column in the sys.dm_exec_query_stats dynamic management function.|  
  
## Explanation  
The statement specified by **\@stmt** or **\@statement_start_offset** either contains a syntax error or is ineligible for use in a plan guide.  
  
## User Action  
Provide a single valid [!INCLUDE[tsql](../../includes/tsql-md.md)] statement or a valid starting position of the statement within the batch. To obtain a valid starting position, query the statement_start_offset column in the sys.dm_exec_query_stats dynamic management function.  
  
## See Also  
[sp_create_plan_guide &#40;Transact-SQL&#41;](~/relational-databases/system-stored-procedures/sp-create-plan-guide-transact-sql.md)  
[Plan Guides](~/relational-databases/performance/plan-guides.md)  
[sys.dm_exec_query_stats &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/sys-dm-exec-query-stats-transact-sql.md)  
[sp_create_plan_guide_from_handle &#40;Transact-SQL&#41;](~/relational-databases/system-stored-procedures/sp-create-plan-guide-from-handle-transact-sql.md)  
  
