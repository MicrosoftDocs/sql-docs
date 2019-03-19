---
title: "Scalar Function Calls | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "escape sequences [ODBC], scalar function calls"
ms.assetid: 10cb4dcf-4cd8-4a56-8725-d080bd3ffe47
author: MightyPen
ms.author: genemi
manager: craigg
---
# Scalar Function Calls
Scalar functions return a value for each row. For example, the absolute value scalar function takes a numeric column as an argument and returns the absolute value of each value in the column. The escape sequence for calling a scalar function is  
  
 **{fn**  _scalar-function_ **}**  
  
 where *scalar-function* is one of the functions listed in [Appendix E: Scalar Functions](../../../odbc/reference/appendixes/appendix-e-scalar-functions.md). For more information about the scalar function escape sequence, see [Scalar Function Escape Sequence](../../../odbc/reference/appendixes/scalar-function-escape-sequence.md) in Appendix C: SQL Grammar.  
  
 For example, the following SQL statements create the same result set of uppercase customer names. The first statement uses the escape-sequence syntax. The second statement uses the native syntax for Ingres for OS/2 and is not interoperable.  
  
```  
SELECT {fn UCASE(Name)} FROM Customers  
  
SELECT uppercase(Name) FROM Customers  
```  
  
 An application can mix calls to scalar functions that use native syntax and calls to scalar functions that use ODBC syntax. For example, assume that names in the Employee table are stored as a last name, a comma, and a first name. The following SQL statement creates a result set of last names of employees in the Employee table. The statement uses the ODBC scalar function **SUBSTRING** and the SQL Server scalar function **CHARINDEX** and will execute correctly only on SQL Server.  
  
```  
SELECT {fn SUBSTRING(Name, 1, CHARINDEX(',', Name) - 1)} FROM Customers  
```  
  
 For maximum interoperability, applications should use the **CONVERT** scalar function to make sure that the output of a scalar function is the required type. The **CONVERT** function converts data from one SQL data type to the specified SQL data type. The syntax of the **CONVERT** function is  
  
 **CONVERT(** _value_exp_ **,** _data_type_**)**  
  
 where *value_exp* is a column name, the result of another scalar function, or a literal value, and *data_type* is a keyword that matches the **#define** name that is used by an SQL data type identifier as defined in [Appendix D: Data Types](../../../odbc/reference/appendixes/appendix-d-data-types.md). For example, the following SQL statement uses the **CONVERT** function to make sure that the output of the **CURDATE** function is a date, instead of a timestamp or character data:  
  
```  
INSERT INTO Orders (OrderID, CustID, OpenDate, SalesPerson, Status)  
   VALUES (?, ?, {fn CONVERT({fn CURDATE()}, SQL_DATE)}, ?, ?)  
```  
  
 To determine which scalar functions are supported by a data source, an application calls **SQLGetInfo** with the SQL_CONVERT_FUNCTIONS, SQL_NUMERIC_FUNCTIONS, SQL_STRING_FUNCTIONS, SQL_SYSTEM_FUNCTIONS, and SQL_TIMEDATE_FUNCTIONS options. To determine which conversion operations are supported by the **CONVERT** function, an application calls **SQLGetInfo** with any of the options that start with SQL_CONVERT.
