---
title: "Automatically initialize Always On availability group | Microsoft Docs"
ms.custom: ""
ms.date: "11/14/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "dbe-high-availability"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 67c6a601-677a-402b-b3d1-8c65494e9e96
caps.latest.revision: 18
author: "MikeRayMSFT"
ms.author: "v-saume"
manager: "jhubbard"
---
# Automatically initialize Always On Availability group
[!INCLUDE[tsql-appliesto-ss2016-xxxx-xxxx-xxx_md](../../../includes/tsql-appliesto-ss2016-xxxx-xxxx-xxx-md.md)]


 SQL Server 2016 introduces automatic seeding of availability groups. When you create an availability group with automatic seeding SQL Server automatically creates the secondary replicas for every database in the group. With automatic seeding you no longer have to manually backup and restore secondary replicas. To enable automatic seeding, create the availability group with T-SQL.
 
## Prerequisites

Automatic seeding requires that the data and log file path is the same on every SQL Server instance participating in the availability group. 

Availability group seeding communicates over the database mirroring endpoint. Open inbound firewall rules to the mirroring endpoint port on each server.

Databases in an availability group must be in full recovery model. The database needs to have a current full backup and transaction log backup. These backup files are not used for automatic seeding, but they are required before including the database in an availability group. 
 
## Create availability group with automatic seeding

To create an availability group with automatic seeding, set `SEEDING_MODE=AUTOMATIC`. 

The following example creates an availability group on a two node windows server failover cluster. Before running the scripts, update the values for your environment.

1. Create the endpoints. Each server will need an endpoint. The following script creates an endpoint that uses TCP port 5022 for the listener. Set `<endpoint_name>` and `LISTENER_PORT` to match your environment and run the script:

    ```
    --Create the endpoint on both servers
    -- Run this script twice, once on each server. 
    CREATE ENDPOINT [<endpoint_name>] 
    STATE=STARTED
    AS TCP (LISTENER_PORT = 5022, LISTENER_IP = ALL)
    FOR DATA_MIRRORING (ROLE = ALL, AUTHENTICATION = WINDOWS NEGOTIATE, ENCRYPTION = REQUIRED ALGORITHM AES)
    GO
    ```

1. Create the availability group. The following script creates the availability group. Update the values for the group name, server names, and domain names, and run it on the primary instance of SQL Server.  

    ```
    ---Run On Primary
    CREATE AVAILABILITY GROUP [<availability_group_name>]
    FOR DATABASE db1
    REPLICA ON'<*primary_server*>'
    WITH (ENDPOINT_URL = N'TCP://<primary_server>.<fully_qualified_domain_name>:5022', 
    FAILOVER_MODE = AUTOMATIC, 
    AVAILABILITY_MODE = SYNCHRONOUS_COMMIT, 
    BACKUP_PRIORITY = 50, 
    SECONDARY_ROLE(ALLOW_CONNECTIONS = NO), 
    SEEDING_MODE = AUTOMATIC),
    N'<secondary_server>' WITH (ENDPOINT_URL = N'TCP://<secondary_server>.<fully_qualified_domain_name>:5022', 
    FAILOVER_MODE = AUTOMATIC, 
    AVAILABILITY_MODE = SYNCHRONOUS_COMMIT, 
    BACKUP_PRIORITY = 50, 
    SECONDARY_ROLE(ALLOW_CONNECTIONS = NO), 
    SEEDING_MODE = AUTOMATIC);
    GO
    ``` 

1. Join the secondary server to the availability group and grant permission to the availability group to create databases. Run the following script on the secondary instance of SQL Server: 
 
    ```
    --Run on Secondary Replica to join to the availability group
    ALTER AVAILABILITY GROUP [<availability_group_name>] JOIN
    GO  
    ALTER AVAILABILITY GROUP [<availability_group_name>] GRANT CREATE ANY DATABASE
    GO
    ```

SQL Server will automatically create the database replica on the secondary server. If the database is large it may take some time to complete synchronization of the database. If a database is in an availability group that is configured for automatic seeding, you can query `sys.dm_hadr_automatic_seeding` system view to monitor the seeding progress. The following query returns one row for every database that is in an availability group configured for automatic seeding.

