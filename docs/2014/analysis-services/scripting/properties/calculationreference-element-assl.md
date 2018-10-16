---
title: "CalculationReference Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "CalculationReference Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "CalculationReference"
helpviewer_keywords: 
  - "CalculationReference element"
ms.assetid: 4dd18b1f-55c3-4673-afbe-736d1bce8331
author: minewiskan
ms.author: owend
manager: craigg
---
# CalculationReference Element (ASSL)
  Contains the name of the named set or calculated cell referenced by the [CalculationProperty](../objects/calculationproperty-element-assl.md).  
  
## Syntax  
  
```xml  
  
<CalculationProperty>  
   ...  
   <CalculationReference>...</CalculationReference>  
   ...  
</CalculationProperty>  
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
|Parent element|[CalculationProperty](../objects/calculationproperty-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 If the value of the `CalculationReference` does not match the name of an existing named set or calculated cell definition, the `CalculationReference` is ignored.  
  
 The element that corresponds to the parent of `CalculationReference` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.CalculationProperty>.  
  
## See Also  
 [CalculationProperties Element &#40;ASSL&#41;](../collections/calculationproperties-element-assl.md)   
 [MdxScript Element &#40;ASSL&#41;](../objects/mdxscript-element-assl.md)   
 [MdxScripts Element &#40;ASSL&#41;](../collections/mdxscripts-element-assl.md)   
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
