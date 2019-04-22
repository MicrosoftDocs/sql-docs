---
title: "Configure a Report Server (Reporting Services Native Mode) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "report server configuration"
  - "report servers [Reporting Services], configuring"
ms.assetid: ef84ce9d-9156-48e9-8073-7e0535476932
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Configure a Report Server (Reporting Services Native Mode)
  Depending on options you selected during installation, the Report Server might require additional configuration before you can use it. At a minimum, a report server configuration consists of the following:  
  
-   A Report Server service account (configured during installation).  
  
-   A Web service URL that provides access to the report server.  
  
-   A report server database that stores application data, reports, and other items.  
  
 Setup configures the minimum settings if you select either of the following installation options: Native mode default configuration or SharePoint integrated mode default configuration. If you installed the report server in files-only mode (this is the **Install but do not configure** option in the Installation wizard), only the service account is configured. The Web service URL and report server database must be configured after Setup is finished.  
  
 Report Manager is an optional feature for a native mode report server, but it is recommended that you configure Report Manager so that you can grant user access to the report server and manage report server content. If you deploy a report server in SharePoint integrated mode, use the Web front-end of a SharePoint server to grant access.  
  
 Additional features, such as report server e-mail and the unattended execution account, can be configured as needed. For more information, see [Manage a Reporting Services Native Mode Report Server](manage-a-reporting-services-native-mode-report-server.md).  
  
 To configure a report server, use the Reporting Services Configuration tool.  
  
### To minimally configure a report server installation  
  
1.  Start the Reporting Services Configuration Manager and connect to the report server instance. For instructions, see [Reporting Services Configuration Manager &#40;Native Mode&#41;](../../sql-server/install/reporting-services-configuration-manager-native-mode.md).  
  
2.  Click **Web Service URL** to open the page for configuring a URL for the report server. For instructions on how to define the URL, see [Configure a URL  &#40;SSRS Configuration Manager&#41;](../install-windows/configure-a-url-ssrs-configuration-manager.md).  
  
3.  Click **Database** to create the report server database. For instructions, see [Create a Native Mode Report Server Database  &#40;SSRS Configuration Manager&#41;](../install-windows/ssrs-report-server-create-a-native-mode-report-server-database.md).  
  
4.  Go back to the **Web Service URL** page and click the URL to verify it works.  
  
5.  Follow the instructions in "Next Steps" to complete your deployment.  
  
## Next Steps  
 To complete your deployment, you should configure Report Manager or SharePoint integration. For more information, see [Configure Report Manager &#40;Native Mode&#41;](configure-web-portal.md).  
  
 If Windows Firewall is turned on, the port that the report server is configured to use is most likely closed. One indication that a port might be closed is a blank page when you attempt to open Report Manager from a remote client computer. For information on configuring the firewall, see [Configure a Firewall for Report Server Access](configure-a-firewall-for-report-server-access.md).  
  
 If you are using Windows Vista or Windows Server 2008, additional steps are required before you can open Report Manager locally. For more information, see [Configure a Native Mode Report Server for Local Administration &#40;SSRS&#41;](configure-a-native-mode-report-server-for-local-administration-ssrs.md).  
  
 Verify your installation by creating folders, uploading items, and running reports. Follow the instructions in [Verify a Reporting Services Installation](../install-windows/verify-a-reporting-services-installation.md) to verify your installation.  
  
## See Also  
 [Manage a Reporting Services Native Mode Report Server](manage-a-reporting-services-native-mode-report-server.md)   
 [Configure a Firewall for Report Server Access](configure-a-firewall-for-report-server-access.md)   
 [Configure a Native Mode Report Server for Local Administration &#40;SSRS&#41;](configure-a-native-mode-report-server-for-local-administration-ssrs.md)   
 [Configure a Report Server for Remote Administration](configure-a-report-server-for-remote-administration.md)   
 [Reporting Services Configuration Manager &#40;Native Mode&#41;](../../sql-server/install/reporting-services-configuration-manager-native-mode.md)  
  
  
