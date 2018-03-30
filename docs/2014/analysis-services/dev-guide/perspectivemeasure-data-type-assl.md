---
title: "PerspectiveMeasure Data Type (ASSL) | Microsoft Docs"
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
  - "PerspectiveMeasure Data Type"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "PerspectiveMeasure"
helpviewer_keywords: 
  - "PerspectiveMeasure data type"
ms.assetid: 8622ad67-3dcf-48e2-ad4a-c5f0a086eec3
caps.latest.revision: 32
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# PerspectiveMeasure Data Type (ASSL)
  Defines a primitive data type that represents information about a measure in a [PerspectiveMeasureGroup](../../../2014/analysis-services/dev-guide/perspectivemeasuregroup-data-type-assl.md) element.  
  
## Syntax  
  
```xml  
  
<PerspectiveMeasure>  
   <MeasureID>...</MeasureID>  
   <Annotations>...</Annotations>  
</PerspectiveMeasure>  
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
|Child elements|[Annotations](../../../2014/analysis-services/dev-guide/annotations-element-assl.md), [MeasureID](../../../2014/analysis-services/dev-guide/measureid-element-assl.md)|  
|Derived elements|[Measure](../../../2014/analysis-services/dev-guide/measure-element-assl.md) ([Measures](../../../2014/analysis-services/dev-guide/measures-element-assl.md) collection of [PerspectiveMeasureGroup](../../../2014/analysis-services/dev-guide/perspectivemeasuregroup-data-type-assl.md))|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.PerspectiveMeasure>.  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/analysis-services-scripting-language-xml-data-types-assl.md)  
  
  