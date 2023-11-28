---
title: "sp_resyncmergesubscription (Transact-SQL)"
description: Resynchronizes a merge subscription to a known validation state that you specify.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 11/23/2023
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_resyncmergesubscription_TSQL"
  - "sp_resyncmergesubscription"
helpviewer_keywords:
  - "sp_resyncmergesubscription"
dev_langs:
  - "TSQL"
---
# sp_resyncmergesubscription (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Resynchronizes a merge subscription to a known validation state that you specify. You can force convergence or synchronize the subscription database to a specific point in time, such as the last time there was a successful validation, or to a specified date. The snapshot isn't reapplied when resynchronizing a subscription using this method. This stored procedure isn't used for snapshot replication subscriptions or transactional replication subscriptions. This stored procedure is executed at the Publisher, on the publication database, or at the Subscriber, on the subscription database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_resyncmergesubscription
    [ [ @publisher = ] N'publisher' ]
    [ , [ @publisher_db = ] N'publisher_db' ]
    , [ @publication = ] N'publication'
    [ , [ @subscriber = ] N'subscriber' ]
    [ , [ @subscriber_db = ] N'subscriber_db' ]
    , [ @resync_type = ] resync_type
    [ , [ @resync_date_str = ] N'resync_date_str' ]
[ ; ]
```

## Arguments

#### [ @publisher = ] N'*publisher*'

The name of the Publisher. *@publisher* is **sysname**, with a default of `NULL`. A value of `NULL` is valid if the stored procedure is run at the Publisher. If the stored procedure is run at the Subscriber, a Publisher must be specified.

#### [ @publisher_db = ] N'*publisher_db*'

The name of the publication database. *@publisher_db* is **sysname**, with a default of `NULL`. A value of `NULL` is valid if the stored procedure is run at the Publisher in the publication database. If the stored procedure is run at the Subscriber, a Publisher must be specified.

#### [ @publication = ] N'*publication*'

The name of the publication. *@publication* is **sysname**, with no default.

#### [ @subscriber = ] N'*subscriber*'

The name of the Subscriber. *@subscriber* is **sysname**, with a default of `NULL`. A value of `NULL` is valid if the stored procedure is run at the Subscriber. If the stored procedure is run at the Publisher, a Subscriber must be specified.

#### [ @subscriber_db = ] N'*subscriber_db*'

The name of the subscription database. *@subscriber_db* is **sysname**, with a default of `NULL`. A value of `NULL` is valid if the stored procedure is run at the Subscriber in the subscription database. If the stored procedure is run at the Publisher, a Subscriber must be specified.

#### [ @resync_type = ] *resync_type*

Defines when the resynchronization should start. *@resync_type* is **int**, and can be one of the following values.

| Value | Description |
| --- | --- |
| `0` | Synchronization starts from after the initial snapshot. This option is the most resource-intensive, because all changes since the initial snapshot are reapplied to the Subscriber. |
| `1` | Synchronization starts since last successful validation. All new or incomplete generations originating since the last successful validation are reapplied to the Subscriber. |
| `2` | Synchronization starts from the date given in *resync_date_str*. All new or incomplete generations originating after the date are reapplied to the Subscriber |

#### [ @resync_date_str = ] N'*resync_date_str*'

Defines the date when the resynchronization should start at. *@resync_date_str* is **nvarchar(30)**, with a default of `NULL`. This parameter is used when the *@resync_type* is a value of `2`. The date given is converted to its equivalent **datetime** value.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_resyncmergesubscription` is used in merge replication.

A value of `0` for the *@resync_type* parameter, which reapplies all changes since the initial snapshot, might be resource-intensive, but possibly a lot less than a full reinitialization. For example, if the initial snapshot was delivered one month ago, this value would cause data from the past month to be reapplied. If the initial snapshot contained 1 gigabyte (GB) of data, but the number of changes from the past month consisted of 2 megabytes (MB) of changed data, it would be more efficient to reapply the data than to reapply the full 1-GB snapshot.

## Permissions

Only members of the **sysadmin** fixed server role or the **db_owner** fixed database role can execute `sp_resyncmergesubscription`.

## Related content

- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
