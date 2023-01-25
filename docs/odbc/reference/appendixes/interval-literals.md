---
description: "Interval Literals"
title: "Interval Literals | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords: 
  - "data types [ODBC], interval data types"
  - "interval literals [ODBC]"
  - "interval data type [ODBC], literals"
ms.assetid: f9e6c3c7-4f98-483f-89d8-ebc5680f021b
author: David-Engel
ms.author: v-davidengel
---
# Interval Literals
ODBC requires that all drivers support conversion of the SQL_CHAR or SQL_VARCHAR data type to all C interval data types. If the underlying data source does not support interval data types, however, the driver needs to know the correct format of the value in the SQL_CHAR field in order to support these conversions. Similarly, ODBC requires that any ODBC C type be convertible to SQL_CHAR or SQL_VARCHAR, so a driver needs to know what format an interval stored in the character field should have. This section describes the syntax of interval literals, which the driver writer needs to use to validate the SQL_CHAR fields during conversion either to or from C interval data types.  
  
> [!NOTE]  
>  The complete BNF syntax for interval literals is shown in the section [Interval Literal Syntax](../../../odbc/reference/appendixes/interval-literal-syntax.md) in Appendix C: SQL Grammar.  
  
 To pass interval literals as part of an SQL statement, an escape clause syntax is defined for interval literals. For more information, see [Date, Time, and Timestamp Literals](../../../odbc/reference/develop-app/date-time-and-timestamp-literals.md).  
  
 An interval literal is of the form:  
  
```  
INTERVAL[<sign>] 'value' <interval qualifier>  
```  
  
 where "INTERVAL" indicates that the character literal is an interval. The sign can be either plus or minus; it is outside the interval string and is optional.  
  
 The interval qualifier can either be a single datetime field or be composed of two datetime fields, in the form: \<*leading field*> TO \<*trailing field*>.  
  
-   When the interval is composed of a single field, the single field can be a non-second field that may be accompanied by an optional leading precision in parentheses. The single datetime field can also be a second field that may be accompanied by the optional leading precision, the optional fractional-seconds precision in parentheses, or both. If both a leading precision and a fractional-seconds precision are present for a seconds field, they are separated by commas. If the seconds field has a fractional-seconds precision, it must also have a leading precision.  
  
-   When the interval is composed of leading and trailing fields, the leading field is a non-second field that may be accompanied by the interval leading field precision in parentheses. The trailing field can be either a non-second field or a second field that may be accompanied by an interval fractional-seconds precision in parentheses.  
  
 The interval string in *value* is enclosed in single quotation marks. It can be either a year-month literal or a day-time literal. The format of the string in *value* is determined by the following rules:  
  
-   The string contains a decimal value for every field that is implied by the \<*interval* *qualifier*>.  
  
-   If the interval precision includes the fields YEAR and MONTH, the values of these fields are separated by a minus sign.  
  
-   If the interval precision includes the fields DAY and HOUR, the values of these fields are separated by a space.  
  
-   If the interval precision includes the field HOUR and the lower order fields (MINUTE and SECOND), the values of these fields are separated by a colon.  
  
-   If the interval precision includes a SECOND field and the expressed or implied seconds precision is nonzero, the character immediately before the first digit of the fractional part of the second is a period.  
  
-   No field can be more than two digits long, except:  
  
    -   The value of the leading field can be as long as the expressed or implied interval leading precision.  
  
    -   The fractional part of the SECOND field can be as long as the expressed or implied seconds precision.  
  
    -   The trailing fields follow the usual constraints of the Gregorian calendar. (See [Constraints of the Gregorian Calendar](../../../odbc/reference/appendixes/constraints-of-the-gregorian-calendar.md).)  
  
 The following table lists examples of valid interval literals as included in the ODBC escape clause for intervals. The syntax of the escape clause is as follows:  
  
> [!NOTE]  
>  *{INTERVAL sign interval-string interval-qualifier}*  
  
