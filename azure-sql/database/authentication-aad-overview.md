---
title: Microsoft Entra authentication
titleSuffix: Azure SQL Database & Azure SQL Managed Instance & Azure Synapse Analytics
description: Learn about how to use Microsoft Entra ID for authentication with Azure SQL Database, Azure SQL Managed Instance, and Synapse SQL in Azure Synapse Analytics
author: nofield
ms.author: nofield
ms.reviewer: wiassaf, vanto, mathoma, randolphwest
ms.date: 09/27/2023
ms.service: sql-db-mi
ms.subservice: security
ms.topic: conceptual
ms.custom:
  - azure-synapse
  - sqldbrb=1
monikerRange: "= azuresql || = azuresql-db || = azuresql-mi"
---
# Use Microsoft Entra authentication

[!INCLUDE[appliesto-sqldb-sqlmi-asa](../includes/appliesto-sqldb-sqlmi-asa.md)]

This article provides an overview of using Microsoft Entra ID ([formerly Azure Active Directory](/entra/fundamentals/new-name)) to authenticate to [Azure SQL Database](sql-database-paas-overview.md), [Azure SQL Managed Instance](../managed-instance/sql-managed-instance-paas-overview.md), [SQL Server on Windows Azure VMs](../virtual-machines/windows/sql-server-on-azure-vm-iaas-what-is-overview.md), [Synapse SQL in Azure Synapse Analytics](/azure/synapse-analytics/sql-data-warehouse/sql-data-warehouse-overview-what-is) and [SQL Server for Windows and Linux](/sql/relational-databases/security/authentication-access/azure-ad-authentication-sql-server-overview).

To learn how to create and populate Microsoft Entra ID, and then configure Microsoft Entra ID with Azure SQL Database, Azure SQL Managed Instance, and Synapse SQL in Azure Synapse Analytics, review [Configure Microsoft Entra ID](authentication-aad-configure.md) and [Microsoft Entra ID with SQL Server on Azure VMs](../virtual-machines/windows/configure-azure-ad-authentication-for-sql-vm.md).

[!INCLUDE [entra-id](../includes/entra-id.md)]

## Overview


With Microsoft Entra authentication, you can centrally manage the identities of database users and other Microsoft services in one central location. Central ID management provides a single place to manage database users and simplifies permission management. Benefits include the following:

- It provides an alternative to SQL Server authentication.
- It helps stop the proliferation of user identities across servers.
- It allows password rotation in a single place.
- Customers can manage database permissions using Microsoft Entra groups.
- It can eliminate storing passwords by enabling integrated Windows authentication and other forms of authentication supported by Microsoft Entra ID.
- Microsoft Entra authentication uses contained database users to authenticate identities at the database level.
- Microsoft Entra ID supports token-based authentication for applications connecting to SQL Database and SQL Managed Instance.
- Microsoft Entra authentication supports:
  - Microsoft Entra cloud-only identities.
  - Microsoft Entra hybrid identities that support:
    - Cloud authentication with two options coupled with seamless single sign-on (SSO) **Pass-through** authentication and **password hash** authentication.
    - Federated authentication.
  - For more information on Microsoft Entra authentication methods and which one to choose, see the following article:
    - [Choose the right authentication method for your Microsoft Entra hybrid identity solution](/azure/active-directory/hybrid/choose-ad-authn)

- SQL Server Management Studio supports connections that use Microsoft Entra with multifactor authentication. Multifactor authentication provides strong authentication with a range of easy verification options — phone call, text message, smart cards with pin, or mobile app notification. For more information, see [SSMS support for Microsoft Entra multifactor authentication with Azure SQL Database, SQL Managed Instance, and Azure Synapse](authentication-mfa-ssms-overview.md)

- SQL Server Data Tools (SSDT) also supports a wide range of authentication options with Microsoft Entra ID. For more information, see [Microsoft Entra ID support in SQL Server Data Tools (SSDT)](/sql/ssdt/azure-active-directory).

The configuration steps include the following procedures to configure and use Microsoft Entra authentication.

1. Create and populate a Microsoft Entra tenant.
1. Optional: Associate or change the current directory that is associated with your Azure Subscription.
1. Create a Microsoft Entra administrator.
1. Configure your client computers.
1. Create contained database users in your database mapped to Microsoft Entra identities.
1. Connect to your database with Microsoft Entra identities.

