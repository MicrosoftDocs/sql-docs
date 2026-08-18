---
title: Temporal Table Metadata Views and Functions
description: A list of temporal table metadata views and functions.
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
# Temporal table metadata views and functions

[!INCLUDE [sqlserver2016-asdb-asdbmi-fabricsqldb](../../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-fabricsqldb.md)]

[!INCLUDE [ssnoversion](../../../includes/ssnoversion-md.md)] and [!INCLUDE [sssds](../../../includes/sssds-md.md)] include several metadata views and functions that administrators use to retrieve information about temporal tables.

The following metadata views expose information about temporal tables:

- [sys.tables](../../system-catalog-views/sys-tables-transact-sql.md)
- [sys.columns](../../system-catalog-views/sys-columns-transact-sql.md)
- [sys.periods](../../system-catalog-views/sys-periods-transact-sql.md)

The following metadata functions expose information about temporal tables:

- [OBJECTPROPERTY](../../../t-sql/functions/objectproperty-transact-sql.md)
- [OBJECTPROPERTYEX](../../../t-sql/functions/objectpropertyex-transact-sql.md)
- [COLUMNPROPERTY](../../../t-sql/functions/columnproperty-transact-sql.md)

## Related content

- [Temporal tables](overview.md)
- [Get started with system-versioned temporal tables](get-started.md)
- [Temporal table system consistency checks](consistency-checks.md)
- [Partition with temporal tables](partitioning.md)
- [Temporal table considerations and limitations](considerations-limitations.md)
- [Temporal table security](security.md)
- [Manage retention of historical data in system-versioned temporal tables](manage-retention.md)
- [System-versioned temporal tables with memory-optimized tables](memory-optimized.md)
