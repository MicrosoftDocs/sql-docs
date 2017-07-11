---
title: "TupleToStr (MDX) | Microsoft Docs"
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
  - "TUPLETOSTR"
dev_langs: 
  - "kbMDX"
helpviewer_keywords: 
  - "TupletoStr function"
ms.assetid: ad12347c-d1c4-4d8b-a910-3116bd6b68e0
caps.latest.revision: 32
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# TupleToStr (MDX)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns a Multidimensional Expressions (MDX)–formatted string that corresponds to a specified tuple.  
  
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
  
  
