---
description: "Supported Concurrency Model (Visual FoxPro ODBC Driver)"
title: "Supported Concurrency Model (Visual FoxPro ODBC Driver) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "Visual FoxPro ODBC driver [ODBC], concurrency"
  - "concurrency models [ODBC]"
  - "FoxPro ODBC driver [ODBC], concurrency"
ms.assetid: c39ed963-3af1-4888-8631-6083692ddcd7
author: David-Engel
ms.author: v-davidengel
---
# Supported Concurrency Model (Visual FoxPro ODBC Driver)
The Visual FoxPro ODBC Driver supports *read-only concurrency*. Your application can call [SQLSetStmtOption](../../odbc/microsoft/sqlsetstmtoption-visual-foxpro-odbc-driver.md) with a SQL_CONCURRENCY option of SQL_CONCUR_READ_ONLY.  
  
 For more information, see the [ODBC Programmer's Reference](../../odbc/reference/odbc-programmer-s-reference.md).  
  
## read-only concurrency  
 The cursor cannot be updated.  
  
## row versioning  
 Essentially timestamp support, in which row versions are compared at update time.
