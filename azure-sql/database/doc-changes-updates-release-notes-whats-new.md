---
title: What's new?
titleSuffix: Azure SQL Database
description: Learn about the new features and documentation improvements for Azure SQL Database.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: mathoma, randolphwest
ms.date: 05/21/2024
ms.service: sql-database
ms.subservice: service-overview
ms.topic: whats-new
ms.custom: ignite-2023, build-2024
monikerRange: "= azuresql || = azuresql-db"
---
# What's new in Azure SQL Database?

[!INCLUDE [appliesto-sqldb](../includes/appliesto-sqldb.md)]

> [!div class="op_single_selector"]
> * [Azure SQL Database](doc-changes-updates-release-notes-whats-new.md?view=azuresql&preserve-view=true)
> * [Azure SQL Managed Instance](../managed-instance/doc-changes-updates-release-notes-whats-new.md?view=azuresql&preserve-view=true)
> * [SQL Server on Azure VMs](../virtual-machines/windows/doc-changes-updates-release-notes-whats-new.md?view=azuresql&preserve-view=true)

This article summarizes the documentation changes associated with new features and improvements in the recent releases of [Azure SQL Database](https://azure.microsoft.com/products/azure-sql/database/). For more information about Azure SQL Database, see [What is Azure SQL Database?](sql-database-paas-overview.md).

> [!TIP]
> For more announcements, discussion, and community content, see the [Azure SQL Database blog](https://techcommunity.microsoft.com/t5/azure-sql-blog/bg-p/AzureSQLBlog).

[!INCLUDE [entra-id](../includes/entra-id.md)]

## Preview

The following table lists the features of Azure SQL Database that are currently in preview.

> [!NOTE]  
> Features currently in preview are available under [supplemental terms of use](https://azure.microsoft.com/support/legal/preview-supplemental-terms/), review for legal terms that apply to Azure features that are in beta, preview, or otherwise not yet released into general availability. Azure SQL Database provides previews to give you a chance to evaluate and [share feedback with the product group](https://feedback.azure.com/d365community/forum/ef2b2b38-2f25-ec11-b6e6-000d3a4f0f84) on features before they become generally available (GA).

| Feature | Details |
| --- | --- |
| [Availability metric](monitoring-metrics-alerts.md#availability-metric)| Availability is now a metric in the Azure Monitor metrics. Driven by a variety of user connection failures, you can [monitor and configure alerts on Azure SQL Database Availability](monitoring-metrics-alerts.md#availability-metric). |
| [Database watcher for Azure SQL](../database-watcher-overview.md)|Database watcher is a managed monitoring solution for database services in the Azure SQL family. Database watcher collects in-depth workload monitoring data to give you a detailed view of database performance, configuration, and health. Learn more about [database watcher](https://aka.ms/dbwatcher-preview-announcement). |
| [Copilot skills in Azure SQL Database](../copilot/copilot-azure-sql-overview.md) | Microsoft Copilot skills in Azure SQL Database include two Azure portal experiences: [Natural language to SQL](../copilot/query-editor-natural-language-to-sql-copilot.md) within the [Azure portal query editor](query-editor.md), and [Azure Copilot integration](../copilot/copilot-azure-sql-overview.md#microsoft-copilot-in-azure-enhanced-scenarios). |
| [Degrees of Parallelism (DOP) feedback](/sql/relational-databases/performance/intelligent-query-processing-degree-parallelism-feedback) | DOP Feedback is currently available as a limited preview. For more information and how to apply for the preview, see [Announcing Degree of Parallelism Feedback Limited Preview](https://techcommunity.microsoft.com/t5/azure-sql-blog/announcing-degree-of-parallelism-feedback-limited-preview/ba-p/3806924). |
| [Elastic queries](elastic-query-overview.md) | The elastic queries feature allows for cross-database queries in Azure SQL Database. |
| [Elastic transactions](elastic-transactions-overview.md) | Elastic transactions allow you to execute transactions distributed among cloud databases in Azure SQL Database. |
| [Fabric mirrored databases](/fabric/database/mirrored-database/overview) | With Fabric Mirroring, you can [mirror databases in Azure SQL Database to Microsoft Fabric](/fabric/database/mirrored-database/overview). You can continuously replicate your existing data estate directly into Fabric's OneLake, including data from Azure SQL Database.|
| [Free Azure SQL Database](free-offer.md) | Try Azure SQL Database for free, for the life of your subscription. This free offer provides a General Purpose database with 100,000 vCore seconds of compute every month. |
| [Hyperscale elastic pools](hyperscale-elastic-pool-overview.md) | Manage and scale multiple Hyperscale databases in Azure SQL Database by using Hyperscale elastic pools. |
| [Hyperscale elastic pool maintenance window support](maintenance-window.md) | You can now configure a non-default [maintenance window](maintenance-window.md) for a [Hyperscale elastic pool](hyperscale-elastic-pool-overview.md). For more information, read [Blog: Maintenance window support for Azure SQL Database Hyperscale elastic pools](https://aka.ms/hsep-fmw). |
| [Hyperscale elastic pools Premium-series hardware](hyperscale-elastic-pool-overview.md) | Premium-series and premium-series memory optimized hardware is in preview for Hyperscale elastic pools. |
| [Hyperscale elastic pools with zone redundancy](hyperscale-elastic-pool-overview.md) | You can now create zone redundant elastic pools in the Hyperscale service tier. You can migrate existing zone-redundant Hyperscale databases into elastic pools. For more information, read [Blog post: Zone redundant Hyperscale elastic pools](https://aka.ms/hsep-zr). |
| [Hyperscale named replica zone redundant support](service-tier-hyperscale-replicas.md) | [Zone redundancy support for Hyperscale named replicas](https://aka.ms/ZRSupportForNRPreview) is in preview. |
| [License-free standby replica](standby-replica-how-to-configure.md) | Save on licensing costs by configuring your secondary database replica for disaster recovery standby. | 
| [Microsoft Entra nonunique name support](authentication-microsoft-entra-create-users-with-nonunique-names.md) | The [CREATE USER](/sql/t-sql/statements/create-user-transact-sql) Transact-SQL (T-SQL) syntax has been extended to include `WITH OBJECT_ID` to support creating Microsoft Entra logins and users in Azure SQL Database that have nonunique names. |
| [Query editor in the Azure portal](query-editor.md) | The query editor in the portal allows you to run queries against your Azure SQL Database directly from the [Azure portal](https://portal.azure.com). |
| [SQL Analytics](/azure/azure-monitor/insights/azure-sql) | Azure SQL Analytics is an advanced cloud monitoring solution for monitoring performance of all of your Azure SQL databases at scale and across multiple subscriptions in a single view. Azure SQL Analytics collects and visualizes key performance metrics with built-in intelligence for performance troubleshooting. |
| [SQL Database emulator](local-dev-experience-sql-database-emulator.md) | The Azure SQL Database emulator provides the ability to locally validate database and query design together with client application code in a simple and frictionless model as part of the application development process. |

## General availability (GA)

The following table lists features of Azure SQL Database that have been made generally available (GA) within the last 12 months:

| Feature | GA Month | Details |
| --- | --- | --- |
| [Elastic jobs](elastic-jobs-overview.md) | April 2024 | [Elastic jobs, now generally available](https://techcommunity.microsoft.com/t5/azure-sql-blog/general-availability-elastic-jobs-in-azure-sql-database/ba-p/4087140), are the SQL Server Agent replacement for Azure SQL Database. Elastic jobs  support Microsoft Entra ID authentication, private endpoints, management via REST APIs, Azure Alerts, and more new features since public preview began. |
| [Maintenance window advance notifications](advance-notifications.md) | March 2024 | Advance notifications are now generally available for databases configured to use a nondefault [maintenance window](maintenance-window.md). |
| [Azure SQL triggers for Azure Functions](/azure/azure-functions/functions-bindings-azure-sql-trigger) | March 2024 | Azure Functions supports function triggers for Azure SQL Database. |
| [Serverless Hyperscale](serverless-tier-overview.md) | February 2024 | Automatically scale your Hyperscale databases up and down based on usage when using the serverless compute tier, now generally available. |
| [Lower, simplified pricing for Azure SQL Database Hyperscale](https://aka.ms/hsignite2023) | December 2023 | Simplified pricing for Azure SQL Database Hyperscale has arrived! For pricing change details, see [Azure SQL Database Hyperscale – lower, simplified pricing!](https://techcommunity.microsoft.com/t5/azure-sql-blog/azure-sql-database-hyperscale-lower-simplified-pricing/ba-p/3982209).|
| [Always Encrypted with VBS enclaves](always-encrypted-enclaves-getting-started-vbs.md) | November 2023 | Take advantage of rich confidential queries and in-place encryption operations for Azure SQL Database with Always Encrypted with virtualization-based security (VBS) enclaves. |
| [DC-series hardware up to 40 vCores](resource-limits-vcore-single-databases.md) | November 2023 | DC-series hardware from 10 to 40 vCores for General Purpose, Business Critical, and Hyperscale provisioned compute. |

## Documentation changes

Learn about significant changes to the Azure SQL Database documentation. For previous years, see the [What's new archive](doc-changes-updates-release-notes-whats-new-archive.md).

### May 2024

| Changes | Details |
| --- | --- |
| **Availability metric (preview)**| Availability is now a metric in the Azure Monitor metrics. Driven by a variety of user connection failures, you can [monitor and configure alerts on Azure SQL Database Availability](monitoring-metrics-alerts.md#availability-metric). |

### April 2024

| Changes | Details |
| --- | --- |
| **Elastic jobs GA**| [Elastic jobs](elastic-jobs-overview.md), [now generally available](https://techcommunity.microsoft.com/t5/azure-sql-blog/general-availability-elastic-jobs-in-azure-sql-database/ba-p/4087140), are the SQL Server Agent replacement for Azure SQL Database. Elastic jobs support Microsoft Entra ID authentication, private endpoints, management via REST APIs, Azure Alerts, and more new features since public preview began. |
|**Convert existing replica to standby** | It's now possible to convert an existing geo-replica to standby or an existing standby replica back to a regular geo-replica by using the Azure portal and REST API. Review [Standby replica](standby-replica-how-to-configure.md) to learn more. |

### March 2024

| Changes | Details |
| --- | --- |
| **Advanced notifications for maintenance windows in Azure SQL Database** | [Advance notifications](advance-notifications.md) for [maintenance windows](maintenance-window.md) are now generally available for Azure SQL Database. |
| **Maintenance window for Hyperscale elastic pools (preview)** | You can now configure a non-default [maintenance window](maintenance-window.md) for a [Hyperscale elastic pool](hyperscale-elastic-pool-overview.md). For more information, read [Blog: Maintenance window support for Azure SQL Database Hyperscale elastic pools](https://aka.ms/hsep-fmw). |
| **Copilot skills in Azure SQL Database preview** |  Microsoft Copilot skills in Azure SQL Database include two Azure portal experiences: [Natural language to SQL](../copilot/query-editor-natural-language-to-sql-copilot.md) within the [Azure portal query editor](query-editor.md), and [Azure Copilot integration](../copilot/copilot-azure-sql-overview.md#microsoft-copilot-in-azure-enhanced-scenarios). |
| **Fabric Mirrored Databases (Preview)** | You can now [mirror databases in Azure SQL Database to Microsoft Fabric](/fabric/database/mirrored-database/overview). You can continuously replicate your existing data estate directly into Fabric's OneLake, including data from Azure SQL Database. |
| **Hyperscale named replica zone redundant support preview** | [Zone redundancy support for Hyperscale named replicas](service-tier-hyperscale-replicas.md) is now available in preview. For more information, see [Blog: Zone redundancy for Hyperscale named replicas](https://aka.ms/ZRSupportForNRPreview).|
|**Azure SQL triggers for Azure Functions GA** | Azure Functions supports function triggers for Azure SQL Database. This feature is now generally available. Review [Azure SQL triggers for Azure Functions](/azure/azure-functions/functions-bindings-azure-sql) to learn more. |
| **Database watcher for Azure SQL preview** | [Database watcher](../database-watcher-overview.md) is a managed monitoring solution for database services in the Azure SQL family. Database watcher collects in-depth workload monitoring data to give you a detailed view of database performance, configuration, and health. This feature is now in preview.  Learn more about [database watchers](https://aka.ms/dbwatcher-preview-announcement).|

### February 2024

| Changes | Details |
| --- | --- |
| **Zone redundancy now available for Hyperscale elastic pools (preview)** | You can now create [zone redundant elastic pools in the Hyperscale service tier](hyperscale-elastic-pool-overview.md). You can migrate existing zone-redundant Hyperscale databases into elastic pools. For more information, read [Blog post: Zone redundant Hyperscale elastic pools](https://aka.ms/hsep-zr). |
| **Serverless Hyperscale GA** | Automatically scale your Hyperscale databases up and down based on usage when using the [Serverless Hyperscale](serverless-tier-overview.md) tier, now generally available. |
| **OBJECT_ID T-SQL syntax preview** |The [CREATE USER](/sql/t-sql/statements/create-user-transact-sql) Transact-SQL (T-SQL) syntax has been extended to include `WITH OBJECT_ID` to support creating Microsoft Entra logins and users in Azure SQL Database that have nonunique names. Using `WITH OBJECT_ID` to create users and logins in Azure SQL Database is currently in preview. To learn more, review [Microsoft Entra nonunique name support](authentication-microsoft-entra-create-users-with-nonunique-names.md). | 

### January 2024

| Changes | Details |
| --- | --- |
| **New tutorial: Develop a Kubernetes Application for Azure SQL Database**| A new tutorial is available to demonstrate [how to develop a modern application using Python, Docker Containers, Kubernetes, and Azure SQL Database](develop-kubernetes-application.md). |

### December 2023

| Changes | Details |
| --- | --- |
| **Lower, simplified Hyperscale pricing** | Simplified [pricing for Azure SQL Database Hyperscale](service-tier-hyperscale.md?view=azuresql-db&preserve-view=true#hyperscale-pricing-model) has arrived! Review the [new pricing tier for Azure SQL Database Hyperscale announcement](https://aka.ms/hsignite2023), and for pricing change details, see [Azure SQL Database Hyperscale – lower, simplified pricing!](https://techcommunity.microsoft.com/t5/azure-sql-blog/azure-sql-database-hyperscale-lower-simplified-pricing/ba-p/3982209).|

### November 2023

| Changes | Details |
| --- | --- |
|**Use Azure Monitor metrics to monitor databases and elastic pools**|A new reference for monitoring Azure SQL Database with Azure Monitor is available, including [a set of recommended alert rules to monitor Azure SQL Database with Azure Monitor metrics and alerts](monitoring-metrics-alerts.md).|
|**Always Encrypted with VBS enclaves GA** | Take advantage of rich confidential queries and in-place encryption operations for Azure SQL Database with Always Encrypted with virtualization-based security (VBS) enclaves. This feature is now generally available. Review [Always Encrypted with VBS enclaves](always-encrypted-enclaves-getting-started-vbs.md) to get started. |
|**DC-series hardware up to 40 vCores GA** | DC-series hardware from 10 to 40 vCores is now generally available for General Purpose, Business Critical, and Hyperscale provisioned compute. Review [Resource limits](resource-limits-vcore-single-databases.md) for more information. |
| **Elastic jobs preview refresh** | [Updated with a preview refresh and new capabilities](https://techcommunity.microsoft.com/t5/azure-sql-blog/azure-sql-database-elastic-jobs-preview-refresh/ba-p/3965759), [elastic jobs](elastic-jobs-overview.md) are the SQL Server Agent replacement for Azure SQL Database. Elastic jobs now support Microsoft Entra ID (formerly Azure Active Directory) authentication, private endpoints, management via REST APIs, Azure Alerts, and new capabilities and user interface in the Azure portal. Job Agents now provide four capacity tiers to scale concurrency for job execution. |
|**License-free standby replica preview** | Save on licensing costs by configuring your secondary database replica for disaster recovery standby. This feature is currently in preview. Review [License-free standby replica](standby-replica-how-to-configure.md) to learn more.  | 
|**Premium-series hardware for Hyperscale elastic pools preview** | Premium-series and premium-series memory optimized hardware is now in preview for [Hyperscale elastic pools](hyperscale-elastic-pool-overview.md). |

## Archive

For previous updates, see the [What's new archive](doc-changes-updates-release-notes-whats-new-archive.md).

## Contribute to content

To contribute to the Azure SQL documentation, see the [Docs contributor guide](/contribute/).
