---
title: "Setting Parameter Values | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "parameter values [ODBC]"
ms.assetid: 13e5da79-b60c-48d0-b467-773f481ef2a4
author: MightyPen
ms.author: genemi
manager: craigg
---
# Setting Parameter Values
To set the value of a parameter, the application simply sets the value of the variable bound to the parameter. It is not important when this value is set, as long as it is set before the statement is executed. The application can set the value before or after binding the variable, and it can change the value as many times as it wants. When the statement is executed, the driver simply retrieves the current value of the variable. This is particularly useful when a prepared statement is executed more than once; the application sets new values for some or all of the variables each time the statement is executed. For an example of this, see [Prepared Execution](../../../odbc/reference/develop-app/prepared-execution-odbc.md), earlier in this section.  
  
 If a length/indicator buffer was bound in the call to **SQLBindParameter**, it must be set to one of the following values before the statement is executed:  
  
-   The byte length of the data in the bound variable. The driver checks this length only if the variable is character or binary (*ValueType* is SQL_C_CHAR or SQL_C_BINARY).  
  
-   SQL_NTS. The data is a null-terminated string.  
  
-   SQL_NULL_DATA. The data value is NULL, and the driver ignores the value of the bound variable.  
  
-   SQL_DATA_AT_EXEC or the result of the SQL_LEN_DATA_AT_EXEC macro. The value of the parameter is to be sent with **SQLPutData**. For more information, see [Sending Long Data](../../../odbc/reference/develop-app/sending-long-data.md), later in this section.  
  
 The following table shows the values of the bound variable and the length/indicator buffer that the application sets for a variety of parameter values.  
  
|Parameter<br /><br /> value|Parameter<br /><br /> (SQL)<br /><br /> data type|Variable (C)<br /><br /> data type|Value in<br /><br /> bound<br /><br /> variable|Value in<br /><br /> length/indicator<br /><br /> buffer[d]|  
|-------------------------|-----------------------------------------|----------------------------------|-------------------------------------|----------------------------------------------------|  
|"ABC"|SQL_CHAR|SQL_C_CHAR|ABC\0[a]|SQL_NTS or 3|  
|10|SQL_INTEGER|SQL_C_SLONG|10|--|  
|10|SQL_INTEGER|SQL_C_CHAR|10\0[a]|SQL_NTS or 2|  
|1 P.M.|SQL_TYPE_TIME|SQL_C_TYPE_TIME|13,0,0[b]|--|  
|1 P.M.|SQL_TYPE_TIME|SQL_C_CHAR|{t '13:00:00'}\0[a], [c]|SQL_NTS or 14|  
|NULL|SQL_SMALLINT|SQL_C_SSHORT|--|SQL_NULL_DATA|  
  
 [a]   "\0" represents a null-termination character. The null-termination character is required only if the value in the length/indicator buffer is SQL_NTS.  
  
 [b]   The numbers in this list are the numbers stored in the fields of the TIME_STRUCT structure.  
  
 [c]   The string uses the ODBC date escape clause. For more information, see [Date, Time, and Timestamp Literals](../../../odbc/reference/develop-app/date-time-and-timestamp-literals.md).  
  
 [d]   Drivers must always check this value to see whether it is a special value, such as SQL_NULL_DATA.  
  
 What a driver does with a parameter value at execution time is driver-dependent. If necessary, the driver converts the value from the C data type and byte length of the bound variable to the SQL data type, precision, and scale of the parameter. In most cases, the driver then sends the value to the data source. In some cases, it formats the value as text and inserts it into the SQL statement before sending the statement to the data source.
