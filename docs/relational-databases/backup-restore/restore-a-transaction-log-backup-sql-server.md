---
title: Restore a Transaction Log Backup (SQL Server)
description: This article describes how to restore a transaction log backup in SQL Server by using SQL Server Management Studio or Transact-SQL.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 10/14/2026
ms.service: sql
ms.subservice: backup-restore
ms.topic: how-to
f1_keywords:
  - "sql13.swb.restoretlog.general.f1"
  - "sql13.swb.restoretlog.options.f1"
helpviewer_keywords:
  - "restore log"
  - "backing up transaction logs [SQL Server], restoring"
  - "transaction log backups [SQL Server], restoring"
  - "restoring transaction logs [SQL Server], restoring backups"
  - "transaction log restores [SQL Server], SQL Server Management Studio"
---
# Restore a transaction log backup (SQL Server)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

You can restore a transaction log backup in [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE [tsql](../../includes/tsql-md.md)].

<a id="BeforeYouBegin"></a>
<a id="Prerequisites"></a>

## Prerequisites

You must restore backups in the order you created them. Before you restore a specific transaction log backup, restore the following earlier backups without rolling back uncommitted transactions, by using `WITH NORECOVERY`:

- The full database backup and, optionally, the last differential backup taken before that transaction log backup. The database must have used the full or bulk-logged recovery model before the most recent full or differential database backup was created.

- All transaction log backups taken after the full database backup or the differential backup (if you restore one) and before that transaction log backup. You must apply log backups in the order you created them, without any gaps in the log chain.

For more information about transaction log backups, see [Transaction log backups](transaction-log-backups-sql-server.md) and [Apply Transaction Log Backups](apply-transaction-log-backups-sql-server.md).

<a id="Security"></a>
<a id="Permissions"></a>

## Permissions

For an existing database, `RESTORE` permissions default to members of the **sysadmin** and **dbcreator** fixed server roles and the owner (`dbo`) of the database.

`RESTORE` permissions go to roles whose membership the server can always check. The server can check fixed database role membership only when the database is accessible and undamaged, which isn't always true when `RESTORE` runs. For this reason, members of the **db_owner** fixed database role don't have `RESTORE` permissions.

<a id="SSMSProcedure"></a>

## Use SQL Server Management Studio

> [!WARNING]  
> In a typical restore, you select the log backups together with the data and differential backups in the **Restore Database** dialog box.

1. Connect to the appropriate instance of the [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)]. In Object Explorer, select the server name to expand the server tree.

1. Expand **Databases**. Select a user database, or expand **System Databases** and select a system database.

1. Right-click the database, point to **Tasks**, point to **Restore**, and then select **Transaction Log**. The **Restore Transaction Log** dialog box opens.

   > [!NOTE]  
   > If **Transaction Log** is unavailable, you might need to restore a full or differential backup first. Use the **Restore Database** dialog box.

1. On the **General** page, in the **Database** list box, select the database. The list shows only databases in the restoring state.

1. To specify the source and location of the backup sets to restore, select one of the following options:

   - **From previous backups of database**

     Select the database to restore from the dropdown list. The list contains only databases with backups recorded in the **msdb** backup history.

   - **From file or tape**

     Select the browse (**...**) button to open the **Select backup devices** dialog box. In the **Backup media type** box, select a device type. To add one or more devices to the **Backup media** box, select **Add**.

     After you add the devices you want, select **OK** to return to the **General** page.

1. In the **Select the transaction log backups to restore** grid, select the backups to restore. The grid lists the transaction log backups available for the selected database. The grid lists log backups in the order of the log sequence numbers (LSNs) they contain, and you must restore them in this order.

   The following table lists the column headers of the grid and describes their values.

   | Header | Value |
   | --- | --- |
   | **Restore** | Selected checkboxes indicate the backup sets to restore. |
   | **Name** | Name of the backup set. |
   | **Component** | Backed-up component: **Database**, **File**, or \<blank> (for transaction logs). |
   | **Database** | Name of the database involved in the backup operation. |
   | **Start Date** | Date and time when the backup operation began, presented in the regional setting of the client. |
   | **Finish Date** | Date and time when the backup operation finished, presented in the regional setting of the client. |
   | **First LSN** | Log sequence number of the first transaction in the backup set. Blank for file backups. |
   | **Last LSN** | Log sequence number of the last transaction in the backup set. Blank for file backups. |
   | **Checkpoint LSN** | Log sequence number of the most recent checkpoint at the time the backup was created. |
   | **Full LSN** | Log sequence number of the most recent full database backup. |
   | **Server** | Name of the Database Engine instance that performed the backup operation. |
   | **User Name** | Name of the user who performed the backup operation. |
   | **Size** | Size of the backup set in bytes. |
   | **Position** | Position of the backup set in the volume. |
   | **Expiration** | Date and time the backup set expires. |

