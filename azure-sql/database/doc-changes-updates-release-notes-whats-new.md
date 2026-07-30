---
title: What's new?
titleSuffix: Azure SQL Database
description: Learn about the new features and documentation improvements for Azure SQL Database.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: mathoma, randolphwest
ms.date: 07/30/2026
ms.service: azure-sql-database
ms.subservice: service-overview
ms.topic: whats-new
ms.custom:
  - ignite-2025
monikerRange: "=azuresql || =azuresql-db"
---
# What's new in Azure SQL Database?

[!INCLUDE [appliesto-sqldb](../includes/appliesto-sqldb.md)]

> [!div class="op_single_selector"]
> * [Azure SQL Database](doc-changes-updates-release-notes-whats-new.md?view=azuresql&preserve-view=true)
> * [Azure SQL Managed Instance](../managed-instance/doc-changes-updates-release-notes-whats-new.md?view=azuresql&preserve-view=true)
> * [SQL Server on Azure VMs](../virtual-machines/windows/doc-changes-updates-release-notes-whats-new.md?view=azuresql&preserve-view=true)

This article summarizes the documentation changes associated with new features and improvements in the recent releases of [Azure SQL Database](https://azure.microsoft.com/products/azure-sql/database/). For more information about Azure SQL Database, see [What is Azure SQL Database?](sql-database-paas-overview.md)

> [!TIP]
> [Deploy Azure SQL Database for free](free-offer.md), for the life of your Azure subscription. This free offer provides up to ten free General Purpose databases, each with 100,000 vCore seconds of compute, every month.

For more announcements, discussion, and community content, see the [Azure SQL Database blog](https://techcommunity.microsoft.com/category/azuredatabases/blog/azuresqlblog).

## Preview

The following table lists the features of Azure SQL Database that are currently in preview.

> [!NOTE]  
> Features currently in preview are available under [supplemental terms of use](https://azure.microsoft.com/support/legal/preview-supplemental-terms/), review for legal terms that apply to Azure features that are in beta, preview, or otherwise not yet released into general availability. Azure SQL Database provides previews to give you a chance to evaluate and [share feedback with the product group](https://feedback.azure.com/d365community/forum/ef2b2b38-2f25-ec11-b6e6-000d3a4f0f84) on features before they become generally available (GA).

| Feature | Details |
| --- | --- |
| **160 and 192 vCore for Hyperscale Premium-series**|160 and 192 vCore options for Hyperscale Premium-series are now available as a preview offering, for both single Hyperscale databases and Hyperscale elastic pools. For more information, see [160 and 192 vCore offering for Hyperscale Premium Series](https://aka.ms/PRMS192vCores).|
| **AES keys for transparent data encryption**| The TDE protector for Azure SQL Database can now be backed by symmetric (AES) keys stored in Azure Key Vault Managed HSM, in addition to asymmetric (RSA) keys. For more information, see [Customer-managed transparent data encryption](transparent-data-encryption-byok-overview.md). |
| **Approximate or fuzzy string matching**| Check if two strings are similar, and calculate the difference between two strings. Use this capability to identify strings that might be different because of character corruption. [What is fuzzy string matching?](/sql/relational-databases/fuzzy-string-match/overview)|
| **Availability metric**| Availability is now a metric in the Azure Monitor metrics. Driven by a variety of user connection failures, you can [monitor and configure alerts on Azure SQL Database Availability](monitoring-metrics-alerts.md#availability-metric). |
| **Automatic index compaction** | [Automatic index compaction](/sql/relational-databases/indexes/automatic-index-compaction) helps you reduce the consumption of storage space, disk I/O, memory, and improve workload performance without investing time and effort into index maintenance jobs. |
|**Change event streaming** | Capture and publish row-level DML changes (inserts, updates, and deletes) on tracked tables in near real-time. Change event streaming publishes each change to Azure Event Hubs or Fabric Eventstream as a CloudEvent that includes the row's current schema, previous values, and new values, serialized as either native JSON or Avro Binary. To learn more, review [Change event streaming](/sql/relational-databases/track-changes/change-event-streaming/overview). |
| **Database watcher for Azure SQL** |[Database watcher](../database-watcher-overview.md) is a managed monitoring solution for database services in the Azure SQL family. Database watcher collects in-depth workload monitoring data to give you a detailed view of database performance, configuration, and health. Learn more about [database watcher](https://aka.ms/dbwatcher-preview-announcement). |
| **Data Virtualization for Azure SQL Database** |Data virtualization, now in preview in Azure SQL Database, enables you to leverage all the power of Transact-SQL (T-SQL) and seamlessly query external data from Azure Data Lake Storage Gen2 or Azure Blob Storage. For more information, see [Data virtualization with Azure SQL Database (Preview)](data-virtualization-overview.md).|
| **Elastic queries** | The [elastic queries](elastic-query-overview.md) feature allows for cross-database queries in Azure SQL Database. |
| **Elastic transactions** | [Elastic transactions](elastic-transactions-overview.md) allow you to execute transactions distributed among cloud databases in Azure SQL Database. |
| **Fixed server roles** | To simplify permission management, Azure SQL Database provides a set of [fixed server-level roles](security-server-roles.md) to help you manage the permissions on a logical server. | 
| **Immutable LTR backups** | You can [configure long-term retention backups of Azure SQL Database as immutable](backup-immutability.md) with legal hold immutability (preview feature). Time-based backup immutability is generally available.| 
| **Import and export using managed identity** | You can [import or export an Azure SQL Database BACPAC file with managed identity authentication](database-import-export-managed-identity.md). Use managed identity authentication for enhanced security when importing or exporting databases. |
| **Import and export using Private Link** | You can [import or export an Azure SQL Database using private link](database-import-export-private-link.md). Leave *Allow Access to Azure Services* off when you import or export a database using a service-managed endpoint. |
| **MAX_ROLLOVER_FILES in extended event sessions using Azure Storage blobs** | For new extended event sessions, you can specify the `MAX_ROLLOVER_FILES` option to retain only the specified number of the most recent Azure Storage blobs. For more information, see [event_file target](/sql/relational-databases/extended-events/targets-for-extended-events-in-sql-server#event_file-target). |
| **Multiple geo-replicas for Hyperscale** | The ability to create up to four geo-replicas for Azure SQL Hyperscale is in preview.  Learn more about [multiple geo-replicas for Hyperscale](https://aka.ms/sqlhs-multi-geo-announcement). |
| **Multiple secondaries for failover groups** | You can [configure multiple secondary servers for failover groups](failover-group-sql-db.md#multiple-secondaries) in Azure SQL Database. |
| **Network Security Perimeter** | [Azure Network Security Perimeter](network-security-perimeter.md) allows organizations to define a logical network isolation boundary for PaaS resources (for example, Azure Storage and SQL Database) that are deployed outside your organization's virtual networks. It restricts public network access to PaaS resources outside of the perimeter, and access can be exempted by using explicit access rules for public inbound and outbound. |
| **New Query editor experience in the Azure portal** | As of March 2026, there's a new [query editor in the Azure portal](query-editor.md) that allows you to run queries against your Azure SQL Database directly from a web browser. |
| **Query Store for readable secondary replicas** | [Query Store for readable secondary replicas](/sql/relational-databases/performance/query-store-for-secondary-replicas) enables Query Store insights for workloads that run on secondary replicas. When enabled, secondary replicas stream query execution information (such as runtime and wait statistics) to the primary replica, where the data is persisted in Query Store and made visible across all replicas. |
| **Regex-based dynamic data masking** | Use the `REGEXP_REPLACE` masking function to define pattern-driven masking rules for variable-length string data such as email addresses, phone numbers, and identifiers. For more information, see [Regex-based dynamic data masking in Azure SQL Database (preview)](dynamic-data-masking-regex.md). |
| **Restart database in the Azure portal** | You can [restart your SQL database or elastic pool](restart-database.md) from the Azure portal. |
| **Soft delete retention for logical server** | You can [configure a soft delete retention period](deleted-logical-server-restore.md) for a logical server and restore a deleted server within the retention period.|
| **Vector indexing and VECTOR_SEARCH enhancements** | DiskANN vector indexes are now in preview with full DML support, iterative filtering, the new `SELECT TOP (N) WITH APPROXIMATE` syntax, and the `FORCE_ANN_ONLY` table hint. Earlier vector index versions and the `TOP_N` parameter are deprecated. For more information, see [CREATE VECTOR INDEX](/sql/t-sql/statements/create-vector-index-transact-sql?view=azuresqldb-current&preserve-view=true) and [VECTOR_SEARCH](/sql/t-sql/functions/vector-search-transact-sql?view=azuresqldb-current&preserve-view=true). |


## General availability (GA)

The following table lists features of Azure SQL Database that have been made generally available (GA) within the last 12 months:

| Feature | GA Month | Details |
| --- | --- | --- |
| **Microsoft Entra server principals** | June 2026 | The ability to [create server principals (logins) for Microsoft Entra identities](authentication-azure-ad-logins.md) in Azure SQL Database is now generally available. |
| **DATEADD number allows bigint** | November 2025 | For `DATEADD (datepart, number, date)`, number can be expressed as a **bigint**. For more information, see [DATEADD (Transact-SQL)](/sql/t-sql/functions/dateadd-transact-sql).|
| **Regular expression functions** | November 2025 | Regular expression (REGEX) functions return text based on values in a search pattern. [Regular expressions](/sql/relational-databases/regular-expressions/overview). |
| **Immutable LTR backups** | November 2025 | You can [configure long-term retention backups of Azure SQL Database as immutable](backup-immutability.md) with time-based immutability. | 
| **Convert to Hyperscale with geo-replicas** | October 2025 | The ability to [convert a geo-replicated database non-Hyperscale database to Hyperscale](convert-to-hyperscale.md) using T-SQL, REST API, PowerShell, or Azure CLI is now generally available. For more information, see [Blog: Hyperscale conversion support for geo-replicas](https://aka.ms/hs-conversion-geodr-ga). |
| **ABORT_QUERY_EXECUTION** | October 2025 | The `ABORT_QUERY_EXECUTION` [query hint](/sql/t-sql/queries/hints-transact-sql-query?view=azuresqldb-current&preserve-view=true#use_hint_abort_query_execution) can block future execution of known problematic queries, for example nonessential queries causing high resource consumption and impacting critical application workloads. For more information, see [Query Store hints: Block future execution of problematic queries](/sql/relational-databases/performance/query-store-hints-best-practices?view=azuresqldb-current&preserve-view=true#block-future-execution-of-problematic-queries). |
| **sys.dm_hs_database_replicas** | August 2025 | You can query the details of Azure SQL Database Hyperscale replicas with the new dynamic management view (DMV) [sys.dm_hs_database_replicas](/sql/relational-databases/system-functions/sys-dm-hs-database-replicas).|
| **UNISTR (Transact-SQL)** | July 2025 | Azure SQL Database now supports the `UNISTR` T-SQL syntax for Unicode string literals. For more information, see [UNISTR (Transact-SQL)](/sql/t-sql/functions/unistr-transact-sql).|
| **\|\| (String concatenation) and \|\|= (Compound assignment) syntax support** | July 2025 | Azure SQL Database now supports [\|\| (String concatenation)](/sql/t-sql/language-elements/string-concatenation-pipes-transact-sql?view=azuresqldb-current&preserve-view=true) and [\|\|= (Compound assignment)](/sql/t-sql/language-elements/compound-assignment-pipes-transact-sql?view=azuresqldb-current&preserve-view=true) Transact-SQL syntax.|
| **Degrees of Parallelism (DOP) feedback** | July 2025 | [DOP Feedback](/sql/relational-databases/performance/intelligent-query-processing-degree-parallelism-feedback?view=azuresqldb-current&preserve-view=true) is now generally available for Azure SQL Database. For more information, see [Smarter Parallelism: Degree of parallelism feedback in SQL Server 2025](https://techcommunity.microsoft.com/blog/sqlserver/smarter-parallelism-degree-of-parallelism-feedback-in-sql-server-2025/4431318). |
| **Audit re-architecture** | July 2025 | Increased availability and reliability of server audits through a re-architecture of auditing in Azure SQL Database that is closely aligned with SQL Server and Azure SQL Managed Instance. For more information, see [Auditing](auditing-overview.md#enhancements-to-performance-availability-and-reliability-in-server-auditing-for-azure-sql-database-july-2025-ga).|
| **Vector data type and functions** | June 2025 | Working with vector data is now easier in Azure SQL Database with the introduction of a new [vector data type](/sql/t-sql/data-types/vector-data-type?view=azuresqldb-current&preserve-view=true) and [vector functions](/sql/t-sql/functions/vector-functions-transact-sql?view=azuresqldb-current&preserve-view=true). For more information, see [Intelligent applications with Azure SQL Database](ai-artificial-intelligence-intelligent-applications.md#vectors).|
| **Hyperscale increased log generation rate** | May 2025 | The transaction log generation rate in Azure SQL Database Hyperscale single databases has been increased from 100 MiB/s to 150 MiB/s for premium-series and premium-series memory optimized hardware. For more information, read [Blog: Enhancements to Azure SQL Database Hyperscale](https://aka.ms/HSenhancements).|
| **Hyperscale continuous priming** | May 2025 | [Continuous priming](service-tier-hyperscale.md#continuous-priming) optimizes Hyperscale performance during failovers by priming high availability secondary compute replicas. Continuous priming is now generally available. For more information, read [Blog: Enhancements to Azure SQL Database Hyperscale](https://aka.ms/HSenhancements). |
| **JSON native data type** | May 2025 | The [**json** data type and JSON aggregate functions](/sql/t-sql/data-types/json-data-type?view=azuresqldb-current&preserve-view=true) provide new capabilities for handling semistructured data in Azure SQL Database. |
| **JSON aggregate functions** | May 2025 | Two [**json** aggregate functions `JSON_OBJECTAGG` and `JSON_ARRAYAGG`](/sql/relational-databases/json/json-data-sql-server?view=azuresqldb-current&preserve-view=true#json-data-from-aggregates) enable construction of JSON objects or arrays based on an aggregate from SQL data. |
| **Copilot in Microsoft Azure with SQL Database** | April 2025 | [Copilot in Azure with Azure SQL Database](../copilot/copilot-azure-sql-overview.md) is a [capability](/azure/copilot/capabilities#perform-tasks) of the [Microsoft Copilot in Azure](/azure/copilot/overview) experience that enhances the management and operation of Azure services, providing robust capabilities for SQL-dependent applications. |
| **Manually initiate cutover for conversion to Hyperscale** | April 2025 | When converting an Azure SQL Database to the Hyperscale service tier, you have a new [option to manually initiate the cutover](https://aka.ms/hs-conversion-v2-ga). For more information, see [Convert an existing database to Hyperscale](convert-to-hyperscale.md). |


## Documentation changes

Learn about significant changes to the Azure SQL Database documentation. For previous years, see the [What's new archive](doc-changes-updates-release-notes-whats-new-archive.md).

### July 2026

| Changes | Details |
| --- | --- |
| **Change event streaming to Fabric Eventstreams** | You can now [stream SQL data changes to Fabric Eventstreams](/fabric/real-time-intelligence/event-streams/stream-sql-change-events-to-eventstream) in addition to Azure Event Hubs. Change event streaming (CES) captures and publishes incremental changes of data to the destination in near real-time. Captured changes include updates, inserts, and deletes (DML). For more information, see [Change event streaming](/sql/relational-databases/track-changes/change-event-streaming/overview). |
| **Regex-based dynamic data masking (preview)** | You can [define pattern-driven masking rules with the `REGEXP_REPLACE` function](dynamic-data-masking-regex.md) for variable-length string data such as email addresses, phone numbers, and identification numbers. Regex masks preserve the segments you choose to reveal, which the built-in masking functions can't express. This capability is now in preview. |

### June 2026

| Changes | Details |
| --- | --- |
| **AES keys for transparent data encryption (preview)** | The TDE protector for Azure SQL Database can now be backed by symmetric (AES) keys stored in Azure Key Vault Managed HSM, in addition to asymmetric (RSA) keys. This capability is in public preview. For more information, see [Customer-managed transparent data encryption](transparent-data-encryption-byok-overview.md). |
| **Microsoft Entra server principals** | The ability to [create server principals (logins) for Microsoft Entra identities](authentication-azure-ad-logins.md) in Azure SQL Database is now generally available. |

### May 2026

| Changes | Details |
| --- | --- |
| **MAX_ROLLOVER_FILES in extended event sessions using Azure Storage blobs (preview)** | For new extended event sessions, you can specify the `MAX_ROLLOVER_FILES` option to retain only the specified number of the most recent Azure Storage blobs. For more information, see [event_file target](/sql/relational-databases/extended-events/targets-for-extended-events-in-sql-server#event_file-target). |



### March 2026

| Changes | Details |
| --- | --- |
| **160 and 192 vCore for Hyperscale Premium-series**|160 and 192 vCore options for Hyperscale Premium-series are now available as a preview offering, for both single Hyperscale databases and Hyperscale elastic pools. For more information, see [160 and 192 vCore offering for Hyperscale Premium-series](https://aka.ms/PRMS192vCores).|
| **Automatic index compaction preview** | [Automatic index compaction](/sql/relational-databases/indexes/automatic-index-compaction) helps you reduce the consumption of storage space, disk I/O, memory, and improve workload performance without investing time and effort into index maintenance jobs. This feature is now in preview. |
| **Import and export using managed identity preview** | You can [import or export an Azure SQL Database BACPAC file with managed identity authentication](database-import-export-managed-identity.md). Use managed identity authentication for enhanced security when importing or exporting databases. This capability is currently in preview for Azure SQL Database. |
| **New Azure portal query editor experience**|A new Query editor (preview) experience in the Azure portal offers a new modernized authentication page and consistency with other portal query editor experiences. For more information, see [Quickstart: Use the Azure portal query editor to query Azure SQL Database](connect-query-portal.md). |
| **Soft delete retention for logical server preview** | You can [configure a soft delete retention period](deleted-logical-server-restore.md) for a logical server and restore a deleted server within the retention period. This capability is now in preview.|
| **Vector indexing and VECTOR_SEARCH enhancements** | Latest version vector indexes now support full DML operations, iterative filtering, new `SELECT TOP (N) WITH APPROXIMATE` syntax, the `FORCE_ANN_ONLY` table hint, and the new [sys.dm_db_vector_indexes](/sql/relational-databases/system-dynamic-management-views/sys-dm-db-vector-indexes-transact-sql?view=azuresqldb-current&preserve-view=true) DMV for monitoring index health. Regional availability is documented at [Feature availability by region](region-availability.md#vector-search). For more information, see [VECTOR_SEARCH](/sql/t-sql/functions/vector-search-transact-sql?view=azuresqldb-current&preserve-view=true) and [CREATE VECTOR INDEX](/sql/t-sql/statements/create-vector-index-transact-sql?view=azuresqldb-current&preserve-view=true). |

### January 2026

| Changes | Details |
| --- | --- |
| **Multiple secondaries for failover groups** | You can [configure multiple secondary servers for failover groups](failover-group-sql-db.md#multiple-secondaries) in Azure SQL Database. |



## Archive

For previous updates, see the [What's new archive](doc-changes-updates-release-notes-whats-new-archive.md).

## Contribute to content

To contribute to the Azure SQL documentation, see the [Docs contributor guide](/contribute/).
