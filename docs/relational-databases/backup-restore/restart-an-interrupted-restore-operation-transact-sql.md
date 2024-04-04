---
title: "Restart interrupted restore operation"
description: This example shows you how to restart an interrupted restore operation in SQL Server using Transact-SQL.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 08/04/2023
ms.service: sql
ms.subservice: backup-restore
ms.topic: conceptual
helpviewer_keywords:
  - "interrupted restore operation"
  - "restoring databases [SQL Server], restarting interrupted operation"
  - "resetting options changed after backup"
  - "database restores [SQL Server], restarting interrupted operation"
  - "restarting interrupted restore operation"
  - "restoring interrupted operation [SQL Server]"
---
# Restart an interrupted restore operation (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

This article explains how to restart an interrupted restore operation.

### Restart an interrupted restore operation

1. Execute the interrupted `RESTORE` statement again, specifying:

   - The same clauses used in the original `RESTORE` statement.
   - The `RESTART` clause.

## Remarks

`RESTORE ... WITH RESTART` *restarts* the restore process. There's no *resume* option for an interrupted restore operation.

However, `RESTART` saves some time by skipping the analysis phase of database recovery, and in most cases, `RESTART` doesn't need to recreate the database files, which can save a significant amount of time for larger databases, especially if [Instant File Initialization](../databases/database-instant-file-initialization.md) (IFI) isn't enabled.

## Example

This example restarts an interrupted restore operation, using the example `AdventureWorks2022` database.

```sql
-- Restore a full database backup of the AdventureWorks database.
RESTORE DATABASE AdventureWorks2022
FROM DISK = 'C:\Temp\AdventureWorks2022.bak';
GO

-- The restore operation halted prematurely.
-- Repeat the original RESTORE statement specifying WITH RESTART.
RESTORE DATABASE AdventureWorks2022
FROM DISK = 'C:\Temp\AdventureWorks2022.bak'
WITH RESTART;
GO
```

## Next steps

- [Complete Database Restores (Full Recovery Model)](complete-database-restores-full-recovery-model.md)
- [Complete Database Restores (Simple Recovery Model)](complete-database-restores-simple-recovery-model.md)
- [RESTORE (Transact-SQL)](../../t-sql/statements/restore-statements-transact-sql.md)
