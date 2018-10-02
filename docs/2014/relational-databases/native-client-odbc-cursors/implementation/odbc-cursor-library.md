---
title: "ODBC Cursor Library | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "cursors [ODBC], library"
  - "SQL_CUR_USE_DRIVER option"
  - "ODBC applications, cursors"
  - "ODBC cursors, library"
  - "SQL_CUR_USE_IF_NEEDED option"
  - "SQLSetConnectAttr function"
  - "SQL_CUR_USE_ODBC option"
ms.assetid: 3c610d3d-6e06-49cf-9a40-05b6a1c83a32
author: MightyPen
ms.author: genemi
manager: craigg
---
# ODBC Cursor Library
  Some ODBC drivers only support the default cursor settings; these drivers also do not support positioned cursor operations, such as **SQLSetPos**. The ODBC cursor library is a component of the Microsoft Data Access Components (MDAC) used to implement block or static cursors on a driver that normally does not support them. The cursor library also implements positioned UPDATE and DELETE statements and **SQLSetPos** for the cursors it creates.  
  
 The ODBC cursor library is implemented as a layer between the ODBC Driver Manager and an ODBC driver. If the ODBC cursor library is loaded, the ODBC Driver Manager routes all cursor-related commands to the cursor library instead of the driver. The cursor library implements a cursor by fetching the entire result set from the underlying driver and caching the result set on the client. When using the ODBC cursor library, the application is limited to the cursor functionality of the cursor library; any support for additional cursor functionality in the underlying driver is not available to the application.  
  
 There is little need to use the ODBC cursor library with the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver because the driver itself supports more cursor functionality than the ODBC cursor library. The only reason to use the ODBC cursor library with the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver is because the driver implements its cursor support through server cursors, and server cursors do not support all SQL statements. Any time there is a need to have a static cursor with stored procedures, batches, or SQL statements containing COMPUTE, COMPUTE BY, FOR BROWSE, or INTO, consider using the ODBC cursor library. However, care must be used with the cursor library because it caches the entire result set on the client, which can use large amounts of memory and slow performance.  
  
 An application invokes the cursor library on a connection-by-connection basis by using [SQLSetConnectAttr](../../native-client-odbc-api/sqlsetconnectattr.md) to set the SQL_ATTR_ODBC_CURSORS connection attribute before connecting to a data source. SQL_ATTR_ODBC_CURSORS is set to one of three values:  
  
 SQL_CUR_USE_ODBC  
 When this option is set with the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver, the ODBC cursor library overrides the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver's native cursor support. Only the cursor types supported by the cursor library can be used for the connection; server cursors cannot be used.  
  
 SQL_CUR_USE_DRIVER  
 When this option is set, all of the cursor support native to the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver can be used for the connection. The ODBC cursor library cannot be used. All cursors are implemented as server cursors.  
  
 SQL_CUR_USE_IF_NEEDED  
 When this option is set, the effect is the same as SQL_CUR_USE_DRIVER when used with the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver. At connect time, the ODBC Driver Manager tests to see if the ODBC driver being connected to supports the SQL_FETCH_PRIOR option of [SQLFetchScroll](../../native-client-odbc-api/sqlfetchscroll.md). If the driver does not support the option, the ODBC Driver Manager loads the ODBC cursor library. If the driver does support the option, the ODBC Driver Manager does not load the ODBC cursor library and the application uses the native support of the driver. Because the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver supports SQL_FETCH_PRIOR, the ODBC Driver Manager does not load the ODBC cursor library.  
  
 The cursor library allows applications to use multiple active statements on a connection, as well as scrollable, updatable cursors. The cursor library must be loaded to support this functionality. Use [SQLSetConnectAttr](../../native-client-odbc-api/sqlsetconnectattr.md) to specify how the cursor library should be used and [SQLSetStmtAttr](../../native-client-odbc-api/sqlsetstmtattr.md) to specify the cursor type, concurrency, and rowset size.  
  
## See Also  
 [How Cursors Are Implemented](how-cursors-are-implemented.md)  
  
  
