---
title: "|| (Logical OR) (SSIS Expression)"
description: "|| (Logical OR) (SSIS Expression)"
ms.date: "03/01/2017"
ms.service: sql
ms.subservice: integration-services
ms.topic: concept-article
helpviewer_keywords:
  - "OR operator"
  - "logical OR (||)"
  - "|| (logical OR)"
---
# || (Logical OR) (SSIS Expression)

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]


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
  
## Related content

- [&#124; (Bitwise Inclusive OR) (SSIS Expression)](bitwise-inclusive-or-ssis-expression.md)
- [^ (Bitwise Exclusive OR) (SSIS Expression)](bitwise-exclusive-or-ssis-expression.md)
- [Operator Precedence and Associativity](operator-precedence-and-associativity.md)
- [Operators (SSIS Expression)](operators-ssis-expression.md)
