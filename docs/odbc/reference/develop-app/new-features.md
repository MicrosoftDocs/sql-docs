---
description: "New Features"
title: "New Features | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "backward compatibility [ODBC], new features in release"
  - "ODBC drivers [ODBC], backward compatibility"
  - "new features [ODBC]"
  - "compatibility [ODBC], new features in release"
  - "ODBC [ODBC], new features"
ms.assetid: a8fcdd00-6cb3-4871-9489-6018b3d0d65f
author: David-Engel
ms.author: v-davidengel
---
# New Features
The following new functionality has been introduced in ODBC *3.x*. An ODBC *3.x* application working with an ODBC *2.x* driver will not be able to use this functionality. The ODBC *3.x* Driver Manager does not map these features when working with an ODBC *2.x* driver.  
  
-   Functions that take a descriptor handle as an argument: **SQLSetDescField**, **SQLGetDescField**, **SQLSetDescRec**, **SQLGetDescRec**, and **SQLCopyDesc**.  
  
-   The functions **SQLSetEnvAttr** and **SQLGetEnvAttr**.  
  
-   The use of **SQLAllocHandle** to allocate a descriptor handle. (The use of **SQLAllocHandle** to allocate environment, connection, and statement handles is duplicated, not new, functionality.)  
  
-   The use of **SQLGetConnectAttr** to get the SQL_ATTR_AUTO_IPD connection attributes. (The use of **SQLSetConnectAttr** to set, and **SQLGetConnectAttr** to get, other connection attributes is duplicated, not new, functionality.)  
  
-   The use of **SQLSetStmtAttr** to set, and **SQLGetStmtAttr** to get, the following statement attributes. (The use of **SQLSetStmtAttr** to set, and **SQLGetStmtAttr** to get, other statement attributes is duplicated, not new, functionality.)  
  
     SQL_ATTR_APP_ROW_DESC  
  
     SQL_ATTR_APP_PARAM_DESC  
  
     SQL_ATTR_ENABLE_AUTO_IPD  
  
     SQL_ATTR_FETCH_BOOKMARK_PTR  
  
     SQL_ATTR_BIND_OFFSET  
  
     SQL_ATTR_METADATA_ID  
  
     SQL_ATTR_PARAM_BIND_OFFSET_PTR  
  
     SQL_ATTR_PARAM_BIND_TYPE  
  
     SQL_ATTR_PARAM_OPERATION_PTR  
  
     SQL_DESC_PARAM_STATUS_PTR  
  
     SQL_ATTR_PARAMS_PROCESSED_PTR  
  
     SQL_ATTR_PARAMSET_SIZE  
  
     SQL_ATTR_ROW_BIND_OFFSET_PTR  
  
     SQL_ATTR_ROW_OPERATION_PTR  
  
     SQL_ATTR_ROW_ARRAY_SIZE  
  
-   The use of **SQLGetStmtAttr** to get the following statement attributes. (The use of **SQLGetStmtAttr** to get other statement attributes is duplicated functionality, not new functionality.)  
  
     SQL_ATTR_IMP_ROW_DESC SQL_ATTR_IMP_PARAM_DESC  
  
-   Use of the interval C data type, the interval SQL data types, the BIGINT C data types, and the SQL_C_NUMERIC data structure.  
  
-   Row-wise binding of parameters.  
  
-   Offset-based bookmark fetches, such as calling **SQLFetchScroll** with a *FetchOrientation* argument of SQL_FETCH_BOOKMARK and specifying an offset other than 0.  
  
-   **SQLFetch** returning the row status array, number of rows fetched, fetching multiple rows, intermixing calls with **SQLFetchScroll**, and intermixing calls with **SQLBulkOperations** or **SQLSetPos**. For more information, see the next section, [Block Cursors, Scrollable Cursors, and Backward Compatibility for ODBC 3.x Applications](../../../odbc/reference/develop-app/block-cursors-scrollable-backward-compatibility-odbc-3-x-applications.md).  
  
-   Named parameters.  
  
-   Any of the ODBC *3.x*-specific **SQLGetInfo** options. (If an ODBC *3.x* application working with an ODBC *2.x* driver calls the SQL_XXX_CURSOR_ATTRIBUTES1 information types, which have replaced several ODBC *2.x* information types, some of the information might be reliable, but some might be unreliable. For more information, see [SQLGetInfo](../../../odbc/reference/syntax/sqlgetinfo-function.md).)  
  
-   Bind offsets.  
  
-   Updating, refreshing, and deleting by bookmarks (through a call to **SQLBulkOperations**).  
  
-   Calling **SQLBulkOperations** or **SQLSetPos** in the S5 state.  
  
-   The ROW_NUMBER and COLUMN_NUMBER fields in the diagnostic record (which have to be retrieved by the replacement functions **SQLGetDiagField** or **SQLGetDiagRec**).  
  
-   Approximate row counts.  
  
-   Warning information (SQL_ROW_SUCCESS_WITH_INFO from **SQLFetchScroll**).  
  
-   Variable-length bookmarks.  
  
-   Extended error information for arrays of parameters.  
  
-   All of the new columns in the result sets returned by the catalog functions.  
  
-   Use of **SQLDescribeCol** and **SQLColAttribute** on column 0.  
  
-   Use of any ODBC *3.x*-specific column attributes in a call to **SQLColAttribute**.  
  
-   Use of multiple environment handles.  
  
 This section contains the following topic.  
  
-   [Block Cursors, Scrollable Cursors, and Backward Compatibility for ODBC 3.x Applications](../../../odbc/reference/develop-app/block-cursors-scrollable-backward-compatibility-odbc-3-x-applications.md)
