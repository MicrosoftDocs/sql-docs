---
title: "Subset (MDX) | Microsoft Docs"
ms.custom: ""
ms.date: "03/02/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "subset"
dev_langs: 
  - "kbMDX"
helpviewer_keywords: 
  - "Subset function"
ms.assetid: 49a7cd28-cd6f-4ae7-8c91-94a8652a97a5
caps.latest.revision: 32
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Subset (MDX)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

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
  
  
