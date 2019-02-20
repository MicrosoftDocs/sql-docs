---
title: "Report Server Status (SSRS Native Mode) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
f1_keywords: 
  - "SQL12.rsconfigtool.serverstatus.F1"
ms.assetid: 2f63ad1c-1bc2-449d-b451-fb39a0060838
author: markingmyname
ms.author: maghan
manager: craigg
---
# Report Server Status (SSRS Native Mode)
  Use this page to view information about the report server instance to which you are currently connected. This page is the start page for report server configuration. Additional pages are available to configure URLs, the service account, the report server database, report server e-mail delivery, scale-out deployment, and encryption keys.  
  
 [!INCLUDE[applies](../../includes/applies-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Native mode.  
  
 To open this page, start the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager and connect to the report server instance. For more information, see [Reporting Services Configuration Manager &#40;del&#41;](/sql/2014/sql-server/install/reporting-services-configuration-manager-native-mode).  
  
> [!TIP]  
>  The[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager (RSConfigTool.exe) is installed with a privilege level of "highestAvailable". This behavior is by design. The [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager requires communication with [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] WMI APIs. Some of the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] WMI communication requires a higher level or administrative of privileges.  
  
 If you connect to the report server and all of the page links are grayed out, verify that the Report Server service is started. The **Report Service Status:** Should be "Started". You can also use the Services console application in Administrator Tools to check service status.  
  
## Options  
 **SQL Server Instance**  
 Displays information about the report server instance to which you are currently connected. Report server instance names are based on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] named instances. The default instance is MSSQLSERVER. A named instance will be a value that you specified during setup. For more information about instances, see [Work with Multiple Versions and Instances of SQL Server](../../../2014/sql-server/install/work-with-multiple-versions-and-instances-of-sql-server.md) in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.  
  
> [!NOTE]  
>  In SQL Server Express with Advanced Services, the default instance is SQLExpress.  
  
 **Instance ID**  
 Corresponds to a folder on the file system that stores program files for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance to which you are connected. The **Instance ID** value is assigned by Setup in the format *component*.*instance*, where *component* is a value that signifies a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] component and *instance* is an instance name. The default instance name is MSSQLSERVER. For example, if you install default instances of the [!INCLUDE[ssDE](../../includes/ssde-md.md)], [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], and [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] components, the corresponding folder names are the following:  
  
-   MSSQL12.MSSQLSERVER  
  
-   MSAS12.MSSQLSERVER  
  
-   MSRS12.MSSQLSERVER  
  
 If you install a second instance of a component that you already installed, such as the [!INCLUDE[ssDE](../../includes/ssde-md.md)], and you name the instance Contoso, the **Instance ID** is MSSQL12.Contoso.  
  
 **Edition**  
 Displays edition information. For a list of features that are supported by the editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Features Supported by the Editions of SQL Server](https://go.microsoft.com/fwlink/?linkid=232473).  
  
 **Product Version**  
 Displays the version of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] that you installed.  
  
 **Report Server Database**  
 Displays the name of the report server database that stores application data for the current report server instance.  
  
 **Report Server Mode**  
 This should always show a value of **Native**. The [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] configuration manager only supports Native mode report servers. If you see a value of **SharePoint integrated mode**, it may indicate your Native mode deployment is not configured correctly, and you need to connect to a Native mode report catalog. Use the **Database** page of Configuration Manager to change the database and either create a new database or connect to an existing native mode report server database catalog.  
  
 **Server Status**  
 Shows whether the Report Server service is running.  
  
 **Start**  
 Starts the Report Server service. Restarting the service is necessary after some configuration changes (for example, when reconfiguring a report server after a computer name change). If you reconfigure the URL reservations, the service will restart automatically. The restart is required to pick up the changes.  
  
 **Stop**  
 Stops the Report Server service. Stopping the service causes the report server to stop working. For more information, see [Start and Stop the Report Server Service](../../reporting-services/report-server/start-and-stop-the-report-server-service.md) in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.  
  
## See Also  
 [Reporting Services Configuration Manager F1 Help Topics &#40;SSRS Native Mode&#41;](../../../2014/sql-server/install/reporting-services-configuration-manager-f1-help-topics-ssrs-native-mode.md)   
 [Reporting Services Configuration Manager &#40;del&#41;](/sql/2014/sql-server/install/reporting-services-configuration-manager-native-mode)   
 [Initialize a Report Server &#40;SSRS Configuration Manager&#41;](../../reporting-services/install-windows/ssrs-encryption-keys-initialize-a-report-server.md)  
  
  
