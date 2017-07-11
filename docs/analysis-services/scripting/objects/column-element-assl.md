---
title: "Column Element (ASSL) | Microsoft Docs"
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
  - "Column Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "Column"
helpviewer_keywords: 
  - "Column element"
ms.assetid: 10dc6d5e-c690-4415-adbb-eaeebaa29cb4
caps.latest.revision: 28
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
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
 **Data type and length**  
  
|Ancestor or Parent|Data Type|  
|------------------------|---------------|  
|[DrillThroughAction](../../../analysis-services/scripting/data-type/drillthroughaction-data-type-assl.md)|[MeasureBinding](../../../analysis-services/scripting/data-type/measurebinding-data-type-assl.md), [CubeAttributeBinding](../../../analysis-services/scripting/data-type/cubeattributebinding-data-type-assl.md)|  
|[Event](../../../analysis-services/scripting/objects/event-element-assl.md)|[EventColumn](../../../analysis-services/scripting/data-type/eventcolumn-data-type-assl.md)|  
|[MiningModel](../../../analysis-services/scripting/objects/miningmodel-element-assl.md), [MiningModelColumn](../../../analysis-services/scripting/data-type/miningmodelcolumn-data-type-assl.md)|[MiningModelColumn](../../../analysis-services/scripting/data-type/miningmodelcolumn-data-type-assl.md)|  
|[MiningStructure](../../../analysis-services/scripting/objects/miningstructure-element-assl.md), [TableMiningStructureColumn](../../../analysis-services/scripting/data-type/tableminingstructurecolumn-data-type-assl.md)|[MiningStructureColumn](../../../analysis-services/scripting/data-type/miningstructurecolumn-data-type-assl.md)|  
  
 **Default value**: None  
  
 **Cardinality**  
  
|Ancestor or Parent|Cardinality|  
|------------------------|-----------------|  
|[Event](../../../analysis-services/scripting/objects/event-element-assl.md)|1-n: Required element that can occur more than once.|  
|All others|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Columns](../../../analysis-services/scripting/collections/columns-element-assl.md)|  
|Child elements|None|  
  
## See Also  
 [Objects &#40;ASSL&#41;](../../../analysis-services/scripting/objects/objects-assl.md)  
  
  