---
title: Replication subscribers and Always On availability groups (SQL Server)
description: Learn what happens if an Always On availability group containing a database that is a replication subscriber fails over in SQL Server.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 03/13/2023
ms.service: sql
ms.subservice: availability-groups
ms.topic: conceptual
helpviewer_keywords:
  - "failover subscribers with AlwaysOn"
  - "failover subscribers with Always On"
  - "Availability Groups [SQL Server], interoperability"
  - "replication [SQL Server], AlwaysOn Availability Groups"
  - "replication [SQL Server], Always On Availability Groups"
---
# Replication subscribers and Always On availability groups (SQL Server)

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

When an Always On availability group (AG) fails over, containing a database that is a replication subscriber, the replication subscription might fail. For transactional replication push subscribers, the distribution agent will continue to replicate automatically after a failover if the subscription was created using the AG listener name. For transactional replication pull subscribers, the distribution agent will continue to replicate automatically after a failover, if the subscription was created using the AG listener name and the original subscriber server is up and running. This is because the distribution agent jobs only get created on the original subscriber (primary replica of the AG). For merge subscribers, a replication administrator must manually reconfigure the subscriber, by recreating the subscription.

## What is supported

[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] replication supports the automatic failover of the publisher and the automatic failover of transactional subscribers. Merge subscribers can be part of an AG, however manual actions are required to configure the new subscriber after a failover. AGs can't be combined with WebSync and SQL Server Compact scenarios.

## Create a transactional subscription in an availability group

For transactional replication, use the following steps to configure and fail over a subscriber AG:

1. Before creating the subscription, add the subscriber database to the appropriate AG.

1. Add the subscriber's AG listener as a linked server to all nodes of the AG. This step ensures that all potential failover partners are aware of and can connect to the listener.

1. Using the script in the [Create a transactional replication push subscription](#create-a-transactional-replication-push-subscription) section, create the subscription using the name of the AG listener of the subscriber. After a failover, the listener name will always remain valid, whereas the actual server name of the subscriber will depend on the actual node that became the new primary.

   > [!NOTE]  
   > The subscription must be created by using a [!INCLUDE[tsql](../../../includes/tsql-md.md)] script and cannot be created using [!INCLUDE[ssManStudio](../../../includes/ssmanstudio-md.md)].

1. To create a pull subscription:

   1. Using the sample script in the [Create a transactional replication pull subscription](#create-a-transactional-replication-pull-subscription) section, create the subscription using the name of the AG listener of the subscriber.

   1. After a failover, create the distribution agent job on the new primary replica using the `sp_addpullsubscription_agent` stored procedure.

When you create a pull subscription, with the subscription database in an AG, after every failover, it is recommended to disable the distribution agent job on the old primary replica and enable the job on the new primary replica.

## Create a transactional replication push subscription

```sql
-- commands to execute at the publisher, in the publisher database:
USE [<publisher database name>];
GO

EXEC sp_addsubscription @publication = N'<publication name>',
    @subscriber = N'<AG listener name>',
    @destination_db = N'<subscriber database name>',
    @subscription_type = N'Push',
    @sync_type = N'automatic',
    @article = N'all',
    @update_mode = N'read only',
    @subscriber_type = 0;
GO
  
EXEC sp_addpushsubscription_agent @publication = N'<publication name>',
    @subscriber = N'<AG listener name>',
    @subscriber_db = N'<subscriber database name>',
    @job_login = NULL,
    @job_password = NULL,
    @subscriber_security_mode = 1;
GO
```

## Create a transactional replication pull subscription

```sql
-- commands to execute at the subscriber, in the subscriber database:
USE [<subscriber database name>];
GO

EXEC sp_addpullsubscription @publisher = N'<publisher name>',
    @publisher_db = N'<publisher database name>',
    @publication = N'<publication name>',
    @subscription_type = N'pull';
GO

EXEC sp_addpullsubscription_agent @publisher = N'<publisher name>',
    @subscriber = N'<AG listener name>',
    @distributor = N'<distributor AG listener name>', -- this parameter should only be used if the distribution database is part of an AG.
    @publisher_db = N'<publisher database name>',
    @publication = N'<publication name>',
    @job_login = NULL,
    @job_password = NULL,
    @subscriber_security_mode = 1;
GO
```

> [!NOTE]  
> When running [sp_addpullsubscription_agent](../../../relational-databases/system-stored-procedures/sp-addpullsubscription-agent-transact-sql.md) for a subscriber that is part of an AG, you must pass the `@Subscriber` parameter value to the stored procedure as the AG listener name. If you are running [!INCLUDE[sssql15-md](../../../includes/sssql16-md.md)] and earlier versions, or [!INCLUDE[sssql17-md](../../../includes/sssql17-md.md)] prior to CU 16, the stored procedure will not reference the AG listener name; it will be created with the subscriber server name on which the command is executed. To resolve this issue, manually update the `@Subscriber` parameter on the [Distribution Agent job](../../../relational-databases/replication/agents/replication-distribution-agent.md) with the AG listener name value.

## Resume the merge agents after the availability group of the subscriber fails over

For merge replication, a replication administrator must manually reconfigure the subscriber with the following steps:

1. Execute `sp_subscription_cleanup` to remove the old subscription for the subscriber. Perform this action on the new primary replica (which was formerly the secondary replica).

1. Recreate the subscription by creating a new subscription, beginning with a new snapshot.

> [!NOTE]  
> The current process is inconvenient for merge replication subscribers, however the main scenario for merge replication is disconnected users (desktops, laptops, handset devices) which will not use AGs on the subscriber.

## See also

- [Replication subscribers](../../../relational-databases/replication/subscribers.md)
- [Replication tutorials](../../../relational-databases/replication/replication-tutorials.md)
- [Distribution database](../../../relational-databases/replication/distribution-database.md)
