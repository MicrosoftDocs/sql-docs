---
title: "What's new in SQL Server 2022 | Microsoft Docs"
description: Learn about new features for SQL Server 2022 (16.x), which gives you choices of development languages, data types, environments, and operating systems.
ms.date: 11/04/2019
ms.prod: sql
ms.reviewer: ""
ms.technology: release-landing
ms.topic: "article"
author: MikeRayMSFT
ms.author: mikeray
monikerRange: ">=sql-server-ver16"
ms.custom:
  - intro-whats-new
---
# What's new in [!INCLUDE[sql-server-2022](../includes/sssql22-md.md)]

[!INCLUDE[sqlserver](../includes/applies-to-version/sqlserver.md)]

[!INCLUDE[sql-server-2022](../includes/sssql22-md.md)] builds on previous releases to grow SQL Server as a platform that gives you choices of development languages, data types, on-premises or cloud environments, and operating systems.

This article summarizes the new features and enhancements for [!INCLUDE[sql-server-2022](../includes/sssql22-md.md)].

For more information and known issues, see [[!INCLUDE[sql-server-2022](../includes/sssql22-md.md)] release notes](sql-server-version-15-release-notes.md).

For the best experience with [!INCLUDE[sql-server-2022](../includes/sssql22-md.md)], use the [latest tools](../azure-data-studio/download-azure-data-studio.md).

The following sections provide an overview of these features.

## Azure Purview integration

* Allow Purview policies to be applied to any SQL Server instance that is enrolled in both Azure Arc and to Azure Purview data governance

- Support for 3 roles:
    - SQL Performance Monitor
    - SQL Security Auditor
    - SQL Data Reader
- Distributed Availabilit


## Operations

* Accelerated database recovery performance improvement
  * Implements a persistent version store cleaner thread per database instead of per instance. Impreoves version cleanup when there are multiple databases on the same instance.

## Performance optimization

* Improvements to string processing for columnstore indexes
* In-Memory OLTP memory management improvements
  * Improve memory management in large memory servers to reduce out of memory conditions
  * Add a new stored procedure to manually release unused memory on demand

* Concurrent GAM and SGAM updates allows multiple threads updating GAM and SGAM pages under S latch.
* Degree of parallelism (DOP) feedback automatically adjusts degree of parallelism for repeating queries to optimize for workloads where excessive parallelism can cause performance issues. Similar to optimizations in Azure SQL Database. See [Configure the max degree of parallelism (MAXDOP) in Azure SQL Database](/azure/azure-sql/database/configure-max-degree-of-parallelism).
* Multiple TCP connections for Distributed Availability Groups
  * Enables the use of multiple TCP connections for better network bandwidth utilization across a remote link with long tcp latencies.

## Language improvements

* Approximate Percentile - There are two new approximate percentile functions introduced.
  * [APPROX_PERCENTILE_CONT](../docs/approximate-percentile/APPROX_PERCENTILE_CONT.md)
  * [APPROX_PERCENTILE_DISC](../docs/approximate-percentile/APPROX_PERCENTILE_DISC.md) 
* [Auto Drop Statistics](https://github.com/microsoft/SQLEAP/blob/main/docs/auto%20drop%20statistics)

## Business continuity and disaster recovery

* Distributed availability group lossless failover
  * Adds distributed availability group for support for `REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT`. For more information, review [CREATE AVAILABILITY GROUP (Transact-SQL)](../t-sql/statements/create-availability-group-transact-sql.md).

* Improved Snapshot backup support by adding T-SQL support for freezing/thawing I/O without requiring a VDI client

## Access control

SQL Server 2022 Preview introduces new granular permissions and roles.

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

## <a id="ml"></a> SQL Server Machine Learning Services

|New feature or update | Details |
|:---|:---|

## SQL Server Analysis Services

This release introduces new features and improvements for performance, resource governance, and client support.

| New feature or update | Details |
|:---|:---|

## SQL Server Integration Services

This release introduces new features to improve file operations.

| New feature or update | Details |
|:---|:---|

## SQL Server [!INCLUDE[master-data-services](../includes/ssmdsshort-md.md)]

| New feature or update | Details |
|:---|:---|

## SQL Server Reporting Services

## See also

- [`SqlServer` PowerShell module](https://www.powershellgallery.com/packages/Sqlserver)
- [SQL Server PowerShell documentation](../powershell/sql-server-powershell.md)

## Next steps

- [SQL Server Workshops](https://aka.ms/sqlworkshops)
- [[!INCLUDE[sql-server-2022](../includes/sssql22-md.md)] release notes](sql-server-version-15-release-notes.md)
- [Microsoft [!INCLUDE[sql-server-2022](../includes/sssql22-md.md)]: Technical white paper](https://aka.ms/sql2019whitepaper)

[!INCLUDE[get-help-options](../includes/paragraph-content/get-help-options.md)]
