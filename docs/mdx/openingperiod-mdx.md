---
description: "OpeningPeriod (MDX)"
title: "OpeningPeriod (MDX) | Microsoft Docs"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
---
# OpeningPeriod (MDX)


  Returns the first sibling among the descendants of a specified level, optionally at a specified member.  
  
## Syntax  
  
```  
  
OpeningPeriod( [ Level_Expression [ , Member_Expression ] ] )  
```  
  
## Arguments  
 *Level_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a level.  
  
 *Member_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a member.  
  
## Remarks  
 This function is primarily intended to be used the Time dimension, but can be used with any dimension.  
  
-   If a level expression is specified, the **OpeningPeriod** function uses the hierarchy that contains the specified level and returns the first sibling among the descendants of the default member at the specified level.  
  
-   If both a level expression and a member expression are specified, the **OpeningPeriod** function returns the first sibling among the descendants of specified member at the specified level within the hierarchy containing the specified level.  
  
-   If neither a level expression nor a member expression are specified, the **OpeningPeriod** function uses the default level and member of the dimension with a type of Time.  
  
> [!NOTE]  
>  The [ClosingPeriod](../mdx/closingperiod-mdx.md) function is similar to the **OpeningPeriod** function, except that the **ClosingPeriod** function returns the last sibling instead of the first sibling.  
  
## Examples  
 The following example returns the value for the default measure for the FY2002 member of the Date dimension (which has a type of Time). This member is returned because the Fiscal Year level is the first descendant of the [All] level, the Fiscal hierarchy is the default hierarchy because it is the first user-defined hierarchy in the hierarchy collection, and the FY2002 member is the first sibling in this hierarchy at this level.  
  
```  
SELECT OpeningPeriod() ON 0  
FROM [Adventure Works]  
  
```  
  
 The following example returns the value for the default measure for July 1, 2001 member at the Date.Date.Date level for the Date.Date attribute hierarchy. This member is the first sibling of the descendant of [All] level in the Date.Date attribute hierarchy.  
  
```  
SELECT OpeningPeriod([Date].[Date].[Date]) ON 0  
FROM [Adventure Works]  
  
```  
  
 The following example returns the value for the default measure for January, 2003 member, which is the first sibling of the descendant of the 2003 member at the year level in the Calendar user-defined hierarchy.  
  
```  
SELECT OpeningPeriod([Date].[Calendar].[Month],[Date].[Calendar].[Calendar Year].&[2003]) ON 0  
FROM [Adventure Works]  
  
```  
  
 The following example returns the value for the default measure for July, 2002 member, which is the first sibling of the descendant of the 2003 member at the year level in the Fiscal user-defined hierarchy.  
  
```  
SELECT OpeningPeriod([Date].[Fiscal].[Month],[Date].[Fiscal].[Fiscal Year].&[2003]) ON 0  
FROM [Adventure Works]  
  
```  
  
## See Also  
 [TopCount &#40;MDX&#41;](../mdx/topcount-mdx.md)   
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)   
 [FirstSibling &#40;MDX&#41;](../mdx/firstsibling-mdx.md)  
  
  
