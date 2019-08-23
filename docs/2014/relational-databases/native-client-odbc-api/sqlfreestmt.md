---
title: "SQLFreeStmt | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "SQLFreeStmt function"
ms.assetid: d9666d0b-3446-480e-bf1a-10b01213e411
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLFreeStmt
  **SQLFreeStmt** is not recommended in ODBC 3.0 and later. The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver supports all defined *Option* values for **SQLFreeStmt**. However, [SQLCloseCursor](sqlclosecursor.md), [SQLBindParameter](sqlbindparameter.md), [SQLBindCol](sqlbindcol.md), **SQLSetDescField**, and [SQLFreeHandle](sqlfreehandle.md) replace or duplicate the function of **SQLFreeStmt** and should be used instead.  
  
## See Also  
 [SQLFreeStmt Function](https://go.microsoft.com/fwlink/?LinkId=59346)   
 [ODBC API Implementation Details](odbc-api-implementation-details.md)  
  
  
