---
title: DacFx and SqlPackage release notes
description: Release notes for Microsoft sqlpackage.
ms.custom: "tools|sos"
ms.date: 02/02/2019
ms.prod: sql
ms.reviewer: "alayu; sstein"
ms.prod_service: sql-tools
ms.topic: conceptual
author: pensivebrian
ms.author: broneill
manager: kenvh
---
# Release notes for SqlPackage.exe

**[Download the latest version](sqlpackage-download.md)**

This article lists the features and fixes delivered by the released versions of SqlPackage.exe.

<!--
TODO:
The Introduction text needs to add clarity regarding the relationship between
'SqlPackage.exe' and 'DacFx' (the Microsoft 'Data-Tier Application Framework').
One added sentence would be sufficient.

Or, if there is no relationship, remove 'DacFx' from the metadata 'title:'.

I discussed this with SStein (SteveStein).
Thanks.  GeneMi (MightyPen in GitHub).  2019-03-27
-->

## 18.6 sqlpackage

|Platform|Download|Release date|Version|Build
|:---|:---|:---|:---|:---|
|Windows|[MSI Installer](https://go.microsoft.com/fwlink/?linkid=2143544)|September 18, 2020|18.6|15.0.4897.1|
|macOS .NET Core |[.zip file](https://go.microsoft.com/fwlink/?linkid=2143659)|September 18, 2020| 18.6|15.0.4897.1|
|Linux .NET Core |[.zip file](https://go.microsoft.com/fwlink/?linkid=2143497)|September 18, 2020| 18.6|15.0.4897.1|
|Windows .NET Core |[.zip file](https://go.microsoft.com/fwlink/?linkid=2143496)|September 18, 2020| 18.6|15.0.4897.1|

### Features
| Feature | Details |
| :------ | :------ |
| Platform | Updated sqlpackage for .NET Core version to .NET Core 3.1 |
| Always Encrypted | Added support for secure enclave import and export for SQL Server 2019 |
| Deployment | Added support to ignore change data capture enabled tables when exporting from Azure SQL Database |
| Deployment | Added support for index option OPTIMIZE_FOR_SEQUENTIAL_KEY in Azure SQL Database |
| Deployment | Added support for identity columns for Azure SQL Data Warehouse | 

### Fixes
| Feature | Details |
| :------ | :------ | 
| Deployment | Fixed an incorrect deployment script generated when targeting Azure SQL Database Managed Instance as a non-sysadmin user  | 
| Deployment | Fixed loading deployment contributors when running script actions | 
| Help | Output correct elapsed time in sqlpackage when operation take longer than 1 day | 
| Help | Output the sqlpackage version in the help (/?) and support the /version parameter | 
| Deployment | Fixed dacpac registration when deploying for .NET Core | 
| Deployment | Fixed sqlpackage on .NET Core handling of the /accessToken (/at) parameter | 
| Deployment | Allow ALTER TABLE statements in stored procedures as non-top level statements | 
| Deployment | Fixed Azure SQL Data Warehouse validation of materialized views to be case insensitive | 

### Known Issues
| Feature | Details |
| :------ | :------ |
| Deployment | The Azure SQL Data Warehouse Workload Management feature (Workload Groups and Workload Classifiers) is not yet supported | 

## 18.5.1 sqlpackage

|Platform|Download|Release date|Version|Build
|:---|:---|:---|:---|:---|
|Windows|[MSI Installer](https://go.microsoft.com/fwlink/?linkid=2134206)|June 24, 2020|18.5.1|15.0.4826.1|
|macOS .NET Core |[.zip file](https://go.microsoft.com/fwlink/?linkid=2134312)|June 24, 2020| 18.5.1|15.0.4826.1|
|Linux .NET Core |[.zip file](https://go.microsoft.com/fwlink/?linkid=2134311)|June 24, 2020| 18.5.1|15.0.4826.1|
|Windows .NET Core |[.zip file](https://go.microsoft.com/fwlink/?linkid=2134310)|June 24, 2020| 18.5.1|15.0.4826.1|

### Fixes
| Feature | Details |
| :------ | :------ |
| Deployment | Fixed a regression that was introduced in 18.5 causing there to be an “Incorrect syntax near 'type'” error when deploying a dacpac or importing a bacpac with a user with external login to on premise | 

## 18.5 sqlpackage

|Platform|Download|Release date|Version|Build
|:---|:---|:---|:---|:---|
|Windows|[MSI Installer](https://go.microsoft.com/fwlink/?linkid=2128142)|April 28, 2020|18.5|15.0.4769.1|
|macOS .NET Core |[.zip file](https://go.microsoft.com/fwlink/?linkid=2128145)|April 28, 2020| 18.5|15.0.4769.1|
|Linux .NET Core |[.zip file](https://go.microsoft.com/fwlink/?linkid=2128144)|April 28, 2020| 18.5|15.0.4769.1|
|Windows .NET Core |[.zip file](https://go.microsoft.com/fwlink/?linkid=2128143)|April 28, 2020| 18.5|15.0.4769.1|

### Features
| Feature | Details |
| :------ | :------ |
| Deployment | Data Sensitivity classification now supported for SQL Server 2008 and up, Azure SQL Database, and Azure SQL Data Warehouse |
| Deployment | Add Azure SQL Data Warehouse support for table constraints |
| Deployment | Add Azure SQL Data Warehouse support for ordered clustered column store index |
| Deployment | Add support for External Data Source (for Oracle, Teradata, MongoDB/CosmosDB, ODBC, Big Data Cluster) and External Table for SQL Server 2019 Big Data Cluster |
| Deployment | Add SQL Database Edge Instance as supported edition |
| Deployment | Support Managed Instance server names of the form '\<server>.\<dnszone>.database.windows.net' |
| Deployment | Add support for copy command in Azure SQL Data Warehouse |
| Deployment | Add deployment option 'IgnoreTablePartitionOptions' during Publish to avoid table recreation when there is change in partition function on table for Azure SQL Data Warehouse |
| .NET Core | Add support for Microsoft.Data.SqlClient in .NET Core version of sqlpackage |
| &nbsp; | &nbsp; |

### Fixes
| Fix | Details |
| :-- | :------ |
| Deployment | Fix parsing json path as expression |
| Deployment | Fix generating GRANT statements for AlterAnyDatabaseScopedConfiguration and AlterAnySensitivityClassification permissions |
| Deployment | Fix External Script permission not being recognized |
| Deployment | Fix for inline property - the implicit addition of the property should not show in difference but explicit mention should show through script |
| Deployment | Resolved an issue where changing a Table referenced by a Materialized View (MV) causes Alter View statements to be generated which is not supported for MVs for Azure SQL Data Warehouse |
| Deployment | Fix publish failing when adding column to a table with data for Azure SQL Data Warehouse |
| Deployment | Fix update script should move data to a new table when changing the distribution column type (data loss scenario) for Azure SQL Data Warehouse |
| ScriptDom | Fix ScriptDom bug where it couldn't recognize inline constraints defined after an inline index |
| ScriptDom | Fix ScriptDom SYSTEM_TIME missing closing parenthesis when in a batch statement |
| Always Encrypted | Fix #tmpErrors table failing to drop if sqlpackage reconnects and the temp table is already gone because the temporary table goes away when the connection dies |
| &nbsp; | &nbsp; |

### Known Issues
| Feature | Details |
| :------ | :------ |
| Deployment |  A regression was introduced in 18.5 causing there to be an “Incorrect syntax near 'type'” error when deploying a dacpac or importing a bacpac with a user with external login to on premise. Workaround is to use sqlpackage 18.4 and it will be fixed in the next sqlpackage release. | 
| .NET Core | Importing bacpacs with Sensitivity Classification fails with "Internal connection fatal error" because of this [known issue](https://github.com/dotnet/SqlClient/issues/559) in Microsoft.Data.SqlClient. This will be fixed in the next sqlpackage release. |
| &nbsp; | &nbsp; |

## 18.4.1 sqlpackage

|Platform|Download|Release date|Version|Build
|:---|:---|:---|:---|:---|
|Windows|[MSI Installer](https://go.microsoft.com/fwlink/?linkid=2113703)|December 13, 2019|18.4.1|15.0.4630.1|
|macOS .NET Core |[.zip file](https://go.microsoft.com/fwlink/?linkid=2113705)|December 13, 2019| 18.4.1|15.0.4630.1|
|Linux .NET Core |[.zip file](https://go.microsoft.com/fwlink/?linkid=2113331)|December 13, 2019| 18.4.1|15.0.4630.1|
|Windows .NET Core |[.zip file](https://go.microsoft.com/fwlink/?linkid=2113704)|December 13, 2019| 18.4.1|15.0.4630.1|

### Fixes
| Fix | Details |
| :-- | :------ |
| ScriptDom |  A ScriptDom parsing regression was introduced in 18.3.1 where 'RENAME' is incorrectly treated as a top-level token, cause parsing to fail.
| &nbsp; | &nbsp; |

### Known Issues 

| Feature | Details |
| :------ | :------ |
| Deployment |  A regression was introduced in 18.4.1 causing there to be a “Object reference not set to an instance of an object.” error when deploying a dacpac or importing a bacpac with a user with external login. Workaround is to use sqlpackage 18.4 and it will be fixed in the next sqlpackage release. | 
| &nbsp; | &nbsp; |

## 18.4 sqlpackage

|Platform|Download|Release date|Version|Build
|:---|:---|:---|:---|:---|
|Windows|[MSI Installer](https://go.microsoft.com/fwlink/?linkid=2108813)|October 29, 2019|18.4|15.0.4573.2|
|macOS .NET Core |[.zip file](https://go.microsoft.com/fwlink/?linkid=2108815)|October 29, 2019| 18.4|15.0.4573.2|
|Linux .NET Core |[.zip file](https://go.microsoft.com/fwlink/?linkid=2108814)|October 29, 2019| 18.4|15.0.4573.2|
|Windows .NET Core |[.zip file](https://go.microsoft.com/fwlink/?linkid=2109019)|October 29, 2019| 18.4|15.0.4573.2|

### Features

| Feature | Details |
| :------ | :------ |
| Deployment | Add support to deploy to Azure SQL Data Warehouse (GA). | 
| Platform | sqlpackage .NET Core GA for macOS, Linux, and Windows. | 
| Security | Remove SHA1 code signing. |
| Deployment | Add support for new Azure database editions: GeneralPurpose, BusinessCritical, Hyperscale |
| Deployment | Add Managed Instance support for AAD user and groups. |
| Deployment | Support the /AccessToken parameter for sqlpackage on .NET Core. |
| &nbsp; | &nbsp; |

### Known Issues 

| Feature | Details |
| :------ | :------ |
| ScriptDom |  A ScriptDom parsing regression was introduced in 18.3.1 where 'RENAME' is incorrectly treated as a top-level token, cause parsing to fail. This will be fixed in the next sqlpackage release. | 
| &nbsp; | &nbsp; |

### Known Issues for .NET Core

| Feature | Details |
| :------ | :------ |
| Import |  For .bacpac files with compressed files over 4GB in size, you might need to use the .NET Core version of sqlpackage to perform the import.  This behavior is due to how .NET Core generates zip headers, which although valid, are not readable by the .NET Full Framework version of sqlpackage. | 
| Deployment | The parameter /p:Storage=File is not supported. Only Memory is supported on .NET Core. | 
| Always Encrypted | sqlpackage .NET Core does not support Always Encrypted columns. | 
| Security | sqlpackage .NET Core does not support the /ua parameter for multi-factor authentication. | 
| Deployment | Older V2 .dacpac and .bacpac files that use json data serialization aren't supported. |
| &nbsp; | &nbsp; |

## 18.3.1 sqlpackage

|Platform|Download|Release date|Version|Build
|:---|:---|:---|:---|:---|
|Windows|[MSI Installer](https://go.microsoft.com/fwlink/?linkid=2102893)|September 13, 2019|18.3.1|15.0.4538.1|
|macOS .NET Core (preview)|[.zip file](https://go.microsoft.com/fwlink/?linkid=2102894)|September 13, 2019| 18.3.1|15.0.4538.1|
|Linux .NET Core (preview)|[.zip file](https://go.microsoft.com/fwlink/?linkid=2102978)|September 13, 2019| 18.3.1|15.0.4538.1|
|Windows .NET Core (preview)|[.zip file](https://go.microsoft.com/fwlink/?linkid=2102979)|September 13, 2019| 18.13.1|15.0.4538.1|

### Features

| Feature | Details |
| :------ | :------ |
| Deployment | Add support to deploy to Azure SQL Data Warehouse (preview). | 
| Deployment | Add /p:DatabaseLockTimeout=(INT32 '60') parameter to sqlpackage. | 
| Deployment | Add /p:LongRunningCommandTimeout=(INT32) parameter to sqlpackage. |
| Export/Extract | Add /p:TempDirectoryForTableData=(STRING) parameter to sqlpackage. |
| Deployment | Allow deployment contributors to be loaded from additional locations. Deployment contributors will be loaded from the same directory as the target .dacpac being deployed, the Extensions directory relative to the sqlpackage.exe binary, and the /p:AdditionalDeploymentContributorPaths=(STRING) parameter added to sqlpackage where additional directory locations can be specified. |
| Deployment | Add support for OPTIMIZE_FOR_SEQUENTIAL_KEY. |
| &nbsp; | &nbsp; |

### Fixes

| Fix | Details |
| :-- | :------ |
| Deployment | Fix to ignore automatic indexes so that they are not dropped on deployment. | 
| Always Encrypted | Fix for handling Always Encrypted varchar columns. | 
| Build/Deployment | Fix to resolve the nodes() method for xml column sets.| 
| ScriptDom | Fix additional cases where the 'URL' string was interpreted as a top level token. | 
| Graph | Fix generated TSQL for pseudo column references in constraints.  | 
| Export | Generate random passwords that meet complexity requirements. | 
| Deployment | Fix to honor command timeouts when retrieving constraints. | 
| .NET Core (preview) | Fix diagnostic logging to a file. | 
| .NET Core (preview) | Use streaming to export table data to support large tables. | 
| &nbsp; | &nbsp; |

## 18.2 sqlpackage

|Platform|Download|Release date|Version|Build
|:---|:---|:---|:---|:---|
|Windows|[MSI Installer](https://go.microsoft.com/fwlink/?linkid=2087429)|April 15, 2019|18.2|15.0.4384.2|
|macOS .NET Core (preview)|[.zip file](https://go.microsoft.com/fwlink/?linkid=2087247)|April 15, 2019 | 18.2 |15.0.4384.2|
|Linux .NET Core (preview)|[.zip file](https://go.microsoft.com/fwlink/?linkid=2087431)|April 15, 2019 | 18.2 |15.0.4384.2|

### Features

| Feature | Details |
| :------ | :------ |
| Graph | Add graph table support for edge constraints and edge constraint clauses. |
| Deployment | Enabled model validation rule to support 32 columns for index keys for SQL Server 2016 and up. |
| &nbsp; | &nbsp; |

### Fixes

| Fix | Details |
| :-- | :------ |
| Deployment | Fix reverse engineering a SQL Server 2016 RTM database due to an unsupported query hint being used. |
| Deployment | Fix deployment ordering of auto close alter statements to occur before create filegroup statements. |
| ScriptDom | Fix ScriptDom parsing regression where the 'URL' string was interpreted as a top level token. |
| Deployment | Fix a null reference exception when parsing an alter table add index statement. | 
| Schema Compare | Fixed schema compare for nullable persisted computed columns always showing as different.|
| &nbsp; | &nbsp; |

## 18.1 sqlpackage

Release date: &nbsp; February 1, 2019  
Build: &nbsp; 15.0.4316.1  
Preview release.

### Features

| Feature | Details |
| :------ | :------ |
| Deployment | Added support for UTF8 collations. |
| Deployment | Enabled nonclustered columnstore indexes on an indexed view. |
| Platform | Moved to .NET Core 2.2. | 
| Schema Compare | Use memory backed storage for schema compare on .NET Core. |
| &nbsp; | &nbsp; |

### Fixes

| Fix | Details |
| :-- | :------ |
| Performance | Performance fix to use the legacy cardinality estimator for reverse engineering queries. | 
| Performance | Fixed a significant schema compare performance issue when generating a script. | 
| Schema Compare | Fixed the schema drift detection logic to ignore certain extended event (xevent) sessions. |
| Graph | Fixed import ordering for graph tables. | 
| Export | Fixed exporting external tables with object permissions. |
| &nbsp; | &nbsp; |

### Known issues

This release includes cross-platform preview builds of sqlpackage that target .NET Core 2.2. The sqlpackage can run on macOS and Linux.

| Known issue | Details |
| :---------- | :------ |
| Deployment | For .NET Core, build and deployment contributors aren't supported. | 
| Deployment | For .NET Core, older .dacpac and .bacpac files that use json data serialization aren't supported. | 
| Deployment | For .NET Core referenced .dacpacs (for example master.dacpac) may not resolve due to issues with case-sensitive file systems. | A workaround is to capitalize the name of the reference file (for example MASTER.BACPAC). |
| &nbsp; | &nbsp; |

## 18.0 sqlpackage

Release date: &nbsp; October 24, 2018  
Build: &nbsp; 15.0.4200.1

### Features

| Feature | Details |
| :------ | :------ |
| Deployment | Added support for database compatibility level 150. | 
| Deployment | Added support for Managed Instances. | 
| Performance | Added MaxParallelism command-line parameter to specify the degree of parallelism for database operations. | 
| Security | Added AccessToken command-line parameter to specify an authentication token when connecting to SQL Server. | 
| Import | Added support to stream BLOB/CLOB data types for imports. | 
| Deployment | Added support for scalar UDF 'INLINE' option. | 
| Graph | Added support for graph table 'MERGE' syntax. |
| &nbsp; | &nbsp; |

### Fixes

| Fix | Details |
| :-- | :------ |
| Graph | Fixed unresolved pseudo-column for graph tables. |
| Deployment | Fixed creating a database with memory optimized file groups when memory optimized tables are used. |
| Deployment | Fixed including extended properties on external tables. |
| &nbsp; | &nbsp; |

## 17.8 sqlpackage

Release date: &nbsp; June 22, 2018  
Build: &nbsp; 14.0.4079.2

### Features

| Feature | Details |
| :------ | :------ |
| Diagnostics | Improved error messages for connection failures, including the SqlClient exception message. |
| Deployment | Support index compression on single partition indexes for import/export. |
| &nbsp; | &nbsp; |

### Fixes

| Fix | Details |
| :-- | :------ |
| Deployment | Fixed a reverse engineering issue for XML column sets with SQL 2017 and later. | 
| Deployment | Fixed an issue where scripting the database compatibility level 140 was ignored for Azure SQL Database. |
| &nbsp; | &nbsp; |

## 17.4.1 sqlpackage

Release date: &nbsp; January 25, 2018  
Build: &nbsp; 14.0.3917.1

### Features

| Feature | Details |
| :------ | :------ |
| Import/Export | Added ThreadMaxStackSize command-line parameter to parse Transact-SQL with a large number of nested statements. |
| Deployment | Database catalog collation support. | 
| &nbsp; | &nbsp; |

### Fixes

| Fix | Details |
| :-- | :------ |
| Import | When importing an Azure SQL Database .bacpac to an on-premises instance, fixed errors due to _Database master keys without password are not supported in this version of SQL Server_. |
| Graph | Fixed an unresolved pseudo column error for graph tables. |
| Schema Compare | Fixed SQL authentication to compare schemas. | 
| &nbsp; | &nbsp; |

## 17.4.0 sqlpackage

Release date: &nbsp; December 12, 2017  
Build: &nbsp; 14.0.3881.1

### Features

| Feature | Details |
| :------ | :------ |
| Deployment |  Added support for _temporal retention policy_ on SQL 2017+ and Azure SQL Database. | 
| Diagnostics | Added /DiagnosticsFile:"C:\Temp\sqlpackage.log" command-line parameter to specify a file path to save diagnostic information. | 
| Diagnostics | Added /Diagnostics command-line parameter to log diagnostic information to the console. |
| &nbsp; | &nbsp; |

### Fixes

| Fix | Details |
| :-- | :------ |
| Deployment | Do not block when encountering a database compatibility level that is not understood. Instead, the latest Azure SQL Database or on-premises platform will be assumed. |
| &nbsp; | &nbsp; |
