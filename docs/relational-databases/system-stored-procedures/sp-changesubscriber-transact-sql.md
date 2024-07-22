---
title: "sp_changesubscriber (Transact-SQL)"
description: sp_changesubscriber changes the options for a Subscriber.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/05/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_changesubscriber"
  - "sp_changesubscriber_TSQL"
helpviewer_keywords:
  - "sp_changesubscriber"
dev_langs:
  - "TSQL"
---
# sp_changesubscriber (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Changes the options for a Subscriber. Any distribution task for the Subscribers to this Publisher is updated. This stored procedure writes to the `MSsubscriber_info` table in the distribution database. This stored procedure is executed at the Publisher on the publication database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_changesubscriber
    [ @subscriber = ] N'subscriber'
    [ , [ @type = ] type ]
    [ , [ @login = ] N'login' ]
    [ , [ @password = ] N'password' ]
    [ , [ @commit_batch_size = ] commit_batch_size ]
    [ , [ @status_batch_size = ] status_batch_size ]
    [ , [ @flush_frequency = ] flush_frequency ]
    [ , [ @frequency_type = ] frequency_type ]
    [ , [ @frequency_interval = ] frequency_interval ]
    [ , [ @frequency_relative_interval = ] frequency_relative_interval ]
    [ , [ @frequency_recurrence_factor = ] frequency_recurrence_factor ]
    [ , [ @frequency_subday = ] frequency_subday ]
    [ , [ @frequency_subday_interval = ] frequency_subday_interval ]
    [ , [ @active_start_time_of_day = ] active_start_time_of_day ]
    [ , [ @active_end_time_of_day = ] active_end_time_of_day ]
    [ , [ @active_start_date = ] active_start_date ]
    [ , [ @active_end_date = ] active_end_date ]
    [ , [ @description = ] N'description' ]
    [ , [ @security_mode = ] security_mode ]
    [ , [ @publisher = ] N'publisher' ]
[ ; ]
```

## Arguments

#### [ @subscriber = ] N'*subscriber*'

The name of the Subscriber on which to change the options. *@subscriber* is **sysname**, with no default.

#### [ @type = ] *type*

The Subscriber type. *@type* is **tinyint**, with a default of `NULL`.

- `0` indicates a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Subscriber.
- `1` specifies a non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or other ODBC data source server Subscriber.

#### [ @login = ] N'*login*'

The [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication login ID. *@login* is **sysname**, with a default of `NULL`.

#### [ @password = ] N'*password*'

The [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication password. *@password* is **sysname**, with a default of `%`, which indicates there's no change to the password property.

#### [ @commit_batch_size = ] *commit_batch_size*

[!INCLUDE [deprecated-parameter](../includes/deprecated-parameter.md)]

#### [ @status_batch_size = ] *status_batch_size*

[!INCLUDE [deprecated-parameter](../includes/deprecated-parameter.md)]

#### [ @flush_frequency = ] *flush_frequency*

[!INCLUDE [deprecated-parameter](../includes/deprecated-parameter.md)]

#### [ @frequency_type = ] *frequency_type*

Specifies the frequency with which to schedule the distribution task. *@frequency_type* is **int**, and can be one of these values.

| Value | Description |
| --- | --- |
| `1` | One time |
| `2` | On demand |
| `4` | Daily |
| `8` | Weekly |
| `16` | Monthly |
| `32` | Monthly relative |
| `64` | Autostart |
| `128` | Recurring |

#### [ @frequency_interval = ] *frequency_interval*

The interval for *@frequency_type*. *@frequency_interval* is **int**, with a default of `NULL`.

#### [ @frequency_relative_interval = ] *frequency_relative_interval*

The date of the distribution task. This parameter is used when *@frequency_type* is set to `32` (monthly relative). *@frequency_relative_interval* is **int**, and can be one of these values.

| Value | Description |
| --- | --- |
| `1` | First |
| `2` | Second |
| `4` | Third |
| `8` | Fourth |
| `16` | Last |

#### [ @frequency_recurrence_factor = ] *frequency_recurrence_factor*

Specifies how often the distribution task should recur during the defined *@frequency_type*. *@frequency_recurrence_factor* is **int**, with a default of `NULL`.

#### [ @frequency_subday = ] *frequency_subday*

Specifies how often to reschedule during the defined period. *@frequency_subday* is **int**, and can be one of these values.

| Value | Description |
| --- | --- |
| `1` | Once |
| `2` | Second |
| `4` | Minute |
| `8` | Hour |

#### [ @frequency_subday_interval = ] *frequency_subday_interval*

The interval for *@frequence_subday*. *@frequency_subday_interval* is **int**, with a default of `NULL`.

#### [ @active_start_time_of_day = ] *active_start_time_of_day*

The time of day when the distribution task is first scheduled, formatted as `HHmmss`. *@active_start_time_of_day* is **int**, with a default of `NULL`.

#### [ @active_end_time_of_day = ] *active_end_time_of_day*

The time of day when the distribution task stops being scheduled, formatted as `HHmmss`. *@active_end_time_of_day* is **int**, with a default of `NULL`.

#### [ @active_start_date = ] *active_start_date*

The date when the distribution task is first scheduled, formatted as `yyyyMMdd`. *@active_start_date* is **int**, with a default of `NULL`.

#### [ @active_end_date = ] *active_end_date*

The date when the distribution task stops being scheduled, formatted as `yyyyMMdd`. *@active_end_date* is **int**, with a default of `NULL`.

#### [ @description = ] N'*description*'

An optional text description. *@description* is **nvarchar(255)**, with a default of `NULL`.

#### [ @security_mode = ] *security_mode*

The implemented security mode. *@security_mode* is **int**, and can be one of these values.

| Value | Description |
| --- | --- |
| `0` | [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication |
| `1` | Windows Authentication |

#### [ @publisher = ] N'*publisher*'

Specifies a non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Publisher. *@publisher* is **sysname**, with a default of `NULL`.

*@publisher* shouldn't be used when changing article properties on a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Publisher.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_changesubscriber` is used in all types of replication.

## Permissions

Only members of the **sysadmin** fixed server role can execute `sp_changesubscriber`.

## Related content

- [sp_addsubscriber (Transact-SQL)](sp-addsubscriber-transact-sql.md)
- [sp_dropsubscriber (Transact-SQL)](sp-dropsubscriber-transact-sql.md)
- [sp_helpdistributiondb (Transact-SQL)](sp-helpdistributiondb-transact-sql.md)
- [sp_helpserver (Transact-SQL)](sp-helpserver-transact-sql.md)
- [sp_helpsubscriberinfo (Transact-SQL)](sp-helpsubscriberinfo-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