1. Select one of the following options:

   - **Point in time**

     Keep the default (**Most recent possible**), or select the browse button to open the **Point in Time Restore** dialog box and select a specific date and time.

   - **Marked transaction**

     Restore the database to a previously marked transaction. This option opens the **Select Marked Transaction** dialog box. The grid in this dialog box lists the marked transactions in the selected transaction log backups.

     By default, the restore operation goes up to, but excludes, the marked transaction. To include the marked transaction in the restore, select **Include marked transaction**.

     The following table lists the column headers of the grid and describes their values.

     | Header | Value |
     | --- | --- |
     | \<blank> | Displays a checkbox for selecting the mark. |
     | **Transaction Mark** | Name of the marked transaction specified by the user when the transaction was committed. |
     | **Date** | Date and time of the transaction when it was committed. Transaction date and time are displayed as recorded in the **logmarkhistory** table in **msdb**, not in the client computer's date and time. |
     | **Description** | Description of the marked transaction, if any, specified by the user when the transaction was committed. |
     | **LSN** | Log sequence number of the marked transaction. |
     | **Database** | Name of the database where the marked transaction was committed. |
     | **User Name** | Name of the database user who committed the marked transaction. |

1. To view or select the advanced options, select **Options** in the **Select a page** pane.

1. In the **Restore options** section, select from the following options:

   - **Preserve the replication settings (WITH KEEP_REPLICATION)**

     Preserves the replication settings when you restore a published database to a different server from the one where the database was created.

     This option is available only with the **Leave the database ready for use by rolling back uncommitted transactions...** option (described later), which is equivalent to restoring a backup with the **RECOVERY** option.

     Selecting this option is equivalent to using the **KEEP_REPLICATION** option in a [!INCLUDE [tsql](../../includes/tsql-md.md)] **RESTORE** statement.

   - **Prompt before restoring each backup**

     Before each backup set after the first, this option opens the **Continue with Restore** dialog box, which asks whether you want to continue the restore sequence. The dialog box shows the name of the next media set (if available), the backup set name, and the backup set description.

     This option is especially useful when you must swap tapes for different media sets, for example, when the server has only one tape device. Select **OK** only when you're ready to continue.

     If you select **No**, the database stays in the restoring state. You can continue the restore sequence later, from the last restore that completed. If the next backup is a data or differential backup, use the **Restore Database** task again. If the next backup is a log backup, use the **Restore Transaction Log** task.

   - **Restrict access to the restored database (WITH RESTRICTED_USER)**

     Makes the restored database available only to the members of **db_owner**, **dbcreator**, or **sysadmin**.

     Selecting this option is equivalent to using the **RESTRICTED_USER** option in a [!INCLUDE [tsql](../../includes/tsql-md.md)] **RESTORE** statement.

1. Under **Recovery state**, specify the state of the database after the restore operation.

   - **Leave the database ready for use by rolling back uncommitted transactions. Additional transaction logs can't be restored. (RESTORE WITH RECOVERY)**

     Recovers the database. This option is equivalent to the **RECOVERY** option in a [!INCLUDE [tsql](../../includes/tsql-md.md)] **RESTORE** statement.

     Choose this option only if you have no other log backups to restore.

   - **Leave the database non-operational, and don't roll back uncommitted transactions. Additional transaction logs can be restored. (RESTORE WITH NORECOVERY)**

     Leaves the database unrecovered, in the **RESTORING** state. This option is equivalent to using the **NORECOVERY** option in a [!INCLUDE [tsql](../../includes/tsql-md.md)] **RESTORE** statement.

     When you choose this option, the **Preserve the replication settings** option is unavailable.

     > [!IMPORTANT]  
     > For a mirror or secondary database, always select this option.

   - **Leave the database in read-only mode. Undo uncommitted transactions, but save the undo actions in a file so that recovery effects can be reversed. (RESTORE WITH STANDBY)**

     Leaves the database in a standby state. This option is equivalent to using the **STANDBY** option in a [!INCLUDE [tsql](../../includes/tsql-md.md)] **RESTORE** statement.

     This option requires a standby file.

1. If you leave the database in read-only mode, specify a standby file name in the **Standby file** text box. You can browse for the standby file or type its path in the text box.

<a id="TsqlProcedure"></a>

## Use Transact-SQL

