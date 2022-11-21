---
description: "Numeric Functions"
title: "Numeric Functions | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords: 
  - "functions [ODBC], numeric functions"
  - "numeric functions [ODBC]"
ms.assetid: 4fa548dc-e8b0-4179-92ff-81d6a79d10c3
author: David-Engel
ms.author: v-davidengel
---
# Numeric Functions
The following table describes numeric functions that are included in the ODBC scalar function set. By calling **SQLGetInfo** with an *information type* of SQL_NUMERIC_FUNCTIONS, an application can determine which numeric functions are supported by a driver.  
  
 All numeric functions return values of data type SQL_FLOAT except for ABS, ROUND, TRUNCATE, SIGN, FLOOR, and CEILING, which return values of the same data type as the input parameters.  
  
 Arguments denoted as *numeric_exp* can be the name of a column, the result of another scalar function, or a *numeric-litera*l, where the underlying data type could be represented as SQL_NUMERIC, SQL_DECIMAL, SQL_TINYINT, SQL_SMALLINT, SQL_INTEGER, SQL_BIGINT, SQL_FLOAT, SQL_REAL, or SQL_DOUBLE.  
  
 Arguments denoted as *float_exp* can be the name of a column, the result of another scalar function, or a *numeric-literal*, where the underlying data type can be represented as SQL_FLOAT.  
  
 Arguments denoted as *integer_exp* can be the name of a column, the result of another scalar function, or a *numeric-literal*, where the underlying data type can be represented as SQL_TINYINT, SQL_SMALLINT, SQL_INTEGER, or SQL_BIGINT.  
  
 The CURRENT_DATE, CURRENT_TIME, and CURRENT_TIMESTAMP scalar functions have been added in ODBC 3.0 to align with SQL-92.  
  
|Function|Description|  
|--------------|-----------------|  
|**ABS(** _numeric_exp_ **)**  (ODBC 1.0)|Returns the absolute value of *numeric_exp*.|  
|**ACOS(** _float_exp_ **)**  (ODBC 1.0)|Returns the arccosine of *float_exp* as an angle, expressed in radians.|  
|**ASIN(** _float_exp_ **)**  (ODBC 1.0)|Returns the arcsine of *float_exp* as an angle, expressed in radians.|  
|**ATAN(** _float_exp_ **)**  (ODBC 1.0)|Returns the arctangent of *float_exp* as an angle, expressed in radians.|  
|**ATAN2(** _float_exp1_, _float_exp2_**)**  (ODBC 2.0)|Returns the arctangent of the *x* and *y* coordinates, specified by *float_exp1* and *float_exp2*, respectively, as an angle, expressed in radians.|  
|**CEILING(** _numeric_exp_ **)**  (ODBC 1.0)|Returns the smallest integer greater than or equal to *numeric_exp*. The return value is of the same data type as the input parameter.|  
|**COS(** _float_exp_ **)**  (ODBC 1.0)|Returns the cosine of *float_exp*, where *float_exp* is an angle expressed in radians.|  
|**COT(** _float_exp_ **)**  (ODBC 1.0)|Returns the cotangent of *float_exp*, where *float_exp* is an angle expressed in radians.|  
|**DEGREES(** _numeric_exp_ **)**  (ODBC 2.0)|Returns the number of degrees converted from *numeric_exp* radians.|  
|**EXP(** _float_exp_ **)**  (ODBC 1.0)|Returns the exponential value of *float_exp*.|  
|**FLOOR(** _numeric_exp_ **)**  (ODBC 1.0)|Returns the largest integer less than or equal to *numeric_exp*. The return value is of the same data type as the input parameter.|  
|**LOG(** _float_exp_ **)**  (ODBC 1.0)|Returns the natural logarithm of *float_exp*.|  
|**LOG10(** _float_exp_ **)**  (ODBC 2.0)|Returns the base 10 logarithm of *float_exp*.|  
|**MOD(** _integer_exp1_, _integer_exp2_**)**  (ODBC 1.0)|Returns the remainder (modulus) of *integer_exp1* divided by *integer_exp2*.|  
|**PI( )**  (ODBC 1.0)|Returns the constant value of pi as a floating-point value.|  
|**POWER(** _numeric_exp_, _integer_exp_**)**  (ODBC 2.0)|Returns the value of *numeric_exp* to the power of *integer_exp*.|  
|**RADIANS(** _numeric_exp_ **)**  (ODBC 2.0)|Returns the number of radians converted from *numeric_exp* degrees.|  
|**RAND(**[*integer_exp*]**)**  (ODBC 1.0)|Returns a random floating-point value using *integer_exp* as the optional seed value.|  
|**ROUND(** _numeric_exp_, _integer_exp_**)**  (ODBC 2.0)|Returns *numeric_exp* rounded to *integer_exp* places right of the decimal point. If *integer_exp* is negative, *numeric_exp* is rounded to &#124;*integer_exp*&#124; places to the left of the decimal point.|  
|**SIGN(** _numeric_exp_ **)**  (ODBC 1.0)|Returns an indicator of the sign of *numeric_exp*. If *numeric_exp* is less than zero, -1 is returned. If *numeric_exp* equals zero, 0 is returned. If *numeric_exp* is greater than zero, 1 is returned.|  
|**SIN(** _float_exp_ **)**  (ODBC 1.0)|Returns the sine of *float_exp*, where *float_exp* is an angle expressed in radians.|  
|**SQRT(** _float_exp_ **)**  (ODBC 1.0)|Returns the square root of *float_exp*.|  
|**TAN(** _float_exp_ **)**  (ODBC 1.0)|Returns the tangent of *float_exp*, where *float_exp* is an angle expressed in radians.|  
|**TRUNCATE(** _numeric_exp_, _integer_exp_**)**  (ODBC 2.0)|Returns *numeric_exp* truncated to *integer_exp* places right of the decimal point. If *integer_exp* is negative, *numeric_exp* is truncated to &#124;*integer_exp*&#124; places to the left of the decimal point.|
