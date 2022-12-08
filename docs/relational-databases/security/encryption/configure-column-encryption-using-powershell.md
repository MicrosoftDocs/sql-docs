---
description: "Configure column encryption using Always Encrypted with PowerShell"
title: "Configure column encryption using Always Encrypted with PowerShell | Microsoft Docs"
ms.custom: ""
ms.date: "10/31/2019"
ms.service: sql
ms.reviewer: vanto
ms.subservice: security
ms.topic: conceptual
ms.assetid: 074c012b-cf14-4230-bf0d-55e23d24f9c8
author: jaszymas
ms.author: jaszymas
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Configure column encryption using Always Encrypted with PowerShell
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../../includes/applies-to-version/sql-asdb-asdbmi.md)]

This article provides the steps for setting the target Always Encrypted configuration for database columns using the [Set-SqlColumnEncryption](/powershell/sqlserver/sqlserver/vlatest/set-sqlcolumnencryption) cmdlet (in the *SqlServer* PowerShell module). The **Set-SqlColumnEncryption** cmdlet modifies both the schema of the target database as well as the data stored in the selected columns. The data stored in a column can be encrypted, re-encrypted, or decrypted, depending on the specified target encryption settings for the columns and the current encryption configuration.

::: moniker range=">=sql-server-ver15"

> [!NOTE]
> If you are using [!INCLUDE [sssql19-md](../../../includes/sssql19-md.md)] and your SQL Server instance is configured with a secure enclave, you can run cryptographic operations in-place, without moving data out of the database. See [Configure column encryption in-place using Always Encrypted with secure enclaves](always-encrypted-enclaves-configure-encryption.md). Note that the PowerShell does not support in-place encryption.

::: moniker-end
For more information about Always Encrypted support in the SqlServer PowerShell module, see [Configure Always Encrypted using PowerShell](../../../relational-databases/security/encryption/configure-always-encrypted-using-powershell.md).

## Prerequisites

