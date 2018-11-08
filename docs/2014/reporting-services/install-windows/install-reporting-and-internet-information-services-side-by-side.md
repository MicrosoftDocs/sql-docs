---
title: "Install Reporting Services and Internet Information Services Side-by-Side (SSRS Native Mode) | Microsoft Docs"
ms.custom: ""
ms.date: "04/27/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "deploying [Reporting Services], IIS"
ms.assetid: 9b651fa5-f582-4f18-a77d-0dde95d9d211
author: markingmyname
ms.author: maghan
manager: craigg
---
# Install Reporting Services and Internet Information Services Side-by-Side (SSRS Native Mode)
  You can install and run [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] (SSRS) and Internet Information Services (IIS) on the same computer. The version of IIS that you are using determines the interoperability issues you must address.  
  
||  
|-|  
|[!INCLUDE[applies](../../includes/applies-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Native mode|  
  
|IIS version|Issues|Description|  
|-----------------|------------|-----------------|  
|IIS 6.0, 7.0, 8.0, 8.5|Requests intended for one application are accepted by a different application.<br /><br /> HTTP.SYS enforces precedence rules for URL reservations. Requests that are sent to applications that have the same virtual directory name and that jointly monitor port 80 might not reach the intended target if the URL reservation is weak relative to the URL reservation of another application.|Under certain conditions, a registered endpoint that supersedes another URL endpoint in the URL reservation scheme might receive HTTP requests intended for the other application.<br /><br /> Using unique virtual directory names for the Report Server Web service and Report Manager helps you avoid this conflict.<br /><br /> Detailed information about this scenario is provided in this topic.|  
  
## Precedence Rules for URL Reservations  
 Before you can address interoperability issues between IIS and [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], you must understand URL reservation precedence rules. Precedence rules can be generalized into the following statement: a URL reservation that has more explicitly defined values is first in line to receive requests that match the URL.  
  
-   A URL reservation that specifies a virtual directory is more explicit than one that omits a virtual directory.  
  
-   A URL reservation that specifies a single address (by way of an IP address, a fully qualified domain name, a network computer name, or a host name) is more explicit than a wildcard.  
  
-   A URL reservation that specifies a strong wildcard is more explicit than a weak wildcard.  
  
 The following examples show a range of URL reservations, ordered from most explicit to least explicit:  
  
|Example|Request|  
|-------------|-------------|  
|http://123.234.345.456:80/reports|Receives all requests that are sent to http://123.234.345.456/reports or http://\<computername>/reports if a domain name service can resolve the IP address to that host name.|  
|http://+:80/reports|Receives any requests that are sent to any IP address or host name that is valid for that computer as long as the URL contains the "reports" virtual directory name.|  
|http://123.234.345.456:80|Receives any request that specifies http://123.234.345.456 or http://\<computername> if a domain name service can resolve the IP address to that host name.|  
|http://+:80|Receives requests that are not already received by other applications, for any application endpoints that are mapped to **All Assigned**.|  
|http://*:80|Receives requests that are not already received by other applications, for application endpoints that are mapped to **All Unassigned**.|  
  
 One indication of a port conflict is that you will see the following error message: 'System.IO.FileLoadException: The process cannot access the file because it is being used by another process. (Exception from HRESULT: 0x80070020).'  
  
## URL Reservations for IIS 6.0, 7.0, 8.0, 8.5 with [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] Reporting Services  
 Given the precedence rules outlined in the previous section, you can begin to understand how URL reservations defined for Reporting Services and IIS promote interoperability. Reporting Services receives requests that explicitly specify the virtual directory names for its applications; IIS receives all remaining requests, which can then be directed to applications that run within the IIS process model.  
  
|Application|URL reservation|Description|Request receipt|  
|-----------------|---------------------|-----------------|---------------------|  
|Report Server|http://+:80/ReportServer|Strong wildcard on port 80, with report server virtual directory.|Receives all requests on port 80 that specify the report server virtual directory. The Report Server Web service receives all requests to http://\<computername>/reportserver.|  
|Report Manager|http://+:80/Reports|Strong wildcard on port 80, with Reports virtual directory.|Receives all requests on port 80 that specify the reports virtual directory. Report Manager receives all requests to http://\<computername>/reports.|  
|IIS|http://*:80/|Weak wildcard on port 80.|Receives any remaining requests on port 80 that are not received by another application.|  
  
## Side-by-Side Deployments of [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] and SQL Server 2005 Reporting Services on IIS 6.0, 7.0, 8.0, 8.5  
 Interoperability issues between IIS and Reporting Services occur when IIS Web sites have virtual directory names that are identical to those used by Reporting Services. For example, suppose you have the following configuration:  
  
-   A Web site in IIS that is assigned to port 80 and a virtual directory named "Reports".  
  
-   A [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] report server instance installed in the default configuration, where the URL reservation also specifies port 80 and the Report Manager application also uses "Reports" for the virtual directory name.  
  
 Given this configuration, a request that is sent to http://\<computername>:80/reports will be received by Report Manager. The application that is accessed through the Reports virtual directory in IIS will no longer receive requests after [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] report server instance is installed.  
  
 If you are running side-by-side deployments of older and newer versions of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], you are likely to encounter the routing problem just described. This is because all versions of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] use "ReportServer" and "Reports" as virtual directory names for the report server and Report Manager applications, increasing the likelihood that you will have a "reports" and "reportserver" virtual directories in IIS.  
  
 To ensure that all applications receive requests, follow these guidelines:  
  
-   For Reporting Services installations, use virtual directory names that are not already used by an IIS Web site on the same port as Reporting Services. If there is a conflict, install Reporting Services in "files-only" mode (using the Install but do not configure the server option in the Installation Wizard) so that you can configure the virtual directories after Setup is finished. One indication that your configuration has a conflict is you will see the error message: System.IO.FileLoadException: The process cannot access the file because it is being used by another process. (Exception from HRESULT: 0x80070020).  
  
-   For installations that you configure manually, adopt the default naming conventions in the URLs that configure. If you install [!INCLUDE[ssRSCurrent](../../includes/ssrscurrent-md.md)] as a named instance, include the instance name when creating a virtual directory.  
  
## See Also  
 [Configure Report Server URLs  &#40;SSRS Configuration Manager&#41;](configure-report-server-urls-ssrs-configuration-manager.md)   
 [Configure a URL  &#40;SSRS Configuration Manager&#41;](configure-a-url-ssrs-configuration-manager.md)   
 [Install Reporting Services Native Mode Report Server](install-reporting-services-native-mode-report-server.md)  
  
  
