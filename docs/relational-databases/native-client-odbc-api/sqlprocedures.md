---
title: "SQLProcedures"
description: "SQLProcedures"
author: markingmyname
ms.author: maghan
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords:
  - "SQLProcedures function"
apitype: "DLLExport"
---
# SQLProcedures
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  All [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] stored procedures return a value. **SQLProcedures** reports SQL_PT_FUNCTION for the result set column PROCEDURE_TYPE.  
  
 **SQLProcedures** returns SQL_SUCCESS whether or not values exist for *CatalogName, SchemaName,* or *ProcName* parameters. **SQLFetch** returns SQL_NO_DATA when invalid values are used in these parameters.  
  
 **SQLProcedures** can be executed on a static server cursor. An attempt to execute **SQLProcedures** on an updatable (dynamic or keyset) cursor will return SQL_SUCCESS_WITH_INFO, indicating that the cursor type has been changed.  
  
 **SQLProcedures** returns information about any procedures whose names match *ProcName* and can be executed by the current user, or for which the current user has been granted VIEW DEFINITION permission.  
  
## See Also  
 [SQLProcedures Function](../../odbc/reference/syntax/sqlprocedures-function.md)   
 [ODBC API Implementation Details](../../relational-databases/native-client-odbc-api/odbc-api-implementation-details.md)  
  
