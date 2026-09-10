---
title: Monitor Performance for Availability Groups
description: This article describes the synchronization process, shows you how to calculate some of the key metrics, and gives you the links to some of the common performance troubleshooting scenarios.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 03/27/2025
ms.service: sql
ms.subservice: availability-groups
ms.topic: how-to
ms.custom:
  - ag-guide
---
# Monitor performance for Always On availability groups

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

The performance aspect of Always On Availability Groups is crucial to maintaining the service-level agreement (SLA) for your mission-critical databases. Understanding how availability groups ship logs to secondary replicas can help you estimate the recovery time objective (RTO) and recovery point objective (RPO) of your availability implementation and identify bottlenecks in poorly performing availability groups or replicas. This article describes the synchronization process, shows you how to calculate some of the key metrics, and gives you the links to some of the common performance troubleshooting scenarios.

## Data synchronization process

To estimate the time to full synchronization and to identify the bottleneck, you need to understand the synchronization process. Performance bottleneck can be anywhere in the process, and locating the bottleneck can help you dig deeper into the underlying issues. The following figure and table illustrate the data synchronization process:

:::image type="content" source="media/monitor-performance-for-always-on-availability-groups/always-onag-datasynchronization.png" alt-text="Screenshot of Availability group data synchronization.":::

| Sequence | Step description | Comments | Useful metrics |
| --- | --- | --- | --- |
| 1 | Log generation | Log data is flushed to disk. This log must be replicated to the secondary replicas. The log records enter the send queue. | [SQL Server:Database > Log bytes flushed/sec](../../../relational-databases/performance-monitor/sql-server-databases-object.md) |
| 2 | Capture | Logs for each database are captured and sent to the corresponding partner queue (one per database-replica pair). This capture process runs continuously as long as the availability replica is connected and data movement isn't suspended for any reason, and the database-replica pair is shown to be either Synchronizing or Synchronized. If the capture process isn't able to scan and enqueue the messages fast enough, the log send queue builds up. | [SQL Server:Availability Replica > Bytes Sent to Replica/sec](../../../relational-databases/performance-monitor/sql-server-availability-replica.md), which is an aggregation of the sum of all database messages queued for that availability replica.<br /><br />[log_send_queue_size](../../../relational-databases/system-dynamic-management-objects/sys-dm-hadr-database-replica-states-transact-sql.md) (KB) and [log_bytes_send_rate](../../../relational-databases/system-dynamic-management-objects/sys-dm-hadr-database-replica-states-transact-sql.md) (KB/sec) on the primary replica. |
| 3 | Send | The messages in each database-replica queue are dequeued and sent across the wire to the respective secondary replica. | [SQL Server:Availability Replica > Bytes sent to transport/sec](../../../relational-databases/performance-monitor/sql-server-availability-replica.md) |
| 4 | Receive and cache | Each secondary replica receives and caches the message. | Performance counter [SQL Server:Availability Replica > Log Bytes Received/sec](../../../relational-databases/performance-monitor/sql-server-availability-replica.md) |
| 5 | Harden | Log is flushed on the secondary replica for hardening. After the log flush, an acknowledgment is sent back to the primary replica.<br /><br />Once the log is hardened, data loss is avoided. | Performance counter [SQL Server:Database > Log Bytes Flushed/sec](../../../relational-databases/performance-monitor/sql-server-databases-object.md)<br />Wait type [HADR_LOGCAPTURE_SYNC](../../../relational-databases/system-dynamic-management-objects/sys-dm-os-wait-stats-transact-sql.md) |
| 6 | Redo | Redo the flushed pages on the secondary replica. Pages are kept in the redo queue as they wait to be redone. | [SQL Server:Database Replica > Redone Bytes/sec](../../../relational-databases/performance-monitor/sql-server-database-replica.md)<br /><br />[redo_queue_size](../../../relational-databases/system-dynamic-management-objects/sys-dm-hadr-database-replica-states-transact-sql.md) (KB) and [redo_rate](../../../relational-databases/system-dynamic-management-objects/sys-dm-hadr-database-replica-states-transact-sql.md).<br />Wait type [REDO_SYNC](../../../relational-databases/system-dynamic-management-objects/sys-dm-os-wait-stats-transact-sql.md) |

