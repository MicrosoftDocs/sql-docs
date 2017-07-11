---
title: "Max (MDX) | Microsoft Docs"
ms.custom: ""
ms.date: "03/02/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "MAX"
dev_langs: 
  - "kbMDX"
helpviewer_keywords: 
  - "Max function [MDX]"
ms.assetid: 745c2b3e-022b-471a-ac16-e39ecb3297ea
caps.latest.revision: 29
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Max (MDX)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns the maximum value of a numeric expression that is evaluated over a set.  
  
## Syntax  
  
```  
  
Max( Set_Expression [ , Numeric_Expression ] )  
```  
  
## Arguments  
 *Set_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a set.  
  
 *Numeric_Expression*  
 A valid numeric expression that is typically a Multidimensional Expressions (MDX) expression of cell coordinates that return a number.  
  
## Remarks  
 If a numeric expression is specified, the specified numeric expression is evaluated across the set and then returns the maximum value from that evaluation. If a numeric expression is not specified, the specified set is evaluated in the current context of the members of the set and then returns the maximum value from that evaluation.  
  
> [!NOTE]  
>  [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] ignores nulls when calculating the maximum value in a set of numbers.  
  
## Example  
 The following example returns the maximum monthly sales for each quarter, subcategory, and country in the Adventure Works cube.  
  
```  
WITH MEMBER Measures.x AS Max   
   ([Date].[Calendar].CurrentMember.Children  
      , [Measures].[Reseller Order Quantity]  
   )  
SELECT Measures.x ON 0  
,NON EMPTY [Date].[Calendar].[Calendar Quarter]*   
   [Product].[Product Categories].[Subcategory].members *  
   [Geography].[Geography].[Country].Members  
ON 1  
FROM [Adventure Works]  
  
```  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
