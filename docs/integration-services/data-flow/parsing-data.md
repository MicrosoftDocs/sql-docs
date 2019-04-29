---
title: "Parsing Data | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "parsing [Integration Services]"
  - "data parsing [Integration Services]"
ms.assetid: 8893ea9d-634c-4309-b52c-6337222dcb39
author: janinezhang
ms.author: janinez
manager: craigg
---
# Parsing Data
  Data flows in packages extract and load data between heterogeneous data stores, which may use a variety of standard and custom data types. In a data flow, [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] sources do the work of extracting data, parsing string data, and converting data to an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] data type. Subsequent transformations may parse data to convert it to a different data type, or create column copies with different data types. Expressions used in components may also cast arguments and operands to different data types. Finally, when the data is loaded into a data store, the destination may parse the data to convert it to a data type that the destination uses. For more information, see [Integration Services Data Types](../../integration-services/data-flow/integration-services-data-types.md).  
  
## Two types of parsing  
 [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] provides two types of parsing for converting data: Fast parse and Standard parse.  
  
-   Fast parse is a fast, simple set of parsing routines that does not support locale-specific data type conversions, and supports only the most frequently used date and time formats. 
  
-   Standard parse is a rich set of parsing routines that supports all the data type conversions that are provided by the Automation data type conversion APIs available in Oleaut32.dll and Ole2dsip.dll.   
  
## Fast Parse
Fast parse provides a fast, simple set of routines for parsing data. These routines are not locale-sensitive and they support only a subset of date, time, and integer formats.  
  
### Requirements and limitations  
 By implementing fast parse, a package forfeits its ability to interpret date, time, and numeric data in locale-specific formats and many frequently used ISO 8601 basic and extended formats, but the package enhances its performance. For example, fast parse supports only the most commonly used date format representations such as YYYYMMDD and YYYY-MM-DD, does not perform locale-specific parsing, does not recognize special characters in currency data, and cannot convert hexadecimal or scientific representation of integers.  
  
 Fast parse is available only when you use the Flat File source or the Data Conversion transformation. The increase in performance can be significant, and you should consider using fast parse in these data flow components if you can.  
  
 If the data flow in the package requires locale-sensitive parsing, standard parse is recommended instead of fast parse. For example, fast parse does not recognize locale-sensitive data that includes decimal symbols such as the comma, date formats other than year-month-date formats, and currency symbols.  
  
 Truncated representations that imply one or more date parts, such as a century, a year, or a month, are not recognized by fast parse. For example, fast parse recognizes neither the '**-YYMM**' format, which specifies a year and month in an implied century, nor '**--MM**', which specifies a month in an implied year. However, some representations with reduced precision are recognized. For example, fast parse recognizes the 'hhmm;' format, which indicates hour and minute only, and '**YYYY**', which indicates year only.  
  
 Fast parse is specified at the column level. In the Flat File source and the Data Conversion transformation, you can specify Fast parse on output columns. Inputs and outputs can include both locale-sensitive and locale-insensitive columns.  
 
## Numeric data formats (Fast Parse)
Fast parse provides a fast, simple, locale-insensitive set of routines for parsing data. Fast parse supports only a limited set of formats for integer data types.  
  
### Integer data type
 The integer data types that [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] provides are DT_I1, DT_UI1, DT_I2, DT_UI2, DT_I4, DT_UI4, DT_I8, and DT_UI8. For more information, see [Integration Services Data Types](../../integration-services/data-flow/integration-services-data-types.md).  
  
 Fast parse supports the following formats for integer data types:  
  
-   Zero or more leading and trailing spaces or tab stops. For example, the value "  123  " is valid. A value that is all spaces evaluates to zero.  
  
-   A leading plus sign, minus sign, or neither. For example, the values +123, -123, and 123 are valid.  
  
-   One or more Hindu-Arabic numerals (0-9). For example, the value 345 is valid. Other language numerals are not supported.  
  
 Non-supported data formats include the following:  
  
-   Special characters. For example, the currency character $ is not supported, and the value $20 cannot be parsed.  
  
-   White-space characters such as line feed, carriage returns, and non-breaking spaces. For example, the value " 123" cannot be parsed.  
  
-   Hexadecimal representations of integers. For example, the value 2EE cannot be parsed.  
  
-   Scientific notation representation of integers. For example, the value 1E+10 cannot be parsed.  
  
 The following formats are output data formats for integers:  
  
-   A minus sign for negative numbers and nothing for positive ones.  
  
-   No white spaces.  
  
-   One or more Hindu-Arabic numerals (0-9).  

## Date and time formats (Fast Parse)
Fast parse provides a fast, simple set of routines for parsing data. Fast parse supports the following formats for date and time data types.  
  
### Date data type 
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
  
 For more information, see [Integration Services Data Types](../../integration-services/data-flow/integration-services-data-types.md).  
  
### Time data type
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
  
     The formats for all time and date/time data can include a time zone element. However, the system ignores the time zone value except when the data is of type DT_DBTIMESTAMPOFFSET. For more information, see [Integration Services Data Types](../../integration-services/data-flow/integration-services-data-types.md).  
  
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
  
 For more information, see [Integration Services Data Types](../../integration-services/data-flow/integration-services-data-types.md).  
  
### Date/Time data type  
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
  
 For more information, see [Integration Services Data Types](../../integration-services/data-flow/integration-services-data-types.md).  
  
## Enable Fast Parse
The fast parse property must be set for each column of the source or transformation that uses fast parse. To set the property, use the Advanced editor of the Flat File source and Data Conversion transformation.  
  
1.  Right-click the Flat File source or Data Conversion transformation, and then click **Show Advanced Editor**.  
  
2.  In the **Advanced Editor** dialog box, click the **Input and Output Properties** tab.  
  
3.  In the **Inputs and Outputs** pane, click the column for which you want to enable fast parse.  
  
4.  In the Properties window, expand the **Custom Properties** node, and then set the **FastParse** property to **True**.  
  
5.  Click **OK**.  

## Standard Parse
Standard parse is a locale-sensitive set of parsing routines that support all the data type conversions provided by the Automation data type conversion APIs that are available in Oleaut32.dll and Ole2dsip.dll. Standard parse is equivalent to the OLE DB parsing APIs.  
  
 Standard parse provides support for data type conversion of international data, and it should be used if the data format is not supported by Fast parse. For more information about the Automation data type conversion API, see "Data Type Conversion APIs" in the [MSDN Library](https://go.microsoft.com/fwlink/?LinkId=79427). 
 
