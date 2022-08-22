---
title: SSIS Projects extension for VS2019 troubleshooting guide
description: "View the release notes for all versions of SQL Server Data Tools (SSDT) that work with Visual Studio 2019 and earlier Visual Studio versions."
ms.prod: sql
ms.prod_service: sql-tools
ms.technology: ssdt
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

## Important
  - **Version 3.16 is the latest general availability (GA) version, which doesn't support target server version SqlServer2022. Download [SQL Server Integration Services Projects 3.16](https://ssis.gallerycdn.vsassets.io/extensions/ssis/sqlserverintegrationservicesprojects/3.16/1645603822968/Microsoft.DataTools.IntegrationServices.exe).
More information is available in release notes.**

- **Due to a limitation of VS marketplace, the version 4.1.2 does not introduce new binaries to download. Version 4.1 contains the latest binaries.**

- Since version 3.3, Power Query Source for SQL Server 2017 and Microsoft Oracle Connector for SQL Server 2019 have been excluded from the installation of this product. To continue using these two components, manually download and install them by yourselves. Here are the download links: [Power Query Source for SQL Server 2017 and 2019](https://www.microsoft.com/download/details.aspx?id=100619), [Microsoft Oracle Connector for SQL Server 2019](https://www.microsoft.com/download/details.aspx?id=58228)

## Common Issues
- SSIS Execute Package Task doesn't support debugging when ExecuteOutOfProcess is set to True.

- SQL Server Integration Services Projects extension doesn't support Visual Studio 2022 yet. 

- In the latest general availability (GA) version, to design packages using Oracle and Teradata connectors and targeting an earlier version of SQL server prior to SQL 2019, in addition to the [Microsoft Oracle Connector for SQL 2019](https://www.microsoft.com/download/details.aspx?id=58228) and [Microsoft Teradata Connector for SQL 2019](https://www.microsoft.com/download/details.aspx?id=100599), you need to also install the corresponding version of Microsoft Connector for Oracle and Teradata by Attunity.
  - [Microsoft Connector Version 5.0 for Oracle and Teradata by Attunity targeting SQL Server 2017](https://www.microsoft.com/download/details.aspx?id=55179)
  - [Microsoft Connector Version 4.0 for Oracle and Teradata by Attunity targeting SQL Server 2016](https://www.microsoft.com/download/details.aspx?id=52950)
  - [Microsoft Connector Version 3.0 for Oracle and Teradata by Attunity targeting SQL Server 2014](https://www.microsoft.com/download/details.aspx?id=44582)
  - [Microsoft Connector Version 2.0 for Oracle and Teradata by Attunity targeting SQL Server 2012](https://www.microsoft.com/download/details.aspx?id=29283)

- Sometimes this product or Visual Studio Tools for Applications 2019 may be somehow deleted during VS instance upgrade. If your existing SSIS projects cannot be loaded, try to repair this product via control panel. If VS doesn't pop up when clicking on "Edit Script", try to repair VSTA 2019 via control panel. We've reported this issue to VS team. Sorry for any inconvenience.

- SQL Server Native Client (SQLNCLI11.1) is deprecated and not installed by VS2019. We recommend upgrading to the new [Microsoft OLE DB driver for SQL Server](../connect/oledb/download-oledb-driver-for-sql-server.md). If you want to continue using SQL Server Native Client, you can download and install it from [here](https://www.microsoft.com/download/details.aspx?id=50402).

## Known issues
  **Version 4.1**
- Known issues:
    1. **Cannot design Oracle and Teradata Components.**

**Version 4.0 preview:**
- Known issues:
    1. **Cannot design Oracle and Teradata Components.**
    2. CDC source component in target SQL Server 2022 can’t do preview.
    3. **When executing SSIS project targeting SqlServer 2019 on the environment that SQL Server 2019 are also installed**, the execution will fail with error "Unable to cast COM object of type System._ComObject to interface type Microsoft.SqlServer.Dts.Runtime.Wrapper.Sql2019.IDTSApplication160".
Workaround: Solution Explorer -> right click project ->properties->debugging->Run64bitRuntime->set to false.

 ## Download issues
 
If you get an error during installation, you can check the logs under %temp%\SsdtisSetup.
Usually, the detail error log is at the end of Microsoft.DataTools.IntegrationServices_{timstamp}_ISVsix.log. 
- If the error is "The file {filefullpath} already exists." 
   1. ```
      cd C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\Common7\IDE
      rm CommonExtensions\Microsoft\SSIS\* 
      rm PublicAssemblies\SSIS\* 
      rm "PublicAssemblies\Microsoft BI\Business Intelligence Projects\Integration Services\"*
      ```
   2. Repair the vs2019 
   3. Restart and start ssdt setup install again
- If the error is "Microsoft.VisualStudio.Setup.CanceledByPrecheckException: Pre-check verification failed with warning(s) :  AnotherInstallationRunning."
    - Kill msiexec.exe process and relaunch. 
 - If it is not above error in ISVsix.log, you can zip the folder and send the logs to ssistoolsfeedbacks@microsoft.com for troubleshooting.
