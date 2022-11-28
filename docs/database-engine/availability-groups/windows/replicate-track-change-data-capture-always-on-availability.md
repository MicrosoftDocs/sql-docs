---
title: "Replication, change tracking, change data capture & availability groups"
description: Learn about the interoperability of replication, change tracking, and change data capture when used with SQL Server Always On availability groups.
author: MashaMSFT
ms.author: mathoma
ms.date: 10/05/2021
ms.service: sql
ms.subservice: availability-groups
ms.topic: conceptual
ms.custom: seo-lt-2019
helpviewer_keywords:
  - "change tracking [SQL Server], AlwaysOn Availability Groups"
  - "change tracking [SQL Server], Always On Availability Groups"
  - "change data capture [SQL Server], AlwaysOn Availability Groups"
  - "change data capture [SQL Server], Always On Availability Groups"
  - "Availability Groups [SQL Server], interoperability"
  - "replication [SQL Server], AlwaysOn Availability Groups"
  - "replication [SQL Server], Always On Availability Groups"
---
# Replication, change tracking, & change data capture - Always On availability groups
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

  [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Replication, change data capture (CDC), and change tracking (CT) are supported on [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)]. [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] helps provide high availability and other database recovery capabilities.  
  
##  <a name="Overview"></a> Overview of replication with availability groups  
  
###  <a name="PublisherRedirect"></a> Publisher Redirection  
 When a published database is aware of [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)], the distributor that provides agent access to the publishing database is configured with redirected_publishers entries. These entries redirect the originally configured publisher/database pair, making use of an availability group listener name to connect to the publisher and publishing database. Established connections through the availability group listener name will fail on failover. When the replication agent restarts after failover, the connection will automatically be redirected to the new primary.  
  
 In an availability group, a secondary database cannot be a publisher. Republishing is only supported when transactional replication is combined with [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)].  
  
 If a published database is a member of an availability group and the publisher is redirected, it must be redirected to an availability group listener name associated with the availability group. It may not be redirected to an explicit node.  
  
> [!NOTE]  
>  After failover to a secondary replica, Replication Monitor is unable to adjust the name of the publishing instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] and will continue to display replication information under the name of the original primary instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. After failover, a tracer token cannot be entered by using the Replication Monitor, however a tracer token entered on the new publisher by using [!INCLUDE[tsql](../../../includes/tsql-md.md)], is visible in Replication Monitor.  
  
###  <a name="Changes"></a> General changes to replication agents to support availability groups  
 Three replication agents were modified to support [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)]. The Log Reader, Snapshot, and Merge agents were modified to query the distribution database for the redirected publisher and to use the returned availability group listener name, if a redirected publisher was declared, to connect to the database publisher.  
  
 By default, when the agents query the distributor to determine whether the original publisher has been redirected, the suitability of the current target or redirection will be verified prior to returning the redirected host to the agent. This behavior is recommended. However, if agent startup occurs frequently the overhead associated with the validation stored procedure may be deemed too costly. A new command-line switch, *BypassPublisherValidation*, has been added to the Log reader, Snapshot, and Merge agents. When the switch is used, the redirected publisher is returned immediately to the agent and execution of the validation stored procedure is bypassed.  
  
 Failures returned from the validation stored procedure are logged in the agent history logs. Those errors with severity greater than or equal to 16 will cause the agents to terminate. Some retry capabilities have been built in to the agents to handle the expected disconnect from a published database when it fails over to a new primary.  
  
#### Log Reader Agent Modifications  
 The Log reader Agent has the following changes.  
  
-   **Replicated Database Consistency**  
  
     When a published database is a member of an availability group, by default the log reader will not process log records that have not already been hardened at all availability group secondary replicas. This ensures that on failover, all rows replicated to a subscriber also are present at the new primary.  
  
     When the publisher has only two availability replicas (one primary and one secondary) and a failover happens, the original primary replica remains down because the log reader does not move forward until all secondary databases are brought back online or until the failing secondary replicas are removed from the availability group. The log reader, now running against the secondary database, will not proceed forward since Always On cannot harden any changes to any secondary database. To allow the log reader to proceed further and still have disaster recovery capacity, remove the original primary replica from the availability group using ALTER AVAILABITY GROUP <group_name> REMOVE REPLICA. Then add a new secondary replica to the availability group.  
  
