---
title: Managed Identity
description: Learn how to use a managed identity with SQL Server 2025.
author: PratimDasgupta
ms.author: prdasgu
ms.reviewer: mikeray, randolphwest, mathoma, vanto
ms.date: 07/06/2025
ms.service: sql
ms.topic: how-to
ms.custom:
  - build-2025
# CustomerIntent: As a database engineer I need to understand how to implement managed identity with SQL Server 2025.
monikerRange: ">=sql-server-ver17"
---
# Managed identity (preview) for SQL Server enabled by Azure Arc

[!INCLUDE [sqlserver2025](../../includes/applies-to-version/sqlserver2025.md)]

This article describes how to configure a managed identity for SQL Server enabled by Azure Arc.

[!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] includes managed identity support for SQL Server on Windows. Use a managed identity to interact with resources in Azure by using Microsoft Entra authentication.

> [!NOTE]  
> Using a managed identity with SQL Server 2025 is currently in **preview**.

## Overview

[!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] introduces support for [Microsoft Entra managed identities](/entra/identity/managed-identities-azure-resources/overview). Use managed identities to authenticate to Azure services without needing to manage credentials. Managed identities are automatically managed by Azure and can be used to authenticate to any service that supports Microsoft Entra authentication. With [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)], you can use managed identities both to authenticate inbound connections, and also to authenticate outbound connections to Azure services.

When you connect your SQL Server instance to Azure Arc, a system-assigned managed identity is automatically created for the SQL Server hostname. After the managed identity is created, you must associate the identity with the SQL Server instance and the Microsoft Entra tenant ID by updating the registry.

When using managed identity with SQL Server enabled by Azure Arc, consider the following:

- The managed identity is assigned at the Azure Arc server level.
- Only system-assigned managed identities are supported.
- SQL Server uses this Azure Arc server level managed identity as the **primary managed identity**.
- SQL Server can use this primary managed identity in either `inbound` and/or `outbound` connections.
  - `Inbound connections` are logins and users connecting to SQL Server. Inbound connections can also be achieved by using [App registration](entra-authentication-setup-tutorial.md), starting in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)].
  - `Outbound connections` are SQL Server connections to Azure resources, like backup to URL, or connecting to Azure Key Vault.
- App Registration **can't** enable a SQL Server to make outbound connections. Outbound connections need a primary managed identity assigned to the SQL Server.

## Prerequisites

Before you can use a managed identity with SQL Server enabled by Azure Arc, ensure that you meet the following prerequisites:

- [Connect your SQL Server to Azure Arc](connect.md).
- The latest version of the [Azure Extension for SQL Server](release-notes.md).

## Enable the primary managed identity

If you've installed the Azure Extension for SQL Server to your server, you can enable the primary managed identity for your SQL Server instance directly from the Azure portal. It's also possible to enable the primary managed identity manually by updating the registry, but should be done with extreme caution.

### [Azure portal](#tab/portal)

To enable the primary managed identity in the Azure portal, follow these steps:

