---
title: SqlPackage Export
description: Learn how to automate database development tasks with SqlPackage.exe Export. View examples and available parameters, properties, and SQLCMD variables.
ms.service: sql
ms.subservice: tools-other
ms.topic: conceptual
ms.assetid: 198198e2-7cf4-4a21-bda4-51b36cb4284b
author: "dzsquared"
ms.author: "drskwier"
ms.reviewer: "maghan"
ms.date: 9/29/2022
---

# SqlPackage Export parameters and properties
The SqlPackage.exe Export action exports a connected database to a BACPAC file (.bacpac). By default, data for all tables will be included in the .bacpac file. Optionally, you can specify only a subset of tables for which to export data. Validation for the Export action ensures Azure SQL Database compatibility for the complete targeted database even if a subset of tables is specified for the export. 

## Command-line syntax

**SqlPackage.exe** initiates the actions specified using the parameters, properties, and SQLCMD variables specified on the command line.  
  
```bash
SqlPackage /Action:Export {parameters} {properties}
```

### Examples

```bash
# example export from Azure SQL Database using SQL authentication and a connection string
SqlPackage /Action:Export /TargetFile:"C:\AdventureWorksLT.bacpac" \
    /SourceConnectionString:"Server=tcp:{yourserver}.database.windows.net,1433;Initial Catalog=AdventureWorksLT;Persist Security Info=False;User ID=sqladmin;Password={your_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"

# example export using short form parameter names, skips schema validation
SqlPackage /a:Export /ssn:"{yourserver}.database.windows.net,1433" /sdn:"AdventureWorksLT" /su:"sqladmin" \
    /sp:"{your_password}" /tf:"C:\AdventureWorksLT.bacpac" /p:VerifyExtraction=False

# example export using Azure Active Directory Service Principal
SqlPackage /Action:Export /TargetFile:"C:\AdventureWorksLT.bacpac" \
    /SourceConnectionString:"Server=tcp:{yourserver}.database.windows.net,1433;Initial Catalog=AdventureWorksLT;Authentication=Active Directory Service Principal;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"

# example export connecting using Azure Active Directory username and password
SqlPackage /Action:Export /TargetFile:"C:\AdventureWorksLT.bacpac" \
    /SourceConnectionString:"Server=tcp:{yourserver}.database.windows.net,1433;Initial Catalog=AdventureWorksLT;Authentication=Active Directory Password;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;User ID={yourusername};Password={yourpassword}"

# example export connecting using Azure Active Directory universal authentication
SqlPackage /Action:Export /TargetFile:"C:\AdventureWorksLT.bacpac" /UniversalAuthentication=True \
    /SourceConnectionString:"Server=tcp:{yourserver}.database.windows.net,1433;Initial Catalog=AdventureWorksLT;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
```

```powershell
# example export connecting using an access token associated with a service principal
$Account = Connect-AzAccount -ServicePrincipal -Tenant $Tenant -Credential $Credential
$AccessToken_Object = (Get-AzAccessToken -Account $Account -Resource "https://database.windows.net/")
$AccessToken = $AccessToken_Object.Token

sqlpackage.exe /at:$AccessToken /Action:Export /TargetFile:"C:\AdventureWorksLT.bacpac" \
    /SourceConnectionString:"Server=tcp:{yourserver}.database.windows.net,1433;Initial Catalog=AdventureWorksLT;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
# OR
sqlpackage.exe /at:$($AccessToken_Object.Token) /Action:Export /TargetFile:"C:\AdventureWorksLT.bacpac" \
    /SourceConnectionString:"Server=tcp:{yourserver}.database.windows.net,1433;Initial Catalog=AdventureWorksLT;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
```

## Parameters for the Export action

