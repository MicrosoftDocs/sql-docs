---
title: "Statement Attributes | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL statements [ODBC], statement attributes"
  - "statement attributes [ODBC]"
ms.assetid: 4c59cd8e-a713-4095-9065-20d5bdeafe43
author: MightyPen
ms.author: genemi
manager: craigg
---
# Statement Attributes
Statement attributes are characteristics of the statement. For example, whether to use bookmarks and what kind of cursor to use with the statement's result set are statement attributes.  
  
 Statement attributes are set with **SQLSetStmtAttr** and their current settings retrieved with **SQLGetStmtAttr**. There is no requirement that an application set any statement attributes; all statement attributes have defaults, some of which are driver-specific.  
  
 When a statement attribute can be set depends on the attribute itself. The SQL_ATTR_CONCURRENCY, SQL_ATTR_CURSOR_TYPE, SQL_ATTR_SIMULATE_CURSOR, and SQL_ATTR_USE_BOOKMARKS statement attributes must be set before the statement is executed. The SQL_ATTR_ASYNC_ENABLE and SQL_ATTR_NOSCAN statement attributes can be set at any time but are not applied until the statement is used again. SQL_ATTR_MAX_LENGTH, SQL_ATTR_MAX_ROWS, and SQL_ATTR_QUERY_TIMEOUT statement attributes can be set at any time, but it is driver-specific whether they are applied before the statement is used again. The remaining statement attributes can be set at any time.  
  
> [!NOTE]  
>  The ability to set statement attributes at the connection level by calling **SQLSetConnectAttr** has been deprecated in ODBC 3.*x*. ODBC 3.*x* applications should never set statement attributes at the connection level. ODBC 3.*x* drivers need only support this functionality if they should work with ODBC 2.*x* applications. For more information, see [SQLSetConnectOption Mapping](../../../odbc/reference/appendixes/sqlsetconnectoption-mapping.md) in Appendix G: Driver Guidelines for Backward Compatibility.  
>   
>  An exception to this is the SQL_ATTR_METADATA_ID and SQL_ATTR_ASYNC_ENABLE attributes, which are both connection attributes and statement attributes and can be set either at the connection level or the statement level.  
>   
>  None of the statement attributes introduced in ODBC 3.*x* (except for SQL_ATTR_METADATA_ID) can be set at the connection level.  
  
 For more information, see the [SQLSetStmtAttr](../../../odbc/reference/syntax/sqlsetstmtattr-function.md) function description.
