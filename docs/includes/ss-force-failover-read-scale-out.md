---
title: "SQL Server force failover for availability groups"
description: "Force failover for availability groups with cluster type of NONE"
author: MikeRayMSFT
ms.author: mikeray
ms.date: 02/05/2018
ms.topic: "include"
ms.custom: include file
---
Each availability group has only one primary replica. The primary replica allows reads and writes. To change which replica is primary, you can fail over. In a typical availability group, the cluster manager automates the failover process. In an availability group with cluster type NONE, the failover process is manual.

There are two ways to fail over the primary replica in an availability group with cluster type NONE:

- Manual failover without data loss
- Forced manual failover with data loss


### Manual failover without data loss

Use this method when the primary replica is available, but you need to temporarily or permanently change which instance hosts the primary replica.
To avoid potential data loss, before you issue the manual failover, ensure that the target secondary replica is up to date.

To manually fail over without data loss:

1. Make the current primary and target secondary replica `SYNCHRONOUS_COMMIT`.

   ```SQL
   ALTER AVAILABILITY GROUP [AGRScale] 
        MODIFY REPLICA ON N'<node2>' 
        WITH (AVAILABILITY_MODE = SYNCHRONOUS_COMMIT);
   ```

1. To identify that active transactions are committed to the primary replica and at least one synchronous secondary replica, run the following query:

   ```SQL
   SELECT ag.name, 
      drs.database_id, 
      drs.group_id, 
      drs.replica_id, 
      drs.synchronization_state_desc, 
      ag.sequence_number
   FROM sys.dm_hadr_database_replica_states drs, sys.availability_groups ag
   WHERE drs.group_id = ag.group_id; 
   ```

   The secondary replica is synchronized when `synchronization_state_desc` is `SYNCHRONIZED`.

1. Update `REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT` to 1.

   The following script sets `REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT` to 1 on an availability group named `ag1`. Before you run the following script, replace `ag1` with the name of your availability group:

   ```SQL
   ALTER AVAILABILITY GROUP [AGRScale] 
        SET (REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT = 1);
   ```

   This setting ensures that every active transaction is committed to the primary replica and at least one synchronous secondary replica.
   >[!NOTE]
   >This setting is not specific to failover and should be set based on the requirements of the environment.

1. Set the primary replica and the secondary replica(s) not participating in the failover offline to prepare for the role change: 

   ```SQL
   ALTER AVAILABILITY GROUP [AGRScale] OFFLINE
   ```

1. Promote the target secondary replica to primary.

   ```SQL
   ALTER AVAILABILITY GROUP AGRScale FORCE_FAILOVER_ALLOW_DATA_LOSS; 
   ```

1. Update the role of the old primary and other secondaries to `SECONDARY`, run the following command on the SQL Server instance that hosts the old primary replica:

   ```SQL
   ALTER AVAILABILITY GROUP [AGRScale] 
        SET (ROLE = SECONDARY); 
   ```

   > [!NOTE]
   > To delete an availability group, use [DROP AVAILABILITY GROUP](../t-sql/statements/drop-availability-group-transact-sql.md). For an availability group that's created with cluster type NONE or EXTERNAL, execute the command on all replicas that are part of the availability group.

1. Resume data movement, run the following command for every database in the availability group on the SQL Server instance that hosts the primary replica:

   ```SQL
   ALTER DATABASE [db1]
        SET HADR RESUME
   ```

1. Re-create any listener you created for read-scale purposes and that isn't managed by a cluster manager. If the original listener points to the old primary, drop it and re-create it to point to the new primary.

### Forced manual failover with data loss

If the primary replica is not available and can't immediately be recovered, then you need to force a failover to the secondary replica with data loss. However, if the original primary replica recovers after failover, it will assume the primary role. To avoid having each replica be in a different state, remove the original primary from the availability group after a forced failover with data loss. Once the original primary comes back online, remove the availability group from it entirely. 

To force a manual failover with data loss from primary replica N1 to secondary replica N2, follow these steps: 

1. On the secondary replica (N2), initiate the forced failover: 

    ```SQL
    ALTER AVAILABILITY GROUP [AGRScale] FORCE_FAILOVER_ALLOW_DATA_LOSS;
    ```
    
1. On the new primary replica (N2), remove the original primary (N1): 

    ```SQL
    ALTER AVAILABILITY GROUP [AGRScale]
    REMOVE REPLICA ON N'N1';
    ```
    
1. Validate that all application traffic is pointed to the listener and/or the new primary replica. 
1. If the original primary (N1) comes online, immediately take availability group AGRScale offline on the original primary (N1):

   ```SQL
   ALTER AVAILABILITY GROUP [AGRScale] OFFLINE
   ```
1. If there is data or unsynchronized changes, preserve this data via backups or other data replicating options that suit your business needs.     
1. Next, remove the availability group from the original primary (N1):

    ```SQL
    DROP AVAILABILITY GROUP [AGRScale];
    ```
1. Drop the availability group database on original primary replica (N1): 

    ```SQL
    USE [master]
    GO
    DROP DATABASE [AGDBRScale]
    GO
    ```
    
 1. (Optional) If desired, you can now add N1 back as a new secondary replica to the availability group AGRScale.
