---
title: "SQLStatistics"
description: "SQLStatistics"
author: markingmyname
ms.author: maghan
ms.date: "03/04/2017"
ms.service: sql
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords:
  - "SQLStatistics function"
apitype: "DLLExport"
---
# SQLStatistics
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  **SQLStatistics** can be executed on a static cursor. An attempt to execute **SQLStatistics** on an updatable (keyset-driven or dynamic) returns SQL_SUCCESS_WITH_INFO indicating the cursor type is changed.  
  
## See Also  
 [SQLStatistics Function](../../odbc/reference/syntax/sqlstatistics-function.md)   
 [ODBC API Implementation Details](../../relational-databases/native-client-odbc-api/odbc-api-implementation-details.md)  
  
