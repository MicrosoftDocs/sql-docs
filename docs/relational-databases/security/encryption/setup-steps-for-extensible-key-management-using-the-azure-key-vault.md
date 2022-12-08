---
title: "Set up Transparent Data Encryption (TDE) Extensible Key Management with Azure Key Vault"
description: Install and configure the SQL Server Connector for Azure Key Vault.
author: Rupp29
ms.author: arupp
ms.reviewer: vanto, randolphwest
ms.date: 10/05/2022
ms.service: sql
ms.subservice: security
ms.topic: conceptual
ms.custom: seo-lt-2019
helpviewer_keywords:
  - "Extensible Key Management"
  - "EKM, with key vault setup"
  - "SQL Server Connector, setup"
  - "SQL Server Connector"
  - "TDE, AKV, EKM"
---
# Set up SQL Server TDE Extensible Key Management by using Azure Key Vault

[!INCLUDE [sql-windows-only](../../../includes/applies-to-version/sql-windows-only.md)]

In this article, you install and configure the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Connector for Azure Key Vault.

> [!NOTE]
> Extensible Key Management is [not supported](../../../linux/sql-server-linux-editions-and-components-2019.md#Unsupported) for SQL Server on Linux.

## Prerequisites

Before you begin using Azure Key Vault with your SQL Server instance, be sure that you've met the following prerequisites:

- You must have an Azure subscription.

- Install [Azure PowerShell version 5.2.0 or later](/powershell/azure/).

- Create an Azure Active Directory (Azure AD) instance.

- Familiarize yourself with the principles of Extensible Key Management (EKM) storage with Azure Key Vault by reviewing [EKM with Azure Key Vault (SQL Server)](../../../relational-databases/security/encryption/extensible-key-management-using-azure-key-vault-sql-server.md).

- Install the version of Visual Studio C++ Redistributable that's based on the version of SQL Server that you're running:

  SQL Server version  | Visual Studio C++ Redistributable version
  ---------|---------
  2008, 2008 R2, 2012, 2014 | [Visual C++ Redistributable packages for Visual Studio 2013](https://www.microsoft.com/download/details.aspx?id=40784)
  2016, 2017, 2019, 2022 | [Visual C++ Redistributable for Visual Studio 2015](https://www.microsoft.com/download/details.aspx?id=48145)

- Familiarize yourself with [Access Azure Key Vault behind a firewall](/azure/key-vault/general/access-behind-firewall) if you plan to use the SQL Server Connector for Azure Key Vault behind a firewall or with a proxy server.

## Step 1: Set up an Azure AD service principal

To grant your SQL Server instance access permissions to your Azure key vault, you need a service principal account in Azure AD.

1. Sign in to the [Azure portal](https://ms.portal.azure.com/), and do either of the following:

    - Select the **Azure Active Directory** button.

      :::image type="content" source="../../../relational-databases/security/encryption/media/ekm/ekm-part1-login-portal.png" alt-text="Screenshot of the Azure services pane.":::

    - Select **More services** and then, in the **All services** box, type **Azure Active Directory**.

      :::image type="content" source="../../../relational-databases/security/encryption/media/ekm/ekm-part1-select-aad.png" alt-text="Screenshot of the All Azure services pane.":::

1. Register an application with Azure Active Directory by doing the following. (For detailed step-by-step instructions, see the "Get an identity for the application" section of the [Azure Key Vault blog post](/archive/blogs/kv/azure-key-vault-step-by-step).)

   1. On the **Azure Active Directory Overview** pane, select **App registrations**.

      :::image type="content" source="../../../relational-databases/security/encryption/media/ekm/ekm-part1-aad-app-register.png" alt-text="Screenshot of the Azure Active Directory Overview pane.":::

   1. On the **App registrations** pane, select **New registration**.

      :::image type="content" source="../../../relational-databases/security/encryption/media/ekm/ekm-part1-aad-new-registration.png" alt-text="Screenshot of the App registrations pane.":::

   1. On the **Register an application** pane, enter the user-facing name for the app, and then select **Register**.

      :::image type="content" source="../../../relational-databases/security/encryption/media/ekm/ekm-part1-aad-register.png" alt-text="Screenshot of the Register an application pane.":::

   1. In the left pane, select **Certificates & secrets**, and then select **New client secret**.

      :::image type="content" source="../../../relational-databases/security/encryption/media/ekm/ekm-part1-aad-certs-secrets.png" alt-text="Screenshot of the Certificates & secrets pane.":::

   1. Under **Add a client secret**, enter a description and an appropriate expiration, and then select **Add**.

      :::image type="content" source="../../../relational-databases/security/encryption/media/ekm/ekm-part1-aad-add-secret.png" alt-text="Screenshot of the Add a client secret section.":::

   1. On the **Certificates & secrets** pane, under **"Value"**, select the **Copy** button next to the value of the client secret to be used to create an asymmetric key in SQL Server.

      :::image type="content" source="../../../relational-databases/security/encryption/media/ekm/ekm-part1-aad-new-secret.png" alt-text="Screenshot of the secret value.":::

   1. In the left pane, select **Overview** and then, in the **Application (client) ID** box, copy the value to be used to create an asymmetric key in SQL Server.

      :::image type="content" source="../../../relational-databases/security/encryption/media/ekm/ekm-part1-aad-appid.png" alt-text="Screenshot of the Application (client) ID value on the Overview pane.":::

## Step 2: Create a key vault

Select the method you want to use to create a key vault.

## [Azure portal](#tab/portal)

### Create a key vault by using the Azure portal

You can use the Azure portal to create the key vault and then add an Azure AD principal to it.

1. Create a resource group.

   All Azure resources that you create via the Azure portal must be contained in a resource group, which you create to house your key vault. The resource name in this example is *ContosoDevRG*. Choose your own resource group and key vault name, because all key vault names must be globally unique.

   On the **Create a resource group** pane, under **Project details**, enter the values, and then select **Review + create**.

   :::image type="content" source="../../../relational-databases/security/encryption/media/ekm/ekm-part2-create-resource-group.png" alt-text="Screenshot of the Create a resource group pane.":::

1. Create a key vault.

    On the **Create key vault** pane, select the **Basics** tab, enter the appropriate values, and then select **Review + create**.

    :::image type="content" source="../../../relational-databases/security/encryption/media/ekm/ekm-part2-create-key-vault.png" alt-text="Screenshot of the Create key vault pane.":::

1. On the **Access policies** pane, select **Add Access Policy**.

    :::image type="content" source="../../../relational-databases/security/encryption/media/ekm/ekm-part2-add-access-policy.png" alt-text="Screenshot of the Add Access Policy link on the Access policies pane.":::

1. On the **Add access policy** pane, do the following:

   1. In the **Configure from template (optional)** drop-down list, select **Key Management**.

   1. In the left pane, select the **Key permissions** tab, and then verify the **Get**, **List**, **Unwrap Key**, and **Wrap Key** check boxes are selected.

   1. Select **Add**.

      :::image type="content" source="../../../relational-databases/security/encryption/media/ekm/ekm-part2-access-policy-permission.png" alt-text="Screenshot of the Add access policy pane.":::

1. In the left pane, select the **Select principal** tab, and then do the following:

   1. In the **Principal** pane, under **Select**, start typing the name of your Azure AD application and then, in the results list, select the application you want to add.

      :::image type="content" source="../../../relational-databases/security/encryption/media/ekm/ekm-part2-select-principal.png" alt-text="Screenshot of application search box on the Principal pane.":::

   1. Select the **Select** button to add the principal to your key vault.

      :::image type="content" source="../../../relational-databases/security/encryption/media/ekm/ekm-part2-save-principal.png" alt-text="Screenshot of the Select button on the Principal pane.":::

   1. At the lower left, select **Add** to save your changes.

      :::image type="content" source="../../../relational-databases/security/encryption/media/ekm/ekm-part2-select-principal-new.png" alt-text="Screenshot of the Add button on the Add access policy pane.":::

1. On the **Key Vault** pane, select **Keys** and enter a key vault name. Use key type **RSA** and RSA Key Size **2048**. Set activation and expiration dates as appropriate and set **Enabled?** as **Yes**.

   :::image type="content" source="../../../relational-databases/security/encryption/media/ekm/ekm-part2-add-key-vault-key.png" alt-text="Screenshot of the Create Key pane.":::

1. On the **Access policies** pane, select **Save**.

   :::image type="content" source="../../../relational-databases/security/encryption/media/ekm/ekm-part2-save-access-policy.png" alt-text="Screenshot of the Save button on the Add access policy pane.":::

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
> The subscription where the key vault is created must be in the same default Azure AD instance where the Azure AD service principal was created. If you want to use an Azure AD instance other than your default instance for creating a service principal for the SQL Server Connector, you must change the default Azure AD instance in your Azure account before you create your key vault. To learn how to change the default Azure AD instance to the one you want to use, see the "Frequently asked questions" section of [SQL Server Connector maintenance & troubleshooting](../../../relational-databases/security/encryption/sql-server-connector-maintenance-troubleshooting.md#AppendixB).

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

   All Azure resources that you create via PowerShell must be contained in resource groups, which you create to house your key vault. The resource name in this example is *ContosoDevRG*. Choose your own resource group and key vault name, because all key vault names must be globally unique.

   ```powershell
   New-AzResourceGroup -Name ContosoDevRG -Location 'East Asia'
   ```

   The statement returns:

   ```console
   ResourceGroupName: ContosoDevRG
   Location         : eastasia
   ProvisioningState: Succeeded
   Tags             :
   ResourceId       : /subscriptions/<subscription_id>/
                       resourceGroups/ContosoDevRG
    ```

   > [!NOTE]  
   > For the `-Location` parameter, use the command `Get-AzureLocation` to identify how to specify a location other than the one in this example. If you need more information, type **Get-Help Get-AzureLocation**.

1. Create a key vault.

   The `New-AzKeyVault` cmdlet requires a resource group name, a key vault name, and a geographic location. For example, for a key vault named `ContosoEKMKeyVault`, run:

   ```powershell
   New-AzKeyVault -VaultName 'ContosoEKMKeyVault' `
      -ResourceGroupName 'ContosoDevRG' -Location 'East Asia'
   ```

   Record the name of your key vault.

   The statement returns:

   ```console
   Vault Name                       : ContosoEKMKeyVault
   Resource Group Name              : ContosoDevRG
   Location                         : East Asia
   ResourceId                       : /subscriptions/<subscription_id>/
                                       resourceGroups/ContosoDevRG/providers/
                                       Microsoft/KeyVault/vaults/ContosoEKMKeyVault
   Vault URI: https://ContosoEKMKeyVault.vault.azure.net
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

1. Grant permissions for the Azure AD service principal to access the Azure key vault.

   You can authorize other users and applications to use your key vault.  For our example, let's use the service principal that you created in [Step 1: Set up an Azure AD service principal](#step-1-set-up-an-azure-ad-service-principal) to authorize the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance.

   > [!IMPORTANT]  
   > The Azure AD service principal must have at least the *get*, *list*,*wrapKey*, and *unwrapKey* permissions for the key vault.

   As shown in the following command, you use the **App (Client) ID** from [Step 1: Set up an Azure AD service principal](#step-1-set-up-an-azure-ad-service-principal) for the `ServicePrincipalName` parameter. `Set-AzKeyVaultAccessPolicy` runs silently with no output if it runs successfully.

   ```powershell
   Set-AzKeyVaultAccessPolicy -VaultName 'ContosoEKMKeyVault' `
     -ServicePrincipalName 9A57CBC5-4C4C-40E2-B517-EA677 `
     -PermissionsToKeys get, list, wrapKey, unwrapKey
   ```

   Call the `Get-AzKeyVault` cmdlet to confirm the permissions. In the statement output under `Access Policies`, you should see your Azure AD application name listed as another tenant that has access to this key vault.

1. Generate an asymmetric key in the key vault. You can do so in either of two ways: import an existing key or create a new key.

   > [!NOTE]  
   > SQL Server supports only 2048-bit & 3072-bit RSA keys and 2048-bit & 3072-bit RSA-HSM keys.

### Best practices

To ensure quick key recovery and be able to access your data outside of Azure, we recommend the following best practices:

- Create your encryption key locally on a local hardware security module (HSM) device. Be sure to use an asymmetric RSA 2048 or 3072 key so that it's supported by SQL Server.
- Import the encryption key to your Azure key vault. This process is described in the next sections.
- Before you use the key in your Azure key vault for the first time, do an Azure key vault key backup using the `Backup-AzureKeyVaultKey` PowerShell cmdlet.
- Whenever you make any changes to the key (for example, adding ACLs, tags, or key attributes), be sure to do another Azure key vault key backup.

  > [!NOTE]  
  > Backing up a key is an Azure Key Vault key operation which returns a file that can be saved anywhere.

### Types of keys

You can generate four types of keys in an Azure key vault that will work with SQL Server. Asymmetric 2048-bit & 3072-bit RSA keys and  2048-bit & 3072-bit RSA-HSM keys.

- **Software-protected**: Processed in software and encrypted at rest. Operations on software-protected keys occur on Azure virtual machines. We recommend this type for keys that aren't used in a production deployment.

- **HSM-protected**: Created and protected by a hardware security module for additional security. The cost is about USD 1 per key version.

  > [!IMPORTANT]  
  > For the SQL Server Connector, use only the characters a-z, A-Z, 0-9, and hyphens (-), with a 26-character limit.
  > Different key versions under the same key name in an Azure key vault don't work with the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Connector. To rotate an Azure key vault key that's being used by [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], see the Key Rollover steps in the "A. Maintenance Instructions for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Connector" section of [SQL Server Connector Maintenance & Troubleshooting](../../../relational-databases/security/encryption/sql-server-connector-maintenance-troubleshooting.md).

### Import an existing key

If you have an existing 2048-bit RSA software-protected key, you can upload the key to your Azure key vault. For example, if you have a PFX file saved to your `C:\` drive in a file named `softkey.pfx` that you want to upload to the Azure key vault, run the following command to set the variable `securepfxpwd` for a password of `12987553` for the PFX file:

```powershell
$securepfxpwd = ConvertTo-SecureString -String '12987553' `
  -AsPlainText -Force
```

Then you can run the following command to import the key from the PFX file, which protects the key by hardware (recommended) in the Key Vault service:

```powershell
    Add-AzureKeyVaultKey -VaultName 'ContosoKeyVault' `
      -Name 'ContosoFirstKey' -KeyFilePath 'C:\softkey.pfx' `
      -KeyFilePassword $securepfxpwd $securepfxpwd  -Destination 'HSM'
```

> [!CAUTION]
> We highly recommend importing the asymmetric key for production scenarios, because doing so allows the administrator to escrow the key in a key escrow system. If the asymmetric key is created in the vault, it can't be escrowed, because the private key can never leave the vault. Keys that are used to protect critical data should be escrowed. The loss of an asymmetric key will result in permanent data loss.

### Create a new key

Alternatively, you can create a new encryption key directly in your Azure key vault and make it either software-protected or HSM-protected.  In this example, let's create a software-protected key by using the `Add-AzureKeyVaultKey` cmdlet:

```powershell
Add-AzureKeyVaultKey -VaultName 'ContosoEKMKeyVault' `
  -Name 'ContosoRSAKey0' -Destination 'Software'
```

The statement returns:

```console
Attributes : Microsoft.Azure.Commands.KeyVault.Models.KeyAttributes
Key        :  {"kid":"https:contosoekmkeyvault.azure.net/keys/
                ContosoRSAKey0/<guid>","dty":"RSA:,"key_ops": ...
VaultName  : contosodevkeyvault
Name       : contosoRSAKey0
Version    : <guid>
Id         : https://contosoekmkeyvault.vault.azure.net:443/
              keys/ContosoRSAKey0/<guid>
```

> [!IMPORTANT]  
> The key vault supports multiple versions of the same named key, but keys to be used by the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Connector should not be versioned or rolled. If the administrator wants to roll the key that's used for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] encryption, a new key with a different name should be created in the key vault and used to encrypt the DEK.

---

## Step 3: Install the SQL Server Connector

Download the SQL Server Connector from the [Microsoft Download Center](https://go.microsoft.com/fwlink/p/?LinkId=521700). The download should be done by the administrator of the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] computer.

> [!NOTE]  
>
> - SQL Server Connector versions 1.0.0.440 and older have been replaced and are no longer supported in production environments and using the instructions on the [SQL Server Connector Maintenance & Troubleshooting](../../../relational-databases/security/encryption/sql-server-connector-maintenance-troubleshooting.md) page under [Upgrade of SQL Server Connector](sql-server-connector-maintenance-troubleshooting.md#upgrade-of--connector).
> - Starting with version 1.0.3.0, the SQL Server Connector reports relevant error messages to the Windows event logs for troubleshooting.
> - Starting with version 1.0.4.0, there is support for private Azure clouds, including Azure China, Azure Germany, and Azure Government.
> - There is a breaking change in version 1.0.5.0 in terms of the thumbprint algorithm. You may experience database restore failures after upgrading to 1.0.5.0. For more information, see [KB article 447099](https://support.microsoft.com/help/4470999/db-backup-problems-to-sql-server-connector-for-azure-1-0-5-0).
> - Starting with version 1.0.5.0 (TimeStamp: September 2020), the SQL Server Connector supports filtering messages and network request retry logic.
> - **Starting with updated version 1.0.5.0 (TimeStamp: November 2020), the SQL Server Connector supports RSA 2048, RSA 3072, RSA-HSM 2048 and RSA-HSM 3072 keys.**

:::image type="content" source="../../../relational-databases/security/encryption/media/ekm/ekm-connector-install.png" alt-text="Screenshot of the SQL Server Connector installation wizard.":::

By default, the Connector is installed at `C:\Program Files\SQL Server Connector for Microsoft Azure Key Vault`. This location can be changed during setup. If you do change it, adjust the scripts in the next section.

There's no interface for the Connector, but if it's installed successfully, the `Microsoft.AzureKeyVaultService.EKM.dll` is installed on the machine. This assembly is the cryptographic EKM provider DLL that needs to be registered with [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] by using the `CREATE CRYPTOGRAPHIC PROVIDER` statement.

The SQL Server Connector installation also allows you to optionally download sample scripts for SQL Server encryption.

To view error code explanations, configuration settings, or maintenance tasks for the SQL Server Connector, see:

- [A. Maintenance Instructions for the SQL Server Connector](../../../relational-databases/security/encryption/sql-server-connector-maintenance-troubleshooting.md#AppendixA)  
- [C. Error Code Explanations for the SQL Server Connector](../../../relational-databases/security/encryption/sql-server-connector-maintenance-troubleshooting.md#AppendixC)

## Step 4: Configure SQL Server

For a note about the minimum permission levels needed for each action in this section, see [B. Frequently Asked Questions](../../../relational-databases/security/encryption/sql-server-connector-maintenance-troubleshooting.md#AppendixB).

1. Run **sqlcmd** or open [!INCLUDE [ssmanstudiofull-md](../../../includes/ssmanstudiofull-md.md)].

1. Configure [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] to use EKM by running the following [!INCLUDE[tsql](../../../includes/tsql-md.md)] script:

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

1. Register the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Connector as an EKM provider with [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].

   Create a cryptographic provider by using the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Connector, which is an EKM provider for the Azure key vault.
   In this example, the provider name is `AzureKeyVault_EKM`.

   ```sql
   CREATE CRYPTOGRAPHIC PROVIDER AzureKeyVault_EKM
   FROM FILE = 'C:\Program Files\SQL Server Connector for Microsoft Azure Key Vault\Microsoft.AzureKeyVaultService.EKM.dll';
   GO
   ```

   > [!NOTE]  
   > The file path length can't exceed 256 characters.

1. Set up a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] credential for a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] login to use the key vault.

   A credential must be added to each login that will perform encryption by using a key from the key vault. This might include:

   - A [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] administrator login that uses the key vault to set up and manage [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] encryption scenarios.

   - Other [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] logins that might enable TDE or other [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] encryption features.

   There is one-to-one mapping between credentials and logins. That is, each login must have a unique credential.

   Modify this [!INCLUDE[tsql](../../../includes/tsql-md.md)] script in the following ways:

   - Edit the `IDENTITY` argument (`ContosoEKMKeyVault`) to point to your Azure key vault.
     - If you're using *global Azure*, replace the `IDENTITY` argument with the name of your Azure key vault from [Step 2: Create a key vault](#step-2-create-a-key-vault).
     - If you're using a *private Azure cloud* (for example, Azure Government, Azure China 21Vianet, or Azure Germany), replace the `IDENTITY` argument with the Vault URI that's returned in step 3 of the [Create a key vault and key by using PowerShell](#create-a-key-vault-and-key-by-using-powershell) section. Don't include "https://" in the Vault URI.
   - Replace the first part of the `SECRET` argument with the Azure Active Directory Client ID from [Step 1: Set up an Azure AD service principal](#step-1-set-up-an-azure-ad-service-principal). In this example, the **Client ID** is `9A57CBC54C4C40E2B517EA677E0EFA00`.

     > [!IMPORTANT]  
     > Be sure to remove the hyphens from the App (Client) ID.

   - Complete the second part of the `SECRET` argument with **Client Secret** from [Step 1: Set up an Azure AD service principal](#step-1-set-up-an-azure-ad-service-principal).  In this example, the Client Secret is `08:k?[:XEZFxcwIPvVVZhTjHWXm7w1?m`. The final string for the `SECRET` argument will be a long sequence of letters and numbers, without hyphens.

     ```sql
     USE master;
     CREATE CREDENTIAL sysadmin_ekm_cred
         WITH IDENTITY = 'ContosoEKMKeyVault',                            -- for public Azure
         -- WITH IDENTITY = 'ContosoEKMKeyVault.vault.usgovcloudapi.net', -- for Azure Government
         -- WITH IDENTITY = 'ContosoEKMKeyVault.vault.azure.cn',          -- for Azure China 21Vianet
         -- WITH IDENTITY = 'ContosoEKMKeyVault.vault.microsoftazure.de', -- for Azure Germany
                --<----Application (Client) ID ---><--Azure AD app (Client) ID secret-->
         SECRET = '9A57CBC54C4C40E2B517EA677E0EFA0008:k?[:XEZFxcwIPvVVZhTjHWXm7w1?m'
     FOR CRYPTOGRAPHIC PROVIDER AzureKeyVault_EKM;
  
     -- Add the credential to the SQL Server administrator's domain login
     ALTER LOGIN [<domain>\<login>]
     ADD CREDENTIAL sysadmin_ekm_cred;
     ```

    For an example of using variables for the `CREATE CREDENTIAL` argument and programmatically removing the hyphens from the Client ID, see [CREATE CREDENTIAL (Transact-SQL)](../../../t-sql/statements/create-credential-transact-sql.md).

1. Open your Azure key vault key in your SQL Server instance.

   Whether you created a new key or imported an asymmetric key, as described in [Step 2: Create a key vault](#step-2-create-a-key-vault), you will need to open the key. Open it by providing your key name in the following [!INCLUDE[tsql](../../../includes/tsql-md.md)] script.

   - Replace `EKMSampleASYKey` with the name you'd like the key to have in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
   - Replace `ContosoRSAKey0` with the name of your key in your Azure key vault.

   ```sql
   CREATE ASYMMETRIC KEY EKMSampleASYKey
   FROM PROVIDER [AzureKeyVault_EKM]
   WITH PROVIDER_KEY_NAME = 'ContosoRSAKey0',
   CREATION_DISPOSITION = OPEN_EXISTING;
   ```

   Beginning with updated version 1.0.5.0 of the SQL Server connector, you can refer to a specific key version in the Azure key vault:

   ```sql
   CREATE ASYMMETRIC KEY EKMSampleASYKey
   FROM PROVIDER [AzureKeyVault_EKM]
   WITH PROVIDER_KEY_NAME = 'ContosoRSAKey0/1a4d3b9b393c4678831ccc60def75379',
   CREATION_DISPOSITION = OPEN_EXISTING;
   ```

   In the preceding example script, `1a4d3b9b393c4678831ccc60def75379` represents the specific version of the key that will be used. If you use this script, it doesn't matter if you update the key with a new version. The key version (for example) `1a4d3b9b393c4678831ccc60def75379` will always be used for database operations. For this scenario, you must complete two prerequisites:

   1. Create a `SQL Server Cryptographic Provider` key on `HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft`.
   1. Delegate `Full Control` permissions on the `SQL Server Cryptographic Provider` key to the user account running the [!INCLUDE [ssdenoversion-md](../../../includes/ssdenoversion-md.md)] service.

      > [!NOTE]  
      > If you use TDE with EKM or Azure Key Vault on a failover cluster instance, you must complete an additional step to add `HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\SQL Server Cryptographic Provider` to the Cluster Registry Checkpoint routine, so the registry can sync across the nodes. Syncing facilitates database recovery after failover and key rotation.
      >  
      > To add the registry key to the Cluster Registry Checkpoint routine, in PowerShell, run the following command:
      >  
      > `Add-ClusterCheckpoint -RegistryCheckpoint "SOFTWARE\Microsoft\SQL Server Cryptographic Provider" -Resourcename "SQL Server"`

1. Create a new login by using the asymmetric key in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] that you created in the preceding step.

    ```sql
   --Create a Login that will associate the asymmetric key to this login
   CREATE LOGIN TDE_Login
   FROM ASYMMETRIC KEY EKMSampleASYKey;
   ```

1. Create a new login from the asymmetric key in SQL Server. Drop the credential mapping from [Step 4: Configure SQL Server](#step-4-configure-sql-server) so that the credentials can be mapped to the new login.

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

1. Create a test database that will be encrypted with the Azure key vault key.

   ```sql
   --Create a test database that will be encrypted with the Azure key vault key
   CREATE DATABASE TestTDE
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

1. Clean up the test objects. Delete all the objects that were created in this test script.

   ```sql
   -- CLEAN UP
   USE master
   ALTER DATABASE [TestTDE] SET SINGLE_USER WITH ROLLBACK IMMEDIATE
   DROP DATABASE [TestTDE]

   ALTER LOGIN [TDE_Login] DROP CREDENTIAL [sysadmin_ekm_cred]
   DROP LOGIN [TDE_Login]

   DROP CREDENTIAL [sysadmin_ekm_cred]

   USE master
   DROP ASYMMETRIC KEY [EKMSampleASYKey]
   DROP CRYPTOGRAPHIC PROVIDER [AzureKeyVault_EKM]
   ```

   If the credential has a client secret that is about to expire, a new secret can be assigned to the credential.

   - Update the secret originally created in [Step 1: Set up an Azure AD service principal](#step-1-set-up-an-azure-ad-service-principal).

      Alter the credential using the same identity and new secret using the following code:

      ```sql
      ALTER CREDENTIAL CREDName
      WITH IDENTITY = 'Original Identity',
      SECRET = 'New Secret';
      ```

   - Restart the SQL Server service.

   - Steps 2 and 3 need to be done on all nodes of an availability group.

For sample scripts, see the blog at [SQL Server Transparent Data Encryption and Extensible Key Management with Azure Key Vault](https://techcommunity.microsoft.com/t5/sql-server/intro-sql-server-transparent-data-encryption-and-extensible-key/ba-p/1427549).

## Next steps

- [Use the SQL Server Connector with SQL encryption features](../../../relational-databases/security/encryption/use-sql-server-connector-with-sql-encryption-features.md)
- [Extensible Key Management with Azure Key Vault](../../../relational-databases/security/encryption/extensible-key-management-using-azure-key-vault-sql-server.md)
- [SQL Server Connector maintenance and troubleshooting](../../../relational-databases/security/encryption/sql-server-connector-maintenance-troubleshooting.md)
