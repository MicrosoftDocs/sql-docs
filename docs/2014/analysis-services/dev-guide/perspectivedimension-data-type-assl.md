---
title: "PerspectiveDimension Data Type (ASSL) | Microsoft Docs"
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
  - "PerspectiveDimension Data Type"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "PerspectiveDimension"
helpviewer_keywords: 
  - "PerspectiveDimension data type"
ms.assetid: c4bc56de-4f42-4ceb-a68d-a4fec92fdfa9
caps.latest.revision: 33
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# PerspectiveDimension Data Type (ASSL)
  Defines a primitive data type that represents information about a dimension in a perspective.  
  
## Syntax  
  
```xml  
  
<PerspectiveDimension>  
   <CubeDimensionID>...</CubeDimensionID>  
   <Attributes>...</Attributes>  
   <Hierarchies>...</Hierarchies>  
      <Annotations>...</Annotations>  
</PerspectiveDimension>  
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
|Child elements|[Annotations](../../../2014/analysis-services/dev-guide/annotations-element-assl.md), [Attributes](../../../2014/analysis-services/dev-guide/attributes-element-assl.md), [CubeDimensionID](../../../2014/analysis-services/dev-guide/cubedimensionid-element-assl.md), [Hierarchies](../../../2014/analysis-services/dev-guide/hierarchies-element-assl.md)|  
|Derived elements|[Dimension](../../../2014/analysis-services/dev-guide/dimension-element-assl.md) ([Dimensions](../../../2014/analysis-services/dev-guide/dimensions-element-assl.md) collection of [Perspective](../../../2014/analysis-services/dev-guide/perspective-element-assl.md))|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.PerspectiveDimension>.  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/analysis-services-scripting-language-xml-data-types-assl.md)  
  
  