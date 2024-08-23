---
title: "sp_addlinkedsrvlogin (Transact-SQL)"
description: Creates or updates a mapping between a login on the local instance of SQL Server and a security account on a remote server.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/22/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_addlinkedsrvlogin_TSQL"
  - "sp_addlinkedsrvlogin"
helpviewer_keywords:
  - "sp_addlinkedsrvlogin"
dev_langs:
  - "TSQL"
---
# sp_addlinkedsrvlogin (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Creates or updates a mapping between a login on the local instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] and a security account on a remote server.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_addlinkedsrvlogin
    [ @rmtsrvname = ] N'rmtsrvname'
    [ , [ @useself = ] 'useself' ]
    [ , [ @locallogin = ] N'locallogin' ]
    [ , [ @rmtuser = ] N'rmtuser' ]
    [ , [ @rmtpassword = ] N'rmtpassword' ]
[ ; ]
```

## Arguments

#### [ @rmtsrvname = ] N'*rmtsrvname*'

The name of a linked server that the login mapping applies to. *@rmtsrvname* is **sysname**, with no default.

#### [ @useself = ] '*useself*'

Determines whether to connect to *rmtsrvname* by impersonating local logins or explicitly submitting a login and password. *@useself* is **varchar(8)**, with a default of `true`.

- A value of `true` specifies that logins use their own credentials to connect to *@rmtsrvname*, with the *@rmtuser* and *@rmtpassword* arguments being ignored.
- `false` specifies that the *@rmtuser* and *@rmtpassword* arguments are used to connect to *@rmtsrvname* for the specified *@locallogin*.

If *@rmtuser* and *@rmtpassword* are set to `NULL`, no login or password is used to connect to the linked server.

#### [ @locallogin = ] N'*locallogin*'

A login on the local server. *@locallogin* is **sysname**, with a default of `NULL`. `NULL` specifies that this entry applies to all local logins that connect to *@rmtsrvname*. If not `NULL`, *@locallogin* can be a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login or a Windows account. The Windows account must have access to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] directly, or through membership in a Windows group.

#### [ @rmtuser = ] N'*rmtuser*'

The remote login used to connect to *@rmtsrvname* when @useself is `false`. *@rmtuser* is **sysname**, with a default of `NULL`. When the remote server is an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] that doesn't use Windows Authentication, *@rmtuser* is a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login.

#### [ @rmtpassword = ] N'*rmtpassword*'

The password associated with *@rmtuser*. *@rmtpassword* is **sysname**, with a default of `NULL`.

## Return code values

`0` (success) or `1` (failure).

## Remarks

When a user logs on to the local server and executes a distributed query that accesses a table on the linked server, the local server must log on to the linked server on behalf of the user to access that table. Use `sp_addlinkedsrvlogin` to specify the credentials that the local server uses to sign into the linked server.

> [!NOTE]  
> To create the best query plans when you're using a table on a linked server, the query processor must have data distribution statistics from the linked server. Users that have limited permissions on any columns of the table might not have sufficient permissions to obtain all the useful statistics, and might receive a less efficient query plan and experience poor performance. If the linked server is an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], to obtain all available statistics, the user must own the table or be a member of the **sysadmin** fixed server role, the **db_owner** fixed database role, or the **db_ddladmin** fixed database role on the linked server. [!INCLUDE [sssql11sp1-md](../../includes/sssql11sp1-md.md)] modifies the permission restrictions for obtaining statistics and allows users with SELECT permission to access statistics available through DBCC SHOW_STATISTICS. For more information, see the Permissions section of [DBCC SHOW_STATISTICS](../../t-sql/database-console-commands/dbcc-show-statistics-transact-sql.md).

A default mapping between all logins on the local server and remote logins on the linked server is automatically created by executing `sp_addlinkedserver`. The default mapping states that [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] uses the user credentials of the local login when connecting to the linked server on behalf of the login. This is equivalent to executing `sp_addlinkedsrvlogin` with @useself set to `true` for the linked server, without specifying a local user name. Use `sp_addlinkedsrvlogin` only to change the default mapping or to add new mappings for specific local logins. To delete the default mapping or any other mapping, use `sp_droplinkedsrvlogin`.

Instead of having to use `sp_addlinkedsrvlogin` to create a predetermined login mapping, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] can automatically use the Windows security credentials (Windows login name and password) of a user issuing the query to connect to a linked server when all the following conditions exist:

- A user is connected to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] by using Windows Authentication Mode.

- Security account delegation is available on the client and sending server.

- The provider supports Windows Authentication Mode; for example, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] running on Windows.

> [!NOTE]  
> Delegation doesn't have to be enabled for single-hop scenarios, but it's required for multiple-hop scenarios.

After the authentication has been performed by the linked server by using the mappings that are defined by executing `sp_addlinkedsrvlogin` on the local instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], the permissions on individual objects in the remote database are determined by the linked server, not the local server.

`sp_addlinkedsrvlogin` can't be executed from within a user-defined transaction.

## Permissions

Requires ALTER ANY LOGIN permission on the server.

## Examples

### A. Connect all local logins to the linked server by using their own user credentials

The following example creates a mapping to make sure that all logins to the local server connect through to the linked server `Accounts` by using their own user credentials.

```sql
EXEC sp_addlinkedsrvlogin 'Accounts';
```

Or

```sql
EXEC sp_addlinkedsrvlogin 'Accounts', 'true';
```

> [!NOTE]  
> If there are explicit mappings created for individual logins, they take precedence over any global mappings that might exist for that linked server.

### B. Connect a specific login to the linked server by using different user credentials

The following example creates a mapping to make sure that the Windows user `Domain\Mary` connects through to the linked server `Accounts` by using the login `MaryP` and password `d89q3w4u`.

```sql
EXEC sp_addlinkedsrvlogin 'Accounts', 'false', 'Domain\Mary', 'MaryP', 'd89q3w4u';
```

> [!CAUTION]  
> This example doesn't use Windows Authentication. Passwords will be transmitted unencrypted. Passwords might be visible in data source definitions and scripts that are saved to disk, in backups, and in log files. Never use an administrator password in this kind of connection. Consult your network administrator for security guidance specific to your environment.

## Related content

- [Linked Servers Catalog Views (Transact-SQL)](../system-catalog-views/linked-servers-catalog-views-transact-sql.md)
- [sp_addlinkedserver (Transact-SQL)](sp-addlinkedserver-transact-sql.md)
- [sp_droplinkedsrvlogin (Transact-SQL)](sp-droplinkedsrvlogin-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
