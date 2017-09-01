---
title: "Date and Time Data Types and Functions (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "09/01/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "dates [SQL Server], functions"
  - "dates [SQL Server]"
  - "date and time [SQL Server], all data types and functions"
  - "date and time [SQL Server]"
  - "functions [SQL Server], time"
  - "functions [SQL Server], date and time"
  - "time [SQL Server], functions"
ms.assetid: 83e378a2-6e89-4c80-bc4f-644958d9e0a9
caps.latest.revision: 79
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# Date and Time Data Types and Functions (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx_md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

The following sections in this topic provide an overview of all [!INCLUDE[tsql](../../includes/tsql-md.md)] date and time data types and functions.
-   [Date and Time Data Types](#DateandTimeDataTypes)  
-   [Date and Time Functions](#DateandTimeFunctions)  
    -   [Function That Get System Date and Time Values](#GetSystemDateandTimeValues)  
    -   [Functions That Get Date and Time Parts](#GetDateandTimeParts)  
    -   [Functions That Get Date and Time Values from Their Parts](#fromParts)  
    -   [Functions That Get Date and Time Difference](#GetDateandTimeDifference)  
    -   [Functions That Modify Date and Time Values](#ModifyDateandTimeValues)  
    -   [Functions That Set or Get Session Format Functions](#SetorGetSessionFormatFunctions)  
    -   [Functions That Validate Date and Time Values](#ValidateDateandTimeValues)  
-   [Date and Time–Related Topics](#DateandTimeRelatedTopics)  
  
##  <a name="DateandTimeDataTypes"></a> Date and Time data types
The [!INCLUDE[tsql](../../includes/tsql-md.md)] date and time data types are listed in the following table:
  
|Data type|Format|Range|Accuracy|Storage size (bytes)|User-defined fractional second precision|Time zone offset|  
|---|---|---|---|---|---|---|
|[time](../../t-sql/data-types/time-transact-sql.md)|hh:mm:ss[.nnnnnnn]|00:00:00.0000000 through 23:59:59.9999999|100 nanoseconds|3 to 5|Yes|No|  
|[date](../../t-sql/data-types/date-transact-sql.md)|YYYY-MM-DD|0001-01-01 through 9999-12-31|1 day|3|No|No|  
|[smalldatetime](../../t-sql/data-types/smalldatetime-transact-sql.md)|YYYY-MM-DD hh:mm:ss|1900-01-01 through 2079-06-06|1 minute|4|No|No|  
|[datetime](../../t-sql/data-types/datetime-transact-sql.md)|YYYY-MM-DD hh:mm:ss[.nnn]|1753-01-01 through 9999-12-31|0.00333 second|8|No|No|  
|[datetime2](../../t-sql/data-types/datetime2-transact-sql.md)|YYYY-MM-DD hh:mm:ss[.nnnnnnn]|0001-01-01 00:00:00.0000000 through 9999-12-31 23:59:59.9999999|100 nanoseconds|6 to 8|Yes|No|  
|[datetimeoffset](../../t-sql/data-types/datetimeoffset-transact-sql.md)|YYYY-MM-DD hh:mm:ss[.nnnnnnn] [+&#124;-]hh:mm|0001-01-01 00:00:00.0000000 through 9999-12-31 23:59:59.9999999 (in UTC)|100 nanoseconds|8 to 10|Yes|Yes|  
  
> [!NOTE]  
>  The [!INCLUDE[tsql](../../includes/tsql-md.md)] [rowversion](../../t-sql/data-types/rowversion-transact-sql.md) data type is not a date or time data type. **timestamp** is a deprecated synonym for **rowversion**.  
  
##  <a name="DateandTimeFunctions"></a> Date and Time functions  
The [!INCLUDE[tsql](../../includes/tsql-md.md)] date and time functions are listed in the following tables. For more information about determinism, see [Deterministic and Nondeterministic Functions](../../relational-databases/user-defined-functions/deterministic-and-nondeterministic-functions.md).
  
###  <a name="GetSystemDateandTimeValues"></a> Functions that get system Date and Time values 
All system date and time values are derived from the operating system of the computer on which the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is running.
  
#### Higher-Precision System Date and Time Functions  
[!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] obtains the date and time values by using the GetSystemTimeAsFileTime() Windows API. The accuracy depends on the computer hardware and version of Windows on which the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is running. The precision of this API is fixed at 100 nanoseconds. The accuracy can be determined by using the GetSystemTimeAdjustment() Windows API.
  
|Function|Syntax|Return value|Return data type|Determinism|  
|---|---|---|---|--|  
|[SYSDATETIME](../../t-sql/functions/sysdatetime-transact-sql.md)|SYSDATETIME ()|Returns a **datetime2(7)** value that contains the date and time of the computer on which the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is running. The time zone offset is not included.|**datetime2(7)**|Nondeterministic|  
|[SYSDATETIMEOFFSET](../../t-sql/functions/sysdatetimeoffset-transact-sql.md)|SYSDATETIMEOFFSET ( )|Returns a **datetimeoffset(7)** value that contains the date and time of the computer on which the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is running. The time zone offset is included.|**datetimeoffset(7)**|Nondeterministic|  
|[SYSUTCDATETIME](../../t-sql/functions/sysutcdatetime-transact-sql.md)|SYSUTCDATETIME ( )|Returns a **datetime2(7)** value that contains the date and time of the computer on which the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is running. The date and time is returned as UTC time (Coordinated Universal Time).|**datetime2(7)**|Nondeterministic|  
  
#### Lower-Precision  system Date and Time functions
  
|Function|Syntax|Return value|Return data type|Determinism|  
|---|---|---|---|---|
|[CURRENT_TIMESTAMP](../../t-sql/functions/current-timestamp-transact-sql.md)|CURRENT_TIMESTAMP|Returns a **datetime** value that contains the date and time of the computer on which the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is running. The time zone offset is not included.|**datetime**|Nondeterministic|  
|[GETDATE](../../t-sql/functions/getdate-transact-sql.md)|GETDATE ( )|Returns a **datetime** value that contains the date and time of the computer on which the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is running. The time zone offset is not included.|**datetime**|Nondeterministic|  
|[GETUTCDATE](../../t-sql/functions/getutcdate-transact-sql.md)|GETUTCDATE ( )|Returns a **datetime** value that contains the date and time of the computer on which the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is running. The date and time is returned as UTC time (Coordinated Universal Time).|**datetime**|Nondeterministic|  
  
###  <a name="GetDateandTimeParts"></a> Functions that get Date and Time parts
  
|Function|Syntax|Return value|Return data type|Determinism|  
|--------------|------------|------------------|----------------------|-----------------|  
|[DATENAME](../../t-sql/functions/datename-transact-sql.md)|DATENAME ( *datepart* , *date* )|Returns a character string that represents the specified *datepart* of the specified date.|**nvarchar**|Nondeterministic|  
|[DATEPART](../../t-sql/functions/datepart-transact-sql.md)|DATEPART ( *datepart* , *date* )|Returns an integer that represents the specified *datepart* of the specified *date*.|**int**|Nondeterministic|  
|[DAY](../../t-sql/functions/day-transact-sql.md)|DAY ( *date* )|Returns an integer that represents the day part of the specified *date*.|**int**|Deterministic|  
|[MONTH](../../t-sql/functions/month-transact-sql.md)|MONTH ( *date* )|Returns an integer that represents the month part of a specified *date*.|**int**|Deterministic|  
|[YEAR](../../t-sql/functions/year-transact-sql.md)|YEAR ( *date* )|Returns an integer that represents the year part of a specified *date*.|**int**|Deterministic|  
  
###  <a name="fromParts"></a> Functions that get Date and Time values from their parts
  
|Function|Syntax|Return value|Return data type|Determinism|  
|---|---|---|---|---|   
|[DATEFROMPARTS](../../t-sql/functions/datefromparts-transact-sql.md)|DATEFROMPARTS  ( *year*, *month*, *day* )|Returns a **date** value for the specified year, month, and day.|**date**|Deterministic|  
|[DATETIME2FROMPARTS](../../t-sql/functions/datetime2fromparts-transact-sql.md)|DATETIME2FROMPARTS  ( *year*, *month*, *day*, *hour*, *minute*, *seconds*, *fractions*, *precision*)|Returns a **datetime2** value for the specified date and time and with the specified precision.|**datetime2(** *precision* **)**|Deterministic|  
|[DATETIMEFROMPARTS](../../t-sql/functions/datetimefromparts-transact-sql.md)|DATETIMEFROMPARTS  ( *year*, *month*, *day*, *hour*, *minute*, *seconds*, *milliseconds*)|Returns a **datetime** value for the specified date and time.|**datetime**|Deterministic|  
|[DATETIMEOFFSETFROMPARTS](../../t-sql/functions/datetimeoffsetfromparts-transact-sql.md)|DATETIMEOFFSETFROMPARTS  ( *year*, *month*, *day*, *hour*, *minute*, *seconds*, *fractions*, *hour_offset*, *minute_offset*, *precision*)|Returns a **datetimeoffset** value for the specified date and time and with the specified offsets and precision.|**datetime(** *precision* **)**|Deterministic|  
|[SMALLDATETIMEFROMPARTS](../../t-sql/functions/smalldatetimefromparts-transact-sql.md)|SMALLDATETIMEFROMPARTS  ( *year*, *month*, *day*, *hour*, *minute* )|Returns a **smalldatetime** value for the specified date and time.|**smalldatetime**|Deterministic|  
|[TIMEFROMPARTS](../../t-sql/functions/timefromparts-transact-sql.md)|TIMEFROMPARTS  ( *hour*, *minute*, *seconds*, *fractions*, *precision* )|Returns a **time** value for the specified time and with the specified precision.|**time(** *precision* **)**|Deterministic|  
  
###  <a name="GetDateandTimeDifference"></a> Functions that get Date and Time difference
  
|Function|Syntax|Return value|Return data type|Determinism|  
|---|---|---|---|---|  
|[DATEDIFF](../../t-sql/functions/datediff-transact-sql.md)|DATEDIFF ( *datepart* , *startdate* , *enddate* )|Returns the number of date or time *datepart* boundaries that are crossed between two specified dates.|**int**|Deterministic|  
|[DATEDIFF_BIG](../../t-sql/functions/datediff-big-transact-sql.md)|DATEDIFF_BIG ( *datepart* , *startdate* , *enddate* )|Returns the number of date or time *datepart* boundaries that are crossed between two specified dates.|**bigint**|Deterministic|  
  
###  <a name="ModifyDateandTimeValues"></a> Functions that modify Date and Time values
  
|Function|Syntax|Return value|Return data type|Determinism|  
|---|---|---|---|---|
|[DATEADD](../../t-sql/functions/dateadd-transact-sql.md)|DATEADD (*datepart* , *number* , *date* )|Returns a new **datetime** value by adding an interval to the specified *datepart* of the specified *date*.|The data type of the *date* argument|Deterministic|  
|[EOMONTH](../../t-sql/functions/eomonth-transact-sql.md)|EOMONTH  ( *start_date* [, *month_to_add* ] )|Returns the last day of the month that contains the specified date, with an optional offset.|Return type is the type of *start_date* or **date**.|Deterministic|  
|[SWITCHOFFSET](../../t-sql/functions/switchoffset-transact-sql.md)|SWITCH*OFFSET* (*DATETIMEOFFSET* , *time_zone*)|SWITCH*OFFSET* changes the time zone offset of a DATETIMEOFFSET value and preserves the UTC value.|**datetimeoffset** with the fractional precision of the *DATETIMEOFFSET*|Deterministic|  
|[TODATETIMEOFFSET](../../t-sql/functions/todatetimeoffset-transact-sql.md)|TODATETIMEOFFSET (*expression* , *time_zone*)|TODATETIMEOFFSET transforms a datetime2 value into a datetimeoffset value. The datetime2 value is interpreted in local time for the specified time_zone.|**datetimeoffset** with the fractional precision of the *datetime* argument|Deterministic|  
  
###  <a name="SetorGetSessionFormatFunctions"></a> Functions that get or set session format
  
|Function|Syntax|Return value|Return data type|Determinism|  
|---|---|---|---|---|
|[@@DATEFIRST](../../t-sql/functions/datefirst-transact-sql.md)|@@DATEFIRST|Returns the current value, for the session, of SET DATEFIRST.|**tinyint**|Nondeterministic|  
|[SET DATEFIRST](../../t-sql/statements/set-datefirst-transact-sql.md)|SET DATEFIRST { *number* &#124; **@***number_var* }|Sets the first day of the week to a number from 1 through 7.|Not applicable|Not applicable|  
|[SET DATEFORMAT](../../t-sql/statements/set-dateformat-transact-sql.md)|SET DATEFORMAT { *format* &#124; **@***format_var* }|Sets the order of the dateparts (month/day/year) for entering **datetime** or **smalldatetime** data.|Not applicable|Not applicable|  
|[@@LANGUAGE](../../t-sql/functions/language-transact-sql.md)|@@LANGUAGE|Returns the name of the language that is currently being used. @@LANGUAGE is not a date or time function. However, the language setting can affect the output of date functions.|Not applicable|Not applicable|  
|[SET LANGUAGE](../../t-sql/statements/set-language-transact-sql.md)|SET LANGUAGE { [ N ] **'***language***'** &#124; **@***language_var* }|Sets the language environment for the session and system messages. SET LANGUAGE is not a date or time function. However, the language setting affects the output of date functions.|Not applicable|Not applicable|  
|[sp_helplanguage](../../relational-databases/system-stored-procedures/sp-helplanguage-transact-sql.md)|**sp_helplanguage** [ [ **@language =** ] **'***language***'** ]|Returns information about date formats of all supported languages. **sp_helplanguage** is not a date or time stored procedure. However, the language setting affects the output of date functions.|Not applicable|Not applicable|  
  
###  <a name="ValidateDateandTimeValues"></a> Functions that validate Date and Time values
  
|Function|Syntax|Return value|Return data type|Determinism|  
|---|---|---|---|---|
|[ISDATE](../../t-sql/functions/isdate-transact-sql.md)|ISDATE ( *expression* )|Determines whether a **datetime** or **smalldatetime** input expression is a valid date or time value.|**int**|ISDATE is deterministic only if you use it with the CONVERT function, when the CONVERT style parameter is specified, and when style is not equal to 0, 100, 9, or 109.|  
  
##  <a name="DateandTimeRelatedTopics"></a> Date and time-related topics 
  
|Topic|Description|  
|-----------|-----------------|  
|[CAST and CONVERT &#40;Transact-SQL&#41;](../../t-sql/functions/cast-and-convert-transact-sql.md)|Provides information about the conversion of date and time values to and from string literals and other date and time formats.|  
|[Write International Transact-SQL Statements](../../relational-databases/collations/write-international-transact-sql-statements.md)|Provides guidelines for portability of databases and database applications that use [!INCLUDE[tsql](../../includes/tsql-md.md)] statements from one language to another, or that support multiple languages.|  
|[ODBC Scalar Functions &#40;Transact-SQL&#41;](../../t-sql/functions/odbc-scalar-functions-transact-sql.md)|Provides information about ODBC scalar functions that can be used in [!INCLUDE[tsql](../../includes/tsql-md.md)] statements. This includes ODBC date and time functions.|  
|[AT TIME ZONE &#40;Transact-SQL&#41;](../../t-sql/queries/at-time-zone-transact-sql.md)|Provides time zone conversion.|  
  
## See also
[Functions](../../t-sql/functions/functions.md)  
[Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md)
  
  
