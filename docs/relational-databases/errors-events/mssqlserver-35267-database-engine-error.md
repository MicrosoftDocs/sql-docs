---
title: "MSSQLSERVER_35267"
description: "MSSQLSERVER_35267"
author: pijocoder
ms.author: jopilov
ms.reviewer: mathoma, randolphwest
ms.date: 06/12/2024
ms.service: sql
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords:
  - "35267 (Database Engine error)"
---
# MSSQLSERVER_35267

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

## Details

| Attribute | Value |
| :--- | :--- |
| Product Name | SQL Server |
| Event ID | 35267 |
| Event Source | MSSQLSERVER |
| Component | SQLEngine |
| Symbolic Name | HADR_DISCONNECTED_DB |
| Message Text | Always On Availability Groups connection with %S_MSG database terminated for %S_MSG database '%.\*ls' on the availability replica '%.\*ls' with Replica ID: {%.8x-%.4x-%.4x-%.2x%.2x-%.2x%.2x%.2x%.2x%.2x%.2x}. This is an informational message only. No user action is required. |

## Explanation

This message occurs when an availability group replica loses its connection to the remote replicas on the database mirroring endpoint.
Here are examples of how you can see this error:

```output
Always On Availability Groups connection with secondary database terminated for primary database 'ContosoDb' on the availability replica 'PRODSQL' with Replica ID: {xxxxxxxx-xxxx-xxxx-xxxxx-xxxxxxxxxxxx}. This is an informational message only. No user action is required.
```

```output
Always On Availability Groups connection with primary database terminated for secondary database 'ContosoDb' on the availability replica 'PRODSQL' with Replica ID: {xxxxxxxx-xxxx-xxxx-xxxxx-xxxxxxxxxxxx}. This is an informational message only. No user action is required.
```

As you can see the error can appear on the Primary replica indicating that it lost communication with the Secondary replica, or vice versa.

Error 35267 is typically intermittent and might resolve itself the moment the underlying cause resolves itself. For example, an intermittent network issue might resolve itself and the connection might re-establish itself.

In many cases, the remote node to which the local node is trying to connect might not even be aware of the connection failure. Therefore, you might only see this error raised on one of the replicas, not both.

Error 35267 can sometimes occur together with error 35206, which is raised when a significant period has elapsed without a successful connection (for example, more than 10 seconds).

```output
A connection timeout has occurred on a previously established connection to availability replica 'PRODSQL' with id [xxxxxxxx-xxxx-xxxx-xxxxx-xxxxxxxxxxxx].  Either a networking or a firewall issue exists or the availability replica has transitioned to the resolving role.

Always On Availability Groups connection with primary database terminated for secondary database 'ContosoHRDb' on the availability replica 'PRODSQL' with Replica ID: {xxxxxxxx-xxxx-xxxx-xxxxx-xxxxxxxxxxxx}. This is an informational message only. No user action is required.
Always On Availability Groups connection with primary database terminated for secondary database 'ContosoFinDb' on the availability replica 'PRODSQL' with Replica ID: {xxxxxxxx-xxxx-xxxx-xxxxx-xxxxxxxxxxxx}. This is an informational message only. No user action is required.
Always On Availability Groups connection with primary database terminated for secondary database 'ContosoMktngDb' on the availability replica 'PRODSQL' with Replica ID: {xxxxxxxx-xxxx-xxxx-xxxxx-xxxxxxxxxxxx}. This is an informational message only. No user action is required.
```

The AG connection termination with the remote replica can lead to various issues local replica. For example, if the AG uses SYNCHRONOUS mode and the connection is lost, the local replica might end up waiting for confirmation from the remote. As a result, transaction log isn't truncated and the transaction log to run out of space (error [MSSQLSERVER_9002](mssqlserver-9002-database-engine-error.md)) and later to become unavailable (error [MSSQLSERVER_9001](mssqlserver-9001-database-engine-error.md)). Here's an example of group of errors where this occurred. The reason for transaction log being full is 'AVAILABILITY_REPLICA', which means this replica is waiting for the remote one to acknowledge it's applied log records.

```output
Error: 9002, Severity: 17, State: 9.
The transaction log for database 'ContosoAnalyticsDb' is full due to 'AVAILABILITY_REPLICA'.
Error: 3314, Severity: 21, State: 3.
During undoing of a logged operation in database 'ContosoAnalyticsDb' (page (1:32573799) if any), an error occurred at log record ID (7672713:36228:159). Typically, the specific failure is logged previously as an error in the operating system error log. Restore the database or file from a backup, or repair the database.
State information for database 'ContosoAnalyticsDb' - Hardened Lsn: '(7672713:38265:1)'    Commit LSN: '(7672712:1683087:46)'    Commit Time: 'JuN  10 2022  5:51AM'

Always On Availability Groups connection with secondary database terminated for primary database 'ContosoAnalyticsDb' on the availability replica 'SQL2019DB' with Replica ID: {xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx}. This is an informational message only. No user action is required.

Database ContosoAnalyticsDb was shutdown due to error 3314 in routine 'XdesRMReadWrite::RollbackToLsn'. Restart for non-snapshot databases will be attempted after all connections to the database are aborted.

Error during rollback. shutting down database (location: 1).
Error: 9001, Severity: 21, State: 5.
The log for database 'ContosoAnalyticsDb' is not available. Check the operating system error log for related error messages. Resolve any errors and restart the database.

Recovery of database 'ContosoAnalyticsDb' (6) is 0% complete (approximately 60177 seconds remain). Phase 2 of 3. This is an informational message only. No user action is required.
```

