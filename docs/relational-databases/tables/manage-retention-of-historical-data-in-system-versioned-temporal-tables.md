---
title: Manage historical data in system-versioned temporal tables
description: Learn how to manage historical data retention in system-versioned temporal tables.
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/29/2024
ms.service: sql
ms.subservice: table-view-index
ms.topic: conceptual
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Manage retention of historical data in system-versioned temporal tables

[!INCLUDE [sqlserver2016-asdb-asdbmi](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi.md)]

With system-versioned temporal tables, the history table might increase your database size more than regular tables, particularly under the following conditions:

- You retain historical data for a long period of time
- You have an update or delete heavy data modification pattern

A large and ever-growing history table can become an issue both due to pure storage costs, and imposing a performance tax on temporal querying. Developing a data retention policy for managing data in the history table is an important aspect of planning and managing the lifecycle of every temporal table.

## Data retention management for history table

Managing temporal table data retention begins with determining the required retention period for each temporal table. Your retention policy, in most cases, should be part of the business logic of the application using the temporal tables. For example, applications in data audit and time travel scenarios have firm requirements regarding how long historical data must be available for online querying.

Once you determine your data retention period, you should develop a plan for managing historical data. Decide how and where you store your historical data, and how to delete historical data that is older than your retention requirements. The following approaches for managing historical data in the temporal history table are available:

