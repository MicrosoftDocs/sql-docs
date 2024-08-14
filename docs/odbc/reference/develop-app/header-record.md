---
title: "Header Record"
description: "Header Record"
author: David-Engel
ms.author: davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords:
  - "diagnostic information [ODBC], diagnostic records"
  - "header records [ODBC]"
  - "diagnostic records [ODBC]"
---
# Header Record
The fields in the header record contain general information about a function's execution, including the return code, row count, number of status records, and type of statement executed. The header record is always created unless the function returns SQL_INVALID_HANDLE. For a complete list of fields in the header record, see the [SQLGetDiagField](../../../odbc/reference/syntax/sqlgetdiagfield-function.md) function description.
