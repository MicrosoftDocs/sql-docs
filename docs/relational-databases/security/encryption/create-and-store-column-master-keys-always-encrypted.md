---
title: "Create & store column master keys for Always Encrypted"
description: Learn how to select a key store and create column master keys for SQL Server Always Encrypted. 
ms.custom: seo-lt-2019
ms.date: "10/31/2019"
ms.service: sql
ms.reviewer: vanto
ms.subservice: security
ms.topic: conceptual
ms.assetid: 856e8061-c604-4ce4-b89f-a11876dd6c88
author: jaszymas
ms.author: jaszymas
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Create and store column master keys for Always Encrypted
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../../includes/applies-to-version/sql-asdb-asdbmi.md)]

*Column Master Keys* are key-protecting keys used in Always Encrypted to encrypt column encryption keys. Column master keys must be stored in a trusted key store, and the keys need to be accessible to applications that need to encrypt or decrypt data, and tools for configuring Always Encrypted and managing Always Encrypted keys.

This article provides details for selecting a key store and creating column master keys for Always Encrypted. For a detailed overview, see [Overview of Key Management for Always Encrypted](../../../relational-databases/security/encryption/overview-of-key-management-for-always-encrypted.md).

## Selecting a Key Store for your Column Master Key

Always Encrypted supports multiple key stores for storing Always Encrypted column master keys. Supported key stores vary depending on which driver and version you're using.

There are two high-level categories of key stores to consider - *Local Key Stores*, and *Centralized Key Stores*.

###  Local or Centralized Key Store?

* **Local Key Stores** - can only be used by applications on computers that contain the local key store. In other words, you need to replicate the key store and key to each computer running your application. An example of a local key store is Windows Certificate Store. When using a local key store, you need to make sure that the key store exists on each machine hosting your application, and that the computer contains the column master keys your application needs to access data protected using Always Encrypted. When you provision a column master key for the first time, or when you change (rotate) the key, you need to make sure the key gets deployed to all machines hosting your application(s).

