---
title: "= (Equal To) (MDX) | Microsoft Docs"
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
# = (Equal To) (MDX)


  Performs a comparison operation that determines whether the value of one Multidimensional Expressions (MDX) expression is equal to the value of another MDX expression.  
  
> [!NOTE]  
>  To compare objects, use the [IS &#40;MDX&#41;](../mdx/is-mdx.md) operator. For example, use the IS operator when you are checking if the current member on a query axis is a specific member.  
  
## Syntax  
  
```  
  
MDX_Expression = MDX_Expression   
```  
  
#### Parameters  
 *MDX_Expression*  
 A valid MDX expression.  
  
## Return Value  
 A Boolean value based on the following conditions:  
  
-   **true** if the value of the first parameter is equal to the value of the second parameter.  
  
-   **false** if the value of the first parameter is not equal to the value of the second parameter.  
  
-   **true** if both parameters are null, or one parameter is null and the other parameter is 0.  
  
## Examples  
 The following query shows examples of these conditions:  
  
 `With`  
  
 `--Returns true`  
  
 `Member [Measures].bool1 as 1=1`  
  
 `--Returns false`  
  
 `Member [Measures].bool2 as 1=0`  
  
 `--Returns true`  
  
 `Member [Measures].bool3 as null=null`  
  
 `--Returns true`  
  
 `Member [Measures].bool4 as 0=null`  
  
 `--Returns false`  
  
 `Member [Measures].bool5 as 1=null`  
  
 `Select {[Measures].bool1,[Measures].bool2,[Measures].bool3,[Measures].bool4,[Measures].bool5}`  
  
 `On 0`  
  
 `From [Adventure Works]`  
  
## See Also  
 [MDX Operator Reference &#40;MDX&#41;](../mdx/mdx-operator-reference-mdx.md)  
  
  
