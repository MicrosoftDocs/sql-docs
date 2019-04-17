---
title: "Descendants (MDX) | Microsoft Docs"
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
# Descendants (MDX)


  Returns the set of descendants of a member at a specified level or distance, optionally including or excluding descendants in other levels.  
  
## Syntax  
  
```  
  
Member expression syntax using a level expression  
Descendants(Member_Expression [ , Level_Expression [ ,Desc_Flag ] ] )  
  
Member expression syntax using a numeric expression  
Descendants(Member_Expression [ , Distance [ ,Desc_Flag ] ] )  
  
Set expression syntax using a level expression  
Descendants(Set_Expression [ , Level_Expression [ ,Desc_Flag ] ] )  
  
Member expression syntax using a numeric expression  
Descendants(Set_Expression [ , Distance [ ,Desc_Flag ] ] )  
  
```  
  
## Arguments  
 *Member_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a member.  
  
 *Set_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a set.  
  
 *Level_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a level.  
  
 *Distance*  
 A valid numeric expression that specifies the distance from the specified member.  
  
 *Desc_Flag*  
 A valid string expression specifying a description flag that distinguishes among possible sets of descendants.  
  
## Remarks  
 If a level is specified, the **Descendants** function returns a set that contains the descendants of the specified member or the members of the specified set, at a specified level, optionally modified by a flag specified in *Desc_Flag*.  
  
 If *Distance* is specified, the **Descendants** function returns a set that contains the descendants of the specified member or the members of the specified set that are the specified number of levels away in the hierarchy of the specified member, optionally modified by a flag specified in *Desc_Flag*. Typically, you use this function with the Distance argument to deal with ragged hierarchies. If the specified distance is zero (0), the function returns a set that consists only of the specified member or the specified set.  
  
 If a set expression is specified, the **Descendants** function is resolved individually for each member of the set, and the set is created again. In other words, the syntax used for the **Descendants** function is functionally equivalent to the MDX [Generate](../mdx/generate-mdx.md) function.  
  
 If no level or distance is specified, the default value for the level used by the function is determined by calling the [Level](../mdx/level-mdx.md) function (<\<Member>>.Level) for the specified member (if a member is specified) or by calling the **Level** function for each member of the specified set (if a set is specified). If no level expression, distance or flags are specified, the function performs as if the following syntax were used:  
  
 `Descendants`  
  
 `(`  
  
 `Member_Expression ,`  
  
 `Member_Expression.Level ,`  
  
 `SELF_BEFORE_AFTER`  
  
 `)`  
  
 If a level is specified and a description flag is not specified, the function performs as if the following syntax were used.  
  
 `Descendants`  
  
 `(`  
  
 `Member_Expression ,`  
  
 `Level_Expression,`  
  
 `SELF`  
  
 `)`  
  
 By changing the value of the description flag, you can include or exclude descendants at the specified level or distance, the children before or after the specified level or distance (until the leaf node), and the leaf children regardless of the specified level or distance. The following table describes the flags allowed in the *Desc_Flag* argument.  
  
|Flag|Description|  
|----------|-----------------|  
|SELF|Returns only descendant members from the specified level or at the specified distance. The function includes the specified member, if the specified level is the level of the specified member.|  
|AFTER|Returns descendant members from all levels subordinate to the specified level or distance.|  
|BEFORE|Returns descendant members from all levels between the specified member and the specified level, or at the specified distance. It includes the specified member, but does not include members from the specified level or distance.|  
|BEFORE_AND_AFTER|Returns descendant members from all levels subordinate to the level of the specified member. It includes the specified member, but does not include members from the specified level or at the specified distance.|  
|SELF_AND_AFTER|Returns descendant members from the specified level or at the specified distance and all levels subordinate to the specified level, or at the specified distance.|  
|SELF_AND_BEFORE|Returns descendant members from the specified level or at the specified distance, and from all levels between the specified member and the specified level, or at the specified distance, including the specified member.|  
|SELF_BEFORE_AFTER|Returns descendant members from all levels subordinate to the level of the specified member, and includes the specified member.|  
|LEAVES|Returns leaf descendant members between the specified member and the specified level, or at the specified distance.|  
  
## Examples  
 The following example returns the specified member (United States), and the members between the specified member (United States) and the members of the level before the specified level (City), The example returns the specified member itself (United States), and the members of the State-Province level (the level before the City level). This example includes commented arguments to enable you to easily test other arguments for this function.  
  
```  
SELECT Descendants  
   ([Geography].[Geography].[Country].&[United States]  
      //, [Geography].[Geography].[Country]  
   , [Geography].[Geography].[City]  
      //, [Geography].[Geography].Levels (3)  
      //, SELF   
      //, AFTER  
      , BEFORE  
      // BEFORE_AND_AFTER  
      //, SELF_AND_AFTER  
      //, SELF_AND_BEFORE  
      //,SELF_BEFORE_AFTER  
      //,LEAVES   
   ) ON 0  
FROM [Adventure Works]   
```  
  
 The following example returns the daily average of the `Measures.[Gross Profit Margin]` measure, calculated across the days of each month in the 2003 fiscal year, from the **Adventure Works** cube. The **Descendants** function returns a set of days determined from the current member of the `[Date].[Fiscal]` hierarchy.  
  
```  
WITH MEMBER Measures.[Avg Gross Profit Margin] AS Avg  
   (  
      Descendants( [Date].[Fiscal].CurrentMember,   
           [Date].[Fiscal].[Date]  
          ),   
        Measures.[Gross Profit Margin]  
   )  
SELECT  
   Measures.[Avg Gross Profit Margin] ON COLUMNS,  
   [Date].[Fiscal].[Month].Members ON ROWS  
FROM [Adventure Works]  
WHERE ([Date].[Fiscal Year].&[2003])  
```  
  
 The following example uses a level expression and returns the Internet Sales Amount for each State-Province in Australia, and returns the percentage of the total Internet Sales Amount for Australia for by each State-Province. This example uses the Item function to extract the first (and only) tuple from the set that is returned by the **Ancestors** function.  
  
```  
WITH MEMBER Measures.x AS   
   [Measures].[Internet Sales Amount] /   
   ( [Measures].[Internet Sales Amount],  
      Ancestors   
         ( [Customer].[Customer Geography].CurrentMember,   
           [Customer].[Customer Geography].[Country]  
         ).Item (0)  
   ), FORMAT_STRING = '0%'  
SELECT {[Measures].[Internet Sales Amount], Measures.x} ON 0,  
{Descendants   
   ( [Customer].[Customer Geography].[Country].&[Australia],   
     [Customer].[Customer Geography].[State-Province], SELF   
   )    
} ON 1  
FROM [Adventure Works]  
  
```  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
