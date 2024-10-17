---
title: "Type Identifiers"
description: "Type Identifiers"
author: David-Engel
ms.author: davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords:
  - "data types [ODBC], identifiers"
  - "type identifiers [ODBC]"
  - "identifiers [ODBC], type"
  - "type identifiers [ODBC], about type identifiers"
---
# Type Identifiers
To describe SQL and C data types, ODBC defines two sets of *type identifiers*. A type identifier describes the type of a SQL column or a C buffer. It is a **#define** value and is generally passed as a function argument or returned in metadata.  
  
 For example, the following call to **SQLBindParameter** binds a variable of type SQL_DATE_STRUCT to a date parameter in a SQL statement. The C type identifier SQL_C_TYPE_DATE specifies the type of the *Date* variable, and the SQL type identifier SQL_TYPE_DATE specifies the type of the dynamic parameter.  
  
```  
SQL_DATE_STRUCT Date;  
SQLINTEGER  DateInd = 0;  
SQLBindParameter(hstmt, 1, SQL_PARAM_INPUT, SQL_C_TYPE_DATE, SQL_TYPE_DATE, 0, 0,  
                  &Date, 0, &DateInd);  
```
