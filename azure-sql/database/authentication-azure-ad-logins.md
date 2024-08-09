---
title: Microsoft Entra server principals
titleSuffix: Azure SQL Database & Azure SQL Managed Instance & Azure Synapse Analytics
description: Using Microsoft Entra server principals (logins) in Azure SQL
author: nofield
ms.author: nofield
ms.reviewer: vanto, mathoma
ms.date: 02/15/2024
ms.service: azure-sql
ms.subservice: security
ms.topic: conceptual
monikerRange: "= azuresql || = azuresql-db || = azuresql-mi"
---

# Microsoft Entra server principals

[!INCLUDE[appliesto-sqldb-sqlmi-asa-dedicated-only](../includes/appliesto-sqldb-sqlmi-asa-dedicated-only.md)]

You can now create and utilize server principals from Microsoft Entra ID ([formerly Azure Active Directory](/entra/fundamentals/new-name)), which are logins in the virtual `master` database of a SQL Database. There are several benefits of using Microsoft Entra server principals for SQL Database:

- Support [Azure SQL Database server roles for permission management](security-server-roles.md).
- Support multiple Microsoft Entra users with [special roles for SQL Database](/sql/relational-databases/security/authentication-access/database-level-roles#special-roles-for--and-azure-synapse), such as the `loginmanager` and `dbmanager` roles.
- Functional parity between SQL logins and Microsoft Entra logins.
- Increase functional improvement support, such as utilizing [Microsoft Entra-only authentication](authentication-azure-ad-only-authentication.md). Microsoft Entra-only authentication allows SQL authentication to be disabled, which includes the SQL server admin, SQL logins and users.
- Allows Microsoft Entra principals to support geo-replicas. Microsoft Entra principals can connect to the geo-replica of a user database, with *read-only* and *deny* permissions to the primary server.
- Use Microsoft Entra service principal logins with special roles to fully automate user and database creation and maintenance with Microsoft Entra applications.

For more information on Microsoft Entra authentication in Azure SQL, see [Use Microsoft Entra authentication](authentication-aad-overview.md).

> [!NOTE]
> Microsoft Entra server principals (logins) are currently in public preview for Azure SQL Database and Azure Synapse Analytics. Microsoft Entra logins is generally available for Azure SQL Managed Instance and SQL Server 2022.

## Permissions

The following permissions are required to utilize or create Microsoft Entra logins in the virtual `master` database.

- Microsoft Entra admin permission or membership in the `loginmanager` server role. The first Microsoft Entra login can only be created by the Microsoft Entra admin.
- Must be a member of Microsoft Entra ID within the same directory used for Azure SQL Database.

By default, newly created Microsoft Entra logins in the `master` database are granted the **VIEW ANY DATABASE** permission. 

<a name='azure-ad-logins-syntax'></a>

## Microsoft Entra principals syntax

Use the following syntax to create and manage Microsoft Entra server and database principals.

### Create login

This syntax creates a server-level login based on a Microsoft Entra identity. Only the Microsoft Entra admin can execute this command in the virtual `master` database.

```syntaxsql
CREATE LOGIN login_name
  { 
    FROM EXTERNAL PROVIDER [WITH OBJECT_ID = 'objectid'] 
    | WITH <option_list> [,..] 
  }

<option_list> ::=
    PASSWORD = { 'password' }
    [ , SID = sid ]
```

The *login_name* specifies the Microsoft Entra principal, which is a Microsoft Entra user, group, or application.

For more information, see [CREATE LOGIN (Transact-SQL)](/sql/t-sql/statements/create-login-transact-sql?view=azuresqldb-current&preserve-view=true).

### Create user from login

The following T-SQL syntax creates a database-level Microsoft Entra principal mapped to a Microsoft Entra login in the virtual `master` database. Similar to the syntax for creating a database contained Microsoft Entra user, the only difference is specifying `FROM LOGIN [login_name]` rather than `FROM EXTERNAL PROVIDER`.

To create a Microsoft Entra user from a Microsoft Entra login, use the following syntax. 

```syntaxsql
CREATE USER [user_name] FROM LOGIN [login_name]
```

You can use the `SID` column from **sys.database_principals** to distinguish between a Microsoft Entra contained database user and a Microsoft Entra user created from a login. For a contained database user, the `SID` is a binary string of length 16. For a login-based user, the `SID` is of length 18 with an `AADE` suffix.

> [!NOTE]
> Appending the `AADE` suffix to the SID is how we identify a Microsoft Entra user as being created from a login. However, this also means that the SIDs for the login and its user(s) don't match between `sys.server_principals` and `sys.database_principals`. To correlate the user back to its login, the `AADE` suffix must first be removed.

To understand the conceptual difference between login-based users and contained database users, see [contained database users](/sql/relational-databases/security/contained-database-users-making-your-database-portable).

For more information on all create user syntax, see [CREATE USER (Transact-SQL)](/sql/t-sql/statements/create-user-transact-sql).

### Disable or enable a login using ALTER LOGIN

The [ALTER LOGIN (Transact-SQL)](/sql/t-sql/statements/alter-login-transact-sql?view=azuresqldb-current&preserve-view=true) DDL syntax is used to enable or disable a Microsoft Entra login in Azure SQL Database.

```syntaxsql
ALTER LOGIN [login_name] DISABLE 
```

When a login is disabled, connections are no longer allowed using that server principal. It also disables all database principals (users) created from that login from being able to connect to their respective databases.

> [!NOTE]
> - `ALTER LOGIN login_name DISABLE` won't affect contained database users, since they aren't associated to logins.
> - `ALTER LOGIN login_name DISABLE` is not supported for Microsoft Entra groups.
> - An individual disabled login cannot belong to a user who is part of a login group created in the `master` database (for example, a Microsoft Entra admin group). 
> - For the `DISABLE` or `ENABLE` changes to take immediate effect, the authentication cache and the **TokenAndPermUserStore** cache must be cleared using the T-SQL commands.
>
>   ```sql
>   DBCC FLUSHAUTHCACHE
>   DBCC FREESYSTEMCACHE('TokenAndPermUserStore') WITH NO_INFOMSGS 
>   ```

<a name='roles-for-azure-ad-principals'></a>

## Roles for Microsoft Entra principals

[Special roles for SQL Database](/sql/relational-databases/security/authentication-access/database-level-roles#special-roles-for--and-azure-synapse) can be assigned to *users* in the virtual `master` database for Microsoft Entra principals, including **dbmanager** and **loginmanager**. 

[Azure SQL Database server roles](security-server-roles.md) can be assigned to *logins* in the virtual `master` database.

For a tutorial on how to grant these roles, see [Tutorial: Create and utilize Microsoft Entra server logins](authentication-azure-ad-logins-tutorial.md).

## Limitations and remarks

- The SQL server admin can't create Microsoft Entra logins or users in any databases.
- A SQL admin or SQL user can't execute the following Microsoft Entra operations:
  - `CREATE LOGIN [bob@contoso.com] FROM EXTERNAL PROVIDER`
  - `CREATE USER [bob@contoso.com] FROM EXTERNAL PROVIDER`
  - `EXECUTE AS USER [bob@contoso.com]`
  - `ALTER AUTHORIZATION ON securable::name TO [bob@contoso.com]`
- Impersonation of Microsoft Entra server principals (logins) isn't supported for Azure SQL Database and Azure Synapse Analytics. It [is supported](/azure/azure-sql/managed-instance/aad-security-configure-tutorial?view=azuresql&preserve-view=true#impersonate-azure-ad-server-level-principals-logins) for SQL Managed Instance:
  - [EXECUTE AS Clause (Transact-SQL)](/sql/t-sql/statements/execute-as-clause-transact-sql)
  - [EXECUTE AS (Transact-SQL)](/sql/t-sql/statements/execute-as-transact-sql)
  - Impersonation of Microsoft Entra database principals (users) in a user database is supported on all Microsoft SQL products.
- Microsoft Entra logins can't overlap with the Microsoft Entra administrator. The Microsoft Entra admin takes precedence over any login. If a Microsoft Entra account already has access to the server as a Microsoft Entra admin, individually or as part of a group, any login created for this account won't have any effect. However, the login creation isn't blocked through T-SQL. After the account authenticates to the server, the login will have the effective permissions of a Microsoft Entra admin, and not of a newly created login.
- Changing permissions on specific Microsoft Entra login object isn't supported:
  - `GRANT <PERMISSION> ON LOGIN :: <Microsoft Entra account> TO <Any other login> `
- When you alter permissions for a Microsoft Entra login, by default the changes only take effect the next time the login connects to the Azure SQL Database. Any existing open connections with the login aren't affected. To force permissions changes to take immediate effect, you can manually clear the authentication and TokenAndPermUserStore, as described earlier in [disable or enable a login using ALTER LOGIN](#disable-or-enable-a-login-using-alter-login). This behavior also applies when making server role membership changes with [ALTER SERVER ROLE](/sql/t-sql/statements/alter-server-role-transact-sql).
- In SQL Server Management Studio and Azure Data Studio, the scripting command to create a user doesn't check if there's already a Microsoft Entra login in `master` with the same name. It always generates the T-SQL for a contained database Microsoft Entra user.
- An error might occur if you're trying to create a login or user from a service principal with a nonunique display name. For more information about mitigating this error, see [Microsoft Entra logins and users with nonunique display names](authentication-microsoft-entra-create-users-with-nonunique-names.md).

### Microsoft Entra group server principal limitations

With Microsoft Entra logins in public preview for Azure SQL Database and Azure Synapse Analytics, the following are known limitations:

- [Azure SQL Database server roles](security-server-roles.md) aren't supported for Microsoft Entra groups.
- If your SQL admin is a Microsoft Entra group, there are some limitations when users of that group connect. Each Microsoft Entra user individually isn't part of the `sys.server_principals` table. This has various consequences, including calls to `SUSER_SID` returning `NULL`.
- Microsoft Entra user logins that are part of Microsoft Entra group logins are also not implicitly created, meaning they won't have a default schema, and not be able to perform operations like `CREATE SCHEMA` until a login for the Microsoft Entra user is created, or a default schema is assigned to the group.
- Changing a database's ownership to a Microsoft Entra group as database owner isn't supported.
  - `ALTER AUTHORIZATION ON database::<mydb> TO [my_aad_group]` fails with an error message:

    ```output
    Msg 33181, Level 16, State 1, Line 4
    The new owner cannot be Azure Active Directory group.
    ```

## Next steps

> [!div class="nextstepaction"]
> [Tutorial: Create and utilize Microsoft Entra server logins](authentication-azure-ad-logins-tutorial.md)
