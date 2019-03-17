---
title: "SQLSpecialColumns | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "SQLSpecialColumns function"
ms.assetid: dffe02ed-8f79-4c9a-af34-98130bbe5462
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLSpecialColumns
  When requesting row identifiers (*IdentifierType* SQL_BEST_ROWID), **SQLSpecialColumns** returns an empty result set (no data rows) for any requested scope other than SQL_SCOPE_CURROW. The generated result set indicates that the columns are only valid within this scope.  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] does not support pseudocolumns for identifiers. The **SQLSpecialColumns** result set will identify all columns as SQL_PC_NOT_PSEUDO.  
  
 **SQLSpecialColumns** can be executed on a static cursor. An attempt to execute **SQLSpecialColumns** on an updatable (keyset-driven or dynamic) returns SQL_SUCCESS_WITH_INFO indicating the cursor type has been changed.  
  
## SQLSpecialColumns Support for Enhanced Date and Time Features  
 For information about the values returned for the columns DATA_TYPE, TYPE_NAME, COLUMN_SIZE, BUFFER_LENGTH, and DECIMAL_DIGTS for date/time types, see [Catalog Metadata](../native-client-odbc-date-time/metadata-catalog.md).  
  
 For more general information, see [Date and Time Improvements &#40;ODBC&#41;](../native-client-odbc-date-time/date-and-time-improvements-odbc.md).  
  
## SQLSpecialColumns Support for Large CLR UDTs  
 **SQLSpecialColumns** supports large CLR user-defined types (UDTs). For more information, see [Large CLR User-Defined Types &#40;ODBC&#41;](../native-client/odbc/large-clr-user-defined-types-odbc.md).  
  
## See Also  
 [SQLSpecialColumns Function](https://go.microsoft.com/fwlink/?LinkId=59371)   
 [ODBC API Implementation Details](odbc-api-implementation-details.md)  
  
  
