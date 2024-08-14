---
title: "Value List Arguments"
description: "Value List Arguments"
author: David-Engel
ms.author: davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords:
  - "catalog functions [ODBC], arguments"
  - "arguments in catalog functions [ODBC], value list"
  - "value list arguments [ODBC]"
---
# Value List Arguments
A value list argument consists of a list of comma-separated values to be used for matching. There is only one value list argument in the ODBC catalog functions: the *TableType* argument in **SQLTables**. Setting *TableType* to a null pointer is the same as if it is set to SQL_ALL_TABLE_TYPES, which enumerates all possible members of the value list. This argument is not affected by the SQL_ATTR_METADATA_ID statement attribute. For more information, see the [SQLTables](../../../odbc/reference/syntax/sqltables-function.md) function description.
