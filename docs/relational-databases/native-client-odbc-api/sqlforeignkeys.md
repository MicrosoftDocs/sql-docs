---
title: "SQLForeignKeys"
description: "SQLForeignKeys"
author: markingmyname
ms.author: maghan
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords:
  - "SQLForeignKeys function"
apitype: "DLLExport"
---
# SQLForeignKeys
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] supports cascading updates and deletes through the foreign key constraint mechanism. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] returns SQL_CASCADE for UPDATE_RULE and/or DELETE_RULE columns if CASCADE option is specified on the ON UPDATE and/or ON DELETE clause of the FOREIGN KEY constraints. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] returns SQL_NO_ACTION for UPDATE_RULE and/or DELETE_RULE columns if NO ACTION option is specified on the ON UPDATE and/or ON DELETE clause of the FOREIGN KEY constraints.  
  
 When invalid values are present in any **SQLForeignKeys** parameter, **SQLForeignKeys** returns SQL_SUCCESS on execution. **SQLFetch** returns SQL_NO_DATA when invalid values are used in these parameters.  
  
 **SQLForeignKeys** can be executed on a static server cursor. An attempt to execute **SQLForeignKeys** on an updatable (dynamic or keyset) cursor returns SQL_SUCCESS_WITH_INFO indicating that the cursor type has been changed.  
  
 The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver supports reporting information for tables on linked servers by accepting a two-part name for the *FKCatalogName* and *PKCatalogName* parameters: *Linked_Server_Name.Catalog_Name*.  
  
## See Also  
 [SQLForeignKeys Function](../../odbc/reference/syntax/sqlforeignkeys-function.md)   
 [ODBC API Implementation Details](../../relational-databases/native-client-odbc-api/odbc-api-implementation-details.md)  
  