- [Table partitioning](#use-table-partitioning-approach)
- [Custom cleanup script](#use-custom-cleanup-script-approach)
- [Retention policy](#use-temporal-history-retention-policy-approach)

With each of these approaches, the logic for migrating or cleaning history data is based on the column that corresponds to end of period in the current table. The end of period value for each row determines the moment when the row version becomes *closed*, that is, when it lands in the history table. For example, the condition `ValidTo < DATEADD (DAYS, -30, SYSUTCDATETIME ())` specifies that historical data older than one month needs to be removed or moved out from the history table.

The examples in this article use the samples created in the [Create a system-versioned temporal table](creating-a-system-versioned-temporal-table.md) article.

## Use table partitioning approach

[Partitioned tables and indexes](../partitions/create-partitioned-tables-and-indexes.md) can make large tables more manageable and scalable. With the table partitioning approach, you can implement custom data cleanup, or offline archival, based on a time condition. Table partitioning also gives you performance benefits when querying temporal tables on a subset of data history, by using partition elimination.

With table partitioning, you can implement a sliding window to move out oldest portion of the historical data from the history table, and keep the size of the retained part constant in terms of age. A sliding window maintains data in the history table equal to required retention period. The operation of switching data out from the history table is supported while `SYSTEM_VERSIONING` is `ON`, which means that you can clean a portion of the history data without introducing a maintenance window or blocking your regular workloads.

> [!NOTE]  
> In order to perform partition switching, your clustered index on history table must be aligned with the partitioning schema (it has to contain `ValidTo`). The default history table created by the system contains a clustered index that includes the `ValidTo` and `ValidFrom` columns, which is optimal for partitioning, inserting new history data, and typical temporal querying. For more information, see [Temporal tables](temporal-tables.md).

A sliding window has two sets of tasks that you need to perform:

- A partitioning configuration task
- Recurring partition maintenance tasks

For the illustration, let's assume that you want to keep historical data for six months and that you want to keep every month of data in a separate partition. Also, let's assume that you activated system-versioning in September of 2023.

A partitioning configuration task creates the initial partitioning configuration for the history table. For this example, you create the same number partitions as the size of sliding window, in months, plus an extra empty partition preprepared (explained later in this article). This configuration ensures that the system is able to store new data correctly when you start the recurring partition maintenance task for the first time, and guarantees that you never split partitions with data to avoid expensive data movements. You should perform this task using Transact-SQL using the example script later in this article.

The following picture shows initial partitioning configuration to keep six months of data.

:::image type="content" source="media/manage-retention-of-historical-data-in-system-versioned-temporal-tables/partitioning.png" alt-text="Diagram showing initial partitioning configuration to keep six months of data." lightbox="media/manage-retention-of-historical-data-in-system-versioned-temporal-tables/partitioning.png":::

> [!NOTE]  
> For performance implications of using `RANGE LEFT` versus `RANGE RIGHT` when configuring partitioning, see [Performance considerations with table partitioning](#performance-considerations-with-table-partitioning) later in this article.

The first and last partitions are *open* on lower and upper boundaries, respectively, to ensure that every new row has destination partition regardless of the value in partitioning column. As time goes by, new rows in history table land in higher partitions. When the sixth partition gets filled up, you reach the targeted retention period. This is the moment to start the recurring partition maintenance task for the first time. It needs to be scheduled to run periodically, once per month in this example.

The following picture illustrates the recurring partition maintenance tasks (see detailed steps later in this section).

:::image type="content" source="media/manage-retention-of-historical-data-in-system-versioned-temporal-tables/partitioning-2.png" alt-text="Diagram showing the recurring partition maintenance tasks." lightbox="media/manage-retention-of-historical-data-in-system-versioned-temporal-tables/partitioning-2.png":::

The detailed steps for the recurring partition maintenance tasks are:

1. `SWITCH OUT`: Create a staging table and then switch a partition between the history table and the staging table using the [ALTER TABLE](../../t-sql/statements/alter-table-transact-sql.md) statement with the `SWITCH PARTITION` argument (see example C. Switching partitions between tables).

   ```sql
   ALTER TABLE [<history table>] SWITCH PARTITION 1 TO [<staging table>];
   ```

   After the partition switch, you can optionally archive the data from staging table, and then either drop or truncate the staging table, to be ready for the next time you need to perform this recurring partition maintenance task.

1. `MERGE RANGE`: Merge the empty partition `1` with partition `2` using the [ALTER PARTITION FUNCTION](../../t-sql/statements/alter-partition-function-transact-sql.md) with `MERGE RANGE` (See example B). By removing the lowest boundary using this function, you effectively merge the empty partition `1` with the former partition `2` to form a new partition `1`. The other partitions also effectively change their ordinals.

1. `SPLIT RANGE`: Create a new empty partition `7` using the [ALTER PARTITION FUNCTION](../../t-sql/statements/alter-partition-function-transact-sql.md) with `SPLIT RANGE` (See example A). By adding a new upper boundary using this function, you effectively create a separate partition for the upcoming month.

### Use Transact-SQL to create partitions on history table

Use the following Transact-SQL script to create the partition function, the partition schema, and recreate the clustered index to be partition-aligned with the partition schema, partitions. For this example, you create a six-month sliding window with monthly partitions, beginning September 2023.

```sql
BEGIN TRANSACTION

/*Create partition function*/
CREATE PARTITION FUNCTION [fn_Partition_DepartmentHistory_By_ValidTo] (DATETIME2(7))
AS RANGE LEFT FOR VALUES (
    N'2023-09-30T23:59:59.999',
    N'2023-10-31T23:59:59.999',
    N'2023-11-30T23:59:59.999',
    N'2023-12-31T23:59:59.999',
    N'2024-01-31T23:59:59.999',
    N'2024-02-29T23:59:59.999'
);

/*Create partition scheme*/
CREATE PARTITION SCHEME [sch_Partition_DepartmentHistory_By_ValidTo]
AS PARTITION [fn_Partition_DepartmentHistory_By_ValidTo] TO (
    [PRIMARY], [PRIMARY], [PRIMARY], [PRIMARY],
    [PRIMARY], [PRIMARY], [PRIMARY]
);

/*Re-create index to be partition-aligned with the partitioning schema*/
CREATE CLUSTERED INDEX [ix_DepartmentHistory] ON [dbo].[DepartmentHistory] (
    ValidTo ASC,
    ValidFrom ASC
)
WITH (
    PAD_INDEX = OFF,
    STATISTICS_NORECOMPUTE = OFF,
    SORT_IN_TEMPDB = OFF,
    DROP_EXISTING = ON,
    ONLINE = OFF,
    ALLOW_ROW_LOCKS = ON,
    ALLOW_PAGE_LOCKS = ON,
    DATA_COMPRESSION = PAGE
) ON [sch_Partition_DepartmentHistory_By_ValidTo](ValidTo);

COMMIT TRANSACTION;
```

### Use Transact-SQL to maintain partitions in sliding window scenario

Use the following Transact-SQL script to maintain partitions in the sliding window scenario. For this example, you switch out the partition for September of 2023 using `MERGE RANGE`, and then add a new partition for March of 2024 using `SPLIT RANGE`.

```sql
BEGIN TRANSACTION

/*(1) Create staging table */
CREATE TABLE [dbo].[staging_DepartmentHistory_September_2023] (
    DeptID INT NOT NULL,
    DeptName VARCHAR(50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    ManagerID INT NULL,
    ParentDeptID INT NULL,
    ValidFrom DATETIME2(7) NOT NULL,
    ValidTo DATETIME2(7) NOT NULL
) ON [PRIMARY]
WITH (DATA_COMPRESSION = PAGE);

/*(2) Create index on the same filegroups as the partition that will be switched out*/
CREATE CLUSTERED INDEX [ix_staging_DepartmentHistory_September_2023]
ON [dbo].[staging_DepartmentHistory_September_2023] (
    ValidTo ASC,
    ValidFrom ASC
)
WITH (
    PAD_INDEX = OFF,
    SORT_IN_TEMPDB = OFF,
    DROP_EXISTING = OFF,
    ONLINE = OFF,
    ALLOW_ROW_LOCKS = ON,
    ALLOW_PAGE_LOCKS = ON
) ON [PRIMARY];

/*(3) Create constraints matching the partition that will be switched out*/
ALTER TABLE [dbo].[staging_DepartmentHistory_September_2023]
    WITH CHECK ADD CONSTRAINT [chk_staging_DepartmentHistory_September_2023_partition_1]
    CHECK (ValidTo <= N'2023-09-30T23:59:59.999')

ALTER TABLE [dbo].[staging_DepartmentHistory_September_2023]
    CHECK CONSTRAINT [chk_staging_DepartmentHistory_September_2023_partition_1]

/*(4) Switch partition to staging table*/
ALTER TABLE [dbo].[DepartmentHistory] SWITCH PARTITION 1
TO [dbo].[staging_DepartmentHistory_September_2023]
    WITH (WAIT_AT_LOW_PRIORITY(MAX_DURATION = 0 MINUTES, ABORT_AFTER_WAIT = NONE))

/*(5) [Commented out] Optionally archive the data and drop staging table
      INSERT INTO [ArchiveDB].[dbo].[DepartmentHistory]
      SELECT * FROM [dbo].[staging_DepartmentHistory_September_2023];
      DROP TABLE [dbo].[staging_DepartmentHIstory_September_2023];
*/

/*(6) merge range to move lower boundary one month ahead*/
ALTER PARTITION FUNCTION [fn_Partition_DepartmentHistory_By_ValidTo]()
    MERGE RANGE(N'2023-09-30T23:59:59.999');

/*(7) Create new empty partition for "April and after" by creating new boundary point and specifying NEXT USED file group*/
ALTER PARTITION SCHEME [sch_Partition_DepartmentHistory_By_ValidTo] NEXT USED [PRIMARY]
    ALTER PARTITION FUNCTION [fn_Partition_DepartmentHistory_By_ValidTo]()
    SPLIT RANGE(N'2024-03-31T23:59:59.999');
COMMIT TRANSACTION
```

You can slightly modify the previous script and use it in regular monthly maintenance process:

1. In step (1), create a new staging table for the month you want to remove (October would be next one in this example).
1. In step (3), create and check the constraint that matches the month of data you want to remove: `ValidTo <= N'2023-10-31T23:59:59.999'` for an October partition.
1. In step (4), `SWITCH` partition `1` to the newly created staging table.
1. In step (6), alter the partition function by merging the lower boundary: `MERGE RANGE(N'2023-10-31T23:59:59.999'` after you move out data for October.
1. In step (7), split the partition function, creating a new upper boundary: `SPLIT RANGE (N'2024-04-30T23:59:59.999'` after you move out data for October.

However, the optimal solution would be to regularly run a generic Transact-SQL script that runs the appropriate action every month without modification. You can generalize the previous script to act upon your provided parameters (the lower boundary that needs to be merged, and the new boundary that is created with the partition split). To avoid creating a staging table every month, you can create one beforehand and reuse it, by changing the check constraint to match the partition that you switch out. For more information, see [how sliding window can be fully automated](/previous-versions/sql/sql-server-2005/administrator/aa964122(v=sql.90)).

### Performance considerations with table partitioning

You must perform the `MERGE` and `SPLIT RANGE` operations to avoid data movement, as data movement can incur significant performance overhead. For more information, see [Modify a partition function](../partitions/modify-a-partition-function.md). You do so by using `RANGE LEFT` rather than `RANGE RIGHT` when you [create the partition function](../../t-sql/statements/create-partition-function-transact-sql.md).

The following diagram describes the `RANGE LEFT` and `RANGE RIGHT` options:

:::image type="content" source="media/manage-retention-of-historical-data-in-system-versioned-temporal-tables/partitioning-3.png" alt-text="Diagram showing the RANGE LEFT and RANGE RIGHT options." lightbox="media/manage-retention-of-historical-data-in-system-versioned-temporal-tables/partitioning-3.png":::

When you define a partition function as `RANGE LEFT`, the specified values are the upper boundaries of the partitions. When you use `RANGE RIGHT`, the specified values are the lower boundaries of the partitions. When you use the `MERGE RANGE` operation to remove a boundary from the partition function definition, the underlying implementation also removes the partition that contains the boundary. If that partition isn't empty, data is moved to the partition that is result of `MERGE RANGE` operation.

In a sliding window scenario, you always remove the *lowest* partition boundary.

- `RANGE LEFT` case: The lowest partition boundary belongs to partition `1`, which is empty (after partition switch out), so `MERGE RANGE` doesn't incur any data movement.

- `RANGE RIGHT` case: The lowest partition boundary belongs to partition `2`, which isn't empty, because partition `1` was emptied by switching out. In this case, `MERGE RANGE` incurs data movement (data from partition `2` is moved to partition `1`). To avoid this, `RANGE RIGHT` in the sliding window scenario needs to have partition `1`, which is always empty. This means that if you use `RANGE RIGHT`, you should create and maintain one extra partition compared to `RANGE LEFT` case.

**Conclusion**: Partition management is easier when you use `RANGE LEFT` in a sliding partition, and avoids data movement. However, defining partition boundaries with `RANGE RIGHT` is slightly easier, because you don't have to deal with date and time check issues.

## Use custom cleanup script approach

In cases when table partitioning isn't viable, another approach is to delete the data from history table using a custom cleanup script. Deleting data from history table is possible only when `SYSTEM_VERSIONING = OFF`. In order to avoid data inconsistency, perform cleanup either during a maintenance window (when workloads that modify data aren't active), or within a transaction (effectively blocking other workloads). This operation requires `CONTROL` permission on current and history tables.

To minimally block regular applications and user queries, delete data in smaller chunks with a delay, when performing the cleanup script inside a transaction. While there's no optimal size for each data chunk to be deleted for all scenarios, deleting more than 10,000 rows in a single transaction might impose a significant penalty.

The cleanup logic is the same for every temporal table, so it can be automated through a generic stored procedure that you schedule to run periodically, for every temporal table for which you want to limit data history.

The following diagram illustrates how your cleanup logic should be organized for a single table to reduce the effect on the running workloads.

:::image type="content" source="media/manage-retention-of-historical-data-in-system-versioned-temporal-tables/custom-cleanup-script-diagram.png" alt-text="Diagram showing how your cleanup logic should be organized for a single table to reduce effect on the running workloads." lightbox="media/manage-retention-of-historical-data-in-system-versioned-temporal-tables/custom-cleanup-script-diagram.png":::

Here are some high-level guidelines for implementing the process. Schedule cleanup logic to run every day and iterate over all temporal tables that need data cleanup. Use the SQL Server Agent or a different tool to schedule this process:

- Delete historical data in every temporal table, starting from the oldest to the most recent rows in several iterations in small chunks, and avoid deleting all rows in a single transaction, as shown in the previous diagram.

- Implement every iteration as an invocation of a generic stored procedure, which removes a portion of data from the history table (see the following code example for this procedure).

- Calculate how many rows you need to delete for an individual temporal table every time you invoke the process. Based on the result and the number of iterations you want to have, determine dynamic split points for every procedure invocation.

- Plan to have a period of delay between iterations for a single table, to reduce the effect on applications that access the temporal table.

A stored procedure that deletes the data for a single temporal table might look like the following code snippet. Review this code carefully, and adjust it before apply in your environment.

This script generates three statements that run inside a transaction:

1. `SET SYSTEM_VERSIONING = OFF`
1. `DELETE FROM <history_table>`
1. `SET SYSTEM_VERSIONING = ON`

In [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)], the first two steps must run in separate `EXEC` statements, or [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] generates an error similar to the following example:

```output
Msg 13560, Level 16, State 1, Line XXX
Cannot delete rows from a temporal history table '<database_name>.<history_table_schema_name>.<history_table_name>'.
```

```sql
DROP PROCEDURE IF EXISTS usp_CleanupHistoryData;
GO
CREATE PROCEDURE usp_CleanupHistoryData @temporalTableSchema SYSNAME,
    @temporalTableName SYSNAME,
    @cleanupOlderThanDate DATETIME2
AS
DECLARE @disableVersioningScript NVARCHAR(MAX) = '';
DECLARE @deleteHistoryDataScript NVARCHAR(MAX) = '';
DECLARE @enableVersioningScript NVARCHAR(MAX) = '';
DECLARE @historyTableName SYSNAME
DECLARE @historyTableSchema SYSNAME
DECLARE @periodColumnName SYSNAME

/*Generate script to discover history table name and end of period column for given temporal table name*/
EXECUTE sp_executesql
N'SELECT @hst_tbl_nm = t2.name,
      @hst_sch_nm = s2.name,
      @period_col_nm = c.name
  FROM sys.tables t1
  INNER JOIN sys.tables t2 ON t1.history_table_id = t2.object_id
  INNER JOIN sys.schemas s1 ON t1.schema_id = s1.schema_id
  INNER JOIN sys.schemas s2 ON t2.schema_id = s2.schema_id
  INNER JOIN sys.periods p ON p.object_id = t1.object_id
  INNER JOIN sys.columns c ON p.end_column_id = c.column_id AND c.object_id = t1.object_id
  WHERE t1.name = @tblName AND s1.name = @schName',
N'@tblName sysname,
    @schName sysname,
    @hst_tbl_nm sysname OUTPUT,
    @hst_sch_nm sysname OUTPUT,
    @period_col_nm sysname OUTPUT',
@tblName = @temporalTableName,
@schName = @temporalTableSchema,
@hst_tbl_nm = @historyTableName OUTPUT,
@hst_sch_nm = @historyTableSchema OUTPUT,
@period_col_nm = @periodColumnName OUTPUT

IF @historyTableName IS NULL OR @historyTableSchema IS NULL OR @periodColumnName IS NULL
    THROW 50010, 'History table cannot be found. Either specified table is not system-versioned temporal or you have provided incorrect argument values.', 1;

SET @disableVersioningScript = @disableVersioningScript
    + 'ALTER TABLE [' + @temporalTableSchema + '].[' + @temporalTableName
    + '] SET (SYSTEM_VERSIONING = OFF)'
SET @deleteHistoryDataScript = @deleteHistoryDataScript + ' DELETE FROM ['
    + @historyTableSchema + '].[' + @historyTableName + '] WHERE ['
    + @periodColumnName + '] < ' + '''' + CONVERT(VARCHAR(128), @cleanupOlderThanDate, 126) + ''''
SET @enableVersioningScript = @enableVersioningScript + ' ALTER TABLE ['
    + @temporalTableSchema + '].[' + @temporalTableName
    + '] SET (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [' + @historyTableSchema
    + '].[' + @historyTableName + '], DATA_CONSISTENCY_CHECK = OFF )); '

BEGIN TRANSACTION
    EXEC (@disableVersioningScript);
    EXEC (@deleteHistoryDataScript);
    EXEC (@enableVersioningScript);
COMMIT;
```

## Use temporal history retention policy approach

**Applies to:** [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] and later versions, and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)].

