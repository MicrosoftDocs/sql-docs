---
title: "DataMiningMeasureGroupDimension Data Type (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "DataMiningMeasureGroupDimension Data Type"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "DataMiningMeasureGroupDimension"
helpviewer_keywords: 
  - "DataMiningMeasureGroupDimension data type"
ms.assetid: 56d5c2ec-7e03-4dc7-a668-c8d598d59e55
author: minewiskan
ms.author: owend
manager: craigg
---
# DataMiningMeasureGroupDimension Data Type (ASSL)
  Defines a derived data type that represents the relationship between a measure group and a data mining dimension.  
  
## Syntax  
  
```xml  
  
<DataMiningMeasureGroupDimension>  
   <!-- The following elements extend MeasureGroupDimension -->  
      <CaseCubeDimensionID>...</CaseCubeDimensionID>  
</DataMiningMeasureGroupDimension>  
```  
  
## Data Type Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Base data types|[MeasureGroupDimension](dimension-data-type-assl.md)|  
|Derived data types|None|  
  
## Data Type Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|None|  
|Child elements|[CaseCubeDimensionID](../properties/id-element-assl.md)|  
|Derived elements|See [Dimension](../objects/dimension-element-assl.md) ([Dimensions](../collections/dimensions-element-assl.md) collection of [MeasureGroup](../objects/group-element-assl.md))|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.DataMiningMeasureGroupDimension>.  
  
## See Also  
 [Analysis Services Scripting Language XML Data Types &#40;ASSL&#41;](analysis-services-scripting-language-xml-data-types-assl.md)  
  
  
