---
title: "Using Tuple Expressions | Microsoft Docs"
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
# Using Tuple Expressions


  A tuple is made up of one member from every dimension that is contained within a cube. Therefore, a tuple uniquely identifies a single cell within the cube.  
  
> [!NOTE]  
>  A tuple that references one or more members that are not valid is known as an empty tuple.  
  
 The complete expression of a tuple identifier is made up of one or more explicitly specified members, framed in parentheses:  
  
 (*Member_expression* [ ,*Member_expression* ... ] )  
  
 A tuple can be fully qualified, can contain implicit members, or can contain a single member.  
  
## Tuples and Implicit Members  
 A tuple that explicitly specifies a single member from every dimension that is contained within a cube is known as a fully qualified tuple. However, a tuple does not have to be fully qualified.  
  
 Any dimension not explicitly referenced within a tuple is implicitly referenced. The member for the implicitly referenced dimension depends on the structure of the dimension and the attribute relationships defined within it. If there is an explicit reference to a hierarchy on the same dimension as the implicitly referenced hierarchy, and there is a direct or indirect relationship defined between the explicitly referenced hierarchy and the implicitly referenced hierarchy, then the tuple behaves as if it contains the member on the implicitly referenced hierarchy that exists with the member on the explicitly referenced hierarchy. For example, if a cube contains a Customer dimension with City and Country attributes, and there is a relationship defined between these two attributes so that a City has one Country and a Country can contain many Cities, then explicitly including the City 'London' in your tuple implicitly references the Country 'United Kingdom'. However, if no attribute relationships are defined, the relationship is in the opposite direction (for example, although City might have a relationship with Country, you cannot determine the City someone lives in just from knowing the Country they live in) or there are no direct relationships between the two attributes defined (there could be a relationship defined from Customer to City and Customer to Country, but no relationship defined between City and Country) then the following rules apply:  
  
-   If the implicitly referenced hierarchy has a default member, the default member is added to the tuple.  
  
-   If the implicitly referenced hierarchy has no default member, the **(All)** member of the default hierarchy is used.  
  
-   If the implicitly referenced hierarchy has no default member the first member of the topmost level of the hierarchy is used.  
  
## One-Member Tuples  
 If the tuple expression has a single member, MDX converts the member into a one-member tuple for the purposes of evaluating the expression. In other words, providing the member expression `[Measures].[TestMeasure]` instead of a tuple expression is functionally equivalent to the tuple expression `( [Measures].[TestMeasure] ).`  
  
## See Also  
 [Expressions &#40;MDX&#41;](../mdx/expressions-mdx.md)   
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
