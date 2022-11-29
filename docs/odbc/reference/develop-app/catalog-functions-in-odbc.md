---
description: "Catalog Functions in ODBC"
title: "Catalog Functions in ODBC | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "catalog functions [ODBC], listed"
  - "functions [ODBC], catalog functions"
ms.assetid: 4f28f557-7eca-4905-aa6d-45a6cf501a66
author: David-Engel
ms.author: v-davidengel
---
# Catalog Functions in ODBC
ODBC contains the following catalog functions:  
  
|Function|Description|  
|--------------|-----------------|  
|**SQLTables**|Returns a list of catalogs, schemas, tables, or table types in the data source.|  
|**SQLColumns**|Returns a list of columns in one or more tables.|  
|**SQLStatistics**|Returns a list of statistics about a single table. Also returns a list of indexes associated with that table.|  
|**SQLSpecialColumns**|Returns a list of columns that uniquely identifies a row in a single table. Also returns a list of columns in that table that are automatically updated.|  
|**SQLPrimaryKeys**|Returns a list of columns that compose the primary key of a single table.|  
|**SQLForeignKeys**|Returns a list of foreign keys in a single table or a list of foreign keys in other tables that refer to a single table.|  
|**SQLTablePrivileges**|Returns a list of privileges associated with one or more tables.|  
|**SQLColumnPrivileges**|Returns a list of privileges associated with one or more columns in a single table.|  
|**SQLProcedures**|Returns a list of procedures in the data source.|  
|**SQLProcedureColumns**|Returns a list of input and output parameters, the return value, and the columns in the result set of a single procedure.|  
|**SQLGetTypeInfo**|Returns a list of the SQL data types supported by the data source. These data types are generally used in **CREATE TABLE** and **ALTER TABLE** statements.|  
  
 Because **SQLTables**, **SQLColumns**, **SQLStatistics**, and **SQLSpecialColumns** conform to the Open Group CLI, and **SQLGetTypeInfo** conforms to the ISO 92 CLI, they are implemented by most drivers. The remaining catalog functions are in the ODBC conformance level.  
  
 This section contains the following topics.  
  
-   [Data Returned by Catalog Functions](../../../odbc/reference/develop-app/data-returned-by-catalog-functions.md)  
  
-   [Arguments in Catalog Functions](../../../odbc/reference/develop-app/arguments-in-catalog-functions.md)  
  
-   [Schema Views](../../../odbc/reference/develop-app/schema-views.md)
