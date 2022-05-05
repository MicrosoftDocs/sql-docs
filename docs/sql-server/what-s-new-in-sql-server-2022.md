---
title: "What's new in SQL Server 2022 | Microsoft Docs"
description: Learn about new features for SQL Server 2022 (16.x), which gives you choices of development languages, data types, environments, and operating systems.
ms.date: 05/04/2022
ms.prod: sql
ms.reviewer: ""
ms.technology: release-landing
ms.topic: "article"
author: MikeRayMSFT
ms.author: mikeray
monikerRange: ">=sql-server-ver15"
ms.custom:
  - intro-whats-new
---

# What's new in [!INCLUDE[sql-server-2022](../includes/sssql22-md.md)]

[!INCLUDE [sqlserver2022](../includes/applies-to-version/sqlserver2022.md)]

[!INCLUDE[sql-server-2022](../includes/sssql22-md.md)] builds on previous releases to grow SQL Server as a platform that gives you choices of development languages, data types, on-premises or cloud environments, and operating systems.

This article summarizes the new features and enhancements for [!INCLUDE[sql-server-2022](../includes/sssql22-md.md)].

For more information and known issues, see [[!INCLUDE[sql-server-2022](../includes/sssql22-md.md)] release notes](sql-server-2022-release-notes.md).

For the best experience with [!INCLUDE[sql-server-2022](../includes/sssql22-md.md)], use the [latest tools](../azure-data-studio/download-azure-data-studio.md).

The following sections provide an overview of these features.

## Azure Synapse Link support

Azure Synapse Link for SQL enables near real-time analytics over operational data in [!INCLUDE[sql-server-2022](../includes/sssql22-md.md)] and [!INCLUDE[ssSDSfull](../includes/sssdsfull-md.md)]. With a seamless integration between operational stores and Azure Synapse Analytics dedicated SQL pools, Azure Synapse Link enables you to run analytics, business intelligence and machine learning scenarios on your operational data with minimum impact on source databases with a new change feed technology. For more information, see [What is Synapse Link for SQL? (Preview)](/azure/synapse-analytics/synapse-link/sql-synapse-link-overview).

## Azure Purview integration

* Allow Purview policies to be applied to any SQL Server instance that is enrolled in both Azure Arc and to Azure Purview data governance

* Support for three roles:
  * SQL Performance Monitor
  * SQL Security Auditor
  * SQL Data Reader
* Distributed Availability

## Operations

* Accelerated database recovery performance improvement
  * Implements a persistent version store cleaner thread per database instead of per instance. Improves version cleanup when there are multiple databases on the same instance.

## Performance optimization

* Improvements to string processing for columnstore indexes
* In-Memory OLTP memory management improvements
  * Improve memory management in large memory servers to reduce out of memory conditions
  * Add a new stored procedure to manually release unused memory on demand

* Concurrent global allocation map (GAM) and shared global allocation map (SGAM) updates allows multiple threads updating GAM and SGAM pages under S latch.
* Degree of parallelism (DOP) feedback automatically adjusts degree of parallelism for repeating queries to optimize for workloads where excessive parallelism can cause performance issues. Similar to optimizations in Azure SQL Database. See [Configure the max degree of parallelism (MAXDOP) in Azure SQL Database](/azure/azure-sql/database/configure-max-degree-of-parallelism).
* Multiple TCP connections for distributed availability groups
  * Enables the use of multiple TCP connections for better network bandwidth utilization across a remote link with long tcp latencies.
* Optimized plan forcing using compilation replay
  * This feature improves the compilation time for forced plan generation by pre-caching non-repeatable plan compilation steps
* Data Virtualization - ODBC driver refresh


## Language improvements

### Approximate percentile

* Approximate Percentile - There are two new approximate percentile functions introduced.
  * [APPROX_PERCENTILE_CONT (Transact-SQL)](../t-sql/functions/approx-percentile-cont-transact-sql.md)
  * [APPROX_PERCENTILE_DISC (Transact-SQL)](../t-sql/functions/approx-percentile-disc-transact-sql.md)

### Time series functions

* [DATE_BUCKET](/azure/azure-sql-edge/date-bucket-tsql)
* [FIRST_VALUE](../t-sql/functions/first-value-transact-sql.md)
* GENERATE_SERIES
* [LAST_VALUE](../t-sql/functions/last-value-transact-sql.md)


