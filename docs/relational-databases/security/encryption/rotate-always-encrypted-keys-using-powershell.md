---
description: "Rotate Always Encrypted keys using PowerShell"
title: "Rotate Always Encrypted keys using PowerShell | Microsoft Docs"
ms.custom: ""
ms.date: 06/26/2019
ms.service: sql
ms.reviewer: vanto
ms.subservice: security
ms.topic: conceptual
ms.assetid: 5117b4fd-c8d3-48d5-87c9-756800769f31
author: VanMSFT
ms.author: vanto
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Rotate Always Encrypted keys using PowerShell
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../../includes/applies-to-version/sql-asdb-asdbmi.md)]

This article provides the steps to rotate keys for Always Encrypted using the SqlServer PowerShell module. For information about how to start using the SqlServer PowerShell module for Always Encrypted, see [Configure Always Encrypted using PowerShell](../../../relational-databases/security/encryption/configure-always-encrypted-using-powershell.md).

Rotating Always Encrypted Keys is the process of replacing an existing key with a new one. You may need to rotate a key if it has been compromised, or in order to comply with your organization's policies or compliance regulations that mandate cryptographic keys must be rotated on a regular basis. 

Always Encrypted uses two types of keys, so there are two high-level key rotation workflows; rotating column master keys, and rotating column encryption keys.

* **Column encryption key rotation** - involves decrypting data that is encrypted with the current key, and re-encrypting the data using the new column encryption key. Because rotating a column encryption key requires access to both the keys and the database, column encryption key rotation can only be performed without role separation.
* **Column master key rotation** - involves decrypting column encryption keys that are protected with the current column master key, re-encrypting them using the new column master key, and updating the metadata for both types of keys. Column master key rotation can be completed with or without role separation (when using the SqlServer PowerShell module).

## Column Master Key Rotation without Role Separation

The method of rotating a column master key described in this section doesn't support role separation between a Security Administrator and a DBA. Some of the below steps combine operations on the physical keys with operations on key metadata so this workflow is recommended for organizations using the DevOps model, or when your database is hosted in the cloud and the primary goal is to restrict cloud administrators (but not on-premises DBAs) from accessing sensitive data. It isn't recommended if potential adversaries include DBAs, or if DBAs should not have access to sensitive data.  


