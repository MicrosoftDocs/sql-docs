---
title: "SQLFreeStmt | Microsoft Docs"
ms.custom: ""
ms.date: "2015-11-23"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apitype: "DLLExport"
helpviewer_keywords: 
  - "SQLFreeStmt function"
ms.assetid: d9666d0b-3446-480e-bf1a-10b01213e411
caps.latest.revision: 35
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# SQLFreeStmt
[!INCLUDE[SNAC_Deprecated](../../includes/snac-deprecated.md)]

  Generally   
      **SQLFreeStmt** is not recommended in ODBC 3.0 and later. However if the application needs to reuse the statement you should still use **SQLFreeStmt** with the **SQL_RESET_PARAMS** and **SQL_UNBIND** options). You might also use [SQLCloseCursor](../../relational-databases/extended-stored-procedures-reference/sqlclosecursor.md), [SQLBindParameter](../../relational-databases/extended-stored-procedures-reference/sqlbindparameter.md), [SQLBindCol](../../relational-databases/extended-stored-procedures-reference/sqlbindcol.md), [SQLSetDescField](../../relational-databases/extended-stored-procedures-reference/sqlsetdescfield.md), and [SQLFreeHandle](../../relational-databases/extended-stored-procedures-reference/sqlfreehandle.md) to replace or duplicate the function of **SQLFreeStmt** and should use them instead.  
  
 In general, it is more efficient to reuse statements than to drop them and allocate new ones. However in  some situations, like the reusing of statements, SQLFreeStmt still must be used.  
  
## See Also  
 [SQLFreeStmt Function](http://go.microsoft.com/fwlink/?LinkId=59346)   
 [ODBC API Implementation Details](../../relational-databases/extended-stored-procedures-reference/odbc-api-implementation-details.md)  
  
  