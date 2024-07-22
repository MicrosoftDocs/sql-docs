---
title: "sp_setnetname (Transact-SQL)"
description: sp_setnetname sets the network names in sys.servers to their actual network computer names for remote instances of SQL Server.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 04/08/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_setnetname"
  - "sp_setnetname_TSQL"
helpviewer_keywords:
  - "sp_setnetname"
dev_langs:
  - "TSQL"
---
# sp_setnetname (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Sets the network names in `sys.servers` to their actual network computer names for remote instances of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. This procedure can be used to enable execution of remote stored procedure calls to computers that have network names containing [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] identifiers that aren't valid.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_setnetname
    [ @server = ] N'server'
    , [ @netname = ] N'netname'
[ ; ]
```

## Arguments

#### [ @server = ] N'*server*'

The name of the remote server as referenced in user-coded remote stored procedure call syntax. *@server* is **sysname**, with no default. Exactly one row in `sys.servers` must already exist to use this *@server*.

#### [ @netname = ] N'*netname*'

The network name of the computer to which remote stored procedure calls are made. *@netname* is **sysname**, with no default.

This name must match the [!INCLUDE [msCoName](../../includes/msconame-md.md)] Windows computer name, and the name can include characters that aren't allowed in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] identifiers.

## Return code values

`0` (success) or `1` (failure).

## Result set

None.

## Remarks

Some remote stored procedure calls to Windows computers can encounter problems if the computer name contains identifiers that aren't valid.

Because linked servers and remote servers reside in the same namespace, they can't have the same name. However, you can define both a linked server and a remote server against a specified server by assigning different names, and by using `sp_setnetname` to set the network name of one of them to the network name of the underlying server.

In this example, Assume `sqlserv2` is the actual name of the SQL Server instance.

```sql
EXEC sp_addlinkedserver 'sqlserv2';
GO
EXEC sp_addserver 'rpcserv2';
GO
EXEC sp_setnetname 'rpcserv2', 'sqlserv2';
```

> [!NOTE]  
> Using `sp_setnetname` to point a linked server back to the local server isn't supported. Servers that are referenced in this manner can't participate in a distributed transaction.

## Permissions

Requires membership in the **sysadmin** and **setupadmin** fixed server roles.

## Examples

The following example shows a typical administrative sequence used on [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to issue the remote stored procedure call.

```sql
USE master;
GO
EXEC sp_addserver 'Win_1';
EXEC sp_setnetname 'Win_1', 'Win-1';
EXEC Win_1.master.dbo.sp_who;
```

## Related content

- [Database Engine stored procedures (Transact-SQL)](database-engine-stored-procedures-transact-sql.md)
- [sp_addlinkedserver (Transact-SQL)](sp-addlinkedserver-transact-sql.md)
- [sp_addserver (Transact-SQL)](sp-addserver-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
