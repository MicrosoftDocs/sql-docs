---
title: What's new?
titleSuffix: Azure SQL Database
description: Learn about the new features and documentation improvements for Azure SQL Database.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: mathoma, randolphwest
ms.service: azure-sql-database
ms.date: 08/14/2024
ms.subservice: service-overview
ms.topic: whats-new
ms.custom:
  - ignite-2023
  - build-2024
monikerRange: "=azuresql||=azuresql-db"
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
| [Copilot skills in Azure SQL Database](../copilot/copilot-azure-sql-overview.md) | Microsoft Copilot skills in Azure SQL Database include two Azure portal experiences: [Natural language to SQL](../copilot/query-editor-natural-language-to-sql-copilot.md) within the [Azure portal query editor](query-editor.md), and [Azure Copilot integration](../copilot/copilot-azure-sql-overview.md#microsoft-copilot-in-azure-enhanced-scenarios). |
| [Database watcher for Azure SQL](../database-watcher-overview.md)|Database watcher is a managed monitoring solution for database services in the Azure SQL family. Database watcher collects in-depth workload monitoring data to give you a detailed view of database performance, configuration, and health. Learn more about [database watcher](https://aka.ms/dbwatcher-preview-announcement). |
| [Degrees of Parallelism (DOP) feedback](/sql/relational-databases/performance/intelligent-query-processing-degree-parallelism-feedback) | DOP Feedback is currently available as a limited preview. For more information and how to apply for the preview, see [Announcing Degree of Parallelism Feedback Limited Preview](https://techcommunity.microsoft.com/t5/azure-sql-blog/announcing-degree-of-parallelism-feedback-limited-preview/ba-p/3806924). |
| [Elastic queries](elastic-query-overview.md) | The elastic queries feature allows for cross-database queries in Azure SQL Database. |
| [Elastic transactions](elastic-transactions-overview.md) | Elastic transactions allow you to execute transactions distributed among cloud databases in Azure SQL Database. |
| [Fabric mirrored databases](/fabric/database/mirrored-database/overview) | With Fabric Mirroring, you can [mirror databases in Azure SQL Database to Microsoft Fabric](/fabric/database/mirrored-database/overview). You can continuously replicate your existing data estate directly into Fabric's OneLake, including data from Azure SQL Database.|
| [Free Azure SQL Database](free-offer.md) | Try Azure SQL Database for free, for the life of your subscription. This free offer provides a General Purpose database with 100,000 vCore seconds of compute every month. |
| [Fixed server roles](security-server-roles.md) |  To simplify permission management, Azure SQL Database provides a set of fixed server-level roles to help you manage the permissions on a logical server. | 
| [Hyperscale elastic pools](hyperscale-elastic-pool-overview.md) | Manage and scale multiple Hyperscale databases in Azure SQL Database by using Hyperscale elastic pools. |
| [Hyperscale elastic pool maintenance window support](maintenance-window.md) | You can now configure a non-default [maintenance window](maintenance-window.md) for a [Hyperscale elastic pool](hyperscale-elastic-pool-overview.md). For more information, read [Blog: Maintenance window support for Azure SQL Database Hyperscale elastic pools](https://aka.ms/hsep-fmw). |
| [Hyperscale elastic pools Premium-series hardware](hyperscale-elastic-pool-overview.md) | Premium-series and premium-series memory optimized hardware is in preview for Hyperscale elastic pools. |
| [Hyperscale elastic pools with zone redundancy](hyperscale-elastic-pool-overview.md) | You can now create zone redundant elastic pools in the Hyperscale service tier. You can migrate existing zone-redundant Hyperscale databases into elastic pools. For more information, read [Blog post: Zone redundant Hyperscale elastic pools](https://aka.ms/hsep-zr). |
| [Hyperscale support for database and file shrink](file-space-manage.md)  | Database and file shrink commands are supported in preview for Azure SQL Database Hyperscale. For more information, see [Shrink for Azure SQL Database Hyperscale](https://aka.ms/hs-shrink-preview). |
| [Import and export using Private Link](database-import-export-private-link.md) | Leave *Allow Access to Azure Services* off when you import or export a database using a service-managed endpoint. |
| [JSON native data type](/sql/t-sql/data-types/json-data-type?view=azuresqldb-current&preserve-view=true) | The new native **json** data type and new JSON aggregate functions are currently in preview. For more information, see [JSON Type and aggregates preview](https://aka.ms/json-type-aggregates-public-preview). |
| [JSON aggregate functions](/sql/relational-databases/json/json-data-sql-server?view=azuresqldb-current&preserve-view=true#json-data-from-aggregates) | Two new **json** aggregate functions `JSON_OBJECTAGG` and `JSON_ARRAYAGG` enable construction of JSON objects or arrays based on an aggregate from SQL data. For more information, see [JSON Type and aggregates preview](https://aka.ms/json-type-aggregates-public-preview). |
| [License-free standby replica](standby-replica-how-to-configure.md) | Save on licensing costs by configuring your secondary database replica for disaster recovery standby. |
| [Microsoft Entra nonunique name support](authentication-microsoft-entra-create-users-with-nonunique-names.md) | The [CREATE USER](/sql/t-sql/statements/create-user-transact-sql) Transact-SQL (T-SQL) syntax has been extended to include `WITH OBJECT_ID` to support creating Microsoft Entra logins and users in Azure SQL Database that have nonunique names. |
| [Query editor in the Azure portal](query-editor.md) | The query editor in the portal allows you to run queries against your Azure SQL Database directly from the [Azure portal](https://portal.azure.com). |
| [SQL Analytics](/azure/azure-monitor/insights/azure-sql) | Azure SQL Analytics is an advanced cloud monitoring solution for monitoring performance of all of your Azure SQL databases at scale and across multiple subscriptions in a single view. Azure SQL Analytics collects and visualizes key performance metrics with built-in intelligence for performance troubleshooting. |
| [SQL Database emulator](local-dev-experience-sql-database-emulator.md) | The Azure SQL Database emulator provides the ability to locally validate database and query design together with client application code in a simple and frictionless model as part of the application development process. |
| [UNISTR (Transact-SQL)](/sql/t-sql/functions/unistr-transact-sql) | Azure SQL Database now supports the `UNISTR` T-SQL syntax for Unicode string literals. For more information, see [UNISTR (Transact-SQL)](/sql/t-sql/functions/unistr-transact-sql).|
| [\|\|](/sql/t-sql/language-elements/string-concatenation-pipes-transact-sql) and [\|\|=](/sql/t-sql/language-elements/compound-assignment-pipes-transact-sql) syntax support | Azure SQL Database now supports [\|\| (String concatenation)](/sql/t-sql/language-elements/string-concatenation-pipes-transact-sql) and [\|\|= (Compound assignment)](/sql/t-sql/language-elements/compound-assignment-pipes-transact-sql) Transact-SQL syntax.|

## General availability (GA)

The following table lists features of Azure SQL Database that have been made generally available (GA) within the last 12 months:

| Feature | GA Month | Details |
| --- | --- | --- |
| [Database compatibility level 160 is now default](/sql/t-sql/statements/alter-database-transact-sql-compatibility-level?view=azuresqldb-current&preserve-view=true) | June 2024 | Database compatibility level 160 is now the default for new databases created in Azure SQL Database. For more information on this announcement, see [General availability: Database compatibility level 160 in Azure SQL Database](https://techcommunity.microsoft.com/t5/azure-sql-blog/general-availability-database-compatibility-level-160-in-azure/ba-p/4172039). |
| [Hyperscale named replica zone redundant support](service-tier-hyperscale-replicas.md) | June 2024 | [Zone redundancy support for Hyperscale named replicas](https://aka.ms/ZRSupportForNRPreview) is now generally available. |
| [License-free standby replica](standby-replica-how-to-configure.md) | May 2024 | Save on licensing costs by configuring your secondary database replica for disaster recovery standby. | 
| [Elastic jobs](elastic-jobs-overview.md) | April 2024 | [Elastic jobs, now generally available](https://techcommunity.microsoft.com/t5/azure-sql-blog/general-availability-elastic-jobs-in-azure-sql-database/ba-p/4087140), are the SQL Server Agent replacement for Azure SQL Database. Elastic jobs  support Microsoft Entra ID authentication, private endpoints, management via REST APIs, Azure Alerts, and more new features since public preview began. |
| [Maintenance window advance notifications](advance-notifications.md) | March 2024 | Advance notifications are now generally available for databases configured to use a nondefault [maintenance window](maintenance-window.md). |
| [Azure SQL triggers for Azure Functions](/azure/azure-functions/functions-bindings-azure-sql-trigger) | March 2024 | Azure Functions supports function triggers for Azure SQL Database. |
| [Serverless Hyperscale](serverless-tier-overview.md) | February 2024 | Automatically scale your Hyperscale databases up and down based on usage when using the serverless compute tier, now generally available. |
| [Lower, simplified pricing for Azure SQL Database Hyperscale](https://aka.ms/hsignite2023) | December 2023 | Simplified pricing for Azure SQL Database Hyperscale has arrived! For pricing change details, see [Azure SQL Database Hyperscale – lower, simplified pricing!](https://techcommunity.microsoft.com/t5/azure-sql-blog/azure-sql-database-hyperscale-lower-simplified-pricing/ba-p/3982209).|
| [Always Encrypted with VBS enclaves](always-encrypted-enclaves-getting-started-vbs.md) | November 2023 | Take advantage of rich confidential queries and in-place encryption operations for Azure SQL Database with Always Encrypted with virtualization-based security (VBS) enclaves. |
| [DC-series hardware up to 40 vCores](resource-limits-vcore-single-databases.md) | November 2023 | DC-series hardware from 10 to 40 vCores for General Purpose, Business Critical, and Hyperscale provisioned compute. |

## Documentation changes

Learn about significant changes to the Azure SQL Database documentation. For previous years, see the [What's new archive](doc-changes-updates-release-notes-whats-new-archive.md).

### August 2024

| Changes | Details |
| --- | --- |
| **Local development experience for Azure SQL Database using Dev Container Templates**| We've provided details and quickstarts to get started with the new [local dev experience for Azure SQL Database](local-dev-experience-dev-containers.md). Dev Container Templates replace the previous local emulator experience. To get started, see [Quickstart: Set up a development environment with Dev Container Templates for Azure SQL Database](local-dev-experience-dev-containers-quickstart.md).|

### July 2024

| Changes | Details |
| --- | --- |
| **Hyperscale support for database and file shrink** | Database and file shrink commands are supported in preview for Azure SQL Database Hyperscale. For more information, see [Shrink for Azure SQL Database Hyperscale](https://aka.ms/hs-shrink-preview). |
| **TLS 1.0 and 1.1 retirement** | Azure has announced that support for older TLS versions (TLS 1.0, and 1.1) ends on October 31, 2024. To learn more about the impact to Azure SQL Database, review [connectivity settings](connectivity-settings.md#upcoming-retirement-changes). | 

### June 2024

| Changes | Details |
| --- | --- |
| **Database compatibility level 160 is now default** | [Database compatibility level 160 is now the default](/sql/t-sql/statements/alter-database-transact-sql-compatibility-level?view=azuresqldb-current&preserve-view=true) for new databases created in Azure SQL Database. For more information on this announcement, see [General availability: Database compatibility level 160 in Azure SQL Database](https://techcommunity.microsoft.com/t5/azure-sql-blog/general-availability-database-compatibility-level-160-in-azure/ba-p/4172039). |
| **Hyperscale named replica zone redundant support** | [Zone redundancy support for Hyperscale named replicas](service-tier-hyperscale-replicas.md) are now generally available. For more information, see [Blog: Zone redundancy for Hyperscale named replicas](https://aka.ms/ZRSupportForNRPreview).|
| **UNISTR (preview)** | Azure SQL Database now supports the `UNISTR`. This syntax is currently in preview. For more information, see [UNISTR (Transact-SQL)](/sql/t-sql/functions/unistr-transact-sql). |
| **\|\| and \|\|= string concatenation support** | Azure SQL Database now supports \|\| and \|\|= compound assignment T-SQL syntax. This syntax is currently in preview. For more information, see [&#124;&#124; (String concatenation)](/sql/t-sql/language-elements/string-concatenation-pipes-transact-sql) and [&#124;&#124;= (Compound assignment)](/sql/t-sql/language-elements/compound-assignment-pipes-transact-sql). |

### June 2024

| Changes | Details |
| --- | --- |
| **TLS 1.3 support** | Azure SQL Database now supports connections encrypted with TLS 1.3. Review [TLS 1.3](/sql/relational-databases/security/networking/tls-1-3) and [Minimum TLS settings](connectivity-settings.md#minimum-tls-version) to learn more. | 

### May 2024

| Changes | Details |
| --- | --- |
| **Availability metric preview**| Availability is now a metric in the Azure Monitor metrics. Driven by a variety of user connection failures, you can [monitor and configure alerts on Azure SQL Database Availability](monitoring-metrics-alerts.md#availability-metric). This feature is currently in preview. |
| **JSON native data type** | The new [native **json** data type](/sql/t-sql/data-types/json-data-type?view=azuresqldb-current&preserve-view=true) and new JSON aggregate functions are currently in preview. For more information, see [JSON Type and aggregates preview](https://aka.ms/json-type-aggregates-public-preview). |
| **JSON aggregate functions** | Two new **json** aggregate functions [JSON_OBJECTAGG and JSON_ARRAYAGG](/sql/relational-databases/json/json-data-sql-server?view=azuresqldb-current&preserve-view=true#json-data-from-aggregates) enable construction of JSON objects or arrays based on an aggregate from SQL data. For more information, see [JSON Type and aggregates preview](https://aka.ms/json-type-aggregates-public-preview). |
| **License-free standby replica GA**| Save on licensing costs by configuring your secondary database replica for disaster recovery standby. This feature is now generally available. Review [License-free standby replica](standby-replica-how-to-configure.md)  to learn more. | 

### April 2024

| Changes | Details |
| --- | --- |
| **Elastic jobs GA**| [Elastic jobs](elastic-jobs-overview.md), [now generally available](https://techcommunity.microsoft.com/t5/azure-sql-blog/general-availability-elastic-jobs-in-azure-sql-database/ba-p/4087140), are the SQL Server Agent replacement for Azure SQL Database. Elastic jobs support Microsoft Entra ID authentication, private endpoints, management via REST APIs, Azure Alerts, and more new features since public preview began. |
|**Convert existing replica to standby** | It's now possible to convert an existing geo-replica to standby or an existing standby replica back to a regular geo-replica by using the Azure portal and REST API. Review [Standby replica](standby-replica-how-to-configure.md) to learn more. |

### March 2024

| Changes | Details |
| --- | --- |
| **Advanced notifications for maintenance windows in Azure SQL Database GA** | [Advance notifications](advance-notifications.md) for [maintenance windows](maintenance-window.md) are now generally available for Azure SQL Database. |
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

## Archive

For previous updates, see the [What's new archive](doc-changes-updates-release-notes-whats-new-archive.md).

## Contribute to content

To contribute to the Azure SQL documentation, see the [Docs contributor guide](/contribute/).
