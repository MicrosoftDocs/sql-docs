---
title: "+ (Add) (MDX) | Microsoft Docs"
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
  - "+ (add)"
  - "add operator (+)"
ms.assetid: f076d5bf-3ff3-4009-887a-28072fd599ca
caps.latest.revision: 29
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# + (Add) (MDX)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Performs an arithmetic operation that adds two numbers.  
  
## Syntax  
  
```  
  
Numeric_Expression + Numeric_Expression  
```  
  
#### Parameters  
 *Numeric Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a numeric value.  
  
## Return Value  
 A value with the data type of the parameter that has the higher precedence.  
  
## Remarks  
 Both expressions must be of the same data type, or one expression must be able to be implicitly converted to the data type of the other expression. If one expression evaluates to a null value, the operator returns the result of the other expression.  
  
## See Also  
 [MDX Operator Reference &#40;MDX&#41;](../mdx/mdx-operator-reference-mdx.md)  
  
  