-   **Trace flag 1448**  
  
     Trace flag 1448 enables the replication log reader to move forward even if the asynchronous secondary replicas have not acknowledged the reception of a change. Even with this trace flag enabled, the log reader always waits for the synchronous secondary replicas(They could become asynchronous commit mode as documented [here](../../../database-engine/availability-groups/windows/availability-modes-always-on-availability-groups.md), so log reader can move forward.). The log reader will not go beyond the min ack of the synchronous secondary replicas. This trace flag applies to the instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], not just to an availability group, an availability database, or a log reader instance. This trace flag takes effect immediately without a restart. It can be activated ahead of time or when an asynchronous secondary replica fails.  
  
###  <a name="StoredProcs"></a> Stored procedures supporting availability groups  
  
-   **sp_redirect_publisher**  
  
     The stored procedure **sp_redirect_publisher** is used to specify a redirected publisher for an existing publisher/database pair. If the publisher database belongs to an availability group, the redirected publisher is the availability group listener name.  
  
-   **sp_get_redirected_publisher**  
  
     The stored procedure **sp_get_redirected_publisher** is used by replication agents to query a distributor to determine whether a publisher/database pair has a defined redirected publisher. This stored procedure serves two purposes. First, it allows the agent to determine whether the original publisher has been redirected. Second, it may also initiate a validation stored procedure run at the distributor (**sp_validate_redirected_publisher**) that verifies the suitability of the target node of the redirection to serve as a publisher for the named database.  
  
     To execute this stored procedure the caller must either be a member of the **sysadmin** server role, the **db_owner** database role for the distribution database, or a member of a **Publication Access List** for a defined publication associated with the publisher database.  
  
-   **sp_validate_redirected_publisher**  
  
     This stored procedure attempts to validate that the current publisher is capable of hosting the published database. It can be called at any time to verify that the current host for the published database is capable of supporting replication.  
  
-   **sp_validate_replicate_hosts_as_publishers**  
  
     While it is useful for the agents to ensure that the current primary can function as the replication publisher for a publisher database, a more general validation capability is needed to establish the validity of an entire replication topology on an Always On availability database. The stored procedure **sp_validate_replica_hosts_as_publishers** is designed to fill this need.  
  
     This stored procedure is always run manually. The caller must either be **sysadmin** at the distributor, **dbowner** of the distribution database, or a member of the **Publication Access List** of a publication of the publisher database. In addition, the login of the caller must be a valid login for all of the availability replica hosts, and have select privileges on the availability database associated with the publisher database.  
  
###  <a name="CDC"></a> Change Data Capture  
 Databases enabled for change data capture (CDC) are able to use [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] in order to ensure not only that the database remains available in the event of failure, but that changes to the database tables continue to be monitored and deposited in the CDC change tables. The order in which CDC and [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] are configured is not important. CDC enabled databases can be added to [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)], and databases that are members of an Always On availability group can be enabled for CDC. In both cases, however, CDC configuration is always performed on the current or intended primary replica. CDC uses the log reader agent and has the same limitations as described in the **Log Reader Agent Modifications** section earlier in this topic.  
  
-   **Harvesting Changes for Change Data Capture Without Replication**  
  
     If CDC is enabled for a database, but replication is not, the capture process used to harvest changes from the log and deposit them in CDC change tables runs at the CDC host as its own SQL Agent job.  
  
     In order to resume the harvesting of changes after failover, the stored procedure **sp_cdc_add_job** must be run at the new primary to create the local capture job.  
  
     The following example creates the capture job.  
  
    ```sql  
    EXEC sys.sp_cdc_add_job @job_type = 'capture';  
    ```  
  
-   **Harvesting Changes for Change Data Capture With Replication**  
  
     If both CDC and replication are enabled for a database, the log reader handles the population of the CDC change tables. In this case, the techniques used by replication to use [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] will ensure that changes continue to be harvested from the log and deposited in CDC change tables after failover. Nothing more needs to be done for CDC in this configuration to ensure that the change tables are populated.  
  
