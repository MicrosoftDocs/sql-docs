---
title: "sp_dropremotelogin (Transact-SQL)"
description: Removes a remote login mapped to a local login used to execute remote stored procedures against the local server running SQL Server.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 11/28/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_dropremotelogin"
  - "sp_dropremotelogin_TSQL"
helpviewer_keywords:
  - "sp_dropremotelogin"
dev_langs:
  - "TSQL"
---
# sp_dropremotelogin (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Removes a remote login mapped to a local login used to execute remote stored procedures against the local server running [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

> [!IMPORTANT]  
> [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use linked servers and linked-server stored procedures instead.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_dropremotelogin
    [ @remotename = ] N'@remotename'
    [ , [ @loginame = ] N'loginame' ]
    [ , [ @remotename = ] N'remotename' ]
[ ; ]
```

## Arguments

#### [ @remotename = ] N'*@remotename*'

The name of the remote server mapped to the remote login that is to be removed. *@remotename* is **sysname**, with no default. *@remotename* must already exist.

#### [ @loginame = ] N'*loginame*'

The optional login name on the local server that is associated with the remote server. *@loginame* is **sysname**, with a default of `NULL`. *@loginame* must already exist if specified.

#### [ @remotename = ] N'*remotename*'

The optional name of the remote login that is mapped to *@loginame* when logging in from the remote server. *@remotename* is **sysname**, with a default of `NULL`.

## Return code values

`0` (success) or `1` (failure).

## Remarks

If only *@remotename* is specified, all remote logins for that remote server are removed from the local server. If *@loginame* is also specified, all remote logins from *@remotename* mapped to that specific local login are removed from the local server. If *remote_name* is also specified, only the remote login for that remote user from *@remotename* is removed from the local server.

To add local server users, use `sp_addlogin`. To remove local server users, use `sp_droplogin`.

Remote logins are required only when you use earlier versions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] 7.0 and later versions use linked server logins instead. Use `sp_addlinkedsrvlogin` and `sp_droplinkedsrvlogin` to add and remove linked server logins.

`sp_dropremotelogin` can't be executed within a user-defined transaction.

## Permissions

Requires membership in the **sysadmin** or **securityadmin** fixed server roles.

## Examples

### A. Drop all remote logins for a remote server

The following example removes the entry for the remote server `ACCOUNTS`, and, therefore, removes all mappings between logins on the local server and remote logins on the remote server.

```sql
EXEC sp_dropremotelogin 'ACCOUNTS';
```

### B. Drop a login mapping

The following example removes the entry for mapping remote logins from the remote server `ACCOUNTS` to the local login `Albert`.

```sql
EXEC sp_dropremotelogin 'ACCOUNTS', 'Albert';
```

### C. Drop a remote user

The following example removes the login for the remote login `Chris` on the remote server `ACCOUNTS` that was mapped to the local login `salesmgr`.

```sql
EXEC sp_dropremotelogin 'ACCOUNTS', 'salesmgr', 'Chris';
```

## Related content

- [Security stored procedures (Transact-SQL)](security-stored-procedures-transact-sql.md)
- [sp_addlinkedsrvlogin (Transact-SQL)](sp-addlinkedsrvlogin-transact-sql.md)
- [sp_addlogin (Transact-SQL)](sp-addlogin-transact-sql.md)
- [sp_addremotelogin (Transact-SQL)](sp-addremotelogin-transact-sql.md)
- [sp_addserver (Transact-SQL)](sp-addserver-transact-sql.md)
- [sp_droplinkedsrvlogin (Transact-SQL)](sp-droplinkedsrvlogin-transact-sql.md)
- [sp_droplogin (Transact-SQL)](sp-droplogin-transact-sql.md)
- [sp_helpremotelogin (Transact-SQL)](sp-helpremotelogin-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
