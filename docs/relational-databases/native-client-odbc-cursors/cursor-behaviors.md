---
title: "Cursor Behaviors | Microsoft Docs"
ms.custom: ""
ms.date: "10/24/2016"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "scrollable cursors [SQL Server]"
  - "SQL Server Native Client ODBC driver, cursors"
  - "version-based optimistic concurrency"
  - "cursors [ODBC], cursor behaviors"
  - "ODBC applications, cursors"
  - "SQL_ATTR_CURSOR_SENSITIVITY option"
  - "SQL_ATTR_CURSOR_SCROLLABLE option"
  - "sensitivity behavior of cursor"
  - "ODBC cursors, cursor behaviors"
ms.assetid: 742ddcd2-232b-4aa1-9212-027df120ad35
author: MightyPen
ms.author: genemi
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Cursor Behaviors
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
[!INCLUDE[SNAC_Deprecated](../../includes/snac-deprecated.md)]

  ODBC supports the ISO options for specifying the behavior of cursors by specifying their scrollability and sensitivity. These behaviors are specified by setting the SQL_ATTR_CURSOR_SCROLLABLE and SQL_ATTR_CURSOR_SENSITIVITY options on a call to [SQLSetStmtAttr](../../relational-databases/native-client-odbc-api/sqlsetstmtattr.md). The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver implements these options by requesting server cursors with the following characteristics.  
  
|Cursor behavior settings|Server cursor characteristics requested|  
|------------------------------|---------------------------------------------|  
|SQL_SCROLLABLE and SQL_SENSITIVE|Keyset-driven cursor and version-based optimistic concurrency|  
|SQL_SCROLLABLE and SQL_INSENSITIVE|Static cursor and read-only concurrency|  
|SQL_SCROLLABLE and SQL_UNSPECIFIED|Static cursor and read-only concurrency|  
|SQL_NONSCROLLABLE and SQL_SENSITIVE|Forward-only cursor and version-based optimistic concurrency|  
|SQL_NONSCROLLABLE and SQL_INSENSITIVE|Default result set (forward-only, read-only)|  
|SQL_NONSCROLLABLE and SQL_UNSPECIFIED|Default result set (forward-only, read-only)|  
  
 Version-based optimistic concurrency requires a **timestamp** column in the underlying table. If version-based optimistic concurrency control is requested on a table that does not have a **timestamp** column, the server uses values-based optimistic concurrency.  
  
## Scrollability  
 When SQL_ATTR_CURSOR_SCROLLABLE is set to SQL_SCROLLABLE, the cursor supports all the different values for the *FetchOrientation* parameter of [SQLFetchScroll](../../relational-databases/native-client-odbc-api/sqlfetchscroll.md). When SQL_ATTR_CURSOR_SCROLLABLE is set to SQL_NONSCROLLABLE, the cursor only supports a *FetchOrientation* value of SQL_FETCH_NEXT.  
  
## Sensitivity  
 When SQL_ATTR_CURSOR_SENSITIVITY is set to SQL_SENSITIVE, the cursor reflects data modifications made by the current user or committed by other users. When SQL_ATTR_CURSOR_SENSITIVITY is set to SQL_INSENSITIVE, the cursor does not reflect data modifications.  
  
## See Also  
 [Using Cursors (ODBC)](../../relational-databases/native-client-odbc-cursors/using-cursors-odbc.md) 
 [Cursor Properties](properties/cursor-properties.md) 
  
  
