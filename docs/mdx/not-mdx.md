---
title: "NOT (MDX) | Microsoft Docs"
ms.date: 06/04/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# NOT (MDX)


  Performs a logical negation on a numeric expression.  
  
## Syntax  
  
```  
  
NOT Expression1  
```  
  
#### Parameters  
 *Expression1*  
 A valid Multidimensional Expressions (MDX) expression that returns a numeric value.  
  
## Return Value  
 A Boolean value that returns **false** if the argument evaluates to **true**; otherwise, **true**.  
  
## Remarks  
 The **NOT** operator treats the expression as a Boolean value (zero, 0, as **false**; otherwise, **true**) before the operator performs the logical negation. The following table illustrates how the **NOT** operator performs the logical negation.  
  
|*Expression1*|Return Value|  
|-------------------|------------------|  
|**true**|**false**|  
|**false**|**true**|  
  
## See Also  
 [MDX Operator Reference &#40;MDX&#41;](../mdx/mdx-operator-reference-mdx.md)  
  
  
