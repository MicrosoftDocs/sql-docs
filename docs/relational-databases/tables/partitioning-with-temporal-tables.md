---
description: "Partitioning with Temporal Tables"
title: "Partitioning with Temporal Tables | Microsoft Docs"
ms.custom: ""
ms.date: "04/26/2016"
ms.service: sql
ms.reviewer: ""
ms.subservice: table-view-index
ms.topic: conceptual
ms.assetid: 313714b8-4ad1-4c14-93a3-7f628a334a51
author: markingmyname
ms.author: maghan
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Partitioning with temporal tables


[!INCLUDE [sqlserver2016-asdb-asdbmi](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi.md)]


You can use partitioning on both the current and the history table independently. However, partitioning cannot be used to change the content of the data without system-versioning.

> [!NOTE]
> Partitioning is an Enterprise Edition feature in SQL Server 2016 prior to Service Pack 1 and earlier versions. Partitioning is supported in all editions in SQL Server 2016 Service Pack 1 and later versions.

- **Current Table:**

  - **SWITCH IN** to the current table can be used to facilitate data loading and querying while **SYSTEM_VERSIONING** is **ON**
  - **SWITCH OUT** is not permitted while **SYSTEM_VERSIONING** is **ON**

- **History Table:**

  - **SWITCH OUT** from history table can performed while **SYSTEM_VERSIONING** is **ON** to purge portions of history data that is no longer relevant.
  - **SWITCH IN** is not allowed while **SYSTEM_VERSIONING** is **ON** since it can invalidate temporal data consistency.

## Next steps

- [Temporal Tables](../../relational-databases/tables/temporal-tables.md)
- [Getting Started with System-Versioned Temporal Tables](../../relational-databases/tables/getting-started-with-system-versioned-temporal-tables.md)
- [Temporal Table System Consistency Checks](../../relational-databases/tables/temporal-table-system-consistency-checks.md)
- [Temporal Table Considerations and Limitations](../../relational-databases/tables/temporal-table-considerations-and-limitations.md)
- [Temporal Table Security](../../relational-databases/tables/temporal-table-security.md)
- [Manage Retention of Historical Data in System-Versioned Temporal Tables](../../relational-databases/tables/manage-retention-of-historical-data-in-system-versioned-temporal-tables.md)
- [System-Versioned Temporal Tables with Memory-Optimized Tables](../../relational-databases/tables/system-versioned-temporal-tables-with-memory-optimized-tables.md)
- [Temporal Table Metadata Views and Functions](../../relational-databases/tables/temporal-table-metadata-views-and-functions.md)
