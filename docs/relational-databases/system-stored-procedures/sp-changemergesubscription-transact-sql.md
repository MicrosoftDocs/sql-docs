---
title: "sp_changemergesubscription (Transact-SQL)"
description: "Changes selected properties of a merge push subscription. This stored procedure is executed at the Publisher on the publication database."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_changemergesubscription_TSQL"
  - "sp_changemergesubscription"
helpviewer_keywords:
  - "sp_changemergesubscription"
dev_langs:
  - "TSQL"
---
# sp_changemergesubscription (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Changes selected properties of a merge push subscription. This stored procedure is executed at the Publisher on the publication database.

> [!IMPORTANT]  
> When configuring a Publisher with a remote Distributor, the values supplied for all parameters, including *@job_login* and *@job_password*, are sent to the Distributor as plain text. You should encrypt the connection between the Publisher and its remote Distributor before executing this stored procedure. For more information, see [Configure SQL Server Database Engine for encrypting connections](../../database-engine/configure-windows/configure-sql-server-encryption.md).

## Syntax

```syntaxsql
sp_changemergesubscription
    [ [ @publication = ] N'publication' ]
    [ , [ @subscriber = ] N'subscriber' ]
    [ , [ @subscriber_db = ] N'subscriber_db' ]
    [ , [ @property = ] N'property' ]
    [ , [ @value = ] N'value' ]
    [ , [ @force_reinit_subscription = ] force_reinit_subscription ]
[ ; ]
```

## Arguments

#### [ @publication = ] N'*publication*'

The name of the publication to change. *@publication* is **sysname**, with a default of `NULL`. The publication must already exist and must conform to the rules for identifiers.

#### [ @subscriber = ] N'*subscriber*'

The name of the Subscriber. *@subscriber* is **sysname**, with a default of `NULL`.

#### [ @subscriber_db = ] N'*subscriber_db*'

The name of the subscription database. *@subscriber_db* is **sysname**, and can be one of the values in the following table.

#### [ @property = ] N'*property*'

The property to change for the given publication. *@property* is **sysname**, and can be one of the values in the following table.

#### [ @value = ] N'*value*'

The new value for the specified *@property*. *@value* is **nvarchar(255)**, with a default of `NULL`.

| Property | Value | Description |
| --- | --- | --- |
| `description` | | Description of this merge subscription. |
| `priority` | | The subscription priority. The priority is used by the default resolver to pick a winner when conflicts are detected. |
| `merge_job_login` | | Login for the Windows account under which the agent runs. |
| `merge_job_password` | | Password for the Windows account under which the agent runs. |
| `publisher_security_mode` | `1` | Use Windows Authentication when connecting to the Publisher. |
| | `0` | Use [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication when connecting to the Publisher. |
| `publisher_login` | | Login name at the Publisher. |
| `publisher_password` | | Strong password for the supplied Publisher login. |
| `subscriber_security_mode` | `1` | Use Windows Authentication when connecting to the Subscriber. |
| | `0` | Use [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication when connecting to the Subscriber. |
| `subscriber_login` | | Login name at the Subscriber. |
| `subscriber_password` | | Strong password for the supplied Subscriber login. |
| `sync_type` | `automatic` | Schema and initial data for published tables are transferred to the Subscriber first. |
| | `none` | Subscriber already has the schema and initial data for published tables; system tables and data are always transferred. |
| `use_interactive_resolver` | `true` | Allows conflicts to be resolved interactively for all articles that allow interactive resolution. |
| | `false` | Conflicts are resolved automatically using a default resolver or custom resolver. |
| `NULL` (default) | `NULL` (default) | |

#### [ @force_reinit_subscription = ] *force_reinit_subscription*

Acknowledges that the action taken by this stored procedure might require existing subscriptions to be reinitialized. *@force_reinit_subscription* is **bit**, with a default of `0`.

- `0` specifies that changes to the merge article don't cause the subscription to be reinitialized. If the stored procedure detects that the change would require subscriptions to be reinitialized, an error occurs and no changes are made.

- `1` specifies that changes to the merge article reinitialize existing subscriptions, and gives permission for the subscription reinitialization to occur.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_changemergesubscription` is used in merge replication.

After changing an agent login or password, you must stop and restart the agent before the change takes effect.

## Permissions

Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute `sp_changemergesubscription`.

## Related content

- [sp_addmergesubscription (Transact-SQL)](sp-addmergesubscription-transact-sql.md)
- [sp_dropmergesubscription (Transact-SQL)](sp-dropmergesubscription-transact-sql.md)
- [sp_helpmergesubscription (Transact-SQL)](sp-helpmergesubscription-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
