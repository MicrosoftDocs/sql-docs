---
title: "sp_link_publication (Transact-SQL)"
description: Sets the configuration and security information used by synchronization triggers of immediate updating subscriptions when connecting to the Publisher.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/16/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_link_publication_TSQL"
  - "sp_link_publication"
helpviewer_keywords:
  - "sp_link_publication"
dev_langs:
  - "TSQL"
---
# sp_link_publication (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Sets the configuration and security information used by synchronization triggers of immediate updating subscriptions when connecting to the Publisher. This stored procedure is executed at the Subscriber on the subscription database.

> [!IMPORTANT]  
> When you configure a Publisher with a remote Distributor, the values supplied for all parameters, including *@job_login* and *@job_password*, are sent to the Distributor as plain text. You should encrypt the connection between the Publisher and its remote Distributor before executing this stored procedure. For more information, see [Configure SQL Server Database Engine for encrypting connections](../../database-engine/configure-windows/configure-sql-server-encryption.md).  

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_link_publication
    [ @publisher = ] N'publisher'
    , [ @publisher_db = ] N'publisher_db'
    , [ @publication = ] N'publication'
    , [ @security_mode = ] security_mode
    [ , [ @login = ] N'login' ]
    [ , [ @password = ] N'password' ]
    [ , [ @distributor = ] N'distributor' ]
[ ; ]
```

## Arguments

#### [ @publisher = ] N'*publisher*'

The name of the Publisher to link to. *@publisher* is **sysname**, with no default.

#### [ @publisher_db = ] N'*publisher_db*'

The name of the Publisher database to link to. *@publisher_db* is **sysname**, with no default.

#### [ @publication = ] N'*publication*'

The name of the publication to link to. *@publication* is **sysname**, with no default.

#### [ @security_mode = ] *security_mode*

The security mode used by the Subscriber to connect to a remote Publisher for immediate updating. *@security_mode* is **int**, and can be one of these values. [!INCLUDE [ssNoteWinAuthentication](../../includes/ssnotewinauthentication-md.md)]

| Value | Description |
| --- | --- |
| `0` | Uses [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication with the login specified in this stored procedure as *@login* and *@password*.<br /><br />Note: In previous versions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], this option was used to specify a dynamic remote procedure call (RPC). |
| `1` | Uses the security context ([!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication or Windows Authentication) of the user making the change at the Subscriber.<br /><br />Note: This account must also exist at the Publisher with sufficient privileges. When you use Windows Authentication, security account delegation must be supported. |
| `2` | Uses an existing, user-defined linked server login created using `sp_link_publication`. |

#### [ @login = ] N'*login*'

The login. *@login* is **sysname**, with a default of `NULL`. This parameter must be specified when *@security_mode* is `0`.

#### [ @password = ] N'*password*'

The password. *@password* is **sysname**, with a default of `NULL`. This parameter must be specified when *@security_mode* is `0`.

#### [ @distributor = ] N'*distributor*'

The name of the Distributor. *@distributor* is **sysname**, with a default of an empty string.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_link_publication` is used by immediate updating subscriptions in transactional replication.

`sp_link_publication` can be used for both push and pull subscriptions. It can be called before or after the subscription is created. An entry is inserted or updated in the [MSsubscription_properties](../system-tables/mssubscription-properties-transact-sql.md) system table.

For push subscriptions, the entry can be cleaned up by [sp_subscription_cleanup](sp-subscription-cleanup-transact-sql.md). For pull subscriptions, the entry can be cleaned up by [sp_droppullsubscription](sp-droppullsubscription-transact-sql.md) or [sp_subscription_cleanup](sp-subscription-cleanup-transact-sql.md). You can also call `sp_link_publication` with a `NULL` password to clear the entry in the [MSsubscription_properties](../system-tables/mssubscription-properties-transact-sql.md) system table for security concerns.

The default mode used by an immediate updating Subscriber when it connects to the Publisher doesn't allow a connection using Windows Authentication. To connect with a mode of Windows Authentication, a linked server has to be set up to the Publisher, and the immediate updating Subscriber should use this connection when updating the Subscriber. This requires the `sp_link_publication` to be run with *@security_mode* set to `2`. When you use Windows Authentication, security account delegation must be supported.

## Examples

:::code language="sql" source="../replication/codesnippet/tsql/sp-link-publication-tran_1.sql":::

## Permissions

Only members of the **sysadmin** fixed server role can execute `sp_link_publication`.

## Related content

- [sp_droppullsubscription (Transact-SQL)](sp-droppullsubscription-transact-sql.md)
- [sp_helpsubscription_properties (Transact-SQL)](sp-helpsubscription-properties-transact-sql.md)
- [sp_subscription_cleanup (Transact-SQL)](sp-subscription-cleanup-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