> [!IMPORTANT]  
> To avoid ambiguity, explicitly specify either `WITH NORECOVERY` or `WITH RECOVERY` in every `RESTORE` statement, especially in scripts.

1. For each transaction log backup before the last one in your restore sequence, run a `RESTORE LOG` statement with the following values:

   - The name of the database to apply the transaction log to.
   - The backup device that contains the transaction log backup.
   - The `NORECOVERY` clause.

   The following example shows the basic syntax, where `<database_name>` is the name of the database and `<backup_device>` is the name of the device that contains the log backup.

   ```syntaxsql
   RESTORE LOG <database_name>
   FROM <backup_device>
   WITH NORECOVERY
   [ ; ]
   ```

   If your restore sequence has only one log backup, skip this step.

1. For the last log backup in your restore sequence, use one of the following methods to recover the database:

   - Recover the database as part of the last `RESTORE LOG` statement:

     ```sql
     RESTORE LOG <database_name>
     FROM <backup_device>
     WITH RECOVERY;
     GO
     ```

   - Recover the database later, with a separate `RESTORE DATABASE` statement:

     ```sql
     RESTORE LOG <database_name>
     FROM <backup_device>
     WITH NORECOVERY;

     RESTORE DATABASE <database_name>
     WITH RECOVERY;
     GO
     ```

     The database stays in the restoring state after the last `RESTORE LOG` statement. You can check that you restored every necessary log backup, and then recover the database. This approach is often useful for a point-in-time restore.

   > [!IMPORTANT]  
   > If you're creating a mirror database, omit the recovery step. A mirror database must remain in the RESTORING state.

<a id="TsqlExample"></a>

### Examples (Transact-SQL)

By default, the [!INCLUDE [ssSampleDBobject](../../includes/sssampledbobject-md.md)] database uses the simple recovery model. The following examples require backups taken under the full recovery model. Before you create those backups, run the following statement to change the recovery model:

```sql
ALTER DATABASE AdventureWorks2025
SET RECOVERY FULL;
```

The switch to the full recovery model takes effect only after the first data backup. Take a full database backup to start the log chain.

#### A. Apply a single transaction log backup

The following example restores the [!INCLUDE [ssSampleDBobject](../../includes/sssampledbobject-md.md)] database from a full database backup on a backup device named `AdventureWorks2025_1`. It then applies the first transaction log backup from a backup device named `AdventureWorks2025_log`. Finally, it recovers the database.

```sql
RESTORE DATABASE AdventureWorks2025
FROM AdventureWorks2025_1
WITH NORECOVERY;
GO

RESTORE LOG AdventureWorks2025
FROM AdventureWorks2025_log
WITH FILE = 1,
     NORECOVERY;
GO

RESTORE DATABASE AdventureWorks2025
WITH RECOVERY;
GO
```

#### B. Apply multiple transaction log backups

The following example restores the [!INCLUDE [ssSampleDBobject](../../includes/sssampledbobject-md.md)] database from a full database backup on a backup device named `AdventureWorks2025_1`. It then applies, one by one, the first three transaction log backups from a backup device named `AdventureWorks2025_log`. Finally, it recovers the database.

```sql
RESTORE DATABASE AdventureWorks2025
FROM AdventureWorks2025_1
WITH NORECOVERY;
GO

RESTORE LOG AdventureWorks2025
FROM AdventureWorks2025_log
WITH FILE = 1,
     NORECOVERY;
GO

RESTORE LOG AdventureWorks2025
FROM AdventureWorks2025_log
WITH FILE = 2,
     NORECOVERY;
GO

RESTORE LOG AdventureWorks2025
FROM AdventureWorks2025_log
WITH FILE = 3,
     NORECOVERY;
GO

RESTORE DATABASE AdventureWorks2025
WITH RECOVERY;
GO
```

<a id="RelatedTasks"></a>

## Related tasks

- [Back up a transaction log](back-up-a-transaction-log-sql-server.md)
- [Restore a Database Backup Using SSMS](restore-a-database-backup-using-ssms.md)
- [Restore Database to Point of Failure - Full Recovery](restore-database-to-point-of-failure-full-recovery.md)
- [Restore a SQL Server Database to a Point in Time (Full Recovery Model)](restore-a-sql-server-database-to-a-point-in-time-full-recovery-model.md)
- [Restore a database to a marked transaction (SQL Server Management Studio)](restore-a-database-to-a-marked-transaction-sql-server-management-studio.md)

## Related content

- [RESTORE Statements (Transact-SQL)](../../t-sql/statements/restore-statements-transact-sql.md)
- [Apply Transaction Log Backups (SQL Server)](apply-transaction-log-backups-sql-server.md)
