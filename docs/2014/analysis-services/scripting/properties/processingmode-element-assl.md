---
title: "ProcessingMode Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "ProcessingMode Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "ProcessingMode"
helpviewer_keywords: 
  - "ProcessingMode element"
ms.assetid: dff6eeba-f09c-4d8c-ad81-caef76254af0
author: minewiskan
ms.author: owend
manager: craigg
---
# ProcessingMode Element (ASSL)
  Indicates whether the instance should index and aggregate during or after processing.  
  
## Syntax  
  
```xml  
  
<Cube> <!-- or Dimension, MeasureGroup, Partition -->  
   ...  
   <ProcessingMode>...</ProcessingMode>  
   ...  
</Cube>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|*Regular*|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Cube](../objects/cube-element-assl.md), [Dimension](../objects/dimension-element-assl.md), [MeasureGroup](../objects/group-element-assl.md), [Partition](../objects/partition-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of `ProcessingMode` on the `Cube` provides the default for the cube, and can be overridden by setting `ProcessingMode` for each partition.  
  
 The value of this element is limited to one of the strings listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*Regular*|The instance indexes and performs aggregations during processing.|  
|*LazyOptimizations*|The instance indexes and performs aggregations after processing.|  
  
 The enumeration that corresponds to the allowed values for `ProcessingMode` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.ProcessingMode>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
