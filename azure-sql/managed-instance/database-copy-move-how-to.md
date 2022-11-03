---
title: Database copy and move
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

This article teaches you how copy or move your database online across instances in Azure SQL Managed Instance. 

The copy and move feature is currently in preview. 

## Overview

Perform an online copy or move operation of your database across managed instances by leveraging Always On availability group technology. The feature creates a new database on the destination instance as a copy of another database at the moment the operation completes. With database copy and move, data replication is continuous, asynchronous, near real-time and ensures there's no data loss. 

When you copy your database, the source database remains online during and after the operation completes. Conversely, when you move your database, the source database gets dropped once the operation completes. You can run multiple database copy and move operations from the source managed instance to one or several target instances. 

Copying and moving your database is different from point-in-time restore (PITR) as it creates a copy of the database when the operation completes as opposed to PITR, which creates a copy of the database from some moment in the past. 

> [!IMPORTANT]
> When a database moves to a new destination, existing PITR backups do not move with the database, and will not be available. The database starts a new backup chain on the destination instance the moment the move operation completes. 

## When to use 

Moving or copying your database is useful to: 

- Manage database growth and performance requirements
- Balance workloads across multiple managed instances. 
- Move databases to an instance with more available resources to handle the workload 
- Consolidate multiple databases from a a number of smaller instances. 
- Create database parity between dev, test, and production environments. 

## Workflow 

The following details the workflow when you move or copy the database: 

1. Choose the database, source managed instance, destination instance, and start the operation. 
1. The database gets seeded to the destination server. Check the status to determine if the operation is in progress, or if it has succeeded. 
1. After seeding completes, the operation state shows as **ready for completion**. You have 24 hours to explicitly complete the operation. Until the operation completes, all changes that happen to the source database are applied to the destination database. 
1. At the moment you complete the operation, your destination database comes online and is ready for read/write workloads. 
1. If you chose to move the database, the source database gets dropped. If you chose to copy the database, the source database remains online, but data replication stops. 

## Requirements 

To copy or move a database, you need the following: 

- The user performing the operation must have read rights for the resource group containing the source managed instance, and write permissions at the database level for both source and destination instances. 
- If the source and destination instances are in different virtual networks, there must be network connectivity in place between the virtual networks of the two instances, such as with Azure VNet peering. 


## Copy or move database 

You can copy or move your database to another managed instance by using the Azure portal. 

To copy or move your database, follow these steps: 

1. Go to your managed instance in the [Azure portal](https://portal.azure.com).
1. Select **Databases** under data management, and then choose either the **Copy** or **Move** options at the top navigation bar. Choosing **Move** drops the source database when the operation completes, whereas **Copy** leaves the source database online when the operation completes. 

   :::image type="content" source="media/database-copy-move-how-to/start-move-copy-operation.png" alt-text="Screenshot of the Azure portal, databases page for Azure SQL Managed Instance, with move and copy highlighted. ":::

1. Selecting either option opens the **Move Managed Database** or the **Copy Managed Database** page. 
1. You can select additional databases to include in the operation on either page. 
1. Provide details for the source database and managed instance on the **Source details** tab. 
1. Provide details for the destination managed instance on the **Destination details** tab. 
1. Select **Review + Start** to validate your source and destination details, and then select **Start** to begin the operation. 
1. Selecting **Start** takes you back to the **Databases** page of your instance, where you can monitor the progress of the operation under **Operation details**. 
1. During seeding, the **Operation details** displays **Move/Copy in progress**. 
1. After seeding completes, the **Operation details** displays **Move/Copy ready for completion**. 
1. Select **Ready for completion** to open the operation details pane, choose the database(s) you're ready to copy or move, and then select **Complete** once you're ready to finalize the operation and bring the destination database online. Changes made to the source database are replicated to the destination database during this time, until you select **Complete**. 
1. Selecting **Complete** finalizes the operation and takes you back to the **Databases** page, where you can see that the operation completed, and, if  you chose move, the database will be grayed out as it's now offline. 


## Limitations

Consider the following limitations:

- The source and destination managed instance cannot be the same. 
- Both source and destination managed instances need to be in the same Azure subscription and same region. 
- You can only copy and move user databases. Copying and moving system databases is not supported. 
- A database can only participate in a single move or copy operation at a time. 
- The source managed instance can run up to 8 copy or move operations at a time. You can start more than 8 operations, but some will be queued and processed later, as managed by the service. 
- You can't rename a database during a copy or move operation. 
- Database copy and move does not copy or move PITR backups. 
- You can't copy or move a database that is part of an [auto-failover group](auto-failover-group-sql-mi.md), or using the [Managed Instance link](managed-instance-link-feature-overview.md). 
- You will need to reconfigure transactional replication, change data capture (CDC), or distributed transactions after you move a database that relies on these features. 

## Next steps

For other data movement options, review: 

- [Managed Instance link](managed-instance-link-feature-overview.md)
- [Transactional Replication](replication-transactional-overview.md)
- [Log Replay Service](log-replay-service-overview.md)
