---
title: What's new?
titleSuffix: Azure SQL Database
description: Learn about the new features and documentation improvements for Azure SQL Database.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: mathoma, randolphwest
ms.date: 1/3/2024
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

## Preview

The following table lists the features of Azure SQL Database that are currently in preview.

> [!NOTE]  
> Features currently in preview are available under [supplemental terms of use](https://azure.microsoft.com/support/legal/preview-supplemental-terms/), review for legal terms that apply to Azure features that are in beta, preview, or otherwise not yet released into general availability. Azure SQL Database provides previews to give you a chance to evaluate and [share feedback with the product group](https://feedback.azure.com/d365community/forum/ef2b2b38-2f25-ec11-b6e6-000d3a4f0f84) on features before they become generally available (GA).

| Feature | Details |
| --- | --- |
| [Azure SQL triggers for Azure Functions](/azure/azure-functions/functions-bindings-azure-sql-trigger) | Azure Functions supports function triggers for Azure SQL Database. |
| [Degrees of Parallelism (DOP) feedback](/sql/relational-databases/performance/intelligent-query-processing-degree-parallelism-feedback) | DOP Feedback is currently available as a limited preview. For more information and how to apply for the preview, see [Announcing Degree of Parallelism Feedback Limited Preview](https://techcommunity.microsoft.com/t5/azure-sql-blog/announcing-degree-of-parallelism-feedback-limited-preview/ba-p/3806924). |
| [Elastic jobs](elastic-jobs-overview.md) | [Updated with a preview refresh and new capabilities in November 2023](https://techcommunity.microsoft.com/t5/azure-sql-blog/azure-sql-database-elastic-jobs-preview-refresh/ba-p/3965759), elastic jobs are the SQL Server Agent replacement for Azure SQL Database. Elastic jobs now support Microsoft Entra ID (formerly Azure Active Directory) authentication, private endpoints, management via REST APIs, Azure Alerts, and new capabilities and user interface in the Azure portal. Job Agents now provide four capacity tiers to scale concurrency for job execution. |
| [Elastic queries](elastic-query-overview.md) | The elastic queries feature allows for cross-database queries in Azure SQL Database. |
| [Elastic transactions](elastic-transactions-overview.md) | Elastic transactions allow you to execute transactions distributed among cloud databases in Azure SQL Database. |
| [Free Azure SQL Database](free-offer.md) | Try Azure SQL Database for free, for the life of your subscription. This free offer provides a General Purpose database with 100,000 vCore seconds of compute every month. |
| [Hyperscale elastic pools](hyperscale-elastic-pool-overview.md) | Manage and scale multiple Hyperscale databases in Azure SQL Database by using Hyperscale elastic pools. |
| [Hyperscale elastic pools Premium-series hardware](hyperscale-elastic-pool-overview.md) | Premium-series and premium-series memory optimized hardware is in preview for Hyperscale elastic pools. |
| [Hyperscale RA-GZRS](hyperscale-automated-backups-overview.md#data-and-backup-storage-redundancy) | Store your Hyperscale database backups on read access geo-zone-redundancy (RA-GZRS) storage. |
| [Invoke External REST endpoints](/sql/relational-databases/system-stored-procedures/sp-invoke-external-rest-endpoint-transact-sql) | Invoke HTTPS REST endpoint natively using a new system stored procedure. |
| [License-free standby replica](standby-replica-how-to-configure.md) | Save on licensing costs by configuring your secondary database replica for disaster recovery standby. | 
| [Maintenance window advance notifications](advance-notifications.md) | Advance notifications are available for databases configured to use a nondefault [maintenance window](maintenance-window.md). Advance notifications for maintenance windows are in public preview for Azure SQL Database. |
| [Query editor in the Azure portal](query-editor.md) | The query editor in the portal allows you to run queries against your Azure SQL Database directly from the [Azure portal](https://portal.azure.com). |
| [Serverless Hyperscale](serverless-tier-overview.md) | Automatically scale your Hyperscale databases up and down based on usage when using the serverless compute tier. |
| [SQL Analytics](/azure/azure-monitor/insights/azure-sql) | Azure SQL Analytics is an advanced cloud monitoring solution for monitoring performance of all of your Azure SQL databases at scale and across multiple subscriptions in a single view. Azure SQL Analytics collects and visualizes key performance metrics with built-in intelligence for performance troubleshooting. |
| [SQL Database emulator](local-dev-experience-sql-database-emulator.md) | The Azure SQL Database emulator provides the ability to locally validate database and query design together with client application code in a simple and frictionless model as part of the application development process. |

## General availability (GA)

The following table lists features of Azure SQL Database that have been made generally available (GA) within the last 12 months:

| Feature | GA Month | Details |
| --- | --- | --- |
| [Lower, simplified pricing for Azure SQL Database Hyperscale](https://aka.ms/hsignite2023) | December 2023 | Simplified pricing for Azure SQL Database Hyperscale has arrived! For pricing change details, see [Azure SQL Database Hyperscale – lower, simplified pricing!](https://techcommunity.microsoft.com/t5/azure-sql-blog/azure-sql-database-hyperscale-lower-simplified-pricing/ba-p/3982209).|
| [Always Encrypted with VBS enclaves](always-encrypted-enclaves-getting-started-vbs.md) | November 2023 | Take advantage of rich confidential queries and in-place encryption operations for Azure SQL Database with Always Encrypted with virtualization-based security (VBS) enclaves. |
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


## Documentation changes

Learn about significant changes to the Azure SQL Database documentation. For previous years, see the [What's new archive](doc-changes-updates-release-notes-whats-new-archive.md).

### January 2024

| Changes | Details |
| --- | --- |
| **New tutorial: Develop a Kubernetes Application for Azure SQL Database**| A new tutorial is available to demonstrate [how to develop a modern application using Python, Docker Containers, Kubernetes, and Azure SQL Database](develop-kubernetes-application.md). |

### December 2023

| Changes | Details |
| --- | --- |
| **Lower, simplified Hyperscale pricing** | Simplified [pricing for Azure SQL Database Hyperscale](service-tier-hyperscale.md?view=azuresql-db&preserve-view=true#hyperscale-pricing-model) has arrived! Review the [new pricing tier for Azure SQL Database Hyperscale announcement](https://aka.ms/hsignite2023), and for pricing change details, see [Azure SQL Database Hyperscale – lower, simplified pricing!](https://techcommunity.microsoft.com/t5/azure-sql-blog/azure-sql-database-hyperscale-lower-simplified-pricing/ba-p/3982209).|

### November 2023

| Changes | Details |
| --- | --- |
|**Use Azure Monitor metrics to monitor databases and elastic pools**|A new reference for monitoring Azure SQL Database with Azure Monitor is available, including [a set of recommended alert rules to monitor Azure SQL Database with Azure Monitor metrics and alerts](monitoring-metrics-alerts.md).|
|**Always Encrypted with VBS enclaves GA** | Take advantage of rich confidential queries and in-place encryption operations for Azure SQL Database with Always Encrypted with virtualization-based security (VBS) enclaves. This feature is now generally available. Review [Always Encrypted with VBS enclaves](always-encrypted-enclaves-getting-started-vbs.md) to get started. |
|**DC-series hardware up to 40 vCores GA** | DC-series hardware from 10 to 40 vCores is now generally available for General Purpose, Business Critical, and Hyperscale provisioned compute. Review [Resource limits](resource-limits-vcore-single-databases.md) for more information. |
| **Elastic jobs preview** | [Updated with a preview refresh and new capabilities](https://techcommunity.microsoft.com/t5/azure-sql-blog/azure-sql-database-elastic-jobs-preview-refresh/ba-p/3965759), [elastic jobs](elastic-jobs-overview.md) are the SQL Server Agent replacement for Azure SQL Database. Elastic jobs now support Microsoft Entra ID (formerly Azure Active Directory) authentication, private endpoints, management via REST APIs, Azure Alerts, and new capabilities and user interface in the Azure portal. Job Agents now provide four capacity tiers to scale concurrency for job execution. |
|**License-free standby replica preview** | Save on licensing costs by configuring your secondary database replica for disaster recovery standby. This feature is currently in preview. Review [License-free standby replica](standby-replica-how-to-configure.md) to learn more.  | 
|**Premium-series hardware for Hyperscale elastic pools preview** | Premium-series and premium-series memory optimized hardware is now in preview for [Hyperscale elastic pools](hyperscale-elastic-pool-overview.md). |



## Archive

For previous updates, see the [What's new archive](doc-changes-updates-release-notes-whats-new-archive.md).

## Contribute to content

To contribute to the Azure SQL documentation, see the [Docs contributor guide](/contribute/).
