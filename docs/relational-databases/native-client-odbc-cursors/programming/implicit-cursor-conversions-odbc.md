---
title: "Implicit Cursor Conversions (ODBC)"
description: "Implicit Cursor Conversions (ODBC)"
author: markingmyname
ms.author: maghan
ms.date: "03/06/2017"
ms.service: sql
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords:
  - "ODBC cursors, implicit cursor conversions"
  - "implicit cursor conversions"
  - "cursors [ODBC], implicit cursor conversions"
---
# Implicit Cursor Conversions (ODBC)
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Applications can request a cursor type through [SQLSetStmtAttr](../../../relational-databases/native-client-odbc-api/sqlsetstmtattr.md) and then execute an SQL statement that is not supported by server cursors of the type requested. A call to **SQLExecute** or **SQLExecDirect** returns SQL_SUCCESS_WITH_INFO and **SQLGetDiagRec** returns:  
  
```  
szSqlState = "01S02", *pfNativeError = 0,  
szErrorMsg="[Microsoft][SQL Server Native Client] Cursor type changed"  
```  
  
 The application can determine what type of cursor is now being used by calling **SQLGetStmtOption** set to SQL_CURSOR_TYPE. The cursor type conversion applies to only one statement. The next **SQLExecDirect** or **SQLExecute** will be done using the original statement cursor settings.  
  
## See Also  
 [Cursor Programming Details &#40;ODBC&#41;](../../../relational-databases/native-client-odbc-cursors/programming/cursor-programming-details-odbc.md)  
  
  
