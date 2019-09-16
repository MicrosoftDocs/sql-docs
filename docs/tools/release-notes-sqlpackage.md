---
title: DacFx and SqlPackage release notes | Microsoft Docs
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
| Azure SQL Data Warehouse (preview) | Add support to deploy to Azure SQL Data Warehouse. | 
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
| Add graph table support for edge constraints and edge constraint clauses. | &nbsp; |
| Enabled model validation rule to support 32 columns for index keys for SQL Server 2016 and up. | &nbsp; |
| &nbsp; | &nbsp; |

### Fixes

| Fix | Details |
| :-- | :------ |
| Fix reverse engineering a SQL Server 2016 RTM database due to an unsupported query hint being used. | &nbsp; |
| Fix deployment ordering of auto close alter statements to occur before create filegroup statements. | &nbsp; |
| Fix ScriptDom parsing regression where the 'URL' string was interpreted as a top level token. | &nbsp; |
| Fix a null reference exception when parsing an alter table add index statement. | &nbsp; |
| Fixed schema compare for nullable persisted computed columns always showing as different.| &nbsp; |
| &nbsp; | &nbsp; |

## 18.1 sqlpackage

Release date: &nbsp; February 1, 2019  
Build: &nbsp; 15.0.4316.1  
Preview release.

### Features

| Feature | Details |
| :------ | :------ |
| Added support for UTF8 collations. | &nbsp; |
| Enabled nonclustered columnstore indexes on an indexed view. | &nbsp; |
| Moved to .NET Core 2.2. | &nbsp; |
| Use memory backed storage for schema compare on .NET Core. | &nbsp; |
| &nbsp; | &nbsp; |

### Fixes

| Fix | Details |
| :-- | :------ |
| Performance fix to use the legacy cardinality estimator for reverse engineering queries. | &nbsp; |
| Fixed a significant schema compare performance issue when generating a script. | &nbsp; |
| Fixed the schema drift detection logic to ignore certain extended event (xevent) sessions. | &nbsp; |
| Fixed import ordering for graph tables. | &nbsp; |
| Fixed exporting external tables with object permissions. | &nbsp; |
| &nbsp; | &nbsp; |

### Known issues

This release includes cross-platform preview builds of sqlpackage that target .NET Core 2.2. The sqlpackage can run on macOS and Linux.

| Known issue | Details |
| :---------- | :------ |
| Build and deployment contributors aren't supported. | &nbsp; |
| Older .dacpac and .bacpac files that use json data serialization aren't supported. | &nbsp; |
| Referenced .dacpacs (for example master.dacpac) may not resolve due to issues with case-sensitive file systems. | A workaround is to capitalize the name of the reference file (for example MASTER.BACPAC). |
| &nbsp; | &nbsp; |

## 18.0 sqlpackage

Release date: &nbsp; October 24, 2018  
Build: &nbsp; 15.0.4200.1

### Features

| Feature | Details |
| :------ | :------ |
| Added support for database compatibility level 150. | &nbsp; |
| Added support for Managed Instances. | &nbsp; |
| Added MaxParallelism command-line parameter to specify the degree of parallelism for database operations. | &nbsp; |
| Added AccessToken command-line parameter to specify an authentication token when connecting to SQL Server. | &nbsp; |
| Added support to stream BLOB/CLOB data types for imports. | &nbsp; |
| Added support for scalar UDF 'INLINE' option. | &nbsp; |
| Added support for graph table 'MERGE' syntax. | &nbsp; |
| &nbsp; | &nbsp; |

### Fixes

| Fix | Details |
| :-- | :------ |
| Fixed unresolved pseudo-column for graph tables. | &nbsp; |
| Fixed creating a database with memory optimized file groups when memory optimized tables are used. | &nbsp; |
| Fixed including extended properties on external tables. | &nbsp; |
| &nbsp; | &nbsp; |

## 17.8 sqlpackage

Release date: &nbsp; June 22, 2018  
Build: &nbsp; 14.0.4079.2

### Features

| Feature | Details |
| :------ | :------ |
| Improved error messages for connection failures, including the SqlClient exception message. | &nbsp; |
| Support index compression on single partition indexes for import/export. | &nbsp; |
| &nbsp; | &nbsp; |

### Fixes

| Fix | Details |
| :-- | :------ |
| Fixed a reverse engineering issue for XML column sets with SQL 2017 and later. | &nbsp; |
| Fixed an issue where scripting the database compatibility level 140 was ignored for Azure SQL Database. | &nbsp; |
| &nbsp; | &nbsp; |

## 17.4.1 sqlpackage

Release date: &nbsp; January 25, 2018  
Build: &nbsp; 14.0.3917.1

### Features

| Feature | Details |
| :------ | :------ |
| Added ThreadMaxStackSize command-line parameter to parse Transact-SQL with a large number of nested statements. | &nbsp; |
| Database catalog collation support. | &nbsp; |
| &nbsp; | &nbsp; |

### Fixes

| Fix | Details |
| :-- | :------ |
| When importing an Azure SQL Database .bacpac to an on-premise instance, fixed errors due to _Database master keys without password are not supported in this version of SQL Server_. | &nbsp; |
| Fixed an unresolved pseudo column error for graph tables. | &nbsp; |
| Fixed using the SchemaCompareDataModel with SQL authentication to compare schemas. | &nbsp; |
| &nbsp; | &nbsp; |

## 17.4.0 sqlpackage

Release date: &nbsp; December 12, 2017  
Build: &nbsp; 14.0.3881.1

### Features

| Feature | Details |
| :------ | :------ |
| Added support for _temporal retention policy_ on SQL 2017+ and Azure SQL Database. | &nbsp; |
| Added /DiagnosticsFile:"C:\Temp\sqlpackage.log" command-line parameter to specify a file path to save diagnostic information. | &nbsp; |
| Added /Diagnostics command-line parameter to log diagnostic information to the console. | &nbsp; |
| &nbsp; | &nbsp; |

### Fixes

| Fix | Details |
| :-- | :------ |
| Do not block when encountering a database compatibility level that is not understood. | Instead, the latest Azure SQL Database or on-premises platform will be assumed. |
| &nbsp; | &nbsp; |
