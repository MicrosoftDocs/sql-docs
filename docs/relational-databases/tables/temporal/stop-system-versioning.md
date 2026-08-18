---
title: Stop System-Versioning on a System-Versioned Temporal Table
description: Learn how to stop versioning on your system-versioned temporal table either temporarily or permanently.
author: rwestMSFT
ms.author: randolphwest
ms.date: 08/18/2026
ms.service: sql
ms.subservice: table-view-index
ms.topic: how-to
ms.custom:
  - ignite-2025
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---

# Stop system-versioning on a system-versioned temporal table

[!INCLUDE [sqlserver2016-asdb-asdbmi-fabricsqldb](../../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-fabricsqldb.md)]

You might want to stop versioning on your temporal table either temporarily or permanently. You can do that by setting the `SYSTEM_VERSIONING` clause to `OFF`.

## Set SYSTEM_VERSIONING = OFF

Stop system versioning if you want to perform specific maintenance operations on a temporal table or don't need a versioned table anymore. This operation gives you two independent tables:

- Current table with a period definition
- History table as a regular table

### Remarks

The history table stops capturing the updates during `SYSTEM_VERSIONING = OFF`.

No data loss happens on the temporal table when you set `SYSTEM_VERSIONING = OFF` or drop the `SYSTEM_TIME` period.

When you set `SYSTEM_VERSIONING = OFF` and don't drop the `SYSTEM_TIME` period, the system continues to update the period columns for every insert and update operation. Deletes on the current table are permanent.

You must drop the `SYSTEM_TIME` period to delete the period columns. To remove the period columns, use `ALTER TABLE <table> DROP <column>;`. For more information, see [Change the schema of a system-versioned temporal table](change-schema.md).

When you set `SYSTEM_VERSIONING = OFF`, all users with sufficient permissions can modify the schema and content of the history table, or even permanently delete the history table.

You can't set `SYSTEM_VERSIONING = OFF` if you have other objects created with `SCHEMABINDING` that use temporal query extensions, such as referencing `SYSTEM_TIME`. This restriction prevents these objects from failing if you set `SYSTEM_VERSIONING = OFF`.

### Permanently remove SYSTEM_VERSIONING

This example permanently removes `SYSTEM_VERSIONING` and deletes the period columns. Removing the period columns is optional.

```sql
ALTER TABLE dbo.Department
    SET (SYSTEM_VERSIONING = OFF);
```

Optionally, drop the period if you want to revert the temporal table to a non-temporal table.

```sql
ALTER TABLE dbo.Department
    DROP PERIOD FOR SYSTEM_TIME;
```

### Temporarily remove SYSTEM_VERSIONING

The following operations require you to set system-versioning to `OFF`:

- Removing unnecessary data from history (`DELETE` or `TRUNCATE`)
- Removing data from the current table without versioning (`DELETE`, `TRUNCATE`)
- Partition `SWITCH OUT` from the current table
- Partition `SWITCH IN` into the history table

This example temporarily stops `SYSTEM_VERSIONING` to allow you to perform specific maintenance operations. If you stop versioning temporarily as a prerequisite for table maintenance, we strongly recommend doing this change inside a transaction to keep data consistency.

When you turn system-versioning back on, specify the `HISTORY_TABLE` argument. If you don't, the system creates a new history table and associates it with the current table. The original history table can still exist as a normal table, but it's no longer associated with the current table.

```sql
BEGIN TRANSACTION;

ALTER TABLE dbo.Department
    SET (SYSTEM_VERSIONING = OFF);

TRUNCATE TABLE [History].[DepartmentHistory]
    WITH ( PARTITIONS (1, 2) );

ALTER TABLE dbo.Department
    SET (SYSTEM_VERSIONING = ON (HISTORY_TABLE = History.DepartmentHistory));

COMMIT TRANSACTION;
```

## Related content

- [Temporal tables](overview.md)
- [Get started with system-versioned temporal tables](get-started.md)
- [Manage retention of historical data in system-versioned temporal tables](manage-retention.md)
- [System-versioned temporal tables with memory-optimized tables](memory-optimized.md)
- [Create a system-versioned temporal table](create.md)
- [Modify data in a system-versioned temporal table](modify-data.md)
- [Query data in a system-versioned temporal table](query-data.md)
- [Change the schema of a system-versioned temporal table](change-schema.md)
