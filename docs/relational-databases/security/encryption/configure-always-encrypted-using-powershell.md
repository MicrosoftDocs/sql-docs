---
title: "Configure Always Encrypted using PowerShell | Microsoft Docs"
description: Learn how to import and use the SqlServer PowerShell module, which provides cmdlets for configuring Always Encrypted in both Azure SQL Database and SQL Server.
ms.custom: ""
ms.date: 04/12/2022
ms.service: sql
ms.reviewer: vanto
ms.subservice: security
ms.topic: conceptual
ms.assetid: 12f2bde5-e100-41fa-b474-2d2332fc7650
author: jaszymas
ms.author: jaszymas
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Configure Always Encrypted using PowerShell
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../../includes/applies-to-version/sql-asdb-asdbmi.md)]

The SqlServer PowerShell module provides cmdlets for configuring [Always Encrypted](../../../relational-databases/security/encryption/always-encrypted-database-engine.md) in both [!INCLUDE[ssSDSFull](../../../includes/sssdsfull-md.md)] or [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].

## Security Considerations when using PowerShell to Configure Always Encrypted

Because the primary goal of Always Encrypted is to ensure encrypted sensitive data is safe, even if the database system gets compromised, executing a PowerShell script that processes keys or sensitive data on the SQL Server computer can reduce or defeat the benefits of the feature. For additional security-related recommendations, see [Security Considerations for Key Management](overview-of-key-management-for-always-encrypted.md#security-considerations-for-key-management).

You can use PowerShell to manage Always Encrypted keys both with and without role separation, providing control over who has access to the actual encryption keys in the key store, and who has access to the database.

 For additional recommendations, see [Security Considerations for Key Management](overview-of-key-management-for-always-encrypted.md#security-considerations-for-key-management).

## Prerequisites

Install the [SqlServer module](/powershell/sqlserver/sqlserver/vlatest/sqlserver) on a secure computer that is NOT a computer hosting your SQL Server instance. The module can be installed directly from the PowerShell gallery.  See the [download](../../../powershell/download-sql-server-ps-module.md) instructions for more details.


## <a name="importsqlservermodule"></a> Importing the SqlServer Module 

To load the SqlServer module:

1.	Use the **Set-ExecutionPolicy** cmdlet to set the appropriate script execution policy.
2.	Use the **Import-Module** cmdlet to import the SqlServer module.

This example loads the SqlServer module.

```
# Import the SQL Server Module.  
Import-Module "SqlServer" 
```

> [!NOTE]
> This example does not work when using Azure AD multi-factor authentication (MFA). In order to use MFA, the authentication needs to be `Active Directory Interactive` authentication, and the library [System.Data.SqlClient](/dotnet/api/system.data.sqlclient.sqlconnection.connectionstring) used in PowerShell does not support the `Active Directory Interactive` authentication method.

## <a name="connectingtodatabase"></a> Connecting to a Database

Some of the Always Encrypted cmdlets work with data or metadata in the database and require that you connect to the database first. There are two recommended methods of connecting to a database when configuring Always Encrypted using the SqlServer module: 
1. Connect using the **Get-SqlDatabase** cmdlet.
2. Connect using SQL Server PowerShell Provider.

[!INCLUDE[freshInclude](../../../includes/paragraph-content/fresh-note-steps-feedback.md)]

### Using Get-SqlDatabase
The **Get-SqlDatabase** cmdlet allows you to connect to a database in SQL Server or in Azure SQL Database. It returns a database object, which you can then pass using the **InputObject** parameter of a cmdlet that connects to the database. 

### Using SQL Server PowerShell

```
# Import the SqlServer module
Import-Module "SqlServer"  

# Connect to your database
# Set the valid server name, database name and authentication keywords in the connection string
$serverName = "<Azure SQL server name>.database.windows.net"
$databaseName = "<database name>"
$connStr = "Server = " + $serverName + "; Database = " + $databaseName + "; Authentication = Active Directory Integrated"
$database = Get-SqlDatabase -ConnectionString $connStr

# List column master keys for the specified database.
Get-SqlColumnMasterKey -InputObject $database
```

Alternatively, you can use piping:


```
$database | Get-SqlColumnMasterKey
```

### Using SQL Server PowerShell Provider
The [SQL Server PowerShell Provider](../../../powershell/sql-server-powershell-provider.md) exposes the hierarchy of SQL Server objects in paths similar to file system paths. With SQL Server PowerShell, you can navigate the paths using Windows PowerShell aliases similar to the commands you typically use to navigate file system paths. Once you navigate to the target instance and the database, the subsequent cmdlets target that database, as shown in the following example. 

> [!NOTE]
> This method of connecting to a database works only for SQL Server (it is not supported in Azure SQL Database).

```
# Import the SqlServer module.
Import-Module "SqlServer"
# Navigate to the database in the remote instance.
cd SQLSERVER:\SQL\servercomputer\DEFAULT\Databases\yourdatabase
# List column master keys in the above database.
Get-SqlColumnMasterKey
```

 
Alternatively, you can specify a database path using the generic **Path** parameter, instead of navigating to the database.


```
# Import the SqlServer module.
Import-Module "SqlServer" 
# List column master keys for the specified database.
Get-SqlColumnMasterKey -Path SQLSERVER:\SQL\servercomputer\DEFAULT\Databases\yourdatabase
```
 
## Always Encrypted Tasks using PowerShell

- [Provision Always Encrypted Keys using PowerShell](configure-always-encrypted-keys-using-powershell.md)
- [Rotate Always Encrypted Keys using PowerShell](../../../relational-databases/security/encryption/rotate-always-encrypted-keys-using-powershell.md)
- [Encrypt, Re-Encrypt, or Decrypt Columns with Always Encrypted using PowerShell](configure-column-encryption-using-powershell.md)


##  <a name="aecmdletreference"></a> Always Encrypted Cmdlet Reference

The following PowerShell cmdlets are available for Always Encrypted:

|CMDLET	|Description
|:---|:---
|**[Add-SqlAzureAuthenticationContext](/powershell/sqlserver/sqlserver/vlatest/add-sqlazureauthenticationcontext)**	|Performs authentication to Azure and acquires an authentication token.
|**[Add-SqlColumnEncryptionKeyValue](/powershell/sqlserver/sqlserver/vlatest/add-sqlcolumnencryptionkeyvalue)**	|Adds a new encrypted value for an existing column encryption key object in the database.
|**[Complete-SqlColumnMasterKeyRotation](/powershell/sqlserver/sqlserver/vlatest/complete-sqlcolumnmasterkeyrotation)**	|Completes the rotation of a column master key
|**[Get-SqlColumnEncryptionKey](/powershell/sqlserver/sqlserver/vlatest/get-sqlcolumnencryptionkey)**	|Returns all column encryption key objects defined in the database, or returns one column encryption key object with the specified name.
|**[Get-SqlColumnMasterKey](/powershell/sqlserver/sqlserver/vlatest/get-sqlcolumnmasterkey)**	|Returns the column master key objects defined in the database, or returns one column master key object with the specified name.
|**[Invoke-SqlColumnMasterKeyRotation](/powershell/sqlserver/sqlserver/vlatest/invoke-sqlcolumnmasterkeyrotation)**	|Initiates the rotation of a column master key.
|**[New-SqlAzureKeyVaultColumnMasterKeySettings](/powershell/sqlserver/sqlserver/vlatest/new-sqlazurekeyvaultcolumnmasterkeysettings)**	|Creates a SqlColumnMasterKeySettings object describing an asymmetric key stored in Azure Key Vault.
|**[New-SqlCngColumnMasterKeySettings](/powershell/sqlserver/sqlserver/vlatest/new-sqlcngcolumnmasterkeysettings)**	|Creates a SqlColumnMasterKeySettings object describing an asymmetric key stored in a key store supporting the Cryptography Next Generation (CNG) API.
|**[New-SqlColumnEncryptionKey](/powershell/sqlserver/sqlserver/vlatest/new-sqlcolumnencryptionkey)**	|Creates a column encryption key object in the database.
|**[New-SqlColumnEncryptionKeyEncryptedValue](/powershell/sqlserver/sqlserver/vlatest/new-sqlcolumnencryptionkeyencryptedvalue)**	|Produces an encrypted value of a column encryption key.
|**[New-SqlColumnEncryptionSettings](/powershell/sqlserver/sqlserver/vlatest/new-sqlcolumnencryptionsettings)**	|Creates a SqlColumnEncryptionSettings object that encapsulates information about a single column's encryption, including CEK and encryption type.
|**[New-SqlColumnMasterKey](/powershell/sqlserver/sqlserver/vlatest/new-sqlcolumnmasterkey)**	|Creates a column master key object in the database.
|**[New-SqlColumnMasterKeySettings](/powershell/sqlserver/sqlserver/vlatest/new-sqlcolumnmasterkeysettings)**|Creates a SqlColumnMasterKeySettings object for a column master key with the specified provider and key path.
|**[New-SqlCspColumnMasterKeySettings](/powershell/sqlserver/sqlserver/vlatest/new-sqlcspcolumnmasterkeysettings)**	|Creates a SqlColumnMasterKeySettings object describing an asymmetric key stored in a key store with a Cryptography Service Provider (CSP) supporting Cryptography API (CAPI).
|**[Remove-SqlColumnEncryptionKey](/powershell/sqlserver/sqlserver/vlatest/remove-sqlcolumnencryptionkey)**	|Removes the column encryption key object from the database.
|**[Remove-SqlColumnEncryptionKeyValue](/powershell/sqlserver/sqlserver/vlatest/remove-sqlcolumnencryptionkeyvalue)**	|Removes an encrypted value from an existing column encryption key object in the database.
|**[Remove-SqlColumnMasterKey](/powershell/sqlserver/sqlserver/vlatest/remove-sqlcolumnmasterkey)**	|Removes the column master key object from the database.
|**[Set-SqlColumnEncryption](/powershell/sqlserver/sqlserver/vlatest/set-sqlcolumnencryption)**	|Encrypts, decrypts, or re-encrypts specified columns in the database.



## See Also

- [Always Encrypted](../../../relational-databases/security/encryption/always-encrypted-database-engine.md)
- [Overview of key management for Always Encrypted](../../../relational-databases/security/encryption/overview-of-key-management-for-always-encrypted.md)
- [Configure Always Encrypted using SQL Server Management Studio](../../../relational-databases/security/encryption/configure-always-encrypted-using-sql-server-management-studio.md)
- [Develop applications using Always Encrypted](always-encrypted-client-development.md)
