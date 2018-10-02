---
title: "DefaultMeasure Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "DefaultMeasure Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "DefaultMeasure"
helpviewer_keywords: 
  - "DefaultMeasure element"
ms.assetid: ceac8b3d-ebae-463f-9e8c-506281d42792
author: minewiskan
ms.author: owend
manager: craigg
---
# DefaultMeasure Element (ASSL)
  Contains a Multidimensional Expressions (MDX) expression that defines the default measure for a [Cube](../objects/cube-element-assl.md) or [Perspective](../objects/perspective-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<Cube> <!-- or Perspective -->  
   ...  
   <DefaultMeasure>...</DefaultMeasure>  
   ...  
</Cube>  
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
|Parent element|[Cube](../objects/cube-element-assl.md), [Perspective](../objects/perspective-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The elements that correspond to the parents of `DefaultMeasure` in the Analysis Management Objects (AMO) object model are <xref:Microsoft.AnalysisServices.Cube> and <xref:Microsoft.AnalysisServices.Perspective>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
