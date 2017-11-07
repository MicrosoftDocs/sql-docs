---
title: "BottomPercent (MDX) | Microsoft Docs"
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
  - "BOTTOMPERCENT"
dev_langs: 
  - "kbMDX"
helpviewer_keywords: 
  - "BottomPercent function"
ms.assetid: c04866e6-e6dd-4ed1-ae79-c718c194930c
caps.latest.revision: 39
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# BottomPercent (MDX)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Sorts a set in ascending order, and returns a set of tuples with the lowest values whose cumulative total is equal to or greater than a specified percentage.  
  
## Syntax  
  
```  
  
BottomPercent(Set_Expression, Percentage, Numeric_Expression)   
```  
  
## Arguments  
 *Set_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a set.  
  
 *Percentage*  
 A valid numeric expression that specifies the percentage of tuples to be returned.  
  
 *Numeric_Expression*  
 A valid numeric expression that is typically a Multidimensional Expressions (MDX) expression of cell coordinates that return a number.  
  
## Remarks  
 The **BottomPercent** function calculates the sum of the specified numeric expression evaluated over a specified set, sorting the set in ascending order. The function then returns the elements with the lowest values whose cumulative percentage of the total summed value is at least the specified percentage. This function returns the smallest subset of a set whose cumulative total is at least the specified percentage. The returned elements are ordered largest to smallest.  
  
> [!IMPORTANT]  
>  The **BottomPercent** function, like the [TopPercent](../mdx/toppercent-mdx.md) function, always breaks the hierarchy. For more information, see Order function.  
  
## Example  
 The following example returns, for the Bike category, the smallest set of members of the City level in the Geography hierarchy in the Geography dimension for fiscal year 2003 whose cumulative total using the Reseller Sales Amount measure is at least 15% of the cumulative total (beginning with the members of this set with the smallest number of sales).  
  
```  
SELECT  
[Product].[Product Categories].Bikes ON 0,  
BottomPercent  
   ({[Geography].[Geography].[City].Members}  
   , 15  
   , ([Measures].[Reseller Sales Amount],[Product].[Product Categories].Bikes)  
   ) ON 1  
FROM [Adventure Works]  
WHERE ([Measures].[Reseller Sales Amount],[Date].[Fiscal].[Fiscal Year].[FY 2003])  
```  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
