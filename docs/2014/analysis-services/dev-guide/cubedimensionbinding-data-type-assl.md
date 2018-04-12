---
title: "CubeDimensionBinding Data Type (ASSL) | Microsoft Docs"
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
  - "CubeDimensionBinding Data Type"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "CubeDimensionBinding"
helpviewer_keywords: 
  - "CubeDimensionBinding data type"
ms.assetid: 7288e345-4a3e-4197-82e9-9daa38f6e928
caps.latest.revision: 40
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# CubeDimensionBinding Data Type (ASSL)
  Defines a derived data type that represents the binding of a [Dimension](../../../2014/analysis-services/dev-guide/dimension-element-assl.md), [Measure](../../../2014/analysis-services/dev-guide/measure-element-assl.md), or [MiningModel](../../../2014/analysis-services/dev-guide/miningmodel-element-assl.md) element to a cube dimension.  
  
## Syntax  
  
```xml  
  
<CubeDimensionBinding>  
      <!-- The following elements extend Binding -->  
   <DataSourceID>...</DataSourceID>  
   <CubeID>...</CubeID>  
   <CubeDimensionID>...</CubeDimensionID>  
   <Filter>...</Filter>  
</CubeDimensionBinding>  
```  
  
## Data Type Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Base data types|[Binding](../../../2014/analysis-services/dev-guide/binding-data-type-assl.md)|  
|Derived data types|None|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|None|  
|Child elements|[CubeDimensionID](../../../2014/analysis-services/dev-guide/cubedimensionid-element-assl.md), [CubeID](../../../2014/analysis-services/dev-guide/cubeid-element-assl.md), [DataSourceID](../../../2014/analysis-services/dev-guide/datasourceid-element-assl.md), [Filter](../../../2014/analysis-services/dev-guide/filter-element-trace-assl.md)|  
|Derived elements|See [Binding](../../../2014/analysis-services/dev-guide/binding-data-type-assl.md)|  
  
## Remarks  
 For additional information about the `Binding` type, including tables of Analysis Services Scripting Language (ASSL) objects of the `Binding` type and the inheritance hierarchy of `Binding` types, see [Binding Data Type &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/binding-data-type-assl.md).  
  
 For an overview of data bindings in ASSL, see [Data Sources and Bindings &#40;SSAS Multidimensional&#41;](../multidimensional-models/data-sources-and-bindings-ssas-multidimensional.md).  
  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.CubeDimensionBinding>.  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/analysis-services-scripting-language-xml-data-types-assl.md)  
  
  