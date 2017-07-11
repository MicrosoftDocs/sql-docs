---
# required metadata

title: Export and import databases on Linux | Microsoft Docs
description: 
author: MikeRayMSFT 
ms.author: mikeray 
manager: jhubbard
ms.date: 03/17/2017
ms.topic: article
ms.prod: sql-linux
ms.technology: database-engine
ms.assetid: 5befd8fa-a451-4cde-a6b9-121f20cc41ea

# optional metadata

# keywords: ""
# ROBOTS: ""
# audience: ""
# ms.devlang: ""
# ms.reviewer: ""
# ms.suite: ""
# ms.tgt_pltfrm: ""
ms.custom: H1Hack27Feb2017

---
# Use SqlPackage to export and import SQL Server databases on Linux

Use `SqlPackage` to create and deploy SQL Server databases and DACPAC packages. `SqlPackage` is a command-line utility for extraction and deployment of database snapshots and other data migration tasks. This tool makes use of the DACPAC format - a self-contained unit of SQL Server database deployment which allows the migration of data in a portable package.  

## SqlPackage location

SqlPackage can be found in an executable file that is in the following location:

```bash 
/opt/mssql/bin/SqlPackage 
```

This file can be executed using the following pattern: 

```bash
./SqlPackage /Action:{action} {parameters}  
./SqlPackage /Action:Extract /SourceServerName:localhost /SourceDatabaseName:your_database /TargetFile:"/path/to/your/file.dacpac" /SourceUser:"sa"
```

## Use the following command to extract a DACPAC file from an existing database with SqlPackage 

Use the following command to extract a DACPAC file from an existing database with SqlPackage: 

```bash
./sqlpackage /Action:Extract /SourceServerName:"localhost/your_server" /SourceDatabaseName:"your_database" /SourceUser:"your_username" /SourcePassword:"your_password" /TargetFile:"/absolute/path/to/your/target/file.dacpac" 
```
 
Using the `Extract` action, you can provide the `SourceServerName`, `SourceDatabaseName`, `SourceUser`, and `SourcePassword` to connect and extract the DACPAC file to a local TargetFile. 

> **Note**: The `"/p:Storage=Memory"` option is the only one supported at this moment. 

## Extract a DACPAC file from an existing database into another database 

Use the following command to extract a DACPAC file from an existing database and deploy it to another database server. 

```bash
./sqlpackage /Action:Extract /SourceServerName:"localhost/your_server" /SourceDatabaseName:"your_database" /SourceUser:"your_username" /SourcePassword:"your_password" /TargetServerName:"target_server" /TargetDatabaseName:"target_database" /TargetUser:"target_username" /TargetPassword:"target_password" 
```

Using the `Extract` action, you can provide the `SourceServerName`, `SourceDatabaseName`, `SourceUser`, and `SourcePassword` to connect and extract the DACPAC file. Afterwards, SqlPackage will connect to a TargetServerName using the `TargetDatabaseName`, `TargetUser`, `TargetPassword`. Note: The `"/p:Storage=Memory"` option is the only one supported at this moment. 

## See also

- [Data-tier Applications](http://msdn.microsoft.com/library/ee210546.aspx)