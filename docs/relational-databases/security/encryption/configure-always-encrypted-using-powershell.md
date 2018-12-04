---
title: "Configure Always Encrypted using PowerShell | Microsoft Docs"
ms.custom: ""
ms.date: "05/17/2017"
ms.prod: sql
ms.reviewer: vanto
ms.technology: security
ms.topic: conceptual
ms.assetid: 12f2bde5-e100-41fa-b474-2d2332fc7650
author: VanMSFT
ms.author: vanto
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Configure Always Encrypted using PowerShell
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]

The SqlServer PowerShell module provides cmdlets for configuring [Always Encrypted](../../../relational-databases/security/encryption/always-encrypted-database-engine.md) in both Azure SQL Database and SQL Server 2016.

Always Encrypted cmdlets in the SqlServer module work with keys or sensitive data, so it is important that you run the cmdlets on a secure computer. When managing Always Encrypted, execute the cmdlets from a different computer than the computer hosting your SQL Server instance.

Because the primary goal of Always Encrypted is to ensure encrypted sensitive data is safe, even if the database system gets compromised, executing a PowerShell script that processes keys or sensitive data on the SQL Server computer can reduce or defeat the benefits of the feature. For additional security-related recommendations, see [Security Considerations for Key Management](../../../relational-databases/security/encryption/overview-of-key-management-for-always-encrypted.md#SecurityForKeyManagement).

Links to the individual cmdlet articles are at the [bottom of this page](#aecmdletreference).

## Prerequisites

Install the [SqlServer module](https://docs.microsoft.com/powershell/sqlserver/sqlserver/vlatest/sqlserver) on a secure computer that is NOT a computer hosting your SQL Server instance. The module can be installed directly from the PowerShell gallery.  See the [download](../../../ssms/download-sql-server-ps-module.md) instructions for more details.


## <a name="importsqlservermodule"></a> Importing the SqlServer Module 

To load the SqlServer module:

1.	Use the **Set-ExecutionPolicy** cmdlet to set the appropriate script execution policy.
2.	Use the **Import-Module** cmdlet to import the SqlServer module.

This example loads the SqlServer module.

```
# Import the SQL Server Module.  
Import-Module "SqlServer" 
```

## <a name="connectingtodatabase"></a> Connecting to a Database

Some of the Always Encrypted cmdlets work with data or metadata in the database and require that you connect to the database first. There are two recommended methods of connecting to a database when configuring Always Encrypted using the SqlServer module: 
1. Connect using SQL Server PowerShell.
2. Connect using SQL Server Management Objects (SMO).

### Using SQL Server PowerShell

This method works only for SQL Server (it is not supported in Azure SQL Database).

With SQL Server PowerShell, you can navigate the paths using Windows PowerShell aliases similar to the commands you typically use to navigate file system paths. Once you navigate to the target instance and the database, the subsequent cmdlets target that database, as shown in the following example:

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
 
### Using SMO

This method works for both Azure SQL Database and SQL Server.
With SMO, you can create an object of the [Database Class](https://msdn.microsoft.com/library/microsoft.sqlserver.management.smo.database.aspx), and then pass the object using the **InputObject** parameter of a cmdlet that connects to the database.


```
# Import the SqlServer module
Import-Module "SqlServer"  

# Connect to your database (Azure SQL database).
$serverName = "<Azure SQL server name>.database.windows.net"
$databaseName = "<database name>"
$connStr = "Server = " + $serverName + "; Database = " + $databaseName + "; Authentication = Active Directory Integrated"
$connection = New-Object Microsoft.SqlServer.Management.Common.ServerConnection
$connection.ConnectionString = $connStr
$connection.Connect()
$server = New-Object Microsoft.SqlServer.Management.Smo.Server($connection)
$database = $server.Databases[$databaseName] 

# List column master keys for the specified database.
Get-SqlColumnMasterKey -InputObject $database
```


Alternatively, you can use piping:


```
$database | Get-SqlColumnMasterKey
```

## Always Encrypted Tasks using PowerShell

- [Configure Always Encrypted Keys using PowerShell](../../../relational-databases/security/encryption/configure-always-encrypted-keys-using-powershell.md) 
- [Rotate Always Encrypted Keys using PowerShell](../../../relational-databases/security/encryption/rotate-always-encrypted-keys-using-powershell.md)
- [Configure Column Encryption using PowerShell](../../../relational-databases/security/encryption/configure-column-encryption-using-powershell.md)


##  <a name="aecmdletreference"></a> Always Encrypted Cmdlet Reference

The following PowerShell cmdlets are available for Always Encrypted:

|CMDLET	|Description
|:---|:---
|**[Add-SqlAzureAuthenticationContext](https://docs.microsoft.com/powershell/sqlserver/sqlserver/vlatest/add-sqlazureauthenticationcontext)**	|Performs authentication to Azure and acquires an authentication token.
|**[Add-SqlColumnEncryptionKeyValue](https://docs.microsoft.com/powershell/sqlserver/sqlserver/vlatest/add-sqlcolumnencryptionkeyvalue)**	|Adds a new encrypted value for an existing column encryption key object in the database.
|**[Complete-SqlColumnMasterKeyRotation](https://docs.microsoft.com/powershell/sqlserver/sqlserver/vlatest/complete-sqlcolumnmasterkeyrotation)**	|Completes the rotation of a column master key
|**[Get-SqlColumnEncryptionKey](https://docs.microsoft.com/powershell/sqlserver/sqlserver/vlatest/get-sqlcolumnencryptionkey)**	|Returns all column encryption key objects defined in the database, or returns one column encryption key object with the specified name.
|**[Get-SqlColumnMasterKey](https://docs.microsoft.com/powershell/sqlserver/sqlserver/vlatest/get-sqlcolumnmasterkey)**	|Returns the column master key objects defined in the database, or returns one column master key object with the specified name.
|**[Invoke-SqlColumnMasterKeyRotation](https://docs.microsoft.com/powershell/sqlserver/sqlserver/vlatest/invoke-sqlcolumnmasterkeyrotation)**	|Initiates the rotation of a column master key.
|**[New-SqlAzureKeyVaultColumnMasterKeySettings](https://docs.microsoft.com/powershell/sqlserver/sqlserver/vlatest/new-sqlazurekeyvaultcolumnmasterkeysettings)**	|Creates a SqlColumnMasterKeySettings object describing an asymmetric key stored in Azure Key Vault.
|**[New-SqlCngColumnMasterKeySettings](https://docs.microsoft.com/powershell/sqlserver/sqlserver/vlatest/new-sqlcngcolumnmasterkeysettings)**	|Creates a SqlColumnMasterKeySettings object describing an asymmetric key stored in a key store supporting the Cryptography Next Generation (CNG) API.
|**[New-SqlColumnEncryptionKey](https://docs.microsoft.com/powershell/sqlserver/sqlserver/vlatest/new-sqlcolumnencryptionkey)**	|Creates a column encryption key object in the database.
|**[New-SqlColumnEncryptionKeyEncryptedValue](https://docs.microsoft.com/powershell/sqlserver/sqlserver/vlatest/new-sqlcolumnencryptionkeyencryptedvalue)**	|Produces an encrypted value of a column encryption key.
|**[New-SqlColumnEncryptionSettings](https://docs.microsoft.com/powershell/sqlserver/sqlserver/vlatest/new-sqlcolumnencryptionsettings)**	|Creates a SqlColumnEncryptionSettings object that encapsulates information about a single column's encryption, including CEK and encryption type.
|**[New-SqlColumnMasterKey](https://docs.microsoft.com/powershell/sqlserver/sqlserver/vlatest/new-sqlcolumnmasterkey)**	|Creates a column master key object in the database.
|**[New-SqlColumnMasterKeySettings](https://docs.microsoft.com/powershell/sqlserver/sqlserver/vlatest/new-sqlcolumnmasterkeysettings)**|Creates a SqlColumnMasterKeySettings object for a column master key with the specified provider and key path.
|**[New-SqlCspColumnMasterKeySettings](https://docs.microsoft.com/powershell/sqlserver/sqlserver/vlatest/new-sqlcspcolumnmasterkeysettings)**	|Creates a SqlColumnMasterKeySettings object describing an asymmetric key stored in a key store with a Cryptography Service Provider (CSP) supporting Cryptography API (CAPI).
|**[Remove-SqlColumnEncryptionKey](https://docs.microsoft.com/powershell/sqlserver/sqlserver/vlatest/remove-sqlcolumnencryptionkey)**	|Removes the column encryption key object from the database.
|**[Remove-SqlColumnEncryptionKeyValue](https://docs.microsoft.com/powershell/sqlserver/sqlserver/vlatest/remove-sqlcolumnencryptionkeyvalue)**	|Removes an encrypted value from an existing column encryption key object in the database.
|**[Remove-SqlColumnMasterKey](https://docs.microsoft.com/powershell/sqlserver/sqlserver/vlatest/remove-sqlcolumnmasterkey)**	|Removes the column master key object from the database.
|**[Set-SqlColumnEncryption](https://docs.microsoft.com/powershell/sqlserver/sqlserver/vlatest/set-sqlcolumnencryption)**	|Encrypts, decrypts, or re-encrypts specified columns in the database.



## Additional Resources

- [Always Encrypted (Database Engine)](../../../relational-databases/security/encryption/always-encrypted-database-engine.md)
- [Overview of Key Management for Always Encrypted](../../../relational-databases/security/encryption/overview-of-key-management-for-always-encrypted.md)
- [Using Always Encrypted with .NET Framework Data Provider for SQL Server](../../../relational-databases/security/encryption/always-encrypted-client-development.md)
- [Configure Always Encrypted using SQL Server Management Studio](../../../relational-databases/security/encryption/configure-always-encrypted-using-sql-server-management-studio.md)


