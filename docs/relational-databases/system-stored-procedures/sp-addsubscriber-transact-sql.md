---
title: "sp_addsubscriber (Transact-SQL)"
description: sp_addsubscriber adds a new Subscriber to a Publisher, enabling it to receive publications.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 01/23/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_addsubscriber"
  - "sp_addsubscriber_TSQL"
helpviewer_keywords:
  - "sp_addsubscriber"
dev_langs:
  - "TSQL"
---
# sp_addsubscriber (Transact-SQL)

[!INCLUDE [sql-asdb](../../includes/applies-to-version/sql-asdb.md)]

Adds a new Subscriber to a Publisher, enabling it to receive publications. This stored procedure is executed at the Publisher on the publication database for snapshot and transactional publications; and for merge publications using a remote Distributor, this stored procedure is executed at the Distributor.

> [!IMPORTANT]  
> This stored procedure has been deprecated. You're no longer required to explicitly register a Subscriber at the Publisher.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_addsubscriber
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
    [ , [ @encrypted_password = ] encrypted_password ]
    [ , [ @publisher = ] N'publisher' ]
[ ; ]
```

## Arguments

#### [ @subscriber = ] N'*subscriber*'

The name of the server to be added as a valid Subscriber to the publications on this server. *@subscriber* is **sysname**, with no default.

#### [ @type = ] *type*

The type of Subscriber. *@type* is **tinyint**, and can be one of these values.

| Value | Description |
| --- | --- |
| `0` (default) | [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Subscriber |
| `1` | ODBC data source server |
| `2` | [!INCLUDE [msCoName](../../includes/msconame-md.md)] Jet database |
| `3` | OLE DB provider |

#### [ @login = ] N'*login*'

The login ID for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication. *@login* is **sysname**, with a default of `NULL`.

> [!NOTE]  
> This parameter is deprecated and is maintained for backward compatibility of scripts. The property is now specified on a per-subscription basis when executing [sp_addsubscription](sp-addsubscription-transact-sql.md). When a value is specified, it's used as a default when creating subscriptions at this Subscriber and a warning message is returned.

#### [ @password = ] N'*password*'

The password for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication. *@password* is **nvarchar(524)**, with a default of `NULL`.

Don't use a blank password. Use a strong password.

> [!NOTE]  
> This parameter is deprecated and is maintained for backward compatibility of scripts. The property is now specified on a per-subscription basis when executing [sp_addsubscription](sp-addsubscription-transact-sql.md). When a value is specified, it's used as a default when creating subscriptions at this Subscriber and a warning message is returned.

#### [ @commit_batch_size = ] *commit_batch_size*

This parameter is deprecated and is maintained for backward compatibility of scripts.

When a value is specified, it's used as a default when creating subscriptions at this Subscriber and a warning message is returned.

#### [ @status_batch_size = ] *status_batch_size*

This parameter is deprecated and is maintained for backward compatibility of scripts.

When a value is specified, it's used as a default when creating subscriptions at this Subscriber and a warning message is returned.

#### [ @flush_frequency = ] *flush_frequency*

This parameter is deprecated and is maintained for backward compatibility of scripts.

When a value is specified, it's used as a default when creating subscriptions at this Subscriber and a warning message is returned.

#### [ @frequency_type = ] *frequency_type*

Specifies the frequency with which to schedule the replication agent. *@frequency_type* is **int**, and can be one of these values.

| Value | Description |
| --- | --- |
| `1` | One time |
| `2` | On demand |
| `4` | Daily |
| `8` | Weekly |
| `16` | Monthly |
| `32` | Monthly relative |
| `64` (default) | Autostart |
| `128` | Recurring |

> [!NOTE]  
> This parameter is deprecated and is maintained for backward compatibility of scripts. The property is now specified on a per-subscription basis when executing [sp_addsubscription](sp-addsubscription-transact-sql.md). When a value is specified, it's used as a default when creating subscriptions at this Subscriber and a warning message is returned.

#### [ @frequency_interval = ] *frequency_interval*

The value applied to the frequency set by *@frequency_type*. *@frequency_interval* is **int**, with a default of `1`.

> [!NOTE]  
> This parameter is deprecated and is maintained for backward compatibility of scripts. The property is now specified on a per-subscription basis when executing [sp_addsubscription](sp-addsubscription-transact-sql.md). When a value is specified, it's used as a default when creating subscriptions at this Subscriber and a warning message is returned.

#### [ @frequency_relative_interval = ] *frequency_relative_interval*

The date of the replication agent. This parameter is used when *@frequency_type* is set to `32` (monthly relative). *@frequency_relative_interval* is **int**, and can be one of these values.

| Value | Description |
| --- | --- |
| `1` (default) | First |
| `2` | Second |
| `4` | Third |
| `8` | Fourth |
| `16` | Last |

> [!NOTE]  
> This parameter is deprecated and is maintained for backward compatibility of scripts. The property is now specified on a per-subscription basis when executing [sp_addsubscription](sp-addsubscription-transact-sql.md). When a value is specified, it's used as a default when creating subscriptions at this Subscriber and a warning message is returned.

#### [ @frequency_recurrence_factor = ] *frequency_recurrence_factor*

The recurrence factor used by *@frequency_type*. *@frequency_recurrence_factor* is **int**, with a default of `0`.

> [!NOTE]  
> This parameter is deprecated and is maintained for backward compatibility of scripts. The property is now specified on a per-subscription basis when executing [sp_addsubscription](sp-addsubscription-transact-sql.md). When a value is specified, it's used as a default when creating subscriptions at this Subscriber and a warning message is returned.

#### [ @frequency_subday = ] *frequency_subday*

How often to reschedule during the defined period. *@frequency_subday* is **int**, and can be one of these values.

| Value | Description |
| --- | --- |
| `1` | Once |
| `2` | Second |
| `4` (default) | Minute |
| `8` | Hour |

> [!NOTE]  
> This parameter is deprecated and is maintained for backward compatibility of scripts. The property is now specified on a per-subscription basis when executing [sp_addsubscription](sp-addsubscription-transact-sql.md). When a value is specified, it's used as a default when creating subscriptions at this Subscriber and a warning message is returned.

#### [ @frequency_subday_interval = ] *frequency_subday_interval*

The interval for *frequency_subday*. *@frequency_subday_interval* is **int**, with a default of `5`.

> [!NOTE]  
> This parameter is deprecated and is maintained for backward compatibility of scripts. The property is now specified on a per-subscription basis when executing [sp_addsubscription](sp-addsubscription-transact-sql.md). When a value is specified, it's used as a default when creating subscriptions at this Subscriber and a warning message is returned.

#### [ @active_start_time_of_day = ] *active_start_time_of_day*

The time of day when the replication agent is first scheduled, formatted as `HHmmss`. *@active_start_time_of_day* is **int**, with a default of `0`.

> [!NOTE]  
> This parameter is deprecated and is maintained for backward compatibility of scripts. The property is now specified on a per-subscription basis when executing [sp_addsubscription](sp-addsubscription-transact-sql.md). When a value is specified, it's used as a default when creating subscriptions at this Subscriber and a warning message is returned.

#### [ @active_end_time_of_day = ] *active_end_time_of_day*

The time of day when the replication agent stops being scheduled, formatted as `HHmmss`. *@active_end_time_of_day* is **int**, with a default of `235959`, which means 11:59:59 P.M. as measured on a 24-hour clock.

> [!NOTE]  
> This parameter is deprecated and is maintained for backward compatibility of scripts. The property is now specified on a per-subscription basis when executing [sp_addsubscription](sp-addsubscription-transact-sql.md). When a value is specified, it's used as a default when creating subscriptions at this Subscriber and a warning message is returned.

#### [ @active_start_date = ] *active_start_date*

The date when the replication agent is first scheduled, formatted as `yyyyMMdd`. *@active_start_date* is **int**, with a default of `0`.

> [!NOTE]  
> This parameter is deprecated and is maintained for backward compatibility of scripts. The property is now specified on a per-subscription basis when executing [sp_addsubscription](sp-addsubscription-transact-sql.md). When a value is specified, it's used as a default when creating subscriptions at this Subscriber and a warning message is returned.

#### [ @active_end_date = ] *active_end_date*

The date when the replication agent stops being scheduled, formatted as `yyyyMMdd`. *@active_end_date* is **int**, with a default of `99991231`, which means December 31, 9999.

> [!NOTE]  
> This parameter is deprecated and is maintained for backward compatibility of scripts. The property is now specified on a per-subscription basis when executing [sp_addsubscription](sp-addsubscription-transact-sql.md). When a value is specified, it's used as a default when creating subscriptions at this Subscriber and a warning message is returned.

#### [ @description = ] N'*description*'

A text description of the Subscriber. *@description* is **nvarchar(255)**, with a default of `NULL`.

#### [ @security_mode = ] *security_mode*

The implemented security mode. *@security_mode* is **int**, with a default of `1`.

- `0` specifies [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] authentication.
- `1` specifies Windows authentication.

> [!NOTE]  
> This parameter is deprecated and is maintained for backward compatibility of scripts. The property is now specified on a per-subscription basis when executing [sp_addsubscription](sp-addsubscription-transact-sql.md). When a value is specified, it's used as a default when creating subscriptions at this Subscriber and a warning message is returned.

#### [ @encrypted_password = ] *encrypted_password*

This parameter is deprecated and is provided for backward-compatibility only. Setting *@encrypted_password* to any value but `0` results in an error.

#### [ @publisher = ] N'*publisher*'

Specifies a non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Publisher. *@publisher* is **sysname**, with a default of `NULL`.

*@publisher* shouldn't be used when publishing from a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Publisher.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_addsubscriber` is used in snapshot replication, transactional replication, and merge replication.

`sp_addsubscriber` isn't required when the Subscriber only has anonymous subscriptions to merge publications.

`sp_addsubscriber` writes to the [MSsubscriber_info](../system-tables/mssubscriber-info-transact-sql.md) table in the **distribution** database.

## Permissions

Only members of the **sysadmin** fixed server role can execute `sp_addsubscriber`.

## Related content

- [Create a push subscription](../replication/create-a-push-subscription.md)
- [Create a Pull Subscription](../replication/create-a-pull-subscription.md)
- [sp_changesubscriber (Transact-SQL)](sp-changesubscriber-transact-sql.md)
- [sp_dropsubscriber (Transact-SQL)](sp-dropsubscriber-transact-sql.md)
- [sp_helpsubscriberinfo (Transact-SQL)](sp-helpsubscriberinfo-transact-sql.md)
