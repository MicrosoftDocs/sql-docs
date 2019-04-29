---
title: "Conversions Performed from Client to Server | Microsoft Docs"
description: "Conversions performed from client to server"
ms.custom: ""
ms.date: "06/14/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: connectivity
ms.topic: "reference"
helpviewer_keywords: 
  - "conversions [OLE DB], client to server"
author: pmasl
ms.author: pelopes
manager: craigg
---
# Conversions Performed from Client to Server
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  This article describes date/time conversions performed between a client application written with OLE DB Driver for SQL Server and [!INCLUDE[ssKatmai](../../../includes/sskatmai-md.md)] (or later).  
  
## Conversions  
 This article describes conversions made on the client. If the client specifies fractional seconds precision for a parameter that differs from that defined on the server, the client conversion might cause a failure in cases where the server would allow the operation to succeed. In particular, the client treats any truncation of fractional seconds as an error, whereas [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] rounds time values to the nearest whole second.  
  
 If ICommandWithParameters::SetParameterInfo is not called, DBTYPE_DBTIMESTAMP bindings are converted as if they were **datetime2**.  
  
|To -><br /><br /> From|DBDATE (date)|DBTIME (time)|DBTIME2 (time)|DBTIMESTAMP (smalldatetime)|DBTIMESTAMP (datetime)|DBTIMESTAMP (datetime2)|DBTIMESTAMPOFFSET (datetimeoffset)|STR|WSTR|SQLVARIANT<br /><br /> (sql_variant)|  
|----------------------|---------------------|---------------------|----------------------|-----------------------------------|------------------------------|-------------------------------|------------------------------------------|---------|----------|-------------------------------------|  
|DATE|1, 2|1, 3, 4|4, 12|1, 12|1, 12|1, 12|1, 5, 12|1, 12|1, 12|1, 12<br /><br /> datetime2(0)|  
|DBDATE|1|-|-|1, 6|1, 6|1, 6|1, 5, 6|1, 10|1, 10|1<br /><br /> date|  
|DBTIME|-|1|1|1, 7|1, 7|1, 7|1, 5, 7|1, 10|1, 10|1<br /><br /> Time(0)|  
|DBTIME2|-|1, 3|1|1, 7, 10, 14|1, 7, 10, 15|1, 7, 10|1, 5, 7, 10|1, 10, 11|1, 10, 11|1<br /><br /> Time(7)|  
|DBTIMESTAMP|1, 2|1, 3, 4|1, 4, 10|1, 10, 14|1, 10, 15|1, 10|1, 5, 10|1, 10,11|1, 10, 11|1, 10<br /><br /> datetime2(7)|  
|DBTIMESTAMPOFFSET|1, 2, 8|1, 3, 4, 8|1, 4, 8, 10|1, 8, 10, 14|1, 8, 10, 15|1, 8, 10|1, 10|1, 10, 11|1, 10, 11|1, 10<br /><br /> datetimeoffset(7)|  
|FILETIME|1, 2|1, 3, 4|1, 4, 13|1, 13|1, 13|1, 13|1, 5, 13|1, 13|1, 10|1, 13<br /><br /> datetime2(3)|  
|BYTES|-|-|-|-|-|-|-|N/A|N/A|N/A|  
|VARIANT|1|1|1|1, 10|1, 10|1, 10|1, 10|N/A|N/A|1, 10|  
|SSVARIANT|1, 16|1, 16|1, 16|1, 10, 16|1, 10, 16|1, 10, 16|1, 10, 16|N/A|N/A|1, 16|  
|BSTR|1, 9|1, 9|1, 9, 10|1, 9, 10|1, 9, 10|1, 9, 10|1, 9, 10|N/A|N/A|N/A|  
|STR|1, 9|1, 9|1, 9, 10|1, 9, 10|1, 9, 10|1, 9, 10|1, 9, 10|N/A|N/A|N/A|  
|WSTR|1, 9|1, 9|1, 9, 10|1, 9, 10|1, 9, 10|1, 9, 10|1, 9, 10|N/A|N/A|N/A|  
  
## Key to Symbols  
  
