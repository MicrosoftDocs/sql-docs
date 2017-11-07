---
title: "NonEmptyCrossjoin (MDX) | Microsoft Docs"
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
  - "NONEMPTYCROSSJOIN"
dev_langs: 
  - "kbMDX"
helpviewer_keywords: 
  - "NonEmptyCrossjoin function"
ms.assetid: 3dc9522d-9126-4f7a-b587-216fa7a06c62
caps.latest.revision: 33
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# NonEmptyCrossjoin (MDX)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns a set that contains the cross product of one or more sets, excluding empty tuples and tuples without associated fact table data.  
  
## Syntax  
  
```  
  
NonEmptyCrossjoin(Set_Expression1 [ ,Set_Expression2,...] [,Count ] )  
```  
  
## Arguments  
 *Set_Expression1*  
 A valid Multidimensional Expressions (MDX) expression that returns a set.  
  
 *Set_Expression2*  
 A valid Multidimensional Expressions (MDX) expression that returns a set.  
  
 *Count*  
 A valid numeric expression that specifies the number of sets to be returned.  
  
## Remarks  
 The **NonEmptyCrossjoin** function returns the cross product of two or more sets as a set, excluding empty tuples or tuples without data supplied by underlying fact tables. Because of how the **NonEmptyCrossjoin** function works, all calculated members are automatically excluded.  
  
 If *Count* is not specified, the function cross joins all specified sets and excludes empty members from the resulting set. If a number of sets is specified, the function cross joins the numbers of sets specified, starting with the first specified set. The **NonEmptyCrossjoin** function uses any remaining sets that are specified in subsequent specified sets, but which have not been cross joined to determine which members are considered nonempty in the resulting cross joined set. The **NonEmptyCrossjoin** function respects the **NON_EMPTY_BEHAVIOR** setting of calculated measures.  
  
> [!IMPORTANT]  
>  This function is deprecated and you should not use it; it is retained only to maintain backwards compatibility. Instead, you should use the [Exists (MDX)](../mdx/exists-mdx.md) function with the measure group name argument or the [NonEmpty (MDX)](../mdx/nonempty-mdx.md) function.  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
