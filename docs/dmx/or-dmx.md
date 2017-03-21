---
title: "OR (DMX) | Microsoft Docs"
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
  - "OR"
dev_langs: 
  - "DMX"
helpviewer_keywords: 
  - "OR operator"
ms.assetid: 727a38a9-7f75-4963-8e8a-aa703c129d30
caps.latest.revision: 12
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# OR (DMX)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  A logical operator that performs a logical disjunction on two numeric expressions.  
  
## Syntax  
  
```  
  
Expression1 OR Expression2  
```  
  
#### Parameters  
 *Expression1*  
 A valid Data Mining Extensions (DMX) expression that returns a numeric value.  
  
 *Expression2*  
 A valid DMX expression that returns a numeric value.  
  
## Return Value  
 A Boolean value that returns TRUE if either argument or both arguments evaluate to TRUE; otherwise FALSE.  
  
## Remarks  
 Both arguments are treated as Boolean values (0 as FALSE; otherwise TRUE) before the operator performs the logical disjunction. If either argument or both arguments evaluate to TRUE, the operator returns TRUE. If *Expression1* evaluates to TRUE and *Expression2* evaluates to FALSE, the operator returns TRUE.  
  
 The following table illustrates how the logical disjunction is performed.  
  
|If Expression1 is|If Expression2 is|Return value is|  
|-----------------------|-----------------------|---------------------|  
|TRUE|TRUE|TRUE|  
|TRUE|FALSE|TRUE|  
|FALSE|TRUE|TRUE|  
|FALSE|FALSE|FALSE|  
  
## See Also  
 [Data Mining Extensions &#40;DMX&#41; Operator Reference](../dmx/data-mining-extensions-dmx-operator-reference.md)   
 [Logical Operators &#40;DMX&#41;](../dmx/operators-logical.md)   
 [Operators &#40;DMX&#41;](../dmx/operators-dmx.md)  
  
  
