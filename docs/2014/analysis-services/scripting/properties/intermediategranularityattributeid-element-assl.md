---
title: "IntermediateGranularityAttributeID Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "IntermediateGranularityAttributeID Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "IntermediateGranularityAttributeID"
helpviewer_keywords: 
  - "IntermediateGranularityAttributeID element"
ms.assetid: 49895ff0-cb0d-4bcc-ab73-8cb3d5961e12
author: minewiskan
ms.author: owend
manager: craigg
---
# IntermediateGranularityAttributeID Element (ASSL)
  Contains the identifier (ID) of the granularity attribute in the intermediate cube dimension that is used to relate a reference dimension to an intermediate dimension.  
  
## Syntax  
  
```xml  
  
<ReferenceMeasureGroupDimension>  
   ...  
   <IntermediateGranularityAttributeID>...  
   </IntermediateGranularityAttributeID>  
   ...  
</ReferenceMeasureGroupDimension>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String|  
|Default value|None|  
|Cardinality|1-1: Required element that occurs once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[ReferenceMeasureGroupDimension](../data-type/dimension-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The element that corresponds to the parent of `IntermediateGranularityAttributeID` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.ReferenceMeasureGroupDimension>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
