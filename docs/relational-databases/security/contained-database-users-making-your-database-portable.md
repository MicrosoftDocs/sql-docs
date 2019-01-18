---
title: "Contained Database Users - Making Your Database Portable | Microsoft Docs"
ms.custom: ""
ms.date: "03/05/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: security
ms.topic: conceptual
helpviewer_keywords: 
  - "contained database, users"
  - "user [SQL Server], about contained database users"
ms.assetid: e57519bb-e7f4-459b-ba2f-fd42865ca91d
author: VanMSFT
ms.author: vanto
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Contained Database Users - Making Your Database Portable
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]

  Use contained database users to authenticate [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and [!INCLUDE[ssSDS](../../includes/sssds-md.md)] connections at the database level. A contained database is a database that is isolated from other databases and from the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]/ [!INCLUDE[ssSDS](../../includes/sssds-md.md)] (and the master database) that hosts the database. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] supports contained database users for both Windows and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] authentication. When using [!INCLUDE[ssSDS](../../includes/sssds-md.md)], combine contained database users with database level firewall rules. This topic reviews the differences and benefits of using the contained database model compared to traditional login/user model and Windows or server-level firewall rules. Specific scenarios, manageability or application business logic may still require use of traditional login/user model and server-level firewall rules.  
  
> [!NOTE]  
>  As [!INCLUDE[msCoName](../../includes/msconame-md.md)] evolves the [!INCLUDE[ssSDS](../../includes/sssds-md.md)] service and moves towards higher guaranteed SLAs you may be required to switch to the contained database user model and database-scoped firewall rules to attain the higher availability SLA and higher max login rates for a given database. [!INCLUDE[msCoName](../../includes/msconame-md.md)] encourage you to consider such changes today.  
  
