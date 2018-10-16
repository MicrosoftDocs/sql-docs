---
title: "Bookmark Types | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "result sets [ODBC], bookmarks"
  - "variable-length bookmarks [ODBC]"
  - "bookmarks [ODBC]"
  - "fixed-length bookmarks [ODBC]"
ms.assetid: cb2e7443-0260-4d1a-930f-0154db447979
author: MightyPen
ms.author: genemi
manager: craigg
---
# Bookmark Types
All bookmarks in ODBC 3*.x* are variable-length bookmarks. This allows a primary key or a unique index associated with a table to be used as a bookmark. The bookmark also can be a 32-bit value, as was used in ODBC 2.*x*. To specify that a bookmark is used with a cursor, an ODBC 3*.x* application sets the SQL_ATTR_USE_BOOKMARK statement attribute to SQL_UB_VARIABLE. A variable-length bookmark is automatically used.  
  
 An application can call **SQLColAttribute** with the *FieldIdentifier* argument set to SQL_DESC_OCTET_LENGTH to obtain the length of the bookmark. Because a variable-length bookmark can be a long value, an application should not bind to column 0 unless it will use the bookmark for many of the rows in the rowset.  
  
 Fixed-length bookmarks are supported only for backward compatibility. If an ODBC 2.*x* application working with an ODBC 3*.x* driver calls **SQLSetStmtOption** to set SQL_USE_BOOKMARKS to SQL_UB_ON, it is mapped in the Driver Manager to SQL_UB_VARIABLE. A variable-length bookmark is used, even if only 32 bits of it are populated. If a driver supports fixed-length bookmarks, it will support variable-length bookmarks. If an ODBC 3*.x* application working with an ODBC 2.*x* driver calls **SQLSetStmtAttr** to set SQL_ATTR_USE_BOOKMARKS to SQL_UB_VARIABLE, it is mapped in the Driver Manager to SQL_UB_ON and a 32-bit fixed-length bookmark is used. The SQL_ATTR_FETCH_BOOKMARK_PTR statement attribute must then point to a 32-bit bookmark. If the bookmarks used are longer than 32 bits, such as when primary keys are used as bookmarks, the cursor must map the actual values to 32-bit values. It could, for example, build a hash table of them. When an ODBC 3*.x* application working with an ODBC 2.*x* driver binds a bookmark, the buffer length must be 4.
