---
title: "ODBC Functions Executed by the Cursor Library"
description: "ODBC Functions Executed by the Cursor Library"
author: David-Engel
ms.author: davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "cursor library [ODBC], functions"
  - "functions [ODBC], cursor library"
  - "ODBC functions [ODBC], cursor library"
  - "ODBC cursor library [ODBC], functions"
---
# ODBC Functions Executed by the Cursor Library
> [!IMPORTANT]  
>  This feature will be removed in a future version of Windows. Avoid using this feature in new development work and plan to modify applications that currently use this feature. Microsoft recommends using the driver's cursor functionality.  
  
 The cursor library executes the following functions. When an application calls a function in this list, the Driver Manager invokes the cursor library, not the driver. Note that the cursor library may call the driver when executing the function.  
  
:::row:::
   :::column span="":::
      **SQLBindCol**<br>      **SQLBindParam**<br>      **SQLBindParameter**<br>      **SQLCloseCursor**<br>      **SQLEndTran**<br>      **SQLExtendedFetch**<br>      **SQLFetchScroll**<br>      **SQLFreeHandle**<br>      **SQLFreeStmt**<br>      **SQLGetData**<br>      **SQLGetDescField**<br>      **SQLGetDescRec**<br>      **SQLGetFunctions**<br>      **SQLGetInfo**<br>      **SQLGetStmtAttr**
   :::column-end:::
   :::column span="":::
      **SQLGetStmtOption**<br>      **SQLNativeSql**<br>      **SQLNumParams**<br>      **SQLParamOptions**<br>      **SQLRowCount**<br>      **SQLSetConnectAttr**<br>      **SQLSetConnectOption**<br>      **SQLSetDescField**<br>      **SQLSetDescRec**<br>      **SQLSetPos**<br>      **SQLSetScrollOptions**<br>      **SQLSetStmtAttr**<br>      **SQLSetStmtOption**<br>      **SQLTransact**
   :::column-end:::
:::row-end:::
