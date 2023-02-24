---
description: "Install Reporting and Internet Information Services Side-by-Side"
title: "Install Reporting and Internet Information Services Side-by-Side | Microsoft Docs"
ms.date: 07/02/2017
ms.service: reporting-services
ms.topic: conceptual
helpviewer_keywords:
  - "deploying [Reporting Services], IIS"
ms.assetid: 9b651fa5-f582-4f18-a77d-0dde95d9d211
author: maggiesMSFT
ms.author: maggies
ms.custom:
  - intro-installation
---

# Install Reporting and Internet Information Services Side-by-Side

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE[ssrs-appliesto-2016-and-later](../../includes/ssrs-appliesto-2016-and-later.md)] [!INCLUDE[ssrs-appliesto-pbirsi](../../includes/ssrs-appliesto-pbirs.md)]

[!INCLUDE [ssrs-previous-versions](../../includes/ssrs-previous-versions.md)]

You can install and run SQL Server Reporting Services (SSRS) and Internet Information Services (IIS) on the same computer. The version of IIS that you are using determines the interoperability issues you must address.  
  
|IIS version|Issues|Description|  
|-----------------|------------|-----------------|  
|8.0, 8.5|Requests intended for one application are accepted by a different application.<br /><br /> HTTP.SYS enforces precedence rules for URL reservations. Requests that are sent to applications that have the same virtual directory name and that jointly monitor port 80 might not reach the intended target if the URL reservation is weak relative to the URL reservation of another application.|Under certain conditions, a registered endpoint that supersedes another URL endpoint in the URL reservation scheme might receive HTTP requests intended for the other application.<br /><br /> Using unique virtual directory names for the Report Server Web service and the [!INCLUDE[ssRSWebPortal-Non-Markdown](../../includes/ssrswebportal-non-markdown-md.md)] helps you avoid this conflict.<br /><br /> Detailed information about this scenario is provided in this topic.|  
  
## Precedence Rules for URL Reservations  
 Before you can address interoperability issues between IIS and [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], you must understand URL reservation precedence rules. Precedence rules can be generalized into the following statement: a URL reservation that has more explicitly defined values is first in line to receive requests that match the URL.  
  
-   A URL reservation that specifies a virtual directory is more explicit than one that omits a virtual directory.  
  
-   A URL reservation that specifies a single address (by way of an IP address, a fully qualified domain name, a network computer name, or a host name) is more explicit than a wildcard.  
  
-   A URL reservation that specifies a strong wildcard is more explicit than a weak wildcard.  
  
 The following examples show a range of URL reservations, ordered from most explicit to least explicit:  
  
|Example|Request|  
|-------------|-------------|  
|`https://123.234.345.456:80/reports`|Receives all requests that are sent to `https://123.234.345.456/reports` or `https://\<computername>/reports` if a domain name service can resolve the IP address to that host name.|  
|`https://+:80/reports`|Receives any requests that are sent to any IP address or host name that is valid for that computer as long as the URL contains the "reports" virtual directory name.|  
|`https://123.234.345.456:80`|Receives any request that specifies `https://123.234.345.456` or `https://\<computername>` if a domain name service can resolve the IP address to that host name.|  
|`https://+:80`|Receives requests that are not already received by other applications, for any application endpoints that are mapped to **All Assigned**.|  
|`https://*:80`|Receives requests that are not already received by other applications, for application endpoints that are mapped to **All Unassigned**.|  
  
 One indication of a port conflict is that you will see the following error message: 'System.IO.FileLoadException: The process cannot access the file because it is being used by another process. (Exception from HRESULT: 0x80070020).'  
  
## URL Reservations for IIS 8.0, 8.5 with SQL Server Reporting Services  
 Given the precedence rules outlined in the previous section, you can begin to understand how URL reservations defined for Reporting Services and IIS promote interoperability. Reporting Services receives requests that explicitly specify the virtual directory names for its applications; IIS receives all remaining requests, which can then be directed to applications that run within the IIS process model.  
  
|Application|URL reservation|Description|Request receipt|  
|-----------------|---------------------|-----------------|---------------------|  
|Report Server|`https://+:80/ReportServer`|Strong wildcard on port 80, with report server virtual directory.|Receives all requests on port 80 that specify the report server virtual directory. The Report Server Web service receives all requests to https://\<computername>/reportserver.|  
|Web portal|`https://+:80/Reports`|Strong wildcard on port 80, with Reports virtual directory.|Receives all requests on port 80 that specify the reports virtual directory. The [!INCLUDE[ssRSWebPortal-Non-Markdown](../../includes/ssrswebportal-non-markdown-md.md)] receives all requests to https://\<computername>/reports.|  
|IIS|`https://*:80/`|Weak wildcard on port 80.|Receives any remaining requests on port 80 that are not received by another application.|  

## Side-by-Side Deployments of SQL Server Reporting Services on IIS 8.0, 8.5

 Interoperability issues between IIS and Reporting Services occur when IIS Web sites have virtual directory names that are identical to those used by Reporting Services. For example, suppose you have the following configuration:  
  
-   A Web site in IIS that is assigned to port 80 and a virtual directory named "Reports".  
  
-   A report server instance installed in the default configuration, where the URL reservation also specifies port 80 and the [!INCLUDE[ssRSWebPortal-Non-Markdown](../../includes/ssrswebportal-non-markdown-md.md)] application also uses "Reports" for the virtual directory name.  
  
 Given this configuration, a request that is sent to https://\<computername>:80/reports will be received by the [!INCLUDE[ssRSWebPortal-Non-Markdown](../../includes/ssrswebportal-non-markdown-md.md)]. The application that is accessed through the Reports virtual directory in IIS will no longer receive requests after the report server instance is installed.  
  
 If you are running side-by-side deployments of older and newer versions of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], you are likely to encounter the routing problem just described. This is because all versions of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] use "ReportServer" and "Reports" as virtual directory names for the report server and the [!INCLUDE[ssRSWebPortal-Non-Markdown](../../includes/ssrswebportal-non-markdown-md.md)] applications, increasing the likelihood that you will have a "reports" and "reportserver" virtual directories in IIS.  
  
 To ensure that all applications receive requests, follow these guidelines:  
  
-   For Reporting Services installations, use virtual directory names that are not already used by an IIS Web site on the same port as Reporting Services. If there is a conflict, install Reporting Services in "files-only" mode (using the Install but do not configure the server option in the Installation Wizard) so that you can configure the virtual directories after Setup is finished. One indication that your configuration has a conflict is you will see the error message: System.IO.FileLoadException: The process cannot access the file because it is being used by another process. (Exception from HRESULT: 0x80070020).  
  
-   For installations that you configure manually, adopt the default naming conventions in the URLs that configure. If you install [!INCLUDE[ssRSCurrent](../../includes/ssrscurrent-md.md)] as a named instance, include the instance name when creating a virtual directory.  

## Next steps

[Configure Report Server URLs](../../reporting-services/install-windows/configure-report-server-urls-ssrs-configuration-manager.md)   
[Configure a URL](../../reporting-services/install-windows/configure-a-url-ssrs-configuration-manager.md)   
[Install Reporting Services Native Mode Report Server](../../reporting-services/install-windows/install-reporting-services-native-mode-report-server.md)  

More questions? [Try asking the Reporting Services forum](/answers/search.html?c=&f=&includeChildren=&q=ssrs+OR+reporting+services&redirect=search%2fsearch&sort=relevance&type=question+OR+idea+OR+kbentry+OR+answer+OR+topic+OR+user)