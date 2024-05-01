---
title: "SQLColumnPrivileges"
description: "SQLColumnPrivileges"
author: markingmyname
ms.author: maghan
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords:
  - "SQLColumnPrivileges function"
apitype: "DLLExport"
---
# SQLColumnPrivileges
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  **SQLColumnPrivileges** returns SQL_SUCCESS whether or not values exist for the*CatalogName*, *SchemaName*, *TableName*, or *ColumnName* parameters. **SQLFetch** returns SQL_NO_DATA when invalid values are used in these parameters.  
  
 **SQLColumnPrivileges** can be executed on a static server cursor. An attempt to execute **SQLColumnPrivileges** on an updatable (dynamic or keyset) cursor will return SQL_SUCCESS_WITH_INFO indicating that the cursor type has been changed.  
  
 The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver supports reporting information for tables on linked servers by accepting a two-part name for the *CatalogName* parameter: *Linked_Server_Name.Catalog_Name*.  
  
## See Also  
 [SQLColumnPrivileges Function](../../odbc/reference/syntax/sqlcolumnprivileges-function.md)   
 [ODBC API Implementation Details](../../relational-databases/native-client-odbc-api/odbc-api-implementation-details.md)  
  
