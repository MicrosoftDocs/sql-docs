---
title: "SQLPrepare (Visual FoxPro ODBC Driver)"
description: "SQLPrepare (Visual FoxPro ODBC Driver)"
author: David-Engel
ms.author: v-davidengel
ms.reviewer: randolphwest
ms.date: 12/15/2023
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "SQLPrepare function [ODBC], Visual FoxPro ODBC Driver"
---
# SQLPrepare (Visual FoxPro ODBC Driver)

> [!NOTE]  
> This article contains Visual FoxPro ODBC Driver-specific information. For general information about this function, see the appropriate article under [ODBC API Reference](../reference/syntax/odbc-api-reference.md).

## Support

Full.

## ODBC API conformance

Core level.

## Remarks

Prepares a SQL statement by planning how to optimize and execute the statement. The SQL statement is compiled for execution by [SQLExecDirect](sqlexecdirect-visual-foxpro-odbc-driver.md).

If your table, view, or field names contain spaces, enclose the names in back quote (`) marks. For example, if your database contains a table named My Table and the field My Field, enclose each element of the identifier as follows:

```sql
SELECT * FROM `My Table`.`My Field`
```

For more information, see [SQLPrepare Function](../reference/syntax/sqlprepare-function.md) in the *ODBC Programmer's Reference*.
