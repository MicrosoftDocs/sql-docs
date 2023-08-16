---
title: What's new?
titleSuffix: Azure SQL Database
description: Learn about the new features and documentation improvements for Azure SQL Database.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: mathoma
ms.date: 08/04/2023
ms.service: sql-database
ms.subservice: service-overview
ms.topic: whats-new
ms.custom:
  - "sqldbrb=2"
  - "references_regions"
---
# What's new in Azure SQL Database?

[!INCLUDE[appliesto-sqldb](../includes/appliesto-sqldb.md)]

> [!div class="op_single_selector"]
> * [Azure SQL Database](doc-changes-updates-release-notes-whats-new.md?view=azuresql-db&preserve-view=true)
> * [Azure SQL Managed Instance](../managed-instance/doc-changes-updates-release-notes-whats-new.md?view=azuresql-mi&preserve-view=true)

This article summarizes the documentation changes associated with new features and improvements in the recent releases of [Azure SQL Database](https://azure.microsoft.com/products/azure-sql/database/). To learn more about Azure SQL Database, see [What is Azure SQL Database?](sql-database-paas-overview.md).

## Preview

The following table lists the features of Azure SQL Database that are currently in preview.

> [!NOTE]  
> Features currently in preview are available under [supplemental terms of use](https://azure.microsoft.com/support/legal/preview-supplemental-terms/), review for legal terms that apply to Azure features that are in beta, preview, or otherwise not yet released into general availability. Azure SQL Database provides previews to give you a chance to evaluate and [share feedback with the product group](https://feedback.azure.com/d365community/forum/ef2b2b38-2f25-ec11-b6e6-000d3a4f0f84) on features before they become generally available (GA).

| Feature | Details |
| ---| --- |
| [Always Encrypted with VBS enclaves](always-encrypted-enclaves-getting-started-vbs.md) | Take advantage of rich confidential queries and in-place cryptographic operations for Azure SQL Database with Always Encrypted with virtualization-based security (VBS) enclaves. | 
| [Azure SQL triggers for Azure Functions](/azure/azure-functions/functions-bindings-azure-sql-trigger) | Azure Functions supports function triggers for the Azure SQL and SQL Server products. | 
| [Database level CMK with TDE](transparent-data-encryption-byok-database-level-overview.md) | Database level CMK allows setting the TDE protector as a customer-managed key individually for each database within the server. |
| [DC-series hardware up to 40 vCores](resource-limits-vcore-single-databases.md) | DC-series hardware from 10 to 40 vCores is now in preview for in General Purpose, Business Critical, and Hyperscale provisioned compute. |
| [Degrees of Parallelism (DOP) feedback](/sql/relational-databases/performance/intelligent-query-processing-feedback#degree-of-parallelism-dop-feedback) | DOP Feedback is currently available as a limited preview. For more information and how to apply for the preview, see [Announcing Degree of Parallelism Feedback Limited Preview](https://techcommunity.microsoft.com/t5/azure-sql-blog/announcing-degree-of-parallelism-feedback-limited-preview/ba-p/3806924). |
| [Elastic jobs](elastic-jobs-overview.md) | The elastic jobs feature is the SQL Server Agent replacement for Azure SQL Database as a PaaS offering.  |
| [Elastic queries](elastic-query-overview.md) | The elastic queries feature allows for cross-database queries in Azure SQL Database. |
| [Elastic transactions](elastic-transactions-overview.md) | Elastic transactions allow you to execute transactions distributed among cloud databases in Azure SQL Database. |
| [Hyperscale elastic pools](hyperscale-elastic-pool-overview.md) | Manage and scale multiple Hyperscale databases in Azure SQL Database by using Hyperscale elastic pools. | 
| [Hyperscale long-term retention](long-term-retention-overview.md) | Save your Hyperscale database backups for up to 10 years with long-term retention (LTR).  | 
| [Hyperscale RA-GZRS](hyperscale-automated-backups-overview.md#data-and-backup-storage-redundancy) | Store your Hyperscale database backups on read access geo-zone-redundancy (RA-GZRS) storage. |
| [Hyperscale short-term retention](hyperscale-automated-backups-overview.md#data-and-backup-storage-redundancy) | Retain backups from 1 up to 35 days for Hyperscale databases, and perform a point-in-time restore within the configured retention period. |
| [Invoke External REST endpoints](/sql/relational-databases/system-stored-procedures/sp-invoke-external-rest-endpoint-transact-sql) | Invoke HTTPS REST endpoint natively using a new system stored procedure. |
| [JavaScript & Python bindings](/azure/azure-functions/functions-bindings-azure-sql)| Use JavaScript or Python SQL bindings with Azure Functions. | 
| [Maintenance window advance notifications](../database/advance-notifications.md)| Advance notifications are available for databases configured to use a non-default [maintenance window](maintenance-window.md). Advance notifications for maintenance windows are in public preview for Azure SQL Database. |
| [Query editor in the Azure portal](query-editor.md) | The query editor in the portal allows you to run queries against your Azure SQL Database directly from the [Azure portal](https://portal.azure.com).|
| [Query Store hints](/sql/relational-databases/performance/query-store-hints?view=azuresqldb-current&preserve-view=true) | Use query hints to optimize your query execution via the OPTION clause. |
| [Reverse migrate from Hyperscale](manage-hyperscale-database.md#reverse-migrate-from-hyperscale) | Reverse migration to the General Purpose service tier allows customers who have recently migrated an existing database in Azure SQL Database to the Hyperscale service tier to move back in an emergency, should Hyperscale not meet their needs. While reverse migration is initiated by a service tier change, it's essentially a size-of-data move between different architectures. |
| [Serverless Hyperscale](serverless-tier-overview.md) | Automatically scale your Hyperscale databases up and down based on usage when using the serverless compute tier. | 
| [SQL Analytics](/azure/azure-monitor/insights/azure-sql)|Azure SQL Analytics is an advanced cloud monitoring solution for monitoring performance of all of your Azure SQL databases at scale and across multiple subscriptions in a single view. Azure SQL Analytics collects and visualizes key performance metrics with built-in intelligence for performance troubleshooting.|
| [SQL Database emulator](local-dev-experience-sql-database-emulator.md) | The Azure SQL Database emulator provides the ability to locally validate database and query design together with client application code in a simple and frictionless model as part of the application development process. | 
| [SQL Database Projects extension](/sql/azure-data-studio/extensions/sql-database-project-extension) | An extension to develop databases for Azure SQL Database with Azure Data Studio and Visual Studio Code. A SQL project is a local representation of SQL objects that comprise the schema for a single database, such as tables, stored procedures, or functions. |

## General availability (GA)

The following table lists the new generally available (GA) features of Azure SQL Database, and those that have transitioned from preview to GA within the last 12 months:

| Feature | GA Month | Details |
| ---| --- |--- |
|  [XML compression](https://techcommunity.microsoft.com/t5/azure-sql-blog/general-availability-of-xml-compression-for-azure-sql-database/ba-p/3888861) | August 2023 | XML compression for Azure SQL Database is now generally available. You can use [ALTER INDEX](/sql/t-sql/statements/alter-index-transact-sql?view=azuresqldb-current&preserve-view=true#xml_compression) to apply XML compression to existing [XML indexes](/sql/relational-databases/xml/xml-indexes-sql-server?view=azuresqldb-current&preserve-view=true). |
| [TDS 8.0 support](/sql/relational-databases/security/networking/tds-8?view=azuresqldb-current&preserve-view=true) | August 2023 | Azure SQL Database now supports TDS 8.0 for strict encryption of data in transit. |
| [Hyperscale premium-series and premium-series memory optimized hardware](service-tier-hyperscale.md#compute-resources) | July 2023 | Premium-series and premium-series memory optimized hardware is now available for Hyperscale databases.|
| [128 vCore](resource-limits-vcore-single-databases.md#general-purpose---provisioned-compute---gen5) | June 2023 | Provision your Azure SQL Database with up to [128 vCores, now generally available](https://techcommunity.microsoft.com/t5/azure-sql-blog/announcing-preview-of-128-vcore-provisioned-compute-size-on/ba-p/3631211). | 
| [Azure SQL bindings for Azure Functions](/azure/azure-functions/functions-bindings-azure-sql-trigger) | May 2023 | Azure Functions supports input and output bindings for the Azure SQL and SQL Server products. | 
| [Cross-tenant CMK with TDE](transparent-data-encryption-byok-cross-tenant.md) | May 2023 | Cross-tenant CMK with TDE allows SQL databases to be in a separate tenant than the tenant holding the Azure Key Vault resource used to encrypt the databases. |
| [UMI for auditing](auditing-overview.md) | April 2023 | Configure the storage account for your SQL auditing logs by using User-assigned Managed Identity (UMI). | 
| [Shrink Database](/sql/t-sql/database-console-commands/dbcc-shrinkdatabase-transact-sql?view=azuresqldb-current&preserve-view=true) and [Shrink File with Low Priority](/sql/t-sql/database-console-commands/dbcc-shrinkfile-transact-sql?view=azuresqldb-current&preserve-view=true) | March 2023 | This feature solves the concurrency issues that can arise from shrink database and shrink file commands, especially during active maintenance or on busy OLTP environments. In WAIT_AT_LOW_PRIORITY mode, necessary tasks to shrink database files can be completed without negatively affecting application query performance. |
| [Approximate percentile](/sql/t-sql/functions/approx-percentile-cont-transact-sql) | March 2023 | Quickly compute percentiles for a large dataset with acceptable rank-based error bounds to help make rapid decisions by using approximate percentile aggregate functions. |
| [Optimized locking](/sql/relational-databases/performance/optimized-locking) | February 2023 | Optimized locking is a new Database Engine capability that offers an improved locking mechanism that reduces lock memory consumption and blocking amongst concurrent transactions. This fundamentally improves concurrency and lowers lock memory. |
| [Azure Synapse Link for Azure SQL Database](/azure/synapse-analytics/synapse-link/sql-synapse-link-overview) | November 2022|  Azure Synapse Link for SQL enables near real time analytics over operational data in Azure SQL Database or SQL Server 2022. |
| [Restore progress](/sql/relational-databases/system-dynamic-management-views/sys-dm-operation-status-azure-sql-database) |November 2022 | Restore progress tracked in the `percent_complete` column sys.dm_operation_status. | 
| [Time series](/sql/t-sql/functions/generate-series-transact-sql) |November 2022 | Generates a series of numbers within a given interval.  Review [GENERATE_SERIES](/sql/t-sql/functions/generate-series-transact-sql) and [DATE_BUCKET](/sql/t-sql/functions/date-bucket-transact-sql) to learn more. | 
|[Automated key rotation for TDE with CMK](../database/transparent-data-encryption-byok-overview.md#rotation-of-tde-protector) | October 2022 | Automatically switch to a new key when using a customer-managed key (CMK) for TDE with Azure SQL Database. | 
| [Database copy from Hyperscale replica](service-tier-hyperscale.md) | October 2022 | Use a Hyperscale geo-secondary replica as source for a database copy. | 
| [Reverse migrate from Hyperscale](manage-hyperscale-database.md#reverse-migrate-from-hyperscale) | September 2022 | Reverse migration to the General Purpose service tier allows customers who have recently migrated an existing database in Azure SQL Database to the Hyperscale service tier to move back in an emergency, should Hyperscale not meet their needs. While reverse migration is initiated by a service tier change, it's essentially a size-of-data move between different architectures. |
| [Zone redundant configuration for Hyperscale databases](high-availability-sla.md#zone-redundant-availability) | August 2022 |The zone redundant configuration feature utilizes [Azure Availability Zones](/azure/availability-zones/az-overview#availability-zones) to replicate databases across multiple physical locations within an Azure region. By selecting [zone redundancy](high-availability-sla.md#zone-redundant-availability), you can make your Hyperscale databases resilient to a much larger set of failures, including catastrophic datacenter outages, without any changes to the application logic.|
| [Query Store hints](/sql/relational-databases/performance/query-store-hints?view=azuresqldb-current&preserve-view=true) | August 2022 | Use query hints to optimize your query execution via the OPTION clause. |
| [Named Replicas](service-tier-hyperscale-replicas.md#named-replica) for Hyperscale databases | June 2022 | Named Replicas enable a broad variety of read scale-out scenarios, and easily implement near-real time hybrid transactional and analytical processing (HTAP) solutions. |
| [Active geo-replication](./active-geo-replication-overview.md) and [Auto-failover groups](./auto-failover-group-sql-db.md) for Hyperscale databases | June 2022 | Active geo-replication and Auto-failover groups provide a turn-key business continuity solution for Hyperscale databases, letting you perform quick disaster recovery of databases in case of a regional disaster or a large scale outage.|
| [Ledger](/sql/relational-databases/security/ledger/ledger-overview) | May 2022 | The ledger feature in Azure SQL Database allows you to cryptographically attest to other parties, such as auditors or other business parties, that your data hasn't been tampered with. |
| [Change data capture](/sql/relational-databases/track-changes/about-change-data-capture-sql-server) | April 2022 | Change data capture (CDC) lets you track all the changes that occur on a database. Though this feature has been available for SQL Server for quite some time, using it with Azure SQL Database is now generally available. |
| [Zone redundant configuration for General Purpose tier](high-availability-sla.md#zone-redundant-availability) | April 2022 | The zone redundant configuration feature utilizes [Azure Availability Zones](/azure/availability-zones/az-overview#availability-zones) to replicate databases across multiple physical locations within an Azure region. By selecting [zone redundancy](high-availability-sla.md#zone-redundant-availability), you can make your provisioned and serverless General Purpose databases and elastic pools resilient to a much larger set of failures, including catastrophic datacenter outages, without any changes to the application logic.|
| [Maintenance window](../database/maintenance-window.md)| March 2022 | The maintenance window feature allows you to configure maintenance schedule for your Azure SQL Database. [Maintenance window advance notifications](../database/advance-notifications.md), however, are in preview.|
| [Storage redundancy for Hyperscale databases](hyperscale-automated-backups-overview.md#data-and-backup-storage-redundancy) | March 2022 |  When creating a Hyperscale database, you can choose your preferred storage type: read-access geo-redundant storage (RA-GRS), zone-redundant storage (ZRS), or locally redundant storage (LRS) Azure standard storage. The selected storage redundancy option will be used for the lifetime of the database for both data storage redundancy and backup storage redundancy. |
| [Azure Active Directory-only authentication](authentication-azure-ad-only-authentication.md) | November 2021 | It's possible to configure your Azure SQL Database to allow authentication only from Azure Active Directory. | 

## Documentation changes

Learn about significant changes to the Azure SQL Database documentation. For previous years, see the [What's new archive](doc-changes-updates-release-notes-whats-new-archive.md). 

### August 2023

| Changes | Details |
| --- | --- |
| **XML compression** | [XML compression](https://techcommunity.microsoft.com/t5/azure-sql-blog/general-availability-of-xml-compression-for-azure-sql-database/ba-p/3888861) for Azure SQL Database is now generally available. You can use [ALTER INDEX](/sql/t-sql/statements/alter-index-transact-sql?view=azuresqldb-current&preserve-view=true#xml_compression) to apply XML compression to existing [XML indexes](/sql/relational-databases/xml/xml-indexes-sql-server?view=azuresqldb-current&preserve-view=true). |
| **TDS 8.0** | Azure SQL Database now supports [TDS 8.0](/sql/relational-databases/security/networking/tds-8?view=azuresqldb-current&preserve-view=true) for strict encryption of data in transit. |

### July 2023

| Changes | Details |
| --- | --- |
| **Hyperscale premium-series and premium-series memory optimized hardware** | Premium-series and premium-series memory optimized hardware is [now available for Hyperscale databases](service-tier-hyperscale.md#compute-resources).|
| **DC-series hardware up to 10 vCores** | [DC-series hardware up to 40 vCores](resource-limits-vcore-single-databases.md) is now in preview for in General Purpose, Business Critical, and Hyperscale provisioned compute. This supports [Always Encrypted on Azure SQL Database with secure enclaves with up to 40 vCores](https://techcommunity.microsoft.com/t5/azure-sql-blog/always-encrypted-with-secure-enclaves-dc-series-databases-with/ba-p/3872338). |

### June 2023

| Changes | Details |
| --- | --- |
| **128 vCore** | Provision your Azure SQL Database with up to [128 vCores](resource-limits-vcore-single-databases.md), [now generally available](https://techcommunity.microsoft.com/t5/azure-sql-blog/announcing-preview-of-128-vcore-provisioned-compute-size-on/ba-p/3631211). | 
| **64 vCore option for Hyperscale premium** | A 64 vCore option is now available for [Azure SQL Database Hyperscale premium-series](resource-limits-vcore-single-databases.md#hyperscale---provisioned-compute---premium-series) and [Hyperscale premium-series memory optimized](resource-limits-vcore-single-databases.md#hyperscale---provisioned-compute---premium-series-memory-optimized).|

### May 2023

| Changes | Details |
| --- | --- |
| **Azure SQL bindings for Azure Functions GA** | Azure Functions supports input bindings, and output bindings for the Azure SQL and SQL Server products. This feature is now generally available. Review [Azure SQL bindings for Azure Functions](/azure/azure-functions/functions-bindings-azure-sql) to learn more.  | 
| **Azure SQL triggers for Azure Functions preview** | Azure Functions supports function triggers for the Azure SQL and SQL Server products. This feature is currently in preview. Review [Azure SQL triggers for Azure Functions](/azure/azure-functions/functions-bindings-azure-sql-trigger) to learn more. | 
| **Cross-tenant CMK with TDE GA** |  Cross-tenant CMK with TDE allows SQL databases to be in a separate tenant than the tenant holding the Azure Key Vault resource used to encrypt the databases. This feature is now generally available. To learn more, review [Cross-tenant CMK with TDE](transparent-data-encryption-byok-cross-tenant.md). |
| **Hyperscale elastic pools preview**| Manage and scale multiple Hyperscale databases in Azure SQL Database by using Hyperscale elastic pools.  This feature is now in preview. See [Hyperscale elastic pools](hyperscale-elastic-pool-overview.md) to learn more. | 
| **Optimized locking available in Hyperscale GA** | Optimized locking is a new Database Engine capability that offers an improved locking mechanism that reduces lock memory consumption and blocking amongst concurrent transactions. This fundamentally improves concurrency and lowers lock memory. Optimized locking is now available in all DTU and vCore service tiers, including provisioned and serverless. This feature is generally available. For more information, see [Optimized locking](/sql/relational-databases/performance/optimized-locking). |

### April 2023

| Changes | Details |
| --- | --- |
| DOP Feedback preview | [Degrees of Parallelism (DOP) Feedback](/sql/relational-databases/performance/intelligent-query-processing-feedback#dop-feedback) is currently available as a limited preview. For more information and how to apply for the preview, see [Announcing Degree of Parallelism Feedback Limited Preview](https://techcommunity.microsoft.com/t5/azure-sql-blog/announcing-degree-of-parallelism-feedback-limited-preview/ba-p/3806924). |

### March 2023

| Changes | Details |
| --- | --- |
| **Approximate Percentile GA** | Quickly compute percentiles for a large dataset with acceptable rank-based error bounds to help make rapid decisions by using approximate percentile aggregate. This feature is generally available now. Review  [Approximate percentile](/sql/t-sql/functions/approx-percentile-cont-transact-sql) to learn more.  | 
| **Shrink Database / Shrink File with Low Priority GA** | This feature solves the concurrency issues that can arise from shrink database and shrink file commands, especially during active maintenance or on busy OLTP environments. In WAIT_AT_LOW_PRIORITY mode, necessary tasks to shrink database files can be completed without negatively affecting application query performance. Review [Shrink Database](/sql/t-sql/database-console-commands/dbcc-shrinkdatabase-transact-sql?view=azuresqldb-current&preserve-view=true) and [Shrink File with Low Priority](/sql/t-sql/database-console-commands/dbcc-shrinkfile-transact-sql?view=azuresqldb-current&preserve-view=true). |
| **Database level CMK with TDE preview** | Previously, TDE with CMK was set at the server level, and was inherited by all encrypted databases associated with that server. Database level CMK allows setting the TDE protector as a customer-managed key individually for each database within the server. This feature is currently in preview. Review [Transparent data encryption (TDE) with customer-managed keys at the database level](transparent-data-encryption-byok-database-level-overview.md) to learn more. |

### February 2023

| Changes | Details |
| --- | --- |
| **Azure SQL Database high availability and disaster recovery checklist** | [This guide provides a detailed review of proactive steps you can take](high-availability-disaster-recovery-checklist.md) to maximize availability, ensure recovery, and prepare for Azure outages. |
| **Always Encrypted with VBS enclaves preview** | Take advantage of rich confidential queries and in-place cryptographic operations for Azure SQL Database with Always Encrypted with virtualization-based security (VBS) enclaves. This feature is currently in preview. Review [Always Encrypted with VBS enclaves](always-encrypted-enclaves-getting-started-vbs.md) to learn more.  | 
| **Optimized locking GA** | Optimized locking is a new Database Engine capability that offers an improved locking mechanism that reduces lock memory consumption and blocking amongst concurrent transactions. This fundamentally improves concurrency and lowers lock memory. This feature is generally available. For more information, see [Optimized locking](/sql/relational-databases/performance/optimized-locking). |
|**Cross-tenant CMK with TDE preview** | Cross-tenant CMK with TDE allows SQL databases to be in a separate tenant than the tenant holding the Azure Key Vault resource used to encrypt the databases. This feature is currently in preview. Review [Cross-tenant CMK with TDE](transparent-data-encryption-byok-cross-tenant.md) to learn more. 
| **Serverless Hyperscale preview** | It's now possible to deploy your Hyperscale databases to the serverless compute tier. This feature is currently in preview. To learn more, see [serverless compute](serverless-tier-overview.md). |

### 2022


| Changes | Details |
| --- | --- |
| **128 vCore preview** | It's now possible to provision your Azure SQL Database with up to 128 vCores in both the General Purpose, and Business Critical service tiers. See [resource limits](resource-limits-vcore-single-databases.md#general-purpose---provisioned-compute---gen5) to learn more. | 
| **Azure Synapse Link for SQL GA** | Azure Synapse Link for SQL, now generally available, enables near real-time analytics over operational data in SQL Server 2022 and Azure SQL Database. With a seamless integration between operational stores and Azure Synapse Analytics dedicated SQL pools, Azure Synapse Link for SQL enables you to run analytics, business intelligence and machine learning scenarios on your operational data with minimum impact on source databases with a new change feed technology. For more information, see [What is Azure Synapse Link for SQL?](/azure/synapse-analytics/synapse-link/sql-synapse-link-overview).|
| **Gen5 hardware rename** | The Gen5 hardware in the vCore purchasing model has been renamed to **standard-series (Gen5)**. | 
| **Hyperscale premium-series and premium-series memory optimized hardware preview** | Premium-series and premium-series memory optimized hardware is [in preview for Hyperscale databases](service-tier-hyperscale.md#compute-resources). For more information, read the [Premium-series announcement blog post](https://aka.ms/AAiq28n).|
| **Invoke external REST endpoints preview** | It's now possible to call an HTTPS REST endpoint natively, using a new system stored procedure. This feature is currently in preview. To learn more, review [sp_invoke_external_rest_endpoint](/sql/relational-databases/system-stored-procedures/sp-invoke-external-rest-endpoint-transact-sql). |
| **Restore progress GA** | Tracking the progress of your restore by using the `percent_complete` column [sys.dm_operation_status](/sql/relational-databases/system-dynamic-management-views/sys-dm-operation-status-azure-sql-database) is now generally available. For more information, read the [Restore progress announcement blog post](https://aka.ms/RestoreProgressGA). |
| **Time series GA** | Generates a series of numbers within a given interval. This feature is generally available.  Review [GENERATE_SERIES](/sql/t-sql/functions/generate-series-transact-sql) and [DATE_BUCKET](/sql/t-sql/functions/date-bucket-transact-sql) to learn more. 
| **Approximate percentiles preview** | Support has been added to quickly compute percentiles using approximate percentile aggregate functions for large datasets with acceptable rank-based error bounds. This feature is currently in preview. To learn more, review [Approx_Percentile_Cont](/sql/t-sql/functions/approx-percentile-cont-transact-sql) and [Approx_Percentile_Disc](/sql/t-sql/functions/approx-percentile-disc-transact-sql). | 
| **Automated TDE key rotation for CMK GA** | Automatically switch to a new key when using a customer-managed key (CMK) with TDE. This feature is now generally available. To learn more, review [Automated key rotation](../database/transparent-data-encryption-byok-overview.md#rotation-of-tde-protector). |
| **Database copy of Hyperscale geo secondary replicas GA** | It's now possible to use a Hyperscale geo-secondary replica as source for a database copy. This feature is generally available. For more information, see [Hyperscale service tier](service-tier-hyperscale.md). |
| **Cross-subscription failover group with Azure PowerShell** | It's now possible to deploy your auto-failover group for a single database across subscriptions by using Azure PowerShell. To learn more, review [Configure auto-failover group](auto-failover-group-configure-sql-db.md?view=azuresql&tabs=azure-powershell&pivots=azure-sql-single-db&preserve-view=true#create-failover-group). |
| **Hyperscale databases LTR preview** | It's now possible to store your Hyperscale database backups for up to 10 years using the long-term retention (LTR) capability. This feature is now in preview. To learn more, review [long-term retention](long-term-retention-overview.md).  | 
| **Hyperscale RA-GZRS preview** | It's now possible to choose read access geo-zone-redundancy (RA-GZRS) as a backup storage redundancy for Hyperscale databases. This feature is currently in preview. To learn more, review [Hyperscale backup storage redundancy](hyperscale-automated-backups-overview.md#data-and-backup-storage-redundancy). |
| **Hyperscale reverse migrate GA** |  This feature allows customers who have recently migrated an existing database in Azure SQL Database to the Hyperscale service tier to move back in an emergency, should Hyperscale not meet their needs. While reverse migration is initiated by a service tier change, it's essentially a size-of-data move between different architectures. This feature is now generally available. To learn more, review [Reverse migration to the General Purpose service tier](manage-hyperscale-database.md#reverse-migrate-from-hyperscale).|
| **UMI support for auditing preview** | It's now possible to configure the storage account used for SQL auditing logs by using User Managed Identity (UMI). This feature is currently in preview. Review [auditing](auditing-overview.md) to learn more. |
| **Zone redundant configuration for Hyperscale databases GA** | The zone redundant configuration feature utilizes [Azure Availability Zones](/azure/availability-zones/az-overview#availability-zones) to replicate databases across multiple physical locations within an Azure region. By selecting [zone redundancy](high-availability-sla.md#zone-redundant-availability), you can make your Hyperscale databases resilient to a much larger set of failures, including catastrophic datacenter outages, without any changes to the application logic. This configuration option is now generally available. To learn more, review  [Zone redundant configuration for Hyperscale databases](high-availability-sla.md#zone-redundant-availability). |
| **Query Store hints GA** | You can use query hints to optimize your query execution via the OPTION clause. This feature is now generally available for Azure SQL Database. To learn more, review [Query Store hints](/sql/relational-databases/performance/query-store-hints?view=azuresqldb-current&preserve-view=true). |
| **Named Replicas for Hyperscale databases GA** | Named Replicas enable a broad variety of read scale-out scenarios, and easily implement near-real time hybrid transactional and analytical processing (HTAP) solutions. This feature is now generally available. See [named replicas](service-tier-hyperscale-replicas.md#named-replica) to learn more. |
| **Active geo-replication and Auto-failover groups for Hyperscale databases GA** | [Active geo-replication](./active-geo-replication-overview.md) and [Auto-failover groups](./auto-failover-group-sql-db.md) are now generally available for Hyperscale databases,  providing a turn-key business continuity solution, letting you perform quick disaster recovery of databases in case of a regional disaster or a large scale outage. |
| **Ledger GA** | The ledger feature in SQL Database is now generally available. Use the ledger feature to cryptographically attest to other parties, such as auditors or other business parties, that your data hasn't been tampered with. See [Ledger](ledger-landing.yml) to learn more.|
| **JavaScript & Python bindings**| Support for JavaScript and Python SQL bindings for Azure Functions is currently in preview. See [Azure SQL bindings for Azure Functions](/azure/azure-functions/functions-bindings-azure-sql) to learn more. |
| **Local development experience** | The Azure SQL Database local development experience is a combination of tools and procedures that empowers application developers and database professionals to design, edit, build/validate, publish, and run database schemas for databases directly on their workstation using an Azure SQL Database containerized environment. To learn more, see [Local development experience for Azure SQL Database](local-dev-experience-overview.md). |
| **SQL Database emulator** | The Azure SQL Database emulator provides the ability to locally validate database and query design together with client application code in a simple and frictionless model as part of the application development process. The SQL Database emulator is currently in preview. Review [SQL Database emulator](local-dev-experience-sql-database-emulator.md) to learn more.  |
| **SDK-style SQL projects**| Use [Microsoft.Build.Sql](https://www.nuget.org/packages/Microsoft.Build.Sql) for SDK-style SQL projects in the SQL Database Projects extension in Azure Data Studio or Visual Studio Code. This feature is currently in preview. To learn more, see [SDK-style SQL projects](/sql/azure-data-studio/extensions/sql-database-project-extension-sdk-style-projects). |
| **Azure Synapse Link for SQL for Azure SQL Database** | Azure Synapse Link for SQL enables near real-time analytics over operational data in SQL Server 2022 and Azure SQL Database. With a seamless integration between operational stores and Azure Synapse Analytics dedicated SQL pools, Azure Synapse Link for SQL enables you to run analytics, business intelligence and machine learning scenarios on your operational data with minimum impact on source databases with a new change feed technology. For more information, see [What is Azure Synapse Link for SQL? (Preview)](/azure/synapse-analytics/synapse-link/sql-synapse-link-overview).|
| **General Purpose tier Zone redundancy GA** | Enabling zone redundancy for your provisioned and serverless General Purpose databases and elastic pools is now generally available in select regions. To learn more, including region availability see [General Purpose zone redundancy](high-availability-sla.md#zone-redundant-availability). |
| **Change data capture GA** | Change data capture (CDC) lets you track all the changes that occur on a database. Though this feature has been available for SQL Server for quite some time, using it with Azure SQL Database is now generally available. To learn more, see [Change data capture](/sql/relational-databases/track-changes/about-change-data-capture-sql-server).  |
| **GA for maintenance window** | The [maintenance window](maintenance-window.md) feature allows you to configure a maintenance schedule for your Azure SQL Database and receive advance notifications of maintenance windows. [Maintenance window advance notifications](../database/advance-notifications.md) are in public preview for databases configured to use a non-default [maintenance window](maintenance-window.md).|
| **Hyperscale zone redundant configuration preview** | It's now possible to create new Hyperscale databases with zone redundancy to make your databases resilient to a much larger set of failures. This feature is currently in preview for the Hyperscale service tier. To learn more, see [Hyperscale zone redundancy](high-availability-sla.md#zone-redundant-availability). |
| **Hyperscale storage redundancy GA** | Choosing your storage redundancy for your databases in the Hyperscale service tier is now generally available. See [Configure backup storage redundancy](hyperscale-automated-backups-overview.md#data-and-backup-storage-redundancy) to learn more. |
| [Elastic transactions](elastic-transactions-overview.md) | Elastic transactions allow you to execute distributed transactions among cloud databases in Azure SQL Database and Azure SQL Managed Instance. Elastic transactions are now generally available. |
| **Hyperscale reverse migration** | Reverse migration is now in preview. Reverse migration to the General Purpose service tier allows customers who have recently migrated an existing database in Azure SQL Database to the Hyperscale service tier to move back in an emergency, should Hyperscale not meet their needs. While reverse migration is initiated by a service tier change, it's essentially a size-of-data move between different architectures. Learn about [reverse migration from Hyperscale](manage-hyperscale-database.md#reverse-migrate-from-hyperscale). Update: This feature is now generally available as of September 2022.|
| **New Hyperscale articles** | We have reorganized some existing content into new articles and added new content for Hyperscale. Learn about [Hyperscale distributed functions architecture](hyperscale-architecture.md), [how to manage a Hyperscale database](manage-hyperscale-database.md), and how to [create a Hyperscale database](hyperscale-database-create-quickstart.md). |
| **Free Azure SQL Database** | Try Azure SQL Database for free using the Azure free account. To learn more, review [Try SQL Database for free](free-sql-db-free-account-how-to-deploy.md).|

## Contribute to content

To contribute to the Azure SQL documentation, see the [Docs contributor guide](/contribute/).
