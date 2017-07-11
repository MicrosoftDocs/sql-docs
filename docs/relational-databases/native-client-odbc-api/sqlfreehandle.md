---
title: "SQLFreeHandle | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apitype: "DLLExport"
helpviewer_keywords: 
  - "SQLFreeHandle function"
ms.assetid: d374e5c8-ed35-43bf-8dd6-c37e38d9b5f1
caps.latest.revision: 29
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# SQLFreeHandle
[!INCLUDE[SNAC_Deprecated](../../includes/snac-deprecated.md)]

  In manual-commit mode, calling **SQLFreeHandle** on a statement handle with an open transaction causes a rollback of pending changes to the database. Calling **SQLFreeHandle** on a statement handle always closes any open cursors and discards pending results, freeing all resources associated with the statement handle.  
  
## See Also  
 [SQLFreeHandle Function](http://go.microsoft.com/fwlink/?LinkId=59345)   
 [ODBC API Implementation Details](../../relational-databases/native-client-odbc-api/odbc-api-implementation-details.md)  
  
  