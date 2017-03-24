---
title: "NOT (DMX) | Microsoft Docs"
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
  - "NOT"
dev_langs: 
  - "DMX"
helpviewer_keywords: 
  - "NOT operator [DMX]"
ms.assetid: 6d91b3d9-270c-4a68-b41f-169cff5faa0e
caps.latest.revision: 13
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# NOT (DMX)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  A logical operator that performs a logical negation on a numeric expression.  
  
## Syntax  
  
```  
  
NOT Expression1  
```  
  
#### Parameters  
 *Expression1*  
 A valid DMX expression that returns a numeric value.  
  
## Return Value  
 A Boolean value that returns FALSE if the argument evaluates to TRUE; otherwise FALSE.  
  
## Remarks  
 The argument is treated as a Boolean value (0 as FALSE; otherwise TRUE) before the operator performs the logical negation. If *Expression1* is TRUE, the operator returns FALSE. If *Expression1* is FALSE, the operator returns TRUE. The following table illustrates how the logical conjunction is performed.  
  
|If Expression1 is|Return value is|  
|-----------------------|---------------------|  
|TRUE|FALSE|  
|FALSE|TRUE|  
  
## See Also  
 [Data Mining Extensions &#40;DMX&#41; Operator Reference](../dmx/data-mining-extensions-dmx-operator-reference.md)   
 [Logical Operators &#40;DMX&#41;](../dmx/operators-logical.md)   
 [Operators &#40;DMX&#41;](../dmx/operators-dmx.md)  
  
  
