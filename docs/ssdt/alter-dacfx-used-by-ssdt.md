---
title: Alter DacFx used by SQL Server Data Tools (SSDT)
description: "Learn how to manually change the DacFx version used by SQL Server Data Tools (SSDT)."
author: dzsquared
ms.author: drskwier
ms.reviewer: maghan
ms.date: 03/15/2024
ms.service: sql
ms.subservice: ssdt
ms.topic: conceptual
keywords: ssdt dacfx
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=azuresqldb-mi-current"
---

# Alter DacFx used by SQL Server Data Tools (SSDT)

Under specific circumstances you might need to use a different version of [DacFx](../tools/sqlpackage/sqlpackage.md) with the SQL Server Data Tools interface in Visual Studio. When possible, it is recommended to use a standalone version of DacFx if an alternative version from SQL Server Data Tools is required.

Follow the process below to alter the version of DacFx used by SQL Server Data Tools (SSDT).

## Replace DacFx files in SSDT

SQL Server Data Tools stores the DacFx files under `Common7\IDE\Extensions\Microsoft\SQLDB\DAC` within the Visual Studio program files. For Visual Studio 2022 Community, the full path is commonly `C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\IDE\Extensions\Microsoft\SQLDB\DAC`.

Substitution should be done within the same major version of DacFx. For example, if Visual Studio 17.9 (2022) utilizes DacFx versions 162.2.33.1, only other 162.x versions should be used. To view the current version, select `Microsoft.SqlServer.Dac.dll` file in File Explorer from the SSDT DacFx folder and use the context menu to open the file properties.

DacFx is published to [NuGet](https://www.nuget.org/packages/Microsoft.SqlServer.DACFx). Identify the desired version within the Microsoft.SqlServer.DacFx NuGet feed and follow these steps to use it with SSDT:

1. Download the NuGet package for the DacFx version from the web interface.
1. Change the *nupkg* file to a *zip* file and extract the archive.
1. Close Visual Studio.
1. Copy the following files from `lib/net462` into the SSDT DacFx folder:

   - Microsoft.Data.Tools.Schema.Sql.dll
   - Microsoft.Data.Tools.Utilities.dll
   - Microsoft.SqlServer.Dac.dll
   - Microsoft.SqlServer.Dac.Extensions.dll
   - Microsoft.SqlServer.Dac.Extensions.xml
   - Micrososft.SqlServer.Dac.xml
   - Microsoft.SqlServer.TransactSql.ScriptDom.dll
   - Microsoft.SqlServer.Types.dll
SQL Projects will require [Clean or Rebuild](/visualstudio/ide/building-and-cleaning-projects-and-solutions-in-visual-studio) after this process to properly incorporate the replaced DacFx version.

## Related content

- [SQL Server Data Tools (SSDT) overview](sql-server-data-tools.md)
- [SqlPackage.exe (DacFx) release notes](../tools/sqlpackage/release-notes-sqlpackage.md)
