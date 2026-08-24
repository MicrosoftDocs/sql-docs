---
title: "Server Configuration: cross db ownership chaining"
description: "Learn how to use the cross db ownership chaining option in SQL Server. View considerations for turning cross-database ownership chaining on and off."
author: rwestMSFT
ms.author: randolphwest
ms.date: 11/19/2025
ms.service: sql
ms.subservice: configuration
ms.topic: how-to
helpviewer_keywords:
  - "cross-database ownership chaining"
  - "cross db ownership chaining option"
  - "chaining ownership"
---
# Server configuration: cross db ownership chaining

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Use the `cross db ownership chaining` option to configure cross-database ownership chaining for an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

This server option allows you to control cross-database ownership chaining at the database level or to allow cross-database ownership chaining for all databases:

- When `cross db ownership chaining` is off (`0`) for the instance, cross-database ownership chaining is disabled for all databases.

- When `cross db ownership chaining` is on (`1`) for the instance, cross-database ownership chaining is on for all databases.

- You can set cross-database ownership chaining for individual databases using the `SET` clause of the `ALTER DATABASE` statement. If you're creating a new database, you can set the cross-database ownership chaining option for the new database using the `CREATE DATABASE` statement.

  Setting `cross db ownership chaining` to `1` isn't recommended unless all of the databases hosted by the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] must participate in cross-database ownership chaining and you're aware of the security implications of this setting.

## Check status of cross-database ownership chaining

To determine the current status of cross-database ownership chaining, execute the following query:

```sql
SELECT is_db_chaining_on,
       name
FROM sys.databases;
```

A result of `1` indicates that cross-database ownership chaining is enabled.

## Control cross-database ownership chaining

Before turning cross-database ownership chaining on or off:

- You must be a member of the **sysadmin** fixed server role to turn cross-database ownership chaining on or off.

- Before turning off cross-database ownership chaining on a production server, fully test all applications, including third-party applications, to ensure that the changes don't affect application functionality.

- You can change the `cross db ownership chaining` option while the server is running if you specify `RECONFIGURE` with `sp_configure`.

- If you have databases that require cross-database ownership chaining, the recommended practice is to turn off the `cross db ownership chaining` option for the instance using `sp_configure`; then, turn on cross-database ownership chaining for individual databases that require it with the [ALTER DATABASE](../../t-sql/statements/alter-database-transact-sql.md) statement.

## Security risk

Enabling cross-database ownership chaining in [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] introduces a potential security vulnerability. When this feature is active, a local database user with elevated privileges can exploit ownership chaining to escalate permissions and potentially gain **sysadmin** access.

You should avoid enabling cross-database ownership chaining at the instance level, and restrict its use to trusted, related databases only.

## Related content

- [ALTER DATABASE (Transact-SQL)](../../t-sql/statements/alter-database-transact-sql.md)
- [CREATE DATABASE](../../t-sql/statements/create-database-transact-sql.md)
- [Server configuration options](server-configuration-options-sql-server.md)
- [sys.sp_configure (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)
- [RECONFIGURE (Transact-SQL)](../../t-sql/language-elements/reconfigure-transact-sql.md)
