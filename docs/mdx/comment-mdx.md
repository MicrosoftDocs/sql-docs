---
description: "Comment (MDX)"
title: "Comment (MDX) | Microsoft Docs"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
---
# Comment (MDX)


  Indicates comment text that is provided by the user.  
  
## Syntax  
  
```  
  
/* Comment_Text */      
```  
  
#### Parameters  
 *Comment_Text*  
 The string that contains the text of the comment.  
  
## Remarks  
 The server does not evaluate the text between the comment characters, /* and \*/. Comments can be inserted on a separate line or within a Multidimensional Expressions (MDX) statement. Multiple-line comments must be indicated by /\* and \*/.  
  
 There is no maximum length for comments. Comments can be nested; for example, `/* Test /*Comment*/ Text*/` is an example of a nested comment.  
  
## Examples  
 The following example demonstrates the use of this operator.  
  
```  
/* This member returns the gross profit margin for product types  
  and reseller types crossjoined by year. */  
SELECT   
    [Date].[Calendar].[Calendar Year].Members *  
    [Reseller].[Reseller Type].Children ON 0,  
    [Product].[Category].[Category].Members ON 1  
FROM /* Select from the Adventure Works cube. */  
    [Adventure Works]  
WHERE  
    [Measures].[Gross Profit Margin]  
```  
  
## See Also  
 [&#40;Comment&#41; &#40;MDX&#41;](../mdx/comment-mdx-double-slash.md)   
 [-- &#40;Comment&#41; &#40;MDX&#41;](../mdx/comment-mdx-operator-reference.md)   
 [MDX Operator Reference &#40;MDX&#41;](../mdx/mdx-operator-reference-mdx.md)  
  
  
