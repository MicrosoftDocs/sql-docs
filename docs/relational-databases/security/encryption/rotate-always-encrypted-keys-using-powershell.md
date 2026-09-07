---
title: "Rotate Always Encrypted keys using PowerShell"
description: "Rotate Always Encrypted keys using PowerShell"
author: VanMSFT
ms.author: vanto
ms.reviewer: vanto
ms.date: 6/17/2026
ms.service: sql
ms.subservice: security
ms.topic: how-to
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
ms.custom: sfi-ropc-nochange
---
# Rotate Always Encrypted keys using PowerShell

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../../includes/applies-to-version/sql-asdb-asdbmi.md)]

This article provides the steps to rotate keys for Always Encrypted using the SqlServer PowerShell module. For information about how to start using the SqlServer PowerShell module for Always Encrypted, see [Configure Always Encrypted using PowerShell](../../../relational-databases/security/encryption/configure-always-encrypted-using-powershell.md).

> [!NOTE]
> Microsoft recommends using PowerShell 7 or later when running Always Encrypted PowerShell scripts. PowerShell 7 provides improved cross-platform support, better performance, and the latest compatibility with the SqlServer module (v22+), which is required for many Always Encrypted scenarios.

Rotating Always Encrypted keys is the process of replacing an existing key with a new one. You might need to rotate a key if it's compromised, or to comply with your organization's policies or compliance regulations that mandate regular cryptographic key rotation.

Always Encrypted uses two types of keys, so there are two high-level key rotation workflows; rotating column master keys, and rotating column encryption keys.

* **Column encryption key rotation** - involves decrypting data that is encrypted with the current key, and re-encrypting the data using the new column encryption key. Because rotating a column encryption key requires access to both the keys and the database, column encryption key rotation can only be performed without role separation.
* **Column master key rotation** - involves decrypting column encryption keys that are protected with the current column master key, re-encrypting them using the new column master key, and updating the metadata for both types of keys. Column master key rotation can be completed with or without role separation (when using the SqlServer PowerShell module).

## Column Master Key Rotation without Role Separation

The method described in this section for rotating a column master key doesn't support role separation between a security administrator and a DBA. Some of the following steps combine operations on the physical keys with operations on key metadata. Use this workflow if your organization uses the DevOps model, or when your database is hosted in the cloud and the primary goal is to restrict cloud administrators (but not on-premises DBAs) from accessing sensitive data. Don't use this method if potential adversaries include DBAs, or if DBAs shouldn't have access to sensitive data.  