## Statistics 
* Statistics [AUTO_DROP option](../relational-databases/statistics/statistics.md#auto_drop-option)

## Business continuity and disaster recovery

* Distributed availability group lossless failover
  * Adds distributed availability group for support for `REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT`. For more information, review [CREATE AVAILABILITY GROUP (Transact-SQL)](../t-sql/statements/create-availability-group-transact-sql.md).

* Improved Snapshot backup support by adding T-SQL support for freezing/thawing I/O without requiring a VDI client

## Access control

[!INCLUDE[sql-server-2022](../includes/sssql22-md.md)] introduces new granular permissions and roles.

### Granular permissions for dynamic data masking

Granular permissions for [Dynamic Data Masking](../relational-databases/security/dynamic-data-masking.md).

## TDS 8.0 - TDS wrapped in TLS

### New granular permissions

* `CREATE LOGIN` and `CREATE USER`
  * Complements the existing `ALTER ANY LOGIN` or `USER` and allows permitting only the creation of accounts but not change existing accounts

* `VIEW SERVER SECURITY STATE` and `VIEW DATABASE SECURITY STATE`
  * All dynamic management views (DMV) in SQL Server are covered by `VIEW SERVER` respectively `VIEW DATABASE STATE`. A subset of DMVs contains information about the security configuration that is not necessary to disclose to for example performance monitoring tasks but would be required to conduct a security auditing. These are covered with this new permission

* `VIEW SERVER PERFORMANCE STATE` and `VIEW DATABASE PERFORMANCE STATE`
  * All DMVs in SQL Server are covered by `VIEW SERVER` respectively `VIEW DATABASE STATE`. A subset of DMVs contains information about the security configuration that is not necessary to disclose to for example performance monitoring tasks. The remaining DMVs are covered with this new permission.
* `VIEW ANY DEFINITION` and `VIEW SECURITY DEFINITION`
  * All catalog views in SQL Server are covered by `VIEW ANY DEFINITION` on server respectively `VIEW DEFINITION` on database. A subset of catalog views contains information about the security configuration that should be limited to a security role. These are covered with this new permission.
* `VIEW ANY CRYPTOGRAPHICALLY SECURED DEFINITION` and `VIEW CRYPTOGRAPHICALLY SECURED DEFINITION` (server and database)
  * There are a small number of columns in catalog views that contain data that is highly sensitive and should not be disclosed to everyone who may otherwise have access to the other columns. Examples: key values and passwords. While none of these are stored anywhere in clear text within SQL Server, having even access to the encrypted or hashed value may be considered too risky to disclose. These new permissions cover access to such data.

### New roles

* `##MS_DefinitionReader##`
  * Holds `VIEW ANY DEFINITION`
* `##MS_ServerStateManager##`
  * Holds `ALTER SERVER STATE`
* `##MS_ServerStateReader##`
  * Holds `VIEW SERVER STATE`
* `##MS_SecurityDefinitionReader##`
  * Holds `VIEW ANY SECURITY DEFINITION`
* `##MS_DatabaseConnector##`
  * Holds `CONNECT ANY DATABASE`
  * Allows connect to any database on the logical server
  * The ideal counterpart for `##MS_ServerStateReader##,` `##MS_DefinitionReader##` and `##MS_SecurityDefinitionReader##`
* `##MS_DatabaseManager##`
* `##MS_LoginManager##`

## Setup options

|New feature or update | Details |
|:---|:---|
|Install Azure Arc agent via SQL Server command line setup |[Install SQL Server from the Command Prompt](../database-engine/install-windows/install-sql-server-from-the-command-prompt.md#install-sql-server-from-the-command-prompt)|

## SQL Server Analysis Services

This release introduces new features and improvements for performance, resource governance, and client support. For specific updates, see [What's new in SQL Server Analysis Services](/analysis-services/what-s-new-in-sql-server-analysis-services).

## See also

* [`SqlServer` PowerShell module](https://www.powershellgallery.com/packages/Sqlserver)
* [SQL Server PowerShell documentation](../powershell/sql-server-powershell.md)
* [SQL Server Workshops](https://aka.ms/sqlworkshops)
* [[!INCLUDE[SQL Server 2022](../includes/sssql22-md.md)] release notes](sql-server-2022-release-notes.md)

[!INCLUDE[get-help-options](../includes/paragraph-content/get-help-options.md)]
