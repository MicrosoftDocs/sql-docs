---
title: "sys.dm_exec_valid_use_hints (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "11/17/2016"
ms.prod: sql
ms.reviewer: ""
ms.technology: system-objects
ms.topic: conceptual
f1_keywords: 
  - "sys.dm_exec_valid_use_hints"
  - "sys.dm_exec_valid_use_hints_TSQL"
  - "dm_exec_valid_use_hints"
  - "dm_exec_valid_use_hints_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_exec_valid_use_hints management view"
ms.assetid: 65d50589-39c2-4046-92b6-0c4587d8c593
author: "pmasl"
ms.author: "pelopes"
manager: craigg
---
# sys.dm_exec_valid_use_hints (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2016-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2016-xxxx-xxxx-xxx-md.md)]

Returns [USE HINT](../../t-sql/queries/hints-transact-sql-query.md#use_hint) supported hint names. It lists one hint name per row.  
  
Use this DMV to see the list of all supported hints under the USE HINT notation.  
  
|Column Name|Data Type|Description|  
|-----------------|---------------|-----------------|  
|name|**sysname**|The name of the hint.|

See [Query Hints](../../t-sql/queries/hints-transact-sql-query.md#use_hint) for descriptions of each hint.

Introduced in [!INCLUDE[ssSQL15_md](../../includes/sssql15-md.md)] SP1.
  
## See Also  
    
 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)   
 [Database Related Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/database-related-dynamic-management-views-transact-sql.md)  

