---
title: Configure license-free DR-only replica
description: Learn how to save on license costs with a DR-only Azure SQL Managed Instance replica. 
author: MladjoA
ms.author: mlandzic
ms.reviewer: mathoma
ms.date: 11/16/2022
ms.service: sql-managed-instance
ms.subservice: high-availability
ms.topic: how-to
ms.custom: azure-sql-split
---

# Configure license-free DR-only replica (Azure SQL Managed Instance)
[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

This article teaches you to configure a DR-only replica for Azure SQL Managed Instance to save on licensing costs. 

## Overview

If your secondary SQL Managed Instance is used as a standby for disaster recovery (DR) and does not have any read-workloads, you can save on licensing costs by designating the replica as **DR-only**. When a secondary instance is designated as only for DR, Microsoft matches the number of vCores used by the primary instance without charging you licensing costs for those vCores, while still charging you for the compute and storage used by the secondary instance. For detailed information, review the [SQL Server licensing terms](https://www.microsoft.com/Licensing/product-licensing/sql-server). 

Auto-failover groups for SQL Managed Instance support only one replica - the replica must either be a readable replica, or designated as a DR-only replica. 


## Cost breakdown

For replicas designated as DR-only, Microsoft provides you the same number of vCores as the primary instance without charging you SQL Server licensing costs for those vCores. 

The benefit translates differently between customers using the pay-as-you-go model vs. customers using the [Azure Hybrid Benefit (AHB)](../azure-hybrid-benefit.md). Pay-as-you-go customers receive an equal number of vCores as the primary instance for free which are then discounted from their invoice, while customers using the AHB have an equal number of vCores as the primary instance uses returned to their licensing pool. 

For example, if you have 16 AHB licenses, and you deploy two managed instances to a failover group with 8 vCores each, you will get an extra 8 vCores for free in your license pool to use with other Azure SQL deployments. 

However, if you have only have 8 AHB licenses, and you deploy the same failover group configuration, you won't be charged SQL Server licensing costs for the vCores used by the secondary instance. In this case, you'll see 8 vCores subtracted from your monthly invoice. Likewise, pay-as-you-go customers that deploy the same configuration will not be charged for the vCores used by the secondary instance and will also see 8 vCores subtracted from their invoice. 


## Functional capabilities 

The following table describes the functional capabilities of a DR-only secondary managed instance:


|Functionality  |Description  |
|---------|---------|
|Limited read-workloads     | Once you designate your instance as DR-only, you are able to run only a limited number read-workloads on the secondary instance, such as DMVs, backups, and DBCC commands.      |
|Planned failover | All planned failover scenarios including recovery drills, relocating databases to different regions, and returning databases to the primary are supported by the DR-only replica. When the secondary switches to the primary, it can then serve read and write queries, while the old primary / new secondary becomes the DR-only replica and should not be used for read workloads.     |
|Unplanned failover | During an unplanned failover, once the secondary switches to the primary role, it can serve both read and write queries. After the outage is mitigated and the old primary reconnects, it becomes the new secondary DR-only replica and should not be used for read workloads.   |
|Backup / restore| There is no difference in backup and restore behavior between a DR-only replica and a readable secondary managed instance.         |
|Monitoring     | All monitoring operations that are supported by a readable secondary replica are supported by the DR-only replica.         |
|RPO & RTO| The DR-only replica provides the same RPO and RTO as a readable secondary replica.          |
|Removing failover group  | If the failover group is removed (using something like [Remove-AzSqlDatabaseInstanceFailoverGroup](/powershell/module/az.sql/remove-azsqldatabaseinstancefailovergroup?view=azps-8.2.0)), the DR-only replica becomes a read-write standalone instance and will now be charged the license price.      |

## Configure DR-only replica 

You can designate your secondary instance as DR-only when you create your auto-failover group, or update the configuration for an existing auto-failover group by using the Azure portal and Azure PowerShell. 

### New failover group

#### [Portal](#tab/azure-portal)


#### [PowerShell](#tab/azure-powershell)

---


### Existing failover group 

#### [Portal](#tab/azure-portal)


#### [PowerShell](#tab/azure-powershell)

---

## View DR status



## Next steps

- For detailed tutorials, see [Add a SQL Managed Instance to a failover group](../managed-instance/failover-group-add-instance-tutorial.md)
- For a sample script, see: [Use PowerShell to create an auto-failover group on a SQL Managed Instance](scripts/add-to-failover-group-powershell.md)
- For a business continuity overview and scenarios, see [Business continuity overview](../database/business-continuity-high-availability-disaster-recover-hadr-overview.md)
- To learn about automated backups, see [SQL Database automated backups](../database/automated-backups-overview.md).
- To learn about using automated backups for recovery, see [Restore a database from the service-initiated backups](../database/recovery-using-backups.md).
