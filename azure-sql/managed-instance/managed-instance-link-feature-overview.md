---
title: Managed Instance link overview
titleSuffix: Azure SQL Managed Instance
description: This article describes the Managed Instance link, which you can use to replicate data continuously between SQL Server and Azure SQL Managed Instance for scenarios such as migrating to the cloud with minimal downtime, or disaster recovery.
author: djordje-jeremic
ms.author: djjeremi
ms.reviewer: mathoma, randolphwest
ms.date: 03/06/2026
ms.service: azure-sql-managed-instance
ms.subservice: data-movement
ms.topic: concept-article
ms.custom:
  - ignite-2025
---

# Overview of the Managed Instance link

[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

This article provides an overview of the Managed Instance link, which enables near real-time data replication between SQL Server and [Azure SQL Managed Instance](sql-managed-instance-paas-overview.md). The link provides hybrid flexibility and database mobility as it unlocks several scenarios, such as scaling read-only workloads, offloading analytics and reporting to Azure, and migrating to Azure. And, with SQL Server 2022 and later, the link enables online disaster recovery with fail back to SQL Server, as well as configuring the link from SQL Managed Instance to SQL Server. 

To get started, review [prepare your environment for the link](managed-instance-link-preparation.md). 

## Overview

The Managed Instance link uses [distributed availability groups](/sql/database-engine/availability-groups/windows/distributed-availability-groups) to extend your data estate in a safe and secure manner. It replicates data in near real-time from SQL Server hosted anywhere to Azure SQL Managed Instance, or from Azure SQL Managed Instance to SQL Server 2022 or later hosted anywhere. 

The link supports single node and multiple-node SQL Server instances with or without existing availability groups. Through the link, you can use benefits of Azure without migrating your SQL Server data estate to the cloud. 

Though the link supports replication of one database per link, you can replicate multiple databases from a single instance of SQL Server to one or more SQL managed instances, or replicate the same database to multiple SQL managed instances, by configuring multiple links - one link for each database to managed instance pair. 

The link feature currently offers the following functionality:

- **One-way replication from SQL Server versions 2016, 2017 and 2019**: Use the link feature to replicate data one way from SQL instance to Azure SQL Managed Instance. Although you can manually fail over to your managed instance if there's a disaster, doing so breaks the link, and failing back isn't supported. 
- **Disaster recovery (SQL Server 2022 and SQL Server 2025)**: Use the link feature to replicate data between SQL Server 2022 or SQL Server 2025 and SQL Managed Instance, manually fail over to your secondary during a disaster, and fail back to your primary after you mitigate the disaster. Either SQL Server or SQL Managed Instance can be the initial primary. 

You can keep running the link for as long as you need it, for months and even years at a time. And for your modernization journey, if or when you're ready to migrate to Azure, the link enables a considerably improved migration experience. Migration through the link offers minimal downtime compared to all other available migrations options, providing a true online migration to your SQL Managed Instance.

You can use databases that are replicated through the link between SQL Server and Azure SQL Managed Instance for several scenarios, such as: 

- Disaster recovery 
- Using Azure services without migrating to the cloud 
- Offloading read-only workloads to Azure 
- Migrating to Azure
- Copying data on-premises

:::image type="content" source="./media/managed-instance-link-feature-overview/mi-link-main-scenario.svg" alt-text="Diagram that illustrates the main Managed Instance link scenario." :::

<a id="prerequisites"></a>

## Version supportability

Both the General Purpose and Business Critical service tiers of Azure SQL Managed Instance support the Managed Instance link. The link feature works with the Enterprise, Developer, and Standard editions of SQL Server.  

One-way replication from SQL Server to Azure SQL Managed Instance is generally available for every supported SQL Server version. Disaster recovery with two-way replication and failback is supported starting with SQL Server 2022, and is based on the [update policy](update-policy.md) your SQL managed instance is configured with.

The following table lists the functionality of the link feature and the minimum supported SQL Server versions:

| Initial primary version  | Operating system (OS)  |  Disaster recovery options |  Minimum required servicing update |
| --- | --- | --- | --- |
| Azure SQL Managed Instance |  [Windows Server](/sql/sql-server/install/hardware-and-software-requirements-for-installing-sql-server-2022#operating-system-support) and [Linux](/sql/linux/sql-server-linux-setup#supported-platforms) for the secondary SQL Server instance replica |  [Bi-directional](#disaster-recovery) | Configuring a link *from* Azure SQL Managed Instance to, and bidirectional failover with, is supported by:  <br /> - SQL Server 2025 and SQL MI with the [**SQL Server 2025 update policy**](update-policy.md#sql-server-2025-update-policy) <br /> - SQL Server 2022 and SQL MI with the [**SQL Server 2022 update policy**](update-policy.md#sql-server-2022-update-policy)  | 
| SQL Server 2025 (17.x) | [Windows Server](/sql/sql-server/install/hardware-and-software-requirements-for-installing-sql-server-2025#operating-system-support) and [Linux](/sql/linux/sql-server-linux-setup#supported-platforms) |  [Bi-directional](#disaster-recovery)|  [SQL Server 2025 RTM (17.0.1000.7)](/sql/sql-server/sql-server-2025-release-notes) |
| SQL Server 2022 (16.x) | [Windows Server](/sql/sql-server/install/hardware-and-software-requirements-for-installing-sql-server-2022#operating-system-support) and [Linux](/sql/linux/sql-server-linux-setup#supported-platforms) |  [Bi-directional](#disaster-recovery) |  - [SQL Server 2022 RTM (16.0.1000.6)](/sql/sql-server/sql-server-2022-release-notes): Creating a link *from* SQL Server 2022 to SQL MI <br /> -  [SQL Server 2022 CU10 (16.0.4095.4)](/troubleshoot/sql/releases/sqlserver-2022/cumulativeupdate10): Creating a link *from* SQL MI to SQL Server 2022<sup>1</sup> <br /> - [SQL Server 2022 CU13 (16.0.4125.3)](/troubleshoot/sql/releases/sqlserver-2022/cumulativeupdate13): Failing over the link using [Transact-SQL](managed-instance-link-failover-how-to.md?tabs=tsql#fail-over-a-database) | 
| SQL Server 2019 (15.x) | [Windows Server](/sql/sql-server/install/hardware-and-software-requirements-for-installing-sql-server-2019#operating-system-support) and [Linux](/sql/linux/sql-server-linux-setup#supported-platforms) | From SQL Server to SQL MI only | [SQL Server 2019 CU20 (15.0.4312.2)](https://support.microsoft.com/topic/kb5024276-cumulative-update-20-for-sql-server-2019-4b282be9-b559-46ac-9b6a-badbd44785d2) |
| SQL Server 2017 (14.x) | [Windows Server](/sql/sql-server/install/hardware-and-software-requirements-for-installing-sql-server#operating-system-support-for-sql-server-2017) and [Linux](/sql/linux/sql-server-linux-setup#supported-platforms) | From SQL Server to SQL MI only | [SQL Server 2017 CU31 (14.0.3456.2)](/troubleshoot/sql/releases/sqlserver-2017/cumulativeupdate31) and the matching [SQL Server 2017 Azure Connect pack (14.0.3490.10)](/troubleshoot/sql/releases/sqlserver-2017/azureconnect) | 
| SQL Server 2016 (13.x) | [Windows Server only](/sql/sql-server/install/hardware-and-software-requirements-for-installing-sql-server#operating-system-support-for-sql-server-2017) | From SQL Server to SQL MI only| [SQL Server 2016 SP3 (13.0.6300.2)](/troubleshoot/sql/releases/sqlserver-2016/build-versions#sql-server-2016-service-pack-3-sp3-cumulative-update-cu-builds) and the matching [SQL Server 2016 Azure Connect pack (13.0.7000.253)](/troubleshoot/sql/releases/sqlserver-2016/build-versions#sql-server-2016-service-pack-3-sp3-azure-connect-pack-builds) |
| SQL Server 2014 (12.x) and earlier | N/A | N/A | Versions before SQL Server 2016 aren't supported.|

<sup>1</sup> While creating a link with SQL Server 2022 as the initial primary is supported starting with the RTM version of SQL Server 2022, creating a link with Azure SQL Managed Instance as the initial primary is supported only starting with SQL Server 2022 CU10. If you create the link from a SQL Managed Instance initial primary, downgrading SQL Server below CU10 isn't supported while the link is active as it can cause issues after failing over in either direction.   

SQL Server versions prior to SQL Server 2016 (SQL Server 2008 - 2014) aren't supported because the link feature relies on distributed availability group technology, which was introduced in SQL Server 2016. 

In addition to the supported SQL Server version, you need:

- Network connectivity between your SQL Server instance and your managed instance. If SQL Server is running on-premises, use a VPN link or Azure ExpressRoute. If SQL Server is running on an Azure virtual machine (VM), either deploy your VM to the same virtual network as your managed instance or use virtual network peering to connect the two separate subnets. 
- An Azure SQL Managed Instance deployment, provisioned to any service tier.

You also need the following tools:

| Tool | Notes  | 
| --- | --- |
| The latest [SSMS](/ssms/install/install) | SQL Server Management Studio (SSMS) is the easiest way to use the Managed Instance link since it provides wizards that automate link setup. |
| The latest [Az.SQL](https://www.powershellgallery.com/packages/Az.Sql) or [Azure CLI](/cli/azure/install-azure-cli) | For link setup via scripts. |

> [!NOTE]
> The Managed Instance link feature is available in all global Azure regions and national or government clouds.

## How the link works

The link feature for SQL Managed Instance works by creating a distributed availability group between SQL Server and Azure SQL Managed Instance. The solution supports single-node systems with or without existing availability groups, or multiple node systems with existing availability groups.  

:::image type="content" source="./media/managed-instance-link-feature-overview/mi-link-distributed-availability-group.svg" alt-text="Diagram showing how the link feature for SQL Managed Instance works using distributed availability group technology.":::

A private connection such as a VPN or Azure ExpressRoute connects an on-premises network and Azure. If you host SQL Server on an Azure VM, the internal Azure backbone can connect the VM and SQL managed instance, such as with virtual network peering. The two systems establish trust using certificate-based authentication, where SQL Server and SQL Managed Instance exchange public keys of their respective certificates.

Azure SQL Managed Instance supports multiple links from the same or different SQL Server sources to a single Azure SQL Managed Instance. The number of links depends on the number of databases a managed instance can host at the same time - up to 100 links for the General Purpose and Business Critical service tiers, and 500 links for the [Next-gen General Purpose tier upgrade](service-tiers-next-gen-general-purpose-use.md). A single SQL Server instance can create multiple parallel database synchronization links with several SQL managed instances, even in different Azure regions, with a one-to-one relationship between a database and a managed instance. 

## Use the link 

To help you set up the initial environment, see the guide to prepare your SQL Server environment to use the link feature with SQL Managed Instance:

- Prepare environment for the link for [SQL Server 2019 and later](managed-instance-link-preparation.md), or for [SQL Server 2016](managed-instance-link-preparation-wsfc.md)
- Automate preparing your environment for the Managed Instance link by using a downloadable script. For more information, see the [Automating link setup blog](https://techcommunity.microsoft.com/t5/modernization-best-practices-and/automating-the-setup-of-azure-sql-managed-instance-link/ba-p/3696961). 

After you meet the initial environment requirements, create the link by using the automated wizard in SQL Server Management Studio (SSMS), or set up the link manually using scripts: 

- [Configure link with SSMS](managed-instance-link-configure-how-to-ssms.md)
- [Configure link with scripts](managed-instance-link-configure-how-to-scripts.md)

After you create the link, follow best practices to maintain the link:

- [Best practices to maintain the link](managed-instance-link-best-practices.md)

## Disaster recovery

The Managed Instance link enables [disaster recovery](managed-instance-link-disaster-recovery.md), where, in the event of a disaster, you can manually fail over your workload from your primary to your secondary. To get started, review [Disaster recovery with Managed Instance link](managed-instance-link-disaster-recovery.md). 

With SQL Server 2016 to SQL Server 2019, the primary is always SQL Server and failover to the secondary SQL managed instance is one-directional. Failing back to SQL Server isn't supported. However, you can recover your data to SQL Server by using data movement options such as [transactional replication](replication-transactional-overview.md) or [exporting a bacpac](../database/database-export.md). 

With SQL Server 2022 and SQL Server 2025, either SQL Server or SQL Managed Instance (with a matching [update policy](update-policy.md)) can be the initial primary and you can establish the link from either SQL Server or SQL Managed Instance. You can fail back your workloads between the primary and secondary, achieving true two-way disaster recovery. 

When failing back to SQL Server, you can choose to fail back: 
-  _online_ by using the Managed Instance link directly. 
-  _offline_ by taking a backup of your database from SQL Managed Instance and [restoring it to your SQL Server instance](restore-database-to-sql-server.md). 

:::image type="content" source="media/managed-instance-link-feature-overview/disaster-recovery-scenario.png" alt-text="Diagram showing the disaster recovery scenario.":::

## Use Azure services

Use the link feature to take advantage of Azure services by using SQL Server data without migrating it to the cloud. Examples include reporting, analytics, backups, machine learning, and other jobs that send data to Azure. 

## Offload workloads to Azure

You can also use the link feature to offload workloads to Azure. For example, an application could use SQL Server for read/write workloads, while it offloads read-only workloads to SQL Managed Instance deployments in any Azure region worldwide. After the link is established, the primary database on SQL Server is read/write accessible, while replicated data to your SQL managed instance in Azure is read-only accessible. This arrangement allows for various scenarios where replicated databases on your SQL managed instance can be used for read scale-out and offloading read-only workloads to Azure. Your SQL managed instance, in parallel, can also host independent read/write databases, which also allows copying the replicated database to another read/write database on the same SQL managed instance for further data processing.

To offload your workload to your SQL managed instance, connect your application to the replicated database and direct read-only queries to it. You can connect by using one of the following endpoints:

- The [VNet-local endpoint](/azure/azure-sql/managed-instance/connectivity-architecture-overview#vnet-local-endpoint), which is accessible from within the virtual network that hosts your SQL managed instance or from peered networks. This is the recommended approach when your application runs in Azure or connects through a VPN or ExpressRoute.
- The [public endpoint](/azure/azure-sql/managed-instance/connectivity-architecture-overview#public-endpoint), which is accessible over the internet. Use this approach when your application needs to connect from outside the virtual network and peering or private endpoints aren't available. The public endpoint carries client traffic only and can't be used for the link's data replication, which always uses the VNet-local endpoint.

The link is database scoped (one link per one database), allowing for consolidation and deconsolidation of workloads in Azure. For example, you can replicate databases from multiple SQL Server instances to a single SQL Managed Instance deployment in Azure (consolidation), or you can replicate databases from a single SQL Server instance to multiple managed instances via a one-to-one relationship between a database and a managed instance, to any Azure region worldwide (deconsolidation). The latter option provides you with an efficient way to quickly bring your workloads closer to your customers in any region worldwide, which you can use as read-only replicas.

## Migrate to Azure

The link feature also facilitates migrating from SQL Server to SQL Managed Instance, which enables: 

- The most performant, minimal downtime migration, compared to all other solutions available today.
- True online migration to SQL Managed Instance in any service tier. 

Because the link feature enables minimal downtime migration, you can migrate to your managed instance as you maintain your primary workload online. Although it's currently possible to achieve online migrations to the *General Purpose* service tier with other solutions, the link feature is the only solution that allows true online migrations to the *Business Critical* service tier. For an in-depth migration comparison between migrating with the link and the Log Replay Service, see [Compare the Managed Instance link to LRS](log-replay-service-compare-mi-link.md).

> [!NOTE]
> You can now migrate your SQL Server instance enabled by Azure Arc to Azure SQL Managed Instance directly through the Azure portal. For more information, see [Migrate to Azure SQL Managed Instance](/sql/sql-server/azure-arc/migrate-to-azure-sql-managed-instance).

## Copy data on-premises

With SQL Server 2022 and later, you can establish your link from SQL Managed Instance to SQL Server, unlocking additional scenarios, such as creating a near real-time database replica outside of Azure, testing business continuity plans, and meeting compliance requirements. 

## Automated backups

After you configure a link with Azure SQL Managed Instance, databases on the SQL managed instance are automatically backed up to Azure storage whether or not SQL Managed Instance is primary. Automated backups with the link take full and transaction log backups, but not differential backups, which can lead to longer restore times. 

You can reduce your on-premises management and operation costs while enjoying the reliability of Azure backups for your replicated databases. You can then perform a [point-in-time restore](point-in-time-restore.md) of your replicated database to any SQL Managed Instance deployment in the same region, as with any other [automated backup](automated-backups-overview.md). 

## License-free passive DR replica

You can save on vCore licensing costs if you activate the [hybrid failover benefit](business-continuity-high-availability-disaster-recover-hadr-overview.md#license-free-dr-replicas) for secondary passive disaster recovery only SQL managed instances that don't have any workloads. 

To get started, review [License-free passive replica](managed-instance-link-disaster-recovery.md#license-free-passive-dr-replica). 

### Cost benefit

If you designate a managed instance replica for disaster recovery only, Microsoft doesn't charge you SQL Server licensing costs for the vCores that the secondary instance uses. The instance is billed at an hour granularity, and you might still be charged licensing costs for a full hour if you update the licensing benefit during the hour. 

The benefit works differently for the pay-as-you-go billing model and the [Azure Hybrid Benefit](../azure-hybrid-benefit.md). For a pay-as-you-go billing model, the vCores are discounted on your invoice. If you use the Azure Hybrid Benefit for the passive replica, the number of vCores that the secondary replica uses are returned to your pool of licenses.

For example, as a pay-as-you-go customer, if you have 16 vCores assigned to the secondary instance, a discount for 16 vCores appears on your invoice if you designate your secondary instance for hybrid failover.

In another example, if you have 16 Azure Hybrid Benefit licenses and your secondary SQL managed instance uses 8 vCores, after you designate the secondary instance for hybrid failover, 8 vCores are returned to your license pool for you to use with other Azure SQL deployments.

For precise terms and conditions of the Hybrid failover rights benefit, see the SQL Server licensing terms online in the [SQL Server – Fail-over Rights](https://www.microsoft.com/licensing/terms/productoffering/SQLServer/EAEAS) section.

## Limitations

Consider the following limitations when you're using the link.

Version supportability limitations include:

- You can't use Windows 10 and 11 clients to host your SQL Server instance, because it's not possible to enable the Always On availability group feature that's required for the link. You must host SQL Server instances on Windows Server 2012 or later.
- The link feature doesn't support SQL Server versions 2008 to 2014, because the SQL engine of these releases doesn't have built-in support for distributed availability groups required for the link. Upgrade to a newer version of SQL Server to use the link.
- Data replication and failover *from* SQL Managed Instance to SQL Server 2022 or SQL Server 2025 is not supported by instances configured with the **Always-up-to-date** update policy. Your instance must be configured with the corresponding SQL Server 2022 or SQL Server 2025 [update policy](update-policy.md) to do the following: 
    - Establish a link _from_ SQL Managed Instance _to_ SQL Server.
    - Fail over from SQL Managed Instance to SQL Server.
- While you can establish a link from SQL Server 2022 or SQL Server 2025 to a SQL managed instance configured with the [Always-up-to-date update policy](update-policy.md#always-up-to-date-update-policy), after failover to SQL Managed Instance, you can't replicate data, or fail back, to SQL Server.

Data replication limitations include:

- You can replicate only user databases. Replication of system databases isn't supported.
- The solution doesn't replicate server-level objects, agent jobs, or user logins from SQL Server to SQL Managed Instance.
- For SQL Server versions 2016, 2017 and 2019, replication of user databases from SQL Server instances to SQL Managed Instance deployments is one way. You can't replicate user databases from SQL Managed Instance deployments back to SQL Server instances via the link. Two-way replication with failback to a SQL Server instance is available only for SQL Server 2022 or SQL Server 2025 when SQL Managed Instance is configured with the corresponding [update policy](update-policy.md).
- Configuring a link from SQL Managed Instance to SQL Server is unsupported for SQL Managed Instance databases that are already linked. 

Configuration limitations include: 

- If there are multiple SQL Server instances on a server, you can configure a link for each instance, but you must configure each instance to use a separate database mirroring endpoint, with a dedicated port per instance. Only the default instance should use port 5022 for the database mirroring endpoint. 
- You can place only one database into a single availability group for one Managed Instance link. However, you can replicate multiple databases in a single SQL Server instance by establishing multiple links. 
  
   > [!NOTE]
   > If you're interested in participating in a limited preview of a change to this behavior, please fill out the following [form](https://aka.ms/milink-multidb-prpr).

- You can create a link with an existing availability group with a single database. If your existing availability group has multiple databases, you can create a link with the availability group only if you remove all databases except one from the availability group.
- A single General Purpose or Business Critical SQL Managed Instance supports up to 100 links, and a single Next-gen General Purpose SQL Managed Instance supports up to 500 links, from the same, or from multiple SQL Server sources.
- A Managed Instance link can replicate a database of any size if it fits into the chosen storage size of the target SQL Managed Instance deployment.
- Managed Instance link authentication between SQL Server and SQL Managed Instance is certificate-based and available only through an exchange of certificates. You can't use Windows authentication to establish the link between the SQL Server instance and the SQL managed instance.
- You can establish a link with only a [VNet-local endpoint](connectivity-architecture-overview.md#vnet-local-endpoint) to SQL Managed Instance. 
- You can't use public endpoint or private endpoints to establish the link with the managed instance.
- You can't replicate databases with multiple log files, because SQL Managed Instance doesn't support multiple log files.

Feature limitations include:

- You can't use [failover groups](failover-group-sql-mi.md) with instances that use the link feature. You can't establish a link on a SQL managed instance that's part of a failover group, and conversely, you can't configure a failover group on an instance that has a link established.
- If you're using Change Data Capture (CDC), log shipping, or a service broker with databases that are replicated on the SQL Server instance, when the database is migrated to a SQL Managed Instance deployment, during a failover to Azure, clients need to connect by using the instance name of the current global primary replica. You need to manually reconfigure these settings. 
- If you're using [transactional replication](/sql/relational-databases/replication/transactional/transactional-replication) on a database with an established link, consider the following: 
   - The linked database on the secondary replica can't be a Publisher in a transactional replication topology.
   - If you're migrating a database that is configured as a Publisher in a transactional replication topology by using the link, you must reconfigure the database as a Publisher on the target instance after the migration is complete.
- If you're using distributed transactions with a database that's replicated from the SQL Server instance and, in a migration scenario, on the cutover to the cloud, Distributed Transaction Coordinator capabilities won't be transferred. It's not possible for the migrated database to get involved in distributed transactions with the SQL Server instance, because the SQL Managed Instance deployment doesn't support distributed transactions with SQL Server at this time. For reference, SQL Managed Instance today supports distributed transactions only between other managed instances. For more information, see [Distributed transactions across cloud databases](../database/elastic-transactions-overview.md#transactions-for-sql-managed-instance).
- If you're using Transparent Data Encryption (TDE) to encrypt SQL Server databases, you need to export the database encryption key from SQL Server and upload it to Azure Key Vault, and you need to also configure the BYOK TDE option on SQL Managed Instance before creating the link.
- If [accelerated database recovery](/sql/relational-databases/accelerated-database-recovery-concepts) is disabled on your source SQL Server 2019 and later instances, you can no longer enable it after migrating to Azure SQL Managed Instance. Additionally, if the persistent version store (PVS) isn't set to `PRIMARY`, you can experience issues with restore operations on the target SQL managed instance.
- If [Service Broker](/sql/database-engine/configure-windows/sql-server-service-broker) is disabled on the source SQL Server instance, you can't use Service Broker on the target SQL managed instance after migration.
- You can't link SQL Managed Instance databases that are encrypted with service-managed TDE keys to SQL Server. You can link an encrypted database to SQL Server only if you encrypted it with a customer-managed key and the destination server has access to the same key that's used to encrypt the database. For more information, see [Set up SQL Server TDE with Azure Key Vault](/sql/relational-databases/security/encryption/setup-steps-for-extensible-key-management-using-the-azure-key-vault). 
- You can't establish a link between SQL Server and SQL Managed Instance if the functionality that you use on the SQL Server instance isn't supported on the SQL managed instance. For example: 
    - You can't replicate databases with file tables and file streams, because SQL Managed Instance doesn't support file tables or file streams.
    - You can replicate databases that use In-Memory OLTP only to SQL Managed Instance in the *Business Critical* service tier, because the *General Purpose* service tier doesn't support In-Memory OLTP. SQL Managed Instance doesn't support databases with multiple In-Memory OLTP files and you can't replicate them.

Trying to add an unsupported functionality to a replicated database in: 
   - SQL Server 2017, 2019 and 2022 fails with an error. 
   - SQL Server 2016 results in breaking the link, which you then need to delete and recreate.

For the full list of differences between SQL Server and SQL Managed Instance, see [T-SQL differences between SQL Server and Azure SQL Managed Instance](./transact-sql-tsql-differences-sql-server.md).

## Related content

To use the link: 
- [Prepare environment for the Managed Instance link](./managed-instance-link-preparation.md)
- [Configure link between SQL Server and SQL Managed instance with SSMS](managed-instance-link-configure-how-to-ssms.md)
- [Configure link between SQL Server and SQL Managed instance with scripts](managed-instance-link-configure-how-to-scripts.md)
- [Fail over the link](managed-instance-link-failover-how-to.md)
- [Migrate with the link](managed-instance-link-migrate.md)
- [Best practices for maintaining the link](managed-instance-link-best-practices.md)
- [Troubleshoot issues with the link](managed-instance-link-troubleshoot-how-to.md)

To learn more about the link: 
- [Disaster recovery with Managed Instance link](managed-instance-link-disaster-recovery.md)

For other replication and migration scenarios, consider:

- [Transactional replication with SQL Managed Instance](replication-transactional-overview.md)
- [Log Replay Service (LRS)](log-replay-service-overview.md)
- [Modifiable configuration reference for Azure SQL Managed Instance](modifiable-configuration-reference.md)
