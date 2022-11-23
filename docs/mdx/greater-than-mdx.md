---
description: "&gt; (Greater Than) (MDX)"
title: "&gt; (Greater Than) (MDX) | Microsoft Docs"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
---
# &gt; (Greater Than) (MDX)


  Performs a comparison operation that determines whether the value of one Multidimensional Expressions (MDX) expression is greater than the value of another MDX expression.  
  
## Syntax  
  
```  
  
MDX_Expression > MDX_Expression  
```  
  
#### Parameters  
 *MDX_Expression*  
 A valid MDX expression.  
  
## Return Value  
 A Boolean value based on the following conditions:  
  
-   **true** if both parameters are non-null, and the first parameter has a value that is greater than the value of the second parameter.  
  
-   **false** if both parameters are non-null, and the first parameter has a value that is either equal to or lower than the value of the second parameter.  
  
-   null if either or both parameters evaluate to a null value.  
  
## Examples  
 The following example query demonstrates the use of this operator.  
  
```  
-- This query returns the gross profit margin (GPM)  
-- for Australia where the GPM is more than 50%.  
With Member [Measures].[HighGPM] as  
  IIF(  
      [Measures].[Gross Profit Margin] > .5,  
      [Measures].[Gross Profit Margin],  
      null)  
SELECT   
NON EMPTY [Sales Territory].[Sales Territory Country].[Australia] ON 0,  
    NON EMPTY [Product].[Category].Members ON 1  
FROM  
    [Adventure Works]  
WHERE  
    ([Measures].[HighGPM])  
  
```  
  
## See Also  
 [MDX Operator Reference &#40;MDX&#41;](../mdx/mdx-operator-reference-mdx.md)  
  
  
