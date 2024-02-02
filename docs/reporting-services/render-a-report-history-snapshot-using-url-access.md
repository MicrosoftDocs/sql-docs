---
title: "Render a report history snapshot using URL access"
description: "Learn how to render a report based on a report history snapshot by supplying the rs:Snapshot parameter and setting its value to a valid snapshot ID."
author: maggiesMSFT
ms.author: maggies
ms.date: 03/01/2017
ms.service: reporting-services
ms.subservice: reporting-services
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "URL access [Reporting Services], report history"
  - "history snapshots [Reporting Services]"
  - "historical data [Reporting Services]"
  - "snapshots [Reporting Services], URL access"
  - "snapshots [Reporting Services], rendering report history"
---
# Render a report history snapshot using URL access
  You can render a report based on a report history snapshot by supplying the ``rs:Snapshot`` parameter and setting its value to a valid snapshot ID. The parameter value is in the format YYYY-MM-DDTHH:MM:SS, based on the International Organization for Standardization (ISO) 8601 standard.  
  
 If you omit this parameter, the report is rendered according to the report execution and cache management option settings of the report server. For more information about report execution, see [Set report processing properties](../reporting-services/report-server/set-report-processing-properties.md).  
  
## Example  
 The following example shows a URL that retrieves a report history snapshot:  
  
```  
https://myrshost/reportserver?/SampleReports/Company Sales&rs:Snapshot=2003-04-07T13:40:02  
```  
  
## Related content

- [URL access &#40;SSRS&#41;](../reporting-services/url-access-ssrs.md)   
- [URL access parameter reference](../reporting-services/url-access-parameter-reference.md)  
  
  
