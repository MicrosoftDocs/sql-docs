---
title: "IgnoreUnrelatedDimensions Element (ASSL) | Microsoft Docs"
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
  - "IgnoreUnrelatedDimensions Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "IgnoreUnrelatedDimensions"
helpviewer_keywords: 
  - "IgnoreUnrelatedDimensions element"
ms.assetid: c7d7a1cd-a8e0-4ae7-9464-a1d2a55a86ab
caps.latest.revision: 33
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# IgnoreUnrelatedDimensions Element (ASSL)
  Determines whether unrelated dimensions are forced to their top level when members of dimensions that are unrelated to the measure group are included in a query.  
  
## Syntax  
  
```xml  
  
<MeasureGroup>  
   ...  
   <IgnoreUnrelatedDimensions>...</IgnoreUnrelatedDimensions>  
   ...  
</MeasureGroup>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|Boolean|  
|Default value|True|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[MeasureGroup](../../../analysis-services/scripting/objects/measuregroup-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 When **IgnoreUnrelatedDimensions** is **true**, unrelated dimensions are forced to their top level; when the value is **false**, dimensions are not forced to their top level. This property is similar to the Multidimensional Expressions (MDX) [ValidMeasure](../../../mdx/validmeasure-mdx.md) function.  
  
 The element that corresponds to the parent of **IgnoreUnrelatedDimensions** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.MeasureGroup>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  