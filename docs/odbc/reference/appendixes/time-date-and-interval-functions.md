---
description: "Time, Date, and Interval Functions"
title: "Time, Date, and Interval Functions | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords: 
  - "functions [ODBC], time functions"
  - "functions [ODBC], date functions"
  - "interval functions [ODBC]"
  - "functions [ODBC], interval functions"
  - "time functions [ODBC]"
  - "date functions [ODBC]"
ms.assetid: bdf054a0-7aba-4e99-a34a-799917376fd5
author: David-Engel
ms.author: v-davidengel
---
# Time, Date, and Interval Functions
The following table lists time and date functions that are included in the ODBC scalar function set. An application can determine which time and date functions are supported by a driver by calling **SQLGetInfo** with an *information type* of SQL_TIMEDATE_FUNCTIONS.  
  
 Arguments denoted as *timestamp_exp* can be the name of a column, the result of another scalar function, or an *ODBC-time-escape*, *ODBC-date- escape*, or *ODBC-timestamp-escape*, where the underlying data type could be represented as SQL_CHAR, SQL_VARCHAR, SQL_TYPE_TIME, SQL_TYPE_DATE, or SQL_TYPE_TIMESTAMP.  
  
 Arguments denoted as *date_exp* can be the name of a column, the result of another scalar function, or an *ODBC-date- escape* or *ODBC-timestamp-escape*, where the underlying data type could be represented as SQL_CHAR, SQL_VARCHAR, SQL_TYPE_DATE, or SQL_TYPE_TIMESTAMP.  
  
 Arguments denoted as *time_exp* can be the name of a column, the result of another scalar function, or an *ODBC-time-escape* or *ODBC-timestamp-escape*, where the underlying data type could be represented as SQL_CHAR, SQL_VARCHAR, SQL_TYPE_TIME, or SQL_TYPE_TIMESTAMP.  
  
 The CURRENT_DATE, CURRENT_TIME, and CURRENT_TIMESTAMP timedate scalar functions have been added in ODBC 3.0 to align with SQL-92.  
  
