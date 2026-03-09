---
title: "Troubleshoot Orphaned Users"
description: Orphaned users occur when a database user login no longer exists in the `master` database. This article discusses how to identify and resolve orphaned users.
author: MashaMSFT
ms.author: mathoma
ms.date: 02/23/2026
ms.service: sql
ms.subservice: high-availability
ms.topic: troubleshooting
helpviewer_keywords:
  - "orphaned users [SQL Server]"
  - "logins [SQL Server], orphaned users"
  - "troubleshooting [SQL Server], user accounts"
  - "user accounts [SQL Server], orphaned users"
  - "failover [SQL Server], managing metadata"
  - "database mirroring [SQL Server], metadata"
  - "users [SQL Server], orphaned"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016"
---
# Troubleshoot orphaned users (SQL Server)

[!INCLUDE [SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

Users are orphaned in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] when a database user is based on a login in the `master` database but the login no longer exists in `master`. This can occur when the login is deleted or when the database is moved to another server on which the login doesn't exist. This article describes how to find orphaned users and remap them to logins.

> [!NOTE]
> Reduce the possibility of orphaned users by using contained database users for databases that might be moved. For more information, see [Make your database portable by using contained databases](../../relational-databases/security/contained-database-users-making-your-database-portable.md).

> [!IMPORTANT]
> The `sp_change_users_login` stored procedure was previously used to fix orphaned users but is now deprecated. Use `ALTER USER ... WITH LOGIN` instead, as described in the [Resolve an orphaned user](#resolve-an-orphaned-user) section. For more information, see [sp_change_users_login (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-change-users-login-transact-sql.md).

## Background

 For connections to a database on an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] that use a security principal (database user identity) based on a login, the principal must have a valid login in the `master` database. This login is used in the authentication process that verifies the principal's identity and determines whether the principal is allowed to connect to the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. The [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] logins on a server instance are visible in the [sys.server_principals](../../relational-databases/system-catalog-views/sys-server-principals-transact-sql.md) catalog view and the [sys.sql_logins](../../relational-databases/system-catalog-views/sys-sql-logins-transact-sql.md) compatibility view.

 [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] logins access individual databases as a "database user" that's mapped to the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login. There are three exceptions to this rule:

-   Contained database users

     Contained database users authenticate at the user-database level and aren't associated with logins. This model is recommended because the databases are more portable, and contained database users can't become orphaned. However, they must be re-created for each database. This model might be impractical in an environment that has many databases.

-   The **guest** account

     When enabled in a database, this account permits [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] logins that aren't mapped to a database user to access the database as the **guest** user. The **guest** account is disabled by default.

-   Microsoft Windows group memberships

     A [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login created from a Windows user can access a database if the Windows user is a member of a Windows group that's also a user in the database.

 Information about the mapping of a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login to a database user is stored in the database. It includes the name of the database user and the security identifier (`SID`) of the corresponding [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login. The permissions of this database user are applied for authorization in the database.

 A database user (based on a login) for which the corresponding [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login is undefined or is incorrectly defined on a server instance can't sign in to the instance. Such a user is an *orphaned user* of the database on that server instance. Orphaning can happen if the database user is mapped to a login `SID` that's not present in the `master` database. A database user can become orphaned after a database is restored or attached to a different instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] where the login was never created. A database user can also become orphaned if the corresponding [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login is dropped. Even if the login is re-created, it has a different `SID`, so the database user is still orphaned.

## Detect orphaned users

**For [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] and PDW**

To detect orphaned users in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] based on missing [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] authentication logins, run the following statement in the user database:

```sql
SELECT dp.type_desc, dp.sid, dp.name AS user_name
FROM sys.database_principals AS dp
LEFT JOIN sys.server_principals AS sp
    ON dp.sid = sp.sid
WHERE sp.sid IS NULL
    AND dp.authentication_type_desc = 'INSTANCE';
```

 The output lists the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] authentication users and corresponding SIDs in the current database that aren't linked to any [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] login.

**For Azure SQL Database and Azure Synapse Analytics**

The [sys.server_principals](../../relational-databases/system-catalog-views/sys-server-principals-transact-sql.md) table isn't available in SQL Database or Azure Synapse Analytics. To identify orphaned users in those environments, complete these steps:

1. Connect to the `master` database and select the SIDs for the logins by using the following query:

    ```sql
    SELECT sid
    FROM sys.sql_logins
    WHERE type = 'S';
    ```

1. Connect to the user database and review the SIDs of the users in the `sys.database_principals` table by using the following query:

    ```sql
    SELECT name, sid, principal_id
    FROM sys.database_principals
    WHERE type = 'S'
      AND name NOT IN ('guest', 'INFORMATION_SCHEMA', 'sys')
      AND authentication_type_desc = 'INSTANCE';
    ```

1. Compare the two lists to determine whether there are user SIDs in the user database `sys.database_principals` table that aren't matched by login SIDs in the `master` database `sql_logins` table.

## Resolve an orphaned user

In the `master` database, use the [CREATE LOGIN](../../t-sql/statements/create-login-transact-sql.md) statement with the `SID` option to re-create a missing login. Provide the `SID` of the database user that you obtained in the previous section.

```sql
CREATE LOGIN <login_name>
WITH PASSWORD = '<use_a_strong_password_here>',
SID = <SID>;
```

 To map an orphaned user to a login that already exists in `master`, run the [ALTER USER](../../t-sql/statements/alter-user-transact-sql.md) statement in the user database, specifying the login name:

```sql
ALTER USER <user_name> WITH Login = <login_name>;
```

 When you re-create a missing login, the user can access the database by using the password provided. The user can then change the password of the login account by using the `ALTER LOGIN` statement:

```sql
ALTER LOGIN <login_name> WITH PASSWORD = '<enterStrongPasswordHere>';
```

> [!IMPORTANT]
> Any login can change its own password. Only logins with the `ALTER ANY LOGIN` permission can change the password of another user's login. However, only members of the **sysadmin** role can modify passwords of **sysadmin** role members.

## Deprecated methods

In earlier versions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], the `sp_change_users_login` stored procedure was used to resolve orphaned users. This procedure is deprecated and might be removed in a future version. Use `ALTER USER` instead.

The following table shows the equivalent modern syntax for common `sp_change_users_login` operations:

| Deprecated syntax | Recommended replacement |
|---|---|
| `EXEC sp_change_users_login 'Report'` | `SELECT dp.name FROM sys.database_principals dp LEFT JOIN sys.server_principals sp ON dp.sid = sp.sid WHERE sp.sid IS NULL AND dp.authentication_type_desc = 'INSTANCE'` |
| `EXEC sp_change_users_login 'Auto_Fix', '<user>'` | `ALTER USER <user> WITH LOGIN = <user>` |
| `EXEC sp_change_users_login 'Update_One', '<user>', '<login>'` | `ALTER USER <user> WITH LOGIN = <login>` |

For more information, see [sp_change_users_login (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-change-users-login-transact-sql.md).

## Related content

- [CREATE LOGIN (Transact-SQL)](../../t-sql/statements/create-login-transact-sql.md)
- [ALTER USER (Transact-SQL)](../../t-sql/statements/alter-user-transact-sql.md)
- [CREATE USER (Transact-SQL)](../../t-sql/statements/create-user-transact-sql.md)
- [sys.database_principals (Transact-SQL)](../../relational-databases/system-catalog-views/sys-database-principals-transact-sql.md)
- [sys.server_principals (Transact-SQL)](../../relational-databases/system-catalog-views/sys-server-principals-transact-sql.md)
- [sys.sql_logins (Transact-SQL)](../../relational-databases/system-catalog-views/sys-sql-logins-transact-sql.md)
- [sp_change_users_login (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-change-users-login-transact-sql.md)
- [Make your database portable by using contained databases](../../relational-databases/security/contained-database-users-making-your-database-portable.md)
