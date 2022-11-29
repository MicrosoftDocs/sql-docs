---
description: "ClosingPeriod (MDX)"
title: "ClosingPeriod (MDX) | Microsoft Docs"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
---
# ClosingPeriod (MDX)


  Returns the member that is the last sibling among the descendants of a specified member at a specified level.  
  
## Syntax  
  
```  
  
ClosingPeriod( [ Level_Expression [ ,Member_Expression ] ] )  
```  
  
## Arguments  
 *Level_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a level.  
  
 *Member_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a member.  
  
## Remarks  
 This function is primarily intended to be used against a dimension with a type of Time, but can be used with any dimension.  
  
-   If a level expression is specified, the **ClosingPeriod** function uses the dimension that contains the specified level and returns the last sibling among the descendants of the default member at the specified level.  
  
-   If both a level expression and a member expression are specified, the **ClosingPeriod** function returns the last sibling among the descendants of specified member at the specified level.  
  
-   If neither a level expression nor a member expression is specified, the **ClosingPeriod** function uses the default level and member of the dimension (if any) in the cube with a type of Time.  
  
 The **ClosingPeriod** function is equivalent to the following MDX statement:  
  
 `Tail(Descendants(Member_Expression, Level_Expression), 1)`.  
  
> [!NOTE]  
>  The [OpeningPeriod](../mdx/openingperiod-mdx.md) function is similar to the **ClosingPeriod** function, except that the **OpeningPeriod** function returns the first sibling instead of the last sibling.  
  
## Examples  
 The following example returns the value for the default measure for FY2007 member of the Date dimension (which has a semantic type of Time). This member is returned because the Fiscal Year level is the first descendant of the [All] level, the Fiscal hierarchy is the default hierarchy because it is the first user-defined hierarchy in the hierarchy collection, and the FY 2007 member is the last sibling in this hierarchy at this level.  
  
```  
SELECT ClosingPeriod() ON 0  
FROM [Adventure Works]  
```  
  
 The following example returns the value for the default measure for November 30, 2006 member at the Date.Date.Date level for the Date.Date attribute hierarchy. This member is the last sibling of the descendant of [All] level in the Date.Date attribute hierarchy.  
  
```  
SELECT ClosingPeriod ([Date].[Date].[Date]) ON 0  
FROM [Adventure Works]  
```  
  
 The following example returns the value for the default measure for December, 2003 member, which is the last sibling of the descendant of the 2003 member at the year level in the Calendar user-defined hierarchy.  
  
```  
SELECT ClosingPeriod ([Date].[Calendar].[Month],[Date].[Calendar].[Calendar Year].&[2003]) ON 0  
FROM [Adventure Works]  
```  
  
 The following example returns the value for the default measure for June, 2003 member, which is the last sibling of the descendant of the 2003 member at the year level in the Fiscal user-defined hierarchy.  
  
```  
SELECT ClosingPeriod ([Date].[Fiscal].[Month],[Date].[Fiscal].[Fiscal Year].&[2003]) ON 0  
FROM [Adventure Works]  
```  
  
## See Also  
 [OpeningPeriod &#40;MDX&#41;](../mdx/openingperiod-mdx.md)   
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)   
 [LastSibling &#40;MDX&#41;](../mdx/lastsibling-mdx.md)  
  
  
