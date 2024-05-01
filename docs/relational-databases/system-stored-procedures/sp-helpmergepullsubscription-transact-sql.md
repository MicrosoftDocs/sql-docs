---
title: "sp_helpmergepullsubscription (Transact-SQL)"
description: Returns information about pull subscriptions that exist at a Subscriber.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 11/23/2023
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_helpmergepullsubscription"
  - "sp_helpmergepullsubscription_TSQL"
helpviewer_keywords:
  - "sp_helpmergepullsubscription"
dev_langs:
  - "TSQL"
---
# sp_helpmergepullsubscription (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns information about pull subscriptions that exist at a Subscriber. This stored procedure is executed at the Subscriber on the subscription database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_helpmergepullsubscription
    [ [ @publication = ] N'publication' ]
    [ , [ @publisher = ] N'publisher' ]
    [ , [ @publisher_db = ] N'publisher_db' ]
    [ , [ @subscription_type = ] N'subscription_type' ]
[ ; ]
```

## Arguments

#### [ @publication = ] N'*publication*'

The name of the publication. *@publication* is **sysname**, with a default of `%`. If *@publication* is `%`, information about all merge publications and subscriptions in the current database is returned.

#### [ @publisher = ] N'*publisher*'

The name of the Publisher. *@publisher* is **sysname**, with a default of `%`.

#### [ @publisher_db = ] N'*publisher_db*'

The name of the Publisher database. *@publisher_db* is **sysname**, with a default of `%`.

#### [ @subscription_type = ] N'*subscription_type*'

Specifies whether to show pull subscriptions. *@subscription_type* is **nvarchar(10)**, with a default of `pull`. Valid values are `push`, `pull`, `both`.

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `subscription_name` | **nvarchar(1000)** | Name of the subscription. |
| `publication` | **sysname** | Name of the publication. |
| `publisher` | **sysname** | Name of the Publisher. |
| `publisher_db` | **sysname** | Name of the Publisher database. |
| `subscriber` | **sysname** | Name of the Subscriber. |
| `subscriber_db` | **sysname** | Name of the subscription database. |
| `status` | **int** | Subscription status:<br /><br />`0` = Inactive subscription<br /><br />`1` = Active subscription<br /><br />`2` = Deleted subscription<br /><br />`3` = Detached subscription<br /><br />`4` = Attached subscription<br /><br />`5` = Subscription has been marked for reinitialization with upload<br /><br />`6` = Attaching the subscription failed<br /><br />`7` = Subscription restored from backup |
| `subscriber_type` | **int** | Type of Subscriber:<br /><br />`1` = Global<br /><br />`2` = Local<br /><br />`3` = Anonymous |
| `subscription_type` | **int** | Type of subscription:<br /><br />`0` = Push<br /><br />`1` = Pull<br /><br />`2` = Anonymous |
| `priority` | **float(8)** | Subscription priority. The value must be less than `100.00`. |
| `sync_type` | **tinyint** | Subscription synchronization type:<br /><br />`1` = Automatic<br /><br />`2` = Snapshot isn't used. |
| `description` | **nvarchar(255)** | Brief description of the pull subscription. |
| `merge_jobid` | **binary(16)** | Job ID of the Merge Agent. |
| `enabled_for_syncmgr` | **int** | Specifies whether the subscription can be synchronized through the [!INCLUDE [msCoName](../../includes/msconame-md.md)] Synchronization Manager. |
| `last_updated` | **nvarchar(26)** | Time that the Merge Agent last successfully synchronized the subscription. |
| `publisher_login` | **sysname** | The Publisher login name. |
| `publisher_password` | **sysname** | The Publisher password. |
| `publisher_security_mode` | **int** | Specifies the security mode of the Publisher:<br /><br />`0` = [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication<br /><br />`1` = Windows Authentication |
| `distributor` | **sysname** | Name of the Distributor. |
| `distributor_login` | **sysname** | The Distributor login name. |
| `distributor_password` | **sysname** | The Distributor password. |
| `distributor_security_mode` | **int** | Specifies the security mode of the Distributor:<br /><br />`0` = [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication<br /><br />`1` = Windows Authentication |
| `ftp_address` | **sysname** | Available for backward compatibility only. The network address of the file transfer protocol (FTP) service for the Distributor. |
| `ftp_port` | **int** | Available for backward compatibility only. The port number of the FTP service for the Distributor. |
| `ftp_login` | **sysname** | Available for backward compatibility only. The username used to connect to the FTP service. |
| `ftp_password` | **sysname** | Available for backward compatibility only. The user password used to connect to the FTP service. |
| `alt_snapshot_folder` | **nvarchar(255)** | Location where snapshot folder is stored if the location is other than or in addition to the default location. |
| `working_directory` | **nvarchar(255)** | Fully qualified path to the directory where snapshot files are transferred using FTP when that option is specified. |
| `use_ftp` | **bit** | Subscription is subscribing to publication over the Internet, and FTP addressing properties are configured. If `0`, Subscription isn't using FTP. If `1`, subscription is using FTP. |
| `offload_agent` | **bit** | Specifies if the agent can be activated and run remotely. If `0`, the agent can't be remotely activated. |
| `offload_server` | **sysname** | Name of the server used for remote activation. |
| `use_interactive_resolver` | **int** | Returns whether or not the interactive resolver is used during reconciliation. If `0`, the interactive resolver isn't used. |
| `subid` | **uniqueidentifier** | ID of the Subscriber. |
| `dynamic_snapshot_location` | **nvarchar(255)** | The path to the folder where the snapshot files are saved. |
| `last_sync_status` | **int** | Synchronization status:<br /><br />`1` = Starting<br /><br />`2` = Succeeded<br /><br />`3` = In progress<br /><br />`4` = Idle<br /><br />`5` = Retrying after a previous failure<br /><br />`6` = Failed<br /><br />`7` = Failed validation<br /><br />`8` = Passed validation<br /><br />`9` = Requested a shutdown |
| `last_sync_summary` | **sysname** | Description of last synchronization results. |
| `use_web_sync` | **bit** | Specifies if the subscription can be synchronized over HTTPS, where a value of `1` means that this feature is enabled. |
| `internet_url` | **nvarchar(260)** | URL that represents the location of the replication listener for Web synchronization. |
| `internet_login` | **nvarchar(128)** | Login that the Merge Agent uses when connecting to the Web server that is hosting Web synchronization using Basic Authentication. |
| `internet_password` | **nvarchar(524)** | Password for the login that the Merge Agent uses when connecting to the Web server that is hosting Web synchronization using Basic Authentication. |
| `internet_security_mode` | **int** | The authentication mode used when connecting to the Web server that is hosting Web synchronization. A value of `1` means Windows Authentication, and a value of `0` means [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication. |
| `internet_timeout` | **int** | Length of time, in seconds, before a Web synchronization request expires. |
| `hostname` | **nvarchar(128)** | Specifies an overloaded value for [HOST_NAME](../../t-sql/functions/host-name-transact-sql.md) when this function is used in the WHERE clause of a parameterized row filter. |
| `job_login` | **nvarchar(512)** | The Windows account under which the Merge agent runs, which is returned in the format *domain*\\*username*. |
| `job_password` | **sysname** | For security reasons, a value of `**` is always returned. |

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_helpmergepullsubscription` is used in merge replication. In the result set, the date returned in `last_updated` is formatted as `yyyyMMdd hh:mm:ss.fff`.

## Permissions

Only members of the **sysadmin** fixed server role and the **db_owner** fixed database role can execute `sp_helpmergepullsubscription`.

## Related content

- [sp_addmergepullsubscription (Transact-SQL)](sp-addmergepullsubscription-transact-sql.md)
- [sp_changemergepullsubscription (Transact-SQL)](sp-changemergepullsubscription-transact-sql.md)
- [sp_dropmergepullsubscription (Transact-SQL)](sp-dropmergepullsubscription-transact-sql.md)
- [Replication stored procedures (Transact-SQL)](replication-stored-procedures-transact-sql.md)
