---
title: What's new?
titleSuffix: Azure SQL Managed Instance
description: Learn about the new features and documentation improvements for Azure SQL Managed Instance.
author: MashaMSFT
ms.author: mathoma
ms.date: 11/30/2022
ms.service: sql-managed-instance
ms.subservice: service-overview
ms.topic: conceptual
ms.custom:
  - references_regions
  - ignite-fall-2021
---
# What's new in Azure SQL Managed Instance?
[!INCLUDE[appliesto-sqldb-sqlmi](../includes/appliesto-sqlmi.md)]

> [!div class="op_single_selector"]
> * [Azure SQL Database](../database/doc-changes-updates-release-notes-whats-new.md)
> * [Azure SQL Managed Instance](../managed-instance/doc-changes-updates-release-notes-whats-new.md)

This article summarizes the documentation changes associated with new features and improvements in the recent releases of [Azure SQL Managed Instance](https://azure.microsoft.com/updates/?product=sql-database&query=sql%20managed%20instance). To learn more about Azure SQL Managed Instance, see the [overview](sql-managed-instance-paas-overview.md).


## Preview

The following table lists the features of Azure SQL Managed Instance that are currently in preview:

| Feature | Details |
| ---| --- |
|[Approximate percentile](/sql/t-sql/functions/approx-percentile-cont-transact-sql) | Quickly compute percentiles for a large dataset with acceptable rank-based error bounds to help make rapid decisions by using approximate percentile aggregate functions. | 
|[Database copy and move](database-copy-move-how-to.md) | Perform an online database copy or move operation across managed instances. | 
|[Distributed Transaction Coordinator (DTC)](distributed-transaction-coordinator-dtc.md) | Use DTC to run distributed transactions in mixed environments such as across managed instances, SQL Servers, other relational database management systems (RDBMSs), custom applications and other transaction participants hosted in any environment that can establish network connectivity to Azure.  | 
|[Endpoint policies](./service-endpoint-policies-configure.md) | Configure which Azure Storage accounts can be accessed from a SQL Managed Instance subnet. Grants an extra layer of protection against inadvertent or malicious data exfiltration.|
|[Instance pools](instance-pools-overview.md) | A convenient and cost-efficient way to migrate smaller SQL Server instances to the cloud. |
|[Instance stop and start](instance-stop-start-how-to.md) | Stop and start your managed instance to save on licensing and compute costs. | 
|[License-free standby replica](auto-failover-group-standby-replica-how-to-configure.md) | Save on licensing costs when you designate your secondary replica as **Standby**. | 
|[Maintenance window advance notifications](../database/advance-notifications.md)| Advance notifications (preview) for databases configured to use a non-default [maintenance window](../database/maintenance-window.md). Advance notifications are in preview for Azure SQL Managed Instance. |
|[Managed Instance link](managed-instance-link-feature-overview.md)| Online replication of SQL Server databases hosted anywhere to Azure SQL Managed Instance. Using the link feature with SQL Server 2019 and earlier, as well as failing back from SQL MI to SQL Server 2022 is in preview. |
|[SDK-style SQL project](/sql/azure-data-studio/extensions/sql-database-project-extension-sdk-style-projects) | Use [Microsoft.Build.Sql](https://www.nuget.org/packages/Microsoft.Build.Sql) for SDK-style SQL projects in the SQL Database Projects extension in Azure Data Studio or VS Code. SDK-style SQL projects are especially advantageous for applications shipped through pipelines or built in cross-platform environments.| 
|[Service Broker cross-instance message exchange](/sql/database-engine/configure-windows/sql-server-service-broker) | Support for cross-instance message exchange using Service Broker on Azure SQL Managed Instance. |
|[SQL Database Projects extension](/sql/azure-data-studio/extensions/sql-database-project-extension) | An extension to develop databases for Azure SQL Database with Azure Data Studio and VS Code. A SQL project is a local representation of SQL objects that comprise the schema for a single database, such as tables, stored procedures, or functions. | 
|[SQL Insights](/azure/azure-monitor/insights/sql-insights-overview) | SQL Insights is a comprehensive solution for monitoring any product in the Azure SQL family. SQL Insights uses dynamic management views to expose the data you need to monitor health, diagnose problems, and tune performance. |
|[Threat detection](threat-detection-configure.md) | Threat detection notifies you of security threats detected to your database. |
|[Zone-redundancy](../database/high-availability-sla.md) | Deploy your Business Critical SQL Managed Instance to multiple availability zones to improve the availability of your instance in the event of a disaster. | 


## General availability (GA)

The following table lists the new generally available (GA) features of Azure SQL Managed Instance, and those that have transitioned from preview to GA within the last 12 months:

| Feature | GA Month | Details |
| ---| --- |--- |
|[Backup transparency](backup-transparency.md) | November 2022 |  Query the `msdb` database to explore your backup history. | 
|[Cross-subscription PITR](point-in-time-restore.md) | November 2022 | Restore your database to an instance in a different subscription than your original managed instance by using point-in-time restore (PITR) . 
|[Migrate to SQL MI with Log Replay Service](log-replay-service-migrate.md) | November 2022 |  Migrate databases from SQL Server to SQL Managed Instance by using Log Replay Service. |
|[One-way Managed Instance link for SQL Server 2022](managed-instance-link-feature-overview.md)| November 2022 | Online one-way replication of SQL Server databases hosted on SQL Server 2022 to Azure SQL Managed Instance. |
|[Restore database from SQL MI to SQL Server](restore-database-to-sql-server.md) | November 2022 |  Restore your database from Azure SQL Managed Instance to SQL Server 2022. | 
|[Time series](/sql/t-sql/functions/generate-series-transact-sql) | November 2022 | Generates a series of numbers within a given interval.  Review [GENERATE_SERIES](/sql/t-sql/functions/generate-series-transact-sql) and [DATE_BUCKET](/sql/t-sql/functions/date-bucket-transact-sql) to learn more. | 
|[Transactional Replication](replication-transactional-overview.md) | November 2022 | Replicate data from SQL Managed Instance to database hosted on SQL Server, SQL Managed Instance or SQL Database, or from SQL Server to SQL Managed Instance. To get started, review [Configure replication in Azure SQL Managed Instance](replication-between-two-instances-configure-tutorial.md). |
|[Automated key rotation for TDE with CMK](../database/transparent-data-encryption-byok-overview.md#rotation-of-tde-protector) | October 2022 | Automatically switch to a new key when using a customer-managed key (CMK) for TDE with Azure SQL Managed Instance. | 
|[Tempdb configurations](https://techcommunity.microsoft.com/t5/azure-sql-blog/improve-your-sql-managed-instance-performance-with-new-tempdb/ba-p/3640094)| September 2022 | Configure the number of `tempdb` files and their growth increments to tune the performance of your instance even more. |
|[Memory optimized premium-series hardware](resource-limits.md#service-tier-characteristics) | September 2022 |Deploy your SQL Managed Instance to the new memory optimized premium-series hardware to take advantage of the latest Intel Ice Lake CPUs. Memory optimized hardware offers higher memory to vCore ratio. | 
|[16-TB support in Business Critical](resource-limits.md#service-tier-characteristics) | September 2022 |Support for allocation up to 16 TB of space on SQL Managed Instance in the Business Critical service tier using the new memory optimized premium-series hardware. | 
|[Data virtualization](data-virtualization-overview.md) | September 2022 |  Join locally stored relational data with data queried from external data sources, such as Azure Data Lake Storage Gen2 or Azure Blob Storage. |
|[GZRS backup storage redundancy](automated-backups-overview.md) | September 2022 | Backup storage redundancy option that combines geo-redundancy and zone-redundancy, improving both availability and resiliency, while also enabling Point-In-Time Restore (PITR) across availability zones.  | 
|[Windows Auth for Azure Active Directory principals](winauth-azuread-overview.md)| August 2022 | Kerberos authentication for Azure Active Directory (Azure AD) enables Windows Authentication access to Azure SQL Managed Instance. |
|[Query Store hints](/sql/relational-databases/performance/query-store-hints?view=azuresqldb-mi-current&preserve-view=true) | August 2022 | Use query hints to optimize your query execution via the OPTION clause. |
|[Premium-series hardware](resource-limits.md#service-tier-characteristics) | July 2022 | Deploy your SQL Managed Instance to the new premium-series hardware to take advantage of the latest Intel Ice Lake CPUs.  | 
|[Maintenance window](../database/maintenance-window.md)| March 2022 | The maintenance window feature allows you to configure maintenance schedule for your Azure SQL Managed Instance. [Maintenance window advance notifications](../database/advance-notifications.md), however, are in preview for Azure SQL Managed Instance.|
|[16-TB support in General Purpose](resource-limits.md)| November 2021 |  Support for allocation up to 16 TB of space on SQL Managed Instance in the General Purpose service tier. |
[Azure Active Directory-only authentication](../database/authentication-azure-ad-only-authentication.md) | November 2021 |  It's now possible to restrict authentication to your Azure SQL Managed Instance only to Azure Active Directory users. |
|[Distributed transactions](../database/elastic-transactions-overview.md) | November 2021 | Distributed database transactions for Azure SQL Managed Instance allow you to run distributed transactions that span several databases across instances. | 
|[Linked server - managed identity Azure AD authentication](/sql/relational-databases/system-stored-procedures/sp-addlinkedserver-transact-sql#h-create-sql-managed-instance-linked-server-with-managed-identity-azure-ad-authentication) |November 2021 | Create a linked server with managed identity authentication for your Azure SQL Managed Instance.|
|[Linked server - pass-through Azure AD authentication](/sql/relational-databases/system-stored-procedures/sp-addlinkedserver-transact-sql#i-create-sql-managed-instance-linked-server-with-pass-through-azure-ad-authentication) |November 2021 | Create a linked server with pass-through Azure AD authentication for your Azure SQL Managed Instance. |
|[Long-term backup retention](long-term-backup-retention-configure.md) |November 2021 | Store full backups for a specific database with configured redundancy for up to 10 years in Azure Blob storage, restoring the database as a new database. |
|[Move instance to different subnet](vnet-subnet-move-instance.md)| November 2021 | Move SQL Managed Instance to a different subnet using the Azure portal, Azure PowerShell or the Azure CLI.  |



## November 2022 feature wave

November 2022 introduced a wave of new features and automatic benefits for Azure SQL Managed Instance. 

Rollout of the November 2022 Feature Wave is happening over the course of several months.  The initial rollout phase targets instances that belong to Dev/Test subscriptions, with other subscription types enrolling in subsequent months. 

Eligible existing instances created prior to November 2022 can enroll into the feature wave immediately to unlock the new benefits and features.  To get started, review [Enroll in the feature wave](november-2022-feature-wave-enroll.md).


## Documentation changes

Learn about significant changes to the Azure SQL Managed Instance documentation.


| Changes | Details |
| --- | --- |
|**Backup transparency with msdb GA** | For the purpose of backup transparency, it's now possible to query the `msdb` database to explore automated backup history. This feature is generally available. To learn more, review [backup transparency](backup-transparency.md). | 
|**Cross-subscription PITR GA** | It's now possible to restore your database for your SQL Managed Instance across subscriptions by using point-in-time restore (PITR). This feature is generally available. To learn more, review [Point-in-time restore](point-in-time-restore.md). | 
|**Database copy and move preview** | Perform an online copy or move operation of your database across managed instances. This feature is currently in preview. To learn more, review [Copy or move your database](database-copy-move-how-to.md). | 
|**Distributed Transaction Coordinator (DTC) preview** | Use DTC to run distributed transactions in mixed environments such as across managed instances, SQL Servers, other relational database management systems (RDBMSs), custom applications and other transaction participants hosted in any environment that can establish network connectivity to Azure.  This feature is currently in preview. Review [Distributed Transaction Coordinator (DTC)](distributed-transaction-coordinator-dtc.md)  to learn more.  | 
|**Free license for standby replica preview** | It's now possible to designate your secondary DR-only instance as **Standby**, saving on licensing costs. This feature is currently in preview. To learn more, review [Configure standby replica](auto-failover-group-standby-replica-how-to-configure.md). 
| **Gen5 hardware rename** | The Gen5 hardware in the vCore purchasing model has been renamed to **standard-series (Gen5)**. | 
|**Instance stop preview** | It's now possible to save on costs by stopping your General Purpose Azure SQL Managed Instance when you're not using it. To learn more, review [stop and start instance](instance-stop-start-how-to.md). | 
|**Log Replay Service GA** | Migrate your databases to Azure SQL Managed Instance using the Log Replay Service (LRS). This feature is now generally available. To learn more, review [Log Replay Service overview](log-replay-service-overview.md). To get started, review [Migrate with LRS](log-replay-service-migrate.md) | 
|**Managed Instance link for SQL Server 2022 GA** | Using the Managed Instance link to replicate data from SQL Server 2022 to Azure SQL Managed Instance is now generally available. Using the link feature with versions older than SQL Server 2022 is still in preview. To learn more, review [Managed Instance link](managed-instance-link-feature-overview.md).  | 
|**Managed Instance link fail over to SQL Server 2022 preview** | It's now possible to use the Managed Instance link feature to fail back from Azure SQL Managed Instance to SQL Server 2022. This feature is currently in preview. To learn more, review [Managed Instance link](managed-instance-link-feature-overview.md).  | 
|**November 2022 feature wave early enrollment** | The November 2022 feature wave is being rolled out over several months but it may be possible to enroll early. Review [Enroll in the feature wave](november-2022-feature-wave-enroll.md) to learn more. | 
|**Simplified connectivity architecture**| The November 2022 feature wave significantly simplifies the connectivity architecture for SQL Managed Instance, such as removing the management endpoint, and reducing the number of mandatory rules. Review [Connectivity architecture](connectivity-architecture-overview.md) to learn more. |
|**Restore database to SQL Server GA** | It's now possible to restore your database backup from Azure SQL Managed Instance to SQL Server 2022. The capability to do so is generally available, and enabled by default on all instances, both currently existing, and those deployed in the future. To learn more, review [Restore database to SQL Server](restore-database-to-sql-server.md). | 
| **Time series GA** | Generates a series of numbers within a given interval. This feature is generally available.  Review [GENERATE_SERIES](/sql/t-sql/functions/generate-series-transact-sql) and [DATE_BUCKET](/sql/t-sql/functions/date-bucket-transact-sql) to learn more. 
|**Transactional replication GA** | Replicate data from SQL Managed Instance to database hosted on SQL Server, SQL Managed Instance or SQL Database, or from SQL Server to SQL Managed Instance. This feature is now generally available. To learn more, review [Transactional Replication](replication-transactional-overview.md). To get started, review [Configure replication in Azure SQL Managed Instance](replication-between-two-instances-configure-tutorial.md). | 
| **Zone-redundancy preview** | It's now possible to deploy your managed instance to multiple availability zones to improve the availability of your instance. This feature is currently in preview and only available to instances in the Business Critical service tier. Review [High availability](../database/high-availability-sla.md) to learn more. |


### October 2022

| Changes | Details |
| --- | --- |
| **Approximate percentiles preview** | Support has been added to quickly compute percentiles using approximate percentile aggregate functions for large datasets with acceptable rank-based error bounds. This feature is currently in preview. To learn more, review [Approx_Percentile_Cont](/sql/t-sql/functions/approx-percentile-cont-transact-sql) and [Approx_Percentile_Disc](/sql/t-sql/functions/approx-percentile-disc-transact-sql). | 
| **Automated TDE key rotation for CMK GA** | Automatically switch to a new key when using a customer-managed key (CMK) with TDE. This feature is now generally available. To learn more, review [Automated key rotation](../database/transparent-data-encryption-byok-overview.md#rotation-of-tde-protector). |


### September 2022

| Changes | Details |
| --- | --- |
| **16 TB support in Business Critical GA** | Support for allocation up to 16 TB of space on SQL Managed Instance in the Business Critical service tier using the new memory optimized premium-series hardware. This feature is now generally available. Review [Resource limits](resource-limits.md#service-tier-characteristics) to learn more. | 
| **Data virtualization GA** | The data virtualization feature allows users to join locally stored relational data with data queried from external data sources, such as Azure Data Lake Storage Gen2 or Azure Blob Storage. This feature is now generally available. Review [Data virtualization](data-virtualization-overview.md) to learn more. |
| **Memory optimized premium-series hardware GA** | Deploy your SQL Managed Instance to the new memory optimized premium-series hardware to take advantage of the latest Intel Ice Lake CPUs. Memory optimized hardware offers higher memory to vCore ratio. This feature is now generally available. [Resource limits](resource-limits.md#service-tier-characteristics) to learn more. | 
| **GZRS backup storage option** | Combine the availability and resiliency of geo-redundancy and zone-redundancy when you choose the new backup storage redundancy option Geo-Zone-Redundant Storage (GZRS) for Azure SQL Managed Instance. This option is generally available. Review [automated backups](automated-backups-overview.md) to learn more. | 



### August 2022

| Changes | Details |
| --- | --- |
| **Windows Auth for Azure Active Directory principals** | Kerberos authentication for Azure Active Directory (Azure AD) enables Windows Authentication access to Azure SQL Managed Instance. This feature is now generally available (GA). To learn more, review [Windows Auth for Azure Active Directory principals](winauth-azuread-overview.md). |
|**Query Store hints**| Use query hints to optimize your query execution via the OPTION clause. This feature is now generally available (GA). To learn more, review, [Query Store hints](/sql/relational-databases/performance/query-store-hints?view=azuresqldb-mi-current&preserve-view=true)| 

### July 2022

| Changes | Details |
| --- | --- |
| **Premium-series hardware GA** | Deploy your SQL Managed Instance to the new premium-series hardware to take advantage of the latest Intel Ice Lake CPUs. The premium-series hardware is now generally available. See [Premium-series hardware](resource-limits.md#service-tier-characteristics) to learn more.   | 

### May 2022

| Changes | Details |
| --- | --- |
| **SDK-style SQL projects**| Use [Microsoft.Build.Sql](https://www.nuget.org/packages/Microsoft.Build.Sql) for SDK-style SQL projects in the SQL Database Projects extension in Azure Data Studio or VS Code. This feature is currently in preview. To learn more, see [SDK-style SQL projects](/sql/azure-data-studio/extensions/sql-database-project-extension-sdk-style-projects). | 
| **JavaScript & Python bindings**| Support for JavaScript and Python SQL bindings for Azure Functions is currently in preview. See [Azure SQL bindings for Azure Functions](/azure/azure-functions/functions-bindings-azure-sql) to learn more. | 

### March 2022

| Changes | Details |
| --- | --- |
| **Data virtualization preview** | It's now possible to query data in external sources such as Azure Data Lake Storage Gen2 or Azure Blob Storage, joining it with locally stored relational data. This feature is currently in preview. To learn more, see [Data virtualization](data-virtualization-overview.md). |  
| **Managed Instance link guidance** | We've published a number of guides for using the [Managed Instance link feature](managed-instance-link-feature-overview.md), including how to [prepare your environment](managed-instance-link-preparation.md), [configure replication by using SSMS](managed-instance-link-use-ssms-to-replicate-database.md), [configure replication via scripts](managed-instance-link-use-scripts-to-replicate-database.md),  [fail over your database by using SSMS](managed-instance-link-use-ssms-to-failover-database.md), [fail over your database via scripts](managed-instance-link-use-scripts-to-failover-database.md) and some [best practices](managed-instance-link-best-practices.md) when using the link feature (currently in preview). |
| **Maintenance window GA, advance notifications preview** | The [maintenance window](../database/maintenance-window.md) feature is now generally available, allowing you to configure a maintenance schedule for your Azure SQL Managed Instance. It's also possible to receive advance notifications for planned maintenance events, which is currently in preview. Review [Maintenance window advance notifications (preview)](../database/advance-notifications.md) to learn more. |
| **Windows Auth for Azure Active Directory principals preview** | Windows Authentication for managed instances empowers customers to move existing services to the cloud while maintaining a seamless user experience, and provides the basis for infrastructure modernization. Learn more in [Windows Authentication for Azure Active Directory principals on Azure SQL Managed Instance](winauth-azuread-overview.md). |



### 2021

| Changes | Details |
| --- | --- |
| **16 TB support for Business Critical preview** | The Business Critical service tier of SQL Managed Instance now provides increased maximum instance storage capacity of up to 16 TB with the new premium-series and memory optimized premium-series hardware, which are currently in preview. See [resource limits](resource-limits.md#service-tier-characteristics) to learn more. |
|**16 TB support for General Purpose GA** | Deploying a 16-TB instance to the General Purpose service tier is now generally available.  See [resource limits](resource-limits.md) to learn more. |
| **Azure AD-only authentication GA** |  Restricting authentication to your Azure SQL Managed Instance only to Azure Active Directory users is now generally available. To learn more, see [Azure AD-only authentication](../database/authentication-azure-ad-only-authentication.md). |
| **Distributed transactions GA** | The ability to execute distributed transactions across managed instances is now generally available. See  [Distributed transactions](../database/elastic-transactions-overview.md) to learn more. |
|**Endpoint policies preview** | It's now possible to configure an endpoint policy to restrict access from a SQL Managed Instance subnet to an Azure Storage account. This grants an extra layer of protection against inadvertent or malicious data exfiltration. See [Endpoint policies](./service-endpoint-policies-configure.md) to learn more. |
|**Link feature preview** | Use the link feature for SQL Managed Instance to replicate data from your SQL Server hosted anywhere to Azure SQL Managed Instance, leveraging the benefits of Azure without moving your data to Azure, to offload your workloads, for disaster recovery, or to migrate to the cloud. See the [Link feature for SQL Managed Instance](managed-instance-link-feature-overview.md) to learn more. The link feature is currently in limited public preview. |
|**Long-term backup retention GA** | Storing full backups for a specific database with configured redundancy for up to 10 years in Azure Blob storage is now generally available. To learn more, see [Long-term backup retention](long-term-backup-retention-configure.md). |
| **Move instance to different subnet GA** | It's now possible to move your SQL Managed Instance to a different subnet. See [Move instance to different subnet](vnet-subnet-move-instance.md) to learn more. |
|**New hardware preview** | There are now two new hardware configurations for SQL Managed Instance: premium-series, and a memory optimized premium-series. Both offerings take advantage of a new hardware powered by the latest Intel Ice Lake CPUs, and offer a higher memory to vCore ratio to support your most resource demanding database applications. As part of this announcement, the Gen5 hardware has been renamed to standard-series. The two new premium hardware offerings are currently in preview. See [resource limits](resource-limits.md#service-tier-characteristics) to learn more. |
|**Split what's new** | The previously combined **What's new** article has been split by product - [What's new in SQL Database](../database/doc-changes-updates-release-notes-whats-new.md) and [What's new in SQL Managed Instance](doc-changes-updates-release-notes-whats-new.md), making it easier to identify what features are currently in preview, generally available, and significant documentation changes. Additionally, the [Known Issues in SQL Managed Instance](doc-changes-updates-known-issues.md) content has moved to its own page. | 
|**16 TB support for General Purpose preview** | Support has been added for allocation of up to 16 TB of space for SQL Managed Instance in the General Purpose service tier. See [resource limits](resource-limits.md) to learn more. This instance offer is currently in preview. | 
| **Parallel backup** | It's now possible to take backups in parallel for SQL Managed Instance in the General Purpose tier, enabling faster backups. See the [Parallel backup for better performance](https://techcommunity.microsoft.com/t5/azure-sql/parallel-backup-for-better-performance-in-sql-managed-instance/ba-p/2421762) blog entry to learn more. |
| **Azure AD-only authentication preview** | It's now possible to restrict authentication to your Azure SQL Managed Instance only to Azure Active Directory users. This feature is currently in preview. To learn more, see [Azure AD-only authentication](../database/authentication-azure-ad-only-authentication.md). | 
| **Resource Health monitor** | Use Resource Health to  monitor the health status of your Azure SQL Managed Instance. See [Resource health](../database/resource-health-to-troubleshoot-connectivity.md) to learn more. |
| **Granular permissions for data masking GA** | Granular permissions for dynamic data masking for Azure SQL Managed Instance is now generally available (GA). To learn more, see [Dynamic data masking](../database/dynamic-data-masking-overview.md#permissions). | 
| **User-defined routes (UDR) tables** | Service-aided subnet configuration for Azure SQL Managed Instance now makes use of service tags for user-defined routes (UDR) tables. See the [connectivity architecture](connectivity-architecture-overview.md) to learn more. |
| **Audit management operations** | The ability to audit SQL Managed Instance operations is now generally available (GA). | 
| **Log Replay Service** | It's now possible to migrate databases from SQL Server to Azure SQL Managed Instance using the Log Replay Service. To learn more, see [Migrate with Log Replay Service](log-replay-service-migrate.md). This feature is currently in preview. | 
| **Long-term backup retention** | Support for Long-term backup retention up to 10 years on Azure SQL Managed Instance. To learn more, see [Long-term backup retention](long-term-backup-retention-configure.md)|
| **Machine Learning Services GA** | The Machine Learning Services for Azure SQL Managed Instance are now generally available (GA). To learn more, see [Machine Learning Services for SQL Managed Instance](machine-learning-services-overview.md).| 
| **Maintenance window** | The maintenance window feature allows you to configure a maintenance schedule for your Azure SQL Managed Instance. To learn more, see [maintenance window](../database/maintenance-window.md).|
| **Service Broker message exchange** | The Service Broker component of Azure SQL Managed Instance allows you to compose your applications from independent, self-contained services, by providing native support for reliable and secure message exchange between the databases attached to the service. Currently in preview. To learn more, see [Service Broker](/sql/database-engine/configure-windows/sql-server-service-broker).
| **SQL Insights (preview)** | SQL Insights (preview) is a comprehensive solution for monitoring any product in the Azure SQL family. SQL Insights uses dynamic management views to expose the data you need to monitor health, diagnose problems, and tune performance. To learn more, see [Azure Monitor SQL Insights (preview)](/azure/azure-monitor/insights/sql-insights-overview). | 

### 2020

The following changes were added to SQL Managed Instance and the documentation in 2020: 

| Changes | Details |
| --- | --- |
| **Audit support operations** | The auditing of Microsoft support operations capability enables you to audit Microsoft support operations when you need to access your servers and/or databases during a support request to your audit logs destination (Preview). To learn more, see [Audit support operations](../database/auditing-overview.md#auditing-of-microsoft-support-operations).|
| **Elastic transactions** | Elastic transactions allow for distributed database transactions spanning multiple databases across Azure SQL Database and Azure SQL Managed Instance. Elastic transactions have been added to enable frictionless migration of existing applications, as well as development of modern multi-tenant applications relying on vertically or horizontally partitioned database architecture (Preview). To learn more, see [Distributed transactions](../database/elastic-transactions-overview.md#transactions-for-sql-managed-instance). | 
| **Configurable backup storage redundancy** | It's now possible to configure Locally redundant storage (LRS) and zone-redundant storage (ZRS) options for backup storage redundancy, providing more flexibility and choice. To learn more, see [Configure backup storage redundancy](../managed-instance/automated-backups-change-settings.md#configure-backup-storage-redundancy).|
| **TDE-encrypted backup performance improvements** | It's now possible to set the point-in-time restore (PITR) backup retention period, and automated compression of backups encrypted with transparent data encryption (TDE) are now 30 percent more efficient in consuming backup storage space, saving costs for the end user. See [Change PITR](../managed-instance/automated-backups-change-settings.md#change-short-term-retention-policy) to learn more. |
| **Azure AD authentication improvements** | Automate user creation using Azure AD applications and create individual Azure AD guest users (preview). To learn more, see [Directory readers in Azure AD](../database/authentication-aad-directory-readers-role.md)|
| **Global VNet peering support** | Global virtual network peering support has been added to SQL Managed Instance, improving the geo-replication experience. See [geo-replication between managed instances](auto-failover-group-configure-sql-mi.md#enabling-connectivity-between-the-instances). |
| **Hosting SSRS catalog databases** | SQL Managed Instance can now host catalog databases of SQL Server Reporting Services (SSRS) for versions 2017 and newer. | 
| **Major performance improvements** | Introducing improvements to SQL Managed Instance performance, including improved transaction log write throughput, improved data and log IOPS for Business Critical instances, and improved TempDB performance. See the [improved performance](https://techcommunity.microsoft.com/t5/azure-sql/announcing-major-performance-improvements-for-azure-sql-database/ba-p/1701256) tech community blog to learn more. 
| **Enhanced management experience** | Using the new [OPERATIONS API](/rest/api/sql/2021-02-01-preview/managed-instance-operations), it's now possible to check the progress of long-running instance operations. To learn more, see [Management operations](management-operations-overview.md?tabs=azure-portal).
| **Machine learning support** | Machine Learning Services with support for R and Python languages now include preview support on Azure SQL Managed Instance (Preview). To learn more, see [Machine learning with SQL Managed Instance](machine-learning-services-overview.md). | 
| **User-initiated failover** | User-initiated failover is now generally available, providing you with the capability to manually initiate an automatic failover using PowerShell, CLI commands, and API calls, improving application resiliency. To learn more, see, [testing resiliency](../database/high-availability-sla.md#testing-application-fault-resiliency). |


## Known issues

The known issues content has moved to a dedicated [known issues in SQL Managed Instance](doc-changes-updates-known-issues.md) article. 


## Contribute to content

To contribute to the Azure SQL documentation, see the [Docs contributor guide](/contribute/).
