---
title: "SQLTablePrivileges"
description: "SQLTablePrivileges"
author: markingmyname
ms.author: maghan
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords:
  - "SQLTablePrivileges function"
apitype: "DLLExport"
---
# SQLTablePrivileges
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  **SQLTablePrivileges** can be executed on a static cursor. An attempt to execute **SQLTablePrivileges** on an updatable (keyset-driven or dynamic) returns SQL_SUCCESS_WITH_INFO indicating the cursor type has been changed.  
  
 The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver supports reporting information for tables on linked servers by accepting a two-part name for the *CatalogName* parameter: *Linked_Server_Name.Catalog_Name*.  
  
## See Also  
 [SQLTablePrivileges Function](https://go.microsoft.com/fwlink/?LinkId=59373\)   
 [ODBC API Implementation Details](~/relational-databases/native-client-odbc-api/odbc-api-implementation-details.md)  
  
  
