---
title: "Provision Always Encrypted Keys Using PowerShell"
description: Learn how to provision keys for Always Encrypted using the SqlServer PowerShell module to provide control access to the encryption keys and the database.
author: Pietervanhove
ms.author: pivanho
ms.reviewer: vanto, mathoma
ms.date: 6/17/2026
ms.service: sql
ms.subservice: security
ms.topic: how-to
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
ms.custom:
  - devx-track-azurepowershell
  - sfi-ropc-nochange
---
# Provision Always Encrypted keys using PowerShell

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../../includes/applies-to-version/sql-asdb-asdbmi.md)]

This article provides the steps to provision keys for Always Encrypted using the [SqlServer PowerShell module](/powershell/sql-server/sql-server-powershell-provider). You can use PowerShell to provision Always Encrypted keys both [with and without role separation](../../../relational-databases/security/encryption/overview-of-key-management-for-always-encrypted.md#KeyManagementRoles), providing control over who has access to the actual encryption keys in the key store, and who has access to the database.

> [!NOTE]
> Microsoft recommends using PowerShell 7 or later when running Always Encrypted PowerShell scripts. PowerShell 7 provides improved cross-platform support, better performance, and the latest compatibility with the SqlServer module (v22+), which is required for many Always Encrypted scenarios.

For an overview of Always Encrypted key management, including some high-level best practice recommendations, see [Overview of key management for Always Encrypted](overview-of-key-management-for-always-encrypted.md).
For information about how to start using the SqlServer PowerShell module for Always Encrypted, see [Configure Always Encrypted using PowerShell](configure-always-encrypted-using-powershell.md).

## Key Provisioning without role separation

The key provisioning method described in this section doesn't support role separation between security administrators and DBAs. Some of the steps in this section combine operations on physical keys with operations on key metadata. Therefore, use this method of provisioning the keys if your organization uses the DevOps model, or if the database is hosted in the cloud and the primary goal is to restrict cloud administrators (but not on-premises DBAs) from accessing sensitive data. Don't use this method if potential adversaries include DBAs, or if DBAs shouldn't have access to sensitive data.

Before running any steps that involves access to plaintext keys or the key store (identified in the **Accesses plaintext keys/key store** column in the following table), make sure that the PowerShell environment runs on a secure machine that is different from a computer hosting your database. For more information, see [Security Considerations for Key Management](overview-of-key-management-for-always-encrypted.md#security-considerations-for-key-management).

Task |Article |Accesses plaintext keys/key store |Accesses database  
---------|---------|---------|---------
Step 1. Create a column master key in a key store.<br><br>**Note:** The SqlServer PowerShell module doesn't support this step. To accomplish this task from a command-line, use the tools that are specific to your selected key store. |[Create and store column master keys for Always Encrypted](create-and-store-column-master-keys-always-encrypted.md) | Yes | No  
Step 2. Start a PowerShell environment and import the SqlServer PowerShell module. |   [Configure Always Encrypted using PowerShell](configure-always-encrypted-using-powershell.md)   |    No    | No  
Step 3. Connect to your server and database.     |     [Connect to a database](../../../relational-databases/security/encryption/configure-always-encrypted-using-powershell.md#connectingtodatabase)    |    No     | Yes  
Step 4. Create a *SqlColumnMasterKeySettings* object that contains information about the location of your column master key. SqlColumnMasterKeySettings is an object that exists in memory (in PowerShell). Use the cmdlet that is specific to your key store.   |     [New-SqlAzureKeyVaultColumnMasterKeySettings](/powershell/sqlserver/sqlserver/vlatest/new-sqlazurekeyvaultcolumnmasterkeysettings)<br /> <br />[New-SqlCertificateStoreColumnMasterKeySettings](/powershell/sqlserver/sqlserver/vlatest/new-sqlcertificatestorecolumnmasterkeysettings)<br /> <br />[New-SqlCngColumnMasterKeySettings](/powershell/sqlserver/sqlserver/vlatest/new-sqlcngcolumnmasterkeysettings)<br /> <br />[New-SqlCspColumnMasterKeySettings](/powershell/sqlserver/sqlserver/vlatest/new-sqlcspcolumnmasterkeysettings)        |   No      | No  
Step 5. Create the metadata about the column master key in your database. <br /> <br /> **Note:** We don't verify the validity of the keys or certificates used to generate the column master key.      |    [New-SqlColumnMasterKey](/powershell/sqlserver/sqlserver/vlatest/new-sqlcolumnmasterkey<br /> <br />**Note:** under the covers, the cmdlet issues the [CREATE COLUMN MASTER KEY](../../../t-sql/statements/create-column-master-key-transact-sql.md) statement to create key metadata.|    No     |    Yes
Step 6. Authenticate to Azure, if your column master key is stored in Azure Key Vault. | [Connect-AzAccount](/powershell/module/az.accounts/connect-azaccount)    | Yes   | No
Step 7. Obtain an access token for Azure Key Vaults, if your column master key is stored in Azure Key Vault. | [Get-AzAccessToken](/powershell/module/az.accounts/get-azaccesstoken) | No | No  
Step 8. Generate a new column encryption key, encrypt it with the column master key and create column encryption key metadata in the database.     |    [New-SqlColumnEncryptionKey](/powershell/sqlserver/sqlserver/vlatest/new-sqlcolumnencryptionkey)<br /> <br />**Note:** Use a variation of the cmdlet that internally generates and encrypts a column encryption key.<br /> <br />**Note:** Under the covers, the cmdlet issues the [CREATE COLUMN ENCRYPTION KEY](../../../t-sql/statements/create-column-encryption-key-transact-sql.md) statement to create key metadata. | Yes | Yes

## Windows Certificate Store without role separation (example)

This script is an end-to-end example for generating a column master key that is a certificate in Windows Certificate Store, generating and encrypting a column encryption key, and creating key metadata in a SQL Server database.

```powershell
[CmdletBinding()]
param(
	[Parameter(Mandatory = $false)]
	[string]$DatabaseName = '<database name>',

	[Parameter(Mandatory = $false)]
	[string]$ServerName = "<server name>",

	[Parameter(Mandatory = $false)]
	[string]$CertificateSubject = "AlwaysEncryptedCert",

	[Parameter(Mandatory = $false)]
	[string]$CmkName = "CMK",

	[Parameter(Mandatory = $false)]
	[string]$CekName = "CEK"
)

Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop"

Import-Module SqlServer -MinimumVersion 22.0.50 -ErrorAction Stop

Write-Host "[AE] Locating certificate '$CertificateSubject' in CurrentUser\\My"
$cert = Get-ChildItem -Path Cert:CurrentUser\My |
	Where-Object { $_.Subject -eq "CN=$CertificateSubject" } |
	Sort-Object NotAfter -Descending |
	Select-Object -First 1

if (-not $cert) {
	Write-Host "[AE] Certificate not found. Creating self-signed certificate."
	$cert = New-SelfSignedCertificate `
		-Subject $CertificateSubject `
		-CertStoreLocation Cert:CurrentUser\My `
		-KeyExportPolicy Exportable `
		-Type DocumentEncryptionCert `
		-KeyUsage DataEncipherment `
		-KeySpec KeyExchange
}

Write-Host "[AE] Connecting to SQL Server '$ServerName' / Database '$DatabaseName'"
$connStr = "Server=$ServerName;Database=$DatabaseName;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30"

try {
	$database = Get-SqlDatabase -ConnectionString $connStr -ErrorAction Stop
}
catch {
	Write-Error "Failed to connect to '$ServerName' database '$DatabaseName'. Verify instance name SQL2025, database existence, and local permissions."
	throw
}

Write-Host "[AE] Creating CMK settings from certificate thumbprint"
$cmkSettings = New-SqlCertificateStoreColumnMasterKeySettings -CertificateStoreLocation "CurrentUser" -Thumbprint $cert.Thumbprint

Write-Host "[AE] Ensuring CMK '$CmkName' exists"
$existingCmk = Get-SqlColumnMasterKey -InputObject $database | Where-Object { $_.Name -eq $CmkName }
if (-not $existingCmk) {
	New-SqlColumnMasterKey -Name $CmkName -InputObject $database -ColumnMasterKeySettings $cmkSettings | Out-Null
}

Write-Host "[AE] Ensuring CEK '$CekName' exists"
$existingCek = Get-SqlColumnEncryptionKey -InputObject $database | Where-Object { $_.Name -eq $CekName }
if (-not $existingCek) {
	New-SqlColumnEncryptionKey -Name $CekName -InputObject $database -ColumnMasterKey $CmkName | Out-Null
}

Write-Host "Completed successfully"
```

## Azure Key Vault without role separation (example)

This script is an end-to-end example for provisioning and configuring a key vault in Azure Key Vault, generating a column master key in the vault, generating and encrypting a column encryption key, and creating key metadata in an Azure SQL database.

```powershell
param(
	[Parameter(Mandatory = $true)] [string]$SubscriptionId,
	[Parameter(Mandatory = $true)] [string]$ResourceGroupName,
	[Parameter(Mandatory = $true)] [string]$AzureLocation,
	[Parameter(Mandatory = $true)] [string]$KeyVaultName,
	[Parameter(Mandatory = $true)] [string]$KeyName,
	[Parameter(Mandatory = $true)] [string]$ServerName,
	[Parameter(Mandatory = $true)] [string]$DatabaseName,
	[string]$CmkName = "CMK",
	[string]$CekName = "CEK",
	[bool]$AssignRbacToCurrentPrincipal = $true
)

Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop"

Import-Module Az.Accounts -ErrorAction Stop
Import-Module Az.Resources -ErrorAction Stop
Import-Module Az.KeyVault -ErrorAction Stop
Import-Module SqlServer -ErrorAction Stop

function Get-CurrentPrincipalObjectId {
	param([string]$AccountId)

	$userSignedIn = Get-AzADUser -SignedIn -ErrorAction SilentlyContinue
	if ($userSignedIn) { return $userSignedIn.Id }

	$user = Get-AzADUser -UserPrincipalName $AccountId -ErrorAction SilentlyContinue
	if ($user) { return $user.Id }

	$sp = Get-AzADServicePrincipal -DisplayName $AccountId -ErrorAction SilentlyContinue | Select-Object -First 1
	if ($sp) { return $sp.Id }

	throw "Could not resolve Microsoft Entra object id for account '$AccountId'."
}

try {
	Write-Host "[AE] Signing in and selecting subscription"
	Connect-AzAccount | Out-Null
	$ctx = Set-AzContext -SubscriptionId $SubscriptionId

	Write-Host "[AE] Ensuring resource group exists"
	$resourceGroup = Get-AzResourceGroup -Name $ResourceGroupName -ErrorAction SilentlyContinue
	if (-not $resourceGroup) {
		$resourceGroup = New-AzResourceGroup -Name $ResourceGroupName -Location $AzureLocation
	}

	Write-Host "[AE] Ensuring key vault exists (RBAC mode)"
	$vault = Get-AzKeyVault -VaultName $KeyVaultName -ResourceGroupName $ResourceGroupName -ErrorAction SilentlyContinue
	if (-not $vault) {
		$vault = New-AzKeyVault -VaultName $KeyVaultName -ResourceGroupName $ResourceGroupName -Location $AzureLocation -EnableRbacAuthorization
	}

	if (-not $vault.EnableRbacAuthorization) {
		throw "Key Vault '$KeyVaultName' is not using RBAC authorization. Enable RBAC authorization on the vault before running this script."
	}

	if ($AssignRbacToCurrentPrincipal) {
		Write-Host "[AE] Ensuring RBAC role assignment"
		$principalSignInName = $ctx.Account.Id
		$roleName = "Key Vault Crypto Officer"
		$existingRole = Get-AzRoleAssignment -SignInName $principalSignInName -Scope $vault.ResourceId -RoleDefinitionName $roleName -ErrorAction SilentlyContinue
		if (-not $existingRole) {
			New-AzRoleAssignment -SignInName $principalSignInName -Scope $vault.ResourceId -RoleDefinitionName $roleName | Out-Null
		}
	}

	Write-Host "[AE] Ensuring column master key material exists in Key Vault"
	$akvKey = Get-AzKeyVaultKey -VaultName $KeyVaultName -Name $KeyName -ErrorAction SilentlyContinue
	if (-not $akvKey) {
		$akvKey = Add-AzKeyVaultKey -VaultName $KeyVaultName -Name $KeyName -Destination "Software"
	}

	Write-Host "[AE] Connecting to Azure SQL and creating metadata"
	$keyVaultAccessToken = (Get-AzAccessToken -ResourceUrl "https://vault.azure.net").Token
	$connStr = "Server=tcp:$ServerName.database.windows.net,1433;Database=$DatabaseName;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;Authentication=Active Directory Interactive"
	$database = Get-SqlDatabase -ConnectionString $connStr -Encrypt Mandatory
	$cmkSettings = New-SqlAzureKeyVaultColumnMasterKeySettings -KeyUrl $akvKey.Key.Kid

	$existingCmk = Get-SqlColumnMasterKey -InputObject $database | Where-Object { $_.Name -eq $CmkName }
	if (-not $existingCmk) {
		New-SqlColumnMasterKey -Name $CmkName -InputObject $database -ColumnMasterKeySettings $cmkSettings | Out-Null
	}

	$existingCek = Get-SqlColumnEncryptionKey -InputObject $database | Where-Object { $_.Name -eq $CekName }
	if (-not $existingCek) {
		New-SqlColumnEncryptionKey -Name $CekName -InputObject $database -ColumnMasterKey $CmkName -KeyVaultAccessToken $keyVaultAccessToken | Out-Null
	}

	Write-Host "Completed successfully"
}
catch {
	Write-Error "Script failed: $($_.Exception.Message)"
	throw
}
```

## CNG/KSP without role separation (example)

The below script is an end-to-end example for generating a column master key in a key store that implements Cryptography Next Generation API (CNG), generating and encrypting a column encryption key, and creating key metadata in a SQL Server database.

The example uses the key store that uses Microsoft Software Key Storage Provider. You can choose to modify the example to use another store, such as your hardware security module. For that, you'll need to make sure the key store provider (KSP) that implements CNG for your device is installed and properly on your machine. You'll need to replace `Microsoft Software Key Storage Provider` with your device's KSP name.

```powershell
[CmdletBinding()]
param(
	[Parameter(Mandatory = $false)]
	[string]$ServerName = "<server name>",

	[Parameter(Mandatory = $true)]
	[string]$DatabaseName = "<database name>",

	[Parameter(Mandatory = $false)]
	[string]$CngKeyName = "AlwaysEncryptedKey",

	[Parameter(Mandatory = $false)]
	[string]$CmkName = "CMK",

	[Parameter(Mandatory = $false)]
	[string]$CekName = "CEK"
)

Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop"

# Local key store provider and key settings.
$cngProviderName = "Microsoft Software Key Storage Provider"
$cngAlgorithmName = "RSA"
$cngKeySize = 2048

Import-Module SqlServer -ErrorAction Stop

Write-Host "[AE] Creating local CNG key '$CngKeyName'"
$cngProvider = New-Object System.Security.Cryptography.CngProvider($cngProviderName)
$cngKeyParams = New-Object System.Security.Cryptography.CngKeyCreationParameters
$cngKeyParams.Provider = $cngProvider
$cngKeyParams.KeyCreationOptions = [System.Security.Cryptography.CngKeyCreationOptions]::OverwriteExistingKey
$keySizeProperty = New-Object System.Security.Cryptography.CngProperty(
	"Length",
	[System.BitConverter]::GetBytes($cngKeySize),
	[System.Security.Cryptography.CngPropertyOptions]::None
)
$cngKeyParams.Parameters.Add($keySizeProperty)
$cngAlgorithm = New-Object System.Security.Cryptography.CngAlgorithm($cngAlgorithmName)
[System.Security.Cryptography.CngKey]::Create($cngAlgorithm, $CngKeyName, $cngKeyParams) | Out-Null

Write-Host "[AE] Connecting to $ServerName / $DatabaseName"
$connStr = "Server=$ServerName;Database=$DatabaseName;Integrated Security=True;Encrypt=True;TrustServerCertificate=True"
$database = Get-SqlDatabase -ConnectionString $connStr

Write-Host "[AE] Preparing column master key settings"
$cmkSettings = New-SqlCngColumnMasterKeySettings -CngProviderName $cngProviderName -KeyName $CngKeyName

Write-Host "[AE] Ensuring CMK exists"
$existingCmk = Get-SqlColumnMasterKey -InputObject $database | Where-Object { $_.Name -eq $CmkName }
if (-not $existingCmk) {
	New-SqlColumnMasterKey -Name $CmkName -InputObject $database -ColumnMasterKeySettings $cmkSettings | Out-Null
}

Write-Host "[AE] Ensuring CEK exists"
$existingCek = Get-SqlColumnEncryptionKey -InputObject $database | Where-Object { $_.Name -eq $CekName }
if (-not $existingCek) {
	New-SqlColumnEncryptionKey -Name $CekName -InputObject $database -ColumnMasterKey $CmkName | Out-Null
}

Write-Host "Completed successfully"
```

## Key provisioning with role separation

This section provides the steps to configure encryption where security administrators don't have access to the database, and database administrators don't have access to the key store or plaintext keys.

### Security administrator

Before running any steps that involves access to plaintext keys or the key store (identified in the **Accesses plaintext keys/key store** column in the following table), make sure that:
- The PowerShell environment runs on a secure machine that is different from a computer hosting your database.
- DBAs in your organization have no access to the machine (that would defeat the purpose of role separation).

For more information, see [Security Considerations for Key Management](overview-of-key-management-for-always-encrypted.md#security-considerations-for-key-management).

Task |Article |Accesses plaintext keys/key store |Accesses database  
---------|---------|---------|---------
Step 1. Create a column master key in a key store.<br><br>**Note:** The SqlServer module doesn't support this step. To accomplish this task from a command-line, you need to use the tools that are specific the type of your key store.     | [Create and store column master keys for Always Encrypted](../../../relational-databases/security/encryption/create-and-store-column-master-keys-always-encrypted.md) |    Yes    | No  
Step 2. Start a PowerShell session and import the SqlServer module.      |     [Import the SqlServer module](../../../relational-databases/security/encryption/configure-always-encrypted-using-powershell.md#importsqlservermodule)     | No | No  
Step 3. Create a *SqlColumnMasterKeySettings* object that contains information about the location of your column master key. *SqlColumnMasterKeySettings* is an object that exists in memory (in PowerShell). Use the cmdlet that is specific to your key store. |      [New-SqlAzureKeyVaultColumnMasterKeySettings](/powershell/sqlserver/sqlserver/vlatest/new-sqlazurekeyvaultcolumnmasterkeysettings)<br><br>[New-SqlCertificateStoreColumnMasterKeySettings](/powershell/sqlserver/sqlserver/vlatest/new-sqlcertificatestorecolumnmasterkeysettings)<br><br>[New-SqlCngColumnMasterKeySettings](/powershell/sqlserver/sqlserver/vlatest/new-sqlcngcolumnmasterkeysettings)<br><br>[New-SqlCspColumnMasterKeySettings](/powershell/sqlserver/sqlserver/vlatest/new-sqlcspcolumnmasterkeysettings)   | No         | No  
Step 4. Authenticate to Azure, if your column master key is stored in Azure Key Vault. | [Connect-AzAccount](/powershell/module/az.accounts/connect-azaccount)    | Yes   | No
Step 5. Obtain an access token for Azure Key Vaults, if your column master key is stored in Azure Key Vault. | [Get-AzAccessToken](/powershell/module/az.accounts/get-azaccesstoken) | No | No  
Step 6. Generate a column encryption key, encrypt it with the column master key to produce an encrypted value of the column encryption key.     |   [New-SqlColumnEncryptionKeyEncryptedValue](/powershell/sqlserver/sqlserver/vlatest/new-sqlcolumnencryptionkeyencryptedvalue)     |    Yes    | No  
Step 7. Provide the location of the column master key (the provider name and a key path of the column master key) and an encrypted value of the column encryption key to the DBA. | See the examples at the end of the article.        |   No      | No

### DBA

DBAs use the information they receive from the Security Admin (step 7 above) to create and manage the Always Encrypted key metadata in the database.

Task |Article |Accesses plaintext keys |Accesses database  
---------|---------|---------|---------
Step 1. Obtain the location of the column master key and encrypted value of the column encryption key from your Security Administrator. |See the examples at the end of the article. | No | No
Step 2. Start a PowerShell environment and import the SqlServer module. | [Configure Always Encrypted using PowerShell](../../../relational-databases/security/encryption/configure-always-encrypted-using-powershell.md) | No | No
Step 3. Connect to your server and a database. | [Connect to a database](../../../relational-databases/security/encryption/configure-always-encrypted-using-powershell.md#connectingtodatabase) | No | Yes
Step 4. Create a SqlColumnMasterKeySettings object that contains information about the location of your column master key. SqlColumnMasterKeySettings is an object that exists in memory. | New-SqlColumnMasterKeySettings | No | No
Step 5. Create the metadata about the column master key in your database. <br /> <br /> **Note:** We don't verify the validity of the keys or certificates used to generate the column master key. | [New-SqlColumnMasterKey](/powershell/sqlserver/sqlserver/vlatest/new-sqlcolumnmasterkey)<br>**Note:** under the covers, the cmdlet issues the [CREATE COLUMN MASTER KEY (Transact-SQL)](../../../t-sql/statements/create-column-master-key-transact-sql.md) statement to create column master key metadata. | No | Yes
Step 6. Create the column encryption key metadata in the database. | New-SqlColumnEncryptionKey<br>**Note:** DBAs use a variation of the cmdlet that only creates column encryption key metadata.<br>Under the covers, the cmdlet issues the [CREATE COLUMN ENCRYPTION KEY (Transact-SQL)](../../../t-sql/statements/create-column-encryption-key-transact-sql.md) statement to create column encryption key metadata. | No | Yes

## Windows Certificate Store with role separation (example)

### Security administrator

```powershell
[CmdletBinding()]
param(
	[Parameter(Mandatory = $false)]
	[string]$ServerName = '<server name>',

	[Parameter(Mandatory = $false)]
	[ValidateNotNullOrEmpty()]
	[string]$DatabaseName = '<database name>',

	[Parameter(Mandatory = $false)]
	[string]$CertificateSubject = 'AlwaysEncryptedCert',

	[Parameter(Mandatory = $false)]
	[string]$CmkName = 'CMK1',

	[Parameter(Mandatory = $false)]
	[string]$CekName = 'CEK1',

	[Parameter(Mandatory = $false)]
	[string]$ExportKeyDataPath = 'C:\temp\keydata.txt'
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

Import-Module SqlServer -MinimumVersion 22.0.50 -ErrorAction Stop

Write-Host "[AE] Finding certificate '$CertificateSubject' in CurrentUser\\My"
$cert = Get-ChildItem -Path 'Cert:CurrentUser\My' |
	Where-Object { $_.Subject -eq "CN=$CertificateSubject" } |
	Sort-Object NotAfter -Descending |
	Select-Object -First 1

if (-not $cert) {
	Write-Host '[AE] Certificate not found. Creating a new self-signed certificate.'
	$cert = New-SelfSignedCertificate `
		-Subject $CertificateSubject `
		-CertStoreLocation 'Cert:CurrentUser\My' `
		-KeyExportPolicy Exportable `
		-Type DocumentEncryptionCert `
		-KeyUsage DataEncipherment `
		-KeySpec KeyExchange
}

Write-Host "[AE] Connecting to SQL Server '$ServerName' / Database '$DatabaseName'"
$connStr = "Server=$ServerName;Database=$DatabaseName;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30"

try {
	$database = Get-SqlDatabase -ConnectionString $connStr -ErrorAction Stop
}
catch {
	Write-Error "Failed to connect to '$ServerName' / '$DatabaseName'. Verify instance name, database, and local permissions."
	throw
}

Write-Host '[AE] Building CMK settings from certificate'
$cmkSettings = New-SqlCertificateStoreColumnMasterKeySettings -CertificateStoreLocation 'CurrentUser' -Thumbprint $cert.Thumbprint

Write-Host "[AE] Ensuring CMK '$CmkName' exists"
$existingCmk = Get-SqlColumnMasterKey -InputObject $database | Where-Object { $_.Name -eq $CmkName }
if (-not $existingCmk) {
	New-SqlColumnMasterKey -Name $CmkName -InputObject $database -ColumnMasterKeySettings $cmkSettings | Out-Null
}

Write-Host "[AE] Ensuring CEK '$CekName' exists"
$existingCek = Get-SqlColumnEncryptionKey -InputObject $database | Where-Object { $_.Name -eq $CekName }
if (-not $existingCek) {
	New-SqlColumnEncryptionKey -Name $CekName -InputObject $database -ColumnMasterKey $CmkName | Out-Null
}

if ($ExportKeyDataPath) {
	Write-Host "[AE] Exporting key metadata to '$ExportKeyDataPath'"
	$encryptedValue = New-SqlColumnEncryptionKeyEncryptedValue -TargetColumnMasterKeySettings $cmkSettings
	"KeyStoreProviderName,KeyPath,EncryptedValue" | Set-Content -Path $ExportKeyDataPath -Encoding UTF8
	"$($cmkSettings.KeyStoreProviderName),$($cmkSettings.KeyPath),$encryptedValue" | Add-Content -Path $ExportKeyDataPath -Encoding UTF8
}

Write-Host 'Completed successfully'
```

### DBA

```powershell
[CmdletBinding()]
param(
	[Parameter(Mandatory = $false)]
	[string]$ServerName = 'localhost\SQL2025',

	[Parameter(Mandatory = $false)]
	[ValidateNotNullOrEmpty()]
	[string]$DatabaseName = 'AdventureWorks2025',

	[Parameter(Mandatory = $false)]
	[ValidateNotNullOrEmpty()]
	[string]$KeyDataFile = 'C:\temp\keydata.txt',

	[Parameter(Mandatory = $false)]
	[ValidateNotNullOrEmpty()]
	[string]$CmkName = 'CMK1',

	[Parameter(Mandatory = $false)]
	[ValidateNotNullOrEmpty()]
	[string]$CekName = 'CEK1'
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

Import-Module SqlServer -MinimumVersion 22.0.50 -ErrorAction Stop

if (-not (Test-Path -Path $KeyDataFile -PathType Leaf)) {
	throw "Key data file not found: $KeyDataFile"
}

Write-Host "[AE] Loading key metadata from '$KeyDataFile'"
$keyData = Import-Csv -Path $KeyDataFile
if (-not $keyData) {
	throw "Key data file '$KeyDataFile' is empty."
}

$keyDataRow = $keyData | Select-Object -First 1
if (-not $keyDataRow.KeyStoreProviderName -or -not $keyDataRow.KeyPath -or -not $keyDataRow.EncryptedValue) {
	throw "Key data file must include non-empty columns: KeyStoreProviderName, KeyPath, EncryptedValue."
}

Write-Host "[AE] Connecting to SQL Server '$ServerName' / Database '$DatabaseName'"
$connStr = "Server=$ServerName;Database=$DatabaseName;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30"

try {
	$database = Get-SqlDatabase -ConnectionString $connStr -ErrorAction Stop
}
catch {
	Write-Error "Failed to connect to '$ServerName' / '$DatabaseName'. Verify instance name, database, and local permissions."
	throw
}

Write-Host "[AE] Building CMK settings for provider '$($keyDataRow.KeyStoreProviderName)'"
$cmkSettings = New-SqlColumnMasterKeySettings -KeyStoreProviderName $keyDataRow.KeyStoreProviderName -KeyPath $keyDataRow.KeyPath

Write-Host "[AE] Ensuring CMK '$CmkName' exists"
$existingCmk = Get-SqlColumnMasterKey -InputObject $database | Where-Object { $_.Name -eq $CmkName }
if (-not $existingCmk) {
	New-SqlColumnMasterKey -Name $CmkName -InputObject $database -ColumnMasterKeySettings $cmkSettings | Out-Null
}

Write-Host "[AE] Ensuring CEK '$CekName' exists"
$existingCek = Get-SqlColumnEncryptionKey -InputObject $database | Where-Object { $_.Name -eq $CekName }
if (-not $existingCek) {
	New-SqlColumnEncryptionKey -Name $CekName -InputObject $database -ColumnMasterKey $CmkName -EncryptedValue $keyDataRow.EncryptedValue | Out-Null
}

Write-Host 'Completed successfully'
```

## Related content

- [Configure column encryption using Always Encrypted with PowerShell](configure-column-encryption-using-powershell.md)
- [Rotate Always Encrypted keys using PowerShell](rotate-always-encrypted-keys-using-powershell.md)
- [Develop applications using Always Encrypted](always-encrypted-client-development.md)
- [Always Encrypted](always-encrypted-database-engine.md)
- [Overview of key management for Always Encrypted](overview-of-key-management-for-always-encrypted.md)
- [Create and store column master keys for Always Encrypted](create-and-store-column-master-keys-always-encrypted.md)
- [Configure Always Encrypted using PowerShell](configure-always-encrypted-using-powershell.md)
- [Provision Always Encrypted keys using SQL Server Management Studio](configure-always-encrypted-keys-using-ssms.md)
- [CREATE COLUMN MASTER KEY (Transact-SQL)](../../../t-sql/statements/create-column-master-key-transact-sql.md)
- [DROP COLUMN MASTER KEY (Transact-SQL)](../../../t-sql/statements/drop-column-master-key-transact-sql.md)
- [CREATE COLUMN ENCRYPTION KEY (Transact-SQL)](../../../t-sql/statements/create-column-encryption-key-transact-sql.md)
- [ALTER COLUMN ENCRYPTION KEY (Transact-SQL)](../../../t-sql/statements/alter-column-encryption-key-transact-sql.md)
- [DROP COLUMN ENCRYPTION KEY (Transact-SQL)](../../../t-sql/statements/drop-column-encryption-key-transact-sql.md)
- [sys.column_master_keys (Transact-SQL)](../../system-catalog-views/sys-column-master-keys-transact-sql.md)
- [sys.column_encryption_keys (Transact-SQL)](../../system-catalog-views/sys-column-encryption-keys-transact-sql.md)
