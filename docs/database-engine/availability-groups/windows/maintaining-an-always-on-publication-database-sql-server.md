---
title: "Manage a replicated Publisher database as part of an availability group"
description: "A description for how to manage and maintain a database that is acting as a Publisher in a SQL replication and is also participating in an Always On availability group."
author: MashaMSFT
ms.author: mathoma
ms.date: "05/18/2016"
ms.service: sql
ms.subservice: availability-groups
ms.topic: how-to
ms.custom: seodec18
helpviewer_keywords:
  - "Availability Groups [SQL Server], interoperability"
  - "replication [SQL Server], AlwaysOn Availability Groups"
  - "replication [SQL Server], Always On Availability Groups"
---
# Manage a replicated Publisher database as part of an Always On availability group
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

  This topic discusses special considerations for maintaining a publication database when you use Always On availability groups.  
  
##  <a name="MaintainPublDb"></a> Maintaining a Published Database in an Availability Group  
 Maintaining an Always On publication database is basically the same as maintaining a standard publication database, with the following considerations:  
  
-   Administration must occur at the primary replica host. In [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)], publications appear under the **Local Publications** folder for the primary replica host and also for readable secondary replicas. After failover, you might have to manually refresh [!INCLUDE[ssManStudio](../../../includes/ssmanstudio-md.md)] for the change to be reflected if the secondary that was promoted to primary was not readable.  
  
-   Replication Monitor always displays publication information under the original publisher. However, this information can be viewed in Replication Monitor from any replica by adding the original publisher as a server.  
  
-   When using stored procedures or Replication Management Objects (RMO) to administer replication at the current primary, for cases in which you specify the Publisher name, you must specify the name of the instance on which the database was enabled for replication (the original publisher). To determine the appropriate name, use the **PUBLISHINGSERVERNAME** function. When a publishing database joins an availability group, the replication metadata stored in the secondary database replicas is identical to that at the primary. Consequently, for publication databases enabled for replication at the primary, the publisher instance name stored in system tables at the secondary is the name of the primary, not the secondary. This affects replication configuration and maintenance if the publication database fails over to a secondary. For example, if you are configuring replication with stored procedures at a secondary after failover, and you want a pull subscription to a publication database that was enabled at a different replica, you must specify the name of the original publisher instead of the current publisher as the *\@publisher* parameter of **sp_addpullsubscription** or **sp_addmergepullsubscription**. However, if you enable a publication database after failover, the publisher instance name stored in the system tables is the name of the current primary host. In this case, you would use the host name of the current primary replica for the *\@publisher* parameter.  
  
    > [!NOTE]  
    >  For some procedures, such as **sp_addpublication**, the *\@publisher* parameter is supported only for publishers that are not instances of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]; in these cases, it is not relevant for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Always On.  
  
-   To synchronize a subscription in [!INCLUDE[ssManStudio](../../../includes/ssmanstudio-md.md)] after a failover, synchronize pull subscriptions from the subscriber and synchronize push subscriptions from the active publisher.  
  
##  <a name="RemovePublDb"></a> Removing a Published Database from an Availability Group  
 Consider the following issues if a published database is removed from an availability group, or if an availability group that has a published member database is dropped.  
  
-   If the publication database at the original publisher is removed from an availability group primary replica, you must run **sp_redirect_publisher** without specifying a value for the *\@redirected_publisher* parameter in order to remove the redirection for the publisher/database pair.  
  
    ```  
    EXEC sys.sp_redirect_publisher   
        @original_publisher = 'MyPublisher',  
        @published_database = 'MyPublishedDB';  
    ```  
  
     The database will be left in the recovering state at the primary and must be restored. Once you do this, replication should work unchanged against the original Publisher.  
  
-   If the publication database fails over from the original publisher to a replica and the database is removed from the availability group primary replica, use the stored procedure **sp_redirect_publisher** to explicitly redirect the original publisher to the new publisher. The database will be left in the recovering state and must be restored. Once you do this, replication should continue to work as it did under the availability group.  
  
    ```  
    EXEC sys.sp_redirect_publisher   
        @original_publisher = 'MyPublisher',  
        @published_database = 'MyPublishedDB',  
        @redirected_publisher = 'MyNewPublisher';  
    ```  
  
     Do not remove the remote server for the original publisher from the distributor, even if the server can no longer be accessed. The server metadata for the original publisher is needed at the distributor to satisfy publication metadata queries.  
  
