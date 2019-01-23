---
title: "Bookmark C Data Type | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "C data types [ODBC], bookmark C data type"
  - "pseudo-type identifiers [ODBC], bookmark C data type"
  - "data types [ODBC], pseudo-type identifiers"
  - "bookmarks [ODBC]"
  - "bookmark C data type [ODBC]"
ms.assetid: add88e48-ada3-4c0c-a5ac-e78903d3ff41
author: MightyPen
ms.author: genemi
manager: craigg
---
# Bookmark C Data Type
The bookmark C data type allows an application to retrieve a bookmark. The bookmark C types are used only to retrieve bookmark values that can be variable in length; they should not be converted to other data types. An application retrieves a bookmark either from column 0 of the result set with **SQLBulkOperations** (with an operation of SQL_ADD), **SQLFetch**, **SQLFetchScroll**, or **SQLGetData**. For more information, see [Bookmarks](../../../odbc/reference/develop-app/bookmarks-odbc.md).  
  
 The following table lists the value of *CType* for the bookmark C data type, the ODBC C data type that implements the bookmark C data type, and the definition of this data type from SQL.H.  
  
> [!NOTE]
>  The SQL_C_BOOKMARK data type has been deprecated. ODBC 3*.x* applications should not use SQL_C_BOOKMARK. ODBC 3*.x* drivers need to support SQL_C_BOOKMARK only if they want to work with ODBC 2.*x* applications that use it. The Driver Manager maps SQL_C_VARBOOKMARK to SQL_C_BOOKMARK when an application works with an ODBC 2.*x* driver.  
  
|C type identifier|ODBC C typedef|C type|  
|-----------------------|--------------------|------------|  
|SQL_C_BOOKMARK<br />(Deprecated)|BOOKMARK|unsigned long int|  
|SQL_C_VARBOOKMARK|SQLCHAR *|unsigned char *|
