---
title: "Arguments in Catalog Functions | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "arguments in catalog functions [ODBC]"
  - "catalog functions [ODBC], arguments"
  - "arguments in catalog functions [ODBC], about arguments"
  - "functions [ODBC], catalog functions"
ms.assetid: f5e0abec-8f24-42e0-b94f-16dd1f2004fd
author: MightyPen
ms.author: genemi
manager: craigg
---
# Arguments in Catalog Functions
All catalog functions accept arguments with which an application can restrict the scope of the data returned. For example, the first and second calls to **SQLTables** in the following code return a result set containing information about all tables, while the third call returns information about the Orders table:  
  
```  
SQLTables(hstmt1, NULL, 0, NULL, 0, NULL, 0, NULL, 0);  
SQLTables(hstmt2, NULL, 0, NULL, 0, "%", SQL_NTS, NULL, 0);  
SQLTables(hstmt3, NULL, 0, NULL, 0, "Orders", SQL_NTS, NULL, 0);  
```  
  
 Catalog function string arguments fall into four different types: ordinary argument (OA), pattern value argument (PV), identifier argument (ID), and value list argument (VL). Most string arguments can be of one of two different types, depending on the value of the SQL_ATTR_METADATA_ID statement attribute. The following table lists the arguments for each catalog function and describes the type of the argument for an SQL_TRUE or SQL_FALSE value of SQL_ATTR_METADATA_ID.  
  
|Function|Argument|Type when SQL_<br /><br /> ATTR_METADATA_<br /><br /> ID = SQL_FALSE|Type when SQL_<br /><br /> ATTR_METADATA_<br /><br /> ID = SQL_TRUE|  
|--------------|--------------|---------------------------------------------------------------|--------------------------------------------------------------|  
|**SQLColumnPrivileges**|*CatalogName* *SchemaName* *TableName* *ColumnName*|OA OA OA PV|ID ID ID ID|  
|**SQLColumns**|*CatalogName* *SchemaName* *TableName* *ColumnName*|OA PV PV PV|ID ID ID ID|  
|**SQLForeignKeys**|*PKCatalogName* *PKSchemaName* *PKTableName* *FKCatalogName* *FKSchemaName* *FKTableName*|OA OA OA OA OA OA|ID ID ID ID ID ID|  
|**SQLPrimaryKeys**|*CatalogName* *SchemaName* *TableName*|OA OA OA|ID ID ID|  
|**SQLProcedureColumns**|*CatalogName* *SchemaName* *ProcName* *ColumnName*|OA PV PV PV|ID ID ID ID|  
|**SQLProcedures**|*CatalogName* *SchemaName* *ProcName*|OA PV PV|ID ID ID|  
|**SQLSpecialColumns**|*CatalogName* *SchemaName* *TableName*|OA OA OA|ID ID ID|  
|**SQLStatistics**|*CatalogName* *SchemaName* *TableName*|OA OA OA|ID ID ID|  
|**SQLTablePrivileges**|*CatalogName* *SchemaName* *TableName*|OA PV PV|ID ID ID|  
|**SQLTables**|*CatalogName* *SchemaName* *TableName* *TableType*|PV PV PV VL|ID ID ID  VL|  
  
 This section contains the following topics.  
  
-   [Ordinary Arguments](../../../odbc/reference/develop-app/ordinary-arguments.md)  
  
-   [Pattern Value Arguments](../../../odbc/reference/develop-app/pattern-value-arguments.md)  
  
-   [Identifier Arguments](../../../odbc/reference/develop-app/identifier-arguments.md)  
  
-   [Value List Arguments](../../../odbc/reference/develop-app/value-list-arguments.md)
