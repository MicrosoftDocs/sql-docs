---
title: SSIS Projects extension for VS2019 troubleshooting guide
description: "View the release notes for all versions of SQL Server Data Tools (SSDT) that work with Visual Studio 2019 and earlier Visual Studio versions."
ms.service: sql
ms.subservice: ssdt
ms.topic: conceptual
author: chugugrace
ms.author: chugu
ms.reviewer: maghan, drskwier
ms.custom: seo-lt-2019
ms.date: 07/26/2022
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=azuresqldb-mi-current"
---
# SSIS Projects extension for VS2019 troubleshooting guide

[!INCLUDE [sql-asdb-asa](../includes/applies-to-version/sql-asdb-asa.md)]

> [!IMPORTANT]
> You can download the [SSIS](https://marketplace.visualstudio.com/items?itemName=SSIS.SqlServerIntegrationServicesProjects) from the [Visual Studio Marketplace](<https://marketplace.visualstudio.com/>).

Visit https://techcommunity.microsoft.com/t5/SQL-Server-Integration-Services/bg-p/SSIS for the latest information, tips, news, and announcements about SSIS directly from the product team.

## Offline installation
Follow the below steps to install this product in an offline environment:
1. Refer to the instructions in [Create an offline installation package of Visual Studio for local installation](/visualstudio/install/create-an-offline-installation-of-visual-studio?view=vs-2019&preserve-view=true), and make sure the following prerequisites are included:
    - Prerequisite Id="Microsoft.VisualStudio.Component.Roslyn.LanguageServices" Version="[16.0,)" DisplayName="C# and Visual Basic"
    - Prerequisite Id="Microsoft.VisualStudio.Component.CoreEditor" Version="[16.0,)" DisplayName="Visual Studio core editor"
    - Prerequisite Id="Microsoft.VisualStudio.Component.SQL.SSDT" Version="[16.0,)" DisplayName="SQL Server Data Tools"
    - Prerequisite Id="Microsoft.Net.Component.4.TargetingPack" Version="[16.0,)" DisplayName=".NET Framework 4 targeting pack"
    - Prerequisite Id="Microsoft.Net.Component.4.5.TargetingPack" Version="[16.0,)" DisplayName=".NET Framework 4.5 targeting pack"
    - Prerequisite Id="Microsoft.Net.Component.4.7.TargetingPack" Version="[16.0,)" DisplayName=".NET Framework 4.7 targeting pack"

1. Launch the installer of this product and perform the installation, or you can run the installer in quiet mode. Launch the installer with "/?" argument to get more details of the arguments list of the installer.

1. VS Community does not support offline activation. To use this product with VS Community, you must login to your Microsoft account occasionally in VS Community. If you want to use this product in a totally offline environment, we recommend you to install this product on VS Professional or Enterprise, which support offline activation via a product key.

## Common Issues
- SSIS Execute Package Task doesn't support debugging when ExecuteOutOfProcess is set to True.

- SQL Server Integration Services Projects extension doesn't support Visual Studio 2022 yet. 

- In the latest general availability (GA) version, to design packages using Oracle and Teradata connectors and targeting an earlier version of SQL server prior to SQL 2019, in addition to the [Microsoft Oracle Connector for SQL 2019](https://www.microsoft.com/download/details.aspx?id=58228) and [Microsoft Teradata Connector for SQL 2019](https://www.microsoft.com/download/details.aspx?id=100599), you need to also install the corresponding version of Microsoft Connector for Oracle and Teradata by Attunity.
  - [Microsoft Connector Version 5.0 for Oracle and Teradata by Attunity targeting SQL Server 2017](https://www.microsoft.com/download/details.aspx?id=55179)
  - [Microsoft Connector Version 4.0 for Oracle and Teradata by Attunity targeting SQL Server 2016](https://www.microsoft.com/download/details.aspx?id=52950)
  - [Microsoft Connector Version 3.0 for Oracle and Teradata by Attunity targeting SQL Server 2014](https://www.microsoft.com/download/details.aspx?id=44582)
  - [Microsoft Connector Version 2.0 for Oracle and Teradata by Attunity targeting SQL Server 2012](https://www.microsoft.com/download/details.aspx?id=29283)

- Sometimes this product or Visual Studio Tools for Applications 2019 may be somehow deleted during VS instance upgrade. If your existing SSIS projects cannot be loaded, try to repair this product via control panel. If VS doesn't pop up when clicking on "Edit Script", try to repair VSTA 2019 via control panel. We've reported this issue to VS team. Sorry for any inconvenience.

- [!INCLUDE[snac-removed-oledb-and-odbc](../includes/snac-removed-oledb-and-odbc.md)]

## Known issues
**Version 4.3**
- Known issues:
    1. **Cannot design Oracle and Teradata Components.**
 
**Version 4.2**
- Known issues:
    1. **Cannot design Oracle and Teradata Components.**
 
**Version 4.1**
- Known issues:
    1. **Cannot design Oracle and Teradata Components.**
    2. **Due to a limitation of VS marketplace, the version 4.1.2 does not introduce new binaries to download. Version 4.1 contains the latest binaries.**
    
**Version 4.0 preview:**
- Known issues:
    1. **Cannot design Oracle and Teradata Components.**
    2. CDC source component in target [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] can't do preview.
    3. **When executing SSIS project targeting SqlServer 2019 on the environment that SQL Server 2019 are also installed**, the execution will fail with error "Unable to cast COM object of type System._ComObject to interface type Microsoft.SqlServer.Dts.Runtime.Wrapper.Sql2019.IDTSApplication160".
Workaround: Solution Explorer -> right click project ->properties->debugging->Run64bitRuntime->set to false.

## Download issues
If you install successfully, but the solution shows **"incompatible"** and "The application is not installed". Please go to Extensions -> Manage Extensions -> Installed and enable "SQL Server Integration Services Project". And relaunch VS

If you get an error during installation, and find **"Bundle action failed: Invalid pointer (0x80004003)"** in the log.You can check the logs under %temp%\SsdtisSetup, the  more detail log is under Microsoft.DataTools.IntegrationServices_{timstamp}_ISVsix.log. 
- If the error is "The file {filefullpath} already exists." 
   1. ```
      cd C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\Common7\IDE
      rm CommonExtensions\Microsoft\SSIS\* 
      rm PublicAssemblies\SSIS\* 
      rm "PublicAssemblies\Microsoft BI\Business Intelligence Projects\Integration Services\"*
      ```
   2. Repair the vs2022
   3. Restart and reinstall
- If the error is "System.NullReferenceException: Object reference not set to an instance of an object."
    - delete the broken instance folder: “%ProgramData%\Microsoft\VisualStudio\Packages\_Instances\<InstallationID>"
- If the error is "Error 0x80091007: Failed to verify hash of payload"
    - delete C:\ProgramData\Package Cache\15160B731819F56D87A626F9A2777550340022D7 and retry.
- If it is not above error in ISVsix.log, you can zip %temp%\SsdtisSetup and send the logs to ssistoolsfeedbacks@microsoft.com for troubleshooting.

