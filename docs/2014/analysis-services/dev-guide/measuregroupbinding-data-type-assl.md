---
title: "MeasureGroupBinding Data Type (ASSL) | Microsoft Docs"
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
  - "MeasureGroupBinding Data Type"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "MeasureGroupBinding"
helpviewer_keywords: 
  - "MeasureGroupBinding data type"
ms.assetid: 47e83eec-e0bc-4118-9a0f-5bfdd6218297
caps.latest.revision: 38
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# MeasureGroupBinding Data Type (ASSL)
  Defines a derived data type that represents a binding to a [MeasureGroup](../../../2014/analysis-services/dev-guide/measuregroup-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<MeasureGroupBinding>  
   <!-- The following elements extend Binding -->  
   <DataSourceID>...</DataSourceID>  
   <CubeID>...</CubeID>  
   <MeasureGroupID>...</MeasureGroupID>  
   <Persistence>...</Persistence>  
   <RefreshPolicy>...</RefreshPolicy>  
   <RefreshInterval>...</RefreshInterval>  
   <Filter>...</Filter>  
</MeasureGroupBinding>  
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
|Child elements|[CubeID](../../../2014/analysis-services/dev-guide/cubeid-element-assl.md), [DataSourceID](../../../2014/analysis-services/dev-guide/datasourceid-element-assl.md), [Filter](../../../2014/analysis-services/dev-guide/filter-element-binding-assl.md), [MeasureGroupID](../../../2014/analysis-services/dev-guide/measuregroupid-element-assl.md), [Persistence](../../../2014/analysis-services/dev-guide/persistence-element-assl.md), [RefreshInterval](../../../2014/analysis-services/dev-guide/refreshinterval-element-assl.md), [RefreshPolicy](../../../2014/analysis-services/dev-guide/refreshpolicy-element-assl.md)|  
|Derived elements|See [Binding](../../../2014/analysis-services/dev-guide/binding-data-type-assl.md)|  
  
## Remarks  
 For additional information about the `Binding` type, including tables of Analysis Services Scripting Language (ASSL) objects of the Binding type and the inheritance hierarchy of `Binding` types, see [Binding Data Type &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/binding-data-type-assl.md).  
  
 For an overview of data bindings in ASSL, see [Data Sources and Bindings &#40;SSAS Multidimensional&#41;](../../../2014/analysis-services/data-sources-and-bindings-ssas-multidimensional.md).  
  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.MeasureGroupBinding>.  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/analysis-services-scripting-language-xml-data-types-assl.md)  
  
  