---
title: "&gt; (Greater Than) (DMX) | Microsoft Docs"
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
  - "greater than operator (>)"
  - "> (greater than operator)"
ms.assetid: a1485c02-8d10-4722-b8cd-b747c472741b
caps.latest.revision: 13
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# &gt; (Greater Than) (DMX)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Performs a comparison operation that determines whether the value of one Data Mining Extensions (DMX) expression is greater than the value of another DMX expression.  
  
## Syntax  
  
```  
  
DMX_Expression > DMX_Expression  
```  
  
#### Parameters  
 *DMX_Expression*  
 A valid DMX expression.  
  
## Return Value  
 A Boolean value that contains TRUE if both parameters are non-null and the first parameter has a value that is greater than the value of the second parameter. The Boolean value contains FALSE if both parameters are non-null and the first parameter has a value that is equal to or less than the value of the second parameter. The Boolean value contains a null value if either parameter or both parameters evaluate to a null value.  
  
## See Also  
 [Comparison Operators &#40;DMX&#41;](../dmx/operators-comparison.md)   
 [Data Mining Extensions &#40;DMX&#41; Operator Reference](../dmx/data-mining-extensions-dmx-operator-reference.md)   
 [Operators &#40;DMX&#41;](../dmx/operators-dmx.md)  
  
  