|Literal escape clause|Interval specified|  
|---------------------------|------------------------|  
|{INTERVAL '326' YEAR(4)}|Specifies an interval of 326 years. The interval leading precision is 4.|  
|{INTERVAL '326' MONTH(3)}|Specifies an interval of 326 months. The interval leading precision is 3.|  
|{INTERVAL '3261' DAY(4)}|Specifies an interval of 3261 days. The interval leading precision is 4.|  
|{INTERVAL '163' HOUR(3)}|Specifies an interval of 163 days. The interval leading precision is 3.|  
|{INTERVAL '163' MINUTE(3)}|Specifies an interval of 163 minutes. The interval leading precision is 3.|  
|{INTERVAL '223.16' SECOND(3,2)}|Specifies an interval of 223.16 seconds. The interval leading precision is 3, and the seconds precision is 2.|  
|{INTERVAL '163-11' YEAR(3) TO MONTH}|Specifies an interval of 163 years and 11 months. The interval leading precision is 3.|  
|{INTERVAL '163 12' DAY(3) TO HOUR}|Specifies an interval of 163 days and 12 hours. The interval leading precision is 3.|  
|{INTERVAL '163 12:39' DAY(3) TO MINUTE}|Specifies an interval of 163 days, 12 hours, and 39 minutes. The interval leading precision is 3.|  
|{INTERVAL '163 12:39:59.163' DAY(3) TO SECOND(3)}|Specifies an interval of 163 days, 12 hours, 39 minutes, and 59.163 seconds. The interval leading precision is 3, and the seconds precision is 3.|  
|{INTERVAL '163:39' HOUR(3) TO MINUTE}|Specifies an interval of 163 hours and 39 minutes. The interval leading precision is 3.|  
|{INTERVAL '163:39:59.163' HOUR(3) TO SECOND(4)}|Specifies an interval of 163 hours, 39 minutes, and 59.163 seconds. The interval leading precision is 3, and the seconds precision is 4.|  
|{INTERVAL '163:59.163' MINUTE(3) TO SECOND(5)}|Specifies an interval of 163 minutes and 59.163 seconds. The interval leading precision is 3, and the seconds precision is 5.|  
|{INTERVAL -'16 23:39:56.23' DAY TO SECOND}|Specifies an interval of minus 16 days, 23 hours, 39 minutes, and 56.23 seconds. The implied leading precision is 2, and the implied seconds precision is 6.|  
  
 The following table lists examples of invalid interval literals:  
  
|Literal escape clause|Reason why invalid|  
|---------------------------|------------------------|  
|{INTERVAL '163' HOUR(2)}|The interval leading precision is 2, but the value of the leading field is 163.|  
|{INTERVAL '223.16' SECOND(2,2)}<br /><br /> {INTERVAL '223.16' SECOND(3,1)}|In the first example, the leading precision is too small, and in the second example, the seconds precision is too small.|  
|{INTERVAL '223.16' SECOND}<br /><br /> {INTERVAL '223' YEAR}|Because the leading precision is unspecified, it defaults to 2, which is too small to hold the specified literal.|  
|{INTERVAL '22.1234567' SECOND}|The seconds precision is unspecified, so it defaults to 6. The literal has seven digits after the decimal point.|  
|{INTERVAL '163-13' YEAR(3) TO MONTH}<br /><br /> {INTERVAL '163 65' DAY(3) TO HOUR}<br /><br /> {INTERVAL '163 62:39' DAY(3) TO MINUTE}<br /><br /> {INTERVAL '163 12:125:59.163' DAY(3) TO SECOND(3)}<br /><br /> {INTERVAL '163:144' HOUR(3) TO MINUTE}<br /><br /> {INTERVAL '163:567:234.163' HOUR(3) TO SECOND(4)}<br /><br /> {INTERVAL '163:591.163' MINUTE(3) TO SECOND(5)}|The trailing field does not follow the rules of the Gregorian calendar.|
