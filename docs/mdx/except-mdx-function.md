---
title: "Except (MDX) | Microsoft Docs"
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
  - "EXCEPT"
dev_langs: 
  - "kbMDX"
helpviewer_keywords: 
  - "Except function"
ms.assetid: 5d832c82-1e6d-4308-9c26-7edb8afe11dd
caps.latest.revision: 37
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Except (MDX) function
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Evaluates two sets and removes those tuples in the first set that also exist in the second set, optionally retaining duplicates.  
  
## Syntax  
  
```  
  
Except(Set_Expression1, Set_Expression2 [, ALL ] )  
```  
  
## Arguments  
 *Set_Expression1*  
 A valid Multidimensional Expressions (MDX) expression that returns a set.  
  
 *Set_Expression2*  
 A valid Multidimensional Expressions (MDX) expression that returns a set.  
  
## Remarks  
 If **ALL** is specified, the function retains duplicates found in the first set; duplicates found in the second set will still be removed. The members are returned in the order they appear in the first set.  
  
## Examples  
 The following example demonstrates the use of this function.  
  
```  
   //This query shows the quantity of orders for all products,  
   //with the exception of Components, which are not  
   //sold.  
SELECT   
   [Date].[Month of Year].Children  ON COLUMNS,  
   Except  
      ([Product].[Product Categories].[All].Children ,  
         {[Product].[Product Categories].[Components]}  
      ) ON ROWS  
FROM  
   [Adventure Works]  
WHERE  
   ([Measures].[Order Quantity])  
```  
  
## See Also  
 [- &#40;Except&#41; &#40;MDX&#41;](../mdx/except-mdx-operator.md)   
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
