---
title: Create Microsoft Entra Guest Users
titleSuffix: Azure SQL Database & Azure SQL Managed Instance
description: How to create Microsoft Entra guest users and set them as Microsoft Entra admin in Azure SQL Database, Azure SQL Managed Instance, and Azure Synapse Analytics
author: VanMSFT
ms.author: vanto
ms.reviewer: wiassaf, vanto, mathoma, randolphwest
ms.date: 02/23/2026
ms.service: azure-sql
ms.subservice: security
ms.topic: how-to
ms.custom:
  - azure-synapse
monikerRange: "=azuresql || =azuresql-db || =azuresql-mi"
---
# Create Microsoft Entra guest users and set them as a Microsoft Entra admin

[!INCLUDE [appliesto-sqldb-sqlmi-sqlvm](../includes/appliesto-sqldb-sqlmi-sqlvm.md)]

> [!NOTE]
> This feature applies to SQL Server 2022 and later on Azure Virtual Machines and Arc enabled SQL Server.

[Guest users](/entra/external-id/user-properties) with Microsoft Entra B2B collaboration are users that have accounts in an external Microsoft Entra organization or an external identity provider (for example, Outlook, Windows Live Mail, or Gmail), which isn't managed within your Microsoft Entra tenant. Guest user accounts are created when those individuals are invited to collaborate within your tenant, while still performing authentication against their identity provider.

This article demonstrates how to create a Microsoft Entra guest user and set that user as a Microsoft Entra admin for Azure SQL Managed Instance or the [logical server in Azure](logical-servers.md) used by Azure SQL Database and Azure Synapse Analytics.

[!INCLUDE [entra-id](../includes/entra-id.md)]

## Feature description

Azure SQL Database, SQL Managed Instance, and Azure Synapse Analytics support creating principals from guest user accounts, either directly or as members of Microsoft Entra groups within your tenant. Guest users can also be set as the Microsoft Entra admin for the logical server or managed instance.

## Prerequisites

