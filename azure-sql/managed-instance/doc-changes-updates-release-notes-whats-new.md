---
title: What's new?
titleSuffix: Azure SQL Managed Instance
description: Learn about the new features and documentation improvements for Azure SQL Managed Instance.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: wiassaf, randolphwest
ms.date: 03/02/2026
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
|[Database watcher for Azure SQL](../database-watcher-overview.md) | Database watcher is a managed monitoring solution for database services in the Azure SQL family. Database watcher collects in-depth workload monitoring data to give you a detailed view of database performance, configuration, and health. Learn more about [database watcher](https://aka.ms/dbwatcher-preview-announcement).|
|[DATEADD number allows bigint](/sql/t-sql/functions/dateadd-transact-sql) | For `DATEADD (datepart, number, date)`, number can be expressed as a **bigint**.|
|[Endpoint policies](./service-endpoint-policies-configure.md) | Configure which Azure Storage accounts can be accessed from a SQL Managed Instance subnet. Grants an extra layer of protection against inadvertent or malicious data exfiltration.|
|[Modernization Advisor](../virtual-machines/modernization-advisor.md) | Use the Modernization Advisor in the Azure portal to help you determine if migrating to Azure SQL Managed Instance from a SQL Server VM saves you money or optimizes performance. |
|[SDK-style SQL project](/sql/tools/sql-database-projects/sql-database-projects) | Use [Microsoft.Build.Sql](https://www.nuget.org/packages/Microsoft.Build.Sql) for SDK-style SQL projects in the SQL Database Projects extension in Visual Studio Code. SDK-style SQL projects are especially advantageous for applications shipped through pipelines or built in cross-platform environments.|
|[Service Broker](/sql/database-engine/configure-windows/sql-server-service-broker) | Support for cross-instance message exchange using Service Broker between instances of Azure SQL Managed Instance, and between SQL Server and Azure SQL Managed Instance. |
|[Vector data type and functions](/sql/t-sql/data-types/vector-data-type?view=azuresqlmi-current&preserve-view=true) | Working with vector data is now easier in Azure SQL Managed Instance with the introduction of a new [vector data type](/sql/t-sql/data-types/vector-data-type?view=azuresqlmi-current&preserve-view=true) and [vector functions](/sql/t-sql/functions/vector-functions-transact-sql?view=azuresqlmi-current&preserve-view=true). For more information, see [Intelligent applications with Azure SQL Managed Instance](ai-artificial-intelligence-intelligent-applications.md#vectors). |


## General availability (GA)

The following table lists features of Azure SQL Managed Instance that have been made generally available (GA) within the last 12 months:

| Feature | GA Month | Details |
| ---| --- |--- |
|[SQL Server 2025 update policy](update-policy.md#sql-server-2025-update-policy) | March 2026 | Align your SQL managed instance database format with the SQL Server 2025 database engine. | 
|[Regular expression functions](/sql/relational-databases/regular-expressions/overview) | November 2025 | Regular expression (REGEX) functions return text based on values in a search pattern. |
|[Flexible memory](resource-limits.md#flexible-memory) | November 2025 | Save on cost by choosing the memory allocation for your [Next-gen General Purpose](service-tiers-next-gen-general-purpose-use.md) instance based on your workload needs.|
|[Next-gen General Purpose](service-tiers-next-gen-general-purpose-use.md) | November 2025 | An architectural upgrade of the General Purpose service tier that uses [Elastic SAN storage](/azure/storage/elastic-san/elastic-san-introduction) for greater resource flexibility, and improved performance while maintaining the same baseline cost as the General Purpose service tier.  |
|[Migrate SQL Server to Azure](/sql/sql-server/azure-arc/migrate-to-azure-sql-managed-instance) | October 2025 | Migrate your SQL Server enabled by Azure Arc instance to Azure SQL Managed Instance through the Azure portal.|
|[Optimized locking](/sql/relational-databases/performance/optimized-locking)| July 2025 | Azure SQL Managed Instance with the **Always-up-to-date** and **SQL Server 2025** [update policy](update-policy.md) now has optimized locking enabled for all user databases. |
|[UNISTR (Transact-SQL)](/sql/t-sql/functions/unistr-transact-sql) | July 2025 | Azure SQL Managed Instance now supports the `UNISTR` T-SQL syntax for Unicode string literals.|
|[\|\| (String concatenation)](/sql/t-sql/language-elements/string-concatenation-pipes-transact-sql?view=azuresqlmi-current&preserve-view=true) and [\|\|= (Compound assignment)](/sql/t-sql/language-elements/compound-assignment-pipes-transact-sql?view=azuresqlmi-current&preserve-view=true) syntax support | July 2025 |Azure SQL Managed Instance now supports [\|\| (String concatenation)](/sql/t-sql/language-elements/string-concatenation-pipes-transact-sql?view=azuresqlmi-current&preserve-view=true) and [\|\|= (Compound assignment)](/sql/t-sql/language-elements/compound-assignment-pipes-transact-sql?view=azuresqlmi-current&preserve-view=true) Transact-SQL syntax.|
|[Degrees of parallelism (DOP) feedback](/sql/relational-databases/performance/intelligent-query-processing-degree-parallelism-feedback?view=azuresqldb-mi-current&preserve-view=true) | July 2025|  DOP feedback improves query performance by identifying parallelism inefficiencies for repeating queries, based on elapsed time and waits. For more information, see the [Smarter Parallelism: Degree of parallelism feedback in SQL Server 2025](https://techcommunity.microsoft.com/blog/sqlserver/smarter-parallelism-degree-of-parallelism-feedback-in-sql-server-2025/4431318) blog. |
| Vector data type and functions | June 2025 | Working with vector data is now easier in Azure SQL Managed Instance with the Always-up-to-date policy, with the introduction of a new [vector data type](/sql/t-sql/data-types/vector-data-type?view=azuresqlmi-current&preserve-view=true) and [vector functions](/sql/t-sql/functions/vector-functions-transact-sql?view=azuresqlmi-current&preserve-view=true). For more information, see [Intelligent applications with Azure SQL Database](ai-artificial-intelligence-intelligent-applications.md#vectors).|
|[Zone redundancy for General Purpose](high-availability-sla-local-zone-redundancy.md#zone-redundant-availability) | June 2025|  Deploy your General Purpose SQL Managed Instance to multiple availability zones to improve the availability of your instance in the event of a disaster. | 
|[Invoke an HTTPS REST endpoint SP](/sql/relational-databases/system-stored-procedures/sp-invoke-external-rest-endpoint-transact-sql) | June 2025 | Use the `sp_invoke_external_rest_endpoint` stored procedure to invoke an HTTPS REST endpoint provided as an input argument to the procedure. | 
|[TLS 1.3 support for replication](replication-transactional-overview.md#tls-13-support) | May 2025 | Configure Azure SQL Managed Instance replication agents to use TLS 1.3. |
|[Free SQL Managed Instance](free-offer.md) | May 2025 | Try Azure SQL Managed Instance for free for the first 12 months after an instance is created.  |
|[JSON native data type](/sql/t-sql/data-types/json-data-type?view=azuresqlmi-current&preserve-view=true) | May 2025 | The **json** data type provides new capabilities for handling semistructured data in Azure SQL Managed Instance. |
|[JSON aggregate functions](/sql/relational-databases/json/json-data-sql-server?view=azuresqlmi-current&preserve-view=true#json-data-from-aggregates) | May 2025 | Two **json** aggregate functions (`JSON_OBJECTAGG` and `JSON_ARRAYAGG`) enable construction of JSON objects or arrays based on an aggregate from SQL data. |
|[MI link from SQL Server 2017](managed-instance-link-feature-overview.md#prerequisites) | March 2025 | Configure a link from SQL Server 2017 to Azure SQL Managed Instance. |


## Documentation changes

Learn about significant changes to the Azure SQL Managed Instance documentation. For previous years, see the [What's new archive](doc-changes-updates-release-notes-whats-new-archive.md).

### March 2026

| Changes | Details |
| --- | --- |
| **SQL Server 2025 update policy GA** | Align your SQL managed instance database format with the SQL Server 2025 database engine. This update policy is now generally available. For more information, review [SQL Server 2025 update policy](update-policy.md#sql-server-2025-update-policy). |

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
