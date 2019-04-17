---
title: "Connection Attributes | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "data sources [ODBC], connection attributes"
  - "ODBC drivers [ODBC], connection attributes"
  - "connecting to data source [ODBC], connection attributes"
  - "connection attributes [ODBC]"
  - "connecting to driver [ODBC], connection attributes"
ms.assetid: e6d03089-30a3-4627-a642-591ba0980894
author: MightyPen
ms.author: genemi
manager: craigg
---
# Connection Attributes
Connection attributes are characteristics of the connection. For example, because transactions occur at the connection level, the transaction isolation level is a connection attribute. Similarly, the login timeout, or number of seconds to wait while trying to connect before timing out, is a connection attribute.  
  
 Connection attributes are set with **SQLSetConnectAttr** and their current settings retrieved with **SQLGetConnectAttr**. If **SQLSetConnectAttr** is called before the driver is loaded, the Driver Manager stores the attributes in its connection structure and sets them in the driver as part of the connection process. There is no requirement that an application set any connection attributes; all connection attributes have defaults, some of which are driver-specific.  
  
 A connection attribute can be set before or after connection, or either, depending on the attribute and the driver. The login timeout (SQL_ATTR_LOGIN_TIMEOUT) applies to the connection process and is effective only if set before connecting. The attributes that specify whether to use the ODBC cursor library (SQL_ATTR_ODBC_CURSORS) and the network packet size (SQL_ATTR_PACKET_SIZE) must be set before connecting, because the ODBC cursor library resides between the Driver Manager and the driver and therefore must be loaded before the driver.  
  
 The attributes to specify whether a data source is read-only or read-write (SQL_ATTR_ACCESS_MODE) and the current catalog (SQL_ATTR_CURRENT_CATALOG) can be set before or after connecting, depending on the driver. However, interoperable applications set them before connecting because some drivers do not support changing these after connecting.  
  
 Some connection attributes have a default before the connection is made, while others do not. Those that do are SQL_ATTR_ACCESS_MODE, SQL_ATTR_AUTOCOMMIT, SQL_ATTR_LOGIN_TIMEOUT, SQL_ATTR_ODBC_CURSORS, SQL_ATTR_TRACE, and SQL_ATTR_TRACEFILE.  
  
 The translation connection attributes (SQL_ATTR_TRANSLATE_DLL and SQL_ATTR_TRANSLATE_OPTION) must be set after connecting.  
  
 All other connection attributes can be set at any time. For more information, see the [SQLSetConnectAttr](../../../odbc/reference/syntax/sqlsetconnectattr-function.md) function description. (Connection attributes cannot be set on the environment level by a call to **SQLSetEnvAttr**.)
