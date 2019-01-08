---
title: "CREATE AVAILABILITY GROUP (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "10/16/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "AVAILABILITY GROUP"
  - "CREATE_AVAILABILITY_TSQL"
  - "CREATE_AVAILABILITY_GROUP_TSQL"
  - "CREATE AVAILABILITY GROUP"
  - "CREATE AVAILABILITY"
  - "AVAILABILITY_GROUP_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "Availability Groups [SQL Server], listeners"
  - "CREATE AVAILABILITY GROUP statement"
  - "Availability Groups [SQL Server], creating"
  - "Availability Groups [SQL Server], Transact-SQL statements"
ms.assetid: a3d55df7-b4e4-43f3-a14b-056cba36ab98
author: "MikeRayMSFT"
ms.author: "mikeray"
manager: craigg
---
# CREATE AVAILABILITY GROUP (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  Creates a new availability group, if the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is enabled for the [!INCLUDE[ssHADR](../../includes/sshadr-md.md)] feature.  
  
> [!IMPORTANT]  
>  Execute CREATE AVAILABILITY GROUP on the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that you intend to use as the initial primary replica of your new availability group. This server instance must reside on a Windows Server Failover Clustering (WSFC) node.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```SQL  
  
CREATE AVAILABILITY GROUP group_name  
   WITH (<with_option_spec> [ ,...n ] )  
   FOR [ DATABASE database_name [ ,...n ] ]  
   REPLICA ON <add_replica_spec> [ ,...n ]  
   AVAILABILITY GROUP ON <add_availability_group_spec> [ ,...2 ]  
   [ LISTENER 'dns_name' ( <listener_option> ) ]  
[ ; ]  
  
<with_option_spec>::=   
    AUTOMATED_BACKUP_PREFERENCE = { PRIMARY | SECONDARY_ONLY| SECONDARY | NONE }  
  | FAILURE_CONDITION_LEVEL  = { 1 | 2 | 3 | 4 | 5 }   
  | HEALTH_CHECK_TIMEOUT = milliseconds  
  | DB_FAILOVER  = { ON | OFF }   
  | DTC_SUPPORT  = { PER_DB | NONE }  
  | BASIC  
  | DISTRIBUTED
  | REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT = { integer }
  | CLUSTER_TYPE = { WSFC | EXTERNAL | NONE } 
  
<add_replica_spec>::=  
  <server_instance> WITH  
    (  
       ENDPOINT_URL = 'TCP://system-address:port',  
       AVAILABILITY_MODE = { SYNCHRONOUS_COMMIT | ASYNCHRONOUS_COMMIT | CONFIGURATION_ONLY },  
       FAILOVER_MODE = { AUTOMATIC | MANUAL | EXTERNAL }  
       [ , <add_replica_option> [ ,...n ] ]  
    )   
  
  <add_replica_option>::=  
       SEEDING_MODE = { AUTOMATIC | MANUAL }  
     | BACKUP_PRIORITY = n  
     | SECONDARY_ROLE ( {   
            [ ALLOW_CONNECTIONS = { NO | READ_ONLY | ALL } ]   
        [,] [ READ_ONLY_ROUTING_URL = 'TCP://system-address:port' ]  
     } )  
     | PRIMARY_ROLE ( {   
            [ ALLOW_CONNECTIONS = { READ_WRITE | ALL } ]   
        [,] [ READ_ONLY_ROUTING_LIST = { ( '<server_instance>' [ ,...n ] ) | NONE } ]  
        [,] [ READ_WRITE_ROUTING_URL = { ( '<server_instance>' ) ] 
     } )  
     | SESSION_TIMEOUT = integer  
  
<add_availability_group_spec>::=  
 <ag_name> WITH  
    (  
       LISTENER_URL = 'TCP://system-address:port',  
       AVAILABILITY_MODE = { SYNCHRONOUS_COMMIT | ASYNCHRONOUS_COMMIT },  
       FAILOVER_MODE = MANUAL,  
       SEEDING_MODE = { AUTOMATIC | MANUAL }  
    )  
  
<listener_option> ::=  
   {  
      WITH DHCP [ ON ( <network_subnet_option> ) ]  
    | WITH IP ( { ( <ip_address_option> ) } [ , ...n ] ) [ , PORT = listener_port ]  
   }  
  
  <network_subnet_option> ::=  
     'four_part_ipv4_address', 'four_part_ipv4_mask'    
  
  <ip_address_option> ::=  
     {   
        'four_part_ipv4_address', 'four_part_ipv4_mask'  
      | 'ipv6_address'  
     }  
  
```  
  
## Arguments  
 *group_name*  
 Specifies the name of the new availability group. *group_name* must be a valid [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)][identifier](../../relational-databases/databases/database-identifiers.md), and it must be unique across all availability groups in the WSFC cluster. The maximum length for an availability group name is 128 characters.  
  
 AUTOMATED_BACKUP_PREFERENCE **=** { PRIMARY | SECONDARY_ONLY| SECONDARY | NONE }  
 Specifies a preference about how a backup job should evaluate the primary replica when choosing where to perform backups. You can script a given backup job to take the automated backup preference into account. It is important to understand that the preference is not enforced by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], so it has no impact on ad-hoc backups.  
  
 The supported values are as follows:  
  
 PRIMARY  
 Specifies that the backups should always occur on the primary replica. This option is useful if you need backup features, such as creating differential backups, that are not supported when backup is run on a secondary replica.  
  
