---
title: "+ (Union) (MDX) | Microsoft Docs"
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
  - "+"
dev_langs: 
  - "kbMDX"
helpviewer_keywords: 
  - "union operator (+)"
  - "+ (union operator)"
ms.assetid: 6c6dfca2-7413-452a-98a2-3d8c58a8a3e6
caps.latest.revision: 43
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Union - MDX operator reference
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Performs a set operation that returns a union of two sets, removing duplicate members.  
  
## Syntax  
  
```  
  
Set_Expression + Set_Expression      
```  
  
#### Parameters  
 *Set_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a set.  
  
## Return Value  
 A set that contains the members of both specified sets.  
  
## Remarks  
 The **+ (Union)** operator is functionally equivalent to the [Union  &#40;MDX&#41;](../mdx/union-mdx.md) function.  
  
## Examples  
 The following example demonstrates the use of this operator.  
  
```  
-- This member returns the gross profit margin for each year for North American countries.  
SELECT   
    [Date].[Calendar].[Calendar Year].Members ON 0,  
    {[Sales Territory].[Sales Territory].[Country].[United States]} +  
     {[Sales Territory].[Sales Territory].[Country].[Canada]} ON 1  
FROM  
    [Adventure Works]  
WHERE  
    ([Measures].[Gross Profit Margin])  
```  
  
## See Also  
 [MDX Operator Reference &#40;MDX&#41;](../mdx/mdx-operator-reference-mdx.md)  
  
  