## Flow control gates

Availability groups are designed with flow control gates on the primary replica to avoid excessive resource consumption, such as network and memory resources, on all availability replicas. These flow control gates don't affect the synchronization health state of the availability replicas, but they can affect the overall performance of your availability databases, including RPO.

After the logs have been captured on the primary replica, they are subject to two levels of flow controls. Once the message threshold of either gate is reached, log messages are no longer sent to a specific replica or for a specific database. Messages can be sent once acknowledgment messages are received for the sent messages to bring the number of sent messages below the threshold.

In addition to the flow control gates, there's another factor that can prevent the log messages from being sent. The synchronization of replicas ensures that the messages are sent and applied in the order of the log sequence numbers (LSN). Before a log message is sent, its LSN also checked against the lowest acknowledged LSN number to make sure that it's less than one of thresholds (depending on the message type). If the gap between the two LSN numbers is larger than the threshold, the messages aren't sent. Once the gap is below the threshold again, the messages are sent.

[!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)] increases the limits to the number of messages that each gate allows. Starting with the following releases, use trace flag 12310 [at startup](../../configure-windows/database-engine-service-startup-options.md) to increase the limit: [!INCLUDE [sssql19-md](../../../includes/sssql19-md.md)] CU9, [!INCLUDE [sssql17-md](../../../includes/sssql17-md.md)] CU18, and [!INCLUDE [sssql16-md](../../../includes/sssql16-md.md)] SP1 CU16. This trace flag can't be used with `DBCC TRACEON`.

The following table compares message limits:

# [New limits](#tab/new-limits)

For versions of SQL Server that enable trace flag 12310, namely [!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)], [!INCLUDE [sssql19-md](../../../includes/sssql19-md.md)] CU9, [!INCLUDE [sssql17-md](../../../includes/sssql17-md.md)] CU18, [!INCLUDE [sssql16-md](../../../includes/sssql16-md.md)] SP1 CU16, and later versions, see the following limits:

| Level | Number of gates | Number of messages | Useful metrics |
| --- | --- | --- | --- |
| Transport | 1 per availability replica | 16384 | Extended event `database_transport_flow_control_action` |
| Database | 1 per availability database | 7168 | [DBMIRROR_SEND](../../../relational-databases/system-dynamic-management-objects/sys-dm-os-wait-stats-transact-sql.md)<br /><br />Extended event `hadron_database_flow_control_action` |

# [Old limits](#tab/old-limits)

For unsupported SQL Server versions, or supported SQL Server versions (starting with SQL Server 2019 CU9, SQL Server 2017 CU18, and SQL Server 2016 SP1 CU16) that **do not** have trace flag 12310 enabled, see the following limits:

| Level | Number of gates | Number of messages | Useful metrics |
| --- | --- | --- | --- |
| Transport | 1 per availability replica | 8192 | Extended event `database_transport_flow_control_action` |
| Database | 1 per availability database | 1792 (x64)<br /><br />1600 (x86) | [DBMIRROR_SEND](../../../relational-databases/system-dynamic-management-objects/sys-dm-os-wait-stats-transact-sql.md)<br /><br />Extended event `hadron_database_flow_control_action` |

---

Two useful performance counters, [SQL Server:Availability Replica > Flow control/sec](../../../relational-databases/performance-monitor/sql-server-availability-replica.md) and [SQL Server:Availability Replica > Flow Control Time (ms/sec)](../../../relational-databases/performance-monitor/sql-server-availability-replica.md), show you, within the last second, how many times flow control was activated and how much time was spent waiting on flow control. Higher wait time on the flow control translate to higher RPO. For more information on the types of issues that can cause a high wait time on the flow control, see [Troubleshoot: Potential data loss with asynchronous-commit availability-group replicas](troubleshoot-availability-group-exceeded-rpo.md).

<a id="estimating-failover-time-rto"></a>

## Estimate failover time (RTO)

