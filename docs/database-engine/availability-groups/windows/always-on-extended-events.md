---
title: Configure Extended Events for Availability Groups
description: Configure Extended Events for availability groups in SQL Server to capture failover, connectivity, and replica state data. Learn how to set up sessions.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 08/17/2026
ms.service: sql
ms.subservice: availability-groups
ms.topic: how-to
ms.custom:
  - ag-guide
---
# Configure Extended Events for availability groups

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

SQL Server defines Extended Events that are specific to availability groups. Monitor these Extended Events in a session to help with root-cause diagnosis when you troubleshoot an availability group. Use the following query to view the availability group Extended Events:

```sql
SELECT *
FROM sys.dm_xe_objects
WHERE name LIKE '%hadr%';
```

<a id="BKMK_alwayson_health"></a>

## The alwayson_health session

The `alwayson_health` Extended Events session is created automatically when you create the availability group, and it captures a subset of the availability group related events. This session is preconfigured as a useful and convenient tool to help you get started quickly while troubleshooting an availability group. The Create Availability Group Wizard automatically starts the session on every participating availability replica configured in the wizard.

> [!IMPORTANT]  
> If you don't create the availability group by using the **New Availability Group Wizard**, the `alwayson_health` session might not start automatically. If the session isn't started, it can't capture event data when an unexpected issue occurs. You should manually start the session and configure the session to start automatically by configuring the session properties.

To view the definition of the `alwayson_health` session:

1. In **Object Explorer**, expand **Management**, **Extended Events**, and then **Sessions**.

1. Right-click **Alwayson_health**, point to **Script Session as**, point to **CREATE To**, and then select **New Query Editor Window**.

<a id="BKMK_Debugging"></a>

## Extended Events for debugging

In addition to the Extended Events that the Alwayson_health session covers, SQL Server defines an extensive set of debug events for availability groups. To use these additional Extended Events in a session, follow these steps:

1. In **Object Explorer**, expand **Management**, **Extended Events**, and then **Sessions**.

1. Right-click **Sessions** and select **New Session**. Or, right-click **Alwayson_health** and select **Properties**.

1. In the **Select a page** pane, select **Events**.

1. In the event library, in the **Category** column, select **alwayson** and clear all other categories.

1. In the **Channel** column, select **Debug**. The event library now shows all the availability group related events that aren't already selected.

1. Highlight an event in the event library, and then select the **>** button to select it for the session.

1. When you finish with the session, select **OK** to close it. Ensure that the session is started so that it captures the events that you selected.

<a id="BKMK_availability_replica_state_change"></a>

## `availability_replica_state_change`

This event occurs when the state of an availability replica changes. Creating an availability group or joining an availability replica can trigger this event. It's useful for diagnosing failed automatic failover and can also help trace the failover steps.

### Event information

| Column | Description |
| --- | --- |
| Name | `availability_replica_state_change` |
| Category | always on |
| Channel | Operational |

### Event fields

| Name | `type_name` | Description |
| --- | --- | --- |
| `availability_group_id` | `guid` | The ID of the Availability Group. |
| `availability_group_name` | `unicode_string` | The name of the Availability Group. |
| `availability_replica_id` | `guid` | The ID of the Availability Replica. |
| `previous_state` | `availability_replica_state` | The role of the replica before the change.<br /><br />**Possible values are:**<br /><br />Primary_Normal<br /><br />Secondary_Normal<br /><br />Resolving_Pending_Failover<br /><br />Resolving_Normal<br /><br />Primary_Pending<br /><br />Not_Available |
| `current_state` | `availability_replica_state` | The role of the replica after the change.<br /><br />**Possible values are:**<br /><br />Primary_Normal<br /><br />Secondary_Normal<br /><br />Resolving_Pending_Failover<br /><br />Resolving_Normal<br /><br />Primary_Pending<br /><br />Not_Available |

### Session definition in `alwayson_health`

