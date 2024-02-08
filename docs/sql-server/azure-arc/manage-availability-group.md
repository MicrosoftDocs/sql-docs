---
title: Always On availability groups inventory and status (preview)
description: Describes how to view Always On availability groups and their status in Azure portal
author: AbdullahMSFT
ms.author: amamun 
ms.reviewer: mikeray, randolphwest
ms.date: 10/20/2023
ms.topic: conceptual
ms.custom: ignite-2023
---

# Always On availability groups inventory and status (preview)

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

An Always On availability group is an enterprise level high availability and disaster recovery solution for SQL Server. This article describes how to view a list of availability groups and their real time health status for a given [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)], in Azure portal.

## View list of availability groups and Status

Follow the steps to view the availability groups that are configured for the [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)]:

1. In your Azure portal, browse to the overview page of the SQL Server enabled by Azure Arc
1. Select **Availability Groups**

   Azure portal will display the availability groups configured for the SQL Server instance on the right

1. Select the availability group that you want to review

Azure portal displays the health and status of the Always on Availability Group similar to the Availability Group dashboard shown in SQL Server Management Studio. This includes:

- The current primary replica
- Availability group state
- Availability group replicas
- Failover mode
- Synchronization state

When the availability group (AG) dashboard loads, fetching the dashboard details is done via a roundtrip down to the [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)] instance. The Arc SQL extension agent connects to the SQL Server, queries the AG related DMV (Dynamic Management Views) metadata, and sends the information back to the Azure portal where it is displayed.

## Failover

Always On availability groups support different failover modes depending on the data synchronization mode.

- Synchronous-commit mode supports two forms of failover
  - Planned manual failover
  - Automatic failover
- Asynchronous-commit mode supports forced manual failover (with possible data loss), typically called, forced failover

SQL Server enabled by Azure Arc supports planned manual failover.

Follow the steps below to initiate a planned manual failover:

1. In Azure portal, browse to the overview page of the SQL Server for the secondary replica where you want to fail over
1. Select **Availability Groups** tab on the left

   Azure portal will display the availability groups configured for the SQL Server instance on the right

1. Select the availability group that you want to perform a failover on
1. In the Availability Group dashboard on the right, select **Failover**

This will initiate a planned, manual failover on the AG replica.

### Considerations

- Currently, manual planned failover is the only mode of failover supported
- [!INCLUDE [ssazurearc](../../includes/ssazurearc.md)] features are not supported for availability groups on failover cluster instances.

## Related content

- [What is an Always On availability group?](../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)
- [View SQL Server databases - Azure Arc](view-databases.md)
- [Recovery Models (SQL Server)](../../relational-databases/backup-restore/recovery-models-sql-server.md)