* **Centralized Key Stores** - serve applications on multiple computers. An example of a centralized key store is [Azure Key Vault](https://azure.microsoft.com/services/key-vault/). A centralized key store usually makes key management easier because you don't need to maintain multiple copies of your column master keys on multiple machines. Ensure that your applications are configured to connect to the centralized key store.

### Which Key Stores are Supported in Always Encrypted Enabled Client Drivers?

Always Encrypted enabled client drivers are SQL Server client drivers that have built-in support for incorporating Always Encrypted into your client applications. Always Encrypted enabled drivers include a few built-in providers for popular key stores. Some drivers also let you implement and register a custom column master key store provider, so that you can use any key store, even if there's no built-in provider for it. When deciding between a built-in provider and a custom provider consider that using a built-in provider typically means fewer changes to your applications (in some cases, only changing a database connection string is required).

The available built-in providers depend on which driver, driver version, and operating system is selected.  Please consult Always Encrypted documentation for your specific driver to determine which key stores are supported out-of-the-box and if your driver supports custom key store providers - [Develop applications using Always Encrypted](always-encrypted-client-development.md).

### Which Key Stores are Supported in SQL Tools?
SQL Server Management Studio, Azure Data Studio and the SqlServer PowerShell module support column master keys stored in:

- Key vaults and [managed HSMs](/azure/key-vault/managed-hsm/overview) in Azure Key Vault.
  > [!NOTE]
  > Managed HSMs require SSMS 18.9 or later and the SqlServer PowerShell module verion 21.1.18235 or later. Azure Data Studio currently does not support managed HSMs.

- Windows Certificate Store.
- Key stores, such as hardware security module, that provide Cryptography Next Generation (CNG) API or Cryptography API (CAPI).

## Creating Column Master Keys in Windows Certificate Store    

A column master key can be a certificate stored in Windows Certificate Store. An Always Encrypted-enabled driver doesn't verify an expiration date or a certificate authority chain. A certificate is simply used as a key pair consisting of a public and private key.

To be a valid column master key, a certificate must:
* be an X.509 certificate.
* be stored in one of the two certificate store locations: *local machine* or *current user*. (To create a certificate in the local machine certificate store location, you must be an administrator on the target machine.)
* contain a private key (the recommended length of the keys in the certificate is 2048 bits or greater).
* be created for key exchange.


There are multiple ways to create a certificate that is a valid column master key but the simplest option is to create a self-signed certificate.


### Create a self-signed certificate using PowerShell

Use the [New-SelfSignedCertificate](/powershell/module/pki/new-selfsignedcertificate) cmdlet to create a self-signed certificate. The following example shows how to generate a certificate that can be used as a column master key for Always Encrypted.

```
# New-SelfSignedCertificate is a Windows PowerShell cmdlet that creates a self-signed certificate. The below examples show how to generate a certificate that can be used as a column master key for Always Encrypted.
$cert = New-SelfSignedCertificate -Subject "AlwaysEncryptedCert" -CertStoreLocation Cert:CurrentUser\My -KeyExportPolicy Exportable -Type DocumentEncryptionCert -KeyUsage KeyEncipherment -KeySpec KeyExchange -KeyLength 2048 

# To create a certificate in the local machine certificate store location you need to run the cmdlet as an administrator.
$cert = New-SelfSignedCertificate -Subject "AlwaysEncryptedCert" -CertStoreLocation Cert:LocalMachine\My -KeyExportPolicy Exportable -Type DocumentEncryptionCert -KeyUsage KeyEncipherment -KeySpec KeyExchange -KeyLength 2048
```

### Create a self-signed certificate using SQL Server Management Studio (SSMS)

For details, see [Provision Always Encrypted keys using SQL Server Management Studio](configure-always-encrypted-keys-using-ssms.md).
For a step-by-step tutorial that uses SSMS and stores Always Encrypted keys in the Windows Certificate Store, see [Always Encrypted Wizard tutorial (Windows Certificate Store)](/azure/azure-sql/database/always-encrypted-certificate-store-configure).


### Making Certificates Available to Applications and Users

If your column master key is a certificate stored in the *local machine* certificate store location, you need to export the certificate with the private key and import it to all machines that host applications that are expected to encrypt or decrypt data stored in encrypted columns, or tools for configuring Always Encrypted and for managing the Always Encrypted keys. Also, each user must be granted a read permission for the certificate stored in the local machine certificate store location to be able to use the certificate as a column master key.

If your column master key is a certificate stored in the *current user* certificate store location, you need to export the certificate with the private key and import it to the current user certificate store location of all user accounts running applications that are expected to encrypt or decrypt data stored in encrypted columns, or tools for configuring Always Encrypted and managing Always Encrypted keys (on all machines containing those applications/tools). No permission configuration is required - after logging on to a machine, a user can access all certificates in their current user certificate store location.

#### Using PowerShell
Use the [Import-PfxCertificate](/powershell/module/pki/import-pfxcertificate) and [Export-PfxCertificate](/powershell/module/pki/export-pfxcertificate) cmdlets to import and export a certificate.

#### Using Microsoft Management Console 

To grant a user the *Read* permission for a certificate stored in the local machine certificate store location, follow these steps:

1.  Open a command prompt and type **mmc**.
2.  In the MMC console, on the **File** menu, click **Add/Remove Snap-in**.
3.  In the **Add/Remove Snap-in** dialog box, click **Add**.
4.  In the **Add Standalone Snap-in** dialog box, click **Certificates**, click **Add**.
5.  In the **Certificates** snap-in dialog box, click **Computer account**, and then click **Finish**.
6.  In the **Add Standalone Snap-in** dialog box, click **Close**.
7.  In the **Add/Remove Snap-in** dialog box, click **OK**.
8.  From the **Certificates** snap-in, locate the certificate in the **Certificates > Personal** folder, right-click the Certificate, point to **All Tasks**, and then click **Manage Private Keys**.
9.  In the **Security** dialog box, add read permission for a user account if needed.

## Creating Column Master Keys in Azure Key Vault

Azure Key Vault helps safeguard cryptographic keys and secrets, and it is a convenient option for storing column master keys for Always Encrypted, especially if your applications are hosted in Azure. To create a key in [Azure Key Vault](/azure/key-vault/general/overview), you need an [Azure subscription](https://azure.microsoft.com/free/) and an Azure Key Vault. A key can be stored in a key vault or in a [managed HSM](/azure/key-vault/managed-hsm/overview). To be a valid column master key, the key managed in Azure Key Vault must be an RSA key.

### Using Azure CLI, Portal or PowerShell

For information on how to create a key in a key vault, see:
- [Quickstart: Set and retrieve a key from Azure Key Vault using Azure CLI](/azure/key-vault/keys/quick-create-cli)
- [Quickstart: Set and retrieve a key from Azure Key Vault using Azure PowerShell](/azure/key-vault/keys/quick-create-powershell)
- [Quickstart: Set and retrieve a key from Azure Key Vault using the Azure portal](/azure/key-vault/keys/quick-create-portal)

For information on how to create a key in a managed HSM, see:
- [Manage a Managed HSM using the Azure CLI](/azure/key-vault/managed-hsm/key-management)

### SQL Server Management Studio (SSMS)

For details on how to create a column master key in a key vault or a managed HSM in Azure Key Vault using SSMS, see [Provision Always Encrypted keys using SQL Server Management Studio](configure-always-encrypted-keys-using-ssms.md).
For a step-by-step tutorial that uses SSMS and stores Always Encrypted keys in a key vault, see [Always Encrypted Wizard tutorial (Azure Key Vault)](/azure/azure-sql/database/always-encrypted-azure-key-vault-configure).

### Making Azure Key Vault Keys Available to Applications and Users

To access an encrypted column, your application needs to be able to access Azure Key Vault and it also needs specific permissions on the column master key to decrypt the column encryption key protecting the column.

To manage keys for Always Encrypted, you need permissions to list and create column master keys in Azure Key Vault, and to perform cryptographic operations using the keys.

#### Key Vaults

If you store your column master keys in a key vault and you are using role permissions for authorization:

* Your application's identity needs to be a member of roles that permit the following data plane actions on the key vault: 

  - Microsoft.KeyVault/vaults/keys/decrypt/action
  - Microsoft.KeyVault/vaults/keys/read
  - Microsoft.KeyVault/vaults/keys/verify/action 
  
  The easiest way to grant the application the required permission is to add its identity to the [Key Vault Crypto User](/azure/role-based-access-control/built-in-roles#key-vault-crypto-user) role. You can also create a custom role with the required permissions.
* A user managing keys for Always Encrypted needs to be a member or roles that permit the following data plane actions on the key vault: 
  - Microsoft.KeyVault/vaults/keys/create/action
  - Microsoft.KeyVault/vaults/keys/decrypt/action
  - Microsoft.KeyVault/vaults/keys/encrypt/action
  - Microsoft.KeyVault/vaults/keys/read
  - Microsoft.KeyVault/vaults/keys/sign/action
  - Microsoft.KeyVault/vaults/keys/verify/action
  
  The easiest way to grant the user the required permission is to add the user to the [Key Vault Crypto User](/azure/role-based-access-control/built-in-roles#key-vault-crypto-user) role.  You can also create a custom role with the required permissions.

If you store your column master keys in a key vault and you are using access policies for authorization:

* Your application's identity needs the following access policy permissions on the key vault: *get*, *unwrapKey*, and *verify*.
* A user managing keys for Always Encrypted needs the following access policy permissions on the key vault: *create*, *get*, *list*, *sign*, *unwrapKey*, *wrapKey*, *verify*.

For general information on how to configure authentication and authorization for key vaults, see [Authorize a security principal to access Key Vault](/azure/key-vault/general/authentication#authorize-a-security-principal-to-access-key-vault).

#### Managed HSMs

Your application's identity needs to be a member of roles that permit the following data plane actions on your managed HSM: 

- Microsoft.KeyVault/managedHsm/keys/decrypt/action
- Microsoft.KeyVault/managedHsm/keys/read/action
- Microsoft.KeyVault/managedHsm/keys/verify/action

Microsoft recommends you create a custom role containing just the above permissions.

A user managing keys for Always Encrypted needs to be a member or roles that permit the following data plane actions on the key: 

- Microsoft.KeyVault/managedHsm/keys/create/action
- Microsoft.KeyVault/managedHsm/keys/decrypt/action
- Microsoft.KeyVault/managedHsm/keys/encrypt/action
- Microsoft.KeyVault/managedHsm/keys/read
- icrosoft.KeyVault/managedHsm/keys/sign/action
- Microsoft.KeyVault/managedHsm/keys/verify/action

The easiest way to grant the user the above permissions is to add the user to the Managed HSM Crypto User role. You can also create a custom role with the required permissions.

For more information about access control for managed HSMs, see:

- [Managed HSM access control](/azure/key-vault/managed-hsm/access-control)
- [Managed HSM local RBAC built-in roles](/azure/key-vault/managed-hsm/built-in-roles#permitted-operations).

## Creating Column Master Keys in Hardware Security Modules using CNG

A column master key for Always Encrypted can be stored in a key store implementing the Cryptography Next Generation (CNG) API. Typically, this type of store is a hardware security module (HSM). An HSM is a physical device that safeguards and manages digital keys and provides crypto-processing. HSMs traditionally come in the form of a plug-in card or an external device that attaches directly to a computer (local HSMs) or a network server.

To make an HSM available to applications on a given machine, a Key Storage Provider (KSP), which implements CNG, must be installed and configured on the machine. An Always Encrypted client driver (a column master key store provider inside the driver), uses the KSP to encrypt and decrypt column encryption keys, protected with column master key stored in the key store.

Windows includes Microsoft Software Key Storage Provider - a software-based KSP, which you can use for testing purposes. See [CNG Key Storage Providers](/windows/desktop/SecCertEnroll/cng-key-storage-providers).

### Creating Column Master Keys in a Key Store using CNG/KSP

A column master key should be an asymmetric key (a public/private key pair), using the RSA algorithm. The recommended key length is 2048 or greater.

#### Using HSM-specific Tools
Consult the documentation for your HSM.

#### Using PowerShell

You can use .NET APIs to create a key in a key store using CNG in PowerShell.


```
$cngProviderName = "Microsoft Software Key Storage Provider" # If you have an HSM, you can use a KSP for your HSM instead of a Microsoft KSP
$cngAlgorithmName = "RSA"
$cngKeySize = 2048 # Recommended key size for Always Encrypted column master keys
$cngKeyName = "AlwaysEncryptedKey" # Name identifying your new key in the KSP
$cngProvider = New-Object System.Security.Cryptography.CngProvider($cngProviderName)
$cngKeyParams = New-Object System.Security.Cryptography.CngKeyCreationParameters
$cngKeyParams.provider = $cngProvider
$cngKeyParams.KeyCreationOptions = [System.Security.Cryptography.CngKeyCreationOptions]::OverwriteExistingKey
$keySizeProperty = New-Object System.Security.Cryptography.CngProperty("Length", [System.BitConverter]::GetBytes($cngKeySize), [System.Security.Cryptography.CngPropertyOptions]::None);
$cngKeyParams.Parameters.Add($keySizeProperty)
$cngAlgorithm = New-Object System.Security.Cryptography.CngAlgorithm($cngAlgorithmName)
$cngKey = [System.Security.Cryptography.CngKey]::Create($cngAlgorithm, $cngKeyName, $cngKeyParams)
```

#### Using SQL Server Management Studio

See [Provision Always Encrypted keys using SQL Server Management Studio](configure-always-encrypted-keys-using-ssms.md).

### Making CNG Keys Available to Applications and Users

Consult your HSM and KSP documentation for how to configure the KSP on a machine and how to grant applications and users access to the HSM.

## Creating Column Master Keys in Hardware Security Modules using CAPI

A column master key for Always Encrypted can be stored in a key store that implements the Cryptography API (CAPI). Typically, such a store is a hardware security module (HSM) - a physical device that safeguards and manages digital keys and provides crypto-processing. HSMs traditionally come in the form of a plug-in card or an external device that attaches directly to a computer (local HSMs) or a network server.

To make an HSM available to applications on a given machine, a Cryptography Service Provider (CSP), which implements CAPI, must be installed and configured on the machine. An Always Encrypted client driver (a column master key store provider inside the driver), uses the CSP to encrypt and decrypt column encryption keys, protected with column master key stored in the key store. 

> [!NOTE]
> CAPI is a legacy, deprecated API. If a KSP is available for your HSM, you should use it, instead of a CSP/CAPI.

A CSP must support the RSA algorithm to be used with Always Encrypted.

Windows includes the following software-based (not backed by an HSM) CSPs that support RSA and can use for testing purposes: Microsoft Enhanced RSA and AES Cryptographic Provider.

### Creating Column Master Keys in a Key Store using CAPI/CSP

A column master key should be an asymmetric key (a public/private key pair), using the RSA algorithm. The recommended key length is 2048 or greater.

#### Using HSM-specific Tools
Consult the documentation for your HSM.

#### Using SQL Server Management Studio (SSMS)
See [Provision Always Encrypted keys using SQL Server Management Studio](configure-always-encrypted-keys-using-ssms.md).

### Making CNG Keys Available to Applications and Users
Consult the documentation for your HSM and CSP for how to configure the CSP on a machine, and how to grant applications and users access to the HSM.
 
## Next Steps  
- [Provision Always Encrypted keys using SQL Server Management Studio](configure-always-encrypted-keys-using-ssms.md)
- [Provision Always Encrypted keys using PowerShell](configure-always-encrypted-keys-using-powershell.md)
  
## See Also 
- [Always Encrypted](../../../relational-databases/security/encryption/always-encrypted-database-engine.md)
- [Overview of key management for Always Encrypted](../../../relational-databases/security/encryption/overview-of-key-management-for-always-encrypted.md)