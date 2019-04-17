---
title: "Descriptors and Desktop Database Drivers | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "desktop database drivers [ODBC], descriptors"
  - "Jet-based ODBC drivers [ODBC], descriptors"
  - "descriptors [ODBC], Jet-supported descriptor fields"
  - "ODBC desktop database drivers [ODBC], descriptors"
ms.assetid: 9ae2d9b5-365f-4f0a-9116-defe9498b401
author: MightyPen
ms.author: genemi
manager: craigg
---
# Descriptors and Desktop Database Drivers
A descriptor is a data structure that holds information about either column data or dynamic parameters. **SQLGetDescField** can be used to retrieve the supported descriptors listed below. Implementation Parameter Descriptors (IPD) are not automatically populated because **SQLDescribeParam** is not supported. Descriptor fields that are not available through Jet (such as SQL_DESC_BASE_TABLE_NAME) are also not supported.  
  
 For more information about Jet-supported descriptor fields, see the *Microsoft Jet Database Engine Programmer's Guide*.  
  
 For more information about descriptors, see the topics under "Descriptors" in the *ODBC Programmer's Reference*.  
  
|Descriptor fields|Support level|  
|-----------------------|-------------------|  
|SQL_DESC_ALLOC_TYPE|Supported|  
|SQL_DESC_ARRAY_SIZE|Supported only for ARD|  
|SQL_DESC_ARRAY_STATUS_PTR|Supported|  
|SQL_DESC_BIND_OFFSET_PTR|Supported|  
|SQL_DESC_BIND_TYPE|Supported|  
|SQL_DESC_COUNT|Supported|  
|SQL_DESC_ROWS_PROCESSED_PTR|Supported only for ARD|  
|SQL_DESC_AUTO_UNIQUE_VALUE|Supported|  
|SQL_DESC_BASE_COLUMN_NAME|Supported (NEW)|  
|SQL_DESC_BASE_TABLE_NAME|Supported (NEW)|  
|SQL_DESC_CASE_SENSITIVE|Always FALSE|  
|SQL_DESC_CATALOG_NAME|Not supported|  
|SQL_DESC_CONCISE_TYPE|Supported|  
|SQL_DESC_DATA_PTR|Supported|  
|SQL_DESC_DATETIME_INTERVAL_CODE|Supported|  
|SQL_DESC_DATETIME_INTERVAL_PRECISION|Supported for INTERVAL C types|  
|SQL_DESC_DISPLAY_SIZE|Supported|  
|SQL_DESC_FIXED_PREC_SCALE|Supported (TRUE for money)|  
|SQL_DESC_INDICATOR_PTR|Supported|  
|SQL_DESC_LABEL|Supported|  
|SQL_DESC_LENGTH|Supported|  
|SQL_DESC_LITERAL_PREFIX|Supported|  
|SQL_DESC_LITERAL_SUFFIX|Supported|  
|SQL_DESC_LOCAL_TYPE_NAME|Not supported (returns EMPTY string)|  
|SQL_DESC_NAME|Supported|  
|SQL_DESC_NULLABLE|Supported<br /><br /> **Note** Unsupported in versions preceding Jet 4.0|  
|SQL_DESC_NUM_PREC_RADIX|Supported|  
|SQL_DESC_OCTET_LENGTH|Supported|  
|SQL_DESC_OCTET_LENGTH_PTR|Supported|  
|SQL_DESC_PARAMETER_TYPE|Only input parameters|  
|SQL_DESC_PRECISION|Supported|  
|SQL_DESC_SCALE|Supported|  
|SQL_DESC_SCHEMA_NAME|Not supported|  
|SQL_DESC_SEARCHABLE|Supported|  
|SQL_DESC_TABLE_NAME|Not supported|  
|SQL_DESC_TYPE|Supported|  
|SQL_DESC_TYPE_NAME|Supported|  
|SQL_DESC_UNNAMED|Supported|  
|SQL_DESC_UNSIGNED|Supported|  
|SQL_DESC_UPDATABLE|Supported|
