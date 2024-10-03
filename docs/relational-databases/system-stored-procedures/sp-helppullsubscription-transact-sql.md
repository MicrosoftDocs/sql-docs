---
title: "sp_helppullsubscription (Transact-SQL)"
description: sp_helppullsubscription displays information about one or more subscriptions at the Subscriber.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_helppullsubscription_TSQL"
  - "sp_helppullsubscription"
helpviewer_keywords:
  - "sp_helppullsubscription"
dev_langs:
  - "TSQL"
---
# sp_helppullsubscription (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Displays information about one or more subscriptions at the Subscriber. This stored procedure is executed at the Subscriber on the subscription database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_helppullsubscription
    [ [ @publisher = ] N'publisher' ]
    [ , [ @publisher_db = ] N'publisher_db' ]
    [ , [ @publication = ] N'publication' ]
    [ , [ @show_push = ] N'show_push' ]
[ ; ]
```

## Arguments

#### [ @publisher = ] N'*publisher*'

The name of the remote server. *@publisher* is **sysname**, with a default of `%`, which returns information for all Publishers.

#### [ @publisher_db = ] N'*publisher_db*'

The name of the Publisher database. *@publisher_db* is **sysname**, with a default of `%`, which returns all the Publisher databases.

#### [ @publication = ] N'*publication*'

The name of the publication. *@publication* is **sysname**, with a default of `%`, which returns all the publications. If this parameter equals to ALL, only pull subscriptions with independent_agent = `0` are returned.

#### [ @show_push = ] N'*show_push*'

Specifies whether all push subscriptions are to be returned. *@show_push* is **nvarchar(5)**, with a default of `false`, which doesn't return push subscriptions.

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `publisher` | **sysname** | Name of the Publisher. |
| `publisher database` | **sysname** | Name of the Publisher database. |
| `publication` | **sysname** | Name of the publication. |
| `independent_agent` | **bit** | Indicates whether there's a stand-alone Distribution Agent for this publication. |
| `subscription type` | **int** | Subscription type to the publication. |
| `distribution agent` | **nvarchar(100)** | Distribution Agent handling the subscription. |
| `publication description` | **nvarchar(255)** | Description of the publication. |
| `last updating time` | **date** | Time the subscription information was updated. This value is a Unicode string of ISO date (114) + ODBC time (121). The format is `yyyyMMdd HH:mm:ss.nnn` where `yyyy` is year, `MM` is month, `dd` is day, `HH` is hour, `mm` is minute, `ss` is seconds, and `nnn` is milliseconds. |
| `subscription name` | **varchar(386)** | Name of the subscription. |
| `last transaction timestamp` | **varbinary(16)** | Timestamp of the last replicated transaction. |
| `update mode` | **tinyint** | Type of updates allowed. |
| `distribution agent job_id` | **int** | Job ID of the Distribution Agent. |
| `enabled_for_synmgr` | **int** | Specifies whether the subscription can be synchronized through the [!INCLUDE [msCoName](../../includes/msconame-md.md)] Synchronization Manager. |
| `subscription guid` | **binary(16)** | Global identifier for the version of the subscription on the publication. |
| `subid` | **binary(16)** | Global identifier for an anonymous subscription. |
| `immediate_sync` | **bit** | Specifies whether the synchronization files are created or re-created each time the Snapshot Agent runs. |
| `publisher login` | **sysname** | Login ID used at the Publisher for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication. |
| `publisher password` | **nvarchar(524)** | Password (encrypted) used at the Publisher for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication. |
| `publisher security_mode` | **int** | Security mode implemented at the Publisher:<br /><br />`0` = [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication<br />`1` = Windows Authentication<br />`2` = The synchronization triggers use a static **sysservers** entry to do remote procedure call (RPC), and *publisher* must be defined in the **sysservers** table as a remote server or linked server. |
| `distributor` | **sysname** | Name of the Distributor. |
| `distributor_login` | **sysname** | Login ID used at the Distributor for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication. |
| `distributor_password` | **nvarchar(524)** | Password (encrypted) used at the Distributor for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication. |
| `distributor_security_mode` | **int** | Security mode implemented at the Distributor:<br /><br />`0` = [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication<br />`1` = Windows Authentication |
| `ftp_address` | **sysname** | [!INCLUDE [deprecated-parameter](../includes/deprecated-parameter.md)] |
| `ftp_port` | **int** | [!INCLUDE [deprecated-parameter](../includes/deprecated-parameter.md)] |
| `ftp_login` | **sysname** | [!INCLUDE [deprecated-parameter](../includes/deprecated-parameter.md)] |
| `ftp_password` | **nvarchar(524)** | [!INCLUDE [deprecated-parameter](../includes/deprecated-parameter.md)] |
| `alt_snapshot_folder` | **nvarchar(255)** | Location where snapshot folder is stored if the location is other than or in addition to the default location. |
| `working_directory` | **nvarchar(255)** | Fully qualified path to the directory where snapshot files are transferred using File Transfer Protocol (FTP) when that option is specified. |
| `use_ftp` | **bit** | Subscription is subscribing to Publication over the Internet and FTP addressing properties are configured. If `0`, Subscription isn't using FTP. If `1`, subscription is using FTP. |
| `publication_type` | **int** | Specifies the replication type of the publication:<br /><br />`0` = Transactional replication<br />`1` = Snapshot replication<br />`2` = Merge replication |
| `dts_package_name` | **sysname** | Specifies the name of the Data Transformation Services (DTS) package. |
| `dts_package_location` | **int** | Location where the DTS package is stored:<br /><br />`0` = Distributor<br />`1` = Subscriber |
| `offload_agent` | **bit** | Specifies if the agent can be activated remotely. If `0`, the agent can't be activated remotely. |
| `offload_server` | **sysname** | Specifies the network name of the server used for remote activation. |
| `last_sync_status` | **int** | Subscription status:<br /><br />`0` = All jobs are waiting to start<br />`1` = One or more jobs are starting<br />`2` = All jobs executed successfully<br />`3` = At least one job is executing<br />`4` = All jobs are scheduled and idle<br />`5` = At least one job is attempting to execute after a previous failure<br />`6` = At least one job failed to execute successfully |
| `last_sync_summary` | **sysname** | Description of last synchronization results. |
| `last_sync_time` | **datetime** | Time the subscription information was updated. This value is a Unicode string of ISO date (114) + ODBC time (121). The format is `yyyyMMdd HH:mm:ss.nnn` where `yyyy` is year, `MM` is month, `dd` is day, `HH` is hour, `mm` is minute, `ss` is seconds, and `nnn` is milliseconds. |
| `job_login` | **nvarchar(512)** | Is the Windows account under which the Distribution agent runs, which is returned in the format *domain*\\*username*. |
| `job_password` | **sysname** | For security reasons, a value of `**********` is always returned. |

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_helppullsubscription` is used in snapshot and transactional replication.

## Permissions

Only members of the **sysadmin** fixed server role or the **db_owner** fixed database role can execute `sp_helppullsubscription` .

## Related content

- [sp_addpullsubscription (Transact-SQL)](sp-addpullsubscription-transact-sql.md)
- [sp_droppullsubscription (Transact-SQL)](sp-droppullsubscription-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
