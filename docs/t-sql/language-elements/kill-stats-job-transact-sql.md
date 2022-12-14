---
title: "KILL STATS JOB (Transact-SQL)"
description: "KILL STATS JOB (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: ""
ms.date: "07/27/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom: ""
f1_keywords:
  - "KILL STATS JOB"
  - "KILL_STATS_JOB_TSQL"
helpviewer_keywords:
  - "ending statistics update jobs [SQL Server]"
  - "stopping statistics update jobs"
  - "terminating statistics update jobs"
  - "asynchronous statistics updates [SQL Server]"
  - "KILL STATS JOB statement"
  - "statistics update jobs [SQL Server]"
dev_langs:
  - "TSQL"
---
# KILL STATS JOB (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

  Terminates an asynchronous statistics update job in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
KILL STATS JOB job_id   
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *job_id*  
 Is the job_id field returned by the sys.dm_exec_background_job_queue dynamic management view for the job.  
  
## Remarks  
 The job_id is unrelated to session_id or unit of work used in other forms of the KILL statement.  
  
## Permissions  
 User must have VIEW SERVER STATE permission to access information from the sys.dm_exec_background_job_queue dynamic management view.  
  
 KILL STATS JOB permissions default to the members of the sysadmin and processadmin fixed database roles and are not transferable.  
  
## Examples  
 The following example shows how to terminate the statistics update associated with a job where the *job_id* = `53`.  
  
```sql  
KILL STATS JOB 53;  
GO  
```  
  
## See Also  
 [KILL &#40;Transact-SQL&#41;](../../t-sql/language-elements/kill-transact-sql.md)   
 [KILL QUERY NOTIFICATION SUBSCRIPTION &#40;Transact-SQL&#41;](../../t-sql/language-elements/kill-query-notification-subscription-transact-sql.md)   
 [sys.dm_exec_background_job_queue &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-exec-background-job-queue-transact-sql.md)   
 [Statistics](../../relational-databases/statistics/statistics.md)  
  
  
