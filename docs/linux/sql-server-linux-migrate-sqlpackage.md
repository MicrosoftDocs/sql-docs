---
# required metadata

title: Export and import a SQL Server database on Linux | Microsoft Docs
description: 
author: sanagama 
ms.author: sanagama 
manager: jhubbard
ms.date: 03/17/2017
ms.topic: article
ms.prod: sql-linux
ms.technology: database-engine
ms.assetid: e239793b-e630-4110-81cc-19b1ef8bb8b0

# optional metadata
# keywords: ""
# ROBOTS: ""
# audience: ""
# ms.devlang: ""
# ms.reviewer: ""
# ms.suite: ""
# ms.tgt_pltfrm: ""
# ms.custom: ""

---
# Export and import a database on Linux with sqlpackage

This topic shows how to the **sqlpackage** tool to export and import databases to SQL Server 2017 RC0 on Linux. 

> [!NOTE]
> If you are migrating a database from one SQL Server instance to another, the recommendation is to use [Backup and restore](sql-server-linux-migrate-restore-database.md).

## Overview
The sqlpackage command line utility can be used to perform a number of operations on databases such as:
- export a database (database schema and user data) to a .bacpac file
- import a database (database schema and/or user data) from a .bacpac file
- create a database snapshot (.dacpac) file for a database
- incrementally update a database schema to match the schema of a source .dacpac file
- create a Transact-SQL incremental update script that updates the schema of a target database to match the schema of a source database

You can use the sqlpackage command line utility with Microsoft SQL Server running on-premises or in the cloud, on Linux, Windows or Docker and from Azure SQL Database. The sqlpackage command line utility is particularly useful when you wish to import a database from Azure SQL Database to SQL Server on Linux or vice-versa.
 
## sqlpackage location

The sqlpackage command line utility is in the following location after you install SQL Server on Linux. Running sqlpackage with no command line parameters displays help about the available parameters:

```bash 
/opt/mssql/bin/sqlpackage
```

You can specify command line parameters for a number of actions along with action specific parameters and properties as follows:

```bash
/opt/mssql/bin/sqlpackage {parameters}{properties}{SQLCMD Variables} 
/opt/mssql/bin/sqlpackage /Action:Extract /SourceServerName:tcp:<your_server> /SourceDatabaseName:<your_database> /TargetFile:</path/to/your/file.dacpac> /SourceUser:<username> /SourcePassword:<password>
```

Command line parameters can be specified using their short forms:

```bash
/opt/mssql/bin/sqlpackage /a:Export /ssn:tcp:<your_server> /sdn:<your_database> /tf:</path/to/your/file.bacpac> /su:<username> /sp:<password>
```

## Export a database (schema and user data) to a .BACPAC file

Use the following command to export database schema and user data to a .BACPAC file:

```bash
/opt/mssql/bin/sqlpackage /a:Export /ssn:tcp:<your_server> /sdn:<your_database> /su:<username> /sp:<password> /tf:</path/to/your/file.bacpac> 
```

## Import a database (schema and user data) from a .BACPAC file

Use the following command to import database schema and user data from a .BACPAC file:

```bash
/opt/mssql/bin/sqlpackage /a:Import /tsn:tcp:<your_server> /tdn:<your_database> /tu:<username> /tp:<password> /sf:</path/to/your/file.bacpac>

```

## See also
- [sqlpackage command line utility](https://msdn.microsoft.com/library/hh550080.aspx)
- [Data-tier Applications](http://msdn.microsoft.com/library/ee210546.aspx)
