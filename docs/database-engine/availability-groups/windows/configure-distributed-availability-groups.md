---
title: "Configure a Distributed Availability Group"
description: "Learn how to configure a distributed availability group by using a Transact-SQL example. Also learn where to find information about distributed availability groups."
author: dimitri-furman
ms.author: dfurman
ms.reviewer: vanto
ms.date: 05/27/2025
ms.service: sql
ms.subservice: availability-groups
ms.topic: how-to
ms.custom:
  - build-2025
---
# Configure a distributed Always On availability group

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

To create a distributed availability group, you must create two availability groups each with its own listener. You then combine these availability groups into a distributed availability group. The following steps provide a basic example in Transact-SQL. This example doesn't cover all of the details of creating availability groups and listeners; instead, it focuses on highlighting the key requirements.

For a technical overview of distributed availability groups, see [Distributed availability groups](distributed-availability-groups.md).


## Prerequisites

To configure a distributed availability group, you must have the following:

- A supported version of [SQL Server](distributed-availability-groups.md#version-and-edition-requirements).

> [!NOTE]  
> If you configured the listener for your availability group on your SQL Server on Azure VM by using a distributed network name (DNN), then configuring a distributed availability group on top of your availability group isn't supported. To learn more, see [SQL Server on Azure VM feature interoperability with AG and DNN listener](/azure/azure-sql/virtual-machines/windows/availability-group-dnn-interoperability).

## Permissions

Requires **CREATE AVAILABILITY GROUP** permission on the server to create an availability group and `sysadmin` to fail over a distributed availability group. 

## Set the database mirroring endpoints to listen on all IP addresses

Make sure the database mirroring endpoints can communicate between the different availability groups in the distributed availability group. If one availability group is set to a specific network on the database mirroring endpoint, the distributed availability group doesn't work properly. On each server that hosts a replica in the distributed availability group, set the database mirroring endpoint to listen on all IP addresses (`LISTENER_IP = ALL`).

### Create a database mirroring endpoint to listen on all IP addresses

For example, the following script creates a new database mirroring endpoint on TCP port 5022 that listens on all IP addresses.

```sql
CREATE ENDPOINT [aodns-hadr]
    STATE = STARTED
    AS TCP
(
            LISTENER_PORT = 5022,
            LISTENER_IP = ALL
)
    FOR DATABASE_MIRRORING
(
            ROLE = ALL,
            AUTHENTICATION = WINDOWS NEGOTIATE,
            ENCRYPTION = REQUIRED ALGORITHM AES
);
GO
```

### Alter an existing database mirroring endpoint to listen on all IP addresses

For example, the following script changes an existing database mirroring endpoint to listen on all IP addresses.

```sql
ALTER ENDPOINT [aodns-hadr]
    AS TCP
(
            LISTENER_IP = ALL
);
GO
```

## Create first availability group

### Create the primary availability group on the first cluster

Create an availability group on the first Windows Server Failover Cluster (WSFC). In this example, the availability group is named `ag1` for the database `db1`. The primary replica of the primary availability group is known as the **global primary** in a distributed availability group. Server1 is the global primary in this example.

```sql
CREATE AVAILABILITY GROUP [ag1]
FOR DATABASE db1
REPLICA ON N'server1' WITH (ENDPOINT_URL = N'TCP://server1.contoso.com:5022',
    FAILOVER_MODE = AUTOMATIC,
    AVAILABILITY_MODE = SYNCHRONOUS_COMMIT,
    BACKUP_PRIORITY = 50,
    SECONDARY_ROLE(ALLOW_CONNECTIONS = NO),
    SEEDING_MODE = AUTOMATIC),
N'server2' WITH (ENDPOINT_URL = N'TCP://server2.contoso.com:5022',
    FAILOVER_MODE = AUTOMATIC,
    AVAILABILITY_MODE = SYNCHRONOUS_COMMIT,
    BACKUP_PRIORITY = 50,
    SECONDARY_ROLE(ALLOW_CONNECTIONS = NO),
    SEEDING_MODE = AUTOMATIC);
GO
```

> [!NOTE]  
> The preceding example uses automatic seeding, where **SEEDING_MODE** is set to **AUTOMATIC** for both the replicas and the distributed availability group. This configuration sets the secondary replicas and secondary availability group to be automatically populated without requiring a manual backup and restore of primary database.

### Join the secondary replicas to the primary availability group

Any secondary replicas must be joined to the availability group with **ALTER AVAILABILITY GROUP** with the **JOIN** option. Because automatic seeding is used in this example, you must also call **ALTER AVAILABILITY GROUP** with the **GRANT CREATE ANY DATABASE** option. This setting allows the availability group to create the database and begin seeding it automatically from the primary replica.

In this example, the following commands are run on the secondary replica, `server2`, to join the `ag1` availability group. The availability group is then permitted to create databases on the secondary.

```sql
ALTER AVAILABILITY GROUP [ag1] JOIN
ALTER AVAILABILITY GROUP [ag1] GRANT CREATE ANY DATABASE
GO
```

> [!NOTE]  
> When the availability group creates a database on a secondary replica, it sets the database owner as the account that ran the `ALTER AVAILABILITY GROUP` statement to grant permission to create any database. For complete information, see [Grant create database permission on secondary replica to availability group](automatic-seeding-secondary-replicas.md#grantCreate).

### Create a listener for the primary availability group

Next add a listener for the primary availability group on the first WSFC. In this example, the listener is named `ag1-listener`. For detailed instructions on creating a listener, see [Create or Configure an Availability Group Listener (SQL Server)](../../../database-engine/availability-groups/windows/create-or-configure-an-availability-group-listener-sql-server.md).

```sql
ALTER AVAILABILITY GROUP [ag1]
    ADD LISTENER 'ag1-listener' (
        WITH IP ( ('2001:db88:f0:f00f::cf3c'),('2001:4898:e0:f213::4ce2') ) ,
        PORT = 60173);
GO
```

## Create second availability group

Then on the second WSFC, create a second availability group, `ag2`. In this case, the database isn't specified, because it's automatically seeded from the primary availability group. The primary replica of the secondary availability group is known as the **forwarder** in a distributed availability group. In this example, server3 is the forwarder.

```sql
CREATE AVAILABILITY GROUP [ag2]
FOR
REPLICA ON N'server3' WITH (ENDPOINT_URL = N'TCP://server3.contoso.com:5022',
    FAILOVER_MODE = MANUAL,
    AVAILABILITY_MODE = SYNCHRONOUS_COMMIT,
    BACKUP_PRIORITY = 50,
    SECONDARY_ROLE(ALLOW_CONNECTIONS = NO),
    SEEDING_MODE = AUTOMATIC),
N'server4' WITH (ENDPOINT_URL = N'TCP://server4.contoso.com:5022',
    FAILOVER_MODE = MANUAL,
    AVAILABILITY_MODE = SYNCHRONOUS_COMMIT,
    BACKUP_PRIORITY = 50,
    SECONDARY_ROLE(ALLOW_CONNECTIONS = NO),
    SEEDING_MODE = AUTOMATIC);
GO
```

> [!NOTE]  
> - The secondary availability group must use the same database mirroring endpoint (in this example port 5022). Otherwise, replication will stop after a local failover.
> - The underlying availability groups should be in the same availability mode - either both availability groups should be in synchronous commit mode or both should be in asynchronous commit mode. If you're not sure which to use, then set both to asynchronous commit mode until you're ready to fail over. 

### Join the secondary replicas to the secondary availability group

In this example, the following commands are run on the secondary replica, `server4`, to join the `ag2` availability group. The availability group is then permitted to create databases on the secondary to support automatic seeding.

```sql
ALTER AVAILABILITY GROUP [ag2] JOIN
ALTER AVAILABILITY GROUP [ag2] GRANT CREATE ANY DATABASE
GO
```

### Create a listener for the secondary availability group

Next add a listener for the secondary availability group on the second WSFC. In this example, the listener is named `ag2-listener`. For detailed instructions on creating a listener, see [Create or Configure an Availability Group Listener (SQL Server)](../../../database-engine/availability-groups/windows/create-or-configure-an-availability-group-listener-sql-server.md).

```sql
ALTER AVAILABILITY GROUP [ag2]
    ADD LISTENER 'ag2-listener' ( WITH IP ( ('2001:db88:f0:f00f::cf3c'),('2001:4898:e0:f213::4ce2') ) , PORT = 60173);
GO
```

## Create distributed availability group on first cluster

On the first WSFC, create a distributed availability group (named `distributedAG` in this example). Use the **CREATE AVAILABILITY GROUP** command with the **DISTRIBUTED** option. The **AVAILABILITY GROUP ON** parameter specifies the member availability groups `ag1` and `ag2`.

# [Automatic seeding](#tab/automatic)

To create your distributed availability group using automatic seeding, use the following Transact-SQL code:

```sql
CREATE AVAILABILITY GROUP [distributedAG]
   WITH (DISTRIBUTED)
   AVAILABILITY GROUP ON
      'ag1' WITH
      (
         LISTENER_URL = 'tcp://ag1-listener.contoso.com:5022',
         AVAILABILITY_MODE = ASYNCHRONOUS_COMMIT,
         FAILOVER_MODE = MANUAL,
         SEEDING_MODE = AUTOMATIC
      ),
      'ag2' WITH
      (
         LISTENER_URL = 'tcp://ag2-listener.contoso.com:5022',
         AVAILABILITY_MODE = ASYNCHRONOUS_COMMIT,
         FAILOVER_MODE = MANUAL,
         SEEDING_MODE = AUTOMATIC
      );
GO
```

# [Manual seeding](#tab/manual)

To create your distributed availability group using manual seeding, use the following Transact-SQL code:

```sql
CREATE AVAILABILITY GROUP [distributedAG]
   WITH (DISTRIBUTED)
   AVAILABILITY GROUP ON
      'ag1' WITH
      (
         LISTENER_URL = 'tcp://ag1-listener.contoso.com:5022',
         AVAILABILITY_MODE = ASYNCHRONOUS_COMMIT,
         FAILOVER_MODE = MANUAL,
         SEEDING_MODE = MANUAL
      ),
      'ag2' WITH
      (
         LISTENER_URL = 'tcp://ag2-listener.contoso.com:5022',
         AVAILABILITY_MODE = ASYNCHRONOUS_COMMIT,
         FAILOVER_MODE = MANUAL,
         SEEDING_MODE = MANUAL
      );
GO
```

---

> [!NOTE]  
> The **LISTENER_URL** specifies the listener for each availability group along with the database mirroring endpoint of the availability group. In this example, that is port `5022` (not port `60173` used to create the listener). If you're using a load balancer, for instance in Azure, [add a load balancing rule for the distributed availability group port](/azure/azure-sql/virtual-machines/windows/availability-group-load-balancer-portal-configure#add-load-balancing-rule-for-distributed-availability-group). Add the rule for the listener port, in addition to the SQL Server instance port.

### Cancel automatic seeding to forwarder

If, for whatever reason, it becomes necessary to cancel the initialization of the forwarder _before_ the two availability groups are synchronized, ALTER the distributed availability group by setting the forwarder's SEEDING_MODE parameter to MANUAL and immediately cancel the seeding. Run the command on the global primary:

```sql
-- Cancel automatic seeding​.  Connect to global primary but specify DAG AG2
ALTER AVAILABILITY GROUP [distributedAG] ​
   MODIFY ​
   AVAILABILITY GROUP ON ​
   'ag2' WITH ​
   ( ​ SEEDING_MODE = MANUAL ​ ); ​
```

## Join distributed availability group on second cluster

Then join the distributed availability group on the second WSFC.

# [Automatic seeding](#tab/automatic)

To join your distributed availability group using automatic seeding, use the following Transact-SQL code:

```sql
ALTER AVAILABILITY GROUP [distributedAG]
   JOIN
   AVAILABILITY GROUP ON
      'ag1' WITH
      (
         LISTENER_URL = 'tcp://ag1-listener.contoso.com:5022',
         AVAILABILITY_MODE = ASYNCHRONOUS_COMMIT,
         FAILOVER_MODE = MANUAL,
         SEEDING_MODE = AUTOMATIC
      ),
      'ag2' WITH
      (
         LISTENER_URL = 'tcp://ag2-listener.contoso.com:5022',
         AVAILABILITY_MODE = ASYNCHRONOUS_COMMIT,
         FAILOVER_MODE = MANUAL,
         SEEDING_MODE = AUTOMATIC
      );
GO
```

# [Manual seeding](#tab/manual)

To join your distributed availability group using manual seeding, use the following Transact-SQL code:

```sql
ALTER AVAILABILITY GROUP [distributedAG]
   JOIN
   AVAILABILITY GROUP ON
      'ag1' WITH
      (
         LISTENER_URL = 'tcp://ag1-listener.contoso.com:5022',
         AVAILABILITY_MODE = ASYNCHRONOUS_COMMIT,
         FAILOVER_MODE = MANUAL,
         SEEDING_MODE = MANUAL
      ),
      'ag2' WITH
      (
         LISTENER_URL = 'tcp://ag2-listener.contoso.com:5022',
         AVAILABILITY_MODE = ASYNCHRONOUS_COMMIT,
         FAILOVER_MODE = MANUAL,
         SEEDING_MODE = MANUAL
      );
GO
```

If manual seeding is used to create the database on the forwarder, perform a full backup and a transaction log backup from the global primary, and restore them to the forwarder with NORECOVERY option. For example:

To back up on the global primary:

```sql
BACKUP DATABASE [db1]
    TO DISK = '<full backup location>'
    WITH FORMAT;

BACKUP LOG [db1]
    TO DISK = '<log backup location>'
    WITH FORMAT;
```

To restore to the forwarder:

```sql
RESTORE DATABASE [db1] FROM DISK = '<full backup location>'
    WITH NORECOVERY;

RESTORE LOG [db1] FROM DISK = '<log backup location>'
    WITH NORECOVERY;
```

After that, run the following on the forwarder:

```sql
ALTER DATABASE [db1]
    SET HADR AVAILABILITY GROUP = [distributedAG];
```

---

<a id="join_secondary"></a>

## Join the database on the secondary of the second availability group

If the second availability group was set up to use automatic seeding, then move to step 2.

1. If the second availability group is using manual seeding, then restore the backup you took on the global primary to the secondary of the second availability group:

   ```sql
   RESTORE DATABASE [db1] FROM DISK = '<full backup location>'
       WITH NORECOVERY;

   RESTORE LOG [db1] FROM DISK = '<log backup location>'
       WITH NORECOVERY;
   ```

1. After the database on the secondary replica of the second availability group is in a restoring state, you have to manually join it to the availability group.

   ```sql
   ALTER DATABASE [db1]
       SET HADR AVAILABILITY GROUP = [ag2];
   ```

<a id="failover"></a>

## Fail over a distributed availability group

Since [!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)] introduced distributed availability group support for the `REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT` setting, instructions to fail over a distributed availability are different for SQL Server 2022 and later versions than for SQL Server 2019 and earlier versions.

For a distributed availability group, the only supported failover type is a manual user-initiated `FORCE_FAILOVER_ALLOW_DATA_LOSS`. Therefore, to prevent data loss, you must take extra steps (described in detail in this section) to ensure data is synchronized between the two replicas before initiating the failover.

In the event of an emergency where data loss is acceptable, you can initiate a failover without ensuring data synchronization by running:

```sql
ALTER AVAILABILITY GROUP distributedAG FORCE_FAILOVER_ALLOW_DATA_LOSS;
```

You can use the same command to fail over to the forwarder, as well as fail back to the global primary.

#### [SQL Server 2022 and later](#tab/sql22)

On [!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)] and later you can configure the `REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT` setting for a distributed availability group, which is designed to guarantee no data loss when a distributed availability group fails over. If this setting is configured, follow the steps in this section to fail over your distributed availability group. If you don't want to use the `REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT` setting, then follow the instructions to fail over a distributed availability group in SQL Server 2019 and earlier.

