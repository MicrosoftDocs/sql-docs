---
title: "Date and Time Formats | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "time data types [Integration Services]"
  - "fast parse [Integration Services]"
  - "date data types"
  - "date and time formats for fast parse"
ms.assetid: bed6e2c1-791a-4fa1-b29f-cbfdd1fa8d39
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Date and Time Formats
  Fast parse provides a fast, simple set of routines for parsing data. Fast parse supports the following formats for date and time data types.  
  
## Date Data Types  
 Fast parse supports the following string formats for date data:  
  
-   Date formats that include leading white spaces. For example, the value " 2004- 02-03" is valid.  
  
-   ISO 8601 formats, as listed in the following table:  
  
    |Format|Description|  
    |------------|-----------------|  
    |YYYYMMDD<br /><br /> YYYY-MM-DD|Basic and extended formats for a four-digit year, a two-digit month, and a two-digit day. In the extended format, the date parts are separated by a hyphen (-).|  
    |YYYY-MM|Basic and extended reduced precision formats for a four-digit year and a two-digit month. In the extended format, the date parts are separated by a hyphen (-).|  
    |YYYY|Reduced precision format is a four-digit year.|  
  
 Fast parse does not support the following formats for date data:  
  
-   Alphabetical month values. For example, the date format Oct-31-2003 is not valid.  
  
-   Ambiguous formats such as DD-MM-YYYY and MM-DD-YYYY. For example, the dates 03-04-1995 and 04-03-1995 are not valid.  
  
-   Basic and extended truncated formats for a four-digit calendar year and a three-digit day within a year, YYYYDDD and YYYY-DDD.  
  
-   Basic and extended formats for a four-digit year, a two-digit number for the week of the year, and a one-digit number for the day of the week, YYYYWwwD and YYYY-Www-D  
  
-   Basic and extended truncated formats for a year and week date are a four-digit year and a two-digit number for the week, YYYWww and YYYY-Www  
  
 Fast parse outputs the data as DT_DBDATE. Date values in truncated formats are padded. For example, YYYY becomes YYYY0101.  
  
 For more information, see [Integration Services Data Types](data-flow/integration-services-data-types.md).  
  
## Time Data Type  
 Fast parse supports the following string formats for time data:  
  
-   Time formats that include leading white spaces. For example, the value " 10:24" is valid.  
  
-   24-hour format. Fast parse does not support the AM and PM notation.  
  
-   ISO 8601 time formats, as listed in the following table:  
  
    |Format|Description|  
    |------------|-----------------|  
    |HHMISS<br /><br /> HH:MI:SS|Basic and extended formats for a two-digit hour, a two-digit minute, and a two-digit second. In the extended format, the time parts are separated by a colon (:).|  
    |HHMI<br /><br /> HH:MI|Basic and extended truncated format for a two-digit hour and a two-digit minute. In the extended format, the time parts are separated by a colon (:).|  
    |HH|Truncated format for a two-digit hour.|  
    |00:00:00<br /><br /> 000000<br /><br /> 0000<br /><br /> 00<br /><br /> 240000<br /><br /> 24:00:00<br /><br /> 2400<br /><br /> 24|The format for midnight.|  
  
-   Time formats that specify a time zone, as listed in the following table:.  
  
    |Format|Description|  
    |------------|-----------------|  
    |+HH:MI<br /><br /> +HHMI|Basic and extended formats that indicate the number of hours and minutes that are added to Coordinated Universal Time (UTC) to obtain the local time.|  
    |-HH:MI<br /><br /> -HHMI|Basic and extended formats that indicate the number of hours and minutes that are subtracted from UTC to obtain the local time.|  
    |+HH|Truncated format that indicates the number of hours that are added to UTC to obtain the local time.|  
    |-HH|Truncated format that indicates the number of hours that are subtracted from UTC to obtain the local time.|  
    |Z|A value of 0 that indicates the time is represented in UTC.|  
  
     The formats for all time and date/time data can include a time zone element. However, the system ignores the time zone value except when the data is of type DT_DBTIMESTAMPOFFSET. For more information, see [Integration Services Data Types](data-flow/integration-services-data-types.md).  
  
     In formats that include a time zone element, there is no space between the time element and the time zone element, as shown in the following example:  
  
     HH:MI:SS[+HH:MI]  
  
     The brackets in the previous example indicate that the time zone value is optional.  
  
-   Time formats that include a decimal fraction, as listed in the following table:  
  
    |Format|Description|  
    |------------|-----------------|  
    |HH[.nnnnnnn]|n is a value between 0 and 9999999 that represents a fraction of hours. The brackets indicate that this value is optional.<br /><br /> For example, the value 12.750 indicates 12:45.|  
    |HHMI[.nnnnnnn]<br /><br /> HH:MI[.nnnnnnn]|n is a value between 0 and 9999999 that represents a fraction of minutes. The brackets indicate that this value is optional.<br /><br /> For example, the value 1220.500 indicates 12:20:30.|  
    |HHMISS[.nnnnnnn]<br /><br /> HH:MI:SS[.nnnnnnn]|n is a value between 0 and 9999999 that represents a fraction of seconds. The brackets indicate that this value is optional.<br /><br /> For example, the value 122040.250 indicates 12:20:40.15.|  
  
    > [!NOTE]  
    >  The fraction separator for the time formats in the previous table can be a decimal or a comma.  
  
-   Time values that include a leap second, as shown in the following examples:  
  
     23:59:60[.0000000]  
  
     235960[.0000000]  
  
 Fast parse outputs the strings as DT_DBTIME and DT_DBTIME2. Time values in truncated formats are padded. For example, HH:MI becomes HH:MM:00.000.  
  
 For more information, see [Integration Services Data Types](data-flow/integration-services-data-types.md).  
  
## Date/Time Data Type  
 Fast parse supports the following string formats for date/time data:  
  
-   Formats that include leading white spaces. For example, the value "  2003-01-10T203910" is valid.  
  
-   Combinations of valid date formats and valid time formats separated by an uppercase T, and valid time zone formats, such as YYYYMMDDT[HHMISS][+HH:MI]. The time and time zone values are not required. For example, "2003-10-14" is valid.  
  
 Fast parse does not support time intervals. For example, a time interval identified by a start and end date and time in the format YYYYMMDDThhmmss/YYYYMMDDThhmmss cannot be parsed.  
  
 Fast parse outputs the strings as DT_DATE, DT_DBTIMESTAMP, DT_DBTIMESTAMP2, and DT_DBTIMESTAMPOFFSET. Date/time values in truncated formats are padded. The following table lists the values that are added for missing date and time parts.  
  
|Date/Time part|Padding|  
|---------------------|-------------|  
|Seconds|Add 00.|  
|Minutes|Add 00:00.|  
|Hour|Add 00:00:00.|  
|Day|Add 01 for the day of the month.|  
|Month|Add 01 for the month of the year.|  
  
 For more information, see [Integration Services Data Types](data-flow/integration-services-data-types.md).  
  
  