| Task | Article | Accesses plaintext keys/keystore| Accesses database
|:---|:---|:---|:---
|Step 1. Create a new column master key in a key store. <br /> <br />**Note:** The SqlServer PowerShell module doesn't support this step. To accomplish this task from the command-line, you need to use tools that are specific for your key store. When using Azure Key Vault as the key store, multitenant customer managed key rotation is not supported. Ensure that the new customer managed key is in the same tenant as the existing one.| [Create and store column master keys for Always Encrypted](../../../relational-databases/security/encryption/create-and-store-column-master-keys-always-encrypted.md)| Yes | No
|Step 2. Start a PowerShell environment and import the SqlServer module | [Import the SqlServer module](../../../relational-databases/security/encryption/configure-always-encrypted-using-powershell.md#importsqlservermodule) | No | No
|Step 3. Connect to your server and database. | [Connecting to a database](../../../relational-databases/security/encryption/configure-always-encrypted-using-powershell.md#connectingtodatabase) | No | Yes
|Step 4. Create a SqlColumnMasterKeySettings object that contains information about the location of your new column master key. SqlColumnMasterKeySettings is an object that exists in memory (in PowerShell). To create it, you need to use the cmdlet that is specific to your key store. |[New-SqlAzureKeyVaultColumnMasterKeySettings](/powershell/sqlserver/sqlserver/vlatest/new-sqlazurekeyvaultcolumnmasterkeysettings)<br><br>[New-SqlCertificateStoreColumnMasterKeySettings](/powershell/sqlserver/sqlserver/vlatest/new-sqlcertificatestorecolumnmasterkeysettings)<br><br>[New-SqlCngColumnMasterKeySettings](/powershell/sqlserver/sqlserver/vlatest/new-sqlcngcolumnmasterkeysettings)<br><br>[New-SqlCspColumnMasterKeySettings](/powershell/sqlserver/sqlserver/vlatest/new-sqlcspcolumnmasterkeysettings)<br> | No | No
|Step 5. Create the metadata about your new column master key in your database. | [New-SqlColumnMasterKey](/powershell/sqlserver/sqlserver/vlatest/new-sqlcolumnmasterkey)<br><br>**Note:** under the covers, this cmdlet issues the [CREATE COLUMN MASTER KEY (Transact-SQL)](../../../t-sql/statements/create-column-master-key-transact-sql.md) statement to create key metadata. | No | Yes
|Step 6. Authenticate to Azure, if your current column master key or your new column master key is stored in a key vault or a managed HSM in Azure Key Vault | [Connect-AzAccount](/powershell/module/az.accounts/connect-azaccount) | Yes | No
|Step 7. Obtain an access token for Azure Key Vaults, if your column master key is stored in Azure Key Vault. | [Get-AzAccessToken](/powershell/module/az.accounts/get-azaccesstoken) | No | No  
|Step 8. Start the rotation, by encrypting each of the column encryption keys, which is currently protected with the old column master key, using the new column master key. After this step each affected column encryption key (associated with the old column master key, being rotated), is encrypted with both the old and the new column master key, and has two encrypted values in the database metadata.| [Invoke-SqlColumnMasterKeyRotation](/powershell/sqlserver/sqlserver/vlatest/invoke-sqlcolumnmasterkeyrotation) | Yes | Yes
|Step 9. Coordinate with the administrators of all applications that query encrypted columns in the database (and are protected with the old column master key), so that they can ensure the applications can access the new column master key.| [Create and Store Column Master Keys (Always Encrypted)](../../../relational-databases/security/encryption/create-and-store-column-master-keys-always-encrypted.md) | Yes | No
|Step 10. Complete the rotation<br><br>**Note:** before executing this step, make sure all applications that query encrypted columns that are protected with the old column master key, have been configured to use the new column master key. If you perform this step prematurely, some of those applications may not be able to decrypt the data. Complete the rotation by removing the encrypted values from the database that were created with the old column master key. This removes the association between the old column master key and the column encryption keys it protects. |[Complete-SqlColumnMasterKeyRotation](/powershell/sqlserver/sqlserver/vlatest/complete-sqlcolumnmasterkeyrotation)| No | Yes
|Step 11. Remove the metadata from the old column master key. |[Remove-SqlColumnMasterKey](/powershell/sqlserver/sqlserver/vlatest/remove-sqlcolumnmasterkey)| No | Yes

> [!NOTE]
> It is highly recommended you do not permanently delete the old column master key after the rotation. Instead, you should keep the old column master key in its current key store or archive it in another secure place. If you restore your database from a backup file to a point in time *before* the new column master key was configured, you will need the old key to access the data.

### Rotating a Column Master Key without Role Separation (Windows Certificate Example)

The below script is an end-to-end example that replaces an existing column master key (CMK1) with a new column master key (CMK2).

```powershell
[CmdletBinding()]
param(
	[Parameter(Mandatory = $false)]
	[string]$ServerName = '<server name>',

	[Parameter(Mandatory = $false)]
	[ValidateNotNullOrEmpty()]
	[string]$DatabaseName = '<database name>',

	[Parameter(Mandatory = $false)]
	[string]$CertificateSubject = 'AlwaysEncryptedCertNew',

	[Parameter(Mandatory = $false)]
	[ValidateNotNullOrEmpty()]
	[string]$OldCmkName = 'CMK1',

	[Parameter(Mandatory = $false)]
	[ValidateNotNullOrEmpty()]
	[string]$NewCmkName = 'CMK2'
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

Import-Module SqlServer -MinimumVersion 22.0.50 -ErrorAction Stop

Write-Host '[AE] Step 1: Creating a new self-signed certificate for the new CMK'
$cert = New-SelfSignedCertificate `
	-Subject $CertificateSubject `
	-CertStoreLocation 'Cert:CurrentUser\My' `
	-KeyExportPolicy Exportable `
	-Type DocumentEncryptionCert `
	-KeyUsage KeyEncipherment `
	-KeySpec KeyExchange `
	-KeyLength 2048
Write-Host "[AE] Certificate created with thumbprint: $($cert.Thumbprint)"

Write-Host "[AE] Step 2: Connecting to SQL Server '$ServerName' / Database '$DatabaseName'"
$connStr = "Server=$ServerName;Database=$DatabaseName;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30"

try {
	$database = Get-SqlDatabase -ConnectionString $connStr -ErrorAction Stop
}
catch {
	Write-Error "Failed to connect to '$ServerName' / '$DatabaseName'. Verify instance, database, and local permissions."
	throw
}

Write-Host "[AE] Step 3: Validating that old CMK '$OldCmkName' exists"
$oldCmk = Get-SqlColumnMasterKey -InputObject $database | Where-Object { $_.Name -eq $OldCmkName }
if (-not $oldCmk) {
	throw "Old CMK '$OldCmkName' does not exist. Cannot rotate."
}
Write-Host "[AE] Old CMK '$OldCmkName' found."

Write-Host "[AE] Step 4: Creating CMK settings for new certificate"
$newCmkSettings = New-SqlCertificateStoreColumnMasterKeySettings -CertificateStoreLocation 'CurrentUser' -Thumbprint $cert.Thumbprint

Write-Host "[AE] Step 5: Registering new CMK '$NewCmkName' in the database"
$newCmk = Get-SqlColumnMasterKey -InputObject $database | Where-Object { $_.Name -eq $NewCmkName }
if ($newCmk) {
	Write-Host "[AE] New CMK '$NewCmkName' already exists. Skipping creation."
}
else {
	New-SqlColumnMasterKey -Name $NewCmkName -InputObject $database -ColumnMasterKeySettings $newCmkSettings | Out-Null
	Write-Host "[AE] New CMK '$NewCmkName' registered."
}

Write-Host "[AE] Step 6: Initiating CMK rotation from '$OldCmkName' to '$NewCmkName'"
Write-Host "[AE] (This re-encrypts all associated CEKs under the new CMK...)"
Invoke-SqlColumnMasterKeyRotation `
	-SourceColumnMasterKeyName $OldCmkName `
	-TargetColumnMasterKeyName $NewCmkName `
	-InputObject $database
Write-Host "[AE] Rotation initiated."

Write-Host "[AE] Step 7: Completing the CMK rotation"
Complete-SqlColumnMasterKeyRotation `
	-SourceColumnMasterKeyName $OldCmkName `
	-InputObject $database
Write-Host "[AE] Rotation completed."

Write-Host "[AE] Step 8: Verifying CEKs are now under '$NewCmkName'"
$query = "SELECT name FROM sys.column_encryption_keys WHERE name = N'$($NewCmkName)'"
$rotatedCeks = Invoke-SqlCmd -ServerInstance $ServerName -Database $DatabaseName -Query $query -TrustServerCertificate  -ErrorAction SilentlyContinue
if ($rotatedCeks) {
	$cekCount = @($rotatedCeks).Count
	if ($cekCount -eq 0) { $cekCount = 1 }
	Write-Host "[AE] Verified: $cekCount CEK(s) now under '$NewCmkName'"
	@($rotatedCeks) | ForEach-Object { Write-Host "  - $($_.name)" }
}

Write-Host "[AE] Step 9: Removing old CMK metadata '$OldCmkName'"
Remove-SqlColumnMasterKey -Name $OldCmkName -InputObject $database
Write-Host "[AE] Old CMK '$OldCmkName' removed."

Write-Host '[AE] ========== Rotation Complete =========='
Write-Host "[AE] Old CMK: $OldCmkName (deleted)"
Write-Host "[AE] New CMK: $NewCmkName (active)"
Write-Host '[AE] All CEKs have been re-encrypted under the new CMK.'
```

## Column Master Key Rotation with Role Separation

The column master key rotation workflow described in this section ensures the separation between a Security Administrator and a DBA.

> [!IMPORTANT]
> Before executing any steps where *Accesses plaintext keys/keystore*=**Yes** in the table below (steps that access plaintext keys or the key store), make sure that the PowerShell environment runs on a secure machine that is different from a computer hosting your database. For more information, see [Security Considerations for Key Management](overview-of-key-management-for-always-encrypted.md#security-considerations-for-key-management).

### Part 1: DBA

A DBA retrieves metadata about the column master key to be rotated, and about the affected column encryption keys, which associated with the current column master key. The DBA shares all this information with a Security Administrator.


| Task | Article | Accesses plaintext keys/keystore| Accesses database
|:---|:---|:---|:---
|Step 1. Start a PowerShell environment and import the SqlServer module. | [Import the SqlServer module](../../../relational-databases/security/encryption/configure-always-encrypted-using-powershell.md#importsqlservermodule) | No | None
|Step 2. Connect to your server and a database. | [Connect to a database](../../../relational-databases/security/encryption/configure-always-encrypted-using-powershell.md#connectingtodatabase) | No | Yes
|Step 3. Retrieve the metadata about old column master key.| [Get-SqlColumnMasterKey](/powershell/sqlserver/sqlserver/vlatest/get-sqlcolumnmasterkey) | No | Yes
|Step 4. Retrieve the metadata about column encryption keys, protected by the old column master key, including their encrypted values. | [Get-SqlColumnEncryptionKey](/powershell/sqlserver/sqlserver/vlatest/get-sqlcolumnencryptionkey) | No | Yes
|Step 5. Share the location of the column master key (the provider name and a key path of the column master key) and the encrypted values of the corresponding column encryption keys, protected with the old column master key.| See the examples below. | No | No

### Part 2: Security Administrator

The Security Administrator generates a new column master key, re-encrypts the affected column encryption keys with the new column master key, and shares the information about the new column master key as well as the set of new encrypted values for the affected column encryption keys, with the DBA.

| Task | Article | Access plaintext keys/keystore| Accesses database
|:---|:---|:---|:---
|Step 1. Obtain the location of the old column master key and the encrypted values of the corresponding column encryption keys, protected with the old column master key, from your DBA.|N/A<br>See the examples below.|No| No
|Step 2. Create a new column master key in a key store.  <br /> <br />**Note:** The SqlServer module doesn't support this step. To accomplish this task from a command-line, you need to use the tools that are specific the type of your key store. When using Azure Key Vault as the key store, multitenant customer managed key rotation is not supported. Ensure that the new customer managed key is in the same tenant as the existing one.|[Create and store column master keys for Always Encrypted](../../../relational-databases/security/encryption/create-and-store-column-master-keys-always-encrypted.md)| Yes | No
|Step 3. Start a PowerShell environment and import the SqlServer module. | [Import the SqlServer module](../../../relational-databases/security/encryption/configure-always-encrypted-using-powershell.md#importsqlservermodule) | No | No
|Step 4. Create a SqlColumnMasterKeySettings object that contains information about the location of your **old** column master key. SqlColumnMasterKeySettings is an object that exists in memory (in PowerShell). |New-SqlColumnMasterKeySettings| No | No
|Step 5. Create a SqlColumnMasterKeySettings object that contains information about the location of your **new** column master key. SqlColumnMasterKeySettings is an object that exists in memory (in PowerShell). To create it, you need to use the cmdlet that is specific to your key store. | [New-SqlAzureKeyVaultColumnMasterKeySettings](/powershell/sqlserver/sqlserver/vlatest/new-sqlazurekeyvaultcolumnmasterkeysettings)<br><br>[New-SqlCertificateStoreColumnMasterKeySettings](/powershell/sqlserver/sqlserver/vlatest/new-sqlcertificatestorecolumnmasterkeysettings)<br><br>[New-SqlCngColumnMasterKeySettings](/powershell/sqlserver/sqlserver/vlatest/new-sqlcngcolumnmasterkeysettings)<br><br>[New-SqlCspColumnMasterKeySettings](/powershell/sqlserver/sqlserver/vlatest/new-sqlcspcolumnmasterkeysettings)| No | No
|Step 6. Authenticate to Azure, if your old (current) column master key or your new column master key is stored in a key vault or a managed HSM in Azure Key Vault. | [Connect-AzAccount](/powershell/module/az.accounts/connect-azaccount) | Yes | No
|Step 7. Obtain an access token for Azure Key Vaults, if your column master key is stored in Azure Key Vault. | [Get-AzAccessToken](/powershell/module/az.accounts/get-azaccesstoken) | No | No  
|Step 8. Re-encrypt each value of the column encryption key, which is currently protected with the old column master key, using the new column master key. | [New-SqlColumnEncryptionKeyEncryptedValue](/powershell/sqlserver/sqlserver/vlatest/new-sqlcolumnencryptionkeyencryptedvalue)<br><br>**Note:** When calling this cmdlet, pass the SqlColumnMasterKeySettings objects for both the old and the new column master key, along with a value of the column encryption key, to be re-encrypted.|Yes|No
|Step 9. Share the location of the new column master key (the provider name and a key path of the column master key) and the set of new encrypted values of the column encryption keys, with your DBA.| See the examples below. | No | No

> [!NOTE]
> It is highly recommended you do not permanently delete the old column master key after the rotation. Instead, you should keep the old column master key in its current key store or archive it in another secure place. If you restore your database from a backup file to a point in time *before* the new column master key was configured, you will need the old key to access the data.

### Part 3: DBA

The DBA creates metadata for the new column master key and updates the metadata of the affected column encryption keys, to add the new set of encrypted values. In this step, the DBA also coordinates with the administrators of the applications querying encryption columns, who ensure the application can access the new column master key. Once all applications are set up to use the new column master key, the DBA removes the old set of encrypted values and the old column master key metadata.

| Task | Article | Access plaintext keys/keystore| Accesses database
|:---|:---|:---|:---
|Step 1. Obtain the location of the new column master key and the new set of encrypted values of the corresponding column encryption keys, protected with the old column master key, from your Security Administrator.| See the examples below. | No | No
|Step 2. Start a PowerShell environment and import the SqlServer module. | [Import the SqlServer module](../../../relational-databases/security/encryption/configure-always-encrypted-using-powershell.md#importsqlservermodule) | No | No
|Step 3. Connect to your server and a database. | [Connecting to a database](../../../relational-databases/security/encryption/configure-always-encrypted-using-powershell.md#connectingtodatabase) | No | Yes
|Step 4. Create a SqlColumnMasterKeySettings object that contains information about the location of your new column master key. SqlColumnMasterKeySettings is an object that exists in memory (in PowerShell). |New-SqlColumnMasterKeySettings| No| No
|Step 5. Create the metadata about your new column master key in your database.|[New-SqlColumnMasterKey](/powershell/sqlserver/sqlserver/vlatest/new-sqlcolumnmasterkey)<br><br>**Note:** Under the covers this cmdlet issues the [CREATE COLUMN MASTER KEY (Transact-SQL)](../../../t-sql/statements/create-column-master-key-transact-sql.md) statement to create key metadata. | No | Yes
|Step 6. Retrieve the metadata about column encryption keys, protected by the old column master key.| [Get-SqlColumnEncryptionKey](/powershell/sqlserver/sqlserver/vlatest/get-sqlcolumnencryptionkey)| No | Yes
|Step 7. Add a new encrypted value (produced using the new column master key) to the metadata for each affected column encryption key.|[Add-SqlColumnEncryptionKeyValue](/powershell/sqlserver/sqlserver/vlatest/add-sqlcolumnencryptionkeyvalue)|No|Yes
|Step 8. Coordinate with the administrators of all applications that query encrypted columns in the database (and are protected with the old column master key), so that they can ensure the applications can access the new column master key.|[Creating and Storing Column Master Keys (Always Encrypted)](../../../relational-databases/security/encryption/create-and-store-column-master-keys-always-encrypted.md)| No|No
|Step 9. Complete the rotation, by removing the encrypted values associated with the old column master key from the database.<br><br>**Note:** Before executing this step, make sure all applications that query encrypted columns that are protected with the old column master key, have been configured to use the new column master key. If you perform this step prematurely, some of those applications may not be able to decrypt the data.<br><br>This step removes an association between the old column master key and the column encryption keys it protects. | [Complete-SqlColumnMasterKeyRotation](/powershell/sqlserver/sqlserver/vlatest/complete-sqlcolumnmasterkeyrotation)<br><br>Alternatively, you can use [Remove-SqlColumnEncryptionKeyValue](/powershell/sqlserver/sqlserver/vlatest/remove-sqlcolumnencryptionkeyvalue) | No|Yes
|Step 10. Remove the old column master key metadata from the database| [Remove-SqlColumnMasterKey](/powershell/sqlserver/sqlserver/vlatest/remove-sqlcolumnmasterkey)| No|Yes

### Rotating a Column Master Key with Role Separation (Windows Certificate Example)

The below script is an end-to-end example for generating a new column master key that is certificate in Windows Certificate store, rotating an existing (current) column master key, to replace it with the new column master key. The script assumes, the target database contains the column master key, named CMK1 (to be rotated), which encrypts some column encryption keys.

Part 1: DBA

```powershell
[CmdletBinding()]
param(
	[Parameter(Mandatory = $false)]
	[ValidateNotNullOrEmpty()]
	[string]$ServerName = '<server name>',

	[Parameter(Mandatory = $false)]
	[ValidateNotNullOrEmpty()]
	[string]$DatabaseName = '<database name>',

	[Parameter(Mandatory = $false)]
	[ValidateNotNullOrEmpty()]
	[string]$OldCmkName = 'CMK2',

	[Parameter(Mandatory = $false)]
	[ValidateNotNullOrEmpty()]
	[string]$OutputFolder = 'C:\temp'
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

Import-Module SqlServer -MinimumVersion 22.0.50 -ErrorAction Stop

Write-Host "[CEK Export] Starting CMK and CEK data export"

# Validate output folder
if (-not (Test-Path -Path $OutputFolder -PathType Container)) {
	Write-Host "[CEK Export] Creating output folder: $OutputFolder"
	New-Item -Path $OutputFolder -ItemType Directory | Out-Null
}

# Connect to database
Write-Host "[CEK Export] Connecting to '$ServerName' / '$DatabaseName'"
$connStr = "Server=$ServerName;Database=$DatabaseName;Integrated Security=True;TrustServerCertificate=True;Connection Timeout=30"

try {
	$database = Get-SqlDatabase -ConnectionString $connStr -ErrorAction Stop
}
catch {
	Write-Error "Failed to connect to '$ServerName' / '$DatabaseName'."
	throw
}

# Retrieve old CMK
Write-Host "[CEK Export] Retrieving CMK '$OldCmkName'"
$oldCmk = Get-SqlColumnMasterKey -InputObject $database | Where-Object { $_.Name -eq $OldCmkName }
if (-not $oldCmk) {
	throw "CMK '$OldCmkName' not found in database '$DatabaseName'."
}

# Export CMK metadata using fixed text file name
$cmkFile = Join-Path $OutputFolder "oldcmkdata.txt"
Write-Host "[CEK Export] Exporting CMK metadata to: $cmkFile"
"CMKName|KeyStoreProviderName|KeyPath" | Set-Content -Path $cmkFile -Encoding UTF8
"$OldCmkName|$($oldCmk.KeyStoreProviderName)|$($oldCmk.KeyPath)" | Add-Content -Path $cmkFile -Encoding UTF8
Write-Host "[CEK Export]   ✓ CMK metadata exported"

# Discover and export CEKs using fixed text file name
Write-Host "[CEK Export] Discovering CEKs associated with '$OldCmkName'"
$ceks = Get-SqlColumnEncryptionKey -InputObject $database
$cekFile = Join-Path $OutputFolder "oldcekvalues.txt"
"CEKName|CEKEncryptedValue|HasMultipleEncryptedValues" | Set-Content -Path $cekFile -Encoding UTF8

$exportedCount = 0
$multiValueCount = 0

foreach ($cek in $ceks) {
	if (-not $cek.ColumnEncryptionKeyValues) {
		continue
	}

	# Check if this CEK has multiple encrypted values
	if ($cek.ColumnEncryptionKeyValues.Count -gt 1) {
		# CEK has multiple encrypted values - check if any reference the old CMK
		$refersToOldCmk = $cek.ColumnEncryptionKeyValues | Where-Object { $_.ColumnMasterKeyName -eq $OldCmkName }
		if ($refersToOldCmk) {
			Write-Warning "CEK '$($cek.Name)' has $($cek.ColumnEncryptionKeyValues.Count) encrypted values. One references '$OldCmkName'. This CEK cannot be rotated automatically."
			"$($cek.Name)|MULTIPLE_ENCRYPTED_VALUES|True" | Add-Content -Path $cekFile -Encoding UTF8
			$multiValueCount++
		}
	}
	else {
		# CEK has single encrypted value - check if it references the old CMK
		if ($cek.ColumnEncryptionKeyValues[0].ColumnMasterKeyName -eq $OldCmkName) {
			$encryptedValueHex = "0x" + -join ($cek.ColumnEncryptionKeyValues[0].EncryptedValue | ForEach-Object { $_.ToString("X2") })
			"$($cek.Name)|$encryptedValueHex|False" | Add-Content -Path $cekFile -Encoding UTF8
			$exportedCount++
		}
	}
}

Write-Host "[CEK Export]   ✓ CEK encrypted values exported"
Write-Host "[CEK Export]     - Exported: $exportedCount CEK(s)"
if ($multiValueCount -gt 0) {
	Write-Warning "      - Multi-valued CEKs (manual review needed): $multiValueCount"
}

Write-Host "[CEK Export] ===== Export Complete ====="
Write-Host "[CEK Export] CMK Metadata:   $cmkFile"
Write-Host "[CEK Export] CEK Values:     $cekFile"
```


Part 2: Security Administrator

```powershell
[CmdletBinding()]
param(
    [Parameter(Mandatory = $false)]
    [ValidateNotNullOrEmpty()]
    [string]$ShareFolder = 'C:\Temp\',

    [Parameter(Mandatory = $false)]
    [ValidateSet('CurrentUser', 'LocalMachine')]
    [string]$StoreLocation = 'CurrentUser',

    [Parameter(Mandatory = $false)]
    [ValidateNotNullOrEmpty()]
    [string]$CertificateSubject = 'AlwaysEncryptedCert'
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

Import-Module SqlServer -MinimumVersion 22.0.50 -ErrorAction Stop

function Import-DelimitedTextFile {
    param(
        [Parameter(Mandatory = $true)] [string]$Path,
        [Parameter(Mandatory = $true)] [string[]]$RequiredColumns
    )

    if (-not (Test-Path -Path $Path -PathType Leaf)) {
        throw "Required file not found: $Path"
    }

    $raw = Get-Content -Path $Path -Raw
    if ([string]::IsNullOrWhiteSpace($raw)) {
        throw "File is empty: $Path"
    }

    $delimiter = if ($raw -match '\|') { '|' } else { ',' }
    $rows = @(Import-Csv -Path $Path -Delimiter $delimiter)
    if ($rows.Count -eq 0) {
        throw "No data rows found in file: $Path"
    }

    $first = $rows[0]
    $RequiredColumns | ForEach-Object {
        if (-not $first.PSObject.Properties[$_]) {
            throw "Missing required column '$_' in file: $Path"
        }
    }

    return $rows
}

if (-not (Test-Path -Path $ShareFolder -PathType Container)) {
    throw "Share folder does not exist: $ShareFolder"
}

$oldCmkDataFile = Join-Path $ShareFolder 'oldcmkdata.txt'
$oldCekValuesFile = Join-Path $ShareFolder 'oldcekvalues.txt'
$newCmkDataFile = Join-Path $ShareFolder 'newcmkdata.txt'
$newCekValuesFile = Join-Path $ShareFolder 'newcekvalues.txt'

Write-Host "[AE] Reading old CMK data from '$oldCmkDataFile'"
$oldCmkDataRows = Import-DelimitedTextFile -Path $oldCmkDataFile -RequiredColumns @('KeyStoreProviderName', 'KeyPath')
$oldCmkData = $oldCmkDataRows[0]

Write-Host "[AE] Reading old CEK values from '$oldCekValuesFile'"
$oldCekValues = Import-DelimitedTextFile -Path $oldCekValuesFile -RequiredColumns @('CEKName', 'CEKEncryptedValue')

Write-Host "[AE] Finding or creating certificate '$CertificateSubject' in $StoreLocation\\My"
$certPath = "Cert:$StoreLocation\My"
$cert = Get-ChildItem -Path $certPath |
    Where-Object { $_.Subject -eq "CN=$CertificateSubject" } |
    Sort-Object NotAfter -Descending |
    Select-Object -First 1

if (-not $cert) {
    $cert = New-SelfSignedCertificate `
        -Subject $CertificateSubject `
        -CertStoreLocation $certPath `
        -KeyExportPolicy Exportable `
        -Type DocumentEncryptionCert `
        -KeyUsage DataEncipherment `
        -KeySpec KeyExchange
}

Write-Host '[AE] Building CMK settings'
$oldCmkSettings = New-SqlColumnMasterKeySettings `
    -KeyStoreProviderName $oldCmkData.KeyStoreProviderName `
    -KeyPath $oldCmkData.KeyPath

$newCmkSettings = New-SqlCertificateStoreColumnMasterKeySettings `
    -CertificateStoreLocation $StoreLocation `
    -Thumbprint $cert.Thumbprint

Write-Host "[AE] Re-encrypting CEK values and writing '$newCekValuesFile'"
"CEKName|CEKEncryptedValue" | Set-Content -Path $newCekValuesFile -Encoding UTF8

$oldCekValues | ForEach-Object {
    $newValue = New-SqlColumnEncryptionKeyEncryptedValue `
        -TargetColumnMasterKeySettings $newCmkSettings `
        -ColumnMasterKeySettings $oldCmkSettings `
        -EncryptedValue $_.CEKEncryptedValue

    "$($_.CEKName)|$newValue" | Add-Content -Path $newCekValuesFile -Encoding UTF8
}

Write-Host "[AE] Writing new CMK data to '$newCmkDataFile'"
"KeyStoreProviderName|KeyPath" | Set-Content -Path $newCmkDataFile -Encoding UTF8
"$($newCmkSettings.KeyStoreProviderName)|$($newCmkSettings.KeyPath)" | Add-Content -Path $newCmkDataFile -Encoding UTF8

Write-Host '[AE] Completed successfully'
Write-Host "[AE] Output files: $newCmkDataFile , $newCekValuesFile"
```


Part 3: DBA

```powershell
[CmdletBinding()]
param(
    [Parameter(Mandatory = $false)]
    [ValidateNotNullOrEmpty()]
    [string]$ServerName = '<server name>',

    [Parameter(Mandatory = $false)]
    [ValidateNotNullOrEmpty()]
    [string]$DatabaseName = '<database name>',

    [Parameter(Mandatory = $false)]
    [ValidateNotNullOrEmpty()]
    [string]$OldCmkName = 'CMK1',

    [Parameter(Mandatory = $false)]
    [ValidateNotNullOrEmpty()]
    [string]$NewCmkName = 'CMK2',

    [Parameter(Mandatory = $false)]
    [ValidateNotNullOrEmpty()]
    [string]$InputFolder = 'C:\temp'
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

Import-Module SqlServer -MinimumVersion 22.0.50 -ErrorAction Stop

function Import-DelimitedTextFile {
    param(
        [Parameter(Mandatory = $true)] [string]$Path,
        [Parameter(Mandatory = $true)] [string[]]$RequiredColumns
    )

    if (-not (Test-Path -Path $Path -PathType Leaf)) {
        throw "Required file not found: $Path"
    }

    $raw = Get-Content -Path $Path -Raw
    if ([string]::IsNullOrWhiteSpace($raw)) {
        throw "File is empty: $Path"
    }

    $delimiter = if ($raw -match '\|') { '|' } else { ',' }
    $rows = @(Import-Csv -Path $Path -Delimiter $delimiter)
    if ($rows.Count -eq 0) {
        throw "No data rows found in file: $Path"
    }

    $first = $rows[0]
    $RequiredColumns | ForEach-Object {
        if (-not $first.PSObject.Properties[$_]) {
            throw "Missing required column '$_' in file: $Path"
        }
    }

    return $rows
}

if (-not (Test-Path -Path $InputFolder -PathType Container)) {
    throw "Input folder not found: $InputFolder"
}

$newCmkDataFile = Join-Path $InputFolder 'newcmkdata.txt'
$newCekValuesFile = Join-Path $InputFolder 'newcekvalues.txt'

Write-Host "[AE] Reading new CMK data from '$newCmkDataFile'"
$newCmkRows = Import-DelimitedTextFile -Path $newCmkDataFile -RequiredColumns @('KeyStoreProviderName', 'KeyPath')
$newCmkData = $newCmkRows[0]

Write-Host "[AE] Reading new CEK values from '$newCekValuesFile'"
$newCekValues = Import-DelimitedTextFile -Path $newCekValuesFile -RequiredColumns @('CEKName', 'CEKEncryptedValue')

Write-Host "[AE] Connecting to '$ServerName' / '$DatabaseName'"
$connStr = "Server=$ServerName;Database=$DatabaseName;Integrated Security=True;TrustServerCertificate=True;Connection Timeout=30"
$database = Get-SqlDatabase -ConnectionString $connStr -ErrorAction Stop

Write-Host "[AE] Ensuring target CMK '$NewCmkName' exists"
$newCmkSettings = New-SqlColumnMasterKeySettings -KeyStoreProviderName $newCmkData.KeyStoreProviderName -KeyPath $newCmkData.KeyPath
$existingNewCmk = Get-SqlColumnMasterKey -InputObject $database | Where-Object { $_.Name -eq $NewCmkName }
if (-not $existingNewCmk) {
    New-SqlColumnMasterKey -Name $NewCmkName -InputObject $database -ColumnMasterKeySettings $newCmkSettings | Out-Null
}

Write-Host "[AE] Adding new encrypted CEK values under '$NewCmkName'"
$ceks = Get-SqlColumnEncryptionKey -InputObject $database

$ceksToRotate = @(
    $ceks | Where-Object {
        $_.ColumnEncryptionKeyValues -and
        @($_.ColumnEncryptionKeyValues | Where-Object { $_.ColumnMasterKeyName -eq $OldCmkName }).Count -gt 0
    }
)

$ceksToRotate | ForEach-Object {
    $cek = $_
    if (@($cek.ColumnEncryptionKeyValues).Count -gt 1) {
        throw "CEK '$($cek.Name)' already has multiple encrypted values and still references '$OldCmkName'."
    }

    $newValueRow = @($newCekValues | Where-Object { $_.CEKName -eq $cek.Name }) | Select-Object -First 1
    if (-not $newValueRow) {
        throw "No new encrypted value found for CEK '$($cek.Name)' in file '$newCekValuesFile'."
    }

    Add-SqlColumnEncryptionKeyValue `
        -ColumnMasterKeyName $NewCmkName `
        -Name $cek.Name `
        -EncryptedValue $newValueRow.CEKEncryptedValue `
        -InputObject $database | Out-Null
}

Write-Host "[AE] Completing rotation for source CMK '$OldCmkName'"
Complete-SqlColumnMasterKeyRotation -SourceColumnMasterKeyName $OldCmkName -InputObject $database

Write-Host "[AE] Removing source CMK '$OldCmkName' metadata"
Remove-SqlColumnMasterKey -Name $OldCmkName -InputObject $database

Write-Host '[AE] Completed successfully'
```

## Rotating a Column Encryption Key

Rotating a column encryption key involves decrypting the data in all columns, encrypted with the key to be rotated, and re-encrypting the data using the new column encryption key. This rotation workflow requires access to both the keys and the database, and therefore can't be performed with role separation. 
Rotating a column encryption key can take a long time, if the tables containing columns encrypted with the key, being rotated, are large. Therefore, your organization needs to plan a column encryption key rotation carefully.

You can rotate a column encryption key using an offline or an online approach. The former method is likely to be faster, but your applications can't write to the affected tables. The latter approach will likely take longer, but you can limit the time interval, during which the affected tables aren't available to applications. For more information, see [Configure column encryption using Always Encrypted with PowerShell](configure-column-encryption-using-powershell.md)  and [Set-SqlColumnEncryption](/powershell/module/sqlserver/set-sqlcolumnencryption/).

| Task | Article | Accesses plaintext keys/keystore| Accesses database
|:---|:---|:---|:---
|Step 1. Start a PowerShell environment and import the SqlServer module. | [Import the SqlServer module](../../../relational-databases/security/encryption/configure-always-encrypted-using-powershell.md#importsqlservermodule) | No | No
|Step 2. Connect to your server and a database. | [Connecting to a database](../../../relational-databases/security/encryption/configure-always-encrypted-using-powershell.md#connectingtodatabase) | No | Yes
|Step 3. Authenticate to Azure, if your column master key (protecting the column encryption key, to be rotated), is stored in a key vault or a managed HSM in Azure Key Vault. | [Connect-AzAccount](/powershell/module/az.accounts/connect-azaccount) | Yes | No
|Step 4. Obtain an access token for Azure Key Vaults, if your column master key is stored in Azure Key Vault. | [Get-AzAccessToken](/powershell/module/az.accounts/get-azaccesstoken) | No | No
|Step 5. Generate a new column encryption key, encrypt it with the column master key and create column encryption key metadata in the database.  | [New-SqlColumnEncryptionKey](/powershell/sqlserver/sqlserver/vlatest/new-sqlcolumnencryptionkey)<br><br>**Note:** Use a variation of the cmdlet that internally generates and encrypts a column encryption key.<br>Under the covers this cmdlet issues the [CREATE COLUMN ENCRYPTION KEY (Transact-SQL)](../../../t-sql/statements/create-column-encryption-key-transact-sql.md) statement to create the key metadata. | Yes | Yes
|Step 6. Find all columns encrypted with the old column encryption key. | [SQL Server Management Objects (SMO) Programming Guide](../../../relational-databases/server-management-objects-smo/sql-server-management-objects-smo-programming-guide.md) | No | Yes
|Step 7. Create a *SqlColumnEncryptionSettings* object for each affected column.  SqlColumnEncryptionSettings is an object that exists in memory (in PowerShell). It specifies the target encryption scheme for a column. In this case, the object should specify the affected column should be encrypted using the new column encryption key. | [New-SqlColumnEncryptionSettings](/powershell/sqlserver/sqlserver/vlatest/new-sqlcolumnencryptionsettings) | No | No
|Step 8. Re-encrypt the columns, identified in step 5, using the new column encryption key. | [Set-SqlColumnEncryption](/powershell/sqlserver/sqlserver/vlatest/set-sqlcolumnencryption)<br><br>**Note:** This step may take a long time. Your applications won't be able to access the tables through the entire operation or a portion of it, depending on the approach (online vs. offline), you select. | Yes | Yes
|Step 9. Remove the metadata for the old column encryption key. | [Remove-SqlColumnEncryptionKey](/powershell/sqlserver/sqlserver/vlatest/remove-sqlcolumnencryptionkey) | No | Yes

### Example - Rotating a Column Encryption Key

The below script demonstrates rotating a column encryption key.  The script assumes, the target database contains some columns encrypted with a column encryption key, named CEK1 (to be rotated), which is protected using a column master key, named CMK1 (the column master key isn't stored in Azure Key Vault). 


```powershell
[CmdletBinding()]
param(
    [Parameter(Mandatory = $false)]
    [ValidateNotNullOrEmpty()]
    [string]$ServerName = '<server name>',

    [Parameter(Mandatory = $false)]
    [ValidateNotNullOrEmpty()]
    [string]$DatabaseName = '<database name>',

    [Parameter(Mandatory = $false)]
    [ValidateNotNullOrEmpty()]
    [string]$OldCekName = 'CEK1',

    [Parameter(Mandatory = $false)]
    [ValidateNotNullOrEmpty()]
    [string]$NewCekName = 'CEK2',

    [Parameter(Mandatory = $false)]
    [ValidateNotNullOrEmpty()]
    [string]$CmkName = 'CMK2',

    [Parameter(Mandatory = $false)]
    [ValidateRange(0, 3600)]
    [int]$MaxDowntimeInSeconds = 120,

    [Parameter(Mandatory = $false)]
    [ValidateNotNullOrEmpty()]
    [string]$LogFileDirectory = '.'
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

Import-Module SqlServer -MinimumVersion 22.0.50 -ErrorAction Stop

if ($OldCekName -eq $NewCekName) {
    throw 'OldCekName and NewCekName must be different.'
}

if (-not (Test-Path -Path $LogFileDirectory -PathType Container)) {
    New-Item -Path $LogFileDirectory -ItemType Directory | Out-Null
}

Write-Host "[AE] Connecting to '$ServerName' / '$DatabaseName'"
$connStr = "Server=$ServerName;Database=$DatabaseName;Integrated Security=True;TrustServerCertificate=True;Connection Timeout=30"
$database = Get-SqlDatabase -ConnectionString $connStr -ErrorAction Stop

Write-Host "[AE] Ensuring CMK '$CmkName' exists"
$cmk = Get-SqlColumnMasterKey -InputObject $database | Where-Object { $_.Name -eq $CmkName }
if (-not $cmk) {
    throw "Column master key '$CmkName' was not found."
}

Write-Host "[AE] Ensuring target CEK '$NewCekName' exists"
$existingNewCek = Get-SqlColumnEncryptionKey -InputObject $database | Where-Object { $_.Name -eq $NewCekName }
if (-not $existingNewCek) {
    New-SqlColumnEncryptionKey -Name $NewCekName -InputObject $database -ColumnMasterKey $CmkName | Out-Null
}

Write-Host "[AE] Discovering encrypted columns using '$OldCekName'"
$settings = @()
$tables = @($database.Tables)
$tables | ForEach-Object {
    $table = $_
    @($table.Columns) | ForEach-Object {
        $column = $_
        if ($column.IsEncrypted -and $column.ColumnEncryptionKeyName -eq $OldCekName) {
            $columnName = "{0}.{1}.{2}" -f $table.Schema, $table.Name, $column.Name
            $settings += New-SqlColumnEncryptionSettings -ColumnName $columnName -EncryptionType $column.EncryptionType -EncryptionKey $NewCekName
        }
    }
}

if ($settings.Count -eq 0) {
    Write-Warning "No encrypted columns found that reference '$OldCekName'. Nothing to rotate."
    return
}

Write-Host "[AE] Re-encrypting $($settings.Count) column(s) to '$NewCekName'"
Set-SqlColumnEncryption `
    -ColumnEncryptionSettings $settings `
    -InputObject $database `
    -UseOnlineApproach `
    -MaxDowntimeInSeconds $MaxDowntimeInSeconds `
    -LogFileDirectory $LogFileDirectory

Write-Host "[AE] Validating no columns still reference '$OldCekName'"
$stillUsingOld = $false
@($database.Tables) | ForEach-Object {
    @($_.Columns) | ForEach-Object {
        if ($_.IsEncrypted -and $_.ColumnEncryptionKeyName -eq $OldCekName) {
            $stillUsingOld = $true
        }
    }
}

if ($stillUsingOld) {
    throw "At least one encrypted column still references '$OldCekName'. Aborting CEK removal."
}

Write-Host "[AE] Removing old CEK '$OldCekName'"
Remove-SqlColumnEncryptionKey -Name $OldCekName -InputObject $database

Write-Host '[AE] Completed successfully'
```

## Related content

- [Always Encrypted](always-encrypted-database-engine.md)
- [Overview of key management for Always Encrypted](overview-of-key-management-for-always-encrypted.md)
- [Configure Always Encrypted using PowerShell](configure-always-encrypted-using-powershell.md)
- [Rotate Always Encrypted keys using SQL Server Management Studio](rotate-always-encrypted-keys-using-ssms.md)
- [CREATE COLUMN MASTER KEY (Transact-SQL)](../../../t-sql/statements/create-column-master-key-transact-sql.md)
- [DROP COLUMN MASTER KEY (Transact-SQL)](../../../t-sql/statements/drop-column-master-key-transact-sql.md)
- [CREATE COLUMN ENCRYPTION KEY (Transact-SQL)](../../../t-sql/statements/create-column-encryption-key-transact-sql.md)
- [ALTER COLUMN ENCRYPTION KEY (Transact-SQL)](../../../t-sql/statements/alter-column-encryption-key-transact-sql.md)
- [DROP COLUMN ENCRYPTION KEY (Transact-SQL)](../../../t-sql/statements/drop-column-encryption-key-transact-sql.md)
- [sys.column_master_keys (Transact-SQL)](../../system-catalog-views/sys-column-master-keys-transact-sql.md)
- [sys.column_encryption_keys (Transact-SQL)](../../system-catalog-views/sys-column-encryption-keys-transact-sql.md)
- [Query columns using Always Encrypted with SQL Server Management Studio](always-encrypted-query-columns-ssms.md)
- [Develop applications using Always Encrypted](always-encrypted-client-development.md)
