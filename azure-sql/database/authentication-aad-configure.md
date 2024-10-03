---
title: Configure Microsoft Entra authentication
titleSuffix: Azure SQL Database & SQL Managed Instance & Azure Synapse Analytics
description: Learn how to connect to Azure SQL Database, Azure SQL Managed Instance, and Azure Synapse Analytics using the Microsoft Entra authentication.
author: nofield
ms.author: nofield
ms.reviewer: wiassaf, vanto, mathoma, maghan
ms.date: 09/27/2024
ms.service: azure-sql
ms.subservice: security
ms.topic: how-to
ms.custom:
  - azure-synapse
  - has-adal-ref
  - sqldbrb=2
  - devx-track-azurepowershell
  - has-azure-ad-ps-ref, azure-ad-ref-level-one-done
monikerRange: "=azuresql || =azuresql-db || =azuresql-mi"
---

# Configure and manage Microsoft Entra authentication with Azure SQL

[!INCLUDE [appliesto-sqldb-sqlmi-asa](../includes/appliesto-sqldb-sqlmi-asa.md)]

This article shows you how to use [Microsoft Entra ID for authentication](authentication-aad-overview.md) with [Azure SQL Database](sql-database-paas-overview.md), [Azure SQL Managed Instance](../managed-instance/sql-managed-instance-paas-overview.md), and [Azure Synapse Analytics](/azure/synapse-analytics/sql-data-warehouse/sql-data-warehouse-overview-what-is).

[!INCLUDE [entra-id](../includes/entra-id.md)]

Alternatively, you can also [configure Microsoft Entra authentication for SQL Server on Azure Virtual Machines](../virtual-machines/windows/configure-azure-ad-authentication-for-sql-vm.md).

<a id='azure-ad-authentication-methods'></a>

## Prerequisites

To use Microsoft Entra authentication with your Azure SQL resource, you need the following prerequisites:

- A Microsoft Entra tenant populated with users and groups.
- An existing Azure SQL resource, such as [Azure SQL Database](single-database-create-quickstart.md), or [Azure SQL Managed Instance](../managed-instance/instance-create-quickstart.md).

### Create and populate a Microsoft Entra tenant

Before you can configure Microsoft Entra authentication for your Azure SQL resource, you need to create a Microsoft Entra tenant and populate it with users and groups. Microsoft Entra tenants can be managed entirely within Azure or used for the federation of an on-premises Active Directory Domain Service.

For more information, see:

- [What is Microsoft Entra ID?](/entra/fundamentals/whatis)
- [Integrating your on-premises identities with Microsoft Entra ID](/entra/identity/hybrid/whatis-hybrid-identity)
- [Add your domain name to Microsoft Entra ID](/entra/fundamentals/add-custom-domain)
- [What is federation with Microsoft Entra ID?](/entra/identity/hybrid/connect/whatis-fed)
- [Directory synchronization with Microsoft Entra ID](/entra/architecture/sync-directory)
- [Manage Microsoft Entra ID using Windows PowerShell](/powershell/module/azuread)
- [Hybrid Identity Required Ports and Protocols](/entra/identity/hybrid/connect/reference-connect-ports)

<a id='provision-azure-ad-admin-sql-database'></a>
<a id='create-and-populate-an-azure-ad-instance'></a>
<a id='associate-or-add-an-azure-subscription-to-azure-active-directory'></a>

## Set Microsoft Entra admin

To use Microsoft Entra authentication with your resource, it needs to have the Microsoft Entra administrator set. While conceptually the steps are the same for Azure SQL Database, Azure Synapse Analytics, and Azure SQL Managed Instance, this section describes in detail the different APIs and portal experiences to do so per product.

The Microsoft Entra admin can also be configured when the Azure SQL resource is created. If a Microsoft Entra admin is already configured, skip this section.

<a id='azure-ad-admin-with-a-server-in-sql-database'></a>
<a id='provision-azure-ad-admin-sql-database'></a>

### Azure SQL Database and Azure Synapse Analytics

