---
title: "DependsOnDimensionID Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "DependsOnDimensionID Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "DependsOnDimensionID"
helpviewer_keywords: 
  - "DependsOnDimensionID element"
ms.assetid: 66ec20dd-b475-4895-a92c-7ac0e7e1c675
author: minewiskan
ms.author: owend
manager: craigg
---
# DependsOnDimensionID Element (ASSL)
  Contains the identifier (ID) of another dimension on which the parent dimension depends.  
  
## Syntax  
  
```xml  
  
<Dimension>  
      ...  
   <DependsOnDimensionID>...</DependsOnDimensionID>  
   ...  
</Dimension>  
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
|Parent elements|[Dimension](../objects/dimension-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The `DependsOnDimensionID` element is used by a dependent dimension to identify the dimension on which it depends.  
  
 The element that corresponds to the parent of `DependsOnDimensionID` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Dimension>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