| Task | Article | Accesses plaintext keys/keystore| Accesses database
|:---|:---|:---|:---
|Step 1. Create a new column master key in a key store.<br><br>**Note:** The SqlServer PowerShell module doesn't support this step. To accomplish this task from the command-line, you need to use tools that are specific for your key store. | [Create and store column master keys for Always Encrypted](../../../relational-databases/security/encryption/create-and-store-column-master-keys-always-encrypted.md)| Yes | No
|Step 2. Start a PowerShell environment and import the SqlServer module | [Import the SqlServer module](../../../relational-databases/security/encryption/configure-always-encrypted-using-powershell.md#importsqlservermodule) | No | No
|Step 3. Connect to your server and database. | [Connecting to a database](../../../relational-databases/security/encryption/configure-always-encrypted-using-powershell.md#connectingtodatabase) | No | Yes
|Step 4. Create a SqlColumnMasterKeySettings object that contains information about the location of your new column master key. SqlColumnMasterKeySettings is an object that exists in memory (in PowerShell). To create it, you need to use the cmdlet that is specific to your key store. |[New-SqlAzureKeyVaultColumnMasterKeySettings](/powershell/sqlserver/sqlserver/vlatest/new-sqlazurekeyvaultcolumnmasterkeysettings)<br><br>[New-SqlCertificateStoreColumnMasterKeySettings](/powershell/sqlserver/sqlserver/vlatest/new-sqlcertificatestorecolumnmasterkeysettings)<br><br>[New-SqlCngColumnMasterKeySettings](/powershell/sqlserver/sqlserver/vlatest/new-sqlcngcolumnmasterkeysettings)<br><br>[New-SqlCspColumnMasterKeySettings](/powershell/sqlserver/sqlserver/vlatest/new-sqlcspcolumnmasterkeysettings)<br> | No | No
|Step 5. Create the metadata about your new column master key in your database. | [New-SqlColumnMasterKey](/powershell/sqlserver/sqlserver/vlatest/new-sqlcolumnmasterkey)<br><br>**Note:** under the covers, this cmdlet issues the [CREATE COLUMN MASTER KEY (Transact-SQL)](../../../t-sql/statements/create-column-master-key-transact-sql.md) statement to create key metadata. | No | Yes
|Step 6. Authenticate to Azure, if your current column master key or your new column master key is stored in a key vault or a managed HSM in Azure Key Vault | [Add-SqlAzureAuthenticationContext](/powershell/sqlserver/sqlserver/vlatest/add-sqlazureauthenticationcontext) | Yes | No
|Step 7. Start the rotation, by encrypting each of the column encryption keys, which is currently protected with the old column master key, using the new column master key. After this step each impacted column encryption key (associated with the old column master key, being rotated), is encrypted with both the old and the new column master key, and has two encrypted values in the database metadata.| [Invoke-SqlColumnMasterKeyRotation](/powershell/sqlserver/sqlserver/vlatest/invoke-sqlcolumnmasterkeyrotation) | Yes | Yes
|Step 8. Coordinate with the administrators of all applications that query encrypted columns in the database (and are protected with the old column master key), so that they can ensure the applications can access the new column master key.| [Create and Store Column Master Keys (Always Encrypted)](../../../relational-databases/security/encryption/create-and-store-column-master-keys-always-encrypted.md) | Yes | No
|Step 9. Complete the rotation<br><br>**Note:** before executing this step, make sure all applications that query encrypted columns that are protected with the old column master key, have been configured to use the new column master key. If you perform this step prematurely, some of those applications may not be able to decrypt the data. Complete the rotation by removing the encrypted values from the database that were created with the old column master key. This removes the association between the old column master key and the column encryption keys it protects. |[Complete-SqlColumnMasterKeyRotation](/powershell/sqlserver/sqlserver/vlatest/complete-sqlcolumnmasterkeyrotation)| No | Yes
|Step 10. Remove the metadata from the old column master key. |[Remove-SqlColumnMasterKey](/powershell/sqlserver/sqlserver/vlatest/remove-sqlcolumnmasterkey)| No | Yes

> [!NOTE]
> It is highly recommended you do not permanently delete the old column master key after the rotation. Instead, you should keep the old column master key in its current key store or archive it in another secure place. If you restore your database from a backup file to a point in time *before* the new column master key was configured, you will need the old key to access the data.

### Rotating a Column Master Key without Role Separation (Windows Certificate Example)

The below script is an end-to-end example that replaces an existing column master key (CMK1) with a new column master key (CMK2).

```powershell
# Create a new column master key in Windows Certificate Store.
$cert = New-SelfSignedCertificate -Subject "AlwaysEncryptedCert" -CertStoreLocation Cert:CurrentUser\My -KeyExportPolicy Exportable -Type DocumentEncryptionCert -KeyUsage KeyEncipherment -KeySpec KeyExchange -KeyLength 2048

# Import the SqlServer module
Import-Module "SqlServer"

# Connect to your database.
$serverName = "<server name>"
$databaseName = "<database name>"
# Change the authentication method in the connection string, if needed.
$connStr = "Server = " + $serverName + "; Database = " + $databaseName + "; Integrated Security = True"
$database = Get-SqlDatabase -ConnectionString $connStr

# Create a SqlColumnMasterKeySettings object for your new column master key. 
$newCmkSettings = New-SqlCertificateStoreColumnMasterKeySettings -CertificateStoreLocation "CurrentUser" -Thumbprint $cert.Thumbprint

# Create metadata for your new column master key in the database.
$newCmkName = "CMK2"
New-SqlColumnMasterKey -Name $newCmkName -InputObject $database -ColumnMasterKeySettings $newCmkSettings

# Initiate the rotation from the current column master key to the new column master key.
$oldCmkName = "CMK1"
Invoke-SqlColumnMasterKeyRotation -SourceColumnMasterKeyName $oldCmkName -TargetColumnMasterKeyName $newCmkName -InputObject $database

# Complete the rotation of the old column master key.
Complete-SqlColumnMasterKeyRotation -SourceColumnMasterKeyName $oldCmkName  -InputObject $database

# Remove the old column master key metadata.
Remove-SqlColumnMasterKey -Name $oldCmkName -InputObject $database
```

## Column Master Key Rotation with Role Separation

The column master key rotation workflow described in this section ensures the separation between a Security Administrator and a DBA.

> [!IMPORTANT]
> Before executing any steps where *Accesses plaintext keys/keystore*=**Yes** in the table below (steps that access plaintext keys or the key store), make sure that the PowerShell environment runs on a secure machine that is different from a computer hosting your database. For more information, see [Security Considerations for Key Management](overview-of-key-management-for-always-encrypted.md#security-considerations-for-key-management).

### Part 1: DBA

A DBA retrieves metadata about the column master key to be rotated, and about the impacted column encryption keys, which associated with the current column master key. The DBA shares all this information with a Security Administrator.


| Task | Article | Accesses plaintext keys/keystore| Accesses database
|:---|:---|:---|:---
|Step 1. Start a PowerShell environment and import the SqlServer module. | [Import the SqlServer module](../../../relational-databases/security/encryption/configure-always-encrypted-using-powershell.md#importsqlservermodule) | No | None
|Step 2. Connect to your server and a database. | [Connect to a database](../../../relational-databases/security/encryption/configure-always-encrypted-using-powershell.md#connectingtodatabase) | No | Yes
|Step 3. Retrieve the metadata about old column master key.| [Get-SqlColumnMasterKey](/powershell/sqlserver/sqlserver/vlatest/get-sqlcolumnmasterkey) | No | Yes
|Step 4. Retrieve the metadata about column encryption keys, protected by the old column master key, including their encrypted values. | [Get-SqlColumnEncryptionKey](/powershell/sqlserver/sqlserver/vlatest/get-sqlcolumnencryptionkey) | No | Yes
|Step 5. Share the location of the column master key (the provider name and a key path of the column master key) and the encrypted values of the corresponding column encryption keys, protected with the old column master key.| See the examples below. | No | No

### Part 2: Security Administrator

The Security Administrator generates a new column master key, re-encrypts the impacted column encryption keys with the new column master key, and shares the information about the new column master key as well as the set of new encrypted values for the impacted column encryption keys, with the DBA.

| Task | Article | Access plaintext keys/keystore| Accesses database
|:---|:---|:---|:---
|Step 1. Obtain the location of the old column master key and the encrypted values of the corresponding column encryption keys, protected with the old column master key, from your DBA.|N/A<br>See the examples below.|No| No
|Step 2. Create a new column master key in a key store.<br><br>**Note:** The SqlServer module doesn't support this step. To accomplish this task from a command-line, you need to use the tools that are specific the type of your key store.|[Create and store column master keys for Always Encrypted](../../../relational-databases/security/encryption/create-and-store-column-master-keys-always-encrypted.md)| Yes | No
|Step 3. Start a PowerShell environment and import the SqlServer module. | [Import the SqlServer module](../../../relational-databases/security/encryption/configure-always-encrypted-using-powershell.md#importsqlservermodule) | No | No
|Step 4. Create a SqlColumnMasterKeySettings object that contains information about the location of your **old** column master key. SqlColumnMasterKeySettings is an object that exists in memory (in PowerShell). |New-SqlColumnMasterKeySettings| No | No
|Step 5. Create a SqlColumnMasterKeySettings object that contains information about the location of your **new** column master key. SqlColumnMasterKeySettings is an object that exists in memory (in PowerShell). To create it, you need to use the cmdlet that is specific to your key store. | [New-SqlAzureKeyVaultColumnMasterKeySettings](/powershell/sqlserver/sqlserver/vlatest/new-sqlazurekeyvaultcolumnmasterkeysettings)<br><br>[New-SqlCertificateStoreColumnMasterKeySettings](/powershell/sqlserver/sqlserver/vlatest/new-sqlcertificatestorecolumnmasterkeysettings)<br><br>[New-SqlCngColumnMasterKeySettings](/powershell/sqlserver/sqlserver/vlatest/new-sqlcngcolumnmasterkeysettings)<br><br>[New-SqlCspColumnMasterKeySettings](/powershell/sqlserver/sqlserver/vlatest/new-sqlcspcolumnmasterkeysettings)| No | No
|Step 6. Authenticate to Azure, if your old (current) column master key or your new column master key is stored in a key vault or a managed HSM in Azure Key Vault. | [Add-SqlAzureAuthenticationContext](/powershell/sqlserver/sqlserver/vlatest/add-sqlazureauthenticationcontext) | Yes | No
|Step 7. Re-encrypt each value of the column encryption key, which is currently protected with the old column master key, using the new column master key. | [New-SqlColumnEncryptionKeyEncryptedValue](/powershell/sqlserver/sqlserver/vlatest/new-sqlcolumnencryptionkeyencryptedvalue)<br><br>**Note:** When calling this cmdlet, pass the SqlColumnMasterKeySettings objects for both the old and the new column master key, along with a value of the column encryption key, to be re-encrypted.|Yes|No
|Step 8. Share the location of the new column master key (the provider name and a key path of the column master key) and the set of new encrypted values of the column encryption keys, with your DBA.| See the examples below. | No | No

> [!NOTE]
> It is highly recommended you do not permanently delete the old column master key after the rotation. Instead, you should keep the old column master key in its current key store or archive it in another secure place. If you restore your database from a backup file to a point in time *before* the new column master key was configured, you will need the old key to access the data.

### Part 3: DBA

The DBA creates metadata for the new column master key and updates the metadata of the impacted column encryption keys, to add the new set of encrypted values. In this step, the DBA also coordinates with the administrators of the applications querying encryption columns, who ensure the application can access the new column master key. Once all applications are set up to use the new column master key, the DBA removes the old set of encrypted values and the old column master key metadata.

| Task | Article | Access plaintext keys/keystore| Accesses database
|:---|:---|:---|:---
|Step 1. Obtain the location of the new column master key and the new set of encrypted values of the corresponding column encryption keys, protected with the old column master key, from your Security Administrator.| See the examples below. | No | No
|Step 2. Start a PowerShell environment and import the SqlServer module. | [Import the SqlServer module](../../../relational-databases/security/encryption/configure-always-encrypted-using-powershell.md#importsqlservermodule) | No | No
|Step 3. Connect to your server and a database. | [Connecting to a database](../../../relational-databases/security/encryption/configure-always-encrypted-using-powershell.md#connectingtodatabase) | No | Yes
|Step 4. Create a SqlColumnMasterKeySettings object that contains information about the location of your new column master key. SqlColumnMasterKeySettings is an object that exists in memory (in PowerShell). |New-SqlColumnMasterKeySettings| No| No
|Step 5. Create the metadata about your new column master key in your database.|[New-SqlColumnMasterKey](/powershell/sqlserver/sqlserver/vlatest/new-sqlcolumnmasterkey)<br><br>**Note:** Under the covers this cmdlet issues the [CREATE COLUMN MASTER KEY (Transact-SQL)](../../../t-sql/statements/create-column-master-key-transact-sql.md) statement to create key metadata. | No | Yes
|Step 6. Retrieve the metadata about column encryption keys, protected by the old column master key.| [Get-SqlColumnEncryptionKey](/powershell/sqlserver/sqlserver/vlatest/get-sqlcolumnencryptionkey)| No | Yes
|Step 7. Add a new encrypted value (produced using the new column master key) to the metadata for each impacted column encryption key.|[Add-SqlColumnEncryptionKeyValue](/powershell/sqlserver/sqlserver/vlatest/add-sqlcolumnencryptionkeyvalue)|No|Yes
|Step 8. Coordinate with the administrators of all applications that query encrypted columns in the database (and are protected with the old column master key), so that they can ensure the applications can access the new column master key.|[Creating and Storing Column Master Keys (Always Encrypted)](../../../relational-databases/security/encryption/create-and-store-column-master-keys-always-encrypted.md)| No|No
|Step 9. Complete the rotation, by removing the encrypted values associated with the old column master key from the database.<br><br>**Note:** Before executing this step, make sure all applications that query encrypted columns that are protected with the old column master key, have been configured to use the new column master key. If you perform this step prematurely, some of those applications may not be able to decrypt the data.<br><br>This step removes an association between the old column master key and the column encryption keys it protects. | [Complete-SqlColumnMasterKeyRotation](/powershell/sqlserver/sqlserver/vlatest/complete-sqlcolumnmasterkeyrotation)<br><br>Alternatively, you can use [Remove-SqlColumnEncryptionKeyValue](/powershell/sqlserver/sqlserver/vlatest/remove-sqlcolumnencryptionkeyvalue) | No|Yes
|Step 10. Remove the old column master key metadata from the database| [Remove-SqlColumnMasterKey](/powershell/sqlserver/sqlserver/vlatest/remove-sqlcolumnmasterkey)| No|Yes

### Rotating a Column Master Key with Role Separation (Windows Certificate Example)

The below script is an end-to-end example for generating a new column master key that is certificate in Windows Certificate store, rotating an existing (current) column master key, to replace it with the new column master key. The script assumes, the target database contains the column master key, named CMK1 (to be rotated), which encrypts some column encryption keys.

Part 1: DBA

```powershell
# Import the SqlServer module.
Import-Module "SqlServer"

# Connect to your database.
$serverName = "<server name>"
$databaseName = "<database name>"
# Change the authentication method in the connection string, if needed.
$connStr = "Server = " + $serverName + "; Database = " + $databaseName + "; Integrated Security = True"
$database = Get-SqlDatabase -ConnectionString $connStr

# Retrieve the data about the old column master key, which needs to be rotated.
$oldCmkName = "CMK1"
$oldCmk = Get-SqlColumnMasterKey -Name $oldCmkName -InputObject $database


# Share the location of the old column master key with a Security Administrator, via a CSV file on a share drive.
$oldCmkDataFile = "Z:\oldcmkdata.txt"
"KeyStoreProviderName, KeyPath" > $oldCmkDataFile
$oldCmk.KeyStoreProviderName +", " + $oldCmk.KeyPath >> $oldCmkDataFile


# Find column encryption keys associated with the old column master key and provide the encrypted values of column encryption keys to the Security Administrator, via a CSV file on a share drive.
$ceks = Get-SqlColumnEncryptionKey -InputObject $database
$oldCekValuesFile = "Z:\oldcekvalues.txt"
"CEKName, CEKEncryptedValue" > $oldCekValuesFile 
for($i=0; $i -lt $ceks.Length; $i++){
    if($ceks[$i].ColumnEncryptionKeyValues.Length -eq 2) {
        # This column encryption has 2 encrypted values - let's check, if it is associated with the old column master key.
        if($ceks[$i].ColumnEncryptionKeyValues[0].ColumnMasterKeyName -eq $oldCmkName -or $ceks[$i].ColumnEncryptionKeyValues[1].ColumnMasterKeyName -eq $oldCmkName) {
            Write-Host $ceks[$i].Name "already has 2 encrypted values and therefore" $oldCmkName + "cannot be rotated"
            exit 1
        }
    }
    if($ceks[$i].ColumnEncryptionKeyValues[0].ColumnMasterKeyName -eq $oldCmkName) {# This column encryption key has 1 encrypted value that was produced using the old column master key
        # Save the name and the encrypted value of the column encryption key in the file.
        $encryptedValue =  "0x" + -join ($ceks[$i].ColumnEncryptionKeyValues[0].EncryptedValue |  foreach {$_.ToString("X2") } )
        $ceks[$i].Name + "," + $encryptedValue >> $oldCekValuesFile
    }
} 
```


Part 2: Security Administrator

```powershell
# Obtain the location of the old column master key and the encrypted values of the corresponding column encryption keys, from your DBA, via a CSV file on a share drive.
$oldCmkDataFile = "Z:\oldcmkdata.txt"
$oldCmkData = Import-Csv $oldCmkDataFile
$oldCekValuesFile = "Z:\oldcekvalues.txt"
$oldCekValues = @(Import-Csv $oldCekValuesFile)

# Create a new column master key in Windows Certificate Store.
$storeLocation = "CurrentUser"
$certPath = "Cert:\" + $storeLocation + "\My"
$cert = New-SelfSignedCertificate -Subject "AlwaysEncryptedCert" -CertStoreLocation $certPath -KeyExportPolicy Exportable -Type DocumentEncryptionCert -KeyUsage DataEncipherment -KeySpec KeyExchange

# Import the SqlServer module.
Import-Module "SqlServer"

# Create a SqlColumnMasterKeySettings object for your old column master key. 
$oldCmkSettings = New-SqlColumnMasterKeySettings -KeyStoreProviderName $oldCmkData.KeyStoreProviderName -KeyPath $oldCmkData.KeyPath

# Create a SqlColumnMasterKeySettings object for your new column master key. 
$newCmkSettings = New-SqlCertificateStoreColumnMasterKeySettings -CertificateStoreLocation $storeLocation -Thumbprint $cert.Thumbprint


# Prepare a CSV file, you will use to share the encrypted values of column encryption keys, produced using the new column master key.
$newCekValuesFile = "Z:\newcekvalues.txt"
"CEKName, CEKEncryptedValue" > $newCekValuesFile

# Re-encrypt each value with the new column master key and save the new encrypted value in the file.
for($i=0; $i -lt $oldCekValues.Count; $i++){
    # Re-encrypt each value with the new CMK
    $newValue = New-SqlColumnEncryptionKeyEncryptedValue -TargetColumnMasterKeySettings $newCmkSettings -ColumnMasterKeySettings $oldCmkSettings -EncryptedValue $oldCekValues[$i].CEKEncryptedValue
    $oldCekValues[$i].CEKName + ", " + $newValue >> $newCekValuesFile
}

# Share the new column master key data with your DBA, via a CSV file.
$newCmkDataFile = $home + "\newcmkdata.txt"
"KeyStoreProviderName, KeyPath" > $newCmkDataFile
$newCmkSettings.KeyStoreProviderName +", " + $newCmkSettings.KeyPath >> $newCmkDataFile
```


Part 3: DBA

```powershell
# Obtain the location of the new column master key and the new encrypted values of the corresponding column encryption keys, from your Security Administrator, via a CSV file on a share drive.
$newCmkDataFile = "Z:\newcmkdata.txt"
$newCmkData = Import-Csv $newCmkDataFile
$newCekValuesFile = "Z:\newcekvalues.txt"
$newCekValues = @(Import-Csv $newCekValuesFile)

# Import the SqlServer module.
Import-Module "SqlServer"

# Connect to your database.
$serverName = "<server name>"
$databaseName = "<database name>"
# Change the authentication method in the connection string, if needed.
$connStr = "Server = " + $serverName + "; Database = " + $databaseName + "; Integrated Security = True"
$database = Get-SqlDatabase -ConnectionString $connStr

# Create a SqlColumnMasterKeySettings object for your new column master key. 
$newCmkSettings = New-SqlColumnMasterKeySettings -KeyStoreProviderName $newCmkData.KeyStoreProviderName -KeyPath $newCmkData.KeyPath
# Create metadata for the new column master key in the database.
$newCmkName = "CMK2"
New-SqlColumnMasterKey -Name $newCmkName -InputObject $database -ColumnMasterKeySettings $newCmkSettings


# Get all CEK objects
$oldCmkName = "CMK1"
$ceks = Get-SqlColumnEncryptionKey -InputObject $database
for($i=0; $i -lt $ceks.Length; $i++){
    if($ceks[$i].ColumnEncryptionKeyValues.Length -eq 2) {# This column encryption key has 2 encrypted values. Let's check, if it is associated with the old CMK.
        if($ceks[$i].ColumnEncryptionKeyValues[0].ColumnMasterKeyName -eq $oldCmkName -or $ceks[$i].ColumnEncryptionKeyValues[1].ColumnMasterKeyName -eq $oldCmkName) {
            Write-Host $ceks[$i].Name "already has 2 encrypted values and therefore" $oldCmkName + "cannot be rotated"
            exit 1
        }
    }
    if($ceks[$i].ColumnEncryptionKeyValues[0].ColumnMasterKeyName -eq $oldCmkName) {
        # Find the corresponding new encrypted value, received from the Security Administrator.
        $newValueRow = ($newCekValues| Where-Object {$_.CEKName -eq $ceks[$i].Name }[0])
        # Update the column encryption key metadata object by adding the new encrypted value
        Add-SqlColumnEncryptionKeyValue -ColumnMasterKeyName $newCmkName -Name $ceks[$i].Name -EncryptedValue $newValueRow.CEKEncryptedValue -InputObject $database 
    }
}

# Complete the rotation of the current column master key.
Complete-SqlColumnMasterKeyRotation -SourceColumnMasterKeyName $oldCmkName  -InputObject $database

# Remove the old column master key.
Remove-SqlColumnMasterKey -Name $oldCmkName -InputObject $database
```

## Rotating a Column Encryption Key

Rotating a column encryption key involves decrypting the data in all columns, encrypted with the key to be rotated, and re-encrypting the data using the new column encryption key. This rotation workflow requires access to both the keys and the database, and therefore can't be performed with role separation. 
Rotating a column encryption key can take a long time, if the tables containing columns encrypted with the key, being rotated, are large. Therefore, your organization needs to plan a column encryption key rotation carefully.

You can rotate a column encryption key using an offline or an online approach. The former method is likely to be faster, but your applications can't write to the impacted tables. The latter approach will likely to take longer, but you can limit the time interval, during which the impacted tables aren't available to applications. See [Configure column encryption using Always Encrypted with PowerShell](configure-column-encryption-using-powershell.md)  and [Set-SqlColumnEncryption](/powershell/module/sqlserver/set-sqlcolumnencryption/) for more details.

| Task | Article | Accesses plaintext keys/keystore| Accesses database
|:---|:---|:---|:---
|Step 1. Start a PowerShell environment and import the SqlServer module. | [Import the SqlServer module](../../../relational-databases/security/encryption/configure-always-encrypted-using-powershell.md#importsqlservermodule) | No | No
|Step 2. Connect to your server and a database. | [Connecting to a database](../../../relational-databases/security/encryption/configure-always-encrypted-using-powershell.md#connectingtodatabase) | No | Yes
|Step 3. Authenticate to Azure, if your column master key (protecting the column encryption key, to be rotated), is stored in a key vault or a managed HSM in Azure Key Vault. | [Add-SqlAzureAuthenticationContext](/powershell/sqlserver/sqlserver/vlatest/add-sqlazureauthenticationcontext) | Yes | No
|Step 4. Generate a new column encryption key, encrypt it with the column master key and create column encryption key metadata in the database.  | [New-SqlColumnEncryptionKey](/powershell/sqlserver/sqlserver/vlatest/new-sqlcolumnencryptionkey)<br><br>**Note:** Use a variation of the cmdlet that internally generates and encrypts a column encryption key.<br>Under the covers this cmdlet issues the [CREATE COLUMN ENCRYPTION KEY (Transact-SQL)](../../../t-sql/statements/create-column-encryption-key-transact-sql.md) statement to create the key metadata. | Yes | Yes
|Step 5. Find all columns encrypted with the old column encryption key. | [SQL Server Management Objects (SMO) Programming Guide](../../../relational-databases/server-management-objects-smo/sql-server-management-objects-smo-programming-guide.md) | No | Yes
|Step 6. Create a *SqlColumnEncryptionSettings* object for each impacted column.  SqlColumnEncryptionSettings is an object that exists in memory (in PowerShell). It specifies the target encryption scheme for a column. In this case, the object should specify the impacted column should be encrypted using the new column encryption key. | [New-SqlColumnEncryptionSettings](/powershell/sqlserver/sqlserver/vlatest/new-sqlcolumnencryptionsettings) | No | No
|Step 7. Re-encrypt the columns, identified in step 5, using the new column encryption key. | [Set-SqlColumnEncryption](/powershell/sqlserver/sqlserver/vlatest/set-sqlcolumnencryption)<br><br>**Note:** This step may take a long time. Your applications won't be able to access the tables through the entire operation or a portion of it, depending on the approach (online vs. offline), you select. | Yes | Yes
|Step 8. Remove the metadata for the old column encryption key. | [Remove-SqlColumnEncryptionKey](/powershell/sqlserver/sqlserver/vlatest/remove-sqlcolumnencryptionkey) | No | Yes

### Example - Rotating a Column Encryption Key

The below script demonstrates rotating a column encryption key.  The script assumes, the target database contains some columns encrypted with a column encryption key, named CEK1 (to be rotated), which is protected using a column master key, named CMK1 (the column master key isn't stored in Azure Key Vault). 


```powershell
# Import the SqlServer module.
Import-Module "SqlServer"

# Connect to your database.
$serverName = "<server name>"
$databaseName = "<database name>"
# Change the authentication method in the connection string, if needed.
$connStr = "Server = " + $serverName + "; Database = " + $databaseName + "; Integrated Security = True"
$database = Get-SqlDatabase -ConnectionString $connStr

# Generate a new column encryption key, encrypt it with the column master key and create column encryption key metadata in the database. 
$cmkName = "CMK1"
$newCekName = "CEK2"
New-SqlColumnEncryptionKey -Name $newCekName -InputObject $database -ColumnMasterKey $cmkName 


# Find all columns encrypted with the old column encryption key, and create a SqlColumnEncryptionSetting object for each column.
$ces = @()
$oldCekName = "CEK1"
$tables = $database.Tables
for($i=0; $i -lt $tables.Count; $i++){
    $columns = $tables[$i].Columns
    for($j=0; $j -lt $columns.Count; $j++) {
        if($columns[$j].isEncrypted -and $columns[$j].ColumnEncryptionKeyName -eq $oldCekName) {
            $threeColPartName = $tables[$i].Schema + "." + $tables[$i].Name + "." + $columns[$j].Name 
            $ces += New-SqlColumnEncryptionSettings -ColumnName $threeColPartName -EncryptionType $columns[$j].EncryptionType -EncryptionKey $newCekName
        }
     }
}

# Re-encrypt all columns, currently encrypted with the old column encryption key, using the new column encryption key.
Set-SqlColumnEncryption -ColumnEncryptionSettings $ces -InputObject $database -UseOnlineApproach -MaxDowntimeInSeconds 120 -LogFileDirectory .

# Remove the old column encryption key metadata.
Remove-SqlColumnEncryptionKey -Name $oldCekName -InputObject $database
```

## Next Steps
- [Query columns using Always Encrypted with SQL Server Management Studio](always-encrypted-query-columns-ssms.md)
- [Develop applications using Always Encrypted](always-encrypted-client-development.md)
  
## See Also
- [Always Encrypted](../../../relational-databases/security/encryption/always-encrypted-database-engine.md)
- [Overview of key management for Always Encrypted](overview-of-key-management-for-always-encrypted.md) 
- [Configure Always Encrypted using PowerShell](configure-always-encrypted-using-powershell.md)
- [Rotate Always Encrypted keys using SQL Server Management Studio](rotate-always-encrypted-keys-using-ssms.md)
- [CREATE COLUMN MASTER KEY (Transact-SQL)](../../../t-sql/statements/create-column-master-key-transact-sql.md)
- [DROP COLUMN MASTER KEY (Transact-SQL)](../../../t-sql/statements/drop-column-master-key-transact-sql.md)
- [CREATE COLUMN ENCRYPTION KEY (Transact-SQL)](../../../t-sql/statements/create-column-encryption-key-transact-sql.md)
- [ALTER COLUMN ENCRYPTION KEY (Transact-SQL)](../../../t-sql/statements/alter-column-encryption-key-transact-sql.md)
- [DROP COLUMN ENCRYPTION KEY (Transact-SQL)](../../../t-sql/statements/drop-column-encryption-key-transact-sql.md) 
- [sys.column_master_keys (Transact-SQL)](../../../relational-databases/system-catalog-views/sys-column-master-keys-transact-sql.md)
- [sys.column_encryption_keys (Transact-SQL)](../../../relational-databases/system-catalog-views/sys-column-encryption-keys-transact-sql.md)
