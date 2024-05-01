---
title: "SQLFreeHandle"
description: "SQLFreeHandle"
author: markingmyname
ms.author: maghan
ms.date: "03/03/2017"
ms.service: sql
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords:
  - "SQLFreeHandle function"
apitype: "DLLExport"
---
# SQLFreeHandle
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  In manual-commit mode, calling **SQLFreeHandle** on a statement handle with an open transaction causes a rollback of pending changes to the database. Calling **SQLFreeHandle** on a statement handle always closes any open cursors and discards pending results, freeing all resources associated with the statement handle.  
  
## See Also  
 [SQLFreeHandle Function](../../odbc/reference/syntax/sqlfreehandle-function.md)   
 [ODBC API Implementation Details](../../relational-databases/native-client-odbc-api/odbc-api-implementation-details.md)  
  
