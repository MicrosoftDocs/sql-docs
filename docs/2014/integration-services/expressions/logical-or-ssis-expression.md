---
title: "|| (Logical OR) (SSIS Expression) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "OR operator"
  - "logical OR (||)"
  - "|| (logical OR)"
ms.assetid: a3c07c09-f121-4187-9617-b01adcf843c4
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# || (Logical OR) (SSIS Expression)
  Performs a logical OR operation. The expression evaluates to TRUE if one or both conditions are TRUE.  
  
## Syntax  
  
```  
  
boolean_expression1 || boolean_expression2  
```  
  
## Arguments  
 *boolean_expression1, boolean_expression2*  
 Is any valid expression that evaluates to TRUE, FALSE, or NULL.  
  
## Result Types  
 DT_BOOL  
  
## Remarks  
 The following table shows the result of the || operator.  
  
|Result|Expression|Expression|  
|------------|----------------|----------------|  
|TRUE|TRUE|TRUE|  
|TRUE|TRUE|FALSE|  
|FALSE|FALSE|FALSE|  
|NULL|NULL|NULL|  
|TRUE|NULL|TRUE|  
|NULL|NULL|FALSE|  
  
## SSIS Expression Examples  
 This example uses the **StandardCost** and **ListPrice** columns. The example evaluates to TRUE if the value of the **StandardCost** column is less than 300 or the **ListPrice** column is greater than 500.  
  
```  
StandardCost < 300 || ListPrice > 500  
```  
  
 This example uses the variables **SPrice** and **LPrice** instead of numeric literals.  
  
```  
StandardCost < @SPrice || ListPrice > @LPrice  
```  
  
## See Also  
 [&#124; &#40;Bitwise Inclusive OR&#41; &#40;SSIS Expression&#41;](bitwise-inclusive-or-ssis-expression.md)   
 [^ &#40;Bitwise Exclusive OR&#41; &#40;SSIS Expression&#41;](bitwise-exclusive-or-ssis-expression.md)   
 [Operator Precedence and Associativity](operator-precedence-and-associativity.md)   
 [Operators &#40;SSIS Expression&#41;](operators-ssis-expression.md)  
  
  
