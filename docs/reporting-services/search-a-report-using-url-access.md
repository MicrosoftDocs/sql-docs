---
title: Search a report by using URL access
description: "Learn how to search a report using URL access. For example, set the rc:FindString parameter on the URL equal to the text for which you want to search."
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: reporting-services
ms.topic: how-to
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "searching reports"
  - "text searches [Reporting Services]"
  - "URL access [Reporting Services], report searches"
---
# Search a report by using URL access
  You can search a report for a specific set of text using URL access. To search a report, set the value of the *rc:FindString* parameter on the URL equal to the text for which you want to search. Additionally, use the *rc:StartFind* and *rc:EndFind* parameters to narrow your search to specific pages within the report.  
  
## Example  
 The following URL access example searches for the first occurrence of the text "Mountain-400" in the Product Catalog sample report starting with page one and ending with page five:  
  
```  
https://server/Reportserver?/SampleReports/Product Catalog&rs:Command=Render&rc:StartFind=1&rc:EndFind=5&rc:FindString=Mountain-400  
```  
  
## Related content

- [URL access (SSRS)](url-access-ssrs.md)
- [URL access parameter reference](url-access-parameter-reference.md)
