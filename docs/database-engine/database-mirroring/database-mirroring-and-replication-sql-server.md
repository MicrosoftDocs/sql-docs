---
title: "Database Mirroring and Replication (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: high-availability
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
helpviewer_keywords: 
  - "database mirroring [SQL Server], interoperability"
  - "replication [SQL Server], database mirroring and"
ms.assetid: 82796217-02e2-4bc5-9ab5-218bae11a2d6
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Database Mirroring and Replication (SQL Server)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  Database mirroring can be used in conjunction with replication to improve availability for the publication database. Database mirroring involves two copies of a single database that typically reside on different computers. At any given time, only one copy of the database is currently available to clients. This copy is known as the principal database. Updates made by clients to the principal database are applied on the other copy of the database, known as the mirror database. Mirroring involves applying the transaction log from every insertion, update, or deletion made on the principal database onto the mirror database.  
  
 Replication failover to a mirror is fully supported for publication databases, with limited support for subscription databases. Database mirroring is not supported for the distribution database. For information about recovering a distribution database or subscription database without any need to reconfigure replication, see [Back Up and Restore Replicated Databases](../../relational-databases/replication/administration/back-up-and-restore-replicated-databases.md).   
  
> [!NOTE]  
>  After a failover, the mirror becomes the principal. In this topic, "principal" and "mirror" always refer to the original principal and mirror.  
  
## Requirements and Considerations for Using Replication with Database Mirroring  
 Be aware of the following requirements and considerations when using replication with database mirroring:  
  
-   The principal and mirror must share a Distributor. We recommend that this be a remote Distributor, which provides greater fault tolerance if the Publisher has an unplanned failover.  
  
-   Replication supports mirroring the publication database for merge replication and for transactional replication with read-only Subscribers or queued updating Subscribers. Immediate updating Subscribers, Oracle Publishers, Publishers in a peer-to-peer topology, and republishing are not supported.  
  
