---
title: "Data Mining Extensions (DMX) Operator Reference | Microsoft Docs"
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
# Data Mining Extensions (DMX) Operator Reference
[!INCLUDE[ssas-appliesto-sqlas](../includes/ssas-appliesto-sqlas.md)]

  The Data Mining Extensions (DMX) language in [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] supports arithmetic, assignment, comparison, logical, and unary operators. The following table lists the operators that DMX supports.  
  
|Operator|Description|  
|--------------|-----------------|  
|[+ &#40;Add&#41; &#40;DMX&#41;](../dmx/add-dmx.md)|An arithmetic operator that adds two numbers together.|  
|[- &#40;Subtract&#41; &#40;DMX&#41;](../dmx/subtract-dmx.md)|An arithmetic operator that subtracts one number from another number.|  
|[&#42; &#40;Multiply&#41; &#40;DMX&#41;](../dmx/multiply-dmx.md)|An arithmetic operator that multiplies one number by another number.|  
|[&#40;Divide&#41; &#40;DMX&#41;](../dmx/divide-dmx.md)|An arithmetic operator that divides one number by another number.|  
|[&#60; &#40;Less Than&#41; &#40;DMX&#41;](../dmx/less-than-dmx.md)|A comparison operator. For arguments that evaluate to non-null values, returns TRUE if the value of the argument on the left is less than the value of the argument on the right; returns FALSE otherwise. If either argument or both arguments evaluate to a null value, the operator returns a null value.|  
|[&#62; &#40;Greater Than&#41; &#40;DMX&#41;](../dmx/greater-than-dmx.md)|A comparison operator. For arguments that evaluate to non-null values, returns TRUE if the value of the argument on the left is greater than the value of the argument on the right; returns FALSE otherwise. If either argument or both arguments evaluate to a null value, the operator returns a null value.|  
|[= &#40;Equal To&#41; &#40;DMX&#41;](../dmx/equal-to-dmx.md)|A comparison operator. For arguments that evaluate to non-null values, returns TRUE if the value of the argument on the left is equal to the value of the argument on the right; returns FALSE otherwise. If either argument or both arguments evaluate to a null value, the operator returns a null value.|  
|[&#60;&#62; &#40;Not Equal To&#41; &#40;DMX&#41;](../dmx/not-equal-to-dmx.md)|A comparison operator. For arguments that evaluate to non-null values, returns TRUE if the value of the argument on the left is not equal to the value of the argument on the right; returns FALSE otherwise. If either argument or both arguments evaluate to a null value, the operator returns a null value.|  
|[&#60;= &#40;Less Than or Equal To&#41; &#40;DMX&#41;](../dmx/less-than-or-equal-to-dmx.md)|A comparison operator. For arguments that evaluate to non-null values, returns TRUE if the value of the argument on the left is less than or equal to the value of the argument on the right; returns FALSE otherwise. If either argument or both arguments evaluate to a null value, the operator returns a null value.|  
|[&#62;= &#40;Greater Than or Equal To&#41; &#40;DMX&#41;](../dmx/greater-than-or-equal-to-dmx.md)|A comparison operator. For arguments that evaluate to non-null values, returns TRUE if the value of the argument on the left is greater than or equal to the value of the argument on the right; returns FALSE otherwise. If either argument or both arguments evaluate to a null value, the operator returns a null value.|  
|[AND &#40;DMX&#41;](../dmx/and-dmx.md)|A logical operator that performs a conjunction on two numeric expressions.|  
|[NOT &#40;DMX&#41;](../dmx/not-dmx.md)|A logical operator that performs a negation on a numeric expression.|  
|[OR &#40;DMX&#41;](../dmx/or-dmx.md)|A logical operator that performs a disjunction on two numeric expressions.|  
|[+ &#40;Positive&#41; &#40;DMX&#41;](../dmx/positive-dmx.md)|A unary operator that returns the positive value of a numeric expression.|  
|[- &#40;Negative&#41; &#40;DMX&#41;](../dmx/negative-dmx.md)|A unary operator that returns the negative value of a numeric expression.|  
|[Double Slash &#40;Comment&#41; &#40;DMX&#41;](../dmx/double-slash-comment-dmx.md)|Indicates a text string that [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] should not execute. You can nest comments within a DMX statement, include them at the end of a line of code, or insert them on a separate line.|  
|[-- &#40;Comment&#41; &#40;DMX&#41; Summary](../dmx/comment-dmx-summary.md)|Indicates a text string that [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] should not execute. You can nest comments within a DMX statement, include them at the end of a line of code, or insert them on a separate line.|  
|[Slash Star &#40;Comment&#41; &#40;DMX&#41;](../dmx/slash-star-comment-dmx.md)|Indicates a text string that [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] should not execute. You can nest comments within a DMX statement, include them at the end of a line of code, or insert them on a separate line.|  
  
## See Also  
 [Data Mining Extensions &#40;DMX&#41; Function Reference](../dmx/data-mining-extensions-dmx-function-reference.md)   
 [Data Mining Extensions &#40;DMX&#41; Reference](../dmx/data-mining-extensions-dmx-reference.md)   
 [Data Mining Extensions &#40;DMX&#41; Statement Reference](../dmx/data-mining-extensions-dmx-statements.md)   
 [Data Mining Extensions &#40;DMX&#41; Syntax Conventions](../dmx/data-mining-extensions-dmx-syntax-conventions.md)   
 [Data Mining Extensions &#40;DMX&#41; Syntax Elements](../dmx/data-mining-extensions-dmx-syntax-elements.md)   
 [Operators &#40;DMX&#41;](../dmx/operators-dmx.md)  
  
  
