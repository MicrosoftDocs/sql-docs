---
title: "Arithmetic Operators (DMX) | Microsoft Docs"
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
# Operators - Arithmetic
[!INCLUDE[ssas-appliesto-sqlas](../includes/ssas-appliesto-sqlas.md)]

  You can use arithmetic operators in Data Mining Extensions (DMX) for arithmetic computations in [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)], including addition, subtraction, multiplication, and division.  
  
 The following table identifies the arithmetic operators that DMX supports.  
  
|Operator|Description|  
|--------------|-----------------|  
|[+ &#40;Add&#41; &#40;DMX&#41;](../dmx/add-dmx.md)|Adds two numbers together.|  
|[- &#40;Subtract&#41; &#40;DMX&#41;](../dmx/subtract-dmx.md)|Subtracts one number from another number.|  
|[&#42; &#40;Multiply&#41; &#40;DMX&#41;](../dmx/multiply-dmx.md)|Multiplies one number by another number.|  
|[&#40;Divide&#41; &#40;DMX&#41;](../dmx/divide-dmx.md)|Divides one number by another number.|  
  
 The following rules determine the order of precedence for arithmetic operators in a DMX expression:  
  
-   When there is more than one arithmetic operator in an expression, multiplication and division are calculated first, followed by subtraction and addition.  
  
-   When all the arithmetic operators in an expression have the same level of precedence, the order of execution is left to right.  
  
-   Expressions that are within parentheses take precedence over all other operations.  
  
## See Also  
 [Data Mining Extensions &#40;DMX&#41; Reference](../dmx/data-mining-extensions-dmx-reference.md)   
 [Data Mining Extensions &#40;DMX&#41; Function Reference](../dmx/data-mining-extensions-dmx-function-reference.md)   
 [Data Mining Extensions &#40;DMX&#41; Operator Reference](../dmx/data-mining-extensions-dmx-operator-reference.md)   
 [Data Mining Extensions &#40;DMX&#41; Statement Reference](../dmx/data-mining-extensions-dmx-statements.md)   
 [Data Mining Extensions &#40;DMX&#41; Syntax Conventions](../dmx/data-mining-extensions-dmx-syntax-conventions.md)   
 [Data Mining Extensions &#40;DMX&#41; Syntax Elements](../dmx/data-mining-extensions-dmx-syntax-elements.md)   
 [Expressions &#40;DMX&#41;](../dmx/expressions-dmx.md)   
 [General Prediction Functions &#40;DMX&#41;](../dmx/general-prediction-functions-dmx.md)   
 [Operators &#40;DMX&#41;](../dmx/operators-dmx.md)   
 [Structure and Usage of DMX Prediction Queries](../dmx/structure-and-usage-of-dmx-prediction-queries.md)   
 [Understanding the DMX Select Statement](../dmx/understanding-the-dmx-select-statement.md)  
  
  
