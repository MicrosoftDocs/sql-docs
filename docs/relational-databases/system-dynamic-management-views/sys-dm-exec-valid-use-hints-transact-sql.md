---
title: "sys.dm_exec_valid_use_hints (Transact-SQL)"
description: sys.dm_exec_valid_use_hints (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "11/17/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: conceptual
f1_keywords:
  - "sys.dm_exec_valid_use_hints"
  - "sys.dm_exec_valid_use_hints_TSQL"
  - "dm_exec_valid_use_hints"
  - "dm_exec_valid_use_hints_TSQL"
helpviewer_keywords:
  - "sys.dm_exec_valid_use_hints management view"
dev_langs:
  - "TSQL"
ms.assetid: 65d50589-39c2-4046-92b6-0c4587d8c593
---
# sys.dm_exec_valid_use_hints (Transact-SQL)
[!INCLUDE [sqlserver2016](../../includes/applies-to-version/sqlserver2016.md)]

Returns [USE HINT](../../t-sql/queries/hints-transact-sql-query.md#use_hint) supported hint names. It lists one hint name per row.  
  
Use this DMV to see the list of all supported hints under the USE HINT notation.  
  
|Column Name|Data Type|Description|  
|-----------------|---------------|-----------------|  
|name|**sysname**|The name of the hint.|

See [Query Hints](../../t-sql/queries/hints-transact-sql-query.md#use_hint) for descriptions of each hint.

Introduced in [!INCLUDE[ssSQL15_md](../../includes/sssql16-md.md)] SP1.
  
## See Also  
    
 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)   
 [Database Related Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/database-related-dynamic-management-views-transact-sql.md)  