-   **Change Data Capture Cleanup**  
  
     To ensure that appropriate cleanup occurs at the new primary database, a local cleanup job should always be created. The following example creates the cleanup job.  
  
    ```sql  
    EXEC sys.sp_cdc_add_job @job_type = 'cleanup';  
    ```  
  
    > [!NOTE]  
    >  You should create the jobs at the new primary replica after failover. The CDC jobs running at the old primary database should be disabled when the local database becomes a secondary database. Post this if the replica becomes primary again, you need to reenable the CDC jobs on the replica. To disable and enable jobs, use the *\@enabled* option of [sp_update_job &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sp-update-job-transact-sql.md). For more information about creating CDC jobs, see [sys.sp_cdc_add_job &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sys-sp-cdc-add-job-transact-sql.md).  
  
-   **Adding CDC Roles to an Always On Primary Database Replica**  
  
     When a table is enabled for CDC, it is possible to associate a database role with the capture instance. If a role is specified, the user wishing to use the CDC table-valued functions to access changes for the table must not only have select access to the tracked table columns, but must also be a member of the named role. If the specified role does not already exist, the role will be created. When database roles are automatically added to an Always On primary database, the roles are also propagated to the secondary databases of the availability group.  
  
-   **Client Applications Accessing CDC Change Data and Always On**  
  
     Client applications that use the table-valued functions (TVFs) or linked servers to access change table data also need the ability to locate an appropriate CDC host after failover. The availability group listener name is the mechanism provided by [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] to transparently allow a connection to be retargeted to a different host. Once an availability group listener name is associated with an availability group, it is available to be used in TCP connection strings. Two different connection scenarios are supported through the availability group listener name.  
  
    -   One ensures that connection requests are always directed to the current primary replica.  
  
    -   One ensures that connection requests are directed to a read-only secondary replica.  
  
     If used to locate a read-only secondary replica, a read-only routing list must also be defined for the availability group. For more information about routing access to readable secondaries, see [To Configure Availability Replicas for Read-Only Routing](../../../database-engine/availability-groups/windows/configure-read-only-routing-for-an-availability-group-sql-server.md).  
  
    > [!NOTE]  
    >  There is some propagation delay associated with the creation of an availability group listener name and its use by client applications to access an availability group database replica.  
  
     Use the following query to determine whether an availability group listener name has been defined for the availability group hosting a CDC database. The query will return the availability group listener name if one has been created.  
  
    ```sql  
    SELECT dns_name   
    FROM sys.availability_group_listeners AS l  
    INNER JOIN sys.availability_databases_cluster AS d  
        ON l.group_id = d.group_id  
    WHERE d.database_name = N'MyCDCDB';  
    ```  
  
-   **Redirecting the Query Load to a Readable Secondary Replica**  
  
     While in many cases a client application will always want to connect to the current primary replica, that is not the only way to use [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)]. If an availability group is configured to support readable secondary replicas, change data can also be gathered from secondary nodes.  
  
     When an availability group is configured, the ALLOW_CONNECTIONS attribute associated with the SECONDARY_ROLE is used to specify the type of secondary access supported. If configured as ALL, all connections to the secondary will be allowed, but only those requiring read-only access will succeed. If configured as READ_ONLY, it is necessary to specify read only intent when making the connection to the secondary database in order for the connection to succeed. For more information, see [Configure Read-Only Access on an Availability Replica &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/configure-read-only-access-on-an-availability-replica-sql-server.md).  
  
     The following query can be used to determine whether read-only intent is needed to connect to a readable secondary replica.  
  
    ```sql  
    SELECT g.name AS AG, replica_server_name, secondary_role_allow_connections_desc  
    FROM sys.availability_replicas AS r  
    JOIN sys.availability_groups AS g  
        ON r.group_id = g.group_id  
    WHERE g.name = N'MY_AG_NAME';  
    ```  
  
     Either the availability group listener name or the explicit node name can be used to locate the secondary replica. If the availability group listener name is used, access will be directed to any suitable secondary replica.  
  
     When **sp_addlinkedserver** is used to create a linked server to access the secondary, the *\@datasrc* parameter is used for the availability group listener name or the explicit server name, and the *\@provstr* parameter is used to specify read-only intent.  
  
    ```sql  
    EXEC sp_addlinkedserver   
    @server = N'linked_svr',   
    @srvproduct=N'SqlServer',  
    @provider=N'MSOLEDBSQL',   
    @datasrc=N'AG_Listener_Name',   
    @provstr=N'ApplicationIntent=ReadOnly',   
    @catalog=N'MY_DB_NAME';  
    ```  
  
