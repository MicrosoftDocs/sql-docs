---
title: "sp_helpremotelogin (Transact-SQL)"
description: Reports information about remote logins for a particular remote server, or for all remote servers, defined on the local server.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/15/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_helpremotelogin_TSQL"
  - "sp_helpremotelogin"
helpviewer_keywords:
  - "sp_helpremotelogin"
dev_langs:
  - "TSQL"
---
# sp_helpremotelogin (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Reports information about remote logins for a particular remote server, or for all remote servers, defined on the local server.

> [!IMPORTANT]  
> [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use linked servers and linked server stored procedures instead.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_helpremotelogin
    [ [ @remoteserver = ] N'remoteserver' ]
    [ , [ @remotename = ] N'remotename' ]
[ ; ]
```

## Arguments

#### [ @remoteserver = ] N'*remoteserver*'

Specifies the remote server about which the remote login information is returned. *@remoteserver* is **sysname**, with a default of `NULL`. If *@remoteserver* isn't specified, information about all remote servers defined on the local server is returned.

#### [ @remotename = ] N'*remotename*'

A specific remote login on the remote server. *@remotename* is **sysname**, with a default of `NULL`. If *@remotename* isn't specified, information about all remote users defined for *@remoteserver* is returned.

## Return code values

`0` (success) or `1` (failure).

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `server` | **sysname** | Name of a remote server defined on the local server. |
| `local_user_name` | **sysname** | Login on the local server that remote logins from server map to. |
| `remote_user_name` | **sysname** | Login on the remote server that maps to `local_user_name`. |
| `options` | **sysname** | `Trusted` = The remote login doesn't need to supply a password when connecting to the local server from the remote server.<br /><br />`Untrusted` (or blank) = The remote login is prompted for a password when connecting to the local server from the remote server. |

## Remarks

Use `sp_helpserver` to list the names of remote servers defined on the local server.

## Permissions

No permissions are checked.

## Examples

### A. Reporting help on a single server

The following example displays information about all remote users on the remote server `Accounts`.

```sql
EXEC sp_helpremotelogin 'Accounts';
```

### B. Reporting help on all remote users

The following example displays information about all remote users on all remote servers known to the local server.

```sql
EXEC sp_helpremotelogin;
```

## Related content

- [sp_addremotelogin (Transact-SQL)](sp-addremotelogin-transact-sql.md)
- [sp_dropremotelogin (Transact-SQL)](sp-dropremotelogin-transact-sql.md)
- [sp_helpserver (Transact-SQL)](sp-helpserver-transact-sql.md)
- [sp_remoteoption (Transact-SQL)](sp-remoteoption-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
