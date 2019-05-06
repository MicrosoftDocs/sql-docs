---
title: "SIGN (SSIS Expression) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "positive values [Integration Services]"
  - "SIGN function"
  - "negative values"
ms.assetid: 1547db08-4329-4781-91c2-36898ed71b15
author: janinezhang
ms.author: janinez
manager: craigg
---
# SIGN (SSIS Expression)
  Returns the positive (+1), negative (-1), or zero (0) sign of a numeric expression.  
  
## Syntax  
  
```  
  
SIGN(numeric_expression)  
```  
  
## Arguments  
 *numeric_expression*  
 Is a valid signed numeric expression. For more information, see [Integration Services Data Types](../../integration-services/data-flow/integration-services-data-types.md).  
  
## Result Types  
 DT_I4  
  
## Remarks  
 SIGN returns a null result if the argument is null.  
  
## Expression Examples  
 This example returns the sign of a numeric literal. The return result is -1.  
  
```  
SIGN(-123.45)  
```  
  
 This example returns the sign of the result of subtracting the **StandardCost** column from the **DealerPrice** column.  
  
```  
SIGN(DealerPrice - StandardCost)  
```  
  
## See Also  
 [Functions &#40;SSIS Expression&#41;](../../integration-services/expressions/functions-ssis-expression.md)  
  
  
