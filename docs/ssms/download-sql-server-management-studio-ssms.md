---
title: Download SQL Server Management Studio (SSMS)
description: Download the latest version of SQL Server Management Studio (SSMS) for managing and configuring instances of SQL Server and Azure SQL.
author: erinstellato-ms
ms.author: erinstellato
ms.reviewer: maghan, randolphwest
ms.date: 07/09/2024
ms.service: sql
ms.subservice: ssms
ms.topic: overview
keywords:
  - "install ssms, download ssms, latest ssms"
  - "SQL Server Management Studio"
  - "ssms.exe"
  - "sql man studio"
  - "sql management studio"
  - "sql management studio install"
  - "download sql management studio"
  - "ms sql management studio"
  - "install sql management studio"
  - "ssms download"
  - "sql server ssms"
  - "ssms express"
---

# Download SQL Server Management Studio (SSMS)

[!INCLUDE [sql-asdb-asdbmi-asa-fabricse-fabricdw](../includes/applies-to-version/sql-asdb-asdbmi-asa-fabricse-fabricdw.md)]

SQL Server Management Studio (SSMS) is an integrated environment for managing any SQL infrastructure, from SQL Server to Azure SQL Database. SSMS provides tools to configure, monitor, and administer instances of SQL Server and databases. Use SSMS to deploy, monitor, and upgrade the data-tier components used by your applications and build queries and scripts.

Use SSMS to query, design, and manage your databases and data warehouses, wherever they are - on your local computer or in the cloud.

For customers needing a cross-platform companion to SSMS for managing SQL and other Azure databases, use [Azure Data Studio](/azure-data-studio/download-azure-data-studio).

For details and more information about what's new in this release, see [Release notes for SQL Server Management Studio (SSMS) 20.2](release-notes-ssms.md).

## Download SSMS

