---
title: "Enable Microsoft Entra authentication"
description: This article teaches you to configure Microsoft Entra authentication for your SQL Server on Azure VM.
author: adbadram
ms.author: adbadram
ms.reviewer: mathoma
ms.date: 09/25/2023
ms.service: virtual-machines-sql
ms.subservice: security
ms.custom: has-azure-ad-ps-ref, devx-track-azurecli
ms.topic: how-to
---
# Enable Microsoft Entra authentication for SQL Server on Azure VMs
[!INCLUDE[appliesto-sqlvm](../../includes/appliesto-sqlvm.md)]

This article teaches you to enable authentication with Microsoft Entra ID ([formerly Azure Active Directory](/entra/fundamentals/new-name)) for your SQL Server on Azure virtual machines (VMs). 

## Overview

Starting with SQL Server 2022, you can connect to SQL Server on Azure VMs using one of the following Microsoft Entra authentication methods: 

[!INCLUDE [entra-authentication-options](../../includes/entra-authentication-options.md)]

When you create a Microsoft Entra login for SQL Server and when a user connects using that login, SQL Server uses a managed identity to query Microsoft Graph. When you enable Microsoft Entra authentication for your SQL Server on Azure VM, you need to provide a managed identity that SQL Server can use to communicate with Microsoft Entra ID. This managed identity needs to have permissions to query Microsoft Graph. 