> [!NOTE]  
> Setting `REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT` to 1 means the primary replica waits for transactions to commit on the secondary replica before they're committed on the primary replica, which can degrade performance. While limiting or stopping transactions on the global primary isn't required for the distributed availability group to synchronize in [!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)], doing so can improve performance for both user transactions and distributed availability group synchronization with `REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT` set to 1.

#### Steps to ensure there's no data loss

To ensure there's no data loss, you must first configure the distributed availability group to support no data loss by following these steps:

1. To prepare for failover, verify the global primary *and* forwarder are in `SYNCHRONOUS_COMMIT` mode. If not, set them to `SYNCHRONOUS_COMMIT` through [ALTER AVAILABILITY GROUP](../../../t-sql/statements/alter-availability-group-transact-sql.md).
1. Set the distributed availability group to synchronous commit on *both* the global primary and the forwarder.
1. Wait until the distributed availability group is synchronized.
1. On the global primary, set the distributed availability group `REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT` setting to 1 by using [ALTER AVAILABILITY GROUP](../../../t-sql/statements/alter-availability-group-transact-sql.md).
1. Verify all replicas in the local AGs and distributed availability group are healthy and the distributed availability group is **SYNCHRONIZED**.
1. On the global primary replica, set the distributed availability group role to `SECONDARY`, which makes the distributed availability group unavailable.
1. On the forwarder (the intended new primary), fail over the distributed availability group by using [ALTER AVAILABILITY GROUP](../../../t-sql/statements/alter-availability-group-transact-sql.md) with `FORCE_FAILOVER_ALLOW_DATA_LOSS`.
1. On the new secondary (the previous global primary replica), set the distributed availability group `REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT` to 0.
1. Optional: If the availability groups are across a geographical distance that causes latency, change the availability mode to `ASYNCHRONOUS_COMMIT`. This reverts the change from the first step, if necessary.

