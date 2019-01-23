---
title: "C Data Types | Microsoft Docs"
ms.custom: ""
ms.date: "07/12/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "data types [ODBC], C data types"
  - "C data types [ODBC], about C data types"
  - "C data types [ODBC]"
  - "C buffers [ODBC]"
ms.assetid: b681d260-3dbb-47df-a616-4910d727add7
author: MightyPen
ms.author: genemi
manager: craigg
---
# C Data Types
ODBC C data types indicate the data type of C buffers used to store data in the application.  
  
 All drivers must support all C data types. This is required because all drivers must support all C types to which SQL types that they support can be converted, and all drivers support at least one character SQL type. Because the character SQL type can be converted to and from all C types, all drivers must support all C types.  
  
 The C data type is specified in the **SQLBindCol** and **SQLGetData** functions with the *TargetType* argument and in the **SQLBindParameter** function with the *ValueType* argument. It can also be specified by calling **SQLSetDescField** to set the SQL_DESC_CONCISE_TYPE field of an ARD or APD, or by calling **SQLSetDescRec** with the *Type* argument (and the *SubType* argument if needed) and the *DescriptorHandle* argument set to the handle of an ARD or APD.  
  
 The following tables lists valid type identifiers for the C data types. The table also lists the ODBC C data type that corresponds to each identifier and the definition of this data type.  
  
|C type identifier|ODBC C typedef|C type|  
|-----------------------|--------------------|------------|  
|SQL_C_CHAR|SQLCHAR *|unsigned char *|  
|SQL_C_WCHAR|SQLWCHAR *|wchar_t *|  
|SQL_C_SSHORT[j]|SQLSMALLINT|short int|  
|SQL_C_USHORT[j]|SQLUSMALLINT|unsigned short int|  
|SQL_C_SLONG[j]|SQLINTEGER|long int|  
|SQL_C_ULONG[j]|SQLUINTEGER|unsigned long int|  
|SQL_C_FLOAT|SQLREAL|float|  
|SQL_C_DOUBLE|SQLDOUBLE, SQLFLOAT|double|  
|SQL_C_BIT|SQLCHAR|unsigned char|  
|SQL_C_STINYINT[j]|SQLSCHAR|signed char|  
|SQL_C_UTINYINT[j]|SQLCHAR|unsigned char|  
|SQL_C_SBIGINT|SQLBIGINT|_int64[h]|  
|SQL_C_UBIGINT|SQLUBIGINT|unsigned _int64[h]|  
|SQL_C_BINARY|SQLCHAR *|unsigned char *|  
|SQL_C_BOOKMARK[i]|BOOKMARK|unsigned long int[d]|  
|SQL_C_VARBOOKMARK|SQLCHAR *|unsigned char *|  
|All C interval data types|SQL_INTERVAL_STRUCT|See the [C Interval Structure](../../../odbc/reference/appendixes/c-interval-structure.md) section, later in this appendix.|  
  
 **C type identifier** SQL_C_TYPE_DATE[c]  
  
 **ODBC C typedef** SQL_DATE_STRUCT  
  
 **C type**  
  
```  
struct tagDATE_STRUCT {  
   SQLSMALLINT year;  
   SQLUSMALLINT month;  
   SQLUSMALLINT day;    
} DATE_STRUCT;[a]  
```  
  
 **C type identifier** SQL_C_TYPE_TIME[c]  
  
 **ODBC C typedef** SQL_TIME_STRUCT  
  
 **C type**  
  
```  
struct tagTIME_STRUCT {  
   SQLUSMALLINT hour;  
   SQLUSMALLINT minute;  
   SQLUSMALLINT second;  
} TIME_STRUCT;[a]  
```  
  
 **C type identifier** SQL_C_TYPE_TIMESTAMP[c]  
  
 **ODBC C typedef** SQL_TIMESTAMP_STRUCT  
  
 **C type**  
  
```  
struct tagTIMESTAMP_STRUCT {  
   SQLSMALLINT year;  
   SQLUSMALLINT month;  
   SQLUSMALLINT day;  
   SQLUSMALLINT hour;  
   SQLUSMALLINT minute;  
   SQLUSMALLINT second;  
   SQLUINTEGER fraction;[b]   
} TIMESTAMP_STRUCT;[a]  
```  
  
 **C type identifier** SQL_C_NUMERIC  
  
 **ODBC C typedef** SQL_NUMERIC_STRUCT  
  
 **C type**  
  
