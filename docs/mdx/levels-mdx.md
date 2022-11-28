---
description: "Levels (MDX)"
title: "Levels (MDX) | Microsoft Docs"
ms.date: 02/17/2022
ms.service: sql
ms.subservice: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
---
# Levels (MDX)


  Returns the level whose position in a dimension or hierarchy is specified by a numeric expression or whose name is specified by a string expression.  
  
## Syntax  
  
```  
  
Numeric expression syntax  
Hierarchy_Expression.Levels( Level_Number )  
  
String expression syntax  
Hierarchy_Expression.Levels( Level_Name )  
```  
  
## Arguments  
 *Hierarchy_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a hierarchy.  
  
 *Level_Number*  
 A valid numeric expression that specifies a level number.  
  
 *Level_Name*  
 A valid string expression that specifies a level name.  
  
## Remarks  
 If a level number is specified, the **Levels** function returns the level associated with the specified zero-based position.  
  
 If a level name is specified, the **Levels** function returns the specified level.  
  
> [!NOTE]  
>  Use the string expression syntax for user-defined functions.  
  
## Examples  
 The following examples illustrate each of the **Levels** function syntaxes.  
  
### Numeric  
 The following example returns the Country level:  
  
```  
SELECT [Geography].[Geography].Levels(1) ON 0  
FROM [Adventure Works]  
```  
  
### String  
 The following example returns the Country level:  
  
```  
SELECT [Geography].[Geography].Levels('Country') ON 0  
FROM [Adventure Works]  
```  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
