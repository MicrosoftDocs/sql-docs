---
title: What's new?
titleSuffix: Azure SQL Managed Instance
description: Learn about the new features and documentation improvements for Azure SQL Managed Instance.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: wiassaf
ms.date: 11/14/2023
ms.service: sql-managed-instance
ms.subservice: service-overview
ms.topic: whats-new
ms.custom: references_regions, ignite-2023
---
# What's new in Azure SQL Managed Instance?
[!INCLUDE[appliesto-sqldb-sqlmi](../includes/appliesto-sqlmi.md)]

> [!div class="op_single_selector"]
> * [Azure SQL Database](../database/doc-changes-updates-release-notes-whats-new.md?view=azuresql&preserve-view=true)
> * [Azure SQL Managed Instance](doc-changes-updates-release-notes-whats-new.md?view=azuresql&preserve-view=true)
> * [SQL Server on Azure VMs](../virtual-machines/windows/doc-changes-updates-release-notes-whats-new.md?view=azuresql&preserve-view=true)

This article summarizes the documentation changes associated with new features and improvements in the recent releases of [Azure SQL Managed Instance](https://azure.microsoft.com/updates/?product=sql-database&query=sql%20managed%20instance). To learn more about Azure SQL Managed Instance, see [What is Azure SQL Managed Instance?](sql-managed-instance-paas-overview.md)


[!INCLUDE [entra-id](../includes/entra-id.md)]

## Preview

The following table lists the features of Azure SQL Managed Instance that are currently in preview.

> [!NOTE]  
> Features currently in preview are available under [supplemental terms of use](https://azure.microsoft.com/support/legal/preview-supplemental-terms/), review for legal terms that apply to Azure features that are in beta, preview, or otherwise not yet released into general availability. Azure SQL Managed Instance provides previews to give you a chance to evaluate and [share feedback with the product group](https://feedback.azure.com/d365community/forum/a99f7006-3425-ec11-b6e6-000d3a4f0f84) on features before they become generally available (GA).

| Feature | Details |
| ---| --- |
|[Endpoint policies](./service-endpoint-policies-configure.md) | Configure which Azure Storage accounts can be accessed from a SQL Managed Instance subnet. Grants an extra layer of protection against inadvertent or malicious data exfiltration.|		
|[Instance pools](instance-pools-overview.md) | A convenient and cost-efficient way to migrate smaller SQL Server instances to the cloud. |		
|[Link from SQL MI to SQL Server](managed-instance-link-feature-overview.md) | Configure a link from Azure SQL Managed Instance to SQL Server 2022. | 		
|[Maintenance window advance notifications](../database/advance-notifications.md)| Advance notifications for databases configured to use a non-default [maintenance window](../database/maintenance-window.md).  |		
|[SDK-style SQL project](/sql/azure-data-studio/extensions/sql-database-project-extension-sdk-style-projects) | Use [Microsoft.Build.Sql](https://www.nuget.org/packages/Microsoft.Build.Sql) for SDK-style SQL projects in the SQL Database Projects extension in Azure Data Studio or Visual Studio Code. SDK-style SQL projects are especially advantageous for applications shipped through pipelines or built in cross-platform environments.| 		
|[Service Broker](/sql/database-engine/configure-windows/sql-server-service-broker) | Support for cross-instance message exchange using Service Broker between instances of Azure SQL Managed Instance, and between SQL Server and Azure SQL Managed Instance. |		
|[Two-way DR with SQL Server 2022](managed-instance-link-disaster-recovery.md) | In the event of a disaster, you can fail your SQL Server 2022 workloads to Azure SQL Managed Instance using the link, and then, once the disaster is mitigated, you can fail back to SQL Server. | 		
|[Threat detection](threat-detection-configure.md) | Threat detection notifies you of security threats detected to your database. |		




## General availability (GA)

The following table lists the new generally available (GA) features of Azure SQL Managed Instance, and those that have transitioned from preview to GA within the last 12 months:

| Feature | GA Month | Details |
| ---| --- |--- |
|[Azure SQL triggers for Azure Functions](/azure/azure-functions/functions-bindings-azure-sql) | November 2023 | Azure Functions supports function triggers for Azure SQL Managed Instance. | 
|[Database copy and move](database-copy-move-how-to.md) | November 2023 | Perform an online database copy or move operation across managed instances. | 
|[Distributed Transaction Coordinator (DTC)](distributed-transaction-coordinator-dtc.md) | November 2023 | Use DTC to run distributed transactions in mixed environments such as across managed instances, SQL Servers, other relational database management systems (RDBMSs), custom applications and other transaction participants hosted in any environment that can establish network connectivity to Azure.  | 
|[Instance stop and start](instance-stop-start-how-to.md) | November 2023 | Stop and start your managed instance to save on licensing and compute costs. | 
|[Ledger](/sql/relational-databases/security/ledger/ledger-overview) | November 2023 | The ledger feature in Azure SQL Managed Instance allows you to cryptographically attest to other parties, such as auditors or other business parties, that your data hasn't been tampered with. |
|[November 2022 feature wave](november-2022-feature-wave-enroll.md) | November 2023 | November 2022 brought a number of new features for Azure SQL Managed Instance, such as [fast provisioning](management-operations-overview.md#fast-provisioning), and [zone redundancy](high-availability-sla.md#zone-redundant-availability) as well as enhancements to the [virtual cluster](virtual-cluster-architecture.md) and [network security](connectivity-architecture-overview.md). |  
|[Zone-redundancy](../managed-instance/high-availability-sla.md#zone-redundant-availability) | November 2023 | Deploy your Business Critical SQL Managed Instance to multiple availability zones to improve the availability of your instance in the event of a disaster. | 
|[Double log write throughput limit](resource-limits.md#service-tier-characteristics) | August 2023 | The max log write throughput limit has doubled for the Business Critical tier, up to 192 MiB/s. | 
|[XML compression](https://techcommunity.microsoft.com/t5/azure-sql-blog/general-availability-of-xml-compression-for-azure-sql-database/ba-p/3888861) | August 2023 | XML compression for Azure SQL Managed Instance is now generally available. You can use [ALTER INDEX](/sql/t-sql/statements/alter-index-transact-sql?view=azuresqldb-mi-current&preserve-view=true#xml_compression) to apply XML compression to existing [XML indexes](/sql/relational-databases/xml/xml-indexes-sql-server?view=azuresqldb-mi-current&preserve-view=true). |
|[TDS 8.0 support](/sql/relational-databases/security/networking/tds-8?view=azuresqldb-mi-current&preserve-view=true) | August 2023 | Azure SQL Managed Instance now supports TDS 8.0 for strict encryption of data in transit. |
|[Private endpoints](private-endpoint-overview.md) | August 2023 | Establish secure and isolated connectivity between Azure SQL Managed Instance and multiple virtual networks without exposing the entire network infrastructure of your service [by using a private endpoint](https://techcommunity.microsoft.com/t5/azure-sql-blog/private-endpoints-ga-for-azure-sql-managed-instance/ba-p/3895434). Review these blog posts on [Scenarios with private endpoints](https://techcommunity.microsoft.com/t5/azure-sql-blog/scenarios-with-private-endpoints-to-azure-sql-managed-instance/ba-p/3902001) and [Advanced scenarios with private endpoints to Azure SQL Managed Instance](https://techcommunity.microsoft.com/t5/azure-sql-blog/advanced-scenarios-with-private-endpoints-to-azure-sql-managed/ba-p/3902198). |
|[Azure SQL bindings for Azure Functions](/azure/azure-functions/functions-bindings-azure-sql) | May 2023 | Azure Functions supports input and output bindings for the Azure SQL and SQL Server products. | 
|[License-free standby replica](auto-failover-group-standby-replica-how-to-configure.md) | May 2023 | Save on licensing costs when you designate your geo-secondary replica as **Standby**. | 
|[CREATE EXTERNAL TABLE AS SELECT (CETAS)](/sql/t-sql/statements/create-external-table-as-select-transact-sql?view=azuresqldb-mi-current&preserve-view=true) | April 2023 | You can use CETAS to create an external table on top of Parquet or CSV files Azure Blob storage or Azure Data Lake Storage (ADLS) Gen2. CETAS can also export, in parallel, the results of a T-SQL SELECT statement into the created external table. There is potential for data exfiltration risk with these capabilities, so CETAS is disabled by default for Azure SQL Managed Instance. To enable, see [CREATE EXTERNAL TABLE AS SELECT (CETAS)](/sql/t-sql/statements/create-external-table-as-select-transact-sql?view=azuresqldb-mi-current&preserve-view=true#enable-cetas-via-azure-powershell). |
|[One-way Managed Instance link for SQL Server 2016 & 2019](managed-instance-link-feature-overview.md)| April 2023 | Online replication of SQL Server databases hosted anywhere to Azure SQL Managed Instance. |
|[SQL Database Projects extension](/azure-data-studio/extensions/sql-database-project-extension) | April 2023 | An extension to develop databases for Azure SQL Database with Azure Data Studio and Visual Studio Code. A SQL project is a local representation of SQL objects that comprise the schema for a single database, such as tables, stored procedures, or functions. |
|[Shrink Database](/sql/t-sql/database-console-commands/dbcc-shrinkdatabase-transact-sql?view=azuresqldb-mi-current&preserve-view=true) and [Shrink File with Low Priority](/sql/t-sql/database-console-commands/dbcc-shrinkfile-transact-sql?view=azuresqldb-mi-current&preserve-view=true) | March 2023 | This feature solves the concurrency issues that can arise from shrink database and shrink file commands, especially during active maintenance or on busy OLTP environments. In WAIT_AT_LOW_PRIORITY mode, necessary tasks to shrink database files can be completed without negatively affecting application query performance. |
|[Approximate percentile](/sql/t-sql/functions/approx-percentile-cont-transact-sql)| March 2023 | Quickly compute percentiles for a large dataset with acceptable rank-based error bounds to help make rapid decisions by using approximate percentile aggregate functions. | 
|[Tempdb configurations - max size](tempdb-configure.md)| January 2023 | Specify the maximum size for your `tempdb` files.  |
|[Backup transparency](backup-transparency.md) | November 2022 |  Query the `msdb` database to explore your backup history. | 
|[Cross-subscription PITR](point-in-time-restore.md) | November 2022 | Restore your database to an instance in a different subscription than your original managed instance by using point-in-time restore (PITR). 
|[Migrate to SQL MI with Log Replay Service](log-replay-service-migrate.md) | November 2022 |  Migrate databases from SQL Server to SQL Managed Instance by using Log Replay Service. |
|[One-way Managed Instance link for SQL Server 2022](managed-instance-link-feature-overview.md)| November 2022 | Online one-way replication of SQL Server databases hosted on SQL Server 2022 to Azure SQL Managed Instance. |
|[Restore database from SQL MI to SQL Server](restore-database-to-sql-server.md) | November 2022 |  Restore your database from Azure SQL Managed Instance to SQL Server 2022. | 
|[Time series](/sql/t-sql/functions/generate-series-transact-sql) | November 2022 | Generates a series of numbers within a given interval.  Review [GENERATE_SERIES](/sql/t-sql/functions/generate-series-transact-sql) and [DATE_BUCKET](/sql/t-sql/functions/date-bucket-transact-sql) to learn more. | 
|[Transactional Replication](replication-transactional-overview.md) | November 2022 | Replicate data from SQL Managed Instance to database hosted on SQL Server, SQL Managed Instance or SQL Database, or from SQL Server to SQL Managed Instance. To get started, review [Configure replication in Azure SQL Managed Instance](replication-between-two-instances-configure-tutorial.md). |


## November 2022 feature wave

The features and benefits provided by the November 2022 feature wave are now generally available. All new instances in eligible subnets in both dev/test and production subscriptions are eligible for the feature wave. Existing instances will be enrolled gradually as part of regular updates during configured maintenance windows. To learn more, see [November 2022 feature wave](november-2022-feature-wave-enroll.md).

## Documentation changes

Learn about significant changes to the Azure SQL Managed Instance documentation. For previous years, see the [What's new archive](doc-changes-updates-release-notes-whats-new-archive.md).

### November 2023

| Changes | Details |
| --- | --- |
|**Azure SQL triggers for Azure Functions GA** | Azure Functions supports function triggers for Azure SQL Managed Instance. This feature is now generally available. Review [Azure SQL triggers for Azure Functions](/azure/azure-functions/functions-bindings-azure-sql) to learn more. |
|**Database copy and move GA**| Perform an online database copy or move operation across managed instances. This feature is now generally available. Review [Database copy and move](database-copy-move-how-to.md) to get started. |
|**Distributed Transaction Coordinator (DTC) GA** | Use DTC to run distributed transactions in mixed environments such as across managed instances, SQL Servers, other relational database management systems (RDBMSs), custom applications and other transaction participants hosted in any environment that can establish network connectivity to Azure. This feature is now generally available. Review [Distributed Transaction Coordinator (DTC)](distributed-transaction-coordinator-dtc.md) to get started. | 
|**Ledger GA** | Ledger in Azure SQL Managed Instance allows you to cryptographically attest to other parties, such as auditors or other business parties, that your data hasn't been tampered with. This feature is now generally available. Review [Ledger](/sql/relational-databases/security/ledger/ledger-overview) to learn more. | 
|**Link from SQL MI to SQL Server 2022 preview** |  Configure a link from Azure SQL Managed Instance to SQL Server 2022. This feature is now in preview. Review [Managed Instance link overview](managed-instance-link-feature-overview.md) to learn more. |
|**Instance stop and start GA** | Stop and start your managed instance to save on licensing and compute costs. This feature is now generally available. Review [instance stop and start](instance-stop-start-how-to.md) to learn more.  |
|**November 2022 feature wave GA**| November 2022 brought a number of new features for Azure SQL Managed Instance, such as [fast provisioning](management-operations-overview.md#fast-provisioning), and [zone redundancy](high-availability-sla.md#zone-redundant-availability) as well as enhancements to the [virtual cluster](virtual-cluster-architecture.md) and [network security](connectivity-architecture-overview.md).  All the features and benefits introduced by the November 2022 feature wave are now generally available. Review [November 2022 Feature wave](november-2022-feature-wave-enroll.md) to learn more. | 
| **SQL Server DR to SQL MI preview** |  In the event of a disaster, you can recover your SQL Server 2022 database by failing over to SQL Managed Instance, and then failing back to SQL Server 2022 online by using the Managed Instance link. This feature has moved from a limited public preview that required signing up to an open public preview available to everyone. Review [Online disaster recovery for SQL Server 2022](managed-instance-link-disaster-recovery.md) to learn more. |
|**Zone-redundancy GA**| Deploy your Business Critical SQL Managed Instance to multiple availability zones to improve the availability of your instance in the event of a disaster. This capability is now generally available. Review [Zone-redundancy](../managed-instance/high-availability-sla.md#zone-redundant-availability) to learn more. |


### October 2023

| Changes | Details |
| --- | --- |
| **Hybrid failover benefit** | The failover rights benefit has been extended to include a license-free DR replica when you configure your passive DR SQL managed instance with the [link feature](managed-instance-link-feature-overview.md#license-free-passive-replica). Review [License-free replicas](business-continuity-high-availability-disaster-recover-hadr-overview.md#license-free-dr-replicas) to learn more. |

### September 2023

| Changes | Details |
| --- | --- |
| **Microsoft Entra ID rebrand**| Azure Active Directory has been rebranded to [Microsoft Entra ID](/azure/active-directory/fundamentals/new-name). | 

### August 2023

| Changes | Details |
| --- | --- |
| **Doubled max log rate GA** | The [log write throughput limit](resource-limits.md#service-tier-characteristics) per instance has doubled for the Business Critical service tier, up to 192 MiB/s from 96 MiB/s. This is generally available. Review the [tech community blog](https://techcommunity.microsoft.com/t5/azure-sql-blog/your-max-log-rate-on-sql-managed-instance-business-critical-is/ba-p/3899817) to learn more. |
| **Private endpoints GA** | Establish secure and isolated connectivity between Azure SQL Managed Instance and multiple virtual networks without exposing the entire network infrastructure of your service [by using a private endpoint](https://techcommunity.microsoft.com/t5/azure-sql-blog/private-endpoints-ga-for-azure-sql-managed-instance/ba-p/3895434). This feature is generally available. Review [Private Link and private endpoints](private-endpoint-overview.md) to learn more, or review these blog posts on [Scenarios with private endpoints](https://techcommunity.microsoft.com/t5/azure-sql-blog/scenarios-with-private-endpoints-to-azure-sql-managed-instance/ba-p/3902001) and [Advanced scenarios with private endpoints to Azure SQL Managed Instance](https://techcommunity.microsoft.com/t5/azure-sql-blog/advanced-scenarios-with-private-endpoints-to-azure-sql-managed/ba-p/3902198). |
| **TDS 8.0 GA** | Azure SQL Database now supports [TDS 8.0](/sql/relational-databases/security/networking/tds-8?view=azuresqldb-mi-current&preserve-view=true) for strict encryption of data in transit. |
| **XML compression GA** | [XML compression](https://techcommunity.microsoft.com/t5/azure-sql-blog/general-availability-of-xml-compression-for-azure-sql-database/ba-p/3888861) for Azure SQL Managed Instance is now generally available. You can use [ALTER INDEX](/sql/t-sql/statements/alter-index-transact-sql?view=azuresqldb-mi-current&preserve-view=true#xml_compression) to apply XML compression to existing [XML indexes](/sql/relational-databases/xml/xml-indexes-sql-server?view=azuresqldb-mi-current&preserve-view=true). |


### July 2023

| Changes | Details |
| --- | --- |
| **New max vCore limits** | It's now possible to provision your Business Critical Azure SQL Managed Instance with up to 128 vCores on both premium-series and memory optimized premium-series hardware. Review [resource limits](resource-limits.md) to learn more. |

### June 2023

| Changes | Details |
| --- | --- |
| **Configure tempdb**| Configure your `tempdb` settings for Azure SQL Managed Instance. Review [tempdb](tempdb-configure.md) to learn more. |


### May 2023

| Changes | Details |
| --- | --- |
| **Azure SQL bindings for Azure Functions GA** | Azure Functions supports input bindings, and output bindings for the Azure SQL and SQL Server products. This feature is now generally available. Review [Azure SQL bindings for Azure Functions](/azure/azure-functions/functions-bindings-azure-sql) to learn more.  | 
| **Azure SQL triggers for Azure Functions preview** | Azure Functions supports function triggers for the Azure SQL and SQL Server products. This feature is currently in preview. Review [Azure SQL triggers for Azure Functions](/azure/azure-functions/functions-bindings-azure-sql-trigger) to learn more. | 
| **Ledger preview**| The ledger feature allows you to cryptographically attest to other parties, such as auditors or other business parties, that your data hasn't been tampered with. This feature is now in preview for Azure SQL Managed Instance. To learn more, review [Ledger](/sql/relational-databases/security/ledger/ledger-overview). |
| **License-free standby replica GA** | Save on licensing costs if your auto-failover group geo-secondary instance is designated as **Standby** and used for disaster recovery purposes only. Thanks to the _Failover Rights_ benefit,  you don't need licenses for your geo-secondary instance if you don't use it for reporting or other read-only workloads. The benefit is available regardless of the licensing model used for by the primary instance. This feature is now generally available. Review [License-free standby replica](auto-failover-group-standby-replica-how-to-configure.md) to learn more.  | 

### April 2023

| Changes | Details |
| --- | --- |
| **CREATE EXTERNAL TABLE AS SELECT (CETAS) GA** | CETAS is now generally available. You can use CETAS to create an external table on top of Parquet or CSV files Azure Blob storage or Azure Data Lake Storage (ADLS) Gen2. CETAS can also export, in parallel, the results of a T-SQL SELECT statement into the created external table. There is potential for data exfiltration risk with these capabilities, so [CREATE EXTERNAL TABLE AS SELECT (CETAS)](/sql/t-sql/statements/create-external-table-as-select-transact-sql?view=azuresqldb-mi-current&preserve-view=true) is disabled by default for Azure SQL Managed Instance. To enable, see [CREATE EXTERNAL TABLE AS SELECT (CETAS)](/sql/t-sql/statements/create-external-table-as-select-transact-sql?view=azuresqldb-mi-current&preserve-view=true#enable-cetas-via-azure-powershell).|
|**Managed Instance link GA** | Online replication of SQL Server databases hosted anywhere to Azure SQL Managed Instance. One-way replication is now generally available for SQL Server 2016 and SQL Server 2019. Review [Managed Instance link](managed-instance-link-feature-overview.md) to learn more. |

### March 2023

| Changes | Details |
| --- | --- |
|**Approximate Percentile GA** | Support has been added to quickly compute percentiles using approximate percentile aggregate functions for large datasets with acceptable rank-based error bounds. This feature is now generally available. To learn more, review [Approx_Percentile_Cont](/sql/t-sql/functions/approx-percentile-cont-transact-sql) and [Approx_Percentile_Disc](/sql/t-sql/functions/approx-percentile-disc-transact-sql). | 
| **Shrink Database / Shrink File with Low Priority GA** | This feature solves the concurrency issues that can arise from shrink database and shrink file commands, especially during active maintenance or on busy OLTP environments. In WAIT_AT_LOW_PRIORITY mode, necessary tasks to shrink database files can be completed without negatively affecting application query performance. Review [Shrink Database](/sql/t-sql/database-console-commands/dbcc-shrinkdatabase-transact-sql?view=azuresqldb-mi-current&preserve-view=true) and [Shrink File with Low Priority](/sql/t-sql/database-console-commands/dbcc-shrinkfile-transact-sql?view=azuresqldb-mi-current&preserve-view=true). |


### 2022

The following changes were added to SQL Managed Instance and the documentation in 2022: 

| Changes | Details |
| --- | --- |
|**Terraform** | Deploy a managed instance by using [Terraform](instance-create-terraform.md). | 
|**Backup transparency with msdb GA** | For the purpose of backup transparency, it's now possible to query the `msdb` database to explore automated backup history. This feature is generally available. To learn more, review [backup transparency](backup-transparency.md). | 
|**Cross-subscription PITR GA** | It's now possible to restore your database for your SQL Managed Instance across subscriptions by using point-in-time restore (PITR). This feature is generally available. To learn more, review [Point-in-time restore](point-in-time-restore.md). | 
|**Database copy and move preview** | Perform an online copy or move operation of your database across managed instances. This feature is currently in preview. To learn more, review [Copy or move your database](database-copy-move-how-to.md). | 
|**Distributed Transaction Coordinator (DTC) preview** | Use DTC to run distributed transactions in mixed environments such as across managed instances, SQL Servers, other relational database management systems (RDBMSs), custom applications and other transaction participants hosted in any environment that can establish network connectivity to Azure.  This feature is currently in preview. Review [Distributed Transaction Coordinator (DTC)](distributed-transaction-coordinator-dtc.md)  to learn more.  | 
|**Free license for standby replica preview** | It's now possible to designate your secondary DR-only instance as **Standby**, saving on licensing costs. This feature is currently in preview. To learn more, review [Configure standby replica](auto-failover-group-standby-replica-how-to-configure.md). 
| **Gen5 hardware rename** | The Gen5 hardware in the vCore purchasing model has been renamed to **standard-series (Gen5)**. | 
|**Instance stop preview** | It's now possible to save on costs by stopping your General Purpose Azure SQL Managed Instance when you're not using it. To learn more, review [stop and start instance](instance-stop-start-how-to.md). | 
|**Log Replay Service GA** | Migrate your databases to Azure SQL Managed Instance using the Log Replay Service (LRS). This feature is now generally available. To learn more, review [Log Replay Service overview](log-replay-service-overview.md). To get started, review [Migrate with LRS](log-replay-service-migrate.md) | 
|**Managed Instance link for SQL Server 2022 GA** | Using the Managed Instance link to replicate data from SQL Server 2022 to Azure SQL Managed Instance is now generally available. Using the link feature with versions older than SQL Server 2022 is still in preview. To learn more, review [Managed Instance link](managed-instance-link-feature-overview.md).  | 
|**Managed Instance link fail over to SQL Server 2022 preview** | It's now possible to use the Managed Instance link feature to fail back from Azure SQL Managed Instance to SQL Server 2022. This feature is currently in preview. To learn more, review [Managed Instance link](managed-instance-link-feature-overview.md).  | 
|**November 2022 feature wave early enrollment** | The November 2022 feature wave is being rolled out over several months but it might be possible to enroll early. Review [Enroll in the feature wave](november-2022-feature-wave-enroll.md) to learn more. | 
|**Simplified connectivity architecture**| The November 2022 feature wave significantly simplifies the connectivity architecture for SQL Managed Instance, such as removing the management endpoint, and reducing the number of mandatory rules. Review [Connectivity architecture](connectivity-architecture-overview.md) to learn more. |
|**Restore database to SQL Server GA** | It's now possible to restore your database backup from Azure SQL Managed Instance to SQL Server 2022. The capability to do so is generally available, and enabled by default on all instances, both currently existing, and those deployed in the future. To learn more, review [Restore database to SQL Server](restore-database-to-sql-server.md). | 
| **Time series GA** | Generates a series of numbers within a given interval. This feature is generally available.  Review [GENERATE_SERIES](/sql/t-sql/functions/generate-series-transact-sql) and [DATE_BUCKET](/sql/t-sql/functions/date-bucket-transact-sql) to learn more. 
|**Transactional replication GA** | Replicate data from SQL Managed Instance to database hosted on SQL Server, SQL Managed Instance or SQL Database, or from SQL Server to SQL Managed Instance. This feature is now generally available. To learn more, review [Transactional Replication](replication-transactional-overview.md). To get started, review [Configure replication in Azure SQL Managed Instance](replication-between-two-instances-configure-tutorial.md). | 
| **Zone-redundancy preview** | It's now possible to deploy your managed instance to multiple availability zones to improve the availability of your instance. This feature is currently in preview and only available to instances in the Business Critical service tier. Review [High availability](../database/high-availability-sla.md) to learn more. |
| **Approximate percentiles preview** | Support has been added to quickly compute percentiles using approximate percentile aggregate functions for large datasets with acceptable rank-based error bounds. This feature is currently in preview. To learn more, review [Approx_Percentile_Cont](/sql/t-sql/functions/approx-percentile-cont-transact-sql) and [Approx_Percentile_Disc](/sql/t-sql/functions/approx-percentile-disc-transact-sql). | 
| **Automated TDE key rotation for CMK GA** | Automatically switch to a new key when using a customer-managed key (CMK) with TDE. This feature is now generally available. To learn more, review [Automated key rotation](../database/transparent-data-encryption-byok-overview.md#rotation-of-tde-protector). |
| **16 TB support in Business Critical GA** | Support for allocation up to 16 TB of space on SQL Managed Instance in the Business Critical service tier using the new memory optimized premium-series hardware. This feature is now generally available. Review [Resource limits](resource-limits.md#service-tier-characteristics) to learn more. | 
| **Data virtualization GA** | The data virtualization feature allows users to join locally stored relational data with data queried from external data sources, such as Azure Data Lake Storage Gen2 or Azure Blob Storage. This feature is now generally available. Review [Data virtualization](data-virtualization-overview.md) to learn more. |
| **Memory optimized premium-series hardware GA** | Deploy your SQL Managed Instance to the new memory optimized premium-series hardware to take advantage of the latest Intel Ice Lake CPUs. Memory optimized hardware offers higher memory to vCore ratio. This feature is now generally available. [Resource limits](resource-limits.md#service-tier-characteristics) to learn more. | 
| **GZRS backup storage option** | Combine the availability and resiliency of geo-redundancy and zone-redundancy when you choose the new backup storage redundancy option Geo-Zone-Redundant Storage (GZRS) for Azure SQL Managed Instance. This option is generally available. Review [automated backups](automated-backups-overview.md) to learn more. | 
| **Windows Auth for Azure Active Directory principals GA** | Kerberos authentication for Azure Active Directory (Azure AD) enables Windows Authentication access to Azure SQL Managed Instance. This feature is now generally available (GA). To learn more, review [Windows Auth for Azure Active Directory principals](winauth-azuread-overview.md). |
|**Query Store hints GA**| Use query hints to optimize your query execution via the OPTION clause. This feature is now generally available (GA). To learn more, review, [Query Store hints](/sql/relational-databases/performance/query-store-hints?view=azuresqldb-mi-current&preserve-view=true)| 
| **Premium-series hardware GA** | Deploy your SQL Managed Instance to the new premium-series hardware to take advantage of the latest Intel Ice Lake CPUs. The premium-series hardware is now generally available. See [Premium-series hardware](resource-limits.md#service-tier-characteristics) to learn more.   | 
| **SDK-style SQL projects**| Use [Microsoft.Build.Sql](https://www.nuget.org/packages/Microsoft.Build.Sql) for SDK-style SQL projects in the SQL Database Projects extension in Azure Data Studio or Visual Studio Code. This feature is currently in preview. To learn more, see [SDK-style SQL projects](/azure-data-studio/extensions/sql-database-project-extension-sdk-style-projects). | 
| **JavaScript & Python bindings**| Support for JavaScript and Python SQL bindings for Azure Functions is currently in preview. See [Azure SQL bindings for Azure Functions](/azure/azure-functions/functions-bindings-azure-sql) to learn more. | 
| **Data virtualization preview** | It's now possible to query data in external sources such as Azure Data Lake Storage Gen2 or Azure Blob Storage, joining it with locally stored relational data. This feature is currently in preview. To learn more, see [Data virtualization](data-virtualization-overview.md). |  
| **Managed Instance link guidance** | We've published a number of guides for using the [Managed Instance link feature](managed-instance-link-feature-overview.md), including how to [prepare your environment](managed-instance-link-preparation.md), [configure replication by using SSMS](managed-instance-link-use-ssms-to-replicate-database.md), [configure replication via scripts](managed-instance-link-use-scripts-to-replicate-database.md),  [fail over your database by using SSMS](managed-instance-link-use-ssms-to-failover-database.md), [fail over your database via scripts](managed-instance-link-use-scripts-to-failover-database.md) and some [best practices](managed-instance-link-best-practices.md) when using the link feature (currently in preview). |
| **Maintenance window GA, advance notifications preview** | The [maintenance window](../database/maintenance-window.md) feature is now generally available, allowing you to configure a maintenance schedule for your Azure SQL Managed Instance. It's also possible to receive advance notifications for planned maintenance events, which is currently in preview. Review [Maintenance window advance notifications (preview)](../database/advance-notifications.md) to learn more. |
| **Windows Auth for Azure Active Directory principals preview** | Windows Authentication for managed instances empowers customers to move existing services to the cloud while maintaining a seamless user experience, and provides the basis for infrastructure modernization. Learn more in [Windows Authentication for Azure Active Directory principals on Azure SQL Managed Instance](winauth-azuread-overview.md). |



## Known issues

The known issues content has moved to a dedicated [known issues in SQL Managed Instance](doc-changes-updates-known-issues.md) article. 


## Contribute to content

To contribute to the Azure SQL documentation, see the [Docs contributor guide](/contribute/).
