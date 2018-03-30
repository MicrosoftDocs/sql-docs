---
title: "Column Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
api_name: 
  - "Column Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "Column"
helpviewer_keywords: 
  - "Column element"
ms.assetid: 10dc6d5e-c690-4415-adbb-eaeebaa29cb4
caps.latest.revision: 27
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# Column Element (ASSL)
  Describes a column in the collection of columns associated with the parent element.  
  
## Syntax  
  
```xml  
  
<Columns>  
   <Column xsi:type="MeasureBinding">...</Column> <!-- parent of collection: DrillThroughAction -->  
   <!-- or -->  
   <Column xsi:type="CubeAttributeBinding">...</Column> <!-- parent of collection: DrillThroughAction -->  
   <!-- or -->  
   <Column xsi:type="EventColumn">...</Column> <!-- parent of collection: Event -->  
   <!-- or -->  
   <Column xsi:type="MiningModelColumn">...</Column> <!-- parent of collection: MiningModel or MiningModelColumn -->  
   <!-- or -->  
   <Column xsi:type="MiningStructureColumn">...</Column> <!-- parent of collection: MiningStructure or TableMiningStructureColumn -->  
</Columns>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length||  
|Default value|None|  
|Cardinality||  
  
|Ancestor or Parent|Data Type|  
|------------------------|---------------|  
|[DrillThroughAction](../../../2014/analysis-services/dev-guide/drillthroughaction-data-type-assl.md)|[MeasureBinding](../../../2014/analysis-services/dev-guide/measurebinding-data-type-assl.md), [CubeAttributeBinding](../../../2014/analysis-services/dev-guide/cubeattributebinding-data-type-assl.md)|  
|[Event](../../../2014/analysis-services/dev-guide/event-element-assl.md)|[EventColumn](../../../2014/analysis-services/dev-guide/eventcolumn-data-type-assl.md)|  
|[MiningModel](../../../2014/analysis-services/dev-guide/miningmodel-element-assl.md), [MiningModelColumn](../../../2014/analysis-services/dev-guide/miningmodelcolumn-data-type-assl.md)|[MiningModelColumn](../../../2014/analysis-services/dev-guide/miningmodelcolumn-data-type-assl.md)|  
|[MiningStructure](../../../2014/analysis-services/dev-guide/miningstructure-element-assl.md), [TableMiningStructureColumn](../../../2014/analysis-services/dev-guide/tableminingstructurecolumn-data-type-assl.md)|[MiningStructureColumn](../../../2014/analysis-services/dev-guide/miningstructurecolumn-data-type-assl.md)|  
  
|Ancestor or Parent|Cardinality|  
|------------------------|-----------------|  
|[Event](../../../2014/analysis-services/dev-guide/event-element-assl.md)|1-n: Required element that can occur more than once.|  
|All others|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Columns](../../../2014/analysis-services/dev-guide/columns-element-assl.md)|  
|Child elements|None|  
  
## See Also  
 [Objects &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/objects-assl.md)  
  
  