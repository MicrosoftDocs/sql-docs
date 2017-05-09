# Enhanced Database Failover
In SQL Server 2012 and 2014, if a database participating in an availability group (AG) on the primary loses the ability to write transactions, it will not trigger a failover even if they are synchronized and the availability group is configured for automatic failover. 

SQL Server 2016 introduces a new optional behavior called *enhanced database failover* that can be set both in the Wizard and in Transact-SQL. If this option is enabled and automatic failover is configured, when one database participating in an availability group is no longer able to write transactions, this will trigger a failover to a synchronized secondary replica.

**Scenario 1**

An accessiblity group is configured between Instance A and Instance B containing a single database named DB1. DB1's data file is on Drive E and its log file on Drive F. The availability mode is set to synchronous commit with a failover mode of automatic. The new enhanced database failover option is configured on the AG. The two replicas are currently in a synchronized state. A problem causes Drive E to fail. This will not cause a failover since the data file was lost, not the transaction log. While losing a data drive can potentially be catastrophic, it is not a scenario handed by enhanced database failover.

**Scenario 2**

This has the same AG configuration as Scenario 1. Instead of Drive E failing, this time the log drive, Drive F, fails. This will cause a failover since the condition covered by enhanced database failover (losing the transaction log, and thus the ability to write transactions) is triggered, the secondary replica was synchronized, and automatic failover was enabled.

**Scenario 3**

An AG is configured between Instance A and Instance B containing two databases: DB1 and DB2. The availability mode is set to synchronous commit with a failover mode of automatic, and enhanced database failover is enabled. Access to the disk containing DB2's data and transaction log files is lost. As soon as the problem is detected, the AG will automatically fail over to Instance B.

## Configuring and Viewing the Enhanced Database Failover Option

Enhanced database failover can be configured using SQL Server Management Studio or Transact-SQL. The PowerShell cmdlets do not currently have this ability. By default, enhanced database failover is disabled.

### SQL Server Management Studio

Enabling enhanced database failover is possible during the creation of an accessibility group using SQL Server Management Studio. The only way to disable or enable it after creating the AG is to use Transact-SQL.

*Manual Availability Group Creation*

Use the instructions found in the topic  Use the [New Availability Group Dialog Box (SQL Server Management Studio)](https://docs.microsoft.com/en-us/sql/database-engine/availability-groups/windows/use-the-new-availability-group-dialog-box-sql-server-management-studio) to create the AG. The option to enable enhanced database failover can be done by selecting the checkbox next to *Database Level Health Detection*.

*Using the Availability Group Wizard*

Use the instructions found in the topic Use the [Availability Group Wizard (SQL Server Management Studio)](https://docs.microsoft.com/en-us/sql/database-engine/availability-groups/windows/use-the-availability-group-wizard-sql-server-management-studio). The option to enable enhanced database failover is found on the Specify Availability Group Name dialog. To enable it, check the box next to *Database Level Health Detection*.

### Transact-SQL

To configure this new behavior during the creation of an AG, DB_FAILOVER must be set to ON in the WITH clause at the top of the statement as follows:
```
CREATE AVAILABILITY GROUP [AGNAME]
WITH ( DB_FAILOVER = ON)
...
```
To add this behavior after an AG is configured, use the ALTER AVAILABILITY GROUP command:
```
ALTER AVAILABILITY GROUP [AGNAME] SET (DB_FAILOVER = ON)
```
To disable this behavior, issue the following ALTER AVAILABILITY GROUP command:
```
ALTER AVAILABILITY GROUP [AGNAME] SET (DB_FAILOVER = OFF)
```
### Dynamic Management View
To see whether an availability group has enhanced database failover enabled, query the DMV `sys.availablity_groups`. The column `db_failover` will have a 0 if disabled or 1 if enabled. 
