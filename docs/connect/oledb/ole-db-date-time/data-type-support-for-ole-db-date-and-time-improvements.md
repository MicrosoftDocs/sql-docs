---
title: "Data type support for date and time improvements (OLE DB driver)"
description: Learn about OLE DB types that support SQL Server date/time data types in OLE DB Driver for SQL Server.
author: David-Engel
ms.author: v-davidengel
ms.date: "06/14/2018"
ms.service: sql
ms.subservice: connectivity
ms.topic: "reference"
helpviewer_keywords:
  - "date/time [OLE DB], data type support"
  - "OLE DB, date/time improvements"
---
# Data Type Support for OLE DB Date and Time Improvements
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  This article provides information about OLE DB (OLE DB Driver for SQL Server) types that support [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] date/time data types.  
  
## Data Type Mapping in Rowsets and Parameters  
 OLE DB provides two new data types to support the new server types: DBTYPE_DBTIME2 and DBTYPE_DBTIMESTAMPOFFSET. The following table shows the complete server type mapping:  
  
|[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data type|OLE DB data type|Value|  
|-----------------------------------------|----------------------|-----------|  
|datetime|DBTYPE_DBTIMESTAMP|135 (oledb.h)|  
|smalldatetime|DBTYPE_DBTIMESTAMP|135 (oledb.h)|  
|date|DBTYPE_DBDATE|133 (oledb.h)|  
|time|DBTYPE_DBTIME2|145 (msoledbsql.h)|  
|datetimeoffset|DBTYPE_DBTIMESTAMPOFFSET|146 (msoledbsql.h)|  
|datetime2|DBTYPE_DBTIMESTAMP|135 (oledb.h)|  
  
## Data Formats: Strings and Literals  
  
|[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data type|OLE DB data type|String format for client conversions|  
|-----------------------------------------|----------------------|------------------------------------------|  
|datetime|DBTYPE_DBTIMESTAMP|'yyyy-mm-dd hh:mm:ss[.999]'<br /><br /> [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] supports up to three fractional second digits for Datetime.|  
|smalldatetime|DBTYPE_DBTIMESTAMP|'yyyy-mm-dd hh:mm:ss'<br /><br /> This data type has accuracy of one minute. The seconds component will be zero on output and will be rounded by the server on input.|  
|date|DBTYPE_DBDATE|'yyyy-mm-dd'|  
|time|DBTYPE_DBTIME2|'hh:mm:ss[.9999999]'<br /><br /> Fractional seconds can optionally be specified using up to seven digits.|  
|datetime2|DBTYPE_DBTIMESTAMP|'yyyy-mm-dd hh:mm:ss[.fffffff]'<br /><br /> Fractional seconds can optionally be specified using up to seven digits.|  
|datetimeoffset|DBTYPE_DBTIMESTAMPOFFSET|'yyyy-mm-dd hh:mm:ss[.fffffff] +/-hh:mm'<br /><br /> Fractional seconds can optionally be specified using up to seven digits.|  
  
 There are no changes to the escape sequences for date/time literals.  
  
 Fractional seconds in results use a dot (.) rather than a colon (:).  
  
 String values returned to applications will always be the same length for a given column. Year, month, day, hour, minute, and second components will be padded with leading zeros to their maximum width. There will be exactly one space between date and time and exactly one space between the time and timezone offset. A timezone offset will always be preceded by a sign. This sign will be a plus (+) when the offset is zero. There will be no white space between the sign and the offset value. Fractional seconds will be padded with trailing zeros, if necessary, up to the defined precision for the column, but no further. For datetime columns, there will be three fractional seconds digits. For smalldatetime columns, there will be no fractional seconds digits and the seconds will always be zero.  
  
 Conversions from string values provided by the application will be more flexible and will allow component values less than maximum width. Years can be 1-4 digits. Months, days, hours, minutes, and seconds can be 1 or 2 digits. There can be arbitrary white space between date/time and time/timezone offsets. The sign of an offset with zero hours and zero minutes can be plus or minus. Trailing zeros are allowed for fractional seconds up to a maximum of 9 digits. A time component can terminate with a decimal point and no fractional seconds digits.  
  
 An empty string is not a valid date/time literal and it does not represent a NULL value. An attempt to convert an empty string to a date/time value will result in errors with SQLState 22018 and the message "Invalid character value for cast specification".  
  
## Data Formats: Data Structures  
 In the OLE DB-specific structures described below, OLE DB conforms to the following constraints. These are taken from the Gregorian calendar:  
  
-   Month range is 1 through 12.  
  
-   Day field range is 1 through the number of days in the month, and must be consistent with year and month fields, taking account of leap years.  
  
-   Hour range is 0 through 23.  
  
-   Minute range is 0 through 59.  
  
-   Seconds range from 0 through 59. This allows up to two leap seconds to maintain synchronization with sidereal time.  
  
 Implementations for the following existing OLE DB structs have been modified to support the new [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] date and time data types. The definitions, however, have not changed.  
  
-   DBTYPE_DATE (This is an automation DATE type. It is internally represented as a **double**. The whole part is the number of days since December 30, 1899 and the fractional part is the fraction of a day. This type has an accuracy of 1 second, so has an effective scale of 0.)  
  
-   DBTYPE_DBDATE  
  
-   DBTYPE_DBTIME  
  
-   DBTYPE_DBTIMESTAMP (the fraction field is defined by OLE DB as the number of billionths of a second (nanoseconds) and ranges from 0-999,999,999)  
  
-   DBTYPE_FILETIME  
  
### DBTYPE_DBTIME2  
 This struct is padded to 12 bytes on both 32-bit and 64-bit operating systems.  
  
```  
typedef struct tagDBTIME2 {  
    USHORT hour;  
    USHORT minute;  
    USHORT second;  
    ULONG fraction;  
    } DBTIME2;  
```  
  
### DBTYPE_ DBTIMESTAMPOFFSET  
  
```  
typedef struct tagDBTIMESTAMPOFFSET {  
    SHORT year;  
    USHORT month;  
    USHORT day;  
    USHORT hour;  
    USHORT minute;  
    USHORT second;  
    ULONG fraction;  
    SHORT timezone_hour;  
    SHORT timezone_minute;  
    } DBTIMESTAMPOFFSET;  
```  
  
 If `timezone_hour` is negative, `timezone_minute` must be negative or zero. If `timezone_hour` is positive, `timezone minute` must be positive or zero. If `timezone_hour` is zero, `timezone minute` can hold a value between -59 and +59.  
  
### SSVARIANT  
 This struct now includes the new structures, DBTYPE_DBTIME2 and DBTYPE_DBTIMESTAMPOFFSET, and adds fractional seconds scale for appropriate types.  
  
```  
struct SSVARIANT {  
   SSVARTYPE vt;  
   DWORD dwReserved1;  
   DWORD dwReserved2;  
   union {  
// ...  
      DBTIMESTAMP tsDateTimeVal;  
      DBDATE dDateVal;  
      struct _Time2Val {  
         DBTIME2 tTime2Val;  
         BYTE bScale;  
      } Time2Val;  
      struct _DateTimeVal {  
         DBTIMESTAMP tsDateTimeVal;  
         BYTE bScale;  
      } DateTimeVal;  
      struct _DateTimeOffsetVal {   
         DBTIMESTAMPOFFSET tsoDateTimeOffsetVal;  
         BYTE bScale;  
      } DateTimeOffsetVal;  
// ...  
   };  
};  
```  
  
 In addition, the enum associated with SSVARIANT type encoding, which determines the type of the enum, will be extended as follows:  
  
```  
enum SQLVARENUM {  
// ...  
   // Datetime  
   VT_SS_DATETIME      = DBTYPE_DBTIMESTAMP,  
   VT_SS_SMALLDATETIME = 206,  
  
   VT_SS_DATE = DBTYPE_DBDATE,  
   VT_SS_TIME2 = DBTYPE_DBTIME2,  
   VT_SS_DATETIME2 = 212  
   VT_SS_DATETIMEOFFSET = DBTYPE_DBTIMESTAMPOFFSET  
};  
```  
  
 Applications migrating to OLE DB Driver for SQL Server that use **sql_variant** and rely on the limited precision of **datetime** will have to be updated if the underlying schema is updated to use **datetime2** rather than **datetime**.  
  
 The access macros for SSVARIANT have also been extended with the addition of the following:  
  
```  
#define V_SS_DATETIME2(X)       V_SS_UNION(X, DateTimeVal)  
#define V_SS_TIME2(X)           V_SS_UNION(X, Time2Val)  
#define V_SS_DATE(X)            V_SS_UNION(X, dDateVal)  
#define V_SS_DATETIMEOFFSET(X)  V_SS_UNION(X, DateTimeOffsetVal)  
```  
  
## Data Type Mapping in ITableDefinition::CreateTable  
 The following type mapping is used with DBCOLUMNDESC structures used by ITableDefinition::CreateTable:  
  
|OLE DB data type (*wType*)|[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data type|Notes|  
|----------------------------------|-----------------------------------------|-----------|  
|DBTYPE_DBDATE|date||  
|DBTYPE_DBTIMESTAMP|**datetime2**(p)|The OLE DB Driver for SQL Server inspects the DBCOLUMDESC *bScale* member to determine the fractional seconds precision.|  
|DBTYPE_DBTIME2|**time**(p)|The OLE DB Driver for SQL Server inspects the DBCOLUMDESC *bScale* member to determine the fractional seconds precision.|  
|DBTYPE_DBTIMESTAMPOFFSET|**datetimeoffset**(p)|The OLE DB Driver for SQL Server inspects the DBCOLUMDESC *bScale* member to determine the fractional seconds precision.|  
  
 When an application specifies DBTYPE_DBTIMESTAMP in *wType*, it can override the mapping to **datetime2** by supplying a type name in *pwszTypeName*. If **datetime** is specified, *bScale* must be 3. If **smalldatetime** is specified, *bScale* must be 0. If *bScale* is not consistent with *wType* and *pwszTypeName*, DB_E_BADSCALE is returned.  
  
## See Also  
 [Date and Time Improvements &#40;OLE DB&#41;](../../oledb/ole-db-date-time/date-and-time-improvements-ole-db.md)  
  
  
