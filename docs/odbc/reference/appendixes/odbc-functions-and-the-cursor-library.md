---
title: "ODBC functions and the cursor library"
description: "Learn how the ODBC cursor library intercepts function calls to provide scrollable cursor support for drivers that don't natively support it."
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, sunilbs, mcimfl
ms.date: 02/05/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
ai-usage: ai-assisted
---
# ODBC functions and the cursor library

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi.md)]

> [!IMPORTANT]
> This feature will be removed in a future version of Windows. Avoid using this feature in new development work and plan to modify applications that currently use this feature. Microsoft recommends using the driver's cursor functionality.

The ODBC cursor library provides scrollable cursor support for ODBC 2.x drivers that support only forward-only cursors. When you enable the cursor library for a connection, the Driver Manager intercepts function calls and routes them through the cursor library instead of directly to the driver. The cursor library either executes the function itself or passes it to the underlying driver.

## Functions executed by the cursor library

These articles describe which ODBC functions the cursor library handles and which it passes through to the driver.

| Article | Description |
| --- | --- |
| [ODBC functions executed by the cursor library](odbc-functions-executed-by-the-cursor-library.md) | Lists functions that the cursor library implements itself to provide scrollable cursor functionality. |
| [ODBC functions not executed by the cursor library](odbc-functions-not-executed-by-the-cursor-library.md) | Lists functions that the cursor library passes through to the driver without modification. |

## Column and parameter binding

These functions handle binding application buffers to result set columns and statement parameters. The cursor library modifies their behavior to support positioned updates and block cursors.

| Function | Description |
| --- | --- |
| [SQLBindCol (Cursor Library)](sqlbindcol-cursor-library.md) | Binds application buffers to result set columns. The cursor library tracks bindings to support positioned update and delete operations. |
| [SQLBindParameter (Cursor Library)](sqlbindparameter-cursor-library.md) | Binds application buffers to parameter markers. The cursor library passes this call to the driver. |

## Bulk and positioned operations

These functions perform bulk operations on rowsets and positioned updates on individual rows within a cursor's current rowset.

| Function | Description |
| --- | --- |
| [SQLBulkOperations (Cursor Library)](sqlbulkoperations-and-the-cursor-library.md) | Performs bulk insert, update, delete, or fetch by bookmark operations. The cursor library maps this to `SQLSetPos` when working with ODBC 2.x drivers. |
| [SQLSetPos (Cursor Library)](sqlsetpos-cursor-library.md) | Positions the cursor within a rowset and allows applications to refresh, update, or delete data in the rowset. |

## Cursor management and transactions

These functions manage cursor lifetime and transaction boundaries.

| Function | Description |
| --- | --- |
| [SQLCloseCursor (Cursor Library)](sqlclosecursor-odbc.md) | Closes a cursor on a statement and discards pending results. |
| [SQLEndTran (Cursor Library)](sqlendtran-cursor-library.md) | Commits or rolls back a transaction. The cursor library manages cursor state across transaction boundaries based on the `SQL_CURSOR_COMMIT_BEHAVIOR` and `SQL_CURSOR_ROLLBACK_BEHAVIOR` settings. |
| [SQLFreeStmt (Cursor Library)](sqlfreestmt-cursor-library.md) | Stops statement processing, closes any associated cursor, discards pending results, and optionally frees all resources associated with the statement handle. |

## Data retrieval

These functions fetch data from result sets. The cursor library provides scrollable cursor functionality for drivers that only support forward-only cursors.

