---
title: "Attribute Conformance | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "data sources [ODBC], conformance levels"
  - "ODBC drivers [ODBC], conformance levels"
  - "conformance levels [ODBC], attribute"
  - "attribute conformance levels [ODBC]"
ms.assetid: 34fea100-10f9-46d5-bc50-3aa867b70f24
author: MightyPen
ms.author: genemi
manager: craigg
---
# Attribute Conformance
The following table indicates the conformance level of each ODBC environment attribute, where this is well defined.  
  
|Function|Conformance level|  
|--------------|-----------------------|  
|SQL_ATTR_CONNECTION_POOLING|--[1]|  
|SQL_ATTR_CP_MATCH|--[1]|  
|SQL_ATTR_ODBC_VER|Core|  
|SQL_ATTR_OUTPUT_NTS|--[1]|  
  
 [1]   This is an optional feature and as such is not part of the conformance levels.  
  
 The following table indicates the conformance level of each ODBC connection attribute, where this is well defined.  
  
|Function|Conformance level|  
|--------------|-----------------------|  
|SQL_ATTR_ACCESS_MODE|Core|  
|SQL_ATTR_ASYNC_ENABLE|Level 1/Level 2[1]|  
|SQL_ATTR_AUTO_IPD|Level 2|  
|SQL_ATTR_AUTOCOMMIT|Level 1|  
|SQL_ATTR_CONNECTION_DEAD|Level 1|  
|SQL_ATTR_CONNECTION_TIMEOUT|Level 2|  
|SQL_ATTR_CURRENT_CATALOG|Level 2|  
|SQL_ATTR_LOGIN_TIMEOUT|Level 2|  
|SQL_ATTR_ODBC_CURSORS|Core|  
|SQL_ATTR_PACKET_SIZE|Level 2|  
|SQL_ATTR_QUIET_MODE|Core|  
|SQL_ATTR_TRACE|Core|  
|SQL_ATTR_TRACEFILE|Core|  
|SQL_ATTR_TRANSLATE_LIB|Core|  
|SQL_ATTR_TRANSLATE_OPTION|Core|  
|SQL_ATTR_TXN_ISOLATION|Level 1/Level 2[2]|  
  
 [1]   Applications that support connection-level asynchrony (required for Level 1) must support setting this attribute to SQL_TRUE by calling **SQLSetConnectAttr**; the attribute need not be settable to a value other than its default value through **SQLSetStmtAttr**. Applications that support statement-level asynchrony (required for Level 2) must support setting this attribute to SQL_TRUE using either function.  
  
 [2]   For Level 1 interface conformance, the driver must support one value in addition to the driver-defined default value (available by calling **SQLGetInfo** with the SQL_DEFAULT_TXN_ISOLATION option). For Level 2 interface conformance, the driver must also support SQL_TXN_SERIALIZABLE.  
  
 The following table indicates the conformance level of each ODBC statement attribute, where this is well defined.  
  
|Function|Conformance level|  
|--------------|-----------------------|  
|SQL_ATTR_APP_PARAM_DESC|Core|  
|SQL_ATTR_APP_ROW_DESC|Core|  
|SQL_ATTR_ASYNC_ENABLE|Level 1/Level 2[1]|  
|SQL_ATTR_CONCURRENCY|Level 1/Level 2[2]|  
|SQL_ATTR_CURSOR_SCROLLABLE|Level 1|  
|SQL_ATTR_CURSOR_SENSITIVITY|Level 2|  
|SQL_ATTR_CURSOR_TYPE|Core/Level 2[3]|  
|SQL_ATTR_ENABLE_AUTO_IPD|Level 2|  
|SQL_ATTR_FETCH_BOOKMARK_PTR|Level 2|  
|SQL_ATTR_IMP_PARAM_DESC|Core|  
|SQL_ATTR_IMP_ROW_DESC|Core|  
|SQL_ATTR_KEYSET_SIZE|Level 2|  
|SQL_ATTR_MAX_LENGTH|Level 1|  
|SQL_ATTR_MAX_ROWS|Level 1|  
|SQL_ATTR_METADATA_ID|Core|  
|SQL_ATTR_NOSCAN|Core|  
|SQL_ATTR_PARAM_BIND_OFFSET_PTR|Core|  
|SQL_ATTR_PARAM_BIND_TYPE|Core|  
|SQL_ATTR_PARAM_OPERATION_PTR|Core|  
|SQL_ATTR_PARAM_STATUS_PTR|Core|  
|SQL_ATTR_PARAMS_PROCESSED_PTR|Core|  
|SQL_ATTR_PARAMSET_SIZE|Core|  
|SQL_ATTR_QUERY_TIMEOUT|Level 2|  
|SQL_ATTR_RETRIEVE_DATA|Level 1|  
|SQL_ATTR_ROW_ARRAY_SIZE|Core|  
|SQL_ATTR_ROW_BIND_OFFSET_PTR|Core|  
|SQL_ATTR_ROW_BIND_TYPE|Core|  
|SQL_ATTR_ROW_NUMBER|Level 1|  
|SQL_ATTR_ROW_OPERATION_PTR|Level 1|  
|SQL_ATTR_ROW_STATUS_PTR|Core|  
|SQL_ATTR_ROWS_FETCHED_PTR|Core|  
|SQL_ATTR_SIMULATE_CURSOR|Level 2|  
|SQL_ATTR_USE_BOOKMARKS|Level 2|  
  
 [1]   Applications that support connection-level asynchrony (required for Level 1) must support setting this attribute to SQL_TRUE by calling **SQLSetConnectAttr**; the attribute need not be settable to a value other than its default value through **SQLSetStmtAttr**. Applications that support statement-level asynchrony (required for Level 2) must support setting this attribute to SQL_TRUE using either function.  
  
 [2]   For Level 2 interface conformance, the driver must support SQL_CONCUR_READ_ONLY and at least one other value.  
  
 [3]   For Level 1 interface conformance, the driver must support SQL_CURSOR_FORWARD_ONLY and at least one other value. For Level 2 interface conformance, the driver must support all values defined in this document.