-   If a complete availability group is removed, the behavior regarding a member replicated database is the same as when a published database is removed from an availability group. Replication can be resumed from the last primary as soon as the database has been restored and the redirection has been modified. If the database is restored at its original publisher, redirection should be removed. If the database is restored at a different host, redirection should be explicitly directed to the new host.  
  
    > [!NOTE]  
    >  When an availability group is removed that has published member databases, or a published database is removed from an availability group, all copies of the published databases will be left in the recovering state. If restored, each will appear as a published database. Only one copy should be retained with publication metadata. To disable replication for a published database copy, first remove all subscriptions and publications from the database.  
  
     Run **sp_dropsubscription** to remove publication subscriptions. Make sure to set the parameter *\@ignore_distributor* to 1 to preserve the metadata for the active publishing database at the distributor.  
  
    ```  
    USE MyDBName;  
    GO  
  
    EXEC sys.sp_dropsubscription   
        @subscriber = 'MySubscriber',  
        @publication = 'MyPublication',  
        @article = 'all',  
        @ignore_distributor = 1;  
    ```  
  
     Run **sp_droppublication** to remove all publications. Again, set the parameter *\@ignore_distributor* to 1 to preserve the metadata for the active publishing database at the distributor.  
  
    ```  
    EXEC sys.sp_droppublication   
        @publication = 'MyPublication',  
        @ignore_distributor = 1;  
    ```  
  
     Run **sp_replicationdboption** to disable replication for the database.  
  
    ```  
    EXEC sys.sp_replicationdboption  
        @dbname = 'MyDBName',  
        @optname = 'publish',  
        @value = 'false';  
    ```  
  
     At this point, the copy of the published database can be retained or dropped.  

## Remove original publisher

There may be instances (replacing older server, OS upgrade, etc.) where you want to remove an original publisher from an Always On availability group. Follow the steps in this section to remove the publisher from the availability group. 

Assume you have servers N1, N2, and D1, where N1 and N2 are the primary and secondary replica of availability group AG1, N1 is the original publisher of a transactional publication and D1 is the distributor. You would like to replace the original publisher N1 with the new publisher N3. 

To remove the publisher, follow these steps: 

1. Install and configure SQL Server to the node N3. The version of SQL Server must be the same as the original publisher. 
1. On distributor server D1, add N3 as a publisher using [sp_adddistpublisher](../../../relational-databases/system-stored-procedures/sp-adddistpublisher-transact-sql.md). 
1. Configure N3 as a publisher with D1 as its distributor. 
1. Add N3 as a replica to availability group AG1. 
1. On the N3 replica, verify that the push subscribers for the publication appear as linked servers. Use either [sp_addlinkedserver](../../../relational-databases/system-stored-procedures/sp-addlinkedserver-transact-sql.md) or SQL Server Management Studio. 
1. Once N3 is synchronized, fail the availability group over to N3 as primary. 
1. Remove N1 from availability group AG1. 

Please consider the following:
- Do not remove remote server of the original publisher (N1 in this case) or any metadata associated with it from the distributor, even if the server can no longer be accessed. The server metadata for the original publisher is needed at the distributor to satisfy publication metadata queries and without it replication will fail. 
- For SQL Server 2014, once the original publisher is removed, you will not be able to use original publisher name for administering replication in Replication Monitor. If you try to register new replica/s as publisher in Replication Monitor, information will not show as there is no metadata associated with it. For administering replication in this scenario, you will have to right-click individual publications and subscriptions in SQL Server Management Studio (SSMS).
- For SQL Server 2016 SP2-CU3, SQL Server 2017 CU6 and above, register the listener of the availability group publisher in Replication Monitor to administer replication using SQL Server Management Studio version 17.7 and above. 
  
##  <a name="RelatedTasks"></a> Related Tasks  
  
-   [Configure Replication for Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/configure-replication-for-always-on-availability-groups-sql-server.md)  
  
-   [Replication, Change Tracking, Change Data Capture, and Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/replicate-track-change-data-capture-always-on-availability.md)  
  
-   [Replication Administration FAQ](../../../relational-databases/replication/administration/frequently-asked-questions-for-replication-administrators.yml)  
  
-   [Replication Subscribers and Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/replication-subscribers-and-always-on-availability-groups-sql-server.md)  
  
## See Also  
 [Prerequisites, Restrictions, and Recommendations for Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/prereqs-restrictions-recommendations-always-on-availability.md)   
 [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)   
 [Always On Availability Groups: Interoperability &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/always-on-availability-groups-interoperability-sql-server.md)   
 [SQL Server Replication](../../../relational-databases/replication/sql-server-replication.md)  
  
  
