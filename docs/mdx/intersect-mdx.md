---
title: "Intersect (MDX) | Microsoft Docs"
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
# Intersect (MDX)


  Returns the intersection of two input sets, optionally retaining duplicates.  
  
## Syntax  
  
```  
  
Intersect(Set_Expression1 , Set_Expression2 [ , ALL ] )  
```  
  
## Arguments  
 *Set_Expression1*  
 A valid Multidimensional Expressions (MDX) expression that returns a set.  
  
 *Set_Expression2*  
 A valid Multidimensional Expressions (MDX) expression that returns a set.  
  
## Remarks  
 The **Intersect** function returns the intersection of two sets. By default, the function removes duplicates from both sets prior to intersecting the sets. The two sets specified must have the same dimensionality.  
  
 The optional **ALL** flag retains duplicates. If **ALL** is specified, the **Intersect** function intersects nonduplicated elements as usual, and also intersects each duplicate in the first set that has a matching duplicate in the second set. The two sets specified must have the same dimensionality.  
  
## Example  
 The following query returns the Years 2003 and 2004, the two members that appear in both the sets specified:  
  
 `SELECT`  
  
 `INTERSECT(`  
  
 `{[Date].[Calendar Year].&[2001], [Date].[Calendar Year].&[2002],[Date].[Calendar Year].&[2003]}`  
  
 `, {[Date].[Calendar Year].&[2002],[Date].[Calendar Year].&[2003], [Date].[Calendar Year].&[2004]})`  
  
 `ON 0`  
  
 `FROM`  
  
 `[Adventure Works]`  
  
 The following query fails because the two sets specified contain members from different hierarchies:  
  
 `SELECT`  
  
 `INTERSECT(`  
  
 `{[Date].[Calendar Year].&[2001]}`  
  
 `, {[Customer].[City].&[Abingdon]&[ENG]})`  
  
 `ON 0`  
  
 `FROM`  
  
 `[Adventure Works]`  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