```  
struct tagSQL_NUMERIC_STRUCT {  
   SQLCHAR precision;  
   SQLSCHAR scale;  
   SQLCHAR sign[g];  
   SQLCHAR val[SQL_MAX_NUMERIC_LEN];[e], [f]   
} SQL_NUMERIC_STRUCT;  
```  
  
 **C type identifier** SQL_C_GUID  
  
 **ODBC C typedef** SQLGUID  
  
 **C type**  
  
```  
struct tagSQLGUID {  
   DWORD Data1;  
   WORD Data2;  
   WORD Data3;  
   BYTE Data4[8];  
} SQLGUID;[k]  
```  
  
 [a]   The values of the year, month, day, hour, minute, and second fields in the datetime C data types must conform to the constraints of the Gregorian calendar. (See [Constraints of the Gregorian Calendar](../../../odbc/reference/appendixes/constraints-of-the-gregorian-calendar.md) later in this appendix.)  
  
 [b]   The value of the fraction field is the number of billionths of a second and ranges from 0 through 999,999,999 (1 less than 1 billion). For example, the value of the fraction field for a half-second is 500,000,000, for a thousandth of a second (one millisecond) is 1,000,000, for a millionth of a second (one microsecond) is 1,000, and for a billionth of a second (one nanosecond) is 1.  
  
 [c]   In ODBC 2.*x*, the C date, time, and timestamp data types are SQL_C_DATE, SQL_C_TIME, and SQL_C_TIMESTAMP.  
  
 [d]   ODBC 3*.x* applications should use SQL_C_VARBOOKMARK, not SQL_C_BOOKMARK. When an ODBC 3*.x* application works with an ODBC 2.*x* driver, the ODBC 3*.x* Driver Manager will map SQL_C_VARBOOKMARK to SQL_C_BOOKMARK.  
  
 [e]   A number is stored in the *val* field of the SQL_NUMERIC_STRUCT structure as a scaled integer, in little endian mode (the leftmost byte being the least-significant byte). For example, the number 10.001 base 10, with a scale of 4, is scaled to an integer of 100010. Because this is 186AA in hexadecimal format, the value in SQL_NUMERIC_STRUCT would be "AA 86 01 00 00 ... 00", with the number of bytes defined by the SQL_MAX_NUMERIC_LEN **#define**.  
  
 For more information about **SQL_NUMERIC_STRUCT**, see [HOWTO: Retrieving Numeric Data with SQL_NUMERIC_STRUCT](retrieve-numeric-data-sql-numeric-struct-kb222831.md).  
  
 [f]   The precision and scale fields of the SQL_C_NUMERIC data type areused for input from an application and for output from the driver to the application. When the driver writes a numeric value into the SQL_NUMERIC_STRUCT, it will use its own driver-specific default as the value for the *precision* field, and it will use the value in the SQL_DESC_SCALE field of the application descriptor (which defaults to 0) for the *scale* field. An application can provide its own values for precision and scale by setting the SQL_DESC_PRECISION and SQL_DESC_SCALE fields of the application descriptor.  
  
 [g]   The sign field is 1 if positive, 0 if negative.  
  
 [h]   _int64 might not be supplied by some compilers.  
  
 [i]   _SQL_C_BOOKMARK has been deprecated in ODBC 3*.x*.  
  
 [j]   _SQL_C_SHORT, SQL_C_LONG, and SQL_C_TINYINT have been replaced in ODBC by signed and unsigned types: SQL_C_SSHORT and SQL_C_USHORT, SQL_C_SLONG and SQL_C_ULONG, and SQL_C_STINYINT and SQL_C_UTINYINT. An ODBC 3*.x* driver that should work with ODBC 2.*x* applications should support SQL_C_SHORT, SQL_C_LONG, and SQL_C_TINYINT, because when they are called, the Driver Manager passes them through to the driver.  
  
 [k]   SQL_C_GUID can be converted only to SQL_CHAR or SQL_WCHAR.  
  
 This section contains the following topic.  
  
-   [64-Bit Integer Structures](../../../odbc/reference/appendixes/64-bit-integer-structures.md)  
  
## See Also  
 [C Data Types in ODBC](../../../odbc/reference/develop-app/c-data-types-in-odbc.md)
