---
title: "sp_help_alert (Transact-SQL)"
description: Reports information about the alerts defined for the server.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/14/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_help_alert"
  - "sp_help_alert_TSQL"
helpviewer_keywords:
  - "sp_help_alert"
dev_langs:
  - "TSQL"
---
# sp_help_alert (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Reports information about the alerts defined for the server.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_help_alert
    [ [ @alert_name = ] N'alert_name' ]
    [ , [ @order_by = ] N'order_by' ]
    [ , [ @alert_id = ] alert_id ]
    [ , [ @category_name = ] N'category_name' ]
    [ , [ @legacy_format = ] legacy_format ]
[ ; ]
```

## Arguments

#### [ @alert_name = ] N'*alert_name*'

The alert name. *@alert_name* is **sysname**, with a default of `NULL`. If *@alert_name* isn't specified, information about all alerts is returned.

#### [ @order_by = ] N'*order_by*'

The sorting order to use for producing the results. *@order_by* is **sysname**, with a default of the *@alert_name*.

#### [ @alert_id = ] *alert_id*

The identification number of the alert to report information about. *@alert_id* is **int**, with a default of `NULL`.

#### [ @category_name = ] N'*category_name*'

The category for the alert. *@category_name* is **sysname**, with a default of `NULL`.

#### [ @legacy_format = ] *legacy_format*

Specifies whether to produce a legacy result set. *@legacy_format* is **bit**, with a default of `0`. When *@legacy_format* is `1`, `sp_help_alert` returns the result set returned by `sp_help_alert` in [!INCLUDE [ssversion2000-md](../../includes/ssversion2000-md.md)].

## Return code values

`0` (success) or `1` (failure).

## Result set

This table only shows the output when *@legacy_format* is `0`, for [!INCLUDE [ssversion2005-md](../../includes/ssversion2005-md.md)] and later versions.

| Column name | Data type | Description |
| --- | --- | --- |
| `id` | **int** | System-assigned unique integer identifier. |
| `name` | **sysname** | Alert name (for example, `Demo: Full msdb log`). |
| `event_source` | **nvarchar(100)** | Source of the event. |
| `event_category_id` | **int** | [!INCLUDE [ssInternalOnly](../../includes/ssinternalonly-md.md)] |
| `event_id` | **int** | [!INCLUDE [ssInternalOnly](../../includes/ssinternalonly-md.md)] |
| `message_id` | **int** | Message error number that defines the alert. (Usually corresponds to an error number in the `sysmessages` table). If severity is used to define the alert, `message_id` is `0` or `NULL`. |
| `severity` | **int** | Severity level (from `9` through `25`, `110`, `120`, `130`, or `140`) that defines the alert. |
| `enabled` | **tinyint** | Status of whether the alert is currently enabled (`1`) or not (`0`). A nonenabled alert isn't sent. |
| `delay_between_responses` | **int** | Wait period, in seconds, between responses to the alert. |
| `last_occurrence_date` | **int** | Data the alert last occurred. |
| `last_occurrence_time` | **int** | Time the alert last occurred. |
| `last_response_date` | **int** | Date the alert was last responded to by the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Agent service. |
| `last_response_time` | **int** | Time the alert was last responded to by the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Agent service. |
| `notification_message` | **nvarchar(512)** | Optional additional message sent to the operator as part of the e-mail or pager notification. |
| `include_event_description` | **tinyint** | Is whether the description of the SQL Server error from the Microsoft Windows application log should be included as part of the notification message. |
| `database_name` | **sysname** | Database in which the error must occur for the alert to fire. If the database name is `NULL`, the alert fires regardless of where the error occurred. |
| `event_description_keyword` | **nvarchar(100)** | Description of the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] error in the Windows application log that must be like the supplied sequence of characters. |
| `occurrence_count` | **int** | Number of times the alert occurred. |
| `count_reset_date` | **int** | Date the `occurrence_count` was last reset. |
| `count_reset_time` | **int** | Time the `occurrence_count` was last reset. |
| `job_id` | **uniqueidentifier** | Identification number of the job to be executed in response to an alert. |
| `job_name` | **sysname** | Name of the job to be executed in response to an alert. |
| `has_notification` | **int** | Nonzero if one or more operators are notified for this alert. The value is one or more of the following values (`OR`ed together):<br /><br />`1` = has e-mail notification<br />`2` = has pager notification<br />`4` = has **net send** notification. |
| `flags` | **int** | [!INCLUDE [ssInternalOnly](../../includes/ssinternalonly-md.md)] |
| `performance_condition` | **nvarchar(512)** | If `type` is `2`, this column shows the definition of the performance condition; otherwise, the column is `NULL`. |
| `category_name` | **sysname** | [!INCLUDE [ssInternalOnly](../../includes/ssinternalonly-md.md)] Will always be `[Uncategorized]` for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] 7.0. |
| `wmi_namespace` | **sysname** | If `type` is `3`, this column shows the namespace for the WMI event. |
| `wmi_query` | **nvarchar(512)** | If `type` is `3`, this column shows the query for the WMI event. |
| `type` | **int** | Type of the event:<br /><br />`1` = [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] event alert<br />`2` = [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] performance alert<br />`3` = WMI event alert |

## Remarks

`sp_help_alert` must be run from the `msdb` database.

## Permissions

[!INCLUDE [msdb-execute-permissions](../../includes/msdb-execute-permissions.md)]

Other users must be granted one of the following [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent fixed database roles in the `msdb` database:

- **SQLAgentUserRole**
- **SQLAgentReaderRole**
- **SQLAgentOperatorRole**

For details about the permissions of these roles, see [SQL Server Agent Fixed Database Roles](../../ssms/agent/sql-server-agent-fixed-database-roles.md).

## Examples

The following example reports information about the `Demo: Sev. 25 Errors` alert.

```sql
USE msdb;
GO

EXEC sp_help_alert @alert_name = 'Demo: Sev. 25 Errors';
GO
```

## Related content

- [sp_add_alert (Transact-SQL)](sp-add-alert-transact-sql.md)
- [sp_update_alert (Transact-SQL)](sp-update-alert-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
