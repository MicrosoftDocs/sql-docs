---
title: "Principals (Database Engine)"
description: Learn about principals in Database Engine, which are entities that can request SQL Server resources. There are SQL Server-level and database-level principals.
author: VanMSFT
ms.author: vanto
ms.date: 09/12/2025
ms.service: sql
ms.subservice: security
ms.topic: concept-article
ms.custom:
  - ignite-2025
f1_keywords:
  - "sql13.swb.roleproperties.selectroll.f1"
  - "sql13.swb.databaseuser.permissions.user.f1--May use common.permissions"
helpviewer_keywords:
  - "certificates [SQL Server], principals"
  - "roles [SQL Server], principals"
  - "permissions [SQL Server], principals"
  - "##MS_SQLAuthenticatorCertificate##"
  - "principals [SQL Server]"
  - "##MS_SQLResourceSigningCertificate##"
  - "groups [SQL Server], principals"
  - "##MS_AgentSigningCertificate##"
  - "authentication [SQL Server], principals"
  - "schemas [SQL Server], principals"
  - "principals [SQL Server], about principals"
  - "security [SQL Server], principals"
  - "users [SQL Server], principals"
  - "##MS_SQLReplicationSigningCertificate##"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# Principals (Database Engine)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance Azure Synapse Analytics PDW FabricSQLDB](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricsqldb.md)]

*Principals* are entities that can request [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] resources. Like other components of the [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] authorization model, principals can be arranged in a hierarchy. The scope of influence of a principal depends on the scope of the definition of the principal: Windows, server, database; and whether the principal is indivisible or a collection. A Windows Login is an example of an indivisible principal, and a Windows Group is an example of a principal that is a collection. Every principal has a security identifier (`SID`). This topic applies to all versions of SQL Server, but there are some restrictions on server-level principals in SQL Database or Azure Synapse Analytics.

[!INCLUDE [entra-id](../../../includes/entra-id.md)]

## SQL Server-level principals

- [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] authentication Login
- Windows authentication login for a Windows user
- Windows authentication login for a Windows group
- Microsoft Entra authentication login for a Microsoft Entra user
- Microsoft Entra authentication login for a Microsoft Entra group
- Microsoft Entra authentication login for a Microsoft Entra service principal
- Server Role

## Database-level principals

- Database User (For more information on types of database users, see [CREATE USER (Transact-SQL)](../../../t-sql/statements/create-user-transact-sql.md).)
- Database Role
- Application Role

## `sa` Login

The [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] `sa` login is a server-level principal. By default, it's created when an instance is installed. Beginning in [!INCLUDE [ssVersion2005](../../../includes/ssversion2005-md.md)], the default database of `sa` is `master`. This is a change of behavior from earlier versions of [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)]. The `sa` login is a member of the `sysadmin` fixed server-level role. The `sa` login has all permissions on the server and can't be limited. The `sa` login can't be dropped, but it can be disabled so that no one can use it.

## `dbo` User and `dbo` Schema

The `dbo` user is a special user principal in each database. All SQL Server administrators, members of the `sysadmin` fixed server role, `sa` login, and owners of the database, enter databases as the `dbo` user. The `dbo` user has all permissions in the database and can't be limited or dropped. `dbo` stands for database owner, but the `dbo` user account isn't the same as the `db_owner` fixed database role, and the `db_owner` fixed database role isn't the same as the user account that is recorded as the owner of the database.
The `dbo` user owns the `dbo` schema. The `dbo` schema is the default schema for all users, unless some other schema is specified.  The `dbo` schema can't be dropped.

## `public` Server Role and Database Role

Every login belongs to the `public` fixed server role, and every database user belongs to the `public` database role. When a login or user hasn't been granted or denied specific permissions on a securable, the login or user inherits the permissions granted to `public` on that securable. The `public` fixed server role and the `public` fixed database role can't be dropped. However you can revoke permissions from the `public` roles. There are many permissions that are assigned to the `public` roles by default. Most of these permissions are needed for routine operations in the database; the type of things that everyone should be able to do. Be careful when revoking permissions from the `public` login or user, as it will affect all logins/users. Generally you shouldn't deny permissions to `public`, because the deny statement overrides any grant statements you might make to individuals.

## `INFORMATION_SCHEMA` and `sys` Users and Schemas

Every database includes two entities that appear as users in catalog views: `INFORMATION_SCHEMA` and `sys`. These entities are required for internal use by the Database Engine. They can't be modified or dropped.

## Certificate-based SQL Server Logins

Server principals with names enclosed by double hash marks (##) are for internal system use only. The following principals are created from certificates when [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] is installed, and shouldn't be deleted.

- \##MS_SQLResourceSigningCertificate##
- \##MS_SQLReplicationSigningCertificate##
- \##MS_SQLAuthenticatorCertificate##
- \##MS_AgentSigningCertificate##
- \##MS_PolicyEventProcessingLogin##
- \##MS_PolicySigningCertificate##
- \##MS_PolicyTsqlExecutionLogin##

These principal accounts don't have passwords that can be changed by administrators as they're based on certificates issued to Microsoft.

## The `guest` User

Each database includes a `guest`. Permissions granted to the `guest` user are inherited by users who have access to the database, but who don't have a user account in the database. The `guest` user can't be dropped, but it can be disabled by revoking its `CONNECT` permission. The `CONNECT` permission can be revoked by executing `REVOKE CONNECT FROM GUEST;` within any database other than `master` or `tempdb`.

## Limitations

- In [!INCLUDE [fabric-sqldb](../../../includes/fabric-sqldb.md)], only database-level users and roles are supported. Server-level logins, roles, and the sa account aren't available. In [!INCLUDE [fabric-sqldb](../../../includes/fabric-sqldb.md)], Microsoft Entra ID for database users is the only supported authentication method. For more information, see [Authorization in SQL database in Microsoft Fabric](/fabric/database/sql/authorization).

## Related tasks

For information about designing a permissions system, see [Get started with Database Engine permissions](getting-started-with-database-engine-permissions.md).

The following articles are included in this section of [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] Books Online:

- [Create a login](create-a-login.md)

- [Server-level roles](server-level-roles.md)

- [Database-level roles](database-level-roles.md)

- [Application Roles](application-roles.md)

## Related content

- [Securing SQL Server](../securing-sql-server.md)
- [sys.database_principals (Transact-SQL)](../../system-catalog-views/sys-database-principals-transact-sql.md)
- [sys.server_principals (Transact-SQL)](../../system-catalog-views/sys-server-principals-transact-sql.md)
- [sys.sql_logins (Transact-SQL)](../../system-catalog-views/sys-sql-logins-transact-sql.md)
- [sys.database_role_members (Transact-SQL)](../../system-catalog-views/sys-database-role-members-transact-sql.md)
- [Server-level roles](server-level-roles.md)
- [Database-level roles](database-level-roles.md)
