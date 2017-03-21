---
title: "* (Crossjoin) (MDX) | Microsoft Docs"
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
  - "*"
dev_langs: 
  - "kbMDX"
helpviewer_keywords: 
  - "* (crossjoin operator)"
  - "crossjoin operator (*)"
ms.assetid: e00cb260-0189-4c4e-b3d2-088f4421337b
caps.latest.revision: 41
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Crossjoin  - MDX Operator Reference
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Performs a set operation that returns the cross product of two sets.  
  
## Syntax  
  
```  
  
Set_Expression * Set_Expression  
```  
  
## Parameter  
 *Set_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a set.  
  
## Return Value  
 A set that contains the cross product of both specified parameters.  
  
## Remarks  
 The **\* (Crossjoin)** operator is functionally equivalent to the [Crossjoin](../mdx/crossjoin-mdx.md) function.  
  
## Examples  
 The following example demonstrates the use of this operator.  
  
```  
-- This query returns the gross profit margin for product types  
-- and reseller types crossjoined by year.  
SELECT   
    [Date].[Calendar].[Calendar Year].Members *  
    [Reseller].[Reseller Type].Children ON 0,  
    [Product].[Category].[Category].Members ON 1  
FROM  
    [Adventure Works]  
WHERE  
    ([Measures].[Gross Profit Margin])  
```  
  
## See Also  
 [MDX Operator Reference &#40;MDX&#41;](../mdx/mdx-operator-reference-mdx.md)  
  
  
