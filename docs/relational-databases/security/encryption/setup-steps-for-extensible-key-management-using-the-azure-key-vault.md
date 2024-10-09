---
title: "Set up Transparent Data Encryption (TDE) Extensible Key Management with Azure Key Vault"
description: Install and configure the SQL Server Connector for Azure Key Vault.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 09/23/2024
ms.service: sql
ms.subservice: security
ms.topic: conceptual
helpviewer_keywords:
  - "Extensible Key Management"
  - "EKM, with key vault setup"
  - "SQL Server Connector, setup"
  - "SQL Server Connector"
  - "TDE, AKV, EKM"
---
# Set up SQL Server TDE Extensible Key Management by using Azure Key Vault

[!INCLUDE [sqlserver](../../../includes/applies-to-version/sqlserver.md)]

In this article, you install and configure the [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] Connector for Azure Key Vault.

[!INCLUDE [entra-id](../../../includes/entra-id.md)]

Extensible Key Management using Azure Key Vault (AKV) is available for SQL Server on Linux environments, starting with [!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)] Cumulative Update 12. Follow the same instructions, but skip steps 3 and 4.

## Prerequisites

Before you begin using Azure Key Vault with your SQL Server instance, be sure that you've met the following prerequisites:

- You must have an Azure subscription.

- Install [Azure PowerShell version 5.2.0 or later](/powershell/azure/).

- Create a Microsoft Entra tenant.

- Familiarize yourself with the principles of Extensible Key Management (EKM) storage with Azure Key Vault by reviewing [Extensible Key Management Using Azure Key Vault (SQL Server)](extensible-key-management-using-azure-key-vault-sql-server.md).

- Be able to modify the registry on the [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] computer.

