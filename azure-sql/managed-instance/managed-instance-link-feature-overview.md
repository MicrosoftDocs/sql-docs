---
title: Managed Instance link feature overview
titleSuffix: Azure SQL Managed Instance
description: This article describes the link feature of Azure SQL Managed Instance, which you can use to replicate data continuously from SQL Server to the cloud or migrate your SQL Server databases with minimal downtime.
author: danimir
ms.author: danil
ms.reviewer: mathoma, randolphwest
ms.date: 04/26/2023
ms.service: sql-managed-instance
ms.subservice: data-movement
ms.topic: conceptual
ms.custom: ignite-fall-2021
---

# Overview of the Managed Instance link feature

[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

This article provides an overview of the Managed Instance link feature, which enables near real-time data replication from SQL Server to Azure SQL Managed Instance. The link provides hybrid flexibility and database mobility as it unlocks several scenarios, such as scaling read-only workloads, offloading analytics and reporting to Azure, and migrating to the cloud. And, with SQL Server 2022, the link feature enables disaster recovery. 

If you have product improvement suggestions or comments, or you want to report issues, contact our team through [Managed Instance link user feedback](https://aka.ms/mi-link-feedback).

> [!NOTE]
> It's possible to automate preparing your environment for the Managed Instance link by using a downloadable script. Review the [Automating link setup blog](https://techcommunity.microsoft.com/t5/modernization-best-practices-and/automating-the-setup-of-azure-sql-managed-instance-link/ba-p/3696961) to learn more. 

## Overview

The Managed Instance link feature uses distributed availability groups to extend your SQL Server on-premises Always On availability group hosted anywhere to Azure SQL Managed Instance in a safe and secure manner, replicating data in near real-time. 

The link supports single node SQL Server instances without existing availability groups, and also multiple-node SQL Server instances with existing availability groups. Through the link, you can use the latest benefits of Azure without migrating your entire SQL Server data estate to the cloud. 

Though the link supports replicating one database per link, it's possible to replicate multiple databases from a single instance of SQL Server to a single SQL managed instance by configuring multiple links - one link for each database you want to replicate. 

The link feature currently offers the following functionality:

- **One-way replication (SQL Server versions 2016 and 2019)**: Use the link feature to replicate data one way from a SQL Server instance to your managed instance. Although you can manually fail over to your managed instance if there's a disaster, doing so breaks the link, and failing back isn't supported. 
- **Disaster recovery (SQL Server 2022)**: Use the link feature to replicate data from a SQL Server 2022 instance to your managed instance, manually fail over to your managed instance during a disaster, and fail back to the SQL Server instance after you've mitigated the disaster.  

    This feature is currently in limited public preview. [You must sign up for limited public preview](https://aka.ms/mi-link-dr-preview-signup) so that the product group can configure your environment for the preview. 

You can keep running the link for as long as you need it, for months and even years at a time. And for your modernization journey, if or when you're ready to migrate to Azure, the link enables a considerably improved migration experience. It offers minimal downtime, compared to all other options available today, and it provides a true online migration to your SQL Managed Instance deployment.

## Prerequisites

The Managed Instance link is supported on both the General Purpose and Business Critical service tier of Azure SQL Managed Instance. The link feature works with the Enterprise, Developer, and Standard editions of SQL Server. 

The following table lists the functionality of the link feature and the supported SQL Server versions:

| SQL Server version  | Operating system (OS)  | One-way replication  (for migration & DR) |  Disaster recovery with fail back | Servicing update requirement |
| --- | --- | --- | --- | --- |
| SQL Server 2022 (16.x) | Windows Server and Linux |  Generally available | [Must sign up for limited public preview](https://aka.ms/mi-link-dr-preview-signup)  | SQL Server 2022 RTM | 
| SQL Server 2019 (15.x) | Windows Server only | Generally available |N/A | [SQL Server 2019 CU20 (KB5024276)](https://support.microsoft.com/topic/kb5024276-cumulative-update-20-for-sql-server-2019-4b282be9-b559-46ac-9b6a-badbd44785d2) |
| SQL Server 2017 (14.x) | N/A | N/A | N/A| N/A | 
| SQL Server 2016 (13.x) | Windows Server only | Generally available | N/A|   [SQL Server 2016 SP3 (KB 5003279)](https://support.microsoft.com/help/5003279) and [SQL Server 2016 Azure Connect pack (KB 5014242)](https://support.microsoft.com/help/5014242) |

SQL Server versions prior to SQL Server 2016 (SQL Server 2008 - 2014) aren't supported because the link feature relies on distributed availability group technology, which was introduced in SQL Server 2016. 

In addition to the supported SQL Server version, you'll need:

- Network connectivity between your SQL Server instance and your managed instance. If SQL Server is running on-premises, use a VPN link or Azure ExpressRoute. If SQL Server is running on an Azure virtual machine (VM), either deploy your VM to the same virtual network as your managed instance or use virtual network peering to connect the two separate subnets. 
- An Azure SQL Managed Instance deployment, provisioned to any service tier.

You'll also need the following tooling:

| Tool | Notes  | 
| --- | --- |
| [SSMS&nbsp;19.0](/sql/ssms/download-sql-server-management-studio-ssms) or later | SQL Server Management Studio (SSMS) is the easiest way to use the Managed Instance link. It provides graphical wizards for automated link setup for SQL Server versions 2016, 2019, and 2022. The ability to use SSMS to fail back from your managed instance to SQL Server 2022 is available only in limited public preview. [Sign up for limited public preview](https://aka.ms/mi-link-dr-preview-signup). |
| [Az.SQL&nbsp;3.9.0](https://www.powershellgallery.com/packages/Az.Sql) or later | A PowerShell module is required for the manual configuration steps. |

> [!NOTE]
> The Managed Instance link feature is available in all public Azure regions and national or government clouds.

## How the link feature works

The underlying technology behind the link feature for SQL Managed Instance creates a distributed availability group between SQL Server and Azure SQL Managed Instance. The solution supports single-node systems without existing availability groups, or multiple node systems with existing availability groups.  

:::image type="content" source="./media/managed-instance-link-feature-overview/mi-link-ag-dag.png" alt-text="Diagram showing how the link feature for SQL Managed Instance works.":::

Secure connectivity, such as a VPN or Azure ExpressRoute is used between an on-premises network and Azure. If SQL Server is hosted on an Azure VM, the internal Azure backbone can be used between the VM and managed instance – such as, for example, virtual network peering. The trust between the two systems is established using certificate-based authentication, in which SQL Server and SQL Managed Instance exchange their public keys.

There can be up to 100 links from the same or various SQL Server sources to a single Azure SQL Managed Instance. This limit is governed by the number of databases that can be hosted on a managed instance at the same time. Likewise, a single SQL Server instance can establish multiple parallel database synchronization links with several managed instances in different Azure regions in a one-to-one relationship between a database and a managed instance. 

## Supported scenarios

Databases that are replicated through the link feature from SQL Server to Azure SQL Managed Instance can be used with several scenarios, such as: 

- Using Azure services without migrating to the cloud 
- Offloading read-only workloads to Azure 
- Migrating to Azure
- Disaster recovery with SQL Server 2022 (currently in limited public preview)

:::image type="content" source="./media/managed-instance-link-feature-overview/mi-link-main-scenario.png" alt-text="Diagram that illustrates the main Managed Instance link scenario.":::

### Use Azure services

Use the link feature to take advantage of Azure services by using SQL Server data without migrating it to the cloud. Examples include reporting, analytics, backups, machine learning, and other jobs that send data to Azure. 

### Offload workloads to Azure

You can also use the link feature to offload workloads to Azure. For example, an application could use SQL Server for read/write workloads, while it offloads read-only workloads to SQL Managed Instance deployments in any Azure region worldwide. After the link is established, the primary database on SQL Server is read/write accessible, while replicated data to your managed instance in Azure is read-only accessible. This arrangement allows for various scenarios where replicated databases on your managed instance can be used for read scale-out and offloading read-only workloads to Azure. Your managed instance, in parallel, can also host independent read/write databases. This allows for copying the replicated database to another read/write database on the same managed instance for further data processing.

The link is database scoped (one link per one database), allowing for consolidation and deconsolidation of workloads in Azure. For example, you can replicate databases from multiple SQL Server instances to a single SQL Managed Instance deployment in Azure (consolidation), or you can replicate databases from a single SQL Server instance to multiple managed instances via a one-to-one relationship between a database and a managed instance, to any Azure region worldwide (deconsolidation). The latter option provides you with an efficient way to quickly bring your workloads closer to your customers in any region worldwide, which you can use as read-only replicas.

### Migrate to Azure

The link feature also facilitates migrating from SQL Server to SQL Managed Instance, which enables: 

- The most performant, minimal downtime migration, compared to all other solutions available today.
- True online migration to SQL Managed Instance in any service tier. 

Because the link feature enables minimal downtime migration, you can migrate to your managed instance as you maintain your primary workload online. Although it's currently possible to achieve online migrations to the *General Purpose* service tier with other solutions, the link feature is the only solution that allows true online migrations to the *Business Critical* tier. 

### Automated backups

After your databases are replicated to your Azure SQL Managed Instance, they're automatically backed up with Azure Backup. You can reduce your on-premises management and operation costs while enjoying the reliability of Azure Backup for your replicated databases. You can then perform a [point-in-time restore](point-in-time-restore.md) of your replicated database to any SQL Managed Instance deployment in the same region, as with any other [automated backup](automated-backups-overview.md). 

### Disaster recovery

If you're running SQL Server 2022, you can use the Managed Instance link for disaster recovery, where, in the event of a disaster, you can manually fail over your workload to Azure SQL Managed Instance. After the disaster is mitigated, you can fail back to SQL Server 2022. 

This feature is currently in limited public preview. [You must sign up for limited public preview](https://aka.ms/mi-link-dr-preview-signup) so that the product group can configure your environment for the preview.  

:::image type="content" source="media/managed-instance-link-feature-overview/disaster-recovery-scenario.png" alt-text="Diagram showing the disaster recovery scenario.":::

## Use the link feature

After you've created the link, to ensure that you're following the best practices for maintaining the link, see [Best practices for the link feature in Azure SQL Managed Instance](managed-instance-link-best-practices.md).

When you're ready to migrate a database to Azure with minimal downtime, you can do so by using an automated wizard in SSMS or manually by using scripts.

Do either of the following:

- [Replicate a database by using the link feature in SSMS](managed-instance-link-use-ssms-to-replicate-database.md)
- [Replicate a database by using the Managed Instance link feature with T-SQL and PowerShell scripts](managed-instance-link-use-scripts-to-replicate-database.md)

## Limitations

Consider the following limitations when you're using the link.

Version supportability limitations include:

- You can't use Windows 10 and 11 clients to host your SQL Server instance, because it's not possible to enable the Always On availability group feature that's required for the link. SQL Server instances must be hosted on Windows Server 2012 or later.
- SQL Server versions 2008 to 2014 aren't supported by the link feature, as the SQL engine of these releases doesn't have built-in support for distributed availability groups required for the link. Upgrade to a newer version of SQL Server to use the link.

Data replication limitations include:

- Only user databases can be replicated. Replication of system databases isn't supported.
- The solution doesn't replicate server-level objects, agent jobs, or user logins from SQL Server to SQL Managed Instance.
- For SQL Server versions 2016 and 2019, replication of user databases from SQL Server instances to SQL Managed Instance deployments is one way. User databases from SQL Managed Instance deployments can't be replicated back to SQL Server instances. Two-way replication with failback to a SQL Server instance is available only for SQL Server 2022.

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

- [Auto-failover groups](auto-failover-group-sql-mi.md) aren't supported with instances that use the link feature. You can't establish a link on a managed instance that's part of an auto-failover group, and conversely, you can't configure an auto-failover group on an instance that has a link established.
- If you're using Change Data Capture (CDC), log shipping, or a service broker with databases that are replicated on the SQL Server instance, when the database is migrated to a SQL Managed Instance deployment, during a failover to Azure, clients need to connect by using the instance name of the current global primary replica. These settings should be manually reconfigured. 
- If you're using transactional replication with a database on a SQL Server instance in a migration scenario, during failover to Azure, the transactional replication on the SQL Managed Instance deployment will fail and should be manually reconfigured. 
- If you're using distributed transactions with a database that's replicated from the SQL Server instance and, in a migration scenario, on the cutover to the cloud, Distributed Transaction Coordinator capabilities won't be transferred. It's not possible for the migrated database to get involved in distributed transactions with the SQL Server instance, because the SQL Managed Instance deployment doesn't support distributed transactions with SQL Server at this time. For reference, SQL Managed Instance today supports distributed transactions only between other managed instances. For more information, see [Distributed transactions across cloud databases](../database/elastic-transactions-overview.md#transactions-for-sql-managed-instance).
- If you're using Transparent Data Encryption (TDE) to encrypt SQL Server databases, the database encryption key from SQL Server needs to be exported and uploaded to Azure Key Vault, and you need to also configure the BYOK TDE option on SQL Managed Instance before creating the link.
- You can't establish a link between SQL Server and SQL Managed Instance if the functionality that's used on the SQL Server instance isn't supported on the managed instance. For example: 
    - Databases with file tables and file streams can't be replicated, because SQL Managed Instance doesn't support file tables or file streams.
    - Databases that use In-Memory OLTP (Hekaton) can be replicated only to the *Business Critical* service tier for SQL Managed Instance, because the *General Purpose* service tier doesn't support In-Memory OLTP. Databases with multiple Hekaton files can't be replicated to the Business Critical service tier for SQL Managed Instance, as multiple Hekaton files aren't supported.

Trying to add an unsupported functionality to a replicated database in: 
   - SQL Server 2019 and 2022 fails with an error. 
   - SQL Server 2016 results in breaking the link, which will then need to be deleted and recreated. 

For the full list of differences between SQL Server and SQL Managed Instance, see [T-SQL differences between SQL Server and Azure SQL Managed Instance](./transact-sql-tsql-differences-sql-server.md).

## Next steps

For more information about the link feature, see:

- [Managed Instance link: Connecting SQL Server to Azure reimagined](https://aka.ms/mi-link-techblog)
- [Prepare for the Managed Instance link](./managed-instance-link-preparation.md)
- [Use a Managed Instance link via SSMS to replicate a database](./managed-instance-link-use-ssms-to-replicate-database.md)
- [Use a Managed Instance link via SSMS to migrate a database](./managed-instance-link-use-ssms-to-failover-database.md)

For other replication and migration scenarios, consider:

- [Transactional replication with SQL Managed Instance](replication-transactional-overview.md)
- [Log Replay Service (LRS)](log-replay-service-overview.md)
