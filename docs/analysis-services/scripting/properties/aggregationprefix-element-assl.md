---
title: "AggregationPrefix Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "AggregationPrefix Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "AggregationPrefix"
helpviewer_keywords: 
  - "AggregationPrefix element"
ms.assetid: 1581e0df-ae8e-41ce-9c92-f0f7cac487f2
caps.latest.revision: 36
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
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
|Parent elements|[Cube](../../../analysis-services/scripting/objects/cube-element-assl.md), [Database](../../../analysis-services/scripting/objects/database-element-assl.md), [MeasureGroup](../../../analysis-services/scripting/objects/measuregroup-element-assl.md), [Partition](../../../analysis-services/scripting/objects/partition-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 Aggregation prefixes generate aggregation names in [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)], and also generate table names in the relational database for aggregations stored in a relational OLAP (ROLAP) partition.  
  
 A fully expanded aggregation name has the following parts:  
  
 *\<DatabasePrefix>\<CubePrefix>\<MeasureGroupPrefix>\<PartitionPrefix>\<AggregationID>*  
  
 The first four parts of the aggregation name make up the aggregation prefix. The user provides the first four parts:  
  
-   *DatabasePrefix* represents the value of the **AggregationPrefix** element associated with a **Database** element.  
  
-   *CubePrefix* represents the value of the **AggregationPrefix** element associated with a **Cube** element.  
  
-   *MeasureGroupPrefix* represents the value of the **AggregationPrefix** element associated with a **MeasureGroup** element.  
  
-   *PartitionPrefix* represents the value of the **AggregationPrefix** element associated with a **Partition** element.  
  
 The fifth part of the name, *AggregationID*, is a system-defined ID, and users have no control over this part of the name.  
  
 The elements that correspond to the parents of **AggregationPrefix** in the Analysis Management Objects (AMO) object model are <xref:Microsoft.AnalysisServices.Cube>, <xref:Microsoft.AnalysisServices.Database>, <xref:Microsoft.AnalysisServices.MeasureGroup>, and <xref:Microsoft.AnalysisServices.Partition>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  