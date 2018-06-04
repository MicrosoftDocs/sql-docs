---
title: "SetToStr (MDX) | Microsoft Docs"
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
# SetToStr (MDX)


  Returns a Multidimensional Expressions (MDX)-formatted string of that corresponds to a specified set.  
  
## Syntax  
  
```  
  
SetToStr(Set_Expression)  
```  
  
## Arguments  
 *Set_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a set.  
  
## Remarks  
 This function is used to transfer a string-representation of a set to an external function for parsing. The string that is returned is enclosed in braces {}, with each item in the set separated by a comma.  
  
## Example  
 The following example returns a string containing all of the members of the Geography.Country attribute hierarchy.  
  
```  
WITH MEMBER Measures.x AS SetToStr (Geography.Geography.Children)  
SELECT Measures.x ON 0  
FROM [Adventure Works]  
```  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
