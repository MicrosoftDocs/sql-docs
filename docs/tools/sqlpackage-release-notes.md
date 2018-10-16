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

## sqlpackage 17.8

Release date: June 22, 2018  
Build: 14.0.4079.2  

The release includes the following fixes:

- Improved error messages for connection failures, including the SqlClient exception message.
- Added MaxParallelism command-line  parameter to specify the degree of parallelism for database operations.
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

## sqlpackage on macOS and Linux 0.0.1 (preview)

Release date: May 9, 2018  
Build: 15.0.4057.1

This release contains the cross-platform preview build of sqlpackage that targets .NET Core 2.0, and can run on macOS and Linux. 

This release is an early preview with following known issues:

- The /p:CommandTimeout parameter is hard coded to 120.
- Build and deployment contributors aren't supported.
  - Will be fixed after moving to .NET Core 2.1 where System.ComponentModel.Composition.dll is supported.
  - Need to handle case-sensitive paths.
- SQL CLR UDT types aren't supported, including SQL Server CLR UDT Types: SqlGeography, SqlGeometry, & SqlHierarchyId.
- Older .dacpac and .bacpac files that use json data serialization aren't supported.
- Referenced .dacpacs (for example master.dacpac) may not resolve due to issues with case-sensitive file systems.
  - A workaround is to capitalize the name of the reference file (for example MASTER.BACPAC).