The RTO in your SLA depends on the failover time of your Always On implementation at any given time, which can be expressed in the following formula:

:::image type="content" source="media/monitor-performance-for-always-on-availability-groups/always-on-rto.png" alt-text="Screenshot of Availability groups RTO calculation.":::

> [!IMPORTANT]  
> If an availability group contains more than one availability database, then the availability database with the highest Tfailover becomes the limiting value for RTO compliance.

The failure detection time, Tdetection, is the time it takes for the system to detect the failure. This time depends on cluster-level settings and not on the individual availability replicas. Depending on the automatic failover condition that is configured, a failover can be triggered as an instant response to a critical SQL Server internal error, such as orphaned spinlocks. In this case, detection can be as fast as the [sp_server_diagnostics](../../../relational-databases/system-stored-procedures/sp-server-diagnostics-transact-sql.md) error report is sent to the Windows Server Failover Cluster (WSFC). The default interval is 1/3 of the health check time-out. A failover can also be triggered because of a time-out, such as the cluster health check time-out has expired (30 seconds by default) or the lease between the resource DLL and the SQL Server instance has expired (20 seconds by default). In this case, detection time is as long as the time-out interval. For more information, see [Flexible failover policy for automatic failover of an availability group (SQL Server)](configure-flexible-automatic-failover-policy.md?viewFallbackFrom=sql-server-2014).

The only thing that the secondary replica needs to do to become ready for a failover is for the redo to catch up to the end of log. The redo time, Tredo, is calculated using the following formula:

:::image type="content" source="media/monitor-performance-for-always-on-availability-groups/always-on-redo.png" alt-text="Screenshot of Availability groups redo time calculation.":::

where *redo_queue* is the value in [redo_queue_size](../../../relational-databases/system-dynamic-management-objects/sys-dm-hadr-database-replica-states-transact-sql.md) and *redo_rate* is the value in [redo_rate](../../../relational-databases/system-dynamic-management-objects/sys-dm-hadr-database-replica-states-transact-sql.md).

The failover overhead time, Toverhead, includes the time it takes to fail over the WSFC cluster and to bring the databases online. This time is usually short and constant.

<a id="estimating-potential-data-loss-rpo"></a>

## Estimate potential data loss (RPO)

The RPO in your SLA depends on the possible data loss of your Always On implementation at any given time. This possible data loss can be expressed in the following formula:

:::image type="content" source="media/monitor-performance-for-always-on-availability-groups/always-on-rpo.gif" alt-text="Screenshot of Availability groups RPO calculation.":::

where *log_send_queue* is the value of [log_send_queue_size](../../../relational-databases/system-dynamic-management-objects/sys-dm-hadr-database-replica-states-transact-sql.md) and *log generation rate* is the value of [SQL Server:Database > Log Bytes Flushed/sec](../../../relational-databases/performance-monitor/sql-server-databases-object.md).

> [!WARNING]  
> If an availability group contains more than one availability database, then the availability database with the highest Tdata_loss becomes the limiting value for RPO compliance.

The log send queue represents all the data that can be lost from a catastrophic failure. At first glance, it's curious that the log generation rate is used instead of the log send rate (see [log_send_rate](../../../relational-databases/system-dynamic-management-objects/sys-dm-hadr-database-replica-states-transact-sql.md)). However, remember that using the log send rate only gives you the time to synchronize, while RPO measures data loss based on how fast it's generated, not on how fast it's synchronized.

A simpler way to estimate Tdata_loss is to use [last_commit_time](../../../relational-databases/system-dynamic-management-objects/sys-dm-hadr-database-replica-states-transact-sql.md). The DMV on the primary replica reports this value for all replicas. You can calculate the difference between the value for the primary replica and the value for the secondary replica to estimate how fast the log on the secondary replica is catching up to the primary replica. As stated previously, this calculation doesn't tell you the potential data loss based on how fast the log is generated, but it should be a close approximation.

## Estimate RTO and RPO with the SSMS dashboard

In Always On Availability Groups, the RTO and RPO are calculated and displayed for the databases hosted on the secondary replicas. In the SQL Server Management Stuiod (SSMS) dashboard, on the primary replica, the RTO and RPO are grouped by the secondary replica.

