---
title: What's new? (Archive)
titleSuffix: Azure SQL Managed Instance
description: Learn about the features and documentation improvements for Azure SQL Managed Instance made in previous years. (Archive)
author: MashaMSFT
ms.author: mathoma
ms.reviewer: wiassaf, randolphwest
ms.date: 04/26/2023
ms.service: sql-managed-instance
ms.subservice: service-overview
ms.topic: whats-new
ms.custom: references_regions
---
# What's new in Azure SQL Managed Instance? (Archive)

[!INCLUDE[appliesto-sqldb-sqlmi](../includes/appliesto-sqlmi.md)]

> [!div class="op_single_selector"]
> - [Azure SQL Database](../database/doc-changes-updates-release-notes-whats-new-archive.md?view=azuresql-db&preserve-view=true)
> - [Azure SQL Managed Instance](../managed-instance/doc-changes-updates-release-notes-whats-new-archive.md?view=azuresql-mi&preserve-view=true)

This article summarizes older documentation changes associated with new features and improvements in the releases of [Azure SQL Managed Instance](https://azure.microsoft.com/updates/?product=sql-database&query=sql%20managed%20instance). To learn more about Azure SQL Managed Instance, see the [overview](sql-managed-instance-paas-overview.md).

Return to [What's new in Azure SQL Managed Instance?](doc-changes-updates-release-notes-whats-new.md)

## 2021

The following changes were added to SQL Managed Instance and the documentation in 2021:

| Changes | Details |
| --- | --- |
| **16 TB support for Business Critical preview** | The Business Critical service tier of SQL Managed Instance now provides increased maximum instance storage capacity of up to 16 TB with the new premium-series and memory optimized premium-series hardware, which are currently in preview. See [resource limits](resource-limits.md#service-tier-characteristics) to learn more. |
| **16 TB support for General Purpose GA** | Deploying a 16-TB instance to the General Purpose service tier is now generally available. See [resource limits](resource-limits.md) to learn more. |
| **Azure AD-only authentication GA** | Restricting authentication to your Azure SQL Managed Instance only to Azure Active Directory users is now generally available. To learn more, see [Azure AD-only authentication](../database/authentication-azure-ad-only-authentication.md). |
| **Distributed transactions GA** | The ability to execute distributed transactions across managed instances is now generally available. See [Distributed transactions](../database/elastic-transactions-overview.md) to learn more. |
| **Endpoint policies preview** | It's now possible to configure an endpoint policy to restrict access from a SQL Managed Instance subnet to an Azure Storage account. This grants an extra layer of protection against inadvertent or malicious data exfiltration. See [Endpoint policies](./service-endpoint-policies-configure.md) to learn more. |
| **Link feature preview** | Use the link feature for SQL Managed Instance to replicate data from your SQL Server hosted anywhere to Azure SQL Managed Instance, leveraging the benefits of Azure without moving your data to Azure, to offload your workloads, for disaster recovery, or to migrate to the cloud. See the [Link feature for SQL Managed Instance](managed-instance-link-feature-overview.md) to learn more. The link feature is currently in limited public preview. |
| **Long-term backup retention GA** | Storing full backups for a specific database with configured redundancy for up to 10 years in Azure Blob storage is now generally available. To learn more, see [Long-term backup retention](long-term-backup-retention-configure.md). |
| **Move instance to different subnet GA** | It's now possible to move your SQL Managed Instance to a different subnet. See [Move instance to different subnet](vnet-subnet-move-instance.md) to learn more. |
| **New hardware preview** | There are now two new hardware configurations for SQL Managed Instance: premium-series, and a memory optimized premium-series. Both offerings take advantage of a new hardware powered by the latest Intel Ice Lake CPUs, and offer a higher memory to vCore ratio to support your most resource demanding database applications. As part of this announcement, the Gen5 hardware has been renamed to standard-series. The two new premium hardware offerings are currently in preview. See [resource limits](resource-limits.md#service-tier-characteristics) to learn more. |
| **Split what's new** | The previously combined **What's new** article has been split by product - [What's new in SQL Database](../database/doc-changes-updates-release-notes-whats-new.md) and [What's new in SQL Managed Instance](doc-changes-updates-release-notes-whats-new.md), making it easier to identify what features are currently in preview, generally available, and significant documentation changes. Additionally, the [Known Issues in SQL Managed Instance](doc-changes-updates-known-issues.md) content has moved to its own page. |
| **16 TB support for General Purpose preview** | Support has been added for allocation of up to 16 TB of space for SQL Managed Instance in the General Purpose service tier. See [resource limits](resource-limits.md) to learn more. This instance offer is currently in preview. |
| **Parallel backup** | It's now possible to take backups in parallel for SQL Managed Instance in the General Purpose tier, enabling faster backups. See the [Parallel backup for better performance](https://techcommunity.microsoft.com/t5/azure-sql/parallel-backup-for-better-performance-in-sql-managed-instance/ba-p/2421762) blog entry to learn more. |
| **Azure AD-only authentication preview** | It's now possible to restrict authentication to your Azure SQL Managed Instance only to Azure Active Directory users. This feature is currently in preview. To learn more, see [Azure AD-only authentication](../database/authentication-azure-ad-only-authentication.md). |
| **Resource Health monitor** | Use Resource Health to monitor the health status of your Azure SQL Managed Instance. See [Resource health](../database/resource-health-to-troubleshoot-connectivity.md) to learn more. |
| **Granular permissions for data masking GA** | Granular permissions for dynamic data masking for Azure SQL Managed Instance is now generally available (GA). To learn more, see [Dynamic data masking](../database/dynamic-data-masking-overview.md#permissions). |
| **User-defined routes (UDR) tables** | Service-aided subnet configuration for Azure SQL Managed Instance now makes use of service tags for user-defined routes (UDR) tables. See the [connectivity architecture](connectivity-architecture-overview.md) to learn more. |
| **Audit management operations** | The ability to audit SQL Managed Instance operations is now generally available (GA). |
| **Log Replay Service** | It's now possible to migrate databases from SQL Server to Azure SQL Managed Instance using the Log Replay Service. To learn more, see [Migrate with Log Replay Service](log-replay-service-migrate.md). This feature is currently in preview. |
| **Long-term backup retention** | Support for Long-term backup retention up to 10 years on Azure SQL Managed Instance. To learn more, see [Long-term backup retention](long-term-backup-retention-configure.md) |
| **Machine Learning Services GA** | The Machine Learning Services for Azure SQL Managed Instance are now generally available (GA). To learn more, see [Machine Learning Services for SQL Managed Instance](machine-learning-services-overview.md). |
| **Maintenance window** | The maintenance window feature allows you to configure a maintenance schedule for your Azure SQL Managed Instance. To learn more, see [maintenance window](../database/maintenance-window.md). |
| **Service Broker message exchange** | The Service Broker component of Azure SQL Managed Instance allows you to compose your applications from independent, self-contained services, by providing native support for reliable and secure message exchange between the databases attached to the service. Currently in preview. To learn more, see [Service Broker](/sql/database-engine/configure-windows/sql-server-service-broker).
| **SQL Insights (preview)** | SQL Insights (preview) is a comprehensive solution for monitoring any product in the Azure SQL family. SQL Insights uses dynamic management views to expose the data you need to monitor health, diagnose problems, and tune performance. To learn more, see [Azure Monitor SQL Insights (preview)](/azure/azure-monitor/insights/sql-insights-overview). |

## 2020

The following changes were added to SQL Managed Instance and the documentation in 2020:

| Changes | Details |
| --- | --- |
| **Audit support operations** | The auditing of Microsoft support operations capability enables you to audit Microsoft support operations when you need to access your servers and/or databases during a support request to your audit logs destination (Preview). To learn more, see [Audit support operations](../database/auditing-microsoft-support-operations.md). |
| **Elastic transactions** | Elastic transactions allow for distributed database transactions spanning multiple databases across Azure SQL Database and Azure SQL Managed Instance. Elastic transactions have been added to enable frictionless migration of existing applications, as well as development of modern multi-tenant applications relying on vertically or horizontally partitioned database architecture (Preview). To learn more, see [Distributed transactions](../database/elastic-transactions-overview.md#transactions-for-sql-managed-instance). |
| **Configurable backup storage redundancy** | It's now possible to configure Locally redundant storage (LRS) and zone-redundant storage (ZRS) options for backup storage redundancy, providing more flexibility and choice. To learn more, see [Configure backup storage redundancy](../managed-instance/automated-backups-change-settings.md#configure-backup-storage-redundancy). |
| **TDE-encrypted backup performance improvements** | It's now possible to set the point-in-time restore (PITR) backup retention period, and automated compression of backups encrypted with transparent data encryption (TDE) are now 30 percent more efficient in consuming backup storage space, saving costs for the end user. See [Change PITR](../managed-instance/automated-backups-change-settings.md#change-short-term-retention-policy) to learn more. |
| **Azure AD authentication improvements** | Automate user creation using Azure AD applications and create individual Azure AD guest users (preview). To learn more, see [Directory readers in Azure AD](../database/authentication-aad-directory-readers-role.md) |
| **Global VNet peering support** | Global virtual network peering support has been added to SQL Managed Instance, improving the geo-replication experience. See [geo-replication between managed instances](auto-failover-group-configure-sql-mi.md#enabling-connectivity-between-the-instances). |
| **Hosting SSRS catalog databases** | SQL Managed Instance can now host catalog databases of SQL Server Reporting Services (SSRS) for versions 2017 and newer. |
| **Major performance improvements** | Introducing improvements to SQL Managed Instance performance, including improved transaction log write throughput, improved data and log IOPS for Business Critical instances, and improved `tempdb` performance. For more information, see the [improved performance](https://techcommunity.microsoft.com/t5/azure-sql/announcing-major-performance-improvements-for-azure-sql-database/ba-p/1701256) tech community blog to learn more.  
| **Enhanced management experience** | Using the new [OPERATIONS API](/rest/api/sql/2021-02-01-preview/managed-instance-operations), it's now possible to check the progress of long-running instance operations. To learn more, see [Management operations](management-operations-overview.md?tabs=azure-portal).
| **Machine learning support** | Machine Learning Services with support for R and Python languages now include preview support on Azure SQL Managed Instance (Preview). To learn more, see [Machine learning with SQL Managed Instance](machine-learning-services-overview.md). |
| **User-initiated failover** | User-initiated failover is now generally available, providing you with the capability to manually initiate an automatic failover using PowerShell, CLI commands, and API calls, improving application resiliency. To learn more, see, [testing resiliency](../database/high-availability-sla.md#testing-application-fault-resiliency). |

## Known issues

The known issues content has moved to a dedicated [known issues in SQL Managed Instance](doc-changes-updates-known-issues.md) article.

## Contribute to content

To contribute to the Azure SQL documentation, see the [Docs contributor guide](/contribute/).
