---
title: "About URL Reservations and Registration  (SSRS Configuration Manager) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "URL reservations"
  - "URL registration"
  - "Report Server service, URL reservations"
ms.assetid: c2c460c3-e749-4efd-aa02-0f8a98ddbc76
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# About URL Reservations and Registration  (SSRS Configuration Manager)
  URLs for Reporting Services applications are defined as URL reservations in HTTP.SYS. A URL reservation defines the syntax of a URL endpoint to a Web application. URL reservations are defined for both the Report Server Web service and Report Manager when you configure the applications on the report server. URL reservations are created for you automatically when configure URLs through Setup or the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool:  
  
-   Setup will create URL reservations using default values. If Setup installs the default configuration, it will reserve two URLs; one of the Report Server Web service and another for Report Manager. You can use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool to add more URLs or modify the default URLs that Setup creates.  
  
-   The [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool will create a URL reservation based on the URL you specify in the **Web Service URL** or **Report Manager URL** pages in the tool.  
  
 Both Setup and the tool will also assign permissions on the URL to the Report Server service, check for duplicate instances, and add the URL reservation to HTTP.SYS. Never create or modify a Reporting Services URL reservation directly using HttpCfg.exe or other tool. If you skip a step or set an invalid value, you will encounter problems that might be difficult to diagnose or fix.  
  
> [!NOTE]  
>  HTTP.SYS is an operating system component that listens for network requests and routes them to a request queue. In this release of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], HTTP.SYS establishes and maintains the request queue for the Report Server Web service and Report Manager. Internet Information Services (IIS) is no longer used to host or access [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] applications. For more information about HTTP.SYS functionality, see [HTTP Server API](https://go.microsoft.com/fwlink/?LinkId=92652) on MSDN.  
  
##  <a name="ReportingServicesURLs"></a> URLs in Reporting Services  
 In a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] installation, you can access the following tools, applications, and items through URLs:  
  
-   Report Server Web service  
  
-   Report Manager  
  
-   Report Builder  
  
-   Reports that have been published to a report server  
  
 Other published URL-addressable items, such as models and shared data sources, should not be accessed through URLs as stand-alone items. The report server does not display those items in a meaningful format when viewed in a browser window.  
  
> [!NOTE]  
>  This topic does not describe URL access to Report Builder or to specific reports that are stored on the report server. For more information about URL access to these items, see [Access Report Server Items Using URL Access](../access-report-server-items-using-url-access.md) in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.  
  
##  <a name="URLreservation"></a> URL Reservation and Registration  
 A URL reservation defines the URLs that can be used to access a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] application. [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] will reserve one or more URLs for the Report Server Web service and Report Manager in HTTP.SYS, and then register them when the service starts. URLs to Report Builder and reports are based on the Report Server Web service URL reservation. By appending parameters to the URL, you can open Report Builder or reports through the Web service. Reservations and registration is provided by HTTP.SYS. For more information, see [Namespace Reservations, Registration, and Routing](https://go.microsoft.com/fwlink/?LinkId=92653) on MSDN.  
  
 *URL reservation* is a process by which a URL endpoint to a Web application is created and stored in HTTP.SYS. HTTP.SYS is the common repository of all URL reservations that are defined on a computer and defines a set of common rules that guarantee unique URL reservations.  
  
 *URL registration* occurs when the service starts. The request queue is created and HTTP.SYS begins routing requests to that queue. A URL endpoint must be registered before requests that are directed to that endpoint are added to the queue. When the Report Server service starts, it will register all URLs that it has reserved for all enabled applications. This means that the Web service must be enabled in order for registration to occur. If you set the **WebServiceAndHTTPAccessEnabled** property to **False** in the Surface Area Configuration for Reporting Services facet of Policy-Based Management, the URL for the Web service will not register when the service starts.  
  
 URLs are unregistered if you stop the service or recycle the Web service or Report Manager application domain. If you modify a URL reservation while the service is running, the report server will recycle the application domain immediately so that the old URL can be unregistered and the new one put into use.  
  
 A few simple examples illustrate the concept of a URL reservation and how it relates to URL addresses used for [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] applications. A key point to notice is that the URL reservation has different syntax than the URL you use to access the application:  
  
|URL Reservation in HTTP.SYS|URL|Explanation|  
|---------------------------------|---------|-----------------|  
|http://+:80/reportserver|http://\<computername>/reportserver<br /><br /> http://\<IPAddress>/reportserver<br /><br /> http://localhost/reportserver|The URL reservation specifies a wildcard (+) on port 80. This puts into the report server queue any incoming request that specifies a host that resolves to the report server computer on port 80. Notice that with this URL reservation, any number of URLs can be used to access the report server.<br /><br /> This is the default URL reservation for a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] report server for most operating systems.|  
|http://123.45.67.0:80/reportserver|http://123.45.67.0/reportserver|This URL reservation specifies an IP address and is much more restrictive than the wildcard URL reservation. Only URLs that include the IP address can be used to connect to the report server. Given this URL reservation, a request to a report server at http://\<computername>/reportserver or http://localhost/reportserver would fail.|  
  
##  <a name="DefaultURLs"></a> Default URLs  
 If you install [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] in the default configuration, Setup will reserve URLs for the Report Server Web service and Report Manager. You can also accept these default values when you define URL reservations in the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration tool. Default URLs will include an instance name if you install [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)] or if you install [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] as a named instance.  
  
> [!IMPORTANT]  
>  The instance character is an underscore character (`_`).  
  
 URL reservations include a port number. The following operating systems will allow multiple Web applications to share a port:  
  
