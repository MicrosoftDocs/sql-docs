---
title: "Data Type Support for ODBC Date and Time Improvements | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "date/time [ODBC], data type support"
  - "ODBC, date/time improvements"
ms.assetid: 8e0d9ba2-3ec1-4680-86e3-b2590ba8e2e9
author: MightyPen
ms.author: genemi
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Data Type Support for ODBC Date and Time Improvements
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
[!INCLUDE[SNAC_Deprecated](../../includes/snac-deprecated.md)]

  This topic provides information about ODBC types that support [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] date and time data types.  
  
## Data Type Mapping in Parameters and Resultsets  
 In addition to the ODBC data types (SQL_TYPE_TIMESTAMP and SQL_TIMESTAMP), two new data types were added in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC to expose the new server types:  
  
-   SQL_SS_TIME2  
  
-   SQL_SS_TIMESTAMPOFFSET  
  
 The following table shows the complete server-type mapping. Notice that some cells of the table contain two entries; in these cases, the first is the ODBC 3.0 value and the second is the ODBC 2.0 value.  
  
|SQL Server data type|SQL data type|Value|  
|--------------------------|-------------------|-----------|  
|Datetime|SQL_TYPE_TIMESTAMP<br /><br /> SQL_TIMESTAMP|93 (sql.h)<br /><br /> 11 (sqlext.h)|  
|Smalldatetime|SQL_TYPE_TIMESTAMP<br /><br /> SQL_TIMESTAMP|93 (sql.h)<br /><br /> 11 (sqlext.h)|  
|Date|SQL_TYPE_DATE<br /><br /> SQL_DATE|91 (sql.h)<br /><br /> 9 (sqlext.h)|  
|Time|SQL_SS_TIME2|-154 (SQLNCLI.h)|  
|DatetimeOFFSET|SQL_SS_TIMESTAMPOFFSET|-155 (SQLNCLI.h)|  
|Datetime2|SQL_TYPE_TIMESTAMP<br /><br /> SQL_TIMESTAMP|93 (sql.h)<br /><br /> 11 (sqlext.h)|  
  
 The following table lists the corresponding structures and ODBC C types. Because ODBC does not permit driver defined C types, SQL_C_BINARY is used for time and datetimeoffset as binary structures.  
  
|SQL data type|Memory layout|Default C data type|Value (sqlext.h)|  
|-------------------|-------------------|-------------------------|------------------------|  
|SQL_TYPE_TIMESTAMP<br /><br /> SQL_TIMESTAMP|SQL_TIMESTAMP_STRUCT<br /><br /> TIMESTAMP_STRUCT|SQL_C_TYPE_TIMESTAMP<br /><br /> SQL_C_TIMESTAMP|SQL_TYPE_TIMESTAMP<br /><br /> SQL_TIMESTAMP|  
|SQL_TYPE_DATE<br /><br /> SQL_DATE|SQL_DATE_STRUCT<br /><br /> DATE_STRUCT|SQL_C_TYPE_DATE<br /><br /> SQL_C_DATE|SQL_TYPE_DATE<br /><br /> SQL_DATE|  
|SQL_SS_TIME2|SQL_SS_TIME2_STRUCT|SQL_C_SS_TIME2<br /><br /> SQL_C_BINARY (ODBC 3.5 and earlier)|0x4000 (sqlncli.h)<br /><br /> SQL_BINARY (-2)|  
|SQL_SS_TIMESTAMPOFFSET|SQL_SS_TIMESTAMPOFFSET_STRUCT|SQL_C_SS_TIMESTAMPOFFSET<br /><br /> SQL_C_BINARY (ODBC 3.5 and earlier)|0x4001 (sqlncli.h)<br /><br /> SQL_BINARY (-2)|  
  
 When SQL_C_BINARY binding is specified, alignment checking will be performed and an error reported for incorrect alignment. The SQLSTATE for this error will be IM016, with the message "Incorrect structure alignment".  
  
