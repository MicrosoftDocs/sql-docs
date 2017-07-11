---
title: "+ (String Concatenation) (MDX) | Microsoft Docs"
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
  - "string concatenation operators"
  - "+ (string concatenation)"
ms.assetid: d77636b1-2973-4587-af35-54aba5700d9a
caps.latest.revision: 29
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# + (String Concatenation) (MDX)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Performs a string operation that concatenates two or more character strings, tuples, or a combination of strings and tuples.  
  
## Syntax  
  
```  
  
String_Expression + String_Expression  
```  
  
#### Parameters  
 *String_Expression*  
 A valid Multidimensional Expressions (MDX) expression that returns a string value.  
  
## Return Value  
 A value with the data type of the parameter that has the higher precedence.  
  
## Remarks  
 Both expressions must be of the same data type, or one expression must be able to be implicitly converted to the data type of the other expression. If one expression evaluates to a null value, the operator returns the result of the other expression.  
  
## See Also  
 [MDX Operator Reference &#40;MDX&#41;](../mdx/mdx-operator-reference-mdx.md)  
  
  
