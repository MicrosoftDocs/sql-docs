---
title: "DrillupLevel (MDX) | Microsoft Docs"
ms.custom: ""
ms.date: "03/02/2016"
ms.prod: "analysis-services"
ms.prod_service: "analysis-services"
ms.service: ""
ms.component: ""
ms.reviewer: ""
ms.suite: "pro-bi"
ms.technology: 
  - "analysis-services"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "DRILLUPLEVEL"
dev_langs: 
  - "kbMDX"
helpviewer_keywords: 
  - "DrillupLevel function"
ms.assetid: 63431f79-f3a1-40c4-bf57-2b6bd8991cc3
caps.latest.revision: 32
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
ms.workload: "Inactive"
---
# DrillupLevel (MDX)
[!INCLUDE[ssas-appliesto-sqlas](../includes/ssas-appliesto-sqlas.md)]

  Drills up the members of a set that are below a specified level.  
  
## Syntax  
  
```  
  
DrillupLevel(Set_Expression [ , Level_Expression ] )  
```  
  
## Arguments  
 *Set_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a set.  
  
 *Level_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a level.  
  
## Remarks  
 The **DrillupLevel** function returns a set of members organized hierarchically based on the members included in the specified set. Order is preserved among the members in the specified set.  
  
 If a level expression is specified, the **DrillupLevel** function constructs the set by retrieving only those members that are above the specified level. If a level expression is specified and there is no member of the specified level represented in the specified set, the specified set is returned.  
  
 If a level expression is not specified, the function constructs the set by retrieving only those members that are one level higher than the lowest level of the first dimension referenced in the specified set.  
  
## Example  
 The following example returns the set of members from the first set that are above the Subcategory level.  
  
```  
SELECT DrillUpLevel   
  ({[Product].[Product Categories].[All Products]  
    ,[Product].[Product Categories].[Subcategory].&[32],  
    [Product].[Product Categories].[Product].&[215]},  
  [Product].[Product Categories].[Subcategory]  
    )  
  ON 0  
  FROM [Adventure Works]  
  WHERE [Measures].[Internet Order Quantity]  
```  
  
## See Also  
 [MDX Function Reference &#40;MDX&#41;](../mdx/mdx-function-reference-mdx.md)  
  
  
