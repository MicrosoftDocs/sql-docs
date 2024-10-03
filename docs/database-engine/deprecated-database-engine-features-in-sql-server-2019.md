---
title: "Deprecated Database Engine features"
titleSuffix: "SQL Server 2019"
description: Find out about deprecated Database Engine features that are still available in SQL Server 2019 (15.x), but shouldn't be used in new applications.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 07/09/2024
ms.service: sql
ms.subservice: release-landing
ms.topic: conceptual
helpviewer_keywords:
  - "deprecated changes 2019 [SQL Server]"
monikerRange: ">=sql-server-ver15 || >=sql-server-linux-ver15"
---
# Deprecated Database Engine features in SQL Server 2019 (15.x)

[!INCLUDE [sqlserver2019](../includes/applies-to-version/sqlserver2019.md)]

[!INCLUDE [sssql19-md](../includes/sssql19-md.md)] doesn't deprecate any features beyond features deprecated in prior releases:

- [Deprecated Database Engine features in SQL Server 2017 (14.x)](deprecated-database-engine-features-in-sql-server-2017.md)
- [Deprecated Database Engine features in SQL Server 2016 (13.x)](deprecated-database-engine-features-in-sql-server-2016.md)

## Deprecation guidelines

When a feature is marked deprecated, it means:

- The feature is in maintenance mode only. No new changes are added, including changes related to addressing interoperability with new features.
- We strive not to remove a deprecated feature from future releases to make upgrades easier. However, under rare situations, we might choose to permanently discontinue (remove) the feature from [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] if it limits future innovations.
- For new development work, don't use deprecated features. For existing applications, plan to modify applications that currently use these features as soon as possible.

You can monitor the use of deprecated features by using the [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] Deprecated Features Object performance counter, or the `deprecation_announcement` and `deprecation_final_support` extended events. For more information, see [Use SQL Server Objects](../relational-databases/performance-monitor/use-sql-server-objects.md).

## Query deprecated features

The values of these counters are also available by executing the following statement:

[!INCLUDE [deprecated-os-performance-counters](../includes/deprecated-os-performance-counters.md)]

## Related content

- [Breaking changes to Database Engine features in SQL Server 2019](breaking-changes-to-database-engine-features-in-sql-server-2019.md)
- [Discontinued Database Engine functionality in SQL Server](discontinued-database-engine-functionality-in-sql-server.md)
