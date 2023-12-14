---
title: sp_dropserver (Transact-SQL)
description: Removes a server from the list of known remote and linked servers on the local instance of SQL Server.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 11/28/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_dropserver_TSQL"
  - "sp_dropserver"
helpviewer_keywords:
  - "sp_dropserver"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-mi-current || >=sql-server-2016 || >=sql-server-linux-2017"
---
# sp_dropserver (Transact-SQL)

[!INCLUDE [SQL Server - ASDBMI](../../includes/applies-to-version/sql-asdbmi.md)]

Removes a server from the list of known remote and linked servers on the local instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_dropserver
    [ @server = ] N'server'
    [ , [ @droplogins = ] 'droplogins' ]
[ ; ]
```

## Arguments

#### [ @server = ] N'*server*'

The server to be removed. *@server* is **sysname**, with no default. *@server* must exist.

#### [ @droplogins = ] '*droplogins*'

Indicates that related remote and linked server logins for *@server* must also be removed if *@droplogins* is specified. *@droplogins* is **char(10)**, with a default of `NULL`.

## Return code values

`0` (success) or `1` (failure).

## Remarks

An error is returned if you run `sp_dropserver` on a server with associated remote and linked server login entries, or is configured as a replication publisher. To remove all remote and linked server logins for a server when you remove the server, use the *@droplogins* argument.

`sp_dropserver` can't be executed inside a user-defined transaction.

Using `sp_dropserver` to change the local server name can cause unintended effects or unsupported configurations.

## Permissions

Requires `ALTER ANY LINKED SERVER` permission on the server.

## Examples

The following example removes the remote server `ACCOUNTS` and all associated remote logins from the local instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

```sql
EXEC sp_dropserver 'ACCOUNTS', 'droplogins';
```

## Related content

- [Security stored procedures (Transact-SQL)](security-stored-procedures-transact-sql.md)
- [sp_addserver (Transact-SQL)](sp-addserver-transact-sql.md)
- [sp_dropremotelogin (Transact-SQL)](sp-dropremotelogin-transact-sql.md)
- [sp_helpremotelogin (Transact-SQL)](sp-helpremotelogin-transact-sql.md)
- [sp_helpserver (Transact-SQL)](sp-helpserver-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