- [Az.Sql 2.9.0](https://www.powershellgallery.com/packages/Az.Sql/2.9.0) module or higher is needed when using PowerShell to set a guest user as a Microsoft Entra admin for the logical server or managed instance.

## Create database user for Microsoft Entra guest user

Follow these steps to create a database user using a Microsoft Entra guest user. In this section, replace `<guest_user>` with a valid email address, for example `guest_user@example.com`.

### [SQL Database and Azure Synapse](#tab/sql-database)

1. Ensure that the guest user is already added into your Microsoft Entra ID and a Microsoft Entra admin has been set for the database server. Having a Microsoft Entra admin is required for Microsoft Entra authentication.

1. Connect to the SQL database as the Microsoft Entra admin, or a Microsoft Entra user with sufficient SQL permissions to create users, and run the following command on the database where the guest user needs to be added:

   ```sql
   CREATE USER [<guest_user>] FROM EXTERNAL PROVIDER;
   ```

1. There should now be a database user created for the guest user.

1. Run the following command to verify the database user got created successfully:

   ```sql
   SELECT * FROM sys.database_principals;
   ```

1. Disconnect and sign into the database as the guest user using [SQL Server Management Studio (SSMS)](/ssms/sql-server-management-studio-ssms) using the authentication method **Azure Active Directory - Universal with MFA**. For more information, see [Using Microsoft Entra multifactor authentication](authentication-mfa-ssms-overview.md).

### [SQL Managed Instance](#tab/sql-managed-instance)

> [!NOTE]  
> SQL Managed Instance supports logins for Microsoft Entra users, as well as Microsoft Entra ID contained database users. The following steps show how to create a login and user for a Microsoft Entra guest user in SQL Managed Instance. You can also choose to create a [contained database user](/sql/relational-databases/security/contained-database-users-making-your-database-portable) in SQL Managed Instance by using the method in the **SQL Database and Azure Synapse** tab.

1. Ensure that the guest user is already added into your Microsoft Entra tenant and a Microsoft Entra admin has been set for the SQL Managed Instance. Having a Microsoft Entra admin is required for Microsoft Entra authentication.

1. Connect to the SQL Managed Instance as the Microsoft Entra admin, or a Microsoft Entra user with sufficient SQL permissions to create users, and run the following command on the `master` database to create a login for the guest user:

   ```sql
   CREATE LOGIN [<guest_user>] FROM EXTERNAL PROVIDER;
   ```

1. There should now be a login created for the guest user in the `master` database.

1. Run the following command to verify the login got created successfully:

   ```sql
   SELECT * FROM sys.server_principals;
   ```

1. Run the following command on the database where the guest user needs to be added:

   ```sql
   CREATE USER [<guest_user>] FROM LOGIN [<guest_user>];
   ```

1. There should now be a database user created for the guest user.

1. Disconnect and sign into the database as the guest user using [SQL Server Management Studio (SSMS)](/ssms/sql-server-management-studio-ssms) using the authentication method **Azure Active Directory - Universal with MFA**. For more information, see [Using Microsoft Entra multifactor authentication](authentication-mfa-ssms-overview.md).

### [SQL Server](#tab/sql-server)

> [!NOTE]
> Use this section after Microsoft Entra authentication is enabled for your SQL Server on Azure VMs or Arc-enabled SQL Server.

1. Verify that Microsoft Entra authentication is enabled for the SQL Server.

1. Make sure the guest user is already added to your Microsoft Entra tenant.

1. Verify that the managed identity selected for enabling Microsoft Entra authentication has either the **Directory Readers** role or these Microsoft Graph app roles: **User.Read.All**, **GroupMember.Read.All**, and **Application.Read.All**.

1. Connect to the SQL Server instance as a Microsoft Entra admin (sysadmin).

1. Create the guest user using one of the following options:

**Option A: Create a login first (server principal), then create a database user from that login**

```sql
-- Run in master
CREATE LOGIN [<guest_user>] FROM EXTERNAL PROVIDER;
GO
```

```sql
-- Run in the target user database
CREATE USER [<guest_user>] FROM LOGIN [<guest_user>];
GO
```

**Option B: Create a contained database user (no server login)**

```sql
-- Run in the target user database
CREATE USER [<guest_user>] FROM EXTERNAL PROVIDER;
GO
```

#### Set a guest user as a server admin

In this section, replace `<guest_user>` with a valid email address, for example `guest_user@example.com`.

```sql
USE [master];
GO
```

```sql
-- Create the Microsoft Entra login for the guest user
CREATE LOGIN [<guest_user>] FROM EXTERNAL PROVIDER;
GO
```

```sql
-- Grant full server admin rights
ALTER SERVER ROLE [sysadmin] ADD MEMBER [<guest_user>];
GO
```

> [!NOTE]
> If you want guest users to be able to create other Microsoft Entra logins or users, they must have permissions to read other identities in the Microsoft Entra directory. This permission is configured at the directory-level. For more information, see [guest access permissions in Microsoft Entra ID](/entra/identity/users/users-restrict-guest-permissions).

---

## Set a guest user as a Microsoft Entra admin

Set the Microsoft Entra admin using either the Azure portal, Azure PowerShell, or the Azure CLI. In this section, replace `<guest_user>` with a valid email address, for example `guest_user@example.com`.

> [!NOTE]  
> If you want guest users to be able to create other Microsoft Entra logins or users, they must have permissions to read other identities in the Microsoft Entra directory. This permission is configured at the directory-level. For more information, see [guest access permissions in Microsoft Entra ID](/entra/identity/users/users-restrict-guest-permissions).

### Azure portal

To set up a Microsoft Entra admin for a logical server or a managed instance using the Azure portal, follow these steps:

1. Open the [Azure portal](https://portal.azure.com).
1. Navigate to your SQL server or managed instance resource **Microsoft Entra** page under **Settings**.
1. Select **Set admin** to open the **Microsoft Entra ID** pane.
1. In the **Microsoft Entra ID** pane, type the guest user account name.
1. Select this new user, and then save the operation.

For more information, see [Setting Microsoft Entra admin](authentication-aad-configure.md#provision-azure-ad-admin-sql-database).

### Azure PowerShell (SQL Database and Azure Synapse)

To set up a Microsoft Entra guest user for a logical server, follow these steps:

1. Ensure that the guest user is already added into your Microsoft Entra tenant.

1. Run the following PowerShell command to add the guest user as the Microsoft Entra admin for your logical server:

   - Replace `<ResourceGroupName>` with your Azure Resource Group name that contains the logical server.
   - Replace `<ServerName>` with your logical server name. If your server name is `myserver.database.windows.net`, replace `<Server Name>` with `myserver`.
   - Replace `<DisplayNameOfGuestUser>` with your guest user name.

   ```powershell
   Set-AzSqlServerActiveDirectoryAdministrator -ResourceGroupName <ResourceGroupName> -ServerName <ServerName> -DisplayName <DisplayNameOfGuestUser>
   ```

You can also use the Azure CLI command [az sql server ad-admin](/cli/azure/sql/server/ad-admin) to set the guest user as a Microsoft Entra admin for your logical server.

### Azure PowerShell (SQL Managed Instance)

To set up a Microsoft Entra guest user for a managed instance, follow these steps:

1. Ensure that the guest user is already added into your Microsoft Entra tenant.

1. Go to the [Azure portal](https://portal.azure.com), and go to your **Microsoft Entra ID** resource. Under **Manage**, go to the **Users** pane. Select your guest user, and record the `Object ID`.

1. Run the following PowerShell command to add the guest user as the Microsoft Entra admin for your SQL Managed Instance:

   - Replace `<ResourceGroupName>` with your Azure Resource Group name that contains the SQL Managed Instance.
   - Replace `<ManagedInstanceName>` with your SQL Managed Instance name.
   - Replace `<DisplayNameOfGuestUser>` with your guest user name.
   - Replace `<AADObjectIDOfGuestUser>` with the `Object ID` gathered earlier.

   ```powershell
   Set-AzSqlInstanceActiveDirectoryAdministrator -ResourceGroupName <ResourceGroupName> -InstanceName "<ManagedInstanceName>" -DisplayName <DisplayNameOfGuestUser> -ObjectId <AADObjectIDOfGuestUser>
   ```

You can also use the Azure CLI command [az sql mi ad-admin](/cli/azure/sql/mi/ad-admin) to set the guest user as the Microsoft Entra admin for your managed instance.

## Related content

- [Configure and manage Microsoft Entra authentication with Azure SQL](authentication-aad-configure.md)
- [Using Microsoft Entra multifactor authentication](authentication-mfa-ssms-overview.md)
- [CREATE USER (Transact-SQL)](/sql/t-sql/statements/create-user-transact-sql)
- [Microsoft Entra authentication for Arc-enabled SQL Server](/sql/sql-server/azure-arc/microsoft-entra-authentication-with-managed-identity)
- [Configure Microsoft Entra authentication for SQL Server on Azure VMs](/azure/azure-sql/virtual-machines/windows/configure-azure-ad-authentication-for-sql-vm)
