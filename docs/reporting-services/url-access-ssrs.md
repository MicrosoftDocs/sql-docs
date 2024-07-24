---
title: "URL access"
description: Learn how URL access of the report server in SQL Server Reporting Services (SSRS) enables you to send commands to a report server through a URL request.
author: maggiesMSFT
ms.author: maggies
ms.date: 07/24/2024
ms.service: reporting-services
ms.subservice: reporting-services
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "URL access [Reporting Services]"
  - "links [Reporting Services], URL access"
  - "URL access [Reporting Services], about URL access"
  - "parameters [Reporting Services], URL access"
  - "report servers [Reporting Services], URL access"
  - "hyperlinks [Reporting Services]"
#customer-intent: As a SQL Server Reporting Services report server user,  I want to send commands through a URL request so that I can customize reports on a native mode report server or in a SharePoint library.
---
# URL access (SSRS)

URL access of the report server in SQL Server Reporting Services (SSRS) enables you to send commands to a report server through a URL request. This functionality lets you customize how you render reports on a native mode report server or in a SharePoint library. For example, you might view the report by using a specific set of report parameter values, or you might view a particular page of interest in the report. You can define these parameters in a URL by using predefined URL access parameters. 

You can customize how the report server processes the report by including parameters for rendering formats or for specifying the look and feel of the report viewer. You can then send this URL to others so they can access your report in the same manner in the browser.

Other actions you can perform through URL access include:

- [Access report server items by using URL access](../reporting-services/access-report-server-items-using-url-access.md).
- [Specify device information settings in a URL](../reporting-services/specify-device-information-settings-in-a-url.md)
- [Set the language for report parameters in a URL](../reporting-services/set-the-language-for-report-parameters-in-a-url.md)
- [Render a report history snapshot by using URL access](../reporting-services/render-a-report-history-snapshot-using-url-access.md).
- [Search a report by using URL access](../reporting-services/search-a-report-using-url-access.md).

## URL access concepts

The report server processes parameters contained in URL requests to the report server. How the report server handles URL requests depends on the parameters, parameter prefixes, and types of items included in the URL. Report server URLs adhere to the URL formatting guidelines proposed by the joint World Wide Web Consortium W3C/IETF draft standard. [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] URL functionality is compatible with most internet browsers or applications that support standard URL addressing.

## URL access syntax

URL requests can contain multiple parameters that are listed in any order. You separate the parameters by using an ampersand (`&`) and the name/value pairs by an equal sign (`=`). 

Understanding the different components of a URL access string is essential for effectively sending commands to an SSRS report server. The URL access string includes: 

- `https://[rswebserviceurl]`: The web service URL of the report server.
- `?`: Indicates the start of the query string that contains parameters.
- `[pathinfo]`: The path to the report or item on the server.
- `[&prefix:param=value]`: One or more parameters with optional prefixes that customize the report rendering or behavior.
```
https://[rswebserviceurl]?[pathinfo][&prefix:param=value]...[&prefix:param=value]
```

## Syntax description

The following section provides a detailed description of some parameters used in the URL access string. For a complete list, see [URL access parameter reference](../reporting-services/url-access-parameter-reference.md). 

### *rswebserviceurl*

The web service URL of the report server. 

- For native mode, it's the web service report server instance web service URL configured in Reporting Services Configuration Manager. For more information, see [Configure report server URLs &#40;Report Server Configuration Manager&#41;](../reporting-services/install-windows/configure-report-server-urls-ssrs-configuration-manager.md). 

   For example:

   ```
   https://myrshost/reportserver
   https://machine.adventure-works.com/reportserver_MYNAMEDINSTANCE
   ```

- For SharePoint integrated mode, it's the URL of the [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] proxy at a SharePoint site integrated with [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)].

   For example:

   ```
   https://myspsite/subsite/_vti_bin/reportserver
   ```

   > [!TIP]
   > It's important the URL include the `_vti_bin` proxy syntax to route the request through SharePoint and the [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] HTTP proxy. The proxy adds some context to the HTTP request, context that'ss required to ensure proper execution of the report for SharePoint mode report servers.

### *pathinfo*

The relative path name of the item in the native mode report server database, or the fully qualified URL of the item in a SharePoint catalog.

The path of the catalog item. For native mode, it's the relative path of the item in the report server database, beginning with a slash (`/`). For example:

```
/AdventureWorks2022/Employee_Sales_Summary_2022
```

For SharePoint integrated mode, it's the fully qualified URL of the item in the SharePoint library, including the item extension. For example:

```
https://myspsite/subsite/AdventureWorks2022/Employee_Sales_Summary_2022.rdl
```

### *&prefix:param=value*

In a URL access string, you add parameters and their corresponding values to customize the report rendering or behavior. You construct parameters in name/value pairs by using the syntax `param=value`, separated by an ampersand (`&`).

Use optional prefixes like `rs:` or `rc:` to target specific processes within the report server. 

> [!Note]
> If a prefix for a URL access parameter isn't included, the parameter is processed by the report server as a report parameter. Report parameters don't use a parameter prefix and are case-sensitive.

The following example shows the complete URL as described in this article:

```
https://myrshost/reportserver?/AdventureWorks2022/Employee_Sales_Summary_2022&rs:Command=Render&rc:Toolbar=false&ReportMonth=3&ReportYear=2008
```

## Related content

- [Integrate Reporting Services by using URL access](../reporting-services/application-integration/integrating-reporting-services-using-url-access.md)
- [Find, view, and manage reports &#40;Report Builder and SSRS&#41;](../reporting-services/report-builder/finding-viewing-and-managing-reports-report-builder-and-ssrs.md)
- [Pass a report parameter within a URL](../reporting-services/pass-a-report-parameter-within-a-url.md)
- [Export a report by using URL access](../reporting-services/export-a-report-using-url-access.md)
