---
title: "Functions (SSIS Expression) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "integration-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "functions [Integration Services]"
  - "expressions [Integration Services], functions"
  - "string functions"
  - "SQL Server Integration Services, functions"
  - "SSIS, functions"
ms.assetid: e9a41a31-94f4-46a4-b737-c707dd59ce48
caps.latest.revision: 35
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# Functions (SSIS Expression)
  The expression language includes a set of functions for use in expressions. An expression can use a single function, but typically an expression combines functions with operators and uses multiple functions.  
  
 The functions can be categorized into the following groups:  
  
-   Mathematical functions that perform calculations based on numeric input values provided as parameters to the functions and return numeric values.  
  
-   String functions that perform operations on string or hexadecimal input values and return a string or numeric value.  
  
-   Date and time functions that perform operations on date and time values and return string, numeric, or date and time values.  
  
-   System functions that return information about an expression.  
  
 The expression language provides the following mathematical functions.  
  
|Function|Description|  
|--------------|-----------------|  
|[ABS &#40;SSIS Expression&#41;](../../2014/integration-services/abs-ssis-expression.md)|Returns the absolute, positive value of a numeric expression.|  
|[EXP &#40;SSIS Expression&#41;](../../2014/integration-services/exp-ssis-expression.md)|Returns the exponent to base e of the specified expression.|  
|[CEILING &#40;SSIS Expression&#41;](../../2014/integration-services/ceiling-ssis-expression.md)|Returns the smallest integer that is greater than or equal to a numeric expression.|  
|[FLOOR &#40;SSIS Expression&#41;](../../2014/integration-services/floor-ssis-expression.md)|Returns the largest integer that is less than or equal to a numeric expression.|  
|[LN &#40;SSIS Expression&#41;](../../2014/integration-services/ln-ssis-expression.md)|Returns the natural logarithm of a numeric expression.|  
|[LOG &#40;SSIS Expression&#41;](../../2014/integration-services/log-ssis-expression.md)|Returns the base-10 logarithm of a numeric expression.|  
|[POWER &#40;SSIS Expression&#41;](../../2014/integration-services/power-ssis-expression.md)|Returns the result of raising a numeric expression to a power.|  
|[ROUND &#40;SSIS Expression&#41;](../../2014/integration-services/round-ssis-expression.md)|Returns a numeric expression that is rounded to the specified length or precision. .|  
|[SIGN &#40;SSIS Expression&#41;](../../2014/integration-services/sign-ssis-expression.md)|Returns the positive (+), negative (-), or zero (0) sign of a numeric expression.|  
|[SQUARE &#40;SSIS Expression&#41;](../../2014/integration-services/square-ssis-expression.md)|Returns the square of a numeric expression.|  
|[SQRT &#40;SSIS Expression&#41;](../../2014/integration-services/sqrt-ssis-expression.md)|Returns the square root of a numeric expression.|  
  
 The expression evaluator provides the following string functions.  
  
|Function|Description|  
|--------------|-----------------|  
|[CODEPOINT &#40;SSIS Expression&#41;](../../2014/integration-services/codepoint-ssis-expression.md)|Returns the Unicode code value of the leftmost character of a character expression.|  
|[FINDSTRING &#40;SSIS Expression&#41;](../../2014/integration-services/findstring-ssis-expression.md)|Returns the one-based index of the specified occurrence of a character string within an expression.|  
|[HEX &#40;SSIS Expression&#41;](../../2014/integration-services/hex-ssis-expression.md)|Returns a string representing the hexadecimal value of an integer.|  
|[LEN &#40;SSIS Expression&#41;](../../2014/integration-services/len-ssis-expression.md)|Returns the number of characters in a character expression.|  
|[LEFT &#40;SSIS Expression&#41;](../../2014/integration-services/left-ssis-expression.md)|Returns the specified number of characters from the leftmost portion of the given character expression.|  
|[LOWER &#40;SSIS Expression&#41;](../../2014/integration-services/lower-ssis-expression.md)|Returns a character expression after converting uppercase characters to lowercase characters.|  
|[LTRIM &#40;SSIS Expression&#41;](../../2014/integration-services/ltrim-ssis-expression.md)|Returns a character expression after removing leading spaces.|  
|[REPLACE &#40;SSIS Expression&#41;](../../2014/integration-services/replace-ssis-expression.md)|Returns a character expression after replacing a string within the expression with either a different string or an empty string.|  
|[REPLICATE &#40;SSIS Expression&#41;](../../2014/integration-services/replicate-ssis-expression.md)|Returns a character expression, replicated a specified number of times.|  
|[REVERSE &#40;SSIS Expression&#41;](../../2014/integration-services/reverse-ssis-expression.md)|Returns a character expression in reverse order.|  
|[RIGHT &#40;SSIS Expression&#41;](../../2014/integration-services/right-ssis-expression.md)|Returns the specified number of characters from the rightmost portion of the given character expression.|  
|[RTRIM &#40;SSIS Expression&#41;](../../2014/integration-services/rtrim-ssis-expression.md)|Returns a character expression after removing trailing spaces.|  
|[SUBSTRING &#40;SSIS Expression&#41;](../../2014/integration-services/substring-ssis-expression.md)|Returns a part of a character expression.|  
|[TRIM &#40;SSIS Expression&#41;](../../2014/integration-services/trim-ssis-expression.md)|Returns a character expression after removing leading and trailing spaces.|  
|[UPPER &#40;SSIS Expression&#41;](../../2014/integration-services/upper-ssis-expression.md)|Returns a character expression after converting lowercase characters to uppercase characters.|  
  
 The expression evaluator provides the following date and time functions.  
  
|Function|Description|  
|--------------|-----------------|  
|[DATEADD &#40;SSIS Expression&#41;](../../2014/integration-services/dateadd-ssis-expression.md)|Returns a new DT_DBTIMESTAMP value by adding a date or time interval to a specified date.|  
|[DATEDIFF &#40;SSIS Expression&#41;](../../2014/integration-services/datediff-ssis-expression.md)|Returns the number of date and time boundaries crossed between two specified dates.|  
|[DATEPART &#40;SSIS Expression&#41;](../../2014/integration-services/datepart-ssis-expression.md)|Returns an integer representing a datepart of a date.|  
|[DAY &#40;SSIS Expression&#41;](../../2014/integration-services/day-ssis-expression.md)|Returns an integer that represents the day of the specified date.|  
|[GETDATE &#40;SSIS Expression&#41;](../../2014/integration-services/getdate-ssis-expression.md)|Returns the current date of the system.|  
|[GETUTCDATE &#40;SSIS Expression&#41;](../../2014/integration-services/getutcdate-ssis-expression.md)|Returns the current date of the system in UTC time (Universal Time Coordinate or Greenwich Mean Time).|  
|[MONTH &#40;SSIS Expression&#41;](../../2014/integration-services/month-ssis-expression.md)|Returns an integer that represents the month of the specified date.|  
|[YEAR &#40;SSIS Expression&#41;](../../2014/integration-services/year-ssis-expression.md)|Returns an integer that represents the year of the specified date.|  
  
 The expression evaluator provides the following null functions.  
  
|Function|Description|  
|--------------|-----------------|  
|[ISNULL &#40;SSIS Expression&#41;](../../2014/integration-services/isnull-ssis-expression.md)|Returns a Boolean result based on whether an expression is null.|  
|[NULL &#40;SSIS Expression&#41;](../../2014/integration-services/null-ssis-expression.md)|Returns a null value of a requested data type.|  
  
 Expression names are shown in uppercase characters, but expression names are not case-sensitive. For example, using "null" works as well as using "NULL".  
  
## See Also  
 [Operators &#40;SSIS Expression&#41;](../../2014/integration-services/operators-ssis-expression.md)   
 [Examples of Advanced Integration Services Expressions](../../2014/integration-services/examples-of-advanced-integration-services-expressions.md)   
 [Integration Services &#40;SSIS&#41; Expressions](../../2014/integration-services/integration-services-ssis-expressions.md)  
  
  