To view the RTO and RPO within the dashboard, do the following steps:

1. In SQL Server Management Studio, expand the **Always On High Availability** node, right-click the name of your availability group, and select **Show Dashboard**.

1. Select **Add/Remove Columns** under the **Group by** tab. Check both **Estimated Recovery Time(seconds)** [RTO] and **Estimated Data Loss (time)** [RPO].

   :::image type="content" source="media/monitor-performance-for-always-on-availability-groups/rto-rpo-dashboard.png" alt-text="Screenshot showing the RTO RPO dashboard." lightbox="media/monitor-performance-for-always-on-availability-groups/rto-rpo-dashboard.png":::

### Calculation of secondary database RTO

The recovery time calculation determines how much time is needed to recover the *secondary database* after a failover happens. The failover time is usually short and constant. The detection time depends on cluster-level settings and not on the individual availability replicas.

For a secondary database (`DB_sec`), calculation and display of its RTO is based on its `redo_queue_size` and `redo_rate`:

:::image type="content" source="media/monitor-performance-for-always-on-availability-groups/calculate-rto.png" alt-text="Screenshot of Calculation of RTO.":::

Except corner cases, the formula to calculate a secondary database's RTO is:

:::image type="content" source="media/monitor-performance-for-always-on-availability-groups/formula-calc-second-dba-rto.png" alt-text="Screenshot of Formula to calculate RTO." lightbox="media/monitor-performance-for-always-on-availability-groups/formula-calc-second-dba-rto.png":::

### Calculation of secondary database RPO

For a secondary database (`DB_sec`), calculation and display of its RPO is based on its `is_failover_ready`, `last_commit_time`, and its correlated primary database (`DB_pri`)'s `last_commit_time` values. When the value of `DB_sec.is_failover_ready` is `1`, then data between the primary and secondaries is synchronized, and no data loss occurs upon failover. However, if this value is `0`, then there's a gap between the `last_commit_time` on the primary database and the `last_commit_time` on the secondary database.

For the primary database, the `last_commit_time` is the time when the latest transaction has been committed. For the secondary database, the `last_commit_time` is the latest commit time for the transaction from the primary database that has been successfully hardened on the secondary database as well. This number is the same for both the primary and secondary database. However, a gap between these two values is the duration in which pending transactions haven't been hardened on the secondary database, and might be lost in the event of a failover.

:::image type="content" source="media/monitor-performance-for-always-on-availability-groups/calculate-rpo.png" alt-text="Screenshot of Calculation of RPO.":::

### Performance metrics used in RTO/RPO formulas

- `redo_queue_size` (KB): The redo queue size, used in RTO, is the size of transaction logs between its `last_received_lsn` and `last_redone_lsn`. The `last_received_lsn` value is the log block ID identifying the point up to which all log blocks have been received by the secondary replica that hosts this secondary database. The value of `last_redone_lsn` is the log sequence number of the last log record that was redone on the secondary database. Based on these two values, we can find IDs of the starting log block (`last_received_lsn`) and the end log block (`last_redone_lsn`). The space between these two log blocks then can represent how many transaction log blocks aren't yet redone. This is measured in Kilobytes(KB).

- `redo_rate` (KB/sec): Used in RTO calculation, this is a cumulative value shows how much of the transaction log (KB) has been redone or replayed on the secondary database per second.

- `last_commit_time` (**datetime**): Used in RPO, this value has a different meaning between the primary and secondary database. For the primary database, `last_commit_time` is the time when the latest transaction was committed. For the secondary database, the `last_commit_time` is the latest commit for the transaction on the primary database that was successfully hardened on the secondary database as well. Since this value on the secondary should be synchronized with the same value on the primary, any gap between these two values is the estimate of data loss (RPO).

## Estimate RTO and RPO using DMVs

It's possible to query the DMVs [sys.dm_hadr_database_replica_states](../../../relational-databases/system-dynamic-management-objects/sys-dm-hadr-database-replica-states-transact-sql.md) and [sys.dm_hadr_database_replica_cluster_states](../../../relational-databases/system-dynamic-management-objects/sys-dm-hadr-database-replica-cluster-states-transact-sql.md) to estimate the RPO and RTO of a database. The below queries create stored procedures that accomplish both things.

