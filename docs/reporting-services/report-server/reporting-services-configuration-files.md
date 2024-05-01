---
title: "Reporting Services configuration files"
description: Learn about configuration files where Reporting Services stores component information. You might need to modify the files to add or configure advanced settings.
author: maggiesMSFT
ms.author: maggies
ms.date: 05/30/2019
ms.service: reporting-services
ms.subservice: report-server
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "deploying [Reporting Services], configuration files"
  - "configuration options [Reporting Services]"
  - "modifying configuration files"
  - "configuration files [Reporting Services]"
---
# Reporting Services configuration files
  [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] stores component information in the registry and in configuration files that are copied to the file system during setup. Configuration files contain a combination of internal-use-only and user-defined values. User-defined values are specified through Setup, the configuration tools, the command line utilities, and by manually editing the configuration files.  
  
 Modifying the configuration files is only necessary if you're adding or configuring advanced settings. Configuration settings are specified as either XML elements or attributes. If you understand XML and configuration files, you can use a text or code editor to modify user-definable settings. For more information about how to modify a configuration file or to learn more about how the report server reads new and updated configuration settings, see [Modify a Reporting Services configuration file &#40;RSreportserver.config&#41;](../../reporting-services/report-server/modify-a-reporting-services-configuration-file-rsreportserver-config.md).  
  
> [!NOTE]  
> In previous releases, Report Manager had its own configuration file named `RSWebApplication.config`. That file is now obsolete. If you upgraded from a previous installation, the file isn't deleted but the report server doesn't read any settings from it. If the file exists on your computer, you should delete it. In [!INCLUDE[sql2008-md](../../includes/sql2008-md.md)] and later versions, all Report Manager and web portal configuration settings are stored in and read from the `RSReportServer.config` file. To review a list of which settings were deleted or moved, see [Breaking changes in SQL Server Reporting Services in SQL Server 2016](../../reporting-services/breaking-changes-in-sql-server-reporting-services-in-sql-server-2016.md).  
  
 In this article:  
  
