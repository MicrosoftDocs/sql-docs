---
title: "OR (DMX) | Microsoft Docs"
ms.date: 06/07/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: dmx
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# OR (DMX)
[!INCLUDE[ssas-appliesto-sqlas](../includes/ssas-appliesto-sqlas.md)]

  A logical operator that performs a logical disjunction on two numeric expressions.  
  
## Syntax  
  
```  
  
Expression1 OR Expression2  
```  
  
#### Parameters  
 *Expression1*  
 A valid Data Mining Extensions (DMX) expression that returns a numeric value.  
  
 *Expression2*  
 A valid DMX expression that returns a numeric value.  
  
## Return Value  
 A Boolean value that returns TRUE if either argument or both arguments evaluate to TRUE; otherwise FALSE.  
  
## Remarks  
 Both arguments are treated as Boolean values (0 as FALSE; otherwise TRUE) before the operator performs the logical disjunction. If either argument or both arguments evaluate to TRUE, the operator returns TRUE. If *Expression1* evaluates to TRUE and *Expression2* evaluates to FALSE, the operator returns TRUE.  
  
 The following table illustrates how the logical disjunction is performed.  
  
|If Expression1 is|If Expression2 is|Return value is|  
|-----------------------|-----------------------|---------------------|  
|TRUE|TRUE|TRUE|  
|TRUE|FALSE|TRUE|  
|FALSE|TRUE|TRUE|  
|FALSE|FALSE|FALSE|  
  
## See Also  
 [Data Mining Extensions &#40;DMX&#41; Operator Reference](../dmx/data-mining-extensions-dmx-operator-reference.md)   
 [Logical Operators &#40;DMX&#41;](../dmx/operators-logical.md)   
 [Operators &#40;DMX&#41;](../dmx/operators-dmx.md)  
  
  