Temporal history retention can be configured at the individual table level, which allows users to create flexible aging policies. Temporal retention requires that you set only one parameter during table creation or schema change.

After you define the retention policy, the [!INCLUDE [ssde-md](../../includes/ssde-md.md)] starts checking regularly if there are historical rows that are eligible for automatic data cleanup. Identification of matching rows and their removal from the history table occur transparently, in a background task that is scheduled and run by the system. The age condition for history table rows is checked based on the column representing the end of the `SYSTEM_TIME` period (in these examples, the `ValidTo` column). If the retention period is set to six months, for example, table rows eligible for cleanup satisfy the following condition:

```sql
ValidTo < DATEADD (MONTH, -6, SYSUTCDATETIME())
```

In the preceding example, the `ValidTo` column corresponds to the end of the `SYSTEM_TIME` period.

### How to configure retention policy

Before you configure retention policy for a temporal table, check whether temporal historical retention is enabled at the database level:

```sql
SELECT is_temporal_history_retention_enabled, name
FROM sys.databases;
```

The database flag `is_temporal_history_retention_enabled` is set to `ON` by default, but you can change it with the `ALTER DATABASE` statement. This value is automatically set to `OFF` after a point-in-time restore (PITR) operation. To enable temporal history retention cleanup for your database, run the following statement. You must replace `<myDB>` with the database you wish to alter:

```sql
ALTER DATABASE [<myDB>]
SET TEMPORAL_HISTORY_RETENTION ON;
```

Retention policy is configured during table creation by specifying value for the `HISTORY_RETENTION_PERIOD` parameter:

```sql
CREATE TABLE dbo.WebsiteUserInfo
(
    UserID INT NOT NULL PRIMARY KEY CLUSTERED,
    UserName NVARCHAR(100) NOT NULL,
    PagesVisited int NOT NULL,
    ValidFrom DATETIME2(0) GENERATED ALWAYS AS ROW START,
    ValidTo DATETIME2(0) GENERATED ALWAYS AS ROW END,
    PERIOD FOR SYSTEM_TIME (ValidFrom, ValidTo)
)
WITH (SYSTEM_VERSIONING = ON
    (
        HISTORY_TABLE = dbo.WebsiteUserInfoHistory,
        HISTORY_RETENTION_PERIOD = 6 MONTHS
    )
);
```

You can specify the retention period using different time units: `DAYS`, `WEEKS`, `MONTHS`, and `YEARS`. If `HISTORY_RETENTION_PERIOD` is omitted, `INFINITE` retention is assumed. You can also use the `INFINITE` keyword explicitly.

In some scenarios, you might want to configure retention after table creation, or to change the previously configured value. In that case, use the `ALTER TABLE` statement:

