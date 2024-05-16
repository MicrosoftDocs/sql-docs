---
title: Managed Instance link feature overview
titleSuffix: Azure SQL Managed Instance
description: This article describes the link feature of Azure SQL Managed Instance, which you can use to replicate data continuously from SQL Server to the cloud or migrate your SQL Server databases with minimal downtime.
author: danimir
ms.author: danil
ms.reviewer: mathoma, randolphwest
ms.date: 11/14/2023
ms.service: sql-managed-instance
ms.subservice: data-movement
ms.topic: conceptual
ms.custom: ignite-2023
---

# Overview of the Managed Instance link

[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

This article provides an overview of the Managed Instance link feature, which enables near real-time data replication between SQL Server and Azure SQL Managed Instance. The link provides hybrid flexibility and database mobility as it unlocks several scenarios, such as scaling read-only workloads, offloading analytics and reporting to Azure, and migrating to Azure. And, with SQL Server 2022, the link enables online disaster recovery with fail back to SQL Server (currently in preview), as well as configuring the link from SQL Managed Instance to SQL Server 2022 (also in preview). 

## Overview

The Managed Instance link uses [distributed availability groups](/sql/database-engine/availability-groups/windows/distributed-availability-groups) to extend your data estate in a safe and secure manner, replicating data in near real-time from SQL Server hosted anywhere to Azure SQL Managed Instance, or from Azure SQL Managed Instance to SQL Server 2022 hosted anywhere. 

The link supports single node and multiple-node SQL Server instances with or without existing availability groups. Through the link, you can use benefits of Azure without migrating your SQL Server data estate to the cloud. 

Though the link supports replication of one database per link, it's possible to replicate multiple databases from a single instance of SQL Server to one or more SQL managed instances, or replicate the same database to multiple SQL managed instances, by configuring multiple links - one link for each database to managed instance pair. 

The link feature currently offers the following functionality:

- **One-way replication from SQL Server versions 2016 and 2019**: Use the link feature to replicate data one way from SQL instance to Azure SQL Managed Instance. Although you can manually fail over to your managed instance if there's a disaster, doing so breaks the link, and failing back isn't supported. 
- **Disaster recovery (SQL Server 2022)**: Use the link feature to replicate data between SQL Server 2022 and SQL Managed Instance, manually fail over to your secondary during a disaster, and fail back to your primary after you've mitigated the disaster. Either SQL Server or SQL Managed Instance can be the initial primary. This feature is currently in preview. 

You can keep running the link for as long as you need it, for months and even years at a time. And for your modernization journey, if or when you're ready to migrate to Azure, the link enables a considerably improved migration experience. Migration through the link offers minimal downtime compared to all other available migrations options, providing a true online migration to your SQL Managed Instance.

Databases that are replicated through the link between SQL Server and Azure SQL Managed Instance can be used for several scenarios, such as: 

- Disaster recovery 
- Using Azure services without migrating to the cloud 
- Offloading read-only workloads to Azure 
- Migrating to Azure
- Copying data on-premises

:::image type="content" source="./media/managed-instance-link-feature-overview/mi-link-main-scenario.svg" alt-text="Diagram that illustrates the main Managed Instance link scenario." lightbox="./media/managed-instance-link-feature-overview/mi-link-main-scenario.svg":::

## <a id="prerequisites"></a>Version supportability

The Managed Instance link is supported on both the General Purpose and Business Critical service tier of Azure SQL Managed Instance. The link feature works with the Enterprise, Developer, and Standard editions of SQL Server. 

The following table lists the functionality of the link feature and the minimum supported SQL Server versions:

| Initial primary version  | Operating system (OS)  | One-way replication |  Disaster recovery options |  Servicing update requirement |
| --- | --- | --- | --- | --- |
| Azure SQL Managed Instance | Windows Server and Linux | Preview |  [Bi-directional preview](#disaster-recovery) |  [SQL Server 2022 CU10 (KB5031778)](/troubleshoot/sql/releases/sqlserver-2022/cumulativeupdate10) <sup>1</sup> | 
| SQL Server 2022 (16.x) | Windows Server and Linux |  Generally available |  [Bi-directional](#disaster-recovery): <br /> Offline (Generally available) <br /> Online (preview) |  SQL Server 2022 RTM | 
| SQL Server 2019 (15.x) | Windows Server only | Generally available | From SQL Server to SQL MI only | [SQL Server 2019 CU20 (KB5024276)](https://support.microsoft.com/topic/kb5024276-cumulative-update-20-for-sql-server-2019-4b282be9-b559-46ac-9b6a-badbd44785d2) |
| SQL Server 2017 (14.x) | N/A | N/A | N/A| N/A | 
| SQL Server 2016 (13.x) | Windows Server only | Generally available | From SQL Server to SQL MI only|   [SQL Server 2016 SP3 (KB 5003279)](https://support.microsoft.com/help/5003279) and [SQL Server 2016 Azure Connect pack (KB 5014242)](https://support.microsoft.com/help/5014242) |

<sup>1</sup> While creating a link with SQL Server 2022 as the initial primary is supported starting with the RTM version of SQL Server 2022, creating a link with Azure SQL Managed Instance as the initial primary is supported starting with SQL Server 2022 CU10. If you create the link from a SQL Managed Instance initial primary, downgrading SQL Server below CU10 isn't supported while the link is active as it can cause issues after failing over in either direction.

SQL Server versions prior to SQL Server 2016 (SQL Server 2008 - 2014) aren't supported because the link feature relies on distributed availability group technology, which was introduced in SQL Server 2016. 

In addition to the supported SQL Server version, you need:

- Network connectivity between your SQL Server instance and your managed instance. If SQL Server is running on-premises, use a VPN link or Azure ExpressRoute. If SQL Server is running on an Azure virtual machine (VM), either deploy your VM to the same virtual network as your managed instance or use virtual network peering to connect the two separate subnets. 
- An Azure SQL Managed Instance deployment, provisioned to any service tier.

You'll also need the following tools:

| Tool | Notes  | 
| --- | --- |
| [SSMS 19.2](/sql/ssms/download-sql-server-management-studio-ssms) or later | SQL Server Management Studio (SSMS) is the easiest way to use the Managed Instance link since it provides wizards that automate link setup. |
| [Az.SQL 3.9.0](https://www.powershellgallery.com/packages/Az.Sql) or later | A PowerShell module is required for manual configuration steps. |

> [!NOTE]
> The Managed Instance link feature is available in all public Azure regions and national or government clouds.

## How the link works

The underlying technology behind the link feature for SQL Managed Instance is based on creating a distributed availability group between SQL Server and Azure SQL Managed Instance. The solution supports single-node systems with or without existing availability groups, or multiple node systems with existing availability groups.  

:::image type="content" source="./media/managed-instance-link-feature-overview/mi-link-distributed-availability-group.svg" alt-text="Diagram showing how the link feature for SQL Managed Instance works using distributed availability group technology." lightbox="./media/managed-instance-link-feature-overview/mi-link-distributed-availability-group.svg":::

Private connection such as a VPN or Azure ExpressRoute is used between an on-premises network and Azure. If SQL Server is hosted on an Azure VM, the internal Azure backbone can be used between the VM and managed instance – such as, for example, virtual network peering. Trust between the two systems is established using certificate-based authentication, in which SQL Server and SQL Managed Instance exchange public keys of their respective certificates.

There can be up to 100 links from the same or various SQL Server sources to a single Azure SQL Managed Instance. This limit is governed by the number of databases that can be hosted on a managed instance at the same time. Likewise, a single SQL Server instance can establish multiple parallel database synchronization links with several managed instances in different Azure regions in a one-to-one relationship between a database and a managed instance. 

## Use the link 

To help you set up the initial environment, review the guide to prepare your SQL Server environment to use the link feature with SQL Managed Instance:

- Prepare environment for the link for [SQL Server 2019 and later](managed-instance-link-preparation.md), or for [SQL Server 2016](managed-instance-link-preparation-wsfc.md)
- It's possible to automate preparing your environment for the Managed Instance link by using a downloadable script. Review the [Automating link setup blog](https://techcommunity.microsoft.com/t5/modernization-best-practices-and/automating-the-setup-of-azure-sql-managed-instance-link/ba-p/3696961) to learn more. 

After you've ensured initial environment requirements are met, you can create the link by using the automated wizard in SQL Server Management Studio (SSMS), or you can choose to set up the link manually using scripts: 

- [Configure link with SSMS](managed-instance-link-configure-how-to-ssms.md)
- [Configure link with scripts](managed-instance-link-configure-how-to-scripts.md)

After the link is created, follow best practices to maintain the link:

- [Best practices to maintain the link](managed-instance-link-best-practices.md)

## Disaster recovery

The Managed Instance link enables [disaster recovery](managed-instance-link-disaster-recovery.md), where, in the event of a disaster, you can manually fail over your workload from your primary to your secondary. To get started, review [Disaster recovery with Managed Instance link](managed-instance-link-disaster-recovery.md). 

With SQL Server 2016, and SQL Server 2019, the primary is always SQL Server and failover to the secondary managed instance is one-directional. Failing back to SQL Server isn't supported. However, it's possible to recover your data to SQL Server using data movement options such as [transactional replication](replication-transactional-overview.md) or [exporting a bacpac](../database/database-export.md). 

With SQL Server 2022, either SQL Server or SQL Managed Instance can be the initial primary and you can establish the link from either SQL Server or SQL Managed Instance. You can fail back your workloads between the primary and secondary, achieving true two-way disaster recovery. 

When failing back to SQL Server, you can choose to fail back: 
-  _online_ by using the Managed Instance link directly. This option is currently in a preview. 
-  _offline_ by taking a backup of your database from SQL Managed Instance and [restoring it to your SQL Server 2022 instance](restore-database-to-sql-server.md). This option is generally available.

:::image type="content" source="media/managed-instance-link-feature-overview/disaster-recovery-scenario.png" alt-text="Diagram showing the disaster recovery scenario.":::

## Use Azure services

Use the link feature to take advantage of Azure services by using SQL Server data without migrating it to the cloud. Examples include reporting, analytics, backups, machine learning, and other jobs that send data to Azure. 

## Offload workloads to Azure

You can also use the link feature to offload workloads to Azure. For example, an application could use SQL Server for read/write workloads, while it offloads read-only workloads to SQL Managed Instance deployments in any Azure region worldwide. After the link is established, the primary database on SQL Server is read/write accessible, while replicated data to your managed instance in Azure is read-only accessible. This arrangement allows for various scenarios where replicated databases on your managed instance can be used for read scale-out and offloading read-only workloads to Azure. Your managed instance, in parallel, can also host independent read/write databases. This allows for copying the replicated database to another read/write database on the same managed instance for further data processing.

The link is database scoped (one link per one database), allowing for consolidation and deconsolidation of workloads in Azure. For example, you can replicate databases from multiple SQL Server instances to a single SQL Managed Instance deployment in Azure (consolidation), or you can replicate databases from a single SQL Server instance to multiple managed instances via a one-to-one relationship between a database and a managed instance, to any Azure region worldwide (deconsolidation). The latter option provides you with an efficient way to quickly bring your workloads closer to your customers in any region worldwide, which you can use as read-only replicas.

## Migrate to Azure

The link feature also facilitates migrating from SQL Server to SQL Managed Instance, which enables: 

- The most performant, minimal downtime migration, compared to all other solutions available today.
- True online migration to SQL Managed Instance in any service tier. 

Because the link feature enables minimal downtime migration, you can migrate to your managed instance as you maintain your primary workload online. Although it's currently possible to achieve online migrations to the *General Purpose* service tier with other solutions, the link feature is the only solution that allows true online migrations to the *Business Critical* tier. 

## Copy data on-premises

With SQL Server 2022, you can establish your link from SQL Managed Instance to SQL Server, unlocking additional scenarios, such as creating a near real-time database replica outside of Azure, testing business continuity plans, and meeting compliance requirements. Establishing a link from SQL Managed Instance to SQL Server 2022 is currently in preview. 

## Automated backups

After your databases are replicated to your Azure SQL Managed Instance, they're automatically backed up to Azure storage. You can reduce your on-premises management and operation costs while enjoying the reliability of Azure backups for your replicated databases. You can then perform a [point-in-time restore](point-in-time-restore.md) of your replicated database to any SQL Managed Instance deployment in the same region, as with any other [automated backup](automated-backups-overview.md). 


## License-free passive DR replica

You can save on vCore licensing costs if you activate the [hybrid failover benefit](business-continuity-high-availability-disaster-recover-hadr-overview.md#license-free-dr-replicas) for secondary passive disaster recovery only SQL managed instances that don't have any workloads. 

To get started, review [License-free passive replica](managed-instance-link-disaster-recovery.md#license-free-passive-dr-replica). 

### Cost benefit

If you designate a managed instance replica for disaster recovery only, Microsoft doesn't charge you SQL Server licensing costs for the vCores that the secondary instance uses. Be aware that the instance is billed at an hour granularity, and you might still be charged licensing costs for a full hour if you update the licensing benefit during the hour. 

The benefit reflects differently for the pay-as-you-go billing model and the [Azure Hybrid Benefit](../azure-hybrid-benefit.md). For a pay-as-you-go billing model, the vCores are discounted on your invoice. If you use the Azure Hybrid Benefit for the passive replica, the number of vCores that the secondary replica uses are returned to your pool of licenses.

For example, as a pay-as-you-go customer, if you have 16 vCores assigned to the secondary instance, a discount for 16 vCores appears on your invoice if you designate your secondary instance for hybrid failover.

In another example, if you have 16 Azure Hybrid Benefit licenses and your secondary SQL managed instance uses 8 vCores, after you designate the secondary instance for hybrid failover, 8 vCores are returned to your license pool for you to use with other Azure SQL deployments.

For precise terms and conditions of the Hybrid failover rights benefit, see the SQL Server licensing terms online in the [“SQL Server – Fail-over Rights”](https://www.microsoft.com/licensing/terms/productoffering/SQLServer/EAEAS) section.


## Limitations

Consider the following limitations when you're using the link.

Version supportability limitations include:

- You can't use Windows 10 and 11 clients to host your SQL Server instance, because it's not possible to enable the Always On availability group feature that's required for the link. SQL Server instances must be hosted on Windows Server 2012 or later.
- SQL Server versions 2008 to 2014 aren't supported by the link feature, as the SQL engine of these releases doesn't have built-in support for distributed availability groups required for the link. Upgrade to a newer version of SQL Server to use the link.
- The following link capabilities are only supported between SQL Server 2022 and SQL managed instances with the [SQL Server 2022 update policy](update-policy.md#sql-server-2022-update-policy): 
    - Establishing a link _from_ SQL Managed Instance _to_ SQL Server. 
    - Failing over from SQL Managed Instance to SQL Server 2022. 
- While you can establish a link from SQL Server 2022 to a SQL managed instance configured with the [Always-up-to-date update policy](update-policy.md#always-up-to-date-update-policy), after fail over to SQL Managed Instance, you will no longer be able to replicate data or fail back to SQL Server 2022. 


Data replication limitations include:

- Only user databases can be replicated. Replication of system databases isn't supported.
- The solution doesn't replicate server-level objects, agent jobs, or user logins from SQL Server to SQL Managed Instance.
- For SQL Server versions 2016 and 2019, replication of user databases from SQL Server instances to SQL Managed Instance deployments is one way. User databases from SQL Managed Instance deployments can't be replicated back to SQL Server instances. Two-way replication with failback to a SQL Server instance is available only for SQL Server 2022.
- Configuring a link from SQL Managed Instance to SQL Server on a database is unsupported for SQL Managed Instance databases that are already linked. 

Configuration limitations include: 

  - If there are multiple SQL Server instances on a server, it's possible to configure a link with each instance, but each instance must be configured to use a separate database mirroring endpoint, with a dedicated port per instance. Only the default instance should use port 5022 for the database mirroring endpoint. 
  - Only one database can be placed into a single availability group for one Managed Instance link. However, it's possible to replicate multiple databases in a single SQL Server instance by establishing multiple links. 
  - A single managed instance supports up to 100 links from multiple SQL Server instances. 
  - A Managed Instance link can replicate a database of any size if it fits into the chosen storage size of the target SQL Managed Instance deployment.
  - Managed Instance link authentication between SQL Server and SQL Managed Instance is certificate-based and available only through an exchange of certificates. Using Windows authentication to establish the link between the SQL Server instance and the managed instance isn't supported.
  - Only [VNet-local endpoint](connectivity-architecture-overview.md#vnet-local-endpoint) is supported to establish a link with SQL Managed Instance. 
  - You can't use public endpoint or private endpoints to establish the link with the managed instance.
  - Databases with multiple log files can't be replicated, because SQL Managed Instance doesn't support multiple log files.

Feature limitations include:

- [Failover groups](failover-group-sql-mi.md) aren't supported with instances that use the link feature. You can't establish a link on a managed instance that's part of a failover group, and conversely, you can't configure a failover group on an instance that has a link established.
- If you're using Change Data Capture (CDC), log shipping, or a service broker with databases that are replicated on the SQL Server instance, when the database is migrated to a SQL Managed Instance deployment, during a failover to Azure, clients need to connect by using the instance name of the current global primary replica. These settings should be manually reconfigured. 
- If you're using transactional replication with a database on a SQL Server instance in a migration scenario, during failover to Azure, transactional replication on the SQL Managed Instance deployment will fail and should be manually reconfigured. 
- If you're using distributed transactions with a database that's replicated from the SQL Server instance and, in a migration scenario, on the cutover to the cloud, Distributed Transaction Coordinator capabilities won't be transferred. It's not possible for the migrated database to get involved in distributed transactions with the SQL Server instance, because the SQL Managed Instance deployment doesn't support distributed transactions with SQL Server at this time. For reference, SQL Managed Instance today supports distributed transactions only between other managed instances. For more information, see [Distributed transactions across cloud databases](../database/elastic-transactions-overview.md#transactions-for-sql-managed-instance).
- If you're using Transparent Data Encryption (TDE) to encrypt SQL Server databases, the database encryption key from SQL Server needs to be exported and uploaded to Azure Key Vault, and you need to also configure the BYOK TDE option on SQL Managed Instance before creating the link.
- SQL Managed Instance databases that are encrypted with service-managed TDE keys can't be linked to SQL Server. You can link an encrypted database to SQL Server only if it was encrypted with a customer-managed key and the destination server has access to the same key that's used to encrypt the database. For more information, see [Set up SQL Server TDE with Azure Key Vault](/sql/relational-databases/security/encryption/setup-steps-for-extensible-key-management-using-the-azure-key-vault). 
- You can't establish a link between SQL Server and SQL Managed Instance if the functionality that's used on the SQL Server instance isn't supported on the managed instance. For example: 
    - Databases with file tables and file streams can't be replicated, because SQL Managed Instance doesn't support file tables or file streams.
    - Databases that use In-Memory OLTP can be replicated only to SQL Managed Instance in the*Business Critical* service tier, because the *General Purpose* service tier doesn't support In-Memory OLTP. Databases with multiple In-Memory OLTP files are not supported by SQL Managed Instance and can't be replicated.

Trying to add an unsupported functionality to a replicated database in: 
   - SQL Server 2019 and 2022 fails with an error. 
   - SQL Server 2016 results in breaking the link, which will then need to be deleted and recreated. 

For the full list of differences between SQL Server and SQL Managed Instance, see [T-SQL differences between SQL Server and Azure SQL Managed Instance](./transact-sql-tsql-differences-sql-server.md).

## Related content

- [Managed Instance link: Connecting SQL Server to Azure reimagined](https://aka.ms/mi-link-techblog)
- [Prepare environment for the Managed Instance link](./managed-instance-link-preparation.md)
- [Configure link between SQL Server and SQL Managed instance with SSMS](managed-instance-link-configure-how-to-ssms.md)
- [Configure link between SQL Server and SQL Managed instance with scripts](managed-instance-link-configure-how-to-scripts.md)
- [Disaster recovery with Managed Instance link](managed-instance-link-disaster-recovery.md)
- [Best practices for maintaining the link](managed-instance-link-best-practices.md)

For other replication and migration scenarios, consider:

- [Transactional replication with SQL Managed Instance](replication-transactional-overview.md)
- [Log Replay Service (LRS)](log-replay-service-overview.md)