#### T-SQL example

This section provides the steps in a detailed example to fail over the distributed availability group named `distributedAG` by using Transact-SQL. The example environment has a total of 4 nodes for the distributed availability group. The global primary **N1** and **N2** host availability group `ag1` while the forwarder **N3** and **N4** host availability group `ag2`. The distributed availability group `distributedAG` pushes changes from `ag1` to `ag2`.

1. Query to verify `SYNCHRONOUS_COMMIT` on the primaries of the local availability groups that form the distributed availability group. Run the following T-SQL directly on the forwarder *and* global primary:

   ```sql
   SELECT DISTINCT ag.name AS [Availability Group],
                   ar.replica_server_name AS [Replica],
                   ar.availability_mode_desc AS [Availability Mode]
   FROM sys.availability_replicas AS ar
        INNER JOIN
        sys.availability_groups AS ag
        ON ar.group_id = ag.group_id
        INNER JOIN
        sys.dm_hadr_database_replica_states AS rs
        ON ar.group_id = rs.group_id
           AND ar.replica_id = rs.replica_id
   WHERE ag.name IN ('ag1', 'ag2')
         AND rs.is_primary_replica = 1
   ORDER BY [Availability Group];
   --if needed, to set a given replica to SYNCHRONOUS for node N1, default instance. If named, change from N1 to something like N1\SQL22

   ALTER AVAILABILITY GROUP [testag] MODIFY REPLICA ON N'N1\SQL22' WITH (AVAILABILITY_MODE = SYNCHRONOUS_COMMIT);
   ```

