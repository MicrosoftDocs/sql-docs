---
title: "FLOOR (SSIS Expression)"
description: "FLOOR (SSIS Expression)"
ms.date: "03/01/2017"
ms.service: sql
ms.subservice: integration-services
ms.topic: reference
helpviewer_keywords:
  - "largest integer less than or equal to expression"
  - "FLOOR function [SSIS]"
---
# FLOOR (SSIS Expression)

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]


  Returns the largest integer that is less than or equal to a numeric expression.  
  
## Syntax  
  
```  
  
FLOOR(numeric_expression)  
```  
  
## Arguments  
 *numeric_expression*  
 Is a valid numeric expression.  
  
## Result Types  
 The numeric data type of the argument expression. The result is the integer portion of the calculated value in the same data type as *numeric_expression.*  
  
## Remarks  
 FLOOR returns a null result if the argument is null.  
  
## Expression Examples  
 These examples apply the FLOOR function to positive, negative, and zero values.  
  
```  
FLOOR(123.45)  
```  
  
 Returns 123.00  
  
```  
FLOOR(-123.45)  
```  
  
 Returns -124.00  
  
```  
FLOOR(0.00)  
```  
  
 Returns 0.00  
  
## Related content

- [CEILING (SSIS Expression)](ceiling-ssis-expression.md)
- [Functions (SSIS Expression)](functions-ssis-expression.md)
