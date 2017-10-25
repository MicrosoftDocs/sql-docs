Each availability group has only one primary replica. The primary replica allows reads and writes. To change which replica is the primary, you can fail over. In an availability group for high availability, the cluster manager automates in the failover process. In a read scale-out availability group, the failover process is manual. There are two ways to fail over the primary replica in a read scale availability group.

- Forced manual fail over with data loss

- Manual fail over without data loss

### Forced fail over with data loss

Use this method when the primary replica is not available and can not be recovered. You can find more information about forced failover with data loss at [Perform a Forced Manual Failover](../database-engine/availability-groups/windows/perform-a-forced-manual-failover-of-an-availability-group-sql-server.md).

To force fail over with data loss, connect to the SQL instance hosting the target secondary replica and run:
```Transact-SQL
ALTER AVAILABILITY GROUP [ag1] FORCE_FAILOVER_ALLOW_DATA_LOSS;
```

### Manual fail over without data loss

Use this method when the primary replica is available, but you need to temporarily or permanently change the configuration and change the SQL Server instance that hosts the primary replica. Before issuing manual failing over, ensure that the target secondary replica is up to date, so that there is no potential data loss. 

The following steps describe how to manually fail over without data loss:

1. Make the target secondary replica synchronous commit.

   ```Transact-SQL
   ALTER AVAILABILITY GROUP [ag1] MODIFY REPLICA ON N'**<node2>*' WITH (AVAILABILITY_MODE = SYNCHRONOUS_COMMIT);
   ```
1. Update `required_synchronized_secondaries_to_commit`to 1.

   This setting ensures that every active transaction is committed to the primary replica and at least one synchronous secondary. The availability group is ready to fail over when the synchronization_state_desc is SYNCHRONIZED and the sequence_number is the same for both primary and target secondary replica. Run this query to check:

   ```Transact-SQL
   SELECT ag.name, 
      drs.database_id, 
      drs.group_id, 
      drs.replica_id, 
      drs.synchronization_state_desc, 
      ag.sequence_number
   FROM sys.dm_hadr_database_replica_states drs, sys.availability_groups ag
   WHERE drs.group_id = ag.group_id; 
   ```

1. Demote the primary replica to secondary replica. After the primary replica is demoted, it is read-only. Run this command on the SQL instance hosting the primary replica to update the role to SECONDARY:

   ```Transact-SQL
   ALTER AVAILABILITY GROUP [ag1] SET (ROLE = SECONDARY); 
   ```

1. Promote the target secondary replica to primary. 

   ```Transact-SQL
   ALTER AVAILABILITY GROUP distributedag FORCE_FAILOVER_ALLOW_DATA_LOSS; 
   ```  

   > [!NOTE] 
   > To delete an availability group use [DROP AVAILABILITY GROUP](https://docs.microsoft.com/en-us/sql/t-sql/statements/drop-availability-group-transact-sql). For an availability group created with CLUSTER_TYPE NONE or EXTERNAL, the command has to be executed on all replicas part of the availability group.