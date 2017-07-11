---
title: "RegularMeasureGroupDimension Data Type (ASSL) | Microsoft Docs"
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
  - "RegularMeasureGroupDimension Data Type"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "RegularMeasureGroupDimension"
helpviewer_keywords: 
  - "RegularMeasureGroupDimension data type"
ms.assetid: 5c4ce400-6d7c-40fc-9bcb-392718b77182
caps.latest.revision: 40
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# RegularMeasureGroupDimension Data Type (ASSL)
  Defines a derived data type that represents a regular relationship between a dimension and a measure group.  
  
## Syntax  
  
```xml  
  
<RegularMeasureGroupDimension>  
   <!-- The following elements extend MeasureGroupDimension -->  
   <Cardinality>...</Cardinality>  
   <Attributes>...</Attributes>  
</RegularMeasureGroupDimension>  
```  
  
## Data Type Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Base data types|[MeasureGroupDimension](../../../analysis-services/scripting/data-type/measuregroupdimension-data-type-assl.md)|  
|Derived data types|[ReferenceMeasureGroupDimension](../../../analysis-services/scripting/data-type/referencemeasuregroupdimension-data-type-assl.md), [DegenerateMeasureGroupDimension](../../../analysis-services/scripting/data-type/degeneratemeasuregroupdimension-data-type-assl.md)|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|None|  
|Child elements|[Attributes](../../../analysis-services/scripting/collections/attributes-element-assl.md), [Cardinality](../../../analysis-services/scripting/properties/cardinality-element-assl.md)|  
|Derived elements|[Dimension](../../../analysis-services/scripting/objects/dimension-element-assl.md) ([Dimensions](../../../analysis-services/scripting/collections/dimensions-element-assl.md) collection)|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.RegularMeasureGroupDimension>.  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](../../../analysis-services/scripting/data-type/analysis-services-scripting-language-xml-data-types-assl.md)  
  
  