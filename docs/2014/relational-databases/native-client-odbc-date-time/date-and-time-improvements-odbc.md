---
title: "Date and Time Improvements (ODBC) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "date/time [ODBC]"
  - "ODBC, date/time improvements"
ms.assetid: e31d5ca5-2103-498f-954c-1ee93e217186
author: MightyPen
ms.author: genemi
manager: craigg
---
# Date and Time Improvements (ODBC)
  [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] introduced new date and time data types. This section describes how these new types are exposed as extensions in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client. For an overview of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client support for the new date and time data types, see [Date and Time Improvements](../native-client/features/date-and-time-improvements.md). For a sample demonstrating ODBC date/time support, see [Use Date and Time Types](../native-client-odbc-how-to/use-date-and-time-types.md).  
  
 For more general information about date and time data types, see [datetime &#40;Transact-SQL&#41;](/sql/t-sql/data-types/datetime-transact-sql).  
  
## In This Section  
 [Data Type Support for ODBC Date and Time Improvements](../../relational-databases/native-client-odbc-date-time/data-type-support-for-odbc-date-and-time-improvements.md)  
 Provides information about ODBC types that support [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] date and time data types.  
  
 [Metadata &#40;ODBC&#41;](../../database-engine/dev-guide/metadata-odbc.md)  
 Describes information returned in the implementation parameter descriptor (IPD) and implementation row descriptor (IRD) fields, as well as column metadata returned by `SQLColumns` and `SQLProcedureColumns`. Also describes data type metadata returned by `SQLGetTypeInfo`.  
  
 [datetime Data Type Conversions &#40;ODBC&#41;](datetime-data-type-conversions-odbc.md)  
 Describes how to convert between datetime and datetimeoffset values.  
  
 [sql_variant Support for Date and Time Types](sql-variant-support-for-date-and-time-types.md)  
 Describes SQL_VARIANT function support for enhanced date and time functionality.  
  
 [Bulk Copy Changes for Enhanced Date and Time Types &#40;OLE DB and ODBC&#41;](../../relational-databases/native-client-odbc-date-time/bulk-copy-changes-for-enhanced-date-and-time-types-ole-db-and-odbc.md)  
 Describes date/time enhancements to support bulk copy operations.  
  
 [Enhanced Date and Time Type Behavior with Previous SQL Server Versions &#40;ODBC&#41;](enhanced-date-and-time-type-behavior-with-previous-sql-server-versions-odbc.md)  
 Describes the expected behavior when a client application using enhanced date and time features communicates with an older version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], and when a client compiled with an older version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client sends commands to a server that supports enhanced date and time features.  
  
 [ODBC API Support for Enhanced Date and Time Features](odbc-api-support-for-enhanced-date-and-time-features.md)  
 Lists the ODBC functions that support enhanced date and time functionality.  
  
## See Also  
 [SQL Server Native Client &#40;ODBC&#41;](../../relational-databases/native-client/odbc/sql-server-native-client-odbc.md)  
  
  
