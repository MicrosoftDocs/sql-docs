---
title: "sp_helplogins (Transact-SQL)"
description: sp_helplogins provides information about logins and the users associated with them in each database.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/15/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_helplogins_TSQL"
  - "sp_helplogins"
helpviewer_keywords:
  - "sp_helplogins"
dev_langs:
  - "TSQL"
---
# sp_helplogins (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Provides information about logins and the users associated with them in each database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_helplogins [ [ @LoginNamePattern = ] N'LoginNamePattern' ]
[ ; ]
```

## Arguments

#### [ @LoginNamePattern = ] N'*LoginNamePattern*'

*@LoginNamePattern* is **sysname**, with a default of `NULL`.

A login name. *@LoginNamePattern* is **sysname**, with a default of `NULL`. *@LoginNamePattern* must exist if specified. If *@LoginNamePattern* isn't specified, information about all logins is returned.

## Return code values

`0` (success) or `1` (failure).

## Result set

The first report contains information about each login specified, as shown in the following table.

| Column name | Data type | Description |
| --- | --- | --- |
| `LoginName` | **sysname** | Login name. |
| `SID` | **varbinary(85)** | Login security identifier (SID). |
| `DefDBName` | **sysname** | Default database that `LoginName` uses when connecting to an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. |
| `DefLangName` | **sysname** | Default language used by `LoginName`. |
| `Auser` | **char(5)** | `Yes` = `LoginName` has an associated user name in a database.<br /><br />`No` = `LoginName` doesn't have an associated user name. |
| `ARemote` | **char(7)** | `Yes` = `LoginName` has an associated remote login.<br /><br />`No` = `LoginName` doesn't have an associated login. |

The second report contains information about users mapped to each login, and the role memberships of the login as shown in the following table.

| Column name | Data type | Description |
| --- | --- | --- |
| `LoginName` | **sysname** | Login name. |
| `DBName` | **sysname** | Default database that `LoginName` uses when connecting to an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. |
| `UserName` | **sysname** | User account that `LoginName` is mapped to in `DBName`, and the roles that `LoginName` is a member of in `DBName`. |
| `UserOrAlias` | **char(8)** | Member of = `UserName` is a role.<br /><br />User = `UserName` is a user account. |

## Remarks

Before removing a login, use `sp_helplogins` to identify user accounts that are mapped to the login.

## Permissions

Requires membership in the **securityadmin** fixed server role.

To identify all user accounts mapped to a given login, `sp_helplogins` must check all databases within the server. Therefore, for each database on the server, at least one of the following conditions must be true:

- The user that is executing `sp_helplogins` has permission to access the database.

- The **guest** user account is enabled in the database.

If `sp_helplogins` can't access a database, `sp_helplogins` will return as much information as it can and display error message 15622.

## Examples

The following example reports information about the login `John`.

```sql
EXEC sp_helplogins 'John';
GO
```

[!INCLUDE [ssresult-md](../../includes/ssresult-md.md)]

```output
LoginName SID                        DefDBName DefLangName AUser ARemote
--------- -------------------------- --------- ----------- ----- -------
John      0x23B348613497D11190C100C  master    us_english  yes   no

LoginName   DBName   UserName   UserOrAlias
---------   ------   --------   -----------
John        pubs     John       User
```

## Related content

- [Security stored procedures (Transact-SQL)](security-stored-procedures-transact-sql.md)
- [sp_helpdb (Transact-SQL)](sp-helpdb-transact-sql.md)
- [sp_helpuser (Transact-SQL)](sp-helpuser-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
