---
title: "Relationships Element (ASSL) | Microsoft Docs"
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
applies_to: 
  - "SQL Server 2016 Preview"
ms.assetid: e78882c9-b14e-4044-848e-ea7fddd3b75d
caps.latest.revision: 7
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
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
|Parent elements|[CubeDimension](../../../analysis-services/scripting/data-type/cubedimension-data-type-assl.md), [Dimension](../../../analysis-services/scripting/objects/dimension-element-assl.md), [PerspectiveDimension](../../../analysis-services/scripting/data-type/perspectivedimension-data-type-assl.md), [RegularMeasureGroupDimension](../../../analysis-services/scripting/data-type/regularmeasuregroupdimension-data-type-assl.md)|  
|Child elements|[Relationship](../../../analysis-services/scripting/data-type/relationship-data-type-assl.md)|  
  
## Remarks  
 The corresponding elements in the Analysis Management Objects (AMO) object model are <xref:Microsoft.AnalysisServices.RelationshipCollection>.  
  
## See Also  
 [Collections &#40;ASSL&#41;](../../../analysis-services/scripting/collections/collections-assl.md)  
  
  