-   **Client Access to CDC Change Data and Domain Logins**  
  
     In general, you should use domain logins for client access to change data residing in databases that are members of Always On availability groups. To ensure continued access to change data after failover, the domain user will need access privileges on all of the hosts supporting availability group replicas. If a database user is added to a database in a primary replica, and the user is associated with a domain login, the database user is propagated to secondary databases and continues to be associated with the specified domain login. If the new database user is associated with a SQL Server authentication login, the user at the secondary databases will be propagated without a login. While the associated [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] authentication login could be used to access change data at the primary where the database user was originally defined, that node is the only one where access would be possible. The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] authentication login would not be able to access data from any secondary database, nor from any new primary databases other than the original database where the database user was defined.  
     
-   **Disabling Change Data Capture**  
If Change Data Capture needs to be disabled on a database that is part of an Always On Availability Group, then you will need to perform more steps to ensure that log truncation is not affected. You will need to implement one of the following steps to prevent Change Data Capture from blocking log truncation after disabling Change Data Capture:
    - Restart the SQL Server service on every secondary replica instance
    - OR remove the database from all the secondary replica instances of the Availability Group and add it to the Availability Group replica instances using automatic or manual seeding
  
###  <a name="CT"></a> Change Tracking  
 A database enabled for change tracking (CT) can be part of an Always On availability group. No more configuration is needed. Change tracking client applications that use the CDC table-valued functions (TVFs) to access change data will need the ability to locate the primary replica after failover. If the client application connects through the availability group listener name, connection requests will always be appropriately directed to the current primary replica.  
  
> [!NOTE]  
>  Change tracking data must always be obtained from the primary replica. An attempt to access change data from a secondary replica will result in the following error:  
>   
>  Msg 22117, Level 16, State 1, Line1  
>   
>  For databases that are members of a secondary replica (that is, for secondary databases), change tracking is not supported. As an alternative to running change tracking queries on the primary replica, you can create a database snapshot of an AG database from the secondary replica and then use that to query change data. A database snapshot is a read-only, static view of a SQL Server database (the source database), so change tracking data in the database snapshot will be of the time when the snapshot was taken on the AG database from the secondary replica.

> [!NOTE]  
>  When a failover occurs on a database with Change Tracking enabled, recovery time on the new primary replica may take longer than usual as Change Tracking requires a full database restart. 


##  <a name="Prereqs"></a> Prerequisites, Restrictions, and Considerations for Using Replication  
 This section describes considerations for deploying replication with [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)], including prerequisites, restrictions, and recommendations.  
  
### Prerequisites  
  
-   When using transactional replication and the publishing database is in an availability group, both the publisher and the distributor must run at least [!INCLUDE[ssSQL11](../../../includes/sssql11-md.md)]. The subscriber can be using a lower level of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
-   When using merge replication and the publishing database is in an availability group:  
  
    -   Push subscription: Both the publisher and the distributor must run at least [!INCLUDE[ssSQL11](../../../includes/sssql11-md.md)].  
  
    -   Pull subscription: The publisher, distributor, and subscriber databases must be on at least [!INCLUDE[ssSQL11](../../../includes/sssql11-md.md)]. This is because the merge agent on the subscriber must understand how an availability group can fail over to its secondary.  
  
-   The Publisher instances satisfy all the prerequisites required to participate in an Always On availability group. For more information, see [Prerequisites, Restrictions, and Recommendations for Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/prereqs-restrictions-recommendations-always-on-availability.md).  
  
### Restrictions  
 Supported combinations of replication on [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)]:  
  
|Replication|Publisher|Distributor<sup>1</sup>|Subscriber|  
|-|-|-|-|  
|**Transactional**|Yes<br /><br /> Note: Does not include support for bi-directional and reciprocal transactional replication.|Yes|Yes|
|**Peer-to-peer**<sup>2</sup>|Yes|No|Yes|  
|**Merge**|Yes|No|No|  
|**Snapshot**|Yes|No|Yes|
  
<sup>1</sup> The Distributor database is not supported for use with database mirroring.

<sup>2</sup> Requires SQL Server 2019 CU 13 or later.
  
### Considerations  
  
