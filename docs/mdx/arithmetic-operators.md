---
title: "Arithmetic Operators | Microsoft Docs"
ms.custom: ""
ms.date: "03/02/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
dev_langs: 
  - "kbMDX"
helpviewer_keywords: 
  - "arithmetic operators"
ms.assetid: 1dff3e20-fe9d-4155-bf06-27d6458188e9
caps.latest.revision: 27
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Arithmetic Operators
[!INCLUDE[tsql-appliesto-ss2008-all_md](../includes/tsql-appliesto-ss2008-all-md.md)]

  You can use arithmetic operators in Multidimensional Expressions (MDX) for any arithmetic computations, including addition, subtraction, multiplication, and division.  
  
 MDX supports the arithmetic operators listed in the following table.  
  
|Operator|Description|  
|--------------|-----------------|  
|[+ (Add)](../mdx/add-mdx.md)|Adds two numbers.|  
|[/ (Divide)](../mdx/divide-mdx-operator-reference.md)|Divides one number by another number.|  
|[* (Multiply)](../mdx/multiply-mdx.md)|Multiplies two numbers.|  
|[- (Subtract)](../mdx/subtract-mdx.md)|Subtracts two numbers.|  
|^ (Power)|Raises one number by another number.|  
  
> [!NOTE]  
>  MDX does not include a function to obtain the square root of a number. To obtain the square root of a number, raise it to the power of 0.5 using the ^ operatior.  
  
## Order of Precedence  
 The following rules determine the order of precedence for arithmetic operators in an MDX expression:  
  
-   When there is more than one arithmetic operator in an expression, MDX performs multiplication and division first, followed by subtraction and addition.  
  
-   When all arithmetic operators in an expression have the same level of precedence, the order of execution is left to right.  
  
-   Expressions within parentheses take precedence over all other operations.  
  
## See Also  
 [MDX Operator Reference &#40;MDX&#41;](../mdx/mdx-operator-reference-mdx.md)   
 [Operators &#40;MDX Syntax&#41;](../mdx/operators-mdx-syntax.md)  
  
  
