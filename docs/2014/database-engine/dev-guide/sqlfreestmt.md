---
title: "SQLFreeStmt | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "SQLFreeStmt function"
ms.assetid: d9666d0b-3446-480e-bf1a-10b01213e411
caps.latest.revision: 30
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# SQLFreeStmt
  **SQLFreeStmt** is not recommended in ODBC 3.0 and later. The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver supports all defined *Option* values for **SQLFreeStmt**. However, [SQLCloseCursor](../../../2014/database-engine/dev-guide/sqlclosecursor.md), [SQLBindParameter](../../../2014/database-engine/dev-guide/sqlbindparameter.md), [SQLBindCol](../../../2014/database-engine/dev-guide/sqlbindcol.md), **SQLSetDescField**, and [SQLFreeHandle](../../../2014/database-engine/dev-guide/sqlfreehandle.md) replace or duplicate the function of **SQLFreeStmt** and should be used instead.  
  
## See Also  
 [SQLFreeStmt Function](http://go.microsoft.com/fwlink/?LinkId=59346)   
 [ODBC API Implementation Details](../../../2014/database-engine/dev-guide/odbc-api-implementation-details.md)  
  
  