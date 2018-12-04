---
title: "Configure Report Manager (Native Mode) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "Report Manager [Reporting Services], configuring"
ms.assetid: e918986c-af15-48f6-8178-256aed829c6a
author: markingmyname
ms.author: maghan
manager: craigg
---
# Configure Report Manager (Native Mode)
  Report Manager is a Web front end application used to view reports, manage report server content, and grant user access to a native mode report server. Report Manager is installed with the Report Server Web service within the same report server instance and optionally configured if you select the **Install in the default native mode configuration** option in Setup. You can also configure Report Manager as a post-installation task. This topic provides information about the following Report Manager configuration scenarios:  
  
-   [Configure Report Manager to use the default URL](#ConfigureRMURL)  
  
     Report Manager is a Web application that users access in a Web browser. Minimally, you must define the URL used to open the application in a browser window. The URL consists of a host name, port, and virtual directory. Default values for this URL include the host name and port values that you defined for the Report Server Web service URL, plus the **reports** virtual directory name. If you have a named instance, the virtual directory is **reports_instance**, where **instance** is the name of your [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] instance.  
  
-   **Run Report Manager from a remote computer**. Depending on the configuration of your network, you may need to enable port 80 on computers to allow requests with Report Manager.  
  
    > [!TIP]  
    >  If you try to access Report Manager on a remote computer and you receive connection error messages in your browser, a common cause is Firewall settings. For more information, see [Configure a Firewall for Report Server Access](configure-a-firewall-for-report-server-access.md).  
  
     If necessary, enable port 80 on both computers to allow requests over that port. For more information, see [Configure a Firewall for Report Server Access](configure-a-firewall-for-report-server-access.md).  
  
-   [Configure Report Manager to use a specific report server URL](#ConfigureSpecificURL)  
  
     By default, Report Manager connects to the Report Server Web service that runs in the same Report Server service. The Report Manager uses the Report Server Web service URL to make the connection. If you define multiple URLs for the Report Server Web service, Report Manager uses the last one that you defined. However, for some deployments, you might want Report Manager to always connect to the Web service through a static URL. An example of why you might want to do so is if you configured packet filtering on a specific port or IP address, and you want all connections to the report server to go through the filter rules you defined.  
  
-   [Point Report Manager to a Remote Report Server](#ConfigureRemoteRS)  
  
     By default, Report Manager provides front end access to the Report Server Web service that runs in the same server instance, but you can configure Report Manager to connect to a remote Report Server Web service if you want to run the Web service and Report Manager in separate processes or if you are configuring server access differently for each server (for example, if you are deploying Report Manager to users on an extranet or Internet connection and you want to place a firewall between the report server and Report Manager).  
  
-   [Customize styles and application title](#ModifyTitle)  
  
     Report Manager, HTML report viewer, and report toolbar can be customized in limited ways by changing styles and editing the application title that appears in Report Manager.  
  
-   [Turn off Report Manager](#DisableRM)  
  
     When you install a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] instance that uses native mode, Report Manager is enabled by default. However, you can turn Report Manager off if you have a custom front-end application that provides equivalent functionality, if you only want to use the SOAP or URL Access interfaces to access the report server, or if you are using a Report Manager from a different report server instance.  
  
## Prerequisites  
 To use Report Manager, you must satisfy the following prerequisites:  
  
-   You must have a minimally configured report server. For more information about minimally configuring a report server, see [Configure a Report Server &#40;Reporting Services Native Mode&#41;](configure-a-report-server-reporting-services-native-mode.md).  
  
-   Your report server must run in native mode. You cannot use Report Manager with a report server that is configured for SharePoint integrated mode. In SQL Server 2012 you cannot switch a report server from one mode to the other. If you want to change the type of report server that your environment uses, you must install the desired mode of report server and then copy or move the report items to the new report server. This process is typically referred to as a 'migration'. The steps needed to migrate depend on the mode you are migrating to and the version you are migrating from. For more information, see [Upgrade and Migrate Reporting Services](../install-windows/upgrade-and-migrate-reporting-services.md).  
  
-   You must also have Internet Explorer 7.0 or later with scripting enabled. For more information, see [Planning for Reporting Services and Power View Browser Support &#40;Reporting Services 2014&#41;](../browser-support-for-reporting-services-and-power-view.md).  
  
##  <a name="ConfigureRMURL"></a> Configure Report Manager to use the default URL  
 By default, the Report Manager URL consists of a unique virtual directory name, plus the port and host name that is defined for the Report Server Web service that runs in the same instance. In most cases, the host name is the network name of the report server computer, but it can also be an IP address or host header that resolves the computer. To configure Report Manager to use the default URL, use the **Report Manager URL** page in the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool.  
  
#### To configure the default Report Manager URL and virtual directory  
  
1.  Start the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool and connect to the report server instance.  
  
2.  In the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool, click **Report Manager URL** to open the page for configuring the URL.  
  
3.  Enter a unique virtual directory name for Report Manager.  
  
4.  Click **Apply**.  
  
5.  If you are using [!INCLUDE[wiprlhlong](../../includes/wiprlhlong-md.md)] or Windows Server 2008, additional steps might be required before you can use Report Manager. For more information, see [Configure a Native Mode Report Server for Local Administration &#40;SSRS&#41;](configure-a-native-mode-report-server-for-local-administration-ssrs.md).  
  
##  <a name="ConfigureSpecificURL"></a> Configure Report Manager to use a specific report server URL  
 When you configure URLs in the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool, Report Manager automatically detects and uses any new and updated URLs for the report server that runs in the same server instance. If your deployment requires that you use a single, static URL for all report server requests, you can specify that URL in the RSReportServer.config file.  
  
#### To configure a static report server URL  
  
1.  Open the **RSReportServer.config** file in a text editor. By default, it is located at \Program Files\Microsoft SQL Server\MSRS12.\<*instancename*>\Reporting Services\ReportServer.  
  
2.  Find `ReportServerURL`.  
  
3.  Replace it with the URL of the report server instance.  
  
4.  Save your changes and close the file.  
  
 For more information about the configuration file, see [Modify a Reporting Services Configuration File &#40;RSreportserver.config&#41;](modify-a-reporting-services-configuration-file-rsreportserver-config.md) and [RSReportServer Configuration File](rsreportserver-config-configuration-file.md).  
  
##  <a name="ConfigureRemoteRS"></a> Configure Report Manager to use a remote report server  
 For deployment configurations that place Report Manager and the report server on different computers, you must have two separate installations of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]. Report Manager is embedded in the Report Server service and cannot be installed by itself. If you want to run Report Manager on a different computer within its own process, you must install a second report server. Both server instances must be [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] report servers.  
  
#### To connect Report Manager to a remote report server instance  
  
1.  Install two report server instances.  
  
2.  Configure the first installation that will host the report server:  
  
    1.  Start the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool.  
  
    2.  Click **Web Service URL** to configure a host name, port, and virtual directory for the report server.  
  
    3.  Click **Database** to configure the report server database.  
  
3.  Configure the second installation that will host Report Manager:  
  
    1.  Start the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool.  
  
    2.  Click **Report Manager URL** to enter a virtual directory name for Report Manager.  
  
     Do not configure the database. Do not test the URLs.  
  
4.  On the Report Manager computer, modify configuration settings in RSReportServer.config to point to the remote report server instance. On start up, Report Manager will read the configuration file to get the URL to the report server:  
  
    1.  Open RSReportServer.config in a text editor. By default, it is located at \Program Files\Microsoft SQL Server\MSRS11.\<*instancename*>\Reporting Services\ReportServer.  
  
    2.  Find `ReportServerURL`.  
  
    3.  Replace it with the URL of the remote report server instance.  
  
    4.  Save your changes and close the file.  
  
5.  > [!TIP]  
    >  If necessary, enable port 80 on both computers to allow requests over that port. For more information, see [Configure a Firewall for Report Server Access](configure-a-firewall-for-report-server-access.md).  
  
6.  Restart the report server.  
  
7.  Open Report Manager in a browser window. If it was already open, refresh the browser to verify Report Manager is connected to the remote server. You should see the content of the target server.  
  
8.  Turn off server features that you are not using:  
  
    -   On the Report Manager computer, turn off `WebServiceAndHTTPAccessEnabled` and `ScheduleEventsAndReportDeliveryEnabled`.  
  
    -   On the Report Server computer, turn off `ReportManagerEnabled`.  
  
 For more information about turning off features, see [Turn Reporting Services Features On or Off](turn-reporting-services-features-on-or-off.md).  
  
##  <a name="ModifyTitle"></a> Customize Styles or Application Title  
 [!INCLUDE[msCoName](../../includes/msconame-md.md)] does not support customization of the Report Manager style sheets. However, if you have expertise in Web development, you can modify the styles at your own risk. For more information about which files contain style information, see [Customize Style Sheets for HTML Viewer and Report Manager](../customize-style-sheets-for-html-viewer-and-report-manager.md).  
  
 Report Manager has an application title that appears at the top of the page. By default, the title is **SQL Server Reporting Services**. This title can be customized. To change the title, use the Site Settings page in Report Manager. To modify application settings in Report Manager, you must be assigned to the **System Administrator** role to set properties on the Site Settings page. To view the application title, users must be assigned to the **System User** role.  
  
#### To modify application title  
  
1.  Log on using an account that is assigned **System Administrator** permissions on the report server.  
  
2.  Open Internet Explorer.  
  
3.  Enter the Report Manager URL. By default, it is http://\<**your-server-name**>/reports, but if you installed Reporting Services as a named instance, the default URL will be http://\<**your-server-name**>/reports\<**_instancename**>.  
  
4.  Click **Site Settings**.  
  
5.  On the **General** tab, in **Name**, replace **SQL Server Reporting Services** with a different name.  
  
6.  Click **Apply**.  
  
##  <a name="DisableRM"></a> Turn Off Report Manager  
 You can turn off Report Manager if you have a custom application that provides equivalent functionality or if you are using the Report Manager application of a different service instance. To turn off Report Manager, you can modify the RSReportServer.config file.  
  
#### To turn off Report Manager  
  
1.  Open the RSReportServer.config file in a text editor. By default, it is located at \Program Files\Microsoft SQL Server\MSRS11.\<*instancename*>\Reporting Services\ReportServer.  
  
2.  Find **IsReportManagerEnabled**.  
  
3.  Set the value to **False**.  
  
4.  Save your changes and close the file.  
  
 For more information about how to modify the configuration file, see [Modify a Reporting Services Configuration File &#40;RSreportserver.config&#41;](modify-a-reporting-services-configuration-file-rsreportserver-config.md). For more information about disabling features in [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], see [Turn Reporting Services Features On or Off](turn-reporting-services-features-on-or-off.md).  
  
## See Also  
 [Report Manager  &#40;SSRS Native Mode&#41;](../report-manager-ssrs-native-mode.md)   
 [Planning for Reporting Services and Power View Browser Support &#40;Reporting Services 2014&#41;](../browser-support-for-reporting-services-and-power-view.md)   
 [Configure a URL  &#40;SSRS Configuration Manager&#41;](../install-windows/configure-a-url-ssrs-configuration-manager.md)   
 [Verify a Reporting Services Installation](../install-windows/verify-a-reporting-services-installation.md)   
 [Customize Style Sheets for HTML Viewer and Report Manager](../customize-style-sheets-for-html-viewer-and-report-manager.md)   
 [Turn Reporting Services Features On or Off](turn-reporting-services-features-on-or-off.md)   
 [Manage a Reporting Services Native Mode Report Server](manage-a-reporting-services-native-mode-report-server.md)   
 [RSReportServer Configuration File](rsreportserver-config-configuration-file.md)   
 [Configure a Native Mode Report Server for Local Administration &#40;SSRS&#41;](configure-a-native-mode-report-server-for-local-administration-ssrs.md)  
  
  
