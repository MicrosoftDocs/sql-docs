---
title: "Fail over link"
titleSuffix: Azure SQL Managed Instance
description: Learn how to fail over a link between SQL Server and Azure SQL Managed Instance with SQL Server Management Studio (SSMS) and PowerShell.
author: djordje-jeremic
ms.author: djjeremi
ms.reviewer: mathoma, danil
ms.date: 09/10/2024
ms.service: azure-sql-managed-instance
ms.subservice: data-movement
ms.custom: ignite-2023
ms.topic: how-to
---
# Fail over link - Azure SQL Managed Instance

[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

This article teaches you how to fail over a database [linked](managed-instance-link-configure-how-to-ssms.md) between SQL Server and Azure SQL Managed Instance by using SQL Server Management Studio (SSMS) or PowerShell for the purpose of disaster recovery or migration. 


## Prerequisites 

To fail over your databases to your secondary replica through the link, you need the following prerequisites: 

- An active Azure subscription. If you don't have one, [create a free account](https://azure.microsoft.com/free/).
- [Supported version of SQL Server](managed-instance-link-feature-overview.md#prerequisites) with required service update installed.
- [Link](managed-instance-link-configure-how-to-ssms.md) configured between your primary and secondary replica. 
- You can fail over the link by using Transact-SQL (currently in preview) starting with [SQL Server 2022 CU13 (KB5036432)](/troubleshoot/sql/releases/sqlserver-2022/cumulativeupdate13).

## Stop workload

If you're ready to fail over your database to the secondary replica, first stop any application workloads on the primary replica during your maintenance hours. This enables database replication to catch up on the secondary so you can fail over to the secondary without data loss. Ensure your applications aren't committing transactions to the primary before failing over. 

## Fail over a database

You can fail over a linked database by using Transact-SQL (T-SQL),  SQL Server Management Studio, or PowerShell. 

### [Transact-SQL](#tab/tsql)

You can fail over the link by using Transact-SQL (currently in preview) starting with [SQL Server 2022 CU13 (KB5036432)](/troubleshoot/sql/releases/sqlserver-2022/cumulativeupdate13).

To perform a planned failover for a link, use the following T-SQL command on the primary replica:

```sql
ALTER AVAILABILITY GROUP [<DAGname>] FAILOVER
```

To perform a forced failover, use the following T-SQL command on the secondary replica: 

```sql
ALTER AVAILABILITY GROUP [<DAGname>] FORCE_FAILOVER_ALLOW_DATA_LOSS
```

### [SQL Server Management Studio (SSMS)](#tab/ssms)

Use the **Failover between SQL Server and Managed Instance** wizard in SSMS to fail over your database from your primary to your secondary replica. 

You can perform a planned failover from either the primary or the secondary replica. To do a forced failover, connect to the secondary replica.

> [!CAUTION]
> - Before failing over, stop the workload on the source database to allow the replicated database to completely catch up and fail over without data loss. If you're performing a forced failover, you could lose data. 
> - Failing over a database in SQL Server 2019 and earlier versions breaks and removes the link between the two replicas. You can't fail back to the initial primary.
> - Failing over a database while maintaining the link with SQL Server 2022 is currently in preview. 

To fail over your database, follow these steps: 

1. Open SSMS and connect to either replica.  
1. In **Object Explorer**, right-click your replicated database, hover over **Azure SQL Managed Instance link**, and select **Failover...** to open the **Failover between SQL Server and Managed Instance** wizard.  If you have multiple links from the same database, expand **Availability Groups** under **Always On availability groups** in **Object Explorer** and right-click the distributed availability group for the link you want to fail over. Select **Failover...** to open the **Failover between SQL Server and Managed Instance** wizard for that specific link.

   :::image type="content" source="./media/managed-instance-link-configure-how-to-ssms/link-failover-ssms-database-context-failover-database.png" alt-text="Screenshot that shows a database's context menu option for failover.":::

1. On the **Introduction** page, select **Next**.
1. The **Choose failover type** page shows you details about each replica, the role of the database you selected, and the supported failover types. You can initiate failover from any replica. If you choose a forced failover you must check the box to indicate you understand there could be potential data loss. Select **Next**. 

   > [!NOTE]
   > If you're migrating to Azure SQL Managed Instance, choose **Planned failover**. 

1. On the **Sign to Azure and Remote Instance** page: 
    1. Select **Sign-in** to provide your credentials and sign in to your Azure account. 
    1. Based on the **Failover type** selected on the previous page, the **Sign in** option functions differently. For a _planned failover_, signing into the remote instance (either SQL Server or SQL Managed Instance) is mandatory. For a _forced failover_, signing is optional since the following two scenarios are supported: 
       - _True disaster recovery_: Since the primary instance is typically unavailable during a true disaster, signing in isn't possible, and the user must fail over to the secondary instance immediately, making it the new primary instance. After the outage is resolved, the link is in an inconsistent state as both replicas are now in the primary role (split-brain scenario). 
       - _Disaster recovery drill_: Doing disaster recovery drills with forced failover is discouraged as there could be potential data loss. However, during a drill, since the primary instance is available, signing in is supported, and you are given the option to reverse roles for both replicas to avoid the split-brain scenario. 
1. On the **Post-Failover Operations** page, options differ between SQL Server 2022 and earlier versions, and whether or not you were able to connect to the primary instance. 
 
    -  For **SQL Server 2022**, you can choose to stop replication between replicas, which drops the link and distributed availability group after failover completes. If you want to maintain the link and continue replicating data between replicas, leave the box unchecked. If you choose to drop the link, you can also check the box to drop the availability group if you created it solely for the purpose of replicating your database to Azure and you no longer need it. Check the boxes that fit your scenario, and then select **Next**. 
    -  For **SQL Server 2019** and earlier versions, the option to **Remove the link** is checked by default, and you can't uncheck it since failing over to SQL Managed Instance stops replication, breaks the link, and drops the distributed availability group. Check the box to indicate you understand the link will be dropped, and then select **Next**. 
    - (Optionally) If you were able to sign into the SQL Server instance on the previous page, you also have the option to delete the availability group on the SQL Server instance after a forced failover by checking the box in the **Clean-up** section. 

1. On the **Summary** page, review the actions. Optionally, select **Script** to generate a script so you can easily fail over the database using the same link in the future. Select **Finish** when you're ready to fail over the database. 
1. After all steps finish, the **Results** page shows check marks next to the successfully completed actions. You can now close the window. 

If you chose to maintain the link for SQL Server 2022, the secondary becomes the new primary, the link is still active and you can fail back to the secondary. 

If you're on SQL Server 2019 and earlier versions, or if you chose to drop the link for SQL Server 2022, the link is dropped and no longer exists after failover completes. The source database and target database on each replica can both execute a read/write workload. They're completely independent. 

> [!IMPORTANT]
> After successful fail over to SQL Managed Instance, manually repoint your application(s) connection string to the SQL managed instance FQDN to complete the migration or fail over process and continue running in Azure.

### [PowerShell](#tab/powershell)

To fail over, you first have to switch the replication modes SQL Server instance by using Transact-SQL (T-SQL). 

Then, you can fail over and switch roles by using PowerShell. 


### Switch replication mode (Failover to SQL MI)

Replication between SQL Server and SQL Managed Instance is asynchronous by default. If you're failing over _from SQL Server to Azure SQL Managed Instance_, before you fail over your database, switch the link to synchronous mode on SQL Server by using Transact-SQL (T-SQL). 

> [!NOTE]
> - Skip this step if you're failing over from SQL Managed Instance to SQL Server 2022. 
> - Synchronous replication across large network distances might slow down transactions on the primary replica.

Run the following T-SQL script on SQL Server to change the replication mode of the distributed availability group from async to sync. Replace:
- `<DAGName>` with the name of the distributed availability group (used to create the link).
- `<AGName>` with the name of the availability group created on SQL Server (used to create the link). 
- `<ManagedInstanceName>` with the name of your managed instance.

```sql
-- Run on SQL Server
-- Sets the distributed availability group to a synchronous commit.
-- ManagedInstanceName example: 'sqlmi1'
USE master
GO
ALTER AVAILABILITY GROUP [<DAGName>] 
MODIFY 
AVAILABILITY GROUP ON
    '<AGName>' WITH
    (AVAILABILITY_MODE = SYNCHRONOUS_COMMIT),
    '<ManagedInstanceName>' WITH
    (AVAILABILITY_MODE = SYNCHRONOUS_COMMIT);
```

To confirm that you've changed the link's replication mode successfully, use the following dynamic management view. Results indicate the `SYNCHRONOUS_COMMIT` state.

```sql
-- Run on SQL Server
-- Verifies the state of the distributed availability group
SELECT
    ag.name, ag.is_distributed, ar.replica_server_name,
    ar.availability_mode_desc, ars.connected_state_desc, ars.role_desc,
    ars.operational_state_desc, ars.synchronization_health_desc
FROM
    sys.availability_groups ag
    join sys.availability_replicas ar
    on ag.group_id=ar.group_id
    left join sys.dm_hadr_availability_replica_states ars
    on ars.replica_id=ar.replica_id
WHERE
    ag.is_distributed=1
```

Now that you've switched SQL Server to synchronous commit mode, replication between the two instances is synchronous. If you need to reverse this state, follow the same steps and set the `AVAILABILITY_MODE` to `ASYNCHRONOUS_COMMIT`.

### Check LSN values on both SQL Server and SQL Managed Instance

To complete the failover or migration, confirm that replication to the secondary is finished. For this, ensure the log sequence numbers (LSNs) in the log records for both SQL Server and SQL Managed Instance are the same.

Initially, it's expected that the LSN on the primary is higher than the LSN on the secondary. Network latency can cause replication to lag somewhat behind the primary. Because the workload has been stopped on the primary, the LSNs will match and stop changing after some time. 

Use the following T-SQL query on SQL Server to read the LSN of the last recorded transaction log. Replace:
- `<DatabaseName>` with your database name and look for the last hardened LSN number.

```sql
-- Run on SQL Server
-- Obtain the last hardened LSN for the database on SQL Server.
SELECT
    ag.name AS [Replication group],
    db.name AS [Database name], 
    drs.database_id AS [Database ID], 
    drs.group_id, 
    drs.replica_id, 
    drs.synchronization_state_desc AS [Sync state], 
    drs.end_of_log_lsn AS [End of log LSN],
    drs.last_hardened_lsn AS [Last hardened LSN] 
FROM
    sys.dm_hadr_database_replica_states drs
    inner join sys.databases db on db.database_id = drs.database_id
    inner join sys.availability_groups ag on drs.group_id = ag.group_id
WHERE
    ag.is_distributed = 1 and db.name = '<DatabaseName>'
```

Use the following T-SQL query on SQL Managed Instance to read the last hardened LSN for your database. Replace `<DatabaseName>` with your database name.

This query works on a General Purpose SQL Managed Instance. For a Business Critical SQL Managed Instance, uncomment `and drs.is_primary_replica = 1` at the end of the script. On the Business Critical service tier, this filter ensures that details are only read from the primary replica.

```sql
-- Run on SQL managed instance
-- Obtain the LSN for the database on SQL Managed Instance.
SELECT
    db.name AS [Database name],
    drs.database_id AS [Database ID], 
    drs.group_id, 
    drs.replica_id, 
    drs.synchronization_state_desc AS [Sync state],
    drs.end_of_log_lsn AS [End of log LSN],
    drs.last_hardened_lsn AS [Last hardened LSN]
FROM
    sys.dm_hadr_database_replica_states drs
    inner join sys.databases db on db.database_id = drs.database_id
WHERE
    db.name = '<DatabaseName>'
    -- for Business Critical, add the following as well
    -- AND drs.is_primary_replica = 1
```

Alternatively, you could also use the [Get-AzSqlInstanceLink](/powershell/module/az.sql/get-azsqlinstancelink) PowerShell or [az sql mi link show](/cli/azure/sql/mi/link#az-sql-mi-link-show) Azure CLI command to fetch the `LastHardenedLsn` property for your link on SQL Managed Instance to provide the same information as the previous T-SQL query.

> [!IMPORTANT]
> Verify once again that your workload is stopped on the primary. Check that LSNs on both SQL Server and SQL Managed Instance match, and that they **remain matched** and unchanged for some time. Stable LSNs on both instances indicate the tail log has been replicated to the secondary and the workload is effectively stopped.

#### Fail over a database

If you want to use PowerShell to fail over a database between SQL Server 2022 and SQL Managed Instance while still maintaining the link, or to perform a failover with data loss for any version of SQL Server, use the **Failover between SQL Server and Managed Instance** wizard in SSMS to generate the script for your environment. You can perform a planned failover from either the primary or the secondary replica. To do a forced failover, connect to the secondary replica.

To break the link and stop replication when you fail over or migrate your database regardless of SQL Server version, use the [Remove-AzSqlInstanceLink](/powershell/module/az.sql/remove-azsqlinstancelink) PowerShell or [az sql mi link delete](/cli/azure/sql/mi/link#az-sql-mi-link-delete) Azure CLI command. 

> [!CAUTION]
> - Before failing over, stop the workload on the source database to allow the replicated database to completely catch up and failover without data loss. If you perform a forced failover, or if you break the link before LSNs match, you might lose data.
> - Failing over a database in SQL Server 2019 and earlier versions breaks and removes the link between the two replicas. You can't fail back to the initial primary.
> - Failing over a database while maintaining the link with SQL Server 2022 is currently in preview. 

The following sample script breaks the link and ends replication between your replicas, making the database read/write on both instances. Replace:
- `<ManagedInstanceName>` with the name of your managed instance. 
- `<DAGName>` with the name of the link you're failing over (output of the property `Name` from `Get-AzSqlInstanceLink` command executed earlier above).

```powershell-interactive
# Run in Azure Cloud Shell (select PowerShell console) 
# =============================================================================
# POWERSHELL SCRIPT TO FAIL OVER OR MIGRATE DATABASE TO AZURE
# ===== Enter user variables here ====

# Enter your managed instance name – for example, "sqlmi1"
$ManagedInstanceName = "<ManagedInstanceName>"
$LinkName = "<DAGName>"

# ==== Do not customize the following cmdlet ====

# Find out the resource group name
$ResourceGroup = (Get-AzSqlInstance -InstanceName $ManagedInstanceName).ResourceGroupName

# Failover the specified link
Remove-AzSqlInstanceLink -ResourceGroupName $ResourceGroup |
-InstanceName $ManagedInstanceName -Name $LinkName -Force
```

When failover succeeds, the link is dropped and no longer exists. The SQL Server database and SQL Managed Instance database can both execute read/write workloads as they're now completely independent. 

> [!IMPORTANT]
> After successful fail over to SQL Managed Instance, manually repoint your application(s) connection string to the SQL managed instance FQDN to complete the migration or fail over process and continue running in Azure.

After the link is dropped, you can keep the availability group on SQL Server, but you must drop the _distributed_ availability group to remove link metadata from SQL Server. This additional step is only necessary when failing over by using PowerShell since SSMS performs this action for you. 

To drop your distributed availability group, replace the following value and then run the sample T-SQL code: 

- `<DAGName>` with the name of the distributed availability group on SQL Server (used to create the link). 

``` sql
-- Run on SQL Server
USE MASTER
GO
DROP AVAILABILITY GROUP <DAGName> 
GO
```

---

## View database after failover

For SQL Server 2022, if you chose to maintain the link, you can check that the distributed availability group exists under **Availability Groups** in **Object Explorer** in SQL Server Management Studio. 

If you dropped the link during failover, you can use **Object Explorer** to confirm the distributed availability group no longer exists. If you chose to keep the availability group, the database will still be **Synchronized**. 

## Clean up after failover

Unless **Remove link after successful failover** is selected, failing over with SQL Server 2022 doesn't break the link. You can maintain the link after failover, which leaves the availability group, and distributed availability group active. No further action is needed. 

Dropping the link only drops the distributed availability group, and leaves the availability group active. You can decide to keep the availability group, or drop it. 

If you decide to drop your availability group, replace the following value and then run the sample T-SQL code: 

- `<AGName>` with the name of the availability group on SQL Server (used to create the link).

``` sql
-- Run on SQL Server
USE MASTER
GO
DROP AVAILABILITY GROUP <AGName> 
GO
```


## Inconsistent state after forced failover

Following a forced failover, you might encounter a split-brain scenario where both replicas are in the primary role, leaving the link in an inconsistent state. This can happen if you fail over to the secondary replica during a disaster, and then the primary replica comes back online.

First, confirm you're in a split-brain scenario. You can do so by using SQL Server Management Studio (SSMS) or Transact-SQL (T-SQL).

Connect to both SQL Server and SQL managed instance in SSMS, and then in **Object Explorer**, expand **Availability replicas** under the **Availability group** node in **Always On High Availability**.  If two different replicas are listed as **(Primary)**, you're in a split-brain scenario. 

Alternatively, you can run the following T-SQL script on *both* SQL Server and SQL Managed Instance to check the role of the replicas:

```sql
-- Execute on SQL Server and SQL Managed Instance 

declare @link_name varchar(max) = '<DAGName>' 
USE MASTER 
GO

SELECT
   ag.name [Link name], 
   rs.role_desc [Link role] 
FROM
   sys.availability_groups ag 
   join sys.dm_hadr_availability_replica_states rs 
   on ag.group_id = rs.group_id 
WHERE 
   rs.is_local = 1 and ag.name = @link_name 
GO
```

If both instances list a different **Primary** in the **Link role** column, you're in a split-brain scenario.

To resolve the split brain state, first take a backup on whichever replica was the original primary. If the original primary was SQL Server, then take a [tail log backup](/sql/relational-databases/backup-restore/tail-log-backups-sql-server). If the original primary was SQL Managed Instance, then take a [copy-only full backup](/sql/relational-databases/backup-restore/copy-only-backups-sql-server). After the backup completes, set the distributed availability group to the secondary role for the replica that used to be the original primary but will now be the new secondary.
 
For example, in the event of a true disaster, assuming you've forced a failover of your SQL Server workload to Azure SQL Managed Instance, and you intend to continue running your workload on SQL Managed Instance, take a tail log backup on SQL Server, and then set the distributed availability group to the secondary role on SQL Server such as the following example:

```sql
--Execute on SQL Server 
USE MASTER

ALTER availability group [<DAGName>] 
SET (role = secondary) 
GO 
```

Next, execute a planned manual failover from SQL Managed Instance to SQL Server by using the link, such as the following example: 

```sql
--Execute on SQL Managed Instance 
USE MASTER

ALTER availability group [<DAGName>] FAILOVER 
GO 
```


## Related content

For more information on the link feature, review the following resources:

- [Managed Instance link overview](managed-instance-link-feature-overview.md)
- [Prepare your environment for Managed Instance link](./managed-instance-link-preparation.md)
- [Configure link between SQL Server and SQL Managed instance with scripts](managed-instance-link-configure-how-to-scripts.md)
- [Disaster recovery with Managed Instance link](managed-instance-link-disaster-recovery.md)
- [Best practices for maintaining the link](managed-instance-link-best-practices.md)
