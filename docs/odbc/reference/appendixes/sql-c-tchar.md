---
title: "SQL_C_TCHAR"
description: "SQL_C_TCHAR"
author: David-Engel
ms.author: davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "sql_c_tchar [ODBC]"
  - "pseudo-type identifiers [ODBC], SQL_C_TCHAR"
  - "data types [ODBC], pseudo-type identifiers"
---
# SQL_C_TCHAR
The SQL_C_TCHAR type identifier does not actually identify a data type; it is a macro that exists within the header file for Unicode conversion. It is replaced by SQL_C_CHAR or SQL_C_WCHAR depending on the setting of the UNICODE **#define**. It is useful for an application transferring character data that is compiled as both an ANSI and a Unicode application.
