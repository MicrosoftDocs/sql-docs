---
title: "= (Equal To) (MDX) | Microsoft Docs"
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
  - "="
dev_langs: 
  - "kbMDX"
helpviewer_keywords: 
  - "equals operator (=)"
  - "= (equals operator)"
ms.assetid: 5e1f3b58-a646-4fc1-a3f1-19090a5437b7
caps.latest.revision: 36
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# = (Equal To) (MDX)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Performs a comparison operation that determines whether the value of one Multidimensional Expressions (MDX) expression is equal to the value of another MDX expression.  
  
> [!NOTE]  
>  To compare objects, use the [IS &#40;MDX&#41;](../mdx/is-mdx.md) operator. For example, use the IS operator when you are checking if the current member on a query axis is a specific member.  
  
## Syntax  
  
```  
  
MDX_Expression = MDX_Expression   
```  
  
#### Parameters  
 *MDX_Expression*  
 A valid MDX expression.  
  
## Return Value  
 A Boolean value based on the following conditions:  
  
-   **true** if the value of the first parameter is equal to the value of the second parameter.  
  
-   **false** if the value of the first parameter is not equal to the value of the second parameter.  
  
-   **true** if both parameters are null, or one parameter is null and the other parameter is 0.  
  
## Examples  
 The following query shows examples of these conditions:  
  
 `With`  
  
 `--Returns true`  
  
 `Member [Measures].bool1 as 1=1`  
  
 `--Returns false`  
  
 `Member [Measures].bool2 as 1=0`  
  
 `--Returns true`  
  
 `Member [Measures].bool3 as null=null`  
  
 `--Returns true`  
  
 `Member [Measures].bool4 as 0=null`  
  
 `--Returns false`  
  
 `Member [Measures].bool5 as 1=null`  
  
 `Select {[Measures].bool1,[Measures].bool2,[Measures].bool3,[Measures].bool4,[Measures].bool5}`  
  
 `On 0`  
  
 `From [Adventure Works]`  
  
## See Also  
 [MDX Operator Reference &#40;MDX&#41;](../mdx/mdx-operator-reference-mdx.md)  
  
  
