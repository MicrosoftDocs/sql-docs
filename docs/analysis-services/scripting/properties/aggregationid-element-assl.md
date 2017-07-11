---
title: "AggregationID Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "AggregationID Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
helpviewer_keywords: 
  - "AggregationID element"
ms.assetid: 6056da1d-b6b4-4074-84db-45be719df49a
caps.latest.revision: 12
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# AggregationID Element (ASSL)
  Identifies the aggregation definition from the [AggregationDesign](../../../analysis-services/scripting/objects/aggregationdesign-element-assl.md) element used to create the aggregation instance.  
  
## Syntax  
  
```xml  
  
<AggregationInstance>  
   ...  
   <AggregationID>...</AggregationID>  
   ...  
</AggregationInstance>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[AggregationInstance](../../../analysis-services/scripting/objects/aggregationinstance-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 If this element is missing or set to a blank string, the **AggregationInstance** represents a user-defined aggregation.  
  
 The element that corresponds to the parent of **AggregationID** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.AggregationInstance>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  