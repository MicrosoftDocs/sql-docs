---
title: "Var (MDX) | Microsoft Docs"
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
  - "VAR"
dev_langs: 
  - "kbMDX"
helpviewer_keywords: 
  - "Var function [MDX]"
ms.assetid: 5575b68e-ebc1-4eaf-9547-1321d495ea62
caps.latest.revision: 32
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Var (MDX)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns the sample variance of a numeric expression evaluated over a set, using the unbiased population formula (dividing by *n*).  
  
## Syntax  
  
```  
  
Var(Set_Expression [ ,Numeric_Expression ] )  
```  
  
## Arguments  
 *Set_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a set.  
  
 *Numeric_Expression*  
 A valid numeric expression that is typically a Multidimensional Expressions (MDX) expression of cell coordinates that return a number.  
  
## Remarks  
 The **Var** function returns the unbiased variance of a specified numeric expression evaluated over a specified set.  
  
 The **Var** function uses the unbiased population formula, and the [VarP](../mdx/varp-mdx.md) function uses the biased population formula.  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
