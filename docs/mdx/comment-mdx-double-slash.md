---
title: "(Comment) (MDX) | Microsoft Docs"
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
  - "//"
dev_langs: 
  - "kbMDX"
helpviewer_keywords: 
  - "commenting characters"
  - "// (comment)"
ms.assetid: 9a3ee822-4689-41a8-9997-8b307850cd68
caps.latest.revision: 37
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Comment MDX double slash
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Indicates user-provided text.  
  
## Syntax  
  
```  
  
// Comment_Text   
```  
  
#### Parameters  
 *Comment_Text*  
 The string that contains the text of the comment.  
  
## Remarks  
 Comments can be inserted on a separate line, nested at the end of a Multidimensional Expressions (MDX) script line, or nested within an MDX statement. The server does not evaluate the comment.  
  
 Use // for single-line comments only. Comments inserted with // are delimited by the newline character.  
  
 There is no maximum length for comments.  
  
## Examples  
 The following example demonstrates the use of this operator.  
  
```  
// This member returns the gross profit margin for product types  
// and reseller types crossjoined by year.  
SELECT   
    [Date].[Calendar].[Calendar Year].Members *  
      [Reseller].[Reseller Type].Children ON 0,  
    [Product].[Category].[Category].Members ON 1  
FROM // Select from the Adventure Works cube.  
    [Adventure Works]  
WHERE  
    [Measures].[Gross Profit Margin]  
```  
  
## See Also  
 [Comment &#40;MDX&#41;](../mdx/comment-mdx.md)   
 [-- &#40;Comment&#41; &#40;MDX&#41;](../mdx/comment-mdx-operator-reference.md)   
 [MDX Operator Reference &#40;MDX&#41;](../mdx/mdx-operator-reference-mdx.md)  
  
  