1. Set the distributed availability group to synchronous commit by running the following code on *both* the global primary and the forwarder:

   ```sql
    -- sets the distributed availability group to synchronous commit
    ALTER AVAILABILITY GROUP [distributedAG] MODIFY AVAILABILITY GROUP ON
    'ag1' WITH (AVAILABILITY_MODE = SYNCHRONOUS_COMMIT),
    'ag2' WITH (AVAILABILITY_MODE = SYNCHRONOUS_COMMIT);
   ```

   > [!NOTE]  
   > In a distributed availability group, the synchronization status between the two availability groups depends on the availability mode of both replicas. For synchronous commit mode, both the current primary availability group and the current secondary availability group must have `SYNCHRONOUS_COMMIT` availability mode. For this reason, you must run this script on both the global primary replica and the forwarder.

1. Wait until the status of the distributed availability group changes to `SYNCHRONIZED`. Run the following query on the global primary:

   ```sql
   -- Run this query on the Global Primary

   -- Check the results to see if synchronization_state_desc is SYNCHRONIZED
   SELECT ag.name,
          drs.database_id AS [Availability Group],
          db_name(drs.database_id) AS database_name,
          drs.synchronization_state_desc,
          drs.last_hardened_lsn
   FROM sys.dm_hadr_database_replica_states AS drs
        INNER JOIN
        sys.availability_groups AS ag
        ON drs.group_id = ag.group_id
   WHERE ag.name = 'distributedAG'
   ORDER BY [Availability Group];
   ```

   Proceed after the availability group **synchronization_state_desc** is `SYNCHRONIZED`.