> [!IMPORTANT]  
>  If you plan to use log shipping to prepare any secondary databases for an availability group, set the automated backup preference to **Primary** until all the secondary databases have been prepared and joined to the availability group.  
  
 SECONDARY_ONLY  
 Specifies that backups should never be performed on the primary replica. If the primary replica is the only replica online, the backup should not occur.  
  
 SECONDARY  
 Specifies that backups should occur on a secondary replica except when the primary replica is the only replica online. In that case, the backup should occur on the primary replica. This is the default behavior.  
  
 NONE  
 Specifies that you prefer that backup jobs ignore the role of the availability replicas when choosing the replica to perform backups. Note backup jobs might evaluate other factors such as backup priority of each availability replica in combination with its operational state and  connected state.  
  
> [!IMPORTANT]  
>  There is no enforcement of the AUTOMATED_BACKUP_PREFERENCE setting. The interpretation of this preference depends on the logic, if any, that you script into back jobs for the databases in a given availability group. The automated backup preference setting has no impact on ad-hoc backups. For more information, see [Configure Backup on Availability Replicas &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/configure-backup-on-availability-replicas-sql-server.md).  
  
> [!NOTE]  
>  To view the automated backup preference of an existing availability group, select the **automated_backup_preference** or **automated_backup_preference_desc** column of the [sys.availability_groups](../../relational-databases/system-catalog-views/sys-availability-groups-transact-sql.md) catalog view. Additionally, [sys.fn_hadr_backup_is_preferred_replica  &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-hadr-backup-is-preferred-replica-transact-sql.md) can be used to determine the preferred backup replica.  This function returns 1 for at least one of the replicas, even when `AUTOMATED_BACKUP_PREFERENCE = NONE`.  
  
 FAILURE_CONDITION_LEVEL **=** { 1 | 2 | **3** | 4 | 5 }  
 Specifies what failure conditions trigger an automatic failover for this availability group. FAILURE_CONDITION_LEVEL is set at the group level but is relevant only on availability replicas that are configured for synchronous-commit availability mode (AVAILABILITY_MODE **=** SYNCHRONOUS_COMMIT). Furthermore, failure conditions can trigger an automatic failover only if both the primary and secondary replicas are configured for automatic failover mode (FAILOVER_MODE **=** AUTOMATIC) and the secondary replica is currently synchronized with the primary replica.  
  
 The failure-condition levels (1-5) range from the least restrictive, level 1, to the most restrictive, level 5. A given condition level encompasses all the less restrictive levels. Thus, the strictest condition level, 5, includes the four less restrictive condition levels (1-4), level 4 includes levels 1-3, and so forth. The following table describes the failure-condition that corresponds to each level.  
  
|Level|Failure Condition|  
|-----------|-----------------------|  
|1|Specifies that an automatic failover should be initiated when any of the following occurs:<br /><br /> -The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service is down.<br /><br /> -The lease of the availability group for connecting to the WSFC cluster expires because no ACK is received from the server instance. For more information, see [How It Works: SQL Server Always On Lease Timeout](https://blogs.msdn.com/b/psssql/archive/2012/09/07/how-it-works-sql-server-AlwaysOn-lease-timeout.aspx).|  
|2|Specifies that an automatic failover should be initiated when any of the following occurs:<br /><br /> -The instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] does not connect to cluster, and the user-specified HEALTH_CHECK_TIMEOUT threshold of the availability group is exceeded.<br /><br /> -The availability replica is in failed state.|  
|3|Specifies that an automatic failover should be initiated on critical [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] internal errors, such as orphaned spinlocks, serious write-access violations, or too much dumping.<br /><br /> This is the default behavior.|  
|4|Specifies that an automatic failover should be initiated on moderate [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] internal errors, such as a persistent out-of-memory condition in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] internal resource pool.|  
|5|Specifies that an automatic failover should be initiated on any qualified failure conditions, including:<br /><br /> -Exhaustion of SQL Engine worker-threads.<br /><br /> -Detection of an unsolvable deadlock.|  
  
> [!NOTE]  
>  Lack of response by an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to client requests is not relevant to availability groups.  
  
 The FAILURE_CONDITION_LEVEL and HEALTH_CHECK_TIMEOUT values, define a *flexible failover policy* for a given group. This flexible failover policy provides you with granular control over what conditions must cause an automatic failover. For more information, see [Flexible Failover Policy for Automatic Failover of an Availability Group &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/flexible-automatic-failover-policy-availability-group.md).  
  
 HEALTH_CHECK_TIMEOUT **=** *milliseconds*  
 Specifies the wait time (in milliseconds) for the [sp_server_diagnostics](../../relational-databases/system-stored-procedures/sp-server-diagnostics-transact-sql.md) system stored procedure to return server-health information before the WSFC cluster assumes that the server instance is slow or hung. HEALTH_CHECK_TIMEOUT is set at the group level but is relevant only on availability replicas that are configured for synchronous-commit availability mode with automatic failover (AVAILABILITY_MODE **=** SYNCHRONOUS_COMMIT).  Furthermore, a health-check timeout can trigger an automatic failover only if both the primary and secondary replicas are configured for automatic failover mode (FAILOVER_MODE **=** AUTOMATIC) and the secondary replica is currently synchronized with the primary replica.  
  
 The default HEALTH_CHECK_TIMEOUT value is 30000 milliseconds (30 seconds). The minimum value is 15000 milliseconds (15 seconds), and the maximum value is 4294967295 milliseconds.  
  
