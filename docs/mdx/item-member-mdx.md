---
title: "Item (Member) (MDX) | Microsoft Docs"
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
  - "ITEM"
dev_langs: 
  - "kbMDX"
helpviewer_keywords: 
  - "Item function"
ms.assetid: 71cca249-910b-4ecd-9097-1a17b224e219
caps.latest.revision: 35
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Item (Member) (MDX)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns a member from a specified tuple.  
  
## Syntax  
  
```  
  
Tuple_Expression.Item( Index )  
```  
  
## Arguments  
 *Tuple_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a tuple.  
  
 *Index*  
 A valid numeric expression that specifies the specific member by position within the tuple to be returned.  
  
## Remarks  
 The **Item** function returns a member from the specified tuple. The function returns the member found at the zero-based position specified by *Index*.  
  
## Example  
 The following example returns the member `[2003]` - the first item in the tuple `[Date].[Calendar Year].&[2003], [Measures].[Internet Sales Amount] ).` - on columns :  
  
 `SELECT`  
  
 `{( [Date].[Calendar Year].&[2003], [Measures].[Internet Sales Amount] ).Item(0)} ON 0`  
  
 `,{[Measures].[Reseller Sales Amount]} ON 1`  
  
 `FROM [Adventure Works]`  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
