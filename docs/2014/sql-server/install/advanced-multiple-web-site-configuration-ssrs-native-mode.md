---
title: "Advanced Multiple Web Site Configuration (SSRS Native Mode) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
f1_keywords: 
  - "SQL12.rsconfigtool.advancedmultiplewebsiteconfig.F1"
ms.assetid: af4ede43-2225-45b5-ae7e-9202411551ba
author: markingmyname
ms.author: maghan
manager: craigg
---
# Advanced Multiple Web Site Configuration (SSRS Native Mode)
  Use this dialog box to create and manage the URLs used to access a report server or Report Manager. The **Advanced Multiple Web Site Configuration** dialog box is used to create additional URLs, custom URLs that include a host header name, or specify an IP address in the IPv4 or IPv6 format.  
  
 [!INCLUDE[applies](../../includes/applies-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Native mode.  
  
 Creating multiple URLs is useful if you want to configure different ways to access a report server. For example, report server access over intranet and extranet connection typically requires that you have different URLs for each type of connection.  
  
 To open the **Advanced Multiple Web Site Configuration** dialog box, click **Advanced** on the **Web Service URL** or the **Report Manager URL** page in the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager. After the **Advanced Multiple Web Site Configuration** dialog box is open, you can click **Add** or **Edit** to define new URLs or modify or delete existing URLs.  
  
 Click **OK** to save your changes. If you add or remove URLs, but then close the dialog box without first clicking **OK**, your changes will be lost.  
  
## Options  
 **IP Address**  
 Identifies the report server computer on a TCP/IP network. Valid values include:  
  
-   **All Assigned** specifies that any of the IP addresses that are assigned to the computer can be used in a URL that points to a report server application. This value also encompasses friendly host names (such as computer names) that can be resolved by a domain name server to an IP address that is assigned to the computer. This is the default value for a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] URL.  
  
-   **All Unassigned** specifies that the report server will accept any request that does not have an exact match for the IP address or host name. Do not use this value if another Web application is already using it. If you do so, you will disrupt service for the other application.  
  
-   **127.0.0.1** is used to access to localhost. It supports local administration on the report server computer. If you select only this value, only users who are logged on locally to the report server computer will have access the application.  
  
-   *Nnn.nnn.nnn.nnn* is the IPv4 address of a network adapter card on your computer. If your network uses IPv6 addressing, the IP address will be a 128-bit value of 8 4-byte fields similar to the following format: \<header>:*nnnn:nnnn:nnnn:nnnn*.  
  
     If you have multiple cards, you will see an IP address for each one. If you select only this value, it will limit application access to the just the IP address (and any host name that a domain name server maps to that address). You cannot use localhost to access a report server, and you cannot use the IP addresses of other network adapter cards that are installed on the report server computer.  
  
 **Port**  
 Specifies the port that report server monitors for requests. Port 80 is the default port. If you use port 80, you do not have to include it in the URL. If you use any other port number, you must always include it in the URL (for example, http://localhost:8181/reports).  
  
 **Host Header**  
 If you already have a host header defined on a domain name server that resolves to your computer, you can specify that host header in a URL that you configure for report server access.  
  
 A host header is a unique name that allows multiple Web sites to share a single IP address and port. Host header names are easier to remember and type than IP address and port numbers. An example of a host header name might be www.adventure-works.com.  
  
 **SSL Port**  
 Specifies the port for SSL connections. The default port for SSL is 443.  
  
 **SSL Certificate**  
 Specifies the certificate name of an SSL certificate that you installed on this computer. If the certificate maps to a wildcard, you can use it for a report server connection.  
  
 Specifies the fully qualified computer name for which the certificate is registered. The name that you specify must be identical to the name for which the certificate is registered.  
  
 You must have a certificate installed to use this option. You must also modify the UrlRoot configuration setting in the RSReportServer.config file so that it specifies the fully qualified name of the computer for which the certificate is registered. For more information, see [Configure SSL Connections on a Native Mode Report Server](../../reporting-services/security/configure-ssl-connections-on-a-native-mode-report-server.md) in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.  
  
 **Issued To**  
 Shows the name of the computer for which the certificate was created.  
  
 **Add**  
 Define an additional URL.  
  
 **Edit**  
 Modify any part of the URL syntax.  
  
 **Remove**  
 Clear a URL entry from the list.  
  
## See Also  
 [Reporting Services Configuration Manager &#40;Native Mode&#41;](../../../2014/sql-server/install/reporting-services-configuration-manager-native-mode.md)   
 [Configure a URL  &#40;SSRS Configuration Manager&#41;](../../reporting-services/install-windows/configure-a-url-ssrs-configuration-manager.md)   
 [Configure Report Server URLs  &#40;SSRS Configuration Manager&#41;](../../reporting-services/install-windows/configure-report-server-urls-ssrs-configuration-manager.md)  
  
  
