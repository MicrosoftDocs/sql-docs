---
title: "- (Subtract) (DMX) | Microsoft Docs"
ms.custom: ""
ms.date: "03/02/2016"
ms.prod: "analysis-services"
ms.prod_service: "analysis-services"
ms.service: ""
ms.component: ""
ms.reviewer: ""
ms.suite: "pro-bi"
ms.technology: 
  - "analysis-services"
  - "analysis-services/data-mining"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
dev_langs: 
  - "DMX"
helpviewer_keywords: 
  - "- (subtract)"
  - "subtract operator (-)"
ms.assetid: 9602e908-e80c-442a-a412-073e10d0abd4
caps.latest.revision: 12
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
ms.workload: "Inactive"
---
# - (Subtract) (DMX)
[!INCLUDE[ssas-appliesto-sqlas](../includes/ssas-appliesto-sqlas.md)]

  Performs an arithmetic operation that subtracts one number from another number.  
  
## Syntax  
  
```  
  
Numeric_Expression - Numeric_Expression  
```  
  
#### Parameters  
 *Numeric_Expression*  
 A valid Data Mining Extensions (DMX) expression that returns a numeric value.  
  
## Return Value  
 A value that has the data type of the parameter that has the higher precedence.  
  
## Remarks  
 Both expressions must be of the same data type, or one expression must be able to be implicitly converted to the data type of the other expression. If one expression evaluates to a null value, the operator returns the result of the other expression.  
  
## See Also  
 [Arithmetic Operators &#40;DMX&#41;](../dmx/operators-arithmetic.md)   
 [Data Mining Extensions &#40;DMX&#41; Operator Reference](../dmx/data-mining-extensions-dmx-operator-reference.md)   
 [Operators &#40;DMX&#41;](../dmx/operators-dmx.md)  
  
  
