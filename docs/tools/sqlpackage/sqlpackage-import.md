---
title: SqlPackage Import
description: Learn how to automate database development tasks with SqlPackage.exe Import. View examples and available parameters, properties, and SQLCMD variables.
ms.prod: sql
ms.prod_service: sql-tools
ms.technology: tools-other
ms.topic: conceptual
ms.assetid: 198198e2-7cf4-4a21-bda4-51b36cb4284b
author: "dzsquared"
ms.author: "drskwier"
ms.reviewer: "maghan; sstein"
ms.date: 6/10/2021
---

# SqlPackage Import parameters and properties
The SqlPackage.exe Import action imports the schema and table data from a BACPAC package - .bacpac file - into a new or empty database in SQL Server or Azure SQL Database. At the time of the import operation to an existing database the target database cannot contain any user-defined schema objects. Alternatively, a new database can be created by the import action when the authenticated user has [create database permissions](/sql/t-sql/statements/create-database-transact-sql#permissions).

## Command-line syntax

**SqlPackage.exe** initiates the actions specified using the parameters, properties, and SQLCMD variables specified on the command line.  
  
```bash
SqlPackage {parameters}{properties}{SQLCMD Variables}  
```

## Parameters for the Import action

|Parameter|Short Form|Value|Description|
|---|---|---|---|
|**/Action:**|**/a**|Import|Specifies the action to be performed. |
|**/AccessToken:**|**/at**|{string}| Specifies the token based authentication access token to use when connect to the target database. |
|**/Diagnostics:**|**/d**|{True&#124;False}|Specifies whether diagnostic logging is output to the console. Defaults to False. |
|**/DiagnosticsFile:**|**/df**|{string}|Specifies a file to store diagnostic logs. |
|**/MaxParallelism:**|**/mp**|{int}| Specifies the degree of parallelism for concurrent operations running against a database. The default value is 8. |
|**/Properties:**|**/p**|{PropertyName}={Value}|Specifies a name value pair for an action-specific property;{PropertyName}={Value}. Refer to the help for a specific action to see that action's property names. Example: sqlpackage.exe /Action:Import /?.|
|**/Quiet:**|**/q**|{True&#124;False}|Specifies whether detailed feedback is suppressed. Defaults to False.|
|**/SourceFile:**|**/sf**|{string}|Specifies a source file to be used as the source of action. If this parameter is used, no other source parameter shall be valid. |
|**/TargetConnectionString:**|**/tcs**|{string}|Specifies a valid SQL Server/Azure connection string to the target database. If this parameter is specified, it shall be used exclusively of all other target parameters. |
|**/TargetDatabaseName:**|**/tdn**|{string}|Specifies an override for the name of the database that is the target of sqlpackage.exe Action. |
|**/TargetEncryptConnection:**|**/tec**|{True&#124;False}|Specifies if SQL encryption should be used for the target database connection. |
|**/TargetPassword:**|**/tp**|{string}|For SQL Server Auth scenarios, defines the password to use to access the target database. |
|**/TargetServerName:**|**/tsn**|{string}|Defines the name of the server hosting the target database. |
|**/TargetTimeout:**|**/tt**|{int}|Specifies the timeout for establishing a connection to the target database in seconds. For Azure AD, it is recommended that this value be greater than or equal to 30 seconds.|
|**/TargetTrustServerCertificate:**|**/ttsc**|{True&#124;False}|Specifies whether to use TLS to encrypt the target database connection and bypass walking the certificate chain to validate trust. |
|**/TargetUser:**|**/tu**|{string}|For SQL Server Auth scenarios, defines the SQL Server user to use to access the target database. |
|**/TenantId:**|**/tid**|{string}|Represents the Azure AD tenant ID or domain name. This option is required to support guest or imported Azure AD users as well as Microsoft accounts such as outlook.com, hotmail.com, or live.com. If this parameter is omitted, the default tenant ID for Azure AD will be used, assuming that the authenticated user is a native user for this AD. However, in this case any guest or imported users and/or Microsoft accounts hosted in this Azure AD are not supported and the operation will fail. <br/> For more information about Active Directory Universal Authentication, see [Universal Authentication with SQL Database and Azure Synapse Analytics (SSMS support for MFA)](/azure/sql-database/sql-database-ssms-mfa-authentication).|
|**/UniversalAuthentication:**|**/ua**|{True&#124;False}|Specifies if Universal Authentication should be used. When set to True, the interactive authentication protocol is activated supporting MFA. This option can also be used for Azure AD authentication without MFA, using an interactive protocol requiring the user to enter their username and password or integrated authentication (Windows credentials). When /UniversalAuthentication is set to True, no Azure AD authentication can be specified in SourceConnectionString (/scs). When /UniversalAuthentication is set to False, Azure AD authentication must be specified in SourceConnectionString (/scs). <br/> For more information about Active Directory Universal Authentication, see [Universal Authentication with SQL Database and Azure Synapse Analytics (SSMS support for MFA)](/azure/sql-database/sql-database-ssms-mfa-authentication).|

## Properties specific to the Import action

|Property|Value|Description|
|---|---|---|
|**/p:**|CommandTimeout=(INT32 '60')|Specifies the command timeout in seconds when executing queries against SQL Server.|
|**/p:**|DatabaseEdition=({Basic&#124;Standard&#124;Premium&#124;DataWarehouse&#124;GeneralPurpose&#124;BusinessCritical&#124;Hyperscale&#124;Default} 'Default')|Defines the edition of an Azure SQL Database.|
|**/p:**|DatabaseLockTimeout=(INT32 '60')| Specifies the database lock timeout in seconds when executing queries against SQLServer. Use -1 to wait indefinitely.|
|**/p:**|DatabaseMaximumSize=(INT32)|Defines the maximum size in GB of an Azure SQL Database.|
|**/p:**|DatabaseServiceObjective=(STRING)|Defines the performance level of an Azure SQL Database such as"P0" or "S1".|
|**/p:**|DisableIndexesForDataPhase=(BOOLEAN TRUE)|When true (default), disables indexes before importing data into SQL Server. When false, indexes are not rebuilt. |
|**/p:**|ImportContributorArguments=(STRING)|Specifies deployment contributor arguments for the deployment contributors. This should be a semi-colon delimited list of values.|
|**/p:**|ImportContributors=(STRING)|Specifies the deployment contributors, which should run when the bacpac is imported. This should be a semi-colon delimited list of fully qualified build contributor names or IDs.|
|**/p:**|ImportContributorPaths=(STRING)|Specifies paths to load additional deployment contributors. This should be a semi-colon delimited list of values. |
|**/p:**|LongRunningCommandTimeout=(INT32)| Specifies the long running command timeout in seconds when executing queries against SQL Server. Use 0 to wait indefinitely.|
|**/p:**|RebuildIndexesOfflineForDataPhase=(BOOLEAN FALSE)|When true, rebuilds indexes offline after importing data into SQL Server.|
|**/p:**|Storage=({File&#124;Memory})|Specifies how elements are stored when building the database model. For performance reasons the default is InMemory. For large databases, File backed storage is required.|
  
## Next Steps

- Learn more about [sqlpackage](sqlpackage.md)