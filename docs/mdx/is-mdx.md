---
title: "IS (MDX) | Microsoft Docs"
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
  - "IS"
dev_langs: 
  - "kbMDX"
helpviewer_keywords: 
  - "IS operator"
ms.assetid: dc8c0b91-3bb1-49e5-8d70-57545baaa8e0
caps.latest.revision: 29
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# IS (MDX)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Performs a logical comparison on two object expressions.  
  
## Syntax  
  
```  
  
Expression1 IS ( Expression2 | NULL )  
```  
  
#### Parameters  
 *Expression1*  
 A valid Multidimensional Expressions (MDX) expression that returns an MDX object reference.  
  
 *Expression2*  
 A valid MDX expression that returns an MDX object reference.  
  
## Return Value  
 A Boolean value that returns **true** if both arguments refer to the same object; otherwise, **false**. If the **NULL** keyword is specified, the operator returns **true** if *Expression1* is **null**; otherwise, **false**.  
  
## Remarks  
 The **IS** operator is often used to determine whether tuples and members are idempotent, meaning that they are exactly equivalent.  
  
## Examples  
 The following example shows how to use the **IS** operator to check if the current member on an axis is a specific member:  
  
 `With`  
  
 `//Returns TRUE if the currentmember is Bikes`  
  
 `Member [Measures].[IsBikes?] AS`  
  
 `[Product].[Category].CurrentMember IS [Product].[Category].&[1]`  
  
 `SELECT`  
  
 `{[Measures].[IsBikes?]} ON 0,`  
  
 `[Product].[Category].[Category].Members ON 1`  
  
 `FROM`  
  
 `[Adventure Works]`  
  
## See Also  
 [MDX Operator Reference &#40;MDX&#41;](../mdx/mdx-operator-reference-mdx.md)  
  
  
