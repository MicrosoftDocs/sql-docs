---
title: "Upgrade Replicated Databases | Microsoft Docs"
ms.custom: ""
ms.date: "07/24/2016"
ms.prod: sql
ms.reviewer: ""
ms.technology: install
ms.topic: conceptual
helpviewer_keywords: 
  - "merge replication database upgrades [SQL Server replication]"
  - "replication [SQL Server], upgrading"
  - "transactional replication, upgrading databases"
  - "snapshot replication [SQL Server], upgrading databases"
  - "upgrading replicated databases"
ms.assetid: 9926a4f7-bcd8-4b9b-9dcf-5426a5857116
author: MashaMSFT
ms.author: mathoma
monikerRange: ">=sql-server-2016||=sqlallproducts-allversions"
manager: craigg
---
# Upgrade Replicated Databases

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]
  
  [!INCLUDE[ssNoversion](../../includes/ssnoversion-md.md)] supports upgrading replicated databases from previous versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]; it is not required to stop activity at other nodes while a node is being upgraded. Ensure that you adhere to the rules regarding which versions are supported in a topology:  
  
-   A Distributor can be any version as long as it is greater than or equal to the Publisher version (in many cases the Distributor is the same instance as the Publisher).    
-   A Publisher can be any version as long as it less than or equal to the Distributor version.    
-   Subscriber version depends on the type of publication:    
    - A Subscriber to a transactional publication can be any version within two versions of the Publisher version. For example: a SQL Server 2012 (11.x) Publisher can have SQL Server 2014 (12.x) and SQL Server 2016 (13.x) Subscribers; and a SQL Server 2016 (13.x) Publisher can have SQL Server 2014 (12.x) and SQL Server 2012 (11.x) Subscribers.     
    - A Subscriber to a merge publication can be all versions equal to or lower than the Publisher version which are supported as per the versions life cycle support cycle.  
 
