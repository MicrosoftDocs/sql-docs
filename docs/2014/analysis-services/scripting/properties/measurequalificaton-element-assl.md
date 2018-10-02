---
title: "MeasureQualificaton Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "MeasureQualificaton Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "MeasureQualification element"
ms.assetid: 754a037c-f20b-4717-a6e8-12f495e8e3b4
author: minewiskan
ms.author: owend
manager: craigg
---
# MeasureQualificaton Element (ASSL)
  Determines whether a prefix is applied to measures in the [MeasureGroup](../objects/group-element-assl.md).  
  
## Syntax  
  
```xml  
  
<MeasureGroup>  
   ...  
   <MeasureQualification>...</MeasureQualification>  
   ...  
</MeasureGroup>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|*None*|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[MeasureGroup](../objects/group-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of this element is limited to one of the strings listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*None*|No prefix is applied to measures within this measure group.|  
|*PrefixMeasureGroup*|The unique name and caption of each measure in this measure group is prefixed by the name of the measure group and a single space.|  
  
## Remarks  
 The element that corresponds to the parent of `MeasureQualification` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.MeasureGroup>.  
  
## See Also  
 [Cube Element &#40;ASSL&#41;](../objects/cube-element-assl.md)   
 [Dimension Element &#40;ASSL&#41;](../objects/dimension-element-assl.md)   
 [MeasureGroup Element &#40;ASSL&#41;](../objects/group-element-assl.md)   
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
