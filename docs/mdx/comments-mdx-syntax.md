---
description: "Comments (MDX Syntax)"
title: "Comments (MDX Syntax) | Microsoft Docs"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
---
# Comments (MDX Syntax)


  Comments are non-executing text strings in program code. (Comments are also known as remarks). You can use comments to document code, or temporarily disable parts of Multidimensional Expressions (MDX) statements and scripts being diagnosed. By using comments to document code, you can make future program code maintenance easier. You frequently use comments to record the program name, the author name, and the dates of major code changes. You can also use comments to describe complex calculations or explain a programming method.  
  
 Comments in MDX follow these guidelines:  
  
-   All alphanumeric characters or symbols can be used within the comment.  All characters within a comment are ignored.  
  
-   There is no maximum length for a comment within a statement or script. A comment can be made up of one or more lines.  
  
 MDX supports three types of commenting characters:  
  
 // (double forward slashes)  
 These comment characters can be used on the same line as code to be run or on a line by themselves. Everything from the double forward slashes to the end of the line is part of the comment. For a multiple-line comment, the double forward slashes must appear at the starting of each comment line. For more information, see [&#40;Comment&#41; &#40;MDX&#41;](../mdx/comment-mdx-double-slash.md).  
  
 -- (double hyphens)  
 These comment characters can be used on the same line as code to be run or on a line by themselves. Everything from the double hyphens to the end of the line is part of the comment. For a multiple-line comment, the double hyphens must appear at the starting of each comment line. For more information, see [-- &#40;Comment&#41; &#40;MDX&#41;](../mdx/comment-mdx-operator-reference.md).  
  
 /* ... \*/ (forward slash-asterisk character pairs)  
 These comment characters can be used on the same line as code to be run, on lines by themselves, or even within executable code. Everything from the open comment pair (/\*) to the close comment pair (\*/) is considered part of the comment. For a multiple-line comment, the open-comment character pair (/\*) must start the comment, and the close-comment character pair (\*/) must end the comment. No other comment characters can appear on any lines of the comment. For more information, see [/*...\*/ (Comment)](../mdx/comment-mdx.md).  
  
## Example  
 The following query shows examples of all three types of comment:
 
```mdx
//An example of a comment using the double-forward slash

--An example of a comment using the double-hypen  

/*An example of a  
multi-line  
comment*/  

SELECT  
  {[Measures].[Internet Sales Amount]} ON Columns,  
  [Date].[Calendar].MEMBERS ON Rows  
FROM [Adventure Works]
```
  
## See Also  
 [MDX Syntax Elements &#40;MDX&#41;](../mdx/mdx-syntax-elements-mdx.md)  
  
  
