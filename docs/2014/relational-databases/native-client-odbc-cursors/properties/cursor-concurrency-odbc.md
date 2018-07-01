---
title: "Cursor Concurrency (ODBC) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
helpviewer_keywords: 
  - "concurrency [ODBC]"
  - "cursors [ODBC], concurrency"
  - "ODBC cursors, concurrency"
ms.assetid: 68228ece-cbf1-4f19-bfdc-053884c1af48
caps.latest.revision: 28
author: MightyPen
ms.author: genemi
manager: craigg
---
# Cursor Concurrency (ODBC)
  Cursor operations, like cursor types, are affected by the concurrency options set by the application. Concurrency options are set using the SQL_ATTR_CONCURRENCY option of [SQLSetStmtAttr](../../native-client-odbc-api/sqlsetstmtattr.md). The concurrency types are:  
  
-   Read-only (SQL_CONCUR_READONLY)  
  
-   Values (SQL_CONCUR_VALUES)  
  
-   Row version (SQL_CONCUR_ROWVER)  
  
-   Lock (SQL_CONCUR_LOCK)  
  
## See Also  
 [Cursor Properties](cursor-properties.md)  
  
  
