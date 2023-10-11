---
title: "MSSQLSERVER_7357"
description: "MSSQLSERVER_7357"
author: PiJoCoder
ms.author: jopilov
ms.reviewer: jopilov, aartigoyle, v-sidong
ms.date: 08/08/2023
ms.service: sql
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords:
  - "7357 (Database Engine error)"
---
# MSSQLSERVER_7357

| Attribute | Value |
| :--- | :--- |
| Product Name | SQL Server |
| Event ID | 7357 |
| Event Source | MSSQLSERVER |
| Component | SQLEngine |
| Symbolic Name | RMT_ZERO_COL_OBJECT |
| Message Text | Cannot process the object "%ls". The OLE DB provider "%ls" for linked server "%ls" indicates that either the object has no columns or the current user does not have permissions on that object. |

## Explanation

The error typically occurs when there's an issue with the query passed to the [Open Query statement](../../t-sql/functions/openquery-transact-sql.md).

## User action

Review the following potential causes and recommended solutions for this error.

### OPENQUERY doesn't return a result set

- Use four-part names (`linked_server_name.catalog.schema.object_name`) to perform insert, update, or delete operations.
- Reference the `OPENQUERY` function as the target table of an `INSERT`, `UPDATE`, or `DELETE` statement, depending on the capabilities of the OLE DB provider, as documented in the [Examples](../../t-sql/functions/openquery-transact-sql.md#examples) section of "OPENQUERY (Transact-SQL)."

### The OLEDB provider for a pass-through query returns zero columns

- Examine and correct the pass-through query text to ensure it returns valid columns from the remote data source.
- Execute the pass-through query directly against the remote data source using the client tools provided for that data source and ensure at least one valid column is returned. For examples of pass-through queries, see [OPENQUERY (Transact-SQL)](../../t-sql/functions/openquery-transact-sql.md#examples).
- Use a four-part linked server query as an alternative `linked_server_name.database.schema.object`.

### The first line in the query is a comment

- Move the comment to the end of the query or procedure.
