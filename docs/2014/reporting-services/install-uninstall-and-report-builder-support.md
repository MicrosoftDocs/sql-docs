---
title: "Install, Uninstall, and Report Builder Support | Microsoft Docs"
ms.custom: ""
ms.date: "12/29/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "administering Report Builder"
ms.assetid: 2c9a5814-17bf-4947-8fb3-6269e7caa416
author: maggiesmsft
ms.author: douglasl
manager: craigg
---
# Install, Uninstall, and Report Builder Support
  Report Builder is a report authoring tool that you use to create, update, and share reports, report parts and shared datasets. Report Builder is available in two versions: stand-alone and [!INCLUDE[ndptecclick](../includes/ndptecclick-md.md)]. The stand-alone version is installed on your computer by you or an administrator. The [!INCLUDE[ndptecclick](../includes/ndptecclick-md.md)] version is installed automatically with [!INCLUDE[ssRSCurrent](../includes/ssrscurrent-md.md)] and downloaded to your computer from Report Manager or a SharePoint site integrated with [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)].  
  
 The stand-alone version of Report Builder is not installed with [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)]. You must download and install it separately from the [Microsoft® SQL Server® 2012 Report Builder](https://go.microsoft.com/fwlink/?LinkId=401502).  
  
> [!NOTE]  
>  Report Builder cannot be installed on Itanium-based computers. This applies to the [!INCLUDE[ndptecclick](../includes/ndptecclick-md.md)] and stand-alone versions of Report Builder.  
  
 An administrator typically installs and configures [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)], grants permission to use the [!INCLUDE[ndptecclick](../includes/ndptecclick-md.md)] version of Report Builder, and manages folders and permissions to reports, report parts, and shared datasets saved to the report server. For more information about [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] administration, see [Reporting Services Report Server &#40;Native Mode&#41;](report-server/reporting-services-report-server-native-mode.md) in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [Books Online](https://go.microsoft.com/fwlink/?LinkId=154888) on msdn.microsoft.com.  
  
##  <a name="Installing"></a> Installing Report Builder  
 Report Builder is available as a stand-alone and [!INCLUDE[ndptecclick](../includes/ndptecclick-md.md)] versions. You or your administrator download and install the stand-alone version on your computer, whereas the [!INCLUDE[ndptecclick](../includes/ndptecclick-md.md)] version is installed with [!INCLUDE[ssRSCurrent](../includes/ssrscurrent-md.md)]. You can download Report Builder from the [Microsoft Download Center](https://go.microsoft.com/fwlink/?LinkID=186083).  
  
> [!NOTE]  
>  Report Builder cannot be installed on Itanium 64-based computers. This applies to the [!INCLUDE[ndptecclick](../includes/ndptecclick-md.md)] and stand-alone versions of Report Builder.  
  
 Before you install either version of Report Builder, verify system requirements and install any prerequisites.  
  
### System Requirements  
 Report Builder requires that the [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../includes/dnprdnshort-md.md)] version 3.5 is installed on the local computer. If the [!INCLUDE[dnprdnshort](../includes/dnprdnshort-md.md)] is not installed on the local computer when you install Report Builder, you will be prompted to install it before you can continue and complete the installation.  
  
 The .NET Framework 3.5 is free. You can download the .NET Framework 3.5 from the [Microsoft Download Center](https://go.microsoft.com/fwlink/?LinkID=110520).  
  
 You can install Report Builder on any [!INCLUDE[msCoName](../includes/msconame-md.md)] Windows operating system that supports the [!INCLUDE[dnprdnshort](../includes/dnprdnshort-md.md)] 3.5. For example, you can install Report Builder on Windows Vista or Windows 7.  
  
 It is recommended that the computers that will run Report Builder have 512 MB of RAM. However, depending on the complexity of reports you run, you might want less or more RAM.  
  
### Installing the Stand-alone Version of Report Builder Directly on Your Computer  
 You install Report Builder from the download site, [Microsoft Download Center](https://go.microsoft.com/fwlink/?LinkID=186083), or an administrator makes the ReportBuilder3.msi file, the Windows Installer Package for Report Builder, available on a share from which you can install it.  
  
 You can also perform a command line installation and include options such as making the installation silent and writing log files for the installation. The documentation for Windows Installer that runs .msi files provides information about the available options.  
  
 For more information, see [Install the Stand-Alone Version of Report Builder &#40;Report Builder&#41;](install-windows/install-report-builder.md).  
  
 An administrator can also use software such as Microsoft Systems Manager Server (SMS) to push the program to your computer. To learn how to use specific software to install Report Builder, consult the documentation for the software.   
  
### Installing the ClickOnce Version of Report Builder on Your Computer  
 The [!INCLUDE[ndptecclick](../includes/ndptecclick-md.md)] version of Report Builder is installed with [!INCLUDE[ssRSCurrent](../includes/ssrscurrent-md.md)]. It is installed by both native and SharePoint integrated installations of [!INCLUDE[ssRSCurrent](../includes/ssrscurrent-md.md)].  
  
 [!INCLUDE[ndptecclick](../includes/ndptecclick-md.md)] is a Microsoft technology for deploying Windows applications. [!INCLUDE[ndptecclick](../includes/ndptecclick-md.md)] enables users to install and run a Windows application such as Report Builder by clicking a link on a web page. For more information about deploying [!INCLUDE[ndptecclick](../includes/ndptecclick-md.md)] applications, applying [!INCLUDE[ndptecclick](../includes/ndptecclick-md.md)] application security, or running [!INCLUDE[ndptecclick](../includes/ndptecclick-md.md)] applications in the Internet zone, see the "ClickOnce Deployment for Windows Forms Applications", "Security in Windows Forms Overview", or "Trusted Application Deployment Overview" articles on the [!INCLUDE[msCoName](../includes/msconame-md.md)] Developer Network Web site at [www.microsoft.com/msdn](https://www.microsoft.com/msdn).  
  
 The [!INCLUDE[ndptecclick](../includes/ndptecclick-md.md)] version of Report Builder is located on the report server, and installs on your computer when you click the **Report Builder** button in Report Manager or click the **Report Builder Report** option on the **New Document** menu in a SharePoint library.  
  
> [!NOTE]  
>  If the **New Document** menu does not list the **Report Builder Report**, **Report Builder Model**, and **Report Data Source** options, their content types need to be added to the SharePoint library.   
  
 You can open Report Builder from Report Manager or a SharePoint library. For more information about opening Report Builder, see [Start Report Builder &#40;Report Builder&#41;](report-builder/start-report-builder.md).  
  
### Report Builder Languages  
 Report Builder is available in 21 languages in addition to English. When you download the stand-alone version of Report Builder, you select the language version that you want to install. You must repeat the download for each language version that you want to use.  
  
 For the [!INCLUDE[ndptecclick](../includes/ndptecclick-md.md)] version, all language versions are installed on the report server when you install [!INCLUDE[ssRSCurrent](../includes/ssrscurrent-md.md)]. The culture of the user's computer determines which language version is installed on the computer. If the culture does not match an available Report Builder language, the English version is installed.  
  
 The following table has information about the available language versions.  
  
|LCID|Language|Culture|  
|----------|--------------|-------------|  
|1028|Chinese (Traditional)|zh-TW|  
|1029|Czech|cs-CZ|  
|1030|Danish|da-DK|  
|1031|German|de-DE|  
|1032|Greek|el-GR|  
|1033|English|en-US|  
|1035|Finnish|fi-FI|  
|1036|French|fr-FR|  
|1038|Hungarian|hu-HU|  
|1040|Italian|it-IT|  
|1041|Japanese|ja-JP|  
|1042|Korean|ko-KR|  
|1043|Dutch|nl-NL|  
|1044|Norwegian (Bokmal)|nb-NO|  
|1045|Polish|pl-PLl|  
|1046|Portuguese (Brazil)|pt-BR|  
|1049|Russian|ru-RU|  
|1053|Swedish|sv-SE|  
|1055|Turkish|tr-TR|  
|2052|Chinese (Simplified)|zh-CN|  
|2070|Portuguese (Portugal)|pt-PT|  
|3082|Spanish (Spain)|es-ES|  
  
  
##  <a name="Uninstalling"></a> Uninstalling Report Builder  
 You can uninstall the stand-alone version of Report Builder from the control panel or the command line. This applies only to the stand-alone version of Report Builder. The [!INCLUDE[ndptecclick](../includes/ndptecclick-md.md)] of Report Builder cannot be uninstalled separately. It is always installed and uninstalled with [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)].  
  
 For more information, see [Uninstall the Stand-Alone Version of Report Builder &#40;Report Builder&#41;](install-windows/uninstall-report-builder.md).  
  
  
##  <a name="Supporting"></a> Supporting Report Builder  
 To support report authors, an administrator is responsible for managing folders, reports, and report related items on the report server, grant permission to resources on the report server, and configure the report server for access.  
  
### Folders, Reports, and Report Related Items  
 The following folders, reports, and report related items are managed on the report server:  
  
-   Folders in which you store reports, shared data sources, models and so forth.  
  
-   My Reports, the private folder in which you store your reports and report related items.  
  
-   Shared data sources that enable report authors to use data sources that are stored externally to reports.  
  
-   Shared datasets that can provide ready-to-use queried data to multiple reports for multiple users.  
  
-   Report parts such as tables and charts that enable users to enhance and reuse parts of reports, created by others, in a collaborative environment.  
  
-   Report models that can make it simpler and easier to get data for reports from complex data sources.  
  
-   Images such as background images and logos that might be used in multiple reports and are stored externally from reports for easy maintenance.  
  
 For more information, see [Report Server Content Management &#40;SSRS Native Mode&#41;](report-server/report-server-content-management-ssrs-native-mode.md) in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [Books Online](https://go.microsoft.com/fwlink/?LinkId=154888) on msdn.microsoft.com.  
  
### Permissions  
 The administrator grants permission to the report server. As a Report Builder user, you need permissions to the report server before you can access the content and functionality of the report server. For example, you might want to use report parts stored on the report server, update reports and resave them to the report server, and run reports in Report Manager. Depending on your needs and the tasks you perform, lower or higher permissions might be granted. For example, permissions with lower privileges are granted to users who need only to open shared reports in comparison to users who need to modify a shared report.  
  
 When [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] is installed in native mode, an administrator can:  
  
-   Enable the My Reports feature to provide you a private folder to create and save your own reports.  
  
-   Use the Report Builder role on public folders to allow you to open a copy of a shared report, and then save a modified version to a private folder.  
  
-   Use the Publisher role to allow you to manage reports and shared data sources in public folders. This role is granted to more experienced users.  
  
 When [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] is installed in SharePoint integrated mode, an administrator can:  
  
-   Use the Read permission level, granted to the Visitors group by default, to allow you to open a copy of a report in a public folder, and then save the modified version of the report to a private folder or to your computer.  
  
-   Use the Contribute permission level, granted to the Members groups by default, to allow you to manage reports and shared data sources in public folders. This level of permission is granted to more experienced users.  
  
 For general information about permissions and creating and using roles, see the [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] [!INCLUDE[ssDEnoversion](../includes/ssdenoversion-md.md)] documentation in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [Books Online](https://go.microsoft.com/fwlink/?LinkId=154888) on msdn.microsoft.com.  
  
### Configuration of Report Server  
 When you author reports in Report Builder and connect to an instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] that is installed on Windows Vista, Windows Server 2008, or Windows 7, you might encounter an access denied error when you attempt to access the report server to open or save a report. This occurs because the security feature, User Account Control (UAC), in Windows Vista, Windows Server 2008, and Windows 7 limit the overuse of elevated permissions by removing administrator permissions when accessing applications.  
  
 However, with additional configuration the report server is available to Report Builder users. You can add [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] URLs to trusted sites. By default, Internet Explorer 7.0 or later runs in Protected Mode on Windows Vista, Windows Server 2008, and Windows 7. Protected Mode is a feature that blocks browser requests from reaching high-level processes that run on the same computer. You can disable protected mode for the report server applications by adding them as Trusted Sites. You must have administrator permission to make this change.  
  
 For more information about configuring [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)], see [Reporting Services Configuration Manager &#40;del&#41;](/sql/2014/sql-server/install/reporting-services-configuration-manager-native-mode) in the [Reporting Services documentation](https://go.microsoft.com/fwlink/?linkid=121312) on msdn.microsoft.com.  
  
  
##  <a name="SampleDatabases"></a> SQL Server Sample Databases  
 The Adventure Works family of sample databases provides data that you can use to learn report authoring and write sample reports.  
  
 The databases are available in the following versions:  
  
-   Adventure Works OLTP database supports standard online transaction processing scenarios for a fictitious bicycle manufacturer (Adventure Works Cycles). Scenarios include Manufacturing, Sales, Purchasing, Product Management, Contact Management, and Human Resources.  
  
-   The [!INCLUDE[ssSampleDBDWobject](../includes/sssampledbdwobject-md.md)] database demonstrates how to build a data warehouse.  
  
-   The [!INCLUDE[ssAWDWsp](../includes/ssawdwsp-md.md)] project can be used to build an AS database for business intelligence scenarios.  
  
 The sample databases are not included with [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)] and not installed when you install [!INCLUDE[ssRSCurrent](../includes/ssrscurrent-md.md)] or the stand-alone version of Report Builder. Instead, you download sample databases from [CodePlex](https://go.microsoft.com/fwlink/?LinkId=87843). All versions of the sample databases are downloaded together. You can also download earlier database versions that were released with [!INCLUDE[ssVersion2005](../includes/ssversion2005-md.md)], [!INCLUDE[ssKatmai](../includes/sskatmai-md.md)], and [!INCLUDE[ssKilimanjaro](../includes/sskilimanjaro-md.md)].  
  
 For prerequisites and instructions about downloading and installing the [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)] sample databases, see [Installation Prerequisites for the SQL Server 2008 Sample Databases](https://go.microsoft.com/fwlink/?LinkId=166648) and [Installing Sample Databases](https://go.microsoft.com/fwlink/?LinkId=166649) on CodePlex.  
  
  
##  <a name="HowTo"></a> How-to Topics  
 This section lists procedures that show you how to install and uninstall Report Builder.  
  
 [Install the Stand-Alone Version of Report Builder &#40;Report Builder&#41;](install-windows/install-report-builder.md)  
  
 [Uninstall the Stand-Alone Version of Report Builder &#40;Report Builder&#41;](install-windows/uninstall-report-builder.md)  
  
 [Start Report Builder &#40;Report Builder&#41;](report-builder/start-report-builder.md)  
  
  
## See Also  
 [Report Builder in SQL Server 2014](report-builder/report-builder-in-sql-server-2016.md)  
  
  