## Data Formats: Strings and Literals  
 The following table shows the mappings between [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data types, ODBC data types, and the ODBC string literals.  
  
|SQL Server data type|ODBC data type|String format for client conversions|  
|--------------------------|--------------------|------------------------------------------|  
|Datetime|SQL_TYPE_TIMESTAMP<br /><br /> SQL_TIMESTAMP|'yyyy-mm-dd hh:mm:ss[.999]'<br /><br /> [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] supports up to three fractional second digits for Datetime.|  
|Smalldatetime|SQL_TYPE_TIMESTAMP<br /><br /> SQL_TIMESTAMP|'yyyy-mm-dd hh:hh:ss'<br /><br /> This data type has an accuracy of one minute. The seconds component will be zero on output and will be rounded by the server on input.|  
|Date|SQL_TYPE_DATE<br /><br /> SQL_DATE|'yyyy-mm-dd'|  
|Time|SQL_SS_TIME2|'hh:mm:ss[.9999999]'<br /><br /> Fractional seconds can optionally be specified using up to seven digits.|  
|Datetime2|SQL_TYPE_TIMESTAMP<br /><br /> SQL_TIMESTAMP|'yyyy-mm-dd hh:mm:ss[.9999999]'<br /><br /> Fractional seconds can optionally be specified using up to seven digits.|  
|DatetimeOFFSET|SQL_SS_TIMESTAMPOFFSET|'yyyy-mm-dd hh:mm:ss[.9999999] +/- hh:mm'<br /><br /> Fractional seconds can optionally be specified using up to seven digits.|  
  
 There are no changes to the ODBC escape sequences for date/time literals.  
  
 Fractional seconds in results always use a dot (.), rather than a colon (:).  
  
 String values returned to applications are always the same length for a given column. Year, month, day, hour, minute, and second components are padded with leading zeros to their maximum width, and there is one space between date and time in datetime values. There is also one space between the time and timezone offset in a datetimeoffset value. A timezone offset is always preceded by a sign; when the offset is zero, this sign is a plus (+). Fractional seconds are padded with trailing zeros if necessary, up to the defined precision for the column. For datetime columns, there are three fractional seconds digits. For smalldatetime columns, there are no fractional seconds' digits, and the seconds will always be zero.  
  
 An empty string is not a valid date/time literal and it does not represent a NULL value. An attempt to convert an empty string to a date/time value will result in SQLState 22018 error and the message "Invalid character value for cast specification".  
  
 Conversions from string parameters will expect strings in the same format, with the exceptions that the sign of a timezone with zero hours and zero minutes can be either plus or minus, and trailing zeros are allowed for fractional seconds up to a maximum of 9 digits. A time component can terminate with a decimal point and no fractional seconds digits.  
  
 Currently, the driver allows additional white space around punctuation characters and the space between time and timezone offset is optional. However, this might change in a future release; applications should not rely on the current behavior.  
  
## Data Formats: Data Structures  
 In the structures described below, ODBC specifies the following constraints, which are taken from the Gregorian calendar:  
  
-   Month range is 1 through 12.  
  
-   Day field range is 1 through the number of days in the month, and must be consistent with year and month fields, taking account of leap years.  
  
-   Hour range is 0 through 23.  
  
-   Minute range is 0 through 59.  
  
-   Seconds range is 0 through 61.9(n). This allows up to two leap seconds to maintain synchronization with sideral time.  
  
     Note that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] does not allow leap seconds, so second values greater than 59 will cause a server error.  
  
 Implementations for the following existing ODBC structs have been modified to support the new [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] date and time data types. The definitions, however, have not changed.  
  
-   DATE_STRUCT  
  
-   TIME_STRUCT  
  
-   TIMESTAMP_STRUCT  
  
 There are also two new structs:  
  
-   SQL_SS_TIME2_STRUCT  
  
-   SQL_SS_TIMESTAMPOFFSET_STRUCT  
  
### SQL_SS_TIME2_STRUCT  
 This struct is padded to 12 bytes on both 32-bit and 64-bit operating systems.  
  
```  
typedef struct tagSS_TIME2_STRUCT {  
   SQLUSMALLINT hour;  
   SQLUSMALLINT minute;  
   SQLUSMALLINT second;  
   SQLUINTEGER fraction;  
} SQL_SS_TIME2_STRUCT;  
```  
  
### SQL_SS_TIMESTAMPOFFSET_STRUCT  
  
```  
typedef struct tagSS_TIMESTAMPOFFSET_STRUCT {  
   SQLSMALLINT year;  
   SQLUSMALLINT month;  
   SQLUSMALLINT day;  
   SQLUSMALLINT hour;  
   SQLUSMALLINT minute;  
   SQLUSMALLINT second;  
   SQLUINTEGER fraction;  
   SQLSMALLINT timezone_hour;  
   SQLSMALLINT timezone_minute;  
} SQL_SS_TIMESTAMPOFFSET_STRUCT;  
```  
  
 If the **timezone_hour** is negative, the **timezone_minute** must be negative or zero. If the **timezone_hour** is positive, the **timezone_minute** must be positive or zero. If the **timezone_hour** is zero, the **timezone_minute** may have any value in the range -59 through +59.  
  
## See Also  
 [Date and Time Improvements &#40;ODBC&#41;](../../relational-databases/native-client-odbc-date-time/date-and-time-improvements-odbc.md)  
  
  
