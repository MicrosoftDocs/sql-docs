---
title: Configure Microsoft Entra authentication
titleSuffix: Azure SQL Database & SQL Managed Instance & Azure Synapse Analytics
description: Learn how to connect to SQL Database, SQL Managed Instance, and Azure Synapse Analytics using the Microsoft Entra authentication.
author: nofield
ms.author: nofield
ms.reviewer: wiassaf, vanto, mathoma, maghan
ms.date: 01/31/2024
ms.service: azure-sql
ms.subservice: security
ms.topic: how-to
ms.custom:
  - azure-synapse
  - has-adal-ref
  - sqldbrb=2
  - devx-track-azurepowershell
  - has-azure-ad-ps-ref, azure-ad-ref-level-one-done
monikerRange: "=azuresql||=azuresql-db||=azuresql-mi"
---

# Configure and manage Microsoft Entra authentication with Azure SQL

[!INCLUDE [appliesto-sqldb-sqlmi-asa](../includes/appliesto-sqldb-sqlmi-asa.md)]

This article shows you how to create and populate a Microsoft Entra tenant and use Microsoft Entra ID ([formerly Azure Active Directory](/entra/fundamentals/new-name)) with [Azure SQL Database](sql-database-paas-overview.md), [Azure SQL Managed Instance](../managed-instance/sql-managed-instance-paas-overview.md), and [Azure Synapse Analytics](/azure/synapse-analytics/sql-data-warehouse/sql-data-warehouse-overview-what-is). For an overview, see [Microsoft Entra authentication](authentication-aad-overview.md).

[!INCLUDE [entra-id](../includes/entra-id.md)]

<a name='azure-ad-authentication-methods'></a>

## Microsoft Entra authentication methods

Microsoft Entra ID supports the following authentication methods:

- Microsoft Entra cloud-only identities
- Microsoft Entra hybrid identities that support:
  - Cloud authentication with two options coupled with seamless single sign-on (SSO)
    - Microsoft Entra password hash authentication
    - Microsoft Entra pass-through authentication
  - Federated authentication

For more information on Microsoft Entra authentication methods and which one to choose, see [Choose the correct authentication method for your Microsoft Entra hybrid identity solution](/azure/active-directory/hybrid/choose-ad-authn).

For more information on Microsoft Entra hybrid identities, setup, and synchronization, see:

- Password hash authentication - [Implement password hash synchronization with Microsoft Entra Connect Sync](/azure/active-directory/hybrid/how-to-connect-password-hash-synchronization)
- Pass-through authentication - [Microsoft Entra pass-through authentication](/azure/active-directory/hybrid/how-to-connect-pta-quick-start)
- Federated authentication - [Deploying Active Directory Federation Services in Azure](/windows-server/identity/ad-fs/deployment/how-to-connect-fed-azure-adfs) and [Microsoft Entra Connect and federation](/azure/active-directory/hybrid/how-to-connect-fed-whatis)

<a name='create-and-populate-an-azure-ad-instance'></a>

## Create and populate a Microsoft Entra tenant

Create a Microsoft Entra tenant and populate it with users and groups. Microsoft Entra tenants can be managed entirely within Azure or used for the federation of an on-premises Active Directory Domain Service.

For more information, see:

- [What is Microsoft Entra ID?](/azure/active-directory/fundamentals/active-directory-whatis)
- [Integrating your on-premises identities with Microsoft Entra ID](/azure/active-directory/hybrid/whatis-hybrid-identity)
- [Add your domain name to Microsoft Entra ID](/azure/active-directory/fundamentals/add-custom-domain)
- [What is Federation with Microsoft Entra ID?](/azure/active-directory/hybrid/connect/whatis-fed)
- [Directory synchronization with Microsoft Entra ID](/azure/active-directory/fundamentals/sync-directory)
- [Manage Microsoft Entra ID using Windows PowerShell](/powershell/module/azuread)
- [Hybrid Identity Required Ports and Protocols](/azure/active-directory/hybrid/reference-connect-ports).

<a name='associate-or-add-an-azure-subscription-to-azure-active-directory'></a>

## Associate or add an Azure subscription to Microsoft Entra ID

1. Associate your Azure subscription to Microsoft Entra ID by making the directory a trusted directory for the Azure subscription hosting the database. For details, see [Associate or add an Azure subscription to your Microsoft Entra tenant](/azure/active-directory/fundamentals/active-directory-how-subscriptions-associated-directory).

1. Use the directory switcher in the Azure portal to switch to the subscription associated with the domain.

   > [!IMPORTANT]  
   > Every Azure subscription has a trust relationship with a Microsoft Entra instance. It trusts that directory to authenticate users, services, and devices. Multiple subscriptions can trust the same directory, but a subscription trusts only one directory. This trust relationship that a subscription has with a directory is unlike that of a subscription with all other resources in Azure (websites, databases, and so on), which are more like child resources of a subscription. If a subscription expires, then access to those other resources associated with the subscription also stops. However, the directory remains in Azure, and you can associate another subscription with that directory and continue to manage the directory users. For more information about resources, see [Understanding resource access in Azure](/azure/active-directory/external-identities/add-users-administrator). To learn more about this trusted relationship, see [How to associate or add an Azure subscription to Microsoft Entra ID](/azure/active-directory/fundamentals/active-directory-how-subscriptions-associated-directory).

<a name='azure-ad-admin-with-a-server-in-sql-database'></a>

## Microsoft Entra admin with a server in SQL Database

Each [logical server](logical-servers.md) in Azure (which hosts SQL Database or Azure Synapse) starts with a single server administrator account that is the administrator of the entire server. Create a second administrator account as a Microsoft Entra account. This principal is created as a contained database user in the `master` database of the server. Administrator accounts are members of the **db_owner** role in every user database, and each user database is entered as the **dbo** user. For more information about administrator accounts, see [Managing Databases and Logins](logins-create-manage.md).