> [!NOTE]  
> Be sure to create and run the stored procedure to estimate the RTO first, as the values it produces are necessary to run the stored procedure for estimating the RPO.

### Create a stored procedure to estimate RTO

1. On the target secondary replica, create stored procedure `proc_calculate_RTO`. If this stored procedure already exists, drop it first, and then recreate it.

   ```sql
   IF object_id(N'proc_calculate_RTO', 'p') IS NOT NULL
       DROP PROCEDURE proc_calculate_RTO;
   GO

   RAISERROR ('creating procedure proc_calculate_RTO', 0, 1)
       WITH NOWAIT;
   GO

   -- name: proc_calculate_RTO
   --
   -- description: Calculate RTO of a secondary database.
   -- 
   -- parameters: @secondary_database_name nvarchar(max): name of the secondary database.
   --
   -- security: this is a public interface object.
   --
   CREATE PROCEDURE proc_calculate_RTO
   @secondary_database_name NVARCHAR (MAX)
   AS
   BEGIN
       DECLARE @db AS sysname;
       DECLARE @is_primary_replica AS BIT;
       DECLARE @is_failover_ready AS BIT;
       DECLARE @redo_queue_size AS BIGINT;
       DECLARE @redo_rate AS BIGINT;
       DECLARE @replica_id AS UNIQUEIDENTIFIER;
       DECLARE @group_database_id AS UNIQUEIDENTIFIER;
       DECLARE @group_id AS UNIQUEIDENTIFIER;
       DECLARE @RTO AS FLOAT;
       SELECT @is_primary_replica = dbr.is_primary_replica,
              @is_failover_ready = dbcs.is_failover_ready,
              @redo_queue_size = dbr.redo_queue_size,
              @redo_rate = dbr.redo_rate,
              @replica_id = dbr.replica_id,
              @group_database_id = dbr.group_database_id,
              @group_id = dbr.group_id
       FROM sys.dm_hadr_database_replica_states AS dbr
            INNER JOIN sys.dm_hadr_database_replica_cluster_states AS dbcs
                ON dbr.replica_id = dbcs.replica_id
               AND dbr.group_database_id = dbcs.group_database_id
       WHERE dbcs.database_name = @secondary_database_name;
       IF @is_primary_replica IS NULL
          OR @is_failover_ready IS NULL
          OR @redo_queue_size IS NULL
          OR @replica_id IS NULL
          OR @group_database_id IS NULL
          OR @group_id IS NULL
           BEGIN
               PRINT 'RTO of Database ' + @secondary_database_name + ' is not available';
               RETURN;
           END
       ELSE
           IF @is_primary_replica = 1
               BEGIN
                   PRINT 'You are visiting wrong replica';
                   RETURN;
               END
       IF @redo_queue_size = 0
           SET @RTO = 0;
       ELSE
           IF @redo_rate IS NULL
              OR @redo_rate = 0
               BEGIN
                   PRINT 'RTO of Database ' + @secondary_database_name + ' is not available';
                   RETURN;
               END
           ELSE
               SET @RTO = CAST (@redo_queue_size AS FLOAT) / @redo_rate;
       PRINT 'RTO of Database ' + @secondary_database_name + ' is ' + CONVERT (VARCHAR, ceiling(@RTO));
       PRINT 'group_id of Database ' + @secondary_database_name + ' is ' + CONVERT (NVARCHAR (50), @group_id);
       PRINT 'replica_id of Database ' + @secondary_database_name + ' is ' + CONVERT (NVARCHAR (50), @replica_id);
       PRINT 'group_database_id of Database ' + @secondary_database_name + ' is ' + CONVERT (NVARCHAR (50), @group_database_id);
   END
   ```

1. Execute `proc_calculate_RTO` with the target secondary database name:

   ```sql
   EXECUTE proc_calculate_RTO @secondary_database_name = N'DB_sec';
   ```

