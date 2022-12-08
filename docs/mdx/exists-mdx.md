---
description: "Exists (MDX)"
title: "Exists (MDX) | Microsoft Docs"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
---
# Exists (MDX)


  Returns the set of tuples of the first set specified that exist with one or more tuples of the second set specified. This function performs manually what auto exists performs automatically. For more information about auto exists, see [Key Concepts in MDX &#40;Analysis Services&#41;](/analysis-services/multidimensional-models/mdx/key-concepts-in-mdx-analysis-services).  
  
 If the optional \<Measure Group Name> is provided, the function returns tuples that exist with one or more tuples from the second set and those tuples that have associated rows in the fact table of the specified measure group.  
  
## Syntax  
  
```  
  
Exists( Set_Expression1 , Set_Expression2 [, MeasureGroupName] )  
```  
  
## Arguments  
 *Set_Expression1*  
 A valid Multidimensional Expressions (MDX) expression that returns a set.  
  
 *Set_Expression2*  
 A valid Multidimensional Expressions (MDX) expression that returns a set.  
  
 *MeasureGroupName*  
 A valid string expression specifying a measure group name.  
  
## Remarks  
  
1.  Measure group rows with measures containing null values contribute to **Exists** when the MeasureGroupName argument is specified. This is the difference between this form of Exists and the Nonempty function: if the NullProcessing property of these measures is set to Preserve, this means the measures will show Null values when queries are run against that part of the cube; NonEmpty will always remove tuples from a set that have Null measure values, whereas Exists with the MeasureGroupName argument will not filter tuples that have associated measure group rows, even if the measure values are Null.  
  
2.  If *MeasureGroupName* parameter is used, results will depend on whether there are visible measures in the referenced measure group; if there are no visible measures in the referenced measure group then EXISTS will always return an empty set, regardless of the values of *Set_Expression1* and *Set_Expression2*.  
  
## Examples  
 Customers who live in California:  
  
```  
SELECT [Measures].[Internet Sales Amount] ON 0,  
EXISTS(  
[Customer].[Customer].[Customer].MEMBERS  
, {[Customer].[State-Province].&[CA]&[US]}  
) ON 1   
FROM [Adventure Works]  
  
```  
  
 Customers who live in California with sales:  
  
```  
SELECT [Measures].[Internet Sales Amount] ON 0,  
EXISTS(  
[Customer].[Customer].[Customer].MEMBERS  
, {[Customer].[State-Province].&[CA]&[US]}  
, "Internet Sales") ON 1   
FROM [Adventure Works]  
  
```  
  
 Customers with sales:  
  
```  
SELECT [Measures].[Internet Sales Amount] ON 0,  
EXISTS(  
[Customer].[Customer].[Customer].MEMBERS  
, , "Internet Sales") ON 1   
FROM [Adventure Works]  
  
```  
  
 Customers whom bought Bikes:  
  
```  
SELECT [Measures].[Internet Sales Amount] ON 0,  
EXISTS(  
[Customer].[Customer].[Customer].MEMBERS  
, {[Product].[Product Categories].[Category].&[1]}  
, "Internet Sales") ON 1   
FROM [Adventure Works]  
```  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)   
 [Crossjoin &#40;MDX&#41;](../mdx/crossjoin-mdx.md)   
 [NonEmptyCrossjoin &#40;MDX&#41;](../mdx/nonemptycrossjoin-mdx.md)   
 [NonEmpty &#40;MDX&#41;](../mdx/nonempty-mdx.md)   
 [IsEmpty &#40;MDX&#41;](../mdx/isempty-mdx.md)  
  
