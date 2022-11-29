---
description: "Functions (SSIS Expression)"
title: "Functions (SSIS Expression) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "functions [Integration Services]"
  - "expressions [Integration Services], functions"
  - "string functions"
  - "SQL Server Integration Services, functions"
  - "SSIS, functions"
ms.assetid: e9a41a31-94f4-46a4-b737-c707dd59ce48
author: chugugrace
ms.author: chugu
---
# Functions (SSIS Expression)

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]


  The expression language includes a set of functions for use in expressions. An expression can use a single function, but typically an expression combines functions with operators and uses multiple functions.  
  
 The functions can be categorized into the following groups:  
  
-   Mathematical functions that perform calculations based on numeric input values provided as parameters to the functions and return numeric values.  
  
-   String functions that perform operations on string or hexadecimal input values and return a string or numeric value.  
  
-   Date and time functions that perform operations on date and time values and return string, numeric, or date and time values.  
  
-   System functions that return information about an expression.  
  
 The expression language provides the following mathematical functions.  
  
|Function|Description|  
|--------------|-----------------|  
|[ABS &#40;SSIS Expression&#41;](../../integration-services/expressions/abs-ssis-expression.md)|Returns the absolute, positive value of a numeric expression.|  
|[EXP &#40;SSIS Expression&#41;](../../integration-services/expressions/exp-ssis-expression.md)|Returns the exponent to base e of the specified expression.|  
|[CEILING &#40;SSIS Expression&#41;](../../integration-services/expressions/ceiling-ssis-expression.md)|Returns the smallest integer that is greater than or equal to a numeric expression.|  
|[FLOOR &#40;SSIS Expression&#41;](../../integration-services/expressions/floor-ssis-expression.md)|Returns the largest integer that is less than or equal to a numeric expression.|  
|[LN &#40;SSIS Expression&#41;](../../integration-services/expressions/ln-ssis-expression.md)|Returns the natural logarithm of a numeric expression.|  
|[LOG &#40;SSIS Expression&#41;](../../integration-services/expressions/log-ssis-expression.md)|Returns the base-10 logarithm of a numeric expression.|  
|[POWER &#40;SSIS Expression&#41;](../../integration-services/expressions/power-ssis-expression.md)|Returns the result of raising a numeric expression to a power.|  
|[ROUND &#40;SSIS Expression&#41;](../../integration-services/expressions/round-ssis-expression.md)|Returns a numeric expression that is rounded to the specified length or precision. .|  
|[SIGN &#40;SSIS Expression&#41;](../../integration-services/expressions/sign-ssis-expression.md)|Returns the positive (+), negative (-), or zero (0) sign of a numeric expression.|  
|[SQUARE &#40;SSIS Expression&#41;](../../integration-services/expressions/square-ssis-expression.md)|Returns the square of a numeric expression.|  
|[SQRT &#40;SSIS Expression&#41;](../../integration-services/expressions/sqrt-ssis-expression.md)|Returns the square root of a numeric expression.|  
  
 The expression evaluator provides the following string functions.  
  
|Function|Description|  
|--------------|-----------------|  
|[CODEPOINT &#40;SSIS Expression&#41;](../../integration-services/expressions/codepoint-ssis-expression.md)|Returns the Unicode code value of the leftmost character of a character expression.|  
|[FINDSTRING &#40;SSIS Expression&#41;](../../integration-services/expressions/findstring-ssis-expression.md)|Returns the one-based index of the specified occurrence of a character string within an expression.|  
|[HEX &#40;SSIS Expression&#41;](../../integration-services/expressions/hex-ssis-expression.md)|Returns a string representing the hexadecimal value of an integer.|  
|[LEN &#40;SSIS Expression&#41;](../../integration-services/expressions/len-ssis-expression.md)|Returns the number of characters in a character expression.|  
|[LEFT &#40;SSIS Expression&#41;](../../integration-services/expressions/left-ssis-expression.md)|Returns the specified number of characters from the leftmost portion of the given character expression.|  
|[LOWER &#40;SSIS Expression&#41;](../../integration-services/expressions/lower-ssis-expression.md)|Returns a character expression after converting uppercase characters to lowercase characters.|  
|[LTRIM &#40;SSIS Expression&#41;](../../integration-services/expressions/ltrim-ssis-expression.md)|Returns a character expression after removing leading spaces.|  
|[REPLACE &#40;SSIS Expression&#41;](../../integration-services/expressions/replace-ssis-expression.md)|Returns a character expression after replacing a string within the expression with either a different string or an empty string.|  
|[REPLICATE &#40;SSIS Expression&#41;](../../integration-services/expressions/replicate-ssis-expression.md)|Returns a character expression, replicated a specified number of times.|  
|[REVERSE &#40;SSIS Expression&#41;](../../integration-services/expressions/reverse-ssis-expression.md)|Returns a character expression in reverse order.|  
|[RIGHT &#40;SSIS Expression&#41;](../../integration-services/expressions/right-ssis-expression.md)|Returns the specified number of characters from the rightmost portion of the given character expression.|  
|[RTRIM &#40;SSIS Expression&#41;](../../integration-services/expressions/rtrim-ssis-expression.md)|Returns a character expression after removing trailing spaces.|  
|[SUBSTRING &#40;SSIS Expression&#41;](../../integration-services/expressions/substring-ssis-expression.md)|Returns a part of a character expression.|  
|[TRIM &#40;SSIS Expression&#41;](../../integration-services/expressions/trim-ssis-expression.md)|Returns a character expression after removing leading and trailing spaces.|  
|[UPPER &#40;SSIS Expression&#41;](../../integration-services/expressions/upper-ssis-expression.md)|Returns a character expression after converting lowercase characters to uppercase characters.|  
  
 The expression evaluator provides the following date and time functions.  
  
|Function|Description|  
|--------------|-----------------|  
|[DATEADD &#40;SSIS Expression&#41;](../../integration-services/expressions/dateadd-ssis-expression.md)|Returns a new DT_DBTIMESTAMP value by adding a date or time interval to a specified date.|  
|[DATEDIFF &#40;SSIS Expression&#41;](../../integration-services/expressions/datediff-ssis-expression.md)|Returns the number of date and time boundaries crossed between two specified dates.|  
|[DATEPART &#40;SSIS Expression&#41;](../../integration-services/expressions/datepart-ssis-expression.md)|Returns an integer representing a datepart of a date.|  
|[DAY &#40;SSIS Expression&#41;](../../integration-services/expressions/day-ssis-expression.md)|Returns an integer that represents the day of the specified date.|  
|[GETDATE &#40;SSIS Expression&#41;](../../integration-services/expressions/getdate-ssis-expression.md)|Returns the current date of the system.|  
|[GETUTCDATE &#40;SSIS Expression&#41;](../../integration-services/expressions/getutcdate-ssis-expression.md)|Returns the current date of the system in UTC time (Universal Time Coordinate or Greenwich Mean Time).|  
|[MONTH &#40;SSIS Expression&#41;](../../integration-services/expressions/month-ssis-expression.md)|Returns an integer that represents the month of the specified date.|  
|[YEAR &#40;SSIS Expression&#41;](../../integration-services/expressions/year-ssis-expression.md)|Returns an integer that represents the year of the specified date.|  
  
 The expression evaluator provides the following null functions.  
  
|Function|Description|  
|--------------|-----------------|  
|[ISNULL &#40;SSIS Expression&#41;](../../integration-services/expressions/isnull-ssis-expression.md)|Returns a Boolean result based on whether an expression is null.|  
|[NULL &#40;SSIS Expression&#41;](../../integration-services/expressions/null-ssis-expression.md)|Returns a null value of a requested data type.|  
  
 Expression names are shown in uppercase characters, but expression names are not case-sensitive. For example, using "null" works as well as using "NULL".  
  
## See Also  
 [Operators &#40;SSIS Expression&#41;](../../integration-services/expressions/operators-ssis-expression.md)   
 [Examples of Advanced Integration Services Expressions](../../integration-services/expressions/examples-of-advanced-integration-services-expressions.md)   
 [Integration Services &#40;SSIS&#41; Expressions](../../integration-services/expressions/integration-services-ssis-expressions.md)  
  
  
