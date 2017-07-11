---
title: "MeasureExpression Element (ASSL) | Microsoft Docs"
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
  - "MeasureExpression Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "MeasureExpression"
helpviewer_keywords: 
  - "MeasureExpression element"
ms.assetid: a0b6490d-a793-41be-8c97-41b08e1580a1
caps.latest.revision: 32
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
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
|Parent element|[Measure](../../../analysis-services/scripting/objects/measure-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The element that corresponds to the parent of **MeasureExpression** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Measure>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  