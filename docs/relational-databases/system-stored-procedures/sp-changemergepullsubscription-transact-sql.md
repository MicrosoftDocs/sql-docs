---
title: "sp_changemergepullsubscription (Transact-SQL)"
description: "Changes the properties of the merge pull subscription. This stored procedure is executed at the Subscriber on the subscription database."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 11/23/2023
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_changemergepullsubscription"
  - "sp_changemergepullsubscription_TSQL"
helpviewer_keywords:
  - "sp_changemergepullsubscription"
dev_langs:
  - "TSQL"
---
# sp_changemergepullsubscription (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Changes the properties of the merge pull subscription. This stored procedure is executed at the Subscriber on the subscription database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_changemergepullsubscription
    [ [ @publication = ] N'publication' ]
    [ , [ @publisher = ] N'publisher' ]
    [ , [ @publisher_db = ] N'publisher_db' ]
    [ , [ @property = ] N'property' ]
    [ , [ @value = ] N'value' ]
[ ; ]
```

## Arguments

#### [ @publication = ] N'*publication*'

The name of the publication. *@publication* is **sysname**, with a default of `%`.

#### [ @publisher = ] N'*publisher*'

The name of the Publisher. *@publisher* is **sysname**, with a default of `%`.

#### [ @publisher_db = ] N'*publisher_db*'

The name of the Publisher database. *@publisher_db* is **sysname**, with a default of `%`.

#### [ @property = ] N'*property*'

The name of the property to change. *@property* is **sysname**, and can be one of the values in the following table.

#### [ @value = ] N'*value*'

The new value for the specified property. *@value* is **nvarchar(255)**, and can be one of the values in the following table.

| Property | Value | Description |
| --- | --- | --- |
| `alt_snapshot_folder` | | Location where the snapshot folder is stored if the location is other than or in addition to the default location. |
| `description` | | Description of this merge pull subscription. |
| `distributor` | | Name of the Distributor. |
| `distributor_login` | | Login ID used at the Distributor for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication |
| `distributor_password` | | Password (encrypted) used at the Distributor for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication. |
| `distributor_security_mode` | `1` | Use Windows Authentication when connecting to the Distributor. |
| | `0` | Use [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication when connecting to the Distributor. |
| `dynamic_snapshot_location` | | Path to the folder where the snapshot files are saved. |
| `ftp_address` | | Available for backward compatibility only. The network address of the File Transfer Protocol (FTP) service for the Distributor. |
| `ftp_login` | | Available for backward compatibility only. The username used to connect to the FTP service. |
| `ftp_password` | | Available for backward compatibility only. The user password used to connect to the FTP service. |
| `ftp_port` | | Available for backward compatibility only. The port number of the FTP service for the Distributor. |
| `hostname` | | Specifies the value for `HOST_NAME()` when this function is used in the `WHERE` clause of a join filter or logical record relationship. |
| `internet_login` | | Login that the Merge Agent uses when connecting to the Web server that is hosting Web synchronization using Basic Authentication. |
| `internet_password` | | Password for the login that the Merge Agent uses when connecting to the Web server that is hosting Web synchronization using Basic Authentication. |
| `internet_security_mode` | `1` | Use Windows Authentication when connecting to the Web server that is hosting Web synchronization. |
| | `0` | Use Basic Authentication when connecting to the Web server that is hosting Web synchronization. |
| `internet_timeout` | | Length of time, in seconds, before a Web synchronization request expires. |
| `internet_url` | | URL that represents the location of the replication listener for Web synchronization. |
| `merge_job_login` | | Login for the Windows account under which the agent runs. |
| `merge_job_password` | | Password for the Windows account under which the agent runs. |
| `priority` | | Available for backward compatibility only; run [sp_changemergesubscription](sp-changemergesubscription-transact-sql.md) at the Publisher instead to modify the priority of a subscription. |
| `publisher_login` | | Login ID used at the Publisher for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication. |
| `publisher_password` | | Password (encrypted) used at the Publisher for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication. |
| `publisher_security_mode` | `0` | Use [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication when connecting to the Publisher. |
| | `1` | Use Windows Authentication when connecting to the Publisher. |
| | `2` | Synchronization triggers use a static `sysservers` entry to do remote procedure call (RPC), and the Publisher must be defined in the `sysservers` table as a remote server or linked server. |
| `sync_type` | `automatic` | Schema and initial data for published tables are transferred to the Subscriber first. |
| | `none` | Subscriber already has the schema and initial data for published tables; system tables and data are always transferred. |
| `use_ftp` | `true` | Use FTP instead of the typical protocol, to retrieve snapshots. |
| | `false` | Use the typical protocol to retrieve snapshots. |
| `use_web_sync` | `true` | Subscription can be synchronized over HTTP. |
| | `false` | Subscription can't be synchronized over HTTP. |
| `use_interactive_resolver` | `true` | Interactive resolver is used during reconciliation. |
| | `false` | Interactive resolver isn't used. |
| `working_directory` | | Fully qualified path to the directory where snapshot files are transferred using FTP when that option is specified. |
| `NULL` (default) | | Returns the list of supported values for *@property*. |

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_changemergepullsubscription` is used in merge replication.

The current server and current database are assumed to be the Subscriber and Subscriber database.

After changing an agent login or password, you must stop and restart the agent before the change takes effect.

## Permissions

Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute `sp_changemergepullsubscription`.

## Related content

- [View and Modify Pull Subscription Properties](../replication/view-and-modify-pull-subscription-properties.md)
- [sp_addmergepullsubscription (Transact-SQL)](sp-addmergepullsubscription-transact-sql.md)
- [sp_dropmergepullsubscription (Transact-SQL)](sp-dropmergepullsubscription-transact-sql.md)
- [sp_helpmergepullsubscription (Transact-SQL)](sp-helpmergepullsubscription-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
