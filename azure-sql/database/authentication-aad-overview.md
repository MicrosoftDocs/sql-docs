---
title: Microsoft Entra authentication
titleSuffix: Azure SQL Database & Azure SQL Managed Instance & Azure Synapse Analytics
description: Learn about how to use Microsoft Entra ID for authentication with Azure SQL Database, Azure SQL Managed Instance, and Synapse SQL in Azure Synapse Analytics
author: nofield
ms.author: nofield
ms.reviewer: wiassaf, vanto, mathoma, randolphwest
ms.date: 09/27/2024
ms.service: azure-sql
ms.subservice: security
ms.topic: conceptual
ms.custom:
  - azure-synapse
  - sqldbrb=1
monikerRange: "=azuresql || =azuresql-db || =azuresql-mi"
---
# Microsoft Entra authentication for Azure SQL

[!INCLUDE [appliesto-sqldb-sqlmi-asa](../includes/appliesto-sqldb-sqlmi-asa.md)]

This article provides an in depth overview of using Microsoft Entra authentication with [Azure SQL Database](sql-database-paas-overview.md), [Azure SQL Managed Instance](../managed-instance/sql-managed-instance-paas-overview.md), [SQL Server on Azure VMs](../virtual-machines/windows/sql-server-on-azure-vm-iaas-what-is-overview.md), [Synapse SQL in Azure Synapse Analytics](/azure/synapse-analytics/sql-data-warehouse/sql-data-warehouse-overview-what-is) and [SQL Server for Windows and Linux](/sql/relational-databases/security/authentication-access/azure-ad-authentication-sql-server-overview).

If you want to configure Microsoft Entra authentication, review: 
- [Azure SQL Database and Azure SQL Managed Instance](authentication-aad-configure.md)
- [SQL Server on Azure VMs](../virtual-machines/windows/configure-azure-ad-authentication-for-sql-vm.md)

[!INCLUDE [entra-id](../includes/entra-id.md)]

## Overview

Microsoft Entra ID allows you to centrally manage the identities of humans and services in your data estate. By integrating Microsoft Entra with Azure SQL for authentication, you can simplify identity and permission management while also enabling detailed conditional access and governance over all connections to your data.

Using Microsoft Entra authentication includes the following benefits:

- Replaces less secure authentication methods like usernames and passwords.
- Eliminates, or helps stop, the proliferation of user identities across servers.
- Microsoft Entra groups enable database permission management to be abstracted away from individual accounts and into operational groups.
- Allows password rotation in a single place.
- Microsoft Entra-only authentication provides a complete alternative to SQL authentication.
- Managed identities for Azure resources eliminate the need to store passwords for services that connect to your databases, and connections from your databases to other Azure resources.
- Enables modern security controls including strong multifactor authentication with a range of easy verification options, such as a phone call, text message, smart card with pin, or mobile app notification.
- Microsoft Entra ID enables [integration with many modern authentication protocols](/entra/architecture/auth-sync-overview), including OpenID Connect, OAuth2.0, Kerberos Constrained Delegation, and more.
- Enables centralized monitoring of connections to data sources.
- Enables conditional access controls, such as requiring compliant devices or authentication methods for successful connections.
- Centrally manage and monitor authentication with [Azure Policies](/azure/azure-sql/database/authentication-azure-ad-only-authentication-policy).

> [!NOTE]  
> Microsoft Entra authentication only supports access tokens that originated from Microsoft Entra ID, and not third-party access tokens. Microsoft Entra ID also doesn't support redirecting Microsoft Entra ID queries to third-party endpoints. This applies to all SQL platforms and all operating systems that support Microsoft Entra authentication.

### Configuration steps

The general steps to configure Microsoft Entra authentication are:

1. Create and populate a Microsoft Entra tenant.
1. Create a logical server or instance in Azure.
1. Assign a Microsoft Entra administrator to the server or instance.
1. Create SQL principals in your database that are mapped to Microsoft Entra identities.
1. Configure your client applications to connect using Azure Identity libraries and authentication methods.
1. Connect to your database with Microsoft Entra identities.

