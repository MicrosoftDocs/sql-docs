---
title: "sp_server_diagnostics (Transact-SQL)"
description: sp_server_diagnostics captures diagnostic data and health information about SQL Server to detect potential failures.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 04/08/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_server_diagnostics"
  - "sp_server_diagnostics_TSQL"
helpviewer_keywords:
  - "sp_server_diagnostics"
dev_langs:
  - "TSQL"
---
# sp_server_diagnostics (Transact-SQL)

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

Captures diagnostic data and health information about [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to detect potential failures. The procedure runs in repeat mode and sends results periodically. It can be invoked from either a regular connection, or a [dedicated admin connection](../../database-engine/configure-windows/diagnostic-connection-for-database-administrators.md).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_server_diagnostics [ @repeat_interval = ] 'repeat_interval'
[ ; ]
```

## Arguments

#### [ @repeat_interval = ] '*repeat_interval*'

Indicates the time interval at which the stored procedure runs repeatedly to send health information.

*@repeat_interval* is **int** with the default of `0`. The valid parameter values are `0`, or any value equal to or more than `5`. The stored procedure has to run at least 5 seconds to return complete data. The minimum value for the stored procedure to run in the repeat mode is 5 seconds.

If this parameter isn't specified, or if the specified value is `0`, the stored procedure returns data one time and then exit.

If the specified value is less than the minimum value, it raises an error and return nothing.

If the specified value is equal to or more than `5`, the stored procedure runs repeatedly to return the health state until it's manually canceled.

## Return code values

`0` (success) or `1` (failure).

## Result set

`sp_server_diagnostics` returns the following information.

| Column | Data type | Description |
| --- | --- | --- |
| `create_time` | **datetime** | Indicates the time stamp of row creation. Each row in a single rowset has the same time stamp. |
| `component_type` | **sysname** | Indicates whether the row contains information for the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance level component or for an Always On availability group:<br /><br />`instance`<br />`Always On:AvailabilityGroup` |
| `component_name` | **sysname** | Indicates the name of component or the name of the availability group:<br /><br />`system`<br />`resource`<br />`query_processing`<br />`io_subsystem`<br />`events`<br />`<name of the availability group>` |
| `state` | **int** | Indicates the health status of the component. Can be one of the following values: `0`, `1`, `2`, or `3` |
| `state_desc` | **sysname** | Describes the state column. Descriptions that correspond to the values in the state column are:<br /><br />0: `Unknown`<br />1: `clean`<br />2: `warning`<br />3: `error` |
| `data` | **varchar (max)** | Specifies data that is specific to the component. |

Here are the descriptions of the five components:

- **system**: Collects data from a system perspective on spinlocks, severe processing conditions, non-yielding tasks, page faults, and CPU usage. This information produces an overall health state recommendation.

- **resource**: Collects data from a resource perspective on physical and virtual memory, buffer pools, pages, cache, and other memory objects. This information produces an overall health state recommendation.

- **query_processing**: Collects data from a query-processing perspective on the worker threads, tasks, wait types, CPU intensive sessions, and blocking tasks. This information produces an overall health state recommendation.

- **io_subsystem**: Collects data on IO. In addition to diagnostic data, this component produces a clean healthy or warning health state only for an IO subsystem.

- **events**: Collects data and surfaces through the stored procedure on the errors and events of interest recorded by the server, including details about ring buffer exceptions, ring buffer events about memory broker, out of memory, scheduler monitor, buffer pool, spinlocks, security, and connectivity. Events always show `0` as the state.

- **\<name of the availability group\>**: Collects data for the specified availability group (if `component_type = "Always On:AvailabilityGroup"`).

## Remarks

From a failure perspective, the `system`, `resource`, and `query_processing` components are used for failure detection while the `io_subsystem` and `events` components are used for diagnostic purposes only.

The following table maps the components to their associated health states.

| Components | Clean (1) | Warning (2) | Error (3) | Unknowns (0) |
| --- | --- | --- | --- | --- |
| `system` | x | x | x | |
| `resource` | x | x | x | |
| `query_processing` | x | x | x | |
| `io_subsystem` | x | x | | |
| `events` | | | | x |

The `x` in each row represents valid health states for the component. For example, `io_subsystem` either shows as `clean` or `warning`. It doesn't show the error states.

> [!NOTE]  
> The `sp_server_diagnostics` internal procedure is implemented on a preemptive thread at high priority.

## Permissions

Requires `VIEW SERVER STATE` permission on the server.

## Examples

It's best practice to use Extended Events sessions to capture the health information and save it to a file that is located outside of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)]. Therefore, you can still access it if there's a failure.

### A. Save the output from an Extended Events session to a file

The following example saves the output from an event session to a file:

```sql
CREATE EVENT SESSION [diag]
ON SERVER
    ADD EVENT [sp_server_diagnostics_component_result] (set collect_data=1)
    ADD TARGET [asynchronous_file_target] (set filename='C:\temp\diag.xel');
GO
ALTER EVENT SESSION [diag]
    ON SERVER STATE = start;
GO
```

### B. Read the Extended Events session log

The following query reads the Extended Events session log file on [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)]:

```sql
SELECT xml_data.value('(/event/@name)[1]', 'varchar(max)') AS Name,
    xml_data.value('(/event/@package)[1]', 'varchar(max)') AS Package,
    xml_data.value('(/event/@timestamp)[1]', 'datetime') AS 'Time',
    xml_data.value('(/event/data[@name=''component_type'']/value)[1]', 'sysname') AS SYSNAME,
    xml_data.value('(/event/data[@name=''component_name'']/value)[1]', 'sysname') AS Component,
    xml_data.value('(/event/data[@name=''state'']/value)[1]', 'int') AS STATE,
    xml_data.value('(/event/data[@name=''state_desc'']/value)[1]', 'sysname') AS State_desc,
    xml_data.query('(/event/data[@name="data"]/value/*)') AS Data
FROM (
    SELECT object_name AS event,
        CONVERT(XML, event_data) AS xml_data
    FROM sys.fn_xe_file_target_read_file('C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\Log\*.xel', NULL, NULL, NULL)
) AS XEventData
ORDER BY TIME;
```

### C. Capture `sp_server_diagnostics` output to a table

The following example captures the output of `sp_server_diagnostics` to a table in a non-repeat mode:

```sql
CREATE TABLE SpServerDiagnosticsResult (
    create_time DATETIME,
    component_type SYSNAME,
    component_name SYSNAME,
    [state] INT,
    state_desc SYSNAME,
    [data] XML
);

INSERT INTO SpServerDiagnosticsResult
EXEC sp_server_diagnostics;
```

The following query reads the summary output from the example table:

```sql
SELECT create_time,
    component_name,
    state_desc
FROM SpServerDiagnosticsResult;
```

### D. Read detailed output from each component

The following example queries read some of the detailed output from each component, in the table created in the previous example.

**System:**

```sql
SELECT data.value('(/system/@systemCpuUtilization)[1]', 'bigint') AS 'System_CPU',
    data.value('(/system/@sqlCpuUtilization)[1]', 'bigint') AS 'SQL_CPU',
    data.value('(/system/@nonYieldingTasksReported)[1]', 'bigint') AS 'NonYielding_Tasks',
    data.value('(/system/@pageFaults)[1]', 'bigint') AS 'Page_Faults',
    data.value('(/system/@latchWarnings)[1]', 'bigint') AS 'Latch_Warnings',
    data.value('(/system/@BadPagesDetected)[1]', 'bigint') AS 'BadPages_Detected',
    data.value('(/system/@BadPagesFixed)[1]', 'bigint') AS 'BadPages_Fixed'
FROM SpServerDiagnosticsResult
WHERE component_name LIKE 'system'
GO
```

**Resource Monitor:**

```sql
SELECT data.value('(./Record/ResourceMonitor/Notification)[1]', 'VARCHAR(max)') AS [Notification],
    data.value('(/resource/memoryReport/entry[@description=''Working Set'']/@value)[1]', 'bigint') / 1024 AS [SQL_Mem_in_use_MB],
    data.value('(/resource/memoryReport/entry[@description=''Available Paging File'']/@value)[1]', 'bigint') / 1024 AS [Avail_Pagefile_MB],
    data.value('(/resource/memoryReport/entry[@description=''Available Physical Memory'']/@value)[1]', 'bigint') / 1024 AS [Avail_Physical_Mem_MB],
    data.value('(/resource/memoryReport/entry[@description=''Available Virtual Memory'']/@value)[1]', 'bigint') / 1024 AS [Avail_VAS_MB],
    data.value('(/resource/@lastNotification)[1]', 'varchar(100)') AS 'LastNotification',
    data.value('(/resource/@outOfMemoryExceptions)[1]', 'bigint') AS 'OOM_Exceptions'
FROM SpServerDiagnosticsResult
WHERE component_name LIKE 'resource'
GO
```

**Nonpreemptive waits:**

```sql
SELECT waits.evt.value('(@waitType)', 'varchar(100)') AS 'Wait_Type',
    waits.evt.value('(@waits)', 'bigint') AS 'Waits',
    waits.evt.value('(@averageWaitTime)', 'bigint') AS 'Avg_Wait_Time',
    waits.evt.value('(@maxWaitTime)', 'bigint') AS 'Max_Wait_Time'
FROM SpServerDiagnosticsResult
CROSS APPLY data.nodes('/queryProcessing/topWaits/nonPreemptive/byDuration/wait') AS waits(evt)
WHERE component_name LIKE 'query_processing'
GO
```

**Preemptive waits:**

```sql
SELECT waits.evt.value('(@waitType)', 'varchar(100)') AS 'Wait_Type',
    waits.evt.value('(@waits)', 'bigint') AS 'Waits',
    waits.evt.value('(@averageWaitTime)', 'bigint') AS 'Avg_Wait_Time',
    waits.evt.value('(@maxWaitTime)', 'bigint') AS 'Max_Wait_Time'
FROM SpServerDiagnosticsResult
CROSS APPLY data.nodes('/queryProcessing/topWaits/preemptive/byDuration/wait') AS waits(evt)
WHERE component_name LIKE 'query_processing'
GO
```

**CPU intensive requests:**

```sql
SELECT cpureq.evt.value('(@sessionId)', 'bigint') AS 'SessionID',
    cpureq.evt.value('(@command)', 'varchar(100)') AS 'Command',
    cpureq.evt.value('(@cpuUtilization)', 'bigint') AS 'CPU_Utilization',
    cpureq.evt.value('(@cpuTimeMs)', 'bigint') AS 'CPU_Time_ms'
FROM SpServerDiagnosticsResult
CROSS APPLY data.nodes('/queryProcessing/cpuIntensiveRequests/request') AS cpureq(evt)
WHERE component_name LIKE 'query_processing'
GO
```

**Blocked process report:**

```sql
SELECT blk.evt.query('.') AS 'Blocked_Process_Report_XML'
FROM SpServerDiagnosticsResult
CROSS APPLY data.nodes('/queryProcessing/blockingTasks/blocked-process-report') AS blk(evt)
WHERE component_name LIKE 'query_processing'
GO
```

**Input/output:**

```sql
SELECT data.value('(/ioSubsystem/@ioLatchTimeouts)[1]', 'bigint') AS 'Latch_Timeouts',
    data.value('(/ioSubsystem/@totalLongIos)[1]', 'bigint') AS 'Total_Long_IOs'
FROM SpServerDiagnosticsResult
WHERE component_name LIKE 'io_subsystem'
GO
```

**Event information:**

```sql
SELECT xevts.evt.value('(@name)', 'varchar(100)') AS 'xEvent_Name',
    xevts.evt.value('(@package)', 'varchar(100)') AS 'Package',
    xevts.evt.value('(@timestamp)', 'datetime') AS 'xEvent_Time',
    xevts.evt.query('.') AS 'Event Data'
FROM SpServerDiagnosticsResult
CROSS APPLY data.nodes('/events/session/RingBufferTarget/event') AS xevts(evt)
WHERE component_name LIKE 'events'
GO
```

## Related content

- [Failover Policy for Failover Cluster Instances](../../sql-server/failover-clusters/windows/failover-policy-for-failover-cluster-instances.md)
