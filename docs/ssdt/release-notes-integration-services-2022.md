---
title: SSIS Projects extension for VS2022 troubleshooting guide
description: "View the release notes for all versions of SSIS that work with Visual Studio 2022 and earlier Visual Studio versions."
ms.service: sql
ms.subservice: ssdt
ms.topic: conceptual
author: chugugrace
ms.author: chugu
ms.reviewer: maghan, drskwier
ms.custom: seo-lt-2022
ms.date: 11/21/2022
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=azuresqldb-mi-current"
---
# SSIS Projects extension for VS2022 troubleshooting guide

[!INCLUDE [sql-asdb-asa](../includes/applies-to-version/sql-asdb-asa.md)]

> [!IMPORTANT]
> You can download the [SSIS](https://marketplace.visualstudio.com/items?itemName=SSIS.MicrosoftDataToolsIntegrationServices) from the [Visual Studio Marketplace](<https://marketplace.visualstudio.com/>).

Visit https://techcommunity.microsoft.com/t5/SQL-Server-Integration-Services/bg-p/SSIS for the latest information, tips, news, and announcements about SSIS directly from the product team.

## Component Download
- To design packages using Oracle and Teradata connectors and targeting an earlier version of SQL Server prior to SQL 2019, in addition to the [Microsoft Oracle Connector](https://aka.ms/SSISMSOracleConnector) and [Microsoft Teradata Connector](https://www.microsoft.com/download/details.aspx?id=100599), you need to also install the corresponding version of Microsoft Connector for Oracle and Teradata by Attunity.
  - [Microsoft Connector Version 5.0 for Oracle and Teradata by Attunity targeting SQL Server 2017](https://www.microsoft.com/download/details.aspx?id=55179)
  - [Microsoft Connector Version 4.0 for Oracle and Teradata by Attunity targeting SQL Server 2016](https://www.microsoft.com/download/details.aspx?id=52950)
  - [Microsoft Connector Version 3.0 for Oracle and Teradata by Attunity targeting SQL Server 2014](https://www.microsoft.com/download/details.aspx?id=44582)
  - [Microsoft Connector Version 2.0 for Oracle and Teradata by Attunity targeting SQL Server 2012](https://www.microsoft.com/download/details.aspx?id=29283)

- Since version 3.3, Power Query Source for SQL Server 2017-2022 have been excluded from the installation of this product. To continue using this component, manually download and install them by yourselves. Here are the download links: [Power Query Source for SQL Server 2017-2022](https://www.microsoft.com/download/details.aspx?id=100619)

## Common Issues
- SSIS Execute Package Task doesn't support debugging when ExecuteOutOfProcess is set to True.

- Third party components aren't supported yet.

- Sometimes this product or Visual Studio Tools for Applications 2022 may be somehow deleted during VS instance upgrade. If your existing SSIS projects cannot be loaded, try to repair this product via control panel. If VS doesn't pop up when clicking on "Edit Script", try to repair VSTA 2022 via control panel. 

- **SSDT side by side issue**. SQL Server Analysis Services and SQL Server Reporting Services extensions can now work side-by-side with this extension in VS2022 17.4 and previous version. The workaround is to download Visual Studio 2022 17.5 Preview 2 or later.

- [!INCLUDE[snac-removed-oledb-and-odbc](../includes/snac-removed-oledb-and-odbc.md)]

## Known issues
**Version 0.3**
  1. Target sever versions supported: SQL server 2017, SQL server 2019 and SQL server 2022
  2. Can't design Dimension Processing and Partition Processing.
  3. Can't design DQS related component.
  4. Project name in Solution Explorer UI doesn’t show target server version as suffix.
  5. Localization and globalization aren't supported.
  6. Oracle Connection Manager execute failed with error code 0x80004005 when targeting SQL Server 2017.
  
**Version 0.2**
  1. Target sever versions supported: SQL server 2019 and SQL server 2022
  2. Can't design Dimension Processing and Partition Processing.
  3. Can't design DQS related component.
  4. Project name in Solution Explorer UI doesn’t show target server version as suffix.
  5. Side by side, localization and globalization aren't supported.
  6. Azure-enabled SSIS projects aren't supported
  7. Repair action doesn't take effct. Reinstall it instead.
  8. Logging container can't choose packages.
  
**Version 0.1**
  1. Target server versions supported: SQL server 2019 and SQL server 2022 
  1. Can't design Dimension Processing and Partition Processing.
  1. Can't design DQS related component.
  1. Project name in Solution Explorer UI doesn't show target server version as suffix.
  1. Side by side, localization and globalization aren't supported.
  1. Packages using Oracle and Teradata connectors aren't supported. 
  1. Azure-enabled SSIS projects aren't supported.
  1. Logging container can't choose packages.
  1. Right click Package and select SSIS Import and Export Wizard throws error, workaround: execute Common7\IDE\CommonExtensions\Microsoft\SSIS\160\Binn\DTSWizard.exe
  1. Right click Package and select Upgrade All Packages throws error, workaround: execute Common7\IDE\CommonExtensions\Microsoft\SSIS\160\Binn\SSISUpgrade.exe
## Installation issues

If you install successfully, but the solution shows **"incompatible"**, and "The application isn't installed":
1. Open Visual Studio -> Extension -> Manage Extensions -> Installed
1. Enable SSIS extension
1. Relaunch Visual Studio

If you get an error during installation, and find **"Bundle action failed: Invalid pointer (0x80004003)"** in the log. You can check the logs under %temp%\SsdtisSetup, the  more detail log is under Microsoft.DataTools.IntegrationServices_{timestamp}_ISVsix.log:
- When the error is "The file {filefullpath} already exists.":
   1. ```
      cd C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\IDE
      rm CommonExtensions\Microsoft\SSIS\* 
      rm PublicAssemblies\SSIS\* 
      rm "PublicAssemblies\Microsoft BI\Business Intelligence Projects\Integration Services\"* 
      ```
   1. Repair the vs2022
   1. Restart and reinstall
- When the error is "Object reference not set to an instance of an object.",
  - delete the broken instance folder "%ProgramData%\Microsoft\VisualStudio\Packages\_Instances\<InstallationID>"
- When the error is "Error 0x80091007: Failed to verify hash of payload",
  - delete C:\ProgramData\Package Cache\15160B731819F56D87A626F9A2777550340022D7 and retry.
- When the error isn't in the above list, you can zip %temp%\SsdtisSetup and send the logs to ssistoolsfeedbacks@microsoft.com for troubleshooting.

## Offline installation

Follow the below steps to install this product in an offline environment:
1. Refer to the instructions in [Create an offline installation package of Visual Studio for local installation](/visualstudio/install/create-an-offline-installation-of-visual-studio?view=vs-2022&preserve-view=true), and make sure the following prerequisites are included:
    - Prerequisite ID="Microsoft.VisualStudio.Component.Roslyn.LanguageServices" Version="[17.0,)" DisplayName="C# and Visual Basic"
    - Prerequisite ID="Microsoft.VisualStudio.Component.CoreEditor" Version="[17.0,)" DisplayName="Visual Studio core editor"
    - Prerequisite ID="Microsoft.Net.Component.4.7.TargetingPack" Version="[17.0,)" DisplayName=".NET Framework 4.7 targeting pack"

1. Launch the installer of this product and perform the installation, or you can run the installer in quiet mode. Launch the installer with "/?" argument to get more details of the arguments list of the installer.

1. VS Community doesn't support offline activation. To use this product with VS Community, you must log in to your Microsoft account occasionally in VS Community. If you want to use this product in an offline environment, we recommend you to install this product on VS Professional or Enterprise, which support offline activation via a product key.
