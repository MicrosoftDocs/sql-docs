---
title: "MeasureExpression Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "MeasureExpression Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "MeasureExpression"
helpviewer_keywords: 
  - "MeasureExpression element"
ms.assetid: a0b6490d-a793-41be-8c97-41b08e1580a1
author: minewiskan
ms.author: owend
manager: craigg
---
# MeasureExpression Element (ASSL)
  Contains the Multidimensional Expressions (MDX) expression that defines how the values of parent measure are obtained.  
  
## Syntax  
  
```xml  
  
<Measure>  
   ...  
   <MeasureExpression>...</MeasureExpression>  
   ...  
</Measure>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[Measure](../objects/measure-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The element that corresponds to the parent of `MeasureExpression` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Measure>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