1. For [!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)] and later, on the global primary, set `REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT` to 1 by using the following T-SQL:

   ```sql
   ALTER AVAILABILITY GROUP distributedAG SET (REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT = 1);
   ```

1. Verify your availability groups are healthy on all replicas by querying the global primary *and* the forwarder:

   ```sql
   SELECT ag.name AS [AG Name],
          db_name(drs.database_id) AS database_name,
          ar.replica_server_name AS [replica],
          drs.synchronization_state_desc,
          drs.last_hardened_lsn
   FROM sys.dm_hadr_database_replica_states AS drs
        INNER JOIN
        sys.availability_groups AS ag
        ON drs.group_id = ag.group_id
        INNER JOIN
        sys.availability_replicas AS ar
        ON drs.replica_id = ar.replica_id
           AND drs.replica_id = ar.replica_id
   WHERE ag.name IN ('ag1', 'ag2', 'distributedAG');
   ```

1. On the global primary, set the distributed availability group role to `SECONDARY`. **At this point, the distributed availability group is not available.** After this step completes, you can't fail back until the rest of the steps are performed.

    ```sql
    ALTER AVAILABILITY GROUP distributedAG SET (ROLE = SECONDARY);
    ```

1. Fail over from the global primary by running the following query on the forwarder to transition the availability groups and bring the distributed availability group back online:

    ```sql
    -- Run the following command on the forwarder, the SQL Server instance that hosts the primary replica of the secondary availability group.
    ALTER AVAILABILITY GROUP distributedAG FORCE_FAILOVER_ALLOW_DATA_LOSS;
    ```

   After this step:
   - The global primary transitions from `N1` to `N3`.
   - The forwarder transitions from `N3` to `N1`.
   - The distributed availability group is available.

