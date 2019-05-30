---
title: "SQLTables | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "SQLTables function"
ms.assetid: 77b6c15c-9cf7-4019-b3f0-3d27d23ef656
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLTables
  SQLTables can be executed on a static server cursor. An attempt to execute SQLTables on an updatable (dynamic or keyset) cursor will return SQL_SUCCESS_WITH_INFO indicating that the cursor type has been changed.  
  
 SQLTables reports tables from all databases when the *CatalogName* parameter is SQL_ALL_CATALOGS and all other parameters contain default values (NULL pointers).  
  
 To report available catalogs, schemas, and table types, SQLTables makes special use of empty strings (zero-length byte pointers). Empty strings are not default values (NULL pointers).  
  
 The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver supports reporting information for tables on linked servers by accepting a two-part name for the *CatalogName* parameter: *Linked_Server_Name.Catalog_Name*.  
  
 SQLTables returns information about any tables whose names match *TableName* and are owned by the current user.  
  
## SQLTables and Table-Valued Parameters  
 When the statement attribute SQL_SOPT_SS_NAME_SCOPE has the value SQL_SS_NAME_SCOPE_TABLE_TYPE, rather than its default value of SQL_SS_NAME_SCOPE_TABLE, SQLTables returns information about table types. The TABLE_TYPE value returned for a table type in column 4 of the result set returned by SQLTables is TABLE TYPE. For more information on SQL_SOPT_SS_NAME_SCOPE, see [SQLSetStmtAttr](sqlsetstmtattr.md).  
  
 Tables, views, and synonyms share a common namespace that is distinct from the namespace used by table types. Although it is not possible to have a table and a view with the same name, it is possible to have a table and a table type with the same in the same catalog and schema.  
  
 For more information about table-valued parameters, see [Table-Valued Parameters &#40;ODBC&#41;](../native-client-odbc-table-valued-parameters/table-valued-parameters-odbc.md).  
  
## Example  
  
```  
// Get a list of all tables in the current database.  
SQLTables(hstmt, NULL, 0, NULL, 0, NULL, 0, NULL,0);  
  
// Get a list of all tables in all databases.  
SQLTables(hstmt, (SQLCHAR*) "%", SQL_NTS, NULL, 0, NULL, 0, NULL,0);  
  
// Get a list of databases on the current connection's server.  
SQLTables(hstmt, (SQLCHAR*) "%", SQL_NTS, (SQLCHAR*)"", 0, (SQLCHAR*)"",  
    0, NULL, 0);  
```  
  
## See Also  
 [SQLTables Function](https://go.microsoft.com/fwlink/?LinkId=59374)   
 [ODBC API Implementation Details](odbc-api-implementation-details.md)  
  
  
