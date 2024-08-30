---
title: "xp_logininfo (Transact-SQL)"
description: "Returns information about Windows users and Windows groups."
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 11/02/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "xp_logininfo_TSQL"
  - "xp_logininfo"
helpviewer_keywords:
  - "xp_logininfo"
dev_langs:
  - "TSQL"
---
# xp_logininfo (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns information about Windows users and Windows groups.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
xp_logininfo [ [ @acctname = ] 'account_name' ]
     [ , [ @option = ] 'all' | 'members' ]
     [ , [ @privilege = ] 'variable_name' OUTPUT ]
```

## Arguments

#### [ @acctname = ] '*@acctname*'

The name of a Windows user or group granted access to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. *@acctname* is **sysname**, with a default of `NULL`. If *@acctname* isn't specified, all Windows groups and Windows users that have been explicitly granted login permission are reported. *@acctname* must be fully qualified. For example, `CONTOSO\macraes`, or `BUILTIN\Administrators`.

#### [ @option = ] 'all' | 'members' ]

Specifies whether to report information about all permission paths for the account, or to report information about the members of the Windows group. *@option* is **varchar(10)**, with a default of `NULL`. Unless `all` is specified, only the first permission path is displayed.

#### [ @privilege = ] '*variable_name*' OUTPUT ]

An output parameter that returns the privilege level of the specified Windows account. *@privilege* is **varchar(10)**, with a default of `Not wanted`. The privilege level returned is **user**, **admin**, or **null**.

When `OUTPUT` is specified, this option puts *@privilege* in the output parameter.

## Return code values

`0` (success) or `1` (failure).

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| **account name** | **sysname** | Fully qualified Windows account name. |
| **type** | **char(8)** | Type of Windows account. Valid values are `user` or `group`. |
| **privilege** | **char(9)** | Access privilege for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. Valid values are `admin`, `user`, or `NULL`. |
| **mapped login name** | **sysname** | For user accounts that have user privilege, **mapped login name** shows the mapped login name that [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] tries to use when logging in with this account by using the mapped rules with the domain name added before it. |
| **permission path** | **sysname** | Group membership that allowed the account access. |

## Remarks

If *@acctname* is specified, `xp_logininfo` reports the highest privilege level of the specified Windows user or group. If a Windows user has access as both a system administrator and as a domain user, it's reported as a system administrator. If the user is a member of multiple Windows groups of equal privilege level, only the group that was first granted access to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is reported.

If *@acctname* is a valid Windows user or group that isn't associated with a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login, an empty result set is returned. If *@acctname* can't be identified as a valid Windows user or group, an error message is returned.

If *@acctname* and `all` are specified, all permission paths for the Windows user or group are returned. If *@acctname* is a member of multiple groups, all of which have been granted access to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], multiple rows are returned. The `admin` privilege rows are returned before the `user` privilege rows, and within a privilege level, rows are returned in the order in which the corresponding [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] logins were created.

If *@acctname* and `members` are specified, a list of the next-level members of the group is returned. If *@acctname* is a local group, the listing can include local users, domain users, and groups. If *@acctname* is a domain account, the list is made up of domain users. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] must connect to the domain controller to retrieve group membership information. If the server can't contact the domain controller, no information is returned.

`xp_logininfo` only returns information from Active Directory global groups, not universal groups.

## Permissions

Requires membership in the **sysadmin** fixed server role or membership in the **public** fixed database role in the `master` database with EXECUTE permission granted.

## Examples

The following example displays information about the `BUILTIN\Administrators` Windows group.

```sql
EXEC xp_logininfo 'BUILTIN\Administrators';
```

## Related content

- [sp_denylogin (Transact-SQL)](sp-denylogin-transact-sql.md)
- [sp_grantlogin (Transact-SQL)](sp-grantlogin-transact-sql.md)
- [sp_revokelogin (Transact-SQL)](sp-revokelogin-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [General extended stored procedures (Transact-SQL)](general-extended-stored-procedures-transact-sql.md)
