---
title: "Working with Members, Tuples, and Sets (MDX) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "MDX [Analysis Services], tuples"
  - "member keys [MDX]"
  - "MDX [Analysis Services], sets"
  - "calculated members [MDX]"
  - "members [MDX]"
  - "Multidimensional Expressions [Analysis Services], members"
  - "named sets [MDX]"
  - "Multidimensional Expressions [Analysis Services], tuples"
  - "member functions [MDX]"
  - "sets [MDX]"
  - "MDX [Analysis Services], members"
  - "member names [MDX]"
  - "Multidimensional Expressions [Analysis Services], sets"
  - "tuple functions"
  - "tuples"
  - "set functions [MDX]"
ms.assetid: b6ec2439-caef-46d3-9fd7-5f4526cee334
author: minewiskan
ms.author: owend
manager: craigg
---
# Working with Members, Tuples, and Sets (MDX)
  MDX provides numerous functions that return one or more members, tuples, or sets; or that act upon a member, tuple, or set.  
  
## Member Functions  
 MDX provides several functions for retrieving members from other MDX entities, such as from dimensions, levels, sets, or tuples. For example, the [FirstChild](/sql/mdx/firstchild-mdx) function is a function that acts upon a member and returns a member.  
  
 To obtain the first child member of the Time dimension, you can explicitly state the member, as in the following example.  
  
```  
SELECT [Date].[Calendar Year].[CY 2001] on 0  
FROM [Adventure Works]  
  
```  
  
 You can also use the `FirstChild` function to return the same member, as in the following example.  
  
```  
SELECT [Date].[Calendar Year].FirstChild on 0  
FROM [Adventure Works]  
  
```  
  
 For more information about MDX member functions, see [MDX Function Reference &#40;MDX&#41;](/sql/mdx/mdx-function-reference-mdx).  
  
## Tuple Functions  
 MDX provides several functions that return tuples, and they can be used anywhere that a tuple is accepted. For example, the [Item &#40;Tuple&#41; &#40;MDX&#41;](/sql/mdx/item-tuple-mdx) function can be used to extract the first tuple from set, which is very useful when you know that a set is composed of a single tuple and you want to supply that tuple to a function that requires a tuple.  
  
 The following example returns the first tuple from within the set of tuples on the column axis.  
  
```  
SELECT {  
   ([Measures].[Reseller Sales Amount]  
      ,[Date].[Calendar Year].[CY 2003]  
   )  
, ([Measures].[Reseller Sales Amount]  
      ,[Date].[Calendar Year].[CY 2004]  
   )  
}.Item(0)  
ON COLUMNS   
FROM [Adventure Works]  
```  
  
 For more information about tuple functions, see [MDX Function Reference &#40;MDX&#41;](/sql/mdx/mdx-function-reference-mdx).  
  
## Set Functions  
 MDX provides several functions that return sets. Explicitly typing tuples and enclosing them in braces is not the only way to retrieve a set. For more information about the members function to return a set, see [Key Concepts in MDX &#40;Analysis Services&#41;](../key-concepts-in-mdx-analysis-services.md). There are many additional set functions.  
  
 The colon operator lets you use the natural order of members to create a set. For example, the set shown in the following example contains tuples for the 1st through the 4th quarter of calendar year 2002.  
  
```  
SELECT   
   {[Calendar Quarter].[Q1 CY 2002]:[Calendar Quarter].[Q4 CY 2002]}   
ON 0  
FROM [Adventure Works]  
```  
  
 If you do not use the colon operator to create the set, you can create the same set of members by specifying the tuples in the following example.  
  
```  
SELECT {  
   [Calendar Quarter].[Q1 CY 2002],   
   [Calendar Quarter].[Q2 CY 2002],   
   [Calendar Quarter].[Q3 CY 2002],   
   [Calendar Quarter].[Q4 CY 2002]  
   } ON 0  
FROM [Adventure Works]  
  
```  
  
 The colon operator is an inclusive function. The members on both sides of the colon operator are included in the resulting set.  
  
 For more information about set functions, see [MDX Function Reference &#40;MDX&#41;](/sql/mdx/mdx-function-reference-mdx).  
  
## Array Functions  
 An array function acts upon a set and returns an array. For more information about array functions, see [MDX Function Reference &#40;MDX&#41;](/sql/mdx/mdx-function-reference-mdx).  
  
## Hierarchy Functions  
 A hierarchy function returns a hierarchy by acting upon a member, level, hierarchy, or string. For more information about hierarchy functions, see [MDX Function Reference &#40;MDX&#41;](/sql/mdx/mdx-function-reference-mdx).  
  
## Level Functions  
 A level function returns a level by acting upon a member, level, or string. For more information about level functions, see [MDX Function Reference &#40;MDX&#41;](/sql/mdx/mdx-function-reference-mdx).  
  
## Logical Functions  
 A logical function acts upon a MDX expression to return information about the tuples, members, or sets in the expression. For example, the [IsEmpty &#40;MDX&#41;](/sql/mdx/isempty-mdx) function evaluates whether an expression has returned an empty cell value. For more information about logical functions, see [MDX Function Reference &#40;MDX&#41;](/sql/mdx/mdx-function-reference-mdx).  
  
## Numeric Functions  
 A numeric function acts upon a MDX expression to return a scalar value. For example, the [Aggregate &#40;MDX&#41;](/sql/mdx/aggregate-mdx) function returns a scalar value calculated by aggregating measures over the tuples in a specified set. For more information about numeric functions, see [MDX Function Reference &#40;MDX&#41;](/sql/mdx/mdx-function-reference-mdx).  
  
## String Functions  
 A string function acts upon a MDX expression to return a string. For example, the [UniqueName &#40;MDX&#41;](/sql/mdx/uniquename-mdx) function returns a string value containing the unique name of a dimension, hierarchy, level, or member. For more information about string functions, see [MDX Function Reference &#40;MDX&#41;](/sql/mdx/mdx-function-reference-mdx).  
  
## See Also  
 [Key Concepts in MDX &#40;Analysis Services&#41;](../key-concepts-in-mdx-analysis-services.md)   
 [MDX Query Fundamentals &#40;Analysis Services&#41;](mdx-query-fundamentals-analysis-services.md)   
 [MDX Function Reference &#40;MDX&#41;](/sql/mdx/mdx-function-reference-mdx)  
  
  
