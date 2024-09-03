---
title: "DBCC OUTPUTBUFFER (Transact-SQL)"
description: DBCC OUTPUTBUFFER returns the current output buffer in hexadecimal and ASCII format for the specified session.
author: rwestMSFT
ms.author: randolphwest
ms.date: 12/05/2022
ms.service: sql
ms.subservice: t-sql
ms.topic: "language-reference"
f1_keywords:
  - "DBCC OUTPUTBUFFER"
  - "OUTPUTBUFFER_TSQL"
  - "OUTPUTBUFFER"
  - "DBCC_OUTPUTBUFFER_TSQL"
helpviewer_keywords:
  - "DBCC OUTPUTBUFFER statement"
  - "output buffers"
  - "current output buffer"
dev_langs:
  - "TSQL"
---
# DBCC OUTPUTBUFFER (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

Returns the current output buffer in hexadecimal and ASCII format for the specified *session_id*.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
DBCC OUTPUTBUFFER ( session_id [ , request_id ] )
[ WITH NO_INFOMSGS ]
```

## Arguments

#### *session_id*

The session ID associated with each active primary connection.

#### *request_id*

The exact request (batch) to search for within the current session.

The following query returns *request_id*:

```sql
SELECT request_id
FROM sys.dm_exec_requests
WHERE session_id = @@spid;
```

#### WITH

Allows for options to be specified.

#### NO_INFOMSGS

Suppresses all informational messages that have severity levels from 0 through 10.

## Remarks

`DBCC OUTPUTBUFFER` displays the results sent to the specified client (*session_id*). For processes that don't contain output streams, an error message is returned.

To show the statement executed that returned the results displayed by `DBCC OUTPUTBUFFER`, execute `DBCC INPUTBUFFER`.

## Result sets

`DBCC OUTPUTBUFFER` returns the following (values may vary):

```output
Output Buffer
------------------------------------------------------------------------
01fb8028:  04 00 01 5f 00 00 00 00 e3 1b 00 01 06 6d 00 61  ..._.........m.a
01fb8038:  00 73 00 74 00 65 00 72 00 06 6d 00 61 00 73 00  .s.t.e.r..m.a.s.
'...'
01fb8218:  04 17 00 00 00 00 00 d1 04 18 00 00 00 00 00 d1  ................
01fb8228:   .
  
(33 row(s) affected)
  
DBCC execution completed. If DBCC printed error messages, contact your system administrator.
```

## Permissions

Requires membership in the **sysadmin** fixed server role.

## Examples

The following example returns current output buffer information for an assumed session ID of `52`.

```sql
DBCC OUTPUTBUFFER (52);
```

## See also

- [DBCC (Transact-SQL)](../../t-sql/database-console-commands/dbcc-transact-sql.md)
- [sp_who (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-who-transact-sql.md)
- [Trace Flags (Transact-SQL)](../../t-sql/database-console-commands/dbcc-traceon-trace-flags-transact-sql.md)
