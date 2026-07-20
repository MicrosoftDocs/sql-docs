---
title: "SQL Server Properties (Always On High Availability Tab)"
description: To use availability groups as a high-availability and disaster-recovery solution, turn on the Always On availability groups feature in SQL Server.
author: rwestMSFT
ms.author: randolphwest
ms.date: 12/15/2025
ms.service: sql
ms.subservice: tools-other
ms.topic: ui-reference
ms.collection:
  - data-tools
monikerRange: ">=sql-server-2017"
---
# SQL Server Properties (Always On High Availability tab)

[!INCLUDE [SQL Server Windows Only](../../includes/applies-to-version/sql-windows-only.md)]

Use the **Always On High Availability** tab of the **SQL Server Properties** dialog box in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager to enable or disable the Always On availability groups (AGs) feature in [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)]. Enabling AGs is a prerequisite for an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to use availability groups as a high availability and disaster recovery solution.

## Prerequisites

To support availability groups, a server instance must meet the following prerequisites:

- The server instance must reside on a Windows Server Failover Clustering (WSFC) node.

- To be in the same availability group, instances must be in the same WSFC cluster. An availability group can't span WSFC clusters.

- The server instance must be running an edition of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] that supports [!INCLUDE [ssHADR](../../includes/sshadr-md.md)].

- Enable AGs for only one server instance at a time. After enabling AGs, wait until the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] service restarts before you enable the next server instance.

For information about feature support and for information about additional prerequisites, restrictions, and recommendations for [!INCLUDE [ssHADR](../../includes/sshadr-md.md)], see [Business continuity and database recovery - SQL Server](../../database-engine/sql-server-business-continuity-dr.md).

For more information about availability group support in SQL Server on Linux, see [Availability groups for SQL Server on Linux](../../linux/sql-server-linux-availability-group-overview.md).

## Options

#### Windows failover cluster name

Shows the name of the WSFC cluster where the local computer acts as a node.

#### Enable Always On Availability Groups

Use this check box to enable or disable AGs on this instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

> [!TIP]  
> If the **Always On Availability Groups** tab is missing, or the option to enable it is grayed out, either your environment isn't [supported](../../database-engine/availability-groups/windows/prereqs-restrictions-recommendations-always-on-availability.md), or it hasn't been properly configured.

| Current state | Description | Action |
| --- | --- | --- |
| Check box is currently **empty** | Always On Availability Groups is currently **disabled**. | To enable AGs, select this check box, select **OK**, and manually restart the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] service. |
| Check box is currently **selected** | Always On Availability Groups is currently **enabled**. | To disable AGs, clear the check box and select **OK**. This action restarts the server instance. |

After you disable Always On Availability Groups, remove any local availability replicas from the server instance. If you remove the last replica of a given availability group, remove the group as well.

## Related content

- [Getting Started with Always On Availability Groups](../../database-engine/availability-groups/windows/getting-started-with-always-on-availability-groups-sql-server.md)
- [Business continuity and database recovery - SQL Server](../../database-engine/sql-server-business-continuity-dr.md)
- [Availability groups for SQL Server on Linux](../../linux/sql-server-linux-availability-group-overview.md)
