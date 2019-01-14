---
title: "Replication Subscribers and Always On Availability Groups (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "03/08/2018"
ms.prod: sql
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
helpviewer_keywords: 
  - "failover subscribers with AlwaysOn"
  - "Availability Groups [SQL Server], interoperability"
  - "replication [SQL Server], AlwaysOn Availability Groups"
ms.assetid: 0995f269-0580-43ed-b8bf-02b9ad2d7ee6
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Replication Subscribers and Always On Availability Groups (SQL Server)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

  When an Always On availability group containing a database that is a replication subscriber fails over, the replication subscription might fail. For transactional subscribers, the distribution agent will continue to replicate automatically if the subscription is using the name of the availability group listener of the subscriber. For merge subscribers, a replication administrator must manually reconfigure the subscriber, by recreating the subscription.  
  
## What is Supported  
 [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] replication supports the automatic failover of the publisher and the automatic failover of transactional subscribers. The failover of a distributor on an availability database is not supported. Merge subscribers can be part of an availability group, however manual actions are required to configure the new susbcriber after a failover. Availability Groups cannot be combined with Websync and SQL Server Compact scenarios.  
  
## How to Create Transactional Subscription in an Always On Environment  
 For transactional replication, use the following steps to configure and failover a subscriber availability group:  
  
1.  Before creating the subscription, add the subscriber database to the appropriate Always On availability group.  
  
2.  Add the subscriber's availability group Listener as a linked server to all nodes of the availability group. This step ensures that all potential failover partners are aware of and can connect to the listener.  
  
3.  Using the script in the **Creating a Transactional Replication Push Subscription** section below, create the subscription using the name of the availability group listener of the subscriber. After a failover, the listener name will always remain valid, whereas the actual server name of the subscriber will depend on the actual node that became the new primary.  
  
    > [!NOTE]  
    >  The subscription must be created by using a [!INCLUDE[tsql](../../../includes/tsql-md.md)] script and cannot be created using [!INCLUDE[ssManStudio](../../../includes/ssmanstudio-md.md)].  
  
4.  If creating a pull subscription:  
  
    1.  In [!INCLUDE[ssManStudio](../../../includes/ssmanstudio-md.md)], on the primary subscriber node, open the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Agent tree.  
  
    2.  Identify the **Pull Distribution Agent** job and edit the job.  
  
    3.  On the **Run Agent** job step, check the `-Publisher` and `-Distributor` parameters. Make sure that these parameters contain the correct direct server and instance names of the publisher and distributor server.  
  
    4.  Change the `-Subscriber` parameter to the subscriber's availability group listener name.  
  
 When you create your subscription following these steps, then you won't have to do anything after a failover.  
  
## Creating a Transactional Replication Push Subscription  
  
```  
-- commands to execute at the publisher, in the publisher database:  
use [<publisher database name>]  
EXEC sp_addsubscription @publication = N'<publication name>',   
       @subscriber = N'<availability group listener name>',   
       @destination_db = N'<subscriber database name>',   
       @subscription_type = N'Push',   
       @sync_type = N'automatic', @article = N'all', @update_mode = N'read only', @subscriber_type = 0;  
GO  
  
EXEC sp_addpushsubscription_agent @publication = N'<publication name>',   
       @subscriber = N'<availability group listener name>',   
       @subscriber_db = N'<subscriber database name>',   
       @job_login = null, @job_password = null, @subscriber_security_mode = 1;  
GO  
```  
  
## To Resume the Merge Agents After the Availability Group of the Subscriber Fails Over  
 For merge replication, a replication administrator must manually reconfigure the subscriber with the following steps:  
  
1.  Execute **sp_subscription_cleanup** to remove the old subscription for the subscriber. Perform this action on the new primary replica (which was formerly the secondary replica).  
  
2.  Recreate the subscription by creating a new subscription, beginning with a new snapshot.  
  
> [!NOTE]  
>  The current process is inconvenient for merge replication subscribers, however the main scenario for merge replication is disconnected users (desktops, laptops, handset devices) which will not use Always On availability groups on the subscriber.  
  
  