```sql
CREATE EVENT SESSION [alwayson_health] ON SERVER
ADD EVENT availability_replica_state_change
ADD TARGET package0.event_file (
    SET filename = N'alwayson_health.xel',
    max_file_size = (5),
    max_rollover_files = (4),
    metadatafile = N'alwayson_health.xem'
)
WITH (
    MAX_MEMORY = 4096 KB,
    EVENT_RETENTION_MODE = ALLOW_SINGLE_EVENT_LOSS,
    MAX_DISPATCH_LATENCY = 30 SECONDS,
    MAX_EVENT_SIZE = 0 KB,
    MEMORY_PARTITION_MODE = NONE,
    TRACK_CAUSALITY = OFF,
    STARTUP_STATE = ON
)
GO
```

<a id="BKMK_availability_group_lease_expired"></a>

## availability_group_lease_expired

This event occurs when the cluster and availability group have a connectivity issue and the lease expires. It indicates that connectivity between the availability group and the underlying WSFC cluster is broken. If the connectivity issue occurs on the primary replica, the event might cause an automatic failover or cause the availability group to go offline.

### Event information

| Column | Description |
| --- | --- |
| Name | `availability_group_lease_expired` |
| Category | always on |
| Channel | Operational |

### Event fields

| Name | `type_name` | Description |
| --- | --- | --- |
| `availability_group_id` | `guid` | The ID of the availability group. |
| `availability_group_name` | `unicode_string` | The name of the availability group. |

### Session definition in `alwayson_health`

```sql
CREATE EVENT SESSION [alwayson_health] ON SERVER
ADD EVENT availability_group_lease_expired
ADD TARGET package0.event_file (
    SET filename = N'alwayson_health.xel',
    max_file_size = (5),
    max_rollover_files = (4),
    metadatafile = N'alwayson_health.xem'
)
WITH (
    MAX_MEMORY = 4096 KB,
    EVENT_RETENTION_MODE = ALLOW_SINGLE_EVENT_LOSS,
    MAX_DISPATCH_LATENCY = 30 SECONDS,
    MAX_EVENT_SIZE = 0 KB,
    MEMORY_PARTITION_MODE = NONE,
    TRACK_CAUSALITY = OFF,
    STARTUP_STATE = ON
)
GO
```

<a id="BKMK_availability_replica_automatic_failover_validation"></a>

## availability_replica_automatic_failover_validation

This event occurs when automatic failover validates the readiness of an availability replica as a primary replica. It shows whether the target availability replica is ready to be the new primary replica. For example, the failover validation returns false when not all databases are synchronized or joined. This event provides a failure point during failovers. The database administrator is especially interested in this information for automatic failovers because an automatic failover is an unattended operation. The database administrator can review the event to see why an automatic failover failed.

### Event information

| Name | Description |
| --- | --- |
| Name | `availability_replica_automatic_failover_validation` |
| Category | always on |
| Channel | Analytic |

### Event fields

| Name | `type_name` | Description |
| --- | --- | --- |
| `availability_group_id` | `guid` | The ID of the availability group. |
| `availability_group_name` | `unicode_string` | The name of the availability group. |
| `availability_replica_id` | `guid` | The ID of the availability replica. |
| `forced_quorum` | `validation_result_type` | If the value is `TRUE`, the automatic failover is invalidated on this availability replica.<br /><br />TRUE<br /><br />FALSE |
| `joined_and_synchronized` | `validation_result_type` | If the value is `FALSE`, the automatic failover is invalidated on this availability replica.<br /><br />TRUE<br /><br />FALSE |
| `previous_primary_or_automatic_failover_target` | `validation_result_type` | If the value is `FALSE`, the automatic failover is invalidated on this availability replica.<br /><br />TRUE<br /><br />FALSE |

### Session definition in `alwayson_health`

```sql
CREATE EVENT SESSION [alwayson_health] ON SERVER
ADD EVENT availability_replica_automatic_failover_validation (
    WHERE (
        [forced_quorum] = (TRUE)
        OR [joined_and_synchronized] = (FALSE)
        OR [previous_primary_or_automatic_failover_target] = (TRUE)
    )
)
ADD TARGET package0.event_file (
    SET filename = N'alwayson_health.xel',
    max_file_size = (5),
    max_rollover_files = (4),
    metadatafile = N'alwayson_health.xem'
)
WITH (
    MAX_MEMORY = 4096 KB,
    EVENT_RETENTION_MODE = ALLOW_SINGLE_EVENT_LOSS,
    MAX_DISPATCH_LATENCY = 30 SECONDS,
    MAX_EVENT_SIZE = 0 KB,
    MEMORY_PARTITION_MODE = NONE,
    TRACK_CAUSALITY = OFF,
    STARTUP_STATE = ON
)
GO
```

