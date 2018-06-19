---
title: "Operators (DMX) | Microsoft Docs"
ms.date: 06/07/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: dmx
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Operators (DMX)
[!INCLUDE[ssas-appliesto-sqlas](../includes/ssas-appliesto-sqlas.md)]

  You can use Data Mining Extensions (DMX) operators to perform arithmetic, comparison, concatenation, and logical operations in a query in [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)].  
  
 [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] uses operators to perform the following actions:  
  
-   Search for values or objects that meet a specific condition.  
  
-   Implement a decision between values or expressions.  
  
 DMX uses several categories of operators, described in the following sections. For additional information about individual operators, see [Data Mining Extensions &#40;DMX&#41; Operator Reference](../dmx/data-mining-extensions-dmx-operator-reference.md).  
  
|Operator category|Type of operation|  
|-----------------------|-----------------------|  
|[Arithmetic Operators &#40;DMX&#41;](../dmx/operators-arithmetic.md)|Perform addition, subtraction, multiplication, or division.|  
|[Comparison Operators &#40;DMX&#41;](../dmx/operators-comparison.md)|Compare one value against another value or an expression.|  
|[Logical Operators &#40;DMX&#41;](../dmx/operators-logical.md)|Test for the truth of a condition, such as AND, OR, or NOT.|  
|[Unary Operators &#40;DMX&#41;](../dmx/operators-unary.md)|Perform an operation on a single operand.|  
  
 You can use operators to combine smaller expressions in DMX into more complex expressions. In complex expressions, the operators are evaluated in order based on the [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] definition of operator precedence. Operators that have higher precedence are performed before operators that have lower precedence. For more information about expressions, see [Expressions &#40;DMX&#41;](../dmx/expressions-dmx.md).  
  
 When you combine simple expressions to form a complex expression, the data type of the resulting expression is determined by combining the rules for the operators with the rules for data type precedence. If the result is a character or a Unicode value, [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] determines the collation of the result by combining the rules for the operators with the rules for collation precedence. There are also rules that determine the precision, scale, and length of the result based on the precision, scale, and length of the simple expressions.  
  
## See Also  
 [Data Mining Extensions &#40;DMX&#41; Reference](../dmx/data-mining-extensions-dmx-reference.md)   
 [Data Mining Extensions &#40;DMX&#41; Function Reference](../dmx/data-mining-extensions-dmx-function-reference.md)   
 [Data Mining Extensions &#40;DMX&#41; Statement Reference](../dmx/data-mining-extensions-dmx-statements.md)   
 [Data Mining Extensions &#40;DMX&#41; Syntax Conventions](../dmx/data-mining-extensions-dmx-syntax-conventions.md)   
 [Data Mining Extensions &#40;DMX&#41; Syntax Elements](../dmx/data-mining-extensions-dmx-syntax-elements.md)   
 [General Prediction Functions &#40;DMX&#41;](../dmx/general-prediction-functions-dmx.md)   
 [Structure and Usage of DMX Prediction Queries](../dmx/structure-and-usage-of-dmx-prediction-queries.md)   
 [Understanding the DMX Select Statement](../dmx/understanding-the-dmx-select-statement.md)  
  
  
