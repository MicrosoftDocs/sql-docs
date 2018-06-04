---
title: "&lt;&gt; (Not Equal To) (MDX) | Microsoft Docs"
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
  
## See Also  
 [MDX Operator Reference &#40;MDX&#41;](../mdx/mdx-operator-reference-mdx.md)  
  
  
