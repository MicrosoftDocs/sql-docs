---
title: Build a Project from the Command Line
description: Build a SQL Server Database Projects from the command line
ms.custom: "seodec18"
ms.date: "06/25/2020"
ms.reviewer: "drskwier, maghan, sstein"
ms.prod: sql
ms.technology: azure-data-studio
ms.topic: conceptual
author: "dzsquared"
ms.author: "drskwier"
---
# Build and Publish a project

The build process in the SQL Database Projects extension for Azure Data Studio allows for *dacpac* creation in Windows, macOS, and Linux environments. The project can be deployed to a local or cloud environment with the publish process.

## Prerequisites
- Install and configure [SQL Database Projects extension for Azure Data Studio](sql-database-project-extension.md).


 ## Build a Database Project from Command Line
 The following .NET Core dlls and the target file `Microsoft.Data.Tools.Schema.SqlTasts.targets` are required to build a SQL database project from the command line from all platforms supported by the Azure Data Studio extension for SQL Database Projects.

## Next steps

- [SQL Database Projects extension for Azure Data Studio](sql-database-project-extension.md)
- [Publish SQL database projects](sql-database-project-extension-build.md#publish-a-database-project)