## Supported identities and authentication methods

Azure SQL supports using the following Microsoft Entra identities as logins and users (principals) in your servers and databases:

  - **Microsoft Entra users**: Any [type of user](/entra/fundamentals/how-to-create-delete-users#types-of-users) in a Microsoft Entra tenant, which includes internal users, external users, guests, and members. Members of an Active Directory domain [federated with Microsoft Entra ID](/entra/identity/hybrid/connect/whatis-fed) are also supported, and can be configured for [seamless single sign-on](/entra/identity/hybrid/connect/how-to-connect-sso).
  - **Applications**: applications that exist in Azure can use service principals or managed identities to authenticate directly to Azure SQL. Using managed identities is preferable since authentication is passwordless and eliminates the need for developer-managed credentials.
  - **Microsoft Entra groups**, which can simplify access management across your organization by managing user and application access based on group membership.

For *user identities*, the following authentication methods are supported:

- **Microsoft Entra Integrated (Windows Authentication)** supported by Microsoft Entra Hybrid Identities with Active Directory [federation].
- **Microsoft Entra MFA**, or multifactor authentication, which requires additional security checks beyond the user's knowledge.
- **Microsoft Entra Password** authentication, which uses user credentials stored and managed in Microsoft Entra ID.
- **Microsoft Entra Default** authentication, which scans various credential caches on the application's machine, and can use user tokens to authenticate to SQL.

For *service or workload identities*, the following authentication methods are supported:

- **Managed identities** for Azure resources, both user-assigned and system-assigned. Managed identity authentication is token-based, in which the identity is assigned to the resource that wants to authenticate using it. The Azure Identity platform validates that relationship, which enables passwordless authentication.
- **Microsoft Entra service principal name and application (client) secret**. This authentication method isn't recommended because of the risk associated with passwords that can be guessed and leaked.
- **Microsoft Entra Default** authentication, which scans various credential caches on the application's machine, and can use application tokens to authenticate to SQL.

## Microsoft Entra administrator

To enable Microsoft Entra authentication, a Microsoft Entra administrator has to be set for your logical server or managed instance. This admin exists alongside the SQL Server administrator (SA). The Microsoft Entra admin can be any one security object in your Azure tenant, including Microsoft Entra users, groups, service principals, and managed identities. The Microsoft Entra administrator is a singular property, not a list, meaning only one identity can be configured at any time. Removing the Microsoft Entra admin from the server disables all Microsoft Entra authentication-based connections, even for existing Microsoft Entra users with permissions in a database.

> [!TIP]  
> Microsoft Entra groups enables multiple identities to act as the Microsoft Entra administrator on the server. When the administrator is set to a group, all group members inherit the Microsoft Entra administrator role. A Microsoft Entra group admin enhances manageability by shifting admin management from server data plane actions into Microsoft Entra ID and the hands of the group owners. Groups can be used for all Microsoft Entra identities that connect to SQL, allowing for onetime user and permission configuration in the server and databases, leaving all user management to the groups.

The Microsoft Entra admin plays a special role: it's the first account that can create other Microsoft Entra logins (in preview in SQL Database) and users, collectively referred to as principals. The admin is a contained database user in the `master` database of the server. Administrator accounts are members of the **db_owner** role in every user database, and each user database is entered as the **dbo** user. For more information about administrator accounts, see [Managing Databases and Logins](logins-create-manage.md).

## Microsoft Entra principals

> [!NOTE]
> [Microsoft Entra server principals (logins)](authentication-azure-ad-logins.md) are currently in public preview for Azure SQL Database and Azure Synapse Analytics. Microsoft Entra logins are generally available for Azure SQL Managed Instance and SQL Server 2022.

Microsoft Entra identities can be created as principals in Azure SQL in three ways:

- as server principals or [logins](authentication-azure-ad-logins.md) (in preview for Azure SQL Database)
- as login-based users (a type of database principal)
- as contained database users

> [!IMPORTANT]  
> Microsoft Entra authentication for Azure SQL doesn't integrate with Azure RBAC. Using Microsoft Entra identities to connect to Azure SQL and execute queries requires those identities to be created as Microsoft Entra principals in the database(s) they need to access. The `SQL Server Contributor` and `SQL DB Contributor` roles are used to secure management-related deployment operations, not database connectivity access.

### Logins (server principals)

[Server principals (logins) for Microsoft Entra identities](authentication-azure-ad-logins.md) are generally available for Azure SQL Managed Instance, SQL Server 2022, and SQL Server on Azure VMs. Microsoft Entra logins are in preview for Azure SQL Database.

The following T-SQL shows how to create a Microsoft Entra login:

```sql
CREATE LOGIN [MSEntraUser] FROM EXTERNAL PROVIDER
```

A Microsoft Entra login has the following property values in [sys.server_principals](/sql/relational-databases/system-catalog-views/sys-server-principals-transact-sql):

| Property | Value |
| --- | --- |
| SID (Security Identifier) | Binary representation of the Microsoft Entra identity's object ID |
| type | E = External login or application from Microsoft Entra ID<br />X = External group from Microsoft Entra ID |
| type_desc | EXTERNAL_LOGIN for Microsoft Entra login or app<br />EXTERNAL_GROUP for Microsoft Entra group |

### Login-based users

Login-based users inherit the server-level roles and permissions assigned to its Microsoft Entra login. Microsoft Entra login-based users are in preview for Azure SQL Database.

The following T-SQL shows how to create a login-based user for a Microsoft Entra identity:

```sql
CREATE USER [MSEntraUser] FROM LOGIN [MSEntraUser]
```

The following table details the Microsoft Entra login-based user property values in [sys.database_principals](/sql/relational-databases/system-catalog-views/sys-database-principals-transact-sql):

| Property | Value |
| --- | --- |
| SID (Security Identifier) | Binary representation of the Microsoft Entra identity's object ID, plus 'AADE' |
| type | E = External login or application from Microsoft Entra ID<br />X = External group from Microsoft Entra ID |
| type_desc | EXTERNAL_LOGIN for Microsoft Entra login or app<br />EXTERNAL_GROUP for Microsoft Entra group |

### Contained database users

Contained database users are portable with the database. They have no connections to identities defined in the server or instance, and thus can be easily moved along with the database from one server or instance to another without disruption.

The following T-SQL shows how to create a contained database user for a Microsoft Entra identity:

```sql
CREATE USER [MSEntraUser] FROM EXTERNAL PROVIDER
```

A Microsoft Entra database-based user has the same property values as login-based users in [sys.database_principals](/sql/relational-databases/system-catalog-views/sys-database-principals-transact-sql), except for how the SID is constructed:

| Property | Value |
| --- | --- |
| SID (Security Identifier) | Binary representation of the Microsoft Entra identity's object ID |
| type | E = External login or application from Microsoft Entra ID<br />X = External group from Microsoft Entra ID |
| type_desc | EXTERNAL_LOGIN for Microsoft Entra login or app<br />EXTERNAL_GROUP for Microsoft Entra group |

To get the original Microsoft Entra GUID that the SID is based on, use the following T-SQL conversion:

```sql
SELECT CAST(sid AS UNIQUEIDENTIFIER) AS EntraID FROM sys.database_principals
```

> [!CAUTION]  
> It's possible to unintentionally create a contained Microsoft Entra database user with the same name as a Microsoft Entra login at the server or instance level. Since the principals aren't connected to each other, the database user doesn't inherit permissions from the server login, and identities can become conflated in connection requests, resulting in undefined behavior.

Use the following T-SQL query to determine if a database user is a login-based user or a contained database user:

```sql
SELECT CASE
    WHEN CONVERT(VARCHAR(100), sid, 2) LIKE '%AADE' AND len(sid) = 18 THEN 'login-based user'
    ELSE 'contained database user'
    END AS user_type,
    *
FROM sys.database_principals WHERE TYPE = 'E' OR TYPE = 'X'
```

Use the following T-SQL query to view all Microsoft Entra principals in a database:

```sql
SELECT
  name,
  CAST(sid AS UNIQUEIDENTIFIER) AS EntraID,
  CASE WHEN TYPE = 'E' THEN 'App/User' ELSE 'Group' AS user_type,
  sid
FROM sys.database_principals WHERE TYPE = 'E' OR TYPE = 'X'
```


## Microsoft Entra-only authentication

With Microsoft Entra-only authentication enabled, all other authentication methods are disabled and can't be used to connect to the server, instance, or database - which includes SA and all other SQL authentication-based accounts for Azure SQL, as well as Windows authentication for Azure SQL Managed Instance.

To get started, review [Configure Microsoft Entra-only authentication](authentication-azure-ad-only-authentication.md).

## Multifactor authentication (MFA)

[Microsoft Entra multifactor authentication](/entra/identity/authentication/concept-mfa-howitworks) is a security feature provided by Microsoft's cloud-based identity and access management service. Multifactor authentication enhances the security of user sign-ins by requiring users to provide extra verification steps beyond a password.

Microsoft Entra multifactor authentication helps safeguard access to data and applications while meeting user demand for a simple sign-in process. MFA adds an extra layer of security to user sign-ins by requiring users to provide two or more authentication factors. These factors typically include something the user knows (password), something the user possesses (smartphone or hardware token), and/or something the user is (biometric data). By combining multiple factors, MFA significantly reduces the likelihood of unauthorized access.

Multifactor authentication is a supported authentication method for [Azure SQL Database](sql-database-paas-overview.md), [Azure SQL Managed Instance](../managed-instance/sql-managed-instance-paas-overview.md), [Azure Synapse Analytics](/azure/synapse-analytics/sql-data-warehouse/sql-data-warehouse-overview-what-is), and [SQL Server 2022 (16.x)](/sql/relational-databases/security/authentication-access/azure-ad-authentication-sql-server-overview) and later versions.

To get started, review [Configure Microsoft Entra multifactor authentication](authentication-aad-configure.md#configure-multifactor-authentication).

## Microsoft Entra B2B support

Microsoft Entra authentication in all SQL products also supports [Microsoft Entra B2B collaboration](/entra/external-id/what-is-b2b), which enables businesses to invite guest users to collaborate with their organization. Guest users can connect to databases either as individual users or members of a Microsoft Entra group. For more information, see [Create guest user](authentication-aad-guest-users.md#create-guest-user-in-sql-database-and-azure-synapse).

## Trust architecture for Microsoft Entra federation to Active Directory

Microsoft Entra ID also integrates with familiar identity and access management solutions like Active Directory. Hybrid joining your on-premises AD enables Windows identities federated through Microsoft Entra ID to use single sign-on credentials to connect to Azure SQL.

For federation, Microsoft Entra ID provides two secure authentication methods: pass-through and password hash authentication. If you're considering federating your on-premises Active Directory to Microsoft Entra ID, review [Choose the right authentication method for your Microsoft Entra hybrid identity solution](/entra/identity/hybrid/connect/choose-ad-authn).

For more information on the setup and synchronization of Microsoft Entra hybrid identities, see the following articles:

- [Implement password hash synchronization with Microsoft Entra Connect Sync](/entra/identity/hybrid/connect/how-to-connect-password-hash-synchronization)
- [Microsoft Entra pass-through authentication](/entra/identity/hybrid/connect/how-to-connect-pta-quick-start)
- [Deploying Active Directory Federation Services in Azure](/windows-server/identity/ad-fs/deployment/how-to-connect-fed-azure-adfs) and [Microsoft Entra Connect and federation](/entra/identity/hybrid/connect/how-to-connect-fed-whatis)

This diagram shows a sample federated authentication with ADFS infrastructure (or user/password for Windows credentials). The arrows indicate communication pathways.

:::image type="content" source="media/authentication-aad-overview/azure-ad-authentication-diagram.png" alt-text="Diagram of Microsoft Entra authentication for Azure SQL.":::

The following diagram indicates the federation, trust, and hosting relationships that allow a client to connect to a database by submitting a token. Microsoft Entra ID authenticates the token, and the database trusts it and validates the issuer and other details. Customer 1 can represent Microsoft Entra ID with native users or Microsoft Entra ID with federated users. Customer 2 represents a possible solution including imported users, in this example coming from a federated Microsoft Entra ID with ADFS being synchronized with Microsoft Entra ID. It's important to understand that access to a database using Microsoft Entra authentication requires that the hosting subscription is associated to the Microsoft Entra ID. The same subscription must be used to create the Azure SQL or Azure Synapse resources.

:::image type="content" source="./media/authentication-aad-overview/2-subscription-relationship.png" alt-text="Diagram shows the relationship between subscriptions in the Microsoft Entra configuration.":::

## Permissions

Permissions assigned to the Microsoft Entra admin are different to the permissions assigned principals in Azure SQL. In a few scenarios, Azure SQL also needs Microsoft Graph permissions to use Microsoft Entra authentication.

### Admin permissions

The Microsoft Entra admin is assigned the following permissions and roles when created:

- **db_owner** of each database on the server or instance

:::image type="content" source="./media/authentication-aad-overview/3-admin-structure.png" alt-text="Diagram shows the administrator structure for Microsoft Entra ID used with SQL Server.":::

### Azure SQL permissions

A principal needs the `ALTER ANY USER` permission in the database to create a user.
By default, `ALTER ANY USER` is given to: server administrator accounts, database users with `CONTROL ON DATABASE`, and members of the `db_owner` database role.

To create a Microsoft Entra principal in Azure SQL, the requesting identity has to query Microsoft Graph for details about the principal. On initial deployment, the only identity possibly capable of querying MS Graph is the Microsoft Entra admin; thus, the admin has to be the first identity to create other Microsoft Entra principals. After that, it can assign `ALTER ANY USER` to other principals to allow them to also create other Microsoft Entra principals.

#### Zero-touch deployments with Microsoft Entra authentication

Since the Microsoft Entra administrator must be the first identity to connect to the database and create other Microsoft Entra users, it can be helpful to add the identity of your deployment infrastructure as the administrator. Your deployments can then do initial setup like creating other Microsoft Entra principals and assigning them permissions. Deployments can use tools like PowerShell ARM templates to script automated principal creation. Azure SQL doesn't support native APIs today to configure user creation and permission management; these operations are only allowed to be done with a direct connection to the SQL instance.

### Microsoft Graph permissions

For creating Microsoft Entra principals and a few other scenarios, Azure SQL needs to make Microsoft Graph calls to retrieve information about, and validate the existence of, the identity in Microsoft Entra ID. In order to do so, the SQL process must have, or obtain, access to MS Graph read permissions within the customer tenant, which is achieved a few ways:

- If the SQL principal executing the command is a user identity, no additional permissions on the SQL instance are required to query MS Graph.
- If the SQL principal executing the command is a service identity, for example a service principal or managed identity, then the Azure SQL instance requires its own permissions to query MS Graph.
   - Application permissions can be assigned to the primary server identity (managed identity) of the logical server or managed instance. The SQL process can use the primary server identity to authenticate to other Azure services in a tenant, such as MS Graph. The following table explains various scenarios and the MS Graph permissions required for the command to execute successfully.

| Scenario | Minimum Permission |
| --- | --- |
| CREATE USER or CREATE LOGIN for a Microsoft Entra service principal or managed identity | Application.Read.All |
| CREATE USER or CREATE LOGIN for a Microsoft Entra user | User.Read.All |
| CREATE USER or CREATE LOGIN for a Microsoft Entra group | GroupMember.Read.All |
| Microsoft Entra authentication with Azure SQL Managed Instance | Directory Readers role assigned to the managed instance identity |

> [!TIP]  
> The Directory Readers role is the smallest-scoped role which can be assigned to an identity that covers all permissions Azure SQL needs. Using roles has the advantage of being able to be assigned to Microsoft Entra security groups, abstracting management away from individual entities and into conceptual groups.

## Tools support

[SQL Server Management Studio (SSMS)](/sql/ssms/download-sql-server-management-studio-ssms) supports a number of Microsoft Entra authentication connection options, including [multifactor authentication](authentication-mfa-ssms-overview.md).

[SQL Server Data Tools (SSDT)](/sql/ssdt/sql-server-data-tools) for Visual Studio, starting with 2015, supports Password, Integrated, and Interactive authentication with Microsoft Entra ID. For more information, see [Microsoft Entra ID support in SQL Server Data Tools (SSDT)](/sql/ssdt/azure-active-directory).
- Currently, Microsoft Entra users aren't shown in SSDT Object Explorer. As a workaround, view the users in [sys.database_principals](/sql/relational-databases/system-catalog-views/sys-database-principals-transact-sql).

### Minimum versions

To use Microsoft Entra authentication with Azure SQL, you need the following minimum versions when using these tools:

- SQL Server Management Studio (SSMS) 18.6 or later
- SQL Server Data Tools for Visual Studio 2015, version 14.0.60311.1 (April 2016) or later
- **.NET Framework Data Provider for SqlServer**, minimum version .NET Framework 4.6
- Beginning with version 15.0.1, [sqlcmd utility](/sql/tools/sqlcmd-utility) and [bcp utility](/sql/tools/bcp-utility) support Active Directory Interactive authentication with multifactor authentication.
- [Microsoft JDBC Driver 6.0 for SQL Server](https://www.microsoft.com/download/details.aspx?id=11774) supports Microsoft Entra authentication. Also, see [Setting the Connection Properties](/sql/connect/jdbc/setting-the-connection-properties).

## Connect with Microsoft Entra to Azure SQL resources

Once Microsoft Entra authentication has been configured for your Azure SQL resource, you can connect to by using [SQL Server Management Studio](authentication-microsoft-entra-connect-to-azure-sql.md#connect-with-ssms-or-ssdt), [SQL Server Data Tools](authentication-microsoft-entra-connect-to-azure-sql.md#connect-with-ssms-or-ssdt), and [a client application](authentication-microsoft-entra-connect-to-azure-sql.md#connect-from-a-client-application).

## Limitations

When using Microsoft Entra authentication with Azure SQL, consider the following limitations:

- Microsoft Entra users and service principals (Microsoft Entra applications) that are members of more than 2048 Microsoft Entra security groups aren't supported and can't log into the database.
- The following system functions aren't supported and return NULL values when executed by Microsoft Entra principals:

  - `SUSER_ID()`
  - `SUSER_NAME(<ID>)`
  - `SUSER_SNAME(<SID>)`
  - `SUSER_ID(<name>)`
  - `SUSER_SID(<name>)`

- We recommend setting the connection timeout to 30 seconds.

### Azure SQL Database and Azure Synapse Analytics

When using Microsoft Entra authentication with Azure SQL Database and Azure Synapse Analytics, consider the following limitations:

- Microsoft Entra users that are part of a group that is member of the `db_owner` database role might see the following error when attempting to use the **[CREATE DATABASE SCOPED CREDENTIAL](/sql/t-sql/statements/create-database-scoped-credential-transact-sql)** syntax against Azure SQL Database and Azure Synapse:

    `SQL Error [2760] [S0001]: The specified schema name 'user@mydomain.com' either doesn't exist or you do not have permission to use it.`

    To mitigate the **CREATE DATABASE SCOPED CREDENTIAL** issue, add the Microsoft Entra user's identity to the `db_owner` role directly.

- Azure SQL Database and Azure Synapse Analytics doesn't create implicit users for users logged in as part of a Microsoft Entra group membership. Because of this, various operations that require assigning ownership can fail, even if the Microsoft Entra group is added as a member to a role with those permissions.

   For example, a user signed into a database via a Microsoft Entra group with the **db_ddladmin** role can't execute CREATE SCHEMA, ALTER SCHEMA, and other object creation statements without a schema explicitly defined (such as table, view, or type, for example). To resolve this, a Microsoft Entra user must be created for that user, or the Microsoft Entra group must be altered to assign a DEFAULT_SCHEMA such as **dbo**.

- When using geo-replication and failover groups, the Microsoft Entra administrator must be configured for both the primary and the secondary servers. If a server doesn't have a Microsoft Entra administrator, then Microsoft Entra logins and users receive a `Cannot connect` error.
- Removing the Microsoft Entra administrator for the server prevents any Microsoft Entra authentication connections to the server. If necessary, a SQL Database administrator can drop unusable Microsoft Entra users manually.

### Azure SQL Managed Instance

When using Microsoft Entra authentication with Azure SQL Managed Instance, consider the following limitations:

- Microsoft Entra server principals (logins) and users are supported for [SQL Managed Instance](../managed-instance/sql-managed-instance-paas-overview.md).
- Setting a Microsoft Entra group login as a database owner isn't supported in [SQL Managed Instance](../managed-instance/sql-managed-instance-paas-overview.md).

   - An extension of this is that when a group is added as part of the `dbcreator` server role, users from this group can connect to the SQL Managed Instance and create new databases, but won't be able to access the database. This is because the new database owner is SA, and not the Microsoft Entra user. This issue doesn't manifest if the individual user is added to the `dbcreator` server role.

- Microsoft Entra server principals (logins) for SQL Managed Instance allows the possibility of creating multiple logins that can be added to the `sysadmin` role.
- SQL Agent management and jobs execution are supported for Microsoft Entra logins.
- Database backup and restore operations can be executed by Microsoft Entra server principals (logins).
- Auditing of all statements related to Microsoft Entra server principals (logins) and authentication events is supported.
- Dedicated administrator connection for Microsoft Entra server principals (logins) which are members of sysadmin server role is supported.
  - Supported through SQLCMD Utility and SQL Server Management Studio.
- Logon triggers are supported for logon events coming from Microsoft Entra server principals (logins).
- Service Broker and DB mail can be setup using a Microsoft Entra server principal (login).
- When using failover groups, the Microsoft Entra administrator must be configured for both the primary and the secondary instances. If an instance doesn't have a Microsoft Entra administrator, then Microsoft Entra logins and users receive a `Cannot connect` error.
- PolyBase can't authenticate using Microsoft Entra authentication.
- Removing the Microsoft Entra administrator for the instance prevents any Microsoft Entra authentication connections to the instance. If necessary, a SQL Managed Instance administrator can drop unusable Microsoft Entra users manually.

## Related content

- [Configure and manage Microsoft Entra authentication with Azure SQL](authentication-aad-configure.md)
- [Tutorial: Secure with Microsoft Entra logins - Azure SQL Managed Instance](../managed-instance/aad-security-configure-tutorial.md)
- [Authorize database access to SQL Database, SQL Managed Instance, and Azure Synapse Analytics](logins-create-manage.md)
- [Database principals](/sql/relational-databases/security/authentication-access/principals-database-engine)
- [Database roles](/sql/relational-databases/security/authentication-access/database-level-roles)
- [CREATE LOGIN syntax for SQL Managed Instance Microsoft Entra server principals (logins)](/sql/t-sql/statements/create-login-transact-sql?view=azuresqldb-mi-current&preserve-view=true)
- [Azure SQL Database and Azure Synapse IP firewall rules](firewall-configure.md)
