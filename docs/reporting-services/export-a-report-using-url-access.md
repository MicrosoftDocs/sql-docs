---
title: "Export a Report Using URL Access | Microsoft Docs"
ms.date: 03/01/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: reporting-services


ms.topic: conceptual
helpviewer_keywords: 
  - "formats [Reporting Services], URL rendering"
  - "URL access [Reporting Services], rendering formats"
ms.assetid: 6a3b7fc3-3d91-4d12-8371-42ea12e74517
author: maggiesMSFT
ms.author: maggies
---
# Export a Report Using URL Access
  You can optionally specify the format in which to render a report by using the *rs:Format* URL parameter.  The HTML4.0 and HTM5 formats (rendering extension) will render in the browser and for other formats, the browser will prompt to save the report output to a local file.  
  
 For example, to get a PDF copy of a report directly from a native mode report server:  
  
```  
https://myrshost/ReportServer?/myreport&rs:Format=PDF  
```  
  
 And, from a SharePoint integrated mode report server:  
  
```  
https://myspsite/subsite/_vti_bin/reportserver?https://myspsite/subsite/myrereport.rdl&rs:Format=PDF  
```  
  
 For example the following  URL command in your browser exports a PPTX report from a named instance of the report server:  
  
```  
https://servername/ReportServer_THESQLINSTANCE/Pages/ReportViewer.aspx?%2freportfolder%2freport+name+with+spaces&rs:Format=pptx  
```  
  
 Valid values for this parameter are based on the report rendering extensions that are installed on the report server being accessed. Common extensions are HTML4.0, MHTML, IMAGE, EXCELOPENXML (xlsx) , WORDOPENXML (docx), CSV, PDF, XML, and NULL. If a specified rendering extension is not installed on the report server, the report is not rendered and an error is generated and displayed in the browser.  
  
 If you do not include the *Format* parameter as part of the URL, the report server detects the browser and renders the report in the appropriate HTML format.  
  
## See Also  
 [URL Access &#40;SSRS&#41;](../reporting-services/url-access-ssrs.md)   
 [URL Access Parameter Reference](../reporting-services/url-access-parameter-reference.md)  
  
  