|Function|Description|  
|--------------|-----------------|  
|**CURRENT_DATE( )** (ODBC 3.0)|Returns the current date.|  
|**CURRENT_TIME[(** *time-precision* **)]** (ODBC 3.0)|Returns the current local time. The *time-precision* argument determines the seconds precision of the returned value.|  
|**CURRENT_TIMESTAMP**<br /> **[(** *timestamp-precision* **)]** (ODBC 3.0)|Returns the current local date and local time as a timestamp value. The *timestamp-precision* argument determines the seconds precision of the returned timestamp.|  
|**CURDATE( )** (ODBC 1.0)|Returns the current date.|  
|**CURTIME( )** (ODBC 1.0)|Returns the current local time.|  
|**DAYNAME(** *date_exp* **)** (ODBC 2.0)|Returns a character string containing the data source-specific name of the day (for example, Sunday through Saturday or Sun. through Sat. for a data source that uses English, or Sonntag through Samstag for a data source that uses German) for the day portion of *date_exp*.|  
|**DAYOFMONTH(** *date_exp* **)** (ODBC 1.0)|Returns the day of the month based on the month field in *date_exp* as an integer value in the range of 1-31.|  
|**DAYOFWEEK(** *date_exp* **)** (ODBC 1.0)|Returns the day of the week based on the week field in *date_exp* as an integer value in the range of 1-7, where 1 represents Sunday.|  
|**DAYOFYEAR(** *date_exp* **)** (ODBC 1.0)|Returns the day of the year based on the year field in *date_exp* as an integer value in the range of 1-366.|  
|**EXTRACT(** *extract-field FROM* *extract-source* **)** (ODBC 3.0)|Returns the *extract-field* portion of the *extract-source*. The *extract-source* argument is a datetime or interval expression. The *extract-field* argument can be one of the following keywords:<br /><br /> YEAR MONTH DAY HOUR MINUTE SECOND<br /><br /> The precision of the returned value is implementation-defined. The scale is 0 unless SECOND is specified, in which case the scale is not less than the fractional seconds precision of the *extract-source* field.|  
|**HOUR(** *time_exp* **)** (ODBC 1.0)|Returns the hour based on the hour field in *time_exp* as an integer value in the range of 0-23.|  
|**MINUTE(** *time_exp* **)** (ODBC 1.0)|Returns the minute based on the minute field in *time_exp* as an integer value in the range of 0-59.|  
|**MONTH(** *date_exp* **)** (ODBC 1.0)|Returns the month based on the month field in *date_exp* as an integer value in the range of 1-12.|  
|**MONTHNAME(** *date_exp* **)** (ODBC 2.0)|Returns a character string containing the data source-specific name of the month (for example, January through December or Jan. through Dec. for a data source that uses English, or Januar through Dezember for a data source that uses German) for the month portion of *date_exp*.|  
|**NOW( )** (ODBC 1.0)|Returns current date and time as a timestamp value.|  
|**QUARTER(** *date_exp* **)** (ODBC 1.0)|Returns the quarter in *date_exp* as an integer value in the range of 1-4, where 1 represents January 1 through March 31.|  
|**SECOND(** *time_exp* **)** (ODBC 1.0)|Returns the second based on the second field in *time_exp* as an integer value in the range of 0-59.|
|**TIMESTAMPADD(** *interval*, *integer_exp*, *timestamp_exp* **)** (ODBC 2.0)|Returns the timestamp calculated by adding *integer_exp* intervals of type *interval* to *timestamp_exp*. Valid values of *interval* are the following keywords:<br /><br /> SQL_TSI_FRAC_SECOND<br /><br /> SQL_TSI_SECOND<br /><br /> SQL_TSI_MINUTE<br /><br /> SQL_TSI_HOUR<br /><br /> SQL_TSI_DAY<br /><br /> SQL_TSI_WEEK<br /><br /> SQL_TSI_MONTH<br /><br /> SQL_TSI_QUARTER<br /><br /> SQL_TSI_YEAR<br /><br /> where fractional seconds are expressed in billionths of a second. For example, the following SQL statement returns the name of each employee and his or her one-year anniversary date:<br /><br /> `SELECT NAME, {fn  TIMESTAMPADD(SQL_TSI_YEAR, 1, HIRE_DATE)} FROM  EMPLOYEES`<br /><br /> If *timestamp_exp* is a time value and *interval* specifies days, weeks, months, quarters, or years, the date portion of *timestamp_exp* is set to the current date before calculating the resulting timestamp.<br /><br /> If *timestamp_exp* is a date value and *interval* specifies fractional seconds, seconds, minutes, or hours, the time portion of *timestamp_exp* is set to 0 before calculating the resulting timestamp.<br /><br /> An application determines which intervals a data source supports by calling **SQLGetInfo** with the SQL_TIMEDATE_ADD_INTERVALS option.|  
|**TIMESTAMPDIFF(** *interval*, *timestamp_exp1*, *timestamp_exp2* **)** (ODBC 2.0)|Returns the integer number of intervals of type *interval* by which *timestamp_exp2* is greater than *timestamp_exp1*. Valid values of *interval* are the following keywords:<br /><br /> SQL_TSI_FRAC_SECOND<br /><br /> SQL_TSI_SECOND<br /><br /> SQL_TSI_MINUTE<br /><br /> SQL_TSI_HOUR<br /><br /> SQL_TSI_DAY<br /><br /> SQL_TSI_WEEK<br /><br /> SQL_TSI_MONTH<br /><br /> SQL_TSI_QUARTER<br /><br /> SQL_TSI_YEAR<br /><br /> where fractional seconds are expressed in billionths of a second. For example, the following SQL statement returns the name of each employee and the number of years he or she has been employed:<br /><br /> `SELECT NAME, {fn  TIMESTAMPDIFF(SQL_TSI_YEAR, {fn CURDATE()}, HIRE_DATE)} FROM EMPLOYEES`<br /><br /> If either timestamp expression is a time value and *interval* specifies days, weeks, months, quarters, or years, the date portion of that timestamp is set to the current date before calculating the difference between the timestamps.<br /><br /> If either timestamp expression is a date value and *interval* specifies fractional seconds, seconds, minutes, or hours, the time portion of that timestamp is set to 0 before calculating the difference between the timestamps.<br /><br /> An application determines which intervals a data source supports by calling **SQLGetInfo** with the SQL_TIMEDATE_DIFF_INTERVALS option.|  
|**WEEK(** *date_exp* **)** (ODBC 1.0)|Returns the week of the year based on the week field in *date_exp* as an integer value in the range of 1-53.|  
|**YEAR(** *date_exp* **)** (ODBC 1.0)|Returns the year based on the year field in *date_exp* as an integer value. The range is data source-dependent.|