When enabling a [managed identity](/azure/active-directory/managed-identities-azure-resources/overview#managed-identity-types) for a resource in Azure, the security boundary of the identity is the resource to which it's attached. For example, the security boundary for a virtual machine with managed identities for Azure resources enabled is the virtual machine. Any code running on that VM is able to call the managed identities endpoint and request tokens. When enabling a managed identity for SQL Server on Azure VMs, the identity is attached to the virtual machine, so the security boundary is the virtual machine. The experience is similar when working with other resources that support managed identities. For more information, read the [Managed Identities FAQ](/azure/active-directory/managed-identities-azure-resources/managed-identities-faq).

The system-assigned and user-assigned managed identities used for Microsoft Entra authentication with SQL Server on Azure VMs offer the following benefits: 

- **System-assigned managed identity** offers a simplified configuration process. Since the managed identity has the same lifetime as the virtual machine, there's no need to delete it separately when you delete the virtual machine. 
- **User-assigned managed identity** offers scalability since it can be attached to, and used for Microsoft Entra authentication, for multiple SQL Server on Azure VMs. 

To get started with managed identities, review [Configure managed identities using the Azure portal](/azure/active-directory/managed-identities-azure-resources/qs-configure-portal-windows-vm). 


## Prerequisites

To enable Microsoft Entra authentication on your SQL Server, you need the following prerequisites: 

- Use SQL Server 2022. 
- Register SQL Server VM with the [SQL Server Iaas Agent extension](sql-agent-extension-manually-register-single-vm.md). 
- Have an existing **system-assigned** or **user-assigned** managed identity in the same Microsoft Entra tenant as your SQL Server VM. [Configure managed identities using the Azure portal](/azure/active-directory/managed-identities-azure-resources/qs-configure-portal-windows-vm) to learn more. 
- [Azure CLI 2.48.0 or later](/cli/azure/install-azure-cli) if you intend to use the Azure CLI to configure Microsoft Entra authentication for your SQL Server VM. 

## Grant permissions

The managed identity you choose to facilitate authentication between SQL Server and Microsoft Entra ID has to have the following three Microsoft Graph application permissions (app roles): `User.Read.All`, `GroupMember.Read.All`, and `Application.Read.All`. 

Alternatively, adding the managed identity to the **Microsoft Entra Directory Readers role** grants sufficient permissions. Another way to assign the **Directory Readers** role to a managed identity is to assign the **Directory Readers role** to a group in Microsoft Entra ID. The group owners can then add the Virtual Machine managed identity as a member of this group. This minimizes involving Microsoft Entra Global administrators and delegates the responsibility to the group owners. 


### Add managed identity to the role

This section explains how to add your managed identity to the **Directory Readers** role in Microsoft Entra ID. You need to have Global Administrator privileges to make changes to the Directory Readers role assignments. If you don't have sufficient permission, work with your Microsoft Entra administrator to follow these steps.

To grant your managed identity the **Directory Readers** role, follow these steps: 

1. Open [Microsoft Entra ID Roles and administrators](https://portal.azure.com/#blade/Microsoft_AAD_IAM/RolesManagementMenuBlade) in the Azure portal:

1. Type _Directory readers_ in the search box, and then select the role **Directory readers** to open the **Directory Readers | Assignments** page: 

   :::image type="content" source="media/configure-azure-ad-authentication-for-sql-vm/search-for-directory-readers.png" alt-text="Screenshot of the Roles and administrators page of the Azure portal, searching for and selecting the Directory Readers role.":::

1. On the **Directory Readers | Assignments** page, select **+ Add assignments** to open the **Add assignment** page. 

   :::image type="content" source="media/configure-azure-ad-authentication-for-sql-vm/azure-ad-directory-readers.png" alt-text="Screenshot of the Directory Readers page of the Azure portal.":::

1. On the **Add assignments** page, choose **No member selected** under **Select members** to open the **Select a member** page. 

   :::image type="content" source="media/configure-azure-ad-authentication-for-sql-vm/azure-ad-add-assignment.png" alt-text="Screenshot of the add assignment page of the Azure portal, with No member selected highlighted.":::

1. On the **Select a member** page, search for the name of the managed identity you want to use with your SQL Server VM and add it to the **Directory Readers** role. For system-assigned managed identities, search for the name of the VM. Use **Select** to confirm the identity and go back to the **Add assignments** page. 

   :::image type="content" source="media/configure-azure-ad-authentication-for-sql-vm/azure-ad-select-member.png" alt-text="Screenshot searching for members to select in the Azure portal.":::

1. Verify that you see your chosen identity under **Select members** and then select **Next**.  

   :::image type="content" source="media/configure-azure-ad-authentication-for-sql-vm/azure-ad-verify-assignment.png" alt-text="Screenshot of the Add assignment page in the Azure portal, with VM2 added as an assignment.":::

1. Verify that your assignment type is set to **Active** and the box next to **Permanently assigned** is checked. Enter a business justification, such as _Adding Directory Reader role permissions to the system-assigned identity for VM2_ and then select **Assign** to save your settings and go back to the **Directory Readers | Assignments** page. 

   :::image type="content" source="media/configure-azure-ad-authentication-for-sql-vm/azure-ad-verify-assignment-settings.png" alt-text="Screenshot of settings on the Add assignment in the Azure portal.":::

1. On the **Directory Readers | Assignments** page, confirm you see your newly added identity under **Directory Readers**. 

   :::image type="content" source="media/configure-azure-ad-authentication-for-sql-vm/azure-ad-verify-directory-reader.png" alt-text="Screenshot of the Directory Readers page of the Azure portal showing your VM assignment added to the role.":::

### Add app role permissions

You can use [Azure PowerShell](/powershell/azure/install-azure-powershell) to grant app roles to a managed identity. To do so, follow these steps: 

[!INCLUDE [Azure AD PowerShell deprecation note](~/../azure-sql/reusable-content/msgraph-powershell/includes/aad-powershell-deprecation-note.md)]

1. Connect to Microsoft Graph

   ```powershell
   Connect-MgGraph -Scopes "AppRoleAssignment.ReadWrite.All" -TenantId "<tenant id>"
   ```

1. Retrieve the managed identity: 

   ```powershell
   $Graph_SP = Get-MgServicePrincipal -Filter "DisplayName eq 'Microsoft Graph'"
   $MSI = Get-MgServicePrincipal -Filter "displayName eq '<your managed identity display name>'"
   ```

1. Assign the `User.Read.All` role to the identity: 

   ```powershell
   $AAD_AppRole = $Graph_SP.AppRoles | Where-Object {$_.Value -eq "User.Read.All"}  
   New-MgServicePrincipalAppRoleAssignment -ServicePrincipalId $MSI.Id -BodyParameter @{principalId=$MSI.Id; resourceId=$Graph_SP.Id; appRoleId=$AAD_AppRole.Id}
   ```

1. Assign `GroupMember.Read.All` role to the identity: 

    ```powershell
    $AAD_AppRole = $Graph_SP.AppRoles | Where-Object {$_.Value -eq "GroupMember.Read.All"}  
    New-MgServicePrincipalAppRoleAssignment -ServicePrincipalId $MSI.Id -BodyParameter @{principalId=$MSI.Id; resourceId=$Graph_SP.Id; appRoleId=$AAD_AppRole.Id}
    ```

1. Assign `Application.Read.All` role to the identity: 

   ```powershell
   $AAD_AppRole = $Graph_SP.AppRoles | Where-Object {$_.Value -eq "Application.Read.All"}  
   New-MgServicePrincipalAppRoleAssignment -ServicePrincipalId $MSI.Id -BodyParameter @{principalId=$MSI.Id; resourceId=$Graph_SP.Id; appRoleId=$AAD_AppRole.Id}
   ```

You can validate permissions were assigned to the managed identity by doing the following:

1. Go to **Microsoft Entra ID** in the [Azure portal](https://portal.azure.com). 
1. Choose **Enterprise applications** and then select **All applications** under **Manage**. 
1. Filter the **Application type** by `Managed identities`. 
1. Select the managed identity and then choose **Permissions** under **Security**. You should see the following permissions: `User.Read.All`, `GroupMember.Read.All`, `Application.Read.All`. 

## Enable outbound communication

For Microsoft Entra authentication to work, you need the following:

- Outbound communication from SQL Server to Microsoft Entra ID and the Microsoft Graph endpoint. 
- Outbound communication from the SQL client to Microsoft Entra ID. 

Default Azure VM configurations allow outbound communication to the Microsoft Graph endpoint, as well as Microsoft Entra ID, but some users choose to restrict outbound communication either by using an OS level firewall, or the Azure VNet network security group (NSG). 

Firewalls on the SQL Server VM and any SQL client need to allow outbound traffic on ports 80 and 443. 

The Azure VNet NSG rule for the VNet that hosts your SQL Server VM should have the following: 

- A Service Tag of `AzureActiveDirectory`.
- **Destination port ranges** of: 80, 443.
- Action set to **Allow**. 
- A high priority (which is a low number). 

<a name='enable-azure-ad-authentication'></a>

## Enable Microsoft Entra authentication

You can enable Microsoft Entra authentication to your SQL Server VM by using the Azure portal, or the Azure CLI. 

>[!NOTE]
> After Microsoft Entra authentication is enabled, you can follow the same steps in this section to change the configuration to use a different managed identity. 

### [Portal](#tab/azure-portal)

To enable Microsoft Entra authentication to your SQL Server VM, follow these steps: 

1. Navigate to your [SQL virtual machines resource](manage-sql-vm-portal.md#security-configuration) in the Azure portal. 
1. Select **Security Configuration** under **Security**. 
1. Choose **Enable** under **Microsoft Entra authentication**. 
1. Choose the managed identity type from the drop-down, either **System-assigned** or **User-assigned**. If you choose user-assigned, then select the identity you want to use to authenticate to SQL Server on your Azure VM from the **User-assigned managed identity** drop-down that appears. 

   :::image type="content" source="media/configure-azure-ad-authentication-for-sql-vm/enable-azure-ad-in-portal.png" alt-text="Screenshot of the security configuration page for SQL VM in the Azure portal, with Microsoft Entra authentication selected.":::

After Microsoft Entra authentication has been enabled, you can follow the same steps to change which managed identity can authenticate to your SQL Server VM. 


> [!NOTE]
> The error `The selected managed identity does not have enough permissions for Microsoft Entra authentication` indicates that permissions haven't been properly assigned to the identity you've selected. Check the [Grant permissions](#grant-permissions) section to assign proper permissions. 

### [Azure CLI](#tab/azure-cli)

The following table lists the Azure CLI commands you can use to work with Microsoft Entra authentication for your SQL Server on Azure VMs. 


|Command  |Description  |
|---------|---------|
|[az sql vm enable-azure-ad-auth](/cli/azure/sql/vm#az-sql-vm-enable-azure-ad-auth)     | Enables Microsoft Entra authentication to your SQL Server VM.         |
|[az sql vm validate-azure-ad-auth](/cli/azure/sql/vm#az-sql-vm-validate-azure-ad-auth)  | - Run before you enable Microsoft Entra authentication to validate the configuration, such as to confirm the managed identity has the necessary permissions. <br /> - Run after you enable Microsoft Entra authentication to debug unexpected issues, such as the removal of a managed identity, or the removal of the necessary permissions for a managed identity.       |
|[az sql vm show --expand *](/cli/azure/sql/vm#az-sql-vm-show)     | Validate the status of Microsoft Entra authentication to your SQL Server on Azure VMs.     |

<a name='validate-azure-ad-environment-'></a>

#### Validate Microsoft Entra environment 

You can validate permissions have been correctly assigned to the specified managed identity by running the [az sql vm validate-azure-ad-auth](/cli/azure/sql/vm#az-sql-vm-validate-azure-ad-auth) command at the client.  

- Validate Microsoft Entra authentication with a system-assigned managed identity: 

   ```azurecli
   az sql vm validate-azure-ad-auth -n sqlvm -g myresourcegroup
   ```

- Validate Microsoft Entra authentication with a user-assigned managed identity: 

   ```azurecli
   az sql vm validate-azure-ad-auth -n sqlvm -g myresourcegroup 
   --msi-client-id 11111111-2222-3333-4444-555555555555
   ```

<a name='enable-azure-ad-authentication-'></a>

#### Enable Microsoft Entra authentication 

You can enable Microsoft Entra authentication to the specified machine by running the [az sql vm enable-azure-ad-auth](/cli/azure/sql/vm#az-sql-vm-enable-azure-ad-auth) command. 

Assuming your SQL Server VM name is `sqlvm` and your resource group is `myResourceGroup`, the following examples enable Microsoft Entra authentication: 

- Enable Microsoft Entra authentication with a system-assigned managed identity using client-side validation: 

   ```azurecli
   az sql vm enable-azure-ad-auth -n sqlvm -g myresourcegroup
   ```

- Enable Microsoft Entra authentication with a system assigned managed identity, but skip client side validation and rely on the server-side validation that always happens: 

   ```azurecli
   az sql vm enable-azure-ad-auth -n sqlvm -g myresourcegroup 
   --skip-client-validation
   ```

- Enable Microsoft Entra authentication with a user-assigned managed identity and client-side validation: 

   ```azurecli
   az sql vm enable-azure-ad-auth -n sqlvm -g myresourcegroup 
   --msi-client-id 11111111-2222-3333-4444-555555555555
   ```

- Enable Microsoft Entra authentication with a user-assigned managed identity but skip client-side validation and rely on the server-side validation that always happens:

   ```azurecli
   az sql vm enable-azure-ad-auth -n sqlvm -g myresourcegroup 
   --msi-client-id 11111111-2222-3333-4444-555555555555 --skip-client-validation 
   ````

<a name='check-status-of-azure-ad-authentication-'></a>

#### Check status of Microsoft Entra authentication 

You can check if Microsoft Entra authentication has been enabled by running the [az sql vm show --expand *](/cli/azure/sql/vm#az-sql-vm-show) command.

Microsoft Entra ID isn't enabled if **AzureAdAuthenticationSettings** from `az sql vm show --expand *` shows `NULL`. 

For example, when you run: 

```azurecli
az sql vm show -n sqlvm -g myresourcegroup --expand * 
```

The following output indicates Microsoft Entra authentication has been enabled with a user-assigned managed identity: 

```
    "azureAdAuthenticationSettings": { 
      "clientId": "11111111-2222-3333-4444-555555555555" },
```

---

## Limitations

Consider the following limitations: 

- Microsoft Entra authentication is only supported with SQL Server 2022 running on Windows VMs registered with the [SQL IaaS Agent extension](sql-server-iaas-agent-extension-automate-management.md) and deployed to the public cloud. Only supported scenarios of the SQL IaaS Agent extension are supported, such as a default instance, or a single named instance. Failover cluster instances are not supported. 
- The identity you choose to authenticate to SQL Server has to have either the **Directory Readers** role in Microsoft Entra ID or the following three Microsoft Graph application permissions (app roles): `User.Read.All`, `GroupMember.Read.All`, and `Application.Read.All`. 
- Once Microsoft Entra authentication is enabled, there's no way to disable it. 
- Currently, authenticating to SQL Server on Azure VMs through Microsoft Entra authentication using the [FIDO2 method](/azure/active-directory/authentication/howto-authentication-passwordless-faqs) isn't supported. 

## Next steps

Review the security best practices for [SQL Server](/sql/relational-databases/security/). 

For other articles related to running SQL Server in Azure VMs, see [SQL Server on Azure Virtual Machines overview](sql-server-on-azure-vm-iaas-what-is-overview.md). If you have questions about SQL Server virtual machines, see the [Frequently asked questions](frequently-asked-questions-faq.yml).

To learn more, see the other articles in this best practices series:

- [Quick checklist](performance-guidelines-best-practices-checklist.md)
- [VM size](performance-guidelines-best-practices-vm-size.md)
- [Storage](performance-guidelines-best-practices-storage.md)
- [HADR settings](hadr-cluster-best-practices.md)
- [Collect baseline](performance-guidelines-best-practices-collect-baseline.md)
