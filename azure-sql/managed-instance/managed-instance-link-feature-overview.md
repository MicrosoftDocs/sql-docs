---
title: Managed Instance link feature overview - Azure SQL Managed Instance
titleSuffix: Azure SQL Managed Instance
description: Learn about the link feature for Azure SQL Managed Instance to continuously replicate data from SQL Server to the cloud, or migrate your SQL Server databases with the best possible minimum downtime.
author: danimir
ms.author: danil
ms.reviewer: mathoma, randolphwest
ms.date: 12/02/2022
ms.service: sql-managed-instance
ms.subservice: data-movement
ms.topic: conceptual
ms.custom: ignite-fall-2021
---
# Managed Instance link feature overview - Azure SQL Managed Instance

[!INCLUDE[appliesto-sqlmi](../includes/appliesto-sqlmi.md)]

This article provides an overview of the Managed Instance link feature, which enables near real-time data replication from SQL Server to Azure SQL Managed Instance. The link provides hybrid flexibility, database mobility, and unlocks a number of scenarios, such as scaling read-only workloads, offloading analytics and reporting to Azure, migrating to the cloud, and, with SQL Server 2022, disaster recovery.

If you have product improvement suggestions, comments, or you want to report issues, the best way to contact our team is through [SQL Managed Instance link user feedback](https://aka.ms/mi-link-feedback).

## Overview

The Managed Instance link feature uses distributed availability groups to extend your SQL Server on-premises availability group hosted anywhere to Azure SQL Managed Instance in a safe and secure manner, replicating data in near real-time.

The solution supports single-node systems without existing availability groups, or multiple node systems with existing availability groups.  The link supports single node SQL Server instances without existing availability groups, and also multiple-node SQL Server instances with existing availability groups. Through the link, you can use the modern benefits of Azure without migrating your entire SQL Server data estate to the cloud.

The link feature currently offers the following functionality:

- **One-way replication (SQL Server 2017 - 2019)**: Use the link feature to replicate data one way from SQL Server to Azure SQL Managed Instance. While manual failover to SQL MI is available in the event of a disaster, doing so breaks the link, and failing back is not supported.
- **Disaster recovery (SQL Server 2022)**: Use the link feature to replicate data from SQL Server 2022 to Azure SQL Managed Instance, manually fail over to SQL MI in the event of a disaster, and fail back to SQL Server once the disaster is mitigated.  This functionality is currently in limited public preview that users [must sign up for](https://aka.ms/mi-link-dr-preview-signup).

You can keep running the link for as long as you need it, for months and even years at a time. And for your modernization journey, if or when you're ready to migrate to Azure, the link enables a considerably improved migration experience with the minimum possible downtime compared to all other options available today, providing a true online migration to SQL Managed Instance.

## Requirements

To use the link feature, you'll need a supported Enterprise, Standard, or Developer edition of SQL Server running on Windows Server.

The following table lists the functionality of the link feature and the supported SQL Server versions:

| SQL Server version | Operating system (OS) | One-way replication | Disaster recovery | Servicing update requirement |
| --- | --- | --- | --- |
| SQL Server 2022 (16.x) | Windows Server & Linux | Generally available | [Must sign up for limited public preview](https://aka.ms/mi-link-dr-preview-signup) | SQL Server 2022 RTM |
| SQL Server 2019 (15.x) | Windows Server | Preview | Not supported | [SQL Server 2019 CU15 (KB5008996)](https://support.microsoft.com/help/5008996), or above for Enterprise and Developer editions, and [CU17 (KB5016394)](https://support.microsoft.com/help/5016394), or above, for Standard editions. |
| SQL Server 2017 (14.x) | N/A | Not supported | Not supported | N/A |
| SQL Server 2016 (13.x) | Windows Server | Preview | Not supported | [SQL Server 2016 SP3 (KB 5003279)](https://support.microsoft.com/help/5003279) and [SQL Server 2016 Azure Connect pack (KB 5014242)](https://support.microsoft.com/help/5014242) |

SQL Server 2008 to SQL Server 2014 isn't supported, as the link feature relies on availability group technology introduced in SQL Server 2016.

In addition to the supported version, you'll need:

- Network connectivity between your SQL Server and managed instance is required. If your SQL Server is running on-premises, use a VPN link or Express route. If your SQL Server is running on an Azure VM, either deploy your VM to the same VNet as your managed instance, or use VNet peering to connect two separate subnets.
- Azure SQL Managed Instance provisioned to any service tier.

You'll also need the following tooling:

| Tool | Notes |
| --- | --- |
| [SSMS 19.0](/sql/ssms/download-sql-server-management-studio-ssms), or higher | SQL Server Management Studio (SSMS) is the easiest way to use SQL Managed Instance link. Provides graphical wizards for automated link setup for SQL Server 2016, 2019, and 2022. The ability to use SSMS to fail back from SQL MI to SQL Server 2022 is only available in limited public preview. Sign up at [https://aka.ms/mi-link-dr-preview-signup](https://aka.ms/mi-link-dr-preview-signup). |
| [Az.SQL 3.9.0](https://www.powershellgallery.com/packages/Az.Sql), or higher | PowerShell module is required for manual configuration steps. |

> [!NOTE]  
> SQL Managed Instance link feature is available in all public Azure regions and national\government clouds.

## How it works

The underlying technology behind the link feature for SQL Managed Instance creates a distributed availability group between SQL Server and Azure SQL Managed Instance. The solution supports single-node systems without existing availability groups, or multiple node systems with existing availability groups.

:::image type="content" source="./media/managed-instance-link-feature-overview/mi-link-ag-dag.png" alt-text="Diagram showing how the link feature for SQL Managed Instance works.":::

Secure connectivity, such as VPN or Express Route is used between an on-premises network and Azure. If SQL Server is hosted on an Azure VM, the internal Azure backbone can be used between the VM and managed instance – such as, for example, VNet peering. The trust between the two systems is established using certificate-based authentication, in which SQL Server and SQL Managed Instance exchange their public keys.

There could exist up to 100 links from the same, or various SQL Server sources to a single SQL Managed Instance. This limit is governed by the number of databases that could be hosted on a managed instance at this time. Likewise, a single SQL Server can establish multiple parallel database replication links with several managed instances in different Azure regions in a 1 to 1 relationship between a database and a managed instance.

## Supported scenarios

Databases replicated through the link feature from SQL Server to Azure SQL Managed Instance can be used with several scenarios, such as:

- **Use Azure services without migrating to the cloud**
- **Offload read-only workloads to Azure**
- **Migrate to Azure**
- **Disaster recovery with SQL Server 2022** (currently in limited public preview)

:::image type="content" source="./media/managed-instance-link-feature-overview/mi-link-main-scenario.png" alt-text="Diagram showing the main Managed Instance link scenario.":::

### Use Azure services

Use the link feature to leverage Azure services using SQL Server data without migrating to the cloud. Examples include reporting, analytics, backups, machine learning, and other jobs that send data to Azure.

### Offload workloads to Azure

You can also use the link feature to offload workloads to Azure. For example, an application could use SQL Server for read-write workloads, while offloading read-only workloads to SQL Managed Instance in any Azure region worldwide. Once the link is established, the primary database on SQL Server is read/write accessible, while replicated data to SQL Managed Instance in Azure is read-only accessible. This allows for various scenarios where replicated databases on SQL Managed Instance can be used for read scale-out and offloading read-only workloads to Azure. SQL Managed Instance, in parallel, can also host independent read/write databases. This allows for copying the replicated database to another read/write database on the same managed instance for further data processing.

The link is database scoped (one link per one database), allowing for consolidation and deconsolidation of workloads in Azure. For example, you can replicate databases from multiple SQL Servers to a single SQL Managed Instance in Azure (consolidation), or replicate databases from a single SQL Server to multiple managed instances via a 1 to 1 relationship between a database and a managed instance -  to any of Azure's regions worldwide (deconsolidation). The latter provides you with an efficient way to quickly bring your workloads closer to your customers in any region worldwide, which you can use as read-only replicas.

### Migrate to Azure

The link feature also facilitates migrating from SQL Server to SQL Managed Instance, enabling:

- The most performant minimum downtime migration compared to all other solutions available today
- True online migration to SQL Managed Instance in any service tier

Since the link feature enables minimum downtime migration, you can migrate to your managed instance while maintaining your primary workload online. While online migration was possible to achieve previously with other solutions when migrating to the General Purpose service tier, the link feature now also allows for true online migrations to the Business Critical service tier as well.

### Automated backups

Once your databases are replicated to SQL Managed Instance, they are automatically backed up to Azure Blob Storage with Azure backup. You can reduce your on-premises management and operation costs while enjoying the reliability of Azure backup for your replicated databases. You can then perform a [point-in-time restore](point-in-time-restore.md) of your replicated database to any SQL Managed Instance in the same region, as with any other [automated backup](automated-backups-overview.md).

### Disaster recovery

SQL Server 2022 customers can use the Managed Instance link for disaster recovery where, in the event of a disaster, you can manually fail your workload over to Azure SQL Managed Instance. Once the disaster is mitigated, you can fail back over to your SQL Server 2022 instance. This feature is currently in limited public preview that you must [sign up for](https://aka.ms/mi-link-dr-preview-signup) so the product group can configure your environment for the preview.

:::image type="content" source="media/managed-instance-link-feature-overview/disaster-recovery-scenario.png" alt-text="Diagram showing the disaster recovery scenario.":::

Select the following link to sign up and get access to the limited public preview of this feature:

> [!div class="nextstepaction"]
> [Preview DR with SQL Server 2022](https://aka.ms/mi-link-dr-preview-signup)

To learn more about limited public preview of the DR feature, see https://aka.ms/mi-link-dr-preview-announcement.

## Use the link feature

To help you set up the initial environment, review the guide how to prepare your SQL Server environment to use with the link feature for SQL Managed Instance:

- [Prepare environment for the link](managed-instance-link-preparation.md)

Once you've ensured the pre-requirements have been met, you can create the link using the automated wizard in SSMS, or you can choose to set up the link manually using scripts. Create the link using one of the following instructions:

- [Replicate database with link feature in SSMS](managed-instance-link-use-ssms-to-replicate-database.md), or alternatively
- [Replicate database with Azure SQL Managed Instance link feature with T-SQL and PowerShell scripts](managed-instance-link-use-scripts-to-replicate-database.md)

Once the link has been created, ensure that you follow the best practices for maintaining the link, by following instructions described at this page:

- [Best practices with link feature for Azure SQL Managed Instance](managed-instance-link-best-practices.md)

If and when you're ready to migrate a database to Azure with a minimum downtime, you can do this using an automated wizard in SSMS, or you can choose to do this manually with scripts. Migrate database to Azure link using one of the following instructions:

- [Failover database to SQL MI in SSMS](managed-instance-link-use-ssms-to-failover-database.md), or alternatively
- [Failover database to SQL MI with T-SQL and PowerShell scripts](managed-instance-link-use-scripts-to-failover-database.md)

## Limitations

Consider the following limitations when using the link.

Version supportability limitations include:

- Client Windows OS 10 and 11 can't be used to host your SQL Server, as it's not possible to enable the Always On availability group feature required for the link. SQL Server must be hosted on Windows Server 2012 or higher.
- SQL Server versions 2008 to 2014 aren't supported by the link feature, as the SQL engine of these releases doesn't have built-in support for distributed availability groups required for the link. Upgrade to a newer version of SQL Server to use the link.

Data replication limitations include:

- Only user databases can be replicated. Replication of system databases isn't supported.
- The solution doesn't replicate server level objects, agent jobs, nor user logins from SQL Server to SQL Managed Instance.
- For SQL Server 2016 - 2019, replication of user databases from SQL Server to SQL Managed Instance is one-way. User databases from SQL Managed Instance can't be replicated back to SQL Server. Two-way replication with fail back to SQL Server is only available for SQL Server 2022.

Configuration limitations include:

- If there are multiple SQL Server instances on a server, it's possible to configure a link with each instance but each instance must be configured to use a separate database mirroring endpoint with a dedicated port per instance. Only the default instance should use port 5022 for the database mirroring endpoint.
- Only one database can be placed into a single availability group per one Managed Instance link.
- Managed Instance link can replicate a database of any size if it fits into the chosen storage size of the target SQL Managed Instance.
- Managed Instance link authentication between SQL Server instance and SQL Managed Instance is certificate-based, available only through exchange of certificates. Using Windows authentication to establish the link between SQL Server and managed instance isn't supported.
- Private endpoint (VPN/virtual network) is supported to establish the link with SQL Managed Instance. Public endpoint can't be used to establish the link with SQL Managed Instance.

Feature limitations include:

- [Auto failover groups](auto-failover-group-sql-mi.md) replication to secondary SQL Managed Instance can't be used in parallel while operating the Managed Instance link with SQL Server.
- If Change Data Capture (CDC), log shipping, or service broker is used with databases replicated on the SQL Server, when the database is migrated to SQL Managed Instance and during failover to Azure, clients will need to connect using the instance name of the current global primary replica. These settings should be manually reconfigured.
- If transactional replication is used with a database on SQL Server in a migration scenario, during failover to Azure, transactional replication on SQL Managed Instance will fail and should be manually reconfigured.
- If distributed transactions are used with a database replicated from the SQL Server, and in a migration scenario, on the cutover to the cloud, DTC capabilities won't be transferred. There will be no possibility for migrated database to get involved in distributed transactions with SQL Server, as SQL Managed Instance doesn't support distributed transactions with SQL Server at this time. For reference, SQL Managed Instance today supports distributed transactions only between other SQL Managed Instances, see [Distributed transactions across cloud databases](../database/elastic-transactions-overview.md#transactions-for-sql-managed-instance).
- The link can't be established between SQL Server and SQL Managed Instance if the functionality used on SQL Server isn't supported on SQL Managed Instance:
  - File tables and file streams aren't supported for replication, as SQL Managed Instance doesn't support them.
  - Replicating Databases using In-memory OLTP (Hekaton) isn't supported on SQL Managed Instance General Purpose service tier. In-memory OLTP is only supported on SQL Managed Instance Business Critical service tier.
  - For the full list of differences between SQL Server and SQL Managed Instance, see [T-SQL differences between SQL Server & Azure SQL Managed Instance](./transact-sql-tsql-differences-sql-server.md).

## Next steps

If you have product improvement suggestions, comments, or you want to report issues, the best way to contact our team is through [SQL Managed Instance link user feedback](https://aka.ms/mi-link-feedback).

For more information on the link feature, see the following:

- [Managed Instance link – connecting SQL Server to Azure reimagined](https://aka.ms/mi-link-techblog).
- [Prepare for SQL Managed Instance link](./managed-instance-link-preparation.md).
- [Use SQL Managed Instance link via SSMS to replicate database](./managed-instance-link-use-ssms-to-replicate-database.md).
- [Use SQL Managed Instance link via SSMS to migrate database](./managed-instance-link-use-ssms-to-failover-database.md).

For other replication and migration scenarios, consider:

- [Transactional replication with Azure SQL Managed Instance](replication-transactional-overview.md)
- [Log Replay Service (LRS)](log-replay-service-overview.md)
