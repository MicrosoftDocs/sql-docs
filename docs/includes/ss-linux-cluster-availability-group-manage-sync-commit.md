## Managing synchronous commit mode

>[!WARNING]
>In some cases, a SQL Server availability group in synchronous commit mode on Linux may be vulnerable to data loss. See the following details.

In SQL Server availability groups, applications write to the databases in the primary replica. When any secondary replicas are in synchronous commit mode, writes on the primary replica databases wait until the writes are committed to the synchronous secondary replica database transaction logs before committing on the primary. 

If a synchronous replica becomes unresponsive, the SQL Server instance that hosts the primary replica can automatically change that replica to asynchronous to prevent infinite wait times.

After a SQL Server service has automatically changed a replica from synchronous to asynchronous commit mode, it is possible for the resource agent to promote the asynchronous replica to a primary replica. In this case the availability group resource agent will fail the promote action when it detects that the local replica is asynchronous.

In a cluster with Windows Server Failover Clustering (WSFC) the known commit mode for each replica – as determined by the primary replica - is stored in global storage managed by WSFC. Each replica can query the shared state to determine if it is still synchronous. In the preceding scenario, if the WSFC tries to promote SQL-B, SQL-B will notice that SQL-A had marked it asynchronous, and fail the promote action.

But in a Pacemaker-managed availability group with sqlVNext CTP 1.4 the resource agent can receive Pacemaker notifications to verify the state of synchronization. 

### Pacemaker notification for Availability Group resource promotion

SQL Server vNext CTP 1.4 adds `sequence_number` to `sys.availability_groups`. `sequence_number` is a monotonically increasing BIGINT that represents how up-to-date the local AG replica is with respect to the rest of the system. Performing failovers, adding/removing replicas and other AG operations update this number. The number is updated on the primary, then pushed to secondaries. Thus a secondary that is up-to-date will have the same number as the primary.

Before CTP 1.4, the Pacemaker resource agent for availability groups could not know if a replica marked as `SYNCHRONOUS_COMMIT` was really up-to-date or not. It was possible that the replica had stopped synchronizing with the primary but was not aware. Thus the agent could promote an out-of-date replica to primary - which if successful would cause data loss. With the addition of this `sequence_number` column, this issue has been resolved. When Pacemaker decides to promote a replica to primary, it first sends a notification to all replicas to extract the sequence number and store it. Next, when Pacemaker actually tries to promote a replica to primary, the replica only promotes itself if its sequence number is the highest of all the sequence numbers and rejects the promote operation otherwise.

In this way only the replica with the highest sequence number can be promoted to primary, ensuring no data loss. Note that this is only guaranteed to work as long as at least one replica has the same sequence number as the previous primary. Set `REQUIRED_COPIES_TO_COMMIT` to the maximum number of synchronized replicas to ensure this.

Because the resource agent requires Pacemaker to send it notifications, the resource needs to be configured with `notify=true`. For every `ocf:mssql:ag` resource, the user will need to recreate it with `notify=true` before updating the `mssql-server-ha` package to CTP 1.4, otherwise the resource will go into error state.

sqlVnext introduces a new feature to force a certain number of secondaries to be available before any transactions can be committed on the primary. `REQUIRED_COPIES_TO_COMMIT` allows you to set a number of replicas that must commit to secondary replica database transaction logs before a transaction can proceed. You can use this option with `CREATE AVAILABILITY GROUP` or `ALTER AVAILABILITY GROUP`. See [CREATE AVAILABILITY GROUP](http://msdn.microsoft.com/library/ff878399.aspx).

When `REQUIRED_COPIES_TO_COMMIT` is set, transactions at the primary replica databases will wait until the transaction is committed on the required number of synchronous secondary replica database transaction logs. If enough synchronous secondary replicas are not online, transactions will stop until communication with sufficient secondary replicas resume.

The following example sets an availability group name [ag1] to `REQUIRED_COPIES_TO_COMMIT = 2`.

```Transact-SQL
ALTER AVAILABILITY GROUP [ag1]
SET (REQUIRED_COPIES_TO_COMMIT = 2)
```

In the example above, if the availability group has three secondary replicas, it will wait until two of the secondary replicas acknowledge commits. If the availability group has two replicas and one becomes unresponsive, transactions will be blocked.

### Potential for data loss

In the following case, an availability group may still be vulnerable to data loss. In a cluster that includes five nodes, if there is an availability group with replicas on three instances of SQL Server, it is possible for the primary replica and one secondary replica to be ahead of the other secondary replica. When they are ahead, the `sequence_number` in `sys.availability_groups` will be higher than the other replicas. In this condition, if the two replicas lose connectivity with the rest of the cluster, Pacemaker may see the remaining three nodes as a quorum, and allow the latent replica to promote itself to pimary. In this situation, there is potential for data loss. 
