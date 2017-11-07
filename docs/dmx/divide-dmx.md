---
title: "(Divide) (DMX) | Microsoft Docs"
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
  - "/ (divide)"
  - "divide operator (/)"
ms.assetid: 7afc06cd-054b-48c3-9c3c-9a0c17d15e63
caps.latest.revision: 14
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# (Divide) (DMX)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx_md](../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Performs an arithmetic operation that divides one number by another number.  
  
## Syntax  
  
```  
  
Dividend / Divisor  
```  
  
#### Parameters  
 *Dividend*  
 A valid Data Mining Extensions (DMX) expression that returns a numeric value.  
  
 *Divisor*  
 A valid DMX expression that returns a numeric value.  
  
## Return Value  
 A value that has the data type of the parameter that has the higher precedence.  
  
## Remarks  
 The value that this operator returns represents the quotient of the first expression divided by the second expression.  
  
 Both expressions must be of the same data type, or one expression must be able to be implicitly converted to the data type of the other expression. If the divisor evaluates to a null value, the operator raises an error. If both the divisor and the dividend evaluate to a null value, the operator returns a null value.  
  
## See Also  
 [Arithmetic Operators &#40;DMX&#41;](../dmx/operators-arithmetic.md)   
 [Data Mining Extensions &#40;DMX&#41; Operator Reference](../dmx/data-mining-extensions-dmx-operator-reference.md)   
 [Operators &#40;DMX&#41;](../dmx/operators-dmx.md)   
 [Divide &#40;SSIS Expression&#41;](../integration-services/expressions/divide-ssis-expression.md)   
 [&#40;Divide&#41; &#40;Transact-SQL&#41;](../t-sql/language-elements/divide-transact-sql.md)  
  
  
