---
title: Get Started with System-Versioned Temporal Tables
description: Learn how to get started with system-versioned temporal tables.
author: rwestMSFT
ms.author: randolphwest
ms.date: 08/18/2026
ms.service: sql
ms.subservice: table-view-index
ms.topic: how-to
ms.custom:
  - intro-get-started
  - ignite-2025
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric-sqldb"
---
# Get started with system-versioned temporal tables

[!INCLUDE [sqlserver2016-asdb-asdbmi-fabricsqldb](../../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-fabricsqldb.md)]

Depending on your scenario, you can either create new system-versioned temporal tables, or modify existing ones by adding temporal attributes to the existing table schema. When you modify data in a temporal table, the system builds version history transparently to applications and end users. As a result, temporal tables don't require any change to how you modify the table or query the latest (current) state of the data.

In addition to regular data modification and querying, temporal tables also provide easy ways to get insights from data history through extended Transact-SQL syntax. Every system-versioned table has an assigned history table, which is transparent to users. However, you can optimize workload performance, or the storage footprint, by creating more indexes or choosing different storage options.

The following diagram shows a typical workflow with temporal tables:

:::image type="content" source="media/get-started/workflow.svg" alt-text="Diagram of getting started with temporal tables.":::

This section is divided into the following five articles:

- [Create a system-versioned temporal table](create.md)
- [Modify data in a system-versioned temporal table](modify-data.md)
- [Query data in a system-versioned temporal table](query-data.md)
- [Change the schema of a system-versioned temporal table](change-schema.md)
- [Stop system-versioning on a system-versioned temporal table](stop-system-versioning.md)

## Related content

- [Temporal tables](overview.md)
- [Temporal table system consistency checks](consistency-checks.md)
- [Partition with temporal tables](partitioning.md)
- [Temporal table considerations and limitations](considerations-limitations.md)
- [Temporal table security](security.md)
- [Manage retention of historical data in system-versioned temporal tables](manage-retention.md)
- [System-versioned temporal tables with memory-optimized tables](memory-optimized.md)
- [Temporal table metadata views and functions](metadata-views-functions.md)
