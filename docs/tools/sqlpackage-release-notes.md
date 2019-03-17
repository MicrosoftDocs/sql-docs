---
title: Microsoft DacFx & sqlpackage release notes| Microsoft Docs
description: 'Microsoft sqlpackage  release notes'
ms.custom: "tools|sos"
ms.date: "06/20/2018"
ms.prod: sql
ms.reviewer: "alayu; sstein"
ms.prod_service: sql-tools
ms.topic: conceptual
author: "pensivebrian"
ms.author: "broneill"
manager: kenvh
---
# sqlpackage release notes

**[Download the latest version](sqlpackage-download.md)**

## sqlpackage 18.1

Release date: February 1, 2019  
Build: 15.0.4316.1 

The release includes the following features and fixes:

- Added support for UTF8 collations.
- Performance fix to use the legacy cardinality estimator for reverse engineering queries.
- Enabled non-clustered columnstore indexes on an indexed views.
- Fixed a significant schema compare performance issue when generating a script.
- Fixed the schema drift detection logic to ignore certain xevent sessions.
- Fixed import ordering for graph tables.
- Fixed exporting external tables with object permissions.
- Moved to .NET Core 2.2 
- Use memory backed storage for schema compare on .NET Core.

This release includes cross-platform preview builds of sqlpackage that target .NET Core 2.2, and can run on macOS and Linux. This preview release has the following known issues:

- Build and deployment contributors aren't supported.
- Older .dacpac and .bacpac files that use json data serialization aren't supported.
- Referenced .dacpacs (for example master.dacpac) may not resolve due to issues with case-sensitive file systems.
  - A workaround is to capitalize the name of the reference file (for example MASTER.BACPAC).
## sqlpackage 18.0

Release date: October 24, 2018  
Build: 15.0.4200.1 

The release includes the following features and fixes:

- Added support for database compatibility level 150.
- Added support for Managed Instances.
- Added MaxParallelism command-line parameter to specify the degree of parallelism for database operations.
- Add AccessToken command-line parameter to specify an authentication token when connecting to SQL Server.
- Added support to stream BLOB/CLOB data types for imports.
- Added support for scalar UDF 'INLINE' option.
- Added support for graph table 'MERGE' syntax.
- Fixed unresolved pseudo-column for graph tables.
- Fixed creating a database with memory optimized file groups when memory optimized tables are used.
- Fixed including extended properties on external tables.

## sqlpackage 17.8

Release date: June 22, 2018  
Build: 14.0.4079.2  

The release includes the following fixes:

- Improved error messages for connection failures, including the SqlClient exception message.
- Support index compression on single partition indexes for import/export.
- Fixed a reverse engineering issue for XML column sets with SQL 2017 and later.
- Fixed an issue where scripting the database compatibility level 140 was ignored for Azure SQL Database.

## sqlpackage 17.4.1

Release date: January 25, 2018  
Build: 14.0.3917.1

The release includes the following fixes:

- When importing an Azure SQL Database .bacpac to an on-premise instance, fixed errors due to 'Database master keys without password are not supported in this version of SQL Server'.
- Database catalog collation support.
- Fixed an unresolved pseudo column error for graph tables.
- Added ThreadMaxStackSize command-line parameter to parse TSQL with a large number of nested statements.
- Fixed using the SchemaCompareDataModel with SQL authentication to compare schemas.

## sqlpackage 17.4.0

Release date: December 12, 2017  
Build: 14.0.3881.1

The release includes the following fixes:

- Do not block when encountering a database compatibility level not understood. Instead, the latest Azure SQL Database or on-premise platform will be assumed.
- Added support for 'temporal retention policy' on SQL2017+ and Azure SQL Database.
- Added /DiagnosticsFile:"C:\Temp\sqlpackage.log" command-line parameter to specify a file path to save diagnostic information.
- Added /Diagnostics command-line parameter to log diagnostic information to the console.

