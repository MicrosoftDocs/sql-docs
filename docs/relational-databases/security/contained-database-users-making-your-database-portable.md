---
title: "Contained user access to contained databases"
description: Learn how to configure contained user access for contained databases, and the differences from a traditional login/user model.
author: VanMSFT
ms.author: vanto
ms.date: "09/14/2023"
ms.service: sql
ms.subservice: security
ms.topic: conceptual
helpviewer_keywords:
  - "contained database, users"
  - "user [SQL Server], about contained database users"
monikerRange: "=azuresqldb-current||>=sql-server-2016||=azure-sqldw-latest||>=sql-server-linux-2017||=azuresqldb-mi-current"
---

# Make your database portable by using contained databases

[!INCLUDE [SQL Server ASDB, ASDBMI, ASDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa.md)]

Use contained database users to authenticate [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and [!INCLUDE[ssazure-sqldb](../../includes/ssazure-sqldb.md)] connections at the database level. A contained database is a database that's isolated from other databases and from the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or [!INCLUDE[ssSDS](../../includes/sssds-md.md)] (and the `master` database) that hosts the database.

[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] supports contained database users for both Windows and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] authentication. When you're using [!INCLUDE[ssSDS](../../includes/sssds-md.md)], combine contained database users with database-level firewall rules.

This article reviews the benefits of using the contained database model compared to the traditional login/user model and Windows or server-level firewall rules. Specific scenarios, manageability, or application business logic might still require use of the traditional login/user model and server-level firewall rules.


## Traditional login and user model

In the traditional connection model, Windows users or members of Windows groups connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)] by providing user or group credentials authenticated by Windows. Or users can provide both a name and password and connect by using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] authentication. In both cases, the master database must have a login that matches the connecting credentials.

After the [!INCLUDE[ssDE](../../includes/ssde-md.md)] confirms the Windows authentication credentials or authenticates the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] authentication credentials, the connection typically attempts to connect to a user database. To connect to a user database, the login must be mapped to (that is, associated with) a database user in the user database. The connection string might also specify connecting to a specific database, which is optional in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] but required in [!INCLUDE[ssSDS](../../includes/sssds-md.md)].

The important principle is that both the login (in the `master` database) and the user (in the user database) must exist and be related to each other. The connection to the user database has a dependency upon the login in the `master` database. This dependency limits the ability of the database to be moved to a different hosting [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance or [!INCLUDE[ssazure-sqldb](../../includes/ssazure-sqldb.md)] server.

If a connection to the `master` database is not available (for example, a failover is in progress), the overall connection time will increase, or the connection might time out. An unavailable connection might reduce connection scalability.

## Contained database user model

In the contained database user model, the login in the `master` database is not present. Instead, the authentication process occurs at the user database. The database user in the user database doesn't have an associated login in the `master` database.

The contained database user model supports both Windows authentication and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] authentication. You can use it in both [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and [!INCLUDE[ssSDS](../../includes/sssds-md.md)].

To connect as a contained database user, the connection string must always contain a parameter for the user database. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] uses this parameter to know which database is responsible for managing the authentication process.

The activity of the contained database user is limited to the authenticating database. The database user account must be independently created in each database that the user needs. To change databases, [!INCLUDE[ssSDS](../../includes/sssds-md.md)] users must create a new connection. Contained database users in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can change databases if an identical user is present in another database.

In Azure, [!INCLUDE[sssds](../../includes/sssds-md.md)] and [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] support identities from Microsoft Entra ID ([formerly Azure Active Directory](/entra/fundamentals/new-name)) as contained database users. [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)] supports contained database users who use [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] authentication, but [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] doesn't. For more information, see [Connect to SQL Database by using Microsoft Entra authentication](/azure/azure-sql/database/authentication-aad-overview).

When you're using Microsoft Entra authentication, users can make connections from SQL Server Management Studio by using Microsoft Entra universal authentication. Administrators can configure universal authentication to require multifactor authentication, which verifies identity by using a phone call, text message, smart card with PIN, or mobile app notification. For more information, see [Using Microsoft Entra multifactor authentication](/azure/azure-sql/database/authentication-mfa-ssms-overview).

For [!INCLUDE[ssSDS](../../includes/sssds-md.md)] and [!INCLUDE[ssazuresynapse-md](../../includes/ssazuresynapse-md.md)], the database name is always required in the connection string. So you don't need to change the connection string when you're switching from the traditional model to the contained database user model. For [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] connections, the name of the database must be added to the connection string, if it's not already present.

> [!IMPORTANT]
> When you're using the traditional model, the server-level roles and server-level permissions can limit access to all databases. When you're using the contained database model, database owners and database users who have the ALTER ANY USER permission can grant access to the database. This permission reduces the access control of highly privileged server logins and expands the access control to include highly privileged database users.

## Firewalls

### [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]

For SQL Server, Windows Firewall rules apply to all connections and have the same effects on logins (traditional model connections) and contained database users. For more information about Windows Firewall, see [Configure Windows Firewall for Database Engine access](../../database-engine/configure-windows/configure-a-windows-firewall-for-database-engine-access.md).