- Install the version of Visual Studio C++ Redistributable that's based on the version of SQL Server that you're running:

  SQL Server version | Visual Studio C++ Redistributable version
  ---------|---------
  2008, 2008 R2, 2012, 2014 | [Visual C++ Redistributable packages for Visual Studio 2013](https://www.microsoft.com/download/details.aspx?id=40784)
  2016, 2017, 2019, 2022 | [Visual C++ Redistributable for Visual Studio 2015](https://www.microsoft.com/download/details.aspx?id=48145)

- Familiarize yourself with [Access Azure Key Vault behind a firewall](/azure/key-vault/general/access-behind-firewall) if you plan to use the SQL Server Connector for Azure Key Vault behind a firewall or with a proxy server.

> [!NOTE]  
> In [!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)] CU 14 and later versions, [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] on Linux supports TDE Extensible Key Management with Azure Key Vault. Steps 3 and 4 in this guide aren't required for [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] on Linux.

<a name='step-1-set-up-an-azure-ad-service-principal'></a>

## Step 1: Set up a Microsoft Entra service principal

To grant your SQL Server instance access permissions to your Azure key vault, you need a service principal account in Microsoft Entra ID.

1. Sign in to the [Azure portal](https://ms.portal.azure.com/), and do either of the following:

    - Select the **Microsoft Entra ID** button.

      :::image type="content" source="media/setup-steps-for-extensible-key-management-using-the-azure-key-vault/ekm-part-1-login-portal.png" alt-text="Screenshot of the Azure services pane.":::

    - Select **More services** and then, in the **All services** pane, type **Microsoft Entra ID**.

1. Register an application with Microsoft Entra ID by doing the following. For detailed step-by-step instructions, see the **Get an identity for the application** section of the Azure Key Vault blog post, [Azure Key Vault – Step by Step](/archive/blogs/kv/azure-key-vault-step-by-step#get-an-identity-for-the-application).

   1. On the **Manage** section of your **Microsoft Entra ID** resource, select **App registrations**.

      :::image type="content" source="media/setup-steps-for-extensible-key-management-using-the-azure-key-vault/ekm-part-1-azure-active-directory-app-register.png" alt-text="Screenshot of the Microsoft Entra ID Overview page in the Azure portal.":::

   1. On the **App registrations** page, select **New registration**.

      :::image type="content" source="media/setup-steps-for-extensible-key-management-using-the-azure-key-vault/ekm-part-1-azure-active-directory-new-registration.png" alt-text="Screenshot of the App registrations pane in the Azure portal.":::

   1. On the **Register an application** pane, enter the user-facing name for the app, and then select **Register**.

      :::image type="content" source="media/setup-steps-for-extensible-key-management-using-the-azure-key-vault/ekm-part-1-azure-active-directory-register.png" alt-text="Screenshot of the Register an application pane." lightbox="media/setup-steps-for-extensible-key-management-using-the-azure-key-vault/ekm-part-1-azure-active-directory-register.png":::

   1. In the left pane, select **Certificates & secrets > Client secrets > New client secret**.

      :::image type="content" source="media/setup-steps-for-extensible-key-management-using-the-azure-key-vault/ekm-part-1-azure-active-directory-certificates-secrets.png" alt-text="Screenshot of the Certificates & secrets pane for the App in the Azure portal." lightbox="media/setup-steps-for-extensible-key-management-using-the-azure-key-vault/ekm-part-1-azure-active-directory-certificates-secrets.png":::

   1. Under **Add a client secret**, enter a description and an appropriate expiration, and then select **Add**. You can't choose an expiration period greater than 24 months. For more information, see [Add a client secret](/azure/active-directory/develop/quickstart-register-app#add-a-client-secret).

      :::image type="content" source="media/setup-steps-for-extensible-key-management-using-the-azure-key-vault/ekm-part-1-azure-active-directory-add-secret.png" alt-text="Screenshot of the Add a client secret section for the App in the Azure portal.":::

   1. On the **Certificates & secrets** pane, under **Value**, select the **Copy** button next to the value of the client secret to be used to create an asymmetric key in SQL Server.

      :::image type="content" source="media/setup-steps-for-extensible-key-management-using-the-azure-key-vault/ekm-part-1-azure-active-directory-new-secret.png" alt-text="Screenshot of the secret value in the Azure portal.":::

   1. In the left pane, select **Overview** and then, in the **Application (client) ID** box, copy the value to be used to create an asymmetric key in SQL Server.

      :::image type="content" source="media/setup-steps-for-extensible-key-management-using-the-azure-key-vault/ekm-part-1-azure-active-directory-application-id.png" alt-text="Screenshot of the Application (client) ID value on the Overview pane.":::

## Step 2: Create a key vault

Select the method you want to use to create a key vault.

## [Azure portal](#tab/portal)

### Create a key vault by using the Azure portal

You can use the Azure portal to create the key vault and then add a Microsoft Entra principal to it.

1. Create a resource group.

   All Azure resources that you create via the Azure portal must be contained in a resource group, which you create to house your key vault. The resource name in this example is *DocsSampleRG*. Choose your own resource group and key vault name, because all key vault names must be globally unique.

   On the **Create a resource group** pane, under **Project details**, enter the values, and then select **Review + create**.

   :::image type="content" source="media/setup-steps-for-extensible-key-management-using-the-azure-key-vault/ekm-part-2-create-resource-group.png" alt-text="Screenshot of the Create a resource group pane in the Azure portal.":::

1. In the Azure portal, search or select the **Key vaults** services to create a key vault. Select **Create**.

    On the **Create a key vault** pane, select the **Basics** tab. Enter the appropriate values for the tab. We also recommend enabling purge protection.

    :::image type="content" source="media/setup-steps-for-extensible-key-management-using-the-azure-key-vault/ekm-part-2-create-key-vault.png" alt-text="Screenshot of the Create key vault pane in the Azure portal.":::

1. On the **Access configuration** tab, you have the option of selecting **Azure role-based access control** or **Vault access policy**. We go over both options, but the **Azure role-based access control** option is recommended. For more information, see [Access model overview](/azure/key-vault/general/security-features#access-model-overview).

    :::image type="content" source="media/setup-steps-for-extensible-key-management-using-the-azure-key-vault/ekm-part-2-access-configuration.png" alt-text="Screenshot of the Create key vault pane and Access configuration tab in the Azure portal." lightbox="media/setup-steps-for-extensible-key-management-using-the-azure-key-vault/ekm-part-2-access-configuration.png":::

1. You can leave the **Networking** tab as the default, or you can configure the network settings for the key vault. If you're using a firewall with the key vault, the option **Allow trusted Microsoft services to bypass the firewall** must be enabled, unless you're using [private endpoint connections](/azure/key-vault/general/private-link-service). For more information, see [Configure Azure Key Vault firewalls and virtual networks](/azure/key-vault/general/network-security).

1. Select **Review + create** and create the key vault.

#### Azure role-based access control

The recommended method is to use [Azure role-based access control (RBAC)](/azure/role-based-access-control/overview) to assign permissions to the key vault. This method allows you to assign permissions to users, groups, and applications at a more granular level. You can assign permissions to the key vault at the management plane (Azure role assignments), and at the data plane (key vault access policies). If you're only able to use access policy, you can skip this section and go to the [Vault access policy](#vault-access-policy) section. For more information on Azure Key Vault RBAC permissions, see [Azure built-in roles for Key Vault data plane operations](/azure/key-vault/general/rbac-guide#azure-built-in-roles-for-key-vault-data-plane-operations).

1. Go to the key vault resource that you created, and select the **Access control (IAM)** setting.

1. Select **Add** > **Add role assignment**.

   :::image type="content" source="media/setup-steps-for-extensible-key-management-using-the-azure-key-vault/ekm-part-2-add-role-assignment.png" alt-text="Screenshot of the Add role assignment button on the Access control (IAM) pane in the Azure portal." lightbox="media/setup-steps-for-extensible-key-management-using-the-azure-key-vault/ekm-part-2-add-role-assignment.png":::

1. The EKM application needs the **Key Vault Crypto Service Encryption User** role to perform wrap and unwrap operations. Search for **Key Vault Crypto Service Encryption User** and select the role. Select **Next**.

   :::image type="content" source="media/setup-steps-for-extensible-key-management-using-the-azure-key-vault/ekm-part-2-select-role-assignment.png" alt-text="Screenshot of selecting a role assignment in the Azure portal." lightbox="media/setup-steps-for-extensible-key-management-using-the-azure-key-vault/ekm-part-2-select-role-assignment.png":::

1. In the **Members** tab, select the **Select members** option, and then search for the Microsoft Entra application that you created in Step 1. Select the application and then the **Select** button.

   :::image type="content" source="media/setup-steps-for-extensible-key-management-using-the-azure-key-vault/ekm-part-2-add-app-to-role.png" alt-text="Screenshot of the Select members pane for adding a role assignment in the Azure portal." lightbox="media/setup-steps-for-extensible-key-management-using-the-azure-key-vault/ekm-part-2-add-app-to-role.png":::

1. Select **Review + assign** twice to complete the role assignment.

1. The user creating the key needs the **Key Vault Administrator** role. Search for **Key Vault Administrator** and select the role. Select **Next**.

1. Just like the previous steps, add the member creating the key and assign the role.

#### Vault access policy

> [!NOTE]  
> If you are using the **Azure role-based access control** option, you can skip this section. If you are changing the permission model, you can do so by going to the **Access configuration** menu of the key vault. Make sure you have the correct permissions to manage the key vault. For more information, see [Enable Azure RBAC permissions on Key Vault](/azure/key-vault/general/rbac-guide#enable-azure-rbac-permissions-on-key-vault).

1. From the **Access configuration** tab, select **Vault access policy**. If you're using an existing Key vault, you can select the **Access policies** menu from the Key vault resource, and select **Create**.

1. On the **Create an access policy** pane, select *Get* and *List* permissions from the **Key Management Operations** options. Select *Unwrap Key* and *Wrap Key* permissions from the **Cryptographic Operations** options. Select **Next**

    :::image type="content" source="media/setup-steps-for-extensible-key-management-using-the-azure-key-vault/ekm-part-2-add-access-policy.png" alt-text="Screenshot of the Add Access Policy link on the Access policies pane.":::

1. On the **Principal** tab, select the application that was created in Step 1.

   :::image type="content" source="media/setup-steps-for-extensible-key-management-using-the-azure-key-vault/ekm-part-2-select-principal.png" alt-text="Screenshot of application search box on the Principal pane.":::

1. Select **Next** and then **Create**.

#### Create a key

1. On the **Key Vault** pane, select **Keys** and then select the option **Generate/Import**. This opens the **Create a key** pane. Enter a key vault name. Select the **Generate** option, and enter a name for the key. The [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] Connector requires the key name to only use the characters "a-z", "A-Z", "0-9", and "-", with a 26-character limit.

1. Use key type **RSA** and **RSA key size** as **2048**. EKM currently only supports an RSA key. Set activation and expiration dates as appropriate and set **Enabled** as **Yes**.

   :::image type="content" source="media/setup-steps-for-extensible-key-management-using-the-azure-key-vault/ekm-part-2-add-key-vault-key.png" alt-text="Screenshot of the Create Key pane.":::

### Best practices

To ensure quick key recovery and be able to access your data outside of Azure, we recommend the following best practices:

- Create your encryption key locally on a local hardware security module (HSM) device. Be sure to use an asymmetric RSA 2048 or 3072 key so that it's supported by SQL Server.
- Import the encryption key to your Azure key vault. This process is described in the next sections.
- Before you use the key in your Azure key vault for the first time, do an Azure key vault key backup using the `Backup-AzureKeyVaultKey` PowerShell cmdlet.
- Whenever you make any changes to the key (for example, adding ACLs, tags, or key attributes), be sure to do another Azure key vault key backup.

  > [!NOTE]  
  > Backing up a key is an Azure Key Vault key operation which returns a file that can be saved anywhere.

  Using the SQL Server Connector for Azure Key Vault behind a firewall or proxy server can affect performance if traffic is delayed or blocked. Familiarize yourself with [Access Azure Key Vault behind a firewall](/azure/key-vault/general/access-behind-firewall) to ensure the correct rules are in place.

## [PowerShell](#tab/powershell)

### Create a key vault and key by using PowerShell

The key vault and key that you create here are used by the [!INCLUDE [ssdenoversion-md](../../../includes/ssdenoversion-md.md)] for encryption key protection.

> [!IMPORTANT]  
> The subscription where the key vault is created must be in the same default Microsoft Entra tenant where the Microsoft Entra service principal was created. If you want to use a Microsoft Entra tenant other than your default tenant to create a service principal for the SQL Server Connector, you must change the default Microsoft Entra tenant in your Azure account before you create your key vault. To learn how to change the default Microsoft Entra tenant to the one you want to use, see the "Frequently asked questions" section of [SQL Server Connector maintenance & troubleshooting](sql-server-connector-maintenance-troubleshooting.md#AppendixB).

1. Install and sign in to [Azure PowerShell 5.2.0 or later](/powershell/azure/) by using the following command:

   ```powershell
   Connect-AzAccount
   ```

   The statement returns:

   ```console
   Environment           : AzureCloud
   Account               : <account_name>
   TenantId              : <tenant_id>
   SubscriptionId        : <subscription_id>
   CurrentStorageAccount :
   ```

   > [!NOTE]  
   > If you have multiple subscriptions and want to specify one subscription to use for the vault, run `Get-AzSubscription` to view the subscriptions and `Select-AzSubscription` to choose the correct subscription. Otherwise, PowerShell will select one for you by default.

1. Create a resource group.

   All Azure resources that you create via PowerShell must be contained in resource groups, which you create to house your key vault. The resource name in this example is *DocsSampleRG*. Choose your own resource group and key vault name, because all key vault names must be globally unique.

   ```powershell
   New-AzResourceGroup -Name DocsSampleRG -Location 'East US'
   ```

   The statement returns:

   ```console
   ResourceGroupName: DocsSampleRG
   Location         : eastus
   ProvisioningState: Succeeded
   Tags             :
   ResourceId       : /subscriptions/<subscription_id>/
                       resourceGroups/DocsSampleRG
   ```

   > [!NOTE]  
   > For the `-Location` parameter, use the command `Get-AzureLocation` to identify how to specify a location other than the one in this example. If you need more information, type **Get-Help Get-AzureLocation**.

1. Create a key vault.

   The `New-AzKeyVault` cmdlet requires a resource group name, a key vault name, and a geographic location. For example, for a key vault named `DocsSampleEKMKeyVault`, run:

   ```powershell
   New-AzKeyVault -VaultName 'DocsSampleEKMKeyVault' `
      -ResourceGroupName 'DocsSampleRG' -Location 'East US'
   ```

   Record the name of your key vault.

   The statement returns:

   ```console
   Vault Name                       : DocsSampleEKMKeyVault
   Resource Group Name              : DocsSampleRG
   Location                         : East US
   ResourceId                       : /subscriptions/<subscription_id>/
                                       resourceGroups/DocsSampleRG/providers/
                                       Microsoft/KeyVault/vaults/DocsSampleEKMKeyVault
   Vault URI: https://DocsSampleEKMKeyVault.vault.azure.net
   Tenant ID                        : <tenant_id>
   SKU                              : Standard
   Enabled For Deployment?          : False
   Enabled For Template Deployment? : False
   Enabled For Disk Encryption?     : False
   Access Policies                  :
             Tenant ID              : <tenant_id>
             Object ID              : <object_id>
             Application ID         :
             Display Name           : <display_name>
             Permissions to Keys    : get, create, delete, list, update, import,
                                      backup, restore
             Permissions to Secrets : all
   Tags                             :
   ```

1. Grant permissions for the Microsoft Entra service principal to access the Azure key vault.

   You can authorize other users and applications to use your key vault. For our example, let's use the service principal that you created in [Step 1: Set up a Microsoft Entra service principal](#step-1-set-up-an-azure-ad-service-principal) to authorize the [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] instance.

   > [!IMPORTANT]  
   > The Microsoft Entra service principal must have at least the *get*, *list*, *wrapKey*, and *unwrapKey* permissions for the key vault.

   As shown in the following command, you use the **App (Client) ID** from [Step 1: Set up a Microsoft Entra service principal](#step-1-set-up-an-azure-ad-service-principal) for the `ServicePrincipalName` parameter. `Set-AzKeyVaultAccessPolicy` runs silently with no output if it runs successfully.

   ```powershell
   Set-AzKeyVaultAccessPolicy -VaultName 'DocsSampleEKMKeyVault' `
     -ServicePrincipalName 9A57CBC5-4C4C-40E2-B517-EA677 `
     -PermissionsToKeys get, list, wrapKey, unwrapKey
   ```

   Call the `Get-AzKeyVault` cmdlet to confirm the permissions. In the statement output under `Access Policies`, you should see your Microsoft Entra application name listed as another tenant that has access to this key vault.

1. Generate an asymmetric key in the key vault. You can do so in either of two ways: import an existing key or create a new key.

   > [!NOTE]  
   > SQL Server supports only 2048-bit & 3072-bit RSA keys and 2048-bit & 3072-bit RSA-HSM keys.

### Best practices

To ensure quick key recovery and be able to access your data outside of Azure, we recommend the following best practices:

- Create your encryption key locally on a local hardware security module (HSM) device. Be sure to use an asymmetric RSA 2048 or 3072 key so that it's supported by SQL Server.
- Import the encryption key to your Azure Key Vault. This process is described in the next sections.
- Before you use the key in your Azure Key Vault for the first time, do an Azure Key Vault key backup using the `Backup-AzureKeyVaultKey` PowerShell cmdlet.
- Whenever you make any changes to the key (for example, adding ACLs, tags, or key attributes), be sure to do another Azure Key Vault key backup.

  > [!NOTE]  
  > Backing up a key is an Azure Key Vault key operation which returns a file that can be saved anywhere.

### Types of keys

You can generate four types of keys in an Azure Key Vault that will work with SQL Server: Asymmetric 2048-bit & 3072-bit RSA keys and 2048-bit & 3072-bit RSA-HSM keys.

- **Software-protected**: Processed in software and encrypted at rest. Operations on software-protected keys occur on Azure virtual machines. We recommend this type for keys that aren't used in a production deployment.

- **HSM-protected**: Created and protected by a hardware security module for additional security. The cost is about USD 1 per key version.

  > [!IMPORTANT]  
  > For the SQL Server Connector, use only the characters a-z, A-Z, 0-9, and hyphens (-), with a 26-character limit.
  > Different key versions under the same key name in an Azure Key Vault don't work with the [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] Connector. To rotate an Azure Key Vault key that's being used by [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)], see the Key Rollover steps in the "A. Maintenance Instructions for [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] Connector" section of [SQL Server Connector Maintenance & Troubleshooting](sql-server-connector-maintenance-troubleshooting.md).
  >
  > When rotating versions of the key, do not disable the version originally used to encrypt the database. SQL Server will be unable to recover the database (it will be in a 'recovery pending' state) and might generate a 'Crypto Exception' memory dump until the old version is enabled.

### Import an existing key

If you have an existing 2048-bit RSA software-protected key, you can upload the key to your Azure Key Vault. For example, if you have a PFX file saved to your `C:\` drive in a file named `softkey.pfx` that you want to upload to the Azure Key Vault, run the following command to set the variable `securepfxpwd` for a password of `12987553` for the PFX file:

```powershell
$securepfxpwd = ConvertTo-SecureString -String '12987553' `
  -AsPlainText -Force
```

Then you can run the following command to import the key from the PFX file, which protects the key by hardware (recommended) in the Key Vault service:

```powershell
    Add-AzureKeyVaultKey -VaultName 'DocsSampleEKMKeyVault' `
      -Name 'DocsFirstKey' -KeyFilePath 'C:\softkey.pfx' `
      -KeyFilePassword $securepfxpwd $securepfxpwd  -Destination 'HSM'
```

> [!CAUTION]  
> We highly recommend importing the asymmetric key for production scenarios, because doing so allows the administrator to escrow the key in a key escrow system. If the asymmetric key is created in the vault, it can't be escrowed, because the private key can never leave the vault. Keys that are used to protect critical data should be escrowed. The loss of an asymmetric key will result in permanent data loss.

### Create a new key

Alternatively, you can create a new encryption key directly in your Azure Key Vault and make it either software-protected or HSM-protected. In this example, let's create a software-protected key by using the `Add-AzureKeyVaultKey` cmdlet:

```powershell
Add-AzureKeyVaultKey -VaultName 'DocsSampleEKMKeyVault' `
  -Name 'ContosoRSAKey0' -Destination 'Software'
```

The statement returns:

```console
Attributes : Microsoft.Azure.Commands.KeyVault.Models.KeyAttributes
Key        :  {"kid":"https:DocsSampleEKMKeyVault.azure.net/keys/
                ContosoRSAKey0/<guid>","dty":"RSA:,"key_ops": ...
VaultName  : contosodevkeyvault
Name       : contosoRSAKey0
Version    : <guid>
Id         : https://DocsSampleEKMKeyVault.vault.azure.net:443/
              keys/ContosoRSAKey0/<guid>
```

> [!IMPORTANT]  
> The key vault supports multiple versions of the same named key, but keys to be used by the [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] Connector should not be versioned or rolled. If the administrator wants to roll the key that's used for [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] encryption, a new key with a different name should be created in the key vault and used to encrypt the DEK.

---

## Optional - Configure an Azure Key Vault Managed HSM (Hardware Security Module)

[Azure Key Vault Managed HSM (Hardware Security Module)](/azure/key-vault/managed-hsm/overview) is supported for SQL Server and SQL Server on Azure Virtual Machines (VMs) when using the latest version of the [SQL Server Connector](use-sql-server-connector-with-sql-encryption-features.md), as well as Azure SQL. Managed HSM is a fully managed, highly available, single-tenant HSM service. Managed HSM provides a secure foundation for cryptographic operations and key storage. Managed HSM is designed to meet the most stringent security and compliance requirements.

In [step 2](#step-2-create-a-key-vault), we learned how to create a key vault and key in Azure Key Vault. You can optionally use an Azure Key Vault Managed HSM to store or create a key to be used with the SQL Server Connector. Here are the steps:

1. Create an Azure Key Vault Managed HSM. This can be done using the Azure portal by searching for the Azure Key Vault Managed HSM service and creating the new resource, or by using [the Azure CLI](/azure/key-vault/managed-hsm/quick-create-cli), [PowerShell](/azure/key-vault/managed-hsm/quick-create-powershell), or an [ARM template](/azure/key-vault/managed-hsm/quick-create-template).

1. Activate the Managed HSM. Only the designated administrators that were assigned during the Managed HSM creation can activate the HSM. This can be done by selecting the Managed HSM resource in the Azure portal by selecting **Download Security Domain** in the **Overview** menu of the resource. Then follow one of the [quickstarts to activate your Managed HSM](/azure/key-vault/managed-hsm/quick-create-cli#activate-your-managed-hsm).

1. Grant permissions for the Microsoft Entra service principal to access the Managed HSM. The **Managed HSM Administrator** role doesn't give permissions to create a key. Similar to [step 2](#step-2-create-a-key-vault), the EKM application needs the **Managed HSM Crypto User** or **Managed HSM Crypto Service Encryption User** role to perform wrap and unwrap operations. Choose the **Enterprise application** type when adding the principal for the role assignment. For more information, see [Local RBAC built-in roles for Managed HSM](/azure/key-vault/managed-hsm/built-in-roles).

1. In the Azure Key Vault Managed HSM service menu, under **Setting**, select **Keys**. In the **Keys** window, select **Generate/Import/Restore Backup** to create a key or import an existing key.

   > [!NOTE]
   > When creating a credential to access the Managed HSM, the identity is `<name of Managed HSM>.managedhsm.azure.net`, which can be found in the Azure Key Vault Managed HSM **Overview** as the **HSM URI** in the Azure portal.
   >
   > Automatic key rotation is supported in Azure Key Vault Managed HSM. For more information, see [Configure key auto-rotation in Azure Managed HSM](/azure/key-vault/managed-hsm/key-rotation).
   >
   > [SQL Server Connector version 15.0.2000.440](https://www.microsoft.com/en-us/download/details.aspx?id=45344) or later is required to support Azure Key Vault Managed HSM.
   >
   > Managed HSM supports private endpoint connections. For more information, see [Integrate Managed HSM with Azure Private Link](/azure/key-vault/managed-hsm/private-link). In this configuration, the **Microsoft trusted service bypass** option must be enabled for the Azure Key Vault Managed HSM **Networking** setting.

## Step 3: Install the SQL Server Connector

Download the SQL Server Connector from the [Microsoft Download Center](https://go.microsoft.com/fwlink/p/?LinkId=521700). The download should be done by the administrator of the [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] computer.

> [!NOTE]  
>
> - SQL Server Connector versions 1.0.0.440 and older have been replaced and are no longer supported in production environments and using the instructions on the [SQL Server Connector Maintenance & Troubleshooting](sql-server-connector-maintenance-troubleshooting.md) page under [Upgrade of SQL Server Connector](sql-server-connector-maintenance-troubleshooting.md#upgrade-of--connector).
> - Starting with version 1.0.3.0, the SQL Server Connector reports relevant error messages to the Windows event logs for troubleshooting.
> - Starting with version 1.0.4.0, there is support for private Azure clouds, including Azure operated by 21Vianet, Azure Germany, and Azure Government.
> - There is a breaking change in version 1.0.5.0 in terms of the thumbprint algorithm. You might experience database restore failures after upgrading to 1.0.5.0. For more information, see [KB article 447099](/troubleshoot/sql/database-engine/backup-restore/error-33111-restore-issues-sql-connector).
> - Starting with version 1.0.5.0 (TimeStamp: September 2020), the SQL Server Connector supports filtering messages and network request retry logic.
> - Starting with updated version 1.0.5.0 (TimeStamp: November 2020), the SQL Server Connector supports RSA 2048, RSA 3072, RSA-HSM 2048 and RSA-HSM 3072 keys.
> - Starting with updated version 1.0.5.0 (TimeStamp: November 2020), you can refer to a specific key version in the Azure Key Vault.

:::image type="content" source="media/setup-steps-for-extensible-key-management-using-the-azure-key-vault/ekm-connector-install.png" alt-text="Screenshot of the SQL Server Connector installation wizard.":::

By default, the Connector is installed at `C:\Program Files\SQL Server Connector for Microsoft Azure Key Vault`. This location can be changed during setup. If you do change it, adjust the scripts in the next section.

There's no interface for the Connector, but if it's installed successfully, the `Microsoft.AzureKeyVaultService.EKM.dll` is installed on the machine. This assembly is the cryptographic EKM provider DLL that needs to be registered with [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] by using the `CREATE CRYPTOGRAPHIC PROVIDER` statement.

The SQL Server Connector installation also allows you to optionally download sample scripts for SQL Server encryption.

To view error code explanations, configuration settings, or maintenance tasks for the SQL Server Connector, see:

- [A. Maintenance Instructions for the SQL Server Connector](sql-server-connector-maintenance-troubleshooting.md#AppendixA)
- [C. Error Code Explanations for the SQL Server Connector](sql-server-connector-maintenance-troubleshooting.md#AppendixC)

## Step 4: Add registry key to support EKM provider

> [!WARNING]  
> Modifying the registry should be performed by users that know exactly what they are doing. Serious problems might occur if you modify the registry incorrectly. For added protection, back up the registry before you modify it. You can restore the registry if a problem occurs.

1. Make sure that SQL Server is installed and running.
1. Run **regedit** to open the Registry Editor.
1. Create a `SQL Server Cryptographic Provider` registry key on `HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft`. The full path is `HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\SQL Server Cryptographic Provider`.
1. Right-click the `SQL Server Cryptographic Provider` registry key, and then select **Permissions**.

1. Give **Full Control** permissions on the `SQL Server Cryptographic Provider` registry key to the user account running the SQL Server service.

   :::image type="content" source="media/setup-steps-for-extensible-key-management-using-the-azure-key-vault/ekm-part-4-registry-permissions.png" alt-text="Screenshot of the EKM registry key in Registry Editor.":::

1. Select **Apply** and then **OK**.
1. Close Registry Editor and restart the SQL Server service.

   > [!NOTE]  
   > If you use TDE with EKM or Azure Key Vault on a failover cluster instance, you must complete an additional step to add `HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\SQL Server Cryptographic Provider` to the Cluster Registry Checkpoint routine, so the registry can sync across the nodes. Syncing facilitates database recovery after failover and key rotation.
   >
   > To add the registry key to the Cluster Registry Checkpoint routine, in PowerShell, run the following command:
   >
   > `Add-ClusterCheckpoint -RegistryCheckpoint "SOFTWARE\Microsoft\SQL Server Cryptographic Provider" -Resourcename "SQL Server"`

## Step 5: Configure SQL Server

For a note about the minimum permission levels needed for each action in this section, see [B. Frequently Asked Questions](sql-server-connector-maintenance-troubleshooting.md#AppendixB).

### Configure the `master` database

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

   A credential must be added to each login that will perform encryption by using a key from the key vault. This might include:

   - A [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] administrator login that uses the key vault to set up and manage [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] encryption scenarios.

   - Other [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] logins that might enable TDE or other [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] encryption features.

   There's one-to-one mapping between credentials and logins. That is, each login must have a unique credential.

   Modify this [!INCLUDE [tsql](../../../includes/tsql-md.md)] script in the following ways:

   - Edit the `IDENTITY` argument (`DocsSampleEKMKeyVault`) to point to your Azure Key Vault.
     - If you're using *global Azure*, replace the `IDENTITY` argument with the name of your Azure Key Vault from [Step 2: Create a key vault](#step-2-create-a-key-vault).
     - If you're using a *private Azure cloud* (for example, Azure Government, Microsoft Azure operated by 21Vianet, or Azure Germany), replace the `IDENTITY` argument with the Vault URI that's returned in step 3 of the [Create a key vault and key by using PowerShell](#create-a-key-vault-and-key-by-using-powershell) section. Don't include `https://` in the key vault URI.
   - Replace the first part of the `SECRET` argument with the Microsoft Entra Client ID from [Step 1: Set up a Microsoft Entra service principal](#step-1-set-up-an-azure-ad-service-principal). In this example, the **Client ID** is `d956f6b9xxxxxxx`.

     > [!IMPORTANT]  
     > Be sure to remove the hyphens from the App (Client) ID.

   - Complete the second part of the `SECRET` argument with **Client Secret** from [Step 1: Set up a Microsoft Entra service principal](#step-1-set-up-an-azure-ad-service-principal). In this example, the Client Secret is `yrA8X~PldtMCvUZPxxxxxxxx`. The final string for the `SECRET` argument will be a long sequence of letters and numbers, without hyphens (except for the Client Secret section, in case the Client Secret contains any hyphens).

     ```sql
     USE master;
     CREATE CREDENTIAL sysadmin_ekm_cred
         WITH IDENTITY = 'DocsSampleEKMKeyVault',                            -- for public Azure
         -- WITH IDENTITY = 'DocsSampleEKMKeyVault.vault.usgovcloudapi.net', -- for Azure Government
         -- WITH IDENTITY = 'DocsSampleEKMKeyVault.vault.azure.cn',          -- for Microsoft Azure operated by 21Vianet
         -- WITH IDENTITY = 'DocsSampleEKMKeyVault.vault.microsoftazure.de', -- for Azure Germany
         -- WITH IDENTITY = '<name of Managed HSM>.managedhsm.azure.net',    -- for Managed HSM (HSM URI in the Azure portal resource)
                --<----Application (Client) ID ---><--Microsoft Entra app (Client) ID secret-->
         SECRET = 'd956f6b9xxxxxxxyrA8X~PldtMCvUZPxxxxxxxx'
     FOR CRYPTOGRAPHIC PROVIDER AzureKeyVault_EKM;

     -- Add the credential to the SQL Server administrator's domain login
     ALTER LOGIN [<domain>\<login>]
     ADD CREDENTIAL sysadmin_ekm_cred;
     ```

    For an example of using variables for the `CREATE CREDENTIAL` argument and programmatically removing the hyphens from the Client ID, see [CREATE CREDENTIAL](../../../t-sql/statements/create-credential-transact-sql.md).

1. Open your Azure Key Vault key in your SQL Server instance.

   Whether you created a new key or imported an asymmetric key, as described in [Step 2: Create a key vault](#step-2-create-a-key-vault), you need to open the key. Open it by providing your key name in the following [!INCLUDE [tsql](../../../includes/tsql-md.md)] script.

     > [!IMPORTANT]  
     > Be sure to first complete the Registry prerequisites for this step.

   - Replace `EKMSampleASYKey` with the name you'd like the key to have in [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)].
   - Replace `ContosoRSAKey0` with the name of your key in your Azure Key Vault or Managed HSM. Below is an example of a version-less key.

   ```sql
   CREATE ASYMMETRIC KEY EKMSampleASYKey
   FROM PROVIDER [AzureKeyVault_EKM]
   WITH PROVIDER_KEY_NAME = 'ContosoRSAKey0',
   CREATION_DISPOSITION = OPEN_EXISTING;
   ```

   Beginning with updated version 1.0.5.0 of the SQL Server connector, you can refer to a specific key version in the Azure Key Vault:

   ```sql
   CREATE ASYMMETRIC KEY EKMSampleASYKey
   FROM PROVIDER [AzureKeyVault_EKM]
   WITH PROVIDER_KEY_NAME = 'ContosoRSAKey0/1a4d3b9b393c4678831ccc60def75379',
   CREATION_DISPOSITION = OPEN_EXISTING;
   ```

   In the preceding example script, `1a4d3b9b393c4678831ccc60def75379` represents the specific version of the key that will be used. If you use this script, it doesn't matter if you update the key with a new version. The key version (for example) `1a4d3b9b393c4678831ccc60def75379` will always be used for database operations.

1. Create a new login by using the asymmetric key in [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] that you created in the preceding step.

    ```sql
   --Create a Login that will associate the asymmetric key to this login
   CREATE LOGIN TDE_Login
   FROM ASYMMETRIC KEY EKMSampleASYKey;
   ```

1. Create a new login from the asymmetric key in SQL Server. Drop the credential mapping from [Step 5: Configure SQL Server](#step-5-configure-sql-server) so that the credentials can be mapped to the new login.

   ```sql
   --Now drop the credential mapping from the original association
   ALTER LOGIN [<domain>\<login>]
   DROP CREDENTIAL sysadmin_ekm_cred;
   ```

1. Alter the new login, and map the EKM credentials to the new login.

   ```sql
   --Now add the credential mapping to the new Login
   ALTER LOGIN TDE_Login
   ADD CREDENTIAL sysadmin_ekm_cred;
   ```

### Configure the user database to be encrypted

1. Create a test database that will be encrypted with the Azure Key Vault key.

   ```sql
   --Create a test database that will be encrypted with the Azure Key Vault key
   CREATE DATABASE TestTDE;
   ```

1. Create a database encryption key by using the `ASYMMETRIC KEY` (`EKMSampleASYKey`).

   ```sql
   USE <DB Name>;
   --Create an ENCRYPTION KEY using the ASYMMETRIC KEY (EKMSampleASYKey)
   CREATE DATABASE ENCRYPTION KEY
   WITH ALGORITHM = AES_256
   ENCRYPTION BY SERVER ASYMMETRIC KEY EKMSampleASYKey;
   ```

1. Encrypt the test database. Enable TDE by setting `ENCRYPTION ON`.

   ```sql
   --Enable TDE by setting ENCRYPTION ON
   ALTER DATABASE TestTDE
   SET ENCRYPTION ON;
   ```

### Registry details

1. Execute the following [!INCLUDE [tsql](../../../includes/tsql-md.md)] query in the `master` database to show the asymmetric key used.

   ```sql
   SELECT name, algorithm_desc, thumbprint FROM sys.asymmetric_keys;
   ```

   The statement returns:

   ```output
   name            algorithm_desc    thumbprint
   EKMSampleASYKey RSA_2048          <key thumbprint>
   ```

1. In the user database (`TestTDE`), execute the following [!INCLUDE [tsql](../../../includes/tsql-md.md)] query to show the encryption key used.

   ```sql
   SELECT encryptor_type, encryption_state_desc, encryptor_thumbprint
   FROM sys.dm_database_encryption_keys
   WHERE database_id = DB_ID('TestTDE');
   ```

   The statement returns:

   ```output
   encryptor_type encryption_state_desc encryptor_thumbprint
   ASYMMETRIC KEY ENCRYPTED             <key thumbprint>
   ```

### Clean up

1. Clean up the test objects. Delete all the objects that were created in this test script.

   ```sql
   -- CLEAN UP
   USE master;
   GO
   ALTER DATABASE [TestTDE] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
   DROP DATABASE [TestTDE];
   GO

   ALTER LOGIN [TDE_Login] DROP CREDENTIAL [sysadmin_ekm_cred];
   DROP LOGIN [TDE_Login];
   GO

   DROP CREDENTIAL [sysadmin_ekm_cred];
   GO

   USE master;
   GO
   DROP ASYMMETRIC KEY [EKMSampleASYKey];
   DROP CRYPTOGRAPHIC PROVIDER [AzureKeyVault_EKM];
   GO
   ```

   For sample scripts, see the blog at [SQL Server Transparent Data Encryption and Extensible Key Management with Azure Key Vault](https://techcommunity.microsoft.com/t5/sql-server-blog/intro-sql-server-transparent-data-encryption-and-extensible-key/ba-p/1427549).

1. The `SQL Server Cryptographic Provider` registry key isn't cleaned up automatically after a key or all EKM keys are deleted. It must be cleaned up manually. Cleaning the registry key should be done with extreme caution, since cleaning the registry prematurely can break the EKM functionality. To clean up the registry key, delete the `SQL Server Cryptographic Provider` registry key on `HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft`.

### Troubleshooting

If the registry key `HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\SQL Server Cryptographic Provider` isn't created or the required permissions aren't granted, the following DDL statement will fail:

```sql
CREATE ASYMMETRIC KEY EKMSampleASYKey
FROM PROVIDER [AzureKeyVault_EKM]
WITH PROVIDER_KEY_NAME = 'ContosoRSAKey0',
CREATION_DISPOSITION = OPEN_EXISTING;
```

```output
Msg 33049, Level 16, State 2, Line 65
Key with name 'ContosoRSAKey0' does not exist in the provider or access is denied. Provider error code: 2058.  (Provider Error - No explanation is available, consult EKM Provider for details)
```

## Client secrets that are about to expire

If the credential has a client secret that is about to expire, a new secret can be assigned to the credential.

1. Update the secret originally created in [Step 1: Set up a Microsoft Entra service principal](#step-1-set-up-an-azure-ad-service-principal).

1. Alter the credential using the same identity and new secret using the following code. Replace `<New Secret>` with your new secret:

   ```sql
   ALTER CREDENTIAL sysadmin_ekm_cred
   WITH IDENTITY = 'DocsSampleEKMKeyVault',
   SECRET = '<New Secret>';
   ```

1. Restart the SQL Server service.

> [!NOTE]  
> If you are using EKM in an availability group (AG), you will need to alter the credential and restart the SQL Server service on all nodes of the AG.

## Rotate asymmetric key with a new AKV key or a new AKV key version

> [!NOTE]  
>
> - When manually rotating an AKV key, SQL Server supports both AKV version-less key or versioned key and there's no need to use a different AKV key.
> - The original AKV key can be rotated creating a new version that can replace the previous key created in SQL Server.
> - For manual key rotation, a new SQL Server asymmetric key must be created referring to the version-less key or versioned key that was rotated in AKV. For the new SQL Server asymmetric key, the version-less AKV key will automatically be chosen using the highest key version in AKV. For the versioned key, you must indicate the highest version in AKV using the syntax `WITH PROVIDER_KEY_NAME = <key_name>/<version>`. You can alter the database encryption key to re-encrypt with the new asymmetric key. The same key name (versioned or version-less) can be used with AKV rotation policy. For versioned key, the current version must be added. For version-less key, use the same key name.

SQL Server doesn't have a mechanism to automatically rotate the asymmetric key used for TDE. The steps to rotate an asymmetric key manually are as follows.

1. The credential used in our initial setup (`sysadmin_ekm_cred`) can also be reused for the key rotation. Optionally, create a new credential for the new asymmetric key.

   ```sql
   CREATE CREDENTIAL <new_credential_name>
       WITH IDENTITY = <key vault>,
       SECRET = 'existing/new secret'
       FOR CRYPTOGRAPHIC PROVIDER AzureKeyVault_EKM;
   ```

1. Add the credential to the principal:

   ```sql
   ALTER LOGIN [domain\userName];
   ADD CREDENTIAL <new_credential_name>;
   ```

1. Create the new asymmetric key based on the new key (after rotating the key). The new key could be a version-less key (`ContosoRSAKey0` in our example) or a versioned key (`ContosoRSAKey0/1a4d3b9b393c4678831ccc60def75379` where `1a4d3b9b393c4678831ccc60def75379` is the version of the updated key in AKV):

   ```sql
   CREATE ASYMMETRIC KEY <new_ekm_key_name>
    FROM PROVIDER [AzureKeyVault_EKM]
    WITH PROVIDER_KEY_NAME = <new_key_from_key_vault>,
    CREATION_DISPOSITION = OPEN_EXISTING;
   ```

1. Create a new login from the new asymmetric key:

   ```sql
   CREATE LOGIN <new_login_name>
   FROM ASYMMETRIC KEY <new_ekm_key_name>;
   ```

1. Drop the credential from the principal:

   ```sql
   ALTER LOGIN [domain\username]
   DROP CREDENTIAL <new_credential_name>;
   ```

1. Map AKV credential to the new login:

   ```sql
   ALTER LOGIN <new_login_name>;
   ADD CREDENTIAL <new_credential_name>;
   ```

1. Alter the database encryption key (DEK) to re-encrypt with the new asymmetric key:

   ```sql
   USE [databaseName];
   GO
   ALTER DATABASE ENCRYPTION KEY ENCRYPTION BY SERVER ASYMMETRIC KEY <new_ekm_key_name>;
   ```

1. You can verify the new asymmetric key and the encryption key used in the database:

   ```sql
   SELECT encryptor_type, encryption_state_desc, encryptor_thumbprint
   FROM sys.dm_database_encryption_keys
   WHERE database_id = DB_ID('databaseName');
   ```

   This thumbprint should match the registry key under the path `HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\SQL Server Cryptographic Provider\Azure Key Vault\<key_vault_url>\<thumbprint>` and give you the `KeyUri` for your rotated key.

> [!IMPORTANT]  
> Rotating the logical TDE protector for a server means switching to a new asymmetric key or certificate that protects the database encryption key (DEK). Key rotation is an online operation and should only take a few seconds to complete, because this only decrypts and re-encrypts the DEK and not the entire database.
>
> Don't delete previous versions of the key after rotation. When keys are rotated, some data is still encrypted with the previous keys, such as older database backups, backed-up log files, virtual log files (VLF), and transaction log files. Previous keys might also be required for a database recovery or a database restore.

## Related content

- [Use SQL Server Connector with SQL Encryption Features](use-sql-server-connector-with-sql-encryption-features.md)
- [Extensible Key Management Using Azure Key Vault (SQL Server)](extensible-key-management-using-azure-key-vault-sql-server.md)
- [SQL Server Connector Maintenance & Troubleshooting](sql-server-connector-maintenance-troubleshooting.md)
