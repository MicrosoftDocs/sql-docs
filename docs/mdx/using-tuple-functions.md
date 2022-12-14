---
description: "Using Tuple Functions"
title: "Using Tuple Functions | Microsoft Docs"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
---
# Using Tuple Functions


  A tuple function retrieves a tuple from a set or retrieves a tuple by resolving the string representation of a tuple.  
  
 Tuple functions, like member functions and set functions, are essential to negotiating the multidimensional structures found in [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)].  
  
 There are three tuple functions in MDX, [Current &#40;MDX&#41;](../mdx/current-mdx.md), [Item &#40;Tuple&#41; &#40;MDX&#41;](../mdx/item-tuple-mdx.md) and [StrToTuple &#40;MDX&#41;](../mdx/strtotuple-mdx.md). The following example query shows how to use of each of them:  
  
 `WITH`  
  
 `//Creates a set of tuples consisting of Years and Countries`  
  
 `SET MyTuples AS  [Date].[Calendar Year].[Calendar Year].MEMBERS * [Customer].[Country].[Country].MEMBERS`  
  
 `//Returns a string representation of that set using the Current and Generate functions`  
  
 `MEMBER MEASURES.CURRENTDEMO AS GENERATE(MyTuples, TUPLETOSTR(MyTuples.CURRENT), ", ")`  
  
 `//Retrieves the fourth tuple from that set and displays it as a string`  
  
 `MEMBER MEASURES.ITEMDEMO AS TUPLETOSTR(MyTuples.ITEM(3))`  
  
 `//Creates a tuple consisting of the measure Internet Sales Amount and the country Australia from a string`  
  
 `MEMBER MEASURES.STRTOTUPLEDEMO AS STRTOTUPLE("([Measures].[Internet Sales Amount]" + ", [Customer].[Country].&[Australia])")`  
  
 `SELECT{MEASURES.CURRENTDEMO,MEASURES.ITEMDEMO,MEASURES.STRTOTUPLEDEMO} ON COLUMNS`  
  
 `FROM [Adventure Works]`  
  
## See Also  
 [Functions &#40;MDX Syntax&#41;](../mdx/functions-mdx-syntax.md)   
 [Using Member Functions](../mdx/using-member-functions.md)   
 [Using Set Functions](../mdx/using-set-functions.md)  
  
  
