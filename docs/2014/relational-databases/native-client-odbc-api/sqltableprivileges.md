---
title: "SQLTablePrivileges | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "SQLTablePrivileges function"
ms.assetid: 8cce22d5-28b1-4b50-a5bc-1de03e0ffd6b
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLTablePrivileges
  **SQLTablePrivileges** can be executed on a static cursor. An attempt to execute **SQLTablePrivileges** on an updatable (keyset-driven or dynamic) returns SQL_SUCCESS_WITH_INFO indicating the cursor type has been changed.  
  
 The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver supports reporting information for tables on linked servers by accepting a two-part name for the *CatalogName* parameter: *Linked_Server_Name.Catalog_Name*.  
  
## See Also  
 [SQLTablePrivileges Function](https://go.microsoft.com/fwlink/?LinkId=59373\)   
 [ODBC API Implementation Details](odbc-api-implementation-details.md)  
  
  
