---
title: "Enhanced Date and Time Type Behavior with Previous SQL Server Versions (ODBC) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "date/time [ODBC], enhanced behavior with earlier SQL Server versions"
ms.assetid: cd4e137f-dc5e-4df7-bc95-51fe18c587e0
author: MightyPen
ms.author: genemi
manager: craigg
---
# Enhanced Date and Time Type Behavior with Previous SQL Server Versions (ODBC)
  This topic describes the expected behavior when a client application that uses enhanced date and time features communicates with a version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] earlier than [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)], and when a client application using Microsoft Data Access Components, Windows Data Access Components, or a version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client earlier than [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] sends commands to a server that supports enhanced date and time features.  
  
## Down-Level Client Behavior  
 Client applications that were compiled using a version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client prior to [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] see the new date/time types as nvarchar columns. The column contents are the literal representations, as described in "Data Formats: Strings and Literals" section of [Data Type Support for ODBC Date and Time Improvements](data-type-support-for-odbc-date-and-time-improvements.md). The column size is the maximum literal length for the fractional seconds precision specified for the column.  
  
 Catalog APIs will return metadata consistent with the down-level data type code returned to the client (for example, nvarchar) and the associated down-level representation (for example, the appropriate literal format). However, the data type name returned will be the real [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] type name.  
  
 Statement metadata returned by SQLDescribeCol, SQLDescribeParam, SQGetDescField, and SQLColAttribute will return metadata that is consistent with the down-level type in all respects, including the type name. An example of such a down-level type is `nvarchar`.  
  
 When a down-level client application runs against a [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] (or later) server on which schema changes to date/time types have been made, the expected behavior is as follows:  
  
|SQL Server 2005 type|[!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] (or later) Type|ODBC client type|Result conversion (SQL to C)|Parameter conversion (C to SQL)|  
|--------------------------|----------------------------------------------|----------------------|------------------------------------|---------------------------------------|  
|Datetime|Date|SQL_C_TYPE_DATE|OK|OK (1)|  
|||SQL_C_TYPE_TIMESTAMP|Time fields set to zero.|OK (2)<br /><br /> Fails if time field is non-zero. Works with [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)].|  
||Time(0)|SQL_C_TYPE_TIME|OK|OK (1)|  
|||SQL_C_TYPE_TIMESTAMP|Date fields set to current date.|OK (2)<br /><br /> Date ignored. Fails if fractional seconds are non-zero. Works with [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)].|  
||Time(7)|SQL_C_TIME|Fails - invalid time literal.|OK (1)|  
|||SQL_C_TYPE_TIMESTAMP|Fails - invalid time literal.|OK (1)|  
||Datetime2(3)|SQL_C_TYPE_TIMESTAMP|OK|OK (1)|  
||Datetime2(7)|SQL_C_TYPE_TIMESTAMP|OK|Value will be rounded to 1/300th second by client conversion.|  
|Smalldatetime|Date|SQL_C_TYPE_DATE|OK|OK|  
|||SQL_C_TYPE_TIMESTAMP|Time fields set to zero.|OK (2)<br /><br /> Fails if time field is non-zero. Works with [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)].|  
||Time(0)|SQL_C_TYPE_TIME|OK|OK|  
|||SQL_C_TYPE_TIMESTAMP|Date fields set to current date.|OK (2)<br /><br /> Date ignored. Fails if fractional seconds non-zero.<br /><br /> Works with [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)].|  
||Datetime2(0)|SQL_C_TYPE_TIMESTAMP|OK|OK|  
  
## Key to Symbols  
  
|Symbol|Meaning|  
|------------|-------------|  
|1|If it worked with [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] it should continue to work with a more recent version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
|2|An application that worked with [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] could fail with a more recent version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
  
 Note that only common schema changes have been considered. The following are common changes:  
  
-   Using a new type where logically an application requires only a date or time value. However, the application was forced to use datetime or smalldatetime due to the lack of separate date and time types.  
  
-   Using a new type to gain additional fractional seconds precision or accuracy.  
  
-   Switching to datetime2 because this is the preferred date and time datatype.  
  
### Column Metadata Returned by SQLColumns, SQLProcedureColumns, and SQLSpecialColumns  
 The following column values are returned for date/time types:  
  