1.  [!INCLUDE[win8srv](../../includes/win8srv-md.md)]  
  
2.  [!INCLUDE[winserver2008r2](../../includes/winserver2008r2-md.md)]  
  
3.  [!INCLUDE[firstref_longhorn](../../includes/firstref-longhorn-md.md)]  
  
4.  [!INCLUDE[win7](../../includes/win7-md.md)]  
  
5.  [!INCLUDE[wiprlhlong](../../includes/wiprlhlong-md.md)]  
  
|Instance Type|Application|Default URL|Actual URL reservation in HTTP.SYS|  
|-------------------|-----------------|-----------------|----------------------------------------|  
|Default instance|Report Server Web service|http://\<servername>/reportserver|http://\<servername>:80/reportserver|  
|Default instance|Report Manager|http://\<servername>/reportserver|http://\<servername>:80/reportserver|  
|Named instance|Report Server Web service|http://\<servername>/reportserver_\<instancename>|http://\<servername>:80/reportserver_\<instancename>|  
|Named instance|Report Manager|http://\<servername>/reports_\<instancename>|http://\<servername>:80/reports_\<instancename>|  
|SQL Server Express|Report Server Web service|http://\<servername>/reportserver_SQLExpress|http://\<servername>:80/reportserver_SQLExpress|  
|SQL Server Express|Report Manager|http://\<servername>/reports_SQLExpress|http://\<servername>:80/reports_SQLExpress|  
  
##  <a name="URLPermissionsAccounts"></a> Authentication and Service Identity for Reporting Services URLs  
 [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] URL reservations specify the service account of the Report Server service. The account under which the service runs is used for all URLs that are created for the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] applications that run in the same instance. The service identity of the report server instance is stored in the RSReportServer.config file.  
  
 The service account has no default value. However, specifying a service account is required during Setup and is specified in `URLReservation` in RSReportServer.config even if you install the server in files-only mode. Valid values for the service account include a domain user account, `LocalSystem`, or `NetworkService`.  
  
 Anonymous access is disabled because the default security is `RSWindowsNegotiate`. For intranet access, report server URLs use network computer names. If you want to configure [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] for Internet connections, you must use different settings. For more information about authentication, see [Authentication with the Report Server](../security/authentication-with-the-report-server.md) in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.  
  
##  <a name="URLlocalAdmin"></a> URLs for Local Administration  
 You can use http://localhost/reportserver or http://localhost/reports if you specified a strong or weak wildcard for the URL reservation.  
  
 The http://localhost URL is interpreted as http://127.0.0.1. If you pegged the URL reservation to a computer name or single IP address, you cannot use localhost unless you create an additional reservation for 127.0.0.1 on the local computer. Similarly, if localhost or 127.0.0.1 is disabled on your computer, you cannot use that URL.  
  
 [!INCLUDE[wiprlhlong](../../includes/wiprlhlong-md.md)] and [!INCLUDE[nextref_longhorn](../../includes/nextref-longhorn-md.md)] include new security features to minimize the risk of accidentally running programs with elevated privileges. Additional steps are necessary to enable local administration on these operating systems. For more information, see [Configure a Native Mode Report Server for Local Administration &#40;SSRS&#41;](../report-server/configure-a-native-mode-report-server-for-local-administration-ssrs.md).  
  
##  <a name="URLSharePoint"></a> URLs for Report Server in SharePoint Integrated Mode  
 If a stand-alone report server is configured to run within a larger deployment of a SharePoint product or technology, URL and virtual directory construction will be affected in the following ways:  
  
-   URLs for reports and other items are addressed through the SharePoint Web application URL. For URL access to specific reports, always use a fully qualified URL that includes the site path, the document library, the item name, and a file name extension (such as .rdl for a report). You must specify fully qualified URLs when you reference shared data sources and models in reports, and when you specify a target server and folders for publish operations to a report server.  
  
-   The file name extension is used to distinguish between different types of report server items. Valid extensions include .rdl for report definitions, .smdl for report models, and .rsds for shared data sources that are created for a SharePoint site.  
  
-   Although SharePoint products and technologies have URL reservations defined for them, you can ignore the reservation when publishing to the server. For SharePoint Web applications, URL reservation is an internal operation.  
  
-   For single server deployments where an integrated report server and SharePoint technology instance are installed on the same computer, you cannot use http://localhost/reportserver. If http://localhost is used to access the SharePoint Web application, you must use a non-default Web site or a unique port assignment to access a report server. Furthermore, if the report server is integrated with a SharePoint farm, localhost access to a reports server will not resolve for nodes in the deployment that are installed on remote computers.  
  
-   The URL reservation and endpoint for Report Manager cannot be configured for a report server that runs in SharePoint integrated mode. If you do configure it, it will no longer work after you deploy a report server in SharePoint integrated mode. Report Manager is not supported in this mode.  
  
 If you integrated a report server scale-out deployment to run within a larger deployment of a SharePoint product or technology, load balance the report server nodes and define a single virtual server URL to the scale-out deployment. Report Server integration settings only allow you to specify a single report server URL. In the case of a scale-out deployment, the URL must be the access point for the server nodes in the scale-out deployment.  
  
## See Also  
 [Configure a URL  &#40;SSRS Configuration Manager&#41;](configure-a-url-ssrs-configuration-manager.md)   
 [URL Reservation Syntax  &#40;SSRS Configuration Manager&#41;](url-reservation-syntax-ssrs-configuration-manager.md)  
  
  
