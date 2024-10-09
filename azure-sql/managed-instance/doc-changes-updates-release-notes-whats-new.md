---
title: What's new?
titleSuffix: Azure SQL Managed Instance
description: Learn about the new features and documentation improvements for Azure SQL Managed Instance.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: wiassaf, mathoma
ms.date: 10/09/2024
ms.service: azure-sql-managed-instance
ms.subservice: service-overview
ms.topic: whats-new
ms.custom:
  - references_regions
  - ignite-2023
  - build-2024
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
|[Database watcher for Azure SQL](../database-watcher-overview.md)|Database watcher is a managed monitoring solution for database services in the Azure SQL family. Database watcher collects in-depth workload monitoring data to give you a detailed view of database performance, configuration, and health. Learn more about [database watcher](https://aka.ms/dbwatcher-preview-announcement).|
|[Endpoint policies](./service-endpoint-policies-configure.md) | Configure which Azure Storage accounts can be accessed from a SQL Managed Instance subnet. Grants an extra layer of protection against inadvertent or malicious data exfiltration.|
|[Free SQL Managed Instance](free-offer.md) | Try Azure SQL Managed Instance for free, for the first 12 months after you create your instance. |
|[Instance pools](instance-pools-overview.md) | Share resources between multiple instances in a pool within a single virtual machine. A convenient and cost-efficient way to migrate smaller SQL Server instances to the cloud, and the only way to deploy a 2-vCore managed instance. |
|[JSON native data type](/sql/t-sql/data-types/json-data-type) | The new **JSON** native data type is currently in preview. For more information, see [JSON Type and aggregates preview](https://aka.ms/json-type-aggregates-public-preview). Your SQL managed instance must be configured with the [Always-up-to-date update policy](update-policy.md#always-up-to-date-update-policy).|
|[JSON aggregate functions](/sql/relational-databases/json/json-data-sql-server#json-data-from-aggregates) | Two new **JSON** aggregate functions `JSON_OBJECTAGG` and `JSON_ARRAYAGG` enable construction of JSON objects or arrays based on an aggregate from SQL data. Your SQL managed instance must be configured with the [Always-up-to-date update policy](update-policy.md#always-up-to-date-update-policy). For more information, see [JSON Type and aggregates preview](https://aka.ms/json-type-aggregates-public-preview). |
|[Microsoft Entra nonunique name support](../database/authentication-microsoft-entra-create-users-with-nonunique-names.md) |  The [CREATE USER](/sql/t-sql/statements/create-user-transact-sql) Transact-SQL (T-SQL) syntax has been extended to include `WITH OBJECT_ID` to support creating Microsoft Entra logins and users in Azure SQL Managed Instance that have nonunique names. |
|[Native Windows principals](native-windows-principals.md) | Use the new **Windows** authentication metadata mode to allow Windows authentication or Microsoft Entra authentication (using a Windows principal metadata) with Azure SQL Managed Instance. |
|[Next-gen General Purpose](service-tiers-next-gen-general-purpose-use.md) | An architectural upgrade of the General Purpose service tier that uses managed disks for greater resource flexibility, and improved performance while maintaining the same baseline cost as the General Purpose service tier.  |
|[SDK-style SQL project](/sql/azure-data-studio/extensions/sql-database-project-extension-sdk-style-projects) | Use [Microsoft.Build.Sql](https://www.nuget.org/packages/Microsoft.Build.Sql) for SDK-style SQL projects in the SQL Database Projects extension in Azure Data Studio or Visual Studio Code. SDK-style SQL projects are especially advantageous for applications shipped through pipelines or built in cross-platform environments.|
|[Service Broker](/sql/database-engine/configure-windows/sql-server-service-broker) | Support for cross-instance message exchange using Service Broker between instances of Azure SQL Managed Instance, and between SQL Server and Azure SQL Managed Instance. |
|[Two-way DR with SQL Server 2022](managed-instance-link-disaster-recovery.md) | In the event of a disaster, you can fail your SQL Server 2022 workloads to Azure SQL Managed Instance using the link, and then, once the disaster is mitigated, you can fail back to SQL Server. |
|[Threat detection](threat-detection-configure.md) | Threat detection notifies you of security threats detected to your database. |
|[Zone redundancy for General Purpose](high-availability-sla-local-zone-redundancy.md#zone-redundant-availability) |  Deploy your General Purpose SQL Managed Instance to multiple availability zones to improve the availability of your instance in the event of a disaster. | 


## General availability (GA)

The following table lists features of Azure SQL Managed Instance that have been made generally available (GA) within the last 12 months:

| Feature | GA Month | Details |
| ---| --- |--- |
|[Fail over a link with T-SQL](managed-instance-link-failover-how-to.md?tabs=tsql#fail-over-a-database) | October 2024 | You can now fail over a [Managed Instance link](managed-instance-link-feature-overview.md) by using Transact-SQL (T-SQL) commands. |
|[Link from SQL MI to SQL Server](managed-instance-link-feature-overview.md) | October 2024 | Configure a link *from* Azure SQL Managed Instance to SQL Server 2022. |
|[CURRENT_DATE Transact-SQL](/sql/t-sql/functions/current-date-transact-sql) | August 2024 |  A Transact-SQL (T-SQL) function that returns the current database system date as a date value, without the database time and time zone offset. |
|[Maintenance window advance notifications](advance-notifications.md)| June 2024 | Advance notifications for SQL managed instance [maintenance window](maintenance-window.md) are now generally available.  |
|[Update policy](update-policy.md) | May 2024 | Use the update policy to control your internal database format alignment and access to the latest SQL Database Engine features. You can choose to either limit the feature set to features that are available in SQL Server 2022, or ensure your instance takes advantage of all the latest features of Azure SQL Managed Instance.| 
|[Azure SQL triggers for Azure Functions](/azure/azure-functions/functions-bindings-azure-sql) | March 2024 | Azure Functions supports function triggers for Azure SQL Managed Instance. | 
|[Cross-subscription database copy and move](database-copy-move-how-to.md)| December 2023 | Refresh of database copy and move functionality with added support for cross-subscription operations. |
|[Database copy and move](database-copy-move-how-to.md) | November 2023 | Perform an online database copy or move operation across managed instances. | 
|[Distributed Transaction Coordinator (DTC)](distributed-transaction-coordinator-dtc.md) | November 2023 | Use DTC to run distributed transactions in mixed environments such as across managed instances, SQL Servers, other relational database management systems (RDBMSs), custom applications and other transaction participants hosted in any environment that can establish network connectivity to Azure.  | 
|[Instance stop and start](instance-stop-start-how-to.md) | November 2023 | Stop and start your managed instance to save on licensing and compute costs. | 
|[Ledger](/sql/relational-databases/security/ledger/ledger-overview) | November 2023 | The ledger feature in Azure SQL Managed Instance allows you to cryptographically attest to other parties, such as auditors or other business parties, that your data hasn't been tampered with. |
|[November 2022 feature wave](november-2022-feature-wave-enroll.md) | November 2023 | November 2022 brought a number of new features for Azure SQL Managed Instance, such as [fast provisioning](management-operations-overview.md#fast-provisioning), and [zone redundancy](high-availability-sla-local-zone-redundancy.md#zone-redundant-availability) as well as enhancements to the [virtual cluster](virtual-cluster-architecture.md) and [network security](connectivity-architecture-overview.md). |  
|[Zone-redundancy](../managed-instance/high-availability-sla-local-zone-redundancy.md#zone-redundant-availability) | November 2023 | Deploy your Business Critical SQL Managed Instance to multiple availability zones to improve the availability of your instance in the event of a disaster. | 
|[Double log write throughput limit](resource-limits.md#service-tier-characteristics) | August 2023 | The max log write throughput limit has doubled for the Business Critical tier, up to 192 MiB/s. | 
|[XML compression](https://techcommunity.microsoft.com/t5/azure-sql-blog/general-availability-of-xml-compression-for-azure-sql-database/ba-p/3888861) | August 2023 | XML compression for Azure SQL Managed Instance is now generally available. You can use [ALTER INDEX](/sql/t-sql/statements/alter-index-transact-sql?view=azuresqldb-mi-current&preserve-view=true#xml_compression) to apply XML compression to existing [XML indexes](/sql/relational-databases/xml/xml-indexes-sql-server?view=azuresqldb-mi-current&preserve-view=true). |
|[TDS 8.0 support](/sql/relational-databases/security/networking/tds-8?view=azuresqldb-mi-current&preserve-view=true) | August 2023 | Azure SQL Managed Instance now supports TDS 8.0 for strict encryption of data in transit. |
|[Private endpoints](private-endpoint-overview.md) | August 2023 | Establish secure and isolated connectivity between Azure SQL Managed Instance and multiple virtual networks without exposing the entire network infrastructure of your service [by using a private endpoint](https://techcommunity.microsoft.com/t5/azure-sql-blog/private-endpoints-ga-for-azure-sql-managed-instance/ba-p/3895434). Review these blog posts on [Scenarios with private endpoints](https://techcommunity.microsoft.com/t5/azure-sql-blog/scenarios-with-private-endpoints-to-azure-sql-managed-instance/ba-p/3902001) and [Advanced scenarios with private endpoints to Azure SQL Managed Instance](https://techcommunity.microsoft.com/t5/azure-sql-blog/advanced-scenarios-with-private-endpoints-to-azure-sql-managed/ba-p/3902198). |


## November 2022 feature wave

Changes and capabilities that were introduced in the November 2022 feature wave have been integrated to the majority of instances and are now available by default. Since taking separate action to enroll an instance is no longer necessary, options that mention the November 2022 feature wave have been removed from the Azure portal for instances that have enrolled in the feature wave. The last remaining instances are currently in the enrollment process. 

All new instances on production subscriptions are enrolled in the feature wave by default if they are created in eligible subnets. The following subnet types are eligible:

- Newly created subnet (default)
- Existing subnets that are empty
- Existing subnets that already have the feature wave enabled, and contain only instances **with** the feature wave enabled
- Existing subnets that don't have the feature wave enabled, and contain only instances **without** the feature wave

The benefits in the feature wave include:

- [**Fast instance provisioning**](management-operations-overview.md#fast-provisioning) -  It takes less time to deploy an instance.
- [**Improved network security**](connectivity-architecture-overview.md) - Internal service traffic is now isolated and secured by Microsoft. 
- [**Enhanced virtual cluster**](virtual-cluster-architecture.md) -  The functionality of the underlying virtual cluster is enhanced.

The features available in the wave are:

- [Instance stop/start](instance-stop-start-how-to.md): You can start and stop your instance at your discretion to save on billing costs for vCores and SQL Server licensing.
- [Zone redundancy for Business Critical tier](..//managed-instance/high-availability-sla.md): You can deploy your Business Critical tier managed instance across multiple availability zones to improve the availability of your service.
- [Managed DTC](distributed-transaction-coordinator-dtc.md): Run distributed transactions in mixed environments.


## Documentation changes

Learn about significant changes to the Azure SQL Managed Instance documentation. For previous years, see the [What's new archive](doc-changes-updates-release-notes-whats-new-archive.md).

### October 2024 

| Changes | Details |
| --- | --- |
|**Fail over a link with T-SQL GA** |  You can now fail over a [Managed Instance link](managed-instance-link-feature-overview.md) by using Transact-SQL (T-SQL) commands. This feature is now generally available. Review [Fail over a link with T-SQL](managed-instance-link-failover-how-to.md?tabs=tsql#fail-over-a-database) to learn more. |
|**Link from SQL MI to SQL Server GA** |  Configure a link *from* Azure SQL Managed Instance to SQL Server 2022. This feature is now generally available. Review [Link from SQL MI to SQL Server](managed-instance-link-feature-overview.md)  to learn more.  |

### August 2024

| Changes | Details |
| --- | --- |
| **CURRENT_DATE Transact-SQL GA** | The `CURRENT_DATE` Transact-SQL (T-SQL) function returns the current database system date as a date value, without the database time and time zone offset. This function is now generally available. For more information, see [CURRENT_DATE (Transact-SQL)](/sql/t-sql/functions/current-date-transact-sql). |
| **JSON native data type preview** | The new [**JSON** native data type](/sql/t-sql/data-types/json-data-type) and is currently in preview. For more information, see [JSON Type and aggregates preview](https://aka.ms/json-type-aggregates-public-preview). Your SQL managed instance must be configured with the [Always-up-to-date update policy](update-policy.md#always-up-to-date-update-policy).|
| **JSON aggregate functions preview** | Two new **JSON** aggregate functions [JSON_OBJECTAGG and JSON_ARRAYAGG](/sql/relational-databases/json/json-data-sql-server#json-data-from-aggregates) enable construction of JSON objects or arrays based on an aggregate from SQL data. For more information, see [JSON Type and aggregates preview](https://aka.ms/json-type-aggregates-public-preview). Your SQL managed instance must be configured with the [the Always-up-to-date update policy](update-policy.md#always-up-to-date-update-policy).|
| **Fail over link with T-SQL preview** | You can now fail over a [Managed Instance link](managed-instance-link-feature-overview.md) by using Transact-SQL (T-SQL) commands. This capability is currently in preview starting with [SQL Server 2022 CU13 (KB5036432)](/troubleshoot/sql/releases/sqlserver-2022/cumulativeupdate13). To learn more, review [fail over a database](managed-instance-link-failover-how-to.md?tabs=tsql#fail-over-a-database). |

### July 2024

| Changes | Details |
| --- | --- |
|**Native Windows principals**| [Native Windows principals](native-windows-principals.md) in SQL Managed Instance is in preview. |

### June 2024

| Changes | Details |
| --- | --- |
|**Advance notifications GA**| [Advance notifications](advance-notifications.md) for upcoming SQL managed instance [maintenance windows](maintenance-window.md) are now generally available. |
|**November 2022 feature wave integration** | Changes and capabilities introduced in the November 2022 feature wave are now default for all instances. November 2022 feature selection options are being removed from areas in the Azure portal.| 

### May 2024

| Changes | Details |
| --- | --- |
|**Instance pool in the portal** | It's now possible to create a new instance pool, or a new instance inside an existing instance pool, by using the Azure portal. Review [Configure instance pool](instance-pools-configure.md?view=azure-portal&preserve-view=true#create-instance-pool) to learn more. The instance pool feature remains in preview.  |
|**Update policy GA** | Use the update policy to control your internal database format alignment and access to the latest SQL Database Engine features. You can choose to either limit the feature set to features that are available in SQL Server 2022, or ensure your instance takes advantage of all the latest features of Azure SQL Managed Instance. This feature is generally available. Review [Update policy](update-policy.md) to learn more.  | 
| **Zone redundancy guide** | We've published a guide making it easier for you to enable [zone redundancy](instance-zone-redundancy-configure.md) for Azure SQL Managed Instance. | 

### March 2024

| Changes | Details |
| --- | --- |
|**Azure SQL triggers for Azure Functions GA** | Azure Functions supports function triggers for Azure SQL Managed Instance. This feature is now generally available. Review [Azure SQL triggers for Azure Functions](/azure/azure-functions/functions-bindings-azure-sql) to learn more. |
| **Database watcher for Azure SQL preview** | [Database watcher](../database-watcher-overview.md) is a managed monitoring solution for database services in the Azure SQL family. Database watcher collects in-depth workload monitoring data to give you a detailed view of database performance, configuration, and health. This feature is now in preview.  Learn more about [database watchers](https://aka.ms/dbwatcher-preview-announcement).|
|**Next-gen General Purpose preview** |  An architectural upgrade of the General Purpose service tier that uses managed disks for greater resource flexibility, and improved performance while maintaining the same baseline cost as the General Purpose service tier. This service tier upgrade is currently in preview. Review [Next-gen General Purpose](service-tiers-next-gen-general-purpose-use.md) to learn more.   |

### February 2024

| Changes | Details |
| --- | --- |
| **OBJECT_ID T-SQL syntax preview** | The [CREATE USER](/sql/t-sql/statements/create-user-transact-sql) Transact-SQL (T-SQL) syntax has been extended to include `WITH OBJECT_ID` to support creating Microsoft Entra logins and users in Azure SQL Managed Instance that have nonunique names. Using the `OBJECT_ID` syntax to create users and logins in Azure SQL Managed Instance is currently in preview. To learn more, review [Microsoft Entra nonunique name support](../database/authentication-microsoft-entra-create-users-with-nonunique-names.md). | 

### January 2024

| Changes | Details |
| --- | --- |
|**Instance pool preview refresh** | Instance pools have a number of additional capabilities, such as the ability to deploy a 2-vCore instance. The preview of this feature has been refreshed. Review [instance pools](instance-pools-overview.md#whats-new) to learn more. |


## Archive

For previous news, see the [What's new archive](doc-changes-updates-release-notes-whats-new-archive.md).

## Known issues

The known issues content has moved to a dedicated [known issues in SQL Managed Instance](doc-changes-updates-known-issues.md) article. 


## Contribute to content

To contribute to the Azure SQL documentation, see the [Docs contributor guide](/contribute/).
