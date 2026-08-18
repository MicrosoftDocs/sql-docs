---
title: Partition with Temporal Tables
description: Learn how to use table partitioning, on both the current and the history table independently.
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
# Partition with temporal tables

[!INCLUDE [sqlserver2016-asdb-asdbmi-fabricsqldb](../../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-fabricsqldb.md)]

You can use partitioning on both the current and the history table independently. However, you can't use partitioning to change the data content without system-versioning.

Partitioning is an Enterprise edition feature in [!INCLUDE [sssql16-md](../../../includes/sssql16-md.md)] before Service Pack 1 and earlier versions. Partitioning is supported in all editions in [!INCLUDE [sssql16-md](../../../includes/sssql16-md.md)] with Service Pack 1, and later versions.

## Partition temporal tables

This section describes how to use `SWITCH IN` and `SWITCH OUT` with temporal tables.

### Current table

You can use `SWITCH IN` to the current table to help load and query data while `SYSTEM_VERSIONING` is `ON`.

`SWITCH OUT` isn't allowed while `SYSTEM_VERSIONING` is `ON`.

### History table

You can run `SWITCH OUT` from the history table while `SYSTEM_VERSIONING` is `ON`, to purge history data that's no longer relevant.

`SWITCH IN` isn't allowed while `SYSTEM_VERSIONING` is `ON`, because it can invalidate temporal data consistency.

## Related content

- [Temporal tables](overview.md)
- [Get started with system-versioned temporal tables](get-started.md)
- [Temporal table system consistency checks](consistency-checks.md)
- [Temporal table considerations and limitations](considerations-limitations.md)
- [Temporal table security](security.md)
- [Manage retention of historical data in system-versioned temporal tables](manage-retention.md)
- [System-versioned temporal tables with memory-optimized tables](memory-optimized.md)
- [Temporal table metadata views and functions](metadata-views-functions.md)
