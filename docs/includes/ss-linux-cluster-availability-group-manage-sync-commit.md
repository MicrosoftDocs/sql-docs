## Managing synchronous commit mode
<a name="sync-commit"></a>

>[!WARNING]
>In some cases, a SQL Server availability group in synchronous commit mode on Linux may be vulnerable to data loss. See the following details.

In SQL Server availability groups, applications write to the databases in the primary replica. When any secondary replicas are in synchronous commit mode, writes on the primary replica databases wait until the writes are committed to the synchronous secondary replica database transaction logs before committing on the primary. 

If a synchronous replica becomes unresponsive, the SQL Server instance that hosts the primary replica can automatically change that replica to asynchronous to prevent infinite wait times.

After a SQL Server service has automatically changed a replica from synchronous to asynchronous commit mode, it is possible for the resource agent to promote the asynchronous replica to a primary replica. In this case the availability group resource agent will fail the promote action when it detects that the local replica is asynchronous.

For example, an availability group has a primary replica on SQL-A. The group has secondary replicas on SQL-B, SQL-C, and SQL-D in synchronous commit mode with automatic failover. If SQL-B quits responding to write request because of a network outage, SQL-A can change the replica commit mode on SQL-B to asynchronous. This allows transactions to continue on the other replicas. The replica on SQL-B is not aware that it is now asynchronous. It does not communicate any problems to Pacemaker via the resource monitor action.

In this situation, if SQL-A goes down, Pacemaker will promote one of the replicas to primary. If it attempts to promote SQL-B it will succeed. In this case data loss is possible.

In a cluster with Windows Server Failover Clustering (WSFC) the known commit mode for each replica – as determined by the primary replica - is stored in global storage managed by WSFC. Each replica can query the shared state to determine if it is still synchronous. In the preceding scenario, if the WSFC tries to promote SQL-B, SQL-B will notice that SQL-A had marked it asynchronous, and fail the promote action.

But in a Pacemaker-managed availability group this does not happen, and in sqlVNext CTP 1.3 there is a possibility that a lagging asynchronous secondary might be promoted to a primary, causing data loss.

sqlVnext introduces a new feature to force a certain number of secondaries to be available before any transactions can be committed on the primary. You can use this feature to work around the above bug. `REQUIRED_COPIES_TO_COMMIT` allows you to set a number of replicas that must commit to secondary replica database transaction logs before a transaction can proceed. You can use this option with `CREATE AVAILABILITY GROUP` or `ALTER AVAILABILITY GROUP`. See [CREATE AVAILABILITY GROUP](http://msdn.microsoft.com/library/ff878399.aspx).

When `REQUIRED_COPIES_TO_COMMIT` is set, transactions at the primary replica databases will wait until the transaction is committed on the required number of synchronous secondary replica database transaction logs. If enough synchronous secondary replicas are not online, transactions will stop until communication with sufficient secondary replicas resume.

The following example sets an availability group name [ag1] to `REQUIRED_COPIES_TO_COMMIT = 2`.

```Transact-SQL
ALTER AVAILABILITY GROUP [ag1]
SET (REQUIRED_COPIES_TO_COMMIT = 2)
```

In the example above, if the availability group has three secondary replicas, it will wait until two of the secondary replicas acknowledge commits. If the availability group has two replicas and one becomes unresponsive, transactions will be blocked.
