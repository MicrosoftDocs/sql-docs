---
title: SqlPackage Import
description: Learn how to automate database development tasks with SqlPackage.exe Import. View examples and available parameters, properties, and SQLCMD variables.
ms.service: sql
ms.subservice: tools-other
ms.topic: conceptual
ms.assetid: 198198e2-7cf4-4a21-bda4-51b36cb4284b
author: "dzsquared"
ms.author: "drskwier"
ms.reviewer: "maghan"
ms.date: 9/29/2022
---

# SqlPackage Import parameters and properties
The SqlPackage.exe Import action imports the schema and table data from a BACPAC file (.bacpac) into a new or empty database in SQL Server or Azure SQL Database. At the time of the import operation to an existing database the target database cannot contain any user-defined schema objects. Alternatively, a new database can be created by the import action when the authenticated user has [create database permissions](../../t-sql/statements/create-database-transact-sql.md#permissions).

## Command-line syntax

**SqlPackage.exe** initiates the actions specified using the parameters, properties, and SQLCMD variables specified on the command line.  
  
```bash
SqlPackage /Action:Import {parameters} {properties}
```

### Examples

```bash
# example import from Azure SQL Database using SQL authentication and a connection string
SqlPackage /Action:Import /SourceFile:"C:\AdventureWorksLT.bacpac" \
    /TargetConnectionString:"Server=tcp:{yourserver}.database.windows.net,1433;Initial Catalog=AdventureWorksLT;Persist Security Info=False;User ID=sqladmin;Password={your_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"

# example import using short form parameter names
SqlPackage /a:Import /tsn:"{yourserver}.database.windows.net,1433" /tdn:"AdventureWorksLT" /tu:"sqladmin" \
    /tp:"{your_password}" /sf:"C:\AdventureWorksLT.bacpac"

# example import using Azure Active Directory Service Principal
SqlPackage /Action:Import /SourceFile:"C:\AdventureWorksLT.bacpac" \
    /TargetConnectionString:"Server=tcp:{yourserver}.database.windows.net,1433;Initial Catalog=AdventureWorksLT;Authentication=Active Directory Service Principal;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"

# example import connecting using Azure Active Directory username and password
SqlPackage /Action:Import /SourceFile:"C:\AdventureWorksLT.bacpac" \
    /TargetConnectionString:"Server=tcp:{yourserver}.database.windows.net,1433;Initial Catalog=AdventureWorksLT;Authentication=Active Directory Password;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;User ID={yourusername};Password={yourpassword}"

# example import connecting using Azure Active Directory universal authentication
SqlPackage /Action:Import /SourceFile:"C:\AdventureWorksLT.bacpac" /UniversalAuthentication=True \
    /TargetConnectionString:"Server=tcp:{yourserver}.database.windows.net,1433;Initial Catalog=AdventureWorksLT;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
```

```powershell
# example import connecting using an access token associated with a service principal
$Account = Connect-AzAccount -ServicePrincipal -Tenant $Tenant -Credential $Credential
$AccessToken_Object = (Get-AzAccessToken -Account $Account -Resource "https://database.windows.net/")
$AccessToken = $AccessToken_Object.Token

sqlpackage.exe /at:$AccessToken /Action:Import /SourceFile:"C:\AdventureWorksLT.bacpac" \
    /TargetConnectionString:"Server=tcp:{yourserver}.database.windows.net,1433;Initial Catalog=AdventureWorksLT;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
# OR
sqlpackage.exe /at:$($AccessToken_Object.Token) /Action:Import /SourceFile:"C:\AdventureWorksLT.bacpac" \
    /TargetConnectionString:"Server=tcp:{yourserver}.database.windows.net,1433;Initial Catalog=AdventureWorksLT;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
```

## Parameters for the Import action

|Parameter|Short Form|Value|Description|
|---|---|---|---|
|**/AccessToken:**|**/at**|{string}| Specifies the token-based authentication access token to use when connect to the target database. |
|**/Action:**|**/a**|Import|Specifies the action to be performed. |
|**/AzureCloudConfig:**|**/acc**|{string}|Specifies the custom endpoints for connecting to Azure Active Directory in the format: AzureActiveDirectoryAuthority={value};DatabaseServicePrincipalName={value}" .|
|**/Diagnostics:**|**/d**|{True&#124;False}|Specifies whether diagnostic logging is output to the console. Defaults to False. |
|**/DiagnosticsFile:**|**/df**|{string}|Specifies a file to store diagnostic logs. |
|**/MaxParallelism:**|**/mp**|{int}| Specifies the degree of parallelism for concurrent operations running against a database. The default value is 8.|
|**/ModelFilePath:**|**/mfp**|{string}|Specifies the file path to override the model.xml in the source file. Use of this setting may result in deployment failure and/or unintended data loss. This setting is intended only for use when troubleshooting issues with publish, import, or script generation.|
|**/Properties:**|**/p**|{PropertyName}={Value}|Specifies a name value pair for an [action-specific property](#properties-specific-to-the-import-action); {PropertyName}={Value}. |
|**/Quiet:**|**/q**|{True&#124;False}|Specifies whether detailed feedback is suppressed. Defaults to False.|
|**/SourceFile:**|**/sf**|{string}|Specifies a source file to be used as the source of action from local storage. If this parameter is used, no other source parameter shall be valid. |
|**/TargetConnectionString:**|**/tcs**|{string}|Specifies a valid SQL Server/Azure connection string to the target database. If this parameter is specified, it shall be used exclusively of all other target parameters. |
|**/TargetDatabaseName:**|**/tdn**|{string}|Specifies an override for the name of the database that is the target of SqlPackage.exe Action. |
|**/TargetEncryptConnection:**|**/tec**|{Optional&#124;Mandatory&#124;Strict&#124;True&#124;False}|Specifies if SQL encryption should be used for the target database connection. Default value is True. |
|**/TargetHostNameInCertificate:**|**/thnic**|{string}|Specifies value that is used to validate the target SQL Server TLS/SSL certificate when the communication layer is encrypted by using TLS.|
|**/TargetPassword:**|**/tp**|{string}|For SQL Server Auth scenarios, defines the password to use to access the target database. |
|**/TargetServerName:**|**/tsn**|{string}|Defines the name of the server hosting the target database. |
|**/TargetTimeout:**|**/tt**|{int}|Specifies the timeout for establishing a connection to the target database in seconds. For Azure AD, it is recommended that this value be greater than or equal to 30 seconds.|
|**/TargetTrustServerCertificate:**|**/ttsc**|{True&#124;False}|Specifies whether to use TLS to encrypt the target database connection and bypass walking the certificate chain to validate trust. Default value is False. |
|**/TargetUser:**|**/tu**|{string}|For SQL Server Auth scenarios, defines the SQL Server user to use to access the target database. |
|**/TenantId:**|**/tid**|{string}|Represents the Azure AD tenant ID or domain name. This option is required to support guest or imported Azure AD users as well as Microsoft accounts such as outlook.com, hotmail.com, or live.com. If this parameter is omitted, the default tenant ID for Azure AD will be used, assuming that the authenticated user is a native user for this AD. However, in this case any guest or imported users and/or Microsoft accounts hosted in this Azure AD are not supported and the operation will fail. <br/> For more information about Active Directory Universal Authentication, see [Universal Authentication with SQL Database and Azure Synapse Analytics (SSMS support for MFA)](/azure/sql-database/sql-database-ssms-mfa-authentication).|
|**/ThreadMaxStackSize:**|**/tmss**|{int}|Specifies the maximum size in megabytes for the thread running the SqlPackage.exe action. This option should only be used when encountering stack overflow exceptions that occur when parsing very large TSQL statements.|
|**/UniversalAuthentication:**|**/ua**|{True&#124;False}|Specifies if Universal Authentication should be used. When set to True, the interactive authentication protocol is activated supporting MFA. This option can also be used for Azure AD authentication without MFA, using an interactive protocol requiring the user to enter their username and password or integrated authentication (Windows credentials). When /UniversalAuthentication is set to True, no Azure AD authentication can be specified in SourceConnectionString (/scs). When /UniversalAuthentication is set to False, Azure AD authentication must be specified in SourceConnectionString (/scs). <br/> For more information about Active Directory Universal Authentication, see [Universal Authentication with SQL Database and Azure Synapse Analytics (SSMS support for MFA)](/azure/sql-database/sql-database-ssms-mfa-authentication).|

## Properties specific to the Import action

|Property|Value|Description|
|---|---|---|
|**/p:**|CommandTimeout=(INT32 '60')|Specifies the command timeout in seconds when executing queries against SQL Server.|
|**/p:**|DatabaseEdition=({ Basic &#124; Standard &#124; Premium &#124; DataWarehouse &#124; GeneralPurpose &#124; BusinessCritical &#124; Hyperscale &#124; Default } 'Default')|Defines the edition of an Azure SQL Database. See [Azure SQL Database service tiers](/azure/azure-sql/database/service-tiers-general-purpose-business-critical).|
|**/p:**|DatabaseLockTimeout=(INT32 '60')| Specifies the database lock timeout in seconds when executing queries against SQLServer. Use -1 to wait indefinitely.|
|**/p:**|DatabaseMaximumSize=(INT32 '0')|Defines the maximum size in GB of an Azure SQL Database.|
|**/p:**|DatabaseServiceObjective=(STRING)|Defines the performance level of an Azure SQL Database such as "P0" or "S1".|
|**/p:**|DisableIndexesForDataPhase=(BOOLEAN 'True')|When true (default), disables indexes before importing data. When false, indexes are not rebuilt. |
|**/p:**|DisableParallelismForEnablingIndexes=(BOOLEAN 'False')|Not using parallelism when rebuilding indexes while importing data into SQL Server.|
|**/p:**|HashObjectNamesInLogs=(BOOLEAN 'False')|Specifies whether to replace all object names in logs with a random hash value.|
|**/p:**|ImportContributorArguments=(STRING)|Specifies deployment contributor arguments for the deployment contributors. This property should be a semi-colon delimited list of values.|
|**/p:**|ImportContributorPaths=(STRING)|Specifies paths to load additional import contributors. This property should be a semi-colon delimited list of values.|
|**/p:**|ImportContributors=(STRING)|Specifies the deployment contributors, which should run when the bacpac is imported. This property should be a semi-colon delimited list of fully qualified build contributor names or IDs.|
|**/p:**|LongRunningCommandTimeout=(INT32 '0')|Specifies the long running command timeout in seconds when executing queries against SQL Server. Use 0 to wait indefinitely.|
|**/p:**|PreserveIdentityLastValues=(BOOLEAN 'False')|Specifies whether last values for identity columns should be preserved during deployment.|
|**/p:**|RebuildIndexesOfflineForDataPhase=(BOOLEAN 'False')|When true, rebuilds indexes offline after importing data into SQL Server.|
|**/p:**|Storage=({File&#124;Memory})|Specifies how elements are stored when building the database model. For performance reasons the default is InMemory. For large databases, File backed storage is required.|

## Next Steps

- Learn more about [SqlPackage](sqlpackage.md)
- [Troubleshooting with SqlPackage](./troubleshooting-issues-and-performance-with-sqlpackage.md)