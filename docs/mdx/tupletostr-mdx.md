---
description: "TupleToStr (MDX)"
title: "TupleToStr (MDX) | Microsoft Docs"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
---
# TupleToStr (MDX)


  Returns a Multidimensional Expressions (MDX)-formatted string that corresponds to a specified tuple.  
  
## Syntax  
  
```  
  
TupleToStr(Tuple_Expression)   
```  
  
## Arguments  
 *Tuple_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a tuple.  
  
## Remarks  
 This function is used to transfer a string-representation of a tuple to an external function for parsing. The string that is returned is enclosed in braces {} and each member, if more than one is expressly defined in the tuple, is separated by a comma.  
  
## Examples  
 The following example returns the string ([Date].[Calendar Year].&[2001],[Geography].[Geography].[Country].&[United States]) :  
  
```  
WITH MEMBER Measures.x AS TupleToStr   
   (   
      ([Date].[Calendar Year].&[2001]  
         , [Geography].[Geography].[Country].&[United States]  
      )  
   )     
SELECT Measures.x ON 0  
FROM [Adventure Works]  
```  
  
 The following example returns the same string as the previous example.  
  
```  
WITH SET s AS   
   {  
      ([Date].[Calendar Year].&[2001],  
         [Geography].[Geography].[Country].&[United States]  
      )   
   }  
MEMBER Measures.x AS TupleToStr ( s.Item(0) )  
SELECT Measures.x ON 0  
FROM [Adventure Works]  
```  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
