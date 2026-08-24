---
title: "Set up Transparent Data Encryption with Azure Key Vault for SQL Server"
description: Install and configure the SQL Server Connector for Azure Key Vault and configure Transparent Data Encryption
author: Pietervanhove
ms.author: pivanho
ms.reviewer: randolphwest, vanto
ms.date: 06/16/2026
ms.service: sql
ms.subservice: security
ms.topic: how-to
ai-usage: ai-assisted
ms.custom:
  - sfi-image-nochange
helpviewer_keywords:
  - "Extensible Key Management"
  - "EKM, with key vault setup"
  - "SQL Server Connector, setup"
  - "SQL Server Connector"
  - "TDE, AKV, EKM"
---
# Set up Transparent Data Encryption with Azure Key Vault for SQL Server

[!INCLUDE [sqlserver](../../../includes/applies-to-version/sqlserver.md)]

In this article, you install and configure the [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] Connector for Azure Key Vault, and then configure Transparent Data Encryption (TDE) by using a key in Azure Key Vault.

## Prerequisites

Before you begin using Azure Key Vault with your SQL Server instance, make sure that you meet the following prerequisites:

- You must have an Azure subscription.

- Install [Azure PowerShell version 5.2.0 or later](/powershell/azure/).

- Create a Microsoft Entra tenant.

- Review the principles of Extensible Key Management (EKM) storage with Azure Key Vault. See [Extensible Key Management Using Azure Key Vault (SQL Server)](extensible-key-management-using-azure-key-vault-sql-server.md).

- You can modify the registry on the [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] computer.

