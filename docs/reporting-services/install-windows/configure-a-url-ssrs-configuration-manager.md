---
description: "Configure a URL  (Report Server Configuration Manager)"
title: "Configure a URL  (Configuration Manager) | Microsoft Docs"
ms.date: 05/26/2016
ms.service: reporting-services

ms.topic: conceptual
helpviewer_keywords: 
  - "URL access [Reporting Services], syntax"
ms.assetid: 851e163a-ad2a-491e-bc1e-4df92327092f
author: maggiesMSFT
ms.author: maggies
---
# Configure a URL  (Report Server Configuration Manager)
  Before you can use the [!INCLUDE[ssRSWebPortal](../../includes/ssrswebportal.md)] or the Report Server Web service, you must configure at least one URL for each application. Configuring the URLs is mandatory if you installed [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] in "files-only" mode (that is, by selecting the **Install but do not configure the server** option on the Report Server Installation Options page in the Installation Wizard). If you installed [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] in the default configuration, URLs are already configured for each application.  
  
 Use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool to configure the URLs. All parts of the URL are defined in this tool. Unlike earlier releases, Internet Information Services (IIS) Web sites no longer provide access to [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] applications in [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] and later versions.  
  
 [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] provides default values that work well in most deployment scenarios, including side-by-side deployments with other Web services and applications. Default URLs incorporate instance names, minimizing the risk of URL conflicts if you run multiple report server instances on the same computer.  
  
 This topic provides instructions for the following tasks:  
  
-   Create a URL for the Report Server Web service.  
  
-   Create a URL for the [!INCLUDE[ssRSWebPortal](../../includes/ssrswebportal.md)].  
  
-   Set advanced URL properties to define additional URLs.  
  
 For more information about how URLs are stored and maintained or interoperability issues, see [About URL Reservations and Registration  &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/about-url-reservations-and-registration-ssrs-configuration-manager.md) and [Install Reporting Services and Internet Information Services Side-by-Side &#40;SSRS Native Mode&#41;](../../reporting-services/install-windows/install-reporting-and-internet-information-services-side-by-side.md). To review examples of URLs often used in a Reporting Services installation, see [Examples of URLs](#URLExamples) in this topic.  
  
## Prerequisites  
 Before you create or modify a URL, remember the following points:  
  
-   You must be a member of the local Administrators group on the report server computer.  
  
-   If IIS is installed on the same computer, check the names of virtual directories on any Web site that uses port 80. If you see any virtual directories that use the default Reporting Services virtual directory names (that is, "Reports" and "ReportServer"), choose different virtual directory names for the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] URLs that you configure.  
  
-   You must use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool to configure the URL. Do not use a system utility. Never modify URL reservations in the **URLReservations** section of the RSReportServer.config file directly. Using the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool is necessary to update both the underlying URL reservation that is stored internally and synchronize the URL settings stored in the RSReportServer.config file.  
  
-   Choose a time that has low report activity. Each time the URL reservation changes, you can expect that the application domains for Report Server Web service and the [!INCLUDE[ssRSWebPortal](../../includes/ssrswebportal.md)] might be recycled.  
  
-   For an overview of URL construction and usage in [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], see [Configure Report Server URLs  &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/configure-report-server-urls-ssrs-configuration-manager.md).  
  
### To configure a URL for the Report Server Web service  
  
1.  Start the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool and connect to a local report server instance.  
  
2.  Click **Web Service URL**.  
  
3.  Specify the virtual directory. The virtual directory name identifies which application receives the request. Because an IP address and port can be shared by multiple applications, the virtual directory name specifies which application receives the request.  
  
     This value must be unique to ensure that the request reaches its intended destination. This value is required. It is case-insensitive. There is a one-to-one correspondence between a virtual directory name and an instance of a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] application. If you create multiple URLs to the same application instance, you must use the same virtual directory name in all of the URLs you define for this application instance.  
  
     For the Report Server Web service, the default virtual directory name is **ReportServer**.  
  
