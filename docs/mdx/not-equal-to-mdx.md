---
title: "&lt;&gt; (Not Equal To) (MDX)"
description: "&lt;&gt; (Not Equal To) (MDX)"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.topic: reference
ms.custom: mdx
---
# &lt;&gt; (Not Equal To) (MDX)


  Performs a comparison operation that determines whether the value of one Multidimensional Expressions (MDX) expression is not equal to the value of another MDX expression.  
  
## Syntax  
  
```  
  
MDX_Expression <> MDX_Expression  
```  
  
#### Parameters  
 *MDX_Expression*  
 A valid MDX expression.  
  
## Return Value  
 A Boolean value based on the following conditions:  
  
-   **true** if both parameters are non-null, and the first parameter is not equal to the second parameter.  
  
-   **false** if both parameters are non-null, and the first parameter is equal to the second parameter.  
  
-   null if either or both parameters evaluate to a null value.  
  
## Related content

- [MDX Operator Reference (MDX)](mdx-operator-reference-mdx.md)
