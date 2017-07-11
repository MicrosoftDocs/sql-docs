---
title: "Search a Report Using URL Access | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "reporting-services-sharepoint"
  - "reporting-services-native"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "searching reports"
  - "text searches [Reporting Services]"
  - "URL access [Reporting Services], report searches"
ms.assetid: 6f3410c4-7944-448f-bae8-bab3e8152d46
caps.latest.revision: 34
author: "guyinacube"
ms.author: "asaxton"
manager: "erikre"
---
# Search a Report Using URL Access
  You can search a report for a specific set of text using URL access. To search a report, set the value of the *rc:FindString* parameter on the URL equal to the text for which you want to search. Additionally, use the *rc:StartFind* and *rc:EndFind* parameters to narrow your search to specific pages within the report.  
  
## Example  
 The following URL access example searches for the first occurrence of the text "Mountain-400" in the Product Catalog sample report starting with page one and ending with page five:  
  
```  
http://server/Reportserver?/SampleReports/Product Catalog&rs:Command=Render&rc:StartFind=1&rc:EndFind=5&rc:FindString=Mountain-400  
```  
  
## See Also  
 [URL Access &#40;SSRS&#41;](../reporting-services/url-access-ssrs.md)   
 [URL Access Parameter Reference](../reporting-services/url-access-parameter-reference.md)  
  
  