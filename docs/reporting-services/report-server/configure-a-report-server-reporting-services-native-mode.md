---
title: "Configure a report server (Reporting Services native mode)"
description: Learn about another configuration for SQL Server Report Server, which depends on options you chose during installation.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: report-server
ms.topic: conceptual
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "report server configuration"
  - "report servers [Reporting Services], configuring"
---
# Configure a report server (Reporting Services native mode)
  Depending on options you selected during installation, the Report Server might require more configuration before you can use it. At a minimum, a report server configuration consists of the following items:  
  
-   A Report Server service account (configured during installation).  
  
-   A Web service URL that provides access to the report server.  
  
-   A report server database that stores application data, reports, and other items.  
  
 Setup configures the minimum settings if you select either of the following installation options: Native mode default configuration or SharePoint integrated mode default configuration. If you installed the report server in files-only mode with the **Install but do not configure** option in the Installation wizard, only the service account is configured. The Web service URL and report server database must be configured after Setup is finished.  
  
You should configure web portal so that you can grant user access to the report server and manage report server content. If you deploy a report server in SharePoint integrated mode, use the Web front end of a SharePoint server to grant access.  
  
 Other features, such as report server e-mail and the unattended execution account, can be configured as needed. For more information, see [Manage a Reporting Services native mode report server](../../reporting-services/report-server/manage-a-reporting-services-native-mode-report-server.md).  
  
 To configure a report server, use the Reporting Services Configuration tool.  
  
## Minimally configure a report server installation  
  
1.  Start the Reporting Services Configuration tool and connect to the report server instance. For instructions, see [Report Server Configuration Manager &#40;native mode&#41;](../../reporting-services/install-windows/reporting-services-configuration-manager-native-mode.md).  
  
2.  Select **Web Service URL** to open the page for configuring a URL for the report server. For instructions on how to define the URL, see [Configure a URL  &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/configure-a-url-ssrs-configuration-manager.md).  
  
3.  Select **Database** to create the report server database. For instructions, see [Create a native mode report server database  &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/ssrs-report-server-create-a-native-mode-report-server-database.md).  
  
4.  Go back to the **Web Service URL** page and select the URL to verify it works.  
  
5.  Follow the instructions in the next section to complete your deployment.  
  
## Complete your deployment
  
 To complete your deployment, you should configure the web portal or SharePoint integration. For more information, see [Configure the web portal](../../reporting-services/report-server/configure-web-portal.md).  
  
 If Windows Firewall is turned on, the port that the report server is configured to use is most likely closed. One indication that a port might be closed is a blank page when you attempt to open the web portal from a remote client computer. For information on configuring the firewall, see [Configure a firewall for report server access](../../reporting-services/report-server/configure-a-firewall-for-report-server-access.md).  
  
 If you're using Windows Vista or Windows Server 2008, more steps are required before you can open the web portal locally. For more information, see [Configure a native mode report server for local administration &#40;SSRS&#41;](../../reporting-services/report-server/configure-a-native-mode-report-server-for-local-administration-ssrs.md).  
  
 Verify your installation by creating folders, uploading items, and running reports. Follow the instructions in [Verify a Reporting Services installation](../../reporting-services/install-windows/verify-a-reporting-services-installation.md) to verify your installation.  
  
## Related content

- [Manage a Reporting Services native mode report server](../../reporting-services/report-server/manage-a-reporting-services-native-mode-report-server.md)
- [Configure a firewall for report server access](../../reporting-services/report-server/configure-a-firewall-for-report-server-access.md)
- [Configure a native mode report server for local administration &#40;SSRS&#41;](../../reporting-services/report-server/configure-a-native-mode-report-server-for-local-administration-ssrs.md)
- [Configure a report server for remote administration](../../reporting-services/report-server/configure-a-report-server-for-remote-administration.md)
- [Report Server Configuration Manager &#40;native mode&#41;](../../reporting-services/install-windows/reporting-services-configuration-manager-native-mode.md)
