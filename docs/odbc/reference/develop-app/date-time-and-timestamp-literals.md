---
title: "Date, Time, and Timestamp Literals | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "escape sequences [ODBC], literals"
ms.assetid: 2b42a52a-6353-494c-a179-3a7533cd729f
author: MightyPen
ms.author: genemi
manager: craigg
---
# Date, Time, and Timestamp Literals
The escape sequence for date, time, and timestamp literals is  
  
 **{**  _-type_ **'** _value_ **'}**  
  
 where *literal-type* is one of the values listed in the following table.  
  
|*literal-type*|Meaning|Format of *value*|  
|---------------------|-------------|-----------------------|  
|**d**|Date|*yyyy*-*mm*-*dd*|  
|**t**|Time*|*hh*:*mm*:*ss*[1]|  
|**ts**|Timestamp|*yyyy*-*mm*-*dd* *hh*:*mm*:*ss*[.*f...*][1]|  
  
 [1]   The number of digits to the right of the decimal point in a time or timestamp interval literal containing a seconds component is dependent on the seconds precision, as contained in the SQL_DESC_PRECISION descriptor field. (For more information, see [SQLSetDescField](../../../odbc/reference/syntax/sqlsetdescfield-function.md).)  
  
 For more information about the date, time, and timestamp escape sequences, see [Date, Time, and Timestamp Escape Sequences](../../../odbc/reference/appendixes/date-time-and-timestamp-escape-sequences.md) in Appendix C: SQL Grammar.  
  
 For example, both of the following SQL statements update the open date of sales order 1023 in the Orders table. The first statement uses the escape sequence syntax. The second statement uses the Oracle Rdb native syntax for the DATE column and is not interoperable.  
  
```  
UPDATE Orders SET OpenDate={d '1995-01-15'} WHERE OrderID=1023  
UPDATE Orders SET OpenDate='15-Jan-1995' WHERE OrderID=1023  
```  
  
 The escape sequence for a date, time, or timestamp literal also can be placed in a character variable bound to a date, time, or timestamp parameter. For example, the following code uses a date parameter bound to a character variable to update the open date of sales order 1023 in the Orders table:  
  
```  
SQLCHAR      OpenDate[56]; // The size of a date literal is 55.  
SQLINTEGER   OpenDateLenOrInd = SQL_NTS;  
  
// Bind the parameter.  
SQLBindParameter(hstmt, 1, SQL_PARAM_INPUT, SQL_C_CHAR, SQL_TYPE_DATE, 0, 0,  
                  OpenDate, sizeof(OpenDate), &OpenDateLenOrInd);  
  
// Place the date in the OpenDate variable. In addition to the escape  
// sequence shown, it would also be possible to use either of the  
// strings "{d '1995-01-15'}" and "15-Jan-1995", although the latter  
// is data source-specific.  
strcpy_s( (char*) OpenDate, _countof(OpenDate), "{d '1995-01-15'}");  
  
// Execute the statement.  
SQLExecDirect(hstmt, "UPDATE Orders SET OpenDate=? WHERE OrderID = 1023", SQL_NTS);  
```  
  
 However, it is usually more efficient to bind the parameter directly to a date structure:  
  
```  
SQL_DATE_STRUCT   OpenDate;  
SQLINTEGER        OpenDateInd = 0;  
  
// Bind the parameter.  
SQLBindParameter(hstmt, 1, SQL_PARAM_INPUT, SQL_C_TYPE_DATE, SQL_TYPE_DATE, 0, 0,  
                  &OpenDate, 0, &OpenDateLen);  
  
// Place the date in the dsOpenDate structure.  
OpenDate.year = 1995;  
OpenDate.month = 1;  
OpenDate.day = 15;  
  
// Execute the statement.  
SQLExecDirect(hstmt, "UPDATE Employee SET OpenDate=? WHERE OrderID = 1023", SQL_NTS);  
```  
  
 To determine whether a driver supports the ODBC escape sequences for date, time, or timestamp literals, an application calls **SQLGetTypeInfo**. If the data source supports a date, time, or timestamp data type, it must also support the corresponding escape sequence.  
  
 Data sources can also support the datetime literals defined in the ANSI SQL-92 specification, which are different from the ODBC escape sequences for date, time, or timestamp literals. To determine whether a data source supports the ANSI literals, an application calls **SQLGetInfo** with the SQL_ANSI_SQL_DATETIME_LITERALS option.  
  
 To determine whether a driver supports the ODBC escape sequences for interval literals, an application calls **SQLGetTypeInfo**. If the data source supports a datetime interval data type, it must also support the corresponding escape sequence.  
  
 Data sources can also support the datetime literals defined in the ANSI SQL-92 specification, which are different from the ODBC escape sequences for datetime interval literals. To determine whether a data source supports the ANSI literals, an application calls **SQLGetInfo** with the SQL_ANSI_SQL_DATETIME_LITERALS option.
