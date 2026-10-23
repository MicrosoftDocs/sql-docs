---
title: Build a Project from the Command Line
description: Build a SQL Server Database Project from the command line
author: cwebb-msft
ms.author: chweb
ms.reviewer: drskwier
ms.date: 01/28/2026
ms.service: sql
ms.subservice: sql-database-projects
ms.topic: how-to
ms.collection:
  - data-tools
---

# Build a database project from the command line

While the SQL Database Project extension in **Visual Studio Code** provides a graphical user interface to build [SQL database projects](../../sql-database-projects/sql-database-projects.md), a command line build experience is also available for Windows, macOS, and Linux environments. This article outlines the prerequisites and syntax needed to build a `.dacpac` from a Microsoft.Build.Sql SDK-style SQL project at the command line.

## Microsoft.Build.Sql with .NET

Using [Microsoft.Build.Sql](https://www.nuget.org/packages/Microsoft.Build.Sql) with [SDK-style SQL projects](../../sql-database-projects/sql-database-projects.md#original-projects-vs-sdk-style-projects) is the preferred method for working with SQL projects from the command line.

To build an SDK-style SQL project from the command line on Windows, macOS, or Linux, use the following command:

```bash
dotnet build
```

Optionally, specify the project name. If you specify the project name, you can build a specific project within a more complex folder structure.

```bash
dotnet build AdventureWorks/AdventureWorks.sqlproj
```

## Microsoft.Build.Sql with .NET Framework

Starting with Microsoft.Build.Sql 2.0.0-preview.3, you can build SDK-style SQL projects from the command line using .NET Framework and msbuild. For SQLCLR objects, .NET Framework is required to build the SQL project.

```bash
msbuild AdventureWorks/AdventureWorks.sqlproj
```

## Related content

- [Publish SQL database projects from Visual Studio Code](publish-database-project.md)
- [SQL Database Projects extension](sql-database-projects-extension.md)
- [Tutorial: Create and deploy a SQL project](../../sql-database-projects/tutorials/create-deploy-sql-project.md)
- [Get help with the SQL Database Projects extension](https://github.com/microsoft/azuredatastudio/issues)
- [Get help with SQL projects and DacFx](https://github.com/microsoft/dacfx)
