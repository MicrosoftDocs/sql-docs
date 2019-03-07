---
title: "URLs in Configuration Files  (SSRS Configuration Manager) | Microsoft Docs"
ms.date: 05/18/2016
ms.prod: reporting-services
ms.prod_service: "reporting-services-native"

ms.topic: conceptual
helpviewer_keywords: 
  - "URL configuration [Reporting Services]"
ms.assetid: 4f5e7fe0-b5b1-4665-93d4-80dce12d6b14
author: markingmyname
ms.author: maghan
---
# URLs in Configuration Files  (SSRS Configuration Manager)
  [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] stores application settings in a RSReportServer.config file. Within this file, there are configuration settings for both URLs and URL reservations. These configuration settings have very different purposes and rules for modification. If you are accustomed to modifying configuration files to tune a deployment, this topic can help you understand how each URL setting is used.  
  
## URL Settings in RSReportServer.config File  
 [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] stores URLs for application and report access, and to connect Web front-end components to a back-end report server.  
  
#### URLs for Application Access  
 URLs are used to access the Report Server Web service and the [!INCLUDE[ssRSWebPortal](../../includes/ssrswebportal.md)]. To configure the URLs, you must use the Reporting Services Configuration tool. The tool creates the URL reservations for each application in HTTP.SYS and adds entries for the URLs in the **URLReservations** section of RSReportServer.config.  
  
-   To view descriptions of each element in the **URLReservations** section, see [RsReportServer.config Configuration File](../../reporting-services/report-server/rsreportserver-config-configuration-file.md) in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.  
  
