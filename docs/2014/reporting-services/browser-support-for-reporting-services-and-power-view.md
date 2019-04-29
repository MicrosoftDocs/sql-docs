---
title: Planning for Reporting Services and Power View Browser Support (Reporting Services 2014)
author: maggiesMSFT
ms.author: maggies
manager: kfile
ms.reviewer: ""
ms.prod: sql-server-2014
ms.technology: reporting-services-native
ms.topic: conceptual
ms.custom: ""
ms.date: 06/13/2017
---

# Planning for Reporting Services and Power View Browser Support (Reporting Services 2014)
  In [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)][!INCLUDE[ssCurrent](../includes/sscurrent-md.md)], you use a web browser to view reports and run Report Manager. Not all browsers support all report functionality. This topic describes the support and requirements for Report Manager management features, viewing reports, the Report Viewer controls in Visual Studio. The topic also summarizes feature availability for the supported browsers, authentication requirements, and script requirements.  
  
 **[!INCLUDE[applies](../includes/applies-md.md)]**  [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] SharePoint mode | [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] Native mode  
  
 **In this Topic:**  
  
- [Power View Browser Scenarios](#bkmk_powerview)  
  
- [Report Manager Browser Requirements (Native mode)](#bkmk_reportmanager)  
  
- [Browser Requirements for Viewing Reports](#bkmk_reportviewer)  
  
- [Authentication Requirements](#bkmk_authentication)  
  
- [Browser Support for ReportViewer Web Server Controls in Visual Studio](#bkmk_controls)  
  
##  <a name="bkmk_powerview"></a> Power View Browser Scenarios

 The list of supported browsers and browser versions that [!INCLUDE[ssCrescent](../includes/sscrescent-md.md)] supports, depends on what type of document is opened. Excel 2013 workbooks and "**.rdlx**" files utilize different components.  
  
|Document Type|Environment|Browser support|  
|-------------------|-----------------|---------------------|  
|Power View report (.RDLX)|**SharePoint Server:** [!INCLUDE[ssCrescent](../includes/sscrescent-md.md)] on [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] SharePoint integrated mode and the Power View web application.|See [Power View on SharePoint Server and Reporting Services SharePoint Integrated Mode](#bkmk_powerview_on_SSRS).|  
|Excel 2013 workbook with Power View sheets|**SharePoint Server:** [!INCLUDE[ssCrescent](../includes/sscrescent-md.md)] in Excel Services.<br /><br /> **SharePoint Online (Office 365):** [!INCLUDE[ssCrescent](../includes/sscrescent-md.md)] on Excel Web App.|See [Power View on Excel Services or the Excel Web App on SharePoint Online](#bkmk_powerview_on_ExcelServices).|  
  
###  <a name="bkmk_powerview_on_SSRS"></a> Power View on SharePoint Server and Reporting Services SharePoint Integrated Mode  
 The following table summarizes the supported browser versions for [!INCLUDE[ssCrescent](../includes/sscrescent-md.md)] when a user opens a Power View report (.RDLX) on a SharePoint farm that has a [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] service application and the [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] add-in for SharePoint installed and configured.  
  
- The table applies to SharePoint 2010 and SharePoint 2013.  
  
- For more information on the SharePoint 2013 browser support, see [Plan browser support in SharePoint 2013](https://technet.microsoft.com//library/cc263526\(office.15\).aspx) (https://technet.microsoft.com/library/cc263526(office.15).aspx).  
  
- For more information on the SharePoint 2010 browser support, see [Plan browser support (SharePoint Server 2010)](https://technet.microsoft.com/library/cc263526\(office.14\).aspx) (https://technet.microsoft.com/library/cc263526(office.14).aspx).  
  
|**Browser**|**Windows 8 and 8.1**|**Windows 7**|**Windows Server 2012 and 2012 R2**|**Windows Server 2008 R2**|**Windows Server 2008**|**Mac OS X 10.6 - 10.9**|  
|-----------------|---------------------------|-------------------|-----------------------------------------|--------------------------------|-----------------------------|------------------------------|  
|**Internet Explorer 11 (for the desktop)**|32-bit, 64-bit|32-bit, 64-bit|32-bit, 64-bit|32-bit, 64-bit|Not supported|Not supported|  
|**Internet Explorer 10 (for the desktop)**|32-bit, 64-bit|32-bit, 64-bit|32-bit, 64-bit|32-bit, 64-bit|Not supported|Not supported|  
|**Internet Explorer 9**|Not supported|32-bit, 64-bit|Not supported|32-bit, 64-bit|32-bit, 64-bit|Not supported|  
|**Internet Explorer 8**|Not supported|32-bit, 64-bit|Not supported|32-bit, 64-bit|32-bit, 64-bit|Not supported|  
|**Mozilla Firefox (latest publicly released version)**|32-bit|32-bit|32-bit|32-bit|32-bit|Not supported|  
|**Apple Safari (latest publicly released version)**|Not supported|Not supported|Not supported|Not supported|Not supported|32-bit, 64-bit|  
  
> [!NOTE]  
> "32-bit" refers to the browser, not the operating system. You can use 32-bit Internet Explorer 9 on 64-bit Windows 7, for example.  
  
#### InPrivate browsing feature in Internet Explorer

 [!INCLUDE[ssCrescent](../includes/sscrescent-md.md)] does not support the InPrivate browsing feature in [!INCLUDE[msCoName](../includes/msconame-md.md)] Internet Explorer 8 and Internet Explorer 9. For more information about InPrivate browsing, see [What is InPrivate Browsing?](http://windows.microsoft.com/Windows7/What-is-InPrivate-Browsing) (http://windows.microsoft.com/Windows7/What-is-InPrivate-Browsing).  
  
###  <a name="bkmk_powerview_on_ExcelServices"></a> Power View on Excel Services or the Excel Web App on SharePoint Online

 The following table summarizes the supported browser versions for [!INCLUDE[ssCrescent](../includes/sscrescent-md.md)] when a user opens an Excel 2013 workbook with Power View sheets on a SharePoint Server that is running Excel Services:  
  
-   For more information on the SharePoint 2013 browser support, see [Plan browser support in SharePoint 2013](https://technet.microsoft.com/library/cc263526\(office.15\).aspx) (https://technet.microsoft.com/library/cc263526(office.15).aspx).  
  
|**Browser**|**Windows 8 and 8.1**|**Windows 7**|**Windows Server 2012 and 2012 R2**|**Windows Server 2008 R2**|**Windows Server 2008**|**Mac OS X 10.6 - 10.9**|  
|-----------------|---------------------------|-------------------|-----------------------------------------|--------------------------------|-----------------------------|------------------------------|  
|**Internet Explorer 11 (for the desktop)**|32-bit, 64-bit|32-bit, 64-bit|32-bit, 64-bit|32-bit, 64-bit|Not supported|Not supported|  
|**Internet Explorer 10 (for the desktop)**|32-bit, 64-bit|32-bit, 64-bit|32-bit, 64-bit|32-bit, 64-bit|Not supported|Not supported|  
|**Internet Explorer 9**|Not supported|32-bit, 64-bit|Not supported|32-bit, 64-bit|32-bit, 64-bit|Not supported|  
|**Internet Explorer 8**|Not supported|32-bit, 64-bit|Not supported|32-bit, 64-bit|32-bit, 64-bit|Not supported|  
|**Mozilla Firefox (latest publicly released version)**|32-bit|32-bit|32-bit|32-bit|32-bit|32-bit, 64-bit|  
|**Apple Safari (latest publicly released version)**|Not supported|Not supported|Not supported|Not supported|Not supported|32-bit, 64-bit|  
|**Google Chrome (latest publicly released version)**|32-bit **(\*)** For limited time|32-bit **(\*)** For limited time|32-bit **(\*)** For limited time|32-bit **(\*)** For limited time|32-bit **(\*)** For limited time|Not supported|  
  
 **(\*)** Chrome will stop supporting the Netscape Plug-in API (NPAPI), used by Silverlight. Power View is dependent on Silverlight.  For more information, see [The Final Countdown for NPAPI](http://blog.chromium.org/2014/11/the-final-countdown-for-npapi.html).  
  
##  <a name="bkmk_reportmanager"></a> Report Manager Browser Requirements (Native mode)

 The following is the current list of supported browsers you can use to run the [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] native mode Report Manager to manage reports and the report server.  
  
|Browser|  
|-------------|  
|Internet Explorer 7 or newer and scripting must be enabled.|  
|Mozilla FireFox (latest publicly released version)|  
|Apple Safari (latest publicly released version)|  
|Google Chrome (latest publicly released version)|  
  
##  <a name="bkmk_reportviewer"></a> Browser Requirements for Viewing Reports

 The following is the current list of browsers and features supported with the report viewer. The report viewer supports viewing reports from [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] report manager and SharePoint libraries.  
  
|**Browser**|**Windows 8 and 8.1**|**Windows 7**|**Windows Server 2012 and 2012 R2**|**Windows Server 2008 R2**|**Windows Server 2008**|**Mac OS X 10.6 - 10.9**|**iOS 6 -7 for iPad**|  
|-----------------|---------------------------|-------------------|-----------------------------------------|--------------------------------|-----------------------------|------------------------------|----------------------------|  
|**Internet Explorer 11 (for the desktop)**|32-bit, 64-bit|32-bit, 64-bit|32-bit, 64-bit|Not supported|Not supported|Not supported|Not supported|  
|**Internet Explorer 10 (for the desktop)**|32-bit, 64-bit|32-bit, 64-bit|32-bit, 64-bit|Not supported|Not supported|Not supported|Not supported|  
|**Internet Explorer 9**|Not supported|32-bit, 64-bit|Not supported|32-bit, 64-bit|32-bit, 64-bit|Not supported|Not supported|  
|**Internet Explorer 8**|Not supported|32-bit, 64-bit|Not supported|32-bit, 64-bit|32-bit, 64-bit|Not supported|Not supported|  
|**Internet Explorer 7**|Not supported|Not supported|Not supported|Not supported|32-bit, 64-bit|Not supported|Not supported|  
|**Mozilla Firefox (latest publicly released version)**|32-bit|32-bit|32-bit|32-bit|32-bit|Not supported|Not supported|  
|**Apple Safari (latest publicly released version)**|Not supported|Not supported|Not supported|Not supported|Not supported|32-bit, 64-bit|Supported with limited features <sup>(1)</sup>|  
|**Google Chrome (latest publicly released version)**|32-bit|32-bit|32-bit|32-bit|32-bit|Not supported|Not supported|  
  
 **<sup>(1)</sup>**  The following features are supported:  
  
- Export to PDF and TIFF format.  
  
- Interactively view reports in Apple Safari on iOS devices. Features support includes expand/collapse, the parameter pane, and interactive sorting.  
  
- For more information, see [View Reporting Services Reports on Microsoft Surface Devices and  Apple iOS Devices](../../2014/reporting-services/view-reporting-services-reports-surface-ios-devices.md).  
  
 **Note** If you are accessing a report server from a Macintosh computer, we recommend that you use Safari. If you are using a SharePoint product that is integrated with [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)], see [Plan browser support (Windows SharePoint Services)](https://go.microsoft.com/fwlink/?LinkId=183583).  
  
### URL Access for Viewing Reports

 To view reports directly, rather than viewing them through Report Manager, you can use URL Access to link to the report and the report viewer. URL access supports a variety of browsers.  
  
 For more information on URL access, see the following topic:  
  
- [URL Access Parameter Reference](url-access-parameter-reference.md)  
  
###  <a name="bkmk_authentication"></a> Authentication Requirements

 Browsers support specific authentication schemes that must be handled by the report server in order for the client request to succeed. The following table identifies the default authentication types supported by each browser running on a Windows operating system.  
  
|**Browser type**|**Supports**|**Browser default**|**Server default**|  
|----------------------|------------------|-------------------------|------------------------|  
|**Internet Explorer**|Negotiated, Kerberos, NTLM, Basic|Negotiate|Yes. The default authentication settings work with Internet Explorer.|  
|**Firefox**|NTLM, Basic|NTLM|Yes. The default authentication settings work with Firefox.|  
|**Safari**|Basic|Basic|Yes. The default authentication settings work with Safari.|  
|**Chrome**|Negotiated, NTLM, Basic|Negotiated|Yes. The default authentication settings work with Chrome.|  
  
### Script Requirements

 To use the report viewer, configure your browser to run scripts.  
  
 If scripting is not enabled, you will see an error message similar to the following when you open a report:  
  
- **Your browser does not support scripts or has been configured to not allow scripts to run. Click here to view this report without scripts**.  
  
 If you choose to view the report without script support, the report is rendered in HTML without report viewer capabilities such as the report toolbar and the document map.  
  
> [!NOTE]  
> The report toolbar is part of the HTML Viewer component. By default the toolbar appears at the top of every report that is rendered in a browser window. The report viewer provides features include the ability to search the report for information, scroll to a specific page, and adjust the page size for viewing purposes. For more information about the report toolbar or HTML Viewer, see [HTML Viewer and the Report Toolbar](html-viewer-and-the-report-toolbar.md).  
  
##  <a name="bkmk_controls"></a> Browser Support for ReportViewer Web Server Controls in Visual Studio

 The ReportViewer Web server control is used to embed report functionality in an ASP.NET web application. The controls are included with Visual Studio and support different browsers and browser versions than the other components described in this topic. The type of browser used to view the application determines the kind of ReportViewer functionality that you can provide in your application. Use the table provided in this topic to determine which of the supported browsers are subject to report functionality restrictions and the supported platforms.  
  
 Due to differences in the rendering engines of the supported browsers, some advance report features may be displayed differently in differnt browsers.  For example, text rotation.  
  
### Scripting Requirements

 Use a browser that has script support enabled. If the browser cannot run scripts, you cannot view the report.  
  
### Browser Requirements for Viewing Reports with the ReportViewer web server controls

 Support for interactive report features varies by browser type. The following support matrix shows which browser types are supported on which platforms, subject to constraints noted in the Notes column.  
  
|||||||||  
|-|-|-|-|-|-|-|-|  
|**Browser**|**Windows 8** and **Windows 8.1**|**Windows 7**|**Windows Server 2012** and **2012 R2**|**Windows Server 2008** and **2008 R2**|**Windows Server 2003**|**Mac OS X 10.6 - 10.9**|**Notes**|  
|**Internet Explorer 11 (for the desktop**|Yes|Yes|Yes|Not supported|Not supported|Not supported|Internet Explorer supports the complete set of ReportViewer features.|  
|**Internet Explorer 10 (for the desktop)**|Yes|Yes|Yes|Not supported|Not supported|Not supported|Internet Explorer supports the complete set of ReportViewer features.|  
|**Internet Explorer 9**|Not supported|Yes|Not supported|Yes|Yes|Yes|Internet Explorer supports the complete set of ReportViewer features.|  
|**Internet Explorer 8.0**|Not supported|Yes|Not supported|Yes|Yes<sup>1</sup>|Not supported|Internet Explorer supports the complete set of ReportViewer features. <sup>1</sup>|  
|**Internet Explorer 7.0**|Not supported|Yes|Not supported|Yes|Yes<sup>1</sup>|Not supported|Internet Explorer supports the complete set of ReportViewer features. <sup>1</sup>|  
|**Firefox (latest publicly released version)**|Yes|Yes|Yes|Yes|Yes|Not supported|Printing and zooming are not supported.|  
|**Safari (latest publicly released version)**|Not supported|Not supported|Not supported|Not supported|Not supported|Yes|Printing and zooming are not supported.<br /><br /> The Calendar control that is used to select dates on a parameterized report is disabled in this browser. Users must type the dates that they want to use manually in the parameter prompt area.|  
|**Chrome (latest publicly released version)**|Yes|Yes|Yes|Yes|Yes|Not supported|Printing and Zooming are not supported.|  
  
 <sup>1</sup>In standards mode, Internet Explorer 7.0 and 8.0 do not display slanted lines in reports. If you use slanted lines in your reports, set your ASP.NET page to run in quirks mode in Internet Explorer. To do this, find the \<!DOCTYPE> tag in your ASP.NET page. Or, if you use a master page, you can find the tag in the .master file. This tag looks like the following:  
  
```
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">  
```
  
 Replace the \<!DOCTYPE> tag with the following tag:  
  
```
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">  
```
  
 For more information on compatibility modes in Internet Explorer, see [Defining Document Compatibility](https://go.microsoft.com/fwlink/?LinkId=180380) (https://go.microsoft.com/fwlink/?LinkId=180380).  
  
 For more information on using the ReportViewer controls, see [Deploying Reports and ReportViewer Controls](https://msdn.microsoft.com/library/ms251723.aspx) (https://msdn.microsoft.com/library/ms251723.aspx).  
  
## Next steps

 [Reporting Services Tools](tools/reporting-services-tools.md)
 [Report Manager  &#40;SSRS Native Mode&#41;](../../2014/reporting-services/report-manager-ssrs-native-mode.md)
 [HTML Viewer and the Report Toolbar](html-viewer-and-the-report-toolbar.md)
 [URL Access Parameter Reference](url-access-parameter-reference.md)  
