---
title: "Browser Support for Reporting Services and Power View | Microsoft Docs"
ms.date: 07/02/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: reporting-services


ms.topic: conceptual
helpviewer_keywords: 
  - "displaying reports"
  - "scripts [Reporting Services], requirements"
  - "viewing reports"
  - "browsers [Reporting Services]"
  - "Web browsers [Reporting Services], about browser support"
  - "browsing reports [Reporting Services]"
  - "components [Reporting Services], browsers"
  - "Web browsers [Reporting Services]"
ms.assetid: 48a75bbb-0029-4c43-891d-dc8f4fc0ebe1
author: markingmyname
ms.author: maghan
---
# Browser Support for Reporting Services and Power View

[!INCLUDE[ssrs-appliesto](../includes/ssrs-appliesto.md)] [!INCLUDE[ssrs-appliesto-2016-and-later](../includes/ssrs-appliesto-2016-and-later.md)] [!INCLUDE[ssrs-appliesto-pbirsi](../includes/ssrs-appliesto-pbirs.md)]

Learn about what browser versions are supported for managing and viewing SQL Server Reporting Services, the ReportViewer Controls and Power View.

> [!NOTE]
> Reporting Services integration with SharePoint is no longer available after SQL Server 2016.

## Browser requirements for the web portal

The following is the current list of browsers supported for the web portal.

**Microsoft Windows**  
*Windows 7, 8.1, 10; Windows Server 2008 R2, 2012, 2012 R2*
- Microsoft Edge (+)
- Microsoft Internet Explorer 10 or 11
- Google Chrome (+)
- Mozilla Firefox (+)

**Apple OS X**  
*OS X 10.9-10.11*

- Apple Safari (+)
- Google Chrome (+)
- Mozilla Firefox (+)

**Apple iOS**  
*iPhone and iPad with iOS 9*

- Apple Safari (+)

**Google Android**  
*Phones and tablets with Android 4.4 (KitKat) or later*

- Google Chrome (+)

 **(+)** Latest publicly released version

## Browser requirements for the ReportViewer web control (2015)

 The following is the current list of browsers supported with the ReportViewer web control (2015). The report viewer supports viewing reports from [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] web portal and SharePoint libraries.  

**Microsoft Windows**  
*Windows 7, 8.1, 10; Windows Server 2008 R2, 2012, 2012 R2*

- Microsoft Edge (+)
- Microsoft Internet Explorer 10 or 11
- Google Chrome (+)
- Mozilla Firefox (+)

**Apple OS X**  
*OS X 10.9-10.11*

- Apple Safari (+)

 **(+)** Latest publicly released version

 If you are using a SharePoint product that is integrated with [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)], see  [Plan browser support in SharePoint 2016](https://technet.microsoft.com//library/cc263526\(v=office.16\).aspx).

### Authentication requirements

 Browsers support specific authentication schemes that must be handled by the report server in order for the client request to succeed. The following table identifies the default authentication types supported by each browser running on a Windows operating system.

|**Browser type**|**Supports**|**Browser default**|**Server default**|
|----------------------|------------------|-------------------------|------------------------|
|**Microsoft Edge** (+)|Negotiate, Kerberos, NTLM, Basic|Negotiate|Yes. The default authentication settings work with Edge.|
|**Microsoft Internet Explorer**|Negotiate, Kerberos, NTLM, Basic|Negotiate|Yes. The default authentication settings work with Internet Explorer.|
|**Google Chrome**(+)|Negotiate, NTLM, Basic|Negotiate|Yes. The default authentication settings work with Chrome.|
|**Mozilla Firefox**(+)|NTLM, Basic|NTLM|Yes. The default authentication settings work with Firefox.|
|**Apple Safari**(+)|NTLM, Basic|Basic|Yes. The default authentication settings work with Safari.|

 **(+)** Latest publicly released version

### Script requirements for viewing reports

 To use the report viewer, configure your browser to run scripts.

 If scripting is not enabled, you will see an error message similar to the following when you open a report:

- **Your browser does not support scripts or has been configured to not allow scripts to run. Click here to view this report without scripts**.

 If you choose to view the report without script support, the report is rendered in HTML without report viewer capabilities such as the report toolbar and the document map.

> [!NOTE]
> The report toolbar is part of the HTML Viewer component. By default the toolbar appears at the top of every report that is rendered in a browser window. The report viewer provides features include the ability to search the report for information, scroll to a specific page, and adjust the page size for viewing purposes. For more information about the report toolbar or HTML Viewer, see [HTML Viewer and the Report Toolbar](../reporting-services/html-viewer-and-the-report-toolbar.md).

## Browser support for ReportViewer web server controls in Visual Studio

 The ReportViewer Web server control is used to embed report functionality in an ASP.NET web application. The controls are included with Visual Studio and support different browsers and browser versions than the other components described in this topic. The type of browser used to view the application determines the kind of ReportViewer functionality that you can provide in your application. Use the table provided in this topic to determine which of the supported browsers are subject to report functionality restrictions and the supported platforms.  

 Use a browser that has script support enabled. If the browser cannot run scripts, you cannot view the report.

**Microsoft Windows**  
*Windows 7, 8.1, 10; Windows Server 2008 R2, 2012, 2012 R2*

- Microsoft Edge (+)
- Microsoft Internet Explorer 10 or 11
- Google Chrome (+)
- Mozilla Firefox (+)

 **(+)** Latest publicly released version

## Power View browser support

**Microsoft Windows**  
*Windows 7, 8.1, 10; Windows Server 2008 R2, 2012, 2012 R2*

- Microsoft Internet Explorer 10 or 11
- Mozilla Firefox (+)

**Apple OS X**  
*OS X 10.9-10.11*

- Apple Safari (+)

 **(+)** Latest publicly released version

 For more information on the SharePoint 2016 browser support, see [Plan browser support in SharePoint 2013](https://technet.microsoft.com//library/cc263526\(v=office.16\).aspx).

## Next steps

[Finding and Viewing Reports in the web portal](report-builder/finding-and-viewing-reports-in-the-web-portal-report-builder-and-ssrs.md)  
[Reporting Services Tools](../reporting-services/tools/reporting-services-tools.md)  
[Web portal (SSRS Native Mode)](https://msdn.microsoft.com/7349e626-6ed5-4d21-b05f-cf042ad9ad70)  
[HTML Viewer and the Report Toolbar](../reporting-services/html-viewer-and-the-report-toolbar.md)  
[URL Access Parameter Reference](../reporting-services/url-access-parameter-reference.md)  

More questions? [Try asking the Reporting Services forum](https://go.microsoft.com/fwlink/?LinkId=620231)

