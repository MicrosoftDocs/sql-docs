---
title: Download SQL Server Management Studio (SSMS) 19
description: Download the latest version of SQL Server Management Studio (SSMS) 19.
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
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
author: erinstellato-ms
ms.author: erinstellato
ms.reviewer: maghan
ms.custom: 
- event-tier1-build-2022
ms.date: 08/11/2022
---

# Download SQL Server Management Studio (SSMS) 19 (Preview)

[!INCLUDE [SQL Server ASDB, ASDBMI, ASDW ](../includes/applies-to-version/sql-asdb-asdbmi-asa.md)]

SQL Server Management Studio (SSMS) is an integrated environment for managing any SQL infrastructure, from SQL Server to Azure SQL Database. SSMS provides tools to configure, monitor, and administer instances of SQL Server and databases. Use SSMS to deploy, monitor, and upgrade the data-tier components used by your applications, and build queries and scripts.

Use SSMS to query, design, and manage your databases and data warehouses, wherever they are - on your local computer, or in the cloud.

## Download SSMS

To download the latest general availability (GA) version of SSMS, visit [Download SSMS](download-sql-server-management-studio-ssms.md).

:::image type="icon" source="../includes/media/download.svg" border="false"::: **[Download SQL Server Management Studio (SSMS) 19 (Preview)](https://go.microsoft.com/fwlink/?linkid=2203303&clcid=0x409)**

SSMS 19 Preview 3 is the latest preview.

- Release number: 19.0 (Preview 3)
- Build number: 16.0.19061.0
- Release date: August 11, 2022

By using SQL Server Management Studio, you agree to its [license terms](/Legal/sql/sql-server-management-studio-license-terms) and [privacy statement](https://privacy.microsoft.com/privacystatement). If you have comments or suggestions, or you want to report issues, the best way to contact the SSMS team is at [SQL Server user feedback](https://aka.ms/sqlfeedback).

The SSMS 19.x installation doesn't upgrade or replace SSMS versions 18.x or earlier. SSMS 19.x installs side by side with previous versions, so both versions are available for use. However, if you have an earlier *preview* version of SSMS 19 installed, you must uninstall it before installing the latest SSMS 19 preview. You can see if you have a preview version by going to the **Help > About** window.

If a computer contains side-by-side installations of SSMS, verify you start the correct version for your specific needs. The latest version is labeled **Microsoft SQL Server Management Studio v19.0 Preview 3**.

[!INCLUDE [ssms-ads-install](../includes/ssms-azure-data-studio-install.md)]

## Available languages

This release of SSMS can be installed in the following languages:

SQL Server Management Studio 19 (Preview):  
[Chinese (Simplified)](https://go.microsoft.com/fwlink/?linkid=2203303&clcid=0x804) | [Chinese (Traditional)](https://go.microsoft.com/fwlink/?linkid=2203303&clcid=0x404) | [English (United States)](https://go.microsoft.com/fwlink/?linkid=2203303&clcid=0x409) | [French](https://go.microsoft.com/fwlink/?linkid=2203303&clcid=0x40c) | [German](https://go.microsoft.com/fwlink/?linkid=2203303&clcid=0x407) | [Italian](https://go.microsoft.com/fwlink/?linkid=2203303&clcid=0x410) | [Japanese](https://go.microsoft.com/fwlink/?linkid=2203303&clcid=0x411) | [Korean](https://go.microsoft.com/fwlink/?linkid=2203303&clcid=0x412) | [Portuguese (Brazil)](https://go.microsoft.com/fwlink/?linkid=2203303&clcid=0x416) | [Russian](https://go.microsoft.com/fwlink/?linkid=2203303&clcid=0x419) | [Spanish](https://go.microsoft.com/fwlink/?linkid=2203303&clcid=0x40a)

> [!TIP]
> If you are accessing this page from a non-English language version, and want to see the most up-to-date content, please select **Read in English** at the top of this page. You can download different languages from the US-English version site by selecting [available languages](#available-languages).

> [!NOTE]
> The SQL Server PowerShell module is a separate install through the PowerShell Gallery. For more information, see [Download SQL Server PowerShell Module](../powershell/download-sql-server-ps-module.md).

## Previous versions

This article is for the Preview version of SSMS 19 only. To download previous versions of SSMS, visit [Previous SSMS releases](../ssms/release-notes-ssms.md#previous-ssms-releases).

[!INCLUDE[ssms-connect-azure-ad](../includes/ssms-connect-azure-ad.md)]

## Unattended install

You can also install SSMS using a command prompt script.

If you want to install SSMS in the background with no GUI prompts, then follow the steps below.

1. Launch the command prompt with elevated permissions.

2. Type the command below in the command prompt.

    ```console
    start "" /w <path where SSMS-Setup-ENU.exe file is located> /Quiet SSMSInstallRoot=<path where you want to install SSMS>
    ```

    Example:

    ```console
    start "" /w %systemdrive%\SSMSfrom\SSMS-Setup-ENU.exe /Quiet SSMSInstallRoot=%systemdrive%\SSMSto
    ```

    You can also pass */Passive* instead of */Quiet* to see the setup UI.

3. If all goes well, you can see SSMS installed at %systemdrive%\SSMSto\Common7\IDE\Ssms.exe" based on the example. If something went wrong, you could inspect the error code returned and take a peek at the %TEMP%\SSMSSetup for the log file.

## Installation with Azure Data Studio

- Starting with SSMS 18.7, SSMS installs a system version of Azure Data Studio by default. If an equal or greater version of Azure Data Studio (GA or insiders build) is already present on the workstation compared to the included version of Azure Data Studio, the installation of Azure Data Studio by SSMS is skipped. The Azure Data Studio version can be found in the release notes
- The Azure Data Studio system installer requires the same security rights as the SSMS installer.
- The Azure Data Studio installation is completed with the default Azure Data Studio installation options. These are to create a Start Menu folder and add Azure Data Studio to PATH. A desktop shortcut isn't created and Azure Data Studio isn't registered as a default editor for any file types.
- Localization of Azure Data Studio is accomplished through Language Pack extensions. To localize Azure Data Studio, download the corresponding language pack from the [extension marketplace](../azure-data-studio/extensions/add-extensions.md).
- At this time, the installation of Azure Data Studio can be skipped by launching the SSMS installer with the command line flag `DoNotInstallAzureDataStudio=1`.

## Uninstall

SSMS may install shared components if it's determined that they're missing during SSMS installation. SSMS won't automatically uninstall these components when you uninstall SSMS.

The shared components are:

- Azure Data Studio
- Microsoft .NET Framework 4.7.2
- Microsoft OLE DB Driver for SQL Server
- Microsoft ODBC Driver 17 for SQL Server
- Microsoft Visual C++ 2013 Redistributable (x86)
- Microsoft Visual C++ 2017 Redistributable (x86)
- Microsoft Visual C++ 2017 Redistributable (x64)
- Microsoft Visual Studio Tools for Applications 2017

These components aren't uninstalled because they can be shared with other products. If uninstalled, you may run the risk of disabling other products.

## Supported SQL offerings

- This version of SSMS works with all [supported versions of SQL Server 2008 - SQL Server 2022](../sql-server/end-of-support/sql-server-end-of-support-overview.md) and provides the greatest level of support for working with the latest cloud features in Azure SQL Database and Azure Synapse Analytics.
- Additionally, SSMS 19 (Preview) can be installed side by side with SSMS 18.x, 17.x, SSMS 16.x, or SQL Server 2014 SSMS and earlier.
- SQL Server Integration Services (SSIS) - SSMS version 17.x or later doesn't support connecting to the legacy SQL Server Integration Services service. To connect to an earlier version of the legacy Integration Services, use the version of SSMS aligned with the version of SQL Server. For example, use SSMS 16.x to connect to the legacy SQL Server 2016 Integration Services service. SSMS 17.x and SSMS 16.x can be installed side by side on the same computer. Since the release of SQL Server 2012, the SSIS Catalog database, SSISDB, is the recommended way to store, manage, run, and monitor Integration Services packages. For details, see [SSIS Catalog](../integration-services/catalog/ssis-catalog.md).

## SSMS System Requirements

The current release of SSMS supports the following 64-bit platforms when used with the latest available service pack:

Supported Operating Systems:

- Windows 11 (64-bit)
- Windows 10 (64-bit) version 1607 (10.0.14393) or later
- Windows Server 2022 (64-bit)
- Windows Server 2019 (64-bit)
- Windows Server 2016 (64-bit)

Supported hardware:

- 1.8 GHz or faster x86 (Intel, AMD) processor. Dual-core or better recommended
- 2 GB of RAM; 4 GB of RAM recommended (2.5 GB minimum if running on a virtual machine)
- Hard disk space: Minimum of 2 GB up to 10 GB of available space

> [!NOTE]
> SSMS is available only as a 32-bit application for Windows. If you need a tool that runs on operating systems other than Windows, we recommend Azure Data Studio. Azure Data Studio is a cross-platform tool that runs on macOS, Linux, as well as Windows. For details, see [Azure Data Studio](../azure-data-studio/what-is-azure-data-studio.md).

[!INCLUDE[get-help-sql-tools](../includes/paragraph-content/get-help-sql-tools.md)]

## Next steps

- [SQL tools](../tools/overview-sql-tools.md)
- [SQL Server Management Studio documentation](sql-server-management-studio-ssms.md)
- [Azure Data Studio](../azure-data-studio/what-is-azure-data-studio.md)
- [Download SQL Server Data Tools (SSDT)](../ssdt/download-sql-server-data-tools-ssdt.md)
- [Latest updates](../database-engine/install-windows/latest-updates-for-microsoft-sql-server.md)
- [Azure Data Architecture Guide](/azure/architecture/data-guide/)
- [SQL Server Blog](https://cloudblogs.microsoft.com/sqlserver/)

[!INCLUDE[contribute-to-content](../includes/paragraph-content/contribute-to-content.md)]
