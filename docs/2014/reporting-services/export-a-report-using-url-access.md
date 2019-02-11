---
title: "Export a Report Using URL Access | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "formats [Reporting Services], URL rendering"
  - "URL access [Reporting Services], rendering formats"
ms.assetid: 6a3b7fc3-3d91-4d12-8371-42ea12e74517
author: markingmyname
ms.author: maghan
manager: kfile
---
# Export a Report Using URL Access
  You can optionally specify the format in which to render a report by using the *rs:Format* parameter. For example, to get a PDF copy of a report directly from a native mode report server:  
  
```  
http://myrshost/ReportServer?/myreport&rs:Format=PDF  
```  
  
 And, from a SharePoint integrated mode report server:  
  
```  
http://myspsite/subsite/_vti_bin/reportserver?http://myspsite/subsite/myrereport.rdl&rs:Format=PDF  
```  
  
 Valid values for this parameter are based on the report rendering extensions that are installed on the report server being accessed. Common extensions are HTML4.0, MHTML, IMAGE, EXCEL, WORD, CSV, PDF, XML, and NULL. If a specified rendering extension is not installed on the report server, the report is not rendered and an error is generated and displayed in the browser.  
  
 If you do not include the *Format* parameter as part of the URL, the report server detects the browser and renders the report in the appropriate HTML format.  
  
## See Also  
 [URL Access &#40;SSRS&#41;](url-access-ssrs.md)   
 [URL Access Parameter Reference](url-access-parameter-reference.md)  
  
  