1. On the new forwarder (previous global primary, `N1`), clear the distributed availability group property `REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT` by setting it to 0:

   ```sql
   ALTER AVAILABILITY GROUP distributedAG SET (REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT = 0);
   ```

1. *OPTIONAL*: If the availability groups are across a geographical distance that causes latency, consider changing the availability mode back to `ASYNCHRONOUS_COMMIT` on *both* the global primary and the forwarder. This reverts the change made in the first step, if it's needed.

   ```sql
    -- If applicable: sets the distributed availability group to asynchronous commit:
    ALTER AVAILABILITY GROUP distributedAG MODIFY AVAILABILITY GROUP ON
    'ag1' WITH (AVAILABILITY_MODE = ASYNCHRONOUS_COMMIT),
    'ag2' WITH (AVAILABILITY_MODE = ASYNCHRONOUS_COMMIT);
   ```

#### [SQL Server 2019 and earlier](#tab/sql219)

The instructions in this section apply if `REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT` is not set for the distributed availability group, such as versions before [!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)] as they don't support it for distributed availability groups.

#### Steps to ensure there's no data loss

To ensure there's no data loss, follow these steps:

1. Stop all transactions running on the global primary availability group database(s).
1. Verify the global primary and forwarder are in `SYNCHRONOUS_COMMIT` mode. If not, set them to `SYNCHRONOUS_COMMIT` through [ALTER AVAILABILITY GROUP](../../../t-sql/statements/alter-availability-group-transact-sql.md).
1. Set the distributed availability group to synchronous commit on the global primary and the forwarder.
1. Wait until the distributed availability group is synchronized and has the same **last_hardened_lsn** per database. Wait until the **last_hardened_lsn** per database matches between replicas.
1. On the global primary replica, set the distributed availability group role to `SECONDARY`, which makes the distributed availability group unavailable.
1. Test failover readiness by verifying the **last_hardened_lsn** values are the same on the global primary and forwarder. If they don't match, fail back to the global primary and start the process over from the first step.
1. Fail over the primary availability group by using [ALTER AVAILABILITY GROUP](../../../t-sql/statements/alter-availability-group-transact-sql.md) with `FORCE_FAILOVER_ALLOW_DATA_LOSS`.
1. Optional: If the availability groups are across a geographical distance that causes latency, change the availability mode back to `ASYNCHRONOUS_COMMIT`.

#### T-SQL example

This section provides the steps in a detailed example to fail over the distributed availability group named `distributedAG` by using Transact-SQL. The example environment has a total of 4 nodes for the distributed availability group. The global primary **N1** and **N2** host availability group `ag1` while the forwarder **N3** and **N4** host availability group `ag2`. The distributed availability group `distributedAG` pushes changes from `ag1` to `ag2`.

