---
title: dbo.sysjobs (Transact-SQL)
description: dbo.sysjobs stores the information for each scheduled job that SQL Server Agent runs.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 08/11/2026
ms.service: sql
ms.subservice: system-objects
ms.topic: reference
f1_keywords:
  - "sysjobs"
  - "sysjobs_TSQL"
  - "dbo.sysjobs"
  - "dbo.sysjobs_TSQL"
helpviewer_keywords:
  - "sysjobs system table"
dev_langs:
  - TSQL
---
# dbo.sysjobs (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Stores the information for each scheduled job that [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent runs. This table is in the `msdb` database.

| Column name | Data type | Description |
| --- | --- | --- |
| `job_id` | **uniqueidentifier** | Unique ID of the job. |
| `originating_server_id` | **int** | ID of the server from which the job came. |
| `name` | **sysname** | Name of the job. |
| `enabled` | **tinyint** | Indicates whether the job is enabled to run. |
| `description` | **nvarchar(512)** | Description for the job. |
| `start_step_id` | **int** | ID of the step in the job where execution begins. |
| `category_id` | **int** | ID of the job category. |
| `owner_sid` | **varbinary(85)** | Security identifier number (SID) of the job owner. Grant `SELECT` access only to users who need it. |
| `notify_level_eventlog` | **int** | Bitmask indicating under what circumstances to log a notification event to the Microsoft Windows application log:<br /><br />`0` = Never<br />`1` = When the job succeeds<br />`2` = When the job fails<br />`3` = Whenever the job completes (regardless of the job outcome) |
| `notify_level_email` | **int** | Bitmask indicating under what circumstances to send a notification email when a job completes:<br /><br />`0` = Never<br />`1` = When the job succeeds<br />`2` = When the job fails<br />`3` = Whenever the job completes (regardless of the job outcome) |
| `notify_level_netsend` | **int** | Bitmask indicating under what circumstances to send a network message when a job completes:<br /><br />`0` = Never<br />`1` = When the job succeeds<br />`2` = When the job fails<br />`3` = Whenever the job completes (regardless of the job outcome) |
| `notify_level_page` | **int** | Bitmask indicating under what circumstances to send a page when a job completes:<br /><br />`0` = Never<br />`1` = When the job succeeds<br />`2` = When the job fails<br />`3` = Whenever the job completes (regardless of the job outcome) |
| `notify_email_operator_id` | **int** | Email name of the operator to notify. |
| `notify_netsend_operator_id` | **int** | ID of the computer or user for sending network messages. |
| `notify_page_operator_id` | **int** | ID of the computer or user for sending a page. |
| `delete_level` | **int** | Bitmask indicating under what circumstances to delete the job when a job completes:<br /><br />`0` = Never<br />`1` = When the job succeeds<br />`2` = When the job fails<br />`3` = Whenever the job completes (regardless of the job outcome) |
| `date_created` | **datetime** | Date the job was created. |
| `date_modified` | **datetime** | Date the job was last modified. |
| `version_number` | **int** | Version of the job. |

## Security

The `owner_sid` column shows which login runs Transact-SQL job steps. Grant `SELECT` access only to users who need it.

## Related content

- [SQL Server Agent overview](/ssms/agent/sql-server-agent)
- [Select an account for the SQL Server Agent service](/ssms/agent/select-an-account-for-the-sql-server-agent-service)
- [SQL Server Agent tables (Transact-SQL)](sql-server-agent-tables-transact-sql.md)
