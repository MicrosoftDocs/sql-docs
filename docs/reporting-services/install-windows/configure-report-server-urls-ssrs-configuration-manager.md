---
title: "Configure report server URLs (Report Server Configuration Manager)"
description: Learn about how you can configure URLs by using Reporting Services to access the Report Server web service and web portal.
author: maggiesMSFT
ms.author: maggies
ms.date: 08/02/2024
ms.service: reporting-services
ms.subservice: report-server
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "Report Server Windows service, virtual directories"
  - "report servers [Reporting Services], virtual directories"
  - "virtual directories [Reporting Services]"
#customer intent: As a SQL Server administrator, I want to understand how to configure report server URLs so that I can ensure proper access to the Report Server web service and the web portal.
---
# Configure report server URLs (Report Server Configuration Manager)

Learn how [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] uses URLs to access the Report Server web service and the [!INCLUDE[ssRSWebPortal](../../includes/ssrswebportal.md)]. Before you use either application, configure at least one URL each for the web service and the [!INCLUDE[ssRSWebPortal](../../includes/ssrswebportal.md)]. [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] provides default values for both URLs that work in most deployment scenarios, including side-by-side deployments with other web services and applications.

## URL configuration

When you install SQL Server Reporting Services (SSRS), URLs for accessing the Report Server web service and the web portal are set up to facilitate initial and ongoing use. This configuration occurs in two primary ways:

- **Default configuration**: If you select the default configuration during SSRS installation, the system automatically creates URLs by using default values that are suitable for most deployment scenarios. These URLs allow immediate access to the Report Server web service and the web portal without requiring more configuration.

- **Custom configuration**: You can use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool to create or modify URLs after initial setup. The tool allows you to accept the default values for a URL or specify custom values. A test link of the URL appears on page when you define the URL so that you can verify that your settings result in a valid connection. For step-by-step instructions on how to configure and test a URL, see [Configure a URL (Report Server Configuration Manager)](../../reporting-services/install-windows/configure-a-url-ssrs-configuration-manager.md).

## Define a report server URL

The URL identifies the location of a report server instance on your network. The following table shows the parts you specify when you create a report server URL.

|Part|Description|
|----------|-----------------|
|Host name|A TCP/IP network uses an IP address to uniquely identify a device on the network. There's a physical IP address for each network adapter card installed in a computer. If the IP address resolves to a host header, you can specify the host header. If you deploy the report server on a corporate network, you can use the network name of the computer.|
|Port|A Transmission Control Protocol (TCP) port is an endpoint on the device. The report server listens for requests on a designated port.|
|Virtual directory|Multiple web services or applications often share a port. A report server URL always includes a virtual directory that corresponds to the application that receives the request. Specify unique virtual directory names for each [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] application that listens on the same IP address and port.|
|SSL settings|URLs in [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] can be configured to use an existing TLS/SSL certificate installed on the computer. For more information, see [Configure TLS connections on a native mode report server](../../reporting-services/security/configure-ssl-connections-on-a-native-mode-report-server.md).|

## Default URLs

 When you access a report server or the [!INCLUDE[ssRSWebPortal](../../includes/ssrswebportal.md)] through its URL, include the host name instead of the IP address. On a TCP/IP network, the IP address resolves to a host name or the network name of the computer. If you used the default values to configure URLs, you can access the Report Server web service by using URLs that specify the computer name or localhost as the host name:

- `https://<computername>/reportserver`
- `https://localhost/reportserver`

 The following table shows the default values that enable a report server connection through URLs that include a host name:

|Part|Value|Explanation|
|----------|-----------|-----------------|
|IP address|All Assigned|The domain name service on your network resolves the host name in the URL to the computer's IP address. A request reaches its intended host as long as you specify the IP address in the URL.|
|Port|80|Port 80 is the default port for TCP/IP connections. Because the report server listens on port 80, you can omit the port number from the URL. If you specify another port, include it in the URL.|
|Virtual directory|ReportServer|Both example URLs include the virtual directory name. Unless you customize the URL definition, always specify the application's virtual directory name on the URL.|

> [!NOTE]
> An underlying URL reservation enables any valid host name to be used on a URL. The [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool creates a URL reservation in `HTTP.SYS` by using syntax that allows variations of the host name to resolve to a particular report server instance. For more information about URL reservations, see [About URL reservations and registration (Report Server Configuration Manager)](../../reporting-services/install-windows/about-url-reservations-and-registration-ssrs-configuration-manager.md).

## Server-side permissions on a report server URL

Permissions on each URL endpoint are granted exclusively to the Report Server service account. Only this account can accept requests directed to the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] URLs. Discretionary Access Control Lists (DACLs) are created and maintained for the account when you configure the service identity through setup or the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool. If you change the service account, the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool updates all URL reservations that you create to use the new account information. For more information, see [URL reservation syntax &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/url-reservation-syntax-ssrs-configuration-manager.md).

## Authenticate client requests sent to a report server URL

By default, Windows Authentication is the authentication type supported on the URL endpoints. This setting is the default security extension. If you implement a custom or Forms authentication provider, modify the authentication settings on the report server. You can also change the Windows Authentication settings to match the authentication subsystem used in your network. For more information, see [Authentication in a report server](../../reporting-services/security/authentication-with-the-report-server.md).

## Related content

- [URLs in configuration files (Report Server Configuration Manager)](../../reporting-services/install-windows/urls-in-configuration-files-ssrs-configuration-manager.md)
- [URL reservations for multi-instance report server deployments (Report Server Configuration Manager)](../../reporting-services/install-windows/url-reservations-for-multi-instance-report-server-deployments.md)
