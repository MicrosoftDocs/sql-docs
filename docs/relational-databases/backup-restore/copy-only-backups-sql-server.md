---
title: Copy-only backups
description: A copy-only backup is a SQL Server backup that is independent of the sequence of SQL Server backups. It doesn't affect how later backups are restored.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 09/22/2023
ms.service: sql
ms.subservice: backup-restore
ms.topic: conceptual
helpviewer_keywords:
  - "copy-only backups [SQL Server]"
  - "COPY_ONLY option [BACKUP statement]"
  - "backups [SQL Server], copy-only backups"
monikerRange: "=azuresqldb-mi-current || >=sql-server-2016 || >=sql-server-linux-2017"
---
# Copy-only backups

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

A *copy-only backup* is a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] backup that is independent of the sequence of conventional [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] backups. Usually, taking a backup changes the database and affects how later backups are restored. However, occasionally, it's useful to take a backup for a special purpose without affecting the overall backup and restore procedures for the database. Copy-only backups serve this purpose.

The types of copy-only backups are as follows:

- **Copy-only full backups (all recovery models)**

  - A copy-only backup can't serve as a differential base or differential backup and doesn't affect the differential base.

  - Restoring a copy-only full backup is the same as restoring any other full backup.

- **Copy-only log backups (full recovery model and bulk-logged recovery model only)**

  - A copy-only log backup preserves the existing log archive point and, therefore, doesn't affect the sequencing of regular log backups. Copy-only log backups are typically unnecessary. Instead, you can create a new routine log backup (using `WITH NORECOVERY`) and use that backup together with any previous log backups that are required for the restore sequence. However, a copy-only log backup can sometimes be useful for performing an online restore. For more information, follow the instructions in the article [Example: Online restore of a read-write file (full recovery model)](example-online-restore-of-a-read-write-file-full-recovery-model.md), using the copy-only backup files instead.

  - The transaction log is never truncated after a copy-only backup.

Copy-only backups are recorded in the `is_copy_only` column of the [backupset](../../relational-databases/system-tables/backupset-transact-sql.md) table.

> [!IMPORTANT]  
> In [!INCLUDE [ssazuremi_md](../../includes/ssazuremi_md.md)], copy-only backups can't be created for a database encrypted with [service-managed Transparent Data Encryption (TDE)](/azure/sql-database/transparent-data-encryption-azure-sql?tabs=azure-portal#service-managed-transparent-data-encryption). Service-managed TDE uses internal key for encryption of data, and that key can't be exported, so you couldn't restore the backup anywhere else. Consider using [customer-managed TDE](/azure/sql-database/transparent-data-encryption-byok-azure-sql) instead to be able to create copy-only backups of encrypted databases, but make sure to have encryption key available for later restore.

## Create a copy-only backup

You can create a copy-only backup with [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)], [!INCLUDE [name-sos-short](../../includes/name-sos-short.md)], [!INCLUDE [tsql](../../includes/tsql-md.md)], or PowerShell.

### <a id="SSMSProcedure"></a> A. Use SQL Server Management Studio

In this example, a copy-only backup of the `Sales` database is backed up to disk at the default backup location.

1. In **Object Explorer**, connect to an instance of the SQL Server Database Engine and then expand that instance.

1. Expand **Databases**, right-click `Sales`, point to **Tasks**, and then select **Back Up...**.

1. On the **General** page in the **Source** section, check the **Copy-only backup** checkbox.

1. Select **OK**.

### <a id="TsqlProcedure"></a> B. Use Transact-SQL

This example creates a copy-only backup for the `Sales` database utilizing the `COPY_ONLY` parameter. A copy-only backup of the transaction log is taken as well.

```sql
BACKUP DATABASE Sales
TO DISK = 'E:\BAK\Sales_Copy.bak'
WITH COPY_ONLY;

BACKUP LOG Sales
TO DISK = 'E:\BAK\Sales_LogCopy.trn'
WITH COPY_ONLY;
```

> [!NOTE]  
> `COPY_ONLY` has no effect when specified with the `DIFFERENTIAL` option.

### <a id="PowerShellProcedure"></a> C. Use PowerShell

This example creates a copy-only backup for the `Sales` database utilizing the `-CopyOnly` parameter.

```powershell
Backup-SqlDatabase -ServerInstance 'SalesServer' -Database 'Sales' -BackupFile 'E:\BAK\Sales_Copy.bak' -CopyOnly
```

## Related tasks

### Create a full or log backup

- [Create a Full Database Backup (SQL Server)](create-a-full-database-backup-sql-server.md)
- [Back Up a Transaction Log (SQL Server)](back-up-a-transaction-log-sql-server.md)

### View copy-only backups

- [backupset (Transact-SQL)](../../relational-databases/system-tables/backupset-transact-sql.md)

### Set up and use the SQL Server PowerShell provider

- [SQL Server PowerShell Provider](../../powershell/sql-server-powershell-provider.md)

## Related content

- [Backup Overview (SQL Server)](backup-overview-sql-server.md)
- [Recovery Models (SQL Server)](recovery-models-sql-server.md)
- [Copy Databases with Backup and Restore](../../relational-databases/databases/copy-databases-with-backup-and-restore.md)
- [Restore and Recovery Overview (SQL Server)](restore-and-recovery-overview-sql-server.md)
- [BACKUP (Transact-SQL)](../../t-sql/statements/backup-transact-sql.md)
- [Backup-SqlDatabase](/powershell/module/sqlserver/backup-sqldatabase)
