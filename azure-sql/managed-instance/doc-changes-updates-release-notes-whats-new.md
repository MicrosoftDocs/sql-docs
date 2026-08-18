---
title: What's new?
titleSuffix: Azure SQL Managed Instance
description: Learn about the new features and documentation improvements for Azure SQL Managed Instance.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: wiassaf, randolphwest
ms.date: 08/17/2026
ms.service: azure-sql-managed-instance
ms.subservice: service-overview
ms.topic: whats-new
ms.custom:
  - ignite-2025
---
# What's new in Azure SQL Managed Instance?
[!INCLUDE [appliesto-sqldb-sqlmi](../includes/appliesto-sqlmi.md)]

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
|[Approximate or fuzzy string matching](/sql/relational-databases/fuzzy-string-match/overview)| Check if two strings are similar, and calculate the difference between two strings. Use this capability to identify strings that might be different because of character corruption.|
| [Automatic index compaction](/sql/relational-databases/indexes/automatic-index-compaction) | Reduce the consumption of storage space, disk I/O, memory, and improve workload performance without investing time and effort into index maintenance jobs. |
| [Change event streaming](/sql/relational-databases/track-changes/change-event-streaming/overview) | Capture and publish row-level DML changes (inserts, updates, and deletes) on tracked tables in near real-time. Change event streaming publishes each change to Azure Event Hubs or Fabric Eventstream as a CloudEvent that includes the row's current schema, previous values, and new values, serialized as either native JSON or Avro Binary. |
|[Database watcher for Azure SQL](../database-watcher-overview.md) | Database watcher is a managed monitoring solution for database services in the Azure SQL family. Database watcher collects in-depth workload monitoring data to give you a detailed view of database performance, configuration, and health. Learn more about [database watcher](https://aka.ms/dbwatcher-preview-announcement).|
|[Endpoint policies](./service-endpoint-policies-configure.md) | Configure which Azure Storage accounts can be accessed from a SQL Managed Instance subnet. Grants an extra layer of protection against inadvertent or malicious data exfiltration.|
| [Flexible memory](resource-limits.md#flexible-memory) | Save on cost and better serve your workload needs by modifying the memory allocation for your SQL managed instance. Flexible memory is in preview on Premium-series hardware for Business Critical instances (locally redundant and zone-redundant) and for zone-redundant Next-gen General Purpose instances. |
|[Modernization Advisor](../virtual-machines/modernization-advisor.md) | Use the Modernization Advisor in the Azure portal to help you determine if migrating to Azure SQL Managed Instance from a SQL Server VM saves you money or optimizes performance. |
|[SDK-style SQL project](/sql/tools/sql-database-projects/sql-database-projects) | Use [Microsoft.Build.Sql](https://www.nuget.org/packages/Microsoft.Build.Sql) for SDK-style SQL projects in the SQL Database Projects extension in Visual Studio Code. SDK-style SQL projects are especially advantageous for applications shipped through pipelines or built in cross-platform environments.|
|[Service Broker](/sql/database-engine/configure-windows/sql-server-service-broker) | Support for cross-instance message exchange using Service Broker between instances of Azure SQL Managed Instance, and between SQL Server and Azure SQL Managed Instance. |
|[Vector data type and functions](/sql/t-sql/data-types/vector-data-type?view=azuresqlmi-current&preserve-view=true) | Working with vector data is now easier in Azure SQL Managed Instance with the introduction of a new [vector data type](/sql/t-sql/data-types/vector-data-type?view=azuresqlmi-current&preserve-view=true) and [vector functions](/sql/t-sql/functions/vector-functions-transact-sql?view=azuresqlmi-current&preserve-view=true). For more information, see [Intelligent applications with Azure SQL Managed Instance](ai-artificial-intelligence-intelligent-applications.md#vectors). |
| [Query Store for readable secondary replicas](/sql/relational-databases/performance/query-store-for-secondary-replicas) | Query Store for readable secondary replicas enables Query Store insights for workloads that run on secondary replicas. When enabled, secondary replicas stream query execution information (such as runtime and wait statistics) to the primary replica, where the data is persisted in Query Store and made visible across all replicas. |
| [Zone redundancy for Next-gen General Purpose](high-availability-sla-local-zone-redundancy.md#zone-redundant-availability) | Improve resilience to availability zone outages by enabling zone redundancy for a Next-gen General Purpose SQL managed instance. The preview also supports flexible memory for zone-redundant instances on Premium-series hardware. |

## General availability (GA)

The following table lists features of Azure SQL Managed Instance that have been made generally available (GA) within the last 12 months:

| Feature | GA Month | Details |
| ---| --- |--- |
|[Automatic backup immutability](../automatic-backup-immutability.md) | August 2026 | Automatic backup immutability protects the most recent seven days of point-in-time restore backups in Azure SQL Managed Instance. |
|[Internal connectivity testing](connectivity-testing-overview.md) | May 2026 | Azure SQL Managed Instance now performs automatic internal connectivity tests to monitor service reliability and accelerate issue detection. |
|[Block T-SQL CRUD commands](../database/block-crud-tsql.md) | March 2026 | Azure administrators can block T-SQL commands to create or modify Azure SQL resources. |
|[SQL Server 2025 update policy](update-policy.md#sql-server-2025-update-policy) | March 2026 | Align your SQL managed instance database format with the SQL Server 2025 database engine. | 
|[DATEADD number allows bigint](/sql/t-sql/functions/dateadd-transact-sql) | November 2025 | For `DATEADD (datepart, number, date)`, number can be expressed as a **bigint**.|
|[Regular expression functions](/sql/relational-databases/regular-expressions/overview) | November 2025 | Regular expression (REGEX) functions return text based on values in a search pattern. |
|[Flexible memory](resource-limits.md#flexible-memory) | November 2025 | Save on cost by choosing the memory allocation for your [Next-gen General Purpose](service-tiers-next-gen-general-purpose-use.md) instance based on your workload needs.|
|[Next-gen General Purpose](service-tiers-next-gen-general-purpose-use.md) | November 2025 | An architectural upgrade of the General Purpose service tier that uses [Elastic SAN storage](/azure/storage/elastic-san/elastic-san-introduction) for greater resource flexibility, and improved performance while maintaining the same baseline cost as the General Purpose service tier.  |
|[Migrate SQL Server to Azure](/sql/sql-server/azure-arc/migrate-to-azure-sql-managed-instance) | October 2025 | Migrate your SQL Server enabled by Azure Arc instance to Azure SQL Managed Instance through the Azure portal.|
|[Optimized locking](/sql/relational-databases/performance/optimized-locking)| July 2025 | Azure SQL Managed Instance with the **Always-up-to-date** and **SQL Server 2025** [update policy](update-policy.md) now has optimized locking enabled for all user databases. |
|[UNISTR (Transact-SQL)](/sql/t-sql/functions/unistr-transact-sql) | July 2025 | Azure SQL Managed Instance now supports the `UNISTR` T-SQL syntax for Unicode string literals.|
|[\|\| (String concatenation)](/sql/t-sql/language-elements/string-concatenation-pipes-transact-sql?view=azuresqlmi-current&preserve-view=true) and [\|\|= (Compound assignment)](/sql/t-sql/language-elements/compound-assignment-pipes-transact-sql?view=azuresqlmi-current&preserve-view=true) syntax support | July 2025 |Azure SQL Managed Instance now supports [\|\| (String concatenation)](/sql/t-sql/language-elements/string-concatenation-pipes-transact-sql?view=azuresqlmi-current&preserve-view=true) and [\|\|= (Compound assignment)](/sql/t-sql/language-elements/compound-assignment-pipes-transact-sql?view=azuresqlmi-current&preserve-view=true) Transact-SQL syntax.|
|[Degrees of parallelism (DOP) feedback](/sql/relational-databases/performance/intelligent-query-processing-degree-parallelism-feedback?view=azuresqldb-mi-current&preserve-view=true) | July 2025|  DOP feedback improves query performance by identifying parallelism inefficiencies for repeating queries, based on elapsed time and waits. For more information, see the [Smarter Parallelism: Degree of parallelism feedback in SQL Server 2025](https://techcommunity.microsoft.com/blog/sqlserver/smarter-parallelism-degree-of-parallelism-feedback-in-sql-server-2025/4431318) blog. |
| [Vector data type](/sql/t-sql/data-types/vector-data-type?view=azuresqlmi-current&preserve-view=true) and [vector functions](/sql/t-sql/functions/vector-functions-transact-sql?view=azuresqlmi-current&preserve-view=true) | June 2025 | Working with vector data is now easier in Azure SQL Managed Instance with the Always-up-to-date update policy, with the introduction of a new vector data type and vector functions. For more information, see [Intelligent applications with Azure SQL Managed Instance](/sql/sql-server/ai/artificial-intelligence-intelligent-applications#vectors).|
|[Zone redundancy for General Purpose](high-availability-sla-local-zone-redundancy.md#zone-redundant-availability) | June 2025|  Deploy your General Purpose SQL Managed Instance to multiple availability zones to improve the availability of your instance in the event of a disaster. | 
|[Invoke an HTTPS REST endpoint SP](/sql/relational-databases/system-stored-procedures/sp-invoke-external-rest-endpoint-transact-sql) | June 2025 | Use the `sp_invoke_external_rest_endpoint` stored procedure to invoke an HTTPS REST endpoint provided as an input argument to the procedure. | 

## Documentation changes

Learn about significant changes to the Azure SQL Managed Instance documentation. For previous years, see the [What's new archive](doc-changes-updates-release-notes-whats-new-archive.md).

### August 2026

| Changes | Details |
| --- | --- |
| **Automatic backup immutability GA** |  Automatic backup immutability protects up to the most recent seven days of point-in-time restore backups in Azure SQL Managed Instance. This feature is now generally available (GA). To learn more, see [Automatic backup immutability](../automatic-backup-immutability.md). |
| **Next-gen General Purpose zone redundancy preview** | Zone redundancy for the Next-gen General Purpose service tier is now in preview. The preview also supports flexible memory for zone-redundant instances on Premium-series hardware. To learn more, see [Availability through local and zone redundancy](high-availability-sla-local-zone-redundancy.md) and [Flexible memory](resource-limits.md#flexible-memory). |
| **Changes to quota request types** | SQL Managed Instance quota request types changed. Subnet quotas no longer apply, and vCore quotas are now scoped per hardware generation (Standard-series, Premium-series, and Memory optimized premium-series). Existing regional vCore allowances are carried forward for each hardware generation. To learn more, see [SQL Managed Instance quota request types](../database/quota-increase-request.md#sql-managed-instance-quota-request-types). |

### July 2026

| Changes | Details |
| --- | --- |
| **Change event streaming AMQP deprecation** | Starting August 15, 2026, the AMQP protocol is deprecated for change event streaming. Existing AMQP-configured stream groups continue working until April 2027. For migration steps and timelines, see [AMQP protocol deprecation](/sql/relational-databases/track-changes/change-event-streaming/amqp-deprecation). |
| **Change event streaming to Fabric Eventstreams** | You can now [stream SQL data changes to Fabric Eventstreams](/fabric/real-time-intelligence/event-streams/stream-sql-change-events-to-eventstream) in addition to Azure Event Hubs. Change event streaming (CES) captures and publishes incremental changes of data to the destination in near real-time. Captured changes include updates, inserts, and deletes (DML). For more information, see [Change event streaming](/sql/relational-databases/track-changes/change-event-streaming/overview). |

### May 2026

| Changes | Details |
| --- | --- |
| **Internal connectivity testing GA** | [!INCLUDE [auto-connectivity-tests](../includes/auto-connectivity-tests.md)] |


### April 2026

| Changes | Details |
| --- | --- |
| **Flexible memory preview** | It's now possible to modify the memory allocation for your Business Critical SQL managed instance based on your workload needs. This capability is now in preview for this service tier. Flexible memory is generally available (GA) for the Next-gen General Purpose service tier. To learn more, review [Flexible memory](resource-limits.md#flexible-memory). |

### March 2026

| Changes | Details |
| --- | --- |
| **Automatic index compaction preview** | Automatic index compaction helps you reduce the consumption of storage space, disk I/O, memory, and improve workload performance without investing time and effort into index maintenance jobs. This feature is now in preview. To learn more, review [Automatic index compaction](/sql/relational-databases/indexes/automatic-index-compaction). |
| **Block T-SQL CRUD GA** | Allow Azure administrators to block the creation or modification of Azure SQL Managed Instance resources through T-SQL. This is enforced at the subscription level to block T-SQL commands from affecting SQL managed instance resources. This feature is generally available for Azure SQL Managed Instance. To learn more, review [Block T-SQL CRUD](../database/block-crud-tsql.md). |
| **Change event streaming preview** | Capture and publish row-level DML changes (inserts, updates, and deletes) on tracked tables in near real-time. Change event streaming publishes each change to Azure Event Hubs or Fabric Eventstream as a CloudEvent that includes the row's current schema, previous values, and new values, serialized as either native JSON or Avro Binary. This feature is now in preview for Azure SQL Managed Instance configured with the SQL Server 2025 and Always-up-to-date update policy. To learn more, review [Change event streaming](/sql/relational-databases/track-changes/change-event-streaming/overview).
| **Deploy free instance with command line tools** | You can now create your free SQL managed instance by using [Azure PowerShell](free-offer.md?tabs=powershell#create-a-free-sql-managed-instance), the [Azure CLI](free-offer.md?tabs=azure-cli#create-a-free-sql-managed-instance), and the [REST API](free-offer.md?tabs=rest-api#create-a-free-sql-managed-instance). |
| **Easily upgrade your free instance** | You can now easily upgrade your free SQL managed instance to a paid offer in the Azure portal. To upgrade, navigate to the **Overview** page for your instance and select **Upgrade** from the navigation bar to open the **Compute + storage** page, where you can choose the paid offer under **Offer type**. For more information, see [Free SQL Managed Instance](free-offer.md#upgrade-to-paid-instance). |
| **Free offer supportability** | The free SQL Managed Instance offer is now available in all regions, and for all subscription types, that support the paid Azure SQL Managed Instance offer. For more information about the free offer, see [Free SQL Managed Instance](free-offer.md#supportability). |
|**Migrate multiple DBs through Azure Arc** | You can now migrate multiple databases simultaneously from a SQL Server instance enabled with Azure Arc to Azure SQL Managed Instance by using the Managed Instance link. For more information, see [Migrate SQL Server database to Azure SQL Managed Instance](/sql/sql-server/azure-arc/migration-sql-mi-prepare-link). |
| **SQL Server 2025 update policy GA** | Align your SQL managed instance database format with the SQL Server 2025 Database Engine. This update policy is now generally available. For more information, review [SQL Server 2025 update policy](update-policy.md#sql-server-2025-update-policy). |

### February 2026

| Changes | Details |
| --- | --- |
| **Query Store for readable secondary replicas preview** | Query Store for readable secondary replicas enables Query Store insights for workloads that run on secondary replicas. When enabled, secondary replicas stream query execution information (such as runtime and wait statistics) to the primary replica, where the data is persisted in Query Store and made visible across all replicas. This feature is in preview for Azure SQL Managed Instance configured with the [Always-up-to-date update policy](update-policy.md#always-up-to-date-update-policy), and is enabled by default. For more information, see [Query Store for readable secondary replicas](/sql/relational-databases/performance/query-store-for-secondary-replicas). |

### December 2025

| Changes | Details |
| --- | --- |
|**Traffic management** | Learn about how network traffic is managed in Azure SQL Managed Instance, including user-managed and service-managed traffic. Review [Traffic management overview](traffic-management-overview.md) to learn more. |

### November 2025

| Changes | Details |
| --- | --- |
| **Regular expression functions preview** | Regular expression (REGEX) functions return text based on values in a search pattern.  Regular expression (REGEX) functions are now generally available for Azure SQL Managed Instance. For more information, see [Regular expressions](/sql/relational-databases/regular-expressions/overview) or [General Availability Announcement: Regex Support in SQL Server 2025 & Azure SQL](https://devblogs.microsoft.com/azure-sql/general-availability-announcement-regex-support-in-sql-server-2025-azure-sql/). |
|**Flexible memory GA** | Save on cost by choosing the memory allocation for your [Next-gen General Purpose](service-tiers-next-gen-general-purpose-use.md) instance based on your workload needs. This capability is now generally available. To learn more, review [Flexible memory](resource-limits.md#flexible-memory). |
|**Next-gen General Purpose GA** | An architectural upgrade of the General Purpose service tier that uses [Elastic SAN storage](/azure/storage/elastic-san/elastic-san-introduction) for greater resource flexibility, and improved performance while maintaining the same baseline cost as the General Purpose service tier. This upgrade to the service tier is now generally available (GA). To learn more, review [Next-gen General Purpose](service-tiers-next-gen-general-purpose-use.md).|
|**Zone redundancy add-on for GP** | To help save on cost, the zone redundancy reservation add-on is now available for Azure SQL Managed Instance in the General Purpose service tier. To learn more, review [zone redundancy reservation add-on](../database/reservations-discount-overview.md#reservations-for-zone-redundant-resources). |

### October 2025

| Changes | Details |
| --- | --- |
| **Database migration GA** | Migrate your SQL Server enabled by Azure Arc instance to Azure SQL Managed Instance through the Azure portal. This feature is now generally available (GA). Review [Migrate SQL Server instance to Azure SQL Managed Instance](/sql/sql-server/azure-arc/migrate-to-azure-sql-managed-instance) to learn more. |
| **Redirect connection type default** | The [redirect connection type](connection-types-overview.md#redirect-connection-type-default) is now default for all new and existing instances. The redirect connection type has better latency and throughput performance compared to the legacy proxy connection type. SQL managed instances with the `proxyOverride` value set to `Default` before October 2025 are converted to the legacy `Proxy` connection type, which you can change to `Redirect` manually using the Azure portal or PowerShell. |

### September 2025

| Changes | Details |
| --- | --- |
| **New Azure SQL hub** | Choosing the right Azure SQL service can be challenging. To make this easier, we built the [Azure SQL hub at aka.ms/azuresqlhub](https://aka.ms/azuresqlhub), a new home for everything related to Azure SQL in the Azure portal. For more information, read the blog post [Introducing the Azure SQL hub: A simpler, guided entry into Azure SQL](https://aka.ms/azuresqlhubblog). | 
|**SQL Server 2025 update policy preview**|  Align your SQL managed instance database format with the SQL Server 2025 database engine. This capability is now in preview. To learn more, review [SQL Server 2025 update policy](update-policy.md#sql-server-2025-update-policy). | 


## Archive

For previous news, see the [What's new archive](doc-changes-updates-release-notes-whats-new-archive.md).

## Known issues

The known issues content has moved to a dedicated [known issues in SQL Managed Instance](doc-changes-updates-known-issues.md) article. 


## Contribute to content

To contribute to the Azure SQL documentation, see the [Docs contributor guide](/contribute/).
