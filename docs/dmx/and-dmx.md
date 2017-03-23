---
title: "AND (DMX) | Microsoft Docs"
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
f1_keywords: 
  - "AND"
dev_langs: 
  - "DMX"
helpviewer_keywords: 
  - "AND, DMX"
ms.assetid: d337b20c-f80e-48be-9354-371367e53735
caps.latest.revision: 13
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# AND (DMX)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Performs a logical conjunction on two numeric expressions.  
  
## Syntax  
  
```  
  
Expression1 AND Expression2  
```  
  
#### Parameters  
 *Expression1*  
 A valid Data Mining Extensions (DMX) expression that returns a numeric value.  
  
 *Expression2*  
 A valid DMX expression that returns a numeric value.  
  
## Return Value  
 A Boolean value that returns TRUE if both parameters evaluate to TRUE; otherwise FALSE.  
  
## Remarks  
 Both parameters are treated as Boolean values (0 as FALSE; otherwise TRUE) before the operator performs the logical conjunction. The following table lists the values that are returned based on the various combinations of parameter values.  
  
|If Expression1 is|If Expression2 is|Return value is|  
|-----------------------|-----------------------|---------------------|  
|TRUE|TRUE|TRUE|  
|TRUE|FALSE|FALSE|  
|FALSE|TRUE|FALSE|  
|FALSE|FALSE|FALSE|  
  
## See Also  
 [Data Mining Extensions &#40;DMX&#41; Operator Reference](../dmx/data-mining-extensions-dmx-operator-reference.md)   
 [Logical Operators &#40;DMX&#41;](../dmx/operators-logical.md)   
 [Operators &#40;DMX&#41;](../dmx/operators-dmx.md)  
  
  
