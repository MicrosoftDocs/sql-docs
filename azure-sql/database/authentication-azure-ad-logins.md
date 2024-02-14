---
title: Microsoft Entra server principals
titleSuffix: Azure SQL Database & Azure SQL Managed Instance & Azure Synapse Analytics
description: Using Microsoft Entra server principals (logins) in Azure SQL
author: nofield
ms.author: nofield
ms.reviewer: vanto
ms.date: 02/15/2024
ms.service: sql-db-mi
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

## Microsoft Entra logins and users with nonunique display names

Using the display name of a service principal that isn't unique in Microsoft Entra ID leads to errors when creating the login or user in Azure SQL. For example, if `myapp` isn't unique, you may run into the following error when executing the following query:

```sql
CREATE LOGIN [myapp] FROM EXTERNAL PROVIDER 
```

```output
Msg 33131, Level 16, State 1, Line 4 
Principal 'myapp' has a duplicate display name. Make the display name unique in Azure Active Directory and execute this statement again. 
```

This error occurs because Microsoft Entra ID allows duplicate display names for [Microsoft Entra application (service principal)](authentication-aad-service-principal.md), while Azure SQL requires unique names to create Microsoft Entra logins and users. To mitigate this problem, the DDL statements to create logins and users (`WITH OBJECT_ID`) have been extended to include the **Object ID** of the Azure resource.

> [!NOTE]
> Most nonunique display names in Microsoft Entra ID are related to service principals, though occasionally group names can also be non-unique. Microsoft Entra user principal names are unique, as two users cannot have the same user principal. However, an app registration (service principal) can be created with a display name that is the same as a user principal name.
>
> If the service principal display name is not a duplicate, the default `CREATE LOGIN` or `CREATE USER` statement should be used. The `WITH OBJECT_ID` extension is in **public preview**, and is a troubleshooting repair item implemented for use with non-unique service principals. Using it with a unique service principal is not necessary. Using the `WITH OBJECT_ID` extension for a service principal without adding a suffix will run successfully, but it will not be obvious which service principal the login or user was created for. It's recommended to create an alias using a suffix to uniquely identify the service principal. The `WITH OBJECT_ID` extension is not supported for Azure SQL Managed Instance or SQL Server, nor is it supported for SQL Server Management Objects (SMO) Framework.

### T-SQL create login/user extension for nonunique display names

```sql
CREATE LOGIN [login_name] FROM EXTERNAL PROVIDER 
  WITH OBJECT_ID = 'objectid'
```

```sql
CREATE USER [user_name] FROM EXTERNAL PROVIDER 
  WITH OBJECT_ID = 'objectid'
```

With the T-SQL DDL extension to create logins or users with the Object ID, you can avoid error *33131* and also specify an alias for the login or user created with the Object ID. For example, the following will create a login `myapp4466e` using the application Object ID `4466e2f8-0fea-4c61-a470-xxxxxxxxxxxx`.

```sql
CREATE LOGIN [myapp4466e] FROM EXTERNAL PROVIDER 
  WITH OBJECT_ID = '4466e2f8-0fea-4c61-a470-xxxxxxxxxxxx' 
```

- To execute the above query, the specified Object ID must exist in the Microsoft Entra tenant where the Azure SQL resource resides. Otherwise, the `CREATE` command will fail with the error message: `Msg 37545, Level 16, State 1, Line 1 '' is not a valid object id for '' or you do not have permission.`
- The login or user name must contain the original service principal name extended by a user-defined suffix when using the `CREATE LOGIN` or `CREATE USER` statement. As a best practice, the suffix can include an initial part of its Object ID. For example, `myapp2ba6c` for the Object ID `2ba6c0a3-cda4-4878-a5ca-xxxxxxxxxxxx`.  

This naming convention is recommended to explicitly associate the database user or login back to its object in Microsoft Entra ID.

> [!NOTE]
> The alias adheres to the T-SQL specification for `sysname`, including a max length of 128 characters. We recommend limiting the suffix to the first 5 characters of the Object ID.
>
>  The display name of the service principal in Microsoft Entra ID is not synchronized to the database login or user alias. Running `CREATE LOGIN` or `CREATE USER` will not affect the display name in the Azure Portal. Similarly, modifying the Microsoft Entra ID display name will not affect the database login or user alias.

### Identify the user created for the application

For nonunique service principals, it's important to verify the Microsoft Entra alias is tied to the correct application. To check that the user was created for the correct service principal (application):

1. Get the **Application ID** of the application, or **Object ID** of the Microsoft Entra group from the user created in SQL Database. See the following queries:

   - To get the **Application ID** of the service principal from the user created, execute the following query:

     ```sql
     SELECT CAST(sid as uniqueidentifier) ApplicationID, create_date FROM sys.server_principals WHERE NAME = 'myapp2ba6c' 
     ```

     **Example output:**

     :::image type="content" source="media/authentication-azure-ad-logins/application-id-output.png" alt-text="Screenshot of SSMS output for the Application ID.":::

     The Application ID is converted from the security identification number (SID) for the specified login or user name, which we can confirm by executing the below query and comparing the last several digits and create dates:

     ```sql
     SELECT SID, create_date FROM sys.server_principals WHERE NAME = 'myapp2ba6c' 
     ```

     **Example output:**

     :::image type="content" source="media/authentication-azure-ad-logins/security-id-output.png" alt-text="Screenshot of SSMS output for the SID of the application.":::

   - To get the **Object ID** of the Microsoft Entra group from the user created, execute the following query:

     ```sql
     SELECT CAST(sid as uniqueidentifier) ObjectID, createdate FROM sys.sysusers WHERE NAME = 'myappgroupd3451b' 
     ```

     **Example output:**

     :::image type="content" source="media/authentication-azure-ad-logins/object-id-output.png" alt-text="Screenshot of SSMS output for the Object ID of the Microsoft Entra group.":::

     To check the SID of the Microsoft Entra group from the user created, execute the following query: 

     ```sql
     SELECT SID, createdate FROM sys.sysusers WHERE NAME = 'myappgroupd3451b' 
     ```

     **Example output:**

     :::image type="content" source="media/authentication-azure-ad-logins/security-id-group-output.png" alt-text="Screenshot of SSMS output for the SID of the group.":::

   - To get the Object ID and Application ID of the application using PowerShell execute the following command: 

     ```powershell
     Get-AzADApplication -DisplayName "myapp2ba6c"
     ```

1. Go to the [Azure portal](https://portal.azure.com), and in your **Enterprise application** or Microsoft Entra group resource, check the **Application ID** or **Object ID** respectively. See if it matches the one obtained from the above query.

> [!NOTE]
> When creating a user from a service principal, the **Object ID** is required when using the `WITH OBJECT_ID` clause with the `CREATE` T-SQL statement. This is different from the **Application ID** that is returned when you are trying to verify the alias in Azure SQL. Using this verification process, you can identify the main owner of the SQL alias in Microsoft Entra ID, and prevent possible mistakes when creating logins or users with an Object ID.

### Finding the right Object ID

For information on the Object ID of a service principal, see [Service principal object](/azure/active-directory/develop/app-objects-and-service-principals#service-principal-object). You can locate the Object ID of the service principal listed next to the application name in the Azure portal under **Enterprise applications**.

> [!WARNING]
> The Object ID obtained in the **App registration** Overview page is different from the Object ID obtained in the **Enterprise applications** Overview page. If you're in the **App registration** Overview page, select the linked **Managed application in local directory** application name to navigate to the right Object ID on the **Enterprise applications** Overview page.

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