```
 SELECT start_time,
       ag.name,
       db.database_name,
       current_state,
       performed_seeding,
       failure_state,
       failure_state_desc
 FROM sys.dm_hadr_automatic_seeding autos 
    JOIN sys.availability_databases_cluster db ON autos.ag_db_id = db.group_database_id
    JOIN sys.availability_groups ag ON autos.ag_id = ag.group_id
```

## Prevent automatic seeding after an availability group

To temporarily prevent the primary from seeding more databases to the secondary replica you can deny the availability group permission to create databases. Run the following query on the instance that hosts the secondary replica in order to deny the availability group permission to create replica databases.

```
ALTER AVAILABILITY GROUP [<availability_group_name>] DENY CREATE ANY DATABASE
GO
```


## Enable automatic seeding on an existing availability group

You can set automatic seeding on an existing database. The following command will change an availability group to use automatic seeding. 

```
ALTER AVAILABILITY GROUP [<availability_group_name>] 
MODIFY REPLICA ON '<primary_node>' WITH (SEEDING_MODE = AUTOMATIC)
GO
```

This will force a database to restart seeding if needed. For example, if seeding fails because of insufficient disk space on the secondary replica, the you can run `ALTER AVAILABILITY GROUP ... WITH (SEEDING_MODE=AUTOMATIC)` to restart seeding after you have added free space.

## Stop automatic seeding

To stop automatic seeding for an availability group, run the following script on the instance that hosts the primary replica:

```
ALTER AVAILABILITY GROUP [<availability_group_name>] 
    MODIFY REPLICA ON '<primary_node>'   
    WITH (SEEDING_MODE = MANUAL)
GO
```

This will cancel any replicas that are currently seeding, and prevent SQL Server from automatically initializing any replicas in this availability group. This will not stop synchronization for any replicas that are already initialized. 


## Monitor automatic seeding availability group

### Use system dynamic management views to monitor seeding

The following system views show the status of SQL Server automatic seeding.

**sys.dm_hadr_automatic_seeding** 

On the primary replica, query `sys.dm_hadr_automatic_seeding` to check the status of the automatic seeding process. The view returns one row for each seeding process. For example:

``` 
SELECT start_time, 
        completion_time
        is_source,
        current_state,
        failure_state,
        failure_state_desc
FROM sys.dm_hadr_automatic_seeding
```
 
**sys.dm_hadr_physical_seeding_stats** 

On the primary replica, query `sys.dm_hadr_physical_seeding_stats` DMV to see the physical statistics for each seeding process that is currently running. The following query returns rows when seeding is running:

```
SELECT * FROM sys.dm_hadr_physical_seeding_stats;
```

### Diagnose database initialization using automatic seeding in the error log

When you add a database to an availability group configured for automatic seeding, SQL Server performs a VDI backup over the availability group endpoint. Review the SQL Server error log for information on when the backup completed and the secondary was synchronized.

### Diagnose database level health with extended events

Automatic seeding has new extended events for tracking state change, failures, and performance statistics during initialization. 

For example, this script creates an extended events session that captures events related to automatic seeding: 

```
CREATE EVENT SESSION [AlwaysOn_autoseed] ON SERVER 
    ADD EVENT sqlserver.hadr_automatic_seeding_state_transition,
    ADD EVENT sqlserver.hadr_automatic_seeding_timeout,
    ADD EVENT sqlserver.hadr_db_manager_seeding_request_msg,
    ADD EVENT sqlserver.hadr_physical_seeding_backup_state_change,
    ADD EVENT sqlserver.hadr_physical_seeding_failure,
    ADD EVENT sqlserver.hadr_physical_seeding_forwarder_state_change,
    ADD EVENT sqlserver.hadr_physical_seeding_forwarder_target_state_change,
    ADD EVENT sqlserver.hadr_physical_seeding_progress,
    ADD EVENT sqlserver.hadr_physical_seeding_restore_state_change,
    ADD EVENT sqlserver.hadr_physical_seeding_submit_callback
    ADD TARGET package0.event_file(SET filename=N’autoseed.xel’,max_file_size=(5),max_rollover_files=(4))
WITH (MAX_MEMORY=4096 KB,EVENT_RETENTION_MODE=ALLOW_SINGLE_EVENT_LOSS,MAX_DISPATCH_LATENCY=30 SECONDS,MAX_EVENT_SIZE=0 KB,MEMORY_PARTITION_MODE=NONE,TRACK_CAUSALITY=OFF,STARTUP_STATE=ON)
GO 

ALTER EVENT SESSION AlwaysOn_autoseed ON SERVER STATE=START
GO 
```


