---
title: "Manage a Reporting Services native mode report server"
description: Refer to these articles when configuring a native mode report server, or when configuring a report server to run in SharePoint integrated mode.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: report-server
ms.topic: conceptual
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "Reporting Services Configuration tool"
  - "configuration options [Reporting Services]"
  - "report servers [Reporting Services], configuring"
---
# Manage a Reporting Services native mode report server
  This section contains procedures for configuring a native mode report server instance using the Report Server Configuration Manager.  
  
## In this section  
 The articles in this section are organized into categories so that you can more easily find the instructions you want. The first section contains articles for basic configuration tasks for a native mode report server. The second section contains advanced configuration articles. The third section contains articles for configuring a report server to run in SharePoint integrated mode.  
  
### Basic configuration  
 [Report Server Configuration Manager &#40;native mode&#41;](../../reporting-services/install-windows/reporting-services-configuration-manager-native-mode.md)  
 Provides steps for starting the Reporting Services Configuration tool.  
  
 [Configure a service account &#40;Report Server Configuration Manager&#41;](../install-windows/configure-the-report-server-service-account-ssrs-configuration-manager.md)  
 Explains how to specify account and password information for the Report Server service.  
  
 [Register a service principal name &#40;SPN&#41; for a report server](../../reporting-services/report-server/register-a-service-principal-name-spn-for-a-report-server.md)  
 Explains how to manually register an SPN for a report server that runs under a domain user account on a network that uses Kerberos authentication.  
  
 [Configure a URL  &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/configure-a-url-ssrs-configuration-manager.md)  
 Explains how to establish one or more URLs used to access the Report Server Web service and the web portal.  
  
 [Create a native mode report server database  &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/ssrs-report-server-create-a-native-mode-report-server-database.md)  
 Provides steps for creating a report server database. This step is required for deploying a Reporting Services installation.  
  
### Advanced or optional configuration  
 [Configure a native mode report server scale-out deployment &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/configure-a-native-mode-report-server-scale-out-deployment.md)  
 Provides steps for configuring multiple report servers to share a report server database.  
  
 [Email delivery in Reporting Services](../install-windows/e-mail-settings-reporting-services-native-mode-configuration-manager.md)   
 Provides steps for configuring a report server for e-mail distribution.  
  
 [Configure a firewall for report server access](../../reporting-services/report-server/configure-a-firewall-for-report-server-access.md)  
 Explains how to open ports used for inbound requests and outbound responses from a report server.  
  
 [Configure a native mode report server for local administration &#40;SSRS&#41;](../../reporting-services/report-server/configure-a-native-mode-report-server-for-local-administration-ssrs.md)  
 Describes other steps required to connect to the web portal or a report server using `https://localhost`.  
  
 [Configure a report server for remote administration](../../reporting-services/report-server/configure-a-report-server-for-remote-administration.md)  
 Explains how to configure a remote report server instance so that you can connect to and configure it from a different computer.  
  
 [Turn Reporting Services features on or off](../../reporting-services/report-server/turn-reporting-services-features-on-or-off.md)  
 Explains how to remove unused features in a Reporting Services installation.  
  
 [Enable remote errors &#40;Reporting Services&#41;](../../reporting-services/report-server/enable-remote-errors-reporting-services.md)  
 Explains how to set server properties on a report server to return additional information about error conditions that occur on remote servers.  
  
## Related content

- [Configure and administer a report server &#40;SSRS native mode&#41;](../../reporting-services/report-server/configure-and-administer-a-report-server-ssrs-native-mode.md)
- [Configuration and administration of a report server &#40;Reporting Services SharePoint mode&#41;](../../reporting-services/report-server-sharepoint/configuration-and-administration-of-a-report-server.md)
