---
title: "Memory-Optimized Container and Filegroup Removal"
description: Learn how to remove the memory-optimized containers and filegroup from a SQL Server database.
author: dimitri-furman
ms.author: dfurman
ms.reviewer: randolphwest
ms.date: 04/29/2025
ms.service: sql
ms.subservice: in-memory-oltp
ms.topic: concept-article
monikerRange: ">=sql-server-ver17 || >=sql-server-linux-ver17"
ms.custom:
  - build-2025
---

# Memory-optimized container and filegroup removal

[!INCLUDE [sqlserver2025-and-later](../../includes/applies-to-version/sqlserver2025-and-later.md)]

In [!INCLUDE [sql-server-2022](../../includes/sssql22-md.md)] and older versions, if In-Memory OLTP is enabled for a database, the last memory-optimized container and the memory-optimized filegroup can't be removed even if all In-Memory OLTP objects are dropped. As a result, the In-Memory OLTP engine continues to run when not in use.

Starting with [!INCLUDE [sql-server-2025](../../includes/sssql25-md.md)], the In-Memory OLTP engine can be stopped by completely removing all memory-optimized containers and filegroups in databases with no remaining In-Memory OLTP objects.

To remove the memory-optimized containers and filegroup and stop the In-Memory OLTP engine:

1. As a validation step, connect to the database, and execute the following query to confirm that the In-Memory OLTP (XTP) engine is deployed in the database:

    ```sql
    SELECT deployment_state,
           deployment_state_desc
    FROM sys.dm_db_xtp_undeploy_status;
    ```

    If the `deployment_state` column is 1 or 2, the In-Memory OLTP engine is deployed and you can proceed with the following steps. If the `deployment_state` column is 0, the In-Memory OLTP engine isn't deployed in the current database or has already been stopped, and the remainder of this article doesn't apply.

1. Drop all In-Memory OLTP objects in the database, including memory-optimized tables and table types, and natively compiled stored procedures.

    > [!CAUTION]  
    > This step might cause permanent loss of data in the memory-optimized tables in the current database. Before proceeding, make sure that the data isn't needed and make a backup of the database.

    To find In-Memory OLTP objects in a database, execute the following T-SQL statements:

    ```sql
    USE [<database-name-placeholder>];

    /* memory-optimized tables */
    SELECT object_id,
           OBJECT_SCHEMA_NAME(object_id) AS schema_name,
           name AS table_name
    FROM sys.tables
    WHERE is_memory_optimized = 1;

    /* natively compiled modules */
    SELECT object_id,
           OBJECT_SCHEMA_NAME(object_id) AS schema_name,
           OBJECT_NAME(object_id) AS module_name
    FROM sys.all_sql_modules
    WHERE uses_native_compilation = 1;

    /* memory-optimized table types */
    SELECT SCHEMA_NAME(schema_id) AS type_schema_name,
           name AS type_name,
           OBJECT_NAME(type_table_object_id) AS type_table_name
    FROM sys.table_types
    WHERE is_memory_optimized = 1;
    ```

    For more information, see [DROP TABLE](../../t-sql/statements/drop-table-transact-sql.md), [DROP PROCEDURE](../../t-sql/statements/drop-procedure-transact-sql.md), and [DROP TYPE](../../t-sql/statements/drop-type-transact-sql.md).

