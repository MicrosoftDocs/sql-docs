---
title: "Get started with Database Engine permissions"
description: Review some basic security concepts in SQL Server and learn about a typical implementation of Database Engine permissions.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 10/14/2022
ms.service: sql
ms.subservice: security
ms.topic: quickstart
ms.custom: intro-quickstart
helpviewer_keywords:
  - "permissions [SQL Server], getting started"
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Get started with Database Engine permissions

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

Permissions in the [!INCLUDE[ssDE](../../../includes/ssde-md.md)] are managed at the server level through logins and server roles, and at the database level through database users and database roles. The model for [!INCLUDE[ssSDS](../../../includes/sssds-md.md)] exposes the same system within each database, but the server level permissions aren't available. This article reviews some basic security concepts and then describes a typical implementation of the permissions.

## Security principals

Security principal is the official name of the identities that use [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] and that can be assigned permission to take actions. They are usually people or groups of people, but can be other entities that pretend to be people. The security principals can be created and managed using the [!INCLUDE[tsql](../../../includes/tsql-md.md)] listed, or by using [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)].

#### Logins

Logins are individual user accounts for logging on to the [!INCLUDE[ssDEnoversion](../../../includes/ssdenoversion-md.md)]. [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] and [!INCLUDE[ssSDS](../../../includes/sssds-md.md)] support logins based on Windows authentication and logins based on [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] authentication. For information about the two types of logins, see [Choose an Authentication Mode](../../../relational-databases/security/choose-an-authentication-mode.md).

#### Fixed server roles

In [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], fixed server roles are a set of pre-configured roles that provide convenient group of server-level permissions. Logins can be added to the roles using the `ALTER SERVER ROLE ... ADD MEMBER` statement. For more information, see [ALTER SERVER ROLE (Transact-SQL)](../../../t-sql/statements/alter-server-role-transact-sql.md). [!INCLUDE[ssSDS](../../../includes/sssds-md.md)] doesn't support the fixed server roles, but has two roles in the `master` database (`dbmanager` and `loginmanager`) that act like server roles.

#### User-defined server roles

In [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], you can create your own server roles and assign server-level permissions to them. Logins can be added to the server roles using the `ALTER SERVER ROLE ... ADD MEMBER` statement. For more information, see [ALTER SERVER ROLE (Transact-SQL)](../../../t-sql/statements/alter-server-role-transact-sql.md). [!INCLUDE[ssSDS](../../../includes/sssds-md.md)] doesn't support the user-defined server roles.

#### Database users

Logins are granted access to a database by creating a database user in a database and mapping that database user to sign in. Typically the database user name is the same as the login name, though it doesn't have to be the same. Each database user maps to a single login. A login can be mapped to only one user in a database, but can be mapped as a database user in several different databases.

Database users can also be created that don't have a corresponding login. These users are called *contained database users*. [!INCLUDE[msCoName](../../../includes/msconame-md.md)] encourages the use of contained database users because it makes it easier to move your database to a different server. Like a login, a contained database user can use either Windows authentication or [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] authentication. For more information, see [Contained Database Users - Making Your Database Portable](../../../relational-databases/security/contained-database-users-making-your-database-portable.md).

There are 12 types of users with slight differences in how they authenticate, and who they represent. To see a list of users, see [CREATE USER (Transact-SQL)](../../../t-sql/statements/create-user-transact-sql.md).

#### Fixed database roles

Fixed database roles are a set of pre-configured roles that provide convenient group of database-level permissions. Database users and user-defined database roles can be added to the fixed database roles using the `ALTER ROLE ... ADD MEMBER` statement. For more information, see [ALTER ROLE (Transact-SQL)](../../../t-sql/statements/alter-role-transact-sql.md).

#### User-defined database roles

Users with the `CREATE ROLE` permission can create new user-defined database roles to represent groups of users with common permissions. Typically permissions are granted or denied to the entire role, simplifying permissions management and monitoring. Database users can be added to the database roles by using the `ALTER ROLE ... ADD MEMBER` statement. For more information, see [ALTER ROLE (Transact-SQL)](../../../t-sql/statements/alter-role-transact-sql.md).

#### Other principals

Additional security principals not discussed here include application roles, and logins and users based on certificates or asymmetric keys.

