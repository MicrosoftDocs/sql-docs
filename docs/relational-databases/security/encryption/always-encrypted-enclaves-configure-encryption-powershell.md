---
title: "Configure column encryption using Always Encrypted with secure enclaves with PowerShell"
description: "Configure column encryption using Always Encrypted with secure enclaves with PowerShell"
author: Pietervanhove
ms.author: pivanho
ms.reviewer: vanto
ms.date: "06/17/2026"
ms.service: sql
ms.subservice: security
ms.topic: how-to
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
ms.custom:
  - devx-track-azurepowershell
  - sfi-ropc-nochange
---
# Configure column encryption in-place with PowerShell
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../../includes/applies-to-version/sql-asdb-asdbmi.md)]

This article provides the steps for setting the target Always Encrypted configuration for database columns using the [Set-SqlColumnEncryption](/powershell/sqlserver/sqlserver/vlatest/set-sqlcolumnencryption) cmdlet (in the *SqlServer* PowerShell module). The **Set-SqlColumnEncryption** cmdlet modifies both the schema of the target database and the data stored in the selected columns. The data stored in a column can be encrypted, re-encrypted, or decrypted, depending on the specified target encryption settings for the columns and the current encryption configuration. To trigger in-place cryptographic operations using an enclave, **Set-SqlColumnEncryption** must use a database connection created using a connection string with the Attestation Protocol and optionally the Attestation URL keywords.

## Prerequisites

