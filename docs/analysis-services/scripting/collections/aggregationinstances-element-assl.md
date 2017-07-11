---
title: "AggregationInstances Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "AggregationInstances Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
helpviewer_keywords: 
  - "AggregationInstances element"
ms.assetid: e8321aa8-361b-4d8a-bd89-a596eeb814b1
caps.latest.revision: 11
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# AggregationInstances Element (ASSL)
  Contains the collection of aggregation instances that are defined in a [Partition](../../../analysis-services/scripting/objects/partition-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<Partition>  
   ...  
   <AggregationInstances>  
      <AggregationInstance>...</AggregationInstance>  
   </AggregationInstances>  
   ...  
</Partition>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None (collection)|  
|Default value|None (collection)|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Partition](../../../analysis-services/scripting/objects/partition-element-assl.md)|  
|Child elements|[AggregationInstance](../../../analysis-services/scripting/objects/aggregationinstance-element-assl.md)|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.AggregationInstanceCollection>.  
  
## See Also  
 [Collections &#40;ASSL&#41;](../../../analysis-services/scripting/collections/collections-assl.md)  
  
  