For a graphic showing the relationships between Windows users, Windows groups, logins, and database users, see [Create a Database User](../../../relational-databases/security/authentication-access/create-a-database-user.md).

## Typical scenario

The following example represents a common and recommended method of configuring permissions.

#### In Windows Active Directory or Azure Active Directory

1. Create a Windows user for each person.

1. Create Windows groups that represent the work units and the work functions.

1. Add the Windows users to the Windows groups.

#### If the person connecting will be connecting to many databases

1. Create a login for the Windows groups. (If using [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] authentication, skip the Active Directory steps, and create [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] authentication logins here.)

1. In the user database, create a database user for the login representing the Windows groups.

1. In the user database, create one or more user-defined database roles, each representing a similar function. For example financial analyst, and sales analyst.

1. Add the database users to one or more user-defined database roles.

1. Grant permissions to the user-defined database roles.

#### If the person connecting will be connecting to only one database

1. In the user database, create a contained database user for the Windows group. (If using [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] authentication, skip the Active Directory steps, and create contained database user [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] authentication here.

1. In the user database, create one or more user-defined database roles, each representing a similar function. For example financial analyst, and sales analyst.

1. Add the database users to one or more user-defined database roles.

1. Grant permissions to the user-defined database roles.

The typical result at this point is that a Windows user is a member of a Windows group. The Windows group has a login in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] or [!INCLUDE[ssSDS](../../../includes/sssds-md.md)]. The login is mapped to a user identity in the user-database. The user is a member of a database role. Now you need to add permissions to the role.

## Assign permissions

Most permission statements have the format:

```sql
AUTHORIZATION PERMISSION ON SECURABLE::NAME TO PRINCIPAL;
```

- `AUTHORIZATION` must be `GRANT`, `REVOKE` or `DENY`.

