---
title: Disaster recovery drills
description: Learn guidance and best practices for using Azure SQL Managed Instance to perform disaster recovery drills.
author: Stralle
ms.author: strrodic
ms.reviewer: wiassaf, mathoma
ms.date: 06/25/2024
ms.service: azure-sql-managed-instance
ms.subservice: high-availability
ms.topic: conceptual
---
# Performing disaster recovery drills - Azure SQL Managed Instance
[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

> [!div class="op_single_selector"]
> * [Azure SQL Database](../database/disaster-recovery-drills.md?view=azuresql-db&preserve-view=true)
> * [Azure SQL Managed Instance](disaster-recovery-drills.md?view=azuresql-mi&preserve-view=true)

It's recommended to periodically test and validate that applications are ready for a recovery workflow. Verifying the application behavior and implications of data loss and/or the disruption that failover involves is good engineering practice. It is also a requirement by most industry standards as part of business continuity certification.

Performing a disaster recovery drill consists of:

* Simulating data tier outage
* Recovering
* Validate application integrity post recovery

Depending on how you [designed your application for business continuity](business-continuity-high-availability-disaster-recover-hadr-overview.md), the workflow to execute the drill can vary. This article describes the best practices for conducting a disaster recovery drill in the context of Azure SQL Managed Instance.

## Geo-restore

To prevent the potential data loss when conducting a disaster recovery drill, perform the drill using a test environment by creating a copy of the production environment and using it to verify the application's failover workflow.

### Outage simulation

To simulate the outage, you can rename the source database. This name change causes application connectivity failures.

### Recovery

* Perform a geo-restore of the database to a different instance as described in [disaster recovery guidance](disaster-recovery-guidance.md).
* Change the application configuration to connect to the recovered instance and follow the [Configure a database after recovery](disaster-recovery-guidance.md#configure-your-database-after-recovery) guide to complete the recovery.

### Validation

Complete the drill by verifying the application integrity post recovery (including connection strings, logins, basic functionality testing, or other validations part of standard application signoffs procedures).

## Failover groups

For an instance protected by failover groups, the drill exercise involves planned failover to the secondary instance. The planned failover ensures that the primary and the secondary instances in the failover group remain in sync when the roles are switched. Unlike the unplanned failover, this operation does not result in data loss, so the drill can be performed in a production environment.

Configure your failover group with the [failover policy](failover-group-sql-mi.md#failover-policy) that suits your business need, and test failover regardless of how your failover policy is configured. For more information, review [test failover](failover-group-configure-sql-mi.md#test-failover). A customer-managed failover policy is recommended to give you control over the failover process.


> [!IMPORTANT]
> Since [system databases aren't replicated](failover-group-configure-sql-mi.md#enable-scenarios-dependent-on-objects-from-the-system-databases) between instances in a failover group, manually recreate system objects on the secondary instance and then test environments with system object dependencies to ensure they continue functioning properly after a failover. 

### Outage simulation

To simulate the outage, you can disable the web application or virtual machine connected to the database. This outage simulation results in the connectivity failures for the web clients.

### Recovery

* Make sure the application configuration in the DR region points to the former secondary, which becomes the fully accessible new primary.
* Initiate a [planned failover](failover-group-configure-sql-mi.md#test-failover) of the failover group from the secondary instance.
* Follow the [Configure a database after recovery](disaster-recovery-guidance.md) guide to complete the recovery.

### Validation

Complete the drill by verifying the application integrity post recovery (including connectivity, basic functionality testing, or other validations required for the drill signoffs).

## Related content

To learn more, review: 

* [Continuity scenarios](business-continuity-high-availability-disaster-recover-hadr-overview.md).
* [Automated backups](automated-backups-overview.md)
* [Restore a database from the service-initiated backups](recovery-using-backups.md).
* [Failover groups](failover-group-sql-mi.md).
* [Disaster recovery guidance](disaster-recovery-guidance.md)  
* [High availability and disaster recovery checklist](high-availability-disaster-recovery-checklist.md). 
