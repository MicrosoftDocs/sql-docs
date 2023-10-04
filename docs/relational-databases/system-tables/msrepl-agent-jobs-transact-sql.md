---
title: "MSrepl_agent_jobs (Transact-SQL)"
description: MSrepl_agent_jobs contains information about Replication agent jobs to sync between primary and secondary replicas when the Distribution database is part of an availability group.
author: briancarrig
ms.author: brcarrig
ms.reviewer: randolphwest
ms.date: 03/13/2023
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "MSrepl_agent_jobs_TSQL"
  - "MSrepl_agent_jobs"
helpviewer_keywords:
  - "MSrepl_agent_jobs system table"
dev_langs:
  - "TSQL"
---
# MSrepl_agent_jobs (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

The `MSrepl_agent_jobs` table contains information about Replication agent jobs to sync between primary and secondary replicas when the Distribution database is part of an availability group.

| Column name | Data type | Description |
| --- | --- | --- |
| **job_id** | **uniqueidentifier** | The `job_id` remains consistent across replicas. |
| **name** | **sysname** | The name of the agent job. This name is also used to construct the `msdb.dbo.sysproxies` name. |
| **enabled** | **tinyint** | Replication agent jobs can be enabled or disabled. |
| **description** | **nvarchar(512)** | The agent job description. |
| **category_name** | **sysname** | Category of the agent job. Maps to `msdb.dbo.syscategories`. |
| **owner_login_name** | **nvarchar(60)** | This is the login that creates the replication agent job. It is typically the replication Distribution database linked server login. |
| **notify_level_eventlog** | **int** | A value indicating when to place an entry in the Windows application log.<br /><br />0 - Never<br />1 - On success<br />2 - On failure (default)<br />3 - Always |
| **notify_level_email** | **int** | A value that indicates when to send an e-mail upon the completion of this job. `notify_level_email` uses the same values as `notify_level_eventlog`.<br /><br />0 - Never (default)<br />1 - On success<br />2 - On failure<br />3 - Always |
| **notify_email_operator_name** | **sysname** | The e-mail name of the person to send e-mail to when `notify_level_email` is reached. `notify_email_operator_name` is **sysname**, with a default of NULL. |
| **job_step_uid** | **uniqueidentifier** | `job_step_uid` contains the same value as the `step_uid` column in the `msdb.dbo.sysjobsteps` table. |
| **job_login** | **nvarchar(4000)** | Used to look for existing credential in the `sys.credentials` catalog view. |
| **subsystem** | **nvarchar(40)** | Maps to `msdb.dbo.syssubsystems`. |
| **command** | **nvarchar(3200)** | The replication agent command text. The "-distributor" value points to the AG listener URL when this Distribution database is in an availability group. |
| **cmdexec_success_code** | **int** |
| **server** | **sysname** |
| **database_user_name** | **sysname** | Always set with the default value of NULL. |
| **retry_attempts** | **int** |
| **retry_interval** | **int** |
| **os_run_priority** | **int** |
| **job_login** | **nvarchar(257)** |
| **freq_type** | **int** |
| **freq_interval** | **int** |
| **freq_subday_type** | **int** |
| **freq_subday_interval** | **int** |
| **freq_relative_interval** | **int** |
| **freq_recurrence_factor** | **int** |
| **active_start_date** | **int** |
| **active_start_end** | **int** |
| **active_start_time** | **int** |
| **active_end_time** | **int** |
| **agent_id** | **int** |

## See also

- [Replication Tables (Transact-SQL)](../../relational-databases/system-tables/replication-tables-transact-sql.md)
- [Replication Views (Transact-SQL)](../../relational-databases/system-views/replication-views-transact-sql.md)
