---
title: "Configure column encryption using Always Encrypted with secure enclaves with PowerShell"
description: "Configure column encryption using Always Encrypted with secure enclaves with PowerShell"
author: Pietervanhove
ms.author: pivanho
ms.reviewer: vanto
ms.date: "04/05/2023"
ms.service: sql
ms.subservice: security
ms.custom: devx-track-azurepowershell
ms.topic: conceptual
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Configure column encryption in-place with PowerShell
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../../includes/applies-to-version/sql-asdb-asdbmi.md)]

This article provides the steps for setting the target Always Encrypted configuration for database columns using the [Set-SqlColumnEncryption](/powershell/sqlserver/sqlserver/vlatest/set-sqlcolumnencryption) cmdlet (in the *SqlServer* PowerShell module). The **Set-SqlColumnEncryption** cmdlet modifies both the schema of the target database and the data stored in the selected columns. The data stored in a column can be encrypted, re-encrypted, or decrypted, depending on the specified target encryption settings for the columns and the current encryption configuration. To trigger in-place cryptographic operations using an enclave, **Set-SqlColumnEncryption** must use a database connection created using a connection string with the Attestation Protocol and optionally the Attestation URL keywords.

## Prerequisites

To set the target encryption configuration, you need to make sure:
- an enclave-enabled column encryption key is configured in the database (if you're encrypting or re-encrypting a column). For details, see [Manage keys for Always Encrypted with secure enclaves](../../../relational-databases/security/encryption/always-encrypted-enclaves-manage-keys.md).
- you're connected to the database with Always Encrypted enabled and the attestation properties specified in the connection string.
- you can access the column master key for each column you want to encrypt, re-encrypt, or decrypt, from the computer running the PowerShell cmdlets.
- you use SqlServer PowerShell module version 22.0.50 or later.

## Availability Considerations

The in-place **Set-SqlColumnEncryption** cmdlet doesn't support online encryption.

With the offline approach, the target tables (and any tables related to the target tables, for example, any tables a target table have foreign key relationships with) are unavailable to write transactions throughout the duration of the operation. The semantics of foreign key constraints (**CHECK** or **NOCHECK**) are always preserved when using the offline approach.

If you can't afford downtime during the encryption process, we suggest using [Configure column encryption in-place with Transact-SQL](always-encrypted-enclaves-configure-encryption-tsql.md), which supports online encryption.

## Security Considerations

The **Set-SqlColumnEncryption** cmdlet, used to configure encryption for database columns, handles both Always Encrypted keys and the data stored in database columns. Therefore, it's important you run the cmdlet on a secure computer. If your database is in SQL Server, execute the cmdlet from a different computer than the computer hosting your SQL Server instance. Because the primary goal of Always Encrypted is to ensure encrypted sensitive data is safe even if the database system gets compromised, executing a PowerShell script that processes keys and/or sensitive data on the SQL Server computer can reduce or defeat the benefits of the feature.

Task  |Article  |Accesses plaintext keys/key store  |Accesses database   
---|---|---|---
Step 1. Start a PowerShell environment and import the SqlServer module. | [Import the SqlServer module](../../../relational-databases/security/encryption/configure-always-encrypted-using-powershell.md#importsqlservermodule) | No | No
Step 2. Connect to your server and database | [Connecting to a database](../../../relational-databases/security/encryption/configure-always-encrypted-using-powershell.md#connectingtodatabase) | No | Yes
Step 3. Authenticate to Azure, if your column master key (protecting the column encryption key, to be rotated), is stored in Azure Key Vault | [Connect-AzAccount](/powershell/module/az.accounts/connect-azaccount) | Yes | No
Step 4. Obtain an access token for Azure Key Vaults. | [Get-AzAccessToken](/powershell/module/az.accounts/get-azaccesstoken) | No | No
Step 5. Construct an array of SqlColumnEncryptionSettings objects - one for each database column, you want to encrypt, re-encrypt, or decrypt. SqlColumnMasterKeySettings is an object that exists in memory (in PowerShell). It specifies the target encryption scheme for a column. | [New-SqlColumnEncryptionSettings](/powershell/sqlserver/sqlserver/vlatest/new-sqlcolumnencryptionsettings) | No | No
Step 5. Set the desired encryption configuration, specified in the array of SqlColumnMasterKeySettings objects, you created in the previous step. A column will be encrypted, re-encrypted, or decrypted, depending on the specified target settings and the current encryption configuration of the column.| [Set-SqlColumnEncryption](/powershell/sqlserver/sqlserver/vlatest/set-sqlcolumnencryption)<br><br>**Note:** This step may take a long time. Your applications won't be able to access the tables through the entire operation or a portion of it, depending on the approach (online vs. offline), you select. | Yes | Yes

## Encrypt Columns using VBS enclaves

The below example demonstrates setting the target encryption configuration for a couple of columns. If either column isn't already encrypted, it will be encrypted. If any column is already encrypted using a different key and/or a different encryption type, it will be decrypted and then re-encrypted with the specified target key/type. VBS enclaves currently don't support attestation. The EnclaveAttestationProtocol parameter should be set to *None* and the EnclaveAttestationUrl isn't required.


```PowerShell
# Import modules
Import-Module "SqlServer" -MinimumVersion 22.0.50
Import-Module Az.Accounts -MinimumVersion 2.2.0

#Connect to Azure
Connect-AzAccount

# Obtain an access token for key vaults.
$keyVaultAccessToken = (Get-AzAccessToken -ResourceUrl https://vault.azure.net).Token  

# Connect to your database.
$serverName = "<servername>.database.windows.net"
$databaseName = "<DatabaseName>"
# Change the authentication method in the connection string, if needed.
$connStr = "Server = " + $serverName + "; Database = " + $databaseName + ";  Integrated Security = True"
$database = Get-SqlDatabase -ConnectionString $connStr

# Encrypt the selected columns (or re-encrypt, if they are already encrypted using keys/encrypt types, different than the specified keys/types.
$ces = @() 
$ces += New-SqlColumnEncryptionSettings -ColumnName "dbo.Employees.SSN" -EncryptionType "Randomized" -EncryptionKey "CEK" 
$ces += New-SqlColumnEncryptionSettings -ColumnName "dbo.Employees.Salary" -EncryptionType "Randomized" -EncryptionKey "CEK" 
Set-SqlColumnEncryption -InputObject $database -ColumnEncryptionSettings $ces -LogFileDirectory . -EnclaveAttestationProtocol "None" -KeyVaultAccessToken $keyVaultAccessToken
```

### Decrypt Columns - Example

The following example shows how to decrypt all columns that are currently encrypted in a database.


```PowerShell
# Import modules
Import-Module "SqlServer" -MinimumVersion 22.0.50
Import-Module Az.Accounts -MinimumVersion 2.2.0

#Connect to Azure
Connect-AzAccount

# Obtain an access token for key vaults.
$keyVaultAccessToken = (Get-AzAccessToken -ResourceUrl https://vault.azure.net).Token  

# Connect to your database.
$serverName = "<server name>"
$databaseName = "<database name>"
# Change the authentication method in the connection string, if needed.
$connStr = "Server = " + $serverName + "; Database = " + $databaseName + "; Integrated Security = True"
$database = Get-SqlDatabase -ConnectionString $connStr

# Find all encrypted columns, and create a SqlColumnEncryptionSetting object for each column.
$ces = @()
$tables = $database.Tables
for($i=0; $i -lt $tables.Count; $i++){
    $columns = $tables[$i].Columns
    for($j=0; $j -lt $columns.Count; $j++) {
        if($columns[$j].isEncrypted) {
            $threeColPartName = $tables[$i].Schema + "." + $tables[$i].Name + "." + $columns[$j].Name 
            $ces += New-SqlColumnEncryptionSettings -ColumnName $threeColPartName -EncryptionType "Plaintext" 
        }
    }
}

# Decrypt all columns.
Set-SqlColumnEncryption -ColumnEncryptionSettings $ces -InputObject $database -LogFileDirectory . -EnclaveAttestationProtocol "None" -KeyVaultAccessToken $keyVaultAccessToken
```

## Encrypt Columns using SGX enclaves

The below example demonstrates setting the target encryption configuration for a couple of columns. If either column isn't already encrypted, it will be encrypted. If any column is already encrypted using a different key and/or a different encryption type, it will be decrypted and then re-encrypted with the specified target key/type. To trigger in-place cryptographic operations using an enclave, the EnclaveAttestationProtocol and the EnclaveAttestationUrl parameters are required.

```PowerShell
# Import modules
Import-Module "SqlServer" -MinimumVersion 22.0.50
Import-Module Az.Accounts -MinimumVersion 2.2.0

#Connect to Azure
Connect-AzAccount

# Obtain an access token for key vaults.
$keyVaultAccessToken = (Get-AzAccessToken -ResourceUrl https://vault.azure.net).Token  

# Connect to your database.
$serverName = "<servername>.database.windows.net"
$databaseName = "<DatabaseName>"
# Change the authentication method in the connection string, if needed.
$connStr = "Server = " + $serverName + "; Database = " + $databaseName + ";  Integrated Security = True"
$database = Get-SqlDatabase -ConnectionString $connStr

# Encrypt the selected columns (or re-encrypt, if they are already encrypted using keys/encrypt types, different than the specified keys/types.
$ces = @() 
$ces += New-SqlColumnEncryptionSettings -ColumnName "dbo.Employees.SSN" -EncryptionType "Randomized" -EncryptionKey "CEK" 
$ces += New-SqlColumnEncryptionSettings -ColumnName "dbo.Employees.Salary" -EncryptionType "Randomized" -EncryptionKey "CEK" 
Set-SqlColumnEncryption -InputObject $database -ColumnEncryptionSettings $ces -LogFileDirectory . -EnclaveAttestationProtocol "AAS" -EnclaveAttestationURL "https://<attestationURL>"   -KeyVaultAccessToken $keyVaultAccessToken
```

### Decrypt Columns - Example

The following example shows how to decrypt all columns that are currently encrypted in a database.


```PowerShell
# Import modules
Import-Module "SqlServer" -MinimumVersion 22.0.50
Import-Module Az.Accounts -MinimumVersion 2.2.0

#Connect to Azure
Connect-AzAccount

# Obtain an access token for key vaults.
$keyVaultAccessToken = (Get-AzAccessToken -ResourceUrl https://vault.azure.net).Token  

# Connect to your database.
$serverName = "<server name>"
$databaseName = "<database name>"
# Change the authentication method in the connection string, if needed.
$connStr = "Server = " + $serverName + "; Database = " + $databaseName + "; Integrated Security = True"
$database = Get-SqlDatabase -ConnectionString $connStr

# Find all encrypted columns, and create a SqlColumnEncryptionSetting object for each column.
$ces = @()
$tables = $database.Tables
for($i=0; $i -lt $tables.Count; $i++){
    $columns = $tables[$i].Columns
    for($j=0; $j -lt $columns.Count; $j++) {
        if($columns[$j].isEncrypted) {
            $threeColPartName = $tables[$i].Schema + "." + $tables[$i].Name + "." + $columns[$j].Name 
            $ces += New-SqlColumnEncryptionSettings -ColumnName $threeColPartName -EncryptionType "Plaintext" 
        }
    }
}

# Decrypt all columns.
Set-SqlColumnEncryption -ColumnEncryptionSettings $ces -InputObject $database -LogFileDirectory . -EnclaveAttestationProtocol "AAS" -EnclaveAttestationURL "https://<attestationURL>" -KeyVaultAccessToken $keyVaultAccessToken
```
 
## Next steps
- [Develop applications using Always Encrypted with secure enclaves](always-encrypted-enclaves-client-development.md)

## See also  
 - [Always Encrypted with secure enclaves](../../../relational-databases/security/encryption/always-encrypted-enclaves.md)
 - [Manage keys for Always Encrypted with secure enclaves](always-encrypted-enclaves-manage-keys.md)
 - [Configure column encryption in-place with Transact-SQL](always-encrypted-enclaves-configure-encryption-tsql.md)
 - [Configure column encryption in-place with a DAC package](always-encrypted-enclaves-configure-encryption-dacpac.md)
 - [Configure column encryption in-place with the Always Encrypted wizard in SSMS](always-encrypted-wizard.md)