## Cause

- Network connection issues can exist between the primary and secondary replicas
- SQL Server or OS issues on the primary or secondary replicas causing threads not to be able to run. Examples include:
  - SQL OS Scheduler issues (non-yielding or deadlock schedulers)
  - Low memory on the machine leading to Working set trimming of all processes on the system including SQL Server
  - Operating system issues causing processes to stop responding
- Slow I/O issues that cause intermittent long waits on the primary or secondary replica

## User action

The information below outlines the more common scenarios but isn't an exhaustive list of troubleshooting steps. The specific reasons for the occurrence of this problem can include a long list of possibilities.

### Connection issues

To check for connection issues from the SQL Server where the error is raised to the remote SQL Server, you can consider the following steps:

#### Step 1. Ensure the endpoint on the remote SQL Server is active

Run the following query to discover the endpoint

   ```sql
   SELECT
    tep.name as EndPointName,
    sp.name As CreatedBy,
    tep.type_desc,
    tep.state_desc,
    tep.port
   FROM
    sys.tcp_endpoints tep
   INNER JOIN sys.server_principals sp ON tep.principal_id = sp.principal_id
   WHERE tep.type = 4
   ```

#### Step 2. Test connectivity to the remote endpoint

Use **[Test-NetConnection](/powershell/module/nettcpip/test-netconnection)** to validate connectivity. If the Endpoint is listening and connection is successful, look for the `TcpTestSucceeded        : True`. Replace ServerName or IP_Address with remote SQL Server and the port number with that of the database mirroring endpoint.

  ```powershell
  Test-NetConnection -ComputerName <ServerName> -Port <port_number>
  Test-NetConnection -ComputerName <IP_address> -Port <port_number>
  ```

#### Step 3. Collect a network trace

