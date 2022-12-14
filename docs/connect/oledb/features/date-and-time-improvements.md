---
title: "Date and Time Improvements"
description: Learn about the OLE DB Driver for SQL Server support for the date and time data types that were added in SQL Server 2008.
author: David-Engel
ms.author: v-davidengel
ms.date: "06/12/2018"
ms.service: sql
ms.subservice: connectivity
ms.topic: "reference"
---
# Date and Time Improvements
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  This topic describes the OLE DB Driver for SQL Server support for the date and time data types that were added in [!INCLUDE[ssKatmai](../../../includes/sskatmai-md.md)].
  
 For more information about date/time improvements, see [Date and Time Improvements &#40;OLE DB&#41;](../../oledb/ole-db-date-time/date-and-time-improvements-ole-db.md).


## Usage  
 The following sections describe various ways of using the new date and time types.  
  
### Use Date as a Distinct Data Type  
 Beginning with [!INCLUDE[ssKatmai](../../../includes/sskatmai-md.md)], enhanced support for date/time types makes it more efficient to use the DBTYPE_DBDATE OLE DB type.  
  
### Use Time as a Distinct Data Type  
 OLE DB already has a data type that just contains the time, DBTYPE_DBTIME, which has a precision of 1 second.
  
 The new [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] time data type has fractional seconds accurate to 100 nanoseconds. This requires a new type in OLE DB Driver for SQL Server: DBTYPE_DBTIME2. Existing applications written to use times with no fractional seconds can use time(0) columns. The existing OLE DB DBTYPE_TIME type and its corresponding structs should work correctly, unless the applications rely on the type returned in metadata.  
  
### Use Time as a Distinct Data Type with Extended Fractional Seconds Precision  
 Some applications, such as process control and manufacturing applications, require the ability to handle time data with a precision of up to 100 nanoseconds. New type for this purpose in OLE DB is DBTYPE_DBTIME2.  
  
### Use Datetime with Extended Fractional Seconds Precision  
 OLE DB already defines a type with a precision of up to 1 nanosecond. However, this type is already used by existing [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] applications and such applications have an expectation of only 1/300 of a second precision. The new **datetime2(3)** type is not directly compatible with the existing datetime type. If there is a risk that this will affect application behavior, applications must use a new DBCOLUMN flag to determine the actual server type.    
  
### Use Datetime with Extended Fractional Seconds Precision and Timezone  
 Some applications require datetime values with timezone information. This is supported by the new DBTYPE_DBTIMESTAMPOFFSET type.
  
### Use Date/Time/Datetime/Datetimeoffset Data with Client-Side Conversions Consistent with Existing Conversions  
 The conversions are extended in a consistent manner to include conversions between all date and time types introduced in [!INCLUDE[ssKatmai](../../../includes/sskatmai-md.md)].  
  
## See Also  
 [OLE DB Driver for SQL Server Features](../../oledb/features/oledb-driver-for-sql-server-features.md)  
  
  
