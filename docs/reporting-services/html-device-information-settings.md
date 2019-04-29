---
title: "HTML Device Information Settings | Microsoft Docs"
ms.date: 03/16/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: reporting-services


ms.topic: conceptual
helpviewer_keywords: 
  - "HTML [Reporting Services], rendering"
  - "device information settings [Reporting Services], HTML rendering"
ms.assetid: f505f478-dd6d-444a-957c-34f7cfb98911
author: maggiesMSFT
ms.author: maggies
---
# HTML Device Information Settings
The following table lists the device information settings for rendering in HTML format.  
  
> [!IMPORTANT]  
>  The device information settings listed in the table below with a **(\*)** have been deprecated and they should not be used in new applications. For more information, see [Deprecated Features in SQL Server Reporting Services in SQL Server 2016](../reporting-services/deprecated-features-in-sql-server-reporting-services-ssrs.md)   
  
|Setting|Value|  
|-------------|-----------|  
|**AccessibleTablix**|Indicates whether to render with additional accessibility metadata for use with screen readers. The additional accessibility metadata causes the rendered report to be compliant with the following technical standards in the "Web-based Intranet and Internet Information and Applications" section (1194.22) of the Electronic and Information Technology Accessibility Standards (Section 508) document:<br /><br /> (g) Row and column headers shall be identified for data tables.<br /><br /> (h) Markup shall be used to associate data cells and header cells for data tables that have two or more logical levels of row or column headers.|  
|**ActionScript(\*)**|Specifies the name of the JavaScript function to use when an action event occurs, such as a drillthrough or bookmark click. If this parameter is specified, an action event will trigger the named JavaScript function instead of a postback to the server.|  
|**BookmarkID**|The bookmark ID to jump to in the report.|  
|**DocMap**|Indicates whether to show or hide the report document map. The default value of this parameter is **true**.|  
|**ExpandContent**|Indicates whether the report should be enclosed in a table structure which constricts horizontal size.|  
|**FindString**|The text to search for in the report. The default value of this parameter is an empty string.|  
|**GetImage (\*)**|Gets a particular icon for the HTML Viewer user interface.|  
|**HTMLFragment**|Indicates whether an HTML fragment is created in place of a full HTML document. An HTML fragment includes the report content in a TABLE element and omits the HTML and BODY elements. The default value is **false**. If you are rendering to HTML using the **M:ReportExecution2005.ReportExecutionService.Render(System.String,System.String,System.String@,System.String@,System.String@, ReportExecution2005.Warning[]@,System.String[]@)** method of the SOAP API, you need to set this device information to **true** if you are rendering a report with images. Rendering using SOAP with the **HTMLFragment** property set to **true** creates URLs containing session information that can be used to properly request images. The images must be uploaded resources in the report server database.|  
|**ImageConsolidation**|Indicates whether to consolidate the rendered chart, map, gauge, and indicator images into one large image. The consolidation of images helps improve the performance of the report in the client browser when the report contains many data visualization items. The default value is **true** for most modern browsers.|  
|**JavaScript**|Indicates whether JavaScript is supported in the rendered report. The default value is **true**.|  
|**LinkTarget**|The target for hyperlinks in the report. You can target a window or frame by providing the name of the window, like **LinkTarget**=*window_name*, or you can target a new window using **LinkTarget**=_blank. Other valid target names include _self, _parent, and _top.|  
|**OnlyVisibleStyles(\*)**|Indicates that only shared styles for currently rendered page are generated.|  
|**OutlookCompat**|Indicates whether to render with extra metadata that makes the report look better in Outlook. For others, the default value is **false**.|  
|**Parameters**|Indicates whether to show or hide the parameters area of the toolbar. If you set this parameter to a value of **true**, the parameters area of the toolbar is displayed. The default value of this parameter is **true**.|  
|**PrefixId**|When used with **HTMLFragment**, adds the specified prefix to all **ID** attributes in the HTML fragment that is created.|  
|**ReplacementRoot(\*)**|The string to prepend to all drillthrough, toggle, and bookmark links in the report when rendered outside of the ReportViewer control. For example, this is used for redirecting a user's click to a custom page.|  
|**ResourceStreamRoot(\*)**|The string to prepend to the URL for all image resources, such as images for toggle or sort.|  
|**Section**|The page number of the report to render. A value of **0** indicates that all sections of the report are rendered. The default value is **1**.|  
|**StreamRoot (\*)**|The path used for prefixing the value of the **src** attribute of the IMG element in the HTML report returned by the report server. By default, the report server provides the path. You can use this setting to specify a root path for the images in a report (for example, **https://\<servername>/resources/companyimages**).|  
|**StyleStream**|Indicates whether styles and scripts are created as a separate stream instead of in the document. The default value is **false**.|  
|**Toolbar**|Indicates whether to show or hide the toolbar. The default of this parameter is **true**. If the value of this parameter is **false**, all remaining options (except the document map) are ignored. If you omit this parameter, the toolbar is automatically displayed for rendering formats that support it.<br /><br /> The Report Viewer toolbar is rendered when you use URL access to render a report. The toolbar is not rendered through the SOAP API. However, the **Toolbar** device information setting affects the way that the report is displayed when using the SOAP **Render** method. If the value of this parameter is **true** when using SOAP to render to HTML, only the first section of the report is rendered. If the value is **false**, the entire HTML report is rendered as a single HTML page.|  
|**UserAgent**|The **user-agent** string of the browser that is making the request, which is found in the HTTP request.|  
|**Zoom (\*)**|The report zoom value as an integer percentage or a string constant. Standard string values include **Page Width** and **Whole Page**. This parameter is ignored by versions of [!INCLUDE[msCoName](../includes/msconame-md.md)] Internet Explorer earlier than Internet Explorer 5.0 and all non-[!INCLUDE[msCoName](../includes/msconame-md.md)] browsers. The default value of this parameter is **100**.|  
|**DataVisualizationFitSizing**|Indicates data visualization fit behavior when inside a tablix. This includes chart, gauge, and map.<br /><br /> The possible values are **Approximate** and **Exact**.<br /><br /> The default value is **Approximate**. If the setting is removed from the **rsreportserver.config** file then the default behavior is **Exact**.<br /><br /> Enabling **Exact** may have performance impact because the processing to determine the exact size may take longer.|  
  
## See Also  
 [Passing Device Information Settings to Rendering Extensions](../reporting-services/report-server-web-service/net-framework/passing-device-information-settings-to-rendering-extensions.md)   
 [Customize Rendering Extension Parameters in RSReportServer.Config](../reporting-services/customize-rendering-extension-parameters-in-rsreportserver-config.md)   
 [Technical Reference &#40;SSRS&#41;](../reporting-services/technical-reference-ssrs.md)  
  
  
