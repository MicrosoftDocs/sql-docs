---
title: "CubeAttributeBinding Data Type (ASSL) | Microsoft Docs"
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
  - "CubeAttributeBinding Data Type"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "CubeAttributeBinding"
helpviewer_keywords: 
  - "CubeAttributeBinding data type"
ms.assetid: 04e3d619-1de8-4fc8-a089-9a44ac0f930c
caps.latest.revision: 41
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# CubeAttributeBinding Data Type (ASSL)
  Defines a derived data type that represents the binding of an attribute in a cube dimension to either an action or a mining structure column.  
  
## Syntax  
  
```xml  
  
<CubeAttributeBinding>  
   <!-- The following elements extend Binding -->  
   <CubeID>...</CubeID>  
   <CubeDimensionID>...</CubeDimensionID>  
      <AttributeID>...</AttributeID>  
      <Type>...</Type>  
   <Ordinal>...</Ordinal>  
</CubeAttributeBinding>  
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
|Child elements|[AttributeID](../../../2014/analysis-services/dev-guide/attributeid-element-assl.md), [CubeDimensionID](../../../2014/analysis-services/dev-guide/cubedimensionid-element-assl.md), [CubeID](../../../2014/analysis-services/dev-guide/cubeid-element-assl.md), [Ordinal](../../../2014/analysis-services/dev-guide/ordinal-element-assl.md), [Type](../../../2014/analysis-services/dev-guide/type-element-binding-assl.md)|  
|Derived elements|See [Binding](../../../2014/analysis-services/dev-guide/binding-data-type-assl.md)|  
  
## Remarks  
 For additional information about the `Binding` type, including tables of Analysis Services Scripting Language (ASSL) objects of the `Binding` type and the inheritance hierarchy of `Binding` types, see [Binding Data Type &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/binding-data-type-assl.md).  
  
 For an overview of data bindings in ASSL, see [Data Sources and Bindings &#40;SSAS Multidimensional&#41;](../multidimensional-models/data-sources-and-bindings-ssas-multidimensional.md).  
  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.CubeAttributeBinding>.  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/analysis-services-scripting-language-xml-data-types-assl.md)  
  
  