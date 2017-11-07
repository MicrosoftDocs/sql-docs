---
title: "Correlation (MDX) | Microsoft Docs"
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
  - "CORRELATION"
dev_langs: 
  - "kbMDX"
helpviewer_keywords: 
  - "Correlation function"
ms.assetid: 9b3662c9-95a1-4644-b952-9460fe0cf160
caps.latest.revision: 38
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Correlation (MDX)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns the correlation coefficient of x-y pairs of values evaluated over a set.  
  
## Syntax  
  
```  
  
Correlation( Set_Expression, Numeric_Expression_y [ ,Numeric_Expression_x ] )  
```  
  
## Arguments  
 *Set_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a set.  
  
 *Numeric_Expression_y*  
 A valid numeric expression that is typically a Multidimensional Expressions (MDX) expression of cell coordinates that return a number that represents values for the y-axis.  
  
 *Numeric_Expression_x*  
 A valid numeric expression that is typically a Multidimensional Expressions (MDX) expression of cell coordinates that return a number that represents values for the x-axis.  
  
## Remarks  
 The **Correlation** function calculates the correlation coefficient of two pairs of values by first evaluating the specified set against the first numeric expression to obtain the values for the y-axis. The function then evaluates the specified set against the second numeric expression, if present, to obtain the set of values for the x-axis. If the second numeric expression is not specified, the function uses the current context of the cells in the specified set as the values for the x-axis.  
  
> [!NOTE]  
>  The **Correlation** function ignores empty cells or cells that contain text or logical values. However, the function includes cells with values of zero.  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
