---
title: "&amp;&amp; (Logical AND) (SSIS Expression) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "&& (logical AND)"
  - "AND, logical AND"
  - "logical AND (&&)"
ms.assetid: a8cb3517-d5d1-4861-9f04-905c719185ff
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# &amp;&amp; (Logical AND) (SSIS Expression)
  Performs a logical AND operation. The expression evaluates to TRUE if all conditions are TRUE.  
  
## Syntax  
  
```  
  
boolean_expression1 && boolean_expression2  
```  
  
## Arguments  
 *boolean _expression1, boolean_expression2*  
 Is any valid expression that evaluates to TRUE, FALSE, or NULL.  
  
## Result Types  
 DT_BOOL  
  
## Remarks  
 The following table shows the result of the && operator.  
  
|Result|Expression|Expression|  
|------------|----------------|----------------|  
|TRUE|TRUE|TRUE|  
|FALSE|TRUE|FALSE|  
|FALSE|FALSE|FALSE|  
|NULL|NULL|NULL|  
|NULL|NULL|TRUE|  
|FALSE|NULL|FALSE|  
  
## Expression Examples  
 This example uses the **StandardCost** and the **ListPrice** columns. The example evaluates to TRUE if the value of the **StandardCost** column is less than 300 and the **ListPrice** column is greater than 500.  
  
```  
StandardCost < 300 && ListPrice > 500  
```  
  
 This example uses the variables **SPrice** and **LPrice** instead of literals.  
  
```  
StandardCost < @SPrice && ListPrice > @LPrice  
```  
  
## See Also  
 [& &#40;Bitwise AND&#41; &#40;SSIS Expression&#41;](bitwise-and-ssis-expression.md)   
 [Operator Precedence and Associativity](operator-precedence-and-associativity.md)   
 [Operators &#40;SSIS Expression&#41;](operators-ssis-expression.md)  
  
  
