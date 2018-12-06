---
title: "Arithmetic Operators | Microsoft Docs"
ms.date: 06/04/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: mdx
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Arithmetic Operators


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
  
  
