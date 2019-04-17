---
title: "snapshots.fn_trace_getdata (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "snapshots.fn_trace_getdata"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "snapshots.fn_trace_getdata function"
ms.assetid: ac28ef48-f4f4-4bf2-ba22-d44e1be88172
author: "rothja"
ms.author: "jroth"
manager: craigg
---
# snapshots.fn_trace_getdata (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  This function returns all the events captured for the specified trace.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
snapshots.fn_trace_gettable ( trace_info_id, start_time, end_time )  
```  
  
## Arguments  
 *trace_info_id*  
 The unique identifier for the primary key in the snapshots.trace_info table in the management data warehouse database. *trace_info_id* is **int**.  
  
 *start_time*  
 The time that the trace started. *start_time* is **datetime**.  
  
 *end_time*  
 The time that the trace ended. *end_time* is **datetime**.  
  
## Table Returned  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|\<All trace columns>|\<Varies>|The trace data from the snapshots.trace_data table in the management data warehouse database.<br /><br /> A list of the columns for the specified trace can be obtained by using the following query:<br /><br /> `SELECT * FROM sys.trace_columns`<br /><br /> **Note:** The columns that are returned by the snapshots.fn_trace_gettable function correspond to the values in the name column in the sys.trace_columns system view. The only difference is that the GroupID column is not returned by the function.|  
  
## Permissions  
 Requires SELECT permission for mdw_reader.  
  
## See Also  
 [Data Collection](../../relational-databases/data-collection/data-collection.md)  
  
  
