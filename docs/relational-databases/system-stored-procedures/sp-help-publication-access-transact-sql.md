---
title: "sp_help_publication_access (Transact-SQL)"
description: Returns a list of all granted logins for a publication.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/14/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_help_publication_access"
  - "sp_help_publication_access_TSQL"
helpviewer_keywords:
  - "sp_help_publication_access"
dev_langs:
  - "TSQL"
---
# sp_help_publication_access (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Returns a list of all granted logins for a publication. This stored procedure is executed at the Publisher on the publication database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_help_publication_access
    [ @publication = ] N'publication'
    [ , [ @return_granted = ] return_granted ]
    [ , [ @login = ] N'login' ]
    [ , [ @initial_list = ] initial_list ]
    [ , [ @publisher = ] N'publisher' ]
[ ; ]
```

## Arguments

#### [ @publication = ] N'*publication*'

The name of the publication to access. *@publication* is **sysname**, with no default.

#### [ @return_granted = ] *return_granted*

The login ID. *@return_granted* is **bit**, with a default of `1`.

If `0` is specified and [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication is used, the available logins that appear at the Publisher but not at the Distributor are returned.

If `0` is specified and Windows Authentication is used, the logins that aren't specifically denied access at either the Publisher or Distributor, are returned.

#### [ @login = ] N'*login*'

The standard security login ID. *@login* is **sysname**, with a default of `%`.

#### [ @initial_list = ] *initial_list*

Specifies whether to return all members with publication access or just the members who had access before new members were added to the list. *@initial_list* is **bit**, with a default of `0`.

- `1` returns information for all members of the **sysadmin** fixed server role with valid logins at the Distributor that existed when the publication was created, and the current login.

- `0` returns information for all members of the **sysadmin** fixed server role with valid logins at the Distributor that existed when the publication was created, and all users in the publication access list who don't belong to the **sysadmin** fixed server role.

#### [ @publisher = ] N'*publisher*'

[!INCLUDE [ssinternalonly-md](../../includes/ssinternalonly-md.md)]

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `Loginname` | **nvarchar(256)** | Actual login name. |
| `Isntname` | **int** | `0` = Login isn't a Windows user.<br />`1` = Login is a Windows user. |
| `Isntgroup` | **int** | `0` = Login isn't a Windows group.<br />`1` = Login is a Windows group. |

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_help_publication_access` is used in all types of replication.

When both `Isntname` and `Isntgroup` in the result set are `0`, the login is assumed to be a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login.

## Permissions

Only members of the **sysadmin** fixed server role or the **db_owner** fixed database role can execute `sp_help_publication_access`.

## Related content

- [sp_grant_publication_access (Transact-SQL)](sp-grant-publication-access-transact-sql.md)
- [sp_revoke_publication_access (Transact-SQL)](sp-revoke-publication-access-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
