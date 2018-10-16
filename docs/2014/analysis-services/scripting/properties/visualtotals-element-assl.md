---
title: "VisualTotals Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "VisualTotals Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "VisualTotals"
helpviewer_keywords: 
  - "VisualTotals element"
ms.assetid: 352a05b1-846c-4d58-ac36-1f5ad418ba7d
author: minewiskan
ms.author: owend
manager: craigg
---
# VisualTotals Element (ASSL)
  Contains a Multidimensional Expressions (MDX) expression that determines whether visual totals are displayed for members of this attribute.  
  
## Syntax  
  
```xml  
  
<AttributePermission>  
      ...  
      <VisualTotals>...</VisualTotals>  
   ...  
</AttributePermission>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String|  
|Default value|`0`|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[AttributePermission](../objects/attributepermission-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The element that corresponds to the parent of `VisualTotals` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.AttributePermission>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
