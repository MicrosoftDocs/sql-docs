---
title: "sp_changesubscription (Transact-SQL)"
description: Changes the properties of a snapshot or transactional push subscription or a pull subscription involved in queued updating transactional replication.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/05/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "changesubscription"
  - "sp_changesubscription"
  - "changesubscription_TSQL"
  - "sp_changesubscription_TSQL"
helpviewer_keywords:
  - "sp_changesubscription"
dev_langs:
  - "TSQL"
---
# sp_changesubscription (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Changes the properties of a snapshot or transactional push subscription or a pull subscription involved in queued updating transactional replication. To change properties of all other types of pull subscriptions, use [sp_change_subscription_properties](sp-change-subscription-properties-transact-sql.md). `sp_changesubscription` is executed at the Publisher on the publication database.

> [!IMPORTANT]  
> When configuring a Publisher with a remote Distributor, the values supplied for all parameters, including *@job_login* and *@job_password*, are sent to the Distributor as plain text. You should encrypt the connection between the Publisher and its remote Distributor before executing this stored procedure. For more information, see [Configure SQL Server Database Engine for encrypting connections](../../database-engine/configure-windows/configure-sql-server-encryption.md).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_changesubscription
    [ @publication = ] N'publication'
    , [ @article = ] N'article'
    , [ @subscriber = ] N'subscriber'
    , [ @destination_db = ] N'destination_db'
    , [ @property = ] N'property'
    , [ @value = ] N'value'
    [ , [ @publisher = ] N'publisher' ]
[ ; ]
```

## Arguments

#### [ @publication = ] N'*publication*'

The name of the publication to change. *@publication* is **sysname**, with no default.

#### [ @article = ] N'*article*'

The name of the article to change. *@article* is **sysname**, with no default.

#### [ @subscriber = ] N'*subscriber*'

The name of the Subscriber. *@subscriber* is **sysname**, with no default.

#### [ @destination_db = ] N'*destination_db*'

The name of the subscription database. *@destination_db* is **sysname**, with no default.

#### [ @property = ] N'*property*'

The property to change for the given subscription. *@property* is **nvarchar(30)**, and can be one of the values in the table.

#### [ @value = ] N'*value*'

The new value for the specified *property*. *@value* is **nvarchar(4000)**, and can be one of the values in the table.

| Property | Value | Description |
| --- | --- | --- |
| `distrib_job_login` | | Login for the Windows account under which the agent runs. |
| `distrib_job_password` | | Password for the Windows account under which the agent runs. |
| `subscriber_catalog` <sup>1</sup> | | Catalog to be used when making a connection to the OLE DB provider. |
| `subscriber_datasource` <sup>1</sup> | | Name of the data source as understood by the OLE DB provider. |
| `subscriber_location` <sup>1</sup> | | Location of the database as understood by the OLE DB provider. |
| `subscriber_login` | | Login name at the Subscriber. |
| `subscriber_password` | | Strong password for the supplied login. |
| `subscriber_security_mode` | `1` | Use Windows Authentication when connecting to the Subscriber. |
| | `0` | Use [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication when connecting to the Subscriber. |
| `subscriber_provider` <sup>1</sup> | | Unique programmatic identifier (PROGID) with which the OLE DB provider for the non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] data source is registered. |
| `subscriber_providerstring` <sup>1</sup> | | OLE DB provider-specific connection string that identifies the data source. |
| `subscriptionstreams` | | The number of connections allowed per Distribution Agent to apply batches of changes in parallel to a Subscriber. A range of values from `1` to `64` is supported for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Publishers. This property must be `0` for non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Subscribers, Oracle Publishers, or peer-to-peer subscriptions. |
| `subscriber_type` | `1` | ODBC data source server |
| | `3` | OLE DB provider |
| `memory_optimized` | **bit** | Indicates that the subscription supports memory optimized tables. `memory_optimized` is **bit**, where `1` is `true` (the subscription supports memory optimized tables). |

<sup>1</sup> This property is only valid for non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Subscribers.

#### [ @publisher = ] N'*publisher*'

Specifies a non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Publisher. *@publisher* is **sysname**, with a default of `NULL`.

*@publisher* shouldn't be specified for a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Publisher.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_changesubscription` is used in snapshot and transactional replication.

`sp_changesubscription` can only be used to modify the properties of push subscriptions or pull subscriptions involved in queued updating transactional replication. To change properties of all other types of pull subscriptions, use [sp_change_subscription_properties](sp-change-subscription-properties-transact-sql.md).

After changing an agent login or password, you must stop and restart the agent before the change takes effect.

## Permissions

Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute `sp_changesubscription`.

## Related content

- [sp_addsubscription (Transact-SQL)](sp-addsubscription-transact-sql.md)
- [sp_dropsubscription (Transact-SQL)](sp-dropsubscription-transact-sql.md)
