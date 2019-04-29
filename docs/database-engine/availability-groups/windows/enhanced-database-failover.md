---
title: "Enable enhanced database failover for an availability group"
description: "Steps to enable enhanced database failover, which triggers a failover if a database in an Always On availability group is no longer able to write transactions."
ms.custom: "seodec18"
ms.date: "09/25/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
helpviewer_keywords: 
- "Availability Groups [SQL Server], enhanced database failover"
- "Availability Groups [SQL Server], failover"
ms.assetid: 
author: "MashaMSFT"
ms.reviewer: "mikeray"
ms.author: mathoma
manager: craigg
---
# Enable enhanced database failover to a database in an Always On availability group
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

In SQL Server 2012 and 2014, if a database participating in an availability group on the primary replica loses the ability to write transactions, it will not trigger a failover even if the replicas are synchronized and configured for automatic failover.

SQL Server 2016 introduces a new optional behavior named *enhanced database failover* that can be set either by the Wizard or by using Transact-SQL. If this option is enabled and automatic failover is configured, when one database participating in an availability group is no longer able to write transactions, this will trigger a failover to a synchronized secondary replica.

**Scenario 1**

An availability group is configured between Instance A and Instance B, containing a single database named DB1. DB1's data file is on Drive E and its transaction log file is on Drive F. The availability mode is set to synchronous commit with automatic failover. The new enhanced database failover option is configured on the availability group. The two replicas are currently in a synchronized state. A problem causes Drive E to fail. This scenario will not cause an enhanced database failover, as Drive E does not contain the transaction log.  

**Scenario 2**

This has the same availability group configuration as Scenario 1. Rather than Drive E failing, this time the transaction log drive, Drive F, fails. This will trigger a failover, as it meets the condition covered by enhanced database failover: the transaction log is not reachable, which means the database cannot write transactions.

**Scenario 3**

An availability group is configured between Instance A and Instance B containing two databases: DB1 and DB2. The availability mode is set to synchronous commit with a failover mode of automatic, and enhanced database failover is enabled. Access to the disk containing DB2's data and transaction log files is lost. When the problem is detected, the availability group will automatically fail over to Instance B.

## Configure and view the enhanced database failover option

Enhanced database failover can be configured using SQL Server Management Studio or Transact-SQL. The PowerShell cmdlets do not currently have this ability. By default, enhanced database failover is disabled.

### SQL Server Management Studio

Enabling enhanced database failover is possible during the creation of an accessibility group using SQL Server Management Studio. The only way to disable or enable it after creating the availability group is to use Transact-SQL.

*Manual Availability Group Creation*

Use the instructions found in the article [Use the New Availability Group Dialog Box (SQL Server Management Studio)](use-the-new-availability-group-dialog-box-sql-server-management-studio.md) to create the availability group. To enable enhanced database failover, select its checkbox next to *Database Level Health Detection*.

*Using the Availability Group Wizard*

Use the instructions found in the article [Use the Availability Group Wizard (SQL Server Management Studio)](use-the-availability-group-wizard-sql-server-management-studio.md). The option to enable enhanced database failover is found on the Specify Availability Group Name dialog. To enable it, check the box next to *Database Level Health Detection*.

### Transact-SQL

To configure enhanced database failover behavior during the creation of an availability group, DB_FAILOVER must be set to ON as follows:

```SQL
CREATE AVAILABILITY GROUP [AGNAME]
WITH ( DB_FAILOVER = ON)
...
```
To add this behavior after an availability group is configured, use the ALTER AVAILABILITY GROUP command:
```SQL
ALTER AVAILABILITY GROUP [AGNAME] SET (DB_FAILOVER = ON)
```
To disable this behavior, issue the following ALTER AVAILABILITY GROUP command:
```SQL
ALTER AVAILABILITY GROUP [AGNAME] SET (DB_FAILOVER = OFF)
```
### Dynamic management view
To see whether an availability group has enhanced database failover enabled, query the dynamic management view `sys.availability_groups`. The column `db_failover` will have a zero if disabled or 1 if enabled. 

## Next steps 

- [Configure database health detection](sql-server-always-on-database-health-detection-failover-option.md)

- [Use the Availability Group Wizard (SQL Server Management Studio)](use-the-availability-group-wizard-sql-server-management-studio.md)

- [Use the New Availability Group Dialog Box (SQL Server Management Studio)](use-the-new-availability-group-dialog-box-sql-server-management-studio.md)
 
- [Create an availability group with Transact-SQL](create-an-availability-group-transact-sql.md)

