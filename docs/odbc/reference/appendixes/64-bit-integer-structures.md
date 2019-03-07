---
title: "64-Bit Integer Structures | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "C data types [ODBC], 64-bit integer structures"
  - "data types [ODBC], C data types"
  - "64-bit integer structures [ODBC]"
ms.assetid: ac80c798-d9b2-4430-85ed-bd2461db0ac7
author: MightyPen
ms.author: genemi
manager: craigg
---
# 64-Bit Integer Structures
The C type for the SQL_C_SBIGINT and SQL_C_UBIGINT data type identifiers on Microsoft C compilers is _int64. When a compiler other than a Microsoft® C compiler is used, the C type might be different. If the compiler supports 64-bit integers natively, the driver or application should define ODBCINT64 to be the native 64-bit integer type. If the compiler does not support 64-bit integers natively, an application or driver can define the following structures to ensure that it has access to this data:  
  
```  
typedef struct{  
SQLUINTEGER dwLowWord;  
SQLUINTEGER dwHighWord;  
} SQLUBIGINT  
  
typedef struct{  
SQLUINTEGER dwLowWord;  
SQLINTEGER sdwHighWord;  
} SQLBIGINT  
```  
  
 These structures should be aligned to an 8-byte boundary because a 64-bit integer is aligned to the 8-byte boundary.
