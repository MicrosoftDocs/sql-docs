---
title: "Calculations Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "Calculations Element"
apilocation: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
apitype: "Schema"
applies_to: 
  - "SQL Server 2016 Preview"
f1_keywords: 
  - "Calculations"
helpviewer_keywords: 
  - "Calculations element"
ms.assetid: 03e5e91c-1f66-4dc7-8aad-4d9876928df0
caps.latest.revision: 37
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Calculations Element (ASSL)
  Contains the collection of [PerspectiveCalculation](../../../analysis-services/scripting/data-type/perspectivecalculation-data-type-assl.md) elements associated with a [Perspective](../../../analysis-services/scripting/objects/perspective-element-assl.md) element.  
  
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
|Parent elements|[Perspective](../../../analysis-services/scripting/objects/perspective-element-assl.md)|  
|Child elements|[Calculation](../../../analysis-services/scripting/objects/calculation-element-assl.md) of type [PerspectiveCalculation](../../../analysis-services/scripting/data-type/perspectivecalculation-data-type-assl.md)|  
  
## Remarks  
 The corresponding element in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.PerspectiveCalculationCollection>.  
  
## See Also  
 [Collections &#40;ASSL&#41;](../../../analysis-services/scripting/collections/collections-assl.md)  
  
  