---
title: "MSSQLSERVER_988"
description: "MSSQLSERVER_988"
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: randolphwest
ms.date: 07/17/2025
ms.service: sql
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords:
  - "988 (Database Engine error)"
---
# MSSQLSERVER_988

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

## Details

| Attribute | Value |
| --- | --- |
| Product Name | SQL Server |
| Event ID | 988 |
| Event Source | MSSQLSERVER |
| Component | SQLEngine |
| Symbolic Name | DB_HADRON_DATABASE_NO_QUORUM |
| Message Text | Unable to access database '%.\*ls' because it lacks a quorum of nodes for high availability. Try the operation again later. |

## Symptoms

When you attempt to add a database to an Always On availability group or perform read/write operations on the primary replica, you might receive the following SQL Server error 988:

```output
Unable to access database '<DB Name>' because it lacks a quorum of nodes for high availability. (Microsoft SQL Server, Error: 988)
```

This error indicates that the database can't be accessed or added because the required number of synchronized secondary replicas isn't available to commit the transaction.

## Cause

The [REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT](../../t-sql/statements/alter-availability-group-transact-sql.md#required_synchronized_secondaries_to_commit) setting enforces that the primary replica must wait for a specified number of synchronous secondary replicas to harden each transaction before committing. If the required number of replicas isn't online, connected, and synchronized, you might encounter the following issues, which lead to blocking or failure scenarios that trigger error 988.

- The primary replica can't complete commits.
- A database being added can't complete the join process, as secondaries aren't yet participating.

## Scenarios

You might encounter this error in the following scenarios:

### Scenario 1: Add a new database

When a new database is added to the availability group, secondaries aren't yet part of the group and can't acknowledge the commit, causing a blocking condition.

### Scenario 2: Runtime commit failures

When the configured value of `REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT` is greater than the number of available healthy synchronous secondaries, the primary can't proceed with commits.

## Workaround

To work around this issue, use one of the following options:

### Option 1: Adjust the REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT setting

Lowering the value to `0` allows the primary to commit without waiting for synchronous secondaries.

> [!WARNING]  
> This option improves availability but increases the risk of data loss in failover scenarios.

#### Use SQL Server Management Studio (SSMS)

1. Navigate to the availability group name in SSMS.
1. Right-click the name and select **Properties**.
1. Set the **REQUIRED SYNCHRONIZED SECONDARIES TO COMMIT** value to `0` or an appropriate value.

#### Use T-SQL

Run the following query:

```sql
ALTER AVAILABILITY GROUP [AGNAME] SET (REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT = 0);
```

### Option 2: Pre-Seed the Secondary Replicas (for Adding Databases)

Make sure that secondaries are ready before adding a database. Then, use automatic seeding, or manually restore the database on each secondary by using the **Join only** option.

## Troubleshooting

To diagnose and resolve this issue, follow these steps:

### Step 1: Confirm the REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT setting

To verify if the `REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT` setting is enabled for your availability group (AG), use SSMS, or T-SQL. If the value is `1` or higher, proceed to [Step 2: Verify the state of synchronous secondary replicas](#step-2-verify-the-state-of-synchronous-secondary-replicas).

#### Use SSMS

1. Navigate to the availability group name in SSMS.
1. Right-click the name and select **Properties**.
1. Check the **REQUIRED SYNCHRONIZED SECONDARIES TO COMMIT** value.

#### Use T-SQL

Run the following query on the primary replica:

```sql
SELECT name AS Availability_group_name,
       required_synchronized_secondaries_to_commit,
       *
FROM sys.availability_groups;
```

> [!NOTE]  
> This query can be executed even if the 988 error starts occurring.

### Step 2: Verify the state of synchronous secondary replicas

To check if the minimum number of synchronous secondary replicas are connected, synchronized, and healthy, use SSMS, or T-SQL.

#### Use SSMS

1. Open **Availability Groups Dashboard** on the primary replica.
1. Review the state of secondary replicas.

#### Use T-SQL

Run the following query:

```sql
SELECT ag.name AS Availability_group_name,
       drcs.database_name,
       ar.replica_server_name,
       ars.role_desc,
       ars.connected_state_desc,
       ars.synchronization_health_desc,
       ars.last_connect_error_description,
       ars.last_connect_error_number,
       ars.last_connect_error_timestamp,
       ar.endpoint_url
FROM sys.dm_hadr_availability_replica_states AS ars
     INNER JOIN sys.availability_replicas AS ar
         ON ars.replica_id = ar.replica_id
     INNER JOIN sys.availability_groups AS ag
         ON ar.group_id = ag.group_id
     INNER JOIN sys.dm_hadr_database_replica_cluster_states AS drcs
         ON ar.replica_id = drcs.replica_id;
```

Make sure that the number of `CONNECTED`, `SYNCHRONIZED`, and `HEALTHY` replicas matches the `REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT` setting.

The following extended event session captures commit policy settings and synchronization state changes to diagnose why required synchronized secondaries prevent transaction commits in SQL Server Always On availability groups.

```sql
CREATE EVENT SESSION [ag_state_change] ON SERVER
ADD EVENT sqlserver.alwayson_ddl_executed
    (ACTION (sqlos.system_thread_id, sqlserver.session_id, sqlserver.sql_text)),
ADD EVENT sqlserver.hadr_db_commit_mgr_harden
    (ACTION (sqlos.system_thread_id, sqlserver.session_id, sqlserver.sql_text)),
ADD EVENT sqlserver.hadr_db_commit_mgr_set_policy
    (ACTION (sqlos.system_thread_id, sqlserver.session_id, sqlserver.sql_text)),
ADD EVENT sqlserver.hadr_db_commit_mgr_update_harden
    (ACTION (sqlos.system_thread_id, sqlserver.session_id, sqlserver.sql_text)),
ADD EVENT sqlserver.hadr_db_partner_set_policy
    (ACTION (sqlos.system_thread_id, sqlserver.session_id, sqlserver.sql_text)),
ADD EVENT sqlserver.hadr_db_partner_set_sync_state
    (ACTION (sqlos.system_thread_id, sqlserver.session_id, sqlserver.sql_text))
ADD TARGET package0.event_file
    (SET filename = N'ag_state_change')
WITH
(
        MAX_MEMORY = 4096 KB,
        EVENT_RETENTION_MODE = ALLOW_SINGLE_EVENT_LOSS,
        MAX_DISPATCH_LATENCY = 30 SECONDS,
        MAX_EVENT_SIZE = 0 KB,
        MEMORY_PARTITION_MODE = NONE,
        TRACK_CAUSALITY = OFF,
        STARTUP_STATE = OFF
);
GO
```

The **hadr_db_commit_mgr_update_harden** event could be used to identify the issue. When the issue occurs, the **MinSyncCommitFailure** status means there aren't enough synchronization secondaries to meet the configured minimum synchronization count.
