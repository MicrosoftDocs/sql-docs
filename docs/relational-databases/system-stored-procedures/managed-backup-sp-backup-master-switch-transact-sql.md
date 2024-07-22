---
title: "managed_backup.sp_backup_master_switch (Transact-SQL)"
description: "Pauses or resumes the SQL Server Managed Backup to Microsoft Azure."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/31/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_ backup_master_switch"
  - "smart_admin.sp_backup_master_switch"
  - "sp_ backup_master_switch_TSQL"
  - "smart_admin.sp_backup_master_switch_TSQL"
helpviewer_keywords:
  - "sp_ backup_master_switch"
  - "smart_admin.sp_backup_master_switch"
dev_langs:
  - "TSQL"
---
# managed_backup.sp_backup_master_switch (Transact-SQL)

[!INCLUDE [sqlserver2016](../../includes/applies-to-version/sqlserver2016.md)]

Pauses or resumes the [!INCLUDE [ss-managed-backup](../../includes/ss-managed-backup-md.md)].

Use `managed_backup.sp_backup_master_switch` to temporarily pause and then resume [!INCLUDE [ss-managed-backup](../../includes/ss-managed-backup-md.md)]. This procedure makes sure that all the configurations settings remain, and are retained when the operations resume. When [!INCLUDE [ss-managed-backup](../../includes/ss-managed-backup-md.md)] is paused the retention period isn't enforced.

In other words, there's no check to determine:

- whether files should be deleted from storage
- if there are corrupted backup files
- if there's a break in the log chain.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
EXEC managed_backup.sp_backup_master_switch [ @new_state = ] { 0 | 1 }
[ ; ]
```

## Arguments

#### [ @new_state = ] { 0 | 1 }

Set the state of [!INCLUDE [ss-managed-backup](../../includes/ss-managed-backup-md.md)]. *@new_state* is **bit**. When set to a value of `0`, the operations are paused, and when set to a value of `1`, the operation resume.

## Return code values

`0` (success) or `1` (failure).

## Permissions

Requires membership in **db_backupoperator** database role, with ALTER ANY CREDENTIAL permissions, and EXECUTE permissions on `sp_delete_backuphistory` stored procedure.

## Examples

The following example can be used to pause [!INCLUDE [ss-managed-backup](../../includes/ss-managed-backup-md.md)] on the instance it is executed on:

```sql
USE msdb;
GO
EXEC managed_backup.sp_backup_master_switch @new_state = 0;
Go
```

The following example can be used to resume [!INCLUDE [ss-managed-backup](../../includes/ss-managed-backup-md.md)].

```sql
USE msdb;
GO
EXEC managed_backup.sp_backup_master_switch @new_state = 1;
Go
```

## Related content

- [SQL Server Managed Backup to Microsoft Azure](../backup-restore/sql-server-managed-backup-to-microsoft-azure.md)
