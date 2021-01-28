---
title: "SQL Server force failover for availability groups"
description: "Force failover for availability groups with cluster type of NONE"
services: ""
author: MikeRayMSFT
ms.topic: "include"
ms.date: 02/05/2018
ms.author: mikeray
ms.custom: "include file"
---
Each availability group has only one primary replica. The primary replica allows reads and writes. To change which replica is primary, you can fail over. In an availability group for high availability, the cluster manager automates the failover process. In an availability group with cluster type NONE, the failover process is manual.

There are two ways to fail over the primary replica in an availability group with cluster type NONE:

- Forced manual failover with data loss
- Manual failover without data loss

### Forced manual failover with data loss
<!-- 
Use this method when the primary replica isn't available and can't be immediately recovered.

To force failover with data loss, connect to the SQL Server instance that hosts the target secondary replica and then run the following command:

```SQL
ALTER AVAILABILITY GROUP [ag1] FORCE_FAILOVER_ALLOW_DATA_LOSS;
``` -->

When the previous primary replica recovers, it will also assume the primary role. In the process of coming online and assuming the primary role, it will have updated the configuration information of the Availability Group.  To avoid having each replica being in a different state, we must drop and re-add the original primary and its database(es).  Failure to do this can lead to data loss.
<!-- To ensure that the previous primary replica transitions into a secondary role run the following command on the previous primary replica.

```SQL

ALTER AVAILABILITY GROUP [ag1]  SET (ROLE = SECONDARY);
``` -->

>[!NOTE]If the original primary replica is brought back online, the recommended procedure would be:

```SQL

-- step 1 – On the secondary replica (N2 in this case) execute the following to initiate force failover
ALTER AVAILABILITY GROUP [AGRScale] FORCE_FAILOVER_ALLOW_DATA_LOSS;

-- Assume that the original primary replica (N1) recovers from hardware issues
-- step 2 
-- Make appropriate changes such that application doesn’t make modification to AG DB on this replica
-- On the original primary (N1) execute the following to set the AG offline. This will help prevent accidental changes / write occurring to this replica
alter availability group [AGRScale] offline

-- step 3 -- on the new primary replica (N2), remove the original primary replica
ALTER AVAILABILITY GROUP [AGRScale]
REMOVE REPLICA ON N'N1';

-- step 4 -- drop the availability group on the original primary replica (N1)
DROP AVAILABILITY GROUP [AGRScale];

-- step 5 – drop the AG database on original primary replica (N1). Please make sure to take backup of this database if it has un-synched changes
USE [master]
GO
DROP DATABASE [AGDBRScale]
GO

-- step 6 -- add the original primary replica back to Availability group

```

### Manual failover without data loss

Use this method when the primary replica is available, but you need to temporarily or permanently change the configuration and change the SQL Server instance that hosts the primary replica. 
To avoid potential data loss, before you issue the manual failover, ensure that the target secondary replica is up to date. 

To manually fail over without data loss:

1. Make the current primary and target secondary replica `SYNCHRONOUS_COMMIT`.

   ```SQL
   ALTER AVAILABILITY GROUP [ag1] 
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
   ALTER AVAILABILITY GROUP [ag1] 
        SET (REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT = 1);
   ```

   This setting ensures that every active transaction is committed to the primary replica and at least one synchronous secondary replica. 
   >[!NOTE]
   >This setting is not specific to failover and should be set based on the requirements of the environment.
   
1. Offline the primary replica in preparation for role changes.
   ```SQL
   ALTER AVAILABILITY GROUP [ag1] OFFLINE
   ```

1. Promote the target secondary replica to primary. 

   ```SQL
   ALTER AVAILABILITY GROUP ag1 FORCE_FAILOVER_ALLOW_DATA_LOSS; 
   ``` 

1. Update the role of the old primary to `SECONDARY`, run the following command on the SQL Server instance that hosts the old primary replica:

   ```SQL
   ALTER AVAILABILITY GROUP [ag1] 
        SET (ROLE = SECONDARY); 
   ```

   > [!NOTE] 
   > To delete an availability group, use [DROP AVAILABILITY GROUP](../t-sql/statements/drop-availability-group-transact-sql.md). For an availability group that's created with cluster type NONE or EXTERNAL, execute the command on all replicas that are part of the availability group.

1. Resume data movement, run the following command for every database in the availability group on the SQL Server instance that hosts the primary replica: 

   ```sql
   ALTER DATABASE [db1]
        SET HADR RESUME
   ```

1. Re-create any listener you created for read-scale purposes and that isn't managed by a cluster manager. If the original listener points to the old primary, drop it and re-create it to point to the new primary.