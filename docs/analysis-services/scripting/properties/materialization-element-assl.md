---
title: "Materialization Element (ASSL) | Microsoft Docs"
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
  - "Materialization Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
helpviewer_keywords: 
  - "Materialization element"
ms.assetid: a87a95ae-d89c-4005-b22c-47c8991673b7
caps.latest.revision: 12
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Materialization Element (ASSL)
  Indicates the type of relationship between the measure group and the reference dimension.  
  
## Syntax  
  
```xml  
  
<ReferenceMeasureGroupDimension >  
   ...  
   <Materialization>...</Materialization>  
   ...  
</ReferenceMeasureGroupDimension>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|*Indirect*|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[ReferenceMeasureGroupDimension](../../../analysis-services/scripting/data-type/referencemeasuregroupdimension-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of this element is limited to one of the strings listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*Regular*|The reference dimension has a regular relationship, as with regular dimensions.|  
|*Indirect*|The reference dimension has an indirect relationship, such as with many-to-many dimensions.|  
  
 The element that corresponds to the parent of **Materialization** in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.ReferenceMeasureGroupDimension>.  
  
## See Also  
 [Dimension Element &#40;ASSL&#41;](../../../analysis-services/scripting/objects/dimension-element-assl.md)   
 [Properties &#40;ASSL&#41;](../../../analysis-services/scripting/properties/properties-assl.md)  
  
  