> [!IMPORTANT]  
>  **sp_server_diagnostics** does not perform health checks at the database level.  
  
 DB_FAILOVER  **=** { ON | OFF }  
 Specifies the response to take when a database on the primary replica is offline. When set to ON, any status other than ONLINE for a database in the availability group triggers an automatic failover. When this option is set to OFF, only the health of the instance is used to trigger automatic failover.  
  
  For more information regarding this setting, see [Database Level Health Detection Option](../../database-engine/availability-groups/windows/sql-server-always-on-database-health-detection-failover-option.md) 
  
 DTC_SUPPORT  **=** { PER_DB | NONE }  
 Specifies whether cross-database transactions are supported through the distributed transaction coordinator (DTC). Cross-database transactions are only supported beginning in [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)]. PER_DB creates the availability group with support for these transactions. For more information, see [Cross-Database Transactions and Distributed Transactions for Always On Availability Groups and Database Mirroring &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/transactions-always-on-availability-and-database-mirroring.md).  
  
 BASIC  
 Used to create a basic availability group. Basic availability groups are limited to one database and two replicas: a primary replica and one secondary replica. This option is a replacement for the deprecated database mirroring feature on SQL Server Standard Edition. For more information, see [Basic Availability Groups &#40;Always On Availability Groups&#41;](../../database-engine/availability-groups/windows/basic-availability-groups-always-on-availability-groups.md). Basic availability groups are supported beginning in [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)].  

 DISTRIBUTED  
 Used to create a distributed availability group. This option is used with the AVAILABILITY GROUP ON parameter to connect two availability groups in separate Windows Server Failover Clusters.  For more information, see [Distributed Availability Groups &#40;Always On Availability Groups&#41;](../../database-engine/availability-groups/windows/distributed-availability-groups-always-on-availability-groups.md). Distributed availability groups are supported beginning in [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)]. 

 REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT   
 Introduced in SQL Server 2017. Used to set a minimum number of synchronous secondary replicas required to commit before the primary commits a transaction. Guarantees that SQL Server transaction waits until the transaction logs are updated on the minimum number of secondary replicas. The default is 0 which gives the same behavior as SQL Server 2016. The minimum value is 0. The maximum value is the number of replicas minus 1. This option relates to replicas in synchronous commit mode. When replicas are in synchronous commit mode, writes on the primary replica wait until writes on the secondary synchronous replicas are committed to the replica database transaction log. If a SQL Server that hosts a secondary synchronous replica stops responding, the SQL Server that hosts the primary replica marks that secondary replica as NOT SYNCHRONIZED and proceed. When the unresponsive database comes back online it is in a "not synced" state and the replica marked as unhealthy until the primary can make it synchronous again. This setting guarantees that the primary replica waits until the minimum number of replicas have committed each transaction. If the minimum number of replicas is not available then commits on the primary fail. For cluster type `EXTERNAL` the setting is changed when the availability group is added to a cluster resource. See [High availability and data protection for availability group configurations](../../linux/sql-server-linux-availability-group-ha.md).

 CLUSTER_TYPE  
 Introduced in SQL Server 2017. Used to identify if the availability group is on a Windows Server Failover Cluster (WSFC).  Set to WSFC when availability group is on a failover cluster instance on a Windows Server failover cluster. Set to EXTERNAL when the cluster is managed by a cluster manager that is not a Windows Server failover cluster, like Linux Pacemaker. Set to NONE when availability group not using WSFC for cluster coordination. For example, when an availability group includes Linux servers with no cluster manager. 

 DATABASE *database_name*  
 Specifies a list of one or more user databases on the local [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance (that is, the server instance on which you are creating the availability group). You can specify multiple databases for an availability group, but each database can belong to only one availability group. For information about the type of databases that an availability group can support, see [Prerequisites, Restrictions, and Recommendations for Always On Availability Groups &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/prereqs-restrictions-recommendations-always-on-availability.md). To find out which local databases already belong to an availability group, see the **replica_id** column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view.  
  
 The DATABASE clause is optional. If you omit it, the new availability group is empty.  
  
 After you have created the availability group, connect to each server instance that hosts a secondary replica and then prepare each secondary database and join it to the availability group. For more information, see [Start Data Movement on an Always On Secondary Database &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/start-data-movement-on-an-always-on-secondary-database-sql-server.md).  
  
> [!NOTE]  
>  Later, you can add eligible databases on the server instance that hosts the current primary replica to an availability group. You can also remove a database from an availability group. For more information, see [ALTER AVAILABILITY GROUP &#40;Transact-SQL&#41;](../../t-sql/statements/alter-availability-group-transact-sql.md).  
  
 REPLICA ON  
 Specifies from one to five SQL server instances to host availability replicas in the new availability group.  Each replica is specified by its server instance address followed by a WITH (...) clause. Minimally, you must specify your local server instance, which becomes the initial primary replica. Optionally, you can also specify up to four secondary replicas.  
  
 You need to join every secondary replica to the availability group. For more information, see [ALTER AVAILABILITY GROUP &#40;Transact-SQL&#41;](../../t-sql/statements/alter-availability-group-transact-sql.md).  
  
> [!NOTE]  
>  If you specify less than four secondary replicas when you create an availability group, you can an additional secondary replica at any time by using the [ALTER AVAILABILITY GROUP](../../t-sql/statements/alter-availability-group-transact-sql.md)[!INCLUDE[tsql](../../includes/tsql-md.md)] statement. You can also use this statement this remove any secondary replica from an existing availability group.  
  
 \<server_instance> 
 Specifies the address of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that is the host for an replica. The address format depends on whether the instance is the default instance or a named instance and whether it is a standalone instance or a failover cluster instance (FCI), as follows:  
  
 { '*system_name*[\\*instance_name*]' | '*FCI_network_name*[\\*instance_name*]' }  
  
 The components of this address are as follows:  
  
 *system_name*  
 Is the NetBIOS name of the computer system on which the target instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] resides. This computer must be a WSFC node.  
  
 *FCI_network_name*  
 Is the network name that is used to access a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] failover cluster. Use this if the server instance participates as a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] failover partner. Executing SELECT [@@SERVERNAME](../../t-sql/functions/servername-transact-sql.md) on an FCI server instance returns its entire '*FCI_network_name*[\\*instance_name*]'  string (which is the full replica name).  
  
 *instance_name*  
 Is the name of an instance of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that is hosted by *system_name* or *FCI_network_name* and that has HADR service is enabled. For a default server instance, *instance_name* is optional. The instance name is case insensitive. On a stand-alone server instance, this value name is the same as the value returned by executing SELECT [@@SERVERNAME](../../t-sql/functions/servername-transact-sql.md).  
  
 \  
 Is a separator used only when specifying *instance_name*, in order to separate it from *system_name* or *FCI_network_name*.  
  
 For information about the prerequisites for WSFC nodes and server instances, see [Prerequisites, Restrictions, and Recommendations for Always On Availability Groups &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/prereqs-restrictions-recommendations-always-on-availability.md).  
  
 ENDPOINT_URL **='**TCP**://**_system-address_**:**_port_**'**  
 Specifies the URL path for the [database mirroring endpoint](../../database-engine/database-mirroring/the-database-mirroring-endpoint-sql-server.md) on the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that hosts the availability replica that you are defining in your current REPLICA ON clause.  
  
 The ENDPOINT_URL clause is required. For more information, see [Specify the Endpoint URL When Adding or Modifying an Availability Replica &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/specify-endpoint-url-adding-or-modifying-availability-replica.md).  
  
 **'**TCP**://**_system-address_**:**_port_**'**  
 Specifies a URL for specifying an endpoint URL or read-only routing URL. The URL parameters are as follows:  
  
 *system-address*  
 Is a string, such as a system name, a fully qualified domain name, or an IP address, that unambiguously identifies the destination computer system.  
  
 *port*  
 Is a port number that is associated with the mirroring endpoint of the partner server instance (for the ENDPOINT_URL option) or the port number used by the [!INCLUDE[ssDE](../../includes/ssde-md.md)] of the server instance (for the READ_ONLY_ROUTING_URL option).  
  
 AVAILABILITY_MODE **=** { {SYNCHRONOUS_COMMIT | ASYNCHRONOUS_COMMIT | CONFIGURATION_ONLY }  
 SYNCHRONOUS_COMMIT or ASYNCHRONOUS_COMMIT specifies whether the primary replica has to wait for the secondary replica to acknowledge the hardening (writing) of the log records to disk before the primary replica can commit the transaction on a given primary database. The transactions on different databases on the same primary replica can commit independently. SQL Server 2017 CU 1 introduces CONFIGURATION_ONLY. CONFIGURATION_ONLY replica only applies to availability groups with CLUSTER_TYPE = EXTERNAL or CLUSTER_TYPE = NONE. 
  
 SYNCHRONOUS_COMMIT  
 Specifies that the primary replica waits to commit transactions until they have been hardened on this secondary replica (synchronous-commit mode). You can specify SYNCHRONOUS_COMMIT for up to three replicas, including the primary replica.  
  
 ASYNCHRONOUS_COMMIT  
 Specifies that the primary replica commits transactions without waiting for this secondary replica to harden the log (synchronous-commit availability mode). You can specify ASYNCHRONOUS_COMMIT for up to five availability replicas, including the primary replica.  

 CONFIGURATION_ONLY
 Specifies that the primary replica synchronously commit availability group configuration metadata to the master database on this replica. The replica will not contain user data. This option:

- Can be hosted on any edition of SQL Server, including Express Edition.
- Requires the data mirroring endpoint of the CONFIGURATION_ONLY replica to be type `WITNESS`.
- Can not be altered.
- Is not valid when `CLUSTER_TYPE = WSFC`. 

   For more information, see [Configuration only replica](../../linux/sql-server-linux-availability-group-ha.md).
  
 The AVAILABILITY_MODE clause is required. For more information, see [Availability Modes &#40;Always On Availability Groups&#41;](../../database-engine/availability-groups/windows/availability-modes-always-on-availability-groups.md).  
  
 FAILOVER_MODE **=** { AUTOMATIC | MANUAL }  
 Specifies the failover mode of the availability replica that you are defining.  
  
 AUTOMATIC  
 Enables automatic failover. This option is supported only if you also specify AVAILABILITY_MODE = SYNCHRONOUS_COMMIT. You can specify AUTOMATIC for two availability replicas, including the primary replica.  
  
> [!NOTE]  
>  SQL Server Failover Cluster Instances (FCIs) do not support automatic failover by availability groups, so any availability replica that is hosted by an FCI can only be configured for manual failover.  
  
 MANUAL  
 Enables planned manual failover or forced manual failover (typically called *forced failover*) by the database administrator.  
  
 The FAILOVER_MODE clause is required. The two types of manual failover, manual failover without data loss and forced failover (with possible data loss), are supported under different conditions. For more information, see [Failover and Failover Modes &#40;Always On Availability Groups&#41;](../../database-engine/availability-groups/windows/failover-and-failover-modes-always-on-availability-groups.md).  
  
 SEEDING_MODE **=** { AUTOMATIC | MANUAL }  
 Specifies how the secondary replica is initially seeded.  
  
 AUTOMATIC  
 Enables direct seeding. This method seeds the secondary replica over the network. This method does not require you to backup and restore a copy of the primary database on the replica.  
  
> [!NOTE]  
>  For direct seeding, you must allow database creation on each secondary replica by calling **ALTER AVAILABILITY GROUP** with the  **GRANT CREATE ANY DATABASE** option.  
  
 MANUAL  
 Specifies manual seeding (default). This method requires you to create a backup of the database on the primary replica and manually restore that backup on the secondary replica.  
  
 BACKUP_PRIORITY **=** *n*  
 Specifies your priority for performing backups on this replica relative to the other replicas in the same availability group. The value is an integer in the range of 0..100. These values have the following meanings:  
  
-   1..100 indicates that the availability replica could be chosen for performing backups. 1 indicates the lowest priority, and 100 indicates the highest priority. If BACKUP_PRIORITY = 1, the availability replica would be chosen for performing backups only if no higher priority availability replicas are currently available.  
  
-   0 indicates that this availability replica is not for performing backups. This is useful, for example, for a remote availability replica to which you never want backups to fail over.  
  
 For more information, see [Active Secondaries: Backup on Secondary Replicas &#40;Always On Availability Groups&#41;](../../database-engine/availability-groups/windows/active-secondaries-backup-on-secondary-replicas-always-on-availability-groups.md).  
  
 SECONDARY_ROLE **(** ... **)**  
 Specifies role-specific settings that take effect if this availability replica currently owns the secondary role (that is, whenever it is a secondary replica). Within the parentheses,  specify either or both secondary-role options. If you specify both, use a comma-separated list.  
  
 The secondary role options are as follows:  
  
 ALLOW_CONNECTIONS **=** { NO | READ_ONLY | ALL }  
 Specifies whether the databases of a given availability replica that is performing the secondary role (that is, is acting as a secondary replica) can accept connections from clients, one of:  
  
 NO  
 No user connections are allowed to secondary databases of this replica. They are not available for read access. This is the default behavior.  
  
 READ_ONLY  
 Only connections are allowed to the databases in the secondary replica where the Application Intent property is set to **ReadOnly**. For more information about this property, see [Using Connection String Keywords with SQL Server Native Client](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md).  
  
 ALL  
 All connections are allowed to the databases in the secondary replica for read-only access.  
  
 For more information, see [Active Secondaries: Readable Secondary Replicas &#40;Always On Availability Groups&#41;](../../database-engine/availability-groups/windows/active-secondaries-readable-secondary-replicas-always-on-availability-groups.md).  
  
 READ_ONLY_ROUTING_URL **='**TCP**://**_system-address_**:**_port_**'**  
 Specifies the URL to be used for routing read-intent connection requests to this availability replica. This is the URL on which the SQL Server Database Engine listens. Typically, the default instance of the SQL Server Database Engine listens on TCP port 1433.  
  
 For a named instance, you can obtain the port number by querying the **port** and **type_desc** columns of the [sys.dm_tcp_listener_states](../../relational-databases/system-dynamic-management-views/sys-dm-tcp-listener-states-transact-sql.md) dynamic management view. The server instance uses the Transact-SQL listener (**type_desc='TSQL'**).  
  
 For more information about calculating the read-only routing URL for a replica, see [Calculating read_only_routing_url for Always On](https://blogs.msdn.com/b/mattn/archive/2012/04/25/calculating-read-only-routing-url-for-AlwaysOn.aspx).  
  
> [!NOTE]  
>  For a named instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the Transact-SQL listener should be configured to use a specific port. For more information, see [Configure a Server to Listen on a Specific TCP Port &#40;SQL Server Configuration Manager&#41;](../../database-engine/configure-windows/configure-a-server-to-listen-on-a-specific-tcp-port.md).  
  
 PRIMARY_ROLE **(** ... **)**  
 Specifies role-specific settings that take effect if this availability replica currently owns the primary role (that is, whenever it is the primary replica). Within the parentheses,  specify either or both primary-role options. If you specify both, use a comma-separated list.  
  
 The primary role options are as follows:  
  
 ALLOW_CONNECTIONS **=** { READ_WRITE | ALL }  
 Specifies the type of connection that the databases of a given availability replica that is performing the primary role (that is, is acting as a primary replica) can accept from clients, one of:  
  
 READ_WRITE  
 Connections where the Application Intent connection property is set to **ReadOnly** are disallowed.  When the Application Intent property is set to **ReadWrite** or the Application Intent connection property is not set, the connection is allowed. For more information about Application Intent connection property, see [Using Connection String Keywords with SQL Server Native Client](../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md).  
  
 ALL  
 All connections are allowed to the databases in the primary replica. This is the default behavior.  
  
 READ_ONLY_ROUTING_LIST **=** { **('**\<server_instance>**'** [ **,**...*n* ] **)** | NONE } 
 Specifies a comma-separated list of server instances that host availability replicas for this availability group that meet the following requirements when running under the secondary role:  
  
-   Be configured to allow all connections or read-only connections (see the ALLOW_CONNECTIONS argument of the SECONDARY_ROLE option, above).  
  
-   Have their read-only routing URL defined (see the READ_ONLY_ROUTING_URL argument of the SECONDARY_ROLE option, above).  
  
 The READ_ONLY_ROUTING_LIST values are as follows:  
  
 \<server_instance> 
 Specifies the address of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that is the host for a replica that is a readable secondary replica when running under the secondary role.  
  
 Use a comma-separated list to specify all the server instances that might host a readable secondary replica. Read-only routing follows the order in which server instances are specified in the list. If you include a replica's host server instance on the replica's read-only routing list, placing this server instance at the end of the list is typically a good practice, so that read-intent connections go to a secondary replica, if one is available.  
  
 Beginning with [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)], you can load-balance read-intent requests across readable secondary replicas. You specify this by placing the replicas in a nested set of parentheses within the read-only routing list. For more information and examples, see [Configure load-balancing across read-only replicas](../../database-engine/availability-groups/windows/configure-read-only-routing-for-an-availability-group-sql-server.md#loadbalancing).  
  
 NONE  
 Specifies that when this availability replica is the primary replica, read-only routing is not supported. This is the default behavior.  
  
 SESSION_TIMEOUT **=** *integer*  
 Specifies the session-timeout period in seconds. If you do not specify this option, by default, the time period is 10 seconds. The minimum value is 5 seconds.  
  
> [!IMPORTANT]  
>  We recommend that you keep the time-out period at 10 seconds or greater.  
  
 For more information about the session-timeout period, see [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md).  
  
 AVAILABILITY GROUP ON  
 Specifies two availability groups that constitute a *distributed availability group*. Each availability group is part of its own Windows Server Failover Cluster (WSFC). When you create a distributed availability group, the availability group on the current SQL Server Instance becomes the primary availability group. The second availability group becomes the secondary availability group.  
  
 You need to join the secondary availability group to the distributed availability group. For more information, see [ALTER AVAILABILITY GROUP &#40;Transact-SQL&#41;](../../t-sql/statements/alter-availability-group-transact-sql.md).  
  
 \<ag_name> 
 Specifies the name  of the availability group that makes up one half of the distributed availability group.  
  
 LISTENER **='**TCP**://**_system-address_**:**_port_**'**  
 Specifies the URL path for the listener associated with the availability group.  
  
 The LISTENER clause is required.  
  
 **'**TCP**://**_system-address_**:**_port_**'**  
 Specifies a URL for the listener associated with the availability group. The URL parameters are as follows:  
  
 *system-address*  
 Is a string, such as a system name, a fully qualified domain name, or an IP address, that unambiguously identifies the listener.  
  
 *port*  
 Is a port number that is associated with the mirroring endpoint of the availability group. Note that this is not the port of the listener.  
  
 AVAILABILITY_MODE **=** { SYNCHRONOUS_COMMIT | ASYNCHRONOUS_COMMIT | CONFIGURATION_ONLY }  
 Specifies whether the primary replica has to wait for the secondary availability group to acknowledge the hardening (writing) of the log records to disk before the primary replica can commit the transaction on a given primary database.  
  
 SYNCHRONOUS_COMMIT  
 Specifies that the primary replica waits to commit transactions until they have been hardened on the secondary availability group. You can specify SYNCHRONOUS_COMMIT for up to two availability groups, including the primary availability group.  
  
 ASYNCHRONOUS_COMMIT  
 Specifies that the primary replica commits transactions without waiting for this secondary availability group to harden the log. You can specify ASYNCHRONOUS_COMMIT for up to two availability groups, including the primary availability group.  
  
 The AVAILABILITY_MODE clause is required.  
  
 FAILOVER_MODE **=** { MANUAL }  
 Specifies the failover mode of the distributed availability group.  
  
 MANUAL  
 Enables planned manual failover or forced manual failover (typically called *forced failover*) by the database administrator.  
  
 The FAILOVER_MODE clause is required, and the only option is MANUAL. Automatic failover to the secondary availability group is not supported.  
  
 SEEDING_MODE **=** { AUTOMATIC | MANUAL }  
 Specifies how the secondary availability group is initially seeded.  
  
 AUTOMATIC  
 Enables direct seeding. This method seeds the secondary availability group over the network. This method does not require you to backup and restore a copy of the primary database on the replicas of the secondary availability group.  
  
 MANUAL  
 Specifies manual seeding (default). This method requires you to create a backup of the database on the primary replica and manually restore that backup on the replica(s) of the secondary availability group.  
  
 LISTENER **'**_dns\_name_**'(** \<listener_option\> **)** 
 Defines a new availability group listener for this availability group. LISTENER is an optional argument.  
  
> [!IMPORTANT]
>  Before you create your first listener, we strongly recommend that you read [Create or Configure an Availability Group Listener &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/create-or-configure-an-availability-group-listener-sql-server.md).  
> 
>  After you create a listener for a given availability group, we strongly recommend that you do the following:  
> 
>  -   Ask your network administrator to reserve the listener's IP address for its exclusive use.  
> -   Give the listener's DNS host name to application developers to use in connection strings when requesting client connections to this availability group.  
  
 *dns_name*  
 Specifies the DNS host name of the availability group listener. The DNS name of the listener must be unique in the domain and in NetBIOS.  
  
 *dns_name* is a string value. This name can contain only alphanumeric characters, dashes (-), and hyphens (_), in any order. DNS host names are case insensitive. The maximum length is 63 characters.  
  
 We recommend that you specify a meaningful string. For example, for an availability group named `AG1`, a meaningful DNS host name would be `ag1-listener`.  
  
> [!IMPORTANT]  
>  NetBIOS recognizes only the first 15 chars in the dns_name. If you have two WSFC clusters that are controlled by the same Active Directory and you try to create availability group listeners in both clusters using names with more than 15 characters and an identical 15 character prefix, an error reports that the Virtual Network Name resource could not be brought online. For information about prefix naming rules for DNS names, see [Assigning Domain Names](https://technet.microsoft.com/library/cc731265\(WS.10\).aspx).  
  
 \<listener_option> 
 LISTENER takes one of the following \<listener_option> options: 
  
 WITH DHCP [ ON { **('**_four\_part\_ipv4\_address_**','**_four\_part\_ipv4\_mask_**')** } ]  
 Specifies that the availability group listener uses the Dynamic Host Configuration Protocol (DHCP).  Optionally, use the ON clause to identify the network on which this listener is created. DHCP is limited to a single subnet that is used for every server instances that hosts a replica in the availability group.  
  
> [!IMPORTANT]  
>  We do not recommend DHCP in production environment. If there is a down time and the DHCP IP lease expires, extra time is required to register the new DHCP network IP address that is associated with the listener DNS name and impact the client connectivity. However, DHCP is good for setting up your development and testing environment to verify basic functions of availability groups and for integration with your applications.  
  
 For example:  
  
 `WITH DHCP ON ('10.120.19.0','255.255.254.0')`  
  
 WITH IP **(** { **('**_four\_part\_ipv4\_address_**','**_four\_part\_ipv4\_mask_**')** | **('**_ipv6\_address_**')** } [ **,** ...*n* ] **)** [ **,** PORT **=**_listener\_port_ ]  
 Specifies that, instead of using DHCP, the availability group listener uses one or more static IP addresses. To create an availability group across multiple subnets, each subnet requires one static IP address in the listener configuration. For a given subnet, the static IP address can be either an IPv4 address or an IPv6 address. Contact your network administrator to get a static IP address for each subnet that hosts a replica for the new availability group.  
  
 For example:  
  
 `WITH IP ( ('10.120.19.155','255.255.254.0') )`  
  
 *four_part_ipv4_address*  
 Specifies an IPv4 four-part address for an availability group listener. For example, `10.120.19.155`.  
  
 *four_part_ipv4_mask*  
 Specifies an IPv4 four-part mask for an availability group listener. For example, `255.255.254.0`.  
  
 *ipv6_address*  
 Specifies an IPv6 address for an availability group listener. For example, `2001::4898:23:1002:20f:1fff:feff:b3a3`.  
  
 PORT **=** *listener_port*  
 Specifies the port number-*listener_port*-to be used by an availability group listener that is specified by a WITH IP clause. PORT is optional.  
  
 The default port number, 1433, is supported. However, if you have security concerns, we recommend using a different port number.  
  
 For example: `WITH IP ( ('2001::4898:23:1002:20f:1fff:feff:b3a3') ) , PORT = 7777`  
  
## Prerequisites and Restrictions  
 For information about the prerequisites for creating an availability group, see [Prerequisites, Restrictions, and Recommendations for Always On Availability Groups &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/prereqs-restrictions-recommendations-always-on-availability.md).  
  
 For information about restrictions on the AVAILABILITY GROUP Transact-SQL statements, see [Overview of Transact-SQL Statements for Always On Availability Groups &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/transact-sql-statements-for-always-on-availability-groups.md).  
  
## Security  
  
### Permissions  
 Requires membership in the **sysadmin** fixed server role and either CREATE AVAILABILITY GROUP server permission, ALTER ANY AVAILABILITY GROUP permission, or CONTROL SERVER permission.  
  
## Examples  
  
### A. Configuring Backup on Secondary Replicas, Flexible Failover Policy, and Connection Access  
 The following example creates an availability group named `MyAg` for two user databases, `ThisDatabase` and `ThatDatabase`. The following table summarizes the values specified for the options that are set for the availability group as a whole.  
  
|Group Option|Setting|Description|  
|------------------|-------------|-----------------|  
|AUTOMATED_BACKUP_PREFERENCE|SECONDARY|This automated backup preference indicates that backups should occur on a secondary replica except when the primary replica is the only replica online (this is the default behavior). For the AUTOMATED_BACKUP_PREFERENCE setting to have any effect, you need to script backup jobs on the availability databases to take the automated backup preference into account.|  
|FAILURE_CONDITION_LEVEL|3|This failure condition level setting specifies that an automatic failover should be initiated on critical SQL Server internal errors, such as orphaned spinlocks, serious write-access violations, or too much dumping.|  
|HEALTH_CHECK_TIMEOUT|600000|This health check timeout value, 60 seconds, specifies that the WSFC cluster waits 60000 milliseconds for the [sp_server_diagnostics](../../relational-databases/system-stored-procedures/sp-server-diagnostics-transact-sql.md) system stored procedure to return server-health information about a server instance that is hosting a synchronous-commit replica with automatic before the cluster assumes that the host server instance is slow or hung. (The default value is 30000 milliseconds).|  
  
 Three availability replicas are to be hosted by the default server instances on computers named `COMPUTER01`, `COMPUTER02`, and `COMPUTER03`. The following table summarizes the values specified for the replica options of each replica.  
  
|Replica Option|Setting on `COMPUTER01`|Setting on `COMPUTER02`|Setting on `COMPUTER03`|Description|  
|--------------------|-----------------------------|-----------------------------|-----------------------------|-----------------|  
|ENDPOINT_URL|TCP://*COMPUTER01:5022*|TCP://*COMPUTER02:5022*|TCP://*COMPUTER03:5022*|In this example, the systems are the same domain, so the endpoint URLs can use the name of the computer system as the system address.|  
|AVAILABILITY_MODE|SYNCHRONOUS_COMMIT|SYNCHRONOUS_COMMIT|ASYNCHRONOUS_COMMIT|Two of the replicas use synchronous-commit mode. When synchronized, they support failover without data loss. The third replica, which uses asynchronous-commit availability mode.|  
|FAILOVER_MODE|AUTOMATIC|AUTOMATIC|MANUAL|The synchronous-commit replicas support automatic failover and planned manual failover. The synchronous-commit availability mode replica supports only forced manual failover.|  
|BACKUP_PRIORITY|30|30|90|A higher priority, 90, is assigned to the asynchronous-commit replica, than to the synchronous-commit replicas. Backups tend to occur on the server instance that hosts the asynchronous-commit replica.|  
|SECONDARY_ROLE|( ALLOW_CONNECTIONS = NO,<br /><br /> READ_ONLY_ROUTING_URL = 'TCP://COMPUTER01:1433' )|( ALLOW_CONNECTIONS = NO,<br /><br /> READ_ONLY_ROUTING_URL = 'TCP://COMPUTER02:1433' )|( ALLOW_CONNECTIONS = READ_ONLY, <br />READ_ONLY_ROUTING_URL = 'TCP://COMPUTER03:1433' )|Only the asynchronous-commit replica serves as a readable secondary replica.<br /><br /> Specifies the computer name and default Database Engine port number (1433).<br /><br /> This argument is optional.|  
|PRIMARY_ROLE|( ALLOW_CONNECTIONS = READ_WRITE, <br />READ_ONLY_ROUTING_LIST = (COMPUTER03) )|( ALLOW_CONNECTIONS = READ_WRITE, <br />READ_ONLY_ROUTING_LIST = (COMPUTER03) )|( ALLOW_CONNECTIONS = READ_WRITE, <br />READ_ONLY_ROUTING_LIST = NONE )|In the primary role, all the replicas reject read-intent connection attempts.<br /><br /> Read-intent connection requests are routed to COMPUTER03 if the local replica is running under the secondary role. When that replica runs under the primary role, read-only routing is disabled.<br /><br /> This argument is optional.|  
|SESSION_TIMEOUT|10|10|10|This example specifies the default session timeout value (10). This argument is optional.|  
  
 Finally, the example specifies the optional LISTENER clause to create an availability group listener for the new availability group. A unique DNS name, `MyAgListenerIvP6`, is specified for this listener. The two replicas are on different subnets, so the listener must use static IP addresses. For each of the two availability replicas, the WITH IP clause specifies a static IP address, `2001:4898:f0:f00f::cf3c` and `2001:4898:e0:f213::4ce2`, which use the IPv6 format. This example also specifies uses the optional PORT argument to specify port `60173` as the listener port.  
  
```SQL
CREATE AVAILABILITY GROUP MyAg   
   WITH (  
      AUTOMATED_BACKUP_PREFERENCE = SECONDARY,  
      FAILURE_CONDITION_LEVEL  =  3,   
      HEALTH_CHECK_TIMEOUT = 600000  
       )  
  
   FOR   
      DATABASE  ThisDatabase, ThatDatabase   
   REPLICA ON   
      'COMPUTER01' WITH   
         (  
         ENDPOINT_URL = 'TCP://COMPUTER01:5022',  
         AVAILABILITY_MODE = SYNCHRONOUS_COMMIT,  
         FAILOVER_MODE = AUTOMATIC,  
         BACKUP_PRIORITY = 30,  
         SECONDARY_ROLE (ALLOW_CONNECTIONS = NO,   
            READ_ONLY_ROUTING_URL = 'TCP://COMPUTER01:1433' ),
         PRIMARY_ROLE (ALLOW_CONNECTIONS = READ_WRITE,   
            READ_ONLY_ROUTING_LIST = (COMPUTER03) ),  
         SESSION_TIMEOUT = 10  
         ),   
  
      'COMPUTER02' WITH   
         (  
         ENDPOINT_URL = 'TCP://COMPUTER02:5022',  
         AVAILABILITY_MODE = SYNCHRONOUS_COMMIT,  
         FAILOVER_MODE = AUTOMATIC,  
         BACKUP_PRIORITY = 30,  
         SECONDARY_ROLE (ALLOW_CONNECTIONS = NO,   
            READ_ONLY_ROUTING_URL = 'TCP://COMPUTER02:1433' ),  
         PRIMARY_ROLE (ALLOW_CONNECTIONS = READ_WRITE,   
            READ_ONLY_ROUTING_LIST = (COMPUTER03) ),  
         SESSION_TIMEOUT = 10  
         ),   
  
      'COMPUTER03' WITH   
         (  
         ENDPOINT_URL = 'TCP://COMPUTER03:5022',  
         AVAILABILITY_MODE = ASYNCHRONOUS_COMMIT,  
         FAILOVER_MODE =  MANUAL,  
         BACKUP_PRIORITY = 90,  
         SECONDARY_ROLE (ALLOW_CONNECTIONS = READ_ONLY,   
            READ_ONLY_ROUTING_URL = 'TCP://COMPUTER03:1433' ),  
         PRIMARY_ROLE (ALLOW_CONNECTIONS = READ_WRITE,   
            READ_ONLY_ROUTING_LIST = NONE ),  
         SESSION_TIMEOUT = 10  
         );
GO  
ALTER AVAILABILITY GROUP [MyAg]
  ADD LISTENER 'MyAgListenerIvP6' ( WITH IP ( ('2001:db88:f0:f00f::cf3c'),('2001:4898:e0:f213::4ce2') ) , PORT = 60173 );   
GO  
```  
  
##  <a name="RelatedTasks"></a> Related Tasks  
  
-   [Create an Availability Group &#40;Transact-SQL&#41;](../../database-engine/availability-groups/windows/create-an-availability-group-transact-sql.md)  
  
-   [Use the Availability Group Wizard &#40;SQL Server Management Studio&#41;](../../database-engine/availability-groups/windows/use-the-availability-group-wizard-sql-server-management-studio.md)  
  
-   [Use the New Availability Group Dialog Box &#40;SQL Server Management Studio&#41;](../../database-engine/availability-groups/windows/use-the-new-availability-group-dialog-box-sql-server-management-studio.md)  
  
-   [Use the Availability Group Wizard &#40;SQL Server Management Studio&#41;](../../database-engine/availability-groups/windows/use-the-availability-group-wizard-sql-server-management-studio.md)  
  
## See Also  
 [ALTER AVAILABILITY GROUP &#40;Transact-SQL&#41;](../../t-sql/statements/alter-availability-group-transact-sql.md)   
 [ALTER DATABASE SET HADR &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql-set-hadr.md)   
 [DROP AVAILABILITY GROUP &#40;Transact-SQL&#41;](../../t-sql/statements/drop-availability-group-transact-sql.md)   
 [Troubleshoot Always On Availability Groups Configuration &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/troubleshoot-always-on-availability-groups-configuration-sql-server.md)   
 [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)   
 [Availability Group Listeners, Client Connectivity, and Application Failover &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/listeners-client-connectivity-application-failover.md)  
  
  