The upgrade path to SQL Server is different depending on the deployment pattern. SQL Server offers two upgrade paths in general:
- Side-by-side: Deploy a parallel environment and move databases along with the associated instance level objects, such as logins, jobs, etc. to the new environment. 
- In-place upgrade: Allow the SQL Server installation media to upgrade the existing SQL Server installation by replacing the SQL Server bits, and upgrading the database objects. For environments running Always On Availability Groups or Failover Cluster Instances, an in-place upgrade is combined with a [rolling upgrade](choose-a-database-engine-upgrade-method.md#rolling-upgrade) to minimize downtime. 

A common approach that has been adopted for side-by-side upgrades of replication topologies is to move publisher-subscriber pairs in parts to the new side-by-side environment as opposed to a movement of the entire topology. This phased approach helps control downtime and minimize the impact to a certain extent for the business dependent on replication.  


> [!NOTE]  
> **For more detailed information regarding upgrading replication topology to SQL 2016, please see the blog post [Upgrading a Replication Topology to SQL Server 2016](https://blogs.msdn.microsoft.com/sql_server_team/upgrading-a-replication-topology-to-sql-server-2016/)**. 

 >[!WARNING]
 > Upgrading a replication topology is a multi-step process. We recommend attempting an upgrade of a replica of your replication topology in a test environment before running the upgrade on the actual production environment. This will help iron out any operational documentation that is required for handling the upgrade smoothly without incurring expensive and long downtimes during the actual upgrade process. We have seen customers reduce downtime significantly with the use of Always On Availability Groups and/or SQL Server Failover Cluster Instances for their production environments while upgrading their replication topology. Additionally, we recommend taking backups of all the databases including MSDB, Master, Distribution database(s) and the user databases participating in replication before attempting the upgrade.


## Replication Matrix

[!INCLUDE[repl matrix](../../includes/replication-compat-matrix.md)]
  
## Run the Log Reader Agent for Transactional Replication Before Upgrade  
 Before you upgrade [!INCLUDE[ssNoversion](../../includes/ssnoversion-md.md)], you must make sure that all committed transactions from published tables have been processed by the Log Reader Agent. To make sure that all transactions have been processed, perform the following steps for each database that contains transactional publications:  
  
1.  Make sure that the Log Reader Agent is running for the database. By default, the agent runs continuously.    
2.  Stop user activity on published tables.  
3.  Allow time for the Log Reader Agent to copy transactions to the distribution database, and then stop the agent.  
4.  Execute [sp_replcmds](../../relational-databases/system-stored-procedures/sp-replcmds-transact-sql.md) to verify that all transactions have been processed. The result set from this procedure should be empty.   
5.  Execute [sp_replflush](../../relational-databases/system-stored-procedures/sp-replflush-transact-sql.md) to close the connection from sp_replcmds.   
6.  Perform the server upgrade to the latest version of  [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)].   
7.  Restart [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent and the Log Reader Agent if they do not start automatically after the upgrade.  
  
## Run Agents for Merge Replication After Upgrade  
 After upgrade, run the Snapshot Agent for each merge publication and the Merge Agent for each subscription to update replication metadata. You do not have to apply the new snapshot, because it is not necessary to reinitialize subscriptions. Subscription metadata is updated the first time the Merge Agent is run after upgrade. This means that the subscription database can remain online and active during the Publisher upgrade.  
  
 Merge replication stores publication and subscription metadata in a number of system tables in the publication and subscription databases. Running the Snapshot Agent updates publication metadata and running the Merge Agent updates subscription metadata. It is only required to generate a publication snapshot. If a merge publication uses parameterized filters, each partition also has a snapshot. It is not necessary to update these partitioned snapshots.  
  
 Run the agents from [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], Replication Monitor, or from the command line. For more information about running the Snapshot Agent, see the following articles:  
  
-   [Create and Apply the Initial Snapshot](../../relational-databases/replication/create-and-apply-the-initial-snapshot.md)
-   [Start and Stop a Replication Agent &#40;SQL Server Management Studio&#41;](../../relational-databases/replication/agents/start-and-stop-a-replication-agent-sql-server-management-studio.md)
-   [Create and Apply the Initial Snapshot](../../relational-databases/replication/create-and-apply-the-initial-snapshot.md)
-   [Replication Agent Executables Concepts](../../relational-databases/replication/concepts/replication-agent-executables-concepts.md)  
  

For more information about running the Merge Agent, see the following articles:
-   [Synchronize a Pull Subscription](../../relational-databases/replication/synchronize-a-pull-subscription.md)
-   [Synchronize a Push Subscription](../../relational-databases/replication/synchronize-a-push-subscription.md)  
  

After upgrading [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in a topology that uses merge replication, change the publication compatibility level of any publications if you want to use new features.  
  
## Upgrading to Standard, Workgroup, or Express Editions  
Before upgrading from one edition of [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] to another, verify that the functionality you are currently using is supported in the edition to which you are upgrading. For more information, see the section on Replication in [Editions and supported features of SQL Server](../../sql-server/editions-and-components-of-sql-server-2017.md).  

## Steps to upgrade a replication topology
These steps outline the order in which servers in a replication topology should be upgraded. The same steps apply whether you're running transactional or merge replication. However, these steps do not cover Peer-to-Peer replication, queued updating subscriptions, nor immediate updating subscriptions. 

### In-place upgrade 
1. Upgrade the Distributor. 
2. Upgrade the Publisher and the Subscriber. These can be upgraded in any order. 

 >[!NOTE]
 > For SQL 2008 and 2008 R2, the upgrade of the publisher and subscriber must be done at the same time to align with the replication topology matrix. A SQL 2008 or 2008R2 publisher or subscriber cannot have a SQL 2016 (or greater) publisher nor subscriber. If upgrading at the same time is not possible, use an intermediate upgrade to upgrade the SQL instances to SQL 2014, and then upgrade them again to SQL 2016 (or greater).  

### Side by side upgrade
1. Upgrade the Distributor.
1. Reconfigure [Distribution](../../relational-databases/replication/configure-distribution.md) on the new SQL Server instance.
1. Upgrade the Publisher.
1. Upgrade the Subscriber.
1. Reconfigure all Publisher-Subscriber pairs, including reinitialization of the Subscriber. 


## Steps for side-by-side migration of the Distributor to Windows Server 2012 R2
If you are planning to upgrade your SQL Server instance to SQL 2016 (or greater), and your current OS is Windows 2008 (or 2008 R2), then you will need to perform a side-by side upgrade of the OS to Windows Server 2012 R2 or greater. The reason for this intermediate OS upgrade is that SQL Server 2016 cannot be installed on a Windows Server 2008/2008 R2, and Windows Server 2008/20008 R2 does not allow in-place upgrades for Failover Clusters. The following steps can be performed on either a standalone SQL Server instance, or one within an Always On Failover Cluster Instance (FCI).

1. Set up a new SQL Server instance (either standalone, or Always On Failover Cluster), edition, and version as your distributor on Windows Server 2012 R2/2016 with a different windows cluster and SQL Server FCI name or standalone host name. You will need to keep the directory structure same as the old distributor to ensure that the replication agents executables, replication folders, and database file paths are found at the same path on the new environment. This will reduce any post migration/upgrade steps required.
1. Ensure that your replication is synchronized and then shut down all of the replication agents. 
1. Shut down the current SQL Server Distributor instance. If this is a standalone instance, shut down the server. If this is a SQL FCI, then take the entire SQL Server role offline in cluster manager, including the network name. 
1. Remove the DNS and AD computer object entries for the old (current distributor instance) environment. 
1. Change the hostname of the new server to match that of the old server.
    1. If this is a SQL FCI, rename the new SQL FCI with the same virtual server name as the old instance. 
1. Copy the database files from the previous instance using SAN redirection, storage copy, or file copy. 
1. Bring the new SQL Server instance online. 
1. Restart all of the replication agents and verify if the agents are running successfully.
1. Validate if replication is working as expected. 
1. Use the SQL Server setup media to run an in-place upgrade of your SQL Server instance to the new version of SQL Server. 


  >[!NOTE]
  > To reduce downtime, we recommend that you perform the *side-by-side migration* of the distributor as one activity and the *in-place upgrade to SQL Server 2016* as another activity. This will allow you to take a phased approach, reduce risk and minimize downtime.

## Web Synchronization for Merge Replication  
 The Web synchronization option for merge replication requires that the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Replication Listener (replisapi.dll) be copied to the virtual directory on the Internet Information Services (IIS) server used for synchronization. When you configure Web synchronization, the file is copied to the virtual directory by the Configure Web Synchronization Wizard. If you upgrade the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] components installed on the IIS server, you must manually copy replisapi.dll from the COM directory to the virtual directory on the IIS server. For more information about configuring Web synchronization, see [Configure Web Synchronization](../../relational-databases/replication/configure-web-synchronization.md).  
  
## Restoring a Replicated Database from an Earlier Version  
 To ensure replication settings are retained when restoring a backup of a replicated database from a previous version: restore to a server and database with the same names as the server and database at which the backup was taken.  
  
## See Also  
 [SQL Server Replication](../../relational-databases/replication/sql-server-replication.md)  
 [Replication Administration FAQ](../../relational-databases/replication/administration/frequently-asked-questions-for-replication-administrators.md)   
 [Replication Backward Compatibility](../../relational-databases/replication/replication-backward-compatibility.md)   
 [Supported Version and Edition Upgrades](../../database-engine/install-windows/supported-version-and-edition-upgrades.md)   
 [Upgrade SQL Server](../../database-engine/install-windows/upgrade-sql-server.md)  
 [Upgrading a Replication Topology to SQL Server 2016](https://blogs.msdn.microsoft.com/sql_server_team/upgrading-a-replication-topology-to-sql-server-2016/)
