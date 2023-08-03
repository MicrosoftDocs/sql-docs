---
title: "MSSQLSERVER_3183"
description: "MSSQLSERVER_3183"
author: MashaMSFT
ms.author: mathoma
ms.reviewer: jopilov
ms.date: 10/14/2022
ms.service: sql
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords:
  - "3183 (Database Engine error)"
---

# MSSQLSERVER_3183

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

## Details

| Attribute | Value |
| :-------- | :---- |
|Product Name|SQL Server|
|Event ID|3183|
|Event Source|MSSQLSERVER|
|Component|SQLEngine|
|Symbolic Name|LDDB_PAGE_ERROR_DURING_RESTORE|
|Message Text|RESTORE detected an error on page (%d:%d) in database "%ls" as read from the backup set.|

## Explanation

This error is raised if a page validation fails when restoring a database backup that was performed with the CHECKSUM option. SQL Server terminates the restore operation and reports an error that looks like this:

```output
Msg 3183, Level 16, State 1, Line 1
RESTORE detected an error on page (1:243) in database "corruption_errors_test" as read from the backup set.
Msg 3013, Level 16, State 1, Line 1
RESTORE DATABASE is terminating abnormally.
```

A database page could be damaged due to many reasons including hardware failures and OS issues

## User Action

- Use a last know good backup or use the CONTINUE_AFTER_ERROR clause to restore, then use DBCC CHECKDB to check. See [To specify whether a restore operation continues or stops after encountering an error](../backup-restore/specify-if-backup-or-restore-continues-or-stops-after-error.md#to-specify-whether-backup-continues-or-stops-after-an-error-is-encountered).

- Investigate the hardware where the backup was initially taken and where it was stored.  This action is to ensure no other database backups are impacted and that this issue doesn't occur in the future.
