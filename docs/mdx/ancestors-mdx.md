---
description: "Ancestors (MDX)"
title: "Ancestors (MDX) | Microsoft Docs"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
---
# Ancestors (MDX)


  A function that returns the set of all ancestors of a specified member at a specified level or at a specified distance from the member. With [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)], the set returned will always consist of a single member - [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] does not support multiple parents for a single member.  
  
## Syntax  
  
```  
  
Level syntax  
Ancestors(Member_Expression, Level_Expression)  
  
Numeric syntax  
Ancestors(Member_Expression, Distance)  
```  
  
## Arguments  
 *Member_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a member.  
  
 *Level_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a level.  
  
 *Distance*  
 A valid numeric expression that specifies the distance from the specified member.  
  
## Remarks  
 With the **Ancestors** function, you provide the function with an MDX member expression and then provide either an MDX expression of a level that is an ancestor of that member or a numeric expression that represents the number of levels above that member. With this information, the **Ancestors** function returns the set of members (which will be a set consisting of one member) at that level.  
  
> [!NOTE]  
>  To return an ancestor member, rather than an ancestor set, use the [Ancestor](../mdx/ancestor-mdx.md) function.  
  
 If a level expression is specified, the **Ancestors** function returns the set of all ancestors of the specified member at the specified level. If the specified member is not within the same hierarchy as the specified level, the function returns an error.  
  
 If a distance is specified, the **Ancestors** function returns the set of all members that are the number of steps specified up in the hierarchy specified by the member expression. A member may be specified as a member of an attribute hierarchy, a user-defined hierarchy, or, in some cases, a parent-child hierarchy. A number of 1 returns the set of members at the parent level and a number of 2 returns the set of members at the grandparent level (if one exists). A number of 0 returns the set including only the member itself.  
  
> [!NOTE]  
>  Use this form of the **Ancestors** function for cases in which the level of the parent is unknown or cannot be named.  
  
## Examples  
 The following example uses the **Ancestors** function to return the Internet Sales Amount measure for a member, its parent, and its grandparent. This example uses level expressions to specify the levels to be returned. The levels are in the same hierarchy as the member specified in the member expression.  
  
```  
SELECT {  
    Ancestors([Product].[Product Categories].[Product].[Mountain-100 Silver, 38],[Product].[Product Categories].[Category]),  
    Ancestors([Product].[Product Categories].[Product].[Mountain-100 Silver, 38],[Product].[Product Categories].[Subcategory]),  
    Ancestors([Product].[Product Categories].[Product].[Mountain-100 Silver, 38],[Product].[Product Categories].[Product])  
    } ON 0,  
[Measures].[Internet Sales Amount] ON 1  
FROM [Adventure Works]  
```  
  
 The following example uses the **Ancestors** function to return the Internet Sales Amount measure for a member, its parent, and its grandparent. This example uses numeric expressions to specify the levels being returned. The levels are in the same hierarchy as the member specified in the member expression.  
  
```  
SELECT {  
   Ancestors(  
      [Product].[Product Categories].[Product].[Mountain-100 Silver, 38],2  
      ),  
   Ancestors(  
      [Product].[Product Categories].[Product].[Mountain-100 Silver, 38],1  
      ),  
   Ancestors(  
      [Product].[Product Categories].[Product].[Mountain-100 Silver, 38],0  
      )  
   } ON 0,  
[Measures].[Internet Sales Amount] ON 1  
FROM  [Adventure Works]  
```  
  
 The following example uses the **Ancestors** function to return the Internet Sales Amount measure for the parent of a member of an attribute hierarchy. This example uses a numeric expression to specify the level being returned. Since the member in the member expression is a member of an attribute hierarchy, its parent is the [All] level.  
  
```  
SELECT {  
   Ancestors(  
      [Product].[Product].[Mountain-100 Silver, 38],1  
      )  
   } ON 0,  
[Measures].[Internet Sales Amount] ON 1  
FROM [Adventure Works]  
```  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
