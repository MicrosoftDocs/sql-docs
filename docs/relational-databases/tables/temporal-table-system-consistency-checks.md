---
title: Temporal table system consistency checks
description: Learn how the system performs several consistency checks to ensure the schema complies with the requirements for temporal data.
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/29/2024
ms.service: sql
ms.subservice: table-view-index
ms.topic: conceptual
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Temporal table system consistency checks

[!INCLUDE [sqlserver2016-asdb-asdbmi](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi.md)]

With temporal tables, the system performs several consistency checks to ensure the schema complies with the requirements for temporal and the data is consistent, and remains consistent. In addition, temporal checks are available in the `DBCC CHECKCONSTRAINTS` statement.

## System consistency checks

Before `SYSTEM_VERSIONING` is set to `ON`, a set of checks is performed on the history table and the current table. These checks are grouped into schema checks and data checks (if history table isn't empty). In addition, the system also performs a runtime consistency check.

### Schema check

When creating or alter a table to become a temporal table, the system verifies that requirements are met:

1. The names and number of columns is the same in both the current table and the history table.

1. The datatypes match for each column between the current table and the history table.

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

When creating a link to an existing history table, you can choose to perform a data consistency check. This data consistency check ensures that existing records don't overlap and that temporal requirements are fulfilled for every individual record. Performing the data consistency check is the default. You should perform the data consistency check whenever the data between the current and history tables might be out of sync. For example, when incorporating an existing history table that is populated with history data.

> [!WARNING]  
> Manual changes to the system clock will cause the system to fail unexpectedly, because the runtime data consistency checks to prevent overlap conditions (namely that the end time for a record isn't less than its start time) fail.

## Use DBCC CHECKCONSTRAINTS

The `DBCC CHECKCONSTRAINTS` command includes temporal data consistency checks. For more information, see [DBCC CHECKCONSTRAINTS](../../t-sql/database-console-commands/dbcc-checkconstraints-transact-sql.md).

## Related content

- [Temporal tables](temporal-tables.md)
- [Get started with system-versioned temporal tables](getting-started-with-system-versioned-temporal-tables.md)
- [Partition with temporal tables](partitioning-with-temporal-tables.md)
- [Temporal table considerations and limitations](temporal-table-considerations-and-limitations.md)
- [Temporal table security](temporal-table-security.md)
- [Manage retention of historical data in system-versioned temporal tables](manage-retention-of-historical-data-in-system-versioned-temporal-tables.md)
- [System-versioned temporal tables with memory-optimized tables](system-versioned-temporal-tables-with-memory-optimized-tables.md)
- [Temporal table metadata views and functions](temporal-table-metadata-views-and-functions.md)
