---
title: "SQLSetScrollOptions (Visual FoxPro ODBC Driver) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "SQLSetScrollOptions function [ODBC], Visual FoxPro ODBC Driver"
ms.assetid: 693e6e28-a845-41b1-9622-5058b0d87229
author: MightyPen
ms.author: genemi
manager: craigg
---
# SQLSetScrollOptions (Visual FoxPro ODBC Driver)
> [!NOTE]  
>  This topic contains Visual FoxPro ODBC Driver-specific information. For general information about this function, see the appropriate topic under [ODBC API Reference](../../odbc/reference/syntax/odbc-api-reference.md).  
  
 Support: Partial  
  
 ODBC API Conformance: Level 2  
  
 Sets options that control the behavior of cursors associated with a statement handle, *hstmt*.  
  
 The Visual FoxPro ODBC Driver supports only SQL_CONCUR_READ_ONLY; it does not support the *fConcurrency* value SQL_CONCUR_ROWVER. The driver converts SQL_KEYSET_SIZE, SQL_CURSOR_DYNAMIC, and SQL_CURSOR_KEYSET_DRIVEN to SQL_SCROLL_STATIC with warning ODBC_01S02.  
  
 For more information, see [SQLSetScrollOptions](../../odbc/reference/syntax/sqlsetscrolloptions-function.md) in the *ODBC Programmer's Reference*.
