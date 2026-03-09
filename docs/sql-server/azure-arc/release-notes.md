---
title: Release Notes
description: Latest release notes for SQL Server enabled by Azure Arc
author: anosov1960
ms.author: sashan
ms.reviewer: mikeray, randolphwest, mathoma
ms.date: 01/27/2026
ms.topic: release-notes
ms.custom:
  - ignite-2025
ai-usage: ai-assisted
---

# Release notes - SQL Server enabled by Azure Arc

[!INCLUDE [sqlserver](../../includes/applies-to-version/sqlserver.md)]

This article lists:

- Features in preview
- Features that have reached generally availability (GA) within the last 12 months
- Release dates
- Azure extension for SQL Server version numbers
- High level feature descriptions
- Links to additional feature documentation

Extension versions are cumulative. Higher extension versions include all of the updates from previous versions. A release might also include internal features.

Only Azure extension for SQL Server versions released within the last year are supported.

## Preview

The following table lists the features of SQL Server enabled by Azure Arc that are currently in preview.

> [!NOTE]  
> Features currently in preview are available under [supplemental terms of use](https://azure.microsoft.com/support/legal/preview-supplemental-terms/). Review the legal terms that apply to features that are in beta, preview, or otherwise not yet released into general availability (GA). SQL Server enabled by Azure Arc provides previews to give you a chance to evaluate and [share feedback with the product group](https://feedback.azure.com/d365community/forum/04fe6ee0-3b25-ec11-b6e6-000d3a4f0da0) on features before they become generally available.

| Feature | Details |
| --- | --- |
| [Automated backups](backup-local.md) | Automatically perform backups to local storage or network shares. |
| [Azure Extension for SQL Server on Linux](connect.md?tabs=linux) | Connect SQL Server on Linux to Azure Arc. |
| [Microsoft Purview: data owner policies](/purview/legacy/how-to-policies-data-owner-authoring-generic) | Manage access to user data in sources that have been registered for Data Policy Enforcement in Microsoft Purview for your SQL Server instances and databases. |
| [Monitoring](sql-monitoring.md) | Monitor SQL Server performance and activity with built-in dashboards in the Azure portal. |
| [Restore to a point in time](point-in-time-restore.md) | Restore a database to a specific point in time. |

## General availability (GA)

The following table lists features of SQL Server enabled by Azure Arc that have reached generally availability (GA) within the last 12 months:

| Feature | GA Month | Details |
| --- | --- | --- |
| [Backup to URL with a managed identity](backup-to-url.md)| November 2025 | Use a managed identity to authenticate to Azure Blob Storage for backups to URL. |
| [Managed identity](managed-identity.md) | November 2025 | Use a managed identity to authenticate to your SQL Server instance starting with [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)]. |
| [Database migration](migrate-to-azure-sql-managed-instance.md) | October 2025 | Migrate your SQL Server databases to Azure SQL Managed Instance directly from the Azure portal. |
| [Client connection summary](sql-connection-summary.md)| October 2025 | View a summary of all client connections to your SQL Server instances. |
| [US Government Virginia region availability](us-government-region.md) | August 2025 | Connect SQL Server instances in US Government Virginia to Azure Arc. |
| [Migration assessment](migration-assessment.md) | June 2025 | Assess your SQL Server instances for migration readiness to Azure SQL. |
| [Move resources](move-resources.md) | October 2024 | Move SQL Server enabled by Azure Arc resources to a new resource group or subscription. |

## Release notes by date

This section lists the release notes by date, starting with the most recent release:

