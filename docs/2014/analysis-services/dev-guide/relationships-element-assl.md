---
title: "Relationships Element (ASSL) | Microsoft Docs"
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
ms.assetid: e78882c9-b14e-4044-848e-ea7fddd3b75d
caps.latest.revision: 6
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# Relationships Element (ASSL)
  Contains the collection of relationships for the associated dimension.  
  
## Syntax  
  
```xml  
  
<CubeDimension> <!-- or one of the elements listed below in the Element Relationships table -->  
   ...  
   <Attributes>  
      <Attribute>...</Attribute>  
  </Attributes>  
   ...  
</CubeDimension>  
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
|Parent elements|[CubeDimension](../../../2014/analysis-services/dev-guide/cubedimension-data-type-assl.md), [Dimension](../../../2014/analysis-services/dev-guide/dimension-element-assl.md), [PerspectiveDimension](../../../2014/analysis-services/dev-guide/perspectivedimension-data-type-assl.md), [RegularMeasureGroupDimension](../../../2014/analysis-services/dev-guide/regularmeasuregroupdimension-data-type-assl.md)|  
|Child elements|[Relationship](../../../2014/analysis-services/dev-guide/relationship-data-type-assl.md)|  
  
## Remarks  
 The corresponding elements in the Analysis Management Objects (AMO) object model are <xref:Microsoft.AnalysisServices.RelationshipCollection>.  
  
## See Also  
 [Collections &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/collections-assl.md)  
  
  