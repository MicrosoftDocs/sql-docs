---
title: "SQLColumnPrivileges | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "SQLColumnPrivileges function"
ms.assetid: c78acd4e-8668-4abc-9bc9-6ad381965863
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLColumnPrivileges
  **SQLColumnPrivileges** returns SQL_SUCCESS whether or not values exist for the*CatalogName*, *SchemaName*, *TableName*, or *ColumnName* parameters. **SQLFetch** returns SQL_NO_DATA when invalid values are used in these parameters.  
  
 **SQLColumnPrivileges** can be executed on a static server cursor. An attempt to execute **SQLColumnPrivileges** on an updatable (dynamic or keyset) cursor will return SQL_SUCCESS_WITH_INFO indicating that the cursor type has been changed.  
  
 The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver supports reporting information for tables on linked servers by accepting a two-part name for the *CatalogName* parameter: *Linked_Server_Name.Catalog_Name*.  
  
## See Also  
 [SQLColumnPrivileges Function](https://go.microsoft.com/fwlink/?LinkId=59335)   
 [ODBC API Implementation Details](odbc-api-implementation-details.md)  
  
  