1. The output displays the RTO value of the target secondary replica database. Save the *group_id*, *replica_id*, and *group_database_id* to use with the RPO-estimation stored procedure.

   Sample Output:

   ```output
   RTO of Database DB_sec' is 0
   group_id of Database DB4 is F176DD65-C3EE-4240-BA23-EA615F965C9B
   replica_id of Database DB4 is 405554F6-3FDC-4593-A650-2067F5FABFFD
   group_database_id of Database DB4 is 39F7942F-7B5E-42C5-977D-02E7FFA6C392
   ```

### Create a stored procedure to estimate RPO

1. On the primary replica, create stored procedure `proc_calculate_RPO`. If it already exists, drop it first, and then recreate it.

   ```sql
   IF object_id(N'proc_calculate_RPO', 'p') IS NOT NULL
       DROP PROCEDURE proc_calculate_RPO;
   GO

   RAISERROR ('creating procedure proc_calculate_RPO', 0, 1)
       WITH NOWAIT;
   GO

   -- name: proc_calculate_RPO
   --
   -- description: Calculate RPO of a secondary database.
   -- 
   -- parameters: @group_id uniqueidentifier: group_id of the secondary database.
   --             @replica_id uniqueidentifier: replica_id of the secondary database.
   --             @group_database_id uniqueidentifier: group_database_id of the secondary database.
   --
   -- security: this is a public interface object.
   --
   CREATE PROCEDURE proc_calculate_RPO
   @group_id UNIQUEIDENTIFIER, @replica_id UNIQUEIDENTIFIER, @group_database_id UNIQUEIDENTIFIER
   AS
   BEGIN
       DECLARE @db_name AS sysname;
       DECLARE @is_primary_replica AS BIT;
       DECLARE @is_failover_ready AS BIT;
       DECLARE @is_local AS BIT;
       DECLARE @last_commit_time_sec AS DATETIME;
       DECLARE @last_commit_time_pri AS DATETIME;
       DECLARE @RPO AS NVARCHAR (MAX);
       SELECT @db_name = dbcs.database_name,
              @is_failover_ready = dbcs.is_failover_ready,
              @last_commit_time_sec = dbr.last_commit_time
       FROM sys.dm_hadr_database_replica_states AS dbr
            INNER JOIN sys.dm_hadr_database_replica_cluster_states AS dbcs
                ON dbr.replica_id = dbcs.replica_id
               AND dbr.group_database_id = dbcs.group_database_id
       WHERE dbr.group_id = @group_id
             AND dbr.replica_id = @replica_id
             AND dbr.group_database_id = @group_database_id;
       SELECT @last_commit_time_pri = dbr.last_commit_time,
              @is_local = dbr.is_local
       FROM sys.dm_hadr_database_replica_states AS dbr
            INNER JOIN sys.dm_hadr_database_replica_cluster_states AS dbcs
                ON dbr.replica_id = dbcs.replica_id
               AND dbr.group_database_id = dbcs.group_database_id
       WHERE dbr.group_id = @group_id
             AND dbr.is_primary_replica = 1
             AND dbr.group_database_id = @group_database_id;
       IF @is_local IS NULL
          OR @is_failover_ready IS NULL
           BEGIN
               PRINT 'RPO of database ' + @db_name + ' is not available';
               RETURN;
           END
       IF @is_local = 0
           BEGIN
               PRINT 'You are visiting wrong replica';
               RETURN;
           END
       IF @is_failover_ready = 1
           SET @RPO = '00:00:00';
       ELSE
           IF @last_commit_time_sec IS NULL
              OR @last_commit_time_pri IS NULL
               BEGIN
                   PRINT 'RPO of database ' + @db_name + ' is not available';
                   RETURN;
               END
           ELSE
               BEGIN
                   IF DATEDIFF(ss, @last_commit_time_sec, @last_commit_time_pri) < 0
                       BEGIN
                           PRINT 'RPO of database ' + @db_name + ' is not available';
                           RETURN;
                       END
                   ELSE
                       SET @RPO = CONVERT (VARCHAR, DATEADD(ms, datediff(ss, @last_commit_time_sec, @last_commit_time_pri) * 1000, 0), 114);
               END
       PRINT 'RPO of database ' + @db_name + ' is ' + @RPO;
   END

   -- secondary database's last_commit_time -- correlated primary database's last_commit_time
   ```

