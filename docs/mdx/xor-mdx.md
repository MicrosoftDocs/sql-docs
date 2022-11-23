---
description: "XOR (MDX)"
title: "XOR (MDX) | Microsoft Docs"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
---
# XOR (MDX)


  Performs a logical exclusion on two numeric expressions.  
  
## Syntax  
  
```  
  
Expression1 XOR Expression2  
  
```  
  
#### Parameters  
 *Expression1*  
 A valid Multidimensional Expressions (MDX) expression that returns a numeric value.  
  
 *Expression2*  
 A valid MDX expression that returns a numeric value.  
  
## Return Value  
 A Boolean value that returns **true** if one and only one argument evaluates to **true**; otherwise, **false**.  
  
## Remarks  
 The **XOR** operator treats both parameters as Boolean values (zero, 0, as **false**; otherwise, **true**) before the operator performs the logical exclusion. The following table illustrates how the **XOR** operator performs the logical exclusion.  
  
|*Expression1*|*Expression2*|Return Value|  
|-------------------|-------------------|------------------|  
|**true**|**true**|**false**|  
|**true**|**false**|**true**|  
|**false**|**true**|**true**|  
|**false**|**false**|**false**|  
  
## See Also  
 [MDX Operator Reference &#40;MDX&#41;](../mdx/mdx-operator-reference-mdx.md)  
  
  
