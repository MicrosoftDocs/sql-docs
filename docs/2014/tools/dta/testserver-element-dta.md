---
title: "TestServer Element (DTA) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: tools-other
ms.topic: conceptual
dev_langs: 
  - "XML"
helpviewer_keywords: 
  - "TestServer element"
ms.assetid: caa3547a-2cd5-47ad-ace2-a36752835cfe
author: stevestein
ms.author: sstein
manager: craigg
---
# TestServer Element (DTA)
  Specifies the test server to use when tuning a production server.  
  
## Syntax  
  
```  
  
<TuningOptions>  
  <TestServer>...</TestServer>  
   ...code removed here...  
</TuningOptions>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|**Data type and length**|**string**, unlimited length.|  
|**Default value**|None.|  
|**Occurrence**|Optional. Can use once for each `TuningOptions` element.|  
  
## Element Relationships  
  
|Relationship|Elements|  
|------------------|--------------|  
|**Parent element**|[TuningOptions Element &#40;DTA&#41;](tuningoptions-element-dta.md)|  
|**Child elements**|None.|  
  
## Example  
 For a usage example of this element, see [Reduce the Production Server Tuning Load](../../relational-databases/performance/reduce-the-production-server-tuning-load.md).  
  
## See Also  
 [XML Input File Reference &#40;Database Engine Tuning Advisor&#41;](xml-input-file-reference-database-engine-tuning-advisor.md)  
  
  
