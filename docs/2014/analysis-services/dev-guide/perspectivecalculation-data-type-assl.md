---
title: "PerspectiveCalculation Data Type (ASSL) | Microsoft Docs"
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
  - "PerspectiveCalculation Data Type"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "PerspectiveCalculation"
helpviewer_keywords: 
  - "PerspectiveCalculation data type"
ms.assetid: 5a5173d2-c96d-4a55-a35c-0cbfd5b0e599
caps.latest.revision: 34
author: "Minewiskan"
ms.author: "owend"
manager: "mblythe"
---
# PerspectiveCalculation Data Type (ASSL)
  Defines a primitive data type that represents the relationship between a calculation and a [Perspective](../../../2014/analysis-services/dev-guide/perspective-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<PerspectiveCalculation>  
      <Name>...</Name>  
   <Type>...</Type>  
   <Annotations>...</Annotations>  
</PerspectiveCalculation>  
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
|Child elements|[Annotations](../../../2014/analysis-services/dev-guide/annotations-element-assl.md), [Name](../../../2014/analysis-services/dev-guide/name-element-assl.md), [Type](../../../2014/analysis-services/dev-guide/type-element-perspectivecalculation-assl.md)|  
|Derived elements|[Calculation](../../../2014/analysis-services/dev-guide/calculation-element-assl.md) ([Calculations](../../../2014/analysis-services/dev-guide/calculations-element-assl.md) collection of [Perspective](../../../2014/analysis-services/dev-guide/perspective-element-assl.md))|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.PerspectiveCalculation>.  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](../../../2014/analysis-services/dev-guide/analysis-services-scripting-language-xml-data-types-assl.md)  
  
  