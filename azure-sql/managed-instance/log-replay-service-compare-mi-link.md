---
title: Compare LRS with Managed Instance Link
titleSuffix: Azure SQL Managed Instance
description: Compare log replay service (LRS) with the Managed Instance link when migrating to Azure SQL Managed Instance.
author: danimir
ms.author: danil
ms.reviewer: mathoma, randolphwest
ms.date: 01/26/2026
ms.service: azure-sql-managed-instance
ms.subservice: migration
ms.topic: product-comparison
ms.custom:
  - ignite-2025
monikerRange: "=azuresql || =azuresql-mi"
---
# Compare LRS with Managed Instance link

[!INCLUDE [appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

This article compares the [Log Replay Service (LRS)](log-replay-service-overview.md) with the [Managed Instance link](managed-instance-link-feature-overview.md) when migrating to [Azure SQL Managed Instance](sql-managed-instance-paas-overview.md).

## Overview

Log Replay Service (LRS) has been used for migrations to Azure SQL Managed Instance since the service launched in November of 2018. Under the hood, LRS relies on the implementation of [log shipping](/sql/database-engine/log-shipping/about-log-shipping-sql-server), which also powers [Azure Database Migration Service (DMS)](/azure/dms/dms-overview).

In March 2022, the Managed Instance link (MI link) was introduced as a more performant migration option, with a promise of the best possible minimum downtime migration. The Managed Instance link uses [distributed Always On availability group](/sql/database-engine/availability-groups/windows/distributed-availability-groups) technology to replicate data near real-time from SQL Server to Azure SQL Managed Instance. With the link, you can also fall back online *from* SQL Managed Instance *to* SQL Server 2022 or later as a migration insurance policy.

LRS and the MI link complement each other in capabilities, with each technology suiting different business needs. Review the capabilities of each tool to determine which is best to use for migration based on your specific circumstances.

## Compare LRS and MI link

The fundamental difference between LRS and the MI link stems from the underlying technology. Since LRS is based on log shipping, differential and transaction log backups are continuously taken from SQL Server, uploaded to Azure Blob Storage, and restored to SQL Managed Instance. The process isn't real-time, as it takes time to back up files, upload them, and restore them. The performance of LRS is based on the size of the backup chunks.

In contrast, MI link uses Always On availability group technology to send transaction log records in near real-time from SQL Server to SQL Managed Instance, making it a considerably more performant migration solution. However, to configure the MI link, you need to set up a VPN between SQL Server and SQL Managed Instance and open the appropriate ports in the firewall whereas LRS works out of the box by using a public endpoint. LRS can be used for all editions of SQL Server 2008 and later, while MI link can be used for SQL Server 2016 and later, for Standard, Enterprise, and Developer editions only.

> [!NOTE]  
> SQL Server 2025 introduces separate Enterprise Developer and Standard Developer editions of SQL Server.

A major benefit of the MI link is the ability to perform a reverse migration back to SQL Server 2022 and later, which isn't possible with LRS. Another major benefit of migrating with the MI link is that the database on the SQL Managed Instance can be used for read-only workloads while the migration is in progress. This capability isn't available with LRS, since the database is in a restoring state until the migration is complete. Likewise, when you perform a reverse migration back to SQL Server 2022 and later, the database is accessible for read-only workloads on SQL Server while the migration is in progress.

The following table compares both LRS and MI link in greater detail:

| **Functionality** | **Managed Instance link (MI link)** | **Log Replay Service (LRS)** | **Notes** |
| --- | --- | --- | --- |
| **Underlying technology** | Distributed availability groups (AG) | Log shipping | MI link uses a distributed availability group for replication, which is newer and more advanced when compared to the log shipping technology used by LRS. |
| **Replication performance** | Near real-time. | Restores every few minutes. | Replicating data through the MI link is considerably more performant than applying transaction log backups with LRS. |
| **Minimum supported source version** | SQL Server 2016 and above | SQL Server 2008 and above | LRS can support much older SQL Server versions than MI link. |
| **Minimum supported Windows Server version** | Windows Server 2012 R2 | Windows Server 2008 | LRS can support much older Windows Server versions than MI link. |
| **Read-only secondary** | Supported. | Not supported. | While replication is in progress, SQL Managed Instance databases replicated through the link can be used for read-only workloads, which lets you test your migration before the cut over, or use your databases before migrating to Azure. Likewise, when you perform a reverse migration back to SQL Server 2022 and later, the database is accessible for read-only workloads on SQL Server while the migration is in progress. This capability isn't available with LRS. |
| **Replication of TDE encrypted databases** | Yes, requires importing security keys to SQL Managed Instance. | Yes, requires importing security keys to SQL Managed Instance. | The requirement and procedure to migrate the corresponding encryption certificate from SQL Server to SQL managed instance before starting the migration is the same for both migration options. |
| **Network connectivity type** | - Private endpoint<br />- VPN configured with both inbound and outbound ports | Public endpoint | While MI link provides additional layers of security, and offers a VPN as an option, networking is more difficult to configure compared to LRS.<br /><br />By default, LRS provides a simplified experience so you can use it immediately without any network or VPN configuration. LRS uses a public endpoint by default, which is less secure than the VPN used with MI link, and it might not satisfy some of the most demanding security requirements since it uses a publicly exposed Azure Blob storage account as an intermediary to save data before it's restored to SQL Managed Instance. While it's possible to use a private endpoint with LRS to make the transmission of data more secure, it increases initial configuration complexity. |
| **Data encryption in transmission** | - Data encrypted with AES, and<br />- SSL is used for data transmission encryption. | SSL is used for data transmission encryption. | MI link uses an additional data AES encryption layer. SSL is used for transmission of data for migration tools. |
| **Authentication for the replication** | Certificates signed by a trusted authority (CA) | Managed identities or SAS tokens | MI link requires a Certificate Authority (CA) to sign a certificate for authentication. For LRS, using managed identities is more secure than using self-generated SAS tokens. |
| **Affected by system updates or failover** | No, other than a minimum interruption for a short failover. | - For General Purpose instances, the migration is automatically paused and resumed after interruptions.<br />- For Business Critical instances, the migration process is canceled for interruptions and must be restarted manually. | MI link is resilient and migration isn't affected by SQL Managed Instance failovers.<br /><br />Conversely, LRS migrations are delayed by restarts or failovers of SQL managed instances in the General Purpose service tier, and migration is restarted for instances in the Business Critical service tier. |
| **Replication duration** | Unlimited replication time using the link (months and even years at a time). | LRS job can run up to 30 days. | An MI link can run for an unlimited amount of time.<br /><br />LRS is limited to maximum of 30 days of continuous log-shipping, after which the migration is automatically stopped, and needs to be restarted from the beginning. |
| **Type of migration** | True online migration with only a short failover (measured in seconds). | - Online migration with expected downtime during the cutover for the time it takes to restore the last backup file.<br />- Cutover takes considerably longer for instances in the Business Critical service tier. | MI link is the only solution that offers a minimum downtime solution (<1 minute) for all SQL Managed Instance service tiers.<br /><br />With LRS, the last backup file is still restoring during cutover so based on the size of the last backup file and the time it takes to restore it, there could be a significant wait until the database becomes available on SQL Managed Instance.<br /><br />When using LRS to migrate to the Business Critical service tier, the migration cutover downtime can be significantly longer, as the entire database must be replicated to the secondary nodes from the primary node before the database is available for workloads on the primary. Depending on the overall database size, replication to the other nodes, and therefore downtime, can sometimes take hours.<br /><br />As such, databases can come online considerably slower with LRS than with the MI link, which can be almost instantaneous. |
| **Maintenance required on source** | Yes, regular transaction log backups. | No. | MI link requires regular transaction log backups of the source SQL Server instance during the migration to truncate the transaction log and prevent running out of disk space.<br /><br />Conversely, no maintenance is required for LRS. |
| **Resiliency** | Automatically resumes link replication if SQL Server restarts. | - Migration stalls if there's a broken backup chain, or an incorrectly specified last backup file.<br />- Doesn't support backup files from multiple databases in the same folder (migration fails). | MI link is more resilient than LRS because replication automatically resumes after issues (such as unexpected downtime, upgrades, loss of network connectivity, and many others) have been resolved. In addition, MI link is resilient to SQL MI failovers, or service updates.<br /><br />Certain conditions result in LRS stalling. LRS migration is automatically restarted if migration to the General Purpose service tier is interrupted, but needs to be restarted if a migration to the Business Critical service tier is interrupted. |
| **Reverse migration from SQL MI back to SQL Server** | Offline and online migration back to SQL Server 2022 and above is supported. | Not supported. | MI link is the only solution that offers online and offline reverse migration to SQL Server 2022 and later versions - reverse migration isn't available for older versions of SQL Server. |

## What to choose?

The choice between LRS and the MI link depends on your circumstances and business needs. The notable difference between the migration solutions is performance. LRS has a simpler initial setup, which allows you to migrate quickly. While the initial configuration for MI link is more complex, it provides greater resiliency, security, and flexibility.

Additionally, the cutover time is considerably shorter with MI link, which is a significant advantage for many customers. In fact, the potentially considerable downtime when migrating to the Business Critical service tier with LRS is why the MI link is referred to as the only "true online" migration to the Business Critical service tier.

Finally, if you need your database accessible for read-only workloads on the migration target while the migration is in progress, or if you need to perform a reverse migration back to SQL Server 2022 and later, the MI link is the only option that supports these scenarios.

## Related content

- [Overview of Log Replay Service with Azure SQL Managed Instance](log-replay-service-overview.md)
- [Migrate databases from SQL Server by using Log Replay Service - Azure SQL Managed Instance](log-replay-service-migrate.md)
- [Migrate with the link - Azure SQL Managed Instance](managed-instance-link-migrate.md)
