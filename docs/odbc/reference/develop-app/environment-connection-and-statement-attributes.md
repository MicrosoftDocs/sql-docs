---
description: "Environment, Connection, and Statement Attributes"
title: "Environment, Connection, and Statement Attributes | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "environment attributes [ODBC]"
  - "connection attributes [ODBC]"
  - "statement attributes [ODBC]"
ms.assetid: 9e15b276-3b7a-428a-b72f-a3ddfe1ba1ce
author: David-Engel
ms.author: v-davidengel
---
# Environment, Connection, and Statement Attributes
ODBC defines a number of attributes that are associated with environments, connections, or statements.  
  
 Environment attributes affect the entire environment, such as whether connection pooling is enabled. Environment attributes are set with **SQLSetEnvAttr** and retrieved with **SQLGetEnvAttr**.  
  
 Connection attributes affect each connection individually, such as how long a driver should wait while attempting to connect to a data source before timing out. Connection attributes are set with **SQLSetConnectAttr** and retrieved with **SQLGetConnectAttr**. For more information about connection attributes, see [Connection Attributes](../../../odbc/reference/develop-app/connection-attributes.md).  
  
 Statement attributes affect each statement individually, such as whether a statement should be executed asynchronously. Statement attributes are set with **SQLSetStmtAttr** and retrieved with **SQLGetStmtAttr**. A few statement attributes are read-only attributes and cannot be set. For example, the SQL_ATTR_ROW_NUMBER statement attribute, which is used to retrieve the number of the current row in the cursor, is read-only. For more information about statement attributes, see [Statement Attributes](../../../odbc/reference/develop-app/statement-attributes.md).  
  
 In addition to attributes defined by ODBC, a driver can define its own connection and statement attributes. Driver-defined attributes must be registered with Open Group to ensure that two driver vendors do not assign the same integer value to different, proprietary attributes. For more information, see [Driver-Specific Data Types, Descriptor Types, Information Types, Diagnostic Types, and Attributes](../../../odbc/reference/develop-app/driver-specific-data-types-descriptor-information-diagnostic.md).  
  
 For a complete list of attributes, see [SQLSetEnvAttr](../../../odbc/reference/syntax/sqlsetenvattr-function.md), [SQLSetConnectAttr](../../../odbc/reference/syntax/sqlsetconnectattr-function.md), and [SQLSetStmtAttr](../../../odbc/reference/syntax/sqlsetstmtattr-function.md). Most attributes are also described in the description of the ODBC function that they affect.
