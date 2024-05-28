---
title: What's new?
titleSuffix: Azure SQL Managed Instance
description: Learn about the new features and documentation improvements for Azure SQL Managed Instance.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: wiassaf, mathoma
ms.date: 05/25/2024
ms.service: sql-managed-instance
ms.subservice: service-overview
ms.topic: whats-new
ms.custom: references_regions, ignite-2023, build-2024
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
|[Database watcher for Azure SQL](../database-watcher-overview.md)|Database watcher is a managed monitoring solution for database services in the Azure SQL family. Database watcher collects in-depth workload monitoring data to give you a detailed view of database performance, configuration, and health. Learn more about [database watcher](https://aka.ms/dbwatcher-preview-announcement).|
|[Endpoint policies](./service-endpoint-policies-configure.md) | Configure which Azure Storage accounts can be accessed from a SQL Managed Instance subnet. Grants an extra layer of protection against inadvertent or malicious data exfiltration.|
|[Free SQL Managed Instance](free-offer.md) | Try Azure SQL Managed Instance for free, for the first 12 months after you create your instance. |
|[Instance pools](instance-pools-overview.md) | Share resources between multiple instances in a pool within a single virtual machine. A convenient and cost-efficient way to migrate smaller SQL Server instances to the cloud, and the only way to deploy a 2-vCore managed instance. |
|[Link from SQL MI to SQL Server](managed-instance-link-feature-overview.md) | Configure a link from Azure SQL Managed Instance to SQL Server 2022. |
|[Microsoft Entra nonunique name support](../database/authentication-microsoft-entra-create-users-with-nonunique-names.md) |  The [CREATE USER](/sql/t-sql/statements/create-user-transact-sql) Transact-SQL (T-SQL) syntax has been extended to include `WITH OBJECT_ID` to support creating Microsoft Entra logins and users in Azure SQL Managed Instance that have nonunique names. |
|[Next-gen General Purpose](service-tiers-next-gen-general-purpose-use.md) | An architectural upgrade of the General Purpose service tier that uses managed disks for greater resource flexibility, and improved performance while maintaining the same baseline cost as the General Purpose service tier.  |
|[Maintenance window advance notifications](advance-notifications.md)| Advance notifications for instance [maintenance window](maintenance-window.md).  |
|[SDK-style SQL project](/sql/azure-data-studio/extensions/sql-database-project-extension-sdk-style-projects) | Use [Microsoft.Build.Sql](https://www.nuget.org/packages/Microsoft.Build.Sql) for SDK-style SQL projects in the SQL Database Projects extension in Azure Data Studio or Visual Studio Code. SDK-style SQL projects are especially advantageous for applications shipped through pipelines or built in cross-platform environments.|
|[Service Broker](/sql/database-engine/configure-windows/sql-server-service-broker) | Support for cross-instance message exchange using Service Broker between instances of Azure SQL Managed Instance, and between SQL Server and Azure SQL Managed Instance. |
|[Two-way DR with SQL Server 2022](managed-instance-link-disaster-recovery.md) | In the event of a disaster, you can fail your SQL Server 2022 workloads to Azure SQL Managed Instance using the link, and then, once the disaster is mitigated, you can fail back to SQL Server. |
|[Threat detection](threat-detection-configure.md) | Threat detection notifies you of security threats detected to your database. |
|[Zone redundancy for General Purpose](high-availability-sla.md#zone-redundant-availability) |  Deploy your General Purpose SQL Managed Instance to multiple availability zones to improve the availability of your instance in the event of a disaster. | 


## General availability (GA)

The following table lists features of Azure SQL Managed Instance that have been made generally available (GA) within the last 12 months:

| Feature | GA Month | Details |
| ---| --- |--- |
|[Update policy](update-policy.md) | May 2024 | Use the update policy to control your internal database format alignment and access to the latest SQL Database Engine features. You can choose to either limit the feature set to features that are available in SQL Server 2022, or ensure your instance takes advantage of all the latest features of Azure SQL Managed Instance.| 
|[Azure SQL triggers for Azure Functions](/azure/azure-functions/functions-bindings-azure-sql) | March 2024 | Azure Functions supports function triggers for Azure SQL Managed Instance. | 
|[Cross-subscription database copy and move](database-copy-move-how-to.md)| December 2023 | Refresh of database copy and move functionality with added support for cross-subscription operations. |
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
|[License-free standby replica](failover-group-standby-replica-how-to-configure.md) | May 2023 | Save on licensing costs when you designate your geo-secondary replica as **Standby**. | 
|[CREATE EXTERNAL TABLE AS SELECT (CETAS)](/sql/t-sql/statements/create-external-table-as-select-transact-sql?view=azuresqldb-mi-current&preserve-view=true) | April 2023 | You can use CETAS to create an external table on top of Parquet or CSV files Azure Blob storage or Azure Data Lake Storage (ADLS) Gen2. CETAS can also export, in parallel, the results of a T-SQL SELECT statement into the created external table. There is potential for data exfiltration risk with these capabilities, so CETAS is disabled by default for Azure SQL Managed Instance. To enable, see [CREATE EXTERNAL TABLE AS SELECT (CETAS)](/sql/t-sql/statements/create-external-table-as-select-transact-sql?view=azuresqldb-mi-current&preserve-view=true#enable-cetas-via-azure-powershell). |
|[One-way Managed Instance link for SQL Server 2016 & 2019](managed-instance-link-feature-overview.md)| April 2023 | Online replication of SQL Server databases hosted anywhere to Azure SQL Managed Instance. |
|[SQL Database Projects extension](/azure-data-studio/extensions/sql-database-project-extension) | April 2023 | An extension to develop databases for Azure SQL Database with Azure Data Studio and Visual Studio Code. A SQL project is a local representation of SQL objects that comprise the schema for a single database, such as tables, stored procedures, or functions. |
|[Shrink Database](/sql/t-sql/database-console-commands/dbcc-shrinkdatabase-transact-sql?view=azuresqldb-mi-current&preserve-view=true) and [Shrink File with Low Priority](/sql/t-sql/database-console-commands/dbcc-shrinkfile-transact-sql?view=azuresqldb-mi-current&preserve-view=true) | March 2023 | This feature solves the concurrency issues that can arise from shrink database and shrink file commands, especially during active maintenance or on busy OLTP environments. In WAIT_AT_LOW_PRIORITY mode, necessary tasks to shrink database files can be completed without negatively affecting application query performance. |
|[Approximate percentile](/sql/t-sql/functions/approx-percentile-cont-transact-sql)| March 2023 | Quickly compute percentiles for a large dataset with acceptable rank-based error bounds to help make rapid decisions by using approximate percentile aggregate functions. | 

## November 2022 feature wave

The features and benefits provided by the November 2022 feature wave are now generally available. All new instances in eligible subnets in both dev/test and production subscriptions are eligible for the feature wave. Existing instances will be enrolled gradually as part of regular updates during configured maintenance windows. To learn more, see [November 2022 feature wave](november-2022-feature-wave-enroll.md).

## Documentation changes

Learn about significant changes to the Azure SQL Managed Instance documentation. For previous years, see the [What's new archive](doc-changes-updates-release-notes-whats-new-archive.md).

### May 2024

| Changes | Details |
| --- | --- |
| **Instance pool in the portal** | It's now possible to create a new instance pool, or a new instance inside an existing instance pool, by using the Azure portal. Review [Configure instance pool](instance-pools-configure.md?view=azure-portal&preserve-view=true#create-instance-pool) to learn more. The instance pool feature remains in preview.  |
|**Update policy GA** | Use the update policy to control your internal database format alignment and access to the latest SQL Database Engine features. You can choose to either limit the feature set to features that are available in SQL Server 2022, or ensure your instance takes advantage of all the latest features of Azure SQL Managed Instance. This feature is generally available. Review [Update policy](update-policy.md) to learn more.  | 


### March 2024

| Changes | Details |
| --- | --- |
|**Azure SQL triggers for Azure Functions GA** | Azure Functions supports function triggers for Azure SQL Managed Instance. This feature is now generally available. Review [Azure SQL triggers for Azure Functions](/azure/azure-functions/functions-bindings-azure-sql) to learn more. |
| **Database watcher for Azure SQL preview** | [Database watcher](../database-watcher-overview.md) is a managed monitoring solution for database services in the Azure SQL family. Database watcher collects in-depth workload monitoring data to give you a detailed view of database performance, configuration, and health. This feature is now in preview.  Learn more about [database watchers](https://aka.ms/dbwatcher-preview-announcement).|
|**Next-gen General Purpose preview** |  An architectural upgrade of the General Purpose service tier that uses managed disks for greater resource flexibility, and improved performance while maintaining the same baseline cost as the General Purpose service tier. This service tier upgrade is currently in preview. Review [Next-gen General Purpose](service-tiers-next-gen-general-purpose-use.md) to learn more.   |

### February 2024

| Changes | Details |
| --- | --- |
| **OBJECT_ID T-SQL syntax preview** | The [CREATE USER](/sql/t-sql/statements/create-user-transact-sql) Transact-SQL (T-SQL) syntax has been extended to include `WITH OBJECT_ID` to support creating Microsoft Entra logins and users in Azure SQL Managed Instance that have nonunique names. Using the `OBJECT_ID` syntax to create users and logins in Azure SQL Managed Instance is currently in preview. To learn more, review [Microsoft Entra nonunique name support](../database/authentication-microsoft-entra-create-users-with-nonunique-names.md). | 

### January 2024

| Changes | Details |
| --- | --- |
|**Instance pool preview refresh** | Instance pools have a number of additional capabilities, such as the ability to deploy a 2-vCore instance. The preview of this feature has been refreshed. Review [instance pools](instance-pools-overview.md#whats-new) to learn more. |


### December 2023

| Changes | Details |
| --- | --- |
|**Free Azure SQL Managed Instance preview** |  Try Azure SQL Managed Instance for free, for the first 12 months after you create your instance. This free offer provides a General Purpose instance with up to 100 databases, and 720 vCore hours of compute every month. This offer is currently in preview. Review [Free SQL Managed Instance offer](free-offer.md) to learn more. |

### November 2023

| Changes | Details |
| --- | --- |
|**Database copy and move GA**| Perform an online database copy or move operation across managed instances. This feature is now generally available. Review [Database copy and move](database-copy-move-how-to.md) to get started. |
|**Distributed Transaction Coordinator (DTC) GA** | Use DTC to run distributed transactions in mixed environments such as across managed instances, SQL Servers, other relational database management systems (RDBMSs), custom applications and other transaction participants hosted in any environment that can establish network connectivity to Azure. This feature is now generally available. Review [Distributed Transaction Coordinator (DTC)](distributed-transaction-coordinator-dtc.md) to get started. | 
|**Ledger GA** | Ledger in Azure SQL Managed Instance allows you to cryptographically attest to other parties, such as auditors or other business parties, that your data hasn't been tampered with. This feature is now generally available. Review [Ledger](/sql/relational-databases/security/ledger/ledger-overview) to learn more. | 
|**Link from SQL MI to SQL Server 2022 preview** |  Configure a link from Azure SQL Managed Instance to SQL Server 2022. This feature is now in preview. Review [Managed Instance link overview](managed-instance-link-feature-overview.md) to learn more. |
|**Instance stop and start GA** | Stop and start your managed instance to save on licensing and compute costs. This feature is now generally available. Review [instance stop and start](instance-stop-start-how-to.md) to learn more.  |
|**November 2022 feature wave GA**| November 2022 brought a number of new features for Azure SQL Managed Instance, such as [fast provisioning](management-operations-overview.md#fast-provisioning), and [zone redundancy](high-availability-sla.md#zone-redundant-availability) as well as enhancements to the [virtual cluster](virtual-cluster-architecture.md) and [network security](connectivity-architecture-overview.md).  All the features and benefits introduced by the November 2022 feature wave are now generally available. Review [November 2022 Feature wave](november-2022-feature-wave-enroll.md) to learn more. | 
| **SQL Server DR to SQL MI preview** |  In the event of a disaster, you can recover your SQL Server 2022 database by failing over to SQL Managed Instance, and then failing back to SQL Server 2022 online by using the Managed Instance link. This feature has moved from a limited public preview that required signing up to an open public preview available to everyone. Review [Online disaster recovery for SQL Server 2022](managed-instance-link-disaster-recovery.md) to learn more. |
|**Zone-redundancy for Business Critical GA**| Deploy your Business Critical SQL Managed Instance to multiple availability zones to improve the availability of your instance in the event of a disaster. This capability is now generally available. Review [Zone-redundancy](../managed-instance/high-availability-sla.md#zone-redundant-availability) to learn more. |
|**Zone-redundancy for General Purpose preview**| Deploy your General Purpose SQL Managed Instance to multiple availability zones to improve the availability of your instance in the event of a disaster. This capability is now in preview. Review [Zone-redundancy](../managed-instance/high-availability-sla.md#zone-redundant-availability) to learn more. |

## Archive

For previous news, see the [What's new archive](doc-changes-updates-release-notes-whats-new-archive.md).

## Known issues

The known issues content has moved to a dedicated [known issues in SQL Managed Instance](doc-changes-updates-known-issues.md) article. 


## Contribute to content

To contribute to the Azure SQL documentation, see the [Docs contributor guide](/contribute/).
