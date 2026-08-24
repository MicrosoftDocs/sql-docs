---
title: "NOT (MDX)"
description: "NOT (MDX)"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.topic: reference
ms.custom: mdx
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
  
## Related content

- [MDX Operator Reference (MDX)](mdx-operator-reference-mdx.md)
