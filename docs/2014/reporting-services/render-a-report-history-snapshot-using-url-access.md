---
title: "Render a Report History Snapshot Using URL Access | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "URL access [Reporting Services], report history"
  - "history snapshots [Reporting Services]"
  - "historical data [Reporting Services]"
  - "snapshots [Reporting Services], URL access"
  - "snapshots [Reporting Services], rendering report history"
ms.assetid: 3f87f82d-0e61-4492-9c4b-f5238c39e8cd
author: markingmyname
ms.author: maghan
manager: kfile
---
# Render a Report History Snapshot Using URL Access
  You can render a report based on a report history snapshot by supplying the *rs:Snapshot* parameter and setting its value to a valid snapshot ID. The parameter value is in the format YYYY-MM-DDTHH:MM:SS, based on the International Organization for Standardization (ISO) 8601 standard.  
  
 If you omit this parameter, the report is rendered according to the report execution and cache management option settings of the report server. For more information about report execution, see [Set Report Processing Properties](report-server/set-report-processing-properties.md).  
  
## Example  
 The following example shows a URL that retrieves a report history snapshot:  
  
```  
http://myrshost/reportserver?/SampleReports/Company Sales&rs:Snapshot=2003-04-07T13:40:02  
```  
  
## See Also  
 [URL Access &#40;SSRS&#41;](url-access-ssrs.md)   
 [URL Access Parameter Reference](url-access-parameter-reference.md)  
  
  
