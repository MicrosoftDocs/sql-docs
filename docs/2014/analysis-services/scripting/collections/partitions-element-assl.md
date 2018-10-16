---
title: "Partitions Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Partitions Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "Partitions"
helpviewer_keywords: 
  - "Partitions element"
ms.assetid: e41c97ca-da44-48e9-a454-d25ee74209fd
author: minewiskan
ms.author: owend
manager: craigg
---
# Partitions Element (ASSL)
  Contains the collection of [Partition](../objects/partition-element-assl.md) elements used by a [MeasureGroup](../objects/group-element-assl.md) element, or the collection of partition bindings that make up an out-of-line [MeasureGroupBinding](../data-type/measuregroupbinding-data-type-out-of-line-assl.md) element.  
  
## Syntax  
  
```xml  
  
<MeasureGroup> <!-- or MeasureGroupBinding -->  
   ...  
   <Partitions>  
      <Partition>...</Partition> <!-- parent: MeasureGroup -->  
      <!-- or -->  
      <Partition xsi:type="PartitionBinding">...</Partition> <!-- parent: MeasureGroupBinding -->  
   </Partitions>  
   ...  
</MeasureGroup>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[MeasureGroup](../objects/group-element-assl.md), [MeasureGroupBinding](../data-type/measuregroupbinding-data-type-out-of-line-assl.md)|  
  
|Ancestor or Parent|Child Element|  
|------------------------|-------------------|  
|[MeasureGroup](../objects/group-element-assl.md)|[Partition](../objects/partition-element-assl.md)|  
|[MeasureGroupBinding](../data-type/measuregroupbinding-data-type-out-of-line-assl.md)|[Partition](../objects/partition-element-assl.md) of type [PartitionBinding](../data-type/binding-data-type-assl.md)|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.PartitionCollection>.  
  
## See Also  
 [Collections &#40;ASSL&#41;](collections-assl.md)  
  
  