```sql
ALTER TABLE dbo.WebsiteUserInfo
SET (SYSTEM_VERSIONING = ON (HISTORY_RETENTION_PERIOD = 9 MONTHS));
```

To review the current state of the retention policy, use the following sample. This query joins the temporal retention enablement flag at the database level with retention periods for individual tables:

```sql
SELECT DB.is_temporal_history_retention_enabled,
    SCHEMA_NAME(T1.schema_id) AS TemporalTableSchema,
    T1.name AS TemporalTableName,
    SCHEMA_NAME(T2.schema_id) AS HistoryTableSchema,
    T2.name AS HistoryTableName,
    T1.history_retention_period,
    T1.history_retention_period_unit_desc
FROM sys.tables T1
OUTER APPLY (
    SELECT is_temporal_history_retention_enabled
    FROM sys.databases
    WHERE name = DB_NAME()
    ) AS DB
LEFT JOIN sys.tables T2
    ON T1.history_table_id = T2.object_id
WHERE T1.temporal_type = 2;
```

### How the Database Engine deletes aged rows

The cleanup process depends on the index layout of the history table. Only history tables with a clustered index (B+ tree or columnstore) can have a finite retention policy configured. A background task is created to perform aged data cleanup for all temporal tables with a finite retention period. Cleanup logic for the rowstore (B+ tree) clustered index deletes aged rows in smaller chunks (up to 10,000), minimizing pressure on the database log and I/O subsystem. Although cleanup logic uses the required B+ tree index, the order of deletions for the rows older than the retention period can't be guaranteed. Don't take any dependency on the cleanup order in your applications.