4.  Specify the IP address that uniquely identifies the report server computer on the network. If you want to specify a host header or define additional URLs for the same application instance, you must click **Advanced**. For instructions on how to set advanced properties on the URL, see the instructions later in this topic. Otherwise, use the **Web Service URL** page to select from the following values:  
  
    -   **All Assigned** specifies that any of the IP addresses that are assigned to the computer can be used in a URL that points to a report server application. This value also encompasses friendly host names (such as computer names) that can be resolved by a domain name server to an IP address that is assigned to the computer. This is the default value for a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] URL.  
  
    -   **All Unassigned** specifies that the report server will receive any request that has not been handled by another application. We recommend that you avoid this option. If you select this option, it becomes possible for another application that has a stronger URL reservation to intercept requests intended for the report server.  
  
    -   **127.0.0.1** is the IPv4 address used to access to localhost. It supports local administration on the report server computer. If you select only this value, only users who are logged on locally to the report server computer will have access to the application.  
  
    -   **::1** is the loopback address in IPv6 format.  
  
    -   Specific IP addresses also appear in this list. IP addresses can be in IPv4 and IPv6 formats. *Nnn.nnn.nnn.nnn* is the 32-bit IPv4 address of a network adapter card on your computer. IPv6 addresses are 128-bit, with eight 4-byte fields separated by colons: \<prefix>:*nnnn:nnnn:nnnn:nnnn:nnnn:nnnn*  
  
         If you have multiple cards or if your network supports both IPv4 and IPv6 addresses, you will see multiple IP addresses. If you select only one IP address, it will limit application access to the just the IP address (and any host name that a domain name server maps to that address). You cannot use localhost to access a report server, and you cannot use the IP addresses of other network adapter cards that are installed on the report server computer. Typically, if you select this value, it is because you are configuring multiple URL reservations that also specify explicit IP addresses or host names (for example, one for a network adapter card used for intranet connections and a second one used for extranet connections).  
  
