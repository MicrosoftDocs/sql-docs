---
title: "String limitations"
description: "String limitations"
author: David-Engel
ms.author: davidengel
ms.reviewer: randolphwest
ms.date: 12/14/2023
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "ODBC desktop database drivers [ODBC]"
  - "desktop database drivers [ODBC]"
---
# String limitations

The maximum length of an SQL statement string is 65,000 characters.

When the Microsoft Access driver is used, only SQL-92 string constants (with single quotation marks, not double quotation marks) are supported.

The pipe character (&#124;) can't be used in a string, whether the character is enclosed in back quotes or not.

For maximum interoperability, applications should pass strings in parameters, rather than passing quoted strings.