-   For more information about the syntax of just the **UrlString** element, see [URL Reservation Syntax  &#40;SSRS Configuration Manager&#41;](../../reporting-services/install-windows/url-reservation-syntax-ssrs-configuration-manager.md).  
  
-   For instructions on how to configure URLs for application access, see [Configure a URL  &#40;SSRS Configuration Manager&#41;](../../reporting-services/install-windows/configure-a-url-ssrs-configuration-manager.md).  
  
#### URLs for Report Access  
 [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] includes a report server e-mail delivery extension that you can use to send report links or attachments. A report link is constructed when the report is delivered. The report server e-mail delivery extension uses the **UrlRoot** setting in the configuration file to create the link. **UrlRoot** is also used to resolve links in a rendered report that is generated through unattended report processing.  
  
 **UrlRoot** is specified automatically in the RSReportServer.config file when you configure URLs for application access. If you modify this value in the configuration file, you must specify a valid URL address to a Report Server Web service that is connected to a report server database that contains the reports you want to deliver. You can only specify one **UrlRoot** for a single report server instance; only one **UrlRoot** entry can exist in the RSReportServer.config file for any given report server instance. If you have multiple URLs reserved for the Report Server Web service, you must choose one of the available values for **UrlRoot**.  
  
 In most cases, you do not need to modify **UrlRoot**. However, if the report server will be accessed through a fully qualified URL, and you did not configure a URL that uses a host header to the fully qualified site name, you must edit the RSReportServer.config manually to set the **UrlRoot** to the fully qualified report server URL that will be used to render the report (for example, https://www.adventure-works.com/mywebapp/reportserver).  
  
#### URLs Connecting the [!INCLUDE[ssRSWebPortal](../../includes/ssrswebportal.md)] and Web Parts to the Report Server Web Service  
 the [!INCLUDE[ssRSWebPortal](../../includes/ssrswebportal.md)] and the SharePoint 2.0 Web Parts for Reporting Services are Web front-end components that connect to a report server. URLs used to connect to a backend report server include the following:  
  
-   **ReportServerUrl** (used by the [!INCLUDE[ssRSWebPortal](../../includes/ssrswebportal.md)])  
  
-   **ReportServerExternalUrl** (used by Web Parts)  
  
> [!NOTE]  
>  Previous versions of Reporting Services included the **ReportServerVirtualDirectory** element. This value is obsolete in [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] and later versions. If you upgraded an existing installation and are using a configuration file that contains this setting, the report server no longer reads this value.  
  
 The following table provides a summary of all the URLs that can be specified in a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] configuration file.  
  
|Setting|Usage|Description|  
|-------------|-----------|-----------------|  
|**ReportServerUrl**|Optional. This element is not included in the RSReportServer.config file unless you add it yourself.<br /><br /> Set this element only if you are configuring one of the following scenarios:<br /><br /> the [!INCLUDE[ssRSWebPortal](../../includes/ssrswebportal.md)] provides Web front-end access to a Report Server Web service that runs on a different computer or a different instance on the same computer.<br /><br /> When you have multiple URLs to a report server and you want the [!INCLUDE[ssRSWebPortal](../../includes/ssrswebportal.md)] to use a specific URL.<br /><br /> You have a specific report server URL through which you want all the [!INCLUDE[ssRSWebPortal](../../includes/ssrswebportal.md)] connections to use.<br /><br /> For example, you might enable the [!INCLUDE[ssRSWebPortal](../../includes/ssrswebportal.md)] access for all computers on network, yet require that the [!INCLUDE[ssRSWebPortal](../../includes/ssrswebportal.md)] connect to the report server through a local connection. In this case, you might configure **ReportServerUrl** to "`https://localhost/reportserver`".|This value specifies a URL to the Report Server Web service. This value is read by the [!INCLUDE[ssRSWebPortal](../../includes/ssrswebportal.md)] application at startup. If this value is set, the [!INCLUDE[ssRSWebPortal](../../includes/ssrswebportal.md)] will connect to the report server that is specified in the URL.<br /><br /> By default, the [!INCLUDE[ssRSWebPortal](../../includes/ssrswebportal.md)] provides Web front-end access to the Report Server Web service that runs within the same report server instance as the [!INCLUDE[ssRSWebPortal](../../includes/ssrswebportal.md)]. However, if you want to use the [!INCLUDE[ssRSWebPortal](../../includes/ssrswebportal.md)] with a Report Server Web service that is part of another instance or runs in an instance on a different computer, you can set this URL to direct the [!INCLUDE[ssRSWebPortal](../../includes/ssrswebportal.md)] to connect to the external Report Server Web service.<br /><br /> If a Secure Sockets Layer (SSL) certificate is installed on the report server to which you are connecting, the **ReportServerUrl** value must be the name of the server that is registered for that certificate. If you get the error, "The underlying connection was closed: Could not establish trust relationship for the SSL/TLS security channel", set **ReportServerUrl** to the fully qualified domain name of the server for which the SSL certificate was issued. For example, if the certificate is registered to **https://adventure-works.com.onlinesales**, the report server URL would be **https://adventure-works.com.onlinesales/reportserver**.|  
|**ReportServerExternalUrl**|Optional. This element is not included in the RSReportServer.config file unless you add it yourself.<br /><br /> Set this element only if you are using the SharePoint 2.0 Web Parts and you want users to be able to retrieve a report and open it in a new browser window.<br /><br /> Add \<**ReportServerExternalUrl**> underneath the \<**ReportServerUrl**> element, and then set it to a fully qualified report server name that resolves to a report server instance when accessed in a separate browser window. Do not delete \<**ReportServerUrl**>.<br /><br /> The following example illustrates the syntax:<br /><br /> `<ReportServerExternalUrl>https://myserver/reportserver</ReportServerExternalUrl>`|This value is used by the SharePoint 2.0 Web Parts.<br /><br /> In previous releases, it was recommended that you set this value to deploy Report Builder on an Internet-facing report server. This is an untested deployment scenario. If you used this setting in the past to support Internet access to Report Builder, you should consider an alternative strategy.|  
  
## See Also  
 [Configure Report Server URLs  &#40;SSRS Configuration Manager&#41;](../../reporting-services/install-windows/configure-report-server-urls-ssrs-configuration-manager.md)   
 [Configure a URL  &#40;SSRS Configuration Manager&#41;](../../reporting-services/install-windows/configure-a-url-ssrs-configuration-manager.md)
