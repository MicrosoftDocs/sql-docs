---
title: "Report Manager URL (SSRS Native Mode) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
f1_keywords: 
  - "sql12.rsconfigtool.reportmanagervirtualdirectory.f1"
ms.assetid: 45768952-23a6-45a5-b541-e7bf192b4a78
author: markingmyname
ms.author: maghan
manager: craigg
---
# Report Manager URL (SSRS Native Mode)
  Use the Report Manager URL page to configure or modify the URL used to access Report Manager. By default, the Report Manager URL inherits the prefix, IP address, and port of the Report Server Web service URL. This is because Report Manager provides front-end access to the Web service that runs within the same Report Server service. If you are isolating the service applications and using Report Manager to access a Report Server Web service on a different computer, you must edit RSReportServer.config file to point Report Manager to a different instance. For more information about configuring a Report Manager connection to a remote report server, see [Reporting Services Configuration Manager &#40;Native Mode&#41;](../../../2014/sql-server/install/reporting-services-configuration-manager-native-mode.md).  
  
 [!INCLUDE[applies](../../includes/applies-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Native mode.  
  
 If you are configuring the report server to run in SharePoint integrated mode, do not create a Report Manager URL. Report Manager is not supported on a report server that runs in SharePoint integrated mode. If a URL already exists for Report Manager, it will become unavailable after you configure the report server to run in SharePoint integrated mode.  
  
 To open this page, start the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager and click **Report Manager URL** in the navigation pane. For more information about how to start the Configuration Manager, see [Reporting Services Configuration Manager &#40;Native Mode&#41;](../../../2014/sql-server/install/reporting-services-configuration-manager-native-mode.md).  
  
> [!NOTE]  
>  If Report Manager is not enabled, you cannot set options on this page. For more information about enabling Report Manager, see [Reporting Services Configuration Manager &#40;Native Mode&#41;](../../../2014/sql-server/install/reporting-services-configuration-manager-native-mode.md).  
  
## Options  
 **Virtual Directory**  
 Specifies the virtual directory name for Report Manager. You can only have one virtual directory name for each Report Manager instance on the same computer.  
  
 **URLs**  
 Displays the URL defined for the current Report Manager instance.  
  
 **Advanced**  
 Add an additional URL for the current Report Manager instance.  
  
## See Also  
 [Configure a URL  &#40;SSRS Configuration Manager&#41;](../../reporting-services/install-windows/configure-a-url-ssrs-configuration-manager.md)   
 [URLs in Configuration Files  &#40;SSRS Configuration Manager&#41;](../../reporting-services/install-windows/urls-in-configuration-files-ssrs-configuration-manager.md)   
 [Configure Report Server URLs  &#40;SSRS Configuration Manager&#41;](../../reporting-services/install-windows/configure-report-server-urls-ssrs-configuration-manager.md)  
  
  
