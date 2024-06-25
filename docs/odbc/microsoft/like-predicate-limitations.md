---
title: "LIKE predicate limitations"
description: "LIKE predicate limitations"
author: David-Engel
ms.author: davidengel
ms.reviewer: randolphwest
ms.date: 12/14/2023
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "LIKE predicate limitations [ODBC]"
  - "ODBC SQL grammar, LIKE predicate limitations"
---
# LIKE predicate limitations

If data in a column is longer than 255 characters, the `LIKE` comparison is based only on the first 255 characters.

A `LIKE` used in a procedure is supported only with constant patterns. The Desktop Database Drivers support SQL-92 `LIKE` pattern matching.

Use of an escape clause in a `LIKE` predicate isn't supported.

A `LIKE` comparison shouldn't be performed on a column containing data of a numeric or float data type. The results might be unpredictable. For more information, see the *Microsoft Jet Database Engine Programmer's Guide*.
