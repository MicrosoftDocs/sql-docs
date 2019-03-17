---
title: "Web Service URL (SSRS Native Mode) | Microsoft Docs"
ms.custom: ""
ms.date: "04/27/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
f1_keywords: 
  - "SQL12.rsconfigtool.reportservervirtualdirectory.F1"
helpviewer_keywords: 
  - "Reporting Services, Web service"
ms.assetid: 9d210b5d-2a08-4e56-a4f5-c16715b00d79
author: markingmyname
ms.author: maghan
manager: craigg
---
# Web Service URL (SSRS Native Mode)
  Use the Web Service URL page to configure or modify the URL used to access the report server. A *URL reservation* will be created based on the URL you specify. The URL reservation defines the syntax and rules for all URLs that can be subsequently used to access the Report Server Web service. It specifies the prefix, host, port, and virtual directory for the Report Server Web service. Depending on how you specify the host, multiple URLs might be possible for a single reservation. The default value for the host specifies a strong wildcard. A strong wildcard allows you to specify in a URL any host name that can be resolved to the computer that hosts the report server. For more information about URL configuration and reservations, see [Configure a URL  &#40;SSRS Configuration Manager&#41;](../../reporting-services/install-windows/configure-a-url-ssrs-configuration-manager.md) and [Configure Report Server URLs  &#40;SSRS Configuration Manager&#41;](../../reporting-services/install-windows/configure-report-server-urls-ssrs-configuration-manager.md).  
  
 [!INCLUDE[applies](../../includes/applies-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Native mode.  
  
 To open this page, start the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager and click **Web Service URL** in the navigation pane. For more information, see [Reporting Services Configuration Manager &#40;Native Mode&#41;](../../../2014/sql-server/install/reporting-services-configuration-manager-native-mode.md).  
  
 This page provides values that are commonly used in report server URLs. If you want to create additional URLs, use host headers, or specify the IP address in a particular format, click **Advanced**.  
  
 A link to the Web service will appear on this page after you click **Apply**. If you click this link before the report server database is created, you can expect to see a "Page Not Found" error. This error will no longer appear once the database is configured. For more information, see [Create a Native Mode Report Server Database  &#40;SSRS Configuration Manager&#41;](../../reporting-services/install-windows/ssrs-report-server-create-a-native-mode-report-server-database.md).  
  
 If you reinstalled [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] and find that you get errors when attempting to use the default IP address value of All Assigned and port 80, you can usually resolve the error by re-creating the URL after restarting the service.  
  
## Options  
 **Virtual Directory**  
 Specifies the virtual directory name for the Report Server Web service. You can only have one virtual name for each Report Server Web service instance on the same computer.  
  
 **IP Address**  
 Identifies the report server computer on a TCP/IP network. Valid values include:  
  
-   **All Assigned** specifies that any of the IP addresses that are assigned to the computer can be used in a URL that points to a report server application. This value also encompasses friendly host names (such as computer names) that can be resolved by a domain name server to an IP address that is assigned to the computer. This is the default value for a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] URL.  
  
-   **All Unassigned** specifies that the report server will accept any request that does not have an exact match for the IP address or host name. Do not use this value if another Web application is already using it. If you do so, you will disrupt service for the other application.  
  
-   **127.0.0.1** is used to access to localhost. It supports local administration on the report server computer. If you select only this value, only users who are logged on locally to the report server computer will have access the application.  
  
-   *Nnn.nnn.nnn.nnn* is the IPv4 address of a network adapter card on your computer. If your network uses IPv6 addressing, the IP address will be a 128-bit value of 8 4-byte fields similar to the following format: \<header>:*nnnn:nnnn:nnnn:nnnn*  
  
     If you have multiple cards, you will see an IP address for each one. If you select only this value, it will limit application access to the just the IP address (and any host name that a domain name server maps to that address). You cannot use localhost to access a report server, and you cannot use the IP addresses of other network adapter cards that are installed on the report server computer.  
  
 **TCP Port**  
 Specifies the port that report server monitors for HTTP requests for URLs that include the report server virtual directory name.  
  
 **SSL Certificate**  
 Binds a certificate to the IP address you specified. The certificate must be installed and configured on the computer. Reporting a Services does not provide features for managing certificates. The certificate must be issued to a host name or a computer name that resolves to the IP address. For example, to use a certificate that was issued to http://salesreports, the IP address you specified must resolve to a server named "salesreports".  
  
 If you use a certificate, you must modify the `UrlRoot` configuration setting in the RSReportServer.config file so that it specifies the fully qualified name of the computer for which the certificate is registered. For more information, see [Configure SSL Connections on a Native Mode Report Server](../../reporting-services/security/configure-ssl-connections-on-a-native-mode-report-server.md) in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.  
  
 **SSL Port**  
 Specifies the port for SSL connections.  
  
 **URLs**  
 Displays the URLs defined for the current report server instance.  
  
 **Advanced**  
 Click to create additional URLs for the current application instance.  
  
> [!NOTE]
>  If you have existing SSL Bindings and URL Reservations and you want to change the SSL Binding, for example use a different certificate or hostheader, then it is recommended you complete the following steps in order:  
> 
>  1.  First remove all URL Reservations.  
> 2.  Then remove all SSL Bindings.  
> 3.  Then recreate the URLs and the SSL bindings.  
> 
>  The previous steps can be completed using [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager.  
> 
>  Microsoft Windows supports one binding for each IP address to Port combination. If you configure a report server to use a specific hostheader value and the certificate on the Port to IP address combination is also issued to a different hostheader value, you will see in your browser, a warning indicating the certificate does not match the URL that is being used.  
> 
>  To correct the issue, delete all bindings and then create new bindings with unique settings or configure the Reporting Services URL registrations with wildcards.  
  
## See Also  
 [Reporting Services Configuration Manager F1 Help Topics &#40;SSRS Native Mode&#41;](../../../2014/sql-server/install/reporting-services-configuration-manager-f1-help-topics-ssrs-native-mode.md)   
 [Configure Report Server URLs  &#40;SSRS Configuration Manager&#41;](../../reporting-services/install-windows/configure-report-server-urls-ssrs-configuration-manager.md)  
  
  