| Date of release | Extension version |
| --- | --- |
| [January 2026](#january-2026) | `1.1.3307.355` (no longer available) |
| [December 2025](#december-2025) | `1.1.3238.350` |
| [October 2025](#october-2025) | `1.1.3211.337` |
| [September 15, 2025](#september-15-2025) | `1.1.3176.319` |
| [August 2025](#august-2025) | `1.1.3139.313` |
| [August 14, 2025](#august-14-2025) | N/A (GA announcement) |
| [July 29, 2025](#july-29-2025) | `1.1.3119.307` |
| [July 2025](#july-2025) | `1.1.3106.305` |
| [June 2025](#june-2025) | `1.1.3089.297` |
| [June 9, 2025](#june-9-2025) | N/A (Preview announcement) |
| [May 2025](#may-2025) | `1.1.3049.285` (no longer available) |
| [April 30, 2025](#april-30-2025) | `1.1.3042.282` |
| [April 14, 2025](#april-14-2025) | `1.1.3021.274` |
| [March 2025](#march-2025) | `1.1.2986.256` |
| [February 2025](#february-2025) | `1.1.2971.246` |
| [January 2025](#january-2025) | `1.1.2914.231` |
| [November 2024](#november-2024) | `1.1.2859.223` |
| [October 2024](#october-2024) | `1.1.2830.214` |

### January 2026

**Extension version**: `1.1.3307.355`

This version is no longer available.

### December 2025

**Extension version**: `1.1.3238.350`

Certain limitations in [US Government Virginia](us-government-region.md) are lifted.

- [Failover cluster instances support](support-for-fci.md) is now generally available in Azure Government Virginia region.

### November 2025

The following features are now generally available (GA):
- [Backup to URL with a managed identity](backup-to-url.md):  Use a managed identity to authenticate to Azure Blob Storage for backups to URL.
- [Managed identity](managed-identity.md): Use a managed identity to authenticate to your SQL Server instance starting with [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)].

### October 2025

**Extension version**: `1.1.3211.337`

The following features are now generally available (GA):
- [Client connection summary](sql-connection-summary.md): You can view a summary of all client connections to your SQL Server instances in the Azure portal.
- [Database migration](migrate-to-azure-sql-managed-instance.md): Migrate your SQL Server databases to Azure SQL Managed Instance directly from the Azure portal.

### September 15, 2025

**Extension version**: `1.1.3176.319`

Certain limitations in [US Government Virginia](us-government-region.md) are lifted. You can now:

- [License physical cores (p-cores) with unlimited virtualization](manage-license-billing.md#unlimited-virtualization)
- [License physical cores (p-cores) without virtual machines](manage-license-billing.md#license-pcores-without-vms)

### August 2025

**Extension version**: `1.1.3139.313`

Certain limitations in US Government Virginia are lifted. You can now onboard environments with:

- [Always On availability groups](manage-availability-group.md)
- Associated SQL Server services:
  [!INCLUDE [sql-server-associated-services](includes/sql-server-associated-services.md)]

### August 14, 2025

SQL Server enabled by Azure Arc is generally available (GA) in US Government Virginia. For more information, review [SQL Server enabled by Azure Arc in US Government](us-government-region.md). In addition to features announced earlier, you can:

- [Subscribe to Extended Security Updates in a production environment](extended-security-updates.md#subscribe-to-extended-security-updates-in-a-production-environment)
- [Manage licensing and billing](manage-license-billing.md)

This release announces the feature is generally available. It doesn't update extension version or other components of any agent or extension.

### July 29, 2025

**Extension version**: `1.1.3119.307`

This release updates the extension to support specific cloud environments.

### July 2025

**Extension version**: `1.1.3106.305`

This release enables the following feature:
- [Migrate to Azure SQL Managed Instance directly from the Azure portal](migrate-to-azure-sql-managed-instance.md)

### June 2025

**Extension version**: `1.1.3089.297`

This release enables the following features:

- Recurring [pay-as-you-go billing](manage-license-billing.md#licensing-and-billing-in-the-production-environment)
- [Migration assessment](migration-assessment.md)
  - Released for general availability
  - Improved user interface
  - Includes retail pricing information to simplify decisions
  - Recommends a single migration target type among the Azure SQL offerings based your migration strategy selection:
    - Minimize cost
    - Modernize to Platform as a service (PaaS)
  - Introduces assessment settings experience to set pricing options and migration strategy to suit your requirements

  For details, review [Assess migration readiness - SQL Server enabled by Azure Arc](migration-assessment.md).

### June 9, 2025

SQL Server enabled by Azure Arc is available as a preview in US Government Virginia with a limited set of features. For more information, review [SQL Server enabled by Azure Arc in US Government Preview](us-government-region.md).

This release announces the preview region availability. It doesn't update extension version or other components of any agent or extension.

### May 2025

**Extension version**: `1.1.3049.285`

This version is no longer available.

### April 30, 2025

**Extension version**: `1.1.3042.282`

This version is no longer available.

### April 14, 2025

**Extension version**: `1.1.3021.274`

### March 2025

**Extension version**: `1.1.2986.256`

This release introduces:

- [SQL Server client connection summary](sql-connection-summary.md) to enable migration planning (preview).

- New Arc SQL Server properties table and stored procedure in `msdb`.

### February 2025

**Extension version**: `1.1.2971.246`

This release introduces:

- Support for Windows 10 and Windows 11.

- Support for SQL Server [pay-as-you-go subscriptions](manage-license-billing.md#licensing-and-billing-in-the-production-environment) and [Extended Security Updates subscriptions](extended-security-updates.md) for the following services:

  [!INCLUDE [sql-server-associated-services](includes/sql-server-associated-services.md)]

  Review [Manage licensing of SQL Server associated services](manage-license-billing.md#manage-ssxs) and [Manage SQL Server ESU subscriptions for the associated services](extended-security-updates.md#esu-subscription-ssxs).

- New documentation that describes how to connect without routing through the Internet: [Connect to Azure with a private path for SQL Server enabled by Azure Arc](configure-private-path.md).

### January 2025

**Extension version**: `1.1.2914.231`

This release includes bug fixes in the high availability and disaster recovery (HADR) detection logic. Client connections to the following system databases are allowed on the free HADR replicas:

- `tempdb`
- `msdb`
- `master`
- `model`

### November 2024

**Extension version**: `1.1.2859.223`

Minor bug fixes. This release doesn't enable or introduce new features.

### October 2024

**Extension version**: `1.1.2830.214`

### Move resources

- Move instances or databases to a different subscription or resource group - general availability. Review [Move SQL Server enabled by Azure Arc resources to a new resource group or subscription](move-resources.md).

### Additional services inventory

Inventory additional service resources in portal (preview):

[!INCLUDE [sql-server-associated-services](includes/sql-server-associated-services.md)]

### ESU update to bill-back calculation

ESU sets the bill-back date to the beginning of the current ESU year based on the timestamp when ESU was enabled, or p-core ESU license was activated. For details, review [SQL Server Extended Security Updates enabled by Azure Arc](extended-security-updates.md).

### Security improvements

Various security improvements for SQL Server enabled by Azure Arc.