1. Execute `proc_calculate_RPO` with the target secondary database's *group_id*, *replica_id*, and *group_database_id*.

   ```sql
   EXECUTE proc_calculate_RPO
       @group_id = 'F176DD65-C3EE-4240-BA23-EA615F965C9B',
       @replica_id = '405554F6-3FDC-4593-A650-2067F5FABFFD',
       @group_database_id = '39F7942F-7B5E-42C5-977D-02E7FFA6C392';
   ```

1. The output displays the RPO value of the target secondary replica database.

<a id="monitoring-for-rto-and-rpo"></a>

## Monitor for RTO and RPO

This section demonstrates how to monitor your availability groups for RTO and RPO metrics. This demonstration is similar to the GUI tutorial given in [The Always On health model, part 2: Extending the health model](/archive/blogs/sqlalwayson/the-alwayson-health-model-part-2-extending-the-health-model).

Elements of the failover time and potential data loss calculations in [Estimating failover time (RTO)](#estimating-failover-time-rto) and [Estimating potential data loss (RPO)](#estimating-potential-data-loss-rpo) are conveniently provided as performance metrics in the policy management facet **Database Replica State**. For more information, see [View Policy-Based Management facets on a SQL Server Object](../../../relational-databases/policy-based-management/view-the-policy-based-management-facets-on-a-sql-server-object.md). You can monitor these two metrics on a schedule and be alerted when the metrics exceed your RTO and RPO, respectively.

The demonstrated scripts create two system policies that are run on their respective schedules, with the following characteristics:

- An RTO policy that fails when estimated failover time exceeds 10 minutes, evaluated every 5 minutes

- An RPO policy that fails when estimated data loss exceeds 1 hour, evaluated every 30 minutes

- The two policies have identical configuration on all availability replicas

- Policies are evaluated on all servers, but only on the availability groups for which the local availability replica is the primary replica. If the local availability replica isn't the primary replica, the policies aren't evaluated.

- Policy failures are conveniently displayed in the Always On Dashboard when you view it on the primary replica.

To create the policies, follow these instructions on all server instances that participate in the availability group:

1. [Start the SQL Server Agent service](/ssms/agent/start-stop-or-pause-the-sql-server-agent-service) if it isn't already started.

1. In SQL Server Management Studio, from the **Tools** menu, select **Options**.

1. In the **SQL Server Always On** tab, select **Enable user-defined Always On policy** and select **OK**.

     This setting enables you to display properly configured custom policies in the Always On Dashboard.

1. Create a [policy-based management condition](../../../relational-databases/policy-based-management/create-a-new-policy-based-management-condition.md) using the following specifications:

    - **Name**: `RTO`
    - **Facet**: **Database Replica State**
    - **Field**: `Add(@EstimatedRecoveryTime, 60)`
    - **Operator**: **<=**
    - **Value**: `600`

     This condition fails when potential failover time exceeds 10 minutes, including a 60-second overhead for both failure detection and failover.

1. Create a second [policy-based management condition](../../../relational-databases/policy-based-management/create-a-new-policy-based-management-condition.md) using the following specifications:

    - **Name**: `RPO`
    - **Facet**: **Database Replica State**
    - **Field**: `@EstimatedDataLoss`
    - **Operator**: **<=**
    - **Value**: `3600`

    This condition fails when potential data loss exceeds 1 hour.

1. Create a third [policy-based management condition](../../../relational-databases/policy-based-management/create-a-new-policy-based-management-condition.md) using the following specifications:

    - **Name**: `IsPrimaryReplica`
    - **Facet**: **Availability Group**
    - **Field**: `@LocalReplicaRole`
    - **Operator**: **=**
    - **Value**: `Primary`

    This condition checks whether the local availability replica for a given availability group is the primary replica.

1. Create a [policy-based management policy](../../../relational-databases/policy-based-management/create-a-policy-based-management-policy.md) using the following specifications:

   - **General** page:

     - **Name**: `CustomSecondaryDatabaseRTO`
     - **Check condition**: `RTO`
     - **Against targets**: **Every DatabaseReplicaState** in **IsPrimaryReplica AvailabilityGroup**

       This setting ensures that the policy is evaluated only on availability groups for which the local availability replica is the primary replica.

       - **Evaluation mode**: **On schedule**

       - **Schedule**: **CollectorSchedule_Every_5min**

       - **Enabled**: **selected**

   - **Description** page:

     - **Category**: **Availability database warnings**

       This setting enables the policy evaluation results to be displayed in the Always On Dashboard.

       - **Description**: **The current replica has an RTO that exceeds 10 minutes, assuming an overhead of 1 minute for discovery and failover. You should investigate performance issues on the respective server instance immediately.**

       - **Text to display**: **RTO Exceeded!**

1. Create a second [policy-based management policy](../../../relational-databases/policy-based-management/create-a-policy-based-management-policy.md) using the following specifications:

   - **General** page:

     - **Name**: `CustomAvailabilityDatabaseRPO`
     - **Check condition**: `RPO`
     - **Against targets**: **Every DatabaseReplicaState** in **IsPrimaryReplica AvailabilityGroup**
     - **Evaluation mode**: **On schedule**
     - **Schedule**: **CollectorSchedule_Every_30min**
     - **Enabled**: **selected**

   - **Description** page:

     - **Category**: **Availability database warnings**

     - **Description**: **The availability database has exceeded your RPO of 1 hour. You should investigate performance issues on the availability replicas immediately.**

     - **Text to display**: **RPO Exceeded!**

When you're done, two new SQL Server Agent jobs are created, one for each of the policy evaluation schedule. These jobs should have names that begin with `syspolicy_check_schedule`.

You can view the job history to inspect evaluation results. Evaluation failures are also recorded in the Windows application log (in the Event Viewer) with the Event ID 34052. You can also configure SQL Server Agent to send alerts on the policy failures. For more information, see [Configure Alerts to Notify Policy Administrators of Policy Failures](../../../relational-databases/policy-based-management/configure-alerts-to-notify-policy-administrators-of-policy-failures.md).

<a id="BKMK_SCENARIOS"></a>

## Performance troubleshooting scenarios

The following table lists the common performance-related troubleshooting scenarios.

| Scenario | Description |
| --- | --- |
| [Troubleshoot: Availability group exceeded RTO](troubleshoot-availability-group-exceeded-rto.md) | After an automatic failover or a planned manual failover without data loss, the failover time exceeds your RTO. Or, when you estimate the failover time of a synchronous-commit secondary replica (such as an automatic failover partner), you find that it exceeds your RTO. |
| [Troubleshoot: Availability group exceeded RPO](troubleshoot-availability-group-exceeded-rpo.md) | After you perform a forced manual failover, your data loss is more than your RPO. Or, when you calculate the potential data loss of an asynchronous-commit secondary replica, you find that it exceeds your RPO. |
| [Troubleshoot: Changes on the primary replica are not reflected on the secondary replica](troubleshoot-primary-changes-not-reflected-on-secondary.md) | The client application completes an update on the primary replica successfully, but querying the secondary replica shows that the change isn't reflected. |

<a id="BKMK_XEVENTS"></a>

## Useful extended events

The following extended events are useful when troubleshooting replicas in the **Synchronizing** state.

| Event Name | Category | Channel | Availability replica |
| --- | --- | --- | --- |
| `redo_caught_up` | transactions | Debug | Secondary |
| `redo_worker_entry` | transactions | Debug | Secondary |
| `hadr_transport_dump_message` | `alwayson` | Debug | Primary |
| `hadr_worker_pool_task` | `alwayson` | Debug | Primary |
| `hadr_dump_primary_progress` | `alwayson` | Debug | Primary |
| `hadr_dump_log_progress` | `alwayson` | Debug | Primary |
| `hadr_undo_of_redo_log_scan` | `alwayson` | Analytic | Secondary |
