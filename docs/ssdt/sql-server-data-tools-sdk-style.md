---
title: SQL Server Data Tools, SDK-style (preview)
description: SDK-style SQL projects in Visual Studio enable the next generation of SQL projects.
author: dzsquared
ms.author: drskwier
ms.date: 09/10/2024
ms.service: sql
ms.subservice: ssdt
ms.topic: conceptual
---

# SQL Server Data Tools, SDK-style (preview)

**SQL Server Data Tools (SSDT)** is a set of development tools in Visual Studio with focused on building SQL Server databases and Azure SQL databases.  SDK-style SQL projects in Visual Studio enable the next generation of SQL projects as part of the **SQL Server Data Tools, SDK-style (preview)** feature available for Visual Studio 2022. The **SQL projects** capability extends to CI/CD pipelines, enabling you to automate the build and deployment of your database projects with the [SqlPackage CLI](../tools/sqlpackage/sqlpackage.md).

## Overview

The original SQL project format is based on MSBuild (.NET Framework) and is the format used by SQL Server Data Tools in Visual Studio. The SDK-style project format is based on the new SDK-style projects (Microsoft.Build.Sql) and is the format used by the SQL Database Projects extension for Azure Data Studio and VS Code. The Microsoft.Build.Sql project SDK is more flexible than the original SQL projects and contains new features:

- .NET 8 support (cross platform)
- NuGet [package references](../tools/sql-database-projects/concepts/package-references.md) for database references
- Default globbing pattern for .sql files in the project

New development work should consider using the SDK-style project format, as it's the format that will be supported in the future. SDK-style projects have a superset of functionality from original SQL projects and existing SQL projects can be converted to SDK-style projects through [modification of the project file](../tools/sql-database-projects/howto/convert-original-sql-project.md). The exception to the functionality coverage is support for SQLCLR objects, which require .NET Framework and aren't supported in SDK-style projects.

Further documentation on SQL projects is available in the [SQL database projects](../tools/sql-database-projects/sql-database-projects.md) topic and the Microsoft.Build.Sql SDK is available on [GitHub](https://github.com/microsoft/DacFx) and on [NuGet.org](https://www.nuget.org/packages/Microsoft.build.sql).

## Install

> [!WARNING]
> The SDK-style SQL projects feature is in preview and side-by-side install with the original SQL projects isn't supported. Installing the SDK-style SQL projects in a standalone Visual Studio instance is advised.

To install the SDK-style SQL projects in Visual Studio 2022, follow these steps:

1. Download and install Visual Studio 2022 preview (17.12 preview 2 or later) from the [Visual Studio download page](https://visualstudio.microsoft.com/downloads/).
1. During install, select the **Individual components** tab and search for "SQL" to locate and select "SQL Server Data Tools SDK-style (Preview)". Selecting this item automatically selects required dependencies.
1. Continue the install without selecting workloads or extra features.

:::image type="content" source="media/sql-server-data-tools-sdk-style/installer-individual-component.png" alt-text="Screenshot of the Visual Studio installer with the individual components tab open." lightbox="media/sql-server-data-tools-sdk-style/installer-individual-component.png":::

The Visual Studio [documentation](/visualstudio/install/modify-visual-studio#change-workloads-or-individual-components) provides additional information on modifying Visual Studio installations to select individual components.

Unexpected behavior may occur if the SDK-style SQL projects are installed side-by-side with the original SQL projects and installing SDK-style SQL Server Data Tools is advised as a standalone component. To verify what components are part of a Visual Studio install, use the **Help** menu in Visual Studio and select **About Microsoft Visual Studio**. Several approaches are available to utilize multiple Visual Studio instances on a single machine:

- Install different release channels of Visual Studio, such as Visual Studio 2022 and Visual Studio 2022 preview.
- Install [different editions](/visualstudio/install/install-visual-studio-versions-side-by-side#install-different-editions-of-the-same-major-visual-studio-version-side-by-side) of Visual Studio, such as Visual Studio Community and Visual Studio Enterprise.
- Install to a [specific directory](/visualstudio/install/change-installation-locations) to separate a Visual Studio instance from a previously installed version.

## Limitations

A full comparison of functionality between the SQL projects tools is available in [SQL projects tools](../tools/sql-database-projects/sql-projects-tools.md). The SDK-style SQL projects feature in Visual Studio has the following limitations:

- Side-by-side install with original SQL projects isn't supported
- The SQL project file uses the extension `.sqlprojx` instead of `.sqlproj` in Visual Studio 17.12 preview 2
- SQLCLR objects aren't supported
- Schema compare interface isn't enabled
- Table designer interface isn't enabled
- Data comparison interface isn't enabled
- Database unit testing isn't enabled

Support for SDK-style SQL projects in Visual Studio is in preview and installing from the latest Visual Studio 2022 preview release is recommended. The SQL projects feature in Visual Studio is under active development and feedback is welcome through the [Developer Community](https://developercommunity.visualstudio.com/).

## Convert existing projects

Microsoft.Build.Sql SDK-style SQL projects can be created from scratch or converted from existing SQL projects. The conversion of an original SQL project to a Microsoft.Build.Sql project can be done in place but creating a backup of the project before converting is recommended. To convert an existing SQL project to an SDK-style project, follow the steps in the [Convert original SQL project](../tools/sql-database-projects/howto/convert-original-sql-project.md) how-to guide.

## Related content

- [SQL database projects](../tools/sql-database-projects/sql-database-projects.md)
- [Visual Studio 2022 preview release notes](/visualstudio/releases/2022/release-notes-preview)
- [SQL projects in VS Code](https://marketplace.visualstudio.com/items?itemName=ms-mssql.sql-database-projects-vscode)