- The `PERMISSION` establishes what action is allowed or prohibited. The exact number of permissions differs between SQL Server and SQL Database. The permissions are listed in the article [Permissions &#40;Database Engine&#41;](../../../relational-databases/security/permissions-database-engine.md) and in the chart referenced below.

- `ON SECURABLE::NAME` is the type of securable (server, server object, database, or database object) and its name. Some permissions don't require `ON SECURABLE::NAME` because it is unambiguous or inappropriate in the context. For example, the `CREATE TABLE` permission doesn't require the `ON SECURABLE::NAME` clause (`GRANT CREATE TABLE TO Mary;` allows Mary to create tables).

- `PRINCIPAL` is the security principal (login, user, or role) which receives or loses the permission. Grant permissions to roles whenever possible.

 The following example grant statement, grants the `UPDATE` permission on the `Parts` table or view that is contained in the `Production` schema to the role named `PartsTeam`:

```sql
GRANT UPDATE ON OBJECT::Production.Parts TO PartsTeam;
```

 The following example grant statement grants the `UPDATE` permission on the `Production` schema, and by extension on any table or view contained within this schema to the role named `ProductionTeam`, which is a more effective and salable approach to assigning permissions than on individual object-level:

```sql
GRANT UPDATE ON SCHEMA::Production TO ProductionTeam;
```

Permissions are granted to security principals (logins, users, and roles) by using the `GRANT` statement. Permissions are explicitly denied by using the `DENY` command. A previously granted or denied permission is removed by using the `REVOKE` statement. Permissions are cumulative, with the user receiving all the permissions granted to the user, login, and any group memberships; however any permission denial overrides all grants.

> [!TIP]  
> A common mistake is to attempt to remove a `GRANT` by using `DENY` instead of `REVOKE`. This can cause problems when a user receives permissions from multiple sources; which is quite common. The following example demonstrates the principal.

The Sales group receives `SELECT` permissions on the OrderStatus table through the statement `GRANT SELECT ON OBJECT::OrderStatus TO Sales;`. User Jae is a member of the Sales role. Jae has also been granted `SELECT` permission to the OrderStatus table under their own user name through the statement `GRANT SELECT ON OBJECT::OrderStatus TO Jae;`. Presume the administer wishes to remove the `GRANT` to the Sales role.

- If the administrator correctly executes `REVOKE SELECT ON OBJECT::OrderStatus TO Sales;`, then Jae will retain `SELECT` access to the OrderStatus table through their individual `GRANT` statement.

- If the administrator incorrectly executes `DENY SELECT ON OBJECT::OrderStatus TO Sales;` then Jae, as a member of the Sales role, will be denied the `SELECT` permission because the `DENY` to Sales overrides their individual `GRANT`.

> [!NOTE]  
> Permissions can be configured using [!INCLUDE[ssManStudio](../../../includes/ssmanstudio-md.md)]. Find the securable in Object Explorer, right-click the securable, and then select **Properties**. Select the **Permissions** page. For help on using the permission page, see [Permissions or Securables Page](../../../relational-databases/security/permissions-or-securables-page.md).

## Permission hierarchy

Permissions have a parent/child hierarchy. That is, if you grant `SELECT` permission on a database, that permission includes `SELECT` permission on all (child) schemas in the database. If you grant `SELECT` permission on a schema, it includes `SELECT` permission on all the (child) tables and views in the schema. The permissions are transitive; that is, if you grant `SELECT` permission on a database, it includes `SELECT` permission on all (child) schemas, and all (grandchild) tables and views.

Permissions also have covering permissions. The `CONTROL` permission on an object, normally gives you all other permissions on the object.

Because both the parent/child hierarchy and the covering hierarchy can act on the same permission, the permission system can get complicated. For example, let's take a table (Region), in a schema (Customers), in a database (SalesDB).

- `CONTROL` permission on table Region includes all the other permissions on the table Region, including `ALTER`, `SELECT`, `INSERT`, `UPDATE`, `DELETE`, and some other permissions.

- `SELECT` on the Customers schema that owns the Region table includes the `SELECT` permission on the Region table.

So `SELECT` permission on the Region table can be achieved through any of these six statements:

```sql
GRANT SELECT ON OBJECT::Region TO Jae;

GRANT CONTROL ON OBJECT::Region TO Jae;

GRANT SELECT ON SCHEMA::Customers TO Jae;

GRANT CONTROL ON SCHEMA::Customers TO Jae;

GRANT SELECT ON DATABASE::SalesDB TO Jae;

GRANT CONTROL ON DATABASE::SalesDB TO Jae;
```

## Grant the least permission

The first permission listed above (`GRANT SELECT ON OBJECT::Region TO Jae;`) is the most granular, that is, that statement is the least permission possible that grants the `SELECT`. No permissions to subordinate objects come with it. It's a good principle to always grant the least permission possible (you can read more about the [Principle of Least Privilege](https://techcommunity.microsoft.com/t5/azure-sql/security-the-principle-of-least-privilege-polp/ba-p/2067390)), but at the same time (contradicting that) try to grant at higher levels in order to simplify the granting system. So if Jae needs permissions to the entire schema, grant `SELECT` once at the schema level, instead of granting `SELECT` at the table or view level many times. The design of the database can dramatically affect how successful this strategy can be. This strategy will work best when your database is designed so that objects needing identical permissions are included in a single schema.

> [!TIP]  
> When designing a database and its objects, from the beginning, plan who or which applications will access which objects and based on that place objects, namely tables but also views, functions and stored procedures in schemas according to buckets of access type as much as possible. You can read more about this approach in this blog post by Andreas Wolter [Schema-design for SQL Server: recommendations for Schema design with security in mind](http://andreas-wolter.com/en/schema-design-for-sql-server-recommendations-for-schema-design-with-security-in-mind/).

## Diagram of permissions

[!INCLUDE[database-engine-permissions](../../../includes/paragraph-content/database-engine-permissions.md)]

For a graphic showing the relationships among the [!INCLUDE[ssDE](../../../includes/ssde-md.md)] principals and server and database objects, see [Permissions Hierarchy &#40;Database Engine&#41;](../../../relational-databases/security/permissions-hierarchy-database-engine.md).

## Permissions vs. fixed server and fixed database roles

 The permissions of the fixed server roles and fixed database roles are similar but not exactly the same as the granular permissions. For example, members of the `sysadmin` fixed server role have all permissions on the instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], as do logins with the `CONTROL SERVER` permission. But granting the `CONTROL SERVER` permission doesn't make a login a member of the sysadmin fixed server role, and adding a login to the `sysadmin` fixed server role doesn't explicitly grant the login the `CONTROL SERVER` permission. Sometimes a stored procedure will check permissions by checking the fixed role and not checking the granular permission. For example, detaching a database requires membership in the `db_owner` fixed database role. The equivalent `CONTROL DATABASE` permission isn't enough. These two systems operate in parallel but rarely interact with each other. Microsoft recommends using the newer, granular permission system instead of the fixed roles whenever possible.

## Monitor permissions

 The following views return security information.

- The logins and user-defined server roles on a server can be examined by using the `sys.server_principals` view. This view isn't available in [!INCLUDE[ssSDS](../../../includes/sssds-md.md)].

- The users and user-defined roles in a database can be examined by using the `sys.database_principals` view.

- The permissions granted to logins and user-defined fixed server roles can be examined by using the `sys.server_permissions` view. This view isn't available in [!INCLUDE[ssSDS](../../../includes/sssds-md.md)].

- The permissions granted to users and user-defined fixed database roles can be examined by using the `sys.database_permissions` view.

- Database role membership can be examined by using the `sys. sys.database_role_members` view.

- Server role membership can be examined by using the `sys.server_role_members` view. This view isn't available in [!INCLUDE[ssSDS](../../../includes/sssds-md.md)].

- For additional security related views, see [Security Catalog Views (Transact-SQL)](../../../relational-databases/system-catalog-views/security-catalog-views-transact-sql.md) .

## Examples

 The following statements return useful information about permissions.

### A. List of database-permissions for each user

To return the explicit permissions granted or denied in a database ([!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] and [!INCLUDE[ssSDS](../../../includes/sssds-md.md)]), execute the following statement in the database.

```sql
SELECT
    perms.state_desc AS State,
    permission_name AS [Permission],
    obj.name AS [on Object],
    dp.name AS [to User Name]
FROM sys.database_permissions AS perms
JOIN sys.database_principals AS dp
    ON perms.grantee_principal_id = dp.principal_id
JOIN sys.objects AS obj
    ON perms.major_id = obj.object_id;
```

### B. List server-role members

To return the members of the server roles ([!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] only), execute the following statement.

```sql
SELECT roles.principal_id AS RolePrincipalID,
    roles.name AS RolePrincipalName,
    server_role_members.member_principal_id AS MemberPrincipalID,
    members.name AS MemberPrincipalName
FROM sys.server_role_members AS server_role_members
INNER JOIN sys.server_principals AS roles
    ON server_role_members.role_principal_id = roles.principal_id
LEFT JOIN sys.server_principals AS members
    ON server_role_members.member_principal_id = members.principal_id;
```

### C. List all database-principals that are members of a database-level role

To return the members of the database roles ([!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] and [!INCLUDE[ssSDS](../../../includes/sssds-md.md)]), execute the following statement in the database.

```sql
SELECT dRole.name AS [Database Role Name], dp.name AS [Members]
FROM sys.database_role_members AS dRo
JOIN sys.database_principals AS dp
    ON dRo.member_principal_id = dp.principal_id
JOIN sys.database_principals AS dRole
    ON dRo.role_principal_id = dRole.principal_id;
```

## See also

- [Security Center for SQL Server Database Engine and Azure SQL Database](../../../relational-databases/security/security-center-for-sql-server-database-engine-and-azure-sql-database.md)
- [Security Functions (Transact-SQL)](../../../t-sql/functions/security-functions-transact-sql.md)
- [Security-Related Dynamic Management Views and Functions (Transact-SQL)](../../../relational-databases/system-dynamic-management-views/security-related-dynamic-management-views-and-functions-transact-sql.md)
- [Security Catalog Views (Transact-SQL)](../../../relational-databases/system-catalog-views/security-catalog-views-transact-sql.md)
- [sys.fn_builtin_permissions (Transact-SQL)](../../../relational-databases/system-functions/sys-fn-builtin-permissions-transact-sql.md)
- [Determining Effective Database Engine Permissions](../../../relational-databases/security/authentication-access/determining-effective-database-engine-permissions.md)

## Next steps

- [Tutorial: Getting Started with the Database Engine](../../../relational-databases/tutorial-getting-started-with-the-database-engine.md)
- [Creating a Database &#40;Tutorial&#41;](../../../t-sql/lesson-1-creating-database-objects.md)
- [Tutorial: SQL Server Management Studio](../../../ssms/quickstarts/ssms-connect-query-sql-server.md)
- [Tutorial: Writing Transact-SQL Statements](../../../t-sql/tutorial-writing-transact-sql-statements.md)
