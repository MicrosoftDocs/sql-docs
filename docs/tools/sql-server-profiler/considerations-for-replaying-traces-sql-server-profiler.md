---
title: Considerations for Replaying Traces
titleSuffix: SQL Server Profiler
description: Find out which operations, stored procedures, templates, and log activities prevent SQL Server Profiler from replaying traces.
author: rwestMSFT
ms.author: randolphwest
ms.date: 06/05/2025
ms.service: sql
ms.subservice: profiler
ms.topic: concept-article
ms.collection:
  - data-tools
---

# Considerations for replaying traces (SQL Server Profiler)

[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

[!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)] can't replay the following kinds of traces:

- Traces that contain transactional replication and other transaction log activity. These events are skipped. Other types of replication don't mark the transaction log so they aren't affected.

- Traces that contain operations that involve globally unique identifiers (GUID). These events will be skipped.

- Traces that contain operations on **text**, **ntext**, and **image** columns involving the **bcp** utility, the `BULK INSERT`, `READTEXT`, `WRITETEXT`, and `UPDATETEXT` statements, and full-text operations. These events are skipped.

- Traces that contain session binding: `sp_getbindtoken` and `sp_bindsession` system stored procedures. These events are skipped.

If you don't use the preconfigured replay template (**TSQL_Replay**), and don't capture all required data, [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)] doesn't replay the trace. For more information, see [Replay Requirements](replay-requirements.md).

For information about what permissions are required to replay a trace, see [Permissions required to run SQL Server Profiler](permissions-required-to-run-sql-server-profiler.md).

## Related content

- [bcp utility](../bcp/bcp-utility.md)
- [SQL Server Event Class Reference](../../relational-databases/event-classes/sql-server-event-class-reference.md)
- [sp_getbindtoken (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-getbindtoken-transact-sql.md)
- [sp_bindsession (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-bindsession-transact-sql.md)
- [BULK INSERT (Transact-SQL)](../../t-sql/statements/bulk-insert-transact-sql.md)
- [READTEXT (Transact-SQL)](../../t-sql/queries/readtext-transact-sql.md)
- [WRITETEXT (Transact-SQL)](../../t-sql/queries/writetext-transact-sql.md)
- [UPDATETEXT (Transact-SQL)](../../t-sql/queries/updatetext-transact-sql.md)