<a id="BKMK_error_reported"></a>

## error_reported (multiple error numbers): For transport or connection issues

Each filtered event indicates that a connectivity issue occurred in the transport or database mirroring endpoint that availability group depends on.

| Column | Description |
| --- | --- |
| Name | `error_reported`<br /><br />numbers to filter: `35201`, `35202`, `35206`, `35204`, `35207`, `35217`, `9642`, `9666`, `9691`, `9692`, `9693`, `28034`, `28036`, `28080`, `28091`, `33309` |
| Category | errors |
| Channel | Admin |

### Error numbers to filter

| Error Number | Description |
| --- | --- |
| 9642 | An error occurred in a Service Broker/Database Mirroring transport connection endpoint, Error: %i, State: %i. (Near endpoint role: %S_MSG, far endpoint address: '%.\*hs') |
| 9666 | The %S_MSG endpoint is in disabled or stopped state. |
| 9691 | The %S_MSG endpoint has stopped listening for connections. |
| 9692 | The %S_MSG endpoint cannot listen on port %d because it is in use by another process. |
| 9693 | The %S_MSG endpoint cannot listen for connections due to the following error: '%.\*ls'. |
| 28034 | Connection handshake failed. The login '%.\*ls' does not have CONNECT permission on the endpoint. State %d. |
| 28036 | Connection handshake failed. The certificate used by this endpoint was not found: %S_MSG. Use DBCC CHECKDB in master database to verify the metadata integrity of the endpoints. State %d. |
| 28080 | Connection handshake failed. The %S_MSG endpoint is not configured. State %d. |
| 28091 | Starting endpoint for %S_MSG with no authentication is not supported. |
| 33309 | Cannot start cluster endpoint because the default %S_MSG endpoint configuration has not been loaded yet. |
| 35201 | A connection timeout has occurred while attempting to establish a connection to availability replica '%ls' with id \[%ls\]. Either a networking or firewall issue exists, or the endpoint address provided for the replica is not the database mirroring endpoint of the host server instance. |
| 35202 | A connection for availability group '%ls' from availability replica '%ls' with id \[%ls\] to '%ls' with id \[%ls\] has been successfully established. This is an informational message only. No user action is required. |
| 35204 | The connection between server instances '%ls' with id \[%ls\] and '%ls' with id \[%ls\] has been disabled because the database mirroring endpoint was either disabled or stopped. Restart the endpoint by using the ALTER ENDPOINT Transact-SQL statement with STATE = STARTED. |
| 35206 | A connection timeout has occurred on a previously established connection to availability replica '%ls' with id \[%ls\]. Either a networking or a firewall issue exists or the availability replica has transitioned to the resolving role. |
| 35207 | Connection attempt on availability group id '%ls' from replica id '%ls' to replica id '%ls' failed because of error %d, severity %d, state %d.<br /><br />**Note**: This error might not have a good DBA use. Check and remove later in that case. |
| 35217 | The thread pool for Always On Availability Groups was unable to start a new worker thread because there are not enough available worker threads. This may degrade Always On Availability Groups performance. Use the "max worker threads" configuration option to increase number of allowable threads.<br /><br />**Applies to**: SQL Server 2019 CU 15 and later versions. |

### Session definition in `alwayson_health`

```sql
CREATE EVENT SESSION [alwayson_health] ON SERVER
ADD EVENT sqlserver.error_reported
    (
    WHERE (
        --Connectivity Error Messages
        [error_number] = (35201)
        OR [error_number] = (35202)
        OR [error_number] = (35204)
        OR [error_number] = (35206)
        OR [error_number] = (35207)
        OR [error_number] = (35217)
        OR [error_number] = (9642)
        --OR [error_number]=(9666)
        OR [error_number] = (9691)
        OR [error_number] = (9692)
        OR [error_number] = (9693)
        OR [error_number] = (28034)
        OR [error_number] = (28036)
        OR [error_number] = (28080)
        OR [error_number] = (28091)
        OR [error_number] = (33309)
    )
)
ADD TARGET package0.event_file (
    SET filename = N'alwayson_health.xel',
        max_file_size = (5),
        max_rollover_files = (4),
        metadatafile = N'alwayson_health.xem'
)
WITH
(
    MAX_MEMORY = 4096 KB,
    EVENT_RETENTION_MODE = ALLOW_SINGLE_EVENT_LOSS,
    MAX_DISPATCH_LATENCY = 30 SECONDS,
    MAX_EVENT_SIZE = 0 KB,
    MEMORY_PARTITION_MODE = NONE,
    TRACK_CAUSALITY = OFF,
    STARTUP_STATE = ON
);
GO
```