| Function | Description |
| --- | --- |
| [SQLFetch (Cursor Library)](sqlfetch-cursor-library.md) | Fetches the next rowset of data from the result set. When working with ODBC 2.x drivers, the cursor library maps this to `SQLExtendedFetch`. |
| [SQLFetchScroll (Cursor Library)](sqlfetchscroll-cursor-library.md) | Fetches the specified rowset of data from the result set and returns data for all bound columns. Supports various scroll directions including first, last, next, prior, absolute, and relative positioning. |
| [SQLExtendedFetch (Cursor Library)](sqlextendedfetch-cursor-library.md) | Fetches the specified rowset from the result set. This function is deprecated in ODBC 3.x; use `SQLFetchScroll` instead. |
| [SQLGetData (Cursor Library)](sqlgetdata-cursor-library.md) | Retrieves data for a single column in the result set after `SQLFetch` or `SQLFetchScroll` has been called. |

## Descriptor operations

These functions get and set descriptor field values. Descriptors define the attributes of parameters and result set columns.

| Function | Description |
| --- | --- |
| [SQLGetDescField and SQLGetDescRec (Cursor Library)](sqlgetdescfield-and-sqlgetdescrec-cursor-library.md) | Retrieve individual field values or complete descriptor records. The cursor library handles these for its internal descriptors. |
| [SQLSetDescField and SQLSetDescRec (Cursor Library)](sqlsetdescfield-and-sqlsetdescrec-cursor-library.md) | Set individual field values or complete descriptor records. The cursor library tracks changes to descriptor fields. |

## Driver capability queries

These functions return information about the driver and cursor library capabilities.

| Function | Description |
| --- | --- |
| [SQLGetFunctions (Cursor Library)](sqlgetfunctions-cursor-library.md) | Returns information about which ODBC functions the driver supports. The cursor library modifies the response to include functions it implements. |
| [SQLGetInfo (Cursor Library)](sqlgetinfo-cursor-library.md) | Returns general information about the driver and data source. The cursor library modifies certain cursor-related information types to reflect its capabilities. |

## Statement and connection attributes

These functions get and set statement, connection, and environment attributes that affect cursor behavior.

| Function | Description |
| --- | --- |
| [SQLGetStmtAttr (Cursor Library)](sqlgetstmtattr-cursor-library.md) | Returns the current value of a statement attribute. The cursor library handles attributes related to cursor behavior. |
| [SQLGetStmtOption (Cursor Library)](sqlgetstmtoption-cursor-library.md) | Returns statement option values. This function is deprecated in ODBC 3.x; use `SQLGetStmtAttr` instead. |
| [SQLSetStmtAttr (Cursor Library)](sqlsetstmtattr-cursor-library.md) | Sets a statement attribute. The cursor library processes cursor-related attributes to implement scrollable cursor support. |
| [SQLSetScrollOptions (Cursor Library)](sqlsetscrolloptions-cursor-library.md) | Sets options that control cursor behavior. This function is deprecated in ODBC 3.x; use `SQLSetStmtAttr` with cursor-related attributes instead. |
| [SQLSetConnectAttr (Cursor Library)](sqlsetconnectattr-cursor-library.md) | Sets a connection attribute. The cursor library processes the `SQL_ATTR_ODBC_CURSORS` attribute to enable or disable cursor library use. |
| [SQLSetEnvAttr (Cursor Library)](sqlsetenvattr-and-the-cursor-library.md) | Sets an environment attribute. The cursor library passes this call to the Driver Manager. |

## SQL translation and row operations

These functions handle SQL statement translation and row count retrieval.

| Function | Description |
| --- | --- |
| [SQLNativeSql (Cursor Library)](sqlnativesql-cursor-library.md) | Returns the SQL string as modified by the driver. The cursor library passes this call to the driver without modification. |
| [SQLRowCount (Cursor Library)](sqlrowcount-cursor-library.md) | Returns the number of rows affected by an UPDATE, INSERT, or DELETE statement. The cursor library tracks row counts for positioned update and delete operations. |

## Related content

- [The ODBC Cursor Library](../develop-app/the-odbc-cursor-library.md)
- [Block Cursors, Scrollable Cursors, and Backward Compatibility](block-cursors-scrollable-cursors-and-backward-compatibility.md)
- [Scrollable Cursors](../develop-app/scrollable-cursors.md)
