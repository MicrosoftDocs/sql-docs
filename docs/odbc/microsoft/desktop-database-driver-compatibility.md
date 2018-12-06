---
title: "Desktop Database Driver Compatibility | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "compatibility [ODBC], Unicode"
  - "Unicode [ODBC], desktop database drivers"
  - "ODBC desktop database drivers [ODBC], Unicode"
  - "backward compatibility [ODBC], Unicode"
  - "desktop database drivers [ODBC], Unicode"
  - "Jet-based ODBC drivers [ODBC], Unicode"
ms.assetid: dd695638-1a0b-4e27-8a6a-9510ebb5a5ee
author: MightyPen
ms.author: genemi
manager: craigg
---
# Desktop Database Driver Compatibility
Unicode is a method of software character encoding that treats all characters as having a fixed width of two bytes. This method is used as an alternative to Windows ANSI character encoding, which, because it represents characters in one byte, is limited to 256 characters. Because Unicode can represent over 65,000 characters, it accommodates many languages whose characters are not represented in ANSI encoding.  
  
 The ODBC 3.5 (or later) Driver Manager is Unicode-enabled. This affects two major areas: function calls and string data types. The Driver Manager maps function string arguments and string data as required by the application and driver, both of which can be either Unicode-enabled or ANSI-enabled.  
  
 The ODBC 3.5 (or later) Driver Manager supports the use of a Unicode driver with both a Unicode application and an ANSI application. It also supports the use of an ANSI driver with an ANSI application. The Driver Manager provides limited Unicode-to-ANSI mapping for a Unicode application working with an ANSI driver. This allows access to the Jet 3.5 databases and support of all existing ISAM file types.  
  
 When an ANSI application uses the ODBC Desktop Database Driver 4.0 and accesses Microsoft Access 4.0 or later, the driver exposes the data type as SQL_CHAR, SQL_VARCHAR, or SQL_LONGVARCHAR even though Jet 4.0 supports the wide version. Older versions of Jet do not support SQL_WCHAR, SQL_WVARCHAR, and SQL_WLONGVARCHAR. This restriction also applies in cases where the old formats are used with the Jet 4.0 Database Engine.  
  
 For more information concerning Unicode issues with ODBC, see [Unicode](../../odbc/reference/develop-app/unicode.md) in Programming Considerations.
