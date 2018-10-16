---
title: "CaseCubeDimensionID Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "CaseCubeDimensionID Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "CaseCubeDimensionID"
helpviewer_keywords: 
  - "CaseCubeDimensionID element"
ms.assetid: 96720e13-7f9b-4768-ad4b-4def40758707
author: minewiskan
ms.author: owend
manager: craigg
---
# CaseCubeDimensionID Element (ASSL)
  Contains the identifier (ID) of the cube dimension that relates the data mining dimension to the measure group.  
  
## Syntax  
  
```xml  
  
<DataMiningMeasureGroupDimension>  
   ...  
   <CaseCubeDimensionID>...</CaseCubeDimensionID>  
   ...  
</DataMiningMeasureGroupDimension>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String|  
|Default value|None|  
|Cardinality|1-1: Required element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[DataMiningMeasureGroupDimension](../data-type/dimension-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The element that corresponds to the parent of `CaseCubeDimensionID` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.DataMiningMeasureGroupDimension>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
