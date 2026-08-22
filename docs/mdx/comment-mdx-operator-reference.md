---
title: "-- (Comment) (MDX)"
description: "Comment - MDX Operator Reference"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.topic: reference
ms.custom: mdx
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
  
## Related content

- [Comment (MDX)](comment-mdx.md)
- [Comment MDX double slash](comment-mdx-double-slash.md)
- [MDX Operator Reference (MDX)](mdx-operator-reference-mdx.md)
