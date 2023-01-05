---
title: Stopping System-Versioning on a System-Versioned Temporal Table
description: "Stopping System-Versioning on a System-Versioned Temporal Table"
ms.service: sql
ms.subservice: table-view-index
ms.topic: conceptual
author: markingmyname
ms.author: maghan
ms.reviewer: ""
ms.custom: ""
ms.date: 04/28/2020
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---

# Stopping system-versioning on a system-versioned temporal table

[!INCLUDE [sqlserver2016-asdb-asdbmi](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi.md)]

You may want to stop versioning on your temporal table either temporarily or permanently. You can do that by setting the **SYSTEM_VERSIONING** clause to **OFF**.

## Setting SYSTEM_VERSIONING = OFF

Stop system-versioning if you want to perform specific maintenance operations on a temporal table or don't need a versioned table anymore. Because of this operation, you get two independent tables:

- Current table with a period definition

- History table as a regular table

### Important remarks

- History Table **stops** capturing the updates during **SYSTEM_VERSIONING = OFF**.
- No data loss happens on the **temporal table** when you set**SYSTEM_VERSIONING = OFF** or drop the **SYSTEM_TIME** period.
- When you set **SYSTEM_VERSIONING = OFF** and don't remove drop the **SYSTEM_TIME** period, the system continues to update the period columns for every insert and update operation. Deletes on the current table are permanent.
- Drop the **SYSTEM_TIME** period to delete the period columns.
- When you set, **SYSTEM_VERSIONING = OFF**, all users with sufficient permissions can modify the schema and content of the history table or even permanently delete the history table.
- You can't set **SYSTEM_VERSIONING = OFF** if you have other objects created with SCHEMABINDING using temporal query extensions - such as referencing **SYSTEM_TIME**. This restriction prevents these objects from failing if you set **SYSTEM_VERSIONING = OFF**.

### Permanently remove SYSTEM_VERSIONING

This example permanently removes SYSTEM_VERSIONING and deletes the period columns. Removing the period columns is optional.

```sql
ALTER TABLE dbo.Department SET (SYSTEM_VERSIONING = OFF);
/*Optionally, DROP PERIOD if you want to revert temporal table to a non-temporal*/
ALTER TABLE dbo.Department
DROP PERIOD FOR SYSTEM_TIME;
```

### Temporarily remove SYSTEM_VERSIONING

This is the list of operations that requires system-versioning to be set to **OFF**:

- Removing unnecessary data from history (**DELETE** or **TRUNCATE**)
- Removing data from current table without versioning (**DELETE**, **TRUNCATE**)
- Partition **SWITCH OUT** from the current table
- Partition **SWITCH IN** into the history table

This example temporarily stops SYSTEM_VERSIONING to allow you to perform specific maintenance operations. If you stop versioning temporarily as a prerequisite for table maintenance, we strongly recommend doing this inside a transaction to keep data consistency.

> [!NOTE]
> When turning system versioning back on, don't forget to specify the HISTORY_TABLE argument. Failing to do so results in a new history table being created and associated with the current table. The original history table can still exist as a normal table but won't be associated with the current table.

```sql
BEGIN TRAN
ALTER TABLE dbo.Department SET (SYSTEM_VERSIONING = OFF);
TRUNCATE TABLE [History].[DepartmentHistory]
WITH (PARTITIONS (1,2))
ALTER TABLE dbo.Department SET
(
SYSTEM_VERSIONING = ON (HISTORY_TABLE = History.DepartmentHistory)
);
COMMIT ;
```

## Next steps

- [Temporal Tables](../../relational-databases/tables/temporal-tables.md)
- [Getting Started with System-Versioned Temporal Tables](../../relational-databases/tables/getting-started-with-system-versioned-temporal-tables.md)
- [Manage Retention of Historical Data in System-Versioned Temporal Tables](../../relational-databases/tables/manage-retention-of-historical-data-in-system-versioned-temporal-tables.md)
- [System-Versioned Temporal Tables with Memory-Optimized Tables](../../relational-databases/tables/system-versioned-temporal-tables-with-memory-optimized-tables.md)
- [Creating a System-Versioned Temporal Table](../../relational-databases/tables/creating-a-system-versioned-temporal-table.md)
- [Modifying Data in a System-Versioned Temporal Table](../../relational-databases/tables/modifying-data-in-a-system-versioned-temporal-table.md)
- [Querying Data in a System-Versioned Temporal Table](../../relational-databases/tables/querying-data-in-a-system-versioned-temporal-table.md)
- [Changing the Schema of a System-Versioned Temporal Table](../../relational-databases/tables/changing-the-schema-of-a-system-versioned-temporal-table.md)
