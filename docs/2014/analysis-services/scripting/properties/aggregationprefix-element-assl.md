---
title: "AggregationPrefix Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "AggregationPrefix Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "AggregationPrefix"
helpviewer_keywords: 
  - "AggregationPrefix element"
ms.assetid: 1581e0df-ae8e-41ce-9c92-f0f7cac487f2
author: minewiskan
ms.author: owend
manager: craigg
---
# AggregationPrefix Element (ASSL)
  Defines the common prefix to be used for aggregation names throughout the associated parent element.  
  
## Syntax  
  
```xml  
  
<Cube> <!-- or Database, MeasureGroup, Partition -->  
   ...  
   <AggregationPrefix>...</AggregationPrefix>  
   ...  
</Database>  
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
|Parent elements|[Cube](../objects/cube-element-assl.md), [Database](../objects/database-element-assl.md), [MeasureGroup](../objects/group-element-assl.md), [Partition](../objects/partition-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 Aggregation prefixes generate aggregation names in [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)], and also generate table names in the relational database for aggregations stored in a relational OLAP (ROLAP) partition.  
  
 A fully expanded aggregation name has the following parts:  
  
 *\<DatabasePrefix>\<CubePrefix>\<MeasureGroupPrefix>\<PartitionPrefix>\<AggregationID>*  
  
 The first four parts of the aggregation name make up the aggregation prefix. The user provides the first four parts:  
  
-   *DatabasePrefix* represents the value of the `AggregationPrefix` element associated with a `Database` element.  
  
-   *CubePrefix* represents the value of the `AggregationPrefix` element associated with a `Cube` element.  
  
-   *MeasureGroupPrefix* represents the value of the `AggregationPrefix` element associated with a `MeasureGroup` element.  
  
-   *PartitionPrefix* represents the value of the `AggregationPrefix` element associated with a `Partition` element.  
  
 The fifth part of the name, *AggregationID*, is a system-defined ID, and users have no control over this part of the name.  
  
 The elements that correspond to the parents of `AggregationPrefix` in the Analysis Management Objects (AMO) object model are <xref:Microsoft.AnalysisServices.Cube>, <xref:Microsoft.AnalysisServices.Database>, <xref:Microsoft.AnalysisServices.MeasureGroup>, and <xref:Microsoft.AnalysisServices.Partition>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
