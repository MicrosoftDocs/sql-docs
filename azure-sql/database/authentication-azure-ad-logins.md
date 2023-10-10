---
title: Microsoft Entra server principals
description: Using Microsoft Entra server principals (logins) in Azure SQL
author: nofield
ms.author: nofield
ms.reviewer: vanto
ms.date: 09/27/2023
ms.service: sql-db-mi
ms.subservice: security
ms.topic: conceptual
monikerRange: "= azuresql || = azuresql-db || = azuresql-mi"
---

# Microsoft Entra server principals

[!INCLUDE[appliesto-sqldb-sqlmi-asa-dedicated-only](../includes/appliesto-sqldb-sqlmi-asa-dedicated-only.md)]


You can now create and utilize server principals from Microsoft Entra ID ([formerly Azure Active Directory](/azure/active-directory/fundamentals/new-name)), which are logins in the virtual `master` database of a SQL Database. There are several benefits of using Microsoft Entra server principals for SQL Database:

- Support [Azure SQL Database server roles for permission management](security-server-roles.md).
- Support multiple Microsoft Entra users with [special roles for SQL Database](/sql/relational-databases/security/authentication-access/database-level-roles#special-roles-for--and-azure-synapse), such as the `loginmanager` and `dbmanager` roles.
- Functional parity between SQL logins and Microsoft Entra logins.
- Increase functional improvement support, such as utilizing [Microsoft Entra-only authentication](authentication-azure-ad-only-authentication.md). Microsoft Entra-only authentication allows SQL authentication to be disabled, which includes the SQL server admin, SQL logins and users.
- Allows Microsoft Entra principals to support geo-replicas. Microsoft Entra principals will be able to connect to the geo-replica of a user database, with a *read-only* permission and *deny* permission to the primary server.
- Ability to use Microsoft Entra service principal logins with special roles to execute a full automation of user and database creation, as well as maintenance provided by Microsoft Entra applications.

For more information on Microsoft Entra authentication in Azure SQL, see [Use Microsoft Entra authentication](authentication-aad-overview.md)

> [!NOTE]
> Microsoft Entra server principals (logins) are currently in public preview for Azure SQL Database. Azure SQL Managed Instance can already utilize Microsoft Entra logins.

## Permissions

The following permissions are required to utilize or create Microsoft Entra logins in the virtual `master` database.

- Microsoft Entra admin permission or membership in the `loginmanager` server role. The first Microsoft Entra login can only be created by the Microsoft Entra admin.
- Must be a member of Microsoft Entra ID within the same directory used for Azure SQL Database 

By default, the standard permission granted to newly created Microsoft Entra login in the `master` database is **VIEW ANY DATABASE**. 

<a name='azure-ad-logins-syntax'></a>

## Microsoft Entra logins syntax

Azure SQL Database uses different syntax to create server principals from Microsoft Entra ID ([formerly Azure Active Directory](/azure/active-directory/fundamentals/new-name)).

### Create login syntax

```syntaxsql
CREATE LOGIN login_name { FROM EXTERNAL PROVIDER | WITH <option_list> [,..] }  

<option_list> ::=      
    PASSWORD = {'password'}   
    | , SID = sid, ] 
```

The *login_name* specifies the Microsoft Entra principal, which is a Microsoft Entra user, group, or application.

For more information, see [CREATE LOGIN (Transact-SQL)](/sql/t-sql/statements/create-login-transact-sql?view=azuresqldb-current&preserve-view=true).

### Create user syntax

The below T-SQL syntax is already available in SQL Database, and can be used for creating database-level Microsoft Entra principals mapped to Microsoft Entra logins in the virtual `master` database.

To create a Microsoft Entra user from a Microsoft Entra login, use the following syntax. Only the Microsoft Entra admin can execute this command in the virtual `master` database.

```syntaxsql
CREATE USER user_name FROM LOGIN login_name
```

For more information, see [CREATE USER (Transact-SQL)](/sql/t-sql/statements/create-user-transact-sql).

### Disable or enable a login using ALTER LOGIN syntax

The [ALTER LOGIN (Transact-SQL)](/sql/t-sql/statements/alter-login-transact-sql?view=azuresqldb-current&preserve-view=true) DDL syntax can be used to enable or disable a Microsoft Entra login in Azure SQL Database.

```syntaxsql
ALTER LOGIN login_name DISABLE 
```

The Microsoft Entra principal `login_name` won't be able to log into any user database in the SQL Database logical server where a Microsoft Entra user principal, `user_name` mapped to login `login_name` was created.

> [!NOTE]
> - `ALTER LOGIN login_name DISABLE` is not supported for contained users.
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
- Changing a database ownership to a Microsoft Entra group as database owner isn't supported.
  - `ALTER AUTHORIZATION ON database::<mydb> TO [my_aad_group]` fails with an error message:
    ```output
    Msg 33181, Level 16, State 1, Line 4
    The new owner cannot be Azure Active Directory group.
    ```
  - Changing a database ownership to an individual user is supported.
- A SQL admin or SQL user can't execute the following Microsoft Entra operations: 
  - `CREATE LOGIN [bob@contoso.com] FROM EXTERNAL PROVIDER` 
  - `CREATE USER [bob@contoso.com] FROM EXTERNAL PROVIDER` 
  - `EXECUTE AS USER [bob@contoso.com]`
  - `ALTER AUTHORIZATION ON securable::name TO [bob@contoso.com]`
- Impersonation of Microsoft Entra server-level principals (logins) isn't supported: 
  - [EXECUTE AS Clause (Transact-SQL)](/sql/t-sql/statements/execute-as-clause-transact-sql)
  - [EXECUTE AS (Transact-SQL)](/sql/t-sql/statements/execute-as-transact-sql)
  - Impersonation of Microsoft Entra database-level principals (users) at a user database-level is still supported.
- Microsoft Entra logins overlapping with Microsoft Entra administrator aren't supported. Microsoft Entra admin takes precedence over any login. If a Microsoft Entra account already has access to the server as a Microsoft Entra admin, either directly or as a member of the admin group, the login created for this user won't have any effect. The login creation isn't blocked through T-SQL. After the account authenticates to the server, the login will have the effective permissions of a Microsoft Entra admin, and not of a newly created login.
- Changing permissions on specific Microsoft Entra login object isn't supported:
  - `GRANT <PERMISSION> ON LOGIN :: <Azure AD account> TO <Any other login> `
- When permissions are altered for a Microsoft Entra login with existing open connections to an Azure SQL Database, permissions aren't effective until the user reconnects. Also [flush the authentication cache and the TokenAndPermUserStore cache](#disable-or-enable-a-login-using-alter-login-syntax). This applies to server role membership change using the [ALTER SERVER ROLE](/sql/t-sql/statements/alter-server-role-transact-sql) statement. 
- Setting a Microsoft Entra login mapped to a Microsoft Entra group as the database owner isn't supported.
- [Azure SQL Database server roles](security-server-roles.md) aren't supported for Microsoft Entra groups.
- The current scripting command in SQL Server Management Studio and in Azure Data Studio for Microsoft Entra users with logins does not generate a correct T-SQL syntax for a user creation with a login. Instead, the script generates a T-SQL syntax for a contained Microsoft Entra user without a login in the virtual `master` database.
- To distinguish between the Microsoft Entra contained user without a login in the virtual `master` database and a Microsoft Entra user created from a login in the virtual `master` database, view the `SID` in **sys.database_principals**, and check for the `AADE` suffix appended in the `SID` column for a user created with a login.

## Next steps

> [!div class="nextstepaction"]
> [Tutorial: Create and utilize Microsoft Entra server logins](authentication-azure-ad-logins-tutorial.md)
