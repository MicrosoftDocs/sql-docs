---
title: "sysopentapes (Transact-SQL)"
description: sysopentapes (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: 05/26/2026
ai-usage: ai-assisted
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sysopentapes"
  - "sysopentapes_TSQL"
helpviewer_keywords:
  - "backup media [SQL Server], sysopentapes system table"
  - "sysopentapes system table"
dev_langs:
  - "TSQL"
---
# sysopentapes (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Contains one row for each currently open tape device. This view is stored in the `master` database.

> [!IMPORTANT]
> This system table appears as a view for backward compatibility. Instead, use the [sys.dm_io_backup_tapes (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sys-dm-io-backup-tapes-transact-sql.md) dynamic management view.

> [!NOTE]
> You can't drop the `sysopentapes` view.

## Columns

|Column name|Data type|Description|
|-----------------|---------------|-----------------|
|`openTape`|`nvarchar(64)`|Physical file name of an open tape device. For more information about opening and releasing tape devices, see [BACKUP (Transact-SQL)](../../t-sql/statements/backup-transact-sql.md) and [RESTORE (Transact-SQL)](../../t-sql/statements/restore-statements-transact-sql.md).|

## Permissions

The user needs `VIEW SERVER STATE` permission on the server.

### Permissions for SQL Server 2022 and later

Requires `VIEW SERVER PERFORMANCE STATE` permission on the server.

## Related content

- [sys.dm_io_backup_tapes (Transact-SQL)](../../relational-databases/system-dynamic-management-views/sys-dm-io-backup-tapes-transact-sql.md)
- [BACKUP (Transact-SQL)](../../t-sql/statements/backup-transact-sql.md)
- [RESTORE statements (Transact-SQL)](../../t-sql/statements/restore-statements-transact-sql.md)
- [Mapping system tables to system views (Transact-SQL)](../../relational-databases/system-tables/mapping-system-tables-to-system-views-transact-sql.md)
- [System tables (Transact-SQL)](../../relational-databases/system-tables/system-tables-transact-sql.md)
