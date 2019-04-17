---
title: "-- (Comment) (MDX) | Microsoft Docs"
ms.date: 06/04/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Comment - MDX Operator Reference


  Indicates comment text that is provided by the user.  
  
## Syntax  
  
```  
  
-- Comment_Text      
```  
  
#### Parameters  
 *Comment_Text*  
 The string that contains the text of the comment.  
  
## Remarks  
 Comments can be inserted on a separate line, nested at the end of a Multidimensional Expressions (MDX) script line, or nested within an MDX statement. The server does not evaluate the comment.  
  
 Use this operator for single-line or nested comments. Comments inserted with -- are delimited by the newline character.  
  
 There is no maximum length for comments.  
  
## Examples  
 The following example demonstrates the use of this operator.  
  
```  
-- This member returns the gross profit margin for product types  
-- and reseller types crossjoined by year.  
SELECT   
    [Date].[Calendar].[Calendar Year].Members *  
      [Reseller].[Reseller Type].Children ON 0,  
    [Product].[Category].[Category].Members ON 1  
FROM -- Select from the Adventure Works cube.  
    [Adventure Works]  
WHERE  
    [Measures].[Gross Profit Margin]  
```  
  
## See Also  
 [Comment &#40;MDX&#41;](../mdx/comment-mdx.md)   
 [&#40;Comment&#41; &#40;MDX&#41;](../mdx/comment-mdx-double-slash.md)   
 [MDX Operator Reference &#40;MDX&#41;](../mdx/mdx-operator-reference-mdx.md)  
  
  
