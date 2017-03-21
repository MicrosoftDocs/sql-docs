---
title: "Comparison Operators (DMX) | Microsoft Docs"
ms.custom: ""
ms.date: "03/02/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "analysis-services/data-mining"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
dev_langs: 
  - "DMX"
helpviewer_keywords: 
  - "comparison operators [DMX]"
ms.assetid: eea3cf90-9683-4453-b1f3-da740f3a4885
caps.latest.revision: 17
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Operators - Comparison
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  You can use comparison operators with scalar data in any Data Mining Extensions (DMX) expression in [!INCLUDE[msCoName](../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)]. Comparison operators evaluate to a Boolean data type; they return TRUE or FALSE based on the outcome of the tested condition.  
  
 The following table identifies the comparison operators that DMX supports.  
  
|Operator|Description|  
|--------------|-----------------|  
|[&#60; &#40;Less Than&#41; &#40;DMX&#41;](../dmx/less-than-dmx.md)|For arguments that evaluate to a non-null value, returns TRUE if the value of the argument on the left is less than the value of the argument on the right; returns FALSE otherwise. If either argument or both arguments evaluate to a null value, the operator returns a null value.|  
|[&#62; &#40;Greater Than&#41; &#40;DMX&#41;](../dmx/greater-than-dmx.md)|For arguments that evaluate to a non-null value, returns TRUE if the value of the argument on the left is greater than the value of the argument on the right; returns FALSE otherwise. If either argument or both arguments evaluate to a null value, the operator returns a null value.|  
|[= &#40;Equal To&#41; &#40;DMX&#41;](../dmx/equal-to-dmx.md)|For arguments that evaluate to a non-null value, returns TRUE if the value of the argument on the left is equal to the value of the argument on the right; returns FALSE otherwise. If either argument or both arguments evaluate to a null value, the operator returns a null value.|  
|[&#60;&#62; &#40;Not Equal To&#41; &#40;DMX&#41;](../dmx/not-equal-to-dmx.md)|For arguments that evaluate to a non-null value, returns TRUE if the value of the argument on the left is not equal to the value of the argument on the right; returns FALSE otherwise. If either argument or both arguments evaluate to a null value, the operator returns a null value.|  
|[&#60;= &#40;Less Than or Equal To&#41; &#40;DMX&#41;](../dmx/less-than-or-equal-to-dmx.md)|For arguments that evaluate to a non-null value, returns TRUE if the value of the argument on the left is less than or equal to the value of the argument on the right; returns FALSE otherwise. If either argument or both arguments evaluate to a null value, the operator returns a null value.|  
|[&#62;= &#40;Greater Than or Equal To&#41; &#40;DMX&#41;](../dmx/greater-than-or-equal-to-dmx.md)|For arguments that evaluate to a non-null value, returns TRUE if the value of the argument on the left is greater than or equal to the value of the argument on the right; returns FALSE otherwise. If either argument or both arguments evaluate to a null value, the operator returns a null value.|  
  
 You can also use comparison operators in DMX statements and functions to look for a condition.  
  
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
  
  