## Traditional Login and User Model  
 In the traditional connection model, Windows users or members of Windows groups connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)] by providing user or group credentials authenticated by Windows. Or you can provide both a name and password and connects by using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] authentication. In both cases, the master database must have a login that matches the connecting credentials. After the [!INCLUDE[ssDE](../../includes/ssde-md.md)] confirms the Windows authentication credentials or authenticates the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] authentication credentials, the connection typically attempts to connect to a user database. To connect to a user database, the login must be able to be mapped to (that is, associated with) a database user in the user database. The connection string may also specify connecting to a specific database which is optional in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] but required in [!INCLUDE[ssSDS](../../includes/sssds-md.md)].  
  
 The important principal is that both the login (in the master database) and the user (in the user database) must exist and be related to each other. This means that the connection to the user database has a dependency upon the login in the master database, and this limits the ability of the database to be moved to a different hosting [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or [!INCLUDE[ssSDSFull](../../includes/sssdsfull-md.md)] server. And if, for any reason, a connection to the master database is not available (for example, a failover is in progress), the overall connection time will be increased or connection might time out. Consequently this may reduce connection scalability.  
  
## Contained Database User Model  
 In the contained database user model, the login in the master database is not present. Instead, the authentication process occurs at the user database, and the database user in the user database does not have an associated login in the master database. The contained database user model supports both Windows authentication and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] authentication, and can be used in both [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and [!INCLUDE[ssSDS](../../includes/sssds-md.md)]. To connect as a contained database user, the connection string must always contain a parameter for the user database so that the [!INCLUDE[ssDE](../../includes/ssde-md.md)] knows which database is responsible for managing the authentication process. The activity of the contained database user is limited to the authenticating database, so when connecting as a contained database user, the database user account must be independently created in each database that the user will need. To change databases, [!INCLUDE[ssSDS](../../includes/sssds-md.md)] users must create a new connection. Contained database users in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can change databases if an identical user is present in another database.  
  
**Azure:** [!INCLUDE[sqldbesa](../../includes/sqldbesa-md.md)] and [!INCLUDE[ssSDW_md](../../includes/sssdw-md.md)] support Azure Active Directory identities as contained database users. [!INCLUDE[ssSDS_md](../../includes/sssds-md.md)] supports contained database users using [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] authentication, but [!INCLUDE[ssSDW_md](../../includes/sssdw-md.md)] does not. For more information, see [Connecting to SQL Database By Using Azure Active Directory Authentication](https://azure.microsoft.com/documentation/articles/sql-database-aad-authentication/). When using Azure Active Directory authentication, connections from SSMS can be made using Active Directory Universal Authentication.  Administrators can configure Universal Authentication to require Multi-Factor Authentication, which verifies identity by using a phone call, text message, smart card with pin, or mobile app notification. For more information, see [SSMS support for Azure AD MFA with SQL Database and SQL Data Warehouse](https://azure.microsoft.com/documentation/articles/sql-database-ssms-mfa-authentication/).  
  
 For [!INCLUDE[ssSDS](../../includes/sssds-md.md)] and [!INCLUDE[ssSDW_md](../../includes/sssdw-md.md)], since the database name is always required in the connection string, no changes are required to the connection string when switching from the traditional model to the contained database user model. For [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] connections, the name of the database must be added to the connection string, if it is not already present.  
  
> [!IMPORTANT]  
>  When using the traditional model, the server level roles and server level permissions can limit access to all databases. When using the contained database model, database owners and database users with the ALTER ANY USER permission can grant access to the database. This reduces the access control of high privileged server logins and expands the access control to include high privileged database users.  
  
## Firewalls  
  
### [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]  
 Windows firewall rules apply to all connections and have the same effects on logins (traditional model connections) and contained database users. For more information about the Windows firewall, see [Configure a Windows Firewall for Database Engine Access](../../database-engine/configure-windows/configure-a-windows-firewall-for-database-engine-access.md).  
  
### [!INCLUDE[ssSDS](../../includes/sssds-md.md)] Firewalls  
 [!INCLUDE[ssSDS](../../includes/sssds-md.md)] allows separate firewall rules for server level connections (logins) and for database level connections (contained database users). When connecting to a user database, first database firewall rules are checked. If there is no rule that allows access to the database, the server level firewall rules are checked, which requires access to the logical server master database. Database level firewall rules combined with contained database users can eliminate necessity to access master database of the server during connection providing improved connection scalability.  
  
 For more information about [!INCLUDE[ssSDS](../../includes/sssds-md.md)] firewall rules, see the following topics:  
  
-   [Azure SQL Database Firewall](https://msdn.microsoft.com/library/azure/ee621782.aspx)  
  
-   [How to: Configure Firewall Settings (Azure SQL Database)](https://msdn.microsoft.com/library/azure/jj553530.aspx)  
  
-   [sp_set_firewall_rule &#40;Azure SQL Database&#41;](../../relational-databases/system-stored-procedures/sp-set-firewall-rule-azure-sql-database.md)  
  
-   [sp_set_database_firewall_rule &#40;Azure SQL Database&#41;](../../relational-databases/system-stored-procedures/sp-set-database-firewall-rule-azure-sql-database.md)  
  
## Syntax Differences  
  
|Traditional model|Contained database user model|  
|-----------------------|-----------------------------------|  
|When connected to the master database:<br /><br /> `CREATE LOGIN login_name  WITH PASSWORD = 'strong_password';`<br /><br /> Then when connected to a user database:<br /><br /> `CREATE USER 'user_name' FOR LOGIN 'login_name';`|When connected to a user database:<br /><br /> `CREATE USER user_name  WITH PASSWORD = 'strong_password';`|  
  
|Traditional model|Contained database user model|  
|-----------------------|-----------------------------------|  
|To change password, in context of master DB:<br /><br /> `ALTER LOGIN login_name  WITH PASSWORD = 'strong_password';`|To change password, in context of user DB:<br /><br /> `ALTER USER user_name  WITH PASSWORD = 'strong_password';`|  
  
## Remarks  
  
-   In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], contained database users must be enabled for the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For more information, see [contained database authentication Server Configuration Option](../../database-engine/configure-windows/contained-database-authentication-server-configuration-option.md).  
  
-   Contained database users and logins with non-overlapping names can co-exist in your applications.  
  
-   If there is a login in master database with the name **name1** and you create a contained database user named **name1**, when a database name is provided in the connection string, the context of the database user will be picked over login context when connecting to the database. That is, contained database user will take precedence over logins with the same name.  
  
-   In [!INCLUDE[ssSDS](../../includes/sssds-md.md)] the name of contained database user cannot be the same as the name of the server admin account.  
  
-   The [!INCLUDE[ssSDS](../../includes/sssds-md.md)] server admin account can never be a contained database user. The server admin has sufficient permissions to create and manage contained database users. The server admin can grant permissions to contained database users on user databases.  
  
-   Since contained database users are database level principals, you need to create contained database users in every database that you would use them. The identity is confined to the database and is independent in all aspects from a user with same name and same password in another database in the same server.  
  
-   Use the same strength passwords that you would normally use for logins.  
  
## See Also  
 [Contained Databases](../../relational-databases/databases/contained-databases.md)   
 [Security Best Practices with Contained Databases](../../relational-databases/databases/security-best-practices-with-contained-databases.md)   
 [CREATE USER &#40;Transact-SQL&#41;](../../t-sql/statements/create-user-transact-sql.md)   
 [Connecting to SQL Database By Using Azure Active Directory Authentication](https://azure.microsoft.com/documentation/articles/sql-database-aad-authentication/)  
  
  