You should associate the Microsoft Entra ID admin with a Microsoft Entra Group. This client ID of the Microsoft Entra Group is inserted into the `sys.database_principals` table in the `master` database. 

The Microsoft Entra administrator must be configured for both the primary and the secondary servers when using Microsoft Entra ID with geo-replication. If a server doesn't have a Microsoft Entra administrator, then Microsoft Entra logins and users receive a `Cannot connect` to a server error.

> [!NOTE]  
> Users not based on a Microsoft Entra account (including the server administrator account) can't create Microsoft Entra-based users, because they don't have permission to validate proposed database users with Microsoft Entra ID.

<a name='provision-azure-ad-admin-sql-managed-instance'></a>

## Provision Microsoft Entra admin (SQL Managed Instance)

> [!IMPORTANT]  
> Only follow these steps if you are provisioning an Azure SQL Managed Instance. This operation can only be executed by Global Administrator or a Privileged Role Administrator in Microsoft Entra ID.
>  
> You can assign the **Directory Readers** role to a group in Microsoft Entra ID. The group owners can then add the managed instance identity as a member of this group, which allows you to provision a Microsoft Entra admin for the SQL Managed Instance. For more information on this feature, see [Directory Readers role in Microsoft Entra for Azure SQL](authentication-aad-directory-readers-role.md).

Your SQL Managed Instance needs permission to read Microsoft Entra ID to accomplish tasks such as authentication of users through security group membership or creation of new users. For this to work, you must grant the SQL Managed Instance permission to read Microsoft Entra ID. You can do this using the Azure portal or PowerShell.

### Azure portal

To grant your SQL Managed Instance read permissions to Microsoft Entra ID using the Azure portal, sign in as a Global Administrator and follow these steps:

