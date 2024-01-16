---
title: SqlPackage Extract
description: Learn how to automate database development tasks with SqlPackage Extract. View examples and available parameters, properties, and SQLCMD variables.
author: "dzsquared"
ms.author: "drskwier"
ms.reviewer: "maghan"
ms.date: 9/29/2022
ms.service: sql
ms.subservice: tools-other
ms.topic: conceptual
---

# SqlPackage Extract parameters and properties
The SqlPackage Extract action creates a schema of a connected database in a DACPAC file (.dacpac). By default, data is not included in the .dacpac file. To include data, utilize the [Export action](sqlpackage-export.md) or use the Extract properties *ExtractAllTableData*/*TableData*. 

[!INCLUDE [entra-id](../../includes/entra-id-hard-coded.md)]

## Command-line syntax

**SqlPackage** initiates the actions specified using the parameters, properties, and SQLCMD variables specified on the command line.  
  
```bash
SqlPackage /Action:Extract {parameters} {properties}
```

> [!NOTE]
> When a database with password credentials (for example, a SQL authentication user) is extracted, the password is replaced with a different password of suitable complexity. SqlPackage or DacFx users should change the password after the dacpac is published.

### Examples

```bash
# example extract to create a schema-only .dacpac file connecting using SQL authentication
SqlPackage /Action:Extract /TargetFile:{filename}.dacpac /DiagnosticsFile:{logFile}.log /p:ExtractAllTableData=false /p:VerifyExtraction=true \
    /SourceServerName:{serverFQDN} /SourceDatabaseName:{databaseName} /SourceUser:{username} /SourcePassword:{password}

# example extract to create a .sql file containing the schema definition of the database
SqlPackage /Action:Extract /TargetFile:{filename}.dacpac /DiagnosticsFile:{logFile}.log /SourceServerName:{serverFQDN} \
    /SourceDatabaseName:{databaseName} /SourceUser:{username} /SourcePassword:{password} /p:ExtractTarget=File

# example extract to create a .dacpac file with data connecting using SQL authentication
SqlPackage /Action:Extract /TargetFile:{filename}.dacpac /DiagnosticsFile:{logFile}.log /p:ExtractAllTableData=true /p:VerifyExtraction=true \
    /SourceServerName:{serverFQDN} /SourceDatabaseName:{databaseName} /SourceUser:{username} /SourcePassword:{password}


# example extract to create a schema-only .dacpac file connecting using Microsoft Entra managed identity
SqlPackage /Action:Extract /TargetFile:"C:\AdventureWorksLT.dacpac" \
    /SourceConnectionString:"Server=tcp:{yourserver}.database.windows.net,1433;Initial Catalog=AdventureWorksLT;Authentication=Active Directory Managed Identity;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"


# example extract to create a schema-only .dacpac file connecting using Microsoft Entra username and password
SqlPackage /Action:Extract /TargetFile:"C:\AdventureWorksLT.dacpac" \
    /SourceConnectionString:"Server=tcp:{yourserver}.database.windows.net,1433;Initial Catalog=AdventureWorksLT;Authentication=Active Directory Password;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;User ID={yourusername};Password={yourpassword}"


# example extract to create a schema-only .dacpac file connecting using Microsoft Entra universal authentication
SqlPackage /Action:Extract /TargetFile:"C:\AdventureWorksLT.dacpac" /UniversalAuthentication:True \
    /SourceConnectionString:"Server=tcp:{yourserver}.database.windows.net,1433;Initial Catalog=AdventureWorksLT;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
```

```powershell
# example extract to create a schema-only .dacpac file connecting using an access token associated with a service principal
$Account = Connect-AzAccount -ServicePrincipal -Tenant $Tenant -Credential $Credential
$AccessToken_Object = (Get-AzAccessToken -Account $Account -Resource "https://database.windows.net/")
$AccessToken = $AccessToken_Object.Token

SqlPackage /at:$AccessToken /Action:Extract /TargetFile:"C:\AdventureWorksLT.dacpac" \
    /SourceConnectionString:"Server=tcp:{yourserver}.database.windows.net,1433;Initial Catalog=AdventureWorksLT;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
# OR
SqlPackage /at:$($AccessToken_Object.Token) /Action:Extract /TargetFile:"C:\AdventureWorksLT.dacpac" \
    /SourceConnectionString:"Server=tcp:{yourserver}.database.windows.net,1433;Initial Catalog=AdventureWorksLT;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
```

## Parameters for the Extract action

|Parameter|Short Form|Value|Description|
|---|---|---|---|
|**/AccessToken:**|**/at:**|{string}| Specifies the token based authentication access token to use when connect to the target database. |
|**/Action:**|**/a:**|Extract|Specifies the action to be performed. |
|**/AzureCloudConfig:**|**/acc:**|{string}|Specifies the custom endpoints for connecting to Microsoft Entra ID in the format: AzureActiveDirectoryAuthority={value};DatabaseServicePrincipalName={value}" .|
|**/Diagnostics:**|**/d:**|{True&#124;False}|Specifies whether diagnostic logging is output to the console. Defaults to False. |
|**/DiagnosticsFile:**|**/df:**|{string}|Specifies a file to store diagnostic logs. |
|**/MaxParallelism:**|**/mp:**|{int}| Specifies the degree of parallelism for concurrent operations running against a database. The default value is 8. |
|**/OverwriteFiles:**|**/of:**|{True&#124;False}|Specifies if SqlPackage should overwrite existing files. Specifying false causes SqlPackage to abort action if an existing file is encountered. Default value is True. |
|**/Properties:**|**/p:**|{PropertyName}={Value}|Specifies a name value pair for an [action-specific property](#properties-specific-to-the-extract-action); {PropertyName}={Value}. |
|**/Quiet:**|**/q:**|{True&#124;False}|Specifies whether detailed feedback is suppressed. Defaults to False. |
|**/SourceConnectionString:**|**/scs:**|{string}|Specifies a valid [SQL Server/Azure connection string](/dotnet/api/microsoft.data.sqlclient.sqlconnection.connectionstring) to the source database. If this parameter is specified, it shall be used exclusively of all other source parameters. |
|**/SourceDatabaseName:**|**/sdn:**|{string}|Defines the name of the source database. |
|**/SourceEncryptConnection:**|**/sec:**|{Optional&#124;Mandatory&#124;Strict&#124;True&#124;False}|Specifies if SQL encryption should be used for the source database connection. Default value is True. |
|**/SourceHostNameInCertificate:**|**/shnic:**|{string}|Specifies value that is used to validate the source SQL Server TLS/SSL certificate when the communication layer is encrypted by using TLS.|
|**/SourcePassword:**|**/sp:**|{string}|For SQL Server Auth scenarios, defines the password to use to access the source database. |
|**/SourceServerName:**|**/ssn:**|{string}|Defines the name of the server hosting the source database. |
|**/SourceTimeout:**|**/st:**|{int}|Specifies the timeout for establishing a connection to the source database in seconds. |
|**/SourceTrustServerCertificate:**|**/stsc:**|{True&#124;False}|Specifies whether to use TLS to encrypt the source database connection and bypass walking the certificate chain to validate trust. Default value is False. |
|**/SourceUser:**|**/su:**|{string}|For SQL Server Auth scenarios, defines the SQL Server user to use to access the source database. |
|**/TargetFile:**|**/tf:**|{string}| Specifies a target file (that is, a .dacpac file) in local storage to be used as the target of action instead of a database. If this parameter is used, no other target parameter shall be valid. This parameter shall be invalid for actions that only support database targets.| 
|**/TenantId:**|**/tid:**|{string}|Represents the Microsoft Entra tenant ID or domain name. This option is required to support guest or imported Microsoft Entra users as well as Microsoft accounts such as outlook.com, hotmail.com, or live.com. If this parameter is omitted, the default tenant ID for Microsoft Entra ID will be used, assuming that the authenticated user is a native user for this tenant. However, in this case any guest or imported users and/or Microsoft accounts hosted in this Microsoft Entra ID are not supported and the operation will fail. <br/> For more information, see [Universal authentication with SQL Database and Azure Synapse Analytics (SSMS support for MFA)](/azure/azure-sql/database/authentication-mfa-ssms-overview).|
|**/ThreadMaxStackSize:**|**/tmss:**|{int}|Specifies the maximum size in megabytes for the thread running the SqlPackage action. This option should only be used when encountering stack overflow exceptions that occur when parsing very large Transact-SQL statements.|
|**/UniversalAuthentication:**|**/ua:**|{True&#124;False}|Specifies if universal authentication should be used. When set to True, the interactive authentication protocol is activated supporting MFA. This option can also be used for Microsoft Entra authentication without MFA, using an interactive protocol requiring the user to enter their username and password or integrated authentication (Windows credentials). When /UniversalAuthentication is set to True, no Microsoft Entra authentication can be specified in SourceConnectionString (/scs). When /UniversalAuthentication is set to False, Microsoft Entra authentication must be specified in SourceConnectionString (/scs). <br/> For more information, see [Universal authentication with SQL Database and Azure Synapse Analytics (SSMS support for MFA)](/azure/sql-database/sql-database-ssms-mfa-authentication).|

## Properties specific to the Extract action

|Property|Value|Description|
|---|---|---|
|**/p:**|AzureSharedAccessSignatureToken=(STRING)|Azure shared access signature (SAS) token. See [SqlPackage for Azure Synapse Analytics](sqlpackage-for-azure-synapse-analytics.md#publish-import-data) and [SqlPackage with data in Parquet files](sqlpackage-with-data-in-parquet-files.md).|
|**/p:**|AzureStorageBlobEndpoint=(STRING)|Azure Blob Storage endpoint. See [SqlPackage for Azure Synapse Analytics](sqlpackage-for-azure-synapse-analytics.md#extract-export-data) and [SqlPackage with data in Parquet files](sqlpackage-with-data-in-parquet-files.md).|
|**/p:**|AzureStorageContainer=(STRING)|Azure Blob Storage container. See [SqlPackage for Azure Synapse Analytics](sqlpackage-for-azure-synapse-analytics.md#extract-export-data) and [SqlPackage with data in Parquet files](sqlpackage-with-data-in-parquet-files.md).|
|**/p:**|AzureStorageKey=(STRING)|Azure storage account key. See [SqlPackage for Azure Synapse Analytics](sqlpackage-for-azure-synapse-analytics.md#extract-export-data) and [SqlPackage with data in Parquet files](sqlpackage-with-data-in-parquet-files.md).|
|**/p:**|AzureStorageRootPath=(STRING)|Storage root path within the container. Without this property, the path defaults to `servername/databasename/timestamp/`. See [SqlPackage for Azure Synapse Analytics](sqlpackage-for-azure-synapse-analytics.md#extract-export-data) and [SqlPackage with data in Parquet files](sqlpackage-with-data-in-parquet-files.md).|
|**/p:**|CommandTimeout=(INT32 '60')|Specifies the command timeout in seconds when executing queries against SQL Server.|
|**/p:**|CompressionOption=({Normal&#124;Maximum&#124;Fast&#124;SuperFast&#124;NotCompressed} 'Normal')|Specifies the type of compression.|
|**/p:**|DacApplicationDescription=(STRING)|Defines the Application description to be stored in the DACPAC metadata.|
|**/p:**|DacApplicationName=(STRING)|Defined the Application name to be stored in the DACPAC metadata. The default value is the database name.|
|**/p:**|DacMajorVersion=(INT32 '1')|Defines the major version to be stored in the DACPAC metadata.|
|**/p:**|DacMinorVersion=(INT32 '0')|Defines the minor version to be stored in the DACPAC metadata.|
|**/p:**|DatabaseLockTimeout=(INT32 '60')|Specifies the database lock timeout in seconds when executing queries against SQLServer. Use -1 to wait indefinitely.|
|**/p:**|ExtractAllTableData=(BOOLEAN 'False')|Indicates whether data from all user tables is extracted. If 'true', data from all user tables is extracted, and you cannot specify individual user tables for extracting data. If 'false', specify one or more user tables to extract data from.|
|**/p:**|ExtractApplicationScopedObjectsOnly=(BOOLEAN 'True')|If true, only extract application-scoped objects for the specified source. If false, extract all objects for the specified source.|
|**/p:**|ExtractReferencedServerScopedElements=(BOOLEAN 'True')|If true, extract login, server audit, and credential objects referenced by source database objects.|
|**/p:**|ExtractTarget=({DacPac&#124;File&#124;Flat&#124;ObjectType&#124;Schema&#124;SchemaObjectType} 'DacPac')|Specifies alternative output formats of the database schema, default is 'DacPac' to output a `.dacpac` single file. Additional options output one or more `.sql` files organized by either 'SchemaObjectType' (files in folders for each schema and object type), 'Schema' (files in folders for each schema), 'ObjectType' (files in folders for each object type), 'Flat' (all files in the same folder), or 'File' (1 single file).|
|**/p:**|ExtractUsageProperties=(BOOLEAN 'False')|Specifies whether usage properties, such as table row count and index size, will be extracted from the database.|
|**/p:**|HashObjectNamesInLogs=(BOOLEAN 'False')|Specifies whether to replace all object names in logs with a random hash value.|
|**/p:**|IgnoreExtendedProperties=(BOOLEAN 'False')|Specifies whether extended properties should be ignored.|
|**/p:**|IgnorePermissions=(BOOLEAN 'True')|Specifies whether permissions should be ignored.|
|**/p:**|IgnoreUserLoginMappings=(BOOLEAN 'False')|Specifies whether relationships between users and logins are ignored.|
|**/p:**|LongRunningCommandTimeout=(INT32 '0')| Specifies the long running command timeout in seconds when executing queries against SQL Server. Use 0 to wait indefinitely.|
|**/p:**|Storage=({File&#124;Memory})|Specifies the type of backing storage for the schema model used during extraction. 'Memory' is default for .NET Core version of SqlPackage. 'File' is only available and default for .NET Framework version of SqlPackage.|
|**/p:**|TableData=(STRING)|Indicates the table from which data will be extracted. Specify the table name with or without the brackets surrounding the name parts in the following format: schema_name.table_identifier.  This property may be specified multiple times to indicate multiple options.  Applies to data extracted to both `.dacpac` and Parquet files.|
|**/p:**|TempDirectoryForTableData=(STRING)|Specifies the temporary directory used to buffer table data before being written to the package file.|
|**/p:**|VerifyExtraction=(BOOLEAN 'False')|Specifies whether the extracted schema model should be verified.|



## Next Steps

- Learn more about [SqlPackage](sqlpackage.md)
- [Troubleshooting with SqlPackage](./troubleshooting-issues-and-performance-with-sqlpackage.md)