To set the target encryption configuration, you need to make sure:
- a column encryption key is configured in the database (if you're encrypting or re-encrypting a column). For details, see [Configure Always Encrypted keys using PowerShell](../../../relational-databases/security/encryption/configure-always-encrypted-keys-using-powershell.md).
- you can access the column master key for each column you want to encrypt, re-encrypt, or decrypt, from the computer running the PowerShell cmdlets. 

## Performance and Availability Considerations

To apply the specified target encryption settings for the database, the **Set-SqlColumnEncryption** cmdlet transparently downloads all data from the columns containing the target tables, uploads the data back to a set of temporary table (with the target encrypted settings), and finally replaces the original tables with the new versions of the tables. The underlying .NET Framework Data Provider for SQL Server encrypts or/and decrypts data on download or/and upload, depending on the current encryption configuration of the target column is and the specified target encryption settings for the target columns. The operation to move the data may take a long time, depending on the size of the data in impacted tables and the network bandwidth.

The **Set-SqlColumnEncryption** cmdlet supports two approaches for setting up the target encryption configuration: online and offline.

With the offline approach, the target tables (and any tables related to the target tables, for example, any tables a target table have foreign key relationships with) are unavailable to write transactions throughout the duration of the operation. The semantics of foreign key constraints (**CHECK** or **NOCHECK**) are always preserved when using the offline approach.

With the online approach (requires the SqlServer PowerShell module version 21.x or later), the operation of copying and encrypting, decrypting, or re-encrypting the data is performed incrementally. Applications can read and write data from and to the target tables throughout the data movement operation, except the last iteration, the duration of which is limited by the **MaxDownTimeInSeconds** parameter (which you can define). To detect and process the changes applications can make while the data is being copied, the cmdlet enables [Change Tracking](../../track-changes/enable-and-disable-change-tracking-sql-server.md) in the target database. Because of that, the online approach is likely to consume more resources on the server side than the offline approach. The operation may also take much more time with the online approach, especially if a write-heavy workload is running against the database. The online approach can be used to encrypt one table at a time and the table must have a primary key. By default, foreign key constraints are recreated with the **NOCHECK** option to minimize the impact on applications. You can enforce preserving the semantics of foreign key constraints by specifying the **KeepCheckForeignKeyConstraints** option. 

Here are the guidelines for choosing between the offline and online approaches:

Use the offline approach:
- To minimize the duration of the operation. 
- To encrypt/decrypt/re-encrypt columns in multiple tables at the same time.
- If the target table doesn't have a primary key.

Use the online approach:
- To minimize the downtime/unavailability of the database to your applications.

## Security Considerations

The **Set-SqlColumnEncryption** cmdlet, used to configure encryption for database columns, handles both Always Encrypted keys and the data stored in database columns. Therefore, it's important you run the cmdlet on a secure computer. If your database is in SQL Server, execute the cmdlet from a different computer than the computer hosting your SQL Server instance. Because the primary goal of Always Encrypted is to ensure encrypted sensitive data is safe even if the database system gets compromised, executing a PowerShell script that processes keys and/or sensitive data on the SQL Server computer can reduce or defeat the benefits of the feature.

Task  |Article  |Accesses plaintext keys/key store  |Accesses database   
---|---|---|---
Step 1. Start a PowerShell environment and import the SqlServer module. | [Import the SqlServer module](../../../relational-databases/security/encryption/configure-always-encrypted-using-powershell.md#importsqlservermodule) | No | No
Step 2. Connect to your server and database | [Connecting to a database](../../../relational-databases/security/encryption/configure-always-encrypted-using-powershell.md#connectingtodatabase) | No | Yes
Step 3. Authenticate to Azure, if your column master key (protecting the column encryption key, to be rotated), is stored in Azure Key Vault | [Add-SqlAzureAuthenticationContext](/powershell/sqlserver/sqlserver/vlatest/add-sqlazureauthenticationcontext) | Yes | No
Step 4. Construct an array of SqlColumnEncryptionSettings objects - one for each database column, you want to encrypt, re-encrypt, or decrypt. SqlColumnMasterKeySettings is an object that exists in memory (in PowerShell). It specifies the target encryption scheme for a column. | [New-SqlColumnEncryptionSettings](/powershell/sqlserver/sqlserver/vlatest/new-sqlcolumnencryptionsettings) | No | No
Step 5. Set the desired encryption configuration, specified in the array of SqlColumnMasterKeySettings objects, you created in the previous step. A column will be encrypted, re-encrypted, or decrypted, depending on the specified target settings and the current encryption configuration of the column.| [Set-SqlColumnEncryption](/powershell/sqlserver/sqlserver/vlatest/set-sqlcolumnencryption)<br><br>**Note:** This step may take a long time. Your applications won't be able to access the tables through the entire operation or a portion of it, depending on the approach (online vs. offline), you select. | Yes | Yes

## Encrypt Columns using Offline Approach - Example

The below example demonstrates setting the target encryption configuration for a couple of columns. If either column isn't already encrypted, it will be encrypted. If any column is already encrypted using a different key and/or a different encryption type, it will be decrypted and then re-encrypted with the specified target key/type.


```PowerShell
# Import the SqlServer module.
Import-Module "SqlServer"

# Connect to your database.
$serverName = "<server name>"
$databaseName = "<database name>"
# Change the authentication method in the connection string, if needed.
$connStr = "Server = " + $serverName + "; Database = " + $databaseName + "; Integrated Security = True"
$database = Get-SqlDatabase -ConnectionString $connStr

# Encrypt the selected columns (or re-encrypt, if they are already encrypted using keys/encrypt types, different than the specified keys/types.
$ces = @()
$ces += New-SqlColumnEncryptionSettings -ColumnName "dbo.Patients.SSN" -EncryptionType "Deterministic" -EncryptionKey "CEK1"
$ces += New-SqlColumnEncryptionSettings -ColumnName "dbo.Patients.BirthDate" -EncryptionType "Randomized" -EncryptionKey "CEK1"
Set-SqlColumnEncryption -InputObject $database -ColumnEncryptionSettings $ces -LogFileDirectory .
```
 
## Encrypt Columns using Online Approach - Example

The below example demonstrates setting the target encryption configuration for a couple of columns using the online approach. If either column isn't already encrypted, it will be encrypted. If any column is already encrypted using a different key and/or a different encryption type, it will be decrypted and then re-encrypted with the specified target key/type.

```PowerShell
# Import the SqlServer module.
Import-Module "SqlServer"

# Connect to your database.
$serverName = "<server name>"
$databaseName = "<database name>"
# Change the authentication method in the connection string, if needed.
$connStr = "Server = " + $serverName + "; Database = " + $databaseName + "; Integrated Security = True"
$database = Get-SqlDatabase -ConnectionString $connStr

# Encrypt the selected columns (or re-encrypt, if they are already encrypted using keys/encrypt types, different than the specified keys/types.
$ces = @()
$ces += New-SqlColumnEncryptionSettings -ColumnName "dbo.Patients.SSN" -EncryptionType "Deterministic" -EncryptionKey "CEK1"
$ces += New-SqlColumnEncryptionSettings -ColumnName "dbo.Patients.BirthDate" -EncryptionType "Randomized" -EncryptionKey "CEK1"
Set-SqlColumnEncryption -InputObject $database -ColumnEncryptionSettings $ces -UseOnlineApproach -MaxDowntimeInSeconds 180 -LogFileDirectory .
```
## Decrypt Columns - Example

The following example shows how to decrypt all columns that are currently encrypted in a database.


```PowerShell
# Import the SqlServer module.
Import-Module "SqlServer"

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
Set-SqlColumnEncryption -ColumnEncryptionSettings $ces -InputObject $database -LogFileDirectory .
```
 
## Next Steps
- [Develop applications using Always Encrypted](always-encrypted-client-development.md)

## See Also  
 - [Always Encrypted](../../../relational-databases/security/encryption/always-encrypted-database-engine.md)
 - [Overview of key management for Always Encrypted](overview-of-key-management-for-always-encrypted.md) 
 - [Configure Always Encrypted using PowerShell](../../../relational-databases/security/encryption/configure-always-encrypted-using-powershell.md)
 - [Configure column encryption using Always Encrypted Wizard](always-encrypted-wizard.md)
 - [Configure column encryption using Always Encrypted with a DAC package](configure-always-encrypted-using-dacpac.md)