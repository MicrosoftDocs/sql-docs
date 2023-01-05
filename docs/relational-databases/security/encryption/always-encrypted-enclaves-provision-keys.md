---
description: "Provision enclave-enabled keys"
title: "Provision enclave-enabled keys | Microsoft Docs"
ms.custom:
- event-tier1-build-2022
ms.date: 05/24/2022
ms.service: sql
ms.reviewer: vanto
ms.subservice: security
ms.topic: conceptual
author: jaszymas
ms.author: jaszymas
monikerRange: ">= sql-server-ver15"
---
# Provision enclave-enabled keys

[!INCLUDE [sqlserver2019-windows-only-asdb](../../../includes/applies-to-version/sqlserver2019-windows-only-asdb.md)]

This article describes how to provision enclave-enabled keys that support computations inside server-side secure enclaves used for [Always Encrypted with secure enclaves](always-encrypted-enclaves.md). 

The general guidelines and processes for [managing Always Encrypted keys](overview-of-key-management-for-always-encrypted.md) apply when you provision enclave-enabled keys. This article address details specific to Always Encrypted with secure enclaves.

To provision an enclave-enabled column master key using SQL Server Management Studio or PowerShell, make sure the new key supports enclave computations. This will cause the tool (SSMS or PowerShell) to generate the `CREATE COLUMN MASTER KEY` statement that sets the `ENCLAVE_COMPUTATIONS` in the columns master key metadata in the database. For more information, see [CREATE COLUMN MASTER KEY (Transact-SQL)](../../../t-sql/statements/create-column-master-key-transact-sql.md). 

The tool will also digitally sign the column master properties with the column master key, and it will store the signature in the database metadata. The signature prevents malicious tampering with the `ENCLAVE_COMPUTATIONS` setting. The SQL client drivers verify the signatures before allowing the enclave use. This provides security administrators with control over which column data can be computed inside the enclave.

The `ENCLAVE_COMPUTATIONS` is immutable, meaning, you can't change it once you define the column master key in the metadata. To enable enclave computations using a column encryption key, that a given column master key encrypts, you need to rotate the column master key and replace it with an enclave-enabled column master key. See [Rotate enclave-enabled keys](always-encrypted-enclaves-rotate-keys.md).

> [!NOTE]
> Currently, both SSMS and PowerShell support enclave-enabled column master keys stored in Azure Key Vault or Windows Certificate Store. Hardware security modules (using CNG or CAPI) are not supported.

To create an enclave-enabled column encryption key, you need to ensure that you select an enclave-enabled column master key to encrypt the new key. 

The following sections provide more details on how to provision enclave-enabled keys using SSMS and PowerShell.

## Provision enclave-enabled keys using SQL Server Management Studio
In SQL Server Management Studio you can provision:
- An enclave-enabled column master key using the **New Column Master Key** dialog.
- An enclave-enabled column encryption key using the **New Column Encryption Key** dialog.

> [!NOTE]
> The [Always Encrypted wizard](always-encrypted-wizard.md) does not currently support generating enclave-enabled keys. You can, however, create enclave-enabled keys using the above dialogs first, and then when you run the wizard, select an already existing enclave-enabled column encryption for columns that you want to encrypt.

Minimum SSMS version requirements:

- SSMS 18.3 when using [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].
- SSMS 18.8 when using [!INCLUDE[ssSDSfull](../../../includes/sssdsfull-md.md)].

