---
title: "Use Contained Databases with Availability Groups"
description: "Learn about the using a contained database with Always On availability groups in SQL Server 2019 (15.x)."
author: dimitri-furman
ms.author: dfurman
ms.reviewer: vanto
ms.date: 02/04/2026
ms.service: sql
ms.subservice: availability-groups
ms.topic: how-to
helpviewer_keywords:
  - "Availability Groups [SQL Server], interoperability"
  - "contained database, AlwaysOnAvailabilityGroups"
  - "contained database, Always On Availability Groups"
---
# Use contained databases with Always On availability groups

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

This article contains information about the using a contained database with [!INCLUDE [ssHADR](../../../includes/sshadr-md.md)] in [!INCLUDE [ssnoversion](../../../includes/ssnoversion-md.md)].

## Prerequisites

Before adding a contained database to an availability group, ensure that the `contained database authentication` server configuration option is set to `1` on every server instance that hosts an availability replica for the availability group.

For more information, see [Server Configuration: contained database authentication](../../configure-windows/contained-database-authentication-server-configuration-option.md).

## Related tasks

- [Server configuration options](../../configure-windows/server-configuration-options-sql-server.md)

## Related content

- [What is an Always On availability group?](overview-of-always-on-availability-groups-sql-server.md)
- [Contained Databases](../../../relational-databases/databases/contained-databases.md)
