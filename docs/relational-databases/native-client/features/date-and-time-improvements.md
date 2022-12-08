---
description: "SQL Server Native Client Date and Time Improvements"
title: "Date and Time Improvements | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.reviewer: ""
ms.service: sql
ms.subservice: native-client
ms.topic: "reference"
ms.assetid: 9b1d0d9d-1f6e-4399-8f61-e23f9a486a7a
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# SQL Server Native Client Date and Time Improvements
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

> [!IMPORTANT] 
> [!INCLUDE[snac-removed-oledb-and-odbc](../../../includes/snac-removed-oledb-and-odbc.md)]

  This topic describes the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client support for the date and time data types that were added in [!INCLUDE[ssKatmai](../../../includes/sskatmai-md.md)].  
  
 For more information about date/time improvements, see [Date and Time Improvements &#40;OLE DB&#41;](../../../relational-databases/native-client-ole-db-date-time/date-and-time-improvements-ole-db.md) and [Date and Time Improvements &#40;ODBC&#41;](../../../relational-databases/native-client-odbc-date-time/date-and-time-improvements-odbc.md).  
  
## Usage  
 The following sections describe various ways of using the new date and time types.  
  
### Use Date as a Distinct Data Type  
 Beginning with [!INCLUDE[ssKatmai](../../../includes/sskatmai-md.md)], enhanced support for date/time types makes it more efficient to use the SQL_TYPE_DATE ODBC type (SQL_DATE for ODBC 2.0 applications) and the DBTYPE_DBDATE OLE DB type.  
  
### Use Time as a Distinct Data Type  
 OLE DB already has a data type that just contains the time, DBTYPE_DBTIME, which has a precision of 1 second. In ODBC, the equivalent type is SQL_TYPE_TIME (SQL_TIME for ODBC 2.0 applications).  
  
 The new [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] time data type has fractional seconds accurate to 100 nanoseconds. This requires new types in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client: DBTYPE_DBTIME2 (OLE DB) and SQL_SS_TIME2 (ODBC). Existing applications written to use times with no fractional seconds can use time(0) columns. The existing OLE DB DBTYPE_TIME and ODBC SQL_TYPE_TIME types and their corresponding structs should work correctly, unless the applications rely on the type returned in metadata.  
  
### Use Time as a Distinct Data Type with Extended Fractional Seconds Precision  
 Some applications, such as process control and manufacturing applications, require the ability to handle time data with a precision of up to 100 nanoseconds. New types for this purpose are DBTYPE_DBTIME2 (OLE DB) and SQL_SS_TIME2 (ODBC).  
  
### Use Datetime with Extended Fractional Seconds Precision  
 OLE DB already defines a type with a precision of up to 1 nanosecond. However, this type is already used by existing [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] applications and such applications have an expectation of only 1/300 of a second precision. The new **datetime2(3)** type is not directly compatible with the existing datetime type. If there is a risk that this will affect application behavior, applications must use a new DBCOLUMN flag to determine the actual server type.  
  
 ODBC also defines a type with a precision of up to 1 nanosecond. However, this type is already used by existing [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] applications and such applications expect only 3 millisecond precision. The new **datetime2(3)** type is not  directly compatible with the existing **datetime** type. **datetime2(3)** has a precision of one millisecond, and **datetime** has a precision of 1/300 of a second. In ODBC, applications can determine which server type is in use with the descriptor field SQL_DESC_TYPE_NAME. Therefore, the existing type SQL_TYPE_TIMESTAMP (SQL_TIMESTAMP for ODBC 2.0 applications) can be used for both types.  
  
### Use Datetime with Extended Fractional Seconds Precision and Timezone  
 Some applications require datetime values with timezone information. This is supported by the new DBTYPE_DBTIMESTAMPOFFSET (OLE DB) and SQL_SS_TIMESTAMPOFFSET (ODBC) types.  
  
### Use Date/Time/Datetime/Datetimeoffset Data with Client-Side Conversions Consistent with Existing Conversions  
 The ODBC standard describes how conversions between existing date, time, and timestamp types work. These are extended in a consistent manner to include conversions between all date and time types introduced in [!INCLUDE[ssKatmai](../../../includes/sskatmai-md.md)].  
  
## See Also  
 [SQL Server Native Client Features](../../../relational-databases/native-client/features/sql-server-native-client-features.md)  
  
  
