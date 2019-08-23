---
title: "SQLGetTypeInfo | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "SQLGetTypeInfo function"
ms.assetid: 13b982c3-ae03-4155-bc0d-e225050703ce
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLGetTypeInfo
  The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver reports the additional column USERTYPE in the result set of `SQLGetTypeInfo`. USERTYPE reports the DB-Library data type definition and is useful to developers porting existing DB-Library applications to ODBC.  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] treats identity as an attribute, whereas ODBC treats it as a data type. To resolve this mismatch, `SQLGetTypeInfo` returns the data types: **intidentity**, **smallintidentity**, **tinyintidentity**, **decimalidentity**, and **numericidentity**. The `SQLGetTypeInfo` result set column AUTO_UNIQUE_VALUE reports the value TRUE for these data types.  
  
 For **varchar**, **nvarchar** and **varbinary**, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver continues to report 8000, 4000 and 8000 respectively for the COLUMN_SIZE value, even though it is actually unlimited. This is to ensure backward compatibility.  
  
 For the **xml** data type, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver reports SQL_SS_LENGTH_UNLIMITED for COLUMN_SIZE to denote unlimited size.  
  
## SQLGetTypeInfo and Table-Valued Parameters  
 The table type for table-valued parameters is effectively a meta-type-that is, a type used to define other types. Therefore, it does not have to be exposed through SQLGetTypeInfo. Applications must use SQLTables, rather than SQLGetTypeInfo, to retrieve metadata for table types used with table-valued parameters.  
  
 For more information, about retrieving metdata for table-valued parameters, see [Statement Attributes that Affect Table-Valued Parameters](../native-client-odbc-table-valued-parameters/statement-attributes-that-affect-table-valued-parameters.md).  
  
 For more information about table-valued parameters, see [Table-Valued Parameters &#40;ODBC&#41;](../native-client-odbc-table-valued-parameters/table-valued-parameters-odbc.md).  
  
## SQLGetTypeInfo Support for Enhanced Date and Time Features  
 For the values returned for date/time types, see [Catalog Metadata](../native-client-odbc-date-time/metadata-catalog.md).  
  
 For more general information, see [Date and Time Improvements &#40;ODBC&#41;](../native-client-odbc-date-time/date-and-time-improvements-odbc.md).  
  
## SQLGetTypeInfo Support for Large CLR UDTs  
 `SQLGetTypeInfo` supports large CLR user-defined types (UDTs). For more information, see [Large CLR User-Defined Types &#40;ODBC&#41;](../native-client/odbc/large-clr-user-defined-types-odbc.md).  
  
## See Also  
 [SQLGetTypeInfo Function](https://go.microsoft.com/fwlink/?LinkId=59356)   
 [ODBC API Implementation Details](odbc-api-implementation-details.md)  
  
  
