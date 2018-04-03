---
title: "Columns Element (ASSL) | Microsoft Docs"
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
  - "Columns Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "COLUMNS"
helpviewer_keywords: 
  - "Columns element"
ms.assetid: 14011eed-6f10-4120-b256-d599d59bde80
caps.latest.revision: 35
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# Columns Element (ASSL)
  Contains the collection of columns associated with the parent element.  
  
## Syntax  
  
```xml  
  
<Action xsi:type="DrillThroughAction"> <!-- or one of the elements listed below in the Element Relationships table -->  
   <Columns>  
      <Column xsi:type="MeasureBinding">...</Column> <!-- parent: DrillThroughAction -->  
      <!-- or -->  
      <Column xsi:type="CubeAttributeBinding">...</Column> <!-- parent: DrillThroughAction -->  
      <!-- or -->  
      <Column xsi:type="EventColumn">...</Column> <!-- parent: Event -->  
      <!-- or -->  
      <Column xsi:type="MiningModelColumn">...</Column> <!-- parent: MiningModel or MiningModelColumn -->  
      <!-- or -->  
      <Column xsi:type="MiningStructureColumn">...</Column> <!-- parent: MiningStructure or TableMiningStructureColumn -->  
   </Columns>  
</DrillThroughAction>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
  
|Ancestor or Parent|Cardinality|  
|------------------------|-----------------|  
|[Event](../../../2014/analysis-services/dev-guide/event-element-assl.md)|1-1: Required element that occurs once and only once.|  
|All others|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Action](../../../2014/analysis-services/dev-guide/action-element-assl.md) of type [DrillThroughAction](../../../2014/analysis-services/dev-guide/drillthroughaction-data-type-assl.md), [Event](../../../2014/analysis-services/dev-guide/event-element-assl.md), [MiningModel](../../../2014/analysis-services/dev-guide/miningmodel-element-assl.md), [MiningModelColumn](../../../2014/analysis-services/dev-guide/miningmodelcolumn-data-type-assl.md), [MiningStructure](../../../2014/analysis-services/dev-guide/miningstructure-element-assl.md), [TableMiningStructureColumn](../../../2014/analysis-services/dev-guide/tableminingstructurecolumn-data-type-assl.md)|  
  
|Ancestor or Parent|Child Elements|  
|------------------------|--------------------|  
|[DrillThroughAction](../../../2014/analysis-services/dev-guide/drillthroughaction-data-type-assl.md)|[CubeAttributeBinding](../../../2014/analysis-services/dev-guide/cubeattributebinding-data-type-assl.md) or [MeasureBinding](../../../2014/analysis-services/dev-guide/measurebinding-data-type-assl.md)|  
|[Event](../../../2014/analysis-services/dev-guide/event-element-assl.md)|[EventColumn](../../../2014/analysis-services/dev-guide/eventcolumn-data-type-assl.md)|  
|[MiningModel](../../../2014/analysis-services/dev-guide/miningmodel-element-assl.md), [MiningModelColumn](../../../2014/analysis-services/dev-guide/miningmodelcolumn-data-type-assl.md)|[MiningModelColumn](../../../2014/analysis-services/dev-guide/miningmodelcolumn-data-type-assl.md)|  
|[MiningStructure](../../../2014/analysis-services/dev-guide/miningstructure-element-assl.md), [TableMiningStructureColumn](../../../2014/analysis-services/dev-guide/tableminingstructurecolumn-data-type-assl.md)|[MiningStructureColumn](../../../2014/analysis-services/dev-guide/miningstructurecolumn-data-type-assl.md)|  
  
## Remarks  
 For `DrillThroughAction` elements, the `Columns` collection identifies the columns that contain data to be returned when the action is performed.  
  
 For `TableMiningStructureColumn` elements, the `Columns` collection allows only one level of recursion. In other words, any `TableMiningStructureColumn` element included in this collection cannot contain any `TableMiningStructureColumn` elements in its `Columns` collection.  
  
 Some of the corresponding elements in the Analysis Management Objects (AMO) object model are <xref:Microsoft.AnalysisServices.TraceColumnCollection>, <xref:Microsoft.AnalysisServices.MiningModelColumnCollection>, and <xref:Microsoft.AnalysisServices.MiningStructureColumnCollection>.  
  
## See Also  
 [Collections &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/collections-assl.md)  
  
  