---
title: SqlPackage.exe
description: Learn how to automate database development tasks with SqlPackage.exe. View examples and available parameters, properties, and SQLCMD variables.
ms.prod: sql
ms.prod_service: sql-tools
ms.technology: tools-other
ms.topic: conceptual
ms.assetid: 198198e2-7cf4-4a21-bda4-51b36cb4284b
author: "dzsquared"
ms.author: "drskwier"
ms.reviewer: "maghan; sstein"
ms.date: 11/4/2020
---

# SqlPackage.exe

**SqlPackage.exe** is a command-line utility that automates the following database development tasks:  
  
- [Version](#version): Returns the build number of the SqlPackage application.  Added in version 18.6.

- [Extract](sqlpackage-extract.md): Creates a database snapshot (.dacpac) file from a live SQL Server or Azure SQL Database.  
  
- [Publish](sqlpackage-publish.md): Incrementally updates a database schema to match the schema of a source .dacpac file. If the database does not exist on the server, the publish operation creates it. Otherwise, an existing database is updated.  
  
- [Export](sqlpackage-export.md): Exports a live database - including database schema and user data - from SQL Server or Azure SQL Database to a BACPAC package (.bacpac file).  
  
- [Import](sqlpackage-import.md): Imports the schema and table data from a BACPAC package into a new user database in an instance of SQL Server or Azure SQL Database.  
  
- [DeployReport](sqlpackage-deploy-drift-report.md): Creates an XML report of the changes that would be made by a publish action.  
  
- [DriftReport](sqlpackage-deploy-drift-report.md): Creates an XML report of the changes that have been made to a registered database since it was last registered.  
  
- [Script](sqlpackage-script.md): Creates a Transact-SQL incremental update script that updates the schema of a target to match the schema of a source.  
  
The **SqlPackage.exe** command line allows you to specify these actions along with action-specific parameters and properties.  

**[Download the latest version](sqlpackage-download.md)**. For details about the latest release, see the [release notes](release-notes-sqlpackage.md).
  
## Command-Line Syntax

**SqlPackage.exe** initiates the actions specified using the parameters, properties, and SQLCMD variables specified on the command line.  
  
```
SqlPackage {parameters}{properties}{SQLCMD Variables}  
```

### Usage examples

**Generate a comparison between databases by using .dacpac files with a SQL script output**

Start by creating a .dacpac file of your latest database changes:

```
sqlpackage.exe /TargetFile:"C:\sqlpackageoutput\output_current_version.dacpac" /Action:Extract /SourceServerName:"." /SourceDatabaseName:"Contoso.Database"
 ```
 
Create a .dacpac file of your database target (that has no changes):

 ```
 sqlpackage.exe /TargetFile:"C:\sqlpackageoutput\output_target.dacpac" /Action:Extract /SourceServerName:"." /SourceDatabaseName:"Contoso.Database"
 ```

Create a SQL script that generates the differences of two .dacpac files:

```
sqlpackage.exe /Action:Script /SourceFile:"C:\sqlpackageoutput\output_current_version.dacpac" /TargetFile:"C:\sqlpackageoutput\output_target.dacpac" /TargetDatabaseName:"Contoso.Database" /OutputPath:"C:\sqlpackageoutput\output.sql"
 ```


## Version

Displays the sqlpackage version as a build number.  Can be used in interactive prompts as well as in [automated pipelines](sqlpackage-pipelines.md).

```
sqlpackage.exe /Version
 ```


## Exit codes

Commands that return the following exit codes:

- 0 = success
- non-zero = failure


## Next steps

- Learn more about [SqlPackage Extract](sqlpackage-extract.md)
- Learn more about [SqlPackage Publish](sqlpackage-publish.md)
- Learn more about [SqlPackage Export](sqlpackage-export.md)
- Learn more about [SqlPackage Import](sqlpackage-import.md)