1. To ensure that no data is lost, stop all transactions on the global primary databases (that is, databases of the primary availability group).

1. Query to verify `SYNCHRONOUS_COMMIT` on the primaries of the local availability groups that compose the distributed availability group. Run directly on the forwarder and global primary:

   ```sql
   SELECT ag.name AS [Availability Group],
          ar.replica_server_name AS [Replica],
          ar.availability_mode_desc AS [Availability Mode]
   FROM sys.availability_replicas AS ar
        INNER JOIN
        sys.availability_groups AS ag
        ON ar.group_id = ag.group_id
        INNER JOIN
        sys.dm_hadr_database_replica_states AS rs
        ON ar.group_id = rs.group_id
           AND ar.replica_id = rs.replica_id
   WHERE ag.name IN ('ag1', 'ag2')
         AND rs.is_primary_replica = 1
   ORDER BY [Availability Group];
   ```

1. Set the distributed availability group to synchronous commit by running the following code on *both* the global primary and the forwarder:

   ```sql
    -- sets the distributed availability group to synchronous commit
    ALTER AVAILABILITY GROUP [distributedAG]
    MODIFY AVAILABILITY GROUP ON
    'ag1' WITH (AVAILABILITY_MODE = SYNCHRONOUS_COMMIT),
    'ag2' WITH (AVAILABILITY_MODE = SYNCHRONOUS_COMMIT);

    -- query to verify the commit state of the distributed availability group
    SELECT
     ag.name,
     ag.is_distributed,
     ar.replica_server_name [Local AG Names],
     ar.availability_mode_desc,
     ars.connected_state_desc,
     ars.role_desc,
     ars.operational_state_desc,
     ars.synchronization_health_desc
    FROM sys.availability_groups ag
    INNER JOIN sys.availability_replicas ar ON ag.group_id=ar.group_id
    LEFT JOIN sys.dm_hadr_availability_replica_states ars on ars.replica_id=ar.replica_id and ag.group_id = ars.group_id
    WHERE ag.is_distributed=1;
   ```

   > [!NOTE]  
   > In a distributed availability group, the synchronization status between the two availability groups depends on the availability mode of both replicas. For synchronous commit mode, both the current primary availability group, and the current secondary availability group must have `SYNCHRONOUS_COMMIT` availability mode. For this reason, you must run this script on *both* the global primary replica, and the forwarder.

1. Wait until the status of the distributed availability group changes to `SYNCHRONIZED` and all replicas have the same **last_hardened_lsn** (per database). Run the following query on both the global primary, which is the primary replica of the primary availability group, and the forwarder to check the synchronization_state_desc and **last_hardened_lsn**:

   ```sql
   -- Run this query on the global primary and the forwarder

   -- Check the results to see if synchronization_state_desc is SYNCHRONIZED, and the last_hardened_lsn is the same per database on both the global primary and forwarder

   -- If not, rerun the query on both side every 5 seconds until it is the case

   --
   SELECT ag.name AS [AG Name],
          ar.replica_server_name AS [replica],
          db_name(drs.database_id) AS database_name,
          drs.synchronization_state_desc,
          drs.last_hardened_lsn
   FROM sys.availability_groups AS ag
        INNER JOIN
        sys.availability_replicas AS ar
        ON ag.group_id = ar.group_id
        INNER JOIN
        sys.dm_hadr_database_replica_states AS drs
        ON drs.replica_id = ar.replica_id
           AND ag.group_id = drs.group_id
   WHERE ag.name IN ('ag1', 'ag2');
   ```

   Proceed after the availability group **synchronization_state_desc** is `SYNCHRONIZED`, and the **last_hardened_lsn** is the same per database on *both* the global primary and forwarder. If **synchronization_state_desc** isn't `SYNCHRONIZED` or **last_hardened_lsn** isn't the same, run the command every five seconds until it changes. To prevent data loss, don't proceed until the **synchronization_state_desc** = `SYNCHRONIZED` and **last_hardened_lsn** is the same per database.

1. On the global primary, set the distributed availability group role to `SECONDARY`:

    ```sql
    ALTER AVAILABILITY GROUP distributedAG SET (ROLE = SECONDARY);
    ```

   **At this point, the distributed availability group is not available.**