1. In the [Azure portal](https://portal.azure.com), in the upper-right corner select your account, and then choose **Switch directories** to confirm which directory is your **Current directory**. Switch directories, if necessary.

   :::image type="content" source="media/authentication-aad-configure/switch-directory.png" alt-text="Screenshot of the Azure portal showing where to switch your directory.":::

1. Choose the correct Microsoft Entra directory as the **Current directory**.

   This step links the subscription associated with Microsoft Entra ID to the SQL Managed Instance, ensuring the Microsoft Entra tenant and SQL Managed Instance use the same subscription.

1. Now, you can choose your Microsoft Entra admin for your SQL Managed Instance. For that, go to your managed instance resource in the Azure portal and select **Microsoft Entra admin** under **Settings**.

   :::image type="content" source="media/authentication-aad-configure/active-directory-pane.png" alt-text="Screenshot of the Azure portal showing the Microsoft Entra admin page open for the selected SQL managed instance." lightbox="media/authentication-aad-configure/active-directory-pane.png":::

1. Select the banner on top of the Microsoft Entra admin page and grant permission to the current user.

    :::image type="content" source="media/authentication-aad-configure/grant-permissions.png" alt-text="Screenshot of the dialog for granting permissions to a SQL managed instance for accessing Microsoft Entra ID with the Grant permissions button selected.":::

1. After the operation succeeds, the following notification will show up in the top-right corner:

    :::image type="content" source="media/authentication-aad-configure/success.png" alt-text="Screenshot of a notification confirming that Microsoft Entra ID read permissions have been successfully updated for the managed instance.":::

1. On the **Microsoft Entra admin** page, select **Set admin** from the navigation bar to open the **Microsoft Entra ID** pane.

    :::image type="content" source="media/authentication-aad-configure/set-admin.png" alt-text="Screenshot showing the Set admin command highlighted on the Microsoft Entra admin page for the selected SQL managed instance." lightbox="media/authentication-aad-configure/set-admin.png":::

1. On the **Microsoft Entra ID** pane, search for a user, check the box next to the user or group to be an administrator, and then press **Select** to close the pane and go back to the **Microsoft Entra admin** page for your managed instance.

   The **Microsoft Entra ID** pane shows all members and groups within your current directory. Grayed-out users or groups can't be selected because they aren't supported as Microsoft Entra administrators. See the list of supported admins in [Microsoft Entra features and limitations](./authentication-aad-overview.md#azure-ad-features-and-limitations). Azure role-based access control (Azure RBAC) applies only to the Azure portal and isn't propagated to SQL Database, SQL Managed Instance, or Azure Synapse.

1. From the navigation bar of the **Microsoft Entra admin** page for your managed instance, select **Save** to confirm your Microsoft Entra administrator.

    :::image type="content" source="media/authentication-aad-configure/save.png" alt-text="Screenshot of the Microsoft Entra admin page with the Save button in the top row next to the Set admin and Remove admin buttons." lightbox="media/authentication-aad-configure/save.png":::

    The process of changing the administrator might take several minutes. Then the new administrator appears in the Microsoft Entra admin box.

    The **Object ID** is displayed next to the admin name for Microsoft Entra users and groups. For applications (service principals), the **Application ID** is displayed.

After provisioning a Microsoft Entra admin for your SQL Managed Instance, you can begin to create Microsoft Entra server principals (logins) with the [CREATE LOGIN](/sql/t-sql/statements/create-login-transact-sql?view=azuresqldb-mi-current&preserve-view=true) syntax. For more information, see [SQL Managed Instance overview](../managed-instance/sql-managed-instance-paas-overview.md#azure-active-directory-integration).

> [!TIP]  
> To remove an Admin later, at the top of the Microsoft Entra admin page, select **Remove admin**, then select **Save**.

### PowerShell

To use PowerShell to grant your SQL Managed Instance read permissions to Microsoft Entra ID, run this script:

```powershell
# This script grants "Directory Readers" permission to a service principal representing the SQL Managed Instance.
# It can be executed only by a user who is a member of the **Global Administrator** or **Privileged Roles Administrator** role.

Import-Module Microsoft.Graph.Authentication
$managedInstanceName = "<ManagedInstanceName>" # Enter the name of your managed instance
$tenantId = "<TenantId>"                       # Enter your tenant ID

Connect-MgGraph -TenantId $tenantId -Scopes "RoleManagement.ReadWrite.Directory"
 
# Get Microsoft Entra "Directory Readers" role and create if it doesn't exist
$roleName = "Directory Readers"
$role = Get-MgDirectoryRole -Filter "DisplayName eq '$roleName'"
if ($role -eq $null) {
    # Instantiate an instance of the role template
    $roleTemplate = Get-MgDirectoryRoleTemplate -Filter "DisplayName eq '$roleName'"
    New-MgDirectoryRoleTemplate -RoleTemplateId $roleTemplate.Id
    $role = Get-MgDirectoryRole -Filter "DisplayName eq '$roleName'"
}

# Get service principal for your SQL Managed Instance
$roleMember = Get-MgServicePrincipal -Filter "DisplayName eq '$managedInstanceName'"
$roleMember.Count
if ($roleMember -eq $null) {
    Write-Output "Error: No service principal with name '$($managedInstanceName)' found, make sure that managedInstanceName parameter was entered correctly."
    exit
}
if (-not ($roleMember.Count -eq 1)) {
    Write-Output "Error: Multiple service principals with name '$($managedInstanceName)'"
    Write-Output $roleMember | Format-List DisplayName, Id, AppId
    exit
}

# Check if service principal is already member of Directory Readers role
$isDirReader = Get-MgDirectoryRoleMember -DirectoryRoleId $role.Id -Filter "Id eq '$($roleMember.Id)'"
if ($isDirReader -eq $null) {
    # Add principal to Directory Readers role
    Write-Output "Adding service principal '$($managedInstanceName)' to 'Directory Readers' role..."
    $body = @{
        "@odata.id"= "https://graph.microsoft.com/v1.0/directoryObjects/{$($roleMember.Id)}"
    }
    New-MgDirectoryRoleMemberByRef -DirectoryRoleId $role.Id -BodyParameter $body
    Write-Output "'$($managedInstanceName)' service principal added to 'Directory Readers' role."
} else {
    Write-Output "Service principal '$($managedInstanceName)' is already member of 'Directory Readers' role."
}
```

### PowerShell for SQL Managed Instance

# [PowerShell](#tab/azure-powershell)

To run PowerShell cmdlets, you need to have Azure PowerShell installed and running. See [How to install and configure Azure PowerShell](/powershell/azure/) for detailed information.

> [!IMPORTANT]  
> Azure SQL Managed Instance still supports the PowerShell Azure Resource Manager (RM) module, but all future development is for the Az.Sql module. The AzureRM module will receive bug fixes until at least December 2020.  The arguments for the commands in the Az module and in the AzureRm modules are substantially identical. For more about their compatibility, see [Introducing the new Azure PowerShell Az module](/powershell/azure/new-azureps-module-az).

To provision a Microsoft Entra admin, execute the following Azure PowerShell commands:

- Connect-AzAccount
- Select-AzSubscription

The cmdlets used to provision and manage Microsoft Entra admin for your SQL Managed Instance are listed in the following table:

| Cmdlet name | Description |
| --- | --- |
| [Set-AzSqlInstanceActiveDirectoryAdministrator](/powershell/module/az.sql/set-azsqlinstanceactivedirectoryadministrator) | Provisions a Microsoft Entra administrator for the SQL Managed Instance in the current subscription. (Must be from the current subscription) |
| [Remove-AzSqlInstanceActiveDirectoryAdministrator](/powershell/module/az.sql/remove-azsqlinstanceactivedirectoryadministrator) | Removes a Microsoft Entra administrator for the SQL Managed Instance in the current subscription. |
| [Get-AzSqlInstanceActiveDirectoryAdministrator](/powershell/module/az.sql/get-azsqlinstanceactivedirectoryadministrator) | Returns information about a Microsoft Entra administrator for the SQL Managed Instance in the current subscription. |

The following command gets information about a Microsoft Entra administrator for a SQL Managed Instance named ManagedInstance01 associated with a resource group named ResourceGroup01.

```powershell
Get-AzSqlInstanceActiveDirectoryAdministrator -ResourceGroupName "ResourceGroup01" -InstanceName "ManagedInstance01"
```

The following command provisions a Microsoft Entra administrator group named DBAs for the SQL Managed Instance named ManagedInstance01. This server is associated with the resource group ResourceGroup01.

```powershell
Set-AzSqlInstanceActiveDirectoryAdministrator -ResourceGroupName "ResourceGroup01" -InstanceName "ManagedInstance01" -DisplayName "DBAs" -ObjectId "40b79501-b343-44ed-9ce7-da4c8cc7353b"
```

The following command removes the Microsoft Entra administrator for the SQL Managed Instance named ManagedInstanceName01 associated with the resource group ResourceGroup01.

```powershell
Remove-AzSqlInstanceActiveDirectoryAdministrator -ResourceGroupName "ResourceGroup01" -InstanceName "ManagedInstanceName01" -Confirm -PassThru
```

# [Azure CLI](#tab/azure-cli)

You can also provision a Microsoft Entra admin for the SQL Managed Instance by calling the following CLI commands:

| Command | Description |
| --- | --- |
| [az sql mi ad-admin create](/cli/azure/sql/mi/ad-admin#az-sql-mi-ad-admin-create) | Provisions a Microsoft Entra administrator for the SQL Managed Instance (must be from the current subscription). |
| [az sql mi ad-admin delete](/cli/azure/sql/mi/ad-admin#az-sql-mi-ad-admin-delete) | Removes a Microsoft Entra administrator for the SQL Managed Instance. |
| [az sql mi ad-admin list](/cli/azure/sql/mi/ad-admin#az-sql-mi-ad-admin-list) | Returns information about a Microsoft Entra administrator currently configured for the SQL Managed Instance. |
| [az sql mi ad-admin update](/cli/azure/sql/mi/ad-admin#az-sql-mi-ad-admin-update) | Updates the Microsoft Entra administrator for the SQL Managed Instance. |

For more CLI commands, see [az sql mi](/cli/azure/sql/mi).

---

<a name='provision-azure-ad-admin-sql-database'></a>

## Provision Microsoft Entra admin (SQL Database)

> [!IMPORTANT]  
> Only follow these steps if you are provisioning a [server](logical-servers.md) for SQL Database or Azure Synapse.

The following two procedures show you how to provision a Microsoft Entra administrator for your server in the Azure portal and by using PowerShell.

### Azure portal

1. In the [Azure portal](https://portal.azure.com/), in the upper-right corner, select your account and then choose **Switch directory** to open the **Directories + subscriptions** page. Choose the Microsoft Entra directory, which contains your Azure SQL Database or Azure Synapse Analytics as the **Current directory**.

1. Search for **SQL servers** and select the logical server for your Azure SQL Database.

    :::image type="content" source="media/authentication-aad-configure/search-for-and-select-sql-servers.png" alt-text="Search for and select SQL servers.":::

    > [!NOTE]  
    > On this page, before you select **SQL servers**, you can select the **star** next to the name to *favorite* the category and add **SQL servers** to the left navigation menu.
    >  
    > Consider also visiting [your Azure SQL dashboard](https://portal.azure.com/#view/HubsExtension/BrowseResource/resourceType/Microsoft.Sql%2Fazuresql).

1. On the **SQL server** page, select **Microsoft Entra ID**.

1. On the **Microsoft Entra ID** page, select **Set admin** to open the **Microsoft Entra ID** pane

    :::image type="content" source="media/authentication-aad-configure/sql-servers-set-active-directory-admin.png" alt-text="Screenshot shows the option to set the Microsoft Entra admin for SQL servers." lightbox="media/authentication-aad-configure/sql-servers-set-active-directory-admin.png":::

1. On the **Microsoft Entra ID** pane, search for a user and then select the user or group to be an administrator.  Use **Select** to confirm your choice and close the pane to return to your logical server's **Microsoft Entra ID** page.  (The **Microsoft Entra ID** pane shows all members and groups of your current directory. Grayed-out users or groups can't be selected because they aren't supported as Microsoft Entra administrators. See the list of supported admins in the **Microsoft Entra features and limitations** section of [Use Microsoft Entra authentication with SQL Database or Azure Synapse](authentication-aad-overview.md).) Azure role-based access control (Azure RBAC) applies only to the portal and isn't propagated to the server.

1. At the top of the **Microsoft Entra ID** page for your logical server, select **Save**.

    :::image type="content" source="media/authentication-aad-configure/save.png" alt-text="Screenshot shows the option to save a Microsoft Entra admin." lightbox="media/authentication-aad-configure/save.png":::

    The **Object ID** is displayed next to the admin name for Microsoft Entra users and groups. For applications (service principals), the **Application ID** is displayed.

The process of changing the administrator might take several minutes. Then the new administrator appears in the **Microsoft Entra admin** field.

   > [!NOTE]  
   > When setting up the Microsoft Entra admin, the new admin name (user or group) can't already be present in the virtual `master` database as a server authentication user. If present, the Microsoft Entra admin setup fails and rolls back, indicating such an admin (name) already exists. Since a server authentication user isn't part of Microsoft Entra ID, any effort to connect to the server using Microsoft Entra authentication fails.

To remove the admin later, at the top of the **Microsoft Entra ID** page, select **Remove admin**, then select **Save**. This disables Microsoft Entra authentication for your logical server.

### PowerShell for SQL Database and Azure Synapse

# [PowerShell](#tab/azure-powershell)

To run PowerShell cmdlets, you need to have Azure PowerShell installed and running. See [How to install and configure Azure PowerShell](/powershell/azure/) for detailed information. To provision a Microsoft Entra admin, execute the following Azure PowerShell commands:

- Connect-AzAccount
- Select-AzSubscription

Cmdlets used to provision and manage Microsoft Entra admin for SQL Database and Azure Synapse:

| Cmdlet name | Description |
| --- | --- |
| [Set-AzSqlServerActiveDirectoryAdministrator](/powershell/module/az.sql/set-azsqlserveractivedirectoryadministrator) | Provisions a Microsoft Entra administrator for the server hosting SQL Database or Azure Synapse. (Must be from the current subscription) |
| [Remove-AzSqlServerActiveDirectoryAdministrator](/powershell/module/az.sql/remove-azsqlserveractivedirectoryadministrator) | Removes a Microsoft Entra administrator for the server hosting SQL Database or Azure Synapse. |
| [Get-AzSqlServerActiveDirectoryAdministrator](/powershell/module/az.sql/get-azsqlserveractivedirectoryadministrator) | Returns information about a Microsoft Entra administrator currently configured for the server hosting SQL Database or Azure Synapse. |

Use PowerShell command get-help to see more information for each of these commands. For example, `get-help Set-AzSqlServerActiveDirectoryAdministrator`.

The following script provisions a Microsoft Entra administrator group named **DBA_Group** (object ID `40b79501-b343-44ed-9ce7-da4c8cc7353f`) for the **demo_server** server in a resource group named **Group-23**:

```powershell
Set-AzSqlServerActiveDirectoryAdministrator -ResourceGroupName "Group-23" -ServerName "demo_server" -DisplayName "DBA_Group"
```

The **DisplayName** input parameter accepts either the Microsoft Entra ID display name or the User Principal Name. For example, ``DisplayName="John Smith"`` and ``DisplayName="johns@contoso.com"``. For Microsoft Entra groups only the Microsoft Entra ID display name is supported.

> [!NOTE]  
> The Azure PowerShell command `Set-AzSqlServerActiveDirectoryAdministrator` doesn't prevent you from provisioning Microsoft Entra admins for unsupported users. An unsupported user can be provisioned but can't connect to a database.

The following example uses the optional **ObjectID**:

```powershell
Set-AzSqlServerActiveDirectoryAdministrator -ResourceGroupName "Group-23" -ServerName "demo_server" `
    -DisplayName "DBA_Group" -ObjectId "40b79501-b343-44ed-9ce7-da4c8cc7353f"
```

> [!NOTE]  
> The **ObjectID** is required when the **DisplayName** is not unique. To retrieve the **ObjectID** and **DisplayName** values, you can view the properties of a user or group in the Microsoft Entra ID section of the Azure Portal.

The following example returns information about the current Microsoft Entra admin for the server:

```powershell
Get-AzSqlServerActiveDirectoryAdministrator -ResourceGroupName "Group-23" -ServerName "demo_server" | Format-List
```

The following example removes a Microsoft Entra administrator:

```powershell
Remove-AzSqlServerActiveDirectoryAdministrator -ResourceGroupName "Group-23" -ServerName "demo_server"
```

# [Azure CLI](#tab/azure-cli)

You can provision a Microsoft Entra admin by calling the following CLI commands:

| Command | Description |
| --- | --- |
| [az sql server ad-admin create](/cli/azure/sql/server/ad-admin#az-sql-server-ad-admin-create) | Provisions a Microsoft Entra administrator for the server hosting SQL Database or Azure Synapse. (Must be from the current subscription) |
| [az sql server ad-admin delete](/cli/azure/sql/server/ad-admin#az-sql-server-ad-admin-delete) | Removes a Microsoft Entra administrator for the server hosting SQL Database or Azure Synapse. |
| [az sql server ad-admin list](/cli/azure/sql/server/ad-admin#az-sql-server-ad-admin-list) | Returns information about a Microsoft Entra administrator currently configured for the server hosting SQL Database or Azure Synapse. |
| [az sql server ad-admin update](/cli/azure/sql/server/ad-admin#az-sql-server-ad-admin-update) | Updates the Microsoft Entra administrator for the server hosting SQL Database or Azure Synapse. |

For more CLI commands, see [az sql server](/cli/azure/sql/server)

---

> [!NOTE]  
> You can also provision a Microsoft Entra Administrator by using the REST APIs. For more information, see [Service Management REST API Reference and Operations for Azure SQL Database Operations for Azure SQL Database](/rest/api/sql/)

## Configure your client computers

> [!NOTE]  
> [System.Data.SqlClient](/dotnet/api/system.data.sqlclient) uses the Azure Active Directory Authentication Library (ADAL), which is deprecated. If you're using the [System.Data.SqlClient](/dotnet/api/system.data.sqlclient) namespace for Microsoft Entra authentication, migrate applications to [Microsoft.Data.SqlClient](/sql/connect/ado-net/introduction-microsoft-data-sqlclient-namespace) and the [Microsoft Authentication Library (MSAL)](/azure/active-directory/develop/msal-migration). For more information, see [Using Microsoft Entra authentication with SqlClient](/sql/connect/ado-net/sql/azure-active-directory-authentication).
>  
> If you must continue using *ADAL.DLL* in your applications, you can use the links in this section to install the latest ODBC or OLE DB driver, which contain the latest *ADAL.DLL* library.

On all client machines from which your applications or users connect to SQL Database or Azure Synapse using Microsoft Entra identities, you must install the following software:

- .NET Framework 4.6 or later from [https://msdn.microsoft.com/library/5a4x27ek.aspx](/dotnet/framework/install/guide-for-developers).
- [Microsoft Authentication Library (MSAL)](/azure/active-directory/develop/msal-migration) or Microsoft Authentication Library for SQL Server (*ADAL.DLL*). Below are the download links to install the latest SSMS, ODBC, and OLE DB driver that contains the *ADAL.DLL* library.
  - [SQL Server Management Studio](/sql/ssms/download-sql-server-management-studio-ssms)
  - [ODBC Driver 17 for SQL Server](/sql/connect/odbc/download-odbc-driver-for-sql-server?view=sql-server-ver15&preserve-view=true)
  - [OLE DB Driver 18 for SQL Server](/sql/connect/oledb/download-oledb-driver-for-sql-server?view=sql-server-ver15&preserve-view=true)

You can meet these requirements by:

- Installing the latest version of [SQL Server Management Studio](/sql/ssms/download-sql-server-management-studio-ssms) or [SQL Server Data Tools](/sql/ssdt/download-sql-server-data-tools-ssdt) meets the .NET Framework 4.6 requirement.
  - SSMS installs the x86 version of *ADAL.DLL*.
  - SSDT installs the amd64 version of *ADAL.DLL*.
  - The latest Visual Studio from [Visual Studio Downloads](https://www.visualstudio.com/downloads/download-visual-studio-vs) meets the .NET Framework 4.6 requirement but doesn't install the required amd64 version of *ADAL.DLL*.

<a name='create-contained-users-mapped-to-azure-ad-identities'></a>

## Create contained users mapped to Microsoft Entra identities

This section reviews the requirements and important considerations to use Microsoft Entra authentication with Azure SQL Database, Azure SQL Managed Instance, and Azure Synapse.

- Microsoft Entra authentication with SQL Database and Azure Synapse requires using contained database users based on a Microsoft Entra identity. A contained database user doesn't have a login in the `master` database, and maps to an identity in the Microsoft Entra ID associated with the database. The Microsoft Entra identity can be an individual user account, group, or application. For more information about contained database users, see [Contained Database Users- Making Your Database Portable](/sql/relational-databases/security/contained-database-users-making-your-database-portable). For more information about creating contained database users based on Microsoft Entra identities, see [CREATE USER (Transact-SQL)](/sql/t-sql/statements/create-user-transact-sql).

- Because SQL Managed Instance supports Microsoft Entra server principals (logins), using contained database users isn't required. This lets you create logins from Microsoft Entra users, groups, or applications. This means you can authenticate with your SQL Managed Instance using the Microsoft Entra server login rather than a contained database user. For more information, see [SQL Managed Instance overview](../managed-instance/sql-managed-instance-paas-overview.md#azure-active-directory-integration). For syntax on creating Microsoft Entra server principals (logins), see [CREATE LOGIN](/sql/t-sql/statements/create-login-transact-sql?view=azuresqldb-mi-current&preserve-view=true).

- Database users (except administrators) can't create a database using the Azure portal. Microsoft Entra roles aren't propagated to the database in SQL Database, SQL Managed Instance, or Azure Synapse. Microsoft Entra roles manage Azure resources and don't apply to database permissions. For example, the **SQL Server Contributor** role doesn't grant access to connect to the database in SQL Database, SQL Managed Instance, or Azure Synapse. The access permission must be granted directly in the database using Transact-SQL statements.

- You can't directly create a database user for an identity managed in a different Microsoft Entra tenant than the one associated with your Azure subscription. However, users in other directories can be imported into the associated directory as external users. They can then be used to create contained database users that can access the SQL Database. External users can also gain access through membership in Microsoft Entra groups that contain database users.

- Special characters like colon `:` or ampersand `&` when included as user names in the T-SQL `CREATE LOGIN` and `CREATE USER` statements aren't supported.

> [!IMPORTANT]  
> Microsoft Entra users and service principals (Microsoft Entra applications) that are members of more than 2048 Microsoft Entra security groups are not supported to login into the database in SQL Database, SQL Managed Instance, or Azure Synapse.

To create a Microsoft Entra ID-based contained database user (other than the server administrator that owns the database), connect to the database with a Microsoft Entra identity as a user with at least the **ALTER ANY USER** permission. In the following T-SQL example, `Microsoft_Entra_principal_name` can be the user principal name of a Microsoft Entra user or the display name for a Microsoft Entra group.

```sql
CREATE USER [<Microsoft_Entra_principal_name>] FROM EXTERNAL PROVIDER;
```

**Examples:**
To create a contained database user representing a Microsoft Entra federated or managed domain user:

```sql
CREATE USER [bob@contoso.com] FROM EXTERNAL PROVIDER;
CREATE USER [alice@fabrikam.onmicrosoft.com] FROM EXTERNAL PROVIDER;
```

To create a contained database user representing a Microsoft Entra group, provide the display name of the group:

```sql
CREATE USER [ICU Nurses] FROM EXTERNAL PROVIDER;
```

To create a contained database user representing an application that connects using a Microsoft Entra token:

```sql
CREATE USER [appName] FROM EXTERNAL PROVIDER;
```

The `CREATE USER ... FROM EXTERNAL PROVIDER` command requires SQL access to Microsoft Entra ID (the "external provider") on behalf of the logged-in user. Sometimes, circumstances arise that cause Microsoft Entra ID to return an exception to SQL.

- You might encounter SQL error 33134, which contains the Microsoft Entra ID-specific error message. The error usually says that access is denied, that the user must enroll in MFA to access the resource, or that access between first-party applications must be handled via preauthorization. In the first two cases, the issue is usually caused by Conditional Access policies that are set in the user's Microsoft Entra tenant: they prevent the user from accessing the external provider. Updating the Conditional Access policies to allow access to the application '00000003-0000-0000-c000-000000000000' (the application ID of the Microsoft Graph API) should resolve the issue. If the error says access between first-party applications must be handled via preauthorization, the issue is because the user is signed in as a service principal. The command should succeed if it's executed by a user instead.
- If you receive a **Connection Timeout Expired**, you might need to set the `TransparentNetworkIPResolution`
parameter of the connection string to false. For more information, see [Connection timeout issue with .NET Framework 4.6.1 - TransparentNetworkIPResolution](/archive/blogs/dataaccesstechnologies/connection-timeout-issue-with-net-framework-4-6-1-transparentnetworkipresolution).

> [!IMPORTANT]  
> Removing the Microsoft Entra administrator for the server prevents any Microsoft Entra authentication user from connecting to the server. If necessary, a SQL Database administrator can drop unusable Microsoft Entra users manually.

When you create a database user, that user receives the **CONNECT** permission and can connect to that database as a member of the **PUBLIC** role. Initially, the only permissions available to the user are granted to the **PUBLIC** role and to any Microsoft Entra groups where they're a member. Granting permissions to Microsoft Entra-based contained database users operates the same way as granting permission to any other type of user. It's recommended to grant permissions to database roles and add users to those roles rather than directly granting permissions to individual users. For more information, see [Database Engine Permission Basics](https://techcommunity.microsoft.com/t5/sql-server-blog/database-engine-permission-basics/ba-p/383905). For more information about special SQL Database roles, see [Managing Databases and Logins in Azure SQL Database](logins-create-manage.md).
A federated domain user account that is imported into a managed domain as an external user, must use the managed domain identity.

Microsoft Entra users are marked in the database metadata with type E (EXTERNAL_USER) and for groups with type X (EXTERNAL_GROUPS). For more information, see [sys.database_principals](/sql/relational-databases/system-catalog-views/sys-database-principals-transact-sql).

## Connect to the database using SSMS or SSDT

To confirm the Microsoft Entra administrator is properly set up, connect to the `master` database using the Microsoft Entra administrator account.
To create a Microsoft Entra-based contained database user, connect to the database with a Microsoft Entra identity with access to the database and at least the `ALTER ANY USER` permission.
<a name='using-an-azure-ad-identity-to-connect-using-ssms-or-ssdt'></a>  
<a id="using-a-microsoft-entra-identity-to-connect-using-ssms-or-ssdt"></a>

## Use a Microsoft Entra identity to connect using SSMS or SSDT

The following procedures show you how to connect to SQL Database with a Microsoft Entra identity using SQL Server Management Studio (SSMS) or SQL Server Database Tools (SSDT).

<a name='azure-active-directory---integrated'></a>

### Microsoft Entra ID - Integrated

Use this method if you're logged into Windows using your Microsoft Entra credentials from a federated domain, or a managed domain configured for seamless single sign-on for pass-through and password hash authentication. For more information, see [Microsoft Entra seamless single sign-on](/azure/active-directory/hybrid/how-to-connect-sso).

1. Start SSMS or SSDT and in the **Connect to Server** (or **Connect to Database Engine**) dialog box, in the **Authentication** box, select **Azure Active Directory - Integrated**. No need to enter a password because your existing credentials are presented for the connection.

    :::image type="content" source="media/authentication-aad-configure/sql-server-management-studio-microsoft-entra-integrated-authentication.png" alt-text="Screenshot from SSMS showing Microsoft Entra Integrated authentication.":::

1. Select the **Options** button, and on the **Connection Properties** page, in the **Connect to database** box, type the name of the user database you want to connect to.

   :::image type="content" source="media/authentication-aad-configure/sql-server-management-studio-connect-to-server-options.png" alt-text="Screenshot from SSMS of the Options menu.":::

<a name='azure-active-directory---password'></a>

### Microsoft Entra ID - Password

Use this method when connecting with a Microsoft Entra principal name using the Microsoft Entra managed domain. You can also use it for federated accounts without access to the domain, for example, when working remotely.

Use this method to authenticate to the database in SQL Database or the SQL Managed Instance with Microsoft Entra cloud-only identity users, or those who use Microsoft Entra hybrid identities. This method supports users who want to use their Windows credential, but their local machine isn't joined with the domain (for example, using remote access). In this case, a Windows user can indicate their domain account and password, and can authenticate to the database in SQL Database, the SQL Managed Instance, or Azure Synapse.

1. Start SSMS or SSDT and in the **Connect to Server** (or **Connect to Database Engine**) dialog box, in the **Authentication** box, select **Azure Active Directory - Password**.

1. In the **User name** box, type your Microsoft Entra user name in the format `username\@domain.com`. User names must be an account from Microsoft Entra ID or an account from a managed or federated domain with Microsoft Entra ID.

1. In the **Password** box, type your user password for the Microsoft Entra account or managed/federated domain account.

    :::image type="content" source="media/authentication-aad-configure/sql-server-management-studio-microsoft-entra-password-authentication.png" alt-text="Screenshot from SSMS using Microsoft Entra Password authentication.":::

1. Select the **Options** button, and on the **Connection Properties** page, in the **Connect to database** box, type the name of the user database you want to connect to. (See the graphic in the previous option.)

<a name='azure-active-directory---universal-with-mfa'></a>

### Microsoft Entra ID - Universal with MFA

Use this method for interactive authentication with multifactor authentication (MFA), with the password being requested interactively. This method can be used to authenticate to databases in SQL Database, SQL Managed Instance, and Azure Synapse for Microsoft Entra cloud-only identity users, or those who use Microsoft Entra hybrid identities.

For more information, see [Using multi-factor Microsoft Entra authentication with SQL Database and Azure Synapse (SSMS support for MFA)](authentication-mfa-ssms-overview.md).

<a name='azure-active-directory---service-principal'></a>

### Microsoft Entra ID - Service Principal

Use this method to authenticate to the database in SQL Database or SQL Managed Instance with Microsoft Entra service principals (Microsoft Entra applications).  For more information, see [Microsoft Entra service principal with Azure SQL](authentication-aad-service-principal.md).

<a name='azure-active-directory---managed-identity'></a>

### Microsoft Entra ID - Managed Identity

Use this method to authenticate to the database in SQL Database or SQL Managed Instance with Microsoft Entra managed identities.  For more information, see [Managed identities in Microsoft Entra for Azure SQL](authentication-azure-ad-user-assigned-managed-identity.md).

<a name='azure-active-directory---default'></a>

### Microsoft Entra ID - Default

The Default authentication option with Microsoft Entra ID enables authentication that's performed through password-less and non-interactive mechanisms including managed identities.

## Use Microsoft Entra identity to connect using Azure portal Query editor for Azure SQL Database

For more information on the Azure portal Query editor for Azure SQL Database, see [Quickstart: Use the Azure portal query editor to query Azure SQL Database](connect-query-portal.md).

1. Navigate to your SQL database in the Azure portal. For example, visit [your Azure SQL dashboard](https://portal.azure.com/#view/HubsExtension/BrowseResource/resourceType/Microsoft.Sql%2Fazuresql).

1. On your SQL database **Overview** page in the [Azure portal](https://portal.azure.com), select **Query editor** from the left menu.

1. On the sign-in screen under **Welcome to SQL Database Query Editor**, select **Continue as \<your user or group ID>**.

   :::image type="content" source="media/authentication-aad-configure/query-editor-login-menu.png" alt-text="Screenshot showing sign-in to the Azure portal Query editor with Microsoft Entra authentication." lightbox="media/authentication-aad-configure/query-editor-login-menu.png":::

<a name='using-an-azure-ad-identity-to-connect-from-a-client-application'></a>

## <a id="using-a-microsoft-entra-identity-to-connect-from-a-client-application"></a> Use a Microsoft Entra identity to connect from a client application

The following procedures show you how to connect to a SQL Database with a Microsoft Entra identity from a client application. This isn't a comprehensive list of authentication methods when using a Microsoft Entra identity. For more information, see [Connect to Azure SQL with Microsoft Entra authentication and SqlClient](/sql/connect/ado-net/sql/azure-active-directory-authentication).

<a name='azure-active-directory-integrated-authentication'></a>

### Microsoft Entra integrated authentication

To use integrated Windows authentication, your domain's Active Directory must be federated with Microsoft Entra ID, or should be a managed domain that is configured for seamless single sign-on for pass-through or password hash authentication. For more information, see [Microsoft Entra seamless single sign-on](/azure/active-directory/hybrid/how-to-connect-sso).

Your client application (or a service) connecting to the database must be running on a domain-joined machine under a user's domain credentials.

To connect to a database using integrated authentication and a Microsoft Entra identity, the Authentication keyword in the database connection string must be set to `Active Directory Integrated`. Replace `<database_name>` with your database name. The following C# code sample uses ADO .NET.

```csharp
string ConnectionString = @"Data Source=<database_name>.database.windows.net; Authentication=Active Directory Integrated; Initial Catalog=testdb;";
SqlConnection conn = new SqlConnection(ConnectionString);
conn.Open();
```

The connection string keyword `Integrated Security=True` isn't supported for connecting to Azure SQL Database. When making an ODBC connection, you need to remove spaces and set authentication to `ActiveDirectoryIntegrated`.

<a name='azure-active-directory-password-authentication'></a>

### Microsoft Entra password authentication

To connect to a database using Microsoft Entra cloud-only identity user accounts, or those who use Microsoft Entra hybrid identities, the Authentication keyword must be set to `Active Directory Password`. The connection string must contain User ID/UID and Password/PWD keywords and values. Replace `<database_name>`, `<email_address>`, and `<password>` with the appropriate values. The following C# code sample uses ADO .NET.

```csharp
string ConnectionString =
@"Data Source=<database_name>.database.windows.net; Authentication=Active Directory Password; Initial Catalog=testdb; UID=<email_address>; PWD=<password>";
SqlConnection conn = new SqlConnection(ConnectionString);
conn.Open();
```

Learn more about Microsoft Entra authentication methods using the demo code samples available at [Microsoft Entra authentication GitHub Demo](https://github.com/Microsoft/sql-server-samples/tree/master/samples/features/security/azure-active-directory-auth).

<a name='azure-active-directory-access-token'></a>

### Microsoft Entra ID access token

This authentication method allows middle-tier services to obtain [JSON Web Tokens (JWT)](/azure/active-directory/develop/id-tokens) to connect to the database in SQL Database, the SQL Managed Instance, or Azure Synapse by obtaining a token from Microsoft Entra ID. This method enables various application scenarios including service identities, service principals, and applications using certificate-based authentication. You must complete four basic steps to use Microsoft Entra token authentication:

1. Register your application with Microsoft Entra ID and get the client ID for your code.
1. Create a database user representing the application. (Completed earlier in the section [Create contained users mapped to Microsoft Entra identities](#create-contained-users-mapped-to-microsoft-entra-identities).)
1. Create a certificate on the client computer runs the application.
1. Add the certificate as a key for your application.

Sample connection string. Replace `<database_name>` with your database name:

```csharp
string ConnectionString = @"Data Source=<database_name>.database.windows.net; Initial Catalog=testdb;";
SqlConnection conn = new SqlConnection(ConnectionString);
conn.AccessToken = "Your JWT token";
conn.Open();
```

For more information, see [SQL Server Security Blog](/archive/blogs/sqlsecurity/token-based-authentication-support-for-azure-sql-db-using-azure-ad-auth). For information about adding a certificate, see [Get started with certificate-based authentication in Microsoft Entra ID](/azure/active-directory/authentication/active-directory-certificate-based-authentication-get-started).

### sqlcmd

The following statements connect using version 13.1 of sqlcmd. [Download Microsoft Command Line Utilities 14.0 for SQL Server](https://www.microsoft.com/download/details.aspx?id=53591).

> [!NOTE]  
> `sqlcmd` with the `-G` command does not work with system identities, and requires a user principal login.

```cmd
sqlcmd -S Target_DB_or_DW.testsrv.database.windows.net -G
sqlcmd -S Target_DB_or_DW.testsrv.database.windows.net -U bob@contoso.com -P MyAADPassword -G -l 30
```

<a name='troubleshoot-azure-ad-authentication'></a>

## Troubleshoot Microsoft Entra authentication

For guidance on troubleshooting issues with Microsoft Entra authentication, see [Blog: Troubleshooting problems related to Azure AD authentication with Azure SQL DB and DW](https://techcommunity.microsoft.com/t5/azure-sql-database/troubleshooting-problems-related-to-azure-ad-authentication-with/ba-p/1062991).

## Related content

- [Logins, users, database roles, and user accounts](logins-create-manage.md)
- [Principals](/sql/relational-databases/security/authentication-access/principals-database-engine)
- [Database roles](/sql/relational-databases/security/authentication-access/database-level-roles)
- [SQL Database firewall rules](firewall-configure.md)
- [Create Microsoft Entra guest users and set as a Microsoft Entra admin](authentication-aad-guest-users.md)
- [Create Microsoft Entra users using Microsoft Entra applications](authentication-aad-service-principal-tutorial.md)