> [!NOTE]  
> For Azure SQL, Azure VMs, and SQL Server 2022, Microsoft Entra authentication only supports access tokens which originate from Microsoft Entra ID and doesn't support third-party access tokens. Microsoft Entra ID also doesn't support redirecting Microsoft Entra ID queries to third-party endpoints. This applies to all SQL platforms and all operating systems that support Microsoft Entra authentication.

## Trust architecture

- Only the cloud portion of Microsoft Entra ID, SQL Database, SQL Managed Instance, [SQL Server on Windows Azure VMs](../virtual-machines/windows/configure-azure-ad-authentication-for-sql-vm.md), and Azure Synapse is considered to support Microsoft Entra native user passwords.
- To support Windows single sign-on credentials (or user/password for Windows credential), use Microsoft Entra credentials from a federated or managed domain that is configured for seamless single sign-on for pass-through and password hash authentication. For more information, see [Microsoft Entra seamless single sign-on](/azure/active-directory/hybrid/how-to-connect-sso).
- To support Federated authentication (or user/password for Windows credentials), the communication with ADFS block is required.

For more information on Microsoft Entra hybrid identities, the setup, and synchronization, see the following articles:

- Password hash authentication - [Implement password hash synchronization with Microsoft Entra Connect Sync](/azure/active-directory/hybrid/how-to-connect-password-hash-synchronization)
- Pass-through authentication - [Microsoft Entra pass-through authentication](/azure/active-directory/hybrid/how-to-connect-pta-quick-start)
- Federated authentication - [Deploying Active Directory Federation Services in Azure](/windows-server/identity/ad-fs/deployment/how-to-connect-fed-azure-adfs) and [Microsoft Entra Connect and federation](/azure/active-directory/hybrid/how-to-connect-fed-whatis)

For a sample federated authentication with ADFS infrastructure (or user/password for Windows credentials), see the diagram below. The arrows indicate communication pathways.

:::image type="content" source="media/authentication-aad-overview/azure-ad-authentication-diagram.png" alt-text="Diagram of Microsoft Entra authentication for Azure SQL.":::

The following diagram indicates the federation, trust, and hosting relationships that allow a client to connect to a database by submitting a token. The token is authenticated by Microsoft Entra ID, and is trusted by the database. Customer 1 can represent Microsoft Entra ID with native users or Microsoft Entra ID with federated users. Customer 2 represents a possible solution including imported users, in this example coming from a federated Microsoft Entra ID with ADFS being synchronized with Microsoft Entra ID. It's important to understand that access to a database using Microsoft Entra authentication requires that the hosting subscription is associated to the Microsoft Entra ID. The same subscription must be used to create the Azure SQL Database, SQL Managed Instance, or Azure Synapse resources.

:::image type="content" source="./media/authentication-aad-overview/2-subscription-relationship.png" alt-text="Diagram shows the relationship between subscriptions in the Microsoft Entra configuration.":::

## Administrator structure

When using Microsoft Entra authentication, there are two Administrator accounts: the original Azure SQL Database administrator and the Microsoft Entra administrator. The same concepts apply to Azure Synapse. Only the administrator based on a Microsoft Entra account can create the first Microsoft Entra ID contained database user in a user database. The Microsoft Entra administrator login can be a Microsoft Entra user or a Microsoft Entra group. When the administrator is a group account, it can be used by any group member, enabling multiple Microsoft Entra administrators for the server. Using a group account as the administrator enhances manageability by allowing you to centrally add and remove group members in Microsoft Entra ID without changing the users or permissions in SQL Database or Azure Synapse. Only one Microsoft Entra administrator (a user or group) can be configured at any time.

:::image type="content" source="./media/authentication-aad-overview/3-admin-structure.png" alt-text="Diagram shows the administrator structure for Microsoft Entra ID used with SQL Server.":::

> [!NOTE]  
> Microsoft Entra authentication with Azure SQL supports only the single Microsoft Entra tenant where the Azure SQL resource currently resides. All Microsoft Entra objects from this tenant can be set up as users allowing access to Azure SQL in this tenant. The Microsoft Entra admin must also be from the Azure SQL resource's tenant. Microsoft Entra multi-tenant authentication accessing Azure SQL from different tenants are not supported.

## Permissions

To create new users, you must have the `ALTER ANY USER` permission in the database. The `ALTER ANY USER` permission can be granted to any database user. The `ALTER ANY USER` permission is also held by the server administrator accounts, and database users with the `CONTROL ON DATABASE` or `ALTER ON DATABASE` permission for that database, and by members of the `db_owner` database role.