5.  Specify the port. Port 80 is the default because it can be shared with other applications. If you want to use a custom port number, remember that you will have to always specify it in the URL used to access the report server. You can use the following techniques to find an available port:  
  
    -   From a command prompt, type the following command to return a list of TCP ports that are being used:  
  
         `netstat -anp tcp`  
  
    -   Review the Microsoft Support article, [Information about TCP/IP port assignments](https://www.betaarchive.com/wiki/index.php?title=Microsoft_KB_Archive/174904), to read about TCP port assignments and the differences between Well Known Ports (0 through 1023), Registered Ports (1024 through 49151), and Dynamic or Private Ports (49152 through 65535).  
  
    -   If you are using Windows Firewall, you must open the port. For instructions, see [Configure a Firewall for Report Server Access](../../reporting-services/report-server/configure-a-firewall-for-report-server-access.md).  
  
6.  If you have not done so already, verify that IIS (if it is installed) does not have virtual directory with the same name you plan to use.  
  
7.  If you installed a TLS/SSL certificate, you can select it now to bind the URL to the TLS/SSL certificate that is installed on your computer.  
  
8.  Optionally, if you select a TLS/SSL certificate, you can specify a custom port. The default is 443 but you can use any port that is available.  
  
9. Click **Apply** to create the URL.  
  
10. Test the URL by clicking the link in the **URLs** section of page. Note that the report server database must be created and configured before you can test the URL. For instructions, see [Create a Native Mode Report Server Database  &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/ssrs-report-server-create-a-native-mode-report-server-database.md).  

> [!NOTE]
>  If you have existing TLS Bindings and URL Reservations and you want to change the TLS Binding, for example use a different certificate or hostheader, then it is recommended you complete the following steps in order:  
> 
>  1.  First remove all URL Reservations.  
> 2.  Then remove all TLS Bindings.  
> 3.  Then recreate the URLs and the TLS bindings.  
> 
>  The previous steps can be completed using [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager.  
> 
>  Microsoft Windows supports one binding for each IP address to Port combination. If you configure a report server to use a specific hostheader value and the certificate on the Port to IP address combination is also issued to a different hostheader value, you will see in your browser, a warning indicating the certificate does not match the URL that is being used.  
> 
>  To correct the issue, delete all bindings and then create new bindings with unique settings or configure the Reporting Services URL registrations with wildcards.
  
### To create a URL reservation for the [!INCLUDE[ssRSWebPortal](../../includes/ssrswebportal.md)]  
  
1.  Start the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool and connect to the report server instance.  
  
2.  Click **Web Portal URL**.  
  
3.  Specify the virtual directory. The [!INCLUDE[ssRSWebPortal](../../includes/ssrswebportal.md)] listens on the same IP address and port as the Report Server Web service. If you configured the [!INCLUDE[ssRSWebPortal](../../includes/ssrswebportal.md)] to point to a different Report Server Web service, you must modify the [!INCLUDE[ssRSWebPortal](../../includes/ssrswebportal.md)] URL settings in the RSReportServer.config file.  
  
4.  If you installed a TLS/SSL certificate, you can select it to require that all requests to the [!INCLUDE[ssRSWebPortal](../../includes/ssrswebportal.md)] are routed over HTTPS.  
  
     Optionally, if you select a TLS/SSL certificate, you can specify a custom port. The default is 443 but you can use any port that is available.  
  
5.  Click **Apply** to create the URL.  
  
6.  Test the URL by clicking the link in the **URLs** section of page.  
  
## Setting Advanced Properties to Specify Additional URLs  
 You can reserve multiple URLs for the Report Server Web service or the [!INCLUDE[ssRSWebPortal](../../includes/ssrswebportal.md)] by specifying different ports or host names (either an IP address or a host header name that a domain name server can resolve to an IP address assigned to the computer). By creating multiple URLs, you can set up different access paths to the same report server instance. For example, to enable intranet and extranet access to a report server, you might use the default URL for access across the intranet, and an additional fully qualified host name for extranet access:  
  
-   `https://myserver01/reportserver`  
  
-   `https://www.adventure-works.com/reportserver`  
  
 You cannot set multiple virtual directory names for the same application instance. Each [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] application instance is mapped to a single virtual directory name. If you have multiple instances of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] on the same computer, the virtual directory name for an application should include the instance name to ensure that each request reaches its intended target.  
 
  **Host Header**  
 If you already have a host header defined on a domain name server that resolves to your computer, you can specify that host header in a URL that you configure for report server access.  
  
 A host header is a unique name that allows multiple Web sites to share a single IP address and port. Host header names are easier to remember and type than IP address and port numbers. An example of a host header name might be www.adventure-works.com.  
  
 **SSL Port**  
 Specifies the port for TLS/SSL connections. The default port for TLS is 443.  
  
 **SSL Certificate**  
 Specifies the certificate name of a TLS/SSL certificate that you installed on this computer. If the certificate maps to a wildcard, you can use it for a report server connection.  
  
 Specifies the fully qualified computer name for which the certificate is registered. The name that you specify must be identical to the name for which the certificate is registered.  
  
 You must have a certificate installed to use this option. You must also modify the UrlRoot configuration setting in the RSReportServer.config file so that it specifies the fully qualified name of the computer for which the certificate is registered. For more information, see [Configure TLS Connections on a Native Mode Report Server](../../reporting-services/security/configure-ssl-connections-on-a-native-mode-report-server.md).  
  
### To set advanced properties on a URL  
  
1.  On either the **Web Service URL** or **Web Portal URL** page, click **Advanced**.  
  
2.  Click **Add**.  
  
3.  Click IP Address or Host Header Name. If you specify a host header, be sure to specify a name that the DNS service can resolve. If you are specifying publicly available domain name, include the whole URL, including `https://www`.  
  
4.  Specify the port. If you specify a custom port, the URL to the application must always include the port number.  
  
5.  Click **OK**.  
  
6.  Test the URL by opening a browser window and entering the URL.  
  
## URLs for Multiple Report Server Instances on the Same Computer  
 If you are reserving URLs for multiple instances of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], you should follow naming conventions so that you can avoid naming conflicts. For more information, see [URL Reservations for Multi-Instance Report Server Deployments  &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/url-reservations-for-multi-instance-report-server-deployments.md).  
  
##  <a name="URLExamples"></a> Examples of URL Configurations  
 The following list shows some examples of what a report server URL might resemble:  
  
-   `https://localhost/reportserver`  
  
-   `https://localhost/reportserver_SQLEXPRESS`  
  
-   `https://sales01/reportserver`  
  
-   `https://sales01:8080/reportserver`  
  
-   `https://sales.adventure-works.com/reportserver`  
  
-   `https://www.adventure-works.com:8080/reportserver01`  
  
 URLs that you use to access the [!INCLUDE[ssRSWebPortal](../../includes/ssrswebportal.md)] share a similar format and are typically created under the same Web site that hosts the report server. The only difference is the virtual directory name (in this case, it is **reports** but you can configure it to use whatever name that you want):  
  
-   `https://localhost/reports`  
  
-   `https://localhost/reports_SQLEXPRESS`  
  
-   `https://sales01/reports`  
  
-   `https://sales01:8080/reports`  
  
-   `https://sales.adventure-works.com/reports`  
  
-   `https://www.adventure-works.com:8080/reports`  
  
## See Also

 [Configure Report Server URLs  &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/configure-report-server-urls-ssrs-configuration-manager.md)
 [Report Server Configuration Manager &#40;Native Mode&#41;](../../reporting-services/install-windows/reporting-services-configuration-manager-native-mode.md)   
