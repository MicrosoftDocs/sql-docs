---
title: "Configure and Administer a Report Server (Native Mode) | Microsoft Docs"
description: Learn about approaches that you can use to configure Reporting Services and find articles about how to configure components, features, or server capabilities.
ms.date: 05/15/2019
ms.service: reporting-services
ms.subservice: report-server


ms.topic: conceptual
helpviewer_keywords: 
  - "Reporting Services, components"
  - "deploying [Reporting Services], component options"
  - "report servers [Reporting Services], component options"
  - "configuration options [Reporting Services]"
  - "administering Reporting Services"
  - "components [Reporting Services], configuring"
  - "configuring servers [Reporting Services]"
ms.assetid: cfec012b-56f1-4346-9814-247acf22351c
author: maggiesMSFT
ms.author: maggies
---
# Configure and Administer a Report Server (SSRS Native Mode)
  This article summarizes the approaches that you can use to configure [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]. It also includes a list of topics that explain how to configure specific components, features, or server capabilities. To configure [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], you can:  
  
-   Use the Report Server Configuration Manager. Many of the topics in this section contain information about how to configure specific features through this tool.  
  
-   Use [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] to customize server properties, enable My Reports, enable trace logs, and set site-wide defaults. For more information about site settings, see [Reporting Services Report Server &#40;Native Mode&#41;](../../reporting-services/report-server/reporting-services-report-server-native-mode.md) for Management Studio. Note that you can create and run script that sets server properties programmatically. For more information, see [Script Deployment and Administrative Tasks](../../reporting-services/tools/script-deployment-and-administrative-tasks.md) and [Report Server System Properties](../../reporting-services/report-server-web-service/net-framework/reporting-services-properties-report-server-system-properties.md).  
  
-   Use the web portal to grant permissions to access the report server. Permissions are conveyed through role assignments that you define for each user or group account. For more information, see [Roles and Permissions &#40;Reporting Services&#41;](../../reporting-services/security/roles-and-permissions-reporting-services.md).  
  
-   Optionally, modify configuration files to change application settings. For more information about each file and guidelines for modifying them, see [Reporting Services Configuration Files](../../reporting-services/report-server/reporting-services-configuration-files.md).  
  
## In this section  
 [Configure Report Server URLs  &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/configure-report-server-urls-ssrs-configuration-manager.md)  
 Describes how to define the URLs used to access the report server and the web portal.  
  
 [Configure the Report Server Service Account &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/configure-the-report-server-service-account-ssrs-configuration-manager.md)  
 Provides recommendations and steps on how to modify service account and password.  
  
 [Create a Report Server Database  &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/ssrs-report-server-create-a-report-server-database.md)  
 Describes how to create a report server database, required for storing server metadata and objects.  
  
 [Configure a Report Server Database Connection  &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/configure-a-report-server-database-connection-ssrs-configuration-manager.md)  
 Describes how to modify the connection string used by the report server to connect to the report server database.  
  
 [E-Mail Delivery in Reporting Services](../install-windows/e-mail-settings-reporting-services-native-mode-configuration-manager.md)  
 Describes how to configure a report server to support e-mail report distribution.  
  
 [Configure the Unattended Execution Account &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/configure-the-unattended-execution-account-ssrs-configuration-manager.md)  
 Describes how to configure a user account to process reports in unattended mode.  
  
## See also  
 [Reporting Services Configuration Files](../../reporting-services/report-server/reporting-services-configuration-files.md)   
 [Report Server Configuration Manager &#40;Native Mode&#41;](../../reporting-services/install-windows/reporting-services-configuration-manager-native-mode.md)   
 [Reporting Services Security and Protection](../../reporting-services/security/reporting-services-security-and-protection.md)   
 [Reporting Services Report Server &#40;Native Mode&#41;](../../reporting-services/report-server/reporting-services-report-server-native-mode.md)  
  
  
