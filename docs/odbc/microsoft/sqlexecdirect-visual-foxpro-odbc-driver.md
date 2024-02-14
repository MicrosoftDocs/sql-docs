---
title: "SQLExecDirect (Visual FoxPro ODBC Driver)"
description: "SQLExecDirect (Visual FoxPro ODBC Driver)"
author: David-Engel
ms.author: v-davidengel
ms.reviewer: randolphwest
ms.date: 12/15/2023
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords:
  - "SQLExecDirect function [ODBC], Visual FoxPro ODBC Driver"
---
# SQLExecDirect (Visual FoxPro ODBC Driver)

> [!NOTE]  
> This article contains Visual FoxPro ODBC Driver-specific information. For general information about this function, see the appropriate article under [ODBC API Reference](../reference/syntax/odbc-api-reference.md).

## Support

Full.

## ODBC API conformance

Core level.

## Remarks

Executes a new, [preparable SQL statement](visual-foxpro-terminology.md). The Visual FoxPro ODBC Driver uses the current values of the parameter marker variables if any parameters exist in the statement.

To create a batch command to submit more than one SQL statement at a time, use a semicolon (`;`) to separate each SQL statement in the batch.

If your table, view, or field names contain spaces, enclose the names in back quote marks. For example, if your database contains a table named My Table and the field My Field, enclose each element of the identifier as follows:

```sql
SELECT `My Table`.`Field1`, `My Table`.`Field2` FROM `My Table`;
```

For more information, see [SQLExecDirect Function](../reference/syntax/sqlexecdirect-function.md) in the *ODBC Programmer's Reference*.
