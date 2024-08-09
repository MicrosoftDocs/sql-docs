---
title: Get started with system-versioned temporal tables
description: Learn how to get started with system-versioned temporal tables.
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/29/2024
ms.service: sql
ms.subservice: table-view-index
ms.topic: conceptual
ms.custom:
  - intro-get-started
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Get started with system-versioned temporal tables

[!INCLUDE [sqlserver2016-asdb-asdbmi](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi.md)]

Depending on your scenario, you can either create new system-versioned temporal tables, or modify existing ones by adding temporal attributes to the existing table schema. When the data in temporal table is modified, the system builds version history transparently to applications and end users. As a result, working with temporal tables doesn't require any change to the way table is modified or how the latest (current) state of the data is queried.

In addition to regular data modification and querying, temporal tables also provide convenient and easy ways to get insights from data history through extended Transact-SQL syntax. Every system-versioned table has a history table assigned, which is transparent to users. However, you can optimize workload performance, or the storage footprint, by creating more indexes or choosing different storage options.

The following diagram depicts typical workflow with temporal tables:

:::image type="content" source="media/getting-started-with-system-versioned-temporal-tables/getting-started-with-temporal.svg" alt-text="Diagram of getting started with temporal tables.":::

This section is divided into the following five articles:

- [Create a system-versioned temporal table](creating-a-system-versioned-temporal-table.md)
- [Modify data in a system-versioned temporal table](modifying-data-in-a-system-versioned-temporal-table.md)
- [Query data in a system-versioned temporal table](querying-data-in-a-system-versioned-temporal-table.md)
- [Change the schema of a system-versioned temporal table](changing-the-schema-of-a-system-versioned-temporal-table.md)
- [Stop system-versioning on a system-versioned temporal table](stopping-system-versioning-on-a-system-versioned-temporal-table.md)

## Related content

- [Temporal tables](temporal-tables.md)
- [Temporal table system consistency checks](temporal-table-system-consistency-checks.md)
- [Partition with temporal tables](partitioning-with-temporal-tables.md)
- [Temporal table considerations and limitations](temporal-table-considerations-and-limitations.md)
- [Temporal table security](temporal-table-security.md)
- [Manage retention of historical data in system-versioned temporal tables](manage-retention-of-historical-data-in-system-versioned-temporal-tables.md)
- [System-versioned temporal tables with memory-optimized tables](system-versioned-temporal-tables-with-memory-optimized-tables.md)
- [Temporal table metadata views and functions](temporal-table-metadata-views-and-functions.md)
