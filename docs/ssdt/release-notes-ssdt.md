---
title: Release notes for SQL Server Data Tools (SSDT) in VS2017
description: "View the release notes for all versions of SQL Server Data Tools (SSDT) that work with Visual Studio 2017 and earlier Visual Studio versions."
ms.service: sql
ms.subservice: ssdt
ms.topic: conceptual
author: chugugrace
ms.author: chugu
ms.reviewer: maghan, drskwier
ms.custom: seo-lt-2019
ms.date: 04/14/2022
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=azuresqldb-mi-current"
---

# Release notes for SQL Server Data Tools (SSDT) in VS2017

[!INCLUDE [sql-asdb-asa](../includes/applies-to-version/sql-asdb-asa.md)]

> [!IMPORTANT]
> Release notes updates for **SSDT 2019 and later** are now listed under [Visual Studio](/visualstudio/ide/whats-new-visual-studio-docs).
> You can download the [SSAS](https://marketplace.visualstudio.com/items?itemName=ProBITools.MicrosoftAnalysisServicesModelingProjects), [SSIS](https://marketplace.visualstudio.com/items?itemName=SSIS.SqlServerIntegrationServicesProjects), and the [SSRS](https://marketplace.visualstudio.com/items?itemName=ProBITools.MicrosoftReportProjectsforVisualStudio) 2019 and later VSIX files from the [Visual Studio Marketplace](<https://marketplace.visualstudio.com/>).

These release notes are for [SQL Server Data Tools (SSDT)](download-sql-server-data-tools-ssdt.md) for Visual Studio (VS).

## 15.9.10,&nbsp; SSDT for VS 2017

_Released:_ &nbsp; April 6, 2022  
_Build Number:_ &nbsp; 14.0.16248.0
_SSDT for Visual Studio 2017._

### What's new?

| New Item | Details |
|-----------------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Integration Services (SSIS) | Fixed issue that executing SSIS package via Azure will fail on Azure-SSIS IR under newly created data factory. |
| Integration Services (SSIS) | Removed dependency on log4j. |

### Known issues

| Known issue | Details |
| :---------- | :------ |
| SSIS Execute Package Task doesn't support debugging when ExecuteOutOfProcess is set to True. | This issue applies only to debugging. Save, deploy, and execution via DTExec.exe or SSIS catalog aren't affected. |

## 15.9.9,&nbsp; SSDT for VS 2017

_Released:_ &nbsp; August 27, 2021  
_Build Number:_ &nbsp; 14.0.16245.0  
_SSDT for Visual Studio 2017._

### What's new?

| New Item | Details |
|-----------------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Integration Services (SSIS) | Azure-enabled SQL Server Integration Services (SSIS) projects now support national cloud (Azure US Government and Azure China). |
| Integration Services (SSIS) | Fixed an issue that can't sign in with Azure Active Directory when editing Analysis Services Processing Task. |
| Integration Services (SSIS) | Fixed some issues related to accessibility and high DPI. |

### Known issues

| Known issue | Details |
| :---------- | :------ |
| SSIS Execute Package Task doesn't support debugging when ExecuteOutOfProcess is set to True. | This issue applies only to debugging. Save, deploy, and execution via DTExec.exe or SSIS catalog aren't affected. |

## 15.9.8,&nbsp; SSDT for VS 2017

_Released:_ &nbsp; April 27, 2021  
_Build Number:_ &nbsp; 14.0.16236.0  
_SSDT for Visual Studio 2017._

### What's new?

| New Item | Details |
|-----------------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Integration Services (SSIS) | Add ExecuteOnProxy property on Execute SQL Task and Execute Process Task so as to support [enabling self-hosted Integration Runtime as proxy](/azure/data-factory/self-hosted-integration-runtime-proxy-ssis). |
| Integration Services (SSIS) | Allow connectivity retry for OLE DB connection manager via exposing ConnectRetryCount and ConnectRetryInterval properties. |
| Integration Services (SSIS) | List available locations based on selected subscription in Integration Runtime Creation Wizard. |
| Integration Services (SSIS) | Fixed an issue that test connection on Analysis Services connection manager may fail due to fail to load managed ADAL component. |
| Integration Services (SSIS) | Fixed some issues related to accessibility and high DPI. |

### Known issues

| Known issue | Details |
| :---------- | :------ |
| SSIS Execute Package Task doesn't support debugging when ExecuteOutOfProcess is set to True. | This issue applies only to debugging. Save, deploy, and execution via DTExec.exe or SSIS catalog isn't affected. |

## 15.9.7,&nbsp; SSDT for VS 2017

_Released:_ &nbsp; January 5, 2021  
_Build Number:_ &nbsp; 14.0.16228.0  
_SSDT for Visual Studio 2017._

### What's new?

| New Item | Details |
|-----------------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Integration Services (SSIS) | Make creating SSISDB optional in IR Creation Wizard. |
| Integration Services (SSIS) | Fixed an issue that Azure Subscription ComboBox items are duplicated in IR Creation Wizard and Azure-Enabled Project Wizard when different subscriptions have the same name. |
| Integration Services (SSIS) | Fixed an issue that sometimes Connect button can't be enabled in IR Creation Wizard. |
| Integration Services (SSIS) | Fixed an issue that Azure Subscription ComboBox items are duplicated in IR Creation Wizard and Azure-Enabled Project Wizard when different subscriptions have the same name. |
| Integration Services (SSIS) | Fixed an issue that auto-generated code under bufferwrapper.cs of script component adds extra double-quotes when current locale is Germany. |
| Integration Services (SSIS) | Fixed an issue that download WSDL button isn't displayed when target server version is SQL Server 2012, 2014, 2016. |
| Integration Services (SSIS) | Fixed an issue that building large projects may fail due to out of memory exception. |
| Integration Services (SSIS) | Fixed an issue that the package isn't downgraded to current target server version of the project when it's saved as copy to file system or MSDB in package deployment model. |
| Integration Services (SSIS) | Fixed an issue that Dimension Processing Destination doesn't work due to "No such interface" error. |
| Integration Services (SSIS) | Fixed some issues related to accessibility and high-DPI. |

### Known issues

| Known issue | Details |
| :---------- | :------ |
| SSIS Execute Package Task doesn't support debugging when ExecuteOutOfProcess is set to True. | This issue applies only to debugging. Save, deploy, and execution via DTExec.exe or SSIS catalog isn't affected. |

## 15.9.6,&nbsp; SSDT for VS 2017

_Released:_ &nbsp; August 31, 2020  
_Build Number:_ &nbsp; 14.0.16222.0  
_SSDT for Visual Studio 2017._

### What's new?

| New Item | Details |
|-----------------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Integration Services (SSIS) | Fixed an issue that **Preview** button doesn't work on the OLE DB source when connecting to a SQL Server Analysis Services (SSAS) data source. |
| Integration Services (SSIS) | Fixed an issue that removing an input or output of a data flow component before removing the associated path might cause a COMException error. |
| Integration Services (SSIS) | Fixed an issue that the SSAS Processing task can't connect to a Power BI workspace and refresh its models. |
| Integration Services (SSIS) | Fixed an issue that Visual Studio hangs on debugging script task/component when using x64 runtime and targeting SQL Server 2017. |
| Integration Services (SSIS) | Fixed an issue that the Import/Export wizard crashes when selecting a MySQL driver in some environments. |
| Integration Services (SSIS) | Fixed some issues related to accessibility and high DPI. |
| Integration Services (SSIS) | Allow users to skip validation when opening packages, which improves performance. For more information, see [Accelerate opening SSIS packages in SSDT](https://techcommunity.microsoft.com/t5/sql-server-integration-services/accelerate-the-opening-of-ssis-package-in-ssdt/ba-p/1607099). |
| Integration Services (SSIS) | Block deployment to Azure-SSIS when target server version isn't SQL Server 2017. |

### Known issues

| Known issue | Details |
| :---------- | :------ |
| SSIS Execute Package Task doesn't support debugging when ExecuteOutOfProcess is set to True. | This issue applies only to debugging. Save, deploy, and execution via DTExec.exe or SSIS catalog isn't affected. |
| Power Query Source might not support OData v4 when SSIS and SSAS are installed on the same Visual Studio instance. | &nbsp; |
| Power Query Source might not support using ODBC to connect to Oracle when SSIS and SSAS are installed on the same Visual Studio instance. | &nbsp; |
| Power Query Source isn't localized. | &nbsp; |

## 15.9.5,&nbsp; SSDT for VS 2017

_Released:_ &nbsp; May 27, 2020  
_Build Number:_ &nbsp; 14.0.16218.0  
_SSDT for Visual Studio 2017._

### What's new?

| New Item | Details |
|-----------------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Integration Services (SSIS) | Added support for searching tasks and pipeline components by adding a search box in SSIS toolbox. |
| Integration Services (SSIS) | Added progress bar when switching target server version. |
| Integration Services (SSIS) | Added additional cloud configuration for Azure-enabled project and add Windows Authentication support for executing packages in Azure. |
| Integration Services (SSIS) | Added assessment for packages to be executed in Azure in Azure-enabled project. |
| Integration Services (SSIS) | Fixed an issue that in some cases VS instances can't be listed in the installer |
| Integration Services (SSIS) | Fixed an issue that this production can't be uninstalled if the VS instance has been uninstalled. |
| Integration Services (SSIS) | Fixed an issue that a script component copied from another one in the same package can't be correctly loaded during debugging when target server version is lower than SQL Server 2019. |
| Integration Services (SSIS) | Fixed an accessibility issue that luminosity ratios for the component connector lines are less than 3:1 under package designer window. |
| Integration Services (SSIS) | Fixed an accessibility issue that luminosity ratio is less than 3:1 for "Fit View to window" control present under package designer window. |
| Integration Services (SSIS) | Fixed an issue that Transfer Database Task doesn't work when a database has filegroups that contain a filestream. |
| Integration Services (SSIS) | Fixed an issue that when using ODBC components in Foreach Loop component, the ODBC component will meet 'Function sequence error' in the second loop during package execution. |
| Integration Services (SSIS) | Fixed an issue that Rebuild Index Task UI will be cut off in low-resolution mode. |
| Integration Services (SSIS) | Fixed an issue that the "Sign In" button doesn't show up in high DPI mode. |
| Integration Services (SSIS) | Fixed an issue that connection manager elements are displayed too large in high DPI mode. |
| Integration Services (SSIS) | Fixed an issue that execution results are stacked on top of each other in high DPI mode. |

### Known issues

| Known issue | Details |
| :---------- | :------ |
| SSIS Execute Package Task doesn't support debugging when ExecuteOutOfProcess is set to True. | This issue only applies to debugging. Save, deploy, and execution via DTExec.exe or SSIS catalog isn't impacted. |
| Power Query Source may not support OData v4 when SSIS and SSAS are installed on the same Visual Studio instance. | &nbsp; |
| Power Query Source may not support using ODBC to connect to Oracle when SSIS and SSAS are installed on the same Visual Studio instance. | &nbsp; |
| Power Query Source isn't localized | &nbsp; |
| When targeting SQL Server 2017 and SxS with SQL Server 2017 patched with CU19 or later CU, debugging packages containing Script Task/Component with breakpoints hangs if Run64BitRuntime is set to true. | &nbsp; |

## 15.9.4,&nbsp; SSDT for VS 2017

_Released:_ &nbsp; March 26, 2020  
_Build Number:_ &nbsp; 14.0.16214.0  
_SSDT for Visual Studio 2017._

### What's new?

| New Item | Details |
|-----------------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Integration Services (SSIS) | Fixed an issue that VS may crash when moving control flow constraint lines inside a container. |
| Integration Services (SSIS) | Fixed an issue that maintenance plan tasks' UI can't list ADO.NET connection managers created outside of the task UI. |
| Integration Services (SSIS) | Fixed an issue that Azure interactive sign in page doesn't show up when deploying an SSAS project, which belongs to a solution also having SSIS projects loaded. |
| Integration Services (SSIS) | Fixed an issue that clicking on MSOLAP driver properties button makes DTS wizard crash when SQL Server isn't installed. |
| Integration Services (SSIS) | Fixed an issue that MSOLEDBSQL driver doesn't support Azure AD auth in DTS Wizard. |
| Integration Services (SSIS) | Fixed an issue that XML Source and ADO.NET Destination can't be correctly persisted when targeting to SQL Server 2012. |
| Integration Services (SSIS) | Fixed an issue that the "Download WSDL" button in Web Service Task editor may not be properly displayed. |
| Integration Services (SSIS) | Fixed an issue that table may not be able to be selected in Connection Manager page of LookUp Transformation editor. |
| Integration Services (SSIS) | Fixed an issue that the layout of Cache Transformation editor may be messed. |
| Integration Services (SSIS) | Fixed an issue that the "Connection Managers" area in package editor may not be properly displayed. |
| Integration Services (SSIS) | Fixed an issue that the status icon may not be properly displayed in the Convert to Package Deployment Model wizard. |
| Integration Services (SSIS) | Changed the installer to full installer that doesn't require downloading payload from internet. |

### Known issues

| Known issue | Details |
| :---------- | :------ |
| SSIS Execute Package Task doesn't support debugging when ExecuteOutOfProcess is set to True. | This issue only applies to debugging. Save, deploy, and execution via DTExec.exe or SSIS catalog isn't impacted. |
| Power Query Source may not support OData v4 when SSIS and SSAS are installed on the same Visual Studio instance. | &nbsp; |
| Power Query Source may not support using ODBC to connect to Oracle when SSIS and SSAS are installed on the same Visual Studio instance. | &nbsp; |
| Power Query Source isn't localized | &nbsp; |

## 15.9.3,&nbsp; SSDT for VS 2017

_Released:_ &nbsp; January 3, 2020  
_Build Number:_ &nbsp; 14.0.16203.0  
_SSDT for Visual Studio 2017._

### What's new?

| New Item | Details |
|-----------------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Integration Services (SSIS) | Removed the inbox component Power Query Source for SQL Server 2017. Now we have announced Power Query Source for SQL Server 2017 & 2019 as out-of-box component, which can be downloaded [here](https://www.microsoft.com/en-us/download/details.aspx?id=100619). |
| Integration Services (SSIS) | Removed the inbox component Microsoft Oracle Connector for SQL Server 2019. Now we have announced Microsoft Oracle Connector for SQL Server 2019 as out-of-box component, which can be downloaded [here](https://www.microsoft.com/en-us/download/details.aspx?id=58228). |
| Integration Services (SSIS) | Fixed an issue that SSIS debugger may occasionally fail to be launched due to IDtsHost interface not registered when target server version is SQL Server 2017 or 2019. |
| Integration Services (SSIS) | Fixed major UI layout issues in high DPI mode. |
| Integration Services (SSIS) | Upgraded .NET framework version to 4.7 for script task/component when the target server version is SQL Server 2019. |
| Integration Services (SSIS) | Added ConnectByProxy property in ODBC Connection Manager so as to support enabling self-hosted Integration Runtime as proxy in ODBC connection manager. |
| Integration Services (SSIS) | Fixed an issue that users couldn't add new data sources under package deployment mode. |
| Integration Services (SSIS) | Fixed an issue that users couldn't debug script task/component if the code used any new syntaxes introduced after .NET 4.5. |
| Integration Services (SSIS) | Fixed an issue that creating the first Data Factory in Azure subscription via Integration Runtime Creation Wizard might fail due to Data Factory resource provider not being registered. |
| Integration Services (SSIS) | Fixed an issue that the SSIS in ADF Connection Wizard couldn't display the Azure storage account list correctly when there was a file only storage account in the subscription. |
| Integration Services (SSIS) | Fixed an issue that "Execute in Azure" didn't work when the package included a container. |
| Integration Services (SSIS) | Fixed an issue that char(n char) and varchar2(n char) were mapped to incorrect DTS types in Oracle Connector. |

### Known issues

| Known issue | Details |
| :---------- | :------ |
| SSIS Execute Package Task doesn't support debugging when ExecuteOutOfProcess is set to True. | This issue only applies to debugging. Save, deploy, and execution via DTExec.exe or SSIS catalog isn't impacted. |
| Power Query Source may not support OData v4 when SSIS and SSAS are installed on the same Visual Studio instance. | &nbsp; |
| Power Query Source may not support using ODBC to connect to Oracle when SSIS and SSAS are installed on the same Visual Studio instance. | &nbsp; |
| Power Query Source isn't localized | &nbsp; |

## 15.9.2,&nbsp; SSDT for VS 2017

_Released:_ &nbsp; July 17, 2019  
_Build Number:_ &nbsp; 14.0.16194.0  
_SSDT for Visual Studio 2017._

### What's new?

| New Item | Details |
|-----------------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Integration Services (SSIS) | Add AzureEnabled feature. Enable packages of the project to be run on SSIS Platform-as-a-Service (PaaS) in Azure Data Factory |
| Integration Services (SSIS) | Fix an issue that Oracle connector properties aren't able to be set from variable expression |
| Integration Services (SSIS) | Fix an issue that Oracle connector has VS_NEEDSNEWMETATDATA error when debugging packages targeted to pre-SQL Server 2019 |
| Integration Services (SSIS) | Fix an issue that Oracle connector failed to upgrade/downgrade package/project if the package/project uses expressions for connection manager's properties |
| Integration Services (SSIS) | Fix an issue that Download WSDL button of Web Service Task Editor doesn't support TLS 1.1 & 1.2 protocol (target is SQL Server 2019) |
| Integration Services (SSIS) | Fix an issue that packages containing DQS connection manager can't be loaded again after saving |

### Known issues

| Known issue | Details |
| :---------- | :------ |
| SSIS Execute Package Task doesn't support debugging when ExecuteOutOfProcess is set to True. | This issue only applies to debugging. Save, deploy, and execution via DTExec.exe or SSIS catalog isn't impacted. |
| can't create or Edit Data Sources in Package Deployment model. | Fails to open the Data Source Wizard. |
| Power Query Source may not support OData v4 when SSIS and SSAS are installed on the same Visual Studio instance. | &nbsp; |
| Power Query Source may not support using ODBC to connect to Oracle when SSIS and SSAS are installed on the same Visual Studio instance. | &nbsp; |
| Power Query Source isn't localized | &nbsp; |

## 15.9.1,&nbsp; SSDT for VS 2017

_Released:_ &nbsp; April 27, 2019  
_Build Number:_ &nbsp; 14.0.16191.0  
_SSDT for Visual Studio 2017._

### What's new?

| New Item | Details |
|-----------------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Integration Services (SSIS) | Fix an issue that package part can't be correctly persisted when targeting to previous SQL Server version. |
| Integration Services (SSIS) | Fix an issue that can't add expression to precedence constraint when using package part. |
| Integration Services (SSIS) | Fix an issue that the "Help" button of Power Query Source & Connection Manager doesn't link to the correct document. |
| Integration Services (SSIS) | ix an issue that SSIS build version isn't displayed in VS help window. |
| Integration Services (SSIS) | Add the property "ConnectByProxy" for Ole DB and Flat File connection manager, which can enable access on-premises data with Self-hosted IR in Azure-SSIS IR. |
| Integration Services (SSIS) | Fix an issue that ODBC components map to DT_DBDATE data type incorrectly. |
| Integration Services (SSIS) | Add the property "ConnectUsingManagedIdentity" for ADO.NET and OLE DB connection manager, which enable managed identity authentication to connect to data source in Azure-SSIS IR. |

### Known issues

| Known issue | Details |
| :---------- | :------ |
| SSIS Execute Package Task doesn't support debugging when ExecuteOutOfProcess is set to True. | This issue only applies to debugging. Save, deploy, and execution via DTExec.exe or SSIS catalog isn't impacted. |
| can't create or Edit Data Sources in Package Deployment model. | Fails to open the Data Source Wizard. |
| Power Query Source may not support OData v4 when SSIS and SSAS are installed on the same Visual Studio instance. | &nbsp; |
| Power Query Source may not support using ODBC to connect to Oracle when SSIS and SSAS are installed on the same Visual Studio instance. | &nbsp; |
| Power Query Source isn't localized. | &nbsp; |

## 15.9.0,&nbsp; SSDT for VS 2017

_Released:_ &nbsp; January 28, 2019  
_Build Number:_ &nbsp; 14.0.16186.0  
_SSDT for Visual Studio 2017._

### What's new?

| New Item | Details |
|-----------------------------|---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Integration Services (SSIS) | Add Power Query Source (Preview) for SSIS in ADF 2017. |
| Integration Services (SSIS) | Add back the support for SQL Server 2012. |
| Integration Services (SSIS) | Add Oracle source and destination for SQL Server 2019. |
| Integration Services (SSIS) | Oracle source and destination targeting SQL Server 2019 have already been installed by SSDT.<br/><br/>To design package targeting server version 2017 or below, download the corresponding Oracle connector version from Microsoft download site and install it on the SSDT machine.<br/><br/>[Microsoft Connector Version 5.0 for Oracle by Attunity targeting SQL Server 2017](https://www.microsoft.com/download/details.aspx?id=55179)<br/>[Microsoft Connector Version 4.0 for Oracle by Attunity targeting SQL Server 2016](https://www.microsoft.com/download/details.aspx?id=52950)<br/>[Microsoft Connector Version 3.0 for Oracle by Attunity targeting SQL Server 2014](https://www.microsoft.com/download/details.aspx?id=44582)<br/>[Microsoft Connector Version 2.0 for Oracle by Attunity targeting SQL Server 2012](https://www.microsoft.com/download/details.aspx?id=29283) |
| Integration Services (SSIS) | Fix an issue that Script Task/Component can't be loaded when migrating from earlier SSIS versions. |
| Integration Services (SSIS) | Fix an issue that data viewer doesn't work on Windows 7 SP1 and Windows 8.1. |
| Integration Services (SSIS) | Fix an issue that, in some cases, saving the package causes Visual Studio to crash. |
| Integration Services (SSIS) | Fix an issue that, in some cases, the package can't be executed. |
| Integration Services (SSIS) | This problem occurred when both of the following conditions are true:< br />< br /> &bull;   Protection level is EncryptSensitiveWithPassword.< br /> &bull;   Target server version is earlier than SQL Server 2017.          |
| Integration Services (SSIS) | Fix an issue that annotations with default font aren't displayed in SSDT. |
| Integration Services (SSIS) | ISDeploymentWizard supports SQL authentication, Azure Active Directory integrated authentication, and Azure Active Directory password authentication in command-line mode. |

### Known issues

| Known issue | Details |
| :---------- | :------ |
| SSIS Execute Package Task doesn't support debugging when ExecuteOutOfProcess is set to True. | This issue only applies to debugging. Save, deploy, and execution via DTExec.exe or SSIS catalog isn't impacted. |
| Power Query Source may not support OData v4 when SSIS and SSAS are installed on the same Visual Studio instance. | &nbsp; |
| Power Query Source may not support using ODBC to connect to Oracle when SSIS and SSAS are installed on the same Visual Studio instance. | &nbsp; |
| Power Query Source isn't localized. | &nbsp; |

## 15.8.2,&nbsp; SSDT for VS 2017

_Released:_ &nbsp; November 5, 2018  
_Build Number:_ &nbsp; 14.0.16182.0  
_SSDT for Visual Studio 2017._

### What's new?
**SSIS:**

Fixed an issue that deploying SSIS project, which contains packages containing Script Task/Flat file destination to Azure-SSIS will result in the packages failing to execute in Azure-SSIS. 

### Known issues:

- SSIS Execute Package Task doesn't support debugging when ExecuteOutOfProcess is set to True. This issue only applies to debugging. Save, deploy, and execution via DTExec.exe or SSIS catalog isn't impacted.

## 15.8.1,&nbsp; SSDT for VS 2017

_Released:_ &nbsp; September 27, 2018  
_Build number:_ &nbsp; 14.0.16179.0  
_SSDT for Visual Studio 2017._

### What's new?

**SSIS:**

1. Add support for [!INCLUDE[sql-server-2019](../includes/sssql19-md.md)].
2. Remove support for SQL Server 2012.

### Known issues:

- SSIS Execute Package Task doesn't support debugging when ExecuteOutOfProcess is set to True. This issue only applies to debugging. Save, deploy, and execution via DTExec.exe or SSIS catalog isn't impacted.
- Deploying SSIS projects that have packages that contain Script Task/Flat file destination to Azure-SSIS will result in the packages failing to execute in Azure-SSIS.

## 15.8,&nbsp; SSDT for VS 2017

_Released:_ &nbsp; September 5, 2018  
_Build number:_ &nbsp; 14.0.16174.0  
_SSDT for Visual Studio 2017._

### What's new?

**SSIS:**

1. Fix regression in VS 15.8 that saving Script Task/Component will hit compile error.
1. Fix regression in VS 15.8 that deployment wizard doesn't work.
1. Fix an issue that ADO.NET connection manager doesn't support third-party ADO.NET provider.

**Installer:**

- Implement reboot-in-the-middle when installing SSDT on Windows 10.


### Known issues:

- SSIS Execute Package Task doesn't support debugging when ExecuteOutOfProcess is set to True. This issue only applies to debugging. Save, deploy, and execution via DTExec.exe or SSIS catalog isn't impacted.

## 15.7.1,&nbsp; SSDT for VS 2017

_Released:_ &nbsp; July 2, 2018  
_Build number:_ &nbsp; 14.0.16167.0  
_SSDT for Visual Studio 2017._

### What's new?

**SSIS:**

- Add support for new Azure Government AAD authority (login.microsoftonline.us) for use with AS Tasks.
- Fix an issue that AS processing task UI will show "Method not found" when target server version is SQLServer2016.
- Fix an issue that some pipeline components can't be executed when target server version is SQLServer2012.

**Installer:**

- Filter the VS instance list to exclude the instances that can't install SSDT.

### Known issues:

- SSIS Execute Package Task doesn't support debugging when ExecuteOutOfProcess is set to True. This issue only applies to debugging. Save, deploy, and execution via DTExec.exe or SSIS catalog isn't impacted.
- When installing SSDT on Windows 10 and choosing "Install new SQL Server Data Tools for Visual Studio 2017 instance", the installation will fail on "The requested metafile operation isn't supported". Reboot the machine and launch SSDT installer again to continue the installation.

## 15.7.0,&nbsp; SSDT for VS 2017

_Released:_ &nbsp; June 4, 2018  
_Build number:_ &nbsp; 14.0.16165.0  
_SSDT for Visual Studio 2017._

### What's new?

**SSIS:**

- Fix an issue that *Integration Services Designers* page in Options dialog can't be shown properly.  
- Fix an issue that luminosity ratio issue for text appearing in *Sort Transformation Editor* editor.  
- Fix an issue that *Resolve References* dialog disappears when attempting to edit a combobox.  
- Fix an issue that F1 help link of *Hadoop Connection Manager* doesn't work.  
- Fix an issue that script task code will be lost if it's in a container when targeting SQL Server 2016.  

**Installer:**

- Fix an issue that SSAS can't be installed before SSRS and SSIS are installed in VS 15.7.2.

### Known issues:

- SSIS Execute Package Task doesn't support debugging when *ExecuteOutOfProcess* is set to *True*. This issue only applies to debugging. Save, deploy, and execution via DTExec.exe or SSIS catalog isn't impacted.

## 15.6.0,&nbsp; SSDT for VS 2017

_Released:_ &nbsp; April 10, 2018  
_Build number:_ &nbsp; 14.0.16162.0  
_SSDT for Visual Studio 2017._

### What's new?

**SSIS:**

- Fix an issue that AS processing task doesn't log any processing steps when targeting to SQLServer2016 and SQLServer2017
- Fix an issue that access violation will happen when opening dtsx with long non-English task names in SSDT
- Fix an issue that sometimes variable list of ScriptTask will disappear in task UI
- Fix an issue that adding copy of existing package will fail when the package location is SQL Server
- Fix an issue that focus gets stuck while accessing the combo box in some editor dialog box.
- Fix an issue that background won't change while switching VS theme.
- Fix an issue that annotation and loading label is invisible in dark theme.
- Fix an issue that the state property isn't defined correctly for SSIS toolbox disabled items.
- Fix an issue that it always fails to execute WebServiceTask.
- Fix an issue that package deployment will fail if connection string is set to variable having expression dependent on project parameters.

**Installer:**

- Add the link of "Customer Experience Improvement Program for SQL Server Data Tools" in privacy disclaimer.
- Fix an issue that VS installer window pops up when selecting "Install new SQL Server Data Tools for Visual Studio 2017 instance"

### Known issues:
- SSIS Execute Package Task doesn't support debugging when ExecuteOutOfProcess is set to True. This issue only applies to debugging. Save, deploy, and execution via DTExec.exe or SSIS catalog isn't impacted.

## 15.5.2,&nbsp; SSDT for VS 2017

_Build number:_ &nbsp; 14.0.16156.0  
_SSDT for Visual Studio 2017._

### What's new?

**SSIS**
- Fix an issue that migrating SSIS 2008 projects will fail when both SSAS and SSIS are installed to the same VS 2017 instance.
- Fix an issue that Rdlc projects can't be built when both Rdlc report designer and SSIS are installed to same VS 2017 instance.
- Fix an issue that the annotation color can't update.
- Fix an issue that some strings in Hadoop connection manager editor are truncated in other languages.
- Fix an issue that some strings are truncated in OData connection manager editor.
- Fix an issue that some strings are truncated in Integration Services import project wizard window.
- Fix an issue with the title: in the SSIS tool box information window.
- Fix an issue that some strings are truncated in Integration Services Deployment Wizard window. 

**Installer**
- Fix an issue that sometimes downloading payload will fail with error "The system can't find the file specified (0x80070002)".  

### Known issues
- SSIS Execute Package Task doesn't support debugging when *ExecuteOutOfProcess* is set to *True*. This issue only applies to debugging. Save, deploy, and execution via DTExec.exe or SSIS catalog isn't impacted.

## 15.5.1,&nbsp; SSDT for VS 2017

_Build number:_ &nbsp; 14.0.16148.0  
_SSDT for Visual Studio 2017._

### What's new?

Visual Studio 2017 (15.5.1) is the same release as version 15.5.0 except for the following bug fixes to the installer:

1. Fix an issue where the installer stops responding on SQL Server Integration Services post install.
2. Fix an issue where setup fails with the following error message: "The requested metafile operation isn't supported (0x800707D3)".

In addition to these two bug fixes, the following details for 15.5.0 still apply to 15.5.1

## 15.5.0,&nbsp; SSDT for VS 2017

_Build number:_ &nbsp; 14.0.16146.0  
_SSDT for Visual Studio 2017._

### What's new?

SSDT for Visual Studio 2017 (15.5.0) moves from preview to general availability (GA).

**Installer**
1. Setup UI is localized.
1. Replace the icon with a higher-quality version.

**Integration Services (IS)**
1. Added package validation step in Deployment Wizard when deploying to Azure SSIS IR in ADF, which discovers potential compatibility issues in SSIS packages to execute in Azure SSIS IR. For more info, see [Validate SSIS packages deployed to Azure](../integration-services/lift-shift/ssis-azure-validate-packages.md).
1. SSIS extension is localized.

### Bug fixes

**Integration Services (IS)**
1. Fixed an issue where the layout of OLEDB and ADO.NET connection manager is corrupt.
2. Fixed an issue where an assembly not found error is raised when attempting to edit a Dimension Processing Task.

### Known issues

**Integration Services (IS)**
SSIS Execute Package Task doesn't support debugging when ExecuteOutOfProcess is set to True. This issue only applies to debugging. Save, deploy, and execution via DTExec.exe or SSIS catalog isn't impacted.

## 17.3,&nbsp; SSDT for VS 2015

_Build number:_ &nbsp; 14.0.61712.050  
_SSDT for Visual Studio 2015._

### What's new?

**Analysis Services (AS) projects**
- Added three new options to tabular projects (under Options > Analysis Services Tabular > Data Import):
  - Enable Legacy data sources - allows the user to create older "1200 compatibility mode" data sources in newer compatibility modes.
  - Automatic type detection - when enabled the Query Editor for modern data sources will attempt to detect data types for unstructured queries when they're loaded. If the detection is successful, a new step may be added to the query.
  - Run background analysis - when enabled the Query Editor for modern data sources will run queries against the data source as the queries are loaded in order to analyze the query's output schema.

**Integration Services (IS)**
- Added package validation step in Deployment Wizard when deploying to Azure SSIS IR in ADF, which discovers potential compatibility issues in SSIS packages to execute in Azure SSIS IR. For more info, see [Validate SSIS packages deployed to Azure](../integration-services/lift-shift/ssis-azure-validate-packages.md).

### Bug fixes

**Analysis Services (AS) projects:**
- Fixed an issue that could cause an unhandled exception when checking in model changes to TFS.
- Fixed an issue that could cause an exception when adding table with complex M expression to a 1400 model.
- Fixed an issue that could cause a crash in Visual Studio when searching metadata in the model diagram view.
- Fixed an issue with 1400 models that could cause calculated columns to get removed from the table definition when saving changes to partition M queries.
- Fixed an issue when using Rename Query on 1400 models in the Get Data\Table Editor UI that could freeze while validating compatibility with current data model.
- Fixed an issue that caused a missing Newtonsoft assembly reference when deploying 1400 model to Azure Analysis Service.
- Fixed an issue that caused an error importing data through PQ into a 1400 model in certain cases.
- Fixed a scaling issue in the PowerQuery user interface dialogs that would appear when Windows scaling set.
- Fixed an issue with renaming roles.
- Fixed issues with the Project Configurations that may have caused changes to not save\sync properly in some cases.
- Fixed an issue in the PowerQuery editor that was adding "Change Type" steps automatically.
- Fixed an issue that caused an error opening the BIM file after switching to\from Integrated Workspace mode.
- MaxConnections property is now visible for data sources in tabular models.
- Increased the initial size of the PowerQuery editor window.
- M Query keywords such as "Source" in the PowerQuery editor will now show as localized.
- Cache credentials when working with 1400 models and structured data sources to prevent having to enter the same credentials for each table edited.

**RS Projects:**
- Fixed an issue that prevented deploying a single report in a multi report project
- Fixed an issue with Shared Data Sources that may have caused an issue on deployment
- Fixed an issue that could crash in the Undo manager when switching between code view, design view, and query editor window
- Fixed an issue that may have caused the parameter pane to disappear after runtime error
- Fixed an issue with Report Projects that may have caused them to lose source control mappings

**Integration Services:**
- Fixed an issue that may have occurred when switching a connection on an Analysis Services Process Task
- Fixed an issue where some tasks/components aren't localized well.
- Fixed an issue where CDC components break after applying a SQL fix for CDC that adds \__$command\_id column.

## 15.4.0 (preview),&nbsp; SSDT for VS 2017

_Build number:_ &nbsp; 14.0.16134.0  
_SSDT for Visual Studio 2017._
  
### What's new?

This release provides a standalone web installer for SQL Server Database, Analysis Services, Reporting Services, and Integration Services projects in Visual Studio 2017 15.4 or later.

### Installer

- Allow user to set nickname when installing a new SSDT for VS2017 instance.
- Hide feature selection checkboxes of installer if no VS instance is selected.
- Refine some messages of installer based on customer feedback.
- Fix an issue that installer doesn't support upgrade.

### SSIS

- Fix an issue that Import/Export Wizard can't list data source when Azure feature pack is installed.
- Fix an issue that editing an SSIS Analysis Services Process Task throws an exception while switching connection.
- Fix an issue that CDC components breaks after applying SQL fix that adds __$command_id column.
- Fix an issue that third party package can't be edited and executed when targeting old SQL Server.
- Fix an issue that Flat File Source configure dialog doesn't show correctly when double-click DTSWizard.exe and select Flat File Source.
- Fix an issue that a package containing Azure Feature Pack task/component can't execute when targeting SQL Server 2017.

**Known Issues**

- The installer isn't localized.
- SSIS Execute Package Task doesn't support debugging when *ExecuteOutOfProcess* is set to True. This issue only applies to debugging. Save, deploy, and execution via DTExec.exe or SSIS catalog isn't impacted.

## 17.30,&nbsp; SSDT for VS 2015

_Build number:_ &nbsp; 14.0.61709.290  
_SSDT for Visual Studio 2015._

### What's new?

**Analysis Services (AS)**

- Cosmos DB and HDI Spark are enabled in 1400 models.
- Tabular data source properties.
- "Blank Query" is now a supported option for creating a new Query in the Query Editor for models at the 1400 compatibility level.
- The Query Editor for 1400-mode models now allows for saving queries without new tables automatically being processed.

**Reporting Services (RS)**

- Projects now prompt on open to upgraded format to support using MSBuild to build and deploy.

### Known Issues

**Analysis Services (AS)**

- Models of 1400 compatibility level in Direct Query mode that have perspectives fail on querying or discovering metadata.

**Reporting Services (RS)**

- New Report Project format doesn't retain source control binding, and raises an error similar to the  message:

   *The project file C:\path isn't bound to source control, but the solution contains source control binding information in it.*

   To work around this issue, select **Use solution binding**  every time the solution is opened.

- After upgrading your project to the new MSBuild format, save may fail with a message similar to the following:

   *"Parameter "unevaluatedValue" can't be null."*

   To work around this issue, please update your *Project Configurations* and populate the *Platform* property.

### Bug Fixes

**Analysis Services (AS)**

- Vastly improved performance when loading tabular model diagram view.
- Fixed a number of issues to improve PowerQuery integration and experience in 1400-compat level models.
    - Fixed an issue that prevented editing permissions for File sources.
    - Fixed an issue Can't change the source for File sources.
    - Fixed an issue Wrong UI displayed for File sources.
- Fixed an issue that caused the "JoinOnDate" property to be removed when a "Join on Date" relationship was made inactive.
- New Query option in Query Builder now allows creating a new blank query.
- Fixed an issue that caused edits to an existing data source query to not update the model definition of the table in 1400-compat level.
- Fixed issues with custom context expressions that may have caused exceptions.
- When importing new table with duplicate name in 1400 tabular models, user is now notified that there was a name conflict and the name adjusted to be unique.
- Current User impersonation mode has been removed from models in Import mode, as it isn't a supported scenario.
- PowerQuery integration now supports Options for Additional Data Sources (OData.Feed, Odbc.DataSource, Access.Database, SapBusinessWarehouse.Cubes).
- PowerQuery Options strings for Data Sources will now correctly show localized text based on client locale.
- Diagram view now shows newly created columns from M Query Editor in 1400-compat level models.
- Power Query Editor now gives the option to not import data.
- Fixed an issue with installing a data cartridge used to import tables from Oracle in multi-dimensional models in VS2017.
- Fixed an issue that may have led to a crash when mouse cursor leaving the tabular formula bar in rare cases.
- Fixed an issue in Edit Table Properties dialog where changing the table name incorrectly changed source table name causing an unexpected error.
- Fixed a crash that could occur in VS2017 when trying to invoke Test Cube Security in the Roles designer Cell Data tab designer in multi-dimensional projects.
- SSDT: Properties are uneditable for tabular data sources.
- Fixed an issue that may have caused MSBuild and DevEnv builds to not work correctly in some cases with solution files.
- Vastly improved performance when committing model changes (DAX edits for measures, calculated columns) when tabular model contains larger metadata
- Fixed a number of issues with importing data using PowerQuery in 1400-compat level models
    - Import takes a long time after clicking Import and UI shows no status
    - A large list of tables on Navigator views when trying to select tables to import very slow
    - Query Editor poor performance working with list of 35 queries in Query editor view (issue in PBI desktop too)
    - Importing multiple tables disabled toolbar and may never finish in certain situations
    - Model designer appeared disabled and showed no data after import of table using PQ
    - Unselecting "Create new Table" in PQ UI still resulted in a new table being created
    - Folder data source not prompting for credentials
    - Object reference not set exception that may occur trying to get updated credentials on structured data source
    - Opening partition manager with M-expression was very slow
    - Selecting Properties on table in PQ editor didn't show the properties
    - Improved robustness in Power Query UI integration to catch top-level exceptions and show in Output window
- Fixed an issue with ChangeSource on structure datasource not persisting changes when context expression
- Fixed an issue where M expression errors may cause failures to update the model without error message shown
- Fixed an issue closing SSDT with error "The build must be stopped before the solution can be closed"
- Fixed an issue where VS may appear to stop responding when setting wrong impersonation mode in 1400 compat-level model 
- Detail rows property will now only be serialized to JSON when it isn't empty (changed from default)
- Oracle OLEDB driver now available in the list for tabular Direct Query mode
- Adding M-Expressions in 1400-compat tabular models now appear\refresh in the Tabular Model Explorer (TME)
- Fixed an issue that caused MSOLAP provider to not show up in VS2017 when trying to import using "Other" datasource in pre-1400 compat level models
- Fixed an issue where adding a translation through TME may cause issues 
- Fixed an issue in the Object Level Security interface that caused the tab to appear\hide incorrectly in certain cases
- Fixed an issue where failure could occur attempting to open previously loaded multi-dimensional model using Connect to Database dialog
- Fixed an issue that caused an error when adding custom assemblies to a multi-dimensional model

**Reporting Services (RS)**

- Fixed an issue with compile and build of RDLC in VS 2017

## 15.3.0 (preview),&nbsp; SSDT for VS 2017

_Build number:_ &nbsp; 14.0.16121.0  
_SSDT for Visual Studio 2017._
  
### What's new?

This preview is the first version of SSDT for Visual Studio 2017. This release introduces a standalone web installation experience for SQL Server Database, Analysis Services, Reporting Services, and Integration Services projects in Visual Studio 2017 15.3 or later.

**Known Issues**

- The installer isn't localized.
- SSIS isn't localized.
- SSIS Execute Package Task doesn't support debugging when *ExecuteOutofProcess* is set to *True*. This issue only applies to debugging. Save, deploy, and execution via DTExec.exe or SSIS catalog isn't impacted.
- SSIS Packages containing third-party extensions can't be switched to target other server versions.


## 17.2,&nbsp; SSDT for VS 2015

_Build number:_ &nbsp; 14.0.61707.300  
_SSDT for Visual Studio 2015._

### What's new?

**AS projects:**
- Object Level Security can now be configured in the *Roles* dialog for advanced security in 1400 compatibility level tabular models.
- New AAD role member selection for users without email addresses in AS Azure models in SSDT AS projects for VS2017.
- New AS Azure "Always Prompt" project property in SSDT AS tabular projects to customize behavior of ADAL credential caching.

### Bug Fixes

**General**
- Updated branding references for SQL Server 2017.

**AS projects**
- Significant performance fixes made to improve experience when committing DAX measure changes and other model edits.
- Fixed a number of issues with Power Query integration in Analysis Services projects using 1400-compatibility level tabular models.
- Fixed an issue in Multi-Dimensional projects in VS2017 only where Design Aggregation designer may fail to load.
- Fixed an issue when dragging an item in the Analysis Services multi-dimensional DSV diagram that could crash VS 2017.
- Fixed an issue in AS projects where the Deploy Dialog wasn't always in the foreground on top of Visual Studio.
- Removed Analysis Services import from Data Marketplace as data source since the service has been decommissioned.
- Fixed an issue that left the table designer disabled after Import New Table from existing data source through Tabular Model Explorer.
- Fixed an issue that may cause Model menu items Import from Datasource/Add Datasource to remain hidden in the wrong context.
- Improved experience when creating a measure from the Tabular Model Explorer to avoid switching focus back to the column used to create a measure.
- When switching from integrated workspace in AS tabular projects to explicit workspace server, the old database files are now be cleaned up.
- Fixed an issue in AS tabular 1400 models projects where the Row Level Security checkbox UI state initially showed as unchecked regardless of actual underlying object state.
- Fixed a crash that could occur when importing text file or Excel file into 1400-compat mode tabular model using Power Query and unhandled exception thrown.
- Fixed an issue that could occur with the scrollbar thumb in the DAX formula editing control in AS tabular model designer.
- Fixed an issue that prevented modifying a PowerQuery mashup data source when it contained a username/password authentication.
- Fixed an issue that could prevent a data source to connect when additional properties set in connection string.
- Fixed an issue that could crash VS when multiple AS tabular model projects loaded and closing the second model designer without interacting with anything in the designer first.
- Fixed an issue where edits made to KPI formatting weren't getting persisted in some cases.
- Fixed an issue with PowerQuery UI that showed the wrong menu checked state for whether the formula bar was shown.
- Fixed an issue in AS Tabular 1400-compat level projects with PowerQuery data sources that could crash VS when selecting Change Data Source menu from Tabular Model Explorer.
- Fixed an intermittent issue where loading a 1400 tabular model may show the error *'Couldn't load file or assembly 'Microsoft.ProBI.MashupLibrary'*.

**RS projects**
- User preferences for RS Ruler and Parameter box settings selection state is remembered correctly across sessions.

**IS projects**
- Fixed an issue where ADO/ADO.NET ForEachLoop Container didn't show correctly
- Fixed an issue where some tasks/components/wizards aren't localized
- Changed latest *TargetServerVersion* from "SQL Server vNext" to "SQL Server 2017"

## 17.10,&nbsp; SSDT for VS 2015

_Build number:_ &nbsp; 14.0.61705.170  
_SSDT for Visual Studio 2015._

### What's new?
**AS projects:**
- Users can set encoding hints on columns in the UI on 1400 models
- Non-model-related IntelliSense is now available in offline mode
- Tabular Model Explorer now contains a node to represent named M expressions available across the model (1400 compatibility level tabular models)
- Azure Active Directory People Picker, similar to Microsoft Azure portal's IAM, now available when setting up Role Members in Tabular Models

**Database projects:**
- Updated to DacFx 17.1

### Bug Fixes
- Fixed an issue where the Business Intelligence Designers group name was displayed incorrectly in Visual Studio Options in VS2017
- Fixed an issue where a crash could occur generating a Code Map for a solution with a Report Project or AS Project
- Fixed a number of issues with PowerQuery integration for Analysis Services 1400 compatibility level tabular models
- Fixed an issue in the new DAX editor tool window where the assignment operator couldn't be on a separate line when defining a measure
- Fixed an issue that prevented the tabular measure display from updating when renaming measures in perspective
- Updated Analysis Services integrated workspace engine and Tabular Object Model that fixes a regression that caused 1200 tabular projects containing translations to fail on deploy to SQL Server 2016 Analysis Services server
- Fixed a performance issue that made creation\deletion of new 1400 tabular data sources very slow
- Fixed an issue where the DSV diagram in multi-dimensional models could stop rendering if changing view quickly between different DSVs

## DacFx 17.1

- Fixed an issue when encrypting a column with memory-optimized tables with other identity columns
- SQLDOM support for CATALOG_COLLATION option for CREATE DATABASE

## DacFx 17.0.1

- Fix for issue with databases with an asymmetric key by an HSM with an EKM provider [Connect item](https://connect.microsoft.com/SQLServer/feedback/details/3132749/sqlpackage-exe-fails-when-extracting-a-database-which-contains-an-asymmetric-key-using-an-ekm-provider)

## 17.0,&nbsp; SSDT for VS 2015

_Build number:_ &nbsp; 14.0.61704.140  
_SSDT for Visual Studio 2015._  
_Supports up to SQL Server 2017._

### What's new?
**Database projects:**
- Amending a clustered index on a view will no longer block deployment
- Schema comparison strings relating to column encryption uses the proper name rather than the instance name.   
- Added a new command-line option to SqlPackage: ModelFilePath.  This provides an option for advanced users to specify an external model.xml file for import, publishing, and scripting operations   
- The DacFx API was extended to support  Azure AD Universal Authentication and Multi-factor authentication (MFA)

**IS projects:**
- The SSIS OData Source and OData Connection Manager now support connecting to the OData feeds of Microsoft Dynamics AX Online and Microsoft Dynamics CRM Online.
- SSIS project now supports target server version of "SQL Server 2017" 
- Support for CDC Control Task, CDC Splitter and CDC Source when targeting SQL Server 2017. 

**AS projects:**
- Analysis Services PowerQuery Integration (1400 compatibility level tabular models):
    - DirectQuery is available for SQL Oracle, And Teradata if user has installed third-Party drivers
    - Add columns by example in PowerQuery
    - Data access options in 1400 models (model-level properties used by M engine)
        - Enable fast combine (default is false - when set to true, the mashup engine ignores data source privacy levels when combining data)
        - Enable Legacy Redirects (default is false - when set to true, the mashup engine follows HTTP redirects that are potentially insecure.  For example, a redirect from an HTTPS to an HTTP URI)  
        - Return Error Values as Null (default is false - when set to true, cell level errors are returned as null. When false, an exception is raised when a cell contains an error)  
    - Additional data sources (file data sources) using PowerQuery
        - Excel 
		- Text/CSV 
		- Xml 
		- Json 
		- Folder 
		- Access Database 
		- Azure Blob Storage 
    - Localized PowerQuery user interface
- DAX Editor Tool Window
    - Improved DAX editing experience for measures, calculated columns, and detail-rows expressions, available via the View, Other Windows menu in SSDT
	- Improvements to DAX parser\Intellisense


**RS projects:**
- Embeddable RVC Control is now available supporting SSRS 2016

### Bug fixes
**AS projects:**
- Fixed the template priority for BI Projects so they don't show up at the top of the New Projects categories in VS
- Fixed a VS crash that may occur in rare circumstances when SSIS, SSAS, or SSRS solution opened
- Tabular: A variety of enhancements and performance fixes for DAX parsing and the formula bar.
- Tabular: Tabular Model Explorer will no longer be visible if no SSAS Tabular projects are open.
- Multi-dimensional: Fixed an issue where the processing dialog was unusable on High-DPI machines.
- Tabular: Fixed an issue where SSDT faults when opening any BI project when SSMS is already open. [Connect Item](https://connect.microsoft.com/SQLServer/feedback/details/3100900/ssdt-faults-when-opening-any-bi-project-when-ssms-is-already-open)
- Tabular: Fixed an issue where hierarchies weren't being properly saved to the bim file in a 1103 model.[Connect Item](https://connect.microsoft.com/SQLServer/feedback/details/3105222/vs-2015-ssdt)
- Tabular: Fixed an issue where Integrated Workspace mode was allowed on 32-bit machines even though it isn't supported.
- Tabular: Fixed an issue where clicking on anything while in semi-select mode (typing a DAX expression but clicking a measure, for example) could cause crashes.
- Tabular: Fixed an issue where Deployment Wizard would reset the model's.Name property back to "Model". [Connect Item](https://connect.microsoft.com/SQLServer/feedback/details/3107018/ssas-deployment-wizard-resets-modelname-to-model)
- Tabular: Fixed an issue where selecting a hierarchy in TME should display properties even if Diagram View isn't selected.
- Tabular: Fixed an issue where pasting into the DAX Formula bar would paste images or other content instead of text when pasting from certain applications.
- Tabular: Fixed an issue where some old models in the 1103 couldn't be opened due to presence of measures with a specific definition.
- Tabular: Fixed an issue where XEvent Sessions couldn't be deleted.
- Fixed an issue with attempting to build AS "smproj" files with devenv.com would fail
- Fixed an issue that was finalizing text changes too frequently when using the Korean IME in AS tabular model sheet tab titles
- Fixed an issue where the intellisense for DAX Related() function wasn't working correctly to show columns from other tables
- Improved AS Tabular project import from database dialog by sorting the list of AS databases
- Fixed an issue when creating calculated tables in AS tabular model where Tables weren't listed as suggested objects in the expression
- Fixed an issue in preview 1400 AS models trying to open using Integrated Workspace server after viewing code
- Fixed an issue that was preventing some data sources (with no support for initial catalog) from working correctly in certain circumstances 
- Deployment Wizard should apply changes to calculated table partitions even when the option to keep partitions is enabled
- Fixed an issue where Advanced Properties dialog to existing AS Connection didn't show full list until reselected
- Fixed a few issues with clipped UI strings that appeared in some localized builds
- Fixed a number of issues with PowerQuery integration in 1400 compatibility level AS tabular models
- Fixed an issue with Report Wizard style templates not showing up correctly
- Fixed an issue with the Report Wizard that could lead to incorrect data source settings when changing from SQL to AS
- Fixed an issue causing Analysis Services (Tabular) project build failure from command line (devenv.com\exe)
- Fixed an issue with the DAX measure parser to show highlighted and correct text color when starting with letters before :=
- Fixed an issue triggering an ObjectRefException if paths got too long attempting to Show All Files for Tabular project in integrated workspace mode
- Fixed an issue with the Data Source Designer for Compact 4.0 Client Data Provider where it appeared unavailable
- Fixed an issue that caused an error trying to browse AS mining model in VS2017
- Fixed an issue in AS multi-dimensional model in VS2017 where DSV diagram stops rendering after changing views and then hits an exception
- Fixed an issue previewing reports with an AS connection failed in VS2017
 

**RS projects:**
- Fixed an issue when designing reports in SSDT the tree view of parameters, data sources, and datasets would collapse when most changes made 
- Fixed an issue where Save should save the version of RDL, not the latest version.
- Fixed an issue where SSDT RS is backing up files when backup is turned off and several other issues.
- Fixed an issue in Report Builder where an error would be shown when clicking "Split Cells". [Connect Item](https://connect.microsoft.com/SQLServer/feedback/details/3101818/ssdt-2015-ssrs-designer-error-by-matrix-cell-split)
- Fixed an issue where caching could cause incorrect data in a report. [Connect Item](https://connect.microsoft.com/SQLServer/feedback/details/3102158/ssdtbi-14-0-60812-report-preview-data-is-frequently-wrong-due-to-bad-caching)

**IS projects:**
- Fixed an issue that run64bitruntime setting doesn't stick.
- Fixed an issue that DataViewer doesn't save displayed columns.
- Fixed an issue that Package Parts hides annotations. [Connect Item](https://connect.microsoft.com/SQLServer/feedback/details/3106624/package-parts-hide-annotations)
- Fixed an issue that Package Parts discards Data Flow layouts and annotations. [Connect Item](https://connect.microsoft.com/SQLServer/feedback/details/3109241/package-parts-discard-data-flow-layouts-and-annotations)
- Fixed an issue that SSDT crashes when importing project from sql server.
- Fixed an issue with Hadoop File System Task TimeoutInMinutes default to 10 after opening saved SSIS package and at runtime.

**Database projects:**
- SSDT DACPAC deploys add setting back in for IgnoreColumnOrder [Connect item](https://connect.microsoft.com/SQLServer/feedback/details/1221587/ssdt-dacpac-deploy-add-setting-back-in-for-ignorecolumnorder)
- SSDT failing to compile if STRING_SPLIT is used [Connect item](https://connect.microsoft.com/SQLServer/feedback/details/2906200/ssdt-failing-to-compile-if-string-split-is-used)
- Fix issue where DeploymentContributors have access to the public model but the backing schema hasn't been initialized [GitHub issue](https://github.com/Microsoft/DACExtensions/issues/8)
- DacFx temporal fix for FILEGROUP placement
- Fix for "Unresolved Reference" error for external synonyms. 
- Always Encrypted: Online encryption doesn't disable change tracking on cancellation and doesn't work properly if change tracking hasn't been cleaned prior to start encryption

## 16.5,&nbsp; SSDT for VS 2015

_Released:_ &nbsp; October 20, 2016  
_Build number:_ &nbsp; 14.0.61021.0  
_SSDT for Visual Studio 2015._  
_Supports up to SQL Server 2016._

**What's new?**

### Connection Improvements

* The new search box in the **Browse** tab helps you filter your Local servers, Network servers, and Azure SQL databases. This is useful if you have a large number of servers or databases appearing in these lists.
* The **History** tab has right-click menu options to pin / unpin favorites, and a new option to remove connections from history.

### SqlPackage and DacFx API Improvements

Using SqlPackage.exe and the DacFx APIs you can now generate a deployment report, deployment script, and publish to a database all in one action. This is a timesaver for anyone who likes to keep a report of what was published during a deployment. Another benefit is that for Azure scenarios, separate scripts for the master database and the deploy target database are created. Up to now a single script was created which wasn't useful for repeated deployments.

For SqlPackage's Publish and Script actions, two new arguments have been added.

* DeployScriptPath (shortname: dsp). This is an optional path to write the deployment script to. For Azure deployment, if there were TSQL commands to create of modify the DB a master script will be written to the same path but with "Filename_Master.sql" as the output file name.
* DeployReportPath (shortname: drp). This is an optional path to write the deployment report to.

For the Script action, either the existing Output Path arguments or the new script/report-specific arguments should be used, but not both.

Sample usage:

**Publish Action**

```Sqlpackage.exe /a:Publish /tsn:(localdb)\ProjectsV13 /tdn:MyDatabase /deployscriptpath:"My\DeployScript.sql" /deployreportpath:"My\DeployReport.xml"```

**Script Action**

```Sqlpackage.exe /a:Script /tsn:(localdb)\ProjectsV13 /tdn:MyDatabase /deployscriptpath:"My\DeployScript.sql" /deployreportpath:"My\DeployReport.xml"```

In DacFx, two new APIs have been added: DacServices.Publish() and DacServices.Script(). These also support performing publish + script + report actions in a single operation. Sample usage:

```
DacServices service = new DacServices(connectionString);
using(DacPackage package = DacPackage.Load(@"C:\My\db.dacpac")) {
var options = new PublishOptions() {
    GenerateDeploymentScript = true, // Should a deployment script be created?
    GenerateDeploymentReport = true, // Should an xml deploy report be created?
    DatabaseScriptPath = @"C:\My\OutputScript.sql", // optional path to save script to
    MasterDbScriptPath = @"C:\My\OutputScript_Master.sql", // optional path to save master script to
    DeployOptions = new DacDeployOptions()
};

// Call publish and receive deployment script & report in the results
PublishResult result = service.Publish(package, "TargetDb", options);
Console.WriteLine(result.DatabaseScript);
Console.WriteLine(result.MasterDbScript);
Console.WriteLine(result.DeploymentReport);

// Call script and receive deployment script & report in results
result = service.Script(package, "TargetDb", options);
Console.WriteLine(result.DatabaseScript);
Console.WriteLine(result.MasterDbScript);
Console.WriteLine(result.DeploymentReport);
```

**Analysis Services & Reporting Services**

SSAS tabular designer DAX parser has improved performance when working with large DAX expressions.
For more information, please read the [Analysis Services blog post](/archive/blogs/analysisservices/introducing-integrated-workspace-mode-for-sql-server-data-tools-for-analysis-services-tabular-projects-ssdt-tabular).

### Fixed / Improved this month

**Database Tools**

* [Connect bug 3055711](https://connect.microsoft.com/SQLServer/feedback/details/3055711/columns-can't-be-selected-from-cross-apply-openjson-with-explicit-schema) - Columns can't be selected from CROSS APPLY OPENJSON with explicit schema
* Fixed - issue with Autogenerated History table indexes, where DacFx dropped index on redeployment
* Fixed - issue with DacFx batch parser not parsing escaped bracket ']' characters, which caused publish to fail
* Improved - SqlPackage now includes descriptions for each action in the help output
* Fixed - The "Remember Password" option in the connection dialog wasn't being preserved when editing Advanced options and when editing a connection string saved in Publish, Schema Compare and other files
* Fixed - For connections show in the History tab with IntegratedAuthentication=true, the Authentication field in connection properties was left blank. This now shows "Windows Authentication" as expected
* Fixed - Changes to the SQL Server Tools Intellisense settings under Tools -> Options -> Text Editor weren't being preserved
* Improved - the Pin/Unpin button in the connection dialog History tab is now more compact, reducing the likelihood of a scrollbar appearing
* Fixed - several accessibility issues in the connection dialog were fixed.

**Analysis Services & Reporting Services**

* Fixed an issue in SSDT AS tabular designer where clicking the scrollbar thumb in data grid crashed in certain situations
* Fixed an issue where option to impersonate connection as current user in SSDT AS tabular wasn't available
* Fixed an issue in SSDT AS tabular designer where expanding the formula bar too far could make the project unable to reopen
* Fixed a crash in SSDT AS tabular designer that would occur on key down if table tab was selected
* Fixed an issue in SSDT AS projects where Analyze in Excel wouldn't connect to down-level AS server versions

**Integration Service**

* Fixed Connect bug [1608896](https://connect.microsoft.com/SQLServer/feedback/details/1608896/move-multiple-integration-service-package-tasks): Move Multiple Integration Service Package Tasks

## 16.4, SSDT for VS 2015

_Released:_ &nbsp; September 20, 2016  
_Build number:_ &nbsp; 14.0.60918  
_For SQL Server 2016._

**What's new?**

Schema Compare is now supported in SqlPackage.exe and the Data-Tier Application Framework (DacFx) API. For details, see [Schema Compare in SqlPackage and the Data-Tier Application Framework](/archive/blogs/ssdt/schema-compare-in-sqlpackage-and-the-data-tier-application-framework-dacfx).

**Analysis Services - Integrated Workspace Mode for SSDT Tabular (SSAS)**

SSDT Tabular now includes an internal SSAS instance, which SSDT Tabular starts automatically in the background if integrated workspace mode is enabled so that you can add and view tables, columns, and data in the model designer without having to provide an external workspace server instance. Integrated workspace mode doesn't change how SSDT Tabular works with a workspace server and database. What changes is where SSDT Tabular hosts the workspace database. To enable integrated workspace mode, select the Integrated Workspace option in the Tabular Model Designer dialog box displayed when creating a new tabular project. For existing tabular projects that currently use an explicit workspace server, you can switch to integrated workspace mode by setting the Integrated Workspace Mode parameter to True in the Properties window, which is displayed when you select the Model.bim file in Solution Explorer. For details, see the [Analysis Services blog post](/archive/blogs/analysisservices/introducing-integrated-workspace-mode-for-sql-server-data-tools-for-analysis-services-tabular-projects-ssdt-tabular).

**Updates and fixes**
**Database tools:**

- [Connect Issue 3087775](https://connect.microsoft.com/SQLServer/feedback/details/3087775): Temporal tables broken in VS Data Tools July update 14.0.60629.0, "Value can't be null. Parameter name: reportedElement"
- [Connect Issue 1026648](https://connect.microsoft.com/SQLServer/feedback/details/1026648): IsPersistedNullable shows as different in SSDT Comparison
- [Connect Issue 2054735](https://connect.microsoft.com/SQLServer/feedback/details/2054735): Identity is reset when importing a BACPAC
- [Connect Issue 2900167](https://connect.microsoft.com/SQLServer/feedback/details/2900167): Running SSDT unit tests leaves temp files behind
- [Connect Issue 1807712](https://connect.microsoft.com/SQLServer/feedback/details/1807712): Backwards compatibility breakage - AppLocal and Nugetization

**Analysis Services & Reporting Services**

* Fixed an issue in SSDT where error tip pop-ups were in the way when editing DAX for DirectQuery calculated columns.
* Fixed an issue in SSDT AS tabular grid where the KPI icon wasn't showing in measure grid when Windows scaling factor set at high-DPI 200%+.
* Fixed an issue in SSDT AS where pasting large table data could make the tabular project unresponsive.
* Fixed an issue in SSDT AS tabular model editor to mark the model as needing to save changes when renaming connection-friendly name.
* Fixed an issue in the SSDT AS tabular projects where width of columns in the manage relationships dialog couldn't be resized.
* Fixed an issue in SSDT AS tabular 1200-level models where pasting data from Excel with locale settings like German didn't treat the comma as the decimal separator correctly.
* Fixed an issue in SSDT AS projects with some KPI icon sets, which could yield an error "Couldn't retrieve the data for this visual".
* Fixed an issue with SSDT AS project properties dialog to anchored correctly when resized at High-DPI scaling.
* Fixed an issue in SSDT AS projects that may have caused an error upgrading certain models with Pasted tables.
* Fixed an issue in SSDT AS where pasting full sheet rows from Excel was slow and created many unwanted columns.
* Fixed an issue in SSDT AS where large static DataTable expressions parsing and highlight were slow or appeared to stop responding.
* Fixed an issue in SSDT AS to add measures and KPI values to the current perspective selected in the editor.
* Fixed an issue in SSDT where data import into AS project from SQL Azure didn't support schema types other than "dbo".

## 16.3,&nbsp; SSDT for VS 2015

_Released:_ &nbsp; August 15, 2016  
_Build number:_ &nbsp; 14.0.60812.0  
_For SQL Server 2016._

**What's new?**

- **Release Versioning & Numbering:** Releases are now tagged numerically rather than by month. This aligns with the new SSMS policy and simplifies cases where we have multiple releases or hotfixes in a month. This release is 16.3, which means the third update after the RTM release. Any hotfix will be 16.3.1, and so on, with our next update (planned for next month) being 16.4.
- **Analysis Services - Tabular Model Explorer:** Tabular Model Explorer lets you conveniently navigate through the various metadata objects in a model, such as data sources, tables, measures, and relationships. It's implemented as a separate tools window that you can display by opening the View menu in Visual Studio, pointing to Other Windows, and then clicking Tabular Model Explorer. The Tabular Model Explorer appears by default in the Solution Explorer area on a separate tab. Tabular Model Explorer organizes the metadata objects in a tree structure that closely resembles the schema of a tabular 1200 model and many more new features.
- **Database Tools - Always Encrypted**:  This release provides new [Always Encrypted Key management](../relational-databases/security/encryption/overview-of-key-management-for-always-encrypted.md) dialogs to easily add Column Master Keys or Column Encryption Keys to your database project, or a live database in SQL Server Object Explorer. This release supports certificates in Windows Certificate Store. In upcoming releases, Azure Key Vault and CNG Providers will be supported.
    - While creating Column Master Key or Column Encryption Key, you may experience that the changes aren't reflected on SQL Server Object Explorer immediately after clicking Update Database. To work around, refresh the database node in SQL Server Object Explorer.
    - If you try to encrypt a column in a table with data from SQL Server Object Explorer, you may experience a failure. This feature is currently supported only in SSDT database projects and SSMS. Support for SQL Server Object Explorer will be enabled in a later release.

**Updates and fixes**
* **Database tools:**
    - **SSDT:**
        - Connect bug 1898001 [Fixed a column description issue with 128 character limitation](https://connect.microsoft.com/SQLServer/feedback/details/1898001/column-description-limited-to-128-characters).
        - Fixed an issue where publishing a database from VS didn't apply DatabaseServiceObjective property in the publish profile xml.
        - Connect bug 2900167 [Fixed a unit test issue for incorrectly leaving temp files](https://connect.microsoft.com/SQLServer/feedback/details/2900167/running-ssdt-unit-tests-leaves-temp-files-behind).
        - Fixed an issue where Retention Period combo box on Database Settings is truncated.
        - Fixed an issue for the missing verification of empty old password on SQL CLR project properties while changing the password.
    - **DACFx:**
        - Fixed an issue where DACFx compatibility level isn't updated for SqlAzureV12 error.
        - Fixed an issue where IsAutoGeneratedHistoryTable property is incorrectly excluded from model comparison.

- **Analysis Services & Reporting Services**
    - **SSDT:**
        - Fixed an issue that Tabular model can't be saved when the server connection is lost.
        - Fixed an issue that SSDT becomes unresponsive due to a possible infinite loop issue in AS.
        - Fixed a DAX expression issue that caused inconsistent behaviors based on how you commit the expression.
        - Fixed a VS crash issue when creating KPIs.
        - Fixed an issue that generated invalid reports for SQL Server 2008 R2, 2012 and 2014.
        - Fixed a hierarchy order issue that caused an infinite loop error for.dwpro project.
        - Fixed an RS RDL issue where downgrading RDL required a full rebuild, which caused user's confusion.
        - Fixed a KPI issue where Hide From Client Tools had no effect.

## July 2016,&nbsp; SSDT for VS 2015

_Released:_ &nbsp; June 30, 2016  
_Build number:_ &nbsp; 14.0.60629.0  
_For SQL Server 2016._

**What's new?**  
- **Always Encrypted support:** For Databases that contain Always Encrypted columns, this release adds full support for Always Encrypted through our core APIs and command-line tool (SqlPackage.exe). You can build and publish database projects with full support for all Always Encrypted features.  
- **Temporal Tables enhanced support:** Simplified the experience by unlinking temporal tables before alterations and relinking once these have completed. This means that Temporal Tables have parity with other table types (standard, in-memory) in terms of the operations that are supported. 
- **SqlPackage.exe and installation changes:** Changes to isolate SSDT from SQL Server engine and SSMS updates. For details, see [Changes to SSDT and SqlPackage.exe installation and updates](/archive/blogs/ssdt/changes-to-ssdt-and-sqlpackage-exe-installation-and-updates).


**Updates and fixes**
* **Database tools:**
    * From now on SSDT will never disable Transparent Data Encryption (TDE) on a database. Previously since the default encryption option in a project's database settings was disabled, it would turn off encryption. With this fix encryption can be enabled but never disabled during publish. 
    * Increased the retry count and resiliency for Azure SQL Database connections during initial connection.
    * If the default filegroup isn't PRIMARY, Import/Publish to Azure V12 would fail. Now this setting is ignored when publishing.
    * Fixed an issue where when exporting a database with an object with Quoted Identifier on, export validation could fail in some instances.
    * Fixed an issue where the TEXTIMAGE_ON option was incorrectly added for Hekaton table creations where it isn't allowed.
    * Fixed an issue where Export took a long time exporting with large amount of data due to a write to the model.xml file after data phase completed caused contents of the .bacpac file to be rewritten.
    * Fixed an issue where Users weren't appearing in the Security folder for Azure SQL DW and APS connections.

* **Analysis Services & Reporting Services:**
    * Fixed a SxS issue with MSOLAP OLEDB provider where only the 32-bit provider was getting installed, impacting 64-bit Excel 2016 connecting to SQL Server 2014 (didn't repro with ClickOnce installs from Office365, only MSI Excel install).
    * Fixed an issue for a corner case to be more robust when upgrading AS model with pasted tables from 1103 to 1200 compatibility level that could give error "Relationship uses an invalid column ID".
    * Fixed a SxS issue when SSDT-BI 2013 on same machine, could no longer import data in AS model after uninstalling SSDT 2015 (cartridges shared registry setting).
    * Improved robustness to address issues\crashes when the connection to the AS engine is lost (i.e. SSDT left open overnight and AS server recycled, or other cases where the connection is temporarily lost). 
    * Fixed issues with dialogs opening on different screens than VS in multi-monitor scenarios. 
    * Fixed/enabled support for pasting from HTML tables (grid data) in AS model pasted tables. 
    * Fixed issue where upgrade failed to upgrade an empty pasted table to 1200 (used only as container table for measures). 
    * Fixed an issue with upgrading AS tabular model with pasted tables to 1200 to work around an AS engine issue with CalcTables (which are used for Pasted Tables in 1200), to perform a process full on the new calc tables after the upgrade. 
    * Fixed an issue where canceling creation of new AS 1200 model calculated table with incomplete DAX expression could crash. 
    * Fixed an issue importing 1200 model from AS server into SSDT AS project when DB name and a table name were the same. 
    * Fixed an issue with editing KPI measure in 1103 tabular model. 
    * Fixed an Object reference not set exception hit while pasting a KPI measure in the grid for an AS 1200 model. 
    * Fixed an issue where a column in a calculated table couldn't be deleted from the diagram view in 1200 models. 
    * Fixed an Object Reference not set exception when viewing the model.bim project file properties while in code view. 
    * Fixed an issue with pasting data into AS model grid to create pasted table yielded incorrect values on international locales using comma as decimal separator. 
    * Fixed an issue opening 2008 RS project in SSDT and choosing to not upgrade it. 
    * Fixed issue in 1200 compatibility level models calculated table UI when using default formatting for column type to allow changing the formatting type from the UI.

## June 2016,&nbsp; SSDT for VS 2015

_Released:_ &nbsp; June 1, 2016  
_Build number:_ &nbsp; 14.0.60525.0  
_For SQL Server 2016._

SSDT General Availability (GA) is now released. The SSDT GA update for June 2016 adds support for the latest updates of SQL Server 2016 RTM, and various bug fixes. For details, see [SQL Server Data Tools GA update for June 2016](/archive/blogs/ssdt/sql-server-data-tools-ga-update-for-june-2016).

## Additional Resources

- [Download SQL Server Data Tools &#40;SSDT&#41;](../ssdt/download-sql-server-data-tools-ssdt.md)
- [Previous releases of SQL Server Data Tools &#40;SSDT and SSDT-BI&#41;](../ssdt/previous-releases-of-sql-server-data-tools-ssdt-and-ssdt-bi.md)
- [What's New in Database Engine](../sql-server/what-s-new-in-sql-server-2016.md)
- [What's New in Analysis Services](/analysis-services/what-s-new-in-analysis-services)
- [What's New in Integration Services](../integration-services/what-s-new-in-integration-services-in-sql-server-2016.md)
