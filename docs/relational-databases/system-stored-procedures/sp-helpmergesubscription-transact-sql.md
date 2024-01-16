---
title: "sp_helpmergesubscription (Transact-SQL)"
description: Returns information about a subscription to a merge publication, both push and pull.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 11/23/2023
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_helpmergesubscription"
  - "sp_helpmergesubscription_TSQL"
helpviewer_keywords:
  - "sp_helpmergesubscription"
dev_langs:
  - "TSQL"
---
# sp_helpmergesubscription (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns information about a subscription to a merge publication, both push and pull. This stored procedure is executed at the Publisher on the publication database or at a republishing Subscriber on the subscription database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_helpmergesubscription
    [ [ @publication = ] N'publication' ]
    [ , [ @subscriber = ] N'subscriber' ]
    [ , [ @subscriber_db = ] N'subscriber_db' ]
    [ , [ @publisher = ] N'publisher' ]
    [ , [ @publisher_db = ] N'publisher_db' ]
    [ , [ @subscription_type = ] N'subscription_type' ]
    [ , [ @found = ] found OUTPUT ]
[ ; ]
```

## Arguments

#### [ @publication = ] N'*publication*'

The name of the publication. *@publication* is **sysname**, with a default of `%`. The publication must already exist and conform to the rules for [identifiers](../databases/database-identifiers.md). If `NULL` or `%`, information about all merge publications and subscriptions in the current database is returned.

#### [ @subscriber = ] N'*subscriber*'

The name of the Subscriber. *@subscriber* is **sysname**, with a default of `%`. If `NULL` or `%`, information about all subscriptions to the given publication is returned.

#### [ @subscriber_db = ] N'*subscriber_db*'

The name of the subscription database. *@subscriber_db* is **sysname**, with a default of `%`, which returns information about all subscription databases.

#### [ @publisher = ] N'*publisher*'

The name of the Publisher. *@publisher* is **sysname**, with a default of `%`, with a default of `%`, which returns information about all Publishers. The Publisher must be a valid server.

#### [ @publisher_db = ] N'*publisher_db*'

The name of the Publisher database. *@publisher_db* is **sysname**, with a default of `%`, which returns information about all Publisher databases.

#### [ @subscription_type = ] N'*subscription_type*'

The type of subscription. *@subscription_type* is **nvarchar(15)**, and can be one of these values.

| Value | Description |
| --- | --- |
| `push` (default) | Push subscription |
| `pull` | Pull subscription |
| `both` | Both a push and pull subscription |

#### [ @found = ] *found* OUTPUT

A flag to indicate returning rows. *@found* is an OUTPUT parameter of type **int**.

- `1` indicates the publication is found.
- `0` indicates the publication isn't found.

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `subscription_name` | **sysname** | Name of the subscription. |
| `publication` | **sysname** | Name of the publication. |
| `publisher` | **sysname** | Name of the Publisher. |
| `publisher_db` | **sysname** | Name of the Publisher database. |
| `subscriber` | **sysname** | Name of the Subscriber. |
| `subscriber_db` | **sysname** | Name of the subscription database. |
| `status` | **int** | Status of the subscription:<br /><br />`0` = All jobs are waiting to start<br /><br />`1` = One or more jobs are starting<br /><br />`2` = All jobs have executed successfully<br /><br />`3` = At least one job is executing<br /><br />`4` = All jobs are scheduled and idle<br /><br />`5` = At least one job is attempting to execute after a previous failure<br /><br />`6` = At least one job has failed to execute successfully |
| `subscriber_type` | **int** | Type of Subscriber. |
| `subscription_type` | **int** | Type of subscription:<br /><br />`0` = Push<br /><br />`1` = Pull<br /><br />`2` = Both |
| `priority` | **float(8)** | Number indicating the priority for the subscription. |
| `sync_type` | **tinyint** | Subscription sync type. |
| `description` | **nvarchar(255)** | Brief description of this merge subscription. |
| `merge_jobid` | **binary(16)** | Job ID of the Merge Agent. |
| `full_publication` | **tinyint** | Whether the subscription is to a full or filtered publication. |
| `offload_enabled` | **bit** | Specifies if offload execution of a replication agent has been set to run at the Subscriber. If `NULL`, execution is run at the Publisher. |
| `offload_server` | **sysname** | Name of the server to where the agent is running. |
| `use_interactive_resolver` | **int** | Returns whether or not the interactive resolver is used during reconciliation. If `0`, the interactive resolver not is used. |
| `hostname` | **sysname** | Value supplied when a subscription is filtered by the value of the [HOST_NAME](../../t-sql/functions/host-name-transact-sql.md) function. |
| `subscriber_security_mode` | **smallint** | The security mode at the Subscriber, where `1` means Windows Authentication, and `0` means [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication. |
| `subscriber_login` | **sysname** | The login name at the Subscriber. |
| `subscriber_password` | **sysname** | Actual Subscriber password is never returned. The result is masked by a `******` string. |

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_helpmergesubscription` is used in merge replication to return subscription information stored at the Publisher or republishing Subscriber.

For anonymous subscriptions, the *subscription_type*value is always `1` (pull). However, you must execute [sp_helpmergepullsubscription](sp-helpmergepullsubscription-transact-sql.md) at the Subscriber for information on anonymous subscriptions.

## Permissions

Only members of the **sysadmin** fixed server role, the **db_owner** fixed database role, or the publication access list for the publication to which the subscription belongs, can execute `sp_helpmergesubscription`.

## Related content

- [sp_addmergesubscription (Transact-SQL)](sp-addmergesubscription-transact-sql.md)
- [sp_changemergesubscription (Transact-SQL)](sp-changemergesubscription-transact-sql.md)
- [sp_dropmergesubscription (Transact-SQL)](sp-dropmergesubscription-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