:::image type="icon" source="../includes/media/download.svg" border="false"::: **[Download SQL Server Management Studio (SSMS) 20.2](https://aka.ms/ssmsfullsetup)**

SSMS 20.2 is the latest generally available (GA) version. If you have a *preview* version of SSMS 20 installed, uninstall it before installing SSMS 20.2. Installing SSMS 20.2 doesn't upgrade or replace SSMS 19.x and earlier versions.

- Release number: 20.2
- Build number: 20.2.30.0
- Release date: July 9, 2024

By using SQL Server Management Studio, you agree to its [license terms](/Legal/sql/sql-server-management-studio-license-terms) and [privacy statement](https://privacy.microsoft.com/privacystatement). If you have comments or suggestions or want to report issues, the best way to contact the SSMS team is at [SQL user feedback](https://aka.ms/ssms-feedback).

The SSMS 20.x installation doesn't upgrade or replace SSMS 19.x and earlier versions. SSMS 20.x installs alongside previous versions, so both versions are available. However, if you have an earlier *preview* version of SSMS 20 installed, you must uninstall it before installing the latest release of SSMS 20. You can see if you have a preview version by going to the **Help > About** window.

If a computer contains side-by-side installations of SSMS, verify you start the correct version for your specific needs. The latest version is labeled **Microsoft SQL Server Management Studio v20.2**.

[!INCLUDE [ssms-ads-install](../includes/ssms-azure-data-studio-install.md)]

## Available languages

This release of SSMS can be installed in the following languages:

SQL Server Management Studio 20.2:

- [Chinese (Simplified)](https://aka.ms/ssmsfullsetup?clcid=0x804)
- [Chinese (Traditional)](https://aka.ms/ssmsfullsetup?clcid=0x404)
- [English (United States)](https://aka.ms/ssmsfullsetup?clcid=0x409)
- [French](https://aka.ms/ssmsfullsetup?clcid=0x40c)
- [German](https://aka.ms/ssmsfullsetup?clcid=0x407)
- [Italian](https://aka.ms/ssmsfullsetup?clcid=0x410)
- [Japanese](https://aka.ms/ssmsfullsetup?clcid=0x411)
- [Korean](https://aka.ms/ssmsfullsetup?clcid=0x412)
- [Portuguese (Brazil)](https://aka.ms/ssmsfullsetup?clcid=0x416)
- [Russian](https://aka.ms/ssmsfullsetup?clcid=0x419)
- [Spanish](https://aka.ms/ssmsfullsetup?clcid=0x40a)

If you access this page from a non-English language version and want to see the most up-to-date content, select **Read in English** at the top of this page. To download different languages, select [available languages](#available-languages).

> [!NOTE]  
> The SQL Server PowerShell module is a separate install through the PowerShell Gallery. For more information, see [Install the SQL Server PowerShell module](/powershell/sql-server/download-sql-server-ps-module).

## What's new

For details and more information about what's new in this release, see [Release notes for SQL Server Management Studio (SSMS)](release-notes-ssms.md).

## Previous versions

This article is for the latest version of SSMS only. To download previous versions of SSMS, visit [Previous SSMS releases](../ssms/release-notes-ssms.md#previous-ssms-releases).

[!INCLUDE [ssms-connect-azure-ad](../includes/ssms-connect-azure-ad.md)]

## Unattended install

You can install SSMS using PowerShell.

Follow these steps to install SSMS in the background with no GUI prompts.

1. Launch PowerShell with elevated permissions.

1. Type the following command.

   ```powershell
   $media_path = "<path where SSMS-Setup-ENU.exe file is located>"
   $install_path = "<root location where all SSMS files will be installed>"
   $params = " /Install /Quiet SSMSInstallRoot=$install_path"

   Start-Process -FilePath $media_path -ArgumentList $params -Wait
   ```

   For example:

   ```powershell
   $media_path = "C:\Installers\SSMS-Setup-ENU.exe"
   $install_path = "$env:SystemDrive\SSMSto"
   $params = "/Install /Quiet SSMSInstallRoot=`"$install_path`""

   Start-Process -FilePath $media_path -ArgumentList $params -Wait
   ```

   You can also pass `/Passive` instead of `/Quiet` to see the setup UI.

1. If all goes well, you can see SSMS installed at *%systemdrive%\SSMSto\Common7\IDE\Ssms.exe* based on the example. If something went wrong, you could inspect the error code returned and review the log file in `%TEMP%\SSMSSetup`.

## Uninstall

SSMS might install shared components if it determines they're missing during SSMS installation. SSMS doesn't automatically uninstall these components when you uninstall SSMS.

The shared components are:

- Microsoft OLE DB Driver 18 for SQL Server
- Microsoft ODBC Driver 17 for SQL Server
- Microsoft Visual C++ 2017 Redistributable (x86)
- Microsoft Visual C++ 2017 Redistributable (x64)
- Microsoft Visual Studio Tools for Applications 2019

These components aren't uninstalled because they can be shared with other products. If uninstalled, you might run the risk of disabling other products.

## Installation with Azure Data Studio

- SSMS installs Azure Data Studio by default for versions 18.7 through 19.3.
  - The installation of Azure Data Studio by SSMS is skipped if an equal or higher version of Azure Data Studio is already installed.
  - The Azure Data Studio version can be found in the [release notes](release-notes-ssms.md).
- The Azure Data Studio system installer requires the same security rights as the SSMS installer.
- The Azure Data Studio installation is completed with the default Azure Data Studio installation options. These are to create a Start Menu folder and add Azure Data Studio to PATH. A desktop shortcut isn't created, and Azure Data Studio isn't registered as a default editor for any file type.
- Localization of Azure Data Studio is accomplished through Language Pack extensions. Install the corresponding language pack from the [extension marketplace](/azure-data-studio/extensions/add-extensions) to localize Azure Data Studio.
- For versions 18.7 through 19.3, the installation of Azure Data Studio can be skipped by launching the SSMS installer with the command line flag `DoNotInstallAzureDataStudio=1`.

## Supported SQL offerings

- This version of SSMS works with [!INCLUDE [sssql14-md](../includes/sssql14-md.md)] and later versions. It provides the most significant support for working with the latest cloud features in Azure SQL Database, Azure Synapse Analytics, and Microsoft Fabric.

- Additionally, SSMS 20.x can be installed alongside with SSMS 19.x, SSMS 18.x, SSMS 17.x, and SSMS 16.x.

- For SQL Server Integration Services (SSIS), SSMS 17.x and later versions don't support connecting to the legacy SQL Server Integration Services service. To connect to an earlier version of the legacy Integration Services, use the version of SSMS aligned with the version of SQL Server. For example, use SSMS 16.x to connect to the legacy [!INCLUDE [sssql16-md](../includes/sssql16-md.md)] Integration Services service. SSMS 17.x and SSMS 16.x can be installed on the same computer. Since the release of [!INCLUDE [sssql11-md](../includes/sssql11-md.md)], the SSIS Catalog database, SSISDB, is the recommended way to store, manage, run, and monitor Integration Services packages. See [SSIS Catalog](../integration-services/catalog/ssis-catalog.md) for details.

## SSMS system requirements

The current release of SSMS supports the following 64-bit platforms when used with the latest available service pack:

Supported operating systems:

- Windows 11 (x64)
- Windows 10 (x64) version 1607 (10.0.14393) and later versions
- Windows Server 2022 (x64)
- Windows Server Core 2022 (x64)
- Windows Server 2019 (x64)
- Windows Server Core 2019 (x64)
- Windows Server 2016 (x64) <sup>1</sup>

<sup>1</sup> SSMS requires .NET Framework 4.7.2.

> [!NOTE]  
> To install SSMS on Windows Server Core, you must install the [Server Core App Compatibility Feature on Demand](/windows-server/get-started/server-core-app-compatibility-feature-on-demand).

Supported hardware:

- 1.8 GHz or faster x86 (Intel, AMD) processor. Dual-core or better recommended
- 2 GB of RAM; 4 GB of RAM recommended (2.5 GB minimum if running on a virtual machine)
- Hard disk space: Minimum of 3 GB up to 10 GB of available space

> [!NOTE]  
> SSMS is available only as a 32-bit application for Windows. If you need a tool that runs on operating systems other than Windows, we recommend Azure Data Studio. Visit [Azure Data Studio](/azure-data-studio/what-is-azure-data-studio), for more details.

[!INCLUDE [get-help-sql-tools](../includes/paragraph-content/get-help-sql-tools.md)]

## Related content

- [SQL tools overview](../tools/overview-sql-tools.md)
- [What is SQL Server Management Studio (SSMS)?](sql-server-management-studio-ssms.md)
- [What is Azure Data Studio?](/azure-data-studio/what-is-azure-data-studio)
- [Download SQL Server Data Tools (SSDT) for Visual Studio](../ssdt/download-sql-server-data-tools-ssdt.md)
- [Latest updates and version history for SQL Server](/troubleshoot/sql/releases/download-and-install-latest-updates?bc=%2fsql%2fbreadcrumb%2ftoc.json&toc=%2fsql%2ftoc.json)
- [Azure Data Architecture Guide](/azure/architecture/data-guide/)
- [SQL Server Blog](https://cloudblogs.microsoft.com/sqlserver/)

[!INCLUDE [contribute-to-content](../includes/paragraph-content/contribute-to-content.md)]
