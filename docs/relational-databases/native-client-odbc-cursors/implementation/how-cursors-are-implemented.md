---
description: "How Cursors Are Implemented"
title: "How Cursors Are Implemented | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "SQL Server Native Client ODBC driver, cursors"
  - "ODBC cursors, about ODBC cursors"
  - "ODBC applications, cursors"
  - "cursors [ODBC], about ODBC cursors"
ms.assetid: 2b1d7dd4-08a4-43fc-b3eb-70c183d0941f
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# How Cursors Are Implemented
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  ODBC applications control the behavior of a cursor by setting one or more statement attributes before executing an SQL statement. ODBC has two different ways to specify the characteristics of a cursor:  
  
-   Cursor type  
  
     Cursor types are set using the SQL_ATTR_CURSOR_TYPE attribute of [SQLSetStmtAttr](../../../relational-databases/native-client-odbc-api/sqlsetstmtattr.md). The ODBC cursor types are forward-only, static, keyset-driven, mixed, and dynamic. Setting the cursor type was the original method of specifying cursors in ODBC.  
  
-   Cursor behavior  
  
     Cursor behavior is set using the SQL_ATTR_CURSOR_SCROLLABLE and SQL_ATTR_CURSOR_SENSITIVITY attributes of **SQLSetStmtAttr**. These attributes are modeled on the SCROLL and SENSITIVE keywords defined for the DECLARE CURSOR statement in ISO standards. These two ISO options were introduced in ODBC version 3.0.  
  
 The characteristics of an ODBC cursor should be specified using either one or the other of these two methods, with the preference being to use the ODBC cursor types.  
  
 In addition to setting the type of a cursor, ODBC applications also set other options, such as the number of rows returned on each fetch, concurrency options, and transaction isolation levels. These options can be set for either ODBC-style cursors (forward-only, static, keyset-driven, mixed, and dynamic) or ISO style cursors (scrollability and sensitivity).  
  
 The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver supports several ways to physically implement the various types of cursors. The driver implements some types of cursors using a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] default result set; it implements others as server cursors or by using the ODBC Cursor Library.  
  
## In This Section  
  
-   [Using SQL Server Default Result Sets](../../../relational-databases/native-client-odbc-cursors/implementation/using-sql-server-default-result-sets.md)  
  
-   [Using Server Cursors](../../../relational-databases/native-client-odbc-cursors/implementation/using-server-cursors.md)  
  
-   [ODBC Cursor Library](../../../relational-databases/native-client-odbc-cursors/implementation/odbc-cursor-library.md)  
  
## See Also  
 [Using Cursors &#40;ODBC&#41;](../../../relational-databases/native-client-odbc-cursors/using-cursors-odbc.md)  
  
  
