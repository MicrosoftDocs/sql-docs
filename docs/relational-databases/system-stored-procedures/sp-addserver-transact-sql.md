---
title: "sp_addserver (Transact-SQL)"
description: sp_addserver defines the name of the local instance of SQL Server.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 01/23/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_addserver"
  - "sp_addserver_TSQL"
helpviewer_keywords:
  - "sp_addserver"
  - "renaming servers"
  - "machine names [SQL Server]"
  - "computer names"
dev_langs:
  - "TSQL"
---
# sp_addserver (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Defines the name of the local instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. When the computer hosting [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is renamed, use `sp_addserver` to inform the instance of the [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)] of the new computer name. This procedure must be executed on all instances of the [!INCLUDE [ssDE](../../includes/ssde-md.md)] hosted on the computer.

The instance name of the [!INCLUDE [ssDE](../../includes/ssde-md.md)] can't be changed. To change the instance name of a named instance, install a new instance with the desired name, detach the database files from old instance, attach the databases to the new instance, and drop the old instance. Alternatively, you can create a client alias name on the client computer, redirecting the connection to different server and instance name or `<server>:<port>` combination without changing the name of the instance on the server computer.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_addserver
    [ @server = ] N'server'
    [ , [ @local = ] 'LOCAL' ]
    [ , [ @duplicate_ok = ] 'duplicate_OK' ]
[ ; ]
```

## Arguments

#### [ @server = ] N'*server*'

The name of the server. Server names must be unique and follow the rules for [!INCLUDE [msCoName](../../includes/msconame-md.md)] Windows computer names, although spaces aren't allowed. *@server* is **sysname**, with no default.

When multiple instances of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] are installed on a computer, an instance operates as if it's on a separate server. Specify a named instance by referring to *@server* as `<servername>\<instancename>`.

#### [ @local = ] 'LOCAL'

Specifies that the server is being added as a local server. *@local* is **varchar(10)**, with a default of `NULL`. Specifying *@local* as `LOCAL` defines *@server* as the name of the local server, and causes the `@@SERVERNAME` function to return the value of *@server*.

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Setup sets this variable to the computer name during installation. By default, the computer name is the way users connect to an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] without requiring additional configuration.

The local definition takes effect only after the [!INCLUDE [ssDE](../../includes/ssde-md.md)] is restarted. Only one local server can be defined in each instance of the [!INCLUDE [ssDE](../../includes/ssde-md.md)].

#### [ @duplicate_ok = ] 'duplicate_OK'

Specifies whether a duplicate server name is allowed. *@duplicate_ok* is **varchar(13)**, with a default of `NULL`. *@duplicate_ok* can only have the value `duplicate_OK` or `NULL`. If *@duplicate_ok* is specified and the server name that is being added already exists, no error is raised. If named parameters aren't used, *@local* must be specified.

## Return code values

`0` (success) or `1` (failure).

## Remarks

To set or clear server options, use `sp_serveroption`.

`sp_addserver` can't be used inside a user-defined transaction.

Using `sp_addserver` to add a remote server is discontinued. Use [sp_addlinkedserver](sp-addlinkedserver-transact-sql.md) instead.

Using `sp_addserver` to change the local server name might cause undesired effects or unsupported configurations when using availability groups or Replication.

## Permissions

Requires membership in the **setupadmin** fixed server role.

## Examples

The following example changes the [!INCLUDE [ssDE](../../includes/ssde-md.md)] entry for the name of the computer hosting [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to `ACCOUNTS`.

```sql
EXEC sp_addserver 'ACCOUNTS', 'local';
```

## Related content

- [Rename a computer that hosts a stand-alone instance of SQL Server](../../database-engine/install-windows/rename-a-computer-that-hosts-a-stand-alone-instance-of-sql-server.md)
- [sp_addlinkedserver (Transact-SQL)](sp-addlinkedserver-transact-sql.md)
- [sp_dropserver (Transact-SQL)](sp-dropserver-transact-sql.md)
- [sp_helpserver (Transact-SQL)](sp-helpserver-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
- [Security stored procedures (Transact-SQL)](security-stored-procedures-transact-sql.md)
- [Connect to the Database Engine](../../sql-server/connect-to-database-engine.md)
