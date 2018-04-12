---
title: "CubeBinding Data Type (out-of-line) (ASSL) | Microsoft Docs"
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
  - "CubeBinding Data Type (out-of-line)"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "CubeBinding"
helpviewer_keywords: 
  - "CubeBinding data type"
ms.assetid: 5e1ee8ef-855c-4f3d-ae21-a33360d00d66
caps.latest.revision: 37
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# CubeBinding Data Type (out-of-line) (ASSL)
  Defines a primitive data type that represents the relationship between a [Cube](../../../2014/analysis-services/dev-guide/cube-element-assl.md) element and a [DataSource](../../../2014/analysis-services/dev-guide/datasource-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<CubeBinding>  
   <ID>...</ID>  
   <DataSourceID>...</DataSourceID>  
   <DataSource>...</DataSource>  
   <MeasureGroup>...</MeasureGroup>  
</CubeBinding>  
```  
  
## Data Type Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Base data types|None|  
|Derived data types|None|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|None|  
|Child elements|[DataSource](../../../2014/analysis-services/dev-guide/datasource-element-assl.md), [DataSourceID](../../../2014/analysis-services/dev-guide/datasourceid-element-assl.md), [ID](../../../2014/analysis-services/dev-guide/id-element-assl.md), [MeasureGroup](../../../2014/analysis-services/dev-guide/measuregroup-element-assl.md)|  
|Derived elements|[Binding](../../../2014/analysis-services/dev-guide/binding-element-xmla.md) ([Bindings](../../../2014/analysis-services/dev-guide/bindings-element-xmla.md) collection of [Process](../../../2014/analysis-services/dev-guide/process-element-xmla.md) or [Batch](../../../2014/analysis-services/dev-guide/batch-element-xmla.md) commands)|  
  
## Remarks  
 For more information about out-of-line bindings, see [Data Sources and Bindings &#40;SSAS Multidimensional&#41;](../multidimensional-models/data-sources-and-bindings-ssas-multidimensional.md).  
  
## See Also  
 [Binding Data Type &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/binding-data-type-assl.md)   
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/analysis-services-scripting-language-xml-data-types-assl.md)  
  
  