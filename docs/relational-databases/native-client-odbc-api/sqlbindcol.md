---
title: "SQLBindCol | Microsoft Docs"
ms.custom: ""
ms.date: "03/17/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
apitype: "DLLExport"
helpviewer_keywords: 
  - "SQLBindCol function"
ms.assetid: fbd7ba20-d917-4ca9-b018-018ac6af9f98
author: MightyPen
ms.author: genemi
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# SQLBindCol
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
[!INCLUDE[SNAC_Deprecated](../../includes/snac-deprecated.md)]

  As a general rule, consider the implications of using **SQLBindCol** to cause data conversion. Binding conversions are client processes, so, for example, retrieving a floating-point value bound to a character column causes the driver to perform the float-to-character conversion locally when a row is fetched. The [!INCLUDE[tsql](../../includes/tsql-md.md)] CONVERT function can be used to place the cost of data conversion on the server.  
  
 An instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can return multiple sets of result rows on a single statement execution. Each result set must be bound separately. For more information about binding for multiple result sets, see [SQLMoreResults](../../relational-databases/native-client-odbc-api/sqlmoreresults.md).  
  
 The developer can bind columns to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]-specific C data types using the *TargetType* value **SQL_C_BINARY**. Columns bound to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]-specific types are not portable. The defined [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]-specific ODBC C data types match the type definitions for DB-Library, and DB-Library developers porting applications may want to take advantage of this feature.  
  
 Reporting data truncation is an expensive process for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver. You can avoid truncation by ensuring that all bound data buffers are wide enough to return data. For character data, the width should include space for a string terminator when the default driver behavior for string termination is used. For example, binding a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **char(5)** column to an array of five characters results in truncation for every value fetched. Binding the same column to an array of six characters avoids the truncation by providing a character element in which to store the null terminator. [SQLGetData](../../relational-databases/native-client-odbc-api/sqlgetdata.md) can be used to efficiently retrieve long character and binary data without truncation.  
  
 For large value data types, if the user supplied buffer isn't large enough to hold the entire value of the column, **SQL_SUCCESS_WITH_INFO** is returned and the "string data; right truncation" warning is issued. The **StrLen_or_IndPtr** argument will contain the number of chars/bytes stored in the buffer.  
  
## SQLBindCol Support for Enhanced Date and Time Features  
 Result column values of date/time types are converted as described in [Conversions from SQL to C](../../relational-databases/native-client-odbc-date-time/datetime-data-type-conversions-from-sql-to-c.md). Note that to retrieve time and datetimeoffset columns as their corresponding structures (**SQL_SS_TIME2_STRUCT** and **SQL_SS_TIMESTAMPOFFSET_STRUCT**), *TargetType* must be specified as **SQL_C_DEFAULT** or **SQL_C_BINARY**.  
  
 For more information, see [Date and Time Improvements &#40;ODBC&#41;](../../relational-databases/native-client-odbc-date-time/date-and-time-improvements-odbc.md).  
  
## SQLBindCol Support for Large CLR UDTs  
 **SQLBindCol** supports large CLR user-defined types (UDTs). For more information, see [Large CLR User-Defined Types &#40;ODBC&#41;](../../relational-databases/native-client/odbc/large-clr-user-defined-types-odbc.md).  
  
## See Also  
 [SQLBindCol Function](https://go.microsoft.com/fwlink/?LinkId=59327)   
 [ODBC API Implementation Details](../../relational-databases/native-client-odbc-api/odbc-api-implementation-details.md)  
  
  
