---
title: Browser Support for Reporting Services and Power View
description: Learn about what browser versions are supported for managing and viewing SQL Server Reporting Services, the ReportViewer Controls and Power View.
ms.date: 06/11/2026
ms.service: reporting-services
ms.subservice: reporting-services
ms.topic: concept-article
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "displaying reports"
  - "scripts [Reporting Services], requirements"
  - "viewing reports"
  - "browsers [Reporting Services]"
  - "Web browsers [Reporting Services], about browser support"
  - "browsing reports [Reporting Services]"
  - "components [Reporting Services], browsers"
  - "Web browsers [Reporting Services]"
---
# Browser support for Reporting Services and Power View

[!INCLUDE [ssrs-appliesto](../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-2016-and-later](../includes/ssrs-appliesto-2016-and-later.md)] [!INCLUDE [ssrs-appliesto-pbirsi](../includes/ssrs-appliesto-pbirs.md)]

Learn about what browser versions are supported for managing and viewing SQL Server Reporting Services (SSRS), the ReportViewer Controls, and Power View.

> [!NOTE]  
> In [!INCLUDE [sssql17-md](../includes/sssql17-md.md)] and later versions, Reporting Services integration with SharePoint isn't available.
>
> In [!INCLUDE [sssql17-md](../includes/sssql17-md.md)] and later versions, Power View support isn't available.
>
> Support for the Microsoft Edge legacy browser ended on March 9, 2021. Support for Microsoft Internet Explorer 11 ended on August 17, 2021.

## Browser requirements for the web portal

Review the following list of supported browsers for the web portal.

### Microsoft Windows

*Windows 11; Windows Server 2016, 2019, 2022, 2025*

Latest publicly released versions of:

- Microsoft Edge
- Google Chrome
- Mozilla Firefox

### Apple macOS

*macOS 26*

Latest publicly released versions of:

- Apple Safari
- Google Chrome
- Mozilla Firefox

### Apple iOS

*iPhone and iPad with iOS 26*

Latest publicly released version of:

- Apple Safari

### Google Android

*Phones and tablets with Android 4.4 (KitKat) or later versions*

Latest publicly released version of:

- Google Chrome

## Browser requirements for the ReportViewer web control (2015)

The following list shows the current browsers that the ReportViewer web control (2015) supports. The report viewer supports viewing reports from [!INCLUDE [ssRSnoversion](../includes/ssrsnoversion-md.md)] web portal and SharePoint libraries.

### Microsoft Windows

*Windows 11; Windows Server 2016, 2019, 2022, 2025*

Latest publicly released versions of:

- Microsoft Edge
- Google Chrome
- Mozilla Firefox

### Apple macOS

*macOS 26*

Latest publicly released version of:

- Apple Safari

### Authentication requirements

Browsers support specific authentication schemes that the report server must handle for the client request to succeed. The following table identifies the default authentication types supported by each browser running on a Windows operating system.

| Browser type | Supports | Browser default | Server default |
| --- | --- | --- | --- |
| **Microsoft Edge**<sup>1</sup> | Negotiate, NTLM, Basic | Negotiate | Yes. The default authentication settings work with Edge. |
| **Google Chrome**<sup>1</sup> | Negotiate, NTLM, Basic | Negotiate | Yes. The default authentication settings work with Chrome. |
| **Mozilla Firefox**<sup>1</sup> | NTLM, Basic | NTLM | Yes. The default authentication settings work with Firefox. |
| **Apple Safari**<sup>1</sup> | NTLM, Basic | Basic | Yes. The default authentication settings work with Safari. |

<sup>1</sup> Latest publicly released version.

### Script requirements for viewing reports

To use the report viewer, configure your browser to run scripts.

If scripting isn't enabled, you see an error message similar to the following when you open a report:

```output
Your browser does not support scripts or has been configured to not allow scripts to run. Click here to view this report without scripts.
```

If you choose to view the report without script support, the report is rendered in HTML without report viewer capabilities such as the report toolbar and the document map.

> [!NOTE]  
> The report toolbar is part of the HTML Viewer component. By default, the toolbar appears at the top of every report that is rendered in a browser window. The report viewer features include the ability to search the report for information, scroll to a specific page, and adjust the page size for viewing purposes. For more information about the report toolbar or HTML Viewer, see [HTML Viewer and the report toolbar](html-viewer-and-the-report-toolbar.md).

## Browser support for ReportViewer web server controls in Visual Studio

Use the ReportViewer web server control to embed report functionality in an ASP.NET web application. Visual Studio includes the controls, and they support different browsers and browser versions than the other components described in this article. The type of browser you use to view the application determines the kind of ReportViewer functionality that you can provide in your application. Review the following lists to determine which supported browsers and platforms work with the ReportViewer web server controls.

Use a browser that has script support enabled. If the browser can't run scripts, you can't view the report.

### Microsoft Windows

*Windows 11; Windows Server 2016, 2019, 2022, 2025*

Latest publicly released versions of:

- Microsoft Edge
- Google Chrome
- Mozilla Firefox

### Apple macOS

*macOS 26*

Latest publicly released version of:

- Apple Safari

## Related content

- [Find and view reports in the web portal (Report Builder and SSRS)](report-builder/finding-and-viewing-reports-in-the-web-portal-report-builder-and-ssrs.md)
- [SQL Server Reporting Services tools](tools/reporting-services-tools.md)
- [What is the report server web portal (Native mode)?](web-portal-ssrs-native-mode.md)
- [HTML Viewer and the report toolbar](html-viewer-and-the-report-toolbar.md)
- [URL access parameter reference](url-access-parameter-reference.md)
- [Try asking the Reporting Services forum](/answers/search.html?c=&f=&includeChildren=&q=ssrs+OR+reporting+services&redirect=search%2fsearch&sort=relevance&type=question+OR+idea+OR+kbentry+OR+answer+OR+topic+OR+user)
