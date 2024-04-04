---
title: Managed identity in Microsoft Entra for Azure SQL
titleSuffix: Azure SQL Database & Azure SQL Managed Instance
description: Learn about system assigned and user assigned managed identities in Microsoft Entra for Azure SQL Database and Azure SQL Managed Instance.
author: nofield
ms.author: nofield
ms.reviewer: vanto, wiassaf, mathoma
ms.date: 10/24/2023
ms.service: sql-db-mi
ms.subservice: security
ms.custom: has-azure-ad-ps-ref, azure-ad-ref-level-one-done
ms.topic: conceptual
monikerRange: "= azuresql || = azuresql-db || = azuresql-mi"
---

# Managed identities in Microsoft Entra for Azure SQL

[!INCLUDE[appliesto-sqldb-sqlmi](../includes/appliesto-sqldb-sqlmi.md)]

Microsoft Entra ID ([formerly Azure Active Directory](/entra/fundamentals/new-name)) supports two types of managed identities: system-assigned managed identity (SMI) and user-assigned managed identity (UMI). For more information, see [Managed identity types](/entra/identity/managed-identities-azure-resources/overview#managed-identity-types).

An SMI is automatically assigned to Azure SQL Managed Instance when it's created. When you're using Microsoft Entra authentication with Azure SQL Database, you must assign an SMI when Azure service principals are used to create Microsoft Entra users in SQL Database.

Previously, only an SMI could be assigned to the Azure SQL Managed Instance or SQL Database server identity. Now, a UMI can be assigned to SQL Managed Instance or SQL Database as the instance or server identity.

In addition to using a UMI and an SMI as the instance or server identity, you can use them to access the database by using the SQL connection string option `Authentication=Active Directory Managed Identity`. You need to create a SQL user from the managed identity in the target database by using the [CREATE USER](/sql/t-sql/statements/create-user-transact-sql) statement. For more information, see [Using Microsoft Entra authentication with SqlClient](/sql/connect/ado-net/sql/azure-active-directory-authentication).

To retrieve the current UMI(s) or SMI for Azure SQL Managed instance or Azure SQL Database, see [Get or set a managed identity for a logical server or managed instance](#get-or-set-a-managed-identity-for-a-logical-server-or-managed-instance) later in this article.

## Benefits of using user-assigned managed identities

There are several benefits of using a UMI as a server identity:

- Users have the flexibility to create and maintain their own UMIs for a tenant. You can use UMIs as server identities for Azure SQL. A UMI is managed by the user, whereas an SMI is uniquely defined per server and assigned by the system.
- In the past, you needed the Microsoft Entra ID [Directory Readers](authentication-aad-directory-readers-role.md) role when using an SMI as the server or instance identity. With the introduction of accessing Microsoft Entra ID through [Microsoft Graph](/graph/api/resources/azure-ad-overview), users who are concerned with giving high-level permissions such as the Directory Readers role to the SMI or UMI can alternatively give lower-level permissions so that the server or instance identity can access Microsoft Graph. 

  For more information on providing Directory Readers permissions and its function, see [Directory Readers role in Microsoft Entra ID for Azure SQL](authentication-aad-directory-readers-role.md).
- Users can choose a specific UMI to be the server or instance identity for all databases or managed instances in the tenant. Or they can have multiple UMIs assigned to different servers or instances. 

  UMIs can be used in different servers to represent different features. For example, a UMI can serve transparent data encryption (TDE) in one server, and a UMI can serve Microsoft Entra authentication in another server.
- You need a UMI to create a [logical server in Azure](logical-servers.md) configured with TDE with customer-managed keys (CMKs). For more information, see [Customer-managed transparent data encryption using user-assigned managed identity](transparent-data-encryption-byok-identity.md).
- UMIs are independent from logical servers or managed instances. When a logical server or instance is deleted, the SMI is also deleted. UMIs aren't deleted with the server.

> [!NOTE]
> You must enable the instance identity (SMI or UMI) to allow support for Microsoft Entra authentication in SQL Managed Instance. For SQL Database, enabling the server identity is optional and required only if a Microsoft Entra service principal (Microsoft Entra application) oversees creating and managing Microsoft Entra users, groups, or applications in the server. For more information, see [Microsoft Entra service principal with Azure SQL](authentication-aad-service-principal.md).

## <a id="creating-a-user-assigned-managed-identity"></a> Create a user-assigned managed identity

For information on how to create a UMI, see [Manage user-assigned managed identities](/entra/identity/managed-identities-azure-resources/how-manage-user-assigned-managed-identities).

## Permissions

After the UMI is created, some permissions are needed to allow the UMI to read from [Microsoft Graph](/graph/api/resources/azure-ad-overview) as the server identity. Grant the following permissions, or give the UMI the [Directory Readers](authentication-aad-directory-readers-role-tutorial.md) role. 

These permissions should be granted before you provision a logical server or managed instance. After you grant the permissions to the UMI, they're enabled for all servers or instances that are created with the UMI assigned as a server identity.

> [!IMPORTANT]
> Only a [Global Administrator](/entra/identity/role-based-access-control/permissions-reference#global-administrator) or [Privileged Role Administrator](/entra/identity/role-based-access-control/permissions-reference#privileged-role-administrator) can grant these permissions.

- [User.Read.All](/graph/permissions-reference#user-permissions): Allows access to Microsoft Entra user information.
- [GroupMember.Read.All](/graph/permissions-reference#group-permissions): Allows access to Microsoft Entra group information.
- [Application.Read.ALL](/graph/permissions-reference#application-resource-permissions): Allows access to Microsoft Entra service principal (application) information.

### Grant permissions

The following sample PowerShell script grants the necessary permissions for a managed identity. This sample assigns permissions to the user-assigned managed identity `umiservertest`.

To run the script, you must sign in as a user with a Global Administrator or Privileged Role Administrator role.

The script grants the `User.Read.All`, `GroupMember.Read.All`, and `Application.Read.ALL` permissions to a managed identity to access [Microsoft Graph](/graph/auth/auth-concepts#microsoft-graph-permissions).

```powershell
# Script to assign permissions to an existing UMI 
# The following required Microsoft Graph permissions will be assigned: 
#   User.Read.All
#   GroupMember.Read.All
#   Application.Read.All

Import-Module Microsoft.Graph.Authentication
Import-Module Microsoft.Graph.Applications

$tenantId = "<tenantId>"        # Your tenant ID
$MSIName = "<managedIdentity>"; # Name of your managed identity

# Log in as a user with the "Global Administrator" or "Privileged Role Administrator" role
Connect-MgGraph -TenantId $tenantId -Scopes "AppRoleAssignment.ReadWrite.All,Application.Read.All"

# Search for Microsoft Graph
$MSGraphSP = Get-MgServicePrincipal -Filter "DisplayName eq 'Microsoft Graph'";
$MSGraphSP

# Sample Output

# DisplayName     Id                                   AppId                                SignInAudience      ServicePrincipalType
# -----------     --                                   -----                                --------------      --------------------
# Microsoft Graph 47d73278-e43c-4cc2-a606-c500b66883ef 00000003-0000-0000-c000-000000000000 AzureADMultipleOrgs Application

$MSI = Get-MgServicePrincipal -Filter "DisplayName eq '$MSIName'" 
if($MSI.Count -gt 1)
{ 
Write-Output "More than 1 principal found with that name, please find your principal and copy its object ID. Replace the above line with the syntax $MSI = Get-MgServicePrincipal -ServicePrincipalId <your_object_id>"
Exit
}

# Get required permissions
$Permissions = @(
  "User.Read.All"
  "GroupMember.Read.All"
  "Application.Read.All"
)

# Find app permissions within Microsoft Graph application
$MSGraphAppRoles = $MSGraphSP.AppRoles | Where-Object {($_.Value -in $Permissions)}

# Assign the managed identity app roles for each permission
foreach($AppRole in $MSGraphAppRoles)
{
    $AppRoleAssignment = @{
	    principalId = $MSI.Id
	    resourceId = $MSGraphSP.Id
	    appRoleId = $AppRole.Id
    }

    New-MgServicePrincipalAppRoleAssignment `
    -ServicePrincipalId $AppRoleAssignment.PrincipalId `
    -BodyParameter $AppRoleAssignment -Verbose
}
```

### Check permissions for user-assigned managed identity

To check permissions for a UMI, go to the [Azure portal](https://portal.azure.com). In the **Microsoft Entra ID** resource, go to **Enterprise applications**. Select **All Applications** for **Application type**, and search for the UMI that was created.

:::image type="content" source="media/authentication-azure-ad-user-assigned-managed-identity/azure-ad-search-enterprise-applications.png" alt-text="Screenshot of enterprise application settings in the Azure portal.":::

Select the UMI, and go to the **Permissions** settings under **Security**.

:::image type="content" source="media/authentication-azure-ad-user-assigned-managed-identity/azure-ad-check-user-assigned-managed-identity-permissions.png" alt-text="Screenshot of user-assigned managed identity permissions.":::

<a id="manage-a-managed-identity-for-a-server-or-instance"></a>

## Get or set a managed identity for a logical server or managed instance

To create a server by using a UMI, see the following guide: [Create an Azure SQL logical server by using a user-assigned managed identity](authentication-azure-ad-user-assigned-managed-identity-create-server.md).

### Get the SMI for Azure SQL Database logical server

The Azure portal displays the system-assigned managed identity (SMI) ID in the **Properties** menu of the Azure SQL Database logical server.

:::image type="content" source="media/authentication-azure-ad-user-assigned-managed-identity/get-system-assigned-managed-identity-azure-sql-server-azure-portal.png" alt-text="Screenshot of the Azure portal page for an Azure SQL Database logical server. In the Properties menu, the System Assigned Managed Identity is highlighted.":::

- To retrieve the UMI(s) for Azure SQL Managed Instance or Azure SQL Database, use the following PowerShell or Azure CLI examples.
- To retrieve the SMI for Azure SQL Managed Instance, use the following PowerShell or Azure CLI examples.

### Set a managed identity in the Azure portal

To set the user-managed identity for the Azure SQL Database logical server or Azure SQL Managed Instance in the [Azure portal](https://portal.azure.com):

1. Go to your **SQL server** or **SQL managed instance** resource.
1. Under **Security**, select the **Identity** setting.
1. Under **User assigned managed identity**, select **Add**.
1. Select a subscription, and then for **Primary identity**, select a managed identity for the subscription. Then choose the **Select** button.

:::image type="content" source="media/authentication-azure-ad-user-assigned-managed-identity/existing-server-select-managed-identity.png" alt-text="Azure portal screenshot of selecting a user-assigned managed identity when configuring an existing server identity.":::

### Create or set a managed identity by using the Azure CLI

The Azure CLI 2.26.0 (or later) is required to run these commands with a UMI.

#### Azure SQL Database managed identity using the Azure CLI

- To provision a new server with a user-assigned managed identity, use the [az sql server create](/cli/azure/sql/server#az-sql-server-create) command.
- To obtain the managed identities for a logical server, use the [az sql server show](/cli/azure/sql/server#az-sql-server-show) command.
  - For example, to retrieve the user-assigned managed identities of a logical server, look for the `principalId` of each:

    ```azurecli
    az sql server show --resource-group "resourcegroupnamehere" --name "sql-logical-server-name-here" --query identity.userAssignedIdentities
    ```

  - To retrieve the system-assigned managed identity of an Azure SQL Database logical server:

    ```azurecli
    az sql server show --resource-group "resourcegroupnamehere" --name "sql-logical-server-name-here" --query identity.principalId
    ```

- To update the UMI's server setting, use the [az sql server update](/cli/azure/sql/server#az-sql-server-update) command.

#### Azure SQL Managed Instance managed identity using the Azure CLI

- To provision a new managed instance with a UMI, use the [az sql mi create](/cli/azure/sql/mi#az-sql-mi-create) command.
- To obtain the system-assigned and user-assigned managed identities for managed instances, use the [az sql mi show](/cli/azure/sql/mi#az-sql-mi-show) command.
  - For example, to retrieve the UMI(s) for a managed instance, look for the `principalId` of each:
  
    ```azurecli
    az sql mi show --resource-group "resourcegroupnamehere" --name "sql-mi-name-here" --query identity.userAssignedIdentities
    ```

  - To retrieve the SMI of a managed instance:
  
    ```azurecli
    az sql mi show --resource-group "resourcegroupnamehere" --name "sql-mi-name-here" --query identity.principalId
    ```

- To update the UMI's managed instance setting, use the [az sql mi update](/cli/azure/sql/mi#az-sql-mi-update) command.

### Create or set a managed identity by using PowerShell

[Az.Sql module 3.4](https://www.powershellgallery.com/packages/Az.Sql/3.4.0) or later is required for using PowerShell with a UMI. The [latest version of PowerShell](/powershell/scripting/install/installing-powershell) is recommended, or use the [Azure Cloud Shell in the Azure portal](/azure/cloud-shell/overview).

#### Azure SQL Database managed identity using PowerShell

- To provision a new server with a UMI, use the [New-AzSqlServer](/powershell/module/az.sql/new-azsqlserver) command.
- To obtain the managed identities for a logical server, use the [Get-AzSqlServer](/powershell/module/az.sql/get-azsqlserver) command.
  - For example, to retrieve the UMI(s) of a logical server, look for the `principalId` of each:

    ```powershell
    $MI = Get-AzSqlServer -ResourceGroupName "resourcegroupnamehere" -Name "sql-logical-server-name-here"
    $MI.Identity.UserAssignedIdentities | ConvertTo-Json 
    ```

  - To retrieve the SMI of an Azure SQL Database logical server:

    ```powershell
    $MI = Get-AzSqlServer -ResourceGroupName "resourcegroupnamehere" -Name "sql-logical-server-name-here"
    $MI.Identity.principalId
    ```

- To update the UMI's server setting, use the [Set-AzSqlServer](/powershell/module/az.sql/set-azsqlserver) command.

#### Azure SQL Managed Instance managed identity using PowerShell

- To provision a new managed instance with a UMI, use the [New-AzSqlInstance](/powershell/module/az.sql/new-azsqlinstance) command.
- To obtain the managed identities for a managed instance, use the [Get-AzSqlInstance](/powershell/module/az.sql/get-azsqlinstance) command.
  - For example, to retrieve the UMI(s) of a managed instance, look for the `principalId` of each:

    ```powershell
    $MI = Get-AzSqlInstance -ResourceGroupName "resourcegroupnamehere" -Name "sql-mi-name-here"
    $MI.Identity.UserAssignedIdentities | ConvertTo-Json 
    ```

  - To retrieve the SMI of a managed instance:

    ```powershell
    $MI = Get-AzSqlInstance -ResourceGroupName "resourcegroupnamehere" -Name "sql-mi-name-here"
    $MI.Identity.principalId
    ```

- To update the UMI's managed instance setting, use the [Set-AzSqlInstance](/powershell/module/az.sql/set-azsqlinstance) command.

### Create or set a managed identity by using the REST API

To update the UMI settings for the server, you can also use the REST API provisioning script used in [Create a logical server by using a user-assigned managed identity](authentication-azure-ad-user-assigned-managed-identity-create-server.md) or [Create a managed instance by using a user-assigned managed identity](../managed-instance/authentication-azure-ad-user-assigned-managed-identity-create-managed-instance.md). Rerun the provisioning command in the guide with the updated user-assigned managed identity property that you want to update.

### Create or set a managed identity by using an ARM template

To update the UMI settings for the server, you can also use the Azure Resource Manager template (ARM template) used in [Create a logical server by using a user-assigned managed identity](authentication-azure-ad-user-assigned-managed-identity-create-server.md) or [Create a managed instance by using a user-assigned managed identity](../managed-instance/authentication-azure-ad-user-assigned-managed-identity-create-managed-instance.md). Rerun the provisioning command in the guide with the updated user-assigned managed identity property that you want to update.

> [!NOTE]
> You can't change the server administrator or password, or change the Microsoft Entra admin, by rerunning the provisioning command for the ARM template.

## Limitations and known issues

- After you create a managed instance, the **Microsoft Entra admin** page for your managed instance in the Azure portal shows a warning: `Managed Instance needs permissions to access Microsoft Entra ID. Click here to grant "Read" permissions to your Managed Instance.` If you gave the UMI the appropriate permissions [discussed earlier in this article](#permissions), you can ignore this warning.
- If you use an SMI or a UMI as the server or instance identity, deleting the identity will make the server or instance unable to access Microsoft Graph. Microsoft Entra authentication and other functions will fail. To restore Microsoft Entra functionality, assign a new SMI or UMI to the server with appropriate permissions.
- To grant permissions to access Microsoft Graph through an SMI or a UMI, you need to use PowerShell. You can't grant these permissions by using the Azure portal.

## Related content

> [!div class="nextstepaction"]
> [Create an Azure SQL logical server by using a user-assigned managed identity](authentication-azure-ad-user-assigned-managed-identity-create-server.md)

> [!div class="nextstepaction"]
> [Create a managed instance by using a user-assigned managed identity](../managed-instance/authentication-azure-ad-user-assigned-managed-identity-create-managed-instance.md)
