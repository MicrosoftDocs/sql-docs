---
title: "Breaking Changes in SQL Server Reporting Services in SQL Server 2014 | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "Me.Value references"
  - "Reporting Services, backward compatibility"
  - "breaking changes [Reporting Services]"
ms.assetid: 39c7aafd-dcb9-4317-b8f7-d15828eb4f9a
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Breaking Changes in SQL Server Reporting Services in SQL Server 2014
  This topic describes breaking changes in [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)]. These changes might break applications, scripts, or functionalities that are based on earlier versions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. You might encounter these issues when you upgrade, or in custom scripts or reports. For more information, see [Use Upgrade Advisor to Prepare for Upgrades](../sql-server/install/use-upgrade-advisor-to-prepare-for-upgrades.md).  
  
 **In this topic:**  
  
-   [SQL Server 2014 Reporting Services Breaking Changes](#bkmk_sql14)  
  
-   [SQL Server 2012 Reporting Services Breaking Changes](#bkmk_rc0)  
  
-   [SQL Server 2008 R2 Reporting Services Breaking Changes](#bkmk_kj)  
  
##  <a name="bkmk_sql14"></a> [!INCLUDE[ssSQL14](../includes/sssql14-md.md)] Reporting Services Breaking Changes  
 There are no [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] breaking changes in [!INCLUDE[ssSQL14](../includes/sssql14-md.md)].  
  
##  <a name="bkmk_rc0"></a> [!INCLUDE[ssSQL11](../includes/sssql11-md.md)] Reporting Services Breaking Changes  
  
### SharePoint Mode Server References Require the SharePoint Site  
 You cannot browse or reference directly to the Report Server using the virtual directly name in the URL path. For example:  
  
 `http://<Server name>/ReportServer`  
  
 It is now required that you include the SharePoint site in the URL path. For example, if your site name is '`videos`' and used the '`sites`' prefix, the URL would look similar to the following:  
  
 `http://<Server Name>/sites/videos/_vti_bin/ReportServer`  
  
### Changes to SharePoint Mode Command-Line Installation  
 The input setting **/RSINSTALLMODE** only works with Native mode installations and it does not work for SharePoint mode installations. For example, the following is not supported in [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)]: **/RSINSTALLMODE="DefaultSharePointMode"**. In place of that input setting, use **/RSSHPINSTALLMODE="DefaultSharePointMode"**.  
  
 The following statement is an example of a complete installation command and parameter set: **setup /ACTION=install /FEATURES=SQL,RS /InstanceName=Denali_INST1 .... /RSSHPINSTALLMODE="DefaultSharePointMode"**  
  
 For more information on command-line installations, see [Command Prompt Installation of Reporting Services SharePoint Mode and Native Mode](install-windows/install-reporting-services-at-the-command-prompt.md).  
  
### The Reporting Services WMI Provider no longer supports Configuration of SharePoint Mode  
 The Configuration of [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] SharePoint is now completed using PowerShell cmdlets and SharePoint Central Administration. The new [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] SharePoint mode architecture utilizes the SharePoint services architecture. SharePoint does not support WMI interfaces.  
  
 These changes affect the following list of components and work flows:  
  
-   Custom applications that use the [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] WMI provider for [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] in SharePoint mode.  
  
-   The [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] Configuration Manager, rskeymgmt.exe, and rsconfig.exe. Instead of using those utilities for configuration of [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] SharePoint mode, use SharePoint Central Administration and PowerShell.  
  
-   SQL Server Management Studio: Customers cannot reference a server with syntax similar to <machine_name>/<instance_name>. Starting with the [!INCLUDE[ssKatmai](../includes/sskatmai-md.md)] release, the recommended method was to use a SharePoint site URL. For example, **http://<sharepoint_server>/<sharePoint_site>**. Starting with [!INCLUDE[ssSQL11](../includes/sssql11-md.md)], a SharePoint site URL is the only supported syntax.  
  
### Report Model Designer is not available in SQL Server Data Tools  
 [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)] no longer supports report model projects. The Report Model designer is not available in [!INCLUDE[ssRSCurrent](../includes/ssrscurrent-md.md)]. You cannot create new Report Model projects or open existing projects in [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)] and you cannot create or update report models. To update report models, you can use [!INCLUDE[ssKilimanjaro](../includes/sskilimanjaro-md.md)][!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] or earlier tools. You can continue to use report models as data sources in reports authored in [!INCLUDE[ssRSCurrent](../includes/ssrscurrent-md.md)] tools such as Report Builder and Report Designer. The query designer that you use to create queries to extract report data from report models continues to be available in [!INCLUDE[ssSQL11](../includes/sssql11-md.md)][!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)].  
  
##  <a name="bkmk_kj"></a> SQL Server 2008 R2 Reporting Services Breaking Changes  
 This section describes breaking changes in [!INCLUDE[ssKilimanjaro](../includes/sskilimanjaro-md.md)] [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)].  
  
> [!NOTE]  
>  Because SQL Server 2008 R2 is a minor version upgrade of SQL Server 2008, we recommend that you also review the content in the SQL Server 2008 section.  
  
### Expanded CSV Data Renderer  
 In [!INCLUDE[ssKilimanjaro](../includes/sskilimanjaro-md.md)] [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)], the CSV file includes chart and gauge data. Applications that depend on a previous CSV file structure will no longer work because of the additional columns for charts and gauges.  
  
 For more information, see [Exporting to a CSV File &#40;Report Builder and SSRS&#41;](report-builder/exporting-to-a-csv-file-report-builder-and-ssrs.md).  
  
## See Also  
 [Behavior Changes to SQL Server Reporting Services  in SQL Server 2014](behavior-changes-to-sql-server-reporting-services-in-sql-server-2016.md)   
 [What's New &#40;Reporting Services&#41;](what-s-new-reporting-services.md)   
 [Deprecated Features in SQL Server Reporting Services in SQL Server 2014](deprecated-features-in-sql-server-reporting-services-ssrs.md)   
 [Discontinued Functionality to SQL Server Reporting Services in SQL Server 2014](discontinued-functionality-to-sql-server-reporting-services-in-sql-server.md)  
  
  
