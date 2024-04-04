---
title: "sp_addremotelogin (Transact-SQL)"
description: sp_addremotelogin adds a new remote login ID on the local server.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 01/23/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_addremotelogin_TSQL"
  - "sp_addremotelogin"
helpviewer_keywords:
  - "sp_addremotelogin"
dev_langs:
  - "TSQL"
---
# sp_addremotelogin (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Adds a new remote login ID on the local server. This enables remote servers to connect and execute remote procedure calls.

> [!IMPORTANT]  
> [!INCLUDE [ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use linked servers and linked server stored procedures instead.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_addremotelogin
    [ @remoteserver = ] N'remoteserver'
    [ , [ @loginame = ] N'loginame' ]
    [ , [ @remotename = ] N'remotename' ]
[ ; ]
```

## Arguments

#### [ @remoteserver = ] N'*remoteserver*'

The name of the remote server that the remote login applies to. *@remoteserver* is **sysname**, with no default. If only *@remoteserver* is specified, all users on *@remoteserver* are mapped to existing logins of the same name on the local server. The server must be known to the local server. This is added by using [sp_addserver](sp-addserver-transact-sql.md). When users on *@remoteserver* connect to the local server that is running [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to execute a remote stored procedure, they connect as the local login that matches their own login on *@remoteserver*. *@remoteserver* is the server that initiates the remote procedure call.

#### [ @loginame = ] N'*loginame*'

The login ID of the user on the local instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. *@loginame* is **sysname**, with a default of `NULL`. *@loginame* must already exist on the local instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. If *@loginame* is specified, all users on *@remoteserver* are mapped to that specific local login. When users on *@remoteserver* connect to the local instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to execute a remote stored procedure, they connect as *@loginame*.

#### [ @remotename = ] N'*remotename*'

The login ID of the user on the remote server. *@remotename* is **sysname**, with a default of `NULL`. *@remotename* must exist on *@remoteserver*. If *@remotename* is specified, the specific user *@remotename* is mapped to *@loginame* on the local server. When *@remotename* on *@remoteserver* connects to the local instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to execute a remote stored procedure, it connects as *@loginame*. The login ID of *@remotename* can be different from the login ID on the remote server, *@loginame*.

## Return code values

`0` (success) or `1` (failure).

## Remarks

To execute distributed queries, use `sp_addlinkedsrvlogin`.

`sp_addremotelogin` can't be used inside a user-defined transaction.

## Permissions

Only members of the **sysadmin** and **securityadmin** fixed server roles can execute `sp_addremotelogin`.

## Examples

### A. Map one to one

The following example maps remote names to local names when the remote server `ACCOUNTS` and local server have the same user logins.

```sql
EXEC sp_addremotelogin 'ACCOUNTS';
```

### B. Map many to one

The following example creates an entry that maps all users from the remote server `ACCOUNTS` to the local login ID `Albert`.

```sql
EXEC sp_addremotelogin 'ACCOUNTS', 'Albert';
```

### C. Use explicit one-to-one mapping

The following example maps a remote login from the remote user `Chris` on the remote server `ACCOUNTS` to the local user `salesmgr`.

```sql
EXEC sp_addremotelogin 'ACCOUNTS', 'salesmgr', 'Chris';
```

## Related content

- [sp_addlinkedsrvlogin (Transact-SQL)](sp-addlinkedsrvlogin-transact-sql.md)
- [sp_addlogin (Transact-SQL)](sp-addlogin-transact-sql.md)
- [sp_addserver (Transact-SQL)](sp-addserver-transact-sql.md)
- [sp_dropremotelogin (Transact-SQL)](sp-dropremotelogin-transact-sql.md)
- [sp_grantlogin (Transact-SQL)](sp-grantlogin-transact-sql.md)
- [sp_helpremotelogin (Transact-SQL)](sp-helpremotelogin-transact-sql.md)
- [sp_helpserver (Transact-SQL)](sp-helpserver-transact-sql.md)
- [sp_remoteoption (Transact-SQL)](sp-remoteoption-transact-sql.md)
- [sp_revokelogin (Transact-SQL)](sp-revokelogin-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
