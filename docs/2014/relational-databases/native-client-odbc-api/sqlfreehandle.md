---
title: "SQLFreeHandle | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "SQLFreeHandle function"
ms.assetid: d374e5c8-ed35-43bf-8dd6-c37e38d9b5f1
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLFreeHandle
  In manual-commit mode, calling **SQLFreeHandle** on a statement handle with an open transaction causes a rollback of pending changes to the database. Calling **SQLFreeHandle** on a statement handle always closes any open cursors and discards pending results, freeing all resources associated with the statement handle.  
  
## See Also  
 [SQLFreeHandle Function](https://go.microsoft.com/fwlink/?LinkId=59345)   
 [ODBC API Implementation Details](odbc-api-implementation-details.md)  
  
  