### [!INCLUDE[ssSDS](../../includes/sssds-md.md)] firewalls

[!INCLUDE[ssSDS](../../includes/sssds-md.md)] allows separate firewall rules for server-level connections (logins) and for database-level connections (contained database users). When [!INCLUDE[ssSDS](../../includes/sssds-md.md)] connects to a user database, it first checks database firewall rules. If there's no rule that allows access to the database, [!INCLUDE[ssSDS](../../includes/sssds-md.md)] checks the server-level firewall rules. Checking server-level firewall rules requires access to the [!INCLUDE[ssSDS](../../includes/sssds-md.md)] server's `master` database.

Database-level firewall rules, combined with contained database users, can eliminate the need to access the `master` database of the server during the connection. The result is improved connection scalability.

For more information about [!INCLUDE[ssSDS](../../includes/sssds-md.md)] firewall rules, see the following topics:

- [Azure SQL Database firewall](/previous-versions/azure/ee621782(v=azure.100))
- [Configure firewall settings (Azure SQL Database)](/previous-versions/azure/jj553530(v=azure.100))
- [sp_set_firewall_rule (Azure SQL Database)](../../relational-databases/system-stored-procedures/sp-set-firewall-rule-azure-sql-database.md)
- [sp_set_database_firewall_rule (Azure SQL Database)](../../relational-databases/system-stored-procedures/sp-set-database-firewall-rule-azure-sql-database.md)

## Syntax differences

|Traditional model|Contained database user model|
|-----------------------|-----------------------------------|
|When you're connected to the `master` database:<br /><br /> `CREATE LOGIN login_name  WITH PASSWORD = 'strong_password';`<br /><br /> Then, when you're connected to a user database:<br /><br /> `CREATE USER 'user_name' FOR LOGIN 'login_name';`|When you're connected to a user database:<br /><br /> `CREATE USER user_name  WITH PASSWORD = 'strong_password';`|

|Traditional model|Contained database user model|
|-----------------------|-----------------------------------|
|To change a password in the context of the `master` database:<br /><br /> `ALTER LOGIN login_name  WITH PASSWORD = 'strong_password';`|To change a password in the context of the user database:<br /><br /> `ALTER USER user_name  WITH PASSWORD = 'strong_password';`|

### SQL Managed Instance

Azure SQL Managed Instance behaves like [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on-premises in the context of contained databases. Be sure to change the context of your database from the master database to the user database when you're creating your contained user. Additionally, there should be no active connections to the user database when you're setting the containment option. Use the following code as a guide.

> [!WARNING]
> The following sample script uses a `kill` statement to close all user processes on the database. Make sure that you understand the consequences of this script and that it fits your business before running it. Also make sure that no other connections are active on your SQL Managed Instance database, because the script will disrupt other processes that are running on the database.

```sql
USE master;

SELECT * FROM sys.dm_exec_sessions
WHERE database_id  = db_id('Test')

DECLARE @kill_string varchar(8000) = '';
SELECT @kill_string = @kill_string + 'KILL ' + str(session_id) + '; '  
FROM sys.dm_exec_sessions
WHERE database_id  = db_id('Test') and is_user_process = 1;

EXEC(@kill_string);
GO

sp_configure 'contained database authentication', 1;  
GO
 
RECONFIGURE;  
GO 

SELECT * FROM sys.dm_exec_sessions
WHERE database_id  = db_id('Test')

ALTER DATABASE Test
SET containment=partial

USE Test;  
GO 

CREATE USER Carlo  
WITH PASSWORD='Enterpwdhere*'  

SELECT containment_desc FROM sys.databases
WHERE name='Test'
```

## Remarks

- Contained database users must be enabled for each instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [Contained database authentication (server configuration option)](../../database-engine/configure-windows/contained-database-authentication-server-configuration-option.md).
- Contained database users and logins with non-overlapping names can coexist in your applications.
- Assume that a login in the `master` database has the name **name1**. If you create a contained database user named **name1**, when a database name is provided in the connection string, the context of the database user will be chosen over the login context for connecting to the database. That is, the contained database user takes precedence over logins that have the same name.
- In [!INCLUDE[ssSDS](../../includes/sssds-md.md)], the name of contained database user can't be the same as the name of the server admin account.
- The [!INCLUDE[ssSDS](../../includes/sssds-md.md)] server admin account can never be a contained database user. The server admin has sufficient permissions to create and manage contained database users. The server admin can grant permissions to contained database users on user databases.
- Because contained database users are database-level principals, you need to create contained database users in every database where you would use them. The identity is confined to the database. The identity is independent (in all aspects) from a user who has the same name and the same password in another database in the same server.
- Use the same strength of passwords that you would normally use for logins.

## Related content

- [Contained databases](../../relational-databases/databases/contained-databases.md)
- [Security best practices with contained databases](../../relational-databases/databases/security-best-practices-with-contained-databases.md)
- [CREATE USER (Transact-SQL)](../../t-sql/statements/create-user-transact-sql.md)
- [Connect to Azure SQL Database by using Microsoft Entra authentication](/azure/azure-sql/database/authentication-aad-overview)