1. Test the failover readiness. Run the **SELECT** query again to check the **last_hardened_lsn** (per database) on both the global primary and the forwarder. Verify the last_hardened_lsn matches for all databases between the global primary and the forwarder.

1. Depending on the result from the previous step, your next step will vary.

   - If the **last_hardened_lsns** match from the **SELECT** query, fail over from the primary availability group to the secondary availability group. Run the **ALTER AVAILABILITY GROUP** command on the forwarder, the SQL Server that hosts the primary replica of the secondary availability group.

   - If the **last_hardened_lsns** **do not** match from the **SELECT** query after some time, fail the distributed AG back over to the original forwarder (N1) by running the failover command against the global primary. Then start the process again from the first step.

   ```sql
   -- If the last_hardened_lsns match, you can run the failover query below against the forwarder

   -- If the last_hardened_lsns do not match after some time, you can run the failover query against the original global primary
   ALTER AVAILABILITY GROUP distributedAG FORCE_FAILOVER_ALLOW_DATA_LOSS;
   ```

   After this step, the distributed availability group is available.

1. *OPTIONAL*: After a successful failover, if the availability groups are across a geographical distance that experiences latency, consider changing the availability mode back to `ASYNCHRONOUS_COMMIT` to revert the change made previously.

   ```sql
    -- If applicable: sets the distributed availability group to asynchronous commit:
    ALTER AVAILABILITY GROUP distributedAG MODIFY AVAILABILITY GROUP ON
    'ag1' WITH (AVAILABILITY_MODE = ASYNCHRONOUS_COMMIT),
    'ag2' WITH (AVAILABILITY_MODE = ASYNCHRONOUS_COMMIT);
   ```

After completing these steps, the distributed availability group fails over without any data loss.

---

## Remove a distributed availability group

The following Transact-SQL statement removes a distributed availability group named `distributedAG`:

```sql
DROP AVAILABILITY GROUP distributedAG;
```

## Create distributed availability group on failover cluster instances

You can create a distributed availability group using an availability group on a failover cluster instance (FCI). In this case, you don't need an availability group listener. Use the virtual network name (VNN) for the primary replica of the FCI instance. The following example shows a distributed availability group called SQLFCIDAG. One availability group is SQLFCIAG. SQLFCIAG has two FCI replicas. The VNN for the primary FCI replica is SQLFCIAG-1, and the VNN for the secondary FCI replica is SQLFCIAG-2. The distributed availability group also includes SQLAG-DR, for disaster recovery.

:::image type="content" source="media/configure-distributed-availability-groups/always-on-availability-group-distributed.png" alt-text="Diagram of an Always On distributed availability group.":::

The following DDL creates this distributed availability group: 

```sql
CREATE AVAILABILITY GROUP [SQLFCIDAG]
   WITH (DISTRIBUTED)
   AVAILABILITY GROUP ON
  'SQLFCIAG' WITH
      (
         LISTENER_URL = 'tcp://SQLFCIAG-1.contoso.com:5022',
         AVAILABILITY_MODE = ASYNCHRONOUS_COMMIT,
         FAILOVER_MODE = MANUAL,
         SEEDING_MODE = AUTOMATIC
      ),
  'SQLAG-DR' WITH
      (
         LISTENER_URL = 'tcp://SQLAG-DR.contoso.com:5022',
         AVAILABILITY_MODE = ASYNCHRONOUS_COMMIT,
         FAILOVER_MODE = MANUAL,
         SEEDING_MODE = AUTOMATIC
      );
```

The listener URL is the VNN of the primary FCI instance.

## Manually fail over FCI in distributed availability group

To manually fail over the FCI availability group, update the distributed availability group to reflect the change of listener URL. For example, run the following DDL on both the global primary of the distributed AG and the forwarder of the distributed AG of SQLFCIDAG:

```sql
ALTER AVAILABILITY GROUP [SQLFCIDAG]
   MODIFY AVAILABILITY GROUP ON
 'SQLFCIAG' WITH
    (
        LISTENER_URL = 'tcp://SQLFCIAG-2.contoso.com:5022'
    )
```

## Related content

- [CREATE AVAILABILITY GROUP (Transact-SQL)](../../../t-sql/statements/create-availability-group-transact-sql.md)
- [ALTER AVAILABILITY GROUP (Transact-SQL)](../../../t-sql/statements/alter-availability-group-transact-sql.md)
