---
title: "TopCount (MDX) | Microsoft Docs"
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
  - "TOPCOUNT"
dev_langs: 
  - "kbMDX"
helpviewer_keywords: 
  - "TopCount function"
ms.assetid: 15026a8f-35c5-4307-8856-348f5c44bfd5
caps.latest.revision: 32
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# TopCount (MDX)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Sorts a set in descending order and returns the specified number of elements with the highest values.  
  
## Syntax  
  
```  
  
TopCount(Set_Expression,Count [ ,Numeric_Expression ] )  
```  
  
## Arguments  
 *Set_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a set.  
  
 *Count*  
 A valid numeric expression that specifies the number of tuples to be returned.  
  
 *Numeric_Expression*  
 A valid numeric expression that is typically a Multidimensional Expressions (MDX) expression of cell coordinates that return a number.  
  
## Remarks  
 If a numeric expression is specified, the **TopCount** function sorts, in descending order, the tuples in the set specified by the specified set according to the value specified by the numeric expression, as evaluated over the specified set. After sorting the set, the **TopCount** function then returns the specified number of tuples with the highest value.  
  
> [!IMPORTANT]  
>  Like the [BottomCount](../mdx/bottomcount-mdx.md) function, the **TopCount** function always breaks the hierarchy.  
  
 If a numeric expression is not specified, the function returns the set of members in natural order, without any sorting, behaving like the [Head (MDX)](../mdx/head-mdx.md) function.  
  
## Examples  
 The following example returns the top 10 dates by Internet Sales Amount:  
  
 `SELECT [Measures].[Internet Sales Amount] ON 0,`  
  
 `TOPCOUNT([Date].[Date].[Date].MEMBERS, 10, [Measures].[Internet Sales Amount])`  
  
 `ON 1`  
  
 `FROM [Adventure Works]`  
  
 The following example returns, for the Bike category, the first five members in the set containing all combinations of members of the City level in the Geography hierarchy in the Geography dimension and all fiscal years from the Fiscal hierarchy of the Date dimension, ordered by the Reseller Sales Amount measure (beginning with the members of this set with the largest number of sales).  
  
```  
SELECT [Measures].[Reseller Sales Amount] ON 0,  
TopCount  
   ({[Geography].[Geography].[City].Members   
      *[Date].[Fiscal].[Fiscal Year].Members}  
   , 5  
   , [Measures].[Reseller Sales Amount]  
   ) ON 1  
FROM [Adventure Works]  
WHERE([Product].[Product Categories].Bikes)  
```  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
