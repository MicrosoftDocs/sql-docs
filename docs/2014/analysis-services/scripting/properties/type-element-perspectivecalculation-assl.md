---
title: "Type Element (PerspectiveCalculation) (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Type Element (PerspectiveCalculation)"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "TYPE"
helpviewer_keywords: 
  - "Type element"
ms.assetid: d7b87aea-3265-4f3c-a7ee-4f3e90f9a0b7
author: minewiskan
ms.author: owend
manager: craigg
---
# Type Element (PerspectiveCalculation) (ASSL)
  Indicates the type of the [PerspectiveCalculation](../data-type/perspectivecalculation-data-type-assl.md) element.  
  
## Syntax  
  
```xml  
  
<PerspectiveCalculation>  
   ...  
   <Type>...</Type>  
   ...  
</PerspectiveCalculation>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|String (enumeration)|  
|Default value|None|  
|Cardinality|1-1: Required element that occurs once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent element|[PerspectiveCalculation](../data-type/perspectivecalculation-data-type-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The value of this element is limited to one of the strings listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|*Member*|Calculated member|  
|*Set*|Named set|  
  
 The enumeration that corresponds to the allowed values for `Type` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.PerspectiveCalculationType>.  
  
 The element that corresponds to the parent of `Type` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.PerspectiveCalculation>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
