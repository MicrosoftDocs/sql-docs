---
title: Tune compression for availability group
description: Learn how SQL Server compresses data streams for availability groups, which reduces network traffic, increases CPU load, and might induce latency.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 01/25/2024
ms.service: sql
ms.subservice: availability-groups
ms.topic: conceptual
---
# Tune compression for availability group

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

By default, SQL Server compresses data streams where appropriate for availability groups. Compression reduces network traffic, increases CPU load, and might induce latency. You must be a member of the **sysadmin** fixed server role to enable compression. The following table shows when SQL Server uses compression for availability group log streams:

| Scenario | Compression setting |
| --- | --- |
| Synchronous-commit replica | Not compressed |
| Asynchronous-commit replicas | Compressed |
| During automatic seeding | Not compressed |
| TDE enabled and asynchronous-commit in database | Compressed |
| TDE enabled and synchronous-commit in database | Not compressed |

## Trace flags for availability group compression

For most scenarios, we don't recommend changing these settings. You can use global trace flags to test changing these settings. SQL Server applies global trace flags to the entire instance. All of the availability groups in the instance are affected by these settings.

The following table shows trace flags that change the default compression behavior for SQL Server.

| Trace flag | Description |
| --- | --- |
| 1462 | Disables log stream compression for availability groups with asynchronous replicas. This feature is enabled by default on asynchronous replicas to optimize network bandwidth. |
| 9567 | Enables compression of the data stream for availability groups during automatic seeding. During automatic seeding, compression can significantly reduce the transfer time, and increase the load on the processor. |
| 9592 | Enables log stream compression for availability groups with synchronous replicas. This feature is disabled by default on synchronous replicas because compression adds latency. Log stream compression is enabled by default for asynchronous replicas. |

## Related content

- [Database Engine Service startup options](../../configure-windows/database-engine-service-startup-options.md)
- [Use automatic seeding to initialize an Always On availability group](automatically-initialize-always-on-availability-group.md)
- [Prerequisites, restrictions, and recommendations for Always On availability groups](prereqs-restrictions-recommendations-always-on-availability.md)
