---
title: "fn_syscollector_get_execution_stats (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "fn_syscollector_get_execution_stats"
  - "fn_syscollector_get_execution_stats_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "fn_syscollector_get_execution_stats function"
ms.assetid: 793ad72c-a992-4a8d-8584-bcb6b3b476f1
author: "rothja"
ms.author: "jroth"
manager: craigg
---
# fn_syscollector_get_execution_stats (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  Returns detailed statistics about the collection set or package, including the number of error rows that are logged by a package data flow task. A data flow task is an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] component that processes data. This data is in relational format, so it has an input and an output dataset consisting of rows.  
  
 The statistics are calculated from entries in the syscollector_execution_stats view.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
fn_syscollector_get_execution_stats ( log_id )  
```  
  
## Arguments  
 *log_id*  
 The local unique identifier for the execution log. *log_id* is **int**.  
  
## Table Returned  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|avg_row_count_in|**int**|Average number of rows that entered the data flow tasks of the package.<br /><br /> Note: A data flow task is an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] component that processes data. This data is in relational format, so it has an input dataset that consists of rows. This is the number of rows that entered the task. After the data is transformed, it is output as a result set consisting of rows. The data flow task transforms the data and outputs a result set consisting of rows. This output is the number of rows that exited the task.|  
|min_row_count_in|**int**|Minimum number of rows that entered the data flow tasks of the package.|  
|max_row_count_in|**int**|Maximum number of rows that entered the data flow tasks of the package.|  
|avg_row_count_out|**int**|Average number of rows that exited the data flow tasks of the package.|  
|min_row_count_out|**int**|Minimum number of rows that exited the data flow tasks of the package.|  
|max_row_count_out|**int**|Maximum number of rows that exited the data flow tasks of the package.|  
|avg_duration|**int**|Average time, in milliseconds, spent in the data flow component of the package.|  
|min_duration|**int**|Minimum time, in milliseconds, spent in the data flow component of the package.|  
|max_duration|**int**|Maximum time, in milliseconds, spent in the data flow component of the package.|  
  
## Permissions  
 Requires SELECT for **dc_operator**.  
  
## See Also  
 [syscollector_execution_stats &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/syscollector-execution-stats-transact-sql.md)   
 [Data Collection](../../relational-databases/data-collection/data-collection.md)  
  
  
