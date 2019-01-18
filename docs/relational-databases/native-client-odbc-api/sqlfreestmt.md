---
title: "SQLFreeStmt | Microsoft Docs"
ms.custom: ""
ms.date: "11/23/2015"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
apitype: "DLLExport"
helpviewer_keywords: 
  - "SQLFreeStmt function"
ms.assetid: d9666d0b-3446-480e-bf1a-10b01213e411
author: MightyPen
ms.author: genemi
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# SQLFreeStmt
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
[!INCLUDE[SNAC_Deprecated](../../includes/snac-deprecated.md)]

  Generally   
      **SQLFreeStmt** is not recommended in ODBC 3.0 and later. However if the application needs to reuse the statement you should still use **SQLFreeStmt** with the **SQL_RESET_PARAMS** and **SQL_UNBIND** options). You might also use [SQLCloseCursor](../../relational-databases/native-client-odbc-api/sqlclosecursor.md), [SQLBindParameter](../../relational-databases/native-client-odbc-api/sqlbindparameter.md), [SQLBindCol](../../relational-databases/native-client-odbc-api/sqlbindcol.md), [SQLSetDescField](../../relational-databases/native-client-odbc-api/sqlsetdescfield.md), and [SQLFreeHandle](../../relational-databases/native-client-odbc-api/sqlfreehandle.md) to replace or duplicate the function of **SQLFreeStmt** and should use them instead.  
  
 In general, it is more efficient to reuse statements than to drop them and allocate new ones. However in  some situations, like the reusing of statements, SQLFreeStmt still must be used.  
  
## See Also  
 [SQLFreeStmt Function](https://go.microsoft.com/fwlink/?LinkId=59346)   
 [ODBC API Implementation Details](../../relational-databases/native-client-odbc-api/odbc-api-implementation-details.md)  
  
  