Intermittent network errors are often difficult to track down unless you capture a network trace, which shows network resets (dropped packets) or similar issues. For more information, see [0300 Intermittent or Periodic Network Issue](https://github.com/microsoft/CSS_SQL_Networking_Tools/wiki/0300-Intermittent-or-Periodic-Network-Issue)

### SQL Server scheduler issues

If the SQL Server worker threads are running into scheduler problems for various reasons, then the threads that service incoming requests can stop responding temporarily while the scheduler issues last.

#### Step 4. Check for scheduler issues on SQL Server

A typical non-yielding scheduler issue is recorded in SQL Server error log after 70 seconds of non-yield state. However, SQL Server checks the state of schedulers more frequently than that and reports those intermediate non-yielding states in Extended events. If you uncover scheduler issues on the remote node that correspond to the time of error 35267, focus on resolving those first.
Here's how you can check for short-lived occurrences of scheduler issues that don't reach the 70-second threshold, but occur for say 10 or 20 seconds.

**Use the [System Health](../extended-events/use-the-system-health-session.md) extended event file**

1. Locate the [System Health](../extended-events/use-the-system-health-session.md) extended event file from the time of the event.
1. Double-click on the `system_health_0_xxxxxxxxxxxxxxxxxx.xel` to open it in SQL Server Management Studio (SSMS). Alternatively, you can use `sys.fn_xe_file_target_read_file` to view or import the file as a table for easier filtering.
1. Search for any occurrences of **scheduler_monitor_non_yielding_ring_buffer_recorded** event. If you find any, that's an indication that SQL Server detected non-yielding scheduler events and is recording them. These events are recorded earlier than the actual non-yiedling scheduler memory dumps and error log entries, which occur after 60-70 seconds of non-yielding state. In other words, you can use the **scheduler_monitor_non_yielding_ring_buffer_recorded** to detect short-lived non-yielding scheduler issues that aren't logged in the Error log but still occurred. Those could be reasons for intermittent, or short-lived lack of connectivity between AG nodes.

**Use the [Diagnostics Log](../../sql-server/failover-clusters/windows/view-and-read-failover-cluster-instance-diagnostics-log.md)**

1. Locate the [Diagnostics Log](../../sql-server/failover-clusters/windows/view-and-read-failover-cluster-instance-diagnostics-log.md) in the \Log directory from the time of the event (applicable to Windows Cluster systems). The file name format is like this `SERVERNAME_MSSQLSERVER_SQLDIAG_x_xxxxxxxxxxxxxxxxxx.xel`.
1. Double-click to open the file in SQL Server Management Studio (SSMS). Alternatively, you can use `sys.fn_xe_file_target_read_file` to view or import the file as a table for easier filtering.
1. Once opened in SSMS, locate an instance of **component_health_result** event and right-click on the following and choose **Show Column in Table**: **component**, **state_desc**
1. Then right-click on each column and choose **Filter by this value** to apply the following filters:
    - the **component_health_result** event to be the only one displayed
    - **component** field='query processing'
    - **state_desc** <> 'clean'.
1. Then double-click on the **data** column to open the XML data and look `trackingNonYieldingScheduler` value in the first row.
1. If the value is different from `0x0` that means SQL Server has detected early signs of a non-yielding scheduler and reporting it here.

   Here's an example where SQL Server has detected a non-yielding condition with a scheduler address "0x4fedb840040":

   ```output
    <queryProcessing maxWorkers="9600" workersCreated="2574" workersIdle="1883" tasksCompletedWithinInterval="175591" pendingTasks="3" ... trackingNonYieldingScheduler="0x4fedb840040">
   ```

### Operating system low memory

There could be various issues at the operating system (OS) level that trigger such intermittent lack of response. A common one is low memory. On the remote AG node where the suspected issue is occurring, do the following steps:

#### Step 5. Check for OS memory issues that lead to SQL Server memory paging to disk

1. Check the Windows System event log for any errors indicating low physical or virtual memory.
1. Check for error 17890 in the SQL Server error log or the Windows Application event log to see if low memory on the machine is leading to Working set trimming of all processes on the system including SQL Server. The error looks like this:

   ```output
   A significant part of SQL Server process memory has been paged out. This may result in a performance degradation. Duration: 0 seconds. Working set (KB): 3383250, committed (KB):    9112480, memory utilization: 37%.
   ```

   For detailed t-shooting steps, see [MSSQLSERVER_17890](mssqlserver-17890-database-engine-error.md)

#### Step 6. Configure Max Server Memory and Lock pages in memory correctly

1. Configure SQL Server Max Server Memory to a value that allows for the OS and other process use have memory available. A recommended value in to set SQL Server max server memory to no more than 75% of RAM size on the system. For more information, see [Server memory configuration options](../../database-engine/configure-windows/server-memory-server-configuration-options.md)
1. [Enable the Lock pages in memory option (Windows)](../../database-engine/configure-windows/enable-the-lock-pages-in-memory-option-windows.md) to prevent massive paging of the SQL Server buffer cache.

### Slow disk I/O

In some cases excessively slow I/O can cause the SQL Server threads to stop responding temporarily, which might cause the other AG replica to disconnect.

#### Step 7. Resolve any slow I/O issues

If you encounter errors that indicate slow I/O, troubleshoot the underlying reasons for slow I/O.

```output
SQL Server has encountered 2 occurrence(s) of I/O requests taking longer than 15 seconds to complete on file [F:\TLOG\ContosoDb.ldf] in database id 9.  The OS file handle is 0x00000000000003BC.  The offset of the latest long I/O is: 0x0000003d26f600
SQL Server has encountered 2 occurrence(s) of I/O requests taking longer than 15 seconds to complete on file [F:\DATA\t38data\ContosoDb2.mdf] in database id 7.  The OS file handle is 0x000000000000118C.  The offset of the latest long I/O is: 0x00000000012000
SQL Server has encountered 1 occurrence(s) of I/O requests taking longer than 15 seconds to complete on file [F:\DATA\t38data\ContosoDb.mdf] in database id 9.  The OS file handle is 0x000000000000134C.  The offset of the latest long I/O is: 0x00000000012000

Always On Availability Groups connection with primary database terminated for secondary database 'ContosoDb2' on the availability replica 'SQLNODE1\INSTANCE19' with Replica ID: {xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx}. This is an informational message only. No user action is required.
Always On Availability Groups connection with primary database terminated for secondary database 'ContosoDb' on the availability replica 'SQLNODE1\INSTANCE19' with Replica ID: {xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx}. This is an informational message only. No user action is required.
```

- Update all device drivers and firmware or perform other diagnostics that are associated with your I/O subsystem
- Disk access can be slowed by filter drivers, for example, an antivirus program. To increase access speed, exclude the SQL Server data files from the active virus scans
- Work with your hardware vendor and system administrator to diagnose and resolve the cause for slow I/O

For detailed instructions, see [Troubleshoot slow SQL Server performance caused by I/O issues](/troubleshoot/sql/database-engine/performance/troubleshoot-sql-io-performance) and [MSSQLSERVER_833](mssqlserver-833-database-engine-error.md).
