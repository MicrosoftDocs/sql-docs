---
title: "Tail (MDX) | Microsoft Docs"
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
# Tail (MDX)


  Returns a subset from the end of a set.  
  
## Syntax  
  
```  
  
Tail(Set_Expression [ ,Count ] )  
```  
  
## Arguments  
 *Set_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a set.  
  
 *Count*  
 A valid numeric expression that specifies the number of tuples to be returned.  
  
## Remarks  
 The **Tail** function returns the specified number of tuples from the end of the specified set. The order of elements is preserved. The default value of *Count* is 1. If the specified number of tuples is less than 1, the function returns the empty set. If the specified number of tuples exceeds the number of tuples in the set, the function returns the original set.  
  
## Example  
 The following example returns the Reseller Sales Measure for the top five selling subcategories of products, irrespective of hierarchy, based on Reseller Gross Profit. The **Tail** function is used to return only the last five sets in the result after the result is reverse-ordered using the **Order** function.  
  
```  
SELECT Tail  
   (Order   
      ([Product].[Product Categories].[SubCategory].members  
         ,[Measures].[Reseller Gross Profit]  
         ,BASC  
      )  
   ,5  
   ) ON 0  
FROM [Adventure Works]  
```  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