<a id="BKMK_data_movement_suspend_resume"></a>

## data_movement_suspend_resume

This event occurs when the database movement of a database replica is suspended or resumed.

### Event information

| Column | Description |
| --- | --- |
| Name | data_movement_suspend_resume |
| Category | Always on |
| Channel | Operational |

### Event fields

| Name | `type_name` | Description |
| --- | --- | --- |
| `availability_group_id` | `guid` | The ID of the availability group. |
| `availability_group_name` | `unicode_string` | The name of the availability group, if available. |
| `availability_replica_id` | `guid` | The ID of the availability replica. |
| `database_replica_id` | `guid` | The ID of the availability database. |
| `database_replica_name` | `unicode_string` | The name of the availability database. |
| `database_id` | `uint32` | The ID of the availability database. |
| `suspend_status` | `suspend_status_type` | The suspend status values.<br /><br />SUSPEND_NULL<br /><br />RESUMED<br /><br />SUSPENDED<br /><br />SUSPENDED_INVALID |
| `suspend_source` | `suspend_source_type` | The source of the suspend or resume action. |
| `suspend_reason` | `unicode_string` | The suspend reason captured in database replica manager. |

### Session definition in `alwayson_health`

```sql
CREATE EVENT SESSION [alwayson_health] ON SERVER
ADD EVENT data_movement_suspend_resume (
    WHERE (
        [suspend_status] = (SUSPENDED)
        OR [suspend_status] = (SUSPENDED_INVALID)
        OR [suspend_status] = (SUSPEND_NULL)
    )
)
ADD TARGET package0.event_file (
    SET filename = N'alwayson_health.xel',
    max_file_size = (5),
    max_rollover_files = (4),
    metadatafile = N'alwayson_health.xem'
)
WITH (
    MAX_MEMORY = 4096 KB,
    EVENT_RETENTION_MODE = ALLOW_SINGLE_EVENT_LOSS,
    MAX_DISPATCH_LATENCY = 30 SECONDS,
    MAX_EVENT_SIZE = 0 KB,
    MEMORY_PARTITION_MODE = NONE,
    TRACK_CAUSALITY = OFF,
    STARTUP_STATE = ON
)
GO
```

<a id="BKMK_alwayson_ddl_executed"></a>

## alwayson_ddl_executed

This event occurs when an availability group data definition language (DDL) statement starts executing, including `CREATE`, `ALTER`, or `DROP`. The event primarily indicates an issue with a user action on an availability replica, or it marks the starting point of an operational action. This action is followed by a runtime issue, such as a manual failover, a forced failover, suspended data movement, or resumed data movement.

### Event information

| Column | Description |
| --- | --- |
| Name | alwayson_ddl_execution |
| Category | always on |
| Channel | Analytic |

### Event fields

| Name | type_name | Description |
| --- | --- | --- |
| `availability_group_id` | `guid` | The ID of the availability group. |
| `availability_group_name` | `unicode_string` | The name of the availability group. |
| `ddl_action` | `alwayson_ddl_action` | Indicates the type of DDL action: `CREATE`, `ALTER`, or `DROP`. |
| `ddl_phase` | `ddl_opcode` | Indicates the phase of the DDL operation: `BEGIN`, `COMMIT`, or `ROLLBACK`. |
| `Statement` | `unicode_string` | The text of the statement that was executed. |

### Session definition in `alwayson_health`

