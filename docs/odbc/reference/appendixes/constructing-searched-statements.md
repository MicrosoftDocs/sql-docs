---
title: "Constructing Searched Statements | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "searched statements [ODBC]"
  - "ODBC cursor library [ODBC], statement processing"
  - "ODBC cursor library [ODBC], searched statements"
  - "SQL statements [ODBC], cursor library"
  - "cursor library [ODBC], statement processing"
  - "cursor library [ODBC], searched statements"
  - "SQL statements [ODBC], searched statements"
ms.assetid: e429254c-c43f-4fbf-98b2-5f1ed53501ff
author: MightyPen
ms.author: genemi
manager: craigg
---
# Constructing Searched Statements
> [!IMPORTANT]  
>  This feature will be removed in a future version of Windows. Avoid using this feature in new development work and plan to modify applications that currently use this feature. Microsoft recommends using the driver's cursor functionality.  
  
 To support positioned update and delete statements, the cursor library constructs a searched **UPDATE** or **DELETE** statement from the positioned statement. To support calls to **SQLGetData** in a block of data, the cursor library constructs a searched **SELECT** statement to create a result set containing the current row of data. In each of these statements, the **WHERE** clause enumerates the values stored in the cache for each bound column that returns SQL_PRED_SEARCHABLE or SQL_PRED_BASIC for the SQL_DESC_SEARCHABLE field identifier in **SQLColAttribute**.  
  
> [!CAUTION]  
>  The **WHERE** clause constructed by the cursor library to identify the current row can fail to identify any rows, identify a different row, or identify more than one row.  
  
 If a positioned update or delete statement affects more than one row, the cursor library updates the row status array only for the row on which the cursor is positioned and returns SQL_SUCCESS_WITH_INFO and SQLSTATE 01001 (Cursor operation conflict). If the statement does not identify any rows, the cursor library does not update the row status array and returns SQL_SUCCESS_WITH_INFO and SQLSTATE 01001 (Cursor operation conflict). An application can call **SQLRowCount** to determine the number of rows that were updated or deleted.  
  
 If the **SELECT** clause used to position the cursor for a call to **SQLGetData** identifies more than one row, **SQLGetData** is not guaranteed to return the correct data. If it does not identify any rows, **SQLGetData** returns SQL_NO_DATA.  
  
 If an application conforms to the following guidelines, the **WHERE** clause constructed by the cursor library should uniquely identify the current row, except when this is impossible, such as when the data source contains duplicate rows.  
  
-   **Bind columns that uniquely identify the row.** If the bound columns do not uniquely identify the row, the **WHERE** clause constructed by the cursor library might identify more than one row. In a positioned update or delete statement, such a clause might cause more than one row to be updated or deleted. In a call to **SQLGetData**, such a clause might cause the driver to return data for the wrong row. Binding all the columns in a unique key guarantees that each row is uniquely identified.  
  
-   **Allocate data buffers large enough that no truncation occurs.** The cursor library's cache is a copy of the values in the rowset buffers bound to the result set with **SQLBindCol**. If data is truncated when it is placed in these buffers, it will also be truncated in the cache. A **WHERE** clause constructed from truncated values might not correctly identify the underlying row in the data source.  
  
-   **Specify non-null length buffers for binary C data.** The cursor library allocates length buffers in its cache only if the *StrLen_or_IndPtr* argument in **SQLBindCol** is non-null. When the *TargetType* argument is SQL_C_BINARY, the cursor library requires the length of the binary data to construct a **WHERE** clause from the data. If there is no length buffer for a SQL_C_BINARY column and the application calls **SQLGetData** or attempts to execute a positioned update or delete statement, the cursor library returns SQL_ERROR and SQLSTATE SL014 (A positioned request was issued and not all column count fields were buffered).  
  
-   **Specify non-null length buffers for nullable columns.** The cursor library allocates length buffers in its cache only if the *StrLen_or_IndPtr* argument in **SQLBindCol** is non-null. Because SQL_NULL_DATA is stored in the length buffer, the cursor library assumes that any column for which no length buffer is specified is non-nullable. If no length column is specified for a nullable column, the cursor library constructs a **WHERE** clause that uses the data value for the column. This clause will not correctly identify the row.
