---
title: "= (Equal To) (DMX) | Microsoft Docs"
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
  - "equal operator (=)"
  - "= (equals operator)"
ms.assetid: 6ac019b1-6373-4e2c-a1ad-fe2dc6188ac3
caps.latest.revision: 13
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
ms.workload: "Inactive"
---
# = (Equal To) (DMX)
[!INCLUDE[ssas-appliesto-sqlas](../includes/ssas-appliesto-sqlas.md)]

  Performs a comparison operation that determines whether the value of one Data Mining Extensions (DMX) expression is equal to the value of another DMX expression.  
  
## Syntax  
  
```  
  
DMX_Expression = DMX_Expression   
```  
  
#### Parameters  
 *DMX_Expression*  
 A valid DMX expression.  
  
## Return Value  
 A Boolean value that contains TRUE if both parameters are non-null and the value of the first parameter is equal to the value of the second parameter. The Boolean value contains FALSE if both parameters are non-null and the value of the first parameter is not equal to the value of the second parameter. The Boolean value contains a null value if either parameter or both parameters evaluate to a null value.  
  
## See Also  
 [Comparison Operators &#40;DMX&#41;](../dmx/operators-comparison.md)   
 [Data Mining Extensions &#40;DMX&#41; Operator Reference](../dmx/data-mining-extensions-dmx-operator-reference.md)   
 [Operators &#40;DMX&#41;](../dmx/operators-dmx.md)  
  
  