|Parameter|Short Form|Value|Description|
|---------|----------|-----|-----------|
|**/AccessToken:**|**/at**|{string}| Specifies the token-based authentication access token to use when connect to the target database. |
|**/Action:**|**/a**|Export|Specifies the action to be performed. |
|**/AzureCloudConfig:**|**/acc**|{string}|Specifies the custom endpoints for connecting to Azure Active Directory in the format: AzureActiveDirectoryAuthority={value};DatabaseServicePrincipalName={value}" .|
|**/Diagnostics:**|**/d**|{True&#124;False}|Specifies whether diagnostic logging is output to the console. Defaults to False. |
|**/DiagnosticsFile:**|**/df**|{string}|Specifies a file to store diagnostic logs. |
|**/MaxParallelism:**|**/mp**|{int}| Specifies the degree of parallelism for concurrent operations running against a database. The default value is 8. |
|**/OverwriteFiles:**|**/of**|{True&#124;False}|Specifies if SqlPackage.exe should overwrite existing files. Specifying false causes SqlPackage.exe to abort action if an existing file is encountered. Default value is True. |
|**/Properties:**|**/p**|{PropertyName}={Value}|Specifies a name value pair for an [action-specific property](#properties-specific-to-the-export-action);{PropertyName}={Value}. |
|**/Quiet:**|**/q**|{True&#124;False}|Specifies whether detailed feedback is suppressed. Defaults to False.|
|**/SourceConnectionString:**|**/scs**|{string}|Specifies a valid SQL Server/Azure connection string to the source database. If this parameter is specified, it shall be used exclusively of all other source parameters. |
|**/SourceDatabaseName:**|**/sdn**|{string}|Defines the name of the source database. |
|**/SourceEncryptConnection:**|**/sec**|{Optional&#124;Mandatory&#124;Strict&#124;True&#124;False}|Specifies if SQL encryption should be used for the source database connection. Default value is True. |
|**/SourceHostNameInCertificate:**|**/shnic**|{string}|Specifies value that is used to validate the source SQL Server TLS/SSL certificate when the communication layer is encrypted by using TLS.|
|**/SourcePassword:**|**/sp**|{string}|For SQL Server Auth scenarios, defines the password to use to access the source database. |
|**/SourceServerName:**|**/ssn**|{string}|Defines the name of the server hosting the source database. |
|**/SourceTimeout:**|**/st**|{int}|Specifies the timeout for establishing a connection to the source database in seconds. |
|**/SourceTrustServerCertificate:**|**/stsc**|{True&#124;False}|Specifies whether to use TLS to encrypt the source database connection and bypass walking the certificate chain to validate trust. Default value is False. |
|**/SourceUser:**|**/su**|{string}|For SQL Server Auth scenarios, defines the SQL Server user to use to access the source database. |
|**/TargetFile:**|**/tf**|{string}| Specifies a target file (that is, a .dacpac file) to be used as the target of action instead of a database. If this parameter is used, no other target parameter shall be valid. This parameter shall be invalid for actions that only support database targets.|
|**/TenantId:**|**/tid**|{string}|Represents the Azure AD tenant ID or domain name. This option is required to support guest or imported Azure AD users as well as Microsoft accounts such as outlook.com, hotmail.com, or live.com. If this parameter is omitted, the default tenant ID for Azure AD will be used, assuming that the authenticated user is a native user for this AD. However, in this case any guest or imported users and/or Microsoft accounts hosted in this Azure AD are not supported and the operation will fail. <br/> For more information about Active Directory Universal Authentication, see [Universal Authentication with SQL Database and Azure Synapse Analytics (SSMS support for MFA)](/azure/sql-database/sql-database-ssms-mfa-authentication).|
|**/ThreadMaxStackSize:**|**/tmss**|{int}|Specifies the maximum size in megabytes for the thread running the SqlPackage.exe action. This option should only be used when encountering stack overflow exceptions that occur when parsing very large TSQL statements.|
|**/UniversalAuthentication:**|**/ua**|{True&#124;False}|Specifies if Universal Authentication should be used. When set to True, the interactive authentication protocol is activated supporting MFA. This option can also be used for Azure AD authentication without MFA, using an interactive protocol requiring the user to enter their username and password or integrated authentication (Windows credentials). When /UniversalAuthentication is set to True, no Azure AD authentication can be specified in SourceConnectionString (/scs). When /UniversalAuthentication is set to False, Azure AD authentication must be specified in SourceConnectionString (/scs). <br/> For more information about Active Directory Universal Authentication, see [Universal Authentication with SQL Database and Azure Synapse Analytics (SSMS support for MFA)](/azure/sql-database/sql-database-ssms-mfa-authentication).|

## Properties specific to the Export action

|Property|Value|Description|
|--------|-----|-----------|
|**/p:**|CommandTimeout=(INT32 '60')|Specifies the command timeout in seconds when executing queries against SQL Server.|
|**/p:**|CompressionOption=({Normal&#124;Maximum&#124;Fast&#124;SuperFast&#124;NotCompressed} 'Normal')|Specifies the type of compression.|
|**/p:**|DatabaseLockTimeout=(INT32 '60')| Specifies the database lock timeout in seconds when executing queries against SQLServer. Use -1 to wait indefinitely.|
|**/p:**|HashObjectNamesInLogs=(BOOLEAN 'False')|Specifies whether to replace all object names in logs with a random hash value.|
|**/p:**|IgnoreIndexesStatisticsOnEnclaveEnabledColumns=(BOOLEAN 'False')|Specifies whether indexes or statistics on columns encrypted using randomized encryption and enclave-enabled column encryption keys should be ignored (not included in the generated bacpac). By default (false) any index or a statistic on a column encrypted using randomized encryption and an enclave-enabled column encryption key will block the export action.|
|**/p:**|LongRunningCommandTimeout=(INT32 '0')|Specifies the long running command timeout in seconds when executing queries against SQL Server. Use 0 to wait indefinitely.|
|**/p:**|Storage=({File&#124;Memory})|Specifies the type of backing storage for the schema model used during extraction. 'Memory' is default for .NET Core version of SqlPackage. 'File' is only available and default for .NET Framework version of SqlPackage. |
|**/p:**|TableData=(STRING[])|Indicates the table from which data will be extracted. Specify the table name with or without the brackets surrounding the name parts in the following format: schema_name.table_identifier. This option may be specified multiple times.|
|**/p:**|TargetEngineVersion=({Default&#124;Latest&#124;V11&#124;V12} 'Latest')|This property is deprecated and use is not recommended. Specifies the version the target engine for Azure SQL Database is expected to be.|
|**/p:**|TempDirectoryForTableData=(STRING)|Specifies an alternative temporary directory used to buffer table data before being written to the package file. The space required in this location may be large and is relative to the full size of the database.|
|**/p:**|VerifyExtraction=(BOOLEAN 'True')|Specifies whether the extracted schema model should be verified. If set to true, schema validation rules are run on the dacpac or bacpac.|
|**/p:**|VerifyFullTextDocumentTypesSupported=(BOOLEAN 'False')|Specifies whether the supported full-text document types for Microsoft Azure SQL Database v12 should be verified.|

## Next Steps

- Learn more about [SqlPackage](sqlpackage.md)
- [Troubleshooting with SqlPackage](./troubleshooting-issues-and-performance-with-sqlpackage.md)