### Provision enclave-enabled column master keys with the New Column Master Key dialog
To provision an enclave-enabled column master key, follow the steps in [Provision Column Master Keys with the New Column Master Key Dialog](configure-always-encrypted-keys-using-ssms.md#provision-column-master-keys-with-the-new-column-master-key-dialog). Make sure you select **Allow enclave computations**. See the below screenshot:

![Allow enclave computations](./media/always-encrypted-enclaves/allow-enclave-computations.png)

> [!NOTE]
> The **Allow enclave computations** checkbox appears only if a secure enclave is  configured for your database. If you are using [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], see [Configure the secure enclave in SQL Server](always-encrypted-enclaves-configure-enclave-type.md). If you are using [!INCLUDE[ssSDSfull](../../../includes/sssdsfull-md.md)], see [Enable Intel SGX for your Azure SQL database](/azure/azure-sql/database/always-encrypted-enclaves-enable-sgx).

> [!TIP]
> To check if a column master key is enclave-enabled, right-click on it in Object Explorer and select **Properties**. If the key is enclave-enabled, **Enclave Computations: Allowed** appears in the window showing the properties of the key. Alternativelly, you can use the [sys.column_master_keys (Transact-SQL)](../../system-catalog-views/sys-column-master-keys-transact-sql.md) view.

### Provision enclave-enabled column encryption keys with the New Column Encryption Key dialog
To provision an enclave-enabled column encryption key, follow the steps in [Provision Column Encryption Keys with the New Column Encryption Key Dialog](configure-always-encrypted-keys-using-ssms.md#provision-column-encryption-keys-with-the-new-column-encryption-key-dialog). When selecting a column master key, make sure it is enclave-enabled.

> [!TIP]
> To check if a column encryption key is enclave-enabled, right-click on it in Object Explorer and select **Properties**. If the key is enclave-enabled, **Enclave Computations: Allowed** appears in the window showing the properties of the key.

## Provision enclave-enabled keys using PowerShell
To provision enclave-enabled keys using PowerShell, you need the SqlServer PowerShell module version 21.1.18179 or higher.

In general, PowerShell key provisioning workflows (with and without role separation) for Always Encrypted, described in [Provision Always Encrypted Keys using PowerShell](configure-always-encrypted-keys-using-powershell.md) also apply to enclave-enabled keys. This section describes details specific to enclave-enabled keys.

The SqlServer PowerShell module extends the  [**New-SqlCertificateStoreColumnMasterKeySettings**](/powershell/module/sqlserver/new-sqlcertificatestorecolumnmasterkeysettings) and [**New-SqlAzureKeyVaultColumnMasterKeySettings**](/powershell/module/sqlserver/new-sqlazurekeyvaultcolumnmasterkeysettings) cmdlets with the `-AllowEnclaveComputations` parameter to allow you to specify a column master key that is enclave-enabled during the provisioning process. Either cmdlet creates a local object containing properties of a column master key (stored in Azure Key Vault or in the Windows Certificate Store). If specified, the `-AllowEnclaveComputations` property marks the key as enclave-enabled in the local object. It also causes the cmdlet to access the referenced column master key (in Azure Key Vault or in Windows Certificate Store) to digitally sign the properties of the key. Once you create a settings object for a new enclave-enabled column master key, you can use it in a subsequent invocation of the [**New-SqlColumnMasterKey**](/powershell/module/sqlserver/new-sqlcolumnmasterkey) cmdlet to create a metadata object describing the new key in the database.

Provisioning enclave-enabled column encryption keys is no different from provisioning column encryption keys that are not enclave-enabled. You just need to make sure that a column master key used to encrypt the new column encryption key is enclave-enabled.

> [!NOTE]
> The SqlServer PowerShell module does not currently support provisioning enclave-enabled keys stored in hardware security modules (using CNG or CAPI).

### Example - provision enclave-enabled keys using Windows Certificate Store
The below end-to-end example shows how to provision enclave-enabled keys, storing the column master key stored in Windows Certificate Store. The script is based on the example in [Windows Certificate Store without Role Separation (Example)](configure-always-encrypted-keys-using-powershell.md#windows-certificate-store-without-role-separation-example). Important to note is the use of the `-AllowEnclaveComputations` parameter in the [**New-SqlCertificateStoreColumnMasterKeySettings**](/powershell/module/sqlserver/new-sqlcertificatestorecolumnmasterkeysettings) cmdlet, which is the only difference between the workflows in the two examples.

```powershell
# Create a column master key in Windows Certificate Store.
$cert = New-SelfSignedCertificate -Subject "AlwaysEncryptedCert" -CertStoreLocation Cert:CurrentUser\My -KeyExportPolicy Exportable -Type DocumentEncryptionCert -KeyUsage DataEncipherment -KeySpec KeyExchange

# Import the SqlServer module.
Import-Module "SqlServer"

# Connect to your database.
$serverName = "<server name>"
$databaseName = "<database name>"
# Change the authentication method in the connection string, if needed.
$connStr = "Server = " + $serverName + "; Database = " + $databaseName + "; Integrated Security = True"
$database = Get-SqlDatabase -ConnectionString $connStr

# Create a SqlColumnMasterKeySettings object for your column master key
# using the -AllowEnclaveComputations parameter.
$cmkSettings = New-SqlCertificateStoreColumnMasterKeySettings -CertificateStoreLocation "CurrentUser" -Thumbprint $cert.Thumbprint -AllowEnclaveComputations

# Create column master key metadata in the database.
$cmkName = "CMK1"
New-SqlColumnMasterKey -Name $cmkName -InputObject $database -ColumnMasterKeySettings $cmkSettings

# Generate a column encryption key, encrypt it with the column master key and create column encryption key metadata in the database. 
$cekName = "CEK1"
New-SqlColumnEncryptionKey -Name $cekName  -InputObject $database -ColumnMasterKey $cmkName
```

### Example - provision enclave-enabled keys using Azure Key Vault
The below end-to-end example shows how to provision enclave-enabled keys, storing the column master key in a key vault in Azure Key Vault. The script is based on the example in [Azure Key Vault without Role Separation (Example)](configure-always-encrypted-keys-using-powershell.md#azure-key-vault-without-role-separation-example). It's important to note two differences between the workflow for enclave-enabled keys compared to the keys that are not enclave-enabled. 
- In the below script, the [**New-SqlCertificateStoreColumnMasterKeySettings**](/powershell/module/sqlserver/new-sqlcertificatestorecolumnmasterkeysettings) uses the `-AllowEnclaveComputations` parameter to make the new column master key enclave-enabled. 
- The below script calls the [**Add-SqlAzureAuthenticationContext**](/powershell/module/sqlserver/add-sqlazureauthenticationcontext) cmdlet to sign in to Azure before calling the [**New-SqlAzureKeyVaultColumnMasterKeySettings**](/powershell/module/sqlserver/new-sqlazurekeyvaultcolumnmasterkeysettings) cmdlet. Singing in to Azure first is necessary, because the `-AllowEnclaveComputations` parameter causes the **New-SqlAzureKeyVaultColumnMasterKeySettings** to call Azure Key Vault to sign the properties of the column master key.

```powershell
# Create a column master key in Azure Key Vault.
Import-Module Az
Connect-AzAccount
$SubscriptionId = "<Azure SubscriptionId>"
$resourceGroup = "<resource group name>"
$azureLocation = "<datacenter location>"
$akvName = "<key vault name>"
$akvKeyName = "<key name>"
$azureCtx = Set-AzConteXt -SubscriptionId $SubscriptionId # Sets the context for the below cmdlets to the specified subscription.
New-AzResourceGroup -Name $resourceGroup -Location $azureLocation # Creates a new resource group - skip, if your desired group already exists.
New-AzKeyVault -VaultName $akvName -ResourceGroupName $resourceGroup -Location $azureLocation # Creates a new key vault - skip if your vault already exists.
Set-AzKeyVaultAccessPolicy -VaultName $akvName -ResourceGroupName $resourceGroup -PermissionsToKeys get, create, delete, list, wrapKey,unwrapKey, sign, verify -UserPrincipalName $azureCtx.Account
$akvKey = Add-AzureKeyVaultKey -VaultName $akvName -Name $akvKeyName -Destination "Software"

# Connect to your database.
$serverName = "<server name>"
$databaseName = "<database name>"
# Change the authentication method in the connection string, if needed.
$connStr = "Server = " + $serverName + "; Database = " + $databaseName + "; Integrated Security = True"
$database = Get-SqlDatabase -ConnectionString $connStr

# Authenticate to Azure - it is required before calling the next cmdlet.
Add-SqlAzureAuthenticationContext -Interactive

# Create a SqlColumnMasterKeySettings object for your column master key. 
$cmkSettings = New-SqlAzureKeyVaultColumnMasterKeySettings -KeyURL $akvKey.ID -AllowEnclaveComputations

# Create column master key metadata in the database.
$cmkName = "CMK1"
New-SqlColumnMasterKey -Name $cmkName -InputObject $database -ColumnMasterKeySettings $cmkSettings

# Generate a column encryption key, encrypt it with the column master key and create column encryption key metadata in the database. 
$cekName = "CEK1"
New-SqlColumnEncryptionKey -Name $cekName -InputObject $database -ColumnMasterKey $cmkName
```

## Next Steps
- [Run Transact-SQL statements using secure enclaves](always-encrypted-enclaves-query-columns.md)
- [Configure column encryption in-place using Always Encrypted with secure enclaves](always-encrypted-enclaves-configure-encryption.md)
- [Enable Always Encrypted with secure enclaves for existing encrypted columns](always-encrypted-enclaves-enable-for-encrypted-columns.md)
- [Develop applications using Always Encrypted with secure enclaves](always-encrypted-enclaves-client-development.md) 

## See Also  
- [Tutorial: Getting started with Always Encrypted with secure enclaves in SQL Server](../tutorial-getting-started-with-always-encrypted-enclaves.md)
- [Tutorial: Getting started with Always Encrypted with secure enclaves in Azure SQL Database](/azure/azure-sql/database/always-encrypted-enclaves-getting-started)
- [Manage keys for Always Encrypted with secure enclaves](always-encrypted-enclaves-manage-keys.md)
- [CREATE COLUMN MASTER KEY (Transact-SQL)](../../../t-sql/statements/create-column-master-key-transact-sql.md)
