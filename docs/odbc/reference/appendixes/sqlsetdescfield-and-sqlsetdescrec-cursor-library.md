---
description: "SQLSetDescField and SQLSetDescRec (Cursor Library)"
title: "SQLSetDescField and SQLSetDescRec (Cursor Library) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords: 
  - "SQLSetDescField function [ODBC], Cursor Library"
  - "SQLSetDescRec function [ODBC], Cursor Library"
ms.assetid: 4ccff067-85cd-4bfa-a6cd-7f28051fb5b9
author: David-Engel
ms.author: v-davidengel
---
# SQLSetDescField and SQLSetDescRec (Cursor Library)
> [!IMPORTANT]  
>  This feature will be removed in a future version of Windows. Avoid using this feature in new development work and plan to modify applications that currently use this feature. Microsoft recommends using the driver's cursor functionality.  
  
 This topic discusses the use of the **SQLSetDescField** and **SQLSetDescRec** functions in the cursor library. For general information about these functions, see [SQLSetDescField Function](../../../odbc/reference/syntax/sqlsetdescfield-function.md) and [SQLSetDescRec Function](../../../odbc/reference/syntax/sqlsetdescrec-function.md).  
  
 The cursor library executes **SQLSetDescField** when it is called to return the value of the fields set for bookmark columns:  
  
 SQL_DESC_DATA_PTR  
  
 SQL_DESC_INDICATOR_PTR  
  
 SQL_DESC_OCTET_LENGTH_PTR  
  
 SQL_DESC_LENGTH  
  
 SQL_DESC_OCTET_LENGTH  
  
 SQL_DESC_DATETIME_INTERVAL_CODE  
  
 SQL_DESC_SCALE  
  
 SQL_DESC_PRECISION  
  
 SQL_DESC_TYPE  
  
 SQL_DESC_NAME  
  
 SQL_DESC_UNNAMED  
  
 SQL_DESC_NULLABLE  
  
 The cursor library executes calls to **SQLSetDescRec** for a bookmark column.  
  
 When working with an ODBC *2.x* driver, the cursor library returns SQLSTATE HY090 (Invalid string or buffer length) when **SQLSetDescField** or **SQLSetDescRec** is called to set the SQL_DESC_OCTET_LENGTH field for the bookmark record of an ARD to a value not equal to 4. When working with an ODBC *3.x* driver, the cursor library allows the buffer to be any size.  
  
 The cursor library executes **SQLSetDescField** when it is called to return the value of the SQL_DESC_BIND_OFFSET_PTR, SQL_DESC_BIND_TYPE, SQL_DESC_ROW_ARRAY_SIZE, or SQL_DESC_ROW_STATUS_PTR field. These fields can be returned for any row, not just the bookmark row.  
  
 The cursor library does not execute **SQLSetDescField** to change any descriptor field other than the fields mentioned previously. If an application calls **SQLSetDescField** to set any other field while the cursor library is loaded, the call is passed through to the driver.  
  
 The cursor library supports changing the SQL_DESC_DATA_PTR, SQL_DESC_INDICATOR_PTR, and SQL_DESC_OCTET_LENGTH_PTR fields of any row of an application row descriptor dynamically (after a call to **SQLExtendedFetch**, **SQLFetch**, or **SQLFetchScroll**). The SQL_DESC_OCTET_LENGTH_PTR field can be changed to a null pointer only to unbind the length buffer for a column.  
  
 The cursor library does not support changing the SQL_DESC_BIND_TYPE field in an APD or ARD when a cursor is open. The SQL_DESC_BIND_TYPE field can be changed only after the cursor is closed and before a new cursor is opened. The only descriptor fields that the cursor library supports changing when a cursor is open are SQL_DESC_ARRAY_STATUS_PTR, SQL_DESC_BIND_OFFSET_PTR, SQL_DESC_DATA_PTR, SQL_DESC_INDICATOR_PTR, SQL_DESC_OCTET_LENGTH_PTR, and SQL_DESC_ROWS_PROCESSED_PTR.  
  
 The cursor library does not support modifying the SQL_DESC_COUNT field of the ARD after **SQLExtendedFetch** or **SQLFetchScroll** has been called and before the cursor has been closed.