|Column Type|date|time|smalldatetime|datetime|datetime2|datetimeoffset|  
|-----------------|----------|----------|-------------------|--------------|---------------|--------------------|  
|DATA_TYPE|SQL_WVARCHAR|SQL_WVARCHAR|SQL_TYPE_TIMESTAMP|SQL_TYPE_TIMESTAMP|SQL_WVARCHAR|SQL_WVARCHAR|  
|TYPE_NAME|date|time|smalldatetime|datetime|datetime2|datetimeoffset|  
|COLUMN_SIZE|10|8,10..16|16|23|19, 21..27|26, 28..34|  
|BUFFER_LENGTH|20|16, 20..32|16|16|38, 42..54|52, 56..68|  
|DECIMAL_DIGITS|NULL|NULL|0|3|NULL|NULL|  
|SQL_DATA_TYPE|SQL_WVARCHAR|SQL_WVARCHAR|SQL_DATETIME|SQL_DATETIME|SQL_WVARCHAR|SQL_WVARCHAR|  
|SQL_DATETIME_SUB|NULL|NULL|SQL_CODE_TIMESTAMP|SQL_CODE_TIMESTAMP|NULL|NULL|  
|CHAR_OCTET_LENGTH|NULL|NULL|NULL|NULL|NULL|NULL|  
|SS_DATA_TYPE|0|0|111|111|0|0|  
  
 SQLSpecialColumns does not return SQL_DATA_TYPE, SQL_DATETIME_SUB, CHAR_OCTET_LENGTH, or SS_DATA_TYPE.  
  
### Data Type Metadata Returned by SQLGetTypeInfo  
 The following column values are returned for date/time types:  
  
|Column Type|date|time|smalldatetime|datetime|datetime2|datetimeoffset|  
|-----------------|----------|----------|-------------------|--------------|---------------|--------------------|  
|TYPE_NAME|date|time|smalldatetime|datetime|datetime2|datetimeoffset|  
|DATA_TYPE|SQL_WVARCHAR|SQL_WVARCHAR|SQL_TYPE_TIMESTAMP|SQL_TYPE_TIMESTAMP|SQL_WVARCHAR|SQL_WVARCHAR|  
|COLUMN_SIZE|10|16|16|23|27|34|  
|LITERAL_PREFIX|'|'|'|'|'|'|  
|LITERAL_SUFFIX|'|'|'|'|'|'|  
|CREATE_PARAMS|NULL|NULL|NULL|NULL|NULL|NULL|  
|NULLABLE|SQL_NULLABLE|SQL_NULLABLE|SQL_NULLABLE|SQL_NULLABLE|SQL_NULLABLE|SQL_NULLABLE|  
|CASE_SENSITIVE|SQL_FALSE|SQL_FALSE|SQL_FALSE|SQL_FALSE|SQL_FALSE|SQL_FALSE|  
|SEARCHABLE|SQL_PRED_SEARCHABLE|SQL_PRED_SEARCHABLE|SQL_PRED_SEARCHABLE|SQL_PRED_SEARCHABLE|SQL_PRED_SEARCHABLE|SQL_PRED_SEARCHABLE|  
|UNSIGNED_ATTRIBUTE|NULL|NULL|NULL|NULL|NULL|NULL|  
|FXED_PREC_SCALE|SQL_FALSE|SQL_FALSE|SQL_FALSE|SQL_FALSE|SQL_FALSE|SQL_FALSE|  
|AUTO_UNIQUE_VALUE|NULL|NULL|NULL|NULL|NULL|NULL|  
|LOCAL_TYPE_NAME|date|time|smalldatetime|datetime|datetime2|datetimeoffset|  
|MINIMUM_SCALE|NULL|NULL|0|3|NULL|NULL|  
|MAXIMUM_SCALE|NULL|NULL|0|3|NULL|NULL|  
|SQL_DATA_TYPE|SQL_WVARCHAR|SQL_WVARCHAR|SQL_DATETIME|SQL_DATETIME|SQL_WVARCHAR|SQL_WVARCHAR|  
|SQL_DATETIME_SUB|NULL|NULL|SQL_CODE_TIMESTAMP|SQL_CODE_TIMESTAMP|NULL|NULL|  
|NUM_PREC_RADIX|NULL|NULL|NULL|NULL|NULL|NULL|  
|INTERVAL_PRECISION|NULL|NULL|NULL|NULL|NULL|NULL|  
|USERTYPE|0|0|12|22|0|0|  
  
## Down-Level Server Behavior  
 When connected to a server instance of an earlier version that [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)], any attempt to use the new server types or associated metadata codes and descriptor fields will result in SQL_ERROR being returned. A diagnostic record will be generated with SQLSTATE HY004 and the message "Invalid SQL data type for server version on connection", or with 07006 and "Restricted data type attribute violation".  
  
## See Also  
 [Date and Time Improvements &#40;ODBC&#41;](date-and-time-improvements-odbc.md)  
  
  
