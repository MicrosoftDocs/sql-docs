---
title: "sys.sp_xtp_control_proc_exec_stats (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.sp_xtp_control_proc_exec_stats"
  - "sys.sp_xtp_control_proc_exec_stats_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.sp_xtp_control_proc_exec_stats"
ms.assetid: f5119808-76a1-4522-8529-9e02ee39adcb
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sys.sp_xtp_control_proc_exec_stats (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2014-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2014-xxxx-xxxx-xxx-md.md)]

  Enables statistics collection for natively compiled stored procedures for the instance.  
  
 To enable statistics collection at the query level for natively compiled stored procedures, see [sys.sp_xtp_control_query_exec_stats &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sys-sp-xtp-control-query-exec-stats-transact-sql.md).  
  
## Syntax  
  
```  
sp_xtp_control_proc_exec_stats [ [ @new_collection_value = ] collection_value ], [ @old_collection_value]  
```  
  
## Arguments  
 @new_collection_value = *value*  
 Determines whether procedure-level statistics collection is on (1) or off (0).  
  
 @new_collection_value is set to zero when [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or the database starts.  
  
 @old_collection_value = *value*  
 Returns the current status.  
  
## Return Code  
 0 for success. Nonzero for failure.  
  
## Permissions  
 Requires membership in the fixed sysadmin role.  
  
## Code Samples  
 To set @new_collection_value and query for the value of @new_collection_value:  
  
```  
exec [sys].[sp_xtp_control_proc_exec_stats] @new_collection_value = 1  
declare @c bit  
exec sp_xtp_control_proc_exec_stats @old_collection_value=@c output  
select @c as 'collection status'  
```  
  
## See Also  
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)   
 [In-Memory OLTP &#40;In-Memory Optimization&#41;](../../relational-databases/in-memory-oltp/in-memory-oltp-in-memory-optimization.md)  
  
  
