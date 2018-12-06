---
title: "Subset (MDX) | Microsoft Docs"
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
# Subset (MDX)


  Returns a subset of tuples from a specified set.  
  
## Syntax  
  
```  
  
Subset(Set_Expression, Start [ ,Count ] )  
```  
  
## Arguments  
 *Set_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a set.  
  
 *Start*  
 A valid numeric expression that specifies the position of the first tuple to be returned.  
  
 *Count*  
 A valid numeric expression that specifies the number of tuples to be returned.  
  
## Remarks  
 From the specified set, the **Subset** function returns a subset that contains the specified number of tuples, beginning at the specified start position. The start position is based on a zero-based index; that is, zero (0) corresponds to the first tuple in the specified set, 1 corresponds to the second, and so on.  
  
 If *Count* is not specified, the function returns all tuples from *Start* to the end of the set.  
  
## Example  
 The following example returns the Reseller Sales Measure for the top five selling subcategories of products, irrespective of hierarchy, based on Reseller Gross Profit. The **Subset** function is used to return only the first five sets in the result after the result is ordered using the **Order** function.  
  
```  
SELECT Subset  
   (Order   
      ([Product].[Product Categories].[SubCategory].members  
         ,[Measures].[Reseller Gross Profit]  
         ,BDESC  
      )  
   ,0  
   ,5  
   ) ON 0  
FROM [Adventure Works]  
```  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
