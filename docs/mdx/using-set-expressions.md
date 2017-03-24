---
title: "Using Set Expressions | Microsoft Docs"
ms.custom: ""
ms.date: "03/02/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
dev_langs: 
  - "kbMDX"
helpviewer_keywords: 
  - "expressions [MDX], set"
  - "tuples"
  - "set expressions [MDX]"
ms.assetid: 88d65872-0cbe-4c95-b36f-e1aa4ac8ed07
caps.latest.revision: 28
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Using Set Expressions
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  A set consists of an ordered list of zero or more tuples. A set that does not contain any tuples is known as an empty set.  
  
 The complete expression of a set consists of zero or more explicitly specified tuples, framed in curly braces:  
  
 { [ { *Tuple_expression* | *Member_expression* } [ , { *Tuple_expression* | *Member_expression* } ] ... ] }  
  
 The member expressions specified in a set expression are converted to one-member tuple expressions.  
  
## Example  
 The following example shows two set expressions used on the Columns and Rows axes of a query:  
  
 `SELECT`  
  
 `{[Measures].[Internet Sales Amount], [Measures].[Internet Tax Amount]} ON COLUMNS,`  
  
 `{([Product].[Product Categories].[Category].&[4], [Date].[Calendar].[Calendar Year].&[2004]),`  
  
 `([Product].[Product Categories].[Category].&[1], [Date].[Calendar].[Calendar Year].&[2003]),`  
  
 `([Product].[Product Categories].[Category].&[3], [Date].[Calendar].[Calendar Year].&[2004])}`  
  
 `ON ROWS`  
  
 `FROM [Adventure Works]`  
  
 On the Columns axis, the set  
  
 {[Measures].[Internet Sales Amount], [Measures].[Internet Tax Amount]}  
  
 consists of two members from the Measures dimension. On the Rows axis, the set  
  
 {([Product].[Product Categories].[Category].&[4], [Date].[Calendar].[Calendar Year].&[2004]),  
  
 ([Product].[Product Categories].[Category].&[1], [Date].[Calendar].[Calendar Year].&[2003]),  
  
 ([Product].[Product Categories].[Category].&[3], [Date].[Calendar].[Calendar Year].&[2004])}  
  
 consists of three tuples, each of which contains two explicit references to members on the Product Categories hierarchy of the Product dimension and the Calendar hierarchy of the Date dimension.  
  
 For examples of functions that return sets, see [Working with Members, Tuples, and Sets &#40;MDX&#41;](../analysis-services/multidimensional-models/mdx/working-with-members-tuples-and-sets-mdx.md).  
  
## See Also  
 [Expressions &#40;MDX&#41;](../mdx/expressions-mdx.md)  
  
  
