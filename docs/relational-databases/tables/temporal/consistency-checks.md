---
title: Temporal Table System Consistency Checks
description: Learn how the system performs several consistency checks to ensure the schema complies with the requirements for temporal data.
author: rwestMSFT
ms.author: randolphwest
ms.date: 08/18/2026
ms.service: sql
ms.subservice: table-view-index
ms.topic: concept-article
ms.custom:
  - ignite-2025
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# Temporal table system consistency checks

[!INCLUDE [sqlserver2016-asdb-asdbmi-fabricsqldb](../../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-fabricsqldb.md)]

With temporal tables, the system performs several consistency checks. These checks ensure that the schema complies with temporal requirements and that the data is consistent and remains consistent. The `DBCC CHECKCONSTRAINTS` statement also provides temporal checks.

## System consistency checks

Before `SYSTEM_VERSIONING` is set to `ON`, a set of checks is performed on the history table and the current table. These checks fall into schema checks and data checks (if the history table isn't empty). The system also performs a runtime consistency check.

### Schema check

When you create or alter a table to become a temporal table, the system verifies that these requirements are met:

1. The names and number of columns are the same in both the current table and the history table.

1. The data types match for each column between the current table and the history table.

1. The period columns are set to `NOT NULL`.

1. The current table has a primary key constraint and the history table doesn't have a primary key constraint.

1. No `IDENTITY` columns are defined in the history table.

1. No triggers are defined in the history table.

1. No foreign keys are defined in the history table.

1. No table or column constraints are defined on the history table. However, default column values on the history table are permitted.

1. The history table isn't placed in a read-only filegroup.

1. The history table isn't configured for change tracking or change data capture.

### Data consistency check

Before `SYSTEM_VERSIONING` is set to `ON` and as part of any data manipulation language (DML) operation, the system performs the following check: `ValidTo >= ValidFrom`

When you create a link to an existing history table, you can choose to perform a data consistency check. This data consistency check ensures that existing records don't overlap and that every record meets temporal requirements. The data consistency check runs by default. Perform the data consistency check whenever the data between the current and history tables might be out of sync. For example, perform it when you incorporate an existing history table that already contains history data.

> [!WARNING]  
> Manual changes to the system clock cause the system to fail unexpectedly, because the runtime data consistency checks that prevent overlap conditions (namely that the end time for a record isn't less than its start time) fail.

## Use DBCC CHECKCONSTRAINTS

The `DBCC CHECKCONSTRAINTS` command includes temporal data consistency checks. For more information, see [DBCC CHECKCONSTRAINTS](../../../t-sql/database-console-commands/dbcc-checkconstraints-transact-sql.md).

## Related content

- [Temporal tables](overview.md)
- [Get started with system-versioned temporal tables](get-started.md)
- [Partition with temporal tables](partitioning.md)
- [Temporal table considerations and limitations](considerations-limitations.md)
- [Temporal table security](security.md)
- [Manage retention of historical data in system-versioned temporal tables](manage-retention.md)
- [System-versioned temporal tables with memory-optimized tables](memory-optimized.md)
- [Temporal table metadata views and functions](metadata-views-functions.md)
