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

Use this method when the primary replica isn't available and can't be recovered. 

To force failover with data loss, connect to the SQL Server instance that hosts the target secondary replica and then run the following command:

```SQL
ALTER AVAILABILITY GROUP [ag1] FORCE_FAILOVER_ALLOW_DATA_LOSS;
```

When the previous primary replica recovers, it will also assume the primary role. To ensure that the previous primary replica transitions into a secondary role run the following command on the previous primary replica.

```SQL
ALTER AVAILABILITY GROUP [ag1]  SET (ROLE = SECONDARY);
```

### Manual failover without data loss

Use this method when the primary replica is available, but you need to temporarily or permanently change the configuration and change the SQL Server instance that hosts the primary replica. 
To avoid potential data loss, before you issue the manual failover, ensure that the target secondary replica is up to date. 

To manually fail over without data loss:

1. Make the target secondary replica `SYNCHRONOUS_COMMIT`.

   ```SQL
   ALTER AVAILABILITY GROUP [ag1] 
        MODIFY REPLICA ON N'<node2>' 
        WITH (AVAILABILITY_MODE = SYNCHRONOUS_COMMIT);
   ```

2. To identify that active transactions are committed to the primary replica and at least one synchronous secondary replica, run the following query: 

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

3. Update `REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT` to 1.

   The following script sets `REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT` to 1 on an availability group named `ag1`. Before you run the following script, replace `ag1` with the name of your availability group:

   ```SQL
   ALTER AVAILABILITY GROUP [ag1] 
        SET REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT = 1;
   ```

   This setting ensures that every active transaction is committed to the primary replica and at least one synchronous secondary replica. 

4. Demote the primary replica to a secondary replica. After the primary replica is demoted, it's read-only. To update the role to `SECONDARY`, run the following command on the SQL Server instance that hosts the primary replica:

   ```SQL
   ALTER AVAILABILITY GROUP [ag1] 
        SET (ROLE = SECONDARY); 
   ```

5. Promote the target secondary replica to primary. 

   ```SQL
   ALTER AVAILABILITY GROUP ag1 FORCE_FAILOVER_ALLOW_DATA_LOSS; 
   ```  

   > [!NOTE] 
   > To delete an availability group, use [DROP AVAILABILITY GROUP](https://docs.microsoft.com/sql/t-sql/statements/drop-availability-group-transact-sql). For an availability group that's created with cluster type NONE or EXTERNAL, execute the command on all replicas that are part of the availability group.
