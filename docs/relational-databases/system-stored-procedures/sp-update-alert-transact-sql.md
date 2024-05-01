---
title: "sp_update_alert (Transact-SQL)"
description: Updates the settings of an existing alert.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 03/20/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_update_alert_TSQL"
  - "sp_update_alert"
helpviewer_keywords:
  - "sp_update_alert"
dev_langs:
  - "TSQL"
---
# sp_update_alert (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Updates the settings of an existing alert.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_update_alert
    [ @name = ] N'name'
    [ , [ @new_name = ] N'new_name' ]
    [ , [ @enabled = ] enabled ]
    [ , [ @message_id = ] message_id ]
    [ , [ @severity = ] severity ]
    [ , [ @delay_between_responses = ] delay_between_responses ]
    [ , [ @notification_message = ] N'notification_message' ]
    [ , [ @include_event_description_in = ] include_event_description_in ]
    [ , [ @database_name = ] N'database_name' ]
    [ , [ @event_description_keyword = ] N'event_description_keyword' ]
    [ , [ @job_id = ] 'job_id' ]
    [ , [ @job_name = ] N'job_name' ]
    [ , [ @occurrence_count = ] occurrence_count ]
    [ , [ @count_reset_date = ] count_reset_date ]
    [ , [ @count_reset_time = ] count_reset_time ]
    [ , [ @last_occurrence_date = ] last_occurrence_date ]
    [ , [ @last_occurrence_time = ] last_occurrence_time ]
    [ , [ @last_response_date = ] last_response_date ]
    [ , [ @last_response_time = ] last_response_time ]
    [ , [ @raise_snmp_trap = ] raise_snmp_trap ]
    [ , [ @performance_condition = ] N'performance_condition' ]
    [ , [ @category_name = ] N'category_name' ]
    [ , [ @wmi_namespace = ] N'wmi_namespace' ]
    [ , [ @wmi_query = ] N'wmi_query' ]
[ ; ]
```

## Arguments

#### [ @name = ] N'*name*'

The name of the alert that is to be updated. *@name* is **sysname**, with no default.

#### [ @new_name = ] N'*new_name*'

A new name for the alert. The name must be unique. *@new_name* is **sysname**, with a default of `NULL`.

#### [ @enabled = ] *enabled*

Specifies whether the alert is enabled (`1`) or not enabled (`0`). *@enabled* is **tinyint**, with a default of `NULL`. An alert must be enabled to fire.

#### [ @message_id = ] *message_id*

A new message or error number for the alert definition. Typically, *message_id* corresponds to an error number in the **sysmessages** table. *@message_id* is **int**, with a default of `NULL`. A message ID can be used only if the severity level setting for the alert is `0`.

#### [ @severity = ] *severity*

A new severity level (from `1` through `25`) for the alert definition. Any [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] message sent to the Windows application log with the specified severity activates the alert. *@severity* is **int**, with a default of `NULL`. A severity level can be used only if the message ID setting for the alert is `0`.

#### [ @delay_between_responses = ] *delay_between_responses*

The new waiting period, in seconds, between responses to the alert. *@delay_between_responses* is **int**, with a default of `NULL`.

#### [ @notification_message = ] N'*notification_message*'

The revised text of an additional message sent to the operator as part of the e-mail, **net send**, or pager notification. *@notification_message* is **nvarchar(512)**, with a default of `NULL`.

#### [ @include_event_description_in = ] *include_event_description_in*

Specifies whether the description of the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] error from the Windows application log should be included in the notification message. *@include_event_description_in* is **tinyint**, and can be one or more of these values.

| Value | Description |
| --- | --- |
| `0` | None |
| `1` | E-mail |
| `2` | Pager |
| `4` | `net send` |
| `7` | All |

#### [ @database_name = ] N'*database_name*'

The name of the database in which the error must occur for the alert to fire. *@database_name* is **sysname**, with a default of `NULL`. Names that are enclosed in brackets (`[]`) aren't allowed.

#### [ @event_description_keyword = ] N'*event_description_keyword*'

A sequence of characters that must be found in the description of the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] error in the error message log. *@event_description_keyword* is **nvarchar(100)**, with a default of `NULL`. This parameter is useful for filtering object names (for example, `customer_table`).

> [!NOTE]  
> [!INCLUDE [tsql](../../includes/tsql-md.md)] `LIKE` expression pattern-matching characters can't be used.

#### [ @job_id = ] '*job_id*'

*@job_id* is **uniqueidentifier**, with a default of `NULL`.

#### [ @job_name = ] N'*job_name*'

The job identification number. *@job_name* is **sysname**, with a default of `NULL`. If *job_id* is specified, *job_name* must be omitted.

#### [ @occurrence_count = ] *occurrence_count*

Resets the number of times the alert has occurred. *@occurrence_count* is **int**, with a default of `NULL`, and can only be set to `0`.

#### [ @count_reset_date = ] *count_reset_date*

Resets the date the occurrence count was last reset. *@count_reset_date* is **int**, with a default of `NULL`.

#### [ @count_reset_time = ] *count_reset_time*

Resets the time the occurrence count was last reset. *@count_reset_time* is **int**, with a default of `NULL`.

#### [ @last_occurrence_date = ] *last_occurrence_date*

Resets the date the alert last occurred. *@last_occurrence_date* is **int**, with a default of `NULL`, and can be set only to `0`.

#### [ @last_occurrence_time = ] *last_occurrence_time*

Resets the time the alert last occurred. *@last_occurrence_time* is **int**, with a default of `NULL`and can be set only to `0`.

#### [ @last_response_date = ] *last_response_date*

Resets the date the alert was last responded to by the SQLServerAgent service. *@last_response_date* is **int**, with a default of `NULL`, and can be set only to `0`.

#### [ @last_response_time = ] *last_response_time*

Resets the time the alert was last responded to by the SQLServerAgent service. *@last_response_time* is **int**, with a default of `NULL`, and can be set only to `0`.

#### [ @raise_snmp_trap = ] *raise_snmp_trap*

[!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)]

#### [ @performance_condition = ] N'*performance_condition*'

A value expressed in the format `<itemcomparatorvalue>`. *@performance_condition* is **nvarchar(512)**, and consists of the following elements.

| Format element | Description |
| --- | --- |
| *Item* | A performance object, performance counter, or named instance of the counter |
| *Comparator* | One of these operators: `>`, `<`, `=` |
| *Value* | Numeric value of the counter |

#### [ @category_name = ] N'*category_name*'

The name of the alert category. *@category_name* is **sysname**, with a default of `NULL`.

#### [ @wmi_namespace = ] N'*wmi_namespace*'

The WMI namespace to query for events. *@wmi_namespace* is **sysname**, with a default of `NULL`.

#### [ @wmi_query = ] N'*wmi_query*'

The query that specifies the WMI event for the alert. *@wmi_query* is **nvarchar(512)**, with a default of `NULL`.

## Return code values

`0` (success) or `1` (failure).

## Remarks

Only `sysmessages` written to the Windows application log can fire an alert.

`sp_update_alert` changes only those alert settings for which parameter values are supplied. If a parameter is omitted, the current setting is retained.

## Permissions

To run this stored procedure, users must be a member of the **sysadmin** fixed server role.

## Examples

The following example changes the enabled setting of `Test Alert` to `0`.

```sql
USE msdb;
GO

EXEC dbo.sp_update_alert
    @name = N'Test Alert',
    @enabled = 0;
GO
```

## Related content

- [sp_add_alert (Transact-SQL)](sp-add-alert-transact-sql.md)
- [sp_help_alert (Transact-SQL)](sp-help-alert-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
