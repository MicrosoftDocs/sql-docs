---
title: "Provision Always Encrypted keys using PowerShell | Microsoft Docs"
description: Learn how to provision keys for Always Encrypted using the SqlServer PowerShell module to provide control access to the encryption keys and the database.
ms.custom: ""
ms.date: 04/15/2021
ms.service: sql
ms.reviewer: vanto
ms.subservice: security
ms.topic: conceptual
ms.assetid: 3bdf8629-738c-489f-959b-2f5afdaf7d61
author: jaszymas
ms.author: jaszymas
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Provision Always Encrypted keys using PowerShell
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../../includes/applies-to-version/sql-asdb-asdbmi.md)]

    
This article provides the steps to provision keys for Always Encrypted using the [SqlServer PowerShell module](../../../powershell/sql-server-powershell-provider.md). You can use PowerShell to provision Always Encrypted keys both [with and without role separation](../../../relational-databases/security/encryption/overview-of-key-management-for-always-encrypted.md#KeyManagementRoles), providing control over who has access to the actual encryption keys in the key store, and who has access to the database. 

For an overview of Always Encrypted key management, including some high-level best practice recommendations, see [Overview of key management for Always Encrypted](../../../relational-databases/security/encryption/overview-of-key-management-for-always-encrypted.md).
For information about how to start using the SqlServer PowerShell module for Always Encrypted, see [Configure Always Encrypted using PowerShell](../../../relational-databases/security/encryption/configure-always-encrypted-using-powershell.md).


## <a name="KeyProvisionWithoutRoles"></a> Key Provisioning without Role Separation

The key provisioning method described in this section doesn't support role separation between Security Administrators and DBAs. Some of the below steps combine operations on physical keys with operations on key metadata. Therefore, this method of provisioning the keys is recommended for organizations using the DevOps model, or if the database is hosted in the cloud and the primary goal is to restrict cloud administrators (but not on-premises DBAs) from accessing sensitive data. It is not recommended if potential adversaries include DBAs, or if DBAs shouldn't have access to sensitive data.

Before running any steps that involves access to plaintext keys or the key store (identified in the **Accesses plaintext keys/key store** column in the below table), make sure that the PowerShell environment runs on a secure machine that is different from a computer hosting your database. For more information, see [Security Considerations for Key Management](overview-of-key-management-for-always-encrypted.md#security-considerations-for-key-management).


Task  |Article  |Accesses plaintext keys/key store  |Accesses database   
---------|---------|---------|---------
Step 1. Create a column master key in a key store.<br><br>**Note:** The SqlServer PowerShell module doesn't support this step. To accomplish this task from a command-line, use the tools that are specific to your selected key store. |[Create and store column master keys for Always Encrypted](../../../relational-databases/security/encryption/create-and-store-column-master-keys-always-encrypted.md) | Yes | No     
Step 2.  Start a PowerShell environment and import the SqlServer PowerShell module.  |   [Configure Always Encrypted using PowerShell](../../../relational-databases/security/encryption/configure-always-encrypted-using-powershell.md)   |    No    | No         
Step 3.  Connect to your server and database.     |     [Connect to a database](../../../relational-databases/security/encryption/configure-always-encrypted-using-powershell.md#connectingtodatabase)    |    No     | Yes         
Step 4.  Create a *SqlColumnMasterKeySettings* object that contains information about the location of your column master key. SqlColumnMasterKeySettings is an object that exists in memory (in PowerShell). Use the cmdlet that is specific to your key store.   |     [New-SqlAzureKeyVaultColumnMasterKeySettings](/powershell/sqlserver/sqlserver/vlatest/new-sqlazurekeyvaultcolumnmasterkeysettings)<br><br>[New-SqlCertificateStoreColumnMasterKeySettings](/powershell/sqlserver/sqlserver/vlatest/new-sqlcertificatestorecolumnmasterkeysettings)<br><br>[New-SqlCngColumnMasterKeySettings](/powershell/sqlserver/sqlserver/vlatest/new-sqlcngcolumnmasterkeysettings)<br><br>[New-SqlCspColumnMasterKeySettings](/powershell/sqlserver/sqlserver/vlatest/new-sqlcspcolumnmasterkeysettings)        |   No      | No         
Step 5.  Create the metadata about the column master key in your database.      |    [New-SqlColumnMasterKey](/powershell/sqlserver/sqlserver/vlatest/new-sqlcolumnmasterkey)<br><br>**Note:** under the covers, the cmdlet issues the [CREATE COLUMN MASTER KEY (Transact-SQL)](../../../t-sql/statements/create-column-master-key-transact-sql.md) statement to create key metadata.|    No     |    Yes
Step 6.  Authenticate to Azure, if your column master key is stored in Azure Key Vault. | [Add-SqlAzureAuthenticationContext](/powershell/sqlserver/sqlserver/vlatest/add-sqlazureauthenticationcontext)    |  Yes   | No         
Step 7.  Generate a new column encryption key, encrypt it with the column master key and create column encryption key metadata in the database.     |    [New-SqlColumnEncryptionKey](/powershell/sqlserver/sqlserver/vlatest/new-sqlcolumnencryptionkey)<br><br>**Note:** Use a variation of the cmdlet that internally generates and encrypts a column encryption key.<br><br>**Note:** Under the covers, the cmdlet issues the [CREATE COLUMN ENCRYPTION KEY (Transact-SQL)](../../../t-sql/statements/create-column-encryption-key-transact-sql.md) statement to create key metadata.  | Yes | Yes
  

## Windows Certificate Store without Role Separation (Example)

This script is an end-to-end example for generating a column master key that is a certificate in Windows Certificate Store, generating and encrypting a column encryption key, and creating key metadata in a SQL Server database. 


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

# Create a SqlColumnMasterKeySettings object for your column master key. 
$cmkSettings = New-SqlCertificateStoreColumnMasterKeySettings -CertificateStoreLocation "CurrentUser" -Thumbprint $cert.Thumbprint

# Create column master key metadata in the database.
$cmkName = "CMK1"
New-SqlColumnMasterKey -Name $cmkName -InputObject $database -ColumnMasterKeySettings $cmkSettings


# Generate a column encryption key, encrypt it with the column master key and create column encryption key metadata in the database. 
$cekName = "CEK1"
New-SqlColumnEncryptionKey -Name $cekName  -InputObject $database -ColumnMasterKey $cmkName
```

## Azure Key Vault without Role Separation (Example)

This script is an end-to-end example for provisioning and configuring a key vault in Azure Key Vault, generating a column master key in the vault, generating and encrypting a column encryption key, and creating key metadata in an Azure SQL database.


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
$akvKey = Add-AzKeyVaultKey -VaultName $akvName -Name $akvKeyName -Destination "Software"

# Connect to your database (Azure SQL database).
Import-Module "SqlServer"
$serverName = "<Azure SQL server name>.database.windows.net"
$databaseName = "<database name>"
# Change the authentication method in the connection string, if needed.
$connStr = "Server = " + $serverName + "; Database = " + $databaseName + "; Authentication = Active Directory Integrated"
$database = Get-SqlDatabase -ConnectionString $connStr

# Create a SqlColumnMasterKeySettings object for your column master key. 
$cmkSettings = New-SqlAzureKeyVaultColumnMasterKeySettings -KeyURL $akvKey.Key.Kid

# Create column master key metadata in the database.
$cmkName = "CMK1"
New-SqlColumnMasterKey -Name $cmkName -InputObject $database -ColumnMasterKeySettings $cmkSettings

# Authenticate to Azure
Add-SqlAzureAuthenticationContext -Interactive

# Generate a column encryption key, encrypt it with the column master key and create column encryption key metadata in the database. 
$cekName = "CEK1"
New-SqlColumnEncryptionKey -Name $cekName -InputObject $database -ColumnMasterKey $cmkName
```

## CNG/KSP without Role Separation (Example)

The below script is an end-to-end example for generating a column master key in a key store that implements Cryptography Next Generation API (CNG), generating and encrypting a column encryption key, and creating key metadata in a SQL Server database.

The example leverages the key store that uses Microsoft Software Key Storage Provider. You may choose to modify the example to use another store, such as your hardware security module. For that, you'll need to make sure the key store provider (KSP) that implements CNG for your device is installed and properly on your machine. You'll need to replace `Microsoft Software Key Storage Provider` with your device's KSP name.


```powershell
# Create a column master key in a key store that has a CNG provider, a.k.a key store provider (KSP).
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

# Import the SqlServer module.
Import-Module "SqlServer"

# Connect to your database.
$serverName = "<server name>"
$databaseName = "<database name>"
# Change the authentication method in the connection string, if needed.
$connStr = "Server = " + $serverName + "; Database = " + $databaseName + "; Integrated Security = True"
$database = Get-SqlDatabase -ConnectionString $connStr

# Create a SqlColumnMasterKeySettings object for your column master key. 
$cmkSettings = New-SqlCngColumnMasterKeySettings -CngProviderName $cngProviderName -KeyName $cngKeyName

# Create column master key metadata in the database.
$cmkName = "CMK1"
New-SqlColumnMasterKey -Name $cmkName -InputObject $database -ColumnMasterKeySettings $cmkSettings

# Generate a column encryption key, encrypt it with the column master key and create column encryption key metadata in the database. 
$cekName = "CEK1"
New-SqlColumnEncryptionKey -Name $cekName -InputObject $database -ColumnMasterKey $cmkName
```

## <a name="KeyProvisionWithRoles"></a> Key Provisioning With Role Separation

This section provides the steps to configure encryption where security administrators don't have access to the database, and database administrators don't have access to the key store or plaintext keys.


### Security Administrator

Before running any steps that involves access to plaintext keys or the key store (identified in the **Accesses plaintext keys/key store** column in the below table), make sure that:
1.  The PowerShell environment runs on a secure machine that is different from a computer hosting your database.
2.  DBAs in your organization have no access to the machine (that would defeat the purpose of role separation).

For more information, see [Security Considerations for Key Management](overview-of-key-management-for-always-encrypted.md#security-considerations-for-key-management).


Task  |Article  |Accesses plaintext keys/key store  |Accesses database  
---------|---------|---------|---------
Step 1. Create a column master key in a key store.<br><br>**Note:** The SqlServer module doesn't support this step. To accomplish this task from a command-line, you need to use the tools that are specific the type of your key store.     | [Create and store column master keys for Always Encrypted](../../../relational-databases/security/encryption/create-and-store-column-master-keys-always-encrypted.md)  |    Yes    | No 
Step 2.  Start a PowerShell session and import the SqlServer module.      |     [Import the SqlServer module](../../../relational-databases/security/encryption/configure-always-encrypted-using-powershell.md#importsqlservermodule)     | No | No         
Step 3.  Create a *SqlColumnMasterKeySettings* object that contains information about the location of your column master key. *SqlColumnMasterKeySettings* is an object that exists in memory (in PowerShell). Use the cmdlet that is specific to your key store. |      [New-SqlAzureKeyVaultColumnMasterKeySettings](/powershell/sqlserver/sqlserver/vlatest/new-sqlazurekeyvaultcolumnmasterkeysettings)<br><br>[New-SqlCertificateStoreColumnMasterKeySettings](/powershell/sqlserver/sqlserver/vlatest/new-sqlcertificatestorecolumnmasterkeysettings)<br><br>[New-SqlCngColumnMasterKeySettings](/powershell/sqlserver/sqlserver/vlatest/new-sqlcngcolumnmasterkeysettings)<br><br>[New-SqlCspColumnMasterKeySettings](/powershell/sqlserver/sqlserver/vlatest/new-sqlcspcolumnmasterkeysettings)   | No         | No         
Step 4.  Authenticate to Azure, if your column master key is stored in Azure Key Vault |    [Add-SqlAzureAuthenticationContext](/powershell/sqlserver/sqlserver/vlatest/add-sqlazureauthenticationcontext)    |Yes|No         
Step 5.  Generate a column encryption key, encrypt it with the column master key to produce an encrypted value of the column encryption key.     |   [New-SqlColumnEncryptionKeyEncryptedValue](/powershell/sqlserver/sqlserver/vlatest/new-sqlcolumnencryptionkeyencryptedvalue)     |    Yes    | No        
Step 6.  Provide the location of the column master key (the provider name and a key path of the column master key) and an encrypted value of the column encryption key to the DBA.  | See the examples below.        |   No      | No         

### DBA 

DBAs use the information they receive from the Security Admin (step 6 above) to create and manage the Always Encrypted key metadata in the database.

Task  |Article  |Accesses plaintext keys  |Accesses database   
---------|---------|---------|---------
Step 1.  Obtain the location of the column master key and encrypted value of the column encryption key from your Security Administrator. |See the examples below. | No | No
Step 2.  Start a PowerShell environment and import the SqlServer module.  | [Configure Always Encrypted using PowerShell](../../../relational-databases/security/encryption/configure-always-encrypted-using-powershell.md)  | No | No
Step 3.  Connect to your server and a database. | [Connect to a database](../../../relational-databases/security/encryption/configure-always-encrypted-using-powershell.md#connectingtodatabase) | No | Yes
Step 4.  Create a SqlColumnMasterKeySettings object that contains information about the location of your column master key. SqlColumnMasterKeySettings is an object that exists in memory. | New-SqlColumnMasterKeySettings | No | No
Step 5. Create the metadata about the column master key in your database | [New-SqlColumnMasterKey](/powershell/sqlserver/sqlserver/vlatest/new-sqlcolumnmasterkey)<br>**Note:** under the covers, the cmdlet issues the [CREATE COLUMN MASTER KEY (Transact-SQL)](../../../t-sql/statements/create-column-master-key-transact-sql.md) statement to create column master key metadata. | No | Yes
Step 6. Create the column encryption key metadata in the database. | New-SqlColumnEncryptionKey<br>**Note:** DBAs use a variation of the cmdlet that only creates column encryption key metadata.<br>Under the covers, the cmdlet issues the [CREATE COLUMN ENCRYPTION KEY (Transact-SQL)](../../../t-sql/statements/create-column-encryption-key-transact-sql.md) statement to create column encryption key metadata. | No | Yes
  
## Windows Certificate Store with Role Separation (Example)

### Security Administrator


```powershell
# Create a column master key in Windows Certificate Store.
$storeLocation = "CurrentUser"
$certPath = "Cert:" + $storeLocation + "\My"
$cert = New-SelfSignedCertificate -Subject "AlwaysEncryptedCert" -CertStoreLocation $certPath -KeyExportPolicy Exportable -Type DocumentEncryptionCert -KeyUsage DataEncipherment -KeySpec KeyExchange

# Import the SqlServer module
Import-Module "SqlServer"

# Create a SqlColumnMasterKeySettings object for your column master key. 
$cmkSettings = New-SqlCertificateStoreColumnMasterKeySettings -CertificateStoreLocation "CurrentUser" -Thumbprint $cert.Thumbprint

# Generate a column encryption key, encrypt it with the column master key to produce an encrypted value of the column encryption key.
$encryptedValue = New-SqlColumnEncryptionKeyEncryptedValue -TargetColumnMasterKeySettings $cmkSettings

# Share the location of the column master key and an encrypted value of the column encryption key with a DBA, via a CSV file on a share drive
$keyDataFile = "Z:\keydata.txt"
"KeyStoreProviderName, KeyPath, EncryptedValue" > $keyDataFile
$cmkSettings.KeyStoreProviderName + ", " + $cmkSettings.KeyPath + ", " + $encryptedValue >> $keyDataFile

# Read the key data back to verify
$keyData = Import-Csv $keyDataFile
$keyData.KeyStoreProviderName
$keyData.KeyPath
$keyData.EncryptedValue 
```

### DBA

```powershell
# Obtain the location of the column master key and the encrypted value of the column encryption key from your Security Administrator, via a CSV file on a share drive.
$keyDataFile = "Z:\keydata.txt"
$keyData = Import-Csv $keyDataFile

# Import the SqlServer module
Import-Module "SqlServer"

# Connect to your database.
$serverName = "<server name>"
$databaseName = "<database name>"
$connStr = "Server = " + $serverName + "; Database = " + $databaseName + "; Integrated Security = True"
$database = Get-SqlDatabase -ConnectionString $connStr

# Create a SqlColumnMasterKeySettings object for your column master key. 
$cmkSettings = New-SqlColumnMasterKeySettings -KeyStoreProviderName $keyData.KeyStoreProviderName -KeyPath $keyData.KeyPath

# Create column master key metadata in the database.
$cmkName = "CMK1"
New-SqlColumnMasterKey -Name $cmkName -InputObject $database -ColumnMasterKeySettings $cmkSettings

# Generate a  column encryption key, encrypt it with the column master key and create column encryption key metadata in the database. 
$cekName = "CEK1"
New-SqlColumnEncryptionKey -Name $cekName -InputObject $database -ColumnMasterKey $cmkName -EncryptedValue $keyData.EncryptedValue
```  
 
## Next Steps    
- [Configure column encryption using Always Encrypted with PowerShell](configure-column-encryption-using-powershell.md)  
- [Rotate Always Encrypted keys using PowerShell](../../../relational-databases/security/encryption/rotate-always-encrypted-keys-using-powershell.md)
- [Develop applications using Always Encrypted](always-encrypted-client-development.md)

## See Also    
- [Always Encrypted](../../../relational-databases/security/encryption/always-encrypted-database-engine.md)    
- [Overview of key management for Always Encrypted](../../../relational-databases/security/encryption/overview-of-key-management-for-always-encrypted.md)
- [Create and store column master keys for Always Encrypted](../../../relational-databases/security/encryption/create-and-store-column-master-keys-always-encrypted.md)
- [Configure Always Encrypted using PowerShell](../../../relational-databases/security/encryption/configure-always-encrypted-using-powershell.md)
- [Provision Always Encrypted keys using SQL Server Management Studio](configure-always-encrypted-keys-using-ssms.md)
- [CREATE COLUMN MASTER KEY (Transact-SQL)](../../../t-sql/statements/create-column-master-key-transact-sql.md)
- [DROP COLUMN MASTER KEY (Transact-SQL)](../../../t-sql/statements/drop-column-master-key-transact-sql.md)
- [CREATE COLUMN ENCRYPTION KEY (Transact-SQL)](../../../t-sql/statements/create-column-encryption-key-transact-sql.md)
- [ALTER COLUMN ENCRYPTION KEY (Transact-SQL)](../../../t-sql/statements/alter-column-encryption-key-transact-sql.md)
- [DROP COLUMN ENCRYPTION KEY (Transact-SQL)](../../../t-sql/statements/drop-column-encryption-key-transact-sql.md) 
- [sys.column_master_keys (Transact-SQL)](../../../relational-databases/system-catalog-views/sys-column-master-keys-transact-sql.md)
- [sys.column_encryption_keys (Transact-SQL)](../../../relational-databases/system-catalog-views/sys-column-encryption-keys-transact-sql.md)