|Symbol|Meaning|  
|------------|-------------|  
|-|No conversion is supported. If the binding is validated when IAccessor::CreateAccessor is called, DBBINDSTATUS_UPSUPPORTEDCONVERSION is returned in *rgStatus*. When accessor validation is deferred, DBSTATUS_E_BADACCESSOR is set.|  
|N/A|Not applicable.|  
|1|If the data supplied is not valid, DBSTATUS_E_CANTCONVERTVALUE is set. Input data is validated before conversions are applied, so even when a component will be ignored by a subsequent conversion it must still be valid for the conversion to succeed.|  
|2|Time fields are ignored.|  
|3|Fractional seconds must be zero or DBSTATUS_E_DATAOVERFLOW is set.|  
|4|The date component is ignored.|  
|5|The timezone is set to the client's timezone setting.|  
|6|The time is set to zero.|  
|7|The date is set to the current date.|  
|8|The time is converted to UTC. If an error occurs during this conversion, DBSTATUS_E_CANTCONVERTVALUE is set.|  
|9|The string is parsed as an ISO literal and converted to the target type. If this fails, the string is parsed as an OLE date literal (which also has time components) and converted from an OLE date (DBTYPE_DATE) to the target type.<br /><br /> If the target type is DBTIMESTAMP, **smalldatetime**, **datetime**, or **datetime2**, the string must conform to the syntax for date, time, or **datetime2** literals, or the syntax recognized by OLE. If the string is a date literal, all time components are set to zero. If the string is a time literal, the date is set to the current date.<br /><br /> For all other target types, the string must conform to the syntax for literals of the target type.|  
|10|If truncation of fractional seconds with data loss occurs, DBSTATUS_E_DATAOVERFLOW is set. For string conversions, overflow checking is only possible when the string conforms to ISO syntax. If the string is an OLE date literal, fractional seconds are rounded.<br /><br /> For conversion from DBTIMESTAMP (datetime) to smalldatetime OLE DB Driver for SQL Server will silently truncate the seconds value instead of raising the DBSTATUS_E_DATAOVERFLOW error.|  
|11|The number of fractional second digits (the scale) is determined from the destination column's size, according to the table below. For column sizes larger than the range in the table, a scale of 9 is implied. This conversion should allow for up to nine fractional second digits, the maximum allowed by OLE DB.<br /><br /> However, if the source type is DBTIMESTAMP and the fractional seconds is zero, no fractional second digits or decimal point are generated. This behavior ensures backwards compatibility for applications developed using older OLE DB providers.<br /><br /> A column size of ~0 implies unlimited size in OLE DB (9 digits, unless the 3-digit rule for DBTIMESTAMP applies).|  
|12|Conversion semantics prior to [!INCLUDE[ssKatmai](../../../includes/sskatmai-md.md)] for DBTYPE_DATE are maintained. Fractional seconds are truncated to zero.|  
|13|Conversion semantics prior to [!INCLUDE[ssKatmai](../../../includes/sskatmai-md.md)] for DBTYPE_FILETIME are maintained. If you use the Windows FileTimeToSystemTime API, the fractional seconds precision is limited to 1 millisecond.|  
|14|Conversion semantics prior to [!INCLUDE[ssKatmai](../../../includes/sskatmai-md.md)] for **smalldatetime** are maintained. Seconds are set to zero.|  
|15|Conversion semantics prior to [!INCLUDE[ssKatmai](../../../includes/sskatmai-md.md)] for **datetime** are maintained. Seconds are rounded to the nearest 300th of a second.|  
|16|The conversion behavior of a value (of a given type) embedded in a SSVARIANT client struct is the same as the behavior of the same value and type when not embedded in a SSVARIANT client struct.|  
  
||||  
|-|-|-|  
|Type|Length (in chars)|Scale|  
|DBTIME2|8, 10..18|0,1..9|  
|DBTIMESTAMP|19, 21..29|0,1..9|  
|DBTIMESTAMPOFFSET|26, 28..36|0,1..9|  
  
## See Also  
 [Bindings and Conversions &#40;OLE DB&#41;](../../oledb/ole-db-date-time/conversions-ole-db.md)  
  
  
