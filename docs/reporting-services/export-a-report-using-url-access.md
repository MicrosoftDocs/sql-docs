---
title: "Export a report by using URL access"
description: "Learn how to export a report using URL access by specifying the format in which to render a report by using the rs:Format URL parameter."
author: maggiesMSFT
ms.author: maggies
ms.date: 03/01/2017
ms.service: reporting-services
ms.subservice: reporting-services
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "formats [Reporting Services], URL rendering"
  - "URL access [Reporting Services], rendering formats"
---
# Export a report by using URL access
  You can optionally specify the format in which to render a report by using the ``rs:Format`` URL parameter.  The HTML4.0 and HTM5 formats (rendering extension) render in the browser and for other formats, the browser prompts to save the report output to a local file.  
  
 For example, to get a PDF copy of a report directly from a native mode report server:  
  
```  
https://myrshost/ReportServer?/myreport&rs:Format=PDF  
```  

::: moniker range="=sql-server-2016"
  
 And, from a SharePoint integrated mode report server:  
  
```  
https://myspsite/subsite/_vti_bin/reportserver?https://myspsite/subsite/myrereport.rdl&rs:Format=PDF  
```  
 
::: moniker-end
 
 For example the following  URL command in your browser exports a PPTX report from a named instance of the report server:  
  
```  
https://servername/ReportServer_THESQLINSTANCE/Pages/ReportViewer.aspx?%2freportfolder%2freport+name+with+spaces&rs:Format=pptx  
```  
  
 Valid values for this parameter are based on the report rendering extensions that are installed on the report server being accessed. Common extensions are HTML4.0, MHTML, IMAGE, EXCELOPENXML (xlsx), WORDOPENXML (docx), CSV, PDF, XML, and NULL. If a specified rendering extension isn't installed on the report server, the report isn't rendered and an error is generated and displayed in the browser.  
  
 If you don't include the *Format* parameter as part of the URL, the report server detects the browser and renders the report in the appropriate HTML format.  
  
## Related content

- [URL access &#40;SSRS&#41;](../reporting-services/url-access-ssrs.md)   
- [URL access parameter reference](../reporting-services/url-access-parameter-reference.md)  
  
  
