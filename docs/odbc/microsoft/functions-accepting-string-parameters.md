---
title: "Functions Accepting String Parameters"
description: "Functions Accepting String Parameters"
author: David-Engel
ms.author: davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "desktop database drivers [ODBC], string parameters"
  - "ODBC desktop database drivers [ODBC], string parameters"
  - "Jet-based ODBC drivers [ODBC], string parameters"
  - "functions [ODBC], string parameters"
  - "string parameters [ODBC]"
---
# Functions Accepting String Parameters
All functions that take string parameters will be converted to Unicode. (The "W" form of the function will be exported.) Count of bytes is converted to count of characters for those applicable ODBC APIs. This applies to the following functions:  
  
-   **SQLConnect**  
  
-   **SQLDriverConnect**  
  
-   **SQLColAttributes**  
  
-   **SQLDescribeCol**  
  
-   **SQLError** (replaced by **SQLGetDiagField**)  
  
-   **SQLExecDirect**  
  
-   **SQLGetCursorName**  
  
-   **SQLSetCursorName**  
  
-   **SQLGetStmtAttr**  
  
-   **SQLGetInfo**  
  
-   **SQLGetStmtOption** (becomes **SQLGetStmtAttr**)  
  
-   **SQLSetStmtOption** (becomes **SQLSetStmtAttr**)  
  
-   **SQLGetConnectOption**  
  
-   **SQLSetConnectOption**  
  
-   **SQLGetTypeInfo**  
  
-   **SQLStatistics**  
  
-   **SQLTables**  
  
-   **SQLNativeSQL**  
  
-   **SQLSpecialColumns**  
  
-   **ConfigDSNEx**  
  
-   **ConfigDSN**
