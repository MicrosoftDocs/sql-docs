---
title: "Translations Element (ASSL) | Microsoft Docs"
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
  - "Translations Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "Translations"
helpviewer_keywords: 
  - "Translations element"
ms.assetid: 7f6b8ff2-e834-44d3-a176-216203158a8d
caps.latest.revision: 38
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# Translations Element (ASSL)
  Contains the collection of [Translation](../../../2014/analysis-services/dev-guide/translation-element-assl.md) elements associated with the parent element.  
  
## Syntax  
  
```xml  
  
<Action><!-- or one of the elements listed below in the Element Relationships table -->  
   ...  
   <Translations>  
      <Translation>...</Translation>  
      <!-- or -->  
      <Translation xsi:type="AttributeTranslation">...</Translation><!-- parent: DimensionAttribute or ScalarMiningStructureColumn -->  
      <!-- or -->  
      <Translation xsi:type="RelationshipEndTranslation">...</Translation><!-- parent: RelationshipEnd -->  
   </Translations>  
   ...  
</Action>  
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
|Parent elements|[Action](../../../2014/analysis-services/dev-guide/action-element-assl.md), [AttributeRelationship](../../../2014/analysis-services/dev-guide/attributerelationship-element-assl.md), [CalculationProperty](../../../2014/analysis-services/dev-guide/calculationproperty-element-assl.md), [Cube](../../../2014/analysis-services/dev-guide/cube-element-assl.md), [CubeDimension](../../../2014/analysis-services/dev-guide/cubedimension-data-type-assl.md), [Database](../../../2014/analysis-services/dev-guide/database-element-assl.md), [Dimension](../../../2014/analysis-services/dev-guide/dimension-element-assl.md), [DimensionAttribute](../../../2014/analysis-services/dev-guide/dimensionattribute-data-type-assl.md), [Hierarchy](../../../2014/analysis-services/dev-guide/hierarchy-element-assl.md), [Kpi](../../../2014/analysis-services/dev-guide/kpi-element-assl.md), [Level](../../../2014/analysis-services/dev-guide/level-element-assl.md), [Measure](../../../2014/analysis-services/dev-guide/measure-element-assl.md), [MiningModel](../../../2014/analysis-services/dev-guide/miningmodel-element-assl.md), [MiningModelColumn](../../../2014/analysis-services/dev-guide/miningmodelcolumn-data-type-assl.md), [MiningStructure](../../../2014/analysis-services/dev-guide/miningstructure-element-assl.md), [Perspective](../../../2014/analysis-services/dev-guide/perspective-element-assl.md), [RelationshipEnd](../../../2014/analysis-services/dev-guide/relationshipend-data-type-assl.md), [ScalarMiningStructureColumn](../../../2014/analysis-services/dev-guide/scalarminingstructurecolumn-data-type-assl.md), [TableMiningStructureColumn](../../../2014/analysis-services/dev-guide/tableminingstructurecolumn-data-type-assl.md)|  
  
 **Child Elements**  
  
|Ancestor or Parent|Child Element|  
|------------------------|-------------------|  
|[DimensionAttribute](../../../2014/analysis-services/dev-guide/dimensionattribute-data-type-assl.md) or [ScalarMiningStructureColumn](../../../2014/analysis-services/dev-guide/scalarminingstructurecolumn-data-type-assl.md)|[Translation](../../../2014/analysis-services/dev-guide/translation-element-assl.md) of type [AttributeTranslation](../../../2014/analysis-services/dev-guide/attributetranslation-data-type-assl.md)|  
|[RelationshipEnd](../../../2014/analysis-services/dev-guide/relationshipend-data-type-assl.md)|[Translation](../../../2014/analysis-services/dev-guide/relationshipendtranslation-element-assl.md) of type [RelationshipEndTranslation](../../../2014/analysis-services/dev-guide/relationshipendtranslation-element-assl.md)|  
|All others|[Translation](../../../2014/analysis-services/dev-guide/translation-element-assl.md)|  
  
## Remarks  
 The corresponding elements in the Analysis Management Objects (AMO) object model are <xref:Microsoft.AnalysisServices.TranslationCollection> and <xref:Microsoft.AnalysisServices.AttributeTranslationCollection>.  
  
## See Also  
 [Collections &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/collections-assl.md)  
  
  