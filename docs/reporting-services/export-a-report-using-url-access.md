---
title: "Export a report by using the rs:Format URL parameter"
description: "Learn how to export a report from SQL Server Reporting Services (SSRS) in different file formats by using the rs:Format URL parameter."
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: reporting-services
ms.topic: conceptual
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "formats [Reporting Services], URL rendering"
  - "URL access [Reporting Services], rendering formats"
# customer intent: As a SQL Server report user or administrator, I want to learn about the rs:Format parameter so I can export reports in different file formats.
---
# Export a report by using URL access

Learn how you can export reports from SQL Server Reporting Services (SSRS) by using URL access and the `rs:Format` URL parameter. This method allows you to render reports in various file formats, such as PDF and PPTX, directly from the report server.

## Prerequisites

- Access to an SSRS report server.
- URLs of the reports you want to export.
- Appropriate rendering extensions installed on your report server.

## Specify the export format

Specify the format in which to render a report by using the `rs:Format` URL parameter. The HTML4.0 and HTML 5 formats render in the browser, while other formats prompt you to save the report output to a local file. 

## Export a PDF report 
  
To get a PDF copy of a report directly from a native mode report server, use the following URL command in your browser:  
  
```  
https://myrshost/ReportServer?/myreport&rs:Format=PDF  
```  

::: moniker range="=sql-server-2016"
  
And, from a SharePoint integrated mode report server, use the following URL command in your browser:  
  
```  
https://myspsite/subsite/_vti_bin/reportserver?https://myspsite/subsite/myrereport.rdl&rs:Format=PDF  
```  
 
::: moniker-end

## Export a PPTX report
 
To export a PPTX report from a named instance of the report server, use the following URL command in your browser:  
  
```  
https://servername/ReportServer_THESQLINSTANCE/Pages/ReportViewer.aspx?%2freportfolder%2freport+name+with+spaces&rs:Format=pptx  
```  

## Common values for the `rs:Format` parameter

The `rs:Format` parameter accepts the following common values based on the report rendering extensions installed on the report server:

- HTML4.0
- HTML5
- MHTML
- IMAGE
- EXCELOPENXML (xlsx)
- WORDOPENXML (docx)
- CSV
- PDF
- XML
- NULL

If a specified rendering extension isn't installed on the report server, an error displays in the browser.  
  
If you don't include the `rs:Format` parameter as part of the URL, the report server detects the browser and renders the report in the appropriate HTML format.  
  
## Related content

- [URL access &#40;SSRS&#41;](../reporting-services/url-access-ssrs.md)
- [URL access parameter reference](../reporting-services/url-access-parameter-reference.md)
