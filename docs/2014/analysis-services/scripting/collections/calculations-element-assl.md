---
title: "Calculations Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Calculations Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "Calculations"
helpviewer_keywords: 
  - "Calculations element"
ms.assetid: 03e5e91c-1f66-4dc7-8aad-4d9876928df0
author: minewiskan
ms.author: owend
manager: craigg
---
# Calculations Element (ASSL)
  Contains the collection of [PerspectiveCalculation](../data-type/perspectivecalculation-data-type-assl.md) elements associated with a [Perspective](../objects/perspective-element-assl.md) element.  
  
## Syntax  
  
```xml  
  
<Perspective>  
      ...  
   <Calculations>  
      <Calculation xsi:type="PerspectiveCalculation">...</Calculation>  
   </Calculations>  
   ...  
</Perspective>  
```  
  
## Element Characteristics  
  
|Characteristic|Description|  
|--------------------|-----------------|  
|Data type and length|None|  
|Default value|None|  
|Cardinality|0-1: Optional element that can occur once and only once.|  
  
## Element Relationships  
  
|Relationship|Element|  
|------------------|-------------|  
|Parent elements|[Perspective](../objects/perspective-element-assl.md)|  
|Child elements|[Calculation](../objects/calculation-element-assl.md) of type [PerspectiveCalculation](../data-type/perspectivecalculation-data-type-assl.md)|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.PerspectiveCalculationCollection>.  
  
## See Also  
 [Collections &#40;ASSL&#41;](collections-assl.md)  
  
  
