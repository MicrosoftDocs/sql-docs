---
description: "Max (MDX)"
title: "Max (MDX) | Microsoft Docs"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
---
# Max (MDX)


  Returns the maximum value of a numeric expression that is evaluated over a set.  
  
## Syntax  
  
```  
  
Max( Set_Expression [ , Numeric_Expression ] )  
```  
  
## Arguments  
 *Set_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a set.  
  
 *Numeric_Expression*  
 A valid numeric expression that is typically a Multidimensional Expressions (MDX) expression of cell coordinates that return a number.  
  
## Remarks  
 If a numeric expression is specified, the specified numeric expression is evaluated across the set and then returns the maximum value from that evaluation. If a numeric expression is not specified, the specified set is evaluated in the current context of the members of the set and then returns the maximum value from that evaluation.  
  
> [!NOTE]  
>  [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] ignores nulls when calculating the maximum value in a set of numbers.  
  
## Example  
 The following example returns the maximum monthly sales for each quarter, subcategory, and country/region in the Adventure Works cube.  
  
```  
WITH MEMBER Measures.x AS Max   
   ([Date].[Calendar].CurrentMember.Children  
      , [Measures].[Reseller Order Quantity]  
   )  
SELECT Measures.x ON 0  
,NON EMPTY [Date].[Calendar].[Calendar Quarter]*   
   [Product].[Product Categories].[Subcategory].members *  
   [Geography].[Geography].[Country].Members  
ON 1  
FROM [Adventure Works]  
  
```  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