- Install the version of Visual Studio C++ Redistributable that's based on the version of SQL Server that you're running:

  SQL Server version | Visual Studio C++ Redistributable version
  ---------|---------
  2008, 2008 R2, 2012, 2014 | [Visual C++ Redistributable packages for Visual Studio 2013](https://www.microsoft.com/download/details.aspx?id=40784)
  2016, 2017, 2019, 2022, 2025 | [Visual C++ Redistributable for Visual Studio 2015](https://www.microsoft.com/download/details.aspx?id=48145)

- Read [Access Azure Key Vault behind a firewall](/azure/key-vault/general/access-behind-firewall) if you plan to use the SQL Server Connector for Azure Key Vault behind a firewall or with a proxy server.

> [!NOTE]  
> In [!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)] CU 12 and later versions, [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] on Linux supports TDE Extensible Key Management with Azure Key Vault. Steps 3 and 4 in this guide aren't required for [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] on Linux.

## Quick flow

1. Select an authentication model in [Step 1: Set up the authentication model](#step-1-set-up-the-authentication-model).
1. Create a key vault and key in [Step 2: Create a key vault](#step-2-create-a-key-vault).
1. Install the connector in [Step 3: Install the SQL Server Connector](#step-3-install-the-sql-server-connector).
1. Configure the registry prerequisite in [Step 4: Add registry key to support EKM provider](#step-4-add-registry-key-to-support-ekm-provider).
1. Configure SQL Server and validate encryption in [Step 5: Configure SQL Server](#step-5-configure-sql-server).

## Step 1: Set up the authentication model

> [!IMPORTANT]
> Choose your authentication model before you continue:
>
> - Use the **Service principal** tab for SQL Server on-premises.
> - Use the **Managed identity** tab for SQL Server on Azure VMs or SQL Server enabled by Azure Arc, where managed identity is supported.

Authentication model support matrix:

| Authentication model | SQL Server version | Where SQL Server runs | Supported |
|---|---|---|---|
| Service principal | Supported versions covered by this article | On-premises, Azure VM, SQL Server enabled by Azure Arc | Yes |
| Managed identity | SQL Server 2022 CU17 and later | Azure VM | Yes |
| Managed identity | SQL Server 2025 and later | SQL Server enabled by Azure Arc | Yes |
| Managed identity | Any | On-premises | No |

# [Service principal](#tab/ServicePrincipal)
To grant your SQL Server instance access permissions to your Azure key vault, you need a service principal account in Microsoft Entra ID.

1. Sign in to the [Azure portal](https://ms.portal.azure.com/), and do either of the following steps:

   - Select the **Microsoft Entra ID** button.

     :::image type="content" source="media/setup-steps-for-extensible-key-management-using-the-azure-key-vault/ekm-part-1-login-portal.png" alt-text="Screenshot of the Azure services pane.":::

   - Select **More services** and then, in the **All services** pane, type **Microsoft Entra ID**.

1. Register an application with Microsoft Entra ID by doing the following steps. For detailed step-by-step instructions, see the **Get an identity for the application** section of the Azure Key Vault blog post, [Azure Key Vault – Step by Step](/archive/blogs/kv/azure-key-vault-step-by-step#get-an-identity-for-the-application).

   1. On the **Manage** section of your **Microsoft Entra ID** resource, select **App registrations**.

      :::image type="content" source="media/setup-steps-for-extensible-key-management-using-the-azure-key-vault/ekm-part-1-azure-active-directory-app-register.png" alt-text="Screenshot of the Microsoft Entra ID Overview page in the Azure portal.":::

   1. On the **App registrations** page, select **New registration**.

      :::image type="content" source="media/setup-steps-for-extensible-key-management-using-the-azure-key-vault/ekm-part-1-azure-active-directory-new-registration.png" alt-text="Screenshot of the App registrations pane in the Azure portal.":::

   1. On the **Register an application** pane, enter the user-facing name for the app, and then select **Register**.

      :::image type="content" source="media/setup-steps-for-extensible-key-management-using-the-azure-key-vault/ekm-part-1-azure-active-directory-register.png" alt-text="Screenshot of the Register an application pane." lightbox="media/setup-steps-for-extensible-key-management-using-the-azure-key-vault/ekm-part-1-azure-active-directory-register.png":::

   1. In the left pane, select **Certificates & secrets** > **Client secrets** > **New client secret**.

      :::image type="content" source="media/setup-steps-for-extensible-key-management-using-the-azure-key-vault/ekm-part-1-azure-active-directory-certificates-secrets.png" alt-text="Screenshot of the Certificates & secrets pane for the App in the Azure portal." lightbox="media/setup-steps-for-extensible-key-management-using-the-azure-key-vault/ekm-part-1-azure-active-directory-certificates-secrets.png":::

   1. Under **Add a client secret**, enter a description and an appropriate expiration, and then select **Add**. You can't choose an expiration period greater than 24 months. For more information, see [Add a client secret](/azure/active-directory/develop/quickstart-register-app#add-a-client-secret).

      :::image type="content" source="media/setup-steps-for-extensible-key-management-using-the-azure-key-vault/ekm-part-1-azure-active-directory-add-secret.png" alt-text="Screenshot of the Add a client secret section for the App in the Azure portal.":::

   1. On the **Certificates & secrets** pane, under **Value**, select the **Copy** button next to the value of the client secret to use it to create an asymmetric key in SQL Server.

      :::image type="content" source="media/setup-steps-for-extensible-key-management-using-the-azure-key-vault/ekm-part-1-azure-active-directory-new-secret.png" alt-text="Screenshot of the secret value in the Azure portal.":::

   1. In the left pane, select **Overview** and then, in the **Application (client) ID** box, copy the value to use it to create an asymmetric key in SQL Server.

      :::image type="content" source="media/setup-steps-for-extensible-key-management-using-the-azure-key-vault/ekm-part-1-azure-active-directory-application-id.png" alt-text="Screenshot of the Application (client) ID value on the Overview pane.":::

# [Managed identity](#tab/ManagedIdentity)

Starting with SQL Server 2022 Cumulative Update 17 (CU17) or SQL Server 2025 enabled by Azure Arc, managed identities are supported for Extensible Key Management with Azure Key Vault. Managed identities are the recommended authentication method to allow different Azure services to authenticate the SQL Server without using passwords or secrets. For more information on managed identities, see [Managed identity types](/entra/identity/managed-identities-azure-resources/overview#managed-identity-types).

> [!NOTE]  
> Managed identities are supported only for SQL Server on Azure VMs or SQL Server 2025 enabled by Azure Arc. SQL Server on-premises doesn't support managed identities.
>
> To set up EKM with AKV for SQL Server on-premises, use the **service principal** tab.

To grant your SQL Server instance access permissions to your Azure key vault, meet the following prerequisites.

- SQL Server 2022 CU17 or later on an Azure VM [registered with the SQL IaaS Agent extension](/azure/azure-sql/virtual-machines/windows/sql-agent-extension-manually-register-single-vm), or SQL Server 2025 [enabled by Azure Arc](../../../sql-server/azure-arc/overview.md).
- You must configure the SQL Server instance with Microsoft Entra authentication. For SQL Server on Azure VMs, see [Configure Microsoft Entra authentication](/azure/azure-sql/virtual-machines/windows/configure-azure-ad-authentication-for-sql-vm) and [Configure managed identities on Azure virtual machines (VMs)](/entra/identity/managed-identities-azure-resources/how-to-configure-managed-identities). For SQL Server 2025 enabled by Azure Arc, see [Set up managed identity and Microsoft Entra authentication for SQL Server enabled by Azure Arc](../../../sql-server/azure-arc/microsoft-entra-authentication-with-managed-identity.md).
- The primary managed identity for SQL Server must have the `Key Vault Crypto Service Encryption User` role on the key vault when you use [Azure role-based access control](#azure-role-based-access-control), or the *Unwrap Key* and *Wrap Key* permissions when you use a vault access policy.

---

## Step 2: Create a key vault

Select the method you want to use to create a key vault.

> [!NOTE]  
> Only Azure Key Vault and Azure Key Vault Managed HSM are supported. Azure Cloud HSM isn't supported.

## [Azure portal](#tab/portal)

### Create a key vault by using the Azure portal

To create a key vault by using the Azure portal, see [Quickstart: Create a key vault using the Azure portal](/azure/key-vault/general/quick-create-portal).

#### Azure role-based access control

Use [Azure role-based access control (RBAC)](/azure/role-based-access-control/overview) to manage access to the Azure Key Vault. Don't use legacy access policies. Legacy access policies have known security vulnerabilities, lack support for Privileged Identity Management (PIM), and shouldn't be used for critical data and workloads. For more information on Azure Key Vault RBAC permissions, see [Azure built-in roles for Key Vault data plane operations](/azure/key-vault/general/rbac-guide#azure-built-in-roles-for-key-vault-data-plane-operations).

1. Go to the key vault resource that you created, and select the **Access control (IAM)** setting.

1. Select **Add** > **Add role assignment**.

   :::image type="content" source="media/setup-steps-for-extensible-key-management-using-the-azure-key-vault/ekm-part-2-add-role-assignment.png" alt-text="Screenshot of the Add role assignment button on the Access control (IAM) pane in the Azure portal." lightbox="media/setup-steps-for-extensible-key-management-using-the-azure-key-vault/ekm-part-2-add-role-assignment.png":::

1. The EKM application or managed identity needs the **Key Vault Crypto Service Encryption User** role to perform wrap and unwrap operations. Search for **Key Vault Crypto Service Encryption User** and select the role. Select **Next**.

   :::image type="content" source="media/setup-steps-for-extensible-key-management-using-the-azure-key-vault/ekm-part-2-select-role-assignment.png" alt-text="Screenshot of selecting a role assignment in the Azure portal." lightbox="media/setup-steps-for-extensible-key-management-using-the-azure-key-vault/ekm-part-2-select-role-assignment.png":::

1. In the **Members** tab, select the **Select members** option, and then search for the Microsoft Entra application or managed identity that you created in Step 1. Select the application or managed identity and then the **Select** button.

   :::image type="content" source="media/setup-steps-for-extensible-key-management-using-the-azure-key-vault/ekm-part-2-add-app-to-role.png" alt-text="Screenshot of the Select members pane for adding a role assignment in the Azure portal." lightbox="media/setup-steps-for-extensible-key-management-using-the-azure-key-vault/ekm-part-2-add-app-to-role.png":::

1. Select **Review + assign** twice to complete the role assignment.

#### Create a key
The user creating the key needs the **Key Vault Administrator** role.
Just like the previous steps, add the member creating the key and assign the role.

1. On the **Key Vault** pane, select **Keys** and then select the option **Generate/Import**. This action opens the **Create a key** pane. Select the **Generate** option, and enter a name for the key. The [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] Connector requires the key name to only use the characters "a-z", "A-Z", "0-9", and "-", with a 26-character limit.

1. Use key type **RSA** and **RSA key size** as **2048**. EKM currently only supports an RSA key. Set activation and expiration dates as appropriate and set **Enabled** as **Yes**.

   :::image type="content" source="media/setup-steps-for-extensible-key-management-using-the-azure-key-vault/ekm-part-2-add-key-vault-key.png" alt-text="Screenshot of the Create Key pane.":::

## [PowerShell](#tab/powershell)

### Create a key vault and key by using PowerShell

To create a key vault, see [Quickstart: Create a key vault using PowerShell](/azure/key-vault/general/quick-create-powershell).

#### Azure role-based access control

Use [Azure role-based access control (RBAC)](/azure/role-based-access-control/overview) to manage access to the Azure Key Vault. Don't use legacy access policies. Legacy access policies have known security vulnerabilities, lack support for Privileged Identity Management (PIM), and shouldn't be used for critical data and workloads. For more information on Azure Key Vault RBAC permissions, see [Azure built-in roles for Key Vault data plane operations](/azure/key-vault/general/rbac-guide#azure-built-in-roles-for-key-vault-data-plane-operations).

The EKM application or managed identity needs the **Key Vault Crypto Service Encryption User** role to perform wrap and unwrap operations. Use the [`New-AzRoleAssignment`](/powershell/module/az.resources/new-azroleassignment) cmdlet to assign the RBAC role.

   ```powershell
   New-AzRoleAssignment `
  -ObjectId <managed-identity-object-id> `
  -RoleDefinitionName "Key Vault Crypto Service Encryption User" `
  -Scope "/subscriptions/<sub-id>/resourceGroups/<rg>/providers/Microsoft.KeyVault/vaults/<kv-name>"
   ```

#### Create a key

Generate an asymmetric key in the key vault. You can do so in either of two ways: import an existing key or create a new key.

   > [!NOTE]  
   > SQL Server supports only 2048-bit and 3072-bit RSA keys, and 2048-bit and 3072-bit RSA-HSM keys.

##### Import an existing key

If you have an existing 2048-bit RSA software-protected key, you can upload the key to your Azure Key Vault. For example, if you have a PFX file saved to your `C:\` drive in a file named `softkey.pfx` that you want to upload to the Azure Key Vault, run the following command to set the variable `securepfxpwd` for a password of `12987553` for the PFX file:

```powershell
$securepfxpwd = ConvertTo-SecureString -String '12987553' `
  -AsPlainText -Force
```

Then run the following command to import the key from the PFX file. The Key Vault service protects the key by hardware (recommended):

```powershell
Add-AzKeyVaultKey -VaultName 'DocsSampleEKMKeyVault' `
  -Name 'DocsFirstKey' -KeyFilePath 'C:\softkey.pfx' `
  -KeyFilePassword $securepfxpwd -Destination 'HSM'
```

> [!CAUTION]  
> Import the asymmetric key for production scenarios, because doing so allows the administrator to escrow the key in a key escrow system. If you create the asymmetric key in the vault, you can't escrow it, because the private key can never leave the vault. Escrow any keys that protect critical data. The loss of an asymmetric key results in permanent data loss.

##### Create a new key

Alternatively, you can create a new encryption key directly in your Azure Key Vault and make it either software-protected or HSM-protected. In this example, create a software-protected key by using the `Add-AzKeyVaultKey` cmdlet:

```powershell
Add-AzKeyVaultKey -VaultName 'DocsSampleEKMKeyVault' `
  -Name 'ContosoRSAKey0' -Destination 'Software'
```

> [!IMPORTANT]  
> The key vault supports multiple versions of the same named key. The SQL Server Connector doesn't automatically switch the TDE protector when Azure Key Vault creates a new key version. To rotate the key used for [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] encryption, create a new [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] asymmetric key that references the new key version or a new key name, and then re-encrypt the DEK. For steps, see [Rotate asymmetric keys for TDE by using Azure Key Vault on SQL Server](rotate-transparent-data-encryption-asymmetric-keys-azure-key-vault-sql-server.md).

---

## Configure an Azure Key Vault Managed HSM (optional)

[Azure Key Vault Managed HSM (Hardware Security Module)](/azure/key-vault/managed-hsm/overview) supports SQL Server and SQL Server on Azure Virtual Machines (VMs) when you use the latest version of the [SQL Server Connector](use-sql-server-connector-with-sql-encryption-features.md), and Azure SQL. Managed HSM is a fully managed, highly available, single-tenant HSM service. Managed HSM provides a secure foundation for cryptographic operations and key storage. Managed HSM is designed to meet the most stringent security and compliance requirements.

[Step 2](#step-2-create-a-key-vault) shows how to create a key vault and key in Azure Key Vault. You can optionally use an Azure Key Vault Managed HSM to store or create a key for the SQL Server Connector. Follow these steps:

1. Create an Azure Key Vault Managed HSM by using the [Azure portal](/azure/key-vault/managed-hsm/overview), [the Azure CLI](/azure/key-vault/managed-hsm/quick-create-cli), [PowerShell](/azure/key-vault/managed-hsm/quick-create-powershell), or an [ARM template](/azure/key-vault/managed-hsm/quick-create-template).

1. Activate the Managed HSM. Only the designated administrators assigned during creation can activate it. In the Azure portal, select the Managed HSM resource, and then select **Download Security Domain** in the **Overview** menu. Follow one of the [quickstarts to activate your Managed HSM](/azure/key-vault/managed-hsm/quick-create-cli#activate-your-managed-hsm).

1. Grant permissions for the Microsoft Entra service principal or managed identity to access the Managed HSM. The **Managed HSM Administrator** role doesn't give permissions to create a key. Similar to [step 2](#step-2-create-a-key-vault), the EKM application or managed identity needs the **Managed HSM Crypto User** or **Managed HSM Crypto Service Encryption User** role to perform wrap and unwrap operations. For more information, see [Local RBAC built-in roles for Managed HSM](/azure/key-vault/managed-hsm/built-in-roles).

1. In the Azure Key Vault Managed HSM service menu, under **Setting**, select **Keys**. In the **Keys** window, select **Generate/Import/Restore Backup** to create a key or import an existing key.

   > [!NOTE]  
   > Algorithms RSA-HSM_2048 and RSA-HSM_3072 are supported starting in SQL Server 2022 (16.x) Cumulative Update 13.
   >
   > Azure Key Vault Managed HSM supports automatic key rotation. For more information, see [Configure key auto-rotation in Azure Managed HSM](/azure/key-vault/managed-hsm/key-rotation).
   >
   > Managed HSM supports private endpoint connections. For more information, see [Integrate Managed HSM with Azure Private Link](/azure/key-vault/managed-hsm/private-link). In this configuration, you must enable the **Microsoft trusted service bypass** option in the Azure Key Vault Managed HSM **Networking** setting.

## Step 3: Install the SQL Server Connector

Have a [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] administrator download the latest version of the SQL Server Connector for Microsoft Azure Key Vault from the [Microsoft Download Center](https://go.microsoft.com/fwlink/p/?LinkId=521700) and run the installer.

:::image type="content" source="media/setup-steps-for-extensible-key-management-using-the-azure-key-vault/ekm-connector-install.png" alt-text="Screenshot of the SQL Server Connector installation wizard.":::

By default, the Connector is installed at `C:\Program Files\SQL Server Connector for Microsoft Azure Key Vault`. You can change this location during setup. If you change it, adjust the scripts in the next section.

A successful installation places `Microsoft.AzureKeyVaultService.EKM.dll` on the machine. This assembly is the cryptographic EKM provider DLL. Register it with [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] by using the `CREATE CRYPTOGRAPHIC PROVIDER` statement.

The installer also offers sample scripts for SQL Server encryption.

For error code explanations, configuration settings, or maintenance tasks, see:

- [Maintenance instructions for the SQL Server Connector](sql-server-connector-maintenance-troubleshooting.md#AppendixA)
- [Error code explanations for the SQL Server Connector](sql-server-connector-maintenance-troubleshooting.md#AppendixC)

## Step 4: Add registry key to support EKM provider

> [!WARNING]  
> Only a [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] administrator who knows exactly what they're doing should modify the registry. Incorrect changes can cause serious problems. Back up the registry before making any changes so you can restore it if a problem occurs.

1. Run **regedit** to open the Registry Editor.
1. Create a `SQL Server Cryptographic Provider` registry key at `HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\SQL Server Cryptographic Provider`.
1. Right-click the `SQL Server Cryptographic Provider` registry key, and then select **Permissions**.
1. Grant **Full Control** on the `SQL Server Cryptographic Provider` key to the user account running the SQL Server service.

   :::image type="content" source="media/setup-steps-for-extensible-key-management-using-the-azure-key-vault/ekm-part-4-registry-permissions.png" alt-text="Screenshot of the EKM registry key in Registry Editor.":::

1. Select **Apply** and then **OK**.
1. Close Registry Editor and restart the SQL Server service.

   > [!NOTE]  
   > If you use TDE with EKM or Azure Key Vault on a failover cluster instance, also add `HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\SQL Server Cryptographic Provider` to the Cluster Registry Checkpoint routine so the registry syncs across nodes, which facilitates database recovery after failover and key rotation.
   >
   > Run the following PowerShell command to add the registry key to the checkpoint routine:
   >
   > ```powershell
   > Add-ClusterCheckpoint -RegistryCheckpoint "SOFTWARE\Microsoft\SQL Server Cryptographic Provider" -Resourcename "SQL Server"
   > ```

## Step 5: Configure SQL Server

For a note about the minimum permission levels needed for each action in this section, see [B. Frequently Asked Questions](sql-server-connector-maintenance-troubleshooting.md#AppendixB).

### Phase 1: Configure cryptographic provider and credentials in `master`

Choose your authentication model and follow the matching steps.

# [Service principal](#tab/ServicePrincipal)

1. Run **sqlcmd** or open [!INCLUDE [ssmanstudiofull-md](../../../includes/ssmanstudiofull-md.md)].

1. Configure [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] to use EKM by running the following [!INCLUDE [tsql](../../../includes/tsql-md.md)] script:

   ```sql
   -- Enable advanced options.
   USE master;
   GO

   EXEC sp_configure 'show advanced options', 1;
   GO
   RECONFIGURE;
   GO

   -- Enable EKM provider
   EXEC sp_configure 'EKM provider enabled', 1;
   GO
   RECONFIGURE;
   ```

1. Register the [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] Connector as an EKM provider with [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)].

   Create a cryptographic provider by using the [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] Connector, which is an EKM provider for the Azure Key Vault.
   In this example, the provider name is `AzureKeyVault_EKM`.

   ```sql
   CREATE CRYPTOGRAPHIC PROVIDER AzureKeyVault_EKM
   FROM FILE = 'C:\Program Files\SQL Server Connector for Microsoft Azure Key Vault\Microsoft.AzureKeyVaultService.EKM.dll';
   GO
   ```

   > [!NOTE]  
   > The file path length can't exceed 256 characters.

1. Set up a [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] credential for a [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] login to use the key vault.

   Add a credential to each login that performs encryption by using a key from the key vault. There's a one-to-one mapping between credentials and logins - each login must have a unique credential.

   Modify this [!INCLUDE [tsql](../../../includes/tsql-md.md)] script in the following ways:

   - Edit the `IDENTITY` argument (`DocsSampleEKMKeyVault`) to point to your Azure Key Vault.
     - If you're using *global Azure*, replace the `IDENTITY` argument with the name of your Azure Key Vault from [Step 2: Create a key vault](#step-2-create-a-key-vault).
     - If you're using a *private Azure cloud* (for example, Azure Government, Microsoft Azure operated by 21Vianet, or Azure Germany), replace the `IDENTITY` argument with the Vault URI returned in [Create a key vault and key by using PowerShell](#create-a-key-vault-and-key-by-using-powershell). Don't include `https://` in the key vault URI.
   - Replace the first part of the `SECRET` argument with the Microsoft Entra **Client ID** from [Step 1: Set up the authentication model](#step-1-set-up-the-authentication-model). In this example, the **Client ID** is `d956f6b9xxxxxxx`.

     > [!IMPORTANT]  
     > Remove the hyphens from the App (Client) ID.

   - Complete the second part of the `SECRET` argument with the **Client Secret** from Step 1. The final string is a long sequence of letters and numbers without hyphens (except for any hyphens in the Client Secret itself).

   ```sql
   USE master;
   CREATE CREDENTIAL sysadmin_ekm_cred
      -- Set IDENTITY to the vault name (public Azure) or full vault hostname without https:// (sovereign clouds / Managed HSM)
      -- See https://learn.microsoft.com/azure/key-vault/general/about-keys-secrets-certificates#dns-suffixes-for-base-url
      WITH IDENTITY = 'DocsSampleEKMKeyVault',
            --<----Application (Client) ID ---><--Microsoft Entra app (Client) ID secret-->
      SECRET = 'd956f6b9xxxxxxxyrA8X~PldtMCvUZPxxxxxxxx'
   FOR CRYPTOGRAPHIC PROVIDER AzureKeyVault_EKM;

   -- Add the credential to the SQL Server administrator's domain login
   ALTER LOGIN [<domain>\<login>]
       ADD CREDENTIAL sysadmin_ekm_cred;
   ```

   For an example of using variables and programmatically removing hyphens from the Client ID, see [CREATE CREDENTIAL](../../../t-sql/statements/create-credential-transact-sql.md).

1. Open the Azure Key Vault key in your SQL Server instance.

   Whether you created a new key or imported an asymmetric key in [Step 2: Create a key vault](#step-2-create-a-key-vault), open the key in [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] by using `CREATE ASYMMETRIC KEY`.

   > [!IMPORTANT]  
   > Complete the registry prerequisites before you perform this step.

   In the following examples:

   - Replace `EKMSampleASYKey` with the name that you want to use in SQL Server.
   - Replace `ContosoRSAKey0` with your key name in Azure Key Vault or Managed HSM.

   Use a versionless key name (recommended for most scenarios):

   ```sql
   CREATE ASYMMETRIC KEY EKMSampleASYKey
      FROM PROVIDER [AzureKeyVault_EKM]
      WITH PROVIDER_KEY_NAME = 'ContosoRSAKey0',
         CREATION_DISPOSITION = OPEN_EXISTING;
   ```

   Use a specific key version when you need to pin operations to one version:

   ```sql
   CREATE ASYMMETRIC KEY EKMSampleASYKey
      FROM PROVIDER [AzureKeyVault_EKM]
      WITH PROVIDER_KEY_NAME = 'ContosoRSAKey0/1a4d3b9b393c4678831ccc60def75379',
         CREATION_DISPOSITION = OPEN_EXISTING;
   ```

   In this example, `1a4d3b9b393c4678831ccc60def75379` is the specific key version that SQL Server uses for database operations.

1. Create a new login by using the asymmetric key in [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] that you created in the preceding step.

   ```sql
   -- Create a login that associates the asymmetric key with this login
   CREATE LOGIN TDE_Login
       FROM ASYMMETRIC KEY EKMSampleASYKey;
   ```

1. Move the credential mapping from the original administrator login to the login created from the asymmetric key.

   SQL Server uses the login created from the asymmetric key (`TDE_Login`) for EKM operations. To ensure SQL Server can access Azure Key Vault during encryption and recovery operations, map the credential to `TDE_Login` instead of the original setup login.

   ```sql
   -- Remove the service principal credential from the original setup login
   ALTER LOGIN [<domain>\<login>]
      DROP CREDENTIAL sysadmin_ekm_cred;

   -- Map the service principal credential to the login created from the asymmetric key
   ALTER LOGIN TDE_Login
      ADD CREDENTIAL sysadmin_ekm_cred;
   ```

# [Managed identity](#tab/ManagedIdentity)

1. Run **sqlcmd** or open [!INCLUDE [ssmanstudiofull-md](../../../includes/ssmanstudiofull-md.md)].

1. Configure [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] to use EKM by running the following [!INCLUDE [tsql](../../../includes/tsql-md.md)] script:

   ```sql
   -- Enable advanced options.
   USE master;
   GO

   EXEC sp_configure 'show advanced options', 1;
   GO
   RECONFIGURE;
   GO

   -- Enable EKM provider
   EXEC sp_configure 'EKM provider enabled', 1;
   GO
   RECONFIGURE;
   ```

1. Register the [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] Connector as an EKM provider with [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)].

   Create a cryptographic provider by using the [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] Connector, which is an EKM provider for the Azure Key Vault.
   In this example, the provider name is `AzureKeyVault_EKM`.

   ```sql
   CREATE CRYPTOGRAPHIC PROVIDER AzureKeyVault_EKM
   FROM FILE = 'C:\Program Files\SQL Server Connector for Microsoft Azure Key Vault\Microsoft.AzureKeyVaultService.EKM.dll';
   GO
   ```

   > [!NOTE]  
   > The file path length can't exceed 256 characters.

1. Set up a [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] credential for a [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] login to use the key vault.

   Add a credential to each login that performs encryption by using a key from the key vault. There's a one-to-one mapping between credentials and logins - each login must have a unique credential.

   When you use managed identity authentication, you don't need a client secret. Set the `SECRET` argument to an empty string. Azure handles authentication automatically by using the managed identity assigned to the SQL Server VM.

   Replace the `IDENTITY` argument with the name of your Azure Key Vault (or the Managed HSM URI):

   ```sql
   USE master;
   -- Credential name = full vault hostname without https://
   -- See https://learn.microsoft.com/azure/key-vault/general/about-keys-secrets-certificates#dns-suffixes-for-base-url
   CREATE CREDENTIAL [DocsSampleEKMKeyVault.vault.azure.net]
      WITH IDENTITY = 'Managed Identity'
   FOR CRYPTOGRAPHIC PROVIDER AzureKeyVault_EKM;

   -- Add the credential to the SQL Server administrator's domain login
   ALTER LOGIN [<domain>\<login>]
       ADD CREDENTIAL [DocsSampleEKMKeyVault.vault.azure.net];
   ```

1. Open the Azure Key Vault key in your SQL Server instance.

   Whether you created a new key or imported an asymmetric key in [Step 2: Create a key vault](#step-2-create-a-key-vault), open the key in [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] by using `CREATE ASYMMETRIC KEY`.

   > [!IMPORTANT]  
   > Complete the registry prerequisites before you perform this step.

   In the following examples:

   - Replace `EKMSampleASYKey` with the name that you want to use in SQL Server.
   - Replace `ContosoRSAKey0` with your key name in Azure Key Vault or Managed HSM.

   Use a versionless key name (recommended for most scenarios):

   ```sql
   CREATE ASYMMETRIC KEY EKMSampleASYKey
      FROM PROVIDER [AzureKeyVault_EKM]
      WITH PROVIDER_KEY_NAME = 'ContosoRSAKey0',
         CREATION_DISPOSITION = OPEN_EXISTING;
   ```

   Use a specific key version when you need to pin operations to one version:

   ```sql
   CREATE ASYMMETRIC KEY EKMSampleASYKey
      FROM PROVIDER [AzureKeyVault_EKM]
      WITH PROVIDER_KEY_NAME = 'ContosoRSAKey0/1a4d3b9b393c4678831ccc60def75379',
         CREATION_DISPOSITION = OPEN_EXISTING;
   ```

   In this example, `1a4d3b9b393c4678831ccc60def75379` is the specific key version that SQL Server uses for database operations.

1. Create a new login by using the asymmetric key in [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] that you created in the preceding step.

   ```sql
   -- Create a login that associates the asymmetric key with this login
   CREATE LOGIN TDE_Login
       FROM ASYMMETRIC KEY EKMSampleASYKey;
   ```

1. Move the credential mapping from the original administrator login to the login created from the asymmetric key.

   SQL Server uses the login created from the asymmetric key (`TDE_Login`) for EKM operations. To ensure SQL Server can access Azure Key Vault during encryption and recovery operations, map the credential to `TDE_Login` instead of the original setup login.

   ```sql
   -- Remove the managed identity credential from the original setup login
   ALTER LOGIN [<domain>\<login>]
      DROP CREDENTIAL [DocsSampleEKMKeyVault.vault.azure.net];

   -- Map the managed identity credential to the login created from the asymmetric key
   ALTER LOGIN TDE_Login
      ADD CREDENTIAL [DocsSampleEKMKeyVault.vault.azure.net];
   ```

   Replace `[DocsSampleEKMKeyVault.vault.azure.net]` with your managed identity credential name.

---

### Phase 2: Encrypt and validate the user database

#### Configure the user database to be encrypted

1. Create a test database to encrypt by using the Azure Key Vault key.

   ```sql
   -- Create a test database for the TDE example.
   CREATE DATABASE TestTDE;
   ```

1. Create a database encryption key by using the server asymmetric key (`EKMSampleASYKey`).

   ```sql
   USE TestTDE;
   -- Create a DEK protected by the EKM asymmetric key.
   CREATE DATABASE ENCRYPTION KEY
   WITH ALGORITHM = AES_256
   ENCRYPTION BY SERVER ASYMMETRIC KEY EKMSampleASYKey;
   ```

1. Enable TDE on the database by setting `ENCRYPTION ON`.

   ```sql
   -- Enable TDE for the database.
   ALTER DATABASE TestTDE
       SET ENCRYPTION ON;
   ```

#### Verify key usage and encryption state

1. In the `master` database, execute the following [!INCLUDE [tsql](../../../includes/tsql-md.md)] query to verify that the EKM asymmetric key exists and capture its thumbprint.

   ```sql
   SELECT name,
          algorithm_desc,
          thumbprint
   FROM sys.asymmetric_keys;
   ```

   The statement returns output similar to the following:

   ```output
   name            algorithm_desc    thumbprint
   EKMSampleASYKey RSA_2048          <key thumbprint>
   ```

1. In the user database (`TestTDE`), execute the following [!INCLUDE [tsql](../../../includes/tsql-md.md)] query to verify that TDE is enabled and that the database encryption key is protected by an asymmetric key.

   ```sql
   SELECT encryptor_type,
          encryption_state_desc,
          encryptor_thumbprint
   FROM sys.dm_database_encryption_keys
   WHERE database_id = DB_ID('TestTDE');
   ```

   The statement returns output similar to the following:

   ```output
   encryptor_type encryption_state_desc encryptor_thumbprint
   ASYMMETRIC KEY ENCRYPTED             <key thumbprint>
   ```

   Confirm that `encryptor_thumbprint` matches the thumbprint returned from `sys.asymmetric_keys`. A mismatch usually indicates that the database encryption key is protected by a different key than expected.

#### Clean up

Use the cleanup steps that match the authentication model you configured.

# [Service principal](#tab/ServicePrincipal)

1. Clean up the test objects you created in this procedure.

   ```sql
   -- CLEAN UP: shared objects + service principal credential
   USE master;
   GO
   ALTER DATABASE [TestTDE] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
   DROP DATABASE [TestTDE];
   GO

   DROP LOGIN [TDE_Login];
   GO

   DROP ASYMMETRIC KEY [EKMSampleASYKey];
   DROP CRYPTOGRAPHIC PROVIDER [AzureKeyVault_EKM];
   GO

   DROP CREDENTIAL [sysadmin_ekm_cred];
   GO
   ```

1. Review whether you should remove the `SQL Server Cryptographic Provider` registry key.

   > [!IMPORTANT]
   > The `SQL Server Cryptographic Provider` registry key isn't removed automatically after you delete EKM keys.
   >
   > Delete this registry key only when you no longer need EKM on the instance. Deleting it too early can break EKM functionality and recovery operations.
   >
   > Registry path: `HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\SQL Server Cryptographic Provider`

# [Managed identity](#tab/ManagedIdentity)

1. Clean up the test objects you created in this procedure.

   Replace `[DocsSampleEKMKeyVault.vault.azure.net]` with your managed identity credential name.

   ```sql
   -- CLEAN UP: shared objects + managed identity credential
   USE master;
   GO
   ALTER DATABASE [TestTDE] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
   DROP DATABASE [TestTDE];
   GO

   DROP LOGIN [TDE_Login];
   GO

   DROP ASYMMETRIC KEY [EKMSampleASYKey];
   DROP CRYPTOGRAPHIC PROVIDER [AzureKeyVault_EKM];
   GO

   DROP CREDENTIAL [DocsSampleEKMKeyVault.vault.azure.net];
   GO
   ```

1. Review whether you should remove the `SQL Server Cryptographic Provider` registry key.

   > [!IMPORTANT]
   > The `SQL Server Cryptographic Provider` registry key isn't removed automatically after you delete EKM keys.
   >
   > Delete this registry key only when you no longer need EKM on the instance. Deleting it too early can break EKM functionality and recovery operations.
   >
   > Registry path: `HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\SQL Server Cryptographic Provider`

---

## Rotate asymmetric keys for TDE with Azure Key Vault

Use a dedicated operational guide for key rotation steps, including authentication-specific scripts, verification, and safety checks:

- [Rotate asymmetric keys for TDE by using Azure Key Vault on SQL Server](rotate-transparent-data-encryption-asymmetric-keys-azure-key-vault-sql-server.md)

> [!IMPORTANT]  
> Don't delete previous versions of the key after rotation. Earlier versions might still be required for restoring older backups, log files, and recovery artifacts.

## Related content

- [Use SQL Server Connector with SQL Encryption Features](use-sql-server-connector-with-sql-encryption-features.md)
- [Extensible Key Management using Azure Key Vault (SQL Server)](extensible-key-management-using-azure-key-vault-sql-server.md)
- [SQL Server Connector maintenance and troubleshooting](sql-server-connector-maintenance-troubleshooting.md)
