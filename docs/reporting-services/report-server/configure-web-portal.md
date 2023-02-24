---
title: "Configure the web portal | Microsoft Docs"
description: Learn how to configure the web portal application, used to view reports, manage report server content, and grant user access to a native mode report server.
ms.date: 05/10/2017
ms.service: reporting-services
ms.subservice: report-server

ms.topic: conceptual
helpviewer_keywords: 
  - "the web portal [Reporting Services], configuring"
ms.assetid: e918986c-af15-48f6-8178-256aed829c6a
author: maggiesMSFT
ms.author: maggies
---
# Configure the web portal

the web portal is a Web front end application used to view reports, manage report server content, and grant user access to a native mode report server. the web portal is installed with the Report Server Web service within the same report server instance and optionally configured if you select the **Install in the default native mode configuration** option in Setup. You can also configure the web portal as a post-installation task. This topic provides information about the following web portal configuration scenarios:

## Prerequisites

To use the web portal, you must satisfy the following prerequisites:

- You must have a minimally configured report server. For more information about minimally configuring a report server, see [Configure a Report Server](../../reporting-services/report-server/configure-a-report-server-reporting-services-native-mode.md).

- Your report server must run in native mode. You cannot use the web portal with a report server that is configured for SharePoint integrated mode. In SQL Server 2012 you cannot switch a report server from one mode to the other. If you want to change the type of report server that your environment uses, you must install the desired mode of report server and then copy or move the report items to the new report server. This process is typically referred to as a 'migration'. The steps to migrate depend on the mode you're migrating to and the version you're migrating from. For more information, see [Upgrade and Migrate Reporting Services](../../reporting-services/install-windows/upgrade-and-migrate-reporting-services.md).

- You must also have Internet Explorer 11 or later with scripting enabled. For more information, see [Browser Support for Reporting Services](../../reporting-services/browser-support-for-reporting-services-and-power-view.md).

## Configure the web portal to use the default URL

The web portal is a Web application that users access in a Web browser. Minimally, you must define the URL used to open the application in a browser window. The URL consists of a host name, port, and virtual directory. Default values for this URL include the host name and port values that you defined for the Report Server Web service URL, plus the **reports** virtual directory name. If you have a named instance, the virtual directory is **reports_instance**, where **instance** is the name of your [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] instance.

By default, the web portal URL consists of a unique virtual directory name, plus the port and host name that is defined for the Report Server Web service that runs in the same instance. In most cases, the host name is the network name of the report server computer, but it can also be an IP address or host header that resolves the computer. To configure the web portal to use the default URL, use the **Web Portal URL** page in the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool.

> [!TIP]
> If you try to access the web portal on a remote computer and you receive connection error messages in your browser, a common cause is Firewall settings. For more information, see [Configure a Firewall for Report Server Access](../../reporting-services/report-server/configure-a-firewall-for-report-server-access.md).

#### To configure the default the web portal URL and virtual directory

1. Start the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool and connect to the report server instance.

2. In the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool, select **Web Portal URL** to open the page for configuring the URL.

3. Enter a unique virtual directory name for the web portal.

4. Click **Apply**.

5. If you're using [!INCLUDE[winvista](../../includes/winvista-md.md)] or Windows Server 2008, additional steps might be required before you can use the web portal. For more information, see [Configure a Native Mode Report Server for Local Administration &#40;SSRS&#41;](../../reporting-services/report-server/configure-a-native-mode-report-server-for-local-administration-ssrs.md).

## Configure the web portal to use a specific report server URL

By default, the web portal connects to the Report Server Web service that runs in the same Report Server service. The web portal uses the Report Server Web service URL to make the connection. If you define multiple URLs for the Report Server Web service, the web portal uses the last one that you defined. However, for some deployments, you might want the web portal to always connect to the Web service through a static URL. An example of why you might want to do so is if you configured packet filtering on a specific port or IP address, and you want all connections to the report server to go through the filter rules you defined.

When you configure URLs in the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool, the web portal automatically detects and uses any new and updated URLs for the report server that runs in the same server instance. If your deployment requires that you use a single, static URL for all report server requests, you can specify that URL in the RSReportServer.config file.

#### To configure a static report server URL

1. Open the **RSReportServer.config** file in a text editor. By default, it's located at \Program Files\Microsoft SQL Server\MSRS12.\<*instancename*>\Reporting Services\ReportServer.  

2. Find **ReportServerURL**.

3. Replace it with the URL of the report server instance.

4. Save your changes and close the file.

For more information about the configuration file, see [Modify a Reporting Services Configuration File &#40;RSreportserver.config&#41;](../../reporting-services/report-server/modify-a-reporting-services-configuration-file-rsreportserver-config.md) and [RsReportServer.config Configuration File](../../reporting-services/report-server/rsreportserver-config-configuration-file.md).

## Customize Styles or Application Title

You can create a custom brand package to alter the colors used for the web portal. For more information, see [Branding the web portal](../branding-the-web-portal.md)

#### To modify application title

1. Log on using an account that is assigned **System Administrator** permissions on the report server.

2. Open Internet Explorer.

3. Enter the web portal URL. By default, it's https://\<**your-server-name**>/reports, but if you installed Reporting Services as a named instance, the default URL will be https://\<**your-server-name**>/reports\<**_instancename**>.

4. Select **Site Settings**.

5. On the **General** tab, in **Name**, replace **SQL Server Reporting Services** with a different name.

6. Select **Apply**.

## Next steps

[Web portal](../../reporting-services/web-portal-ssrs-native-mode.md)  
[Browser Support for Reporting Services](../../reporting-services/browser-support-for-reporting-services-and-power-view.md)
[Configure a URL](../../reporting-services/install-windows/configure-a-url-ssrs-configuration-manager.md)   
[Verify a Reporting Services Installation](../../reporting-services/install-windows/verify-a-reporting-services-installation.md)   
[Turn Reporting Services Features On or Off](../../reporting-services/report-server/turn-reporting-services-features-on-or-off.md)   
[Manage a Reporting Services Native Mode Report Server](../../reporting-services/report-server/manage-a-reporting-services-native-mode-report-server.md)   
[RsReportServer.config Configuration File](../../reporting-services/report-server/rsreportserver-config-configuration-file.md)   
[Configure a Native Mode Report Server for Local Administration](../../reporting-services/report-server/configure-a-native-mode-report-server-for-local-administration-ssrs.md)

 More questions? [Try asking the Reporting Services forum](https://go.microsoft.com/fwlink/?LinkId=620231)
