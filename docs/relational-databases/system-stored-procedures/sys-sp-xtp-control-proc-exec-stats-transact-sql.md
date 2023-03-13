---
title: "sys.sp_xtp_control_proc_exec_stats (Transact-SQL)"
description: "sys.sp_xtp_control_proc_exec_stats (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "03/15/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.sp_xtp_control_proc_exec_stats"
  - "sys.sp_xtp_control_proc_exec_stats_TSQL"
helpviewer_keywords:
  - "sys.sp_xtp_control_proc_exec_stats"
dev_langs:
  - "TSQL"
---
# sys.sp_xtp_control_proc_exec_stats (Transact-SQL)
[!INCLUDE[sqlserver](../../includes/applies-to-version/sqlserver.md)]

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
 [In-Memory OLTP &#40;In-Memory Optimization&#41;](../in-memory-oltp/overview-and-usage-scenarios.md)  
  
