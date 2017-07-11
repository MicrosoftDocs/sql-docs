---
title: "OnlineMode Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "OnlineMode Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "OnlineMode"
helpviewer_keywords: 
  - "OnlineMode element"
ms.assetid: 0bbac4e2-002f-4be4-8dd6-ccd7034f5f93
caps.latest.revision: 30
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
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
|Parent element|[ProactiveCaching](../../../analysis-services/scripting/objects/proactivecaching-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of this element is limited to one of the strings listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*Immediate*|Database is brought back online immediately when the rebuilding of the cache is initiated.|  
|*OnCacheComplete*|Database is brought back online only when the rebuilding of the cache is complete.|  
  
 The enumeration that corresponds to the allowed values for **OnlineMode** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.ProactiveCaching>.  
  
## See Also  
 [ProactiveCaching Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/proactivecaching-element-assl.md)  
  
  