---
title: "sp_helpsubscriberinfo (Transact-SQL)"
description: sp_helpsubscriberinfo displays information about a Subscriber.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/15/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_helpsubscriberinfo"
  - "sp_helpsubscriberinfo_TSQL"
helpviewer_keywords:
  - "sp_helpsubscriberinfo"
dev_langs:
  - "TSQL"
---
# sp_helpsubscriberinfo (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Displays information about a Subscriber. This stored procedure is executed at the Publisher on any database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_helpsubscriberinfo
    [ [ @subscriber = ] N'subscriber' ]
    [ , [ @publisher = ] N'publisher' ]
[ ; ]
```

## Arguments

#### [ @subscriber = ] N'*subscriber*'

The name of the Subscriber. *@subscriber* is **sysname**, with a default of `%`, which returns all information.

#### [ @publisher = ] N'*publisher*'

The name of the Publisher. *@publisher* is **sysname**, and defaults to the name of the current server.

*@publisher* shouldn't be specified, except when it's an Oracle Publisher.

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `publisher` | **sysname** | Name of the Publisher. |
| `subscriber` | **sysname** | Name of the Subscriber. |
| `type` | **tinyint** | Type of Subscriber:<br /><br />`0` = [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] database `1` = ODBC data source |
| `login` | **sysname** | Login ID for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication. |
| `password` | **sysname** | Password for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication. |
| `commit_batch_size` | **int** | Not supported. |
| `status_batch_size` | **int** | Not supported. |
| `flush_frequency` | **int** | Not supported. |
| `frequency_type` | **int** | Frequency with which the Distribution Agent is run:<br /><br />`1` = One time<br />`2` = On demand<br />`4` = Daily<br />`8` = Weekly<br />`16` = Monthly<br />`32` = Monthly relative<br />`64` = Autostart<br />`128` = Recurring |
| `frequency_interval` | **int** | Value applied to the frequency set by `frequency_type`. |
| `frequency_relative_interval` | **int** | Date of the Distribution Agent used when `frequency_type` is set to `32` (monthly relative):<br /><br />`1` = First<br />`2` = Second<br />`4` = Third<br />`8` = Fourth<br />`16` = Last |
| `frequency_recurrence_factor` | **int** | Recurrence factor used by `frequency_type`. |
| `frequency_subday` | **int** | How often to reschedule during the defined period:<br /><br />`1` = Once<br />`2` = Second<br />`4` = Minute<br />`8` = Hour |
| `frequency_subday_interval` | **int** | Interval for `frequency_subday`. |
| `active_start_time_of_day` | **int** | Time of day when the Distribution Agent is first scheduled, formatted as `HHmmss`. |
| `active_end_time_of_day` | **int** | Time of day when the Distribution Agent stops being scheduled, formatted as `HHmmss`. |
| `active_start_date` | **int** | Date when the Distribution Agent is first scheduled, formatted as `yyyyMMdd`. |
| `active_end_date` | **int** | Date when the Distribution Agent stops being scheduled, formatted as `yyyyMMdd`. |
| `retryattempt` | **int** | Not supported. |
| `retrydelay` | **int** | Not supported. |
| `description` | **nvarchar(255)** | Text description of the Subscriber. |
| `security_mode` | **int** | Implemented security mode:<br /><br />`0` = [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication<br />`1` = Windows Authentication |
| `frequency_type2` | **int** | Frequency with which the Merge Agent is run:<br /><br />`1` = One time<br />`2` = On demand<br />`4` = Daily<br />`8` = Weekly<br />`16` = Monthly<br />`32` = Monthly relative<br />`64` = Autostart<br />`128` = Recurring |
| `frequency_interval2` | **int** | Value applied to the frequency set by `frequency_type`. |
| `frequency_relative_interval2` | **int** | Date of the Merge Agent used when `frequency_type` is set to 32(monthly relative):<br /><br />`1` = First<br />`2` = Second<br />`4` = Third<br />`8` = Fourth<br />`16` = Last |
| `frequency_recurrence_factor2` | **int** | Recurrence factor used by `frequency_type`. |
| `frequency_subday2` | **int** | How often to reschedule during the defined period:<br /><br />`1` = Once<br />`2` = Second<br />`4` = Minute<br />`8` = Hour |
| `frequency_subday_interval2` | **int** | Interval for `frequency_subday`. |
| `active_start_time_of_day2` | **int** | Time of day when the Merge Agent is first scheduled, formatted as `HHmmss`. |
| `active_end_time_of_day2` | **int** | Time of day when the Merge Agent stops being scheduled, formatted as `HHmmss`. |
| `active_start_date2` | **int** | Date when the Merge Agent is first scheduled, formatted as `yyyyMMdd`. |
| `active_end_date2` | **int** | Date when the Merge Agent stops being scheduled, formatted as `yyyyMMdd`. |

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_helpsubscriberinfo` is used in snapshot replication, transactional replication, and merge replication.

## Permissions

Only members of the **sysadmin** fixed server role, the **db_owner** fixed database role, or the publication access list for the publication can execute `sp_helpsubscriberinfo`.

## Related content

- [sp_adddistpublisher (Transact-SQL)](sp-adddistpublisher-transact-sql.md)
- [sp_addpullsubscription (Transact-SQL)](sp-addpullsubscription-transact-sql.md)
- [sp_changesubscriber (Transact-SQL)](sp-changesubscriber-transact-sql.md)
- [sp_dropsubscriber (Transact-SQL)](sp-dropsubscriber-transact-sql.md)
- [sp_helpdistributor (Transact-SQL)](sp-helpdistributor-transact-sql.md)
- [sp_helpserver (Transact-SQL)](sp-helpserver-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