To set the target encryption configuration, you need to make sure:
- an enclave-enabled column encryption key is configured in the database (if you're encrypting or re-encrypting a column). For details, see [Manage keys for Always Encrypted with secure enclaves](../../../relational-databases/security/encryption/always-encrypted-enclaves-manage-keys.md).
- you're connected to the database with Always Encrypted enabled and the attestation properties specified in the connection string.
- you can access the column master key for each column you want to encrypt, re-encrypt, or decrypt, from the computer running the PowerShell cmdlets.
- you use SqlServer PowerShell module version 22.0.50 or later. For in-place online encryption use SqlServer PowerShell module version 22.3.0 or later.

> [!NOTE]
> Microsoft recommends using PowerShell 7 or later when running Always Encrypted PowerShell scripts. PowerShell 7 provides improved cross-platform support, better performance, and the latest compatibility with the SqlServer module (v22+), which is required for many Always Encrypted scenarios.

## Security Considerations

The **Set-SqlColumnEncryption** cmdlet, used to configure encryption for database columns, handles both Always Encrypted keys and the data stored in database columns. Therefore, it's important you run the cmdlet on a secure computer. If your database is in SQL Server, execute the cmdlet from a different computer than the computer hosting your SQL Server instance. Because the primary goal of Always Encrypted is to ensure encrypted sensitive data is safe even if the database system gets compromised, executing a PowerShell script that processes keys and/or sensitive data on the SQL Server computer can reduce or defeat the benefits of the feature.

Task  |Article  |Accesses plaintext keys/key store  |Accesses database   
---|---|---|---
Step 1. Start a PowerShell environment and import the SqlServer module. | [Import the SqlServer module](../../../relational-databases/security/encryption/configure-always-encrypted-using-powershell.md#importsqlservermodule) | No | No
Step 2. Connect to your server and database | [Connecting to a database](../../../relational-databases/security/encryption/configure-always-encrypted-using-powershell.md#connectingtodatabase) | No | Yes
Step 3. Authenticate to Azure, if your column master key (protecting the column encryption key, to be rotated), is stored in Azure Key Vault | [Connect-AzAccount](/powershell/module/az.accounts/connect-azaccount) | Yes | No
Step 4. Obtain an access token for Azure Key Vaults. | [Get-AzAccessToken](/powershell/module/az.accounts/get-azaccesstoken) | No | No
Step 5. Construct an array of SqlColumnEncryptionSettings objects - one for each database column, you want to encrypt, re-encrypt, or decrypt. SqlColumnMasterKeySettings is an object that exists in memory (in PowerShell). It specifies the target encryption scheme for a column. | [New-SqlColumnEncryptionSettings](/powershell/sqlserver/sqlserver/vlatest/new-sqlcolumnencryptionsettings) | No | No
| Step 6. Set the desired encryption configuration, specified in the array of SqlColumnMasterKeySettings objects, you created in the previous step. A column is encrypted, re-encrypted, or decrypted, depending on the specified target settings and the current encryption configuration of the column.| [Set-SqlColumnEncryption](/powershell/sqlserver/sqlserver/vlatest/set-sqlcolumnencryption)<br><br>**Note:** This step might take a long time. Your applications can't access the tables through the entire operation or a portion of it, depending on the approach (online vs. offline), you select. | Yes | Yes |

## Encrypt columns by using VBS enclaves

The below example demonstrates setting the target encryption configuration for a couple of columns. If either column isn't already encrypted, it will be encrypted. If any column is already encrypted using a different key and/or a different encryption type, it will be decrypted and then re-encrypted with the specified target key/type. VBS enclaves currently don't support attestation. The EnclaveAttestationProtocol parameter should be set to *None* and the EnclaveAttestationUrl isn't required.


```PowerShell
# Import modules
Import-Module SqlServer
Import-Module Az.Accounts

# Edit these values.
$serverName = '<your-server>.database.windows.net'
$databaseName = 'ContosoHR'
$cekName = 'CEK'
$subscriptionId = '<your-subscription-id>'

# Columns to encrypt with the CEK.
$columnsToEncrypt = @(
    'dbo.Employees.SSN',
    'dbo.Employees.Salary'
)

# Sign in with Microsoft Entra and select subscription.
Connect-AzAccount
Set-AzContext -SubscriptionId $subscriptionId

# Token needed when CEK uses Azure Key Vault CMK.
$keyVaultAccessToken = (Get-AzAccessToken -ResourceUrl 'https://vault.azure.net').Token

# Connect to Azure SQL Database using Entra auth.
$connStr = "Server=tcp:$serverName,1433;Database=$databaseName;Encrypt=True;TrustServerCertificate=False;Authentication=Active Directory Interactive"
$database = Get-SqlDatabase -ConnectionString $connStr

# Build encryption settings for target columns.
$columnEncryptionSettings = @(
    $columnsToEncrypt | ForEach-Object {
        New-SqlColumnEncryptionSettings -ColumnName $_ -EncryptionType Randomized -EncryptionKey $cekName
    }
)

# Encrypt or re-encrypt the columns.
Set-SqlColumnEncryption -InputObject $database -ColumnEncryptionSettings $columnEncryptionSettings -EnclaveAttestationProtocol None -LogFileDirectory . -KeyVaultAccessToken $keyVaultAccessToken

Write-Host 'Done.'
```

### Decrypt columns - Example

The following example shows how to decrypt all columns that are currently encrypted in a database.


```PowerShell
# Import modules
Import-Module SqlServer -MinimumVersion 22.0.50
Import-Module Az.Accounts -MinimumVersion 2.2.0

#Connect to Azure
Connect-AzAccount

# Obtain an access token for key vaults.
$keyVaultAccessToken = (Get-AzAccessToken -ResourceUrl https://vault.azure.net).Token  

# Connect to your database.
$serverName = "<server name>"
$databaseName = "<database name>"
# Connect to Azure SQL Database using Entra auth.
$connStr = "Server=tcp:$serverName,1433;Database=$databaseName;Encrypt=True;TrustServerCertificate=False;Authentication=Active Directory Interactive"
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

## Encrypt columns by using SGX enclaves

The below example demonstrates setting the target encryption configuration for a couple of columns. If either column isn't already encrypted, it will be encrypted. If any column is already encrypted using a different key and/or a different encryption type, it will be decrypted and then re-encrypted with the specified target key/type. To trigger in-place cryptographic operations using an enclave, the EnclaveAttestationProtocol and the EnclaveAttestationUrl parameters are required.

```PowerShell
# Import modules
Import-Module SqlServer
Import-Module Az.Accounts

# Edit these values.
$serverName = '<your-server>.database.windows.net'
$databaseName = 'ContosoHR'
$cekName = 'CEK'
$subscriptionId = '<your-subscription-id>'

# Columns to encrypt with the CEK.
$columnsToEncrypt = @(
    'dbo.Employees.SSN',
    'dbo.Employees.Salary'
)

# Sign in with Microsoft Entra and select subscription.
Connect-AzAccount
Set-AzContext -SubscriptionId $subscriptionId

# Token needed when CEK uses Azure Key Vault CMK.
$keyVaultAccessToken = (Get-AzAccessToken -ResourceUrl 'https://vault.azure.net').Token

# Connect to Azure SQL Database using Entra auth.
$connStr = "Server=tcp:$serverName,1433;Database=$databaseName;Encrypt=True;TrustServerCertificate=False;Authentication=Active Directory Interactive"
$database = Get-SqlDatabase -ConnectionString $connStr

# Build encryption settings for target columns.
$columnEncryptionSettings = @(
    $columnsToEncrypt | ForEach-Object {
        New-SqlColumnEncryptionSettings -ColumnName $_ -EncryptionType Randomized -EncryptionKey $cekName
    }
)
# Encrypt or re-encrypt the columns.
Set-SqlColumnEncryption -InputObject $database -ColumnEncryptionSettings $columnEncryptionSettings -EnclaveAttestationProtocol "AAS" -EnclaveAttestationURL "https://<attestationURL>"   -KeyVaultAccessToken $keyVaultAccessToken -LogFileDirectory .

Write-Host 'Done.' 
```

### Decrypt columns - Example

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
# Connect to Azure SQL Database using Entra auth.
$connStr = "Server=tcp:$serverName,1433;Database=$databaseName;Encrypt=True;TrustServerCertificate=False;Authentication=Active Directory Interactive"
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
 
## Next step

> [!div class="nextstepaction"]
> [Develop applications using Always Encrypted with secure enclaves](always-encrypted-enclaves-client-development.md)

## Related content

- [Always Encrypted with secure enclaves](always-encrypted-enclaves.md)
- [Manage keys for Always Encrypted with secure enclaves](always-encrypted-enclaves-manage-keys.md)
- [Configure column encryption in-place with Transact-SQL](always-encrypted-enclaves-configure-encryption-tsql.md)
- [Configure column encryption in-place with DAC package](always-encrypted-enclaves-configure-encryption-dacpac.md)
- [Configure column encryption using Always Encrypted Wizard](always-encrypted-wizard.md)
