---
title: "ALTER AVAILABILITY GROUP (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "01/02/2018"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "ALTER_AVAILABILITY_GROUP_TSQL"
  - "ALTER_AVAILABILITY_TSQL"
  - "ALTER AVAILABILITY GROUP"
  - "ALTER AVAILABILITY"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "Availability Groups [SQL Server], availability replicas"
  - "ALTER AVAILABILITY GROUP statement"
  - "Availability Groups [SQL Server], configuring"
  - "Availability Groups [SQL Server], Transact-SQL statements"
ms.assetid: f039d0de-ade7-4aaf-8b7b-d207deb3371a
author: "MikeRayMSFT"
ms.author: "mikeray"
manager: craigg
---
# ALTER AVAILABILITY GROUP (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  Alters an existing Always On availability group in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Most ALTER AVAILABILITY GROUP arguments are supported only the current primary replica. However the JOIN, FAILOVER, and FORCE_FAILOVER_ALLOW_DATA_LOSS arguments are supported only on secondary replicas.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```SQL  
  
ALTER AVAILABILITY GROUP group_name   
  {  
     SET ( <set_option_spec> )   
   | ADD DATABASE database_name   
   | REMOVE DATABASE database_name  
   | ADD REPLICA ON <add_replica_spec>   
   | MODIFY REPLICA ON <modify_replica_spec>  
   | REMOVE REPLICA ON <server_instance>  
   | JOIN  
   | JOIN AVAILABILITY GROUP ON <add_availability_group_spec> [ ,...2 ]  
   | MODIFY AVAILABILITY GROUP ON <modify_availability_group_spec> [ ,...2 ]  
   | GRANT CREATE ANY DATABASE  
   | DENY CREATE ANY DATABASE  
   | FAILOVER  
   | FORCE_FAILOVER_ALLOW_DATA_LOSS   
   | ADD LISTENER 'dns_name' ( <add_listener_option> )  
   | MODIFY LISTENER 'dns_name' ( <modify_listener_option> )  
   | RESTART LISTENER 'dns_name'  
   | REMOVE LISTENER 'dns_name'  
   | OFFLINE  
  }  
[ ; ]  
  
<set_option_spec> ::=   
    AUTOMATED_BACKUP_PREFERENCE = { PRIMARY | SECONDARY_ONLY| SECONDARY | NONE }  
  | FAILURE_CONDITION_LEVEL  = { 1 | 2 | 3 | 4 | 5 }   
  | HEALTH_CHECK_TIMEOUT = milliseconds  
  | DB_FAILOVER  = { ON | OFF }   
  | DTC_SUPPORT  = { PER_DB | NONE }  
  | REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT = { integer }
  
<server_instance> ::=   
 { 'system_name[\instance_name]' | 'FCI_network_name[\instance_name]' }  
  
