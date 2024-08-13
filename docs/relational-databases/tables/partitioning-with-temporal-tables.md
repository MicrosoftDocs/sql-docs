---
title: Partition with temporal tables
description: Learn how to use table partitioning, on both the current and the history table independently.
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/29/2024
ms.service: sql
ms.subservice: table-view-index
ms.topic: conceptual
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Partition with temporal tables

[!INCLUDE [sqlserver2016-asdb-asdbmi](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi.md)]

You can use partitioning on both the current and the history table independently. However, partitioning can't be used to change the content of the data without system-versioning.

Partitioning is an Enterprise edition feature in [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] before Service Pack 1 and earlier versions. Partitioning is supported in all editions in [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] with Service Pack 1, and later versions.

## Partition temporal tables

This section describes how to use `SWITCH IN` and `SWITCH OUT` with temporal tables.

### Current table

`SWITCH IN` to the current table can be used to facilitate data loading and querying while `SYSTEM_VERSIONING` is `ON`.

`SWITCH OUT` isn't permitted while `SYSTEM_VERSIONING` is `ON`.

### History table

You can run `SWITCH OUT` from the history table while `SYSTEM_VERSIONING` is `ON`, to purge portions of history data that is no longer relevant.

`SWITCH IN` isn't allowed while `SYSTEM_VERSIONING` is `ON`, since it can invalidate temporal data consistency.

## Related content

- [Temporal tables](temporal-tables.md)
- [Get started with system-versioned temporal tables](getting-started-with-system-versioned-temporal-tables.md)
- [Temporal table system consistency checks](temporal-table-system-consistency-checks.md)
- [Temporal table considerations and limitations](temporal-table-considerations-and-limitations.md)
- [Temporal table security](temporal-table-security.md)
- [Manage retention of historical data in system-versioned temporal tables](manage-retention-of-historical-data-in-system-versioned-temporal-tables.md)
- [System-versioned temporal tables with memory-optimized tables](system-versioned-temporal-tables-with-memory-optimized-tables.md)
- [Temporal table metadata views and functions](temporal-table-metadata-views-and-functions.md)
