---
title: Release notes for SQL Server Data Tools (SSDT) in VS2019
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
# Release notes for SQL Server Data Tools (SSDT) in VS2019

[!INCLUDE [sql-asdb-asa](../includes/applies-to-version/sql-asdb-asa.md)]

> [!IMPORTANT]
> Release notes updates for **SSDT 2019 and later** are now listed under [Visual Studio](/visualstudio/ide/whats-new-visual-studio-docs).
> You can download the [SSAS](https://marketplace.visualstudio.com/items?itemName=ProBITools.MicrosoftAnalysisServicesModelingProjects), [SSIS](https://marketplace.visualstudio.com/items?itemName=SSIS.SqlServerIntegrationServicesProjects), and the [SSRS](https://marketplace.visualstudio.com/items?itemName=ProBITools.MicrosoftReportProjectsforVisualStudio) 2019 and later VSIX files from the [Visual Studio Marketplace](<https://marketplace.visualstudio.com/>).

Visit https://techcommunity.microsoft.com/t5/SQL-Server-Integration-Services/bg-p/SSIS for the latest information, tips, news, and announcements about SSIS directly from the product team.

## Important
  - **Version 3.16 is the latest general availability (GA) version, which does not support target server version SqlServer2022. Download [SQL Server Integration Services Projects 3.16](https://ssis.gallerycdn.vsassets.io/extensions/ssis/sqlserverintegrationservicesprojects/3.16/1645603822968/Microsoft.DataTools.IntegrationServices.exe).
More information is available in release notes.**
  - Since version 3.3, Power Query Source for SQL Server 2017 and Microsoft Oracle Connector for SQL Server 2019 have been excluded from the installation of this product. To continue using these two components, please manually download and install them by yourselves. Here are the download links: [Power Query Source for SQL Server 2017 and 2019](https://www.microsoft.com/download/details.aspx?id=100619), [Microsoft Oracle Connector for SQL Server 2019](https://www.microsoft.com/download/details.aspx?id=58228)

## Common Issues
- In the latest general availability (GA) version, to design packages using Oracle and Teradata connectors and targeting an earlier version of SQL server prior to SQL 2019, in addition to the [Microsoft Oracle Connector for SQL 2019](https://www.microsoft.com/download/details.aspx?id=58228) and [Microsoft Teradata Connector for SQL 2019](https://www.microsoft.com/download/details.aspx?id=100599), you need to also install the corresponding version of Microsoft Connector for Oracle and Teradata by Attunity.
  - [Microsoft Connector Version 5.0 for Oracle and Teradata by Attunity targeting SQL Server 2017](https://www.microsoft.com/download/details.aspx?id=55179)
  - [Microsoft Connector Version 4.0 for Oracle and Teradata by Attunity targeting SQL Server 2016](https://www.microsoft.com/download/details.aspx?id=52950)
  - [Microsoft Connector Version 3.0 for Oracle and Teradata by Attunity targeting SQL Server 2014](https://www.microsoft.com/download/details.aspx?id=44582)
  - [Microsoft Connector Version 2.0 for Oracle and Teradata by Attunity targeting SQL Server 2012](https://www.microsoft.com/download/details.aspx?id=29283)

- Sometimes this product or Visual Studio Tools for Applications 2019 may be somehow deleted during VS instance upgrade. If your existing SSIS projects cannot be loaded, please try to repair this product via control panel. If VS doesn't pop up when clicking on "Edit Script", please try to repair VSTA 2019 via control panel. We've reported this issue to VS team. Sorry for any inconvenience.

- SQL Server Native Client (SQLNCLI11.1) is deprecated and not installed by VS2019. We recommend upgrading to the new [Microsoft OLE DB driver for SQL Server](/sql/connect/oledb/download-oledb-driver-for-sql-server). If you want to continue using SQL Server Native Client, you can download and install it from [here](https://www.microsoft.com/download/details.aspx?id=50402).

- SQL Server Integration Services Projects extension doesn't support Visual Studio 2022 yet. 

## Offline Installation
Please follow the below steps to install this product in an offline environment:
1. Follow this [document](/visualstudio/install/create-an-offline-installation-of-visual-studio) to create an offline installation of Visual Studio, and make sure following prerequisites are included:
    - Prerequisite Id="Microsoft.VisualStudio.Component.Roslyn.LanguageServices" Version="[16.0,)" DisplayName="C# and Visual Basic"
    - Prerequisite Id="Microsoft.VisualStudio.Component.CoreEditor" Version="[16.0,)" DisplayName="Visual Studio core editor"
    - Prerequisite Id="Microsoft.VisualStudio.Component.SQL.SSDT" Version="[16.0,)" DisplayName="SQL Server Data Tools"
    - Prerequisite Id="Microsoft.Net.Component.4.TargetingPack" Version="[16.0,)" DisplayName=".NET Framework 4 targeting pack"
    - Prerequisite Id="Microsoft.Net.Component.4.5.TargetingPack" Version="[16.0,)" DisplayName=".NET Framework 4.5 targeting pack"
    - Prerequisite Id="Microsoft.Net.Component.4.7.TargetingPack" Version="[16.0,)" DisplayName=".NET Framework 4.7 targeting pack"

1. Double click on the installer of this product and perform installation, or you can run the installer in quite mode. Please launch the installer with "/?" argument to get more details of the arguments list of the installer.

1. VS Community does not support offline activation. To use this product with VS Community, you must login to your Microsoft account occasionally in VS Community. If you want to use this product in a totally offline environment, we recommend you to install this product on VS Professional or Enterprise, which support offline activation via a product key.

## Release Notes
  **Version 4.1**
- Release Date: Jul 26th, 2022
- Build Version: 16.0.694.0
- Tested against Visual Studio 2019 16.11
- What’s new:
    1. Support Microsoft Analysis targets to sqlserver 2022. 
    2. IR deployment wizard is now supporting Synapse link.
- Bug fixes:
    1. Fixed a bug that testing connection in OData connection manager failed when the authentication type is “Microsoft Dynamics CRM online” and target server version is SQL Server 2016 or SQL Server 2017.  
    2. Fixed a bug that ssis project targeting SqlServer 2019 execute will fail if the machine install sqlserver 2019 at the same time.
- Known issues:
    1. SSIS Execute Package Task doesn't support debugging when ExecuteOutOfProcess is set to True.
    2. **Cannot design Oracle and Teradata Components.**

**Version 4.0 preview:**
- Release Date: Jun 1th, 2022
- Download [SQL Server Integration Services Projects 4.0](https://ssis.gallerycdn.vsassets.io/extensions/ssis/sqlserverintegrationservicesprojects/4.0/1654076518070/Microsoft.DataTools.IntegrationServices.exe)
- Build Version: 16.0.521.0
- Tested against Visual Studio 2019 16.11
- What’s new:
    1. Support Sqlserver2022 as target server version
- Bug fixes:
    1. Fixed ODBC incorrect binding when output column sequence is different from external column sequence
    2. Fixed ODBC connection manager incorrectly put connection string keyword “UID” as “User ID” under certain circumstances.
- Known issues:
    1. SSIS Execute Package Task doesn't support debugging when ExecuteOutOfProcess is set to True.
    2. **Cannot design Oracle and Teradata Components.**
    3. CDC source component in target SQL Server 2022 can’t do preview.
    4. **When executing ssis project targeting SqlServer 2019 on the environment that SQL Server 2019 are also installed**, the execution will fail with error "Unable to cast COM object of type System._ComObject to interface type Microsoft.SqlServer.Dts.Runtime.Wrapper.Sql2019.IDTSApplication160".
Workaround: Solution Explorer -> right click project ->properties->debugging->Run64bitRuntime->set to false.

**Version 3.16:**
- Release Date: Feb 23th, 2022
- Download [SQL Server Integration Services Projects 3.16](https://ssis.gallerycdn.vsassets.io/extensions/ssis/sqlserverintegrationservicesprojects/3.16/1645603822968/Microsoft.DataTools.IntegrationServices.exe)
- Build Version: 15.0.2000.180
- Tested against Visual Studio 2019 16.11
- Bug fixes:
    1. Fixed an issue that executing SSIS package via Azure will fail on Azure-SSIS IR under newly created data factory
    1. Removed VS2022 option from installer which is not supported now.
    1. Removed dependency on log4j .
- Known issues:
    1. SSIS Execute Package Task doesn't support debugging when ExecuteOutOfProcess is set to True.

**Version 3.15:**
- Release Date: Aug 16th, 2021
- Download [SQL Server Integration Services Projects 3.15](https://ssis.gallerycdn.vsassets.io/extensions/ssis/sqlserverintegrationservicesprojects/3.15/1629091547410/Microsoft.DataTools.IntegrationServices.exe)
- Build Version: 15.0.2000.170
- Tested against Visual Studio 2019 16.11
- Bug fixes:
    1. Fixed an issue that cannot distinguish stable and preview instance for same VS version on installer.
    1. Fixed an issue that cannot sign in with AAD when editing Analysis Services Processing Task.
- Known issues:
    1. SSIS Execute Package Task doesn't support debugging when ExecuteOutOfProcess is set to True.


**Version 3.14:**
- Release Date: Jun 3rd, 2021
- Download [SQL Server Integration Services Projects 3.14](https://ssis.gallerycdn.vsassets.io/extensions/ssis/sqlserverintegrationservicesprojects/3.14/1622687129435/Microsoft.DataTools.IntegrationServices.exe)
- Build Version: 15.0.2000.167
- Tested against Visual Studio 2019 16.10
- What's new:
    1. Azure-enabled SQL Server Integration Services (SSIS) projects now support national cloud (Azure US Government and Azure China).
- Bug fixes:
    1. Fixed some issues related to accessibility and high-DPI.
- Known issues:
    1. SSIS Execute Package Task doesn't support debugging when ExecuteOutOfProcess is set to True.

**Version 3.13.1:**
- Release Date: Apr 22nd, 2021
- Download [SQL Server Integration Services Projects 3.13.1](https://ssis.gallerycdn.vsassets.io/extensions/ssis/sqlserverintegrationservicesprojects/3.13.1/1619075132660/Microsoft.DataTools.IntegrationServices.exe)
- Build Version: 15.0.2000.166
- Tested against Visual Studio 2019 16.9
- Bug fixes:
    1. Fixed an issue that tasks and data flow components cannot be renamed in design surface.
    1. Fixed an issue that cannot add annotation in design surface.
    1. Fixed an issue that SSIS containers cannot be collapsed.
- Known issues:
    1. SSIS Execute Package Task doesn't support debugging when ExecuteOutOfProcess is set to True.

**Version 3.13:**
- Release Date: Apr 19th, 2021
- Download [SQL Server Integration Services Projects 3.13](https://ssis.gallerycdn.vsassets.io/extensions/ssis/sqlserverintegrationservicesprojects/3.13/1618812066840/Microsoft.DataTools.IntegrationServices.exe)
- Build Version: 15.0.2000.165
- Tested against Visual Studio 2019 16.9
- Whats' new:
    1. Add ExecuteOnProxy property on Execute Process Task so as to support [enabling self-hosted Integration Runtime as proxy](/azure/data-factory/self-hosted-integration-runtime-proxy-ssis).
    1. Allow connectivity retry for OLE DB connection manager via exposing ConnectRetryCount and ConnectRetryInterval properties. 
    1. List available locations based on selected subscription in Integration Runtime Creation Wizard
- Bug fixes:
    1. Fixed some issues related to accessibility and high-DPI.
- Known issues:
    1. SSIS Execute Package Task doesn't support debugging when ExecuteOutOfProcess is set to True.

**Version 3.12.1:**
- Release Date: March 17th, 2021
- Build Version: 15.0.2000.157
- Tested against Visual Studio 2019 16.9
- Bug fixes:
    1. Fixed an issue that script task/component cannot be saved in VS2019 16.9.
- Known issues:
    1. SSIS Execute Package Task doesn't support debugging when ExecuteOutOfProcess is set to True.

**Version 3.12:**
- Release Date: Jan 25th, 2021
- Build Version: 15.0.2000.152
- Tested against Visual Studio 2019 16.8
- Whats' new:
    1. Add ExecuteOnProxy property on Execute SQL Task so as to support [enabling self-hosted Integration Runtime as proxy](/azure/data-factory/self-hosted-integration-runtime-proxy-ssis).
- Bug fixes:
    1. Fixed an issue that test connection on Analysis Services connection manager may fail due to fail to load managed ADAL component.
    1. Fixed some issues related to accessibility and high-DPI.
- Known issues:
    1. SSIS Execute Package Task doesn't support debugging when ExecuteOutOfProcess is set to True.

**Version 3.11.1:**
- Release Date: December 28th, 2020
- Build Version: 15.0.2000.150
- Tested against Visual Studio 2019 16.8
- Bug fixes:
    1. Fixed an issue that Data Flow Path Editor cannot show up.
- Known issues:
    1. SSIS Execute Package Task doesn't support debugging when ExecuteOutOfProcess is set to True.


**Version 3.11:**
- Release Date: December 23th, 2020
- Build Version: 15.0.2000.149
- Tested against Visual Studio 2019 16.8
- Whats' new:
    1. Make creating SSISDB optional in IR Creation Wizard.
- Bug fixes:
    1. Fixed an issue that Azure Subscription ComboBox items are duplicate in IR Creation Wizard and Azure-Enabled Project Wizard when different subscriptions have the same name.
    1. Fixed an issue that auto-generated code under bufferwrapper.cs of script component adds extra double-quotes when current locale is Germany.
    1. Fixed an issue that download WSDL buttonis not displayed when target server version is SQL Server 2012, 2014, 2016.
    1. Fixed an issue that building large projects may fail due to out of memory exception.
    1. Fixed an issue that the package is not downgraded to current target server version of the project when it is saved as copy to file system or MSDB in package deployment model.
    1. Fixed an issue that Dimension Processing Destination doesn't work due to "No such interface" error.
    1. Fixed some issues related to accessibility and high-DPI.
- Known issues:
    1. SSIS Execute Package Task doesn't support debugging when ExecuteOutOfProcess is set to True.


**Version 3.10:**
- Release Date: September 23th, 2020
- Build Version: 15.0.2000.132
- Tested against Visual Studio 2019 16.7
- Bug fixes:
    1. Fixed an issue that Azure Subscription ComboBox items are duplicate in IR Creation Wizard and Azure-Enabled Project Wizard when different subscriptions have the same name.
    1. Fixed an issue that sometimes Connect button cannot be enabled in IR Creation Wizard.
    1. Fixed some issues related to accessibility and high-DPI.
- Known issues:
    1. SSIS Execute Package Task doesn't support debugging when ExecuteOutOfProcess is set to True.


**Version 3.9:**
- Release Date: August 24th, 2020
- Build Version: 15.0.2000.128
- Tested against Visual Studio 2019 16.7
- What's new:
    1. Allow users to skip validation when opening packages, which improves the performance. Please refer to this [article](https://techcommunity.microsoft.com/t5/sql-server-integration-services/accelerate-the-opening-of-ssis-package-in-ssdt/ba-p/1607099) for more details.
    2. Block deployment to Azure-SSIS when target server version is not SQL Server 2017.
- Bug fixes:
    1. Fixed some issues related to accessibility and high-DPI.
- Known issues:
    1. SSIS Execute Package Task doesn't support debugging when ExecuteOutOfProcess is set to True.


**Version 3.8:**
- Release Date: June 20th, 2020
- Build Version: 15.0.2000.123
- Tested against Visual Studio 2019 16.6
- Bug fixes:
    1. Fixed an issue that "Preview" button doesn't work on OLE DB source when connecting to an Analysis Services data source.
    1. Fixed an issue that removing an input or output of a data flow component before removing the associated path may cause a COMException.
    1. Fixed an issue that AS Processing task cannot connect to PBI workspace and refresh its models.
    1. Fixed an issue that VS will hang on debugging script task/component when using x64 runtime and targeting to SQL Server 2017.
    1. Fixed an issue that Import/Export wizard will crash when selecting MySql driver in some environment.
    1. Fixed some issues related to accessibility and high-DPI.
- Known issues:
    1. SSIS Execute Package Task doesn't support debugging when ExecuteOutOfProcess is set to True.


**Version 3.7:**
- Release Date: May 20th, 2020
- Build Version: 15.0.2000.121
- Tested against Visual Studio 2019 16.6
- What's New:
    1. Added assessment for packages to be executed in Azure in Azure-enabled project.
- Bug fixes:
    1. Fixed an issue that Trasfer Database Task does not work when a database has filegroups that contains a filestream.
    1. Fixed an issue that when using ODBC components in Foreach Loop component, the ODBC component will meet 'Function sequence error' in the second loop during package execution.
    1. Fixed an issue that Rebuild Index Task UI will be cut off in low resolution mode.
    1. Fixed an issue that the "Sign In" button does not show up in high DPI mode.
    1. Fixed an issue that connection manager elements are displayed too large in high DPI mode.
    1. Fixed an issue that execution results are stacked on top of each other in high DPI mode.
- Known issues:
    1. SSIS Execute Package Task doesn't support debugging when ExecuteOutOfProcess is set to True.


**Version 3.6:**
- Release Date: April 20th, 2020
- Build Version: 15.0.2000.94
- Tested against Visual Studio 2019 16.5
- What's New:
    1. Added support for searching tasks and pipeline components by adding a search box in SSIS toolbox.
    1. Added progress bar when switching target server version.
    1. Added additional cloud configuration for Azure-enabled project and add Windows Authentication support for executing packages in Azure.
- Bug fixes:
    1. Fixed an issue that in some cases VS instances cannot be listed in the installer
    1. Fixed an issue that this production cannot be uninstalled if the VS instance has been uninstalled.
    1. Fixed an issue that a script component copied from another one in the same package cannot be correctly loaded during debugging when target server version is lower than SQL Server 2019.
    1. Fixed an accessibility issue that luminosity ratio for the component connector lines are less than 3:1 under package designer window.
    1. Fixed an accessibility issue that luminosity ratio is less than 3:1 for “Fit View to window” control present under package designer window.
- Known issues:
    1. SSIS Execute Package Task doesn't support debugging when ExecuteOutOfProcess is set to True.


**Version 3.5:**
- Release Date: March 17th, 2020
- Build Version: 15.0.2000.93
- Tested against Visual Studio 2019 16.5
- Bug fixes:
    1. Fixed an issue that clicking on MSOLAP driver properties button makes DTS wizard crash when SQL Server is not installed.
    1. Fixed an issue that MSOLEDBSQL driver doesn't support AAD auth in DTS Wizard.
    1. Fixed an issue that XML Source and ADO.NET Destination cannot be correctly persisted when targeting to SQL Server 2012.
    1. Fixed an issue that the "Download WSDL" button in Web Service Task editor may not be properly displayed.
    1. Fixed an issue that table may not be able to be selected in Connection Manager page of LookUp Transformation editor.
    1. Fixed an issue that the layout of Cache Transformation editor may be messed.
    1. Fixed an issue that the "Connection Managers" area in package editor may not be properly displayed.
    1. Fixed an issue that the status icon may not be properly displayed in the Convert to Package Deployment Model wizard.
- Known issues:
    1. SSIS Execute Package Task doesn't support debugging when ExecuteOutOfProcess is set to True.


**Version 3.4:**
- Release Date: January 19th, 2020
- Build Version: 15.0.2000.71
- What’s new:
    1. Default target server version has been changed to SQL Server 2019 when creating a new SSIS Project via the templates “Integration Services Project” or “Integration Services Import Project Wizard”.
- Bug fixes:
    1. Fixed an issue that VS may crash when moving control flow constraint lines inside a container.
    1. Fixed an issue that maintenance plan tasks’ UI cannot list ADO.NET connection managers created outside of the task UI
    1. Fixed an issue that Azure interactive login page doesn't show up when deploying an SSAS project which belongs to a solution also having SSIS projects loaded
- Known issues:
    1. SSIS Execute Package Task doesn't support debugging when ExecuteOutOfProcess is set to True.


**Version 3.3:**
- Release Date: December 25th, 2019
- Build Version: 15.0.2000.68
- This is the general available (GA) release.
- What's New:
    1. Removed the inbox component Power Query Source for SQL Server 2017. Now we have announced Power Query Source for SQL Server 2017 & 2019 as out-of-box component, which can be downloaded [here](https://www.microsoft.com/download/details.aspx?id=100619).
    1. Removed the inbox component Microsoft Oracle Connector for SQL Server 2019. Now we have announced Microsoft Oracle Connector for SQL Server 2019 as out-of-box component, which can be downloaded [here](https://www.microsoft.com/download/details.aspx?id=58228).
    1. Added localization support for designer UI when target server version is SQL Server 2012. Please refer to the release notes of version 3.2 for the full list of supported languages.
- Bug Fixes:
    1. Fixed an issue that SSIS debugger may occasionally fail to be launched due to IDtsHost interface not registered when target server version is SQL Server 2017 or 2019.
    1. Fixed an issue that there are duplicated "Integration Services Project" template in "Create a new project" dialog.
    1. Fixed an issue that "Integration Services Project" and "Integration Services Import Project Wizard" cannot be searched with keyword "SSIS" in "Create a new project" dialog.
    1. Fixed an issue that ODBC components UI layout is not displayed properly in high DPI mode.
    1. Fixed an issue that OLE DB connection manager UI is not displayed correctly when the provider is selected as "Microsoft OLE DB Driver for SQL Server" (MSOLEDBSQL).
- Known issues:
    1. SSIS Execute Package Task doesn't support debugging when ExecuteOutOfProcess is set to True.


**Version 3.2.2 Preview:**
- Release Date: November 28th, 2019
- Build Version: 15.0.1900.80
- This is the second hotfix release for version 3.2 preview. We do not recommend using it for production. You don't need to upgrade to this version unless you hit the issue mentioned below.
- Bug Fixes:
    1. Fixed an issue that Visual Studio stopped responding when trying to connect to SSIS Integration Runtime in Azure Data Factory or opening a project that was connected to SSIS Integration Runtime.
- Known issues:
    1. SSIS Execute Package Task doesn't support debugging when ExecuteOutOfProcess is set to True.
    1. When the target server version is set to SQL Server 2012, the display language will always be English.


**Version 3.2.1 Preview:**
- Release Date: November 19th, 2019
- Build Version: 15.0.1900.78
- This is a hotfix release for version 3.2 preview. We do not recommend using it for production. You don't need to upgrade to this version unless you hit the issue mentioned below.
- Bug Fixes:
    1. Fixed an issue that VS may throw TypeLoadException from assembly 'Microsoft.DataWarehouse.VsIntegration, Version=15.6.0.0' when designing the package.
- Known issues:
    1. SSIS Execute Package Task doesn't support debugging when ExecuteOutOfProcess is set to True.
    1. When the target server version is set to SQL Server 2012, the display language will always be English.


**Version 3.2 Preview:**
- Release Date: November 5th, 2019
- Build Version: 15.0.1900.73
- This is the third and the last preview release. We do not recommend using it for production.
- What's New:
    1. Fixed major UI layout issues in high DPI mode.
    1. Upgraded .NET framework version to 4.7 for script task/component when the target server version is SQL Server 2019
    1. Added ConnectByProxy property in ODBC Connection Manager so as to support [enabling self-hosted Integration Runtime as proxy](/azure/data-factory/self-hosted-integration-runtime-proxy-ssis) in ODBC connection manager.
    1. Added localization support for the installer and the designer UI when the target server version is SQL Server 2014, or 2016. Now we support following languages:
        - Chinese (Simplified)
        - Chinese (Traditional)
        - English (United States)
        - French
        - German
        - Italian
        - Japanese
        - Korean
        - Portuguese (Brazil)
        - Russian
        - Spanish
- Bug Fixes:
    1. Fixed an issue that users could not add new data sources under package deployment mode.
    1. Fixed an issue that users could not debug script task/component if the code used any new syntaxes introduced after .NET 4.5.
    1. Fixed an issue that data viewer, variable window, getting started window and SSIS toolbox could not be displayed properly if .NET 4.8 is installed.
    1. Fixed an issue that creating the first Data Factory in Azure subscription via Integration Runtime Creation Wizard might fail due to Data Factory resource provider not being registered. 
    1. Fixed an issue that the SSIS in ADF Connection Wizard could not display the Azure storage account list correctly when there was a file only storage account in the subscription.
    1. Fixed an issue that "Execute in Azure" did not work when the package included a container.
    1. Fixed an issue that char(n char) and varchar2(n char) were mapped to incorrect DTS types in Oracle Connector.
- Known issues:
    1. SSIS Execute Package Task doesn't support debugging when ExecuteOutOfProcess is set to True.
    1. When the target server version is set to SQL Server 2012, the display language will always be English.


**Version 3.1 Preview:**
- Release Date: July 8th, 2019
- Build Version: 15.0.1301.433
- This is the second preview release. We do not recommend using it for production.
- What's New:
    1. Add AzureEnabled feature. Enable packages of the project to be run on SSIS Platform-as-a-Service (PaaS) in Azure Data Factory.
- Bug Fixes:
    1. Fix an issue that Oracle connector properties are not able to be set from variable expression
    1. Fix an issue that Oracle connector has VS_NEEDSNEWMETATDATA error when debugging packages targeted to pre-SQL Server 2019
    1. Fix an issue that Oracle connector failed to upgrade/downgrade package/project if the package/project uses expressions for connection manager’s properties.
    1. Fix an issue that Download WSDL button of Web Service Task Editor doesn't support TLS 1.1 & 1.2 protocal (targetting to SQL Server 2019).
    1. Fix an issue that packages containing DQS connection manager cannot be loaded again after saving. 
- Known issues:
    1. SSIS Execute Package Task doesn't support debugging when ExecuteOutOfProcess is set to True.
    1. The installer is not localized.
    1. When the target server version is set to SQL Server 2012~2016, the display language will always be English
    1. Cannot add new data source under package deployment mode. The error message is "The wizard for 'Microsoft.AnalysisServices.DataSource' cannot be found."
    1. Variable window and SSIS toolbox may not be displayed properly if .NET 4.8 is installed (Windows 10 1903 installs .NET 4.8 by default). To work around this: 1) open Tools->Options window; 2) navigate to Environment->General; 3) uncheck "Optimize rendering for screens with different pixel densities"; 4) restart VS. For more details of this issue, please see: https://developercommunity.visualstudio.com/content/problem/638322/vs-2019-regression-transparent-toolwindowpane-with.html


**Version 3.0 Preview:**
- Release Date: April 15th, 2019
- Build Version: 15.0.1300.375
- Initial release of SQL Server Integration Services Projects. This is a preview release. We do not recommend using it for production.
- Known issues:
    1. SSIS Execute Package Task doesn't support debugging when ExecuteOutOfProcess is set to True.
    1. The installer is not localized.
    1. When the target server version is set to SQL Server 2012~2016, the display language will always be English
    1. On some systems, the OLEDB connection manager connection setting page is not displayed correctly. Use the All properties grid to configure connection settings. 
    1. Cannot be uninstalled from control panel. Here is a work around: open extension.vsixmanifest under &lt;VSInstanceRootDir&gt;\Common7\IDE\CommonExtensions\Microsoft\SSIS, change the value of attribute "InstalledByMsi" at line 10 from "true" to "false", save extension.vsixmanifest, and then you can uninstall this product from control panel.
    1. Installation will fail if you install this product on an empty VS instance which doesn't install any workloads. To work around this, please make sure NuGet manager is installed before you launching this installer.
    1. Cannot add new data source under package deployment mode. The error message is "The wizard for 'Microsoft.AnalysisServices.DataSource' cannot be found."
    1. Packages containing DQS connection manager cannot be opened again after saving using this product.
