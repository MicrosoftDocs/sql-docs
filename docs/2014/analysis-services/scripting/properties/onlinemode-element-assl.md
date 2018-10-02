---
title: "OnlineMode Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "OnlineMode Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "OnlineMode"
helpviewer_keywords: 
  - "OnlineMode element"
ms.assetid: 0bbac4e2-002f-4be4-8dd6-ccd7034f5f93
author: minewiskan
ms.author: owend
manager: craigg
---
# OnlineMode Element (ASSL)
  Specifies whether the database is brought back online immediately when the rebuilding of the cache is initiated, or only when the rebuilding of the cache is complete.  
  
## Syntax  
  
```xml  
  
<ProactiveCaching>  
   ...  
   <OnlineMode>...</OnlineMode>  
   ...  
</ProactiveCaching>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|*Immediate*|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[ProactiveCaching](../objects/proactivecaching-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of this element is limited to one of the strings listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*Immediate*|Database is brought back online immediately when the rebuilding of the cache is initiated.|  
|*OnCacheComplete*|Database is brought back online only when the rebuilding of the cache is complete.|  
  
 The enumeration that corresponds to the allowed values for `OnlineMode` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.ProactiveCaching>.  
  
## See Also  
 [ProactiveCaching Element &#40;ASSL&#41;](../objects/proactivecaching-element-assl.md)  
  
  
