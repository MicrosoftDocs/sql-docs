---
title: Azure Active Directory server principals
description: Using Azure Active Directory server principals (logins) in Azure SQL
author: nofield
ms.author: nofield
ms.reviewer: vanto
ms.date: 10/05/2023
ms.service: sql-db-mi
ms.subservice: security
ms.topic: conceptual
monikerRange: "= azuresql || = azuresql-db || = azuresql-mi"
---

# Azure Active Directory server principals

[!INCLUDE[appliesto-sqldb-sqlmi-asa-dedicated-only](../includes/appliesto-sqldb-sqlmi-asa-dedicated-only.md)]

> [!NOTE]
> Azure Active Directory (Azure AD) server principals (logins) are currently in public preview for Azure SQL Database. Azure SQL Managed Instance can already utilize Azure AD logins.

You can now create and utilize Azure AD server principals, which are logins in the virtual `master` database of a SQL Database. There are several benefits of using Azure AD server principals for SQL Database:

- Support [Azure SQL Database server roles for permission management](security-server-roles.md).
- Support multiple Azure AD users with [special roles for SQL Database](/sql/relational-databases/security/authentication-access/database-level-roles#special-roles-for--and-azure-synapse), such as the `loginmanager` and `dbmanager` roles.
- Functional parity between SQL logins and Azure AD logins.
- Increase functional improvement support, such as utilizing [Azure AD-only authentication](authentication-azure-ad-only-authentication.md). Azure AD-only authentication allows SQL authentication to be disabled, which includes the SQL server admin, SQL logins and users.
- Allows Azure AD principals to support geo-replicas. Azure AD principals will be able to connect to the geo-replica of a user database, with a *read-only* permission and *deny* permission to the primary server.
- Ability to use Azure AD service principal logins with special roles to execute a full automation of user and database creation, as well as maintenance provided by Azure AD applications.
- Closer functionality between Azure SQL Managed Instance and Azure SQL Database, as SQL Managed Instance already supports Azure AD logins in the `master` database.

For more information on Azure AD authentication in Azure SQL, see [Use Azure Active Directory authentication](authentication-aad-overview.md).

## Permissions

The following permissions are required to utilize or create Azure AD logins in the virtual `master` database.

- Azure AD admin permission or membership in the `loginmanager` server role. The first Azure AD login can only be created by the Azure AD admin.
- Must be a member of Azure AD within the same directory used for Azure SQL Database 

By default, the standard permission granted to newly created Azure AD login in the `master` database is **VIEW ANY DATABASE**. 

## Azure AD logins syntax

New syntax for Azure SQL Database to use Azure AD server principals has been introduced with this feature release.

### Create login syntax

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

The *login_name* specifies the Azure AD principal, which is an Azure AD user, group, or application.

For more information, see [CREATE LOGIN (Transact-SQL)](/sql/t-sql/statements/create-login-transact-sql?view=azuresqldb-current&preserve-view=true).

### Create user syntax

The following T-SQL syntax is already available in SQL Database, and can be used for creating database-level Azure AD principals mapped to Azure AD logins in the virtual `master` database.

To create an Azure AD user from an Azure AD login, use the following syntax. Only the Azure AD admin can execute this command in the virtual `master` database.

```syntaxsql
CREATE USER user_name FROM LOGIN login_name
```

For more information and syntax options, see [CREATE USER (Transact-SQL)](/sql/t-sql/statements/create-user-transact-sql).

### Disable or enable a login using ALTER LOGIN syntax

The [ALTER LOGIN (Transact-SQL)](/sql/t-sql/statements/alter-login-transact-sql?view=azuresqldb-current&preserve-view=true) DDL syntax can be used to enable or disable an Azure AD login in Azure SQL Database.

```syntaxsql
ALTER LOGIN login_name DISABLE 
```

The Azure AD principal `login_name` won't be able to log into any user database in the SQL Database logical server where an Azure AD user principal, `user_name` mapped to login `login_name` was created.

> [!NOTE]
> - `ALTER LOGIN login_name DISABLE` is not supported for contained users.
> - `ALTER LOGIN login_name DISABLE` is not supported for Azure AD groups.
> - An individual disabled login cannot belong to a user who is part of a login group created in the `master` database (for example, an Azure AD admin group). 
> - For the `DISABLE` or `ENABLE` changes to take immediate effect, the authentication cache and the **TokenAndPermUserStore** cache must be cleared using the T-SQL commands.
>
>   ```sql
>   DBCC FLUSHAUTHCACHE
>   DBCC FREESYSTEMCACHE('TokenAndPermUserStore') WITH NO_INFOMSGS 
>   ```

## Azure AD logins and users with nonunique display names

Using the display name of a service principal that isn't unique in Azure AD leads to errors when creating the login or user in Azure SQL. For example, if `myapp` isn't unique, you may run into the following error when executing the following query:

```sql
CREATE LOGIN [myapp] FROM EXTERNAL PROVIDER 
```

```output
Msg 33131, Level 16, State 1, Line 4 
Principal 'myapp' has a duplicate display name. Make the display name unique in Azure Active Directory and execute this statement again. 
```

This error occurs because Azure AD allows duplicate display names for [Azure AD application (service principal)](authentication-aad-service-principal.md), while Azure SQL requires unique names to create Azure AD logins and users. To mitigate this problem, the DDL statements to create logins and users have been extended to include the **Object ID** of the Azure resource.

> [!NOTE]
> Most non-unique display names in Azure AD are related to service principals, though occasionally group names can also be non-unique. Azure AD user principal names are unique, as two users cannot have the same user principal. However, an app registration (service principal) can be created with a display name that is the same as a user principal name.
>
> If the service principal display name is not a duplicate, the default `CREATE LOGIN` or `CREATE USER` statement should be used. The `WITH OBJECT_ID` extension is in **public preview**, and is a troubleshooting repair item implemented for use with non-unique service principals. Using it with a unique service principal is not necessary. Using the `WITH OBJECT_ID` extension for a service principal without adding a suffix will run successfully, but it will not be obvious which service principal the login or user was created for. It's recommended to create an alias using a suffix to uniquely identify the service principal. The `WITH OBJECT_ID` extension is not supported for Azure SQL Managed Instance or SQL Server, nor is it supported for SQL Server Management Objects (SMO) Framework.

### T-SQL create login/user extension for nonunique display names

```sql
CREATE LOGIN login_name FROM EXTERNAL PROVIDER 
  WITH OBJECT_ID = 'objectid'
```

```sql
CREATE USER [user_name] FROM EXTERNAL PROVIDER 
  WITH OBJECT_ID = 'objectid'
```

With the T-SQL DDL extension to create logins or users with the Object ID, you can avoid error *33131* and also specify an alias for the login or user created with the Object ID. For example, the following will create a login `myapp4466e` using the application Object ID `4466e2f8-0fea-4c61-a470-xxxxxxxxxxxx`.

```sql
CREATE LOGIN [myapp4466e] FROM EXTERNAL PROVIDER 
  WITH OBJECT_ID='4466e2f8-0fea-4c61-a470-xxxxxxxxxxxx' 
```

- To execute the above query, the specified Object ID must exist in the Azure AD tenant where the Azure SQL resource resides. Otherwise, the `CREATE` command will fail with the error message: `Msg 37545, Level 16, State 1, Line 1 '' is not a valid object id for '' or you do not have permission.`
- The login name must contain the original service principal name extended by a user-defined suffix. As a best practice, the suffix can include an initial part of its Object ID. For example, `myapp2ba6c` for the Object ID `2ba6c0a3-cda4-4878-a5ca-xxxxxxxxxxxx`.  

The prefix of the alias is your service principal, or application display name, and must be a part of the initial `CREATE LOGIN` or `CREATE USER` statement. The alias suffix should be the first few characters of the Object ID.

We recommend this naming convention for the suffix to explicitly associate the application login or user alias with its Object ID.

> [!NOTE]
> The application alias adheres to T-SQL syntax, including a max length of up to 128-characters. However we recommend limiting the suffix to the first 5 characters of the Object ID.
>
> Changing the display name of the service principal in the Azure Portal after running the `CREATE LOGIN` or `CREATE USER` statement with the `WITH OBJECT_ID` extension doesn't affect the new database login or user, as there is no synchronization between Azure AD and Azure SQL after the CREATE statement.

### Identify the user created for the application

For nonunique service principals, it's important to verify the Azure AD alias is tied to the correct application. To check that the user was created for the correct service principal (application):

1. Get the **Application ID** of the application, or **Object ID** of the Azure AD group from the user created in SQL Database. See the following queries:

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

   - To get the **Object ID** of the Azure AD group from the user created, execute the following query:

     ```sql
     SELECT CAST(sid as uniqueidentifier) ObjectID, createdate FROM sys.sysusers WHERE NAME = 'myappgroupd3451b' 
     ```

     **Example output:**

     :::image type="content" source="media/authentication-azure-ad-logins/object-id-output.png" alt-text="Screenshot of SSMS output for the Object ID of the Azure AD group.":::

     To check the SID of the Azure AD group from the user created, execute the following query: 

     ```sql
     SELECT SID, createdate FROM sys.sysusers WHERE NAME = 'myappgroupd3451b' 
     ```

     **Example output:**

     :::image type="content" source="media/authentication-azure-ad-logins/security-id-group-output.png" alt-text="Screenshot of SSMS output for the SID of the group.":::

   - To get the Object ID and Application ID of the application using PowerShell execute the following command: 

     ```powershell
     Get-AzADApplication -DisplayName "myapp2ba6c"
     ```

1. Go to the [Azure portal](https://portal.azure.com), and in your **Enterprise application** or Azure AD group resource, check the **Application ID** or **Object ID** respectively. See if it matches the one obtained from the above query.

> [!NOTE]
> When creating a user from a service principal, the **Object ID** is required when using the `WITH OBJECT_ID` clause with the `CREATE` T-SQL statement. This is different from the **Application ID** that is returned when you are trying to verify the alias in Azure SQL. Using this verification process, you can identify the main owner of the SQL alias in Azure AD, and prevent possible mistakes when creating logins or users with an Object ID.

### Finding the right Object ID

For information on the Object ID of a service principal, see [Service principal object](/azure/active-directory/develop/app-objects-and-service-principals#service-principal-object). You can locate the Object ID of the service principal listed next to the application name in the Azure portal under **Enterprise applications**.

> [!WARNING]
> The Object ID obtained in the **App registration** Overview page is different from the Object ID obtained in the **Enterprise applications** Overview page. If you're in the **App registration** Overview page, select the linked **Managed application in local directory** application name to navigate to the right Object ID on the **Enterprise applications** Overview page.

## Roles for Azure AD principals

[Special roles for SQL Database](/sql/relational-databases/security/authentication-access/database-level-roles#special-roles-for--and-azure-synapse) can be assigned to *users* in the virtual `master` database for Azure AD principals, including **dbmanager** and **loginmanager**. 

[Azure SQL Database server roles](security-server-roles.md) can be assigned to *logins* in the virtual `master` database.

For a tutorial on how to grant these roles, see [Tutorial: Create and utilize Azure Active Directory server logins](authentication-azure-ad-logins-tutorial.md).

## Limitations and remarks

- The SQL server admin can't create Azure AD logins or users in any databases.
- Changing a database ownership to an Azure AD group as database owner isn't supported.
  - `ALTER AUTHORIZATION ON database::<mydb> TO [my_aad_group]` fails with an error message:
    ```output
    Msg 33181, Level 16, State 1, Line 4
    The new owner cannot be Azure Active Directory group.
    ```
  - Changing a database ownership to an individual user is supported.
- A SQL admin or SQL user can't execute the following Azure AD operations: 
  - `CREATE LOGIN [bob@contoso.com] FROM EXTERNAL PROVIDER` 
  - `CREATE USER [bob@contoso.com] FROM EXTERNAL PROVIDER` 
  - `EXECUTE AS USER [bob@contoso.com]`
  - `ALTER AUTHORIZATION ON securable::name TO [bob@contoso.com]`
- Impersonation of Azure AD server-level principals (logins) isn't supported: 
  - [EXECUTE AS Clause (Transact-SQL)](/sql/t-sql/statements/execute-as-clause-transact-sql)
  - [EXECUTE AS (Transact-SQL)](/sql/t-sql/statements/execute-as-transact-sql)
  - Impersonation of Azure AD database-level principals (users) at a user database-level is still supported.
- Azure AD logins overlapping with Azure AD administrator aren't supported. Azure AD admin takes precedence over any login. If an Azure AD account already has access to the server as an Azure AD admin, either directly or as a member of the admin group, the login created for this user won't have any effect. The login creation isn't blocked through T-SQL. After the account authenticates to the server, the login will have the effective permissions of an Azure AD admin, and not of a newly created login.
- Changing permissions on specific Azure AD login object isn't supported:
  - `GRANT <PERMISSION> ON LOGIN :: <Azure AD account> TO <Any other login> `
- When permissions are altered for an Azure AD login with existing open connections to an Azure SQL Database, permissions aren't effective until the user reconnects. Also [flush the authentication cache and the TokenAndPermUserStore cache](#disable-or-enable-a-login-using-alter-login-syntax). This applies to server role membership change using the [ALTER SERVER ROLE](/sql/t-sql/statements/alter-server-role-transact-sql) statement. 
- Setting an Azure AD login mapped to an Azure AD group as the database owner isn't supported.
- [Azure SQL Database server roles](security-server-roles.md) aren't supported for Azure AD groups.
- The current scripting command in SQL Server Management Studio and in Azure Data Studio for Azure AD users with logins doesn't generate a correct T-SQL syntax for a user creation with a login. Instead, the script generates a T-SQL syntax for a contained Azure AD user without a login in the virtual `master` database.
- To distinguish between the Azure AD contained user without a login in the virtual `master` database and an Azure AD user created from a login in the virtual `master` database, view the `SID` in **sys.database_principals**, and check for the `AADE` suffix appended in the `SID` column for a user created with a login.

## Next steps

> [!div class="nextstepaction"]
> [Tutorial: Create and utilize Azure Active Directory server logins](authentication-azure-ad-logins-tutorial.md)
