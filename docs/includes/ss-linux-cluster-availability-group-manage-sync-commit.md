## Managing synchronous commit mode

>[!WARNING]
>In some cases, a SQL Server availability group in synchronous commit mode on Linux may be vulnerable to data loss. See the following details on the root cause and the workaround to make sure data loss is avoided. This issue is going to be fixed in the upcoming releases.

### Pacemaker notification for Availability Group resource promotion

Before CTP 1.4 release, the Pacemaker resource agent for availability groups could not know if a replica marked as `SYNCHRONOUS_COMMIT` was really up-to-date or not. It was possible that the replica had stopped synchronizing with the primary but was not aware. Thus the agent could promote an out-of-date replica to primary - which if successful would cause data loss. 

SQL Server 2017 CTP 1.4 adds `sequence_number` to `sys.availability_groups` to solve this issue. `sequence_number` is a monotonically increasing BIGINT that represents how up-to-date the local AG replica is with respect to the rest of the system. Performing failovers, adding/removing replicas and other AG operations update this number. The number is updated on the primary, then pushed to secondaries. Thus a secondary that is up-to-date will have the same number as the primary.

When Pacemaker decides to promote a replica to primary, it first sends a notification to all replicas to extract the sequence number and store it. Next, when Pacemaker actually tries to promote a replica to primary, the replica only promotes itself if its sequence number is the highest of all the sequence numbers and rejects the promote operation otherwise. In this way only the replica with the highest sequence number can be promoted to primary, ensuring no data loss.

Note that this is only guaranteed to work as long as at least one replica available for promotion has the same sequence number as the previous primary. Because the resource agent requires Pacemaker to send notifications to all replicas, the availability resource needs to be configured with `notify=true`. For every existing `ocf:mssql:ag` resource, the user will need to update he resource configuration with `notify=true` before updating the `mssql-server-ha` package to CTP 1.4, otherwise the resource will go into error state. 

### How to avoid potential for data loss 

In the following case, an availability group may still be vulnerable to data loss. In a cluster that includes five nodes, if there is an availability group with replicas on three instances of SQL Server, it is possible for the primary replica and one secondary replica to be ahead of the other secondary replica. When they are ahead, the `sequence_number` in `sys.availability_groups` will be higher than the other replicas. In this condition, if the two replicas lose connectivity with the rest of the cluster, Pacemaker may see the remaining three nodes as a quorum, and allow the latent replica to promote itself to primary. In this situation, there is potential for data loss.

sql2017 introduces a new feature to force a certain number of secondaries to be available before any transactions can be committed on the primary. `REQUIRED_COPIES_TO_COMMIT` allows you to set a number of replicas that must commit to secondary replica database transaction logs before a transaction can proceed. You can use this option with `CREATE AVAILABILITY GROUP` or `ALTER AVAILABILITY GROUP`. See [CREATE AVAILABILITY GROUP](https://msdn.microsoft.com/library/ff878399.aspx).

To avoid the potential of data loss described above set `REQUIRED_COPIES_TO_COMMIT` to the maximum number of synchronized replicas. Upcoming releases will have built in logic to handle setting `REQUIRED_COPIES_TO_COMMIT` automatically.
When `REQUIRED_COPIES_TO_COMMIT` is set, transactions at the primary replica databases will wait until the transaction is committed on the required number of synchronous secondary replica database transaction logs. If enough synchronous secondary replicas are not online, transactions will stop until communication with sufficient secondary replicas resume.

The following example sets an availability group name [ag1] to `REQUIRED_COPIES_TO_COMMIT = 2`. 

```Transact-SQL
ALTER AVAILABILITY GROUP [ag1]
SET (REQUIRED_COPIES_TO_COMMIT = 2)
```

In the example above, if the availability group has two secondary replicas, it will wait until both secondary replicas acknowledge commits. If one becomes unresponsive, transactions will be blocked.
