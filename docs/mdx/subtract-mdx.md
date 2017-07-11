---
title: "- (Subtract) (MDX) | Microsoft Docs"
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
  - "-"
dev_langs: 
  - "kbMDX"
helpviewer_keywords: 
  - "- (subtract)"
  - "subtract operator (-)"
ms.assetid: 56e610a1-efbd-4c11-bd7c-bf20a6553e57
caps.latest.revision: 35
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# - (Subtract) (MDX)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Performs an arithmetic operation that subtracts one number from another number.  
  
## Syntax  
  
```  
  
Numeric_Expression - Numeric_Expression  
```  
  
#### Parameters  
 *Numeric_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a numeric value.  
  
## Return Value  
 A value with the data type of the parameter that has the higher precedence.  
  
## Remarks  
 Both expressions must be of the same data type, or one expression must be able to be implicitly converted to the data type of the other expression. If one expression evaluates to a null value, the operator returns the result of the non-null expression.  
  
## Examples  
 The following example demonstrates the use of this operator.  
  
```  
-- This member returns the increase or decrease  
-- in gross profit margin over a month.  
WITH MEMBER [Measures].[GPM Delta] AS  
 (  
(Measures.[Gross Profit Margin]) -   
([Date].[Calendar].CurrentMember.PrevMember,   
Measures.[Gross Profit Margin])  
  ), FORMAT_STRING = 'Percent'  
  
SELECT   
DESCENDANTS(  
[Date].[Calendar].[Calendar Year].&[2002],   
[Date].[Calendar].[Month]) ON 0,  
[Product].[Category].[Category].Members ON 1  
FROM  
[Adventure Works]  
WHERE  
([Measures].[GPM Delta])  
```  
  
## See Also  
 [MDX Operator Reference &#40;MDX&#41;](../mdx/mdx-operator-reference-mdx.md)  
  
  
