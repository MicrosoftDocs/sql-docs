---
title: "+ (Union) (MDX) | Microsoft Docs"
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
# Union - MDX operator reference


  Performs a set operation that returns a union of two sets, removing duplicate members.  
  
## Syntax  
  
```  
  
Set_Expression + Set_Expression      
```  
  
#### Parameters  
 *Set_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a set.  
  
## Return Value  
 A set that contains the members of both specified sets.  
  
## Remarks  
 The **+ (Union)** operator is functionally equivalent to the [Union  &#40;MDX&#41;](../mdx/union-mdx.md) function.  
  
## Examples  
 The following example demonstrates the use of this operator.  
  
```  
-- This member returns the gross profit margin for each year for North American countries.  
SELECT   
    [Date].[Calendar].[Calendar Year].Members ON 0,  
    {[Sales Territory].[Sales Territory].[Country].[United States]} +  
     {[Sales Territory].[Sales Territory].[Country].[Canada]} ON 1  
FROM  
    [Adventure Works]  
WHERE  
    ([Measures].[Gross Profit Margin])  
```  
  
## See Also  
 [MDX Operator Reference &#40;MDX&#41;](../mdx/mdx-operator-reference-mdx.md)  
  
  