```sql
CREATE EVENT SESSION [alwayson_health] ON SERVER
ADD EVENT alwayson_ddl_executed
ADD TARGET package0.event_file (
    SET filename = N'alwayson_health.xel',
    max_file_size = (5),
    max_rollover_files = (4),
    metadatafile = N'alwayson_health.xem'
)
WITH (
    MAX_MEMORY = 4096 KB,
    EVENT_RETENTION_MODE = ALLOW_SINGLE_EVENT_LOSS,
    MAX_DISPATCH_LATENCY = 30 SECONDS,
    MAX_EVENT_SIZE = 0 KB,
    MEMORY_PARTITION_MODE = NONE,
    TRACK_CAUSALITY = OFF,
    STARTUP_STATE = ON
)
GO
```

<a id="BKMK_availability_replica_manager_state"></a>

## availability_replica_manager_state

This event occurs when the state of the availability replica manager changes. It indicates the heartbeat of the availability replica manager. When the availability replica manager isn't in a healthy state, all availability replicas in the SQL Server instance go down.

### Event information

| Column | Description |
| --- | --- |
| Name | `availability_replica_manager_state_change` |
| Category | always on |
| Channel | Operational |

### Event fields

| Name | `type_name` | Description |
| --- | --- | --- |
| `current_state` | `manager_state` | The current state of the availability replica manager.<br /><br />Online<br /><br />Offline<br /><br />WaitingForClusterCommunication |

### Session definition in `alwayson_health`

```sql
CREATE EVENT SESSION [alwayson_health] ON SERVER
ADD EVENT availability_replica_manager_state (
    WHERE ([current_state] = (OFFLINE))
)
ADD TARGET package0.event_file (
    SET filename = N'alwayson_health.xel',
    max_file_size = (5),
    max_rollover_files = (4),
    metadatafile = N'alwayson_health.xem'
)
WITH (
    MAX_MEMORY = 4096 KB,
    EVENT_RETENTION_MODE = ALLOW_SINGLE_EVENT_LOSS,
    MAX_DISPATCH_LATENCY = 30 SECONDS,
    MAX_EVENT_SIZE = 0 KB,
    MEMORY_PARTITION_MODE = NONE,
    TRACK_CAUSALITY = OFF,
    STARTUP_STATE = ON
)
GO
```

<a id="BKMK_error_reported_1480"></a>

## error_reported (1480): Database replica role change

This filtered error_reported event occurs asynchronously after an availability replica role change. It indicates which availability database fails to change its expected role during the failover process.

### Event information

| Column | Description |
| --- | --- |
| Name | `error_reported`<br /><br />`Error Number 1480: The REPLICATION_TYPE_MSG database "DATABASE_NAME" is changing roles from "OLD_ROLE" to "NEW_ROLE" due to REASON_MSG` |
| Category | errors |
| Channel | Admin |

### Session definition in `alwayson_health`

```sql
CREATE EVENT SESSION [alwayson_health] ON SERVER
ADD EVENT sqlserver.error_reported (
    WHERE (
        --database replica role change message
        OR [error_number] = (1480)
        --database replica runtime error messages
        OR [error_number] = (823)
        OR [error_number] = (824)
        OR [error_number] = (829)
    )
)
ADD TARGET package0.event_file (
    SET filename = N'alwayson_health.xel',
    max_file_size = (5),
    max_rollover_files = (4),
    metadatafile = N'alwayson_health.xem'
)
WITH (
    MAX_MEMORY = 4096 KB,
    EVENT_RETENTION_MODE = ALLOW_SINGLE_EVENT_LOSS,
    MAX_DISPATCH_LATENCY = 30 SECONDS,
    MAX_EVENT_SIZE = 0 KB,
    MEMORY_PARTITION_MODE = NONE,
    TRACK_CAUSALITY = OFF,
    STARTUP_STATE = ON
)
GO
```

## sqlserver.sp_server_diagnostics_component_result

Captures diagnostic data and health information about SQL Server to detect potential failures. The procedure runs in repeat mode and sends results periodically. This extended event session is available starting with SQL Server 2019 CU15 (15.0.4198.2).

### Event information

| Name | Description |
| --- | --- |
| Name | `sp_server_diagnostics_component_result` |
| Category | Server |
| Channel | Debug |

### Event fields

| Name | `type_name` | Description |
| --- | --- | --- |
| `component` | `uint8` | Component name. |
| `state` | `uint8` | Indicates the health status of the component. |
| `data` | `xml` | XML field containing extra information about the component. |

