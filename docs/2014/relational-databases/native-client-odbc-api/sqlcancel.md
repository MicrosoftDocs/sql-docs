---
title: "SQLCancel | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "SQLCancel function"
ms.assetid: d4c965ae-c1ac-4e9d-b4b9-32b561401106
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLCancel
  The [SQLCancel](https://go.microsoft.com/fwlink/?LinkId=203516) topic says that in ODBC 2.x, if an application calls `SQLCancel` when no processing is being done on the statement, `SQLCancel` has the same effect as `SQLFreeStmt` with the `SQL_CLOSE` option; this behavior is defined only for completeness and applications should call `SQLFreeStmt` or `SQLCloseCursor` to close cursors. But even if your [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client application sets the ODBC API version to be 3.5.x or later, the `SQLCancel` function will use the ODBC 2.x behavior.  
  
## See Also  
 [SQLCancel](https://go.microsoft.com/fwlink/?LinkId=203516)   
 [ODBC API Implementation Details](odbc-api-implementation-details.md)  
  
  
