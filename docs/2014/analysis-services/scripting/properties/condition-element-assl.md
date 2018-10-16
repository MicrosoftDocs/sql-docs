---
title: "Condition Element (ASSL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
api_name: 
  - "Condition Element"
api_location: 
  - "http://schemas.microsoft.com/analysisservices/2003/engine"
topic_type: 
  - "apiref"
f1_keywords: 
  - "condition"
helpviewer_keywords: 
  - "Condition element"
ms.assetid: 9c3cb31c-4aa1-49e4-aeb2-6cab54db0be3
author: minewiskan
ms.author: owend
manager: craigg
---
# Condition Element (ASSL)
  Contains a Multidimensional Expressions (MDX) expression that determines whether the [Action](../objects/action-element-assl.md) parent element applies to the target.  
  
## Syntax  
  
```xml  
  
<Action>  
   ...  
   <Condition>...</Condition  
      ...  
</Action>  
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
|Parent element|[Action](../objects/action-element-assl.md)|  
|Child elements|None|  
  
## Remarks  
 The `Condition` element contains an MDX expression that evaluates to a Boolean value. If the expression returns `True`, then the `Action` applies to the target specified in the [Target](target-element-assl.md) element. Otherwise, the `Action` is not applicable.  
  
 The element that corresponds to the parent of `Condition` in the Analysis Management Objects (AMO) object model is <xref:Microsoft.AnalysisServices.Action>.  
  
## See Also  
 [Properties &#40;ASSL&#41;](properties-assl.md)  
  
  
