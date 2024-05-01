---
title: "Deprecated database engine features in SQL Server 2022"
description: Deprecated database engine features in SQL Server 2022.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 12/07/2023
ms.service: sql
ms.subservice: release-landing
ms.topic: conceptual
helpviewer_keywords:
  - "deprecated changes 2022 [SQL Server]"
monikerRange: ">=sql-server-ver16 || >=sql-server-linux-ver16"
---
# Deprecated database engine features in SQL Server 2022 (16.x)

[!INCLUDE [sqlserver2022](../includes/applies-to-version/sqlserver2022.md)]

[!INCLUDE [sssql22-md](../includes/sssql22-md.md)] deprecates:

- Distributed Replay
- Machine Learning server
- Stretch Database

Features that were deprecated in prior releases are also deprecated in [!INCLUDE [sssql22-md](../includes/sssql22-md.md)]:

- [Deprecated database engine features in SQL Server 2019 (15.x)](deprecated-database-engine-features-in-sql-server-2019.md)
- [[!INCLUDE [sssql17-md](../includes/sssql17-md.md)]](deprecated-database-engine-features-in-sql-server-2017.md)
- [[!INCLUDE [sssql16-md](../includes/sssql16-md.md)]](deprecated-database-engine-features-in-sql-server-2016.md)

## Deprecation guidelines

When a feature is marked deprecated, it means:

- The feature is in maintenance mode only. No new changes are made, including changes to address interoperability with new features.

- We strive not to remove a deprecated feature from future releases to make upgrades easier. However, under rare situations, we might choose to permanently discontinue (remove) the feature from [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] if it limits future innovations.

- For new development work, don't use deprecated features. For existing applications, plan to modify applications that currently use these features as soon as possible.

You can monitor the use of deprecated features by using the [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)] Deprecated Features Object performance counter, or the `deprecation_announcement`  and `deprecation_final_support` extended events. For more information, see [Use SQL Server Objects](../relational-databases/performance-monitor/use-sql-server-objects.md).

## Query deprecated features

The values of these counters are also available by executing the following statement:

[!INCLUDE [deprecated-os-performance-counters](../includes/deprecated-os-performance-counters.md)]

## Related content

- [Discontinued Database Engine functionality in SQL Server](discontinued-database-engine-functionality-in-sql-server.md)
