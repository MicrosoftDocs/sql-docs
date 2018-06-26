---
title: Microsoft DacFx & sqlpackage release notes| Microsoft Docs
description: 'Microsoft sqlpackage  release notes'
ms.custom: "tools|sos"
ms.date: "06/20/2018"
ms.prod: sql
ms.reviewer: "alayu; sstein"
ms.suite: "sql"
ms.prod_service: sql-tools
ms.component: sqlpackage
ms.tgt_pltfrm: ""
ms.topic: conceptual
author: "pensivebrian"
ms.author: "broneill"
manager: kenvh
---
# sqlpackage release notes

**[Download the latest version](sql-package-download.md)**

## sqlpackage 17.8

release date: June 22, 2018  
build: 14.0.4079.2  

The release contains the following highlights:  

- Improved error messages for connection failures.  Also include the SqlClient exception message.
- Added MaxParallelism command line parameter to specify the degree of parallelism for database operations.
- Support index compression on single partition indexes for import/export.
- Fixed a reverse engineering issue for XML column sets with SQL 2017 and later.
- Fixed an issue where scripting the database compat level 140 was ignored for Azure SQL Database.

## sqlpackage 17.4.1

release date: January 25, 2018  
build: 14.0.3917.1

This build contains the following highlights:  

- When importing .bacpac from Azure SQL Database using a database master key without a password to an on-premise database, create a database master key with a password
- Database catalog collation support.
- Fix unresolved pseudo columns for graph tables.
- Added ThreadMaxStackSize command line parameter for models with TSQL statements having a very large number of nested SQL statements.
- Fix using the SchemaCompareDataModel and SQL authentication to compare schemas.

## sqlpackage 17.4.0

release date: December 12, 2017  
build: 14.0.3881.1

This build contains the following highlights:  

- Added /d command line parameter to log diagnostic information to the console.- Do not block when encountering a database compat level not understood. Instead, the latest Azure SQL Database or on-premise SqlPlatform will be assumed.
- Added support for 'temporal retention policy' on  SQL2017+ and Azure SQL Database.
- Added /df:"C:\Temp\sqlpackage.log" command line parameter to specify a file path to save diagnostic information.
- Added /d command line parameter to log diagnostic information to the console.

## sqlpackage on macOS and Linux 0.0.1 (preview)

release date: May 9, 2018  
build: 15.0.4057.1

This build contains the cross-platform preview build of sqlpackage that targets .NET Core 2.0, and can run macOS and Linux. 

This is an early preview with following known issues:

- The /p:CommandTimeout parameter is hard coded to 120
- Build and deployment contributors are not supported
  - Will be fixed in the move to .NET Core 2.1 where System.ComponentModel.Composition.dll is supported
  - Need to handle case-sensitive paths
- SQL CLR UDT types are not supported.
  - This includes SQL Server Types SqlGeography, SqlGeometry, & SqlHierarchyId
- Older .dacpac and .bacpac files that use json data serialization are not supported
- Referenced .dacpacs (e.g. master.dacpac) may not resolve due to issues with case-sensitive file systems.
  - A workaround is to capitalize the name of the reference file (e.g. MASTER.BACPAC)