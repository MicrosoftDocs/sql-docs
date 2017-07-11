---
title: "&lt;&gt; (Not Equal To) (DMX) | Microsoft Docs"
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
  - "not equal operator (<>)"
  - "<> (not equal to operator)"
ms.assetid: df0e7901-9e31-452a-af14-471f5130c09d
caps.latest.revision: 12
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# &lt;&gt; (Not Equal To) (DMX)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Performs a comparison operation that determines whether the value of one Data Mining Extensions (DMX) expression is not equal to the value of another DMX expression.  
  
## Syntax  
  
```  
  
DMX_Expression <> DMX_Expression  
```  
  
#### Parameters  
 *DMX_Expression*  
 A valid DMX expression.  
  
## Return Value  
 A Boolean value that contains TRUE if both parameters are non-null and the value of the first parameter is not equal to the value of the second parameter. The Boolean value contains FALSE if both parameters are non-null and the value of the first parameter is equal to the value of the second parameter. The Boolean value contains a null value if either parameter or both parameters evaluate to a null value.  
  
## See Also  
 [Comparison Operators &#40;DMX&#41;](../dmx/operators-comparison.md)   
 [Data Mining Extensions &#40;DMX&#41; Operator Reference](../dmx/data-mining-extensions-dmx-operator-reference.md)   
 [Operators &#40;DMX&#41;](../dmx/operators-dmx.md)  
  
  
