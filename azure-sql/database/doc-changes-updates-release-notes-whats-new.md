---
title: What's new?
titleSuffix: Azure SQL Database
description: Learn about the new features and documentation improvements for Azure SQL Database.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: mathoma, randolphwest
ms.date: 11/17/2023
ms.service: sql-database
ms.subservice: service-overview
ms.topic: whats-new
ms.custom: sqldbrb=2, references_regions, ignite-2023
---
# What's new in Azure SQL Database?

[!INCLUDE [appliesto-sqldb](../includes/appliesto-sqldb.md)]

> [!div class="op_single_selector"]
> * [Azure SQL Database](doc-changes-updates-release-notes-whats-new.md?view=azuresql&preserve-view=true)
> * [Azure SQL Managed Instance](../managed-instance/doc-changes-updates-release-notes-whats-new.md?view=azuresql&preserve-view=true)
> * [SQL Server on Azure VMs](../virtual-machines/windows/doc-changes-updates-release-notes-whats-new.md?view=azuresql&preserve-view=true)

This article summarizes the documentation changes associated with new features and improvements in the recent releases of [Azure SQL Database](https://azure.microsoft.com/products/azure-sql/database/). For more information about Azure SQL Database, see [What is Azure SQL Database?](sql-database-paas-overview.md).

[!INCLUDE [entra-id](../includes/entra-id.md)]

> [!TIP]
> Simplified pricing for SQL Database Hyperscale coming soon. Review the [Hyperscale pricing blog](https://aka.ms/hsignite2023) for details.

## Preview

The following table lists the features of Azure SQL Database that are currently in preview.

> [!NOTE]  
> Features currently in preview are available under [supplemental terms of use](https://azure.microsoft.com/support/legal/preview-supplemental-terms/), review for legal terms that apply to Azure features that are in beta, preview, or otherwise not yet released into general availability. Azure SQL Database provides previews to give you a chance to evaluate and [share feedback with the product group](https://feedback.azure.com/d365community/forum/ef2b2b38-2f25-ec11-b6e6-000d3a4f0f84) on features before they become generally available (GA).

| Feature | Details |
| --- | --- |
| [Degrees of Parallelism (DOP) feedback](/sql/relational-databases/performance/intelligent-query-processing-feedback#degree-of-parallelism-dop-feedback) | DOP Feedback is currently available as a limited preview. For more information and how to apply for the preview, see [Announcing Degree of Parallelism Feedback Limited Preview](https://techcommunity.microsoft.com/t5/azure-sql-blog/announcing-degree-of-parallelism-feedback-limited-preview/ba-p/3806924). |
| [Elastic jobs](elastic-jobs-overview.md) | [Updated with a preview refresh and new capabilities in November 2023](https://techcommunity.microsoft.com/t5/azure-sql-blog/azure-sql-database-elastic-jobs-preview-refresh/ba-p/3965759), elastic jobs are the SQL Server Agent replacement for Azure SQL Database. Elastic jobs now support Microsoft Entra ID (formerly Azure Active Directory) authentication, private endpoints, management via REST APIs, Azure Alerts, and new capabilities and user interface in the Azure portal. Job Agents now provide four capacity tiers to scale concurrency for job execution. |
| [Elastic queries](elastic-query-overview.md) | The elastic queries feature allows for cross-database queries in Azure SQL Database. |
| [Elastic transactions](elastic-transactions-overview.md) | Elastic transactions allow you to execute transactions distributed among cloud databases in Azure SQL Database. |
| [Free Azure SQL Database](free-offer.md) | Try Azure SQL Database for free, for the life of your subscription. This free offer provides a General Purpose database with 100,000 vCore seconds of compute every month. |
| [Hyperscale elastic pools](hyperscale-elastic-pool-overview.md) | Manage and scale multiple Hyperscale databases in Azure SQL Database by using Hyperscale elastic pools. |
| [Hyperscale elastic pools Premium-series hardware](hyperscale-elastic-pool-overview.md) | Premium-series and premium-series memory optimized hardware is in preview for Hyperscale elastic pools. |
| [Hyperscale RA-GZRS](hyperscale-automated-backups-overview.md#data-and-backup-storage-redundancy) | Store your Hyperscale database backups on read access geo-zone-redundancy (RA-GZRS) storage. |
| [Invoke External REST endpoints](/sql/relational-databases/system-stored-procedures/sp-invoke-external-rest-endpoint-transact-sql) | Invoke HTTPS REST endpoint natively using a new system stored procedure. |
| [License-free standby replica](standby-replica-how-to-configure.md) | Save on licensing costs by configuring your secondary database replica for disaster recovery standby. | 
| [Maintenance window advance notifications](advance-notifications.md) | Advance notifications are available for databases configured to use a non-default [maintenance window](maintenance-window.md). Advance notifications for maintenance windows are in public preview for Azure SQL Database. |
| [Query editor in the Azure portal](query-editor.md) | The query editor in the portal allows you to run queries against your Azure SQL Database directly from the [Azure portal](https://portal.azure.com). |
| [Serverless Hyperscale](serverless-tier-overview.md) | Automatically scale your Hyperscale databases up and down based on usage when using the serverless compute tier. |
| [SQL Analytics](/azure/azure-monitor/insights/azure-sql) | Azure SQL Analytics is an advanced cloud monitoring solution for monitoring performance of all of your Azure SQL databases at scale and across multiple subscriptions in a single view. Azure SQL Analytics collects and visualizes key performance metrics with built-in intelligence for performance troubleshooting. |
| [SQL Database emulator](local-dev-experience-sql-database-emulator.md) | The Azure SQL Database emulator provides the ability to locally validate database and query design together with client application code in a simple and frictionless model as part of the application development process. |

## General availability (GA)

The following table lists the new generally available (GA) features of Azure SQL Database, and those features that have transitioned from preview to GA within the last 12 months:

| Feature | GA Month | Details |
| --- | --- | --- |
| [Always Encrypted with VBS enclaves](always-encrypted-enclaves-getting-started-vbs.md) | November 2023 | Take advantage of rich confidential queries and in-place encryption operations for Azure SQL Database with Always Encrypted with virtualization-based security (VBS) enclaves. |
| [Azure SQL triggers for Azure Functions](/azure/azure-functions/functions-bindings-azure-sql-trigger) | November 2023 | Azure Functions supports function triggers for Azure SQL Database. |
| [DC-series hardware up to 40 vCores](resource-limits-vcore-single-databases.md) | November 2023 | DC-series hardware from 10 to 40 vCores for General Purpose, Business Critical, and Hyperscale provisioned compute. |
| [Database level CMK with TDE](transparent-data-encryption-byok-database-level-overview.md) | September 2023 | Database level CMK allows setting the TDE protector as a customer-managed key individually for each database within the server. |
| [Hyperscale long-term retention](long-term-retention-overview.md) | September 2023 | Save your Hyperscale database backups for up to 10 years [with the GA of long-term retention (LTR)](https://techcommunity.microsoft.com/t5/azure-sql-blog/announcing-general-availability-of-sql-database-hyperscale-long/ba-p/3930616). |
| [Hyperscale short-term retention](hyperscale-automated-backups-overview.md#data-and-backup-storage-redundancy) | September 2023 | In line with other Azure SQL Database tiers, Hyperscale databases now [retain backups from 1 up to 35 days](automated-backups-overview.md) [with the GA of short-term retention](https://techcommunity.microsoft.com/t5/azure-sql-blog/announcing-general-availability-of-sql-database-hyperscale-short/ba-p/3930640). |
| [External REST Endpoint Invocation is now GA](https://techcommunity.microsoft.com/t5/azure-sql-blog/external-rest-endpoint-invocation-is-now-ga/ba-p/3909108) | August 2023 | [External REST Endpoint Invocation using sp_invoke_external_rest_endpoint](/sql/relational-databases/system-stored-procedures/sp-invoke-external-rest-endpoint-transact-sql?view=azuresqldb-current&preserve-view=true) makes it possible for developers to call REST/GraphQL endpoints from other Azure Services from right in the Azure SQL Database. |
| [XML compression](https://techcommunity.microsoft.com/t5/azure-sql-blog/general-availability-of-xml-compression-for-azure-sql-database/ba-p/3888861) | August 2023 | XML compression for Azure SQL Database is now generally available. You can use [ALTER INDEX](/sql/t-sql/statements/alter-index-transact-sql?view=azuresqldb-current&preserve-view=true#xml_compression) to apply XML compression to existing [XML indexes](/sql/relational-databases/xml/xml-indexes-sql-server?view=azuresqldb-current&preserve-view=true). |
| [TDS 8.0 support](/sql/relational-databases/security/networking/tds-8?view=azuresqldb-current&preserve-view=true) | August 2023 | Azure SQL Database now supports TDS 8.0 for strict encryption of data in transit. |
| [Hyperscale premium-series and premium-series memory optimized hardware](service-tier-hyperscale.md#compute-resources) | July 2023 | Premium-series and premium-series memory optimized hardware is now available for Hyperscale databases. |
| [128 vCore](resource-limits-vcore-single-databases.md#general-purpose---provisioned-compute---gen5) | June 2023 | Provision your Azure SQL Database with up to [128 vCores, now generally available](https://techcommunity.microsoft.com/t5/azure-sql-blog/announcing-preview-of-128-vcore-provisioned-compute-size-on/ba-p/3631211). |
| [Azure SQL bindings for Azure Functions](/azure/azure-functions/functions-bindings-azure-sql-trigger) | May 2023 | Azure Functions supports input and output bindings for the Azure SQL and SQL Server products. |
| [Cross-tenant CMK with TDE](transparent-data-encryption-byok-cross-tenant.md) | May 2023 | Cross-tenant CMK with TDE allows SQL databases to be in a separate tenant than the tenant holding the Azure Key Vault resource used to encrypt the databases. |
| [UMI for auditing](auditing-overview.md) | April 2023 | Configure the storage account for your SQL auditing logs by using User-assigned Managed Identity (UMI). |
| [Shrink Database](/sql/t-sql/database-console-commands/dbcc-shrinkdatabase-transact-sql?view=azuresqldb-current&preserve-view=true) and [Shrink File with Low Priority](/sql/t-sql/database-console-commands/dbcc-shrinkfile-transact-sql?view=azuresqldb-current&preserve-view=true) | March 2023 | This feature solves the concurrency issues that can arise from shrink database and shrink file commands, especially during active maintenance or on busy OLTP environments. In WAIT_AT_LOW_PRIORITY mode, necessary tasks to shrink database files can be completed without negatively affecting application query performance. |
| [Approximate percentile](/sql/t-sql/functions/approx-percentile-cont-transact-sql) | March 2023 | Quickly compute percentiles for a large dataset with acceptable rank-based error bounds to help make rapid decisions by using approximate percentile aggregate functions. |
| [Optimized locking](/sql/relational-databases/performance/optimized-locking) | February 2023 | Optimized locking is a new Database Engine capability that offers an improved locking mechanism that reduces lock memory consumption and blocking among concurrent transactions. This fundamentally improves concurrency and lowers lock memory. |
| [Azure Synapse Link for Azure SQL Database](/azure/synapse-analytics/synapse-link/sql-synapse-link-overview) | November 2022 | Azure Synapse Link for SQL enables near real time analytics over operational data in Azure SQL Database or SQL Server 2022. |
| [Restore progress](/sql/relational-databases/system-dynamic-management-views/sys-dm-operation-status-azure-sql-database) | November 2022 | Restore progress tracked in the `percent_complete` column `sys.dm_operation_status`. |
| [Time series](/sql/t-sql/functions/generate-series-transact-sql) | November 2022 | Generates a series of numbers within a given interval. For more information, review [GENERATE_SERIES](/sql/t-sql/functions/generate-series-transact-sql) and [DATE_BUCKET](/sql/t-sql/functions/date-bucket-transact-sql). |


## Documentation changes

Learn about significant changes to the Azure SQL Database documentation. For previous years, see the [What's new archive](doc-changes-updates-release-notes-whats-new-archive.md).

### November 2023

| Changes | Details |
| --- | --- |
|**Use Azure Monitor metrics to monitor databases and elastic pools**|A new reference for monitoring Azure SQL Database with Azure Monitor is available, including [a set of recommended alert rules to monitor Azure SQL Database with Azure Monitor metrics and alerts](monitoring-metrics-alerts.md).|
|**Always Encrypted with VBS enclaves GA** | Take advantage of rich confidential queries and in-place encryption operations for Azure SQL Database with Always Encrypted with virtualization-based security (VBS) enclaves. This feature is now generally available. Review [Always Encrypted with VBS enclaves](always-encrypted-enclaves-getting-started-vbs.md) to get started. |
|**Azure SQL triggers for Azure Functions GA** | Azure Functions supports function triggers for Azure SQL Managed Instance. This feature is now generally available. Review [Azure SQL triggers for Azure Functions](/azure/azure-functions/functions-bindings-azure-sql) to learn more. | 
|**DC-series hardware up to 40 vCores GA** | DC-series hardware from 10 to 40 vCores is now generally available for General Purpose, Business Critical, and Hyperscale provisioned compute. Review [Resource limits](resource-limits-vcore-single-databases.md) for more information. |
| **Elastic jobs preview** | [Updated with a preview refresh and new capabilities](https://techcommunity.microsoft.com/t5/azure-sql-blog/azure-sql-database-elastic-jobs-preview-refresh/ba-p/3965759), [elastic jobs](elastic-jobs-overview.md) are the SQL Server Agent replacement for Azure SQL Database. Elastic jobs now support Microsoft Entra ID (formerly Azure Active Directory) authentication, private endpoints, management via REST APIs, Azure Alerts, and new capabilities and user interface in the Azure portal. Job Agents now provide four capacity tiers to scale concurrency for job execution. |
|**License-free standby replica preview** | Save on licensing costs by configuring your secondary database replica for disaster recovery standby. This feature is currently in preview. Review [License-free standby replica](standby-replica-how-to-configure.md) to learn more.  | 
|**Premium-series hardware for Hyperscale elastic pools preview** | Premium-series and premium-series memory optimized hardware is now in preview for [Hyperscale elastic pools](hyperscale-elastic-pool-overview.md). |

### October 2023

| Changes | Details |
| --- | --- |
| **Intelligent applications**| Learn how to use AI integration with Azure SQL Database, such as OpenAI, to build intelligent applications. To learn more, review [intelligent applications](ai-artificial-intelligence-intelligent-applications.md). | 

### September 2023

| Changes | Details |
| --- | --- |
| **BASE64_ENCODE and BASE64_DECODE support** | The [BASE64_ENCODE](/sql/t-sql/functions/base64-encode-transact-sql) and [BASE64_DECODE](/sql/t-sql/functions/base64-decode-transact-sql) are now available in Azure SQL Database. |
| **Database level CMK with TDE GA** | [Database level CMK](transparent-data-encryption-byok-database-level-overview.md) allows setting the TDE protector as a customer-managed key individually for each database within the server. This feature is now generally available. |
| **Hyperscale short-term and long-term retention GA** | [Long-term retention](long-term-retention-overview.md) and [short-term retention](hyperscale-automated-backups-overview.md#data-and-backup-storage-redundancy) are now both generally available for Hyperscale databases. For more information, read about [the GA of long-term retention (LTR) for Hyperscale](https://techcommunity.microsoft.com/t5/azure-sql-blog/announcing-general-availability-of-sql-database-hyperscale-long/ba-p/3930616) and [the GA of short-term retention for Hyperscale](https://techcommunity.microsoft.com/t5/azure-sql-blog/announcing-general-availability-of-sql-database-hyperscale-short/ba-p/3930640). |
| **Microsoft Entra ID rebrand**| Azure Active Directory has been rebranded to [Microsoft Entra ID](/azure/active-directory/fundamentals/new-name). | 
| **Optimized locking additional diagnostic information** | Additional wait types, wait and lock resources, and deadlock graph elements are available for [Optimized locking](/sql/relational-databases/performance/optimized-locking). |
| **Try Azure SQL Database for free preview** | [Try Azure SQL Database for free](free-offer.md), for the life of your subscription. This free offer provides a General Purpose database with 100,000 vCore seconds of compute every month. This offer is currently in preview. |



### August 2023

| Changes | Details |
| --- | --- |
| **External REST Endpoint Invocation GA** | [External REST Endpoint Invocation](https://techcommunity.microsoft.com/t5/azure-sql-blog/external-rest-endpoint-invocation-is-now-ga/ba-p/3909108) using [sp_invoke_external_rest_endpoint](/sql/relational-databases/system-stored-procedures/sp-invoke-external-rest-endpoint-transact-sql?view=azuresqldb-current&preserve-view=true) makes it possible for developers to call REST/GraphQL endpoints from other Azure Services from right in the Azure SQL Database. |
| **XML compression GA** | [XML compression](https://techcommunity.microsoft.com/t5/azure-sql-blog/general-availability-of-xml-compression-for-azure-sql-database/ba-p/3888861) for Azure SQL Database is now generally available. You can use [ALTER INDEX](/sql/t-sql/statements/alter-index-transact-sql?view=azuresqldb-current&preserve-view=true#xml_compression) to apply XML compression to existing [XML indexes](/sql/relational-databases/xml/xml-indexes-sql-server?view=azuresqldb-current&preserve-view=true). |
| **TDS 8.0 GA** | Azure SQL Database now supports [TDS 8.0](/sql/relational-databases/security/networking/tds-8?view=azuresqldb-current&preserve-view=true) for strict encryption of data in transit. |

### July 2023

| Changes | Details |
| --- | --- |
| **Hyperscale premium-series and premium-series memory optimized hardware GA** | Premium-series and premium-series memory optimized hardware is [now available for Hyperscale databases](service-tier-hyperscale.md#compute-resources). |
| **DC-series hardware up to 10 vCores preview** | [DC-series hardware up to 40 vCores](resource-limits-vcore-single-databases.md) is now in preview for in General Purpose, Business Critical, and Hyperscale provisioned compute. This supports [Always Encrypted on Azure SQL Database with secure enclaves with up to 40 vCores](https://techcommunity.microsoft.com/t5/azure-sql-blog/always-encrypted-with-secure-enclaves-dc-series-databases-with/ba-p/3872338). |

### June 2023

| Changes | Details |
| --- | --- |
| **128 vCore GA** | Provision your Azure SQL Database with up to [128 vCores](resource-limits-vcore-single-databases.md), [now generally available](https://techcommunity.microsoft.com/t5/azure-sql-blog/announcing-preview-of-128-vcore-provisioned-compute-size-on/ba-p/3631211). |
| **64 vCore option for Hyperscale premium** | A 64 vCore option is now available for [Azure SQL Database Hyperscale premium-series](resource-limits-vcore-single-databases.md#hyperscale---provisioned-compute---premium-series) and [Hyperscale premium-series memory optimized](resource-limits-vcore-single-databases.md#hyperscale---provisioned-compute---premium-series-memory-optimized). |

### May 2023

| Changes | Details |
| --- | --- |
| **Azure SQL bindings for Azure Functions GA** | Azure Functions supports input bindings, and output bindings for the Azure SQL and SQL Server products. This feature is now generally available. For more information, review [Azure SQL bindings for Azure Functions](/azure/azure-functions/functions-bindings-azure-sql). |
| **Azure SQL triggers for Azure Functions preview** | Azure Functions supports function triggers for the Azure SQL and SQL Server products. This feature is currently in preview. For more information, review [Azure SQL triggers for Azure Functions](/azure/azure-functions/functions-bindings-azure-sql-trigger). |
| **Cross-tenant CMK with TDE GA** | Cross-tenant CMK with TDE allows placing SQL databases into a separate tenant than the tenant that holds the Azure Key Vault resource used to encrypt the databases. This feature is now generally available. For more information, review [Cross-tenant CMK with TDE](transparent-data-encryption-byok-cross-tenant.md). |
| **Hyperscale elastic pools preview** | Manage and scale multiple Hyperscale databases in Azure SQL Database by using Hyperscale elastic pools. This feature is now in preview. For more information, review [Hyperscale elastic pools](hyperscale-elastic-pool-overview.md). |
| **Optimized locking available in Hyperscale GA** | Optimized locking is a new Database Engine capability that offers an improved locking mechanism that reduces lock memory consumption and blocking among concurrent transactions. This fundamentally improves concurrency and lowers lock memory. Optimized locking is now available in all DTU and vCore service tiers, including provisioned and serverless. This feature is generally available. For more information, review [Optimized locking](/sql/relational-databases/performance/optimized-locking). |

### April 2023

| Changes | Details |
| --- | --- |
| **DOP Feedback preview** | [Degrees of Parallelism (DOP) Feedback](/sql/relational-databases/performance/intelligent-query-processing-feedback#dop-feedback) is currently available as a limited preview. For more information and how to apply for the preview, see [Announcing Degree of Parallelism Feedback Limited Preview](https://techcommunity.microsoft.com/t5/azure-sql-blog/announcing-degree-of-parallelism-feedback-limited-preview/ba-p/3806924). |

### March 2023

| Changes | Details |
| --- | --- |
| **Approximate Percentile GA** | Quickly compute percentiles for a large dataset with acceptable rank-based error bounds to help make rapid decisions by using approximate percentile aggregate. This feature is generally available now. For more information, review [Approximate percentile](/sql/t-sql/functions/approx-percentile-cont-transact-sql). |
| **Shrink Database / Shrink File with Low Priority GA** | This feature solves the concurrency issues that can arise from shrink database and shrink file commands, especially during active maintenance or on busy OLTP environments. In WAIT_AT_LOW_PRIORITY mode, necessary tasks to shrink database files can be completed without negatively affecting application query performance. Review [Shrink Database](/sql/t-sql/database-console-commands/dbcc-shrinkdatabase-transact-sql?view=azuresqldb-current&preserve-view=true) and [Shrink File with Low Priority](/sql/t-sql/database-console-commands/dbcc-shrinkfile-transact-sql?view=azuresqldb-current&preserve-view=true). |
| **Database level CMK with TDE preview** | Previously, TDE with CMK was set at the server level, and was inherited by all encrypted databases associated with that server. Database level CMK allows setting the TDE protector as a customer-managed key individually for each database within the server. This feature is currently in preview. For more information, review [Transparent data encryption (TDE) with customer-managed keys at the database level](transparent-data-encryption-byok-database-level-overview.md). |

### February 2023

| Changes | Details |
| --- | --- |
| **Azure SQL Database high availability and disaster recovery checklist** | [This guide provides a detailed review of proactive steps you can take](high-availability-disaster-recovery-checklist.md) to maximize availability, ensure recovery, and prepare for Azure outages. |
| **Always Encrypted with VBS enclaves preview** | Take advantage of rich confidential queries and in-place cryptographic operations for Azure SQL Database with Always Encrypted with virtualization-based security (VBS) enclaves. This feature is currently in preview. For more information, review [Always Encrypted with VBS enclaves](always-encrypted-enclaves-getting-started-vbs.md). |
| **Optimized locking GA** | Optimized locking is a new Database Engine capability that offers an improved locking mechanism that reduces lock memory consumption and blocking among concurrent transactions. This fundamentally improves concurrency and lowers lock memory. This feature is generally available. For more information, review [Optimized locking](/sql/relational-databases/performance/optimized-locking). |
| **Cross-tenant CMK with TDE preview** | Cross-tenant CMK with TDE allows SQL databases to be in a separate tenant than the tenant holding the Azure Key Vault resource used to encrypt the databases. This feature is currently in preview. For more information, review [Cross-tenant CMK with TDE](transparent-data-encryption-byok-cross-tenant.md). |
| **Serverless Hyperscale preview** | It's now possible to deploy your Hyperscale databases to the serverless compute tier. This feature is currently in preview. For more information, review [serverless compute](serverless-tier-overview.md). |

### 2022

| Changes | Details |
| --- | --- |
| **128 vCore preview** | It's now possible to provision your Azure SQL Database with up to 128 vCores in both the General Purpose, and Business Critical service tiers. For more information, review [resource limits](resource-limits-vcore-single-databases.md#general-purpose---provisioned-compute---gen5). |
| **Azure Synapse Link for SQL GA** | Azure Synapse Link for SQL, now generally available, enables near real-time analytics over operational data in SQL Server 2022 and Azure SQL Database. With a seamless integration between operational stores and Azure Synapse Analytics dedicated SQL pools, Azure Synapse Link for SQL enables you to run analytics, business intelligence and machine learning scenarios on your operational data with minimum impact on source databases with a new change feed technology. For more information, review [What is Azure Synapse Link for SQL?](/azure/synapse-analytics/synapse-link/sql-synapse-link-overview). |
| **Gen5 hardware rename** | The Gen5 hardware in the vCore purchasing model has been renamed to **standard-series (Gen5)**. |
| **Hyperscale premium-series and premium-series memory optimized hardware preview** | Premium-series and premium-series memory optimized hardware is [in preview for Hyperscale databases](service-tier-hyperscale.md#compute-resources). For more information, read the [Premium-series announcement blog post](https://aka.ms/AAiq28n). |
| **Invoke external REST endpoints preview** | It's now possible to call an HTTPS REST endpoint natively, using a new system stored procedure. This feature is currently in preview. For more information, review [sp_invoke_external_rest_endpoint](/sql/relational-databases/system-stored-procedures/sp-invoke-external-rest-endpoint-transact-sql). |
| **Restore progress GA** | Tracking the progress of your restore by using the `percent_complete` column [sys.dm_operation_status](/sql/relational-databases/system-dynamic-management-views/sys-dm-operation-status-azure-sql-database) is now generally available. For more information, read the [Restore progress announcement blog post](https://aka.ms/RestoreProgressGA). |
| **Time series GA** | Generates a series of numbers within a given interval. This feature is generally available. For more information, see [GENERATE_SERIES](/sql/t-sql/functions/generate-series-transact-sql) and [DATE_BUCKET](/sql/t-sql/functions/date-bucket-transact-sql). |
| **Approximate percentiles preview** | Support has been added to quickly compute percentiles using approximate percentile aggregate functions for large datasets with acceptable rank-based error bounds. This feature is currently in preview. For more information, see [Approx_Percentile_Cont](/sql/t-sql/functions/approx-percentile-cont-transact-sql) and [Approx_Percentile_Disc](/sql/t-sql/functions/approx-percentile-disc-transact-sql). |
| **Automated TDE key rotation for CMK GA** | Automatically switch to a new key when using a customer-managed key (CMK) with TDE. This feature is now generally available. For more information, see [Automated key rotation](../database/transparent-data-encryption-byok-overview.md#rotation-of-tde-protector). |
| **Database copy of Hyperscale geo secondary replicas GA** | It's now possible to use a Hyperscale geo-secondary replica as source for a database copy. This feature is generally available. For more information, see [Hyperscale service tier](service-tier-hyperscale.md). |
| **Cross-subscription failover group with Azure PowerShell** | It's now possible to deploy your auto-failover group for a single database across subscriptions by using Azure PowerShell. For more information, see [Configure auto-failover group](auto-failover-group-configure-sql-db.md?view=azuresql&tabs=azure-powershell&pivots=azure-sql-single-db&preserve-view=true#create-failover-group). |
| **Hyperscale databases LTR preview** | It's now possible to store your Hyperscale database backups for up to 10 years using the long-term retention (LTR) capability. This feature is now in preview. For more information, see [long-term retention](long-term-retention-overview.md). |
| **Hyperscale RA-GZRS preview** | It's now possible to choose read access geo-zone-redundancy (RA-GZRS) as a backup storage redundancy for Hyperscale databases. This feature is currently in preview. For more information, see [Hyperscale backup storage redundancy](hyperscale-automated-backups-overview.md#data-and-backup-storage-redundancy). |
| **Hyperscale reverse migrate GA** | This feature allows customers who have recently migrated an existing database in Azure SQL Database to the Hyperscale service tier to move back in an emergency, should Hyperscale not meet their needs. While reverse migration is initiated by a service tier change, it's essentially a size-of-data move between different architectures. This feature is now generally available. For more information, see [Reverse migration to the General Purpose service tier](manage-hyperscale-database.md#reverse-migrate-from-hyperscale). |
| **UMI support for auditing preview** | It's now possible to configure the storage account used for SQL auditing logs by using User Managed Identity (UMI). This feature is currently in preview. For more information, see [auditing](auditing-overview.md). |
| **Zone redundant configuration for Hyperscale databases GA** | The zone redundant configuration feature utilizes [Azure Availability Zones](/azure/availability-zones/az-overview#availability-zones) to replicate databases across multiple physical locations within an Azure region. By selecting [zone redundancy](high-availability-sla.md#zone-redundant-availability), you can make your Hyperscale databases resilient to a much larger set of failures, including catastrophic datacenter outages, without any changes to the application logic. This configuration option is now generally available. For more information, see [Zone redundant configuration for Hyperscale databases](high-availability-sla.md#zone-redundant-availability). |
| **Query Store hints GA** | You can use query hints to optimize your query execution via the OPTION clause. This feature is now generally available for Azure SQL Database. For more information, see [Query Store hints](/sql/relational-databases/performance/query-store-hints?view=azuresqldb-current&preserve-view=true). |
| **Named Replicas for Hyperscale databases GA** | Named Replicas enable a broad variety of read scale-out scenarios, and easily implement near-real time hybrid transactional and analytical processing (HTAP) solutions. This feature is now generally available. For more information, see [named replicas](service-tier-hyperscale-replicas.md#named-replica). |
| **Active geo-replication and Auto-failover groups for Hyperscale databases GA** | [Active geo-replication](./active-geo-replication-overview.md) and [Auto-failover groups](./auto-failover-group-sql-db.md) are now generally available for Hyperscale databases, providing a turn-key business continuity solution, letting you perform quick disaster recovery of databases in case of a regional disaster or a large scale outage. |
| **Ledger GA** | The ledger feature in SQL Database is now generally available. Use the ledger feature to cryptographically attest to other parties, such as auditors or other business parties, that your data hasn't been tampered with. For more information, see [Ledger](ledger-landing.yml). |
| **JavaScript & Python bindings** | Support for JavaScript and Python SQL bindings for Azure Functions is currently in preview. For more information, see [Azure SQL bindings for Azure Functions](/azure/azure-functions/functions-bindings-azure-sql). |
| **Local development experience** | The Azure SQL Database local development experience is a combination of tools and procedures that empowers application developers and database professionals to design, edit, build/validate, publish, and run database schemas for databases directly on their workstation using an Azure SQL Database containerized environment. For more information, see [Local development experience for Azure SQL Database](local-dev-experience-overview.md). |
| **SQL Database emulator** | The Azure SQL Database emulator provides the ability to locally validate database and query design together with client application code in a simple and frictionless model as part of the application development process. The SQL Database emulator is currently in preview. For more information, see [SQL Database emulator](local-dev-experience-sql-database-emulator.md). |
| **SDK-style SQL projects** | Use [Microsoft.Build.Sql](https://www.nuget.org/packages/Microsoft.Build.Sql) for SDK-style SQL projects in the SQL Database Projects extension in Azure Data Studio or Visual Studio Code. This feature is currently in preview. For more information, see [SDK-style SQL projects](/azure-data-studio/extensions/sql-database-project-extension-sdk-style-projects). |
| **Azure Synapse Link for SQL for Azure SQL Database** | Azure Synapse Link for SQL enables near real-time analytics over operational data in SQL Server 2022 and Azure SQL Database. With a seamless integration between operational stores and Azure Synapse Analytics dedicated SQL pools, Azure Synapse Link for SQL enables you to run analytics, business intelligence, and machine learning scenarios on your operational data with minimum impact on source databases with a new change feed technology. For more information, see [What is Azure Synapse Link for SQL? (Preview)](/azure/synapse-analytics/synapse-link/sql-synapse-link-overview). |
| **General Purpose tier Zone redundancy GA** | Enabling zone redundancy for your provisioned and serverless General Purpose databases and elastic pools is now generally available in select regions. For more information, including region availability, see [General Purpose zone redundancy](high-availability-sla.md#zone-redundant-availability). |
| **Change data capture GA** | Change data capture (CDC) lets you track all the changes that occur on a database. Though this feature has been available for SQL Server for quite some time, using it with Azure SQL Database is now generally available. For more information, see [Change data capture](/sql/relational-databases/track-changes/about-change-data-capture-sql-server). |
| **GA for maintenance window** | The [maintenance window](maintenance-window.md) feature allows you to configure a maintenance schedule for your Azure SQL Database and receive advance notifications of maintenance windows. [Maintenance window advance notifications](../database/advance-notifications.md) are in public preview for databases configured to use a non-default [maintenance window](maintenance-window.md). |
| **Hyperscale zone redundant configuration preview** | It's now possible to create new Hyperscale databases with zone redundancy to make your databases resilient to a much larger set of failures. This feature is currently in preview for the Hyperscale service tier. For more information, see [Hyperscale zone redundancy](high-availability-sla.md#zone-redundant-availability). |
| **Hyperscale storage redundancy GA** | Choosing your storage redundancy for your databases in the Hyperscale service tier is now generally available. For more information, see [Configure backup storage redundancy](hyperscale-automated-backups-overview.md#data-and-backup-storage-redundancy). |
| **Elastic transactions** | [Elastic transactions](elastic-transactions-overview.md) allow you to execute distributed transactions among cloud databases in Azure SQL Database and Azure SQL Managed Instance. Elastic transactions are now generally available. |
| **New Hyperscale articles** | We have reorganized some existing content into new articles and added new content for Hyperscale. Learn about [Hyperscale distributed functions architecture](hyperscale-architecture.md), [how to manage a Hyperscale database](manage-hyperscale-database.md), and how to [create a Hyperscale database](hyperscale-database-create-quickstart.md). |

## Contribute to content

To contribute to the Azure SQL documentation, see the [Docs contributor guide](/contribute/).
