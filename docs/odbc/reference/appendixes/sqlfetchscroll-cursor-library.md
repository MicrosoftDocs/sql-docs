---
description: "SQLFetchScroll (Cursor Library)"
title: "SQLFetchScroll (Cursor Library) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords: 
  - "SQLFetchScroll function [ODBC], Cursor Library"
ms.assetid: 4417e57c-31dd-475e-8fe9-eab00a459c80
author: David-Engel
ms.author: v-davidengel
---
# SQLFetchScroll (Cursor Library)
> [!IMPORTANT]  
>  This feature will be removed in a future version of Windows. Avoid using this feature in new development work and plan to modify applications that currently use this feature. Microsoft recommends using the driver's cursor functionality.  
  
 This topic discusses the use of the **SQLFetchScroll** function in the cursor library. For general information about **SQLFetchScroll**, see [SQLFetchScroll Function](../../../odbc/reference/syntax/sqlfetchscroll-function.md).  
  
 The cursor library implements **SQLFetchScroll** by repeatedly calling **SQLFetch** in the driver. It transfers the data it retrieves from the driver to the rowset buffers provided by the application. It also caches the data in memory and disk files. When an application requests a new rowset, the cursor library retrieves it as necessary from the driver (if it has not been previously fetched) or the cache (if it has been previously fetched). Finally, the cursor library maintains the status of the cached data and returns this information to the application in the row status array.  
  
 When the cursor library is used, calls to **SQLFetchScroll** cannot be mixed with calls to either **SQLFetch** or **SQLExtendedFetch**.  
  
 When the cursor library is used, calls to **SQLFetchScroll** are supported both for ODBC 2.*x* and for ODBC 3.*x* drivers.  
  
## Rowset Buffers  
 The cursor library optimizes the transfer of data from the driver to the rowset buffer provided by the application if:  
  
-   The application uses row-wise binding.  
  
-   There are no unused bytes between fields in the structure the application declares to hold a row of data.  
  
-   The fields in which **SQLFetch** or **SQLFetchScroll** returns the length/indicator for a column follows the buffer for that column and precedes the buffer for the next column. These fields are optional.  
  
 When the application requests a new rowset, the cursor library retrieves data from its cache and from the driver as necessary. If the new and old rowsets overlap, the cursor library can optimize its performance by reusing the data from the overlapping sections of the rowset buffers. Therefore, unsaved changes to the rowset buffers are lost unless the new and old rowsets overlap and the changes are in the overlapping sections of the rowset buffers. To save the changes, an application submits a positioned update statement.  
  
 Note that the cursor library always refreshes the rowset buffers with data from the cache when an application calls **SQLFetchScroll** with the *FetchOrientation* argument set to SQL_FETCH_RELATIVE and the *FetchOffset* argument set to 0.  
  
 The cursor library supports calling **SQLSetStmtAttr** with an *Attribute* of SQL_ATTR_ROW_ARRAY_SIZE to change the rowset size while a cursor is open. The new rowset size will take effect the next time **SQLFetchScroll** is called.  
  
## Result Set Membership  
 The cursor library retrieves data from the driver only as the application requests it. Depending on the data source and the setting of the SQL_CONCURRENCY statement attribute, this has the following consequences:  
  
-   The data retrieved by the cursor library might differ from the data that was available at the time the statement was executed. For example, after the cursor was opened, rows inserted at a point beyond the current cursor position can be retrieved by some drivers.  
  
-   The data in the result set might be locked by the data source for the cursor library and therefore be unavailable to other users.  
  
 After the cursor library has cached a row of data, it cannot detect changes to that row in the underlying data source (except for positioned updates and deletes operating on the same cursor's cache). This occurs because, for calls to **SQLFetchScroll**, the cursor library never refetches data from the data source. Instead, it refetches data from its cache.  
  
## Scrolling  
 The cursor library supports the following fetch types in **SQLFetchScroll**.  
  
|Cursor type|Fetch types|  
|-----------------|-----------------|  
|Forward-only|SQL_FETCH_NEXT|  
|Static|SQL_FETCH_NEXT<br /><br /> SQL_FETCH_PRIOR<br /><br /> SQL_FETCH_FIRST<br /><br /> SQL_FETCH_LAST<br /><br /> SQL_FETCH_RELATIVE<br /><br /> SQL_FETCH_ABSOLUTE<br /><br /> SQL_FETCH_BOOKMARK|  
  
## Errors  
 When **SQLFetchScroll** is called and one of the calls to **SQLFetch** returns SQL_ERROR, the cursor library proceeds as follows. After it completes these steps, the cursor library continues processing.  
  
1.  Calls **SQLGetDiagRec** to obtain error information from the driver and posts this as a diagnostic record in the Driver Manager.  
  
2.  Sets the SQL_DIAG_ROW_NUMBER field in the diagnostic record to the appropriate value.  
  
3.  Sets the SQL_DIAG_COLUMN_NUMBER field in the diagnostic record to the appropriate value, if applicable; otherwise, it sets it to 0.  
  
4.  Sets the value for the row in error in the row status array to SQL_ROW_ERROR.  
  
 After the cursor library has called **SQLFetch** multiple times in its implementation of **SQLFetchScroll**, any error or warning returned by one of the calls to **SQLFetch** will be in a diagnostic record and can be retrieved by a call to **SQLGetDiagRec**. If the data was truncated when it was fetched, the truncated data will now reside in the cursor library's cache. Subsequent calls to **SQLFetchScroll** to scroll to a row with truncated data will return the truncated data, and no warning will be raised because the data is fetched from the cursor library's cache. To keep track of the length of data returned so that it can determine whether the data returned in a buffer has been truncated, an application should bind the length/indicator buffer.  
  
## Bookmark Operations  
 The cursor library supports calling **SQLFetchScroll** with a *FetchOrientation* of SQL_FETCH_BOOKMARK. It also supports specifying an offset in the *FetchOffset* argument that can be used in the bookmark operation. This is the only bookmark operation the cursor library supports. The cursor library does not support calling **SQLBulkOperations**.  
  
 If the application has set the SQL_ATTR_USE_BOOKMARKS statement attribute and has bound to the bookmark column, the cursor library generates a fixed-length bookmark and returns it to the application. The cursor library creates and maintains the bookmarks that it uses; it does not use bookmarks maintained at the data source. When **SQLFetchScroll** is called to retrieve a block of data that has already been fetched from the data source, it retrieves the data from the cursor library cache. As a result, the bookmark used in a call to **SQLFetchScroll** with a *FetchOrientation* of SQL_FETCH_BOOKMARK must be created and maintained by the cursor library.  
  
## Interaction with Other Functions  
 An application must call **SQLFetch** or **SQLFetchScroll** before it prepares or executes any positioned update or delete statements.
