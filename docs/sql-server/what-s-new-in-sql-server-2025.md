---
title: "What's New in SQL Server 2025"
description: Learn about new features for SQL Server 2025 (17.x), which gives you choices of development languages, data types, environments, and operating systems.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: wiassaf, randolphwest
ms.date: 08/21/2025
ms.service: sql
ms.subservice: release-landing
ms.topic: whats-new
ms.custom:
  - intro-whats-new
  - build-2025
monikerRange: ">=sql-server-2016"
---

# What's new in SQL Server 2025 Preview

[!INCLUDE [sqlserver2025](../includes/applies-to-version/sqlserver2025.md)]

[!INCLUDE [sql-server-2025](../includes/sssql25-md.md)] builds on previous releases to grow SQL Server as a platform that gives you choices of development languages, data types, on-premises or cloud environments, and operating systems.

This article summarizes the new features and enhancements for [!INCLUDE [sql-server-2025](../includes/sssql25-md.md)].

&nbsp;

> [!VIDEO https://www.youtube-nocookie.com/embed/3RgncKX6K9A]

## Get SQL Server 2025

[Get SQL Server 2025 Preview](https://info.microsoft.com/ww-landing-sql-server-2025.html).

Review:

- [Known issues](sql-server-2025-release-notes.md#known-issues)
- [Build number](sql-server-2025-release-notes.md#build-number)
- [Breaking changes](../database-engine/breaking-changes-to-database-engine-features-in-sql-server-2025.md)

For the best experience with [!INCLUDE [sql-server-2025](../includes/sssql25-md.md)], use the [latest tools](../tools/overview-sql-tools.md).

## Release candidate 0

Currently [!INCLUDE [sql-server-2025](../includes/sssql25-md.md)] includes features available through release candidate (RC) 0.

### Upgrade

Beginning with RC 0, [!INCLUDE [sssql25-md](../includes/sssql25-md.md)] supports upgrading from previous versions of SQL Server. The operating system environment must meet the requirements in [Hardware and software requirements for SQL Server 2025 Preview](install/hardware-and-software-requirements-for-installing-sql-server-2025.md). For known issues, review [SQL Server 2025 Preview release notes](sql-server-2025-release-notes.md).

### New features

> [!TIP]  
> Explore new development features in the database using an opt-in mechanism through a new database scoped configuration - [PREVIEW_FEATURES](../t-sql/statements/alter-database-scoped-configuration-transact-sql.md#preview-features). To learn more, review the [release notes](sql-server-2025-release-notes.md).

In addition to features announced previously, RC 0 adds the following changes and features:

- AI
  - Vector search and index:
    - [CREATE VECTOR INDEX](../t-sql/statements/create-vector-index-transact-sql.md) is available via [PREVIEW_FEATURES database scoped configuration](../t-sql/statements/alter-database-scoped-configuration-transact-sql.md#preview-features) option
    - [VECTOR_SEARCH](../t-sql/functions/vector-search-transact-sql.md): is available via [`PREVIEW_FEATURES` database scoped configuration](../t-sql/statements/alter-database-scoped-configuration-transact-sql.md#preview-features) option
    - Updated [sys.vector_indexes](../relational-databases/system-catalog-views/sys-vector-indexes-transact-sql.md): Fixed column names and inherited columns from `sys.indexes`
  - Text chunking
  - [AI_GENERATE_CHUNKS](../t-sql/functions/ai-generate-chunks-transact-sql.md): is available via [`PREVIEW_FEATURES` database scoped configuration](../t-sql/statements/alter-database-scoped-configuration-transact-sql.md#preview-features) option
  - [CREATE EXTERNAL MODEL](../t-sql/statements/create-external-model-transact-sql.md) now supports local ONNX models hosted directly on the SQL Server via [`PREVIEW_FEATURES` database scoped configuration](../t-sql/statements/alter-database-scoped-configuration-transact-sql.md#preview-features) option. Currently available for instances on Windows only.
- Linux
  - [Support for TLS 1.3](../linux/sql-server-linux-encrypted-connections.md)
  - [Support for Ubuntu 24.04](../linux/quickstart-install-connect-ubuntu.md)
- Database engine
  - [`PREVIEW_FEATURES` database scoped configuration](../t-sql/statements/alter-database-scoped-configuration-transact-sql.md#preview-features) option
  - [Time-bound extended event sessions](../relational-databases/extended-events/sql-server-extended-events-sessions.md#time-bound-event-sessions): create an event session that stops automatically after the specified time elapses.
  - CE Feedback for Expressions cache persistence
- [Change event streaming](../relational-databases/track-changes/change-event-streaming/overview.md) is available via [PREVIEW_FEATURES database scoped configuration](../t-sql/statements/alter-database-scoped-configuration-transact-sql.md#preview-features) option. 
- Analytics
  - [PolyBase support for managed identity](../relational-databases/polybase/managed-identity.md) communications to Microsoft Azure Blob Storage and Microsoft Azure Data Lake Storage. 
  - [TDS 8.0 support for PolyBase](../relational-databases/polybase/polybase-guide.md#sql-server-2025-polybase-enhancements) as an external data source.
- Language
   - [JARO_WINKLER_DISTANCE](../t-sql/functions/jaro-winkler-distance-transact-sql.md) returns **float**. Previously **real**.
   - [JARO_WINKLER_SIMILARITY](../t-sql/functions/jaro-winkler-similarity-transact-sql.md) returns **int**. Previously **real**.
   - [REGEXP_REPLACE](../t-sql/functions/regexp-replace-transact-sql.md) supports large object (LOB) types (**varchar(max)** and **nvarchar(max)**).
   - [REGEXP_SUBSTR](../t-sql/functions/regexp-substr-transact-sql.md) supports LOB types (**varchar(max)** and **nvarchar(max)**).
   - [REGEXP_LIKE](../t-sql/functions/regexp-like-transact-sql.md) is *SARGable* <sup>1</sup>.
- SQL Server Agent
   - Default `Encrypt=Mandatory` encryption for [SQL Server Agent](/ssms/agent/sql-server-agent#tds-80-and-strict-encryption-support) with support for [TDS 8.0](../relational-databases/security/networking/tds-8.md) and [TLS 1.3](../relational-databases/security/networking/tls-1-3.md) encryption.
- Availability
   - Configure [TLS 1.3](../relational-databases/security/networking/tls-1-3.md) encryption for communication between the Windows Server Failover Cluster and replicas in an [Always On availability group](../relational-databases/security/networking/connect-with-strict-encryption.md#connect-to-an-always-on-availability-group) or [Always On failover cluster instances (FCI)](../relational-databases/security/networking/connect-with-strict-encryption.md#connect-to-a-failover-cluster-instance) with [TDS 8.0](../relational-databases/security/networking/tds-8.md) support.
- Linked servers
   - Default `Encrypt=Mandatory` encryption for [linked servers](../relational-databases/linked-servers/linked-servers-database-engine.md#related-content) with [TDS 8.0](../relational-databases/security/networking/tds-8.md) and [TLS 1.3](../relational-databases/security/networking/tls-1-3.md) support. The `Encrypt` parameter is now required and must be used when connecting through a linked server to an instance of [!INCLUDE [sssql25-md](../includes/sssql25-md.md)].
- Replication
   - Configure [TLS 1.3](../relational-databases/security/networking/tls-1-3.md) encryption for [Transactional](../relational-databases/replication/transactional/transactional-replication.md#configure-tls-13-encryption), [Merge](../relational-databases/replication/merge/merge-replication.md#configure-tls-13-encryption), [Peer-to-peer](../relational-databases/replication/transactional/peer-to-peer-transactional-replication.md#configure-tls-13-encryption) and [Snapshot](../relational-databases/replication/snapshot-replication.md#configure-tls-13-encryption) with [TDS 8.0](../relational-databases/security/networking/tds-8.md) replication support. 
   - Default `Encrypt=Mandatory` encryption for inter-instance linked server communication between [!INCLUDE [sssql25-md](../includes/sssql25-md.md)] instances in a replication topology. 
- Log shipping
   - Configure [TLS 1.3](../relational-databases/security/networking/tls-1-3.md) encryption between servers in a [log shipping topology](../database-engine/log-shipping/about-log-shipping-sql-server.md#enforce-tls-13-encryption) with [TDS 8.0](../relational-databases/security/networking/tds-8.md) support.
- Fabric
   - See [Fabric](#microsoft-fabric).
- [!INCLUDE [ssrs-power-bi-consolidation](../reporting-services/includes/ssrs-power-bi-consolidation.md)]


<sup>1</sup> [!INCLUDE [search-argument](../includes/paragraph-content/search-argument.md)]

## Feature highlights

The following sections identify features that are improved or introduced in [!INCLUDE [sssql22-md](../includes/sssql25-md.md)].

- [AI](#ai)
- [Developer](#developer)
- [Analytics](#analytics)
- [Availability](#availability-and-disaster-recovery)
- [Security](#security)
- [Database engine](#database-engine)
- [Query Store and intelligent query processing](#query-store-and-intelligent-query-processing)
- [Language](#language)
- [Tools](#tools)
- [Microsoft Fabric](#microsoft-fabric)

## AI

| New feature or update | Details |
| --- | --- |
| [Copilot in SQL Server Management Studio](/ssms/copilot/copilot-in-ssms-overview) | Ask questions. Get answers from your data. |
| [Vector data type](../t-sql/data-types/vector-data-type.md) | Store vector data optimized for operations such as similarity search and machine learning applications. Vectors are stored in an optimized binary format but are exposed as JSON arrays for convenience. Each element of the vector is stored as a single-precision (4-byte) floating-point value. |
| [Vector functions](../t-sql/functions/vector-functions-transact-sql.md) | New scalar functions perform operations on vectors in binary format, allowing applications to store and manipulate vectors in the SQL Database Engine. |
| [Vector index](../relational-databases/vectors/vectors-sql-server.md#vector-search) | Create and manage approximate vector indexes to quickly and efficiently find similar vectors to a given reference vector.<br /><br />Query vector indexes from [sys.vector_indexes](../relational-databases/system-catalog-views/sys-vector-indexes-transact-sql.md). |
| [Manage external AI models](../t-sql/statements/create-external-model-transact-sql.md) | Manage external AI model objects for embedding tasks (creating vector arrays) accessing REST AI inference endpoints. |

## Developer

| New feature or update | Details |
| --- | --- |
| [Change event streaming](../relational-databases/track-changes/change-event-streaming/overview.md) | Capture and publish incremental DML changes of data (such as updates, inserts, and deletes) in near real-time. Change event streaming sends details of data changes such as the schema, previous values, and new values to Azure Event Hubs in a simple *CloudEvent*, serialized as either native JSON or Avro Binary. |
| [Fuzzy string matching](../relational-databases/fuzzy-string-match/overview.md) | Check if two strings are similar, and calculate the difference between two strings. |
| [Regular expressions](../relational-databases/regular-expressions/overview.md) | Define a search pattern for text with a sequence of characters. Query SQL Server with regex to find, replace, or validate text data. |
| [Regular expressions functions](../t-sql/functions/regular-expressions-functions-transact-sql.md) | Match complex patterns and manipulate data in SQL Server with regular expressions. |
| [External REST endpoint invocation](../relational-databases/system-stored-procedures/sp-invoke-external-rest-endpoint-transact-sql.md) | Call REST/GraphQL endpoints from other Azure services from the SQL Database. With a quick call to the system stored procedure [sp_invoke_external_rest_endpoint](../relational-databases/system-stored-procedures/sp-invoke-external-rest-endpoint-transact-sql.md), you can:<br /><br />- Have data processed via an Azure Function<br />- Update a Power BI dashboard<br />- Call a local, in-house enterprise REST endpoint<br />- Talk to Azure OpenAI Services |
| [JSON data in SQL Server](../relational-databases/json/json-data-sql-server.md#sql-server-2025-changes) | Use SQL Server built-in functions and operators to:<br /><br />- Parse JSON text and read or modify values.<br />- Transform arrays of JSON objects into table format.<br />- Run any Transact-SQL query on the converted JSON objects.<br />- Format the results of Transact-SQL queries in JSON format.<br />- Review examples at: [JSON data type](../t-sql/data-types/json-data-type.md): Store JSON in a native binary format. |
| Batch mode optimizations for built-in functions | Performance improvements for the following built-in functions:<br /><br />- [Mathematical functions](../t-sql/functions/mathematical-functions-transact-sql.md)<br />- [DATETRUNC](../t-sql/functions/datetrunc-transact-sql.md) |
| New Chinese collations | Version 160 to support GB18030-2022 standard. |

## New developer editions

> [!NOTE]  
> Full edition and feature support for SQL Server 2025 isn't completely documented until the product is generally available (GA). The features and editions described in this article are subject to change until GA.

The following free editions are designed to provide all the features of their corresponding paid editions. They can be used to develop SQL Server applications without requiring a paid license.

For features by edition, review [Editions and supported features of SQL Server 2025 Preview](editions-and-components-of-sql-server-2025.md).

The editions and supported features for [!INCLUDE [sssql25-md](../includes/sssql25-md.md)] are subject to change until the product is generally available.

### Standard Developer edition

SQL Server 2025 Standard Developer edition is a free edition licensed for development. It includes all features of SQL Server Standard edition.

- Develop new applications for Standard edition.
- Set up a staging environment to certify the upgrade of an existing application from the Standard edition to SQL Server 2025 Standard edition before deploying it in production.

### Enterprise Developer edition

SQL Server 2025 Enterprise Developer edition includes SQL Server Enterprise edition features.

- Develop new applications for Enterprise edition.

Functionally equivalent to Developer edition in previous versions.

## Analytics

| New feature or update | Details |
| --- | --- |
| [Connect to ODBC data sources with PolyBase on SQL Server on Linux](../linux/sql-server-linux-polybase.md) | Supports ODBC data sources for SQL Server on Linux. |
| [Native support for specific source types](../relational-databases/polybase/polybase-guide.md#sql-server-2025-polybase-enhancements) | PolyBase services no longer required for parquet, Delta, or CSV. |
| [TDS 8.0 support for PolyBase](../relational-databases/polybase/polybase-guide.md#sql-server-2025-polybase-enhancements) | When you use [Features of the Microsoft ODBC Driver for SQL Server on Windows](../connect/odbc/windows/features-of-the-microsoft-odbc-driver-for-sql-server-on-windows.md) for PolyBase, TDS 8.0 is available for [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] as an [external data source](../t-sql/statements/create-external-data-source-transact-sql.md).|

## Availability and disaster recovery

| New feature or update | Details |
| --- | --- |
| **Always On availability groups** | |
| [Availability group asynchronous page request dispatching improvement](../database-engine/availability-groups/windows/failover-and-failover-modes-always-on-availability-groups.md#asynchronous-page-request-dispatching-improvement) | Perform asynchronous page requests and in batches during failover recovery. Enabled by default. |
| [Allow database to switch to resolving state](../database-engine/availability-groups/windows/failover-and-failover-modes-always-on-availability-groups.md#databases-switch-to-resolving-state-after-a-failure) | After a failure to read the persisted configuration data due to network service interruption. |
| [Configure AG group commit wait in milliseconds](../database-engine/configure-windows/availability-group-commit-time-server-configuration-options.md) | Set `availability group commit time` in milliseconds for an availability group replica so that transactions are sent to the secondary replica faster. |
| [Control communication flow for availability groups](../database-engine/configure-windows/ucs-flow-control-sp-configure.md) | A new `sp_configure` option lets the primary replica determine if the secondary replica falls behind. With the new configuration option, you can optimize communication between HADR endpoints. |
| [Distributed AG support for a contained AG](../database-engine/availability-groups/windows/contained-availability-groups-overview.md#distributed-availability-groups) | Configure a distributed availability group between two contained availability groups. |
| [Distributed AG synchronization improvements](../database-engine/availability-groups/windows/distributed-availability-groups.md#improvement-to-distributed-ag-synchronization) | Improves synchronization performance by reducing network saturation when the global primary and forwarder replicas are in asynchronous commit mode. |
| Fast failover for [persistent AG health issues](../database-engine/availability-groups/windows/failover-and-failover-modes-always-on-availability-groups.md#fast-failover-for-persistent-health-issues) | Set the RestartThreshold for an Always On availability group to 0, which tells the WSFC to fail over the availability group resource immediately when a persistent health issue is detected. |
| [Improved health check timeout diagnostics](../database-engine/availability-groups/windows/availability-group-lease-healthcheck-timeout.md#sql-server-2025-improved-health-check-timeout-diagnostics) | Improves synchronization performance by reducing network saturation when the global primary and forwarder replicas are in asynchronous commit mode. This change is enabled by default and doesn't require any configuration. |
| [REMOVE listener IP address](../t-sql/statements/alter-availability-group-transact-sql.md#remove-ip--four_part_ipv4_address--ipv6_address-) | New parameter in the `ALTER AVAILABILITY GROUP` Transact-SQL command lets you remove an IP address from a listener without deleting the listener. |
| Set `NONE` for [read-only](../t-sql/statements/alter-availability-group-transact-sql.md#read_only_routing_url-tcpsystem-addressport--none) or [read-write](../t-sql/statements/alter-availability-group-transact-sql.md#read_write_routing_url--tcpsystem-addressport--none) routing | When configure `READ_WRITE_ROUTING_URL` and `READ_ONLY_ROUTING_URL`, you can set to `NONE` to revert specified routing by using the `ALTER AVAILABILITY GROUP` Transact-SQL command to automatically route traffic back to the primary replica. |
| [Configure TLS 1.3 encryption with TDS 8.0](../relational-databases/security/networking/connect-with-strict-encryption.md#connect-to-an-always-on-availability-group) | Configure [TLS 1.3](../relational-databases/security/networking/tls-1-3.md) encryption for communication between the Windows Server Failover Cluster and an Always On availability group replica with [TDS 8.0](../relational-databases/security/networking/tds-8.md) support. |
| **Always On failover cluster instance** | |
| [Configure TLS 1.3 encryption with TDS 8.0](../relational-databases/security/networking/connect-with-strict-encryption.md#connect-to-a-failover-cluster-instance) | Configure [TLS 1.3](../relational-databases/security/networking/tls-1-3.md) encryption for communication between the Windows Server Failover Cluster and Always On failover cluster instance (FCI) with [TDS 8.0](../relational-databases/security/networking/tds-8.md) support. |
| **Backups** | |
| [Back up to immutable blob storage](../relational-databases/backup-restore/sql-server-backup-to-url.md#azure-immutable-storage-support) | Available when backing up to URL. |
| [Back up on secondary replicas](../database-engine/availability-groups/windows/active-secondaries-backup-on-secondary-replicas-always-on-availability-groups.md#new-for-sql-server-2025) | In addition to copy-only backups, you can now also perform full and differential backups on any secondary replica. |
| **Log shipping** | |
| [Configure TLS 1.3 encryption with TDS 8.0](../database-engine/log-shipping/about-log-shipping-sql-server.md#enforce-tls-13-encryption) | Configure [TLS 1.3](../relational-databases/security/networking/tls-1-3.md) encryption for communication between servers in a log shipping topology. |

## Security

| New feature or update | Details |
| --- | --- |
| [Security cache improvements](../relational-databases/security-cache.md) | Invalidates caches for only a specific login. When security cache entries are invalidated, only those entries belonging to the affected login are affected. This improvement minimizes the impact of non-cache permissions validation for unaffected login users. |
| [OAEP padding mode support for RSA encryption](../relational-databases/security/encryption/encryption-hierarchy.md) | Support for certificates and asymmetric keys, adding security layers to encryption and decryption processes. |
| [PBKDF for password hashes on by default](../t-sql/statements/create-application-role-transact-sql.md#remarks) | Uses **PBKDF2** for password hashes by default, enhancing password security and helping customers comply with NIST SP 800-63b. |
| [Managed identity with Microsoft Entra authentication](azure-arc/managed-identity.md) | Can use the Arc-enabled server managed identity in outbound connections to communicate with Azure resources, and inbound connections for external users to connect to SQL Server. Requires SQL Server enabled by Azure Arc. |
| [Back up to/restore from URL with managed identity](azure-arc/backup-to-url.md) | Back up to, or restore from, URL with a managed identity. Requires SQL Server enabled by Azure Arc. |
| [Managed Identity support for Extensible Key Management with Azure Key Vault](azure-arc/managed-identity-extensible-key-management.md) | Supported for EKM with AKV and Managed Hardware Security Modules (HSM). Requires SQL Server enabled by Azure Arc. |
| [Create Microsoft Entra logins and users with nonunique display names](/azure/azure-sql/database/authentication-microsoft-entra-create-users-with-nonunique-names) | Support for the T-SQL syntax `WITH OBJECT_ID` when using the [CREATE LOGIN](../t-sql/statements/create-login-transact-sql.md?view=sql-server-ver17&preserve-view=true#with-object_id--objectid) or [CREATE USER](../t-sql/statements/create-user-transact-sql.md#with-object_id--objectid) statement. |
| [Support custom password policy on Linux](../linux/sql-server-linux-custom-password-policy.md) | Enforce a custom password policy for SQL authentication logins on SQL Server on Linux. |
| [Configure TLS 1.3 encryption with TDS 8.0 support](../relational-databases/security/networking/tds-8.md#sql-server-2025-support) | TLS 1.3 encryption added with TDS 8 for the following features: <br />- [SQL Server Agent](/ssms/agent/sql-server-agent#tds-80-and-strict-encryption-support)<br />- [sqlcmd utility](../tools/sqlcmd/sqlcmd-utility.md#tds-80-support)<br />- [bcp utility](../tools/bcp-utility.md#tds-80-support)<br />- [SQL Writer service](../database-engine/configure-windows/sql-writer-service.md)<br />- [Configure usage and diagnostic data collection for SQL Server (CEIP)](usage-and-diagnostic-data-configuration-for-sql-server.md)<br />- [Data virtualization with PolyBase in SQL Server](../relational-databases/polybase/polybase-guide.md#sql-server-2025-polybase-enhancements)<br />- [Always On availability groups](../relational-databases/security/networking/connect-with-strict-encryption.md#connect-to-an-always-on-availability-group)<br />- [Always On failover cluster instances (FCI)](../relational-databases/security/networking/connect-with-strict-encryption.md#connect-to-a-failover-cluster-instance)<br />- [Linked servers](../relational-databases/linked-servers/linked-servers-database-engine.md#related-content)<br />- [Transactional replication](../relational-databases/replication/transactional/transactional-replication.md#configure-tls-13-encryption)<br />- [Merge replication](../relational-databases/replication/merge/merge-replication.md#configure-tls-13-encryption)<br />- [Peer-to-peer](../relational-databases/replication/transactional/peer-to-peer-transactional-replication.md#configure-tls-13-encryption) <br />- [Snapshot replication](../relational-databases/replication/snapshot-replication.md#configure-tls-13-encryption)<br />- [Log shipping](../database-engine/log-shipping/about-log-shipping-sql-server.md#enforce-tls-13-encryption)<br /><br />Review [Breaking changes](../database-engine/breaking-changes-to-database-engine-features-in-sql-server-2025.md). |

## Database Engine

| New feature or update | Details |
| --- | --- |
| [Optimized locking](../relational-databases/performance/optimized-locking.md) | Reduces blocking and lock memory consumption, and avoids lock escalation. |
| [`tempdb` space resource governance](../relational-databases/resource-governor/tempdb-space-resource-governance.md) | Improves reliability and avoids outages by preventing runaway workloads from consuming a large amount of space in `tempdb`. Supports [percent-based limits](../relational-databases/resource-governor/tempdb-space-resource-governance.md#set-limits-on-tempdb-space-consumption). |
| [Accelerated database recovery in tempdb](../relational-databases/accelerated-database-recovery-concepts.md#adr-improvements-in-sql-server-2025) | Provides the benefits of accelerated database recovery for transactions in the `tempdb` database, such as transactions that use temporary tables. |
| [Persisted statistics for readable secondaries](../relational-databases/performance/persisted-stats-secondary-replicas.md) | Creates persisted statistics on readable secondaries so that workloads that run against secondary replicas are optimized. |
| [Change tracking improvements](../relational-databases/track-changes/about-change-tracking-sql-server.md#sql-server-2025-changes) | Adaptive shallow cleanup improves change tracking auto cleanup performance. |
| [Columnstore improvements](../relational-databases/indexes/columnstore-indexes-what-s-new.md#sql-server-2025-17x) | Multiple improvements in columnstore indexes:<br />- Ordered nonclustered columnstore indexes<br />- Online index build and improved sort quality for ordered columnstore indexes<br />- Improved shrink operations when clustered columnstore indexes are present |
| [Memory-optimized container and filegroup removal](../relational-databases/in-memory-oltp/memory-optimized-container-filegroup-removal.md) | Supports removal of memory-optimized containers and filegroups when all In-Memory OLTP objects are deleted. |
| [tmpfs support for tempdb on Linux](../linux/sql-server-linux-tmpfs-tempdb.md) | Enable and run `tempdb` on **tmpfs** for SQL Server on Linux. |
| [ZSTD Backup compression algorithm](../relational-databases/backup-restore/backup-compression-sql-server.md#zstd-compression-algorithm-introduced-in-sql-server-2025) | [!INCLUDE [sssql25-md](../includes/sssql25-md.md)] adds a faster and more effective backup compression algorithm - ZSTD. |
| Optimized [sp_executesql](../relational-databases/system-stored-procedures/sp-executesql-transact-sql.md?view=sql-server-ver17&preserve-view=true#optimized_sp_executesql) | Effectively reduce the impact of compilation storms. A compilation storms refers to a situation where a large number of queries are being compiled simultaneously, leading to performance issues and resource contention. Enable this feature to allow invocations of `sp_executesql` to behave like objects such as stored procedures and triggers from a compilation perspective.<br /><br />Allowing batches which use `sp_executesql` to serialize the compilation process reduces the impact of compilation storms. |
| [Time-bound extended event sessions](../relational-databases/extended-events/sql-server-extended-events-sessions.md#time-bound-event-sessions) | Automatically stops an extended events session after a time limit elapses. This helps avoid situations where sessions might be left running indefinitely by mistake, consuming resources and potentially generating a large amount of data. |

## Query Store and intelligent query processing

The [intelligent query processing (IQP)](../relational-databases/performance/intelligent-query-processing.md) feature family includes features that improve the performance of existing workloads with minimal implementation effort.

:::image type="content" source="media/what-s-new-in-sql-server-2025/intelligent-query-processing-features.png" alt-text="Screenshot of Chart representing the features in the intelligent query processing family." lightbox="media/what-s-new-in-sql-server-2025/intelligent-query-processing-features.png":::

| New feature or update | Details |
| --- | --- |
| [Cardinality estimation feedback for expressions](../relational-databases/performance/intelligent-query-processing-ce-feedback-for-expressions.md) | Learns from previous executions of expressions across queries in order to find appropriate CE model choices and apply what has been learned to future executions of those expressions. |
| [Optional parameter plan optimization (OPPO)](../relational-databases/performance/optional-parameter-optimization.md) | Leverages the adaptive plan optimization (Multiplan) infrastructure that was introduced with the Parameter Sensitive Plan Optimization (PSPO) improvement, which generates multiple plans from a single statement. This allows the feature to make different assumptions depending on the parameter values used in the query. |
| Degree of parallelism (DOP) feedback | Now on by default. |
| [Query Store for readable secondaries](../relational-databases/performance/query-store-for-secondary-replicas.md) | Now on by default. |
| [ABORT_QUERY_EXECUTION query hint](../relational-databases/performance/query-store-hints-best-practices.md#block-future-execution-of-problematic-queries) | Blocks future execution of known problematic queries, for example nonessential queries affecting application workloads. |

## Language

| New feature or update | Details |
| --- | --- |
| **Artificial intelligence** | |
| [VECTOR_DISTANCE](../t-sql/functions/vector-distance-transact-sql.md) | Calculates the distance between two vectors using a specified distance metric. |
| [VECTOR_NORM](../t-sql/functions/vector-norm-transact-sql.md) | Returns the norm of the vector (which is a measure of its length or magnitude). |
| [VECTOR_NORMALIZE](../t-sql/functions/vector-normalize-transact-sql.md) | Returns a normalized vector. |
| [VECTORPROPERTY](../t-sql/functions/vectorproperty-transact-sql.md) | Returns specific properties of a given vector. |
| [CREATE EXTERNAL MODEL](../t-sql/statements/create-external-model-transact-sql.md) | Creates an external model object that contains the location, authentication method, and purpose of an AI model inference endpoint. |
| [ALTER EXTERNAL MODEL](../t-sql/statements/alter-external-model-transact-sql.md) | Alters an external model object. |
| [DROP EXTERNAL MODEL](../t-sql/statements/drop-external-model-transact-sql.md) | Drops an external model object. |
| [AI_GENERATE_CHUNKS](../t-sql/functions/ai-generate-chunks-transact-sql.md) | Creates *chunks*, or fragments of text based on a type, size, and source expression. |
| [AI_GENERATE_EMBEDDINGS](../t-sql/functions/ai-generate-embeddings-transact-sql.md) | Creates embeddings (vector arrays) using a precreated AI model definition stored in the database. |
| **Regular expressions** | |
| [REGEXP_LIKE](../t-sql/functions/regexp-like-transact-sql.md) | Indicates if the regular expression pattern matches in a string. |
| [REGEXP_REPLACE](../t-sql/functions/regexp-replace-transact-sql.md) | Returns a modified source string replaced by a replacement string, where the occurrence of the regular expression pattern found. If no matches are found, the function returns the original string. |
| [REGEXP_SUBSTR](../t-sql/functions/regexp-substr-transact-sql.md) | Returns one occurrence of a substring of a string that matches the regular expression pattern. If no match is found, it returns `NULL`. |
| [REGEXP_INSTR](../t-sql/functions/regexp-instr-transact-sql.md) | Returns the starting or ending position of the matched substring, depending on the value of the `return_option` argument. |
| [REGEXP_COUNT](../t-sql/functions/regexp-count-transact-sql.md) | Counts the number of times that a regular expression pattern is matched in a string. |
| [REGEXP_MATCHES](../t-sql/functions/regexp-matches-transact-sql.md) | Returns tabular results captured substrings resulting from matching a regular expression pattern to a string. If no match is found, the function returns no row. |
| [REGEXP_SPLIT_TO_TABLE](../t-sql/functions/regexp-split-to-table-transact-sql.md) | Returns strings split, delimited by the regex pattern. If there's no match to the pattern, the function returns the whole string expression. |
| **JSON** | |
| [JSON_OBJECTAGG](../t-sql/functions/json-objectagg-transact-sql.md) | Construct a JSON object from an aggregation. |
| [JSON_ARRAYAGG](../t-sql/functions/json-arrayagg-transact-sql.md) | Construct a JSON array from an aggregation. |
| **Other additions and improvements** | |
| [SUBSTRING](../t-sql/functions/substring-transact-sql.md) | *length* is now optional, defaults to *expression* length. This change aligns the function with ANSI standard. |
| [DATEADD](../t-sql/functions/dateadd-transact-sql.md) | *number* supports **bigint** type. |
| [UNISTR](../t-sql/functions/unistr-transact-sql.md) | Specify Unicode encoding values. Returns Unicode characters. |
| [PRODUCT](../t-sql/functions/product-aggregate-transact-sql.md) | The `PRODUCT()` aggregate function calculates the product of a set of values. |
| [CURRENT_DATE](../t-sql/functions/current-date-transact-sql.md) | Returns the current database system date as a date value. |
| [EDIT_DISTANCE](../t-sql/functions/edit-distance-transact-sql.md) | Calculates the number of insertions, deletions, substitutions, and transpositions needed to transform one string to another. |
| [EDIT_DISTANCE_SIMILARITY](../t-sql/functions/edit-distance-similarity-transact-sql.md) | Calculates a similarity value ranging from 0 (indicating no match) to 100 (indicating full match). |
| [JARO_WINKLER_DISTANCE](../t-sql/functions/jaro-winkler-distance-transact-sql.md) | Calculates the edit distance between two strings giving preference to strings that match from the beginning for a set prefix length. |
| [JARO_WINKLER_SIMILARITY](../t-sql/functions/jaro-winkler-similarity-transact-sql.md) | Calculates a similarity value ranging from 0 (indicating no match) to 100 (indicating full match). |
| - [BASE64_ENCODE](../t-sql/functions/base64-encode-transact-sql.md)<br />- [BASE64_DECODE](../t-sql/functions/base64-decode-transact-sql.md) | Convert binary data into a text format that's safe for transmission across various systems. It can be used in diverse ways as it ensures that your binary data, such as images or files, remains intact during transfer, even when passing through text-only systems. |
| [&#124;&#124; (String concatenation)](../t-sql/language-elements/string-concatenation-pipes-transact-sql.md) | Concatenate expressions with `expression || expression`. |

## Tools

| New feature or update | Details |
| --- | --- |
| **[bcp utility](../tools/bcp-utility.md)** | Authentication enhancements |
| **[sqlcmd utility](../tools/sqlcmd/sqlcmd-utility.md)** | Authentication enhancements |

## Microsoft Fabric

| New feature or update | Details |
| --- | --- |
| [Mirroring in Fabric](/fabric/database/mirrored-database/overview) | Continuously replicate data to Microsoft Fabric from SQL Server 2025 on-premises. Microsoft Fabric already includes mirroring from a variety of sources, including Azure SQL Database and Azure SQL Managed Instance. For more information on SQL Server 2025 database mirroring to Fabric, see [Mirrored SQL Server databases in Microsoft Fabric](/fabric/database/mirrored-database/sql-server). |

### Fabric mirroring for SQL Server 2025 RC0 (Preview)

- You can configure SQL Server resource governor to manage resource usage for Mirroring in Fabric for [!INCLUDE [sssql25-md](../includes/sssql25-md.md)]. Each workload group is for a specific phase of mirroring.

   For an example and to get started, see [Configure resource governor for Fabric mirroring](/fabric/database/mirrored-database/sql-server-performance#resource-governor-for-sql-server-mirroring). For more information, see [Resource governor workload group](../relational-databases/resource-governor/resource-governor-workload-group.md). 

- You can enable and configure the autoreseed feature for Fabric mirroring to prevent the transaction log from filling in [!INCLUDE [sssql25-md](../includes/sssql25-md.md)].

   For an example and to get started, see [Configure automatic reseed](/fabric/database/mirrored-database/sql-server-configure-automatic-reseed).

- You can configure a maximum and lower bound of transactions to be processed by Fabric Mirroring in [!INCLUDE [sssql25-md](../includes/sssql25-md.md)].

   For an example and to get started, see [Configure control scan performance](/fabric/database/mirrored-database/sql-server-performance#control-scan-performance).

## SQL Server Analysis Services

Installing SQL Server Analysis Services in CTP 2.0 to run using a local account can fail. Use a domain account for testing Analysis Services in CTP 2.0 instead.

The error you see in the Windows Event Viewer is:

```output
Server Gen2 cryptokey is not present, but server assembly object System is set to use server gen2 cryptokey. Terminating server.
```

For specific updates, see [What's new in SQL Server Analysis Services](/analysis-services/what-s-new-in-sql-server-analysis-services).

## Power BI Report Server

[!INCLUDE [ssrs-power-bi-consolidation](../reporting-services/includes/ssrs-power-bi-consolidation.md)]

## SQL Server Integration Services

For changes related to SQL Server Integration Services, see [What's New in SQL Server 2025 Integration Services Preview](../integration-services/what-s-new-in-integration-services-in-sql-server-2025.md).

## Discontinued services and deprecated features

**Data Quality Services** (DQS) is discontinued in this version of [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)]. We continue to support DQS in [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] and earlier versions.

**Master Data Services** (MDS) is discontinued in this version of [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)]. We continue to support MDS in [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] and earlier versions.

**Synapse Link** is discontinued in this version of [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)]. Use [Mirroring in Fabric](/fabric/database/mirrored-database/overview) instead. For more information, see [Mirroring in Fabric – What's new](https://aka.ms/IntroMirroringSQL).

The [Hot add CPU](../relational-databases/thread-and-task-architecture-guide.md#hot-add-cpu) feature is deprecated in this version of [!INCLUDE [ssNoVersion](../includes/ssnoversion-md.md)], and is planned to be removed in a future version.

**Purview access policies** (DevOps policies and data owner policies) are discontinued in this version of SQL Server. Use [Fixed server roles](../relational-databases/security/authentication-access/server-level-roles.md#fixed-server-level-roles-introduced-in-sql-server-2022) instead.

- In place of the **SQL Performance Monitoring** Purview policy action, use the `##MS_ServerPerformanceStateReader##` and/or `##MS_PerformanceDefinitionReader##` fixed server roles.

- In place of **SQL Security Auditing** Purview policy action, use the `##MS_ServerSecurityStateReader##` and/or `##MS_SecurityDefinitionReader##` fixed server roles.

- Use the `##MS_DatabaseConnector##` server role with existing logins, to connect to a database without the need to create a user in that database.

## Other services

None at this time.

## Breaking changes

[!INCLUDE [sssql25-md](../includes/sssql25-md.md)] introduces breaking changes to a few SQL Server Database Engine features. For more information, see [Breaking changes in SQL Server 2025 preview](../database-engine/breaking-changes-to-database-engine-features-in-sql-server-2025.md).

## Related content

- [SqlServer PowerShell module](https://www.powershellgallery.com/packages/Sqlserver)
- [SQL Server PowerShell](/powershell/sql-server/sql-server-powershell)
- [SQL Server Workshops](https://aka.ms/sqlworkshops)
- [SQL Server 2025 Preview release notes](sql-server-2025-release-notes.md)

[!INCLUDE [get-help-options](../includes/paragraph-content/get-help-options.md)]
