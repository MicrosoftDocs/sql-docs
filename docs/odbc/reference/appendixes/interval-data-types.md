---
description: "Interval Data Types"
title: "Interval Data Types | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords: 
  - "second intervals [ODBC]"
  - "data types [ODBC], interval data types"
  - "interval data type [ODBC]"
  - "day-time intervals [ODBC]"
  - "intervals [ODBC], about intervals"
  - "minute intervals [ODBC]"
  - "day intervals [ODBC]"
  - "year intervals [ODBC]"
  - "month intervals [ODBC]"
  - "interval data type [ODBC], about interval data types"
  - "SQL data types [ODBC], interval"
  - "year-month intervals [ODBC]"
  - "C data types [ODBC], interval"
  - "interval fields [ODBC]"
ms.assetid: fba93f65-c1db-44f4-91ba-532f87241cf7
author: David-Engel
ms.author: v-davidengel
---
# Interval Data Types
An interval is defined as the difference between two dates and times. Intervals are expressed in one of two different ways. One is a *year-month* interval that expresses intervals in terms of years and an integral number of months. The other is a *day-time* interval that expresses intervals in terms of days, minutes, and seconds. These two types of intervals are distinct and cannot be mixed, because months can have varying numbers of days.  
  
 An interval consists of a set of fields. There is an implied ordering among the fields. For example, in a year-to-month interval, the year comes first, followed by the month. Similarly, in a day-to-minute interval, the fields are in the order day, hour, and minute. The first field in an interval type is called the *leading* field, or the *high-order* field. The last field is called the *trailing* field.  
  
 In all intervals, the leading field is not constrained by rules of the Gregorian calendar. For example, in an hour-to-minute interval, the hour field is not constrained to be between 0 and 23 (inclusive), as it normally is. The trailing fields subsequent to the leading field follow the usual constraints of the Gregorian calendar. For more information, see [Constraints of the Gregorian Calendar](../../../odbc/reference/appendixes/constraints-of-the-gregorian-calendar.md), later in this appendix.  
  
 There are 13 interval SQL data types and 13 interval C data types. Each of the interval C data types uses the same structure, SQL_INTERVAL_STRUCT, to contain the interval data. (For more information, see the next section, [C Interval Structure](../../../odbc/reference/appendixes/c-interval-structure.md).) For more information on the SQL data types, see [SQL Data Types](../../../odbc/reference/appendixes/sql-data-types.md); for more information on the C data types, see [C Data Types](../../../odbc/reference/appendixes/c-data-types.md).  
  
|Type identifier|Class|Description|  
|---------------------|-----------|-----------------|  
|MONTH|Year-Month|Number of months between two dates.|  
|YEAR|Year-Month|Number of years between two dates.|  
|YEAR_TO_MONTH|Year-Month|Number of years and months between two dates.|  
|DAY|Day-Time|Number of days between two dates.|  
|HOUR|Day-Time|Number of hours between two date/times.|  
|MINUTE|Day-Time|Number of minutes between two date/times.|  
|SECOND|Day-Time|Number of seconds between two date/times.|  
|DAY_TO_HOUR|Day-Time|Number of days/hours between two date/times.|  
|DAY_TO_MINUTE|Day-Time|Number of days/hours/minutes between two date/times.|  
|DAY_TO_SECOND|Day-Time|Number of days/hours/minutes/seconds between two date/times.|  
|HOUR_TO_MINUTE|Day-Time|Number of hours/minutes between two date/times.|  
|HOUR_TO_SECOND|Day-Time|Number of hours/minutes/seconds between two date/times.|  
|MINUTE_TO_SECOND|Day-Time|Number of minutes/seconds between two date/times.|  
  
 This section contains the following topics.  
  
-   [C Interval Structure](../../../odbc/reference/appendixes/c-interval-structure.md)  
  
-   [Interval Data Type Precision](../../../odbc/reference/appendixes/interval-data-type-precision.md)  
  
-   [Interval Data Type Length](../../../odbc/reference/appendixes/interval-data-type-length.md)  
  
-   [Interval Literals](../../../odbc/reference/appendixes/interval-literals.md)  
  
-   [Overriding Default Leading and Seconds Precision for Interval Data Types](../../../odbc/reference/appendixes/overriding-default-leading-and-seconds-precision-for-interval-data-types.md)
