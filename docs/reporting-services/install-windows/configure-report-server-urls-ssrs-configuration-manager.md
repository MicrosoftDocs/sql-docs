---
description: "Configure Report Server URLs  (Report Server Configuration Manager)"
title: "Configure Report Server URLs  (Configuration Manager) | Microsoft Docs"
ms.date: 05/18/2016
ms.service: reporting-services

ms.topic: conceptual
helpviewer_keywords: 
  - "Report Server Windows service, virtual directories"
  - "report servers [Reporting Services], virtual directories"
  - "virtual directories [Reporting Services]"
ms.assetid: a0134ef0-086c-443e-93b9-7213a3d76393
author: maggiesMSFT
ms.author: maggies
---
# Configure Report Server URLs  (Report Server Configuration Manager)
  In [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], URLs are used to access the Report Server Web service and the [!INCLUDE[ssRSWebPortal](../../includes/ssrswebportal.md)]. Before you can use either application, you must configure at least one URL each for the Web service and the [!INCLUDE[ssRSWebPortal](../../includes/ssrswebportal.md)]. [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] provides default values for both application URLs that work well in most deployment scenarios, including side-by-side deployments with other Web services and applications.  
  
-   If you installed the default configuration, URLs were created automatically using the default values.  
  
-   If you are using the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool to create or modify the URLs, you can accept the default values for a URL or specify custom values. A test link of the URL appears on page when you define the URL so that you can immediately confirm that the settings you specified result in a valid connection. For step-by-step instructions on how to configure and test a URL, see [Configure a URL  &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/configure-a-url-ssrs-configuration-manager.md).  
  
## Defining a Report Server URL  
 The URL precisely identifies the location of an instance of a report server application on your network. When you create a report server URL, you must specify the following parts.  
  
|Part|Description|  
|----------|-----------------|  
|Host name|A TCP/IP network uses an IP address to uniquely identify a device on the network. There is a physical IP address for each network adapter card installed in a computer. If the IP address resolves to a host header, you can specify the host header. If you are deploying the report server on a corporate network, you can use the network name of the computer.|  
|Port|A TCP port is an endpoint on the device. The report server will listen for requests on a designated port.|  
|Virtual directory|A port is often shared by multiple Web services or applications. For this reason, a report server URL always includes a virtual directory that corresponds to the application that gets the request. You must specify unique virtual directory names for each [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] application that listens on the same IP address and port.|  
|SSL settings|URLs in [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] can be configured to use an existing TLS/SSL certificate that you previously installed on the computer. For more information, see [Configure TLS Connections on a Native Mode Report Server](../../reporting-services/security/configure-ssl-connections-on-a-native-mode-report-server.md).|  
  
## Default URLs  
 When you access a report server or the [!INCLUDE[ssRSWebPortal](../../includes/ssrswebportal.md)] through its URL, the URL should include the host name and not the IP address. On a TCP/IP network, the IP address will resolve to a host name (or the network name of the computer). If you used the default values to configure URLs, you should be able to access the Report Server Web service using URLs that specify the computer name or localhost as the host name:  
  
-   `https://<computername>/reportserver`  
  
-   `https://localhost/reportserver`  
  
 The settings that make these URLs available appear in the following table. This table shows the default values that enable a report server connection though URLs that include a host name:  
  
|Part|Value|Explanation|  
|----------|-----------|-----------------|  
|IP address|All Assigned|The domain name service on your network resolves the host name on the URL to the computer's IP address. As long as the IP address is specified in the URL that you define, a request that is sent to a specific host will reach its intended target.|  
|Port|80|Port 80 is the default port for TCP/IP connections on a computer. Because the report server is listening on port 80, you can omit the port number from the URL. If you specify another port, you must specify it in the URL.|  
|Virtual directory|ReportServer|Notice that both of the example URLs includes the virtual directory name. Unless you customize the URL definition, you must always specify the application's virtual directory name on the URL.|  
  
> [!NOTE]  
>  An underlying URL reservation enables any valid host name to be used on a URL. The [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool creates a URL reservation in HTTP.SYS using syntax that allows variations of the host name to resolve to a particular report server instance. For more information about URL reservations, see [About URL Reservations and Registration  &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/about-url-reservations-and-registration-ssrs-configuration-manager.md).  
  
## Server-side Permissions on a Report Server URL  
 Permissions on each URL endpoint are granted exclusively to the Report Server service account. Only this account has rights to accept requests that are directed to the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] URLs. Discretionary Access Control Lists (DACLs) are created and maintained for the account when you configure the service identity through Setup or the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool. If you change the service account, the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool will update all URL reservations that you created to pick up the new account information. For more information, see [URL Reservation Syntax  &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/url-reservation-syntax-ssrs-configuration-manager.md).  
  
## Authenticating Client Requests Sent to a Report Server URL  
 By default, the authentication type supported on the URL endpoints is Windows Authentication. This is the default security extension. If you are implementing a custom or Forms authentication provider, you must modify the authentication settings on the report server. Optionally, you can also change the Windows Authentication settings to match the authentication subsystem used in your network. For more information, see [Authentication with the Report Server](../../reporting-services/security/authentication-with-the-report-server.md).  
  
## In This Section  
 [Configure a URL  &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/configure-a-url-ssrs-configuration-manager.md)  
 This topic provides instructions for setting and modifying a URL reservation in the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool.  
  
 [About URL Reservations and Registration  &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/about-url-reservations-and-registration-ssrs-configuration-manager.md)  
 URLs are used to access applications and reports. This topic explains the application URLs, the default URLs, and how URL reservations and registration work in [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)].  
  
 [URL Reservation Syntax  &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/url-reservation-syntax-ssrs-configuration-manager.md)  
 The default URL reservations that [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] uses are valid for most scenarios. However, if you want to restrict access or extend the deployment to enable Internet or extranet access, you might have to customize the settings to fit your requirements. This topic describes the syntax of a URL reservation and provides recommendations for creating custom reservations for your deployment.  
  
 [URLs in Configuration Files  &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/urls-in-configuration-files-ssrs-configuration-manager.md)  
 The RSReportServer.config file contains multiple entries for URL reservations and the URLs used by the [!INCLUDE[ssRSWebPortal](../../includes/ssrswebportal.md)] and report server e-mail delivery. This topic summarizes the URL configuration settings so that you can understand how they compare.  
  
 [URL Reservations for Multi-Instance Report Server Deployments  &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/url-reservations-for-multi-instance-report-server-deployments.md)  
 When you install multiple instances of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] on a single computer, you increase the probability of encountering URL duplication when a URL is registered. To avoid these errors, follow the recommendations in this topic for creating instance-specific URL reservations.  
  
## See Also  
 [Configure a URL  &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/configure-a-url-ssrs-configuration-manager.md) 