-   Metadata and objects that exist outside the database are not copied to the mirror, including logins, jobs, linked servers, and so on. If you require the metadata and objects at the mirror, you must copy them manually. For more information, see [Management of Logins and Jobs After Role Switching &#40;SQL Server&#41;](../../sql-server/failover-clusters/management-of-logins-and-jobs-after-role-switching-sql-server.md).  
  
## Configuring Replication with Database Mirroring  
 Configuring replication and database mirroring involves five steps. Each step is described in more detail in the following section.  
  
1.  Configure the Publisher.  
  
2.  Configure database mirroring.  
  
3.  Configure the mirror to use the same Distributor as the principal.  
  
4.  Configure replication agents for failover.  
  
5.  Add the principal and mirror to Replication Monitor.  
  
 Steps 1 and 2 can also be performed in the opposite order.  
  
#### To configure database mirroring for a publication database  
  
1.  Configure the Publisher:  
  
    1.  We recommend using a remote Distributor. For more information about configuring distribution, see [Configure Distribution](../../relational-databases/replication/configure-distribution.md).  
  
    2.  You can enable a database for snapshot and transactional publications and/or merge publications. For mirrored databases that will contain more than one type of publication, you must enable the database for both types at the same node using [sp_replicationdboption](../../relational-databases/system-stored-procedures/sp-replicationdboption-transact-sql.md). For example, you could execute the following stored procedure calls at the principal:  
  
        ```  
        exec sp_replicationdboption @dbname='<PublicationDatabase>', @optname='publish', @value=true;  
        exec sp_replicationdboption @dbname='<PublicationDatabase>', @optname='mergepublish', @value=true;  
        ```  
  
         For more information about creating publications, see [Publish Data and Database Objects](../../relational-databases/replication/publish/publish-data-and-database-objects.md).  
  
2.  Configure database mirroring. For more information, see [Establish a Database Mirroring Session Using Windows Authentication &#40;SQL Server Management Studio&#41;](../../database-engine/database-mirroring/establish-database-mirroring-session-windows-authentication.md) and [Setting Up Database Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/setting-up-database-mirroring-sql-server.md).  
  
3.  Configure distribution for the mirror. Specify the mirror name as the Publisher, and specify the same Distributor and snapshot folder that the principal uses. For example, if you are configuring replication with stored procedures, execute [sp_adddistpublisher](../../relational-databases/system-stored-procedures/sp-adddistpublisher-transact-sql.md) at the Distributor; and then execute [sp_adddistributor](../../relational-databases/system-stored-procedures/sp-adddistributor-transact-sql.md) at the mirror. For **sp_adddistpublisher**:  
  
    -   Set the value of the **@publisher** parameter to the network name of the mirror.  
  
    -   Set the value of the **@working_directory** parameter to the snapshot folder used by the principal.  
  
4.  Specify the mirror name for the **-PublisherFailoverPartner** agent parameter. Agent This parameter is required for the following agents to identify the mirror after failover:  
  
    -   Snapshot Agent (for all publications)  
  
    -   Log Reader Agent (for all transactional publications)  
  
    -   Queue Reader Agent (for transactional publications that support queued updating subscriptions)  
  
    -   Merge Agent (for merge subscriptions)  
  
    -   [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] replication listener (replisapi.dll: for merge subscriptions synchronized using Web synchronization)  
  
    -   SQL Merge ActiveX Control (for merge subscriptions synchronized with the control)  
  
     The Distribution Agent and Distribution ActiveX Control do not have this parameter because they do not connect to the Publisher.  
  
     Agent parameter changes take effect the next time the agent is started. If the agent runs continuously, you must stop and restart the agent. Parameters can be specified in agent profiles and from the command prompt. For more information, see:  
  
    -   [View and Modify Replication Agent Command Prompt Parameters &#40;SQL Server Management Studio&#41;](../../relational-databases/replication/agents/view-and-modify-replication-agent-command-prompt-parameters.md)  
  
    -   [Replication Agent Executables Concepts](../../relational-databases/replication/concepts/replication-agent-executables-concepts.md)  
  
     We recommend adding the **-PublisherFailoverPartner** to an agent profile, and then specifying the mirror name in the profile. For example, if you are configuring replication with stored procedures:  
  
    ```  
    -- Execute sp_help_agent_profile in the context of the distribution database to get the list of profiles.  
    -- Select the profile id of the profile that needs to be updated from the result set.  
    -- In the agent_type column returned by sp_help_agent_profile:   
    -- 1 = Snapshot Agent; 2 = Log Reader Agent; 3 = Distribution Agent; 4 = Merge Agent; 9 = Queue Reader Agent.  
  
    exec sp_help_agent_profile;  
  
    -- Setting the -PublisherFailoverPartner parameter in the default Snapshot Agent profile (profile 1).  
    -- Execute sp_add_agent_parameter in the context of the distribution database.  
    exec sp_add_agent_parameter @profile_id = 1, @parameter_name = N'-PublisherFailoverPartner', @parameter_value = N'<Failover Partner Name>';  
  
    -- Setting the -PublisherFailoverPartner parameter in the default Merge Agent profile (profile 6).  
    -- Execute sp_add_agent_parameter in the context of the distribution database.  
    exec sp_add_agent_parameter @profile_id = 6, @parameter_name = N'-PublisherFailoverPartner', @parameter_value = N'<Failover Partner Name>';  
    ```  
  
5.  Add the principal and mirror to Replication Monitor. For more information, see [Add and Remove Publishers from Replication Monitor](../../relational-databases/replication/monitor/add-and-remove-publishers-from-replication-monitor.md).  
  
## Maintaining a Mirrored Publication Database  
 Maintaining a mirrored publication database is essentially the same as maintaining a non-mirrored database, with the following considerations:  
  
-   Administration and monitoring must occur at the active server. In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], publications appear under the **Local Publications** folder only for the active server. For example, if you failover to the mirror, the publications are displayed at the mirror and are no longer displayed at the principal. If the database fails over to the mirror, you might need to manually refresh [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] and Replication Monitor for the change to be reflected.  
  
-   Replication Monitor displays Publisher nodes in the object tree for both the principal and the mirror. If the principal is the active server, publication information is displayed only under the principal node in Replication Monitor.  
  
     If the mirror is the active server:  
  
    -   If an agent has an error, the error is indicated only on the principal node, not on the mirror node.  
  
    -   If the principal is unavailable, the principal and mirror nodes display identical lists of publications. Monitoring should be performed on the publications under the mirror node.  
  
-   When using stored procedures or Replication Management Objects (RMO) to administer replication at the mirror, for cases in which you specify the Publisher name, you must specify the name of the instance on which the database was enabled for replication. To determine the appropriate name, use the function [publishingservername](../../t-sql/functions/replication-functions-publishingservername.md).  
  
     When a publication database is mirrored, the replication metadata stored in the mirrored database is identical to the metadata stored in the principal database. Consequently, for publication databases enabled for replication at the principal, the Publisher instance name stored in system tables at the mirror is the name of the principal, not the mirror. This affects replication configuration and maintenance if the publication database fails over to the mirror. For example, if you are configuring replication with stored procedures on the mirror after a failover, and you want to add a pull subscription to a publication database that was enabled at the principal, you must specify the principal name rather than the mirror name for the **@publisher** parameter of **sp_addpullsubscription** or **sp_addmergepullsubscription**.  
  
     If you enable a publication database at the mirror after failover to the mirror, the Publisher instance name stored in system tables is the name of the mirror; in this case, you would use the name of the mirror for the **@publisher** parameter.  
  
    > [!NOTE]  
    >  In some cases, such as **sp_addpublication**, the **@publisher** parameter is supported only for non-[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Publishers; in these cases, it is not relevant for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database mirroring.  
  
-   To synchronize a subscription in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] after a failover: synchronize pull subscriptions from the Subscriber; and synchronize push subscriptions from the active Publisher.  
  
### Replication Behavior if Mirroring is Removed  
 Keep the following issues in mind if database mirroring is removed from a published database:  
  
-   If the publication database at the principal is no longer mirrored, replication continues to work unchanged against the original principal.  
  
-   If the publication database fails over from the principal to the mirror and the mirroring relationship is subsequently disabled or removed, replication agents will not function against the mirror. If the principal is permanently lost, disable and then reconfigure replication with the mirror specified as the Publisher.  
  
-   If database mirroring is removed completely, the mirror database is in a recovery state and must be restored in order to become functional. The behavior of the recovered database with respect to replication depends on whether the KEEP_REPLICATION option is specified. This option forces the restore operation to preserve replication settings when restoring a published database to a server other than that on which the backup was created. Use the KEEP_REPLICATION option only when the other publication database is unavailable. The option is not supported if the other publication database is still intact and replicating. For more information about KEEP_REPLICATION, see [RESTORE &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-transact-sql.md).  
  
## Log Reader Agent Behavior  
 The following table describes Log Reader Agent behavior for the various operating modes of database mirroring.  
  
|Operating mode|Log Reader Agent behavior if the mirror is unavailable|  
|--------------------|------------------------------------------------------------|  
|High-safety mode with automatic failover|If the mirror is unavailable, the Log Reader Agent propagates commands to the distribution database. The principal cannot failover to the mirror until the mirror is back online and has all transactions from the principal.|  
|High-performance mode|If the mirror is unavailable, the principal database is running exposed (that is, unmirrored). However, the Log Reader Agent only replicates those transactions that are hardened on the mirror. If service is forced and the mirror server assumes the role of the principal, the Log Reader Agent will work against the mirror and start picking up the new transactions.<br /><br /> Be aware that replication latency will increase if the mirror falls behind the principal.|  
|High-safety mode without automatic failover|All committed transactions are guaranteed to be hardened to disk on the mirror. The Log Reader Agent replicates only those transactions that are hardened on the mirror. If the mirror is unavailable, the principal disallows further activity in the database; therefore the Log Reader Agent has no transactions to replicate.|  
  
## See Also  
 [SQL Server Replication](../../relational-databases/replication/sql-server-replication.md)   
 [Log Shipping and Replication &#40;SQL Server&#41;](../../database-engine/log-shipping/log-shipping-and-replication-sql-server.md)  
  
  
