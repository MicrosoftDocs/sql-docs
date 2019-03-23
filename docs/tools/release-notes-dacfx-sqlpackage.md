---
title: DacFx and SqlPackage release notes | Microsoft Docs
description: Microsoft sqlpackage release notes
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
# Release notes for SqlPackage.exe of DacFx

**[Download the latest version](sqlpackage-download.md)**

This article lists the features and fixes delivered by the released versions of SqlPackage.exe for the Microsoft Data-Tier Application Framework (DacFx).

## 18.1 sqlpackage

Release date: &nbsp; February 1, 2019  
Build: &nbsp; 15.0.4316.1  
Preview release.

### Features

| Feature | Details |
| :------ | :------ |
| Added support for UTF8 collations. | &nbsp; |
| Enabled non-clustered columnstore indexes on an indexed view. | &nbsp; |
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
