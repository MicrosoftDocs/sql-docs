---
title: "URL Access (SSRS) | Microsoft Docs"
ms.date: 03/03/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: reporting-services


ms.topic: conceptual
helpviewer_keywords: 
  - "URL access [Reporting Services]"
  - "links [Reporting Services], URL access"
  - "URL access [Reporting Services], about URL access"
  - "parameters [Reporting Services], URL access"
  - "report servers [Reporting Services], URL access"
  - "hyperlinks [Reporting Services]"
ms.assetid: 52c3f2a3-3d6d-4fee-9c46-83f366919398
author: markingmyname
ms.author: maghan
---
# URL Access (SSRS)
  URL access of the report server in SQL Server Reporting Services (SSRS) enables you to send commands to a report server through a URL request. For example, you can customize the rendering of a report on a native mode report server or in a SharePoint library. You may have viewed the report using a specific set of report parameter values, or you may have been viewing a particular page of interest in the report. You can encapsulate this information in the URL using predefined URL access parameters. You can further customize how the report server processes the report by embedding parameters for rendering formats or for the look and feel of the report viewer. You can then paste this URL directly into an email or Web page to let others to access your report in the same manner in the browser.  
  
 Other actions you can perform through URL access are:  
  
-   Send commands to the HTML viewer, such as adjusting its look and feel  
  
-   List the children of a catalog folder  
  
-   Retrieve the XML definition of a catalog item  
  
-   Render a specific report history snapshot  
  
-   Manage report sessions  
  
 For the complete list of commands and settings available through URL access, see [URL Access Parameter Reference](../reporting-services/url-access-parameter-reference.md).  
  
## URL Access Concepts  
 URL requests to the report server contain parameters that are processed by the report server. The way in which the report server handles URL requests depends on the parameters, parameter prefixes, and types of items that are included in the URL. Report server URLs adhere to the URL formatting guidelines as proposed by the joint World Wide Web Consortium W3C/IETF draft standard. [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] URL functionality is compatible with most Internet browsers or applications that support standard URL addressing.  
  
### URL Access Syntax  
 URL requests can contain multiple parameters that are listed in any order. Parameters are separated by an ampersand (&) and name/value pairs are separated by an equal sign (=).  
  
```  
  
rswebserviceurl  
?  
reportpath  
      [&prefix:param=value]...n]  
  
```  
  
### Syntax Description  
 *rswebserviceurl*  
 The Web service URL of the report server. For native mode, it is the Web service URL of the report server instance configured in Reporting Services Configuration Manager (see [Configure Report Server URLs  &#40;SSRS Configuration Manager&#41;](../reporting-services/install-windows/configure-report-server-urls-ssrs-configuration-manager.md)). For example:  
  
```  
https://myrshost/reportserver  
https://machine.adventure-works.com/reportserver_MYNAMEDINSTANCE  
```  
  
 For SharePoint integrated mode, it is the URL of the [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] proxy at a SharePoint site integrated with [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)]. For example:  
  
```  
https://myspsite/subsite/_vti_bin/reportserver  
```  
  
> [!TIP]  
>  It is important the URL include the `_vti_bin` proxy syntax to route the request through SharePoint and the [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] HTTP proxy. The proxy adds some context to the HTTP request, context that is required to ensure proper execution of the report for SharePoint mode report servers.  
  
 *pathinfo*  
 The relative path name of the item in the native mode report server database, or the fully qualified URL of the item in a SharePoint catalog.  
  
 The path of the catalog item. For native mode, it is the relative path of the item in the report server database, beginning with a slash (**/**). For example:  
  
```  
/AdventureWorks 2008R2/Employee_Sales_Summary_2008R2  
```  
  
 For SharePoint integrated mode, it is the fully qualified URL of the item in the SharePoint library, including the item extension. For example:  
  
```  
https://myspsite/subsite/AdventureWorks 2008R2/Employee_Sales_Summary_2008R2.rdl  
```  
  
 **&**  
 Used to separate name and value pairs of URL access parameters.  
  
 **prefix**  
 Optional. A prefix for the URL access parameter (for example, `rs:` or `rc:`) that accesses a specific process running within the report server.  
  
> [!NOTE]  
>  If a prefix for a URL access parameter is not included, the parameter is processed by the report server as a report parameter. Report parameters do not use a parameter prefix and are case-sensitive.  
  
 **param**  
 The parameter name.  
  
 *value*  
 URL text corresponding to the value of the parameter being used.  
  
 **Note:** For a list of the available URL access parameters, see [URL Access Parameter Reference](../reporting-services/url-access-parameter-reference.md). For examples passing report parameters on the URL, see [Pass a Report Parameter Within a URL](../reporting-services/pass-a-report-parameter-within-a-url.md).  
  
## Related Tasks  
  
|Task Descriptions|Links|  
|-----------------------|-----------|  
|Access report server items, such as reports, shared data sources, and resources.|[Access Report Server Items Using URL Access](../reporting-services/access-report-server-items-using-url-access.md)|  
|Pass report parameters to a report.|[Pass a Report Parameter Within a URL](../reporting-services/pass-a-report-parameter-within-a-url.md)|  
|Set the locale of the report parameters in the URL access string, which defines the locale-specific interpretations of dates, currencies, and so on.|[Set the Language for Report Parameters in a URL](../reporting-services/set-the-language-for-report-parameters-in-a-url.md)|  
|Send rendering extension specific settings that customize how the report is rendered.|[Specify Device Information Settings in a URL](../reporting-services/specify-device-information-settings-in-a-url.md)|  
|Export a report directly to a file format without viewing it in the browser.|[Export a Report Using URL Access](../reporting-services/export-a-report-using-url-access.md)|  
|Open a report and navigate directly to the location of a string.|[Search a Report Using URL Access](../reporting-services/search-a-report-using-url-access.md)|  
|Render a specific report history snapshot.|[Render a Report History Snapshot Using URL Access](../reporting-services/render-a-report-history-snapshot-using-url-access.md)|  
  
## See Also  
 [Pass a Report Parameter Within a URL](../reporting-services/pass-a-report-parameter-within-a-url.md)   
 [URL Access Parameter Reference](../reporting-services/url-access-parameter-reference.md)   
 [Integrating Reporting Services Using URL Access](../reporting-services/application-integration/integrating-reporting-services-using-url-access.md)   
 [Finding, Viewing, and Managing Reports &#40;Report Builder and SSRS &#41;](../reporting-services/report-builder/finding-viewing-and-managing-reports-report-builder-and-ssrs.md)  
  
  