To create a contained database user in Azure SQL Database, Azure SQL Managed Instance, or Azure Synapse, you must connect to the database or instance using a Microsoft Entra identity. To create the first contained database user, you must connect to the database by using a Microsoft Entra administrator (who is the owner of the database). This is demonstrated in [Configure and manage Microsoft Entra authentication with SQL Database or Azure Synapse](authentication-aad-configure.md). Microsoft Entra authentication is only possible if the Microsoft Entra admin was created for Azure SQL Database, Azure SQL Managed Instance, or Azure Synapse. If the Microsoft Entra admin was removed from the server, existing Microsoft Entra users created previously inside the server can no longer connect to the database using their Microsoft Entra credentials.

<a name='azure-ad-features-and-limitations'></a>

## Microsoft Entra features and limitations

- The following members of Microsoft Entra ID can be provisioned for Azure SQL Database:

  - Microsoft Entra users: Any [type of user](/entra/fundamentals/how-to-create-delete-users#types-of-users) in a Microsoft Entra tenant, including internal, external, guests, and members. Members of an Active Directory domain configured for [federation with Microsoft Entra ID](/entra/identity/hybrid/connect/whatis-fed) are also supported, and can be configured for [seamless single sign-on](/entra/identity/hybrid/connect/how-to-connect-sso).
  - Applications: applications that exist in Azure can use service principals or managed identities to authenticate directly to Azure SQL Database. Using managed identities for authentication is preferred due to it being passwordless and eliminating the need for developer-managed credentials.
  - Microsoft Entra groups, which can simplify access management across your organization by managing appropriate user and application access based on their group membership.

- Microsoft Entra users that are part of a group that is member of the `db_owner` database role cannot use the **[CREATE DATABASE SCOPED CREDENTIAL](/sql/t-sql/statements/create-database-scoped-credential-transact-sql)** syntax against Azure SQL Database and Azure Synapse. You'll see the following error:

    `SQL Error [2760] [S0001]: The specified schema name 'user@mydomain.com' either doesn't exist or you do not have permission to use it.`

    To mitigate the **CREATE DATABASE SCOPED CREDENTIAL** issue add the individual Microsoft Entra user the `db_owner` role directly.

- These system functions aren't supported and return NULL values when executed by Microsoft Entra principals:

  - `SUSER_ID()`
  - `SUSER_NAME(<ID>)`
  - `SUSER_SNAME(<SID>)`
  - `SUSER_ID(<name>)`
  - `SUSER_SID(<name>)`

- Azure SQL Database doesn't create implicit users for users logged in as part of a Microsoft Entra group membership. Because of this, various operations that require assigning ownership will fail, even if the Microsoft Entra group is added as a member to a role with those permissions.

   For example, a user signed into a database via a Microsoft Entra group with the **db_ddladmin** role, will not be able to execute CREATE SCHEMA, ALTER SCHEMA, and other object creation statements without a schema explicitly defined (such as table, view, or type, for example). To resolve this, a Microsoft Entra user must be created for that user, or the Microsoft Entra group must be altered to assign the DEFAULT_SCHEMA to **dbo**.

### SQL Managed Instance

- Microsoft Entra server principals (logins) and users are supported for [SQL Managed Instance](../managed-instance/sql-managed-instance-paas-overview.md).
- Setting Microsoft Entra logins mapped to a Microsoft Entra group as database owner isn't supported in [SQL Managed Instance](../managed-instance/sql-managed-instance-paas-overview.md).

  - An extension of this is that when a group is added as part of the `dbcreator` server role, users from this group can connect to the SQL Managed Instance and create new databases, but won't be able to access the database. This is because the new database owner is SA, and not the Microsoft Entra user. This issue doesn't manifest if the individual user is added to the `dbcreator` server role.

- SQL Agent management and jobs execution are supported for Microsoft Entra logins.
- Database backup and restore operations can be executed by Microsoft Entra server principals (logins).
- Auditing of all statements related to Microsoft Entra server principals (logins) and authentication events is supported.
- Dedicated administrator connection for Microsoft Entra server principals (logins) which are members of sysadmin server role is supported.
  - Supported through SQLCMD Utility and SQL Server Management Studio.
- Logon triggers are supported for logon events coming from Microsoft Entra server principals (logins).
- Service Broker and DB mail can be setup using a Microsoft Entra server principal (login).

<a name='connect-by-using-azure-ad-identities'></a>

## Connect by using Microsoft Entra identities

Microsoft Entra authentication supports the following methods of connecting to a database using Microsoft Entra identities:

- Microsoft Entra Password
- Microsoft Entra integrated
- Microsoft Entra Universal with multifactor authentication
- Using Application token authentication

The following authentication methods are supported for Microsoft Entra server principals (logins):

- Microsoft Entra Password
- Microsoft Entra integrated
- Microsoft Entra Universal with multifactor authentication

### Additional considerations

- To enhance manageability, we recommend you provision a dedicated Microsoft Entra group as an administrator.
- Only one Microsoft Entra administrator (a user or group) can be configured for a server in SQL Database or Azure Synapse at any time.
  - The addition of Microsoft Entra server principals (logins) for SQL Managed Instance allows the possibility of creating multiple Microsoft Entra server principals (logins) that can be added to the `sysadmin` role.
- Only a Microsoft Entra administrator for the server can initially connect to the server or managed instance using a Microsoft Entra account. The Microsoft Entra administrator can configure subsequent Microsoft Entra database users.
- Microsoft Entra users and service principals (Microsoft Entra applications) that are members of more than 2048 Microsoft Entra security groups aren't supported to log into the database in SQL Database, SQL Managed Instance, or Azure Synapse.
- We recommend setting the connection timeout to 30 seconds.
- SQL Server 2016 Management Studio and SQL Server Data Tools for Visual Studio 2015 (version 14.0.60311.1April 2016 or later) support Microsoft Entra authentication. (Microsoft Entra authentication is supported by the **.NET Framework Data Provider for SqlServer**; at least version .NET Framework 4.6). Therefore the newest versions of these tools and data-tier applications (DAC and BACPAC) can use Microsoft Entra authentication.
- Beginning with version 15.0.1, [sqlcmd utility](/sql/tools/sqlcmd-utility) and [bcp utility](/sql/tools/bcp-utility) support Active Directory Interactive authentication with multifactor authentication.
- SQL Server Data Tools for Visual Studio 2015 requires at least the April 2016 version of the Data Tools (version 14.0.60311.1). Currently, Microsoft Entra users aren't shown in SSDT Object Explorer. As a workaround, view the users in [sys.database_principals](/sql/relational-databases/system-catalog-views/sys-database-principals-transact-sql).
- [Microsoft JDBC Driver 6.0 for SQL Server](https://www.microsoft.com/download/details.aspx?id=11774) supports Microsoft Entra authentication. Also, see [Setting the Connection Properties](/sql/connect/jdbc/setting-the-connection-properties).
- PolyBase cannot authenticate by using Microsoft Entra authentication.
- Microsoft Entra authentication is supported for Azure SQL Database and Azure Synapse by using the Azure portal **Import Database** and **Export Database** blades. Import and export using Microsoft Entra authentication is also supported from a PowerShell command.
- Microsoft Entra authentication is supported for SQL Database, SQL Managed Instance, and Azure Synapse with using the CLI. For more information, see [Configure and manage Microsoft Entra authentication with SQL Database or Azure Synapse](authentication-aad-configure.md) and [SQL Server - az sql server](/cli/azure/sql/server).

## Next steps

- To learn how to create and populate a Microsoft Entra tenant and then configure it with Azure SQL Database, Azure SQL Managed Instance, or Azure Synapse, see [Configure and manage Microsoft Entra authentication with SQL Database, SQL Managed Instance, or Azure Synapse](authentication-aad-configure.md).
- For a tutorial of using Microsoft Entra server principals (logins) with SQL Managed Instance, see [Microsoft Entra server principals (logins) with SQL Managed Instance](../managed-instance/aad-security-configure-tutorial.md)
- For an overview of logins, users, database roles, and permissions in SQL Database, see [Logins, users, database roles, and permissions](logins-create-manage.md).
- For more information about database principals, see [Principals](/sql/relational-databases/security/authentication-access/principals-database-engine).
- For more information about database roles, see [Database roles](/sql/relational-databases/security/authentication-access/database-level-roles).
- For syntax on creating Microsoft Entra server principals (logins) for SQL Managed Instance, see  [CREATE LOGIN](/sql/t-sql/statements/create-login-transact-sql?view=azuresqldb-mi-current&preserve-view=true).
- For more information about firewall rules in SQL Database, see [SQL Database firewall rules](firewall-configure.md).
