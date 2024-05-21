---
title: "sp_helpdistpublisher (Transact-SQL)"
description: sp_helpdistpublisher returns properties of the Publishers using a Distributor.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/15/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_helpdistpublisher_TSQL"
  - "sp_helpdistpublisher"
helpviewer_keywords:
  - "sp_helpdistpublisher"
dev_langs:
  - "TSQL"
---
# sp_helpdistpublisher (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Returns properties of the Publishers using a Distributor. This stored procedure is executed at the Distributor on any database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_helpdistpublisher
    [ [ @publisher = ] N'publisher' ]
    [ , [ @check_user = ] check_user ]
[ ; ]
```

## Arguments

#### [ @publisher = ] N'*publisher*'

Specifies the Publisher for which properties are returned. *@publisher* is **sysname**, with a default of `%`.

#### [ @check_user = ] *check_user*

[!INCLUDE [ssInternalOnly](../../includes/ssinternalonly-md.md)]

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `name` | **sysname** | Name of Publisher. |
| `distribution_db` | **sysname** | Distribution database for the specified Publisher. |
| `security_mode` | **int** | Security mode used by replication agents to connect to the Publisher for queued updating subscriptions, or with a non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Publisher.<br /><br />`0` = [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication<br />`1` = Windows Authentication |
| `login` | **sysname** | Login name used by replication agents to connect to the Publisher for queued updating subscriptions, or with a non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Publisher. |
| `password` | **nvarchar(524)** | Password returned (in simple encrypted form). Password is `NULL` for users other than **sysadmin**. |
| `active` | **bit** | Whether a remote Publisher is using the local server as a Distributor:<br /><br />`0` = No<br />`1` = Yes |
| `working_directory` | **nvarchar(255)** | Name of the working directory. |
| `trusted` | **bit** | If the password is required when the Publisher connects to the Distributor. For [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)] and later versions, this column should always return `0`, which means that the password is required. |
| `thirdparty_flag` | **bit** | Whether the publication is enabled by [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or by a third-party application:<br /><br />`0` = [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], Oracle, or Oracle Gateway Publisher.<br />`1` = Publisher is integrated with [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] using a third-party application. |
| `publisher_type` | **sysname** | Type of Publisher; can be one of the following values:<br /><br />`MSSQLSERVER`<br />`ORACLE`<br />`ORACLE GATEWAY` |
| `publisher_data_source` | **nvarchar(4000)** | Name of the OLE DB data source on the Publisher. |
| `storage_connection_string` | **nvarchar(4000)** | Storage access key for working directory when distributor or publisher in Azure SQL Database. |

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_helpdistpublisher` is used in all types of replication.

`sp_helpdistpublisher` doesn't display the publisher login or password in the result set for non-**sysadmin** logins.

## Permissions

Members of the **sysadmin** fixed server role might execute `sp_helpdistpublisher` for any Publisher using the local server as a Distributor. Members of the **db_owner** fixed database role or the **replmonitor** role in a distribution database might execute `sp_helpdistpublisher` for any Publisher using that distribution database. Users in the publication access list for a publication at the specified *@publisher* might execute `sp_helpdistpublisher`. If *@publisher* isn't specified, information is returned for all Publishers that the user has rights to access.

## Related content

- [View and Modify Distributor and Publisher Properties](../replication/view-and-modify-distributor-and-publisher-properties.md)
- [sp_adddistpublisher (Transact-SQL)](sp-adddistpublisher-transact-sql.md)
- [sp_changedistpublisher (Transact-SQL)](sp-changedistpublisher-transact-sql.md)
- [sp_dropdistpublisher (Transact-SQL)](sp-dropdistpublisher-transact-sql.md)
