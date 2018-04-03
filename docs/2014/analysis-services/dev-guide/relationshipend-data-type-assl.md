---
title: "RelationshipEnd Data Type (ASSL) | Microsoft Docs"
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
ms.assetid: 3a974dd4-e1d6-45b2-b8c8-1a914bc13a02
caps.latest.revision: 6
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# RelationshipEnd Data Type (ASSL)
  Defines a primitive data type that represents a relationship end in a relationship.  
  
## Syntax  
  
```xml  
  
<RelationshipEnd>  
   <Role>...</Role>  
   <Multiplicity>...</Multiplicity>  
   <DimensionID>...</DimensionID>  
   <Attributes>...</Attributes>  
   <Translations>...</Translations>  
   <VisualizationProperties>...</VisualizationProperties>  
</Relationship>  
```  
  
## Data Type Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Base data types|None|  
|Derived data types|None|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Relationship](../../../2014/analysis-services/dev-guide/relationship-data-type-assl.md)|  
|Child elements|[Role](../../../2014/analysis-services/dev-guide/role-element-xmla.md), [Multiplicity](../../../2014/analysis-services/dev-guide/multiplicity-element-assl.md), [DimensionID](../../../2014/analysis-services/dev-guide/dimensionid-element-assl.md), [Attributes](../../../2014/analysis-services/dev-guide/attributes-element-assl.md), [Translations](../../../2014/analysis-services/dev-guide/translations-element-assl.md), [VisualizationProperties](../../../2014/analysis-services/dev-guide/relationshipendvisualizationproperties-data-type-assl.md)|  
|Derived elements||  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.RelationshipEnd>.  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/analysis-services-scripting-language-xml-data-types-assl.md)  
  
  