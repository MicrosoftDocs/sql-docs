---
title: "This (MDX) | Microsoft Docs"
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
# This (MDX)


  Returns the current subcube for use with assignments in the Multidimensional Expressions (MDX) calculation script.  
  
## Syntax  
  
```  
  
This   
```  
  
## Remarks  
 The **This** function can be used in the place of any subcube expression to provide the current subcube within the current scope within the MDX calculation script. The **This** function must be used on the left side of an assignment.  
  
## Examples  
 The following MDX Script fragment shows how the This keyword can be used with SCOPE statements to make assignments to subcubes:  
  
 `Scope`  
  
 `(`  
  
 `[Date].[Fiscal Year].&[2005],`  
  
 `[Date].[Fiscal].[Fiscal Quarter].Members,`  
  
 `[Measures].[Sales Amount Quota]`  
  
 `) ;`  
  
 `This = ParallelPeriod`  
  
 `(`  
  
 `[Date].[Fiscal].[Fiscal Year], 1,`  
  
 `[Date].[Fiscal].CurrentMember`  
  
 `) * 1.35 ;`  
  
 `/*-- Allocate equally to months in FY 2002 -----------------------------*/`  
  
 `Scope`  
  
 `(`  
  
 `[Date].[Fiscal Year].&[2002],`  
  
 `[Date].[Fiscal].[Month].Members`  
  
 `) ;`  
  
 `This = [Date].[Fiscal].CurrentMember.Parent / 3 ;`  
  
 `End Scope ;`  
  
 `End Scope;`  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)   
 [Calculations](../analysis-services/multidimensional-models-olap-logical-cube-objects/calculations.md)  
  
  
