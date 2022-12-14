---
description: "Desktop Database Driver Performance Issues"
title: "Desktop Database Driver Performance Issues | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "ODBC desktop database drivers [ODBC], performance"
  - "desktop database drivers [ODBC], performance"
  - "Jet-based ODBC drivers [ODBC], performance"
ms.assetid: 1a4c4b7e-9744-411f-9b6e-06dfdad92cf7
author: David-Engel
ms.author: v-davidengel
---
# Desktop Database Driver Performance Issues
To ensure compatibility with existing ANSI applications, the SQL_WCHAR, SQL_WVARCHAR, and SQL_WLONGVARCHAR data types are exposed as SQL_CHAR, SQL_VARCHAR, and SQL_LONGVARCHAR for Microsoft Access 4.0 or higher data sources. The data sources do not return WIDE CHAR data types but the data still must be sent to Jet in Wide Char form. It is important to understand that conversion will take place if a SQL_C_CHAR parameter or result column is bound to a SQL_CHAR data type in an ANSI application.  
  
 This conversion can be especially inefficient in terms of memory when a SQL_C_CHAR type is bound to a parameter of type LONGVARCHAR. Since the Jet 4.0 engine is unable to stream LONGTEXT parameter data, a UNICODE conversion buffer must be allocated that is twice the size of the SQL_C_CHAR ANSI buffer. The most efficient mechanism is for the application to perform the UNICODE conversion and bind the parameter as type SQL_C_WCHAR. When a parameter is marked as data-at-execution and the data is supplied in multiple calls to SQLPutData, a longtext data buffer is grown. One way to avoid the expense of growing this "Put Data" buffer is to supply an optional length via SQL_DATA_AT_EXEC_LEN(x), where *x* is the expected length of bytes. This will initialize the size of an internal PutData buffer to *x* bytes.  
  
> [!NOTE]  
>  An efficient way to insert or update long data can be accomplished using **SQLBulkOperations()** or **SQLSetPos()** and setting the long data to SQL_DATA_AT_EXEC. (EXEC_LEN is ignored in this case.) Data can be streamed in chunks by calling **SQLPutData** multiple times, which will effectively append the data to the table.  
  
 When an application using a Jet 3.5 database through the Microsoft ODBC Desktop Database Drivers is upgraded to version 4.0, some performance degradation and an increased working set size may occur. This is because when a version 3.*x* database is opened using the new version 4.0 driver, it loads Jet 4.0. When Jet 4.0 opens the database and sees that the database is a 3.*x* version, it loads an Installable ISAM driver that is equivalent to loading the Jet 3.5 engine as well. To remove the performance and size penalty, the Jet 3.*x* database should be compacted into a Jet 4.0 format database. This will eliminate loading two Jet engines and minimize the code path to the data.  
  
 Also, the Jet 4.0 engine is a Unicode engine. All strings are stored and manipulated in Unicode. When an ANSI application accesses a Jet 3.*x* database through the Jet 4.0 engine, the data is converted from ANSI to Unicode and back to ANSI. If the database is updated to the version 4.0 format, the strings are converted to Unicode, removing one level of string conversion as well as minimizing the code path to the data by going through only one Jet engine.
