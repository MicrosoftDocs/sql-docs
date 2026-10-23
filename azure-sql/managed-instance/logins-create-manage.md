---
title: Authorize Server and Database Access Using Logins and User Accounts
titleSuffix: SQL Managed Instance
description: Learn about how SQL Managed Instance authenticate users for access using logins and user accounts. Also learn how to grant database roles and explicit permissions to authorize logins and users to perform actions and query data.
author: VanMSFT
ms.author: vanto
ms.reviewer: vanto
ms.date: 05/28/2026
ms.service: azure-sql-managed-instance
ms.subservice: security
ms.topic: concept-article
keywords:
  - "sql managed instance security"
  - "database security management"
  - "login security"
  - "database security"
  - "database access"
monikerRange: "=azuresql || =azuresql-mi"
---
# Authorize database access to Azure SQL Managed Instance

[!INCLUDE [appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

> [!div class="op_single_selector"]
> - [Azure SQL Database](../database/logins-create-manage.md?view=azuresql-db&preserve-view=true)
> - [Azure SQL Managed Instance](logins-create-manage.md?view=azuresql-mi&preserve-view=true)

In this article, you learn about:

- Configuration options for SQL Managed Instance that enable users to perform administrative tasks and access data stored in these databases.
- Access and authorization configuration after creating a new server.
- How to add logins and user accounts in the `master` database and then grant these accounts administrative permissions.
- How to add user accounts in user databases, either associated with logins or as contained user accounts.
- How to configure user accounts with permissions in user databases by using database roles and explicit permissions.
- For Azure SQL Database, see [Authorize database access to SQL Database](../database/logins-create-manage.md).
- For Azure Synapse Analytics, see [Authorize database access to Azure Synapse Analytics](/azure/synapse-analytics/sql/logins-create-manage).

[!INCLUDE [entra-id](../includes/entra-id.md)]

## Authentication and authorization

[**Authentication**](../database/security-overview.md#authentication) is the process of proving the user is who they claim to be. A user connects to a database using a user account.

When a user attempts to connect to a database, they provide a user account and authentication information. The user is authenticated using one of the following two authentication methods:

- [SQL authentication](/sql/relational-databases/security/choose-an-authentication-mode?view=azuresqldb-mi-current&preserve-view=true#connecting-through-sql-server-authentication)

  By using this authentication method, the user submits a user account name and associated password to establish a connection. The `master` database stores this password for user accounts linked to a login. The database containing the user accounts *not* linked to a login stores the password.

  > [!NOTE]  
  > For password policy in Azure SQL Managed Instance, see [Azure SQL Managed Instance frequently asked questions (FAQ)](../managed-instance/frequently-asked-questions-faq.yml#password-policy).

- [Microsoft Entra authentication for Azure SQL](../database/authentication-aad-overview.md)

  By using this authentication method, the user submits a user account name and requests that the service use the credential information stored in Microsoft Entra ID ([formerly Azure Active Directory](/entra/fundamentals/new-name)).

**Logins and users**: You can associate a user account in a database with a login that the `master` database stores, or you can make it a user name that an individual database stores.

- A **login** is an account in the `master` database, to which you can link a user account in one or more databases. By using a login, you store the credential information for the user account with the login.
- A **user account** is an individual account in any database that might be, but doesn't have to be, linked to a login. By using a user account that isn't linked to a login, you store the credential information with the user account.

[**Authorization**](../database/security-overview.md#authorization-and-access-management) to access data and perform various actions is managed by using database roles and explicit permissions. Authorization refers to the permissions assigned to a user, and it determines what that user is allowed to do. Your user account's database [role memberships](/sql/relational-databases/security/authentication-access/database-level-roles) and [object-level permissions](/sql/relational-databases/security/permissions-database-engine) control authorization. As a best practice, grant users the least privileges necessary.

## Existing logins and user accounts after creating a new database

When you first deploy an Azure SQL resource, specify a login name and a password for a special type of administrative login, the **Server admin**. During deployment, the following configuration of logins and users in the `master` and user databases occurs:

[!INCLUDE [server-admin-login-security-note](../includes/server-admin-login-security-note.md)]

- The deployment process creates a SQL authentication login with administrative privileges by using the login name you specified. A [login](/sql/relational-databases/security/authentication-access/principals-database-engine#sa-login) is an individual account for signing in to Azure SQL Managed Instance.
- The deployment process grants this authentication login full administrative permissions on all databases as a [server-level principal](/sql/relational-databases/security/authentication-access/principals-database-engine). The authentication login has all available permissions and can't be limited. In a SQL Managed Instance, the deployment process adds this authentication login to the [sysadmin fixed server role](/sql/relational-databases/security/authentication-access/server-level-roles).
- When this account signs into a database, it matches to the special user account `dbo` ([user account](/sql/relational-databases/security/authentication-access/getting-started-with-database-engine-permissions#database-users)), which exists in each user database. The [dbo](/sql/relational-databases/security/authentication-access/principals-database-engine) user has all database permissions in the database and is member of the `db_owner` fixed database role. Additional fixed database roles are discussed later in this article.

To identify the **Server admin** account:

1. Go to [Azure SQL hub at aka.ms/azuresqlhub](https://aka.ms/azuresqlhub). 
1. In the resource menu, go to your SQL managed instance.
1. Under **Settings**, select the **Properties** page.
1. View the values for **Server admin login** or **Microsoft Entra admin**.

> [!IMPORTANT]  
> You can't change the name of the **Server admin** account after you create it. To reset the password, go to the Azure portal, select your SQL managed instance, and select **Reset password**. You can also use PowerShell or the Azure CLI.

## Create additional logins and users having administrative permissions

At this point, your SQL managed instance is only configured for a single SQL authentication login and user account. To create additional logins with full or partial administrative permissions, use the following options:

- **Create a Microsoft Entra administrator account with full administrative permissions**

  Enable Microsoft Entra authentication and add a **Microsoft Entra admin**. You can configure one Microsoft Entra account as an administrator of the Azure SQL deployment with full administrative permissions. This account can be either an individual or security group account. You *must* configure a **Microsoft Entra admin** if you want to use Microsoft Entra accounts to connect to Azure SQL Managed Instance. For detailed information on enabling Microsoft Entra authentication for all Azure SQL deployment types, see the following articles:

  - [Microsoft Entra authentication for Azure SQL](../database/authentication-aad-overview.md?view=azuresql-mi&preserve-view=true)
  - [Configure and manage Microsoft Entra authentication with Azure SQL](../database/authentication-aad-configure.md?view=azuresql-mi&preserve-view=true)

- **In SQL Managed Instance, create SQL authentication logins with full administrative permissions**

  - Create an additional SQL authentication login in the `master` database.
    - Add the login to the [sysadmin fixed server role](/sql/relational-databases/security/authentication-access/server-level-roles?view=azuresqldb-mi-current&preserve-view=true) by using the [ALTER SERVER ROLE](/sql/t-sql/statements/alter-server-role-transact-sql?view=azuresqldb-mi-current&preserve-view=true) statement. This login has full administrative permissions.
  - Alternatively, create a [Microsoft Entra authentication login](../database/authentication-aad-configure.md?view=azuresql-mi&preserve-view=true#provision-azure-ad-admin-sql-managed-instance) by using the [CREATE LOGIN](/sql/t-sql/statements/create-login-transact-sql?view=azuresqldb-mi-current&preserve-view=true) syntax.

  > [!NOTE]  
  > The `dbmanager` and `loginmanager` roles don't pertain to Azure SQL Managed Instance deployments.

## Create accounts for nonadministrator users

Create accounts for nonadministrative users by using one of the following methods:

- **Create a login**

    Create a SQL authentication login in the `master` database. Then create a user account in each database that the user needs access to and associate the user account with the login. Use this approach when the user needs to access multiple databases and you want to keep the passwords synchronized. However, this approach has complexities when used with geo-replication as the login must be created on both the primary server and the secondary servers.

- **Create a user account**

    Create a user account in the database that a user needs access to.

  With SQL Managed Instance supporting [Microsoft Entra server principals](../database/authentication-aad-configure.md#create-microsoft-entra-principals-in-sql), you can create user accounts to authenticate to the SQL Managed Instance without requiring database users to be created as a contained database user.

  By using this approach, the user authentication information is stored in each database and automatically replicated to geo-replicated databases. However, if the same account exists in multiple databases and you're using SQL authentication, you must keep the passwords synchronized manually. Additionally, if a user has an account in different databases with different passwords, remembering those passwords can become a problem.

> [!IMPORTANT]  
> To create contained users mapped to Microsoft Entra identities in SQL Managed Instance, use a SQL authentication login with `sysadmin` permissions to also create a Microsoft Entra authentication login or user.

For examples that show how to create logins and users, see:

- [Create login for Azure SQL Managed Instance](/sql/t-sql/statements/create-login-transact-sql?view=azuresqldb-mi-current&preserve-view=true#examples-2)
- [Create user](/sql/t-sql/statements/create-user-transact-sql?view=azuresqldb-mi-current&preserve-view=true#examples)
- [Creating Microsoft Entra contained users](../database/authentication-aad-configure.md?view=azuresql-mi&preserve-view=true#contained-database-users)

## Using fixed and custom database roles

After creating a user account in a database, either based on a login or as a contained user, you can authorize that user to perform various actions and to access data in a particular database. Use the following methods to authorize access:

- **Fixed database roles**

  Add the user account to a [fixed database role](/sql/relational-databases/security/authentication-access/database-level-roles?view=azuresqldb-mi-current&preserve-view=true). There are nine fixed database roles, each with a defined set of permissions. The most common fixed database roles are: **db_owner**, **db_ddladmin**, **db_datawriter**, **db_datareader**, **db_denydatawriter**, and **db_denydatareader**. Use **db_owner** to grant full permission to only a few users. The other fixed database roles are useful for getting a simple database in development quickly, but aren't recommended for most production databases. For example, the **db_datareader** fixed database role grants read access to every table in the database, which is more than is strictly necessary.

  - To add a user to a fixed database role, use the [ALTER ROLE](/sql/t-sql/statements/alter-role-transact-sql?view=azuresqldb-mi-current&preserve-view=true) statement. For examples, see [ALTER ROLE examples](/sql/t-sql/statements/alter-role-transact-sql?view=azuresqldb-mi-current&preserve-view=true#examples).

- **Custom database role**

  Create a custom database role by using the [CREATE ROLE](/sql/t-sql/statements/create-role-transact-sql?view=azuresqldb-mi-current&preserve-view=true) statement. A custom role enables you to create your own user-defined database roles and carefully grant each role the least permissions necessary for the business need. Then add users to the custom role. When a user is a member of multiple roles, they aggregate the permissions of them all.

- **Grant permissions directly**

  Grant the user account [permissions](/sql/relational-databases/security/permissions-database-engine?view=azuresqldb-mi-current&preserve-view=true) directly. There are over 100 permissions that you can individually grant or deny. Many of these permissions are nested. For example, the `UPDATE` permission on a schema includes the `UPDATE` permission on each table within that schema. As in most permission systems, the denial of a permission overrides a grant. Because of the nested nature and the number of permissions, it can take careful study to design an appropriate permission system to properly protect your database. Start with the list of permissions at [Permissions (Database Engine)](/sql/relational-databases/security/permissions-database-engine?view=azuresqldb-mi-current&preserve-view=true) and review the [poster size graphic](/sql/relational-databases/security/media/database-engine-permissions.png) of the permissions.

## Using groups

Efficient access management assigns permissions to Microsoft Entra security groups and fixed or custom roles instead of to individual users.

- When using Microsoft Entra authentication, put Microsoft Entra users into a Microsoft Entra security group. Create a contained database user for the group. Add one or more database users as a member to custom or built-in database roles with the specific permissions appropriate to that group of users.

- When using SQL authentication, create contained database users in the database. Place one or more database users into a custom database role with specific permissions appropriate to that group of users.

  > [!NOTE]  
  > You can also use groups for noncontained database users.

Familiarize yourself with the following features that can be used to limit or elevate permissions:

- [Impersonation](/dotnet/framework/data/adonet/sql/customizing-permissions-with-impersonation-in-sql-server) and [module-signing](/dotnet/framework/data/adonet/sql/signing-stored-procedures-in-sql-server) can be used to securely elevate permissions temporarily.
- [Row-Level Security](/sql/relational-databases/security/row-level-security?view=azuresqldb-mi-current&preserve-view=true) can be used to limit which rows a user can access.
- [Dynamic data masking](../database/dynamic-data-masking-overview.md?view=azuresql-mi&preserve-view=true) can be used to limit exposure of sensitive data.
- [Stored procedures](/sql/relational-databases/stored-procedures/stored-procedures-database-engine?view=azuresqldb-mi-current&preserve-view=true) can be used to limit the actions that can be taken on the database.

## Next step

> [!div class="nextstepaction"]
> [An overview of Azure SQL Database and SQL Managed Instance security capabilities](../database/security-overview.md)
