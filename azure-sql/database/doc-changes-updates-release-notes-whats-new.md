---
title: What's new?
titleSuffix: Azure SQL Database
description: Learn about the new features and documentation improvements for Azure SQL Database.
author: MashaMSFT
ms.author: mathoma
ms.date: 11/30/2022
ms.service: sql-database
ms.subservice: service-overview
ms.topic: conceptual
ms.custom:
  - "sqldbrb=2"
  - "references_regions"
  - "ignite-fall-2021"
---
# What's new in Azure SQL Database?

[!INCLUDE[appliesto-sqldb](../includes/appliesto-sqldb.md)]

> [!div class="op_single_selector"]
> * [Azure SQL Database](doc-changes-updates-release-notes-whats-new.md)
> * [Azure SQL Managed Instance](../managed-instance/doc-changes-updates-release-notes-whats-new.md)

This article summarizes the documentation changes associated with new features and improvements in the recent releases of [Azure SQL Database](https://azure.microsoft.com/products/azure-sql/database/). To learn more about Azure SQL Database, see [What is Azure SQL Database?](sql-database-paas-overview.md).

## Preview

The following table lists the features of Azure SQL Database that are currently in preview:

| Feature | Details |
| ---| --- |
| [128 vCore](resource-limits-vcore-single-databases.md#general-purpose---provisioned-compute---gen5) | Provision your Azure SQL Database with up to 128 virtual cores (vCores). | 
| [Approximate percentile](/sql/t-sql/functions/approx-percentile-cont-transact-sql) | Quickly compute percentiles for a large dataset with acceptable rank-based error bounds to help make rapid decisions by using approximate percentile aggregate functions. | 
| [Elastic jobs](elastic-jobs-overview.md) | The elastic jobs feature is the SQL Server Agent replacement for Azure SQL Database as a PaaS offering.  |
| [Elastic queries](elastic-query-overview.md) | The elastic queries feature allows for cross-database queries in Azure SQL Database. |
| [Elastic transactions](elastic-transactions-overview.md) | Elastic transactions allow you to execute transactions distributed among cloud databases in Azure SQL Database. |
| [Hyperscale long-term retention](long-term-retention-overview.md) | Save your Hyperscale database backups for up to 10 years with long-term retention (LTR).  | 
| [Hyperscale RA-GZRS](hyperscale-automated-backups-overview.md#data-and-backup-storage-redundancy) | Store your Hyperscale database backups on read access geo-zone-redundancy (RA-GZRS) storage. |
| Hyperscale premium-series and premium-series memory optimized hardware | Premium-series and premium-series memory optimized hardware is [in preview for  for Hyperscale databases](service-tier-hyperscale.md#compute-resources). For more information, read the [Premium-series announcement blog post](https://aka.ms/AAiq28n).|
| [Hyperscale short-term retention](hyperscale-automated-backups-overview.md#data-and-backup-storage-redundancy) | Retain backups from 1 up to 35 days for Hyperscale databases, and perform a point-in-time restore within the configured retention period. |
| [Invoke External REST endpoints](/sql/relational-databases/system-stored-procedures/sp-invoke-external-rest-endpoint-transact-sql) | Invoke HTTPS REST endpoint natively using a new system stored procedure. |
| [JavaScript & Python bindings](/azure/azure-functions/functions-bindings-azure-sql)| Use JavaScript or Python SQL bindings with Azure Functions. | 
| [Maintenance window advance notifications](../database/advance-notifications.md)| Advance notifications are available for databases configured to use a non-default [maintenance window](maintenance-window.md). Advance notifications for maintenance windows are in public preview for Azure SQL Database. |
| [Query editor in the Azure portal](connect-query-portal.md) | The query editor in the portal allows you to run queries against your Azure SQL Database directly from the [Azure portal](https://portal.azure.com).|
| [SQL Analytics](/azure/azure-monitor/insights/azure-sql)|Azure SQL Analytics is an advanced cloud monitoring solution for monitoring performance of all of your Azure SQL databases at scale and across multiple subscriptions in a single view. Azure SQL Analytics collects and visualizes key performance metrics with built-in intelligence for performance troubleshooting.|
| [SQL Database emulator](local-dev-experience-sql-database-emulator.md) | The Azure SQL Database emulator provides the ability to locally validate database and query design together with client application code in a simple and frictionless model as part of the application development process. |
| [SQL Database Projects extension](/sql/azure-data-studio/extensions/sql-database-project-extension) | An extension to develop databases for Azure SQL Database with Azure Data Studio and VS Code. A SQL project is a local representation of SQL objects that comprise the schema for a single database, such as tables, stored procedures, or functions. |
| [SQL Insights](/azure/azure-monitor/insights/sql-insights-overview) | SQL Insights is a comprehensive solution for monitoring any product in the Azure SQL family. SQL Insights (preview) uses dynamic management views to expose the data you need to monitor health, diagnose problems, and tune performance.|
| [UMI for auditing](auditing-overview.md) | Configure the storage account for your SQL auditing logs by using User Managed Identity (UMI). | 


## General availability (GA)

The following table lists the new generally available (GA) features of Azure SQL Database, and those that have transitioned from preview to GA within the last 12 months:

| Feature | GA Month | Details |
| ---| --- |--- |
| [Azure Synapse Link for Azure SQL Database](/azure/synapse-analytics/synapse-link/sql-synapse-link-overview) | November 2022|  Azure Synapse Link for SQL enables near real time analytics over operational data in Azure SQL Database or SQL Server 2022. |
| [Restore progress](/sql/relational-databases/system-dynamic-management-views/sys-dm-operation-status-azure-sql-database) |November 2022 | Restore progress tracked in the `percent_complete` column sys.dm_operation_status. | 
| [Time series](/sql/t-sql/functions/generate-series-transact-sql) |November 2022 | Generates a series of numbers within a given interval.  Review [GENERATE_SERIES](/sql/t-sql/functions/generate-series-transact-sql) and [DATE_BUCKET](/sql/t-sql/functions/date-bucket-transact-sql) to learn more. | 
|[Automated key rotation for TDE with CMK](../database/transparent-data-encryption-byok-overview.md#rotation-of-tde-protector) | October 2022 | Automatically switch to a new key when using a customer-managed key (CMK) for TDE with Azure SQL Database. | 
| [Database copy from Hyperscale replica](service-tier-hyperscale.md) | October 2022 | Use a Hyperscale geo-secondary replica as source for a database copy. | 
| [Reverse migrate from Hyperscale](manage-hyperscale-database.md#reverse-migrate-from-hyperscale) | September 2022 | Reverse migration to the General Purpose service tier allows customers who have recently migrated an existing database in Azure SQL Database to the Hyperscale service tier to move back in an emergency, should Hyperscale not meet their needs. While reverse migration is initiated by a service tier change, it's essentially a size-of-data move between different architectures. |
| [Zone redundant configuration for Hyperscale databases](high-availability-sla.md#hyperscale-service-tier-zone-redundant-availability) | August 2022 |The zone redundant configuration feature utilizes [Azure Availability Zones](/azure/availability-zones/az-overview#availability-zones) to replicate databases across multiple physical locations within an Azure region. By selecting [zone redundancy](high-availability-sla.md#hyperscale-service-tier-zone-redundant-availability), you can make your Hyperscale databases resilient to a much larger set of failures, including catastrophic datacenter outages, without any changes to the application logic.|
| [Query Store hints](/sql/relational-databases/performance/query-store-hints?view=azuresqldb-current&preserve-view=true) | August 2022 | Use query hints to optimize your query execution via the OPTION clause. |
| [Named Replicas](service-tier-hyperscale-replicas.md#named-replica) for Hyperscale databases | June 2022 | Named Replicas enable a broad variety of read scale-out scenarios, and easily implement near-real time hybrid transactional and analytical processing (HTAP) solutions. |
| [Active geo-replication](./active-geo-replication-overview.md) and [Auto-failover groups](./auto-failover-group-sql-db.md) for Hyperscale databases | June 2022 | Active geo-replication and Auto-failover groups provide a turn-key business continuity solution for Hyperscale databases, letting you perform quick disaster recovery of databases in case of a regional disaster or a large scale outage.|
| [Ledger](/sql/relational-databases/security/ledger/ledger-overview) | May 2022 | The ledger feature in Azure SQL Database allows you to cryptographically attest to other parties, such as auditors or other business parties, that your data hasn't been tampered with. |
| [Change data capture](/sql/relational-databases/track-changes/about-change-data-capture-sql-server) | April 2022 | Change data capture (CDC) lets you track all the changes that occur on a database. Though this feature has been available for SQL Server for quite some time, using it with Azure SQL Database is now generally available. |
| [Zone redundant configuration for General Purpose tier](high-availability-sla.md#general-purpose-service-tier-zone-redundant-availability) | April 2022 | The zone redundant configuration feature utilizes [Azure Availability Zones](/azure/availability-zones/az-overview#availability-zones) to replicate databases across multiple physical locations within an Azure region. By selecting [zone redundancy](high-availability-sla.md#general-purpose-service-tier-zone-redundant-availability), you can make your provisioned and serverless General Purpose databases and elastic pools resilient to a much larger set of failures, including catastrophic datacenter outages, without any changes to the application logic.|
| [Maintenance window](../database/maintenance-window.md)| March 2022 | The maintenance window feature allows you to configure maintenance schedule for your Azure SQL Database. [Maintenance window advance notifications](../database/advance-notifications.md), however, are in preview.|
| [Storage redundancy for Hyperscale databases](hyperscale-automated-backups-overview.md#data-and-backup-storage-redundancy) | March 2022 |  When creating a Hyperscale database, you can choose your preferred storage type: read-access geo-redundant storage (RA-GRS), zone-redundant storage (ZRS), or locally redundant storage (LRS) Azure standard storage. The selected storage redundancy option will be used for the lifetime of the database for both data storage redundancy and backup storage redundancy. |
| [Azure Active Directory-only authentication](authentication-azure-ad-only-authentication.md) | November 2021 | It's possible to configure your Azure SQL Database to allow authentication only from Azure Active Directory. | 



## Documentation changes

Learn about significant changes to the Azure SQL Database documentation.

### November 2022

| Changes | Details |
| --- | --- |
| **128 vCore preview** | It's now possible to provision your Azure SQL Database with up to 128 vCores in both the General Purpose, and Business Critical service tiers. See [resource limits](resource-limits-vcore-single-databases.md#general-purpose---provisioned-compute---gen5) to learn more. | 
| **Azure Synapse Link for SQL GA** | Azure Synapse Link for SQL, now generally available, enables near real-time analytics over operational data in SQL Server 2022 and Azure SQL Database. With a seamless integration between operational stores and Azure Synapse Analytics dedicated SQL pools, Azure Synapse Link for SQL enables you to run analytics, business intelligence and machine learning scenarios on your operational data with minimum impact on source databases with a new change feed technology. For more information, see [What is Azure Synapse Link for SQL?](/azure/synapse-analytics/synapse-link/sql-synapse-link-overview).|
| **Gen5 hardware rename** | The Gen5 hardware in the vCore purchasing model has been renamed to **standard-series (Gen5)**. | 
| **Hyperscale premium-series and premium-series memory optimized hardware preview** | Premium-series and premium-series memory optimized hardware is [in preview for  for Hyperscale databases](service-tier-hyperscale.md#compute-resources). For more information, read the [Premium-series announcement blog post](https://aka.ms/AAiq28n).|
| **Invoke external REST endpoints preview** | It's now possible to call an HTTPS REST endpoint natively, using a new system stored procedure. This feature is currently in preview. To learn more, review [sp_invoke_external_rest_endpoint](/sql/relational-databases/system-stored-procedures/sp-invoke-external-rest-endpoint-transact-sql). |
| **Restore progress GA** | Tracking the progress of your restore by using the `percent_complete` column [sys.dm_operation_status](/sql/relational-databases/system-dynamic-management-views/sys-dm-operation-status-azure-sql-database) is now generally available. For more information, read the [Restore progress announcement blog post](https://aka.ms/RestoreProgressGA). |
| **Time series GA** | Generates a series of numbers within a given interval. This feature is generally available.  Review [GENERATE_SERIES](/sql/t-sql/functions/generate-series-transact-sql) and [DATE_BUCKET](/sql/t-sql/functions/date-bucket-transact-sql) to learn more. 

 

### October 2022

| Changes | Details |
| --- | --- |
| **Approximate percentiles preview** | Support has been added to quickly compute percentiles using approximate percentile aggregate functions for large datasets with acceptable rank-based error bounds. This feature is currently in preview. To learn more, review [Approx_Percentile_Cont](/sql/t-sql/functions/approx-percentile-cont-transact-sql) and [Approx_Percentile_Disc](/sql/t-sql/functions/approx-percentile-disc-transact-sql). | 
| **Automated TDE key rotation for CMK GA** | Automatically switch to a new key when using a customer-managed key (CMK) with TDE. This feature is now generally available. To learn more, review [Automated key rotation](../database/transparent-data-encryption-byok-overview.md#rotation-of-tde-protector). |
| **Database copy of Hyperscale geo secondary replicas GA** | It's now possible to use a Hyperscale geo-secondary replica as source for a database copy. This feature is generally available. For more information, see [Hyperscale service tier](service-tier-hyperscale.md). |


### September 2022

| Changes | Details |
| --- | --- |
| **Cross-subscription failover group with Azure PowerShell** | It's now possible to deploy your auto-failover group for a single database across subscriptions by using Azure PowerShell. To learn more, review [Configure auto-failover group](auto-failover-group-configure-sql-db.md?view=azuresql&tabs=azure-powershell&pivots=azure-sql-single-db&preserve-view=true#create-failover-group). |
| **Hyperscale databases LTR preview** | It's now possible to store your Hyperscale database backups for up to 10 years using the long-term retention (LTR) capability. This feature is now in preview. To learn more, review [long-term retention](long-term-retention-overview.md).  | 
| **Hyperscale RA-GZRS preview** | It's now possible to choose read access geo-zone-redundancy (RA-GZRS) as a backup storage redundancy for Hyperscale databases. This feature is currently in preview. To learn more, review [Hyperscale backup storage redundancy](hyperscale-automated-backups-overview.md#data-and-backup-storage-redundancy). |
| **Hyperscale reverse migrate GA** |  This feature allows customers who have recently migrated an existing database in Azure SQL Database to the Hyperscale service tier to move back in an emergency, should Hyperscale not meet their needs. While reverse migration is initiated by a service tier change, it's essentially a size-of-data move between different architectures. This feature is now generally available. To learn more, review [Reverse migration to the General Purpose service tier](manage-hyperscale-database.md#reverse-migrate-from-hyperscale).|
| **UMI support for auditing preview** | It's now possible to configure the storage account used for SQL auditing logs by using User Managed Identity (UMI). This feature is currently in preview. Review [auditing](auditing-overview.md) to learn more. |

### August 2022

| Changes | Details |
| --- | --- |
| **Zone redundant configuration for Hyperscale databases GA** | The zone redundant configuration feature utilizes [Azure Availability Zones](/azure/availability-zones/az-overview#availability-zones) to replicate databases across multiple physical locations within an Azure region. By selecting [zone redundancy](high-availability-sla.md#hyperscale-service-tier-zone-redundant-availability), you can make your Hyperscale databases resilient to a much larger set of failures, including catastrophic datacenter outages, without any changes to the application logic. This configuration option is now generally available. To learn more, review  [Zone redundant configuration for Hyperscale databases](high-availability-sla.md#hyperscale-service-tier-zone-redundant-availability). |
| **Query Store hints GA** | You can use query hints to optimize your query execution via the OPTION clause. This feature is now generally available for Azure SQL Database. To learn more, review [Query Store hints](/sql/relational-databases/performance/query-store-hints?view=azuresqldb-current&preserve-view=true). |

### June 2022

| Changes | Details |
| --- | --- |
| **Named Replicas for Hyperscale databases GA** | Named Replicas enable a broad variety of read scale-out scenarios, and easily implement near-real time hybrid transactional and analytical processing (HTAP) solutions. This feature is now generally available. See [named replicas](service-tier-hyperscale-replicas.md#named-replica) to learn more. |
| **Active geo-replication and Auto-failover groups for Hyperscale databases GA** | [Active geo-replication](./active-geo-replication-overview.md) and [Auto-failover groups](./auto-failover-group-sql-db.md) are now generally available for Hyperscale databases,  providing a turn-key business continuity solution, letting you perform quick disaster recovery of databases in case of a regional disaster or a large scale outage. |

### May 2022

| Changes | Details |
| --- | --- |
| **Ledger GA** | The ledger feature in SQL Database is now generally available. Use the ledger feature to cryptographically attest to other parties, such as auditors or other business parties, that your data hasn't been tampered with. See [Ledger](ledger-landing.yml) to learn more.|
| **JavaScript & Python bindings**| Support for JavaScript and Python SQL bindings for Azure Functions is currently in preview. See [Azure SQL bindings for Azure Functions](/azure/azure-functions/functions-bindings-azure-sql) to learn more. |
| **Local development experience** | The Azure SQL Database local development experience is a combination of tools and procedures that empowers application developers and database professionals to design, edit, build/validate, publish, and run database schemas for databases directly on their workstation using an Azure SQL Database containerized environment. To learn more, see [Local development experience for Azure SQL Database](local-dev-experience-overview.md). |
| **SQL Database emulator** | The Azure SQL Database emulator provides the ability to locally validate database and query design together with client application code in a simple and frictionless model as part of the application development process. The SQL Database emulator is currently in preview. Review [SQL Database emulator](local-dev-experience-sql-database-emulator.md) to learn more.  |
| **SDK-style SQL projects**| Use [Microsoft.Build.Sql](https://www.nuget.org/packages/Microsoft.Build.Sql) for SDK-style SQL projects in the SQL Database Projects extension in Azure Data Studio or Visual Studio Code. This feature is currently in preview. To learn more, see [SDK-style SQL projects](/sql/azure-data-studio/extensions/sql-database-project-extension-sdk-style-projects). |
| **Azure Synapse Link for SQL for Azure SQL Database** | Azure Synapse Link for SQL enables near real-time analytics over operational data in SQL Server 2022 and Azure SQL Database. With a seamless integration between operational stores and Azure Synapse Analytics dedicated SQL pools, Azure Synapse Link for SQL enables you to run analytics, business intelligence and machine learning scenarios on your operational data with minimum impact on source databases with a new change feed technology. For more information, see [What is Azure Synapse Link for SQL? (Preview)](/azure/synapse-analytics/synapse-link/sql-synapse-link-overview).|

### April 2022

| Changes | Details |
| --- | --- |
| **General Purpose tier Zone redundancy GA** | Enabling zone redundancy for your provisioned and serverless General Purpose databases and elastic pools is now generally available in select regions. To learn more, including region availability see [General Purpose zone redundancy](high-availability-sla.md#general-purpose-service-tier-zone-redundant-availability). |
| **Change data capture GA** | Change data capture (CDC) lets you track all the changes that occur on a database. Though this feature has been available for SQL Server for quite some time, using it with Azure SQL Database is now generally available. To learn more, see [Change data capture](/sql/relational-databases/track-changes/about-change-data-capture-sql-server).  |

### March 2022

| Changes | Details |
| --- | --- |
| **GA for maintenance window** | The [maintenance window](maintenance-window.md) feature allows you to configure a maintenance schedule for your Azure SQL Database and receive advance notifications of maintenance windows. [Maintenance window advance notifications](../database/advance-notifications.md) are in public preview for databases configured to use a non-default [maintenance window](maintenance-window.md).|
| **Hyperscale zone redundant configuration preview** | It's now possible to create new Hyperscale databases with zone redundancy to make your databases resilient to a much larger set of failures. This feature is currently in preview for the Hyperscale service tier. To learn more, see [Hyperscale zone redundancy](high-availability-sla.md#hyperscale-service-tier-zone-redundant-availability). |
| **Hyperscale storage redundancy GA** | Choosing your storage redundancy for your databases in the Hyperscale service tier is now generally available. See [Configure backup storage redundancy](hyperscale-automated-backups-overview.md#data-and-backup-storage-redundancy) to learn more. |
| [Elastic transactions](elastic-transactions-overview.md) | Elastic transactions allow you to execute distributed transactions among cloud databases in Azure SQL Database and Azure SQL Managed Instance. Elastic transactions are now generally available. |

### February 2022

| Changes | Details |
| --- | --- |
| **Hyperscale reverse migration** | Reverse migration is now in preview. Reverse migration to the General Purpose service tier allows customers who have recently migrated an existing database in Azure SQL Database to the Hyperscale service tier to move back in an emergency, should Hyperscale not meet their needs. While reverse migration is initiated by a service tier change, it's essentially a size-of-data move between different architectures. Learn about [reverse migration from Hyperscale](manage-hyperscale-database.md#reverse-migrate-from-hyperscale). Update: This feature is now generally available as of September 2022.|
| **New Hyperscale articles** | We have reorganized some existing content into new articles and added new content for Hyperscale. Learn about [Hyperscale distributed functions architecture](hyperscale-architecture.md), [how to manage a Hyperscale database](manage-hyperscale-database.md), and how to [create a Hyperscale database](hyperscale-database-create-quickstart.md). |
| **Free Azure SQL Database** | Try Azure SQL Database for free using the Azure free account. To learn more, review [Try SQL Database for free](free-sql-db-free-account-how-to-deploy.md).|

### 2021

| Changes | Details |
| --- | --- |
| **Azure AD-only authentication** | Restricting authentication to your Azure SQL Database only to Azure Active Directory users is now generally available. To learn more, see [Azure AD-only authentication](../database/authentication-azure-ad-only-authentication.md). |
|**Split what's new** | The previously combined **What's new** article has been split by product - [What's new in SQL Database](doc-changes-updates-release-notes-whats-new.md) and [What's new in SQL Managed Instance](../managed-instance/doc-changes-updates-release-notes-whats-new.md), making it easier to identify what features are currently in preview, generally available, and significant documentation changes. Additionally, the [Known Issues in SQL Managed Instance](../managed-instance/doc-changes-updates-known-issues.md) content has moved to its own page.  |
| **Maintenance Window support for availability zones** | You can now use the [Maintenance Window feature](maintenance-window.md) if your Azure SQL Database is deployed to an availability zone. This feature is currently in preview.  |
| **Azure AD-only authentication** | It's now possible to restrict authentication to your Azure SQL Database to Azure Active Directory users only. This feature is currently in preview. To learn more, see [Azure AD-only authentication](authentication-azure-ad-only-authentication.md). |
| **Query store hints** | It's now possible to use query hints to optimize your query execution via the OPTION clause. To learn more, see [Query store hints](/sql/relational-databases/performance/query-store-hints?view=azuresqldb-current&preserve-view=true). |
| **Change data capture** | Using change data capture (CDC) with Azure SQL Database is now in preview. To learn more, see [Change data capture](/sql/relational-databases/track-changes/about-change-data-capture-sql-server). |
| **SQL Database ledger** | SQL Database ledger is in preview, and introduces the ability to cryptographically attest to other parties, such as auditors or other business parties, that your data hasn't been tampered with. To learn more, see [Ledger](/sql/relational-databases/security/ledger/ledger-overview). |
| **Maintenance window** | The maintenance window feature allows you to configure a maintenance schedule for your Azure SQL Database, currently in preview. To learn more, see [maintenance window](maintenance-window.md).|
| **SQL Insights** | SQL Insights is a comprehensive solution for monitoring any product in the Azure SQL family. The SQL Insights solution uses dynamic management views to expose the data you need to monitor health, diagnose problems, and tune performance. To learn more, see [SQL Insights](/azure/azure-monitor/insights/sql-insights-overview). |

## Contribute to content

To contribute to the Azure SQL documentation, see the [Docs contributor guide](/contribute/).
