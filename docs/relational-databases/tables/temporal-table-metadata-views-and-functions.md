---
title: Temporal table metadata views and functions
description: A list of temporal table metadata views and functions.
author: rwestMSFT
ms.author: randolphwest
ms.date: 07/29/2024
ms.service: sql
ms.subservice: table-view-index
ms.topic: conceptual
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Temporal table metadata views and functions

[!INCLUDE [sqlserver2016-asdb-asdbmi](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi.md)]

[!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] and [!INCLUDE [sssds](../../includes/sssds-md.md)] include several metabase views and functions to enable administrators to retrieve information about temporal tables.

Information about temporal tables is exposed in the following metadata views:

- [sys.tables](../system-catalog-views/sys-tables-transact-sql.md)
- [sys.columns](../system-catalog-views/sys-columns-transact-sql.md)
- [sys.periods](../system-catalog-views/sys-periods-transact-sql.md)

Information about temporal tables is exposed in the following metadata functions:

- [OBJECTPROPERTY](../../t-sql/functions/objectproperty-transact-sql.md)
- [OBJECTPROPERTYEX](../../t-sql/functions/objectpropertyex-transact-sql.md)
- [COLUMNPROPERTY](../../t-sql/functions/columnproperty-transact-sql.md)

## Related content

- [Temporal tables](temporal-tables.md)
- [Get started with system-versioned temporal tables](getting-started-with-system-versioned-temporal-tables.md)
- [Temporal table system consistency checks](temporal-table-system-consistency-checks.md)
- [Partition with temporal tables](partitioning-with-temporal-tables.md)
- [Temporal table considerations and limitations](temporal-table-considerations-and-limitations.md)
- [Temporal table security](temporal-table-security.md)
- [Manage retention of historical data in system-versioned temporal tables](manage-retention-of-historical-data-in-system-versioned-temporal-tables.md)
- [System-versioned temporal tables with memory-optimized tables](system-versioned-temporal-tables-with-memory-optimized-tables.md)
