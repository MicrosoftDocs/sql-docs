---
title: "Manage a Reporting Services Native Mode Report Server | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "Reporting Services Configuration tool"
  - "configuration options [Reporting Services]"
  - "report servers [Reporting Services], configuring"
ms.assetid: 6ca03a09-d6a8-4c93-ba12-1c99dcbfb618
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Manage a Reporting Services Native Mode Report Server
  This section contains procedures for configuring a native mode report server instance using the Reporting Services Configuration Manager.  
  
## In This Section  
 The topics in this section are organized into categories so that you can more easily find the instructions you want. The first section contains topics for basic configuration tasks for a native mode report server. The second section contains advanced configuration topics. The third section contains topics for configuring a report server to run in SharePoint integrated mode.  
  
### Basic Configuration  
 [Reporting Services Configuration Manager &#40;Native Mode&#41;](../../sql-server/install/reporting-services-configuration-manager-native-mode.md)  
 Provides steps for starting the Reporting Services Configuration tool.  
  
 [Configure a Service Account &#40;SSRS Configuration Manager&#41;](../../sql-server/install/configure-a-service-account-ssrs-configuration-manager.md)  
 Explains how to specify account and password information for the Report Server service.  
  
 [Register a Service Principal Name &#40;SPN&#41; for a Report Server](register-a-service-principal-name-spn-for-a-report-server.md)  
 Explains how to manually register an SPN for a report server that runs under a domain user account on a network that uses Kerberos authentication.  
  
 [Configure a URL  &#40;SSRS Configuration Manager&#41;](../install-windows/configure-a-url-ssrs-configuration-manager.md)  
 Explains how to establish one or more URLs used to access the Report Server Web service and Report Manager.  
  
 [Create a Native Mode Report Server Database  &#40;SSRS Configuration Manager&#41;](../install-windows/ssrs-report-server-create-a-native-mode-report-server-database.md)  
 Provides steps for creating a report server database. This step is required for deploying a Reporting Services installation.  
  
### Advanced or Optional Configuration  
 [Configure a Native Mode Report Server Scale-Out Deployment &#40;SSRS Configuration Manager&#41;](../install-windows/configure-a-native-mode-report-server-scale-out-deployment.md)  
 Provides steps for configuring multiple report servers to share a report server database.  
  
 [Configure a Report Server for E-Mail Delivery &#40;SSRS Configuration Manager&#41;](../../sql-server/install/configure-a-report-server-for-e-mail-delivery-ssrs-configuration-manager.md)  
 Provides steps for configuring a report server for e-mail distribution.  
  
 [Configure a Firewall for Report Server Access](configure-a-firewall-for-report-server-access.md)  
 Explains how to open ports used for inbound requests and outbound responses from a report server.  
  
 [Configure a Native Mode Report Server for Local Administration &#40;SSRS&#41;](configure-a-native-mode-report-server-for-local-administration-ssrs.md)  
 Describes additional steps required to connect to Report Manager or a report server using http://localhost.  
  
 [Configure a Report Server for Remote Administration](configure-a-report-server-for-remote-administration.md)  
 Explains how to configure a remote report server instance so that you can connect to and configure it from a different computer.  
  
 [Turn Reporting Services Features On or Off](turn-reporting-services-features-on-or-off.md)  
 Explains how to remove unused features in a Reporting Services installation.  
  
 [Enable Remote Errors &#40;Reporting Services&#41;](enable-remote-errors-reporting-services.md)  
 Explains how to set server properties on a report server to return additional information about error conditions that occur on remote servers.  
  
## See Also  
 [Configure and Administer a Report Server &#40;SSRS Native Mode&#41;](configure-and-administer-a-report-server-ssrs-native-mode.md)   
 [Configuration and Administration of a Report Server &#40;Reporting Services SharePoint Mode&#41;](../configure-administer-report-server-reporting-services-sharepoint-mode.md)  
  
  