Setting the Microsoft Entra admin enables Microsoft Entra authentication for your [logical server](logical-servers.md) for Azure SQL Database and Azure Synapse Analytics. You can set a Microsoft Entra admin for your server by using the Azure portal, PowerShell, Azure CLI, or REST APIs.

In the Azure portal, you can find the **logical server** name 

- In the **server name** field on the **Overview** page of Azure SQL Database.
- In the **server name** field on the **Overview** page of your standalone dedicated SQL pool in Azure Synapse Analytics.
- In the relevant **SQL endpoint** on the **Overview** page of your Azure Synapse Analytics workspace.

#### [Azure portal](#tab/azure-portal)

To set the Microsoft Entra admin for your logical server in the Azure portal, follow these steps:

1. In the [Azure portal **Directories + subscriptions pane**](https://portal.azure.com/#settings/directory), choose the directory that contains your Azure SQL resource as the **Current directory**.

1. Search for **SQL servers** and then select the logical server for your database resource to open the **SQL server** pane.

    :::image type="content" source="media/authentication-aad-configure/search-for-and-select-sql-servers.png" alt-text="Screenshot showing how to search for and select SQL servers.":::

1. On the **SQL server** pane for your logical server, select **Microsoft Entra ID** under **Settings** to open the **Microsoft Entra ID** pane.

1. On the **Microsoft Entra ID** pane, select **Set admin** to open the **Microsoft Entra ID** pane.

    :::image type="content" source="media/authentication-aad-configure/sql-servers-set-active-directory-admin.png" alt-text="Screenshot shows the option to set the Microsoft Entra admin for SQL servers." lightbox="media/authentication-aad-configure/sql-servers-set-active-directory-admin.png":::

1. The **Microsoft Entra ID** pane shows all users, groups, and applications in your current directory and allows you to search by name, alias, or ID. Find your desired identity for your Microsoft Entra admin and select it, then click **Select** to close the pane.

1. At the top of the **Microsoft Entra ID** page for your logical server, select **Save**.

    :::image type="content" source="media/authentication-aad-configure/save.png" alt-text="Screenshot shows the option to save a Microsoft Entra admin." lightbox="media/authentication-aad-configure/save.png":::

    The **Object ID** is displayed next to the admin name for Microsoft Entra users and groups. For applications (service principals), the **Application ID** is displayed.

The process of changing the administrator might take several minutes. Then the new administrator appears in the **Microsoft Entra admin** field.

To remove the admin, at the top of the **Microsoft Entra ID** page, select **Remove admin**, then select **Save**. Removing the Microsoft Entra admin disables Microsoft Entra authentication for your logical server.

#### [PowerShell](#tab/azure-powershell)

To run PowerShell cmdlets, you need to have Azure PowerShell installed and running. See [How to install and configure Azure PowerShell](/powershell/azure/) for detailed information.

The following Azure PowerShell cmdlets can be used to set and manage a Microsoft Entra admin for Azure SQL Database and Azure Synapse Analytics:

| Cmdlet name | Description |
| --- | --- |
| [Set-AzSqlServerActiveDirectoryAdministrator](/powershell/module/az.sql/set-azsqlserveractivedirectoryadministrator) | Sets a Microsoft Entra administrator for the server hosting SQL Database or Azure Synapse. |
| [Remove-AzSqlServerActiveDirectoryAdministrator](/powershell/module/az.sql/remove-azsqlserveractivedirectoryadministrator) | Removes a Microsoft Entra administrator for the server hosting SQL Database or Azure Synapse. |
| [Get-AzSqlServerActiveDirectoryAdministrator](/powershell/module/az.sql/get-azsqlserveractivedirectoryadministrator) | Returns information about a Microsoft Entra administrator currently configured for the server hosting SQL Database or Azure Synapse. |

Use PowerShell command get-help to see more information for each of these commands. For example, `get-help Set-AzSqlServerActiveDirectoryAdministrator`.

The following script sets a Microsoft Entra administrator group named **DBA_Group** (sample object ID `aaaaaaaa-0000-1111-2222-bbbbbbbbbbbb`) for the sample server **example-server** in a sample resource group named **Example-Resource-Group**:

```powershell
$parameters = @{
    ResourceGroupName = "Example-Resource-Group"
    ServerName = "example-server"
    DisplayName = "DBA_Group"
}

Set-AzSqlServerActiveDirectoryAdministrator @parameters
```

The **DisplayName** parameter accepts either the Microsoft Entra ID display name or the User Principal Name, such as the following examples: ``DisplayName="Adrian King"`` and ``DisplayName="adrian@contoso.com"``. If you're using a Microsoft Entra group, then only the display name is supported.

The following example uses the optional **ObjectID** parameter:

```powershell
$parameters = @{
  ResourceGroupName = "Example-Resource-Group"
  ServerName = "example-server"
  DisplayName = "DBA_Group"
  ObjectId = "aaaaaaaa-0000-1111-2222-bbbbbbbbbbbb"
}
Set-AzSqlServerActiveDirectoryAdministrator @parameters
```

> [!NOTE]  
> The **ObjectID** is required when the **DisplayName** is not unique. To retrieve the **ObjectID** and **DisplayName** values, you can view the properties of a user or group in the Microsoft Entra ID section of the Azure portal.

The following example returns information about the current Microsoft Entra admin for the server:

```powershell
$parameters = @{
    ResourceGroupName = "Example-Resource-Group"
    ServerName = "example-server"
}

Get-AzSqlServerActiveDirectoryAdministrator @parameters | Format-List
```

The following example removes a Microsoft Entra administrator:

```powershell
$parameters = @{
    ResourceGroupName = "Example-Resource-Group"
    ServerName = "example-server"
}

Remove-AzSqlServerActiveDirectoryAdministrator @parameters
```

#### [Azure CLI](#tab/azure-cli)

You can set a Microsoft Entra admin for Azure SQL Database and Azure Synapse Analytics with the following Azure CLI commands:

| Command | Description |
| --- | --- |
| [az sql server ad-admin create](/cli/azure/sql/server/ad-admin#az-sql-server-ad-admin-create) | Sets a Microsoft Entra administrator for the logical server hosting SQL Database or Azure Synapse Analytics. |
| [az sql server ad-admin delete](/cli/azure/sql/server/ad-admin#az-sql-server-ad-admin-delete) | Removes a Microsoft Entra administrator from the logical server hosting the SQL Database or Azure Synapse Analytics. |
| [az sql server ad-admin list](/cli/azure/sql/server/ad-admin#az-sql-server-ad-admin-list) | Returns information about the Microsoft Entra administrator configured for the server hosting the SQL Database or Azure Synapse Analytics. |
| [az sql server ad-admin update](/cli/azure/sql/server/ad-admin#az-sql-server-ad-admin-update) | Updates the Microsoft Entra administrator for the server hosting the SQL Database or Azure Synapse Analytics. |

For more CLI commands, see [az sql server](/cli/azure/sql/server).

#### [REST APIs](#tab/rest-apis)

You can also use the [Server Azure AD Administrator](/rest/api/sql/server-azure-ad-administrators) REST APIs to create, update, delete, and get the Microsoft Entra administrator for Azure SQL Database and Azure Synapse Analytics.

| API | Description |
| --- | --- |
| [Create Or Update](/rest/api/sql/server-azure-ad-administrators/create-or-update) | Creates or updates an existing Microsoft Entra administrator. |
| [Delete](/rest/api/sql/server-azure-ad-administrators/delete) | Deletes the Microsoft Entra administrator with the given name. |
| [Get](/rest/api/sql/server-azure-ad-administrators/get) | Gets the Microsoft Entra administrator for the named server. |
| [List By Server](/rest/api/sql/server-azure-ad-administrators/list-by-server) | Gets a list of Microsoft Entra administrators in a server. |

---

> [!NOTE]  
> The Microsoft Entra admin is stored in the server's `master` database as a user (database principal). Since database principal names must be unique, the display name of the admin can't be the same as the name of any user in the server's `master` database. If a user with the name already exists, the Microsoft Entra admin setup fails and rolls back, indicating that the name is already in use.

<a id='provision-azure-ad-admin-sql-managed-instance'></a>

### Azure SQL Managed Instance

Setting the Microsoft Entra admin enables Microsoft Entra authentication for Azure SQL Managed Instance. You can set a Microsoft Entra admin for your SQL managed instance by using the Azure portal, PowerShell, Azure CLI, or REST APIs.

#### [Azure portal](#tab/azure-portal)

To grant your SQL managed instance read permissions to Microsoft Entra ID by using the Azure portal, sign in as a **Global Administrator** or **Privileged Role Administrator** and follow these steps:

1. In the [Azure portal](https://portal.azure.com), in the upper-right corner select your account, and then choose **Switch directories** to confirm which directory is your **Current directory**. Switch directories, if necessary.

   :::image type="content" source="media/authentication-aad-configure/switch-directory.png" alt-text="Screenshot of the Azure portal showing where to switch your directory.":::

1. In the [Azure portal **Directories + subscriptions pane**](https://portal.azure.com/#settings/directory), choose the directory that contains your managed instance as the **Current directory**.```


1. Search for **SQL managed instances** and then select your managed instance to open the **SQL managed instance** pane. Then,  select **Microsoft Entra ID** under **Settings** to open the **Microsoft Entra ID** pane for your instance.

   :::image type="content" source="media/authentication-aad-configure/active-directory-pane.png" alt-text="Screenshot of the Azure portal showing the Microsoft Entra admin page open for the selected SQL managed instance." lightbox="media/authentication-aad-configure/active-directory-pane.png":::

1. On the **Microsoft Entra admin** pane, select **Set admin** from the navigation bar to open the **Microsoft Entra ID** pane.

    :::image type="content" source="media/authentication-aad-configure/set-admin.png" alt-text="Screenshot showing the Set admin command highlighted on the Microsoft Entra admin page for the selected SQL managed instance." lightbox="media/authentication-aad-configure/set-admin.png":::

1. On the **Microsoft Entra ID** pane, search for a user, check the box next to the user or group to be an administrator, and then press **Select** to close the pane and go back to the **Microsoft Entra admin** page for your managed instance.

   The **Microsoft Entra ID** pane shows all members and groups within your current directory. Grayed-out users or groups can't be selected because they aren't supported as Microsoft Entra administrators. Select the identity you want to assign as your administrator.

1. From the navigation bar of the **Microsoft Entra admin** page for your managed instance, select **Save** to confirm your Microsoft Entra administrator.

    :::image type="content" source="media/authentication-aad-configure/save.png" alt-text="Screenshot of the Microsoft Entra admin page with the Save button in the top row next to the Set admin and Remove admin buttons." lightbox="media/authentication-aad-configure/save.png":::

    After the administrator change operation completes, the new administrator appears in the Microsoft Entra admin field.

    The **Object ID** is displayed next to the admin name for Microsoft Entra users and groups. For applications (service principals), the **Application ID** is displayed.

> [!TIP]  
> To remove the admin, select **Remove admin** at the top of the Microsoft Entra ID page, then select **Save**.

#### [PowerShell](#tab/azure-powershell)

To run PowerShell cmdlets, you need to have Azure PowerShell installed and running. See [How to install and configure Azure PowerShell](/powershell/azure/) for detailed information.

The following table lists the PowerShell cmdlets you can use to define and manage the Microsoft Entra admin for managed instances:

| Cmdlet name | Description |
| --- | --- |
| [Set-AzSqlInstanceActiveDirectoryAdministrator](/powershell/module/az.sql/set-azsqlinstanceactivedirectoryadministrator) | Sets a Microsoft Entra administrator for the managed instance. |
| [Remove-AzSqlInstanceActiveDirectoryAdministrator](/powershell/module/az.sql/remove-azsqlinstanceactivedirectoryadministrator) | Removes the Microsoft Entra administrator for the managed instance. |
| [Get-AzSqlInstanceActiveDirectoryAdministrator](/powershell/module/az.sql/get-azsqlinstanceactivedirectoryadministrator) | Returns information about the Microsoft Entra administrator for the managed instance. |

This example command gets information about a Microsoft Entra administrator for a managed instance named "Sample-Instance" associated with a resource group named "Example-Resource-Group".

```powershell
$parameters = @{
    ResourceGroupName = "Example-Resource-Group"
    InstanceName = "Sample-Instance"
}

Get-AzSqlInstanceActiveDirectoryAdministrator @parameters
```

This example command sets the Microsoft Entra administrator to a group named DBAs (with sample object ID `aaaaaaaa-0000-1111-2222-bbbbbbbbbbbb`) for the SQL Managed Instance named "Sample-Instance". This server is associated with the resource group "Example-Resource-Group".

```powershell
$parameters = @{
    ResourceGroupName = "Example-Resource-Group"
    InstanceName = "Sample-Instance"
    DisplayName = "DBAs"
    ObjectId = "aaaaaaaa-0000-1111-2222-bbbbbbbbbbbb"
}

Set-AzSqlInstanceActiveDirectoryAdministrator @parameters
```

This example command removes the Microsoft Entra administrator for the SQL Managed Instance named "Sample-Instance" associated with the resource group "Example-Resource-Group".

```powershell
$parameters = @{
    ResourceGroupName = "Example-Resource-Group"
    InstanceName = "Sample-Instance"
    Confirm = $true
    PassThru = $true
}

Remove-AzSqlInstanceActiveDirectoryAdministrator @parameters
```

#### [Azure CLI](#tab/azure-cli)

You can set and manage a Microsoft Entra admin for the SQL Managed Instance by calling the following Azure CLI commands:

| Command | Description |
| --- | --- |
| [az sql mi ad-admin create](/cli/azure/sql/mi/ad-admin#az-sql-mi-ad-admin-create) | Provisions a Microsoft Entra administrator for the SQL managed instance. |
| [az sql mi ad-admin delete](/cli/azure/sql/mi/ad-admin#az-sql-mi-ad-admin-delete) | Removes a Microsoft Entra administrator for the SQL managed instance. |
| [az sql mi ad-admin list](/cli/azure/sql/mi/ad-admin#az-sql-mi-ad-admin-list) | Returns information about a Microsoft Entra administrator currently configured for the SQL managed instance. |
| [az sql mi ad-admin update](/cli/azure/sql/mi/ad-admin#az-sql-mi-ad-admin-update) | Updates the Microsoft Entra administrator for the SQL managed instance. |

For more CLI commands, see [az sql mi](/cli/azure/sql/mi).

#### [REST APIs](#tab/rest-apis)

You can use the [Managed Instance Administrators](/rest/api/sql/managed-instance-administrators) REST APIs to create, update, delete, and get the Microsoft Entra administrator for SQL managed instances.

| API | Description |
| --- | --- |
| [Create Or Update](/rest/api/sql/managed-instance-administrators/create-or-update) | Creates or updates an existing Microsoft Entra administrator. |
| [Delete](/rest/api/sql/managed-instance-administrators/delete) | Deletes the Microsoft Entra administrator with the given name from the named managed instance. |
| [Get](/rest/api/sql/managed-instance-administrators/get) | Gets the Microsoft Entra administrator for the named managed instance. |
| [List By Instance](/rest/api/sql/managed-instance-administrators/list-by-instance) | Gets a list of Microsoft Entra administrators in a managed instance. |

---

## Assign Microsoft Graph permissions

SQL Managed Instance needs permissions to read Microsoft Entra ID for scenarios like authorizing users who connect through security group membership and new user creation. For Microsoft Entra authentication to work, you need to assign the managed instance identity to the **Directory Readers** role. You can do this using the Azure portal or PowerShell.

For some operations, Azure SQL Database and Azure Synapse Analytics also require permissions to query Microsoft Graph, explained in [Microsoft Graph permissions](./authentication-aad-overview.md#microsoft-graph-permissions). Azure SQL Database and Azure Synapse Analytics support fine-grained Graph permissions for these scenarios, whereas SQL Managed Instance requires the **Directory Readers** role. Fine-grained permissions and their assignment are described in detail in [enable service principals to create Microsoft Entra users](authentication-aad-service-principal.md#enable-service-principals-to-create-azure-ad-users).

### Directory Readers role

#### [Azure portal](#tab/azure-portal)

The **Microsoft Entra ID** page for SQL Managed Instance in the Azure portal displays a convenient banner when the instance isn't assigned the Directory Reader permissions.

1. Select the banner on top of the **Microsoft Entra ID** page and grant permission to the system-assigned or user-assigned managed identity that represents your instance. Only a Global Administrator or Privileged Role Administrator in your tenant can perform this operation.

    :::image type="content" source="media/authentication-aad-configure/grant-permissions.png" alt-text="Screenshot of the dialog for granting permissions to a SQL managed instance for accessing Microsoft Entra ID with the Grant permissions button selected.":::

1. When the operation succeeds, a **Success** notification shows in the top-right corner:

    :::image type="content" source="media/authentication-aad-configure/success.png" alt-text="Screenshot of a notification confirming that Microsoft Entra ID read permissions are successfully updated for the managed instance.":::

#### [PowerShell](#tab/azure-powershell)

The following PowerShell script adds an identity to the Directory Readers role. This can be used to assign permissions to a managed instance or primary server identity for the logical server (or any Microsoft Entra identity).

```powershell
# This script grants "Directory Readers" permission to a service principal representing a SQL Managed Instance or logical server.
# It can be executed only by a user who is a member of the **Global Administrator** or **Privileged Roles Administrator** role.

Import-Module Microsoft.Graph.Authentication
$instanceName = "<InstanceName>"        # Enter the name of your managed instance or server
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

# Get service principal for your SQL Managed Instance or logical server
$roleMember = Get-MgServicePrincipal -Filter "DisplayName eq '$instanceName'"
$roleMember.Count
if ($roleMember -eq $null) {
    Write-Output "Error: No service principal with name '$($instanceName)' found, make sure that instanceName parameter was entered correctly."
    exit
}
if (-not ($roleMember.Count -eq 1)) {
    Write-Output "Error: Multiple service principals with name '$($instanceName)'"
    Write-Output $roleMember | Format-List DisplayName, Id, AppId
    exit
}

# Check if service principal is already member of Directory Readers role
$isDirReader = Get-MgDirectoryRoleMember -DirectoryRoleId $role.Id -Filter "Id eq '$($roleMember.Id)'"
if ($isDirReader -eq $null) {
    # Add principal to Directory Readers role
    Write-Output "Adding service principal '$($instanceName)' to 'Directory Readers' role..."
    $body = @{
        "@odata.id"= "https://graph.microsoft.com/v1.0/directoryObjects/{$($roleMember.Id)}"
    }
    New-MgDirectoryRoleMemberByRef -DirectoryRoleId $role.Id -BodyParameter $body
    Write-Output "'$($instanceName)' service principal added to 'Directory Readers' role."
} else {
    Write-Output "Service principal '$($instanceName)' is already member of 'Directory Readers' role."
}
```

#### [Azure CLI](#tab/azure-cli)

See details for how to assign Azure roles via the Azure CLI here [Assign Azure roles using Azure CLI](/azure/role-based-access-control/role-assignments-cli).

#### [REST APIs](#tab/rest-apis)

[Assign Azure roles using the REST API](/azure/role-based-access-control/role-assignments-rest).

---

The Microsoft Entra admin can now be used to create Microsoft Entra server principals (logins) and database principals (users). For more information, see [Microsoft Entra integration with Azure SQL Managed Instance](../managed-instance/sql-managed-instance-paas-overview.md#microsoft-entra-integration).

## Create Microsoft Entra principals in SQL

To connect to a database in SQL Database or Azure Synapse Analytics with Microsoft Entra authentication, a principal has to be configured on the database for that identity with at least the `CONNECT` permission.

### Database user permissions

When a database user is created, it receives the **CONNECT** permission to the database by default. A database user also inherits permissions in two circumstances:

- If the user is a member of a Microsoft Entra group that's also assigned permissions on the server.
- If the user is created from a login, it inherits the server-assigned permissions of the login applicable on the database.

Managing permissions for server and database principals works the same regardless of the type of principal (Microsoft Entra ID, SQL authentication, etc.). We recommend granting permissions to database roles instead of directly granting permissions to users. Then users can be added to roles with appropriate permissions. This simplifies long-term permissions management and reduces the likelihood of an identity retaining access past when is appropriate.

For more information, see:
- [Database engine permissions and examples](/sql/relational-databases/security/permissions-database-engine)
- [Blog: Database Engine permission basics](https://techcommunity.microsoft.com/t5/sql-server-blog/database-engine-permission-basics/ba-p/383905)
- [Managing special databases roles and logins in Azure SQL Database](logins-create-manage.md)

### Contained database users

A contained database user is a type of SQL user that isn't connected to a login in the `master` database. To create a Microsoft Entra contained database user, connect to the database with a Microsoft Entra identity that has at least the **ALTER ANY USER** permission. The following T-SQL example creates a database principal `Microsoft_Entra_principal_name` from Microsoft Entra ID.

```sql
CREATE USER [<Microsoft_Entra_principal_name>] FROM EXTERNAL PROVIDER;
```

To create a contained database user for a Microsoft Entra group, enter the display name of the group:

```sql
CREATE USER [ICU Nurses] FROM EXTERNAL PROVIDER;
```

To create a contained database user for a managed identity or service principal, enter the display name of the identity:

```sql
CREATE USER [appName] FROM EXTERNAL PROVIDER;
```

To create a contained database user for a Microsoft Entra user, enter the user principal name of the identity:

```sql
CREATE USER [adrian@contoso.com] FROM EXTERNAL PROVIDER;
```

### Login based users

> [!NOTE]
> [Microsoft Entra server principals (logins)](authentication-azure-ad-logins.md) are currently in public preview for Azure SQL Database and Azure Synapse Analytics. Microsoft Entra logins are generally available for Azure SQL Managed Instance and SQL Server 2022.

[Microsoft Entra server principals (or logins)](authentication-azure-ad-logins.md) are supported, which means contained database users aren't required. Database principals (users) can be created based off of a server principal, which means Microsoft Entra users can inherit server-level assigned permissions of a login.

```sql
CREATE USER [appName] FROM LOGIN [appName];
```

For more information, see [SQL Managed Instance overview](../managed-instance/sql-managed-instance-paas-overview.md#microsoft-entra-integration). For syntax on creating Microsoft Entra server principals (logins), see [CREATE LOGIN](/sql/t-sql/statements/create-login-transact-sql?view=azuresqldb-mi-current&preserve-view=true).

### External users

You can't directly create a database user for an identity managed in a different Microsoft Entra tenant than the one associated with your Azure subscription. However, users in other directories can be imported into the associated directory as external users. They can then be used to create contained database users that can access the database. External users can also gain access through membership in Microsoft Entra groups.

**Examples:**
To create a contained database user representing a Microsoft Entra federated or managed domain user:

```sql
CREATE USER [alice@fabrikam.com] FROM EXTERNAL PROVIDER;
```

A federated domain user account that is imported into a managed domain as an external user, must use the managed domain identity.

### Naming considerations

Special characters like colon `:` or ampersand `&` when included as user names in the T-SQL `CREATE LOGIN` and `CREATE USER` statements aren't supported.

Microsoft Entra ID and Azure SQL diverge in their user management design in one key way: Microsoft Entra ID allows display names to be duplicated within a tenant, whereas Azure SQL requires all server principals on a server or instance and all database principals on a database to have a unique name. Because Azure SQL directly uses the Microsoft Entra display name of the identity when creating principals, this can result in errors when creating users. To solve this issue, Azure SQL has released the `WITH OBJECT_ID` enhancement currently in preview, which allows users to specify the Microsoft Entra object ID of the identity being added to the server or instance.

### Microsoft Graph permissions

The `CREATE USER ... FROM EXTERNAL PROVIDER` command requires Azure SQL access to Microsoft Entra ID (the "external provider") on behalf of the logged-in user. Sometimes, circumstances arise that cause Microsoft Entra ID to return an exception to Azure SQL.

- You might encounter SQL error 33134, which contains the Microsoft Entra ID-specific error message. The error usually says that access is denied, that the user must enroll in MFA to access the resource, or that access between first-party applications must be handled via preauthorization. In the first two cases, the issue is usually caused by Conditional Access policies that are set in the user's Microsoft Entra tenant: they prevent the user from accessing the external provider. Updating the Conditional Access policies to allow access to the application '00000003-0000-0000-c000-000000000000' (the application ID of the Microsoft Graph API) should resolve the issue. If the error says access between first-party applications must be handled via preauthorization, the issue is because the user is signed in as a service principal. The command should succeed if it's executed by a user instead.
- If you receive a **Connection Timeout Expired**, you might need to set the `TransparentNetworkIPResolution`
parameter of the connection string to false. For more information, see [Connection timeout issue with .NET Framework 4.6.1 - TransparentNetworkIPResolution](/archive/blogs/dataaccesstechnologies/connection-timeout-issue-with-net-framework-4-6-1-transparentnetworkipresolution).

For more information about creating contained database users based on Microsoft Entra identities, see [CREATE USER](/sql/t-sql/statements/create-user-transact-sql).

## Configure multifactor authentication

For improved security to your Azure SQL resource, consider configuring [multifactor authentication (MFA)](authentication-aad-overview.md#multifactor-authentication-mfa), which prompts the user to use a second alternative method to authenticate to the database, such as a phone call or an authenticator app.

To use multifactor authentication with your Azure SQL resource, first [enable multifactor authentication](/entra/identity/authentication/concept-mfa-howitworks#how-to-enable-and-use-microsoft-entra-multifactor-authentication), and then use a [conditional access policy](conditional-access-configure.md) to enforce MFA for your Azure SQL resource.

## Connect with Microsoft Entra

After Microsoft Entra authentication has been configured, you can use it to connect to your SQL resource with Microsoft tools like [SQL Server Management Studio](authentication-microsoft-entra-connect-to-azure-sql.md#connect-with-ssms-or-ssdt) and [SQL Server Data Tools](authentication-microsoft-entra-connect-to-azure-sql.md#connect-with-ssms-or-ssdt), and configure [client applications](authentication-microsoft-entra-connect-to-azure-sql.md#connect-from-a-client-application) to connect using Microsoft Entra identities.

## Troubleshoot Microsoft Entra authentication

For guidance on troubleshooting issues, see [Blog: Troubleshooting problems related to Microsoft Entra authentication with Azure SQL Database and Azure Synapse](https://techcommunity.microsoft.com/t5/azure-sql-blog/troubleshooting-problems-related-to-azure-ad-authentication-with/ba-p/1062991).

## Related content

- [Authorize database access to SQL Database, SQL Managed Instance, and Azure Synapse Analytics](logins-create-manage.md)
- [Principals](/sql/relational-databases/security/authentication-access/principals-database-engine)
- [Database roles](/sql/relational-databases/security/authentication-access/database-level-roles)
- [Azure SQL Database and Azure Synapse IP firewall rules](firewall-configure.md)
- [Create Microsoft Entra guest users and set them as a Microsoft Entra admin](authentication-aad-guest-users.md)
- [Tutorial: Create Microsoft Entra users using Microsoft Entra applications](authentication-aad-service-principal-tutorial.md)

<a id="using-an-azure-ad-identity-to-connect-using-ssms-or-ssdt"></a>  
<a id="using-a-microsoft-entra-identity-to-connect-using-ssms-or-ssdt"></a>
<a id='using-an-azure-ad-identity-to-connect-using-ssms-or-ssdt'></a>
<a id='azure-active-directory---integrated'></a>
<a id='azure-active-directory---password'></a>
<a id='azure-active-directory---universal-with-mfa'></a>
<a id='azure-active-directory---service-principal'></a>
<a id='azure-active-directory---managed-identity'></a>
<a id='azure-active-directory---default'></a>
<a id='using-an-azure-ad-identity-to-connect-from-a-client-application'></a>
<a id='azure-active-directory-integrated-authentication'></a>
<a id='azure-active-directory-password-authentication'></a>
<a id='azure-active-directory-access-token'></a>
<a id='troubleshoot-azure-ad-authentication'></a>