-   The distribution database is not supported for use with database mirroring but is supported with [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] subject to certain limitations, see [Configure Distribution Availability Group](../../../relational-databases/replication/configure-distribution-availability-group.md#limitations-or-exclusions). Replication configuration is coupled to the SQL Server instance where the Distributor is configured; therefore the distribution database cannot be mirrored or replicated. It is also possible to provide high availability for the Distributor using a SQL Server failover cluster. For more information, see [Always On Failover Cluster Instances &#40;SQL Server&#41;](../../../sql-server/failover-clusters/windows/always-on-failover-cluster-instances-sql-server.md).  
  
-   Subscriber failover to a secondary database, while supported, is a manual procedure for merge replication subscribers. The procedure is essentially identical to the method used to fail over a mirrored subscriber database. Transactional replication subscribers do not need special handling while participating in [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)]. Subscribers must be running [!INCLUDE[ssSQL11](../../../includes/sssql11-md.md)] or later to participate in an availability group.  For more information, see [Replication Subscribers and Always On Availability Groups (SQL Server)](../../../database-engine/availability-groups/windows/replication-subscribers-and-always-on-availability-groups-sql-server.md)
  
-   Metadata and objects that exist outside the database are not propagated to the secondary replicas, including logins, jobs, linked servers. If you require the metadata and objects at the new primary database after failover, you must copy them manually. For more information, see [Management of Logins and Jobs for the Databases of an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/logins-and-jobs-for-availability-group-databases.md).  

### Distributed Availability Groups

The publisher, or distribution database in an Availability Group cannot be configured as part of a Distributed Availability Group. The publisher database in an Availability Group and the distribution database in an Availability Group both require a listener endpoint for proper configuration and usage. However, it is not possible to configure a listener endpoint for a Distributed Availability group.
  
##  <a name="RelatedTasks"></a> Related Tasks  
 **Replication**  
  
-   [Configure Replication for Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/configure-replication-for-always-on-availability-groups-sql-server.md)  
  
-   [Maintaining an Always On Publication Database &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/maintaining-an-always-on-publication-database-sql-server.md)  
  
-   [Replication Administration FAQ](../../../relational-databases/replication/administration/frequently-asked-questions-for-replication-administrators.yml)  
  
 **Change data capture**  
  
-   [Enable and Disable Change Data Capture &#40;SQL Server&#41;](../../../relational-databases/track-changes/enable-and-disable-change-data-capture-sql-server.md)  
  
-   [Administer and Monitor Change Data Capture &#40;SQL Server&#41;](../../../relational-databases/track-changes/administer-and-monitor-change-data-capture-sql-server.md)  
  
-   [Work with Change Data &#40;SQL Server&#41;](../../../relational-databases/track-changes/work-with-change-data-sql-server.md)  
  
 **Change tracking**  
  
-   [Enable and Disable Change Tracking &#40;SQL Server&#41;](../../../relational-databases/track-changes/enable-and-disable-change-tracking-sql-server.md)  
  
-   [Manage Change Tracking &#40;SQL Server&#41;](../../../relational-databases/track-changes/manage-change-tracking-sql-server.md)  
  
-   [Work with Change Tracking &#40;SQL Server&#41;](../../../relational-databases/track-changes/work-with-change-tracking-sql-server.md)  
  
## See Also  
 [Replication Subscribers and Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/replication-subscribers-and-always-on-availability-groups-sql-server.md)   
 [Prerequisites, Restrictions, and Recommendations for Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/prereqs-restrictions-recommendations-always-on-availability.md)   
 [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)   
 [Always On Availability Groups: Interoperability &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/always-on-availability-groups-interoperability-sql-server.md)   
 [Always On Failover Cluster Instances &#40;SQL Server&#41;](../../../sql-server/failover-clusters/windows/always-on-failover-cluster-instances-sql-server.md)   
 [About Change Data Capture &#40;SQL Server&#41;](../../../relational-databases/track-changes/about-change-data-capture-sql-server.md)   
 [About Change Tracking &#40;SQL Server&#41;](../../../relational-databases/track-changes/about-change-tracking-sql-server.md)   
 [SQL Server Replication](../../../relational-databases/replication/sql-server-replication.md)   
 [Track Data Changes &#40;SQL Server&#41;](../../../relational-databases/track-changes/track-data-changes-sql-server.md)   
 [sys.sp_cdc_add_job &#40;Transact-SQL&#41;](../../../relational-databases/system-stored-procedures/sys-sp-cdc-add-job-transact-sql.md)  
  
  