1. Go to your [SQL Server enabled by Azure Arc](https://portal.azure.com/#view/Microsoft_Azure_ArcCenterUX/ArcCenterMenuBlade/~/sqlServerInstances) resource in the Azure portal.
1. Under **Settings**, select **Microsoft Entra ID and Purview** to open the **Microsoft Entra ID and Purview** page.

   > [!NOTE]  
   > If you don't see the **Enable Microsoft Entra ID authentication** option, ensure that your SQL Server instance is connected to Azure Arc and that you have the latest SQL extension installed.

1. On the **Microsoft Entra ID and Purview** page, check the box next to **Use a primary managed identity** and then use **Save** to apply your configuration:

   :::image type="content" source="media/managed-identity/entra-portal.png" alt-text="Screenshot of the Microsoft Entra option in the Azure portal." lightbox="media/managed-identity/entra-portal.png":::

### [Manually](#tab/manual)

It's possible to manually enable the primary managed identity for your SQL Server instance by updating the registry, but should be done with extreme caution.

### Grant permission to the Tokens folder

Grant **Read & execute** operating system permissions on the folder `C:\ProgramData\AzureConnectedMachineAgent\Tokens\` to the SQL Server 2025 instance service account. By default, the service account is `NT Service\MSSQLSERVER`, or for named instances, `NT Service\MSSQL$<instancename>`.

:::image type="content" source="media/managed-identity/tokens-folder-permissions.png" alt-text="Screenshot of Tokens folder Security properties tab.":::

You might need to grant admin permissions for the SQL Server service account on the `AzureConnectedMachineAgent` folder before the `Tokens` folder:

:::image type="content" source="media/managed-identity/azure-connected-machine-agent-folder-permissions.png" alt-text="Screenshot of AzureConnectedMachineAgent folder Security properties tab.":::

### Add SQL Server service account to the Hybrid agent extension applications group

Add the SQL Server service account (default: `NT Service\MSSQLSERVER` or for named instances, `NT Service\MSSQL$instancename`) to the **Hybrid agent extension applications** group.

- Open Windows **Computer Management**.
- Go to **Local Users and Groups** and select **Groups**.
- Add the SQL Server service account to the **Hybrid agent extension applications** group.

:::image type="content" source="media/managed-identity/computer-management.png" alt-text="Screenshot of Computer Management showing the hybrid agent extension application group.":::

:::image type="content" source="media/managed-identity/hybrid-agent-extension-applications-group-properties.png" alt-text="Screenshot of the hybrid agent extension application group properties.":::

### Update the registry

> [!WARNING]  
> Incorrectly editing the registry can severely damage your system. Before making changes to the registry, we recommend you back up any valued data on the computer.

In the registry, update the **\HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\MSSQL17.MSSQLSERVER\MSSQLServer\FederatedAuthentication** subkey.

Create the following entries:

| Entry | Value |
| --- | --- |
| `ArcServerManagedIdentityClientId` | Empty (no value) |
| `HIMDSApiVersion` | `2020-06-01` |
| `HIMDSEndpoint` | `http://localhost:40342/metadata/identity/oauth2/token` |
| `ArcServerSystemAssignedManagedIdentityTenantId` | `Arc-AAD-Tenant-ID` |
| `ArcServerSystemAssignedManagedIdentityClientId` | `Arc-Machine-Client-Id` |
| `PrimaryAADTenant` | `Arc-AAD-Tenant-ID` |
| `AADChannelMaxBufferedMessageSize` | `200000` |
| `AADGraphEndPoint` | `graph.windows.net` |
| `AADGroupLookupMaxRetryAttempts` | `10` |
| `AADGroupLookupMaxRetryDuration` | `30000` |
| `AADGroupLookupRetryInitialBackoff` | `100` |
| `AADServerAdminSid` | `00000000-0000-0000-0000-000000000000` |
| `AuthenticationEndpoint` | `login.microsoftonline.com` |
| `CacheMaxSize` | `300` |
| `ClientCertBlackList` | Empty (no value) |
| `FederationMetadataEndpoint` | `login.windows.net` |
| `GraphAPIEndpoint` | `graph.windows.net` |
| `IssuerURL` | `https://sts.windows.net/` |
| `OnBehalfOfAuthority` | `https://login.windows.net/` |
| `STSURL` | `https://login.windows.net/` |
| `MsGraphEndPoint` | `graph.microsoft.com` |
| `SendX5c` | `false` |
| `ServicePrincipalName` | `https://database.windows.net/` |
| `ServicePrincipalNameForArcadia` | `https://sql.azuresynapse.net` |
| `ServicePrincipalNameForArcadiaDogfood` | `https://sql.azuresynapse-dogfood.net` |
| `ServicePrincipalNameNoSlash` | `https://database.windows.net` |
| `AADBecWSConnectionPoolMaxSize` | `500` |

### Back up and edit the registry

The following sections describe how to back up and edit the registry with Registry Editor.

#### Open the Registry Editor

1. Press **Windows key + R** to open the Run dialog box.
1. Type `regedit` and press **Enter**.
1. If prompted by User Account Control, select **Yes**.

#### Back up the registry key

This step backs up the registry before you make any changes. You can import this file back into the registry later if your changes cause a problem.

1. Select **File** from the menu.
1. Select **Export**.
1. In the Export Registry File dialog box, choose a location to save the backup.
1. Enter a name for the backup file in the **File name** field.
1. Ensure **All** is selected in the Export range.
1. Select **Save**.

#### Add entries

In this step, you add entries to the registry with Registry Editor.

1. Navigate this subkey: **\HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\MSSQL17.MSSQLSERVER\MSSQLServer\FederatedAuthentication**.

1. Right-click on **FederatedAuthentication** and select **String Value**.

   :::image type="content" source="media/managed-identity/federated-authentication-registry-key-substring.png" alt-text="Screenshot of Registry Editor.":::

1. Repeat for each entry listed at [Update the registry](#update-the-registry)

   This image demonstrates a correctly configured registry:

   :::image type="content" source="media/managed-identity/federated-authentication-registry-key.png" alt-text="Screenshot of the registry set with correct entries." lightbox="media/managed-identity/federated-authentication-registry-key.png":::

#### Restore the registry key (if needed)

If you need to restore to previous registry settings, follow these steps.

1. Open the Registry Editor as described previously.
1. Select **File** from the menu.
1. Select **Import**.
1. Navigate to the location of your saved backup file.
1. Select the backup file and select **Open**.

For details, review [How to add, modify, or delete registry subkeys and values by using a .reg file](https://support.microsoft.com/topic/how-to-add-modify-or-delete-registry-subkeys-and-values-by-using-a-reg-file-9c7f37cf-a5e9-e1cd-c4fa-2a26218a1a23).

---

## Grant application permissions to the identity

The system-assigned managed identity, which uses the Arc-enabled machine name, must have the following Microsoft Graph application permissions (app roles): `User.Read.All`, `GroupMember.Read.All`, and `Application.Read.All`.

You can use PowerShell to grant required permissions to the managed identity. Alternatively, you can [create a role-assignable group](/entra/identity/role-based-access-control/groups-create-eligible). After the group is created, assign the **Directory readers** role to the group, and add all system-assigned managed identities for your Arc-enabled machines to the group.

The following PowerShell script grants the required permissions to the managed identity. Make sure this script is run on PowerShell 7.5 or a later version, and has the `Microsoft.Graph` module 2.28 or later installed.

```powershell
# Set your Azure tenant and managed identity name
$tenantID = '<Enter-Your-Azure-Tenant-Id>'
$managedIdentityName = '<Enter-Your-Arc-HostMachine-Name>'

# Connect to Microsoft Graph
try {
    Connect-MgGraph -TenantId $tenantID -ErrorAction Stop
    Write-Output "Connected to Microsoft Graph successfully."
}
catch {
    Write-Error "Failed to connect to Microsoft Graph: $_"
    return
}

# Get Microsoft Graph service principal
$graphAppId = '00000003-0000-0000-c000-000000000000'
$graphSP = Get-MgServicePrincipal -Filter "appId eq '$graphAppId'"
if (-not $graphSP) {
    Write-Error "Microsoft Graph service principal not found."
    return
}

# Get the managed identity service principal
$managedIdentity = Get-MgServicePrincipal -Filter "displayName eq '$managedIdentityName'"
if (-not $managedIdentity) {
    Write-Error "Managed identity '$managedIdentityName' not found."
    return
}

# Define roles to assign
$requiredRoles = @(
    "User.Read.All",
    "GroupMember.Read.All",
    "Application.Read.All"
)

# Assign roles using scoped syntax
foreach ($roleValue in $requiredRoles) {
    $appRole = $graphSP.AppRoles | Where-Object {
        $_.Value -eq $roleValue -and $_.AllowedMemberTypes -contains "Application"
    }

    if ($appRole) {
        try {
            New-MgServicePrincipalAppRoleAssignment   -ServicePrincipalId $managedIdentity.Id `
                -PrincipalId $managedIdentity.Id `
                -ResourceId $graphSP.Id `
                -AppRoleId $appRole.Id `
                -ErrorAction Stop

            Write-Output "Successfully assigned role '$roleValue' to '$managedIdentityName'."
        }
        catch {
            Write-Warning "Failed to assign role '$roleValue': $_"
        }
    }
    else {
        Write-Warning "Role '$roleValue' not found in Microsoft Graph AppRoles."
    }
}
```

## Create logins and users

Follow the steps in the [Microsoft Entra tutorial](../../sql-server/azure-arc/entra-authentication-setup-tutorial.md#create-logins-and-users) to create logins and users for the managed identity.

## Limitations

Consider the following limitations when using a managed identity with SQL Server 2025:

- Microsoft Entra authentication is only supported with Arc enabled SQL Server 2025 running on Windows Server.
- Using Microsoft Entra authentication with failover cluster instances isn't supported.
- The identity you choose to authenticate to SQL Server has to have either the **Directory Readers** role in Microsoft Entra ID or the following three Microsoft Graph application permissions (app roles): `User.Read.All`, `GroupMember.Read.All`, and `Application.Read.All`.
- Once Microsoft Entra authentication is enabled, disabling isn't advisable. Disabling Microsoft Entra authentication forcefully by deleting registry entries can result in unpredictable behavior with SQL Server 2025.
- Authenticating to SQL Server on Arc machines through Microsoft Entra authentication using the [FIDO2 method](/azure/active-directory/authentication/howto-authentication-passwordless-faqs) isn't currently supported.
- [OPENROWSET BULK](../../t-sql/functions/openrowset-bulk-transact-sql.md) operations can also read the tokens folder `C:\ProgramData\AzureConnectedMachineAgent\Tokens\`. The `BULK` option requires either `ADMINISTER BULK OPERATIONS` or `ADMINISTER DATABASE BULK OPERATIONS` permissions. These permissions should be treated as equivalent to **[sysadmin](../../relational-databases/security/authentication-access/server-level-roles.md)**.

## Related content

- [Microsoft Entra authentication for SQL Server](../../relational-databases/security/authentication-access/azure-ad-authentication-sql-server-overview.md)
- [What are managed identities for Azure resources?](/entra/identity/managed-identities-azure-resources/overview)
- [Enable Microsoft Entra authentication for SQL Server on Azure VMs](/azure/azure-sql/virtual-machines/windows/configure-azure-ad-authentication-for-sql-vm)
