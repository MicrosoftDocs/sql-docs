---
title: SQLFreeStmt
description: SQLFreeStmt
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 10/20/2025
ms.service: sql
ms.subservice: native-client
ms.topic: reference
helpviewer_keywords:
  - "SQLFreeStmt function"
apitype: DLLExport
---
# SQLFreeStmt

[!INCLUDE [SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

Generally, `SQLFreeStmt` isn't recommended in ODBC 3.0 and later. However if the application needs to reuse the statement you should still use `SQLFreeStmt` (with the `SQL_RESET_PARAMS` and `SQL_UNBIND` options).

You might also use [SQLCloseCursor](sqlclosecursor.md), [SQLBindParameter](sqlbindparameter.md), [SQLBindCol](sqlbindcol.md), [SQLSetDescField](sqlsetdescfield.md), and [SQLFreeHandle](sqlfreehandle.md) to replace or duplicate the function of `SQLFreeStmt` and should use them instead.

In general, it's more efficient to reuse statements than to drop them and allocate new ones. However in some situations, like the reusing of statements, SQLFreeStmt still must be used.

## Related content

- [SQLFreeStmt Function](../../odbc/reference/syntax/sqlfreestmt-function.md)
- [ODBC API implementation details](odbc-api-implementation-details.md)