### Session definition in `alwayson_health`

```sql
CREATE EVENT SESSION [alwayson_health] ON SERVER
ADD EVENT sqlserver.sp_server_diagnostics_component_result (
    SET collect_data = (1)
    WHERE ([state] = (3))
)
ADD TARGET package0.event_file (
    SET filename = N'alwayson_health.xel',
        max_file_size = (5),
        max_rollover_files = (4),
        metadatafile = N'alwayson_health.xem'
)
WITH
(
    MAX_MEMORY = 4096 KB,
    EVENT_RETENTION_MODE = ALLOW_SINGLE_EVENT_LOSS,
    MAX_DISPATCH_LATENCY = 30 SECONDS,
    MAX_EVENT_SIZE = 0 KB,
    MEMORY_PARTITION_MODE = NONE,
    TRACK_CAUSALITY = OFF,
    STARTUP_STATE = ON
);
GO
```

## ucs.ucs_connection_setup

Dumps connectivity or network-related logs between primary and secondary replicas. This extended event session is available starting with SQL Server 2019 CU15 (15.0.4198.2).

### Event information

| Name | Description |
| --- | --- |
| Name | `ucs_connection_setup` |
| Category | Transport |
| Channel | Debug |

### Event fields

| Name | `type_name` | Description |
| --- | --- | --- |
| `setup_event` | `int32` | Connection setup event |
| `obj_address` | `pointer` | Connection endpoint address |
| `endpoint_type` | `int32` | Endpoint type |
| `stream_status` | `int32` | Connection stream status |
| `error_number` | `uint32` | Connection error code |
| `connection_id` | `guid` | Connection ID |
| `error_message` | `unicode_string` | Connection error message |
| `address` | `unicode_string` | Connection target address |
| `circuit_id` | `unicode_string` | Connection circuit ID |

### Session definition in `alwayson_health`

```sql
CREATE EVENT SESSION [alwayson_health] ON SERVER
ADD EVENT ucs.ucs_connection_setup
ADD TARGET package0.event_file (
    SET filename = N'alwayson_health.xel',
        max_file_size = (5),
        max_rollover_files = (4),
        metadatafile = N'alwayson_health.xem'
)
WITH
(
    MAX_MEMORY = 4096 KB,
    EVENT_RETENTION_MODE = ALLOW_SINGLE_EVENT_LOSS,
    MAX_DISPATCH_LATENCY = 30 SECONDS,
    MAX_EVENT_SIZE = 0 KB,
    MEMORY_PARTITION_MODE = NONE,
    TRACK_CAUSALITY = OFF,
    STARTUP_STATE = ON
);
GO
```

## sqlserver.hadr_trace_message

Redirects the output of some DBCC commands and HADR log information to the extended event session (similar to trace flag 3605). This extended event session is available starting with SQL Server 2019 CU15 (15.0.4198.2).

### Event information

| Name | Description |
| --- | --- |
| Name | `hadr_trace_message` |
| Category | Always on |
| Channel | Debug |

### Event fields

| Name | `type_name` | Description |
| --- | --- | --- |
| `hadr_message` | `unicode_string` | Redirects the output of some DBCC commands and HADR log information to the extended event session (similar to trace flag 3605). |

### Session definition in `alwayson_health`

```sql
CREATE EVENT SESSION [alwayson_health] ON SERVER
ADD EVENT sqlserver.hadr_trace_message
ADD TARGET package0.event_file (
    SET filename = N'alwayson_health.xel',
        max_file_size = (5),
        max_rollover_files = (4),
        metadatafile = N'alwayson_health.xem'
)
WITH
(
    MAX_MEMORY = 4096 KB,
    EVENT_RETENTION_MODE = ALLOW_SINGLE_EVENT_LOSS,
    MAX_DISPATCH_LATENCY = 30 SECONDS,
    MAX_EVENT_SIZE = 0 KB,
    MEMORY_PARTITION_MODE = NONE,
    TRACK_CAUSALITY = OFF,
    STARTUP_STATE = ON
);
GO
```

## Related content

- [View event session data](/previous-versions/sql/sql-server-2012/hh710068(v=sql.110))