<add_replica_spec>::=  
  <server_instance> WITH  
    (  
       ENDPOINT_URL = 'TCP://system-address:port',  
       AVAILABILITY_MODE = { SYNCHRONOUS_COMMIT | ASYNCHRONOUS_COMMIT | CONFIGURATION_ONLY },  
       FAILOVER_MODE = { AUTOMATIC | MANUAL }   
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
  
<modify_replica_spec>::=  
  <server_instance> WITH  
    (    
       ENDPOINT_URL = 'TCP://system-address:port'   
     | AVAILABILITY_MODE = { SYNCHRONOUS_COMMIT | ASYNCHRONOUS_COMMIT }   
     | FAILOVER_MODE = { AUTOMATIC | MANUAL }   
     | SEEDING_MODE = { AUTOMATIC | MANUAL }   
     | BACKUP_PRIORITY = n  
     | SECONDARY_ROLE ( {   
          ALLOW_CONNECTIONS = { NO | READ_ONLY | ALL }   
        | READ_ONLY_ROUTING_URL = 'TCP://system-address:port'   
          } )  
     | PRIMARY_ROLE ( {   
          ALLOW_CONNECTIONS = { READ_WRITE | ALL }   
        | READ_ONLY_ROUTING_LIST = { ( '<server_instance>' [ ,...n ] ) | NONE }   
          } )  
     | SESSION_TIMEOUT = seconds  
    )   
  
<add_availability_group_spec>::=  
 <ag_name> WITH  
    (  
       LISTENER_URL = 'TCP://system-address:port',  
       AVAILABILITY_MODE = { SYNCHRONOUS_COMMIT | ASYNCHRONOUS_COMMIT },  
       FAILOVER_MODE = MANUAL,  
       SEEDING_MODE = { AUTOMATIC | MANUAL }  
    )  
  
<modify_availability_group_spec>::=  
 <ag_name> WITH  
    (  
       LISTENER = 'TCP://system-address:port'  
       | AVAILABILITY_MODE = { SYNCHRONOUS_COMMIT | ASYNCHRONOUS_COMMIT }  
       | SEEDING_MODE = { AUTOMATIC | MANUAL }  
    )  
  
<add_listener_option> ::=  
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
  
<modify_listener_option>::=  
    {  
       ADD IP ( <ip_address_option> )   
     | PORT = listener_port  
    }  
  
```  
  
## Arguments  
 *group_name*  
 Specifies the name of the new availability group. *group_name* must be a valid [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] identifier, and it must be unique across all availability groups in the WSFC cluster.  
  
 AUTOMATED_BACKUP_PREFERENCE **=** { PRIMARY | SECONDARY_ONLY| SECONDARY | NONE }  
 Specifies a preference about how a backup job should evaluate the primary replica when choosing where to perform backups. You can script a given backup job to take the automated backup preference into account. It is important to understand that the preference is not enforced by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], so it has no impact on ad hoc backups.  
  
 Supported only on the primary replica.  
  
 The values are as follows:  
  
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
>  There is no enforcement of the AUTOMATED_BACKUP_PREFERENCE setting. The interpretation of this preference depends on the logic, if any, that you script into back jobs for the databases in a given availability group. The automated backup preference setting has no impact on ad hoc backups. For more information, see [Configure Backup on Availability Replicas &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/configure-backup-on-availability-replicas-sql-server.md).  
  
> [!NOTE]  
>  To view the automated backup preference of an existing availability group, select the **automated_backup_preference** or **automated_backup_preference_desc** column of the [sys.availability_groups](../../relational-databases/system-catalog-views/sys-availability-groups-transact-sql.md) catalog view. Additionally, [sys.fn_hadr_backup_is_preferred_replica  &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-hadr-backup-is-preferred-replica-transact-sql.md) can be used to determine the preferred backup replica.  This function will always return 1 for at least one of the replicas, even when `AUTOMATED_BACKUP_PREFERENCE = NONE`.  
  
 FAILURE_CONDITION_LEVEL **=** { 1 | 2 | **3** | 4 | 5 }  
 Specifies what failure conditions will trigger an automatic failover for this availability group. FAILURE_CONDITION_LEVEL is set at the group level but is relevant only on availability replicas that are configured for synchronous-commit availability mode (AVAILABILITY_MODE **=** SYNCHRONOUS_COMMIT). Furthermore, failure conditions can trigger an automatic failover only if both the primary and secondary replicas are configured for automatic failover mode (FAILOVER_MODE **=** AUTOMATIC) and the secondary replica is currently synchronized with the primary replica.  
  
 Supported only on the primary replica.  
  
 The failure-condition levels (1-5) range from the least restrictive, level 1, to the most restrictive, level 5. A given condition level encompasses all of the less restrictive levels. Thus, the strictest condition level, 5, includes the four less restrictive condition levels (1-4), level 4 includes levels 1-3, and so forth. The following table describes the failure-condition that corresponds to each level.  
  
|Level|Failure Condition|  
|-----------|-----------------------|  
|1|Specifies that an automatic failover should be initiated when any of the following occurs:<br /><br /> The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service is down.<br /><br /> The lease of the availability group for connecting to the WSFC cluster expires because no ACK is received from the server instance. For more information, see [How It Works: SQL Server Always On Lease Timeout](https://blogs.msdn.com/b/psssql/archive/2012/09/07/how-it-works-sql-server-Always%20On-lease-timeout.aspx).|  
|2|Specifies that an automatic failover should be initiated when any of the following occurs:<br /><br /> The instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] does not connect to cluster, and the user-specified HEALTH_CHECK_TIMEOUT threshold of the availability group is exceeded.<br /><br /> The availability replica is in failed state.|  
|3|Specifies that an automatic failover should be initiated on critical [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] internal errors, such as orphaned spinlocks, serious write-access violations, or too much dumping.<br /><br /> This is the default behavior.|  
|4|Specifies that an automatic failover should be initiated on moderate [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] internal errors, such as a persistent out-of-memory condition in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] internal resource pool.|  
|5|Specifies that an automatic failover should be initiated on any qualified failure conditions, including:<br /><br /> Exhaustion of SQL Engine worker-threads.<br /><br /> Detection of an unsolvable deadlock.|  
  
> [!NOTE]  
>  Lack of response by an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to client requests is not relevant to availability groups.  
  
 The FAILURE_CONDITION_LEVEL and HEALTH_CHECK_TIMEOUT values, define a *flexible failover policy* for a given group. This flexible failover policy provides you with granular control over what conditions must cause an automatic failover. For more information, see [Flexible Failover Policy for Automatic Failover of an Availability Group &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/flexible-automatic-failover-policy-availability-group.md).  
  
 HEALTH_CHECK_TIMEOUT **=** *milliseconds*  
 Specifies the wait time (in milliseconds) for the [sp_server_diagnostics](../../relational-databases/system-stored-procedures/sp-server-diagnostics-transact-sql.md) system stored procedure to return server-health information before WSFC cluster assumes that the server instance is slow or hung. HEALTH_CHECK_TIMEOUT is set at the group level but is relevant only on availability replicas that are configured for synchronous-commit availability mode with automatic failover (AVAILABILITY_MODE **=** SYNCHRONOUS_COMMIT).  Furthermore, a health-check timeout can trigger an automatic failover only if both the primary and secondary replicas are configured for automatic failover mode (FAILOVER_MODE **=** AUTOMATIC) and the secondary replica is currently synchronized with the primary replica.  
  
 The default HEALTH_CHECK_TIMEOUT value is 30000 milliseconds (30 seconds). The minimum value is 15000 milliseconds (15 seconds), and the maximum value is 4294967295 milliseconds.  
  
 Supported only on the primary replica.  
  
> [!IMPORTANT]  
>  **sp_server_diagnostics** does not perform health checks at the database level.  
  
 DB_FAILOVER  **=** { ON | OFF }  
 Specifies the response to take when a database on the primary replica is offline. When set to ON, any status other than ONLINE for a database in the availability group triggers an automatic failover. When this option is set to OFF, only the health of the instance is used to trigger automatic failover.  
 
 For more information regarding this setting, see [Database Level Health Detection Option](../../database-engine/availability-groups/windows/sql-server-always-on-database-health-detection-failover-option.md) 

DTC_SUPPORT  **=** { PER_DB | NONE }  
Specifies whether distributed transactions are enabled for this Availability Group. Distributed transactions are only supported for availability group databases beginning in [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)], and cross-database transactions are only supported beginning in [!INCLUDE[ssSQL16](../../includes/sssql15-md.md)] SP2. `PER_DB` creates the availability group with support for these transactions and will automatically promote cross-database transactions involving database(s) in the Availability Group into distributed transactions. `NONE` prevents the automatic promotion of cross-database transactions to distributed transactions and does not register the database with a stable RMID in DTC. Distributed transactions are not prevented when the `NONE` setting is used, but database failover and automatic recovery may not succeed under some circumstances. For more information, see [Cross-Database Transactions and Distributed Transactions for Always On Availability Groups and Database Mirroring &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/transactions-always-on-availability-and-database-mirroring.md). 
 
> [!NOTE]
> Support for changing the DTC_SUPPORT setting of an Availability Group was introduced in [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] Service Pack 2. This option cannot be used with earlier versions. To change this setting in earlier versions of [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)], you must DROP and CREATE the availability group again.
 
 REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT   
 Introduced in SQL Server 2017. Used to set a minimum number of synchronous secondary replicas required to commit before the primary commits a transaction. Guarantees that SQL Server transactions will wait until the transaction logs are updated on the minimum number of secondary replicas. The default is 0 which gives the same behavior as SQL Server 2016. The minimum value is 0. The maximum value is the number of replicas minus 1. This option relates to replicas in synchronous commit mode. When replicas are in synchronous commit mode, writes on the primary replica wait until writes on the secondary synchronous replicas are committed to the replica database transaction log. If a SQL Server that hosts a secondary synchronous replica stops responding, the SQL Server that hosts the primary replica will mark that secondary replica as NOT SYNCHRONIZED and proceed. When the unresponsive database comes back online it will be in a "not synced" state and the replica will be marked as unhealthy until the primary can make it synchronous again. This setting guarantees that the primary replica will not proceed until the minimum number of replicas have committed each transaction. If the minimum number of replicas is not available then commits on the primary will fail. For cluster type `EXTERNAL` the setting is changed when the availability group is added to a cluster resource. See [High availability and data protection for availability group configurations](../../linux/sql-server-linux-availability-group-ha.md).
  
 ADD DATABASE *database_name*  
 Specifies a list of one or more user databases that you want to add to the availability group. These databases must reside on the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that hosts the current primary replica. You can specify multiple databases for an availability group, but each database can belong to only one availability group. For information about the type of databases that an availability group can support, see [Prerequisites, Restrictions, and Recommendations for Always On Availability Groups &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/prereqs-restrictions-recommendations-always-on-availability.md). To find out which local databases already belong to an availability group, see the **replica_id** column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view.  
  
 Supported only on the primary replica.  
  
> [!NOTE]  
>  After you have created the availability group, you will need connect to each server instance that hosts a secondary replica and then prepare each secondary database and join it to the availability group. For more information, see [Start Data Movement on an Always On Secondary Database &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/start-data-movement-on-an-always-on-secondary-database-sql-server.md).  
  
 REMOVE DATABASE *database_name*  
 Removes the specified primary database and the corresponding secondary databases from the availability group. Supported only on the primary replica.  
  
 For information about the recommended follow up after removing an availability database from an availability group, see [Remove a Primary Database from an Availability Group &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/remove-a-primary-database-from-an-availability-group-sql-server.md).  
  
 ADD REPLICA ON  
 Specifies from one to eight SQL server instances to host secondary replicas in an availability group.  Each replica is specified by its server instance address followed by a WITH (...) clause.  
  
 Supported only on the primary replica.  
  
 You need to join every new secondary replica to the availability group. For more information, see the description of the JOIN option, later in this section.  
  
 \<server_instance>  
 Specifies the address of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that is the host for a replica. The address format depends on whether the instance is the default instance or a named instance and whether it is a standalone instance or a failover cluster instance (FCI). The syntax is as follows:  
  
 { '*system_name*[\\*instance_name*]' | '*FCI_network_name*[\\*instance_name*]' }  
  
 The components of this address are as follows:  
  
 *system_name*  
 Is the NetBIOS name of the computer system on which the target instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] resides. This computer must be a WSFC node.  
  
 *FCI_network_name*  
 Is the network name that is used to access a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] failover cluster. Use this if the server instance participates as a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] failover partner. Executing SELECT [@@SERVERNAME](../../t-sql/functions/servername-transact-sql.md) on an FCI server instance returns its entire '*FCI_network_name*[\\*instance_name*]'  string (which is the full replica name).  
  
 *instance_name*  
 Is the name of an instance of a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that is hosted by *system_name* or *FCI_network_name* and that has Always On enabled. For a default server instance, *instance_name* is optional. The instance name is case insensitive. On a stand-alone server instance, this value name is the same as the value returned by executing SELECT [@@SERVERNAME](../../t-sql/functions/servername-transact-sql.md).  
  
 \  
 Is a separator used only when specifying *instance_name*, in order to separate it from *system_name* or *FCI_network_name*.  
  
 For information about the prerequisites for WSFC nodes and server instances, see [Prerequisites, Restrictions, and Recommendations for Always On Availability Groups &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/prereqs-restrictions-recommendations-always-on-availability.md).  
  
 ENDPOINT_URL ='TCP://*system-address*:*port*'  
 Specifies the URL path for the [database mirroring endpoint](../../database-engine/database-mirroring/the-database-mirroring-endpoint-sql-server.md) on the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that will host the availability replica that you are adding or modifying.  
  
 ENDPOINT_URL is required in the ADD REPLICA ON clause and optional in the MODIFY REPLICA ON clause.  For more information, see [Specify the Endpoint URL When Adding or Modifying an Availability Replica &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/specify-endpoint-url-adding-or-modifying-availability-replica.md).  
  
 **'**TCP**://**_system-address_**:**_port_**'**  
 Specifies a URL for specifying an endpoint URL or read-only routing URL. The URL parameters are as follows:  
  
 *system-address*  
 Is a string, such as a system name, a fully qualified domain name, or an IP address, that unambiguously identifies the destination computer system.  
  
 *port*  
 Is a port number that is associated with the mirroring endpoint of the server instance (for the ENDPOINT_URL option) or the port number used by the [!INCLUDE[ssDE](../../includes/ssde-md.md)] of the server instance (for the READ_ONLY_ROUTING_URL option).  
  
 AVAILABILITY_MODE **=** { SYNCHRONOUS_COMMIT | ASYNCHRONOUS_COMMIT | CONFIGURATION_ONLY }  
 Specifies whether the primary replica has to wait for the secondary replica to acknowledge the hardening (writing) of the log records to disk before the primary replica can commit the transaction on a given primary database. The transactions on different databases on the same primary replica can commit independently.  
  
 SYNCHRONOUS_COMMIT  
 Specifies that the primary replica will wait to commit transactions until they have been hardened on this secondary replica (synchronous-commit mode). You can specify SYNCHRONOUS_COMMIT for up to three replicas, including the primary replica.  
  
 ASYNCHRONOUS_COMMIT  
 Specifies that the primary replica commits transactions without waiting for this secondary replica to harden the log (synchronous-commit availability mode). You can specify ASYNCHRONOUS_COMMIT for up to five availability replicas, including the primary replica.  

 CONFIGURATION_ONLY
 Specifies that the primary replica synchronously commit availability group configuration metadata to the master database on this replica. The replica will not contain user data. This option:

- Can be hosted on any edition of SQL Server, including Express Edition.
- Requires the data mirroring endpoint of the CONFIGURATION_ONLY replica to be type `WITNESS`.
- Can not be altered.
- Is not valid when `CLUSTER_TYPE = WSFC`. 

   For more information, see [Configuration only replica](../../linux/sql-server-linux-availability-group-ha.md).
    
 AVAILABILITY_MODE is required in the ADD REPLICA ON clause and optional in the MODIFY REPLICA ON clause. For more information, see [Availability Modes &#40;Always On Availability Groups&#41;](../../database-engine/availability-groups/windows/availability-modes-always-on-availability-groups.md).  
  
 FAILOVER_MODE **=** { AUTOMATIC | MANUAL }  
 Specifies the failover mode of the availability replica that you are defining.  
  
 AUTOMATIC  
 Enables automatic failover. AUTOMATIC is supported only if you also specify AVAILABILITY_MODE = SYNCHRONOUS_COMMIT. You can specify AUTOMATIC for three availability replicas, including the primary replica.  
  
> [!NOTE]  
>  Prior to SQL Server 2016, you were limited to two automatic failover replicas, including the primary replica
  
> [!NOTE]  
>  SQL Server Failover Cluster Instances (FCIs) do not support automatic failover by availability groups, so any availability replica that is hosted by an FCI can only be configured for manual failover.  
  
 MANUAL  
 Enables manual failover or forced manual failover (*forced failover*) by the database administrator.  
  
 FAILOVER_MODE is required in the ADD REPLICA ON clause and optional in the MODIFY REPLICA ON clause. Two types of manual failover exist, manual failover without data loss and forced failover (with possible data loss), which are supported under different conditions.  For more information, see [Failover and Failover Modes &#40;Always On Availability Groups&#41;](../../database-engine/availability-groups/windows/failover-and-failover-modes-always-on-availability-groups.md).  
  
 SEEDING_MODE **=** { AUTOMATIC | MANUAL }  
 Specifies how the secondary replica will be initially seeded.  
  
 AUTOMATIC  
 Enables direct seeding. This method will seed the secondary replica over the network. This method does not require you to backup and restore a copy of the primary database on the replica.  
  
> [!NOTE]  
>  For direct seeding, you must allow database creation on each secondary replica by calling **ALTER AVAILABILITY GROUP** with the  **GRANT CREATE ANY DATABASE** option.  
  
 MANUAL  
 Specifies manual seeding (default). This method requires you to create a backup of the database on the primary replica and manually restore that backup on the secondary replica.  
  
 BACKUP_PRIORITY **=**_n_  
 Specifies your priority for performing backups on this replica relative to the other replicas in the same availability group. The value is an integer in the range of 0..100. These values have the following meanings:  
  
-   1..100 indicates that the availability replica could be chosen for performing backups. 1 indicates the lowest priority, and 100 indicates the highest priority. If BACKUP_PRIORITY = 1, the availability replica would be chosen for performing backups only if no higher priority availability replicas are currently available.  
  
-   0 indicates that this availability replica will never be chosen for performing backups. This is useful, for example, for a remote availability replica to which you never want backups to fail over.  
  
 For more information, see [Active Secondaries: Backup on Secondary Replicas &#40;Always On Availability Groups&#41;](../../database-engine/availability-groups/windows/active-secondaries-backup-on-secondary-replicas-always-on-availability-groups.md).  
  
 SECONDARY_ROLE **(** ... **)**  
 Specifies role-specific settings that will take effect if this availability replica currently owns the secondary role (that is, whenever it is a secondary replica). Within the parentheses,  specify either or both secondary-role options. If you specify both, use a comma-separated list.  
  
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
  
 For more information about calculating the read-only routing URL for an availability replica, see [Calculating read_only_routing_url for Always On](https://blogs.msdn.com/b/mattn/archive/2012/04/25/calculating-read-only-routing-url-for-Always%20On.aspx).  
  
> [!NOTE]  
>  For a named instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the Transact-SQL listener should be configured to use a specific port. For more information, see [Configure a Server to Listen on a Specific TCP Port &#40;SQL Server Configuration Manager&#41;](../../database-engine/configure-windows/configure-a-server-to-listen-on-a-specific-tcp-port.md).  
  
 PRIMARY_ROLE **(** ... **)**  
 Specifies role-specific settings that will take effect if this availability replica currently owns the primary role (that is, whenever it is the primary replica). Within the parentheses,  specify either or both primary-role options. If you specify both, use a comma-separated list.  
  
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
 Specifies the address of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that is the host for an availability replica that is a readable secondary replica when running under the secondary role.  
  
 Use a comma-separated list to specify all of the server instances that might host a readable secondary replica. Read-only routing will follow the order in which server instances are specified in the list. If you include a replica's host server instance on the replica's read-only routing list, placing this server instance at the end of the list is typically a good practice, so that read-intent connections go to a secondary replica, if one is available.  
  
 Beginning with [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)], you can load-balance read-intent requests across readable secondary replicas. You specify this by placing the replicas in a nested set of parentheses within the read-only routing list. For more information and examples, see [Configure load-balancing across read-only replicas](../../database-engine/availability-groups/windows/configure-read-only-routing-for-an-availability-group-sql-server.md#loadbalancing).  
  
 NONE  
 Specifies that when this availability replica is the primary replica, read-only routing will not be supported. This is the default behavior. When used with MODIFY REPLICA ON, this value disables an existing list, if any.  
  
 SESSION_TIMEOUT **=**_seconds_  
 Specifies the session-timeout period in seconds. If you do not specify this option, by default, the time period is 10 seconds. The minimum value is 5 seconds.  
  
> [!IMPORTANT]  
>  We recommend that you keep the time-out period at 10 seconds or greater.  
  
 For more information about the session-timeout period, see [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md).  
  
 MODIFY REPLICA ON  
 Modifies any of the replicas of the availability group. The list of replicas to be modified contains the server instance address and a WITH (...) clause for each replica.  
  
 Supported only on the primary replica.  
  
 REMOVE REPLICA ON  
 Removes the specified secondary replica from the availability group. The current primary replica cannot be removed from an availability group. On being removed, the replica stops receiving data. Its secondary databases are removed from the availability group and enter the RESTORING state.  
  
 Supported only on the primary replica.  
  
> [!NOTE]  
>  If you remove a replica while it is unavailable or failed, when it comes back online it will discover that it no longer belongs the availability group.  
  
 JOIN  
 Causes the local server instance to host a secondary replica in the specified availability group.  
  
 Supported only on a secondary replica that has not yet been joined to the availability group.  
  
 For more information, see [Join a Secondary Replica to an Availability Group &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/join-a-secondary-replica-to-an-availability-group-sql-server.md).  
  
 FAILOVER  
Initiates a manual failover of the availability group without data loss to the secondary replica to which you are connected. The replica that will host the primary replica is the *failover target*.  The failover target will take over the primary role and recover its copy of each database and bring them online as the new primary databases. The former primary replica concurrently transitions to the secondary role, and its databases become secondary databases and are immediately suspended. Potentially, these roles can be switched back and forth by a series of failures.  
  
 Supported only on a synchronous-commit secondary replica that is currently synchronized with the primary replica. Note that for a secondary replica to be synchronized the primary replica must also be running in synchronous-commit mode.  
  
> [!NOTE]  
>  A failover command returns as soon as the failover target has accepted the command. However, database recovery occurs asynchronously after the availability group has finished failing over.  
  
 For information about the limitations, prerequisites and recommendations for a performing a planned manual failover, see [Perform a Planned Manual Failover of an Availability Group &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/perform-a-planned-manual-failover-of-an-availability-group-sql-server.md).  
  
 FORCE_FAILOVER_ALLOW_DATA_LOSS  
 > [!CAUTION]  
>  Forcing failover, which might involve some data loss, is strictly a disaster recovery method. Therefore, We strongly recommend that you force failover only if the primary replica is no longer running, you are willing to risk losing data, and you must restore service to the availability group immediately.  
  
 Supported only on a replica whose role is in the SECONDARY or RESOLVING state. --The replica on which you enter a failover command is known as the *failover target*.  
  
 Forces failover of the availability group, with possible data loss, to the failover target. The failover target will take over the primary role and recover its copy of each database and bring them online as the new primary databases. On any remaining secondary replicas, every secondary database is suspended until manually resumed. When the former primary replica becomes available, it will switch to the secondary role, and its databases will become suspended secondary databases.  
  
> [!NOTE]  
>  A failover command returns as soon as the failover target has accepted the command. However, database recovery occurs asynchronously after the availability group has finished failing over.  
  
 For information about the limitations, prerequisites and recommendations for forcing failover and the effect of a forced failover on the former primary databases in the availability group, see [Perform a Forced Manual Failover of an Availability Group &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/perform-a-forced-manual-failover-of-an-availability-group-sql-server.md).  
  
 ADD LISTENER **'**_dns\_name_**'(** \<add_listener_option> **)**  
 Defines a new availability group listener for this availability group. Supported only on the primary replica.  
  
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
>  NetBIOS recognizes only the first 15 chars in the dns_name. If you have two WSFC clusters that are controlled by the same Active Directory and you try to create availability group listeners in both of clusters using names with more than 15 characters and an identical 15 character prefix, you will get an error reporting that the Virtual Network Name resource could not be brought online. For information about prefix naming rules for DNS names, see [Assigning Domain Names](https://technet.microsoft.com/library/cc731265\(WS.10\).aspx).  
  
 JOIN AVAILABILITY GROUP ON  
 Joins to a *distributed availability group*. When you create a distributed availability group, the availability group on the cluster where it is created is the primary availability group. The availability group that joins the distributed availability group is the secondary availability group.  
  
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
  
 AVAILABILITY_MODE **=** { SYNCHRONOUS_COMMIT | ASYNCHRONOUS_COMMIT }  
 Specifies whether the primary replica has to wait for the secondary availability group to acknowledge the hardening (writing) of the log records to disk before the primary replica can commit the transaction on a given primary database.  
  
 SYNCHRONOUS_COMMIT  
 Specifies that the primary replica will wait to commit transactions until they have been hardened on the secondary availability group. You can specify SYNCHRONOUS_COMMIT for up to two availability groups, including the primary availability group.  
  
 ASYNCHRONOUS_COMMIT  
 Specifies that the primary replica commits transactions without waiting for this secondary availability group to harden the log. You can specify ASYNCHRONOUS_COMMIT for up to two availability groups, including the primary availability group.  
  
 The AVAILABILITY_MODE clause is required.  
  
 FAILOVER_MODE **=** { MANUAL }  
 Specifies the failover mode of the distributed availability group.  
  
 MANUAL  
 Enables planned manual failover or forced manual failover (typically called *forced failover*) by the database administrator.  
  
 Automatic failover to the secondary availability group is not supported.  
  
 SEEDING_MODE**=** { AUTOMATIC | MANUAL }  
 Specifies how the secondary availability group will be initially seeded.  
  
 AUTOMATIC  
 Enables automatic seeding. This method will seed the secondary availability group over the network. This method does not require you to backup and restore a copy of the primary database on the replicas of the secondary availability group.  
  
 MANUAL  
 Specifies manual seeding. This method requires you to create a backup of the database on the primary replica and manually restore that backup on the replica(s) of the secondary availability group.  
  
 MODIFY AVAILABILITY GROUP ON  
 Modifies any of the availability group settings  of a distributed availability group. The list of availability groups to be modified contains the availability group name and a WITH (...) clause for each availability group.  
  
> [!IMPORTANT]  
>  This command must be repeated on both the primary availability group and secondary availability group instances.  
  
 GRANT CREATE ANY DATABASE  
 Permits the availability group to create databases on behalf of the primary replica, which supports direct seeding (SEEDING_MODE = AUTOMATIC). This parameter should be run on every secondary replica that supports direct seeding after that secondary joins the availability group.  Requires the CREATE ANY DATABASE permission.  
  
 DENY CREATE ANY DATABASE  
 Removes the ability of the availability group to create databases on behalf of the primary replica.  
  
 \<add_listener_option>  
 ADD LISTENER takes one of the following options:  
  
 WITH DHCP [ ON { **('**_four\_part\_ipv4\_address_**','**_four\_part\_ipv4\_mask_**')** } ]  
 Specifies that the availability group listener will use the Dynamic Host Configuration Protocol (DHCP).  Optionally, use the ON clause to identify the network on which this listener will be created. DHCP is limited to a single subnet that is used for every server instances that hosts an availability replica in the availability group.  
  
> [!IMPORTANT]  
>  We do not recommend DHCP in production environment. If there is a down time and the DHCP IP lease expires, extra time is required to register the new DHCP network IP address that is associated with the listener DNS name and impact the client connectivity. However, DHCP is good for setting up your development and testing environment to verify basic functions of availability groups and for integration with your applications.  
  
 For example:  
  
 `WITH DHCP ON ('10.120.19.0','255.255.254.0')`  
  
 WITH IP **(** { **('**_four\_part\_ipv4\_address_**','**_four\_part\_ipv4\_mask_**')** | **('**_ipv6\_address_**')** } [ **,** ..._n_ ] **)** [ **,** PORT **=**_listener\_port_ ]  
 Specifies that, instead of using DHCP, the availability group listener will use one or more static IP addresses. To create an availability group across multiple subnets, each subnet requires one static IP address in the listener configuration. For a given subnet, the static IP address can be either an IPv4 address or an IPv6 address. Contact your network administrator to get a static IP address for each subnet that will host an availability replica for the new availability group.  
  
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
  
 MODIFY LISTENER **'**_dns\_name_**'(** \<modify\_listener\_option\> **)**  
 Modifies an existing availability group listener for this availability group. Supported only on the primary replica.  
  
 \<modify\_listener\_option\>  
 MODIFY LISTENER takes one of the following options:  
  
 ADD IP { **('**_four\_part\_ipv4\_address_**','**_four\_part\_ipv4_mask_**')** \| <b>('</b>dns\_name*ipv6\_address*__')__ }  
 Adds the specified IP address to the availability group listener specified by *dns\_name*.  
  
 PORT **=** *listener_port*  
 See the description of this argument earlier in this section.  
  
 RESTART LISTENER **'**_dns\_name_**'**  
 Restarts the listener that is associated with the specified DNS name. Supported only on the primary replica.  
  
 REMOVE LISTENER **'**_dns\_name_**'**  
 Removes the listener that is associated with the specified DNS name. Supported only on the primary replica.  
  
 OFFLINE  
 Takes an online availability group offline. There is no data loss for synchronous-commit databases.  
  
 After an availability group goes offline, its databases become unavailable to clients, and you cannot bring the availability group back online. Therefore, use the OFFLINE option only during a cross-cluster migration of [!INCLUDE[ssHADR](../../includes/sshadr-md.md)], when migrating availability group resources to a new WSFC cluster.  
  
 For more information, see [Take an Availability Group Offline &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/take-an-availability-group-offline-sql-server.md).  
  
## Prerequisites and Restrictions  
 For information about prerequisites and restrictions on availability replicas and on their host server instances and computers, see [Prerequisites, Restrictions, and Recommendations for Always On Availability Groups &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/prereqs-restrictions-recommendations-always-on-availability.md).  
  
 For information about restrictions on the AVAILABILITY GROUP Transact-SQL statements, see [Overview of Transact-SQL Statements for Always On Availability Groups &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/transact-sql-statements-for-always-on-availability-groups.md).  
  
## Security  
  
### Permissions  
 Requires ALTER AVAILABILITY GROUP permission on the availability group, CONTROL AVAILABILITY GROUP permission, ALTER ANY AVAILABILITY GROUP permission, or CONTROL SERVER permission.  Also requires ALTER ANY DATABASE permission.   
  
## Examples  
  
###  <a name="Join_Secondary_Replica"></a> A. Joining a secondary replica to an availability group  
 The following example joins a secondary replica to which you are connected to the `AccountsAG` availability group.  
  
```SQL  
ALTER AVAILABILITY GROUP AccountsAG JOIN;  
GO  
```  
  
###  <a name="Force_Failover"></a> B. Forcing failover of an availability group  
 The following example forces the `AccountsAG` availability group to fail over to the secondary replica to which you are connected.  
  
```SQL
ALTER AVAILABILITY GROUP AccountsAG FORCE_FAILOVER_ALLOW_DATA_LOSS;  
GO  
```  
  
## See Also  
 [CREATE AVAILABILITY GROUP &#40;Transact-SQL&#41;](../../t-sql/statements/create-availability-group-transact-sql.md)   
 [ALTER DATABASE SET HADR &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql-set-hadr.md)   
 [DROP AVAILABILITY GROUP &#40;Transact-SQL&#41;](../../t-sql/statements/drop-availability-group-transact-sql.md)   
 [sys.availability_replicas &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-availability-replicas-transact-sql.md)   
 [sys.availability_groups &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-availability-groups-transact-sql.md)   
 [Troubleshoot Always On Availability Groups Configuration &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/troubleshoot-always-on-availability-groups-configuration-sql-server.md)   
 [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)   
 [Availability Group Listeners, Client Connectivity, and Application Failover &#40;SQL Server&#41;](../../database-engine/availability-groups/windows/listeners-client-connectivity-application-failover.md)  
  
  
