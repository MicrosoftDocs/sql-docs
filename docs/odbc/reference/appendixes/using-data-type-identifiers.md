---
title: "Using Data Type Identifiers | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "data types [ODBC], identifiers"
  - "identifiers [ODBC], data types"
ms.assetid: 467e0c0c-a818-4737-8a24-3d8e15c7e162
author: MightyPen
ms.author: genemi
manager: craigg
---
# Using Data Type Identifiers
Applications use data type identifiers in two ways: to describe their buffers to the driver, and to retrieve metadata about the result set from the driver so that they can determine what type of C buffers to use to store the data. Applications call the following functions to perform these tasks:  
  
-   **SQLBindParameter**, **SQLBindCol**, and **SQLGetData** - to describe the C data type of application buffers.  
  
-   **SQLBindParameter** - to describe the SQL data type of dynamic parameters.  
  
-   **SQLColAttribute** and **SQLDescribeCol** - to retrieve the SQL data types of result set columns.  
  
-   **SQLDescribeParameter** - to retrieve the SQL data types of parameters.  
  
-   **SQLColumns**, **SQLProcedureColumns**, and **SQLSpecialColumns** - to retrieve the SQL data types of various schema information  
  
-   **SQLGetTypeInfo** - to retrieve a list of supported data types  
  
 Data type identifiers are stored in the SQL_DESC_CONCISE_TYPE field of a descriptor. The descriptor functions **SQLSetDescField** and **SQLSetDescRec** can be used with the appropriate types to perform the tasks listed in the previous list. For more information, see [SQLSetDescField](../../../odbc/reference/syntax/sqlsetdescfield-function.md).