1. Remove all memory-optimized containers using the `ALTER DATABASE ... REMOVE FILE` statement. For more information, see [ALTER DATABASE File and Filegroup Options](../../t-sql/statements/alter-database-transact-sql-file-and-filegroup-options.md). You can also remove memory-optimized containers using the **Files** page in the **Database Properties** dialog for your database in SQL Server Management Studio (SSMS).

    Removing the last memory-optimized container for the database starts the removal of the In-Memory OLTP engine. This is a long-running operation that might require additional steps. For more information, see [Steps to complete the last memory-optimized container removal](#steps-to-complete-the-last-memory-optimized-container-removal) later in this article.

    If you cancel or abort a long-running `ALTER DATABASE ... REMOVE FILE` statement while removing the last memory-optimized container, the removal might be partially complete. To complete the removal, you can execute the `ALTER DATABASE ... REMOVE FILE` statement later.

1. Remove the memory-optimized filegroup using the `ALTER DATABASE ... REMOVE FILEGROUP` statement, or using the **Filegroups** page in the **Database Properties** dialog in SSMS.

The removal of the memory-optimized containers and filegroup is successful, and the In-Memory OLTP engine is stopped when the value in the `deployment_state` column in `sys.dm_db_xtp_undeploy_status` is 0.

> [!NOTE]
> Memory-optimized container removal isn't supported when the database has any [database snapshots](../databases/database-snapshots-sql-server.md). Drop all snapshots before removing memory-optimized containers.

### Steps to complete the last memory-optimized container removal

If the `ALTER DATABASE ... REMOVE FILE` statement to remove the last memory-optimized container doesn't complete immediately, additional steps are required.

The [sys.dm_db_xtp_undeploy_status](../system-dynamic-management-views/sys-dm-db-xtp-undeploy-status.md) DMV provides the status of the In-Memory OLTP engine removal process. In the following steps, use this query to determine the current status and the required actions:

```sql
SELECT deployment_state,
       deployment_state_desc,
       undeploy_lsn,
       start_of_log_lsn
FROM sys.dm_db_xtp_undeploy_status;
```

1. If the `deployment_state` value is 3 and the value in the `undeploy_lsn` column is 0, execute the following command:

    ```sql
    CHECKPOINT;
    ```

1. If the `deployment_state` value is 3 and the value in the `undeploy_lsn` column is other than 0, then the container removal process is waiting for the log sequence number (LSN) in the `start_of_log_lsn` column to advance beyond the LSN value in the `undeploy_lsn` column. This requires transaction log to be truncated. To truncate the log:

    - Execute the following command:

        ```sql
        CHECKPOINT;
        ```

        To advance `start_of_log_lsn` beyond `undeploy_lsn`, you might need to execute this command several times, waiting a minute after each command execution.

    - If the database uses the **Full** or **Bulk-logged** [recovery model](../backup-restore/recovery-models-sql-server.md), then in addition to executing the `CHECKPOINT` commands, you might need to determine and address the reason for the delay in log truncation, which is reported in the `log_reuse_wait_desc` column in the `sys.databases` catalog view.

        If `deployment_state` value remains 3 after executing the `CHECKPOINT` commands, execute the following statement:

        ```sql
        SELECT name,
               log_reuse_wait_desc
        FROM sys.databases
        WHERE database_id = DB_ID();
        ```

        Once you know the reason for the delay in log truncation, see [Factors that can delay log truncation](../logs/the-transaction-log-sql-server.md#factors-that-can-delay-log-truncation) for more information and to determine the appropriate action.

        In active databases, transaction log is usually truncated after some time without any additional user action. For example, if `log_reuse_wait_desc` in `sys.databases` is `LOG_BACKUP`, then scheduled or on demand log backups truncate the log. For more information, see [Back up a transaction log](../backup-restore/back-up-a-transaction-log-sql-server.md).

        If the value in the `log_reuse_wait_desc` column is `NOTHING`, but the value in `deployment_state` column remains 3, back up the transaction log. To truncate the transaction log, you might need to make more than one transaction log backup.

1. If the `deployment_state` value is 4, then the start of the active portion of the transaction log has advanced beyond the undeployment LSN, and the container removal process is waiting for the final undeployment log record. In most cases, this step completes within a few seconds without any additional action.

    If the database has availability replicas, the undeployment record must propagate and apply to all replicas. If value 4 persists for a long time, see [Determine why changes from primary replica are not reflected on secondary replica for an Always On availability group](../../database-engine/availability-groups/windows/troubleshoot-primary-changes-not-reflected-on-secondary.md) for more information.

1. If the `deployment_state` value is 5, then the container removal process is waiting for the final XTP checkpoint operation to complete. To initiate a checkpoint immediately, execute the following command:

   ```sql
   CHECKPOINT;
   ```

   An XTP checkpoint occurs automatically once the transaction log has grown by a certain threshold. For more information, see [Checkpoint Operation for Memory-Optimized Tables](checkpoint-operation-for-memory-optimized-tables.md).

1. Once the final XTP checkpoint operation completes, the removal of the last memory-optimized container completes successfully, and the value in the `deployment_state` column becomes 0.

1. If the `deployment_state` value is 6, it means that the `ALTER DATABASE ... REMOVE FILE` statement for the last memory-optimized container was canceled or aborted. Execute the statement again to complete container removal.

## Related content

- [sys.dm_db_xtp_undeploy_status (Transact-SQL)](../system-dynamic-management-objects/sys-dm-db-xtp-undeploy-status.md)
- [In-Memory OLTP overview and usage scenarios](overview-and-usage-scenarios.md)
- [The memory-optimized filegroup](the-memory-optimized-filegroup.md)
- [ALTER DATABASE (Transact-SQL) File and Filegroup Options](../../t-sql/statements/alter-database-transact-sql-file-and-filegroup-options.md)
