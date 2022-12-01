---
title: Copy or move a database (preview)
titleSuffix: Azure SQL Managed Instance
description: Learn how to perform an online move or copy operation of your database across instances for Azure SQL Managed Instance. 
author: sasapopo 
ms.author: sasapopo
ms.reviewer: mathoma, danil
ms.date: 11/16/2022
ms.service: sql-managed-instance
ms.subservice: data-movement
ms.topic: how-to
ms.custom: 
---
# Copy or move a database (preview) - Azure SQL Managed Instance 
[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

This article describes how to copy or move a database online across instances in Azure SQL Managed Instance. 

> [!NOTE]
> The copy and move feature for Azure SQL Managed Instance is currently in preview. 

## Overview

You can perform an online copy or move operation of a database across managed instances by using Always On availability group technology. The copy and move feature creates a new database on the destination instance as a copy of the source database. With this feature, data replication is continuous, asynchronous, and near real-time, and it ensures that there's no data loss. 

When you *copy* a database, the source database remains online during the operation and after it finishes. 

Conversely, when you *move* a database, the source database gets dropped after the operation finishes. 

You can run multiple database copy and move operations from the source managed instance to one or more target instances. 

Copying and moving a database is different from point-in-time restore (PITR), because it creates a copy of the database after the operation finishes. PITR creates a copy of the database from a specified moment in the past. 

> [!IMPORTANT]
> When you move a database to a new destination, existing PITR backups don't move with the database, and they're not available. The database starts a new backup chain on the destination instance the moment the move operation is completed. 

## When to use the feature 

Moving or copying a database is useful when you want to: 

- Manage database growth and performance requirements.
- Balance workloads across multiple managed instances. 
- Move databases to an instance with more available resources to handle the workload.
- Consolidate multiple databases from a number of smaller instances. 
- Create database parity between dev, test, and production environments. 

## Workflow 

Here's the workflow for copying or moving a database: 

1. Choose the database, source managed instance, and destination instance, and then start the operation. 
   
   The database gets seeded to the destination server. Check the status to determine whether the operation is in progress or whether it has succeeded. 
1. After the seeding finishes, the operation state shows as *ready for completion*. 

   Until the operation is complete, all changes that happen to the source database are applied to the destination database. You can cancel the operation at any time. You have 24 hours to explicitly complete the operation. If you don't complete the operation within 24 hours, it's automatically canceled, and the destination database is dropped. 
1. When the operation is completed, your destination database comes online and is ready for read/write workloads. 
1. If you've chosen to *move* the database, the source database gets dropped. If you've chosen to *copy* the database, the source database remains online, but data replication stops. 

An example workflow for a move operation is illustrated in the following diagram: 

:::image type="content" source="media/database-copy-move-how-to/database-move-diagram.png" alt-text="Diagram that illustrates the workflow of a move operation.":::

## Prerequisites 

Before you can copy or move a database, you must meet the following requirements: 

- You must have *read* permissions for the resource group that contains the source managed instance, and you must have *write* permissions at the database level for both the source and destination instances. 
- If the source and destination instances are in different virtual networks, there must be network connectivity between the virtual networks of the two instances, such as with Azure virtual network peering. 

## Copy or move database 

You can copy or move a database to another managed instance by using the Azure portal. To do so: 

1. Go to your managed instance in the [Azure portal](https://portal.azure.com).
1. Under **Data management**, select **Databases**.
1. Select one or more databases, and then select either the **Copy** or **Move** option at the top of the pane. 

    Selecting **Move** drops the source database when the operation finishes, and selecting **Copy** leaves the source database online when the operation finishes. Selecting either option opens the **Move Managed Database** or **Copy Managed Database** page. After the page opens, you can select additional databases to include in the operation. 

   :::image type="content" source="media/database-copy-move-how-to/start-move-copy-operation.png" alt-text="Screenshot of the 'Databases' page for Azure SQL Managed Instance, with the 'Move' and 'Copy' options highlighted.":::

1. On the **Source details** pane, provide details for the source database and managed instance. 
1. On the **Destination details** pane, provide details for the destination managed instance. 
1. Select **Review + Start** to validate your source and destination details, and then select **Start** to begin the operation. 

   Selecting **Start** takes you back to the **Databases** page of your instance, where you can monitor the progress of the operation. 
1. On the **Databases** page, check the **Operation details** pane to verify that the status of your operation is *Move in progress* or *Copy in progress*. 

   If you need to cancel, select **In progress**, select the database you're working with, and then select **Cancel operation** to stop the seeding and drop the destination database. 

   :::image type="content" source="media/database-copy-move-how-to/copy-in-progress.png" alt-text="Screenshot of the 'Databases' page for Azure SQL Managed Instance, showing that a copy operation is in progress.":::

1. Monitor the operation. After the seeding finishes, the **Operation details** pane displays a status of *Move ready for completion* or *Copy ready for completion*. 
1. Select **Ready for completion** to open the **Operation details** pane, choose the database that you're ready to copy or move, and then select **Complete** to finalize the operation and bring the destination database online. 

    Changes made to the source database are replicated to the destination database during this time, until you select **Complete**. If you don't complete the operation within 24 hours, it's automatically canceled, and the destination database is dropped. Selecting **Complete** finalizes the operation and takes you back to the **Databases** page, where you can verify that the operation is complete.
    
    If you've moved the database, the database name is unavailable because it's now offline. 


## Limitations

Consider the following limitations of the copy and move feature:

- The source and destination instances can't be the same. 
- Both the source instance and destination instance need to be in the same Azure subscription and same region. 
- You can copy and move *user* databases only. Copying and moving *system* databases isn't supported. 
- A database can participate in only a single move or copy operation at a time. 
- The source instance can run up to eight copy or move operations at a time. You can start more than eight operations, but some will be queued and processed later, as managed by the service. 
- You can't rename a database during a copy or move operation. 
- Database copy and move operations don't copy or move PITR backups. 
- You can't copy or move a database that's part of an [auto-failover group](auto-failover-group-sql-mi.md) or that's using the [Azure SQL Managed Instance link](managed-instance-link-feature-overview.md). 
- You'll need to reconfigure transactional replication, change data capture (CDC), or distributed transactions after you move a database that relies on these features. 

## Next steps

For other data movement options, review: 

- [Managed Instance link](managed-instance-link-feature-overview.md)
- [Transactional replication](replication-transactional-overview.md)
- [Log Replay Service](log-replay-service-overview.md)