The cleanup task for the clustered columnstore removes entire row groups at once (typically containing 1 million rows each), which is more efficient, especially when historical data is generated at a high pace.

:::image type="content" source="media/manage-retention-of-historical-data-in-system-versioned-temporal-tables/cci-retention.png" alt-text="Screenshot of clustered columnstore retention." lightbox="media/manage-retention-of-historical-data-in-system-versioned-temporal-tables/cci-retention.png":::

Data compression and retention cleanup makes clustered columnstore index a perfect choice for scenarios when your workload rapidly generates a high amount of historical data. That pattern is typical for intensive transactional processing workloads that use temporal tables for change tracking and auditing, trend analysis, or IoT data ingestion.

For more information, see [Manage historical data in Temporal Tables with retention policy](/azure/sql-database/sql-database-temporal-tables-retention-policy).

## Related content

- [Temporal tables](temporal-tables.md)
- [Get started with system-versioned temporal tables](getting-started-with-system-versioned-temporal-tables.md)
- [Temporal table system consistency checks](temporal-table-system-consistency-checks.md)
- [Partition with temporal tables](partitioning-with-temporal-tables.md)
- [Temporal table considerations and limitations](temporal-table-considerations-and-limitations.md)
- [Temporal table security](temporal-table-security.md)
- [System-versioned temporal tables with memory-optimized tables](system-versioned-temporal-tables-with-memory-optimized-tables.md)
- [Temporal table metadata views and functions](temporal-table-metadata-views-and-functions.md)
