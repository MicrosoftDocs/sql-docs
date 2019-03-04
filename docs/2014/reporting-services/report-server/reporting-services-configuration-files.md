---
title: "Reporting Services Configuration Files | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "deploying [Reporting Services], configuration files"
  - "configuration options [Reporting Services]"
  - "modifying configuration files"
  - "configuration files [Reporting Services]"
ms.assetid: 21e5c32f-ad67-4917-b55a-8e21bd64f5a6
author: markingmyname
ms.author: maghan
manager: kfile
---
# Reporting Services Configuration Files
  [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] stores component information in the registry and in configuration files that are copied to the file system during setup. Configuration files contain a combination of internal-use-only and user-defined values. User-defined values are specified through Setup, the configuration tools, the command line utilities, and by manually editing the configuration files.  
  
 Modifying the configuration files is only necessary if you are adding or configuring advanced settings. Configuration settings are specified as either XML elements or attributes. If you understand XML and configuration files, you can use a text or code editor to modify user-definable settings. For more information about how to modify a configuration file or to learn more about how the report server reads new and updated configuration settings, see [Modify a Reporting Services Configuration File &#40;RSreportserver.config&#41;](modify-a-reporting-services-configuration-file-rsreportserver-config.md).  
  
> [!NOTE]  
>  In previous releases, Report Manager had its own configuration file named RSWebApplication.config. That file is now obsolete. If you upgraded from a previous installation, the file will not be deleted but the report server will not read any settings from it. If the file exists on your computer, you should delete it. In [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] and later versions, all Report Manager configuration settings are stored in and read from the RSReportServer.config file. To review a list of which settings were deleted or moved, see [Breaking Changes in SQL Server Reporting Services in SQL Server 2014](../breaking-changes-in-sql-server-reporting-services-in-sql-server-2016.md).  
  
 In this topic:  
  
-   [Summary of Configuration Files (Native Mode)](#bkmk_config_file_Summary_native_mode)  
  
-   [Summary of Configuration Files (SharePoint Mode)](#bkmk_config_file_Summary_sharepoint_mode)  
  
##  <a name="bkmk_config_file_Summary_native_mode"></a> Summary of Configuration Files (Native Mode)  
 The following table provides a description of where configuration settings are stored. Most configuration settings are stored in configuration files that are included with [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]. By default, the installation directory is the following:  
  
```  
C:\Program Files\Microsoft SQL Server\MSRS12.MSSQLSERVER  
```  
  
|Stored in:|Description|Location|  
|----------------|-----------------|--------------|  
|RSReportServer.config|Stores configuration settings for feature areas of the Report Server service: Report Manager, the Report Server Web service, and background processing. For more information about each setting, see [RSReportServer Configuration File](rsreportserver-config-configuration-file.md).|\<Installation directory> \Reporting Services \ReportServer|  
|RSSrvPolicy.config|Stores the code access security policies for the server extensions. For more information about this file, see [Using Reporting Services Security Policy Files](../extensions/secure-development/using-reporting-services-security-policy-files.md).|\<Installation directory> \Reporting Services \ReportServer|  
|RSMgrPolicy.config|Stores the code access security policies for Report Manager. For more information about this file, see [Using Reporting Services Security Policy Files](../extensions/secure-development/using-reporting-services-security-policy-files.md).|\<Installation directory> \Reporting Services \ReportManager|  
|Web.config for the Report Server Web service|Includes only those settings that are required for ASP.NET.|\<Installation directory> \Reporting Services \ReportServer|  
|Web.config for Report Manager|Includes only those settings that are required for ASP.NET.|\<Installation directory> \Reporting Services \ReportManager|  
|ReportingServicesService.exe.config|Stores configuration settings that specify the trace levels and logging options for the Report Server service. For more information about the elements in this file, see [ReportingServicesService Configuration File](reportingservicesservice-configuration-file.md).|\<Installation directory> \Reporting Services \ReportServer \Bin|  
|Registry settings|Stores configuration state and other settings used to uninstall Reporting Services. If you are troubleshooting an installation or configuration problem, you can view these settings to get information about how the report server is configured.<br /><br /> Do not modify these settings directly as this can invalidate your installation.|HKEY_LOCAL_MACHINE \SOFTWARE \Microsoft \Microsoft SQL Server \\<InstanceID\> \Setup<br /><br /> **- And -**<br /><br /> HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\Services\ReportServer|  
|RSReportDesigner.config|Stores configuration settings for Report Designer. For more information, see [RSReportDesigner Configuration File](rsreportdesigner-configuration-file.md).|\<drive>:\Program Files \Microsoft Visual Studio 10 \Common7 \IDE \PrivateAssemblies.|  
|RSPreviewPolicy.config|Stores the code access security policies for the server extensions used during report preview. For more information about this file, see [Using Reporting Services Security Policy Files](../extensions/secure-development/using-reporting-services-security-policy-files.md).|C:\Program Files\Microsoft Visual Studio 10.0\Common7\IDE\PrivateAssembliesr|  
  
##  <a name="bkmk_config_file_Summary_sharepoint_mode"></a> Summary of Configuration Files (SharePoint Mode)  
 The following table provides a description of configuration files used for a SharePoint mode report server. Most configuration settings are stored in SharePoint service application databases. For more information, see [Reporting Services SharePoint Service and Service Applications](../reporting-services-sharepoint-service-and-service-applications.md).  
  
 By default, the installation directory for SharePoint mode is the following:  
  
```  
C:\Program Files\Common Files\Microsoft Shared\Web Server Extensions\15\WebServices\Reporting  
```  
  
|Stored in:|Description|Location|  
|----------------|-----------------|--------------|  
|RSReportServer.config|Stores configuration settings for feature areas of the Report Server service: Report Manager, the Report Server Web service, and background processing. For more information about each setting, see [RSReportServer Configuration File](rsreportserver-config-configuration-file.md).|\<Installation directory> \Reporting Services \ReportServer|  
|RSSrvPolicy.config|Stores the code access security policies for the server extensions. For more information about this file, see [Using Reporting Services Security Policy Files](../extensions/secure-development/using-reporting-services-security-policy-files.md).|\<Installation directory> \Reporting Services \ReportServer|  
|Web.config for the Report Server Web service|Includes only those settings that are required for ASP.NET.|\<Installation directory> \Reporting Services \ReportServer|  
|Registry settings|Stores configuration state and other settings used to uninstall Reporting Services. Also stores information about each [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] service application.<br /><br /> Do not modify these settings directly as this can invalidate your installation.|HKEY_LOCAL_MACHINE \SOFTWARE \Microsoft \Microsoft SQL Server \\<InstanceID\> \Setup<br /><br /> Example instance ID: MSSQL12.MSSQLSERVER<br /><br /> **- And -**<br /><br /> HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\Reporting Services\Service Applications|  
|RSReportDesigner.config|Stores configuration settings for Report Designer. For more information, see [RSReportDesigner Configuration File](rsreportdesigner-configuration-file.md).|\<drive>:\Program Files \Microsoft Visual Studio 10 \Common7 \IDE \PrivateAssemblies.|  
  
## See Also  
 [Reporting Services Report Server &#40;Native Mode&#41;](reporting-services-report-server-native-mode.md)   
 [Reporting Services Extensions](../extensions/reporting-services-extensions.md)   
 [rsconfig Utility &#40;SSRS&#41;](../tools/rsconfig-utility-ssrs.md)   
 [Start and Stop the Report Server Service](start-and-stop-the-report-server-service.md)  
  
  