-   [Summary of configuration files (native mode)](#bkmk_config_file_Summary_native_mode)  
  
-   [Summary of configuration files (SharePoint mode)](#bkmk_config_file_Summary_sharepoint_mode)  
  
##  <a name="bkmk_config_file_Summary_native_mode"></a> Summary of configuration files (native mode)  
 The following table provides a description of where configuration settings are stored. Most configuration settings are stored in configuration files that are included with [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]. By default, the installation directory is:  
  
```
Install Paths  
C:\Program Files\Microsoft SQL Server\MSRSxx.MSSQLSERVER  (where xx is the MS SQL version number)
  or  
C:\Program Files\Microsoft SQL Server Reporting Services\SSRS  
  depending on the SSRS version
```  
  
|Stored in:|Description|Location|  
|----------------|-----------------|--------------|  
|`RSReportServer.config`|Stores configuration settings for feature areas of the Report Server service: Report Manager or the web portal, the Report Server Web service, and background processing. For more information about each setting, see [RsReportServer.config configuration file](../../reporting-services/report-server/rsreportserver-config-configuration-file.md).|`<Installation directory>\Reporting Services\ReportServer`|  
|`RSSrvPolicy.config`|Stores the code access security policies for the server extensions. For more information about this file, see [Use Reporting Services security policy files](../../reporting-services/extensions/secure-development/using-reporting-services-security-policy-files.md).|`<Installation directory>\Reporting Services\ReportServer`|  
|`RSMgrPolicy.config`|Stores the code access security policies for the web portal. For more information about this file, see [Use Reporting Services security policy files](../../reporting-services/extensions/secure-development/using-reporting-services-security-policy-files.md).|`<Installation directory>\Reporting Services\ReportManager`|  
|`Web.config` for the Report Server Web service|Includes only those settings that are required for ASP.NET.|`<Installation directory>\Reporting Services\ReportServer`|  
|`Web.config` for Report Manager|Includes only those settings that are required for ASP.NET if applicable for the SSRS version.|`<Installation directory>\Reporting Services\ReportManager`|  
|`ReportingServicesService.exe.config`|Stores configuration settings that specify the trace levels and logging options for the Report Server service. For more information about the elements in this file, see [ReportingServicesService configuration file](../../reporting-services/report-server/reportingservicesservice-configuration-file.md).|`<Installation directory>\Reporting Services\ReportServer\Bin`|  
|Registry settings|Stores configuration state and other settings used to uninstall Reporting Services. If you're troubleshooting an installation or configuration problem, you can view these settings to get information about how the report server is configured.<br /><br /> Don't modify these settings directly as this change can invalidate your installation.|`HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\<InstanceID>\Setup`<br /><br /> **- And -**<br /><br /> `HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\Services\ReportServer`|  
|`RSReportDesigner.config`|Stores configuration settings for Report Designer. For more information, see [RSReportDesigner configuration file](../../reporting-services/report-server/rsreportdesigner-configuration-file.md).|`<drive>:\Program Files\Microsoft Visual Studio 10\Common7\IDE\PrivateAssemblies`.|  
|`RSPreviewPolicy.config`|Stores the code access security policies for the server extensions used during report preview. For more information about this file, see [Use Reporting Services security policy files](../../reporting-services/extensions/secure-development/using-reporting-services-security-policy-files.md).|`C:\Program Files\Microsoft Visual Studio 10.0\Common7\IDE\PrivateAssembliesr`|  
  
##  <a name="bkmk_config_file_Summary_sharepoint_mode"></a> Summary of configuration files (SharePoint mode)  
 The following table provides a description of configuration files used for a SharePoint mode report server. Most configuration settings are stored in SharePoint service application databases. For more information, see [Reporting Services SharePoint service and service applications](../../reporting-services/report-server-sharepoint/reporting-services-sharepoint-service-and-service-applications.md).  
  
 By default, the installation directory for SharePoint mode is:  
  
```
Install path
C:\Program Files\Common Files\Microsoft Shared\Web Server Extensions\15\WebServices\Reporting  
```  
  
|Stored in:|Description|Location|  
|----------------|-----------------|--------------|  
|`RSReportServer.config`|Stores configuration settings for feature areas of the Report Server service: Report Manager or the web portal, the Report Server Web service, and background processing. For more information about each setting, see [RsReportServer.config configuration file](../../reporting-services/report-server/rsreportserver-config-configuration-file.md).|`<Installation directory>\Reporting Services\ReportServer`|  
|`RSSrvPolicy.config`|Stores the code access security policies for the server extensions. For more information about this file, see [Use Reporting Services security policy files](../../reporting-services/extensions/secure-development/using-reporting-services-security-policy-files.md).|`<Installation directory>\Reporting Services\ReportServer`|  
|`Web.config` for the Report Server Web service|Includes only those settings that are required for ASP.NET if applicable for the SSRS version.|`<Installation directory>\Reporting Services\ReportServer`|  
|Registry settings|Stores configuration state and other settings used to uninstall Reporting Services. Also stores information about each [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] service application.<br /><br /> Don't modify these settings directly as this change can invalidate your installation.|`HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft \Microsoft SQL Server\<InstanceID>\Setup`<br /><br /> Example instance ID: `MSSQL13.MSSQLSERVER`<br /><br /> **- And -**<br /><br /> `HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\Reporting Services\Service Applications`|  
|`RSReportDesigner.config`|Stores configuration settings for Report Designer. For more information, see [RSReportDesigner configuration file](../../reporting-services/report-server/rsreportdesigner-configuration-file.md).|`<drive>:\Program Files\Microsoft Visual Studio 10\Common7\IDE\PrivateAssemblies`.|  
  
## Related content  
 [Reporting Services report server &#40;native mode&#41;](../../reporting-services/report-server/reporting-services-report-server-native-mode.md)   
 [Reporting Services extensions](../../reporting-services/extensions/reporting-services-extensions.md)   
 [rsconfig utility &#40;SSRS&#41;](../../reporting-services/tools/rsconfig-utility-ssrs.md)   
 [Start and stop the Report Server service](../../reporting-services/report-server/start-and-stop-the-report-server-service.md)  
  
  