The following table lists extended events related to automatic seeding: 

| Name | Description|
|------------ |---------------| 
|hadr_db_manager_seeding_request_msg |  Seeding request message.
|hadr_physical_seeding_backup_state_change |    Physical seeding backup side state change.
|hadr_physical_seeding_restore_state_change |Physical seeding restore side state change.
|hadr_physical_seeding_forwarder_state_change | Physical seeding forwarder side state change.
|hadr_physical_seeding_forwarder_target_state_change |  Physical seeding forwarder target side state change.
|hadr_physical_seeding_submit_callback  |Physical seeding submit callback event.
|hadr_physical_seeding_failure  |Physical seeding failure event.
|hadr_physical_seeding_progress |   Physical seeding progress event.
|hadr_physical_seeding_schedule_long_task_failure   |physical seeding schedule long task failure event.
|hadr_automatic_seeding_start   |Occurs when an automatic seeding operation is submitted.
|hadr_automatic_seeding_state_transition    |Occurs when an automatic seeding operation changes state.
|hadr_automatic_seeding_success |Occurs when an automatic seeding operation succeeds.
|hadr_automatic_seeding_failure |Occurs when an automatic seeding operation fails.
|hadr_automatic_seeding_timeout |Occurs when an automatic seeding operation times out.

### Other troubleshooting considerations

**Monitor when automatic seeding will complete**

Query `sys.dm_hadr_physical_seeding_stats` for currently running automatic seeding processes. The view returns one row for each database. For example:

```
SELECT local_database_name, 
       role_desc, 
       internal_state_desc, 
       transfer_rate_bytes_per_second, 
       transferred_size_bytes, 
       database_size_bytes, 
       start_time_utc, 
       end_time_utc, estimate_time_complete_utc, 
       total_disk_io_wait_time_ms, 
       total_network_wait_time_ms, 
       is_compression_enabled 
FROM sys.dm_hadr_physical_seeding_stats
```

**Troubleshoot why a database fails to appear in an availability group configured for automatic seeding**


When a database fails to appear as part of an availability group with automatic seeding enabled, the automatic seeding likely failed. This prevents addition of the database to the availability group on either the primary and secondary replica. Query `sys.dm_hadr_automatic_seeding` on both the primary and secondary replicas. For example, run the following query to identify failure state of automatic seeding.

```
SELECT start_time, 
       completion_time, 
       is_source, 
       current_state, 
       failure_state, 
       failure_state_desc, 
       error_code 
FROM sys.dm_hadr_automatic_seeding
```

## Automatic seeding and performance considerations

SQL Server uses a fixed number of threads for automatic seeding. On the primary instance, SQL Server uses one thread per LUN to read changes. On the secondary instance SQL Server uses one thread per LUN to initialize database.

Set trace flag 9567 on the primary replica to enable compression of the data stream during automatic seeding. This can significantly reduce the transfer time of automatic seeding, however it also will increase the CPU usage. For more information, see [Tune compression for availability group](../../../database-engine/availability-groups/windows/tune-compression-for-availability-group.md). 


## When not to use automatic seeding

In some scenarios automatic seeding may not be optimal for initializing a secondary replica. During automatic seeding, SQL Server performs a backup over the network for initialization. This process can be slow if databases are very large, or the secondary replica is remote. The transaction log for these databases cannot be truncated during the backup process, so a prolonged initialization process on a busy database can result in significant transaction log growth.
Before adding a database to an availability group with automatic seeding, evaluate the database size, load, and site distance between replicas.

## Resources

[CREATE AVAILABILITY GROUP (Transact-SQL)
-](https://msdn.microsoft.com/library/ff878399.aspx)

[AlwaysOn Availability Groups Troubleshooting and Monitoring Guide](http://technet.microsoft.com/library/dn135328.aspx)

