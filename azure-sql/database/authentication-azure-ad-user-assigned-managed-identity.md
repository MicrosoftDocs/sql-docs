---
title: Managed identity in Azure AD for Azure SQL
titleSuffix: Azure SQL Database & Azure SQL Managed Instance
description: Learn about system assigned and user assigned managed identities in Azure AD (Azure AD) for Azure SQL Database and SQL Managed Instance.
author: GithubMirek
ms.author: mireks
ms.reviewer: vanto, wiassaf
ms.date: 10/11/2022
ms.service: sql-db-mi
ms.subservice: security
ms.topic: conceptual
monikerRange: "= azuresql || = azuresql-db || = azuresql-mi"
---

# Managed identities in Azure AD for Azure SQL

[!INCLUDE[appliesto-sqldb-sqlmi](../includes/appliesto-sqldb-sqlmi.md)]

Azure Active Directory (Azure AD) supports two types of managed identities: system-assigned managed identity (SMI) and user-assigned managed identity (UMI). For more information, see [Managed identity types](/azure/active-directory/managed-identities-azure-resources/overview#managed-identity-types).

An SMI is automatically assigned to Azure SQL Managed Instance when it's created. When you're using Azure AD authentication with Azure SQL Database, you must assign an SMI when Azure service principals are used to create Azure AD users in SQL Database.

Previously, only an SMI could be assigned to the Azure SQL Managed Instance or SQL Database server identity. Now, a UMI can be assigned to SQL Managed Instance or SQL Database as the instance or server identity.

In addition to using a UMI and an SMI as the instance or server identity, you can use them to access the database by using the SQL connection string option `Authentication=Active Directory Managed Identity`. You need to map a SQL user to the managed identity in the target database. For more information, see [Using Azure Active Directory authentication with SqlClient](/sql/connect/ado-net/sql/azure-active-directory-authentication).

To retrieve the current UMI(s) or SMI for Azure SQL Managed instance or Azure SQL Database, see [Get or set a managed identity for a logical server or managed instance](#get-or-set-a-managed-identity-for-a-logical-server-or-managed-instance) later in this article.

## Benefits of using user-assigned managed identities

There are several benefits of using a UMI as a server identity:

- Users have the flexibility to create and maintain their own UMIs for a tenant. You can use UMIs as server identities for Azure SQL. A UMI is managed by the user, whereas an SMI is uniquely defined per server and assigned by the system.
- In the past, you needed the Azure AD [Directory Readers](authentication-aad-directory-readers-role.md) role when using an SMI as the server or instance identity. With the introduction of accessing Azure AD through [Microsoft Graph](/graph/api/resources/azure-ad-overview), users who are concerned with giving high-level permissions such as the Directory Readers role to the SMI or UMI can alternatively give lower-level permissions so that the server or instance identity can access Microsoft Graph. 

  For more information on providing Directory Readers permissions and its function, see [Directory Readers role in Azure Active Directory for Azure SQL](authentication-aad-directory-readers-role.md).
- Users can choose a specific UMI to be the server or instance identity for all databases or managed instances in the tenant. Or they can have multiple UMIs assigned to different servers or instances. 

  UMIs can be used in different servers to represent different features. For example, a UMI can serve transparent data encryption (TDE) in one server, and a UMI can serve Azure AD authentication in another server.
- You need a UMI to create a [logical server in Azure](logical-servers.md) configured with TDE with customer-managed keys (CMKs). For more information, see [Customer-managed transparent data encryption using user-assigned managed identity](transparent-data-encryption-byok-identity.md).
- UMIs are independent from logical servers or managed instances. When a logical server or instance is deleted, the SMI is also deleted. UMIs aren't deleted with the server.

> [!NOTE]
> You must enable the instance identity (SMI or UMI) to allow support for Azure AD authentication in SQL Managed Instance. For SQL Database, enabling the server identity is optional and required only if an Azure AD service principal (Azure AD application) oversees creating and managing Azure AD users, groups, or applications in the server. For more information, see [Azure Active Directory service principal with Azure SQL](authentication-aad-service-principal.md).

## <a id="creating-a-user-assigned-managed-identity"></a> Create a user-assigned managed identity

For information on how to create a UMI, see [Manage user-assigned managed identities](/azure/active-directory/managed-identities-azure-resources/how-manage-user-assigned-managed-identities).

## Permissions

After the UMI is created, some permissions are needed to allow the UMI to read from [Microsoft Graph](/graph/api/resources/azure-ad-overview) as the server identity. Grant the following permissions, or give the UMI the [Directory Readers](authentication-aad-directory-readers-role-tutorial.md) role. 

These permissions should be granted before you provision a logical server or managed instance. After you grant the permissions to the UMI, they're enabled for all servers or instances that are created with the UMI assigned as a server identity.

> [!IMPORTANT]
> Only a [Global Administrator](/azure/active-directory/roles/permissions-reference#global-administrator) or [Privileged Role Administrator](/azure/active-directory/roles/permissions-reference#privileged-role-administrator) can grant these permissions.

- [User.Read.All](/graph/permissions-reference#user-permissions): Allows access to Azure AD user information.
- [GroupMember.Read.All](/graph/permissions-reference#group-permissions): Allows access to Azure AD group information.
- [Application.Read.ALL](/graph/permissions-reference#application-resource-permissions): Allows access to Azure AD service principal (application) information.

### Grant permissions

The following sample PowerShell script grants the necessary permissions for a UMI or an SMI. This sample assigns permissions to the UMI `umiservertest`. 

To run the script, you must sign in as a user with a Global Administrator or Privileged Role Administrator role.

The script grants the `User.Read.All`, `GroupMember.Read.All`, and `Application.Read.ALL` permissions to a UMI or an SMI to access [Microsoft Graph](/graph/auth/auth-concepts#microsoft-graph-permissions).

```powershell
# Script to assign permissions to the UMI "umiservertest"

import-module AzureAD
$tenantId = '<tenantId>' # Your Azure AD tenant ID

Connect-AzureAD -TenantID $tenantId
# Log in as a user with a "Global Administrator" or "Privileged Role Administrator" role
# Script to assign permissions to an existing UMI 
# The following Microsoft Graph permissions are required: 
#   User.Read.All
#   GroupMember.Read.All
#   Application.Read.ALL

# Search for Microsoft Graph
$AAD_SP = Get-AzureADServicePrincipal -SearchString "Microsoft Graph";
$AAD_SP
# Use Microsoft Graph; in this example, this is the first element $AAD_SP[0]

#Output

#ObjectId                             AppId                                DisplayName
#--------                             -----                                -----------
#47d73278-e43c-4cc2-a606-c500b66883ef 00000003-0000-0000-c000-000000000000 Microsoft Graph
#44e2d3f6-97c3-4bc7-9ccd-e26746638b6d 0bf30f3b-4a52-48df-9a82-234910c4a086 Microsoft Graph #Change 

$MSIName = "<managedIdentity>";  # Name of your user-assigned or system-assigned managed identity
$MSI = Get-AzureADServicePrincipal -SearchString $MSIName 
if($MSI.Count -gt 1)
{ 
Write-Output "More than 1 principal found, please find your principal and copy the right object ID. Now use the syntax $MSI = Get-AzureADServicePrincipal -ObjectId <your_object_id>"

# Choose the right UMI or SMI

Exit
} 

# If you have more UMIs with similar names, you have to use the proper $MSI[ ]array number

# Assign the app roles

$AAD_AppRole = $AAD_SP.AppRoles | Where-Object {$_.Value -eq "User.Read.All"}
New-AzureADServiceAppRoleAssignment -ObjectId $MSI.ObjectId  -PrincipalId $MSI.ObjectId  -ResourceId $AAD_SP.ObjectId[0]  -Id $AAD_AppRole.Id 
$AAD_AppRole = $AAD_SP.AppRoles | Where-Object {$_.Value -eq "GroupMember.Read.All"}
New-AzureADServiceAppRoleAssignment -ObjectId $MSI.ObjectId  -PrincipalId $MSI.ObjectId  -ResourceId $AAD_SP.ObjectId[0]  -Id $AAD_AppRole.Id
$AAD_AppRole = $AAD_SP.AppRoles | Where-Object {$_.Value -eq "Application.Read.All"}
New-AzureADServiceAppRoleAssignment -ObjectId $MSI.ObjectId  -PrincipalId $MSI.ObjectId  -ResourceId $AAD_SP.ObjectId[0]  -Id $AAD_AppRole.Id
```

In the final steps of the script, if you have more UMIs with similar names, you have to use the proper `$MSI[ ]array` number. An example is `$AAD_SP.ObjectId[0]`.

### Check permissions for user-assigned managed identity

To check permissions for a UMI, go to the [Azure portal](https://portal.azure.com). In the **Azure Active Directory** resource, go to **Enterprise applications**. Select **All Applications** for **Application type**, and search for the UMI that was created.

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
1. Select a subscription, and then for **Primary identity**, select a UMI for the subscription. Then choose the **Select** button. 

:::image type="content" source="media/authentication-azure-ad-user-assigned-managed-identity/existing-server-select-managed-identity.png" alt-text="Azure portal screenshot of selecting a user-assigned managed identity when configuring an existing server identity.":::

### Create or set a managed identity by using the Azure CLI

The Azure CLI 2.26.0 (or later) is required to run these commands with a UMI.

#### Azure SQL Database

- To provision a new server with a UMI, use the [az sql server create](/cli/azure/sql/server#az-sql-server-create) command.
- To obtain the managed identities for a logical server, use the [az sql server show](/cli/azure/sql/server#az-sql-server-show) command. 
    - For example, to retrieve the UMI(s) of a logical server, look for the `principalId` of each:
    ```azurecli
    az sql server show --resource-group "resourcegroupnamehere" --name "sql-logical-server-name-here" --query identity.userAssignedIdentities
    ```
    - To retrieve the SMI of an Azure SQL Database logical server:
    ```azurecli
    az sql server show --resource-group "resourcegroupnamehere" --name "sql-logical-server-name-here" --query identity.principalId
    ``` 
- To update the UMI's server setting, use the [az sql server update](/cli/azure/sql/server#az-sql-server-update) command.

#### Azure SQL Managed Instance

- To provision a new managed instance with a UMI, use the [az sql mi create](/cli/azure/sql/mi#az-sql-mi-create) command.
- To obtain the system-assigned and user-assigned MI's for managed instances, use the [az sql mi show](/cli/azure/sql/mi#az-sql-mi-show) command.
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

#### Azure SQL Database

- To provision a new server with a UMI, use the [New-AzSqlServer](/powershell/module/az.sql/new-azsqlserver) command.
- To obtain the managed identities for a logical server, use the [Get-AzSqlServer](/powershell/module/az.sql/get-azsqlserver) command.
    - For example, to retrieve the UMI(s) of a logical server, look for the `principalId` of each:
    ```powershell
    $MI = get-azsqlserver -resourcegroupname "resourcegroupnamehere" -name "sql-logical-server-name-here"
    $MI.Identity.UserAssignedIdentities | ConvertTo-Json 
    ``` 
    - To retrieve the SMI of an Azure SQL Database logical server:
    ```powershell
    $MI = get-azsqlserver -resourcegroupname "resourcegroupnamehere" -name "sql-logical-server-name-here"
    $MI.Identity.principalId
    ```
- To update the UMI's server setting, use the [Set-AzSqlServer](/powershell/module/az.sql/set-azsqlserver) command.

#### Azure SQL Managed Instance

- To provision a new managed instance with a UMI, use the [New-AzSqlInstance](/powershell/module/az.sql/new-azsqlinstance) command.
- To obtain the managed identities for a managed instance, use the [Get-AzSqlInstance](/powershell/module/az.sql/get-azsqlinstance) command.
    - For example, to retrieve the UMI(s) of a managed instance, look for the `principalId` of each:
    ```powershell
    $MI = get-azsqlinstance -resourcegroupname "resourcegroupnamehere" -name "sql-mi-name-here"
    $MI.Identity.UserAssignedIdentities | ConvertTo-Json 
    ``` 
    - To retrieve the SMI of a managed instance:
    ```powershell
    $MI = get-azsqlinstance -resourcegroupname "resourcegroupnamehere" -name "sql-mi-name-here"
    $MI.Identity.principalId
    ```
- To update the UMI's managed instance setting, use the [Set-AzSqlInstance](/powershell/module/az.sql/set-azsqlinstance) command.

### Create or set a managed identity by using the REST API

To update the UMI settings for the server, you can also use the REST API provisioning script used in [Create a logical server by using a user-assigned managed identity](authentication-azure-ad-user-assigned-managed-identity-create-server.md) or [Create a managed instance by using a user-assigned managed identity](../managed-instance/authentication-azure-ad-user-assigned-managed-identity-create-managed-instance.md). Rerun the provisioning command in the guide with the updated user-assigned managed identity property that you want to update.

### Create or set a managed identity by using an ARM template
To update the UMI settings for the server, you can also use the Azure Resource Manager template (ARM template) used in [Create a logical server by using a user-assigned managed identity](authentication-azure-ad-user-assigned-managed-identity-create-server.md) or [Create a managed instance by using a user-assigned managed identity](../managed-instance/authentication-azure-ad-user-assigned-managed-identity-create-managed-instance.md). Rerun the provisioning command in the guide with the updated user-assigned managed identity property that you want to update.

> [!NOTE]
> You can't change the server administrator or password, or change the Azure AD admin, by rerunning the provisioning command for the ARM template.

## Limitations and known issues

- After you create a managed instance, the **Azure Active Directory** pane in the Azure portal shows a warning: `Managed Instance needs permissions to access Azure Active Directory. Click here to grant "Read" permissions to your Managed Instance.` If you gave the UMI the appropriate permissions [discussed earlier in this article](#permissions), you can ignore this warning.
- If you use an SMI or a UMI as the server or instance identity, deleting the identity will make the server or instance unable to access Microsoft Graph. Azure AD authentication and other functions will fail. To restore Azure AD functionality, assign a new SMI or UMI to the server with appropriate permissions.
- To grant permissions to access Microsoft Graph through an SMI or a UMI, you need to use PowerShell. You can't grant these permissions by using the Azure portal.

## Next steps

> [!div class="nextstepaction"]
> [Create an Azure SQL logical server by using a user-assigned managed identity](authentication-azure-ad-user-assigned-managed-identity-create-server.md)

> [!div class="nextstepaction"]
> [Create a managed instance by using a user-assigned managed identity](../managed-instance/authentication-azure-ad-user-assigned-managed-identity-create-managed-instance.md)
