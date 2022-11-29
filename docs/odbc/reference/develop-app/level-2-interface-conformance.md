---
description: "Level 2 Interface Conformance"
title: "Level 2 Interface Conformance | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "interface conformance levels [ODBC]"
  - "level 2 interface conformance levels [ODBC]"
  - "conformance levels [ODBC], interface"
ms.assetid: 2dc87840-f2fe-43dd-9d7b-bd95523081d9
author: David-Engel
ms.author: v-davidengel
---
# Level 2 Interface Conformance
The Level 2 interface conformance level includes the Level 1 interface conformance-level functionality plus the following features:  
  
|Feature number|Description|  
|-|-|  
|201|Use three-part names of database tables and views. (For more information, see the two-part naming support feature 101 in [Level 1 Interface Conformance](../../../odbc/reference/develop-app/level-1-interface-conformance.md).)|  
|202|Describe dynamic parameters, by calling **SQLDescribeParam**.|  
|203|Use not only input parameters but also output and input/output parameters, and result values of stored procedures.|  
|204|Use bookmarks, including retrieving bookmarks, by calling **SQLDescribeCol** and **SQLColAttribute** on column number 0; fetching based on a bookmark, by calling **SQLFetchScroll** with the *FetchOrientation* argument set to SQL_FETCH_BOOKMARK; and update, delete, and fetch by bookmark operations, by calling **SQLBulkOperations** with the *Operation* argument set to SQL_UPDATE_BY_BOOKMARK, SQL_DELETE_BY_BOOKMARK, or SQL_FETCH_BY_BOOKMARK.|  
|205|Retrieve advanced information about the data dictionary, by calling **SQLColumnPrivileges**, **SQLForeignKeys**, and **SQLTablePrivileges**.|  
|206|Use ODBC functions instead of SQL statements to perform additional database operations, by calling **SQLBulkOperations** with SQL_ADD, or **SQLSetPos** with SQL_DELETE or SQL_UPDATE. (Support for calls to **SQLSetPos** with the *LockType* argument set to SQL_LOCK_EXCLUSIVE or SQL_LOCK_UNLOCK is not a part of the conformance levels but is an optional feature.)|  
|207|Enable asynchronous execution of ODBC functions for specified individual statements.|  
|208|Obtain the SQL_ROWVER row-identifying column of tables, by calling **SQLSpecialColumns**. (For more information, see the support for **SQLSpecialColumns** with the *IdentifierType* argument set to SQL_BEST_ROWID as feature 20 in [Core Interface Conformance](../../../odbc/reference/develop-app/core-interface-conformance.md).)|  
|209|Set the SQL_ATTR_CONCURRENCY statement attribute to at least one value other than SQL_CONCUR_READ_ONLY.|  
|210|The ability to time out login request and SQL queries (SQL_ATTR_LOGIN_TIMEOUT and SQL_ATTR_QUERY_TIMEOUT).|  
|211|The ability to change the default isolation level; the ability to execute transactions with the "serializable" level of isolation.|
