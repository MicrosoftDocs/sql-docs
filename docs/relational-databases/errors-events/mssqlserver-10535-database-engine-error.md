---
title: "MSSQLSERVER_10535"
description: "MSSQLSERVER_10535"
author: MashaMSFT
ms.author: mathoma
ms.date: "04/04/2017"
ms.service: sql
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords:
  - "10535 (Database Engine error)"
---
# MSSQLSERVER_10535
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
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
[sp_create_plan_guide_from_handle &#40;Transact-SQL&#41;](~/relational-databases/system-stored-procedures/sp-create-plan-guide-from-handle-transact-sql.md)  
[sys.dm_exec_query_stats &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/sys-dm-exec-query-stats-transact-sql.md)  
  
