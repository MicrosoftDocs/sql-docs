---
title: "TopSum (MDX) | Microsoft Docs"
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
  - "TOPSUM"
dev_langs: 
  - "kbMDX"
helpviewer_keywords: 
  - "TopSum function"
ms.assetid: e32496fd-4897-43c9-a388-4028609f4ffb
caps.latest.revision: 32
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# TopSum (MDX)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Sorts a set and returns the topmost elements whose cumulative total is at least a specified value.  
  
## Syntax  
  
```  
  
TopSum(Set_Expression, Value, Numeric_Expression)   
```  
  
## Arguments  
 *Set_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a set.  
  
 *Value*  
 A valid numeric expression that specifies the value against which each tuple is compared.  
  
 *Numeric_Expression*  
 A valid numeric expression that is typically a Multidimensional Expressions (MDX) expression that returns a measure.  
  
## Remarks  
 The **TopSum** function calculates the sum of a specified measure evaluated over a specified set, sorting the set in descending order. The function then returns the elements with the highest values whose total of the specified numeric expression is at least the specified value. This function returns the smallest subset of a set whose cumulative total is at least the specified value. The returned elements are ordered largest to smallest.  
  
> [!IMPORTANT]  
>  Like the [BottomSum](../mdx/bottomsum-mdx.md) function, the **TopSum** function always breaks the hierarchy.  
  
## Example  
 The following example returns, for the Bike category, the smallest set of members of the City level in the Geography hierarchy in the Geography dimension whose cumulative total using the Reseller Sales Amount measure is at least the sum of 6,000,000 (beginning with the members of this set with the largest number of sales).  
  
```  
SELECT [Measures].[Reseller Sales Amount] ON 0,  
TopSum  
   ({[Geography].[Geography].[City].Members}  
   , 6000000  
   , [Measures].[Reseller Sales Amount]  
   ) ON 1  
FROM [Adventure Works]  
WHERE([Product].[Product Categories].Bikes)  
```  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
